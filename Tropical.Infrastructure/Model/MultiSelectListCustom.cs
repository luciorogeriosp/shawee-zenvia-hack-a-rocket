using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Tropical.Infrastructure.Model
{
    public class MultiSelectListCustom 
    {
        public String Value { get; set; }
        public String Text { get; set; }
        public Boolean Selected { get; set; }
        public Boolean DetailActive { get; set; }
        public String DetailText { get; set; }
        public String DetailTitle { get; set; }
    }
}
