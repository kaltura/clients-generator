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
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Web;
using System.IO;
using System.Security.Cryptography;
using System.Xml;
using System.Xml.XPath;
using System.Runtime.Serialization;
using System.Threading;
using Kaltura.Types;
using Kaltura.Enums;

namespace Kaltura
{
    public class ClientBase
    {
        protected ClientConfiguration clientConfiguration = new ClientConfiguration();
        protected RequestConfiguration requestConfiguration = new RequestConfiguration();

        private const int BLOCK_SIZE = 16;        
        private const string FIELD_EXPIRY = "_e";
        private const string FIELD_USER = "_u";
	    private const string FIELD_TYPE = "_t";
	    private const int RANDOM_SIZE = 16; 

        public Configuration Configuration
        {
            get;
            set;
        }

        public ClientConfiguration ClientConfiguration
        {
            get { return clientConfiguration; }
        }

        public RequestConfiguration RequestConfiguration
        {
            get { return requestConfiguration; }
        }

        public ClientBase(Configuration config)
        {
            Configuration = config;
        }

        public string GenerateSession(int partnerId, string adminSecretForSigning, string userId = "", SessionType type = SessionType.USER, int expiry = 86400, string privileges = "")
        {
            return GenerateSessionV2(partnerId, adminSecretForSigning, userId, type, expiry, privileges);
        }

        public string GenerateSessionV1(int partnerId, string adminSecretForSigning, string userId = "", SessionType type = SessionType.USER, int expiry = 86400, string privileges = "")
        {
            string ks = string.Format("{0};{0};{1};{2};{3};{4};{5};", partnerId, UnixTimeNow() + expiry, type.GetHashCode(), DateTime.Now.Ticks, userId, privileges);

            SHA1 sha = new SHA1CryptoServiceProvider();

            byte[] ksTextBytes = Encoding.ASCII.GetBytes(adminSecretForSigning + ks);

            byte[] sha1Bytes = sha.ComputeHash(ksTextBytes);

            string sha1Hex = "";
            foreach (char c in sha1Bytes)
                sha1Hex += string.Format("{0:x2}", (int)c);

            ks = sha1Hex.ToLower() + "|" + ks;

            return EncodeTo64(ks);
        }

        public string GenerateSessionV2(int partnerId, string adminSecretForSigning, string userId = "", SessionType type = SessionType.USER, int expiry = 86400, string privileges = "")
        {
		    // build fields array
            Dictionary<string, string> fields = new Dictionary<string,string>();
		    string[] privilegesArr = privileges.Split(',');
		    foreach (string curPriv in privilegesArr) 
            {
			    string privilege = curPriv.Trim();
			    if(privilege.Length == 0)
                {
				    continue;
                }
			    if(privilege.Equals("*"))
                {
				    privilege = "all:*";
                }
			
			    string[] splittedPriv = privilege.Split(':');
			    if(splittedPriv.Length > 1) 
                {
				    fields.Add(splittedPriv[0], HttpUtility.UrlEncode(splittedPriv[1], Encoding.UTF8));
			    } 
                else 
                {
				    fields.Add(splittedPriv[0], "");
			    }
		    }
		
		    long expiryTime = (UnixTimeNow() + expiry);
		    fields.Add(FIELD_EXPIRY,  expiryTime.ToString());
		    fields.Add(FIELD_TYPE, ((int) type).ToString());
		    fields.Add(FIELD_USER, userId);
		
		    // build fields string
		    byte[] randomBytes = createRandomByteArray(RANDOM_SIZE);
            string fieldsString = string.Join("&", fields.Select(kvp => string.Format("{0}={1}", kvp.Key, kvp.Value)));
            byte[] fieldsByteArray = Encoding.ASCII.GetBytes(fieldsString);
		    int totalLength = randomBytes.Length + fieldsByteArray.Length;

		    byte[] fieldsAndRandomBytes = new byte[totalLength];
		    Array.Copy(randomBytes, 0, fieldsAndRandomBytes, 0, randomBytes.Length);
            Array.Copy(fieldsByteArray, 0, fieldsAndRandomBytes, randomBytes.Length, fieldsByteArray.Length);

		    byte[] infoSignature = signInfoWithSHA1(fieldsAndRandomBytes);
		    byte[] input = new byte[infoSignature.Length + fieldsAndRandomBytes.Length];
            Array.Copy(infoSignature, 0, input, 0, infoSignature.Length);
            Array.Copy(fieldsAndRandomBytes, 0, input, infoSignature.Length, fieldsAndRandomBytes.Length);
		
		    // encrypt and encode
		    byte[] encryptedFields = aesEncrypt(adminSecretForSigning, input);
		    string prefix = "v2|" + partnerId + "|";
            byte[] prefixBytes = Encoding.ASCII.GetBytes(prefix);
		
		    byte[] output = new byte[encryptedFields.Length + prefix.Length];
            Array.Copy(prefixBytes, 0, output, 0, prefix.Length);
		    Array.Copy(encryptedFields,0,output,prefix.Length, encryptedFields.Length);

            string encodedKs = EncodeTo64(output);
		    encodedKs = encodedKs.Replace("\\+", "-");
            encodedKs = encodedKs.Replace("/", "_");
		    encodedKs = encodedKs.Replace("\n", "");
		    encodedKs = encodedKs.Replace("\r", "");
		
		    return encodedKs;
        }
	
	    private byte[] aesEncrypt(string secretForSigning, byte[] text)
        {
            byte[] hashedKey = signInfoWithSHA1(secretForSigning);
            byte[] keyBytes = new byte[BLOCK_SIZE];
            Array.Copy(hashedKey, 0, keyBytes, 0, BLOCK_SIZE);

            //IV
            byte[] ivBytes = new byte[BLOCK_SIZE];

            // Text
            int textSize = ((text.Length + BLOCK_SIZE - 1) / BLOCK_SIZE) * BLOCK_SIZE;
            byte[] textAsBytes = new byte[textSize];
            Array.Copy(text, 0, textAsBytes, 0, text.Length);

            // Encrypt
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = keyBytes;
                aesAlg.IV = ivBytes;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.None;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cst = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        cst.Write(textAsBytes, 0, textSize);
                        return ms.ToArray();
                    }
                }
            }
	    }

	    private byte[] signInfoWithSHA1(string text) 
        {
            return signInfoWithSHA1(Encoding.ASCII.GetBytes(text));
	    }
	
	    private byte[] signInfoWithSHA1(byte[] data) 
        {
            SHA1 sha = new SHA1CryptoServiceProvider();
            return sha.ComputeHash(data);
	    }

        private byte[] createRandomByteArray(int size)
        {
            byte[] b = new byte[size];
            new Random().NextBytes(b);
            return b;
        }

        public long UnixTimeNow()
        {
            TimeSpan _TimeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
            return (long)_TimeSpan.TotalSeconds;
        }

        private string EncodeTo64(string toEncode)
        {
            return EncodeTo64(Encoding.ASCII.GetBytes(toEncode));
        }

        private string EncodeTo64(byte[] toEncode)
        {
            string returnValue = System.Convert.ToBase64String(toEncode);
            return returnValue;
        }
    }
}
