using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEnd.Util.API.Model
{
    public class CreateCategoryReturnModel
    {
        [JsonProperty(PropertyName = "catgoria")]
        public CategoryModel Category { get; set; }

        [JsonProperty(PropertyName = "mesangem")]
        public string Mensagem { get; set; }
    }
}