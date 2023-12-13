#!/bin/bash -e

# Make sure we're running from the root of the repo
cd $(dirname $0)/../..

# Input env vars:
client_name=all
outdir=$PWD/web/content
distdir=$PWD/dist

schema_url="https://www.kaltura.com/api_v3/api_schema.php"
clientsList="php5 php53 php5Zend node nodePlayServer java android js flex35 ajax as3FlexClient cli pojoOld bpmn typescript swift ngx python nodeTypescript"
# clientsList="php5 node" 

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
colorGreen="\033[0;32m"

echo -e "${colorGreen}Creating required folders ..."
mkdir -p $outdir/clientlibs
mkdir -p $outdir/temp

echo -e "${colorGreen}Getting XML ..."
curl -o "$outdir/clientlibs/KalturaClient.xml" "$schema_url"

for generateClient in $clients
do
    echo -e "-----------------------------------------------\n"
    echo -e "${colorPurple}Generating client: $generateClient"
    rm -rf $outdir/temp/$generateClient
    mkdir -p $outdir/clientlibs/$generateClient
    echo -e "$colorDefault"
    php exec.php -rserver -x$outdir/clientlibs/KalturaClient.xml $generateClient $outdir/clientlibs
    if [ $? -eq 1 ]
    then    
        echo "Error Generating client $generateClient"
        echo "exit 1"
    fi
    mv -f $outdir/clientlibs/$generateClient $outdir/temp/
    # tar czf $outdir/temp/$generateClient.tar.gz -C $outdir/temp/$generateClient .
done

echo -e "${colorGreen}Copying generated clients ..."
rm -rf "$distdir"
mkdir -p "$distdir"
mv -f $outdir/temp/* "$distdir"
mv -f $outdir/clientlibs/KalturaClient.xml "$distdir"
rm -rf server web
