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
                }else
                {
                    $result = $defaultValue;
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

    protected function toNG2TypeExp($type, $typeClassName)
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
                $result = "kclasses.{$typeClassName}[]";
                break;
            case KalturaServerTypes::Enum:
                $result = 'kenums.' . "$typeClassName";
                break;
            case KalturaServerTypes::Object:
                $result = 'kclasses.' . "$typeClassName";
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

        if (isset($resultHandlerFunction))
        {
            $result = $resultHandlerFunction($type, $typeClassName, $result);
        }
        return  $result;
    }

    protected function getBanner()
    {
        $banner = "";
        return $banner;
    }

}