<?php

require_once (__DIR__. '/TypescriptGeneratorBase.php');
require_once (__DIR__. '/GeneratedFileData.php');

class ClassesGenerator extends TypescriptGeneratorBase
{

    function __construct($serverMetadata)
    {
        parent::__construct($serverMetadata);
    }

    public function generate()
    {
        foreach ($this->serverMetadata->services as $service) {
            foreach ($service->actions as $serviceAction) {
                $result[] = $this->createServiceActionFile($service, $serviceAction);
            }
        }

        foreach ($this->serverMetadata->classTypes as $class) {
            $result[] = $this->createClassFile($class);
        }

        $result[] = $this->createRequestBaseFile($this->serverMetadata->apiVersion);

        return $result;
    }



    function createRequestBaseFile($apiVersion)
    {
        $createClassArgs = new stdClass();
        $createClassArgs->name = "KalturaRequestBase";
        $createClassArgs->description = "";
        $createClassArgs->base = "KalturaObjectBase";
        $createClassArgs->basePath = "./";
        $createClassArgs->enumPath = "./types/";
        $createClassArgs->typesPath = "./types/";
        $createClassArgs->importedItems = array();
        $createClassArgs->customMetadataProperties = array();

        // create a property named 'acceptedTypes' which holds relevant 'KalturaObjectBase' to that request.
        // note that this property is marked as local property
        $acceptedTypes = new stdClass();
        $acceptedTypes->name = "acceptedTypes";
        $acceptedTypes->localProperty = true;
        $acceptedTypes->customDeclaration = "{new(...args) : KalturaObjectBase}[]";
        $acceptedTypes->optional = true;
        $acceptedTypes->type = KalturaServerTypes::ArrayOfObjects;
        $acceptedTypes->typeClassName = "KalturaObjectBase";
        $customProperties = array();
        $customProperties[] = $acceptedTypes;

        $createClassArgs->properties = array_merge(
            $customProperties,
            $this->serverMetadata->requestSharedParameters
        );
        $createClassArgs->requireDataInCtor = true;


        $customMetadataProperties = array();
        $customMetadataProperties[] = $this->createMetadataProperty('apiVersion',false,KalturaServerTypes::Simple,'constant', $apiVersion);
        $createClassArgs->customMetadataProperties = $customMetadataProperties;


        $classBody = $this->createClassExp($createClassArgs);

        $fileContent = "{$this->getBanner()}
import { KalturaObjectMetadata } from './kaltura-object-base';
{$classBody}
";

        $file = new GeneratedFileData();
        $file->path = "kaltura-request-base.ts";
        $file->content = $fileContent;
        $result[] = $file;
        return $file;
    }

    function createClassFile(ClassType $class)
    {
        $createClassArgs = new stdClass();
        $createClassArgs->name = $class->name;
        $createClassArgs->description = $class->description;
        $createClassArgs->base = $class->base ? $class->base : 'KalturaObjectBase';
        $createClassArgs->baseIsGenerated = $class->base ? true : false;

        $createClassArgs->basePath = $class->base ? "./" : "../";
        $createClassArgs->enumPath = "./";
        $createClassArgs->typesPath = "./";
        $createClassArgs->properties = $class->properties;
        $createClassArgs->importedItems = array();
        $createClassArgs->customMetadataProperties[] = $this->createMetadataProperty('objectType',false,KalturaServerTypes::Simple,'constant', $class->name);
        $createClassArgs->requireDataInCtor = false;

        $classBody = $this->createClassExp($createClassArgs);

        $classFunctionName = ucfirst($class->name);
        $fileContent = "{$this->getBanner()}
import { KalturaObjectMetadata } from '../kaltura-object-base';
import { KalturaTypesFactory } from '../kaltura-types-factory';
{$classBody}
KalturaTypesFactory.registerType('$class->name',$classFunctionName);
";

        $file = new GeneratedFileData();
        $fileName = $class->name; //$this->utils->toLispCase($class->name);
        $file->path = "types/{$fileName}.ts";
        $file->content = $fileContent;
        $result[] = $file;
        return $file;
    }

    function createServiceActionFile(Service $service,ServiceAction $serviceAction)
    {
        $className = ucfirst($service->name) . ucfirst($serviceAction->name) . "Action";
        if ($serviceAction->resultType === KalturaServerTypes::File) {
            $actionNG2ResultType = "string";
        }else {
            $actionNG2ResultType = $this->toNG2TypeExp($serviceAction->resultType, $serviceAction->resultClassName);
        }

        $importedItems = array($className,'KalturaRequest');

        $getImportExpForTypeArgs = new stdClass();
        $getImportExpForTypeArgs->enumPath = "./";
        $getImportExpForTypeArgs->typesPath = "./";
        $getImportExpForTypeArgs->type = $serviceAction->resultType;
        $getImportExpForTypeArgs->typeClassName = $serviceAction->resultClassName;
        $importResultType = $this->getImportExpForType($getImportExpForTypeArgs,$importedItems);
        if ($importResultType) {
            // prevent duplicate import for the result class
            $importedItems[] = $serviceAction->resultClassName;
        }

        $createClassArgs = new stdClass();
        $createClassArgs->name = $className;
        $createClassArgs->basePath = "../";
        $createClassArgs->enumPath = "./";
        $createClassArgs->typesPath = "./";
        $createClassArgs->properties = $serviceAction->params;
        $createClassArgs->importedItems = &$importedItems;
        $createClassArgs->customMetadataProperties[] = $this->createMetadataProperty('service',false,KalturaServerTypes::Simple,'constant', $service->id);
        $createClassArgs->customMetadataProperties[] = $this->createMetadataProperty('action',false,KalturaServerTypes::Simple,'constant', $serviceAction->name);
        $base = $this->hasFileProperty($serviceAction->params) ? "KalturaUploadRequest" : "KalturaRequest";
        $createClassArgs->base = "{$base}<{$actionNG2ResultType}>";


        $createClassArgs->documentation = "/**
 * Build request payload for service '{$service->name}' action '{$serviceAction->name}'.
 * Used for {$serviceAction->description}.
 *
 * Server response type {$actionNG2ResultType}
 *
 * @class
 * @extends {$base}
 */";

        $resultType = $this->toApplicationType($serviceAction->resultType, $serviceAction->resultClassName);

        if (isset($resultType->subType)) {
            $createClassArgs->superArgs = "{responseType : '{$resultType->type}', responseSubType : '{$resultType->subType}', responseConstructor : {$resultType->subType}  }";
        }else
        {
            $createClassArgs->superArgs = "{responseType : '{$resultType->type}', responseSubType : '{$resultType->subType}', responseConstructor : null }";
        }
        $createClassArgs->requireDataInCtor = $this->hasRequiredProperty($serviceAction->params);

        $classBody = $this->createClassExp($createClassArgs);


        $fileContent = "{$this->getBanner()}
import { KalturaObjectMetadata } from '../kaltura-object-base';
{$importResultType}

{$classBody}
";

        $file = new GeneratedFileData();
        $fileName = $className; //$this->utils->toLispCase($className);
        $file->path = "types/{$fileName}.ts";
        $file->content = $fileContent;
        $result[] = $file;
        return $file;
    }

    function createClassExp($args)
    {
        $name = $args->name;
        $classDocumentation = isset($args->documentation) ? $args->documentation : '' ;
        $base = isset($args->base) ? $args->base : null;
        $baseStrippedClassName = isset($base) ? preg_replace('/<.+>/i','',$base) : null;
        $baseIsGenerated = isset($args->baseIsGenerated) ? $args->baseIsGenerated : false;
        $basePath = isset($args->basePath) ? $args->basePath : null;
        $customMetadataProperties = isset($args->customMetadataProperties) ? $args->customMetadataProperties : array();
        $classTypeName = Utils::upperCaseFirstLetter($name);
        $superArgs = isset($args->superArgs) ? $args->superArgs : '';
        $requireDataInCtor = $args->requireDataInCtor;

        $importedItems = &$args->importedItems;
        $importedItems[] = $name;

        // enrich super args
        if ($superArgs === '')
        {
            $superArgs = 'data';
        }else
        {
            $superArgs =  'data, ' . $superArgs;
        }

        $baseImport = null;
        if ($baseStrippedClassName && $basePath)
        {
            $importedItems[] = $baseStrippedClassName;
            if ($baseIsGenerated)
            {
                $importFilePath = $basePath . $baseStrippedClassName; //utils::toLispCase($strippedBase);
            }else
            {
                $importFilePath = $basePath . utils::toLispCase($baseStrippedClassName);
            }
            $baseImport = "import { {$baseStrippedClassName}, {$baseStrippedClassName}Args } from '{$importFilePath}';";
        }

        $aggregatedData = $this->aggregateClassData($args);

        if ($baseImport) {
            $aggregatedData->imports[] = $baseImport;
        }

        $classMetadataProperties = array_merge(
            $customMetadataProperties,
            $aggregatedData->propertiesMetadata
        );

        $result = "{$this->utils->buildExpression($aggregatedData->imports,NewLine)}

export interface {$classTypeName}Args {$this->utils->ifExp($baseStrippedClassName, " extends " . $baseStrippedClassName . "Args","")} {
    {$this->utils->buildExpression($aggregatedData->constructorArgs, NewLine, 1)}
}

{$classDocumentation}
export class {$classTypeName} extends {$this->utils->ifExp($base, $base,'')} {

    {$this->utils->buildExpression($aggregatedData->classProperties, NewLine, 1)}

    constructor(data{$this->utils->ifExp($requireDataInCtor,"","?")} : {$classTypeName}Args)
    {
        super({$superArgs});";

        if (count($aggregatedData->assignPropertiesDefault)) {
            $result .= "
        {$this->utils->buildExpression($aggregatedData->assignPropertiesDefault, NewLine, 2)}";
        }

    $result .= "
    }

    protected _getMetadata() : KalturaObjectMetadata
    {
        const result = super._getMetadata();
        Object.assign(
            result.properties,
            {
                {$this->utils->buildExpression($classMetadataProperties,',' . NewLine,4)}
            }
        );
        return result;
    }
}
";

        return $result;
    }


    function aggregateClassData($args)
    {
        $typesPath = $args->typesPath;
        $enumPath = $args->enumPath;
        $properties = $args->properties;
        $importedItems = &$args->importedItems;

        $result = new stdClass();
        $result->constructorArgs = array();
        $result->classProperties = array();
        $result->propertiesMetadata = array();
        $result->imports = array();
        $result->buildContent = array();
        $result->constructorContent = array();
        $result->assignPropertiesDefault = array();

        if (count($properties) != 0)
        {
            foreach($properties as $property) {

                if (isset($property->customDeclaration))
                {
                    $ng2ParamType = $property->customDeclaration;
                } else if ($property->type === KalturaServerTypes::File) {
                    $ng2ParamType = "File";
                }else {
                    $ng2ParamType = $this->toNG2TypeExp($property->type, $property->typeClassName);
                }

                $default = $this->toNG2DefaultByType($property->type, $property->typeClassName, isset($property->default) ? $property->default : null);
                $readOnly = isset($property->readOnly) && $property->readOnly;
                $localProperty = isset($property->localProperty) && $property->localProperty;

                // update the properties declaration
                if (!$readOnly) {
                    $isOptional = isset($property->optional) && $property->optional;
                    $result->constructorArgs[] = "{$property->name}{$this->utils->ifExp($isOptional,"?","")} : {$ng2ParamType};";
                }

                $result->classProperties[] = ($readOnly ? "readonly " : "") . "{$property->name} : {$ng2ParamType};";

                if ($default)
                {
                    $result->assignPropertiesDefault[] = "if (typeof this.{$property->name} === 'undefined') this.{$property->name} = {$default};";
                }

                if (!$localProperty) {
                    $result->propertiesMetadata[] = $this->createMetadataProperty($property->name, isset($property->readOnly) ? $property->readOnly : false, $property->type, $property->typeClassName);
                }

                $getImportExpForTypeArgs = new stdClass();
                $getImportExpForTypeArgs->enumPath = $enumPath;
                $getImportExpForTypeArgs->typesPath = $typesPath;
                $getImportExpForTypeArgs->type = $property->type;
                $getImportExpForTypeArgs->typeClassName = $property->typeClassName;
                $propertyImport = $this->getImportExpForType($getImportExpForTypeArgs, $importedItems);
                if ($propertyImport)
                {
                    $result->imports[] = $propertyImport;
                }
            }
        }

        return $result;
    }

    private function createMetadataProperty($name, $readOnly, $type, $typeClassName, $defaultValue = null)
    {
        $readOnlyExp = (isset($readOnly) && $readOnly) ? ', readOnly : true' : '';
        $defaultValueExp = isset($defaultValue) ? ", default : '{$defaultValue}'" : '';
        $propertyMetadataType = $this->toApplicationType($type, $typeClassName);
        $subType = isset($propertyMetadataType->subType) ? ", subTypeConstructor : {$propertyMetadataType->subType}, subType : '{$propertyMetadataType->subType}'" : '';
        return "{$name} : { type : '{$propertyMetadataType->type}'{$defaultValueExp}{$readOnlyExp}{$subType} }";
    }

    private function getImportExpForType($args, &$importedItems)
    {
        $type = $args->type;
        $typeClassName = $args->typeClassName;
        $typesPath = $args->typesPath;
        $enumPath = $args->enumPath;

        $result = null;
        switch ($type) {
            case KalturaServerTypes::EnumOfInt:
            case KalturaServerTypes::EnumOfString:
            if (in_array($typeClassName,$importedItems) === false) {
                $importedItems[] = $typeClassName;
                $fileName = $typeClassName; //$this->utils->toLispCase($typeClassName);
                $result = "import { {$typeClassName} } from '{$enumPath}{$fileName}';";
            }
            break;
            case KalturaServerTypes::Object:
            case KalturaServerTypes::ArrayOfObjects:
            case KalturaServerTypes::MapOfObjects:
                if (!in_array($typeClassName,$importedItems)) {
                    $importedItems[] = $typeClassName;

                    if ($typeClassName === 'KalturaObjectBase')
                    {
                        $typesPath = "../";
                        $fileName = $this->utils->toLispCase($typeClassName);
                    }else
                    {
                        $fileName = $typeClassName; //$this->utils->toLispCase($typeClassName);

                    }
                    $result = "import { {$typeClassName} } from '{$typesPath}{$fileName}';";
                }
                break;
            default:
                break;
        }

        return $result;
    }

    protected function toApplicationType($type, $typeClassName)
    {
        $result = new stdClass();
        $result->type = null;
        $result->subType = null;

        switch ($type) {
            case KalturaServerTypes::File:
                $result->type = 'f';
                break;
            case KalturaServerTypes::Simple:
                switch ($typeClassName) {
                    case "constant":
                        $result->type = "c";
                        break;
                    case "bool":
                        $result->type = "b";
                        break;
                    case "bigint":
                    case "float":
                    case "int":
                        $result->type = 'n';
                        break;
                    case "string":
                        $result->type = 's';
                        break;
                    default:
                        throw new Exception("toApplicationType: Unknown simple type {$typeClassName}");
                }
                break;
            case KalturaServerTypes::ArrayOfObjects:
                $result->type = "a";
                $result->subType = $typeClassName;
                break;
            case KalturaServerTypes::MapOfObjects:
                $result->type = "m";
                $result->subType = $typeClassName;
                break;
            case KalturaServerTypes::EnumOfInt:
                $result->type = "en";
                $result->subType = $typeClassName;
                break;
            case KalturaServerTypes::EnumOfString:
                $result->type = "es";
                $result->subType = $typeClassName;
                break;
            case KalturaServerTypes::Object:
                $result->type = "o";
                $result->subType = $typeClassName;
                break;
            case KalturaServerTypes::Date:
                $result->type = "d";
                break;
            case KalturaServerTypes::Void:
                $result->type = "v";
                break;
            default:
                throw new Exception("toApplicationType: Unknown type requested {$type}");
        }

        return $result;
    }

    private function hasFileProperty($array)
    {
        $searchedValue = KalturaServerTypes::File;
        $neededObject = array_filter(
            $array,
            function ($e) use (&$searchedValue) {
                return $e->type === $searchedValue;
            }
        );

        return $neededObject !== null && !empty($neededObject);
    }

    private function hasRequiredProperty($array)
    {
        $neededObject = array_filter(
            $array,
            function ($e)  {
                return !$e->optional;
            }
        );

        return $neededObject !== null && !empty($neededObject);
    }

    protected function toNG2TypeExp($type, $typeClassName, $resultCreatedCallback = null)
    {
        return parent::toNG2TypeExp($type,$typeClassName,function($type,$typeClassName,$result)
        {
            return $result;
        });
    }
}


