using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;
using Tropical.Infrastructure.Model;
using System.Web.Routing;

namespace Tropical.Infrastructure.Filters
{
    public class MvcAuthorize : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            bool ajaxRequest = filterContext.HttpContext.Request.IsAjaxRequest();

            HttpSessionStateBase Session = filterContext.HttpContext.Session;
            User user = null;
            if (Tropical.Infrastructure.Util.SessionData.SessionReader("UsuarioLogado") != null)
                user = (User)Tropical.Infrastructure.Util.SessionData.SessionReader("UsuarioLogado");

            if (user == null || String.IsNullOrEmpty(user.Id))
            {
                RouteValueDictionary routeValues = new RouteValueDictionary();
                if (!ajaxRequest)
                {
                    routeValues["area"] = "";
                    routeValues["controller"] = "Home";
                    routeValues["action"] = "Index";
                    filterContext.Controller.TempData["MsgErrorPag"] = "Seu Login expirou.";
                }
                else
                {
                    routeValues["area"] = "";
                    routeValues["controller"] = "Home";
                    routeValues["action"] = "RedirectToLogin";
                    filterContext.Controller.TempData["MsgErrorPag"] = "Seu Login expirou."; //<br /><a href=\"/Admin/Login\" target=\"_top\">Clique aqui</a> e refaça seu Login.
                }                
                filterContext.Result = new RedirectToRouteResult(routeValues);
            }            
        }   
    }
}
