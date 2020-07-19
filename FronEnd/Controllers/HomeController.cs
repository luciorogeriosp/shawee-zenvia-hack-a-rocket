using FrontEnd.Models;
using FrontEnd.Util.API;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Xml.Linq;
using Tropical.Infrastructure.Filters;
using Tropical.Infrastructure.Helper;

namespace FrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public ActionResult Index()
        {
            // Rodar Robots somente no servidor.
            if (System.Web.HttpContext.Current.Request.Url.Host != "localhost")
            {

            }

            if (Util.Configuracao.UsuarioLogado != null)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            return View();
        }

        public ActionResult Manutencao()
        {
            return View();
        }

        public ActionResult RedirectToLogin()
        {
            return View();
        }

        public ActionResult FormLogin()
        {
            FormLoginModel model = new FormLoginModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FormLogin(FormLoginModel model)
        {
            try
            {
                string redirectToUrl = Url.Action("Index", "Dashboard");

                if (string.IsNullOrEmpty(model.Email))
                {
                    model.ReturnAttribute.AddError("Email", "Preencha o e-mail.");
                }
                else
                {
                    if (!Helpers.Validation.IsValidEmail(model.Email))
                    {
                        model.ReturnAttribute.AddError("Email", "Preencha um e-mail válido.");
                    }
                }

                if (string.IsNullOrEmpty(model.Password))
                {
                    model.ReturnAttribute.AddError("Senha", "Preencha a senha.");
                }

                if (ModelState.IsValid)
                {
                    string senha = Tropical.Infrastructure.Util.Cryptography.EncryptIt(model.Password);
                    bool AccessDenied = true;

                    StoreAPI storeAPI = new StoreAPI();
                    FormLoginModel userValidated = (storeAPI.GetByLogin(model.Email, senha)).Cast<FormLoginModel>();

                    if (userValidated != null)
                    {
                        Tropical.Infrastructure.Model.User user = new Tropical.Infrastructure.Model.User()
                        {
                            Id = userValidated.Id.ToString(),
                            Nome = userValidated.Name,
                            Email = userValidated.Email
                        };

                        UsuarioClientePerfilModel objUsuarioClientePerfilModel = new UsuarioClientePerfilModel();
                        objUsuarioClientePerfilModel.PerfilId = 1;
                        objUsuarioClientePerfilModel.Cliente = new ClienteModel();
                        objUsuarioClientePerfilModel.Cliente.Id = userValidated.Id;
                        objUsuarioClientePerfilModel.Cliente.Logotipo = Url.Content("~/img/avatars/male.png") ; // userValidated.Logo;
                        objUsuarioClientePerfilModel.Cliente.Nome = userValidated.Name;

                        Tropical.Infrastructure.Util.SessionData.SessionWriter("PerfilSelecionadoLogado", objUsuarioClientePerfilModel); 

                        Tropical.Infrastructure.Util.SessionData.SessionWriter("UsuarioLogado", user);
                        FormsAuthentication.SetAuthCookie(userValidated.Id.ToString(), model.RememberMe);
                        AccessDenied = false;
                    }

                    if (!AccessDenied)
                    {
                        model.ReturnAttribute.Status = Helpers.Constantes.StatusRetorno.Sucesso;
                        model.ReturnAttribute.Mensagem = "Acessando o Painel de Controle, aguarde...";
                        model.ReturnAttribute.RedirectUrl = redirectToUrl;
                    }
                    else
                    {
                        model.ReturnAttribute.Status = Helpers.Constantes.StatusRetorno.Erro;
                        model.ReturnAttribute.Mensagem = "Dados de acesso inválidos, verifique e tente novamente.";
                    }
                }
            }
            catch (ERPException exception)
            {
                model.ReturnAttribute.Status = Helpers.Constantes.StatusRetorno.Erro;
                model.ReturnAttribute.Mensagem = exception.Mensagem;
            }
            catch (Exception a)
            {
                model.ReturnAttribute.Status = Helpers.Constantes.StatusRetorno.Erro;
                model.ReturnAttribute.Mensagem = Helpers.Constantes.MsgNaoFoiPossivelCompletarOperacao;
            }

            return View(model);
        }

        public ActionResult FormSignIn()
        {
            FormLoginModel model = new FormLoginModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FormSignIn(FormLoginModel model)
        {
            try
            {
                string redirectToUrl = Url.Action("Index", "Dashboard");

                if (string.IsNullOrEmpty(model.Name))
                {
                    model.ReturnAttribute.AddError("Name", "Preencha o Nome ou Razão Social.");
                }

                if (string.IsNullOrEmpty(model.Document))
                {
                    model.ReturnAttribute.AddError("Document", "Preencha o CPF ou CNPJ.");
                }

                if (string.IsNullOrEmpty(model.Address))
                {
                    model.ReturnAttribute.AddError("Address", "Preencha o Endereço.");
                }

                if (string.IsNullOrEmpty(model.AddressNumber))
                {
                    model.ReturnAttribute.AddError("AddressNumber", "Número.");
                }

                if (string.IsNullOrEmpty(model.City))
                {
                    model.ReturnAttribute.AddError("City", "Preencha a Cidade.");
                }

                if (string.IsNullOrEmpty(model.State))
                {
                    model.ReturnAttribute.AddError("State", "Preencha o Estado.");
                }

                if (string.IsNullOrEmpty(model.PhoneNumber))
                {
                    model.ReturnAttribute.AddError("PhoneNumber", "Preencha o Whatsapp.");
                }

                if (string.IsNullOrEmpty(model.Email))
                {
                    model.ReturnAttribute.AddError("Email", "Preencha o e-mail.");
                }
                else
                {
                    if (!Helpers.Validation.IsValidEmail(model.Email))
                    {
                        model.ReturnAttribute.AddError("Email", "Preencha um e-mail válido.");
                    }
                }

                if (string.IsNullOrEmpty(model.Password))
                {
                    model.ReturnAttribute.AddError("Password", "Preencha a senha.");
                }

                if (ModelState.IsValid && model.ReturnAttribute.Errors.Any() == false)
                {
                    model.Password = Tropical.Infrastructure.Util.Cryptography.EncryptIt(model.Password);
                    model.Code = RandomString(6);

                    StoreAPI storeAPI = new StoreAPI();
                    FormLoginModel userCreated = (storeAPI.Create(model.Cast<Util.API.Model.StoreModel>())).Cast<FormLoginModel>();

                    if (userCreated != null)
                    {
                        Tropical.Infrastructure.Model.User user = new Tropical.Infrastructure.Model.User()
                        {
                            Id = userCreated.Id.ToString(),
                            Nome = userCreated.Name,
                            Email = userCreated.Email
                        };

                        UsuarioClientePerfilModel objUsuarioClientePerfilModel = new UsuarioClientePerfilModel();
                        objUsuarioClientePerfilModel.PerfilId = 1;
                        objUsuarioClientePerfilModel.Cliente = new ClienteModel();
                        objUsuarioClientePerfilModel.Cliente.Id = userCreated.Id;
                        objUsuarioClientePerfilModel.Cliente.Logotipo = Url.Content("~/img/avatars/male.png"); // userValidated.Logo;
                        objUsuarioClientePerfilModel.Cliente.Nome = userCreated.Name;

                        Tropical.Infrastructure.Util.SessionData.SessionWriter("PerfilSelecionadoLogado", objUsuarioClientePerfilModel);

                        Tropical.Infrastructure.Util.SessionData.SessionWriter("UsuarioLogado", user);
                        FormsAuthentication.SetAuthCookie(userCreated.Id.ToString(), model.RememberMe);

                        model.ReturnAttribute.Status = Helpers.Constantes.StatusRetorno.Sucesso;
                        model.ReturnAttribute.Mensagem = "Acessando o Painel de Controle, aguarde...";
                        model.ReturnAttribute.RedirectUrl = redirectToUrl;
                    }
                    else
                    {
                        model.ReturnAttribute.Status = Helpers.Constantes.StatusRetorno.Erro;
                        model.ReturnAttribute.Mensagem = "Ocorreu um problema o tentar realizar o cadastro.";
                    }
                }
            }
            catch (ERPException exception)
            {
                model.ReturnAttribute.Status = Helpers.Constantes.StatusRetorno.Erro;
                model.ReturnAttribute.Mensagem = exception.Mensagem;
            }
            catch (Exception a)
            {
                model.ReturnAttribute.Status = Helpers.Constantes.StatusRetorno.Erro;
                model.ReturnAttribute.Mensagem = Helpers.Constantes.MsgNaoFoiPossivelCompletarOperacao;
            }

            return View(model);
        }

        [MvcAuthorize]
        public new ActionResult Profile()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ProfileForm()
        {
            Tropical.Infrastructure.Model.User user = Tropical.Infrastructure.Util.SessionData.SessionReader("UsuarioLogado") as Tropical.Infrastructure.Model.User;

            StoreAPI storeAPI = new StoreAPI();
            FormLoginModel userAPI = storeAPI.GetById(Convert.ToInt32(user.Id)).Cast<FormLoginModel>();

            return View(userAPI);
        }

        [HttpPost]
        public ActionResult ProfileForm(FormLoginModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Name))
                {
                    throw new Exception("Preencha o Nome.");
                }

                if (string.IsNullOrEmpty(model.Email))
                {
                    throw new Exception("Preencha o E-mail.");
                }


                ViewBag.MsgSuccess = true;
                ViewBag.MsgTexto = "Dados atualizados com sucesso.";
            }
            catch (Exception ex)
            {
                ViewBag.MsgSuccess = false;
                ViewBag.MsgTexto = ex.Message;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult EsqueciMinhaSenha(string Email)
        {
            return Content("Sua senha foi enviada no e-mail informado.");
        }

        [MvcAuthorize]
        public ActionResult Logout()
        {
            Session.Abandon();

            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
        
    }
}