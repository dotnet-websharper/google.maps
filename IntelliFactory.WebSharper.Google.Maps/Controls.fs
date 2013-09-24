/// Definitions for the Controls part of the API. See:
/// http://developers.google.com/maps/documentation/javascript/reference
module IntelliFactory.WebSharper.Google.Maps.Controls

open IntelliFactory.WebSharper.InterfaceGenerator
open IntelliFactory.WebSharper.Google.Maps.Notation

let ControlPosition =
    let t = Type.New()
    Class "ControlPosition"
    |=> t
    |+> [
        "BOTTOM_CENTER" =? t
        |> WithComment "Elements are positioned in the center of the bottom row."

        "BOTTOM_LEFT" =? t
        |> WithComment "Elements are positioned in the bottom left and flow towards the middle. Elements are positioned to the right of the Google logo."

        "BOTTOM_RIGHT" =? t
        |> WithComment "Elements are positioned in the bottom right and flow towards the middle. Elements are positioned to the left of the copyrights."

        "LEFT_BOTTOM" =? t
        |> WithComment "Elements are positioned on the left, above bottom-left elements, and flow upwards."

        "LEFT_CENTER" =? t
        |> WithComment "Elements are positioned in the center of the left side."

        "LEFT_TOP" =? t
        |> WithComment "Elements are positioned on the left, below top-left elements, and flow downwards."

        "RIGHT_BOTTOM" =? t
        |> WithComment "Elements are positioned on the right, above bottom-right elements, and flow upwards."

        "RIGHT_CENTER" =? t
        |> WithComment "Elements are positioned in the center of the right side."

        "RIGHT_TOP" =? t
        |> WithComment "Elements are positioned on the right, below top-right elements, and flow downwards."

        "TOP_CENTER" =? t
        |> WithComment "Elements are positioned in the center of the top row."

        "TOP_LEFT" =? t
        |> WithComment "Elements are positioned in the top left and flow towards the middle."

        "TOP_RIGHT" =? t
        |> WithComment "Elements are positioned in the top right and flow towards the middle."
    ]

let MapTypeControlStyle =
    let t = Type.New()
    Class "google.maps.MapTypeControlStyle"
    |=> t
    |+> [
        "DEFAULT" =? t
        |> WithComment "Uses the default map type control. The control which DEFAULT maps to will vary according to window size and other factors. It may change in future versions of the API."
        
        "DROPDOWN_MENU" =? t
        |> WithComment "A dropdown menu for the screen realestate conscious."

        "HORIZONTAL_BAR" =? t
        |> WithComment "The standard horizontal radio buttons bar."
    ]

let MapTypeControlOptions =
    Config "MapTypeControlOptions"
    |+> Protocol [
            "mapTypeIds" =% Type.ArrayOf Forward.MapTypeId
            |> WithComment "IDs of map types to show in the control."

            "position" =% ControlPosition
            |> WithComment "Position id. Used to specify the position of the control on the map. The default position is TOP_RIGHT."

            "style" =% MapTypeControlStyle
            |> WithComment "Style id. Used to select what style of map type control to display."
        ]

let OverviewMapControlOptions =
    Config "OverviewMapControlOptions"
    |+> Protocol [
            "opened" =% T<bool>
            |> WithComment "Whether the control should display in opened mode or collapsed (minimized) mode. By default, the control is closed."
        ]

let PanControlOptions =
    Config "PanControlOptions"
    |+> Protocol [
            "position" =% ControlPosition
            |> WithComment "Position id. Used to specify the position of the control on the map. The default position is TOP_LEFT."
        ]

let RotateControlOptions =
    Config "RotateControlOptions"
    |+> Protocol [
            "position" =% ControlPosition
            |> WithComment "Position id. Used to specify the position of the control on the map. The default position is TOP_LEFT."
        ]

let ScaleControlStyle =
    let t = Type.New()
    Class "google.maps.ScaleControlStyle"
    |=> t
    |+> [
        "DEFAULT" =? t
        |> WithComment "The standard scale control."
    ]

let ScaleControlOptions =
    Config "ScaleControlOptions"
    |+> Protocol [
            "position" =% ControlPosition
            |> WithComment "Position id. Used to specify the position of the control on the map. The default position is BOTTOM_LEFT when google.maps.visualRefresh is set to false. When google.maps.visualRefresh is true the scale control will be fixed at the BOTTOM_RIGHT."

            "style" =% ScaleControlStyle
            |> WithComment "Style id. Used to select what style of scale control to display."
        ]

let StreetViewControlOptions =
    Config "StreetViewControlOptions"
    |+> Protocol [
            "position" =% ControlPosition
            |> WithComment "Position id. Used to specify the position of the control on the map. The default position is embedded within the navigation (zoom and pan) controls. If this position is empty or the same as that specified in the zoomControlOptions or panControlOptions, the Street View control will be displayed as part of the navigation controls. Otherwise, it will be displayed separately."
        ]

let ZoomControlStyle =
    let t = Type.New()
    Class "google.maps.ZoomControlStyle"
    |=> t
    |+> [
        "DEFAULT" =? t
        |> WithComment "The default zoom control. The control which DEFAULT maps to will vary according to map size and other factors. It may change in future versions of the API."

        "LARGE" =? t
        |> WithComment "The larger control, with the zoom slider in addition to +/- buttons."

        "SMALL" =? t
        |> WithComment "A small control with buttons to zoom in and out."
    ]

let ZoomControlOptions =
    Config "ZoomControlOptions"
    |+> Protocol [
            "position" =% ControlPosition
            |> WithComment "Position id. Used to specify the position of the control on the map. The default position is TOP_LEFT."

            "style" =% ZoomControlStyle
            |> WithComment "Style id. Used to select what style of zoom control to display."
        ]
