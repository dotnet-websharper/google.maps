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

module Map =

    open WebSharper.InterfaceGenerator
    open Notation

    let MapTypeId = Forward.MapTypeId

    let MapRestriction =
        Class "google.maps.MapRestriction"
        |+> Instance [
            "latLngBounds" =@ Base.LatLngBounds + Base.LatLngBoundsLiteral
            |> WithComment "When set, a user can only pan and zoom inside the given bounds. Bounds can restrict both longitude and latitude, or can restrict latitude only. For latitude-only bounds use west and east longitudes of -180 and 180, respectively, for example, latLngBounds: {north: northLat, south: southLat, west: -180, east: 180}."

            "strictBounds" =@ T<bool>
            |> WithComment "Bounds can be made more restrictive by setting the strictBounds flag to true. This reduces how far a user can zoom out, ensuring that everything outside of the restricted bounds stays hidden. The default is false, meaning that a user can zoom out until the entire bounded area is in view, possibly including areas outside the bounded area."
        ]

    let MapOptions =
        Class "google.maps.MapOptions"
        |+> Static [
            Ctor [
                Base.LatLng.Type?Center
                T<int>?Zoom
            ]
            |> WithInline "{center: $Center, zoom: $Zoom}"
        ]
        |+> Instance [
            "backgroundColor" =@ T<string>
            |> WithComment "Color used for the background of the Map div. This color will be visible when tiles have not yet loaded as the user pans. This option can only be set when the map is initialized."

            "center" =@ (Base.LatLng + Base.LatLngLiteral)
            |> WithComment "The initial Map center. Required."

            "clickableIcons" =@ T<bool>
            |> WithComment "When false, map icons are not clickable. A map icon represents a point of interest, also known as a POI."

            "controlSize" =@ T<int>
            |> WithComment "Size in pixels of the controls appearing on the map. This value must be supplied directly when creating the Map, updating this value later may bring the controls into an undefined state. Only governs the controls made by the Maps API itself. Does not scale developer created custom controls."

            "disableDefaultUI" =@ T<bool>
            |> WithComment "Enables/disables all default UI buttons. May be overridden individually. Does not disable the keyboard controls, which are separately controlled by the MapOptions.keyboardShortcuts option. Does not disable gesture controls, which are separately controlled by the MapOptions.gestureHandling option."

            "disableDoubleClickZoom" =@ T<bool>
            |> WithComment "Note: This property is not recommended. To disable zooming on double click, you can use the gestureHandling property, and set it to \"none\"."

            "draggable" =@ T<bool>
            |> WithComment "If false, prevents the map from being dragged. Dragging is enabled by default."
            |> ObsoleteWithMessage "Deprecated: Deprecated in 2017. To disable dragging on the map, you can use the gestureHandling property, and set it to \"none\"."

            "draggableCursor" =@ T<string>
            |> WithComment "The name or url of the cursor to display when mousing over a draggable map. This property uses the css cursor attribute to change the icon. As with the css property, you must specify at least one fallback cursor that is not a URL. For example: draggableCursor: 'url(http://www.example.com/icon.png), auto;'."

            "draggingCursor" =@ T<string>
            |> WithComment "The name or url of the cursor to display when the map is being dragged. This property uses the css cursor attribute to change the icon. As with the css property, you must specify at least one fallback cursor that is not a URL. For example: draggingCursor: 'url(http://www.example.com/icon.png), auto;'."

            "fullscreenControl" =@ T<bool>
            |> WithComment "The enabled/disabled state of the Fullscreen control."

            "fullscreenControlOptions" =@ Controls.FullscreenControlOptions
            |> WithComment "The display options for the Fullscreen control."

            "gestureHandling" =@ T<string>
            |> WithComment """This setting controls how the API handles gestures on the map. Allowed values:

    "cooperative": Scroll events and one-finger touch gestures scroll the page, and do not zoom or pan the map. Two-finger touch gestures pan and zoom the map. Scroll events with a ctrl key or ⌘ key pressed zoom the map.
    In this mode the map cooperates with the page.
    "greedy": All touch gestures and scroll events pan or zoom the map.
    "none": The map cannot be panned or zoomed by user gestures.
    "auto": (default) Gesture handling is either cooperative or greedy, depending on whether the page is scrollable or in an iframe."""

            "heading" =@ T<float>
            |> WithComment "The heading for aerial imagery in degrees measured clockwise from cardinal direction North. Headings are snapped to the nearest available angle for which imagery is available."

            "isFractionalZoomEnabled" =@ T<bool>
            |> WithComment "Whether the map should allow fractional zoom levels. Listen to isfractionalzoomenabled_changed to know when the default has been set. Default: true for vector maps and false for raster maps"

            "keyboardShortcuts" =@ T<bool>
            |> WithComment "If false, prevents the map from being controlled by the keyboard. Keyboard shortcuts are enabled by default."

            "mapId" =@ T<string>
            |> WithComment "The Map ID of the map. This parameter cannot be set or changed after a map is instantiated."

            "mapTypeControl" =@ T<bool>
            |> WithComment "The initial enabled/disabled state of the Map type control."

            "mapTypeControlOptions" =@ Controls.MapTypeControlOptions
            |> WithComment "The initial display options for the Map type control."

            "mapTypeId" =@ MapTypeId
            |> WithComment "The initial Map mapTypeId. Defaults to ROADMAP."

            "maxZoom" =@ T<int>
            |> WithComment "The maximum zoom level which will be displayed on the map. If omitted, or set to null, the maximum zoom from the current map type is used instead. Valid zoom values are numbers from zero up to the supported maximum zoom level."

            "minZoom" =@ T<int>
            |> WithComment "The minimum zoom level which will be displayed on the map. If omitted, or set to null, the minimum zoom from the current map type is used instead. Valid zoom values are numbers from zero up to the supported maximum zoom level."

            "noClear" =@ T<bool>
            |> WithComment "If true, do not clear the contents of the Map div."

            "panControl" =@ T<bool>
            |> WithComment "The enabled/disabled state of the Pan control."
            |> ObsoleteWithMessage "Deprecated: The Pan control is deprecated as of September 2015."

            "panControlOptions" =@ Controls.PanControlOptions
            |> WithComment "The display options for the Pan control."
            |> ObsoleteWithMessage "Deprecated: The Pan control is deprecated as of September 2015."

            "restriction" =@ MapRestriction
            |> WithComment "Defines a boundary that restricts the area of the map accessible to users. When set, a user can only pan and zoom while the camera view stays inside the limits of the boundary."

            "rotateControl" =@ T<bool>
            |> WithComment "The enabled/disabled state of the Rotate control."

            "rotateControlOptions" =@ Controls.RotateControlOptions
            |> WithComment "The display options for the Rotate control."

            "scaleControl" =@ T<bool>
            |> WithComment "The initial enabled/disabled state of the Scale control."

            "scaleControlOptions" =@ Controls.ScaleControlOptions
            |> WithComment "The initial display options for the Scale control."

            "scrollwheel" =@ T<bool>
            |> WithComment "If false, disables zooming on the map using a mouse scroll wheel. The scrollwheel is enabled by default.

Note: This property is not recommended. To disable zooming using scrollwheel, you can use the gestureHandling property, and set it to either \"cooperative\" or \"none\"."

            "streetView" =@ StreetView.StreetViewPanorama
            |> WithComment "A StreetViewPanorama to display when the Street View pegman is dropped on the map. If no panorama is specified, a default StreetViewPanorama will be displayed in the map's div when the pegman is dropped."

            "streetViewControl" =@ T<bool>
            |> WithComment "The initial enabled/disabled state of the Street View Pegman control. This control is part of the default UI, and should be set to false when displaying a map type on which the Street View road overlay should not appear (e.g. a non-Earth map type)."

            "streetViewControlOptions" =@ Controls.StreetViewControlOptions
            |> WithComment "The initial display options for the Street View Pegman control."

            "styles" =@ Type.ArrayOf MapTypes.MapTypeStyle
            |> WithComment "Styles to apply to each of the default map types. Note that for Satellite/Hybrid and Terrain modes, these styles will only apply to labels and geometry."

            "tilt" =@ T<float>
            |> WithComment "Controls the automatic switching behavior for the angle of incidence of the map. The only allowed values are 0 and 45. The value 0 causes the map to always use a 0° overhead view regardless of the zoom level and viewport. The value 45 causes the tilt angle to automatically switch to 45 whenever 45° imagery is available for the current zoom level and viewport, and switch back to 0 whenever 45° imagery is not available (this is the default behavior). 45° imagery is only available for SATELLITE and HYBRID map types, within some locations, and at some zoom levels. Note: getTilt returns the current tilt angle, not the value specified by this option. Because getTilt and this option refer to different things, do not bind() the tilt property; doing so may yield unpredictable effects."

            "zoom" =@ T<int>
            |> WithComment "The initial Map zoom level. Valid zoom values are numbers from zero up to the supported maximum zoom level. Larger zoom values correspond to a higher resolution."

            "zoomControl" =@ T<bool>
            |> WithComment "The enabled/disabled state of the Zoom control."

            "zoomControlOptions" =@ Controls.ZoomControlOptions
            |> WithComment "The display options for the Zoom control."
        ]

    let CameraOptions =
        Interface "google.maps.CameraOptions"
        |+> [
            "center" =@ Base.LatLngLiteral + Base.LatLng
            "heading" =@ T<int>
            "tilt" =@ T<int>
            "zoom" =@ T<int>
        ]

    let RenderingType =
        Class "google.maps.places.RenderingType"
        |+> Static [
            "RASTER" =? TSelf
            |> WithComment "Indicates that the map is a raster map."

            "UNINITIALIZED" =? TSelf
            |> WithComment "Indicates that it is unknown yet whether the map is vector or raster, because the map has not finished initializing yet."

            "VECTOR" =? TSelf
            |> WithComment " 	Indicates that the map is a vector map."
        ]

    let MapCapabilities =
        Class "google.maps.MapCapabilities"
        |+> Instance [
            "isAdvancedMarkersAvailable" =@ T<bool>
            |> WithComment "If true, this map is configured properly to allow for the use of advanced markers. Note that you must still import the marker library in order to use advanced markers. See https://goo.gle/gmp-isAdvancedMarkersAvailable for more information."

            "isDataDrivenStylingAvailable" =@ T<bool>
            |> WithComment "If true, this map is configured properly to allow for the use of data-driven styling for at least one FeatureLayer. See https://goo.gle/gmp-data-driven-styling and https://goo.gle/gmp-FeatureLayerIsAvailable for more information."
        ]

    let MapMouseEvent =
        Interface "google.maps.MapMouseEvent"
        |+> [
            //TODO: locate TouchEvent and PointerEvent
            // "domEvent" =? T<MouseEvent> + T<TouchEvent> + T<PointerEvent> + T<KeyboardEvent> + T<Event>
            "domEvent" =? MouseEvent + KeyboardEvent + JsEvent
            |> WithComment "The corresponding native DOM event. Developers should not rely on target, currentTarget, relatedTarget and path properties being defined and consistent. Developers should not also rely on the DOM structure of the internal implementation of the Maps API. Due to internal event mapping, the domEvent may have different semantics from the MapMouseEvent (e.g. a MapMouseEvent \"click\" may have a domEvent of type KeyboardEvent)."

            "latLng" =@ Base.LatLng
            |> WithComment "The latitude/longitude that was below the cursor when the event occurred."

            "stop" => T<unit -> unit>
        ]

    let IconMouseEvent =
        Class "google.maps.IconMouseEvent"
        |=> Inherits MapMouseEvent

    let Map =
        Forward.Map
        |=> Inherits MVC.MVCObject
        |+> Static [
            Ctor [
                Node?mapDiv
                !? MapOptions?options
            ]
            |> WithComment "Creates a new map inside of the given HTML container, which is typically a DIV element."

            "DEMO_MAP_ID" =? TSelf
            |> WithComment "Map ID which can be used for code samples which require a Map ID. This Map ID is not intended for use in production applications and cannot be used for features which require cloud configuration (such as Cloud Styling)."
        ]
        |+> Instance [
                "fitBounds" => (Base.LatLngBounds + Base.LatLngBoundsLiteral)?bounds * (T<int> + Base.Padding)?padding ^-> T<unit>
                |> WithComment """Sets the viewport to contain the given bounds.
Note: When the map is set to display: none, the fitBounds function reads the map's size as 0x0, and therefore does not do anything. To change the viewport while the map is hidden, set the map to visibility: hidden, thereby ensuring the map div has an actual size. For vector maps, this method sets the map's tilt and heading to their default zero values. Calling this method may cause a smooth animation as the map pans and zooms to fit the bounds. Whether or not this method animates depends on an internal heuristic."""

                "getBounds" => T<unit> ^-> Base.LatLngBounds
                |> WithComment "Returns the lat/lng bounds of the current viewport. If more than one copy of the world is visible, the bounds range in longitude from -180 to 180 degrees inclusive. If the map is not yet initialized or center and zoom have not been set then the result is undefined. For vector maps with non-zero tilt or heading, the returned lat/lng bounds represents the smallest bounding box that includes the visible region of the map's viewport. See MapCanvasProjection.getVisibleRegion for getting the exact visible region of the map's viewport."

                "getCenter" => T<unit> ^-> Base.LatLng
                |> WithComment "Returns the position displayed at the center of the map. Note that this LatLng object is not wrapped. See LatLng for more information. If the center or bounds have not been set then the result is undefined."

                "getClickableIcons" => T<unit> ^-> T<bool>
                |> WithComment "Returns the clickability of the map icons. A map icon represents a point of interest, also known as a POI. If the returned value is true, then the icons are clickable on the map."

                "getDatasetFeatureLayer" => T<string> ^-> Forward.FeatureLayer
                |> WithComment "Returns the FeatureLayer for the specified datasetId. Dataset IDs must be configured in the Google Cloud Console. If the dataset ID is not associated with the map's map style, or if Data-driven styling is not available (no map ID, no vector tiles, no Data-Driven Styling feature layers or Datasets configured in the Map Style), this logs an error, and the resulting FeatureLayer.isAvailable will be false."

                "getDiv" => T<unit> ^-> Element

                "getFeatureLayer" => Forward.FeatureType ^-> Forward.FeatureLayer
                |> WithComment "Returns the FeatureLayer of the specific FeatureType. A FeatureLayer must be enabled in the Google Cloud Console. If a FeatureLayer of the specified FeatureType does not exist on this map, or if Data-driven styling is not available (no map ID, no vector tiles, and no FeatureLayer enabled in the map style), this logs an error, and the resulting FeatureLayer.isAvailable will be false."

                "getHeading" => T<unit> ^-> T<float>
                |> WithComment "Returns the compass heading of the map. The heading value is measured in degrees (clockwise) from cardinal direction North. If the map is not yet initialized then the result is undefined."

                "getMapCapabilities" => T<unit> ^-> MapCapabilities
                |> WithComment "Informs the caller of the current capabilities available to the map based on the Map ID that was provided."

                "getMapTypeId" => T<unit> ^-> MapTypeId

                "getProjection" => T<unit> ^-> MapTypes.Projection
                |> WithComment "Returns the current Projection. If the map is not yet initialized then the result is undefined. Listen to the projection_changed event and check its value to ensure it is not undefined."

                "getRenderingType" => T<unit> ^-> RenderingType
                |> WithComment "Returns the current RenderingType of the map."

                "getStreetView" => T<unit> ^-> StreetView.StreetViewPanorama
                |> WithComment "Returns the default StreetViewPanorama bound to the map, which may be a default panorama embedded within the map, or the panorama set using setStreetView(). Changes to the map's streetViewControl will be reflected in the display of such a bound panorama."

                "getTilt" => T<unit> ^-> T<float>
                |> WithComment "Returns the current angle of incidence of the map, in degrees from the viewport plane to the map plane. For raster maps, the result will be 0 for imagery taken directly overhead or 45 for 45° imagery. This method does not return the value set by setTilt. See setTilt for details."

                "getZoom" => T<unit> ^-> T<int>
                |> WithComment "Returns the zoom of the map. If the zoom has not been set then the result is undefined."

                "moveCamera" => CameraOptions ^-> T<unit>
                |> WithComment "Immediately sets the map's camera to the target camera options, without animation."

                "panBy" => (T<int> * T<int>) ^-> T<unit>
                |> WithComment "Changes the center of the map by the given distance in pixels. If the distance is less than both the width and height of the map, the transition will be smoothly animated. Note that the map coordinate system increases from west to east (for x values) and north to south (for y values)."

                "panTo" => Base.LatLng + Base.LatLngLiteral ^-> T<unit>
                |> WithComment "Changes the center of the map to the given LatLng. If the change is less than both the width and height of the map, the transition will be smoothly animated."

                "panToBounds" => Base.LatLngBounds + Base.LatLngBoundsLiteral ^-> T<unit> * Base.Padding
                |> WithComment "Pans the map by the minimum amount necessary to contain the given LatLngBounds. It makes no guarantee where on the map the bounds will be, except that the map will be panned to show as much of the bounds as possible inside {currentMapSizeInPx} - {padding}. For both raster and vector maps, the map's zoom, tilt, and heading will not be changed."

                "setCenter" => Base.LatLng + Base.LatLngLiteral ^-> T<unit>

                "setClickableIcons" => T<bool> ^-> T<unit>
                |> WithComment "Controls whether the map icons are clickable or not. A map icon represents a point of interest, also known as a POI. To disable the clickability of map icons, pass a value of false to this method."

                "setHeading" => T<float> ^-> T<unit>
                |> WithComment "Sets the compass heading for map measured in degrees from cardinal direction North. For raster maps, this method only applies to aerial imagery."

                "setMapTypeId" => (MapTypeId + T<string>) ^-> T<unit>

                "setOptions" => MapOptions ^-> T<unit>

                "setStreetView" => StreetView.StreetViewPanorama ^-> T<unit>
                |> WithComment "Binds a StreetViewPanorama to the map. This panorama overrides the default StreetViewPanorama, allowing the map to bind to an external panorama outside of the map. Setting the panorama to null binds the default embedded panorama back to the map."

                "setTilt" => T<float> ^-> T<unit>
                |> WithComment "For vector maps, sets the angle of incidence of the map. The allowed values are restricted depending on the zoom level of the map. For raster maps, controls the automatic switching behavior for the angle of incidence of the map. The only allowed values are 0 and 45. setTilt(0) causes the map to always use a 0° overhead view regardless of the zoom level and viewport. setTilt(45) causes the tilt angle to automatically switch to 45 whenever 45° imagery is available for the current zoom level and viewport, and switch back to 0 whenever 45° imagery is not available (this is the default behavior). 45° imagery is only available for satellite and hybrid map types, within some locations, and at some zoom levels. Note: getTilt returns the current tilt angle, not the value set by setTilt. Because getTilt and setTilt refer to different things, do not bind() the tilt property; doing so may yield unpredictable effects."

                "setZoom" => T<int> ^-> T<unit>
                |> WithComment "Sets the zoom of the map."

                "controls" =@ (Type.ArrayOf MVC.MVCArray.[Node])
                |> WithComment "Additional controls to attach to the map. To add a control to the map, add the control's <div> to the MVCArray corresponding to the ControlPosition where it should be rendered."

                "data" =@ Forward.Data
                |> WithComment "An instance of Data, bound to the map. Add features to this Data object to conveniently display them on this map."

                "mapTypes" =@ MapTypes.MapTypeRegistry
                |> WithComment "A registry of MapType instances by string ID."

                "overlayMapTypes" =@ MVC.MVCArray.[MapTypes.MapType]
                |> WithComment "Additional map types to overlay. Overlay map types will display on top of the base map they are attached to, in the order in which they appear in the overlayMapTypes array (overlays with higher index values are displayed in front of overlays with lower index values)."

                // EVENTS
                "bounds_changed" =@ T<obj> -* T<unit> ^-> T<unit>
                |> WithComment "This event is fired when the viewport bounds have changed."

                "center_changed" =@ T<obj> -* T<unit> ^-> T<unit>
                |> WithComment "This event is fired when the map center property changes."

                "click" =@ T<obj> -* (MapMouseEvent + IconMouseEvent) ^-> T<unit>
                |> WithComment "This event is fired when the user clicks on the map. A MapMouseEvent with properties for the clicked location is returned unless a place icon was clicked, in which case an IconMouseEvent with a place ID is returned. IconMouseEvent and MapMouseEvent are identical, except that IconMouseEvent has the place ID field. The event can always be treated as an MapMouseEvent when the place ID is not important. The click event is not fired if a marker or info window was clicked."

                "contextmenu" =@ T<obj> -* MapMouseEvent ^-> T<unit>
                |> WithComment "This event is fired when the DOM contextmenu event is fired on the map container."

                "dblclick" =@ T<obj> -* MapMouseEvent ^-> T<unit>
                |> WithComment "This event is fired when the user double-clicks on the map. Note that the click event will sometimes fire once and sometimes twice, right before this one."

                "drag" =@ T<obj> -* T<unit> ^-> T<unit>
                |> WithComment "This event is repeatedly fired while the user drags the map."

                "dragend" =@ T<obj> -* T<unit> ^-> T<unit>
                |> WithComment "This event is fired when the user stops dragging the map."

                "dragstart" =@ T<obj> -* T<unit> ^-> T<unit>
                |> WithComment "This event is fired when the user starts dragging the map."

                "heading_changed" =@ T<obj> -* T<unit> ^-> T<unit>
                |> WithComment "This event is fired when the map heading property changes."

                "idle" =@ T<obj> -* T<unit> ^-> T<unit>
                |> WithComment "This event is fired when the map becomes idle after panning or zooming."

                "isfractionalzoomenabled_changed" =@ T<obj> -* T<unit> ^-> T<unit>
                |> WithComment "This event is fired when the isFractionalZoomEnabled property has changed."

                "mapcapabilities_changed" =@ T<obj> -* T<unit> ^-> T<unit>
                |> WithComment "This event is fired when the map capabilities change."

                "maptypeid_changed" =@ T<obj> -* T<unit> ^-> T<unit>
                |> WithComment "This event is fired when the mapTypeId property changes."

                "mousemove" =@ T<obj> -* MapMouseEvent ^-> T<unit>
                |> WithComment "This event is fired whenever the user's mouse moves over the map container."

                "mouseout" =@ T<obj> -* MapMouseEvent ^-> T<unit>
                |> WithComment "This event is fired when the user's mouse exits the map container."

                "mouseover" =@ T<obj> -* MapMouseEvent ^-> T<unit>
                |> WithComment "This event is fired when the user's mouse enters the map container."

                "projection_changed" =@ T<obj> -* T<unit> ^-> T<unit>
                |> WithComment "This event is fired when the projection has changed."

                "renderingtype_changed" =@ T<obj> -* T<unit> ^-> T<unit>
                |> WithComment "This event is fired when the renderingType has changed."

                "tilesloaded" =@ T<obj> -* T<unit> ^-> T<unit>
                |> WithComment "This event is fired when the visible tiles have finished loading."

                "tilt_changed" =@ T<obj> -* T<unit> ^-> T<unit>
                |> WithComment "This event is fired when the map tilt property changes."

                "zoom_changed" =@ T<obj> -* T<unit> ^-> T<unit>
                |> WithComment "This event is fired when the map zoom property changes."

                "rightclick" =@ T<obj> -* MapMouseEvent ^-> T<unit>
                |> WithComment "This event is fired when the user right-clicks on the map."
                |> ObsoleteWithMessage "Deprecated: Use the Map.contextmenu event instead in order to support usage patterns like control-click on macOS."
         ]

    let MapElementOptions =
        Class "google.maps.MapElementOptions"
        |+> Instance [
            "center" =@ Base.LatLng + Base.LatLngLiteral
            |> WithComment "The initial Map center."

            "mapId" =@ T<string>
            |> WithComment "The Map ID of the map. This parameter cannot be set or changed after a map is instantiated."

            "zoom" =@ T<int>
            |> WithComment "The initial Map zoom level. Valid zoom values are numbers from zero up to the supported maximum zoom level. Larger zoom values correspond to a higher resolution."
        ]

    let ZoomChangeEvent =
        Class "google.maps.ZoomChangeEvent"
        |=> Inherits Events.Event

    let MapElement =
        Class "google.maps.MapElement"
        |=> Inherits HTMLElement
        |+> Static [
            Ctor [
                !? MapElementOptions?options
            ]
        ]
        |+> Instance [
            "center" =@ (Base.LatLng + Base.LatLngLiteral)
            |> WithComment "The center latitude/longitude of the map."

            "innerMap" =@ Map
            |> WithComment "A reference to the Map that the MapElement uses internally."

            "mapId" =@ T<string>
            |> WithComment "The Map ID of the map. See the Map ID documentation for more information."

            "zoom" =@ T<int>
            |> WithComment "The zoom level of the map."

            //TODO: where is EventListener?
            // "addEventListener" => T<string> * (Events.EventListener + T<EventListenerObject>) * !?(T<bool> + T<AddEventListenerOptions>) ^-> T<unit>
            "addEventListener" => T<string> * T<obj->unit> * !?(T<bool> + AddEventListenerOptions) ^-> T<unit>
            |> WithComment "Sets up a function that will be called whenever the specified event is delivered to the target. See addEventListener"

            //TODO: where is EventListener?
            // "removeEventListener" => T<string> * (T<EventListener> + T<EventListenerObject>) * !?(T<bool> + T<EventListenerOptions>) ^-> T<unit>
            "removeEventListener" => T<string> * T<obj->unit> * !?(T<bool> + EventListenerOptions) ^-> T<unit>
            |> WithComment "Removes an event listener previously registered with addEventListener from the target. See removeEventListener"


            // EVENTS
            "gmp-zoomchange" => T<obj> -* ZoomChangeEvent ^-> T<unit>
            |> WithComment "This event is fired when the map zoom property changes."
        ]

    let VisibleRegion =
        Class "google.maps.VisibleRegion"
        |+> Instance [
            "farLeft" =@ Base.LatLng
            "farRight" =@ Base.LatLng
            "latLngBounds" =@ Base.LatLngBounds
            "nearLeft" =@ Base.LatLng
            "nearRight" =@ Base.LatLng
        ]
