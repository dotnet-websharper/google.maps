
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
// Definitions for the Places part of the API. See:
// http://developers.google.com/maps/documentation/javascript/reference
namespace WebSharper.Google.Maps.Geometry

open WebSharper.Google.Maps.Definition.Base
open WebSharper.Google.Maps.Definition.MVC
open WebSharper.Google.Maps.Definition.Specification

module Encoding =
    open WebSharper.InterfaceGenerator

    // Decodes an encoded path string into a sequence of LatLngs.
    let decodePath = T<string> ^-> T<string[]>

    // Encodes a sequence of LatLngs into an encoded path string.
    let encodePath = Type.ArrayOf (LatLng + LatLngLiteral) + MVCArray.[LatLng + LatLngLiteral] ^-> T<string>

module Spherical =
    open WebSharper.InterfaceGenerator

    // Returns the unsigned area of a closed path, in the range [0, 2×pi×radius²]. The computed area uses the same units as the radius. The radiusOfSphere defaults to the Earth's radius in meters, in which case the area is in square meters. Passing a Circle requires the radius to be set to a non-negative value. Additionally, the Circle must not cover more than 100% of the sphere. And when passing a LatLngBounds, the southern LatLng cannot be more north than the northern LatLng.
    let computeArea =
        ((Type.ArrayOf (LatLng + LatLngLiteral)) + MVCArray.[LatLng + LatLngLiteral] + Circle + CircleLiteral + LatLngBounds + LatLngBoundsLiteral) * !? T<float> ^-> T<float>

    // Returns the distance, in meters, between two LatLngs. You can optionally specify a custom radius. The radius defaults to the radius of the Earth.
    let computeDistanceBetween =
        (LatLng + LatLngLiteral) * (LatLng + LatLngLiteral) * !? T<float> ^-> T<float>

    // Returns the heading from one LatLng to another LatLng. Headings are expressed in degrees clockwise from North within the range [-180,180).
    let computeHeading =
        (LatLng + LatLngLiteral) * (LatLng + LatLngLiteral) ^-> T<float>

    // Returns the length of the given path.
    let computeLength =
        (Type.ArrayOf (LatLng + LatLngLiteral) + MVCArray.[LatLng + LatLngLiteral]) * !? T<float> ^-> T<float>

    // Returns the LatLng resulting from moving a distance from an origin in the specified heading (expressed in degrees clockwise from north).
    let computeOffset =
        (LatLng + LatLngLiteral) * T<float> * T<float> * !? T<float> ^-> LatLng

    // Returns the location of origin when provided with a LatLng destination, meters travelled and original heading. Headings are expressed in degrees clockwise from North. This function returns null when no solution is available.
    let computeOffsetOrigin =
        (LatLng + LatLngLiteral) * T<float> * T<float> * !? T<float> ^-> LatLng

    // Returns the signed area of a closed path, where counterclockwise is positive, in the range [-2×pi×radius², 2×pi×radius²]. The computed area uses the same units as the radius. The radius defaults to the Earth's radius in meters, in which case the area is in square meters. 
    // The area is computed using the parallel transport method; the parallel transport around a closed path on the unit sphere twists by an angle that is equal to the area enclosed by the path. This is simpler and more accurate and robust than triangulation using Girard, l'Huilier, or Eriksson on each triangle. In particular, since it doesn't triangulate, it suffers no instability except in the unavoidable case when an edge (not a diagonal) of the polygon spans 180 degrees.
    let computeSignedArea =
        (Type.ArrayOf(LatLng + LatLngLiteral) + MVCArray.[LatLng + LatLngLiteral]) * !? T<float> ^-> T<float>

    // Returns the LatLng which lies the given fraction of the way between the origin LatLng and the destination LatLng.
    let interpolate =
        (LatLng + LatLngLiteral) * (LatLng + LatLngLiteral) * !? T<float> ^-> T<string>


module Poly =
    open WebSharper.InterfaceGenerator

    // Computes whether the given point lies inside the specified polygon.
    let containsLocation =
        (LatLng  + LatLngLiteral) * Polygon ^-> T<bool>

    // Computes whether the given point lies on or near to a polyline, or the edge of a polygon, within a specified tolerance. Returns true when the difference between the latitude and longitude of the supplied point, and the closest point on the edge, is less than the tolerance. The tolerance defaults to 10-9 degrees.
    let isLocationOnEdge =
        (LatLng + LatLngLiteral) * (Polygon + Polyline) * !?T<float> ^-> T<bool>
