using FrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEnd.Util
{
    public class Configuracao
    {
        public static UsuarioClientePerfilModel PerfilSelecionadoLogado
        {
            get
            {
                return Tropical.Infrastructure.Util.SessionData.SessionReader("PerfilSelecionadoLogado") as UsuarioClientePerfilModel;
            }
        }

        public static Tropical.Infrastructure.Model.User UsuarioLogado
        {
            get
            {
                return Tropical.Infrastructure.Util.SessionData.SessionReader("UsuarioLogado") as Tropical.Infrastructure.Model.User;
            }
        }
    }
}