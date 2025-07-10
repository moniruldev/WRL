using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;



namespace PG.Web.Admin
{
    public partial class AdminMain : BagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRestart_Click(object sender, EventArgs e)
        {
            //using (var sc = new System.ServiceProcess.ServiceController("IISAdmin"))
            //{
            //    sc.Stop();
            //    sc.Start();
            //}


            //HttpRuntime.UnloadAppDomain();
            if (PG.Core.Web.WebUtility.RestartWebApplication())
            {
                lblText.Text = "Restart Success";
            }
            else
            {
                lblText.Text = "Restart Error!";
            }

        }

        protected void btnClearCache_Click(object sender, EventArgs e)
        {
            AppCache.Clear();
            lblText.Text = "Cache Cleared.";

        }


    }
}
