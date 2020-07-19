using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Tropical.Infrastructure.CustomException;
using Tropical.Infrastructure.Model;

namespace Tropical.Infrastructure.Util
{
    public class Messages
    {
        public static ContentResult SessionExpiredContent()
        {
            ContentResult ret = new ContentResult();
            ret.Content = "Sua sessão expirou.<meta http-equiv=\"Refresh\" content=\"2;.\">";
            return ret;
        }

        public static ContentResult NoMatchContent()
        {
            ContentResult ret = new ContentResult();
            ret.Content = NoMatch();
            return ret;
        }

        public static String NoMatch()
        {
            return "Não há registros a serem exibidos.";
        }

        public static String NoFilter()
        {
            return "Defina um filtro.";
        }

        public static ContentResult MessageContent(String message)
        {
            ContentResult ret = new ContentResult();
            ret.Content = message;
            return ret;
        }

        public static ContentResult ExceptionErrorContent(Exception ex)
        {
            ContentResult ret = new ContentResult();
            ret.Content = "" +
                "<div class=\"alert alert-error\">" +
                "   <a class=\"close\" data-dismiss=\"alert\" href=\"#\">&times;</a>" + ex.Message +
                "</div>";
            return ret;
        }

        public static void MessageFormAddSucess(TempDataDictionary TempData)
        {
            TempData["MsgSucess"] = "Registro incluído com sucesso.";
        }

        public static void MessageFormDeleteSucess(TempDataDictionary TempData)
        {
            TempData["MsgSucess"] = "Registro apagado com sucesso.";
        }

        public static void MessageFormUpdateSucess(TempDataDictionary TempData)
        {
            TempData["MsgSucess"] = "Registro alterado com sucesso.";
        }

        public static void MessageFormAddError(TempDataDictionary TempData)
        {
            TempData["MsgError"] = "Não foi possível incluir o registro.";
        }

        public static void MessageFormDeleteError(TempDataDictionary TempData)
        {
            TempData["MsgError"] = "Não foi possível apagar o registro.";
        }

        public static void MessageFormUpdateError(TempDataDictionary TempData)
        {
            TempData["MsgError"] = "Não foi possível alterar o registro.";
        }

        public static void MessageFormException(TempDataDictionary TempData, Exception ex)
        {
            TempData["MsgError"] = ex.Message;
        }

        public static ReturnMessageModel ReturnMessageFromException(Exception ex)
        {
            ReturnMessageModel returnMessageModel = new ReturnMessageModel();

            returnMessageModel.MessageType = EnumMessageType.Error;
            returnMessageModel.Message = ex.Message;
            returnMessageModel.InnerException = ex.InnerException != null ? ex.InnerException.ToString() : "";
            returnMessageModel.Source = ex.Source;
            returnMessageModel.Target = ex.TargetSite.ToString();

            return returnMessageModel;
        }

        private static ReturnMessageModel ReturnMessage(String message, EnumMessageType messageType)
        {
            ReturnMessageModel returnMessageModel = new ReturnMessageModel();

            returnMessageModel.MessageType = messageType;
            returnMessageModel.Message = message;

            return returnMessageModel;
        }

        public static ReturnMessageModel ReturnMessageInformation(String message)
        {
            return ReturnMessage(message, EnumMessageType.Information);
        }

        public static ReturnMessageModel ReturnMessageWarning(String message)
        {
            return ReturnMessage(message, EnumMessageType.Warning);
        }

        public static ReturnMessageModel ReturnMessageSucess(String message)
        {
            return ReturnMessage(message, EnumMessageType.Sucess);
        }

        public static ReturnMessageModel ReturnMessageError(String message)
        {
            return ReturnMessage(message, EnumMessageType.Error);
        }
    }
}

