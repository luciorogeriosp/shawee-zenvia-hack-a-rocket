using FrontEnd.Util.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEnd.Models
{
    public class FormLoginModel: StoreModel
    {
        public FormLoginModel()
        {
            ReturnAttribute = new ReturnModel();
        }

        public bool RememberMe { get; set; }

        public ReturnModel ReturnAttribute { get; set; }
    }
}