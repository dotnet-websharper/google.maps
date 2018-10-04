// $begin{copyright}
//
// This file is part of WebSharper
//
// Copyright (c) 2008-2018 IntelliFactory
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
/// Definitions for the Weather part of the API. See:
/// http://developers.google.com/maps/documentation/javascript/reference
module WebSharper.Google.Maps.Weather

open WebSharper.InterfaceGenerator
open WebSharper.Google.Maps.Notation
open WebSharper.Google.Maps.Base
open WebSharper.Google.Maps.Specification

let CloudLayer =
    Class "google.maps.weather.CloudLayer"
    |+> Static [Constructor T<unit>]
    |+> Instance [
        "getMap" => T<unit> ^-> Map.Map
        |> WithComment "Returns the map on which this layer is displayed."

        "setMap" => Map.Map ^-> T<unit>
        |> WithComment "Renders the layer on the specified map. If map is set to null, the layer will be removed."
    ]

let TemperatureUnit =
    Class "google.maps.weather.TemperatureUnit"
    |+> Static [
        "CELSIUS" =? TSelf
        "FAHRENHEIT" =? TSelf
    ]

let WindSpeedUnit =
    Class "google.maps.weather.WindSpeedUnit"
    |+> Static [
        "KILOMETERS_PER_HOUR" =? TSelf
        "METERS_PER_SECOND" =? TSelf
        "MILES_PER_HOUR" =? TSelf
    ]

let LabelColor =
    Class "google.maps.weather.LabelColor"
    |+> Static [
        "BLACK" =? TSelf
        "WHITE" =? TSelf
    ]

let WeatherLayerOptions =
    Config "google.maps.weather.WeatherLayerOptions"
    |+> Instance [
        "clickable" =@ T<bool>
        |> WithComment "If true, the layer receives mouse events. Default value is true."

        "labelColor" =@ LabelColor
        |> WithComment "The color of labels on the weather layer. If this is not explicitly set, the label color is chosen automatically depending on the map type."

        "map" =@ Map.Map

        "suppressInfoWindows" =@ T<bool>

        "temperatureUnits" =@ TemperatureUnit

        "windSpeedUnits" =@ WindSpeedUnit
    ]

let WeatherLayer =
    Class "google.maps.weather.WeatherLayer"
    |+> Static [Constructor (!?WeatherLayerOptions)]
    |+> Instance [
        "getMap" => T<unit> ^-> Map.Map
        |> WithComment "Returns the map on which this layer is displayed."

        "setMap" => Map.Map ^-> T<unit>
        |> WithComment "Renders the layer on the specified map. If map is set to null, the layer will be removed."

        "setOptions" => WeatherLayerOptions ^-> T<unit>
    ]

let WeatherConditions =
    Class "google.maps.weather.WeatherConditions"
    |+> Instance [
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
    |+> Instance [
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
    |+> Instance [
        "current" =? WeatherConditions
        |> WithComment "The current weather conditions at this location."

        "forecast" =? Type.ArrayOf WeatherForecast
        |> WithComment "A forecast of weather conditions over the next four days. The forecast array is always in chronological order."

        "location" =? T<string>
        |> WithComment "The location name of this feature, e.g. \"San Francisco, California\"."

        "temperatureUnit" =? TemperatureUnit
        "windSpeedUnit" =? WindSpeedUnit
    ]
