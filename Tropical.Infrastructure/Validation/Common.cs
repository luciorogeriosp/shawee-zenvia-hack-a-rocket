using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using Tropical.Infrastructure.Helper;
using System.Net.Mail;

namespace Tropical.Infrastructure.Validation
{
    public class Common
    {
        private static string DateFormat = "dd/MM/yyyy";

        public static bool IsValidEmail(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static bool IsValidName(String nome)
        {
            if (!char.IsLetter(nome[0]))
            {
                return false;
            }

            char currentChar = nome[0];
            int count = 1;
            for (int i = 1; i < nome.Length; i++)
            {
                if (nome[i] == currentChar)
                {
                    count++;
                }
                else
                {
                    currentChar = nome[i];
                    count = 1;
                }

                if (count > 2)
                    return false;
            }

            return true;
        }

        public static bool IsValidRepeat(String nome, Int32 qtde)
        {

            String currentChar = "";
            int count = 0;
            for (int i = 1; i < nome.Length; i++)
            {
                if (currentChar.TextWithoutAccentHelper().ToUpper() == nome.Substring(i, 1).TextWithoutAccentHelper().ToUpper())
                {
                    count++;
                }
                else
                {
                    currentChar = nome.Substring(i, 1);
                    count = 1;
                }

                if (count > qtde)
                    return false;
            }

            return true;
        }

        public static bool IsValidRegEx(String regex, String value)
        {
            Regex _regex = new Regex(@regex);
            Match _match = _regex.Match(value);

            return _match.Success;
        }

        /// <summary>
        /// Retorna uma data fornecida usando um formato de data.
        /// </summary>
        /// <param name="dateValue">Data</param>
        /// <returns>DateTime </returns>
        public static DateTime ParseDate(string dateValue)
        {
            try
            {
                return DateTime.ParseExact(dateValue, DateFormat, CultureInfo.InvariantCulture);
            }
            catch
            {
                return new DateTime();
            }
        }

        /// <summary>
        /// Retorna uma data fornecida usando um formato de data.
        /// </summary>
        /// <param name="dateValue">Data</param>
        /// <returns>DateTime </returns>
        public static DateTime? ParseDateNull(string dateValue)
        {
            try
            {
                return DateTime.ParseExact(dateValue, DateFormat, CultureInfo.InvariantCulture);
            }
            catch
            {
                return null;
            }
        }

        public static bool IsValidDate(object data, int min)
        {
            try
            {
                if (data == null)
                    return false;

                if (data.ToString().Length == 10)
                {
                    DateTime? dataValida = null;
                    dataValida = DateTime.ParseExact(data.ToString(), DateFormat, CultureInfo.InvariantCulture);

                    if (!dataValida.HasValue)
                        return false;

                    if (dataValida.Value.Year < min)
                        return false;
                }
                else
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidMonthYear(object data, int min, int max)
        {
            try
            {
                if (data.ToString().Length == 7)
                {
                    string[] dateParse = data.ToString().Split('/');
                    if ((!IsNumber(dateParse[0])) || (!IsNumber(dateParse[1])))
                        return false;

                    int mes = Convert.ToInt32(dateParse[0]);
                    int ano = Convert.ToInt32(dateParse[1]);

                    if (mes < 1 || mes > 12)
                        return false;

                    if (ano < min || ano > max)
                        return false;
                }
                else
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsNumber(object number)
        {
            try
            {
                Int32 parseInt = Convert.ToInt32(number.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Consulta de CPF
        /// </summary>
        /// <param name="value">CPF</param>
        /// <returns>booleano indicando se o CPF é válido.</returns>
        public static bool IsCpf(object value)
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

        /// <summary>
        /// Consulta de CPPJ
        /// </summary>
        /// <param name="cnpj">CNPJ</param>
        /// <returns>booleano indicando se o CNPJ é válido.</returns>
        public static bool IsCnpj(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;
            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }
    }
}
