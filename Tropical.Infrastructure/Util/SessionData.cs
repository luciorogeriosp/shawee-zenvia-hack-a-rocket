using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Tropical.Infrastructure.Util
{
    public class SessionData
    {
        public static object SessionReader(string SessionKey)
        {
            object ret = null;

            if (HttpContext.Current.Session[HttpContext.Current.Session.SessionID + "_" + SessionKey] != null)
                ret = HttpContext.Current.Session[HttpContext.Current.Session.SessionID + "_" + SessionKey];

            return ret;
        }

        public static void SessionWriter(string SessionKey, object SessionValue)
        {
            HttpContext.Current.Session[HttpContext.Current.Session.SessionID + "_" + SessionKey] = SessionValue;
        }

        public static void SessionDestroy(string SessionKey)
        {
            HttpContext.Current.Session[HttpContext.Current.Session.SessionID + "_" + SessionKey] = null;
        }
    }
}
