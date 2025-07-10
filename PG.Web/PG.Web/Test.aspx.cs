using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;

using PG.DBClass.AccountingDC;
using PG.DBClass.AccountingDC.AccEnums;

using PG.Report;
using PG.Report.ReportClass;
using PG.Report.ReportEnums;
using PG.Report.ReportGen.AccountingRGN;

using iTextSharp.text.pdf;
using iTextSharp.text;


namespace PG.Web
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string rptKey = GenarateJournalPrint();

            string reportURL = PageLinks.ReportLinks.GetLink_ReportGetPDF;
            reportURL += "?rk=" + rptKey;

            //this.ifrReport.Attributes.Add("src", reportURL);

        }

        public string GenarateJournalPrint()
        {
            clsPrmLedger prmLedger = new clsPrmLedger();
            prmLedger.CompanyID = 1;
            prmLedger.JournalID = 23;

            prmLedger.JournalReportFormat = JournalReportFormatEnum.SingleVoucher;


            ReportOptions rptOption = new ReportOptions();

            //rptOption.ReportViewMode = this.ucPrintButton.DefaultViewMode;
            //rptOption.ReportOpenType = this.ucPrintButton.DefaultPrintAction;
            //rptOption.ReportExportType = this.ucPrintButton.DefaultExportType;

            AppInfo.SetAppInfoToReportOptions(rptOption);
            CompanyInfo.SetCompanyInfoToReportOptions(rptOption, this.Context);

            //AppInfo.SetAppInfoToReportOptions(rptParams);
            //CompanyInfo.SetCompanyInfoToReportOptions(rptParams, this.Context);
            AppReport rpt = JournalRGN.GenerateJournal(prmLedger, rptOption);
            string rptKey = AppReport.SetAppReportToSession(rpt, this.Context);
            //string rptKey = "";

            return rptKey;
        }

        protected string GetAutoPrintJs()
        {
            var script = new StringBuilder();
            script.Append("var pp = getPrintParams();");
            script.Append("pp.interactive= pp.constants.interactionLevel.full;");
            script.Append("print(pp);"); return script.ToString();



        //    PdfSharp.Pdf.PdfDictionary dictJS = new PdfSharp.Pdf.PdfDictionary();
        //    dictJS.Elements["/S"] = new PdfSharp.Pdf.PdfName("/JavaScript");
        //    //dictJS.Elements["/JS"] = new PdfSharp.Pdf.PdfStringObject(outputDocument, "print(true);");
        //    //dictJS.Elements["/JS"] = new PdfSharp.Pdf.PdfStringObject(outputDocument, "this.print({bUI: false, bSilent: true, bShrinkToFit: true});");
        //    //dictJS.Elements["/JS"] = new PdfSharp.Pdf.PdfStringObject(outputDocument, "var pp = this.getPrintParams(); pp.NumCopies = 2; pp.interactive = pp.constants.interactionLevel.automatic; this.print({printParams: pp});");
        //    dictJS.Elements["/JS"] = new PdfSharp.Pdf.PdfStringObject(outputDocument, JSScript);


        //    outputDocument.Internals.AddObject(dictJS);

        }
        protected void StreamPdf(Stream pdfSource)
        {
            var outputStream = new MemoryStream();
            var pdfReader = new PdfReader(pdfSource);
            var pdfStamper = new PdfStamper(pdfReader, outputStream);
            //Add the auto-print javascript
            var writer = pdfStamper.Writer;
            writer.AddJavaScript(GetAutoPrintJs());
            pdfStamper.Close();
            var content = outputStream.ToArray();
            outputStream.Close();
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(content);
            Response.End();
            outputStream.Close();
            outputStream.Dispose();
        }



        protected void Button1_Click(object sender, EventArgs e)
        {
            //this.TextBox1.Text = "ASDFASdf";
            //string path = Server.MapPath("~/");

            //PdfReader reader = new PdfReader();


            var doc1 = new Document(PageSize.A4);

            string path = "D:\\";
            PdfWriter writer = PdfWriter.GetInstance(doc1, new FileStream(path + "/Doc1.pdf", FileMode.Create));

            doc1.Open();
            //PdfAction jAction = PdfAction.JavaScript("this.print(true);\r", writer); //with print dialog

            PdfAction jAction = PdfAction.JavaScript("this.print(false);\r", writer);
            writer.AddJavaScript(jAction);



            doc1.Add(new Paragraph("My first PDF"));

            doc1.Close();

            

        }
    }
}