<?php

require_once (__DIR__. '/NG2TypescriptGeneratorBase.php');
require_once (__DIR__. '/GeneratedFileData.php');
require_once (__DIR__. '/Utils.php');


class ServiceActionsGenerator extends NG2TypescriptGeneratorBase
{

    function __construct($serverMetadata)
    {
        parent::__construct($serverMetadata);
    }

    public function generate()
    {
        $result = array_merge(
            $this->createServiceActions()
        );

        return $result;
    }

    function createServiceActions()
    {
        $result = array();

        foreach ($this->serverMetadata->services as $service) {
            foreach($service->actions as $serviceAction) {
                $result[] = $this->createServiceAction($serviceAction, $service->name);
            }
        }

        return $result;
    }

    function createServiceAction(ServiceAction $serviceAction,$serviceName)
    {
        $result = new GeneratedFileData();

        $actionClassName = Utils::upperCaseFirstLetter($serviceName) . Utils::upperCaseFirstLetter($serviceAction->name) . "Action";
        $desc = $serviceAction->description;

        $content = $this->createContentFromServiceAction($serviceAction);

        $fileContent = "
{$this->getBanner()}
import {KalturaRequest} from \"../../kaltura-request\";
import {NativeResponseTypes} from \"../../utils/native-response-types\";
import {KalturaResponse} from \"../../kaltura-response\";{$this->utils->ifExp(count($content->importTypes) != 0,"
import {{$this->utils->buildExpression($content->importTypes, ', ', 2)}} from \"../../kaltura-types\";","")}{$this->utils->ifExp(count($content->importEnums) != 0,"
import {{$this->utils->buildExpression($content->importEnums, ', ', 2)}} from \" ../../kaltura-enums\";","")}

{$this->utils->createDocumentationExp('',$desc)}
export class {$actionClassName} extends KalturaRequest<{$serviceAction->resultClassName}>{

    {$this->utils->buildExpression($content->properties, NewLine, 1)}

    constructor({$this->utils->buildExpression($content->constructor, ', ')})
    {
        super('{$serviceName}','{$serviceAction->name}',{$this->mapToKalturaResponseType($serviceAction->resultType, $serviceAction->resultClassName)});

        {$this->utils->buildExpression($content->constructorContent, NewLine, 2 )}
    }

    setData(handler : (request :  {$actionClassName}) => void) :  {$actionClassName}
    {
        if (handler)
        {
            handler(this);
        }

        return this;
    }

    setCompletion(callback : (response : KalturaResponse<$serviceAction->resultClassName>) => void) : {$actionClassName}
    {
        this.callback = callback;
        return this;
    }

    build():any {
        return Object.assign({},
            super.build(),
            {
                {$this->utils->buildExpression($content->buildContent,  ',' . NewLine,4)}
            });
    };
}";

        $fileName = Utils::toLispCase($actionClassName) . ".ts";
        $result->path = "services/{$this->utils->toLispCase($serviceName)}/{$fileName}";
        $result->content = $fileContent;
        return $result;
    }


    function mapToKalturaResponseType($type, $typeClassName)
    {
        $result = null;

        switch($type)
        {
            case KalturaServerTypes::File:
                $result = "NativeResponseTypes.String";
                break;
            case KalturaServerTypes::Void:
                $result = "NativeResponseTypes.Void";
                break;
            case KalturaServerTypes::Simple:
                $result = "NativeResponseTypes." . Utils::upperCaseFirstLetter($this->toNG2TypeExp(KalturaServerTypes::Simple,$typeClassName));
                break;
            case KalturaServerTypes::Enum:
            case KalturaServerTypes::Date:
            case KalturaServerTypes::ArrayObject:
            case KalturaServerTypes::Object:
                $result = "\"$type\"";
                break;
            default:
                throw new Exception("Unknown type  {$type} > {$typeClassName} to map to Kaltura response type");
        }

        return $result;
    }

    function createContentFromServiceAction(ServiceAction $serviceAction)
    {
        $result = new stdClass();
        $result->properties = array();
        $result->constructor = array();
        $result->constructorContent = array();
        $result->buildContent = array();
        $result->importEnums = array();
        $result->importTypes = array();

        // update import statements with the result type
        switch($serviceAction->resultType)
        {
            case KalturaServerTypes::Object:
            case KalturaServerTypes::ArrayObject:
                $result->importTypes[] = $serviceAction->resultClassName;
                break;
            case KalturaServerTypes::Enum:
                $result->importEnum[] = $serviceAction->resultClassName;
                break;
            default:
                break;
        }

        foreach($serviceAction->params as $param) {
            // update the build function
            $result->buildContent[] = $this->requestBuildExp($param->name, $param->type);

            // update the import statements
            switch($param->type)
            {
                case KalturaServerTypes::ArrayObject:
                case KalturaServerTypes::Object:
                    $result->importTypes[] = $param->typeClassName;
                    break;
                case KalturaServerTypes::Enum:
                    $result->importEnums[] = $param->typeClassName;
                    break;

            }
        }

        $requiredParams = $serviceAction->getRequiredParams();
        foreach($requiredParams as $param)
        {
            // update the constructor function & properties statements to handle REQUIRED params
            $ng2ParamType = $this->toNG2TypeExp($param->type, $param->typeClassName);
            $result->constructor[] = "{$param->name} : {$ng2ParamType}";
            $result->constructorContent[] =  "this.{$param->name} = {$param->name};";
            $result->properties[] = "{$param->name} : {$ng2ParamType}{$this->utils->ifExp($param->default, ' = ' . $param->default,"")};";
        }

        $optionalParams = $serviceAction->getOptionalParams();
        if (count($optionalParams) != 0)
        {
            // update the constructor function & properties statements to handle OPTIONAL params
            $constructorOptionalParams = array();
            foreach($optionalParams as $param)
            {
                $ng2ParamType = $this->toNG2TypeExp($param->type,$param->typeClassName);
                $constructorOptionalParams[] = "{$param->name}? : {$ng2ParamType}";
                $result->properties[] = "{$param->name} : {$ng2ParamType};";
                $result->constructorContent[] = "this.{$param->name} =  typeof additional.{$param->name} !== 'undefined' ?  additional.{$param->name} :  {$this->mapToDefaultValue($param->type, $param->typeClassName, $param->default)};";
            }
            $result->constructor[] = "additional? : {" . join(", ", $constructorOptionalParams) . "} = {}";

        }

        return $result;
    }
}