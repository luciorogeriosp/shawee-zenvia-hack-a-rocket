using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Helpers
{
    public class UserInterface
    {
        public static bool isTablet()
        {
            var Request = System.Web.HttpContext.Current.Request;

            int width = Request.Browser.ScreenPixelsWidth;
            int height = Request.Browser.ScreenPixelsHeight;

            return (width >= 480 && width <= 1024 && height >= 480 && height <= 1366);
        }
    }
}