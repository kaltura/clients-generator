<?php

require_once (__DIR__. '/GeneratorBase.php');
require_once (__DIR__. '/GeneratedFileData.php');
require_once (__DIR__. '/Utils.php');


class ServiceActionsGenerator extends GeneratorBase
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

        $actionClassName = $this->upperCaseFirstLetter($serviceName) . $this->upperCaseFirstLetter($serviceAction->name) . "Action";
        $desc = $serviceAction->description;

        $paramsContent = $this->createParamsContent($serviceAction);

        switch($serviceAction->resultTypeCategory)
        {
            case ServerTypeCategories::Object:
            case ServerTypeCategories::ArrayObject:
                $paramsContent->importTypes[] = $serviceAction->resultType;
                break;
            case ServerTypeCategories::Enum:
                $paramsContent->importEnum[] = $serviceAction->resultType;
                break;
            default:
                break;
        }

        $fileContent = "
{$this->getBanner()}
import {KalturaRequest} from \"../../kaltura-request\";
import {NativeResponseTypes} from \"../../utils/native-response-types\";
import {KalturaResponse} from \"../../kaltura-response\";{$this->ifExp(count($paramsContent->importTypes) != 0,"
import {{$this->buildExpression($paramsContent->importTypes, ', ', 2)}} from \"../../types\";","")}{$this->ifExp(count($paramsContent->importEnums) != 0,"
import {{$this->buildExpression($paramsContent->importEnums, ', ', 2)}} from \" ../../types\";","")}

{$this->createDocumentationExp('',$desc)}
export class {$actionClassName} extends KalturaRequest<{$serviceAction->resultType}>{

    {$this->buildExpression($paramsContent->properties, NewLine, 1)}

    constructor({$this->buildExpression($paramsContent->constructor, ', ')})
    {
        super('{$serviceName}','{$serviceAction->name}',{$this->mapToKalturaResponseType($serviceAction->resultType, $serviceAction->resultTypeCategory)});

        {$this->buildExpression($paramsContent->constructorContent, NewLine, 2 )}
    }

    setData(handler : (request :  {$actionClassName}) => void) :  {$actionClassName}
    {
        if (handler)
        {
            handler(this);
        }

        return this;
    }

    setCompletion(callback : (response : KalturaResponse<$serviceAction->resultType>) => void) : {$actionClassName}
    {
        this.callback = callback;
        return this;
    }

    build():any {
        return Object.assign({},
            super.build(),
            {
                {$this->buildExpression($paramsContent->buildContent,  ',' . NewLine,4)}
            });
    };
}";

        $fileName = $this->toLispCase($actionClassName) . ".ts";
        $result->path = "services/{$this->toLispCase($serviceName)}/{$fileName}";
        $result->content = $fileContent;
        return $result;
    }

    function mapToKalturaResponseType($type, $typeCategory)
    {
        $result = null;

        switch($typeCategory)
        {
            case ServerTypeCategories::File:
                $result = "NativeResponseTypes.String";
                break;
            case ServerTypeCategories::Simple:
            case ServerTypeCategories::Void:
                $result = "NativeResponseTypes." . Utils::upperCaseFirstLetter($type);
                break;
            case ServerTypeCategories::Enum:
            case ServerTypeCategories::Date:
            case ServerTypeCategories::ArrayObject:
            case ServerTypeCategories::Object:
                $result = "\"$type\"";
                break;
            default:
                throw new Exception("Unknown type requested {$typeCategory} > {$type} to map to Kaltura response type");
        }

        return $result;
    }

    function createParamsContent(ServiceAction $serviceAction)
    {
        $result = new stdClass();
        $result->properties = array();
        $result->constructor = array();
        $result->constructorContent = array();
        $result->buildContent = array();
        $result->importEnums = array();
        $result->importTypes = array();

        foreach($serviceAction->params as $param) {
            $result->buildContent[] = Utils::requestBuildExp($param->name, $param->type, $param->typeCategory);

            switch($param->typeCategory)
            {
                case ServerTypeCategories::ArrayObject:
                case ServerTypeCategories::Object:
                    $result->importTypes[] = $param->type;
                    break;
                case ServerTypeCategories::Enum:
                    $result->importEnums[] = $param->type;
                    break;

            }
        }

        $requiredParams = $serviceAction->getRequiredParams();
        $optionalParams = $serviceAction->getOptionalParams();

        foreach($requiredParams as $param)
        {
            $ng2ParamType = $this::toNG2TypeExp($param->type);
            $result->constructor[] = "{$param->name} : {$ng2ParamType}";
            $result->constructorContent[] =  "this.{$param->name} = {$param->name};";
            $result->properties[] = "{$param->name} : {$ng2ParamType}{$this->ifExp($param->default, ' = ' . $param->default,"")};";
        }

        if (count($optionalParams) != 0)
        {
            $constructorOptionalParams = array();

            foreach($optionalParams as $param)
            {
                $ng2ParamType = $this::toNG2TypeExp($param->type);

                $constructorOptionalParams[] = "{$param->name}? : {$ng2ParamType}";

                $result->properties[] = "{$param->name} : {$ng2ParamType};";
                $result->constructorContent[] = "this.{$param->name} =  typeof additional.{$param->name} !== 'undefined' ?  additional.{$param->name} :  $param->default;";
            }

            $result->constructor[] = "additional? : {" . join(", ", $constructorOptionalParams) . "} = {}";

        }

        return $result;
    }
}