using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;    

namespace Tropical.Infrastructure.Helper
{
    public static class IEnumerableHelper
    {
        public static T FirstOrNullObject<T>(this IEnumerable<T> enumerable, Func<T, bool> func, T nullObject)
        {
            var val = enumerable.FirstOrDefault<T>(func);
            if (val == null)
            {
                val = nullObject;
            }

            return val;
        }


        public static T FirstOrNullObject<T>(this IEnumerable<T> enumerable, T nullObject)
        {
            var val = enumerable.FirstOrDefault<T>();
            if (val == null)
            {
                val = nullObject;
            }
            return val;
        }

        public static T FirstOrNew<T>(this IEnumerable<T> enumerable, int id) where T : class, new()
        {
            var val = enumerable.FirstOrDefault();
            if (val == null)
            {
                var constructorTypeSignature = new Type[] { typeof(Int32) };
                var constructorParameters = new object[] { id };
                val = (T) new T().GetType().GetConstructor(constructorTypeSignature).Invoke(constructorParameters);
            }
            return val;
        }

        public static T FirstOrNew<T>(this IEnumerable<T> enumerable) where T : class, new()
        {
            var val = enumerable.FirstOrDefault();
            if (val == null)
            {
                val = (T)new T();
            }
            return val;
        }

        public static T FirstOrNew<T>(this IEnumerable<T> enumerable, Func<T, bool> func, T newObject)
        {
            return enumerable.FirstOrNullObject<T>(func, newObject);
        }


        public static T FirstOrNew<T>(this IEnumerable<T> enumerable, T newObject)
        {
            return enumerable.FirstOrNullObject<T>(newObject);
        }


        public static T FirstOrDefault<T>(this IEnumerable<T> enumerable, Func<T, bool> func, T defaultObject)
        {
            return enumerable.FirstOrNullObject<T>(func, defaultObject);
        }


        public static T FirstOrDefault<T>(this IEnumerable<T> enumerable, T defaultObject)
        {
            return enumerable.FirstOrNullObject<T>(defaultObject);
        }


        public static T LastOrNullObject<T>(this IEnumerable<T> enumerable, Func<T, bool> func, T nullObject)
        {
            var val = enumerable.LastOrDefault<T>(func);
            if (val == null)
            {
                val = nullObject;
            }

            return val;
        }


        public static T LastOrNullObject<T>(this IEnumerable<T> enumerable, T nullObject)
        {
            var val = enumerable.LastOrDefault<T>();
            if (val == null)
            {
                val = nullObject;
            }
            return val;
        }


        public static T LastOrNew<T>(this IEnumerable<T> enumerable, Func<T, bool> func, T newObject)
        {
            return enumerable.LastOrNullObject<T>(func, newObject);
        }


        public static T LastOrNew<T>(this IEnumerable<T> enumerable, T newObject)
        {
            return enumerable.LastOrNullObject<T>(newObject);
        }


        public static T LastOrDefault<T>(this IEnumerable<T> enumerable, Func<T, bool> func, T defaultObject)
        {
            return enumerable.LastOrNullObject<T>(func, defaultObject);
        }


        public static T LastOrDefault<T>(this IEnumerable<T> enumerable, T defaultObject)
        {
            return enumerable.LastOrNullObject<T>(defaultObject);
        }        
 
    }
}
