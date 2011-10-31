using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using IntelliFactory.WebSharper.Core;

namespace IntelliFactory.WebSharper.Google.Maps.Dependencies
{
    public class GoogleMaps : IntelliFactory.WebSharper.Core.Resources.IResource
    {
        public void Render(IntelliFactory.WebSharper.Core.Resources.Context context,
            System.Web.UI.HtmlTextWriter html)
        {
            var url = ConfigurationManager.AppSettings["google.maps"];
            if (url == null) {
                url = "http://maps.google.com/maps/api/js";
            }
            var sensor = ConfigurationManager.AppSettings["google.maps.sensor"];
            if (sensor == null) {
                sensor = "false";
            }
            html.AddAttribute("type", "text/javascript");
            html.AddAttribute("src", url + "?sensor=" + Uri.EscapeDataString(sensor));
            html.RenderBeginTag("script");
            html.RenderEndTag();
        }
    }
}
