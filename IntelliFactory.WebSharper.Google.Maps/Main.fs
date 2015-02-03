module IntelliFactory.WebSharper.Google.Maps.Main

open IntelliFactory.WebSharper.InterfaceGenerator
open IntelliFactory.WebSharper.Google.Maps.Specification

module Res =
    let Js =
        Resource "Js" "https://maps.googleapis.com/maps/api/js?key=AIzaSyBclwK3qO1qtBU_OgZzlZ_oVv1bkyUXaF0&sensor=true"

let Assembly =
    Assembly [
        Namespace "IntelliFactory.WebSharper.Google.Maps.Resources" [
            Res.Js
        ]
        Namespace "IntelliFactory.WebSharper.Google.Maps" [

            Events.MapsEventListener

            MVC.MVCObject
            MVC.MVCArray

            Base.LatLng
            Base.LatLngBounds
            Base.Point
            Base.Size

            Controls.ControlPosition
            Controls.MapTypeControlOptions
            Controls.MapTypeControlStyle
            Controls.OverviewMapControlOptions
            Controls.PanControlOptions
            Controls.RotateControlOptions
            Controls.ScaleControlOptions
            Controls.ScaleControlStyle
            Controls.StreetViewControlOptions
            Controls.ZoomControlOptions
            Controls.ZoomControlStyle

            Drawing.OverlayType
            Drawing.DrawingControlOptions
            Drawing.DrawingManager
            Drawing.DrawingManagerOptions

            StreetView.StreetViewAddressControlOptions
            StreetView.StreetViewCoverageLayer
            StreetView.StreetViewLink
            StreetView.StreetViewLocation
            StreetView.StreetViewPanorama
            StreetView.StreetViewPanoramaData
            StreetView.StreetViewPanoramaOptions
            StreetView.StreetViewPov
            StreetView.StreetViewService
            StreetView.StreetViewStatus
            StreetView.StreetViewTileData

            MapTypes.ImageMapType
            MapTypes.ImageMapTypeOptions
            MapTypes.MapType
            MapTypes.MapTypeRegistry
            MapTypes.MapTypeStyle
            MapTypes.MapTypeStyleElementType
            MapTypes.MapTypeStyleFeatureType
            MapTypes.MapTypeStyler
            MapTypes.Projection
            MapTypes.Visibility

            Map.Map
            Map.MapOptions
            Map.MapTypeId

            Places.Autocomplete
            Places.AutocompleteOptions
            Places.AutocompletePrediction
            Places.AutocompleteService
            Places.AutocompletionRequest
            Places.ComponentRestrictions
            Places.PhotoOptions
            Places.PlaceAspectRating
            Places.PlaceDetailsRequest
            Places.PlaceGeometry
            Places.PlaceResult
            Places.PlaceReview
            Places.PlaceSearchPagination
            Places.PlaceSearchRequest
            Places.PlacesService
            Places.PlacesServiceStatus
            Places.PredictionSubstring
            Places.PredictionTerm
            Places.QueryAutocompletePrediction
            Places.QueryAutocompletionRequest
            Places.RadarSearchRequest
            Places.RankBy
            Places.SearchBox
            Places.SearchBoxOptions
            Places.TextSearchRequest

            Visualization.DemographicsLayer
            Visualization.DemographicsLayerOptions
            Visualization.DemographicsPolygonOptions
            Visualization.DemographicsPropertyStyle
            Visualization.DemographicsQuery
            Visualization.DemographicsStyle
            Visualization.DynamicMapsEngineLayer
            Visualization.FeatureStyle
            Visualization.HeatmapLayer
            Visualization.HeatmapLayerOptions
            Visualization.MapsEngineLayer
            Visualization.MapsEngineLayerOptions
            Visualization.MapsEngineLayerProperties
            Visualization.MapsEngineStatus
            Visualization.WeightedLocation

            Weather.CloudLayer
            Weather.LabelColor
            Weather.TemperatureUnit
            Weather.WeatherConditions
            Weather.WeatherForecast
            Weather.WeatherFeature
            Weather.WeatherLayerOptions
            Weather.WeatherLayer
            Weather.WindSpeedUnit

            Animation
            BicyclingLayer
            Circle
            CircleOptions
            DirectionsLeg
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
            Duration
            ElevationResult
            ElevationService
            ElevationStatus
            Event
            FusionTablesCell
            FusionTablesHeatmap
            FusionTablesLayer
            FusionTablesLayerOptions
            FusionTablesMarkerOptions
            FusionTablesMouseEvent
            FusionTablesPolygonOptions
            FusionTablesPolylineOptions
            FusionTablesStyle
            Geocoder
            GeocoderAddressComponent
            GeocoderComponentRestrictions
            GeocoderGeometry
            GeocoderLocationType
            GeocoderRequest
            GeocoderResult
            GeocoderStatus
            GroundOverlay
            GroundOverlayOptions
            Icon
            IconSequence
            InfoWindow
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
            MapCanvasProjection
            MapPanes
            Marker
            MarkerImage
            MarkerOptions
            MarkerShape
            MarkerShapeType
            MaxZoomResult
            MaxZoomService
            MaxZoomStatus
            NavigationControlOptions
            NavigationControlStyle
            OverlayView
            PathElevationRequest
            Polygon
            PolygonOptions
            Polyline
            PolylineOptions
            Rectangle
            RectangleOptions
            StrokePosition
            // StyledMapType: FIXME after bugs 103, 108
            StyledMapTypeOptions
            Symbol
            SymbolPath
            Time
            TrafficLayer
            TransitAgency
            TransitDetails
            TransitLayer
            TransitLine
            TransitOptions
            TransitStop
            TransitVehicle
            TravelMode
            UnitSystem
            VehicleType
        ]
    ]
    |> Requires [Res.Js]

[<Sealed>]
type Extension() =
    interface IExtension with
        member this.Assembly = Assembly

[<assembly:Extension(typeof<Extension>)>]
do ()
