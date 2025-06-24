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
// Definitions for the Base part of the API. See:
// http://developers.google.com/maps/documentation/javascript/reference
namespace WebSharper.Google.Maps.Definition

module Base =

    open WebSharper.InterfaceGenerator
    open Notation

    let LatLngLiteral =
        Config "google.maps.LatLngLiteral"
            [
              "lat", T<float>
              "lng", T<float>
            ] 
            []

    let LatLng =
        Class "google.maps.LatLng"
        |+> Static [
                Ctor [
                    (T<float> + TSelf + LatLngLiteral)?latOrLatLngOrLatLngLiteral
                    !? (T<float> + T<bool>)?lngOrNoClampNoWrap
                    !? T<bool>?noClampNoWrap
                ]
                |> WithComment "Creates a LatLng object representing a geographic point. Latitude is specified in degrees within the range [-90, 90]. Longitude is specified in degrees within the range [-180, 180). Set noClampNoWrap to true to enable values outside of this range. Note the ordering of latitude and longitude."
            ]
        |+> Instance [
            "equals" => !?TSelf?other ^-> T<bool>
            |> WithComment "Comparison function."

            "lat" => T<unit> ^-> T<float>
            |> WithComment "Returns the latitude in degrees."

            "lng" => T<unit> ^-> T<float>
            |> WithComment "Returns the longitude in degrees."

            "toJSON" => T<unit> ^-> LatLngLiteral
            |> WithComment "Converts to JSON representation. This function is intended to be used via JSON.stringify."

            "toString" => T<unit -> string>
            |> WithComment "Converts to string representation."

            "toUrlValue" => (!? T<int>) ^-> T<string>
            |> WithComment "Returns a string of the form \"lat,lng\" for this LatLng. We round the lat/lng values to 6 decimal places by default."
        ]

    let LatLngBoundsLiteral =
        Config "google.maps.LatLngBoundsLiteral" [
                "east", T<float>
                "north", T<float>
                "south", T<float>
                "west", T<float>
            ]
            []

    let LatLngBounds =
        Class "google.maps.LatLngBounds"
        |+> Static [
            Ctor [
                !? (LatLng + LatLngLiteral + TSelf + LatLngBoundsLiteral)?swOrLatLngBounds
                !? (LatLng + LatLngLiteral)?ne
            ]

            "MAX_BOUNDS" =? TSelf
            |> WithComment "LatLngBounds for the max bounds of the Earth. These bounds will encompass the entire globe."
        ]
        |+> Instance [
            "contains" => (LatLng + LatLngLiteral) ^-> T<bool>
            |> WithComment "Returns true if the given lat/lng is in this bounds."

            "equals" => TSelf + LatLngBoundsLiteral ^-> T<bool>
            |> WithComment "Returns true if this bounds approximately equals the given bounds."

            "extend" => (LatLng + LatLngLiteral) ^-> TSelf
            |> WithComment "Extends this bounds to contain the given point."

            "getCenter" => T<unit> ^-> LatLng
            |> WithComment "Computes the center of this LatLngBounds"

            "getNorthEast" => T<unit> ^-> LatLng
            |> WithComment "Returns the north-east corner of this bounds."

            "getSouthWest" => T<unit> ^-> LatLng
            |> WithComment "Returns the south-west corner of this bounds."

            "intersects" => (TSelf + LatLngBoundsLiteral) ^-> T<bool>
            |> WithComment "Returns true if this bounds shares any points with the other bounds."

            "isEmpty" => T<unit> ^-> T<bool>
            |> WithComment "Returns if the bounds are empty."

            "toJSON" => T<unit> ^-> LatLngBoundsLiteral
            |> WithComment "Converts to JSON representation. This function is intended to be used via JSON.stringify."

            "toSpan" => T<unit> ^-> LatLng
            |> WithComment "Converts the given map bounds to a lat/lng span."

            "toString" => T<unit -> string>
            |> WithComment "Converts to string."

            "toUrlValue" => (!? T<float>) ^-> T<string>
            |> WithComment "Returns a string of the form \"lat_lo,lng_lo,lat_hi,lng_hi\" for this bounds, where \"lo\" corresponds to the southwest corner of the bounding box, while \"hi\" corresponds to the northeast corner of that box."

            "union" => (TSelf + LatLngBoundsLiteral) ^-> TSelf
            |> WithComment "Extends this bounds to contain the union of this and the given bounds."
        ]

    let LatLngAltitudeLiteral =
        Config "google.maps.LatLngAltitudeLiteral"
            [
                "altitude", T<float>
                "lat", T<float>
                "lng", T<float>
            ] 
            []

    let LatLngAltitude =
        Class "google.maps.LatLngAltitude"
        |+> Static [
                Ctor [
                    (TSelf + LatLngAltitudeLiteral + LatLng + LatLngLiteral)?value
                    !? T<bool>?noClampNoWrap
                ]
            ]
        |+> Instance [
            "equals" => !?TSelf?other ^-> T<bool>
            |> WithComment "Comparison function."

            "altitude" =@ T<float>
            |> WithComment "Returns the altitude."

            "lat" => T<unit> ^-> T<float>
            |> WithComment "Returns the latitude."

            "lng" => T<unit> ^-> T<float>
            |> WithComment "Returns the longitude."

            "toJSON" => T<unit> ^-> LatLngAltitudeLiteral
        ]

    let Point =
        Class "google.maps.Point"
        |+> Static [
                Ctor [
                    T<float>?x
                    T<float>?y
                ]
                |> WithComment "A point on a two-dimensional plane."
            ]
        |+> Instance [
            "equals" => TSelf ^-> T<bool>
            |> WithComment "Compares two Points"

            "toString" => T<unit -> string>
            |> WithComment "Returns a string representation of this Point."

            "x" =@ T<float>
            |> WithComment "The X coordinate"

            "y" =@ T<float>
            |> WithComment "The Y coordinate"
        ]

    let Size =
        Class "google.maps.Size"
        |+> Static [
                Ctor [
                    T<float>?Width
                    T<float>?Height
                    !? T<string>?WidthUnit
                    !? T<string>?HeightUnit
                ]
                |> WithComment "Two-dimensonal size, where width is the distance on the x-axis, and height is the distance on the y-axis."
            ]
        |+> Instance [
            "equals" => (TSelf) ^-> T<bool>
            |> WithComment "Compares two Sizes."

            "toString" => T<unit -> string>
            |> WithComment "Returns a string representation of this Size."

            "height" =@ T<float>
            |> WithComment "The height along the y-axis, in pixels."

            "width" =@ T<float>
            |> WithComment "The width along the x-axis, in pixels."
        ]

    let Padding =
        Config "google.maps.Padding"
            []
            [
                "bottom", T<int>
                "left", T<int>
                "right", T<int>
                "top", T<int>
            ]
        
