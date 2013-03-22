/// The rest of the spec.
/// TODO: this code needs revision to update to the latest 3.11 API
module IntelliFactory.WebSharper.Google.Maps.Specification

open IntelliFactory.WebSharper.InterfaceGenerator
open IntelliFactory.WebSharper.Google.Maps.Base
open IntelliFactory.WebSharper.Google.Maps.Notation
module M = IntelliFactory.WebSharper.Google.Maps.Map

let NavigationControlStyle =
    Pattern.EnumInlines "NavigationControlStyle" [
        // The small zoom control similar to the one used by the native Maps application on Android.
        "ANDROID", "google.maps.NavigationControlStyle.ANDROID"
        // The default navigation control. The control which DEFAULT maps to will vary according to map size and other factors. It may change in future versions of the API.
        "DEFAULT", "google.maps.NavigationControlStyle.DEFAULT"
        // The small, zoom only control.
        "SMALL", "google.maps.NavigationControlStyle.SMALL"
        // The larger control, with the zoom slider and pan directional pad.
        "ZOOM_PAN", "google.maps.NavigationControlStyle.ZOOM_PAN"
    ]

let NavigationControlOptions =
    Pattern.Config "NavigationControlOptions" {
        Required = []
        Optional =
            [
                // Position id. Used to specify the position of the control on the map. The default position is TOP_LEFT.
                "position", Controls.ControlPosition.Type
                // Style id. Used to select what style of navigation control to display.
                "style", NavigationControlStyle.Type
            ]
    }

let ScaleControlStyle =
    Pattern.EnumInlines "ScaleControlStyle" ["DEFAULT", "google.maps.ScaleControlStyle"]

let ScaleControlOptions =
    Pattern.Config "ScaleControlOptions" {
        Required = []
        Optional =
            [
                // Position id. Used to specify the position of the control on the map. The default position is BOTTOM_LEFT.
                "position", Controls.ControlPosition.Type
                // Style id. Used to select what style of scale control to display.
                "style", ScaleControlStyle.Type
            ]
    }

let MapPanes =
    Class "google.maps.MapPanes"
    |+> Protocol [
        // This pane contains the info window. It is above all map overlays. (Pane 6).
        "floatPane" =@ Node
        // This pane contains the info window shadow. It is above the overlayImage, so that markers can be in the shadow of the info window. (Pane 4).
        "floatShadow" =@ Node
        // This pane is the lowest pane and is above the tiles. (Pane 0).
        "mapPane" =@ Node
        // This pane contains the marker foreground images. (Pane 3).
        "overlayImage" =@ Node
        // This pane contains polylines, polygons, ground overlays and tile layer overlays. (Pane 1).
        "overlayLayer" =@ Node
        // This pane contains transparent elements that receive DOM mouse events for the markers. It is above the floatShadow, so that markers in the shadow of the info window can be clickable. (Pane 5).
        "overlayMouseTarget" =@ Node
        // This pane contains the marker shadows. (Pane 2).
        "overlayShadow" =@ Node
    ]


let MapCanvasProjection =
    Class "google.maps.MapCanvasProjection"
    |+> Protocol [
        // Computes the geographical coordinates from pixel coordinates in the map's container.
        "fromContainerPixelToLatLng" => Point ^-> LatLng
        // Computes the geographical coordinates from pixel coordinates in the div that holds the draggable map.
        "fromDivPixelToLatLng" => Point ^-> LatLng
        // Computes the pixel coordinates of the given geographical location in the DOM element the map's outer container.
        "fromLatLngToContainerPixel" => LatLng ^-> Point
        // Computes the pixel coordinates of the given geographical location in the DOM element that holds the draggable map.
        "fromLatLngToDivPixel" => LatLng ^-> Point
        // The width of the world in pixels in the current zoom level.
        "getWorldWidth" => T<unit> ^-> T<int>
    ]


let StyledMapType, StyledMapTypeOptions =
    let StyledMapType = Class "google.maps.StyledMapType"
    let StyledMapTypeOptions =
        Pattern.Config "StyledMapTypeOptions" {
            Required = []
            Optional =
                [
                    // Alt text to display when this MapType's button is hovered over in the map type control.
                    "alt", T<string>
                    // A StyledMapType whose style should be used as a base for defining a StyledMapType's style. The MapTypeStyle rules will be appended to the base's styles.
                    "baseMapType", StyledMapType.Type
                    // The maximum zoom level for the map when displaying this MapType. Optional.
                    "maxZoom", T<int>
                    // The minimum zoom level for the map when displaying this MapType. Optional.
                    "minZoom", T<int>
                    // Name to display in the map type control.
                    "name", T<string>
                ]
        }

    let StyledMapType =
        StyledMapType
        |=> Implements [MapTypes.MapType]
        |+> [Constructor (Type.ArrayOf MapTypes.MapTypeStyle * !? StyledMapTypeOptions)]
        |=> Inherits MVC.MVCObject
    (StyledMapType, StyledMapTypeOptions)

let RectangleOptions =
    Pattern.Config "RectangleOptions" {
        Required = []
        Optional =
            [
                // The bounds.
                "bounds", LatLngBounds.Type
                // Indicates whether this Rectangle handles click events. Defaults to true.
                "clickable", T<bool>
                // The fill color in HTML hex style, ie. "#00AAFF"
                "fillColor", T<string>
                // The fill opacity between 0.0 and 1.0
                "fillOpacity", T<float>
                // Map on which to display Rectangle.
                "map", M.Map.Type
                // The stroke color in HTML hex style, ie. "#FFAA00"
                "strokeColor", T<string>
                // The stroke opacity between 0.0 and 1.0
                "strokeOpacity", T<float>
                // The stroke width in pixels.
                "strokeWeight", T<int>
                // The zIndex compared to other polys.
                "zIndex", T<int>
            ]
    }

let CircleOptions =
    Pattern.Config "CircleOptions" {
        Required = []
        Optional =
            [
                // The center
                "center", LatLng.Type
                // Indicates whether this Circle handles click events. Defaults to true.
                "clickable", T<bool>
                // The fill color in HTML hex style, ie. "#00AAFF"
                "fillColor", T<string>
                // The fill opacity between 0.0 and 1.0
                "fillOpacity", T<float>
                // Map on which to display Circle.
                "map", M.Map.Type
                // The radius in meters on the Earth's surface
                "radius", T<float>
                // The stroke color in HTML hex style, ie. "#FFAA00"
                "strokeColor", T<string>
                // The stroke opacity between 0.0 and 1.0
                "strokeOpacity", T<float>
                // The stroke width in pixels.
                "strokeWeight", T<int>
                // The zIndex compared to other polys.
                "zIndex", T<int>
            ]
    }

let Circle =
    Class "google.maps.Circle"
    |+> [Constructor CircleOptions]
    |+> Protocol [

        // Gets the LatLngBounds of this Circle.
        "getBounds" => T<unit> ^-> LatLngBounds
        // Returns the center of this circle.
        "getCenter" => T<unit> ^-> LatLng
        // Returns the map on which this circle is displayed.
        "getMap" => T<unit> ^-> M.Map
        // Returns the radius of this circle (in meters).
        "getRadius" => T<unit> ^-> T<float>
        // Sets the center of this circle.
        "setCenter" => LatLng ^-> T<unit>
        // Renders the circle on the specified map. If map is set to null, the circle will be removed.
        "setMap" => M.Map ^-> T<unit>
        // Sets the radius of this circle (in meters).
        "setRadius" => T<float> ^-> T<unit>
        "setOptions" => CircleOptions ^-> T<unit>
    ]

let Rectangle =
    Class "google.maps.Rectangle"
    |+> [Constructor RectangleOptions]
    |+> Protocol [
        // Returns the bounds of this rectangle.
        "getBounds" => T<unit> ^-> LatLngBounds
        // Returns the map on which this rectangle is displayed.
        "getMap" => T<unit> ^-> M.Map
        // Sets the bounds of this rectangle.
        "setBounds" => LatLngBounds ^-> T<unit>
        // Renders the rectangle on the specified map. If map is set to null, the rectangle will be removed.
        "setMap" => M.Map ^-> T<unit>
        //
        "setOptions" => (RectangleOptions) ^-> T<unit>
    ]

let GroundOverlayOptions =
    Pattern.Config "GroundOverlayOptions" {
        Required = []
        Optional =
            [
                // If true, the ground overlay can receive click events.
                "clickable", T<bool>
                // The map on which to display the overlay.
                "map", M.Map.Type
            ]
    }

let GroundOverlay =
    Class "google.maps.GroundOverlay"
    |+> [Constructor GroundOverlayOptions]
    |=> Inherits MVC.MVCObject
    |+> Protocol [
        // Gets the LatLngBounds of this overlay.
        "getBounds" => T<unit> ^-> LatLngBounds
        // Returns the map on which this ground overlay is displayed.
        "getMap" => T<unit> ^-> M.Map
        // Gets the url of the projected image.
        "getUrl" => T<unit> ^-> T<string>
        // Renders the ground overlay on the specified map. If map is set to null, the overlay is removed.
        "setMap" => (M.Map) ^-> T<unit>
    ]

let BicyclingLayer =
    Class "google.maps.BicyclingLayer"
    |+> [Constructor T<unit>]
    |=> Inherits MVC.MVCObject
    |+> Protocol [
        // Returns the map on which this layer is displayed.
        "getMap" => T<unit> ^-> M.Map
        // Renders the layer on the specified map. If map is set to null, the layer will be removed.
        "setMap" => (M.Map) ^-> T<unit>
    ]

let FusionTablesLayerOptions =
    Pattern.Config "FusionTablesLayerOptions" {
        Required = []
        Optional =
            [
                // By default, table data is displayed as geographic features. If true, the layer is used to display a heatmap representing the density of the geographic features returned by querying the selected table.
                "heatmap", T<bool>
                // The map on which to display the layer.
                "map", M.Map.Type
                // A Fusion Tables query to apply when selecting the data to display. Queries should not be URL escaped.
                "query", T<string>
                // Suppress the rendering of info windows when layer features are clicked.
                "suppressInfoWindows", T<bool>
            ]
    }

let FusionTablesLayer =
    Class "google.maps.FusionTablesLayer"
    |+> [Constructor <| T<string> * !? FusionTablesLayerOptions]
    |=> Inherits MVC.MVCObject
    |+> Protocol [
        // Returns the map on which this layer is displayed.
        "getMap" => T<unit> ^-> M.Map
        // Returns the ID of the table from which to query data.
        "getTableId" => T<unit> ^-> T<int>
        "getQuery" => T<unit> ^-> T<string>
        // Renders the layer on the specified map. If map is set to null, the layer will be removed.
        "setMap" => M.Map ^-> T<unit>
        // Sets the query to execute on the table specified by the tableId property. The layer will be updated to display the results of the query.
        "setQuery" => T<string> ^-> T<unit>
        "setOptions" => (FusionTablesLayerOptions) ^-> T<unit>
        // Sets the ID of the table from which to query data. Setting this value will cause the layer to be redrawn.
        "setTableId" => (T<int>) ^-> T<unit>
    ]

let FusionTablesMouseEvent =
    Class "FusionTablesMouseEvent"
    |+> Protocol [
        // Pre-rendered HTML content, as placed in the infowindow by the default UI.
        "infoWindowHtml" =@ T<string>
        // The position at which to anchor an infowindow on the clicked feature.
        "latLng" =@ LatLng.Type
        // The offset to apply to an infowindow anchored on the clicked feature.
        "pixelOffset" =@ Size.Type
        // A collection of FusionTablesCell objects, indexed by column name, representing the contents of the table row which included the clicked feature.
        "row" =@ T<obj>
    ]

let FusionTablesCell =
    Class "FusionTablesCell"
    |+> Protocol [
        // The name of the column in which the cell was located.
        "columnName" =@ T<string>
        // The contents of the cell.
        "value" =@ T<string>
    ]

let KmlAuthor =
    Class "KmlAuthor"
    |+> Protocol [
        // The author's e-mail address, or an empty T<string> if not specified.
        "email" =@ T<string>
        // The author's name, or an empty T<string> if not specified.
        "name" =@ T<string>
        // The author's home page, or an empty T<string> if not specified.
        "uri" =@ T<string>
    ]

let KmlFeatureData =
    Class "KmlFeatureData"
    |+> Protocol [
        // The feature's <atom:author>, extracted from the layer markup (if specified).
        "author" =@ KmlAuthor.Type
        // The feature's <description>, extracted from the layer markup.
        "description" =@ T<string>
        // The feature's <id>, extracted from the layer markup. If no <id> has been specified, a unique ID will be generated for this feature.
        "id" =@ T<string>
        // The feature's balloon styled text, if set.
        "infoWindowHtml" =@ T<string>
        // The feature's <name>, extracted from the layer markup.
        "name" =@ T<string>
        // The feature's <Snippet>, extracted from the layer markup.
        "snippet" =@ T<string>
    ]

let KmlMouseEvent =
    Class "KmlMouseEvent"
    |+> Protocol [
        // A KmlFeatureData object, containing information about the clicked feature.
        "featureData" =@ KmlFeatureData.Type
        // The position at which to anchor an infowindow on the clicked feature.
        "latLng" =@ LatLng.Type
        // The offset to apply to an infowindow anchored on the clicked feature.
        "pixelOffset" =@ Size.Type
    ]


let KmlLayerMetadata =
    Class "KmlLayerMetadata"
    |+> Protocol [
        // The layer's <atom:author>, extracted from the layer markup.
        "author" =@ KmlAuthor.Type
        // The layer's <description>, extracted from the layer markup.
        "description" =@ T<string>
        // The layer's <name>, extracted from the layer markup.
        "name" =@ T<string>
        // The layer's <Snippet>, extracted from the layer markup
        "snippet" =@ T<string>
    ]

let KmlLayerOptions =
    Pattern.Config "KmlLayerOptions" {
        Required = []
        Optional =
            [
                // The map on which to display the layer.
                "map", M.Map.Type
                // By default, the input map is centered and zoomed to the bounding box of the contents of the layer. If this option is set to true, the viewport is left unchanged, unless the map's center and zoom were never set.
                "preserveViewport", T<bool>
                // Suppress the rendering of info windows when layer features are clicked.
                "suppressInfoWindows", T<bool>
            ]
    }

let KmlLayer =
    Class "google.maps.KmlLayer"
    |+> [Constructor <| T<string> * !? KmlLayerOptions]
    |=> Inherits MVC.MVCObject
    |+> Protocol [
        // Get the default viewport for the layer being displayed.
        "getDefaultViewport" => T<unit> ^-> LatLngBounds
        // Get the map on which the KML Layer is being rendered.
        "getMap" => T<unit> ^-> M.Map
        // Get the metadata associated with this layer, as specified in the layer markup.
        "getMetadata" => T<unit> ^-> KmlLayerMetadata
        // Get the URL of the geographic markup which is being displayed.
        "getUrl" => T<unit> ^-> T<string>
        // Renders the KML Layer on the specified map. If map is set to null, the layer is removed.
        "setMap" => (M.Map) ^-> T<unit>
    ]

let TrafficLayer =
    Class "google.maps.TrafficLayer"
    |+> [Constructor T<unit>]
    |=> Inherits MVC.MVCObject
    |+> Protocol [
        // Returns the map on which this layer is displayed.
        "getMap" => T<unit> ^-> M.Map
        // Renders the layer on the specified map. If map is set to null, the layer will be removed.
        "setMap" => M.Map ^-> T<unit>
    ]

let MarkerShapeType =
    Pattern.EnumStrings "MarkerShapeType" ["circle"; "poly"; "rect"]

let MarkerShape =
    Pattern.Config "MarkerShape" {
        Optional = []
        Required =
        [
            "coord", Type.ArrayOf T<int>
            "type", MarkerShapeType.Type
        ]
    }

let MarkerImage =
    Class "google.maps.MarkerImage"
    |+> [Constructor (T<string> * !? Size * !? Point * !? Point * !? Size)]

let MarkerOptions =
    Pattern.Config "MarkerOptions" {
        Required =
            [
                // Marker position. Required.
                "position", LatLng.Type
            ]
        Optional =
            [
                // If true, the marker receives mouse and touch events. Default value is true.
                "clickable", T<bool>
                // Mouse cursor to show on hover
                "cursor", T<string>
                // If true, the marker can be dragged. Default value is false.
                "draggable", T<bool>
                // If true, the marker shadow will not be displayed.
                "flat", T<bool>
                // Icon for the foreground
                "icon", T<string> + MarkerImage
                // Map on which to display Marker.
                "map", M.Map + StreetView.StreetViewPanorama
                // Shadow image
                "shadow", T<string> + MarkerImage
                // Image map region definition used for drag/click.
                "shape", MarkerShape.Type
                // Rollover text
                "title", T<string>
                // If true, the marker is visible
                "visible", T<bool>
                // All Markers are displayed on the map in order of their zIndex, with higher values displaying in front of Markers with lower values. By default, Markers are displayed according to their latitude, with Markers of lower latitudes appearing in front of Markers at higher latitudes.
                "zIndex", T<int>
            ]
    }

// TODO: Events
let Marker =
    Class "google.maps.Marker"
    |=> Inherits MVC.MVCObject
    |+> [Constructor !? MarkerOptions]
    |+> Protocol [
        "getClickable" => T<unit> ^-> T<bool>
        "getCursor" => T<unit> ^-> T<string>
        "getDraggable" => T<unit> ^-> T<bool>
        "getFlat" => T<unit> ^-> T<bool>
        "getIcon" => T<unit> ^-> T<string> + MarkerImage
        "getMap" => T<unit> ^-> M.Map + StreetView.StreetViewPanorama
        "getPosition" => T<unit> ^-> LatLng
        "getShadow" => T<unit> ^-> T<string> + MarkerImage
        "getShape" => T<unit> ^-> MarkerShape
        "getTitle" => T<unit> ^-> T<string>
        "getVisible" => T<unit> ^-> T<bool>
        "getZIndex" => T<unit> ^-> T<int>
        "setClickable" => T<bool> ^-> T<unit>
        "setCursor" => T<string> ^-> T<unit>
        "setDraggable" => T<bool> ^-> T<unit>
        "setFlat" => T<bool> ^-> T<unit>
        "setIcon" => T<string> + MarkerImage ^-> T<unit>
        // Renders the marker on the specified map or panorama. If map is set to null, the marker will be removed.
        "setMap" => (M.Map + StreetView.StreetViewPanorama) ^-> T<unit>
        "setOptions" => MarkerOptions ^-> T<unit>
        "setPosition" => LatLng ^-> T<unit>
        "setShadow" => T<string> + MarkerImage ^-> T<unit>
        "setShape" => MarkerShape ^-> T<unit>
        "setTitle" => T<string> ^-> T<unit>
        "setVisible" => T<bool> ^-> T<unit>
        "setZIndex" => T<int> ^-> T<unit>
    ]

let PolylineOptions =
    Pattern.Config "PolylineOptions" {
        Required = []
        Optional =
            [
                // Indicates whether this Polyline handles click events. Defaults to true.
                "clickable", T<bool>
                // Render each edge as a geodesic (a segment of a "great circle"). A geodesic is the shortest path between two points along the surface of the Earth.
                "geodesic", T<bool>
                // Map on which to display Polyline.
                "map", M.Map.Type
                "path", MVC.MVCArray LatLng + Type.ArrayOf LatLng
                // The ordered sequence of coordinates of the Polyline. This path may be specified using either a simple array of LatLngs, or an MVCArray of LatLngs. Note that if you pass a simple array, it will be converted to an MVCArray Inserting or removing LatLngs in the MVCArray will automatically update the polyline on the map.
                "Array", LatLng.Type
                // The stroke color in HTML hex style, ie. "#FFAA00"
                "strokeColor", T<string>
                // The stroke opacity between 0.0 and 1.0
                "strokeOpacity", T<float>
                // The stroke width in pixels.
                "strokeWeight", T<int>
                // The zIndex compared to other polys.
                "zIndex", T<int>
            ]
    }

// TODO: Events
let Polyline =
    Class "google.maps.Polyline"
    |=> Inherits MVC.MVCObject
    |+> [Constructor !? PolylineOptions]
    |+> Protocol [
        // Returns the map on which this poly is attached.
        "getMap" => T<unit> ^-> M.Map
        // Retrieves the first path.
        "getPath" => T<unit> ^-> MVC.MVCArray LatLng
        // Renders this Polyline or Polygon on the specified map. If map is set to null, the Poly will be removed.
        "setMap" => M.Map ^-> T<unit>
        //
        "setOptions" => PolylineOptions ^-> T<unit>
        // Sets the first path. See Polyline options for more details.
        "setPath" => (MVC.MVCArray LatLng + Type.ArrayOf LatLng) ^-> T<unit>
    ]

let PolygonOptions =
    Pattern.Config "PolygonOptions" {
        Required = []
        Optional =
            [
                // Indicates whether this Polygon handles click events. Defaults to true.
                "clickable", T<bool>
                // The fill color in HTML hex style, ie. "#00AAFF"
                "fillColor", T<string>
                // The fill opacity between 0.0 and 1.0
                "fillOpacity", T<float>
                // Render each edge as a geodesic (a segment of a "great circle"). A geodesic is the shortest path between two points along the surface of the Earth.
                "geodesic", T<bool>
                // Map on which to display Polygon.
                "map", M.Map.Type
                // The ordered sequence of coordinates that designates a closed loop. Unlike polylines, a polygon may consist of one or more paths. As a result, the paths property may specify one or more arrays of LatLng coordinates. Simple polygons may be defined using a single array of LatLngs. More complex polygons may specify an array of arrays. Any simple arrays are convered into MVCArrays. Inserting or removing LatLngs from the MVCArray will automatically update the polygon on the map.
                "paths", (MVC.MVCArray LatLng + MVC.MVCArray (MVC.MVCArray LatLng)
                            + Type.ArrayOf LatLng + Type.ArrayOf (Type.ArrayOf LatLng))
                // The stroke color in HTML hex style, ie. "#FFAA00"
                "strokeColor", T<string>
                // The stroke opacity between 0.0 and 1.0
                "strokeOpacity", T<float>
                // The stroke width in pixels.
                "strokeWeight", T<int>
                // The zIndex compared to other polys.
                "zIndex", T<int>
            ]

    }

// TODO: Events
let Polygon =
    Class "google.maps.Polygon"
    |=> Inherits MVC.MVCObject
    |+> [Constructor !? PolygonOptions]
    |+> Protocol [
        // Returns the map on which this poly is attached.
        "getMap" => T<unit> ^-> M.Map.Type
        // Retrieves the first path.
        "getPath" => T<unit> ^-> MVC.MVCArray LatLng
        // Retrieves the paths for this Polygon.
        "getPaths" => T<unit> ^-> MVC.MVCArray (MVC.MVCArray LatLng)
        // Renders this Polyline or Polygon on the specified map. If map is set to null, the Poly will be removed.
        "setMap" => M.Map ^-> T<unit>
        //
        "setOptions" => PolygonOptions ^-> T<unit>
        // Sets the first path. See Polyline options for more details.
        "setPath" => MVC.MVCArray LatLng + Type.ArrayOf LatLng ^-> T<unit>
        // Sets the path for this Polygon.
        "setPaths" => (MVC.MVCArray (MVC.MVCArray LatLng) + MVC.MVCArray LatLng
                        + Type.ArrayOf (Type.ArrayOf LatLng) + Type.ArrayOf LatLng) ^-> T<unit>
    ]

let InfoWindowOptions =
    Pattern.Config "InfoWindowOptions" {
        Required = []
        Optional =
            [
                // Content to display in the InfoWindow. This can be an HTML element, a plain-text string, or a string containing HTML. The InfoWindow will be sized according to the content. To set an explicit size for the content, set content to be a HTML element with that size.
                "content", T<string> + Node
                // Disable auto-pan on open. By default, the info window will pan the map so that it is fully visible when it opens.
                "disableAutoPan", T<bool>
                // Maximum width of the infowindow, regardless of content's width. This value is only considered if it is set before a call to open. To change the maximum width when changing content, call close, setOptions, and then open.
                "maxWidth", T<int>
                // The offset, in pixels, of the tip of the info window from the point on the map at whose geographical coordinates the info window is anchored. If an InfoWindow is opened with an anchor, the pixelOffset will be calculated from the top-center of the anchor's bounds.
                "pixelOffset", Size.Type
                // The LatLng at which to display this InfoWindow. If the InfoWindow is opened with an anchor, the anchor's position will be used instead.
                "position", LatLng.Type
                // All InfoWindows are displayed on the map in order of their zIndex, with higher values displaying in front of InfoWindows with lower values. By default, InfoWinodws are displayed according to their latitude, with InfoWindows of lower latitudes appearing in front of InfoWindows at higher latitudes. InfoWindows are always displayed in front of markers.
                "zIndex", T<int>
            ]
    }

// TODO: Events
let InfoWindow =
    Class "google.maps.InfoWindow"
    |=> Inherits MVC.MVCObject
    |+> [Constructor !? InfoWindowOptions]
    |+> Protocol [
        // Closes this InfoWindow by removing it from the DOM structure.
        "close" => T<unit> ^-> T<unit>
        //
        "getContent" => T<unit> ^-> T<string> + Node
        //
        "getPosition" => T<unit> ^-> LatLng
        //
        "getZIndex" => T<unit> ^-> T<int>
        // Opens this InfoWindow on the given map. Optionally, an InfoWindow can be associated with an anchor. In the core API, the only anchor is the Marker class. However, an anchor can be any MVCObject that exposes the position property and optionally pixelBounds for calculating the pixelOffset (see InfoWindowOptions).
        "open" => ((M.Map + StreetView.StreetViewPanorama) * !? MVC.MVCObject) ^-> T<unit>
        //
        "setContent" => (T<string>  + Node) ^-> T<unit>
        //
        "setOptions" => InfoWindowOptions ^-> T<unit>
        //
        "setPosition" => LatLng ^-> T<unit>
        "setZIndex" => T<int> ^-> T<unit>
    ]

let GeocoderRequest =
    Pattern.Config "GeocoderRequest" {
        Required = []
        Optional =
            [
                // Address. Optional.
                "address", T<string>
                // LatLngBounds within which to search. Optional.
                "bounds", LatLngBounds.Type
                // Preferred language for results. Optional.
                "language", T<string>
                // LatLng about which to search. Optional.
                "location", LatLng.Type
                // Country code top-level domain within which to search. Optional.
                "region", T<string>
            ]
    }

let GeocoderStatus =
    Pattern.EnumInlines "GeocoderStatus" [
        // There was a problem contacting the Google servers.
        "ERROR", "google.maps.GeocoderStatus.ERROR"
        // This GeocoderRequest was invalid.
        "INVALID_REQUEST", "google.maps.GeocoderStatus.INVALID_REQUEST"
        // The response contains a valid GeocoderResponse.
        "OK", "google.maps.GeocoderStatus.OK"
        // The webpage has gone over the requests limit in too short a period of time.
        "OVER_QUERY_LIMIT", "google.maps.GeocoderStatus.OVER_QUERY_LIMIT"
        // The webpage is not allowed to use the geocoder.
        "REQUEST_DENIED", "google.maps.GeocoderStatus.REQUEST_DENIED"
        // A geocoding request could not be processed due to a server error. The request may succeed if you try again.
        "UNKNOWN_ERROR", "google.maps.GeocoderStatus.UNKNOWN_ERROR"
        // No result was found for this GeocoderRequest.
        "ZERO_RESULTS", "google.maps.GeocoderStatus.ZERO_RESULTS"
    ]

let GeocoderLocationType =
    Pattern.EnumInlines "GeocoderLocationType" [
        // The returned result is approximate.
        "APPROXIMATE", "google.maps.GeocoderLocationType.APPROXIMATE"
        // The returned result is the geometric center of a result such a line (e.g. street) or polygon (region).
        "GEOMETRIC_CENTER", "google.maps.GeocoderLocationType.GEOMETRIC_CENTER"
        // The returned result reflects an approximation (usually on a road) interpolated between two precise points (such as intersections). Interpolated results are generally returned when rooftop geocodes are unavilable for a street address.
        "RANGE_INTERPOLATED", "google.maps.GeocoderLocationType.RANGE_INTERPOLATED"
        // The returned result reflects a precise geocode.
        "ROOFTOP", "google.maps.GeocoderLocationType.ROOFTOP"
    ]


let GeocoderAddressComponent =
    Class "GeocoderAddressComponent"
    |+> Protocol [
        // The full text of the address component
        "long_name" =@ T<string>
        // The abbreviated, short text of the given address component
        "short_name" =@ T<string>
        // An array of strings denoting the type of this address component
        "types" =@ Type.ArrayOf T<string>
    ]

let GeocoderGeometry =
    Class "GeocoderGeometry"
    |+> Protocol [
        // The precise bounds of this GeocodeResult, if applicable
        "bounds" =@ LatLngBounds
        // The latitude/longitude coordinates of this result
        "location" =@ LatLng
        // The type of location returned in location
        "location_type" =@ GeocoderLocationType
        // The bounds of the recommended viewport for displaying this GeocodeResult
        "viewport" =@ LatLngBounds
    ]

let GeocoderResult =
    Class "GeocoderResult"
    |+> Protocol [
        // An array of GeocoderAddressComponents
        "address_components" =@ Type.ArrayOf GeocoderAddressComponent
        // A GeocoderGeometry object
        "geometry" =@ GeocoderGeometry
        // An array of strings denoting the type of the returned geocoded element. A type consists of a unique string identifying the geocode result. (For example, "administrative_area_level_1", "country", etc.)
        "types" =@ Type.ArrayOf T<string>
    ]

let Geocoder =
    Class "google.maps.Geocoder"
    |=> Inherits MVC.MVCObject
    |+> [Constructor T<unit>]
    |+> Protocol [
        "geocode" => GeocoderRequest * ((Type.ArrayOf GeocoderResult * GeocoderStatus) ^-> T<unit>) ^-> T<unit>
    ]

let DirectionsDistance =
    Class "DirectionsDistance"
    |+> Protocol [
        // A T<string> representation of the distance value, using the DirectionsUnitSystem specified in the request.
        "text" =@ T<string>
        // The distance in meters.
        "value" =@ T<float>
    ]

let DirectionsDuration =
    Class "DirectionsDuration"
    |+> Protocol [
        // A T<string> representation of the duration value.
        "text" =@ T<string>
        // The duration in seconds.
        "value" =@ T<float>
    ]

let DirectionsStep =
    Class "DirectionsStep"
    |+> Protocol [
        // The distance covered by this step. This property may be undefined as the distance may be unknown.
        "distance" =@ DirectionsDistance
        // The typical time required to perform this step in seconds and in text form. This property may be undefined as the duration may be unknown.
        "duration" =@ DirectionsDuration
        // The ending location of this step.
        "end_location" =@ LatLng
        // Instructions for this step.
        "instructions" =@ T<string>
        // A sequence of LatLngs describing the course of this step.
        "path" =@ Type.ArrayOf LatLng
        // The starting location of this step.
        "start_location" =@ LatLng
    ]

let DirectionsLeg =
    Class "DirectionsLeg"
    |+> Protocol [
        // The total distance covered by this leg. This property may be undefined as the distance may be unknown.
        "distance" =@ DirectionsDistance
        // The total duration of this leg. This property may be undefined as the duration may be unknown.
        "duration" =@ DirectionsDuration
        // The address of the destination of this leg.
        "end_address" =@ T<string>
        // The DirectionsService calculates directions between locations by using the nearest transportation option (usually a road) at the start and end locations. end_location indicates the actual geocoded destination, which may be different than the end_location of the last step if, for example, the road is not near the destination of this leg.
        "end_location" =@ LatLng
        // The address of the origin of this leg.
        "start_address" =@ T<string>
        // The DirectionsService calculates directions between locations by using the nearest transportation option (usually a road) at the start and end locations. start_location indicates the actual geocoded origin, which may be different than the start_location of the first step if, for example, the road is not near the origin of this leg.
        "start_location" =@ LatLng
        // An array of DirectionsSteps, each of which contains information about the individual steps in this leg.
        "steps" =@ Type.ArrayOf DirectionsStep
    ]

let DirectionsRoute =
    Class "DirectionsRoute"
    |+> Protocol [
        // The bounds for this route.
        "bounds" =@ LatLngBounds
        // Copyrights text to be displayed for this route.
        "copyrights" =@ T<string>
        // An array of DirectionsLegs, each of which contains information about the steps of which it is composed. There will be one leg for each waypoint or destination specified. So a route with no waypoints will contain one DirectionsLeg and a route with one waypoint will contain two. (This property was formerly known as "routes".)
        "legs" =@ Type.ArrayOf DirectionsLeg
        // An array of LatLngs representing the entire course of this route. The path is simplified in order to make it suitable in contexts where a small number of vertices is required (such as Static Maps API URLs).
        "overview_path" =@ Type.ArrayOf LatLng
        // Warnings to be displayed when showing these directions.
        "warnings" =@ Type.ArrayOf T<string>
        // If optimizeWaypoints was set to true, this field will contain the re-ordered permutation of the input waypoints.
        "waypoint_order" =@ Type.ArrayOf T<int>
    ]

let DirectionsResult =
    Class "DirectionsResult"
    |+> Protocol [
        // An array of DirectionsRoutes, each of which contains information about the legs and steps of which it is composed. There will only be one route unless the DirectionsRequest was made with provideRouteAlternatives set to true. (This property was formerly known as "trips".)
        "routes" =@ Type.ArrayOf DirectionsRoute
    ]

let DirectionsStatus =
    Pattern.EnumInlines "DirectionsStatus" [
        // The DirectionsRequest provided was invalid.
        "INVALID_REQUEST", "google.maps.DirectionsStatus.INVALID_REQUEST"
        // Too many DirectionsWaypoints were provided in the DirectionsRequest. The total allowed waypoints is 8, plus the origin and destination.
        "MAX_WAYPOINTS_EXCEEDED", "google.maps.DirectionsStatus.MAX_WAYPOINTS_EXCEEDED"
        // At least one of the origin, destination, or waypoints could not be geocoded.
        "NOT_FOUND", "google.maps.DirectionsStatus.NOT_FOUND"
        // The response contains a valid DirectionsResult.
        "OK", "google.maps.DirectionsStatus.OK"
        // The webpage has gone over the requests limit in too short a period of time.
        "OVER_QUERY_LIMIT", "google.maps.DirectionsStatus.OVER_QUERY_LIMIT"
        // The webpage is not allowed to use the directions service.
        "REQUEST_DENIED", "google.maps.DirectionsStatus.REQUEST_DENIED"
        // A directions request could not be processed due to a server error. The request may succeed if you try again.
        "UNKNOWN_ERROR", "google.maps.DirectionsStatus.UNKNOWN_ERROR"
        // No route could be found between the origin and destination.
        "ZERO_RESULTS", "google.maps.DirectionsStatus.ZERO_RESULTS"
    ]

let DirectionsWaypoint =
    Pattern.Config "DirectionsWaypoint" {
        Required = []
        Optional =
            [
                // Waypoint location. Can be an address string or LatLng. Optional.
                "location", LatLng + T<string>
                // If true, indicates that this waypoint is a stop between the origin and destination. This has the effect of splitting the route into two. This value is true by default. Optional.
                "stopover", T<bool>
            ]
    }

let DirectionsUnitSystem =
    Pattern.EnumInlines "DirectionsUnitSystem" [
        // Specifies that distances in the DirectionsResult should be expressed in imperial units.
        "IMPERIAL", "google.maps.DirectionsUnitSystem.IMPERIAL"
        // Specifies that distances in the DirectionsResult should be expressed in metric units.
        "METRIC", "google.maps.DirectionsUnitSystem.METRIC"
    ]

let DirectionsTravelMode =
    Pattern.EnumInlines "DirectionsTravelMode" [
        // Specifies a bicycling directions request.
        "BICYCLING", "google.maps.DirectionsTravelMode.BICYCLING"
        // Specifies a driving directions request.
        "DRIVING", "google.maps.DirectionsTravelMode.DRIVING"
        // Specifies a walking directions request.
        "WALKING", "google.maps.DirectionsTravelMode.WALKING"
    ]

let DirectionsRequest =
    Pattern.Config "DirectionsRequest" {
        Optional =
            [
                // If true, instructs the Directions service to avoids highways where possible. Optional.
                "avoidHighways", T<bool>
                // If true, instructs the Directions service to avoids toll roads where possible. Optional.
                "avoidTolls", T<bool>
                // If set to true, the DirectionService will attempt to re-order the supplied intermediate waypoints to minimize overall cost of the route. If waypoints are optimized, inspect DirectionsRoute.waypoint_order in the response to determine the new ordering.
                "optimizeWaypoints", T<bool>
                // Whether or not route alternatives should be provided. Optional.
                "provideRouteAlternatives", T<bool>
                // Region code used as a bias for geocoding requests. Optional.
                "region", T<string>
                // Preferred unit system to use when displaying distance. Defaults to the unit system used in the country of origin.
                "unitSystem", DirectionsUnitSystem.Type
                // Array of intermediate waypoints. Directions will be calculated from the origin to the destination by way of each waypoint in this array. Optional.
                "waypoints", Type.ArrayOf DirectionsWaypoint

            ]
        Required =
            [
                // Location of destination. This can be specified as either a string to be geocoded or a LatLng. Required.
                "destination", LatLng + T<string>
                // Location of origin. This can be specified as either a string to be geocoded or a LatLng. Required.
                "origin", LatLng + T<string>
                // Type of routing requested. Required.
                "travelMode", DirectionsTravelMode.Type
            ]
    }


let DirectionsRendererOptions =
    Pattern.Config "DirectionsRendererOptions" {
        Required = []
        Optional =
            [
                // The directions to display on the map and/or in a <div> panel, retrieved as a DirectionsResult object from DirectionsService.
                "directions", DirectionsResult.Type
                // This property indicates whether the renderer should provide UI to select amongst alternative routes. By default, this flag is false and a user-selectable list of routes will be shown in the directions' associated panel. To hide that list, set hideRouteList to true.
                "hideRouteList", T<bool>
                // Map on which to display the directions.
                "map", M.Map.Type
                // Options for the markers. All markers rendered by the DirectionsRenderer will use these options.
                "markerOptions", MarkerOptions.Type
                // The <div> in which to display the directions steps.
                "panel", Node
                // Options for the polylines. All polylines rendered by the DirectionsRenderer will use these options.
                "polylineOptions", PolylineOptions.Type
                // By default, the input map is centered and zoomed to the bounding box of this set of directions. If this option is set to true, the viewport is left unchanged, unless the map's center and zoom were never set.
                "preserveViewport", T<bool>
                // The index of the route within the DirectionsResult object. The default value is 0.
                "routeIndex", T<int>
                // Suppress the rendering of the BicyclingLayer when bicycling directions are requested.
                "suppressBicyclingLayer", T<bool>
                // Suppress the rendering of info windows.
                "suppressInfoWindows", T<bool>
                // Suppress the rendering of markers.
                "suppressMarkers", T<bool>
                // Suppress the rendering of polylines.
                "suppressPolylines", T<bool>
            ]
    }

let DirectionsRenderer =
    Class "google.maps.DirectionsRenderer"
    |=> Inherits MVC.MVCObject
    |+> [Constructor !? DirectionsRendererOptions]
    |+> Protocol [
        // Returns the renderer's current set of directions.
        "getDirections" =>  T<unit> ^-> DirectionsResult
        // Returns the map on which the DirectionsResult is rendered.
        "getMap" =>  T<unit> ^-> M.Map
        // Returns the panel <div> in which the DirectionsResult is rendered.
        "getPanel" =>  T<unit> ^-> Node
        // Returns the current (zero-based) route index in use by this DirectionsRenderer object.
        "getRouteIndex" =>  T<unit> ^-> T<int>
        // Set the renderer to use the result from the DirectionsService. Setting a valid set of directions in this manner will display the directions on the renderer's designated map and panel.
        "setDirections" =>  DirectionsResult ^-> T<unit>
        // This method specifies the map on which directions will be rendered. Pass null to remove the directions from the map.
        "setMap" => M.Map ^-> T<unit>
        // Change the options settings of this DirectionsRenderer after initialization.
        "setOptions" =>  DirectionsRendererOptions ^-> T<unit>
        // This method renders the directions in a <div>. Pass null to remove the content from the panel.
        "setPanel" =>  Node ^-> T<unit>
        // Set the (zero-based) index of the route in the DirectionsResult object to render. By default, the first route in the array will be rendered.
        "setRouteIndex" =>  T<unit> ^-> T<unit>
    ]

let DirectionsService =
    Class "google.maps.DirectionsService"
    |+> [Constructor T<unit>]
    |+> Protocol [
        "route" => (DirectionsRequest * (DirectionsResult * DirectionsStatus ^-> T<unit>)) ^-> T<unit>
    ]

let PathElevationRequest =
    Pattern.Config "PathElevationRequest" {
        Optional = []
        Required =
            [
                // The path along which to collect elevation values.
                "path", Type.ArrayOf LatLng
                // Required. The number of equidistant points along the given path for which to retrieve elevation data, including the endpoints. The number of samples must be a value between 2 and 1024.
                "samples", T<int>
            ]
    }

let LocationElevationRequest =
    Pattern.Config "LocationElevationRequest" {
        Optional = []
        Required =
            [
                //The discrete locations for which to retrieve elevations.
                "locations", Type.ArrayOf LatLng
            ]
    }

let ElevationResult =
    Class "ElevationResult"
    |+> Protocol [
        // The elevation of this point on Earth, in meters above sea level.
        "elevation" =@ T<float>
        // The location of this elevation result.
        "location" =@ LatLng
    ]

let ElevationStatus =
    Pattern.EnumInlines "ElevationStatus" [
        // This request was invalid.
        "INVALID_REQUEST", "google.maps.ElevationStatus.INVALID_REQUEST"
        // The request did not encounter any errors.
        "OK", "google.maps.ElevationStatus.OK"
        // The webpage has gone over the requests limit in too short a period of time.
        "OVER_QUERY_LIMIT", "google.maps.ElevationStatus.OVER_QUERY_LIMIT"
        // The webpage is not allowed to use the elevation service for some reason.
        "REQUEST_DENIED", "google.maps.ElevationStatus.REQUEST_DENIED"
        // A geocoding, directions or elevation request could not be successfully processed, yet the exact reason for the failure is not known.
        "UNKNOWN_ERROR", "google.maps.ElevationStatus.UNKNOWN_ERROR"
    ]

let ElevationService =
    Class "google.maps.ElevationService"
    |+> [Constructor T<unit>]
    |+> Protocol [
        // Makes an elevation request along a path, where the elevation data are returned as distance-based samples along that path.
        "getElevationAlongPath" => (PathElevationRequest * (Type.ArrayOf ElevationResult ^-> ElevationStatus)) ^-> T<unit>
        // Makes an elevation request for a list of discrete locations.
        "getElevationForLocations" => (LocationElevationRequest * (Type.ArrayOf ElevationResult ^-> ElevationStatus)) ^-> T<unit>
    ]

// TODO
let OverlayView = Class "OverlayView"

let MapsEventListener = Class "MapsEventListener"

// TODO
let Event =
    Class "google.maps.event"
    |+> [
        // Cross browser event handler registration. This listener is removed by calling removeListener(handle) for the handle that is returned by this function.
        "addDomListener" => (T<obj> * T<string> * (T<obj> ^-> T<unit>)) ^-> MapsEventListener
        "addDomListener" => (T<obj> * T<string> * (T<obj> ^-> T<unit>) * T<bool>) ^-> MapsEventListener
        // Wrapper around addDomListener that removes the listener after the first event.
        "addDomListenerOnce" => (T<obj> * T<string> * (T<obj> ^-> T<unit>)) ^-> MapsEventListener
        "addDomListenerOnce" => (T<obj> * T<string> * (T<obj> ^-> T<unit>) * T<bool>) ^-> MapsEventListener
        // Adds the given listener function to the given event name for the given object instance. Returns an identifier for this listener that can be used with removeListener().
        "addListener" => (T<obj> * T<string> * (T<obj> ^-> T<unit>)) ^-> MapsEventListener
        // Like addListener, but the handler removes itself after handling the first event.
        "addListenerOnce" => (T<obj> * T<string> * (T<obj> ^-> T<unit>)) ^-> MapsEventListener
        // Removes all listeners for all events for the given instance.
        "clearInstanceListeners" => (T<obj>) ^-> T<unit>
        // Removes all listeners for the given event for the given instance.
        "clearListeners" => (T<obj> * T<string>) ^-> T<unit>
        // Removes the given listener, which should have been returned by addListener above.
        "removeListener" => (MapsEventListener) ^-> T<unit>
        // Triggers the given event. All arguments after eventName are passed as arguments to the listeners.
        // FIXME: "trigger" => (T<obj> * T<string> * (!* T<obj>)) ^-> T<unit>
    ]

