<?php

require_once (__DIR__. '/Utils.php');


class NG2TypescriptGeneratorBase
{
    protected $serverMetadata;
    protected $utils;

    function __construct($serverMetadata)
    {
        $this->utils = new Utils();
        $this->serverMetadata = $serverMetadata;
    }

    function toNG2DefaultByType($type, $typeClassName, $defaultValue)
    {
        $result = null;

        switch ($type)
        {
            case KalturaServerTypes::Simple:
                if ($typeClassName == "string" && $defaultValue !== "null")
                {
                    $result = $defaultValue ?  "\"{$defaultValue}\"" : "\"\"";
                }else if ($defaultValue)
                {
                    $result = $defaultValue;
                }else
                {
                    // TODO workaround, handling scenarios like param of type int without default value
                    $result = "null";
                }
                break;
            case KalturaServerTypes::ArrayOfObjects:
                $result = $defaultValue ? $defaultValue : "[]";
                break;
            default:
                $result = $defaultValue ? $defaultValue : "null";
                break;

        }

        return $result;
    }


    protected function toNG2TypeExp($type, $typeClassName, $resultCreatedCallback)
    {
        $result = null;
        switch($type)
        {
            case KalturaServerTypes::File:
                $result = 'string';
                break;
            case KalturaServerTypes::Simple:
                switch($typeClassName)
                {
                    case "bool":
                        $result = "boolean";
                        break;
                    case "bigint":
                    case "float":
                    case "int":
                        $result = 'number';
                        break;
                    case "string":
                        $result = 'string';
                        break;
                    default:
                        throw new Exception("Unknown simple type {$typeClassName}");
                }
                break;
            case KalturaServerTypes::ArrayOfObjects:
                $result = "{$typeClassName}[]";
                break;
            case KalturaServerTypes::EnumOfInt:
            case KalturaServerTypes::EnumOfString:
            case KalturaServerTypes::Object:
                $result = $typeClassName;
                break;
            case KalturaServerTypes::Date:
                $result = "Date";
                break;
            case KalturaServerTypes::Void:
                $result = "void";
                break;
            default:
                throw new Exception("toNG2TypeExp: Unknown type requested {$type}");
        }

        if (isset($resultCreatedCallback))
        {
            $result = $resultCreatedCallback($type, $typeClassName, $result);
        }
        return  $result;
    }

    protected function getPropertyDecorator($type, $typeClassName, $className = "")
    {
        $result = null;

        switch($type)
        {
            case KalturaServerTypes::ArrayOfObjects:
                $result = "@JsonMember({elements : {$className}{$typeClassName}})";
                break;
            default:
                $result = "@JsonMember";
                break;
        }

        return $result;
    }

    protected function getBanner()
    {
        $banner = "";
        return $banner;
    }

}