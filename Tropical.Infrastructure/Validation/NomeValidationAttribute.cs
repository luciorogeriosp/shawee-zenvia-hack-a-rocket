using System.ComponentModel.DataAnnotations;

namespace Tropical.Infrastructure.Validation
{
    /// <summary>
    /// ValidationAttribute para Nome.
    /// </summary>
    public class NomeValidationAttribute : ValidationAttribute
    {
        /// <summary>
        /// Valida se um Nome é válido
        /// </summary>
        /// <param name="value">Nome</param>
        /// <returns>Booleano indicando se o nome é válido.</returns>
        public override bool IsValid(object value)
        {
            if (value == null) 
                return false;

            return Validation.Common.IsValidName(value.ToString());
        }
    }
}

