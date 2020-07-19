using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Tropical.Infrastructure.Util
{
    public class ParameterCollection
    {
        public static Dictionary<string, string> Parameters = new Dictionary<string, string>();

        public static string GetParamValue(string name)
        {
            return Parameters.ContainsKey(name) ? Parameters[name] : ConfigurationManager.AppSettings[name];
        }

    }
}
