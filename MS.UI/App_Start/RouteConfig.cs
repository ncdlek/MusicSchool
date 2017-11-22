using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MS.UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Program Add",
                url: "Program/Add",
                defaults: new { controller = "Program", action = "Add"}
            );

            routes.MapRoute(
                name: "Program",
                url: "Program/{date}",
                defaults: new { controller = "Program", action = "Index", date = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Program", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
