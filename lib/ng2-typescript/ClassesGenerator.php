<?php

require_once (__DIR__. '/NG2TypescriptGeneratorBase.php');
require_once (__DIR__. '/GeneratedFileData.php');

class ClassesGenerator extends NG2TypescriptGeneratorBase
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
        $classTypes = array();

        $kalturaKnownTypes = array();

        foreach ($this->serverMetadata->classTypes as $class) {
            $classTypes[] = $this->createClassTypeExp($class);

            $classTypeName = Utils::upperCaseFirstLetter($class->name);
        }

        $fileContent = "
import {KalturaServerObject, DependentProperty, DependentPropertyTarget, KalturaPropertyTypes} from \"./utils/kaltura-server-object\";
import * as kenums from \"./kaltura-enums\";
import { JsonMember, JsonObject } from './utils/typed-json';

{$this->utils->buildExpression($classTypes,NewLine)}
";

        $result = array();
        $file = new GeneratedFileData();
        $file->path = "kaltura-types.ts";
        $file->content = $fileContent;
        $result[] = $file;
        return $result;
    }

    function createClassTypeExp(ClassType $class)
    {
        $classTypeName = Utils::upperCaseFirstLetter($class->name);
        $desc = $class->description;

        $content = $this->createContentFromClass($class);

        $result = "
{$this->getBanner()}
{$this->utils->createDocumentationExp('',$desc)}
@JsonObject
export {$this->utils->ifExp($class->abstract, "abstract", "")} class {$classTypeName} extends {$this->utils->ifExp($class->base, $class->base,"KalturaServerObject")} {

    get objectType() : string
    {
        return '{$class->name}';
    }

    {$this->utils->buildExpression($content->properties, NewLine, 1)}

    constructor()
    {
        super();

        {$this->utils->buildExpression($content->constructorContent, NewLine, 1)}
    }
}";

        return $result;
    }


    function createContentFromClass(ClassType $class)
    {
        $result = new stdClass();
        $result->properties = array();
        $result->buildContent = array();
        $result->constructorContent = array();

        if (count($class->properties) != 0)
        {
            foreach($class->properties as $property) {
                $ng2ParamType = $this->toNG2TypeExp($property->type, $property->typeClassName);

                // update the build function
                $result->buildContent[] = "\"{$property->name}\"";// $this->requestBuildExp($property->name, $property->type,false);

                // update constructor content
                if ($property->type == KalturaServerTypes::ArrayOfObjects)
                {
                    $result->constructorContent[] = "this.{$property->name} = []";
                }

                // update the properties declaration
                $decorator = null;
                switch($property->type)
                {
                    case KalturaServerTypes::ArrayOfObjects:
                        $decorator = "@JsonMember({elements : {$property->typeClassName}})";
                        break;
                    default:
                        $decorator = "@JsonMember";
                        break;
                }
                $result->properties[] = "{$decorator} {$property->name} : {$ng2ParamType};";
            }
        }

        return $result;
    }

    protected function toNG2TypeExp($type, $typeClassName, $resultCreatedCallback = null)
    {
        return parent::toNG2TypeExp($type,$typeClassName,function($type,$typeClassName,$result)
        {
            switch($type) {
                case KalturaServerTypes::EnumOfInt:
                case KalturaServerTypes::EnumOfString:
                    $result = 'kenums.' . $result;
                    break;
            }

            return $result;
        });
    }
}