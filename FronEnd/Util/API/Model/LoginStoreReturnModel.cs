using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEnd.Util.API.Model
{
    public class LoginStoreReturnModel
    {
        [JsonProperty(PropertyName = "store")]
        public StoreModel Store { get; set; }
    }
}