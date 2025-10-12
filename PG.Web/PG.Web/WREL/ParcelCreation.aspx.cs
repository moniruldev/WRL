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
using PG.Report.ReportGen.WRELRGN;

namespace PG.Web.WREL
{
    public partial class ParcelCreation : BagePage
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
        
        

        List<dcCN_CREATION_MST> listDetails = new List<dcCN_CREATION_MST>();
        List<dcTEMP_CN_INFO> CNTEMPlistDetails = new List<dcTEMP_CN_INFO>();
        
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

            this.CN_ID = base.GetPageQueryInteger("id");

            if (!IsPostBack) //first Time
            {


                hdnLoggedInUser.Value = loggedinUser.UserID.ToString();
                //FillCombo();


                hdnHubId.Value = "1";
                txtHubName.Text = "Dhaka Central Hub";

                if (this.CN_ID == 0) //not query string
                {
                    //List<dcCN_CREATION_MST> roomList = HMRESERVATION_DTLBL.GetRoomInfoList();
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
                this.CN_ID = int.Parse(ViewState[ViewStateKey].ToString());
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
            this.listDetails.Clear();
            CheckAndAddGridBlankRow();
            BindDataToGrid(this.listDetails);
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

           List<dcCN_CREATION_MST> cObjList = CN_CREATION_MSTBL.GetCNInfoListById(id,null);
           if (cObjList.Any())
           {
               var obj = cObjList.First();

               hdnClientId.Value = obj.CLIENT_ID.ToString();
               txtClientName.Text = obj.CLIENT_NAME.ToString();
               txtHubName.Text = obj.HUB_NAME;
               hdnDeptID.Value = obj.CLIENT_DEPT_ID.ToString();
               txtDepartment.Text = obj.DEPT_NAME;
               hdnHubId.Value = obj.HUB_ID.ToString();

               BindDataToGrid(cObjList);
               bStatus = true;
           }
            return bStatus;

        }

        private void SetControl(FormDataMode dataMode)
        {
            //bool isEnabled = false;

            //if (dataMode == FormDataMode.Add | dataMode == FormDataMode.Edit)
            //{
            //    isEnabled = true;
            //}

          //  txtClientName.Enabled = isEnabled;
          //txtDepartment.Enabled = isEnabled;
          //txtHubName.Enabled= isEnabled;
            //txtRemarks.Enabled = isEnabled;
            //txtStartingDist.Enabled = isEnabled;
            //txtDestinationDist.Enabled = isEnabled;
            //txtDestinationTown.Enabled = isEnabled;
            //txtManagerName.Enabled = isEnabled;
            ////buttons
            //btnAddNew.Visible = !isEnabled;
            //btnEdit.Visible = !isEnabled;
            //btnSave.Visible = isEnabled;
            ////btnUpdate.Visible = !isEnabled;

            //SetControlGrid(dataMode);

            bool isEnabled = (dataMode == FormDataMode.Add || dataMode == FormDataMode.Edit);

            // Apply disabled/enabled logic without losing Bootstrap styling
            //SetTextBoxState(txtCNNo, false);
            SetTextBoxState(txtClientName, isEnabled);
            SetTextBoxState(txtDepartment, isEnabled);
            SetTextBoxState(txtHubName, isEnabled);
            //SetTextBoxState(txtStartingDist, isEnabled);
            //SetTextBoxState(txtDestinationDist, isEnabled);
            //SetTextBoxState(txtDestinationTown, isEnabled);
            //SetTextBoxState(txtManagerName, isEnabled);
            //SetTextBoxState(txtRoute, isEnabled);

            //txtCargoDate.Enabled = isEnabled;
            // buttons
            btnAddNew.Visible = !isEnabled;
            btnEdit.Visible = !isEnabled;
            btnSave.Visible = isEnabled;

            SetControlGrid(dataMode);

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
                int currentRowIndex = e.Row.RowIndex;
                int serialNo = currentRowIndex +1;

                Label lblSerial = (Label)e.Row.FindControl("lblSerialNo");
                if (lblSerial != null)
                {
                    lblSerial.Text = serialNo.ToString();
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
                RefreshGrid();

            }

            //if (e.CommandName == "roomdetials")
            //{
            //    int roomTypeId = Convert.ToInt32(e.CommandArgument);
            //    DisplayRoomDetails(roomTypeId);


            //}
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
                        ((Label)gvR.FindControl("lblSerialNo")).Text = slNo.ToString();
                    }
                }
            }
        }

        private void BindDataToGrid(List<dcCN_CREATION_MST> listData)
        {
            int rowCount = listData.Count;
            this.totalRowCount = listData.Count;
            if (rowCount == 0)
            {
                listData.Add(new dcCN_CREATION_MST());
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

                    //((TextBox)gvR.FindControl("txtPICKUP_DATE")).Attributes.Add("readonly", "readonly");
                    ((TextBox)gvR.FindControl("txtRecipientName")).Attributes.Add("readonly", "readonly");
                    ((TextBox)gvR.FindControl("txtSERVICE_CHARGE")).Attributes.Add("readonly", "readonly");
                    ((TextBox)gvR.FindControl("txtRecipientAddress")).Attributes.Add("readonly", "readonly");
                    ((TextBox)gvR.FindControl("txtRecipientMobileNo")).Attributes.Add("readonly", "readonly");
                    ((TextBox)gvR.FindControl("txtITEM_NAME")).Attributes.Add("readonly", "readonly");
                    ((TextBox)gvR.FindControl("txtPRODUCT_TYPE")).Attributes.Add("readonly", "readonly");

                    ((TextBox)gvR.FindControl("txtTaka")).Attributes.Add("readonly", "readonly");
                    //((TextBox)gvR.FindControl("txtDestinationDist")).Attributes.Add("readonly", "readonly");
                    ((TextBox)gvR.FindControl("txtSLA_DAYS")).Attributes.Add("readonly", "readonly");
                    ((TextBox)gvR.FindControl("txtTotalAmount")).Attributes.Add("readonly", "readonly");
                    ((TextBox)gvR.FindControl("txtRATE")).Attributes.Add("readonly", "readonly");
                    ((TextBox)gvR.FindControl("txtQTY")).Attributes.Add("readonly", "readonly");
                    ((TextBox)gvR.FindControl("txtWEIGHT")).Attributes.Add("readonly", "readonly");
                    
                    LinkButton lnkDelete = (LinkButton)gvR.FindControl("btnDeleteRow");
                    lnkDelete.Enabled = isEnabled;
                    if (!isEnabled)
                    {
                        lnkDelete.OnClientClick = "";
                    }

                }
            }

            btnNewRow.Enabled = isEnabled;

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

                    dcCN_CREATION_MST cObj = new dcCN_CREATION_MST();
                    ReadGridRowToObject(gvR, this.GridView1.DataKeys, cObj);

                    if (cObj._RecordState == RecordStateEnum.Deleted)
                    {
                        if (cObj.CN_ID > 0)
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

        private void ReadGridRowToObject(GridViewRow gvR, DataKeyArray dataKeys, dcCN_CREATION_MST cObj)
        {
            decimal d;
            string strD;

          
            strD = ((HiddenField)gvR.FindControl("hdnCNId")).Value;
            cObj.CN_ID = Conversion.StringToInt(strD);
            if (cObj.CN_ID > 0)
            {
                cObj._RecordState = RecordStateEnum.Edited;
            }
            else
            {
                cObj._RecordState = RecordStateEnum.Added;
            }

            strD = ((HiddenField)gvR.FindControl("hdnAgreementDTLID")).Value;
            cObj.AGR_DETAIL_ID = Conversion.StringToInt(strD);
            //strD = ((TextBox)gvR.FindControl("txtPICKUP_DATE")).Text;
            //cObj.PICKUP_DATE =Conversion.StringToDate(strD);
            strD = ((Label)gvR.FindControl("txtCNNumber")).Text;
            cObj.CN_NUMBER = strD;
            strD = ((TextBox)gvR.FindControl("txtRecipientName")).Text;
            cObj.CONSIGNEE_NAME = strD;
            strD = ((TextBox)gvR.FindControl("txtRecipientAddress")).Text;
            cObj.CONSIGNEE_ADDRESS = strD;
            strD = ((TextBox)gvR.FindControl("txtRecipientMobileNo")).Text;
            cObj.CONSIGNEE_MOBILE_NO = strD;
            strD = ((TextBox)gvR.FindControl("txtITEM_NAME")).Text;
            cObj.ITEM_NAME = strD;
            strD = ((HiddenField)gvR.FindControl("hdnITEM_ID")).Value;
            cObj.ITEM_ID =Conversion.StringToInt(strD);
            //strD = ((TextBox)gvR.FindControl("txtPRODUCT_TYPE")).Text;
            //cObj.PRODUCT_TYPE = strD;
            //strD = ((TextBox)gvR.FindControl("txtUPS")).Text;
            //cObj.UPS = strD;
            //strD = ((TextBox)gvR.FindControl("txtDestinationDist")).Text;
            //cObj.DESTINATION = strD;
            strD = ((TextBox)gvR.FindControl("txtSLA_DAYS")).Text;
            cObj.SLA_DAYS =Conversion.StringToInt(strD);
            strD = ((TextBox)gvR.FindControl("txtWEIGHT")).Text;
            cObj.WEIGHT = Conversion.StringToDecimal(strD);
            strD = ((TextBox)gvR.FindControl("txtQTY")).Text;
            cObj.QTY = Conversion.StringToDecimal(strD);
            strD = ((TextBox)gvR.FindControl("txtTaka")).Text;
            cObj.TAKA = Conversion.StringToDecimal(strD);
           
             strD = ((TextBox)gvR.FindControl("txtSERVICE_CHARGE")).Text;
            cObj.SERVICE_CHARGE = Conversion.StringToDecimal(strD);
            strD = ((TextBox)gvR.FindControl("txtTotalAmount")).Text;
            cObj.TOTAL_AMT = Conversion.StringToDecimal(strD);
            
                
                
            strD = ((HiddenField)gvR.FindControl("hdnServiceAmount")).Value;
            cObj.SERVICE_AMOUNT = Conversion.StringToDecimal(strD);
            strD = ((HiddenField)gvR.FindControl("hdnDISTANCE_TYPE_ID")).Value;
            cObj.DISTANCE_TYPE_ID = Conversion.StringToInt(strD);
            


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

        private bool ValidateDetails(List<dcCN_CREATION_MST> list)
        {
            bool y = true;
            int i = 0;
            foreach (var item in list)
            {
                i = i + 1;
                if (item.ITEM_ID == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Please Enter Proper ITem Name Row SL# " + i + "!!');", true);
                    y = false;

                }
                if(item.DISTANCE_TYPE_ID==0)
                {
                    ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Please Enter Proper Distance Type Row SL# " + i + "!!');", true);
                    y = false;
                }

            }

            return y;
        }

        private bool CheckData()
        {
            errMsg = string.Empty;

            if (txtClientName.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('error', 'Enter Client Name!', 'Error');", true);
                txtClientName.Focus();
                return false;

            }

            if (txtDepartment.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('error', 'Enter Client Department!', 'Error');", true);
                txtDepartment.Focus();
                return false;

            }

            

            ReadDetailsFromGrid();
            listDetails = Session["CNList"] as List<dcCN_CREATION_MST>;
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

           
        }

        //newly added comment
        private bool SaveData()
        {

            bool bStatus = false;
            bool isAdd = false;
            int newCN_ID = 0;
           // ReadDetailsFromGrid();
            listDetails = Session["CNList"] as List<dcCN_CREATION_MST>;
            foreach(dcCN_CREATION_MST obj in this.listDetails)
            {
                dcCN_CREATION_MST cObj = new dcCN_CREATION_MST();
                cObj._RecordState = RecordStateEnum.Added;
                if(cObj._RecordState == RecordStateEnum.Added)
                {
                    cObj.CREATE_BY = loggedinUser.UserID.ToString();
                    cObj.CREATE_DATE = DateTime.Now;
                }
                else
                {
                    cObj.EDIT_BY = loggedinUser.UserID.ToString();
                    cObj.EDIT_DATE = DateTime.Now;
                }

                cObj.CN_NUMBER =CN_CREATION_MSTBL.Get_New_CN_No(DateTime.Now.ToString("dd-MMM-yy"), null);
                cObj.CLIENT_ID = Conversion.StringToInt(hdnClientId.Value);
                //cObj.CLIENT_DEPT_ID = Conversion.StringToInt(hdnDeptID.Value);
                cObj.AGR_DETAIL_ID = obj.AGR_DETAIL_ID;
                cObj.PICKUP_DATE = obj.PICKUP_DATE;
                cObj.CN_CLIENT_CODE = obj.CN_CLIENT_CODE;
                cObj.AGR_DETAIL_ID = Conversion.StringToInt(hdnAggrementDtlId.Value);
                cObj.ITEM_ID = obj.ITEM_ID;
                cObj.ITEM_NAME = obj.ITEM_NAME;
                cObj.SERVICE_AMOUNT = obj.SERVICE_AMOUNT;
                cObj.CLIENT_DEPT_ID = Conversion.StringToInt(hdnDeptID.Value);
                cObj.HUB_ID = Conversion.StringToInt(hdnHubId.Value);

                cObj.CONSIGNEE_NAME = obj.CONSIGNEE_NAME;
                cObj.CONSIGNEE_ADDRESS = obj.CONSIGNEE_ADDRESS;
                cObj.CONSIGNEE_MOBILE_NO = obj.CONSIGNEE_MOBILE_NO;
                //cObj.DESTINATION = obj.DESTINATION;
                //cObj.DESTINATION_DIST_ID = obj.DESTINATION_DIST_ID;
                //cObj.PRODUCT_TYPE = obj.PRODUCT_TYPE;
                //cObj.UPS = obj.UPS;
                //cObj.DESTINATION = obj.DESTINATION;
                cObj.SLA_DAYS = obj.SLA_DAYS;
                //cObj.NARRATION = obj.NARRATION;
                //cObj.STATUS = obj.STATUS;
                //cObj.REF_TYPE = obj.REF_TYPE;
                cObj.DISTANCE_TYPE_ID = obj.DISTANCE_TYPE_ID;
                cObj.WEIGHT = obj.WEIGHT;
                cObj.QTY = obj.QTY;
                cObj.RATE = obj.RATE;
                cObj.TAKA = obj.TAKA;
                cObj.SERVICE_CHARGE = obj.SERVICE_CHARGE;

                newCN_ID = CN_CREATION_MSTBL.Save(cObj);
            }
   

          
            if (newCN_ID > 0)
            {
                this.CN_ID = newCN_ID;
                //ReadTask();
                SetControl(FormDataMode.Read);
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


        protected void btnNewRow_Click(object sender, EventArgs e)
        {
            ReadDetailsFromGrid();
            AddBlankRowToGridList();

            BindDataToGrid(this.listDetails);
            // to focusing the row
            if (GridView1.Rows.Count > 0)
            {
                int lastRowIndex = GridView1.Rows.Count - 1;
                TextBox txtLastCN = GridView1.Rows[lastRowIndex].FindControl("txtRecipientName") as TextBox;
                if (txtLastCN != null)
                {
                    ScriptManager.GetCurrent(this).SetFocus(txtLastCN);
                }
            }

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
            dcCN_CREATION_MST cObj = new dcCN_CREATION_MST();
            cObj._RecordState = RecordStateEnum.Added;
            this.listDetails.Add(cObj);
            //this.listDetails.Insert(0, cObj); // this is for row top order
        }

        protected void txtRecipientName_TextChanged(object sender, EventArgs e)
        {
            //dcCN_CREATION_MST cnInfo = CN_CREATION_MSTBL.GetCNInfoByCNNumber(txtRecipientName.text);
            btnNewRow_Click(sender, e);
            //SetFocusToLasttxtRecipientName();
        }
        private void SetFocusToLasttxtRecipientName()
        {
            int lastIndex = GridView1.Rows.Count - 1;
            if (lastIndex >= 0)
            {
                TextBox lastTxtCN = GridView1.Rows[0].FindControl("txtRecipientName") as TextBox;
                if (lastTxtCN != null)
                {
                    ScriptManager.GetCurrent(this).SetFocus(lastTxtCN);
                }
            }
        }

        protected void btnDownloadSample_Click(object sender, EventArgs e)
        {
              string fileName = "sample_parcel_import.xlsx"; // Name of the file to download
            string filePath = Server.MapPath("~/DownloadSample/" + fileName);

            if (File.Exists(filePath))
            {
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", string.Format("attachment; filename={0}", fileName));
                Response.TransmitFile(filePath);
                Response.End();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('error', ' File not found!', 'Error');", true);
            }
        }

        #region paste Excel
        protected void btnPasteData_Click(object sender, EventArgs e)
        {
            finddataPaste();
        }

        private void finddataPaste()
        {
            //string rundate = BaseContent.GetCompanyDate().ToString("dd-MMM-yyyy");
            if (DrPasteTextBox.Text != "")
            {
                try
                {
                    DataTable dtCh = new DataTable("dtCh");
                    dtCh.Columns.Add("xCONSIGNEE_NAME", typeof(string));
                    dtCh.Columns.Add("xCONSIGNEE_ADDRESS", typeof(string));
                    dtCh.Columns.Add("xCONSIGNEE_MOBILE_NO", typeof(string));
                    dtCh.Columns.Add("xDESTINATION_DIST_NAME", typeof(string));
                    dtCh.Columns.Add("xDESTINATION_TOWN_NAME", typeof(string));
                    


                    string copiedContent = Request.Form[DrPasteTextBox.UniqueID];
                    foreach (string row in copiedContent.Split('\n'))
                    {
                        if (!string.IsNullOrEmpty(row))
                        {
                            dtCh.Rows.Add();
                            int i = 0;
                            foreach (string cell in row.Split('\t'))
                            {
                                dtCh.Rows[dtCh.Rows.Count - 1][i] = cell;
                                i++;
                            }
                        }
                    }
                    DrPasteTextBox.Text = "";


                    LoadGridDr(dtCh);


                }
                 catch (Exception ex)
                {
                    //messageLabel2.Text = ex.ToString();
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(btnSave, this.Page.GetType(), "Message", string.Format("alert(\"Please select an Excel File{0}.\");", ""), true);
            }
        }

        private void LoadGridDr(DataTable dt)
        {
            DataTable dtDebitTrans = new DataTable();
            dtDebitTrans.Columns.Add("SlNo", typeof(int));
            dtDebitTrans.Columns.Add("CN_ID", typeof(int));
            dtDebitTrans.Columns.Add("CN_NUMBER", typeof(string));
            dtDebitTrans.Columns.Add("CONSIGNEE_NAME", typeof(string));
            dtDebitTrans.Columns.Add("CONSIGNEE_ADDRESS", typeof(string));
            dtDebitTrans.Columns.Add("CONSIGNEE_MOBILE_NO", typeof(string));
            dtDebitTrans.Columns.Add("DESTINATION_DIST_NAME", typeof(string));
            dtDebitTrans.Columns.Add("DESTINATION_TOWN_NAME", typeof(string));
            dtDebitTrans.Columns.Add("DESTINATION_DIST_ID", typeof(int));
            dtDebitTrans.Columns.Add("DESTINATION_TOWN_ID", typeof(int));
            
                

            Session["dtDebitTrans"] = dtDebitTrans;

            DataTable dtdr = (DataTable)Session["dtDebitTrans"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                #region forloop
                {
                    string strCNNo = "";
                    string strDescription = "";
                    int DIST_ID = 0;
                    int TOWN_ID = 0;
                    string strclientName = "";

                  


                    string strCONSIGNEE_NAME = dt.Rows[i]["xCONSIGNEE_NAME"].ToString();
                    string strCONSIGNEE_ADDRESS = dt.Rows[i]["xCONSIGNEE_ADDRESS"].ToString();
                    string strCONSIGNEE_MOBILE_NO = dt.Rows[i]["xCONSIGNEE_MOBILE_NO"].ToString();
                    string strDESTINATION_DIST_NAME = dt.Rows[i]["xDESTINATION_DIST_NAME"].ToString();
                    //string strDESTINATION_TOWN_NAME = dt.Rows[i]["xDESTINATION_TOWN_NAME"].ToString();
                    string strDESTINATION_TOWN_NAME = dt.Rows[i]["xDESTINATION_TOWN_NAME"].ToString().Replace("\r", "").Trim();

                    dcDISTRICT_MST cobjb = DISTRICT_MSTBL.GetDistrictInfoByName(strDESTINATION_DIST_NAME);
                    DIST_ID = cobjb.DIST_ID;
                    dcTHANA_TOWN_MST cobjbt = THANA_TOWN_MSTBL.GetThanaInfoByName(strDESTINATION_TOWN_NAME);
                    //string cleanData = rawData.Replace("\r", "");
                    TOWN_ID = cobjbt.TOWN_ID;

                    DataRow row;
                    row = dtdr.NewRow();
                    //row["SlNo"] = (i + 1).ToString();
                    row["CN_ID"] = 0;
                    row["CN_NUMBER"] = "";
                    row["CONSIGNEE_NAME"] = strCONSIGNEE_NAME; //strSubLedgerName;
                    row["CONSIGNEE_ADDRESS"] = strCONSIGNEE_ADDRESS;
                    row["CONSIGNEE_MOBILE_NO"] = strCONSIGNEE_MOBILE_NO;
                    row["DESTINATION_DIST_NAME"] = strDESTINATION_DIST_NAME;

                    row["DESTINATION_DIST_ID"] = DIST_ID;
                    row["DESTINATION_TOWN_NAME"] = strDESTINATION_TOWN_NAME;

                    row["DESTINATION_TOWN_ID"] = TOWN_ID;

                    dtdr.Rows.Add(row);
                    ViewState["dtdr"] = dtdr;
                }
                #endregion forloop
                DataTable dtf = new DataTable();
                dtf = (DataTable)ViewState["dtdr"];
                ViewState["BulkDebit"] = dtf;
                Session["dtDebitTrans"] = dtf;

                GridView1.DataSource = dtf;
                GridView1.DataBind();
            }
        }
        protected void ClearButton_OnClick(object sender, EventArgs e)
        {
            DrPasteTextBox.Text = "";

            //Modalpopupextender2.Show();
        }
        #endregion
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            this.ReportOpenType = ReportOpenTypeEnum.Preview;
            OpenEntryPreviewReport();
        }

        private void OpenEntryPreviewReport()
        {
            clsPrmWREL prm = new clsPrmWREL();
            ReportOptions rptOption = GetReportOptions();
            prm.CN_NUMBER = "WR002507001";


            AppReport rpt = WRELRGN.CN_Barcode_Report(prm, rptOption);
            string rk = AppReport.SetAppReportToSession(rpt, this.Context);
            ShowReport(rk);
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            int k = 0;
            if (FileUpload1.HasFile)
            {
                string ext = Path.GetExtension(FileUpload1.FileName).ToLower();
                if (ext == ".xlsx")
                {
                    string filePath = Server.MapPath("~/Uploads/" + FileUpload1.FileName);
                    FileUpload1.SaveAs(filePath);

                    var tbl = new DataTable();

                    using (var package = new ExcelPackage(new FileInfo(filePath)))
                    {
                        if (package.Workbook.Worksheets.Count == 0)
                        {
                            throw new Exception("❌ No worksheets found in the Excel file.");
                        }

                        var sheet = package.Workbook.Worksheets[1];

                        if (sheet.Dimension == null)
                        {
                            throw new Exception("Worksheet is empty.");
                        }

                        bool hasHeader = true;
                        int totalCols = sheet.Dimension.End.Column;
                        int totalRows = sheet.Dimension.End.Row;

                        // Add columns to DataTable
                        for (int col = 1; col <= totalCols; col++)
                        {
                            string columnName = hasHeader ? sheet.Cells[1, col].Text : "Column{col}";

                            if (string.IsNullOrWhiteSpace(columnName))
                                columnName = "Column{col}";

                            // Ensure uniqueness
                            if (tbl.Columns.Contains(columnName))
                            {
                                int i = 1;
                                string newColumnName;
                                do
                                {
                                    newColumnName = columnName + "_" + i++;
                                } while (tbl.Columns.Contains(newColumnName));
                                columnName = newColumnName;
                            }

                            tbl.Columns.Add(columnName);
                        }

                        // Add rows to DataTable
                        int startRow = hasHeader ? 2 : 1;
                        for (int rowNum = startRow; rowNum <= totalRows; rowNum++)
                        {
                            DataRow row = tbl.NewRow();
                            for (int col = 1; col <= totalCols; col++)
                            {
                                row[col - 1] = sheet.Cells[rowNum, col].Text;
                            }
                            tbl.Rows.Add(row);
                        }
                    }

                    // Populate your list from the DataTable
                    this.listDetails.Clear();
                    DBContext dc = null;
                    bool isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                     try
                      {

                    TEMP_CN_INFOBL.DeleteTempData(null);
                    bool isTransInit = dc.StartTransaction();
                    if (tbl.Rows.Count > 0)
                    {
                        foreach (DataRow Row in tbl.Rows)
                        {
                            //DBContext dc = null;
                            //bool isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                          
                            
                                dcTEMP_CN_INFO cObj = new dcTEMP_CN_INFO();
                                cObj.SLNO =Conversion.StringToInt(Row["SL_NO"].ToString());
                                cObj.CLIENT_DEPTORBRANCH = Row["DEPTORBRANCH"].ToString();
                                cObj.CN_NAME = Row["CONSIGNEE_NAME"].ToString();
                                cObj.ADDRESS = Row["CONSIGNEE_ADDRESS"].ToString();
                                cObj.CN_MOBILE_NO = Row["CONSIGNEE_PHONE"].ToString();
                                cObj.CN_DATE = Conversion.StringToDate(Row["DATE"].ToString());
                                cObj.ITEM_NAME = Row["ITEM"].ToString();

                                cObj.WEIGHT =Conversion.StringToDecimal( Row["WEIGHT"].ToString());
                                cObj.QTY = Conversion.StringToDecimal(Row["QTY"].ToString());
                                cObj.RATE =Conversion.StringToDecimal( Row["RATE"].ToString());
                                cObj.TAKA = Conversion.StringToDecimal(Row["TAKA"].ToString());
                                cObj.SERVICE_CHARGE = Conversion.StringToDecimal(Row["SERVICE_CHARGE"].ToString());
                                cObj.TOTAL_AMT = Conversion.StringToDecimal(Row["TOTAL_AMT"].ToString());     
                                cObj.SLA_BREEZE = Conversion.StringToInt(Row["SLA_Breeze"].ToString());
                                cObj.DISTANCE_TYPE_NAME = Row["DISTANCE_TYPE"].ToString();
                                TEMP_CN_INFOBL.Insert(cObj, dc);
                                dc.CommitTransaction(isTransInit);
                           
                           

                        }

                        CNTEMPlistDetails = TEMP_CN_INFOBL.GetTempCNListInfo("1", dc);
                      
                        foreach (var Item in CNTEMPlistDetails)
                        {
                            dcCN_CREATION_MST objcn = new dcCN_CREATION_MST();

                            objcn.SLNO = Item.SLNO;
                            objcn.CN_NUMBER = "";
                            objcn.CN_ID = 0;
                            //objcn.PICKUP_DATE = Item.PICKUP_DATE;
                            //objcn.CN_CLIENT_CODE = Item.CN_CLIENT_CODE;
                           
                            objcn.CONSIGNEE_NAME = Item.CN_NAME;
                            objcn.CONSIGNEE_MOBILE_NO = Item.CN_MOBILE_NO;
                            objcn.CONSIGNEE_ADDRESS = Item.ADDRESS;
                            objcn.ITEM_NAME = Item.ITEM_NAME;

                            objcn.ITEM_ID =Conversion.StringToInt(ITEM_MSTBL.getItemIDByItemName(Item.ITEM_NAME,null));
                            objcn.DISTANCE_TYPE_NAME = Item.DISTANCE_TYPE_NAME;
                            objcn.DISTANCE_TYPE_ID = DISTANCE_TYPE_MSTBL.getDistanceTypeIDByTypeName(objcn.DISTANCE_TYPE_NAME,null);
                            objcn.SERVICE_AMOUNT = Conversion.StringToDecimal(AGREEMENT_DETAILLBL.getServiceAmountByItemID(Conversion.StringToInt(hdnClientId.Value), objcn.ITEM_ID, objcn.DISTANCE_TYPE_ID, null));
                            objcn.AGR_DETAIL_ID = Conversion.StringToInt(AGREEMENT_DETAILLBL.getAgreementdtlIDByItemID(Conversion.StringToInt(hdnClientId.Value), objcn.ITEM_ID, objcn.DISTANCE_TYPE_ID, null)); 
                            //objcn.PRODUCT_TYPE = Item.PRODUCT_TYPE;
                            //objcn.UPS = Item.UPS;
                            //objcn.DESTINATION = Item.DESTINATION;
                             objcn.WEIGHT =Item.WEIGHT;
                                objcn.QTY = Item.QTY;
                                objcn.RATE =Item.RATE;
                                objcn.TAKA = Item.TAKA;
                                objcn.SERVICE_CHARGE = Item.SERVICE_CHARGE;
                                objcn.TOTAL_AMT = Item.TOTAL_AMT;
                            objcn.SLA_DAYS = Item.SLA_BREEZE;
                            //objcn.STATUS = Item.STATUS;
                            //objcn.NARRATION = Item.NARRATION;
                            objcn.BOOKING_DATE = Item.CN_DATE;
                            //objcn.REF_TYPE = Item.REF_TYPE;
                            
                            listDetails.Add(objcn);
                            
                        }
                        Session["CNList"] = listDetails;
                        GridView1.DataSource = listDetails;
                        GridView1.DataBind();
                        TEMP_CN_INFOBL.DeleteTempData(null);
                        btnSave.Enabled = true;
                    }
                  }
                  catch
                   {
                         dc.RollbackTransaction();
                     }
                    finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            
                }
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            if (Session["CNList"] != null)
            {
                GridView1.DataSource = (List<dcCN_CREATION_MST>)Session["CNList"];
                GridView1.DataBind();
            }
        }

       
        
    }
}