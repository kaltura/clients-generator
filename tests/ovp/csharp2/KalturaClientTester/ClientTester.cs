// ===================================================================================================
//                           _  __     _ _
//                          | |/ /__ _| | |_ _  _ _ _ __ _
//                          | ' </ _` | |  _| || | '_/ _` |
//                          |_|\_\__,_|_|\__|\_,_|_| \__,_|
//
// This file is part of the Kaltura Collaborative Media Suite which allows users
// to do with audio, video, and animation what Wiki platfroms allow them to do with
// text.
//
// Copyright (C) 2006-2011  Kaltura Inc.
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as
// published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
//
// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//
// @ignore
// ===================================================================================================
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Kaltura;
using Kaltura.Enums;
using Kaltura.Types;
using Kaltura.Request;
using Kaltura.Services;

namespace Kaltura.Tester
{
    class ClientTester : ILogger
    {
        private const int PARTNER_ID = @YOUR_PARTNER_ID@; //enter your partner id
        private const string ADMIN_SECRET = "@YOUR_ADMIN_SECRET@"; //enter your admin secret
        private const string SERVICE_URL = "@SERVICE_URL@";
        private const string USER_ID = "testUser";

        private static int code = 0;
        private static HashSet<string> tests = new HashSet<string>();
        
        private static string uniqueTag;

        public void Log(string msg)
        {
            Console.WriteLine(msg);
        }

        abstract class BaseTest
        {
            protected Client client;
            protected string id;

            public BaseTest()
            {
                client = new Client(GetConfig());
                client.KS = client.GenerateSession(PARTNER_ID, ADMIN_SECRET, USER_ID, SessionType.ADMIN, 86400, "");

                id = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 20);
            }

            abstract public void test();

            public string getId()
            {
                return id;
            }

            public void OnError(Exception error)
            {
                if (error != null)
                {
                    Console.WriteLine("ERROR: " + error.Message);
                    code = -1;
                    done(id);
                    return;
                }
            }
        }

        class ChunkUpload
        {
            // setting chunk size to a small chunk, because demo file is 500k size.
            // in actual implementation, a chunk size of 10MB is good practice.
            public const int CHUNK_SIZE = 10240;

            private Stream chunkFile;
            private OnCompletedHandler<UploadToken> handler;
            private bool done = false;

            public ChunkUpload(OnCompletedHandler<UploadToken> handler, Client client, string tokenId, FileStream fileStream, long offset, bool resume = true, bool finalChunk = false)
            {
                this.handler = handler;

                byte[] chunk = new byte[CHUNK_SIZE];
                fileStream.Seek(offset, SeekOrigin.Begin);
                fileStream.Read(chunk, 0, CHUNK_SIZE);
                chunkFile = new MemoryStream(chunk);
                UploadTokenService.Upload(tokenId, chunkFile, resume, finalChunk, offset)
                    .SetCompletion(new OnCompletedHandler<UploadToken>(OnComplete))
                    .Execute(client);
            }

            public void OnComplete(UploadToken uploadToken, Exception error)
            {
                done = true;
                chunkFile.Close();
                handler(uploadToken, error);
            }

            public bool IsDone()
            {
                return done;
            }
        }

        class SampleThreadedChunkUploadTest : BaseTest
        {
            const string fname = "DemoVideo.flv";
            const string mediaName = "C# Media Entry Uploaded in chunks using threads";

            private UploadToken token;
            private MediaEntry entry;

            private FileStream fileStream;
            private int maxUploadThreads = 4;
            private int lastOffset;
            private long fileSize;
            private List<ChunkUpload> uploads = new List<ChunkUpload>();

            public override void test()
            {
                UploadToken myToken = new UploadToken();
                myToken.FileName = fname;
                FileInfo f = new FileInfo(fname);
                myToken.FileSize = f.Length;


                UploadTokenService.Add(myToken)
                    .SetCompletion(new OnCompletedHandler<UploadToken>(OnUploadTokenAddComplete))
                    .Execute(client);

                MediaEntry mediaEntry = new MediaEntry();
                mediaEntry.Name = mediaName;
                mediaEntry.MediaType = MediaType.VIDEO;
                MediaService.Add(mediaEntry)
                    .SetCompletion(new OnCompletedHandler<MediaEntry>(OnMediaAddComplete))
                    .Execute(client);
            }

            public void OnMediaAddComplete(MediaEntry mediaEntry, Exception error)
            {
                if (error != null)
                {
                    Console.WriteLine("ERROR: Failed chunk upload: " + error.Message);
                    code = -1;
                    done(id);
                    return;
                }

                entry = mediaEntry;
                if (token != null)
                {
                    AddContent();
                }
            }

            public void OnUploadTokenAddComplete(UploadToken uploadToken, Exception error)
            {
                if (error != null)
                {
                    Console.WriteLine("ERROR: Failed chunk upload: " + error.Message);
                    code = -1;
                    done(id);
                    return;
                }

                token = uploadToken;

                if (entry != null)
                {
                    AddContent();
                }

                fileStream = new FileStream(fname, FileMode.Open, FileAccess.Read, FileShare.Read);
                fileSize = fileStream.Length - (fileStream.Length % ChunkUpload.CHUNK_SIZE);

                // first chunk upload
                lastOffset = ChunkUpload.CHUNK_SIZE;
                new ChunkUpload(new OnCompletedHandler<UploadToken>(OnFirstChunkComplete), client, token.Id, fileStream, 0, false);
            }

            public void OnFirstChunkComplete(UploadToken uploadToken, Exception error)
            {
                if (error != null)
                {
                    Console.WriteLine("ERROR: Failed chunk upload: " + error.Message);
                    code = -1;
                    done(id);
                    return;
                }

                // start few threads with chunk upload
                for (int i = 0; i < maxUploadThreads; i++)
                {
                    int offset = lastOffset;
                    if ((offset + ChunkUpload.CHUNK_SIZE) < fileSize)
                    {
                        lastOffset += ChunkUpload.CHUNK_SIZE;
                        ChunkUpload upload = new ChunkUpload(new OnCompletedHandler<UploadToken>(OnChunkComplete), client, token.Id, fileStream, offset);
                        uploads.Add(upload);
                    }
                }
            }

            public void OnChunkComplete(UploadToken uploadToken, Exception error)
            {
                if (error != null)
                {
                    Console.WriteLine("ERROR: Failed chunk upload: " + error.Message);
                    code = -1;
                    done(id);
                    return;
                }

                if (uploadToken.Status == UploadTokenStatus.PARTIAL_UPLOAD)
                {
                    if ((lastOffset + ChunkUpload.CHUNK_SIZE) < fileSize)
                    {
                        // for each ended upload start a new one
                        int offset = lastOffset;
                        lastOffset += ChunkUpload.CHUNK_SIZE;
                        ChunkUpload upload = new ChunkUpload(new OnCompletedHandler<UploadToken>(OnChunkComplete), client, token.Id, fileStream, offset);
                        uploads.Add(upload);
                    }
                    else
                    {
                        // verify all upload threads are done
                        foreach (ChunkUpload upload in uploads)
                        {
                            if (!upload.IsDone())
                                return;
                        }

                        // upload final chunk
                        new ChunkUpload(new OnCompletedHandler<UploadToken>(OnLastChunkComplete), client, token.Id, fileStream, lastOffset, true, true);
                    }
                }
            }

            public void OnLastChunkComplete(UploadToken uploadToken, Exception error)
            {
                if (error != null)
                {
                    Console.WriteLine("ERROR: Failed chunk upload: " + error.Message);
                    code = -1;
                    done(id);
                    return;
                }

                if (uploadToken.Status == UploadTokenStatus.CLOSED)
                {
                    done(id);
                }
            }

            private void AddContent()
            {
                UploadedFileTokenResource mediaResource = new UploadedFileTokenResource();
                mediaResource.Token = token.Id;

                MediaService.AddContent(entry.Id, mediaResource)
                    .SetCompletion(new OnErrorHandler(OnContentAddError))
                    .Execute(client);
            }

            public void OnContentAddError(Exception error)
            {
                Console.WriteLine("ERROR: Failed chunk upload: " + error.Message);
                code = -1;
                done(id);
                return;
            }
        }

        class ResponseProfileTest : BaseTest
        {
            private int entriesCount = 4;
            private int metadataProfileCount = 2;
            private int metadataPageSize = 2;

            private List<MediaEntry> entries = new List<MediaEntry>();
            private List<MetadataProfile> metadataProfiles = new List<MetadataProfile>();

            private DetachedResponseProfile metadataResponseProfile;

            public override void test()
            {

                createEntriesWithMetadataObjects();
            }

            public void OnEntryListComplete(ListResponse<BaseEntry> baseEntryListResponse, Exception error)
            {
                if (error != null)
                {
                    Console.WriteLine("ERROR: Failed create listing entries: " + error.Message);
                    code = -1;
                    done(id);
                    return;
                }

                if (entriesCount != baseEntryListResponse.Objects.Count)
                {
                    Console.WriteLine("ERROR: entriesTotalCount[" + entriesCount + "] != list.Count[" + baseEntryListResponse.Objects.Count + "]");
                    code = -1;
                    done(id);
                    return;
                }

                foreach (BaseEntry entry in baseEntryListResponse.Objects)
                {
                    if (entry.RelatedObjects == null)
                    {
                        Console.WriteLine("ERROR: Related objects are missing");
                        code = -1;
                        done(id);
                        return;
                    }

                    if (!entry.RelatedObjects.ContainsKey(metadataResponseProfile.Name))
                    {
                        Console.WriteLine("ERROR: Related object [" + metadataResponseProfile.Name + "] is missing");
                        code = -1;
                        done(id);
                        return;
                    }

                    if (!(entry.RelatedObjects[metadataResponseProfile.Name] is ListResponse<Metadata>))
                    {
                        Console.WriteLine("ERROR: Related object [" + metadataResponseProfile.Name + "] has wrong type [" + entry.RelatedObjects[metadataResponseProfile.Name].GetType() + "]");
                        code = -1;
                        done(id);
                        return;
                    }
                    ListResponse<Metadata> metadataListResponse = (ListResponse<Metadata>)entry.RelatedObjects[metadataResponseProfile.Name];

                    if (metadataListResponse.Objects.Count != metadataProfileCount)
                    {
                        Console.WriteLine("ERROR: Related object [" + metadataResponseProfile.Name + "] has wrong number of objects");
                        code = -1;
                        done(id);
                        return;
                    }

                    foreach (Metadata metadata in metadataListResponse.Objects)
                    {
                        if (metadata.ObjectId != entry.Id)
                        {
                            Console.WriteLine("ERROR: Related object [" + metadataResponseProfile.Name + "] metadata [" + metadata.Id + "] related to wrong object [" + metadata.ObjectId + "]");
                            code = -1;
                            done(id);
                            return;
                        }
                    }
                }

                done(id);
            }

            private void createResponseProfile()
            {
                MetadataFilter metadataFilter = new MetadataFilter();
                metadataFilter.MetadataObjectTypeEqual = MetadataObjectType.ENTRY;

                ResponseProfileMapping metadataMapping = new ResponseProfileMapping();
                metadataMapping.FilterProperty = "objectIdEqual";
                metadataMapping.ParentProperty = "id";

                IList<ResponseProfileMapping> metadataMappings = new List<ResponseProfileMapping>();
                metadataMappings.Add(metadataMapping);

                FilterPager metadataPager = new FilterPager();
                metadataPager.PageSize = metadataPageSize;

                metadataResponseProfile = new DetachedResponseProfile();
                metadataResponseProfile.Name = "metadata_" + uniqueTag;
                metadataResponseProfile.Type = ResponseProfileType.INCLUDE_FIELDS;
                metadataResponseProfile.Fields = "id,objectId,createdAt, xml";
                metadataResponseProfile.Filter = metadataFilter;
                metadataResponseProfile.Pager = metadataPager;
                metadataResponseProfile.Mappings = metadataMappings;

                IList<DetachedResponseProfile> metadataResponseProfiles = new List<DetachedResponseProfile>();
                metadataResponseProfiles.Add(metadataResponseProfile);

                ResponseProfile responseProfile = new ResponseProfile();
                responseProfile.Name = "test_" + uniqueTag;
                responseProfile.SystemName = "test_" + uniqueTag;
                responseProfile.Type = ResponseProfileType.INCLUDE_FIELDS;
                responseProfile.Fields = "id,name,createdAt";
                responseProfile.RelatedProfiles = metadataResponseProfiles;

                ResponseProfileService.Add(responseProfile)
                    .SetCompletion(new OnCompletedHandler<ResponseProfile>(OnResponseProfileAddComplete))
                    .Execute(client);
            }

            public void OnResponseProfileAddComplete(ResponseProfile responseProfile, Exception error)
            {
                if (error != null)
                {
                    Console.WriteLine("ERROR: Failed create response profile: " + error.Message);
                    code = -1;
                    done(id);
                    return;
                }

                ResponseProfileHolder nestedResponseProfile = new ResponseProfileHolder();
                nestedResponseProfile.Id = responseProfile.Id;

                MediaEntryFilter entriesFilter = new MediaEntryFilter();
                entriesFilter.StatusIn = EntryStatus.PENDING.ToString() + "," + EntryStatus.NO_CONTENT.ToString();
                entriesFilter.TagsLike = uniqueTag;

                FilterPager entriesPager = new FilterPager();
                entriesPager.PageSize = entriesCount;

                BaseEntryListRequestBuilder baseEntryListRequestBuilder = BaseEntryService.List(entriesFilter, entriesPager);
                baseEntryListRequestBuilder.ResponseProfile = nestedResponseProfile;
                baseEntryListRequestBuilder
                    .SetCompletion(new OnCompletedHandler<ListResponse<BaseEntry>>(OnEntryListComplete))
                    .Execute(client);
            }

            private void createEntriesWithMetadataObjects()
            {
                MultiRequestBuilder multiRequestBuilder = new MultiRequestBuilder();
                string xsd;
                for (int i = 1; i <= metadataProfileCount; i++)
                {
                    xsd = @"<xsd:schema xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                    <xsd:element name=""metadata"">
                        <xsd:complexType>
                            <xsd:sequence>
                                <xsd:element name=""Choice" + i + @""" minOccurs=""0"" maxOccurs=""1"">
                                    <xsd:annotation>
                                        <xsd:documentation></xsd:documentation>
                                        <xsd:appinfo>
                                            <label>Example choice " + i + @"</label>
                                            <key>choice" + i + @"</key>
                                            <searchable>true</searchable>
                                            <description>Example choice " + i + @"</description>
                                        </xsd:appinfo>
                                    </xsd:annotation>
                                    <xsd:simpleType>
                                        <xsd:restriction base=""listType"">
                                            <xsd:enumeration value=""on"" />
                                            <xsd:enumeration value=""off"" />
                                        </xsd:restriction>
                                    </xsd:simpleType>
                                </xsd:element>
                                <xsd:element name=""FreeText" + i + @""" minOccurs=""0"" maxOccurs=""1"" type=""textType"">
                                    <xsd:annotation>
                                        <xsd:documentation></xsd:documentation>
                                        <xsd:appinfo>
                                            <label>Free text " + i + @"</label>
                                            <key>freeText" + i + @"</key>
                                            <searchable>true</searchable>
                                            <description>Free text " + i + @"</description>
                                        </xsd:appinfo>
                                    </xsd:annotation>
                                </xsd:element>
                            </xsd:sequence>
                        </xsd:complexType>
                    </xsd:element>
                    <xsd:complexType name=""textType"">
                        <xsd:simpleContent>
                            <xsd:extension base=""xsd:string"" />
                        </xsd:simpleContent>
                    </xsd:complexType>
                    <xsd:complexType name=""objectType"">
                        <xsd:simpleContent>
                            <xsd:extension base=""xsd:string"" />
                        </xsd:simpleContent>
                    </xsd:complexType>
                    <xsd:simpleType name=""listType"">
                        <xsd:restriction base=""xsd:string"" />
                    </xsd:simpleType>
                </xsd:schema>";

                    MetadataProfileAddRequestBuilder metadataProfileAddRequestBuilder = createMetadataProfile(MetadataObjectType.ENTRY, xsd);
                    metadataProfileAddRequestBuilder.SetCompletion(new OnCompletedHandler<MetadataProfile>(OnMetadataProfileAddComplete));
                    multiRequestBuilder.Add(metadataProfileAddRequestBuilder);
                }

                string userKS = client.GenerateSession(PARTNER_ID, ADMIN_SECRET, USER_ID);
                string xml;
                for (int i = 0; i < entriesCount; i++)
                {
                    MediaAddRequestBuilder mediaAddRequestBuilder = createEntry();
                    mediaAddRequestBuilder.SetCompletion(new OnCompletedHandler<MediaEntry>(OnMediaAddComplete));
                    mediaAddRequestBuilder.Ks = userKS;
                    multiRequestBuilder.Add(mediaAddRequestBuilder);

                    for (int index = 1; index <= metadataProfileCount; index++)
                    {
                        xml = @"<metadata>
                    <Choice" + index + ">on</Choice" + index + @">
                    <FreeText" + index + ">example text " + index + "</FreeText" + index + @">
                </metadata>";

                        MetadataAddRequestBuilder metadataAddRequestBuilder = new MetadataAddRequestBuilder();
                        metadataAddRequestBuilder.Ks = userKS;
                        metadataAddRequestBuilder.ObjectType = MetadataObjectType.ENTRY;
                        metadataAddRequestBuilder.XmlData = xml;
                        metadataAddRequestBuilder.Map("objectId", mediaAddRequestBuilder.Forward("id"));
                        metadataAddRequestBuilder.Map("metadataProfileId", multiRequestBuilder[index].Forward("id"));
                        multiRequestBuilder.Add(metadataAddRequestBuilder);
                    }
                }

                multiRequestBuilder.SetCompletion(new OnCompletedHandler<List<object>>(OnMultiRequestComplete));
                multiRequestBuilder.Execute(client);
            }


            public void OnMetadataProfileAddComplete(MetadataProfile metadataProfile, Exception error)
            {
                if (error != null)
                {
                    Console.WriteLine("ERROR: Failed create metadata profile: " + error.Message);
                    code = -1;
                    done(id);
                    return;
                }

                metadataProfiles.Add(metadataProfile);
            }

            public void OnMediaAddComplete(MediaEntry entry, Exception error)
            {
                if (error != null)
                {
                    Console.WriteLine("ERROR: Failed create entry: " + error.Message);
                    code = -1;
                    done(id);
                    return;
                }

                entries.Add(entry);
            }

            public void OnMultiRequestComplete(List<object> metadataProfile, Exception error)
            {
                if (error != null)
                {
                    Console.WriteLine("ERROR: Failed create entries: " + error.Message);
                    code = -1;
                    done(id);
                    return;
                }

                createResponseProfile();
            }

            private MediaAddRequestBuilder createEntry()
            {
                MediaEntry entry = new MediaEntry();
                entry.MediaType = MediaType.VIDEO;
                entry.Name = "test_" + Guid.NewGuid().ToString();
                entry.Tags = uniqueTag;

                return MediaService.Add(entry);
            }

            private MetadataProfileAddRequestBuilder createMetadataProfile(MetadataObjectType objectType, string xsdData)
            {
                MetadataProfile metadataProfile = new MetadataProfile();
                metadataProfile.MetadataObjectType = objectType;
                metadataProfile.Name = "test_" + Guid.NewGuid().ToString();

                return MetadataProfileService.Add(metadataProfile, xsdData);
            }
        }

        class ReplaceVideoFlavorAndAddCaptionTest : BaseTest
        {
            private UploadToken entryToken;
            private UploadToken captionToken;
            private MediaEntry entry;
            private int flavorParamsId;
            private string flavorAssetId;
            private bool flavorReplaceDone = false;
            private bool captionReplaceDone = false;

            public override void test()
            {
                UploadTokenService.Add()
                    .SetCompletion(new OnCompletedHandler<UploadToken>(OnEntryUploadTokenAddComplete))
                    .Execute(client);
            }

            public void OnEntryUploadTokenAddComplete(UploadToken uploadToken, Exception error)
            {
                if (error != null)
                {
                    Console.WriteLine("ERROR: Failed to create upload-token: " + error.Message);
                    code = -1;
                    done(id);
                    return;
                }

                entryToken = uploadToken;
                FileStream fileStream = new FileStream("DemoVideo.flv", FileMode.Open, FileAccess.Read);
                UploadTokenService.Upload(uploadToken.Id, fileStream)
                    .SetCompletion(new OnCompletedHandler<UploadToken>(OnEntryUploadComplete))
                    .Execute(client);
            }

            public void OnEntryUploadComplete(UploadToken uploadToken, Exception error)
            {
                if (error != null)
                {
                    Console.WriteLine("ERROR: Failed upload: " + error.Message);
                    code = -1;
                    done(id);
                    return;
                }

                MediaEntry mediaEntry = new MediaEntry();
                mediaEntry.Name = "Media Entry Using C#.Net Client To Test Flavor Replace";
                mediaEntry.MediaType = MediaType.VIDEO;
                MediaService.Add(mediaEntry)
                    .SetCompletion(new OnCompletedHandler<MediaEntry>(OnEntryAddComplete))
                    .Execute(client);
            }

            public void OnEntryAddComplete(MediaEntry mediaEntry, Exception error)
            {
                if (error != null)
                {
                    Console.WriteLine("ERROR: Failed to create entry: " + error.Message);
                    code = -1;
                    done(id);
                    return;
                }

                entry = mediaEntry;

                UploadedFileTokenResource mediaResource = new UploadedFileTokenResource();
                mediaResource.Token = entryToken.Id;
                MediaService.AddContent(mediaEntry.Id, mediaResource)
                    .SetCompletion(new OnCompletedHandler<MediaEntry>(OnEntryAddContentComplete))
                    .Execute(client);
            }

            public void OnEntryAddContentComplete(MediaEntry mediaEntry, Exception error)
            {
                if (error != null)
                {
                    Console.WriteLine("ERROR: Failed to add entry content: " + error.Message);
                    code = -1;
                    done(id);
                    return;
                }

                //verify that the account we're testing has the new iPad flavor enabled on the default conversion profile

                ConversionProfileGetDefaultRequestBuilder conversionProfileRequestBuilder = ConversionProfileService.GetDefault();

                ConversionProfileAssetParamsFilter flavorsListFilter = new ConversionProfileAssetParamsFilter();
                flavorsListFilter.SystemNameEqual = "iPad";
                flavorsListFilter.Map("conversionProfileIdEqual", conversionProfileRequestBuilder.Forward("id"));

                ConversionProfileAssetParamsListRequestBuilder conversionProfileAssetsRequestBuilder = ConversionProfileAssetParamsService.List(flavorsListFilter);
                conversionProfileAssetsRequestBuilder.SetCompletion(new OnCompletedHandler<ListResponse<ConversionProfileAssetParams>>(OnAssetParamsListComplete));

                conversionProfileRequestBuilder.Add(conversionProfileAssetsRequestBuilder)
                    .Execute(client);
            }

            public void OnAssetParamsListComplete(ListResponse<ConversionProfileAssetParams> conversionProfileAssets, Exception error)
            {
                if (error != null)
                {
                    Console.WriteLine("ERROR: Failed to list asset-params: " + error.Message);
                    code = -1;
                    done(id);
                    return;
                }

                if (conversionProfileAssets.Objects.Count == 0)
                {
                    Console.WriteLine("!! Default conversion profile does NOT include the new iPad flavor");
                    Console.WriteLine("!! Skipping the iPad flavor replace test, make sure account has newiPad flavor enabled.");
                    flavorReplaceDone = true;
                }
                else
                {
                    flavorParamsId = conversionProfileAssets.Objects[0].AssetParamsId;
                    getFlavorAsset();

                    Console.WriteLine("** Default conversion profile includes the new iPad flavor, id is: " + flavorParamsId);
                }

                //now let's upload a new caption file to this entry
                UploadTokenService.Add()
                    .SetCompletion(new OnCompletedHandler<UploadToken>(OnCaptionUploadTokenAddComplete))
                    .Execute(client);
            }

            public void OnCaptionUploadTokenAddComplete(UploadToken uploadToken, Exception error)
            {
                if (error != null)
                {
                    Console.WriteLine("ERROR: Failed to create caption upload-token: " + error.Message);
                    code = -1;
                    done(id);
                    return;
                }

                captionToken = uploadToken;
                FileStream fileStreamCaption = new FileStream("DemoCaptions.srt", FileMode.Open, FileAccess.Read);
                UploadTokenService.Upload(uploadToken.Id, fileStreamCaption)
                    .SetCompletion(new OnErrorHandler(OnError))
                    .Execute(client);

                CaptionAsset captionAsset = new CaptionAsset();
                captionAsset.Label = "Test C# Uploaded Caption";
                captionAsset.Language = Language.EN;
                captionAsset.Format = CaptionType.SRT;
                captionAsset.FileExt = "srt";
                CaptionAssetService.Add(entry.Id, captionAsset)
                    .SetCompletion(new OnCompletedHandler<CaptionAsset>(OnCaptionAddComplete))
                    .Execute(client);
            }

            public void OnCaptionAddComplete(CaptionAsset captionAsset, Exception error)
            {
                if (error != null)
                {
                    Console.WriteLine("ERROR: Failed to create caption upload-token: " + error.Message);
                    code = -1;
                    done(id);
                    return;
                }

                Console.WriteLine("6. Added a new caption asset. Id: " + captionAsset.Id);
                UploadedFileTokenResource captionResource = new UploadedFileTokenResource();
                captionResource.Token = captionToken.Id;
                CaptionAssetService.SetContent(captionAsset.Id, captionResource)
                    .SetCompletion(new OnCompletedHandler<CaptionAsset>(OnCaptionSetContentComplete))
                    .Execute(client);
            }

            public void OnCaptionSetContentComplete(CaptionAsset captionAsset, Exception error)
            {
                if (error != null)
                {
                    Console.WriteLine("ERROR: Failed to create caption upload-token: " + error.Message);
                    code = -1;
                    done(id);
                    return;
                }

                Console.WriteLine("7. Uploaded a new caption file and attached to caption asset id: " + captionAsset.Id);
                CaptionAssetService.GetUrl(captionAsset.Id)
                    .SetCompletion(new OnCompletedHandler<string>(OnCaptionGetUrlComplete))
                    .Execute(client);
            }

            public void OnCaptionGetUrlComplete(string captionUrl, Exception error)
            {
                if (error != null)
                {
                    Console.WriteLine("ERROR: Failed to create caption upload-token: " + error.Message);
                    code = -1;
                    done(id);
                    return;
                }

                Console.WriteLine("8. Newly created Caption Asset URL is: " + captionUrl);

                captionReplaceDone = true;
                if (flavorReplaceDone)
                {
                    done(id);
                }
            }

            private void getFlavorAsset()
            {
                Console.WriteLine("2. Waiting for the iPad flavor to be available...");
                System.Threading.Thread.Sleep(5000);

                FlavorAssetFilter flavorAssetsFilter = new FlavorAssetFilter();
                flavorAssetsFilter.EntryIdEqual = entry.Id;
                FlavorAssetService.List(flavorAssetsFilter)
                    .SetCompletion(new OnCompletedHandler<ListResponse<FlavorAsset>>(OnAssetListComplete))
                    .Execute(client);
            }

            public void OnAssetListComplete(ListResponse<FlavorAsset> flavorAssets, Exception error)
            {
                if (error != null)
                {
                    Console.WriteLine("ERROR: Failed to list flavor-assets: " + error.Message);
                    code = -1;
                    done(id);
                    return;
                }

                foreach (FlavorAsset flavor in flavorAssets.Objects)
                {
                    if (flavor.FlavorParamsId == flavorParamsId)
                    {
                        if (flavor.Status == FlavorAssetStatus.NOT_APPLICABLE)
                        {
                            //in case the  Transcoding Decision Layer decided not to convert to this flavor, let's force it.
                            FlavorAssetService.Convert(entry.Id, flavorParamsId);
                        }
                        else if (flavor.Status == FlavorAssetStatus.READY)
                        {
                            Console.WriteLine("3. iPad flavor (" + flavorParamsId + "). It's Ready to ROCK!");
                            flavorAssetId = flavor.Id;
                            FlavorAssetService.GetDownloadUrl(flavorAssetId)
                                .SetCompletion(new OnCompletedHandler<string>(OnGetDownloadUrlComplete))
                                .Execute(client);

                            return;
                        }

                        Console.WriteLine("3. iPad flavor (" + flavorParamsId + "). It's being converted. Waiting...");
                        getFlavorAsset();
                    }
                }
            }

            public void OnGetDownloadUrlComplete(string iPadFlavorUrl, Exception error)
            {
                if (error != null)
                {
                    Console.WriteLine("ERROR: Failed to get download URL: " + error.Message);
                    code = -1;
                    done(id);
                    return;
                }

                Console.WriteLine("4. iPad Flavor URL is: " + iPadFlavorUrl);

                //Alternatively, download URL for a given flavor id can also be retrived by creating the playManifest URL -
                string playManifestURL = "http://www..com/p/{partnerId}/sp/0/playManifest/entryId/{entryId}/format/url/flavorParamId/{flavorParamId}/ks/{ks}/{fileName}.mp4";
                playManifestURL = playManifestURL.Replace("{partnerId}", PARTNER_ID.ToString());
                playManifestURL = playManifestURL.Replace("{entryId}", entry.Id);
                playManifestURL = playManifestURL.Replace("{flavorParamId}", flavorParamsId.ToString());
                playManifestURL = playManifestURL.Replace("{ks}", client.KS);
                playManifestURL = playManifestURL.Replace("{fileName}", entry.Name);
                Console.WriteLine("4. iPad Flavor playManifest URL is: " + playManifestURL);

                //now let's replace the flavor with our video file (e.g. after processing the file outside of )
                UploadTokenService.Add()
                    .SetCompletion(new OnCompletedHandler<UploadToken>(OnFlavorUploadTokenAddComplete))
                    .Execute(client);
            }

            public void OnFlavorUploadTokenAddComplete(UploadToken uploadToken, Exception error)
            {
                if (error != null)
                {
                    Console.WriteLine("ERROR: Failed add upload-token: " + error.Message);
                    code = -1;
                    done(id);
                    return;
                }

                FileStream fileStreamiPad = new FileStream("DemoVideoiPad.mp4", FileMode.Open, FileAccess.Read);
                UploadTokenService.Upload(uploadToken.Id, fileStreamiPad)
                    .SetCompletion(new OnCompletedHandler<UploadToken>(OnFlavorUploadComplete))
                    .Execute(client);
            }

            public void OnFlavorUploadComplete(UploadToken uploadToken, Exception error)
            {
                if (error != null)
                {
                    Console.WriteLine("ERROR: Failed upload: " + error.Message);
                    code = -1;
                    done(id);
                    return;
                }

                UploadedFileTokenResource mediaResource = new UploadedFileTokenResource();
                mediaResource.Token = uploadToken.Id;
                FlavorAssetService.SetContent(flavorAssetId, mediaResource)
                    .SetCompletion(new OnCompletedHandler<FlavorAsset>(OnFlavorSetContentComplete))
                    .Execute(client);

            }

            public void OnFlavorSetContentComplete(FlavorAsset flavorAsset, Exception error)
            {
                if (error != null)
                {
                    Console.WriteLine("ERROR: Failed upload: " + error.Message);
                    code = -1;
                    done(id);
                    return;
                }

                Console.WriteLine("5. iPad Flavor was replaced! id: " + flavorAssetId);

                flavorReplaceDone = true;
                if (captionReplaceDone)
                {
                    done(id);
                }
            }
        }

        class MetadataTest : BaseTest
        {
            private MediaEntry entry;

            public override void test()
            {
                // Setup a pager and search to use
                MediaEntryFilter mediaEntryFilter = new MediaEntryFilter();
                mediaEntryFilter.OrderBy = MediaEntryOrderBy.CREATED_AT_ASC;
                mediaEntryFilter.MediaTypeEqual = MediaType.VIDEO;

                FilterPager pager = new FilterPager();
                pager.PageSize = 1;
                pager.PageIndex = 1;

                Console.WriteLine("List videos, get the first one...");
                MediaService.List(mediaEntryFilter, pager)
                    .SetCompletion(new OnCompletedHandler<ListResponse<MediaEntry>>(OnEntriesListComplete))
                    .Execute(client);
            }

            public void OnEntriesListComplete(ListResponse<MediaEntry> entryList, Exception error)
            {
                if (error != null)
                {
                    Console.WriteLine("ERROR: Failed to list entries: " + error.Message);
                    code = -1;
                    done(id);
                    return;
                }

                MetadataProfile newMetadataProfile = new MetadataProfile();
                newMetadataProfile.MetadataObjectType = MetadataObjectType.ENTRY;
                newMetadataProfile.Name = "Test";

                entry = entryList.Objects[0];

                // The Schema file for the field
                // Currently, you must build the xsd yourself. There is no utility provided.
                string xsdFile = "MetadataSchema.xsd";
                StreamReader fileStream = File.OpenText(xsdFile);
                string xsd = fileStream.ReadToEnd();

                MetadataProfileService.Add(newMetadataProfile, xsd)
                    .SetCompletion(new OnCompletedHandler<MetadataProfile>(OnMetadataProfileAddComplete))
                    .Execute(client);
            }

            public void OnMetadataProfileAddComplete(MetadataProfile metadataProfile, Exception error)
            {
                if (error != null)
                {
                    Console.WriteLine("ERROR: Failed to create metadata profile: " + error.Message);
                    code = -1;
                    done(id);
                    return;
                }

                Console.WriteLine("1. Successfully created the custom metadata profile " + metadataProfile.Name + ".");

                string fieldValue = "VobSub";
                string xmlData = "<metadata><SubtitleFormat>" + fieldValue + "</SubtitleFormat></metadata>";

                MetadataService.Add(metadataProfile.Id, metadataProfile.MetadataObjectType, entry.Id, xmlData)
                    .SetCompletion(new OnCompletedHandler<Metadata>(OnMetadataAddComplete))
                    .Execute(client);
            }

            public void OnMetadataAddComplete(Metadata metadata, Exception error)
            {
                if (error != null)
                {
                    Console.WriteLine("ERROR: Failed to create metadata: " + error.Message);
                    code = -1;
                    done(id);
                    return;
                }

                Console.WriteLine("2. Successfully added the custom data field for entryid: " + entry.Id);

                MetadataFilter metadataFilter = new MetadataFilter();
                metadataFilter.ObjectIdEqual = entry.Id;
                metadataFilter.MetadataProfileIdEqual = metadata.MetadataProfileId;
                MetadataService.List(metadataFilter)
                    .SetCompletion(new OnCompletedHandler<ListResponse<Metadata>>(OnMetadataListComplete))
                    .Execute(client);
            }

            public void OnMetadataListComplete(ListResponse<Metadata> metadataList, Exception error)
            {
                if (error != null)
                {
                    Console.WriteLine("ERROR: Failed to create metadata: " + error.Message);
                    code = -1;
                    done(id);
                    return;
                }

                if (metadataList.TotalCount == 0 || metadataList.Objects.Count == 0)
                {
                    Console.WriteLine("ERROR: Failed to find metadata for entryid: " + entry.Id);
                    code = -1;
                    done(id);
                    return;
                }

                done(id);
            }
        }

        class AdvancedMultiRequestTest : BaseTest
        {
            public override void test()
            {
                //MediaService.List(mediaEntryFilter, pager)
                //    .SetCompletion(new OnCompletedHandler<ListResponse<MediaEntry>>(OnEntriesListComplete))
                //    .Execute(client);

                FileStream fileStream = new FileStream("DemoVideo.flv", FileMode.Open, FileAccess.Read);

                MediaEntry mediaEntry = new MediaEntry();
                mediaEntry.Name = "Media Entry Using C#.Net Client To Test Flavor Replace";
                mediaEntry.MediaType = MediaType.VIDEO;

                UploadedFileTokenResource mediaResource = new UploadedFileTokenResource();
                mediaResource.Token = "{1:result:id}";

                UploadTokenService.Add()
                    .Add(MediaService.Add(mediaEntry))
                    .Add(UploadTokenService.Upload("{1:result:id}", fileStream))
                    .Add(MediaService.AddContent("{2:result:id}", mediaResource))
                    .SetCompletion(new OnCompletedHandler<List<object>>(OnMultiRequestComplete))
                    .Execute(client);

            }

            public void OnMultiRequestComplete(List<object> results, Exception error)
            {
                if (error != null)
                {
                    Console.WriteLine("ERROR: Failed multi-request: " + error.Message);
                    code = -1;
                    done(id);
                    return;
                }

                foreach (object result in results)
                {
                    if (result is APIException)
                    {
                        Console.WriteLine("ERROR: " + ((APIException)result).Message);
                        code = -1;
                        done(id);
                        return;
                    }
                }

                // when accessing the response object we will use an index and not the response number (response number - 1)
                if (results[3] is MediaEntry)
                {
                    MediaEntry newMediaEntry = (MediaEntry)results[3];
                    Console.WriteLine("Multirequest newly added entry id: " + newMediaEntry.Id + ", status: " + newMediaEntry.Status);
                    done(id);
                }
            }
        }

        class PlaylistExecuteMultiRequestTest : BaseTest
        {
            public override void test()
            {
                Client client = new Client(GetConfig());

                // Request 1
                SessionStartRequestBuilder sessionStartRequestBuilder = SessionService.Start(ADMIN_SECRET, "", SessionType.ADMIN, PARTNER_ID, 86400, "");
                
                // Request 2
                sessionStartRequestBuilder.Add(MediaService.List().Map(MediaListRequestBuilder.KS, sessionStartRequestBuilder.Forward()))
                    .SetCompletion(new OnCompletedHandler<List<object>>(OnMediaListMultiRequestComplete))
                    .Execute(client);
            }

            public void OnMediaListMultiRequestComplete(List<object> results, Exception error)
            {
                if (error != null)
                {
                    Console.WriteLine("ERROR: Failed multi-request: " + error.Message);
                    code = -1;
                    done(id);
                    return;
                }

                foreach (object result in results)
                {
                    if (result is APIException)
                    {
                        Console.WriteLine("ERROR: " + ((APIException)result).Message);
                        code = -1;
                        done(id);
                        return;
                    }
                }

                string twoEntries = "";

                if (results[1] is ListResponse<MediaEntry>)
                {
                    ListResponse<MediaEntry> mediaListResponse = (ListResponse<MediaEntry>)results[1];
                    twoEntries = mediaListResponse.Objects[0].Id + ", " + mediaListResponse.Objects[1].Id;
                    Console.WriteLine("We will use the first 2 entries we got as a reponse: " + twoEntries);
                }

                if (twoEntries.Equals(""))
                {
                    done(id);
                    return;
                }

                Client client = new Client(GetConfig());
                SessionStartRequestBuilder sessionStartRequestBuilder = SessionService.Start(ADMIN_SECRET, "", SessionType.ADMIN, PARTNER_ID, 86400, "");

                Playlist newPlaylist = new Playlist();
                newPlaylist.Name = "Test Playlist";
                newPlaylist.PlaylistContent = twoEntries;
                newPlaylist.PlaylistType = PlaylistType.STATIC_LIST;

                sessionStartRequestBuilder.Add(PlaylistService.Add(newPlaylist).Map(PlaylistAddRequestBuilder.KS, sessionStartRequestBuilder.Forward()))
                    .SetCompletion(new OnCompletedHandler<List<object>>(OnPlaylistAddMultiRequestComplete))
                    .Execute(client);
            }

            public void OnPlaylistAddMultiRequestComplete(List<object> results, Exception error)
            {
                if (error != null)
                {
                    Console.WriteLine("ERROR: Failed multi-request: " + error.Message);
                    code = -1;
                    done(id);
                    return;
                }

                foreach (object result in results)
                {
                    if (result is APIException)
                    {
                        Console.WriteLine("ERROR: " + ((APIException)result).Message);
                        code = -1;
                        done(id);
                        return;
                    }
                }

                if (!(results[1] is Playlist))
                {
                    Console.WriteLine("ERROR: Expected playlist, got " + results[1].GetType().Name);
                    code = -1;
                    done(id);
                    return;
                }

                Playlist playlist = results[1] as Playlist;

                PlaylistService.Execute(playlist.Id)
                    .Add(PlaylistService.Execute(playlist.Id))
                    .SetCompletion(new OnCompletedHandler<List<object>>(OnPlaylistExecuteMultiRequestComplete))
                    .Execute(client);
            }

            public void OnPlaylistExecuteMultiRequestComplete(List<object> results, Exception error)
            {
                if (error != null)
                {
                    Console.WriteLine("ERROR: Failed multi-request: " + error.Message);
                    code = -1;
                    done(id);
                    return;
                }

                foreach (object result in results)
                {
                    if (result is APIException)
                    {
                        Console.WriteLine("ERROR: " + ((APIException)result).Message);
                        code = -1;
                        done(id);
                        return;
                    }
                    if (!(result is IList<BaseEntry>))
                    {
                        Console.WriteLine("ERROR: Expected base-entry list, got " + result.GetType().Name);
                        code = -1;
                        done(id);
                        return;
                    }
                }

                done(id);
            }
        }
        
        static void Main(string[] args)
        {
            uniqueTag = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 20);

            Console.WriteLine("Starting C# Kaltura API Client Library");

            BaseTest tester;
            if(args.Length > 0 && args[0].Equals("--with-threads"))
            {
                tester = new SampleThreadedChunkUploadTest();
                tests.Add(tester.getId());
                tester.test();
            }

            tester = new ResponseProfileTest();
            tests.Add(tester.getId());
            tester.test();

            tester = new ReplaceVideoFlavorAndAddCaptionTest();
            tests.Add(tester.getId());
            tester.test();

            tester = new MetadataTest();
            tests.Add(tester.getId());
            tester.test();

            tester = new AdvancedMultiRequestTest();
            tests.Add(tester.getId());
            tester.test();

            tester = new PlaylistExecuteMultiRequestTest();
            tests.Add(tester.getId());
            tester.test();

            while (tests.Count > 0)
            {
                Thread.Sleep(100);
            }

            Console.WriteLine("Done. Exit code was "+code);

            Environment.Exit(code);
        }

        protected static void done(string id)
        {
            tests.Remove(id);
        }

        public static Configuration GetConfig()
        {
            Configuration config = new Configuration();
            config.ServiceUrl = SERVICE_URL;
            config.Logger = new ClientTester();
            return config;
        }
    }
}
