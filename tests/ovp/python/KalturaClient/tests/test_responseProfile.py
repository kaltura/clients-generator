from __future__ import absolute_import

import uuid
import unittest

from .utils import KalturaBaseTest

from KalturaClient.Plugins.Core import (
    KalturaBaseEntryListResponse,
    KalturaDetachedResponseProfile,
    KalturaEntryStatus,
    KalturaFilterPager,
    KalturaMediaEntry,
    KalturaMediaEntryFilter,
    KalturaMediaType,
    KalturaResponseProfile,
    KalturaResponseProfileHolder,
    KalturaResponseProfileMapping,
    KalturaResponseProfileType,
)
from KalturaClient.Plugins.Metadata import (
    KalturaMetadata,
    KalturaMetadataFilter,
    KalturaMetadataListResponse,
    KalturaMetadataObjectType,
    KalturaMetadataProfile,
)


class ResponseProfileTests(KalturaBaseTest):

    def setUp(self):
        KalturaBaseTest.setUp(self)
        self.uniqueTag = self.uniqid('tag_')

    def uniqid(self, prefix):
        return prefix + uuid.uuid1().hex

    def createEntry(self):
        entry = KalturaMediaEntry()
        entry.mediaType = KalturaMediaType.VIDEO
        entry.name = self.uniqid('test_')
        entry.description = self.uniqid('test ')
        entry.tags = self.uniqueTag

        entry = self.client.media.add(entry)

        return entry

    def createMetadata(self, metadataProfileId, objectType, objectId, xmlData):
        metadata = KalturaMetadata()
        metadata.metadataObjectType = objectType
        metadata.objectId = objectId

        metadata = self.client.metadata.metadata.add(
            metadataProfileId, objectType, objectId, xmlData)

        return metadata

    def createMetadataProfile(self, objectType, xsdData):
        metadataProfile = KalturaMetadataProfile()
        metadataProfile.metadataObjectType = objectType
        metadataProfile.name = self.uniqid('test_')
        metadataProfile.systemName = self.uniqid('test_')

        metadataProfile = self.client.metadata.metadataProfile.add(
            metadataProfile, xsdData)

        return metadataProfile

    def createEntriesWithMetadataObjects(self, entriesCount,
                                         metadataProfileCount=2):
        entries = []
        metadataProfiles = {}
        xsd = """\
<xsd:schema xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">
    <xsd:element name=\"metadata\">
        <xsd:complexType>
            <xsd:sequence>
                <xsd:element name=\"Choice{index}\" minOccurs=\"0\"
                    maxOccurs=\"1\">
                    <xsd:annotation>
                        <xsd:documentation></xsd:documentation>
                        <xsd:appinfo>
                            <label>Example choice {index}</label>
                            <key>choice{index}</key>
                            <searchable>true</searchable>
                            <description>Example choice {index}</description>
                        </xsd:appinfo>
                    </xsd:annotation>
                    <xsd:simpleType>
                        <xsd:restriction base=\"listType\">
                            <xsd:enumeration value=\"on\" />
                            <xsd:enumeration value=\"off\" />
                        </xsd:restriction>
                    </xsd:simpleType>
                </xsd:element>
                <xsd:element name=\"FreeText{index}\" minOccurs=\"0\"
                    maxOccurs=\"1\" type=\"textType\">
                    <xsd:annotation>
                        <xsd:documentation></xsd:documentation>
                        <xsd:appinfo>
                            <label>Free text {index}</label>
                            <key>freeText{index}</key>
                            <searchable>true</searchable>
                            <description>Free text {index}</description>
                        </xsd:appinfo>
                    </xsd:annotation>
                </xsd:element>
            </xsd:sequence>
        </xsd:complexType>
    </xsd:element>
    <xsd:complexType name=\"textType\">
        <xsd:simpleContent>
            <xsd:extension base=\"xsd:string\" />
        </xsd:simpleContent>
    </xsd:complexType>
    <xsd:complexType name=\"objectType\">
        <xsd:simpleContent>
            <xsd:extension base=\"xsd:string\" />
        </xsd:simpleContent>
    </xsd:complexType>
    <xsd:simpleType name=\"listType\">
        <xsd:restriction base=\"xsd:string\" />
    </xsd:simpleType>
</xsd:schema>"""
        for i in range(1, metadataProfileCount + 1):
            metadataProfiles[i] = self.createMetadataProfile(
                KalturaMetadataObjectType.ENTRY, xsd.format(index=i))

        xml = """\
<metadata>
    <Choice{index}>on</Choice{index}>
    <FreeText{index}>example text {index}</FreeText{index}>
</metadata>
"""
        for i in range(0, entriesCount):
            entry = self.createEntry()
            entries.append(entry)

            for j in range(1, metadataProfileCount + 1):
                self.createMetadata(
                    metadataProfiles[j].id,
                    KalturaMetadataObjectType.ENTRY, entry.id,
                    xml.format(index=j))

        return [entries, metadataProfiles]

    def test_list(self):
        entriesTotalCount = 4
        entriesPageSize = 3
        metadataPageSize = 2

        entries, metadataProfiles = self.createEntriesWithMetadataObjects(
            entriesTotalCount, metadataPageSize)

        entriesFilter = KalturaMediaEntryFilter()
        entriesFilter.tagsLike = self.uniqueTag
        entriesFilter.statusIn = "{},{}".format(
            KalturaEntryStatus.PENDING, KalturaEntryStatus.NO_CONTENT)

        entriesPager = KalturaFilterPager()
        entriesPager.pageSize = entriesPageSize

        metadataFilter = KalturaMetadataFilter()
        metadataFilter.metadataObjectTypeEqual = (
            KalturaMetadataObjectType.ENTRY)

        metadataMapping = KalturaResponseProfileMapping()
        metadataMapping.filterProperty = 'objectIdEqual'
        metadataMapping.parentProperty = 'id'

        metadataPager = KalturaFilterPager()
        metadataPager.pageSize = metadataPageSize

        metadataResponseProfile = KalturaDetachedResponseProfile()
        metadataResponseProfile.name = self.uniqid('test_')
        metadataResponseProfile.type = (
            KalturaResponseProfileType.INCLUDE_FIELDS)
        metadataResponseProfile.fields = 'id,objectId,createdAt, xml'
        metadataResponseProfile.filter = metadataFilter
        metadataResponseProfile.pager = metadataPager
        metadataResponseProfile.mappings = [metadataMapping]

        responseProfile = KalturaResponseProfile()
        responseProfile.name = self.uniqid('test_')
        responseProfile.systemName = self.uniqid('test_')
        responseProfile.type = KalturaResponseProfileType.INCLUDE_FIELDS
        responseProfile.fields = 'id,name,createdAt'
        responseProfile.relatedProfiles = [metadataResponseProfile]

        responseProfile = self.client.responseProfile.add(responseProfile)

        nestedResponseProfile = KalturaResponseProfileHolder()
        nestedResponseProfile.id = responseProfile.id

        self.client.setResponseProfile(nestedResponseProfile)
        list_ = self.client.baseEntry.list(entriesFilter, entriesPager)

        self.assertIsInstance(list_, KalturaBaseEntryListResponse)
        self.assertEqual(entriesTotalCount, list_.totalCount)
        self.assertEqual(entriesPageSize, len(list_.objects))
        [self.assertIsInstance(entry, KalturaMediaEntry)
         for entry in list_.objects]

        for entry in list_.objects:
            self.assertNotEqual(entry.relatedObjects, NotImplemented)
            self.assertIn(metadataResponseProfile.name, entry.relatedObjects)
            metadataList = entry.relatedObjects[metadataResponseProfile.name]
            self.assertIsInstance(metadataList, KalturaMetadataListResponse)
            self.assertEqual(len(metadataProfiles), len(metadataList.objects))
            for metadata in metadataList.objects:
                self.assertIsInstance(metadata, KalturaMetadata)
                self.assertEqual(entry.id, metadata.objectId)


def test_suite():
    return unittest.TestSuite((
        unittest.makeSuite(ResponseProfileTests)
        ))


if __name__ == "__main__":
    suite = test_suite()
    unittest.TextTestRunner(verbosity=2).run(suite)
