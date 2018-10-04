// $begin{copyright}
//
// This file is part of WebSharper
//
// Copyright (c) 2008-2018 IntelliFactory
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
/// Definitions for the StreetView part of the API. See:
/// http://developers.google.com/maps/documentation/javascript/reference
module WebSharper.Google.Maps.StreetView

open WebSharper.InterfaceGenerator
open WebSharper.Google.Maps.Notation

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
    ]

let StreetViewLink =
    Config "StreetViewLink"
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
    Config "StreetViewPanoramaData"
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
    Config "StreetViewAddressControlOptions"
    |+> Instance [
        "position" =@ Controls.ControlPosition
        |> WithComment "Position id. This id is used to specify the position of the control on the map. The default position is TOP_LEFT."
    ]

let StreetViewPanoramaOptions =
    Config "StreetViewPanoramaOptions"
    |+> Instance [
        "addressControl" =@ T<bool>
        |> WithComment "The enabled/disabled state of the address control."

        "addressControlOptions" =@ StreetViewAddressControlOptions.Type
        |> WithComment "The display options for the address control."

        "clickToGo" =@ T<bool>
        |> WithComment "The enabled/disabled state of click-to-go."

        "disableDefaultUI" =@ T<bool>
        |> WithComment "Enables/disables all default UI. May be overridden individually."

        "disableDoubleClickZoom" =@ T<bool>
        |> WithComment "Enables/disables zoom on double click. Disabled by default."

        "enableCloseButton" =@ T<bool>
        |> WithComment "If true, the close button is displayed. Disabled by default."

        "imageDateControl" =@ T<bool>
        |> WithComment "The enabled/disabled state of the imagery acquisition date control. Disabled by default."

        "linksControl" =@ T<bool>
        |> WithComment "The enabled/disabled state of the imagery acquisition date control. Disabled by default."

        "panControl" =@ T<bool>
        |> WithComment "The enabled/disabled state of the pan control."

        "panControlOptions" =@ Controls.PanControlOptions
        |> WithComment "The display options for the pan control."

        "pano" =@ T<string>
        |> WithComment "The panorama ID, which should be set when specifying a custom panorama."

        "panoProvider" =@ T<string> ^-> StreetViewPanoramaData
        |> WithComment "Custom panorama provider, which takes a string pano id and returns an object defining the panorama given that id. This function must be defined to specify custom panorama imagery."

        "position" =@ Base.LatLng
        |> WithComment "The LatLng position of the Street View panorama."

        "pov" =@ StreetViewPov
        |> WithComment "The camera orientation, specified as heading and pitch, for the panorama."

        "scrollwheel" =@ T<bool>
        |> WithComment "If false, disables scrollwheel zooming in Street View. The scrollwheel is enabled by default."

        "visible" =@ T<bool>
        |> WithComment "If true, the Street View panorama is visible on load."

        "zoomControl" =@ T<bool>
        |> WithComment "The enabled/disabled state of the zoom control."

        "zoomControlOptions" =@ Controls.ZoomControlOptions
        |> WithComment "The display options for the zoom control."
    ]

let StreetViewPanorama =
    Class "google.maps.StreetViewPanorama"
    |+> Static [
            Ctor [
                Node?Container
                !? StreetViewPanoramaOptions
            ]
        ]
    |+> Instance [
        "getLinks" => T<unit> ^-> Type.ArrayOf StreetViewLink
        |> WithComment "Returns the set of navigation links for the Street View panorama."

        "getPano" => T<unit> ^-> T<string>
        |> WithComment "Returns the current panorama ID for the Street View panorama. This id is stable within the browser's current session only."

        "getPhotographerPov" => T<unit> ^-> StreetViewPov
        |> WithComment "Returns the heading and pitch of the photographer when this panorama was taken. For Street View panoramas on the road, this also reveals in which direction the car was travelling. This data is available after the pano_changed event."

        "getPosition" => T<unit> ^-> Base.LatLng
        |> WithComment "Returns the current LatLng position for the Street View panorama."

        "getPov" => T<unit> ^-> StreetViewPov
        |> WithComment "Returns the current point of view for the Street View panorama."

        "getVisible" => T<unit> ^-> T<bool>
        |> WithComment "Returns true if the panorama is visible. It does not specify whether Street View imagery is available at the specified position."

        "getZoom" => T<unit> ^-> T<int>
        |> WithComment "Returns the zoom level of the panorama. Fully zoomed-out is level 0, where the field of view is 180 degrees. Zooming in increases the zoom level."

        "registerPanoProvider" => (T<string> ^-> StreetViewPanoramaData) ^-> T<unit>
        |> WithComment "Set the custom panorama provider called on pano change to load custom panoramas."

        "setPano" => (T<string>) ^-> T<unit>
        |> WithComment "Sets the current panorama ID for the Street View panorama."

        "setPosition" => (Base.LatLng) ^-> T<unit>
        |> WithComment "Sets the current LatLng position for the Street View panorama."

        "setPov" => (StreetViewPov) ^-> T<unit>
        |> WithComment "Sets the point of view for the Street View panorama."

        "setVisible" => (T<bool>) ^-> T<unit>
        |> WithComment "Sets to true to make the panorama visible. If set to false, the panorama will be hidden whether it is embedded in the map or in its own <div>."

        "setZoom" => T<bool> ^-> T<unit>
        |> WithComment "Sets the zoom level of the panorama. Fully zoomed-out is level 0, where the field of view is 180 degrees. Zooming in increases the zoom level."

        "controls" =? Type.ArrayOf MVC.MVCArray.[Node]
        |> WithComment "Additional controls to attach to the panorama. To add a control to the panorama, add the control's <div> to the MVCArray corresponding to the ControlPosition where it should be rendered."
    ]

let StreetViewStatus =
    Class "StreetViewStatus"
    |+> Static [
        "OK" =? TSelf
        |> WithComment "The request was successful."

        "UNKNOWN_ERROR" =? TSelf
        |> WithComment "The request could not be successfully processed, yet the exact reason for failure is unknown."

        "ZERO_RESULTS" =? TSelf
        |> WithComment "There are no nearby panoramas."
    ]

let StreetViewService =
    Class "google.maps.StreetViewService"
    |+> Instance [
        "getPanoramaById" => (T<string> * (StreetViewPanoramaData * StreetViewStatus ^-> T<unit>)) ^-> T<unit>
        |> WithComment "Retrieves the data for the given pano id and passes it to the provided callback as a StreetViewPanoramaData object. Pano ids are unique per panorama and stable for the lifetime of a session, but are liable to change between sessions."

        "getPanoramaByLocation" => (Base.LatLng * T<float> * (StreetViewPanoramaData * StreetViewStatus ^-> T<unit>)) ^-> T<unit>
        |> WithComment "Retrieves the StreetViewPanoramaData for a panorama within a given radius of the given LatLng. The StreetViewPanoramaData is passed to the provided callback. If the radius is less than 50 meters, the nearest panorama will be returned."
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
