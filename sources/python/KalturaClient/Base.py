# =============================================================================
#                           _  __     _ _
#                          | |/ /__ _| | |_ _  _ _ _ __ _
#                          | ' </ _` | |  _| || | '_/ _` |
#                          |_|\_\__,_|_|\__|\_,_|_| \__,_|
#
# This file is part of the Kaltura Collaborative Media Suite which allows users
# to do with audio, video, and animation what Wiki platfroms allow them to do
# with text.
#
# Copyright (C) 2006-2011  Kaltura Inc.
#
# This program is free software: you can redistribute it and/or modify
# it under the terms of the GNU Affero General Public License as
# published by the Free Software Foundation, either version 3 of the
# License, or (at your option) any later version.
#
# This program is distributed in the hope that it will be useful,
# but WITHOUT ANY WARRANTY; without even the implied warranty of
# MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
# GNU Affero General Public License for more details.
#
# You should have received a copy of the GNU Affero General Public License
# along with this program.  If not, see <http:#www.gnu.org/licenses/>.
#
# @ignore
# =============================================================================
from __future__ import absolute_import

from .exceptions import KalturaClientException

import binascii
import hashlib
import json

import six

# Service response formats
KALTURA_SERVICE_FORMAT_JSON = 1
KALTURA_SERVICE_FORMAT_XML = 2
KALTURA_SERVICE_FORMAT_PHP = 3


# Xml utility functions
def getXmlNodeText(xmlNode):
    if not xmlNode.text:
        return six.u('')
    # In Python 2, ElementTree only converts to a unicode object
    # if the text contains non-ASCII characters. To maintain compatibility
    # with xml.dom, always return a unicode (Python 2)/ str (Python 3) object.
    return (
        xmlNode.text.decode('utf8')
        if isinstance(xmlNode.text, six.binary_type) else xmlNode.text)


def getXmlNodeBool(xmlNode):
    text = getXmlNodeText(xmlNode)
    if text == '0' or text == 'false':
        return False
    elif text == '1' or text == 'true':
        return True
    return None


def getXmlNodeInt(xmlNode):
    text = getXmlNodeText(xmlNode)
    if text == '':
        return None
    try:
        return int(text)
    except ValueError:
        return None


def getXmlNodeFloat(xmlNode):
    text = getXmlNodeText(xmlNode)
    if text == '':
        return None
    try:
        return float(text)
    except ValueError:
        return None


# Request parameters container
class KalturaParams(object):
    def __init__(self):
        self.params = {}

    def get(self):
        return self.params

    def put(self, key, value=None):
        if value is None:
            self.params[key + '__null'] = ''
        elif isinstance(value, six.binary_type):
            self.params[key] = value.decode('utf8')
        else:
            self.params[key] = six.text_type(value)

    def update(self, props):
        self.params.update(props)

    def add(self, key, objectProps):
        self.params[key] = objectProps

    def addObjectIfDefined(self, key, obj):
        if obj is NotImplemented:
            return
        if obj is None:
            self.put(key)
            return
        self.add(key, obj.toParams().get())

    def addArrayIfDefined(self, key, array):
        if array is NotImplemented:
            return
        if array is None:
            self.put(key)
            return
        if len(array) == 0:
            self.params[key] = {'-': ''}
        else:
            arr = []
            for curIndex in six.moves.range(len(array)):
                arr.append(array[curIndex].toParams().get())
            self.params[key] = arr

    def addMapIfDefined(self, key, map_):
        if map_ is NotImplemented:
            return
        if map_ is None:
            self.put(key)
            return
        if len(map_) == 0:
            self.params[key] = {'-': ''}
        else:
            dic = {}
            for currentKey in map_:
                dic[currentKey] = map_[currentKey].toParams().get()
            self.params[key] = dic

    def addStringIfDefined(self, key, value):
        if value is not NotImplemented:
            self.put(key, value)

    def addIntIfDefined(self, key, value):
        if value is not NotImplemented:
            self.put(key, value)

    def addStringEnumIfDefined(self, key, value):
        if value is NotImplemented:
            return
        if value is None:
            self.put(key)
            return
        if type(value) == str:
            self.addStringIfDefined(key, value)
        else:
            self.addStringIfDefined(key, value.getValue())

    def addIntEnumIfDefined(self, key, value):
        if value is NotImplemented:
            return
        if value is None:
            self.put(key)
            return
        if type(value) == int:
            self.addIntIfDefined(key, value)
        else:
            self.addIntIfDefined(key, value.getValue())

    def addFloatIfDefined(self, key, value):
        if value is not NotImplemented:
            self.put(key, value)

    def addBoolIfDefined(self, key, value):
        if value is NotImplemented:
            return
        if value is None:
            self.put(key)
            return
        self.put(key, value)

    def sort(self, params):
        for key in params:
            if isinstance(params[key], dict):
                params[key] = self.sort(params[key])
        sortedKeys = sorted(params.keys(), key=lambda x: six.text_type(x))
        sortedDict = {}
        for key in sortedKeys:
            sortedDict[key] = params[key]
        return sortedDict

    def toJson(self):
        return json.dumps(self.params)

    def signature(self, params=None):
        if params is None:
            params = self.params
        params = self.sort(params)
        return self.md5(self.toJson())

    @staticmethod
    def md5(str_):
        m = hashlib.md5()
        m.update(
            str_ if isinstance(str_, six.binary_type) else str_.encode("utf8"))
        return binascii.hexlify(m.digest())


# Kaltura objects factory
class KalturaObjectFactory(object):
    objectFactories = {}

    @classmethod
    def create(cls, objectNode, expectedTypeName):
        expectedType = cls.objectFactories[expectedTypeName]
        objTypeNode = objectNode.find('objectType')
        if objTypeNode is None:
            return None
        objType = getXmlNodeText(objTypeNode)
        if objType not in cls.objectFactories:
            objType = expectedType.__name__
        result = cls.objectFactories[objType]()
        if not isinstance(result, expectedType):
            raise KalturaClientException(
                "Unexpected object type '%s'" % objType,
                KalturaClientException.ERROR_INVALID_OBJECT_TYPE)
        result.fromXml(objectNode)
        return result

    @classmethod
    def createArray(cls, arrayNode, expectedElemType):
        results = []
        for arrayElemNode in list(arrayNode):
            results.append(cls.create(arrayElemNode, expectedElemType))
        return results

    @classmethod
    def createMap(cls, mapNode, expectedElemType):
        results = {}
        for mapElemNode in list(mapNode):
            keyNode = mapElemNode.find('itemKey')
            key = getXmlNodeText(keyNode)
            results[key] = cls.create(mapElemNode, expectedElemType)
        return results

    @classmethod
    def registerObjects(cls, objs):
        cls.objectFactories.update(objs)


# Abstract base class for all client objects
class KalturaObjectBase(object):
    def __init__(self, relatedObjects=NotImplemented):

        # @var map of KalturaListResponse
        # @readonly
        self.relatedObjects = relatedObjects

        KalturaObjectBase.PROPERTY_LOADERS = {
            'relatedObjects': (
                KalturaObjectFactory.createMap, 'KalturaListResponse')
        }

    def fromXmlImpl(self, node, propList):
        for childNode in list(node):
            nodeName = childNode.tag
            propName = nodeName
            if propName not in propList:
                propName += "_"
                if propName not in propList:
                    continue
            propLoader = propList[propName]
            if type(propLoader) == tuple:
                (func, param) = propLoader
                loadedValue = func(childNode, param)
            else:
                func = propLoader
                loadedValue = func(childNode)
            setattr(self, propName, loadedValue)

    def fromXml(self, node):
        self.fromXmlImpl(node, KalturaObjectBase.PROPERTY_LOADERS)

    def toParams(self):
        result = KalturaParams()
        result.put('objectType', 'KalturaObjectBase')
        return result

    def getRelatedObjects(self):
        return self.relatedObjects

    def setRelatedObjects(self, newRelatedObjects):
        self.relatedObjects = newRelatedObjects


# Abstract base class for all client services
class KalturaServiceBase(object):

    def __init__(self, client=None):
        self.client = client

    def setClient(self, client):
        self.client = client


# Client configuration class
class KalturaConfiguration(object):
    # Constructs new Kaltura configuration object
    def __init__(self, serviceUrl="http://www.kaltura.com", logger=None):
        self.logger = logger
        self.serviceUrl = serviceUrl
        self.format = KALTURA_SERVICE_FORMAT_XML
        self.requestTimeout = 120

    # Set logger to get kaltura client debug logs
    def setLogger(self, log):
        self.logger = log

    # Gets the logger (internal client use)
    def getLogger(self):
        return self.logger


# Client plugin interface class
class IKalturaClientPlugin(object):
    # @return KalturaClientPlugin
    @staticmethod
    def get():
        raise NotImplementedError()

    # @return array<KalturaServiceBase>
    def getServices(self):
        raise NotImplementedError()

    # @return string
    def getName(self):
        raise NotImplementedError()


# Client plugin base class
class KalturaClientPlugin(IKalturaClientPlugin):
    pass


# Kaltura enums factory
class KalturaEnumsFactory(object):
    enumFactories = {}

    @staticmethod
    def create(enumValue, enumType):
        if enumType not in KalturaEnumsFactory.enumFactories:
            raise KalturaClientException(
                "Unrecognized enum '%s'" % enumType,
                KalturaClientException.ERROR_INVALID_OBJECT_TYPE)
        return KalturaEnumsFactory.enumFactories[enumType](enumValue)

    @staticmethod
    def createInt(enumNode, enumType):
        enumValue = getXmlNodeInt(enumNode)
        if enumValue is None:
            return None
        return KalturaEnumsFactory.create(enumValue, enumType)

    @staticmethod
    def createString(enumNode, enumType):
        enumValue = getXmlNodeText(enumNode)
        if enumValue == '':
            return None
        return KalturaEnumsFactory.create(enumValue, enumType)

    @staticmethod
    def registerEnums(objs):
        KalturaEnumsFactory.enumFactories.update(objs)


# Implement to get Kaltura Client logs
class IKalturaLogger(object):
    def log(self, msg):
        raise NotImplementedError()
