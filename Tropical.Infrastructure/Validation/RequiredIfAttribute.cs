using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Tropical.Infrastructure.Validation
{
    [AttributeUsageAttribute(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = true)]
    public class RequiredIfAttribute : RequiredAttribute
    {
        private string PropertyName { get; set; }
        private object ValueToCompare { get; set; }

        public RequiredIfAttribute(string propertyName, object valueToCompare)
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
            var conditionIsMet = propertyValue != null && Equals(propertyValue, ValueToCompare);
            return conditionIsMet ? base.IsValid(value, validationContext) : null;
        }
    }
}
