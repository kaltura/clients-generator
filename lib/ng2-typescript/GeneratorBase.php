<?php

require_once (__DIR__. '/Utils.php');


class GeneratorBase
{
    protected $serverMetadata;

    function __construct($serverMetadata)
    {
        $this->serverMetadata = $serverMetadata;
    }


    protected function getBanner()
    {
        $banner = "";
        return $banner;
    }

}