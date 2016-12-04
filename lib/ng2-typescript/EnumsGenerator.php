<?php

require_once (__DIR__. '/NG2TypescriptGeneratorBase.php');
require_once (__DIR__. '/GeneratedFileData.php');

class EnumsGenerator extends NG2TypescriptGeneratorBase
{

    function __construct($serverMetadata)
    {
        parent::__construct($serverMetadata);
    }

    public function generate()
    {
        $result = array_merge(
            $this->createEnumTypes()
        );

        return $result;
    }

    function createEnumTypes()
    {
        $enumTypes = array();

        foreach ($this->serverMetadata->enumTypes as $enum) {
            if (count($enum->values) != "0") {
                // ignore enums without values
                $enumTypes[] = $this->createEnumTypeExp($enum);
            }
        }

        $fileContent = "
import {KalturaUtils} from \"./utils/kaltura-utils\";
import { JsonObject } from './utils/typed-json';

{$this->utils->buildExpression($enumTypes,NewLine . NewLine)}
";

        $result = array();
        $file = new GeneratedFileData();
        $file->path = "kaltura-enums.ts";
        $file->content = $fileContent;
        $result[] = $file;
        return $result;
    }

    function createEnumTypeExp(EnumType $enum)
    {
        $enumTypeName = Utils::upperCaseFirstLetter($enum->name);

        switch($enum->type)
        {
            case 'int':
                $values = array();
                foreach ($enum->values as $item) {
                    $values[] = Utils::fromSnakeCaseToCamelCase($item->name) . "=" . $item->value;
                }

                $result = "
{$this->getBanner()}
export enum {$enumTypeName} {
    {$this->utils->buildExpression($values,',' . NewLine, 1)}
}";
                break;
            case 'string':
                $values = array();
                foreach ($enum->values as $item) {
                    $enumName = Utils::fromSnakeCaseToCamelCase($item->name);
                    $values[] = "static {$enumName} = new {$enumTypeName}('{$item->value}');";
                }

                $result = "
{$this->getBanner()}


@JsonObject({serializer : KalturaUtils.FromEnumOfStringToValue})
export class {$enumTypeName} {
    constructor(private value?:string){
    }

    toString(){
        return this.value;
    }

    {$this->utils->buildExpression($values, NewLine, 1)}
}";
                break;
        }

        return $result;
    }
}