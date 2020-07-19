using FrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEnd.Helpers
{
    public class Notify
    {
        public static string JsNotify(ReturnModel model, string ParentElement = "")
        {
            string ret = string.Empty;
            string type = string.Empty;
            string title = model.Titulo;
            string msg = model.Mensagem;

            if (string.IsNullOrEmpty(title))
            {
                title = "SAL";
            }

            switch (model.Status)
            {
                case Constantes.StatusRetorno.Sucesso:
                    type = "success";
                    break;
                case Constantes.StatusRetorno.Alerta:
                    type = "warning";
                    break;
                case Constantes.StatusRetorno.Erro:
                    type = "danger";
                    break;
                case Constantes.StatusRetorno.Informacao:
                    type = "info";
                    break;
                default:
                    return string.Empty;
            }

            if (string.IsNullOrEmpty(ParentElement))
            {
                ret = "" +
                    "<script>" +
                    "ShowNotify(\"" + type + "\", \"" + title + "\", \"" + msg + "\")" +
                    "</script>";
            }
            else
            {
                ret = "" +
                    "<script>" +
                    "ShowNotify(\"" + type + "\", \"" + title + "\", \"" + msg + "\", \"" + ParentElement + "\")" +
                    "</script>";
            }

            if (!string.IsNullOrEmpty(model.RedirectUrl))
            {
                ret += "" +
                    "<script>" +
                    "OnBeginForm();" +
                    "setTimeout(function() {" +
                        "redirectToURL('" + model.RedirectUrl + "');" + 
                    "}, 0); " +
                "</script>";
            }

            return ret;
        }

    }
}