using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using Microsoft.FSharp.Collections;
using IntelliFactory.WebSharper;

namespace IntelliFactory.WebSharper.Google.Maps.Dependencies
{

    public class GoogleMaps : Resources.IResource
    {
        public IEnumerable<Resources.IResource> Dependencies
        {
            get { return new Resources.IResource[] { }; }
        }

        public string Id
        {
            get { return "google.maps"; }
        }

        public FSharpList<Markup.Node> Render(Resources.IResourceContext value)
        {
            string sensor = ConfigurationManager.AppSettings["google.maps.sensor"];
            if (sensor == null)
            {
                return Resources.RenderJavaScript("http://maps.google.com/maps/api/js?sensor=false");
            }
            return Resources.RenderJavaScript("http://maps.google.com/maps/api/js?sensor=" + sensor);
        }
    }
}
