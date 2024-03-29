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
// Definitions for the Controls part of the API. See:
// http://developers.google.com/maps/documentation/javascript/reference
namespace WebSharper.Google.Maps.Definition

module Controls =

    open WebSharper.InterfaceGenerator
    open Notation

    let ControlPosition =
        Class "google.maps.ControlPosition"
        |+> Static [
            "BLOCK_END_INLINE_CENTER" =? TSelf
            |> WithComment "Equivalent to BOTTOM_CENTER in both LTR and RTL."

            "BLOCK_END_INLINE_END" =? TSelf
            |> WithComment "Equivalent to BOTTOM_RIGHT in LTR, or BOTTOM_LEFT in RTL."

            "BLOCK_END_INLINE_START" =? TSelf
            |> WithComment "Equivalent to BOTTOM_LEFT in LTR, or BOTTOM_RIGHT in RTL."

            "BLOCK_START_INLINE_CENTER" =? TSelf
            |> WithComment "Equivalent to TOP_CENTER in both LTR and RTL."

            "BLOCK_START_INLINE_END" =? TSelf
            |> WithComment "Equivalent to TOP_RIGHT in LTR, or TOP_LEFT in RTL."

            "BLOCK_START_INLINE_START" =? TSelf
            |> WithComment "Equivalent to TOP_LEFT in LTR, or TOP_RIGHT in RTL."

            "BOTTOM_CENTER" =? TSelf
            |> WithComment "Elements are positioned in the center of the bottom row. Consider using BLOCK_END_INLINE_CENTER instead."

            "BOTTOM_LEFT" =? TSelf
            |> WithComment "Elements are positioned in the bottom left and flow towards the middle. Elements are positioned to the right of the Google logo. Consider using BLOCK_END_INLINE_START instead."

            "BOTTOM_RIGHT" =? TSelf
            |> WithComment "Elements are positioned in the bottom right and flow towards the middle. Elements are positioned to the left of the copyrights. Consider using BLOCK_END_INLINE_END instead."

            "INLINE_END_BLOCK_CENTER" =? TSelf
            |> WithComment "Equivalent to RIGHT_CENTER in LTR, or LEFT_CENTER in RTL."

            "INLINE_END_BLOCK_END" =? TSelf
            |> WithComment "Equivalent to RIGHT_BOTTOM in LTR, or LEFT_BOTTOM in RTL."

            "INLINE_END_BLOCK_START" =? TSelf
            |> WithComment "Equivalent to RIGHT_TOP in LTR, or LEFT_TOP in RTL."

            "INLINE_START_BLOCK_CENTER" =? TSelf
            |> WithComment "Equivalent to LEFT_CENTER in LTR, or RIGHT_CENTER in RTL."

            "INLINE_START_BLOCK_END" =? TSelf
            |> WithComment "Equivalent to LEFT_BOTTOM in LTR, or RIGHT_BOTTOM in RTL."

            "INLINE_START_BLOCK_START" =? TSelf
            |> WithComment "Equivalent to LEFT_TOP in LTR, or RIGHT_TOP in RTL."

            "LEFT_BOTTOM" =? TSelf
            |> WithComment "Elements are positioned on the left, above bottom-left elements, and flow upwards. Consider using INLINE_START_BLOCK_END instead."

            "LEFT_CENTER" =? TSelf
            |> WithComment "Elements are positioned in the center of the left side. Consider using INLINE_START_BLOCK_CENTER instead."

            "LEFT_TOP" =? TSelf
            |> WithComment "Elements are positioned on the left, below top-left elements, and flow downwards. Consider using INLINE_START_BLOCK_START instead."

            "RIGHT_BOTTOM" =? TSelf
            |> WithComment "Elements are positioned on the right, above bottom-right elements, and flow upwards. Consider using INLINE_END_BLOCK_END instead."

            "RIGHT_CENTER" =? TSelf
            |> WithComment "Elements are positioned in the center of the right side. Consider using INLINE_END_BLOCK_CENTER instead."

            "RIGHT_TOP" =? TSelf
            |> WithComment " 	Elements are positioned on the right, below top-right elements, and flow downwards. Consider using INLINE_END_BLOCK_START instead."

            "TOP_CENTER" =? TSelf
            |> WithComment "Elements are positioned in the center of the top row. Consider using BLOCK_START_INLINE_CENTER instead."

            "TOP_LEFT" =? TSelf
            |> WithComment "Elements are positioned in the top left and flow towards the middle. Consider using BLOCK_START_INLINE_START instead."

            "TOP_RIGHT" =? TSelf
            |> WithComment "Elements are positioned in the top right and flow towards the middle. Consider using BLOCK_START_INLINE_END instead."
        ]

    let FullscreenControlOptions =
        Config "google.maps.FullscreenControlOptions"
        |+> Instance [
                "position" =@ ControlPosition
                |> WithComment "Position id. Used to specify the position of the control on the map. The default position is INLINE_END_BLOCK_START."
            ]

    let MapTypeControlStyle =
        Class "google.maps.MapTypeControlStyle"
        |+> Static [
            "DEFAULT" =? TSelf
            |> WithComment "Uses the default map type control. When the DEFAULT control is shown, it will vary according to window size and other factors. The DEFAULT control may change in future versions of the API."

            "DROPDOWN_MENU" =? TSelf
            |> WithComment "A dropdown menu for the screen realestate conscious."

            "HORIZONTAL_BAR" =? TSelf
            |> WithComment "The standard horizontal radio buttons bar."
        ]

    let MapTypeControlOptions =
        Config "google.maps.MapTypeControlOptions"
        |+> Instance [
                "mapTypeIds" =@ Type.ArrayOf (Forward.MapTypeId + T<string>)
                |> WithComment "IDs of map types to show in the control."

                "position" =@ ControlPosition
                |> WithComment "Position id. Used to specify the position of the control on the map. The default position is BLOCK_START_INLINE_START."

                "style" =@ MapTypeControlStyle
                |> WithComment "Style id. Used to select what style of map type control to display."
            ]

    let MotionTrackingControlOptions =
        Config "google.maps.MotionTrackingControlOptions"
        |+> Instance [
                "position" =@ ControlPosition
                |> WithComment "Position id. This is used to specify the position of this control on the panorama. The default position is INLINE_END_BLOCK_START."
            ]

    let PanControlOptions =
        Config "google.maps.PanControlOptions"
        |+> Instance [
                "position" =@ ControlPosition
                |> WithComment "Position id. Used to specify the position of the control on the map. The default position is INLINE_END_BLOCK_END."
            ]

    let RotateControlOptions =
        Config "google.maps.RotateControlOptions"
        |+> Instance [
                "position" =@ ControlPosition
                |> WithComment "Position id. Used to specify the position of the control on the map. The default position is INLINE_END_BLOCK_END."
            ]

    let ScaleControlStyle =
        Class "google.maps.ScaleControlStyle"
        |+> Static [
            "DEFAULT" =? TSelf
            |> WithComment "The standard scale control."
        ]

    let ScaleControlOptions =
        Config "google.maps.ScaleControlOptions"
        |+> Instance [
                "style" =@ ScaleControlStyle
                |> WithComment "Style id. Used to select what style of scale control to display."
            ]

    let StreetViewSource =
        Class "google.maps.StreetViewSource"
        |+> Static [
            "DEFAULT" =? TSelf
            |> WithComment "Uses the default sources of Street View, searches will not be limited to specific sources."

            "GOOGLE" =? TSelf
            |> WithComment "Limits Street View searches to official Google collections."

            "OUTDOOR" =? TSelf
            |> WithComment "Limits Street View searches to outdoor collections. Indoor collections are not included in search results. Note also that the search only returns panoramas where it's possible to determine whether they're indoors or outdoors. For example, PhotoSpheres are not returned because it's unknown whether they are indoors or outdoors."
        ]

    let StreetViewControlOptions =
        Config "google.maps.StreetViewControlOptions"
        |+> Instance [
                "position" =@ ControlPosition
                |> WithComment "Position id. Used to specify the position of the control on the map. The default position is embedded within the navigation (zoom and pan) controls. If this position is empty or the same as that specified in the zoomControlOptions or panControlOptions, the Street View control will be displayed as part of the navigation controls. Otherwise, it will be displayed separately."

                "sources" =@ T<System.Collections.Generic.IEnumerable<_>>.[StreetViewSource]
                |> WithComment "Specifies the sources of panoramas to search. This allows a restriction to search for just official Google panoramas for example. Setting multiple sources will be evaluated as the intersection of those sources. Note: the StreetViewSource.OUTDOOR source is not supported at this time. Default: [StreetViewSource.DEFAULT]"
            ]

    let ZoomControlOptions =
        Config "google.maps.ZoomControlOptions"
        |+> Instance [
                "position" =@ ControlPosition
                |> WithComment "Position id. Used to specify the position of the control on the map. The default position is INLINE_END_BLOCK_END."
            ]

