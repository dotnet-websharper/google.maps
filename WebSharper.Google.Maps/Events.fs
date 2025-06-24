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
// Definitions for the Events part of the API. See:
// http://developers.google.com/maps/documentation/javascript/reference
namespace WebSharper.Google.Maps.Definition

module Events =

    open WebSharper.InterfaceGenerator

    let MapsEventListener =
        Interface "google.maps.MapsEventListener"
        |+> [
            "remove" => T<unit -> unit>
            |> WithComment "Removes the listener. Calling listener.remove() is equivalent to google.maps.event.removeListener(listener)."
        ]

    let Event =
        Class "google.maps.event"
        |+> Static [
            "addDomListener" => (T<obj> * T<string> * Notation.Function * !?T<bool>) ^-> MapsEventListener
            |> WithComment "Cross browser event handler registration. This listener is removed by calling removeListener(handle) for the handle that is returned by this function."
            |> ObsoleteWithMessage "Deprecated: google.maps.event.addDomListener() is deprecated, use the standard addEventListener() method instead. The feature will continue to work and there is no plan to decommission it."

            "addDomListenerOnce" => (T<obj> * T<string> * Notation.Function * !?T<bool>) ^-> MapsEventListener
            |> WithComment "Wrapper around addDomListener that removes the listener after the first event."
            |> ObsoleteWithMessage "Deprecated: google.maps.event.addDomListenerOnce() is deprecated, use the standard addEventListener() method instead. The feature will continue to work and there is no plan to decommission it."

            "addListener" => (T<obj> * T<string> * Notation.Function) ^-> MapsEventListener
            |> WithComment "Adds the given listener function to the given event name for the given object instance. Returns an identifier for this listener that can be used with removeListener()."

            "addListenerOnce" => (T<obj> * T<string> * Notation.Function) ^-> MapsEventListener
            |> WithComment "Like addListener, but the handler removes itself after handling the first event."

            "clearInstanceListeners" => T<obj> ^-> T<unit>
            |> WithComment "Removes all listeners for all events for the given instance."

            "clearListeners" => (T<obj> * T<string>) ^-> T<unit>
            |> WithComment "Removes all listeners for the given event for the given instance."

            "hasListeners" => (T<obj> * T<string>) ^-> T<bool>
            |> WithComment "Returns if there are listeners for the given event on the given instance. Can be used to to save the computation of expensive event details."

            "removeListener" => (MapsEventListener) ^-> T<unit>
            |> WithComment "Removes the given listener, which should have been returned by addListener above. Equivalent to calling listener.remove()."

            "trigger" => (T<obj> * T<string> *+ T<obj>) ^-> T<unit>
            |> WithComment "Triggers the given event. All arguments after eventName are passed as arguments to the listeners."
        ]

    let ErrorEvent =
        Notation.Config "google.maps.ErrorEvent"
            ["error", Notation.Error]
            []
