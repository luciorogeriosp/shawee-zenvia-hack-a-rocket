using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Util.API
{
    public class ProductAPI : BaseAPI
    {
        private string apiPrefix = "product";

        public List<Model.ProductModel> GetAll(int StoreId)
        {
            List<Model.ProductModel> objReturn = null;

            string result = base.Get(apiPrefix);

            if (!string.IsNullOrEmpty(result))
            {
                objReturn = JsonConvert.DeserializeObject<List<Model.ProductModel>>(result);
                if (objReturn.Any())
                {
                    objReturn = objReturn
                        .Where(w => w.StoreId == StoreId)
                        .ToList();
                }
            }

            return objReturn;
        }

        public Model.ProductModel GetById(int Id)
        {
            Model.ProductModel objReturn = null;

            string result = base.Get(apiPrefix);

            if (!string.IsNullOrEmpty(result))
            {
                var objList = JsonConvert.DeserializeObject<List<Model.ProductModel>>(result);
                objReturn = objList.
                    Where(w => w.Id == Id)
                    .FirstOrDefault();
            }

            return objReturn;
        }

        public Model.ProductModel Create(Model.ProductModel model)
        {
            Model.ProductModel objReturn = null;

            string json = JsonConvert.SerializeObject(model);

            string result = base.Post(apiPrefix, json);

            if (!string.IsNullOrEmpty(result))
            {
                var objCreate = JsonConvert.DeserializeObject<Model.CreateProductReturnModel>(result);
                objReturn = objCreate.Product;
            }

            return objReturn;
        }
    }
}