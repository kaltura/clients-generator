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

            $actions = array();

            foreach ($service->actions as $serviceAction) {
                $actions[] = $this->createServiceAction($serviceAction, $service->name);
            }

            $serviceActionsFile = new GeneratedFileData();
            $result[] = $serviceActionsFile;
            $formattedServiceName = $this->utils->toLispCase($service->name);
            $serviceActionsFile->path = "services/{$formattedServiceName}/{$formattedServiceName}-actions.ts";
            $serviceActionsFile->content = "import {KalturaRequest} from \"../../kaltura-request\";
import {NativeResponseTypes} from \"../../utils/native-response-types\";
import {KalturaResponse} from \"../../kaltura-response\";
import {VoidResponseResult} from \"../../utils/void-response-result\";
import * as kclasses from \"../../kaltura-types\";
import * as kenums from \"../../kaltura-enums\";
import {DependentProperty, DependentPropertyTarget, KalturaPropertyTypes} from \"../../utils/kaltura-server-object\";

{$this->utils->buildExpression($actions,NewLine)}
";
        }

        return $result;
    }

    function createServiceAction(ServiceAction $serviceAction,$serviceName)
    {
        $actionClassName = Utils::upperCaseFirstLetter($serviceName) . Utils::upperCaseFirstLetter($serviceAction->name) . "Action";
        $desc = $serviceAction->description;

        $content = $this->createContentFromServiceAction($serviceAction);
        $actionNG2ResultType = $this->toNG2TypeExp($serviceAction->resultType, $serviceAction->resultClassName);
        $result = "{$this->getBanner()}

{$this->utils->createDocumentationExp('',$desc)}
export class {$actionClassName} extends KalturaRequest<{$actionNG2ResultType}>{

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

    setDependency(...dependency : DependentProperty[]) : {$actionClassName}
    {
        super.setDependency(...dependency);
        return this;
    }

    setCompletion(callback : (response : KalturaResponse<{$actionNG2ResultType}>) => void) : {$actionClassName}
    {
        this.callback = callback;
        return this;
    }

    build():any {
        return Object.assign(
            super.build(),
            {
                {$this->utils->buildExpression($content->buildContent,  ',' . NewLine,4)}
            });
    };
}";

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
                $result = "\"$typeClassName\"";
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
            $result->buildContent[] = $this->requestBuildExp($param->name, $param->type,true);

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

            $ng2ParamType = $this->toNG2TypeExp($param->type, $param->typeClassName);
            $result->properties[] = "{$param->name} : {$ng2ParamType};";
        }

        $requiredParams = $serviceAction->getRequiredParams();
        foreach($requiredParams as $param)
        {
            // update the constructor function & properties statements to handle REQUIRED params
            $ng2ParamType = $this->toNG2TypeExp($param->type, $param->typeClassName);
            $result->constructor[] = "{$param->name} : {$ng2ParamType}";
            $result->constructorContent[] =  "this.{$param->name} = {$param->name};";

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
                $result->constructorContent[] = "this.{$param->name} =  typeof additional.{$param->name} !== 'undefined' ?  additional.{$param->name} :  {$this->toNG2DefaultByType($param->type, $param->typeClassName, $param->default)};";
            }
            $result->constructor[] = "additional : {" . join(", ", $constructorOptionalParams) . "} = {}";

        }

        return $result;
    }

    protected function toNG2TypeExp($type, $typeClassName, $resultCreatedCallback = null)
    {
        return parent::toNG2TypeExp($type,$typeClassName,function($type,$typeClassName,$result)
        {
            switch($type) {
                case KalturaServerTypes::ArrayObject:
                    $result = "kclasses.{$result}";
                    break;
                case KalturaServerTypes::Enum:
                    $result = 'kenums.' . $result;
                    break;
                case KalturaServerTypes::Object:
                    $result = 'kclasses.' . $result;
                    break;
            }

            return $result;
        });
    }
}