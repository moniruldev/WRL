using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PG.Core.License;

namespace PayRoll
{
    public partial class License : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtMachineID.Text = AppLicense.GetSystemCPUID();
                txtIPAddress.Text = AppLicense.GetSystemIPAddress();
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            AppLicense.IsLicenseReset = true;
            lblMsg.Text = "Reset Done";
        }
    }
}
