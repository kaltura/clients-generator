<?php

require_once (__DIR__. '/TypescriptGeneratorBase.php');
require_once (__DIR__. '/GeneratedFileData.php');

class IndexFilesGenerator extends TypescriptGeneratorBase
{

    function __construct($serverMetadata, $framework)
    {
        parent::__construct($serverMetadata);

        $this->framework = $framework;
    }

    public function generate()
    {
        $isAngularFramework = $this->framework === 'ngx';
        $result = array();

        $classIndex = $this->classIndexFile();
        $enumIndex = $this->enumIndexFile();
        $actionIndex = $this->actionIndexFile();

        $fileContent = $classIndex->content . $enumIndex->content . $actionIndex->content;

        if (!$isAngularFramework) {
        $fileContent .= "

console.warn('Notice! Your application bundle the whole package of kaltura-xxx-client (either rxjs/ngx/typescript), please refer to the library `readme.md` to reduce app bundle size.')";
        }

        $file = new GeneratedFileData();
        $file->path = "./types/index.ts";
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
            $classFileName = $className; // $this->utils->toLispCase($className);

            $fileContent .= "export { {$className}, {$className}Args } from './{$classFileName}'" . NewLine;
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
                $classFileName = $className; //$this->utils->toLispCase($className);

                $fileContent .= "export { {$className} } from './{$classFileName}'" . NewLine;
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
                $classFileName = $className ; //$this->utils->toLispCase($className);

                $fileContent .= "export { {$className}, {$className}Args } from './{$classFileName}'" . NewLine;
            }
        }

        $file = new GeneratedFileData();
        $file->path = "./actions.ts";
        $file->content = $fileContent;
        $result[] = $file;
        return $file;
    }
}
