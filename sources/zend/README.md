## Kaltura PHP API Client Library for Zend framework.
Compatible with Kaltura server version @VERSION@ and above.

## Getting Started

### Autoloading and Setup
The code required to load the client can be found in the test runnner.
See https://github.com/kaltura/clients-generator/blob/master/tests/ovp/zend/tests/run.php

### Reusing API Connections
Setting this option on the config can provide a speed boost by keeping HTTP connections open (keepalive) and reusing TLS sessions. Connections are not reused by default. (PHP 5.5 or greater required)
```php
$config->setCurlReuse(true);
```

## Code contributions

#### Please note that you should not make pull requests to the repo as the clientlibs are auto generated upon every Core release.
#### Instead, please submit pulls to:

https://github.com/kaltura/clients-generator
code is under sources/zend

#### Also, note that we have 2 additional PHP clients libs: generator/sources/php5, generator/sources/php53 so your changes may be relevant to them too.

[![Build Status](https://travis-ci.org/kaltura/KalturaGeneratedAPIClientsZF.svg?branch=master)](https://travis-ci.org/kaltura/KalturaGeneratedAPIClientsZF)