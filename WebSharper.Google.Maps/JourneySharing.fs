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

module JourneySharing =

    open WebSharper.InterfaceGenerator
    open Base
    open Notation
    open Specification
    module M = WebSharper.Google.Maps.Definition.Map

    let AutomaticViewportMode =
        Class "google.maps.journeySharing.AutomaticViewportMode"
        |+> Static [
            "FIT_ANTICIPATED_ROUTE" =? TSelf
            |> WithComment "Automatically adjust the viewport to fit markers and any visible anticipated route polylines. This is the default."

            "NONE" =? TSelf
            |> WithComment "Do not automatically adjust the viewport."
        ]

    let LocationProvider =
        Class "google.maps.journeySharing.LocationProvider"
        |+> Instance [
            "addListener" => T<string> * (T<WebSharper.JavaScript.Function>) ^-> Events.MapsEventListener
            |> WithComment "Adds a MapsEventListener for an event fired by this location provider. Returns an identifier for this listener that can be used with event.removeListener."
        ]

    let PollingLocationProviderIsPollingChangeEvent =
        Interface "google.maps.journeySharing.PollingLocationProviderIsPollingChangeEvent"
        |+> [
            "error" =@ T<WebSharper.JavaScript.Error>
            |> WithComment "The error that caused the polling state to change, if the state change was caused by an error. Undefined if the state change was due to normal operations."
            ]

    let PollingLocationProvider =
        Class "google.maps.journeySharing.PollingLocationProvider"
        |=> Inherits LocationProvider
        |+> Instance [
            "isPolling" =@ T<bool>
            |> WithComment "True if this location provider is polling. Read only."

            "pollingIntervalMillis" =@ T<int>
            |> WithComment "Minimum time between fetching location updates in milliseconds. If it takes longer than pollingIntervalMillis to fetch a location update, the next location update is not started until the current one finishes.

Setting this value to 0, Infinity, or a negative value disables automatic location updates. A new location update is fetched once if the tracking ID parameter (for example, the shipment tracking ID of the shipment location provider), or a filtering option (for example, viewport bounds or attribute filters for fleet location providers) changes.

The default, and minimum, polling interval is 5000 milliseconds. If you set the polling interval to a lower positive value, 5000 is stored and used."

            // EVENTS
            "ispollingchange" => T<obj> -* PollingLocationProviderIsPollingChangeEvent ^-> T<unit>
            |> WithComment "Event that is triggered when the polling state of the location provider is updated. Use PollingLocationProvider.isPolling to determine the current polling state."
        ]

    let MarkerSetupOptions =
        Config "google.maps.journeySharing.MarkerSetupOptions"
        |+> Instance [
            "markerOptions" =@ MarkerOptions
            |> WithComment "Marker options."
        ]

    let DefaultMarkerSetupOptions =
        Config "google.maps.journeySharing.DefaultMarkerSetupOptions"
        |+> Instance [
            "defaultMarkerOptions" =@ MarkerOptions
            |> WithComment "Default marker options."
        ]
        |> ObsoleteWithMessage "Deprecated: Marker setup is deprecated. Use the MarkerCustomizationFunction methods for your location provider instead."

    let PolylineSetupOptions =
        Config "google.maps.journeySharing.PolylineSetupOptions"
        |+> Instance [
            "polylineOptions" =@ PolylineOptions
            |> WithComment "Polyline options."

            "visible" =@ T<bool>
            |> WithComment "Polyline visibility."
        ]
        |> ObsoleteWithMessage "Deprecated: Polyline setup is deprecated. Use the PolylineCustomizationFunction methods for your location provider instead."

    let DefaultPolylineSetupOptions =
        Interface "google.maps.journeySharing.DefaultPolylineSetupOptions"
        |+> [
            "defaultPolylineOptions" =@ PolylineOptions
            |> WithComment "Default polyline options."

            "defaultVisible" =@ T<bool>
            |> WithComment "Default polyline visibility."
        ]

    //TODO: add namespace: google.maps.journeySharing.MarkerSetup
    let MarkerSetup = MarkerSetupOptions + (DefaultMarkerSetupOptions ^-> MarkerSetupOptions)
    // |> ObsoleteWithMessage "Deprecated: Marker setup is deprecated. Use the MarkerCustomizationFunction methods for your location provider instead."

    //TODO: add namespace: google.maps.journeySharing.PolylineSetup
    let PolylineSetup = PolylineSetupOptions + (DefaultPolylineSetupOptions ^-> PolylineSetupOptions)
        // |> ObsoleteWithMessage "Deprecated: Polyline setup is deprecated. Use the PolylineCustomizationFunction methods for your location provider instead."

    let JourneySharingMapViewOptions =
        Interface "google.maps.journeySharing.JourneySharingMapViewOptions"
        |+> [
            "element" =@ Element
            |> WithComment "The DOM element backing the view. Required."

            "anticipatedRoutePolylineSetup" =@ PolylineSetup
            |> WithComment "Configures options for an anticipated route polyline. Invoked whenever a new anticipated route polyline is rendered.

If specifying a function, the function can and should modify the input's defaultPolylineOptions field containing a google.maps.PolylineOptions object, and return it as polylineOptions in the output PolylineSetupOptions object.

Specifying a PolylineSetupOptions object has the same effect as specifying a function that returns that static object.

Do not reuse the same PolylineSetupOptions object in different PolylineSetup functions or static values, and do not reuse the same google.maps.PolylineOptions object for the polylineOptions key in different PolylineSetupOptions objects. If polylineOptions or visible is unset or null, it will be overwritten with the default. Any values set for polylineOptions.map or polylineOptions.path will be ignored."
            |> ObsoleteWithMessage "Deprecated: Polyline setup is deprecated. Use the PolylineCustomizationFunction methods for your location provider instead. This field will be removed in the future."

            "automaticViewportMode" =@ AutomaticViewportMode
            |> WithComment "Automatic viewport mode. Default value is FIT_ANTICIPATED_ROUTE, which enables the map view to automatically adjust the viewport to fit vehicle markers, location markers, and any visible anticipated route polylines. Set this to NONE to turn off automatic fitting."

            "destinationMarkerSetup" =@ MarkerSetup
            |> WithComment "Configures options for a destination location marker. Invoked whenever a new destination marker is rendered.

If specifying a function, the function can and should modify the input's defaultMarkerOptions field containing a google.maps.MarkerOptions object, and return it as markerOptions in the output MarkerSetupOptions object.

Specifying a MarkerSetupOptions object has the same effect as specifying a function that returns that static object.

Do not reuse the same MarkerSetupOptions object in different MarkerSetup functions or static values, and do not reuse the same google.maps.MarkerOptions object for the markerOptions key in different MarkerSetupOptions objects. If markerOptions is unset or null, it will be overwritten with the default. Any value set for markerOptions.map or markerOptions.position will be ignored."
            |> ObsoleteWithMessage "Deprecated: Marker setup is deprecated. Use the MarkerCustomizationFunction methods for your location provider instead. This field will be removed in the future."

            "locationProvider" =@ LocationProvider
            |> WithComment "A source of tracked locations to be shown in the tracking map view. Optional."

            "locationProviders" =@ Type.ArrayOf LocationProvider
            |> WithComment "Sources of tracked locations to be shown in the tracking map view. Optional."

            "mapOptions" =@ M.MapOptions
            |> WithComment "Map options passed into the google.maps.Map constructor."

            "originMarkerSetup" =@ MarkerSetup
            |> WithComment "Configures options for an origin location marker. Invoked whenever a new origin marker is rendered.

If specifying a function, the function can and should modify the input's defaultMarkerOptions field containing a google.maps.MarkerOptions object, and return it as markerOptions in the output MarkerSetupOptions object.

Specifying a MarkerSetupOptions object has the same effect as specifying a function that returns that static object.

Do not reuse the same MarkerSetupOptions object in different MarkerSetup functions or static values, and do not reuse the same google.maps.MarkerOptions object for the markerOptions key in different MarkerSetupOptions objects. If markerOptions is unset or null, it will be overwritten with the default. Any value set for markerOptions.map or markerOptions.position will be ignored."
            |> ObsoleteWithMessage "Deprecated: Marker setup is deprecated. Use the MarkerCustomizationFunction methods for your location provider instead. This field will be removed in the future."

            "pingMarkerSetup" =@ MarkerSetup
            |> WithComment "Configures options for a ping location marker. Invoked whenever a new ping marker is rendered.

If specifying a function, the function can and should modify the input's defaultMarkerOptions field containing a google.maps.MarkerOptions object, and return it as markerOptions in the output MarkerSetupOptions object.

Specifying a MarkerSetupOptions object has the same effect as specifying a function that returns that static object.

Do not reuse the same MarkerSetupOptions object in different MarkerSetup functions or static values, and do not reuse the same google.maps.MarkerOptions object for the markerOptions key in different MarkerSetupOptions objects. If markerOptions is unset or null, it will be overwritten with the default. Any value set for markerOptions.map or markerOptions.position will be ignored."
            |> ObsoleteWithMessage "Deprecated: Marker setup is deprecated. Use the MarkerCustomizationFunction methods for your location provider instead. This field will be removed in the future."

            "successfulTaskMarkerSetup" =@ MarkerSetup
            |> WithComment "Configures options for a successful task location marker. Invoked whenever a new successful task marker is rendered.

If specifying a function, the function can and should modify the input's defaultMarkerOptions field containing a google.maps.MarkerOptions object, and return it as markerOptions in the output MarkerSetupOptions object.

Specifying a MarkerSetupOptions object has the same effect as specifying a function that returns that static object.

Do not reuse the same MarkerSetupOptions object in different MarkerSetup functions or static values, and do not reuse the same google.maps.MarkerOptions object for the markerOptions key in different MarkerSetupOptions objects. If markerOptions is unset or null, it will be overwritten with the default. Any value set for markerOptions.map or markerOptions.position will be ignored."
            |> ObsoleteWithMessage "Deprecated: Marker setup is deprecated. Use the MarkerCustomizationFunction methods for your location provider instead. This field will be removed in the future."

            "takenRoutePolylineSetup" =@ PolylineSetup
            |> WithComment "Configures options for a taken route polyline. Invoked whenever a new taken route polyline is rendered.

If specifying a function, the function can and should modify the input's defaultPolylineOptions field containing a google.maps.PolylineOptions object, and return it as polylineOptions in the output PolylineSetupOptions object.

Specifying a PolylineSetupOptions object has the same effect as specifying a function that returns that static object.

Do not reuse the same PolylineSetupOptions object in different PolylineSetup functions or static values, and do not reuse the same google.maps.PolylineOptions object for the polylineOptions key in different PolylineSetupOptions objects.

Any values set for polylineOptions.map or polylineOptions.path will be ignored. Any unset or null value will be overwritten with the default."
            |> ObsoleteWithMessage "Deprecated: Polyline setup is deprecated. Use the PolylineCustomizationFunction methods for your location provider instead. This field will be removed in the future."

            "taskOutcomeMarkerSetup" =@ MarkerSetup
            |> WithComment "Configures options for a task outcome location marker. Invoked whenever a new task outcome location marker is rendered.

If specifying a function, the function can and should modify the input's defaultMarkerOptions field containing a google.maps.MarkerOptions object, and return it as markerOptions in the output MarkerSetupOptions object.

Specifying a MarkerSetupOptions object has the same effect as specifying a function that returns that static object.

Do not reuse the same MarkerSetupOptions object in different MarkerSetup functions or static values, and do not reuse the same google.maps.MarkerOptions object for the markerOptions key in different MarkerSetupOptions objects. If markerOptions is unset or null, it will be overwritten with the default. Any value set for markerOptions.map or markerOptions.position will be ignored."
            |> ObsoleteWithMessage "Deprecated: Marker setup is deprecated. Use the MarkerCustomizationFunction methods for your location provider instead. This field will be removed in the future."

            "unsuccessfulTaskMarkerSetup" =@ MarkerSetup
            |> WithComment "Configures options for an unsuccessful task location marker. Invoked whenever a new unsuccessful task marker is rendered.

If specifying a function, the function can and should modify the input's defaultMarkerOptions field containing a google.maps.MarkerOptions object, and return it as markerOptions in the output MarkerSetupOptions object.

Specifying a MarkerSetupOptions object has the same effect as specifying a function that returns that static object.

Do not reuse the same MarkerSetupOptions object in different MarkerSetup functions or static values, and do not reuse the same google.maps.MarkerOptions object for the markerOptions key in different MarkerSetupOptions objects. If markerOptions is unset or null, it will be overwritten with the default. Any value set for markerOptions.map or markerOptions.position will be ignored."
            |> ObsoleteWithMessage "Deprecated: Marker setup is deprecated. Use the MarkerCustomizationFunction methods for your location provider instead. This field will be removed in the future."

            "vehicleMarkerSetup" =@ MarkerSetup
            |> WithComment "Configures options for a vehicle location marker. Invoked whenever a new vehicle marker is rendered.

If specifying a function, the function can and should modify the input's defaultMarkerOptions field containing a google.maps.MarkerOptions object, and return it as markerOptions in the output MarkerSetupOptions object.

Specifying a MarkerSetupOptions object has the same effect as specifying a function that returns that static object.

Do not reuse the same MarkerSetupOptions object in different MarkerSetup functions or static values, and do not reuse the same google.maps.MarkerOptions object for the markerOptions key in different MarkerSetupOptions objects. If markerOptions is unset or null, it will be overwritten with the default. Any value set for markerOptions.map or markerOptions.position will be ignored."
            |> ObsoleteWithMessage "Deprecated: Marker setup is deprecated. Use the MarkerCustomizationFunction methods for your location provider instead. This field will be removed in the future."

            "waypointMarkerSetup" =@ MarkerSetup
            |> WithComment "Configures options for a waypoint location marker. Invoked whenever a new waypoint marker is rendered.

If specifying a function, the function can and should modify the input's defaultMarkerOptions field containing a google.maps.MarkerOptions object, and return it as markerOptions in the output MarkerSetupOptions object.

Specifying a MarkerSetupOptions object has the same effect as specifying a function that returns that static object.

Do not reuse the same MarkerSetupOptions object in different MarkerSetup functions or static values, and do not reuse the same google.maps.MarkerOptions object for the markerOptions key in different MarkerSetupOptions objects. If markerOptions is unset or null, it will be overwritten with the default. Any value set for markerOptions.map or markerOptions.position will be ignored."
            |> ObsoleteWithMessage "Deprecated: Marker setup is deprecated. Use the MarkerCustomizationFunction methods for your location provider instead. This field will be removed in the future."
        ]

    let JourneySharingMapView =
        Class "google.maps.journeySharing.JourneySharingMapView"
        |+> Static [
            Constructor JourneySharingMapViewOptions
        ]
        |+> Instance [
            "automaticViewportMode" =@ AutomaticViewportMode
            |> WithComment "This Field is read-only. Automatic viewport mode."

            "element" =@ Element
            |> WithComment "This Field is read-only. The DOM element backing the view."

            "enableTraffic" =@ T<bool>
            |> WithComment "Enables or disables the traffic layer."

            "locationProviders" =@ Type.ArrayOf LocationProvider
            |> WithComment "This field is read-only. Sources of tracked locations to be shown in the tracking map view. To add or remove location providers, use the JourneySharingMapView.addLocationProvider and JourneySharingMapView.removeLocationProvider methods."

            "map" =@ M.Map
            |> WithComment "This Field is read-only. The map object contained in the map view."

            "mapOptions" =@ M.MapOptions
            |> WithComment "This Field is read-only. The map options passed into the map via the map view."

            "locationProvider" =@ LocationProvider
            |> WithComment "This Field is read-only. A source of tracked locations to be shown in the tracking map view."
            |> ObsoleteWithMessage "Deprecated: Use JourneySharingMapView.locationProviders instead."

            "destinationMarkerSetup" =@ MarkerSetup
            |> WithComment "Configures options for a destination location marker. Invoked whenever a new destination marker is rendered.

If specifying a function, the function can and should modify the input's defaultMarkerOptions field containing a google.maps.MarkerOptions object, and return it as markerOptions in the output MarkerSetupOptions object.

Specifying a MarkerSetupOptions object has the same effect as specifying a function that returns that static object.

Do not reuse the same MarkerSetupOptions object in different MarkerSetup functions or static values, and do not reuse the same google.maps.MarkerOptions object for the markerOptions key in different MarkerSetupOptions objects. If markerOptions is unset or null, it will be overwritten with the default. Any value set for markerOptions.map or markerOptions.position will be ignored."
            |> ObsoleteWithMessage "Deprecated: Marker setup is deprecated. Use the MarkerCustomizationFunction methods for your location provider instead. This field will be removed in the future."

            "originMarkerSetup" =@ MarkerSetup
            |> WithComment "Configures options for an origin location marker. Invoked whenever a new origin marker is rendered.

If specifying a function, the function can and should modify the input's defaultMarkerOptions field containing a google.maps.MarkerOptions object, and return it as markerOptions in the output MarkerSetupOptions object.

Specifying a MarkerSetupOptions object has the same effect as specifying a function that returns that static object.

Do not reuse the same MarkerSetupOptions object in different MarkerSetup functions or static values, and do not reuse the same google.maps.MarkerOptions object for the markerOptions key in different MarkerSetupOptions objects. If markerOptions is unset or null, it will be overwritten with the default. Any value set for markerOptions.map or markerOptions.position will be ignored."
            |> ObsoleteWithMessage "Deprecated: Marker setup is deprecated. Use the MarkerCustomizationFunction methods for your location provider instead. This field will be removed in the future."

            "taskOutcomeMarkerSetup" =@ MarkerSetup
            |> WithComment "Configures options for a task outcome location marker. Invoked whenever a new task outcome location marker is rendered.

If specifying a function, the function can and should modify the input's defaultMarkerOptions field containing a google.maps.MarkerOptions object, and return it as markerOptions in the output MarkerSetupOptions object.

Specifying a MarkerSetupOptions object has the same effect as specifying a function that returns that static object.

Do not reuse the same MarkerSetupOptions object in different MarkerSetup functions or static values, and do not reuse the same google.maps.MarkerOptions object for the markerOptions key in different MarkerSetupOptions objects. If markerOptions is unset or null, it will be overwritten with the default. Any value set for markerOptions.map or markerOptions.position will be ignored."
            |> ObsoleteWithMessage "Deprecated: Marker setup is deprecated. Use the MarkerCustomizationFunction methods for your location provider instead. This field will be removed in the future."

            "unsuccessfulTaskMarkerSetup" =@ MarkerSetup
            |> WithComment "Configures options for an unsuccessful task location marker. Invoked whenever a new unsuccessful task marker is rendered.

If specifying a function, the function can and should modify the input's defaultMarkerOptions field containing a google.maps.MarkerOptions object, and return it as markerOptions in the output MarkerSetupOptions object.

Specifying a MarkerSetupOptions object has the same effect as specifying a function that returns that static object.

Do not reuse the same MarkerSetupOptions object in different MarkerSetup functions or static values, and do not reuse the same google.maps.MarkerOptions object for the markerOptions key in different MarkerSetupOptions objects. If markerOptions is unset or null, it will be overwritten with the default. Any value set for markerOptions.map or markerOptions.position will be ignored."
            |> ObsoleteWithMessage "Deprecated: Marker setup is deprecated. Use the MarkerCustomizationFunction methods for your location provider instead. This field will be removed in the future."

            "vehicleMarkerSetup" =@ MarkerSetup
            |> WithComment "Configures options for a vehicle location marker. Invoked whenever a new vehicle marker is rendered.

If specifying a function, the function can and should modify the input's defaultMarkerOptions field containing a google.maps.MarkerOptions object, and return it as markerOptions in the output MarkerSetupOptions object.

Specifying a MarkerSetupOptions object has the same effect as specifying a function that returns that static object.

Do not reuse the same MarkerSetupOptions object in different MarkerSetup functions or static values, and do not reuse the same google.maps.MarkerOptions object for the markerOptions key in different MarkerSetupOptions objects. If markerOptions is unset or null, it will be overwritten with the default. Any value set for markerOptions.map or markerOptions.position will be ignored."
            |> ObsoleteWithMessage "Deprecated: Marker setup is deprecated. Use the MarkerCustomizationFunction methods for your location provider instead. This field will be removed in the future."

            "waypointMarkerSetup" =@ MarkerSetup
            |> WithComment "Configures options for a waypoint location marker. Invoked whenever a new waypoint marker is rendered.

If specifying a function, the function can and should modify the input's defaultMarkerOptions field containing a google.maps.MarkerOptions object, and return it as markerOptions in the output MarkerSetupOptions object.

Specifying a MarkerSetupOptions object has the same effect as specifying a function that returns that static object.

Do not reuse the same MarkerSetupOptions object in different MarkerSetup functions or static values, and do not reuse the same google.maps.MarkerOptions object for the markerOptions key in different MarkerSetupOptions objects. If markerOptions is unset or null, it will be overwritten with the default. Any value set for markerOptions.map or markerOptions.position will be ignored."
            |> ObsoleteWithMessage "Deprecated: Marker setup is deprecated. Use the MarkerCustomizationFunction methods for your location provider instead. This field will be removed in the future."

            "anticipatedRoutePolylineSetup" =@ PolylineSetup
            |> WithComment "Configures options for an anticipated route polyline. Invoked whenever a new anticipated route polyline is rendered.

If specifying a function, the function can and should modify the input's defaultPolylineOptions field containing a google.maps.PolylineOptions object, and return it as polylineOptions in the output PolylineSetupOptions object.

Specifying a PolylineSetupOptions object has the same effect as specifying a function that returns that static object.

Do not reuse the same PolylineSetupOptions object in different PolylineSetup functions or static values, and do not reuse the same google.maps.PolylineOptions object for the polylineOptions key in different PolylineSetupOptions objects. If polylineOptions or visible is unset or null, it will be overwritten with the default. Any values set for polylineOptions.map or polylineOptions.path will be ignored."
            |> ObsoleteWithMessage "Deprecated: Polyline setup is deprecated. Use the PolylineCustomizationFunction methods for your location provider instead. This field will be removed in the future."

            "takenRoutePolylineSetup" =@ PolylineSetup
            |> WithComment "Configures options for a taken route polyline. Invoked whenever a new taken route polyline is rendered.

If specifying a function, the function can and should modify the input's defaultPolylineOptions field containing a google.maps.PolylineOptions object, and return it as polylineOptions in the output PolylineSetupOptions object.

Specifying a PolylineSetupOptions object has the same effect as specifying a function that returns that static object.

Do not reuse the same PolylineSetupOptions object in different PolylineSetup functions or static values, and do not reuse the same google.maps.PolylineOptions object for the polylineOptions key in different PolylineSetupOptions objects.

Any values set for polylineOptions.map or polylineOptions.path will be ignored. Any unset or null value will be overwritten with the default."
            |> ObsoleteWithMessage "Deprecated: Polyline setup is deprecated. Use the PolylineCustomizationFunction methods for your location provider instead. This field will be removed in the future."

            "pingMarkerSetup" =@ MarkerSetup
            |> WithComment "Configures options for a ping location marker. Invoked whenever a new ping marker is rendered.

If specifying a function, the function can and should modify the input's defaultMarkerOptions field containing a google.maps.MarkerOptions object, and return it as markerOptions in the output MarkerSetupOptions object.

Specifying a MarkerSetupOptions object has the same effect as specifying a function that returns that static object.

Do not reuse the same MarkerSetupOptions object in different MarkerSetup functions or static values, and do not reuse the same google.maps.MarkerOptions object for the markerOptions key in different MarkerSetupOptions objects. If markerOptions is unset or null, it will be overwritten with the default. Any value set for markerOptions.map or markerOptions.position will be ignored."
            |> ObsoleteWithMessage "Deprecated: Marker setup is deprecated. Use the MarkerCustomizationFunction methods for your location provider instead. This field will be removed in the future."

            "successfulTaskMarkerSetup" =@ MarkerSetup
            |> WithComment "Configures options for a successful task location marker. Invoked whenever a new successful task marker is rendered.

If specifying a function, the function can and should modify the input's defaultMarkerOptions field containing a google.maps.MarkerOptions object, and return it as markerOptions in the output MarkerSetupOptions object.

Specifying a MarkerSetupOptions object has the same effect as specifying a function that returns that static object.

Do not reuse the same MarkerSetupOptions object in different MarkerSetup functions or static values, and do not reuse the same google.maps.MarkerOptions object for the markerOptions key in different MarkerSetupOptions objects. If markerOptions is unset or null, it will be overwritten with the default. Any value set for markerOptions.map or markerOptions.position will be ignored."
            |> ObsoleteWithMessage "Deprecated: Marker setup is deprecated. Use the MarkerCustomizationFunction methods for your location provider instead. This field will be removed in the future."

            "destinationMarkers" =@ Type.ArrayOf Marker
            |> WithComment "Returns the destination markers, if any."
            |> ObsoleteWithMessage "Deprecated: getting a list of markers via the MapView is deprecated. Use the MarkerCustomizationFunctions for your location provider to receive callbacks when a marker is added to the map or updated."

            "originMarkers" =@ Type.ArrayOf Marker
            |> WithComment "Returns the origin markers, if any."
            |> ObsoleteWithMessage "Deprecated: getting a list of markers via the MapView is deprecated. Use the MarkerCustomizationFunctions for your location provider to receive callbacks when a marker is added to the map or updated."

            "successfulTaskMarkers" =@ Type.ArrayOf Marker
            |> WithComment "Returns the successful task markers, if any."
            |> ObsoleteWithMessage "Deprecated: getting a list of markers via the MapView is deprecated. Use the MarkerCustomizationFunctions for your location provider to receive callbacks when a marker is added to the map or updated."

            "taskOutcomeMarkers" =@ Type.ArrayOf Marker
            |> WithComment "Returns the task outcome markers, if any."
            |> ObsoleteWithMessage "Deprecated: getting a list of markers via the MapView is deprecated. Use the MarkerCustomizationFunctions for your location provider to receive callbacks when a marker is added to the map or updated."

            "unsuccessfulTaskMarkers" =@ Type.ArrayOf Marker
            |> WithComment "Returns the unsuccessful task markers, if any."
            |> ObsoleteWithMessage "Deprecated: getting a list of markers via the MapView is deprecated. Use the MarkerCustomizationFunctions for your location provider to receive callbacks when a marker is added to the map or updated."

            "vehicleMarkers" =@ Type.ArrayOf Marker
            |> WithComment "Returns the vehicle markers, if any."
            |> ObsoleteWithMessage "Deprecated: getting a list of markers via the MapView is deprecated. Use the MarkerCustomizationFunctions for your location provider to receive callbacks when a marker is added to the map or updated."

            "waypointMarkers" =@ Type.ArrayOf Marker
            |> WithComment "Returns the waypoint markers, if any."
            |> ObsoleteWithMessage "Deprecated: getting a list of markers via the MapView is deprecated. Use the MarkerCustomizationFunctions for your location provider to receive callbacks when a marker is added to the map or updated."

            "anticipatedRoutePolylines" =@ Type.ArrayOf Polyline
            |> WithComment "Returns the anticipated route polylines, if any."

            "takenRoutePolylines" =@ Type.ArrayOf Polyline
            |> WithComment "Returns the taken route polylines, if any."
            |> ObsoleteWithMessage "Deprecated: getting a list of polylines via the MapView is deprecated. Use the PolylineCustomizationFunctions for your location provider to receive callbacks when a polyline is added to the map or updated."

            // METHODS
            "addLocationProvider" => LocationProvider ^-> T<unit>
            |> WithComment "Adds a location provider to the map view. If the location provider is already added, no action is performed."

            "removeLocationProvider" => LocationProvider ^-> T<unit>
            |> WithComment "Removes a location provider from the map view. If the location provider is not already added to the map view, no action is performed."
        ]

    let AuthToken =
        Interface "google.maps.journeySharing.AuthToken"
        |+> [
            "expiresInSeconds" =@ T<int>
            |> WithComment "The expiration time in seconds. A token expires in this amount of time after fetching."

            "token" =@ T<int>
            |> WithComment "The token."
        ]

    let AuthTokenContext =
        Interface "google.maps.journeySharing.AuthTokenContext"
        |+> [
            "deliveryVehicleId" =@ T<string>
            |> WithComment "When provided, the minted token should have a private DeliveryVehicleId claim for the provided deliveryVehicleId."

            "taskId" =@ T<string>
            |> WithComment "When provided, the minted token should have a private TaskId claim for the provided taskId."

            "trackingId" =@ T<string>
            |> WithComment "When provided, the minted token should have a private TrackingId claim for the provided trackingId."

            "tripId" =@ T<string>
            |> WithComment "When provided, the minted token should have a private TripId claim for the provided tripId."

            "vehicleId" =@ T<string>
            |> WithComment "When provided, the minted token should have a private VehicleId claim for the provided vehicleId."
        ]

    let FleetEngineServiceType =
        Class "google.maps.journeySharing.FleetEngineServiceType"
        |+> Static [
            "DELIVERY_VEHICLE_SERVICE" =? TSelf
            |> WithComment "Fleet Engine service used to access delivery vehicles."

            "TASK_SERVICE" =? TSelf
            |> WithComment "Fleet Engine service used to access task information."

            "TRIP_SERVICE" =? TSelf
            |> WithComment "Fleet Engine service used to access trip information."

            "UNKNOWN_SERVICE" =? TSelf
            |> WithComment "Unknown Fleet Engine service."
        ]

    let AuthTokenFetcherOptions =
        Interface "google.maps.journeySharing.AuthTokenFetcherOptions"
        |+> [
            "context" =@ AuthTokenContext
            |> WithComment "The auth token context. IDs specified in the context should be added to the request sent to the JSON Web Token minting endpoint."

            "serviceType" =@ FleetEngineServiceType
            |> WithComment "The Fleet Engine service type."
        ]

    //TODO: how to add the namespace?
    let AuthTokenFetcher = AuthTokenFetcherOptions ^-> Promise[AuthToken]

    let MarkerCustomizationFunctionParams =
        Interface "google.maps.journeySharing.MarkerCustomizationFunctionParams"
        |+> [
            "defaultOptions" =@ MarkerOptions
            |> WithComment "The default options used to create this marker."

            "isNew" =@ T<bool>
            |> WithComment "If true, the marker was newly created, and the marker customization function is being called for the first time, before the marker has been added to the map view. False otherwise."

            "marker" =@ Marker
            |> WithComment "The marker. Any customizations should be made to this object directly."
        ]

    let DeliveryVehicleStopState =
        Class "google.maps.journeySharing.DeliveryVehicleStopState"
        |+> Static [
            "ARRIVED" =? TSelf
            |> WithComment "Arrived at stop. Assumes that when the vehicle is routing to the next stop, that all previous stops have been completed."

            "ENROUTE" =? TSelf
            |> WithComment "Assigned and actively routing."

            "NEW" =? TSelf
            |> WithComment "Created, but not actively routing."

            "UNSPECIFIED" =? TSelf
            |> WithComment "Unknown."
        ]

    let TimeWindow =
        Interface "google.maps.journeySharing.TimeWindow"
        |+> [
            "endTime" =@ Date
            |> WithComment "The end time of the time window (inclusive)."

            "startTime" =@ Date
            |> WithComment "The start time of the time window (inclusive)."
        ]

    let TaskInfo =
        Interface "google.maps.journeySharing.TaskInfo"
        |+> [
            "extraDurationMillis" =@ T<int>
            |> WithComment "The extra time it takes to perform the task, in milliseconds."

            "id" =@ T<string>
            |> WithComment "The ID of the task."

            "targetTimeWindow" =@ TimeWindow
            |> WithComment "The time window during which the task should be completed."
        ]

    let DeliveryVehicleStop =
        Interface "google.maps.journeySharing.DeliveryVehicleStop"
        |+> [
            "tasks" =@  Type.ArrayOf TaskInfo
            |> WithComment "The list of Tasks to be performed at this stop.

    id: the ID of the task.
    extraDurationMillis: the extra time it takes to perform the task, in milliseconds."

            "plannedLocation" =@ LatLngLiteral
            |> WithComment "The location of the stop."

            "state" =@ DeliveryVehicleStopState
            |> WithComment "The state of the stop."
        ]

    let VehicleJourneySegment =
        Interface "google.maps.journeySharing.VehicleJourneySegment"
        |+> [
            "drivingDistanceMeters" =@ T<int>
            |> WithComment "The travel distance from the previous stop to this stop, in meters."

            "drivingDurationMillis" =@ T<int>
            |> WithComment "The travel time from the previous stop this stop, in milliseconds."

            "path" =@ Type.ArrayOf LatLngLiteral
            |> WithComment "The path from the previous stop (or the vehicle's current location, if this stop is the first in the list of stops) to this stop."

            "stop" =@ DeliveryVehicleStop
            |> WithComment "Information about the stop."
        ]

    let VehicleLocationUpdate =
        Interface "google.maps.journeySharing.VehicleLocationUpdate"
        |+> [
            "heading" =@ T<int>
            |> WithComment "The heading of the update. 0 corresponds to north, 180 to south."

            "location" =@ LatLngLiteral + LatLng
            |> WithComment "The location of the update."

            "speedKilometersPerHour" =@ T<int>
            |> WithComment "The speed in kilometers per hour."

            "time" =@ Date
            |> WithComment "The time this update was received from the vehicle."
        ]


    let Task =
        Interface "google.maps.journeySharing.Task"
        |+> [
            "attributes" =@ T<System.Collections.Generic.Dictionary<string,_>>
            |> WithComment "Attributes assigned to the task."

            "name" =@ T<string>
            |> WithComment "The task name in the format \"providers/{provider_id}/tasks/{task_id}\". The task_id must be a unique identifier and not a tracking ID. To store a tracking ID of a shipment, use the tracking_id field. Multiple tasks can have the same tracking_id."

            "remainingVehicleJourneySegments" =@ Type.ArrayOf VehicleJourneySegment
            |> WithComment "Information about the segments left to be completed for this task."

            "status" =@ T<string>
            |> WithComment "The current execution state of the task."

            "type" =@ T<string>
            |> WithComment "The task type; for example, a break or shipment."

            "estimatedCompletionTime" =@ Date
            |> WithComment "The timestamp of the estimated completion time of the task."

            "latestVehicleLocationUpdate" =@ VehicleLocationUpdate
            |> WithComment "Information specific to the last location update."

            "outcome" =@ T<string>
            |> WithComment "The outcome of the task."

            "outcomeLocation" =@ LatLngLiteral
            |> WithComment "The location where the task was completed (from provider)."

            "outcomeLocationSource" =@ T<string>
            |> WithComment "The setter of the task outcome location ('PROVIDER' or 'LAST_VEHICLE_LOCATION')"

            "outcomeTime" =@ Date
            |> WithComment "The timestamp of when the task's outcome was set (from provider)."

            "plannedLocation" =@ LatLngLiteral
            |> WithComment "The location where the task is to be completed."

            "targetTimeWindow" =@ TimeWindow
            |> WithComment "The time window during which the task should be completed."

            "trackingId" =@ T<string>
            |> WithComment "The tracking ID of the shipment."

            "vehicleId" =@ T<string>
            |> WithComment "The ID of the vehicle performing this task."
        ]

    let PolylineCustomizationFunctionParams =
        Interface "google.maps.journeySharing.PolylineCustomizationFunctionParams"
        |+> [
            "defaultOptions" =@ PolylineOptions
            |> WithComment "The default options used to create this set of polylines."

            "isNew" =@ T<bool>
            |> WithComment "If true, the list of polylines was newly created, and the polyline customization function is being called for the first time. False otherwise."

            "polylines" =@ Type.ArrayOf Polyline
            |> WithComment "The list of polylines created. They are arranged sequentially to form the rendered route."
        ]

    let TaskTrackingInfo =
        Interface "google.maps.journeySharing.TaskTrackingInfo"
        |+> [
            "attributes" =@ T<System.Collections.Generic.Dictionary<string,_>>
            |> WithComment "Attributes assigned to the task."

            "name" =@ T<string>
            |> WithComment "The name in the format \"providers/{provider_id}/taskTrackingInfo/{tracking_id}\", where tracking_id represents the tracking ID."

            "trackingId" =@ T<string>
            |> WithComment "The tracking ID of a Task.

    Must be a valid Unicode string.
    Limited to a maximum length of 64 characters.
    Normalized according to Unicode Normalization Form C.
    May not contain any of the following ASCII characters: '/', ':', '?', ',', or '#'."

            "estimatedArrivalTime" =@ Date
            |> WithComment "The estimated arrival time to the stop location."

            "estimatedTaskCompletionTime" =@ Date
            |> WithComment "The estimated completion time of a Task."

            "latestVehicleLocationUpdate" =@ VehicleLocationUpdate
            |> WithComment "Information specific to the last location update."

            "plannedLocation" =@ LatLng
            |> WithComment "The location where the Task will be completed."

            "remainingDrivingDistanceMeters" =@ T<int>
            |> WithComment "The total remaining distance in meters to the VehicleStop of interest."

            "remainingStopCount" =@ T<int>
            |> WithComment "Indicates the number of stops the vehicle remaining until the task stop is reached, including the task stop. For example, if the vehicle's next stop is the task stop, the value will be 1."

            "routePolylinePoints" =@ Type.ArrayOf LatLng
            |> WithComment "A list of points which when connected forms a polyline of the vehicle's expected route to the location of this task."

            "state" =@ T<string>
            |> WithComment "The current execution state of the Task."

            "targetTimeWindow" =@ TimeWindow
            |> WithComment "The time window during which the task should be completed."

            "taskOutcome" =@ T<string>
            |> WithComment "The outcome of attempting to execute a Task."

            "taskOutcomeTime" =@ Date
            |> WithComment "The time when the Task's outcome was set by the provider."
        ]

    let ShipmentMarkerCustomizationFunctionParams =
        Interface "google.maps.journeySharing.ShipmentMarkerCustomizationFunctionParams"
        |=> Extends [MarkerCustomizationFunctionParams]
        |+> [
            "taskTrackingInfo" =@ TaskTrackingInfo
            |> WithComment "Information for the task associated with this marker."
        ]

    let ShipmentPolylineCustomizationFunctionParams =
        Interface "google.maps.journeySharing.ShipmentPolylineCustomizationFunctionParams"
        |=> Extends [PolylineCustomizationFunctionParams]
        |+> [
            "taskTrackingInfo" =@ TaskTrackingInfo
            |> WithComment "Information for the task associated with this polyline."
        ]

    let Speed =
        Class "google.maps.journeySharing.Speed"
        |+> Static [
            "NORMAL" =? TSelf
            |> WithComment "Normal speed, no slowdown is detected."

            "SLOW" =? TSelf
            |> WithComment "Slowdown detected, but no traffic jam formed."

            "TRAFFIC_JAM" =? TSelf
            |> WithComment "Traffic jam detected."
        ]

    let SpeedReadingInterval =
        Interface "google.maps.journeySharing.SpeedReadingInterval"
        |+> [
            "endPolylinePointIndex" =@ T<int>
            |> WithComment "The zero-based index of the ending point of the interval in the path."

            "speed" =@ Speed
            |> WithComment "Traffic speed in this interval."

            "startPolylinePointIndex" =@ T<int>
            |> WithComment "The zero-based index of the starting point of the interval in the path."
        ]

    let VehicleWaypoint =
        Interface "google.maps.journeySharing.VehicleWaypoint"
        |+> [
            "distanceMeters" =@ T<int>
            |> WithComment "The path distance between the previous waypoint (or the vehicle's current location, if this waypoint is the first in the list of waypoints) to this waypoint in meters."

            "durationMillis" =@ T<int>
            |> WithComment "Travel time between the previous waypoint (or the vehicle's current location, if this waypoint is the first in the list of waypoints) to this waypoint in milliseconds."

            "location" =@ LatLngLiteral
            |> WithComment "The location of the waypoint."

            "path" =@ Type.ArrayOf LatLngLiteral
            |> WithComment "The path from the previous waypoint (or the vehicle's current location, if this waypoint is the first in the list of waypoints) to this waypoint."

            "speedReadingIntervals" =@ Type.ArrayOf SpeedReadingInterval
            |> WithComment "The list of traffic speeds along the path from the previous waypoint (or vehicle location) to the current waypoint. Each interval in the list describes the traffic on a contiguous segment on the path; the interval defines the starting and ending points of the segment via their indices. See the definition of SpeedReadingInterval for more details."
        ]

    let Trip =
        Interface "google.maps.journeySharing.Trip"
        |+> [
            "name" =@ T<string>
            |> WithComment "In the format \"providers/{provider_id}/trips/{trip_id}\". The trip_id must be a unique identifier."

            "passengerCount" =@ T<int>
            |> WithComment "Number of passengers on this trip; does not include the driver."

            "remainingWaypoints" =@ Type.ArrayOf VehicleWaypoint
            |> WithComment "An array of waypoints indicating the path from the current location to the drop-off point."

            "status" =@ T<string>
            |> WithComment "Current status of the trip. Possible values are UNKNOWN_TRIP_STATUS, NEW, ENROUTE_TO_PICKUP, ARRIVED_AT_PICKUP, ARRIVED_AT_INTERMEDIATE_DESTINATION, ENROUTE_TO_INTERMEDIATE_DESTINATION, ENROUTE_TO_DROPOFF, COMPLETE, or CANCELED."

            "type" =@ T<string>
            |> WithComment "The type of the trip. Possible values are UNKNOWN_TRIP_TYPE, SHARED or EXCLUSIVE."

            "vehicleId" =@ T<string>
            |> WithComment "ID of the vehicle making this trip."

            "actualDropOffLocation" =@ LatLngLiteral
            |> WithComment "Location where the customer was dropped off."

            "actualPickupLocation" =@ LatLngLiteral
            |> WithComment "Location where the customer was picked up."

            "dropOffTime" =@ Date
            |> WithComment "The estimated future time when the passengers will be dropped off, or the actual time when they were dropped off."

            "latestVehicleLocationUpdate" =@ VehicleLocationUpdate
            |> WithComment "Information specific to the last location update."

            "pickupTime" =@ Date
            |> WithComment "The estimated future time when the passengers will be picked up, or the actual time when they were picked up."

            "plannedDropOffLocation" =@ LatLngLiteral
            |> WithComment "Location where the customer indicates they will be dropped off."

            "plannedPickupLocation" =@ LatLngLiteral
            |> WithComment "Location where customer indicates they will be picked up."
        ]

    let TripMarkerCustomizationFunctionParams =
        Interface "google.maps.journeySharing.TripMarkerCustomizationFunctionParams"
        |=> Extends [MarkerCustomizationFunctionParams]
        |+> [
            "trip" =@ Trip
            |> WithComment "The trip associated with this marker.

For information about the vehicle servicing this trip, use Trip.latestVehicleLocationUpdate and Trip.remainingWaypoints."
        ]

    let TripWaypointMarkerCustomizationFunctionParams =
        Interface "google.maps.journeySharing.TripWaypointMarkerCustomizationFunctionParams"
        |=> Extends [TripMarkerCustomizationFunctionParams]
        |+> [
            "waypointIndex" =@ T<int>
            |> WithComment "The 0-based waypoint index associated with this marker. Use this index on Trip.remainingWaypoints to retrieve information about the waypoint."
        ]

    let TripPolylineCustomizationFunctionParams =
        Interface "google.maps.journeySharing.TripPolylineCustomizationFunctionParams"
        |=> Extends [PolylineCustomizationFunctionParams]
        |+> [
            "trip" =@ Trip
            |> WithComment "The trip associated with this polyline."
        ]

    let DeliveryVehicle =
        Interface "google.maps.journeySharing.DeliveryVehicle"
        |+> [
            "attributes" =@ T<System.Collections.Generic.Dictionary<string,_>>
            |> WithComment "Custom delivery vehicle attributes."

            "name" =@ T<string>
            |> WithComment "In the format \"providers/{provider_id}/deliveryVehicles/{delivery_vehicle_id}\". The delivery_vehicle_id must be a unique identifier."

            "navigationStatus" =@ T<string>
            |> WithComment "The current navigation status of the vehicle."

            "remainingDistanceMeters" =@ T<int>
            |> WithComment "The remaining driving distance in the current route segment, in meters."

            "remainingVehicleJourneySegments" =@ Type.ArrayOf VehicleJourneySegment
            |> WithComment "The journey segments assigned to this delivery vehicle, starting from the vehicle's most recently reported location. This is only populated when the DeliveryVehicle data object is provided through FleetEngineDeliveryVehicleLocationProvider."

            "currentRouteSegmentEndPoint" =@ LatLngLiteral
            |> WithComment "The location where the current route segment ends."

            "latestVehicleLocationUpdate" =@ VehicleLocationUpdate
            |> WithComment "The last reported location of the delivery vehicle."

            "remainingDurationMillis" =@ T<int>
            |> WithComment "The remaining driving duration in the current route segment, in milliseconds."
        ]

    let DeliveryVehiclePolylineCustomizationFunctionParams =
        Interface "google.maps.journeySharing.DeliveryVehiclePolylineCustomizationFunctionParams"
        |=> Extends [PolylineCustomizationFunctionParams]
        |+> [
            "deliveryVehicle" =@ DeliveryVehicle
            |> WithComment "The delivery vehicle traversing through this polyline."
        ]

    let DeliveryVehicleMarkerCustomizationFunctionParams =
        Interface "google.maps.journeySharing.DeliveryVehicleMarkerCustomizationFunctionParams"
        |=> Extends [MarkerCustomizationFunctionParams]
        |+> [
            "vehicle" =@ DeliveryVehicle
            |> WithComment "The delivery vehicle represented by this marker."
        ]

    let PlannedStopMarkerCustomizationFunctionParams =
        Interface "google.maps.journeySharing.PlannedStopMarkerCustomizationFunctionParams"
        |=> Extends [DeliveryVehicleMarkerCustomizationFunctionParams]
        |+> [
            "stopIndex" =@ T<int>
            |> WithComment "The 0-based index of this stop in the list of remaining stops."
        ]

    let TaskMarkerCustomizationFunctionParams =
        Interface "google.maps.journeySharing.TaskMarkerCustomizationFunctionParams"
        |=> Extends [DeliveryVehicleMarkerCustomizationFunctionParams]
        |+> [
            "task" =@ Task
            |> WithComment "The task location represented by this marker."
        ]

    let VehicleNavigationStatus =
        Class "google.maps.journeySharing.VehicleNavigationStatus"
        |+> Static [
            "ARRIVED_AT_DESTINATION" =? TSelf
            |> WithComment "The vehicle is within approximately 50m of the destination."

            "ENROUTE_TO_DESTINATION" =? TSelf
            |> WithComment "Turn-by-turn navigation is available and the Driver app navigation has entered GUIDED_NAV mode."

            "NO_GUIDANCE" =? TSelf
            |> WithComment "The Driver app's navigation is in FREE_NAV mode."

            "OFF_ROUTE" =? TSelf
            |> WithComment "The vehicle has gone off the suggested route."

            "UNKNOWN_NAVIGATION_STATUS" =? TSelf
            |> WithComment "Unspecified navigation status."
        ]

    let VehicleState =
        Class "google.maps.journeySharing.VehicleState"
        |+> Static [
            "OFFLINE" =? TSelf
            |> WithComment "The vehicle is not accepting new trips."

            "ONLINE" =? TSelf
            |> WithComment "The vehicle is accepting new trips."

            "UNKNOWN_VEHICLE_STATE" =? TSelf
            |> WithComment "Unknown vehicle state."
        ]

    let VehicleType =
        Class "google.maps.journeySharing.VehicleType"
        |+> Static [
            "AUTO" =? TSelf
            |> WithComment "An automobile."

            "TAXI" =? TSelf
            |> WithComment "Any vehicle that acts as a taxi (typically licensed or regulated)."

            "TRUCK" =? TSelf
            |> WithComment "A vehicle with a large storage capacity."

            "TWO_WHEELER" =? TSelf
            |> WithComment "A motorcycle, moped, or other two-wheeled vehicle."

            "UNKNOWN" =? TSelf
            |> WithComment "Unknown vehicle type."
        ]

    let TripType =
        Class "google.maps.journeySharing.TripType"
        |+> Static [
            "EXCLUSIVE" =? TSelf
            |> WithComment "The trip is exclusive to a vehicle."

            "SHARED" =? TSelf
            |> WithComment "The trip may share a vehicle with other trips."

            "UNKNOWN_TRIP_TYPE" =? TSelf
            |> WithComment "Unknown trip type."
        ]

    let WaypointType =
        Class "google.maps.journeySharing.WaypointType"
        |+> Static [
            "DROP_OFF_WAYPOINT_TYPE" =? TSelf
            |> WithComment "Waypoints for dropping off riders."

            "INTERMEDIATE_DESTINATION_WAYPOINT_TYPE" =? TSelf
            |> WithComment "Waypoints for intermediate destinations in a multi-destination trip."

            "PICKUP_WAYPOINT_TYPE" =? TSelf
            |> WithComment "Waypoints for picking up riders."

            "UNKNOWN_WAYPOINT_TYPE" =? TSelf
            |> WithComment "Unknown waypoint type."
        ]


    let TripWaypoint =
        Interface "google.maps.journeySharing.TripWaypoint "
        |+> [
            "distanceMeters" =@ T<int>
            |> WithComment "The path distance between the previous waypoint (or the vehicle's current location, if this waypoint is the first in the list of waypoints) to this waypoint in meters."

            "durationMillis" =@ T<int>
            |> WithComment "Travel time between the previous waypoint (or the vehicle's current location, if this waypoint is the first in the list of waypoints) to this waypoint in milliseconds."

            "location" =@ LatLng
            |> WithComment "The location of the waypoint."

            "path" =@ Type.ArrayOf LatLng
            |> WithComment "The path from the previous stop (or the vehicle's current location, if this stop is the first in the list of stops) to this stop."

            "speedReadingIntervals" =@ Type.ArrayOf SpeedReadingInterval
            |> WithComment "The list of traffic speeds along the path from the previous waypoint (or vehicle location) to the current waypoint. Each interval in the list describes the traffic on a contiguous segment on the path; the interval defines the starting and ending points of the segment via their indices. See the definition of SpeedReadingInterval for more details."

            "tripId" =@ T<string>
            |> WithComment "The trip associated with this waypoint."

            "waypointType" =@ WaypointType
            |> WithComment "The role this waypoint plays in this trip, such as pickup or dropoff."
        ]

    let Vehicle =
        Interface "google.maps.journeySharing.Vehicle "
        |+> [
            "attributes" =@ T<System.Collections.Generic.Dictionary<string,_>>
            |> WithComment "Custom delivery vehicle attributes."

            "name" =@ T<string>
            |> WithComment "In the format \"providers/{provider_id}/vehicles/{vehicle_id}\". The vehicle_id must be a unique identifier."

            "navigationStatus" =@ VehicleNavigationStatus
            |> WithComment "The current navigation status of the vehicle."

            "remainingDistanceMeters" =@ T<int>
            |> WithComment "The remaining driving distance in the current route segment, in meters."

            "vehicleState" =@ VehicleState
            |> WithComment "The vehicle state."

            "vehicleType" =@ VehicleType
            |> WithComment "The type of this vehicle."

            "currentRouteSegmentEndPoint" =@ TripWaypoint
            |> WithComment "The waypoint where current route segment ends."

            "currentRouteSegmentVersion" =@ Date
            |> WithComment "Time when the current route segment was set."

            "currentTrips" =@ T<string[]>
            |> WithComment "List of trip IDs for trips currently assigned to this vehicle."

            "etaToFirstWaypoint" =@ Date
            |> WithComment "The ETA to the first entry in the waypoints field."

            "latestLocation" =@ VehicleLocationUpdate
            |> WithComment "The last reported location of the vehicle."

            "maximumCapacity" =@ T<int>
            |> WithComment "The total numbers of riders this vehicle can carry. The driver is not considered in this value."

            "supportedTripTypes" =@ Type.ArrayOf TripType
            |> WithComment "Trip types supported by this vehicle."

            "waypoints" =@ Type.ArrayOf TripWaypoint
            |> WithComment "The remaining waypoints assigned to this Vehicle."

            "waypointsVersion" =@ Date
            |> WithComment "Last time the waypoints field was updated."
        ]

    let VehicleMarkerCustomizationFunctionParams =
        Interface "google.maps.journeySharing.VehicleMarkerCustomizationFunctionParams"
        |=> Extends [MarkerCustomizationFunctionParams]
        |+> [
            "vehicle" =@ Vehicle
            |> WithComment "The vehicle represented by this marker."
        ]

    let VehiclePolylineCustomizationFunctionParams =
        Interface "google.maps.journeySharing.VehiclePolylineCustomizationFunctionParams"
        |=> Extends [PolylineCustomizationFunctionParams]
        |+> [
            "vehicle" =@ Vehicle
            |> WithComment "The vehicle traversing through this polyline."
        ]

    let VehicleWaypointMarkerCustomizationFunctionParams =
        Interface "google.maps.journeySharing.VehicleWaypointMarkerCustomizationFunctionParams"
        |=> Extends [VehicleMarkerCustomizationFunctionParams]
        |+> [
            "waypointIndex" =@ T<int>
            |> WithComment "The 0-based waypoint index associated with this marker. Use this index on Vehicle.waypoints to retrieve information about the waypoint."
        ]

    let FleetEngineTripLocationProviderUpdateEvent =
        Interface "google.maps.journeySharing.FleetEngineTripLocationProviderUpdateEvent"
        |+> [
            "trip" =@ Trip
            |> WithComment "The trip structure returned by the update. Unmodifiable."
        ]

    let FleetEngineTripLocationProviderOptions =
        Class "google.maps.journeySharing.FleetEngineTripLocationProviderOptions"
        |+> Instance [
            "authTokenFetcher" =? AuthTokenFetcher
            |> WithComment "Provides JSON Web Tokens for authenticating the client to Fleet Engine."

            "projectId" =? T<string>
            |> WithComment "The consumer's project ID from Google Cloud Console."

            "activePolylineCustomization" =? (T<obj> -* TripPolylineCustomizationFunctionParams ^-> T<unit>) + PolylineOptions
            |> WithComment "Customization applied to the active polyline. An active polyline corresponds to a portion of the route the vehicle is currently traversing through.

Use this field to specify custom styling (such as polyline color) and interactivity (such as click handling).

    If a PolylineOptions object is specified, the changes specified in it are applied to the polyline after the polyline has been created, overwriting its default options if they exist.
    If a function is specified, it is invoked once when the polyline is created. (On this invocation, the isNew parameter in the function parameters object is set to true.) Additionally, this function is invoked when the polyline's coordinates change, or when the location provider receives data from Fleet Engine, regardless of whether the data corresponding to this polyline have changed.

    See TripPolylineCustomizationFunctionParams for a list of supplied parameters and their uses."

            "destinationMarkerCustomization" =? (T<obj> -* TripMarkerCustomizationFunctionParams ^-> T<unit>) + MarkerOptions
            |> WithComment "Customization applied to the destination marker.

Use this field to specify custom styling (such as marker icon) and interactivity (such as click handling).

    If a MarkerOptions object is specified, the changes specified in it are applied to the marker after the marker has been created, overwriting its default options if they exist.
    If a function is specified, it is invoked once when the marker is created, before it is added to the map view. (On this invocation, the isNew parameter in the function parameters object is set to true.) Additionally, this function is invoked when the location provider receives data from Fleet Engine, regardless of whether the data corresponding to this marker have changed.

    See TripMarkerCustomizationFunctionParams for a list of supplied parameters and their uses."

            "originMarkerCustomization" =? (T<obj> -* TripMarkerCustomizationFunctionParams ^-> T<unit>) + MarkerOptions
            |> WithComment "Customization applied to the origin marker.

Use this field to specify custom styling (such as marker icon) and interactivity (such as click handling).

    If a MarkerOptions object is specified, the changes specified in it are applied to the marker after the marker has been created, overwriting its default options if they exist.
    If a function is specified, it is invoked once when the marker is created, before it is added to the map view. (On this invocation, the isNew parameter in the function parameters object is set to true.) Additionally, this function is invoked when the location provider receives data from Fleet Engine, regardless of whether the data corresponding to this marker have changed.

    See TripMarkerCustomizationFunctionParams for a list of supplied parameters and their uses."

            "pollingIntervalMillis" =? T<int>
            |> WithComment "Minimum time between fetching location updates in milliseconds. If it takes longer than pollingIntervalMillis to fetch a location update, the next location update is not started until the current one finishes.

Setting this value to 0 disables recurring location updates. A new location update is fetched if any of the parameters observed by the location provider changes.

The default polling interval is 5000 milliseconds, the minimum interval. If you set the polling interval to a lower non-zero value, 5000 is used."

            "remainingPolylineCustomization" =? (T<obj> -* TripPolylineCustomizationFunctionParams ^-> T<unit>) + PolylineOptions
            |> WithComment "Customization applied to the remaining polyline. A remaining polyline corresponds to a portion of the route the vehicle has not yet started traversing through.

Use this field to specify custom styling (such as polyline color) and interactivity (such as click handling).

    If a PolylineOptions object is specified, the changes specified in it are applied to the polyline after the polyline has been created, overwriting its default options if they exist.
    If a function is specified, it is invoked once when the polyline is created. (On this invocation, the isNew parameter in the function parameters object is set to true.) Additionally, this function is invoked when the polyline's coordinates change, or when the location provider receives data from Fleet Engine, regardless of whether the data corresponding to this polyline have changed.

    See TripPolylineCustomizationFunctionParams for a list of supplied parameters and their uses."

            "takenPolylineCustomization" =? (T<obj> -* TripPolylineCustomizationFunctionParams ^-> T<unit>) + PolylineOptions
            |> WithComment "Customization applied to the taken polyline. A taken polyline corresponds to a portion of the route the vehicle has already traversed through.

Use this field to specify custom styling (such as polyline color) and interactivity (such as click handling).

    If a PolylineOptions object is specified, the changes specified in it are applied to the polyline after the polyline has been created, overwriting its default options if they exist.
    If a function is specified, it is invoked once when the polyline is created. (On this invocation, the isNew parameter in the function parameters object is set to true.) Additionally, this function is invoked when the polyline's coordinates change, or when the location provider receives data from Fleet Engine, regardless of whether the data corresponding to this polyline have changed.

    See TripPolylineCustomizationFunctionParams for a list of supplied parameters and their uses."

            "tripId" =? T<string>
            |> WithComment "The trip ID to track immediately after the location provider is instantiated. If not specified, the location provider does not start tracking any trip; use FleetEngineTripLocationProvider.tripId to set the ID and begin tracking."

            "vehicleMarkerCustomization" =? (T<obj> -* TripMarkerCustomizationFunctionParams ^-> T<unit>) + MarkerOptions
            |> WithComment "Customization applied to the vehicle marker.

Use this field to specify custom styling (such as marker icon) and interactivity (such as click handling).

    If a MarkerOptions object is specified, the changes specified in it are applied to the marker after the marker has been created, overwriting its default options if they exist.
    If a function is specified, it is invoked once when the marker is created, before it is added to the map view. (On this invocation, the isNew parameter in the function parameters object is set to true.) Additionally, this function is invoked when the location provider receives data from Fleet Engine, regardless of whether the data corresponding to this marker have changed.

    See TripMarkerCustomizationFunctionParams for a list of supplied parameters and their uses."

            "waypointMarkerCustomization" =? (T<obj> -* TripWaypointMarkerCustomizationFunctionParams ^-> T<unit>) + MarkerOptions
            |> WithComment "Customization applied to a waypoint marker.

Use this field to specify custom styling (such as marker icon) and interactivity (such as click handling).

    If a MarkerOptions object is specified, the changes specified in it are applied to the marker after the marker has been created, overwriting its default options if they exist.
    If a function is specified, it is invoked once when the marker is created, before it is added to the map view. (On this invocation, the isNew parameter in the function parameters object is set to true.) Additionally, this function is invoked when the location provider receives data from Fleet Engine, regardless of whether the data corresponding to this marker have changed.

    See TripWaypointMarkerCustomizationFunctionParams for a list of supplied parameters and their uses."
        ]

    let FleetEngineTripLocationProvider =
        Class "google.maps.journeySharing.FleetEngineTripLocationProvider"
        |=> Inherits PollingLocationProvider
        |+> Static [
            Constructor FleetEngineTripLocationProviderOptions

            "TRAFFIC_AWARE_ACTIVE_POLYLINE_CUSTOMIZATION_FUNCTION" => TripPolylineCustomizationFunctionParams ^-> T<unit>
            |> WithComment "Polyline customization function that colors the active polyline according to its speed reading. Specify this function as the FleetEngineTripLocationProviderOptions.activePolylineCustomization to render a traffic-aware polyline for the active polyline."

            "TRAFFIC_AWARE_REMAINING_POLYLINE_CUSTOMIZATION_FUNCTION" => TripPolylineCustomizationFunctionParams ^-> T<unit>
            |> WithComment "Polyline customization function that colors the remaining polyline according to its speed reading. Specify this function as the FleetEngineTripLocationProviderOptions.remainingPolylineCustomization to render a traffic-aware polyline for the remaining polyline."
        ]
        |+> Instance [
            "tripId" =@ T<string>
            |> WithComment "The ID for the trip that this location provider observes. Set this field to begin tracking."

            "refresh" => T<unit -> unit>
            |> WithComment "Explicitly refreshes the tracked location."

            // EVENTS
            "error" => T<obj> -* Events.ErrorEvent ^-> T<unit>
            |> WithComment "Event that is triggered when the location provider encounters an error."

            "update" => T<obj> -* FleetEngineTripLocationProviderUpdateEvent ^-> T<unit>
            |> WithComment "Event that is triggered when a Fleet Engine data update request has finished."
        ]

    let FleetEngineTaskFilterOptions =
        Config "google.maps.journeySharing.FleetEngineTaskFilterOptions"
        |+> Instance [
            "completionTimeFrom" =@ Date
            |> WithComment "Exclusive lower bound for the completion time of the task. Used to filter for tasks that were completed after the specified time."

            "completionTimeTo" =@ Date
            |> WithComment "Exclusive upper bound for the completion time of the task. Used to filter for tasks that were completed before the specified time."

            "state" =@ T<string>
            |> WithComment "The state of the task. Valid values are OPEN or CLOSED."
        ]

    let FleetEngineDeliveryVehicleLocationProviderOptions =
        Config "google.maps.journeySharing.FleetEngineDeliveryVehicleLocationProviderOptions"
        |+> Instance [
            "authTokenFetcher" =@ AuthTokenFetcher
            |> WithComment "Provides JSON Web Tokens for authenticating the client to Fleet Engine."

            "projectId" =@ T<string>
            |> WithComment "The consumer's project ID from Google Cloud Console."

            "activePolylineCustomization" =@ (T<obj> -* DeliveryVehiclePolylineCustomizationFunctionParams ^-> T<unit>) + PolylineOptions
            |> WithComment "Customization applied to the active polyline. An active polyline corresponds to a portion of the route the vehicle is currently traversing through.

Use this field to specify custom styling (such as polyline color) and interactivity (such as click handling).

    If a PolylineOptions object is specified, the changes specified in it are applied to the polyline after the polyline has been created, overwriting its default options if they exist.
    If a function is specified, it is invoked once when the polyline is created. (On this invocation, the isNew parameter in the function parameters object is set to true.) Additionally, this function is invoked when the polyline's coordinates change, or when the location provider receives data from Fleet Engine, regardless of whether the data corresponding to this polyline have changed.

    See DeliveryVehiclePolylineCustomizationFunctionParams for a list of supplied parameters and their uses."

            "deliveryVehicleId" =@ T<string>
            |> WithComment "The delivery vehicle ID to track immediately after the location provider is instantiated. If not specified, the location provider does not start tracking any vehicle; use FleetEngineDeliveryVehicleLocationProvider.deliveryVehicleId to set the ID and begin tracking."

            "deliveryVehicleMarkerCustomization" =@ (T<obj> -* DeliveryVehicleMarkerCustomizationFunctionParams ^-> T<unit>) + MarkerOptions
            |> WithComment "Customization applied to the delivery vehicle marker.

Use this field to specify custom styling (such as marker icon) and interactivity (such as click handling).

    If a MarkerOptions object is specified, the changes specified in it are applied to the marker after the marker has been created, overwriting its default options if they exist.
    If a function is specified, it is invoked once when the marker is created, before it is added to the map view. (On this invocation, the isNew parameter in the function parameters object is set to true.) Additionally, this function is invoked when the location provider receives data from Fleet Engine, regardless of whether the data corresponding to this marker have changed.

    See DeliveryVehicleMarkerCustomizationFunctionParams for a list of supplied parameters and their uses."

            "plannedStopMarkerCustomization" =@ (T<obj> -* PlannedStopMarkerCustomizationFunctionParams ^-> T<unit>) + MarkerOptions
            |> WithComment "Customization applied to a planned stop marker.

Use this field to specify custom styling (such as marker icon) and interactivity (such as click handling).

    If a MarkerOptions object is specified, the changes specified in it are applied to the marker after the marker has been created, overwriting its default options if they exist.
    If a function is specified, it is invoked once when the marker is created, before it is added to the map view. (On this invocation, the isNew parameter in the function parameters object is set to true.) Additionally, this function is invoked when the location provider receives data from Fleet Engine, regardless of whether the data corresponding to this marker have changed.

    See PlannedStopMarkerCustomizationFunctionParams for a list of supplied parameters and their uses."

            "pollingIntervalMillis" =@ T<int>
            |> WithComment "Minimum time between fetching location updates in milliseconds. If it takes longer than pollingIntervalMillis to fetch a location update, the next location update is not started until the current one finishes.

Setting this value to 0 disables recurring location updates. A new location update is fetched if any of the parameters observed by the location provider changes.

The default polling interval is 5000 milliseconds, the minimum interval. If you set the polling interval to a lower non-zero value, 5000 is used."

            "remainingPolylineCustomization" =@ (T<obj> -* DeliveryVehiclePolylineCustomizationFunctionParams ^-> T<unit>) + PolylineOptions
            |> WithComment "Customization applied to the remaining polyline. A remaining polyline corresponds to a portion of the route the vehicle has not yet started traversing through.

Use this field to specify custom styling (such as polyline color) and interactivity (such as click handling).

    If a PolylineOptions object is specified, the changes specified in it are applied to the polyline after the polyline has been created, overwriting its default options if they exist.
    If a function is specified, it is invoked once when the polyline is created. (On this invocation, the isNew parameter in the function parameters object is set to true.) Additionally, this function is invoked when the polyline's coordinates change, or when the location provider receives data from Fleet Engine, regardless of whether the data corresponding to this polyline have changed.

    See DeliveryVehiclePolylineCustomizationFunctionParams for a list of supplied parameters and their uses."

            "shouldShowOutcomeLocations" =@ T<bool>
            |> WithComment "Boolean to show or hide outcome locations for the fetched tasks."

            "shouldShowTasks" =@ T<bool>
            |> WithComment "Boolean to show or hide tasks. Setting this to false will prevent the ListTasks endpoint from being called to fetch the tasks. Only the upcoming vehicle stops will be displayed."

            "staleLocationThresholdMillis" =@ T<int>
            |> WithComment "Threshold for stale vehicle location. If the last updated location for the vehicle is older this threshold, the vehicle will not be displayed. Defaults to 24 hours in milliseconds. If the threshold is less than 0, or Infinity, the threshold will be ignored and the vehicle location will not be considered stale."

            "takenPolylineCustomization" =@ (T<obj> -* DeliveryVehiclePolylineCustomizationFunctionParams ^-> T<unit>) + PolylineOptions
            |> WithComment "Customization applied to the taken polyline. A taken polyline corresponds to a portion of the route the vehicle has already traversed through.

Use this field to specify custom styling (such as polyline color) and interactivity (such as click handling).

    If a PolylineOptions object is specified, the changes specified in it are applied to the polyline after the polyline has been created, overwriting its default options if they exist.
    If a function is specified, it is invoked once when the polyline is created. (On this invocation, the isNew parameter in the function parameters object is set to true.) Additionally, this function is invoked when the polyline's coordinates change, or when the location provider receives data from Fleet Engine, regardless of whether the data corresponding to this polyline have changed.

    See DeliveryVehiclePolylineCustomizationFunctionParams for a list of supplied parameters and their uses."

            "taskFilterOptions" =@ FleetEngineTaskFilterOptions
            |> WithComment "Filter options to apply when fetching tasks. The options can include specific vehicle, time, and task status."

            "taskMarkerCustomization" =@ (T<obj> -* TaskMarkerCustomizationFunctionParams ^-> T<unit>) + MarkerOptions
            |> WithComment "Customization applied to a task marker. A task marker is rendered at the planned location of each task assigned to the delivery vehicle.

Use this field to specify custom styling (such as marker icon) and interactivity (such as click handling).

    If a MarkerOptions object is specified, the changes specified in it are applied to the marker after the marker has been created, overwriting its default options if they exist.
    If a function is specified, it is invoked once when the marker is created, before it is added to the map view. (On this invocation, the isNew parameter in the function parameters object is set to true.) Additionally, this function is invoked when the location provider receives data from Fleet Engine, regardless of whether the data corresponding to this marker have changed.

    See TaskMarkerCustomizationFunctionParams for a list of supplied parameters and their uses."

            "taskOutcomeMarkerCustomization" =@ (TaskMarkerCustomizationFunctionParams ^-> T<unit>)
            |> WithComment "Customization applied to a task outcome marker. A task outcome marker is rendered at the actual outcome location of each task assigned to the delivery vehicle.

Use this field to specify custom styling (such as marker icon) and interactivity (such as click handling).

    If a MarkerOptions object is specified, the changes specified in it are applied to the marker after the marker has been created, overwriting its default options if they exist.
    If a function is specified, it is invoked once when the marker is created, before it is added to the map view. (On this invocation, the isNew parameter in the function parameters object is set to true.) Additionally, this function is invoked when the location provider receives data from Fleet Engine, regardless of whether the data corresponding to this marker have changed.

    See TaskMarkerCustomizationFunctionParams for a list of supplied parameters and their uses."
        ]

    let FleetEngineDeliveryVehicleLocationProviderUpdateEvent =
        Interface "google.maps.journeySharing.FleetEngineDeliveryVehicleLocationProviderUpdateEvent"
        |+> [
            "completedVehicleJourneySegments" =@ Type.ArrayOf VehicleJourneySegment
            |> WithComment "The journey segments that have been completed by this vehicle. Unmodifiable."

            "deliveryVehicle" =@ DeliveryVehicle
            |> WithComment "The delivery vehicle data structure returned by the update. Unmodifiable."

            "tasks" =@ Type.ArrayOf Task
            |> WithComment "The list of tasks served by this delivery vehicle. Unmodifiable."
        ]

    let FleetEngineDeliveryVehicleLocationProvider =
        Class "google.maps.journeySharing.FleetEngineDeliveryVehicleLocationProvider"
        |=> Inherits PollingLocationProvider
        |+> Static [
            Constructor FleetEngineDeliveryVehicleLocationProviderOptions
            |> WithComment "Creates a new location provider for a Fleet Engine delivery vehicle."
        ]
        |+> Instance [
            "deliveryVehicleId" =? T<string>
            |> WithComment "ID for the vehicle that this location provider observes. Set this field to track a vehicle."

            "shouldShowOutcomeLocations" =? T<bool>
            |> WithComment "Optionally allow users to display the task's outcome location."

            "shouldShowTasks" =? T<bool>
            |> WithComment "Optionally allow users to display fetched tasks."

            "staleLocationThresholdMillis" =? T<int>
            |> WithComment "This Field is read-only. Threshold for stale vehicle location. If the last updated location for the vehicle is older than this threshold, the vehicle will not be displayed."

            "taskFilterOptions" =? FleetEngineTaskFilterOptions
            |> WithComment "Returns the filter options to apply when fetching tasks."

            // EVENTS
            "error" => T<obj> -* Events.ErrorEvent ^-> T<unit>
            |> WithComment "Event that is triggered when the location provider encounters an error."

            "update" => T<obj> -* FleetEngineDeliveryVehicleLocationProviderUpdateEvent ^-> T<unit>
            |> WithComment "Event that is triggered when a Fleet Engine data update request has finished."
        ]

    let FleetEngineDeliveryFleetLocationProviderOptions =
        Config "google.maps.journeySharing.FleetEngineDeliveryFleetLocationProviderOptions"
        |+> Instance [
            "authTokenFetcher" =@ AuthTokenFetcher
            |> WithComment "Provides JSON Web Tokens for authenticating the client to Fleet Engine."

            "projectId" =@ T<string>
            |> WithComment "The consumer's project ID from Google Cloud Console."

            "deliveryVehicleFilter" =@ T<string>
            |> WithComment "A filter query to apply when fetching delivery vehicles. This filter is passed directly to Fleet Engine.

See ListDeliveryVehiclesRequest.filter for supported formats.

Note that valid filters for attributes must have the \"attributes\" prefix. For example, attributes.x = \"y\" or attributes.\"x y\" = \"z\"."

            "deliveryVehicleMarkerCustomization" =@ T<obj> ^-> DeliveryVehicleMarkerCustomizationFunctionParams ^-> T<unit>
            |> WithComment "Customization applied to a delivery vehicle marker.

Use this field to specify custom styling (such as marker icon) and interactivity (such as click handling).

    If a MarkerOptions object is specified, the changes specified in it are applied to the marker after the marker has been created, overwriting its default options if they exist.
    If a function is specified, it is invoked once when the marker is created, before it is added to the map view. (On this invocation, the isNew parameter in the function parameters object is set to true.) Additionally, this function is invoked when the location provider receives data from Fleet Engine, regardless of whether the data corresponding to this marker have changed.

    See DeliveryVehicleMarkerCustomizationFunctionParams for a list of supplied parameters and their uses."

            "locationRestriction" =@ LatLngBounds + LatLngBoundsLiteral
            |> WithComment "The latitude/longitude bounds within which to track vehicles immediately after the location provider is instantiated. If not set, the location provider does not start tracking any vehicles; use FleetEngineDeliveryFleetLocationProvider.locationRestriction to set the bounds and begin tracking. To track all delivery vehicles regardless of location, set bounds equivalent to the entire earth."

            "staleLocationThresholdMillis" =@ T<int>
            |> WithComment "Threshold for stale vehicle location. If the last updated location for the vehicle is older this threshold, the vehicle will not be displayed. Defaults to 24 hours in milliseconds. If the threshold is less than zero, or Infinity, the threshold will be ignored and the vehicle location will not be considered stale."
        ]

    let FleetEngineDeliveryFleetLocationProviderUpdateEvent =
        Interface "google.maps.journeySharing.FleetEngineDeliveryFleetLocationProviderUpdateEvent"
        |+> [
            "deliveryVehicles" =@ Type.ArrayOf DeliveryVehicle
            |> WithComment "The list of delivery vehicles returned by the query. Unmodifiable."
        ]

    let FleetEngineDeliveryFleetLocationProvider =
        Class "google.maps.journeySharing.FleetEngineDeliveryFleetLocationProvider"
        |=> Inherits PollingLocationProvider
        |+> Static [
            Constructor FleetEngineDeliveryFleetLocationProviderOptions
        ]
        |+> Instance [
            "deliveryVehicleFilter" =@ T<string>
            |> WithComment "The filter applied when fetching the delivery vehicles."

            "locationRestriction" =@  LatLngBounds + LatLngBoundsLiteral
            |> WithComment "The bounds within which to track delivery vehicles. If no bounds are set, no delivery vehicles will be tracked. To track all delivery vehicles regardless of location, set bounds equivalent to the entire earth."

            "staleLocationThresholdMillis" =@ T<int>
            |> WithComment "This Field is read-only. Threshold for stale vehicle location. If the last updated location for the vehicle is older than this threshold, the vehicle will not be displayed."

            // EVENTS
            "update" => T<obj> -* FleetEngineDeliveryFleetLocationProviderUpdateEvent ^-> T<unit>
            |> WithComment "Event that is triggered when a Fleet Engine data update request has finished."
        ]

    let FleetEngineVehicleLocationProviderOptions =
        Config "google.maps.journeySharing.FleetEngineVehicleLocationProviderOptions"
        |+> Instance [
            "authTokenFetcher" =@ AuthTokenFetcher
            |> WithComment "Provides JSON Web Tokens for authenticating the client to Fleet Engine."

            "projectId" =@ T<string>
            |> WithComment "The consumer's project ID from Google Cloud Console."

            "activePolylineCustomization" =@ (T<obj> -* VehiclePolylineCustomizationFunctionParams ^-> T<unit>) + PolylineOptions
            |> WithComment "Customization applied to the active polyline. An active polyline corresponds to a portion of the route the vehicle is currently traversing through.

Use this field to specify custom styling (such as polyline color) and interactivity (such as click handling).

    If a PolylineOptions object is specified, the changes specified in it are applied to the polyline after the polyline has been created, overwriting its default options if they exist.
    If a function is specified, it is invoked once when the polyline is created. (On this invocation, the isNew parameter in the function parameters object is set to true.) Additionally, this function is invoked when the polyline's coordinates change, or when the location provider receives data from Fleet Engine, regardless of whether the data corresponding to this polyline have changed.

    See VehiclePolylineCustomizationFunctionParams for a list of supplied parameters and their uses."

            "destinationMarkerCustomization" =@ (T<obj> -* VehicleWaypointMarkerCustomizationFunctionParams ^-> T<unit>) + MarkerOptions
            |> WithComment "Customization applied to the vehicle trip destination marker.

Use this field to specify custom styling (such as marker icon) and interactivity (such as click handling).

    If a MarkerOptions object is specified, the changes specified in it are applied to the marker after the marker has been created, overwriting its default options if they exist.
    If a function is specified, it is invoked once when the marker is created, before it is added to the map view. (On this invocation, the isNew parameter in the function parameters object is set to true.) Additionally, this function is invoked when the location provider receives data from Fleet Engine, regardless of whether the data corresponding to this marker have changed.

    See VehicleWaypointMarkerCustomizationFunctionParams for a list of supplied parameters and their uses."

            "intermediateDestinationMarkerCustomization" =@ (T<obj> -* VehicleWaypointMarkerCustomizationFunctionParams ^-> T<unit>) + MarkerOptions
            |> WithComment "Customization applied to the vehicle trip intermediate destination markers.

Use this field to specify custom styling (such as marker icon) and interactivity (such as click handling).

    If a MarkerOptions object is specified, the changes specified in it are applied to the marker after the marker has been created, overwriting its default options if they exist.
    If a function is specified, it is invoked once when the marker is created, before it is added to the map view. (On this invocation, the isNew parameter in the function parameters object is set to true.) Additionally, this function is invoked when the location provider receives data from Fleet Engine, regardless of whether the data corresponding to this marker have changed.

    See VehicleWaypointMarkerCustomizationFunctionParams for a list of supplied parameters and their uses."

            "originMarkerCustomization" =@ (T<obj> -* VehicleWaypointMarkerCustomizationFunctionParams ^-> T<unit>) + MarkerOptions
            |> WithComment "Customization applied to the vehicle trip origin marker.

Use this field to specify custom styling (such as marker icon) and interactivity (such as click handling).

    If a MarkerOptions object is specified, the changes specified in it are applied to the marker after the marker has been created, overwriting its default options if they exist.
    If a function is specified, it is invoked once when the marker is created, before it is added to the map view. (On this invocation, the isNew parameter in the function parameters object is set to true.) Additionally, this function is invoked when the location provider receives data from Fleet Engine, regardless of whether the data corresponding to this marker have changed.

    See VehicleWaypointMarkerCustomizationFunctionParams for a list of supplied parameters and their uses."

            "pollingIntervalMillis" =@ T<int>
            |> WithComment "Minimum time between fetching location updates in milliseconds. If it takes longer than pollingIntervalMillis to fetch a location update, the next location update is not started until the current one finishes.

Setting this value to 0 disables recurring location updates. A new location update is fetched if any of the parameters observed by the location provider changes.

The default polling interval is 5000 milliseconds, the minimum interval. If you set the polling interval to a lower non-zero value, 5000 is used."

            "remainingPolylineCustomization" =@ (T<obj> -* VehiclePolylineCustomizationFunctionParams ^-> T<unit>) + PolylineOptions
            |> WithComment "Customization applied to the remaining polyline. A remaining polyline corresponds to a portion of the route the vehicle has not yet started traversing through.

Use this field to specify custom styling (such as polyline color) and interactivity (such as click handling).

    If a PolylineOptions object is specified, the changes specified in it are applied to the polyline after the polyline has been created, overwriting its default options if they exist.
    If a function is specified, it is invoked once when the polyline is created. (On this invocation, the isNew parameter in the function parameters object is set to true.) Additionally, this function is invoked when the polyline's coordinates change, or when the location provider receives data from Fleet Engine, regardless of whether the data corresponding to this polyline have changed.

    See VehiclePolylineCustomizationFunctionParams for a list of supplied parameters and their uses."

            "staleLocationThresholdMillis" =@ T<int>
            |> WithComment "Threshold for stale vehicle location. If the last updated location for the vehicle is older this threshold, the vehicle will not be displayed. Defaults to 24 hours in milliseconds. If the threshold is less than 0, or Infinity, the threshold will be ignored and the vehicle location will not be considered stale."

            "takenPolylineCustomization" =@ (T<obj> -* VehiclePolylineCustomizationFunctionParams ^-> T<unit>) + PolylineOptions
            |> WithComment "Customization applied to the taken polyline. A taken polyline corresponds to a portion of the route the vehicle has already traversed through.

Use this field to specify custom styling (such as polyline color) and interactivity (such as click handling).

    If a PolylineOptions object is specified, the changes specified in it are applied to the polyline after the polyline has been created, overwriting its default options if they exist.
    If a function is specified, it is invoked once when the polyline is created. (On this invocation, the isNew parameter in the function parameters object is set to true.) Additionally, this function is invoked when the polyline's coordinates change, or when the location provider receives data from Fleet Engine, regardless of whether the data corresponding to this polyline have changed.

    See VehiclePolylineCustomizationFunctionParams for a list of supplied parameters and their uses."

            "vehicleId" =@ T<string>
            |> WithComment "The vehicle ID to track immediately after the location provider is instantiated. If not specified, the location provider does not start tracking any vehicle; use FleetEngineVehicleLocationProvider.vehicleId to set the ID and begin tracking."

            "vehicleMarkerCustomization" =@ (T<obj> -* VehicleMarkerCustomizationFunctionParams ^-> T<unit>) + MarkerOptions
            |> WithComment "Customization applied to the vehicle marker.

Use this field to specify custom styling (such as marker icon) and interactivity (such as click handling).

    If a MarkerOptions object is specified, the changes specified in it are applied to the marker after the marker has been created, overwriting its default options if they exist.
    If a function is specified, it is invoked once when the marker is created, before it is added to the map view. (On this invocation, the isNew parameter in the function parameters object is set to true.) Additionally, this function is invoked when the location provider receives data from Fleet Engine, regardless of whether the data corresponding to this marker have changed.

    See VehicleMarkerCustomizationFunctionParams for a list of supplied parameters and their uses."
        ]

    let FleetEngineVehicleLocationProviderUpdateEvent =
        Interface "google.maps.journeySharing.FleetEngineVehicleLocationProviderUpdateEvent"
        |+> [
            "trips" =@ Type.ArrayOf Trip
            |> WithComment "The list of trips completed by this vehicle. Unmodifiable."

            "vehicle" =@ Vehicle
            |> WithComment "The vehicle data structure returned by the update. Unmodifiable."
        ]

    let FleetEngineVehicleLocationProvider =
        Class "google.maps.journeySharing.FleetEngineVehicleLocationProvider"
        |=> Inherits PollingLocationProvider
        |+> Static [
            Constructor FleetEngineVehicleLocationProviderOptions

            "TRAFFIC_AWARE_ACTIVE_POLYLINE_CUSTOMIZATION_FUNCTION" => VehiclePolylineCustomizationFunctionParams ^-> T<unit>
            |> WithComment "Polyline customization function that colors the active polyline according to its speed reading. Specify this function as the FleetEngineVehicleLocationProviderOptions.activePolylineCustomization to render a traffic-aware polyline for the active polyline."

            "TRAFFIC_AWARE_REMAINING_POLYLINE_CUSTOMIZATION_FUNCTION" => VehiclePolylineCustomizationFunctionParams ^-> T<unit>
            |> WithComment "Polyline customization function that colors the remaining polyline according to its speed reading. Specify this function as the FleetEngineVehicleLocationProviderOptions.remainingPolylineCustomization to render a traffic-aware polyline for the remaining polyline."
        ]
        |+> Instance [
            "staleLocationThresholdMillis" =? T<unit>
            |> WithComment "This Field is read-only. Threshold for stale vehicle location. If the last updated location for the vehicle is older than this threshold, the vehicle will not be displayed."

            "vehicleId" =@ T<string>
            |> WithComment "ID for the vehicle that this location provider observes. Set this field to track a vehicle."

            // EVENTS
            "error" => T<obj> -* Events.ErrorEvent ^-> T<unit>
            |> WithComment "Event that is triggered when the location provider encounters an error."

            "update" => T<obj> -* FleetEngineVehicleLocationProviderUpdateEvent ^-> T<unit>
            |> WithComment "Event that is triggered when a Fleet Engine data update request has finished."
        ]

    let FleetEngineFleetLocationProviderOptions =
        Config "google.maps.journeySharing.FleetEngineFleetLocationProviderOptions"
        |+> Instance [
            "authTokenFetcher" =@ AuthTokenFetcher
            |> WithComment "Provides JSON Web Tokens for authenticating the client to Fleet Engine."

            "projectId" =@ T<string>
            |> WithComment "The consumer's project ID from Google Cloud Console."

            "locationRestriction" =@ LatLngBounds + LatLngBoundsLiteral
            |> WithComment "The latitude/longitude bounds within which to track vehicles immediately after the location provider is instantiated. If not set, the location provider does not start tracking any vehicles; use FleetEngineFleetLocationProvider.locationRestriction to set the bounds and begin tracking. To track all vehicles regardless of location, set bounds equivalent to the entire earth."

            "staleLocationThresholdMillis" =@ T<int>
            |> WithComment "Threshold for stale vehicle location. If the last updated location for the vehicle is older than this threshold, the vehicle will not be displayed. Defaults to 24 hours in milliseconds. If the threshold is less than zero, or Infinity, the threshold will be ignored and the vehicle location will not be considered stale."

            "vehicleFilter" =@ T<string>
            |> WithComment "A filter query to apply when fetching vehicles. This filter is passed directly to Fleet Engine.

See ListVehiclesRequest.filter for supported formats.

Note that valid filters for attributes must have the \"attributes\" prefix. For example, attributes.x = \"y\" or attributes.\"x y\" = \"z\"."

            "vehicleMarkerCustomization" =@ T<obj> -* VehicleMarkerCustomizationFunctionParams ^-> T<unit>
            |> WithComment "Customization applied to a vehicle marker.

Use this field to specify custom styling (such as marker icon) and interactivity (such as click handling).

    If a MarkerOptions object is specified, the changes specified in it are applied to the marker after the marker has been created, overwriting its default options if they exist.
    If a function is specified, it is invoked once when the marker is created, before it is added to the map view. (On this invocation, the isNew parameter in the function parameters object is set to true.) Additionally, this function is invoked when the location provider receives data from Fleet Engine, regardless of whether the data corresponding to this marker have changed.

    See VehicleMarkerCustomizationFunctionParams for a list of supplied parameters and their uses."
        ]

    let FleetEngineFleetLocationProviderUpdateEvent =
        Interface "google.maps.journeySharing.FleetEngineFleetLocationProviderUpdateEvent"
        |+> [
            "vehicles" =@ Type.ArrayOf Vehicle
            |> WithComment "The list of vehicles returned by the query. Unmodifiable."
        ]

    let FleetEngineFleetLocationProvider =
        Class "google.maps.journeySharing.FleetEngineFleetLocationProvider"
        |=> Inherits PollingLocationProvider
        |+> Static [
            Constructor FleetEngineFleetLocationProviderOptions
        ]
        |+> Instance [
            "locationRestriction" =@ LatLngBounds + LatLngBoundsLiteral
            |> WithComment "The bounds within which to track vehicles. If no bounds are set, no vehicles will be tracked. To track all vehicles regardless of location, set bounds equivalent to the entire earth."

            "staleLocationThresholdMillis" =@ T<int>
            |> WithComment "This Field is read-only. Threshold for stale vehicle location. If the last updated location for the vehicle is older than this threshold, the vehicle will not be displayed."

            "vehicleFilter" =@ T<string>
            |> WithComment "The filter applied when fetching the vehicles."

            // EVENTS
            "update" => T<obj> -* FleetEngineFleetLocationProviderUpdateEvent ^-> T<unit>
            |> WithComment "Event that is triggered when a Fleet Engine data update request has finished."
        ]

    let FleetEngineShipmentLocationProviderOptions =
        Config "google.maps.journeySharing.FleetEngineShipmentLocationProviderOptions"
        |+> Instance [
            "authTokenFetcher" =@ AuthTokenFetcher
            |> WithComment "Provides JSON Web Tokens for authenticating the client to Fleet Engine."

            "projectId" =@ T<string>
            |> WithComment "The consumer's project ID from Google Cloud Console."

            "activePolylineCustomization" =@  (T<obj> -* ShipmentPolylineCustomizationFunctionParams ^-> T<unit>) + PolylineOptions
            |> WithComment "Customization applied to the active polyline. An active polyline corresponds to a portion of the route the vehicle is currently traversing through.

Use this field to specify custom styling (such as polyline color) and interactivity (such as click handling).

    If a PolylineOptions object is specified, the changes specified in it are applied to the polyline after the polyline has been created, overwriting its default options if they exist.
    If a function is specified, it is invoked once when the polyline is created. (On this invocation, the isNew parameter in the function parameters object is set to true.) Additionally, this function is invoked when the polyline's coordinates change, or when the location provider receives data from Fleet Engine, regardless of whether the data corresponding to this polyline have changed.

    See ShipmentPolylineCustomizationFunctionParams for a list of supplied parameters and their uses."

            "deliveryVehicleMarkerCustomization" =@ (T<obj> -* ShipmentMarkerCustomizationFunctionParams ^-> T<unit>) + MarkerOptions
            |> WithComment "Customization applied to the delivery vehicle marker.

Use this field to specify custom styling (such as marker icon) and interactivity (such as click handling).

    If a MarkerOptions object is specified, the changes specified in it are applied to the marker after the marker has been created, overwriting its default options if they exist.
    If a function is specified, it is invoked once when the marker is created, before it is added to the map view. (On this invocation, the isNew parameter in the function parameters object is set to true.) Additionally, this function is invoked when the location provider receives data from Fleet Engine, regardless of whether the data corresponding to this marker have changed.

    See ShipmentMarkerCustomizationFunctionParams for a list of supplied parameters and their uses."

            "destinationMarkerCustomization" =@ (T<obj> -* ShipmentMarkerCustomizationFunctionParams ^-> T<unit>) + MarkerOptions
            |> WithComment "Customization applied to the destination marker.

Use this field to specify custom styling (such as marker icon) and interactivity (such as click handling).

    If a MarkerOptions object is specified, the changes specified in it are applied to the marker after the marker has been created, overwriting its default options if they exist.
    If a function is specified, it is invoked once when the marker is created, before it is added to the map view. (On this invocation, the isNew parameter in the function parameters object is set to true.) Additionally, this function is invoked when the location provider receives data from Fleet Engine, regardless of whether the data corresponding to this marker have changed.

    See ShipmentMarkerCustomizationFunctionParams for a list of supplied parameters and their uses."

            "pollingIntervalMillis" =@ T<int>
            |> WithComment "Minimum time between fetching location updates in milliseconds. If it takes longer than pollingIntervalMillis to fetch a location update, the next location update is not started until the current one finishes.

Setting this value to 0, Infinity, or a negative value disables automatic location updates. A new location update is fetched once if the tracking ID parameter (for example, the shipment tracking ID of the shipment location provider), or a filtering option (for example, viewport bounds or attribute filters for fleet location providers) changes.

The default, and minimum, polling interval is 5000 milliseconds. If you set the polling interval to a lower positive value, 5000 is stored and used."

            "remainingPolylineCustomization" =@ (T<obj> -* ShipmentPolylineCustomizationFunctionParams ^-> T<unit>) + PolylineOptions
            |> WithComment "Customization applied to the remaining polyline. A remaining polyline corresponds to a portion of the route the vehicle has not yet started traversing through.

Use this field to specify custom styling (such as polyline color) and interactivity (such as click handling).

    If a PolylineOptions object is specified, the changes specified in it are applied to the polyline after the polyline has been created, overwriting its default options if they exist.
    If a function is specified, it is invoked once when the polyline is created. (On this invocation, the isNew parameter in the function parameters object is set to true.) Additionally, this function is invoked when the polyline's coordinates change, or when the location provider receives data from Fleet Engine, regardless of whether the data corresponding to this polyline have changed.

    See ShipmentPolylineCustomizationFunctionParams for a list of supplied parameters and their uses."

            "takenPolylineCustomization" =@ (T<obj> -* ShipmentPolylineCustomizationFunctionParams ^-> T<unit>) + PolylineOptions
            |> WithComment "Customization applied to the taken polyline. A taken polyline corresponds to a portion of the route the vehicle has already traversed through.

Use this field to specify custom styling (such as polyline color) and interactivity (such as click handling).

    If a PolylineOptions object is specified, the changes specified in it are applied to the polyline after the polyline has been created, overwriting its default options if they exist.
    If a function is specified, it is invoked once when the polyline is created. (On this invocation, the isNew parameter in the function parameters object is set to true.) Additionally, this function is invoked when the polyline's coordinates change, or when the location provider receives data from Fleet Engine, regardless of whether the data corresponding to this polyline have changed.

    See ShipmentPolylineCustomizationFunctionParams for a list of supplied parameters and their uses."

            "trackingId" =@ T<string>
            |> WithComment "The tracking ID of the task to track immediately after the location provider is instantiated. If not specified, the location provider does not start tracking any task; use FleetEngineShipmentLocationProvider.trackingId to set the tracking ID and begin tracking."
        ]

    let FleetEngineShipmentLocationProviderUpdateEvent =
        Interface "google.maps.journeySharing.FleetEngineShipmentLocationProviderUpdateEvent"
        |+> [
            "taskTrackingInfo" =@ TaskTrackingInfo
            |> WithComment "The task tracking info structure returned by the update. Unmodifiable."
        ]

    let FleetEngineShipmentLocationProvider =
        Class "google.maps.journeySharing.FleetEngineShipmentLocationProvider"
        |=> Inherits PollingLocationProvider
        |+> Static [
            Constructor FleetEngineShipmentLocationProviderOptions
        ]
        |+> Instance [
            "trackingId" =@ T<string>
            |> WithComment "The tracking ID for the task that this location provider observes. Set this field to begin tracking."

            "refresh" => T<unit -> unit>
            |> WithComment "Explicitly refreshes the tracked location."

            // EVENTS
            "error" => T<obj> -* Events.ErrorEvent ^-> T<unit>
            |> WithComment "Event that is triggered when the location provider encounters an error."

            "update" => T<obj> -* FleetEngineShipmentLocationProviderUpdateEvent ^-> T<unit>
            |> WithComment "Event that is triggered when a Fleet Engine data update request has finished."
        ]
