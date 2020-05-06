import { KalturaObjectBase } from "./kaltura-object-base";
import { typesMapping, KalturaObjectClass } from "./types-mapping";

// TODO [kaltura] constructor type should be 'KalturaObjectBase' (currently any to support enum of type string)
export class KalturaTypesFactory {
  static registerType(typeName: string, objectCtor: KalturaObjectClass): void {
    typesMapping[typeName] = objectCtor;
  }

  static createObject(type: KalturaObjectBase): KalturaObjectBase;
  static createObject(typeName: string): KalturaObjectBase;
  static createObject(type: any): KalturaObjectBase {
    let typeName = "";

    if (type instanceof KalturaObjectBase) {
      typeName = type.getTypeName();
    } else if (typeof type === "string") {
      typeName = type;
    }

    const factory: KalturaObjectClass = typeName ? typesMapping[typeName] : null;
    return factory ? new factory() : null;
  }
}
