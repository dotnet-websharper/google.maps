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
        Config "google.maps.StreetViewLocation"
            [
                "pano", T<string>
            ]
            [
                "description", T<string>
                "latLng", T<obj>
                "shortDescription", T<string>
            ]

    let StreetViewLink =
        Config "google.maps.StreetViewLink"
            [
                "description", T<string>
                "heading", T<float>
                "pano", T<string>
            ]
            []

    let StreetViewPov =
        Config "google.maps.StreetViewPov"
            [
                "heading", T<float>
                "pitch", T<float>
            ]
            []

    let StreetViewPanoramaData =
        Config "google.maps.StreetViewPanoramaData"
            [
                "tiles", StreetViewTileData.Type
            ]
            [
                "copyright", T<string>
                "imageDate", T<string>
                "links", !|StreetViewLink
                "location", StreetViewLocation.Type
            ]

    let StreetViewAddressControlOptions =
        Config "google.maps.StreetViewAddressControlOptions"
            []
            [
                "position", Controls.ControlPosition.Type
            ]

    let StreetViewPanoramaOptions =
        Config "google.maps.StreetViewPanoramaOptions"
            []
            [
                "addressControl", T<bool>
                "addressControlOptions", T<obj>
                "clickToGo", T<bool>
                "controlSize", T<int>
                "disableDefaultUI", T<bool>
                "disableDoubleClickZoom", T<bool>
                "enableCloseButton", T<bool>
                "fullscreenControl", T<bool>
                "fullscreenControlOptions", T<obj>
                "imageDateControl", T<bool>
                "linksControl", T<bool>
                "motionTracking", T<bool>
                "motionTrackingControl", T<bool>
                "motionTrackingControlOptions", T<obj>
                "panControl", T<bool>
                "panControlOptions", T<obj>
                "pano", T<string>
                "position", T<obj>
                "pov", T<obj>
                "scrollwheel", T<bool>
                "showRoadLabels", T<bool>
                "visible", T<bool>
                "zoom", T<float>
                "zoomControl", T<bool>
                "zoomControlOptions", T<obj>
            ]

    let PanoProviderOptions =
        Config "google.maps.PanoProviderOptions"
            []
            [
                "cors", T<bool>
            ]

    let StreetViewStatus =
        Pattern.EnumStrings "google.maps.StreetViewStatus" [
            "OK"
            "UNKNOWN_ERROR"
            "ZERO_RESULTS"
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
        Pattern.EnumStrings "google.maps.StreetViewPreference" [
            "best"
            "nearest"
        ]

    let StreetViewSource = Controls.StreetViewSource

    let StreetViewLocationRequest =
        Config "google.maps.StreetViewLocationRequest"
            []
            [
                "location", Base.LatLng + Base.LatLngLiteral
                "preference", StreetViewPreference.Type
                "radius", T<int>
                "source", StreetViewSource.Type
                "sources", !|StreetViewSource.Type
            ]

    let StreetViewPanoRequest =
        Config "google.maps.StreetViewPanoRequest"
            []
            [
                "pano", T<string>
            ]

    let StreetViewResponse =
        Config "google.maps.StreetViewResponse"
            []
            [
                "data", StreetViewPanoramaData.Type
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
