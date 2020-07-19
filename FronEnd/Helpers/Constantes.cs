using FrontEnd.Util;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;

namespace FrontEnd.Helpers
{
    [Serializable]
    public class Constantes
    {
        internal static readonly string MsgNaoFoiPossivelCompletarOperacao = "Não foi possível realizar essa operação.";
        internal static readonly string MsgRegistroDuplicado = "Registro duplicado";
        internal static readonly string MsgCampoObrigatorioNaoPreenchido = "Campo obrigatório não preenchido";
        internal static readonly string MsgTamanhoCampoMaiorQueDefinido = "Tamanho maior que o definido";
        internal static readonly string MsgCampoInvalido = "Campo inválido";
        internal static readonly string MsgRegistroReferenciadoInexistente = "Registro inexistente";
        internal static readonly string MsgViolacaoExclusaoRegistro = "Violação de exclusão de registro";

        public enum StatusRetorno
        {
            Sucesso = 1,
            Alerta = 2,
            Erro = 3,
            Informacao = 4
        }

        public enum PerfilSistema
        {
            HeptadaArquivista = 1,
            HeptadaMestre = 2,
            HeptadaMonitor = 3,
            HeptadaAssociado = 4,
            HeptadaIniciado = 5,
            HeptadaRecebedoria = 6,
            HeptadaBiblioteca = 7,
            HeptadaVerificacaoContas = 8,

            LojaRosacruzMestre = 9,
            LojaRosacruzSecretario = 10,
            LojaRosacruzPresidenteJunta = 11,
            LojaRosacruzSecretarioJunta = 12,
            LojaRosacruzTesoureiroJunta = 13,
            LojaRosacruzVerificacaoContas = 17,

            CapituloRosacruzMestre = 18,
            CapituloRosacruzSecretario = 19,
            CapituloRosacruzPresidenteJunta = 20,
            CapituloRosacruzSecretarioJunta = 21,
            CapituloRosacruzTesoureiroJunta = 22,
            CapituloRosacruzVerificacaoContas = 26
        }

        public enum CodigoIniciacao
        {
            MartinistaAssociado = 2,
            MartinistaIniciado = 18,
            MartinistaSI = 19,
            RosacruzPrimeiroGrau = 4,
            Loja = 5,
            Pronaos = 4
        }

        public static string DescricaoMes(int mes)
        {
            try
            {
                CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
                TextInfo textInfo = cultureInfo.TextInfo;
                return textInfo.ToTitleCase(new DateTime(1900, mes, 1).ToString("MMMM", new CultureInfo("pt-BR")));
            }
            catch //(Exception ex)
            {
                return mes.ToString();
            }

        }

        public static string DiaDaSemana(DateTime dt)
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;

            return textInfo.ToTitleCase(dt.ToString("dddd", new CultureInfo("pt-BR")));
        }

        public static string DataPorExtenso(DateTime dt)
        {
            string _DataPorExtenso = string.Empty;

            _DataPorExtenso = DiaDaSemana(dt) + ", " + dt.ToString("dd") + " de " + DescricaoMes(dt.Month) + " de " + dt.ToString("yyyy");

            if (dt.Hour > 0 || dt.Millisecond > 0)
            {
                _DataPorExtenso += " às " + dt.ToString("HH:mm");
            }

            return _DataPorExtenso;
        }

        public static string DescricaoMesAbreviado(int mes)
        {
            return new CultureInfo("pt-BR").DateTimeFormat.GetAbbreviatedMonthName(mes);
        }

        public static string DescricaoSemana(int semana)
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            return textInfo.ToTitleCase(new CultureInfo("pt-BR").DateTimeFormat.GetDayName((DayOfWeek)semana));
        }

        public static string DescricaoSemanaAbreviado(int semana)
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            return textInfo.ToTitleCase(new CultureInfo("pt-BR").DateTimeFormat.GetAbbreviatedDayName((DayOfWeek)semana));
        }

        public static void DefineDatasAnuais(int intAno, ref DateTime dtInicial, ref DateTime dtFinal)
        {
            dtInicial = new DateTime(intAno, 1, 1);
            dtFinal = new DateTime(intAno, 12, 31);


            //switch ((TipoOrganismoAfiliado)Util.Configuracao.PerfilSelecionadoLogado.Cliente.OrganismoAfiliadoTipoId)
            //{
            //    case TipoOrganismoAfiliado.AtriumMartinista:
            //    case TipoOrganismoAfiliado.HeptadaMartinista:

            //        dtInicial = new DateTime(intAno, 1, 1);
            //        dtFinal = new DateTime(intAno, 12, 31);

            //        break;
            //    default:
            //        dtInicial = new DateTime(intAno, 4, 1);
            //        dtFinal = dtInicial.AddYears(1);
            //        dtFinal = new DateTime(dtFinal.Year, 4, 1);
            //        dtFinal = dtFinal.AddDays(-1);
            //        break;
            //}
        }

        public static int CapturaAnoRosacruz(DateTime dtReferencia)
        {
            dtReferencia = new DateTime(dtReferencia.Year + 1352, dtReferencia.Month, dtReferencia.Day);

            DateTime dtInicial = new DateTime(1, 3, 21);
            TimeSpan tsDuration;            
            tsDuration = dtReferencia - dtInicial;

            return Convert.ToInt32((tsDuration.Days) / 365);
        }
    }
}