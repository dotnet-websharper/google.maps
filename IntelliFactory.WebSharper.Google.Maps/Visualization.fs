/// See "Visualization Library" at
/// at http://developers.google.com/maps/documentation/javascript/reference
module IntelliFactory.WebSharper.Google.Maps.Visualization

open IntelliFactory.WebSharper.InterfaceGenerator
open IntelliFactory.WebSharper.Google.Maps.Notation
module M = IntelliFactory.WebSharper.Google.Maps.Map

let WeightedLocation =
    Class "google.maps.visualization.WeightedLocation"
    |+> Protocol [
        "location" =@ Base.LatLng
        "weight" =@ T<float>
    ]

let private HeatmapLayerProperties =
    [
        "dissipating", T<bool>
        "gradient", Type.ArrayOf T<string>
        "map", M.Map.Type
        "maxIntensity", T<float>
        "opacity", T<float>
        "radius", T<int>
    ]

let HeatmapLayerOptions =
    Pattern.Config "HeatmapLayerOptions" {
        Required = ["data", MVC.MVCArray(Base.LatLng).Type]
        Optional = HeatmapLayerProperties
    }

let HeatmapLayer =
    let loc = Base.LatLng + WeightedLocation
    Class "google.maps.visualization.HeatmapLayer"
    |=> Inherits MVC.MVCObject
    |+> Protocol [

            "getData" => T<unit> ^-> MVC.MVCArray Base.LatLng
            |> WithSourceName "GetData"

            "getData" => T<unit> ^-> MVC.MVCArray WeightedLocation
            |> WithSourceName "GetWeightedData"

            "getMap" => T<unit> ^-> M.Map
            "setData" => (MVC.MVCArray loc + Type.ArrayOf loc) ^-> T<unit>
            "setMap" => M.Map ^-> T<unit>

            "data" =@ MVC.MVCArray Base.LatLng

        ]
    |+> Protocol [
            for (k, v) in HeatmapLayerProperties do
                yield (k =@ v) :> _
        ]
    |+> [
            Ctor [!? HeatmapLayerOptions?options]
        ]
