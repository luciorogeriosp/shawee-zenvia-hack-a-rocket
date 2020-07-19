using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Tropical.Infrastructure.Validation
{
    [AttributeUsageAttribute(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = true)]
    public class RequiredIfDateAttribute : RequiredAttribute
    {
        private string PropertyName { get; set; }
        private object ValueToCompare { get; set; }
        private string SituacaoPropertyName { get; set; }
        private object SituacaoValueToCompareMin { get; set; }
        private object SituacaoValueToCompareMax { get; set; }
        private DateTime? DateMin { get; set; }
        private DateTime? DateMax { get; set; }
        private bool Like { get; set; }
        private Int32 YearMin { get; set; }

        public RequiredIfDateAttribute(string propertyName, object valueToCompare, string situacaoPropertyName, object situacaoValueToCompareMin, object situacaoValueToCompareMax, bool like = false, string datemin = "", string datemax = "", int yearmin = 0)
        {
            PropertyName = propertyName;
            ValueToCompare = valueToCompare;
            SituacaoPropertyName = situacaoPropertyName;
            SituacaoValueToCompareMin = situacaoValueToCompareMin;
            SituacaoValueToCompareMax = situacaoValueToCompareMax;

            // Se informar o texto today no parametro datemin, o sistema atribui o dia de hoje            
            if (datemin.ToLower() == "today")
                DateMin = Common.ParseDateNull(DateTime.Now.ToString("dd/MM/yyyy"));
            else
                DateMin = Common.ParseDateNull(datemin);

            // Se informar o texto today no parametro datemax, o sistema atribui o dia de hoje
            if (datemax.ToLower() == "today")
                DateMax = Common.ParseDateNull(DateTime.Now.ToString("dd/MM/yyyy"));
            else
                DateMax = Common.ParseDateNull(datemax);

            Like = like;
            YearMin = yearmin;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool situacaoConditionIsMet = false;
            bool isValidValue = true;
            string errorMessage = string.Empty;

            var property = validationContext.ObjectType.GetProperty(PropertyName);
            if (property == null && value != null && ((!Common.IsValidDate(value, YearMin)) || (!Common.IsValidMonthYear(value, YearMin, DateTime.Now.Year))))
            {
                errorMessage = String.Format("O campo \"{0}\" deve ser uma data válida.", this.ErrorMessage);
                situacaoConditionIsMet = true;
                isValidValue = false;
            }

            if (property != null)
            {

                var propertyValue = property.GetValue(validationContext.ObjectInstance, null);
                var conditionIsMet = Equals(propertyValue, ValueToCompare);

                if (conditionIsMet)
                {
                    if (((!Common.IsValidDate(value, YearMin)) && (!Common.IsValidMonthYear(value, YearMin, DateTime.Now.Year))) && value != null)
                    {
                        errorMessage = String.Format("O campo \"{0}\" deve ser uma data válida.", this.ErrorMessage);
                        situacaoConditionIsMet = true;
                        isValidValue = false;
                    }

                    var situacaoProperty = validationContext.ObjectType.GetProperty(SituacaoPropertyName);
                    if (situacaoProperty != null)
                    {

                        var situacaoPropertyValue = situacaoProperty.GetValue(validationContext.ObjectInstance, null);
                        if (situacaoPropertyValue != null)
                        {
                            if ((!situacaoConditionIsMet) && (!String.IsNullOrEmpty(SituacaoValueToCompareMin.ToString())))
                            {
                                if (Like)
                                    situacaoConditionIsMet = situacaoPropertyValue.ToString().ToLower().IndexOf(SituacaoValueToCompareMin.ToString().ToLower()) >= 0;
                                else
                                    situacaoConditionIsMet = Equals(situacaoPropertyValue, SituacaoValueToCompareMin);

                                if (situacaoConditionIsMet)
                                {
                                    errorMessage = String.Format("O campo \"{0}\" deve ser maior do que a data de hoje.", this.ErrorMessage);
                                    isValidValue = this.IsValidValueMin(value);
                                }
                            }

                            if ((!situacaoConditionIsMet) && (!String.IsNullOrEmpty(SituacaoValueToCompareMax.ToString())))
                            {
                                if (Like)
                                    situacaoConditionIsMet = situacaoPropertyValue.ToString().ToLower().IndexOf(SituacaoValueToCompareMax.ToString().ToLower()) >= 0;
                                else
                                    situacaoConditionIsMet = Equals(situacaoPropertyValue, SituacaoValueToCompareMax);

                                if (situacaoConditionIsMet)
                                {
                                    errorMessage = String.Format("O campo \"{0}\" deve ser menor do que a data de hoje.", this.ErrorMessage);
                                    isValidValue = this.IsValidValueMax(value);
                                }
                            }
                        }
                    }
                }
            }
            return situacaoConditionIsMet ? (isValidValue ? ValidationResult.Success : new ValidationResult(errorMessage)) : null;
        }

        private bool IsValidValueMin(object value)
        {
            bool ret = false;

            try
            {
                DateTime dateToValidate;

                if (value != null && DateTime.TryParse(value.ToString(), out dateToValidate))
                {
                    ret = (DateMin.HasValue && dateToValidate > DateMin);
                }
                else
                {
                    ret = false;
                }
            }
            catch { }

            return ret;
        }

        private bool IsValidValueMax(object value)
        {
            bool ret = false;

            try
            {
                DateTime dateToValidate;

                if (value != null && DateTime.TryParse(value.ToString(), out dateToValidate))
                {
                    ret = (DateMax.HasValue && dateToValidate < DateMax);
                }
                else
                {
                    ret = false;
                }
            }
            catch { }

            return ret;
        }
    }
}
