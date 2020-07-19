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
    public class RequiredIfNameAttribute : ValidationAttribute
    {
        private string PropertyName { get; set; }
        private object ValueToCompare { get; set; }

        public RequiredIfNameAttribute(string propertyName, object valueToCompare)
        {
            PropertyName = propertyName;
            ValueToCompare = valueToCompare;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var property = validationContext.ObjectType.GetProperty(PropertyName);
                if (property == null)
                    return new ValidationResult(String.Format("Propriedade {0} não encontrada.", PropertyName));

                var propertyValue = property.GetValue(validationContext.ObjectInstance, null);
                var conditionIsMet = propertyValue != null && Equals(propertyValue, ValueToCompare);

                return conditionIsMet ? (Validation.Common.IsValidName(value.ToString()) ? ValidationResult.Success : new ValidationResult(this.ErrorMessage)) : ValidationResult.Success;
            }
            else
                return ValidationResult.Success;
        }
    }
}
