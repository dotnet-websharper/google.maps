# WebSharper.Google.Maps

**Add interactive maps to your web applications! ...Without a single
line of JavaScript.**

The Google Map API enables you to create rich, interactive maps,
street views, route planning, and much more.  Go and check out their
[homepage][gmaps].

With this WebSharper Extension you can develop
[WebSharper](http://websharper.com) applications that use the Google
Maps API using nothing but F# code.

[gmaps]: http://code.google.com/apis/maps/documentation/javascript

## Status

Current status of the code: the bindings mostly reflect older API,
with some of the modules ported to 3.11 - this is work in progress.

## Installation

This extension is compatible with [WebSharper
2.4](http://websharper.com).  To obtain the latest extension binaries,
install the `WebSharper.Google.Maps` [NuGet](http://nuget.org)
package, and use as you would a regular .NET library.

## Configuration

The Google maps API is a free service that is available to every page
that is free for its users. You can check the specific terms of
service [here][terms].

The version 3.0 of the Google Maps API does not require a key. The
extension will generate code similar to the following for your pages:

    <script type="text/javascript"
        src="http://maps.google.com/maps/api/js?sensor=true">

[terms]: http://code.google.com/apis/maps/terms.html

## Overview

This extension provides a set of classes almost identical to the ones
documented in the [Google Maps API v3][gmap-api].  When used in
WebSharper projects, these stub classes delegate the work to the
actual classes implemented in Google Maps API.

[gmap-api]: http://code.google.com/apis/maps/documentation/javascript

## A Simple Map

Creating a map typically involves 3 steps:

  * Create a container, an `Html` element that will hold the map
    object.

  * Initialize the map and attaching it to the container.

  * Set properties, wire events, add overlays or controls to the
    attached map.

The first two steps are independent of the functionality.  For this
you can create a simple helper function that receives a function that
takes as a parameter the initialized map:

    open IntelliFactory.WebSharper.Google

    [<JavaScript>]
    let Sample buildMap =
        Div [Attr.Style "padding-bottom:20px; width:500px; height:300px;"]
        |>! OnAfterRender (fun mapElement ->
            let center = new Maps.LatLng(37.4419, -122.1419)
            let options = new Maps.MapOptions(center, MapTypeId.ROADMAP, 8)
            let map = new Maps.Map(mapElement.Dom, options)
            buildMap map)

A `<div>` element is created at line 3.  The size of the `<div>` will
determine the size of the map.  You can set all the style properties
you want in this step, including padding or margins.

It is necessary to initialize the map using the `OnAfterRender` method
(line 4).  The Maps API requires that the element is already attached
to the DOM before initializing the map.  Doing the Map initialization
after the widget rendering makes sure that this condition holds.
