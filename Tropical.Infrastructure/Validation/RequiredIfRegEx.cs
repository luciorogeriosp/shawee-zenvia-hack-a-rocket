using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Tropical.Infrastructure.Validation
{
    /// <summary>
    /// Validacao de textos com mais espacos do que o necessario
    /// </summary>
    [AttributeUsageAttribute(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = true)]
    public class RequiredIfRegEx : ValidationAttribute
    {
        private string PropertyName { get; set; }
        private object ValueToCompare { get; set; }
        private string Pattern { get; set; }

        public RequiredIfRegEx(string propertyName, object valueToCompare, string pattern)
        {
            PropertyName = propertyName;
            ValueToCompare = valueToCompare;
            Pattern = pattern;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var property = validationContext.ObjectType.GetProperty(PropertyName);
                if (property == null)
                    return new ValidationResult(String.Format("Propriedade {0} não encontrada.", PropertyName));

                var propertyValue = property.GetValue(validationContext.ObjectInstance, null);
                var conditionIsMet = Equals(propertyValue, ValueToCompare);
                DateTime data = new DateTime();

                if (value != null)
                    DateTime.TryParse(value.ToString(), out data);
                
                return conditionIsMet ? (Common.IsValidRegEx(Pattern, data.ToShortDateString()) ? ValidationResult.Success : new ValidationResult(this.ErrorMessage)) : ValidationResult.Success;
            }
            else
                return ValidationResult.Success;
        }
    }
}
