using PG.Core.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PG.Web.PageLinks
{
    public class HMSLinks
    {
        public static string GetLink_CountryList
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/HMS/GetCountryList.ashx"); }
        }
    }
}