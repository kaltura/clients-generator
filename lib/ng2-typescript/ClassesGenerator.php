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
import { KalturaObjectBase } from \"./utils/kaltura-object-base\";
import * as kenums from \"./kaltura-enums\";
import { KalturaObjects } from \"./utils/kaltura-objects\";

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
        $classMetadata = "KalturaObjects.add('{$classTypeName}',{$classTypeName},{abstract : true, enums : {}, arrays : {}});";

        $result = "
export interface {$classTypeName}Args {$this->utils->ifExp($class->base, " extends " . $class->base . "Args","")} {
    {$this->utils->buildExpression($content->properties, NewLine, 1)}
}
{$this->getBanner()}
{$this->utils->createDocumentationExp('',$desc)}
export class {$classTypeName} extends {$this->utils->ifExp($class->base, $class->base,"KalturaObjectBase")} {

    objectType : string;
    {$this->utils->buildExpression($content->properties, NewLine, 1)}

    constructor(data? : {$classTypeName}Args)
    {
        super();
        {$this->utils->buildExpression($content->constructorContent, NewLine, 1)}
        Object.assign(this, data || {}, { objectType : '{$classTypeName}' });

    }
}{$classMetadata}
";

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
                $result->buildContent[] = "\"{$property->name}\"";

                // update constructor content
                if ($property->type == KalturaServerTypes::ArrayOfObjects)
                {
                    $result->constructorContent[] = "this.{$property->name} = []";
                }

                // update the properties declaration
                $result->properties[] = "{$property->name} : {$ng2ParamType};";
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