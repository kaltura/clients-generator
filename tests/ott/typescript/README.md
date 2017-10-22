# Kaltura Typescript Client

[![Conventional Commits](https://img.shields.io/badge/Conventional%20Commits-1.0.0-yellow.svg)](https://conventionalcommits.org) [![Gitter chat](https://badges.gitter.im/kaltura-ng/kaltura-ng.png)](https://gitter.im/kaltura-ng/kaltura-ng) [![npm version](https://badge.fury.io/js/kaltura-ott-typescript-client.svg)](https://badge.fury.io/js/kaltura-ott-typescript-client)

> An easy-to-use facade to Kaltura OTT server with typescript support for action requests, classes and enums.

This library uses `Typescript` files that are transpiled  into `ECMAScript 5` using `commonjs` as a module system.

This library have **_zero dependencies_** at runtime and it can be used in any Javascript project. Those who are using Typescript will benefit even more.

## Installation

use 'npm' to get the library
```bash
$ npm install kaltura-ott-typescript-client
```

## Instructions

### Getting Started
To keep being update review the [changelog](CHANGELOG.md) frequently.

 **Have a question?** Ask us on [Gitter](https://gitter.im/kaltura-ng/kaltura-ng).

 **Found a bug?** create github issue in [KalturaGeneratedAPIClientsTypescript](https://github.com/kaltura/KalturaGeneratedAPIClientsTypescript/issues) repository.



## Features list
View list of features [here](features.md).

# Building the sources
> This library is auto-generated using `kaltura/clients-generator` php engine. Feel free to clone, build and play with this library but in order to submit PR you should work against the [kaltura/clients-generator](https://github.com/kaltura/clients-generator) repo.


## Building the client library
After cloning the repo you should install dependent libraries:
```bash
$ npm install
$ npm run build
```

## Running integration tests
- In `src/tests` folder you should duplicate file `tests-config.tpl.ts` and name it `tests-config.ts`.
- Modify the file content to include valid information.
- Run `npm run test` to test the library

## License and Copyright Information
All code in this project is released under the [AGPLv3 license](http://www.gnu.org/licenses/agpl-3.0.html) unless a different license for a particular library is specified in the applicable library path.

Copyright © Kaltura Inc. All rights reserved.