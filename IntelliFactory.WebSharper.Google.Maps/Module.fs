namespace IntelliFactory.WebSharper.Google

module MapsSpecification =
    open IntelliFactory.WebSharper.InterfaceGenerator

    let Node = T<IntelliFactory.WebSharper.Dom.Node>
    let Document = T<IntelliFactory.WebSharper.Dom.Document>
    let Element = T<IntelliFactory.WebSharper.Dom.Element>

        
    let ControlPosition = 
        Pattern.EnumInlines "ControlPosition" [
            // Elements are positioned in the center of the bottom row.
            "BOTTOM", "google.maps.ControlPosition.BOTTOM"
            // Elements are positioned in the bottom left and flow towards the middle. Elements are positioned to the right of the Google logo.
            "BOTTOM_LEFT", "google.maps.ControlPosition.BOTTOM_LEFT"
            // Elements are positioned in the bottom right and flow towards the middle. Elements are positioned to the left of the copyrights.
            "BOTTOM_RIGHT", "google.maps.google.maps.ControlPosition.BOTTOM_RIGHT"
            // Elements are positioned on the left, below top-left elements, and flow downwards.
            "LEFT", "google.maps.ControlPosition.LEFT"
            // Elements are positioned on the right, below top-right elements, and flow downwards.
            "RIGHT", "google.maps.ControlPosition.RIGHT"
            // Elements are positioned in the center of the top row.
            "TOP", "google.maps.ControlPosition.TOP"
            // Elements are positioned in the top left and flow towards the middle.
            "TOP_LEFT", "google.maps.ControlPosition.TOP_LEFT"
            // Elements are positioned in the top right and flow towards the middle.
            "TOP_RIGHT", "google.maps.ControlPosition.TOP_RIGHT"
        ]

    let MapTypeControlStyle =
        Pattern.EnumInlines "MapTypeControlStyle" [
            // Uses the default map type control. The control which DEFAULT maps to will vary according to window size and other factors. It may change in future versions of the API.
            "DEFAULT", "google.maps.MapTypeControlStyle.DEFAULT"
            // A dropdown menu for the screen realestate conscious.
            "DROPDOWN_MENU", "google.maps.MapTypeControlStyle.DROPDOWN_MENU"
            // The standard horizontal radio buttons bar.
            "HORIZONTAL_BAR", "google.maps.MapTypeControlStyle.HORIZONTAL_BAR"
        ]

    let MapTypeId =
        Pattern.EnumInlines "MapTypeId" [
            // This map type displays a transparent layer of major streets on satellite images.
            "HYBRID", "google.maps.MapTypeId.HYBRID"
            // This map type displays a normal street map.
            "ROADMAP", "google.maps.MapTypeId.ROADMAP"
            // This map type displays satellite images.
            "SATELLITE", "google.maps.MapTypeId.SATELLITE"
            // This map type displays maps with physical features such as terrain and vegetation.
            "TERRAIN", "google.maps.MapTypeId.TERRAIN"
        ]
    
    let MapTypeControlOptions = 
        Pattern.Config "MapTypeControlOptions" {
            Required = []
            Optional = 
                [
                // IDs of map types to show in the control.
                "mapTypeIds", Type.ArrayOf MapTypeId
                // Position id. Used to specify the position of the control on the map. The default position is TOP_RIGHT.
                "position", ControlPosition.Type
                // Style id. Used to select what style of map type control to display.
                "style", MapTypeControlStyle.Type
                ]
        }

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
                    "position", ControlPosition.Type
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
                    "position", ControlPosition.Type
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

    let LatLng =
        let LatLng = Class "google.maps.LatLng" 
        LatLng
        |+> [Constructor (T<float> * T<float>)
             Constructor (T<float> * T<float> * T<bool>)
            ]
        |+> Protocol [
            // Comparison function.
            "equals" => (LatLng) ^-> T<bool>
            // Returns the latitude in degrees.
            "lat" => T<unit> ^-> T<float>
            // Returns the longitude in degrees.
            "lng" => T<unit> ^-> T<float>
            // Converts to T<string> representation.
            "toString" => T<unit> ^-> T<string>
            // Returns a T<string> of the form "lat,lng" for this LatLng. We round the lat/lng values to 6 decimal places by default.
            "toUrlValue" => (!? T<int>) ^-> T<string>
        ]

    let Point =
        let Point = Class "google.maps.Point"
        Point
        |+> [Constructor <| T<float> * T<float>]
        |+> Protocol [
            // Compares two Points
            "equals" => (Point) ^-> T<bool>
            // Returns a T<string> representation of this Point.
            "toString" => T<unit> ^-> T<string>
            // The X coordinate
            "x" =@ T<float>
            // The Y coordinate
            "y" =@ T<float>        
        ]

    let Size =
        let Size = Class "google.maps.Size"
        Size
        |+> [Constructor <| T<float> * T<float>
             Constructor <| T<float> * T<float> * T<string>
             Constructor <| T<float> * T<float> * T<string> * T<string>
            ]
        |+> Protocol [
            // Compares two Sizes.
            "equals" => (Size) ^-> T<bool>
            // Returns a T<string> representation of this Size.
            "toString" => T<unit> ^-> T<string>
            // The height along the y-axis, in pixels.
            "height" =@ T<int>
            // The width along the x-axis, in pixels.
            "width" =@ T<int>
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

    let MVCArrayType = Generic.Type1()                          

    let StreetViewLink =
        Pattern.Config "StreetViewLink" {
            Required = []
            Optional =
                [    
                    // A localized T<string> describing the link.
                    "description", T<string>
                    // The heading of the link.
                    "heading", T<float>
                    // A unique identifier for the panorama. This id is stable within a session but unstable across sessions.
                    "pano", T<string>
                    // Color of the link
                    "roadColor", T<string>
                    // Opacity of the link
                    "roadOpacity", T<float>
                ]    
        }

    let StreetViewLocation =
        Pattern.Config "StreetViewLocation" {
            Required = []
            Optional =
                [                    
                    // A localized T<string> describing the location.
                    "description", T<string>
                    // The latlng of the panorama.
                    "latLng", LatLng.Type
                    // A unique identifier for the panorama. This is stable within a session but unstable across sessions.
                    "pano", T<string>
                ]
        }

    let StreetViewTileData =
        Class "google.maps.StreetViewTileData"
        |+> Protocol [
            // The heading (in degrees) at the center of the panoramic tiles.
            "centerHeading" =@ T<float>
            // The size (in pixels) at which tiles will be rendered. This may not be the native tile image size.
            "tileSize" =@ Size
            // The size (in pixels) of the whole panorama's "world".
            "worldSize" =@ Size
            // Gets the tile image URL for the specified tile. pano is the panorama ID of the Street View tile. tileZoom is the zoom level of the tile. tileX is the x-coordinate of the tile. tileY is the y-coordinate of the tile. Returns the URL for the tile image.
            "getTileUrl" => (T<string> * T<int> * T<int> * T<int>) ^-> T<string>
        ]
    
    let StreetViewPanoramaData =
        Pattern.Config "StreetViewPanoramaData" {
            Required = []
            Optional =
                [
                    // Specifies the copyright text for this panorama.
                    "copyright", T<string>
                    // Specifies the navigational links to adjacent panoramas.
                    "links", Type.ArrayOf StreetViewLink
                    // Specifies the location meta-data for this panorama.
                    "location", StreetViewLocation.Type
                    // Specifies the custom tiles for this panorama.
                    "tiles", StreetViewTileData.Type
                ]
        }

    let StreetViewStatus =
        Pattern.EnumInlines "StreetViewStatus" [
            // The request was successful.
            "OK", "google.maps.StreetViewStatus.OK"
            // The request could not be successfully processed, yet the exact reason for failure is unknown.
            "UNKNOWN_ERROR", "google.maps.StreetViewStatus.UNKNOWN_ERROR"
            // There are no nearby panoramas.
            "ZERO_RESULTS", "google.maps.StreetViewStatus.ZERO_RESULTS"
        ]

    let StreetViewService =
        Class "google.maps.StreetViewService"
        |+> Protocol [
            // Retrieves the data for the given pano id and passes it to the provided callback as a StreetViewPanoramaData object. Pano ids are unique per panorama and stable for the lifetime of a session, but are liable to change between sessions.
            "getPanoramaById" => (T<string> * (StreetViewPanoramaData * StreetViewStatus ^-> T<unit>)) ^-> T<unit>
            // Retrieves the StreetViewPanoramaData for a panorama within a given radius of the given LatLng. The StreetViewPanoramaData is passed to the provided callback. If the radius is less than 50 meters, the nearest panorama will be returned.
            "getPanoramaByLocation" => (LatLng * T<float> * (StreetViewPanoramaData * StreetViewStatus ^-> T<unit>)) ^-> T<unit>
        ]

    let StreetViewAddressControlOptions =
        Pattern.Config "StreetViewAddressControlOptions" {
            Required = []
            Optional =
                [    
                    // Position id. This id is used to specify the position of the control on the map. The default position is TOP_LEFT.
                    "position", ControlPosition.Type
                    // CSS styles to apply to the Street View address control. For example, {backgroundColor: 'red'}.
                    "style", T<obj>
                ]
        }

    let StreetViewPov =
        Pattern.Config "StreetViewPov" {
            Required = []
            Optional =
                [
                    // The camera heading in degrees relative to true north. True north is 0°, east is 90°, south is 180°, west is 270°.
                    "heading", T<float>
                    // The camera pitch in degrees, relative to the street view vehicle. Ranges from 90° (directly upwards) to -90° (directly downwards).
                    "pitch", T<float>
                    // The zoom level. Fully zoomed-out is level 0, zooming in increases the zoom level.
                    "zoom", T<float>
                ]
        }
    
    let StreetViewPanoramaOptions =
        Pattern.Config "StreetViewPanoramaOptions" {
            Required = []
            Optional =
                [
                    // The enabled/disabled state of the address control.
                    "addressControl", T<bool>
                    // The display options for the address control.
                    "addressControlOptions", StreetViewAddressControlOptions.Type
                    // If true, the close button is displayed. Disabled by default.
                    "enableCloseButton", T<bool>
                    // The enabled/disabled state of the links control.
                    "linksControl", T<bool>
                    // The enabled/disabled state of the navigation control.
                    "navigationControl", T<bool>
                    // The display options for the navigation control.
                    "navigationControlOptions", NavigationControlOptions.Type
                    // The panorama ID, which should be set when specifying a custom panorama.
                    "pano", T<string>
                    // Custom panorama provider, which takes a T<string> pano id and returns an object defining the panorama given that id. This function must be defined to specify custom panorama imagery.
                    "panoProvider", T<string> ^-> StreetViewPanoramaData
                    // The LatLng position of the Street View panorama.
                    "position", LatLng.Type
                    // The camera orientation, specified as heading, pitch, and zoom, for the panorama.
                    "pov", StreetViewPov.Type
                    // If true, the Street View panorama is visible on load.
                    "visible", T<bool>
                ]
        }

    let StreetViewPanorama = 
        Class "google.maps.StreetViewPanorama"
        |+> [Constructor (Node * !? StreetViewPanoramaOptions)]
        |+> Protocol [
            // Additional controls to attach to the panorama. To add a control to the panorama, add the control's <div> to the MVCType.ArrayOf corresponding to the ControlPosition where it should be rendered.
            "controls" =@ Type.ArrayOf (MVCArrayType Node)
            // Returns the set of navigation links for the Street View panorama.
            "getLinks" => T<unit> ^-> Type.ArrayOf StreetViewLink
            // Returns the current panorama ID for the Street View panorama. This id is stable within the browser's current session only.
            "getPano" => T<unit> ^-> T<string>
            // Returns the current LatLng position for the Street View panorama.
            "getPosition" => T<unit> ^-> LatLng
            // Returns the current point of view for the Street View panorama.
            "getPov" => T<unit> ^-> StreetViewPov
            // Returns true if the panorama is visible. It does not specify whether Street View imagery is available at the specified position.
            "getVisible" => T<unit> ^-> T<bool>
            // T<unit>	Set the custom panorama provider called on pano change to load custom panoramas.
            "registerPanoProvider" => (T<string> ^-> StreetViewPanoramaData) ^-> StreetViewPanoramaData
            // Sets the current panorama ID for the Street View panorama.
            "setPano" => (T<string>) ^-> T<unit>
            // Sets the current LatLng position for the Street View panorama.
            "setPosition" => (LatLng) ^-> T<unit>
            // Sets the point of view for the Street View panorama.
            "setPov" => (StreetViewPov) ^-> T<unit>
            // Sets to true to make the panorama visible. If set to false, the panorama will be hidden whether it is embedded in the map or in its own <div>.
            "setVisible" => (T<bool>) ^-> T<unit>
        ]


    let MapOptions =
        Pattern.Config "MapOptions" 
            {
                Optional = 
                    [
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
                        // If false, prevents the map from being controlled by the keyboard. Keyboard shortcuts are enabled by default.
                        "keyboardShortcuts", T<bool>
                        // The initial enabled/disabled state of the Map type control.
                        "mapTypeControl", T<bool>
                        // The initial display options for the Map type control.
                        "mapTypeControlOptions", MapTypeControlOptions.Type
                        // The initial enabled/disabled state of the navigation control.
                        "navigationControl", T<bool>
                        // The initial display options for the navigation control.
                        "navigationControlOptions", NavigationControlOptions.Type
                        // If true, do not clear the contents of the Map div.
                        "noClear", T<bool>
                        // The initial enabled/disabled state of the scale control.
                        "scaleControl", T<bool>
                        // The initial display options for the scale control.
                        "scaleControlOptions", ScaleControlOptions.Type
                        // If false, disables scrollwheel zooming on the map. The scrollwheel is enabled by default.
                        "scrollwheel", T<bool>
                        // A StreetViewPanorama to display when the Street View pegman is dropped on the map. If no panorama is specified, a default StreetViewPanorama will be displayed in the map's div when the pegman is dropped.
                        "streetView", StreetViewPanorama.Type
                        // The initial enabled/disabled state of the Street View pegman control.
                        "streetViewControl", T<bool>

                    ]
                Required = 
                    [
                        // The initial Map zoom level. Required.
                        "zoom", T<int>
                        // The initial Map center. Required.
                        "center", LatLng.Type
                        // The initial Map mapTypeId. Required.
                        "mapTypeId", MapTypeId.Type
                    ]        
            }
    
    
    let MVCObject =
        let MVCObject = Class "google.maps.MVCObject"
        MVCObject
        |+> Protocol [
            // Binds a View to a Model.
            "bindTo" => (T<string> * MVCObject) ^-> T<unit>
            "bindTo" => (T<string> * MVCObject * T<string>) ^-> T<unit>
            "bindTo" => (T<string> * MVCObject * T<string> * T<bool>) ^-> T<unit>
            
            // Generic handler for state changes. Override this in derived classes to handle arbitrary state changes.
            "changed" => T<string> ^-> T<unit>
            // Gets a value.
            "get" => T<string> ^-> T<obj>
            // Notify all observers of a change on this property. This notifies both objects that are bound to the object's property as well as the object that it is bound to.
            "notify" => T<string> ^-> T<unit>
            // Sets a value.
            "set" => T<string> * T<obj> ^-> T<unit>
            // Sets a collection of key-value pairs.
            "setValues" => T<obj> ^-> T<unit>
            // Removes a binding. Unbinding will set the unbound property to the current value. The object will not be notified, as the value has not changed.
            "unbind" => T<string> ^-> T<unit>
            // Removes all bindings.
            "unbindAll" => T<unit> ^-> T<unit>
        ] |+> [Constructor T<unit>]

    let LatLngBounds = 
        let LatLngBounds = Class "google.maps.LatLngBounds"
        LatLngBounds
        |+> [Constructor <| !? LatLng * !? LatLng]
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
            // Converts to T<string>.
            "toString" => T<unit> ^-> T<string>
            // Returns a T<string> of the form "lat_lo,lng_lo,lat_hi,lng_hi" for this bounds, where "lo" corresponds to the southwest corner of the bounding box, while "hi" corresponds to the northeast corner of that box.
            "toUrlValue" => (!? T<float>) ^-> T<string>
            // Extends this bounds to contain the union of this and the given bounds.
            "union" => (LatLngBounds) ^-> LatLngBounds
        ]

    let Projection = 
        Class "google.maps.Projection"
        |+> Protocol [
            // Translates from the LatLng cylinder to the Point plane. This interface specifies a function which implements translation from given LatLng values to world coordinates on the map projection. The Maps API calls this method when it needs to plot locations on screen. Projection objects must implement this method.
            "fromLatLngToPoint" => LatLng * !? Point ^-> Point
            // This interface specifies a function which implements translation from world coordinates on a map projection to LatLng values. The Maps API calls this method when it needs to translate actions on screen to positions on the map. Projection objects must implement this method.
            "fromPointToLatLng" => Point * !? T<bool> ^-> LatLng
        ]
               
    let MVCArray = Generic / fun x -> 
        Class "google.maps.MVCArray"
        |=> MVCArrayType x 

    let MapTypeIMembers : CodeModel.IInterfaceMember list =
        [
//            // Alt text to display when this MapType's button is hovered over in the MapTypeControl. Optional.
//            "alt" =@ T<string>
//            // The maximum zoom level for the map when displaying this MapType. Required for base MapTypes, ignored for overlay MapTypes.
//            "maxZoom" =@ T<int>
//            // The minimum zoom level for the map when displaying this MapType. Optional; defaults to 0.
//            "minZoom" =@ T<int>
//            // Name to display in the MapTypeControl. Optional.
//            "name" =@ T<string>
//            // The Projection used to render this MapType. Optional; defaults to Mercator.
//            "projection" =@ Projection
//            // Radius of the planet for the map, in meters. Optional; defaults to Earth's equatorial radius of 6378137 meters.
//            "radius" =@ T<float>
//            // The dimensions of each tile. Required.
//            "tileSize" =@ Size
//            // Returns a tile for the given tile coordinate (x, y) and zoom level. This tile will be appended to the given ownerDocument.
            "setTile" => Point * T<int> * Document ^-> Node
            // Releases the given tile, performing any necessary cleanup. The provided tile will have already been removed from the document. Optional.
            "releaseTile" => Node ^-> T<unit>
        ]

    // This is a hack to make the interface implementation work due to bug 109 in WIG
    let MapTypeMembers : CodeModel.Member list =
        [
            // Alt text to display when this MapType's button is hovered over in the MapTypeControl. Optional.
            "alt" =@ T<string>
            // The maximum zoom level for the map when displaying this MapType. Required for base MapTypes, ignored for overlay MapTypes.
            "maxZoom" =@ T<int>
            // The minimum zoom level for the map when displaying this MapType. Optional; defaults to 0.
            "minZoom" =@ T<int>
            // Name to display in the MapTypeControl. Optional.
            "name" =@ T<string>
            // The Projection used to render this MapType. Optional; defaults to Mercator.
            "projection" =@ Projection
            // Radius of the planet for the map, in meters. Optional; defaults to Earth's equatorial radius of 6378137 meters.
            "radius" =@ T<float>
            // The dimensions of each tile. Required.
            "tileSize" =@ Size
            // Returns a tile for the given tile coordinate (x, y) and zoom level. This tile will be appended to the given ownerDocument.
            "setTile" => Point * T<int> * Document ^-> Node
            // Releases the given tile, performing any necessary cleanup. The provided tile will have already been removed from the document. Optional.
            "releaseTile" => Node ^-> T<unit>
        ]

    let MapType = 
        Interface "MapType"
        |+> MapTypeIMembers

    let MapTypeRegistry = 
        Class "google.maps.MapTypeRegistry"
        |=> Inherits MVCObject
        |+> [Constructor T<unit>]
        |+> Protocol [
            "set" => T<string> * MapType ^-> T<unit>
        ]
    
    
    let ImageMapTypeOptions = 
        Pattern.Config "ImageMapTypeOptions" {
            Required = []
            Optional = 
                [
                    // Alt text to display when this MapType's button is hovered over in the MapTypeControl.
                    "alt", T<string>
                    // Returns a string (URL) for given tile coordinate (x, y) and zoom level. This function should have a signature of: getTileUrl(Point, number):string
                    "getTileUrl", Point * T<int> ^-> T<string>
                    // Is the image a PNG? This is needed by some old browsers.
                    "isPng", T<bool>
                    // The maximum zoom level for the map when displaying this MapType.
                    "maxZoom", T<int>
                    // The minimum zoom level for the map when displaying this MapType. Optional.
                    "minZoom", T<int>
                    // Name to display in the MapTypeControl.
                    "name", T<string>
                    // The opacity to apply to the tiles. The opacity should be specified as a float value between 0 and 1.0, where 0 is fully transparent and 1 is fully opaque.
                    "opacity", T<float>
                    // The tile size.
                    "tileSize", Size.Type
                ]
        }

    let ImageMapType = 
        Class "google.maps.ImageMapType"
        |=> Implements [MapType]
        |+> Protocol MapTypeMembers
        |+> [Constructor ImageMapTypeOptions]

    let MapTypeStyleFeatureType =
        Pattern.EnumStrings "MapTypeStyleFeatureType" [        
            // Apply the rule to administrative areas.
            "administrative"
            // Apply the rule to countries.
            "administrative.country"
            // Apply the rule to land parcels.
            "administrative.land_parcel"
            // Apply the rule to localities.
            "administrative.locality"
            // Apply the rule to neighborhoods.
            "administrative.neighborhood"
            // Apply the rule to provinces.
            "administrative.province"
            // Apply the rule to all selector types.
            "all"
            // Apply the rule to landscapes.
            "landscape"
            // Apply the rule to man made structures.
            "landscape.man_made"
            // Apply the rule to natural features.
            "landscape.natural"
            // Apply the rule to points of interest.
            "poi"
            // Apply the rule to attractions for tourists.
            "poi.attraction"
            // Apply the rule to businesses.
            "poi.business"
            // Apply the rule to government buildings.
            "poi.government"
            // Apply the rule to emergency services (hospitals, pharmacies, police, doctors, etc).
            "poi.medical"
            // Apply the rule to parks.
            "poi.park"
            // Apply the rule to places of worship, such as church, temple, or mosque.
            "poi.place_of_worship"
            // Apply the rule to schools.
            "poi.school"
            // Apply the rule to sports complexes.
            "poi.sports_complex"
            // Apply the rule to all roads.
            "road"
            // Apply the rule to arterial roads.
            "road.arterial"
            // Apply the rule to highways.
            "road.highway"
            // Apply the rule to local roads.
            "road.local"
            // Apply the rule to all transit stations and lines.
            "transit"
            // Apply the rule to transit lines.
            "transit.line"
            // Apply the rule to all transit stations.
            "transit.station"
            // Apply the rule to airports.
            "transit.station.airport"
            // Apply the rule to bus stops.
            "transit.station.bus"
            // Apply the rule to rail stations.
            "transit.station.rail"
            // Apply the rule to bodies of water.
            "water"
        ]

    let MapTypeStyleElementType = 
        Pattern.EnumStrings "MapStyleElementType" [
            // Apply the rule to all elements of the specified feature.
            "all"
            // Apply the rule to the feature's geometry.
            "geometry"
            // Apply the rule to the feature's labels.
            "labels"
        ]

    let Visibility = 
        Pattern.EnumStrings "Visibility" ["on"; "off"; "simplified"]

    let MapTypeStyler = 
        Pattern.Config "MapTypeStyler" {
            Required = []
            Optional = 
                [
                    // Gamma. Modifies the gamma by raising the lightness to the given power. Valid values: Floating point numbers, [0.01, 10], with 1.0 representing no change.
                    "gamma", T<float>
                    // Sets the hue of the feature to match the hue of the color supplied. Note that the saturation and lightness of the feature is conserved, which means that the feature will not match the color supplied exactly. Valid values: An RGB hex string, i.e. '#ff0000'.
                    "hue", T<string>
                    // Inverts lightness. A value of true will invert the lightness of the feature while preserving the hue and saturation.
                    "invert_lightness", T<bool>
                    // Lightness. Shifts lightness of colors by a percentage of the original value if decreasing and a percentage of the remaining value if increasing. Valid values: [-100, 100].
                    "lightness", T<float>
                    // Saturation. Shifts the saturation of colors by a percentage of the original value if decreasing and a percentage of the remaining value if increasing. Valid values: [-100, 100].
                    "saturation", T<float>
                    // Visibility: Valid values: 'on', 'off' or 'simplifed'.
                    "visibility", Visibility.Type
                ]
        }

    let MapTypeStyle = 
        Pattern.Config "MapTypeStyle" {
            Required = []
            Optional = 
                [
                    // Selects the element type to which a styler should be applied. An element type distinguishes between the different representations of a feature. Optional; if elementType is not specified, the value is assumed to be 'all'.
                    "elementType", MapTypeStyleElementType.Type
                    // Selects the feature, or group of features, to which a styler should be applied. Optional; if featureType is not specified, the value is assumed to be 'all'.
                    "featureType", MapTypeStyleFeatureType.Type
                    // The style rules to apply to the selectors. The rules are applied to the map's elements in the order they are listed in this array.
                    "stylers", Type.ArrayOf MapTypeStyler
                ]
        }

    
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
            |=> Implements [MapType]
            |+> [Constructor (Type.ArrayOf MapTypeStyle * !? StyledMapTypeOptions)]
            |=> Inherits MVCObject
    
        (StyledMapType, StyledMapTypeOptions)        
    
    
    // TODO: Events
    let Map =
        let Map = Class "google.maps.Map"
        Map
        |=> Inherits MVCObject
        |+> // Creates a new map inside of the given HTML container, which is typically a DIV element
            [ Constructor (Node * !? MapOptions) ]
        |+> Protocol [
                // Sets the maps to fit to the given bounds.
                "fitBounds" => LatLngBounds ^-> T<unit>
                // Returns the lat/lng bounds of the current viewport. If the map is not yet initialized (i.e. the mapType is still null), or center and zoom have not been set then the result is null.
                "getBounds" => T<unit> ^-> LatLngBounds
                "getCenter" => T<unit> ^-> LatLng
                "getDiv" => T<unit> ^-> Element
                "getMapTypeId" => T<unit> ^-> MapTypeId
                // Returns the current Projection. If the map is not yet initialized (i.e. the mapType is still null) then the result is null. Listen to projection_changed and check its value to ensure it is not null.
                "getProjection" => T<unit> ^-> Projection
                // Returns the default StreetViewPanorama bound to the map, which may be a default panorama embedded within the map, or the panorama set using setStreetView(). Changes to the map's streetViewControl will be reflected in the display of such a bound panorama.
                "getStreetView" => T<unit> ^-> StreetViewPanorama
                "getZoom" => T<unit>  ^-> T<int>
                // Changes the center of the map by the given distance in pixels. If the distance is less than both the width and height of the map, the transition will be smoothly animated. Note that the map coordinate system increases from west to east (for x values) and north to south (for y values).
                "panBy" => (T<int> * T<int>) ^-> T<unit>
                // Changes the center of the map to the given LatLng. If the change is less than both the width and height of the map, the transition will be smoothly animated.
                "panTo" => LatLng ^-> T<unit>
                // Pans the map by the minimum amount necessary to contain the given LatLngBounds. It makes no guarantee where on the map the bounds will be, except that as much of the bounds as possible will be visible. The bounds will be positioned inside the area bounded by the map type and navigation controls, if they are present on the map. If the bounds is larger than the map, the map will be shifted to include the northwest corner of the bounds. If the change in the map's position is less than both the width and height of the map, the transition will be smoothly animated.
                "panToBounds" => LatLngBounds ^-> T<unit>
                "setCenter" => LatLng ^-> T<unit>
                "setMapTypeId" => MapTypeId ^-> T<unit>
                "setOptions" => MapOptions ^-> T<unit>
                // Binds a StreetViewPanorama to the map. This panorama overrides the default StreetViewPanorama, allowing the map to bind to an external panorama outside of the map. Setting the panorama to null binds the default embedded panorama back to the map.
                "setStreetView" => StreetViewPanorama ^-> T<unit>
                "setZoom" => T<int> ^-> T<unit>
                
                // Additional controls to attach to the map. To add a control to the map, add the control's <div> to the MVCArray corresponding to the ControlPosition where it should be rendered.
                "controls" =@ (Type.ArrayOf <| MVCArrayType Node)
                // A registry of MapType instances by string ID.
                "mapTypes" =@ MapTypeRegistry
                // Additional map types to overlay.
                "overlayMapTypes" =@ MVCArrayType MapType
        ]
    
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
                    "map", Map.Type
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
                    "map", Map.Type
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
            "getMap" => T<unit> ^-> Map
            // Returns the radius of this circle (in meters).
            "getRadius" => T<unit> ^-> T<float>
            // Sets the center of this circle.
            "setCenter" => LatLng ^-> T<unit>
            // Renders the circle on the specified map. If map is set to null, the circle will be removed.
            "setMap" => Map ^-> T<unit>
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
            "getMap" => T<unit> ^-> Map
            // Sets the bounds of this rectangle.
            "setBounds" => LatLngBounds ^-> T<unit>
            // Renders the rectangle on the specified map. If map is set to null, the rectangle will be removed.
            "setMap" => Map ^-> T<unit>
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
                    "map", Map.Type
                ]
        }    

    let GroundOverlay = 
        Class "google.maps.GroundOverlay"
        |+> [Constructor GroundOverlayOptions]
        |=> Inherits MVCObject
        |+> Protocol [            
            // Gets the LatLngBounds of this overlay.
            "getBounds" => T<unit> ^-> LatLngBounds
            // Returns the map on which this ground overlay is displayed.
            "getMap" => T<unit> ^-> Map
            // Gets the url of the projected image.
            "getUrl" => T<unit> ^-> T<string>
            // Renders the ground overlay on the specified map. If map is set to null, the overlay is removed.
            "setMap" => (Map) ^-> T<unit>
        ]

    let BicyclingLayer =
        Class "google.maps.BicyclingLayer"
        |+> [Constructor T<unit>]
        |=> Inherits MVCObject
        |+> Protocol [
            // Returns the map on which this layer is displayed.
            "getMap" => T<unit> ^-> Map
            // Renders the layer on the specified map. If map is set to null, the layer will be removed.
            "setMap" => (Map) ^-> T<unit>
        ]
    
    let FusionTablesLayerOptions =
        Pattern.Config "FusionTablesLayerOptions" {
            Required = []
            Optional = 
                [
                    // By default, table data is displayed as geographic features. If true, the layer is used to display a heatmap representing the density of the geographic features returned by querying the selected table.
                    "heatmap", T<bool>
                    // The map on which to display the layer.
                    "map", Map.Type
                    // A Fusion Tables query to apply when selecting the data to display. Queries should not be URL escaped.
                    "query", T<string>
                    // Suppress the rendering of info windows when layer features are clicked.
                    "suppressInfoWindows", T<bool>
                ]
        }

    let FusionTablesLayer =
        Class "google.maps.FusionTablesLayer"
        |+> [Constructor <| T<string> * !? FusionTablesLayerOptions]
        |=> Inherits MVCObject
        |+> Protocol [ 
            // Returns the map on which this layer is displayed.
            "getMap" => T<unit> ^-> Map
            // Returns the ID of the table from which to query data.
            "getTableId" => T<unit> ^-> T<int>
            "getQuery" => T<unit> ^-> T<string>
            // Renders the layer on the specified map. If map is set to null, the layer will be removed.
            "setMap" => Map ^-> T<unit>
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
                    "map", Map.Type
                    // By default, the input map is centered and zoomed to the bounding box of the contents of the layer. If this option is set to true, the viewport is left unchanged, unless the map's center and zoom were never set.
                    "preserveViewport", T<bool>
                    // Suppress the rendering of info windows when layer features are clicked.
                    "suppressInfoWindows", T<bool>
                ]
        }

    let KmlLayer =
        Class "google.maps.KmlLayer"
        |+> [Constructor <| T<string> * !? KmlLayerOptions]
        |=> Inherits MVCObject
        |+> Protocol [         
            // Get the default viewport for the layer being displayed.
            "getDefaultViewport" => T<unit> ^-> LatLngBounds
            // Get the map on which the KML Layer is being rendered.
            "getMap" => T<unit> ^-> Map
            // Get the metadata associated with this layer, as specified in the layer markup.
            "getMetadata" => T<unit> ^-> KmlLayerMetadata
            // Get the URL of the geographic markup which is being displayed.
            "getUrl" => T<unit> ^-> T<string>
            // Renders the KML Layer on the specified map. If map is set to null, the layer is removed.
            "setMap" => (Map) ^-> T<unit>
        ]    
    
    let TrafficLayer = 
        Class "google.maps.TrafficLayer"
        |+> [Constructor T<unit>]
        |=> Inherits MVCObject
        |+> Protocol [         
            // Returns the map on which this layer is displayed.
            "getMap" => T<unit> ^-> Map
            // Renders the layer on the specified map. If map is set to null, the layer will be removed.
            "setMap" => Map ^-> T<unit>
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
                    "map", Map + StreetViewPanorama
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
        |=> Inherits MVCObject
        |+> [Constructor !? MarkerOptions]
        |+> Protocol [
            "getClickable" => T<unit> ^-> T<bool>
            "getCursor" => T<unit> ^-> T<string>
            "getDraggable" => T<unit> ^-> T<bool>
            "getFlat" => T<unit> ^-> T<bool>
            "getIcon" => T<unit> ^-> T<string> + MarkerImage
            "getMap" => T<unit> ^-> Map + StreetViewPanorama
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
            "setMap" => (Map + StreetViewPanorama) ^-> T<unit>
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
                    "map", Map.Type
                    "path", MVCArrayType LatLng + Type.ArrayOf LatLng
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
        |=> Inherits MVCObject
        |+> [Constructor !? PolylineOptions]
        |+> Protocol [
            // Returns the map on which this poly is attached.
            "getMap" => T<unit> ^-> Map
            // Retrieves the first path.
            "getPath" => T<unit> ^-> MVCArrayType LatLng
            // Renders this Polyline or Polygon on the specified map. If map is set to null, the Poly will be removed.
            "setMap" => Map ^-> T<unit>
            // 
            "setOptions" => PolylineOptions ^-> T<unit>
            // Sets the first path. See Polyline options for more details.
            "setPath" => (MVCArrayType LatLng + Type.ArrayOf LatLng) ^-> T<unit>
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
                    "map", Map.Type
                    // The ordered sequence of coordinates that designates a closed loop. Unlike polylines, a polygon may consist of one or more paths. As a result, the paths property may specify one or more arrays of LatLng coordinates. Simple polygons may be defined using a single array of LatLngs. More complex polygons may specify an array of arrays. Any simple arrays are convered into MVCArrays. Inserting or removing LatLngs from the MVCArray will automatically update the polygon on the map.
                    "paths", (MVCArrayType LatLng + MVCArrayType (MVCArrayType LatLng)
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
        |=> Inherits MVCObject
        |+> [Constructor !? PolygonOptions]
        |+> Protocol [
            // Returns the map on which this poly is attached.
            "getMap" => T<unit> ^-> Map
            // Retrieves the first path.
            "getPath" => T<unit> ^-> MVCArrayType LatLng
            // Retrieves the paths for this Polygon.
            "getPaths" => T<unit> ^-> MVCArrayType (MVCArrayType LatLng)
            // Renders this Polyline or Polygon on the specified map. If map is set to null, the Poly will be removed.
            "setMap" => Map ^-> T<unit>
            // 
            "setOptions" => PolygonOptions ^-> T<unit>
            // Sets the first path. See Polyline options for more details.
            "setPath" => MVCArrayType LatLng + Type.ArrayOf LatLng ^-> T<unit>
            // Sets the path for this Polygon.
            "setPaths" => (MVCArrayType (MVCArray LatLng) + MVCArrayType LatLng
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
        |=> Inherits MVCObject
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
            "open" => ((Map + StreetViewPanorama) * !? MVCObject) ^-> T<unit>
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
        |=> Inherits MVCObject
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
                    "map", Map.Type
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
        |=> Inherits MVCObject
        |+> [Constructor !? DirectionsRendererOptions]
        |+> Protocol [
            // Returns the renderer's current set of directions.
            "getDirections" =>  T<unit> ^-> DirectionsResult
            // Returns the map on which the DirectionsResult is rendered.
            "getMap" =>  T<unit> ^-> Map
            // Returns the panel <div> in which the DirectionsResult is rendered.
            "getPanel" =>  T<unit> ^-> Node
            // Returns the current (zero-based) route index in use by this DirectionsRenderer object.
            "getRouteIndex" =>  T<unit> ^-> T<int>
            // Set the renderer to use the result from the DirectionsService. Setting a valid set of directions in this manner will display the directions on the renderer's designated map and panel.
            "setDirections" =>  DirectionsResult ^-> T<unit>
            // This method specifies the map on which directions will be rendered. Pass null to remove the directions from the map.
            "setMap" =>  Map ^-> T<unit>
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
    

    let Assembly =
        Assembly [
            Namespace "IntelliFactory.WebSharper.Google.Maps" [
                BicyclingLayer
                Circle
                CircleOptions
                ControlPosition
                DirectionsDistance
                DirectionsDuration
                DirectionsLeg
                DirectionsRenderer
                DirectionsRendererOptions
                DirectionsRequest
                DirectionsResult
                DirectionsRoute
                DirectionsService
                DirectionsStatus
                DirectionsStep
                DirectionsTravelMode
                DirectionsUnitSystem
                DirectionsWaypoint
                ElevationResult
                ElevationService
                ElevationStatus
                Event
                FusionTablesCell
                FusionTablesLayer
                FusionTablesLayerOptions
                FusionTablesMouseEvent
                Geocoder
                GeocoderAddressComponent
                GeocoderGeometry
                GeocoderLocationType
                GeocoderRequest
                GeocoderResult
                GeocoderStatus
                GroundOverlay
                GroundOverlayOptions
                ImageMapType
                ImageMapTypeOptions
                InfoWindow
                InfoWindowOptions
                KmlAuthor
                KmlFeatureData
                KmlLayer
                KmlLayerMetadata
                KmlLayerOptions
                KmlMouseEvent
                LatLng
                LatLngBounds
                LocationElevationRequest
                Generic - MVCArray
                MVCObject
                Map
                MapCanvasProjection
                MapsEventListener
                MapOptions
                MapPanes
                MapType
                MapTypeControlOptions
                MapTypeControlStyle
                MapTypeId
                MapTypeRegistry
                MapTypeStyle
                MapTypeStyleElementType
                MapTypeStyleFeatureType
                MapTypeStyler
                Marker
                MarkerImage
                MarkerOptions
                MarkerShape
                MarkerShapeType
                NavigationControlOptions
                NavigationControlStyle
                OverlayView
                PathElevationRequest
                Point
                Polygon
                PolygonOptions
                Polyline
                PolylineOptions
                Projection
                Rectangle
                RectangleOptions
                ScaleControlOptions
                ScaleControlStyle
                Size
                StreetViewAddressControlOptions
                StreetViewLink
                StreetViewLocation
                StreetViewPanorama
                StreetViewPanoramaData
                StreetViewPanoramaOptions
                StreetViewPov
                StreetViewService
                StreetViewStatus
                StreetViewTileData
                // StyledMapType: FIXME after bugs 103, 108
                StyledMapTypeOptions
                TrafficLayer
                Visibility
            ]
        ]

