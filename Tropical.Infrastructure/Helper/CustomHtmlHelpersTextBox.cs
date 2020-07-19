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
        public static MvcHtmlString CustomViewTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            string modelID = GetFormFieldID(htmlHelper, expression);
            string modelValue = GetValue(htmlHelper, expression);                        


            String htmlToRender = GetViewHtml(htmlHelper.ViewContext, "~/Views/Shared/CustomViewTextBox.cshtml");
            htmlToRender = ReplaceValue(htmlToRender, "ID", modelID);
            htmlToRender = ReplaceValue(htmlToRender, "VALUE", modelValue);

            DisplayAttribute displayAttribute = GetDisplayAttribute(expression);
            htmlToRender = ReplaceDisplayAttribute(htmlToRender, displayAttribute);

            return MvcHtmlString.Create(htmlToRender);
        }

        public static MvcHtmlString CustomViewTextBoxLoginFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            string modelID = GetFormFieldID(htmlHelper, expression);
            string modelValue = GetValue(htmlHelper, expression);


            String htmlToRender = GetViewHtml(htmlHelper.ViewContext, "~/Views/Shared/CustomViewTextBoxLogin.cshtml");
            htmlToRender = ReplaceValue(htmlToRender, "ID", modelID);
            htmlToRender = ReplaceValue(htmlToRender, "VALUE", modelValue);

            DisplayAttribute displayAttribute = GetDisplayAttribute(expression);
            htmlToRender = ReplaceDisplayAttribute(htmlToRender, displayAttribute);

            return MvcHtmlString.Create(htmlToRender);
        }

        public static MvcHtmlString CustomViewPasswordFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            string modelID = GetFormFieldID(htmlHelper, expression);
            string modelValue = GetValue(htmlHelper, expression);

            String htmlToRender = GetViewHtml(htmlHelper.ViewContext, "~/Views/Shared/CustomViewPassword.cshtml");
            htmlToRender = ReplaceValue(htmlToRender, "ID", modelID);
            htmlToRender = ReplaceValue(htmlToRender, "VALUE", modelValue);

            DisplayAttribute displayAttribute = GetDisplayAttribute(expression);
            htmlToRender = ReplaceDisplayAttribute(htmlToRender, displayAttribute);

            return MvcHtmlString.Create(htmlToRender);
        }

        public static MvcHtmlString CustomViewEmailFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            string modelID = GetFormFieldID(htmlHelper, expression);
            string modelValue = GetValue(htmlHelper, expression);

            String htmlToRender = GetViewHtml(htmlHelper.ViewContext, "~/Views/Shared/CustomViewEmail.cshtml");
            htmlToRender = ReplaceValue(htmlToRender, "ID", modelID);
            htmlToRender = ReplaceValue(htmlToRender, "VALUE", modelValue);

            DisplayAttribute displayAttribute = GetDisplayAttribute(expression);
            htmlToRender = ReplaceDisplayAttribute(htmlToRender, displayAttribute);

            return MvcHtmlString.Create(htmlToRender);

        }

        public static MvcHtmlString CustomViewUrlFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            string modelID = GetFormFieldID(htmlHelper, expression);
            string modelValue = GetValue(htmlHelper, expression);            

            String htmlToRender = GetViewHtml(htmlHelper.ViewContext, "~/Views/Shared/CustomViewUrl.cshtml");
            htmlToRender = ReplaceValue(htmlToRender, "ID", modelID);
            htmlToRender = ReplaceValue(htmlToRender, "VALUE", modelValue);

            DisplayAttribute displayAttribute = GetDisplayAttribute(expression);
            htmlToRender = ReplaceDisplayAttribute(htmlToRender, displayAttribute);

            return MvcHtmlString.Create(htmlToRender);
        }

        public static MvcHtmlString CustomViewTextBoxCurrencyFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            string modelID = GetFormFieldID(htmlHelper, expression);
            string modelValue = GetValue(htmlHelper, expression);

            String htmlToRender = GetViewHtml(htmlHelper.ViewContext, "~/Views/Shared/CustomViewTextBoxCurrency.cshtml");
            htmlToRender = ReplaceValue(htmlToRender, "ID", modelID);
            htmlToRender = ReplaceValue(htmlToRender, "VALUE", modelValue);

            DisplayAttribute displayAttribute = GetDisplayAttribute(expression);
            htmlToRender = ReplaceDisplayAttribute(htmlToRender, displayAttribute);

            return MvcHtmlString.Create(htmlToRender);
        }
    }
}
