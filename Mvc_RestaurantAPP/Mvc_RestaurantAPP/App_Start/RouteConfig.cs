﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Mvc_RestaurantAPP.Models;

namespace Mvc_RestaurantAPP
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            Database.SetInitializer(new SamplaData());

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
               
            );
        }
    }
}