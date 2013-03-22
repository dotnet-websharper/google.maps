/// Forward type declarations.
module IntelliFactory.WebSharper.Google.Maps.Forward

open IntelliFactory.WebSharper.InterfaceGenerator

let Map = Type.New()

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
