<?php

class PythonClientStubFileProperty
{
	public $name;
	public $class;
	public $importPath;

	public function __construct(string $name, string $class, string $importPath)
	{
		$this->name = $name;
		$this->class = $class;
		$this->importPath = $importPath;
	}
}
