using System;
using System.Collections;
using System.Collections.Generic;
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
using PG.DBClass.InventoryDC;
using PG.BLLibrary.InventoryBL;
using PG.Core.Utility;
using PG.DBClass.HMSDC;
using PG.BLLibrary.HMSBL;
using PG.DBClass.WRELDC;
using PG.BLLibrary.WRElBL;
using PG.Report;
using PG.Report.ReportEnums;
using PG.Report.ReportGen.WRELRGN;
using PG.DBClass.SecurityDC;

namespace PG.Web.WREL
{
    public partial class QuickLinks : BagePage
    {
        public string ReportViewPageLink = PageLinks.ReportLinks.GetLink_ReportView;
        public string ReportViewPDFPageLink = PageLinks.ReportLinks.GetLink_ReportViewPDF;
        public string ReportPrintPageLink = PageLinks.ReportLinks.GetLink_ReportPrint;
        public string ReportPDFPageLink = PageLinks.ReportLinks.GetLink_ReportPDF;

        int CompanyID = 0;
        public string ItemListServiceLink = PageLinks.InventoryLink.GetLink_ItemList;
        public string ItemGroupListServiceLink = PageLinks.InventoryLink.GetLink_ItemGroupList;

        ReportOpenTypeEnum ReportOpenType = ReportOpenTypeEnum.Preview;
        protected override void OnPreInit(EventArgs e)
        {
            if (Globals.AppMasterPage != string.Empty)
            {
                this.MasterPageFile = Globals.AppMasterPage;
            }
            base.OnPreInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.CompanyID = CompanyInfo.GetCompanyID();

            if (!IsPostBack)
            {
               
              
            }
    
        }


     



    }
}
