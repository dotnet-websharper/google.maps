
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
// Definitions for the Places part of the API. See:
// http://developers.google.com/maps/documentation/javascript/reference
namespace WebSharper.Google.Maps.Definition

open Base
open MVC
open Specification
open WebSharper.InterfaceGenerator

module Geometry = 
    let Encoding =
        Class "google.maps.geometry.encoding"
        |+> Static [
            "decodePath" => T<string> ^-> !| LatLng
            "encodePath" => (!| (LatLng + LatLngLiteral) + MVC.MVCArray.[LatLng + LatLngLiteral]) ^-> T<string>
        ]      

    let Poly =
        Class "google.maps.geometry.poly"
        |+> Static [
            "containsLocation" => (LatLng + LatLngLiteral) * Polygon ^-> T<bool>
            "isLocationOnEdge" => (LatLng + LatLngLiteral) * (Polygon + Polyline) * !? T<float> ^-> T<bool>
        ]

    let Spherical =

        let computeAreaType = !| (LatLng + LatLngLiteral) + MVC.MVCArray.[LatLng + LatLngLiteral] + Circle + CircleLiteral + LatLngBounds + LatLngBoundsLiteral

        Class "google.maps.geometry.spherical"
        |+> Static [
            "computeArea" => computeAreaType * !? T<float> ^-> T<float>
            "computeDistanceBetween" => (LatLng + LatLngLiteral) * (LatLng + LatLngLiteral) * !? T<float> ^-> T<float>
            "computeHeading" => (LatLng + LatLngLiteral) * (LatLng + LatLngLiteral) ^-> T<float>
            "computeLength" => (!| (LatLng + LatLngLiteral) + MVC.MVCArray.[LatLng + LatLngLiteral]) * !? T<float> ^-> T<float>
            "computeOffset" => (LatLng + LatLngLiteral) * T<float> * T<float> * !? T<float> ^-> LatLng
            "computeOffsetOrigin" => (LatLng + LatLngLiteral) * T<float> * T<float> * !? T<float> ^-> LatLng + T<obj>
            "computeSignedArea" => (!| (LatLng + LatLngLiteral) + MVC.MVCArray.[LatLng + LatLngLiteral]) * !? T<float> ^-> T<float>
            "interpolate" => (LatLng + LatLngLiteral) * (LatLng + LatLngLiteral) * T<float> ^-> LatLng
        ]
