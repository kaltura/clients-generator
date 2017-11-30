
export class KalturaUtils
{
    static fromServerDate(value : number) : Date
    {
        return (value ? new Date(value * 1000) : null);
    }

    static toServerDate(value : Date) : number
    {
        return value ? Math.round(value.getTime() / 1000) : null;
    }

    static getStartDateValue(value : Date) : Date
    {
	    if (value) {
		    value.setHours(0);
		    value.setMinutes(0);
		    value.setSeconds(0);
		    return value;
	    }else{
	    	return null;
	    }
    }

    static getEndDateValue(value : Date) : Date
    {
	    if (value) {
	        value.setHours(23);
		    value.setMinutes(59);
		    value.setSeconds(59);
	        return value;
	    }else{
		    return null;
	    }
    }

    static fromEnumOfStringToValue<T>(value : T) : string{
        return value ? value.toString() : null
    }

    static toEnumOfStringToValue<T>(value : string, objectConstructor : {new(value:string) : T}) : T{
        return new objectConstructor(value);
    }
}
