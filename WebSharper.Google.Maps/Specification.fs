/// The rest of the spec.
/// TODO: this code needs revision to update to the latest 3.11 API
module WebSharper.Google.Maps.Specification

open WebSharper
open WebSharper.InterfaceGenerator
open WebSharper.Google.Maps.Base
open WebSharper.Google.Maps.Notation
module M = WebSharper.Google.Maps.Map

let Animation =
    Class "google.maps.Animation"
    |+> Static [
        "BOUNCE" =? TSelf
        |> WithComment "Marker bounces until animation is stopped."

        "DROP" =? TSelf
        |> WithComment "Marker falls from the top of the map ending with a small bounce."
        ]

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
    Config "google.maps.MapPanes"
    |+> Instance [
        "floatPane" =@ Node
        |> WithComment "This pane contains the info window. It is above all map overlays. (Pane 6)."

        "floatShadow" =@ Node
        |> WithComment "This pane contains the info window shadow. It is above the overlayImage, so that markers can be in the shadow of the info window. (Pane 4)."

        "mapPane" =@ Node
        |> WithComment "This pane is the lowest pane and is above the tiles. It may not receive DOM events. (Pane 0)."

        "overlayImage" =@ Node
        |> WithComment "This pane contains the marker foreground images. (Pane 3)."

        "overlayLayer" =@ Node
        |> WithComment "This pane contains polylines, polygons, ground overlays and tile layer overlays. It may not receive DOM events. (Pane 1)."

        "overlayMouseTarget" =@ Node
        |> WithComment "This pane contains elements that receive DOM mouse events, such as the transparent targets for markers. It is above the floatShadow, so that markers in the shadow of the info window can be clickable. (Pane 5)."

        "overlayShadow" =@ Node
        |> WithComment "This pane contains the marker shadows. It may not receive DOM events. (Pane 2)."
    ]


let MapCanvasProjection =
    Class "google.maps.MapCanvasProjection"
    |+> Instance [
        "fromContainerPixelToLatLng" => Point ^-> LatLng
        |> WithComment "Computes the geographical coordinates from pixel coordinates in the map's container."

        "fromDivPixelToLatLng" => Point ^-> LatLng
        |> WithComment "Computes the geographical coordinates from pixel coordinates in the div that holds the draggable map."

        "fromLatLngToContainerPixel" => LatLng ^-> Point
        |> WithComment "Computes the pixel coordinates of the given geographical location in the map's container element."

        "fromLatLngToDivPixel" => LatLng ^-> Point
        |> WithComment "Computes the pixel coordinates of the given geographical location in the DOM element that holds the draggable map."

        "getWorldWidth" => T<unit> ^-> T<int>
        |> WithComment "The width of the world in pixels in the current zoom level. For projections with a heading angle of either 90 or 270 degrees, this corresponds to the pixel span in the Y-axis."
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
        |+> Static [Constructor (Type.ArrayOf MapTypes.MapTypeStyle * !? StyledMapTypeOptions)]
        |=> Inherits MVC.MVCObject
    (StyledMapType, StyledMapTypeOptions)

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
    Config "RectangleOptions"
    |+> Instance [
        "bounds" =@ LatLngBounds
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
        |> WithComment "The stroke position. Defaults to CENTER. This property is not supported on Internet Explorer 8 and earlier."

        "strokeWeight" =@ T<int>
        |> WithComment "The stroke width in pixels."

        "visible" =@ T<bool>
        |> WithComment "Whether this rectangle is visible on the map. Defaults to true."

        "zIndex" =@ T<int>
        |> WithComment "The zIndex compared to other polys."
    ]

let CircleOptions =
    Config "CircleOptions"
    |+> Instance [
        "center" =@ LatLng
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
        |> WithComment "The stroke position. Defaults to CENTER. This property is not supported on Internet Explorer 8 and earlier."

        "strokeWeight" =@ T<int>
        |> WithComment "The stroke width in pixels."

        "visible" =@ T<bool>
        |> WithComment "Whether this circle is visible on the map. Defaults to true."

        "zIndex" =@ T<int>
        |> WithComment "The zIndex compared to other polys."
    ]

let Circle =
    Class "google.maps.Circle"
    |+> Static [
        Constructor CircleOptions
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

        "setCenter" => LatLng ^-> T<unit>
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
    ]

let Rectangle =
    Class "google.maps.Rectangle"
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

        "setBounds" => LatLngBounds ^-> T<unit>
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
    ]

let GroundOverlayOptions =
    Config "GroundOverlayOptions"
    |+> Instance [
        "clickable" =@ T<bool>
        |> WithComment "If true, the ground overlay can receive mouse events."

        "map" =@ M.Map.Type
        |> WithComment "The map on which to display the overlay."

        "opacity" =@ T<float>
        |> WithComment "The opacity of the overlay, expressed as a number between 0 and 1. Optional. Defaults to 1."
    ]

let GroundOverlay =
    Class "google.maps.GroundOverlay"
    |+> Static [
        Ctor [
            T<string>?Url
            LatLngBounds?Bounds
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
    Config "KmlLayerOptions"
    |+> Instance [
        "clickable" =@ T<bool>
        |> WithComment "If true, the layer receives mouse events. Default value is true."

        "map" =@ M.Map.Type
        |> WithComment "The map on which to display the layer."

        "preserveViewport" =@ T<bool>
        |> WithComment "By default, the input map is centered and zoomed to the bounding box of the contents of the layer. If this option is set to true, the viewport is left unchanged, unless the map's center and zoom were never set."

        "screenOverlays" =@ T<bool>
        |> WithComment "Whether to render the screen overlays. Default true."

        "suppressInfoWindows" =@ T<bool>
        |> WithComment "Suppress the rendering of info windows when layer features are clicked."

        "url" =@ T<string>
        |> WithComment "The URL of the KML document to display."
    ]

let KmlLayerStatus =
    Class "google.maps.KmlLayerStatus"
    |+> Static [
        "DOCUMENT_NOT_FOUND" =? TSelf
        |> WithComment "The document could not be found. Most likely it is an invalid URL, or the document is not publicly available."

        "DOCUMENT_TOO_LARGA" =? TSelf
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

        "setMap" => (M.Map) ^-> T<unit>
        |> WithComment "Renders the KML Layer on the specified map. If map is set to null, the layer is removed."

        "setUrl" => T<string -> unit>
        |> WithComment "Set the URL of the KML file to display."
    ]

let TrafficLayer =
    Class "google.maps.TrafficLayer"
    |+> Static [Constructor T<unit>]
    |=> Inherits MVC.MVCObject
    |+> Instance [
        "getMap" => T<unit> ^-> M.Map
        |> WithComment "Returns the map on which this layer is displayed."

        "setMap" => M.Map ^-> T<unit>
        |> WithComment "Renders the layer on the specified map. If map is set to null, the layer will be removed."
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
    Config "Icon"
    |=> Implements [MarkerImage]
    |+> Instance [
        "anchor" =@ Point
        |> WithComment "The position at which to anchor an image in correspondance to the location of the marker on the map. By default, the anchor is located along the center point of the bottom of the image."

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

        "Custom" => T<string>?path ^-> TSelf
        |> WithInline "$path"
    ]

let Symbol =
    Class "google.maps.Symbol"
    |=> Implements [MarkerImage]
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

        "crossOnDrag" =@ T<bool>
        |> WithComment "If false, disables cross that appears beneath the marker when dragging. This option is true by default. This option is only enabled when google.maps.visualRefresh is set to true. For backwards compatibility, if raiseOnDrag is set to false then the default for crossOnDrag changes to false."

        "cursor" =@ T<string>
        |> WithComment "Mouse cursor to show on hover"

        "draggable" =@ T<bool>
        |> WithComment "If true, the marker can be dragged. Default value is false."

        "flat" =@ T<bool>
        |> WithComment "If true, the marker shadow will not be displayed."

        "icon" =@ MarkerImage
        |> WithComment "Icon for the foreground. If a string is provided, it is treated as though it were an Icon with the string as url."

        "map" =@ M.Map + StreetView.StreetViewPanorama
        |> WithComment "Map on which to display Marker."

        "optimized" =@ T<bool>
        |> WithComment "Optimization renders many markers as a single static element. Optimized rendering is enabled by default. Disable optimized rendering for animated GIFs or PNGs, or when each marker must be rendered as a separate DOM element (advanced usage only)."

        "position" =@ LatLng
        |> WithComment "Marker position. Required."

        "raiseOnDrag" =@ T<bool>
        |> WithComment "If false, disables raising and lowering the marker on drag. This option is true by default. This option is disabled when google.maps.visualRefresh is set to true. Instead, a cross will appear beneath the marker icon while dragging. Please refer to the crossOnDrag property for new code. For backwards compatibility, if this is set to false then the default for crossOnDrag changes to false."

        "shadow" =@ MarkerImage
        |> WithComment "Shadow image. If a string is provided, it is treated as though it were an Icon with the string as url. Shadows are not rendered when google.maps.visualRefresh is set to true."

        "shape" =@ MarkerShape
        |> WithComment "Image map region definition used for drag/click."

        "title" =@ T<string>
        |> WithComment "Rollover text."

        "visible" =@ T<bool>
        |> WithComment "If true, the marker is visible."

        "zIndex" =@ T<int>
        |> WithComment "All markers are displayed on the map in order of their zIndex, with higher values displaying in front of markers with lower values. By default, markers are displayed according to their vertical position on screen, with lower markers appearing in front of markers further up the screen."
    ]

// TODO: Events
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

        "getClickable" => T<unit> ^-> T<bool>

        "getCursor" => T<unit> ^-> T<string>

        "getDraggable" => T<unit> ^-> T<bool>

        "getFlat" => T<unit> ^-> T<bool>

        "getIcon" => T<unit> ^-> MarkerImage

        "getMap" => T<unit> ^-> M.Map + StreetView.StreetViewPanorama

        "getPosition" => T<unit> ^-> LatLng

        "getShadow" => T<unit> ^-> MarkerImage

        "getShape" => T<unit> ^-> MarkerShape

        "getTitle" => T<unit> ^-> T<string>

        "getVisible" => T<unit> ^-> T<bool>

        "getZIndex" => T<unit> ^-> T<int>

        "setAnimation" => Animation ^-> T<unit>
        |> WithComment "Start an animation. Any ongoing animation will be cancelled. Currently supported animations are: BOUNCE, DROP. Passing in null will cause any animation to stop."

        "setClickable" => T<bool> ^-> T<unit>

        "setCursor" => T<string> ^-> T<unit>

        "setDraggable" => T<bool> ^-> T<unit>

        "setFlat" => T<bool> ^-> T<unit>

        "setIcon" => MarkerImage ^-> T<unit>

        "setMap" => (M.Map + StreetView.StreetViewPanorama) ^-> T<unit>
        |> WithComment "Renders the marker on the specified map or panorama. If map is set to null, the marker will be removed."

        "setOptions" => MarkerOptions ^-> T<unit>

        "setPosition" => LatLng ^-> T<unit>

        "setShadow" => MarkerImage ^-> T<unit>

        "setShape" => MarkerShape ^-> T<unit>

        "setTitle" => T<string> ^-> T<unit>

        "setVisible" => T<bool> ^-> T<unit>

        "setZIndex" => T<int> ^-> T<unit>
    ]

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

let PolylineOptions =
    Config "PolylineOptions"
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

        "path" =@ Type.ArrayOf LatLng
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

// TODO: Events
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

        "setPath" => (MVC.MVCArray.[LatLng] + Type.ArrayOf LatLng) ^-> T<unit>
        |> WithComment "Sets the first path. See PolylineOptions for more details."

        "setVisible" => T<bool -> unit>
        |> WithComment "Hides this poly if set to false."
    ]

let PolygonOptions =
    Config "PolygonOptions"
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
                    + Type.ArrayOf LatLng + Type.ArrayOf (Type.ArrayOf LatLng))
        |> WithComment "The ordered sequence of coordinates that designates a closed loop. Unlike polylines, a polygon may consist of one or more paths. As a result, the paths property may specify one or more arrays of LatLng coordinates. Paths are closed automatically; do not repeat the first vertex of the path as the last vertex. Simple polygons may be defined using a single array of LatLngs. More complex polygons may specify an array of arrays. Any simple arrays are converted into MVCArrays. Inserting or removing LatLngs from the MVCArray will automatically update the polygon on the map."

        "strokeColor" =@ T<string>
        |> WithComment "The stroke color. All CSS3 colors are supported except for extended named colors."

        "strokeOpacity" =@ T<float>
        |> WithComment "The stroke opacity between 0.0 and 1.0"

        "strokePosition" => StrokePosition
        |> WithComment "The stroke position. Defaults to CENTER. This property is not supported on Internet Explorer 8 and earlier."

        "strokeWeight" =@ T<int>
        |> WithComment "The stroke width in pixels."

        "visible" =@ T<bool>
        |> WithComment "Whether this polygon is visible on the map. Defaults to true."

        "zIndex" =@ T<int>
        |> WithComment "The zIndex compared to other polys."
    ]

// TODO: Events
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

        "setPath" => MVC.MVCArray.[LatLng] + Type.ArrayOf LatLng ^-> T<unit>
        |> WithComment "Sets the first path. See PolylineOptions for more details."

        "setPaths" => (MVC.MVCArray.[MVC.MVCArray.[LatLng]] + MVC.MVCArray.[LatLng]
                        + Type.ArrayOf (Type.ArrayOf LatLng) + Type.ArrayOf LatLng) ^-> T<unit>
        |> WithComment "Sets the path for this polygon."

        "setVisible" => T<bool -> unit>
        |> WithComment "Hides this poly if set to false."
    ]

let InfoWindowOptions =
    Config "InfoWindowOptions"
    |+> Instance [
                "content" =@ (T<string> + Node)
                |> WithComment "Content to display in the InfoWindow. This can be an HTML element, a plain-text string, or a string containing HTML. The InfoWindow will be sized according to the content. To set an explicit size for the content, set content to be a HTML element with that size."

                "disableAutoPan" =@ T<bool>
                |> WithComment "Disable auto-pan on open. By default, the info window will pan the map so that it is fully visible when it opens."

                "maxWidth" =@ T<int>
                |> WithComment "Maximum width of the infowindow, regardless of content's width. This value is only considered if it is set before a call to open. To change the maximum width when changing content, call close, setOptions, and then open."

                "pixelOffset" =@ Size
                |> WithComment "The offset, in pixels, of the tip of the info window from the point on the map at whose geographical coordinates the info window is anchored. If an InfoWindow is opened with an anchor, the pixelOffset will be calculated from the anchor's anchorPoint property."

                "position" =@ LatLng
                |> WithComment "The LatLng at which to display this InfoWindow. If the InfoWindow is opened with an anchor, the anchor's position will be used instead."

                "zIndex" =@ T<int>
                |> WithComment "All InfoWindows are displayed on the map in order of their zIndex, with higher values displaying in front of InfoWindows with lower values. By default, InfoWindows are displayed according to their latitude, with InfoWindows of lower latitudes appearing in front of InfoWindows at higher latitudes. InfoWindows are always displayed in front of markers."
            ]

// TODO: Events
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

        "getContent" => T<unit> ^-> T<string> + Node

        "getPosition" => T<unit> ^-> LatLng

        "getZIndex" => T<unit> ^-> T<int>

        "open" => ((M.Map + StreetView.StreetViewPanorama) * !? MVC.MVCObject) ^-> T<unit>
        |> WithComment "Opens this InfoWindow on the given map. Optionally, an InfoWindow can be associated with an anchor. In the core API, the only anchor is the Marker class. However, an anchor can be any MVCObject that exposes a LatLng position property and optionally a Point anchorPoint property for calculating the pixelOffset (see InfoWindowOptions). The anchorPoint is the offset from the anchor's position to the tip of the InfoWindow."

        "setContent" => (T<string>  + Node) ^-> T<unit>

        "setOptions" => InfoWindowOptions ^-> T<unit>

        "setPosition" => LatLng ^-> T<unit>

        "setZIndex" => T<int> ^-> T<unit>
    ]

let GeocoderRequest =
    Config "GeocoderRequest"
    |+> Instance [
        "address" =@ T<string>
        |> WithComment "Address. Optional."

        "bounds" =@ LatLngBounds.Type
        |> WithComment "LatLngBounds within which to search. Optional."

        "location" =@ LatLng.Type
        |> WithComment "LatLng about which to search. Optional."

        "region" =@ T<string>
        |> WithComment "Country code used to bias the search, specified as a Unicode region subtag / CLDR identifier. Optional."
    ]

let GeocoderComponentRestrictions =
    Config "GeocoderComponentRestrictions"
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
        |> WithComment "An array of strings denoting the type of this address component. A list of valid types can be found at https://developers.google.com/maps/documentation/geocoding/#Types"
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

        "partial_match" =@ T<bool>
        |> WithComment "Whether the geocoder did not return an exact match for the original request, though it was able to match part of the requested address."

        "postcode_localities" =@ T<string[]>
        |> WithComment "An array of strings denoting all the localities contained in a postal code. This is only present when the result is a postal code that contains multiple localities."

        "types" =@ T<string[]>
        |> WithComment "An array of strings denoting the type of the returned geocoded element. For a list of possible strings, refer to https://developers.google.com/maps/documentation/javascript/geocoding#GeocodingAddressTypes"
    ]

let Geocoder =
    Class "google.maps.Geocoder"
    |=> Inherits MVC.MVCObject
    |+> Static [Constructor T<unit>]
    |+> Instance [
        "geocode" => GeocoderRequest * ((Type.ArrayOf GeocoderResult * GeocoderStatus) ^-> T<unit>) ^-> T<unit>
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

        "value" =? T<JavaScript.Date>
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

let DirectionsStep =
    Class "google.maps.DirectionsStep"
    |+> Instance [
        "distance" =? Distance
        |> WithComment "The distance covered by this step. This property may be undefined as the distance may be unknown."

        "duration" =? Duration
        |> WithComment "The typical time required to perform this step in seconds and in text form. This property may be undefined as the duration may be unknown."

        "end_location" =? LatLng
        |> WithComment "The ending location of this step."

        "instructions" =? T<string>
        |> WithComment "Instructions for this step."

        "path" =? Type.ArrayOf LatLng
        |> WithComment "A sequence of LatLngs describing the course of this step."

        "start_location" =? LatLng
        |> WithComment "The starting location of this step."

        "steps" =? TSelf
        |> WithComment "Sub-steps of this step. Specified for non-transit sections of transit routes."

        "transit" =? TransitDetails
        |> WithComment "Transit-specific details about this step. This property will be undefined unless the travel mode of this step is TRANSIT."

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
        |> WithComment "The address of the destination of this leg."

        "end_location" =? LatLng
        |> WithComment "The DirectionsService calculates directions between locations by using the nearest transportation option (usually a road) at the start and end locations. end_location indicates the actual geocoded destination, which may be different than the end_location of the last step if, for example, the road is not near the destination of this leg."

        "start_address" =? T<string>
        |> WithComment "The address of the origin of this leg."

        "start_location" =? LatLng
        |> WithComment "The DirectionsService calculates directions between locations by using the nearest transportation option (usually a road) at the start and end locations. start_location indicates the actual geocoded origin, which may be different than the start_location of the first step if, for example, the road is not near the origin of this leg."

        "steps" =? Type.ArrayOf DirectionsStep
        |> WithComment "An array of DirectionsSteps, each of which contains information about the individual steps in this leg."

        "via_waypoints" =? Type.ArrayOf LatLng
        |> WithComment "An array of waypoints along this leg that were not specified in the original request, either as a result of a user dragging the polyline or selecting an alternate route."
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

        "warnings" =? Type.ArrayOf T<string>
        |> WithComment "Warnings to be displayed when showing these directions."

        "waypoint_order" =? Type.ArrayOf T<int>
        |> WithComment "If optimizeWaypoints was set to true, this field will contain the re-ordered permutation of the input waypoints. For example, if the input was: 'Origin: Los Angeles; Waypoints: Dallas, Bangor, Phoenix; Destination: New York' and the optimized output was ordered as follows: 'Origin: Los Angeles; Waypoints: Phoenix, Dallas, Bangor; Destination: New York' then this field will be an Array containing the values [2, 0, 1]. Note that the numbering of waypoints is zero-based. If any of the input waypoints has stopover set to false, this field will be empty, since route optimization is not available for such queries."
    ]

let DirectionsResult =
    Class "google.maps.DirectionsResult"
    |+> Instance [
        "routes" =? Type.ArrayOf DirectionsRoute
        |> WithComment "An array of DirectionsRoutes, each of which contains information about the legs and steps of which it is composed. There will only be one route unless the DirectionsRequest was made with provideRouteAlternatives set to true."
    ]

let DirectionsStatus =
    Class "google.maps.DirectionsStatus"
    |+> Static [
        "INVALID_REQUEST" =? TSelf
        |> WithComment "The DirectionsRequest provided was invalid."

        "MAX_WAYPOINTS_EXCEEDED" =? TSelf
        |> WithComment "Too many DirectionsWaypoints were provided in the DirectionsRequest. The total allowed waypoints is 8, plus the origin and destination. Maps API for Business customers are allowed 23 waypoints, plus the origin, and destination."

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
        "location" =@ Location
        |> WithComment "Waypoint location. Can be an address string or LatLng. Optional."

        "stopover" =@ T<bool>
        |> WithComment "If true, indicates that this waypoint is a stop between the origin and destination. This has the effect of splitting the route into two. This value is true by default. Optional."
    ]

let UnitSystem =
    Class "google.maps.UnitSystem"
    |+> Static [
        "IMPERIAL" =? TSelf
        |> WithComment "Specifies that distances in the DirectionsResult should be expressed in imperial units."

        "METRIC" =? TSelf
        |> WithComment "Specifies that distances in the DirectionsResult should be expressed in metric units."
    ]

let TransitOptions =
    Config "TransitOptions"
    |+> Instance [
        "arrivalTime" =@ T<JavaScript.Date>
        |> WithComment "The desired arrival time for the route, specified as a Date object. The Date object measures time in milliseconds since 1 January 1970. If arrival time is specified, departure time is ignored."

        "departureTime" =@ T<JavaScript.Date>
        |> WithComment "The desired departure time for the route, specified as a Date object. The Date object measures time in milliseconds since 1 January 1970. If neither departure time nor arrival time is specified, the time is assumed to be \"now\"."
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
        "avoidHighways" =@ T<bool>
        |> WithComment "If true, instructs the Directions service to avoid highways where possible. Optional."

        "avoidTolls" =@ T<bool>
        |> WithComment "If true, instructs the Directions service to avoid toll roads where possible. Optional."

        "destination" =@ Location
        |> WithComment "Location of destination. This can be specified as either a string to be geocoded or a LatLng. Required."

        "durationInTraffic" =@ T<bool>
        |> WithComment "Whether or not we should provide trip duration based on current traffic conditions. Only available to Maps API for Business customers."

        "optimizeWaypoints" =@ T<bool>
        |> WithComment "If set to true, the DirectionService will attempt to re-order the supplied intermediate waypoints to minimize overall cost of the route. If waypoints are optimized, inspect DirectionsRoute.waypoint_order in the response to determine the new ordering."

        "origin" =@ Location
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


let DirectionsRendererOptions =
    Config "DirectionsRendererOptions"
    |+> Instance [
        "directions" =@ DirectionsResult
        |> WithComment "The directions to display on the map and/or in a <div> panel, retrieved as a DirectionsResult object from DirectionsService."

        "draggable" =@ T<bool>
        |> WithComment "If true, allows the user to drag and modify the paths of routes rendered by this DirectionsRenderer."

        "hideRouteList" =@ T<bool>
        |> WithComment "This property indicates whether the renderer should provide UI to select amongst alternative routes. By default, this flag is false and a user-selectable list of routes will be shown in the directions' associated panel. To hide that list, set hideRouteList to true."

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
        |> WithComment "By default, the input map is centered and zoomed to the bounding box of this set of directions. If this option is set to true, the viewport is left unchanged, unless the map's center and zoom were never set."

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
    ]

let DirectionsService =
    Class "google.maps.DirectionsService"
    |+> Static [Constructor T<unit>]
    |+> Instance [
        "route" => (DirectionsRequest * (DirectionsResult * DirectionsStatus ^-> T<unit>)) ^-> T<unit>
        |> WithComment "Issue a directions search request."
    ]

let PathElevationRequest =
    Config "PathElevationRequest"
    |+> Static [
        Constructor T<int>?Samples
        |> WithInline "{samples:$Samples}"
    ]
    |+> Instance [
        "path" =@ Type.ArrayOf LatLng
        |> WithComment "The path along which to collect elevation values."

        "samples" =@ T<int>
        |> WithComment "Required. The number of equidistant points along the given path for which to retrieve elevation data, including the endpoints. The number of samples must be a value between 2 and 512 inclusive."
    ]

let LocationElevationRequest =
    Config "LocationElevationRequest"
    |+> Instance [
        "locations" =@ Type.ArrayOf LatLng
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
        |> WithComment "The webpage is not allowed to use the elevation service for some reason."

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
        "getElevationAlongPath" => (PathElevationRequest * (Type.ArrayOf ElevationResult ^-> ElevationStatus)) ^-> T<unit>
        |> WithComment "Makes an elevation request along a path, where the elevation data are returned as distance-based samples along that path."

        "getElevationForLocations" => (LocationElevationRequest * (Type.ArrayOf ElevationResult ^-> ElevationStatus)) ^-> T<unit>
        |> WithComment "Makes an elevation request for a list of discrete locations."
    ]

let MaxZoomStatus =
    Class "google.maps.MaxZoomStatus"
    |+> Static [
        "ERROR" =? TSelf
        |> WithComment "There was a problem contacting the Google servers."

        "OK" =? TSelf
        |> WithComment "The response contains a valid MaxZoomResult."
    ]

let MaxZoomResult =
    Class "google.maps.MaxZoomResult"
    |+> Instance [
        "status" =? MaxZoomStatus
        |> WithComment "Status of the request."

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
        "getMaxZoomAtLatLng" => LatLng * (MaxZoomResult ^-> T<unit>) ^-> T<unit>
        |> WithComment "Returns the maximum zoom level available at a particular LatLng for the Satellite map type. As this request is asynchronous, you must pass a callback function which will be executed upon completion of the request, being passed a MaxZoomResult."
    ]

let DistanceMatrixRequest =
    Class "google.maps.DistanceMatrixRequest"
    |+> Static [
        Ctor [
            (Type.ArrayOf Location)?Origins
            (Type.ArrayOf Location)?Destinations
            TravelMode?TravelMode
        ]
        |> WithInline "{origins:$Origins, destinations:$Destinations, travelMode:$TravelMode}"
    ]
    |+> Instance [
        "avoidHighways" =@ T<bool>
        |> WithComment "If true, instructs the Distance Matrix service to avoid highways where possible. Optional."

        "avoidTolls" =@ T<bool>
        |> WithComment "If true, instructs the Distance Matrix service to avoid toll roads where possible. Optional."

        "destinations" =@ Type.ArrayOf Location
        |> WithComment "An array containing destination address strings and/or LatLngs, to which to calculate distance and time. Required."

        "durationInTraffic" =@ T<bool>
        |> WithComment "Whether or not we should provide trip durations based on current traffic conditions. Only available to Maps API for Business customers."

        "origins" =@ Type.ArrayOf Location
        |> WithComment "An array containing origin address strings and/or LatLngs, from which to calculate distance and time. Required."

        "region" =@ T<string>
        |> WithComment "Region code used as a bias for geocoding requests. Optional."

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
        |> WithComment "Too many elements have been requested within the allowed time period. The request should succeed if you try again after a reasonable amount of time."

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
    |+> Static [Constructor T<unit>]
    |+> Instance [
        "draw" => T<unit -> unit>
        |> WithComment "Implement this method to draw or update the overlay. This method is called after onAdd() and when the position from projection.fromLatLngToPixel() would return a new value for a given LatLng. This can happen on change of zoom, center, or map type. It is not necessarily called on drag or resize."

        "getMap" => M.Map

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

let MapsEventListener = Events.MapsEventListener

// TODO
let Event =
    Class "google.maps.event"
    |+> Static [
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

