using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tropical.Infrastructure.Model
{
    [Serializable()]
    public class User
    {
        public String Id { get; set; }
        public String Cpf { get; set; }

        public String Nome { get; set; }
        public String NomeCurto { get; set; }
        public String Rg { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public String PasswordTemp { get; set; }

        public int Filtro { get; set; }

        public Boolean Administrator { get; set; }
        public Boolean Ativo { get; set; }
    }
}
