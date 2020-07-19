using FrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Areas.Maintenance.Models
{
    public class ProductViewModel : Util.API.Model.ProductModel
    {
        public ProductViewModel()
        {
            ReturnAttribute = new ReturnModel();
        }

        public ReturnModel ReturnAttribute { get; set; }

        public string CategoryIdSelect { get; set; }

        public SelectList CategoryList { get; set; }
    }
}