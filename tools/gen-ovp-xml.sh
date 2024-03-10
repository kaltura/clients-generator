#!/bin/bash -ex

# Required env vars:
# SERVER_BRANCH
# GH_TOKEN that has read access to server-saas-config repo

# Required tools:
# php 7.2
# GNU sed (sed on Linux or gsed on Mac)

cd $(dirname $0)/..


if which gsed; then SED=gsed; else SED=sed; fi

rm -rf server-temp
mkdir server-temp

cd server-temp

# Download server
curl -L "https://github.com/kaltura/server/archive/refs/heads/$SERVER_BRANCH.tar.gz" | tar xz
# cat ../$SERVER_BRANCH | tar xz
mv "server-$SERVER_BRANCH" server

# Download plugins ini from server-saas-config
curl -O -H "Authorization: token $GH_TOKEN" \
  -H 'Accept: application/vnd.github.v3.raw' \
  -L https://api.github.com/repos/kaltura/server-saas-config/contents/configurations/plugins.ini.base

echo "=== plugins.ini.base ===>>"
cat plugins.ini.base
echo "<<======================<<"

# Comment out problematic lines in bootstrap.php
bootstrapFile="server/api_v3/generator/bootstrap.php"
$SED -i 's/date_default_timezone_set/#date_default_timezone_set/g' "$bootstrapFile"
$SED -i 's/kLoggerCache/#kLoggerCache/g' "$bootstrapFile"
$SED -i 's/KalturaLog/#KalturaLog/g' "$bootstrapFile"

# Create required folders
mkdir -p server/cache/api_v3
mkdir -p server/clients/php5
mkdir xmlout

# Generate XML
php server/api_v3/generator/generate_xml.php $PWD

# Move the xml to the root of the repo and clean up
mv KalturaClient.xml ..
cd ..
rm -rf server-temp
