## Kaltura PHP 5 API Client Library.
Compatible with Kaltura server version @VERSION@ and above.

## Getting Started

### Enumerating Content Library
```php
<?php
  require_once('lib/KalturaClient.php');

  $config = new KalturaConfiguration();
  $config->setServiceUrl('https://www.kaltura.com'); //optional if using Saas
  $client = new KalturaClient($config);
  $ks = $client->session->start(
    "YOUR_KALTURA_SECRET",
    "YOUR_USER_ID",
    KalturaSessionType::ADMIN,
    YOUR_PARTNER_ID);
  $client->setKS($ks);

  $filter = new KalturaMediaEntryFilter();
  $pager = new KalturaFilterPager();

  try {
    $result = $client->media->listAction($filter, $pager);
    var_dump($result);
  } catch (Exception $e) {
    echo $e->getMessage();
  }
?>
```

### Reusing API Connections
Setting this option on the config can provide a speed boost by keeping HTTP connections open (keepalive) and reusing TLS sessions. Connections are not reused by default. (PHP 5.5 or greater required)
```php
$config->setCurlReuse(true);
```

## Code contributions

#### Please note that you should not make pull requests to the repo as the clientlibs are auto generated upon every Core release.
#### Instead, please submit pulls to:

https://github.com/kaltura/clients-generator
code is under sources/php5

#### Also, note that we have 2 additional PHP clients libs: generator/sources/php53, generator/sources/zend so your changes may be relevant to them too.

[![Build Status](https://travis-ci.org/kaltura/KalturaGeneratedAPIClientsPHP.svg?branch=master)](https://travis-ci.org/kaltura/KalturaGeneratedAPIClientsPHP)