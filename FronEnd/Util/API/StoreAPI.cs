using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Util.API
{
    public class StoreAPI : BaseAPI
    {
        private string apiPrefix = "store";

        public List<Model.StoreModel> GetAll()
        {
            List<Model.StoreModel> objReturn = null;

            string result = base.Get(apiPrefix);

            if (!string.IsNullOrEmpty(result))
            {
                objReturn = JsonConvert.DeserializeObject<List<Model.StoreModel>>(result);
            }

            return objReturn;
        }

        public Model.StoreModel GetById(int Id)
        {
            Model.StoreModel objReturn = null;

            string result = base.Get(apiPrefix);

            if (!string.IsNullOrEmpty(result))
            {
                var objList = JsonConvert.DeserializeObject<List<Model.StoreModel>>(result);
                objReturn = objList.
                    Where(w => w.Id == Id)
                    .FirstOrDefault();
            }

            return objReturn;
        }

        public Model.StoreModel GetByLogin(string Email, string Password)
        {
            Model.StoreModel objReturn = null;

            try
            {
                string jsonContent = "" + 
                "{" + 
                "  \"email\": \"" + Email + "\"," + 
                "  \"password\": \"" + Password + "\"" + 
                "}";

                string result = base.Post(apiPrefix + "/login", jsonContent);

                if (!string.IsNullOrEmpty(result))
                {
                    var objLogin = JsonConvert.DeserializeObject<Model.LoginStoreReturnModel>(result);
                    objReturn = objLogin.Store;
                }
            }
            catch (System.Exception ex)
            {

                throw;
            }


            return objReturn;
        }

        public Model.StoreModel Create(Model.StoreModel model)
        {
            Model.StoreModel objReturn = null;

            string json = JsonConvert.SerializeObject(model);

            string result = base.Post(apiPrefix, json);

            if (!string.IsNullOrEmpty(result))
            {
                var objCreate = JsonConvert.DeserializeObject<Model.CreateStoreReturnModel>(result);
                objReturn = objCreate.Store;
            }

            return objReturn;
        }
    }
}