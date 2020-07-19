using FrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEnd.Areas.Maintenance.Models
{
    public class CategoryViewModel: Util.API.Model.CategoryModel
    {
        public CategoryViewModel()
        {
            ReturnAttribute = new ReturnModel();
        }

        public ReturnModel ReturnAttribute { get; set; }
    }
}