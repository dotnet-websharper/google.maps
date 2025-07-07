﻿// $begin{copyright}
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

module Errors = 
    open WebSharper.InterfaceGenerator
    open Specification

    let MapsNetworkErrorEndpoint =
        Pattern.EnumStrings "google.maps.MapsNetworkErrorEndpoint" [
            "DIRECTIONS_ROUTE"
            "DISTANCE_MATRIX"
            "ELEVATION_ALONG_PATH"
            "ELEVATION_LOCATIONS"
            "FLEET_ENGINE_GET_DELIVERY_VEHICLE"
            "FLEET_ENGINE_GET_TRIP"
            "FLEET_ENGINE_GET_VEHICLE"
            "FLEET_ENGINE_LIST_DELIVERY_VEHICLES"
            "FLEET_ENGINE_LIST_TASKS"
            "FLEET_ENGINE_LIST_VEHICLES"
            "FLEET_ENGINE_SEARCH_TASKS"
            "GEOCODER_GEOCODE"
            "MAPS_MAX_ZOOM"
            "PLACES_AUTOCOMPLETE"
            "PLACES_DETAILS"
            "PLACES_FIND_PLACE_FROM_PHONE_NUMBER"
            "PLACES_FIND_PLACE_FROM_QUERY"
            "PLACES_GATEWAY"
            "PLACES_GET_PLACE"
            "PLACES_LOCAL_CONTEXT_SEARCH"
            "PLACES_NEARBY_SEARCH"
            "PLACES_SEARCH_TEXT"
            "STREETVIEW_GET_PANORAMA"
        ]

    let RPCStatus =
        Pattern.EnumStrings "google.maps.RPCStatus" [
            "ABORTED"
            "ALREADY_EXISTS"
            "CANCELLED"
            "DATA_LOSS"
            "DEADLINE_EXCEEDED"
            "FAILED_PRECONDITION"
            "INTERNAL"
            "INVALID_ARGUMENT"
            "NOT_FOUND"
            "OK"
            "OUT_OF_RANGE"
            "PERMISSION_DENIED"
            "RESOURCE_EXHAUSTED"
            "UNAUTHENTICATED"
            "UNAVAILABLE"
            "UNIMPLEMENTED"
            "UNKNOWN"
        ]

    let MapsNetworkError =
        Class "google.maps.MapsNetworkError"
        |+> Instance [
            "code" =@ 
                DirectionsStatus 
                + DistanceMatrixStatus
                + ElevationStatus
                + GeocoderStatus
                + MaxZoomStatus
                + Places.PlacesServiceStatus
                + RPCStatus
                + StreetView.StreetViewStatus
            |> WithComment "Identifies the type of error produced by the API."

            "endpoint" =@ MapsNetworkErrorEndpoint
            |> WithComment "Represents the network service that responded with the error."
        ]

    let MapsRequestError =
        Class "google.maps.MapsRequestError"
        |=> Inherits MapsNetworkError

    let MapsServerError =
        Class "google.maps.MapsServerError"
        |=> Inherits MapsNetworkError

