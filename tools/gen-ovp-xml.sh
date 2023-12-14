#!/bin/bash -e

# Required env vars:
# SERVER_BRANCH
# GH_TOKEN or GITHUB_TOKEN that has read access to server-saas-config repo

cd $(dirname $0)/..

rm -rf server-temp
mkdir server-temp

cd server-temp

# Download server
git clone https://github.com/kaltura/server --depth=1 --branch=$SERVER_BRANCH --single-branch

# Download plugins ini from server-saas-config
gh api 'repos/kaltura/server-saas-config/contents/configurations/plugins.ini.base' --jq '.content | @base64d' > server/configurations/plugins.ini

# Comment out problematic lines in bootstrap.php
bootstrapFile="server/api_v3/generator/bootstrap.php"
sed -i 's/date_default_timezone_set/#date_default_timezone_set/g' $bootstrapFile
sed -i 's/kLoggerCache/#kLoggerCache/g' $bootstrapFile
sed -i 's/KalturaLog/#KalturaLog/g' $bootstrapFile

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
