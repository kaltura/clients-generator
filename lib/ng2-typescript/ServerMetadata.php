<?php

class Service
{
    public $id;
    public $name;
    public $description;
    public $actions = array();
}

class ServiceAction
{
    public $name;
    public $resultType;
    public $params = array();
    public $description;
    public $enableInMultiRequest = 1;

    public function getOptionalParams()
    {
        $result = array();

        foreach($this->params as $param)
        {
            if ($param->optional)
            {
                $result[] = $param;
            }
        }

        return $result;
    }

    public function getRequiredParams()
    {
        $result = array();

        foreach($this->params as $param)
        {
            if (!$param->optional)
            {
                $result[] = $param;
            }
        }

        return $result;
    }
}

class ServiceActionParam
{
    public $name;
    public $type;
    public $optional;
    public $default;
}

class ServerMetadata
{
    public $services = array();
}