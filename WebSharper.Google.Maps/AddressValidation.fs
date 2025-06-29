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
namespace WebSharper.Google.Maps.Definition

module AddressValidation = 

    open WebSharper.InterfaceGenerator
    open Notation
    open Base
    open Places

    let ConfirmationLevel =
        Pattern.EnumStrings "google.maps.addressValidation.ConfirmationLevel" [
            "CONFIRMED"
            "UNCONFIRMED_AND_SUSPICIOUS"
            "UNCONFIRMED_BUT_PLAUSIBLE"
        ]

    let AddressComponent =
        Class "google.maps.addressValidation.AddressComponent"
        |+> Instance [
            "componentName" =@ T<string>
            |> WithComment "The component name text."

            "componentNameLanguageCode" =@ T<string>
            |> WithComment "The BCP-47 language code."

            "componentType" =@ T<string>
            |> WithComment "The type of the address component."

            "confirmationLevel" =@ ConfirmationLevel
            |> WithComment "Indicates the level of certainty that the component is correct."

            "inferred" =@ T<bool>
            |> WithComment "If true, this component was not part of the input, but was inferred for the address location."

            "replaced" =@ T<bool>
            |> WithComment "Indicates the name of the component was replaced with a completely different one."

            "spellCorrected" =@ T<bool>
            |> WithComment "Indicates a correction to a misspelling in the component name."

            "unexpected" =@ T<bool>
            |> WithComment "If true, this component is not expected to be present in a postal address for the given region."
        ]

    let Address =
        Class "google.maps.addressValidation.Address"
        |+> Instance [
            "components" =@ Type.ArrayOf AddressComponent
            |> WithComment "The individual address components of the formatted and corrected address, along with validation information."

            "formattedAddress" =@ T<string>
            |> WithComment "The post-processed address, formatted as a single-line address following the address-formatting rules of the region where the address is located."

            "missingComponentTypes" =@ Type.ArrayOf T<string>
            |> WithComment "The types of components that were expected to be present in a correctly formatted mailing address but were not found in the input AND could not be inferred."

            "postalAddress" =@ PostalAddress
            |> WithComment "The post-processed address represented as a postal address."

            "unconfirmedComponentTypes" =@ Type.ArrayOf T<string>
            |> WithComment "The types of the components that are present in the address_components but could not be confirmed to be correct."

            "unresolvedTokens" =@ Type.ArrayOf T<string>
            |> WithComment "Any tokens in the input that could not be resolved."
        ]    

    let AddressMetadata =
        Class "google.maps.addressValidation.AddressMetadata"
        |+> Instance [
            "business" =@ T<bool>
            "poBox" =@ T<bool>
            "residential" =@ T<bool>
        ]

    let Geocode =
        Class "google.maps.addressValidation.Geocode"
        |+> Instance [
            "bounds" =@ LatLngBounds
            |> WithComment "The bounds of the geocoded place."

            "featureSizeMeters" =@ T<float>
            |> WithComment "The size of the geocoded place, in meters."

            "location" =@ LatLngAltitude
            |> WithComment "The geocoded location of the input."

            "placeId" =@ T<string>
            |> WithComment "The Place ID of the geocoded place."

            "placeTypes" =@ !| T<string>
            |> WithComment "The type(s) of place that the input geocoded to."

            "plusCode" =@ PlusCode
            |> WithComment "The plus code corresponding to the location."

            "fetchPlace" => T<unit> ^-> T<unit>
            |> WithComment "Returns a Place representation of this Geocode."
        ]

    let Granularity =
        Pattern.EnumStrings "google.maps.addressValidation.Granularity" [
            "BLOCK"
            "OTHER"
            "PREMISE"
            "PREMISE_PROXIMITY"
            "ROUTE"
            "SUB_PREMISE"
        ]

    let USPSAddress =
        Class "google.maps.addressValidation.USPSAddress"
        |+> Instance [
            "city" =@ T<string>
            "cityStateZipAddressLine" =@ T<string>
            "firm" =@ T<string>
            "firstAddressLine" =@ T<string>
            "secondAddressLine" =@ T<string>
            "state" =@ T<string>
            "urbanization" =@ T<string>
            "zipCode" =@ T<string>
            "zipCodeExtension" =@ T<string>
        ]

    let USPSData =
        Class "google.maps.addressValidation.USPSData"
        |+> Instance [
            "abbreviatedCity" =@ T<string>
            "addressRecordType" =@ T<string>
            "carrierRoute" =@ T<string>
            "carrierRouteIndicator" =@ T<string>
            "cassProcessed" =@ T<bool>
            "county" =@ T<string>
            "deliveryPointCheckDigit" =@ T<string>
            "deliveryPointCode" =@ T<string>
            "dpvCMRA" =@ T<string>
            "dpvConfirmation" =@ T<string>
            "dpvDoorNotAccessible" =@ T<string>
            "dpvDrop" =@ T<string>
            "dpvEnhancedDeliveryCode" =@ T<string>
            "dpvFootnote" =@ T<string>
            "dpvNonDeliveryDays" =@ T<string>
            "dpvNonDeliveryDaysValues" =@ T<float>
            "dpvNoSecureLocation" =@ T<string>
            "dpvNoStat" =@ T<string>
            "dpvNoStatReasonCode" =@ T<float>
            "dpvPBSA" =@ T<string>
            "dpvThrowback" =@ T<string>
            "dpvVacant" =@ T<string>
            "elotFlag" =@ T<string>
            "elotNumber" =@ T<string>
            "errorMessage" =@ T<string>
            "fipsCountyCode" =@ T<string>
            "hasDefaultAddress" =@ T<bool>
            "hasNoEWSMatch" =@ T<bool>
            "lacsLinkIndicator" =@ T<string>
            "lacsLinkReturnCode" =@ T<string>
            "pmbDesignator" =@ T<string>
            "pmbNumber" =@ T<string>
            "poBoxOnlyPostalCode" =@ T<bool>
            "postOfficeCity" =@ T<string>
            "postOfficeState" =@ T<string>
            "standardizedAddress" =@ USPSAddress
            "suiteLinkFootnote" =@ T<string>
        ]

    let Verdict =
        Class "google.maps.addressValidation.Verdict"
        |+> Instance [
            "addressComplete" =@ T<bool>
            |> WithComment "The address is considered complete if there are no unresolved tokens, no unexpected or missing address components."

            "geocodeGranularity" =@ Granularity
            |> WithComment "Information about the granularity of the Geocode."

            "hasInferredComponents" =@ T<bool>
            |> WithComment "At least one address component was inferred."

            "hasReplacedComponents" =@ T<bool>
            |> WithComment "At least one address component was replaced."

            "hasUnconfirmedComponents" =@ T<bool>
            |> WithComment "At least one address component cannot be categorized or validated."

            "inputGranularity" =@ Granularity
            |> WithComment "The granularity of the input address."

            "validationGranularity" =@ Granularity
            |> WithComment "The granularity level that the API can fully validate the address to."
        ]

    let AddressValidation =
        Forward.AddressValidation
            |+> Static [
                "fetchAddressValidation" => AddressValidationRequest ^-> Promise[TSelf]
                |> WithComment "Validates an address using the Address Validation API."
            ]
            |+> Instance [
                "address" =@ Address
                |> WithComment "Information about the address itself as opposed to the geocode."

                "geocode" =@ Geocode
                |> WithComment "Information about the location and place that the address geocoded to."

                "metadata" =@ AddressMetadata
                |> WithComment "Other information relevant to deliverability."

                "responseId" =@ T<string>
                |> WithComment "The UUID that identifies this response."

                "uspsData" =@ USPSData
                |> WithComment "Extra deliverability flags provided by USPS. Only provided in region US and PR."

                "verdict" =@ Verdict
                |> WithComment "Overall verdict flags."

                "toJSON" => T<unit> ^-> T<obj>
                |> WithComment "Converts the AddressValidation class to a JSON object with the same properties."
            ]