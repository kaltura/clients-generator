import { KalturaUtils } from "./utils/kaltura-utils";
import { KalturaTypesFactory } from './kaltura-types-factory';

export type DependentProperty = { property : string, request : number, targetPath? : string[] };

export interface KalturaObjectMetadata
{
    properties : { [key : string] : KalturaObjectPropertyMetadata};
}

export interface KalturaObjectPropertyMetadata
{
    readOnly? : boolean;
    type : string;
    subType? : string;
    default? : string;
    subTypeConstructor? : { new() : KalturaObjectBase };
};

export interface KalturaObjectBaseArgs
{

}

export abstract class KalturaObjectBase{

    private _dependentProperties : { [key : string] : DependentProperty} = {};

    setData(handler : (request :  this) => void) :  this {
        if (handler) {
            handler(this);
        }
        return this;
    }

    constructor(data? : {})
    {
        if (data)
        {
            Object.assign(this, data);
        }
    }

    public getTypeName() : string
    {
        return this._getMetadata().properties['objectType'].default;
    }

    protected _getMetadata() : KalturaObjectMetadata
    {
        return { properties : {}};
    }


    toRequestObject() : {} {
        const metadata = this._getMetadata();
        let result = {};

        try {
            Object.keys(metadata.properties).forEach(propertyName => {
                const propertyData  = metadata.properties[propertyName];
                const propertyValue = this._createRequestPropertyValue(propertyName, propertyData);

                switch (propertyValue.status)
                {
                    case "exists":
                        result[propertyName] = propertyValue.value;
                        break;
                    case "removed":
                        result[`${propertyName}__null`] = ''; // mark property for deletion
                        break;
                    case "missing":
                    default:
                        break;
                }
            });
        }catch(err)
        {
            // TODO [kaltura] should use logHandler
            console.warn(err.message);
            throw err;
        }

        return result;
    }

    fromResponseObject(data : any) : {} {
        const metadata = this._getMetadata();
        let result = {};

        try {
            Object.keys(metadata.properties).forEach(propertyName => {
                const propertyData = metadata.properties[propertyName];
                const propertyValue = this._parseResponseProperty(propertyName, propertyData, data);

                if (propertyValue != null && typeof propertyValue !== 'undefined')
                {
                    this[propertyName] = propertyValue;
                }
            });
        }catch(err)
        {
            // TODO [kaltura] should use logHandler
            console.warn(err.message);
            throw err;
        }

        return result;
    }

    protected _parseResponseProperty(propertyName : string, property : KalturaObjectPropertyMetadata, source : any) : any {

        let result;
        let sourceValue = propertyName ? source[propertyName] : source;

        if (typeof sourceValue !== 'undefined') {
            if (sourceValue === null) {
                result = null;
            } else {
                switch (property.type) {
                    case 'b': // boolean
                        if (typeof sourceValue === 'boolean') {
                            result = sourceValue;
                        } else if (sourceValue + '' === '0') {
                            result = false;
                        } else if (sourceValue + '' === '1') {
                            result = true;
                        }
                        break;
                    case 's': // string
                        result = sourceValue + '';
                        break;
                    case 'n': // number
                    case 'en': // enum of type number
                        result = sourceValue * 1;
                        break;
                    case 'o': // object
                        const propertyObjectType = sourceValue['objectType'];

                        if (propertyObjectType)
                        {
                            result = this._createKalturaObject(propertyObjectType, property.subType);

                            if (result) {
                                result.fromResponseObject(sourceValue);
                            } else {
                                throw new Error(`Failed to create kaltura object of type '${source['objectType']}' (fallback type '${property.subType}')`);
                            }
                        }else
                        {
                            throw new Error(`Failed to create kaltura object for property '${propertyName}' (type '${property.subType}'). provided response object is missing property 'objectType'.`);
                        }

                        break;
                    case 'm': // map
                        const parsedMap = {};
                        if (sourceValue instanceof Object) {
                            Object.keys(sourceValue).forEach(itemKey =>
                            {
                                const itemValue = sourceValue[itemKey];
                               const newItem =  this._createKalturaObject(property.subType);

                                if (itemValue && newItem) {
                                    newItem.fromResponseObject(itemValue);
                                    parsedMap[itemKey] = newItem;
                                } else {
                                    throw new Error(`Failed to create kaltura object for type '${property.subType}'`);
                                }

                            });
                        } else {
                            throw new Error(`failed to parse property '${propertyName}. Expected type object, got type '${typeof sourceValue}`);
                        }
                        break;
                    case 'a': // array
                        if (sourceValue instanceof Array) {
                            const parsedArray = [];
                            sourceValue.forEach(responseItem => {
                                const newItem = this._createKalturaObject(responseItem['objectType'], property.subType);

                                if (newItem) {
                                    newItem.fromResponseObject(responseItem);
                                    parsedArray.push(newItem);
                                } else {
                                    throw new Error(`Failed to create kaltura object for type '${responseItem['objectType']}' and for fallback type '${property.subType}'`);
                                }
                            });

                            if (parsedArray.length)
                            {
                                result = parsedArray;
                            }
                        } else {
                            throw new Error(`failed to parse property '${propertyName}. Expected type array, got type '${typeof sourceValue}`);
                        }
                        break;
                    case 'd': // date
                        if (this._isNumeric(sourceValue)) {
                            result = KalturaUtils.fromServerDate(sourceValue*1)
                        }else {
                            throw new Error(`failed to parse property '${propertyName}. Expected type date, got type '${typeof sourceValue}`);
                        }
                        break;
                    case "es":
                        result = this._createKalturaObject(property.subType);

                        if (result && typeof result !== 'undefined') {
                            result['_value'] = sourceValue + '';
                        } else {
                            throw new Error(`Failed to create kaltura enum for type '${property.subType}'`);
                        }
                        break;
                    default:
                        break;
                }

            }
        }

        return result;
    }

    private _isNumeric(n : any) : boolean {
        return !isNaN(parseFloat(n)) && isFinite(n);
    }

    private _createKalturaObject(objectType : string, fallbackObjectType? : string) : KalturaObjectBase
    {
        let result = null;
        let usedFallbackType = false;
        if (objectType)
        {
            result = KalturaTypesFactory.createObject(objectType);
        }

        if (!result && fallbackObjectType)
        {
            usedFallbackType = true;
            result = KalturaTypesFactory.createObject(fallbackObjectType);
        }

        if (usedFallbackType && result)
        {
                console.warn(`[kaltura-typescript-client]: Could not find object type '${objectType}', Falling back to '${fallbackObjectType}' object type. (Did you remember to set your accepted object types in the request “acceptedTypes” attribute?)`)
        }else if (!result)
        {
            console.warn(`[kaltura-typescript-client]: Could not find object type '${objectType}'. (Did you remember to set your accepted object types in the request “acceptedTypes” attribute?)`)
        }

        return result;
    }

    private _createRequestPropertyValue(propertyName : string, property : KalturaObjectPropertyMetadata) : { status : 'missing' | 'removed' | 'exists', value? : any } {

        let result : { status : 'missing' | 'removed' | 'exists', value? : any } = { status : 'missing'};

        if (property.type === 'c')
        {
            // constant string
            if (property.default)
            {
                result = { status : 'exists', value : property.default};
            }
        } else if (this._dependentProperties[propertyName])
        {
            const dependentProperty = this._dependentProperties[propertyName];
            const resultValue = `{${dependentProperty.request}:result${dependentProperty.targetPath ? ':' + dependentProperty.targetPath : ''}}`;
            result = { status : 'exists', value : resultValue};
        }
        else if (!property.readOnly) {
            let value = this[propertyName];

            if (typeof value !== 'undefined') {
                if (value === null) {
                    result = { status : 'removed'};
                } else {
                    switch (property.type) {
                        case 'b': // boolean
                            result = { status : 'exists', value : value};
                            break;
                        case 's': // string
                            result = { status : 'exists', value : value + ''};
                            break;
                        case 'n': // number
                        case 'en': // enum of type number
                            result = { status : 'exists', value : value * 1};
                            break;
                        case 'o': // object
                            if (value instanceof KalturaObjectBase) {
                                result = { status : 'exists', value : value.toRequestObject()};
                            }else
                            {
                                throw new Error(`failed to parse property. Expected '${propertyName} to be kaltura object`);
                            }
                            break;
                        case 'a': // array
                            if (value instanceof Array) {
                                const parsedArray = [];
                                value.forEach(item =>
                                {
                                    if (item instanceof KalturaObjectBase)
                                    {
                                        parsedArray.push(item.toRequestObject());
                                    }
                                });

                                if (parsedArray.length !== 0) {
                                    if (parsedArray.length === value.length) {
                                        result = {status: 'exists', value: parsedArray};
                                    } else {
                                        throw new Error(`failed to parse array. Expected all '${propertyName} items to be kaltura object`);
                                    }
                                }
                            }else
                            {
                                throw new Error(`failed to parse property. Expected '${propertyName} to be kaltura object`);
                            }
                            break;
                        case 'd': // date
                            if (value instanceof Date) {
                                result = { status : 'exists', value : KalturaUtils.toServerDate(value)};
                            }else {
                                throw new Error(`failed to parse property. Expected '${propertyName} to be date`);
                            }
                            break;
                        case 'es': // enum of type string
                            if (typeof value._value !== 'undefined') {
                                result = { status : 'exists', value : value._value};
                            }else
                            {
                                throw new Error(`failed to parse property. Expected '${propertyName} to be of type string enum`);
                            }
                            break;
                        case 'f':
                            if (value instanceof FormData)
                            {
                                result = {status : 'exists', value : value};
                            }
                            break;
                        default:
                            // do nothing
                            break;
                    }
                }
            }
        }

        return result;
    }

    setDependency(...dependency : (DependentProperty | [string, number] | [string, number,string])[]) : this
    {
        for(let i = 0, len = dependency.length;i<len;i++)
        {
            const item = dependency[i];
            let { property, request, targetPath } = <any>item;
            if (item instanceof Array)
            {
                property = item[0];

                // The server expect one based index (meaning the first item has index 1)
                // since Javascript array are zero based index we expose the api as zero based
                // and transform the index value in the actual request by adding 1
                request = item[1]+1;
                targetPath = item.length == 3 ? item[2] : null;
            }

            this._dependentProperties[property] = { property , request, targetPath };
        }

        return this;
    }
}
