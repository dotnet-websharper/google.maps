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
    open Base

    let MapTypeId = Forward.MapTypeId

    let MapRestriction =
        Class "google.maps.MapRestriction"
        |+> Instance [
            "latLngBounds" =@ Base.LatLngBounds + Base.LatLngBoundsLiteral
            |> WithComment "When set, a user can only pan and zoom inside the given bounds. Bounds can restrict both longitude and latitude, or can restrict latitude only. For latitude-only bounds use west and east longitudes of -180 and 180, respectively, for example, latLngBounds: {north: northLat, south: southLat, west: -180, east: 180}."

            "strictBounds" =@ T<bool>
            |> WithComment "Bounds can be made more restrictive by setting the strictBounds flag to true. This reduces how far a user can zoom out, ensuring that everything outside of the restricted bounds stays hidden. The default is false, meaning that a user can zoom out until the entire bounded area is in view, possibly including areas outside the bounded area."
        ]

    let RenderingType =
        Pattern.EnumStrings "google.maps.RenderingType" [
            "RASTER"
            "UNINITIALIZED"
            "VECTOR"
        ]

    let ColorScheme =
        Pattern.EnumStrings "google.maps.ColorScheme" [
            "DARK"
            "FOLLOW_SYSTEM"
            "LIGHT"
        ]

    let MapOptions =
        Config "google.maps.MapOptions" []
            [
                "backgroundColor", T<string>
                "cameraControl", T<bool>
                "cameraControlOptions", Controls.CameraControlOptions.Type
                "center", LatLng + LatLngLiteral
                "clickableIcons", T<bool>
                "colorScheme", ColorScheme + T<string>
                "controlSize", T<int>
                "disableDefaultUI", T<bool>
                "disableDoubleClickZoom", T<bool>
                "draggable", T<bool>
                "draggableCursor", T<string>
                "draggingCursor", T<string>
                "fullscreenControl", T<bool>
                "fullscreenControlOptions", Controls.FullscreenControlOptions.Type
                "gestureHandling", T<string>
                "heading", T<int>
                "headingInteractionEnabled", T<bool>
                "internalUsageAttributionIds", Type.ArrayOf T<string>
                "isFractionalZoomEnabled", T<bool>
                "keyboardShortcuts", T<bool>
                "mapId", T<string>
                "mapTypeControl", T<bool>
                "mapTypeControlOptions", Controls.MapTypeControlOptions.Type
                "mapTypeId", MapTypeId + T<string>
                "maxZoom", T<int>
                "minZoom", T<int>
                "noClear", T<bool>
                "panControl", T<bool>
                "panControlOptions", Controls.PanControlOptions.Type
                "renderingType", RenderingType.Type
                "restriction", MapRestriction.Type
                "rotateControl", T<bool>
                "rotateControlOptions", Controls.RotateControlOptions.Type
                "scaleControl", T<bool>
                "scaleControlOptions", Controls.ScaleControlOptions.Type
                "scrollwheel", T<bool>
                "streetView", StreetView.StreetViewPanorama.Type
                "streetViewControl", T<bool>
                "streetViewControlOptions", Controls.StreetViewControlOptions.Type
                "styles", Type.ArrayOf MapTypes.MapTypeStyle
                "tilt", T<int>
                "tiltInteractionEnabled", T<bool>
                "zoom", T<int>
                "zoomControl", T<bool>
                "zoomControlOptions", Controls.ZoomControlOptions.Type
                "vector", T<bool>
            ]

    let CameraOptions =
        Config "google.maps.CameraOptions"
            []
            [
                "center", Base.LatLngLiteral + Base.LatLng
                "heading", T<float>
                "tilt", T<float>
                "zoom", T<float>
            ]

    let MapCapabilities =
        Config "google.maps.MapCapabilities"
            []
            [
                "isAdvancedMarkersAvailable", T<bool>
                "isDataDrivenStylingAvailable", T<bool>
                "isWebGLOverlayViewAvailable", T<bool>
            ]

    let MapMouseEventProperties = 
        [
            "domEvent", MouseEvent + KeyboardEvent + JsEvent + T<obj>
            "latLng", Base.LatLng.Type
            "stop", T<unit> ^-> T<unit>
        ]

    // TODO: might be interface
    let MapMouseEvent =
        Config "google.maps.MapMouseEvent"
            []
            MapMouseEventProperties            

    let IconMouseEvent =
        Class "google.maps.IconMouseEvent"
        |=> Inherits MapMouseEvent 
        |+> Instance [
            "placeId" =@ T<string>
            |> WithComment "The place ID of the place that was clicked. This place ID can be used to query more information about the feature that was clicked."
        ]

    let Map =
        Forward.Map
        |=> Inherits MVC.MVCObject
        |+> Static [
            Ctor [
                HTMLElement?mapDiv
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

                "getDiv" => T<unit> ^-> HTMLElement

                "getFeatureLayer" => Forward.FeatureType ^-> Forward.FeatureLayer
                |> WithComment "Returns the FeatureLayer of the specific FeatureType. A FeatureLayer must be enabled in the Google Cloud Console. If a FeatureLayer of the specified FeatureType does not exist on this map, or if Data-driven styling is not available (no map ID, no vector tiles, and no FeatureLayer enabled in the map style), this logs an error, and the resulting FeatureLayer.isAvailable will be false."

                "getHeading" => T<unit> ^-> T<float>
                |> WithComment "Returns the compass heading of the map. The heading value is measured in degrees (clockwise) from cardinal direction North. If the map is not yet initialized then the result is undefined."

                "getHeadingInteractionEnabled" => T<unit> ^-> !| T<bool>
                |> WithComment "Returns whether heading interactions are enabled. This option is only in effect when the map is a vector map. If not set in code, then the cloud configuration for the map ID will be used (if available)."

                "getInternalUsageAttributionIds" => T<unit> ^-> !| T<string>
                |> WithComment "Returns the list of usage attribution IDs, which help Google understand which libraries and samples are helpful to developers, such as usage of a marker clustering library."

                "getMapCapabilities" => T<unit> ^-> MapCapabilities
                |> WithComment "Informs the caller of the current capabilities available to the map based on the Map ID that was provided."

                "getMapTypeId" => T<unit> ^-> T<string> + MapTypeId

                "getProjection" => T<unit> ^-> MapTypes.Projection
                |> WithComment "Returns the current Projection. If the map is not yet initialized then the result is undefined. Listen to the projection_changed event and check its value to ensure it is not undefined."

                "getRenderingType" => T<unit> ^-> RenderingType
                |> WithComment "Returns the current RenderingType of the map."

                "getStreetView" => T<unit> ^-> StreetView.StreetViewPanorama
                |> WithComment "Returns the default StreetViewPanorama bound to the map, which may be a default panorama embedded within the map, or the panorama set using setStreetView(). Changes to the map's streetViewControl will be reflected in the display of such a bound panorama."

                "getTilt" => T<unit> ^-> T<float>
                |> WithComment "Returns the current angle of incidence of the map, in degrees from the viewport plane to the map plane. For raster maps, the result will be 0 for imagery taken directly overhead or 45 for 45° imagery. This method does not return the value set by setTilt. See setTilt for details."

                "getTiltInteractionEnabled" => T<unit> ^-> T<bool>
                |> WithComment "Returns whether tilt interactions are enabled. This option is only in effect when the map is a vector map. If not set in code, then the cloud configuration for the map ID will be used (if available)."

                "getZoom" => T<unit> ^-> T<int>
                |> WithComment "Returns the zoom of the map. If the zoom has not been set then the result is undefined."

                "moveCamera" => CameraOptions ^-> T<unit>
                |> WithComment "Immediately sets the map's camera to the target camera options, without animation."

                "panBy" => (T<int> * T<int>) ^-> T<unit>
                |> WithComment "Changes the center of the map by the given distance in pixels. If the distance is less than both the width and height of the map, the transition will be smoothly animated. Note that the map coordinate system increases from west to east (for x values) and north to south (for y values)."

                "panTo" => Base.LatLng + Base.LatLngLiteral ^-> T<unit>
                |> WithComment "Changes the center of the map to the given LatLng. If the change is less than both the width and height of the map, the transition will be smoothly animated."

                "panToBounds" => (Base.LatLngBounds + Base.LatLngBoundsLiteral) * !? (T<int> + Padding) ^-> T<unit>
                |> WithComment "Pans the map by the minimum amount necessary to contain the given LatLngBounds. It makes no guarantee where on the map the bounds will be, except that the map will be panned to show as much of the bounds as possible inside {currentMapSizeInPx} - {padding}. For both raster and vector maps, the map's zoom, tilt, and heading will not be changed."

                "setCenter" => Base.LatLng + Base.LatLngLiteral ^-> T<unit>

                "setClickableIcons" => T<bool> ^-> T<unit>
                |> WithComment "Controls whether the map icons are clickable or not. A map icon represents a point of interest, also known as a POI. To disable the clickability of map icons, pass a value of false to this method."

                "setHeading" => T<float> ^-> T<unit>
                |> WithComment "Sets the compass heading for map measured in degrees from cardinal direction North. For raster maps, this method only applies to aerial imagery."

                "setHeadingInteractionEnabled" => T<bool> ^-> T<unit>
                |> WithComment "Sets whether heading interactions are enabled. This option is only in effect when the map is a vector map. If not set in code, then the cloud configuration for the map ID will be used (if available)."

                "setMapTypeId" => (T<string> + MapTypeId) ^-> T<unit>

                "setOptions" => MapOptions ^-> T<unit>

                "setRenderingType" => RenderingType.Type ^-> T<unit>
                |> WithComment "Sets the current RenderingType of the map."

                "setStreetView" => StreetView.StreetViewPanorama ^-> T<unit>
                |> WithComment "Binds a StreetViewPanorama to the map. This panorama overrides the default StreetViewPanorama, allowing the map to bind to an external panorama outside of the map. Setting the panorama to null binds the default embedded panorama back to the map."

                "setTilt" => T<float> ^-> T<unit>
                |> WithComment "For vector maps, sets the angle of incidence of the map. The allowed values are restricted depending on the zoom level of the map. For raster maps, controls the automatic switching behavior for the angle of incidence of the map. The only allowed values are 0 and 45. setTilt(0) causes the map to always use a 0° overhead view regardless of the zoom level and viewport. setTilt(45) causes the tilt angle to automatically switch to 45 whenever 45° imagery is available for the current zoom level and viewport, and switch back to 0 whenever 45° imagery is not available (this is the default behavior). 45° imagery is only available for satellite and hybrid map types, within some locations, and at some zoom levels. Note: getTilt returns the current tilt angle, not the value set by setTilt. Because getTilt and setTilt refer to different things, do not bind() the tilt property; doing so may yield unpredictable effects."

                "setTiltInteractionEnabled" => T<bool> ^-> T<unit>
                |> WithComment "Sets whether tilt interactions are enabled. This option is only in effect when the map is a vector map. If not set in code, then the cloud configuration for the map ID will be used (if available)."

                "setZoom" => T<int> ^-> T<unit>
                |> WithComment "Sets the zoom of the map."

                "controls" =@ (Type.ArrayOf MVC.MVCArray.[HTMLElement])
                |> WithComment "Additional controls to attach to the map. To add a control to the map, add the control's <div> to the MVCArray corresponding to the ControlPosition where it should be rendered."

                "data" =@ Forward.Data
                |> WithComment "An instance of Data, bound to the map. Add features to this Data object to conveniently display them on this map."

                "mapTypes" =@ MapTypes.MapTypeRegistry
                |> WithComment "A registry of MapType instances by string ID."

                "overlayMapTypes" =@ MVC.MVCArray.[MapTypes.MapType]
                |> WithComment "Additional map types to overlay. Overlay map types will display on top of the base map they are attached to, in the order in which they appear in the overlayMapTypes array (overlays with higher index values are displayed in front of overlays with lower index values)."

                // EVENTS
                "bounds_changed" =@ T<unit> ^-> T<unit>
                |> WithComment "This event is fired when the viewport bounds have changed."

                "center_changed" =@ T<unit> ^-> T<unit>
                |> WithComment "This event is fired when the map center property changes."

                "click" =@ (MapMouseEvent + IconMouseEvent) ^-> T<unit>
                |> WithComment "This event is fired when the user clicks on the map. A MapMouseEvent with properties for the clicked location is returned unless a place icon was clicked, in which case an IconMouseEvent with a place ID is returned. IconMouseEvent and MapMouseEvent are identical, except that IconMouseEvent has the place ID field. The event can always be treated as an MapMouseEvent when the place ID is not important. The click event is not fired if a marker or info window was clicked."

                "contextmenu" =@ MapMouseEvent ^-> T<unit>
                |> WithComment "This event is fired when the DOM contextmenu event is fired on the map container."

                "dblclick" =@ MapMouseEvent ^-> T<unit>
                |> WithComment "This event is fired when the user double-clicks on the map. Note that the click event will sometimes fire once and sometimes twice, right before this one."

                "drag" =@ T<unit> ^-> T<unit>
                |> WithComment "This event is repeatedly fired while the user drags the map."

                "dragend" =@ T<unit> ^-> T<unit>
                |> WithComment "This event is fired when the user stops dragging the map."

                "dragstart" =@ T<unit> ^-> T<unit>
                |> WithComment "This event is fired when the user starts dragging the map."

                "heading_changed" =@ T<unit> ^-> T<unit>
                |> WithComment "This event is fired when the map heading property changes."

                "idle" =@ T<unit> ^-> T<unit>
                |> WithComment "This event is fired when the map becomes idle after panning or zooming."

                "isfractionalzoomenabled_changed" =@ T<unit> ^-> T<unit>
                |> WithComment "This event is fired when the isFractionalZoomEnabled property has changed."

                "mapcapabilities_changed" =@ T<unit> ^-> T<unit>
                |> WithComment "This event is fired when the map capabilities change."

                "maptypeid_changed" =@ T<unit> ^-> T<unit>
                |> WithComment "This event is fired when the mapTypeId property changes."

                "mousemove" =@ MapMouseEvent ^-> T<unit>
                |> WithComment "This event is fired whenever the user's mouse moves over the map container."

                "mouseout" =@ MapMouseEvent ^-> T<unit>
                |> WithComment "This event is fired when the user's mouse exits the map container."

                "mouseover" =@ MapMouseEvent ^-> T<unit>
                |> WithComment "This event is fired when the user's mouse enters the map container."

                "projection_changed" =@ T<unit> ^-> T<unit>
                |> WithComment "This event is fired when the projection has changed."

                "renderingtype_changed" =@ T<unit> ^-> T<unit>
                |> WithComment "This event is fired when the renderingType has changed."

                "tilesloaded" =@ T<unit> ^-> T<unit>
                |> WithComment "This event is fired when the visible tiles have finished loading."

                "tilt_changed" =@ T<unit> ^-> T<unit>
                |> WithComment "This event is fired when the map tilt property changes."

                "zoom_changed" =@ T<unit> ^-> T<unit>
                |> WithComment "This event is fired when the map zoom property changes."

                "rightclick" =@ MapMouseEvent ^-> T<unit>
                |> WithComment "This event is fired when the user right-clicks on the map."
                |> ObsoleteWithMessage "Deprecated: Use the Map.contextmenu event instead in order to support usage patterns like control-click on macOS."
         ]

    let MapElementOptions =
        Config "google.maps.MapElementOptions"
            []
            [
                // The center latitude/longitude of the map.
                "center", Base.LatLng + Base.LatLngLiteral

                // Whether the map should allow user control of the camera heading.
                "headingInteractionDisabled", T<bool>

                // Adds a usage attribution ID to the initializer.
                "internalUsageAttributionIds", Type.ArrayOf T<string>

                // The map ID of the map.
                "mapId", T<string>

                // Whether the map should be a raster or vector map.
                "renderingType", RenderingType.Type

                // Whether the map should allow user control of the camera tilt.
                "tiltInteractionDisabled", T<bool>

                // The zoom level of the map.
                "zoom", T<int>
            ]


    let ZoomChangeEvent =
        Class "google.maps.ZoomChangeEvent"
        |=> Inherits Events.Event
        |+> Static [Constructor T<unit>]

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

            "headingInteractionDisabled" =@ T<bool>
            |> WithComment "Whether the map should allow user control of the camera heading (rotation). This option is only in effect when the map is a vector map."

            "innerMap" =@ Map
            |> WithComment "A reference to the Map that the MapElement uses internally."

            "internalUsageAttributionIds" =@ !| T<string>
            |> WithComment "Adds a usage attribution ID to the initializer. To opt out, delete or use empty string."

            "mapId" =@ T<string>
            |> WithComment "The map ID of the map. Cannot be set or changed after instantiation."

            "renderingType" =@ RenderingType
            |> WithComment "Whether the map should be a raster or vector map."

            "tiltInteractionDisabled" =@ T<bool>
            |> WithComment "Whether the map should allow user control of the camera tilt."

            "zoom" =@ T<int>
            |> WithComment "The zoom level of the map."

            "addEventListener" => T<string> * T<obj->unit> * !?(T<bool> + AddEventListenerOptions) ^-> T<unit>
            |> WithComment "Sets up a function that will be called whenever the specified event is delivered."

            "removeEventListener" => T<string> * T<obj->unit> * !?(T<bool> + EventListenerOptions) ^-> T<unit>
            |> WithComment "Removes an event listener previously registered with addEventListener."

            // EVENTS
            "gmp-zoomchange" => ZoomChangeEvent ^-> T<unit>
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