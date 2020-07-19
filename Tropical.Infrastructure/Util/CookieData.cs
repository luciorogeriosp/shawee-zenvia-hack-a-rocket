using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace Tropical.Infrastructure.Util
{
    public class CookieData
    {
        public static void Write(String CookieName, object obj)
        {
            HttpCookie cookie = new HttpCookie(CookieName);
            
            if (obj != null)
                cookie.Value = obj.XmlSerializeToString();
            else
                cookie.Value = "";

            cookie.Expires = DateTime.MaxValue;

            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        [ValidateInput(false)]   
        public static string Read(String CookieName)
        {
            string ret = null;

            HttpCookie cookie = new HttpCookie(CookieName);
            cookie = HttpContext.Current.Request.Cookies[CookieName];

            // Read the cookie information and display it.
            if (cookie != null)
                ret = cookie.Value;

            return ret;
        }
    }
}
