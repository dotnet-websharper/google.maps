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
// Definitions for the Map part of the API. See:
// http://developers.google.com/maps/documentation/javascript/reference
namespace WebSharper.Google.Maps.Definition

module LocalContext =

    open WebSharper.InterfaceGenerator
    open Base
    open Notation
    open Places
    module M = WebSharper.Google.Maps.Definition.Map

    let MapDirectionsOptionsLiteral =
        Interface "google.maps.journeySharing.MapDirectionsOptionsLiteral"
        |+> [
            "origin" =@ LatLng + LatLngLiteral
            |> WithComment "Origin for directions and distance."
        ]

    let MapDirectionsOptions =
        Class "google.maps.journeySharing.MapDirectionsOptions"
        |=> Implements [MapDirectionsOptionsLiteral]
        |+> Instance [
            "addListener" => T<string> * T<WebSharper.JavaScript.Function> ^-> Events.MapsEventListener
            |> WithComment "Adds the given listener function to the given event name."
        ]

    let PinOptions =
        Interface "google.maps.journeySharing.PinOptions"
        |+> [
            "background" =@ T<string>
            |> WithComment "The color of the icon's shape, can be any valid CSS color."

            "glyphColor" =@ T<string>
            |> WithComment "The color of the icon's glyph, can be any valid CSS color."

            "scale" =@ T<int>
            |> WithComment "The scale of the icon. The value is absolute, not relative to the default sizes in each state."
        ]

    let PlaceChooserLayoutMode =
        Class "google.maps.journeySharing.PlaceChooserLayoutMode"
        |+> Static [
            "HIDDEN" =? TSelf
            |> WithComment "Place chooser is hidden."

            "SHEET" =? TSelf
            |> WithComment "Place chooser is shown as a sheet."
        ]

    let PlaceChooserPosition =
        Class "google.maps.journeySharing.PlaceChooserPosition"
        |+> Static [
            "BLOCK_END" =? TSelf
            |> WithComment "Place chooser is displayed on a line below the map extending to the end of the container."

            "INLINE_END" =? TSelf
            |> WithComment "Place chooser is displayed inline with the map at the end of the line. (In a left-to-right language this means that the place chooser is to the right of the map.)"

            "INLINE_START" =? TSelf
            |> WithComment "Place chooser is displayed inline with the map at the start of the line. (In a left-to-right language this means that the place chooser is to the left of the map.)"
        ]


    let PlaceChooserViewSetupOptions =
        Interface "google.maps.journeySharing.PlaceChooserViewSetupOptions"
        |+> [
            "layoutMode" =@ PlaceChooserLayoutMode

            "position" =@ PlaceChooserPosition
            |> WithComment "Ignored when layoutMode:HIDDEN. If not passed, a position will be determined automatically based on the layoutMode."
        ]

    let PlaceDetailsLayoutMode =
        Class "google.maps.journeySharing.PlaceDetailsLayoutMode"
        |+> Static [
            "INFO_WINDOW" =? TSelf
            |> WithComment "Place details is displayed in an InfoWindow."

            "SHEET" =? TSelf
            |> WithComment "Place details is displayed in a sheet."
        ]

    let PlaceDetailsPosition =
        Class "google.maps.journeySharing.PlaceDetailsPosition"
        |+> Static [
            "INLINE_END" =? TSelf
            |> WithComment "Place details is displayed inline with the map at the end of the line. (In a left-to-right language this means that the place details is to the right of the map.)"

            "INLINE_START" =? TSelf
            |> WithComment "Place details is displayed inline with the map at the start of the line. (In a left-to-right language this means that the place details is to the left of the map.)"
        ]

    let PlaceDetailsViewSetupOptions =
        Interface "google.maps.journeySharing.PlaceDetailsViewSetupOptions"
        |+> [
            "hidesOnMapClick" =@ T<bool>

            "layoutMode" =@ PlaceDetailsLayoutMode

            "position" =@ PlaceDetailsPosition
            |> WithComment "Ignored when layoutMode:INFO_WINDOW. If not passed, a position will be determined automatically based on the layoutMode."
        ]

    //TODO: how to add the namespace? "google.maps.localContext.PlaceTypePreference"
    let PlaceTypePreference = T<string> + !? T<int>

    let LocalContextMapViewOptions =
        Interface "google.maps.journeySharing.LocalContextMapViewOptions"
        |+> [
            "maxPlaceCount" =@ T<int>
            |> WithComment "The maximum number of places to show. When this parameter is 0, the Local Context Library does not load places. [0,24]"

            "placeTypePreferences" =@ Type.ArrayOf PlaceTypePreference
            |> WithComment "The types of places to search for (up to 10). The type Iterable<string|PlaceTypePreference> is also accepted, but is only supported in browsers which natively support JavaScript Symbols."

            "directionsOptions" =@ MapDirectionsOptions + MapDirectionsOptionsLiteral
            |> WithComment "Options for customizing directions. If not set, directions and distance will be disabled."

            "element" =@ HTMLElement + SVGElement
            |> WithComment "This Field is read-only. The DOM Element backing the view."

            "locationBias" =@ LocationBias
            |> WithComment "A soft boundary or hint to use when searching for places."

            "locationRestriction" =@ LocationRestriction
            |> WithComment "Bounds to constrain search results. If not specified, results will be constrained to the map viewport."

            "map" =@ M.Map
            |> WithComment "An already instantiated Map instance. If passed in, the map will be moved into the LocalContextMapView's DOM, and will not be re-styled. The element associated with the Map may also have styles and classes applied to it by the LocalContextMapView."

            //TODO: confirm how to implement this signature: (function({isSelected:boolean, isHighlighted:boolean}): (PinOptions optional))
            "pinOptionsSetup" =@ (T<obj> -* (T<bool> + T<bool>) ^-> !? PinOptions) + PinOptions
            |> WithComment "Configure the place marker icon based on the icon state. Invoked whenever the input to the callback changes. Pass a function to dynamically override the default setup when the LocalContextMapView draws the place marker. Errors and invalid configurations may be determined asynchronously, and will be ignored (defaults will be used, and errors will be logged to the console)."

            //TODO: confirm how to implement this signature: (function({defaultLayoutMode:PlaceChooserLayoutMode, defaultPosition:PlaceChooserPosition optional}): (PlaceChooserViewSetupOptions optional))|PlaceChooserViewSetupOptions
            "placeChooserViewSetup" =@ (T<obj> -* (PlaceChooserLayoutMode + !? PlaceChooserPosition) ^-> !? PlaceChooserViewSetupOptions) + PlaceChooserViewSetupOptions
            |> WithComment "Overrides the setup of the place chooser view. Pass a function to dynamically override the default setup when the LocalContextMapView might change its layout due to resizing. Errors and invalid configurations may be determined asynchronously, and will be ignored (defaults will be used instead, and errors will be logged to the console). Errors detected at construction will cause errors to be thrown synchronously."

            //TODO: confirm how to implement this signature: (function({defaultLayoutMode:PlaceDetailsLayoutMode, defaultPosition:PlaceDetailsPosition optional}): (PlaceDetailsViewSetupOptions optional))
            "placeDetailsViewSetup" =@ (T<obj> -* (PlaceDetailsLayoutMode + !? PlaceDetailsPosition) ^-> !? PlaceDetailsViewSetupOptions) + PlaceDetailsViewSetupOptions
            |> WithComment "Overrides the setup of the place details view. Pass a function to dynamically override the default setup when the LocalContextMapView might change its layout due to resizing. Errors and invalid configurations may be determined asynchronously, and will be ignored (defaults will be used, and errors will be logged to the console). Errors detected at construction will cause errors to be thrown synchronously."
        ]

    let LocalContextMapView =
        Class "google.maps.journeySharing.LocalContextMapView"
        |=> Implements [LocalContextMapViewOptions]
        |+> Static [
            Constructor LocalContextMapViewOptions
        ]
        |+> Instance [
            "isTransitioningMapBounds" =@ T<bool>
            |> WithComment "Is set to true before LocalContextMapView begins changing the bounds of the inner Map, and set to false after LocalContextMapView finishes changing the bounds of the inner Map. (Not set when layout mode changes happen due to responsive resizing.)"

            "addListener" => T<string> * T<WebSharper.JavaScript.Function> ^-> Events.MapsEventListener
            |> WithComment "Adds the given listener function to the given event name."

            "hidePlaceDetailsView" => T<unit -> unit>
            |> WithComment "Hides the place details."

            "search" => T<unit -> unit>
            |> WithComment "Searches for places to show the user based on the current maxPlaceCount, placeTypePreferences, locationRestriction, and locationBias."

            // EVENTS
            "error" => T<obj> -* Events.ErrorEvent ^-> T<unit>
            |> WithComment "This event is fired if there is an error while performing search."

            "placedetailsviewhidestart" => T<obj> -* T<unit> ^-> T<unit>
            |> WithComment "This event is fired before the place details begins animating out."

            "placedetailsviewshowstart" => T<obj> -* T<unit> ^-> T<unit>
            |> WithComment "This event is fired before the place details begins animating in."
        ]

