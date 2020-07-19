using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tropical.Infrastructure.Model
{
    public enum EnumMessageType
    {
        Sucess = 1,
        Information = 2,
        Warning = 3,
        Error = 4
    }
    
    public class ReturnMessageModel
    {
        public EnumMessageType MessageType { get; set; }
        public String Message {get; set;}
        public String Source  {get; set;}
        public String Target  {get; set;}
        public String InnerException { get; set; }
        public String RedirectTo { get; set; }
    }
}