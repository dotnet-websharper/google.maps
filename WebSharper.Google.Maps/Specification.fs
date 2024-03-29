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
// The rest of the spec.
namespace WebSharper.Google.Maps.Definition

module Specification =

    open WebSharper.InterfaceGenerator
    open Base
    open Notation
    module M = WebSharper.Google.Maps.Definition.Map

    let Animation =
        Class "google.maps.Animation"
        |+> Static [
            "BOUNCE" =? TSelf
            |> WithComment "Marker bounces until animation is stopped by calling Marker.setAnimation with null."

            "DROP" =? TSelf
            |> WithComment "Marker drops from the top of the map to its final location. Animation will cease once the marker comes to rest and Marker.getAnimation will return null. This type of animation is usually specified during creation of the marker."
            ]

    let MapPanes =
        Config "google.maps.MapPanes"
        |+> Instance [
            "floatPane" =@ Node
            |> WithComment "This pane contains the info window. It is above all map overlays. (Pane 4)."

            "mapPane" =@ Node
            |> WithComment "This pane is the lowest pane and is above the tiles. It does not receive DOM events. (Pane 0)."

            "markerLayer" =@ Node
            |> WithComment "This pane contains markers. It does not receive DOM events. (Pane 2)."

            "overlayLayer" =@ Node
            |> WithComment "This pane contains polylines, polygons, ground overlays and tile layer overlays. It does not receive DOM events. (Pane 1)."

            "overlayMouseTarget" =@ Node
            |> WithComment "This pane contains elements that receive DOM events. (Pane 3)."
        ]


    let MapCanvasProjection =
        Class "google.maps.MapCanvasProjection"
        |+> Instance [
            "fromContainerPixelToLatLng" => !? Point + !? T<bool> ^-> LatLng
            |> WithComment "Computes the geographical coordinates from pixel coordinates in the map's container."

            "fromDivPixelToLatLng" => !? Point + !? T<bool> ^-> LatLng
            |> WithComment "Computes the geographical coordinates from pixel coordinates in the div that holds the draggable map."

            "fromLatLngToContainerPixel" => LatLng + LatLngLiteral ^-> Point
            |> WithComment "Computes the pixel coordinates of the given geographical location in the map's container element."

            "fromLatLngToDivPixel" => LatLng + LatLngLiteral ^-> Point
            |> WithComment "Computes the pixel coordinates of the given geographical location in the DOM element that holds the draggable map."

            "getVisibleRegion" => T<unit> ^-> M.VisibleRegion
            |> WithComment "The visible region of the map. Returns null if the map has no size. Returns null if the OverlayView is on a StreetViewPanorama."

            "getWorldWidth" => T<unit> ^-> T<int>
            |> WithComment "The width of the world in pixels in the current zoom level. For projections with a heading angle of either 90 or 270 degrees, this corresponds to the pixel span in the Y-axis."
        ]

    let StrokePosition =
        Class "google.maps.StrokePosition"
        |+> Static [
            "CENTER" =? TSelf
            |> WithComment "The stroke is centered on the polygon's path, with half the stroke inside the polygon and half the stroke outside the polygon."

            "INSIDE" =? TSelf
            |> WithComment "The stroke lies inside the polygon."

            "OUTSIDE" =? TSelf
            |> WithComment "The stroke lies outside the polygon."
        ]

    let RectangleOptions =
        Config "google.maps.RectangleOptions"
        |+> Instance [
            "bounds" =@ LatLngBounds + LatLngBoundsLiteral
            |> WithComment "The bounds."

            "clickable" =@ T<bool>
            |> WithComment "Indicates whether this Rectangle handles mouse events. Defaults to true."

            "draggable" =@ T<bool>
            |> WithComment "If set to true, the user can drag this rectangle over the map. Defaults to false."

            "editable" =@ T<bool>
            |> WithComment "If set to true, the user can edit this rectangle by dragging the control points shown at the corners and on each edge. Defaults to false."

            "fillColor" =@ T<string>
            |> WithComment "The fill color. All CSS3 colors are supported except for extended named colors."

            "fillOpacity" =@ T<float>
            |> WithComment "The fill opacity between 0.0 and 1.0."

            "map" =@ M.Map.Type
            |> WithComment "Map on which to display Rectangle."

            "strokeColor" =@ T<string>
            |> WithComment "The stroke color. All CSS3 colors are supported except for extended named colors."

            "strokeOpacity" =@ T<float>
            |> WithComment "The stroke opacity between 0.0 and 1.0"

            "strokePosition" =@ StrokePosition
            |> WithComment "The stroke position."

            "strokeWeight" =@ T<int>
            |> WithComment "The stroke width in pixels."

            "visible" =@ T<bool>
            |> WithComment "Whether this rectangle is visible on the map. Defaults to true."

            "zIndex" =@ T<int>
            |> WithComment "The zIndex compared to other polys."
        ]

    let CircleOptions =
        Interface "google.maps.CircleOptions"
        |+> [
            "center" =@ LatLng + LatLngLiteral
            |> WithComment "The center"

            "clickable" =@ T<bool>
            |> WithComment "Indicates whether this Circle handles mouse events. Defaults to true."

            "draggable" =@ T<bool>
            |> WithComment "If set to true, the user can drag this circle over the map. Defaults to false."

            "editable" =@ T<bool>
            |> WithComment "If set to true, the user can edit this circle by dragging the control points shown at the center and around the circumference of the circle. Defaults to false."

            "fillColor" =@ T<string>
            |> WithComment "The fill color. All CSS3 colors are supported except for extended named colors."

            "fillOpacity" =@ T<float>
            |> WithComment "The fill opacity between 0.0 and 1.0."

            "map" =@ M.Map.Type
            |> WithComment "Map on which to display Circle."

            "radius" =@ T<float>
            |> WithComment "The radius in meters on the Earth's surface"

            "strokeColor" =@ T<string>
            |> WithComment "The stroke color. All CSS3 colors are supported except for extended named colors."

            "strokeOpacity" =@ T<float>
            |> WithComment "The stroke opacity between 0.0 and 1.0."

            "strokePosition" =@ StrokePosition
            |> WithComment "The stroke position. Defaults to CENTER."

            "strokeWeight" =@ T<int>
            |> WithComment "The stroke width in pixels."

            "visible" =@ T<bool>
            |> WithComment "Whether this circle is visible on the map. Defaults to true."

            "zIndex" =@ T<int>
            |> WithComment "The zIndex compared to other polys."
        ]

    let CircleLiteral =
        Interface "google.maps.CircleLiteral"
        |=> Extends [CircleOptions]
        |+> [
            // "center" =@ LatLng + LatLngLiteral
            // |> WithComment "The center of the Circle."

            // "radius" =@ T<float>
            // |> WithComment "The radius in meters on the Earth's surface."
        ]

    let Circle =
        Class "google.maps.Circle"
        |=> Inherits MVC.MVCObject
        |+> Static [
            Constructor (TSelf + CircleLiteral + CircleOptions)
            |> WithComment "Create a circle using the passed CircleOptions, which specify the center, radius, and style."
        ]
        |+> Instance [
            "getBounds" => T<unit> ^-> LatLngBounds
            |> WithComment "Gets the LatLngBounds of this Circle."

            "getCenter" => T<unit> ^-> LatLng
            |> WithComment "Returns the center of this circle."

            "getDraggable" => T<unit -> bool>
            |> WithComment "Returns whether this circle can be dragged by the user."

            "getEditable" => T<unit -> bool>
            |> WithComment "Returns whether this circle can be edited by the user."

            "getMap" => T<unit> ^-> M.Map
            |> WithComment "Returns the map on which this circle is displayed."

            "getRadius" => T<unit> ^-> T<float>
            |> WithComment "Returns the radius of this circle (in meters)."

            "getVisible" => T<unit -> bool>
            |> WithComment "Returns whether this circle is visible on the map."

            "setCenter" => (LatLng + LatLngLiteral) ^-> T<unit>
            |> WithComment "Sets the center of this circle."

            "setDraggable" => T<bool -> unit>
            |> WithComment "If set to true, the user can drag this circle over the map."

            "setEditable" => T<bool -> unit>
            |> WithComment "If set to true, the user can edit this circle by dragging the control points shown at the center and around the circumference of the circle."

            "setMap" => M.Map ^-> T<unit>
            |> WithComment "Renders the circle on the specified map. If map is set to null, the circle will be removed."

            "setOptions" => CircleOptions ^-> T<unit>

            "setRadius" => T<float> ^-> T<unit>
            |> WithComment "Sets the radius of this circle (in meters)."

            "setVisible" => T<bool -> unit>
            |> WithComment "Hides this circle if set to false."

            // EVENTS
            "center_changed" => T<obj> -* T<unit> ^-> T<unit>
            |> WithComment "This event is fired when the circle's center is changed."

            "click" => T<obj> -* M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM click event is fired on the circle."

            "dblclick" => T<obj> -* M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM dblclick event is fired on the circle."

            "drag" => T<obj> -* M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is repeatedly fired while the user drags the circle."

            "dragend" => T<obj> -* M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the user stops dragging the circle."

            "dragstart" => T<obj> -* M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the user starts dragging the circle."

            "mousedown" => T<obj> -* M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM mousedown event is fired on the circle."

            "mousemove" => T<obj> -* M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM mousemove event is fired on the circle."

            "mouseout" => T<obj> -* M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired on circle mouseout."

            "mouseover" => T<obj> -* M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired on circle mouseover."

            "mouseup" => T<obj> -* M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM mouseup event is fired on the circle."

            "rightclick" => T<obj> -* M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the circle is right-clicked on."
        ]

    let Rectangle =
        Class "google.maps.Rectangle"
        |=> Inherits MVC.MVCObject
        |+> Static [
            Constructor RectangleOptions
            |> WithComment "Create a rectangle using the passed RectangleOptions, which specify the bounds and style."
        ]
        |+> Instance [
            "getBounds" => T<unit> ^-> LatLngBounds
            |> WithComment "Returns the bounds of this rectangle."

            "getDraggable" => T<unit -> bool>
            |> WithComment "Returns whether this rectangle can be dragged by the user."

            "getEditable" => T<unit -> bool>
            |> WithComment "Returns whether this rectangle can be edited by the user."

            "getMap" => T<unit> ^-> M.Map
            |> WithComment "Returns the map on which this rectangle is displayed."

            "getVisible" => T<unit -> bool>
            |> WithComment "Returns whether this rectangle is visible on the map."

            "setBounds" => LatLngBounds + LatLngBoundsLiteral ^-> T<unit>
            |> WithComment "Sets the bounds of this rectangle."

            "setDraggable" => T<bool -> unit>
            |> WithComment "If set to true, the user can drag this rectangle over the map."

            "setEditable" => T<bool -> unit>
            |> WithComment "If set to true, the user can edit this rectangle by dragging the control points shown at the corners and on each edge."

            "setMap" => M.Map ^-> T<unit>
            |> WithComment "Renders the rectangle on the specified map. If map is set to null, the rectangle will be removed."

            "setOptions" => (RectangleOptions) ^-> T<unit>

            "setVisible" => T<bool -> unit>
            |> WithComment "Hides this rectangle if set to false."

            // EVENTS
            "bounds_changed" => T<obj> -* T<unit> ^-> T<unit>
            |> WithComment "This event is fired when the rectangle's bounds are changed."

            "click" => T<obj> -* M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM click event is fired on the rectangle."

            "contextmenu" => T<obj> -* M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM contextmenu event is fired on the rectangle."

            "dblclick" => T<obj> -* M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM dblclick event is fired on the rectangle."

            "drag" => T<obj> -* M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is repeatedly fired while the user drags the rectangle."

            "dragend" => T<obj> -* M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the user stops dragging the rectangle."

            "dragstart" => T<obj> -* M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the user starts dragging the rectangle."

            "mousedown" => T<obj> -* M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM mousedown event is fired on the rectangle."

            "mousemove" => T<obj> -* M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM mousemove event is fired on the rectangle."

            "mouseout" => T<obj> -* M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired on rectangle mouseout."

            "mouseover" => T<obj> -* M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired on rectangle mouseover."

            "mouseup" => T<obj> -* M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM mouseup event is fired on the rectangle."

            "rightclick" => T<obj> -* M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the rectangle is right-clicked on."
            |> ObsoleteWithMessage "Deprecated: Use the Rectangle.contextmenu event instead in order to support usage patterns like control-click on macOS."
        ]

    let GroundOverlayOptions =
        Config "google.maps.GroundOverlayOptions"
        |+> Instance [
            "clickable" =@ T<bool>
            |> WithComment "If true, the ground overlay can receive mouse events."

            "map" =@ M.Map.Type
            |> WithComment "The map on which to display the overlay."

            "opacity" =@ T<float>
            |> WithComment "The opacity of the overlay, expressed as a number between 0 and 1. Optional. Defaults to 1.0"
        ]

    let GroundOverlay =
        Class "google.maps.GroundOverlay"
        |+> Static [
            Ctor [
                T<string>?Url
                (LatLngBounds + LatLngBoundsLiteral)?Bounds
                !? GroundOverlayOptions
            ]
            |> WithComment "Creates a ground overlay from the provided image URL and its LatLngBounds. The image is scaled to fit the current bounds, and projected using the current map projection."
        ]
        |=> Inherits MVC.MVCObject
        |+> Instance [
            "getBounds" => T<unit> ^-> LatLngBounds
            |> WithComment "Gets the LatLngBounds of this overlay."

            "getMap" => T<unit> ^-> M.Map
            |> WithComment "Returns the map on which this ground overlay is displayed."

            "getOpacity" => T<unit -> float>
            |> WithComment "Returns the opacity of this ground overlay."

            "getUrl" => T<unit> ^-> T<string>
            |> WithComment "Gets the url of the projected image."

            "setMap" => (M.Map) ^-> T<unit>
            |> WithComment "Renders the ground overlay on the specified map. If map is set to null, the overlay is removed."

            "setOpacity" => T<float -> unit>
            |> WithComment "Sets the opacity of this ground overlay."

            // EVENTS
            "click" => T<obj> -* M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM click event is fired on the GroundOverlay."

            "dblclick" => T<obj> -* M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM dblclick event is fired on the GroundOverlay."
        ]

    let BicyclingLayer =
        Class "google.maps.BicyclingLayer"
        |+> Static [
            Constructor T<unit>
            |> WithComment "A layer that displays bike lanes and paths and demotes large roads."
        ]
        |=> Inherits MVC.MVCObject
        |+> Instance [
            "getMap" => T<unit> ^-> M.Map
            |> WithComment "Returns the map on which this layer is displayed."

            "setMap" => (M.Map) ^-> T<unit>
            |> WithComment "Renders the layer on the specified map. If map is set to null, the layer will be removed."
        ]

    let FusionTablesHeatmap =
        Config "google.maps.FusionTablesHeatmap"
        |+> Instance [
            "enabled" =@ T<bool>
            |> WithComment "If true, render the layer as a heatmap."
        ]

    let FusionTablesMarkerOptions =
        Config "google.maps.FusionTablesMarkerOptions"
        |+> Instance [
            "iconName" =@ T<string>
            |> WithComment "The name of a Fusion Tables supported icon (http://www.google.com/fusiontables/DataSource?dsrcid=308519)"
        ]

    let FusionTablesPolygonOptions =
        Config "google.maps.FusionTablesPolygonOptions"
        |+> Instance [
            "fillColor" =@ T<string>
            |> WithComment "The fill color, defined by a six-digit hexadecimal number in RRGGBB format (e.g. #00AAFF)."

            "fillOpacity" =@ T<float>
            |> WithComment "The fill opacity between 0.0 and 1.0."

            "strokeColor" =@ T<string>
            |> WithComment "The stroke color, defined by a six-digit hexadecimal number in RRGGBB format (e.g. #00AAFF)."

            "strokeOpacity" =@ T<float>
            |> WithComment "The stroke opacity between 0.0 and 1.0."

            "strokeWeight" =@ T<float>
            |> WithComment "The stroke width in pixels, between 0 and 10."
        ]

    let FusionTablesPolylineOptions =
        Config "google.maps.FusionTablesPolylineOptions"
        |+> Instance [
            "strokeColor" =@ T<string>
            |> WithComment "The stroke color, defined by a six-digit hexadecimal number in RRGGBB format (e.g. #00AAFF)."

            "strokeOpacity" =@ T<float>
            |> WithComment "The stroke opacity between 0.0 and 1.0."

            "strokeWeight" =@ T<float>
            |> WithComment "The stroke width in pixels."
        ]

    let FusionTablesStyle =
        Config "google.maps.FusionTablesStyle"
        |+> Instance [
            "markerOptions" =@ FusionTablesMarkerOptions
            |> WithComment "Options which control the appearance of point features."

            "polygonOptions" =@ FusionTablesPolygonOptions
            |> WithComment "Options which control the appearance of polygons."

            "polylineOptions" =@ FusionTablesPolylineOptions
            |> WithComment "Options which control the appearance of polylines."

            "where" =@ T<string>
            |> WithComment "The SQL predicate to be applied to the layer."
        ]

    let FusionTablesLayerOptions =
        Config "google.maps.FusionTablesLayerOptions"
        |+> Instance [
            "clickable" =@ T<bool>
            |> WithComment "If true, the layer receives mouse events. Default value is true."

            "heatmap" =@ FusionTablesHeatmap
            |> WithComment "Options which define the appearance of the layer as a heatmap."

            "map" =@ M.Map
            |> WithComment "The map on which to display the layer."

            "query" =@ T<string>
            |> WithComment "Options defining the data to display."

            "styles" =@ Type.ArrayOf FusionTablesStyle
            |> WithComment "An array of up to 5 style specifications, which control the appearance of features within the layer."

            "suppressInfoWindows" =@ T<bool>
            |> WithComment "Suppress the rendering of info windows when layer features are clicked."
        ]

    let FusionTablesLayer =
        Class "google.maps.FusionTablesLayer"
        |+> Static [Constructor !? FusionTablesLayerOptions]
        |=> Inherits MVC.MVCObject
        |+> Instance [
            "getMap" => T<unit> ^-> M.Map
            |> WithComment "Returns the map on which this layer is displayed."

            "setMap" => M.Map ^-> T<unit>
            |> WithComment "Renders the layer on the specified map. If map is set to null, the layer will be removed."

            "setOptions" => (FusionTablesLayerOptions) ^-> T<unit>
        ]

    let FusionTablesQuery =
        Class "google.maps.FusionTablesQuery"
        |+> Static [Constructor T<unit>]
        |+> Instance [
            "from" =@ T<string>
            |> WithComment "The ID of the Fusion Tables table to display. This ID can be found in the table's URL, as the value of the dsrcid parameter. Required."

            "limit" =@ T<int>
            |> WithComment "Limit on the number of results returned by the query."

            "offset" =@ T<int>
            |> WithComment "Offset into the sorted results."

            "orderBy" =@ T<string>
            |> WithComment "The method by which to sort the results. Accepts either of:
    * A column name. The column name may be suffixed with ASC or DESC (e.g. col2 DESC) to specify ascending or descending sort.
    * An ST_DISTANCE spatial relationship (sort by distance). A column and the coordinate from which to calculate distance must be passed, for example, orderBy: 'ST_DISTANCE(col1, LATLNG(1.2, 3.4))'."

            "select" =@ T<string>
            |> WithComment "A column, containing geographic features to be displayed on the map. See Fusion Tables Setup in the Maps API documentation for information about valid columns."

            "where" =@ T<string>
            |> WithComment "The SQL predicate to be applied to the layer."
        ]

    let FusionTablesMouseEvent =
        Class "google.maps.FusionTablesMouseEvent"
        |+> Instance [
            "infoWindowHtml" =@ T<string>
            |> WithComment "Pre-rendered HTML content, as placed in the infowindow by the default UI."

            "latLng" =@ LatLng
            |> WithComment "The position at which to anchor an infowindow on the clicked feature."

            "pixelOffset" =@ Size
            |> WithComment "The offset to apply to an infowindow anchored on the clicked feature."

            "row" =@ T<obj>
            |> WithComment "A collection of FusionTablesCell objects, indexed by column name, representing the contents of the table row which included the clicked feature."
        ]

    let FusionTablesCell =
        Class "google.maps.FusionTablesCell"
        |+> Instance [
            "columnName" =@ T<string>
            |> WithComment "The name of the column in which the cell was located."

            "value" =@ T<string>
            |> WithComment "The contents of the cell."
        ]

    let KmlAuthor =
        Class "google.maps.KmlAuthor"
        |+> Instance [
            "email" =? T<string>
            |> WithComment "The author's e-mail address, or an empty string if not specified."

            "name" =? T<string>
            |> WithComment "The author's name, or an empty string if not specified."

            "uri" =? T<string>
            |> WithComment "The author's home page, or an empty string if not specified."
        ]

    let KmlFeatureData =
        Class "google.maps.KmlFeatureData"
        |+> Instance [
            "author" =? KmlAuthor
            |> WithComment "The feature's <atom:author>, extracted from the layer markup (if specified)."

            "description" =? T<string>
            |> WithComment "The feature's <description>, extracted from the layer markup."

            "id" =? T<string>
            |> WithComment "The feature's <id>, extracted from the layer markup. If no <id> has been specified, a unique ID will be generated for this feature."

            "infoWindowHtml" =? T<string>
            |> WithComment "The feature's balloon styled text, if set."

            "name" =? T<string>
            |> WithComment "The feature's <name>, extracted from the layer markup."

            "snippet" =? T<string>
            |> WithComment "The feature's <Snippet>, extracted from the layer markup."
        ]

    let KmlMouseEvent =
        Class "google.maps.KmlMouseEvent"
        |+> Instance [
            "featureData" =? KmlFeatureData
            |> WithComment "A KmlFeatureData object, containing information about the clicked feature."

            "latLng" =? LatLng
            |> WithComment "The position at which to anchor an infowindow on the clicked feature."

            "pixelOffset" =? Size
            |> WithComment "The offset to apply to an infowindow anchored on the clicked feature."
        ]


    let KmlLayerMetadata =
        Class "google.maps.KmlLayerMetadata"
        |+> Instance [
            "author" =? KmlAuthor
            |> WithComment "The layer's <atom:author>, extracted from the layer markup."

            "description" =? T<string>
            |> WithComment "The layer's <description>, extracted from the layer markup."

            "hasScreenOverlays" =? T<bool>
            |> WithComment "Whether the layer has any screen overlays."

            "name" =? T<string>
            |> WithComment "The layer's <name>, extracted from the layer markup."

            "snippet" =? T<string>
            |> WithComment "The layer's <Snippet>, extracted from the layer markup."
        ]

    let KmlLayerOptions =
        Config "google.maps.KmlLayerOptions"
        |+> Instance [
            "clickable" =@ T<bool>
            |> WithComment "If true, the layer receives mouse events. Default value is true."

            "map" =@ M.Map.Type
            |> WithComment "The map on which to display the layer."

            "preserveViewport" =@ T<bool>
            |> WithComment "If this option is set to true or if the map's center and zoom were never set, the input map is centered and zoomed to the bounding box of the contents of the layer. Default: false."

            "screenOverlays" =@ T<bool>
            |> WithComment "Whether to render the screen overlays. Default true."

            "suppressInfoWindows" =@ T<bool>
            |> WithComment "Suppress the rendering of info windows when layer features are clicked."

            "url" =@ T<string>
            |> WithComment "The URL of the KML document to display."

            "zIndex" =@ T<int>
            |> WithComment "The z-index of the layer."
        ]

    let KmlLayerStatus =
        Class "google.maps.KmlLayerStatus"
        |+> Static [
            "DOCUMENT_NOT_FOUND" =? TSelf
            |> WithComment "The document could not be found. Most likely it is an invalid URL, or the document is not publicly available."

            "DOCUMENT_TOO_LARGE" =? TSelf
            |> WithComment "The document exceeds the file size limits of KmlLayer."

            "FETCH_ERROR" =? TSelf
            |> WithComment "The document could not be fetched."

            "INVALID_DOCUMENT" =? TSelf
            |> WithComment "The document is not a valid KML, KMZ or GeoRSS document."

            "INVALID_REQUEST" =? TSelf
            |> WithComment "The KmlLayer is invalid."

            "LIMITS_EXCEEDED" =? TSelf
            |> WithComment "The document exceeds the feature limits of KmlLayer."

            "OK" =? TSelf
            |> WithComment "The layer loaded successfully."

            "TIMED_OUT" =? TSelf
            |> WithComment "The document could not be loaded within a reasonable amount of time."

            "UNKNOWN" =? TSelf
            |> WithComment "The document failed to load for an unknown reason."
        ]

    let KmlLayer =
        Class "google.maps.KmlLayer"
        |+> Static [
            Constructor !? KmlLayerOptions
            |> WithComment "Creates a KmlLayer which renders the contents of the specified KML/KMZ file (https://developers.google.com/kml/documentation/kmlreference) or GeoRSS file (http://www.georss.org)."
        ]
        |=> Inherits MVC.MVCObject
        |+> Instance [
            "getDefaultViewport" => T<unit> ^-> LatLngBounds
            |> WithComment "Get the default viewport for the layer being displayed."

            "getMap" => T<unit> ^-> M.Map
            |> WithComment "Get the map on which the KML Layer is being rendered."

            "getMetadata" => T<unit> ^-> KmlLayerMetadata
            |> WithComment "Get the metadata associated with this layer, as specified in the layer markup."

            "getStatus" => T<unit> ^-> KmlLayerStatus
            |> WithComment "Get the status of the layer, set once the requested document has loaded."

            "getUrl" => T<unit> ^-> T<string>
            |> WithComment "The URL of the KML file being displayed."

            "getZIndex" => T<unit> ^-> T<int>
            |> WithComment "Gets the z-index of the KML Layer."

            "setMap" => (M.Map) ^-> T<unit>
            |> WithComment "Renders the KML Layer on the specified map. If map is set to null, the layer is removed."

            "setOptions" => KmlLayerOptions ^-> T<unit>

            "setUrl" => T<string -> unit>
            |> WithComment "Set the URL of the KML file to display."

            "setZIndex" => T<int> ^-> T<unit>
            |> WithComment "Sets the z-index of the KML Layer."

            // EVENTS
            "click" =@ T<obj> -* KmlMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when a feature in the layer is clicked."

            "defaultviewport_changed" =@ T<obj> -* T<unit> ^-> T<unit>
            |> WithComment "This event is fired when the KML layers default viewport has changed."

            "status_changed" =@ T<obj> -* T<unit> ^-> T<unit>
            |> WithComment "This event is fired when the KML layer has finished loading. At this point it is safe to read the status property to determine if the layer loaded successfully."
        ]

    let TrafficLayerOptions =
        Config "google.maps.TrafficLayerOptions"
        |+> Instance [
            "autoRefresh" =@ T<bool>
            |> WithComment "Whether the traffic layer refreshes with updated information automatically."

            "map" =@ M.Map
            |> WithComment "Map on which to display the traffic layer."
        ]

    let TrafficLayer =
        Class "google.maps.TrafficLayer"
        |+> Static [
            Constructor TrafficLayerOptions
            |> WithComment "A layer that displays current road traffic."
        ]
        |=> Inherits MVC.MVCObject
        |+> Instance [
            "getMap" => T<unit> ^-> M.Map
            |> WithComment "Returns the map on which this layer is displayed."

            "setMap" => M.Map ^-> T<unit>
            |> WithComment "Renders the layer on the specified map. If map is set to null, the layer will be removed."

            "setOptions" =>  TrafficLayerOptions ^-> T<unit>
            |> WithComment ""
        ]

    let TransitLayer =
        Class "google.maps.TransitLayer"
        |+> Static [Constructor T<unit>]
        |=> Inherits MVC.MVCObject
        |+> Instance [
            "getMap" => T<unit> ^-> M.Map
            |> WithComment "Returns the map on which this layer is displayed."

            "setMap" => M.Map ^-> T<unit>
            |> WithComment "Renders the layer on the specified map. If map is set to null, the layer will be removed."
        ]

    let MarkerImage =
        Interface "MarkerImage"

    let Icon =
        Config "google.maps.Icon"
        |+> Instance [
            "anchor" =@ Point
            |> WithComment "The position at which to anchor an image in correspondance to the location of the marker on the map. By default, the anchor is located along the center point of the bottom of the image."

            "labelOrigin" =@ Point
            |> WithComment "The origin of the label relative to the top-left corner of the icon image, if a label is supplied by the marker. By default, the origin is located in the center point of the image."

            "origin" =@ Point
            |> WithComment "The position of the image within a sprite, if any. By default, the origin is located at the top left corner of the image (0, 0)."

            "scaledSize" =@ Size
            |> WithComment "The size of the entire image after scaling, if any. Use this property to stretch/shrink an image or a sprite."

            "size" =@ Size
            |> WithComment "The display size of the sprite or image. When using sprites, you must specify the sprite size. If the size is not provided, it will be set when the image loads."

            "url" =@ T<string>
            |> WithComment "The URL of the image or sprite sheet."
        ]

    let SymbolPath =
        Class "google.maps.SymbolPath"
        |+> Static [
            "BACKWARD_CLOSED_ARROW" =? TSelf
            |> WithComment "A backward-pointing closed arrow."

            "BACKWARD_OPEN_ARROW" =? TSelf
            |> WithComment "A backward-pointing open arrow."

            "CIRCLE" =? TSelf
            |> WithComment "A circle."

            "FORWARD_CLOSED_ARROW" =? TSelf
            |> WithComment "A forward-pointing closed arrow."

            "FORWARD_OPEN_ARROW" =? TSelf
            |> WithComment "A forward-pointing open arrow."
        ]

    let Symbol =
        Class "google.maps.Symbol"
        |+> Static [
            Constructor SymbolPath?Path
            |> WithInline "{path:$Path}"
        ]
        |+> Instance [
            "anchor" =@ Point
            |> WithComment "The position of the symbol relative to the marker or polyline. The coordinates of the symbol's path are translated left and up by the anchor's x and y coordinates respectively. By default, a symbol is anchored at (0, 0). The position is expressed in the same coordinate system as the symbol's path."

            "fillColor" =@ T<string>
            |> WithComment "The symbol's fill color. All CSS3 colors are supported except for extended named colors. For symbol markers, this defaults to 'black'. For symbols on polylines, this defaults to the stroke color of the corresponding polyline."

            "fillOpacity" =@ T<float>
            |> WithComment "The symbol's fill opacity. Defaults to 0."

            "labelOrigin" =@ Point
            |> WithComment "The origin of the label relative to the origin of the path, if label is supplied by the marker. The origin is expressed in the same coordinate system as the symbol's path. This property is unused for symbols on polylines. Default: google.maps.Point(0,0)."

            "path" =@ SymbolPath
            |> WithComment "The symbol's path, which is a built-in symbol path, or a custom path expressed using SVG path notation. Required."

            "rotation" =@ T<float>
            |> WithComment "The angle by which to rotate the symbol, expressed clockwise in degrees. Defaults to 0. A symbol in an IconSequence where fixedRotation is false is rotated relative to the angle of the edge on which it lies."

            "scale" =@ T<float>
            |> WithComment "The amount by which the symbol is scaled in size. For symbol markers, this defaults to 1; after scaling, the symbol may be of any size. For symbols on a polyline, this defaults to the stroke weight of the polyline; after scaling, the symbol must lie inside a square 22 pixels in size centered at the symbol's anchor."

            "strokeColor" =@ T<string>
            |> WithComment "The symbol's stroke color. All CSS3 colors are supported except for extended named colors. For symbol markers, this defaults to 'black'. For symbols on a polyline, this defaults to the stroke color of the polyline."

            "strokeOpacity" =@ T<float>
            |> WithComment "The symbol's stroke opacity. For symbol markers, this defaults to 1. For symbols on a polyline, this defaults to the stroke opacity of the polyline."

            "strokeWeight" =@ T<float>
            |> WithComment "The symbol's stroke weight. Defaults to the scale of the symbol."
        ]

    let CollisionBehavior =
        Pattern.EnumInlines "CollisionBehavior" [
            // Display the marker only if it does not overlap with other markers. If two markers of this type would overlap, the one with the higher zIndex is shown. If they have the same zIndex, the one with the lower vertical screen position is shown.
            "OPTIONAL_AND_HIDES_LOWER_PRIORITY", "google.maps.CollisionBehavior.OPTIONAL_AND_HIDES_LOWER_PRIORITY"
            // Always display the marker regardless of collision. This is the default behavior.
            "REQUIRED", "google.maps.CollisionBehavior.REQUIRED"
            //  	Always display the marker regardless of collision, and hide any OPTIONAL_AND_HIDES_LOWER_PRIORITY markers or labels that would overlap with the marker.
            "REQUIRED_AND_HIDES_OPTIONAL", "google.maps.CollisionBehavior.REQUIRED_AND_HIDES_OPTIONAL"
        ]

    let MarkerShapeType =
        Pattern.EnumStrings "MarkerShapeType" ["circle"; "poly"; "rect"]

    let MarkerShape =
        Pattern.Config "MarkerShape" {
            Optional = []
            Required =
            [
                "coords", Type.ArrayOf T<int>
                "type", MarkerShapeType.Type
            ]
        }

    let MarkerLabel =
        // Interface "MarkerLabel"
        Class "MarkerLabel"
        |+> Static [
            Ctor [
                T<string>?Text
            ]
            |> WithInline "{text: $Text}"
        ]
        |+> Instance [
              "text" =@ T<string>
              |> WithComment "The text to be displayed in the label."

              "className" =@ T<string>
              |> WithComment "The className property of the label's element (equivalent to the element's class attribute). Multiple space-separated CSS classes can be added. The font color, size, weight, and family can only be set via the other properties of MarkerLabel. CSS classes should not be used to change the position nor orientation of the label (e.g. using translations and rotations) if also using marker collision management."

              "color" =@ T<string>
              |> WithComment "The color of the label text."

              "fontFamily" =@ T<string>
              |> WithComment "The font family of the label text (equivalent to the CSS font-family property)."

              "fontSize" =@ T<string>
              |> WithComment "The font size of the label text (equivalent to the CSS font-size property)."

              "fontWeight" =@ T<string>
              |> WithComment "The font weight of the label text (equivalent to the CSS font-weight property)."
            ]

    let MarkerOptions =
        Class "google.maps.MarkerOptions"
        |+> Static [
            Constructor LatLng?Position
            |> WithInline "{position:$Position}"
            ]
        |+> Instance [

            "anchorPoint" =@ Point
            |> WithComment "The offset from the marker's position to the tip of an InfoWindow that has been opened with the marker as anchor."

            "animation" =@ Animation
            |> WithComment "Which animation to play when marker is added to a map."

            "clickable" =@ T<bool>
            |> WithComment "If true, the marker receives mouse and touch events. Default value is true."

            "collisionBehavior" =@ !? (T<string> + CollisionBehavior)
            |> WithComment "Set a collision behavior for markers on vector maps."
            |> ObsoleteWithMessage "Deprecated: collisionBehavior is deprecated as of July 2023. Use AdvancedMarkerElement.collisionBehavior instead."

            "crossOnDrag" =@ T<bool>
            |> WithComment "If false, disables cross that appears beneath the marker when dragging."

            "cursor" =@ T<string>
            |> WithComment "Mouse cursor to show on hover"

            "draggable" =@ T<bool>
            |> WithComment "If true, the marker can be dragged. Default value is false."

            "icon" =@ T<string> + Icon + Symbol
            |> WithComment "Icon for the foreground. If a string is provided, it is treated as though it were an Icon with the string as url."

            "label" =@ T<string> + MarkerLabel
            |> WithComment "Adds a label to the marker. A marker label is a letter or number that appears inside a marker. The label can either be a string, or a MarkerLabel object. If provided and MarkerOptions.title is not provided, an accessibility text (e.g. for use with screen readers) will be added to the marker with the provided label's text. Please note that the label is currently only used for accessibility text for non-optimized markers."

            "map" =@ M.Map + StreetView.StreetViewPanorama
            |> WithComment "Map on which to display Marker. The map is required to display the marker and can be provided with Marker.setMap if not provided at marker construction."

            "opacity" =@ T<float>
            |> WithComment "A number between 0.0, transparent, and 1.0, opaque."

            "optimized" =@ T<bool>
            |> WithComment "Optimization enhances performance by rendering many markers as a single static element. This is useful in cases where a large number of markers is required. Read more about marker optimization."

            "position" =@ LatLng + LatLngLiteral
            |> WithComment "Sets the marker position. A marker may be constructed but not displayed until its position is provided - for example, by a user's actions or choices. A marker position can be provided with Marker.setPosition if not provided at marker construction."

            "shape" =@ MarkerShape
            |> WithComment "Image map region definition used for drag/click."

            "title" =@ T<string>
            |> WithComment "Rollover text. If provided, an accessibility text (e.g. for use with screen readers) will be added to the marker with the provided value. Please note that the title is currently only used for accessibility text for non-optimized markers."

            "visible" =@ T<bool>
            |> WithComment "If true, the marker is visible."

            "zIndex" =@ T<int>
            |> WithComment "All markers are displayed on the map in order of their zIndex, with higher values displaying in front of markers with lower values. By default, markers are displayed according to their vertical position on screen, with lower markers appearing in front of markers further up the screen."
        ]
        |> ObsoleteWithMessage "Deprecated: As of February 21st, 2024, google.maps.Marker is deprecated. Please use google.maps.marker.AdvancedMarkerElement instead. Please see https://developers.google.com/maps/deprecations for deprecation details and https://developers.google.com/maps/documentation/javascript/advanced-markers/migration for the migration guide."

    let Marker =
        Class "google.maps.Marker"
        |=> Inherits MVC.MVCObject
        |+> Static [
            Constructor !? MarkerOptions
            |> WithComment "Creates a marker with the options specified. If a map is specified, the marker is added to the map upon construction. Note that the position must be set for the marker to display."

            "MAX_ZINDEX" =@ T<int>
            |> WithComment "The maximum default z-index that the API will assign to a marker. You may set a higher z-index to bring a marker to the front."
            ]
        |+> Instance [
            "getAnimation" => T<unit> ^-> Animation
            |> WithComment "Get the currently running animation."

            "getClickable" => T<unit> ^-> T<bool>
            |> WithComment "Get the clickable status of the Marker."

            "getCursor" => T<unit> ^-> T<string>
            |> WithComment "Get the mouse cursor type shown on hover."

            "getDraggable" => T<unit> ^-> T<bool>
            |> WithComment "Get the draggable status of the Marker."

            "getIcon" => T<unit> ^-> (T<string> + Icon + Symbol)
            |> WithComment "Get the icon of the Marker. See MarkerOptions.icon."

            "getLabel" => T<unit> ^-> (MarkerLabel + T<string>)
            |> WithComment "Get the label of the Marker. See MarkerOptions.label."

            "getMap" => T<unit> ^-> M.Map + StreetView.StreetViewPanorama
            |> WithComment "Get the map or panaroama the Marker is rendered on."

            "getOpacity" => T<unit> ^-> T<int>
            |> WithComment "Get the opacity of the Marker."

            "getPosition" => T<unit> ^-> LatLng
            |> WithComment "Get the position of the Marker."

            "getShape" => T<unit> ^-> MarkerShape
            |> WithComment "Get the shape of the Marker used for interaction. See MarkerOptions.shape and MarkerShape."

            "getTitle" => T<unit> ^-> T<string>
            |> WithComment "Get the title of the Marker tooltip. See MarkerOptions.title."

            "getVisible" => T<unit> ^-> T<bool>
            |> WithComment "Get the visibility of the Marker."

            "getZIndex" => T<unit> ^-> T<int>
            |> WithComment "Get the zIndex of the Marker. See MarkerOptions.zIndex."

            "setAnimation" => !? Animation ^-> T<unit>
            |> WithComment "Start an animation. Any ongoing animation will be cancelled. Currently supported animations are: Animation.BOUNCE, Animation.DROP. Passing in null will cause any animation to stop."

            "setClickable" => T<bool> ^-> T<unit>
            |> WithComment "Set if the Marker is clickable."

            "setCursor" => !? T<string> ^-> T<unit>
            |> WithComment "Set the mouse cursor type shown on hover."

            "setDraggable" => !? T<bool> ^-> T<unit>
            |> WithComment "Set if the Marker is draggable."

            "setIcon" => !? (T<string> + Icon + Symbol) ^-> T<unit>
            |> WithComment "Set the icon for the Marker. See MarkerOptions.icon."

            "setLabel" => !? (MarkerLabel + T<string>) ^-> T<unit>
            |> WithComment "Set the label for the Marker. See MarkerOptions.label."

            "setMap" => (M.Map + StreetView.StreetViewPanorama) ^-> T<unit>
            |> WithComment "Renders the marker on the specified map or panorama. If map is set to null, the marker will be removed."

            "setOpacity" => !? T<int> ^-> T<unit>
            |> WithComment "Set the opacity of the Marker."

            "setOptions" => MarkerOptions ^-> T<unit>
            |> WithComment "Set the options for the Marker."

            "setPosition" => !? (LatLng + LatLngLiteral) ^-> T<unit>
            |> WithComment "Set the postition for the Marker."

            "setShape" => !? MarkerShape ^-> T<unit>
            |> WithComment "Set the shape of the Marker used for interaction. See MarkerOptions.shape and MarkerShape."

            "setTitle" => !? T<string> ^-> T<unit>
            |> WithComment "Set the title of the Marker tooltip. See MarkerOptions.title."

            "setVisible" => T<bool> ^-> T<unit>
            |> WithComment "Set if the Marker is visible."

            "setZIndex" => !? T<int> ^-> T<unit>
            |> WithComment "Set the zIndex of the Marker. See MarkerOptions.zIndex."

            // EVENTS
            "animation_changed" =@ T<obj> -* T<unit> ^-> T<unit>
            |> WithComment "This event is fired when the Marker animation property changes."

            "click" =@ T<obj> -* M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the Marker icon was clicked."

            "clickable_changed" =@ T<obj> -* T<unit> ^-> T<unit>
            |> WithComment "This event is fired when the Marker clickable property changes."

            "contextmenu" =@ T<obj> -* M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM contextmenu event is fired on the Marker"

            "cursor_changed" =@ T<obj> -* T<unit> ^-> T<unit>
            |> WithComment "This event is fired when the Marker cursor property changes."

            "dblclick" =@ T<obj> -* M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the Marker icon was double clicked."

            "drag" =@ T<obj> -* M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is repeatedly fired while the user drags the Marker."

            "dragend" =@ T<obj> -* M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the user stops dragging the Marker."

            "draggable_changed" =@ T<obj> -* T<unit> ^-> T<unit>
            |> WithComment "This event is fired when the Marker draggable property changes."

            "dragstart" =@ T<obj> -* M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the user starts dragging the Marker."

            "flat_changed" =@ T<obj> -* T<unit> ^-> T<unit>
            |> WithComment "This event is fired when the Marker flat property changes."

            "icon_changed" =@ T<obj> -* T<unit> ^-> T<unit>
            |> WithComment "This event is fired when the Marker icon property changes."

            "mousedown" =@ T<obj> -* M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired for a mousedown on the Marker."

            "mouseout" =@ T<obj> -* M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the mouse leaves the area of the Marker icon."

            "mouseover" =@ T<obj> -* M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the mouse enters the area of the Marker icon."

            "mouseup" =@ T<obj> -* M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired for a mouseup on the Marker."

            "position_changed" =@ T<obj> -* T<unit> ^-> T<unit>
            |> WithComment "This event is fired when the Marker position property changes."

            "shape_changed" =@ T<obj> -* T<unit> ^-> T<unit>
            |> WithComment "This event is fired when the Marker shape property changes."

            "title_changed" =@ T<obj> -* T<unit> ^-> T<unit>
            |> WithComment "This event is fired when the Marker title property changes."

            "visible_changed" =@ T<obj> -* T<unit> ^-> T<unit>
            |> WithComment "This event is fired when the Marker visible property changes."

            "zindex_changed" =@ T<obj> -* T<unit> ^-> T<unit>
            |> WithComment "This event is fired when the Marker zIndex property changes."

            "rightclick" =@ T<obj> -* M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired for a rightclick on the Marker."
            |> ObsoleteWithMessage "Deprecated: Use the Marker.contextmenu event instead in order to support usage patterns like control-click on macOS."
        ]
        |> ObsoleteWithMessage "Deprecated: As of February 21st, 2024, google.maps.Marker is deprecated. Please use AdvancedMarkerElement instead. At this time, google.maps.Marker is not scheduled to be discontinued, but AdvancedMarkerElement is recommended over google.maps.Marker. While google.maps.Marker will continue to receive bug fixes for any major regressions, existing bugs in google.maps.Marker will not be addressed. At least 12 months notice will be given before support is discontinued. Please see https://developers.google.com/maps/deprecations for additional details and https://developers.google.com/maps/documentation/javascript/advanced-markers/migration for the migration guide."

    let IconSequence =
        Config "IconSequence"
        |+> Instance [
            "fixedRotation" =@ T<bool>
            |> WithComment "If true, each icon in the sequence has the same fixed rotation regardless of the angle of the edge on which it lies. Defaults to false, in which case each icon in the sequence is rotated to align with its edge."

            "icon" =@ Symbol
            |> WithComment "The icon to render on the line."

            "offset" =@ T<string>
            |> WithComment "The distance from the start of the line at which an icon is to be rendered. This distance may be expressed as a percentage of line's length (e.g. '50%') or in pixels (e.g. '50px'). Defaults to '100%'."

            "repeat" =@ T<string>
            |> WithComment "The distance between consecutive icons on the line. This distance may be expressed as a percentage of the line's length (e.g. '50%') or in pixels (e.g. '50px'). To disable repeating of the icon, specify '0'. Defaults to '0'."
        ]

    let PolyMouseEvent =
        Class "google.maps.PolyMouseEvent"
        |=> Inherits M.MapMouseEvent
        |+> Instance [
            "edge" =@ T<int>
            |> WithComment "The index of the edge within the path beneath the cursor when the event occurred, if the event occurred on a mid-point on an editable polygon."

            "path" =@ T<int>
            |> WithComment "The index of the path beneath the cursor when the event occurred, if the event occurred on a vertex and the polygon is editable. Otherwise undefined."

            "vertex" =@ T<int>
            |> WithComment "The index of the vertex beneath the cursor when the event occurred, if the event occurred on a vertex and the polyline or polygon is editable. If the event does not occur on a vertex, the value is undefined."
        ]

    let PolylineOptions =
        Config "google.maps.PolylineOptions"
        |+> Instance [
            "clickable" =@ T<bool>
            |> WithComment "Indicates whether this Polyline handles mouse events. Defaults to true."

            "draggable" =@ T<bool>
            |> WithComment "If set to true, the user can drag this shape over the map. The geodesic property defines the mode of dragging. Defaults to false."

            "editable" =@ T<bool>
            |> WithComment "If set to true, the user can edit this shape by dragging the control points shown at the vertices and on each segment. Defaults to false."

            "geodesic" =@ T<bool>
            |> WithComment "When true, edges of the polygon are interpreted as geodesic and will follow the curvature of the Earth. When false, edges of the polygon are rendered as straight lines in screen space. Note that the shape of a geodesic polygon may appear to change when dragged, as the dimensions are maintained relative to the surface of the earth. Defaults to false."

            "icons" =@ Type.ArrayOf IconSequence
            |> WithComment "The icons to be rendered along the polyline."

            "map" =@ M.Map.Type
            |> WithComment "Map on which to display Polyline."

            // "path" =@ MVC.MVCArray.[LatLng] + Type.ArrayOf (LatLng + LatLngLiteral)
            "path" =@ MVC.MVCArray.[LatLng] + Type.ArrayOf LatLng + Type.ArrayOf LatLngLiteral
            |> WithComment "The ordered sequence of coordinates of the Polyline. This path may be specified using either a simple array of LatLngs, or an MVCArray of LatLngs. Note that if you pass a simple array, it will be converted to an MVCArray Inserting or removing LatLngs in the MVCArray will automatically update the polyline on the map."

            "strokeColor" =@ T<string>
            |> WithComment "The stroke color. All CSS3 colors are supported except for extended named colors."

            "strokeOpacity" =@ T<float>
            |> WithComment "The stroke opacity between 0.0 and 1.0."

            "strokeWeight" =@ T<int>
            |> WithComment "The stroke width in pixels."

            "visible" =@ T<bool>
            |> WithComment "Whether this polyline is visible on the map. Defaults to true."

            "zIndex" =@ T<int>
            |> WithComment "The zIndex compared to other polys."
        ]

    let Polyline =
        Class "google.maps.Polyline"
        |=> Inherits MVC.MVCObject
        |+> Static [Constructor !? PolylineOptions]
        |+> Instance [
            "getDraggable" => T<unit -> bool>
            |> WithComment "Returns whether this shape can be dragged by the user."

            "getEditable" => T<unit -> bool>
            |> WithComment "Returns whether this shape can be edited by the user."

            "getMap" => T<unit> ^-> M.Map
            |> WithComment "Returns the map on which this shape is attached."

            "getPath" => T<unit> ^-> MVC.MVCArray.[LatLng]
            |> WithComment "Retrieves the first path."

            "getVisible" => T<bool -> unit>
            |> WithComment "Returns whether this poly is visible on the map."

            "setDraggable" => T<bool -> unit>
            |> WithComment "If set to true, the user can drag this shape over the map. The geodesic property defines the mode of dragging."

            "setEditable" => T<bool -> unit>
            |> WithComment "If set to true, the user can edit this shape by dragging the control points shown at the vertices and on each segment."

            "setMap" => M.Map ^-> T<unit>
            |> WithComment "Renders this shape on the specified map. If map is set to null, the shape will be removed."

            "setOptions" => PolylineOptions ^-> T<unit>

            "setPath" => (MVC.MVCArray.[LatLng] + Type.ArrayOf (LatLng + LatLngLiteral)) ^-> T<unit>
            |> WithComment "Sets the first path. See PolylineOptions for more details."

            "setVisible" => T<bool -> unit>
            |> WithComment "Hides this poly if set to false."

            // EVENTS
            "click" => T<obj> -* PolyMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM click event is fired on the Polyline."

            "contextmenu" => T<obj> -* PolyMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM contextmenu event is fired on Poyline."

            "dblclick" => T<obj> -* PolyMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM dblclick event is fired on the Polyline."

            "drag" => T<obj> -* M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is repeatedly fired while the user drags the polyline."

            "dragend" => T<obj> -* M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the user stops dragging the polyline."

            "dragstart" => T<obj> -* M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the user starts dragging the polyline."

            "mousedown" => T<obj> -* PolyMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM mousedown event is fired on the Polyline."

            "mousemove" => T<obj> -* PolyMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM mousemove event is fired on the Polyline."

            "mouseout" => T<obj> -* PolyMouseEvent ^-> T<unit>
            |> WithComment "This event is fired on Polyline mouseout."

            "mouseover" => T<obj> -* PolyMouseEvent ^-> T<unit>
            |> WithComment "This event is fired on Polyline mouseover."

            "mouseup" => T<obj> -* PolyMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM mouseup event is fired on the Polyline."

            "rightclick" => T<obj> -* PolyMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the Polyline is right-clicked on."
            |> ObsoleteWithMessage "Deprecated: Use the Polyline.contextmenu event instead in order to support usage patterns like control-click on macOS."
        ]

    let PolygonOptions =
        Config "google.maps.PolygonOptions"
        |+> Instance [
            "clickable" =@ T<bool>
            |> WithComment "Indicates whether this Polygon handles mouse events. Defaults to true."

            "draggable" =@ T<bool>
            |> WithComment "If set to true, the user can drag this shape over the map. The geodesic property defines the mode of dragging. Defaults to false."

            "editable" =@ T<bool>
            |> WithComment "If set to true, the user can edit this shape by dragging the control points shown at the vertices and on each segment. Defaults to false."

            "fillColor" =@ T<string>
            |> WithComment "The fill color. All CSS3 colors are supported except for extended named colors."

            "fillOpacity" =@ T<float>
            |> WithComment "The fill opacity between 0.0 and 1.0"

            "geodesic" =@ T<bool>
            |> WithComment "When true, edges of the polygon are interpreted as geodesic and will follow the curvature of the Earth. When false, edges of the polygon are rendered as straight lines in screen space. Note that the shape of a geodesic polygon may appear to change when dragged, as the dimensions are maintained relative to the surface of the earth. Defaults to false."

            "map" =@ M.Map.Type
            |> WithComment "Map on which to display Polygon."

            "paths" =@ (MVC.MVCArray.[LatLng] + MVC.MVCArray.[MVC.MVCArray.[LatLng]]
                        + Type.ArrayOf (LatLng + LatLngLiteral) + Type.ArrayOf (Type.ArrayOf (LatLng + LatLngLiteral)))
            |> WithComment "The ordered sequence of coordinates that designates a closed loop. Unlike polylines, a polygon may consist of one or more paths. As a result, the paths property may specify one or more arrays of LatLng coordinates. Paths are closed automatically; do not repeat the first vertex of the path as the last vertex. Simple polygons may be defined using a single array of LatLngs. More complex polygons may specify an array of arrays. Any simple arrays are converted into MVCArrays. Inserting or removing LatLngs from the MVCArray will automatically update the polygon on the map."

            "strokeColor" =@ T<string>
            |> WithComment "The stroke color. All CSS3 colors are supported except for extended named colors."

            "strokeOpacity" =@ T<float>
            |> WithComment "The stroke opacity between 0.0 and 1.0"

            "strokePosition" => StrokePosition
            |> WithComment "The stroke position. Default: StrokePosition.CENTER."

            "strokeWeight" =@ T<int>
            |> WithComment "The stroke width in pixels."

            "visible" =@ T<bool>
            |> WithComment "Whether this polygon is visible on the map. Defaults to true."

            "zIndex" =@ T<int>
            |> WithComment "The zIndex compared to other polys."
        ]

    let Polygon =
        Class "google.maps.Polygon"
        |=> Inherits MVC.MVCObject
        |+> Static [
            Constructor !? PolygonOptions
            |> WithComment "Create a polygon using the passed PolygonOptions, which specify the polygon's path, the stroke style for the polygon's edges, and the fill style for the polygon's interior regions. A polygon may contain one or more paths, where each path consists of an array of LatLngs. You may pass either an array of LatLngs or an MVCArray of LatLngs when constructing these paths. Arrays are converted to MVCArrays within the polygon upon instantiation."
        ]
        |+> Instance [
            "getDraggable" => T<unit -> bool>
            |> WithComment "Returns whether this shape can be dragged by the user."

            "getEditable" => T<unit -> bool>
            |> WithComment "Returns whether this shape can be edited by the user."

            "getMap" => T<unit> ^-> M.Map.Type
            |> WithComment "Returns the map on which this shape is attached."

            "getPath" => T<unit> ^-> MVC.MVCArray.[LatLng]
            |> WithComment "Retrieves the first path."

            "getPaths" => T<unit> ^-> MVC.MVCArray.[MVC.MVCArray.[LatLng]]
            |> WithComment "Retrieves the paths for this polygon."

            "getVisible" => T<unit -> bool>
            |> WithComment "Returns whether this poly is visible on the map."

            "setDraggable" => T<bool -> unit>
            |> WithComment "If set to true, the user can drag this shape over the map. The geodesic property defines the mode of dragging."

            "setEditable" => T<bool -> unit>
            |> WithComment "If set to true, the user can edit this shape by dragging the control points shown at the vertices and on each segment."

            "setMap" => M.Map ^-> T<unit>
            |> WithComment "Renders this shape on the specified map. If map is set to null, the shape will be removed."

            "setOptions" => PolygonOptions ^-> T<unit>

            "setPath" => MVC.MVCArray.[LatLng] + Type.ArrayOf (LatLng + LatLngLiteral) ^-> T<unit>
            |> WithComment "Sets the first path. See PolylineOptions for more details."

            "setPaths" => (MVC.MVCArray.[MVC.MVCArray.[LatLng]] + MVC.MVCArray.[LatLng]
                            + Type.ArrayOf (Type.ArrayOf (LatLng  + LatLngLiteral)) + Type.ArrayOf (LatLng + LatLngLiteral)) ^-> T<unit>
            |> WithComment "Sets the path for this polygon."

            "setVisible" => T<bool -> unit>
            |> WithComment "Hides this poly if set to false."

            // EVENTS
            "click" => T<obj> -* PolyMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM click event is fired on the Polygon."

            "contextmenu" => T<obj> -* PolyMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM contextmenu event is fired on the Polygon."

            "dblclick" => T<obj> -* PolyMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM dblclick event is fired on the Polygon."

            "drag" => T<obj> -* M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is repeatedly fired while the user drags the polygon."

            "dragend" => T<obj> -* M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the user stops dragging the polygon."

            "dragstart" => T<obj> -* M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the user starts dragging the polygon."

            "mousedown" => T<obj> -* PolyMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM mousedown event is fired on the Polygon."

            "mousemove" => T<obj> -* PolyMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM mousemove event is fired on the Polygon."

            "mouseout" => T<obj> -* PolyMouseEvent ^-> T<unit>
            |> WithComment "This event is fired on Polygon mouseout."

            "mouseover" => T<obj> -* PolyMouseEvent ^-> T<unit>
            |> WithComment "This event is fired on Polygon mouseover."

            "mouseup" => T<obj> -* PolyMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM mouseup event is fired on the Polygon."

            "rightclick" => T<obj> -* PolyMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the Polygon is right-clicked on."
            |> ObsoleteWithMessage "Deprecated: Use the Polygon.contextmenu event instead in order to support usage patterns like control-click on macOS."
        ]

    let AdvancedMarkerClickEvent =
        Class "google.maps.marker.AdvancedMarkerClickEvent"
        |=> Inherits Events.Event

    let AdvancedMarkerElementOptions =
        Interface "google.maps.marker.AdvancedMarkerElementOptions"
        |+> [
            "collisionBehavior" =@ CollisionBehavior
            |> WithComment """An enumeration specifying how an AdvancedMarkerElement should behave when it collides with another AdvancedMarkerElement or with the basemap labels on a vector map.

Note: AdvancedMarkerElement to AdvancedMarkerElement collision works on both raster and vector maps, however, AdvancedMarkerElement to base map's label collision only works on vector maps."""

            "content" =@ Node
            |> WithComment """The DOM Element backing the visual of an AdvancedMarkerElement.

Note: AdvancedMarkerElement does not clone the passed-in DOM element. Once the DOM element is passed to an AdvancedMarkerElement, passing the same DOM element to another AdvancedMarkerElement will move the DOM element and cause the previous AdvancedMarkerElement to look empty. Default: PinElement.element"""

            "gmpClickable" =@ T<bool>
            |> WithComment "If true, the AdvancedMarkerElement will be clickable and trigger the gmp-click event, and will be interactive for accessibility purposes (e.g. allowing keyboard navigation via arrow keys). Default: false"

            "gmpDraggable" =@ T<bool>
            |> WithComment "If true, the AdvancedMarkerElement can be dragged. Note: AdvancedMarkerElement with altitude is not draggable. Default: false"

            "map" =@ M.Map.Type
            |> WithComment "Map on which to display the AdvancedMarkerElement. The map is required to display the AdvancedMarkerElement and can be provided by setting AdvancedMarkerElement.map if not provided at the construction."

            "position" =@ (LatLng + LatLngLiteral + LatLngAltitude + LatLngAltitudeLiteral)
            |> WithComment """Sets the AdvancedMarkerElement's position. An AdvancedMarkerElement may be constructed without a position, but will not be displayed until its position is provided - for example, by a user's actions or choices. An AdvancedMarkerElement's position can be provided by setting AdvancedMarkerElement.position if not provided at the construction.

Note: AdvancedMarkerElement with altitude is only supported on vector maps."""

            "title" =@ T<string>
            |> WithComment "Rollover text. If provided, an accessibility text (e.g. for use with screen readers) will be added to the AdvancedMarkerElement with the provided value."

            "zIndex" =@ T<int>
            |> WithComment "All AdvancedMarkerElements are displayed on the map in order of their zIndex, with higher values displaying in front of AdvancedMarkerElements with lower values. By default, AdvancedMarkerElements are displayed according to their vertical position on screen, with lower AdvancedMarkerElements appearing in front of AdvancedMarkerElements farther up the screen. Note that zIndex is also used to help determine relative priority between CollisionBehavior.OPTIONAL_AND_HIDES_LOWER_PRIORITY Advanced Markers. A higher zIndex value indicates higher priority."
        ]

    let AdvancedMarkerElement =
        Class "google.maps.marker.AdvancedMarkerElement"
        |=> Inherits HTMLElement
        |=> Implements [AdvancedMarkerElementOptions]
        |+> Static [
            Ctor [
                !? AdvancedMarkerElementOptions?options
            ]
        ]
        |+> Instance [
            // "collisionBehavior" =@ CollisionBehavior
            // |> WithComment "See AdvancedMarkerElementOptions.collisionBehavior."

            // "content" =@ Node
            // |> WithComment "See AdvancedMarkerElementOptions.content."

            "element" =? HTMLElement
            |> WithComment "This field is read-only. The DOM Element backing the view."

            // "gmpClickable" =@ T<bool>
            // |> WithComment "See BetaAdvancedMarkerElementOptions.gmpClickable."

            // "gmpDraggable" =@ T<bool>
            // |> WithComment "See AdvancedMarkerElementOptions.gmpDraggable."

            // "map" =@ M.Map.Type
            // |> WithComment "See AdvancedMarkerElementOptions.map."

            // "position" =@ (LatLng + LatLngLiteral + LatLngAltitude + LatLngAltitudeLiteral)
            // |> WithComment "See AdvancedMarkerElementOptions.position."

            // "title" =@ T<string>
            // |> WithComment "See AdvancedMarkerElementOptions.title."

            // "zIndex" =@ T<int>
            // |> WithComment "See AdvancedMarkerElementOptions.zIndex."

            // METHODS
            //TODO: where is EventListener?
            // "addEventListener" => T<string> * (EventListener + EventListenerObject) * !?(T<bool> + AddEventListenerOptions) ^-> T<unit>
            "addEventListener" => T<string> * T<obj -> unit> * !?(T<bool> + AddEventListenerOptions) ^-> T<unit>
            |> WithComment "Sets up a function that will be called whenever the specified event is delivered to the target. See addEventListener"

            //TODO: where is EventListener?
            // "removeEventListener" => T<string> * (EventListener + EventListenerObject) * !?(T<bool> + EventListenerOptions) ^-> T<unit>
            "removeEventListener" => T<string> * T<obj -> unit> * !?(T<bool> + EventListenerOptions) ^-> T<unit>
            |> WithComment "Removes an event listener previously registered with addEventListener from the target. See removeEventListener"

            // EVENTS
            "click" => T<obj> -* M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the AdvancedMarkerElement element is clicked. Not available with addEventListener() (use gmp-click instead)."

            "drag" => T<obj> -* M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is repeatedly fired while the user drags the AdvancedMarkerElement. Not available with addEventListener()."

            "dragend" => T<obj> -* M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the user stops dragging the AdvancedMarkerElement. Not available with addEventListener()."

            "dragstart" => T<obj> -* M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the user starts dragging the AdvancedMarkerElement. Not available with addEventListener()."

            "gmp-click" => T<obj> -* AdvancedMarkerClickEvent ^-> T<unit>
            |> WithComment "This event is fired when the AdvancedMarkerElement element is clicked. Best used with addEventListener() (instead of addListener())."
        ]

    let InfoWindowOptions =
        Config "google.maps.InfoWindowOptions"
        |+> Instance [
                    "ariaLabel" =@ T<string>
                    |> WithComment "AriaLabel to assign to the InfoWindow."

                    "content" =@ (T<string> + Node)
                    |> WithComment "Content to display in the InfoWindow. This can be an HTML element, a plain-text string, or a string containing HTML. The InfoWindow will be sized according to the content. To set an explicit size for the content, set content to be a HTML element with that size."

                    "disableAutoPan" =@ T<bool>
                    |> WithComment "Disable auto-pan on open. By default, the info window will pan the map so that it is fully visible when it opens."

                    "maxWidth" =@ T<int>
                    |> WithComment "Maximum width of the InfoWindow, regardless of content's width. This value is only considered if it is set before a call to open(). To change the maximum width when changing content, call close(), setOptions(), and then open()."

                    "minWidth" =@ T<int>
                    |> WithComment "Minimum width of the InfoWindow, regardless of the content's width. When using this property, it is strongly recommended to set the minWidth to a value less than the width of the map (in pixels). This value is only considered if it is set before a call to open(). To change the minimum width when changing content, call close(), setOptions(), and then open()."

                    "pixelOffset" =@ Size
                    |> WithComment "The offset, in pixels, of the tip of the info window from the point on the map at whose geographical coordinates the info window is anchored. If an InfoWindow is opened with an anchor, the pixelOffset will be calculated from the anchor's anchorPoint property."

                    "position" =@ LatLng + LatLngLiteral
                    |> WithComment "The LatLng at which to display this InfoWindow. If the InfoWindow is opened with an anchor, the anchor's position will be used instead."

                    "zIndex" =@ T<int>
                    |> WithComment "All InfoWindows are displayed on the map in order of their zIndex, with higher values displaying in front of InfoWindows with lower values. By default, InfoWindows are displayed according to their latitude, with InfoWindows of lower latitudes appearing in front of InfoWindows at higher latitudes. InfoWindows are always displayed in front of markers."
                ]

    let InfoWindowOpenOptions =
        Config "google.maps.InfoWindowOpenOptions"
        |+> Instance [
            "ariaLabel" =@ MVC.MVCObject + AdvancedMarkerElement
            |> WithComment "The anchor to which this InfoWindow will be positioned. If the anchor is non-null, the InfoWindow will be positioned at the top-center of the anchor. The InfoWindow will be rendered on the same map or panorama as the anchor (when available)."

            "map" =@ M.Map + StreetView.StreetViewPanorama
            |> WithComment "The map or panorama on which to render this InfoWindow."

            "shouldFocus" =@ T<bool>
            |> WithComment "Whether or not focus should be moved inside the InfoWindow when it is opened. When this property is unset or when it is set to null or undefined, a heuristic is used to decide whether or not focus should be moved. It is recommended to explicitly set this property to fit your needs as the heuristic is subject to change and may not work well for all use cases."
        ]

    let InfoWindow =
        Class "google.maps.InfoWindow"
        |=> Inherits MVC.MVCObject
        |+> Static [
            Constructor !? InfoWindowOptions
            |> WithComment "Creates an info window with the given options. An InfoWindow can be placed on a map at a particular position or above a marker, depending on what is specified in the options. Unless auto-pan is disabled, an InfoWindow will pan the map to make itself visible when it is opened. After constructing an InfoWindow, you must call open to display it on the map. The user can click the close button on the InfoWindow to remove it from the map, or the developer can call close() for the same effect."
        ]
        |+> Instance [

            "close" => T<unit> ^-> T<unit>
            |> WithComment "Closes this InfoWindow by removing it from the DOM structure."

            "focus" => T<unit> ^-> T<unit>
            |> WithComment "Sets focus on this InfoWindow. You may wish to consider using this method along with a visible event to make sure that InfoWindow is visible before setting focus on it. An InfoWindow that is not visible cannot be focused."

            "getContent" => T<unit> ^-> T<string> + Node

            "getPosition" => T<unit> ^-> LatLng

            "getZIndex" => T<unit> ^-> T<int>

            "open" => ((InfoWindowOpenOptions + M.Map + StreetView.StreetViewPanorama) * !? (AdvancedMarkerElement + MVC.MVCObject)) ^-> T<unit>
            |> WithComment "Opens this InfoWindow on the given map. Optionally, an InfoWindow can be associated with an anchor. In the core API, the only anchor is the Marker class. However, an anchor can be any MVCObject that exposes a LatLng position property and optionally a Point anchorPoint property for calculating the pixelOffset (see InfoWindowOptions). The anchorPoint is the offset from the anchor's position to the tip of the InfoWindow. It is recommended to use the InfoWindowOpenOptions interface as the single argument for this method. To prevent changing browser focus on open, set InfoWindowOpenOptions.shouldFocus to false."

            "setContent" => (T<string>  + Node) ^-> T<unit>

            "setOptions" => InfoWindowOptions ^-> T<unit>

            "setPosition" => LatLng + LatLngLiteral ^-> T<unit>

            "setZIndex" => T<int> ^-> T<unit>

            // EVENTS
            "closeclick" => T<obj> -* T<unit> ^-> T<unit>
            |> WithComment "This event is fired when the close button was clicked."

            "content_changed" => T<obj> -* T<unit> ^-> T<unit>
            |> WithComment "This event is fired when the content property changes."

            "domready" => T<obj> -* T<unit> ^-> T<unit>
            |> WithComment "This event is fired when the <div> containing the InfoWindow's content is attached to the DOM. You may wish to monitor this event if you are building out your info window content dynamically."

            "position_changed" => T<obj> -* T<unit> ^-> T<unit>
            |> WithComment "This event is fired when the position property changes."

            "visible" => T<obj> -* T<unit> ^-> T<unit>
            |> WithComment "This event is fired when the InfoWindow is fully visible. This event is not fired when InfoWindow is panned off and then back on screen."

            "zindex_changed" => T<obj> -* T<unit> ^-> T<unit>
            |> WithComment "This event is fired when the InfoWindow's zIndex changes."
        ]

    let Place =
        Interface "google.maps.Place"
        |+> [
            "location" =@ LatLng + LatLngLiteral
            |> WithComment "The LatLng of the entity described by this place."

            "placeId" =@ T<string>
            |> WithComment "The place ID of the place (such as a business or point of interest). The place ID is a unique identifier of a place in the Google Maps database. Note that the placeId is the most accurate way of identifying a place. If possible, you should specify the placeId rather than a query. A place ID can be retrieved from any request to the Places API, such as a TextSearch. Place IDs can also be retrieved from requests to the Geocoding API. For more information, see the overview of place IDs."

            "query" =@ T<string>
            |> WithComment "A search query describing the place (such as a business or point of interest). An example query is \"Quay, Upper Level, Overseas Passenger Terminal 5 Hickson Road, The Rocks NSW\". If possible, you should specify the placeId rather than a query. The API does not guarantee the accuracy of resolving the query string to a place. If both the placeId and query are provided, an error occurs."
        ]

    let GeocoderComponentRestrictions =
        Config "google.maps.GeocoderComponentRestrictions"
        |+> Instance [
            "administrativeArea" =@ T<string>
            |> WithComment "Matches all the administrative_area levels. Optional."

            "country" =@ T<string>
            |> WithComment "Matches a country name or a two letter ISO 3166-1 country code. Optional."

            "locality" =@ T<string>
            |> WithComment "Matches against both locality and sublocality types. Optional."

            "postalCode" =@ T<string>
            |> WithComment "Matches postal_code and postal_code_prefix. Optional."

            "route" =@ T<string>
            |> WithComment "Matches the long or short name of a route. Optional."
        ]

    let GeocoderRequest =
        Config "GeocoderRequest"
        |+> Instance [
            "address" =@ T<string>
            |> WithComment "Address to geocode. One, and only one, of address, location and placeId must be supplied."

            "bounds" =@ LatLngBounds + LatLngBoundsLiteral
            |> WithComment "LatLngBounds within which to search. Optional."

            "componentRestrictions" =@ GeocoderComponentRestrictions
            |> WithComment "Components are used to restrict results to a specific area. A filter consists of one or more of: route, locality, administrativeArea, postalCode, country. Only the results that match all the filters will be returned. Filter values support the same methods of spelling correction and partial matching as other geocoding requests. Optional."

            "language" =@ T<string>
            |> WithComment "A language identifier for the language in which results should be returned, when possible. See the list of supported languages."

            "location" =@ LatLng + LatLngLiteral
            |> WithComment "LatLng (or LatLngLiteral) for which to search. The geocoder performs a reverse geocode. See Reverse Geocoding for more information. One, and only one, of address, location and placeId must be supplied."

            "placeId" =@ T<string>
            |> WithComment "The place ID associated with the location. Place IDs uniquely identify a place in the Google Places database and on Google Maps. Learn more about place IDs in the Places API developer guide. The geocoder performs a reverse geocode. See Reverse Geocoding for more information. One, and only one, of address, location and placeId must be supplied."

            "region" =@ T<string>
            |> WithComment "Country code used to bias the search, specified as a two-character (non-numeric) Unicode region subtag / CLDR identifier. Optional. See Google Maps Platform Coverage Details for supported regions."
        ]

    let GeocoderStatus =
        Class "google.maps.GeocoderStatus"
        |+> Static [
            "ERROR" =? TSelf
            |> WithComment "There was a problem contacting the Google servers."

            "INVALID_REQUEST" =? TSelf
            |> WithComment "This GeocoderRequest was invalid."

            "OK" =? TSelf
            |> WithComment "The response contains a valid GeocoderResponse."

            "OVER_QUERY_LIMIT" =? TSelf
            |> WithComment "The webpage has gone over the requests limit in too short a period of time."

            "REQUEST_DENIED" =? TSelf
            |> WithComment "The webpage is not allowed to use the geocoder."

            "UNKNOWN_ERROR" =? TSelf
            |> WithComment "A geocoding request could not be processed due to a server error. The request may succeed if you try again."

            "ZERO_RESULTS" =? TSelf
            |> WithComment "No result was found for this GeocoderRequest."
        ]

    let GeocoderLocationType =
        Class "google.maps.GeocoderLocationType"
        |+> Static [
            "APPROXIMATE" =? TSelf
            |> WithComment "The returned result is approximate."

            "GEOMETRIC_CENTER" =? TSelf
            |> WithComment "The returned result is the geometric center of a result such a line (e.g. street) or polygon (region)."

            "RANGE_INTERPOLATED" =? TSelf
            |> WithComment "The returned result reflects an approximation (usually on a road) interpolated between two precise points (such as intersections). Interpolated results are generally returned when rooftop geocodes are unavailable for a street address."

            "ROOFTOP" =? TSelf
            |> WithComment "The returned result reflects a precise geocode."
        ]

    let GeocoderAddressComponent =
        Class "google.maps.GeocoderAddressComponent"
        |+> Instance [
            "long_name" =@ T<string>
            |> WithComment "The full text of the address component"

            "short_name" =@ T<string>
            |> WithComment "The abbreviated, short text of the given address component"

            "types" =@ Type.ArrayOf T<string>
            |> WithComment "An array of strings denoting the type of this address component. A list of valid types can be found at https://developers.google.com/maps/documentation/javascript/geocoding#GeocodingAddressTypes"
        ]

    let GeocoderGeometry =
        Class "google.maps.GeocoderGeometry"
        |+> Instance [
            "bounds" =@ LatLngBounds
            |> WithComment "The precise bounds of this GeocoderResult, if applicable"

            "location" =@ LatLng
            |> WithComment "The latitude/longitude coordinates of this result"

            "location_type" =@ GeocoderLocationType
            |> WithComment "The type of location returned in location"

            "viewport" =@ LatLngBounds
            |> WithComment "The bounds of the recommended viewport for displaying this GeocoderResult"
        ]

    let GeocoderResult =
        Class "google.maps.GeocoderResult"
        |+> Instance [
            "address_components" =@ Type.ArrayOf GeocoderAddressComponent
            |> WithComment "An array of GeocoderAddressComponents"

            "formatted_address" =@ T<string>
            |> WithComment "A string containing the human-readable address of this location."

            "geometry" =@ GeocoderGeometry
            |> WithComment "A GeocoderGeometry object"

            "place_id" =@ T<string>
            |> WithComment "The place ID associated with the location. Place IDs uniquely identify a place in the Google Places database and on Google Maps. Learn more about Place IDs in the Places API developer guide."

            "types" =@ T<string[]>
            |> WithComment "An array of strings denoting the type of the returned geocoded element. For a list of possible strings, refer to the Address Component Types section of the Developer's Guide."

            "partial_match" =@ T<bool>
            |> WithComment "Whether the geocoder did not return an exact match for the original request, though it was able to match part of the requested address. If an exact match, the value will be undefined."

            "plus_code" =@ Forward.PlacePlusCode
            |> WithComment "The plus code associated with the location."

            "postcode_localities" =@ T<string[]>
            |> WithComment "An array of strings denoting all the localities contained in a postal code. This is only present when the result is a postal code that contains multiple localities."
        ]

    let GeocoderResponse =
        Config "google.maps.GeocoderResponse"
        |+> Instance [
            "results" =@ Type.ArrayOf GeocoderResult
            |> WithComment "The list of GeocoderResults."
        ]

    let Geocoder =
        Class "google.maps.Geocoder"
        // |=> Inherits MVC.MVCObject
        |+> Static [Constructor T<unit>]
        |+> Instance [
            "geocode" => GeocoderRequest * ((Type.ArrayOf GeocoderResult * GeocoderStatus) ^-> T<unit>) ^-> Promise[GeocoderResponse]
            |> WithComment "Geocode a request."
        ]

    let Distance =
        Class "google.maps.Distance"
        |+> Instance [
            "text" =? T<string>
            |> WithComment "A string representation of the distance value, using the UnitSystem specified in the request."

            "value" =? T<float>
            |> WithComment "The distance in meters."
        ]

    let Duration =
        Class "google.maps.Duration"
        |+> Instance [
            "text" =? T<string>
            |> WithComment "A string representation of the duration value."

            "value" =? T<float>
            |> WithComment "The duration in seconds."
        ]

    let Time =
        Class "google.maps.Time"
        |+> Instance [
            "text" =? T<string>
            |> WithComment "A string representing the time's value. The time is displayed in the time zone of the transit stop."

            "time_zone" =? T<string>
            |> WithComment "The time zone in which this stop lies. The value is the name of the time zone as defined in the IANA Time Zone Database, e.g. \"America/New_York\"."

            "value" =? Date
            |> WithComment "The time of this departure or arrival, specified as a JavaScript Date object."
        ]

    let TransitStop =
        Class "google.maps.TransitStop"
        |+> Instance [
            "location" =? LatLng
            |> WithComment "The location of this stop."

            "name" =? T<string>
            |> WithComment "The name of this transit stop."
        ]

    let VehicleType =
        Class "google.maps.VehicleType"
        |+> Static [
            "BUS" =? TSelf
            |> WithComment "Bus."

            "CABLE_CAR" =? TSelf
            |> WithComment "A vehicle that operates on a cable, usually on the ground. Aerial cable cars may be of the type GONDOLA_LIFT."

            "COMMUTER_TRAIN" =? TSelf
            |> WithComment "Commuter rail."

            "FERRY" =? TSelf
            |> WithComment "Ferry."

            "FUNICULAR" =? TSelf
            |> WithComment "A vehicle that is pulled up a steep incline by a cable."

            "GONDOLA_LIFT" =? TSelf
            |> WithComment "An aerial cable car."

            "HEAVY_RAIL" =? TSelf
            |> WithComment "Heavy rail."

            "HIGH_SPEED_TRAIN" =? TSelf
            |> WithComment "High speed train."

            "INTERCITY_BUS" =? TSelf
            |> WithComment "Intercity bus."

            "METRO_RAIL" =? TSelf
            |> WithComment "Light rail."

            "MONORAIL" =? TSelf
            |> WithComment "Monorail."

            "OTHER" =? TSelf
            |> WithComment "Other vehicles."

            "RAIL" =? TSelf
            |> WithComment "Rail."

            "SHARE_TAXI" =? TSelf
            |> WithComment "Share taxi is a sort of bus transport with ability to drop off and pick up passengers anywhere on its route. Generally share taxi uses minibus vehicles."

            "SUBWAY" =? TSelf
            |> WithComment "Underground light rail."

            "TRAM" =? TSelf
            |> WithComment "Above ground light rail."

            "TROLLEYBUS" =? TSelf
            |> WithComment "Trolleybus."
        ]

    let TransitVehicle =
        Class "google.maps.TransitVehicle"
        |+> Instance [
            "icon" =? T<string>
            |> WithComment "A URL for an icon that corresponds to the type of vehicle used on this line."

            "local_icon" =? T<string>
            |> WithComment "A URL for an icon that corresponds to the type of vehicle used in this region instead of the more general icon."

            "name" =? T<string>
            |> WithComment "A name for this type of TransitVehicle, e.g. \"Train\" or \"Bus\"."

            "type" =? VehicleType
            |> WithComment "The type of vehicle used, e.g. train, bus, or ferry."
        ]

    let TransitAgency =
        Class "google.maps.TransitAgency"
        |+> Instance [
            "name" =? T<string>
            |> WithComment "The name of this transit agency."

            "phone" =? T<string>
            |> WithComment "The transit agency's phone number."

            "url" =? T<string>
            |> WithComment "The transit agency's URL."
        ]

    let TransitLine =
        Class "google.maps.TransitLine"
        |+> Instance [
            "agencies" =? Type.ArrayOf TransitAgency
            |> WithComment "The transit agency that operates this transit line."

            "color" =? T<string>
            |> WithComment "The color commonly used in signage for this transit line, represented as a hex string."

            "icon" =? T<string>
            |> WithComment "The URL for an icon associated with this line."

            "name" =? T<string>
            |> WithComment "The full name of this transit line, e.g. \"8 Avenue Local\"."

            "short_name" =? T<string>
            |> WithComment "The short name of this transit line, e.g. \"E\"."

            "text_color" =? T<string>
            |> WithComment "The text color commonly used in signage for this transit line, represented as a hex string."

            "url" =? T<string>
            |> WithComment "The agency's URL which is specific to this transit line."

            "vehicle" =? TransitVehicle
            |> WithComment "The type of vehicle used, e.g. train or bus."
        ]

    let TransitDetails =
        Class "google.maps.TransitDetails"
        |+> Instance [
            "arrival_stop" =? TransitStop
            |> WithComment "The arrival stop of this transit step."

            "arrival_time" =? Time
            |> WithComment "The arrival time of this step, specified as a Time object."

            "departure_stop" =? TransitStop
            |> WithComment "The departure stop of this transit step."

            "departure_time" =? Time
            |> WithComment "The departure time of this step, specified as a Time object."

            "headsign" =? T<string>
            |> WithComment "The direction in which to travel on this line, as it is marked on the vehicle or at the departure stop."

            "headway" =? T<int>
            |> WithComment "The expected number of seconds between equivalent vehicles at this stop."

            "line" =? TransitLine
            |> WithComment "Details about the transit line used in this step."

            "num_stops" =? T<int>
            |> WithComment "The number of stops on this step. Includes the arrival stop, but not the departure stop."

            "trip_short_name" =? T<string>
            |> WithComment "The text that appears in schedules and sign boards to identify a transit trip to passengers, for example, to identify train numbers for commuter rail trips. The text uniquely identifies a trip within a service day."
        ]

    let TravelMode =
        Class "google.maps.TravelMode"
        |+> Static [
            "BICYCLING" =? TSelf
            |> WithComment "Specifies a bicycling directions request."

            "DRIVING" =? TSelf
            |> WithComment "Specifies a driving directions request."

            "TRANSIT" =? TSelf
            |> WithComment "Specifies a transit directions request."

            "WALKING" =? TSelf
            |> WithComment "Specifies a walking directions request."
        ]

    let TransitFare =
        Interface "google.maps.TransitFare"
        |+> [
            "currency" =@ T<string>
            |> WithComment "An ISO 4217 currency code indicating the currency in which the fare is expressed."

            "value" =@ T<float>
            |> WithComment "The numerical value of the fare, expressed in the given currency."
        ]

    let DirectionsPolyline =
        Interface "google.maps.DirectionsPolyline"
        |+> [
            "points" =@ T<string>
            |> WithComment "An encoded polyline."
        ]

    let DirectionsStep =
        Class "google.maps.DirectionsStep"
        |+> Instance [
            "encoded_lat_lngs" =? T<string>
            |> WithComment "An encoded polyline representation of the step. This is an approximate (smoothed) path of the step."

            "distance" =? Distance
            |> WithComment "The distance covered by this step. This property may be undefined as the distance may be unknown."

            "duration" =? Duration
            |> WithComment "The typical time required to perform this step in seconds and in text form. This property may be undefined as the duration may be unknown."

            "end_location" =? LatLng
            |> WithComment "The ending location of this step."

            "end_point" =? LatLng
            |> WithComment "The ending location of this step."
            |> ObsoleteWithMessage "Deprecated: Please use DirectionsStep.end_location."

            "instructions" =? T<string>
            |> WithComment "Instructions for this step."

            "lat_lngs" =? Type.ArrayOf LatLng
            |> WithComment "A sequence of LatLngs describing the course of this step. This is an approximate (smoothed) path of the step."
            |> ObsoleteWithMessage "Deprecated: Please use DirectionsStep.path."

            "maneuver" =? T<string>
            |> WithComment "Contains the action to take for the current step (turn-left, merge, straight, etc.). Values are subject to change, and new values may be introduced without prior notice."

            "path" =? Type.ArrayOf LatLng
            |> WithComment "A sequence of LatLngs describing the course of this step. This is an approximate (smoothed) path of the step."

            "polyline" =? DirectionsPolyline
            |> WithComment "Contains an object with a single property, 'points', that holds an encoded polyline representation of the step. This polyline is an approximate (smoothed) path of the step."
            |> ObsoleteWithMessage "Deprecated: Please use DirectionsStep.encoded_lat_lngs."

            "start_location" =? LatLng
            |> WithComment "The starting location of this step."

            "start_point" =? LatLng
            |> WithComment "The starting location of this step."
            |> ObsoleteWithMessage "Deprecated: Please use DirectionsStep.start_location."

            "steps" =? TSelf
            |> WithComment "Sub-steps of this step. Specified for non-transit sections of transit routes."

            "transit" =? TransitDetails
            |> WithComment "Transit-specific details about this step. This property will be undefined unless the travel mode of this step is TRANSIT."

            "transit_details" =? TransitDetails
            |> WithComment "Details pertaining to this step if the travel mode is TRANSIT."

            "travel_mode" =? TravelMode
            |> WithComment "The mode of travel used in this step."
        ]

    let DirectionsLeg =
        Class "google.maps.DirectionsLeg"
        |+> Instance [
            "arrival_time" =? Time
            |> WithComment "An estimated arrival time for this leg. Only applicable for TRANSIT requests."

            "departur_time" =? Time
            |> WithComment "An estimated departure time for this leg. Only applicable for TRANSIT requests."

            "distance" =? Distance
            |> WithComment "The total distance covered by this leg. This property may be undefined as the distance may be unknown."

            "duration" =? Duration
            |> WithComment "The total duration of this leg. This property may be undefined as the duration may be unknown."

            "duration_in_traffic" =? Duration
            |> WithComment "The total duration of this leg, taking into account current traffic conditions. This property may be undefined as the duration may be unknown. Only available to Maps API for Business customers when durationInTraffic is set to true when making the request."

            "end_address" =? T<string>
            |> WithComment "The address of the destination of this leg. This content is meant to be read as-is. Do not programmatically parse the formatted address."

            "end_location" =? LatLng
            |> WithComment "The DirectionsService calculates directions between locations by using the nearest transportation option (usually a road) at the start and end locations. end_location indicates the actual geocoded destination, which may be different than the end_location of the last step if, for example, the road is not near the destination of this leg."

            "start_address" =? T<string>
            |> WithComment "The address of the origin of this leg. This content is meant to be read as-is. Do not programmatically parse the formatted address."

            "start_location" =? LatLng
            |> WithComment "The DirectionsService calculates directions between locations by using the nearest transportation option (usually a road) at the start and end locations. start_location indicates the actual geocoded origin, which may be different than the start_location of the first step if, for example, the road is not near the origin of this leg."

            "steps" =? Type.ArrayOf DirectionsStep
            |> WithComment "An array of DirectionsSteps, each of which contains information about the individual steps in this leg."

            "traffic_speed_entry" =? T<obj[]>
            |> WithComment "Information about traffic speed along the leg."
            |> ObsoleteWithMessage "Deprecated: This array will always be empty."

            "via_waypoints" =? Type.ArrayOf LatLng
            |> WithComment "An array of non-stopover waypoints along this leg, which were specified in the original request.

Deprecated in alternative routes. Version 3.27 will be the last version of the API that adds extra via_waypoints in alternative routes.

When using the Directions Service to implement draggable directions, it is recommended to disable dragging of alternative routes. Only the main route should be draggable. Users can drag the main route until it matches an alternative route."
        ]

    let DirectionsRoute =
        Class "google.maps.DirectionsRoute"
        |+> Instance [
            "bounds" =? LatLngBounds
            |> WithComment "The bounds for this route."

            "copyrights" =? T<string>
            |> WithComment "Copyrights text to be displayed for this route."

            "legs" =? Type.ArrayOf DirectionsLeg
            |> WithComment "An array of DirectionsLegs, each of which contains information about the steps of which it is composed. There will be one leg for each waypoint or destination specified. So a route with no waypoints will contain one DirectionsLeg and a route with one waypoint will contain two."

            "overview_path" =? Type.ArrayOf LatLng
            |> WithComment "An array of LatLngs representing the entire course of this route. The path is simplified in order to make it suitable in contexts where a small number of vertices is required (such as Static Maps API URLs)."

            "overview_polyline" =? T<string>
            |> WithComment "An encoded polyline representation of the route in overview_path. This polyline is an approximate (smoothed) path of the resulting directions."

            "summary" =? T<string>
            |> WithComment "Contains a short textual description for the route, suitable for naming and disambiguating the route from alternatives."

            "warnings" =? Type.ArrayOf T<string>
            |> WithComment "Warnings to be displayed when showing these directions."

            "waypoint_order" =? Type.ArrayOf T<int>
            |> WithComment "If optimizeWaypoints was set to true, this field will contain the re-ordered permutation of the input waypoints. For example, if the input was:
  Origin: Los Angeles
  Waypoints: Dallas, Bangor, Phoenix
  Destination: New York
and the optimized output was ordered as follows:
  Origin: Los Angeles
  Waypoints: Phoenix, Dallas, Bangor
  Destination: New York
then this field will be an Array containing the values [2, 0, 1]. Note that the numbering of waypoints is zero-based.
If any of the input waypoints has stopover set to false, this field will be empty, since route optimization is not available for such queries.
            "

            "fare" =? TransitFare
            |> WithComment "The total fare for the whole transit trip. Only applicable to transit requests."
        ]

    let DirectionsGeocodedWaypoint =
        Interface "google.maps.DirectionsGeocodedWaypoint"
        |+> [
            "partial_match" =@ T<bool>
            |> WithComment "Whether the geocoder did not return an exact match for the original waypoint, though it was able to match part of the requested address."

            "place_id" =@ T<string>
            |> WithComment "The place ID associated with the waypoint. Place IDs uniquely identify a place in the Google Places database and on Google Maps. Learn more about Place IDs in the Places API developer guide."

            "types" =@ T<string[]>
            |> WithComment "An array of strings denoting the type of the returned geocoded element. For a list of possible strings, refer to the Address Component Types section of the Developer's Guide."
        ]

    let DirectionsStatus =
        Class "google.maps.DirectionsStatus"
        |+> Static [
            "INVALID_REQUEST" =? TSelf
            |> WithComment "The DirectionsRequest provided was invalid."

            "MAX_WAYPOINTS_EXCEEDED" =? TSelf
            |> WithComment "Too many DirectionsWaypoints were provided in the DirectionsRequest. See the developer's guide for the maximum number of waypoints allowed."

            "NOT_FOUND" =? TSelf
            |> WithComment "At least one of the origin, destination, or waypoints could not be geocoded."

            "OK" =? TSelf
            |> WithComment "The response contains a valid DirectionsResult."

            "OVER_QUERY_LIMIT" =? TSelf
            |> WithComment "The webpage has gone over the requests limit in too short a period of time."

            "REQUEST_DENIED" =? TSelf
            |> WithComment "The webpage is not allowed to use the directions service."

            "UNKNOWN_ERROR" =? TSelf
            |> WithComment "A directions request could not be processed due to a server error. The request may succeed if you try again."

            "ZERO_RESULTS" =? TSelf
            |> WithComment "No route could be found between the origin and destination."
        ]

    let Location =
        Class "google.maps.Location"
        |+> Static [
            Constructor T<string>?Query
            |> WithInline "$Query"

            Constructor LatLng?Position
            |> WithInline "$Position"
        ]
        |+> Instance [
            "isQuery" =? T<bool>
            |> WithGetterInline "typeof $this === 'string'"

            "isPosition" =? T<bool>
            |> WithGetterInline "typeof $this !== 'string'"

            "asQuery" =? T<string>
            |> WithGetterInline "$this"

            "asPosition" =? LatLng
            |> WithGetterInline "$this"
        ]

    let DirectionsWaypoint =
        Config "DirectionsWaypoint"
        |+> Instance [
            "location" =@ T<string> + LatLng + LatLngLiteral + Place
            |> WithComment "Waypoint location. Can be an address string, a LatLng, or a Place. Optional."

            "stopover" =@ T<bool>
            |> WithComment "If true, indicates that this waypoint is a stop between the origin and destination. This has the effect of splitting the route into two legs. If false, indicates that the route should be biased to go through this waypoint, but not split into two legs. This is useful if you want to create a route in response to the user dragging waypoints on a map."
        ]

    let UnitSystem =
        Class "google.maps.UnitSystem"
        |+> Static [
            "IMPERIAL" =? TSelf
            |> WithComment "Specifies that distances in the DirectionsResult should be expressed in imperial units."

            "METRIC" =? TSelf
            |> WithComment "Specifies that distances in the DirectionsResult should be expressed in metric units."
        ]

    let TrafficModel =
        Class "google.maps.TrafficModel"
        |+> Static [
            "BEST_GUESS" =? TSelf
            |> WithComment "Use historical traffic data to best estimate the time spent in traffic."

            "OPTIMISTIC" =? TSelf
            |> WithComment "Use historical traffic data to make an optimistic estimate of what the duration in traffic will be."

            "PESSIMISTIC" =? TSelf
            |> WithComment "Use historical traffic data to make a pessimistic estimate of what the duration in traffic will be."
        ]

    let TransitMode =
        Class "google.maps.TransitMode"
        |+> Static [
            "BUS" =? TSelf
            |> WithComment "Specifies bus as a preferred mode of transit."

            "RAIL" =? TSelf
            |> WithComment "Specifies rail as a preferred mode of transit."

            "SUBWAY" =? TSelf
            |> WithComment "Specifies subway as a preferred mode of transit."

            "TRAIN" =? TSelf
            |> WithComment "Specifies train as a preferred mode of transit."

            "TRAM" =? TSelf
            |> WithComment "Specifies tram as a preferred mode of transit."
        ]

    let TransitRoutePreference =
        Class "google.maps.TransitRoutePreference"
        |+> Static [
            "FEWER_TRANSFERS" =? TSelf
            |> WithComment "Specifies that the calculated route should prefer a limited number of transfers."

            "LESS_WALKING" =? TSelf
            |> WithComment "Specifies that the calculated route should prefer limited amounts of walking."
        ]

    let TransitOptions =
        Config "TransitOptions"
        |+> Instance [
            "arrivalTime" =@ Date
            |> WithComment "The desired arrival time for the route, specified as a Date object. The Date object measures time in milliseconds since 1 January 1970. If arrival time is specified, departure time is ignored."

            "departureTime" =@ Date
            |> WithComment "The desired departure time for the route, specified as a Date object. The Date object measures time in milliseconds since 1 January 1970. If neither departure time nor arrival time is specified, the time is assumed to be \"now\"."

            "modes" =@ Type.ArrayOf TransitMode
            |> WithComment "One or more preferred modes of transit, such as bus or train. If no preference is given, the API returns the default best route."

            "routingPreference" =@ TransitRoutePreference
            |> WithComment "A preference that can bias the choice of transit route, such as less walking. If no preference is given, the API returns the default best route."
        ]

    let DrivingOptions =
        Interface "google.maps.DrivingOptions"
        |+> [
            "departureTime" =@ Date
            |> WithComment "The desired departure time for the route, specified as a Date object. The Date object measures time in milliseconds since 1 January 1970. This must be specified for a DrivingOptions to be valid. The departure time must be set to the current time or some time in the future. It cannot be in the past."

            "trafficModel" =@ TrafficModel
            |> WithComment "The preferred assumption to use when predicting duration in traffic. The default is BEST_GUESS."
        ]

    let DirectionsRequest =
        Class "google.maps.DirectionsRequest"
        |+> Static [
            Ctor [
                (Location + LatLng + T<string>)?Origin
                (Location + LatLng + T<string>)?Destination
                TravelMode?TravelMode
            ]
            |> WithInline "{origin:$Origin, destination:$Destination, travelMode:$TravelMode}"
        ]
        |+> Instance [
            "avoidFerries" =@ T<bool>
            |> WithComment "If true, instructs the Directions service to avoid ferries where possible. Optional."

            "avoidHighways" =@ T<bool>
            |> WithComment "If true, instructs the Directions service to avoid highways where possible. Optional."

            "avoidTolls" =@ T<bool>
            |> WithComment "If true, instructs the Directions service to avoid toll roads where possible. Optional."

            "destination" =@ T<string> + LatLng + Place + LatLngLiteral
            |> WithComment "Location of destination. This can be specified as either a string to be geocoded, or a LatLng, or a Place. Required."

            "drivingOptions" =@ DrivingOptions
            |> WithComment "Settings that apply only to requests where travelMode is DRIVING. This object will have no effect for other travel modes."

            "language" =@ T<string>
            |> WithComment "A language identifier for the language in which results should be returned, when possible. See the list of supported languages."

            "optimizeWaypoints" =@ T<bool>
            |> WithComment "If set to true, the DirectionService will attempt to re-order the supplied intermediate waypoints to minimize overall cost of the route. If waypoints are optimized, inspect DirectionsRoute.waypoint_order in the response to determine the new ordering."

            "origin" =@ T<string> + LatLng + Place + LatLngLiteral
            |> WithComment "Location of origin. This can be specified as either a string to be geocoded or a LatLng. Required."

            "provideRouteAlternatives" =@ T<bool>
            |> WithComment "Whether or not route alternatives should be provided. Optional."

            "region" =@ T<string>
            |> WithComment "Region code used as a bias for geocoding requests. Optional."

            "transitOptions" =@ TransitOptions
            |> WithComment "Settings that apply only to requests where travelMode is TRANSIT. This object will have no effect for other travel modes."

            "travelMode" =@ TravelMode
            |> WithComment "Type of routing requested. Required."

            "unitSystem" =@ UnitSystem
            |> WithComment "Preferred unit system to use when displaying distance. Defaults to the unit system used in the country of origin."

            "waypoints" =@ Type.ArrayOf DirectionsWaypoint
            |> WithComment "Array of intermediate waypoints. Directions will be calculated from the origin to the destination by way of each waypoint in this array. The maximum allowed waypoints is 8, plus the origin, and destination. Maps API for Business customers are allowed 23 waypoints, plus the origin, and destination. Waypoints are not supported for transit directions. Optional."
        ]

    let DirectionsResult =
        Class "google.maps.DirectionsResult"
        |+> Instance [
            "request" =? DirectionsRequest
            |> WithComment "The DirectionsRequest that yielded this result."

            "routes" =? Type.ArrayOf DirectionsRoute
            |> WithComment "An array of DirectionsRoutes, each of which contains information about the legs and steps of which it is composed. There will only be one route unless the DirectionsRequest was made with provideRouteAlternatives set to true."

            "available_travel_modes" =? Type.ArrayOf TravelMode
            |> WithComment "Contains an array of available travel modes. This field is returned when a request specifies a travel mode and gets no results. The array contains the available travel modes in the countries of the given set of waypoints. This field is not returned if one or more of the waypoints are 'via waypoints'."

            "geocoded_waypoints" =? Type.ArrayOf DirectionsGeocodedWaypoint
            |> WithComment "An array of DirectionsGeocodedWaypoints, each of which contains information about the geocoding of origin, destination and waypoints."
        ]

    let DirectionsRendererOptions =
        Config "DirectionsRendererOptions"
        |+> Instance [
            "directions" =@ DirectionsResult
            |> WithComment "The directions to display on the map and/or in a <div> panel, retrieved as a DirectionsResult object from DirectionsService."

            "draggable" =@ T<bool>
            |> WithComment "If true, allows the user to drag and modify the paths of routes rendered by this DirectionsRenderer."

            "hideRouteList" =@ T<bool>
            |> WithComment "This property indicates whether the renderer should provide a user-selectable list of routes shown in the directions panel. Default: false."

            "infoWindow" =@ InfoWindow
            |> WithComment "The InfoWindow in which to render text information when a marker is clicked. Existing info window content will be overwritten and its position moved. If no info window is specified, the DirectionsRenderer will create and use its own info window. This property will be ignored if suppressInfoWindows is set to true."

            "map" =@ M.Map
            |> WithComment "Map on which to display the directions."

            "markerOptions" =@ MarkerOptions
            |> WithComment "Options for the markers. All markers rendered by the DirectionsRenderer will use these options."

            "panel" =@ Node
            |> WithComment "The <div> in which to display the directions steps."

            "polylineOptions" =@ PolylineOptions
            |> WithComment "Options for the polylines. All polylines rendered by the DirectionsRenderer will use these options."

            "preserveViewport" =@ T<bool>
            |> WithComment "If this option is set to true or the map's center and zoom were never set, the input map is centered and zoomed to the bounding box of this set of directions. Default: false."

            "routeIndex" =@ T<int>
            |> WithComment "The index of the route within the DirectionsResult object. The default value is 0."

            "suppressBicyclingLayer" =@ T<bool>
            |> WithComment "Suppress the rendering of the BicyclingLayer when bicycling directions are requested."

            "suppressInfoWindows" =@ T<bool>
            |> WithComment "Suppress the rendering of info windows."

            "suppressMarkers" =@ T<bool>
            |> WithComment "Suppress the rendering of markers."

            "suppressPolylines" =@ T<bool>
            |> WithComment "Suppress the rendering of polylines."
        ]

    let DirectionsRenderer =
        Class "google.maps.DirectionsRenderer"
        |=> Inherits MVC.MVCObject
        |+> Static [
            Constructor !? DirectionsRendererOptions
            |> WithComment "Creates the renderer with the given options. Directions can be rendered on a map (as visual overlays) or additionally on a <div> panel (as textual instructions)."
        ]
        |+> Instance [
            "getDirections" =>  T<unit> ^-> DirectionsResult
            |> WithComment "Returns the renderer's current set of directions."

            "getMap" =>  T<unit> ^-> M.Map
            |> WithComment "Returns the map on which the DirectionsResult is rendered."

            "getPanel" =>  T<unit> ^-> Node
            |> WithComment "Returns the panel <div> in which the DirectionsResult is rendered."

            "getRouteIndex" =>  T<unit> ^-> T<int>
            |> WithComment "Returns the current (zero-based) route index in use by this DirectionsRenderer object."

            "setDirections" =>  DirectionsResult ^-> T<unit>
            |> WithComment "Set the renderer to use the result from the DirectionsService. Setting a valid set of directions in this manner will display the directions on the renderer's designated map and panel."

            "setMap" => M.Map ^-> T<unit>
            |> WithComment "This method specifies the map on which directions will be rendered. Pass null to remove the directions from the map."

            "setOptions" =>  DirectionsRendererOptions ^-> T<unit>
            |> WithComment "Change the options settings of this DirectionsRenderer after initialization."

            "setPanel" =>  Node ^-> T<unit>
            |> WithComment "This method renders the directions in a <div>. Pass null to remove the content from the panel."

            "setRouteIndex" =>  T<unit> ^-> T<unit>
            |> WithComment "Set the (zero-based) index of the route in the DirectionsResult object to render. By default, the first route in the array will be rendered."

            // EVENTS
            "directions_changed" => T<obj> -* T<unit> ^-> T<unit>
            |> WithComment "This event is fired when the rendered directions change, either when a new DirectionsResult is set or when the user finishes dragging a change to the directions path."
        ]

    let DirectionsService =
        Class "google.maps.DirectionsService"
        |+> Static [Constructor T<unit>]
        |+> Instance [
            "route" => (DirectionsRequest * !? (DirectionsResult * DirectionsStatus ^-> T<unit>)) ^-> T<unit>
            |> WithComment "Issue a directions search request."
        ]

    let PathElevationRequest =
        Interface "google.maps.PathElevationRequest"
        |+> [
            "path" =@ Type.ArrayOf (LatLng + LatLngLiteral)
            |> WithComment "The path along which to collect elevation values."

            "samples" =@ T<int>
            |> WithComment "Required. The number of equidistant points along the given path for which to retrieve elevation data, including the endpoints. The number of samples must be a value between 2 and 512 inclusive."
        ]

    let LocationElevationRequest =
        Config "LocationElevationRequest"
        |+> Instance [
            "locations" =@ Type.ArrayOf (LatLng + LatLngLiteral)
            |> WithComment "The discrete locations for which to retrieve elevations."
        ]

    let ElevationResult =
        Class "google.maps.ElevationResult"
        |+> Instance [
            "elevation" =? T<float>
            |> WithComment "The elevation of this point on Earth, in meters above sea level."

            "location" =? LatLng
            |> WithComment "The location of this elevation result."

            "resolution" =? T<float>
            |> WithComment "The distance, in meters, between sample points from which the elevation was interpolated. This property will be missing if the resolution is not known. Note that elevation data becomes more coarse (larger resolution values) when multiple points are passed. To obtain the most accurate elevation value for a point, it should be queried independently."
        ]

    let LocationElevationResponse =
        Interface "google.maps.LocationElevationResponse"
        |+> [
            "results" =@ Type.ArrayOf ElevationResult
            |> WithComment "The list of ElevationResults matching the locations of the LocationElevationRequest."
        ]

    let PathElevationResponse =
        Interface "google.maps.PathElevationResponse"
        |+> [
            "iresults" =@ Type.ArrayOf ElevationResult
            |> WithComment "The list of ElevationResults matching the samples of the PathElevationRequest."
        ]

    let ElevationStatus =
        Class "google.maps.ElevationStatus"
        |+> Static [
            "INVALID_REQUEST" =? TSelf
            |> WithComment "This request was invalid."

            "OK" =? TSelf
            |> WithComment "The request did not encounter any errors."

            "OVER_QUERY_LIMIT" =? TSelf
            |> WithComment "The webpage has gone over the requests limit in too short a period of time."

            "REQUEST_DENIED" =? TSelf
            |> WithComment "The webpage is not allowed to use the elevation service."

            "UNKNOWN_ERROR" =? TSelf
            |> WithComment "A geocoding, directions or elevation request could not be successfully processed, yet the exact reason for the failure is not known."
        ]

    let ElevationService =
        Class "google.maps.ElevationService"
        |+> Static [
            Constructor T<unit>
            |> WithComment "Creates a new instance of a ElevationService that sends elevation queries to Google servers."
        ]
        |+> Instance [
            "getElevationAlongPath" => (PathElevationRequest * (Type.ArrayOf ElevationResult ^-> ElevationStatus)) ^-> Promise[PathElevationResponse]
            |> WithComment "Makes an elevation request along a path, where the elevation data are returned as distance-based samples along that path."

            "getElevationForLocations" => (LocationElevationRequest * (Type.ArrayOf ElevationResult ^-> ElevationStatus)) ^-> Promise[LocationElevationResponse]
            |> WithComment "Makes an elevation request for a list of discrete locations."
        ]

    let MaxZoomStatus =
        Class "google.maps.MaxZoomStatus"
        |+> Static [
            "ERROR" =? TSelf
            |> WithComment "An unknown error occurred."

            "OK" =? TSelf
            |> WithComment "The response contains a valid MaxZoomResult."
        ]

    let MaxZoomResult =
        Class "google.maps.MaxZoomResult"
        |+> Instance [
            "status" =? MaxZoomStatus
            |> WithComment "Status of the request. This property is only defined when using callbacks with MaxZoomService.getMaxZoomAtLatLng (it is not defined when using Promises)."

            "zoom" =? T<int>
            |> WithComment "The maximum zoom level found at the given LatLng."
        ]

    let MaxZoomService =
        Class "google.maps.MaxZoomService"
        |+> Static [
            Constructor T<unit>
            |> WithComment "Creates a new instance of a MaxZoomService that can be used to send queries about the maximum zoom level available for satellite imagery."
        ]
        |+> Instance [
            "getMaxZoomAtLatLng" => (LatLng + LatLngLiteral) * (MaxZoomResult ^-> T<unit>) ^-> T<unit>
            |> WithComment "Returns the maximum zoom level available at a particular LatLng for the Satellite map type. As this request is asynchronous, you must pass a callback function which will be executed upon completion of the request, being passed a MaxZoomResult."
        ]

    let DistanceMatrixRequest =
        Interface "google.maps.DistanceMatrixRequest"
        |+> [
            "avoidFerries" =@ T<bool>
            |> WithComment "If true, instructs the Distance Matrix service to avoid ferries where possible. Optional."

            "avoidHighways" =@ T<bool>
            |> WithComment "If true, instructs the Distance Matrix service to avoid highways where possible. Optional."

            "avoidTolls" =@ T<bool>
            |> WithComment "If true, instructs the Distance Matrix service to avoid toll roads where possible. Optional."

            "drivingOptions" =@ DrivingOptions
            |> WithComment "Settings that apply only to requests where travelMode is DRIVING. This object will have no effect for other travel modes."

            "destinations" =@ Type.ArrayOf (T<string> + LatLng + LatLngLiteral + Place)
            |> WithComment "An array containing destination address strings, or LatLng, or Place objects, to which to calculate distance and time. Required."

            "origins" =@ Type.ArrayOf (T<string> + LatLng + LatLngLiteral + Place)
            |> WithComment "An array containing origin address strings, or LatLng, or Place objects, from which to calculate distance and time. Required."


            "language" =@ T<string>
            |> WithComment "A language identifier for the language in which results should be returned, when possible. See the list of supported languages."

            "region" =@ T<string>
            |> WithComment "Region code used as a bias for geocoding requests. The region code accepts a ccTLD (\"top-level domain\") two-character value. Most ccTLD codes are identical to ISO 3166-1 codes, with some notable exceptions. For example, the United Kingdom's ccTLD is \"uk\" (.co.uk) while its ISO 3166-1 code is \"gb\" (technically for the entity of \"The United Kingdom of Great Britain and Northern Ireland\")."

            "transitOptions" =@ TransitOptions
            |> WithComment "Settings that apply only to requests where travelMode is TRANSIT. This object will have no effect for other travel modes."

            "travelMode" =@ TravelMode
            |> WithComment "Type of routing requested. Required."

            "unitSystem" =@ UnitSystem
            |> WithComment "Preferred unit system to use when displaying distance. Optional; defaults to metric."
        ]

    let DistanceMatrixElementStatus =
        Class "google.maps.DistanceMatrixElementStatus"
        |+> Static [
            "NOT_FOUND" =? TSelf
            |> WithComment "The origin and/or destination of this pairing could not be geocoded."

            "OK" =? TSelf
            |> WithComment "The response contains a valid result."

            "ZERO_RESULTS" =? TSelf
            |> WithComment "No route could be found between the origin and destination."
        ]

    let DistanceMatrixStatus =
        Class "google.maps.DistanceMatrixStatus"
        |+> Static [
            "INVALID_REQUEST" =? TSelf
            |> WithComment "The provided request was invalid."

            "MAX_DIMENSIONS_EXCEEDED" =? TSelf
            |> WithComment "The request contains more than 25 origins, or more than 25 destinations."

            "MAX_ELEMENTS_EXCEEDED" =? TSelf
            |> WithComment "The product of origins and destinations exceeds the per-query limit."

            "OK" =? TSelf
            |> WithComment "The response contains a valid result."

            "OVER_QUERY_LIMIT" =? TSelf
            |> WithComment "Too many elements have been requested within the allowed time period. The request should succeed if you try again after some time."

            "REQUEST_DENIED" =? TSelf
            |> WithComment "The service denied use of the Distance Matrix service by your web page."

            "UNKNOWN_ERROR" =? TSelf
            |> WithComment "A Distance Matrix request could not be processed due to a server error. The request may succeed if you try again."
        ]

    let DistanceMatrixResponseElement =
        Class "google.maps.DistanceMatrixResponseElement"
        |+> Instance [
            "distance" =? Distance
            |> WithComment "The distance for this origin-destination pairing. This property may be undefined as the distance may be unknown."

            "duration" =? Duration
            |> WithComment "The duration for this origin-destination pairing. This property may be undefined as the duration may be unknown."

            "duration_in_traffic" =? Duration
            |> WithComment "The duration for this origin-destination pairing, taking into account the traffic conditions indicated by the trafficModel property. This property may be undefined as the duration may be unknown. Only available to Premium Plan customers when drivingOptions is defined when making the request."

            "fare" =? TransitFare
            |> WithComment "The total fare for this origin-destination pairing. Only applicable to transit requests."

            "status" =? DistanceMatrixElementStatus
            |> WithComment "The status of this particular origin-destination pairing."
        ]

    let DistanceMatrixResponseRow =
        Class "google.maps.DistanceMatrixResponseRow"
        |+> Instance [
            "elements" =? Type.ArrayOf DistanceMatrixResponseElement
            |> WithComment "The row's elements, corresponding to the destination addresses."
        ]

    let DistanceMatrixResponse =
        Class "google.maps.DistanceMatrixResponse"
        |+> Instance [
            "destinationAddresses" =? T<string[]>
            |> WithComment "The formatted destination addresses."

            "originAddresses" =? T<string[]>
            |> WithComment "The formatted origin addresses."

            "rows" =? Type.ArrayOf DistanceMatrixResponseRow
            |> WithComment "The rows of the matrix, corresponding to the origin addresses"
        ]

    let DistanceMatrixService =
        Class "google.maps.DistanceMatrixService"
        |+> Static [
            Constructor T<unit>
            |> WithComment "Creates a new instance of a DistanceMatrixService that sends distance matrix queries to Google servers."
        ]
        |+> Instance [
            "getDistanceMatrix" => DistanceMatrixRequest * (DistanceMatrixResponse * DistanceMatrixStatus ^-> T<unit>) ^-> T<unit>
            |> WithComment "Issues a distance matrix request."
        ]

    let OverlayView =
        Class "google.maps.OverlayView"
        |=> Inherits MVC.MVCObject
        |+> Static [
            Constructor T<unit>

            "preventMapHitsAndGesturesFrom" => Element ^-> T<unit>
            |> WithComment "Stops click, tap, drag, and wheel events on the element from bubbling up to the map. Use this to prevent map dragging and zooming, as well as map \"click\" events."

            "preventMapHitsFrom" => Element ^-> T<unit>
            |> WithComment "Stops click or tap on the element from bubbling up to the map. Use this to prevent the map from triggering \"click\" events."
        ]
        |+> Instance [
            "draw" => T<unit -> unit>
            |> WithComment "Implement this method to draw or update the overlay. Use the position from projection.fromLatLngToDivPixel() to correctly position the overlay relative to the MapPanes. This method is called after onAdd(), and is called on change of zoom or center. It is not recommended to do computationally expensive work in this method."

            "getMap" => M.Map + StreetView.StreetViewPanorama

            "getPanes" => T<unit> ^-> MapPanes
            |> WithComment "Returns the panes in which this OverlayView can be rendered. The panes are not initialized until onAdd is called by the API."

            "getProjection" => T<unit> ^-> MapCanvasProjection
            |> WithComment "Returns the MapCanvasProjection object associated with this OverlayView. The projection is not initialized until onAdd is called by the API."

            "onAdd" => T<unit -> unit>
            |> WithComment "Implement this method to initialize the overlay DOM elements. This method is called once after setMap() is called with a valid map. At this point, panes and projection will have been initialized."

            "onRemove" => T<unit -> unit>
            |> WithComment "Implement this method to remove your elements from the DOM. This method is called once following a call to setMap(null)."

            "setMap" => (M.Map + StreetView.StreetViewPanorama) ^-> T<unit>
            |> WithComment "Adds the overlay to the map or panorama."
        ]

    let PinElementOptions =
        Interface "google.maps.marker.PinElementOptions"
        |+> [
            "background" =@ T<string>
            |> WithComment "The background color of the pin shape. Supports any CSS color value."

            "borderColor" =@ T<string>
            |> WithComment "The border color of the pin shape. Supports any CSS color value."

            "glyph" =@ T<string> + Element + URL
            |> WithComment "The DOM element displayed in the pin."

            "glyphColor" =@ T<string>
            |> WithComment "The color of the glyph. Supports any CSS color value."

            "scale" =@ T<int>
            |> WithComment "The scale of the pin. Default: 1"
        ]

    let PinElement =
        Class "google.maps.marker.PinElement"
        |=> Inherits HTMLElement
        |=> Implements [PinElementOptions]
        |+> Static [
            Ctor [
                !? PinElementOptions?options
            ]
        ]
        |+> Instance [
            // "background" =@ T<string>
            // |> WithComment "See PinElementOptions.background."

            // "borderColor" =@ T<string>
            // |> WithComment "See PinElementOptions.borderColor."

            "element" =? HTMLElement
            |> WithComment "This field is read-only. The DOM Element backing the view."

            // "glyph" =@ T<string> + Element + URL
            // |> WithComment "See PinElementOptions.glyph."

            // "glyphColor" =@ T<string>
            // |> WithComment "See PinElementOptions.glyphColor."

            // "scale" =@ T<int>
            // |> WithComment "See PinElementOptions.scale."

            // METHODS
            //TODO: where is EventListener?
            // "addEventListener" => T<string> * (EventListener + EventListenerObject) * !?(T<bool> + AddEventListenerOptions) ^-> T<unit>
            "addEventListener" => T<string> * T<obj->unit> * !?(T<bool> + AddEventListenerOptions) ^-> T<unit>
            |> WithComment "This function is not yet available for usage."

            //TODO: where is EventListener?
            // "removeEventListener" => T<string> * (EventListener + EventListenerObject) * !?(T<bool> + EventListenerOptions) ^-> T<unit>
            "removeEventListener" => T<string> * T<obj->unit> * !?(T<bool> + EventListenerOptions) ^-> T<unit>
            |> WithComment "Removes an event listener previously registered with addEventListener from the target. See removeEventListener"
        ]

