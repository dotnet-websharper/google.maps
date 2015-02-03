/// Definitions for the Map Types part of the API. See:
/// http://developers.google.com/maps/documentation/javascript/reference
///
/// TODO: this needs to be upgraded to the latest API.
module IntelliFactory.WebSharper.Google.Maps.MapTypes

open IntelliFactory.WebSharper.InterfaceGenerator
open IntelliFactory.WebSharper.Google.Maps.Notation

let Projection =
    Class "google.maps.Projection"
    |+> Static [Constructor T<unit>]
    |+> Instance [
        "fromLatLngToPoint" =@ Base.LatLng * !? Base.Point ^-> Base.Point
        |> WithComment "Translates from the LatLng cylinder to the Point plane. This interface specifies a function which implements translation from given LatLng values to world coordinates on the map projection. The Maps API calls this method when it needs to plot locations on screen. Projection objects must implement this method."

        "fromPointToLatLng" =@ Base.Point * !? T<bool> ^-> Base.LatLng
        |> WithComment "This interface specifies a function which implements translation from world coordinates on a map projection to LatLng values. The Maps API calls this method when it needs to translate actions on screen to positions on the map. Projection objects must implement this method."
    ]

let Visibility =
    Pattern.EnumStrings "Visibility" ["on"; "off"; "simplified"]

let MapTypeStyler =
    Config "MapTypeStyler"
    |+> Static [Constructor T<unit>]
    |+> Instance [
        "color" =@ T<string>
        |> WithComment "Sets the color of the feature. Valid values: An RGB hex string, i.e. '#ff0000'."

        "gamma" =@ T<float>
        |> WithComment "Modifies the gamma by raising the lightness to the given power. Valid values: Floating point numbers, [0.01, 10], with 1.0 representing no change."

        "hue" =@ T<string>
        |> WithComment "Sets the hue of the feature to match the hue of the color supplied. Note that the saturation and lightness of the feature is conserved, which means that the feature will not match the color supplied exactly. Valid values: An RGB hex string, i.e. '#ff0000'."

        "invert_lightness" =@ T<bool>
        |> WithComment "A value of true will invert the lightness of the feature while preserving the hue and saturation."

        "lightness" =@ T<float>
        |> WithComment "Shifts lightness of colors by a percentage of the original value if decreasing and a percentage of the remaining value if increasing. Valid values: [-100, 100]."

        "saturation" =@ T<float>
        |> WithComment "Shifts the saturation of colors by a percentage of the original value if decreasing and a percentage of the remaining value if increasing. Valid values: [-100, 100]."

        "visibility" =@ Visibility
        |> WithComment "Sets the visibility of the feature. Valid values: 'on', 'off' or 'simplifed'."

        "weight" =@ T<int>
        |> WithComment "Sets the weight of the feature, in pixels. Valid values: Integers greater than or equal to zero."
    ]

let MapTypeStyleFeatureType =
    Pattern.EnumStrings "MapTypeStyleFeatureType" [
        // Apply the rule to administrative areas.
        "administrative"
        // Apply the rule to countries.
        "administrative.country"
        // Apply the rule to land parcels.
        "administrative.land_parcel"
        // Apply the rule to localities.
        "administrative.locality"
        // Apply the rule to neighborhoods.
        "administrative.neighborhood"
        // Apply the rule to provinces.
        "administrative.province"
        // Apply the rule to all selector types.
        "all"
        // Apply the rule to landscapes.
        "landscape"
        // Apply the rule to man made structures.
        "landscape.man_made"
        // Apply the rule to natural features.
        "landscape.natural"
        // Apply the rule to points of interest.
        "poi"
        // Apply the rule to attractions for tourists.
        "poi.attraction"
        // Apply the rule to businesses.
        "poi.business"
        // Apply the rule to government buildings.
        "poi.government"
        // Apply the rule to emergency services (hospitals, pharmacies, police, doctors, etc).
        "poi.medical"
        // Apply the rule to parks.
        "poi.park"
        // Apply the rule to places of worship, such as church, temple, or mosque.
        "poi.place_of_worship"
        // Apply the rule to schools.
        "poi.school"
        // Apply the rule to sports complexes.
        "poi.sports_complex"
        // Apply the rule to all roads.
        "road"
        // Apply the rule to arterial roads.
        "road.arterial"
        // Apply the rule to highways.
        "road.highway"
        // Apply the rule to local roads.
        "road.local"
        // Apply the rule to all transit stations and lines.
        "transit"
        // Apply the rule to transit lines.
        "transit.line"
        // Apply the rule to all transit stations.
        "transit.station"
        // Apply the rule to airports.
        "transit.station.airport"
        // Apply the rule to bus stops.
        "transit.station.bus"
        // Apply the rule to rail stations.
        "transit.station.rail"
        // Apply the rule to bodies of water.
        "water"
    ]

let MapTypeStyleElementType =
    Pattern.EnumStrings "MapStyleElementType" [
        // Apply the rule to all elements of the specified feature.
        "all"
        // Apply the rule to the feature's geometry.
        "geometry"
        "geometry.fill"
        "geometry.stroke"
        // Apply the rule to the feature's labels.
        "labels"
        "labels.icon"
        "labels.text"
        "labels.text.fill"
        "labels.text.stroke"
    ]

let MapTypeStyle =
    Config "MapTypeStyle"
    |+> Instance [
        "elementType" =@ MapTypeStyleElementType
        |> WithComment "Selects the element type to which a styler should be applied. An element type distinguishes between the different representations of a feature. Optional; if elementType is not specified, the value is assumed to be 'all'."

        "featureType" =@ MapTypeStyleFeatureType
        |> WithComment "Selects the feature, or group of features, to which a styler should be applied. Optional; if featureType is not specified, the value is assumed to be 'all'."

        "stylers" =@ Type.ArrayOf MapTypeStyler
        |> WithComment "The style rules to apply to the selectors. The rules are applied to the map's elements in the order they are listed in this array."
    ]

let MapType =
    Class "google.maps.MapType"
    |=> Inherits MVC.MVCObject
    |+> Static [
        Constructor T<unit>
        |> WithInline "{}"
    ]
    |+> Instance [

            "getTile" => Base.Point?tileCoord * T<int>?zoom * Document?ownerDocument ^-> Node
            |> WithComment "Returns a tile for the given tile coordinate (x, y) and zoom level. This tile will be appended to the given ownerDocument. Not available for base map types."

            "releaseTile" => Node ^-> T<unit>
            |> WithComment "Releases the given tile, performing any necessary cleanup. The provided tile will have already been removed from the document. Optional."

            "alt" =@ T<string>
            |> WithComment "Alt text to display when this MapType's button is hovered over in the MapTypeControl. Optional."

            "maxZoom" =@ T<int>
            |> WithComment "The maximum zoom level for the map when displaying this MapType. Required for base MapTypes, ignored for overlay MapTypes."

            "minZoom" =@ T<int>
            |> WithComment "The minimum zoom level for the map when displaying this MapType. Optional; defaults to 0."

            "name" =@ T<string>
            |> WithComment "Name to display in the MapTypeControl. Optional."

            "projection" =@ Projection
            |> WithComment "The Projection used to render this MapType. Optional; defaults to Mercator."

            "radius" =@ T<float>
            |> WithComment "Radius of the planet for the map, in meters. Optional; defaults to Earth's equatorial radius of 6378137 meters."

            "tileSize" =@ Base.Size
            |> WithComment "The dimensions of each tile. Required."
        ]

let StyledMapTypeOptions =
    Class "google.maps.StyledMapTypeOptions"
    |+> Static [Constructor T<unit>]
    |+> Instance [
        "alt" =@ T<string>
        |> WithComment "Text to display when this MapType's button is hovered over in the map type control."

        "maxZoom" =@ T<string>
        |> WithComment "The maximum zoom level for the map when displaying this MapType. Optional."

        "minZoom" =@ T<string>
        |> WithComment "The minimum zoom level for the map when displaying this MapType. Optional."
    ]

let StyledMapType =
    Class "google.maps.StyledMapType"
    |=> Inherits MapType
    |+> Static [
        Ctor [
            (Type.ArrayOf MapTypeStyle)?Styles
            StyledMapTypeOptions?Options
        ]
        |> WithComment "Creates a styled MapType with the specified options. The StyledMapType takes an array of MapTypeStyles, where each MapTypeStyle is applied to the map consecutively. A later MapTypeStyle that applies the same MapTypeStylers to the same selectors as an earlier MapTypeStyle will override the earlier MapTypeStyle."
    ]

let MapTypeRegistry =
    Class "google.maps.MapTypeRegistry"
    |=> Inherits MVC.MVCObject
    |+> Static [
        Constructor T<unit>
        |> WithComment "The MapTypeRegistry holds the collection of custom map types available to the map for its use. The API consults this registry when providing the list of avaiable map types within controls, for example."
    ]
    |+> Instance [
        "set" => T<string> * MapType ^-> T<unit>
        |> WithComment "Sets the registry to associate the passed string identifier with the passed MapType."
    ]

let ImageMapTypeOptions =
    Config "ImageMapTypeOptions"
    |+> Instance [
        "alt" =@ T<string>
        |> WithComment "Alt text to display when this MapType's button is hovered over in the MapTypeControl."

        "getTileUrl" =@ T<obj> -* Base.Point * T<int> ^-> T<string>
        |> WithComment "Returns a string (URL) for given tile coordinate (x, y) and zoom level. This function should have a signature of: getTileUrl(Point, number):string"

        "maxZoom" =@ T<int>
        |> WithComment "The maximum zoom level for the map when displaying this MapType."

        "minZoom" =@ T<int>
        |> WithComment "The minimum zoom level for the map when displaying this MapType. Optional."

        "name" =@ T<string>
        |> WithComment "Name to display in the MapTypeControl."

        "opacity" =@ T<float>
        |> WithComment "The opacity to apply to the tiles. The opacity should be specified as a float value between 0 and 1.0, where 0 is fully transparent and 1 is fully opaque."

        "tileSize" =@ Base.Size.Type
        |> WithComment "The tile size."
    ]

let ImageMapType =
    Class "google.maps.ImageMapType"
    |=> Inherits MVC.MVCObject
    |+> Instance [
            "getOpacity" => T<unit> ^-> T<float>
            |> WithComment "Returns the opacity level (0 (transparent) to 1.0) of the ImageMapType tiles."

            "setOpacity" => T<float> ^-> T<unit>
            |> WithComment "Sets the opacity level (0 (transparent) to 1.0) of the ImageMapType tiles."
        ]
    |+> Static [Constructor ImageMapTypeOptions]

