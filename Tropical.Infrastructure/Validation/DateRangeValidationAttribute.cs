using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Tropical.Infrastructure.Validation
{
    /// <summary>
    /// ValidationAttribute para range de Datas.
    /// </summary>
    public class DateRangeValidationAttribute : ValidationAttribute
    {        
        public DateTime MinDate { get; set; }
        public DateTime MaxDate { get; set; }
        public int MinAnos { get; set; }
        public int MaxAnos { get; set; }
        private string PropertyName { get; set; }
        private object ValueToCompare { get; set; }

        /// <summary>
        /// Valida a data, sendo que a data limite é hoje.
        /// </summary>
        public DateRangeValidationAttribute()
            : base("")
        {
            MinDate = DateTime.MinValue;
            MaxDate = DateTime.Today;
        }

        /// <summary>
        /// Valida a data, sendo que a data limite é hoje. Também valida se a data informada até hoje é maior ou igual ao valor de anos.
        /// </summary>
        public DateRangeValidationAttribute(int anos)
            : base("")
        {
            this.MinAnos = anos;
            MinDate = DateTime.MinValue;
            MaxDate = DateTime.Today;
        }

        /// <summary>
        /// Valida a data, sendo que a data limite é hoje. Também valida se a data informada está em um range de anos a partir da data atual.
        /// </summary>
        public DateRangeValidationAttribute(int minAnos, int maxAnos)
            : base("")
        {
            this.MinAnos = minAnos;
            this.MaxAnos = maxAnos;
            MinDate = DateTime.MinValue;
            MaxDate = DateTime.Today;
        }

        /// <summary>
        /// Valida um range de Datas.
        /// </summary>
        /// <param name="minDate">Data de início</param>
        /// <param name="maxDate">Data de término</param>
        public DateRangeValidationAttribute(string minDate, string maxDate)
            : base("")
        {
            MinDate = Common.ParseDate(minDate);
            MaxDate = Common.ParseDate(maxDate);
        }

        /// <summary>
        /// Valida um range de Datas.
        /// </summary>
        /// <param name="minDate">Data de início</param>
        /// <param name="maxDate">Data de término</param>
        public DateRangeValidationAttribute(string propertyName, object valueToCompare, string minDate, string maxDate)
            : base("")
        {
            MinDate = Common.ParseDate(minDate);
            MaxDate = Common.ParseDate(maxDate);
            PropertyName = propertyName;
            ValueToCompare = valueToCompare;
        }
        
        ///// <summary>
        ///// Valida uma entrada de DateTime.
        ///// </summary>
        ///// <param name="value">Data</param>
        ///// <returns>Booleano indicando se é data válida.</returns>
        //public override bool IsValid(object value)
        //{
        //    var isDateTime = false;

        //    DateTime dateValue;

        //    if (value != null && DateTime.TryParse(value.ToString(), out dateValue))
        //    {
        //        isDateTime = MinDate <= dateValue && dateValue <= MaxDate
        //                     && (MinAnos == 0 || MaxDate.AddYears(-MinAnos) >= dateValue)
        //                     && (MaxAnos == 0 || MaxDate <= dateValue.AddYears(MaxAnos));
        //    }

        //    return isDateTime;
        //}

        /// <summary>
        /// Valida uma entrada de DateTime.
        /// </summary>
        /// <param name="value">Data</param>
        /// <returns>Booleano indicando se é data válida.</returns>
        public bool IsValidRangeDate(object value)
        {
            var isDateTime = false;

            DateTime dateValue;

            if (value != null && DateTime.TryParse(value.ToString(), out dateValue))
            {
                isDateTime = MinDate <= dateValue && dateValue <= MaxDate
                             && (MinAnos == 0 || MaxDate.AddYears(-MinAnos) >= dateValue)
                             && (MaxAnos == 0 || MaxDate <= dateValue.AddYears(MaxAnos));
            }

            return isDateTime;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (String.IsNullOrEmpty(PropertyName))
            {
                return (IsValidRangeDate(value) ? ValidationResult.Success : new ValidationResult(this.ErrorMessage));
            }
            else
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
                        return (IsValidRangeDate(value) ? ValidationResult.Success : new ValidationResult(this.ErrorMessage));
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
}

