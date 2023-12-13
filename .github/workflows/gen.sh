#!/bin/bash -e

# Input env vars:
# client_name

schema_url="https://www.kaltura.com/api_v3/api_schema.php"
clientsList="php5 php53 php5Zend node nodePlayServer java android js flex35 ajax as3FlexClient cli pojoOld bpmn typescript swift ngx python nodeTypescript"

# Prepare Clients List
clientName=$client_name
if [ $clientName != "all" ]; then
    clientsList="$clientsList $clientName"
fi

# Generate Clients
clients="$clientsList"
workDir="$PWD"

colorDefault="\033[0m"
colorPurple="\033[0;35m"
colorYellow="\033[1;33m"
colorGreen="\033[0;32m"

echo -e "${colorGreen}Creating required folders ..."
mkdir -p web/content/clientlibs
mkdir -p web/content/clientlibs/php5
mkdir -p web/content/temp

echo -e "${colorGreen}Getting XML ..."
curl -o "$workDir/web/content/clientlibs/KalturaClient.xml" "$schema_url"

for generateClient in $clients
do
    echo -e "-----------------------------------------------\n"
    echo -e "${colorPurple}Generating client: ${colorYellow}$generateClient"
    rm -rf web/content/temp/$generateClient
    mkdir -p web/content/clientlibs/$generateClient
    php $workDir/exec.php -r$workDir/server -x$workDir/web/content/clientlibs/KalturaClient.xml $generateClient $workDir/web/content/clientlibs
    if [ $? -eq 1 ]
    then    
        echo "Error Generating client $generateClient"
        echo "exit 1"
    fi
    mv -f web/content/clientlibs/$generateClient web/content/temp/
    ls -l web/content/temp/$generateClient
    tar czf web/content/temp/$generateClient.tar.gz -C web/content/temp/$generateClient .
done

echo -e "${colorGreen}Copying generated clients ..."
mkdir -p server-saas-clients/web_clients
mv -f web/content/temp/* server-saas-clients/web_clients
mv -f $workDir/web/content/clientlibs/KalturaClient.xml server-saas-clients/web_clients/
