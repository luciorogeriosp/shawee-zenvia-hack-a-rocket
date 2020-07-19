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
    public static class CustomFileUpload
    {
        public static MvcHtmlString ImageUploadFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression,
            Int32 ModelID, 
            String ImagePath, 
            Int32 ImageSizeA,
            Int32 ImageSizeB,
            String UrlUploadAction,
            Int32 ThumbnailWidth = 0,
            Int32 ThumbnailHeight = 0,
            String ThumbnailPrefix = "",
            String ThumbnailSuffix = ""
            )
        {
            String ModelPropertyName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(ExpressionHelper.GetExpressionText(expression));
            String ModelPropertyValue = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).Model != null ? ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).Model.ToString() : "";
            ModelMetadata metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            StringBuilder sb = new StringBuilder();

            String DivToUpdate = ModelPropertyName + "Visualizacao";
            String HiddenToUpdate = ModelPropertyName + "Temporaria";

            sb.Append("<input type=\"hidden\" id=\"" + ModelPropertyName + "\" name=\"" + ModelPropertyName + "\" value=\"" + ModelPropertyValue + "\" />\n");
            sb.Append("<input type=\"hidden\" id=\"" + HiddenToUpdate + "\" name=\"" + HiddenToUpdate + "\" />\n");

            String ImagePreview = "";
            String ImagePathToReturn = "";
            if (!String.IsNullOrEmpty(ModelPropertyValue))
            {
                ImagePathToReturn = ImagePath + ThumbnailPrefix + ModelID + ThumbnailSuffix + "." + ModelPropertyValue + "?ts=" + (DateTime.Now.ToString("yyyymmddHHmmss"));
                ImagePreview = "background-image:url(" + ImagePathToReturn + ");";
                
            }

            

            UrlUploadAction += "?";
            UrlUploadAction += "DivToUpdate=" + DivToUpdate + "&";
            UrlUploadAction += "HiddenToUpdate=" + HiddenToUpdate + "&";
            UrlUploadAction += "Width=" + ThumbnailWidth + "&";
            UrlUploadAction += "Height=" + ThumbnailHeight + "&";
            UrlUploadAction += "SideASize=" + ImageSizeA + "&";
            UrlUploadAction += "SideBSize=" + ImageSizeB + "&";
            UrlUploadAction += "ImagePath=" + ImagePathToReturn + "&";
            sb.Append("<div style=\"width:" + ThumbnailWidth + "px; height:" + ThumbnailHeight + "px; border:solid #808080 1px;" + ImagePreview + "\" id=\"" + DivToUpdate + "\"></div>\n");
            sb.Append("<iframe src=\"" + UrlUploadAction + "\" width=\"150\" height=\"70\" style=\"display:block; border:none; scroll:none;\" scrolling=\"no\"></iframe>\n");
            
            return MvcHtmlString.Create(sb.ToString());
        }
    }
}
