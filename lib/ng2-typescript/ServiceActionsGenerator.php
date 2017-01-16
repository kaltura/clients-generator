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

        //$result[] = $this->createServicesFolderIndex();

        return $result;
    }

    function createServicesFolderIndex()
    {
        $fileContent = array();

        foreach ($this->serverMetadata->services as $service) {
            $formattedServiceName = $this->utils->toLispCase($service->name);

            $fileContent[] = "export * from \"./{$formattedServiceName}\"";
        }

        $result = new GeneratedFileData();
        $result->path = "services/index.ts";
        $result->content = join(NewLine,$fileContent);

        return $result;
    }

    function createServiceActions()
    {
        $result = array();

        foreach ($this->serverMetadata->services as $service) {

            $actions = array();

            foreach ($service->actions as $serviceAction) {
                $actions[] = $this->createServiceAction($serviceAction, $service);
            }

            $serviceActionsFile = new GeneratedFileData();
            $result[] = $serviceActionsFile;
            $formattedServiceName = $this->utils->toLispCase($service->name);
            $serviceActionsFile->path = "services/{$formattedServiceName}.ts";
            $serviceActionsFile->content = "import {KalturaRequest} from \"../kaltura-request\";
import * as kclasses from \"../kaltura-types\";
import * as kenums from \"../kaltura-enums\";
import {JsonMember,JsonSerializableObject} from \"../utils/typed-json\";

{$this->utils->buildExpression($actions,NewLine)}
";
        }

        return $result;
    }

    function createServiceAction(ServiceAction $serviceAction,$service)
    {
        $actionClassName = Utils::upperCaseFirstLetter($service->name) . Utils::upperCaseFirstLetter($serviceAction->name) . "Action";
        $desc = $serviceAction->description;

        $content = $this->createContentFromServiceAction($serviceAction);
        $actionNG2ResultType = $this->toNG2TypeExp($serviceAction->resultType, $serviceAction->resultClassName);
        $baseNG2ResultType = $this->mapToKalturaResponseType($serviceAction->resultType, $serviceAction->resultClassName);;
        $result = "{$this->getBanner()}

{$this->utils->createDocumentationExp('',$desc)}
@JsonSerializableObject({onSerializedFunction : 'onSerialized', requireTypeHints : false})
export class {$actionClassName} extends KalturaRequest<{$actionNG2ResultType}>{

    {$this->utils->buildExpression($content->properties, NewLine, 1)}

    constructor(data : $content->constructor)
    {
        super('{$service->id}','{$serviceAction->name}',{$baseNG2ResultType}, data);

        {$this->utils->buildExpression($content->constructorContent, NewLine, 2 )}

    }
}";

        return $result;
    }


    function mapToKalturaResponseType($type, $typeClassName)
    {
        $result = null;

        switch($type)
        {
            case KalturaServerTypes::File:
                $result = "\"string\"";
                break;
            case KalturaServerTypes::Void:
                $result = "null";
                break;
            case KalturaServerTypes::Simple:
                $result = "\"" . $this->toNG2TypeExp(KalturaServerTypes::Simple,$typeClassName) . "\"";
                break;
            case KalturaServerTypes::EnumOfString:
            case KalturaServerTypes::EnumOfInt:
                $result = "kenums.$typeClassName";
            break;
            case KalturaServerTypes::ArrayOfObjects:
            case KalturaServerTypes::Object:
                $result = "kclasses.$typeClassName";
            break;
            case KalturaServerTypes::Date:
                $result = "\"date\"";
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
        $result->constructor = null;
        $result->constructorContent = array();
        $result->importEnums = array();
        $result->importTypes = array();

        $constructorParameters = array();

        // update import statements with the result type
        switch($serviceAction->resultType)
        {
            case KalturaServerTypes::Object:
            case KalturaServerTypes::ArrayOfObjects:
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
                case KalturaServerTypes::ArrayOfObjects:
                case KalturaServerTypes::Object:
                    $result->importTypes[] = $param->typeClassName;
                    break;
                case KalturaServerTypes::EnumOfInt:
                case KalturaServerTypes::EnumOfString:
                    $result->importEnums[] = $param->typeClassName;
                    break;

            }

            if (!$this->isPropertyOfBaseRequest($param->name)) {
                // handle only properties that are not in base to prevent duplication in declaration
                $ng2ParamType = $this->toNG2TypeExp($param->type, $param->typeClassName);
                $decorator = $this->getPropertyDecorator($param->type,$param->typeClassName, 'kclasses.');
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
                    $result->constructorContent[] = "this.{$param->name} =  data && typeof data.{$param->name} !== 'undefined' ?  data.{$param->name} :  {$this->toNG2DefaultByType($param->type, $param->typeClassName, $param->default)};";
                }
            }
        }

        foreach ($this->serverMetadata->requestSharedParameters as $param) {
            if (!in_array($param->name, $actionProperties) && !$param->transparentToUser) {
                $optionalParamsHandled[] = $param->name;
                $ng2ParamType = $this->toNG2TypeExp($param->type, $param->typeClassName);
                $constructorParameters[] = "{$param->name}? : {$ng2ParamType}";
            }
        }

        $result->constructor = "{" . join(", ", $constructorParameters) . "}";
        if (count($requiredParams) == "0" && count($optionalParamsHandled) !== 0)
        {
            // mark the data parameter as optional if we have optional parameters without any required parameters
            $result->constructor  .= " = {}";
        }

        return $result;
    }

    private function isPropertyOfBaseRequest($propertyName)
    {
        return isset($this->serverMetadata->requestSharedParameters[$propertyName]);
    }

    protected function toNG2TypeExp($type, $typeClassName, $resultCreatedCallback = null)
    {
        return parent::toNG2TypeExp($type,$typeClassName,function($type,$typeClassName,$result)
        {
            switch($type) {
                case KalturaServerTypes::Object:
                case KalturaServerTypes::ArrayOfObjects:
                    $result = "kclasses.{$result}";
                    break;
                case KalturaServerTypes::EnumOfInt:
                case KalturaServerTypes::EnumOfString:
                    $result = 'kenums.' . $result;
                    break;
                default:
                    break;
            }

            return $result;
        });
    }
}