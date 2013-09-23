/// Definitions for the MVC part of the API. See:
/// http://developers.google.com/maps/documentation/javascript/reference
module IntelliFactory.WebSharper.Google.Maps.MVC

open IntelliFactory.WebSharper.InterfaceGenerator
open IntelliFactory.WebSharper.Google.Maps.Notation

let MVCObject =
    let MVCObject = Class "google.maps.MVCObject"
    MVCObject
    |+> Protocol [

        // Adds the given listener function to the given event name.
        // Returns an identifier for this listener that can be used
        // with google.maps.event.removeListener.
        "addListener" => (T<string>?eventName * T<obj->unit>) ^-> Events.MapsEventListener

        // Binds a View to a Model.
        "bindTo" =>
            Fun T<unit> [
                T<string>?key
                MVCObject?target
                T<string>?targetKey
                T<bool>?noNotify
            ]

        // Generic handler for state changes. Override this in derived classes
        // to handle arbitrary state changes.
        "changed" => T<string> ^-> T<unit>

        // Gets a value.
        "get" => T<string>?key ^-> T<obj>

        // Notify all observers of a change on this property.
        // This notifies both objects that are bound to the object's property
        // as well as the object that it is bound to.
        "notify" => T<string>?key ^-> T<unit>

        // Sets a value.
        "set" => Fun T<unit> [T<string>?key; T<obj>?value]

        // Sets a collection of key-value pairs.
        "setValues" => T<obj>?values ^-> T<unit>

        // Removes a binding. Unbinding will set the unbound property to
        // the current value. The object will not be notified, as the value
        // has not changed.
        "unbind" => T<string>?key ^-> T<unit>

        // Removes all bindings.
        "unbindAll" => T<unit> ^-> T<unit>
    ]
    |+> [
        Constructor T<unit>
    ]

let MVCArray = Generic / fun t ->
    (
        Class "google.maps.MVCArray"
        |+> Protocol [
            "clear" => T<unit->unit>
            "forEach" => (t * T<int> ^-> T<unit>) ^-> T<unit>
            "getArray" => T<unit> ^-> Type.ArrayOf t
            "getAt" => T<int> ^-> t
            "getLength" => T<unit->int>
            "insertAt" => T<int> * t ^-> T<unit>
            "pop" => T<unit> ^-> t
            "push" => t ^-> T<unit>
            "removeAt" => T<int> ^-> T<unit>
            "setAt" => T<int> * t ^-> T<unit>

            /// TODO: "insert_at", "remove_at", "set_at" events
        ]
        |+> [
                Constructor T<unit>
                Constructor (Type.ArrayOf t)
            ]
        |=> Inherits MVCObject
    )

