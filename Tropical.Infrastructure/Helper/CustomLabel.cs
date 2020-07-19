using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using System.Linq.Expressions;
using System.Web;

namespace Tropical.Infrastructure.Helper
{
    /// <summary>
    /// Html Helpers Customizados
    /// Autor: Lucio Rogerio
    /// Data: 2013-10-09
    /// </summary>
    public static class CustomLabel
    {
        #region Label Limit
        /// <summary>
        /// Cria uma label com um limite de caracteres, se ultrapassar o helper corta os caracteres na quantidade solicitada
        /// </summary>
        /// <param name="labelText">Texto</param>
        /// <param name="limit">Limite</param>
        /// <param name="htmlAttributes">Propriedades adicionais da label</param>
        /// <returns></returns>
        public static MvcHtmlString LabelLimit(string labelText, int limit = 0, IDictionary<string, object> htmlAttributes = null)
        {
            if (string.IsNullOrEmpty(labelText))
                return MvcHtmlString.Empty;

            TagBuilder tagBuilder = new TagBuilder("label");
            tagBuilder.MergeAttributes(htmlAttributes);
            tagBuilder.Attributes.Add("title", labelText);
            if (labelText.Length > limit && limit > 0)
            {
                if (labelText.Length > limit + 3)
                    labelText = labelText.Substring(0, limit) + "...";
            }
            tagBuilder.SetInnerText(labelText);

            return tagBuilder.ToMvcHtmlString(TagRenderMode.Normal);
        }
        #endregion        

        private static MvcHtmlString ToMvcHtmlString(this TagBuilder tagBuilder, TagRenderMode renderMode)
        {
            return new MvcHtmlString(tagBuilder.ToString(renderMode));
        }
    }
}
