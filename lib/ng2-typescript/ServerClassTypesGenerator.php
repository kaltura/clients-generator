<?php

require_once (__DIR__. '/GeneratorBase.php');
require_once (__DIR__. '/GeneratedFileData.php');

class ServerClassTypesGenerator extends GeneratorBase
{

    function __construct($serverMetadata)
    {
        parent::__construct($serverMetadata);
    }

    public function generate()
    {
        $result = array_merge(
            $this->createClassTypes()
        );

        return $result;
    }

    function createClassTypes()
    {
        $result = array();

        foreach ($this->serverMetadata->classTypes as $class) {
            $result[] = $this->createClassType($class);
        }

        $fileContent="
import {KalturaRequestObject} from \"../utils/kaltura-request-object\";
import {KalturaSearchOperatorType} from \"./enums\";
import {KalturaUtils} from \"../utils/kaltura-utils\";
import {KalturaPermissionStatus} from \"./enums\";
";

        return $result;
    }

    function createClassType(ClassType $class)
    {
        $result = new GeneratedFileData();

        $classTypeName = $this->upperCaseFirstLetter($class->name);
        $desc = $class->description;

        $paramsContent = $this->createPropertyContent($class);

        $fileContent = "
{$this->getBanner()}
import {KalturaObject} from \"../utils/kaltura-object\";
import {KalturaUtils} from \"../utils/kaltura-utils\";
import * as enums from \"./enums\";
import {KalturaTypesFactory} from \"./kaltura-types-factory\";


{$this->createDocumentationExp('',$desc)}
export {$this->ifExp($class->abstract, "abstract", "")} class {$classTypeName} extends {$this->ifExp($class->base, $class->base,"KalturaObject")} {

    get objectType() : string{
        return '{$class->name}';
    }

    {$this->buildExpression($paramsContent->properties, NewLine, 1)}

    constructor({$this->buildExpression($paramsContent->constructor, ', ')})
    {
        {$this->buildExpression($paramsContent->constructorContent, NewLine, 2 )}
    }

    build():any {
        return Object.assign({},
            super.build(),
            {
                {$this->buildExpression($paramsContent->buildContent,  ',' . NewLine, 4)}
            });
    };

  setData(handler : (request :  {$classTypeName}) => void) :  {$classTypeName}
    {
        if (handler)
        {
            handler(this);
        }

        return this;
    }
}";

        $fileName = $this->toLispCase($classTypeName) . ".ts";
        $result->path = "types/{$this->toLispCase($classTypeName)}.ts";
        $result->content = $fileContent;
        return $result;
    }

    function createPropertyContent(ClassType $class)
    {
        $result = new stdClass();
        $result->properties = array();
        $result->constructor = array();
        $result->constructorContent = array();
        $result->buildContent = array();

        $result->buildContent[] = "objectType : \"{$class->name}\"";

        $requiredParams = $class->getRequiredProperties();
        $optionalParams = $class->getOptionaProperties();

        foreach($class->properties as $property)
        {
            $result->properties[] = "
    get {$property->name}() : {$property->type}
    {
        return <{$property->type}>this.objectData['{$property->name}'];
    }

    set {$property->name}(value : {$property->type})
    {
        this.objectData['{$property->name}'] = value;
    }";
        }

        foreach($requiredParams as $property)
        {
            $result->constructor[] = "{$property->name} : {$property->type}";
            $result->constructorContent[] =  "this.{$property->name} = {$property->name};";
            $result->buildContent[] = "{$property->name} : this.{$property->name}";
        }

        if (count($optionalParams) != 0)
        {
            $constructorOptionalParams = array();

            foreach($optionalParams as $property)
            {
                $constructorOptionalParams[] = "{$property->name}? : {$property->type}";
                $result->constructorContent[] = "this.{$property->name} = typeof additional.{$property->name} !== 'undefined' ?  additional.{$property->name} :  $property->default;";
                $result->buildContent[] = "{$property->name} : this.{$property->name}";
            }

            $result->constructor[] = "additional? : {" . join(", ", $constructorOptionalParams) . "} = {}";

        }

        return $result;
    }
}