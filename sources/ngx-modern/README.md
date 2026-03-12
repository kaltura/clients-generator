# Kaltura Angular Modern Client (Angular 18+)

[![Conventional Commits](https://img.shields.io/badge/Conventional%20Commits-1.0.0-yellow.svg)](https://conventionalcommits.org)

> A modern Angular 18+ compatible client for Kaltura API with support for action requests, classes and enums.

## Overview

This is the modern version of the Kaltura NGX client library, updated to support:
- **Angular 18+**
- **Node.js 18+** (including 20.x, 22.x, 24.x)
- **RxJS 7.x**
- **TypeScript 5.4+**
- Modern JavaScript features (ES2022)

## Requirements

- Node.js >= 18.0.0
- npm >= 9.0.0
- Angular >= 18.0.0

## Instructions

### Getting Started

To keep being update review the [changelog](projects/kaltura-ngx-client/CHANGELOG.md) frequently.

**Found a bug?** Create a [kaltura/clients-generator issue](https://github.com/kaltura/clients-generator/issues)

## Features list

View list of features [here](../ngx/features.md).

## Building the sources

> This library is auto-generated using `kaltura/clients-generator` php engine. Feel free to clone, build and play with this library but in order to submit PR you should work against the [kaltura/clients-generator](https://github.com/kaltura/clients-generator) repo.

### Building

```bash
# Install dependencies
npm install

# Build the library
npm run build

# Create a distributable package
npm run deploy
```

## Adding this library as a dependency to your project

Since this library was designed to be consumed directly and not using npmjs repository, you will need to do the following steps to add it to your project:

1. Run the following to transpile the library:
```bash
$ npm install
$ npm run deploy
```

2. Open folder `dist` and find a `tar.gz` file starting with `kaltura-ngx-client-v`.

3. Copy this file to your project (we recommend copying it to folder `libs`)

4. Run the following command:
```bash
npm install file:the_path_to_the_file_including_its_name.tgz
```
> Make sure you prefix the path with `file:` as shown above.

An example of a valid command will be: `npm install file:libs/kaltura-ngx-client-v13.0.0-20260312-1234.tgz`

5. If you already have older version, delete that file. You should do this only after you installed the new version.

## Running unit tests

```bash
npm run test
```

## Running integration tests

- In `projects/kaltura-ngx-client/src/tests` folder you should duplicate file `tests-config.template.ts` and name it `tests-config.ts`.
- Modify the file content to include valid information.
- Run `npm run test` to test the library

> If you want to test the library against the production server use the following as the endpoint in the config file: https://www.kaltura.com/api_v3/index.php/

## Security Information

For important security information about this library, please see [SECURITY.md](../ngx/SECURITY.md).

### Key Points
- This library is **auto-generated** and rebuilt with each Kaltura server deployment to match the latest API schema
- The client infrastructure is now based on **modern Angular 18+** for improved security and compatibility
- Node.js or Angular framework vulnerabilities are runtime concerns that require updating your application's environment

## Differences from legacy ngx library

| Feature | Legacy (ngx) | Modern (ngx-modern) |
|---------|--------------|---------------------|
| Angular version | 6.x | 18.x |
| Node.js support | 8.x - 18.x | 18.x+ |
| RxJS version | 6.x | 7.x |
| TypeScript | 2.7 | 5.4 |
| Module system | ES2015 | ES2022 |
| Build tooling | ng-packagr 3.x | ng-packagr 18.x |

## Migration from legacy ngx library

1. Update your project to Angular 18+
2. Update RxJS imports for deprecated APIs:
   - `throwError(error)` → `throwError(() => error)`
   - `Observable.create()` → `new Observable()`
3. Update your Node.js to version 18 or higher
4. Replace the old library with this modern version

## License and Copyright Information

All code in this project is released under the [AGPLv3 license](http://www.gnu.org/licenses/agpl-3.0.html) unless a different license for a particular library is specified in the applicable library path.

Copyright © Kaltura Inc. All rights reserved.
