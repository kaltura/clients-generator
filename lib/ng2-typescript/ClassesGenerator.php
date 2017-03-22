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
        foreach ($this->serverMetadata->services as $service) {
            foreach ($service->actions as $serviceAction) {
                $result[] = $this->createServiceActionType($service, $serviceAction);
            }
        }

        foreach ($this->serverMetadata->classTypes as $class) {
            $result[] = $this->createClassType($class);
        }

        return $result;
    }

    function createClassType(ClassType $class)
    {
        $classBody = $this->createClass($class->name, $class->description, $class->base, $class->properties);

        $fileContent = "
import { KalturaObjectBase } from \"./utils/kaltura-object-base\";
import * as kenums from \"./kaltura-enums\";
import { KalturaObjects } from \"./utils/kaltura-objects\";

{$classBody}
";

        $file = new GeneratedFileData();
        $fileName = $this->utils->toLispCase($class->name);
        $file->path = "class/{$fileName}.ts";
        $file->content = $fileContent;
        $result[] = $file;
        return $file;
    }

    function createServiceActionType(Service $service,ServiceAction $serviceAction)
    {
        $className = $service->name . ucfirst($serviceAction->name);
        $classBody = $this->createClass($className, $serviceAction->description, null, $serviceAction->params);

        $fileContent = "
import { KalturaObjectBase } from \"./utils/kaltura-object-base\";
import * as kenums from \"./kaltura-enums\";
import { KalturaObjects } from \"./utils/kaltura-objects\";

{$classBody}
";

        $file = new GeneratedFileData();
        $fileName = $this->utils->toLispCase($className);
        $file->path = "action/{$fileName}.ts";
        $file->content = $fileContent;
        $result[] = $file;
        return $file;
    }

    function createClass($name, $description, $base, $properties)
    {
        $classTypeName = Utils::upperCaseFirstLetter($name);
        $desc = $description;

        $content = $this->createContent($properties);
        $classMetadata = "KalturaObjects.add('{$classTypeName}',{$classTypeName},{abstract : true, enums : {}, arrays : {}});";

        $result = "
export interface {$classTypeName}Args {$this->utils->ifExp($base, " extends " . $base . "Args","")} {
    {$this->utils->buildExpression($content->properties, NewLine, 1)}
}
{$this->getBanner()}
{$this->utils->createDocumentationExp('',$desc)}
export class {$classTypeName} extends {$this->utils->ifExp($base, $base,"KalturaObjectBase")} {

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


    function createContent($properties)
    {
        $result = new stdClass();
        $result->properties = array();
        $result->buildContent = array();
        $result->constructorContent = array();

        if (count($properties) != 0)
        {
            foreach($properties as $property) {
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