/**
 * Response format types for Kaltura API requests.
 * This enum defines the possible response formats that can be requested from the Kaltura API.
 */
export enum KalturaResponseType {
    /** JSON response format */
    responseTypeJson = 1,
    /** XML response format */
    responseTypeXml = 2,
    /** JSONP response format */
    responseTypeJsonp = 9,
    /** Asset XML response format */
    responseTypeAssetXml = 30,
    /** Excel response format */
    responseTypeExcel = 31
}
