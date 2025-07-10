using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;

using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;

using System.Globalization;

using Microsoft.Reporting.WebForms;

using iTextSharp.text.pdf;
using iTextSharp.text;

using PG.Core.Utility;

using PG.DBClass.AccountingDC;
using PG.Report.ReportEnums;
using PG.Report.ReportClass;

namespace PG.Report
{
    [Serializable]
    public class AppReport
    {
        [Serializable]
        public class DataSource
        {
            public string DataSourceName = string.Empty;
            public object DataSourceValue = null;

            public DataSource()
            {

            }
            public DataSource(string pDataSourceName, object pDataSourceValue )
            {
                this.DataSourceName = pDataSourceName;
                this.DataSourceValue = pDataSourceValue;
            }
        }

        public ReportIDEnum ReportID = ReportIDEnum.None;
        public ReportOptions ReportOptions = new ReportOptions();
        public int PrintCopies = 0;

        ///public string pQueryString;
        public bool IsExcelExport = false;
        public string ExcelFileName = "export";
        public Byte[] ExcelData = null;

        //public bool IsReportCriteria = false;
        //public string ReportCriteria;


        public string ReportEmbeddedResource = string.Empty;
        public string LocalReportPath = string.Empty;

        public List<DataSource> DataSources = new List<DataSource>();
        public List<ReportParameter> Paremeters = new List<ReportParameter>();

        public IList<Stream> ReportStreams;

        public bool IsError = false;
        public string ErrText = "";
        public string ErrDesc = "";
        public string ErrHeader = "";

        public void AddCriteriaParameter(string pParameterValue)
        {
            AddParameter("prmCriteriaString", pParameterValue);
            
        }
        public void AddParameter(string pParameterName, string pParameterValue)
        {
            if (this.Paremeters == null)
            {
                this.Paremeters = new List<ReportParameter>();
            }
            this.Paremeters.Add(new ReportParameter(pParameterName, pParameterValue));
        }

        public static LocalReport GetLocalReport(AppReport pAppReport)
        {
            LocalReport rpt = new LocalReport();
            rpt.ReportPath = pAppReport.LocalReportPath;


            if (pAppReport.Paremeters != null)
            {
                //try
                {
                    rpt.SetParameters(pAppReport.Paremeters.ToArray());
                }
                //catch
                {
                }
                //foreach (ReportParameter param in pAppReport.Paremeters)
                //{
                //    rpt.SetParameters(par

                //}
            }

            foreach (AppReport.DataSource src in pAppReport.DataSources)
            {
                ReportDataSource rdS = new ReportDataSource();
                rdS.Name = src.DataSourceName;
                rdS.Value = src.DataSourceValue;
                rpt.DataSources.Add(rdS);
            }
            //if (pAppReport.IsReportCriteria)
            //{
            //    try
            //    {
            //        ReportParameter p = new ReportParameter("prmCriteriaString", pAppReport.ReportCriteria);
            //        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { p });
            //    }
            //    catch { }
            //}

            



            return rpt;
        }


        public static void SetLocalReport(LocalReport localReport, AppReport pAppReport)
        {
            //LocalReport rpt = new LocalReport();
            //localReport.AddTrustedCodeModuleInCurrentAppDomain("ClassLibrary1");

            localReport.ReportEmbeddedResource = pAppReport.ReportEmbeddedResource;
            localReport.ReportPath = pAppReport.LocalReportPath;
            //localReport.AddTrustedCodeModuleInCurrentAppDomain("ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
            //localReport.AddTrustedCodeModuleInCurrentAppDomain("PG.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");

            PermissionSet permissions = new PermissionSet(PermissionState.None);
            permissions.AddPermission(new FileIOPermission(PermissionState.Unrestricted));
            permissions.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
            localReport.SetBasePermissionsForSandboxAppDomain(permissions);
            Assembly asm = Assembly.Load("PG.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
            AssemblyName asm_name = asm.GetName();
            localReport.AddFullTrustModuleInSandboxAppDomain(new StrongName(new StrongNamePublicKeyBlob(asm_name.GetPublicKeyToken()), asm_name.Name, asm_name.Version));

            //localReport.AddTrustedCodeModuleInCurrentAppDomain();

            //localReport.AddTrustedCodeModuleInCurrentAppDomain();

           


            if (pAppReport.Paremeters != null)
            {
                try
                {
                    if (pAppReport.Paremeters.Count > 0)
                    {
                        ReportParameterInfoCollection paramColl = localReport.GetParameters();
                        //localReport.SetParameters(pAppReport.Paremeters.ToArray());
                        foreach (ReportParameter rptParam in pAppReport.Paremeters)
                        {
                            if (paramColl.Count(c => c.Name.ToUpper() == rptParam.Name.ToUpper()) > 0)
                            {

                                localReport.SetParameters(rptParam);
                            }
                        }
                    }
                }
                catch
                {
                }
                //foreach (ReportParameter param in pAppReport.Paremeters)
                //{
                //    rpt.SetParameters(par

                //}
            }

            foreach (AppReport.DataSource src in pAppReport.DataSources)
            {
                ReportDataSource rdS = new ReportDataSource();
                rdS.Name = src.DataSourceName;
                rdS.Value = src.DataSourceValue;
                localReport.DataSources.Add(rdS);
            }

            if (pAppReport.ReportOptions.ReportOpenType == ReportOpenTypeEnum.Print)
            {
                pAppReport.GenerateReportStreams(localReport);
            }


            if (pAppReport.ReportOptions.IsReportCriteria)
            {
                try
                {
                    ReportParameter p = new ReportParameter("prmCriteriaString", pAppReport.ReportOptions.ReportCriteriaString);
                    localReport.SetParameters(new ReportParameter[] { p });

                    
                }
                catch { }
            }

            if (pAppReport.ReportOptions.IsReportCriteria2)
            {
                try
                {
                    ReportParameter p = new ReportParameter("prmCriteriaString2", pAppReport.ReportOptions.ReportCriteriaString2);
                    localReport.SetParameters(new ReportParameter[] { p });
                }
                catch { }
            }


        }

        private void GenerateReportStreams(LocalReport report)
        {
            //string deviceInfo =

            //    "<DeviceInfo>" +
            //    " <OutputFormat>EMF</OutputFormat>" +

            //    " <PageWidth>4.0in</PageWidth>" +
            //    " <PageHeight>2.0in</PageHeight>" +

            //    " <MarginTop>0.00in</MarginTop>" +
            //    " <MarginLeft>0.00in</MarginLeft>" +

            //    " <MarginRight>0.00in</MarginRight>" +
            //    " <MarginBottom>0.00in</MarginBottom>" +

            //    "</DeviceInfo>";


            //ReportPageSettings pageSettngs = report.GetDefaultPageSettings();

            //int pageHeight = pageSettngs.PaperSize.Height;
            //int pageWidth = pageSettngs.PaperSize.Width;
            
            //int marginLeft = pageSettngs.Margins.Left;
            //int marginTop = pageSettngs.Margins.Top;

            //int marginBottom = pageSettngs.Margins.Bottom;
            //int marginRight = pageSettngs.Margins.Right;



            //string deviceInfo =

            //   "<DeviceInfo>" +
            //   " <OutputFormat>EMF</OutputFormat>" +

            //   " <PageWidth>8.27in</PageWidth>" +
            //   " <PageHeight>11.69in</PageHeight>" +

            //   " <MarginTop>0.00in</MarginTop>" +
            //   " <MarginLeft>0.00in</MarginLeft>" +

            //   " <MarginRight>0.00in</MarginRight>" +
            //   " <MarginBottom>0.00in</MarginBottom>" +

            //   "</DeviceInfo>";


            string deviceInfo =

               "<DeviceInfo>" +
               " <OutputFormat>EMF</OutputFormat>" +

               "</DeviceInfo>";

                Warning[] warnings; 
                this.ReportStreams = new List<Stream>();
                report.Render("Image", deviceInfo, CreateStream, out warnings);
                foreach (Stream stream in this.ReportStreams)
                {
                    stream.Position = 0;
                }
        }

        public Stream CreateStream(string name, string fileNameExtension, System.Text.Encoding encoding, string mimeType, bool willSeek)
        {

            //string CurrentDrive;
            //CurrentDrive = Application.StartupPath.ToString(); 
            //Stream stream = new FileStream("C:\\Labels\\" + name + "." + fileNameExtension, FileMode.Create);
            Stream stream = new MemoryStream();
            this.ReportStreams.Add(stream);
            return stream;

        }

        private static string ToInches(int hundrethsOfInch)
        {
            double inches = hundrethsOfInch / 100.0;
            return inches.ToString(CultureInfo.InvariantCulture) + "in";
        }

        public static MemoryStream GetReportPDFMemStream(LocalReport report)
        {

            //string reportType = strType;
            string reportType = "PDF";
            //string reportType = "excel";

            string mimeType;
            string encoding;
            string fileNameExtention;


            Warning[] warnings;
            string[] streams;
            byte[] renderBytes;

            renderBytes = report.Render(reportType, null, out mimeType, out encoding, out fileNameExtention, out streams, out warnings);

            //MemoryStream ms = new MemoryStream(data);
            MemoryStream ms = new MemoryStream(renderBytes);

            return ms;
        }

        public static string GetAutoPrintJs()
        {
            return GetAutoPrintJs(true, 0);
        }
        public static string GetAutoPrintJs(bool bPrintDialog, int pNumCopies)
        {
            var script = new StringBuilder();
            script.Append("var pp = getPrintParams();");

            if (bPrintDialog)
            {
                script.Append("pp.interactive= pp.constants.interactionLevel.full;");  //script.Append("pp.interactive= pp.constants.interactionLevel.silent;");
            }
            else
            {
                script.Append("pp.interactive= pp.constants.interactionLevel.silent;");
            }

            if (pNumCopies > 0)
            {
                script.Append(string.Format("pp.NumCopies = {0};",pNumCopies));
            }

            //script.Append("pp.printerName = 'hp officejet d series';");
            ////if (global.defaultPrinter === undefined) {
            ////    global.defaultPrinter = getPrintParams().printerName;
            ////}



            //script.Append("pp.pageHandling = pp.constants.handling.none;");     //none, fit, shrink, tileAll, tileLarge, nUp, booklet

            ////script.Append("pp.DuplexType = pp.constants.duplexTypes.Simplex;"); //DuplexFlipShortEdge,DuplexFlipShortEdge 
            ////script.Append("pp.NumCopies = 2;");

            //script.Append("fv = pp.constants.flagValues;");
            //script.Append("pp.flags |= fv.setPageSize;");
            //script.Append("pp.flags |= (fv.suppressCenter | fv.suppressRotate);");
            
            //script.Append("print(pp);"); 
            script.Append("this.print(pp);\r"); 
            return script.ToString();



            //    PdfSharp.Pdf.PdfDictionary dictJS = new PdfSharp.Pdf.PdfDictionary();
            //    dictJS.Elements["/S"] = new PdfSharp.Pdf.PdfName("/JavaScript");
            //    //dictJS.Elements["/JS"] = new PdfSharp.Pdf.PdfStringObject(outputDocument, "print(true);");
            //    //dictJS.Elements["/JS"] = new PdfSharp.Pdf.PdfStringObject(outputDocument, "this.print({bUI: false, bSilent: true, bShrinkToFit: true});");
            //    //dictJS.Elements["/JS"] = new PdfSharp.Pdf.PdfStringObject(outputDocument, "var pp = this.getPrintParams(); pp.NumCopies = 2; pp.interactive = pp.constants.interactionLevel.automatic; this.print({printParams: pp});");
            //    dictJS.Elements["/JS"] = new PdfSharp.Pdf.PdfStringObject(outputDocument, JSScript);


            //    outputDocument.Internals.AddObject(dictJS);

        }

        public static MemoryStream AddPrintJsToReportStream(MemoryStream pdfSource)
        {
            return AddPrintJsToReportStream(pdfSource, new ReportOptions());
        }

        //public static MemoryStream AddPrintJsToReportStream(MemoryStream pdfSource, ReportOptions rptOptions, bool pAutoPrint, bool pPrintDialog, int pNumCopies)
        //{
        //}

        public static MemoryStream AddPrintJsToReportStream(MemoryStream pdfSource, ReportOptions rptOptions)
        {
            MemoryStream outputStream = new MemoryStream();

            var pdfReader = new PdfReader(pdfSource);
            var pdfStamper = new PdfStamper(pdfReader, outputStream);

            var pdfWriter = pdfStamper.Writer;

            pdfStamper.AddViewerPreference(PdfName.PRINTSCALING, PdfName.NONE);   //PdfName.APPDEFAULT
            pdfStamper.AddViewerPreference(PdfName.PICKTRAYBYPDFSIZE, PdfBoolean.PDFTRUE);

            //pdfStamper.AddViewerPreference(PdfName.DUPLEX, PdfName.SIMPLEX);
            //pdfWriter.SetViewerPreferences(PdfWriter.CenterWindow | PdfWriter.FitWindow);
            if (rptOptions.PrintNumCopies > 0)
            {
                pdfStamper.AddViewerPreference(PdfName.NUMCOPIES, new PdfNumber(rptOptions.PrintNumCopies));
            }


            //pdfWriter.AddViewerPreference(
            if (rptOptions.IsAutoPrint)
            {
                //string strJS = string.Format("this.print({0});\r", pPrintDialog ? "true" : "false");
                string strJS = string.Format("this.print({0});", rptOptions.ShowPrintDialog ? "true" : "false");
                
                //PdfAction jAction = PdfAction.JavaScript("this.print(true);\r", pdfWriter);
                PdfAction jAction = PdfAction.JavaScript(strJS, pdfWriter);
                pdfWriter.AddJavaScript(jAction);

                //this.print({bUI: false, bSilent: true, bShrinkToFit: true});
            }
            else
            {
                if (rptOptions.DisbalePDFPrint)
                {
                    System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
                    pdfWriter.SetEncryption(null, encoding.GetBytes("12345678"), PdfWriter.ALLOW_COPY, PdfWriter.STRENGTH40BITS);
                }
            }

            pdfStamper.Close();

            return outputStream;
        }


        //public static MemoryStream DisablePDFPrintJsToReportStream(MemoryStream pdfSource)
        //{
        //    MemoryStream outputStream = new MemoryStream();

        //    var pdfReader = new PdfReader(pdfSource);
        //    var pdfStamper = new PdfStamper(pdfReader, outputStream);

        //    var pdfWriter = pdfStamper.Writer;

          
        //    //pdfStamper.AddViewerPreference(PdfName.PRINTSCALING, PdfName.NONE);   //PdfName.APPDEFAULT
        //    //pdfStamper.AddViewerPreference(PdfName.PICKTRAYBYPDFSIZE, PdfBoolean.PDFTRUE);


        //    System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
        //    pdfWriter.SetEncryption(null, encoding.GetBytes("12345678"), PdfWriter.ALLOW_COPY, PdfWriter.STRENGTH40BITS);


        //    //if (pNumCopies > 0)
        //    //{
        //    //    pdfStamper.AddViewerPreference(PdfName.NUMCOPIES, new PdfNumber(pNumCopies));
        //    //}


        //    ////pdfWriter.AddViewerPreference(
        //    //if (pAutoPrint)
        //    //{

        //    //    string strJS = string.Format("this.print({0});", pPrintDialog ? "true" : "false");

        //    //    //PdfAction jAction = PdfAction.JavaScript("this.print(true);\r", pdfWriter);
        //    //    PdfAction jAction = PdfAction.JavaScript(strJS, pdfWriter);
        //    //    pdfWriter.AddJavaScript(jAction);

        //    //}


        //    pdfStamper.Close();


        //    return outputStream;
        //}


        public static MemoryStream GetEmptyPDF()
        {
            return GetEmptyPDF(string.Empty);
        }
        public static MemoryStream GetEmptyPDF(string strMsg)
        {
            MemoryStream outputStream = new MemoryStream();
            var doc1 = new Document(PageSize.A4);
            PdfWriter writer = PdfWriter.GetInstance(doc1, outputStream);

            if (strMsg != string.Empty)
            {
                doc1.Open();
                doc1.Add(new Paragraph(strMsg));
                doc1.Close();
            }

            return outputStream;
        }


        public static byte[] GetReportPDF(AppReport rpt)
        {
            LocalReport localReport = new LocalReport();
            AppReport.SetLocalReport(localReport, rpt);

            string exportFileName = "report";
            if (rpt.ReportOptions.ReportExportFileName != string.Empty)
            {
                exportFileName = rpt.ReportOptions.ReportExportFileName;
            }

            string exportType = "PDF";

            
            string mimeType;
            string encoding;
            string fileNameExtention;

            //string deviceInfo =
            //   "<DeviceInfo>" +
            //   "  <OutputFormat>PDF</OutputFormat>" +
            //   "  <PageWidth>8.5in</PageWidth>" +
            //   "  <PageHeight>11in</PageHeight>" +
            //   "  <MarginTop>0.25in</MarginTop>" +
            //   "  <MarginLeft>0.25in</MarginLeft>" +
            //   "  <MarginRight>0.25in</MarginRight>" +
            //   "  <MarginBottom>0.25in</MarginBottom>" +
            //   "</DeviceInfo>";


            Warning[] warnings;
            string[] streams;
            byte[] renderBytes;

            renderBytes = localReport.Render(exportType, null, out mimeType, out encoding, out fileNameExtention, out streams, out warnings);

            MemoryStream ms = new MemoryStream(renderBytes);
            rpt.ReportOptions.IsAutoPrint = rpt.ReportOptions.ReportOpenType == ReportOpenTypeEnum.Print ? true : false;
            ms = AppReport.AddPrintJsToReportStream(ms, rpt.ReportOptions);
            renderBytes = ms.ToArray();

            return renderBytes;
        }


        public static string GetValueFromParams(string pKey, NameValueCollection pReportParams)
        {
            string strValue = string.Empty;
            if (pReportParams != null)
            {
                try
                {
                    strValue = pReportParams[pKey];
                }
                catch { };
            }
            return strValue;
        }


        public static void SetReportOptionsFromParams(ReportOptions rptOptions, NameValueCollection pReportParams)
        {

            int rptOpenType = Conversion.StringToInt(GetValueFromParams("rptopentype", pReportParams));
            int rptViewMode = Conversion.StringToInt(GetValueFromParams("rptviewtype", pReportParams));
            string exportType = Conversion.NullToEmpty( GetValueFromParams("exporttype", pReportParams));


            string appCompanyName = Conversion.NullToEmpty(GetValueFromParams("AppCompanyName", pReportParams));
            string appPoweredBy = Conversion.NullToEmpty(GetValueFromParams("AppPoweredBy", pReportParams));


            string companyName = Conversion.NullToEmpty(GetValueFromParams("CompanyName", pReportParams));
            string companyAddress = Conversion.NullToEmpty(GetValueFromParams("CompanyAddress", pReportParams)); 


            //rptOpenType= 1=view,2=print,3=export

           
            rptOptions.ReportOpenType = (ReportOpenTypeEnum)rptOpenType;
            rptOptions.ReportViewMode = (ReportViewModeEnum)rptViewMode;

            switch (exportType.ToLower())
            {
                case "pdf":
                    rptOptions.ReportExportType = ReportExportTypeEnum.PDF;
                    break;
                case "excel" :
                    rptOptions.ReportExportType = ReportExportTypeEnum.Excel;
                    break;
                case "word":
                    rptOptions.ReportExportType = ReportExportTypeEnum.WORD;
                    break;
                default:
                    rptOptions.ReportExportType = ReportExportTypeEnum.PDF;
                    break;
            }


            rptOptions.AppCompanyName = appCompanyName;
            rptOptions.AppPoweredBy = appPoweredBy;

            rptOptions.CompanyName = companyName;
            rptOptions.CompanyAddress = companyAddress;

        }


        public static string SetAppReportToSession(AppReport pAppReport)
        {
            return SetAppReportToSession(pAppReport, HttpContext.Current);
        }
        public static string SetAppReportToSession(AppReport pAppReport, HttpContext context)
        {
            string sessionKey = ReportGlobals.ReportSessionKeyPrefix +  ((int)pAppReport.ReportID).ToString();
            context.Session[sessionKey] = pAppReport;
            return sessionKey;
        }

        public static void RemoveAppReportFromSession(string pSessionKey)
        {
            RemoveAppReportFromSession(pSessionKey, HttpContext.Current);
        }

        public static void RemoveAppReportFromSession(string pSessionKey, HttpContext context)
        {
            if (context.Session[pSessionKey] != null)
            {
                context.Session[pSessionKey] = null;
            }
        }

        //public static string GenerateReport_PaymentRequisition(NameValueCollection pReportParams)
        //{
        //    return GenerateReport_PaymentRequisition(pReportParams, HttpContext.Current);
        //}


        //public static string GenerateReport_PaymentRequisition(NameValueCollection pReportParams, HttpContext context)
        //{
        //    string rptKey = string.Empty;

        //    //KeyValuePair<string, object> data = new KeyValuePair<string, object>();

        //    int payReqID = PG.Core.Utility.Conversion.StringToInt(GetValueFromParams("PaymentReqID",pReportParams));

        //    if (payReqID <= 0)
        //    {
        //        return rptKey;
        //    }

        //    string crtString = string.Empty;

        //    //if (dBlock == 0 | dBlock == 2)
        //    //{
        //    //    bool pRet = AppSecurity.CheckObjectPermissionByUserID(this.LoginUserID, AppObjectEnum.FrmOpt_BlockedEmployee, PermissionEnum.Read);
        //    //    if (!pRet)
        //    //    {
        //    //        AppMessage appMsg = new AppMessage();
        //    //        appMsg.MessageType = MessageTypeEnum.Permission;
        //    //        appMsg.RemoveMessageOnRead = true;
        //    //        appMsg.MessageString = "You don't have permission for blocked employee!";
        //    //        appMsg.ShowBackButton = true;
        //    //        Globals.ShowMessagePage(appMsg);
        //    //        //MMS.Globals.RemoveStatusMessage();
        //    //        //MMS.Globals.ShowMessagePage(MMS.MessageTypeEnum.Permission, "You don't have read permission for this object", "");
        //    //    }
        //    //}

        //    AppReport rpt = new AppReport();

        //    SetAppReportPropFromParams(rpt, pReportParams);



        //    //rpt.AddCriteriaParameter(GetDateParamString(fromDate, toDate));
        //    //rpt.AddParameter("prmCompanyNames", GetCompanyNames());


        //    rpt.LocalReportPath = @"Report\Payment\rptPaymentRequisition.rdlc";
        //    List<DBClass.Report.Payment.rcPaymentRequisition> rList = BLLibrary.ReportBL.Payment.PaymentRequitionReportBL.GetPaymentRequistion(payReqID);

        //    rpt.DataSources.Add(new AppReport.DataSource("rcPaymentRequisition", rList));
        //    rpt.DataSources.Add(new AppReport.DataSource("rcPaymentRequisitionDet", rList[0].PaymentReqDetails));
        //    rpt.DataSources.Add(new AppReport.DataSource("rcPaymentRequisitionAdjust", rList[0].PaymentReqAdjust));
        //    rpt.DataSources.Add(new AppReport.DataSource("rcPaymentRequisitionSchedule", rList[0].PaymentScheduleDetails));



        //    rptKey = "rk_1630";
        //    context.Session[rptKey] = rpt;
            

        //    return rptKey;
        //}
    }
}
