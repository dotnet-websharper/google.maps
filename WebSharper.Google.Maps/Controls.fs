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
/// Definitions for the Controls part of the API. See:
/// http://developers.google.com/maps/documentation/javascript/reference
module WebSharper.Google.Maps.Controls

open WebSharper.InterfaceGenerator
open WebSharper.Google.Maps.Notation

let ControlPosition =
    Class "ControlPosition"
    |+> Static [
        "BOTTOM_CENTER" =? TSelf
        |> WithComment "Elements are positioned in the center of the bottom row."

        "BOTTOM_LEFT" =? TSelf
        |> WithComment "Elements are positioned in the bottom left and flow towards the middle. Elements are positioned to the right of the Google logo."

        "BOTTOM_RIGHT" =? TSelf
        |> WithComment "Elements are positioned in the bottom right and flow towards the middle. Elements are positioned to the left of the copyrights."

        "LEFT_BOTTOM" =? TSelf
        |> WithComment "Elements are positioned on the left, above bottom-left elements, and flow upwards."

        "LEFT_CENTER" =? TSelf
        |> WithComment "Elements are positioned in the center of the left side."

        "LEFT_TOP" =? TSelf
        |> WithComment "Elements are positioned on the left, below top-left elements, and flow downwards."

        "RIGHT_BOTTOM" =? TSelf
        |> WithComment "Elements are positioned on the right, above bottom-right elements, and flow upwards."

        "RIGHT_CENTER" =? TSelf
        |> WithComment "Elements are positioned in the center of the right side."

        "RIGHT_TOP" =? TSelf
        |> WithComment "Elements are positioned on the right, below top-right elements, and flow downwards."

        "TOP_CENTER" =? TSelf
        |> WithComment "Elements are positioned in the center of the top row."

        "TOP_LEFT" =? TSelf
        |> WithComment "Elements are positioned in the top left and flow towards the middle."

        "TOP_RIGHT" =? TSelf
        |> WithComment "Elements are positioned in the top right and flow towards the middle."
    ]

let MapTypeControlStyle =
    Class "google.maps.MapTypeControlStyle"
    |+> Static [
        "DEFAULT" =? TSelf
        |> WithComment "Uses the default map type control. The control which DEFAULT maps to will vary according to window size and other factors. It may change in future versions of the API."
        
        "DROPDOWN_MENU" =? TSelf
        |> WithComment "A dropdown menu for the screen realestate conscious."

        "HORIZONTAL_BAR" =? TSelf
        |> WithComment "The standard horizontal radio buttons bar."
    ]

let MapTypeControlOptions =
    Config "MapTypeControlOptions"
    |+> Instance [
            "mapTypeIds" =@ Type.ArrayOf Forward.MapTypeId
            |> WithComment "IDs of map types to show in the control."

            "position" =@ ControlPosition
            |> WithComment "Position id. Used to specify the position of the control on the map. The default position is TOP_RIGHT."

            "style" =@ MapTypeControlStyle
            |> WithComment "Style id. Used to select what style of map type control to display."
        ]

let OverviewMapControlOptions =
    Config "OverviewMapControlOptions"
    |+> Instance [
            "opened" =@ T<bool>
            |> WithComment "Whether the control should display in opened mode or collapsed (minimized) mode. By default, the control is closed."
        ]

let PanControlOptions =
    Config "PanControlOptions"
    |+> Instance [
            "position" =@ ControlPosition
            |> WithComment "Position id. Used to specify the position of the control on the map. The default position is TOP_LEFT."
        ]

let RotateControlOptions =
    Config "RotateControlOptions"
    |+> Instance [
            "position" =@ ControlPosition
            |> WithComment "Position id. Used to specify the position of the control on the map. The default position is TOP_LEFT."
        ]

let ScaleControlStyle =
    Class "google.maps.ScaleControlStyle"
    |+> Static [
        "DEFAULT" =? TSelf
        |> WithComment "The standard scale control."
    ]

let ScaleControlOptions =
    Config "ScaleControlOptions"
    |+> Instance [
            "position" =@ ControlPosition
            |> WithComment "Position id. Used to specify the position of the control on the map. The default position is BOTTOM_LEFT when google.maps.visualRefresh is set to false. When google.maps.visualRefresh is true the scale control will be fixed at the BOTTOM_RIGHT."

            "style" =@ ScaleControlStyle
            |> WithComment "Style id. Used to select what style of scale control to display."
        ]

let StreetViewControlOptions =
    Config "StreetViewControlOptions"
    |+> Instance [
            "position" =@ ControlPosition
            |> WithComment "Position id. Used to specify the position of the control on the map. The default position is embedded within the navigation (zoom and pan) controls. If this position is empty or the same as that specified in the zoomControlOptions or panControlOptions, the Street View control will be displayed as part of the navigation controls. Otherwise, it will be displayed separately."
        ]

let ZoomControlStyle =
    Class "google.maps.ZoomControlStyle"
    |+> Static [
        "DEFAULT" =? TSelf
        |> WithComment "The default zoom control. The control which DEFAULT maps to will vary according to map size and other factors. It may change in future versions of the API."

        "LARGE" =? TSelf
        |> WithComment "The larger control, with the zoom slider in addition to +/- buttons."

        "SMALL" =? TSelf
        |> WithComment "A small control with buttons to zoom in and out."
    ]

let ZoomControlOptions =
    Config "ZoomControlOptions"
    |+> Instance [
            "position" =@ ControlPosition
            |> WithComment "Position id. Used to specify the position of the control on the map. The default position is TOP_LEFT."

            "style" =@ ZoomControlStyle
            |> WithComment "Style id. Used to select what style of zoom control to display."
        ]
