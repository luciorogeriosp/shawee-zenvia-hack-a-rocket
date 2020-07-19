using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Util.API
{
    public class CategoryAPI : BaseAPI
    {
        private string apiPrefix = "category";

        public List<Model.CategoryModel> GetAll(int StoreId)
        {
            List<Model.CategoryModel> objReturn = null;

            string result = base.Get(apiPrefix);

            if (!string.IsNullOrEmpty(result))
            {
                objReturn = JsonConvert.DeserializeObject<List<Model.CategoryModel>>(result);
                if (objReturn.Any())
                {
                    objReturn = objReturn
                        .Where(w => w.StoreId == StoreId)
                        .ToList();
                }
            }

            return objReturn;
        }

        public Model.CategoryModel GetById(int Id)
        {
            Model.CategoryModel objReturn = null;

            string result = base.Get(apiPrefix);

            if (!string.IsNullOrEmpty(result))
            {
                var objList = JsonConvert.DeserializeObject<List<Model.CategoryModel>>(result);
                objReturn = objList.
                    Where(w => w.Id == Id)
                    .FirstOrDefault();
            }

            return objReturn;
        }

        public Model.CategoryModel Create(Model.CategoryModel model)
        {
            Model.CategoryModel objReturn = null;

            string json = JsonConvert.SerializeObject(model);

            string result = base.Post(apiPrefix, json);

            if (!string.IsNullOrEmpty(result))
            {
                var objCreate = JsonConvert.DeserializeObject<Model.CreateCategoryReturnModel>(result);
                objReturn = objCreate.Category;
            }

            return objReturn;
        }
    }
}