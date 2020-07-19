using FrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEnd.Models
{
    public class ReturnModel
    {
        public ReturnModel()
        {
            ErrorValidation = new Dictionary<string, string>();
        }

        public Constantes.StatusRetorno Status { get; set; }

        public string Mensagem { get; set; }

        public string Titulo { get; set; }

        public string RedirectUrl { get; set; }

        public Dictionary<string, string> ErrorValidation { get; set; }

        public Dictionary<string, string> Errors
        {
            get { return ErrorValidation; }
        }

        public void AddError(string name, string message)
        {
            ErrorValidation.Add(name, message);
        }



        public string GetError(string name)
        {
            string ret = string.Empty;

            if (ErrorValidation.ContainsKey(name))
            {
                ret = "<span class=\"field-validation-error\">" + ErrorValidation[name] + "</span>";
            }

            return ret;
        }
    }
}