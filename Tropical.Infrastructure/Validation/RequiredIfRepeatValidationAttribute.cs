using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Tropical.Infrastructure.Validation
{
    [AttributeUsageAttribute(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = true)]
    public class RequiredIfRepeatValidationAttribute : RequiredAttribute
    {
        private string PropertyName { get; set; }
        private object ValueToCompare { get; set; }
        private Int32 QtdeChars { get; set; }

        public RequiredIfRepeatValidationAttribute(Int32 qtde)
        {
            QtdeChars = qtde;
        }

        public RequiredIfRepeatValidationAttribute(string propertyName, object valueToCompare, Int32 qtde)
        {
            PropertyName = propertyName;
            ValueToCompare = valueToCompare;
            QtdeChars = qtde;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!(String.IsNullOrEmpty(PropertyName) || ValueToCompare == null))
            {
                var property = validationContext.ObjectType.GetProperty(PropertyName);
                if (property == null)
                    return new ValidationResult(String.Format("Propriedade {0} não encontrada.", PropertyName));

                var propertyValue = property.GetValue(validationContext.ObjectInstance, null);
                var conditionIsMet = propertyValue != null && Equals(propertyValue, ValueToCompare);
                return conditionIsMet ? Common.IsValidRepeat(Convert.ToString(value), QtdeChars) ? ValidationResult.Success : new ValidationResult(this.ErrorMessage) : ValidationResult.Success;
            }
            else
            {
                return Common.IsValidRepeat(Convert.ToString(value), QtdeChars) ? ValidationResult.Success : new ValidationResult(this.ErrorMessage);
            }
        }
    }
}
