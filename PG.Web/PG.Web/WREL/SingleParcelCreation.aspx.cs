using PG.BLLibrary.InventoryBL;
using PG.Core;
using PG.Core.DBBase;
using PG.Core.Utility;
using PG.Core.Web;
using PG.DBClass.InventoryDC;
using PG.DBClass.SecurityDC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PG.BLLibrary.OrganizationBL;
using PG.Report.ReportRBL.InventoryRBL;
using PG.Report.ReportEnums;
using PG.Report;
using PG.Report.ReportGen.InventoryRGN;
using PG.DBClass.HMSDC;
using PG.BLLibrary.HMSBL;
using PG.DBClass.WRELDC;
using PG.BLLibrary.WRElBL;
using System.Collections;

namespace PG.Web.WREL
{
    public partial class SingleParcelCreation : BagePage
    {

        //this 
        string ViewStateKey = "CN_ID";
        string ViewStateKeyPrev = "CN_ID_PREV";
        ReportOpenTypeEnum ReportOpenType = ReportOpenTypeEnum.Preview;
        // int CompanyID = 0;

        int CN_ID = 0;
        private int totalRowCount = 0;
        string saveMsg = string.Empty;
        string errMsg = string.Empty;

        private dcUser loggedinUser = null;
        public string ReportViewPageLink = PageLinks.ReportLinks.GetLink_ReportView;
        public string ReportViewPDFPageLink = PageLinks.ReportLinks.GetLink_ReportViewPDF;
        public string ReportPrintPageLink = PageLinks.ReportLinks.GetLink_ReportPrint;
        public string ReportPDFPageLink = PageLinks.ReportLinks.GetLink_ReportPDF;



        public string DistrictListServiceLink = PageLinks.InventoryLink.GetLink_DistrictList;
        public string TownListServiceLink = PageLinks.InventoryLink.GetLink_TownList;
        public string RouteListServiceLink = PageLinks.InventoryLink.GetLink_RouteList;
        public string CNListServiceLink = PageLinks.InventoryLink.GetLink_CNMasterList;
        public string ClientListServiceLink = PageLinks.InventoryLink.GetLink_ClientList;
        public string AgreementDetailsListServiceLink = PageLinks.InventoryLink.GetLink_AgreementDtlList;
        public string HubListServiceLink = PageLinks.InventoryLink.GetLink_HubList;
        public string DepartmentListbyClientIDServiceLink = PageLinks.InventoryLink.GetLink_DepartmentListbyClientID;
        public string ItemListServiceLink = PageLinks.InventoryLink.GetLink_ItemListByAgreement;
        public string DistanceTypeServiceLink = PageLinks.InventoryLink.GetLink_DistanceTypeList;


        List<dcCN_CREATION_MST> listDetails = new List<dcCN_CREATION_MST>();

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

            loggedinUser = AppSecurity.GetUserInfoFromSession();

            this.CN_ID = base.GetPageQueryInteger("id");

            if (!IsPostBack) //first Time
            {


                hdnLoggedInUser.Value = loggedinUser.UserID.ToString();
                hdnHubId.Value = "1";
                txtHubName.Text = "Dhaka Central Hub";
                if (this.CN_ID == 0) //not query string
                {

                    SetDate();
                    AddTask();
                    this.EditMode = FormDataMode.Add;
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

                }

            }
            else
            {
                this.EditMode = base.GetEditModeFromViewState(base.EditModeViewStateKey);
                this.CN_ID = int.Parse(ViewState[ViewStateKey].ToString());
            }

            SetHyperLink();

            //this.ShowPageMessage(this.lblMessage);
            // Response.Write("UserID : " + this.UserID.ToString());

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            EditTask();

            //List<dcCN_CREATION_MST> roomList = HMRESERVATION_DTLBL.GetRoomInfoList();
            //GridView1.DataSource = roomList;
            //GridView1.DataBind();
        }

        public void FillCombo()
        {
            //ddlCountryId.Items.Clear();
            //ddlCountryId.AppendDataBoundItems = true;
            //ddlCountryId.DataTextField = "COUNTRY_NAME";
            //ddlCountryId.DataValueField = "COUNTRY_ID";
            //ddlCountryId.DataSource = HMCOUNTRY_MSTBL.GetCountryList();
            //ddlCountryId.DataBind();
            //ddlCountryId.SelectedIndex = 0;


        }

        protected override void Render(HtmlTextWriter writer)
        {

            //Page.ClientScript.RegisterForEventValidation(btnPopupTrigger.UniqueID);
            //Page.ClientScript.RegisterForEventValidation(btnPopupTrigger.UniqueID, "");
            //Page.ClientScript.RegisterForEventValidation(btnPopupTrigger.UniqueID, "fillcombo");
            //Page.ClientScript.RegisterForEventValidation(btnPopupTrigger.UniqueID, "getbalance");

            base.Render(writer);
        }

        private void SetDate()
        {


        }

        private void ReadTask()
        {
            this.EditMode = FormDataMode.Read;
            ReadData(this.CN_ID);
            ViewState[ViewStateKey] = this.CN_ID.ToString();

            SetControl(FormDataMode.Read);

        }
        private void AddTask()
        {
            ViewState[ViewStateKeyPrev] = this.CN_ID.ToString();

            this.EditMode = FormDataMode.Add;
            this.IsDirty = false;
            this.CN_ID = 0;
            ViewState[ViewStateKey] = "0";
            //add
            SetControl(FormDataMode.Add);
        }
        private void EditTask()
        {
            this.EditMode = FormDataMode.Edit;
            ReadData(this.CN_ID);
            this.EditMode = FormDataMode.Edit;
            ViewState[ViewStateKey] = this.CN_ID.ToString();
            SetControl(FormDataMode.Edit);
        }

        private bool ReadData(int id)
        {
            bool bStatus = false;

            dcCN_CREATION_MST cObj = CN_CREATION_MSTBL.GetCNInfoListById(id, null).FirstOrDefault();
            if (cObj != null)
            {
                txtCNNo.Text = cObj.CN_NUMBER;
                txtTotalAmount.Text = cObj.TOTAL_AMT.ToString();
                txtAmountTk.Text = cObj.TAKA.ToString();
                txtRate.Text = cObj.RATE.ToString();
                txtQuantity.Text = cObj.QTY.ToString();
                txtWeight.Text = cObj.WEIGHT.ToString();
                txtSLADays.Text = cObj.SLA_DAYS.ToString();
                txtRecipientMobileNo.Text = cObj.CONSIGNEE_MOBILE_NO;
                txtRecipientAddress.Text = cObj.CONSIGNEE_ADDRESS;
                txtRecipientName.Text = cObj.CONSIGNEE_NAME;
                hdnHubId.Value = cObj.HUB_ID.ToString();
                hdnDeptID.Value = cObj.CLIENT_DEPT_ID.ToString();
                txtServiceCharge.Text = cObj.SERVICE_AMOUNT.ToString();
                txtItemName.Text = cObj.ITEM_NAME;
                hdnItemId.Value = cObj.ITEM_ID.ToString();
                hdnAggrementDtlId.Value = cObj.AGR_DETAIL_ID.ToString();
                txtClientName.Text = cObj.CLIENT_NAME;
                txtDepartment.Text = cObj.DEPT_NAME;
                txtDistanceType.Text = cObj.DISTANCE_TYPE_NAME;
                hdnClientId.Value = cObj.CLIENT_ID.ToString();

                bStatus = true;
            }
            return bStatus;

        }

        private void SetControl(FormDataMode dataMode)
        {

            bool isEnabled = (dataMode == FormDataMode.Add || dataMode == FormDataMode.Edit);

            txtCNNo.Attributes.Add("readonly", "readonly");
            txtSLADays.Attributes.Add("readonly", "readonly");
            txtRate.Attributes.Add("readonly", "readonly");
            txtAmountTk.Attributes.Add("readonly", "readonly");
            txtTotalAmount.Attributes.Add("readonly", "readonly");
            //SetTextBoxState(txtRoute, isEnabled);

            //txtCargoDate.Enabled = isEnabled;
            // buttons
            btnAddNew.Visible = !isEnabled;
            btnEdit.Visible =false;
            btnSave.Visible = isEnabled;
        }

        private void SetTextBoxState(TextBox txtBox, bool isEnabled)
        {
            if (isEnabled)
            {
                txtBox.Attributes.Remove("disabled");
                txtBox.CssClass = "form-control form-control-sm";
            }
            else
            {
                txtBox.Attributes["disabled"] = "disabled";
                txtBox.CssClass = "form-control form-control-sm";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            SaveTask();

        }

        private bool SaveTask()
        {

            if (!Page.IsValid)
            { return false; }


            if (CheckData())
            {

                bool bStatus = SaveData();

                if (bStatus)
                {
                    //ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Data Saved Successfully');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('success', 'Data Saved Successfully!', 'Success');", true);

                }
                else
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('error', ' Data not Saved!', 'Error');", true);
                }

            }
            else
            {

                //ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Error !! Data not Saved');", true);
                //this.SetPageMessage(errMsg, MessageTypeEnum.InvalidData);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('error', ' Data not Saved!', 'Error');", true);
            }

            return true;

        }



        private bool CheckData()
        {
            errMsg = string.Empty;

            if (hdnClientId.Value == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('error', 'Please Select Client!', 'Error');", true);
                txtClientName.Focus();
                return false;

            }

            if (hdnDeptID.Value == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('error', 'Select Departrment!', 'Error');", true);
                txtDepartment.Focus();
                return false;

            }

            if (hdnDistanceTypeId.Value == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('error', 'Select Distance Type!', 'Error');", true);
                txtDistanceType.Focus();
                return false;

            }

            if (hdnItemId.Value == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('error', 'Select Item!', 'Error');", true);
                txtItemName.Focus();
                return false;

            }

            if (txtQuantity.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('error', 'Enter Quantity!', 'Error');", true);
                txtQuantity.Focus();
                return false;

            }

            if (txtRate.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('error', 'Enter Rate!', 'Error');", true);
                txtRate.Focus();
                return false;

            }
            return true;

        }
        private void SetHyperLink()
        {


        }

        //newly added comment
        private bool SaveData()
        {

            bool bStatus = false;
            bool isAdd = false;
            int newCN_ID = 0;

            dcCN_CREATION_MST cObj = new dcCN_CREATION_MST();
            if (!(this.CN_ID > 0))
            {
                isAdd = true;
                cObj._RecordState = RecordStateEnum.Added;
            }
        
            if (cObj._RecordState == RecordStateEnum.Added)
            {
                cObj.CREATE_BY = loggedinUser.UserName;
                cObj.CREATE_DATE = DateTime.Now;
            }
            else
            {
                cObj.EDIT_BY = loggedinUser.UserName;
                cObj.EDIT_DATE = DateTime.Now;
            }

            cObj.CN_NUMBER = CN_CREATION_MSTBL.Get_New_CN_No(DateTime.Now.ToString("dd-MMM-yy"), null);
            cObj.CLIENT_ID = Conversion.StringToInt(hdnClientId.Value);
            cObj.AGR_DETAIL_ID = Conversion.StringToInt(hdnAggrementDtlId.Value);
            cObj.ITEM_ID = Conversion.StringToInt(hdnItemId.Value);
            cObj.ITEM_NAME = txtItemName.Text.Trim();
            cObj.SERVICE_CHARGE = Conversion.StringToDecimal(txtServiceCharge.Text);
            cObj.CLIENT_DEPT_ID = Conversion.StringToInt(hdnDeptID.Value);
            cObj.HUB_ID = Conversion.StringToInt(hdnHubId.Value);
            cObj.CONSIGNEE_NAME = txtRecipientName.Text.Trim();
            cObj.CONSIGNEE_ADDRESS = txtRecipientAddress.Text.Trim();
            cObj.CONSIGNEE_MOBILE_NO = txtRecipientMobileNo.Text.Trim();
            cObj.SLA_DAYS = Conversion.StringToDecimal(txtSLADays.Text);
            cObj.DISTANCE_TYPE_ID = Conversion.StringToInt(hdnDistanceTypeId.Value);
            cObj.WEIGHT = Conversion.StringToDecimal(txtWeight.Text);
            cObj.QTY = Conversion.StringToDecimal(txtQuantity.Text);
            cObj.RATE = Conversion.StringToDecimal(txtRate.Text);
            cObj.SERVICE_AMOUNT = Conversion.StringToDecimal(txtRate.Text);
            cObj.TAKA = Conversion.StringToDecimal(txtAmountTk.Text);
            cObj.TOTAL_AMT = Conversion.StringToDecimal(txtTotalAmount.Text);

            newCN_ID = CN_CREATION_MSTBL.Save(cObj);




            if (newCN_ID > 0)
            {
                this.CN_ID = newCN_ID;
                ReadTask();
                bStatus = true;
            }

            return bStatus;
        }



        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            AddTask();
        }




        #region Report

        protected void btnMRRPrint_Click(object sender, EventArgs e)
        {
            //int issueMasterId = Conversion.StringToInt(hdnCN_ID.Value);
            //if (issueMasterId > 0)
            //{
            //    //   string mrrNo = txtIssueNo.Text.Trim();

            //    ReportOptions rptOption = GetReportOptions();
            //    AppReport rpt = IGRReportRGN.GenerateIGRReport(issueMasterId, rptOption); //MaterialReceiveGenerateRGN.GenerateMaterialReceiveItemDetails(mrrNo, rptOption);
            //    string rk = AppReport.SetAppReportToSession(rpt, this.Context);

            //    ShowReport(rk);
            //}
            //else
            //{
            //    ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Please select IGR First. !!');", true);
            //}
        }


        private void ShowReport(string reportKey)
        {
            ReportOpenTypeEnum rptOpenType = this.ReportOpenType;
            ReportViewModeEnum rptViewMode = (ReportViewModeEnum)Convert.ToInt32(1);

            bool pdfView = true;

            string strWait = "true";
            string strIsPrint = "false";
            string strIsPDFAutoPrint = "false";
            string strPDFView = "false";


            switch (rptOpenType)
            {
                case ReportOpenTypeEnum.Preview:
                    //if (ddlReportFormat.SelectedValue == "1")
                    //{
                    //    strPDFView = "true";
                    //}

                    break;
                case ReportOpenTypeEnum.Print:
                    rptViewMode = ReportViewModeEnum.InThisTab;
                    strWait = "false";
                    strIsPrint = "true";
                    break;
                case ReportOpenTypeEnum.Export:
                    //rptViewMode = ReportViewModeEnum.InThisTab;
                    rptViewMode = ReportViewModeEnum.InNewWindow;
                    strWait = "false";
                    break;
            }

            bool isPDFAutoPrint = true;
            if (Request.Browser.Browser.ToLower().Contains("ie") == true)
            {
                // isPDFAutoPrint = !AccSettings.IsIERsClientPrint;
            }

            strIsPDFAutoPrint = isPDFAutoPrint ? "true" : "false";


            //string strTime = DateTime.Now.ToString("hhmm");
            string strTime = DateTime.Now.ToFileTime().ToString();
            //string strTime = DateTime.Now now.getTime().toString();
            string url = this.ReportViewPageLink + "?rk=" + reportKey + "&_tt=" + strTime;
            if (pdfView && rptOpenType == ReportOpenTypeEnum.Preview)
            {
                url = this.ReportViewPDFPageLink + "?rk=" + reportKey + "&_tt=" + strTime;
            }


            string jsScript = "$(function(){tbopen('" + reportKey + "', " + strPDFView + ", " + strIsPrint + "," + strIsPDFAutoPrint + "," + strWait + ");});";


            switch (rptViewMode)
            {
                case ReportViewModeEnum.InThisTab:
                    if (rptOpenType == ReportOpenTypeEnum.Print)
                    {
                        ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "showreport", jsScript, true);
                    }
                    else
                    {
                        Response.Redirect(url, false);
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

            //rptOption.ReportViewMode = (ReportViewModeEnum)Convert.ToInt32(ddlReportFormat.SelectedValue);
            //rptOption.ReportOpenType = this.ReportOpenType;

            //AppInfo.SetAppInfoToReportOptions(rptOption);
            //CompanyInfo.SetCompanyInfoToReportOptions(rptOption, this.Context);
            //rptOption.UserName = base.LoginUserName;


            return rptOption;
        }

        #endregion

    }

}