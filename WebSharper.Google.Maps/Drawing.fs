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
/// Definitions for the Drawing part of the API. See:
/// http://developers.google.com/maps/documentation/javascript/reference
module WebSharper.Google.Maps.Drawing

open WebSharper.InterfaceGenerator
open WebSharper.Google.Maps.Notation
open WebSharper.Google.Maps.Base
open WebSharper.Google.Maps.Specification

let OverlayType =
    Class "google.maps.drawing.OverlayType"
    |+> Static [
        "CIRCLE" =? TSelf
        |> WithComment "Specifies that the DrawingManager creates circles, and that the overlay given in the overlaycomplete event is a circle."

        "MARKER" =? TSelf
        |> WithComment "Specifies that the DrawingManager creates markers, and that the overlay given in the overlaycomplete event is a marker."

        "POLYGON" =? TSelf
        |> WithComment "Specifies that the DrawingManager creates polygons, and that the overlay given in the overlaycomplete event is a polygon."

        "POLYLINE" =? TSelf
        |> WithComment "Specifies that the DrawingManager creates polylines, and that the overlay given in the overlaycomplete event is a polyline."

        "RECTANGLE" =? TSelf
        |> WithComment "Specifies that the DrawingManager creates rectangles, and that the overlay given in the overlaycomplete event is a rectangle."
    ]

let DrawingControlOptions =
    Config "google.maps.drawing.DrawingControlOptions"
    |+> Instance [
        "drawingModes" =@ Type.ArrayOf OverlayType
        |> WithComment "The drawing modes to display in the drawing control, in the order in which they are to be displayed. The hand icon (which corresponds to the null drawing mode) is always available and is not to be specified in this array. Defaults to [MARKER, POLYLINE, RECTANGLE, CIRCLE, POLYGON]."

        "position" =@ Controls.ControlPosition
        |> WithComment "Position id. Used to specify the position of the control on the map. The default position is TOP_LEFT."
    ]

let DrawingManagerOptions =
    Config "google.maps.drawing.DrawingManagerOptions"
    |+> Instance [
        "circleOptions" =@ CircleOptions
        |> WithComment "Options to apply to any new circles created with this DrawingManager. The center and radius properties are ignored, and the map property of a new circle is always set to the DrawingManager's map."

        "drawingControl" =@ T<bool>
        |> WithComment "The enabled/disabled state of the drawing control. Defaults to true."

        "drawingControlOptions" =@ DrawingControlOptions
        |> WithComment "The display options for the drawing control."

        "drawingMode" =@ OverlayType
        |> WithComment "The DrawingManager's drawing mode, which defines the type of overlay to be added on the map. Accepted values are MARKER, POLYGON, POLYLINE, RECTANGLE, CIRCLE, or null. A drawing mode of null means that the user can interact with the map as normal, and clicks do not draw anything."

        "map" =@ Map.Map
        |> WithComment "The Map to which the DrawingManager is attached, which is the Map on which the overlays created will be placed."

        "markerOptions" =@ MarkerOptions
        |> WithComment "Options to apply to any new markers created with this DrawingManager. The position property is ignored, and the map property of a new marker is always set to the DrawingManager's map."

        "polygonOptions" =@ PolygonOptions
        |> WithComment "Options to apply to any new polygons created with this DrawingManager. The paths property is ignored, and the map property of a new polygon is always set to the DrawingManager's map."

        "polylineOptions" =@ PolylineOptions
        |> WithComment "Options to apply to any new polylines created with this DrawingManager. The path property is ignored, and the map property of a new polyline is always set to the DrawingManager's map."

        "rectangleOptions" =@ RectangleOptions
        |> WithComment "Options to apply to any new rectangles created with this DrawingManager. The bounds property is ignored, and the map property of a new rectangle is always set to the DrawingManager's map."
    ]

let DrawingManager =
    Class "google.maps.drawing.DrawingManager"
    |+> Static [
        Constructor !?DrawingManagerOptions
        |> WithComment "Creates a DrawingManager that allows users to draw overlays on the map, and switch between the type of overlay to be drawn with a drawing control."
    ]
    |+> Instance [
        "getDrawingMode" => T<unit> ^-> OverlayType
        |> WithComment "Returns the DrawingManager's drawing mode."

        "getMap" => T<unit> ^-> Map.Map
        |> WithComment "Returns the Map to which the DrawingManager is attached, which is the Map on which the overlays created will be placed."

        "setDrawingMode" => OverlayType ^-> T<unit>
        |> WithComment "Changes the DrawingManager's drawing mode, which defines the type of overlay to be added on the map. Accepted values are MARKER, POLYGON, POLYLINE, RECTANGLE, CIRCLE, or null. A drawing mode of null means that the user can interact with the map as normal, and clicks do not draw anything."

        "setMap" => Map.Map ^-> T<unit>
        |> WithComment "Attaches the DrawingManager object to the specified Map."

        "setOptions" => DrawingManagerOptions ^-> T<unit>
        |> WithComment "Sets the DrawingManager's options."
    ]
