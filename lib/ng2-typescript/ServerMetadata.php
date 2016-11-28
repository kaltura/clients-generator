<?php

class KalturaServerTypes
{
    const Unknown = "Unknown";
    const Simple = "Simple";
    const Object = "Object";
    const ArrayObject = "ArrayObject";
    const Void = "Void";
    const Enum = "Enum";
    const Date = "Date";
    const File = "File";
}

class Service
{
    public $id;
    public $name;
    public $description;
    public $actions = array();

    public function validate()
    {
        $errors = array();

        foreach($this->actions as $action)
        {
            $errors = array_merge($errors, $action->validate($this));
        }

        return $errors;
    }
}

class ServiceAction
{
    public $name;
    public $resultClassName;
    public $resultType;
    public $params = array();
    public $description;
    public $enableInMultiRequest = 1;

    public function validate($service)
    {
        $errors = array();

        if (!isset($this->resultType) || $this->resultType == KalturaServerTypes::Unknown)
        {
            $errors[] = "Unknown type for service {$service->name} > action {$this->name} > result type";
        }

        foreach($this->params as $param)
        {
            $errors = array_merge($errors, $param->validate($service, $this));
        }

        return $errors;
    }

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
    public $typeClassName;
    public $type;
    public $optional = false;
    public $default;

    public function validate($service, $action)
    {
        $errors = array();

        if (!isset($this->type) || $this->type == KalturaServerTypes::Unknown)
        {
            $errors[] = "Unknown type for service {$service->name} > action {$action->name} > param {$this->name}";
        }

        if ($this->optional && (!isset($this->default)))
        {
            $errors[] = "Missing default value for service {$service->name} > action {$action->name} > param {$this->name}";
        }

        return $errors;
    }
}


class ClassType
{
    public $name;
    public $base = null;
    public $plugin = null;
    public $description = null;
    public $abstract = false;
    public $deprecated = false;
    public $properties = array();

    public function validate()
    {
        $errors = array();

        foreach($this->properties as $property)
        {
            $errors = array_merge($errors, $property->validate($this));
        }

        return $errors;
    }

    public function getOptionaProperties()
    {
        $result = array();

        foreach($this->properties as $property)
        {
            if ($property->optional)
            {
                $result[] = $property;
            }
        }

        return $result;
    }

    public function getRequiredProperties()
    {
        $result = array();

        foreach($this->properties as $property)
        {
            if (!$property->optional)
            {
                $result[] = $property;
            }
        }

        return $result;
    }
}

class ClassTypeProperty
{
    public $name;
    public $description = null;
    public $typeClassName = null;
    public $type;
    public $writeOnly = false;
    public $readOnly = false;
    public $insertOnly = false;
    public $optional = true;
    public $default = 'null';

    public function validate($classType)
    {
        $errors = array();

        if (!isset($this->type) || $this->type == KalturaServerTypes::Unknown)
        {
            $errors[] = "Unknown type for class {$classType->name} > property {$this->name}";
        }

        if ($this->optional && (!isset($this->default)))
        {
            $errors[] = "Missing default value for class  {$classType->name}  > property {$this->name}";
        }

        return $errors;
    }
}

class ServerMetadata
{
    public $services = array();
    public $classTypes = array();
}