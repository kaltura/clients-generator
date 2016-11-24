<?php

require_once (__DIR__. '/GeneratorBase.php');
require_once (__DIR__. '/GeneratedFileData.php');

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

        $constuctorContent = "{$this->buildExpression($paramsContent->constructorContentRequired, NewLine, 2 )}";
        if (count($paramsContent->constructorContentOptional) != 0) {
            $constuctorContent .= "

        if (additional)
        {
            {$this->buildExpression($paramsContent->constructorContentOptional, NewLine, 3)}
        }";
        }

        $fileContent = "
{$this->getBanner()}

import {KalturaRequest} from \"../../kaltura-request\";
import {KalturaBaseEntryListResponse} from \"../../types/index\";
import {KalturaBaseEntryFilter} from \"../../types/index\";
import {KalturaFilterPager} from \"../../types/index\";
import {KalturaResponse} from \"../../kaltura-response\";

{$this->createDocumentationExp('',$desc)}
export class {$actionClassName} extends KalturaRequest<{$serviceAction->resultType}>{

    {$this->buildExpression($paramsContent->properties, NewLine, 1)}

    constructor({$this->buildExpression($paramsContent->constructor, ', ')})
    {
        super('{$serviceName}','{$serviceAction->name}','{$serviceAction->resultType}');

        {$constuctorContent}
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
{$this->buildExpression($paramsContent->buildContent,  ', ')}
            });
    };
}";

        $fileName = $this->toLispCase($actionClassName) . ".ts";
        $result->path = "services/{$this->toLispCase($serviceName)}/{$fileName}";
        $result->content = $fileContent;
        return $result;
    }

    function createParamsContent(ServiceAction $serviceAction)
    {
        $result = new stdClass();
        $result->properties = array();
        $result->constructor = array();
        $result->constructorContentRequired = array();
        $result->constructorContentOptional = array();
        $result->buildContent = array();


        $requiredParams = $serviceAction->getRequiredParams();
        $optionalParams = $serviceAction->getOptionalParams();

        foreach($requiredParams as $param)
        {
            $result->constructor[] = "{$param->name} : {$param->type}";
            $result->constructorContentRequired[] =  "this.{$param->name} = {$param->name};";
            $result->properties[] = "{$param->name} : {$param->type};";
        }

        if (count($optionalParams) != 0)
        {
            $constructorOptionalParams = array();
            $constructorOptionalDefault = array();

            foreach($optionalParams as $param)
            {
                $constructorOptionalParams[] = "{$param->name}? : {$param->type}";
                $constructorOptionalDefault[] = "{$param->name} : {$param->default}";

                $result->properties[] = "{$param->name} : {$param->type};";
                $result->constructorContentOptional[] = "this.{$param->name} = additional.{$param->name};";
            }

            $result->constructor[] = "additional : {" . join(", ", $constructorOptionalParams) . "} = {" . join(", ", $constructorOptionalDefault) . "}";

        }

        return $result;
    }
}