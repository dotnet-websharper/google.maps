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

module Places =

    open WebSharper.InterfaceGenerator
    open Notation
    open Base
    open Specification

    (** Places Autocomplete Services *)
    let ComponentRestrictions =
        Config "google.maps.places.ComponentRestrictions"
            []
            [
                "country", (T<string> + Type.ArrayOf T<string>)
            ]

    let BusinessStatus =
        Class "google.maps.places.BusinessStatus"
        |+> Static [
            "CLOSED_PERMANENTLY" =? TSelf
            |> WithComment "The business is closed permanently."

            "CLOSED_TEMPORARILY" =? TSelf
            |> WithComment "The business is closed temporarily."

            "OPERATIONAL"=? TSelf
            |> WithComment "The business is operating normally."
        ]

    let PlaceAspectRating =
        Class "google.maps.places.PlaceAspectRating"
        |+> Instance [
            "rating" =? T<int>
            |> WithComment "The rating of this aspect. For individual reviews this is an integer from 0 to 3. For aggregated ratings of a place this is an integer from 0 to 30."

            "type" =? T<string>
            |> WithComment "The aspect type, e.g. \"food\", \"decor\", \"service\", \"overall\"."
        ]
        |> ObsoleteWithMessage "Deprecated: This interface is no longer used."

    let AutocompleteSessionToken =
        Class "google.maps.places.AutocompleteSessionToken "
        |+> Static [Constructor T<unit>]

    let PlaceDetailsRequest =
        Config "google.maps.places.PlaceDetailsRequest"
            []
            [
                // The Place ID of the Place for which details are being requested.
                "placeId", T<string>

                // Fields to be included in the details response, which will be billed for. 
                // If no fields are specified or ['ALL'] is passed in, all available fields will be returned and billed for (not recommended for production). 
                // For a list of fields see PlaceResult. Nested fields can be specified with dot-paths (e.g., "geometry.location").
                "fields", T<string[]> 

                // A language identifier for the language in which details should be returned.
                "language", T<string>

                // A region code of the user's region. This can affect which photos may be returned, and possibly other things.
                // Accepts a ccTLD ("top-level domain") two-character value. 
                // For example, UK: "uk" (.co.uk) vs ISO 3166-1 code: "gb"
                "region", T<string>

                // Unique reference used to bundle the details request with an autocomplete session.
                "sessionToken", AutocompleteSessionToken.Type
            ]

    let PlaceGeometry =
        Class "google.maps.places.PlaceGeometry"
        |+> Instance [
            "location" =? LatLng
            |> WithComment "The Place's position."

            "viewport" =? LatLngBounds
            |> WithComment "The preferred viewport when displaying this Place on a map. This property will be null if the preferred viewport for the Place is not known.Only available with PlacesService.getDetails."
        ]

    let PlaceOpeningHoursTime =
        Config "google.maps.places.PlaceOpeningHoursTime"
            []
            [
                // The days of the week, as a number in the range [0, 6], starting on Sunday. For example, 2 means Tuesday.
                "day", T<int>

                // The hours of the PlaceOpeningHoursTime.time as a number, in the range [0, 23]. This will be reported in the Place’s time zone.
                "hours", T<int>

                // The minutes of the PlaceOpeningHoursTime.time as a number, in the range [0, 59]. This will be reported in the Place’s time zone.
                "minutes", T<int>

                // The time of day in 24-hour "hhmm" format. Values are in the range ["0000", "2359"]. The time will be reported in the Place’s time zone.
                "time", T<string>

                // The timestamp (as milliseconds since the epoch, suitable for use with new Date()) representing the next occurrence of this PlaceOpeningHoursTime.
                // It is calculated from the PlaceOpeningHoursTime.day of week, the PlaceOpeningHoursTime.time, and the PlaceResult.utc_offset_minutes.
                // If the PlaceResult.utc_offset_minutes is undefined, then nextDate will be undefined.
                "nextDate", T<int64>
            ]

    let PlaceOpeningHoursPeriod =
        Config "google.maps.places.PlaceOpeningHoursPeriod"
            []
            [
                // The opening time for the Place.
                "open", PlaceOpeningHoursTime.Type

                // The closing time for the Place.
                "close", PlaceOpeningHoursTime.Type
            ]


    let PlaceOpeningHours =
        Class "google.maps.places.PlaceOpeningHours"
        |+> Instance [
            "open_now" =? T<bool>
            |> WithComment "Whether the Place is open at the current time."
            |> ObsoleteWithMessage "Deprecated: open_now is deprecated as of November 2019. Use the PlaceOpeningHours.isOpen method from a PlacesService.getDetails result instead. See https://goo.gle/js-open-now"

            "periods" =@ Type.ArrayOf PlaceOpeningHoursPeriod
            |> WithComment "Opening periods covering for each day of the week, starting from Sunday, in chronological order. Days in which the Place is not open are not included. Only available with PlacesService.getDetails."

            "weekday_text" =@ T<string[]>
            |> WithComment "An array of seven strings representing the formatted opening hours for each day of the week. The Places Service will format and localize the opening hours appropriately for the current language. The ordering of the elements in this array depends on the language. Some languages start the week on Monday while others start on Sunday. Only available with PlacesService.getDetails. Other calls may return an empty array."

            "isOpen" => !?Date ^-> T<bool>
            |> WithComment "Check whether the place is open now (when no date is passed), or at the given date. If this place does not have PlaceResult.utc_offset_minutes or PlaceOpeningHours.periods then undefined is returned (PlaceOpeningHours.periods is only available via PlacesService.getDetails). This method does not take exceptional hours, such as holiday hours, into consideration."
        ]

    let PhotoOptions =
        Config "google.maps.places.PhotoOptions"
            []
            [
                // The maximum height in pixels of the returned image.
                "maxHeight", T<int>

                // The maximum width in pixels of the returned image.
                "maxWidth", T<int>
            ]

    let PlacePhoto =
        Class "google.maps.places.PlacePhoto"
        |+> Instance [
            "getUrl" => !?PhotoOptions ^-> T<string>
            |> WithComment "Returns the image URL corresponding to the specified options."

            "height" =? T<int>
            |> WithComment "The height of the photo in pixels."

            "html_attributions" =? T<string[]>
            |> WithComment "Attribution text to be displayed for this photo."

            "width" =? T<int>
            |> WithComment "The width of the photo in pixels."
        ]

    let PlaceReview =
        Class "google.maps.places.PlaceReview"
        |+> Instance [
            "aspects" =? Type.ArrayOf PlaceAspectRating
            |> WithComment "The aspects rated by the review. The ratings on a scale of 0 to 3."
            |> ObsoleteWithMessage "Deprecated: This field is no longer available."

            "author_name" =? T<string>
            |> WithComment "The name of the reviewer."

            "language" =@ T<string>
            |> WithComment "An IETF language code indicating the language in which this review is written. Note that this code includes only the main language tag without any secondary tag indicating country or region. For example, all the English reviews are tagged as 'en' rather than 'en-AU' or 'en-UK'."

            "profile_photo_url" =@ T<string>
            |> WithComment "A URL to the reviwer's profile image."

            "relative_time_description" =@ T<string>
            |> WithComment "A string of formatted recent time, expressing the review time relative to the current time in a form appropriate for the language and country. For example \"a month ago\"."

            "text" =? T<string>
            |> WithComment "The text of a review."

            "time" =@ T<int64>
            |> WithComment "Timestamp for the review, expressed in seconds since epoch."

            "author_url" =? T<string>
            |> WithComment "A URL to the reviewer's profile. This will be undefined when the reviewer's profile is unavailable."

            "rating" =@ T<int>
            |> WithComment "The rating of this review, a number between 1.0 and 5.0 (inclusive)."
        ]

    let PlaceSearchPagination =
        Class "google.maps.places.PlaceSearchPagination"
        |+> Instance [
            "nextPage" => T<unit -> unit>
            |> WithComment "Fetches the next page of results. Uses the same callback function that was provided to the first search request."

            "hasNextPage" =? T<bool>
            |> WithComment "Indicates if further results are available. true when there is an additional results page."
        ]

    let RankBy =
        Class "google.maps.places.RankBy"
        |+> Static [
            "DISTANCE" =? TSelf
            |> WithComment "Ranks place results by distance from the location."

            "PROMINENCE" =? TSelf
            |> WithComment "Ranks place results by their prominence."
        ]

    let LocationBias =
        LatLng + LatLngLiteral + LatLngBounds + LatLngBoundsLiteral + Circle + CircleLiteral + T<string>

    let LocationRestriction =
        LatLngBounds + LatLngBoundsLiteral

    let FindPlaceFromPhoneNumberRequest =
        Config "google.maps.places.FindPlaceFromPhoneNumberRequest"
            []
            [
                // Fields to be included in the response, which will be billed for.
                // If ['ALL'] is passed in, all available fields will be returned and billed for
                // (this is not recommended for production deployments).
                // For a list of fields see PlaceResult. Nested fields can be specified with dot-paths (e.g., "geometry.location").
                "fields", T<string[]>

                // The phone number of the place to look up. Format must be E.164.
                "phoneNumber", T<string>

                // A language identifier for the language in which names and addresses should be returned, when possible.
                // See the list of supported languages.
                "language", T<string>

                // The bias used when searching for Place. The result will be biased towards,
                // but not restricted to, the given LocationBias.
                "locationBias", LocationBias
            ]

    let FindPlaceFromQueryRequest =
        Config "google.maps.places.FindPlaceFromQueryRequest"
            []
            [
                // Fields to be included in the response, which will be billed for.
                // If ['ALL'] is passed in, all available fields will be returned and billed for
                // (this is not recommended for production deployments).
                // For a list of fields see PlaceResult. Nested fields can be specified with dot-paths (e.g., "geometry.location").
                "fields", T<string[]>

                // The request's query. For example, the name or address of a place.
                "phoneNumber", T<string>

                // A language identifier for the language in which names and addresses should be returned, when possible.
                // See the list of supported languages.
                "language", T<string>

                // The bias used when searching for Place. The result will be biased towards,
                // but not restricted to, the given LocationBias.
                "locationBias", LocationBias
            ]

    let PlaceSearchRequest =
        Config "google.maps.places.PlaceSearchRequest"
            []
            [
                // The bounds within which to search for Places.
                // Both location and radius will be ignored if bounds is set.
                "bounds", LatLngBounds + LatLngBoundsLiteral

                // A term to be matched against all available fields, including but not limited to name,
                // type, and address, as well as customer reviews and other third-party content.
                "keyword", T<string>

                // A language identifier for the language in which names and addresses should be returned, when possible.
                // See the list of supported languages.
                "language", T<string>

                // The location around which to search for Places.
                "location", LatLng + LatLngLiteral

                // Restricts results to only those places at the specified price level or lower.
                // Valid values: 0 (most affordable) to 4 (most expensive). Must be >= minPrice if specified.
                "maxPriceLevel", T<int>

                // Restricts results to only those places at the specified price level or higher.
                // Valid values: 0 (most affordable) to 4 (most expensive). Must be <= maxPrice if specified.
                "minPriceLevel", T<int>

                // Equivalent to keyword. Values in this field are combined with values in the keyword field
                // and passed as part of the same search string.
                // Deprecated: Use keyword instead.
                "name", T<string>

                // Restricts results to only those places that are open right now.
                "openNow", T<bool>

                // The distance from the given location within which to search for Places, in meters.
                // Maximum allowed value is 50000.
                "radius", T<float>

                // Specifies the ranking method to use when returning results.
                // Note: If rankBy is DISTANCE, you must specify location, and cannot specify radius or bounds.
                // Default: RankBy.PROMINENCE
                "rankBy", RankBy.Type

                // Searches for places of the given type. The type is translated to the local language
                // of the request's target location and used as a query string. Results of a different type are dropped.
                "type", T<string>
            ]

    let PlacePlusCode = Forward.PlacePlusCode

    let PlaceResult =
        Class "google.maps.places.PlaceResult"
        |+> Instance [
            "address_components" =? Type.ArrayOf GeocoderAddressComponent
            |> WithComment "The collection of address components for this Place’s location. Only available with PlacesService.getDetails."

            "adr_address" =@ T<string>
            |> WithComment "The representation of the Place’s address in the adr microformat. Only available with PlacesService.getDetails."

            "aspects" =? Type.ArrayOf PlaceAspectRating
            |> WithComment "The rated aspects of this Place, based on Google and Zagat user reviews. The ratings are on a scale of 0 to 30."

            "business_status" =@ BusinessStatus
            |> WithComment "A flag indicating the operational status of the Place, if it is a business (indicates whether the place is operational, or closed either temporarily or permanently). If no data is available, the flag is not present in search or details responses."

            "formatted_address" =? T<string>
            |> WithComment "The Place's full address."

            "formatted_phone_number" =? T<string>
            |> WithComment "The Place’s phone number, formatted according to the number's regional convention. Only available with PlacesService.getDetails."

            "geometry" =? PlaceGeometry
            |> WithComment "The Place's geometry-related information."

            "html_attributions" =? T<string[]>
            |> WithComment "Attribution text to be displayed for this Place result. Available html_attributions are always returned regardless of what fields have been requested, and must be displayed."

            "icon" =? T<string>
            |> WithComment "URL to an image resource that can be used to represent this Place's category."

            "icon_background_color" =@ T<string>
            |> WithComment "Background color for use with a Place's icon. See also PlaceResult.icon_mask_base_uri."

            "icon_mask_base_uri" =@ T<string>
            |> WithComment "A truncated URL to an icon mask. Access different icon types by appending a file extension to the end (i.e. .svg or .png)."

            "international_phone_number" =? T<string>
            |> WithComment "The Place’s phone number in international format. International format includes the country code, and is prefixed with the plus (+) sign. Only available with PlacesService.getDetails."

            "name" =? T<string>
            |> WithComment "The Place's name. Note: In the case of user entered Places, this is the raw text, as typed by the user. Please exercise caution when using this data, as malicious users may try to use it as a vector for code injection attacks (See http://en.wikipedia.org/wiki/Code_injection)."

            "opening_hours" =@ PlaceOpeningHours
            |> WithComment "Defines when the Place opens or closes."

            "permanently_closed" =? T<bool>
            |> WithComment "A flag indicating whether the Place is permanently closed. If the place is not permanently closed, the flag is not present in search or details responses."
            |> ObsoleteWithMessage "Deprecated: permanently_closed is deprecated as of May 2020 and will be turned off in May 2021. Use PlaceResult.business_status instead as permanently_closed does not distinguish between temporary and permanent closures."

            "photos" =? Type.ArrayOf PlacePhoto
            |> WithComment "Photos of this Place. The collection will contain up to ten PlacePhoto objects."

            "place_id" =@ T<string>
            |> WithComment "A unique identifier for the Place."

            "plus_code" =@ PlacePlusCode
            |> WithComment "Defines Open Location Codes or \"plus codes\" for the Place."

            "price_level" =? T<int>
            |> WithComment "The price level of the Place, on a scale of 0 to 4. Price levels are interpreted as follows:
                0:	Free
                1:	Inexpensive
                2:	Moderate
                3:	Expensive
                4:	Very Expensive"

            "rating" =? T<float>
            |> WithComment "A rating, between 1.0 to 5.0, based on user reviews of this Place."

            "reviews" =? Type.ArrayOf PlaceReview
            |> WithComment "A list of reviews of this Place. Only available with PlacesService.getDetails."

            "types" =? T<string[]>
            |> WithComment "An array of types for this Place (e.g., [\"political\",  \"locality\"] or [\"restaurant\", \"establishment\"])."

            "url" =? T<string>
            |> WithComment "URL of the official Google page for this place. This is the Google-owned page that contains the best available information about the Place. Only available with PlacesService.getDetails."

            "user_ratings_total" =@ T<float>
            |> WithComment "The number of user ratings which contributed to this Place’s PlaceResult.rating."

            "utc_offset" =@ T<string>
            |> WithComment "The offset from UTC of the Place’s current timezone, in minutes. For example, Sydney, Australia in daylight savings is 11 hours ahead of UTC, so the utc_offset will be 660. For timezones behind UTC, the offset is negative. For example, the utc_offset is -60 for Cape Verde. Only available with PlacesService.getDetails."
            |> ObsoleteWithMessage "Deprecated: utc_offset is deprecated as of November 2019. Use PlaceResult.utc_offset_minutes instead. See https://goo.gle/js-open-now"

            "utc_offset_minutes" =@ T<int>
            |> WithComment "The offset from UTC of the Place’s current timezone, in minutes. For example, Sydney, Australia in daylight savings is 11 hours ahead of UTC, so the utc_offset_minutes will be 660. For timezones behind UTC, the offset is negative. For example, the utc_offset_minutes is -60 for Cape Verde. Only available with PlacesService.getDetails."

            "vicinity" =? T<string>
            |> WithComment "A fragment of the Place's address for disambiguation (usually street name and locality)."

            "website" =? T<string>
            |> WithComment "The authoritative website for this Place, such as a business' homepage."
        ]

    let PlacesServiceStatus =
        Class "google.maps.places.PlacesServiceStatus"
        |+> Static [
            "INVALID_REQUEST" =? TSelf
            |> WithComment "This request was invalid."

            "NOT_FOUND" =? TSelf
            |> WithComment "The place referenced was not found."

            "OK" =? TSelf
            |> WithComment "The response contains a valid result."

            "OVER_QUERY_LIMIT" =? TSelf
            |> WithComment "The application has gone over its request quota."

            "REQUEST_DENIED" =? TSelf
            |> WithComment "The application is not allowed to use the PlacesService."

            "UNKNOWN_ERROR" =? TSelf
            |> WithComment "The PlacesService request could not be processed due to a server error. The request may succeed if you try again."

            "ZERO_RESULTS" =? TSelf
            |> WithComment "No result was found for this request."
        ]

    let PredictionSubstring =
        Config "google.maps.places.PredictionSubstring"
            []
            [
                // The length of the substring.
                "length", T<int>

                // The offset to the substring's start within the description string.
                "offset", T<int>
            ]

    let PredictionTerm =
        Config "google.maps.places.PredictionTerm"
            []
            [
                // The offset, in unicode characters, of the start of this term in the description of the place.
                "offset", T<int>

                // The value of this term, e.g. "Taco Bell".
                "value", T<string>
            ]

    let StructuredFormatting =
        Config "google.maps.places.StructuredFormatting"
            []
            [
                // This is the main text part of the unformatted description of the place suggested by the Places service. Usually the name of the place.
                "main_text", T<string>

                // A set of substrings in the main text that match elements in the user's input, suitable for use in highlighting those substrings. Each substring is identified by an offset and a length, expressed in unicode characters.
                "main_text_matched_substrings", Type.ArrayOf PredictionSubstring

                // This is the secondary text part of the unformatted description of the place suggested by the Places service. Usually the location of the place.
                "secondary_text", T<string>
            ]

    let QueryAutocompletePrediction =
        Class "google.maps.places.QueryAutocompletePrediction"
        |+> Instance [
            "description" =? T<string>
            |> WithComment "This is the unformatted version of the query suggested by the Places service."

            "matched_substrings" =? Type.ArrayOf PredictionSubstring
            |> WithComment "A set of substrings in the place's description that match elements in the user's input, suitable for use in highlighting those substrings. Each substring is identified by an offset and a length, expressed in unicode characters."

            "terms" =? Type.ArrayOf PredictionTerm
            |> WithComment "Information about individual terms in the above description. Categorical terms come first (e.g., \"restaurant\"). Address terms appear from most to least specific. For example, \"San Francisco\", and \"CA\"."

            "place_id" =? T<string>
            |> WithComment "Only available if prediction is a place. A place ID that can be used to retrieve details about this place using the place details service (see PlacesService.getDetails)."
        ]

    let QueryAutocompletionRequest =
        Config "google.maps.places.QueryAutocompletionRequest"
            []
            [
                // Bounds for prediction biasing. Predictions will be biased towards, but not restricted to, the given bounds. Both location and radius will be ignored if bounds is set.
                "bounds", LatLngBounds + LatLngBoundsLiteral

                // The user entered input string.
                "input", T<string>

                // Location for prediction biasing. Predictions will be biased towards the given location and radius. Alternatively, bounds can be used.
                "location", LatLng.Type

                // The character position in the input term at which the service uses text for predictions (the position of the cursor in the input field).
                "offset", T<int>

                // The radius of the area used for prediction biasing. The radius is specified in meters, and must always be accompanied by a location property. Alternatively, bounds can be used.
                "radius", T<float>
            ]

    let TextSearchRequest =
        Config "google.maps.places.TextSearchRequest"
            []
            [
                // Bounds used to bias results when searching for Places (optional). Both location and radius will be ignored if bounds is set. Results will not be restricted to those inside these bounds; but, results inside it will rank higher.
                "bounds", LatLngBounds + LatLngBoundsLiteral

                // A language identifier for the language in which names and addresses should be returned, when possible. See the list of supported languages.
                "language", T<string>

                // The center of the area used to bias results when searching for Places.
                "location", LatLng + LatLngLiteral

                // The request's query term. e.g. the name of a place ('Eiffel Tower'), a category followed by the name of a location ('pizza in New York'), or the name of a place followed by a location disambiguator ('Starbucks in Sydney').
                "query", T<string>

                // The radius of the area used to bias results when searching for Places, in meters.
                "radius", T<float>

                // A region code to bias results towards. The region code accepts a ccTLD ("top-level domain") two-character value. Most ccTLD codes are identical to ISO 3166-1 codes, with some notable exceptions. For example, the United Kingdom's ccTLD is "uk" (.co.uk) while its ISO 3166-1 code is "gb" (technically for the entity of "The United Kingdom of Great Britain and Northern Ireland").
                "region", T<string>

                // Searches for places of the given type. The type is translated to the local language of the request's target location and used as a query string. If a query is also provided, it is concatenated to the localized type string. Results of a different type are dropped from the response. Use this field to perform language and region independent categorical searches.
                "type", T<string>
            ]

    let PlacesService =
        Class "google.maps.places.PlacesService"
        |+> Static [Constructor Element?AttrContainer]
        |+> Instance [
            "findPlaceFromPhoneNumber" => FindPlaceFromPhoneNumberRequest * (!?(Type.ArrayOf PlaceResult) * PlacesServiceStatus ^-> T<unit>) ^-> T<unit>
            |> WithComment "Retrieves a list of places based on a phone number. In most cases there should be just one item in the result list, however if the request is ambiguous more than one result may be returned. The PlaceResults passed to the callback are subsets of a full PlaceResult. Your app can get a more detailed PlaceResult for each place by calling PlacesService.getDetails and passing the PlaceResult.place_id for the desired place."

            "findPlaceFromQuery" => FindPlaceFromQueryRequest * (!?(Type.ArrayOf PlaceResult) * PlacesServiceStatus ^-> T<unit>) ^-> T<unit>
            |> WithComment "Retrieves a list of places based on a query string. In most cases there should be just one item in the result list, however if the request is ambiguous more than one result may be returned. The PlaceResults passed to the callback are subsets of a full PlaceResult. Your app can get a more detailed PlaceResult for each place by calling PlacesService.getDetails and passing the PlaceResult.place_id for the desired place."

            "getDetails" => PlaceDetailsRequest * (!?PlaceResult * PlacesServiceStatus ^-> T<unit>) ^-> T<unit>
            |> WithComment "Retrieves details about the place identified by the given placeId."

            "nearbySearch" => PlaceSearchRequest * (!?(Type.ArrayOf PlaceResult) * PlacesServiceStatus * !?PlaceSearchPagination ^-> T<unit>) ^-> T<unit>
            |> WithComment "Retrieves a list of places near a particular location, based on keyword or type. Location must always be specified, either by passing a LatLngBounds, or location and radius parameters. The PlaceResults passed to the callback are subsets of the full PlaceResult. Your app can get a more detailed PlaceResult for each place by sending a Place Details request passing the PlaceResult.place_id for the desired place. The PlaceSearchPagination object can be used to fetch additional pages of results (null if this is the last page of results or if there is only one page of results)."

            "textSearch" => TextSearchRequest * (!?(Type.ArrayOf PlaceResult) * PlacesServiceStatus * !?PlaceSearchPagination ^-> T<unit>) ^-> T<unit>
            |> WithComment "Retrieves a list of places based on a query string (for example, \"pizza in New York\", or \"shoe stores near Ottawa\"). Location parameters are optional; when the location is specified, results are only biased toward nearby results rather than restricted to places inside the area. Use textSearch when you want to search for places using an arbitrary string, and in cases where you may not want to restrict search results to a particular location. The PlaceSearchPagination object can be used to fetch additional pages of results (null if this is the last page of results or if there is only one page of results)."
        ]

    let SearchBoxOptions =
        Config "google.maps.places.SearchBoxOptions"
            []
            [
                // The area towards which to bias query predictions. Predictions are biased towards, but not restricted to, queries targeting these bounds.
                "bounds", LatLngBounds + LatLngBoundsLiteral
            ]

    let SearchBox =
        Class "google.maps.places.SearchBox"
        |+> Static [Constructor (Element?InputField * !?SearchBoxOptions)]
        |+> Instance [
            "getBounds" => T<unit> ^-> LatLngBounds
            |> WithComment "Returns the bounds to which query predictions are biased."

            "getPlaces" => T<unit> ^-> Type.ArrayOf PlaceResult
            |> WithComment "Returns the query selected by the user, or null if no places have been found yet, to be used with places_changed event."

            "setBounds" => (LatLngBounds + LatLngBoundsLiteral) ^-> T<unit>
            |> WithComment "Sets the region to use for biasing query predictions. Results will only be biased towards this area and not be completely restricted to it."

            "places_changed" =@ (T<unit> ^-> T<unit>)
            |> WithComment "This event is fired when the user selects a query, getPlaces should be used to get new places."
        ]
        |> ObsoleteWithMessage "Deprecated: As of March 1st, 2025, google.maps.places.SearchBox is not available to new customers. At this time, google.maps.places.SearchBox is not scheduled to be discontinued and will continue to receive bug fixes for any major regressions. At least 12 months notice will be given before support is discontinued. Please see https://developers.google.com/maps/legacy for additional details."

    let AutocompleteOptions =
        Config "google.maps.places.AutocompleteOptions"
            []
            [
                // The area in which to search for places.
                "bounds", LatLngBounds + LatLngBoundsLiteral

                // The component restrictions. Component restrictions are used to restrict predictions to only those within the parent component. E.g., the country.
                "componentRestrictions", ComponentRestrictions.Type

                // Fields to be included for the Place in the details response when the details are successfully retrieved, which will be billed for.
                // If ['ALL'] is passed in, all available fields will be returned and billed for (this is not recommended for production deployments).
                // For a list of fields see PlaceResult. Nested fields can be specified with dot-paths (for example, "geometry.location"). The default is ['ALL'].
                "fields", T<string[]>

                // Whether to retrieve only Place IDs. The PlaceResult made available when the place_changed event is fired will only have the place_id, types and name fields, with the place_id, types and description returned by the Autocomplete service.
                "placeIdOnly", T<bool>

                // A boolean value, indicating that the Autocomplete widget should only return those places that are inside the bounds of the Autocomplete widget at the time the query is sent.
                // Setting strictBounds to false (which is the default) will make the results biased towards, but not restricted to, places contained within the bounds.
                "strictBounds", T<bool>

                // The types of predictions to be returned. Four types are supported: 'establishment' for businesses, 'geocode' for addresses,
                // '(regions)' for administrative regions and '(cities)' for localities. If nothing is specified, all types are returned.
                "types", T<string[]>
            ]

    let AutocompletePrediction =
        Config "google.maps.places.AutocompletePrediction"
            []
            [
                // This is the unformatted version of the query suggested by the Places service.
                "description", T<string>

                // A set of substrings in the place's description that match elements in the user's input, suitable for use in highlighting those substrings.
                // Each substring is identified by an offset and a length, expressed in unicode characters.
                "matched_substring", Type.ArrayOf PredictionSubstring

                // A place ID that can be used to retrieve details about this place using the place details service (see PlacesService.getDetails).
                "place_id", T<string>

                // Structured information about the place's description, divided into a main text and a secondary text,
                // including an array of matched substrings from the autocomplete input, identified by an offset and a length, expressed in unicode characters.
                "structured_formatting", StructuredFormatting.Type

                // Information about individual terms in the above description, from most to least specific. For example, "Taco Bell", "Willitis", and "CA".
                "terms", Type.ArrayOf PredictionTerm

                // An array of types that the prediction belongs to, for example 'establishment' or 'geocode'.
                "types", T<string[]>

                // The distance in meters of the place from the AutocompletionRequest.origin.
                "distance_meters", T<float>
            ]

    let Autocomplete =
        Class "google.maps.places.Autocomplete"
        |=> Inherits MVC.MVCObject
        |+> Static [Constructor (HTMLInputElement?InputField * !?AutocompleteOptions)]
        |+> Instance [
            "getBounds" => T<unit> ^-> LatLngBounds
            |> WithComment "Returns the bounds to which predictions are biased."

            "getFields" => T<unit> ^-> T<string[]>
            |> WithComment "Returns the fields to be included for the Place in the details response when the details are successfully retrieved. For a list of fields see PlaceResult."

            "getPlace" => T<unit> ^-> PlaceResult
            |> WithComment "Returns the details of the Place selected by user if the details were successfully retrieved. Otherwise returns a stub Place object, with the name property set to the current value of the input field."

            "setBounds" => LatLngBounds ^-> T<unit>
            |> WithComment "Sets the preferred area within which to return Place results. Results are biased towards, but not restricted to, this area."

            "setComponentsRestrictions" => ComponentRestrictions ^-> T<unit>
            |> WithComment "Sets the component restrictions. Component restrictions are used to restrict predictions to only those within the parent component. E.g., the country."

            "setFields" => T<string[]> ^-> T<unit>
            |> WithComment "Sets the fields to be included for the Place in the details response when the details are successfully retrieved. For a list of fields see PlaceResult."

            "setOptions" => AutocompleteOptions ^-> T<unit>

            "setTypes" => T<string[]> ^-> T<unit>
            |> WithComment "Sets the types of predictions to be returned. Supported types are 'establishment' for businesses and 'geocode' for addresses. If no type is specified, both types will be returned. The setTypes method accepts a single element array."

            "place_changed" =? T<obj> -* T<unit> ^-> T<unit>
            |> WithComment "This event is fired when a PlaceResult is made available for a Place the user has selected.
If the user enters the name of a Place that was not suggested by the control and presses the Enter key, or if a Place Details request fails, the PlaceResult contains the user input in the name property, with no other properties defined."
        ]
        |> ObsoleteWithMessage "Deprecated: As of March 1st, 2025, google.maps.places.Autocomplete is not available to new customers. Please use PlaceAutocompleteElement instead. At this time, google.maps.places.Autocomplete is not scheduled to be discontinued, but PlaceAutocompleteElement is recommended over google.maps.places.Autocomplete. While google.maps.places.Autocomplete will continue to receive bug fixes for any major regressions, existing bugs in google.maps.places.Autocomplete will not be addressed. At least 12 months notice will be given before support is discontinued. Please see https://developers.google.com/maps/legacy for additional details and https://developers.google.com/maps/documentation/javascript/places-migration-overview for the migration guide."

    let AutocompletionRequest =
        Config "google.maps.places.AutocompletionRequest"
            []
            [
                // Bounds for prediction biasing. Predictions will be biased towards, but not restricted to, the given bounds. Both location and radius will be ignored if bounds is set.
                "bounds", LatLngBounds + LatLngBoundsLiteral

                // The component restrictions. Component restrictions are used to restrict predictions to only those within the parent component. E.g., the country.
                "componentRestrictions", ComponentRestrictions.Type

                // The user entered input string.
                "input", T<string>

                // A language identifier for the language in which the results should be returned, if possible. Results in the selected language may be given a higher ranking, but suggestions are not restricted to this language.
                "language", T<string>

                // Location for prediction biasing. Predictions will be biased towards the given location and radius. Alternatively, bounds can be used.
                "location", LatLng.Type

                // A soft boundary or hint to use when searching for places.
                "locationBias", LocationBias

                // Bounds to constrain search results.
                "locationRestriction", LocationRestriction

                // The character position in the input term at which the service uses text for predictions (the position of the cursor in the input field).
                "offset", T<int>

                // The location where AutocompletePrediction.distance_meters is calculated from.
                "origin", LatLng + LatLngLiteral

                // The radius of the area used for prediction biasing. The radius is specified in meters, and must always be accompanied by a location property. Alternatively, bounds can be used.
                "radius", T<float>

                // A region code which is used for result formatting and for result filtering. It does not restrict the suggestions to this country.
                // The region code accepts a ccTLD ("top-level domain") two-character value. Most ccTLD codes are identical to ISO 3166-1 codes, with some notable exceptions.
                // For example, the United Kingdom's ccTLD is "uk" (.co.uk) while its ISO 3166-1 code is "gb" (technically for the entity of "The United Kingdom of Great Britain and Northern Ireland").
                "region", T<string>

                // Unique reference used to bundle individual requests into sessions.
                "sessionToken", AutocompleteSessionToken.Type

                // The types of predictions to be returned. For supported types, see the developer's guide. If no types are specified, all types will be returned.
                "types", T<string[]>
            ]

    let AutocompleteResponse =
        Config "google.maps.places.AutocompleteResponse"
            []
            [
                // The list of AutocompletePredictions.
                "predictions", Type.ArrayOf AutocompletePrediction
            ]

    let AutocompleteService =
        Class "google.maps.places.AutocompleteService"
        |+> Static [Constructor T<unit>]
        |+> Instance [
            "getPlacePredictions" => AutocompletionRequest * (Type.ArrayOf AutocompletePrediction * PlacesServiceStatus ^-> T<unit>) ^-> T<unit>
            |> WithComment "Retrieves place autocomplete predictions based on the supplied autocomplete request."

            "getQueryPredictions" => QueryAutocompletionRequest * (Type.ArrayOf QueryAutocompletePrediction * PlacesServiceStatus ^-> T<unit>) ^-> T<unit>
            |> WithComment "Retrieves query autocomplete predictions based on the supplied query autocomplete request."
        ]


    (** Place Widgets **)
    let StringRange =
        Class "google.maps.places.StringRange"
        |+> Instance [
            "endOffset" =@ T<int>
            |> WithComment "Zero-based offset of the last Unicode character of the substring (exclusive)."

            "startOffset" =@ T<int>
            |> WithComment "Zero-based offset of the first Unicode character of the substring (inclusive)."
        ]

    let FormattableText =
        Class "google.maps.places.FormattableText"
        |+> Instance [
            "matches" =@ !| StringRange
            |> WithComment "A list of string ranges identifying where the input request matched in FormattableText.text. The ranges can be used to format specific parts of text."

            "text" =@ T<string>
            |> WithComment "Text that may be used as is or formatted with FormattableText.matches."
        ]

    let PostalAddressLiteral =
        Config "google.maps.places.PostalAddressLiteral"
            [ "regionCode", T<string> ]
            [
                "addressLines", !| T<string>
                "administrativeArea", T<string>
                "languageCode", T<string>
                "locality", T<string>
                "organization", T<string>
                "postalCode", T<string>
                "recipients", !| T<string>
                "sortingCode", T<string>
                "sublocality", T<string>
            ]

    let AddressValidationRequest =
        Config "google.maps.addressValidation.AddressValidationRequest"
            [ "address", PostalAddressLiteral.Type ]
            [ 
                "previousResponseId", T<string>
                "uspsCASSEnabled", T<bool> 
            ]

    let PlacePrediction =
        Class "google.maps.places.PlacePrediction"
        |+> Instance [
            "distanceMeters" =@ T<float>
            |> WithComment "The length of the geodesic in meters from origin if origin is specified."

            "mainText" =@ FormattableText
            |> WithComment "Represents the name of the Place."

            "placeId" =@ T<string>
            |> WithComment "The unique identifier of the suggested Place. This identifier can be used in other APIs that accept Place IDs."

            "secondaryText" =@ FormattableText
            |> WithComment "Represents additional disambiguating features (such as a city or region) to further identify the Place."

            "text" =@ FormattableText
            |> WithComment "Contains the human-readable name for the returned result."

            "types" =@ !| T<string>
            |> WithComment "List of types that apply to this Place from Table A or Table B in the Places API documentation."

            "fetchAddressValidation" => AddressValidationRequest ^-> Promise[Forward.AddressValidation]
            |> WithComment "Sends an Address Validation request associated with this autocomplete session."

            "toPlace" => T<unit> ^-> Forward.Place
            |> WithComment "Returns a Place representation of this PlacePrediction."
        ]

    let PlaceAutocompletePlaceSelectEvent =
        Class "google.maps.places.PlaceAutocompletePlaceSelectEvent"
        |=> Inherits Events.Event
        |+> Instance [
            "placePrediction" =? PlacePrediction
            |> WithComment "Convert this to a Place by calling PlacePrediction.toPlace."
        ]

    let PlaceAutocompleteRequestErrorEvent =
        Class "google.maps.places.PlaceAutocompleteRequestErrorEvent"
        |=> Inherits Events.Event

    let PlaceAutocompleteElementOptions =
        Interface "google.maps.places.PlaceAutocompleteElementOptions"
        |+> [
            "locationBias" =@ LocationBias

            "locationRestriction" =@ LocationRestriction

            "requestedLanguage" =@ T<string>

            "name" =@ T<string>
        ]
    
    let PostalAddress =
        Class "google.maps.places.PostalAddress"
        |+> Instance [
            "addressLines" =@ !| T<string>
            |> WithComment "Unstructured address lines describing the lower levels of an address."

            "administrativeArea" =@ T<string>
            |> WithComment "The highest administrative subdivision which is used for postal addresses of a country or region."

            "languageCode" =@ T<string>
            |> WithComment "BCP-47 language code of the contents of this address. Examples: 'zh-Hant', 'ja', 'ja-Latn', 'en'."

            "locality" =@ T<string>
            |> WithComment "Generally refers to the city/town portion of the address."

            "organization" =@ T<string>
            |> WithComment "The name of the organization at the address."

            "postalCode" =@ T<string>
            |> WithComment "Postal code of the address."

            "recipients" =@ !| T<string>
            |> WithComment "The recipient at the address."

            "regionCode" =@ T<string>
            |> WithComment "CLDR region code of the country/region of the address. Example: 'CH' for Switzerland."

            "sortingCode" =@ T<string>
            |> WithComment "Sorting code of the address."

            "sublocality" =@ T<string>
            |> WithComment "Sublocality of the address such as neighborhoods, boroughs, or districts."
        ]

    let PlacePredictionSelectEvent =
        Class "google.maps.places.PlacePredictionSelectEvent"
        |=> Inherits JsEvent
        |+> Instance [
            "placePrediction" =@ PlacePrediction
            |> WithComment "Convert this to a Place by calling PlacePrediction.toPlace."
        ]

    let PlaceAutocompleteElement =
        Class "google.maps.places.PlaceAutocompleteElement"
        |=> Inherits HTMLElement
        |+> Static [Constructor PlaceAutocompleteElementOptions]
        |+> Instance [
            "includedPrimaryTypes" =@ !| T<string>
            |> WithComment "Included primary Place type (for example, 'restaurant' or 'gas_station'). A Place is only returned if its primary type is included in this list. Up to 5 values can be specified. If no types are specified, all Place types are returned."

            "includedRegionCodes" =@ !| T<string>
            |> WithComment "Only include results in the specified regions, specified as up to 15 CLDR two-character region codes. An empty set will not restrict the results. If both locationRestriction and includedRegionCodes are set, the results will be located in the area of intersection."

            "locationBias" =@ LocationBias 
            |> WithComment "A soft boundary or hint to use when searching for places."

            "locationRestriction" =@ LocationRestriction 
            |> WithComment "Bounds to constrain search results."

            "name" =@ T<string> 
            |> WithComment "The name to be used for the input element. See https://developer.mozilla.org/en-US/docs/Web/HTML/Element/input#name for details. Follows the same behavior as the name attribute for inputs. Note that this is the name that will be used when a form is submitted. See https://developer.mozilla.org/en-US/docs/Web/HTML/Element/form for details."

            "origin" =@ (LatLng + LatLngLiteral + LatLngAltitude + LatLngAltitudeLiteral) 
            |> WithComment "The origin from which to calculate distance. If not specified, distance is not calculated. The altitude, if given, is not used in the calculation."

            "requestedLanguage" =@ T<string> 
            |> WithComment "A language identifier for the language in which the results should be returned, if possible. Results in the selected language may be given a higher ranking, but suggestions are not restricted to this language. See the list of supported languages."

            "requestedRegion" =@ T<string> 
            |> WithComment "A region code which is used for result formatting and for result filtering. It does not restrict the suggestions to this country. The region code accepts a ccTLD ('top-level domain') two-character value. Most ccTLD codes are identical to ISO 3166-1 codes, with some notable exceptions. For example, the United Kingdom's ccTLD is 'uk' (.co.uk) while its ISO 3166-1 code is 'gb'."

            "unitSystem" =@ UnitSystem 
            |> WithComment "The unit system used to display distances. If not specified, the unit system is determined by requestedRegion."

            // Methods
            "addEventListener" => T<string> * Function * (T<bool> + AddEventListenerOptions) ^-> T<unit>
            |> WithComment "Sets up a function that will be called whenever the specified event is delivered to the target."

            "removeEventListener" => T<string> * Function * (T<bool> + EventListenerOptions) ^-> T<unit>
            |> WithComment "Removes an event listener previously registered with addEventListener from the target."

            // Events
            "gmp-error" => JsEvent ^-> T<unit>
            |> WithComment "This event is fired when a request to the backend was denied (e.g. incorrect API key). This event does not bubble."

            "gmp-select" => PlacePredictionSelectEvent ^-> T<unit>
            |> WithComment "This event is fired when a user selects a place prediction. Contains a PlacePrediction object which can be converted to a Place object."
    
        ]

    (** Place **)
    let PlaceOptions =
        Config "google.maps.places.PlaceOptions"
            []
            [
                // The unique place id.
                "id", T<string>

                // A language identifier for the language in which details should be returned.
                "requestedLanguage", T<string>

                // A region code of the user's region. This can affect which photos may be returned, and possibly other things.
                // The region code accepts a ccTLD ("top-level domain") two-character value. Most ccTLD codes are identical to ISO 3166-1 codes,
                // with some notable exceptions. For example, the United Kingdom's ccTLD is "uk" (.co.uk) while its ISO 3166-1 code is "gb" (technically for the entity of "The United Kingdom of Great Britain and Northern Ireland").
                "requestedRegion", T<string>
            ]

    let AccessibilityOptions =
        Config "google.maps.places.AccessibilityOptions"
            []
            [
                // Whether a place has a wheelchair accessible entrance. Returns 'true' or 'false' if the value is known. Returns 'null' if the value is unknown.
                "hasWheelchairAccessibleEntrance", T<bool>

                // Whether a place has wheelchair accessible parking. Returns 'true' or 'false' if the value is known. Returns 'null' if the value is unknown.
                "hasWheelchairAccessibleParking", T<bool>

                // Whether a place has a wheelchair accessible restroom. Returns 'true' or 'false' if the value is known. Returns 'null' if the value is unknown.
                "hasWheelchairAccessibleRestroom", T<bool>

                // Whether a place offers wheelchair accessible seating. Returns 'true' or 'false' if the value is known. Returns 'null' if the value is unknown.
                "hasWheelchairAccessibleSeating", T<bool>
            ]

    let AddressComponent =
        Class "google.maps.places.AddressComponent"
        |+> Instance [
            "longText" =@ T<string>
            |> WithComment "The full text of the address component."

            "shortText" =@ T<string>
            |> WithComment "The abbreviated, short text of the given address component."

            "types" =@ T<string[]>
            |> WithComment "An array of strings denoting the type of this address component."
        ]

    let Attribution =
        Class "google.maps.places.Attribution"
        |+> Instance [
            "provider" =@ T<string>
            |> WithComment "Attribution text to be displayed for this Place result."

            "providerURI" =@ T<string>
        ]

    let FetchFieldsRequest =
        Class "google.maps.places.FetchFieldsRequest "
        |+> Instance [
            "fields" =@ T<string[]>
            |> WithComment "List of fields to be fetched."
        ]

    let OpeningHoursPoint =
        Class "google.maps.places.OpeningHoursPoint"
        |+> Instance [
            "day" =@ T<int>
            |> WithComment "The day of the week, as a number in the range [0, 6], starting on Sunday. For example, 2 means Tuesday."

            "hour" =@ T<int>
            |> WithComment "The hour of the OpeningHoursPoint.time as a number, in the range [0, 23]. This will be reported in the Place’s time zone."

            "minute" =@ T<int>
            |> WithComment "The minute of the OpeningHoursPoint.time as a number, in the range [0, 59]. This will be reported in the Place’s time zone."
        ]

    let OpeningHoursPeriod =
        Class "google.maps.places.OpeningHoursPeriod"
        |+> Instance [
            "close" =? OpeningHoursPoint
            |> WithComment "The closing time for the Place."

            "open" =? OpeningHoursPoint
            |> WithComment "The opening time for the Place."
        ]

    let OpeningHours =
        Class "google.maps.places.OpeningHours"
        |+> Instance [
            "periods" =? Type.ArrayOf OpeningHoursPeriod
            |> WithComment "Opening periods covering each day of the week, starting from Sunday, in chronological order. Does not include days where the Place is not open."

            "weekdayDescriptions" =? T<string[]>
            |> WithComment "An array of seven strings representing the formatted opening hours for each day of the week. The Places Service will format and localize the opening hours appropriately for the current language. The ordering of the elements in this array depends on the language. Some languages start the week on Monday, while others start on Sunday."
        ]

    let ParkingOptions =
        Config "google.maps.places.ParkingOptions"
            []
            [
                // Whether a place offers free garage parking. Returns 'true' or 'false' if the value is known. Returns 'null' if the value is unknown.
                "hasFreeGarageParking", T<bool>

                // Whether a place offers free parking lots. Returns 'true' or 'false' if the value is known. Returns 'null' if the value is unknown.
                "hasFreeParkingLot", T<bool>

                // Whether a place offers free street parking. Returns 'true' or 'false' if the value is known. Returns 'null' if the value is unknown.
                "hasFreeStreetParking", T<bool>

                // Whether a place offers paid garage parking. Returns 'true' or 'false' if the value is known. Returns 'null' if the value is unknown.
                "hasPaidGarageParking", T<bool>

                // Whether a place offers paid parking lots. Returns 'true' or 'false' if the value is known. Returns 'null' if the value is unknown.
                "hasPaidParkingLot", T<bool>

                // Whether a place offers paid street parking. Returns 'true' or 'false' if the value is known. Returns 'null' if the value is unknown.
                "hasPaidStreetParking", T<bool>

                // Whether a place offers valet parking. Returns 'true' or 'false' if the value is known. Returns 'null' if the value is unknown.
                "hasValetParking", T<bool>
            ]

    let PaymentOptions =
        Config "google.maps.places.PaymentOptions"
            []
            [
                // Whether a place only accepts payment via cash. Returns 'true' or 'false' if the value is known. Returns 'null' if the value is unknown.
                "acceptsCashOnly", T<bool>

                // Whether a place accepts payment via credit card. Returns 'true' or 'false' if the value is known. Returns 'null' if the value is unknown.
                "acceptsCreditCards", T<bool>

                // Whether a place accepts payment via debit card. Returns 'true' or 'false' if the value is known. Returns 'null' if the value is unknown.
                "acceptsDebitCards", T<bool>

                // Whether a place accepts payment via NFC. Returns 'true' or 'false' if the value is known. Returns 'null' if the value is unknown.
                "acceptsNFC", T<bool>
            ]

    let AuthorAttribution =
        Class "google.maps.places.AuthorAttribution"
        |+> Instance [
            "displayName" =@ T<string>
            |> WithComment "Author's name for this result."

            "photoURI" =@ T<string>
            |> WithComment "Author's photo URI for this result. This may not always be available."

            "uri" =@ T<string>
            |> WithComment "Author's profile URI for this result."
        ]

    let PlusCode =
        Class "google.maps.places.PlusCode"
        |+> Instance [
            "compoundCode" =? T<string>
            |> WithComment "A plus code with a 1/8000th of a degree by 1/8000th of a degree area where the first four characters (the area code) are dropped and replaced with a locality description. For example, \"9G8F+5W Zurich, Switzerland\"."

            "globalCode" =? T<string>
            |> WithComment "A plus code with a 1/8000th of a degree by 1/8000th of a degree area. For example, \"8FVC9G8F+5W\"."
        ]

    let Review =
        Class "google.maps.places.Review"
        |+> Instance [
            "authorAttribution" =? AuthorAttribution
            |> WithComment "The reviewer."

            "publishTime" =? Date

            "rating" =? T<float>
            |> WithComment "The rating of this review, a number between 1.0 and 5.0 (inclusive)."

            "relativePublishTimeDescription" =? T<string>
            |> WithComment "A string of formatted recent time, expressing the review time relative to the current time in a form appropriate for the language and country. For example `\"a month ago\"'."

            "text" =? T<string>
            |> WithComment "The text of a review."

            "textLanguageCode" =? T<string>
            |> WithComment "An IETF language code indicating the language in which this review is written. Note that this code includes only the main language tag without any secondary tag indicating country or region. For example, all the English reviews are tagged as 'en' rather than 'en-AU' or 'en-UK'."
        ]

    let Photo =
        Class "google.maps.places.Photo"
        |+> Instance [
            "authorAttributions" =@ Type.ArrayOf AuthorAttribution
            |> WithComment "Attribution text to be displayed for this photo."

            "heightPx" =@ T<float>
            |> WithComment "The height of the photo in pixels."

            "widthPx" =@ T<float>
            |> WithComment "The width of the photo in pixels."

            "getURI" => !?PhotoOptions ^-> T<string>
            |> WithComment "Returns the image URL corresponding to the specified options."
        ]

    let PriceLevel =
        Pattern.EnumStrings "PriceLevel" ["EXPENSIVE"; "FREE"; "INEXPENSIVE"; "MODERATE"; "VERY_EXPENSIVE" ]

    let SearchByTextRankPreference =
        Pattern.EnumStrings "SearchByTextRankPreference" ["DISTANCE"; "RELEVANCE"; ]

    let SearchByTextRequest =
        Config "google.maps.places.SearchByTextRequest"
            []
            [
                // Fields to be included in the response, which will be billed for. If ['*'] is passed in, all available fields will be returned and billed for (this is not recommended for production deployments). For a list of fields see PlaceResult. Nested fields can be specified with dot-paths (for example, "geometry.location").
                "fields", T<string[]>

                // The requested place type. Full list of types supported: https://developers.google.com/maps/documentation/places/web-service/place-types. Only one included type is supported. See SearchByTextRequest.useStrictTypeFiltering
                "includedType", T<string>

                // Used to restrict the search to places that are currently open.
                "isOpenNow", T<bool>

                // Place details will be displayed with the preferred language if available. Will default to the browser's language preference. Current list of supported languages: https://developers.google.com/maps/faq#languagesupport.
                "language", T<string>

                // The region to search. This location serves as a bias which means results around given location might be returned. Cannot be set along with locationRestriction.
                "locationBias", LatLng + LatLngLiteral + LatLngBounds + LatLngBoundsLiteral + CircleLiteral + Circle

                // The region to search. This location serves as a restriction which means results outside given location will not be returned. Cannot be set along with locationBias.
                "locationRestriction", LatLngBounds + LatLngBoundsLiteral

                // Maximum number of results to return. It must be between 1 and 20, inclusively.
                "maxResultCount", T<float>

                // Filter out results whose average user rating is strictly less than this limit. A valid value must be an float between 0 and 5 (inclusively) at a 0.5 cadence i.e. [0, 0.5, 1.0, ... , 5.0] inclusively. The input rating will be rounded up to the nearest 0.5(ceiling). For instance, a rating of 0.6 will eliminate all results with a less than 1.0 rating.
                "minRating", T<float>

                // Used to restrict the search to places that are marked as certain price levels. Any combinations of price levels can be chosen. Defaults to all price levels.
                "priceLevels", Type.ArrayOf PriceLevel

                // Deprecated: Please use textQuery instead
                "query", T<string>

                // Deprecated: Please use rankPreference instead.
                "rankBy", SearchByTextRankPreference.Type

                // How results will be ranked in the response. Default: SearchByTextRankPreference.DISTANCE
                "rankPreference", SearchByTextRankPreference.Type

                // The Unicode country/region code (CLDR) of the location where the request is coming from. This parameter is used to display the place details, like region-specific place name, if available. The parameter can affect results based on applicable law. For more information, see https://www.unicode.org/cldr/charts/latest/supplemental/territory_language_information.html. Note that 3-digit region codes are not currently supported.
                "region", T<string>

                // Required. The text query for textual search.
                "textQuery", T<string>

                // Default: false. Used to set strict type filtering for SearchByTextRequest.includedType. If set to true, only results of the same type will be returned.
                "useStrictTypeFiltering", T<bool>
            ]

    let SearchNearbyRankPreference =
        Pattern.EnumStrings "google.maps.places.SearchNearbyRankPreference" ["DISTANCE"; "POPULARITY" ]

    let SearchNearbyRequest =
        Config "google.maps.places.SearchNearbyRequest"
            [
                "locationRestriction", Circle + CircleLiteral
            ]
            [
                "excludedPrimaryTypes", !| T<string>
                "excludedTypes", !| T<string>
                "fields", !| T<string>
                "includedPrimaryTypes", !| T<string>
                "includedTypes", !| T<string>
                "language", T<string>
                "maxResultCount", T<int>
                "rankPreference", SearchNearbyRankPreference.Type
                "region", T<string>
            ]

    let EVConnectorType =
        Pattern.EnumStrings "google.maps.places.EVConnectorType" [
            "CCS_COMBO_1"
            "CCS_COMBO_2"
            "CHADEMO"
            "J1772"
            "NACS"
            "OTHER"
            "TESLA"
            "TYPE_2"
            "UNSPECIFIED_GB_T"
            "UNSPECIFIED_WALL_OUTLET"
        ]

    let ConnectorAggregation =
        Class "google.maps.places.ConnectorAggregation"
        |+> Instance [
            "availabilityLastUpdateTime" =@ Date
            |> WithComment "The time when the connector availability information in this aggregation was last updated."

            "availableCount" =@ T<int>
            |> WithComment "Number of connectors in this aggregation that are currently available."

            "count" =@ T<int>
            |> WithComment "Number of connectors in this aggregation."

            "maxChargeRateKw" =@ T<float>
            |> WithComment "The static max charging rate in kw of each connector of the aggregation."

            "outOfServiceCount" =@ T<int>
            |> WithComment "Number of connectors in this aggregation that are currently out of service."

            "type" =@ EVConnectorType
            |> WithComment "The connector type of this aggregation."
        ]

    let EVChargeOptions =
        Config "google.maps.places.EVChargeOptions"
            []
            [
                "connectorAggregations", !| ConnectorAggregation
                "connectorCount", T<int>
            ]

    let Money =
        Class "google.maps.places.Money"
        |+> Instance [
            "currencyCode" =@ T<string>
            |> WithComment "The three-letter currency code, defined in ISO 4217."

            "nanos" =@ T<int>
            |> WithComment "Number of nano (10^-9) units of the amount."

            "units" =@ T<int>
            |> WithComment "The whole units of the amount. For example, if Money.currencyCode is 'USD', then 1 unit is 1 US dollar."

            "toString" => T<unit> ^-> T<string>
            |> WithComment "Returns a human-readable representation of the amount of money with its currency symbol."
        ]

    let PriceRange =
        Class "google.maps.places.PriceRange"
        |+> Instance [
            "endPrice" =@ Money
            |> WithComment "The upper end of the price range (inclusive). Price should be lower than this amount."

            "startPrice" =@ Money
            |> WithComment "The low end of the price range (inclusive). Price should be at or above this amount."
        ]

    let FuelType =
        Pattern.EnumStrings "google.maps.places.FuelType" [
            "BIO_DIESEL"
            "DIESEL"
            "DIESEL_PLUS"
            "E100"
            "E80"
            "E85"
            "LPG"
            "METHANE"
            "MIDGRADE"
            "PREMIUM"
            "REGULAR_UNLEADED"
            "SP100"
            "SP91"
            "SP91_E10"
            "SP92"
            "SP95"
            "SP95_E10"
            "SP98"
            "SP99"
            "TRUCK_DIESEL"
        ]

    let FuelPrice =
        Class "google.maps.places.FuelPrice"
        |+> Instance [
            "price" =@ Money
            |> WithComment "The price of the fuel."

            "type" =@ FuelType
            |> WithComment "The type of fuel."

            "updateTime" =@ Date
            |> WithComment "The time the fuel price was last updated."
        ]

    let FuelOptions =
        Class "google.maps.places.FuelOptions"
        |+> Instance [
            "fuelPrices" =@ Type.ArrayOf FuelPrice
            |> WithComment "A list of fuel prices for each type of fuel this station has, one entry per fuel type."
        ]    

    let Place =
        Forward.Place
        |+> Static [
            Constructor PlaceOptions

            "searchByText" => SearchByTextRequest ^-> Promise[!| TSelf]
            |> WithComment "Text query based place search."

            "searchNearby" => SearchNearbyRequest ^-> Promise[!| TSelf]
            |> WithComment "Search for nearby places."
        ]
        |+> Instance [
            "accessibilityOptions" =@ AccessibilityOptions
            |> WithComment "Accessibility options of this Place. undefined if the accessibility options data have not been called for from the server."

            "addressComponents" =@ Type.ArrayOf AddressComponent
            |> WithComment "The collection of address components for this Place’s location. Empty object if there is no known address data. undefined if the address data has not been called for from the server."

            "adrFormatAddress" =@ T<string>
            |> WithComment "The representation of the Place’s address in the adr microformat."

            "allowsDogs" =@ T<bool>

            "attributions" =@ Type.ArrayOf Attribution
            |> WithComment "Attribution text to be displayed for this Place result."

            "businessStatus" =@ BusinessStatus
            |> WithComment "The location's operational status. null if there is no known status. undefined if the status data has not been loaded from the server."

            "displayName" =@ T<string>
            |> WithComment "The location's display name. null if there is no name. undefined if the name data has not been loaded from the server."

            "displayNameLanguageCode" =@ T<string>
            |> WithComment "The language of the location's display name. null if there is no name. undefined if the name data has not been loaded from the server."

            "editorialSummary" =@ T<string>
            |> WithComment "The editorial summary for this place. null if there is no editorial summary. undefined if this field has not yet been requested."

            "editorialSummaryLanguageCode" =@ T<string>
            |> WithComment "The language of the editorial summary for this place. null if there is no editorial summary. undefined if this field has not yet been requested."

            "evChargeOptions" =@ EVChargeOptions
            |> WithComment "EV Charge options provided by the place. undefined if the EV charge options have not been called for from the server."

            "formattedAddress" =@ T<string>
            |> WithComment "The locations’s full address."

            "fuelOptions" =@ FuelOptions
            |> WithComment "Fuel options provided by the place. undefined if the fuel options have not been called for from the server."

            "googleMapsURI" =@ T<string>
            |> WithComment "URL of the official Google page for this place. This is the Google-owned page that contains the best available information about the Place."

            "hasCurbsidePickup" =@ T<bool>
            |> WithComment "Whether a place has curbside pickup. Returns 'true' or 'false' if the value is known. Returns 'null' if the value is unknown. Returns 'undefined' if this field has not yet been requested."

            "hasDelivery" =@ T<bool>
            |> WithComment "Whether a place has delivery. Returns 'true' or 'false' if the value is known. Returns 'null' if the value is unknown. Returns 'undefined' if this field has not yet been requested."

            "hasDineIn" =@ T<bool>
            |> WithComment "Whether a place has dine in. Returns 'true' or 'false' if the value is known. Returns 'null' if the value is unknown. Returns 'undefined' if this field has not yet been requested."

            "hasLiveMusic" =@ T<bool>

            "hasMenuForChildren" =@ T<bool>

            "hasOutdoorSeating" =@ T<bool>

            "hasRestroom" =@ T<bool>

            "hasTakeout" =@ T<bool>
            |> WithComment "Whether a place has takeout. Returns 'true' or 'false' if the value is known. Returns 'null' if the value is unknown. Returns 'undefined' if this field has not yet been requested."

            "hasWiFi" =@ T<bool>
            |> ObsoleteWithMessage "This field was accidentally documented, but has never actually been populated."

            "iconBackgroundColor" =@ T<string>
            |> WithComment "The default HEX color code for the place's category."

            "id" =@ T<string>
            |> WithComment "The unique place id."

            "internationalPhoneNumber" =@ T<string>
            |> WithComment "The Place’s phone number in international format. International format includes the country code, and is prefixed with the plus (+) sign."

            "isGoodForChildren" =@ T<bool>

            "isGoodForGroups" =@ T<bool>

            "isGoodForWatchingSports" =@ T<bool>

            "isReservable" =@ T<bool>
            |> WithComment " Whether a place is reservable. Returns 'true' or 'false' if the value is known. Returns 'null' if the value is unknown. Returns 'undefined' if this field has not yet been requested."

            "location" =@ LatLng
            |> WithComment "The Place’s position."

            "nationalPhoneNumber" =@ T<string>
            |> WithComment "The Place’s phone number, formatted according to the number's regional convention."

            "parkingOptions" =@ ParkingOptions
            |> WithComment "Options of parking provided by the place. undefined if the parking options data have not been called for from the server."

            "paymentOptions" =@ PaymentOptions
            |> WithComment "Payment options provided by the place. undefined if the payment options data have not been called for from the server."

            "photos" =@ Type.ArrayOf Photo
            |> WithComment "Photos of this Place. The collection will contain up to ten Photo objects."

            "plusCode" =@ PlusCode

            "postalAddress" =@ PostalAddress

            "priceLevel" =@ PriceLevel
            |> WithComment "The price level of the Place. This property can return Free, Inexpensive, Moderate, Expensive, Very Expensive."

            "priceRange" =@ PriceRange
            |> WithComment "The price range for this Place. The endPrice may be unset, which indicates a range without upper bound (e.g. 'More than $100')."

            "primaryType" =@ T<string>
            |> WithComment "The location's primary type. null if there is no type. undefined if the type data has not been loaded from the server."

            "primaryTypeDisplayName" =@ T<string>
            |> WithComment "The location's primary type display name. null if there is no type. undefined if the type data has not been loaded from the server."

            "primaryTypeDisplayNameLanguageCode" =@ T<string>
            |> WithComment "The language of the location's primary type display name. null if there is no type. undefined if the type data has not been loaded from the server."

            "rating" =@ T<float>
            |> WithComment "A rating, between 1.0 to 5.0, based on user reviews of this Place."

            "regularOpeningHours" =@ OpeningHours

            "requestedLanguage" =@ T<string>
            |> WithComment "The requested language for this place."

            "requestedRegion" =@ T<string>
            |> WithComment "The requested region for this place."

            "reviews" =@ Type.ArrayOf Review
            |> WithComment "A list of reviews for this Place."

            "servesBeer" =@ T<bool>
            |> WithComment "Whether a place serves beer. Returns 'true' or 'false' if the value is known. Returns 'null' if the value is unknown. Returns 'undefined' if this field has not yet been requested."

            "servesBreakfast" =@ T<bool>
            |> WithComment "Whether a place serves breakfast. Returns 'true' or 'false' if the value is known. Returns 'null' if the value is unknown. Returns 'undefined' if this field has not yet been requested."

            "servesBrunch" =@ T<bool>
            |> WithComment "Whether a place serves brunch. Returns 'true' or 'false' if the value is known. Returns 'null' if the value is unknown. Returns 'undefined' if this field has not yet been requested."

            "servesCocktails" =@ T<bool>

            "servesCoffee" =@ T<bool>

            "servesDessert" =@ T<bool>

            "servesDinner" =@ T<bool>
            |> WithComment "Whether a place serves dinner. Returns 'true' or 'false' if the value is known. Returns 'null' if the value is unknown. Returns 'undefined' if this field has not yet been requested."

            "servesLunch" =@ T<bool>
            |> WithComment "Whether a place serves lunch. Returns 'true' or 'false' if the value is known. Returns 'null' if the value is unknown. Returns 'undefined' if this field has not yet been requested."

            "servesVegetarianFood" =@ T<bool>
            |> WithComment "Whether a place serves vegetarian food. Returns 'true' or 'false' if the value is known. Returns 'null' if the value is unknown. Returns 'undefined' if this field has not yet been requested."

            "servesWine" =@ T<bool>
            |> WithComment "Whether a place serves wine. Returns 'true' or 'false' if the value is known. Returns 'null' if the value is unknown. Returns 'undefined' if this field has not yet been requested."

            "svgIconMaskURI" =@ T<string>
            |> WithComment "URI to the svg image mask resource that can be used to represent a place’s category."

            "types" =@ T<string[]>
            |> WithComment "An array of types for this Place (for example, [\"political\", \"locality\"] or [\"restaurant\", \"establishment\"])."

            "userRatingCount" =@ T<float>
            |> WithComment "The number of user ratings which contributed to this Place’s Place.rating."

            "utcOffsetMinutes" =@ T<float>
            |> WithComment "The offset from UTC of the Place’s current timezone, in minutes. For example, Austrialian Eastern Standard Time (GMT+10) in daylight savings is 11 hours ahead of UTC, so the utc_offset_minutes will be 660. For timezones behind UTC, the offset is negative. For example, the utc_offset_minutes is -60 for Cape Verde."

            "viewport" =@ LatLngBounds
            |> WithComment "The preferred viewport when displaying this Place on a map."

            "websiteURI" =@ T<string>
            |> WithComment "The authoritative website for this Place, such as a business' homepage."

            "openingHours" =@ OpeningHours
            |> ObsoleteWithMessage "Deprecated: Use Place.regularOpeningHours instead."

            "fetchFields" => FetchFieldsRequest ^-> Promise[TSelf]

            "getNextOpeningTime" => !?Date ^-> Promise[Date]
            |> WithComment "Calculates the Date representing the next OpeningHoursTime. Returns undefined if the data is insufficient to calculate the result, or this place is not operational."

            "isOpen" => !?Date ^-> Promise[T<bool>]
            |> WithComment "Check if the place is open at the given datetime. Resolves with undefined if the known data for the location is insufficient to calculate this, e.g. if the opening hours are unregistered."

            "toJSON" => T<unit> ^-> T<obj>
        ]

    let PlaceDetailsOrientation =
        Pattern.EnumStrings "google.maps.places.PlaceDetailsOrientation" ["HORIZONTAL"; "VERTICAL"; ]

    let PlaceDetailsCompactElementOptions =
        Config "google.maps.places.PlaceDetailsCompactElementOptions"
            []
            [
                "orientation", PlaceDetailsOrientation.Type
                "truncationPreferred", T<bool>
            ]

    let PlaceDetailsCompactElement =
        Class "google.maps.places.PlaceDetailsCompactElement"
        |=> Inherits HTMLElement
        |+> Static [
            Constructor (!? PlaceDetailsCompactElementOptions)
        ]
        |+> Instance [
            "orientation" =@ PlaceDetailsOrientation
            |> WithComment "The orientation variant (vertical or horizontal) of the element."

            "place" =@ Forward.Place
            |> WithComment "Place object containing the ID, location, and viewport of the currently rendered place."

            "truncationPreferred" =@ T<bool>
            |> WithComment "If true, truncates the place name and address to fit on one line instead of wrapping."

            "addEventListener" => T<string> * Function * (T<bool> + AddEventListenerOptions) ^-> T<unit>
            |> WithComment "Sets up a function that will be called whenever the specified event is delivered to the target."

            "removeEventListener" => T<string> * Function * (T<bool> + EventListenerOptions) ^-> T<unit>
            |> WithComment "Removes an event listener previously registered with addEventListener from the target."

            "gmp-error" => JsEvent ^-> T<unit>
            |> WithComment "This event is fired when a request to the backend was denied (e.g. incorrect API key). This event does not bubble."

            "gmp-load" => JsEvent ^-> T<unit>
            |> WithComment "This event is fired when the element loads and renders its content. This event does not bubble."
        ]

    let PlaceDetailsElement =
        Class "google.maps.places.PlaceDetailsElement"
        |=> Inherits HTMLElement
        |+> Static [
            Constructor T<unit>
        ]
        |+> Instance [
            "place" =@ Forward.Place
            |> WithComment "Place object containing the ID, location, and viewport of the currently rendered place."

            "addEventListener" => T<string> * Function * (T<bool> + AddEventListenerOptions) ^-> T<unit>
            |> WithComment "Sets up a function that will be called whenever the specified event is delivered to the target."

            "removeEventListener" => T<string> * Function * (T<bool> + EventListenerOptions) ^-> T<unit>
            |> WithComment "Removes an event listener previously registered with addEventListener from the target."

            "gmp-error" => JsEvent ^-> T<unit>
            |> WithComment "This event is fired when a request to the backend was denied (e.g. incorrect API key). This event does not bubble."

            "gmp-load" => JsEvent ^-> T<unit>
            |> WithComment "This event is fired when the element loads and renders its content. This event does not bubble."
        ]