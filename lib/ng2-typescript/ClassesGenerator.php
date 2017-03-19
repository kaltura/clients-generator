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
import * as kenums from \"./kaltura-enums\";
import { KalturaObjectBase } from './kaltura-object-base';
import { KalturaObjectsMeta } from './utils/kaltura-objects-meta';


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
        $types = "'" . implode("', '", $class->types) . "'";

        $result = "
{$this->getBanner()}
{$this->utils->createDocumentationExp('',$desc)}
export interface {$classTypeName} {$this->utils->ifExp($class->base, "extends " . $class->base,"")} {
    {$this->utils->buildExpression($content->properties, NewLine, 1)}
}
KalturaObjectsMeta.setMeta('{$classTypeName}',
{
    types: [{$types}]
});
";

        return $result;
    }

    function createContentFromClass(ClassType $class)
    {
        $result = new stdClass();
        $result->properties = array();

        if (count($class->properties) != 0)
        {
            foreach($class->properties as $property) {
                $ng2ParamType = $this->toNG2TypeExp($property->type, $property->typeClassName);

                // update the properties declaration
                $result->properties[] = "{$property->name}? : {$ng2ParamType};";
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