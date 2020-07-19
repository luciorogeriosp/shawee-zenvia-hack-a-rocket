using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEnd.Util.API.Model
{
    public class CategoryModel
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "storeId")]
        public int StoreId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}