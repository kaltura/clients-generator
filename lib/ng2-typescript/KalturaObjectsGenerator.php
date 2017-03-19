<?php

require_once (__DIR__. '/NG2TypescriptGeneratorBase.php');
require_once (__DIR__. '/GeneratedFileData.php');

class KalturaObjectsGenerator extends NG2TypescriptGeneratorBase
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
            if (!$class->abstract) {
                $classTypes[] = $this->createFactoryFunction($class);
            }
        }

        $fileContent = "
import * as ktypes from \"./kaltura-new-types\";
import { KalturaObjectBase } from './kaltura-object-base';


{$this->getBanner()}
export class KalturaObjects
{
    {$this->utils->buildExpression($classTypes,NewLine,1)}
}
";

        $result = array();
        $file = new GeneratedFileData();
        $file->path = "kaltura-objects.ts";
        $file->content = $fileContent;
        $result[] = $file;
        return $result;
    }

    function createFactoryFunction(ClassType $class)
    {
        $classTypeName = Utils::upperCaseFirstLetter($class->name);
        $desc = $class->description;

        $functionName = lcfirst($class->name);
        $content = $this->createContentFromClass($class);

        $result = "
{$this->utils->createDocumentationExp('',$desc)}
static {$functionName}(data? : ktypes.{$classTypeName}) : KalturaObjectBase<ktypes.{$classTypeName}>
{
    return new KalturaObjectBase<ktypes.{$classTypeName}>('{$classTypeName}', data);
}";

        return $result;
    }


    function createContentFromClass(ClassType $class)
    {
        $result = new stdClass();
        $result->hasRequiredParams = false;

        if (count($class->properties) != 0)
        {
            foreach($class->properties as $property) {
                $result->hasRequiredParams |= !$property->optional;
            }
        }

        return $result;
    }
}