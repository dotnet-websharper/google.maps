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
/// Forward type declarations.
module WebSharper.Google.Maps.Forward

open WebSharper.InterfaceGenerator

let Map = Class "google.maps.Map"

let MapTypeId =
    Pattern.EnumInlines "MapTypeId" [
        // This map type displays a transparent layer of major streets on satellite images.
        "HYBRID", "google.maps.MapTypeId.HYBRID"
        // This map type displays a normal street map.
        "ROADMAP", "google.maps.MapTypeId.ROADMAP"
        // This map type displays satellite images.
        "SATELLITE", "google.maps.MapTypeId.SATELLITE"
        // This map type displays maps with physical features such as terrain and vegetation.
        "TERRAIN", "google.maps.MapTypeId.TERRAIN"
    ]
