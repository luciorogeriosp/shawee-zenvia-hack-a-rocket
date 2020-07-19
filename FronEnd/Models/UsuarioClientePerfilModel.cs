using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEnd.Models
{
    public class UsuarioClientePerfilModel
    {
        public bool Administrador { get; set; }
        public bool AdministradorSegmento { get; set; }
        public bool AdministradorRegiao { get; set; }
        public bool AdministradorOrganismo { get; set; }
        public bool SomenteLeitura { get; set; }

        public bool Oficial { get; set; }

        public bool Operacional { get; set; }

        public bool Atendimento { get; set; }

        public string TipoUsuarioNome { get; set; }
        public int TipoUsuarioId { get; set; }
        public int PerfilId { get; set; }
        public string PerfilNome { get; set; }
        public ClienteModel Cliente { get; set; }
    }
}