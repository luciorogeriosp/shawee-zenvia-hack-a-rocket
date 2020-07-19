using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Tropical.Infrastructure.CustomException;
using System.Web;

namespace Tropical.Infrastructure.Filters
{
    public class MvcException : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
        //    if (filterContext.Exception.GetType() == typeof(BusinessException))
        //    {
        //        filterContext.HttpContext.ClearError();
        //        filterContext.Controller.ViewBag.ExceptionMessage = filterContext.Exception.Message;
        //    }
        //    else if (filterContext.Exception.GetType() == typeof(DatabaseException))
        //    {
        //        filterContext.Controller.ViewBag.ExceptionMessage = "Ocorreu um problema de manipulação de dados.";
        //    }
        //    else
        //    {
        //        UrlHelper url = new UrlHelper(filterContext.RequestContext);
                                
        //        String action = url.Content("~/Shared/Error");
        //        //HttpContext.Current.Response.Redirect(action);
        //        filterContext.HttpContext.Response.Redirect(action, true);            }
            
        }
    }
}
