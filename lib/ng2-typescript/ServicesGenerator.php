<?php

require_once (__DIR__. '/GeneratorBase.php');
require_once (__DIR__. '/GeneratedFileData.php');

class ServicesGenerator extends GeneratorBase
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
        }

        return $result;
    }

    function createService(Service $serviceData)
    {
        $result = new GeneratedFileData();

        $serviceName = $this->upperCaseFirstLetter($serviceData->name);
        $actionsFilePath = "./{$this->toLispCase($serviceName)}-actions.ts";
        $desc = $serviceData->description;

        $serviceActionsContent = $this->createServiceActionsContent($serviceData);
        $serviceActionsContentExp = join(NewLine, $serviceActionsContent);

        $serviceContent = "
{$this->getBanner()}

import { Injectable } from '@angular/core';
import * as actions from \"{$actionsFilePath}\";


{$this->createDocumentationExp('',$desc)}
export class {$serviceName}Service {

    constructor(){
        throw new Error('This class should not be initialized (you should use its static functions to create new requests)');
    }

	{$serviceActionsContentExp}
}";

        $formattedServiceName = $this->toLispCase($serviceName);

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

                if ($actionParam->optional == true) {
                    $optionalParams[] = "{$actionParam->name}? : {$actionParam->type}";
                } else {
                    $requiredParams[] = "{$actionParam->name} : {$actionParam->type}";
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
            $actionResultType = "{$serviceData->name}" . ucwords($actionData->name) . "Builder";
            $functionName = lcfirst($actionData->name);

            $result[] = "
	{$this->createDocumentationExp('	', $actionData->description)}
	static {$functionName}({$functionParamsExp}) : {$actionResultType}
	{
		return new actions.{$actionResultType}({$actionConstuctorParamsExp});
	}";
        }

        return $result;
    }
}