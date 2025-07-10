using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PG.Core.Web;

using PG.Web.Systems;


namespace PG.Web.PageLinks
{
    public class ReportLinks
    {
        public static string GetLink_ExcelData
        {
            get { return WebUtility.GetAbsoluteUrl("~/Report/GetExcelData.ashx"); }
        }

        public static string GetLink_ReportView
        {
            get { return WebUtility.GetAbsoluteUrl("~/Report/ReportView.aspx"); }
        }

        public static string GetLink_ReportPrint
        {
            get { return WebUtility.GetAbsoluteUrl("~/Report/ReportPrint.aspx"); }
        }

        public static string GetLink_ReportViewPDF
        {
            get { return WebUtility.GetAbsoluteUrl("~/Report/ReportViewPDF.aspx"); }
        }

        public static string GetLink_ReportPDF
        {
            get { return WebUtility.GetAbsoluteUrl("~/Report/ReportPDF.aspx"); }
        }


        public static string GetLink_ReportPrintData
        {
            get { return WebUtility.GetAbsoluteUrl("~/Report/GetReportPrintData.ashx"); }
        }

        public static string GetLink_ReportGenerate
        {
            get { return WebUtility.GetAbsoluteUrl("~/Report/GenerateReport.ashx"); }
        }




        public static string GetLink_ReportGetPDF
        {
            get { return WebUtility.GetAbsoluteUrl("~/Report/GetReportPDF.ashx"); }
        }

    }
}