using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Text;
using System.Web.SessionState;
using System.IO;

using PG.Core.Web;
using PG.Report;


namespace PG.Web.Report
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class GetReportPrintData : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            string reportKey =  WebUtility.GetQueryString("rk",context);
            int startPage = WebUtility.GetQueryStringInteger("rc:StartPage", context);

            string strExcel = string.Empty;
            AppReport appReport = null;
            
            //rs:PersistStreams=True
            //rc:StartPage=1
            //rc:EndPage=65535


            //rs:GetNextStream=True
            //rc:StartPage=2
            //rc:EndPage=2



            if (context.Session[reportKey] != null)
            {
                appReport = context.Session[reportKey] as AppReport;
            }


            if (appReport != null)
            {

                if (appReport.ReportStreams != null && appReport.ReportStreams.Count != 0)
                {

                    context.Response.Clear();
                    context.Response.BufferOutput = true;

                    if (startPage > appReport.ReportStreams.Count)
                    {
                        //byte[] bufEmpty = new byte[0];
                        //context.Response.OutputStream.Write(bufEmpty, 0, 0);
                        
                        context.Response.End();
                        return;
                    }

                    int pageIndex = startPage - 1;
                    Stream s = appReport.ReportStreams[pageIndex];

                    BinaryReader reader = new BinaryReader(s);
                    byte[] buf = new byte[256];
                    int count = 0;
                    do
                    {
                        count = reader.Read(buf,0,256);
                        if (count > 0)
                        {
                            context.Response.OutputStream.Write(buf, 0, count);
                        }
                        else
                        {
                            context.Response.End();
                            return;
                        }


                    } while (count > 0);
                    context.Response.Flush();
                    context.Response.End();


                    //for empty buffer
                    //byte[] bufEmtpy = new byte[0];
                    //context.Response.OutputStream.Write(bufEmtpy,0,0);

                }


            }


            //string attachment = "attachment; filename=employee_data.xls";
            //context.Response.ClearContent();
            ////context.Response.Buffer = true;
            //context.Response.AddHeader("content-disposition", attachment);
            //context.Response.ContentType = "application/vnd.ms-excel";
            ////context.Response.ContentType = "application/ms-excel";
            ////context.Response.ContentType = "text/csv";
            //context.Response.Write(strExcel.ToString());
            //context.ApplicationInstance.CompleteRequest();


            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }




    }
}
