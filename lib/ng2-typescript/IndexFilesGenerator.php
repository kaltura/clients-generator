<?php

require_once (__DIR__. '/NG2TypescriptGeneratorBase.php');
require_once (__DIR__. '/GeneratedFileData.php');

class IndexFilesGenerator extends NG2TypescriptGeneratorBase
{

    function __construct($serverMetadata)
    {
        parent::__construct($serverMetadata);
    }



    public function generate()
    {
        $result = array();

        $classIndex = $this->classIndexFile();
        $enumIndex = $this->enumIndexFile();
        $actionIndex = $this->actionIndexFile();

        $fileContent = $classIndex->content . $enumIndex->content . $actionIndex->content;

        $file = new GeneratedFileData();
        $file->path = "./types.ts";
        $file->content = $fileContent;
        $result[] = $file;

        return $result;

        return $result;
    }

    public function classIndexFile()
    {
        $fileContent = '';

        foreach ($this->serverMetadata->classTypes as $class) {
            $className = ucfirst($class->name);
            $classFileName = $this->utils->toLispCase($className);

            $fileContent .= "export { {$className} } from './class/{$classFileName}'" . NewLine;
        }

        $file = new GeneratedFileData();
        $file->path = "./classes.ts";
        $file->content = $fileContent;
        $result[] = $file;
        return $file;
    }

    public function enumIndexFile()
    {
        $fileContent = '';

        foreach ($this->serverMetadata->enumTypes as $enum) {
            if (count($enum->values) != "0") {
                $className = ucfirst($enum->name);
                $classFileName = $this->utils->toLispCase($className);

                $fileContent .= "export { {$className} } from './enum/{$classFileName}'" . NewLine;
            }
        }

        $file = new GeneratedFileData();
        $file->path = "./enums.ts";
        $file->content = $fileContent;
        $result[] = $file;
        return $file;
    }

    public function actionIndexFile()
    {
        $fileContent = '';

        foreach ($this->serverMetadata->services as $service) {
            foreach ($service->actions as $serviceAction) {
                $className = ucfirst($service->name) . ucfirst($serviceAction->name) . "Action";
                $classFileName = $this->utils->toLispCase($className);

                $fileContent .= "export { {$className} } from './action/{$classFileName}'" . NewLine;
            }
        }

        $file = new GeneratedFileData();
        $file->path = "./actions.ts";
        $file->content = $fileContent;
        $result[] = $file;
        return $file;
    }
}