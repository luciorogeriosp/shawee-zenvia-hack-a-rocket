using System;
using System.Web;
using System.Web.Mvc;
using FrameWork.Data.WebServiceProxy;
using Tropical.Infrastructure.ControleAcesso;

namespace Tropical.Infrastructure.Filters
{
    public class ControleAcessoAuthorize : AuthorizeAttribute
    {
        public String mensagemDeErro = "No Error";
        public String Sigla { get; set; }
        private Boolean temPermissao { get; set; }
        private Int32 idLdapDeCripto { get; set; }
        private String _lcpt { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            temPermissao = false;

            if (HttpContext.Current.Session["Curriculo_LCPT" + HttpContext.Current.Session.SessionID] == null)
                HttpContext.Current.Session["Curriculo_LCPT" + HttpContext.Current.Session.SessionID] = HttpContext.Current.Request["lcpt"];


            if (HttpContext.Current.Session["Curriculo_LCPT" + HttpContext.Current.Session.SessionID] != null)
            {
                _lcpt = HttpContext.Current.Session["Curriculo_LCPT" + HttpContext.Current.Session.SessionID].ToString();

                try
                {
                    idLdapDeCripto = Convert.ToInt32(new LDAPCripto().Decriptografar(_lcpt));

                    if (idLdapDeCripto != 0)
                    {
                        if (ChecaPermissao(idLdapDeCripto, Sigla, ref mensagemDeErro))
                            temPermissao = true;
                        else
                            temPermissao = false;
                    }
                }
                catch
                {
                    temPermissao = false;
                }
            }
            else
            {
                mensagemDeErro = "Tempo expirado. O Login deve ser efetuado!!!";
                HttpContext.Current.Session["Erro"] = mensagemDeErro;
                temPermissao = false;
            }
            return temPermissao;

            //return true;
        }

        internal Int32 GetIdLDAP()
        {
            try
            {
                if (HttpContext.Current.Session["Curriculo_LCPT" + HttpContext.Current.Session.SessionID] != null)
                {
                    _lcpt = HttpContext.Current.Session["Curriculo_LCPT" + HttpContext.Current.Session.SessionID].ToString();

                    return Convert.ToInt32(new LDAPCripto().Decriptografar(_lcpt));
                }
                else
                    return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private bool ChecaPermissao(int idLdapDeCripto, string funcSigla, ref string mensagemDeErro)
        {
            try
            {
                var controleAcessoWS = new ControleAcessoSoapClient();

                //String host = "172.28.10.210";
                //HttpContext.Current.Request.UserHostAddress
                if (controleAcessoWS.ChecaPermissaoPorFuncionalidade(idLdapDeCripto.ToString(), Sigla, HttpContext.Current.Request.UserHostAddress, ref mensagemDeErro))
                {
                    return true;
                }
                else
                {
                    HttpContext.Current.Session["Erro"] = mensagemDeErro;
                    return false;
                }
            }
            catch 
            {
                return false;
            }
        }
    }
}
