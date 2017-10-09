# Change Log

All notable changes to this project will be documented in this file. See [standard-version](https://github.com/conventional-changelog/standard-version) for commit guidelines.

<a name="4.1.0"></a>
# 1.0.0 (2017-07-19)


### Features

*  Use generated action classes to easily access Kaltura services.
*  Represent each kaltura types as a typed object with simple API.
*  Bundle only used actions, classes and enums (a.k.a kaltura types) in your application to reduce bundle size.
*  Invoke multi-requests against the server.
  *  Handle each request response separately.
  *  Expose interceptors to handle multi-request responses together.
  *  Use simple API to define dependent properties between requests using placeholders.
*  Support default properties value in requests.
*  Support the following property types:
   *  Dates.
   *  Simple types (number, string, boolean).  
   *  Enums (both numeric enums or string enums).
   *  Kaltura objects including inheritance and fallback mechanism.
*  Handle 'readonly' fields, guard against mutating them or sending them to the server.
*  Ability to upload files including abort & retry operations.
*  Hide complex server API syntax such as:
   *  Classify objects using 'objectType' property.
   *  Mark Field for deletion
*  Runtime configuration to seamlessly attach properties to all the requests.
   *  Valid KS
   *  Partner Id
   *  Client Tag
*  Attach 'apiVersion' to each request
