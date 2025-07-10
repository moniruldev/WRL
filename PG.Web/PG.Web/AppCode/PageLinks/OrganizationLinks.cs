using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using PG.Core.Web;

namespace PG.Web.PageLinks
{
    public class OrganizationLinks
    {
        public static string GetLink_Location
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Organization/GetLocation.ashx"); }
        }
    }
}