This source contains:
 - The Kaltura client library (KalturaClient.py & KalturaClientBase.py)
 - Auto generated core APIs (KalturaCoreClient.py)
 - Auto generated plugin APIs (KalturaPlugins/*.py)
 - Python library test code and data files (KalturaClient/tests)

== STANDARD DEPENDENCIES ==

The API library depends on the following python modules (included with python by default):
 - email.header
 - hashlib
 - httplib
 - mimetypes
 - os
 - re
 - socket
 - sys
 - time
 - urllib
 - urllib2
 - uuid or random & sha
 - xml.dom
 - xml.parsers.expat
 
== EXTERNAL DEPENDENCIES ==

The API client depends on the following python modules that are not included by default with python:
 - setuptools - can be downloaded from https://pypi.python.org/pypi/setuptools
 - requests (2.4.2 or above) - can be downloaded from https://pypi.python.org/pypi/requests/
 - requests-toolbelt - https://pypi.python.org/pypi/requests-toolbelt
 - six - https://pypi.python.org/pypi/six

requests is used to handle API calls to Kaltura. This means that if you want
to do multi part file uploads, you should pass through the file path rather
than an open file handle.

== INSTALLATION ==

Make sure you have the modules listed under the 'external dependencies' installed.
Install the Kaltura client by running 'python setup.py install' in the client's root directory.

== TESTING THE CLIENT LIBRARY ==
  
See KalturaClient/tests/README.txt

== RELEASE NOTES ==

Jan 2017 - Python 3 support. Replaced poster with requests.
Sep 2015 - support JSON requests, compatible with Kaltura server version 10.20.0 and above. 
Aug 2013 - the library was refactored to make it installable as a PyPI package.
	This refactoring changed the way Kaltura client plugin modules are loaded -
	before the change the metadata plugin (for example) was loaded by:
		from KalturaMetadataClientPlugin import *
	when upgrading the client, this will need to be changed to:
		from KalturaClient.Plugins.Metadata import *
