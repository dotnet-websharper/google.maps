/// Definitions for the Map Types part of the API. See:
/// http://developers.google.com/maps/documentation/javascript/reference
///
/// TODO: this needs to be upgraded to the latest API.
module IntelliFactory.WebSharper.Google.Maps.MapTypes

open IntelliFactory.WebSharper.InterfaceGenerator
open IntelliFactory.WebSharper.Google.Maps.Notation

let Projection =
    Class "google.maps.Projection"
    |+> Protocol [
        // Translates from the LatLng cylinder to the Point plane. This interface specifies a function which implements translation from given LatLng values to world coordinates on the map projection. The Maps API calls this method when it needs to plot locations on screen. Projection objects must implement this method.
        "fromLatLngToPoint" => Base.LatLng * !? Base.Point ^-> Base.Point
        // This interface specifies a function which implements translation from world coordinates on a map projection to LatLng values. The Maps API calls this method when it needs to translate actions on screen to positions on the map. Projection objects must implement this method.
        "fromPointToLatLng" => Base.Point * !? T<bool> ^-> Base.LatLng
    ]

let Visibility =
    Pattern.EnumStrings "Visibility" ["on"; "off"; "simplified"]

let MapTypeStyler =
    Pattern.Config "MapTypeStyler" {
        Required = []
        Optional =
            [
                // Gamma. Modifies the gamma by raising the lightness to the given power. Valid values: Floating point numbers, [0.01, 10], with 1.0 representing no change.
                "gamma", T<float>
                // Sets the hue of the feature to match the hue of the color supplied. Note that the saturation and lightness of the feature is conserved, which means that the feature will not match the color supplied exactly. Valid values: An RGB hex string, i.e. '#ff0000'.
                "hue", T<string>
                // Inverts lightness. A value of true will invert the lightness of the feature while preserving the hue and saturation.
                "invert_lightness", T<bool>
                // Lightness. Shifts lightness of colors by a percentage of the original value if decreasing and a percentage of the remaining value if increasing. Valid values: [-100, 100].
                "lightness", T<float>
                // Saturation. Shifts the saturation of colors by a percentage of the original value if decreasing and a percentage of the remaining value if increasing. Valid values: [-100, 100].
                "saturation", T<float>
                // Visibility: Valid values: 'on', 'off' or 'simplifed'.
                "visibility", Visibility.Type
            ]
    }

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
        // Apply the rule to the feature's labels.
        "labels"
    ]

let MapTypeStyle =
    Pattern.Config "MapTypeStyle" {
        Required = []
        Optional =
        [
            // Selects the element type to which a styler should be applied. An element type distinguishes between the different representations of a feature. Optional; if elementType is not specified, the value is assumed to be 'all'.
            "elementType", MapTypeStyleElementType.Type
            // Selects the feature, or group of features, to which a styler should be applied. Optional; if featureType is not specified, the value is assumed to be 'all'.
            "featureType", MapTypeStyleFeatureType.Type
            // The style rules to apply to the selectors. The rules are applied to the map's elements in the order they are listed in this array.
            "stylers", Type.ArrayOf MapTypeStyler
        ]
    }

let MapType =
    Class "google.maps.MapType"
    |+> Protocol [

            "getTitle" =>
                Fun Node [
                    Base.Point?tileCoord
                    T<int>?zoom
                    Document?ownerDocument
                ]

            "releaseTile" => Node ^-> T<unit>

            // Alt text to display when this MapType's button is hovered over in the MapTypeControl. Optional.
            "alt" =@ T<string>
            // The maximum zoom level for the map when displaying this MapType. Required for base MapTypes, ignored for overlay MapTypes.
            "maxZoom" =@ T<int>
            // The minimum zoom level for the map when displaying this MapType. Optional; defaults to 0.
            "minZoom" =@ T<int>
            // Name to display in the MapTypeControl. Optional.
            "name" =@ T<string>
            // The Projection used to render this MapType. Optional; defaults to Mercator.
            "projection" =@ Projection
            // Radius of the planet for the map, in meters. Optional; defaults to Earth's equatorial radius of 6378137 meters.
            "radius" =@ T<float>
            // The dimensions of each tile. Required.
            "tileSize" =@ Base.Size
        ]

let MapTypeRegistry =
    Class "google.maps.MapTypeRegistry"
    |=> Inherits MVC.MVCObject
    |+> [Constructor T<unit>]
    |+> Protocol
        [
            "set" => T<string> * MapType ^-> T<unit>
        ]

let ImageMapTypeOptions =
    Pattern.Config "ImageMapTypeOptions" {
        Required = []
        Optional =
            [
                // Alt text to display when this MapType's button is hovered over in the MapTypeControl.
                "alt", T<string>
                // Returns a string (URL) for given tile coordinate (x, y) and zoom level. This function should have a signature of: getTileUrl(Point, number):string
                "getTileUrl", T<obj> -* Base.Point * T<int> ^-> T<string>
                // The maximum zoom level for the map when displaying this MapType.
                "maxZoom", T<int>
                // The minimum zoom level for the map when displaying this MapType. Optional.
                "minZoom", T<int>
                // Name to display in the MapTypeControl.
                "name", T<string>
                // The opacity to apply to the tiles. The opacity should be specified as a float value between 0 and 1.0, where 0 is fully transparent and 1 is fully opaque.
                "opacity", T<float>
                // The tile size.
                "tileSize", Base.Size.Type
            ]
    }

let ImageMapType =
    Class "google.maps.ImageMapType"
    |=> Inherits MVC.MVCObject
    |+> Protocol [
            "getOpacity" => T<unit> ^-> T<float>
            "setOpacity" => T<float> ^-> T<unit>
        ]
    |+> [Constructor ImageMapTypeOptions]

