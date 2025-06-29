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
// Definitions for the Map part of the API. See:
// http://developers.google.com/maps/documentation/javascript/reference
namespace WebSharper.Google.Maps.Definition

module LocalContext =

    open WebSharper.InterfaceGenerator
    open Base
    open Notation
    open Places
    module M = WebSharper.Google.Maps.Definition.Map

    let MapDirectionsOptionsLiteral =
        Interface "google.maps.localContext.MapDirectionsOptionsLiteral"
        |+> [
            "origin" =@ LatLng + LatLngLiteral
            |> WithComment "Origin for directions and distance."
        ]

    let MapDirectionsOptions =
        Class "google.maps.localContext.MapDirectionsOptions"
        |=> Implements [MapDirectionsOptionsLiteral]
        |+> Instance [
            "addListener" => T<string> * T<WebSharper.JavaScript.Function> ^-> Events.MapsEventListener
            |> WithComment "Adds the given listener function to the given event name."
        ]

    let PinOptions =
        Interface "google.maps.localContext.PinOptions"
        |+> [
            "background" =@ T<string>
            |> WithComment "The color of the icon's shape, can be any valid CSS color."

            "glyphColor" =@ T<string>
            |> WithComment "The color of the icon's glyph, can be any valid CSS color."

            "scale" =@ T<int>
            |> WithComment "The scale of the icon. The value is absolute, not relative to the default sizes in each state."
        ]

    let PlaceChooserLayoutMode = 
        Pattern.EnumStrings "google.maps.localContext.PlaceChooserLayoutMode" ["HIDDEN"; "SHEET"]

    let PlaceChooserPosition = 
        Pattern.EnumStrings "google.maps.localContext.PlaceChooserPosition" ["BLOCK_END"; "INLINE_END"; "INLINE_START"]

    let PlaceChooserViewSetupOptions =
        Interface "google.maps.localContext.PlaceChooserViewSetupOptions"
        |+> [
            "layoutMode" =@ PlaceChooserLayoutMode

            "position" =@ PlaceChooserPosition
            |> WithComment "Ignored when layoutMode:HIDDEN. If not passed, a position will be determined automatically based on the layoutMode."
        ]

    let PlaceDetailsLayoutMode = 
        Pattern.EnumStrings "google.maps.localContext.PlaceDetailsLayoutMode" ["INFO_WINDOW"; "SHEET"]

    let PlaceDetailsPosition = 
        Pattern.EnumStrings "google.maps.localContext.PlaceDetailsPosition" ["INLINE_END"; "INLINE_START"]

    let PlaceDetailsViewSetupOptions =
        Interface "google.maps.localContext.PlaceDetailsViewSetupOptions"
        |+> [
            "hidesOnMapClick" =@ T<bool>

            "layoutMode" =@ PlaceDetailsLayoutMode

            "position" =@ PlaceDetailsPosition
            |> WithComment "Ignored when layoutMode:INFO_WINDOW. If not passed, a position will be determined automatically based on the layoutMode."
        ]

    let PlaceTypePreference = 
        Config 
            "google.maps.localContext.PlaceTypePreference" 
            ["type", T<string>] 
            ["weight", T<int>]

    let PinOptionsSetupObject = 
        Config 
            "google.maps.localContext.PinOptionsSetupObject"
            [
                "isHighlighted", T<string>
                "isSelected", T<string>
            ] 
            []

    let PlaceChooserViewSetupObject = 
        Config 
            "google.maps.localContext.PlaceChooserViewSetupObject"
            [
                "defaultLayoutMode", PlaceChooserLayoutMode.Type
                "defaultPosition", PlaceChooserPosition.Type
            ] 
            []

    let PlaceDetailsViewSetup = 
        Config 
            "google.maps.localContext.PlaceDetailsViewSetup"
            [
                "defaultLayoutMode", PlaceDetailsLayoutMode.Type
                "defaultPosition", PlaceDetailsPosition.Type
            ] 
            []