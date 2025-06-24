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
// Definitions for the Map Types part of the API. See:
// http://developers.google.com/maps/documentation/javascript/reference
namespace WebSharper.Google.Maps.Definition

    module MapTypes =

        open WebSharper.InterfaceGenerator
        open Notation

        let Projection =
            Class "google.maps.Projection"
            |+> Static [Constructor T<unit>]
            |+> Instance [
                "fromLatLngToPoint" =@ (Base.LatLng + Base.LatLngLiteral) * !? Base.Point ^-> Base.Point
                |> WithComment "Translates from the LatLng cylinder to the Point plane. This interface specifies a function which implements translation from given LatLng values to world coordinates on the map projection. The Maps API calls this method when it needs to plot locations on screen. Projection objects must implement this method, but may return null if the projection cannot calculate the Point."

                "fromPointToLatLng" =@ Base.Point * !? T<bool> ^-> Base.LatLng
                |> WithComment "This interface specifies a function which implements translation from world coordinates on a map projection to LatLng values. The Maps API calls this method when it needs to translate actions on screen to positions on the map. Projection objects must implement this method, but may return null if the projection cannot calculate the LatLng."
            ]

        let Visibility =
            Pattern.EnumStrings "Visibility" ["on"; "off"; "simplified"]

        let MapTypeStyler =
            Config "MapTypeStyler"
                []
                [
                    // Sets the color of the feature. Valid values: An RGB hex string, i.e. '#ff0000'.
                    "color", T<string>

                    // Modifies the gamma by raising the lightness to the given power. Valid values: Floating point numbers, [0.01, 10], with 1.0 representing no change.
                    "gamma", T<float>

                    // Sets the hue of the feature to match the hue of the color supplied. Note that the saturation and lightness of the feature is conserved, which means that the feature will not match the color supplied exactly. Valid values: An RGB hex string, i.e. '#ff0000'.
                    "hue", T<string>

                    // A value of true will invert the lightness of the feature while preserving the hue and saturation.
                    "invert_lightness", T<bool>

                    // Shifts lightness of colors by a percentage of the original value if decreasing and a percentage of the remaining value if increasing. Valid values: [-100, 100].
                    "lightness", T<float>

                    // Shifts the saturation of colors by a percentage of the original value if decreasing and a percentage of the remaining value if increasing. Valid values: [-100, 100].
                    "saturation", T<float>

                    // Sets the visibility of the feature. Valid values: 'on', 'off' or 'simplifed'.
                    "visibility", Visibility.Type

                    // Sets the weight of the feature, in pixels. Valid values: Integers greater than or equal to zero.
                    "weight", T<int>
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
            Config "google.maps.MapTypeStyle"
                []
                [
                    "elementType", T<string>
                    "featureType", T<string>
                    "stylers", !|T<obj>
                ]

        let MapType =
            Interface "google.maps.MapType"
            |+> [
                "getTile" => Base.Point?tileCoord * T<int>?zoom * Document?ownerDocument ^-> Element
                |> WithComment "Returns a tile for the given tile coordinate (x, y) and zoom level. This tile will be appended to the given ownerDocument. Not available for base map types."

                "releaseTile" => Element ^-> T<unit>
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

        let StyledMapType =
            Class "google.maps.StyledMapType"
            |=> Inherits MVC.MVCObject
            |=> Implements [MapType]

        let StyledMapTypeOptions =
            Config "google.maps.StyledMapTypeOptions"
                []
                [
                    "alt", T<string>
                    "maxZoom", T<int>
                    "minZoom", T<int>
                    "name", T<string>
                ]

        do 
            StyledMapType
            |+> Static [
                Constructor (Type.ArrayOf MapTypeStyle * !? StyledMapTypeOptions)
                |> WithComment "Creates a styled MapType with the specified options. The StyledMapType takes an array of MapTypeStyles, where each MapTypeStyle is applied to the map consecutively. A later MapTypeStyle that applies the same MapTypeStylers to the same selectors as an earlier MapTypeStyle will override the earlier MapTypeStyle."
            ]
            |+> Instance [
                // "getTile" => Base.Point?tileCoord * T<int>?zoom * Document?ownerDocument ^-> Node

                // "releaseTile" => Node ^-> T<unit>

                // "alt" =@ T<string>

                // "maxZoom" =@ T<int>

                // "minZoom" =@ T<int>

                // "name" =@ T<string>

                // "projection" =@ Projection

                // "radius" =@ T<float>

                // "tileSize" =@ Base.Size
            ]
            |> ignore

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
            Config "google.maps.ImageMapTypeOptions"
                []
                [
                    "alt", T<string>
                    "getTileUrl", Base.Point * T<int> ^-> T<string>
                    "maxZoom", T<int>
                    "minZoom", T<int>
                    "name", T<string>
                    "opacity", T<float>
                    "tileSize", T<obj>
                ]

        let ImageMapType =
            Class "google.maps.ImageMapType"
            |=> Inherits MVC.MVCObject
            |=> Implements [MapType]
            |+> Instance [
                "getOpacity" => T<unit> ^-> T<float>
                |> WithComment "Returns the opacity level (0 (transparent) to 1.0) of the ImageMapType tiles."

                "setOpacity" => T<float> ^-> T<unit>
                |> WithComment "Sets the opacity level (0 (transparent) to 1.0) of the ImageMapType tiles."

                // EVENTS
                "tilesloaded" => T<unit> ^-> T<unit>
                |> WithComment "This event is fired when the visible tiles have finished loading."
                ]
            |+> Static [Constructor ImageMapTypeOptions]