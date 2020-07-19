using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEnd.Util.API.Model
{
    public class CreateProductReturnModel
    {
        [JsonProperty(PropertyName = "product")]
        public ProductModel Product { get; set; }

        [JsonProperty(PropertyName = "mesangem")]
        public string Mensagem { get; set; }
    }
}