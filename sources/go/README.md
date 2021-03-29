# Kaltura GO API Client Library.
Compatible with Kaltura server version 6.2.0.28984 and above.
* [Examples of API](./test)


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
1. all classes will include all of its properites + base class properites.
   1.  if property in class is readOnly/insertOnly/writeOnly/optional/nullable so it will be a pointer with 'omitempty' in json.
   2. if class contains property by the same name as it's base class it will contain only its own property (name and type)
```go
type Filter struct {
	OrderBy string `json:"orderBy"`
}

type AssetFilter struct {
	Name           string                    `json:"name"`
	DynamicOrderBy DynamicOrderBy            `json:"dynamicOrderBy"`
	OrderBy        assetorderby.AssetOrderBy `json:"orderBy"` // Filter.OrderBy is from string type but generatore use AssetFilter.OrderBy
}
```
3. all of non-abstract classes will implement func of MarshalJSON to include "objectType" property.
```go
func (o *MediaAsset) MarshalJSON() ([]byte, error) {
	type Alias MediaAsset
	return json.Marshal(&struct {
		*Alias
		ObjectType string `json:"objectType"`
	}{
		Alias:      (*Alias)(o),
		ObjectType: "KalturaMediaAsset",
	})
}
```
4. all classes will have an interface that will contains getters for all properites in class.
   1. if class has a base class so the class's interface will contain the interface of base class.
   2. all non-abstract class will implement the interface methods
   3. if there is a diplication between getters of current class and base interface so class will implement the base interface by casting the result
```go
type FilterInterface interface {
	GetOrderBy() string
}
type PersistedFilterInterface interface {
	FilterInterface
	GetName() string
}
type AssetFilterInterface interface {
	PersistedFilterInterface
	GetDynamicOrderBy() DynamicOrderBy
}
func (f *AssetFilter) GetOrderBy() string {
	return string(f.OrderBy) // cast return value so it will fit to FilterInterface 
}
func (f *AssetFilter) GetName() string {
	return f.Name
}
func (f *AssetFilter) GetDynamicOrderBy() DynamicOrderBy {
	return f.DynamicOrderBy
}
// more methods for all other iterface methods
```
7. for class which other classed are inherit from it we will generate container that will hold all non-abstract classes that inherit from it (for all inheritence levels)
```text
for example we have this inheritance:
abstract Filter
abstract PersistedFilter : Filter
AssetFilter : PersistedFilter 
abstract BaseSearchAssetFilter : AssetFilter
ChannelFilter : BaseSearchAssetFilter

container generaretion:
FitlerContainer {AssetFilter, ChannelFilter, (will not include Filter, PersistedFilter, BaseSearchAssetFilter)}
PersistedFilterContainer {AssetFilter, ChannelFilter (will not include PersistedFilter, BaseSearchAssetFilter)}
AssetFilterContainer {AssetFilter, ChannelFilter (will not include BaseSearchAssetFilter)}
BaseSearchAssetFilterContainer { ChannelFilter (will not include BaseSearchAssetFilter) }
```
8. all Container struct will implement func of UnmarshalJSON to return specific struct
```go
func (b *AssetContainer) UnmarshalJSON(bytes []byte) error {
	var objectType ObjectType
	err := json.Unmarshal(bytes, &objectType)
	if err != nil {
		return err
	}
	switch objectType.Type {
	case "KalturaMediaAsset":
		a := &MediaAsset{}
		err = json.Unmarshal(bytes, a)
		if err != nil {
			return err
		}
		b.MediaAsset = a
	case "KalturaLiveAsset":
		a := &LiveAsset{}
		err = json.Unmarshal(bytes, a)
		if err != nil {
			return err
		}
		b.LiveAsset = a
	case "KalturaProgramAsset":
		a := &ProgramAsset{}
		err = json.Unmarshal(bytes, a)
		if err != nil {
			return err
		}
		b.ProgramAsset = a
	default:
		return errors.New("unknown type")
	}

	return nil
}
```

## Generation of services:
1. all actions first param is ctx context.Context and last param is extra ...kalturaclient.Param - between then we add the real params of the action.
2. if a param of an action in class of phoniex it will recive the interface of the class and not the class itself
3. all action return 2 objects: pointer to result of the action and error
4. if a param of an action can be null it will pass as pointer and will be set only if not nil
5. if return type of an action has a contanier so the action will return the container and not the class
### exampels for service actions:
```go
func (s *AssetService) List(ctx context.Context, filter *types.AssetFilterInterface, pager *types.FilterPager, extra ...kalturaclient.Param) 
(*types.AssetListResponse, error)

func (s *AssetService) Add(ctx context.Context, asset types.AssetInterface, extra ...kalturaclient.Param) (*types.AssetContainer, error)

func (s *MetaService) Update(ctx context.Context, id int64, meta types.MetaInterface, extra ...kalturaclient.Param) (*types.Meta, error)

func (s *MetaService) Delete(ctx context.Context, id int64, extra ...kalturaclient.Param) (*bool, error)
```