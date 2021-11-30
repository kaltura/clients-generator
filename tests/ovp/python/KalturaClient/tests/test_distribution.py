from __future__ import absolute_import

import unittest

from .utils import KalturaBaseTest

from KalturaClient.Plugins.ContentDistribution import (
    KalturaDistributionProfile,
    KalturaDistributionProfileListResponse,
    KalturaDistributionProvider,
    KalturaDistributionProviderListResponse,
    KalturaEntryDistribution,
    KalturaEntryDistributionListResponse,
    )


class DistributionProviderTests(KalturaBaseTest):

    def test_list(self):
        resp = self.client.contentDistribution.distributionProvider.list()
        self.assertIsInstance(resp, KalturaDistributionProviderListResponse)

        objs = resp.objects
        self.assertIsInstance(objs, list)

        [self.assertIsInstance(o, KalturaDistributionProvider) for o in objs]


class DistributionProfileTests(KalturaBaseTest):

    def test_list(self):
        resp = self.client.contentDistribution.distributionProfile.list()
        self.assertIsInstance(resp, KalturaDistributionProfileListResponse)

        objs = resp.objects
        self.assertIsInstance(objs, list)

        [self.assertIsInstance(o, KalturaDistributionProfile) for o in objs]


class EntryDistributionTests(KalturaBaseTest):

    def test_list(self):
        resp = self.client.contentDistribution.entryDistribution.list()
        self.assertIsInstance(resp, KalturaEntryDistributionListResponse)

        objs = resp.objects
        self.assertIsInstance(objs, list)

        [self.assertIsInstance(o, KalturaEntryDistribution) for o in objs]


def test_suite():
    return unittest.TestSuite((
        unittest.makeSuite(DistributionProviderTests),
        unittest.makeSuite(DistributionProfileTests),
        unittest.makeSuite(EntryDistributionTests),
        ))


if __name__ == "__main__":
    suite = test_suite()
    unittest.TextTestRunner(verbosity=2).run(suite)
