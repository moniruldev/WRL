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
            //base.AppObjectID = BLLibrary.SystemBL.AppObjectEnum.Frm1001_OptionInfo;
            //base.RestrictByPageInTab();

            //ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(this.LinkButton1);

            this.CN_ID = base.GetPageQueryInteger("id");

            if (!IsPostBack) //first Time
            {


                hdnLoggedInUser.Value = loggedinUser.UserID.ToString();
                //FillCombo();





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
               txtAggrementDtl.Text = obj.AGREEMENT_DESCRIPTION;
               hdnAggrementDtlId.Value = obj.AGR_DETAIL_ID.ToString();
               txtItemName.Text = obj.ITEM_NAME;
               hdnItemId.Value = obj.ITEM_ID.ToString();
               txtRoute.Text = obj.ROUTE_NAME;
               hdnRouteId.Value = obj.ROUTE_ID.ToString();
               txtServiceAmt.Text = obj.SERVICE_AMOUNT.ToString();
               txtHubName.Text = obj.HUB_NAME;
               //hdnHubId.Value = obj.HUB_ID.ToString();

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

            //txtCargoNo.Enabled = isEnabled;
            //txtCargoDate.Enabled = isEnabled;
            //txtWeight.Enabled = isEnabled;
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
            ////SetTextBoxState(txtCargoDate, isEnabled);
            //SetTextBoxState(txtWeight, isEnabled);
            //SetTextBoxState(txtRemarks, isEnabled);
            //SetTextBoxState(txtStartingDist, isEnabled);
            //SetTextBoxState(txtDestinationDist, isEnabled);
            //SetTextBoxState(txtDestinationTown, isEnabled);
            //SetTextBoxState(txtManagerName, isEnabled);
            SetTextBoxState(txtRoute, isEnabled);

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




        //protected void DisplayRoomDetails(int roomTypeId)
        //{
        //    byte[] bytes = null;
        //    dcHMROOM_TYPE objRT = HMROOM_TYPEBL.GetRoomTypeInfoById(roomTypeId);
        //    if (objRT.THUMBNAILS_IMAGE != null)
        //    {
        //        bytes = (byte[])objRT.THUMBNAILS_IMAGE;
        //        string strBase64 = Convert.ToBase64String(bytes);
        //        ImgRoomType.ImageUrl = "data:Image/png;base64," + strBase64;
        //    }
        //    lblRoomTitle.Text = "Room Type : " + objRT.TITLE;
        //    lblRoomDescription.Text ="Room Type : " + objRT.TITLE +", Description: "+ objRT.DESCRIPTION + ", Max Person: " + objRT.MAX_PERSON + ", Normal Rate: " + objRT.NORMAL_RATE + ", Discounted Rate: " + objRT.DISCOUNTED_RATE;
        //    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "MyScript", "$('#modalRoomDetails').modal({backdrop: 'static', keyboard: false});", true);
        //}


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
                    ((TextBox)gvR.FindControl("txtRecipientName")).Enabled = isEnabled;
                    //((TextBox)gvR.FindControl("txtCNNumber")).Attributes.Add("readonly", "readonly");
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
                        //if(cObj.ROOM_QTY > 0)
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


            //cObj.CN_NUMBER = strD;
            //cObj.CLIENT_ID = Conversion.StringToInt(hdnClientId.Value);
            //cObj.AGR_DETAIL_ID = Conversion.StringToInt(hdnAggrementDtlId.Value);
            //cObj.ITEM_ID = Conversion.StringToInt(hdnItemId.Value);
            //cObj.ROUTE_ID = Conversion.StringToInt(hdnRouteId.Value);
            //cObj.SERVICE_AMOUNT = Conversion.StringToDecimal(txtServiceAmt.Text);

            strD = ((TextBox)gvR.FindControl("txtRecipientName")).Text;
            cObj.CONSIGNEE_NAME = strD;

            strD = ((TextBox)gvR.FindControl("txtRecipientAddress")).Text;
            cObj.CONSIGNEE_ADDRESS = strD;

            strD = ((TextBox)gvR.FindControl("txtRecipientMobileNo")).Text;
            cObj.CONSIGNEE_MOBILE_NO = strD;

            strD = ((TextBox)gvR.FindControl("txtDestinationDist")).Text;
            cObj.DESTINATION_DIST_NAME = strD;

            strD = ((HiddenField)gvR.FindControl("hdnDestinationDistId")).Value;
            cObj.DESTINATION_DIST_ID = Conversion.StringToInt(strD);

            strD = ((TextBox)gvR.FindControl("txtTownName")).Text;
            cObj.DESTINATION_TOWN_NAME = strD;

            strD = ((HiddenField)gvR.FindControl("hdnTownId")).Value;
            cObj.DESTINATION_TOWN_ID = Conversion.StringToInt(strD);


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

            //if (txtManagerName.Text == "")
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('error', 'Enter Manager Name!', 'Error');", true);
            //    txtManagerName.Focus();
            //    return false;

            //}

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
            int newCN_ID = 0;
            ReadDetailsFromGrid();
            foreach(dcCN_CREATION_MST obj in this.listDetails)
            {
                dcCN_CREATION_MST cObj = new dcCN_CREATION_MST();
                cObj._RecordState = obj._RecordState;
                if(cObj._RecordState == RecordStateEnum.Added)
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
                cObj.ROUTE_ID = Conversion.StringToInt(hdnRouteId.Value);
                cObj.SERVICE_AMOUNT = Conversion.StringToDecimal(txtServiceAmt.Text);

                cObj.CONSIGNEE_NAME = obj.CONSIGNEE_NAME;
                cObj.CONSIGNEE_ADDRESS = obj.CONSIGNEE_ADDRESS;
                cObj.CONSIGNEE_MOBILE_NO = obj.CONSIGNEE_MOBILE_NO;
                cObj.DESTINATION_DIST_NAME = obj.DESTINATION_DIST_NAME;
                cObj.DESTINATION_DIST_ID = obj.DESTINATION_DIST_ID;
                cObj.DESTINATION_TOWN_NAME = obj.DESTINATION_TOWN_NAME;
                cObj.DESTINATION_TOWN_ID = obj.DESTINATION_TOWN_ID;

                newCN_ID = CN_CREATION_MSTBL.Save(cObj);
            }
   

          
            if (newCN_ID > 0)
            {
                this.CN_ID = newCN_ID;
                //ReadTask();
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

        //protected void btnUpload_Click(object sender, EventArgs e)
        //{
        //    string ConStr = "";
        //    string ext = Path.GetExtension(FileUpload1.FileName).ToLower();


        //    if (!string.IsNullOrEmpty(ext))
        //    {
        //        string path = Server.MapPath("~/Uploads/" + FileUpload1.FileName);
        //        if (!File.Exists(path))
        //        {
        //            File.Delete(path);
        //        }
        //        FileUpload1.SaveAs(path);

        //        if (ext.Trim() == ".xls")
        //        {
        //            //connection string for that file which extantion is .xls  
        //            ConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
        //        }
        //        else if (ext.Trim() == ".xlsx")
        //        {
        //            //connection string for that file which extantion is .xlsx  
        //            ConStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
        //        }

        //        string query = "SELECT * FROM [Sheet1$]";

        //        OleDbConnection conn = new OleDbConnection(ConStr);

        //        if (conn.State == ConnectionState.Closed)
        //        {
        //            conn.Open();
        //        }
        //        //create command object  
        //        OleDbCommand cmd = new OleDbCommand(query, conn);

        //        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        //        DataSet ds = new DataSet();

        //        da.Fill(ds);

        //        conn.Close();

        //        DataTable tbl = ds.Tables[0];
        //        int count = tbl.Rows.Count;
        //        string strSql = string.Empty;

        //        this.listDetails.Clear();


        //        foreach (DataRow Row in tbl.Rows)
        //        {
        //            dcCN_CREATION_MST cObj = new dcCN_CREATION_MST();
        //            //cObj.item_code = Row["ITEM_CODE"].ToString();
        //            //dcINV_ITEM_MASTER objitem = new dcINV_ITEM_MASTER();
        //            //objitem = INV_ITEM_MASTERBL.GetItemByCode(cObj.item_code);

        //            //if (objitem != null)
        //            //{
        //            cObj.CONSIGNEE_NAME = (Row["CONSIGNEE_NAME"].ToString());
        //            cObj.CONSIGNEE_ADDRESS = (Row["CONSIGNEE_ADDRESS"].ToString());
        //            cObj.CONSIGNEE_MOBILE_NO = (Row["CONSIGNEE_MOBILE_NO"].ToString());
        //            cObj.DESTINATION_DIST_NAME = (Row["DESTINATION_DIST_NAME"].ToString());
        //            cObj.DESTINATION_TOWN_NAME = (Row["DESTINATION_TOWN_NAME"].ToString());

        //            //cObj.PRIORITY = Row["PRIORITY"].ToString();
        //            //cObj.UOM_ID = objitem.UOM_ID;
        //            //cObj.INDT_REMARKS = Row["INDENT_REMARKS"].ToString();

        //            //this.listDetails.Add(cObj);
        //            //}
        //        }

        //        GridView1.DataSource = listDetails;
        //        GridView1.DataBind();
        //        //SetControlGrid();

        //        btnSave.Enabled = true;

        //    }


           
        //}

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
        
    }
}