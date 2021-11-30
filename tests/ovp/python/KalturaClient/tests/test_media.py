from __future__ import absolute_import

import unittest

import six

from .utils import (getTestFile, KalturaBaseTest)

from KalturaClient.Plugins.Core import (
    KalturaMediaEntry, KalturaMediaListResponse, KalturaMediaType)


class MediaTests(KalturaBaseTest):

    def test_list(self):
        resp = self.client.media.list()
        self.assertIsInstance(resp, KalturaMediaListResponse)

        objs = resp.objects
        self.assertIsInstance(objs, list)

        [self.assertIsInstance(o, KalturaMediaEntry) for o in objs]

    def test_createRemote(self):
        mediaEntry = KalturaMediaEntry()
        mediaEntry.setName('pytest.MediaTests.test_createRemote')
        mediaEntry.setMediaType(KalturaMediaType(KalturaMediaType.VIDEO))

        ulFile = getTestFile('DemoVideo.flv')
        uploadTokenId = self.client.media.upload(ulFile)

        mediaEntry = self.client.media.addFromUploadedFile(
            mediaEntry, uploadTokenId)

        self.assertIsInstance(mediaEntry.getId(), six.text_type)

        # cleanup
        self.client.media.delete(mediaEntry.id)


class Utf8_tests(KalturaBaseTest):

    def test_utf8_name(self):
        # an odd representation of the word 'FOO'
        test_unicode = six.u('\u03dd\xf5\xf6')
        mediaEntry = KalturaMediaEntry()
        mediaEntry.setName(u'pytest.MediaTests.test_UTF8_name'+test_unicode)
        mediaEntry.setMediaType(KalturaMediaType(KalturaMediaType.VIDEO))
        ulFile = getTestFile('DemoVideo.flv')
        uploadTokenId = self.client.media.upload(ulFile)

        # this will throw an exception if fail.
        mediaEntry = self.client.media.addFromUploadedFile(
            mediaEntry, uploadTokenId)

        self.addCleanup(self.client.media.delete, mediaEntry.getId())

    def test_utf8_tags(self):
        # an odd representation of the word 'FOO'
        test_unicode = u'\u03dd\xf5\xf6'
        mediaEntry = KalturaMediaEntry()
        mediaEntry.setName('pytest.MediaTests.test_UTF8_tags')
        mediaEntry.setMediaType(KalturaMediaType(KalturaMediaType.VIDEO))
        ulFile = getTestFile('DemoVideo.flv')
        uploadTokenId = self.client.media.upload(ulFile)

        mediaEntry.setTags(test_unicode)

        # this will throw an exception if fail.
        mediaEntry = self.client.media.addFromUploadedFile(
            mediaEntry, uploadTokenId)

        self.addCleanup(self.client.media.delete, mediaEntry.getId())


def test_suite():
    return unittest.TestSuite((
        unittest.makeSuite(MediaTests),
        unittest.makeSuite(Utf8_tests)
        ))


if __name__ == "__main__":
    suite = test_suite()
    unittest.TextTestRunner(verbosity=2).run(suite)
