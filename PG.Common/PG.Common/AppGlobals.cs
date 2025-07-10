using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.Common
{
    public class AppGlobals
    {
        //public static AppSystemEnum AppSystem = AppSystemEnum.Default;
        //public static bool LocationLogin = false;

        public static int AppID = 1;
        public static AppSystemEnum AppSystem = AppSystemEnum.Default;
        public static bool AppIDUserLogin = false;
        public static int LoginInfoAppID = 1;
        public static bool LocationLogin = false;
        public static bool PasswordCaseInsensitive = false;
        
        public static bool KeepLive = false;
        public static int KeepLiveInterval = 300;

        public static bool BrowserPrivateMode = false;
        public static List<string> BrowserSupport = new List<string>(new string[] { "All" });

    }
}
