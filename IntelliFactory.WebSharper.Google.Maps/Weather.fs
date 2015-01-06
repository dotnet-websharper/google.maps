/// Definitions for the Weather part of the API. See:
/// http://developers.google.com/maps/documentation/javascript/reference
module IntelliFactory.WebSharper.Google.Maps.Weather

open IntelliFactory.WebSharper.InterfaceGenerator
open IntelliFactory.WebSharper.Google.Maps.Notation
open IntelliFactory.WebSharper.Google.Maps.Base
open IntelliFactory.WebSharper.Google.Maps.Specification

let CloudLayer =
    Class "google.maps.weather.CloudLayer"
    |+> [Constructor T<unit>]
    |+> Protocol [
        "getMap" => T<unit> ^-> Map.Map
        |> WithComment "Returns the map on which this layer is displayed."

        "setMap" => Map.Map ^-> T<unit>
        |> WithComment "Renders the layer on the specified map. If map is set to null, the layer will be removed."
    ]

let TemperatureUnit =
    let t = Type.New()
    Class "google.maps.weather.TemperatureUnit"
    |=> t
    |+> [
        "CELSIUS" =? t
        "FAHRENHEIT" =? t
    ]

let WindSpeedUnit =
    let t = Type.New()
    Class "google.maps.weather.WindSpeedUnit"
    |=> t
    |+> [
        "KILOMETERS_PER_HOUR" =? t
        "METERS_PER_SECOND" =? t
        "MILES_PER_HOUR" =? t
    ]

let LabelColor =
    let t = Type.New()
    Class "google.maps.weather.LabelColor"
    |=> t
    |+> [
        "BLACK" =? t
        "WHITE" =? t
    ]

let WeatherLayerOptions =
    Config "google.maps.weather.WeatherLayerOptions"
    |+> Protocol [
        "clickable" =% T<bool>
        |> WithComment "If true, the layer receives mouse events. Default value is true."

        "labelColor" =% LabelColor
        |> WithComment "The color of labels on the weather layer. If this is not explicitly set, the label color is chosen automatically depending on the map type."

        "map" =% Map.Map

        "suppressInfoWindows" =% T<bool>

        "temperatureUnits" =% TemperatureUnit

        "windSpeedUnits" =% WindSpeedUnit
    ]

let WeatherLayer =
    Class "google.maps.weather.WeatherLayer"
    |+> [Constructor (!?WeatherLayerOptions)]
    |+> Protocol [
        "getMap" => T<unit> ^-> Map.Map
        |> WithComment "Returns the map on which this layer is displayed."

        "setMap" => Map.Map ^-> T<unit>
        |> WithComment "Renders the layer on the specified map. If map is set to null, the layer will be removed."

        "setOptions" => WeatherLayerOptions ^-> T<unit>
    ]

let WeatherConditions =
    Class "google.maps.weather.WeatherConditions"
    |+> Protocol [
        "day" =? T<string>
        |> WithComment "The current day of the week in long form, e.g. \"Monday\"."

        "description" =? T<string>
        |> WithComment "A description of the conditions, e.g. \"Partly Cloudy\"."

        "high" =? T<float>
        |> WithComment "The highest temperature reached during the day."

        "humidity" =? T<float>
        |> WithComment "The current humidity, expressed as a percentage."

        "low" =? T<float>
        |> WithComment "The lowest temperature reached during the day."

        "shortDay" =? T<string>
        |> WithComment "The current day of the week in short form, e.g. \"M\"."

        "temperature" =? T<float>
        "windDirection" =? T<string>
        "windSpeed" =? T<string>
    ]

let WeatherForecast =
    Class "google.maps.weather.WeatherForecast"
    |+> Protocol [
        "day" =? T<string>
        |> WithComment "The current day of the week in long form, e.g. \"Monday\"."

        "description" =? T<string>
        |> WithComment "A description of the conditions, e.g. \"Partly Cloudy\"."

        "high" =? T<float>
        |> WithComment "The highest temperature reached during the day."

        "humidity" =? T<float>
        |> WithComment "The current humidity, expressed as a percentage."

        "low" =? T<float>
        |> WithComment "The lowest temperature reached during the day."

        "shortDay" =? T<string>
        |> WithComment "The current day of the week in short form, e.g. \"M\"."
    ]

let WeatherFeature =
    Class "google.maps.weather.WeatherFeature"
    |+> Protocol [
        "current" =? WeatherConditions
        |> WithComment "The current weather conditions at this location."

        "forecast" =? Type.ArrayOf WeatherForecast
        |> WithComment "A forecast of weather conditions over the next four days. The forecast array is always in chronological order."

        "location" =? T<string>
        |> WithComment "The location name of this feature, e.g. \"San Francisco, California\"."

        "temperatureUnit" =? TemperatureUnit
        "windSpeedUnit" =? WindSpeedUnit
    ]
