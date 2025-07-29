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

namespace PG.Web.WREL
{
    public partial class AgreementEntry : BagePage
    {
        //this 
        string ViewStateKey = "AGR_ID";
        string ViewStateKeyPrev = "AGR_ID_PREV";
        ReportOpenTypeEnum ReportOpenType = ReportOpenTypeEnum.Preview;
        // int CompanyID = 0;

        int AGR_ID = 0;
        string saveMsg = string.Empty;
        string errMsg = string.Empty;

        private  dcUser loggedinUser = null;
        public string ReportViewPageLink = PageLinks.ReportLinks.GetLink_ReportView;
        public string ReportViewPDFPageLink = PageLinks.ReportLinks.GetLink_ReportViewPDF;
        public string ReportPrintPageLink = PageLinks.ReportLinks.GetLink_ReportPrint;
        public string ReportPDFPageLink = PageLinks.ReportLinks.GetLink_ReportPDF;



        public string DistrictListServiceLink = PageLinks.InventoryLink.GetLink_DistrictList;
        public string TownListServiceLink = PageLinks.InventoryLink.GetLink_TownList;
        public string RouteListServiceLink = PageLinks.InventoryLink.GetLink_RouteList;
        public string ClientListServiceLink = PageLinks.InventoryLink.GetLink_ClientList;
        public string DepartmentListServiceLink = PageLinks.InventoryLink.GetLink_DepartmentList;
        public string ItemListServiceLink = PageLinks.InventoryLink.GetLink_ItemListCourier;
        public string DistanceTypeListServiceLink = PageLinks.InventoryLink.GetLink_DistanceTypeList;
        
        List<dcAGREEMENT_DETAILL> listDetails = new List<dcAGREEMENT_DETAILL>();

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
            //base.AppObjectID = BLLibrary.SystemBL.AppObjectEnum.Frm1001_OptionInfo;
            //base.RestrictByPageInTab();

            //ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(this.LinkButton1);

            this.AGR_ID = base.GetPageQueryInteger("id");

            if (!IsPostBack) //first Time
            {

              
                hdnLoggedInUser.Value = loggedinUser.UserID.ToString();
                //FillCombo();





                if (this.AGR_ID == 0) //not query string
                {
                    //List<dcCARGO_CREATION_DETAIL> roomList = HMRESERVATION_DTLBL.GetRoomInfoList();
                    //GridView1.DataSource = roomList;
                    //GridView1.DataBind();
                   
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
                this.AGR_ID = int.Parse(ViewState[ViewStateKey].ToString());
            }

            SetHyperLink();

          
            //this.ShowPageMessage(this.lblMessage);
            // Response.Write("UserID : " + this.UserID.ToString());

        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            EditTask();

            //List<dcCARGO_CREATION_DETAIL> roomList = HMRESERVATION_DTLBL.GetRoomInfoList();
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
            ReadData(this.AGR_ID);
            ViewState[ViewStateKey] = this.AGR_ID.ToString();

            SetControl(FormDataMode.Read);

        }
        private void AddTask()
        {
            ViewState[ViewStateKeyPrev] = this.AGR_ID.ToString();

            this.EditMode = FormDataMode.Add;
            this.IsDirty = false;
            this.AGR_ID = 0;
            ViewState[ViewStateKey] = "0";
            this.listDetails.Clear();
            CheckAndAddGridBlankRow();
            BindDataToGrid(this.listDetails);
           //add
            SetControl(FormDataMode.Add);
        }
        private void EditTask()
        {
            this.EditMode = FormDataMode.Edit;
            ReadData(this.AGR_ID);
            this.EditMode = FormDataMode.Edit;
            ViewState[ViewStateKey] = this.AGR_ID.ToString();
            SetControl(FormDataMode.Edit);
        }

        private bool ReadData(int id)
        {
            bool bStatus = false;

            dcAGREEMENT_MST cObj = AGREEMENT_MSTBL.GetAgreementMstInfoById(id);
            if (cObj != null)
            {

                hdnAGR_ID.Value = cObj.AGR_ID.ToString();
                txtAgreementName.Text = cObj.AGREEMENT_NAME;
                txtAgreementdt.Text = Convert.ToDateTime(cObj.AGREEMENT_DATE).ToString("dd-MMM-yyyy"); ;
                txtClientName.Text = cObj.CLIENT_NAME;
                hdnClientId.Value = cObj.CLIENT_ID.ToString();
                txtDepartment.Text = cObj.DEPT_NAME;
                hdnDepartmentId.Value = cObj.DEPT_ID.ToString();
                txtAgreementStDate.Text = Convert.ToDateTime(cObj.AGREEMENT_START_DATE).ToString("dd-MMM-yyyy");
                txtAgrEndDate.Text = Convert.ToDateTime(cObj.AGREEMENT_END_DATE).ToString("dd-MMM-yyyy");
                txtDescription.Text = cObj.DESCRIPTION;
                ddlStatus.SelectedValue = cObj.IS_ACTIVE;


                this.listDetails = AGREEMENT_DETAILLBL.GetAgreementInfoListById(cObj.AGR_ID);
                BindDataToGrid(listDetails);

                bStatus = true;
            }
            return bStatus;

        }

        private void SetControl(FormDataMode dataMode)
        {
            bool isEnabled = false;

            if (dataMode == FormDataMode.Add | dataMode == FormDataMode.Edit)
            {
                isEnabled = true;
            }

            txtAgreementName.Enabled = isEnabled;
            txtAgreementdt.Enabled = isEnabled;
            txtClientName.Enabled = isEnabled;
            txtDepartment.Enabled = isEnabled;
            txtAgreementStDate.Enabled = isEnabled;
            txtAgrEndDate.Enabled = isEnabled;
            txtDescription.Enabled = isEnabled;
            ddlStatus.Enabled = isEnabled;
           
            //buttons
            btnAddNew.Visible = !isEnabled;
            btnEdit.Visible = !isEnabled;
            btnSave.Visible = isEnabled;
            //btnUpdate.Visible = !isEnabled;

            SetControlGrid(dataMode);

        }


        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {

            switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:
                    e.Row.CssClass += " gridRow";
                    break;
                //case DataControlRowType.Header:
                //    e.Row.CssClass += " headerRow";
                //    break;
                //case DataControlRowType.Footer:
                //    e.Row.CssClass += " footerRow";
                //    break;
                //case DataControlRowType.Pager:
                //    e.Row.CssClass += " pagerRow";
                //    break;
                //case DataControlRowType.EmptyDataRow:
                //    e.Row.CssClass += " gridRow";
                //    break;
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string isOtpValue = "";

                object val = DataBinder.Eval(e.Row.DataItem, "IS_OTP_SERVICE");
                if (val != null && val != DBNull.Value)
                {
                    isOtpValue = val.ToString().ToUpper().Trim();
                }

                DropDownList ddlIsOTP = (DropDownList)e.Row.FindControl("ddlIsOTP");
                if (ddlIsOTP != null)
                {
                    // Only set SelectedValue if it exists in the items list
                    if (!string.IsNullOrEmpty(isOtpValue) && ddlIsOTP.Items.FindByValue(isOtpValue) != null)
                    {
                        ddlIsOTP.SelectedValue = isOtpValue;
                    }
                    else
                    {
                        ddlIsOTP.SelectedValue = "N"; // default to "No"
                    }
                }


                string rowID = e.Row.ClientID;
                string js = string.Format("return ShowDetailsPopup('{0}');", rowID);
               
            }


           
        }



        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int RowIndex = gvr.RowIndex;
                GridView1.Rows[RowIndex].Visible = false;

            }

            
        }

       

        private void BindDataToGrid(List<dcAGREEMENT_DETAILL> listData)
        {
            int rowCount = listData.Count;
            if (rowCount == 0)
            {
                listData.Add(new dcAGREEMENT_DETAILL());
            }

            GridView1.DataSource = listData.ToList();
            GridView1.DataBind();
            //GridView1.CssClass = "grid";

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
                    ((TextBox)gvR.FindControl("txtItemName")).Enabled = isEnabled;
                    ((TextBox)gvR.FindControl("txtDistanceType")).Enabled = isEnabled;
                    ((DropDownList)gvR.FindControl("ddlIsOTP")).Enabled = isEnabled;
                    ((TextBox)gvR.FindControl("txtServiceAmt")).Enabled = isEnabled;
                    ((TextBox)gvR.FindControl("txtReturnPrice")).Enabled = isEnabled;
                    ((TextBox)gvR.FindControl("txtSLA_DAYS")).Enabled = isEnabled;
                    ((TextBox)gvR.FindControl("txtREMARKS")).Enabled = isEnabled;
                    
                }
            }

        }

        private void ReadDetailsFromGrid()
        {

            //int locationID = Convert.ToInt32(hdnLocationID.Value);
            this.listDetails.Clear();

            ///addition
            foreach (GridViewRow gvR in this.GridView1.Rows)
            {
                if (gvR.RowType == DataControlRowType.DataRow)
                {

                    dcAGREEMENT_DETAILL cObj = new dcAGREEMENT_DETAILL();
                    ReadGridRowToObject(gvR, this.GridView1.DataKeys, cObj);

                    if (cObj._RecordState == RecordStateEnum.Deleted)
                    {
                        if (cObj.AGR_DETAIL_ID > 0)
                        {
                            this.listDetails.Add(cObj);
                        }
                    }
                    else
                    {
                        
                        this.listDetails.Add(cObj);
                    }

                }
            }
        }

        private void ReadGridRowToObject(GridViewRow gvR, DataKeyArray dataKeys, dcAGREEMENT_DETAILL cObj)
        {
            decimal d;
            string strD;

            strD = ((HiddenField)gvR.FindControl("hdnAGR_DETAIL_ID")).Value;
            cObj.AGR_DETAIL_ID = Conversion.StringToInt(strD);

            strD = ((HiddenField)gvR.FindControl("hdnItemID")).Value;
            cObj.ITEM_ID = Conversion.StringToInt(strD);
            strD = ((TextBox)gvR.FindControl("txtItemName")).Text;
            cObj.ITEM_NAME = strD;
            strD = ((HiddenField)gvR.FindControl("hdnDistanceTypeID")).Value;
            cObj.DISTANCE_TYPE_ID = Conversion.StringToInt(strD);
            strD = ((TextBox)gvR.FindControl("txtDistanceType")).Text;
            cObj.TYPE_NAME = strD;
            strD = ((DropDownList)gvR.FindControl("ddlIsOTP")).SelectedValue;
            cObj.IS_OTP_SERVICE = strD;

            strD = ((TextBox)gvR.FindControl("txtServiceAmt")).Text;
            cObj.SERVICE_AMOUNT =Conversion.StringToDecimal (strD);

            strD = ((TextBox)gvR.FindControl("txtReturnPrice")).Text;
            cObj.RETURN_SERVICE_AMOUNT = Conversion.StringToDecimal(strD);
            strD = ((TextBox)gvR.FindControl("txtSLA_DAYS")).Text;
            cObj.SLA_DAYS = Conversion.StringToInt(strD);
            
            strD = ((TextBox)gvR.FindControl("txtREMARKS")).Text;
            cObj.REMARKS = strD;

            
                

                

            if (cObj.AGR_DETAIL_ID > 0)
            {
                cObj._RecordState = RecordStateEnum.Edited;
            }
            else
            {
                cObj._RecordState = RecordStateEnum.Added;
            }

            
            


            if (!gvR.Visible)
            {
                cObj._RecordState = RecordStateEnum.Deleted;
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
                    ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Data Saved Successfully');", true);
                   
                }
                else
                {
                    
                    ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Error !! Data not Saved');", true);
                }

            }
            else
            {

                ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Error !! Data not Saved');", true);
                this.SetPageMessage(errMsg, MessageTypeEnum.InvalidData);
            }

            return true;

        }

        private bool ValidateDetails(List<dcAGREEMENT_DETAILL> list)
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

            if (txtAgreementName.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Please Enter Agreement Name !!');", true);
                txtAgreementName.Focus();
                return false;

            }

            if (txtAgreementdt.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Please Enter Agreement Date !!');", true);
                txtAgreementdt.Focus();
                return false;

            }

            if (txtClientName.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Please Enter Client !!');", true);
                txtClientName.Focus();
                return false;

            }

            if (hdnClientId.Value == "0")
            {
                ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Please Enter Client !!');", true);
                txtClientName.Focus();
                return false;

            }

            ReadDetailsFromGrid();

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
            int newAGR_ID = 0;
            dcAGREEMENT_MST agreementMst = new dcAGREEMENT_MST();
            if (this.AGR_ID > 0)
            {

                agreementMst.AGR_ID = this.AGR_ID;
                agreementMst._RecordState = RecordStateEnum.Edited;
            }
            else
            {
                agreementMst._RecordState = RecordStateEnum.Added;
                isAdd = true;
            }

            agreementMst.CLIENT_ID = Conversion.StringToInt(hdnClientId.Value);
            agreementMst.DEPT_ID = Conversion.StringToInt(hdnDepartmentId.Value);
            agreementMst.AGREEMENT_NAME = txtAgreementName.Text.Trim();
            agreementMst.AGREEMENT_DATE = Conversion.StringToDate(txtAgreementStDate.Text.Trim());
            agreementMst.AGREEMENT_START_DATE = Conversion.StringToDate(txtAgreementStDate.Text.Trim());
            agreementMst.AGREEMENT_END_DATE = Conversion.StringToDate(txtAgrEndDate.Text.Trim());
            agreementMst.DESCRIPTION = txtDescription.Text.Trim();
            agreementMst.IS_ACTIVE = ddlStatus.SelectedValue;


             
                if(isAdd)
                {
                    agreementMst.CREATE_BY = loggedinUser.UserName;
                    agreementMst.CREATE_DATE = DateTime.Now;
                   
                }
                else
                {
                    agreementMst.EDIT_BY = loggedinUser.UserName;
                    agreementMst.EDIT_DATE = DateTime.Now;

                }



                agreementMst.agreementDetails = this.listDetails;

                newAGR_ID = AGREEMENT_MSTBL.Save(agreementMst);
                if (newAGR_ID > 0)
                {
                    this.AGR_ID = newAGR_ID;
                    ReadTask();
                    bStatus = true;
                    ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Data saved successfully !!');", true);
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
            //int issueMasterId = Conversion.StringToInt(hdnCARGO_ID.Value);
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


        protected void btnNewRow_Click(object sender, EventArgs e)
        {
            ReadDetailsFromGrid();
            AddBlankRowToGridList();

            BindDataToGrid(this.listDetails);
            SetControlGrid(FormDataMode.Add);

        }

        private void CheckAndAddGridBlankRow()
        {

            int RowCheck = 1;
            int cntAdd = listDetails.Count();

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

        }

        private void AddBlankRowToGridList()
        {
            dcAGREEMENT_DETAILL cObj = new dcAGREEMENT_DETAILL();
            cObj._RecordState = RecordStateEnum.Added;
            this.listDetails.Add(cObj);
        }

        protected void GridView1_RowCreated1(object sender, GridViewRowEventArgs e)
        {

        }
    }
}