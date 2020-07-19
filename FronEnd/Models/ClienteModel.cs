using FrontEnd.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Models
{
    [Serializable]
    public class ClienteModel
    {
        public int Id { get; set; }
        public int SegmentoId { get; set; }

        public int RegiaoId { get; set; }
        public string RegiaoSigla { get; set; }

        public int OrganismoAfiliadoTipoId { get; set; }

        public string OrganismoAfiliadoTipoNome { get; set; }

        public string Nome { get; set; }
        public string RazaoSocial { get; set; }
        public string Endereco { get; set; }
        public string EnderecoNumero { get; set; }
        public string EnderecoComplemento { get; set; }
        public string EnderecoBairro { get; set; }
        public string EnderecoCEP { get; set; }

        public string EnderecoEstado { get; set; }
        public SelectList EnderecoEstadoLista { get; set; }

        public SelectList EnderecoCidadeLista { get; set; }

        public int? CDD_ID { get; set; }

        public string Telefone1 { get; set; }
        public string Telefone1DDD { get; set; }
        public string Telefone1Numero { get; set; }
        public string Telefone2 { get; set; }
        public string Telefone2DDD { get; set; }
        public string Telefone2Numero { get; set; }
        public string Email { get; set; }
        public string Site { get; set; }
        public string Facebook { get; set; }
        public string CNPJ { get; set; }
        public string IE { get; set; }

        public string CCM { get; set; }
        public string CaixaPostal { get; set; }
        public string DadosBancariosBancoNumero { get; set; }
        public string DadosBancariosBancoNome { get; set; }
        public string DadosBancariosAgencia { get; set; }
        public string DadosBancariosConta { get; set; }
        public string DadosBancariosContato { get; set; }

        public string EmailPassword { get; set; }

        public bool EmailPasswordValidate { get; set; }

        public string EmailServer { get; set; }

        public int? EmailPort { get; set; }

        public bool EmailUseDefaultCredencials { get; set; }

        public bool EmailEnableSsl { get; set; }

        public bool? Ativo { get; set; }
        public string NumeroAfiliacao { get; internal set; }
        public string NomeCompleto { get; internal set; }

        public string Logotipo { get; set; }

        public int QuantidadeOA { get; set; }

        public DateTime? DataFundacao { get; set; }

        public bool AtualizacaoMembros { get; set; }

        public bool HabilitarOrdemVenda { get; set; }

        public bool HabilitarPlanoFamiliar { get; set; }
    }
}