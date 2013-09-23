/// Definitions for the StreetView part of the API. See:
/// http://developers.google.com/maps/documentation/javascript/reference
module IntelliFactory.WebSharper.Google.Maps.StreetView

open IntelliFactory.WebSharper.InterfaceGenerator
open IntelliFactory.WebSharper.Google.Maps.Notation

let StreetViewTileData =
    Class "google.maps.StreetViewTileData"
    |+> Protocol [
        // The heading (in degrees) at the center of the panoramic tiles.
        "centerHeading" =@ T<float>
        // The size (in pixels) at which tiles will be rendered. This may not be the native tile image size.
        "tileSize" =@ Base.Size
        // The size (in pixels) of the whole panorama's "world".
        "worldSize" =@ Base.Size
        // Gets the tile image URL for the specified tile. pano is the panorama ID of the Street View tile. tileZoom is the zoom level of the tile. tileX is the x-coordinate of the tile. tileY is the y-coordinate of the tile. Returns the URL for the tile image.
        "getTileUrl" =>
            Fun T<string> [
                T<string>?pano
                T<int>?tileZoom
                T<int>?tileX
                T<int>?tileY
            ]
    ]

let StreetViewLocation =
    Pattern.Config "StreetViewLocation" {
        Required = []
        Optional =
        [
            // A localized T<string> describing the location.
            "description", T<string>
            // The latlng of the panorama.
            "latLng", Base.LatLng.Type
            // A unique identifier for the panorama. This is stable within a session but unstable across sessions.
            "pano", T<string>
        ]
    }

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
        ]
    }

let StreetViewPanoramaData =
    Pattern.Config "StreetViewPanoramaData" {
        Required = []
        Optional =
            [
                // Specifies the copyright text for this panorama.
                "copyright", T<string>
                "imageDate", T<string>
                // Specifies the navigational links to adjacent panoramas.
                "links", Type.ArrayOf StreetViewLink
                // Specifies the location meta-data for this panorama.
                "location", StreetViewLocation.Type
                // Specifies the custom tiles for this panorama.
                "tiles", StreetViewTileData.Type
            ]
    }

let StreetViewAddressControlOptions =
    Pattern.Config "StreetViewAddressControlOptions" {
        Required = []
        Optional =
        [
            // Position id. This id is used to specify the position of the control on the map. The default position is TOP_LEFT.
            "position", Controls.ControlPosition.Type
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
                "clickToGo", T<bool>
                "disableDoubleClickZoom", T<bool>
                // If true, the close button is displayed. Disabled by default.
                "enableCloseButton", T<bool>
                "imageDateControl", T<bool>
                // The enabled/disabled state of the links control.
                "linksControl", T<bool>
                "panControl", T<bool>
                "panControlOptions", Controls.PanControlOptions.Type
                // The panorama ID, which should be set when specifying a custom panorama.
                "pano", T<string>
                // Custom panorama provider, which takes a T<string> pano id and returns an object defining the panorama given that id. This function must be defined to specify custom panorama imagery.
                "panoProvider", T<string> ^-> StreetViewPanoramaData
                // The LatLng position of the Street View panorama.
                "position", Base.LatLng.Type
                // The camera orientation, specified as heading, pitch, and zoom, for the panorama.
                "pov", StreetViewPov.Type
                "scrollwheel", T<bool>
                // If true, the Street View panorama is visible on load.
                "visible", T<bool>
                "zoomControl", T<bool>
                "zoomControlOptions", Controls.ZoomControlOptions.Type
            ]
    }

let StreetViewPanorama =
    Class "google.maps.StreetViewPanorama"
    |+>
        [
            Ctor [
                Node?container
                !? StreetViewPanoramaOptions
            ]
        ]
    |+> Protocol [
            // Returns the set of navigation links for the Street View panorama.
            "getLinks" => T<unit> ^-> Type.ArrayOf StreetViewLink
            // Returns the current panorama ID for the Street View panorama. This id is stable within the browser's current session only.
            "getPano" => T<unit> ^-> T<string>
            "getPhotographerPov" => T<unit> ^-> StreetViewPov
            // Returns the current LatLng position for the Street View panorama.
            "getPosition" => T<unit> ^-> Base.LatLng
            // Returns the current point of view for the Street View panorama.
            "getPov" => T<unit> ^-> StreetViewPov
            // Returns true if the panorama is visible. It does not specify whether Street View imagery is available at the specified position.
            "getVisible" => T<unit> ^-> T<bool>
            "getZoom" => T<unit> ^-> T<int>
            // T<unit>	Set the custom panorama provider called on pano change to load custom panoramas.
            "registerPanoProvider" => (T<string> ^-> StreetViewPanoramaData) ^-> T<unit>
            // Sets the current panorama ID for the Street View panorama.
            "setPano" => (T<string>) ^-> T<unit>
            // Sets the current LatLng position for the Street View panorama.
            "setPosition" => (Base.LatLng) ^-> T<unit>
            // Sets the point of view for the Street View panorama.
            "setPov" => (StreetViewPov) ^-> T<unit>
            // Sets to true to make the panorama visible. If set to false, the panorama will be hidden whether it is embedded in the map or in its own <div>.
            "setVisible" => (T<bool>) ^-> T<unit>
            "setZoom" => T<bool> ^-> T<unit>
            // Additional controls to attach to the panorama. To add a control to the panorama, add the control's <div> to the MVCType.ArrayOf corresponding to the ControlPosition where it should be rendered.
            "controls" =@ Type.ArrayOf (MVC.MVCArray Node)
            // TODO: events
        ]

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
        "getPanoramaByLocation" => (Base.LatLng * T<float> * (StreetViewPanoramaData * StreetViewStatus ^-> T<unit>)) ^-> T<unit>
    ]

let StreetViewCoverageLayer =
    Class "google.maps.StreetViewCoverageLayer"
    |=> Inherits MVC.MVCObject
    |+> Protocol [
        "getMap" => T<unit> ^-> Forward.Map
        "setMap" => Forward.Map ^-> T<unit>
        ]
