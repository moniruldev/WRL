using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using PG.Report.ReportEnums;

namespace PG.Report
{
    [Serializable]
    public class ReportOptions
    {

        public string ReportKey = string.Empty;

        public string CompanyName = string.Empty;
        public string UserName = string.Empty;
        public string CompanyAddress = string.Empty;


        public string AppCompanyName = "";
        public string AppPoweredBy = "";



        public string ReportTitle = string.Empty;
        public string ReportTitleSub = string.Empty;

        public bool IsReportCriteria = false;
        public string ReportCriteriaString = string.Empty;

        public bool IsReportCriteria2 = false;
        public string ReportCriteriaString2 = string.Empty;


        public ReportOpenTypeEnum ReportOpenType = ReportOpenTypeEnum.Preview;
        public ReportViewModeEnum ReportViewMode = ReportViewModeEnum.InThisTab;
        public ReportExportTypeEnum ReportExportType = ReportExportTypeEnum.PDF;


        public string ReportExportFileName = string.Empty;

        public string ReportEmbeddedResource = string.Empty;
        public string LocalReportPath = string.Empty;

        public int ReportZoomPercent = 100;

        public bool SetPageSize = false;
        
        public decimal PageHeight = 0;
        public decimal PageWidth = 0;


        public bool SetPageMargin = false;
        public decimal PageMarginLeft = 0;
        public decimal PageMarginTop = 0;
        public decimal PageMarginRight = 0;
        public decimal PageMarginBottom = 0;

        public bool SetPageOrientation = false;
        public bool IsPageLandscape = false;


        public bool IsAutoPrint = false;
        public bool ShowPrintDialog = true;
        public int PrintNumCopies = 0;

        public bool DisbalePDFPrint = false;


        public ReportOptions()
        {
        }

        public ReportOptions(ReportOpenTypeEnum pReportOpenType, ReportViewModeEnum pReportViewMode)
        {
            this.ReportOpenType = pReportOpenType;
            this.ReportViewMode = pReportViewMode;
        }

        public ReportOptions(ReportOpenTypeEnum pReportOpenType, ReportViewModeEnum pReportViewMode, ReportExportTypeEnum pReportExportType )
        {
            this.ReportOpenType = pReportOpenType;
            this.ReportViewMode = pReportViewMode;
            this.ReportExportType = pReportExportType;
        }



        public int Report_Percent { get; set; }

        public string ReportCaption { get; set; }
    }
}
