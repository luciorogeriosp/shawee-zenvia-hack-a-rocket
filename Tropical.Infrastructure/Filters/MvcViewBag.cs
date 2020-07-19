using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Tropical.Infrastructure.Filters
{
    public class MvcViewBag : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.SrvLayoutTopoPesquisa = filterContext.HttpContext.Application["SrvLayoutTopoPesquisa"];
            filterContext.Controller.ViewBag.SrvLayoutTopo = filterContext.HttpContext.Application["SrvLayoutTopo"];
            filterContext.Controller.ViewBag.SrvLayout = filterContext.HttpContext.Application["SrvLayout"];
            filterContext.Controller.ViewBag.SrvLayoutRodape = filterContext.HttpContext.Application["SrvLayoutRodape"];            
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            
        }
    }

}
