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

module WebGL =

    open WebSharper.InterfaceGenerator
    open Base
    open Notation
    module M = WebSharper.Google.Maps.Definition.Map

    let CameraParams =
        Config "google.maps.CameraParams"
            []
            [
                "center", Base.LatLngLiteral + Base.LatLng
                "heading", T<float>
                "tilt", T<float>
                "zoom", T<float>
            ]

    let CoordinateTransformer =
        Class "google.maps.CoordinateTransformer"
        |+> Instance [
            "fromLatLngAltitude" => (Base.LatLngAltitude + Base.LatLngAltitudeLiteral) * !?Float32Array * !?Float32Array ^-> T<unit>

            "getCameraParams" => T<unit> ^-> CameraParams
        ]

    let WebGLDrawOptions =
        Config "google.maps.WebGLDrawOptions"
            [
                "gl", WebGLRenderingContext
                "transformer", CoordinateTransformer.Type
            ]
            []

    let WebGLStateOptions =
        Config "google.maps.WebGLStateOptions"
            [
                "gl", WebGLRenderingContext
            ]
            []

    let WebGLOverlayView =
        Class "google.maps.WebGLOverlayView"
        |=> Inherits MVC.MVCObject
        |+> Static [
            Constructor T<unit>
        ]
        |+> Instance [
            "getMap" => T<unit> ^-> M.Map

            "onAdd" => T<unit -> unit>
            |> WithComment "Implement this method to fetch or create intermediate data structures before the overlay is drawn that don’t require immediate access to the WebGL rendering context. This method must be implemented to render."

            "onContextLost" => T<unit -> unit>
            |> WithComment "This method is called when the rendering context is lost for any reason, and is where you should clean up any pre-existing GL state, since it is no longer needed."

            "onContextRestored" => WebGLStateOptions ^-> T<unit>
            |> WithComment "This method is called once the rendering context is available. Use it to initialize or bind any WebGL state such as shaders or buffer objects."

            "onDraw" => WebGLDrawOptions ^-> T<unit>
            |> WithComment "Implement this method to draw WebGL content directly on the map. Note that if the overlay needs a new frame drawn then call WebGLOverlayView.requestRedraw."

            "onRemove" => T<unit -> unit>
            |> WithComment "This method is called when the overlay is removed from the map with WebGLOverlayView.setMap(null), and is where you should remove all intermediate objects. This method must be implemented to render."

            "onStateUpdate" => WebGLStateOptions ^-> T<unit>
            |> WithComment "Implement this method to handle any GL state updates outside of the render animation frame."

            "requestRedraw" => T<unit -> unit>
            |> WithComment "Triggers the map to redraw a frame."

            "requestStateUpdate" => T<unit -> unit>
            |> WithComment "Triggers the map to update GL state."

            "setMap" => M.Map ^-> T<unit>
            |> WithComment "Adds the overlay to the map."
        ]

