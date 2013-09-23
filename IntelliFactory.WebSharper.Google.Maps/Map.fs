/// Definitions for the Map part of the API. See:
/// http://developers.google.com/maps/documentation/javascript/reference
module IntelliFactory.WebSharper.Google.Maps.Map

open IntelliFactory.WebSharper.InterfaceGenerator
open IntelliFactory.WebSharper.Google.Maps.Notation

let MapTypeId = Forward.MapTypeId

let MapOptions =
    let opt = [
        // Color used for the background of the Map div. This color will be visible when tiles have not yet loaded as the user pans. This option can only be set when the map is initialized.
        "backgroundColor", T<string>
        // Enables/disables all default UI. May be overridden individually.
        "disableDefaultUI", T<bool>
        // Enables/disables zoom and center on double click. Enabled by default.
        "disableDoubleClickZoom", T<bool>
        // If false, prevents the map from being dragged. Dragging is enabled by default.
        "draggable", T<bool>
        // The name or url of the cursor to display on a draggable object.
        "draggableCursor", T<string>
        // The name or url of the cursor to display when an object is dragging.
        "draggingCursor", T<string>
        // The heading for aerial imagery in degrees measured clockwise from cardinal direction
        // North. Headings are snapped to the nearest available angle for which imagery is available.
        "heading", T<float>
        // If false, prevents the map from being controlled by the keyboard. Keyboard shortcuts are enabled by default.
        "keyboardShortcuts", T<bool>
        // True if Map Maker tiles should be used instead of regular tiles.
        "mapMaker", T<bool>
        // The initial enabled/disabled state of the Map type control.
        "mapTypeControl", T<bool>
        // The initial display options for the Map type control.
        "mapTypeControlOptions", Controls.MapTypeControlOptions.Type
        // The maximum zoom level which will be displayed on the map. If omitted, or set to null, the maximum zoom from the current map type is used instead.
        "maxZoom", T<int>
        // The minimum zoom level which will be displayed on the map. If omitted, or set to null, the minimum zoom from the current map type is used instead.
        "minZoom", T<int>
        // If true, do not clear the contents of the Map div.
        "noClear", T<bool>
        // The enabled/disabled state of the Overview Map control.
        "overviewMapControl", T<bool>
        // The display options for the Overview Map control.
        "overviewMapControlOptions", Controls.OverviewMapControlOptions.Type
        // The enabled/disabled state of the Pan control.
        "panControl", T<bool>
        // The display options for the Pan control.
        "panControlOptions", Controls.PanControlOptions.Type
        // The enabled/disabled state of the Rotate control.
        "rotateControl", T<bool>
        // The display options for the Rotate control.
        "rotateControlOptions", Controls.RotateControlOptions.Type
        // The initial enabled/disabled state of the scale control.
        "scaleControl", T<bool>
        // The initial display options for the scale control.
        "scaleControlOptions", Controls.ScaleControlOptions.Type
        // If false, disables scrollwheel zooming on the map. The scrollwheel is enabled by default.
        "scrollwheel", T<bool>
        // A StreetViewPanorama to display when the Street View pegman is dropped on the map. If no panorama is specified, a default StreetViewPanorama will be displayed in the map's div when the pegman is dropped.
        "streetView", StreetView.StreetViewPanorama.Type
        // The initial enabled/disabled state of the Street View pegman control.
        "streetViewControl", T<bool>
        // The initial display options for the Street View Pegman control.
        "streetViewControlOptions", Controls.StreetViewControlOptions.Type
        // Styles to apply to each of the default map types.
        // Note that styles will apply only to the labels and geometry in
        // Satellite/Hybrid and Terrain modes.
        "styles", Type.ArrayOf MapTypes.MapTypeStyle.Type
        // The angle of incidence of the map as measured in degrees from the viewport plane
        // to the map plane. The only currently supported values are 0, indicating no angle
        // of incidence (no tilt), and 45, indicating a tilt of 45deg;. 45deg; imagery is only
        // available for SATELLITE and HYBRID map types, within some locations, and at some zoom levels.
        "tilt", T<float>
        // The enabled/disabled state of the Zoom control.
        "zoomControl", T<bool>
        // The display options for the Zoom control.
        "zoomControlOptions", Controls.ZoomControlOptions.Type
    ]
    let req = [
        // The initial Map center. Required.
        "center", Base.LatLng.Type

        // The initial Map mapTypeId. Required.
        "mapTypeId", MapTypeId.Type

        // The initial Map zoom level. Required.
        "zoom", T<int>
    ]
    Pattern.Config "MapOptions" { Optional = opt; Required = req }

let Map =
    let Map = Class "google.maps.Map"
    Map
    |=> Inherits MVC.MVCObject
    |=> Forward.Map
    |+>
        [
            // Creates a new map inside of the given HTML container, which is typically a DIV element
            Ctor [
                Node?mapDiv
                !? MapOptions?options
            ]
        ]
    |+> Protocol [
            // Sets the maps to fit to the given bounds.
            "fitBounds" => Base.LatLngBounds ^-> T<unit>
            // Returns the lat/lng bounds of the current viewport. If the map is not yet initialized (i.e. the mapType is still null), or center and zoom have not been set then the result is null.
            "getBounds" => T<unit> ^-> Base.LatLngBounds
            "getCenter" => T<unit> ^-> Base.LatLng
            "getDiv" => T<unit> ^-> Element
            "getHeading" => T<unit> ^-> T<float>
            "getMapTypeId" => T<unit> ^-> MapTypeId
            // Returns the current Projection. If the map is not yet initialized (i.e. the mapType is still null) then the result is null. Listen to projection_changed and check its value to ensure it is not null.
            "getProjection" => T<unit> ^-> MapTypes.Projection
            // Returns the default StreetViewPanorama bound to the map, which may be a default panorama embedded within the map, or the panorama set using setStreetView(). Changes to the map's streetViewControl will be reflected in the display of such a bound panorama.
            "getStreetView" => T<unit> ^-> StreetView.StreetViewPanorama
            "getTilt" => T<unit> ^-> T<float>
            "getZoom" => T<unit> ^-> T<int>
            // Changes the center of the map by the given distance in pixels. If the distance is less than both the width and height of the map, the transition will be smoothly animated. Note that the map coordinate system increases from west to east (for x values) and north to south (for y values).
            "panBy" => (T<int> * T<int>) ^-> T<unit>
            // Changes the center of the map to the given LatLng. If the change is less than both the width and height of the map, the transition will be smoothly animated.
            "panTo" => Base.LatLng ^-> T<unit>
            // Pans the map by the minimum amount necessary to contain the given LatLngBounds. It makes no guarantee where on the map the bounds will be, except that as much of the bounds as possible will be visible. The bounds will be positioned inside the area bounded by the map type and navigation controls, if they are present on the map. If the bounds is larger than the map, the map will be shifted to include the northwest corner of the bounds. If the change in the map's position is less than both the width and height of the map, the transition will be smoothly animated.
            "panToBounds" => Base.LatLngBounds ^-> T<unit>
            "setCenter" => Base.LatLng ^-> T<unit>
            "setHeading" => T<float> ^-> T<unit>
            "setMapTypeId" => (MapTypeId + T<string>) ^-> T<unit>
            "setOptions" => MapOptions ^-> T<unit>
            // Binds a StreetViewPanorama to the map. This panorama overrides the default StreetViewPanorama, allowing the map to bind to an external panorama outside of the map. Setting the panorama to null binds the default embedded panorama back to the map.
            "setStreetView" => StreetView.StreetViewPanorama ^-> T<unit>
            "setTilt" => T<float> ^-> T<unit>
            "setZoom" => T<int> ^-> T<unit>

            // Additional controls to attach to the map. To add a control to the map, add the control's <div> to the MVCArray corresponding to the ControlPosition where it should be rendered.
            "controls" =@ (Type.ArrayOf <| MVC.MVCArray Node)
            // A registry of MapType instances by string ID.
            "mapTypes" =@ MapTypes.MapTypeRegistry
            // Additional map types to overlay.
            "overlayMapTypes" =@ MVC.MVCArray MapTypes.MapType

            // TODO: events
    ]
