# Change Log

All notable changes to this project will be documented in this file. See [standard-version](https://github.com/conventional-changelog/standard-version) for commit guidelines.

## 7.0.2 (2020-07-27)

### Fix

* warn when user bundle all the types and suggest a way to fix it

## 7.0.1 (2019-06-12)

### Fix

* upload asset was not working due to bad url creation.

## 7.0.0 (2019-01-24)


 ### Breaking Change
 
 * removed duplicated sources of adapters & types (removed duplicate folder `/types` of `api/types`). Affect only v5.0.3
 * change type of property `relatedObjects` from array to map


 before

 ``
export interface KalturaObjectBaseArgs
{
  relatedObjects? : KalturaObjectBase[];
}
``

 after

 ``
export interface KalturaObjectBaseArgs
{
  relatedObjects? : { [key: string] : KalturaObjectBase };
}
``


## 6.4.2 (2018-11-29)

### Fix

* typescript declaration issue for error response
* async unit-tests 


## 6.4.1 (2018-11-28)

### Fix

* cancel request now the cancel underline xhr request

### Fix

* use user custom chunk size if provided  


## 6.4.0 (2018-11-18)

### Features

* update minimal custom chunk size to 100Kb

### Fix

* use user custom chunk size if provided  

## 6.3.2 (2018-09-13)

### Fix

*  upgrade dependencies to handle vulnerabilities


## 6.3.1 (2018-05-09)

### Fix

* build upload url correctly to prevent failured during upload 

## 6.3.0 (2018-05-08)

### Features

* provide api to allow sending empty arrays to the server

To allow sending empty arrays for properties, use `allowEmptyArray()` method:
```
yourObjectInstance.allowEmptyArray('theRelevantArrayPropertyName')
``` 
this will instruct the kaltura client to send empty arrays if assigned for `restrictions` property

## 6.2.0 (2018-05-03)

### Features

* parse relatedObjects in responses from the server

## 6.1.2 (2018-04-08)

### Fix

* provide the api version as part of the multi-request payload instead of sending it as part of each inner requests payload 


<a name="6.1.1"></a>
## [6.1.1](https://github.com/kaltura/KalturaGeneratedAPIClientsTypescript/compare/v6.1.0...v6.1.1) (2017-12-07)

### Bugs

* non-chunked file upload progress fix


<a name="6.1.0"></a>
## [6.1.0](https://github.com/kaltura/KalturaGeneratedAPIClientsTypescript/compare/v6.0.0...v6.1.0) (2017-12-05)

### Features

* support requests that serve files by returning a valid download url for that files



<a name="6.0.0"></a>
# [6.0.0](https://github.com/kaltura/KalturaGeneratedAPIClientsTypescript/compare/v5.1.2...v6.0.0) (2017-11-26)


### Bug Fixes

* append action value to endpoint uri only if provided by request ([e53a9b5](https://github.com/kaltura/KalturaGeneratedAPIClientsTypescript/commit/e53a9b5))
* generate endpoint to service with '/api_v3/' as a prefix. ([fdaf513](https://github.com/kaltura/KalturaGeneratedAPIClientsTypescript/commit/fdaf513))
* support empty array as a valid resopnse ([6c677df](https://github.com/kaltura/KalturaGeneratedAPIClientsTypescript/commit/6c677df))
* Fix upload file in IE11 and edge and Safari

### Features

* add unit-testing ([2683820](https://github.com/kaltura/KalturaGeneratedAPIClientsTypescript/commit/2683820))
* update services according to new schema from 08/10/17 18:46:25 ([0350d10](https://github.com/kaltura/KalturaGeneratedAPIClientsTypescript/commit/0350d10))


### BREAKING CHANGES

* before:
The service api provided by the application included `/api_v3/` when provided
```
"https://www.kaltura.com/api_v3/"
```

after:
The service api provided by the application shouldn't include `/api_v3/` when provided
```
"https://www.kaltura.com"
```



<a name="5.1.2"></a>
## [5.1.2](https://github.com/kaltura/KalturaGeneratedAPIClientsTypescript/compare/v5.1.1...v5.1.2) (2017-10-29)


### Bug Fixes

* compile issue with typescript version ([c3cfd95](https://github.com/kaltura/KalturaGeneratedAPIClientsTypescript/commit/c3cfd95))
* use chunk upload only for services that support it ([43dd5e2](https://github.com/kaltura/KalturaGeneratedAPIClientsTypescript/commit/43dd5e2))



<a name="5.1.1"></a>
## [5.1.1](https://github.com/kaltura/KalturaGeneratedAPIClientsTypescript/compare/v5.1.0...v5.1.1) (2017-10-22)


### Bug Fixes

* upload of new files whose size is smaller then the chunk size ([107635e](https://github.com/kaltura/KalturaGeneratedAPIClientsTypescript/commit/107635e))



<a name="5.1.0"></a>
# [5.1.0](https://github.com/kaltura/KalturaGeneratedAPIClientsTypescript/compare/v5.0.0...v5.1.0) (2017-10-16)


### Bug Fixes

* generated package.json private attribute is set to false to allow publish to npm ([525a295](https://github.com/kaltura/KalturaGeneratedAPIClientsTypescript/commit/525a295))
* remove gibrish that prevented compilation ([c61caac](https://github.com/kaltura/KalturaGeneratedAPIClientsTypescript/commit/c61caac))


### Features

* add documentation to service actions ([301586e](https://github.com/kaltura/KalturaGeneratedAPIClientsTypescript/commit/301586e))
* support chunk file upload and resume upload action ([e04830a](https://github.com/kaltura/KalturaGeneratedAPIClientsTypescript/commit/e04830a))
* syncing services with server changes on date 02/10/17 04:15:21 ([de7a5a1](https://github.com/kaltura/KalturaGeneratedAPIClientsTypescript/commit/de7a5a1))



<a name="5.0.0"></a>
# [5.0.0](https://github.com/kaltura/KalturaGeneratedAPIClientsTypescript/compare/v4.0.0...v5.0.0) (2017-08-14)


### Bug Fixes

* fix 'acceptedTypes' property compilation issue. ([efe50aa](https://github.com/kaltura/KalturaGeneratedAPIClientsTypescript/commit/efe50aa))


### Features

* attach generated schema 'apiVersion' to each request ([5e5e2c8](https://github.com/kaltura/KalturaGeneratedAPIClientsTypescript/commit/5e5e2c8))
* support kaltura object properties of type map ([c866ca2](https://github.com/kaltura/KalturaGeneratedAPIClientsTypescript/commit/c866ca2))
* update services/actions ([46beb73](https://github.com/kaltura/KalturaGeneratedAPIClientsTypescript/commit/46beb73))


### BREAKING CHANGES

* changes in public api (services/actions/objects)



<a name="4.0.0"></a>
# [4.0.0](https://github.com/kaltura/KalturaGeneratedAPIClientsTypescript/compare/v3.0.0...v4.0.0) (2017-07-13)


### Features

* add service XInternal action XAddBulkDownload ([59b0ac6](https://github.com/kaltura/KalturaGeneratedAPIClientsTypescript/commit/59b0ac6))
* prevent importing the complete library implicitly, force import types explicitly ([cdfa3a6](https://github.com/kaltura/KalturaGeneratedAPIClientsTypescript/commit/cdfa3a6))


### BREAKING CHANGES

* Any imports from types/all should be modified to explicitly import the relevant types.

Before:

import { KalturaPermissionFilter, UserLoginByLoginIdAction } from 'kaltura-typescript-client/types/all';

After:

import { KalturaPermissionFilter } from 'kaltura-typescript-client/types/KalturaPermissionFilter';
import { UserLoginByLoginIdAction } from 'kaltura-typescript-client/types/UserLoginByLoginIdAction';



<a name="3.0.0"></a>
# [3.0.0](https://github.com/kaltura/KalturaGeneratedAPIClientsTypescript/compare/v2.1.0...v3.0.0) (2017-07-13)


### Features

* expose global ks and partner id from the client instead of from the configuration object. ([fac1eb7](https://github.com/kaltura/KalturaGeneratedAPIClientsTypescript/commit/fac1eb7))


### BREAKING CHANGES

* the global ks and partner id must be assigned on the client (previously was on the configuration)



<a name="1.1.1"></a>
## [1.1.1](https://github.com/kaltura/KalturaGeneratedAPIClientsTypescript/compare/v1.1.0...v1.1.1) (2017-05-10)



<a name="2.1.0"></a>
# [2.1.0](https://github.com/kaltura/KalturaGeneratedAPIClientsTypescript/compare/v2.0.0...v2.1.0) (2017-05-22)


### Features

* separate dynamic info (ks, partnerid) from configuration info (client tag, endpointUrl) ([12bf78e](https://github.com/kaltura/KalturaGeneratedAPIClientsTypescript/commit/12bf78e))



<a name="2.0.0"></a>
# [2.0.0](https://github.com/kaltura/KalturaGeneratedAPIClientsTypescript/compare/v1.1.0...v2.0.0) (2017-05-18)


### Features

* **kaltura-clients:** remove configuration objects, assign dynamic data directly on the clients ([8a30a72](https://github.com/kaltura/KalturaGeneratedAPIClientsTypescript/commit/8a30a72))


### BREAKING CHANGES

* **kaltura-clients:** the 'KalturaClientBaseConfiguration' and 'KalturaHttpClientConfiguration' objects were removed.

Any dynamic data assigned on them should be done directly on the client instance.



<a name="1.1.1"></a>
## [1.1.1](https://github.com/kaltura/KalturaGeneratedAPIClientsTypescript/compare/v1.1.0...v1.1.1) (2017-05-10)



<a name="1.1.0"></a>
# [1.1.0](http://github.com/KalturaGeneratedAPIClientsTypescript/compare/v1.0.0...v1.1.0) (2017-05-09)


### Bug Fixes

* fix upload file process ([2db951c](http://github.com/KalturaGeneratedAPIClientsTypescript/commit/2db951c))
* prevent IDE intellisense from importing by default types from the module that bundle the complete library ([61e5c4e](http://github.com/KalturaGeneratedAPIClientsTypescript/commit/61e5c4e))
* seamlessly add enum types used by requests to the bundle ([c417868](http://github.com/KalturaGeneratedAPIClientsTypescript/commit/c417868))
* send 'partnerId' only if provided (previously was sending 'undefined' if wan't provided) ([e128dfc](http://github.com/KalturaGeneratedAPIClientsTypescript/commit/e128dfc))
* setting dependent property in multi-request now uses zero index base. ([1599905](http://github.com/KalturaGeneratedAPIClientsTypescript/commit/1599905))


### Features

* notify developer when response from server returned with kaltura object type that wasn't bundled into the application ([78a2f7c](http://github.com/KalturaGeneratedAPIClientsTypescript/commit/78a2f7c))



<a name="1.0.0"></a>
# 1.0.0 (2017-05-08)


### Features

* **bundling:** we now support bundling only what the app is using ([a7b8ef4](http://github.com/KalturaGeneratedAPIClientsTypescript/commit/a7b8ef4))



<a name="1.0.0-beta.1"></a>
# 1.0.0-beta.1 (2017-05-07)

- [x] Bundle only used actions, classes and enums (a.k.a kaltura types) in your application to reduce bundle size.
- [x] Represent each kaltura types as a typed object with simple API.
- [x] Use generated action classes to easily access Kaltura services.
- [x] Invoke multi-requests against the server.
  - [x] Handle each request response separately.
  - [x] Expose interceptors to handle multi-request responses together.
  - [x] Use simple API to define dependent properties between requests using placeholders.
- [x] Support default properties value in requests.
- [x] Support the following property types:
   - [x] Dates.
   - [x] Simple types (number, string, boolean).  
   - [x] Enums (both numeric enums or string enums).
   - [x] Kaltura objects including inheritance and fallback mechanism.
- [x] Handle 'readonly' fields, guard against mutating them or sending them to the server.
- [x] Ability to upload files including abort & retry operations.
- [x] Hide complex server API syntax such as:
   - [x] Classify objects using 'objectType' property.
   - [x] Mark Field for deletion
- [x] Share properties among requests to reduce code duplication.
   - [x] Valid KS
   - [x] Partner Id
   - [x] Client Tag
