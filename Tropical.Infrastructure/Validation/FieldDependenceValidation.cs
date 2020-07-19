using System;
using System.Collections.Generic;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace Tropical.Infrastructure.Validation
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class FieldDependenceValidation : ValidationAttribute, IClientValidatable
    {
        string errorMessage = string.Empty;
        string campo = string.Empty;
        string valor = string.Empty;
        string dependente = string.Empty;
        string funcao = string.Empty;

        public FieldDependenceValidation(string errorMessage, string campo, string valor, string dependente, string funcao)
            : base()
        {
            this.errorMessage = errorMessage;
            this.campo = campo;
            this.valor = valor;
            this.dependente = dependente;
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
            rule.ValidationParameters.Add("campo", this.campo);

            rule.ValidationParameters.Add("valor", string.IsNullOrEmpty(this.valor) ? null : this.valor.ToLower());
            rule.ValidationParameters.Add("dependente", this.dependente);
            yield return rule;
        }
    }



}