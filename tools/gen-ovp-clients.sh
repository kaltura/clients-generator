#!/bin/bash -e

cd $(dirname $0)/..

# Input env vars:
workDir="$PWD/temp"
client_name=all
outdir=$workDir/web/content
distdir=$PWD/dist

#clientsList='go java js node nodeTypescript php53 python ruby swift typescript'
# clientsList='js node nodeTypescript'
clientsList=nodeTypescript
moreClients='ajax android as3FlexClient cli csharp flex35  ngx  objc php53 php5Zend  rxjs '

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

mkdir -p $outdir/clientlibs
cp KalturaClient.xml "$outdir/clientlibs"

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
