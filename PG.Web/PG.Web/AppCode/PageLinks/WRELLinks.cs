using PG.Core.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PG.Web.AppCode.PageLinks
{
    public class WRELLinks
    {
        public static string GetLink_CountryList
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/WREL/GetDistrictList.ashx"); }
        }
    }
}