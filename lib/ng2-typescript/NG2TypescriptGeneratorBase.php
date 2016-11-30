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
                if ($typeClassName == "string")
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
            case KalturaServerTypes::ArrayObject:
                $result = $defaultValue ? $defaultValue : "[]";
                break;
            default:
                $result = $defaultValue ? $defaultValue : "null";
                break;

        }

        return $result;
    }

    protected function requestBuildExp($name, $type, $insideAction)
    {
        $enumType = 'General';
        switch($type)
        {
            case KalturaServerTypes::ArrayObject:
                $enumType = 'Array';
                break;
            case KalturaServerTypes::Date:
                $enumType = 'Date';
                break;
            default:
                break;
        }

        $valueExp = $insideAction ? "this.{$name}" : "this.objectData['{$name}']";
        return  "{$name} : this.buildPropertyValue('{$name}', {$valueExp}, KalturaPropertyTypes.{$enumType})";
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
            case KalturaServerTypes::ArrayObject:
                $result = "{$typeClassName}[]";
                break;
            case KalturaServerTypes::Enum:
            case KalturaServerTypes::Object:
                $result = $typeClassName;
                break;
            case KalturaServerTypes::Date:
                $result = "Date";
                break;
            case KalturaServerTypes::Void:
                $result = "VoidResponseResult";
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

    protected function getBanner()
    {
        $banner = "";
        return $banner;
    }

}