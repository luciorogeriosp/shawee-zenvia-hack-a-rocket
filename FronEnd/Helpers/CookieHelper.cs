using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tropical.Infrastructure.Util;

namespace FrontEnd.Helpers
{
    public class CookieHelper
    {
        public static void AddCookie(string name, object value)
        {
            HttpCookie cookie = new HttpCookie(name);
            TimeSpan tsMinute = new TimeSpan(0, 0, 20, 0);
            cookie.Expires = DateTime.Now + tsMinute;

            string objectSerialized = value.XmlSerializeToString();
            string objectCrypt = Cryptography.EncryptIt(objectSerialized);

            cookie.Value = objectCrypt;
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}