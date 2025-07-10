using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Text;

using PG.Core;
using PG.Core.Web;
using PG.Core.DBBase;
using PG.Core.Extentions;
using PG.Core.DBFilters;

using PG.BLLibrary.AccountingBL.GeneralLedgerBL;

namespace PG.Web
{
    public partial class Home : BagePage
    {

        public string SearchTestLink = PageLinks.SystemLinks.GetLink_SearchTest;


        public StringBuilder sbTree = new StringBuilder();
        public StringBuilder sbTree2 = new StringBuilder();

        //string sTab = "\t";
        //string sNewLine = "\r\n";


        protected override void OnPreInit(EventArgs e)
        {
            if (Globals.AppMasterPage != string.Empty)
            {
                this.MasterPageFile = Globals.AppMasterPage;
            }
            base.OnPreInit(e);
        }

        private void EmpSelection1_UserControlOKClicked(int id)
        {
            // ... do something when event is fired

            //PayRoll.Controls.EmpSelectionEventArgs y = (PayRoll.Controls.EmpSelectionEventArgs)e;

            int x = id;
        }



        protected void Page_Load(object sender, EventArgs e)
        {
           // Response.Write("UserID : " + this.UserID.ToString());

            lblWelcome.Text = "Welcome to " +  AppInfo.AppNameFull;

            if (!this.IsPostBack)
            {
               

            }




        }

        private void SetHyperLink()
        {

            //new button
            //string hLink = "javascript:tbopenSalInfo("+ this.PRSettingID.ToString() +")";
            //if (base.PageMode == PG.Core.Web.PageModeEnum.InTab)
            //{
            //    hLink = "javascript:tbopenSalInfo(" + this.PRSettingID.ToString() + ")";
            //    //this.btnSalaryInfo = string.Empty;
            //    this.btnSalaryInfo.Attributes.Add("onclick", hLink);
            //}
            //else
            //{
            //    hLink = "~/Master/EmpSalaryInfo.aspx?eid=" + this.PRSettingID.ToString();
            //    //this.btnAddNew.PostBackUrl = hLink;
            //    //this.btnAddNew.OnClientClick = string.Empty;
            //    this.btnSalaryInfo.Attributes.Add("onclick", hLink);
            //}

            
           
        }


       
       

        public string FromControl(int x)
        {
            return x.ToString();
        }

        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    int companyID = CompanyInfo.GetCompanyID();
        //    GLGroupBL.UpdateGLGroupClass(companyID);
        //    GLAccountBL.UpdateGLAccountClass(companyID);
        //}



    }
}
