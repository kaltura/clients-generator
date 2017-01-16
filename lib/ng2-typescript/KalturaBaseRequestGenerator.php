<?php

require_once (__DIR__. '/NG2TypescriptGeneratorBase.php');
require_once (__DIR__. '/GeneratedFileData.php');
require_once (__DIR__. '/Utils.php');


class KalturaBaseRequestGenerator extends NG2TypescriptGeneratorBase
{

    function __construct($serverMetadata)
    {
        parent::__construct($serverMetadata);
    }

    public function generate()
    {
        $result = array();

        $content = $this->createContentFromServiceAction();

        $file = new GeneratedFileData();
        $result[] = $file;
        $file->path = "utils/kaltura-request-base.ts";
        $file->content = "import {KalturaObjectBase} from \"./kaltura-object-base\";
import * as ktypes from \"../types\";
import {JsonMember} from './typed-json';


export abstract class KalturaRequestBase extends KalturaObjectBase{

    {$this->utils->buildExpression($content->properties,NewLine,1)}


    constructor({$content->constructor})
    {
        super();

        {$this->utils->buildExpression($content->constructorRequiredContent, NewLine, 2 )}


        if (data) {
            {$this->utils->buildExpression($content->constructorOptionalContent, NewLine, 3 )}
        }
    }

    setData(handler : (request :  this) => void) :  this {
        if (handler) {
            handler(this);
        }
        return this;
    }

    abstract toObject() : any;
}";
        return $result;
    }

    function createContentFromServiceAction()
    {
        $result = new stdClass();
        $result->properties = array();
        $result->constructor = null;
        $result->constructorOptionalContent = array();
        $result->constructorRequiredContent = array();

        $constructorParameters = array();

        foreach ($this->serverMetadata->requestSharedParameters as $param) {
            $ng2ParamType = $this->toNG2TypeExp($param->type, $param->typeClassName);
            //$result->properties[] = "@JsonMember {$param->name} : {$ng2ParamType} {$this->utils->ifExp($param->defaultValue," = '" .$param->defaultValue . "'",'')};";

            if (!$param->readonly) {
                $result->properties[] = "@JsonMember {$param->name} : {$ng2ParamType};";

                if ($param->optional) {
                    $result->constructorOptionalContent[] = "this.{$param->name} = data.{$param->name};";
                } else {
                    $constructorParameters[] = "{$param->name} : {$ng2ParamType}";
                    $result->constructorRequiredContent[] = "this.{$param->name} = {$param->name};";
                }
            }else if ($param->readonly && $param->defaultValue != "")
            {
                $result->properties[] ="@JsonMember
public get {$param->name}() : string
{
    return '{$param->defaultValue}';
}";
            }
        }

        $result->constructor = "";
        if (count($constructorParameters) !== 0)
        {
            $result->constructor  .= join(", ",$constructorParameters) . ", data : any = {}";
        }else {
            $result->constructor .= "data : any = {}";
        }


        return $result;
    }

    protected function toNG2TypeExp($type, $typeClassName, $resultCreatedCallback = null)
    {
        return parent::toNG2TypeExp($type,$typeClassName,function($type,$typeClassName,$result)
        {
            switch($type) {
                case KalturaServerTypes::Object:
                case KalturaServerTypes::ArrayOfObjects:
                case KalturaServerTypes::EnumOfInt:
                case KalturaServerTypes::EnumOfString:
                    $result = "ktypes.{$result}";
                    break;
                default:
                    break;
            }

            return $result;
        });
    }
}