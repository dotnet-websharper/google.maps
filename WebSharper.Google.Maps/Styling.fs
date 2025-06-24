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
// Definitions for the Controls part of the API. See:
// http://developers.google.com/maps/documentation/javascript/reference
namespace WebSharper.Google.Maps.Definition

module Styling =

    open WebSharper.InterfaceGenerator
    open Notation
    open Places

    let FeatureType =
        Forward.FeatureType
        |+> Static [
            "ADMINISTRATIVE_AREA_LEVEL_1" =? TSelf
            |> WithComment "Indicates a first-order civil entity below the country level."

            "ADMINISTRATIVE_AREA_LEVEL_2" =? TSelf
            |> WithComment "Indicates a second-order civil entity below the country level."

            "COUNTRY" =? TSelf
            |> WithComment "Indicates the national political entity."

            "DATASET" =? TSelf
            |> WithComment "Indicates a third-party dataset."

            "LOCALITY" =? TSelf
            |> WithComment "Indicates an incorporated city or town political entity."

            "POSTAL_CODE" =? TSelf
            |> WithComment "Indicates a postal code as used to address postal mail within the country. Includes zip codes."

            "SCHOOL_DISTRICT" =? TSelf
            |> WithComment "Indicates a school district."
        ]

    let Feature =
        Interface "google.maps.Feature"
        |+> [
            "featureType" =@ FeatureType
            |> WithComment "FeatureType of this Feature."
        ]

    let FeatureStyleFunctionOptions =
        Interface "google.maps.FeatureStyleFunctionOptions"
        |+> [
            "feature" =@ Feature
            |> WithComment "Feature passed into the FeatureStyleFunction for styling."
        ]

    let FeatureStyleOptions =
        Interface "google.maps.FeatureStyleOptions"
        |+> [
            "fillColor" =@ T<string>
            |> WithComment "Hex RGB string (like \"#00FF00\" for green). Only applies to polygon geometries."

            "fillOpacity" =@ T<float>
            |> WithComment "The fill opacity between 0.0 and 1.0. Only applies to polygon geometries."

            "strokeColor" =@ T<string>
            |> WithComment "Hex RGB string (like \"#00FF00\" for green)."

            "strokeOpacity" =@ T<float>
            |> WithComment "The stroke opacity between 0.0 and 1.0. Only applies to line and polygon geometries."

            "strokeWeight" =@ T<int>
            |> WithComment "The stroke width in pixels. Only applies to line and polygon geometries."
        ]

    let FeatureStyleFunction =
        T<obj> -* FeatureStyleFunctionOptions ^-> FeatureStyleOptions

    let FeatureMouseEvent =
        Class "google.maps.FeatureMouseEvent"
        |=> Inherits Map.MapMouseEvent
        |+> Instance [
           "features" =@ Type.ArrayOf Feature
           |> WithComment "The Features at this mouse event."
        ]

    let FeatureLayer =
        Forward.FeatureLayer
        |+> [
              "featureType" =@ FeatureType
              |> WithComment "The FeatureType associated with this FeatureLayer."

              "isAvailable" =@ T<bool>
              |> WithComment "Whether this FeatureLayer is available, meaning whether Data-driven styling is available for this map (there is a map ID using vector tiles with this FeatureLayer enabled in the Google Cloud Console map style.) If this is false (or becomes false), styling on this FeatureLayer returns to default and events are not triggered."

              "datasetId" =@ T<string>
              |> WithComment "The Dataset ID for this FeatureLayer. Only present if the featureType is FeatureType.DATASET."

              "style" =@ FeatureStyleOptions + FeatureStyleFunction
              |> WithComment "The style of Features in the FeatureLayer. The style is applied when style is set. If your style function updates, you must set the style property again. A FeatureStyleFunction must return consistent results when it is applied over the map tiles, and should be optimized for performance. Asynchronous functions are not supported. If you use a FeatureStyleOptions, all features of that layer will be styled with the same FeatureStyleOptions. Set the style to null to remove the previously set style. If this FeatureLayer is not available, setting style does nothing and logs an error."

              // METHODS
              "addListener" => (T<string> * Function) ^-> Events.MapsEventListener
              |> WithComment "Adds the given listener function to the given event name. Returns an identifier for this listener that can be used with event.removeListener."

              // EVENTS
              "click" =@ T<obj> -* FeatureMouseEvent ^-> T<unit>
              |> WithComment "This event is fired when the FeatureLayer is clicked."

              "mousemove"=@ T<obj> -* FeatureMouseEvent ^-> T<unit>
              |> WithComment "This event is fired when the user's mouse moves over the FeatureLayer."
        ]


    let PlaceFeature =
        Interface "google.maps.PlaceFeature"
        |=> Extends [Feature]
        |+> [
            "placeId" =@ T<string>

            "fetchPlace" => T<unit> ^-> Promise[Place]
            |> WithComment "Fetches a Place for this PlaceFeature. In the resulting Place object, the id and the displayName properties will be populated. The display name will be in the language the end user sees on the map. (Additional fields can be subsequently requested via Place.fetchFields() subject to normal Places API enablement and billing.) Do not call this from a FeatureStyleFunction since only synchronous FeatureStyleFunctions are supported. The promise is rejected if there was an error fetching the Place."
        ]


    let DatasetFeature =
        Interface "google.maps.DatasetFeature"
        |=> Extends [Feature]
        |+> [
            "datasetAttributes" =@ T<System.Collections.Generic.Dictionary<_,_>>
            "datasetId" =@ T<string>
            |> WithComment "Dataset id of the dataset that this feature belongs to."
        ]
