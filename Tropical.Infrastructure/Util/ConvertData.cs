using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Tropical.Infrastructure.Util
{
    public class ConvertData
    {
        public static DataTable ListToDataTable<T>(List<T> list)
        {
            object caption;
            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn();
            DataRow dr;
            PropertyInfo[] fieldInfo = typeof(T).GetProperties();
            foreach (PropertyInfo col in fieldInfo)
            {
                caption = GetAttributeValue(typeof(T), col.Name, typeof(DisplayAttribute), "Name");
                if (caption != null)
                {
                    dc = new DataColumn();

                    dc.ColumnName = col.Name;                    

                    dc.Caption = caption.ToString();

                    dt.Columns.Add(dc);
                }

            }

            foreach (var item in list)
            {
                dr = dt.NewRow();
                foreach (DataColumn dcFinal in dt.Columns)
                {
                    dr[dcFinal.ColumnName] = item.GetType().GetProperty(dcFinal.ColumnName).GetValue(item, null);
                }
                dt.Rows.Add(dr);
            }

            return dt;
        }

        public static object GetAttributeValue(Type objectType, string propertyName, Type attributeType, string attributePropertyName)
        {
            var propertyInfo = objectType.GetProperty(propertyName);
            if (propertyInfo != null)
            {
                if (Attribute.IsDefined(propertyInfo, attributeType))
                {
                    var attributeInstance = Attribute.GetCustomAttribute(propertyInfo, attributeType);
                    if (attributeInstance != null)
                    {
                        foreach (PropertyInfo info in attributeType.GetProperties())
                        {
                            if (info.CanRead &&
                              String.Compare(info.Name, attributePropertyName,
                              StringComparison.InvariantCultureIgnoreCase) == 0)
                            {
                                return info.GetValue(attributeInstance, null);
                            }
                        }
                    }
                }
            }
            return null;
        }
    }
}
