using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PubLibIS.View.Util
{
    public static class ConnectionStringResolver
    {
        public static string CurrentConnectionString { get; private set; }
        public static string DefaultConnectionString { get; private set; }
        public static string TempJsonConnectionString { get; private set; }
        public static string SwitchTo
        {
            get
            {
                return CurrentConnectionString == DefaultConnectionString ?
                          TempJsonConnectionString :
                          DefaultConnectionString;
            }
        }
        static ConnectionStringResolver()
        {
            DefaultConnectionString = "LibConnection";
            TempJsonConnectionString = "tempJson";
            CurrentConnectionString = DefaultConnectionString;
        }
        public static void SwitchConnection()
        {
            CurrentConnectionString = SwitchTo;
            DependencyResolverSetter.Inject();
        }
    }
}