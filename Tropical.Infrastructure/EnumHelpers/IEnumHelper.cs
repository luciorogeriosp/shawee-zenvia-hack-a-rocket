using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.ComponentModel;
using System.Reflection;

namespace Tropical.Infrastructure.EnumHelpers
{
    public static class IEnumHelper
    {
        public static string GetEnumDescription<T>(this Int32 value)
        {
            string retGetEnumDescription = "";

            try
            {
                Type enumType = typeof(T);

                retGetEnumDescription = GetEnumDescription(Enum.ToObject(enumType, value));

            }
            catch { }

            return retGetEnumDescription;
        }

        public static string GetEnumDescription(object value)
        {
            string retGetEnumDescription = "";

            try
            {
                FieldInfo fi = value.GetType().GetField(value.ToString());

                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes != null && attributes.Length > 0)
                    retGetEnumDescription = attributes[0].Description;
                else
                    retGetEnumDescription = value.ToString();
            }
            catch { }

            return retGetEnumDescription;
        }
    }
}
