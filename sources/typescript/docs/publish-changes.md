# Publish Changes
Publishing changes requires synchronization between two different repos:
- [client-generator](https://github.com/kaltura/clients-generator)
- this repo (either [KalturaGeneratedAPIClientsTypescript](https://github.com/kaltura/KalturaGeneratedAPIClientsTypescript) or [KalturaOTTGeneratedAPIClientsTypescript](https://github.com/kaltura/KalturaOTTGeneratedAPIClientsTypescript)).

It relies on several manual steps so be careful when you publish.


## Step 1 - Develop changes
Developing is done directly on this repository. You can use `npm link` to test it before you are ready to update the client-generator repository.

**Setup a local link between this library to your application (kmc-ng, n-tvm)**
```
npm run build
cd dist
npm link
```
Make sure you run `npm link` inside `dist` folder as shown above.

**Setup a connection between the application to the local library**
```
cd /path-toyour-application-root-folder
npm link kaltura-typescript-client            # for OVP library
npm link kaltura-ott-typescript-client     # for OTT library
```

You can now test your application and work with the local version. Once you are ready to publish your changes continue to next step.

## Step 2 - Update client-generator repository
> The code snippets below are written in bash.

1. Setup client generators repository.
```
git clone https://github.com/kaltura/KalturaGeneratedAPIClientsTypescript.git       # for OVP library
git clone https://github.com/kaltura/KalturaOttGeneratedAPIClientsTypescript.git  # for OTT library
```

2. Download latest version of `KalturaClient.xml`.
```bash
$ curl -O http://www.kaltura.com/api_v3/api_schema.php
$ mv api_schema.php KalturaClient.xml
```

3. Change the generator so it will build the library with your changes.
- Generator scripts can be found in folder `lib/typescript`.
- Static resources shared between OVP and OTT can be found in folder `sources/typescript`.
- Static resources of OVP library can be found in folder `tests/ovp/typescript`.
- Static resources of OTT library can be found in folder `tests/ott/typescript`.

4. Generate typescript library into `output/typescript` folder.
```bash
$ /usr/local/php5/bin/php exec.php typescript ./output
```
**Note:** you need php5 to build the library

5. Copy your changes into the local library and test again. You can either do it manually or use a script that delete everything beside `.git`, `.idea.` (my IDE folder) and `node_modules`.
```
target=~/dev/github/kaltura/KalturaGeneratedAPIClientsTypescript

find $target -maxdepth 1 ! -name .idea ! -name node_modules ! -name .git ! -path $target -exec rm -rf {} \;

cp -R ./output/typescript/. $target/
```
**Note:** change the target path and the IDE folder to those which are relevant to your machine.

6. Test it.

# Step 4 - Prepare the library to deployment

> This step will prepare [client-generator](https://github.com/kaltura/clients-generator) for deployment.

1. Create a new branch with your changes, make sure you checkout from the default branch. **Since the master branch is not the default one,** use the [following link](https://github.com/kaltura/clients-generator/branches) to find which is the default.


2. Navigate to the relevant static resources folder:
 - Static resources of OVP library can be found in folder `tests/ovp/typescript`.
 - Static resources of OTT library can be found in folder `tests/ott/typescript`.

3. If your changes touch the package dependencies, copy `package.json` and `package.lock.json` files from your local library.

4. Update the following files:
  - `package.json` with the new version.
  - `changelog.me` with the relevant content.
  - `features.md` with new features if added.

5. [Open a PR](https://github.com/kaltura/clients-generator/pulls).

6. Your PR was approved? Continue to the final step.

# Step 5 - Publish your library

> Continue to this step only once your code was approved and merged to default branch.

1. Run the generator from default branch and update your local library

2. Build your local library
```
npm run build
```

3. Re-check your application.

4. Commit changes and tag with your version. prefix with '`v`', for example `git tag v1.2.3` for version 1.2.3

5. push changes including tags.
```
git push --follow-tags origin master;
```

5. Publish to npm if you have the right credentials.
```
cd dist
npm publish
```
**Note** that we publish from `dist` folder!