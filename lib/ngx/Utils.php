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

    public static function startsWithNumber($string) {
        return strlen($string) > 0 && ctype_digit(substr($string, 0, 1));
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

    public static function startsWith($str, $prefix)
    {
        return (substr($str, 0, strlen($prefix)) === $prefix);
    }


    public static function fromSnakeCaseToCamelCase($str)
    {
        $startWithSnake = Utils::startsWith($str,'_');
        $newName =  str_replace(' ', '', ucwords(str_replace(array('-', '_'), ' ', strtolower($str))));

        return ($startWithSnake ? '_' : '') . $newName;
    }

    public static function fromSnakeCaseToLowerCamelCase($str)
    {
        return lcfirst(Utils::fromSnakeCaseToCamelCase($str));
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

    public static function formatDescription($description, $prefix = "")
    {
        if ($description) {

            $description = $prefix ? "{$prefix}{$description}" :  $description;
            $description = array_map('trim', explode("\n", trim($description, ".\n\t\r ")));

            return $description;
        }
        return "";
    }


}
