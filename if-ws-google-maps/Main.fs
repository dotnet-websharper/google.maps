module IntelliFactory.WebSharper.Google.Maps.Main

open IntelliFactory.WebSharper.InterfaceGenerator
open IntelliFactory.WebSharper.Google.Maps.Specification

[<EntryPoint>]
let EntryPoint args =
    let Assembly =
        Assembly [
            Namespace "IntelliFactory.WebSharper.Google.Maps" [

                Events.MapsEventListener

                MVC.MVCObject
                Generic - MVC.MVCArray

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

                Visualization.HeatmapLayer
                Visualization.HeatmapLayerOptions
                Visualization.WeightedLocation

                BicyclingLayer
                Circle
                CircleOptions
                DirectionsDistance
                DirectionsDuration
                DirectionsLeg
                DirectionsRenderer
                DirectionsRendererOptions
                DirectionsRequest
                DirectionsResult
                DirectionsRoute
                DirectionsService
                DirectionsStatus
                DirectionsStep
                DirectionsTravelMode
                DirectionsUnitSystem
                DirectionsWaypoint
                ElevationResult
                ElevationService
                ElevationStatus
                Event
                FusionTablesCell
                FusionTablesLayer
                FusionTablesLayerOptions
                FusionTablesMouseEvent
                Geocoder
                GeocoderAddressComponent
                GeocoderGeometry
                GeocoderLocationType
                GeocoderRequest
                GeocoderResult
                GeocoderStatus
                GroundOverlay
                GroundOverlayOptions
                InfoWindow
                InfoWindowOptions
                KmlAuthor
                KmlFeatureData
                KmlLayer
                KmlLayerMetadata
                KmlLayerOptions
                KmlMouseEvent
                LocationElevationRequest
                MapCanvasProjection
                MapPanes
                Marker
                MarkerImage
                MarkerOptions
                MarkerShape
                MarkerShapeType
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
                // StyledMapType: FIXME after bugs 103, 108
                StyledMapTypeOptions
                TrafficLayer
            ]
        ]
    Compiler.Compile stdout Assembly
    0
