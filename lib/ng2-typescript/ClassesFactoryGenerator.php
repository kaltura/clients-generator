<?php

require_once (__DIR__. '/NG2TypescriptGeneratorBase.php');
require_once (__DIR__. '/GeneratedFileData.php');

class ClassesFactoryGenerator extends NG2TypescriptGeneratorBase
{

    function __construct($serverMetadata)
    {
        parent::__construct($serverMetadata);
    }

    public function generate()
    {
        $result = array_merge(
            $this->createFactory()
        );

        return $result;
    }

    function createFactory()
    {
        $servicesCaseContent = array();

        foreach ($this->serverMetadata->classTypes as $class) {
            $classTypeName = Utils::upperCaseFirstLetter($class->name);
            $servicesCaseContent[] = "case '{$classTypeName}':
    result = new ktypes.{$classTypeName}();
    break;";
        }

        $fileContent = "
import {KalturaServerObject} from './utils/kaltura-server-object';
import * as ktypes from \"./kaltura-types\";

export class KalturaTypesFactory{
    static create(requestType : string, fallbackTypeName : string, responseData : any) : any
    {
        let result : KalturaServerObject = null;

        if (requestType && responseData) {
            switch (requestType) {
                {$this->utils->buildExpression($servicesCaseContent,NewLine,4)};
                default:
                    console.warn(`KalturaTypesFactory.create(): Failed to create wrapper for object of type \${requestType}. Returning the original object instead.`);
                    if (fallbackTypeName) {
                        result = KalturaTypesFactory.create(fallbackTypeName, null, responseData);
                    }else {
                        result = null;
                    }
            }

            if (result) {
                result._setObjectData(responseData)
            }
        }

        return result;
    }
}";

        $result = array();
        $file = new GeneratedFileData();
        $file->path = "kaltura-types-factory.ts";
        $file->content = $fileContent;
        $result[] = $file;
        return $result;
    }

}