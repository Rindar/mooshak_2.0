using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace mooshak_2._0
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
<<<<<<< HEAD

            /* routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
             routes.MapRoute(
             name: "Login",    
             url: "{controller}/{action}/{id}",
             defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }

 );
  */
=======
>>>>>>> 83dc740362b569648d0bac84f2ab9d54b5dfd01e
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
