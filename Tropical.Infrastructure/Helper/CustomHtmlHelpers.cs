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
        public static DisplayAttribute GetDisplayAttribute<TModel, TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            var member = expression.Body as MemberExpression;

            return member.Member
                .GetCustomAttributes(typeof(DisplayAttribute), false)
                .FirstOrDefault() as DisplayAttribute;
        }

        public static string GetValue<TModel, TProperty>(HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            MemberExpression body = (MemberExpression)expression.Body;
            string propertyName = body.Member.Name;
            TModel model = helper.ViewData.Model;
            object objModelValue = typeof(TModel).GetProperty(propertyName).GetValue(model, null);
            return objModelValue != null ? objModelValue.ToString() : "";
        }

        public static bool GetBooleanValue<TModel, TProperty>(HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            MemberExpression body = (MemberExpression)expression.Body;
            string propertyName = body.Member.Name;
            TModel model = helper.ViewData.Model;
            object objModelValue = typeof(TModel).GetProperty(propertyName).GetValue(model, null);
            return objModelValue != null ? Convert.ToBoolean(objModelValue) : false;
        }

        public static String GetViewHtml(ViewContext viewContext, String viewPath)
        {
            ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(viewContext, viewPath);

            StringBuilder sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            {
                using (HtmlTextWriter tw = new HtmlTextWriter(sw))
                {
                    viewResult.View.Render(viewContext, tw);
                }
            }

            return sb.ToString();
        }

        public static string GetFormFieldID<TModel, TProperty>(HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            return helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(ExpressionHelper.GetExpressionText(expression));
        }

        public static object GetFormFieldValue<TModel, TProperty>(HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            object objModelValue = GetValue(helper, expression);

            return objModelValue != null ? objModelValue.ToString() : ""; ;
        }

        public static string ReplaceValue(string htmlToRender, string markID, string markValue)
        {
            return htmlToRender.Replace("{" + markID + "}", markValue);
        }

        public static string ReplaceDisplayAttribute(string htmlToRender, DisplayAttribute displayAttribute)
        {
            string displayName = "";
            string displayDescription = "";
            string displayPrompt = "";

            if (displayAttribute != null)
            {
                displayName = displayAttribute.Name;
                displayPrompt = displayAttribute.Prompt;

                if (!String.IsNullOrEmpty(displayAttribute.Description))
                {
                    displayDescription = "<b class=\"tooltip tooltip-bottom-right\">" + displayAttribute.Description + "</b>";
                }
            }

            htmlToRender = ReplaceValue(htmlToRender, "LABELTEXT", displayName);
            htmlToRender = ReplaceValue(htmlToRender, "TOOLTIP", displayDescription);
            htmlToRender = ReplaceValue(htmlToRender, "PLACEHOLDER", displayPrompt);

            return htmlToRender;
        }
    }
}
