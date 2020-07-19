using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Routing;

namespace Tropical.Infrastructure.Helper
{
    public static class Util
    {
        /// <summary>
        /// Procura o atributo solicitado em uma coleção de Html Attributes
        /// </summary>
        /// <param name="htmlAttributes">Coleção Html Attributes</param>
        /// <param name="attribute">Nome do Parâmetro</param>
        /// <returns></returns>
        public static String ObterHtmlAttribute(object htmlAttributes, String attribute)
        {
            String ret = "";

            foreach (var item in new RouteValueDictionary(htmlAttributes))
            {
                if (item.Key == attribute)
                {
                    ret = item.Value.ToString();
                    break;
                }
            }

            return ret;
        }

        public static T Cast<T>(this Object myobj)
        {
            Type objectType = myobj.GetType();
            Type target = typeof(T);
            var x = Activator.CreateInstance(target, false);
            var z = from source in objectType.GetMembers().ToList()
                    where source.MemberType == MemberTypes.Property
                    select source;
            var d = from source in target.GetMembers().ToList()
                    where source.MemberType == MemberTypes.Property
                    select source;
            List<MemberInfo> members = d.Where(memberInfo => d.Select(c => c.Name)
               .ToList().Contains(memberInfo.Name)).ToList();
            PropertyInfo propertyInfo;
            object value;
            foreach (var memberInfo in members)
            {
                propertyInfo = typeof(T).GetProperty(memberInfo.Name);
                PropertyInfo propertyInfoSource = myobj.GetType().GetProperty(memberInfo.Name);
                if (propertyInfoSource != null)
                {
                    value = propertyInfoSource.GetValue(myobj, null);

                    propertyInfo.SetValue(x, value, null);
                }

            }
            return (T)x;
        }
    }
}
