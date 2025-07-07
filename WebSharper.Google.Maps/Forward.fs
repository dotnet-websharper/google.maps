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
// Forward type declarations.
namespace WebSharper.Google.Maps.Definition

module Forward =

    open WebSharper.InterfaceGenerator

    let FeatureType =
        Class "google.maps.FeatureType"

    let FeatureLayer =
        Interface "google.maps.FeatureLayer"

    let Data =
        Class "google.maps.Data"

    let Map = Class "google.maps.Map"

    let AddressValidation = Class "google.maps.addressValidation.AddressValidation"

    let Place =
        Class "google.maps.places.Place"

    let MapTypeId =
        Pattern.EnumInlines "google.maps.MapTypeId" [
            // This map type displays a transparent layer of major streets on satellite images.
            "HYBRID", "google.maps.MapTypeId.HYBRID"
            // This map type displays a normal street map.
            "ROADMAP", "google.maps.MapTypeId.ROADMAP"
            // This map type displays satellite images.
            "SATELLITE", "google.maps.MapTypeId.SATELLITE"
            // This map type displays maps with physical features such as terrain and vegetation.
            "TERRAIN", "google.maps.MapTypeId.TERRAIN"
        ]

    let PlacePlusCode =
        Notation.Config "google.maps.PlacePlusCode"
            [
                "global_code", T<string>
            ]
            [
                "compound_code", T<string>
            ]
