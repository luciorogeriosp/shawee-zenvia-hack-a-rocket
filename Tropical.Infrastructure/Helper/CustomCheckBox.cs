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

namespace Tropical.Infrastructure.Helper
{
    public static class CustomCheckBox
    {
        #region CheckListFor

        public static MvcHtmlString CheckboxListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression,
                    List<MultiSelectListCustom> listOfValues,
                    String title = "", 
                    String classLeftColumn = "",
                    String classRightColumn = "",
                    Int32 lineHeight = 0,            
                    object htmlAttributesLabel = null, 
                    object htmlAttributesCheckBox = null, 
                    object htmlAttributesTextBox = null)
        {
            String modelName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(ExpressionHelper.GetExpressionText(expression));
            ModelMetadata metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            StringBuilder sb = new StringBuilder();
            TagBuilder tagCheckBox = null;
            TagBuilder tagLabel = null;
            TagBuilder tagText = null;
            TagBuilder tagHidden = null;
            int contador = 0;
            String idCheckBox = "";            

            if (listOfValues != null)
            {
                #region Impressão dos Títulos
                sb.Append("<div" + ((!String.IsNullOrEmpty(classLeftColumn)) ? " class=\"" + classLeftColumn + "\"" : "") + ">");
                if (!String.IsNullOrEmpty(title))
                {
                    sb.Append("<label>" + title + "</label>");
                }

                contador = 0;
                foreach (MultiSelectListCustom item in listOfValues)
                {
                    idCheckBox = string.Format("{0}_{1}", htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(ExpressionHelper.GetExpressionText(expression)), item.Value);

                    tagText = null;
                    if (item.DetailActive)
                    {
                        tagText = new TagBuilder("input");
                        tagText.MergeAttribute("type", "text");
                        tagText.MergeAttributes(new RouteValueDictionary(htmlAttributesTextBox));
                        if (!String.IsNullOrEmpty(item.DetailTitle))
                            tagText.MergeAttribute("placeholder", item.DetailTitle);
                        tagText.MergeAttribute("maxlength", "40");
                        tagText.MergeAttribute("name", String.Format(modelName + "[{0}].DetailText", contador));
                        tagText.MergeAttribute("value", @item.DetailText);
                        tagText.MergeAttribute("id", string.Format("text_{0}_{1}", htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(ExpressionHelper.GetExpressionText(expression)), item.Value));

                        if (System.Web.HttpContext.Current.Request.HttpMethod.ToString() == "POST" && item.DetailActive && item.Selected && String.IsNullOrEmpty(item.DetailText))
                            tagText.MergeAttribute("class", "input-validation-error" + " " + Util.ObterHtmlAttribute(htmlAttributesTextBox, "class"));

                        if (!item.Selected)
                            tagText.MergeAttribute("disabled", "disabled");
                    }
                    tagLabel = new TagBuilder("label");
                    tagLabel.MergeAttributes(new RouteValueDictionary(htmlAttributesLabel));
                    tagLabel.MergeAttribute("for", idCheckBox);
                    tagLabel.SetInnerText(item.Text);
                    sb.AppendFormat("{0}", tagLabel);

                    if (tagText != null)
                    {
                        tagLabel = new TagBuilder("label");
                        tagLabel.InnerHtml = tagText.ToString();
                        sb.AppendFormat("{0}", tagLabel);
                    }

                    contador++;
                }
                sb.Append("</div>");
                #endregion

                #region Impressão dos CheckBox
                sb.Append("<div" + ((!String.IsNullOrEmpty(classLeftColumn)) ? " class=\"" + classRightColumn + "\"" : "") + ">");
                if (!String.IsNullOrEmpty(title))
                {
                    sb.Append("<label>&nbsp;</label>");
                }

                contador = 0;
                foreach (MultiSelectListCustom item in listOfValues)
                {
                    idCheckBox = string.Format("{0}_{1}", htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(ExpressionHelper.GetExpressionText(expression)), item.Value);

                    tagHidden = new TagBuilder("input");
                    tagHidden.MergeAttribute("type", "hidden");
                    tagHidden.MergeAttribute("name", String.Format(modelName + "[{0}].Value", contador));
                    tagHidden.MergeAttribute("value", item.Value);
                    sb.AppendFormat("{0}", tagHidden);

                    tagHidden = new TagBuilder("input");
                    tagHidden.MergeAttribute("type", "hidden");
                    tagHidden.MergeAttribute("name", String.Format(modelName + "[{0}].Text", contador));
                    tagHidden.MergeAttribute("value", item.Text);
                    sb.AppendFormat("{0}", tagHidden);

                    tagHidden = new TagBuilder("input");
                    tagHidden.MergeAttribute("type", "hidden");
                    tagHidden.MergeAttribute("name", String.Format(modelName + "[{0}].DetailActive", contador));
                    tagHidden.MergeAttribute("value", item.DetailActive.ToString());
                    sb.AppendFormat("{0}", tagHidden);

                    tagHidden = new TagBuilder("input");
                    tagHidden.MergeAttribute("type", "hidden");
                    tagHidden.MergeAttribute("name", String.Format(modelName + "[{0}].DetailTitle", contador));
                    tagHidden.MergeAttribute("value", String.IsNullOrEmpty(item.DetailTitle) ? "" : item.DetailTitle.ToString());
                    sb.AppendFormat("{0}", tagHidden);                    

                    tagCheckBox = new TagBuilder("input");                    
                    tagCheckBox.MergeAttribute("type", "checkbox");
                    tagCheckBox.MergeAttribute("name", String.Format(modelName + "[{0}].Selected", contador));
                    tagCheckBox.MergeAttribute("value", "true");
                    tagCheckBox.MergeAttribute("id", idCheckBox);
                    if (item.Selected)
                    {                        
                        tagCheckBox.MergeAttribute("checked", "checked");                        
                    }

                    if (item.DetailActive)
                    {
                        String idText = string.Format("text_{0}_{1}", htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(ExpressionHelper.GetExpressionText(expression)), item.Value);
                        tagCheckBox.MergeAttribute("onclick", "if (this.checked) {this.form." + idText + ".disabled = \"\"; this.form." + idText + ".focus(); } else { this.form." + idText + ".disabled = \"disabled\"; this.form." + idText + ".value = ''; }; this.form." + idText + ".classList.remove(\"input-validation-error\");" + Util.ObterHtmlAttribute(htmlAttributesCheckBox, "onclick"));
                    }

                    tagCheckBox.MergeAttributes(new RouteValueDictionary(htmlAttributesCheckBox));
                    sb.AppendFormat("<label>{0}</label>", tagCheckBox);

                    if (item.DetailActive)
                    {
                        tagLabel = new TagBuilder("label");
                        if (lineHeight != 0)
                            tagLabel.MergeAttribute("style", "height:" + lineHeight + "px;");
                        tagLabel.InnerHtml = "&nbsp;";
                        sb.AppendFormat("{0}", tagLabel);
                    }

                    contador++;
                }
                sb.Append("</div>");

                #endregion
            }
            return MvcHtmlString.Create(sb.ToString());
        }
        #endregion

        /*
        #region CheckboxColumnsFor

        public static MvcHtmlString CheckboxColumnsFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression,
                    List<MultiSelectListCustom> listOfValues,
                    String title = "",
                    String classLeftColumn = "",
                    String classRightColumn = "",
                    Int32 lineHeight = 0,
                    object htmlAttributesLabel = null,
                    object htmlAttributesCheckBox = null,
                    object htmlAttributesTextBox = null)
        {

            ModelMetadata metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            StringBuilder sb = new StringBuilder();
            TagBuilder tagCheckBox = null;
            TagBuilder tagLabel = null;
            TagBuilder tagText = null;

            if (listOfValues != null)
            {
                #region Impressão dos Títulos
                sb.Append("<div" + ((!String.IsNullOrEmpty(classLeftColumn)) ? " class=\"" + classLeftColumn + "\"" : "") + ">");
                if (!String.IsNullOrEmpty(title))
                {
                    sb.Append("<label>" + title + "</label>");
                }

                foreach (MultiSelectListCustom item in listOfValues)
                {
                    tagText = null;
                    if (item.DetailActive)
                    {
                        tagText = new TagBuilder("input");
                        tagText.MergeAttribute("type", "text");
                        String idText = string.Format("text_{0}_{1}", htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(ExpressionHelper.GetExpressionText(expression)), item.Value);
                        String nameText = idText;
                        tagText.MergeAttributes(new RouteValueDictionary(htmlAttributesTextBox));
                        tagText.MergeAttribute("name", nameText);
                        tagText.MergeAttribute("value", @item.DetailText);
                        tagText.MergeAttribute("id", idText);
                        if (!item.Selected)
                            tagText.MergeAttribute("disabled", "disabled");
                    }
                    tagLabel = new TagBuilder("label");
                    tagLabel.MergeAttributes(new RouteValueDictionary(htmlAttributesLabel));
                    tagLabel.SetInnerText(item.Text);
                    sb.AppendFormat("{0}", tagLabel);

                    if (tagText != null)
                    {
                        tagLabel = new TagBuilder("label");
                        tagLabel.InnerHtml = tagText.ToString();
                        sb.AppendFormat("{0}", tagLabel);
                    }
                }
                sb.Append("</div>");
                #endregion

                #region Impressão dos CheckBox
                sb.Append("<div" + ((!String.IsNullOrEmpty(classLeftColumn)) ? " class=\"" + classRightColumn + "\"" : "") + ">");
                if (!String.IsNullOrEmpty(title))
                {
                    sb.Append("<label>&nbsp;</label>");
                }
                foreach (MultiSelectListCustom item in listOfValues)
                {
                    String id = string.Format("{0}_{1}", htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(ExpressionHelper.GetExpressionText(expression)), item.Value);
                    String name = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(ExpressionHelper.GetExpressionText(expression));

                    tagCheckBox = new TagBuilder("input");
                    tagCheckBox.MergeAttribute("type", "checkbox");
                    tagCheckBox.MergeAttribute("name", name);
                    tagCheckBox.MergeAttribute("value", item.Value);
                    tagCheckBox.MergeAttribute("id", id);
                    if (item.Selected)
                    {
                        tagCheckBox.MergeAttribute("checked", "checked");
                    }

                    if (item.DetailActive)
                    {
                        String idText = string.Format("text_{0}_{1}", htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(ExpressionHelper.GetExpressionText(expression)), item.Value);
                        tagCheckBox.MergeAttribute("onclick", "if (this.checked) {this.form." + idText + ".disabled = \"\"; this.form." + idText + ".focus(); } else { this.form." + idText + ".disabled = \"disabled\"; this.form." + idText + ".value = ''; }" + Util.ObterHtmlAttribute(htmlAttributesCheckBox, "onclick"));
                    }

                    tagCheckBox.MergeAttributes(new RouteValueDictionary(htmlAttributesCheckBox));
                    sb.AppendFormat("<label>{0}</label>", tagCheckBox);

                    if (item.DetailActive)
                    {
                        tagLabel = new TagBuilder("label");
                        if (lineHeight != 0)
                            tagLabel.MergeAttribute("style", "height:" + lineHeight + "px;");
                        tagLabel.InnerHtml = "&nbsp;";
                        sb.AppendFormat("{0}", tagLabel);
                    }
                }
                sb.Append("</div>");

                #endregion
            }
            return MvcHtmlString.Create(sb.ToString());
        }
        #endregion
         */ 
    }
}
