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

    function mapToDefaultValue($type, $typeClassName, $defaultValue)
    {
        $result = null;

        switch ($type)
        {
            case KalturaServerTypes::Simple:
                if ($typeClassName == "string")
                {
                    $result = isset($defaultValue) ?  "\"{$defaultValue}\"" : null;
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
                $result = "[]";
                break;
            default:
                $result = "null";
                break;

        }

        return $result;
    }

    protected function requestBuildExp($name, $type)
    {
        $result = null;
        switch($type)
        {
            case KalturaServerTypes::ArrayObject:
                $result = "{$name} : KalturaUtils.toServerArray(this.{$name})";
                break;
            case KalturaServerTypes::Date:
                $result = "{$name} : KalturaUtils.toServerDate(this.{$name})";
                break;
            default:
                $result = "{$name} : this.{$name}";
                break;
        }

        return  $result;
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