<?php

require_once (__DIR__. '/NG2TypescriptGeneratorBase.php');
require_once (__DIR__. '/GeneratedFileData.php');

class ClassTypesGenerator extends NG2TypescriptGeneratorBase
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

        foreach ($this->serverMetadata->classTypes as $class) {
            $classTypes[] = $this->createClassTypeExp($class);
        }

        $fileContent = "
import {KalturaObject} from \"../utils/kaltura-object\";
import {KalturaUtils} from \"../utils/kaltura-utils\";
import * as enums from \"./enums\";
import {KalturaTypesFactory} from \"./kaltura-types-factory\";

{$this->utils->buildExpression($classTypes,NewLine . NewLine)}
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
export {$this->utils->ifExp($class->abstract, "abstract", "")} class {$classTypeName} extends {$this->utils->ifExp($class->base, $class->base,"KalturaObject")} {

    get objectType() : string{
        return '{$class->name}';
    }

    {$this->utils->buildExpression($content->properties, NewLine, 1)}

    build():any {
        return Object.assign({},
            super.build(),
            {
                {$this->utils->buildExpression($content->buildContent,  ',' . NewLine, 4)}
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

        return $result;
    }

    function createContentFromClass(ClassType $class)
    {
        $result = new stdClass();
        $result->properties = array();
        $result->buildContent = array();

        $result->buildContent[] = "objectType : \"{$class->name}\"";


        if (count($class->properties) != 0)
        {
            foreach($class->properties as $property) {
                // update the build function
                $result->buildContent[] = $this->requestBuildExp($property->name, $property->type);

                // update the properties declaration
                $ng2ParamType = $this->toNG2TypeExp($property->type, $property->typeClassName);
                $result->properties[] = "
    get {$property->name}() : {$ng2ParamType}
    {
        return <{$ng2ParamType}>this.objectData['{$property->name}'];
    }

    set {$property->name}(value : {$ng2ParamType})
    {
        this.objectData['{$property->name}'] = value;
    }";
            }
        }

        return $result;
    }
}