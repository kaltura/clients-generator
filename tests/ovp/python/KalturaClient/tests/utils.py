import os, sys, inspect
import unittest

from six.moves import configparser

from KalturaClient import KalturaClient, KalturaConfiguration
from KalturaClient.Base import KalturaObjectFactory, KalturaEnumsFactory
from KalturaClient.Base import IKalturaLogger

from KalturaClient.Plugins.Core import KalturaSessionType

generateSessionFunction = KalturaClient.generateSessionV2
# generateSessionV2() needs the Crypto module, if we don't have it, we fallback to generateSession()
try:
    from Crypto import Random
    from Crypto.Cipher import AES
except ImportError:
    generateSessionFunction = KalturaClient.generateSession

dir = os.path.dirname(__file__)
filename = os.path.join(dir, 'config.ini')

config = configparser.ConfigParser()
config.read(filename)
PARTNER_ID = config.getint("Test", "partnerId")
SERVICE_URL = config.get("Test", "serviceUrl")
ADMIN_SECRET = config.get("Test", "adminSecret")
USER_NAME = config.get("Test", "userName")

import logging
logging.basicConfig(level = logging.DEBUG,
                    format = '%(asctime)s %(levelname)s %(message)s',
                    stream = sys.stdout)

class KalturaLogger(IKalturaLogger):
    def log(self, msg):
        logging.info(msg)

def GetConfig():
    config = KalturaConfiguration()
    config.requestTimeout = 500
    config.serviceUrl = SERVICE_URL
    config.setLogger(KalturaLogger())
    return config

def getTestFile(filename, mode='rb'):
    testFileDir = os.path.dirname(os.path.abspath(inspect.getfile(inspect.currentframe())))
    return open(testFileDir+'/'+filename, mode)
    
    

class KalturaBaseTest(unittest.TestCase):
    """Base class for all Kaltura Tests"""
    #TODO  create a client factory as to avoid thrashing kaltura with logins...
    
    def setUp(self):
        #(client session is enough when we do operations in a users scope)
        self.config = GetConfig()
        self.client = KalturaClient(self.config)
        assert hasattr(self.client, "ks"), "New KalturaClients do not have a .ks attribute."
        self.ks = generateSessionFunction(ADMIN_SECRET, USER_NAME, 
                                             KalturaSessionType.ADMIN, PARTNER_ID, 
                                             86400, "disableentitlement")
        assert self.ks
        self.client.setKs(self.ks)
        assert self.ks == self.client.getKs()
        assert self.ks == self.client.ks
            
            
    def tearDown(self):
        
        #do cleanup first, probably relies on self.client
        self.doCleanups()
        
        del(self.ks)
        del(self.client)
        del(self.config)
        
