
export class KalturaClientUtils
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
		    const result = new Date(value);
		    result.setHours(0);
		    result.setMinutes(0);
		    result.setSeconds(0);
		    return result;
	    }else{
	    	return null;
	    }
    }

    static getEndDateValue(value : Date) : Date
    {
	    if (value) {
	        const result = new Date(value);
	        result.setHours(23);
		    result.setMinutes(59);
		    result.setSeconds(59);
	        return result;
	    }else{
		    return null;
	    }
    }
}
