#!/bin/bash - 
set -o nounset
if [ -r `dirname $0`/colors.sh ];then
    . `dirname $0`/colors.sh
fi
if [ $# -lt 2 ];then
    echo -e "${BRIGHT_RED}Usage: $0 </path/cli/lib/prefix> <partner_id>${NORMAL}"
    exit 1
fi
PREFIX=$1
PARTNER_ID=$2
shopt -s expand_aliases
. $PREFIX/kalcliAutoComplete
. $PREFIX/kalcliAliases.sh
PASSED=0
FAILED=0
report()
{
    TEST_NAME=$1
    RC=$2
    if [ $RC -eq 0 ];then
	PASSED=`expr $PASSED + 1`
	echo -e "${BRIGHT_GREEN}${TEST_NAME} PASSED${NORMAL}"
    else
	FAILED=`expr $FAILED + 1`
	echo -e "${BRIGHT_RED}${TEST_NAME} FAILED${NORMAL}"
    fi
}
TEST_FLV="$PREFIX/tests/DemoVideo.flv"
echo -e "${BRIGHT_BLUE}######### Running tests ###########${NORMAL}"
KS=`genks -b $PARTNER_ID`
kalcli -x media list ks=$KS
report "media->list()" $?
SOME_ENTRY_ID=`kalcli -x baseentry list pager:objectType=KalturaFilterPager pager:pageSize=1 filter:objectType=KalturaBaseEntryFilter   filter:typeEqual=1 ks=$KS|awk '$1 == "id" {print $2}'`
report "baseentry->list()" $?
kalcli -x baseentry updateThumbnailFromSourceEntry  entryId=$SOME_ENTRY_ID sourceEntryId=$SOME_ENTRY_ID ks=$KS  timeOffset=3
report "baseentry->updateThumbnailFromSourceEntry()" $? 
TOKEN=`kalcli -x uploadtoken add uploadToken:objectType=KalturaUploadToken uploadToken:fileName=$TEST_FLV  ks=$KS|awk '$1 == "id" {print $2}'`
report "uploadtoken->add()" $?
kalcli -x uploadtoken upload fileData=@$TEST_FLV uploadTokenId=$TOKEN ks=$KS
report "uploadtoken->upload()" $?
ENTRY_ID=`kalcli -x baseentry addFromUploadedFile uploadTokenId=$TOKEN partnerId=$PARTNER_ID ks=$KS entry:objectType=KalturaBaseEntry |awk '$1 == "id" {print $2}'`
report "baseentry->addFromUploadedFile()" $?
TEST_CAT_NAM='testme'+$RANDOM
kalcli -x category add category:objectType=KalturaCategory category:name=$TEST_CAT_NAM  ks=$KS
report "category->add()" $?
if [ $RC -eq 0 ];then
    TOTALC=`kalcli -x category list filter:objectType=KalturaCategoryFilter filter:fullNameEqual=$TEST_CAT_NAM ks=$KS|awk '$1 == "totalCount" {print $2}'`
    if [ $TOTALC -eq 1 ];then
	report "category->list()" 0
    else
	report "category->list()" 1
    fi
    CAT_ID=`kalcli -x category list filter:objectType=KalturaCategoryFilter filter:fullNameEqual=$TEST_CAT_NAM ks=$KS|awk '$1 == "id" {print $2}'`
    kalcli -x category delete  id=$CAT_ID ks=$KS
    report "category->delete()" $?
fi
echo -e "${BRIGHT_GREEN}PASSED tests: $PASSED ${NORMAL}, ${BRIGHT_RED}FAILED tests: $FAILED ${NORMAL}"
if [ "$FAILED" -gt 0 ];then
    exit 1
fi
