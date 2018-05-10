# Library features list
The following is a list of all features supported by the library:

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
   - [x] Kaltura array & map
- [x] Handle 'readonly' fields, guard against mutating them or sending them to the server.
- [x] Ability to upload files including abort & retry operations.
- [x] Hide complex server API syntax such as:
   - [x] Classify objects using 'objectType' property.
   - [x] Mark Field for deletion
- [x] Runtime configuration to seamlessly attach properties to all the requests.
   - [x] Valid KS
   - [x] Partner Id
   - [x] Client Tag
- [x] Attach 'apiVersion' to each request
- [x] Upload file with chunk support
- [x] Resume file upload action
- [x] Add unit-tests
- [x] Support requests that serve files by returning a valid download url for that files.
- [x] AOT compile support (for kaltura-ngx-client library)
- [x] Simplify syntax of enums representing strings.
- [x] Parse relatedObjects in responses from the server

 Below is a list of features to be added:
- [ ] Generate `kalsig` per request.
- [ ] Request timeout support
- [ ] Cancel file upload should abort request
- [ ] When updating an object, ignore properties marked as insert only property.
- [ ] Code documentation of classes/enums/actions.
- [ ] Add developer/api guide.
- [ ] Warn against deprecated classes/enums/actions.
- [ ] Protect against bundling the complete library by mistake.
- [ ] Support setting 'undefined' for required properties when using 'setDependency' in multiple request.
