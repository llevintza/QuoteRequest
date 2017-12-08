using System.Web.Mvc;
using System.Web.Routing;

namespace QuoteRequest
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("");
        }
    }
}
