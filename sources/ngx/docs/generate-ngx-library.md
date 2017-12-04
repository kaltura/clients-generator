# How to extend/generate NGX (this) library

> The code snippets below are written in bash.

## Step 1 - generate the library
1. Setup client generators repository.
```
git clone https://github.com/kaltura/clients-generator
```

2. Download latest version of `KalturaClient.xml`.
```bash
$ curl -O http://www.kaltura.com/api_v3/api_schema.php
$ mv api_schema.php KalturaClient.xml
```

3. Change the generator so it will build the library with your changes.
- Generator scripts can be found in folder `lib/typescript`.
- Static resources shared between OVP and OTT can be found in folder `sources/ngx`.
- Static resources of OVP library can be found in folder `tests/ovp/ngx`.
- Static resources of OTT library can be found in folder `tests/ott/ngx`.

4. Generate ngx library into `output/ngx` folder.
```bash
$ /usr/local/php5/bin/php exec.php ngx ./output
```
**Note:** you need php5 to build the library


## Step 2 - Test the library locally
You can use `npm link` to test the library locally.

1. link the library to global npm
```
npm install
npm run build
cd dist
npm link
```
Make sure you run `npm link` inside `dist` folder as shown above.

2. link the library to your application
```
cd /path-to-your-application-root-folder
npm link kaltura-ngx-client
```

You can now test your application and work with the local version.