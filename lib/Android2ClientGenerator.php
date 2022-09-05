<?php
class Android2ClientGenerator extends Java2ClientGenerator
{
    private $_testsDir = "";

	function __construct($xmlPath, Zend_Config $config, $sourcePath = "android2")
	{
		$this->_baseClientPath = "KalturaClient/" . $this->_baseClientPath;
		parent::__construct($xmlPath, $config, $sourcePath);
	}
	
	protected function normalizeSlashes($path)
	{
		return str_replace('/', DIRECTORY_SEPARATOR, $path);
	}

    public function setTestsPath($testsDir)
    {
        parent::setTestsPath($testsDir);
        $this->_testsDir = $testsDir;
    }

	protected function addFiles($sourcePath, $destPath)
	{
		$sourcePath = realpath($sourcePath);
        $destPath = $this->normalizeSlashes($destPath);
        KalturaLog::info("Adding files from [$sourcePath] to [$destPath].");
		$this->addSourceFiles($sourcePath, $sourcePath . DIRECTORY_SEPARATOR, $destPath);
	}
	
	public function generate() 
	{
        $this->addFiles("sources/java2/src", "KalturaClient/src/");
        $this->addFiles('tests/'. $this->_testsDir .'/java2/src', "KalturaClient/src/");
        parent::generate();
	}

    protected function addFile($fileName, $fileContents, $addLicense = true)
    {
        $excludePaths = array(
            "KalturaClient/src/main/java/com/kaltura/client/LoggerLog4j.java",
            "KalturaClient/src/main/resources",
        );

        foreach($excludePaths as $excludePath)
        {
            if($this->beginsWith($fileName, $excludePath))
                return;
        }

        parent::addFile($fileName, $fileContents, $addLicense);
    }

    /**
     * Overrides Java implementation and adds Parcelable interface implementation to the client lib classes
     * instead of Serializable, for better serialization support in android devices.
     * @param $classNode
     */
	public function finalizeClass(array &$imports, $classNode)
    {
        $imports[] = "import android.os.Parcel;";
        //$imports[] = "import android.os.Parcelable;";

        $this->generateParcelableImplementation($imports, $classNode);

        parent::finalizeClass($imports, $classNode);
    }


    private function generateParcelableImplementation(array &$imports, DOMElement $classNode)
    {
        $type = $classNode->getAttribute("name");

        if($classNode->hasAttribute("abstract") === false) {
            $this->appendLine("");
            $this->generateParcelCreator($type);
        }


        if ($classNode->childNodes->length > 0) {

            $this->appendLine("");
            $this->generateWriteToParcel($classNode);

        }

        $this->appendLine("");
        $this->generateParcelConstructor($imports, $classNode);
    }

    /**
     * generates the CREATOR for the Parcelable Objects
     * @param $type
     */
    private function generateParcelCreator($type)
    {
        $type = $this->getJavaTypeName($type);
        $this->appendLine("    public static final Creator<$type> CREATOR = new Creator<$type>() {");
        $this->appendLine("        @Override");
        $this->appendLine("        public $type createFromParcel(Parcel source) {");
        $this->appendLine("            return new $type(source);");
        $this->appendLine("        }");
        $this->appendLine("");
        $this->appendLine("        @Override");
        $this->appendLine("        public $type" . "[] newArray(int size) {");
        $this->appendLine("            return new $type"."[size];");
        $this->appendLine("        }");
        $this->appendLine("    };");
    }

    /**
     * generates the "writeToParcel" method, that goes over all class members and writes them
     * to the Parcel, according to their type.
     * @param $classNode
     */
    private function generateWriteToParcel($classNode)
    {
        $this->appendLine("    @Override");
        $this->appendLine("    public void writeToParcel(Parcel dest, int flags) {");
        $this->appendLine("        super.writeToParcel(dest, flags);");

        foreach ($classNode->childNodes as $propertyNode) {
            if ($propertyNode->nodeType != XML_ELEMENT_NODE)
                continue;

            $propType = $this->getFinalPropertyType($propertyNode);
            $propName = $propertyNode->getAttribute("name");
            $isEnum = $propertyNode->hasAttribute("enumType");
            $arrayType = $propertyNode->getAttribute("arrayType");

            if($isEnum)
            {
            	if($propType === 'Boolean')
            	{
            		$propType = 'boolean';
            	}
            	else 
            	{
	                $propType = "enum";
            	}
            }

            switch ($propType) {
                case "long":
                case "int":
                case "float":
                case "String":
                case "double":
                    $this->appendLine("        dest.write".ucfirst($propType)."(this.$propName);");
                    break;

                case "boolean":
                    $this->appendLine("        dest.writeValue(this.$propName);");
                    break;
                
                case "Long":
                case "Integer":
                case "Boolean":
                case "Float":
                case "Double":
                    $this->appendLine("        dest.writeValue(this.$propName);");
                    break;

                case "List":
                    $this->appendListParcel($propName);
                    break;

                case "Map":
                    $this->appendMapParcel($propName, $arrayType);
                    break;

                case "enum":
                    $this->appendLine("        dest.writeInt(this.$propName == null ? -1 : this.$propName.ordinal());");
                    break;

                default:
                    $this->appendLine("        dest.writeParcelable(this.$propName, flags);");
                    break;
            }
        }
        $this->appendLine("    }");

    }

    private function appendListParcel($propName)
    {
        $this->appendLine("        if(this.$propName != null) {");
        $this->appendLine("            dest.writeInt(this.$propName.size());");
        $this->appendLine("            dest.writeList(this.$propName);");
        $this->appendLine("        } else {");
        $this->appendLine("            dest.writeInt(-1);");
        $this->appendLine("        }");
    }

    private function appendMapParcel($propName, $arrayType)
    {
        $arrayType = $this->getObjectType($arrayType, true);

        $this->appendLine("        if(this.$propName != null) {");
        $this->appendLine("            dest.writeInt(this.$propName.size());");
        $this->appendLine("            for (Map.Entry<String, $arrayType> entry : this.$propName.entrySet()) {");
        $this->appendLine("                dest.writeString(entry.getKey());");
        $this->appendLine("                dest.writeParcelable(entry.getValue(), flags);");
        $this->appendLine("            }");
        $this->appendLine("        } else {");
        $this->appendLine("            dest.writeInt(-1);");
        $this->appendLine("        }");
    }

    /**
     * generates the Parcel parametrized constructor for Parcelable implementing classes
     * @param DOMElement $classNode
     */
    private function generateParcelConstructor(array &$imports, DOMElement $classNode){
        $className = $this->getJavaTypeName($classNode->getAttribute("name"));

        $this->appendLine("    public $className(Parcel in) {");
        $this->appendLine("        super(in);");

        foreach ($classNode->childNodes as $propertyNode) {
            if ($propertyNode->nodeType != XML_ELEMENT_NODE)
                continue;

            $propType = $this->getFinalPropertyType($propertyNode);
            $propName = $propertyNode->getAttribute("name");
            $isEnum = $propertyNode->hasAttribute("enumType");
            $arrayType = $propertyNode->getAttribute("arrayType");

            if($isEnum)
            {
            	if($propType === 'Boolean')
            	{
            		$propType = 'boolean';
            	}
            	else 
            	{
                	$enumName = $propType;
	                $propType = "enum";
            	}
            }

            switch ($propType) {
                case "long":
                case "int":
                case "float":
                case "String":
                case "double":
                    $this->appendLine("        this.$propName = in.read".ucfirst($propType)."();");
                    break;

                case "boolean":
                    $upperValue = ucfirst($propType);
                    $this->appendLine("        this.$propName = ($upperValue)in.readValue($upperValue.class.getClassLoader());");
                    break;

                case "Long":
                case "Integer":
                case "Boolean":
                case "Float":
                case "Double":
                    $this->appendLine("        this.$propName = ($propType)in.readValue($propType.class.getClassLoader());");
                    break;

                case "List":
                    $this->readListParcel($propName, $arrayType);
                    $imports[] = "import java.util.ArrayList;";
                    break;

                case "Map":
                    $this->readMapParcel($propName, $arrayType);
					$imports[] = "import java.util.HashMap;";
                    break;

                case "enum":
                    $tmpName = "tmp".ucfirst($propName);
                    $this->appendLine("        int $tmpName = in.readInt();");
                    $this->appendLine("        this.$propName = $tmpName == -1 ? null : $enumName.values()[$tmpName];");
                    break;

                default:
                    $this->appendLine("        this.$propName = in.readParcelable($propType.class.getClassLoader());");
                    break;
            }
        }
        $this->appendLine("    }");

    }

    /**
     * get the raw type of the member. in case of a Map or List cut off the "<...>" brackets
     * @param $propertyNode
     * @return bool|string
     */
    private function getFinalPropertyType($propertyNode)
    {
        $javaType = $this->getJavaType($propertyNode, true);
        $cutHere = strpos($javaType, "<");
        if ($cutHere === false) {
            $cutHere = strlen($javaType);
        }
        $propType = substr($javaType, 0, $cutHere);
        return $propType;
    }

    private function readListParcel($propName, $arrayType)
    {
        $arrayType = $this->getObjectType($arrayType, true);

        $this->appendLine("        int $propName"."Size = in.readInt();");
        $this->appendLine("        if( $propName"."Size > -1) {");
        $this->appendLine("            this.$propName = new ArrayList<>();");
        $this->appendLine("            in.readList(this.$propName, $arrayType.class.getClassLoader());");
        $this->appendLine("        }");
    }

    private function readMapParcel($propName, $arrayType)
    {
        $arrayType = $this->getObjectType($arrayType, true);

        $this->appendLine("        int $propName"."Size = in.readInt();");
        $this->appendLine("        if( $propName"."Size > -1) {");
        $this->appendLine("            this.$propName = new HashMap<>();");
        $this->appendLine("            for (int i = 0; i < $propName"."Size; i++) {");
        $this->appendLine("                String key = in.readString();");
        $this->appendLine("                $arrayType value = in.readParcelable($arrayType.class.getClassLoader());");
        $this->appendLine("                this.$propName.put(key, value);");
        $this->appendLine("            }");
        $this->appendLine("        }");
    }
}
