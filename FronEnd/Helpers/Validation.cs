using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using Tropical.Infrastructure.Helper;

namespace FrontEnd.Helpers
{
    public static class Validation
    {
        public static string DateFormat = "yyyy-MM-dd";
        public static string DateFormatSlash = "dd/MM/yyyy";
        public static string DateFormatFull = "dd/MM/yyyy HH:mm";
        public const string RegExEmail = @"^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}$";

        public static bool IsLocalSiteURL()
        {
            if (System.Web.HttpContext.Current.Request.UrlReferrer.Port != 80)
            {
                return (System.Web.HttpContext.Current.Request.UrlReferrer.Host + ":" + System.Web.HttpContext.Current.Request.UrlReferrer.Port == System.Web.HttpContext.Current.Request.ServerVariables["HTTP_HOST"]);
            }
            else
            {
                return (System.Web.HttpContext.Current.Request.UrlReferrer.Host == System.Web.HttpContext.Current.Request.ServerVariables["HTTP_HOST"]);
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

        public static bool UrlIsValid(string smtpHost)
        {
            bool br = false;
            try
            {
                IPHostEntry ipHost = Dns.GetHostEntry(smtpHost);
                br = true;
            }
            catch
            {
                br = false;
            }
            return br;
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
                    dataValida = DateTime.ParseExact(data.ToString(), DateFormatSlash, CultureInfo.InvariantCulture);

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

        public static string RemoveAccents(this string text)
        {
            string comAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
            string semAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";

            for (int i = 0; i < comAcentos.Length; i++)
            {
                text = text.Replace(comAcentos[i].ToString(), semAcentos[i].ToString());
            }
            return text;
        }
    }
}