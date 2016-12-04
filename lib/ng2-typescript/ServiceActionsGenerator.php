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
            //$serviceActionsFile->path = "services/{$formattedServiceName}/{$formattedServiceName}-actions.ts";
            $serviceActionsFile->path = "services/{$formattedServiceName}/index.ts";
            $serviceActionsFile->content = "import {KalturaRequest} from \"../../kaltura-request\";
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

    constructor($content->constructor)
    {
        super('{$serviceName}','{$serviceAction->name}', data);

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
}";

        return $result;
    }


    // TODO [kmc] remove if not needed
//,{$this->mapToKalturaResponseType($serviceAction->resultType, $serviceAction->resultClassName)}
//    function mapToKalturaResponseType($type, $typeClassName)
//    {
//        $result = null;
//
//        switch($type)
//        {
//            case KalturaServerTypes::File:
//                $result = "NativeResponseTypes.String";
//                break;
//            case KalturaServerTypes::Void:
//                $result = "NativeResponseTypes.Void";
//                break;
//            case KalturaServerTypes::Simple:
//                $result = "NativeResponseTypes." . Utils::upperCaseFirstLetter($this->toNG2TypeExp(KalturaServerTypes::Simple,$typeClassName));
//                break;
//            case KalturaServerTypes::EnumAsString:
//            case KalturaServerTypes::EnumAsInt:
//            case KalturaServerTypes::Date:
//            case KalturaServerTypes::ArrayObject:
//            case KalturaServerTypes::Object:
//                $result = "\"$typeClassName\"";
//                break;
//            default:
//                throw new Exception("Unknown type  {$type} > {$typeClassName} to map to Kaltura response type");
//        }
//
//        return $result;
//    }

    function createContentFromServiceAction(ServiceAction $serviceAction)
    {
        $result = new stdClass();
        $result->properties = array();
        $result->constructor = null;
        $result->constructorContent = array();
        $result->importEnums = array();
        $result->importTypes = array();

        $constructorParameters = array();

        // update import statements with the result type
        switch($serviceAction->resultType)
        {
            case KalturaServerTypes::Object:
            case KalturaServerTypes::ArrayObject:
                $result->importTypes[] = $serviceAction->resultClassName;
                break;
            case KalturaServerTypes::EnumOfString:
            case KalturaServerTypes::EnumOfInt:
                $result->importEnum[] = $serviceAction->resultClassName;
                break;
            default:
                break;
        }

        foreach($serviceAction->params as $param) {
            // update the build function

            // update the import statements
            switch($param->type)
            {
                case KalturaServerTypes::ArrayObject:
                case KalturaServerTypes::Object:
                    $result->importTypes[] = $param->typeClassName;
                    break;
                case KalturaServerTypes::EnumOfInt:
                case KalturaServerTypes::EnumOfString:
                    $result->importEnums[] = $param->typeClassName;
                    break;

            }

            // TODO [kmc]
            $decorator = null;
//            switch($param->type)
//            {
//                case KalturaServerTypes::ArrayObject:
//                    $decorator = "@JsonMember({elements : {$param->typeClassName}})";
//                    break;
//                default:
//                    $decorator = "@JsonMember";
//                    break;
//            }

            if (!$this->isPropertyOfBaseRequest($param->name)) {
                // handle only properties that are not in base to prevent duplication in declaration
                $ng2ParamType = $this->toNG2TypeExp($param->type, $param->typeClassName);
                $result->properties[] = "{$decorator} {$param->name} : {$ng2ParamType};";
            }
        }

        $requiredParams = $serviceAction->getRequiredParams();
        $actionProperties = array();
        foreach($requiredParams as $param)
        {
            $actionProperties[] = $param->name;

            $ng2ParamType = $this->toNG2TypeExp($param->type, $param->typeClassName);
            // update the constructor function
            $constructorParameters[] = "{$param->name} : {$ng2ParamType}";

            if (!$this->isPropertyOfBaseRequest($param->name)) {
                // handle only properties that are not in base to prevent handling in both places
                $result->constructorContent[] = "this.{$param->name} = data.{$param->name};";
            }
        }

        $optionalParams = $serviceAction->getOptionalParams();
        if (count($optionalParams) != 0)
        {
            foreach($optionalParams as $param)
            {
                $actionProperties[] = $param->name;
                $optionalParamsHandled[] = $param->name;
                $ng2ParamType = $this->toNG2TypeExp($param->type,$param->typeClassName);
                $constructorParameters[] = "{$param->name}? : {$ng2ParamType}";

                if (!$this->isPropertyOfBaseRequest($param->name)) {
                    // handle only properties that are not in base to prevent handling in both places
                    $result->constructorContent[] = "this.{$param->name} =  typeof data.{$param->name} !== 'undefined' ?  data.{$param->name} :  {$this->toNG2DefaultByType($param->type, $param->typeClassName, $param->default)};";
                }
            }
        }

        foreach ($this->serverMetadata->requestSharedParameters as $param) {
            if (!in_array($param->name, $actionProperties)) {
                $optionalParamsHandled[] = $param->name;
                $ng2ParamType = $this->toNG2TypeExp($param->type, $param->typeClassName);
                $constructorParameters[] = "{$param->name}? : {$ng2ParamType}";
            }
        }

        $result->constructor = "data : {" . join(", ", $constructorParameters) . "}";
        if (count($requiredParams) == "0" && count($optionalParamsHandled) !== 0)
        {
            // mark the data parameter as optional if we have optional parameters without any required parameters
            $result->constructor  .= " = {}";
        }

        return $result;
    }

    private function isPropertyOfBaseRequest($propertyName)
    {
        return Utils::findInArrayByName($propertyName,$this->serverMetadata->requestSharedParameters);

    }
    protected function toNG2TypeExp($type, $typeClassName, $resultCreatedCallback = null)
    {
        return parent::toNG2TypeExp($type,$typeClassName,function($type,$typeClassName,$result)
        {
            switch($type) {
                case KalturaServerTypes::ArrayObject:
                    $result = "kclasses.{$result}";
                    break;
                case KalturaServerTypes::EnumOfInt:
                case KalturaServerTypes::EnumOfString:
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