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
    public class MvcAuthorizeSingle : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            HttpSessionStateBase Session = filterContext.HttpContext.Session;
            User user = null;
            if (Session["UsuarioLogado" + Session.SessionID] != null)
                user = (User)Session["UsuarioLogado" + Session.SessionID];

            if (user == null || String.IsNullOrEmpty(user.Id))
            {
                RouteValueDictionary routeValues = new RouteValueDictionary();
                routeValues["controller"] = "Conta";
                routeValues["action"] = "Index";

                filterContext.Controller.TempData["MsgErrorPag"] = "Seu Login expirou.";
                filterContext.Result = new RedirectToRouteResult(routeValues);
            }            
        }   
    }
}
