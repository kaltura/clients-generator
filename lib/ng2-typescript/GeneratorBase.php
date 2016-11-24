<?php

class GeneratorBase
{
    protected $serverMetadata;

    function __construct($serverMetadata)
    {
        $this->serverMetadata = $serverMetadata;
    }

    protected function upperCaseFirstLetter($str)
    {
        return ucwords($str);
    }

    protected function indent($text,$n){
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

    protected function toLispCase($input)
    {
        preg_match_all('!([A-Z][A-Z0-9]*(?=$|[A-Z][a-z0-9])|[A-Za-z][a-z0-9]+)!', $input, $matches);
        $ret = $matches[0];
        foreach ($ret as &$match) {
            $match = $match == strtoupper($match) ? strtolower($match) : lcfirst($match);
        }
        return implode('-', $ret);
    }

    protected function buildExpression($items, $lineSuffix, $textIndent = 0)
    {
        if (is_array($items)) {
            return count($items) != 0 ? $this->indent(join($lineSuffix, $items),$textIndent) : "";
        }else{
            return "";
        }
    }


    protected function toSnakeCase($input)
    {
        preg_match_all('!([A-Z][A-Z0-9]*(?=$|[A-Z][a-z0-9])|[A-Za-z][a-z0-9]+)!', $input, $matches);
        $ret = $matches[0];
        foreach ($ret as &$match) {
            $match = $match == strtoupper($match) ? strtolower($match) : lcfirst($match);
        }
        return implode('_', $ret);
    }

    protected function createDocumentationExp($spacer, $documentation)
    {
        if ($documentation) {
            return "/** " . NewLine . "{$spacer}* " . wordwrap(str_replace(array("\t", "\n", "\r"), " ", $documentation), 80, NewLine . "{$spacer}* ") . NewLine . "{$spacer}**/";
        }
        return "";
    }

    protected function getBanner()
    {
        $banner = "";
        return $banner;
    }

}