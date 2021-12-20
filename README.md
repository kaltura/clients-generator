=======
# Kaltura Client Generator
The code in this repo is used to auto generate the Kaltura client libraries for each supported language.

[![License](https://img.shields.io/badge/license-AGPLv3-blue.svg)](http://www.gnu.org/licenses/agpl-3.0.html)

## Deployment Instructions
The list of supported clients is [here](config/generator.all.ini)

Download the API scheme XML from http://www.kaltura.com/api_v3/api_schema.php.

To generate one client run:
```
$ php /opt/kaltura/clients-generator/exec.php -x/path-to-xml/KalturaClient.xml $CLIENT_NAME
```

For example, to generate a `php53` client run:
```
php /opt/kaltura/clients-generator/exec.php -x/path-to-xml/KalturaClient.xml php53
```

To generate all available clients, run:
```
while read CLIENT;do php /opt/kaltura/clients-generator/exec.php -x/path-to-xml/KalturaClient.xml $CLIENT;done < /opt/kaltura/clients-generator/config/generator.all.ini
```

## Getting started with the API
To learn how to use the Kaltura API, go to [developer.kaltura.com](https://developer.kaltura.com/)

## How you can help (guidelines for contributors) 
Thank you for helping Kaltura grow! If you'd like to contribute please follow these steps:
* Use the repository issues tracker to report bugs or feature requests
* Read [Contributing Code to the Kaltura Platform](https://github.com/kaltura/platform-install-packages/blob/master/doc/Contributing-to-the-Kaltura-Platform.md)
* Sign the [Kaltura Contributor License Agreement](https://agentcontribs.kaltura.org/)

## Where to get help
* Join the [Kaltura Community Forums](https://forum.kaltura.org/) to ask questions or start discussions
* Read the [Code of conduct](https://forum.kaltura.org/faq) and be patient and respectful

## Get in touch
You can learn more about Kaltura and start a free trial at: http://corp.kaltura.com    
Contact us via Twitter [@Kaltura](https://twitter.com/Kaltura) or email: community@kaltura.com  
We'd love to hear from you!

## License and Copyright Information
All code in this project is released under the [AGPLv3 license](http://www.gnu.org/licenses/agpl-3.0.html) unless a different license for a particular library is specified in the applicable library path.   

Copyright © Kaltura Inc. All rights reserved.   
Authors and contributors: See [GitHub contributors list](https://github.com/kaltura/clients-generator/graphs/contributors).  

# Kaltura C# OTT API Client Library.
Compatible with Kaltura OTT server version 6.1.0.28931 and above.
