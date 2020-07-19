using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace FrontEnd.Helpers
{
    public class LegacyUrlRoute : RouteBase
    {
        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            const string status = "301 Moved Permanently";
            var request = httpContext.Request;
            var response = httpContext.Response;
            var legacyUrl = request.Url.ToString().ToLower();

            string baseUrl = httpContext.Request.Url.Scheme + "://" + httpContext.Request.Url.Authority + httpContext.Request.ApplicationPath.TrimEnd('/') + "/";

            if (legacyUrl.Contains("/css") || legacyUrl.Contains("/bundles"))
            {
                return null;
            }

            if (ConfigurationManager.AppSettings["SiteMaintenanceOn"] != null && ConfigurationManager.AppSettings["SiteMaintenanceOn"].ToString() == "1" && legacyUrl.Contains("/home/manutencao") == false)
            {
                response.Redirect(baseUrl + "home/manutencao");
                response.End();
            }

            return null;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext,
                    RouteValueDictionary values)
        {
            return null;
        }
    }
}