using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PG.BLLibrary.AccountingBL;
using PG.Core.Utility;
using PG.BLLibrary.AccountingBL.GeneralLedgerBL;
using PG.DBClass.AccountingDC.GeneralLedgerDC;

namespace PG.Web.Accounting.GeneralLedger
{
    public partial class JournalFull : BagePage
    {

        int CompanyID = 0;

        public string LoginSilentLink = PageLinks.SystemLinks.GetLink_LoginSilent;

        public string ReportGeneratePageLink = PageLinks.ReportLinks.GetLink_ReportGenerate;
        public string ReportViewPageLink = PageLinks.ReportLinks.GetLink_ReportView;

        public string GLAccountServiceLink = PageLinks.AccountingLinks.GetLink_GLAccount;
        public string GLGroupServiceLink = PageLinks.AccountingLinks.GetLink_GLGroup;
        public string AccRefServiceLink = PageLinks.AccountingLinks.GetLink_AccRef;
        public string InstrumentGetServiceLink = PageLinks.AccountingLinks.GetLink_InstrumentGet;
        public string InstrumentUpdateServiceLink = PageLinks.AccountingLinks.GetLink_InstrumentUpdate;

        public string GetJournalServiceLink = PageLinks.AccountingLinks.GetLink_GetJournal;
        public string GetJournalListServiceLink = PageLinks.AccountingLinks.GetLink_GetJournalList;
        public string UpdateJournalServiceLink = PageLinks.AccountingLinks.GetLink_UpdateJournal;


        protected override void OnPreInit(EventArgs e)
        {
            if (Globals.AppMasterPage != string.Empty)
            {
                this.MasterPageFile = Globals.AppMasterPage;
            }

            base.OnPreInit(e);
        }

        protected override void OnInit(EventArgs e)
        {
            //ScriptManager.GetCurrent(this).EnablePartialRendering = true;
            //Page.EnableEventValidation = false;

            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.CompanyID = CompanyInfo.GetCompanyID();
            this.hdnCompanyID.Value = this.CompanyID.ToString();

            if (!IsPostBack) // firsttime
            {
                FillCombo();
                int jrnlID = GetPageQueryInteger("id");
                hdnJournalID.Value = jrnlID > 0 ? jrnlID.ToString() : "0";

                int jrnlTypeID = this.GetPageQueryInteger("journaltypeid");
                if (jrnlTypeID > 0)
                {
                    ListItem lstItem = rblJournalType.Items.FindByValue(jrnlTypeID.ToString());
                    if (lstItem != null)
                    {
                        lstItem.Selected = true;
                    }
                    else
                    {
                        rblJournalType.SelectedValue = "1";
                    }
                }
                else
                {
                    rblJournalType.SelectedValue = "1";
                }
            }
            else
            {

            }
        }


        private void FillCombo()
        {
            FillJournalType();
            FillAccYear();
        }

        private void FillAccYear()
        {
            ddlAccYear.DataTextField = "AccYearName";
            ddlAccYear.DataValueField = "AccYearID";
            ddlAccYear.AppendDataBoundItems = true;
            ddlAccYear.DataSource = AccYearBL.GetAccYearList(this.CompanyID, this.DbContext);
            ddlAccYear.DataBind();
        }

        private void FillJournalType()
        {
            //rblJournalType.da

            this.rblJournalType.Items.Clear();
            List<dcJournalType> cList =  JournalTypeBL.GetJournalTypeList(this.CompanyID, this.DbContext);

            foreach (dcJournalType cObj in cList)
            {
                ListItem lstItem = new ListItem();
                lstItem.Value = cObj.JournalTypeID.ToString();
                lstItem.Text = cObj.JournalTypeName;
                lstItem.Attributes.Add("jtbackcolor","#8573");
                rblJournalType.Items.Add(lstItem);

            }






            //rblJournalType.DataTextField = "JournalTypeName";
            //rblJournalType.DataValueField = "JournalTypeID";
            //rblJournalType.AppendDataBoundItems = true;
            //rblJournalType.DataSource = JournalTypeBL.GetJournalTypeList(this.CompanyID, this.DbContext);
            //rblJournalType.DataBind();
        }


        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            FillCombo();
        }
    }
}