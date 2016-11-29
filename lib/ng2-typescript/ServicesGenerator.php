<?php

require_once (__DIR__. '/NG2TypescriptGeneratorBase.php');
require_once (__DIR__. '/GeneratedFileData.php');

class ServicesGenerator extends NG2TypescriptGeneratorBase
{

    function __construct($serverMetadata)
    {
        parent::__construct($serverMetadata);
    }

    public function generate()
    {
        $result = array_merge(
            $this->createServices()
        );

        return $result;
    }


    function createServices()
    {
        $result = array();

        foreach ($this->serverMetadata->services as $service) {
            $result[] = $this->createService($service);
            $result[] = $this->createServiceIndex($service);
        }

        return $result;
    }

    function createServiceIndex($serviceData)
    {
        $formattedServiceName = $this->utils->toLispCase($serviceData->name);

        $result = new GeneratedFileData();
        $result->path = "services/{$formattedServiceName}/index.ts";
        $result->content = "
export * from \"./{$formattedServiceName}.service\"
export * from \"./{$formattedServiceName}-actions\"
";

        return $result;

    }

    function createService(Service $serviceData)
    {
        $serviceName = Utils::upperCaseFirstLetter($serviceData->name);
        $actionsFilePath = "./{$this->utils->toLispCase($serviceName)}-actions";
        $desc = $serviceData->description;

        $serviceActionsContent = $this->createServiceActionsContent($serviceData);

        $serviceContent = "{$this->getBanner()}
import { Injectable } from '@angular/core';
import * as kactions from \"{$actionsFilePath}\";
import {VoidResponseResult} from \"../../utils/void-response-result\";
import * as kclasses from \"../../kaltura-types\";
import * as kenums from \"../../kaltura-enums\";

{$this->utils->createDocumentationExp('',$desc)}
export class {$serviceName}Service {

    constructor(){
        throw new Error('This class should not be initialized (you should use its static functions to create new requests)');
    }

	{$this->utils->buildExpression($serviceActionsContent,NewLine . NewLine,1)}
}";

        $formattedServiceName = $this->utils->toLispCase($serviceName);

        $result = new GeneratedFileData();
        $result->path = "services/{$formattedServiceName}/{$formattedServiceName}.service.ts";
        $result->content = $serviceContent;

        return $result;
    }

    function createServiceActionsContent($serviceData)
    {
        $result = array();

        foreach ($serviceData->actions as $actionData) {

            if (!($actionData instanceof ServiceAction)) {
                continue;
            }

            $requiredParams = array();
            $optionalParams = array();
            $actionConstuctorParams = array();

            foreach ($actionData->params as $actionParam) {
                if (!($actionParam instanceof ServiceActionParam)) {
                    continue;
                }

                $ng2ParamType = $this->toNG2TypeExp($actionParam->type, $actionParam->typeClassName);

                if ($actionParam->optional == true) {
                    $optionalParams[] = "{$actionParam->name}? : {$ng2ParamType}";
                } else {
                    $requiredParams[] = "{$actionParam->name} : {$ng2ParamType}";
                    $actionConstuctorParams[] = "{$actionParam->name}";
                }

            }

            $functionParams = array_merge($requiredParams);

            if (count($optionalParams)) {
                $functionParams[] = "additional? : { " . join(', ', $optionalParams) . " }";
                $actionConstuctorParams[] = 'additional';
            }

            $functionParamsExp = join(', ', $functionParams);
            $actionConstuctorParamsExp = join(', ', $actionConstuctorParams);
            $actionResultType = ucwords($serviceData->name) . ucwords($actionData->name) . "Action";
            $functionName = lcfirst($actionData->name);

            $result[] = "{$this->utils->createDocumentationExp('	', $actionData->description)}
static {$functionName}({$functionParamsExp}) : kactions.{$actionResultType}
{
    return new kactions.{$actionResultType}({$actionConstuctorParamsExp});
}";
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