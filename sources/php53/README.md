## Kaltura PHP 5.3 API Client Library for usage with namespace.
Compatible with Kaltura server version @VERSION@ and above.

## Install
Navigation to php53 directory and run `composer install` to install class autoloader

## Getting Started

### Enumerating Content Library
```php
<?php
    use Kaltura\Client\Configuration as KalturaConfiguration;
    use Kaltura\Client\Client as KalturaClient;
    use Kaltura\Client\Enum\SessionType;
    use Kaltura\Client\Type\MediaEntryFilter;
    use Kaltura\Client\Type\FilterPager;
    use Kaltura\Client\ApiException;
  
    require_once(dirname(__FILE__).DIRECTORY_SEPARATOR.'php53'.DIRECTORY_SEPARATOR.'vendor'.DIRECTORY_SEPARATOR.'autoload.php');
    
    $config = new KalturaConfiguration();
    $config->setServiceUrl('https://www.kaltura.com'); //optional if using Saas
    $client = new KalturaClient($config);
    $ks = $client->generateSession(
        "YOUR_KALTURA_SECRET",
        "YOUR_USER_ID",
        SessionType::ADMIN,
        "YOUR_PARTNER_ID");
    $client->setKS($ks);
    $filter = new MediaEntryFilter();
    $pager = new FilterPager();
  
    try {
      $result = $client->getMediaService()->listAction($filter, $pager);
      var_dump($result);
    } catch (Exception $e) {
      echo $e->getMessage();
    }
?>
```

### Reusing API Connections
Setting this option on the config can provide a speed boost by keeping HTTP connections open (keepalive) and reusing TLS sessions. Connections are not reused by default.
```php
$config->setCurlReuse(true);
```

## Code contributions

#### Please note that you should not make pull requests to the repo as the clientlibs are auto generated upon every Core release.
#### Instead, please submit pulls to:

https://github.com/kaltura/clients-generator
code is under sources/php53

#### Also, note that we have 2 additional PHP clients libs: generator/sources/php5, generator/sources/zend so your changes may be relevant to them too.

[![Build Status](https://travis-ci.org/kaltura/KalturaGeneratedAPIClientsPHP53.svg?branch=master)](https://travis-ci.org/kaltura/KalturaGeneratedAPIClientsPHP53)