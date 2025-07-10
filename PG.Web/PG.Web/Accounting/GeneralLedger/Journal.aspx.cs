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
using System.Collections.Specialized;
using System.Web.Script.Serialization;

using PG.Core;
using PG.Core.Utility;
using PG.Core.Web;
using PG.Core.DBBase;
using PG.Core.Extentions;
using PG.Core.DBFilters;

using PG.DBClass.AccountingDC;
using PG.DBClass.AccountingDC.AccEnums;
using PG.DBClass.AccountingDC.GeneralLedgerDC;
using PG.DBClass.SecurityDC;
using PG.BLLibrary.AccountingBL;
using PG.BLLibrary.AccountingBL.GeneralLedgerBL;

using PG.Report;
using PG.Report.ReportClass;
using PG.Report.ReportEnums;
using PG.Report.ReportGen.AccountingRGN;
using PG.Core.DBValidation;
using PG.BLLibrary.OrganizationBL;
using PG.DBClass.OrganiztionDC;


namespace PG.Web.Accounting.GeneralLedger
{
    public partial class Journal : BagePage
    {
        
        
        int CompanyID = 0;
        int JournalID = 0;
        string ViewStateKey = "JournalID";
        string ViewStateKeyPrev = "JournalID_Prev";
        string saveMsg = string.Empty;

        bool IsPosted = false;

        dcAccSettings AccSettings = null;
        List<dcAccRefSettings> AccRefSettingsList = null;
        //dcInstrumentSettings InstrumentSettings = null;

        dcJournalType JournalType = null;


        bool IsAutoPrint = false;

        //string DateParse = "dd-MMM-yyyy";

        //public string ProjectExpenseSummaryPageLink = PageLinks.ReportLinks.GetLink_ProjectExpenseSummary;
        public string ReportGeneratePageLink = PageLinks.ReportLinks.GetLink_ReportGenerate;
        public string ReportViewPageLink = PageLinks.ReportLinks.GetLink_ReportView;

        public string GetLocationServiceLink = PageLinks.OrganizationLinks.GetLink_Location;

        public string GLAccountServiceLink = PageLinks.AccountingLinks.GetLink_GLAccount;
        public string GLGroupServiceLink = PageLinks.AccountingLinks.GetLink_GLGroup;
        public string AccRefServiceLink = PageLinks.AccountingLinks.GetLink_AccRef;
        public string AccRefCategoryServiceLink = PageLinks.AccountingLinks.GetLink_AccRefCategory;
        public string AccRefSettingsServiceLink = PageLinks.AccountingLinks.GetLink_AccRefSettings;

        public string InstrumentGetServiceLink = PageLinks.AccountingLinks.GetLink_InstrumentGet;
        public string InstrumentUpdateServiceLink = PageLinks.AccountingLinks.GetLink_InstrumentUpdate;


        public string ValidateJournalServiceLink = PageLinks.AccountingLinks.GetLink_ValidateJournal;


        List<dcGLAccount> listGLAccounts = null;

        dcJournal journalData = null; 
        List<dcJournalDet> listDetails = new List<dcJournalDet>();
        List<dcJournalDetRef> listDetailsRef = new List<dcJournalDetRef>();
        List<dcJournalDetIns> listDetailsIns = new List<dcJournalDetIns>();



        private class jsonDetailsRef
        {
           
            public int linkid { get; set; }
            public int journaldetrefid { get; set; }
            public int journaldetid { get; set; }

            public int id { get; set; }
            public string code { get; set; }
            public string name { get; set; }
            public int typeid { get; set; }
            public string typecode { get; set; }
            public string typename { get; set; }
            public int categoryid { get; set; }
            public string categorycode { get; set; }
            public string categoryname { get; set; }

            public int drcr { get; set; }
            //public decimal debitamt { get; set; }
            //public decimal creditamt { get; set; }
            public decimal amt { get; set; }

            public string accrefno { get; set; }
            public string accrefremarks { get; set; }

            public int _recordstateint { get; set; }
        }

        private class jsonDetailsIns
        {
            public int linkid { get; set; }
            public int journaldetinsid { get; set; }
            public int journaldetid { get; set; }

            public int instypeid { get; set; }
            public string instypecode { get; set; }
            public string instypename { get; set; }

            public int insmodeid { get; set; }
            public int inslinktypeid { get; set; }

            public int insid { get; set; }
            public string insno { get; set; }
            public string insdate { get; set; } 
            public decimal insamt { get; set; }

            public string issuename { get; set; }
            public string bankname { get; set; }
            public string branchname { get; set; }
            public string bankbranchname { get; set; }

            public int drcr { get; set; }
            //public decimal debitamt { get; set; }
            //public decimal creditamt { get; set; }
            //public decimal tranamt { get; set; }

            public decimal amt { get; set; }
            

            public string remarks { get; set; }

            public int insstatusid { get; set; }
            public string insstatusname { get; set; }

            public int _recordstateint { get; set; }
        }



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
            this.AppObjectID = AppObjectEnum.Frm_Journal;
            this.CompanyID = CompanyInfo.GetCompanyID();
            this.hdnCompanyID.Value = this.CompanyID.ToString();
            AccSettings = AccSettingsBL.GetAccSettingByCompanyID(this.CompanyID);
            AccRefSettingsList = AccRefSettingsBL.GetAccRefSettingsList(this.CompanyID);

            this.ucPrintButton.PrintClick += new Web.Controls.PrintButton.PrintButtonClickEventHandler(OnPrintAction);
            
            if (!IsPostBack) //first Time
            {
                this.JournalID = base.GetPageQueryInteger("id");
                ViewState[ViewStateKeyPrev] = "0";
                //this.EditMode = (FormDataMode)base.GetPageQueryInteger(this.EditModeViewStateKey);

                FillCombo();
                SetPrintOption();
                if (this.JournalID == 0) //not query string
                {
                    AddTask();
                }
                else
                {
                    FormDataMode fdMode = base.GetEditModeFromQueryString(this.EditModeQueryStringKey);
                    if (fdMode == FormDataMode.Edit)
                    {
                        EditTask();
                    }
                    else
                    {
                        ReadTask();
                    }
                    int iPrint = base.GetPageQueryInteger("print");
                    if (iPrint == 1)
                    {
                        string rptKey = GenarateJournalPrint();
                        if (rptKey != string.Empty)
                        {
                            this.ucPrintButton.ReportKey = rptKey;
                            this.ucPrintButton.ReportError = "";
                        }
                        else
                        {
                            this.ucPrintButton.ReportKey = "";
                            this.ucPrintButton.ReportError = "Print Error!";
                        }
                        this.ucPrintButton.AutoPrint = true;
                        this.ucPrintButton.PrintTask();
                    }
                }
                
            }
            else
            {
                this.EditMode = base.GetEditModeFromViewState(base.EditModeViewStateKey);
                this.JournalID =base.GetViewStateInt(this.ViewStateKey);
            }
            this.ShowPageMessage(this.lblMessage);
            SetHyperLink();
            SetJavascriptEvents();
            this.hdnPopupTriggerID.Value = this.btnPopupTrigger.UniqueID;
            SetGLGroupClassListString();
          

           // Response.Write("UserID : " + this.UserID.ToString());
        }


        protected override void Render(HtmlTextWriter writer)
        {

            Page.ClientScript.RegisterForEventValidation(btnPopupTrigger.UniqueID);
            Page.ClientScript.RegisterForEventValidation(btnPopupTrigger.UniqueID, "");
            //Page.ClientScript.RegisterForEventValidation(btnPopupTrigger.UniqueID, "fillcombo");
            //Page.ClientScript.RegisterForEventValidation(btnPopupTrigger.UniqueID, "getbalance");

            base.Render(writer);
        }


        private void SetHyperLink()
        {

            //new button
            //string hLink = "javascript:tbopenLandPaySchedule(" + hdnLandID.Value + ")";
            if (base.PageMode == PageModeEnum.InTab)
            {
                //hLink = "javascript:tbopenLandPaySchedule(" + this.JournalID.ToString() + ")";
                //this.btnSalaryInfo = string.Empty;
                //hLink = "tbopenLandPaySchedule(" + hdnLandID.Value + ")";
                //this.btnPaymentSchedule.Attributes.Add("onclick", hLink);
            }
            else
            {
                //hLink = "~/Project/LandPaySchedule.aspx?id=" + this.JournalID.ToString();
                //this.btnAddNew.PostBackUrl = hLink;
                //this.btnAddNew.OnClientClick = string.Empty;
                //this.btnPaymentSchedule.Attributes.Add("onclick", hLink);

                //hLink = hLink = "javascript:tbopenLandOwner(" + hdnLandID.Value + ")";
                //this.btnLandOwner.Attributes.Add("onclick", hLink);
            }
            this.btnDelete.OnClientClick = "return confirm('Are you sure to delete this payment requisition.')";
        }

        private void SetJavascriptEvents()
        {
            this.btnSave.OnClientClick = "return btnSaveClick();";

            //this.btnSave.UniqueID
        }


        private void SetPrintOption()
        {
            this.ucPrintButton.DefaultViewMode = (ReportViewModeEnum)AccSettings.DefJournalReportViewMode;
            this.ucPrintButton.DefaultExportType = (ReportExportTypeEnum)AccSettings.DefJournalReportExportType;
            this.ucPrintButton.DefaultPrintAction = (ReportOpenTypeEnum)(ReportExportTypeEnum)AccSettings.DefJouralReportOpenType;


            int printFormat = base.GetPageQueryInteger("printformat");
            if (printFormat > 0)
            {
                ddlReportFormat.SelectedValue = printFormat.ToString();
            }
            else
            {
                ddlReportFormat.SelectedValue = this.AccSettings.DefJournalReportFormat.ToString();
            }
        }

        private void FillCombo()
        {

            ddlAccYear.DataTextField = "AccYearName";
            ddlAccYear.DataValueField = "AccYearID";
            ddlAccYear.AppendDataBoundItems = true;
            ddlAccYear.DataSource = AccYearBL.GetAccYearList(this.CompanyID, this.DbContext);
            ddlAccYear.DataBind();


            ddlJournalType.DataTextField = "JournalTypeName";
            ddlJournalType.DataValueField = "JournalTypeID";
            ddlJournalType.AppendDataBoundItems = true;
            ddlJournalType.DataSource = JournalTypeBL.GetJournalTypeList(this.CompanyID, this.DbContext);
            ddlJournalType.DataBind();

            ddlJournalAdjustType.DataTextField = "JournalAdjustTypeName";
            ddlJournalAdjustType.DataValueField = "JournalAdjustTypeID";
            ddlJournalAdjustType.AppendDataBoundItems = true;
            ddlJournalAdjustType.DataSource = JournalAdjustTypeBL.GetJournalAdjustTypeList(this.DbContext);
            ddlJournalAdjustType.DataBind();
            ddlJournalAdjustType.SelectedValue = "1";


            ddlReportFormat.Items.Clear();
            ddlReportFormat.DataTextField = "JournalReportFormatName";
            ddlReportFormat.DataValueField = "JournalReportFormatID";
            ddlReportFormat.AppendDataBoundItems = true;
            ddlReportFormat.DataSource = JournalReportFormatBL.GetJournalReportFormatList(this.DbContext);
            ddlReportFormat.DataBind();


            //ddlDetTranType.Items.Clear();
            //ddlDetTranType.DataTextField = "JournalTranTypeName";
            //ddlDetTranType.DataValueField = "JournalTranTypeID";
            //ddlDetTranType.AppendDataBoundItems = true;
            //ddlDetTranType.DataSource = JournalTranTypeBL.GetJournalTranTypeList(this.DbContext);
            //ddlDetTranType.DataBind();


            FillCombo_Location();
            FillComboInstrumentType();
            FillComboInstrumentStatus();

        }

        private void FillCombo_Location()
        {
            int companyID = this.CompanyID;
            ddlLocation.Items.Clear();
            //ddlLocation.Items.Add(new ListItem("All", "0"));
            //ddlLocation.AppendDataBoundItems = true;

            ddlLocation.DataTextField = "LocationCodeName";//"LocationName";
            ddlLocation.DataValueField = "LocationID";
            //ddlLocation.DataSource = LocationBL.GetLocationList(companyID);
            //ddlLocation.DataBind();
            ddlLocation.DataSource = AppSecurity.GetValidLocationUserList(); //LocationBL.GetLocationList(this.CompanyID);
            ddlLocation.DataBind();
            //foreach (dcLocation loc in LocationBL.GetLocationList(companyID))
            //{
            //    ddlLocation.Items.Add(new ListItem(loc.LocationCode + " - " + loc.LocationName, loc.LocationID.ToString()));
            //}

            dcUser user = AppSecurity.GetUserInfoFromSession();
            int locID = user.LoginLocationID;
            ddlLocation.SelectedValue = locID.ToString();

            //int locID = AppSecurity.GetValidLocationUserList().Select(c => c.LocationID).FirstOrDefault();

            //if (ddlLocation.Items.Count < 3)
           // {
            //    ddlLocation.SelectedValue = locID.ToString();

          //  }
           
        }


        private void FillComboInstrumentType()
        {
            List<dcInstrumentType> insTypeList = InstrumentTypeBL.GetInstrumentTypeList();

            foreach (dcInstrumentType insType in insTypeList)
            {
                ListItem lst = new ListItem();
                lst.Text = insType.InstrumentTypeName;
                lst.Value = insType.InstrumentTypeID.ToString();
                if (insType.InstrumentTypeID == (int)InstrumentTypeEnum.Cheque)
                {
                    lst.Selected = true;
                }
                ddlInstrumentType.Items.Add(lst);
                ddlInsDetInsType.Items.Add(lst);
            }
        }


        private void FillComboInstrumentStatus()
        {
            List<dcInstrumentStatus> insStatusList = InstrumentStatusBL.GetInstrumentStatusList();
            foreach (dcInstrumentStatus insStatus in insStatusList)
            {
                ListItem lst = new ListItem();
                lst.Text = insStatus.InstrumentStatusName;
                lst.Value = insStatus.InstrumentStatusID.ToString();
                if (insStatus.InstrumentStatusID == (int)InstrumentStatusEnum.Cleared)
                {
                    lst.Selected = true;
                }
                ddlInsDetInsStatus.Items.Add(lst);
 
            }
        }

        private void SetGLGroupClassListString()
        {
            int jTypeID = Convert.ToInt32(ddlJournalType.SelectedValue);
            dcJournalType journalType = JournalTypeBL.GetJournalTypeByID(this.CompanyID, jTypeID);

            

            if (journalType.IsGLGroupClass)
            {
                hdnGLGroupClassInclude.Value = journalType.GLGroupClassIncludeJT;
                hdnGLGroupClassExclude.Value = journalType.GLGroupClassExcludeJT;
            }
            else
            {
                hdnGLGroupClassInclude.Value = journalType.GLGroupClassInclude;
                hdnGLGroupClassExclude.Value = journalType.GLGroupClassExclude;
            }

        }

        public List<dcGLAccount> GetGLAccountList()
        {
            if (listGLAccounts == null)
            {
                List<dcGLAccount> accList = GLAccountBL.GetGLAccountList(false, AccOrderByEnum.Name, string.Empty);

                listGLAccounts = (from c in accList
                                  where c.GLAccountTypeID != (int)GLAccountTypeEnum.ControlAccount
                                  select c).ToList();
            }

            return listGLAccounts;
        }

        private void SetControl(FormDataMode dataMode)
        {
            bool isEnabled = false;

            if (dataMode == FormDataMode.Add | dataMode == FormDataMode.Edit)
            {
                isEnabled = true;
            }

            ddlAccYear.Enabled = dataMode == FormDataMode.Add;
            ddlJournalType.Enabled = dataMode == FormDataMode.Add;
            ddlLocation.Enabled = dataMode == FormDataMode.Add;
            txtJournalNo.Enabled = dataMode == FormDataMode.Add;
            txtJournalDate.Enabled = isEnabled;//isEnabled;
            
            ddlJournalAdjustType.Enabled = isEnabled;
            txtJournalDesc.Enabled = isEnabled;

            txtTotDebitAmt.Enabled = isEnabled;
            txtTotCreditAmt.Enabled = isEnabled;

            txtTotDebitAmt.Attributes.Add("readonly", "readonly");
            txtTotCreditAmt.Attributes.Add("readonly", "readonly");

            //buttons
            btnAddNew.Visible = !isEnabled;
            btnEdit.Visible = !isEnabled;
            btnDelete.Visible = !isEnabled; 
            ucPrintButton.Visible = !isEnabled;

            btnSave.Visible = isEnabled;
            btnCancel.Visible = isEnabled;

            SetControlGrid(dataMode);
            SetControlGrid2(dataMode);

            SetSLNo(dataMode);

        }

        private void SetSLNo(FormDataMode dataMode)
        {
            if (dataMode == FormDataMode.Add | dataMode == FormDataMode.Edit)
            {
                int jTypeID = Convert.ToInt32(ddlJournalType.SelectedValue);
                dcJournalType jType = JournalTypeBL.GetJournalTypeByID(this.CompanyID, jTypeID);
                if (jType.AccSLNoID > 0)
                {
                    txtJournalNo.Attributes.Add("readonly", "readonly");
                }
            }
        }

        private void SetControlGrid(FormDataMode dataMode)
        {
            bool isEnabled = false;

            if (dataMode == FormDataMode.Add | dataMode == FormDataMode.Edit)
            {
                isEnabled = true;
            }

            foreach (GridViewRow gvR in this.GridView1.Rows)
            {
                if (gvR.RowType == DataControlRowType.DataRow)
                {
                    //((DropDownList)gvR.FindControl("ddlLandOwner")).Enabled = isEnabled;

                    //((DropDownList)gvR.FindControl("ddlDrCr")).Enabled = isEnabled;
                    ((TextBox)gvR.FindControl("txtGLGroupCode")).Enabled = isEnabled;
                    ((TextBox)gvR.FindControl("txtGLGroupName")).Enabled = isEnabled;
                    ((TextBox)gvR.FindControl("txtGLAccountCode")).Enabled = isEnabled;
                    ((TextBox)gvR.FindControl("txtGLAccountName")).Enabled = isEnabled;
                    ((TextBox)gvR.FindControl("txtJournalDetDesc")).Enabled = isEnabled;  
                    ((TextBox)gvR.FindControl("txtTranType")).Enabled = isEnabled;
                    ((TextBox)gvR.FindControl("txtCostCenter")).Enabled = isEnabled;
                    ((TextBox)gvR.FindControl("txtReference")).Enabled = isEnabled;
                    ((TextBox)gvR.FindControl("txtInstrument")).Enabled = isEnabled;

                    ((TextBox)gvR.FindControl("txtDebitAmt")).Enabled = isEnabled;
                    //((TextBox)gvR.FindControl("txtCreditAmt")).Enabled = isEnabled;

                    if(isEnabled)
                    {
                        ((HtmlInputButton)gvR.FindControl("btnGLGroupAC")).Attributes.Remove("disabled");
                        ((HtmlInputButton)gvR.FindControl("btnGLAccountAC")).Attributes.Remove("disabled");
                        ((HtmlInputButton)gvR.FindControl("btnTranTypeAC")).Attributes.Remove("disabled");
                    }
                    else
                    {
                        ((HtmlInputButton)gvR.FindControl("btnGLGroupAC")).Attributes.Add("disabled", "disabled");
                        ((HtmlInputButton)gvR.FindControl("btnGLAccountAC")).Attributes.Add("disabled", "disabled");
                        ((HtmlInputButton)gvR.FindControl("btnTranTypeAC")).Attributes.Add("disabled", "disabled");
                    }


                    ////HyperLink lnkEdit = (HyperLink)gvR.Cells[4].Controls[0];
                    //LinkButton lnkEdit = (LinkButton)gvR.FindControl("btnEditRow");
                    //lnkEdit.Enabled = isEnabled;
                    //lnkEdit.Visible = false;

                    //LinkButton lnkDelete = (LinkButton)gvR.Cells[5].Controls[0];
                    LinkButton lnkDelete = (LinkButton)gvR.FindControl("btnDeleteRow");
                    lnkDelete.Enabled = isEnabled;
                    if (!isEnabled)
                    {
                        lnkDelete.OnClientClick = "";
                    }


                    //make grid readonly
                    ((TextBox)gvR.FindControl("txtGLGroupName")).Attributes.Add("readonly", "readonly");
                    ((TextBox)gvR.FindControl("txtGLAccountName")).Attributes.Add("readonly", "readonly");
                    ((TextBox)gvR.FindControl("txtCostCenter")).Attributes.Add("readonly", "readonly");
                    ((TextBox)gvR.FindControl("txtReference")).Attributes.Add("readonly", "readonly");
                    ((TextBox)gvR.FindControl("txtInstrument")).Attributes.Add("readonly", "readonly");
                    //((TextBox)gvR.FindControl("txtRequisitionAmt")).Attributes.Add("readonly", "readonly");
                    //((TextBox)gvR.FindControl("txtApprovedAmt")).Attributes.Add("readonly", "readonly");

                }
            }

            hdnJournalDetInsJson.AddCssClass("hdnInsJson");
            hdnJournalDetRefJson.AddCssClass("hdnRefJson");
            btnNewRow.Enabled = isEnabled;
        }

        private void SetControlGrid2(FormDataMode dataMode)
        {

            bool isEnabled = false;

            if (dataMode == FormDataMode.Add | dataMode == FormDataMode.Edit)
            {
                isEnabled = true;
            }

            foreach (GridViewRow gvR in this.GridView2.Rows)
            {
                if (gvR.RowType == DataControlRowType.DataRow)
                {
                    //((DropDownList)gvR.FindControl("ddlLandOwner")).Enabled = isEnabled;

                    //((DropDownList)gvR.FindControl("ddlDrCr")).Enabled = isEnabled;
                    ((TextBox)gvR.FindControl("txtGLGroupCode")).Enabled = isEnabled;
                    ((TextBox)gvR.FindControl("txtGLGroupName")).Enabled = isEnabled;
                    ((TextBox)gvR.FindControl("txtGLAccountCode")).Enabled = isEnabled;
                    ((TextBox)gvR.FindControl("txtGLAccountName")).Enabled = isEnabled;
                    ((TextBox)gvR.FindControl("txtTranType")).Enabled = isEnabled;
                    ((TextBox)gvR.FindControl("txtJournalDetDesc")).Enabled = isEnabled;  
                    ((TextBox)gvR.FindControl("txtCostCenter")).Enabled = isEnabled;
                    ((TextBox)gvR.FindControl("txtReference")).Enabled = isEnabled;
                    ((TextBox)gvR.FindControl("txtInstrument")).Enabled = isEnabled;

                    //((TextBox)gvR.FindControl("txtDebitAmt")).Enabled = isEnabled;
                    ((TextBox)gvR.FindControl("txtCreditAmt")).Enabled = isEnabled;

                    if (isEnabled)
                    {
                        ((HtmlInputButton)gvR.FindControl("btnGLGroupAC")).Attributes.Remove("disabled");
                        ((HtmlInputButton)gvR.FindControl("btnGLAccountAC")).Attributes.Remove("disabled");
                        ((HtmlInputButton)gvR.FindControl("btnTranTypeAC")).Attributes.Remove("disabled");
                    }
                    else
                    {
                        ((HtmlInputButton)gvR.FindControl("btnGLGroupAC")).Attributes.Add("disabled", "disabled");
                        ((HtmlInputButton)gvR.FindControl("btnGLAccountAC")).Attributes.Add("disabled", "disabled");
                        ((HtmlInputButton)gvR.FindControl("btnTranTypeAC")).Attributes.Add("disabled", "disabled");
                    }

                    ////HyperLink lnkEdit = (HyperLink)gvR.Cells[4].Controls[0];
                    //LinkButton lnkEdit = (LinkButton)gvR.FindControl("btnEditRow");
                    //lnkEdit.Enabled = isEnabled;
                    //lnkEdit.Visible = false;

                    //LinkButton lnkDelete = (LinkButton)gvR.Cells[5].Controls[0];
                    LinkButton lnkDelete = (LinkButton)gvR.FindControl("btnDeleteRow");
                    lnkDelete.Enabled = isEnabled;

                    //make grid readonly
                    ((TextBox)gvR.FindControl("txtGLGroupName")).Attributes.Add("readonly", "readonly");
                    ((TextBox)gvR.FindControl("txtGLAccountName")).Attributes.Add("readonly", "readonly");
                    ((TextBox)gvR.FindControl("txtCostCenter")).Attributes.Add("readonly", "readonly");
                    ((TextBox)gvR.FindControl("txtReference")).Attributes.Add("readonly", "readonly");
                    ((TextBox)gvR.FindControl("txtInstrument")).Attributes.Add("readonly", "readonly");
                    //((TextBox)gvR.FindControl("txtRequisitionAmt")).Attributes.Add("readonly", "readonly");
                    //((TextBox)gvR.FindControl("txtApprovedAmt")).Attributes.Add("readonly", "readonly");

                }
            }

            hdnCompanyID.AddCssClass("hdnc");

            hdnJournalDetRefJson2.AddCssClass("hdnRefJson");
            hdnJournalDetInsJson2.AddCssClass("hdnInsJson");

            btnNewRow2.Enabled = isEnabled;
        }

        private void ReadTask()
        {
            lblHeader.Text = "Journal : View";
            this.EditMode = FormDataMode.Read;
            //this.hdnJournalID.Value = this.JournalID.ToString();
            ReadData(this.JournalID);
            ReadDetails(this.JournalID);
            ViewState[ViewStateKey] = this.JournalID.ToString();
           
            SetControl(FormDataMode.Read);

            //var c = new { Name = "Name", ID = 344 };
        }

        private void AddTask()
        {

            ViewState[ViewStateKeyPrev] = this.JournalID.ToString();
            this.hdnJournalID.Value = "0";
            this.hdnJournalUpdateNo.Value = "0";

            //ddlJournalType.SelectedValue = "1"; //general journal

            ddlAccYear.SelectedValue =  AccYearBL.GetCurrentAccYear(this.CompanyID).AccYearID.ToString();

            txtJournalDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
            txtJournalNo.Text = "";
            txtJournalDesc.Text = "";

            txtTotDebitAmt.Text = "0.00";
            txtTotCreditAmt.Text = "0.00";

            this.EditMode = FormDataMode.Add;
            this.IsDirty = false;
            ViewState[ViewStateKey] = "0";
            hdnEditDataModeInt.Value = ((int)this.EditMode).ToString();
            //lblMode.Text = "Mode: Add";
            this.listDetails.Clear();
            CheckAndAddGridBlankRow();
            CheckAndAddGridBlankRow2();
            BindDataToGrid();
            BindDataToGrid2();

            hdnJournalDetInsJson.Value = "[]";
            hdnJournalDetRefJson.Value = "[]";

            hdnJournalDetInsJson2.Value = "[]";
            hdnJournalDetRefJson2.Value = "[]";

            lblHeader.Text = "Journal : New";

            SetControl(FormDataMode.Add);

            Helper.SetStatusMessage(lblMessage, string.Empty, MessageTypeEnum.None);
        }

        private void EditTask()
        {
            this.EditMode = FormDataMode.Edit;
            //lblMode.Text = "Mode: Edit";
            lblHeader.Text = "Journal: Edit";
            //this.SetMasterHeader("Edit User");
            //this.hdnJournalID.Value = this.JournalID.ToString();
            hdnEditDataModeInt.Value = ((int)this.EditMode).ToString();
            ReadData(this.JournalID);
            ReadDetails(this.JournalID);

            this.IsDirty = false;
            ViewState[ViewStateKey] = this.JournalID.ToString();
            //lnkAddNew.Visible = true;
            SetControl(FormDataMode.Edit);
        }

        private void RefreshTask()
        {
            switch (this.EditMode)
            {
                case FormDataMode.Add:
                    AddTask();
                    break;
                case FormDataMode.Edit:
                    EditTask();
                    break;
                case FormDataMode.Read:
                    ReadTask();
                    break;
            }
        }

        private void DeleteTask()
        {
            if (this.JournalID > 0)
            {
                 //JournalBL.DeleteWithDetails(this.JournalID);

            //    this.SetPageMessage("Journal Deleted Successfully.", MessageTypeEnum.Successful);
            //    this.SetPageMessageToSession();
            //    //string redirectURL = "~/Project/Land.aspx?id=" + this.JournalID.ToString() + "&" + AppMessage.CreateQueryString(this.AppMessageID);

            //    string redirectURL = "~/Journal/Journal.aspx?id=0";
            //    redirectURL = base.SetPageTabQueryString(redirectURL);
            //    redirectURL = base.SetPageMessageQueryString(redirectURL, this.AppMessageID);
            //    Response.Redirect(redirectURL,false);
            }

        }

        private void CancelTask()
        {
            if (EditMode == FormDataMode.Add)
            {
                int prevID = base.GetViewStateInt(ViewStateKeyPrev);
                if (prevID > 0)
                {
                    this.JournalID = prevID;
                    ReadTask();
                }
                else
                {
                    this.JournalID = 0;
                    AddTask();
                }
            }
            else if (EditMode == FormDataMode.Edit)
            {
                ReadTask();
            }
            this.IsDirty = false;
        }


        private void ReadJournalType()
        {
            int jTypeID = Convert.ToInt32(ddlJournalType.SelectedValue);
            this.JournalType = JournalTypeBL.GetJournalTypeByID(this.CompanyID, jTypeID, this.DbContext);          
        }


        private bool ReadData(int pJournalID)
        {
            dcJournal cObj = JournalBL.GetJournalByID(this.CompanyID, pJournalID,0);//TODO Change add 0

            bool bStatus = false;
            if (cObj != null)
            {
                this.JournalID = cObj.JournalID;
                this.hdnJournalID.Value = cObj.JournalID.ToString();
                this.hdnJournalUpdateNo.Value = cObj.JournalUpdateNo.ToString();

                txtJournalNo.Text = cObj.JournalNo;

                txtJournalDate.Text = cObj.JournalDate.ToString("dd-MMM-yyyy");
                txtJournalDesc.Text = cObj.JournalDesc;

                ddlJournalType.SelectedValue = cObj.JournalTypeID.ToString();
                ddlLocation.SelectedValue = cObj.LocationID.ToString();

                ddlAccYear.SelectedValue = cObj.AccYearID.ToString();

                //ddlProjectStatus.SelectedValue = cObj.ProjectStatusID.ToString();

                txtTotDebitAmt.Text = cObj.JournalAmt.ToString("#0.00");
                txtTotCreditAmt.Text = cObj.JournalAmt.ToString("#0.00");

                ddlJournalAdjustType.SelectedValue = cObj.JournalAdjustTypeID.ToString();

                this.IsPosted = cObj.IsPosted;
                
                
                lblPosted.Text = cObj.IsPosted ? "Yes" : "No";

                btnPost.Enabled = !cObj.IsPosted;

                btnSave.Enabled = cObj.IsEditable;

                bStatus = true;
                btnSave.Enabled = true;
            }
            else
            {
                this.hdnJournalID.Value = "0";
                hdnJournalDetRefJson.Value = "[]";
                bStatus = false;
                this.SetPageMessage("Journal data not found!", MessageTypeEnum.Error);
                this.ShowPageMessage(lblMessage);
                btnSave.Enabled = false;
                //MMS.Globals.ShowMessagePage(MMS.MessageTypeEnum.Error, "User ID not Found!", "");
            }
            return bStatus;
        }

        private void SumDetGrid()
        {
            decimal totDebit = 0;
            //decimal totCredit = 0;

            if (this.GridView1.Rows.Count > 0)
            {
                List<dcJournalDet> reqList = GridView1.DataSource as List<dcJournalDet>;
                foreach (dcJournalDet req in reqList)
                {
                    if (req._RecordState != RecordStateEnum.Deleted)
                    {
                        totDebit += req.DebitAmt;
                    }
                }
            }

            txtTotDebitAmt.Text = totDebit.ToString("#0.00");
            //txtTotCreditAmt.Text = totCredit.ToString("#0.00");

        }

        private void SumDetGrid2()
        {
            decimal totDebit = 0;
            decimal totCredit = 0;


            if (this.GridView1.Rows.Count > 0)
            {
                List<dcJournalDet> reqList = GridView2.DataSource as List<dcJournalDet>;
                foreach (dcJournalDet req in reqList)
                {
                    if (req._RecordState != RecordStateEnum.Deleted)
                    {
                        totDebit += req.DebitAmt;
                        totCredit += req.CreditAmt;
                    }

                }
            }
            //txtTotDebitAmt.Text = totDebit.ToString("#0.00");
            txtTotCreditAmt.Text = totCredit.ToString("#0.00");

        }

        private void FormatDetailsData()
        {
            int linkID = 1;
            foreach (dcJournalDet det in this.listDetails)
            {
                det.JournalDetID_Link = linkID;

                List<dcJournalDetRef> detRefList = this.listDetailsRef.Where(c => c.JournalDetID == det.JournalDetID).ToList();
                foreach (dcJournalDetRef detRef in detRefList)
                {
                    detRef.JournalDetID_Link = linkID;
                }

                List<dcJournalDetIns> detInsList = this.listDetailsIns.Where(c => c.JournalDetID == det.JournalDetID).ToList();
                foreach (dcJournalDetIns detIns in detInsList)
                {
                    detIns.JournalDetID_Link = linkID;
                }

                linkID++;
            }
        }


        private void SetDetailsForSingle()
        {
            foreach (dcJournalDet det in this.listDetails)
            {
                ///fill gl tran type\\
                //List<dcJournalDetRef> detRefListTT = this.listDetailsRef.Where(c => c.AccRefTypeID == (int)AccRefTypeEnum.TranType
                //                                                              && c.JournalDetID_Link == det.JournalDetID_Link
                //                                                              && c._RecordState != RecordStateEnum.Deleted).ToList();

                dcJournalDetRef detRefTT = this.listDetailsRef.Where(c => c.AccRefTypeID == (int)AccRefTypeEnum.TranCode
                                                                              && c.JournalDetID_Link == det.JournalDetID_Link
                                                                              && c._RecordState != RecordStateEnum.Deleted).FirstOrDefault();
                
                //if (detRefListTT.Count > 0)
                //{
                //    det.TranTypeID = detRefListTT[0].AccRefID;
                //    det.TranTypeCode = detRefListTT[0].AccRefCode;
                //    det.TranTypeName = detRefListTT[0].AccRefName;
                //}


                if (detRefTT != null)
                {
                    det.TranTypeID = detRefTT.AccRefID;
                    det.TranTypeCode = detRefTT.AccRefCode;
                    det.TranTypeName = detRefTT.AccRefName;
                }


                ////cost center
                //List<dcJournalDetRef> detRefListCC = this.listDetailsRef.Where(c => c.AccRefTypeID == (int)AccRefTypeEnum.CostCenter
                //                                                              && c.JournalDetID_Link == det.JournalDetID_Link
                //                                                              && c._RecordState != RecordStateEnum.Deleted).ToList();

                int ccCount = this.listDetailsRef.Where(c => c.AccRefTypeID == (int)AccRefTypeEnum.CostCenter
                                                                              && c.JournalDetID_Link == det.JournalDetID_Link
                                                                              && c._RecordState != RecordStateEnum.Deleted).Count();


                //det.CostCenterText =  ccCount > 0 ?  ccCount.ToString() + " entry(s)" : string.Empty;
                string cText = ccCount.ToString() + " entry(s)";
                if (ccCount == 1)
                {
                    dcJournalDetRef detRefCC = this.listDetailsRef.Where(c => c.AccRefTypeID == (int)AccRefTypeEnum.CostCenter
                                                                             && c.JournalDetID_Link == det.JournalDetID_Link
                                                                             && c._RecordState != RecordStateEnum.Deleted).FirstOrDefault();
                    cText = detRefCC.AccRefCode;
                }

                det.CostCenterText = ccCount > 0 ? cText : string.Empty;


                //reference

                int rfCount = this.listDetailsRef.Where(c => c.AccRefTypeID == (int)AccRefTypeEnum.Reference
                                                                              && c.JournalDetID_Link == det.JournalDetID_Link
                                                                              && c._RecordState != RecordStateEnum.Deleted).Count();

                string rText = rfCount.ToString() + " entry(s)";
                if (rfCount == 1)
                {
                    //rText = this.listDetailsRef[0].AccRefCode;

                    dcJournalDetRef detRefRF = this.listDetailsRef.Where(c => c.AccRefTypeID == (int)AccRefTypeEnum.Reference
                                                         && c.JournalDetID_Link == det.JournalDetID_Link
                                                         && c._RecordState != RecordStateEnum.Deleted).FirstOrDefault();
                    rText = detRefRF.AccRefCode;
                }  

                det.ReferenceText = rfCount > 0 ? rText : string.Empty;



                ///instrument
                int insCount = this.listDetailsIns.Where(c => c.JournalDetID_Link == det.JournalDetID_Link
                                                              && c._RecordState != RecordStateEnum.Deleted).Count();

                det.InstrumentText = insCount > 0 ? insCount.ToString() + " entry(s)" : string.Empty;


            }
        }
       
        private void ListDetailsRefToJSon(List<dcJournalDetRef> detRefList)
        {
            var jsonListDr = from c in detRefList
                           where c.DrCr == (int)DebitCreditEnum.Debit
                           select new jsonDetailsRef
                           {
                               journaldetid = c.JournalDetID,
                               journaldetrefid = c.JournalDetRefID,
                               linkid = c.JournalDetID_Link,
                               
                               id = c.AccRefID,
                               code = c.AccRefCode,
                               name = c.AccRefName,
                               typeid = c.AccRefTypeID,
                               typecode = c.AccRefTypeCode,
                               typename = c.AccRefTypeName,
                               categoryid = c.AccRefCategoryID,
                               categorycode = c.AccRefCategoryCode,
                               categoryname = c.AccRefCategoryName,
                               drcr = c.DrCr,
                               amt = Math.Abs(c.TranAmt),
                               accrefno = c.AccRefNo,
                               accrefremarks = c.AccRefRemarks,
                               _recordstateint = c._RecordStateInt,
                           };

            JavaScriptSerializer jss = new JavaScriptSerializer();
            string jsonDataDr = jss.Serialize(jsonListDr);
            hdnJournalDetRefJson.Value = jsonDataDr;


            var jsonListCr = from c in detRefList
                             where c.DrCr == (int)DebitCreditEnum.Credit
                             select new jsonDetailsRef
                             {
                                 journaldetid = c.JournalDetID,
                                 journaldetrefid = c.JournalDetRefID,
                                 linkid = c.JournalDetID_Link,

                                 id = c.AccRefID,
                                 code = c.AccRefCode,
                                 name = c.AccRefName,
                                 typeid = c.AccRefTypeID,
                                 typecode = c.AccRefTypeCode,
                                 typename = c.AccRefTypeName,
                                 categoryid = c.AccRefCategoryID,
                                 categorycode = c.AccRefCategoryCode,
                                 categoryname = c.AccRefCategoryName,
                                 drcr = c.DrCr,
                                 amt = Math.Abs(c.TranAmt),
                                 accrefno = c.AccRefNo,
                                 accrefremarks = c.AccRefRemarks,
                                 _recordstateint = c._RecordStateInt,
                             };

            JavaScriptSerializer jss2 = new JavaScriptSerializer();
            string jsonDataCr = jss2.Serialize(jsonListCr);
            hdnJournalDetRefJson2.Value = jsonDataCr;

        }


        private void ReadDetailsInsSingle()
        {
            foreach (dcJournalDet det in this.listDetails)
            {
                int insCount = this.listDetailsIns.Where(c=> c.JournalDetID_Link == det.JournalDetID_Link
                                                                        && c._RecordState != RecordStateEnum.Deleted).Count();
               det.InstrumentText = insCount > 0 ? insCount.ToString() + " entry(s)" : string.Empty;
            }
        }
        private void ListDetailsInsToJSon(List<dcJournalDetIns> detInsList)
        {
            var jsonList = from c in detInsList
                           where c.DrCr == (int)DebitCreditEnum.Debit
                           select new jsonDetailsIns
                           {
                               journaldetid = c.JournalDetID,
                               journaldetinsid = c.JournalDetInsID,
                               linkid = c.JournalDetID_Link,

                               instypeid = c.InstrumentTypeID,
                               instypename = c.InstrumentTypeName,

                               insmodeid = c.InstrumentModeID,
                               inslinktypeid = c.InstrumentLinkTypeID,

                               insid = c.InstrumentID,
                               insno = c.InstrumentNo,
                               insdate = Conversion.DateTimeNullToEmpty(c.InstrumentDate),
                               insamt = c.InstrumentAmt,

                               drcr = c.DrCr,
                               amt = c.InsTranAmt,

                               issuename = c.IssueName,
                               bankname = c.BankName,
                               branchname = c.BranchName,
                               bankbranchname = c.BankName + ',' + c.BranchName,
                               

                               insstatusid = c.InstrumentStatusID,
                               insstatusname = c.InstrumentStatusName,
                               

                               
                               _recordstateint = c._RecordStateInt,
                           };

            JavaScriptSerializer jss = new JavaScriptSerializer();
            string jsonData = jss.Serialize(jsonList);
            hdnJournalDetInsJson.Value = jsonData;


            var jsonList2 = from c in detInsList
                           where c.DrCr == (int)DebitCreditEnum.Credit
                           select new jsonDetailsIns
                           {
                               journaldetid = c.JournalDetID,
                               journaldetinsid = c.JournalDetInsID,
                               linkid = c.JournalDetID_Link,

                               instypeid = c.InstrumentTypeID,
                               instypename = c.InstrumentTypeName,

                               insmodeid = c.InstrumentModeID,
                               inslinktypeid = c.InstrumentLinkTypeID,

                               insid = c.InstrumentID,
                               insno = c.InstrumentNo,
                               insdate = Conversion.DateTimeNullToEmpty(c.InstrumentDate),
                               insamt = c.InstrumentAmt,

                               drcr = c.DrCr,
                               amt = c.InsTranAmt,

                               issuename = c.IssueName,
                               bankname = c.BankName,
                               branchname = c.BranchName,
                               bankbranchname = c.BankName + ',' + c.BranchName,


                               insstatusid = c.InstrumentStatusID,
                               insstatusname = c.InstrumentStatusName,



                               _recordstateint = c._RecordStateInt,
                           };

            JavaScriptSerializer jss2 = new JavaScriptSerializer();
            string jsonData2 = jss.Serialize(jsonList2);
            hdnJournalDetInsJson2.Value = jsonData2;
        }

        private bool ReadDetails(int pJournalID)
        {
            listDetails = JournalDetBL.GetJournalDetList(this.CompanyID, pJournalID);
            listDetails.ForEach(c => c.GLAccountIDEdit = c.GLAccountID);

            listDetailsRef = JournalDetRefBL.GetJournalDetRefList(this.CompanyID, pJournalID, 0);
            listDetailsIns = JournalDetInsBL.GetJournalDetInsList(this.CompanyID, pJournalID);
            FormatDetailsData();
            SetDetailsForSingle();
            //CheckAndAddGridBlankRow();
            BindDataToGrid();
            BindDataToGrid2();
            ListDetailsRefToJSon(this.listDetailsRef);
            ListDetailsInsToJSon(this.listDetailsIns);

            // BindDataToGridDeduction();


            

            return true;
        }



        private void BindDataToGrid()
        {
            //addition
            //List<DBClass.PayRoll.dcSalaryDefDet> listAddition = (from c in listDetails
            //                                                     where c.SalaryHeadType_sopt == (int)BLLibrary.SysOption.SalaryHeadTypeEnum.Addition
            //                                                     orderby c.SalaryDefDetSLNo
            //                                                     select c).ToList();

            this.GridView1.DataSource = listDetails.Where(c => c.DrCr == (int)DebitCreditEnum.Debit).ToList(); ;
            this.GridView1.DataBind();

            SumDetGrid();
        }

        private void BindDataToGrid2()
        {
            //addition
            //List<DBClass.PayRoll.dcSalaryDefDet> listAddition = (from c in listDetails
            //                                                     where c.SalaryHeadType_sopt == (int)BLLibrary.SysOption.SalaryHeadTypeEnum.Addition
            //                                                     orderby c.SalaryDefDetSLNo
            //                                                     select c).ToList();

            this.GridView2.DataSource = listDetails.Where(c=>c.DrCr == (int)DebitCreditEnum.Credit).ToList();
            this.GridView2.DataBind();

            SumDetGrid2();
        }



        private void CheckAndAddGridBlankRow()
        {

            int RowCheck = 1;
            //adddtion
            int cntAdd = listDetails.Where(c => c.DrCr == (int)DebitCreditEnum.Debit).Count();

            //AddBlankRowToGridList();

            if (cntAdd < RowCheck)
            {
                int diffAdd = RowCheck - cntAdd;
                for (int i = 0; i < diffAdd; i++)
                {
                    AddBlankRowToGridList();
                }
            }
            else
            {
                AddBlankRowToGridList();
            }

            //this.listDetails[1].DebitCredit = DebitCreditEnum.Credit;
        }

        private void CheckAndAddGridBlankRow2()
        {

            int RowCheck = 1;
            //adddtion
            int cntAdd = listDetails.Where(c=>c.DrCr == (int)DebitCreditEnum.Credit).Count();

            //AddBlankRowToGridList();

            if (cntAdd < RowCheck)
            {
                int diffAdd = RowCheck - cntAdd;
                for (int i = 0; i < diffAdd; i++)
                {
                    AddBlankRowToGridList2();
                }
            }
            else
            {
                AddBlankRowToGridList2();
            }

            //this.listDetails[1].DebitCredit = DebitCreditEnum.Credit;
        }


        private void AddBlankRowToGridList()
        {
            dcJournalDet cObj = new dcJournalDet();
            cObj._RecordState = RecordStateEnum.Added;
            cObj.DebitCredit = DebitCreditEnum.Debit;
            cObj.JournalDetID = 0;
            cObj.JournalID = this.JournalID;
            cObj.JournalDetSLNo = this.listDetails.Where(c => c._RecordState != RecordStateEnum.Deleted).Count() + 1;
            //cObj.JournalDetID_Link = (this.listDetails.DefaultIfEmpty(new dcJournalDet()).Max(c => c.JournalDetID_Link)) + 1;
            cObj.JournalDetID_Link = this.listDetails.DefaultIfEmpty().Max(c => c == null ? 0 : c.JournalDetID_Link) + 1;
            this.listDetails.Add(cObj);
        }

        private void AddBlankRowToGridList2()
        {
            dcJournalDet cObj = new dcJournalDet();
            cObj._RecordState = RecordStateEnum.Added;
            cObj.DebitCredit = DebitCreditEnum.Credit;
            cObj.JournalDetID = 0;
            cObj.JournalID = this.JournalID;
            cObj.JournalDetSLNo = this.listDetails.Where(c => c._RecordState != RecordStateEnum.Deleted
                                                            && c.DrCr == (int)DebitCreditEnum.Credit).Count() + 1;
            //cObj.JournalDetID_Link = (this.listDetails.DefaultIfEmpty(new dcJournalDet()).Max(c => c.JournalDetID_Link)) + 1;
            cObj.JournalDetID_Link = this.listDetails.DefaultIfEmpty().Max(c => c == null ? 0 : c.JournalDetID_Link) + 1;


            this.listDetails.Add(cObj);
        }


        private dcJournal ReadJouranlFromUI()
        {
            dcJournal cObj = new dcJournal();

            bool isAdd = false;

            //cObj.CompanyID = Convert.ToInt32(ddlCompany.SelectedValue);

            cObj.CompanyID = this.CompanyID;
            cObj.JournalID = this.JournalID;
            cObj.AccYearID = Convert.ToInt32(ddlAccYear.SelectedValue);


            cObj.JournalTypeID = Convert.ToInt32(ddlJournalType.SelectedValue);
            cObj.LocationID = Convert.ToInt32(ddlLocation.SelectedValue);
            cObj.IsEditable = true;

            cObj.JournalAdjustTypeID = Convert.ToInt32(ddlJournalAdjustType.SelectedValue);

            ////now save;

            isAdd = this.EditMode == FormDataMode.Add ? true : false;

            cObj.JournalDate = Conversion.StringToDate(txtJournalDate.Text);
            cObj.JournalNo = txtJournalNo.Text;

            dcUser user = AppSecurity.GetUserInfoFromSession();
            if (isAdd)
            {
                cObj.AddDate = DateTime.Today;
                cObj.AddUserID = user.UserID;
                cObj.JournalUpdateNo = 0;
            }
            else
            {
                cObj._ChangedList.Remove("JournalNo");
                cObj.JournalUpdateNo = Convert.ToInt32(hdnJournalUpdateNo.Value) + 1;
            }

            cObj.JournalDesc = txtJournalDesc.Text;

            cObj.JournalAmt = Conversion.StringToDecimal(txtTotDebitAmt.Text);


            dcJournalType jrType = JournalTypeBL.GetJournalTypeByID(this.CompanyID, cObj.JournalTypeID);
            if (jrType != null)
            {
                if (jrType.IsAutoPost)
                {
                    cObj.IsPosted = true;
                    cObj.PostedDate = DateTime.Today;
                }

                IsAutoPrint = jrType.IsAutoPrint;
            }

            cObj.EditDate = DateTime.Today;
            cObj.EditUserID = user.UserID;
            cObj.EditUserName = user.UserName;

            return cObj;
        }

        private void ReadDetailsFromGrid()
        {
            this.listDetails.Clear();

            foreach (GridViewRow gvR in this.GridView1.Rows)
            {
                if (gvR.RowType == DataControlRowType.DataRow)
                {
                    //DataRow dRow = this.dtGrid.NewRow();
                    dcJournalDet cObj = new dcJournalDet();
                    //cObj.SalaryHeadType_sopt = (int)SalaryHeadTypeEnum.Addition;
                    ReadGridRowToObject(gvR, this.GridView1.DataKeys, cObj);

                    this.listDetails.Add(cObj);
                }
            }

            foreach (GridViewRow gvR in this.GridView2.Rows)
            {
                if (gvR.RowType == DataControlRowType.DataRow)
                {
                    //DataRow dRow = this.dtGrid.NewRow();
                    dcJournalDet cObj = new dcJournalDet();
                    //cObj.SalaryHeadType_sopt = (int)SalaryHeadTypeEnum.Addition;
                    ReadGridRowToObject2(gvR, this.GridView1.DataKeys, cObj);

                    this.listDetails.Add(cObj);
                }
            }

        }

        private void ReadGridRowToObject(GridViewRow gvR, DataKeyArray dataKeys, dcJournalDet cObj)
        {
            string strD;

            //strD = this.GridView1.DataKeys[gvR.RowIndex]["_RecordState"].ToString();
            ////strD = dataKeys[gvR.RowIndex]["_RecordState"].ToString();
            ////decimal.TryParse(strD == string.Empty ? "0" : strD, out d);
            ////cObj._RecordState = (RecordStateEnum)Convert.ToInt32(d);
            //cObj._RecordState = (RecordStateEnum)dataKeys[gvR.RowIndex]["_RecordState"];
            
            cObj.JournalID = this.JournalID;

            strD = ((HiddenField)gvR.FindControl("hdnJournalDetID")).Value;
            cObj.JournalDetID = Conversion.StringToInt(strD);
            
            strD = ((HiddenField)gvR.FindControl("hdnRecordStateInt")).Value;
            cObj._RecordStateInt = Conversion.StringToInt(strD);

            strD = ((HiddenField)gvR.FindControl("hdnJournalDetID_Link")).Value;
            cObj.JournalDetID_Link = Conversion.StringToInt(strD);

            strD = ((Label)gvR.FindControl("lblSLNo")).Text;
            cObj.JournalDetSLNo = Conversion.StringToInt(strD);

            strD = ((HiddenField)gvR.FindControl("hdnDrCr")).Value;
            cObj.DrCr = Conversion.StringToInt(strD);


            strD = ((HiddenField)gvR.FindControl("hdnGLAccountID")).Value;
            cObj.GLAccountID = Conversion.StringToInt(strD);

            strD = ((HiddenField)gvR.FindControl("hdnGLAccountIDEdit")).Value;
            cObj.GLAccountIDEdit = Conversion.StringToInt(strD);


            strD = ((HiddenField)gvR.FindControl("hdnGLGroupID")).Value;
            cObj.GLGroupID = Conversion.StringToInt(strD);

            strD = ((HiddenField)gvR.FindControl("hdnGLGroupIDEdit")).Value;
            cObj.GLGroupIDEdit = Conversion.StringToInt(strD);


            //strD = ((HiddenField)gvR.FindControl("hdnGLClassID")).Value;
            //cObj.GLClassID = Conversion.StringToInt(strD);

            strD = ((HiddenField)gvR.FindControl("hdnGLGroupClassID")).Value;
            cObj.GLGroupClassID = Conversion.StringToInt(strD);

            strD = ((HiddenField)gvR.FindControl("hdnIsInstrument")).Value;
            cObj.IsInstrument = Conversion.StringToBool(strD);


            strD = ((TextBox)gvR.FindControl("txtGLGroupCode")).Text;
            cObj.GLGroupCode = strD;

            strD = ((TextBox)gvR.FindControl("txtGLGroupName")).Text;
            cObj.GLGroupNameShort = strD;

            //strD = ((TextBox)gvR.FindControl("txtGLGroup")).Text;
            //cObj.GLGroupNameShortName = strD;



            strD = ((TextBox)gvR.FindControl("txtGLAccountCode")).Text;
            cObj.GLAccountCode = strD;

            //strD = ((Label)gvR.FindControl("lblGLAccountName")).Text;
            //cObj.AccGLAccountName = strD;

            strD = ((TextBox)gvR.FindControl("txtGLAccountName")).Text;
            cObj.GLAccountName = strD;

            strD = ((HiddenField)gvR.FindControl("hdnTranTypeID")).Value;
            cObj.TranTypeID = Convert.ToInt32(strD);

            strD = ((TextBox)gvR.FindControl("txtTranType")).Text;
            cObj.TranTypeCode = strD;

            strD = ((HiddenField)gvR.FindControl("hdnTranTypeCategoryID")).Value;
            cObj.TranTypeCategoryID = Convert.ToInt32(strD);

            strD = ((TextBox)gvR.FindControl("txtJournalDetDesc")).Text;
            cObj.JournalDetDesc = strD;


            strD = ((TextBox)gvR.FindControl("txtDebitAmt")).Text;
            cObj.DebitAmt = Conversion.StringToDecimal(strD);

            //strD = ((TextBox)gvR.FindControl("txtCreditAmt")).Text;
            //cObj.CreditAmt = Conversion.StringToDecimal(strD);

            strD = ((TextBox)gvR.FindControl("txtInstrument")).Text;
            cObj.InstrumentText = strD;

            strD = ((TextBox)gvR.FindControl("txtCostCenter")).Text;
            cObj.CostCenterText = strD;

            strD = ((TextBox)gvR.FindControl("txtReference")).Text;
            cObj.ReferenceText = strD;


            if (!gvR.Visible)
            {
                cObj._RecordState = RecordStateEnum.Deleted;
            }

        }

        private void ReadGridRowToObject2(GridViewRow gvR, DataKeyArray dataKeys, dcJournalDet cObj)
        {
            string strD;

            //strD = this.GridView1.DataKeys[gvR.RowIndex]["_RecordState"].ToString();
            ////strD = dataKeys[gvR.RowIndex]["_RecordState"].ToString();
            ////decimal.TryParse(strD == string.Empty ? "0" : strD, out d);
            ////cObj._RecordState = (RecordStateEnum)Convert.ToInt32(d);
            //cObj._RecordState = (RecordStateEnum)dataKeys[gvR.RowIndex]["_RecordState"];

            cObj.JournalID = this.JournalID;

            strD = ((HiddenField)gvR.FindControl("hdnJournalDetID")).Value;
            cObj.JournalDetID = Conversion.StringToInt(strD);

            strD = ((HiddenField)gvR.FindControl("hdnRecordStateInt")).Value;
            cObj._RecordStateInt = Conversion.StringToInt(strD);

            strD = ((HiddenField)gvR.FindControl("hdnJournalDetID_Link")).Value;
            cObj.JournalDetID_Link = Conversion.StringToInt(strD);

            strD = ((Label)gvR.FindControl("lblSLNo")).Text;
            cObj.JournalDetSLNo = Conversion.StringToInt(strD);

            strD = ((HiddenField)gvR.FindControl("hdnDrCr")).Value;
            cObj.DrCr = Conversion.StringToInt(strD);


            strD = ((HiddenField)gvR.FindControl("hdnGLAccountID")).Value;
            cObj.GLAccountID = Conversion.StringToInt(strD);

            strD = ((HiddenField)gvR.FindControl("hdnGLAccountIDEdit")).Value;
            cObj.GLAccountIDEdit = Conversion.StringToInt(strD);


            strD = ((HiddenField)gvR.FindControl("hdnGLGroupID")).Value;
            cObj.GLGroupID = Conversion.StringToInt(strD);

            strD = ((HiddenField)gvR.FindControl("hdnGLGroupIDEdit")).Value;
            cObj.GLGroupIDEdit = Conversion.StringToInt(strD);


            //strD = ((HiddenField)gvR.FindControl("hdnGLClassID")).Value;
            //cObj.GLClassID = Conversion.StringToInt(strD);

            strD = ((HiddenField)gvR.FindControl("hdnGLGroupClassID")).Value;
            cObj.GLGroupClassID = Conversion.StringToInt(strD);

            strD = ((HiddenField)gvR.FindControl("hdnIsInstrument")).Value;
            cObj.IsInstrument = Conversion.StringToBool(strD);


            strD = ((TextBox)gvR.FindControl("txtGLGroupCode")).Text;
            cObj.GLGroupCode = strD;

            strD = ((TextBox)gvR.FindControl("txtGLGroupName")).Text;
            cObj.GLGroupNameShort = strD;

            //strD = ((TextBox)gvR.FindControl("txtGLGroupName")).Text;
            //cObj.GLGroupNameShort = strD;

            strD = ((TextBox)gvR.FindControl("txtGLAccountCode")).Text;
            cObj.GLAccountCode = strD;

            //strD = ((Label)gvR.FindControl("lblGLAccountName")).Text;
            //cObj.AccGLAccountName = strD;

            strD = ((TextBox)gvR.FindControl("txtGLAccountName")).Text;
            cObj.GLAccountName = strD;

            strD = ((HiddenField)gvR.FindControl("hdnTranTypeID")).Value;
            cObj.TranTypeID = Convert.ToInt32(strD);

            strD = ((TextBox)gvR.FindControl("txtTranType")).Text;
            cObj.TranTypeCode = strD;

            strD = ((HiddenField)gvR.FindControl("hdnTranTypeCategoryID")).Value;
            cObj.TranTypeCategoryID = Convert.ToInt32(strD);

            strD = ((TextBox)gvR.FindControl("txtJournalDetDesc")).Text;
            cObj.JournalDetDesc = strD;


            //strD = ((TextBox)gvR.FindControl("txtDebitAmt")).Text;
            //cObj.DebitAmt = Conversion.StringToDecimal(strD);

            strD = ((TextBox)gvR.FindControl("txtCreditAmt")).Text;
            cObj.CreditAmt = Conversion.StringToDecimal(strD);

            strD = ((TextBox)gvR.FindControl("txtInstrument")).Text;
            cObj.InstrumentText = strD;

            strD = ((TextBox)gvR.FindControl("txtCostCenter")).Text;
            cObj.CostCenterText = strD;

            strD = ((TextBox)gvR.FindControl("txtReference")).Text;
            cObj.ReferenceText = strD;


            if (!gvR.Visible)
            {
                cObj._RecordState = RecordStateEnum.Deleted;
            }

        }



        private void ReadDetRefListFromJSon()
        {
            List<dcJournalDetRef> detRefList = new List<dcJournalDetRef>();

            //string jsonData = TextBox1.Text;
            string jsonData = hdnJournalDetRefJson.Value;
            string jsonData2 = hdnJournalDetRefJson2.Value;

            JavaScriptSerializer jss = new JavaScriptSerializer();
            jsonData = HttpUtility.HtmlDecode(jsonData);
            jsonData2 = HttpUtility.HtmlDecode(jsonData2);

            //jsonData = EncodeJsString(jsonData);
            //jsonData = jsonData.Replace("\\", "\\\\");
            //jsonData = jsonData.Replace("\"", "\\\"");

            List<jsonDetailsRef> jsonList = jss.Deserialize<List<jsonDetailsRef>>(jsonData);
            jsonList.AddRange(jss.Deserialize<List<jsonDetailsRef>>(jsonData2));

            foreach (jsonDetailsRef jObj in jsonList)
            {
                dcJournalDetRef detRef = new dcJournalDetRef();
                detRef.JournalDetRefID = jObj.journaldetrefid;
                detRef.JournalDetID = jObj.journaldetid;
                detRef.JournalDetID_Link = jObj.linkid;
                detRef._RecordStateInt = jObj._recordstateint;

                detRef.AccRefID = jObj.id;
                detRef.AccRefCode = jObj.code;
                detRef.AccRefName = jObj.name;

                detRef.AccRefTypeID = jObj.typeid;
                detRef.AccRefTypeCode = jObj.typecode;
                detRef.AccRefTypeName = jObj.typename;

                detRef.AccRefCategoryID = jObj.categoryid;
                detRef.AccRefCategoryCode = jObj.categorycode;
                detRef.AccRefCategoryName = jObj.categoryname;

                //detRef.DebitAmt = jObj.debitamt;
                //detRef.CreditAmt = jObj.creditamt;

                detRef.DrCr = jObj.drcr;

                detRef.Amt = jObj.amt;
                detRef.DebitAmt = detRef.DrCr == (int)DebitCreditEnum.Debit ? detRef.Amt : 0;
                detRef.CreditAmt = detRef.DrCr == (int)DebitCreditEnum.Credit ? detRef.Amt : 0;
                detRef.TranAmt = detRef.DebitAmt - detRef.CreditAmt;

                detRef.AccRefNo = jObj.accrefno;
                detRef.AccRefRemarks = jObj.accrefremarks;

                detRefList.Add(detRef);
            }

            this.listDetailsRef = detRefList;
        }

        private void ValidateDetRefList(List<dcJournalDetRef> detRefList)
        {
            List<dcJournalDetRef> newDetRefList = new List<dcJournalDetRef>();

            foreach (dcJournalDet det in this.listDetails)
            {
                List<dcJournalDetRef> detRefDList = detRefList.Where(c => c.JournalDetID_Link == det.JournalDetID_Link).ToList();
                int detID = det.JournalDetID;

                switch (det._RecordState)
                {
                    case RecordStateEnum.Deleted:
                        foreach (dcJournalDetRef detRefD in detRefDList)
                        {
                            detRefD.DrCr = det.DrCr;
                            detRefD.DebitAmt = det.DebitAmt > 0 ? detRefD.DebitAmt : 0;
                            detRefD.CreditAmt = det.CreditAmt > 0 ? detRefD.CreditAmt : 0;
                            detRefD.TranAmt = detRefD.DebitAmt - detRefD.CreditAmt;

                            if (detRefD.JournalDetRefID > 0)
                            {
                                detRefD._RecordState = RecordStateEnum.Deleted;
                                newDetRefList.Add(detRefD);
                            }
                        }
                        break;
                    case RecordStateEnum.Added:
                        foreach (dcJournalDetRef detRefA in detRefDList)
                        {
                            if (detRefA.AccRefID > 0)
                            {
                                detRefA._RecordState = RecordStateEnum.Added;
                                detRefA.JournalDetID = det.JournalDetID;
                                detRefA.DrCr = det.DrCr;
                                detRefA.DebitAmt = det.DebitAmt > 0 ? detRefA.DebitAmt : 0;
                                detRefA.CreditAmt = det.CreditAmt > 0 ? detRefA.CreditAmt : 0;
                                detRefA.TranAmt = detRefA.DebitAmt - detRefA.CreditAmt;
                                newDetRefList.Add(detRefA);
                            }
                        }
                        break;
                    case RecordStateEnum.Edited:
                        foreach (dcJournalDetRef detRefE in detRefDList)
                        {
                            detRefE.DrCr = det.DrCr;
                            detRefE.DebitAmt = det.DebitAmt > 0 ? detRefE.DebitAmt : 0;
                            detRefE.CreditAmt = det.CreditAmt > 0 ? detRefE.CreditAmt : 0;
                            detRefE.TranAmt = detRefE.DebitAmt - detRefE.CreditAmt;

                            if (detRefE._RecordState == RecordStateEnum.Deleted)
                            {
                                if (detRefE.JournalDetRefID > 0)
                                {
                                    detRefE._RecordState = RecordStateEnum.Deleted;
                                    newDetRefList.Add(detRefE);
                                }

                            }
                            else
                            {
                                if (detRefE.AccRefID > 0)
                                {
                                    if (detRefE.JournalDetRefID > 0)
                                    {
                                        detRefE._RecordState = RecordStateEnum.Edited;
                                        newDetRefList.Add(detRefE);
                                    }
                                    else
                                    {
                                        detRefE._RecordState = RecordStateEnum.Added;
                                        newDetRefList.Add(detRefE);
                                    }
                                }
                                else
                                {
                                    if (detRefE.JournalDetRefID > 0)
                                    {
                                        detRefE._RecordState = RecordStateEnum.Deleted;
                                        newDetRefList.Add(detRefE);
                                    }
                                }
                            }
                        }
                        break;
                } //switch
            } //for det loop

            this.listDetailsRef = newDetRefList;
        }

        private void UpdateDetRefFromDetSingle(List<dcJournalDetRef> detRefList)
        {
            //List<dcJournalDetRef> newDetRefList = 

            foreach (dcJournalDet det in this.listDetails)
            {
                List<dcJournalDetRef> detRefListLink = detRefList.Where(c => c.JournalDetID_Link == det.JournalDetID_Link).ToList();
                int detID = det.JournalDetID;

                ///Check Tran Type
                ///

                List<dcJournalDetRef> detRefListTranType = detRefListLink.Where(c => c.AccRefTypeID == (int)AccRefTypeEnum.TranCode).ToList();
                foreach(dcJournalDetRef detRef in detRefListTranType)
                {
                    if (detRef._RecordState != RecordStateEnum.Deleted)
                    {
                        detRef.DebitAmt = det.DebitAmt;
                        detRef.CreditAmt = det.CreditAmt;
                        detRef.TranAmt = detRef.DebitAmt - detRef.CreditAmt;
                    }
                }
            } //det loop

            this.listDetailsRef = detRefList;
        }

        private void UpdatDetailsRefList()
        {
            foreach (dcJournalDet det in this.listDetails)
            {
                if (det.JournalDetRefList == null)
                {
                    det.JournalDetRefList = new List<dcJournalDetRef>();
                }
                List<dcJournalDetRef> detRefDList = this.listDetailsRef.Where(c => c.JournalDetID_Link == det.JournalDetID_Link).ToList();
                det.JournalDetRefList.AddRange(detRefDList);
            }
        }


        private void ReadDetInsListFromJSon()
        {
            List<dcJournalDetIns> detInsList = new List<dcJournalDetIns>();

            //string jsonData = TextBox1.Text;
            string jsonData = hdnJournalDetInsJson.Value;
            string jsonData2 = hdnJournalDetInsJson2.Value;

            JavaScriptSerializer jss = new JavaScriptSerializer();
            jsonData = HttpUtility.HtmlDecode(jsonData);
            jsonData2 = HttpUtility.HtmlDecode(jsonData2);

            //jsonData = EncodeJsString(jsonData);
            //jsonData = jsonData.Replace("\\", "\\\\");
            //jsonData = jsonData.Replace("\"", "\\\"");

            List<jsonDetailsIns> jsonList = jss.Deserialize<List<jsonDetailsIns>>(jsonData);
            jsonList.AddRange(jss.Deserialize<List<jsonDetailsIns>>(jsonData2));


            foreach (jsonDetailsIns jObj in jsonList)
            {
                dcJournalDetIns detIns = new dcJournalDetIns();
                detIns.JournalDetInsID = jObj.journaldetinsid;
                detIns.JournalDetID = jObj.journaldetid;
                detIns.JournalDetID_Link = jObj.linkid;
                detIns._RecordStateInt = jObj._recordstateint;

                detIns.InstrumentID = jObj.insid;
                detIns.InstrumentNo = jObj.insno;
                detIns.InstrumentDate = Conversion.StringToDateORNull(jObj.insdate);

                detIns.InstrumentLinkTypeID = jObj.inslinktypeid;

                detIns.DrCr = jObj.drcr;
                detIns.InsTranAmt = jObj.amt;


                detInsList.Add(detIns);
            }

            this.listDetailsIns = detInsList;
        }

        private void ValidateDetInsList(List<dcJournalDetIns> detInsList)
        {
            List<dcJournalDetIns> newDetInsList = new List<dcJournalDetIns>();

            foreach (dcJournalDet det in this.listDetails)
            {
                List<dcJournalDetIns> detInsDList = detInsList.Where(c => c.JournalDetID_Link == det.JournalDetID_Link).ToList();
                int detID = det.JournalDetID;

                switch (det._RecordState)
                {
                    case RecordStateEnum.Deleted:
                        foreach (dcJournalDetIns detInsD in detInsDList)
                        {
                            if (detInsD.JournalDetInsID > 0)
                            {
                                detInsD._RecordState = RecordStateEnum.Deleted;
                                newDetInsList.Add(detInsD);
                            }
                        }
                        break;
                    case RecordStateEnum.Added:
                        foreach (dcJournalDetIns detInsA in detInsDList)
                        {
                            if (detInsA.InstrumentID > 0)
                            {
                                detInsA._RecordState = RecordStateEnum.Added;
                                detInsA.JournalDetID = det.JournalDetID;

                                detInsA.DrCr = det.DrCr;
                                detInsA.DebitAmt = det.DrCr == (int)DebitCreditEnum.Debit ? detInsA.InsTranAmt : 0;
                                detInsA.CreditAmt = det.DrCr == (int)DebitCreditEnum.Credit ? detInsA.InsTranAmt : 0;
                                detInsA.TranAmt = detInsA.DebitAmt - detInsA.CreditAmt;


                                newDetInsList.Add(detInsA);
                            }
                        }
                        break;
                    case RecordStateEnum.Edited:
                        foreach (dcJournalDetIns detInsE in detInsDList)
                        {
                            detInsE.DrCr = det.DrCr;
                            detInsE.DebitAmt = det.DrCr == (int)DebitCreditEnum.Debit ? detInsE.InsTranAmt : 0;
                            detInsE.CreditAmt = det.DrCr == (int)DebitCreditEnum.Credit ? detInsE.InsTranAmt : 0;
                            detInsE.TranAmt = detInsE.DebitAmt - detInsE.CreditAmt;


                            //set record sate
                            if (detInsE._RecordState == RecordStateEnum.Deleted)
                            {
                                if (detInsE.JournalDetInsID > 0)
                                {
                                    detInsE._RecordState = RecordStateEnum.Deleted;
                                    newDetInsList.Add(detInsE);
                                }

                            }
                            else
                            {
                                if (detInsE.InstrumentID > 0)
                                {
                                    if (detInsE.JournalDetInsID > 0)
                                    {
                                        detInsE._RecordState = RecordStateEnum.Edited;
                                        newDetInsList.Add(detInsE);
                                    }
                                    else
                                    {
                                        detInsE._RecordState = RecordStateEnum.Added;
                                        newDetInsList.Add(detInsE);
                                    }
                                }
                                else
                                {
                                    if (detInsE.JournalDetInsID > 0)
                                    {
                                        detInsE._RecordState = RecordStateEnum.Deleted;
                                        newDetInsList.Add(detInsE);
                                    }
                                }
                            }
                        }
                        break;
                } //switch
            } //for det loop

            this.listDetailsIns = newDetInsList;
        }

        private void UpdatDetailsInsList()
        {
            foreach (dcJournalDet det in this.listDetails)
            {
                if (det.JournalDetInsList == null)
                {
                    det.JournalDetInsList = new List<dcJournalDetIns>();
                }
                List<dcJournalDetIns> detInsDList = this.listDetailsIns.Where(c => c.JournalDetID_Link == det.JournalDetID_Link).ToList();
                det.JournalDetInsList.AddRange(detInsDList);
            }
        }


       private bool CheckData()
        {
            if (this.JournalType.AccSLNoID == 0)
            {
                if (txtJournalNo.Text.Trim() == string.Empty)
                {
                    //Helper.SetStatusMessage(lblMessage, "Please Enter Defination Name", MessageTypeEnum.InvalidData);
                    this.SetPageMessage("Please Enter Journal No", MessageTypeEnum.InvalidData);
                    txtJournalNo.Focus();
                    return false;
                }
            }

            int yearID = Convert.ToInt32(ddlAccYear.SelectedValue);
            if (yearID == 0)
            {
                //Helper.SetStatusMessage(lblMessage, "Please Enter Defination Name", MessageTypeEnum.InvalidData);
                this.SetPageMessage("Please Select Year", MessageTypeEnum.InvalidData);
                ddlAccYear.Focus();
                return false;
            }

            int locationID = Convert.ToInt32(ddlLocation.SelectedValue);
            if (locationID == 0)
            {
                //Helper.SetStatusMessage(lblMessage, "Please Enter Defination Name", MessageTypeEnum.InvalidData);
                this.SetPageMessage("Please Select Location", MessageTypeEnum.InvalidData);
                ddlLocation.Focus();
                return false;
            }

            DateTime dt;
            if (!DateTime.TryParse(txtJournalDate.Text, out dt))
            {
                //Helper.SetStatusMessage(lblMessage, "Please Enter Defination Name", MessageTypeEnum.InvalidData);
                this.SetPageMessage("Please Enter Date", MessageTypeEnum.InvalidData);
                txtJournalDate.Focus();
                return false;
            }
            if (EditMode == FormDataMode.Add)
            {
                base.CheckPermissionAdd();
            }
            else if (EditMode == FormDataMode.Edit)
            {
                base.CheckPermissionEdit();
            }

            return true;
        }


       private bool ValidateDataJournal(dcJournal jrnl)
       {
           bool bStatus = false;

           JournalValidationTask vTask = new JournalValidationTask();
           vTask.DBValidationType = JournalValidtationTypeSet.Journal;
           vTask.Journal = jrnl;

           JournalValidationResult vResult = new JournalValidationResult();
           vResult = JournalValidationBL.ValidateJournalMain(vTask);
           
           if (vResult.IsError)
           {
               saveMsg = vResult.ErrorString;
               return false;
           }
           else
           {
               bStatus = true;
           }
           
           return bStatus;
       }

       private bool ValidateDataJournalFull(dcJournal jrnl)
       {
           bool bStatus = false;

           JournalValidationTask vTask = new JournalValidationTask();
           vTask.DBValidationType = JournalValidtationTypeSet.Journal;
           vTask.Journal = jrnl;
           vTask.UseDetIDLink = true;

           JournalValidationResult vResult = new JournalValidationResult();
           vResult = JournalValidationBL.ValidateJournalFull(vTask);

           if (vResult.IsError)
           {
               saveMsg = vResult.ErrorString;
               return false;
           }
           else
           {
               bStatus = true;
           }

           return bStatus;
       }


        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            //Helper.SetStatusMessage(lblMessage, "", MessageTypeEnum.Successful);
            AddTask();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveTask();
        }

        private Boolean IsGridRowValid(dcJournalDet cObj)
        {
            bool isValid = false;

            if (cObj.GLAccountID > 0)
            {
                isValid = true;
            }
            return isValid;
        }

        private void ValidateGridList(List<dcJournalDet> cList)
        {
            List<int> delIndex = new List<int>();
            List<dcJournalDet> nList = new List<dcJournalDet>();
            foreach (dcJournalDet cObj in cList)
            {
                int detID = cObj.JournalDetID;
                if (cObj._RecordState == RecordStateEnum.Deleted)
                {
                    if (detID > 0)
                    {
                        nList.Add(cObj);
                    }
                }
                else
                {
                    bool isRowValid = IsGridRowValid(cObj);
                    if (isRowValid)
                    {
                        if (detID > 0)
                        {
                            //edited
                            cObj._RecordState = RecordStateEnum.Edited;
                            //dRow["RState"] = "edited";
                        }
                        else
                        {
                            //added
                            cObj._RecordState = RecordStateEnum.Added;
                            //dRow["RState"] = "added";
                        }

                        //dtUpdate.ImportRow(dRow);
                        nList.Add(cObj);
                    }
                    else
                    {
                        if (detID > 0)
                        {
                            cObj._RecordState = RecordStateEnum.Deleted;
                            nList.Add(cObj);
                            //dRow["RState"] = "deleted";
                            //dtUpdate.ImportRow(dRow);
                        }
                        else
                        {
                            //now use less
                            delIndex.Add(cList.IndexOf(cObj));
                            //delIndex.Add(dtToValid.Rows.IndexOf(dRow));
                        }
                    }
                }

            }

            this.listDetails = nList;
        }


        public void SaveTask()
        {
            ReadJournalType();

            if (!Page.IsValid)
            { return; }

            if (!CheckData())
            {
                //this.SetPageMessage(saveMsg, MessageTypeEnum.Error);
                base.ShowPageMessage(lblMessage, true);
                return;
            }

            if (SaveData())
            {
                this.IsDirty = false;
                SetHyperLink();
                //Helper.SetStatusMessage(lblMessage, saveMsg, MessageTypeEnum.Successful);
                this.SetPageMessage(saveMsg, MessageTypeEnum.Successful);
                this.SetPageMessageToSession();
                //string redirectURL = "~/Project/Land.aspx?id=" + this.JournalID.ToString() + "&" + AppMessage.CreateQueryString(this.AppMessageID);

                string redirectURL = "~/Accounting/GeneralLedger/Journal.aspx?id=" + this.JournalID.ToString();
                redirectURL = base.SetPageTabQueryString(redirectURL);
                redirectURL = base.SetPageMessageQueryString(redirectURL, this.AppMessageID);
                if (IsAutoPrint)
                {
                    string printFormat = ddlReportFormat.SelectedValue;
                    redirectURL += "&print=1" + string.Format("&printformat={0}", printFormat);
                }


                Response.Redirect(redirectURL,false);

                //EditTask();
            }
            else
            {
                //  Helper.SetStatusMessage(lblMessage, saveMsg, MessageTypeEnum.Error);
                this.SetPageMessage(saveMsg, MessageTypeEnum.Error);
                base.ShowPageMessage(lblMessage, true);
            }
            //base.ShowPageMessage(lblMessage, true);
        }

        private DBValidationResult CheckDataAccRef(AccRefTypeEnum pAccRefType, dcJournalDet jDet, List<dcJournalDetRef> detRefList)
        {
            DBValidationResult vResult = new DBValidationResult();

            string strDebitCredit = jDet.DebitCredit == DebitCreditEnum.Debit ? "Debit" : "Credit";
            string strRefType = AccRefTypeBL.GetTextFromEnum(pAccRefType);


            List<dcJournalDetRef> refListType = detRefList.Where(c => c.AccRefTypeID == (int)pAccRefType
                                                                     && c._RecordState != RecordStateEnum.Deleted).ToList();

            dcAccRefSettings accRefSettings_Type = AccRefSettingsList.Where(c => c.AccRefTypeID == (int)pAccRefType).FirstOrDefault();

            List<dcGLAccountRefCategory> glAccRefCategoryList = GLAccountRefCategoryBL.GetAccountRefCategoryList(jDet.GLAccountID, (int)pAccRefType);


            //group by category
            var rsListSumCategory = from r in refListType
                                    group r by r.AccRefCategoryID into grps
                                    select new
                                    {
                                        Key = grps.Key,
                                        DebitAmt = grps.Sum(c => c.DebitAmt),
                                        CreditAmt = grps.Sum(c => c.CreditAmt),
                                        TotRec = grps.Count(),
                                        GroupList = grps,
                                    };

            //Check GL Account Mandatory/non optional Category
            List<dcGLAccountRefCategory> accCatMandatory = glAccRefCategoryList.Where(c => c.IsMandatory == true).ToList();
            if (accCatMandatory.Count > 0)
            {
                bool isCatExists = false;
                string nonMatchedCat = string.Empty;
                foreach (dcGLAccountRefCategory accCat in accCatMandatory)
                {
                    // accCatMandatory.Where(c=>c.AccRefCategoryID == rsSumCategory.Key).Count()
                    var catList = rsListSumCategory.Where(c => c.Key == accCat.AccRefCategoryID);
                    if (catList.Count() > 0)
                    {
                        isCatExists = true;
                    }
                    else
                    {
                        isCatExists = false;
                        nonMatchedCat = accCat.AccRefCategoryName;
                        break;
                    }
                }
                if (isCatExists == false)
                {
                    vResult.IsError = true;
                    vResult.ErrorString = string.Format("Mandatory {2} Categroy Not Entered, {1} Line No: {0}", jDet.JournalDetSLNo, strDebitCredit, strRefType);
                    return vResult;
                }
            } //mandatory Categroy


            if (!accRefSettings_Type.AllowMultipleCategory)
            {
                if (rsListSumCategory.Count() > 1)
                {
                    vResult.IsError = true;
                    vResult.ErrorString = string.Format("Multiple {2} Category Not Allowed, {1} Line No: {0}", jDet.JournalDetSLNo, strDebitCredit, strRefType);
                    return vResult;
                }

            }

            if (accRefSettings_Type.TotalSumCheckByCtategory)
            {
                foreach (var rsSumCategory in rsListSumCategory)
                {
                    string catName = rsSumCategory.GroupList.FirstOrDefault().AccRefCategoryName;
                    if (jDet.DebitCredit == DebitCreditEnum.Debit)
                    {
                        if (jDet.DebitAmt != rsSumCategory.DebitAmt)
                        {
                            vResult.IsError = true;
                            vResult.ErrorString = string.Format("{2} Category: [{1}] Debit Amount Not equal For Line No: {0}", jDet.JournalDetSLNo, catName, strRefType);
                            return vResult;
                        }
                    }
                    if (jDet.DebitCredit == DebitCreditEnum.Credit)
                    {
                        if (jDet.CreditAmt != rsSumCategory.CreditAmt)
                        {
                            vResult.IsError = true;
                            vResult.ErrorString = string.Format("{2} Category: [{1}] Credit Amount Not equal For Line No: {0}", jDet.JournalDetSLNo, catName, strRefType);
                            return vResult;
                        }
                    }
                } //foreach cost cat

            }
            else
            {
                if (jDet.DebitCredit == DebitCreditEnum.Debit)
                {
                    decimal debitSum = refListType.Sum(c => c.DebitAmt);
                    if (jDet.DebitAmt != debitSum)
                    {
                        vResult.IsError = true;
                        vResult.ErrorString = string.Format("{1} Debit Amount Not equal For Line No: {0}", jDet.JournalDetSLNo, strRefType);
                        return vResult;

                    }
                }

                if (jDet.DebitCredit == DebitCreditEnum.Credit)
                {
                    decimal creditSum = refListType.Sum(c => c.CreditAmt);
                    if (jDet.CreditAmt != creditSum)
                    {
                        vResult.IsError = true;
                        vResult.ErrorString = string.Format("{1} Credit Amount Not equal For Line No: {0}", jDet.JournalDetSLNo, strRefType);
                        return vResult;
                    }
                }

            } //TotalSumCheckByCtategory


            vResult.IsError = false;
            vResult.ErrorString = "";
            return vResult;
        }



        private bool SaveData()
        {
            dcJournal cObj = ReadJouranlFromUI();

            //get the details data
            ReadDetailsFromGrid();
            ValidateGridList(this.listDetails);
            JournalDetBL.UpdateSLNo(this.listDetails, true);

            ReadDetInsListFromJSon();
            ValidateDetInsList(this.listDetailsIns);
            JournalDetInsBL.UpdateSLNo(this.listDetailsIns);
            UpdatDetailsInsList();

            ReadDetRefListFromJSon();
            ValidateDetRefList(this.listDetailsRef);
            UpdateDetRefFromDetSingle(this.listDetailsRef);
            JournalDetRefBL.UpdateSLNo(this.listDetailsRef);
            UpdatDetailsRefList();

            cObj.JournalDetList = this.listDetails;
            cObj.JournalDetInsList = this.listDetailsIns;
            cObj.JournalDetRefList = this.listDetailsRef;

            //if (CheckDataDetails() == false)
            //{
            //    return false;
            //}

            if (ValidateDataJournalFull(cObj) == false)
            {
                return false;
            }


            bool bStatus = false;
            int newJournalID = 0;
            bool isAdd = false;

            isAdd = this.EditMode == FormDataMode.Add ? true : false;

            cObj.JournalDetList = this.listDetails;
            
            newJournalID = JournalBL.Save(cObj, isAdd);

             if (newJournalID > 0)
             {
                 this.JournalID = newJournalID;
                 bStatus = true;
                 saveMsg = isAdd ? "New Journal saved successfully." : "Edited Journal saved successfully.";
             }
            if (bStatus)
            {
                JournalID = newJournalID;
            }
            return bStatus;
        }

        public string GenarateJournalPrint()
        {
            clsPrmLedger prmLedger = new clsPrmLedger();
            //TODO Change 
            if(lblPosted.Text=="Yes")
            {
                prmLedger.IncludePostType = IncludePostEnum.Posted;
            }
            else if (lblPosted.Text == "No")
            {
                prmLedger.IncludePostType = IncludePostEnum.Unposted;
            }
            else
            {
                prmLedger.IncludePostType = IncludePostEnum.All;
            }
            prmLedger.CompanyID = this.CompanyID;
            prmLedger.JournalID = this.JournalID;

            prmLedger.IncludeInstrument = true;
            prmLedger.JournalReportFormat = (JournalReportFormatEnum)Convert.ToInt32(ddlReportFormat.SelectedValue);


            ReportOptions rptOption = new ReportOptions();

            rptOption.ReportViewMode = this.ucPrintButton.DefaultViewMode;
            rptOption.ReportOpenType = this.ucPrintButton.DefaultPrintAction;
            rptOption.ReportExportType = this.ucPrintButton.DefaultExportType;

            AppInfo.SetAppInfoToReportOptions(rptOption);
            CompanyInfo.SetCompanyInfoToReportOptions(rptOption, this.Context);

            //AppInfo.SetAppInfoToReportOptions(rptParams);
            //CompanyInfo.SetCompanyInfoToReportOptions(rptParams, this.Context);
            AppReport rpt = JournalRGN.GenerateJournal(prmLedger, rptOption);
            string rptKey = AppReport.SetAppReportToSession(rpt, this.Context);
            //string rptKey = "";

            return rptKey;
        }


        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.JournalID > 0)
            {
                //if (BLLibrary.PaymentBL.PaymentRequisitionBL.IsPaymentExists(this.JournalID))
                //{
                //    this.SetPageMessage("Payment Exists For this Requisition. Cannot Edit!", MessageTypeEnum.InvalidData);
                //    this.ShowPageMessage(lblMessage, true);
                //    return;
                //}
            }
            if (lblPosted.Text == "No")
            {
                base.CheckPermissionEdit();
                EditTask();

            }
            else
            {
                this.SetPageMessage("Voucher is posted. Cannot Edit!", MessageTypeEnum.InvalidData);
                this.ShowPageMessage(lblMessage, true);
                return;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            CancelTask();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.JournalID > 0)
            {
                //if (BLLibrary.PaymentBL.PaymentRequisitionBL.IsPaymentExists(this.JournalID))
                //{
                //    this.SetPageMessage("Payment Exists For this Requisition. Cannot Delete!", MessageTypeEnum.InvalidData);
                //    this.ShowPageMessage(lblMessage, true);
                //    return;
                //}
            }
            DeleteTask();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshTask();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                //txtGLAccCode.Enabled = !this.IsPosted;
                //txtGLAccName.Enabled = !this.IsPosted;

                //txtGLTranTypeCode.Enabled = !this.IsPosted;


                string rowID = e.Row.ClientID;
                string js = string.Format("return ShowDetailsPopup('{0}');", rowID);
                //string js = "javascript:ShowDetailsPopup('" +  rowID + "');";

                //LinkButton lnkEdit = (LinkButton)e.Row.FindControl("btnEditRow");
                //lnkEdit.OnClientClick = js;

                //HyperLink lnk = (HyperLink)e.Row.Cells[4].Controls[0];
                //lnk.NavigateUrl = js;


                LinkButton lnkDelete = (LinkButton)e.Row.FindControl("btnDeleteRow");
                string jsDelete = "return confirm('Are you sure to delete current row?');";
                lnkDelete.OnClientClick = jsDelete;


                dcJournalDet det = e.Row.DataItem as dcJournalDet;
                if (det._RecordState == RecordStateEnum.Deleted)
                {
                    e.Row.Visible = false;
                }
            }


            //switch (e.Row.RowType)
            //{
            //    case DataControlRowType.DataRow:
            //        e.Row.CssClass += " gridRow";
            //        break;
            //    case DataControlRowType.Header:
            //        e.Row.CssClass += " headerRow";
            //        break;
            //    case DataControlRowType.Footer:
            //        e.Row.CssClass += " footerRow";
            //        break;
            //    case DataControlRowType.Pager:
            //        e.Row.CssClass += " pagerRow";
            //        break;
            //    case DataControlRowType.EmptyDataRow:
            //        e.Row.CssClass += " gridRow";
            //        break;
            //}
        }

        protected void btnNewRow_Click(object sender, EventArgs e)
        {
            ReadDetailsFromGrid();
            //ReadDetRefListFromJSon();
            AddBlankRowToGridList();
            JournalDetBL.UpdateSLNo(this.listDetails,true);
            //FormatDetailsData();
            //ReadDetailsRefSingle();


            BindDataToGrid();
            //ListDetailsRefToJSon(this.listDetailsRef);
            SetControlGrid(this.EditMode);
        }

        protected void btnPopupTrigger_Click(object sender, EventArgs e)
        {
            //string param = Request["__EVENTARGUMENT"];
            string param = this.hdnPopupCommand.Value;

            switch (param.ToLower())
            {
                case "fillcombo":
                    //int prjID = ZCore.Utility.Conversion.StringToInt(this.ProjectSelectionQuick1.ProjectID);
                    //FillCombo_LandOwner(prjID);
                    break;

                case "getbalance":

                    //if (Convert.ToInt32(ddlLandOwner.SelectedValue) == 0)
                    //{
                    //    //this.txtTotRequisitionAmt.Text = "";
                    //    //txtApprovedAmtPopup.Text = "";
                    //}
                    //else
                    //{
                    //    //this.txtRequisitionAmt.Text = "5000";
                    //    //txtApprovedAmt.Text = "3000";
                    //}
                    //System.Threading.Thread.Sleep(3000); 
                    //txtRequisitionAmt.Text = txtRequisitionAmt.Text + " balance";
                    //ScriptManager.GetCurrent(this).SetFocus(ddlLandOwner);
                    break;
            }
        }

        protected void btnPopupGirdClose_Click(object sender, EventArgs e)
        {

        }

        protected void btnPopupGirdOK_Click(object sender, EventArgs e)
        {
            //int x = 98;
            //txtApprovedAmount.Text = "4000.00 " + txtRequisitionNo.Text;
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int RowIndex = gvr.RowIndex;
                GridView1.Rows[RowIndex].Visible = false;
                RefreshGrid();
                
            }
        }

        private void RefreshGrid()
        {
            int slNo = 0;
            foreach (GridViewRow gvR in this.GridView1.Rows)
            {
                if (gvR.RowType == DataControlRowType.DataRow)
                {
                    if (gvR.Visible)
                    {
                        slNo++;
                        ((Label)gvR.FindControl("lblSLNo")).Text = slNo.ToString();
                    }
                }
            }
        }

        private void RefreshGrid2()
        {
            int slNo = 0;
            foreach (GridViewRow gvR in this.GridView2.Rows)
            {
                if (gvR.RowType == DataControlRowType.DataRow)
                {
                    if (gvR.Visible)
                    {
                        slNo++;
                        ((Label)gvR.FindControl("lblSLNo")).Text = slNo.ToString();
                    }
                }
            }
        }


        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            
        }
        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {

            switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:
                    e.Row.CssClass += " gridRow";
                    break;
                case DataControlRowType.Header:
                    e.Row.CssClass += " headerRow";
                    break;
                case DataControlRowType.Footer:
                    e.Row.CssClass += " footerRow";
                    break;
                case DataControlRowType.Pager:
                    e.Row.CssClass += " pagerRow";
                    break;
                case DataControlRowType.EmptyDataRow:
                    e.Row.CssClass += " gridRow";
                    break;
            }
        }

        protected void OnPrintAction(object sender, EventArgs e)
        {
            if (this.EditMode == FormDataMode.Edit | this.EditMode == FormDataMode.Read)
            {
                string rptKey = GenarateJournalPrint();
                if (rptKey != string.Empty)
                {
                    this.ucPrintButton.ReportKey = rptKey;
                    this.ucPrintButton.ReportError = "";


                    this.ucPrintButton.PDFAutoPrint = true;
                    if (Request.Browser.Browser.ToLower().Contains("ie") == true)
                    {
                        this.ucPrintButton.PDFAutoPrint = !AccSettings.IsIERsClientPrint;
                    }
                }
            }
            else
            {
                this.ucPrintButton.ReportError = "Please Save First. To Print!";
                this.ucPrintButton.ReportKey = "";
            }

            this.ucPrintButton.PrintTask();
        }

        protected void btnPost_Click(object sender, EventArgs e)
        {
            if (this.JournalID == 0)
            {
                this.SetPageMessage("Please Save First!", MessageTypeEnum.Error);
                base.ShowPageMessage(lblMessage, true);
            }
            else
            {
                JournalBL.PostJournal(this.JournalID, DateTime.Today);

                ReadData(this.JournalID);
                ReadDetails(this.JournalID);
                this.SetPageMessage("Journal successfully posted!", MessageTypeEnum.Successful);
            }
            base.ShowPageMessage(lblMessage, true);
        }

        protected void ddlJournalType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetGLGroupClassListString();
            AddTask();
        }

        protected void btnNewRow2_Click(object sender, EventArgs e)
        {
            ReadDetailsFromGrid();
            //ReadDetRefListFromJSon();
            AddBlankRowToGridList2();
            JournalDetBL.UpdateSLNo(this.listDetails,true);
            //FormatDetailsData();
            //ReadDetailsRefSingle();


            BindDataToGrid2();
            //ListDetailsRefToJSon(this.listDetailsRef);
            SetControlGrid2(this.EditMode);
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int RowIndex = gvr.RowIndex;
                GridView2.Rows[RowIndex].Visible = false;
                RefreshGrid2();

            }
        }

        protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:
                    e.Row.CssClass += " gridRow";
                    break;
                case DataControlRowType.Header:
                    e.Row.CssClass += " headerRow";
                    break;
                case DataControlRowType.Footer:
                    e.Row.CssClass += " footerRow";
                    break;
                case DataControlRowType.Pager:
                    e.Row.CssClass += " pagerRow";
                    break;
                case DataControlRowType.EmptyDataRow:
                    e.Row.CssClass += " gridRow";
                    break;
            }
        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                //txtGLAccCode.Enabled = !this.IsPosted;
                //txtGLAccName.Enabled = !this.IsPosted;

                //txtGLTranTypeCode.Enabled = !this.IsPosted;


                string rowID = e.Row.ClientID;
                string js = string.Format("return ShowDetailsPopup('{0}');", rowID);
                //string js = "javascript:ShowDetailsPopup('" +  rowID + "');";

                //LinkButton lnkEdit = (LinkButton)e.Row.FindControl("btnEditRow");
                //lnkEdit.OnClientClick = js;

                //HyperLink lnk = (HyperLink)e.Row.Cells[4].Controls[0];
                //lnk.NavigateUrl = js;


                LinkButton lnkDelete = (LinkButton)e.Row.FindControl("btnDeleteRow");
                string jsDelete = "return confirm('Are you sure to delete current row?');";
                lnkDelete.OnClientClick = jsDelete;


                dcJournalDet det = e.Row.DataItem as dcJournalDet;
                if (det._RecordState == RecordStateEnum.Deleted)
                {
                    e.Row.Visible = false;
                }
            }
        }

        protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetGLGroupClassListString();
            AddTask();
        }
    }
}
