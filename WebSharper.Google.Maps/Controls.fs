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
        Pattern.EnumStrings "google.maps.ControlPosition" [
            "BLOCK_END_INLINE_CENTER"
            "BLOCK_END_INLINE_END"
            "BLOCK_END_INLINE_START"
            "BLOCK_START_INLINE_CENTER"
            "BLOCK_START_INLINE_END"
            "BLOCK_START_INLINE_START"
            "BOTTOM_CENTER"
            "BOTTOM_LEFT"
            "BOTTOM_RIGHT"
            "INLINE_END_BLOCK_CENTER"
            "INLINE_END_BLOCK_END"
            "INLINE_END_BLOCK_START"
            "INLINE_START_BLOCK_CENTER"
            "INLINE_START_BLOCK_END"
            "INLINE_START_BLOCK_START"
            "LEFT_BOTTOM"
            "LEFT_CENTER"
            "LEFT_TOP"
            "RIGHT_BOTTOM"
            "RIGHT_CENTER"
            "RIGHT_TOP"
            "TOP_CENTER"
            "TOP_LEFT"
            "TOP_RIGHT"
        ]

    let FullscreenControlOptions =
        Config "google.maps.FullscreenControlOptions"
            []
            [
                "position", ControlPosition.Type
            ]

    let MapTypeControlStyle =
        Pattern.EnumStrings "google.maps.MapTypeControlStyle" [
            "DEFAULT"
            "DROPDOWN_MENU"
            "HORIZONTAL_BAR"
        ]

    let MapTypeControlOptions =
        Config "google.maps.MapTypeControlOptions"
            []
            [
                "mapTypeIds", !|T<string>
                "position", ControlPosition.Type
                "style", MapTypeControlStyle.Type
            ]

    let MotionTrackingControlOptions =
        Config "google.maps.MotionTrackingControlOptions"
            []
            [
                "position", ControlPosition.Type
            ]

    let PanControlOptions =
        Config "google.maps.PanControlOptions"
            []
            [
                "position", ControlPosition.Type
            ]

    let RotateControlOptions =
        Config "google.maps.RotateControlOptions"
            []
            [
                "position", ControlPosition.Type
            ]

    let ScaleControlStyle =
        Pattern.EnumStrings "google.maps.ScaleControlStyle" [
            "DEFAULT"
        ]

    let ScaleControlOptions =
        Config "google.maps.ScaleControlOptions"
            []
            [
                "style", ScaleControlStyle.Type
            ]

    let StreetViewSource =
        Pattern.EnumStrings "google.maps.StreetViewSource" [
            "default"
            "google"
            "outdoor"
        ]

    let StreetViewControlOptions =
        Config "google.maps.StreetViewControlOptions"
            []
            [
                "position", ControlPosition.Type
                "sources", !|StreetViewSource.Type
            ]

    let ZoomControlOptions =
        Config "google.maps.ZoomControlOptions"
            []
            [
                "position", ControlPosition.Type
            ]

    let CameraControlOptions =
        Config "google.maps.CameraControlOptions"
            []
            [
                "position", ControlPosition.Type
            ]

