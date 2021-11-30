Kaltura JavaScript API Client Library.

The library contain the following files:
 - example.html
 - jquery-3.1.0.min.js
 - KalturaClient.js - all client functionality without the services.
 - KalturaClient.min.js - KalturaClient.js minified.
 - KalturaFullClient.js - all client functionality including all services.
 - KalturaFullClient.min.js - KalturaFullClient.js minified.
 - Services files, e.g. KalturaAccessControlProfileService.js.
 - Minified services files, e.g. KalturaAccessControlProfileService.min.js.

If you're lazy developer and don't want to include each used service separately, 
or if you find yourself including many services as run time,
you might want to use the single KalturaFullClient.min.js that already contains all services.

If your application is using merely few services, it would be more efficient to include only KalturaClient.min.js
and the minified services files that you need.

