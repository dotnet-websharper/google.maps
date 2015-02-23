/// Definitions for the Places part of the API. See:
/// http://developers.google.com/maps/documentation/javascript/reference
module WebSharper.Google.Maps.Places

open WebSharper.InterfaceGenerator
open WebSharper.Google.Maps.Notation
open WebSharper.Google.Maps.Base
open WebSharper.Google.Maps.Specification
open WebSharper.JavaScript.Dom

let ComponentRestrictions =
    Config "google.maps.places.ComponentRestrictions"
    |+> Instance [
        "country" =@ T<string>
        |> WithComment "Restricts predictions to the specified country (ISO 3166-1 Alpha-2 country code, case insensitive). E.g., us, br, au."
    ]

let PlaceAspectRating =
    Class "google.maps.places.PlaceAspectRating"
    |+> Instance [
        "rating" =? T<int>
        |> WithComment "The rating of this aspect. For individual reviews this is an integer from 0 to 3. For aggregated ratings of a place this is an integer from 0 to 30."

        "type" =? T<string>
        |> WithComment "The aspect type, e.g. \"food\", \"decor\", \"service\", \"overall\"."
    ]

let PlaceDetailsRequest =
    Config "google.maps.places.PlaceDetailsRequest"
    |+> Instance [
        "reference" =@ T<string>
        |> WithComment "The reference of the Place for which details are being requested."
    ]

let PlaceGeometry =
    Class "google.maps.places.PlaceGeometry"
    |+> Instance [
        "location" =? LatLng
        |> WithComment "The Place's position."

        "viewport" =? LatLngBounds
        |> WithComment "The preferred viewport when displaying this Place on a map. This property will be null if the preferred viewport for the Place is not known."
    ]

let PhotoOptions =
    Config "google.maps.places.PhotoOptions"
    |+> Instance [
        "maxHeight" =@ T<int>
        "maxWidth" =@ T<int>
    ]

let PlacePhoto =
    Class "google.maps.places.PlacePhoto"
    |+> Instance [
        "getUrl" => PhotoOptions ^-> T<string>
        |> WithComment "Returns the image URL corresponding to the specified options. You must include a PhotoOptions object with at least one of maxWidth or maxHeight specified."

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

        "author_name" =? T<string>
        |> WithComment "The name of the reviewer."

        "author_url" =? T<string>
        |> WithComment "A link to the reviewer's profile. This will be undefined when the reviewer's profile is unavailable."

        "text" =? T<string>
        |> WithComment "The text of a review."
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
    let t = Type.New()
    Class "google.maps.places.RankBy"
    |=> t
    |+> Static [
        "DISTANCE" =? t
        |> WithComment "Ranks place results by distance from the location."

        "PROMINENCE" =? t
        |> WithComment "Ranks place results by their prominence."
    ]

let PlaceSearchRequest =
    Config "google.maps.places.PlaceSearchRequest"
    |+> Instance [
        "bounds" =@ LatLngBounds
        |> WithComment "The bounds within which to search for Places. Both location and radius will be ignored if bounds is set."

        "keyword" =@ T<string>
        |> WithComment "A term to be matched against all available fields, including but not limited to name, type, and address, as well as customer reviews and other third-party content."

        "location" =@ LatLng
        |> WithComment "The location around which to search for Places."

        "maxPriceLevel" =@ T<int>
        |> WithComment "Restricts results to only those places at the specified price level or lower. Valid values are in the range from 0 (most affordable) to 4 (most expensive), inclusive. Must be greater than or equal to minPrice , if specified."

        "minPriceLevel" =@ T<int>
        |> WithComment "Restricts results to only those places at the specified price level or higher. Valid values are in the range from 0 (most affordable) to 4 (most expensive), inclusive. Must be less than or equal to maxPrice, if specified."

        "name" =@ T<string>
        |> WithComment "Restricts the Place search results to Places that include this text in the name."

        "openNow" =@ T<bool>
        |> WithComment "Restricts results to only those places that are open right now."

        "radius" =@ T<float>
        |> WithComment "The distance from the given location within which to search for Places, in meters. The maximum allowed value is 50000."

        "rankBy" =@ RankBy
        |> WithComment "Specifies the ranking method to use when returning results."

        "types" =@ T<string[]>
        |> WithComment "Restricts the Place search results to Places with a type matching at least one of the specified types in this array. Valid types are given here."
    ]

let PlaceResult =
    Class "google.maps.places.PlaceResult"
    |+> Instance [
        "address_components" =? Type.ArrayOf GeocoderAddressComponent
        |> WithComment "The collection of address components for this Place's location."

        "aspects" =? Type.ArrayOf PlaceAspectRating
        |> WithComment "The rated aspects of this Place, based on Google and Zagat user reviews. The ratings are on a scale of 0 to 30."

        "formatted_address" =? T<string>
        |> WithComment "The Place's full address."

        "formatted_phone_number" =? T<string>
        |> WithComment "The Place's phone number, formatted according to the number's regional convention."

        "geometry" =? PlaceGeometry
        |> WithComment "The Place's geometry-related information."

        "html_attributions" =? T<string[]>
        |> WithComment "Attribution text to be displayed for this Place result."

        "icon" =? T<string>
        |> WithComment "URL to an image resource that can be used to represent this Place's category."

        "id" =? T<string>
        |> WithComment "A unique identifier denoting this Place. This identifier may not be used to retrieve information about this Place, and to verify the identity of a Place across separate searches. As ids can occasionally change, it is recommended that the stored id for a Place be compared with the id returned in later Details requests for the same Place, and updated if necessary."

        "international_phone_number" =? T<string>
        |> WithComment "The Place's phone number in international format. International format includes the country code, and is prefixed with the plus (+) sign."

        "name" =? T<string>
        |> WithComment "The Place's name. Note: In the case of user entered Places, this is the raw text, as typed by the user. Please exercise caution when using this data, as malicious users may try to use it as a vector for code injection attacks (See http://en.wikipedia.org/wiki/Code_injection)."

        "permanently_closed" =? T<bool>
        |> WithComment "A flag indicating whether the Place is permanently closed. If the place is not permanently closed, the flag is not present in search or details responses."

        "photos" =? Type.ArrayOf PlacePhoto
        |> WithComment "Photos of this Place. The collection will contain up to ten PlacePhoto objects."

        "price_level" =? T<int>
        |> WithComment "The price level of the Place, on a scale of 0 to 4. Price levels are interpreted as follows:
0:	Free
1:	Inexpensive
2:	Moderate
3:	Expensive
4:	Very Expensive"

        "rating" =? T<float>
        |> WithComment "The Place's rating, from 0.0 to 5.0, based on user reviews."

        "reference" =? T<string>
        |> WithComment "An opaque string that may be used to retrieve up-to-date information about this Place (via PlacesService.getDetails()). reference contains a unique token that you can use to retrieve additional information about this Place in a Place Details request. You can store this token and use it at any time in future to refresh cached data about this Place, but the same token is not guaranteed to be returned for any given Place across different searches."

        "review_summary" =? T<string>
        |> WithComment "The editorial review summary. Only visible in details responses, for customers of Maps API for Business and when extensions: 'review_summary' is specified in the details request. The review_summary field is experimental, and subject to change."

        "reviews" =? Type.ArrayOf PlaceReview
        |> WithComment "A list of reviews of this Place."

        "types" =? T<string[]>
        |> WithComment "An array of types for this Place (e.g., [\"political\",  \"locality\"] or [\"restaurant\", \"establishment\"])."

        "url" =? T<string>
        |> WithComment "URL of the associated Google Place Page."

        "vicinity" =? T<string>
        |> WithComment "A fragment of the Place's address for disambiguation (usually street name and locality)."

        "website" =? T<string>
        |> WithComment "The authoritative website for this Place, such as a business' homepage."
    ]

let PlacesServiceStatus =
    let t = Type.New()
    Class "google.maps.places.PlacesServiceStatus"
    |=> t
    |+> Static [
        "INVALID_REQUEST" =? t
        |> WithComment "This request was invalid."

        "OK" =? t
        |> WithComment "The response contains a valid result."

        "OVER_QUERY_LIMIT" =? t
        |> WithComment "The application has gone over its request quota."

        "REQUEST_DENIED" =? t
        |> WithComment "The application is not allowed to use the PlacesService."

        "UNKNOWN_ERROR" =? t
        |> WithComment "The PlacesService request could not be processed due to a server error. The request may succeed if you try again."

        "ZERO_RESULTS" =? t
        |> WithComment "No result was found for this request."
    ]

let PredictionSubstring =
    Config "google.maps.places.PredictionSubstring"
    |+> Instance [
        "length" =? T<int>
        |> WithComment "The length of the substring."

        "offset" =? T<int>
        |> WithComment "The offset to the substring's start within the description string."
    ]

let PredictionTerm =
    Config "google.maps.places.PredictionTerm"
    |+> Instance [
        "offset" =? T<int>
        |> WithComment "The offset, in unicode characters, of the start of this term in the description of the place."

        "value" =? T<string>
        |> WithComment "The value of this term, e.g. \"Taco Bell\"."
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
    ]

let QueryAutocompletionRequest =
    Config "google.maps.places.QueryAutocompletionRequest"
    |+> Instance [
        "bounds" =@ LatLngBounds
        |> WithComment "Bounds for prediction biasing. Predictions will be biased towards, but not restricted to, the given bounds. Both location and radius will be ignored if bounds is set."

        "input" =@ T<string>
        |> WithComment "The user entered input string."

        "location" =@ LatLng
        |> WithComment "Location for prediction biasing. Predictions will be biased towards the given location and radius. Alternatively, bounds can be used."

        "offset" =@ T<int>
        |> WithComment "The character position in the input term at which the service uses text for predictions (the position of the cursor in the input field)."

        "radius" =@ T<float>
        |> WithComment "The radius of the area used for prediction biasing. The radius is specified in meters, and must always be accompanied by a location property. Alternatively, bounds can be used."
    ]

let RadarSearchRequest =
    Config "google.maps.places.RadarSearchRequest"
    |+> Instance [
        "bounds" =@ LatLngBounds
        |> WithComment "Bounds used to bias results when searching for Places (optional). Both location and radius will be ignored if bounds is set. Results will not be restricted to those inside these bounds; but, results inside it will rank higher."

        "keyword" =@ T<string>
        |> WithComment "A term to be matched against all available fields, including but not limited to name, type, and address, as well as customer reviews and other third-party content."

        "location" =@ LatLng
        |> WithComment "The center of the area used to bias results when searching for Places."

        "name" =@ T<string>
        |> WithComment "Restricts results to Places that include this text in the name."

        "radius" =@ T<float>
        |> WithComment "The radius of the area used to bias results when searching for Places, in meters."

        "types" =@ T<string[]>
        |> WithComment "Restricts the Place search results to Places with a type matching at least one of the specified types in this array. Valid types are given here."
    ]

let TextSearchRequest =
    Config "google.maps.places.TextSearchRequest"
    |+> Instance [
        "bounds" =@ LatLngBounds
        |> WithComment "Bounds used to bias results when searching for Places (optional). Both location and radius will be ignored if bounds is set. Results will not be restricted to those inside these bounds; but, results inside it will rank higher."

        "location" =@ LatLng
        |> WithComment "The center of the area used to bias results when searching for Places."

        "query" =@ T<string>
        |> WithComment "The request's query term. e.g. the name of a place ('Eiffel Tower'), a category followed by the name of a location ('pizza in New York'), or the name of a place followed by a location disambiguator ('Starbucks in Sydney')."

        "radius" =@ T<float>
        |> WithComment "The radius of the area used to bias results when searching for Places, in meters."

        "types" =@ T<string[]>
        |> WithComment "Restricts the Place search results to Places with a type matching at least one of the specified types in this array. Valid types are given here."
    ]

let PlacesService =
    Class "google.maps.places.PlacesService"
    |+> Static [Constructor T<Element>?AttrContainer]
    |+> Instance [
        "getDetails" => PlaceDetailsRequest * (PlaceResult * PlacesServiceStatus ^-> T<unit>) ^-> T<unit>
        |> WithComment "Retrieves details about the Place identified by the given reference."

        "nearbySearch" => PlaceSearchRequest * (Type.ArrayOf PlaceResult * PlacesServiceStatus * PlaceSearchPagination ^-> T<unit>) ^-> T<unit>
        |> WithComment "Retrieves a list of Places in a given area. The PlaceResults passed to the callback are stripped-down versions of a full PlaceResult. A more detailed PlaceResult for each Place can be obtained by sending a Place Details request with the desired Place's reference value."

        "radarSearch" => RadarSearchRequest * (Type.ArrayOf PlaceResult * PlacesServiceStatus ^-> T<unit>) ^-> T<unit>
        |> WithComment "Similar to the nearbySearch function, with the following differences: the search response will include up to 200 Places, identified only by their geographic coordinates and Place reference."

        "textSearch" => TextSearchRequest * (Type.ArrayOf PlaceResult * PlacesServiceStatus ^-> T<unit>) ^-> T<unit>
        |> WithComment "Similar to the nearbySearch function, with the following differences: it retrieves a list of Places based on the query attribute in the given request object; bounds or location + radius parameters are optional; and the region, when provided, will not restrict the results to places inside the area, only bias the response towards results near it."
    ]

let SearchBoxOptions =
    Config "google.maps.places.SearchBoxOptions"
    |+> Instance [
        "bounds" =@ LatLngBounds
        |> WithComment "The area towards which to bias query predictions. Predictions are biased towards, but not restricted to, queries targeting these bounds."
    ]

let SearchBox =
    Class "google.maps.places.SearchBox"
    |+> Static [Constructor (T<Element>?InputField * !?SearchBoxOptions)]
    |+> Instance [
        "getBounds" => T<unit> ^-> LatLngBounds
        |> WithComment "Returns the bounds to which query predictions are biased."

        "getPlaces" => T<unit> ^-> Type.ArrayOf PlaceResult
        |> WithComment "Returns the query selected by the user, or null if no places have been found yet, to be used with places_changed event."

        "setBounds" => LatLngBounds ^-> T<unit>
        |> WithComment "Sets the region to use for biasing query predictions. Results will only be biased towards this area and not be completely restricted to it."
    ]

let AutocompleteOptions =
    Config "google.maps.places.AutocompleteOptions"
    |+> Instance [
        "bounds" =@ LatLngBounds
        |> WithComment "The area in which to search for places. Results are biased towards, but not restricted to, places contained within these bounds."

        "componentRestrictions" =@ ComponentRestrictions
        |> WithComment "The component restrictions. Component restrictions are used to restrict predictions to only those within the parent component. E.g., the country."

        "types" =@ T<string[]>
        |> WithComment "The types of predictions to be returned. Four types are supported: 'establishment' for businesses, 'geocode' for addresses, '(regions)' for administrative regions and '(cities)' for localities. If nothing is specified, all types are returned."
    ]

let AutocompletePrediction =
    Config "google.maps.places.AutocompletePrediction"
    |+> Instance [
        "description" =? T<string>
        |> WithComment "This is the unformatted version of the query suggested by the Places service."

        "id" =? T<string>
        |> WithComment "A stable ID for this place, intended to be interoperable with those returned by the place search service."

        "matched_substring" =? Type.ArrayOf PredictionSubstring
        |> WithComment "A set of substrings in the place's description that match elements in the user's input, suitable for use in highlighting those substrings. Each substring is identified by an offset and a length, expressed in unicode characters."

        "reference" =? T<string>
        |> WithComment "A reference that can be used to retrieve details about this place using the place details service (see PlacesService.getDetails())."

        "terms" =? Type.ArrayOf PredictionTerm
        |> WithComment "Information about individual terms in the above description, from most to least specific. For example, \"Taco Bell\", \"Willitis\", and \"CA\"."

        "types" =? T<string[]>
        |> WithComment "An array of types that the prediction belongs to, for example 'establishment' or 'geocode'."
    ]

let Autocomplete =
    Class "google.maps.places.Autocomplete"
    |+> Static [Constructor (T<Element>?InputField * !?AutocompleteOptions)]
    |+> Instance [
        "getBounds" => T<unit> ^-> LatLngBounds
        |> WithComment "Returns the bounds to which predictions are biased."

        "getPlace" => T<unit> ^-> PlaceResult
        |> WithComment "Returns the details of the Place selected by user if the details were successfully retrieved. Otherwise returns a stub Place object, with the name property set to the current value of the input field."

        "setBounds" => LatLngBounds ^-> T<unit>
        |> WithComment "Sets the preferred area within which to return Place results. Results are biased towards, but not restricted to, this area."

        "setComponentsRestrictions" => ComponentRestrictions ^-> T<unit>
        |> WithComment "Sets the component restrictions. Component restrictions are used to restrict predictions to only those within the parent component. E.g., the country."

        "setTypes" => T<string[]> ^-> T<unit>
        |> WithComment "Sets the types of predictions to be returned. Supported types are 'establishment' for businesses and 'geocode' for addresses. If no type is specified, both types will be returned. The setTypes method accepts a single element array."
    ]

let AutocompletionRequest =
    Config "google.maps.places.AutocompletionRequest"
    |+> Instance [
        "bounds" =@ LatLngBounds
        |> WithComment "Bounds for prediction biasing. Predictions will be biased towards, but not restricted to, the given bounds. Both location and radius will be ignored if bounds is set."

        "componentRestrictions" =@ ComponentRestrictions
        |> WithComment "The component restrictions. Component restrictions are used to restrict predictions to only those within the parent component. E.g., the country."

        "input" =@ T<string>
        |> WithComment "The user entered input string."

        "location" =@ LatLng
        |> WithComment "Location for prediction biasing. Predictions will be biased towards the given location and radius. Alternatively, bounds can be used."

        "offset" =@ T<int>
        |> WithComment "The character position in the input term at which the service uses text for predictions (the position of the cursor in the input field)."

        "radius" =@ T<float>
        |> WithComment "The radius of the area used for prediction biasing. The radius is specified in meters, and must always be accompanied by a location property. Alternatively, bounds can be used."

        "types" =@ T<string[]>
        |> WithComment "The types of predictions to be returned. Four types are supported: 'establishment' for businesses, 'geocode' for addresses, '(regions)' for administrative regions and '(cities)' for localities. If nothing is specified, all types are returned."
    ]

let AutocompleteService =
    Class "google.maps.places.AutocompleteService"
    |+> Static [Constructor T<unit>]
    |+> Instance [
        "getPlacePredictions" => AutocompletionRequest * (Type.ArrayOf AutocompletePrediction * PlacesServiceStatus ^-> T<unit>) ^-> T<unit>
        |> WithComment "Retrieves place autocomplete predictions based on the supplied autocomplete request."

        "getQueryPredictions" => QueryAutocompletionRequest * (Type.ArrayOf QueryAutocompletionRequest * PlacesServiceStatus ^-> T<unit>) ^-> T<unit>
        |> WithComment "Retrieves query autocomplete predictions based on the supplied query autocomplete request."
    ]
