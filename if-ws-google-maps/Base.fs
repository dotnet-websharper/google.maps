/// Definitions for the Base part of the API. See:
/// http://developers.google.com/maps/documentation/javascript/reference
module IntelliFactory.WebSharper.Google.Maps.Base

open IntelliFactory.WebSharper.InterfaceGenerator
open IntelliFactory.WebSharper.Google.Maps.Notation

let LatLng =
    let LatLng = Class "google.maps.LatLng"
    LatLng
    |+> [
            Ctor [
                T<float>?lattitude
                T<float>?longitute
                !? T<bool>?noWrap
            ]
        ]
    |+> Protocol [
        // Comparison function.
        "equals" => LatLng?other ^-> T<bool>

        // Returns the latitude in degrees.
        "lat" => T<unit> ^-> T<float>

        // Returns the longitude in degrees.
        "lng" => T<unit> ^-> T<float>

        // Returns a T<string> of the form "lat,lng" for this LatLng. We round the lat/lng values to 6 decimal places by default.
        "toUrlValue" => (!? T<int>) ^-> T<string>
    ]

let LatLngBounds =
    let LatLngBounds = Class "google.maps.LatLngBounds"
    LatLngBounds
    |+> [
            Ctor [
                !? LatLng?sw
                !? LatLng?ne
            ]
        ]
    |+> Protocol [
        // Returns true if the given lat/lng is in this bounds.
        "contains" => (LatLng) ^-> T<bool>
        // Returns true if this bounds approximately equals the given bounds.
        "equals" => (LatLngBounds) ^-> T<bool>
        // Extends this bounds to contain the given point.
        "extend" => (LatLng) ^-> LatLngBounds
        // Computes the center of this LatLngBounds
        "getCenter" => T<unit> ^-> LatLng
        // Returns the north-east corner of this bounds.
        "getNorthEast" => T<unit> ^-> LatLng
        // Returns the south-west corner of this bounds.
        "getSouthWest" => T<unit> ^-> LatLng
        // Returns true if this bounds shares any points with this bounds.
        "intersects" => (LatLngBounds) ^-> T<bool>
        // Returns if the bounds are empty.
        "isEmpty" => T<unit> ^-> T<bool>
        // Converts the given map bounds to a lat/lng span.
        "toSpan" => T<unit> ^-> LatLng
        // Returns a T<string> of the form "lat_lo,lng_lo,lat_hi,lng_hi" for this bounds, where "lo" corresponds to the southwest corner of the bounding box, while "hi" corresponds to the northeast corner of that box.
        "toUrlValue" => (!? T<float>) ^-> T<string>
        // Extends this bounds to contain the union of this and the given bounds.
        "union" => (LatLngBounds) ^-> LatLngBounds
    ]

let Point =
    let Point = Class "google.maps.Point"
    Point
    |+> [
            Ctor [
                T<float>?x
                T<float>?y
            ]
        ]
    |+> Protocol [
        // Compares two Points
        "equals" => (Point) ^-> T<bool>
        // The X coordinate
        "x" =@ T<float>
        // The Y coordinate
        "y" =@ T<float>
    ]

let Size =
    let Size = Class "google.maps.Size"
    Size
    |+> [
            Ctor [
                T<float>?width
                T<float>?height
                !? T<string>?widthUnit
                !? T<string>?heightUnit
            ]
        ]
    |+> Protocol [
        // Compares two Sizes.
        "equals" => (Size) ^-> T<bool>

        // The height along the y-axis, in pixels.
        "height" =@ T<int>

        // The width along the x-axis, in pixels.
        "width" =@ T<int>
    ]
