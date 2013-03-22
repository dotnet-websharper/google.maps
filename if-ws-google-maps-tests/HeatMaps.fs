// $begin{copyright}
//
// This file is confidential and proprietary.
//
// Copyright (c) IntelliFactory, 2004-2013.
//
// All rights reserved.  Reproduction or use in whole or in part is
// prohibited without the written consent of the copyright holder.
//-----------------------------------------------------------------
// $end{copyright}

module IntelliFactory.WebSharper.Google.Maps.Tests.HeatMapSample

open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.Html
open IntelliFactory.WebSharper.Google.Maps

[<Remote>]
let GetTaxiData () : Async<array<float * float>> =
    async {
        let json = System.Web.HttpContext.Current.Server.MapPath("~/taxi.json")
        let value = Core.Json.Parse(System.IO.File.ReadAllText(json))
        match value with
        | Core.Json.Object ["coordinates", Core.Json.Array values] ->
            return [|
                for v in values do
                    match v with
                    | Core.Json.Array [Core.Json.Number a; Core.Json.Number b] ->
                        yield (double a, double b)
                    | _ -> ()
            |]
        | _ ->
            return [||]
    }

[<JavaScript>]
let Draw (div: Dom.Element) (rawData: array<double*double>) : unit =
    let taxiData = rawData |> Array.map (fun (lat, lng) -> new LatLng(lat, lng))
    let pointArray = MVCArray(taxiData);
    JavaScript.Global?pointArray <- pointArray
    let map =
        let center = LatLng(57.6414, 12.0403)
        let opts = MapOptions(center, MapTypeId.ROADMAP, 13)
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
            let! data = GetTaxiData ()
            do Draw self.Dom data
        }
        |> Async.Start)
