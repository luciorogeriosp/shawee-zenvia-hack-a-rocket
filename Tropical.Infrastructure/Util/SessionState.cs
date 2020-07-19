using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tropical.Infrastructure.Model;
using System.Web;

namespace Tropical.Infrastructure.Util
{
    public class SessionState
    {

        public static void GravarUsuarioLogado(User usuario)
        {
            try
            {                
                HttpContext.Current.Session["UsuarioLogado" + HttpContext.Current.Session.SessionID] = usuario;
            }
            catch (Exception)
            {
                HttpContext.Current.Session["UsuarioLogado" + HttpContext.Current.Session.SessionID] = null;
            }
        }

        public static User ObterUsuarioLogado()
        {
            try
            {
                if (HttpContext.Current.Session["UsuarioLogado" + HttpContext.Current.Session.SessionID] != null)
                    return (User)HttpContext.Current.Session["UsuarioLogado" + HttpContext.Current.Session.SessionID];
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static void GravarUsuarioPesquisaLogado(User usuario)
        {
            try
            {
                HttpContext.Current.Session["UsuarioPesquisaLogado" + HttpContext.Current.Session.SessionID] = usuario;
            }
            catch (Exception)
            {
                HttpContext.Current.Session["UsuarioPesquisaLogado" + HttpContext.Current.Session.SessionID] = null;
            }
        }

        public static User ObterUsuarioPesquisaLogado()
        {
            try
            {
                if (HttpContext.Current.Session["UsuarioPesquisaLogado" + HttpContext.Current.Session.SessionID] != null)
                    return (User)HttpContext.Current.Session["UsuarioPesquisaLogado" + HttpContext.Current.Session.SessionID];
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        
    }
}
