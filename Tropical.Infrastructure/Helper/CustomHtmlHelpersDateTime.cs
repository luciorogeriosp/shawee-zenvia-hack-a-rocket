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
        public static MvcHtmlString CustomViewDateTimeFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            string modelID = GetFormFieldID(htmlHelper, expression);
            string modelValue = GetValue(htmlHelper, expression);
            
            if (modelValue != "")
            {
                if (modelValue == "01/01/0001 00:00:00")
                    modelValue = "";
                else
                    modelValue = modelValue.Substring(1, 16);
            }

            String htmlToRender = GetViewHtml(htmlHelper.ViewContext, "~/Views/Shared/CustomViewDateTime.cshtml");
            htmlToRender = ReplaceValue(htmlToRender, "ID", modelID);
            htmlToRender = ReplaceValue(htmlToRender, "VALUE", modelValue);

            DisplayAttribute displayAttribute = GetDisplayAttribute(expression);
            htmlToRender = ReplaceDisplayAttribute(htmlToRender, displayAttribute);

            return MvcHtmlString.Create(htmlToRender);
        }       
    }
}
