using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using System.Linq.Expressions;
using System.Web;
using Tropical.Infrastructure.Model;
using System.IO;
using System.Web.UI;
using System.ComponentModel.DataAnnotations;

namespace Tropical.Infrastructure.Helper
{
    public static partial class CustomHtmlHelpers
    {
        public static MvcHtmlString CustomViewTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            string modelID = GetFormFieldID(htmlHelper, expression);
            string modelValue = GetValue(htmlHelper, expression);    

            String htmlToRender = GetViewHtml(htmlHelper.ViewContext, "~/Views/Shared/CustomViewTextArea.cshtml");

            htmlToRender = htmlToRender.Replace("{ID}", modelID);
            htmlToRender = htmlToRender.Replace("{VALUE}", modelValue);

            DisplayAttribute displayAttribute = GetDisplayAttribute(expression);
            htmlToRender = ReplaceDisplayAttribute(htmlToRender, displayAttribute);

            return MvcHtmlString.Create(htmlToRender);
        }
    }
}
