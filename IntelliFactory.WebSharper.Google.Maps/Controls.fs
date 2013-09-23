/// Definitions for the Controls part of the API. See:
/// http://developers.google.com/maps/documentation/javascript/reference
module IntelliFactory.WebSharper.Google.Maps.Controls

open IntelliFactory.WebSharper.InterfaceGenerator
open IntelliFactory.WebSharper.Google.Maps.Notation

let ControlPosition =
    Pattern.EnumInlines "ControlPosition" [
        "BOTTOM_CENTER", "google.maps.ControlPosition.BOTTOM_CENTER"
        "BOTTOM_LEFT", "google.maps.ControlPosition.BOTTOM_LEFT"
        "BOTTOM_RIGHT", "google.maps.ControlPosition.BOTTOM_RIGHT"

        "LEFT_BOTTOM", "google.maps.ControlPosition.LEFT_TOP"
        "LEFT_CENTER", "google.maps.ControlPosition.LEFT_CENTER"
        "LEFT_TOP", "google.maps.ControlPosition.LEFT_TOP"

        "RIGHT_BOTTOM", "google.maps.ControlPosition.RIGHT_TOP"
        "RIGHT_CENTER", "google.maps.ControlPosition.RIGHT_CENTER"
        "RIGHT_TOP", "google.maps.ControlPosition.RIGHT_TOP"

        "TOP_CENTER", "google.maps.ControlPosition.TOP_CENTER"
        "TOP_LEFT", "google.maps.ControlPosition.TOP_LEFT"
        "TOP_RIGHT", "google.maps.ControlPosition.TOP_RIGHT"
    ]

let MapTypeControlStyle =
    Pattern.EnumInlines "MapTypeControlStyle" [
        // Uses the default map type control. The control which DEFAULT maps to will vary according to window size and other factors. It may change in future versions of the API.
        "DEFAULT", "google.maps.MapTypeControlStyle.DEFAULT"
        // A dropdown menu for the screen realestate conscious.
        "DROPDOWN_MENU", "google.maps.MapTypeControlStyle.DROPDOWN_MENU"
        // The standard horizontal radio buttons bar.
        "HORIZONTAL_BAR", "google.maps.MapTypeControlStyle.HORIZONTAL_BAR"
    ]

let MapTypeControlOptions =
    Pattern.Config "MapTypeControlOptions" {
        Required = []
        Optional =
        [
            // IDs of map types to show in the control.
            "mapTypeIds", Type.ArrayOf Forward.MapTypeId
            // Position id. Used to specify the position of the control on the map. The default position is TOP_RIGHT.
            "position", ControlPosition.Type
            // Style id. Used to select what style of map type control to display.
            "style", MapTypeControlStyle.Type
        ]
    }

let OverviewMapControlOptions =
    Pattern.Config "OverviewMapControlOptions" {
        Required = []
        Optional =
        [
            "opened", T<bool>
        ]
    }

let PanControlOptions =
    Pattern.Config "PanControlOptions" {
        Required = []
        Optional =
        [
            "position", ControlPosition.Type
        ]
    }

let RotateControlOptions =
    Pattern.Config "RotateControlOptions" {
        Required = []
        Optional =
        [
            "position", ControlPosition.Type
        ]
    }

let StreetViewControlOptions =
    Pattern.Config "StreetViewControlOptions" {
        Required = []
        Optional =
        [
            "position", ControlPosition.Type
        ]
    }

let ScaleControlStyle =
    Pattern.EnumInlines "ScaleControlStyle" [
        "DEFAULT", "google.maps.ScaleControlStyle.DEFAULT"
    ]

let ScaleControlOptions =
    Pattern.Config "ScaleControlOptions" {
        Required = []
        Optional =
        [
            "position", ControlPosition.Type
            "style", ScaleControlStyle.Type
        ]
    }

let ZoomControlStyle =
    Pattern.EnumInlines "ZoomControlStyle" [
        "DEFAULT", "google.maps.ZoomControlStyle.DEFAULT"
        "LARGE", "google.maps.ZoomControlStyle.LARGE"
        "SMALL", "google.maps.ZoomControlStyle.SMALL"
    ]

let ZoomControlOptions =
    Pattern.Config "ZoomControlOptions" {
        Required = []
        Optional =
        [
            "position", ControlPosition.Type
            "style", ZoomControlStyle.Type
        ]
    }
