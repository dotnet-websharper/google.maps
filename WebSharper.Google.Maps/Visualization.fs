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
// See "Visualization Library" at
// at http://developers.google.com/maps/documentation/javascript/reference
namespace WebSharper.Google.Maps.Definition

module Visualization =

    open WebSharper.InterfaceGenerator
    open Notation
    module M = WebSharper.Google.Maps.Definition.Map

    let WeightedLocation =
        Class "google.maps.visualization.WeightedLocation"
        |+> Instance [
            "location" =@ Base.LatLng
            "weight" =@ T<float>
        ]

    let private HeatmapLayerProperties =
        [
            "dissipating", T<bool>
            "gradient", Type.ArrayOf T<string>
            "map", M.Map.Type
            "maxIntensity", T<float>
            "opacity", T<float>
            "radius", T<int>
        ]

    let loc = Base.LatLng + WeightedLocation

    let HeatmapLayerOptions =
        Pattern.Config "HeatmapLayerOptions" {
            Required = ["data", MVC.MVCArray.[loc] + !|(loc)]
            Optional = HeatmapLayerProperties
        }

    let HeatmapLayer =        
        Class "google.maps.visualization.HeatmapLayer"
        |=> Inherits MVC.MVCObject
        |+> Instance [

                "getData" => T<unit> ^-> MVC.MVCArray.[loc]
                |> WithSourceName "GetData"
                |> WithComment "Returns the data points currently displayed by this heatmap."

                "getMap" => T<unit> ^-> M.Map

                "setData" => (MVC.MVCArray.[loc] + Type.ArrayOf loc) ^-> T<unit>
                |> WithComment "Sets the data points to be displayed by this heatmap."

                "setMap" => M.Map ^-> T<unit>
                |> WithComment "Renders the heatmap on the specified map. If map is set to null, the heatmap will be removed."

                "setOptions" => !? HeatmapLayerOptions ^-> T<unit>

            ]
        |+> Instance [
                for (k, v) in HeatmapLayerProperties do
                    yield (k =@ v) :> _
            ]
        |+> Static [
                Ctor [!? HeatmapLayerOptions?options]
            ]
        |> ObsoleteWithMessage "Deprecated: The Heatmap Layer functionality in the Maps JavaScript API is no longer supported. This API was deprecated in May 2025 and will be made unavailable in a later version of the Maps JavaScript API, releasing in May 2026. For more info, see https://developers.google.com/maps/deprecations)."