using System.ComponentModel.DataAnnotations;

namespace Tropical.Infrastructure.Validation
{
    public class CPFValidationAttribute : ValidationAttribute
    {
        /// <summary>
        /// Consulta de CPF
        /// </summary>
        /// <param name="value">CPF</param>
        /// <returns>booleano indicando se o CPF é válido.</returns>
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;



            string cpf = value.ToString();


            if (cpf == "00000000000" ||
                   cpf == "11111111111" ||
                   cpf == "22222222222" ||
                   cpf == "33333333333" ||
                   cpf == "44444444444" ||
                   cpf == "55555555555" ||
                   cpf == "66666666666" ||
                   cpf == "77777777777" ||
                   cpf == "88888888888" ||
                   cpf == "99999999999")
            {
                return false;
            }




            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf;
            string digito;

            int soma;
            int resto;

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            long nCpf = 0;
            if (cpf.Length != 11 || !long.TryParse(cpf, out nCpf))
                return false;

            tempCpf = cpf.Substring(0, 9);
            soma = 0;
            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);

        }
    }
}

