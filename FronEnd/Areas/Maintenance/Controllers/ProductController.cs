using FrontEnd.Areas.Maintenance.Models;
using FrontEnd.Util.API;
using System;
using System.Linq;
using System.Web.Mvc;
using Tropical.Infrastructure.Filters;
using Tropical.Infrastructure.Helper;
using FrontEnd.Util.API.Model;
using FrontEnd.Models;

namespace FrontEnd.Areas.Maintenance.Controllers
{
    [MvcAuthorize]
    public class ProductController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Lista()
        {
            try
            {
                ProductAPI api = new ProductAPI();
                var lista = api.GetAll(Util.Configuracao.PerfilSelecionadoLogado.Cliente.Id);

                return View(lista);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public ActionResult Excluir(int Id)
        {
            try
            {

                // DELETE

                return Json(new { success = true, msg = "Item apagado com sucesso." }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Form(int? Id)
        {
            ProductViewModel model = new ProductViewModel();

            try
            {
                if (Id.HasValue && Id.Value > 0)
                {
                    model.CategoryIdSelect = model.CategoryId.ToString();
                }

                this.loadCategory(model);
            }
            catch (Exception ex)
            {
                model.ReturnAttribute.Titulo = "Erro ao editar";
                model.ReturnAttribute.Mensagem = ex.Message;
                model.ReturnAttribute.Status = Helpers.Constantes.StatusRetorno.Erro;
            }

            return View(model);
        }


        [HttpPost]
        public ActionResult Form(ProductViewModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.CategoryIdSelect) || model.CategoryIdSelect == "0")
                {
                    throw new Exception("Selecione uma Categoria.");
                }


                if (string.IsNullOrEmpty(model.Name))
                {
                    throw new Exception("Informe o Produto.");
                }

                if (model.Price <= 0)
                {
                    throw new Exception("Informe a Preço.");
                }

                if (model.Id == 0)
                {
                    model.StoreId = Util.Configuracao.PerfilSelecionadoLogado.Cliente.Id;
                    model.CategoryId = Convert.ToInt32(model.CategoryIdSelect);

                    ProductAPI api = new ProductAPI();
                    ProductModel created = (api.Create(model.Cast<ProductModel>()));

                    model.ReturnAttribute.Status = Helpers.Constantes.StatusRetorno.Sucesso;
                    model.ReturnAttribute.Mensagem = "Novo produto cadastrada com sucesso!";
                    model.Id = created.Id;
                }
                else
                {
                    // Chamar PUT

                    model.ReturnAttribute.Status = Helpers.Constantes.StatusRetorno.Sucesso;
                    model.ReturnAttribute.Mensagem = "Oroduto alterado com sucesso!";
                }                
            }
            catch (Exception ex)
            {
                model.ReturnAttribute.Titulo = "Erro ao editar produto";
                model.ReturnAttribute.Mensagem = ex.Message;
                model.ReturnAttribute.Status = Helpers.Constantes.StatusRetorno.Erro;
            }
            finally
            {
                loadCategory(model);
            }


            return View(model);
        }

        private void loadCategory(ProductViewModel model)
        {
            // Carrega categorias
            CategoryAPI categoryAPI = new CategoryAPI();
            var lista = categoryAPI.GetAll(Util.Configuracao.PerfilSelecionadoLogado.Cliente.Id);
            model.CategoryList =
            new SelectList(
                lista
                .Select
                (
                    s => new ListItemModel()
                    {
                        Id = s.Id,
                        Text = s.Name.ToUpper()
                    }
                ), "Id", "Text", model.Id
            );
        }
    }
}