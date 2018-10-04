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

module WebSharper.Google.Maps.Tests.HeatMapSample

open WebSharper
open WebSharper.JavaScript
open WebSharper.Html.Client
open WebSharper.JQuery
open WebSharper.Google.Maps

type TaxiData =
    {
        coordinates : array<float * float>
    }

[<JavaScript>]
let Draw (div: Dom.Element) (rawData: array<double*double>) : unit =
    let taxiData = rawData |> Array.map (fun (lat, lng) -> new LatLng(lat, lng))
    let pointArray = MVCArray(taxiData);
    JS.Window?pointArray <- pointArray
    let map =
        let center = LatLng(57.6414, 12.0403)
        let opts = MapOptions(center, 13)
        Map(div, opts)
    let heatmap =
        let opts = new HeatmapLayerOptions(pointArray)
        opts.Radius <- 5
        opts.Opacity <- 0.6
        opts.Dissipating <- true
        opts.MaxIntensity <- 140.0
        opts.Gradient <-
            [|
                yield "rgba(255, 255, 110, 0)"
                for i in 1 .. 20 do
                    yield "rgba(255, 0, 0, 255)"
            |]
        HeatmapLayer(opts)
    heatmap.SetMap(map)

[<JavaScript>]
let Sample () =
    Div [Attr.Style "height: 480px; width: 640px"]
    |>! OnAfterRender (fun self ->
        async {
            let! data =
                Async.FromContinuations(fun (ok, _, cancel) ->
                    JQuery.GetJSON("taxi.js", fun (data, _) ->
                        ok (As<TaxiData> data))
                    |> ignore)
            do Draw self.Dom data.coordinates
        }
        |> Async.Start)
