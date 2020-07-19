using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEnd.Util.API.Model
{
    public class CreateStoreReturnModel
    {
        [JsonProperty(PropertyName = "store")]
        public StoreModel Store { get; set; }

        [JsonProperty(PropertyName = "mesangem")]
        public string Mensagem { get; set; }
    }
}