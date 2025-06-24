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
// Definitions for the MVC part of the API. See:
// http://developers.google.com/maps/documentation/javascript/reference
namespace WebSharper.Google.Maps.Definition

module MVC =

    open WebSharper.InterfaceGenerator
    open Notation

    let MVCObject =
        Class "google.maps.MVCObject"
        |+> Instance [
            "addListener" => (T<string>?eventName * T<obj->unit>) ^-> Events.MapsEventListener
            |> WithComment "Adds the given listener function to the given event name. Returns an identifier for this listener that can be used with google.maps.event.removeListener."

            "bindTo" =>
                Fun T<unit> [
                    T<string>?key
                    TSelf?target
                    T<string>?targetKey
                    T<bool>?noNotify
                ]
            |> WithComment "Binds a View to a Model."

            // Gets a value.
            "get" => T<string>?key ^-> T<obj>
            |> WithComment "Gets a value."

            "notify" => T<string>?key ^-> T<unit>
            |> WithComment "Notify all observers of a change on this property. This notifies both objects that are bound to the object's property as well as the object that it is bound to."

            "set" => Fun T<unit> [T<string>?key; T<obj>?value]
            |> WithComment "Sets a value."

            "setValues" => T<obj>?values ^-> T<unit>
            |> WithComment "Sets a collection of key-value pairs."

            "unbind" => T<string>?key ^-> T<unit>
            |> WithComment "Removes a binding. Unbinding will set the unbound property to the current value. The object will not be notified, as the value has not changed."

            "unbindAll" => T<unit> ^-> T<unit>
            |> WithComment "Removes all bindings."
        ]
        |+> Static [
            Constructor T<unit>
        ]

    let MVCArray = Generic - fun t ->
        (
            Class "google.maps.MVCArray"
            |+> Instance [
                "clear" => T<unit->unit>
                |> WithComment "Removes all elements from the array."

                "forEach" => (t * T<int> ^-> T<unit>) ^-> T<unit>
                |> WithComment "Iterate over each element, calling the provided callback. The callback is called for each element like: callback(element, index)."

                "getArray" => T<unit> ^-> Type.ArrayOf t
                |> WithComment "Returns a reference to the underlying Array. Warning: if the Array is mutated, no events will be fired by this object."

                "getAt" => T<int> ^-> t
                |> WithComment "Returns the element at the specified index."

                "getLength" => T<unit->int>
                |> WithComment "Returns the number of elements in this array."

                "insertAt" => T<int> * t ^-> T<unit>
                |> WithComment "Inserts an element at the specified index."

                "pop" => T<unit> ^-> t
                |> WithComment "Removes the last element of the array and returns that element."

                "push" => t ^-> T<unit>
                |> WithComment "Adds one element to the end of the array and returns the new length of the array."

                "removeAt" => T<int> ^-> T<obj>
                |> WithComment "Removes an element from the specified index."

                "setAt" => T<int> * t ^-> T<unit>
                |> WithComment "Sets an element at the specified index."

                // EVENTS
                "insert_at" => T<obj> -* T<int> ^-> T<unit>
                |> WithComment "This event is fired when insertAt() is called. The event passes the index that was passed to insertAt()."

                "remove_at" => T<obj> -* T<int> * T<bool> ^-> T<unit>
                |> WithComment "This event is fired when removeAt() is called. The event passes the index that was passed to removeAt() and the element that was removed from the array."

                "set_at" => T<obj> -* T<int> * t ^-> T<unit>
                |> WithComment "This event is fired when setAt() is called. The event passes the index that was passed to setAt() and the element that was previously in the array at that index."
            ]
            |+> Static [
                    Constructor (!? (Type.ArrayOf t))
                ]
            |=> Inherits MVCObject
        )

