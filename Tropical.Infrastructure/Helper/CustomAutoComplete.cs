using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using System.Linq.Expressions;
using System.Web;
using Tropical.Infrastructure.Model;

namespace Tropical.Infrastructure.Helper
{
    public static class CustomAutoComplete
    {
        #region CheckListFor

        /// <summary>
        /// Cria uma ComboBox implementando a rotina de Auto-Complete
        /// </summary>
        /// <typeparam name="TModel">Model</typeparam>
        /// <typeparam name="TProperty">Property</typeparam>
        /// <param name="htmlHelper">Html Helper</param>
        /// <param name="expression">Nome do Campo ID na Model</param>
        /// <param name="name">Nome do Campo AutoComplete na Model</param>
        /// <param name="label">Descrição do campo</param>
        /// <param name="AllowNewValues">Permite Novos Valores</param>
        /// <param name="list">Lista de Valores</param>
        /// <param name="htmlAttributes">Atributos Html para Customização do Objeto</param>
        /// <returns></returns>
        public static MvcHtmlString DropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string name, string label, bool AllowNewValues, IEnumerable<SelectListItem> list, object htmlAttributes, bool disabled = false, string groupField = "", string groupValue = "")
        {
            String modelName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(ExpressionHelper.GetExpressionText(expression));
            ModelMetadata metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            StringBuilder sb = new StringBuilder();

            #region ComboBox
            TagBuilder dropdown = new TagBuilder("select");
            dropdown.Attributes.Add("name", modelName);
            dropdown.Attributes.Add("id", modelName);
            dropdown.Attributes.Add("autocompletename", name);
            if (AllowNewValues)
            {
                dropdown.Attributes.Add("acceptnew", "true");
            }
            if (disabled)
            {
                dropdown.Attributes.Add("autocompletedisabled", "true");
            }            

            bool AutoCompleteError = false;
            String AutoCompleteText = String.Empty;
            String AutoCompleteValue = String.Empty;

            if (System.Web.HttpContext.Current.Request.HttpMethod.ToString() == "POST" && (groupField == "" || (groupField != "" && System.Web.HttpContext.Current.Request[groupField].ToString() == groupValue)))
            {
                if (System.Web.HttpContext.Current.Request[name] != null)
                    AutoCompleteText = System.Web.HttpContext.Current.Request[name].ToString();

                if (System.Web.HttpContext.Current.Request[modelName] != null)
                    AutoCompleteValue = System.Web.HttpContext.Current.Request[modelName].ToString();                
                   
                Int32 valueParse = 0;
                if (AutoCompleteValue.Trim() != null)
                    Int32.TryParse(AutoCompleteValue.Trim().ToString(), out valueParse);

                if (valueParse == 0 && String.IsNullOrEmpty(AutoCompleteText))
                    AutoCompleteError = true;                   

                if (String.IsNullOrEmpty(AutoCompleteText) || AutoCompleteText.Length < 5)
                {
                    AutoCompleteError = true;
                }                
            }

            if (AutoCompleteError)
                dropdown.Attributes.Add("autocompleteerror", " input-validation-error");


            if (list != null)
            {
                StringBuilder options = new StringBuilder();
                bool selectedItem = false;
                foreach (SelectListItem item in list)
                {
                    selectedItem = false;
                    if (String.IsNullOrEmpty(AutoCompleteValue))
                    {
                        selectedItem = item.Selected;
                    }
                    else
                    {
                        selectedItem = (AutoCompleteValue.Equals(item.Value));
                    }

                    options.Append("<option value=\"" + item.Value + "\"" + (selectedItem ? " selected" : "") + ">" + item.Text + "</option>\n");
                }
                dropdown.InnerHtml = "\n" + options.ToString();
            }

            dropdown.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            sb.AppendFormat("{0}\n", dropdown);
            #endregion

            #region TextBox
            TagBuilder tagTextBox = new TagBuilder("input");
            tagTextBox.MergeAttribute("type", "text");
            tagTextBox.MergeAttribute("name", name);
            tagTextBox.MergeAttribute("id", name);
            if (metaData.IsRequired)
            {
                tagTextBox.MergeAttribute("data-val", "true");
                if (AllowNewValues) 
                {
                    tagTextBox.MergeAttribute("data-val-required", "O campo \"" + label + "\" é de preenchimento obrigatório ou não é uma entrada válida. Verifique se a quantidade de caracteres está entre 5 e 70.");
                }
                else
                {
                    tagTextBox.MergeAttribute("data-val-required", "O campo \"" + label + "\" é de preenchimento obrigatório.");
                }                                
            }
            tagTextBox.MergeAttribute("value", AutoCompleteText.Trim());
            tagTextBox.MergeAttribute("style", "display:none;");
            if (AutoCompleteError)
                tagTextBox.Attributes.Add("autocompleteerror", " input-validation-error");
            sb.AppendFormat("{0}\n", tagTextBox);
            #endregion

            #region Javascript
            TagBuilder tagScript = new TagBuilder("script");
            tagScript.InnerHtml = "" +
            "$(function () {\n" +
            "   $(\"#" + modelName + "\").combobox();\n" +
            "});\n";
            sb.AppendFormat("{0}\n", tagScript);
            #endregion

            #region Javascript
            TagBuilder tagTooltip = new TagBuilder("span");
            tagTooltip.Attributes.Add("id", modelName + "Comment");
            tagTooltip.Attributes.Add("name", modelName + "Comment");
            tagTooltip.Attributes.Add("class", "descri");
            tagTooltip.SetInnerText("digite o \"nome\" não encontrado na lista para gravar.");
            if (!AllowNewValues)
            {
                tagTooltip.Attributes.Add("style", "display:none;");
            }
            sb.AppendFormat("{0}\n", tagTooltip);
            #endregion

            return MvcHtmlString.Create(sb.ToString());
        }
        #endregion
    }
}
