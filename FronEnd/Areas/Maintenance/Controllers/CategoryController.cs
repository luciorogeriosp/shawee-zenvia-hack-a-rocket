using FrontEnd.Areas.Maintenance.Models;
using FrontEnd.Util.API;
using System;
using System.Linq;
using System.Web.Mvc;
using Tropical.Infrastructure.Filters;
using Tropical.Infrastructure.Helper;
using FrontEnd.Util.API.Model;

namespace FrontEnd.Areas.Maintenance.Controllers
{
    [MvcAuthorize]
    public class CategoryController : Controller
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
                CategoryAPI api = new CategoryAPI();
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
            CategoryViewModel model = new CategoryViewModel();

            try
            {
                
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
        public ActionResult Form(CategoryViewModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Name))
                {
                    throw new Exception("Informe a Categoria.");
                }

                if (model.Id == 0)
                {
                    model.StoreId = Util.Configuracao.PerfilSelecionadoLogado.Cliente.Id;

                    CategoryAPI api = new CategoryAPI();
                    CategoryModel created = (api.Create(model.Cast<CategoryModel>()));

                    model.ReturnAttribute.Status = Helpers.Constantes.StatusRetorno.Sucesso;
                    model.ReturnAttribute.Mensagem = "Nova categoria cadastrada com sucesso!";
                    model.Id = created.Id;
                }
                else
                {
                    // Chamar PUT

                    model.ReturnAttribute.Status = Helpers.Constantes.StatusRetorno.Sucesso;
                    model.ReturnAttribute.Mensagem = "Categoria alterada com sucesso!";
                }
            }
            catch (Exception ex)
            {
                model.ReturnAttribute.Titulo = "Erro ao editar categoria";
                model.ReturnAttribute.Mensagem = ex.Message;
                model.ReturnAttribute.Status = Helpers.Constantes.StatusRetorno.Erro;
            }

            return View(model);
        }
    }
}