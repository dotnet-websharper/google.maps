// $begin{copyright}
//
// This file is part of WebSharper
//
// Copyright (c) 2008-2018 IntelliFactory
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
/// Definitions for the Base part of the API. See:
/// http://developers.google.com/maps/documentation/javascript/reference
module WebSharper.Google.Maps.Base

open WebSharper.InterfaceGenerator
open WebSharper.Google.Maps.Notation

let LatLng =
    Class "google.maps.LatLng"
    |+> Static [
            Ctor [
                T<float>?Lat
                T<float>?Lng
                !? T<bool>?NoWrap
            ]
            |> WithComment "Creates a LatLng object representing a geographic point. Latitude is specified in degrees within the range [-90, 90]. Longitude is specified in degrees within the range [-180, 180]. Set noWrap to true to enable values outside of this range. Note the ordering of latitude and longitude."
        ]
    |+> Instance [
        "equals" => TSelf?other ^-> T<bool>
        |> WithComment "Comparison function."

        "lat" => T<unit> ^-> T<float>
        |> WithComment "Returns the latitude in degrees."

        "lng" => T<unit> ^-> T<float>
        |> WithComment "Returns the longitude in degrees."

        "toString" => T<unit -> string>
        |> WithComment "Converts to string representation."

        "toUrlValue" => (!? T<int>) ^-> T<string>
        |> WithComment "Returns a string of the form \"lat,lng\" for this LatLng. We round the lat/lng values to 6 decimal places by default."
    ]

let LatLngBounds =
    Class "google.maps.LatLngBounds"
    |+> Static [
            Ctor [
                !? LatLng?SW
                !? LatLng?NE
            ]
        ]
    |+> Instance [
        "contains" => (LatLng) ^-> T<bool>
        |> WithComment "Returns true if the given lat/lng is in this bounds."

        "equals" => TSelf ^-> T<bool>
        |> WithComment "Returns true if this bounds approximately equals the given bounds."

        "extend" => (LatLng) ^-> TSelf
        |> WithComment "Extends this bounds to contain the given point."

        "getCenter" => T<unit> ^-> LatLng
        |> WithComment "Computes the center of this LatLngBounds"

        "getNorthEast" => T<unit> ^-> LatLng
        |> WithComment "Returns the north-east corner of this bounds."

        "getSouthWest" => T<unit> ^-> LatLng
        |> WithComment "Returns the south-west corner of this bounds."

        "intersects" => (TSelf) ^-> T<bool>
        |> WithComment "Returns true if this bounds shares any points with this bounds."

        "isEmpty" => T<unit> ^-> T<bool>
        |> WithComment "Returns if the bounds are empty."

        "toSpan" => T<unit> ^-> LatLng
        |> WithComment "Converts the given map bounds to a lat/lng span."

        "toString" => T<unit -> string>
        |> WithComment "Converts to string."

        "toUrlValue" => (!? T<float>) ^-> T<string>
        |> WithComment "Returns a string of the form \"lat_lo,lng_lo,lat_hi,lng_hi\" for this bounds, where \"lo\" corresponds to the southwest corner of the bounding box, while \"hi\" corresponds to the northeast corner of that box."

        "union" => (TSelf) ^-> TSelf
        |> WithComment "Extends this bounds to contain the union of this and the given bounds."
    ]

let Point =
    Class "google.maps.Point"
    |+> Static [
            Ctor [
                T<float>?x
                T<float>?y
            ]
        ]
    |+> Instance [
        "equals" => TSelf ^-> T<bool>
        |> WithComment "Compares two Points"

        "toString" => T<unit -> string>
        |> WithComment "Returns a string representation of this Point."

        "x" =@ T<float>

        "y" =@ T<float>
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

        "height" =@ T<int>
        |> WithComment "The height along the y-axis, in pixels."

        "width" =@ T<int>
        |> WithComment "The width along the x-axis, in pixels."
    ]
