using System;
using System.Collections;
using System.Collections.Specialized;
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
using Microsoft.Reporting.WebForms;
using PG.Core;
using PG.Core.Web;

using PG.DBClass.AccountingDC;
using PG.DBClass.AccountingDC.AccEnums;
using PG.DBClass.AccountingDC.GeneralLedgerDC;

using PG.BLLibrary.AccountingBL;
using PG.BLLibrary.AccountingBL.GeneralLedgerBL;

using PG.Report;
using PG.Report.ReportEnums;
using PG.Report.ReportClass;
using PG.Report.ReportClass.AccountingRC;
using PG.Report.ReportRBL.AccountingRBL;
using PG.Report.ReportGen.AccountingRGN;
using PG.BLLibrary.OrganizationBL;
using PG.DBClass.OrganiztionDC;
using PG.DBClass.SecurityDC;

namespace PG.Web.Report.AccountingReport
{
    public partial class GLReports : BagePage
    {
        int CompanyID = 0;

        public string ReportViewPageLink = PageLinks.ReportLinks.GetLink_ReportView;
        public string ReportViewPDFPageLink = PageLinks.ReportLinks.GetLink_ReportViewPDF;
        public string ReportPrintPageLink = PageLinks.ReportLinks.GetLink_ReportPrint;
        public string ReportPDFPageLink = PageLinks.ReportLinks.GetLink_ReportPDF;


        public string GLAccountServiceLink = PageLinks.AccountingLinks.GetLink_GLAccount;
        public string GLGroupServiceLink = PageLinks.AccountingLinks.GetLink_GLGroup;
        public string GetJournalListServiceLink = PageLinks.AccountingLinks.GetLink_GetJournalList;

        public string AccRefServiceLink = PageLinks.AccountingLinks.GetLink_AccRef;




        ReportOpenTypeEnum ReportOpenType = ReportOpenTypeEnum.Preview;

        DateTime? fromDate = null;
        DateTime? toDate = null;

        dcAccSettings AccSettings = null;



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
            //base.AppObjectID = BLLibrary.SystemBL.AppObjectEnum.Frm1001_OptionInfo;
            //base.RestrictByPageInTab();

            //ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(this.LinkButton1);

            //this.EmpID = base.GetPageQueryInteger("eid");

            this.InitDBContext();
            this.CompanyID = CompanyInfo.GetCompanyID();
            this.hdnCompanyID.Value = this.CompanyID.ToString();
            
            this.AppMessageCurrent = base.GetAppMessage();

            this.AccSettings = AccSettingsBL.GetAccSettingByCompanyID(this.CompanyID);


            if (!IsPostBack) //first Time
            {
                FillReportCombo();
                FillCombo();
                FillComboHeirarcyLevel();
                SetReportParam();

                //if (this.UserID == 0) //not query string
                //{
                //    AddTask();
                //}
                //else
                //{
                //    EditTask();
                //    //if (Session["MsgSaveStatus"] != null)
                //    //{
                //    //    string sMsg = Session["MsgSaveStatus"].ToString();
                //    //    lblMessage.Text = sMsg.ToString();
                //    //    Session["MsgSaveStatus"] = null;
                //    //}
                //}

                //this.GLGroupTree1.SetGLGroupTreeText(this.CompanyID);

                //List<DBClass.Accounting.dcAccGLGroup> cList = BLLibrary.AccountingBL.AccGLGroupBL.GetAccGLGroupList(true, false, DBClass.AccOption.AccOrderByEnum.SLNo, "");
                //litGLGroup.Text = BLLibrary.AccountingBL.AccGLGroupBL.GetAccGLGroupULTreeText();

               // this.GroupTree1.SetGroupTreeText<DBClass.Accounting.dcAccGLGroup>(cList, "AccGLGroupID", "AccGLGroupName", "AccGLGroupIDParent", false);

                //GroupTree1.GroupTreeText = BLLibrary.AccountingBL.AccGLGroupBL.GetAccGLGroupULTreeText();

            }
            else
            {



            }


            SetControl();
            this.Form.DefaultButton = btnView.UniqueID;

            this.ShowPageMessage(this.lblMessage,true);

           // Response.Write("UserID : " + this.UserID.ToString());
        }


        private void FillReportCombo()
        {
            //ddlReport.Items.Clear();
            //ddlReport.Items.Add(new ListItem("(select report)","0"));
            //ddlReport.AppendDataBoundItems = true;

            //ddlReport.Items.Add(new ListItem("Chart Of Accounts", "515"));
            //ddlReport.Items.Add(new ListItem("Trial Balance","501"));

            //ddlReport.Items.Add(new ListItem("Cash Flow Statment", "505"));
            //ddlReport.Items.Add(new ListItem("Income Statement", "502"));
            //ddlReport.Items.Add(new ListItem("Balance Sheet", "503"));
            
            //ddlReport.Items.Add(new ListItem("Journal", "520"));
            //ddlReport.Items.Add(new ListItem("Ledger", "550"));

        }


        private void FillCombo()
        {

            ddlAccYear.Items.Clear();
            ddlAccYear.Items.Add(new ListItem("(select)", "0"));
            ddlAccYear.AppendDataBoundItems = true;

            ddlAccYear.DataTextField = "AccYearName";
            ddlAccYear.DataValueField = "AccYearID";
            ddlAccYear.AppendDataBoundItems = true;
            ddlAccYear.DataSource = AccYearBL.GetAccYearList(this.CompanyID, this.DbContext);
            ddlAccYear.DataBind();

            if (ddlAccYear.Items.Count > 1)
            {
                ddlAccYear.SelectedValue = AccYearBL.GetCurrentAccYear(this.CompanyID, this.DbContext).AccYearID.ToString();
                SetFromToDate();
            }

            //ddlTaxYear.Items.Clear();
            //ddlTaxYear.Items.Add(new ListItem("(select)", "0"));
            //ddlTaxYear.AppendDataBoundItems = true;

            //ddlTaxYear.DataTextField = "TaxYearName";
            //ddlTaxYear.DataValueField = "TaxYearID";
            //ddlTaxYear.AppendDataBoundItems = true;
            //ddlTaxYear.DataSource = BLLibrary.AccountingBL.TaxYearBL.GetTaxYearList(this.DbContext);
            //ddlTaxYear.DataBind();


            ddlJournalType.Items.Clear();
            ddlJournalType.Items.Add(new ListItem("(all type)", "0"));
            ddlJournalType.AppendDataBoundItems = true;

            ddlJournalType.DataTextField = "JournalTypeName";
            ddlJournalType.DataValueField = "JournalTypeID";
            ddlJournalType.AppendDataBoundItems = true;
            ddlJournalType.DataSource =   JournalTypeBL.GetJournalTypeList(this.CompanyID);
            ddlJournalType.DataBind();


            ddlOrderBy.Items.Clear();
            ddlOrderBy.Items.Add(new ListItem("Name", ((int)AccOrderByEnum.Name).ToString()));
            ddlOrderBy.Items.Add(new ListItem("Code", ((int)AccOrderByEnum.Code).ToString()));
            ddlOrderBy.Items.Add(new ListItem("SL No", ((int)AccOrderByEnum.SLNo).ToString()));


            ddlLocation.Items.Clear();
            ddlLocation.Items.Add(new ListItem("All", "0"));
            ddlLocation.AppendDataBoundItems = true;

            ddlLocation.DataTextField = "LocationCodeName";
            ddlLocation.DataValueField = "LocationID";
            ddlLocation.AppendDataBoundItems = true;
            ddlLocation.DataSource = AppSecurity.GetValidLocationUserList(); //LocationBL.GetLocationList(this.CompanyID);
            ddlLocation.DataBind();

            dcUser user = AppSecurity.GetUserInfoFromSession();
            int locID = user.LoginLocationID;
            ddlLocation.SelectedValue = locID.ToString();

            //int locID = AppSecurity.GetValidLocationUserList().Select(c => c.LocationID).FirstOrDefault();

            //if (ddlLocation.Items.Count <3)
           // {
             //   ddlLocation.SelectedValue = locID.ToString();
                
           // }
           


        }

        private void FillComboOpeningBalanceType()
        {
            ddlIncludeOpBal.Items.Clear();
            ddlIncludeOpBal.Items.Add(new ListItem("No", "0"));
            ddlIncludeOpBal.Items.Add(new ListItem("Yes", "1"));
            //ddlIncludeOpBal.Items.Add(new ListItem("Yes - Year", "2"));
            ddlIncludeOpBal.Items.Add(new ListItem("Yes - Date Range", "3"));

            ddlIncludeOpBal.SelectedValue = "1";

        }

        private void FillComboOpeningBalanceTypeLedger()
        {
            ddlIncludeOpBal.Items.Clear();
            ddlIncludeOpBal.Items.Add(new ListItem("No", "0"));
            ddlIncludeOpBal.Items.Add(new ListItem("Yes", "1"));
            //ddlIncludeOpBal.Items.Add(new ListItem("Yes - Year", "2"));
            ddlIncludeOpBal.Items.Add(new ListItem("Yes - Year, Date Range", "4"));
            ddlIncludeOpBal.Items.Add(new ListItem("Yes - Date Range", "3"));
           

            ddlIncludeOpBal.SelectedValue = "1";

        }

        private void FillComboJournalReportType()
        {
            ddlReportType.Items.Clear();
            ddlReportType.Items.Add(new ListItem("Journal List", "0"));
            ddlReportType.Items.Add(new ListItem("Journal Book", "1"));
            ddlReportType.Items.Add(new ListItem("Journal", "2"));
        }

        private void FillComboCashReportType()
        {
            ddlReportType.Items.Clear();
            ddlReportType.Items.Add(new ListItem("Cash/Bank Summary", "0"));
            ddlReportType.Items.Add(new ListItem("Cash Journal List", "1"));
            ddlReportType.Items.Add(new ListItem("Cash Journal Book", "2"));
        }

        private void FillComboLedgerReportType()
        {
            ddlReportType.Items.Clear();
            ddlReportType.Items.Add(new ListItem("Ledger", "0"));
            ddlReportType.Items.Add(new ListItem("Ledger Summary", "1"));
            ddlReportType.Items.Add(new ListItem("Ledger Journal List", "2"));
            ddlReportType.Items.Add(new ListItem("Ledger Journal Book", "3"));
        }

        private void FillComboJournalReportFormat()
        {
            ddlReportFormat.Items.Clear();
            //ddlPrintFormat.Items.Add(new ListItem("(all type)", "0"));
            //ddlPrintFormat.AppendDataBoundItems = true;

            ddlReportFormat.DataTextField = "JournalReportFormatName";
            ddlReportFormat.DataValueField = "JournalReportFormatID";
            ddlReportFormat.AppendDataBoundItems = true;
            ddlReportFormat.DataSource = JournalReportFormatBL.GetJournalReportFormatList();
            ddlReportFormat.DataBind();

            if (this.AccSettings.DefJournalReportFormat > 0)
            {
                ddlReportFormat.SelectedValue = this.AccSettings.DefJournalReportFormat.ToString();
            }
        }


        private void FillComboAccRefCategory(AccRefTypeEnum pAccRefType)
        {
            ddlAccRefCategory.Items.Clear();
            ddlAccRefCategory.Items.Add(new ListItem("All", "0"));


            ddlAccRefCategory.DataTextField = "AccRefCategoryName";
            ddlAccRefCategory.DataValueField = "AccRefCategoryID";
            ddlAccRefCategory.AppendDataBoundItems = true;
            ddlAccRefCategory.DataSource = AccRefCategoryBL.GetAccRefCategoryList(this.CompanyID,(int)pAccRefType);
            ddlAccRefCategory.DataBind();
          

        }


        public void SetFromToDate()
        {
            int yearID = Convert.ToInt32(ddlAccYear.SelectedValue);
            if (yearID > 0)
            {
                dcAccYear year = AccYearBL.GetAccYearByID(this.CompanyID, yearID);

                txtFromDate.Text = year.YearStartDate.Value.ToString("dd-MMM-yyyy");

                if (year.YearEndDate.Value >= DateTime.Today)
                {
                    txtToDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                }
                else
                {
                    txtToDate.Text = year.YearEndDate.Value.ToString("dd-MMM-yyyy");
                }
            }
        }


        public void FillComboHeirarcyLevel()
        {
            ddlHeirarchyLevel.Items.Clear();
            ddlHeirarchyLevel.Items.Add(new ListItem("(all)", "-1"));
            ddlHeirarchyLevel.AppendDataBoundItems = true;

            List<dcGLGroup> grpList = GLGroupBL.GetGLGroupList(this.CompanyID);
            grpList = GLGroupBL.FormatGLGroup(new clsPrmLedger(), grpList);

            int maxLevel = grpList.Max(c => c.GLGroupLevel) + 1;

            for (int i = 0; i <= maxLevel; i++)
            {
                string name = "Level " + (i + 1).ToString();
                //ddlHeirarchyLevel.Items.Add(new ListItem(name, (i + 1).ToString()));
                ddlHeirarchyLevel.Items.Add(new ListItem(name, (i).ToString()));
            }


            ddlHeirarchyLevel.SelectedValue = maxLevel >= 1 ? "1" : "-1"; 
        }

        public void FillBankBranch()
        {
            //ddlBankBranh.Items.Clear();
            //ddlBankBranh.Items.Add(new ListItem("(all branch)", "0"));
            //ddlBankBranh.AppendDataBoundItems = true;

            //ddlBankBranh.DataTextField = "BankBranchName";
            //ddlBankBranh.DataValueField = "BankBranchID";
            //ddlBankBranh.AppendDataBoundItems = true;
            //ddlBankBranh.DataSource = BLLibrary.MasterBL.BankBranchBL.GetBankBranchList(Convert.ToInt32(this.ddlBank.SelectedValue),this.DbContext);
            //ddlBankBranh.DataBind();
        }


        private void SetControl()
        {
            txtGLGroupName.Attributes.Add("readonly", "readonly");
            txtGLAccountName.Attributes.Add("readonly", "readonly");
            txtAccRefName.Attributes.Add("readonly", "readonly");

            //dcAccSettings expreport = AccSettingsBL.GetAccSettingByCompanyID(this.CompanyID);


            if (this.AccSettings.DisableReportExport)
            {
                ddlExport.Enabled = false;
                btnExport.Enabled = false;

            }
            else
            {
                ddlExport.Enabled = true;
                btnExport.Enabled = true;
            }

            if (this.AccSettings.DisableReportPrint)
            {
                btnPrint.Enabled = false;
                //btnView.Enabled = false;

            }
            else
            {
                btnPrint.Enabled = true;
                //btnView.Enabled = true;
            }
            //((TextBox)gvR.FindControl("txtGLAccountName")).Attributes.Add("readonly", "readonly");
        }


        private void SetReportParam()
        {
            //int rptID = Convert.ToInt32(ddlReport.SelectedValue);

            //this.Form.DefaultButton = this.btnDisableEnter.UniqueID;



            int rptID = Convert.ToInt32(tvwReport.SelectedNode.Value);


            lblReportName.Text = tvwReport.SelectedNode.Text;


            lblReportType.Visible = false;
            ddlReportType.Visible = false;


            lblBlock.Visible = false;
            ddlBlock.Visible = false;


            lblOrderBy.Visible = false;
            ddlOrderBy.Visible = false;


            lblAccYear.Visible = false;
            ddlAccYear.Visible = false;

            lblFromDate.Visible = false;
            txtFromDate.Visible = false;
            lblFromDate.Text = "Date:";


            lblToDate.Visible = false;
            txtToDate.Visible = false;
            lblToDate.Text = "To";

            FillComboOpeningBalanceType();
            lblIncludeOpBal.Visible = false;
            ddlIncludeOpBal.Visible = false;

            lblIncludeZero.Visible = false;
            ddlIncludeZero.Visible = false;

            lblHeirarchyLevel.Visible = false;
            ddlHeirarchyLevel.Visible = false;

            lblIncludeAllAccount.Visible = false;
            ddlIncludeAllAccount.Visible = false;

            lblGroupLedgerShowType.Visible = false;
            ddlGroupLedgerShowType.Visible = false;
            ddlGroupLedgerShowType.SelectedValue = "3";


            lblIncludeGLClass.Visible = false;
            ddlIncludeGLClass.Visible = false;

            lblAmountShowType.Visible = false;
            ddlAmountShowType.Visible = false;

            lblIncludePostType.Visible = false;
            ddlIncludePostType.Visible = false;
            ddlIncludePostType.Items[2].Enabled = true;

            lblJournalType.Visible = false;
            ddlJournalType.Visible = false;

            lblBalnceSheetShowMethod.Visible = false;
            ddlBalnceSheetShowMethod.Visible = false;

            lblShowPercentage.Visible = false;
            ddlShowPercentage.Visible = false;


            lblIncludeInstrument.Visible = false;
            ddlIncludeInstrument.Visible = false;

            lblIncludeTranCode.Visible = false;
            ddlIncludeTranCode.Visible = false;

            lblIncludeCostCenter.Visible = false;
            ddlIncludeCostCenter.Visible = false;

            lblIncludeReference.Visible = false;
            ddlIncludeReference.Visible = false;

            lblGLGroup.Visible = false;
            txtGLGroup.Visible = false;
            btnGLGroup.Visible = false;
            txtGLGroupName.Visible = false;


            lblJournalNo.Visible = false;
            txtJournalNo.Visible = false;
            btnJournalNo.Visible = false;

            lblGLAccount.Visible = false;
            txtGLAccount.Visible = false;
            btnGLAccount.Visible = false;
            txtGLAccountName.Visible = false;

            lblIncludeGroupParents.Visible = false;
            ddlIncludeGroupParents.Visible = false;

            lblIncludeGroupChilds.Visible = false;
            ddlIncludeGroupChilds.Visible = false;

            lblIncludeContraEntry.Visible = false;
            ddlIncludeContraEntry.Visible = false;

            lblIncludeDetOfDetails.Visible = false;
            ddlIncludeDetOfDetails.Visible = false;


            lblControlAccountSummary.Visible = false;
            ddlControlAccountSummary.Visible = false;


            lblIncludeSubForControl.Visible = false;
            ddlIncludeSubForControl.Visible = false;


            lblReportFormat.Visible = false;
            ddlReportFormat.Visible = false;

            lblAccRefCategory.Visible = false;
            ddlAccRefCategory.Visible = false;


            lblAccRefCode.Visible = false;
            txtAccRefCode.Visible = false;
            btnAccRefCode.Visible = false;
            txtAccRefName.Visible = false;
            ddlLocation.Visible = false;
            lblLocation.Visible = false;

            ReportIDEnum rptIDEnum = (ReportIDEnum)rptID;

            switch (rptIDEnum)
            {

                case ReportIDEnum.GL_ChartOfAccounts: //Chart of Accounts

                    lblOrderBy.Visible = true;
                    ddlOrderBy.Visible = true;

                    lblGroupLedgerShowType.Visible = true;
                    ddlGroupLedgerShowType.Visible = true;



                    ////lblTaxYear.Visible = true;
                    ////ddlTaxYear.Visible = true;
                    break;



                case ReportIDEnum.GL_TrialBalance: //Trial Balance

                    lblOrderBy.Visible = true;
                    ddlOrderBy.Visible = true;

                    lblAccYear.Visible = true;
                    ddlAccYear.Visible = true;


                    lblFromDate.Visible = true;
                    txtFromDate.Visible = true;

                    lblToDate.Visible = true;
                    txtToDate.Visible = true;

                    lblIncludeOpBal.Visible = true;
                    ddlIncludeOpBal.Visible = true;


                    lblIncludeZero.Visible = true;
                    ddlIncludeZero.Visible = true;

                    lblHeirarchyLevel.Visible = true;
                    ddlHeirarchyLevel.Visible = true;

                    lblGroupLedgerShowType.Visible = true;
                    ddlGroupLedgerShowType.Visible = true;

                    //lblIncludeGLClass.Visible = true;
                    //ddlIncludeGLClass.Visible = true;

                    lblAmountShowType.Visible = true;
                    ddlAmountShowType.Visible = true;

                    lblIncludePostType.Visible = true;
                    ddlIncludePostType.Visible = true;

                    ////lblTaxYear.Visible = true;
                    ////ddlTaxYear.Visible = true;
                    break;

                case ReportIDEnum.GL_IncomeStatement: //Income Statement
                    lblAccYear.Visible = true;
                    ddlAccYear.Visible = true;


                    lblFromDate.Visible = true;
                    txtFromDate.Visible = true;

                    lblToDate.Visible = true;
                    txtToDate.Visible = true;

                    lblGroupLedgerShowType.Visible = true;
                    ddlGroupLedgerShowType.Visible = true;


                    lblIncludeOpBal.Visible = true;
                    ddlIncludeOpBal.Visible = true;

                    lblIncludeZero.Visible = true;
                    ddlIncludeZero.Visible = true;

                    lblHeirarchyLevel.Visible = true;
                    ddlHeirarchyLevel.Visible = true;

                    lblIncludePostType.Visible = true;
                    ddlIncludePostType.Visible = true;

                    ////lblTaxYear.Visible = true;
                    ////ddlTaxYear.Visible = true;
                    break;

                case ReportIDEnum.GL_BalanceSheet: //Balance Sheet

                    lblAccYear.Visible = true;
                    ddlAccYear.Visible = true;


                    lblFromDate.Visible = true;
                    txtFromDate.Visible = true;
                    txtFromDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");


                    lblToDate.Visible = true;
                    txtToDate.Visible = true;
                    txtToDate.Text = txtFromDate.Text;

                    lblGroupLedgerShowType.Visible = true;
                    ddlGroupLedgerShowType.Visible = true;


                    lblIncludeOpBal.Visible = true;
                    ddlIncludeOpBal.Visible = true;

                    lblIncludeZero.Visible = true;
                    ddlIncludeZero.Visible = true;

                    lblHeirarchyLevel.Visible = true;
                    ddlHeirarchyLevel.Visible = true;

                    //lblIncludeAllAccount.Visible = true;
                    //ddlIncludeAllAccount.Visible = true;

                    lblIncludePostType.Visible = true;
                    ddlIncludePostType.Visible = true;
                    ddlIncludePostType.Items[2].Enabled = false; //unposted only

                    ////lblTaxYear.Visible = true;
                    ////ddlTaxYear.Visible = true;
                    break;

                case ReportIDEnum.GL_ReceiptPayment: //ReceiptPayment

                    lblAccYear.Visible = true;
                    ddlAccYear.Visible = true;

                    lblFromDate.Visible = true;
                    txtFromDate.Visible = true;

                    lblToDate.Visible = true;
                    txtToDate.Visible = true;


                    lblGroupLedgerShowType.Visible = true;
                    ddlGroupLedgerShowType.Visible = true;


                    lblIncludeOpBal.Visible = true;
                    ddlIncludeOpBal.Visible = true;

                    lblIncludeZero.Visible = true;
                    ddlIncludeZero.Visible = true;

                    lblHeirarchyLevel.Visible = true;
                    ddlHeirarchyLevel.Visible = true;

                    lblIncludeGroupParents.Visible = true;
                    ddlIncludeGroupParents.Visible = true;

                    lblIncludePostType.Visible = true;
                    ddlIncludePostType.Visible = true;
                    ddlIncludePostType.Items[2].Enabled = false; //unposted only



                    ////lblTaxYear.Visible = true;
                    ////ddlTaxYear.Visible = true;
                    break;

                case ReportIDEnum.GL_Journal: //Journal
                    SetReportParamJournal();
                    break;

                case ReportIDEnum.GL_JournalBook: //Journal
                    SetReportParamJournalBook();
                    break;
                case ReportIDEnum.GL_JournalList: //Journal
                    SetReportParamJournalList();
                    break;


                case ReportIDEnum.GL_CashSummary: //Cash
                    SetReportParamCashSummary();
                    
                    //lblAccYear.Visible = true;
                    //ddlAccYear.Visible = true;

                    ////lblJournalType.Visible = true;
                    ////ddlJournalType.Visible = true;

                    //lblIncludePostType.Visible = true;
                    //ddlIncludePostType.Visible = true;

                    //lblReportType.Visible = true;
                    //ddlReportType.Visible = true;
                    //FillComboCashReportType();
                    //ddlReportType.SelectedValue = "0";
                    //SetReportParamCashSummary();
                    //ddlGroupLedgerShowType.SelectedValue = "2"; 

                    //////lblTaxYear.Visible = true;
                    //////ddlTaxYear.Visible = true;
                    break;


                case ReportIDEnum.GL_CashJournalList: //Cash
                    SetReportParamCashJournalList();
                    break;

                case ReportIDEnum.GL_CashJournalBook: //Cash
                    SetReportParamCashJournalBook();
                    break;

                case ReportIDEnum.GL_Ledger: //Ledger
                    SetReportParamLedger();
                    
                    
                    //lblAccYear.Visible = true;
                    //ddlAccYear.Visible = true;
                    

                    //lblFromDate.Visible = true;
                    //txtFromDate.Visible = true;


                    //lblToDate.Visible = true;
                    //txtToDate.Visible = true;

                    //lblIncludePostType.Visible = true;
                    //ddlIncludePostType.Visible = true;

                    



                    //lblReportType.Visible = true;
                    //ddlReportType.Visible = true;
                    //FillComboLedgerReportType();
                    //ddlReportType.SelectedValue = "0";
                    //SetReportParamLedger();
                    //ddlGroupLedgerShowType.SelectedValue = "2"; 



                    ////lblTaxYear.Visible = true;
                    ////ddlTaxYear.Visible = true;
                    break;

                case ReportIDEnum.GL_LedgerSummary: //Ledger
                    SetReportParamLedgerSummary();
                    break;

                case ReportIDEnum.GL_CostCenterSummary: 
                    SetReportParamAccRefSummary(AccRefTypeEnum.CostCenter);
                    break;
                case ReportIDEnum.GL_ReferenceSummary: //Cash
                    SetReportParamAccRefSummary(AccRefTypeEnum.Reference);
                    break;
                case ReportIDEnum.GL_TranCodeSummary: 
                    SetReportParamAccRefSummary(AccRefTypeEnum.TranCode);
                    break;

                case ReportIDEnum.GL_CostCenterDetails: 
                    SetReportParamAccRefDetails(AccRefTypeEnum.CostCenter);
                    break;
                case ReportIDEnum.GL_ReferenceDetails: 
                    SetReportParamAccRefDetails(AccRefTypeEnum.Reference);
                    break;
                case ReportIDEnum.GL_TranCodeDetails: 
                    SetReportParamAccRefDetails(AccRefTypeEnum.TranCode);
                    break;
            }
        }


        private void SetReportParamJournal()
        {
            lblAccYear.Visible = true;
            ddlAccYear.Visible = true;

            lblJournalType.Visible = true;
            ddlJournalType.Visible = true;

            lblIncludePostType.Visible = true;
            ddlIncludePostType.Visible = true;


            lblJournalNo.Visible = true;
            txtJournalNo.Visible = true;
            btnJournalNo.Visible = true;

            lblIncludeInstrument.Visible = true;
            ddlIncludeInstrument.Visible = true;


            //lblIncludeTranCode.Visible = true;
            //ddlIncludeTranCode.Visible = true;

            lblIncludeCostCenter.Visible = true;
            ddlIncludeCostCenter.Visible = true;

            lblIncludeReference.Visible = true;
            ddlIncludeReference.Visible = true;

            lblControlAccountSummary.Visible = true;
            ddlControlAccountSummary.Visible = true;

            lblIncludeSubForControl.Visible = true;
            ddlIncludeSubForControl.Visible = true;

            FillComboJournalReportFormat();
            lblReportFormat.Visible = true;
            ddlReportFormat.Visible = true;
            ddlLocation.Visible = true;
            lblLocation.Visible = true;

        }

        private void SetReportParamJournalList()
        {
            lblAccYear.Visible = true;
            ddlAccYear.Visible = true;

            lblJournalType.Visible = true;
            ddlJournalType.Visible = true;

            lblIncludePostType.Visible = true;
            ddlIncludePostType.Visible = true;


            lblFromDate.Visible = true;
            txtFromDate.Visible = true;

            lblToDate.Visible = true;
            txtToDate.Visible = true;
            ddlLocation.Visible = true;
            lblLocation.Visible = true;
        }

        private void SetReportParamJournalBook()
        {
            lblAccYear.Visible = true;
            ddlAccYear.Visible = true;

            lblJournalType.Visible = true;
            ddlJournalType.Visible = true;

            lblIncludePostType.Visible = true;
            ddlIncludePostType.Visible = true;


            lblFromDate.Visible = true;
            txtFromDate.Visible = true;

            lblToDate.Visible = true;
            txtToDate.Visible = true;

            lblIncludeInstrument.Visible = true;
            ddlIncludeInstrument.Visible = true;

            //lblIncludeTranCode.Visible = true;
            //ddlIncludeTranCode.Visible = true;

            lblIncludeCostCenter.Visible = true;
            ddlIncludeCostCenter.Visible = true;

            lblIncludeReference.Visible = true;
            ddlIncludeReference.Visible = true;

            lblControlAccountSummary.Visible = true;
            ddlControlAccountSummary.Visible = true;

            lblIncludeSubForControl.Visible = true;
            ddlIncludeSubForControl.Visible = true;
            ddlLocation.Visible = true;
            lblLocation.Visible = true;


        }

        private void SetReportParamCashSummary()
        {
            lblAccYear.Visible = true;
            ddlAccYear.Visible = true;

            lblIncludePostType.Visible = true;
            ddlIncludePostType.Visible = true;

            lblFromDate.Visible = true;
            txtFromDate.Visible = true;

            lblToDate.Visible = true;
            txtToDate.Visible = true;

            FillComboOpeningBalanceTypeLedger();
            lblIncludeOpBal.Visible = true;
            ddlIncludeOpBal.Visible = true;

            lblIncludeZero.Visible = true;
            ddlIncludeZero.Visible = true;

        }


        private void SetReportParamCashJournalList()
        {

            lblAccYear.Visible = true;
            ddlAccYear.Visible = true;

            lblIncludePostType.Visible = true;
            ddlIncludePostType.Visible = true;

            lblFromDate.Visible = true;
            txtFromDate.Visible = true;

            lblToDate.Visible = true;
            txtToDate.Visible = true;

            //FillComboOpeningBalanceTypeLedger();
            //lblIncludeOpBal.Visible = true;
            //ddlIncludeOpBal.Visible = true;

            //lblIncludeZero.Visible = true;
            //ddlIncludeZero.Visible = true;
        }


        private void SetReportParamCashJournalBook()
        {
            //lblAccYear.Visible = true;
            //ddlAccYear.Visible = true;

            //lblIncludePostType.Visible = true;
            //ddlIncludePostType.Visible = true;

            //lblFromDate.Visible = true;
            //txtFromDate.Visible = true;

            //lblToDate.Visible = true;
            //txtToDate.Visible = true;
            lblAccYear.Visible = true;
            ddlAccYear.Visible = true;

            lblJournalType.Visible = true;
            ddlJournalType.Visible = true;

            lblIncludePostType.Visible = true;
            ddlIncludePostType.Visible = true;


            lblFromDate.Visible = true;
            txtFromDate.Visible = true;

            lblToDate.Visible = true;
            txtToDate.Visible = true;

            lblIncludeInstrument.Visible = true;
            ddlIncludeInstrument.Visible = true;

            //lblIncludeTranCode.Visible = true;
            //ddlIncludeTranCode.Visible = true;

            lblIncludeCostCenter.Visible = true;
            ddlIncludeCostCenter.Visible = true;

            lblIncludeReference.Visible = true;
            ddlIncludeReference.Visible = true;

            lblControlAccountSummary.Visible = true;
            ddlControlAccountSummary.Visible = true;

            lblIncludeSubForControl.Visible = true;
            ddlIncludeSubForControl.Visible = true;

        }

        private void SetReportParamLedger()
        {
            lblAccYear.Visible = true;
            ddlAccYear.Visible = true;


            lblFromDate.Visible = true;
            txtFromDate.Visible = true;


            lblToDate.Visible = true;
            txtToDate.Visible = true;

            lblIncludePostType.Visible = true;
            ddlIncludePostType.Visible = true;

            lblFromDate.Visible = true;
            txtFromDate.Visible = true;

            lblToDate.Visible = true;
            txtToDate.Visible = true;


            FillComboOpeningBalanceTypeLedger();
            lblIncludeOpBal.Visible = true;
            ddlIncludeOpBal.Visible = true;


            lblGLGroup.Visible = true;
            txtGLGroup.Visible = true;
            btnGLGroup.Visible = true;
            txtGLGroupName.Visible = true;

            txtGLGroup.Text = string.Empty;
            txtGLGroupName.Text = string.Empty;
            hdnGLGroupID.Value = "0";

            lblGLAccount.Visible = true;
            txtGLAccount.Visible = true;
            btnGLAccount.Visible = true;
            txtGLAccountName.Visible = true;

            txtGLAccount.Text = string.Empty;
            txtGLAccountName.Text = string.Empty;
            hdnGLAccountID.Value = "0";


            lblIncludeInstrument.Visible = true;
            ddlIncludeInstrument.Visible = true;

            //lblIncludeTranCode.Visible = true;
            //ddlIncludeTranCode.Visible = true;

            lblIncludeCostCenter.Visible = true;
            ddlIncludeCostCenter.Visible = true;

            lblIncludeReference.Visible = true;
            ddlIncludeReference.Visible = true;

            lblIncludeDetOfDetails.Visible = true;
            ddlIncludeDetOfDetails.Visible = true;

            lblIncludeContraEntry.Visible = true;
            ddlIncludeContraEntry.Visible = true;

            //lblControlAccountSummary.Visible = true;
            //ddlControlAccountSummary.Visible = true;

            lblIncludeSubForControl.Visible = true;
            ddlIncludeSubForControl.Visible = true;
            lblLocation.Visible = true;
            ddlLocation.Visible = true;


        }

        private void SetReportParamLedgerSummary()
        {
            lblAccYear.Visible = true;
            ddlAccYear.Visible = true;


            lblFromDate.Visible = true;
            txtFromDate.Visible = true;

            lblToDate.Visible = true;
            txtToDate.Visible = true;

            lblIncludePostType.Visible = true;
            ddlIncludePostType.Visible = true;


            FillComboOpeningBalanceType();
            lblIncludeOpBal.Visible = true;
            ddlIncludeOpBal.Visible = true;

            lblIncludeZero.Visible = true;
            ddlIncludeZero.Visible = true;

            lblHeirarchyLevel.Visible = true;
            ddlHeirarchyLevel.Visible = true;

            lblIncludeGroupChilds.Visible = false;
            ddlIncludeGroupChilds.Visible = false;

            lblGroupLedgerShowType.Visible = true;
            ddlGroupLedgerShowType.Visible = true;
            ddlGroupLedgerShowType.SelectedValue = "3";


            lblGLGroup.Visible = true;
            txtGLGroup.Visible = true;
            btnGLGroup.Visible = true;
            txtGLGroupName.Visible = true;


            lblGLAccount.Visible = true;
            txtGLAccount.Visible = true;
            btnGLAccount.Visible = true;
            txtGLAccountName.Visible = true;
            lblLocation.Visible = true;
            ddlLocation.Visible = true;


        }


        private void SetReportParamAccRefSummary(AccRefTypeEnum pAccRefType)
        {
            lblAccYear.Visible = true;
            ddlAccYear.Visible = true;

            lblIncludePostType.Visible = true;
            ddlIncludePostType.Visible = true;

            lblFromDate.Visible = true;
            txtFromDate.Visible = true;

            lblToDate.Visible = true;
            txtToDate.Visible = true;

     
            lblIncludeOpBal.Visible = true;
            ddlIncludeOpBal.Visible = true;

            lblIncludeZero.Visible = true;
            ddlIncludeZero.Visible = true;

            FillComboAccRefCategory(pAccRefType);

            lblAccRefCategory.Visible = true;
            ddlAccRefCategory.Visible  = true;

            lblAccRefCode.Visible = true;
            txtAccRefCode.Visible = true;
            btnAccRefCode.Visible = true;
            txtAccRefName.Visible = true;

            hdnAccRefTypeID.Value = ((int)pAccRefType).ToString();
            txtAccRefCode.Text = "";
            txtAccRefName.Text = "";
            hdnAccRefID.Value = "0";

            lblGLGroup.Visible = true;
            txtGLGroup.Visible = true;
            btnGLGroup.Visible = true;
            txtGLGroupName.Visible = true;

            txtGLGroup.Text = string.Empty;
            txtGLGroupName.Text = string.Empty;
            hdnGLGroupID.Value = "0";

            lblGLAccount.Visible = true;
            txtGLAccount.Visible = true;
            btnGLAccount.Visible = true;
            txtGLAccountName.Visible = true;

            txtGLAccount.Text = string.Empty;
            txtGLAccountName.Text = string.Empty;
            hdnGLAccountID.Value = "0";



            switch (pAccRefType)
            {
                case AccRefTypeEnum.CostCenter:
                    lblAccRefCategory.Text = "Cost Center Category";
                    lblAccRefCode.Text = "Cost Center";
                    break;
                case AccRefTypeEnum.Reference:
                    lblAccRefCategory.Text = "Reference Category";
                    lblAccRefCode.Text = "Reference";
                    break;
                case AccRefTypeEnum.TranCode:
                    lblAccRefCategory.Text = "Tran Code Category";
                    lblAccRefCode.Text = "Tran Code";
                    break;
            }
        }


        private void SetReportParamAccRefDetails(AccRefTypeEnum pAccRefType)
        {
            lblAccYear.Visible = true;
            ddlAccYear.Visible = true;

            lblIncludePostType.Visible = true;
            ddlIncludePostType.Visible = true;

            lblFromDate.Visible = true;
            txtFromDate.Visible = true;

            lblToDate.Visible = true;
            txtToDate.Visible = true;


            lblIncludeOpBal.Visible = true;
            ddlIncludeOpBal.Visible = true;

            lblIncludeZero.Visible = true;
            ddlIncludeZero.Visible = true;

            FillComboAccRefCategory(pAccRefType);

            lblAccRefCategory.Visible = true;
            ddlAccRefCategory.Visible = true;

            lblAccRefCode.Visible = true;
            txtAccRefCode.Visible = true;
            btnAccRefCode.Visible = true;
            txtAccRefName.Visible = true;

            hdnAccRefTypeID.Value = ((int)pAccRefType).ToString();
            txtAccRefCode.Text = "";
            txtAccRefName.Text = "";
            hdnAccRefID.Value = "0";

            lblGLGroup.Visible = true;
            txtGLGroup.Visible = true;
            btnGLGroup.Visible = true;
            txtGLGroupName.Visible = true;

            txtGLGroup.Text = string.Empty;
            txtGLGroupName.Text = string.Empty;
            hdnGLGroupID.Value = "0";

            lblGLAccount.Visible = true;
            txtGLAccount.Visible = true;
            btnGLAccount.Visible = true;
            txtGLAccountName.Visible = true;

            txtGLAccount.Text = string.Empty;
            txtGLAccountName.Text = string.Empty;
            hdnGLAccountID.Value = "0";

            switch (pAccRefType)
            {
                case AccRefTypeEnum.CostCenter:
                    lblAccRefCategory.Text = "Cost Center Category";
                    lblAccRefCode.Text = "Cost Center";
                    break;
                case AccRefTypeEnum.Reference:
                    lblAccRefCategory.Text = "Reference Category";
                    lblAccRefCode.Text = "Reference";
                    break;
                case AccRefTypeEnum.TranCode:
                    lblAccRefCategory.Text = "Tran Code Category";
                    lblAccRefCode.Text = "Tran Code";
                    break;
            }
        }


        protected void btnView_Click(object sender, EventArgs e)
        {
            this.ReportOpenType = ReportOpenTypeEnum.Preview;
            OpenReport();
        }


        private bool CheckData()
        {
            return true;
        }


        private bool ReadDateRangeParam()
        {
            return ReadDateRangeParam(true);
        }
        private bool ReadDateRangeParam(bool isToDate)
        {
            bool bStatus = true;
            DateTime dt;


            fromDate = null;
            toDate = null;

            if (DateTime.TryParse(txtFromDate.Text, out dt))
            {
                fromDate = dt;
            }
            else
            {
                this.SetPageMessage("Please Select From date!", MessageTypeEnum.InvalidData, true);
                base.ShowPageMessage(lblMessage, true);
                txtFromDate.Focus();
                bStatus = false;
            }

            if (isToDate)
            {
                if (DateTime.TryParse(txtToDate.Text, out dt))
                {
                    toDate = dt;
                }
                else
                {
                    this.SetPageMessage("Please Select To date!", MessageTypeEnum.InvalidData, true);
                    base.ShowPageMessage(lblMessage, true);
                    txtToDate.Focus();
                    bStatus = false;
                }
            }
            else
            {
                toDate = fromDate;
            }


            if (fromDate > toDate)
            {
                this.SetPageMessage("From Date Cannote greater then to date!", MessageTypeEnum.InvalidData, true);
                base.ShowPageMessage(lblMessage, true);
                txtFromDate.Focus();
                bStatus = false;
            }


            if (toDate < fromDate)
            {
                this.SetPageMessage("To Date Cannote less then from date!", MessageTypeEnum.InvalidData, true);
                base.ShowPageMessage(lblMessage, true);
                txtToDate.Focus();
                bStatus = false;
            }

            return bStatus;
        }


        private bool ReadDateParam(bool isToDate)
        {
            bool bStatus = true;
            DateTime dt;


            fromDate = null;
            toDate = null;

            if (DateTime.TryParse(txtFromDate.Text, out dt))
            {
                fromDate = dt;
            }
            else
            {
                this.SetPageMessage("Please Select date!", MessageTypeEnum.InvalidData, true);
                base.ShowPageMessage(lblMessage, true);
                txtFromDate.Focus();
                bStatus = false;
            }

            return bStatus;
        }



        private void OpenReport()
        {
            //int rptID = Convert.ToInt32(ddlReport.SelectedValue);
            int rptID = Convert.ToInt32(tvwReport.SelectedNode.Value);


            if (rptID == 0)
            {
                this.SetPageMessage("Please Select Report!", MessageTypeEnum.InvalidData, true);
                base.ShowPageMessage(lblMessage, true);
                //ddlReport.Focus();
                return;
            }


            ReportIDEnum rptIDEnum = (ReportIDEnum)rptID;


            switch (rptIDEnum)
            {
                case ReportIDEnum.GL_ChartOfAccounts: //chart of account
                    ReportCharOfAccounts();
                    break;
                case ReportIDEnum.GL_TrialBalance:    //trial balance
                    ReportTrialBalance();
                    break;
                case ReportIDEnum.GL_IncomeStatement: //income statement
                    ReportIncomeStatement();  
                    break;
                case ReportIDEnum.GL_BalanceSheet: //balance sheet
                    ReportBalanceSheet();
                    break;
                case ReportIDEnum.GL_ReceiptPayment: //balance sheet
                    ReportReceiptPayment();
                    break;
                case ReportIDEnum.GL_Ledger:  //ledger
                    ReportLedger();
                    break;
                case ReportIDEnum.GL_LedgerSummary:  //ledger
                    ReportLedgerSummary();
                    break;
                //case ReportIDEnum.GL_LedgerBook:  //ledger
                //    ReportLedgeBook();
                //    break;
                case ReportIDEnum.GL_Journal:  //Journal
                    ReportJournal();
                    break;  
                  
                case ReportIDEnum.GL_JournalList:  //Journal
                    ReportJournalList();
                    break;

                case ReportIDEnum.GL_JournalBook:  //Journal
                    ReportJournalBook();
                    break;

                case ReportIDEnum.GL_CashSummary:  //Journal
                    ReportCashSummary();
                    break;

                case ReportIDEnum.GL_CashJournalList:  //Journal
                    ReportCashJournalList();
                    break;

                case ReportIDEnum.GL_CashJournalBook:  //Journal
                    ReportCashJournalBook();
                    break;

                case ReportIDEnum.GL_CostCenterSummary:
                    ReportAccRefSummary(AccRefTypeEnum.CostCenter);
                    break;
                case ReportIDEnum.GL_ReferenceSummary:
                    ReportAccRefSummary(AccRefTypeEnum.Reference);
                    break;
                case ReportIDEnum.GL_TranCodeSummary:
                    ReportAccRefSummary(AccRefTypeEnum.TranCode);
                    break;
                case ReportIDEnum.GL_CostCenterDetails:
                    ReportAccRefDetails(AccRefTypeEnum.CostCenter);
                    break;
                case ReportIDEnum.GL_ReferenceDetails:
                    ReportAccRefDetails(AccRefTypeEnum.Reference);
                    break;
                case ReportIDEnum.GL_TranCodeDetails:
                    ReportAccRefDetails(AccRefTypeEnum.TranCode);
                    break;


                default:
                    break;

            }
        }

        private void ReportCashJournalList()
        {
            if (Convert.ToInt32(ddlAccYear.SelectedValue) == 0)
            {
                this.SetPageMessage("Please Select Financial Year!", MessageTypeEnum.InvalidData, true);
                base.ShowPageMessage(lblMessage, true);
                ddlAccYear.Focus();
                return;
            }

            if (ReadDateRangeParam(true) == false)
            {
                return;
            }

            int accYearID = Convert.ToInt32(ddlAccYear.SelectedValue);
            int companyID = this.CompanyID;

            int journalTypeID = Convert.ToInt32(ddlJournalType.SelectedValue);

            InculdeOpBalanceEnum includeOpBalanceType = (InculdeOpBalanceEnum)Convert.ToInt32(ddlIncludeOpBal.SelectedValue);
            IncludePostEnum includePostType = (IncludePostEnum)Convert.ToInt32(ddlIncludePostType.SelectedValue);

            clsPrmLedger prmLdg = new clsPrmLedger();
            prmLdg.CompanyID = companyID;
            prmLdg.JournalID = 0;
            prmLdg.AccYearID = accYearID;
            prmLdg.JournalTypeID = journalTypeID;

            prmLdg.FromDate = fromDate;
            prmLdg.ToDate = toDate;
            prmLdg.IncludeOpBalanceType = includeOpBalanceType;
            prmLdg.IncludePostType = includePostType;

            prmLdg.LocationIDList = GetParamLocationIDList();
            prmLdg.IsLocation = false;

            ReportOptions rptOption = GetReportOptions();

            AppReport rpt = JournalRGN.GenerateCashJournalList(prmLdg, rptOption);

            string rk = AppReport.SetAppReportToSession(rpt, this.Context);
            ShowReport(rk);
            //Response.Redirect(@"~/Report/ReportView.aspx?rk=" + rk);
            //ClientScript.RegisterStartupScript(this.Page.GetType(), "showreport", string.Format("tbopen('{0}');", rk), true);
        }

        private void ShowReport(string reportKey)
        {
            ReportOpenTypeEnum rptOpenType = this.ReportOpenType;
            ReportViewModeEnum rptViewMode = (ReportViewModeEnum)Convert.ToInt32(ddlReportViewMode.SelectedValue);
            
            string strWait = "true";
            string strIsPrint = "false";
            string strIsPDFAutoPrint = "false";
            switch (rptOpenType)
            {
                case ReportOpenTypeEnum.Preview:
                    break;
                case ReportOpenTypeEnum.Print:
                    rptViewMode = ReportViewModeEnum.InThisTab;
                    strWait = "false";
                    strIsPrint = "true";
                    break;
                case ReportOpenTypeEnum.Export:
                    rptViewMode = ReportViewModeEnum.InThisTab;
                    strWait = "false";
                    break;
            }

            bool isPDFAutoPrint = true;
            if (Request.Browser.Browser.ToLower().Contains("ie") == true)
            {
                isPDFAutoPrint = !AccSettings.IsIERsClientPrint;
            }
            
            strIsPDFAutoPrint = isPDFAutoPrint ? "true" : "false";


            //string strTime = DateTime.Now.ToString("hhmm");
            string strTime = DateTime.Now.ToFileTime().ToString();

            string url = this.ReportViewPageLink + "?rk=" + reportKey;
            //string url = this.ReportViewPageLink + "?rk=" + reportKey + "&_tt=" + strTime;
            //string jsScript = "$(function(){tbopen('" + reportKey + "'," + strWait + ");});";

            //string jsScript = "$(function(){tbopen('" + reportKey + "', " +  strIsPrint  +  "," + strWait + ");});";

            string jsScript = "$(function(){tbopen('" + reportKey + "', " + strIsPrint +  "," + strIsPDFAutoPrint  + "," + strWait + ");});";

            //string jsScript = string.Format("$(function(){tbopen('{0}',{1},{2});});",reportKey,strIsPrint,strWait);


            switch (rptViewMode)
            {
                case ReportViewModeEnum.InThisTab:
                    if (rptOpenType == ReportOpenTypeEnum.Print)
                    {
                        ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "showreport", jsScript, true);
                    }
                    else
                    {
                        Response.Redirect(url,false);
                    }


                    break;
                case ReportViewModeEnum.InNewTab:
                    ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "showreport", jsScript, true);
                    break;
                case ReportViewModeEnum.InNewWindow:
                    ClientScript.RegisterStartupScript(this.GetType(), "newWindow", String.Format("<script>window.open('{0}');</script>", url));
                    break;
                case ReportViewModeEnum.InDialog:
                    break;
                default:
                    ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "showreport", jsScript, true);
                    break;
            }
        }


        private ReportOptions GetReportOptions()
        {
            ReportOptions rptOption = new ReportOptions();

            rptOption.ReportViewMode = (ReportViewModeEnum)Convert.ToInt32(ddlReportViewMode.SelectedValue);
            rptOption.ReportOpenType = this.ReportOpenType;
            rptOption.ReportExportType = (ReportExportTypeEnum)Convert.ToInt32(ddlExport.SelectedValue);
            rptOption.DisbalePDFPrint = AccSettings.DisablePDFPrint;

            AppInfo.SetAppInfoToReportOptions(rptOption);
            CompanyInfo.SetCompanyInfoToReportOptions(rptOption, this.Context);


            return rptOption;
        }

        private void ReportCharOfAccounts()
        {
            ////if (Convert.ToInt32(ddlAccYear.SelectedValue) == 0)
            ////{
            ////    this.SetPageMessage("Please Select Accounting Year!", MessageTypeEnum.InvalidData, true);
            ////    base.ShowPageMessage(lblMessage, true);
            ////    ddlAccYear.Focus();
            ////    return;
            ////}

            //int accYearID = Convert.ToInt32(ddlAccYear.SelectedValue);
            //int accSystemID = (int)DBClass.AccOption.AccSystemEnum.ProvidentFund;

            //DateTime dt;

            //DateTime? fromDate = null;
            //if (DateTime.TryParse(txtFromDate.Text, out dt))
            //{
            //    fromDate = dt;
            //}


            //DateTime? toDate = null;
            //if (DateTime.TryParse(txtToDate.Text, out dt))
            //{
            //    toDate = dt;
            //}


            //int dBlock = Convert.ToInt32(ddlBlock.SelectedValue);

            //if (dBlock == 0 | dBlock == 2)
            //{
            //    bool pRet = AppSecurity.CheckObjectPermissionByUserID(this.LoginUserID, AppObjectEnum.FrmOpt_BlockedEmployee, PermissionEnum.Read);
            //    if (!pRet)
            //    {
            //        AppMessage appMsg = new AppMessage();
            //        appMsg.MessageType = MessageTypeEnum.Permission;
            //        appMsg.RemoveMessageOnRead = true;
            //        appMsg.MessageString = "You don't have permission for blocked employee!";
            //        appMsg.ShowBackButton = true;
            //        Globals.ShowMessagePage(appMsg);
            //        //MMS.Globals.RemoveStatusMessage();
            //        //MMS.Globals.ShowMessagePage(MMS.MessageTypeEnum.Permission, "You don't have read permission for this object", "");
            //    }
            //}

            //AppReport rpt = new AppReport();

            //string title = "Chart Of Accounts";
            //rpt.LocalReportPath = @"Report\GL\rptChartOfAccounts.rdlc";

            //rpt.LocalReportPath = "";

            //clsPrmLedger prmLdg = new clsPrmLedger();
            //prmLdg.LedgerOnly = ddlShowType.SelectedValue == "1" ? true : false;
            //prmLdg.OrderBy = (AccOrderByEnum)Convert.ToInt32(ddlOrderBy.SelectedValue);


            //rpt.AddParameter("prmShowParentGroup", prmLdg.LedgerOnly ? "1" : "0");


            //List<DBClass.Report.GL.rcGLReportItem> rList = BLLibrary.ReportBL.GL.AccountListBL.GetAccountList(prmLdg);

            //rpt.ReportTitle = title;

            int companyID = this.CompanyID;
            string groupLedgerShowType = ddlGroupLedgerShowType.SelectedValue;
            string orderBy = ddlOrderBy.SelectedValue;

            clsPrmLedger prmLdg = new clsPrmLedger();
            prmLdg.CompanyID = companyID;
            prmLdg.GroupLedgerShowType = (GroupsLedgerShowEnum)Convert.ToInt32(ddlGroupLedgerShowType.SelectedValue);
            prmLdg.OrderBy = (AccOrderByEnum)Convert.ToInt32(ddlOrderBy.SelectedValue);
            prmLdg.LocationIDList = GetParamLocationIDList();
            prmLdg.IsLocation = false;


            ReportOptions rptOption = GetReportOptions();

            AppReport rpt = ChartOfAccountsRGN.GenerateChartOfAccounts(prmLdg,rptOption);
            string rk = AppReport.SetAppReportToSession(rpt, this.Context);

            //rpt.AddCriteriaParameter(title);

            //rpt.DataSources.Add(new AppReport.DataSource("rcGLReportItem", rList));
          //  rpt.DataSources.Add(new AppReport.DataSource("rcGLReportItem", rList[0].IncomeStatementItems));
            //rpt.DataSources.Add(new AppReport.DataSource("rcLedgerTran", rList[0].LedgerTrans));

            //rpt.IsReportCriteria = true;
            //rpt.ReportCriteria = title;
            //rpt.ReportTitle = title;
            //rpt.TotalReportSource = 1;
            //rpt.ReportSourceName = "rcSalary";
            //rpt.ReportSourceValue = rList;


            ShowReport(rk);
           
        }


        private void ReportTrialBalance()
        {
            if (Convert.ToInt32(ddlAccYear.SelectedValue) == 0)
            {
                this.SetPageMessage("Please Select Financial Year!", MessageTypeEnum.InvalidData, true);
                base.ShowPageMessage(lblMessage, true);
                ddlAccYear.Focus();
                return;
            }

            int companyID = this.CompanyID;
            int accYearID = Convert.ToInt32(ddlAccYear.SelectedValue);
          
            InculdeOpBalanceEnum includeOpBalanceType = (InculdeOpBalanceEnum)Convert.ToInt32(ddlIncludeOpBal.SelectedValue);
            IncludePostEnum includePostType = (IncludePostEnum)Convert.ToInt32(ddlIncludePostType.SelectedValue);
            AmountShowTypeEnum amountShowType = (AmountShowTypeEnum)Convert.ToInt32(ddlAmountShowType.SelectedValue);

            DateTime dt;

            DateTime? fromDate = null;
            if (DateTime.TryParse(txtFromDate.Text, out dt))
            {
                fromDate = dt;
            }

            DateTime? toDate = null;
            if (DateTime.TryParse(txtToDate.Text, out dt))
            {
                toDate = dt;
            }
         
            clsPrmLedger prmLdg = new clsPrmLedger();
            prmLdg.CompanyID = companyID;
            prmLdg.AccYearID = accYearID;
            prmLdg.FromDate = fromDate;
            prmLdg.ToDate = toDate;
            prmLdg.IncludeOpBalanceType = includeOpBalanceType;
            prmLdg.IncludePostType = includePostType;

            prmLdg.IncludeZeroValue = ddlIncludeZero.SelectedValue == "1" ? true : false;
            prmLdg.AmountShowType = amountShowType;

            prmLdg.GLAccountTypeFilter = GLAccountTypeFilterEnum.NormalControlAccount;

            prmLdg.GroupLedgerShowType =  (GroupsLedgerShowEnum)Convert.ToInt32(ddlGroupLedgerShowType.SelectedValue);
            prmLdg.MaxHierarchyLevel = Convert.ToInt32(ddlHeirarchyLevel.SelectedValue);
            prmLdg.IncludeGLClass = ddlIncludeGLClass.SelectedValue == "1" ? true : false;
            prmLdg.OrderBy = (AccOrderByEnum)Convert.ToInt32(ddlOrderBy.SelectedValue);
            prmLdg.LocationIDList = GetParamLocationIDList();
            prmLdg.IsLocation = false;
 
            ReportOptions rptOption = GetReportOptions();
            AppReport rpt = TrialBalanceRGN.GenerateTrialBalance(prmLdg, rptOption);
            string rk = AppReport.SetAppReportToSession(rpt, this.Context);

            Session[rk] = rpt;
            ShowReport(rk);

        }


        private void ReportIncomeStatement()
        {
            if (Convert.ToInt32(ddlAccYear.SelectedValue) == 0)
            {
                this.SetPageMessage("Please Select Financial Year!", MessageTypeEnum.InvalidData, true);
                base.ShowPageMessage(lblMessage, true);
                ddlAccYear.Focus();
                return;
            }

            DateTime dt;

            DateTime? fromDate = null;
            if (DateTime.TryParse(txtFromDate.Text, out dt))
            {
                fromDate = dt;
            }
            else
            {
                this.SetPageMessage("Please Select date!", MessageTypeEnum.InvalidData, true);
                base.ShowPageMessage(lblMessage, true);
                txtFromDate.Focus();
                return;
            }


            int accYearID = Convert.ToInt32(ddlAccYear.SelectedValue);
            int companyID = this.CompanyID;


            GroupsLedgerShowEnum groupLedgerShowType = (GroupsLedgerShowEnum)Convert.ToInt32(ddlGroupLedgerShowType.SelectedValue);
            InculdeOpBalanceEnum includeOpBalanceType = (InculdeOpBalanceEnum)Convert.ToInt32(ddlIncludeOpBal.SelectedValue);
            IncludePostEnum includePostType = (IncludePostEnum)Convert.ToInt32(ddlIncludePostType.SelectedValue);




            DateTime? toDate = null;
            if (DateTime.TryParse(txtToDate.Text, out dt))
            {
                toDate = dt;
            }

            clsPrmLedger prmLdg = new clsPrmLedger();
            prmLdg.CompanyID = companyID;
            prmLdg.AccYearID = accYearID;

            prmLdg.GLAccountTypeFilter = GLAccountTypeFilterEnum.NormalControlAccount;
            prmLdg.FromDate = fromDate;
            prmLdg.ToDate = toDate;
            prmLdg.IncludeOpBalanceType = includeOpBalanceType;
            prmLdg.IncludePostType = includePostType;
            prmLdg.GroupLedgerShowType = groupLedgerShowType;
            prmLdg.IncludeZeroValue = ddlIncludeZero.SelectedValue == "1" ? true : false;
            prmLdg.MaxHierarchyLevel = Convert.ToInt32(ddlHeirarchyLevel.SelectedValue);
            prmLdg.IncludeGLClass = true;
            prmLdg.IncludeRootGLGroup = true;
            //prmLdg.IncludeLastGroupAccounts = ddlIncludeAllAccount.SelectedValue == "1" ? true : false;

            prmLdg.ShowPercentage = ddlShowPercentage.SelectedValue == "1" ? true : false;

            prmLdg.DisplayBalanceLevel = Convert.ToInt32(ddlHeirarchyLevel.SelectedValue);

            prmLdg.IsNonProfit = false;

            prmLdg.LocationIDList = GetParamLocationIDList();
            prmLdg.IsLocation = false;

            ReportOptions rptOption = GetReportOptions();
            AppReport rpt = IncomeStatementRGN.GenerateIncomeStatement(prmLdg, rptOption);
            string rk = AppReport.SetAppReportToSession(rpt, this.Context);

            ShowReport(rk);

        }


        private void ReportBalanceSheet()
        {
            if (Convert.ToInt32(ddlAccYear.SelectedValue) == 0)
            {
                this.SetPageMessage("Please Select Financial Year!", MessageTypeEnum.InvalidData, true);
                base.ShowPageMessage(lblMessage, true);
                ddlAccYear.Focus();
                return;
            }

            DateTime dt;

            DateTime? fromDate = null;
            if (DateTime.TryParse(txtFromDate.Text, out dt))
            {
                fromDate = dt;
            }
            else
            {
                this.SetPageMessage("Please Select date!", MessageTypeEnum.InvalidData, true);
                base.ShowPageMessage(lblMessage, true);
                txtFromDate.Focus();
                return;
            }

            DateTime? toDate = null;
            if (DateTime.TryParse(txtToDate.Text, out dt))
            {
                toDate = dt;
            }

            int accYearID = Convert.ToInt32(ddlAccYear.SelectedValue);
            int companyID = this.CompanyID;

            GroupsLedgerShowEnum groupLedgerShowType = (GroupsLedgerShowEnum)Convert.ToInt32(ddlGroupLedgerShowType.SelectedValue);
            InculdeOpBalanceEnum includeOpBalanceType = (InculdeOpBalanceEnum)Convert.ToInt32(ddlIncludeOpBal.SelectedValue);
            IncludePostEnum includePostType = (IncludePostEnum)Convert.ToInt32(ddlIncludePostType.SelectedValue);

            clsPrmLedger prmLdg = new clsPrmLedger();
            prmLdg.CompanyID = companyID;
            prmLdg.AccYearID = accYearID;

            prmLdg.GLAccountTypeFilter = GLAccountTypeFilterEnum.NormalControlAccount;
            prmLdg.FromDate = fromDate;
            prmLdg.ToDate = toDate;
            prmLdg.IncludeOpBalanceType = includeOpBalanceType;
            prmLdg.IncludePostType = includePostType;
            prmLdg.GroupLedgerShowType = groupLedgerShowType;
            prmLdg.IncludeZeroValue = ddlIncludeZero.SelectedValue == "1" ? true : false;
            prmLdg.MaxHierarchyLevel = Convert.ToInt32(ddlHeirarchyLevel.SelectedValue);
            prmLdg.IncludeGLClass = true;
            prmLdg.IncludeRootGLGroup = true;
            //prmLdg.IncludeLastGroupAccounts = ddlIncludeAllAccount.SelectedValue == "1" ? true : false;

            prmLdg.ShowLiabilitiesFirst = ddlBalnceSheetShowMethod.SelectedValue == "1" ? true : false;

            prmLdg.ShowPercentage = ddlShowPercentage.SelectedValue == "1" ? true : false;

            prmLdg.ShowProfitLossInLiability = false;

            prmLdg.DisplayBalanceLevel = Convert.ToInt32(ddlHeirarchyLevel.SelectedValue);

            prmLdg.IsNonProfit = false;
            prmLdg.LocationIDList = GetParamLocationIDList();
            prmLdg.IsLocation = false;

            ReportOptions rptOption = GetReportOptions();
            AppReport rpt = BalanceSheetRGN.GenerateBalanceSheet(prmLdg, rptOption);
            string rk = AppReport.SetAppReportToSession(rpt, this.Context);

            ShowReport(rk);
            //Response.Redirect(@"~/Report/ReportView.aspx?rk=" + rk);
            //ClientScript.RegisterStartupScript(this.Page.GetType(), "showreport", string.Format("tbopen('{0}');", rk), true);

        }


        private void ReportReceiptPayment()
        {
            if (Convert.ToInt32(ddlAccYear.SelectedValue) == 0)
            {
                this.SetPageMessage("Please Select Financial Year!", MessageTypeEnum.InvalidData, true);
                base.ShowPageMessage(lblMessage, true);
                ddlAccYear.Focus();
                return;
            }

            DateTime dt;

            DateTime? fromDate = null;
            if (DateTime.TryParse(txtFromDate.Text, out dt))
            {
                fromDate = dt;
            }
            else
            {
                this.SetPageMessage("Please Select date!", MessageTypeEnum.InvalidData, true);
                base.ShowPageMessage(lblMessage, true);
                txtFromDate.Focus();
                return;
            }

            DateTime? toDate = null;
            if (DateTime.TryParse(txtToDate.Text, out dt))
            {
                toDate = dt;
            }


            int accYearID = Convert.ToInt32(ddlAccYear.SelectedValue);
            int companyID = this.CompanyID;


            GroupsLedgerShowEnum groupLedgerShowType = (GroupsLedgerShowEnum)Convert.ToInt32(ddlGroupLedgerShowType.SelectedValue);
            InculdeOpBalanceEnum includeOpBalanceType = (InculdeOpBalanceEnum)Convert.ToInt32(ddlIncludeOpBal.SelectedValue);
            IncludePostEnum includePostType = (IncludePostEnum)Convert.ToInt32(ddlIncludePostType.SelectedValue);


            clsPrmLedger prmLdg = new clsPrmLedger();
            prmLdg.CompanyID = companyID;
            prmLdg.AccYearID = accYearID;


            prmLdg.FromDate = fromDate;
            prmLdg.ToDate = toDate;
            prmLdg.IncludeOpBalanceType = includeOpBalanceType;
            prmLdg.IncludePostType = includePostType;
            prmLdg.GroupLedgerShowType = groupLedgerShowType;
            prmLdg.IncludeZeroValue = ddlIncludeZero.SelectedValue == "1" ? true : false;
            prmLdg.MaxHierarchyLevel = Convert.ToInt32(ddlHeirarchyLevel.SelectedValue);
            prmLdg.IncludeGLClass = true;
            prmLdg.IncludeRootGLGroup = true;
            //prmLdg.IncludeLastGroupAccounts = ddlIncludeAllAccount.SelectedValue == "1" ? true : false;


            prmLdg.IncludeGroupParents = ddlIncludeGroupParents.SelectedValue == "1" ? true : false;


           


            prmLdg.DisplayBalanceLevel = Convert.ToInt32(ddlHeirarchyLevel.SelectedValue);

            prmLdg.IsNonProfit = false;

            prmLdg.LocationIDList = GetParamLocationIDList();

            prmLdg.IsLocation = false;


            ReportOptions rptOption = GetReportOptions();
            AppReport rpt = ReceiptPaymentRGN.GenerateReceiptPayment(prmLdg, rptOption);
            string rk = AppReport.SetAppReportToSession(rpt, this.Context);

            ShowReport(rk);
            //Response.Redirect(@"~/Report/ReportView.aspx?rk=" + rk);
            //ClientScript.RegisterStartupScript(this.Page.GetType(), "showreport", string.Format("tbopen('{0}');", rk), true);

        }


        private void ReportLedger()
        {
            if (Convert.ToInt32(ddlAccYear.SelectedValue) == 0)
            {
                this.SetPageMessage("Please Select Financial Year!", MessageTypeEnum.InvalidData, true);
                base.ShowPageMessage(lblMessage, true);
                ddlAccYear.Focus();
                return;
            }


            if (ReadDateRangeParam(false) == false)
            {
                return;
            }



            string glAccountCode = txtGLAccount.Text.Trim();
            if (glAccountCode == string.Empty)
            {
                this.SetPageMessage("Please Enter Account!", MessageTypeEnum.InvalidData, true);
                base.ShowPageMessage(lblMessage, true);
                txtGLAccount.Focus();
                return;
            }

            dcGLAccount glAcc = GLAccountBL.GetGLAccountByCode(this.CompanyID, glAccountCode);
            if (glAcc == null)
            {
                this.SetPageMessage("GL Account Not Found!", MessageTypeEnum.InvalidData, true);
                base.ShowPageMessage(lblMessage, true);
                txtGLAccount.Focus();
                return;
            }


            hdnGLAccountID.Value = glAcc.GLAccountID.ToString();

            DateTime dt;

            DateTime? fromDate = null;
            if (DateTime.TryParse(txtFromDate.Text, out dt))
            {
                fromDate = dt;
            }
            else
            {
                this.SetPageMessage("Please Select date!", MessageTypeEnum.InvalidData, true);
                base.ShowPageMessage(lblMessage, true);
                txtFromDate.Focus();
                return;
            }
            DateTime? toDate = null;
            if (DateTime.TryParse(txtToDate.Text, out dt))
            {
                toDate = dt;
            }



            int accYearID = Convert.ToInt32(ddlAccYear.SelectedValue);
            int accID = Convert.ToInt32(hdnGLAccountID.Value);
            int companyID = this.CompanyID;

            InculdeOpBalanceEnum includeOpBalanceType = (InculdeOpBalanceEnum)Convert.ToInt32(ddlIncludeOpBal.SelectedValue);
            IncludePostEnum includePostType = (IncludePostEnum)Convert.ToInt32(ddlIncludePostType.SelectedValue);


            //int dBlock = Convert.ToInt32(ddlBlock.SelectedValue);

            //if (dBlock == 0 | dBlock == 2)
            //{
            //    bool pRet = AppSecurity.CheckObjectPermissionByUserID(this.LoginUserID, AppObjectEnum.FrmOpt_BlockedEmployee, PermissionEnum.Read);
            //    if (!pRet)
            //    {
            //        AppMessage appMsg = new AppMessage();
            //        appMsg.MessageType = MessageTypeEnum.Permission;
            //        appMsg.RemoveMessageOnRead = true;
            //        appMsg.MessageString = "You don't have permission for blocked employee!";
            //        appMsg.ShowBackButton = true;
            //        Globals.ShowMessagePage(appMsg);
            //        //MMS.Globals.RemoveStatusMessage();
            //        //MMS.Globals.ShowMessagePage(MMS.MessageTypeEnum.Permission, "You don't have read permission for this object", "");
            //    }
            //}

            //List<DBClass.Report.GL.rcLedger> rList = new List<DBClass.Report.GL.rcLedger>();

            clsPrmLedger prmLdg = new clsPrmLedger();
            prmLdg.CompanyID = companyID;
            prmLdg.GLAccountID = accID;
            prmLdg.AccYearID = accYearID;


            prmLdg.FromDate = fromDate;
            prmLdg.ToDate = toDate;
            prmLdg.IncludeOpBalanceType = includeOpBalanceType;
            prmLdg.IncludePostType = includePostType;



            prmLdg.IncludeInstrument = ddlIncludeInstrument.SelectedValue == "1" ? true : false;
            prmLdg.IncludeTranCode = ddlIncludeTranCode.SelectedValue == "1" ? true : false;
            prmLdg.IncludeCostCenter = ddlIncludeCostCenter.SelectedValue == "1" ? true : false;
            prmLdg.IncludeReference = ddlIncludeReference.SelectedValue == "1" ? true : false;

            prmLdg.IncludeContraEntry = ddlIncludeContraEntry.SelectedValue == "1" ? true : false;
            prmLdg.IncludeDetOfDetails = ddlIncludeDetOfDetails.SelectedValue == "1" ? true : false;

            prmLdg.ControlAccountSummary = true;
            prmLdg.IncludeSubAccountForControl = ddlIncludeSubForControl.SelectedValue == "1" ? true : false;
            prmLdg.LocationIDList = GetParamLocationIDList();
            prmLdg.IsLocation = true;
            //rList.Add(BLLibrary.ReportBL.GL.LedgerReportBL.GetLedgerData(prmLedger));

           
            //rpt.AddCriteriaParameter(title);
           
            //rpt.DataSources.Add(new AppReport.DataSource("rcLedger",rList));
            //rpt.DataSources.Add(new AppReport.DataSource("rcLedgerTran", rList[0].LedgerTrans));


            //rpt.IsReportCriteria = true;
            //rpt.ReportCriteria = title;
            //rpt.ReportTitle = title;
            //rpt.TotalReportSource = 1;
            //rpt.ReportSourceName = "rcSalary";
            //rpt.ReportSourceValue = rList;


            ReportOptions rptOption = GetReportOptions();
            AppReport rpt = LedgerRGN.GenerateLedger(prmLdg, rptOption);
            string rk = AppReport.SetAppReportToSession(rpt, this.Context);



            //string rk = "rk_1550";
            //Session[rk] = rpt;
            ShowReport(rk);
            //Response.Redirect(@"~/Report/ReportView.aspx?rk=" + rk);
            //ClientScript.RegisterStartupScript(this.Page.GetType(), "showreport", string.Format("tbopen('{0}');", rk), true);

        }


        private void ReportJournal()
        {

            int yearID = Convert.ToInt32(ddlAccYear.SelectedValue);
            if (yearID == 0)
            {
                this.SetPageMessage("Please Select Financial Year!", MessageTypeEnum.InvalidData, true);
                base.ShowPageMessage(lblMessage, true);
                ddlAccYear.Focus();
                return;
            }

            string jNo = txtJournalNo.Text.Trim();
            if (jNo == string.Empty)
            {
                this.SetPageMessage("Please Enter Journal No!", MessageTypeEnum.InvalidData, true);
                base.ShowPageMessage(lblMessage, true);
                txtJournalNo.Focus();
                return;
            }

            dcJournal jrnl = JournalBL.GetJournalByNo(this.CompanyID, yearID, jNo);
            if (jrnl == null)
            {
                this.SetPageMessage("Journal No Not Found!", MessageTypeEnum.InvalidData, true);
                base.ShowPageMessage(lblMessage, true);
                txtJournalNo.Focus();
                return;
            }


            hdnJournalID.Value = jrnl.JournalID.ToString();

            int journalID = jrnl.JournalID;
            if (journalID == 0)
            {
                this.SetPageMessage("Please Enter Journal No!", MessageTypeEnum.InvalidData, true);
                base.ShowPageMessage(lblMessage, true);
                txtJournalNo.Focus();
                return;
            }

            int accYearID = Convert.ToInt32(ddlAccYear.SelectedValue);
            int companyID = this.CompanyID;

            InculdeOpBalanceEnum includeOpBalanceType = (InculdeOpBalanceEnum)Convert.ToInt32(ddlIncludeOpBal.SelectedValue);
            IncludePostEnum includePostType = (IncludePostEnum)Convert.ToInt32(ddlIncludePostType.SelectedValue);
            JournalReportFormatEnum journalReportFormat = (JournalReportFormatEnum)Convert.ToInt32(ddlReportFormat.SelectedValue);

            clsPrmLedger prmLdg = new clsPrmLedger();
            prmLdg.CompanyID = companyID;
            prmLdg.JournalID = journalID;
            prmLdg.AccYearID = accYearID;

            prmLdg.IncludeOpBalanceType = includeOpBalanceType;
            prmLdg.IncludePostType = includePostType;

            prmLdg.IncludeInstrument = ddlIncludeInstrument.SelectedValue == "1" ? true : false;
            prmLdg.IncludeTranCode = ddlIncludeTranCode.SelectedValue == "1" ? true : false;
            prmLdg.IncludeCostCenter = ddlIncludeCostCenter.SelectedValue == "1" ? true : false;
            prmLdg.IncludeReference = ddlIncludeReference.SelectedValue == "1" ? true : false;

           
            prmLdg.IncludeSubAccountForControl = ddlIncludeSubForControl.SelectedValue == "1" ? true : false;


            prmLdg.JournalReportFormat = journalReportFormat;
            prmLdg.LocationID = Convert.ToInt32(ddlLocation.SelectedValue);
            prmLdg.LocationIDList = GetParamLocationIDList();
            prmLdg.IsLocation = true;

            //rList.Add(BLLibrary.ReportBL.GL.LedgerReportBL.GetLedgerData(prmLedger));

            ReportOptions rptOption = GetReportOptions();

            AppReport rpt = JournalRGN.GenerateJournal(prmLdg, rptOption);

            string rk = AppReport.SetAppReportToSession(rpt, this.Context);


           


            //string rk = "rk_1550";
            //Session[rk] = rpt;
            ShowReport(rk);
            //Response.Redirect(@"~/Report/ReportView.aspx?rk=" + rk);
            //ClientScript.RegisterStartupScript(this.Page.GetType(), "showreport", string.Format("tbopen('{0}');", rk), true);

        }

        public List<int> GetParamLocationIDList()
        {
            List<int> locIDList = new List<int>();
            int locID = Convert.ToInt32(ddlLocation.SelectedValue);

            if (locID == 0)
            {
                locIDList = AppSecurity.GetValidLocationUserList().Select(c => c.LocationID).ToList();
            }
            else
            {
                locIDList.Add(locID);
            }
            return locIDList;
        }


        private void ReportJournalList()
        {
            if (Convert.ToInt32(ddlAccYear.SelectedValue) == 0)
            {
                this.SetPageMessage("Please Select Financial Year!", MessageTypeEnum.InvalidData, true);
                base.ShowPageMessage(lblMessage, true);
                ddlAccYear.Focus();
                return;
            }

            if (ReadDateRangeParam(true) == false)
            {
                return;
            }

            int accYearID = Convert.ToInt32(ddlAccYear.SelectedValue);
            int companyID = this.CompanyID;

            int journalTypeID = Convert.ToInt32(ddlJournalType.SelectedValue);

            InculdeOpBalanceEnum includeOpBalanceType = (InculdeOpBalanceEnum)Convert.ToInt32(ddlIncludeOpBal.SelectedValue);
            IncludePostEnum includePostType = (IncludePostEnum)Convert.ToInt32(ddlIncludePostType.SelectedValue);

            clsPrmLedger prmLdg = new clsPrmLedger();
            prmLdg.CompanyID = companyID;
            prmLdg.JournalID = 0;
            prmLdg.AccYearID = accYearID;
            prmLdg.JournalTypeID = journalTypeID;

            prmLdg.FromDate = fromDate;
            prmLdg.ToDate = toDate;
            prmLdg.IncludeOpBalanceType = includeOpBalanceType;
            prmLdg.IncludePostType = includePostType;
            
            
            
            prmLdg.LocationID = Convert.ToInt32(ddlLocation.SelectedValue);
            prmLdg.LocationIDList = GetParamLocationIDList();
            prmLdg.IsLocation = true;



            ReportOptions rptOption = GetReportOptions();

            AppReport rpt = JournalRGN.GenerateJournalList(prmLdg, rptOption);

            string rk = AppReport.SetAppReportToSession(rpt, this.Context);
            ShowReport(rk);
            //Response.Redirect(@"~/Report/ReportView.aspx?rk=" + rk);
            //ClientScript.RegisterStartupScript(this.Page.GetType(), "showreport", string.Format("tbopen('{0}');", rk), true);

        }

        private void ReportJournalBook()
        {
            if (Convert.ToInt32(ddlAccYear.SelectedValue) == 0)
            {
                this.SetPageMessage("Please Select Financial Year!", MessageTypeEnum.InvalidData, true);
                base.ShowPageMessage(lblMessage, true);
                ddlAccYear.Focus();
                return;
            }

            if (ReadDateRangeParam(true) == false)
            {
                return;
            }

            int accYearID = Convert.ToInt32(ddlAccYear.SelectedValue);
            int companyID = this.CompanyID;

            int journalTypeID = Convert.ToInt32(ddlJournalType.SelectedValue);

            InculdeOpBalanceEnum includeOpBalanceType = (InculdeOpBalanceEnum)Convert.ToInt32(ddlIncludeOpBal.SelectedValue);
            IncludePostEnum includePostType = (IncludePostEnum)Convert.ToInt32(ddlIncludePostType.SelectedValue);

            clsPrmLedger prmLdg = new clsPrmLedger();
            prmLdg.CompanyID = companyID;
            prmLdg.JournalID = 0;
            prmLdg.AccYearID = accYearID;

            prmLdg.JournalTypeID = journalTypeID;

            prmLdg.FromDate = fromDate;
            prmLdg.ToDate = toDate;
            prmLdg.IncludeOpBalanceType = includeOpBalanceType;
            prmLdg.IncludePostType = includePostType;

            prmLdg.IncludeInstrument = ddlIncludeInstrument.SelectedValue == "1" ? true : false;
            prmLdg.IncludeTranCode = ddlIncludeTranCode.SelectedValue == "1" ? true : false;
            prmLdg.IncludeCostCenter = ddlIncludeCostCenter.SelectedValue == "1" ? true : false;
            prmLdg.IncludeReference = ddlIncludeReference.SelectedValue == "1" ? true : false;


            prmLdg.ControlAccountSummary = ddlControlAccountSummary.SelectedValue == "1" ? true : false;
            prmLdg.IncludeSubAccountForControl = ddlIncludeSubForControl.SelectedValue == "1" ? true : false;
            prmLdg.LocationIDList = GetParamLocationIDList();
            prmLdg.IsLocation = true;


            ReportOptions rptOption = GetReportOptions();

            AppReport rpt = JournalRGN.GenerateJournalBook(prmLdg, rptOption);

            string rk = AppReport.SetAppReportToSession(rpt, this.Context);



            //string rk = "rk_1550";
            //Session[rk] = rpt;
            ShowReport(rk);
            //Response.Redirect(@"~/Report/ReportView.aspx?rk=" + rk);
            //ClientScript.RegisterStartupScript(this.Page.GetType(), "showreport", string.Format("tbopen('{0}');", rk), true);

        }

        private void ReportCashJournalBook()
        {
            if (Convert.ToInt32(ddlAccYear.SelectedValue) == 0)
            {
                this.SetPageMessage("Please Select Financial Year!", MessageTypeEnum.InvalidData, true);
                base.ShowPageMessage(lblMessage, true);
                ddlAccYear.Focus();
                return;
            }

            if (ReadDateRangeParam(true) == false)
            {
                return;
            }

            int accYearID = Convert.ToInt32(ddlAccYear.SelectedValue);
            int companyID = this.CompanyID;

            int journalTypeID = Convert.ToInt32(ddlJournalType.SelectedValue);

            InculdeOpBalanceEnum includeOpBalanceType = (InculdeOpBalanceEnum)Convert.ToInt32(ddlIncludeOpBal.SelectedValue);
            IncludePostEnum includePostType = (IncludePostEnum)Convert.ToInt32(ddlIncludePostType.SelectedValue);

            clsPrmLedger prmLdg = new clsPrmLedger();
            prmLdg.CompanyID = companyID;
            prmLdg.JournalID = 0;
            prmLdg.AccYearID = accYearID;

            prmLdg.JournalTypeID = journalTypeID;

            prmLdg.FromDate = fromDate;
            prmLdg.ToDate = toDate;
            prmLdg.IncludeOpBalanceType = includeOpBalanceType;
            prmLdg.IncludePostType = includePostType;

            prmLdg.IncludeInstrument = ddlIncludeInstrument.SelectedValue == "1" ? true : false;
            prmLdg.IncludeTranCode = ddlIncludeTranCode.SelectedValue == "1" ? true : false;
            prmLdg.IncludeCostCenter = ddlIncludeCostCenter.SelectedValue == "1" ? true : false;
            prmLdg.IncludeReference = ddlIncludeReference.SelectedValue == "1" ? true : false;


            prmLdg.ControlAccountSummary = ddlControlAccountSummary.SelectedValue == "1" ? true : false;
            prmLdg.IncludeSubAccountForControl = ddlIncludeSubForControl.SelectedValue == "1" ? true : false;
            prmLdg.LocationIDList = GetParamLocationIDList();
            prmLdg.IsLocation = false;

            ReportOptions rptOption = GetReportOptions();

            AppReport rpt = JournalRGN.GenerateCashJournalBook(prmLdg, rptOption);

            string rk = AppReport.SetAppReportToSession(rpt, this.Context);



            //string rk = "rk_1550";
            //Session[rk] = rpt;
            ShowReport(rk);
            //Response.Redirect(@"~/Report/ReportView.aspx?rk=" + rk);
            //ClientScript.RegisterStartupScript(this.Page.GetType(), "showreport", string.Format("tbopen('{0}');", rk), true);

        }


        private void ReportCashSummary()
        {
            if (Convert.ToInt32(ddlAccYear.SelectedValue) == 0)
            {
                this.SetPageMessage("Please Select Financial Year!", MessageTypeEnum.InvalidData, true);
                base.ShowPageMessage(lblMessage, true);
                ddlAccYear.Focus();
                return;
            }

            if (ReadDateRangeParam(true) == false)
            {
                return;
            }

            int accYearID = Convert.ToInt32(ddlAccYear.SelectedValue);
            int companyID = this.CompanyID;

            //int journalTypeID = Convert.ToInt32(ddlJournalType.SelectedValue);
            
            GroupsLedgerShowEnum groupLedgerShowType = (GroupsLedgerShowEnum)Convert.ToInt32(ddlGroupLedgerShowType.SelectedValue);
            InculdeOpBalanceEnum includeOpBalanceType = (InculdeOpBalanceEnum)Convert.ToInt32(ddlIncludeOpBal.SelectedValue);
            IncludePostEnum includePostType = (IncludePostEnum)Convert.ToInt32(ddlIncludePostType.SelectedValue);

            clsPrmLedger prmLdg = new clsPrmLedger();
            prmLdg.CompanyID = companyID;
            prmLdg.JournalID = 0;
            prmLdg.AccYearID = accYearID;
         

            prmLdg.FromDate = fromDate;
            prmLdg.ToDate = toDate;
            prmLdg.IncludeOpBalanceType = InculdeOpBalanceEnum.IncludeALL;
            prmLdg.IncludePostType = includePostType;

            prmLdg.GroupLedgerShowType = GroupsLedgerShowEnum.Ledgers;
            prmLdg.IncludeZeroValue = ddlIncludeZero.SelectedValue == "1" ? true : false;
            prmLdg.MaxHierarchyLevel = -1;

            prmLdg.IncludeGroupParents = false;
            prmLdg.LocationIDList = GetParamLocationIDList();
            prmLdg.IsLocation = false;

            ReportOptions rptOption = GetReportOptions();

            AppReport rpt = CashRGN.GenerateCashSummary(prmLdg, rptOption);

            string rk = AppReport.SetAppReportToSession(rpt, this.Context);
            ShowReport(rk);
            //Response.Redirect(@"~/Report/ReportView.aspx?rk=" + rk);
            //ClientScript.RegisterStartupScript(this.Page.GetType(), "showreport", string.Format("tbopen('{0}');", rk), true);

        }


        private void ReportLedgerSummary()
        {
            if (Convert.ToInt32(ddlAccYear.SelectedValue) == 0)
            {
                this.SetPageMessage("Please Select Financial Year!", MessageTypeEnum.InvalidData, true);
                base.ShowPageMessage(lblMessage, true);
                ddlAccYear.Focus();
                return;
            }

            if (ReadDateRangeParam(true) == false)
            {
                return;
            }

            int accYearID = Convert.ToInt32(ddlAccYear.SelectedValue);
            int companyID = this.CompanyID;

            //int journalTypeID = Convert.ToInt32(ddlJournalType.SelectedValue);

            GroupsLedgerShowEnum groupLedgerShowType = (GroupsLedgerShowEnum)Convert.ToInt32(ddlGroupLedgerShowType.SelectedValue);
            InculdeOpBalanceEnum includeOpBalanceType = (InculdeOpBalanceEnum)Convert.ToInt32(ddlIncludeOpBal.SelectedValue);
            IncludePostEnum includePostType = (IncludePostEnum)Convert.ToInt32(ddlIncludePostType.SelectedValue);

            clsPrmLedger prmLdg = new clsPrmLedger();
            prmLdg.CompanyID = companyID;
            prmLdg.AccYearID = accYearID;


            prmLdg.FromDate = fromDate;
            prmLdg.ToDate = toDate;
            prmLdg.IncludeOpBalanceType = includeOpBalanceType;//InculdeOpBalanceEnum.IncludeALL;
            prmLdg.IncludePostType = includePostType;
            prmLdg.AmountShowType = AmountShowTypeEnum.OpeningTransClosing;
            prmLdg.GroupLedgerShowType = groupLedgerShowType;
            prmLdg.IncludeZeroValue = ddlIncludeZero.SelectedValue == "1" ? true : false;
            prmLdg.MaxHierarchyLevel = Convert.ToInt32(ddlHeirarchyLevel.SelectedValue);

            prmLdg.GLAccountTypeFilter = GLAccountTypeFilterEnum.NormalControlAccount;

            prmLdg.IncludeGroupParents = false;
            prmLdg.IncludeGroupChilds = ddlIncludeGroupChilds.SelectedValue == "1" ? true : false;


            prmLdg.GLGroupID = Convert.ToInt32(hdnGLGroupID.Value);
            prmLdg.GLAccountID = Convert.ToInt32(hdnGLAccountID.Value);
            prmLdg.LocationIDList = GetParamLocationIDList();
            prmLdg.IsLocation = true;

            ReportOptions rptOption = GetReportOptions();

            AppReport rpt = LedgerRGN.GenerateLedgerSummary(prmLdg, rptOption);

            string rk = AppReport.SetAppReportToSession(rpt, this.Context);
            ShowReport(rk);
            //Response.Redirect(@"~/Report/ReportView.aspx?rk=" + rk);
            //ClientScript.RegisterStartupScript(this.Page.GetType(), "showreport", string.Format("tbopen('{0}');", rk), true);

        }


        private void ReportAccRefSummary(AccRefTypeEnum pAccRefType)
        {
            if (Convert.ToInt32(ddlAccYear.SelectedValue) == 0)
            {
                this.SetPageMessage("Please Select Financial Year!", MessageTypeEnum.InvalidData, true);
                base.ShowPageMessage(lblMessage, true);
                ddlAccYear.Focus();
                return;
            }

            if (ReadDateRangeParam(true) == false)
            {
                return;
            }

            int accYearID = Convert.ToInt32(ddlAccYear.SelectedValue);
            int companyID = this.CompanyID;

            //int journalTypeID = Convert.ToInt32(ddlJournalType.SelectedValue);

           // GroupsLedgerShowEnum groupLedgerShowType = (GroupsLedgerShowEnum)Convert.ToInt32(ddlGroupLedgerShowType.SelectedValue);
            InculdeOpBalanceEnum includeOpBalanceType = (InculdeOpBalanceEnum)Convert.ToInt32(ddlIncludeOpBal.SelectedValue);
            IncludePostEnum includePostType = (IncludePostEnum)Convert.ToInt32(ddlIncludePostType.SelectedValue);


            //AccRefTypeEnum accRefType = (AccRefTypeEnum)Convert.ToInt32(.SelectedValue);
            int accRefCategoryID = Convert.ToInt32(ddlAccRefCategory.SelectedValue);
            int accRefID = Convert.ToInt32(hdnAccRefID.Value);
            int glAccountID = Convert.ToInt32(hdnGLAccountID.Value);

            clsPrmLedger prmLdg = new clsPrmLedger();
            prmLdg.CompanyID = companyID;
            prmLdg.AccYearID = accYearID;


            prmLdg.FromDate = fromDate;
            prmLdg.ToDate = toDate;
            prmLdg.IncludeOpBalanceType = includeOpBalanceType;
            prmLdg.IncludePostType = includePostType;

            prmLdg.AccRefType = pAccRefType;
            prmLdg.AccRefCategoryID = accRefCategoryID;
            prmLdg.AccRefID = accRefID;

            prmLdg.GLAccountID = glAccountID;
           
            prmLdg.IncludeZeroValue = ddlIncludeZero.SelectedValue == "1" ? true : false;
            prmLdg.MaxHierarchyLevel = -1;

            prmLdg.IncludeGroupParents = false;
            prmLdg.LocationIDList = GetParamLocationIDList();
            prmLdg.IsLocation = false;
            //prmLdg.LocationIDList = GetParamLocationIDList();
            //prmLdg.LocationIDList = GetParamLocationIDList();

            ReportOptions rptOption = GetReportOptions();

            AppReport rpt = AccRefRGN.GenerateAccRefSummary(prmLdg, rptOption);

            string rk = AppReport.SetAppReportToSession(rpt, this.Context);
            ShowReport(rk);
            //Response.Redirect(@"~/Report/ReportView.aspx?rk=" + rk);
            //ClientScript.RegisterStartupScript(this.Page.GetType(), "showreport", string.Format("tbopen('{0}');", rk), true);

        }


        private void ReportAccRefDetails(AccRefTypeEnum pAccRefType)
        {
            if (Convert.ToInt32(ddlAccYear.SelectedValue) == 0)
            {
                this.SetPageMessage("Please Select Financial Year!", MessageTypeEnum.InvalidData, true);
                base.ShowPageMessage(lblMessage, true);
                ddlAccYear.Focus();
                return;
            }

            int accRefID = Convert.ToInt32(hdnAccRefID.Value);
            //if (accRefID == 0)
            //{
            //    this.SetPageMessage("Please Select Code!", MessageTypeEnum.InvalidData, true);
            //    base.ShowPageMessage(lblMessage, true);
            //    txtAccRefCode.Focus();
            //    return;
            //}



            if (ReadDateRangeParam(true) == false)
            {
                return;
            }

            int accYearID = Convert.ToInt32(ddlAccYear.SelectedValue);
            int companyID = this.CompanyID;

            int glAccountID = Convert.ToInt32(hdnGLAccountID.Value);

            //int journalTypeID = Convert.ToInt32(ddlJournalType.SelectedValue);

            // GroupsLedgerShowEnum groupLedgerShowType = (GroupsLedgerShowEnum)Convert.ToInt32(ddlGroupLedgerShowType.SelectedValue);
            InculdeOpBalanceEnum includeOpBalanceType = (InculdeOpBalanceEnum)Convert.ToInt32(ddlIncludeOpBal.SelectedValue);
            IncludePostEnum includePostType = (IncludePostEnum)Convert.ToInt32(ddlIncludePostType.SelectedValue);


            //AccRefTypeEnum accRefType = (AccRefTypeEnum)Convert.ToInt32(.SelectedValue);
            int accRefCategoryID = Convert.ToInt32(ddlAccRefCategory.SelectedValue);
           


            clsPrmLedger prmLdg = new clsPrmLedger();
            prmLdg.CompanyID = companyID;
            prmLdg.AccYearID = accYearID;


            prmLdg.FromDate = fromDate;
            prmLdg.ToDate = toDate;
            prmLdg.IncludeOpBalanceType = includeOpBalanceType;
            prmLdg.IncludePostType = includePostType;

            prmLdg.GLAccountID = glAccountID;
            prmLdg.AccRefType = pAccRefType;
            prmLdg.AccRefCategoryID = accRefCategoryID;
            prmLdg.AccRefID = accRefID;

            prmLdg.IncludeZeroValue = ddlIncludeZero.SelectedValue == "1" ? true : false;
            prmLdg.MaxHierarchyLevel = -1;

            prmLdg.IncludeGroupParents = false;
            prmLdg.LocationIDList = GetParamLocationIDList();
            prmLdg.IsLocation = false;

            ReportOptions rptOption = GetReportOptions();

            AppReport rpt = AccRefRGN.GenerateAccRefDetails(prmLdg, rptOption);

            string rk = AppReport.SetAppReportToSession(rpt, this.Context);
            ShowReport(rk);
            //Response.Redirect(@"~/Report/ReportView.aspx?rk=" + rk);
            //ClientScript.RegisterStartupScript(this.Page.GetType(), "showreport", string.Format("tbopen('{0}');", rk), true);

        }


        protected void ddlReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetReportParam();
           // SetBankBranch();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            this.ReportOpenType = ReportOpenTypeEnum.Export;
            OpenReport();
        }

        protected void ddlPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void ddlAccYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetFromToDate();

        }

        protected void tvwReport_SelectedNodeChanged(object sender, EventArgs e)
        {
            string rptID = tvwReport.SelectedNode.Value;

            SetReportParam();
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            this.ReportOpenType = ReportOpenTypeEnum.Print;
            OpenReport();
        }

        protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rptID = Convert.ToInt32(tvwReport.SelectedNode.Value);

            ReportIDEnum rptIDEnum = (ReportIDEnum)rptID;

            switch (rptIDEnum)
            {
                case ReportIDEnum.GL_Journal:
                    SetReportParamJournal();
                    break;
                case ReportIDEnum.GL_Ledger:
                    SetReportParamLedger();
                    break;
                case ReportIDEnum.GL_CashSummary:
                    SetReportParamCashSummary();
                    break;
            }


           
        }


    }
}
