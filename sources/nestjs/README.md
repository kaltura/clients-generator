# Kaltura NestJS Client

<div align="center">
  <a href="http://nestjs.com/" target="_blank">
    <img src="https://nestjs.com/img/logo_text.svg" width="150" alt="Nest Logo" />
  </a>
</div>

[![Conventional Commits](https://img.shields.io/badge/Conventional%20Commits-1.0.0-yellow.svg)](https://conventionalcommits.org) [![Gitter chat](https://badges.gitter.im/kaltura-ng/kaltura-ng.png)](https://gitter.im/kaltura-ng/kaltura-ng) [![Build Status](https://travis-ci.org/kaltura/KalturaGeneratedAPIClientsAngular.svg?branch=master)](https://travis-ci.org/kaltura/KalturaGeneratedAPIClientsNestjs)

> An easy-to-use facade to Kaltura server with typescript support for action requests, classes and enums.


## Instructions

### Getting Started
To keep being update review the [changelog](CHANGELOG.md) frequently.

 **Found a bug?** create [kaltura/clients-generator issue](https://github.com/kaltura/clients-generator/issues)


## Features list
View list of features [here](features.md).

# Building the sources
> This library is auto-generated using `kaltura/clients-generator` php engine. Feel free to clone, build and play with this library but in order to submit PR you should work against the [kaltura/clients-generator](https://github.com/kaltura/clients-generator) repo.


## Adding this library as a dependency to your project
Since this library was designed to be consumed directly and not using `NPMJS` repository, you will need to do the following steps to add it to your project:
1. run the following to transpile the library:
```bash
$ npm install
$ npm run deploy
```
2. open folder `dist` and find a `tar.gz` file starting with `kaltura-nestjs-ott-client-v`.
3. copy this file to your project (we recommend coping it to folder `libs`)
4. run the following command
 ```
 npm install file:the_path_to_the_file_including_its_name.tgz
 ```
> make sure you prefix the path with `file:` as shown above.

An example of a valid command will be: `npm install file:libs/nestjs-ott-client-v7.1.0-20173010-1053.tgz`

5. if you already have older version, delete that file. You should do this only after you installed the new version.

## Running integration tests
- In `src/api/tests` folder you should duplicate file `tests-config.template.ts` and name it `tests-config.ts`.
- Modify the file content to include valid information.
- Run `npm run test` to test the library

> If you want to test the library against the production server use the following as the endpoint in the config file: http://www.kaltura.com/api_v3/index.php/


## License and Copyright Information
All code in this project is released under the [AGPLv3 license](http://www.gnu.org/licenses/agpl-3.0.html) unless a different license for a particular library is specified in the applicable library path.

Copyright © Kaltura Inc. All rights reserved.


