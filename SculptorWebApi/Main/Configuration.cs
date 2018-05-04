using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SculptorWebApi.Main
{
    public static class Configuration
    {
        public static string LOGS { get { return ConfigurationManager.AppSettings["LOGS"]; } }
    }
}