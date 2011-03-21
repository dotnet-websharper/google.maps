using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using Microsoft.FSharp.Collections;
using IntelliFactory.WebSharper;

namespace IntelliFactory.WebSharper.Google.Maps.Dependencies
{

    public class GoogleMaps : Resources.IResourceDefinition
    {
        public Resources.Resource Resource
        {
            get
            {
                return new Resources.Resource(
                    "google.maps",
                    Resources.Body.NewScriptBody(
                        Resources.Location.NewExternalLocation(
                            List<Resources.Part>(
                                Resources.Part.NewConfigurablePart("google.maps", "http://maps.google.com/maps/api/js"),
                                Resources.Part.NewFixedPart("?sensor="),
                                Resources.Part.NewConfigurablePart("google.maps.sensor", "false")
                            )
                        )
                    ),
                    FSharpList<Resources.Resource>.Empty
                );
            }
        }

        static FSharpList<T> List<T>(params T[] arguments)
        {
            return ListModule.OfArray(arguments);
        }
    }

}
