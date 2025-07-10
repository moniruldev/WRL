using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

using PG.DBClass.OrganiztionDC;
using PG.Report;

using PG.BLLibrary.OrganizationBL;

namespace PG.Web
{
    public class CompanyInfo
    {
        public static int CompanyID_Default = 1; //default
        public static string CompanyInfoSessionKey = "SK_CompanyInfo";

        public static int GetCompanyID()
        {
            return GetCompanyID(HttpContext.Current);
        }
        public static int GetCompanyID(HttpContext context)
        {
            int companyID = 0;
            dcCompany compInfo = GetCompanyInfoFromSession(context);
            if (compInfo != null)
            {
                companyID = compInfo.CompanyID;
            }

            return companyID;
        }

        public static dcCompany GetCompanyInfoFromSession()
        {
            return GetCompanyInfoFromSession(HttpContext.Current);
        }

        public static dcCompany GetCompanyInfoFromSession(HttpContext context)
        {
            dcCompany compInfo = null;
            if (context.Session[CompanyInfoSessionKey] != null)
            {
                compInfo = context.Session[CompanyInfoSessionKey] as dcCompany;
            }
            return compInfo;
        }


        public static void SetCompanyInfoToSession(int pCompanyID)
        {
            SetCompanyInfoToSession(pCompanyID, HttpContext.Current);
        }
        public static void SetCompanyInfoToSession(int pCompanyID, HttpContext context)
        {
            dcCompany compInfo = CompanyBL.GetCompanyByID(pCompanyID);
            context.Session[CompanyInfoSessionKey] = compInfo;
        }


        public static void SetCompanyInfoToReportOptions(ReportOptions rptOption)
        {
            SetCompanyInfoToReportOptions(rptOption, HttpContext.Current);
        }

        public static void SetCompanyInfoToReportOptions(ReportOptions rptOption, HttpContext context)
        {
            dcCompany compInfo = GetCompanyInfoFromSession(context);
            if (compInfo != null)
            {
                rptOption.CompanyName =  compInfo.CompanyName;
                rptOption.CompanyAddress = compInfo.CompanyAddress;
            }
        }


        public static void SetCompanyInfoToReportOptions(NameValueCollection rptParams)
        {
            SetCompanyInfoToReportOptions(rptParams, HttpContext.Current);
        }

        public static void SetCompanyInfoToReportOptions(NameValueCollection rptParams, HttpContext context)
        {
            dcCompany compInfo = GetCompanyInfoFromSession(context);
            if (compInfo != null)
            {
                rptParams.Add("CompanyName", compInfo.CompanyName);
                rptParams.Add("CompanyAddress", compInfo.CompanyAddress);
            }
        }

    }
}