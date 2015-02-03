/// See "Visualization Library" at
/// at http://developers.google.com/maps/documentation/javascript/reference
module IntelliFactory.WebSharper.Google.Maps.Visualization

open IntelliFactory.WebSharper.InterfaceGenerator
open IntelliFactory.WebSharper.Google.Maps.Notation
module M = IntelliFactory.WebSharper.Google.Maps.Map

let MapsEngineLayerOptions =
    Config "google.maps.visualization.MapsEngineLayerOptions"
    |+> Instance [
        "accessToken" =@ T<string>
        |> WithComment "The authentication token returned by an OAuth 2.0 authentication request."

        "clickable" =@ T<bool>
        |> WithComment "If true, the layer receives mouse events. Default value is true."

        "layerId" =@ T<string>
        |> WithComment "The ID of a single Maps Engine layer to display."

        "layerKey" =@ T<string>
        |> WithComment "The key of the layer to display. Maps Engine layer keys are only unique within a single map, and can be changed by map owners."

        "map" =@ Map.Map
        |> WithComment "The map on which to display the layer."

        "mapId" =@ T<string>
        |> WithComment "The ID of the Maps Engine map that contains the layer with the given layerKey."

        "suppressInfoWindows" =@ T<bool>
        |> WithComment "Suppress the rendering of info windows when layer features are clicked."
    ]

let MapsEngineLayerProperties =
    Class "google.maps.visualization.MapsEngineLayerProperties"
    |+> Instance [
        "name" =? T<string>
    ]

let MapsEngineStatus =
    let t = Type.New()
    Class "google.maps.visualization.MapsEngineStatus"
    |=> t
    |+> Static [
        "INVALID_LAYER" =? t
        "OK" =? t
        "UNKNOWN_ERROR" =? t
    ]

let MapsEngineLayer =
    Class "google.maps.visualization.MapsEngineLayer"
    |+> Static [Constructor MapsEngineLayerOptions]
    |+> Instance [
        "getLayerId" => T<unit -> string>
        |> WithComment "Returns the ID of the Maps Engine layer being displayed, if set."

        "getLayerKey" => T<unit -> string>
        |> WithComment "Returns the key of the layer to be displayed."

        "getMap" => Map.Map

        "getMapId" => T<unit -> string>
        |> WithComment "Returns the ID of the Maps Engine map to which the layer belongs."

        "getProperties" => T<unit> ^-> MapsEngineLayerProperties
        |> WithComment "Returns properties of the Maps Engine layer, which are available once the layer has loaded."

        "getStatus" => T<unit> ^-> MapsEngineStatus
        |> WithComment "Returns the status of the layer, which is available once the requested layer has loaded."

        "setLayerId" => T<string -> unit>
        |> WithComment "Sets the ID of a single Maps Engine layer to display. Changing this value will cause the layer to be redrawn."

        "setLayerKey" => T<string -> unit>
        |> WithComment "Sets the key of the layer to be displayed. Maps Engine layer keys are only unique within a single map, and can be changed by map owners. Changing this value will cause the layer to be redrawn."

        "setMap" => Map.Map ^-> T<unit>

        "setMapId" => T<string -> unit>
        |> WithComment "Sets the ID of the Maps Engine map that contains the layer with the given layerKey. Changing this value will cause the layer to be redrawn."

        "setOptions" => MapsEngineLayerOptions ^-> T<unit>
    ]

let FeatureStyle =
    Class "google.maps.visualization.FeatureStyle"
    |+> Instance [
        "reset" => T<string -> unit>
        |> WithComment "Resets the given style property to its original value."

        "resetAll" => T<unit -> unit>
        |> WithComment "Resets all style properties to their original values."

        "fillColor" =@ T<string>
        |> WithComment "The feature's fill color. All CSS3 colors are supported except for extended named colors."

        "fillOpacity" =@ T<string>
        |> WithComment "Fill opacity, expressed as a decimal between 0 and 1 inclusive. This property may be set as a number, but it will always be returned as a string."

        "iconAnchor" =@ T<string>
        |> WithComment "The icon's anchor point is the pixel in the source image that is aligned with the point's geographical location, expressed as a whitespace-separated pair of numbers: x y. Defaults to the center of the icon."

        "iconClip" =@ T<string>
        |> WithComment "The rectangular region of the icon's image (in image pixel coordinates) to use, as a whitespace-separated 4-tuple of numbers: x y width height. For example, to use a 32x32 icon situated at (0, 64) in a sprite sheet, specify 0 64 32 32."

        "iconImage" =@ T<string>
        |> WithComment "The image to render at the point. Currently, only url(...) is supported."

        "iconOpacity" =@ T<string>
        |> WithComment "Icon opacity, expressed as a decimal between 0 and 1 inclusive. This property may be set as a number, but it will always be returned as a string."

        "iconSize" =@ T<string>
        |> WithComment "Icon size, expressed as a string with two measurements (with pixel or percentage as unit) separated by whitespace."

        "strokeColor" =@ T<string>
        |> WithComment "The feature's stroke color. All CSS3 colors are supported except for extended named colors."

        "strokeOpacity" =@ T<string>
        |> WithComment "Stroke opacity, expressed as a decimal between 0 and 1 inclusive. This property may be set as a number, but it will always be returned as a string."

        "strokeWidth" =@ T<string>
        |> WithComment "Stroke width in pixels. This property may be set as a number, but it will always be returned as a string."

        "zIndex" =@ T<string>
        |> WithComment "Rendering order. Features with greater zIndex are rendered on top."
    ]

let DynamicMapsEngineLayer =
    Class "google.maps.visualization.DynamicMapsEngineLayer"
    |=> Inherits MVC.MVCObject
    |+> Instance [
        "getFeatureStyle" => T<string> ^-> FeatureStyle
        |> WithComment "Returns the style for the given feature, with which individual style properties can be retrieved or set."

        "getLayerId" => T<unit -> string>
        |> WithComment "Returns the ID of the Maps Engine layer being displayed, if set."

        "getLayerKey" => T<unit -> string>
        |> WithComment "Returns the key of the layer to be displayed."

        "getMap" => T<unit> ^-> Map.Map

        "getMapId" => T<unit -> string>
        |> WithComment "Returns the ID of the Maps Engine map to which the layer belongs."

        "getStatus" => T<unit> ^-> MapsEngineStatus
        |> WithComment "Returns the status of the layer, set once the requested layer has loaded."

        "setLayerId" => T<string -> unit>
        |> WithComment "Sets the ID of a single Maps Engine layer to display."

        "setLayerKey" => T<string -> unit>
        |> WithComment "Sets the key of the layer to be displayed. Maps Engine Layer Keys are only unique within a single map, and can be changed by map owners. Changing this value will cause the layer to be redrawn."

        "setMap" => Map.Map ^-> T<unit>

        "setMapId" => T<string -> unit>
        |> WithComment "Sets the ID of the Maps Engine map to which the layer belongs. Changing this value will cause the layer to be redrawn."

        "setOptions" => MapsEngineLayerOptions ^-> T<unit>
    ]

let WeightedLocation =
    Class "google.maps.visualization.WeightedLocation"
    |+> Instance [
        "location" =@ Base.LatLng
        "weight" =@ T<float>
    ]

let private HeatmapLayerProperties =
    [
        "dissipating", T<bool>
        "gradient", Type.ArrayOf T<string>
        "map", M.Map.Type
        "maxIntensity", T<float>
        "opacity", T<float>
        "radius", T<int>
    ]

let HeatmapLayerOptions =
    Pattern.Config "HeatmapLayerOptions" {
        Required = ["data", MVC.MVCArray.[Base.LatLng]]
        Optional = HeatmapLayerProperties
    }

let HeatmapLayer =
    let loc = Base.LatLng + WeightedLocation
    Class "google.maps.visualization.HeatmapLayer"
    |=> Inherits MVC.MVCObject
    |+> Instance [

            "getData" => T<unit> ^-> MVC.MVCArray.[Base.LatLng]
            |> WithSourceName "GetData"
            |> WithComment "Returns the data points currently displayed by this heatmap."

            "getData" => T<unit> ^-> MVC.MVCArray.[WeightedLocation]
            |> WithSourceName "GetWeightedData"
            |> WithComment "Returns the data points currently displayed by this heatmap."

            "getMap" => T<unit> ^-> M.Map

            "setData" => (MVC.MVCArray.[loc] + Type.ArrayOf loc) ^-> T<unit>
            |> WithComment "Sets the data points to be displayed by this heatmap."

            "setMap" => M.Map ^-> T<unit>

            "data" =@ MVC.MVCArray.[Base.LatLng]

        ]
    |+> Instance [
            for (k, v) in HeatmapLayerProperties do
                yield (k =@ v) :> _
        ]
    |+> Static [
            Ctor [!? HeatmapLayerOptions?options]
        ]

let DemographicsQuery =
    Config "google.maps.visualization.DemographicsQuery"
    |+> Instance [
        "from" =@ T<string>
        |> WithComment "The data set to display."

        "where" =@ T<string>
        |> WithComment "Expression to define the regions to show."
    ]

let DemographicsPropertyStyle =
    Config "google.maps.visualization.DemographicsPropertyStyle"
    |+> Instance [
        "expression" =@ T<string>
        |> WithComment "The expression used to determine the value that determines how to style a region."

        "gradient" =@ T<string[]>
        |> WithComment "The sequence of colors that are mapped to the range of values for selected regions."

        "max" =@ T<float>
        |> WithComment "The maximum of the range of expression values across which the gradient of colors is mapped."

        "min" =@ T<float>
        |> WithComment "The minimum of the range of expression values across which the gradient of colors is mapped."
    ]

let DemographicsPolygonOptions =
    Config "google.maps.visualization.DemographicsPolygonOptions"
    |+> Instance [
        "fillColor" =@ T<string>
        |> WithComment "The fill color, defined by a six-digit hexadecimal number in #RRGGBB format (e.g. #00AAFF)."

        "fillColorStyle" =@ DemographicsPropertyStyle

        "fillOpacity" =@ T<float>
        |> WithComment "The fill opacity between 0.0 and 1.0."

        "fillOpacityStyle" =@ DemographicsPropertyStyle

        "strokeColor" =@ T<string>
        |> WithComment "The fill color, defined by a six-digit hexadecimal number in #RRGGBB format (e.g. #00AAFF)."

        "strokeColorStyle" =@ DemographicsPropertyStyle

        "strokeOpacity" =@ T<float>
        |> WithComment "The stroke opacity between 0.0 and 1.0."

        "strokeOpacityStyle" =@ DemographicsPropertyStyle

        "strokeWeight" =@ T<int>
        |> WithComment "The stroke width in pixels, between 0 and 10."
    ]

let DemographicsStyle =
    Config "google.maps.visualization.DemographicsStyle"
    |+> Instance [
        "polygonOptions" =@ DemographicsPolygonOptions
        |> WithComment "Defines the styling applied to the region polygons selected by this styling rule."

        "where" =@ T<string>
        |> WithComment "Expression that defines the regions to which this style rule applies."
    ]

let DemographicsLayerOptions =
    Config "google.maps.visualization.DemographicsLayerOptions"
    |+> Instance [
        "clickable" =@ T<bool>
        |> WithComment "If true, the layer receives mouse events. Defaults to true."

        "map" =@ Map.Map

        "query" =@ DemographicsQuery
        |> WithComment "Specifies which regions to show on the map."

        "style" =@ Type.ArrayOf DemographicsStyle
        |> WithComment "Specifies how to style the regions matched."

        "suppressInfoWindows" =@ T<string>
        |> WithComment "Suppress the rendering of info windows when layer features are clicked."
    ]

let DemographicsLayer =
    Class "google.maps.visualization.DemographicsLayer"
    |+> Static [Constructor DemographicsLayerOptions]
    |+> Instance [
        "getMap" => T<unit> ^-> Map.Map

        "getQuery" => T<unit> ^-> DemographicsQuery

        "getStyle" => T<unit> ^-> Type.ArrayOf DemographicsStyle

        "setMap" => Map.Map ^-> T<unit>

        "setOptions" => DemographicsLayerOptions ^-> T<unit>

        "setQuery" => DemographicsQuery ^-> T<unit>

        "setStyle" => Type.ArrayOf DemographicsStyle ^-> T<unit>
    ]
