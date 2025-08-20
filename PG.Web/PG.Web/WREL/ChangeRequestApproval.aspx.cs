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
using System.IO;
using System.Data.OleDb;
using System.Data;
using OfficeOpenXml;
using System.ComponentModel;

namespace PG.Web.WREL
{
    public partial class ChangeRequestApproval : BagePage
    {
        //this 
        string ViewStateKey = "REQUEST_ID";
        string ViewStateKeyPrev = "REQUEST_ID_PREV";
        ReportOpenTypeEnum ReportOpenType = ReportOpenTypeEnum.Preview;
        // int CompanyID = 0;

        int REQUEST_ID = 0;
        private int totalRowCount = 0;
        string saveMsg = string.Empty;
        string errMsg = string.Empty;

        private dcUser loggedinUser = null;
        public string ReportViewPageLink = PageLinks.ReportLinks.GetLink_ReportView;
        public string ReportViewPDFPageLink = PageLinks.ReportLinks.GetLink_ReportViewPDF;
        public string ReportPrintPageLink = PageLinks.ReportLinks.GetLink_ReportPrint;
        public string ReportPDFPageLink = PageLinks.ReportLinks.GetLink_ReportPDF;

   
        public string CNListServiceLink = PageLinks.InventoryLink.GetLink_CNMasterList;
        public string AgentListServiceLink = PageLinks.InventoryLink.GetLink_AgentList;

        List<dcCN_INFO_CHANGE_REQUEST> listDetails = new List<dcCN_INFO_CHANGE_REQUEST>();

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

            this.REQUEST_ID = base.GetPageQueryInteger("id");

            if (!IsPostBack) //first Time
            {


                hdnLoggedInUser.Value = loggedinUser.UserID.ToString();
                hdnClientId.Value = loggedinUser.CLIENT_ID.ToString();
                //FillCombo();

                if (this.REQUEST_ID == 0) //not query string
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
                this.REQUEST_ID = int.Parse(ViewState[ViewStateKey].ToString());
            }

            SetHyperLink();


        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {


        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            EditTask();
         
        }

        public void FillCombo()
        {


        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
        }

        private void SetDate()
        {


        }

        private void ReadTask()
        {
            this.EditMode = FormDataMode.Read;
            ReadData(this.REQUEST_ID);
            ViewState[ViewStateKey] = this.REQUEST_ID.ToString();

            SetControl(FormDataMode.Read);

        }
        private void AddTask()
        {
            ViewState[ViewStateKeyPrev] = this.REQUEST_ID.ToString();

            this.EditMode = FormDataMode.Add;
            this.IsDirty = false;
            this.REQUEST_ID = 0;
            ViewState[ViewStateKey] = "0";
            this.listDetails.Clear();
          
            //add
            SetControl(FormDataMode.Add);
        }
        private void EditTask()
        {
            this.EditMode = FormDataMode.Edit;
            ReadData(this.REQUEST_ID);
            this.EditMode = FormDataMode.Edit;
            ViewState[ViewStateKey] = this.REQUEST_ID.ToString();
            SetControl(FormDataMode.Edit);
        }

        private bool ReadData(int id)
        {
            bool bStatus = false;

            dcCN_INFO_CHANGE_REQUEST cObj = CN_INFO_CHANGE_REQUESTBL.GetRequestInfoById(id, null);
            if (cObj != null)
            {

                hdnCnId.Value = cObj.CN_ID.ToString();
                hdnClientId.Value = cObj.CLIENT_ID.ToString();
                txtCnNumber.Text = cObj.CN_NUMBER;
                txtClientName.Text = cObj.CLIENT_NAME;
                txtConsigneeName.Text = cObj.CURRENT_CONSIGNEE_NAME;
                txtConsigneeMobileNo.Text = cObj.CURRENT_MOBILE_NO;
                txtConsigneeAddress.Text = cObj.CURRENT_CONSIGNEE_ADDRESS;
                txtNewAddress.Text = cObj.NEW_CONSIGNEE_ADDRESS;
                if(cObj.APPROVED_STATUS == "Y")
                {
                    btnSave.Visible = false;
                    btnSave.Enabled = false;
                    btnSave.CssClass += " disabled";
                }
                else
                {
                    btnSave.Visible = true;
                    btnSave.Enabled = true;
                    btnSave.CssClass = btnSave.CssClass.Replace("disabled", "").Trim();
                }
              
                bStatus = true;
            }
            return bStatus;

        }

        private void SetControl(FormDataMode dataMode)
        {
           

            bool isEnabled = (dataMode == FormDataMode.Add || dataMode == FormDataMode.Edit);

            txtCnNumber.Attributes.Add("readonly", "readonly");
            txtClientName.Attributes.Add("readonly", "readonly");
            txtConsigneeName.Attributes.Add("readonly", "readonly");
            txtConsigneeMobileNo.Attributes.Add("readonly", "readonly");
            txtConsigneeAddress.Attributes.Add("readonly", "readonly");
            txtNewAddress.Attributes.Add("readonly", "readonly");
          
            //btnSave.Visible = isEnabled;

         

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

        private bool ValidateDetails(List<dcCN_INFO_CHANGE_REQUEST> list)
        {
            bool y = true;
            foreach (var item in list)
            {
                //if(!(item.ROOM_QTY > 0))
                //{
                //    ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Please Select atleast one Room !!');", true);
                //    y = false;

                //}

            }

            return y;
        }

        private bool CheckData()
        {
            errMsg = string.Empty;

            if (txtNewAddress.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('error', 'Enter New Address!', 'Error');", true);
                txtNewAddress.Focus();
                return false;

            }

            //if (txtCargoDate.Text == "")
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('error', 'Enter Cargo Date!', 'Error');", true);
            //    txtCargoDate.Focus();
            //    return false;

            //}

            //if (txtName.Text == "")
            //{
            //    ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Please Enter Name !!');", true);
            //    txtName.Focus();
            //    return false;

            //}

            //if (txtAddress.Text == "")
            //{
            //    ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Please Enter Address !!');", true);
            //    txtAddress.Focus();
            //    return false;

            //}

            //if (txtMobileNo.Text == "")
            //{
            //    ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Please Enter Mobile No !!');", true);
            //    txtMobileNo.Focus();
            //    return false;

            //}

            //if (hdnCountryId.Value == "0")
            //{
            //    ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Please Select Country !!');", true);
            //    txtCountry.Focus();
            //    return false;

            //}

      

            if (ValidateDetails(this.listDetails))
            {
                return true;
            }
            else
            {


                ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('" + errMsg + "');", true);
                this.SetPageMessage(errMsg, MessageTypeEnum.InvalidData);
                return false;
            }

        }
        private void SetHyperLink()
        {

            //new button
            //string hLink = "javascript:tbopenSalInfo("+ this.AccYearID.ToString() +")";
            //if (base.PageMode == PG.Core.Web.PageModeEnum.InTab)
            //{
            //    hLink = "javascript:tbopenSalInfo(" + this.AccYearID.ToString() + ")";
            //    //this.btnSalaryInfo = string.Empty;
            //    this.btnSalaryInfo.Attributes.Add("onclick", hLink);
            //}
            //else
            //{
            //    hLink = "~/Master/EmpSalaryInfo.aspx?eid=" + this.AccYearID.ToString();
            //    //this.btnAddNew.PostBackUrl = hLink;
            //    //this.btnAddNew.OnClientClick = string.Empty;
            //    this.btnSalaryInfo.Attributes.Add("onclick", hLink);
            //}

        }

        //newly added comment
        private bool SaveData()
        {

            bool bStatus = false;
            bool isAdd = false;
            int newREQUEST_ID = 0;
    
          
            dcCN_INFO_CHANGE_REQUEST cObj = new dcCN_INFO_CHANGE_REQUEST();
            if (this.REQUEST_ID > 0)
            {
                dcCN_INFO_CHANGE_REQUEST obj = CN_INFO_CHANGE_REQUESTBL.GetRequestInfoById(this.REQUEST_ID, null);
                if(cObj !=null)
                {
                    dcCN_CREATION_MST objCN = new dcCN_CREATION_MST();
                    objCN.CN_ID = obj.CN_ID;
                    objCN.CONSIGNEE_ADDRESS = obj.NEW_CONSIGNEE_ADDRESS.Trim();
                    objCN.UPDATE_DATE_REQUEST=DateTime.Now;
                    objCN.UPDATE_BY_REQUEST=loggedinUser.UserID.ToString();
                    bStatus = CN_CREATION_MSTBL.Update(objCN);

                }
                if(bStatus)
                {
                    cObj.REQUEST_ID = this.REQUEST_ID;
                    cObj._RecordState = RecordStateEnum.Edited;
                    cObj.APPROVED_BY = loggedinUser.UserID.ToString();
                    cObj.APPROVED_DATE = DateTime.Now;
                    cObj.APPROVED_STATUS = "Y";

                    bStatus = CN_INFO_CHANGE_REQUESTBL.Update(cObj);

                }
            
             
            }
            if(bStatus)
            {
                this.REQUEST_ID = cObj.REQUEST_ID;
                ReadTask();
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
            //int issueMasterId = Conversion.StringToInt(hdnREQUEST_ID.Value);
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

        protected void btnLoad_Click(object sender, EventArgs e)
        {

        }


     
    }
}