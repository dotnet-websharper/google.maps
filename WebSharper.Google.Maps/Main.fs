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
namespace WebSharper.Google.Maps.Definition

module Main =

    open WebSharper.InterfaceGenerator
    open Specification

    module Res =
        let Js =
            //Resource "Js" "https://maps.googleapis.com/maps/api/js?key=AIzaSyBclwK3qO1qtBU_OgZzlZ_oVv1bkyUXaF0&sensor=true"
            Resource "Js" "https://maps.googleapis.com/maps/api/js?key=AIzaSyBclwK3qO1qtBU_OgZzlZ_oVv1bkyUXaF0&libraries=visualization&sensor=true"

    let Assembly =
        Assembly [
            Namespace "WebSharper.Google.Maps.Resources" [
                Res.Js
            ]
            Namespace "WebSharper.Google.Maps" [

                Forward.MapTypeId

                Events.ErrorEvent
                Events.Event
                Events.MapsEventListener

                MVC.MVCObject
                MVC.MVCArray

                Base.LatLngLiteral
                Base.LatLng
                Base.LatLngBounds
                Base.LatLngBoundsLiteral
                Base.LatLngAltitudeLiteral
                Base.LatLngAltitude
                Base.Padding
                Base.Point
                Base.Size

                Controls.ControlPosition
                Controls.FullscreenControlOptions
                Controls.MapTypeControlOptions
                Controls.MapTypeControlStyle
                Controls.MotionTrackingControlOptions
                Controls.PanControlOptions
                Controls.RotateControlOptions
                Controls.ScaleControlOptions
                Controls.ScaleControlStyle
                Controls.StreetViewControlOptions
                Controls.ZoomControlOptions

                Drawing.OverlayType
                Drawing.OverlayCompleteEvent
                Drawing.DrawingControlOptions
                Drawing.DrawingManager
                Drawing.DrawingManagerOptions

                StreetView.PanoProviderOptions
                StreetView.StreetViewAddressControlOptions
                StreetView.StreetViewCoverageLayer
                StreetView.StreetViewLink
                StreetView.StreetViewLocation
                StreetView.StreetViewLocationRequest
                StreetView.StreetViewPanorama
                StreetView.StreetViewPanoramaData
                StreetView.StreetViewPanoramaOptions
                StreetView.StreetViewPanoRequest
                StreetView.StreetViewPov
                StreetView.StreetViewPreference
                StreetView.StreetViewResponse
                StreetView.StreetViewService
                StreetView.StreetViewStatus
                StreetView.StreetViewTileData
                StreetView.StreetViewSource

                Styling.DatasetFeature
                Styling.Feature
                Styling.FeatureLayer
                Styling.FeatureMouseEvent
                Styling.FeatureStyleFunctionOptions
                Styling.FeatureStyleOptions
                Styling.FeatureType
                Styling.PlaceFeature

                LocalContext.LocalContextMapView
                LocalContext.LocalContextMapViewOptions
                LocalContext.MapDirectionsOptions
                LocalContext.MapDirectionsOptionsLiteral
                LocalContext.PinOptions
                LocalContext.PinOptionsSetupObject
                LocalContext.PlaceChooserLayoutMode
                LocalContext.PlaceChooserPosition
                LocalContext.PlaceChooserViewSetupObject
                LocalContext.PlaceChooserViewSetupOptions
                LocalContext.PlaceDetailsLayoutMode
                LocalContext.PlaceDetailsPosition
                LocalContext.PlaceDetailsViewSetup
                LocalContext.PlaceDetailsViewSetupOptions
                LocalContext.PlaceTypePreference

                MapTypes.ImageMapType
                MapTypes.ImageMapTypeOptions
                MapTypes.MapType
                MapTypes.MapTypeRegistry
                MapTypes.MapTypeStyle
                MapTypes.MapTypeStyleElementType
                MapTypes.MapTypeStyleFeatureType
                MapTypes.MapTypeStyler
                MapTypes.Projection
                MapTypes.StyledMapType
                MapTypes.StyledMapTypeOptions
                MapTypes.Visibility

                Map.CameraOptions
                Map.IconMouseEvent
                Map.Map
                Map.MapCapabilities
                Map.MapElement
                Map.MapElementOptions
                Map.MapMouseEvent
                Map.MapOptions
                Map.MapRestriction
                Map.RenderingType
                Map.VisibleRegion
                Map.ZoomChangeEvent

                WebGL.CameraParams
                WebGL.CoordinateTransformer
                WebGL.WebGLDrawOptions
                WebGL.WebGLOverlayView
                WebGL.WebGLStateOptions

                Visualization.HeatmapLayer
                Visualization.HeatmapLayerOptions
                Visualization.WeightedLocation

                Geometry.Encoding
                Geometry.Poly
                Geometry.Spherical

                AdvancedMarkerClickEvent
                AdvancedMarkerElement
                AdvancedMarkerElementOptions
                Animation
                BicyclingLayer
                Circle
                CircleLiteral
                CircleOptions
                CollisionBehavior
                DirectionsGeocodedWaypoint
                DirectionsLeg
                DirectionsPolyline
                DirectionsRenderer
                DirectionsRendererOptions
                DirectionsRequest
                DirectionsResult
                DirectionsRoute
                DirectionsService
                DirectionsStatus
                DirectionsStep
                DirectionsWaypoint
                Distance
                DistanceMatrixService
                DistanceMatrixRequest
                DistanceMatrixResponse
                DistanceMatrixResponseRow
                DistanceMatrixResponseElement
                DistanceMatrixStatus
                DistanceMatrixElementStatus
                DrivingOptions
                Duration
                ElevationResult
                ElevationService
                ElevationStatus
                Geocoder
                GeocoderAddressComponent
                GeocoderComponentRestrictions
                GeocoderGeometry
                GeocoderLocationType
                GeocoderRequest
                GeocoderResponse
                GeocoderResult
                GeocoderStatus
                GroundOverlay
                GroundOverlayOptions
                Icon
                IconSequence
                InfoWindow
                InfoWindowOpenOptions
                InfoWindowOptions
                KmlAuthor
                KmlFeatureData
                KmlLayer
                KmlLayerMetadata
                KmlLayerOptions
                KmlLayerStatus
                KmlMouseEvent
                Location
                LocationElevationRequest
                LocationElevationResponse
                MapCanvasProjection
                MapPanes
                Marker
                MarkerLabel
                MarkerOptions
                MarkerShape
                MaxZoomResult
                MaxZoomService
                MaxZoomStatus
                OverlayView
                PathElevationRequest
                PathElevationResponse
                PinElement
                PinElementOptions
                Place
                Polygon
                PolygonOptions
                PolyMouseEvent
                Polyline
                PolylineOptions
                Rectangle
                RectangleOptions
                StrokePosition
                Symbol
                SymbolPath
                Time
                TrafficLayer
                TrafficLayerOptions
                TrafficModel
                TransitAgency
                TransitDetails
                TransitFare
                TransitLayer
                TransitLine
                TransitMode
                TransitOptions
                TransitRoutePreference
                TransitStop
                TransitVehicle
                TravelMode
                UnitSystem
                VehicleType
            ]
            Namespace "WebSharper.Google.Maps.JournalSharing" [
                JourneySharing.AuthToken
                JourneySharing.AuthTokenContext
                JourneySharing.AuthTokenFetcherOptions
                JourneySharing.AutomaticViewportMode
                JourneySharing.DefaultMarkerSetupOptions
                JourneySharing.DefaultPolylineSetupOptions
                JourneySharing.DeliveryVehicle
                JourneySharing.DeliveryVehicleMarkerCustomizationFunctionParams
                JourneySharing.DeliveryVehiclePolylineCustomizationFunctionParams
                JourneySharing.DeliveryVehicleStop
                JourneySharing.DeliveryVehicleStopState
                JourneySharing.FleetEngineDeliveryFleetLocationProvider
                JourneySharing.FleetEngineDeliveryFleetLocationProviderOptions
                JourneySharing.FleetEngineDeliveryFleetLocationProviderUpdateEvent
                JourneySharing.FleetEngineDeliveryVehicleLocationProvider
                JourneySharing.FleetEngineDeliveryVehicleLocationProviderOptions
                JourneySharing.FleetEngineDeliveryVehicleLocationProviderUpdateEvent
                JourneySharing.FleetEngineFleetLocationProvider
                JourneySharing.FleetEngineFleetLocationProviderOptions
                JourneySharing.FleetEngineFleetLocationProviderUpdateEvent
                JourneySharing.FleetEngineServiceType
                JourneySharing.FleetEngineShipmentLocationProvider
                JourneySharing.FleetEngineShipmentLocationProviderOptions
                JourneySharing.FleetEngineShipmentLocationProviderUpdateEvent
                JourneySharing.FleetEngineTaskFilterOptions
                JourneySharing.FleetEngineTripLocationProvider
                JourneySharing.FleetEngineTripLocationProviderOptions
                JourneySharing.FleetEngineTripLocationProviderUpdateEvent
                JourneySharing.FleetEngineVehicleLocationProvider
                JourneySharing.FleetEngineVehicleLocationProviderOptions
                JourneySharing.FleetEngineVehicleLocationProviderUpdateEvent
                JourneySharing.JourneySharingMapView
                JourneySharing.JourneySharingMapViewOptions
                JourneySharing.LocationProvider
                JourneySharing.MarkerCustomizationFunctionParams
                JourneySharing.MarkerSetupOptions
                JourneySharing.PlannedStopMarkerCustomizationFunctionParams
                JourneySharing.PollingLocationProvider
                JourneySharing.PollingLocationProviderIsPollingChangeEvent
                JourneySharing.PolylineCustomizationFunctionParams
                JourneySharing.PolylineSetupOptions
                JourneySharing.ShipmentMarkerCustomizationFunctionParams
                JourneySharing.ShipmentPolylineCustomizationFunctionParams
                JourneySharing.Speed
                JourneySharing.SpeedReadingInterval
                JourneySharing.Task
                JourneySharing.TaskInfo
                JourneySharing.TaskMarkerCustomizationFunctionParams
                JourneySharing.TaskTrackingInfo
                JourneySharing.TimeWindow
                JourneySharing.Trip
                JourneySharing.TripMarkerCustomizationFunctionParams
                JourneySharing.TripPolylineCustomizationFunctionParams
                JourneySharing.TripType
                JourneySharing.TripWaypoint
                JourneySharing.TripWaypointMarkerCustomizationFunctionParams
                JourneySharing.Vehicle
                JourneySharing.VehicleJourneySegment
                JourneySharing.VehicleLocationUpdate
                JourneySharing.VehicleMarkerCustomizationFunctionParams
                JourneySharing.VehicleNavigationStatus
                JourneySharing.VehiclePolylineCustomizationFunctionParams
                JourneySharing.VehicleState
                JourneySharing.VehicleType
                JourneySharing.VehicleWaypoint
                JourneySharing.VehicleWaypointMarkerCustomizationFunctionParams
                JourneySharing.WaypointType

            ]
            Namespace "WebSharper.Google.Maps.Places" [
                Places.AccessibilityOptions
                Places.AddressComponent
                Places.Attribution
                Places.AuthorAttribution
                Places.Autocomplete
                Places.AutocompleteOptions
                Places.AutocompletePrediction
                Places.AutocompleteResponse
                Places.AutocompleteService
                Places.AutocompleteSessionToken
                Places.AutocompletionRequest
                Places.BusinessStatus
                Places.ComponentRestrictions
                Places.FetchFieldsRequest
                Places.FindPlaceFromPhoneNumberRequest
                Places.FindPlaceFromQueryRequest
                Places.OpeningHours
                Places.OpeningHoursPeriod
                Places.OpeningHoursPoint
                Places.ParkingOptions
                Places.PaymentOptions
                Places.Photo
                Places.PhotoOptions
                Places.Place
                Places.PlaceAspectRating
                Places.PlaceAutocompleteElement
                Places.PlaceAutocompleteElementOptions
                Places.PlaceAutocompletePlaceSelectEvent
                Places.PlaceAutocompleteRequestErrorEvent
                Places.PlaceDetailsRequest
                Places.PlaceGeometry
                Places.PlaceOpeningHours
                Places.PlaceOpeningHoursPeriod
                Places.PlaceOpeningHoursTime
                Places.PlaceOptions
                Places.PlacePhoto
                Places.PlacePlusCode
                Places.PlaceResult
                Places.PlaceReview
                Places.PlaceSearchPagination
                Places.PlaceSearchRequest
                Places.PlacesService
                Places.PlacesServiceStatus
                Places.PlusCode
                Places.PredictionSubstring
                Places.PredictionTerm
                Places.PriceLevel
                Places.QueryAutocompletePrediction
                Places.QueryAutocompletionRequest
                Places.RankBy
                Places.Review
                Places.SearchBox
                Places.SearchBoxOptions
                Places.SearchByTextRankPreference
                Places.SearchByTextRequest
                Places.StructuredFormatting
                Places.TextSearchRequest
            ]
            Namespace "WebSharper.Google.Maps.Data" [
                Data.AddFeatureEvent
                Data.Data
                Data.DataOptions
                Data.Feature
                Data.FeatureOptions
                Data.Geometry
                Data.GeometryCollection
                Data.LineString
                Data.LinearRing
                Data.MultiLineString
                Data.MultiPoint
                Data.MultiPolygon
                Data.Point
                Data.MouseEvent
                Data.Polygon
                Data.RemoveFeatureEvent
                Data.RemovePropertyEvent
                Data.SetGeometryEvent
                Data.SetPropertyEvent
                Data.StyleOptions
                Data.GeoJsonOptions
            ]
        ]
        |> Requires [Res.Js]

    [<Sealed>]
    type Extension() =
        interface IExtension with
            member this.Assembly = Assembly

    [<assembly:Extension(typeof<Extension>)>]
    do ()
