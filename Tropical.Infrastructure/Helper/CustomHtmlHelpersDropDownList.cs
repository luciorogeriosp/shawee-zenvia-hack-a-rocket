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
        public static MvcHtmlString CustomViewDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList)
        {
            string modelID = GetFormFieldID(htmlHelper, expression);
            string modelValue = GetValue(htmlHelper, expression);                        


            String htmlToRender = GetViewHtml(htmlHelper.ViewContext, "~/Views/Shared/CustomViewDropDownList.cshtml");
            htmlToRender = ReplaceValue(htmlToRender, "ID", modelID);
            htmlToRender = ReplaceValue(htmlToRender, "VALUE", modelValue);

            StringBuilder sbItens = new StringBuilder();
            bool selected = false;
            if (selectList != null && selectList.Count() > 0)
            {
                foreach (var item in selectList)
                {
                    selected = (item.Value == modelValue);

                    sbItens.Append("<option value=\"" + item.Value + "\"" + (selected ? " selected" : "")  + ">" + item.Text + "</option>");
                }
            }
            htmlToRender = ReplaceValue(htmlToRender, "LISTA", sbItens.ToString());

            DisplayAttribute displayAttribute = GetDisplayAttribute(expression);
            htmlToRender = ReplaceDisplayAttribute(htmlToRender, displayAttribute);

            return MvcHtmlString.Create(htmlToRender);
        }       
    }
}
