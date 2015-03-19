/// Definitions for the Map part of the API. See:
/// http://developers.google.com/maps/documentation/javascript/reference
module WebSharper.Google.Maps.Map

open WebSharper.InterfaceGenerator
open WebSharper.Google.Maps.Notation

let MapTypeId = Forward.MapTypeId

let MapOptions =
    Class "MapOptions"
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

        "center" =@ Base.LatLng
        |> WithComment "The initial Map center. Required."

        "disableDefaultUI" =@ T<bool>
        |> WithComment "Enables/disables all default UI. May be overridden individually."

        "disableDoubleClickZoom" =@ T<bool>
        |> WithComment "Enables/disables zoom and center on double click. Enabled by default."

        "draggable" =@ T<bool>
        |> WithComment "If false, prevents the map from being dragged. Dragging is enabled by default."

        "draggableCursor" =@ T<string>
        |> WithComment "The name or url of the cursor to display when mousing over a draggable map."

        "draggingCursor" =@ T<string>
        |> WithComment "The name or url of the cursor to display when the map is being dragged."

        "heading" =@ T<float>
        |> WithComment "The heading for aerial imagery in degrees measured clockwise from cardinal direction North. Headings are snapped to the nearest available angle for which imagery is available."

        "keyboardShortcuts" =@ T<bool>
        |> WithComment "If false, prevents the map from being controlled by the keyboard. Keyboard shortcuts are enabled by default."

        "mapMaker" =@ T<bool>
        |> WithComment "True if Map Maker tiles should be used instead of regular tiles."

        "mapTypeControl" =@ T<bool>
        |> WithComment "The initial enabled/disabled state of the Map type control."

        "mapTypeControlOptions" =@ Controls.MapTypeControlOptions
        |> WithComment "The initial display options for the Map type control."

        "mapTypeId" =@ MapTypeId
        |> WithComment "The initial Map mapTypeId. Defaults to ROADMAP."

        "maxZoom" =@ T<int>
        |> WithComment "The maximum zoom level which will be displayed on the map. If omitted, or set to null, the maximum zoom from the current map type is used instead."

        "minZoom" =@ T<int>
        |> WithComment "The minimum zoom level which will be displayed on the map. If omitted, or set to null, the minimum zoom from the current map type is used instead."

        "noClear" =@ T<bool>
        |> WithComment "If true, do not clear the contents of the Map div."

        "overviewMapControl" =@ T<bool>
        |> WithComment "The enabled/disabled state of the Overview Map control."

        "overviewMapControlOptions" =@ Controls.OverviewMapControlOptions
        |> WithComment "The display options for the Overview Map control."

        "panControl" =@ T<bool>
        |> WithComment "The enabled/disabled state of the Pan control."

        "panControlOptions" =@ Controls.PanControlOptions
        |> WithComment "The display options for the Pan control."

        "rotateControl" =@ T<bool>
        |> WithComment "The enabled/disabled state of the Rotate control."

        "rotateControlOptions" =@ Controls.RotateControlOptions
        |> WithComment "The display options for the Rotate control."

        "scaleControl" =@ T<bool>
        |> WithComment "The initial enabled/disabled state of the Scale control."

        "scaleControlOptions" =@ Controls.ScaleControlOptions
        |> WithComment "The initial display options for the Scale control."

        "scrollwheel" =@ T<bool>
        |> WithComment "If false, disables scrollwheel zooming on the map. The scrollwheel is enabled by default."

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
        |> WithComment "The initial Map zoom level. Required."

        "zoomControl" =@ T<bool>
        |> WithComment "The enabled/disabled state of the Zoom control."

        "zoomControlOptions" =@ Controls.ZoomControlOptions
        |> WithComment "The display options for the Zoom control."
    ]

let Map =
    Forward.Map
    |=> Inherits MVC.MVCObject
    |+> Static [
        Ctor [
            Node?mapDiv
            !? MapOptions?options
        ]
        |> WithComment "Creates a new map inside of the given HTML container, which is typically a DIV element."
    ]
    |+> Instance [
            "fitBounds" => Base.LatLngBounds?bounds ^-> T<unit>
            |> WithComment "Sets the maps to fit to the given bounds."

            "getBounds" => T<unit> ^-> Base.LatLngBounds
            |> WithComment "Returns the lat/lng bounds of the current viewport. If more than one copy of the world is visible, the bounds range in longitude from -180 to 180 degrees inclusive. If the map is not yet initialized (i.e. the mapType is still null), or center and zoom have not been set then the result is null or undefined."

            "getCenter" => T<unit> ^-> Base.LatLng
            |> WithComment "Returns the position displayed at the center of the map. Note that this LatLng object is not wrapped. See LatLng for more information."

            "getDiv" => T<unit> ^-> Element

            "getHeading" => T<unit> ^-> T<float>
            |> WithComment "Returns the compass heading of aerial imagery. The heading value is measured in degrees (clockwise) from cardinal direction North."

            "getMapTypeId" => T<unit> ^-> MapTypeId

            "getProjection" => T<unit> ^-> MapTypes.Projection
            |> WithComment "Returns the current Projection. If the map is not yet initialized (i.e. the mapType is still null) then the result is null. Listen to projection_changed and check its value to ensure it is not null."

            "getStreetView" => T<unit> ^-> StreetView.StreetViewPanorama
            |> WithComment "Returns the default StreetViewPanorama bound to the map, which may be a default panorama embedded within the map, or the panorama set using setStreetView(). Changes to the map's streetViewControl will be reflected in the display of such a bound panorama."

            "getTilt" => T<unit> ^-> T<float>
            |> WithComment "Returns the current angle of incidence of the map, in degrees from the viewport plane to the map plane. The result will be 0 for imagery taken directly overhead or 45 for 45° imagery. 45° imagery is only available for SATELLITE and HYBRID map types, within some locations, and at some zoom levels. Note: This method does not return the value set by setTilt. See setTilt for details."

            "getZoom" => T<unit> ^-> T<int>

            "panBy" => (T<int> * T<int>) ^-> T<unit>
            |> WithComment "Changes the center of the map by the given distance in pixels. If the distance is less than both the width and height of the map, the transition will be smoothly animated. Note that the map coordinate system increases from west to east (for x values) and north to south (for y values)."

            "panTo" => Base.LatLng ^-> T<unit>
            |> WithComment "Changes the center of the map to the given LatLng. If the change is less than both the width and height of the map, the transition will be smoothly animated."

            "panToBounds" => Base.LatLngBounds ^-> T<unit>
            |> WithComment "Pans the map by the minimum amount necessary to contain the given LatLngBounds. It makes no guarantee where on the map the bounds will be, except that as much of the bounds as possible will be visible. The bounds will be positioned inside the area bounded by the map type and navigation (pan, zoom, and Street View) controls, if they are present on the map. If the bounds is larger than the map, the map will be shifted to include the northwest corner of the bounds. If the change in the map's position is less than both the width and height of the map, the transition will be smoothly animated."

            "setCenter" => Base.LatLng ^-> T<unit>

            "setHeading" => T<float> ^-> T<unit>
            |> WithComment "Sets the compass heading for aerial imagery measured in degrees from cardinal direction North."

            "setMapTypeId" => (MapTypeId + T<string>) ^-> T<unit>

            "setOptions" => MapOptions ^-> T<unit>

            "setStreetView" => StreetView.StreetViewPanorama ^-> T<unit>
            |> WithComment "Binds a StreetViewPanorama to the map. This panorama overrides the default StreetViewPanorama, allowing the map to bind to an external panorama outside of the map. Setting the panorama to null binds the default embedded panorama back to the map."

            "setTilt" => T<float> ^-> T<unit>
            |> WithComment "Controls the automatic switching behavior for the angle of incidence of the map. The only allowed values are 0 and 45. setTilt(0) causes the map to always use a 0° overhead view regardless of the zoom level and viewport. setTilt(45) causes the tilt angle to automatically switch to 45 whenever 45° imagery is available for the current zoom level and viewport, and switch back to 0 whenever 45° imagery is not available (this is the default behavior). 45° imagery is only available for SATELLITE and HYBRID map types, within some locations, and at some zoom levels. Note: getTilt returns the current tilt angle, not the value set by setTilt. Because getTilt and setTilt refer to different things, do not bind() the tilt property; doing so may yield unpredictable effects."

            "setZoom" => T<int> ^-> T<unit>

            "controls" =@ (Type.ArrayOf MVC.MVCArray.[Node])
            |> WithComment "Additional controls to attach to the map. To add a control to the map, add the control's <div> to the MVCArray corresponding to the ControlPosition where it should be rendered."

            "mapTypes" =@ MapTypes.MapTypeRegistry
            |> WithComment "A registry of MapType instances by string ID."

            "overlayMapTypes" =@ MVC.MVCArray.[MapTypes.MapType]
            |> WithComment "Additional map types to overlay."

            // TODO: events
    ]
