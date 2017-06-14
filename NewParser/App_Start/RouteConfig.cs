using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NewParser
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "NewsParse",
                url: "NewsParse/{action}/{*url}",
                defaults : new { controller = "NewsParser", action="Index", url = UrlParameter.Optional}
            );
            routes.MapRoute(
                name: "Forex",
                url: "Forex/{action}/{*url}",
                defaults: new { controller = "Forex", action = "Index", url = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Arabbit",
                url: "Arabbit/{action}/{*url}",
                defaults: new { controller = "Arabbit", action = "Index", url = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "CryptoCoin",
                url: "CryptoCoin/{action}/{*url}",
                defaults: new { controller = "CryptoCoin", action = "Index", url = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Alarabiya",
                url: "Alarabiya/{action}/{*url}",
                defaults: new { controller = "Alarabiya", action = "Index", url = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Cnbc",
                url: "Cnbc/{action}/{*url}",
                defaults: new { controller = "Cnbc", action = "Index", url = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Cnbcarabia",
                url: "Cnbcarabia/{action}/{*url}",
                defaults: new { controller = "Cnbcarabia", action = "Index", url = UrlParameter.Optional }
            );
        }
    }
}
