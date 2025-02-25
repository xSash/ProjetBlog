﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CsharpSite {
    public class RouteConfig {
        public static void RegisterRoutes( RouteCollection routes ) {
            routes.IgnoreRoute( "{resource}.axd/{*pathInfo}" );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Index", action = "Index"}
            );
            routes.MapRoute(
                name: "Post",
                url: "{controller}/{action}/",
                defaults: new { controller = "Post", action = "Index", id = UrlParameter.Optional }
                );
            routes.MapRoute(
                name: "Unfollow",
                url: "Follow/Unfollow/{userIdToUnFollow}",
                defaults: new { controller = "Follow", action = "Unfollow", userIdToUnFollow = -1 }
            );
            routes.MapRoute(
                name: "Follow",
                url: "Follow/Follow/{userIdToFollow}",
                defaults: new { controller = "Follow", action = "Follow", userIdToFollow = -1 }
            );
            routes.MapRoute(
               name: "ViewProfile",
               url: "Profile/View/{userid}",
               defaults: new { controller = "Profile", action = "View", userid = -1 }
           );
            routes.MapRoute(
                name: "getchat",
                url: "Chat/GetChat/{targetUserId}",
                defaults: new { controller = "Chat", action = "GetChat", targetUserId = -1 }
            );

        }

    }
}
