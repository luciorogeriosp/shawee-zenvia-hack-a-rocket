using System;
using System.Collections.Generic;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace Tropical.Infrastructure.Validation
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class DDDFoneValidation : ValidationAttribute, IClientValidatable
    {
        string errorMessage = string.Empty;
        string ddd = string.Empty;
        string fone = string.Empty;
        string ramal = string.Empty;
        string funcao = string.Empty;

        public DDDFoneValidation(string errorMessage, string ddd, string fone, string ramal, string funcao)
            : base()
        {
            this.errorMessage = errorMessage;
            this.ddd = ddd;
            this.fone = fone;
            this.ramal = ramal;
            this.funcao = funcao;
        }


        public override bool IsValid(object value)
        {
            return true;
        }


        // Client-Side validation
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule { ValidationType = this.funcao, ErrorMessage = this.errorMessage };
            rule.ValidationParameters.Add("ddd", this.ddd);
            rule.ValidationParameters.Add("fone", this.fone);
            rule.ValidationParameters.Add("ramal", this.ramal);
            yield return rule;
        }
    }



}