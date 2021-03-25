# Kaltura GO API Client Library.
Compatible with Kaltura server version 6.2.0.28984 and above.

## Generation of enums:
1. for each enum object code is generating package by the enum name (lower case).
2. each package will contain 1 file named "enum.go" with enum type as string.
3. file location KalturaOttGeneratedAPIClientsGo/kalturaclient/enums/{package}/enum.go
4. enum of enum type will be the same as Phoenix just without "kaltura" prefix.
5. in enum.go code will define all values of enum as const values.
6. in enum.go code generate UnmarshalJSON for this enum type and throw an error if someone tryies to set invalid enum value.
7. if enum does not contain any value, UnmarshalJSON will throw an error (no option to creta an instance of it)

### example for generated enum:
```go
package adspolicy

import (
   "encoding/json"
   "errors"
)

type AdsPolicy string

const (
   NO_ADS AdsPolicy = "NO_ADS"
   KEEP_ADS AdsPolicy = "KEEP_ADS"
)

func (e *AdsPolicy) UnmarshalJSON(b []byte) error {
   var s string
   err := json.Unmarshal(b, &s)
   if err != nil {
      return err
   }
   enumValue := AdsPolicy(s)
   switch enumValue {
   case NO_ADS, KEEP_ADS:
      *e = enumValue
      return nil
   }
   return errors.New("invalid enum value")
}
```

## Generation of types:
1. each class will include all of its properites + base class properites.
2. all of non-abstract classes will implement func of MarshalJSON to include "objectType" property.
3. each class will have an interface that will contains getters for all properites in class.
4. if class has a base class so the class's interface will contain the interface of base class.
5. each class will implement its interface funcions.

who hold the container? FROM WHERE IS THE Container?
duplicate properties in type will be of?
each Container struct will implement func of UnmarshalJSON to return specific struct

## Generation of services:
1. if a param of an action in class of phoniex it will recive the interface of the class and not the class itself
2. all actions first param is ctx context.Context and last param is extra ...kalturaclient.Param - between then we add the real params of the action.
3. all action return 2 objects: pointer to result of the action and error
4. if a param of an action can be null it will pass as pointer and will be set only if not nil
