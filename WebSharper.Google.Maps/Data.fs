// $begin{copyright}
//
// This file is part of WebSharper
//
// Copyright (c) 2008-2024 IntelliFactory
//
// Licensed under the Apache License, Version 2.0 (the "License"); you
// may not use this file except in compliance with the License.  You may
// obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or
// implied.  See the License for the specific language governing
// permissions and limitations under the License.
//
// $end{copyright}
// Definitions for the Map part of the API. See:
// http://developers.google.com/maps/documentation/javascript/reference
namespace WebSharper.Google.Maps.Definition

module DataForward =

    open WebSharper.InterfaceGenerator

    let Feature = Class "google.maps.Data.Feature"


module Data =

    open WebSharper.InterfaceGenerator
    open Notation
    open Specification
    module M = WebSharper.Google.Maps.Definition.Map

    let MouseEvent =
        Interface "google.maps.Data.MouseEvent"
        |=> Extends [M.MapMouseEvent]
        |+> [
            "feature" =@ DataForward.Feature
            |> WithComment "The feature which generated the mouse event."
        ]

    let AddFeatureEvent =
        Interface "google.maps.Data.AddFeatureEvent"
        |+> [
            "feature" =@ DataForward.Feature
            |> WithComment "The feature that was added to the FeatureCollection."
        ]

    let RemoveFeatureEvent =
        Interface "google.maps.Data.RemoveFeatureEvent"
        |+> [
            "feature" =@ DataForward.Feature
            |> WithComment "The feature that was removed from the FeatureCollection."
        ]

    let Geometry =
        Interface "google.maps.Data.Geometry"
        |+> [
            "forEachLatLng" => (Base.LatLng ^-> T<unit>) ^-> T<unit>
            |> WithComment "Repeatedly invokes the given function, passing a point from the geometry to the function on each invocation."

            "getType" => T<unit> ^-> T<string>
            |> WithComment "Returns the type of the geometry object. Possibilities are \"Point\", \"MultiPoint\", \"LineString\", \"MultiLineString\", \"LinearRing\", \"Polygon\", \"MultiPolygon\", or \"GeometryCollection\"."
        ]

    let SetGeometryEvent =
        Interface "google.maps.Data.SetGeometryEvent"
        |+> [
            "feature" =@ DataForward.Feature
            |> WithComment "The feature whose geometry was set."

            "newGeometry" =@ Geometry
            |> WithComment "The new feature geometry."

            "oldGeometry" =@ Geometry
            |> WithComment "The previous feature geometry."
        ]

    let SetPropertyEvent =
        Interface "google.maps.Data.SetPropertyEvent"
        |+> [
            "feature" =@ DataForward.Feature
            |> WithComment "The feature whose geometry was set."

            "name" =@ T<string>
            |> WithComment "The property name."

            "newValue" =@ T<obj>
            |> WithComment "The new value."

            "oldValue" =@ T<obj>
            |> WithComment "The previous value. Will be undefined if the property was added."
        ]

    let RemovePropertyEvent =
        Interface "google.maps.Data.RemovePropertyEvent"
        |+> [
            "feature" =@ DataForward.Feature
            |> WithComment "The feature whose geometry was set."

            "name" =@ T<string>
            |> WithComment "The property name."

            "oldValue" =@ T<obj>
            |> WithComment "The previous value."
        ]

    let Point =
        Class "google.maps.Data.Point"
        |=> Implements [Geometry]
        |+> Static [
            Ctor [
                Base.LatLng + Base.LatLngLiteral
            ]
            |> WithComment "Constructs a Data.Point from the given LatLng or LatLngLiteral."
        ]
        |+> Instance [
            // "forEachLatLng" => (Base.LatLng ^-> T<unit>) ^-> T<unit>

            "get" => T<unit> ^-> Base.LatLng
            |> WithComment "Returns the contained LatLng."

            // "getType" => T<unit> ^-> T<string>
            // |> WithComment "Returns the string \"Point\"."
        ]

    let MultiPoint =
        Class "google.maps.Data.MultiPoint"
        |=> Implements [Geometry]
        |+> Static [
            Ctor [
                Type.ArrayOf (Base.LatLng + Base.LatLngLiteral)
            ]
            |> WithComment "Constructs a Data.MultiPoint from the given LatLngs or LatLngLiterals."
        ]
        |+> Instance [
            // "forEachLatLng" => (Base.LatLng ^-> T<unit>) ^-> T<unit>

            "getArray" => T<unit> ^-> Type.ArrayOf Base.LatLng
            |> WithComment "Returns an array of the contained LatLngs. A new array is returned each time getArray() is called."

            "getAt" => T<int> ^-> Base.LatLng
            |> WithComment "Returns the n-th contained LatLng."

            "getLength" => T<unit> ^-> T<int>
            |> WithComment "Returns the number of contained LatLngs."

            // "getType" => T<unit> ^-> T<string>
            // |> WithComment "Returns the string \"MultiPoint\"."
        ]

    let FeatureOptions =
        Config "google.maps.Data.FeatureOptions"
        |+> Instance [
            "geometry" =@ Geometry + Base.LatLng + Base.LatLngLiteral
            |> WithComment "The feature geometry. If none is specified when a feature is constructed, the feature's geometry will be null. If a LatLng object or LatLngLiteral is given, this will be converted to a Data.Point geometry."
            "id" =@ T<int64> + T<string>
            |> WithComment "Feature ID is optional. If provided, it can be used to look up the feature in a Data object using the getFeatureById() method. Note that a feature's ID cannot be subsequently changed."

            "properties" =@ Object
            |> WithComment "The feature properties. This is an arbitrary mapping of property names to values."
        ]

    let Feature =
        DataForward.Feature
        |+> Static [
            Ctor [
                !? FeatureOptions?options
            ]
            |> WithComment "Constructs a Feature with the given options."
        ]
        |+> Instance [
            "forEachProperty" => (T<obj> * T<string> ^-> T<unit>) ^-> T<unit>
            |> WithComment "Repeatedly invokes the given function, passing a property value and name on each invocation. The order of iteration through the properties is undefined."

            "getGeometry" => T<unit> ^-> Geometry
            |> WithComment "Returns the feature's geometry."

            "getId" => T<unit> ^-> T<int64> + T<string>
            |> WithComment "Returns the feature ID."

            "getProperty" => T<string> ^-> T<obj>
            |> WithComment "Returns the value of the requested property, or undefined if the property does not exist."

            "removeProperty" => T<string> ^-> T<unit>
            |> WithComment "Removes the property with the given name."

            "setGeometry" => Geometry + Base.LatLng + Base.LatLngLiteral ^-> T<unit>
            |> WithComment "Sets the feature's geometry."

            "setProperty" => T<string> * T<obj> ^-> T<unit>
            |> WithComment "Sets the value of the specified property. If newValue is undefined this is equivalent to calling removeProperty."

            "toGeoJson" => (T<obj> ^-> T<unit>) ^-> T<unit>
            |> WithComment "Exports the feature to a GeoJSON object."

            // EVENTS
            "removeproperty" =@ T<obj> -* RemovePropertyEvent ^-> T<unit>
            |> WithComment "This event is triggered when a feature's property is removed."

            "setgeometry" =@ T<obj> -* SetGeometryEvent ^-> T<unit>
            |> WithComment "This event is triggered when a feature's geometry is set."

            "setproperty" =@ T<obj> -* SetPropertyEvent ^-> T<unit>
            |> WithComment "This event is triggered when a feature's property is set."
        ]

    let StyleOptions =
        Config "google.maps.Data.StyleOptions"
        |+> Instance [
            "animation" =@ Specification.Animation
            |> WithComment "The animation to play when marker is added to a map. Only applies to point geometries."

            "clickable" =@ T<bool>
            |> WithComment "If true, the marker receives mouse and touch events. Default: true."

            "cursor" =@ T<bool>
            |> WithComment "Mouse cursor to show on hover. Only applies to point geometries."

            "draggable" =@ T<bool>
            |> WithComment "If true, the object can be dragged across the map and the underlying feature will have its geometry updated. Default: false."

            "editable" =@ T<bool>
            |> WithComment "If true, the object can be edited by dragging control points and the underlying feature will have its geometry updated. Only applies to LineString and Polygon geometries. Default: false."

            "fillColor" =@ T<string>
            |> WithComment "The fill color. All CSS3 colors are supported except for extended named colors. Only applies to polygon geometries."

            "fillOpacity" =@ T<float>
            |> WithComment "The fill opacity between 0.0 and 1.0. Only applies to polygon geometries."

            "icon" =@ T<string> + Icon + Symbol
            |> WithComment "Icon for the foreground. If a string is provided, it is treated as though it were an Icon with the string as url. Only applies to point geometries."

            "icons" =@ Type.ArrayOf IconSequence
            |> WithComment "The icons to be rendered along a polyline. Only applies to line geometries."

            "label" =@ T<string> + MarkerLabel
            |> WithComment "Adds a label to the marker. The label can either be a string, or a MarkerLabel object. Only applies to point geometries."

            "opacity" =@ T<float>
            |> WithComment "The marker's opacity between 0.0 and 1.0. Only applies to point geometries."

            "shape" =@ MarkerShape
            |> WithComment "Defines the image map used for hit detection. Only applies to point geometries."

            "strokeColor" =@ T<string>
            |> WithComment "The stroke color. All CSS3 colors are supported except for extended named colors. Only applies to line and polygon geometries."

            "strokeOpacity" =@ T<float>
            |> WithComment "The stroke opacity between 0.0 and 1.0. Only applies to line and polygon geometries."

            "strokeWeight" =@ T<int>
            |> WithComment "The stroke width in pixels. Only applies to line and polygon geometries."

            "title" =@ T<string>
            |> WithComment "Rollover text. Only applies to point geometries."

            "visible" =@ T<bool>
            |> WithComment "Whether the feature is visible."

            "zIndex" =@ T<int>
            |> WithComment "All features are displayed on the map in order of their zIndex, with higher values displaying in front of features with lower values. Markers are always displayed in front of line-strings and polygons."
        ]

    //TODO how to add a namespace to a typeDef? "google.maps.Data.StylingFunction"
    let StylingFunction = T<obj> -* Feature ^-> StyleOptions

    let DataOptions =
        Config "google.maps.Data.DataOptions"
        |+> Instance [
            "map" =@ M.Map
            |> WithComment "Map on which to display the features in the collection."

            "controlPosition" =@ Controls.ControlPosition
            |> WithComment "The position of the drawing controls on the map. Default: ControlPosition.TOP_LEFT."

            "controls" =@ T<string[]>
            |> WithComment "Describes which drawing modes are available for the user to select, in the order they are displayed. This should not include the null drawing mode, which is added by default. If null, drawing controls are disabled and not displayed. Possible drawing modes are \"Point\", \"LineString\" or \"Polygon\"."

            "drawingMode" =@ T<string>
            |> WithComment "The current drawing mode of the given Data layer. A drawing mode of null means that the user can interact with the map as normal, and clicks do not draw anything. Possible drawing modes are null, \"Point\", \"LineString\" or \"Polygon\"."

            "featureFactory" =@ T<obj> -* Geometry ^-> Feature
            |> WithComment "When drawing is enabled and a user draws a Geometry (a Point, Line String or Polygon), this function is called with that Geometry and should return a Feature that is to be added to the Data layer. If a featureFactory is not supplied, a Feature with no id and no properties will be created from that Geometry instead. Defaults to null."

            "style" =@ StylingFunction + StyleOptions
            |> WithComment "Style for all features in the collection. For more details, see the setStyle() method above."
        ]

    let GeoJsonOptions =
        Config "google.maps.Data.GeoJsonOptions"
        |+> Instance [
            "idPropertyName" =@ T<string>
            |> WithComment "The name of the Feature property to use as the feature ID. If not specified, the GeoJSON Feature id will be used."
        ]

    let Data =
        Forward.Data
        |=> Inherits MVC.MVCObject
        |+> Static [
            Ctor [
                !? DataOptions?options
            ]
            |> WithComment "Creates an empty collection, with the given DataOptions."
        ]
        |+> Instance [
            "add" => !? (Feature + FeatureOptions)?feature ^-> Feature
            |> WithComment """Adds a feature to the collection, and returns the added feature.

If the feature has an ID, it will replace any existing feature in the collection with the same ID. If no feature is given, a new feature will be created with null geometry and no properties. If FeatureOptions are given, a new feature will be created with the specified properties.

Note that the IDs 1234 and '1234' are equivalent. Adding a feature with ID 1234 will replace a feature with ID '1234', and vice versa."""

            "addGeoJson" => Object * !? GeoJsonOptions ^-> Type.ArrayOf Feature
            |> WithComment "Adds GeoJSON features to the collection. Give this method a parsed JSON. The imported features are returned. Throws an exception if the GeoJSON could not be imported."

            "contains" => Feature ^-> T<bool>
            |> WithComment "Checks whether the given feature is in the collection."

            "forEach" => (Feature ^-> T<unit>) ^-> T<unit>
            |> WithComment "Repeatedly invokes the given function, passing a feature in the collection to the function on each invocation. The order of iteration through the features is undefined."

            "getControlPosition" => T<unit> ^-> Type.ArrayOf T<string>
            |> WithComment "Returns which drawing modes are available for the user to select, in the order they are displayed. This does not include the null drawing mode, which is added by default. Possible drawing modes are \"Point\", \"LineString\" or \"Polygon\"."

            "getDrawingMode" => T<unit> ^-> T<string>
            |> WithComment "Returns the current drawing mode of the given Data layer. A drawing mode of null means that the user can interact with the map as normal, and clicks do not draw anything. Possible drawing modes are null, \"Point\", \"LineString\" or \"Polygon\"."

            "getFeatureById" => T<int64> + T<string> ^-> Feature
            |> WithComment """Returns the feature with the given ID, if it exists in the collection. Otherwise returns undefined.

Note that the IDs 1234 and '1234' are equivalent. Either can be used to look up the same feature."""

            "getMap" => T<unit> ^-> M.Map
            |> WithComment "Returns the map on which the features are displayed."

            "getStyle" => T<unit> ^-> StylingFunction + StyleOptions
            |> WithComment "Gets the style for all features in the collection."

            "loadGeoJson" => T<string> * !? GeoJsonOptions * !? (Type.ArrayOf Feature ^-> T<unit>) ^-> T<unit>
            |> WithComment """Loads GeoJSON from a URL, and adds the features to the collection.

NOTE: The GeoJSON is fetched using XHR, and may not work cross-domain. If you have issues, we recommend you fetch the GeoJSON using your choice of AJAX library, and then call addGeoJson()."""

            "overrideStyle" => Feature * StyleOptions ^-> T<unit>
            |> WithComment "Changes the style of a feature. These changes are applied on top of the style specified by setStyle(). Style properties set to null revert to the value specified via setStyle()."

            "remove" => Feature ^-> T<unit>
            |> WithComment "Removes a feature from the collection."

            "revertStyle" => !? Feature ^-> T<unit>
            |> WithComment """Removes the effect of previous overrideStyle() calls. The style of the given feature reverts to the style specified by setStyle().

If no feature is given, all features have their style reverted."""

            "setControlPosition" => Controls.ControlPosition ^-> T<unit>
            |> WithComment "Sets the position of the drawing controls on the map."

            "setControls" => T<string[]> ^-> T<unit>
            |> WithComment "Sets which drawing modes are available for the user to select, in the order they are displayed. This should not include the null drawing mode, which is added by default. If null, drawing controls are disabled and not displayed. Possible drawing modes are \"Point\", \"LineString\" or \"Polygon\"."

            "setDrawingMode" => !? T<string> ^-> T<unit>
            |> WithComment "Sets the current drawing mode of the given Data layer. A drawing mode of null means that the user can interact with the map as normal, and clicks do not draw anything. Possible drawing modes are null, \"Point\", \"LineString\" or \"Polygon\"."

            "setMap" => M.Map ^-> T<unit>
            |> WithComment "Renders the features on the specified map. If map is set to null, the features will be removed from the map."

            "setStyle" => StylingFunction + StyleOptions ^-> T<unit>
            |> WithComment """Sets the style for all features in the collection. Styles specified on a per-feature basis via overrideStyle() continue to apply.

Pass either an object with the desired style options, or a function that computes the style for each feature. The function will be called every time a feature's properties are updated."""

            "toGeoJson" => (Object ^-> T<unit>) ^-> T<unit>
            |> WithComment "Exports the features in the collection to a GeoJSON object."

            // EVENTS
            "addfeature" =@ T<obj> -* AddFeatureEvent ^-> T<unit>
            |> WithComment "This event is fired when a feature is added to the collection."

            "click" =@ T<obj> -* MouseEvent ^-> T<unit>
            |> WithComment "This event is fired for a click on the geometry."

            "contextmenu" =@ T<obj> -* MouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM contextmenu event is fired on the geometry."

            "dblclick" =@ T<obj> -* MouseEvent ^-> T<unit>
            |> WithComment "This event is fired for a double click on the geometry."

            "mousedown" =@ T<obj> -* MouseEvent ^-> T<unit>
            |> WithComment "This event is fired for a mousedown on the geometry."

            "mouseout" =@ T<obj> -* MouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the mouse leaves the area of the geometry."

            "mouseover" =@ T<obj> -* MouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the mouse enters the area of the geometry."

            "mouseup" =@ T<obj> -* MouseEvent ^-> T<unit>
            |> WithComment "This event is fired for a mouseup on the geometry."

            "removefeature" =@ T<obj> -* RemoveFeatureEvent ^-> T<unit>
            |> WithComment "This event is fired when a feature is removed from the collection."

            "removeproperty" =@ T<obj> -* RemovePropertyEvent ^-> T<unit>
            |> WithComment "This event is fired when a feature's property is removed."

            "setgeometry" =@ T<obj> -* SetGeometryEvent ^-> T<unit>
            |> WithComment "This event is fired when a feature's geometry is set."

            "setproperty" =@ T<obj> -* SetPropertyEvent ^-> T<unit>
            |> WithComment "This event is fired when a feature's property is set."

            "rightclick" =@ T<obj> -* MouseEvent ^-> T<unit>
            |> WithComment "This event is fired for a rightclick on the geometry."
            |> ObsoleteWithMessage "Deprecated: Use the Data.contextmenu event instead in order to support usage patterns like control-click on macOS."
        ]

    let LineString =
        Class "google.maps.Data.LineString"
        |=> Implements [Geometry]
        |+> Static [
            Ctor [
                Type.ArrayOf (Base.LatLng + Base.LatLngLiteral)
            ]
            |> WithComment "Constructs a Data.LineString from the given LatLngs or LatLngLiterals."
        ]
        |+> Instance [
            // "forEachLatLng" => (Base.LatLng ^-> T<unit>) ^-> T<unit>

            "getArray" => T<unit> ^-> Type.ArrayOf Base.LatLng
            |> WithComment "Returns an array of the contained LatLngs. A new array is returned each time getArray() is called."

            "getAt" => T<int> ^-> Base.LatLng
            |> WithComment "Returns the n-th contained LatLng."

            "getLength" => T<unit> ^-> T<int>
            |> WithComment "Returns the number of contained LatLngs."

            // "getType" => T<unit> ^-> T<string>
            // |> WithComment "Returns the string \"LineString\"."
        ]

    let MultiLineString =
        Class "google.maps.Data.MultiLineString"
        |=> Implements [Geometry]
        |+> Static [
            Ctor [
                Type.ArrayOf LineString + Type.ArrayOf (Base.LatLng + Base.LatLngLiteral)
            ]
            |> WithComment "Constructs a Data.MultiLineString from the given Data.LineStrings or arrays of positions."
        ]
        |+> Instance [
            // "forEachLatLng" => (Base.LatLng ^-> T<unit>) ^-> T<unit>

            "getArray" => T<unit> ^-> Type.ArrayOf LineString
            |> WithComment "Returns an array of the contained Data.LineStrings. A new array is returned each time getArray() is called."

            "getAt" => T<int> ^-> LineString
            |> WithComment "Returns the n-th contained Data.LineString."

            "getLength" => T<unit> ^-> T<int>
            |> WithComment "Returns the number of contained Data.LineStrings."

            // "getType" => T<unit> ^-> T<string>
            // |> WithComment "Returns the string \"MultiLineString\"."
        ]

    let LinearRing =
        Class "google.maps.Data.LinearRing"
        |=> Implements [Geometry]
        |+> Static [
            Ctor [
                Type.ArrayOf (Base.LatLng + Base.LatLngLiteral)
            ]
            |> WithComment "Constructs a Data.LinearRing from the given LatLngs or LatLngLiterals."
        ]
        |+> Instance [
            // "forEachLatLng" => (Base.LatLng ^-> T<unit>) ^-> T<unit>

            "getArray" => T<unit> ^-> Type.ArrayOf Base.LatLng
            |> WithComment "Returns an array of the contained LatLngs. A new array is returned each time getArray() is called."

            "getAt" => T<int> ^-> Base.LatLng
            |> WithComment "Returns the n-th contained LatLng."

            "getLength" => T<unit> ^-> T<int>
            |> WithComment "Returns the number of contained LatLngs."

            // "getType" => T<unit> ^-> T<string>
            // |> WithComment "Returns the string \"LinearRing\"."
        ]

    let Polygon =
        Class "google.maps.Data.Polygon"
        |=> Implements [Geometry]
        |+> Static [
            Ctor [
                Type.ArrayOf LinearRing + Type.ArrayOf (Base.LatLng + Base.LatLngLiteral)
            ]
            |> WithComment "Constructs a Data.Polygon from the given Data.LinearRings or arrays of positions."
        ]
        |+> Instance [
            // "forEachLatLng" => (Base.LatLng ^-> T<unit>) ^-> T<unit>

            "getArray" => T<unit> ^-> Type.ArrayOf LinearRing
            |> WithComment "Returns an array of the contained Data.LinearRings. A new array is returned each time getArray() is called."

            "getAt" => T<int> ^-> LinearRing
            |> WithComment "Returns the n-th contained Data.LinearRing."

            "getLength" => T<unit> ^-> T<int>
            |> WithComment "Returns the number of contained Data.LinearRings."

            // "getType" => T<unit> ^-> T<string>
            // |> WithComment "Returns the string \"Polygon\"."
        ]

    let MultiPolygon =
        Class "google.maps.Data.MultiPolygon"
        |=> Implements [Geometry]
        |+> Static [
            Ctor [
                Type.ArrayOf Polygon + Type.ArrayOf LinearRing + Type.ArrayOf (Base.LatLng + Base.LatLngLiteral)
            ]
            |> WithComment "Constructs a Data.MultiPolygon from the given Data.Polygons or arrays of positions."
        ]
        |+> Instance [
            // "forEachLatLng" => (Base.LatLng ^-> T<unit>) ^-> T<unit>

            "getArray" => T<unit> ^-> Type.ArrayOf Polygon
            |> WithComment "Returns an array of the contained Data.Polygons. A new array is returned each time getArray() is called."

            "getAt" => T<int> ^-> Polygon
            |> WithComment "Returns the n-th contained Data.Polygon."

            "getLength" => T<unit> ^-> T<int>
            |> WithComment "Returns the number of contained Data.Polygons."

            // "getType" => T<unit> ^-> T<string>
            // |> WithComment "Returns the string \"MultiPolygon\"."
        ]

    let GeometryCollection =
        Class "google.maps.Data.GeometryCollection"
        |=> Implements [Geometry]
        |+> Static [
            Ctor [
                Type.ArrayOf Geometry + Type.ArrayOf (Base.LatLng + Base.LatLngLiteral)
            ]
            |> WithComment "Constructs a Data.GeometryCollection from the given geometry objects or LatLngs."
        ]
        |+> Instance [
            // "forEachLatLng" => (Base.LatLng ^-> T<unit>) ^-> T<unit>

            "getArray" => T<unit> ^-> Type.ArrayOf LinearRing
            |> WithComment "Returns an array of the contained geometry objects. A new array is returned each time getArray() is called."

            "getAt" => T<int> ^-> Geometry
            |> WithComment "Returns the n-th contained geometry object."

            "getLength" => T<unit> ^-> T<int>
            |> WithComment "Returns the number of contained geometry objects."

            // "getType" => T<unit> ^-> T<string>
            // |> WithComment "Returns the string \"GeometryCollection\"."
        ]
