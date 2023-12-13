#!/bin/bash -e

# Make sure we're running from the root of the repo
cd $(dirname $0)

# Input env vars:
workDir="$PWD/temp"
client_name=all
outdir=$workDir/web/content
distdir=$PWD/dist

schema_url="https://www.kaltura.com/api_v3/api_schema.php"
clientsList='ajax android as3FlexClient cli csharp flex35 go java js ngx node nodeTypescript objc php53 php5Zend python ruby rxjs swift typescript'

# php5 has issues


# Prepare Clients List
clientName=$client_name
if [ $clientName != "all" ]; then
    clientsList="$clientsList $clientName"
fi

# Generate Clients
clients="$clientsList"

colorDefault="\033[0m"
colorPurple="\033[0;35m"
colorGreen="\033[0;32m"

echo -e "${colorGreen}Creating required folders ..."
mkdir -p $outdir/clientlibs

echo -e "${colorGreen}Getting XML ..."
curl -o "$outdir/clientlibs/KalturaClient.xml" "$schema_url"

rm -rf "$distdir"
mkdir -p "$distdir"

for generateClient in $clients
do
    echo -e "-----------------------------------------------\n"
    echo -e "${colorPurple}Generating client: $generateClient"
    mkdir -p $outdir/clientlibs/$generateClient
    echo -e "$colorDefault"
    php exec.php -x$outdir/clientlibs/KalturaClient.xml $generateClient $outdir/clientlibs
    if [ $? -eq 1 ]
    then    
        echo "Error Generating client $generateClient"
        echo "exit 1"
    fi
    mv -f $outdir/clientlibs/$generateClient $distdir
done

mv -f $outdir/clientlibs/KalturaClient.xml "$distdir"
rm -rf $workDir
