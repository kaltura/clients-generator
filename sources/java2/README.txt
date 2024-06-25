This is the Readme for the Kaltura Java API Client Library.
You should read this before building and testing the client.
This library replaces the deprecated KalturaClient library.

== CONTENTS OF THIS PACKAGE ==

 - Kaltura Java Client Library API (src/main/java/com)
 - JUnit tests (src/test/java/com)


== DEPENDENCIES ==
 
The API depends on these libraries:
 - JSON in Java: http://json.org/
 - Gson JSON library: https://github.com/google/gson
 - OkHttp: http://square.github.io/okhttp/
 - OkIO: https://github.com/square/okio
 - Log4j: https://logging.apache.org/log4j
 - Apache Commons Codec 1.4: http://commons.apache.org/proper/commons-codec/
 - JUnit 3.8.2 (optional): http://junit.org



== BUILDING FROM SOURCE USING MAVEN ==

Use the following command to build the API without running unit tests:
  mvn -Dmaven.test.skip=true package

After running the command you will find 3 Jar files in the "target" directory.
  -- target/KalturaApiClient-X.X.X.jar contains the compiled client library
  -- target/KalturaApiClient-X.X.X-sources.jar contains the source code
  -- target/KalturaApiClient-X.X.X-javadoc.jar contains the Javadoc documentation for the library

== TESTING THE API CLIENT LIBRARY USING MAVEN ==

Copy src/test/resources/test.template.properties file to src/test/resources/test.properties and edit it, enter valid data to partnerId, adminSecret, and serviceUrl variables.

Use the following command to both build the API and run unit tests:
  mvn package

The same Jar files will be created as above.  The results of the unit tests will be stored in the file
target/surefire-reports/com.kaltura.client.test.KalturaTestSuite.txt



== BUILDING FROM SOURCE USING ECLIPSE ==

To build the API:
 - Setup the project in eclipse.
 - Build the project


== Proxy Support ==

The following methods are supported:

0. After initialising an object of the `Configuration` class, invoke the below methods:
config.setProxy("proxy.host");
config.setProxyPort(int_port);

1. Proxy type can be toggle so it can connect either thru HTTP or SOCKS (https)
If connection is thru an http proxy use 
config.setProxyType("HTTP")
If connection is thru https proxy use
config.setProxyType("SOCKS")

2. When a proxy requires basic authentication invoke the following methods:
config.setProxyUsername("username");
config.setProxyPassword("*****");

3. Set the following Java properties:
- http_proxy
- http_proxy_port
- http_proxy_type
- http_proxy_username
- http_proxy_password

4. Export the following ENV vars:
- http_proxy
- http_proxy_port
- http_proxy_type
- http_proxy_username
- http_proxy_password

=== Order of precedence ===
- If proxy set on the client object, it will be used, otherwise:
    - If Java properties are set and are valid, they will be used, otherwise:
        - If ENV vars are set and are valid, they will be used


== TESTING THE API CLIENT LIBRARY USING ECLIPSE ==

To run the JUnit test suite that accompanies this source:
 - Copy src/test/resources/test.template.properties file to src/test/resources/test.properties and edit it, enter valid data to partnerId, adminSecret, and serviceUrl variables.
 - Compile the client library.
 - Right click the KalturaTestSuite.java file and choose Debug As > JUnit Test.

  
== SETUP log4j LOGGING IN ECLIPSE ==

The launch settings are saved in the following files:
- 1. KalturaTestSuite.launch (the JUnit tests)
- 2. KalturaMainTest.launch (A main test class for quickly testing the build)

There is a log4j.properties file under src/main/resources. 
 - Edit it to set the log level as desired, defaults are:
  log4j.category.ClientBase.class=DEBUG
  log4j.logger.com.kaltura=ERROR

