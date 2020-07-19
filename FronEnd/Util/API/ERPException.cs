using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FrontEnd.Util.API
{
    /// <summary>
    /// Classe para tratamento de ERPException
    /// </summary>
    public class ERPException : Exception
    {
        public string Mensagem 
        {
            get
            {
                var strMensagem = string.Empty;

                if (this.InnerException != null)
                {
                    if (this.InnerException.InnerException != null)
                    {
                        var errorCode = this.InnerException.InnerException.Message.Substring(0, 9);

                        switch (errorCode)
                        {
                            case "ORA-00001":
                                {
                                    strMensagem = Helpers.Constantes.MsgRegistroDuplicado;
                                    break;
                                }

                            case "ORA-20000":
                                {
                                    strMensagem = Helpers.Constantes.MsgRegistroDuplicado;
                                    break;
                                }

                            case "ORA-01400":
                                {
                                    strMensagem = Helpers.Constantes.MsgCampoObrigatorioNaoPreenchido;
                                    break;
                                }

                            case "ORA-01401":
                                {
                                    strMensagem = Helpers.Constantes.MsgTamanhoCampoMaiorQueDefinido;
                                    break;
                                }

                            case "ORA-01407":
                                {
                                    strMensagem = Helpers.Constantes.MsgCampoObrigatorioNaoPreenchido;
                                    break;
                                }

                            case "ORA-02290":
                                {
                                    strMensagem = Helpers.Constantes.MsgCampoInvalido;
                                    break;
                                }

                            case "ORA-02291":
                                {
                                    strMensagem = Helpers.Constantes.MsgRegistroReferenciadoInexistente;
                                    break;
                                }

                            case "ORA-02292":
                                {
                                    strMensagem = Helpers.Constantes.MsgViolacaoExclusaoRegistro;
                                    break;
                                }

                            default:
                                {
                                    strMensagem = Helpers.Constantes.MsgNaoFoiPossivelCompletarOperacao;
                                    break;
                                }
                        }
                    }
                    else
                    {
                        strMensagem = this.Message;
                    }
                }
                else
                {
                    strMensagem = this.Message;
                }

                return strMensagem;
            }
        }

        /// <summary>
        /// Construtor da classe
        /// </summary>
        public ERPException()
        {
            // Add implementation.
        }

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="strMensagem">Mensagem de exceção</param>
        public ERPException(string message)
            : base(message)
        {
            
        }

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="strMensagem">Mensagem de exceção</param>
        /// <param name="innerException">InnerException</param>
        public ERPException(string message, Exception innerException) : base (message, innerException)
        {
        
        }

        /// <summary>
        /// Construtor da classe
        /// </summary>
        protected ERPException(SerializationInfo info, StreamingContext context)
        {
            
        }
    }
}
