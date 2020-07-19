using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tropical.Infrastructure.Validation;

namespace Tropical.Infrastructure.Validation
{
    [AttributeUsageAttribute(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = true)]
    public class RequiredIfLengthAttribute : StringLengthAttribute
    {        
        private string PropertyName { get; set; }
        private object ValueToCompare { get; set; }

        public RequiredIfLengthAttribute(string propertyName, object valueToCompare, int maximumLength) : base (maximumLength)
        {
            PropertyName = propertyName;
            ValueToCompare = valueToCompare;
        }
        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(PropertyName);
            if (property == null)
                return new ValidationResult(String.Format("Propriedade {0} não encontrada.", PropertyName));

            var propertyValue = property.GetValue(validationContext.ObjectInstance, null);
            var conditionIsMet = Equals(propertyValue, ValueToCompare);

            if (conditionIsMet)
            {
                if (value != null)
                {
                    return (value.ToString().Length >= this.MinimumLength && value.ToString().Length <= this.MaximumLength) ? ValidationResult.Success : new ValidationResult(this.ErrorMessage);
                }
                else
                {
                    return new ValidationResult(this.ErrorMessage);
                }
            }
            else
            {
                return ValidationResult.Success;
            }
        }       
    }
}

