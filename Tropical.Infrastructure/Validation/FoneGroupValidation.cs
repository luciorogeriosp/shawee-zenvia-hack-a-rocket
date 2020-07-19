using System;
using System.Collections.Generic;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace Tropical.Infrastructure.Validation
{

     [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class FoneGroupValidation : ValidationAttribute, IClientValidatable
    {

        string errorMessage = string.Empty;
        string funcao = string.Empty;
        string controles = string.Empty;


        public FoneGroupValidation(string errorMessage, string funcao, string controles)
            : base()
        {
            this.errorMessage = errorMessage;
            this.controles = controles;
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
            rule.ValidationParameters.Add("controles", this.controles);
            yield return rule;
        }

    }
}