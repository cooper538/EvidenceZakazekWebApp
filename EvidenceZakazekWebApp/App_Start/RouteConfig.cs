using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EvidenceZakazekWebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // help by https://stackoverflow.com/a/13827068
            routes.MapRoute(name: "IndexRoute", url: "{controller}/{id}",
                defaults: new { controller = "Home", action = "Index" },
                constraints: new { id = @"^[0-9]+$" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
