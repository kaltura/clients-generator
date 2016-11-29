<?php

class Utils
{
    public static function upperCaseFirstLetter($str)
    {
        return ucwords($str);
    }






    public static function indent($text,$n){
        if(is_string($text) && is_int($n)){
            $indent = "";
            $i = 0;
            while($i < $n){
                $i++;
                $indent.= "\t";
            }
            return str_replace("\n", "\n".$indent, str_replace(array("\r\n","\r"), "\n", $text));
        }
    }

    public static function toLispCase($input)
    {
        preg_match_all('!([A-Z][A-Z0-9]*(?=$|[A-Z][a-z0-9])|[A-Za-z][a-z0-9]+)!', $input, $matches);
        $ret = $matches[0];
        foreach ($ret as &$match) {
            $match = $match == strtoupper($match) ? strtolower($match) : lcfirst($match);
        }
        return implode('-', $ret);
    }

    public static function buildExpression($items, $lineSuffix, $textIndent = 0)
    {
        if (is_array($items)) {
            return count($items) != 0 ? Utils::indent(join($lineSuffix, $items),$textIndent) : "";
        }else{
            return "";
        }
    }


    public static function fromSnakeCaseToCamelCase($str)
    {
        return str_replace(' ', '', ucwords(str_replace(['-', '_'], ' ', strtolower($str))));
    }

    public static function ifExp($condition, $truthlyValue, $falsyValue = "")
    {
        return $condition ? $truthlyValue : $falsyValue;
    }

    public static function toSnakeCase($input)
    {
        preg_match_all('!([A-Z][A-Z0-9]*(?=$|[A-Z][a-z0-9])|[A-Za-z][a-z0-9]+)!', $input, $matches);
        $ret = $matches[0];
        foreach ($ret as &$match) {
            $match = $match == strtoupper($match) ? strtolower($match) : lcfirst($match);
        }
        return implode('_', $ret);
    }

    public static function createDocumentationExp($spacer, $documentation)
    {
        if ($documentation) {
            return "/** " . NewLine . "* " . wordwrap(str_replace(array("\t", "\n", "\r"), " ", $documentation), 80, NewLine . "* ") . NewLine . "**/";
        }
        return "";
    }


}