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
        Pattern.EnumStrings "google.maps.Animation" [
            "BOUNCE"
            "DROP"
        ]

    let MapPanes =
        Config "google.maps.MapPanes"
            []
            [
                "floatPane", Element
                "mapPane", Element
                "markerLayer", Element
                "overlayLayer", Element
                "overlayMouseTarget", Element
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
        Pattern.EnumStrings "google.maps.StrokePosition" [
            "CENTER"
            "INSIDE"
            "OUTSIDE"
        ]

    let RectangleOptions =
        Config "google.maps.RectangleOptions"
            []
            [
                "bounds", LatLngBounds + LatLngBoundsLiteral
                "clickable", T<bool>
                "draggable", T<bool>
                "editable", T<bool>
                "fillColor", T<string>
                "fillOpacity", T<float>
                "map", M.Map.Type
                "strokeColor", T<string>
                "strokeOpacity", T<float>
                "strokePosition", StrokePosition.Type
                "strokeWeight", T<float>
                "visible", T<bool>
                "zIndex", T<int>
            ]

    let CircleOptionsProperties = [        
        "clickable", T<bool>
        "draggable", T<bool>
        "editable", T<bool>
        "fillColor", T<string>
        "fillOpacity", T<float>
        "map", M.Map.Type        
        "strokeColor", T<string>
        "strokeOpacity", T<float>
        "strokePosition", StrokePosition.Type
        "strokeWeight", T<float>
        "visible", T<bool>
        "zIndex", T<int>
    ]

    let CircleOptions =
        Config "google.maps.CircleOptions"
            [
                "radius", T<float>
                "center", LatLng + LatLngLiteral
            ]
            CircleOptionsProperties

    let CircleLiteral =
        Config "google.maps.CircleLiteral"
            []
            CircleOptionsProperties

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
            "center_changed" => T<unit> ^-> T<unit>
            |> WithComment "This event is fired when the circle's center is changed."

            "click" => M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM click event is fired on the circle."

            "dblclick" => M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM dblclick event is fired on the circle."

            "drag" => M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is repeatedly fired while the user drags the circle."

            "dragend" => M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the user stops dragging the circle."

            "dragstart" => M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the user starts dragging the circle."

            "mousedown" => M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM mousedown event is fired on the circle."

            "mousemove" => M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM mousemove event is fired on the circle."

            "mouseout" => M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired on circle mouseout."

            "mouseover" => M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired on circle mouseover."

            "mouseup" => M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM mouseup event is fired on the circle."

            "radius_changed" => T<unit> ^-> T<unit>
            |> WithComment "This event is fired when the circle's radius is changed."

            "rightclick" => M.MapMouseEvent ^-> T<unit>
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
            "bounds_changed" => T<unit> ^-> T<unit>
            |> WithComment "This event is fired when the rectangle's bounds are changed."

            "click" => M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM click event is fired on the rectangle."

            "contextmenu" => M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM contextmenu event is fired on the rectangle."

            "dblclick" => M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM dblclick event is fired on the rectangle."

            "drag" => M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is repeatedly fired while the user drags the rectangle."

            "dragend" => M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the user stops dragging the rectangle."

            "dragstart" => M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the user starts dragging the rectangle."

            "mousedown" => M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM mousedown event is fired on the rectangle."

            "mousemove" => M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM mousemove event is fired on the rectangle."

            "mouseout" => M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired on rectangle mouseout."

            "mouseover" => M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired on rectangle mouseover."

            "mouseup" => M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM mouseup event is fired on the rectangle."

            "rightclick" => M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the rectangle is right-clicked on."
            |> ObsoleteWithMessage "Deprecated: Use the Rectangle.contextmenu event instead in order to support usage patterns like control-click on macOS."
        ]

    let GroundOverlayOptions =
        Config "google.maps.GroundOverlayOptions"
            []
            [
                "clickable", T<bool>
                "map", M.Map.Type
                "opacity", T<float>
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
            "click" => M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM click event is fired on the GroundOverlay."

            "dblclick" => M.MapMouseEvent ^-> T<unit>
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

    let KmlAuthor =
        Config "google.maps.KmlAuthor"
            []
            [
                "email", T<string>
                "name", T<string>
                "uri", T<string>
            ]

    let KmlFeatureData =
        Config "google.maps.KmlFeatureData"
            []
            [
                "author", KmlAuthor.Type
                "description", T<string>
                "id", T<string>
                "infoWindowHtml", T<string>
                "name", T<string>
                "snippet", T<string>
            ]

    let KmlMouseEvent =
        Config "google.maps.KmlMouseEvent"
            []
            [
                "featureData", KmlFeatureData.Type
                "latLng", LatLng.Type
                "pixelOffset", Size.Type
            ]

    let KmlLayerMetadata =
        Config "google.maps.KmlLayerMetadata"
            []
            [
                "author", KmlAuthor.Type
                "description", T<string>
                "hasScreenOverlays", T<bool>
                "name", T<string>
                "snippet", T<string>
            ]

    let KmlLayerOptions =
        Config "google.maps.KmlLayerOptions"
            []
            [
                "clickable", T<bool>
                "map", M.Map.Type
                "preserveViewport", T<bool>
                "screenOverlays", T<bool>
                "suppressInfoWindows", T<bool>
                "url", T<string>
                "zIndex", T<int>
            ]

    let KmlLayerStatus =
        Pattern.EnumStrings "google.maps.KmlLayerStatus" [
            "DOCUMENT_NOT_FOUND"
            "DOCUMENT_TOO_LARGE"
            "FETCH_ERROR"
            "INVALID_DOCUMENT"
            "INVALID_REQUEST"
            "LIMITS_EXCEEDED"
            "OK"
            "TIMED_OUT"
            "UNKNOWN"
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
            "click" =@ KmlMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when a feature in the layer is clicked."

            "defaultviewport_changed" =@ T<unit> ^-> T<unit>
            |> WithComment "This event is fired when the KML layers default viewport has changed."

            "status_changed" =@ T<unit> ^-> T<unit>
            |> WithComment "This event is fired when the KML layer has finished loading. At this point it is safe to read the status property to determine if the layer loaded successfully."
        ]

    let TrafficLayerOptions =
        Config "google.maps.TrafficLayerOptions"
            []
            [
                "autoRefresh", T<bool>
                "map", M.Map.Type
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

    let Icon =
        Config "google.maps.Icon"
            []
            [
                "anchor", Point.Type
                "labelOrigin", Point.Type
                "origin", Point.Type
                "scaledSize", Size.Type
                "size", Size.Type
                "url", T<string>
            ]

    let SymbolPath =
        Pattern.EnumStrings "google.maps.SymbolPath" [
            "BACKWARD_CLOSED_ARROW"
            "BACKWARD_OPEN_ARROW"
            "CIRCLE"
            "FORWARD_CLOSED_ARROW"
            "FORWARD_OPEN_ARROW"
        ]

    let Symbol =
        Config "google.maps.Symbol"
            []
            [
                "anchor", Point.Type
                "fillColor", T<string>
                "fillOpacity", T<float>
                "labelOrigin", Point.Type
                "path", SymbolPath + T<string>
                "rotation", T<float>
                "scale", T<float>
                "strokeColor", T<string>
                "strokeOpacity", T<float>
                "strokeWeight", T<float>
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

    let MarkerShape =
        Pattern.Config "google.maps.MarkerShape" {
            Optional = []
            Required =
            [
                "coords", Type.ArrayOf T<int>
                "type", T<string>
            ]
        }

    let MarkerLabel =
        Config "google.maps.MarkerLabel"
            [
                "text", T<string>
            ]
            [
                "className", T<string>
                "color", T<string>
                "fontFamily", T<string>
                "fontSize", T<string>
                "fontWeight", T<string>
            ]

    let MarkerOptions =
        Config "google.maps.MarkerOptions"
            []
            [
                "anchorPoint", Point.Type
                "animation", Animation.Type
                "clickable", T<bool>
                "collisionBehavior", T<string>
                "crossOnDrag", T<bool>
                "cursor", T<string>
                "draggable", T<bool>
                "icon", T<string> + Icon.Type + Symbol.Type
                "label", T<string> + MarkerLabel.Type
                "map", M.Map.Type + StreetView.StreetViewPanorama.Type
                "opacity", T<float>
                "optimized", T<bool>
                "position", LatLng.Type + LatLngLiteral.Type
                "shape", MarkerShape.Type
                "title", T<string>
                "visible", T<bool>
                "zIndex", T<int>
            ]

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
            "animation_changed" =@ T<unit> ^-> T<unit>
            |> WithComment "This event is fired when the Marker animation property changes."

            "click" =@ M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the Marker icon was clicked."

            "clickable_changed" =@ T<unit> ^-> T<unit>
            |> WithComment "This event is fired when the Marker clickable property changes."

            "contextmenu" =@ M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM contextmenu event is fired on the Marker"

            "cursor_changed" =@ T<unit> ^-> T<unit>
            |> WithComment "This event is fired when the Marker cursor property changes."

            "dblclick" =@ M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the Marker icon was double clicked."

            "drag" =@ M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is repeatedly fired while the user drags the Marker."

            "dragend" =@ M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the user stops dragging the Marker."

            "draggable_changed" =@ T<unit> ^-> T<unit>
            |> WithComment "This event is fired when the Marker draggable property changes."

            "dragstart" =@ M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the user starts dragging the Marker."

            "flat_changed" =@ T<unit> ^-> T<unit>
            |> WithComment "This event is fired when the Marker flat property changes."

            "icon_changed" =@ T<unit> ^-> T<unit>
            |> WithComment "This event is fired when the Marker icon property changes."

            "mousedown" =@ M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired for a mousedown on the Marker."

            "mouseout" =@ M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the mouse leaves the area of the Marker icon."

            "mouseover" =@ M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the mouse enters the area of the Marker icon."

            "mouseup" =@ M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired for a mouseup on the Marker."

            "position_changed" =@ T<unit> ^-> T<unit>
            |> WithComment "This event is fired when the Marker position property changes."

            "shape_changed" =@ T<unit> ^-> T<unit>
            |> WithComment "This event is fired when the Marker shape property changes."

            "title_changed" =@ T<unit> ^-> T<unit>
            |> WithComment "This event is fired when the Marker title property changes."

            "visible_changed" =@ T<unit> ^-> T<unit>
            |> WithComment "This event is fired when the Marker visible property changes."

            "zindex_changed" =@ T<unit> ^-> T<unit>
            |> WithComment "This event is fired when the Marker zIndex property changes."

            "rightclick" =@ M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired for a rightclick on the Marker."
            |> ObsoleteWithMessage "Deprecated: Use the Marker.contextmenu event instead in order to support usage patterns like control-click on macOS."
        ]
        |> ObsoleteWithMessage "Deprecated: As of February 21st, 2024, google.maps.Marker is deprecated. Please use AdvancedMarkerElement instead. At this time, google.maps.Marker is not scheduled to be discontinued, but AdvancedMarkerElement is recommended over google.maps.Marker. While google.maps.Marker will continue to receive bug fixes for any major regressions, existing bugs in google.maps.Marker will not be addressed. At least 12 months notice will be given before support is discontinued. Please see https://developers.google.com/maps/deprecations for additional details and https://developers.google.com/maps/documentation/javascript/advanced-markers/migration for the migration guide."

    let IconSequence =
        Config "google.maps.IconSequence"
            []
            [
                "fixedRotation", T<bool>
                "icon", Symbol.Type
                "offset", T<string>
                "repeat", T<string>
            ]

    let PolyMouseEvent =
        Config "google.maps.PolyMouseEvent"
            []
            ([
                "edge", T<int>
                "path", T<int>
                "vertex", T<int>
            ] @ M.MapMouseEventProperties)

    let PolylineOptions =
        Config "google.maps.PolylineOptions"
            []
            [
                "clickable", T<bool>
                "draggable", T<bool>
                "editable", T<bool>
                "geodesic", T<bool>
                "icons", !? IconSequence
                "map", M.Map.Type
                "path", MVC.MVCArray.[LatLng] + !|LatLng + !|LatLngLiteral
                "strokeColor", T<string>
                "strokeOpacity", T<float>
                "strokeWeight", T<int>
                "visible", T<bool>
                "zIndex", T<int>
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

            "getVisible" => T<unit -> bool>
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
            "click" => PolyMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM click event is fired on the Polyline."

            "contextmenu" => PolyMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM contextmenu event is fired on Poyline."

            "dblclick" => PolyMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM dblclick event is fired on the Polyline."

            "drag" => M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is repeatedly fired while the user drags the polyline."

            "dragend" => M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the user stops dragging the polyline."

            "dragstart" => M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the user starts dragging the polyline."

            "mousedown" => PolyMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM mousedown event is fired on the Polyline."

            "mousemove" => PolyMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM mousemove event is fired on the Polyline."

            "mouseout" => PolyMouseEvent ^-> T<unit>
            |> WithComment "This event is fired on Polyline mouseout."

            "mouseover" => PolyMouseEvent ^-> T<unit>
            |> WithComment "This event is fired on Polyline mouseover."

            "mouseup" => PolyMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM mouseup event is fired on the Polyline."

            "rightclick" => PolyMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the Polyline is right-clicked on."
            |> ObsoleteWithMessage "Deprecated: Use the Polyline.contextmenu event instead in order to support usage patterns like control-click on macOS."
        ]

    let PolygonOptions =
        Config "google.maps.PolygonOptions"
            []
            [
                "clickable", T<bool>
                "draggable", T<bool>
                "editable", T<bool>
                "fillColor", T<string>
                "fillOpacity", T<float>
                "geodesic", T<bool>
                "map", M.Map.Type
                "paths", MVC.MVCArray.[T<obj>] + !|LatLng + !|LatLngLiteral
                "strokeColor", T<string>
                "strokeOpacity", T<float>
                "strokePosition", StrokePosition.Type
                "strokeWeight", T<int>
                "visible", T<bool>
                "zIndex", T<int>
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
            "click" => PolyMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM click event is fired on the Polygon."

            "contextmenu" => PolyMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM contextmenu event is fired on the Polygon."

            "dblclick" => PolyMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM dblclick event is fired on the Polygon."

            "drag" => M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is repeatedly fired while the user drags the polygon."

            "dragend" => M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the user stops dragging the polygon."

            "dragstart" => M.MapMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the user starts dragging the polygon."

            "mousedown" => PolyMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM mousedown event is fired on the Polygon."

            "mousemove" => PolyMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM mousemove event is fired on the Polygon."

            "mouseout" => PolyMouseEvent ^-> T<unit>
            |> WithComment "This event is fired on Polygon mouseout."

            "mouseover" => PolyMouseEvent ^-> T<unit>
            |> WithComment "This event is fired on Polygon mouseover."

            "mouseup" => PolyMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the DOM mouseup event is fired on the Polygon."

            "rightclick" => PolyMouseEvent ^-> T<unit>
            |> WithComment "This event is fired when the Polygon is right-clicked on."
            |> ObsoleteWithMessage "Deprecated: Use the Polygon.contextmenu event instead in order to support usage patterns like control-click on macOS."
        ]

    let AdvancedMarkerClickEvent =
        Class "google.maps.marker.AdvancedMarkerClickEvent"
        |=> Inherits Events.Event
        |+> Static [Constructor T<unit>]

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

            "map" =@ M.Map
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
            "element" =@ HTMLElement
            |> WithComment "The DOM Element backing the marker."
            |> ObsoleteWithMessage "Deprecated: Use the AdvancedMarkerElement directly."

            // Methods
            "addEventListener" => T<string> * (T<obj -> unit>) * !?(T<bool> + AddEventListenerOptions) ^-> T<unit>
            |> WithComment "Adds a DOM event listener."

            "addListener" => T<string> * (T<obj -> unit>) ^-> Events.MapsEventListener
            |> WithComment "Adds a Maps event listener."

            "removeEventListener" => T<string> * (T<obj -> unit>) * !?(T<bool> + EventListenerOptions) ^-> T<unit>
            |> WithComment "Removes a DOM event listener."

            // Events
            "click" => M.MapMouseEvent ^-> T<unit>
            |> WithComment "Fired when the marker is clicked."

            "drag" => M.MapMouseEvent ^-> T<unit>
            |> WithComment "Fired while dragging the marker."

            "dragend" => M.MapMouseEvent ^-> T<unit>
            |> WithComment "Fired when dragging ends."

            "dragstart" => M.MapMouseEvent ^-> T<unit>
            |> WithComment "Fired when dragging starts."

            "gmp-click" => AdvancedMarkerClickEvent ^-> T<unit>
            |> WithComment "Fired on gmp-click event."
        ]

    let InfoWindowOptions =
        Config "google.maps.InfoWindowOptions"
            []
            [
                "ariaLabel", T<string>
                "content", T<string> + Element + Text
                "disableAutoPan", T<bool>
                "headerContent", T<string> + Element + Text
                "headerDisabled", T<bool>
                "maxWidth", T<int>
                "minWidth", T<int>
                "pixelOffset", Size.Type
                "position", LatLng + LatLngLiteral
                "zIndex", T<int>
            ]

    let InfoWindowOpenOptions =
        Config "google.maps.InfoWindowOpenOptions"
            []
            [
                "anchor", MVC.MVCObject + AdvancedMarkerElement
                "map", M.Map.Type + StreetView.StreetViewPanorama
                "shouldFocus", T<bool>
            ]

    let InfoWindow =
        Class "google.maps.InfoWindow"
        |=> Inherits MVC.MVCObject
        |+> Static [
            Constructor !? InfoWindowOptions
            |> WithComment "Creates an info window with the given options. An InfoWindow can be placed on a map at a particular position or above a marker, depending on what is specified in the options. Unless auto-pan is disabled, an InfoWindow will pan the map to make itself visible when it is opened. After constructing an InfoWindow, you must call open to display it on the map. The user can click the close button on the InfoWindow to remove it from the map, or the developer can call close() for the same effect."
        ]
        |+> Instance [

            "isOpen" =@ T<bool>
            |> WithComment "Checks if the InfoWindow is open."

            "close" => T<unit> ^-> T<unit>
            |> WithComment "Closes this InfoWindow by removing it from the DOM structure."

            "focus" => T<unit> ^-> T<unit>
            |> WithComment "Sets focus on this InfoWindow. You may wish to consider using this method along with a visible event to make sure that InfoWindow is visible before setting focus on it. An InfoWindow that is not visible cannot be focused."

            "getContent" => T<unit> ^-> T<string> + Element + Text

            "getPosition" => T<unit> ^-> LatLng

            "getZIndex" => T<unit> ^-> T<int>

            "getHeaderContent" => T<unit> ^-> (T<string> + Element + Text)
            |> WithComment "Returns the header content of this InfoWindow."

            "getHeaderDisabled" => T<unit> ^-> T<bool>
            |> WithComment "Whether the whole header row is disabled."

            "open" => (!?(InfoWindowOpenOptions + M.Map + StreetView.StreetViewPanorama) * !? (AdvancedMarkerElement + MVC.MVCObject)) ^-> T<unit>
            |> WithComment "Opens this InfoWindow on the given map. Optionally, an InfoWindow can be associated with an anchor. In the core API, the only anchor is the Marker class. However, an anchor can be any MVCObject that exposes a LatLng position property and optionally a Point anchorPoint property for calculating the pixelOffset (see InfoWindowOptions). The anchorPoint is the offset from the anchor's position to the tip of the InfoWindow. It is recommended to use the InfoWindowOpenOptions interface as the single argument for this method. To prevent changing browser focus on open, set InfoWindowOpenOptions.shouldFocus to false."

            "setContent" => (T<string>  + Node) ^-> T<unit>

            "setOptions" => InfoWindowOptions ^-> T<unit>

            "setPosition" => LatLng + LatLngLiteral ^-> T<unit>

            "setZIndex" => T<int> ^-> T<unit>

            "setHeaderContent" => (T<string> + Node) ^-> T<unit>
            |> WithComment "Sets the header content to be displayed by this InfoWindow."

            "setHeaderDisabled" => T<bool> ^-> T<unit>
            |> WithComment "Specifies whether to disable the whole header row."

            // EVENTS
            "closeclick" => T<unit> ^-> T<unit>
            |> WithComment "This event is fired when the close button was clicked."

            "content_changed" => T<unit> ^-> T<unit>
            |> WithComment "This event is fired when the content property changes."

            "domready" => T<unit> ^-> T<unit>
            |> WithComment "This event is fired when the <div> containing the InfoWindow's content is attached to the DOM. You may wish to monitor this event if you are building out your info window content dynamically."

            "headercontent_changed" => T<unit> ^-> T<unit>
            |> WithComment "This event is fired when the headerContent property changes."

            "headerdisabled_changed" => T<unit> ^-> T<unit>
            |> WithComment "This event is fired when the headerDisabled property changes."

            "position_changed" => T<unit> ^-> T<unit>
            |> WithComment "This event is fired when the position property changes."

            "visible" => T<unit> ^-> T<unit>
            |> WithComment "This event is fired when the InfoWindow is fully visible. This event is not fired when InfoWindow is panned off and then back on screen."

            "zindex_changed" => T<unit> ^-> T<unit>
            |> WithComment "This event is fired when the InfoWindow's zIndex changes."            
        ]

    let Place =
        Config "google.maps.Place"
            []
            [
                "location", LatLng + LatLngLiteral
                "placeId", T<string>
                "query", T<string>
            ]

    let GeocoderComponentRestrictions =
        Config "google.maps.GeocoderComponentRestrictions"
            []
            [
                "administrativeArea", T<string>
                "country", T<string>
                "locality", T<string>
                "postalCode", T<string>
                "route", T<string>
            ]

    let ExtraGeocodeComputation = 
        Pattern.EnumStrings "google.maps.ExtraGeocodeComputation" ["ADDRESS_DESCRIPTORS"]

    let GeocoderRequest =
        Config "google.maps.GeocoderRequest"
            []
            [
                "address", T<string>
                "bounds", LatLngBounds + LatLngBoundsLiteral
                "componentRestrictions", GeocoderComponentRestrictions.Type
                "extraComputations", !| ExtraGeocodeComputation
                "fulfillOnZeroResults", T<bool>
                "language", T<string>
                "location", LatLng + LatLngLiteral
                "placeId", T<string>
                "region", T<string>
            ]

    let GeocoderStatus =
        Pattern.EnumStrings "google.maps.GeocoderStatus"
            [
                "ERROR"
                "INVALID_REQUEST"
                "OK"
                "OVER_QUERY_LIMIT"
                "REQUEST_DENIED"
                "UNKNOWN_ERROR"
                "ZERO_RESULTS"
            ]

    let GeocoderLocationType =
        Pattern.EnumStrings "google.maps.GeocoderLocationType"
            [
                "APPROXIMATE"
                "GEOMETRIC_CENTER"
                "RANGE_INTERPOLATED"
                "ROOFTOP"
            ]

    let GeocoderAddressComponent =
        Config "google.maps.GeocoderAddressComponent"
            []
            [
                "long_name", T<string>
                "short_name", T<string>
                "types", !| T<string>
            ]

    let GeocoderGeometry =
        Config "google.maps.GeocoderGeometry"
            []
            [
                "bounds", LatLngBounds.Type
                "location", LatLng.Type
                "location_type", GeocoderLocationType.Type
                "viewport", LatLngBounds.Type
            ]

    let Containment =
        Pattern.EnumStrings "google.maps.Containment" [
            "NEAR"
            "OUTSKIRTS"
            "WITHIN"
        ]

    let SpatialRelationship =
        Pattern.EnumStrings "google.maps.SpatialRelationship" [
            "ACROSS_THE_ROAD"
            "AROUND_THE_CORNER"
            "BEHIND"
            "BESIDE"
            "DOWN_THE_ROAD"
            "NEAR"
            "WITHIN"
        ]

    let Landmark =
        Config "google.maps.Landmark"
            []
            [
                "display_name", T<string>
                "display_name_language_code", T<string>
                "place_id", T<string>
                "spatial_relationship", SpatialRelationship.Type
                "straight_line_distance_meters", T<float>
                "types", !| T<string>
                "travel_distance_meters", T<float>
            ]

    let Area =
        Config "google.maps.Area"
            []
            [
                "containment", Containment.Type
                "display_name", T<string>
                "display_name_language_code", T<string>
                "place_id", T<string>
            ]


    let AddressDescriptor =
        Config "google.maps.AddressDescriptor"
            []
            [
                "areas", !| Area.Type
                "landmarks", !| Landmark.Type
            ]

    let GeocoderResult =
        Config "google.maps.GeocoderResult"
            []
            [
                "address_components", !| GeocoderAddressComponent
                "formatted_address", T<string>
                "geometry", GeocoderGeometry.Type
                "place_id", T<string>
                "types", !| T<string>
                "address_descriptor", AddressDescriptor.Type
                "partial_match", T<bool>
                "plus_code", Forward.PlacePlusCode.Type
                "postcode_localities", !| T<string>
            ]

    let GeocoderResponse =
        Config "google.maps.GeocoderResponse"
            []
            [
                "results", !| GeocoderResult
                "address_descriptor", AddressDescriptor.Type
                "plus_code", Forward.PlacePlusCode.Type
            ]

    let Geocoder =
        Class "google.maps.Geocoder"
        |+> Static [Constructor T<unit>]
        |+> Instance [
            "geocode" => GeocoderRequest * !? ((Type.ArrayOf GeocoderResult * GeocoderStatus) ^-> T<unit>) ^-> Promise[GeocoderResponse]
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

            "text" =@ T<string>
            |> WithComment "The value of the fare, expressed in the given currency, as a string."

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
        Config "google.maps.DirectionsWaypoint"
            []
            [
                "location", T<string> + LatLng + LatLngLiteral + Place
                "stopover", T<bool>
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
        Config "google.maps.TransitOptions"
            []
            [
                "arrivalTime", Date
                "departureTime", Date
                "modes", !| TransitMode.Type
                "routingPreference", TransitRoutePreference.Type
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
        Config "google.maps.DirectionsRendererOptions"
            []
            [
                "directions", DirectionsResult.Type
                "draggable", T<bool>
                "hideRouteList", T<bool>
                "infoWindow", InfoWindow.Type
                "map", M.Map.Type
                "markerOptions", MarkerOptions.Type
                "panel", HTMLElement
                "polylineOptions", PolylineOptions.Type
                "preserveViewport", T<bool>
                "routeIndex", T<int>
                "suppressBicyclingLayer", T<bool>
                "suppressInfoWindows", T<bool>
                "suppressMarkers", T<bool>
                "suppressPolylines", T<bool>
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
            "directions_changed" => T<unit> ^-> T<unit>
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
            []
            [
                "locations", Type.ArrayOf (LatLng + LatLngLiteral)
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
            "results" =@ Type.ArrayOf ElevationResult
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
            "getElevationAlongPath" => (PathElevationRequest * !? (Type.ArrayOf ElevationResult ^-> ElevationStatus)) ^-> Promise[PathElevationResponse]
            |> WithComment "Makes an elevation request along a path, where the elevation data are returned as distance-based samples along that path."

            "getElevationForLocations" => (LocationElevationRequest * !? (Type.ArrayOf ElevationResult ^-> ElevationStatus)) ^-> Promise[LocationElevationResponse]
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
            "element" =? HTMLElement
            |> WithComment "This field is read-only. The DOM Element backing the view."

            // METHODS
            "addEventListener" => T<string> * T<obj->unit> * !?(T<bool> + AddEventListenerOptions) ^-> T<unit>
            |> WithComment "This function is not yet available for usage."

            "removeEventListener" => T<string> * T<obj->unit> * !?(T<bool> + EventListenerOptions) ^-> T<unit>
            |> WithComment "Removes an event listener previously registered with addEventListener from the target. See removeEventListener"
        ]

    let Settings = 
        Class "google.maps.Settings"
        |+> Static [
            "getInstance" => T<unit> ^-> TSelf 
        ]
        |+> Instance [
            "experienceIds" =@ T<string>
            "fetchAppCheckToken" =@ Promise[T<obj>]
        ]