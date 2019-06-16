using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anand
{
    static class Application
    {
        private static string _MASAPIURL = Properties.Settings.Default.MASAPI;
        private static string _MASAPIResourceID = Properties.Settings.Default.MASResourceID;

        public static string MASAPIURL
        {
            get { return _MASAPIURL; }
        }

        public static string MASAPIResourceID
        {
            get { return _MASAPIResourceID; }
        }

        public static void Initialize()
        {
            _MASAPIURL = Properties.Settings.Default.MASAPI;
            _MASAPIResourceID = Properties.Settings.Default.MASResourceID;
        }
    }
}
   

