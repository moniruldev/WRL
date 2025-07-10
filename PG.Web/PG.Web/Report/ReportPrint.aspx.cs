using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.IO;

namespace PG.Web.Report
{
    public partial class ReportPrint : BagePage
    {
        string roKey = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            //rsclientprint
            //CLSID:5554DCB0-700B-498D-9B58-4E40E5814405
            //CLSID:FA91DF8D-53AB-455D-AB20-F2F023E498D3

            //CLSID:41861299-EAB2-4DCC-986C-802AE12AC499


            roKey = this.GetPageQueryString("rk");
            if (roKey != string.Empty)
            {
                string frameSrc = string.Empty;

                string sk_FrameSource = "sk_" + this.roKey;
                if (Session[sk_FrameSource] != null)
                {
                    frameSrc = Convert.ToString(Session[sk_FrameSource]);
                }

                if (frameSrc != string.Empty)
                {
                    Response.Clear();
                    Response.Write(frameSrc);
                    //Response.End();
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
            }
        }
    }
}
