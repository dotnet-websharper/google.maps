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
// Definitions for the StreetView part of the API. See:
// http://developers.google.com/maps/documentation/javascript/reference
namespace WebSharper.Google.Maps.Definition

module StreetView =

    open WebSharper.InterfaceGenerator
    open Notation

    let StreetViewTileData =
        Class "google.maps.StreetViewTileData"
        |+> Instance [
            "centerHeading" =? T<float>
            |> WithComment "The heading (in degrees) at the center of the panoramic tiles."

            "tileSize" =@ Base.Size
            |> WithComment "The size (in pixels) at which tiles will be rendered."

            "worldSize" =@ Base.Size
            |> WithComment "The size (in pixels) of the whole panorama's \"world\"."

            "getTileUrl" => T<string>?pano * T<int>?tileZoom * T<int>?tileX * T<int>?tileY ^-> T<string>
            |> WithComment "Gets the tile image URL for the specified tile.
    pano is the panorama ID of the Street View tile.
    tileZoom is the zoom level of the tile.
    tileX is the x-coordinate of the tile.
    tileY is the y-coordinate of the tile.
    Returns the URL for the tile image."
        ]

    let StreetViewLocation =
        Config "StreetViewLocation"
        |+> Instance [
            "description" =@ T<string>
            |> WithComment "A localized string describing the location."

            "latLng" =@ Base.LatLng
            |> WithComment "The latlng of the panorama."

            "pano" =@ T<string>
            |> WithComment "A unique identifier for the panorama. This is stable within a session but unstable across sessions."

            "shortDescription" =@ T<string>
            |> WithComment "Short description of the location."
        ]

    let StreetViewLink =
        Config "google.maps.StreetViewLink"
        |+> Instance [
                "description" =@ T<string>
                |> WithComment "A localized string describing the link."

                "heading" =@ T<float>
                |> WithComment "The heading of the link."

                "pano" =@ T<string>
                |> WithComment "A unique identifier for the panorama. This id is stable within a session but unstable across sessions."
        ]

    let StreetViewPov =
        Config "StreetViewPov"
        |+> Instance [
            "heading" =@ T<float>
            |> WithComment "The camera heading in degrees relative to true north. True north is 0°, east is 90°, south is 180°, west is 270°."

            "pitch" =@ T<float>
            |> WithComment "The camera pitch in degrees, relative to the street view vehicle. Ranges from 90° (directly upwards) to -90° (directly downwards)."
        ]

    let StreetViewPanoramaData =
        Config "google.maps.StreetViewPanoramaData"
        |+> Instance [
            "copyright" =@ T<string>
            |> WithComment "Specifies the copyright text for this panorama."

            "imageDate" =@ T<string>
            |> WithComment "Specifies the year and month in which the imagery in this panorama was acquired. The date string is in the form YYYY-MM."

            "links" =@ Type.ArrayOf StreetViewLink
            |> WithComment "Specifies the navigational links to adjacent panoramas."

            "location" =@ StreetViewLocation
            |> WithComment "Specifies the location meta-data for this panorama."

            "tiles" =@ StreetViewTileData
            |> WithComment "Specifies the custom tiles for this panorama."
        ]

    let StreetViewAddressControlOptions =
        Config "google.maps.StreetViewAddressControlOptions"
        |+> Instance [
            "position" =@ Controls.ControlPosition
            |> WithComment "Position id. This id is used to specify the position of the control on the map. The default position is TOP_LEFT."
        ]

    let StreetViewPanoramaOptions =
        Config "google.maps.StreetViewPanoramaOptions"
        |+> Instance [
            "addressControl" =@ T<bool>
            |> WithComment "The enabled/disabled state of the address control."

            "addressControlOptions" =@ StreetViewAddressControlOptions.Type
            |> WithComment "The display options for the address control."

            "clickToGo" =@ T<bool>
            |> WithComment "The enabled/disabled state of click-to-go. Not applicable to custom panoramas."

            "controlSize" =@ T<int>
            |> WithComment "Size in pixels of the controls appearing on the panorama. This value must be supplied directly when creating the Panorama, updating this value later may bring the controls into an undefined state. Only governs the controls made by the Maps API itself. Does not scale developer created custom controls."

            "disableDefaultUI" =@ T<bool>
            |> WithComment "Enables/disables all default UI. May be overridden individually."

            "disableDoubleClickZoom" =@ T<bool>
            |> WithComment "Enables/disables zoom on double click. Disabled by default."

            "enableCloseButton" =@ T<bool>
            |> WithComment "If true, the close button is displayed. Disabled by default."

            "fullscreenControl" =@ T<bool>
            |> WithComment "The enabled/disabled state of the fullscreen control."

            "fullscreenControlOptions" =@ Controls.FullscreenControlOptions
            |> WithComment "The display options for the fullscreen control."

            "imageDateControl" =@ T<bool>
            |> WithComment "The enabled/disabled state of the imagery acquisition date control. Disabled by default."

            "linksControl" =@ T<bool>
            |> WithComment "The enabled/disabled state of the links control."

            "motionTracking" =@ T<bool>
            |> WithComment "Whether motion tracking is on or off. Enabled by default when the motion tracking control is present and permission is granted by a user or not required, so that the POV (point of view) follows the orientation of the device. This is primarily applicable to mobile devices. If motionTracking is set to false while motionTrackingControl is enabled, the motion tracking control appears but tracking is off. The user can tap the motion tracking control to toggle this option. If motionTracking is set to true while permission is required but not yet requested, the motion tracking control appears but tracking is off. The user can tap the motion tracking control to request permission. If motionTracking is set to true while permission is denied by a user, the motion tracking control appears disabled with tracking turned off."

            "motionTrackingControl" =@ T<bool>
            |> WithComment "The enabled/disabled state of the motion tracking control. Enabled by default when the device has motion data, so that the control appears on the map. This is primarily applicable to mobile devices."

            "motionTrackingControlOptions" =@ Controls.MotionTrackingControlOptions
            |> WithComment "The display options for the motion tracking control."

            "panControl" =@ T<bool>
            |> WithComment "The enabled/disabled state of the pan control."

            "panControlOptions" =@ Controls.PanControlOptions
            |> WithComment "The display options for the pan control."

            "pano" =@ T<string>
            |> WithComment "The panorama ID, which should be set when specifying a custom panorama."

            "panoProvider" =@ T<string> ^-> StreetViewPanoramaData
            |> WithComment "Custom panorama provider, which takes a string pano id and returns an object defining the panorama given that id. This function must be defined to specify custom panorama imagery."

            "position" =@ Base.LatLng + Base.LatLngLiteral
            |> WithComment "The LatLng position of the Street View panorama."

            "pov" =@ StreetViewPov
            |> WithComment "The camera orientation, specified as heading and pitch, for the panorama."

            "scrollwheel" =@ T<bool>
            |> WithComment "If false, disables scrollwheel zooming in Street View. The scrollwheel is enabled by default."

            "showRoadLabels" =@ T<bool>
            |> WithComment "The display of street names on the panorama. If this value is not specified, or is set to true, street names are displayed on the panorama. If set to false, street names are not displayed. Default: true."

            "visible" =@ T<bool>
            |> WithComment "If true, the Street View panorama is visible on load."

            "zoom" =@ T<int>
            |> WithComment "The zoom of the panorama, specified as a number. A zoom of 0 gives a 180 degrees Field of View."

            "zoomControl" =@ T<bool>
            |> WithComment "The enabled/disabled state of the zoom control."

            "zoomControlOptions" =@ Controls.ZoomControlOptions
            |> WithComment "The display options for the zoom control."
        ]

    let PanoProviderOptions =
        Config "google.maps.PanoProviderOptions"
        |+> Instance [
            "cors" =@ T<bool>
            |> WithComment "If set, the renderer will use technologies (like webgl) that only work when cors headers are appropriately set on the provided images. It is the developer's task to serve the images correctly in combination with this flag, which might otherwise lead to SecurityErrors."
        ]

    let StreetViewStatus =
        Class "google.maps.StreetViewStatus"
        |+> Static [
            "OK" =? TSelf
            |> WithComment "The request was successful."

            "UNKNOWN_ERROR" =? TSelf
            |> WithComment "The request could not be successfully processed, yet the exact reason for failure is unknown."

            "ZERO_RESULTS" =? TSelf
            |> WithComment "There are no panoramas found that match the search criteria."
        ]

    let StreetViewPanorama =
        Class "google.maps.StreetViewPanorama"
        |=> Inherits MVC.MVCObject
        |+> Static [
                Ctor [
                    Node?Container
                    !? StreetViewPanoramaOptions
                ]
            ]
        |+> Instance [
            "focus" => T<unit> ^-> T<unit>
            |> WithComment "Sets focus on this StreetViewPanorama. You may wish to consider using this method along with a visible_changed event to make sure that StreetViewPanorama is visible before setting focus on it. A StreetViewPanorama that is not visible cannot be focused."

            "getLinks" => T<unit> ^-> Type.ArrayOf StreetViewLink
            |> WithComment "Returns the set of navigation links for the Street View panorama."

            "getLocation" => T<unit> ^-> StreetViewLocation
            |> WithComment "Returns the StreetViewLocation of the current panorama."

            "getMotionTracking" => T<unit -> bool>
            |> WithComment "Returns the state of motion tracker. If true when the user physically moves the device and the browser supports it, the Street View Panorama tracks the physical movements."

            "getPano" => T<unit> ^-> T<string>
            |> WithComment "Returns the current panorama ID for the Street View panorama. This id is stable within the browser's current session only."

            "getPhotographerPov" => T<unit> ^-> StreetViewPov
            |> WithComment "Returns the heading and pitch of the photographer when this panorama was taken. For Street View panoramas on the road, this also reveals in which direction the car was travelling. This data is available after the pano_changed event."

            "getPosition" => T<unit> ^-> Base.LatLng
            |> WithComment "Returns the current LatLng position for the Street View panorama."

            "getPov" => T<unit> ^-> StreetViewPov
            |> WithComment "Returns the current point of view for the Street View panorama."

            "getStatus" => T<unit> ^-> StreetViewStatus
            |> WithComment "Returns the status of the panorama on completion of the setPosition() or setPano() request."

            "getVisible" => T<unit> ^-> T<bool>
            |> WithComment "Returns true if the panorama is visible. It does not specify whether Street View imagery is available at the specified position."

            "getZoom" => T<unit> ^-> T<int>
            |> WithComment "Returns the zoom level of the panorama. Fully zoomed-out is level 0, where the field of view is 180 degrees. Zooming in increases the zoom level."

            "registerPanoProvider" => (T<string> ^-> StreetViewPanoramaData) * !? PanoProviderOptions ^-> T<unit>
            |> WithComment "Set the custom panorama provider called on pano change to load custom panoramas."

            "setLinks" => Type.ArrayOf StreetViewLink ^-> T<unit>
            |> WithComment "Sets the set of navigation links for the Street View panorama."

            "setMotionTracking" => T<bool> ^-> T<unit>
            |> WithComment "Sets the state of motion tracker. If true when the user physically moves the device and the browser supports it, the Street View Panorama tracks the physical movements."

            "setOptions" => StreetViewPanoramaOptions ^-> T<unit>
            |> WithComment "Sets a collection of key-value pairs."

            "setPano" => (T<string>) ^-> T<unit>
            |> WithComment "Sets the current panorama ID for the Street View panorama."

            "setPosition" => (Base.LatLng + Base.LatLngLiteral) ^-> T<unit>
            |> WithComment "Sets the current LatLng position for the Street View panorama."

            "setPov" => (StreetViewPov) ^-> T<unit>
            |> WithComment "Sets the point of view for the Street View panorama."

            "setVisible" => (T<bool>) ^-> T<unit>
            |> WithComment "Sets to true to make the panorama visible. If set to false, the panorama will be hidden whether it is embedded in the map or in its own <div>."

            "setZoom" => T<bool> ^-> T<unit>
            |> WithComment "Sets the zoom level of the panorama. Fully zoomed-out is level 0, where the field of view is 180 degrees. Zooming in increases the zoom level."

            "controls" =? Type.ArrayOf MVC.MVCArray.[Node]
            |> WithComment "Additional controls to attach to the panorama. To add a control to the panorama, add the control's <div> to the MVCArray corresponding to the ControlPosition where it should be rendered."

            // EVENTS
            "closeclick" => T<obj> -* Events.Event ^-> T<unit>
            |> WithComment "This event is fired when the close button is clicked."

            "pano_changed" => T<obj> -* T<unit> ^-> T<unit>
            |> WithComment "This event is fired when the panorama's pano id changes. The pano may change as the user navigates through the panorama or the position is manually set. Note that not all position changes trigger a pano_changed."

            "position_changed" => T<obj> -* T<unit> ^-> T<unit>
            |> WithComment "This event is fired when the panorama's position changes. The position changes as the user navigates through the panorama or the position is set manually."

            "pov_changed" => T<obj> -* T<unit> ^-> T<unit>
            |> WithComment "This event is fired when the panorama's point-of-view changes. The point of view changes as the pitch, zoom, or heading changes."

            "resize" => T<obj> -* T<unit> ^-> T<unit>
            |> WithComment "Developers should trigger this event on the panorama when its div changes size: google.maps.event.trigger(panorama, 'resize')."

            "status_changed" => T<obj> -* T<unit> ^-> T<unit>
            |> WithComment "This event is fired after every panorama lookup by id or location, via setPosition() or setPano()."

            "visible_changed" => T<obj> -* T<unit> ^-> T<unit>
            |> WithComment "This event is fired when the panorama's visibility changes. The visibility is changed when the Pegman is dragged onto the map, the close button is clicked, or setVisible() is called."

            "zoom_changed" => T<obj> -* T<unit> ^-> T<unit>
            |> WithComment "This event is fired when the panorama's zoom level changes."
        ]

    let StreetViewPreference =
        Class "google.maps.StreetViewPreference"
        |+> Static [
            "BEST" =? TSelf
            |> WithComment "Return the Street View panorama that is considered most likely to be what the user wants to see. The best result is determined by algorithms based on user research and parameters such as recognised points of interest, image quality, and distance from the given location."

            "NEAREST" =? TSelf
            |> WithComment " 	Return the Street View panorama that is the shortest distance from the provided location. This works well only within a limited radius. The recommended radius is 1km or less."
        ]

    let StreetViewLocationRequest =
        Interface "google.maps.StreetViewLocationRequest"
        |+> [
            "location" =@ Base.LatLng + Base.LatLngLiteral
            |> WithComment "Specifies the location where to search for a Street View panorama."

            "preference" =@ StreetViewPreference
            |> WithComment "Sets a preference for which panorama should be found within the radius: the one nearest to the provided location, or the best one within the radius."

            "radius" =@ T<float>
            |> WithComment "Sets a radius in meters in which to search for a panorama. Default: 50."

            "source" =@ Controls.StreetViewSource
            |> WithComment "Specifies the source of panoramas to search. This allows a restriction to search for just outdoor panoramas for example. Default: StreetViewSource.DEFAULT."
            |> ObsoleteWithMessage "Deprecated: Use sources instead."

            "sources" =@ T<System.Collections.Generic.IEnumerable<_>>.[Controls.StreetViewSource]
            |> WithComment "Specifies the sources of panoramas to search. This allows a restriction to search for just outdoor panoramas for example. Setting multiple sources will be evaluated as the intersection of those sources. Default: [StreetViewSource.DEFAULT]."
        ]

    let StreetViewPanoRequest =
        Interface "google.maps.StreetViewPanoRequest"
        |+> [
            "pano" =@ T<string>
            |> WithComment "Specifies the pano ID to search for."
        ]

    // let StreetViewResponse =
    //     Interface "google.maps.StreetViewResponse"
    //     |+> [
    //         "data" =@ StreetViewPanoramaData
    //         |> WithComment "The representation of a panorama."
    //     ]

    let StreetViewResponse =
        Class "google.maps.StreetViewResponse"
        |+> Instance [
            "data" =@ StreetViewPanoramaData
            |> WithComment "The representation of a panorama."
        ]

    let StreetViewService =
        Class "google.maps.StreetViewService"
        |+> Instance [
            "getPanorama" => ((StreetViewLocationRequest + StreetViewPanoRequest) * !? (StreetViewPanoramaData * StreetViewStatus ^-> T<unit>)) ^-> Promise[StreetViewResponse]
            |> WithComment "Retrieves the StreetViewPanoramaData for a panorama that matches the supplied Street View query request. The StreetViewPanoramaData is passed to the provided callback."
        ]

    let StreetViewCoverageLayer =
        Class "google.maps.StreetViewCoverageLayer"
        |=> Inherits MVC.MVCObject
        |+> Static [Constructor T<unit>]
        |+> Instance [
            "getMap" => T<unit> ^-> Forward.Map
            |> WithComment "Returns the map on which this layer is displayed."

            "setMap" => Forward.Map ^-> T<unit>
            |> WithComment "Renders the layer on the specified map. If the map is set to null, the layer will be removed."
        ]
