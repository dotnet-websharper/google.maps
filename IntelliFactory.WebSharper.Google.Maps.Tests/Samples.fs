// $begin{copyright}
//
// This file is confidential and proprietary.
//
// Copyright (c) IntelliFactory, 2004-2011.
//
// All rights reserved.  Reproduction or use in whole or in part is
// prohibited without the written consent of the copyright holder.
//-----------------------------------------------------------------
// $end{copyright}

(*! Samples !*)
    (*![
        <p>Multiple samples for the Google Maps Extenstion. It demonstrates how to:</p>
        <ul>
            <li>Create simple maps.</li>
            <li>Add markers and behavior.</li>
        </ul>
        <h2>Source Code Explained</h2>
     ]*)

namespace IntelliFactory.WebSharper.Google.Maps.Tests

open IntelliFactory.WebSharper

module Util =

    open IntelliFactory.WebSharper.JavaScript

    [<Inline "alert($msg)">]
    let Alert (msg: obj) : unit = X

    [<Inline "setTimeout($f, $ms)">]
    let SetTimeout (f: unit -> unit) (ms: int) = X

module SamplesInternals =

    open IntelliFactory.WebSharper.JavaScript
    open IntelliFactory.WebSharper.Google.Maps
    open IntelliFactory.WebSharper.Html.Client
    open IntelliFactory.WebSharper.JQuery

    [<JavaScript>]
    let Sample buildMap =
        Div [Attr.Style "padding-bottom:20px; width:500px; height:300px;"]
        |>! OnAfterRender (fun mapElement ->
            let center = new LatLng(37.4419, -122.1419)
            let options = new MapOptions(center, 8)
            let map = new Google.Maps.Map(mapElement.Body, options)
            buildMap map)

    [<JavaScript>]
    let SimpleMap() =
        Sample <| fun (map: Map) ->
            let latLng = new LatLng(-34.397, 150.644)
            let options = new MapOptions(latLng, 8)
            map.SetOptions options

    [<JavaScript>]
    let PanTo() =
        Sample <| fun map ->

            let center = new LatLng(37.4419, -122.1419)
            let options = new MapOptions(center, 8)
            map.SetOptions options
            let move () = map.PanTo(new LatLng(37.4569, -122.1569))
            // Window.SetTimeout(move, 5000)
            Util.SetTimeout move 5000

    [<JavaScript>]
    let RandomMarkers() =
        Sample <| fun map ->

            let addMarkers (_:obj) =
                // bounds is only available in the "bounds_changed" event.
                let bounds = map.GetBounds()

                let sw = bounds.GetSouthWest()
                let ne = bounds.GetNorthEast()
                let lngSpan = ne.Lng() - sw.Lng()
                let latSpan = ne.Lat() - sw.Lat()
                let rnd = Math.Random
                for i in 1 .. 10 do
                    let point = new LatLng(sw.Lat() + (latSpan * rnd()),
                                           sw.Lng() + (lngSpan * rnd()))
                    let markerOptions = new MarkerOptions(point)
                    markerOptions.Map <- map
                    new Marker(markerOptions) |> ignore

            Event.AddListener(map, "bounds_changed", addMarkers) |> ignore

    [<JavaScript>]
    let InfoWindow() =
        Sample <| fun map ->
            let center = map.GetCenter()
            let helloWorldElement = Span [Text "Hello World"]
            let iwOptions = new InfoWindowOptions()
            iwOptions.Content <- Choice1Of2 helloWorldElement.Body
            iwOptions.Position <- center
            let iw = new InfoWindow(iwOptions)
            iw.Open(map)

    [<JavaScript>]
    let Controls() =
        Sample <| fun map ->
            let center = new LatLng(37.4419, -122.1419)
            let options = new MapOptions(center, 8)
            options.DisableDefaultUI <- true
            let ncOptions = new NavigationControlOptions()
            ncOptions.Style <- NavigationControlStyle.ZOOM_PAN
//            options.NavigationControlOptions <- ncOptions
//            options.NavigationControl <- true
            map.SetOptions options

    [<JavaScript>]
    let SimpleDirections() =
        Sample <| fun map ->
            let directionsService = new DirectionsService()
            let directionsDisplay = new DirectionsRenderer();
            map.SetCenter(new LatLng(41.850033, -87.6500523))
            map.SetZoom 7
            map.SetMapTypeId MapTypeId.ROADMAP
            let a = DirectionsRendererOptions()
            directionsDisplay.SetMap(map)
            let mapDiv = map.GetDiv()
            let dirPanel = Div [ Attr.Name "directionsDiv"]
            let j = JQuery.Of(mapDiv)
            j.After(dirPanel.Dom).Ignore
            directionsDisplay.SetPanel dirPanel.Dom
            let calcRoute () =
                let start = "chicago, il"
                let destination  = "st louis, mo"
                let request = new DirectionsRequest(start, destination, TravelMode.DRIVING)
                directionsService.Route(request, fun (result, status) ->
                    if status = DirectionsStatus.OK then
                        directionsDisplay.SetDirections result)
            calcRoute ()

    [<JavaScriptAttribute>]
    let DirectionsWithWaypoints() =
        Sample <| fun map ->
            let directionsService = new DirectionsService()
            let directionsDisplay = new DirectionsRenderer();
            map.SetCenter(new LatLng(41.850033, -87.6500523))
            map.SetZoom 7
            map.SetMapTypeId MapTypeId.ROADMAP
            let a = DirectionsRendererOptions()
            directionsDisplay.SetMap(map)
            let mapDiv = map.GetDiv()
            let dirPanel = Div [Attr.Name "directionsDiv"]
            let j = JQuery.Of mapDiv
            j.After(dirPanel.Dom).Ignore
            directionsDisplay.SetPanel dirPanel.Dom
            let calcRoute () =
                let start = "chicago, il"
                let destination  = "st louis, mo"

                let request = new DirectionsRequest(start, destination, TravelMode.DRIVING)
                let waypoints =
                    [|"champaign, il"
                      "decatur, il"  |]
                    |> Array.map (fun x ->
                                    let wp = new DirectionsWaypoint()
                                    wp.Location <- Location(x)
                                    wp)

                request.Waypoints <- waypoints
                directionsService.Route(request, fun (result, status) ->
                    if status = DirectionsStatus.OK then
                        directionsDisplay.SetDirections result)
            calcRoute ()

    [<JavaScriptAttribute>]
    /// Since it's not available in v3. We make it using the ImageMapType
    /// Taken from: http://code.google.com/p/gmaps-samples-v3/source/browse/trunk/planetary-maptypes/planetary-maptypes.html?r=206
    let Moon() =
        Sample <| fun map ->
            //Normalizes the tile URL so that tiles repeat across the x axis (horizontally) like the
            //standard Google map tiles.
            let getHorizontallyRepeatingTileUrl(coord: Point, zoom: int, urlfunc: (Point * int -> string)) : string =
                let mutable x = coord.X
                let y = coord.Y
                let tileRange = float (1 <<< zoom)
                if (y < 0. || y >= tileRange)
                then null
                else
                    if x < 0. || x >= tileRange
                    then x <- (x % tileRange + tileRange) % tileRange
                    urlfunc(new Point(x, y), zoom)

            let itOptions = new ImageMapTypeOptions()

            itOptions.GetTileUrl <-
                (fun _ (coord, zoom) ->
                    getHorizontallyRepeatingTileUrl (coord, zoom,
                        (fun (coord, zoom) ->
                            let bound = Math.Pow(float 2, float zoom)
                            ("http://mw1.google.com/mw-planetary/lunar/lunarmaps_v1/clem_bw/"
                              + (string zoom) + "/" + (string coord.X) + "/" + (string (bound - coord.Y - 1.) + ".jpg")))))

            itOptions.TileSize <- new Size(256., 256.)
            itOptions.MaxZoom <- 9
            itOptions.MinZoom <- 0
            itOptions.Name <- "Moon"

            let it = new ImageMapType(itOptions)
            let center = new LatLng(0., 0.)
            let mapIds = [| box "Moon" |> unbox |]
            let mapControlOptions =
                let mco = new MapTypeControlOptions()
                mco.Style <- MapTypeControlStyle.DROPDOWN_MENU
                mco.MapTypeIds <- mapIds
                mco

            let options = new MapOptions(center, 0, MapTypeId = mapIds.[0])
            options.MapTypeControlOptions <- mapControlOptions
            map.SetOptions options
            // FIXME
            // map.MapTypes.Set("Moon", it)
            // TODO: Add the credit part
            ()

    [<JavaScript>]
    let Weather() =
        Sample <| fun map ->
            let images = [| "sun"; "rain"; "snow"; "storm" |]
            let getWeatherIcon () =
                let i = int <| Math.Floor(float images.Length * Math.Random())
                Icon(Url = ("http://gmaps-utility-library.googlecode.com/svn/trunk/markermanager/release/examples/images/"
                            + images.[i] + ".png"))

            let addMarkers (_:obj) =
                let bounds = map.GetBounds()
                let sw = bounds.GetSouthWest()
                let ne = bounds.GetNorthEast()
                let lngSpan = ne.Lng() - sw.Lng()
                let latSpan = ne.Lat() - sw.Lat()
                let rnd = Math.Random
                for i in 1..10 do
                    let point = new LatLng(sw.Lat() + (latSpan * rnd()),
                                           sw.Lng() + (lngSpan * rnd()))
                    let markerOptions = new MarkerOptions(point)
                    markerOptions.Icon <- getWeatherIcon()
                    markerOptions.Map <- map
                    new Marker(markerOptions) |> ignore

            Event.AddListener(map, "bounds_changed", addMarkers) |> ignore

// Not supported in v3.
//
//    [<JavaScript>]
//    let IconSize() =
//        Sample <| fun map ->
//
//            let addMarkers (_:obj) =
//                let bounds = map.GetBounds()
//                let sw = bounds.GetSouthWest()
//                let ne = bounds.GetNorthEast()
//                let lngSpan = ne.Lng() - sw.Lng()
//                let latSpan = ne.Lat() - sw.Lat()
//                let rnd = JMath.Random
//                for i in 1..10 do
//                    let point = new LatLng(sw.Lat() + (latSpan * rnd()),
//                                           sw.Lng() + (lngSpan * rnd()))
//                    let markerOptions = new MarkerOptions(point)
//                    markerOptions.Map <- map
//                    new Marker(markerOptions) |> ignore
//
//            Event.AddListener(map, "bounds_changed", addMarkers) |> ignore

    [<JavaScript>]
    let SimplePolygon() =
        Sample <| fun map ->
            map.SetCenter(new LatLng(37.4419, -122.1419))
            map.SetZoom(13)
            let polygon = new Polygon()
            let coords = [| new LatLng(37.4419, -122.1419)
                            new LatLng(37.4519, -122.1519)
                            new LatLng(37.4419, -122.1319)
                            new LatLng(37.4419, -122.1419) |]
            polygon.SetPath coords
            polygon.SetMap map

    [<JavaScript>]
    let StreetView() =
        Sample <| fun map ->
            let fenwayPark = new LatLng(42.345573, -71.098623)
            map.SetCenter(fenwayPark)
            map.SetZoom(15)
            let marker = new Marker()
            marker.SetPosition fenwayPark
            marker.SetMap map
            let options = new MapOptions(fenwayPark, 14)
            options.StreetViewControl <- true
            map.SetOptions options

    [<JavaScript>]
    let PrimitiveEvent () =
        Sample <| fun map ->
            let clickAction (_:obj) = Util.Alert "Map Clicked!" // Window.Alert "Map Clicked!"
            Event.AddListener(map, "click", clickAction)
            |> ignore

    [<JavaScript>]
    let SimplePolyline() =
        Sample <| fun map ->
            let coords = [| new LatLng(37.4419, -122.1419)
                            new LatLng(37.4519, -122.1519)|]
            let polylineOptions = new PolylineOptions()
            polylineOptions.StrokeColor <- "#ff0000"
            polylineOptions.Path <- coords
            polylineOptions.Map <- map
            new Polyline(polylineOptions)
            |> ignore

    [<JavaScript>]
    let Samples () =
        Div [
            H1 [Text "Google Maps Samples"]
            SimpleMap ()
            PanTo ()
            RandomMarkers ()
            InfoWindow ()
            Controls ()
            SimpleDirections ()
            DirectionsWithWaypoints ()
            Moon ()
            Weather ()
            SimplePolygon ()
            StreetView ()
            PrimitiveEvent ()
            SimplePolyline ()
            H1 [Text "HeatMaps"]
            Div [HeatMapSample.Sample()]
        ]

[<Sealed>]
type Samples() =
    inherit Web.Control()

    [<JavaScript>]
    override this.Body = SamplesInternals.Samples () :> _


open IntelliFactory.WebSharper.Sitelets

type Action = | Index

module Site =

    open IntelliFactory.WebSharper.Html.Server

    let HomePage =
        Content.PageContent <| fun ctx ->
            { Page.Default with
                Title = Some "WebSharper Google Maps Tests"
                Body = [Div [new Samples()]] }

    let Main = Sitelet.Content "/" Index HomePage

[<Sealed>]
type Website() =
    interface IWebsite<Action> with
        member this.Sitelet = Site.Main
        member this.Actions = [Action.Index]

[<assembly: Website(typeof<Website>)>]
do ()
