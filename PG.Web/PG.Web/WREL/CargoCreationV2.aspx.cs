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
//using ClosedXML.Excel;
//using NPOI.SS.UserModel;
//using NPOI.XSSF.UserModel;
//using NPOI.HSSF.UserModel;
//using ClosedXML.Excel;
//using ClosedXML.Excel;

namespace PG.Web.WREL
{
    public partial class CargoCreationV2 : BagePage
    {
        //this 
        string ViewStateKey = "CARGO_ID";
        string ViewStateKeyPrev = "CARGO_ID_PREV";
        ReportOpenTypeEnum ReportOpenType = ReportOpenTypeEnum.Preview;
        // int CompanyID = 0;

        int CARGO_ID = 0;
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
        public string EmplistServiceLink = PageLinks.InventoryLink.GetLink_EmployeeList;
        
        List<dcCARGO_CREATION_DETAIL> listDetails = new List<dcCARGO_CREATION_DETAIL>();
        List<dcCARGO_CREATION_DETAIL> listDetailsCN = new List<dcCARGO_CREATION_DETAIL>();
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

            this.CARGO_ID = base.GetPageQueryInteger("id");

            if (!IsPostBack) //first Time
            {


                hdnLoggedInUser.Value = loggedinUser.UserID.ToString();
                //FillCombo();





                if (this.CARGO_ID == 0) //not query string
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
                this.CARGO_ID = int.Parse(ViewState[ViewStateKey].ToString());
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
            ReadData(this.CARGO_ID);
            ViewState[ViewStateKey] = this.CARGO_ID.ToString();

            SetControl(FormDataMode.Read);

        }
        private void AddTask()
        {
            ViewState[ViewStateKeyPrev] = this.CARGO_ID.ToString();

            this.EditMode = FormDataMode.Add;
            this.IsDirty = false;
            this.CARGO_ID = 0;
            ViewState[ViewStateKey] = "0";
            this.listDetails.Clear();
            //CheckAndAddGridBlankRow();
            BindDataToGrid(this.listDetails);
            //add
            SetControl(FormDataMode.Add);
        }
        private void EditTask()
        {
            this.EditMode = FormDataMode.Edit;
            ReadData(this.CARGO_ID);
            this.EditMode = FormDataMode.Edit;
            ViewState[ViewStateKey] = this.CARGO_ID.ToString();
            SetControl(FormDataMode.Edit);
        }

        private bool ReadData(int id)
        {
            bool bStatus = false;

            dcCARGO_CREATION_MST cObj = CARGO_CREATION_MSTBL.GetCargoMstInfoById(id);
            if (cObj != null)
            {

                hdnCARGO_ID.Value = cObj.CARGO_ID.ToString();
                txtCargoNo.Text = cObj.CARGO_NUMBER;
                txtCargoDate.Text = Convert.ToDateTime(cObj.CARGO_DATE).ToString("dd-MMM-yyyy");
                hdnStartingDistId.Value = cObj.CARGO_STARTING_DIS_ID.ToString();
                hdnDestDistId.Value = cObj.CARGO_DESTINATION_DIST_ID.ToString();
                hdnDestTownId.Value = cObj.CARGO_DESTINATION_TOWN_ID.ToString();
                hdnRouteId.Value = cObj.ROUTE_ID.ToString();
                hdnManagerId.Value = cObj.MANAGER_ID;
                txtWeight.Text = cObj.WEIGHT_IN_KG.ToString("0.##");
                txtRemarks.Text = cObj.REMARKS;
                txtStartingDist.Text = cObj.STARTING_DIST_NAME;
                txtDestinationDist.Text = cObj.DESTINATION_DIST_NAME;
                txtDestinationTown.Text = cObj.TOWN_NAME;
                txtManagerName.Text = cObj.MANAGER_NAME;
                txtRoute.Text = cObj.ROUTE_NAME;
                this.listDetails = CARGO_CREATION_DETAILBL.GetCargoDtlListByCargoId(cObj.CARGO_ID,null);
                BindDataToGrid(listDetails);

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
            SetTextBoxState(txtCargoNo, isEnabled);
            //SetTextBoxState(txtCargoDate, isEnabled);
            SetTextBoxState(txtWeight, isEnabled);
            SetTextBoxState(txtRemarks, isEnabled);
            SetTextBoxState(txtStartingDist, isEnabled);
            SetTextBoxState(txtDestinationDist, isEnabled);
            SetTextBoxState(txtDestinationTown, isEnabled);
            SetTextBoxState(txtManagerName, isEnabled);
            SetTextBoxState(txtRoute, isEnabled);

            txtCargoDate.Enabled = isEnabled;
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
                //int currentRowIndex = e.Row.RowIndex;
                //int serialNo = totalRowCount - currentRowIndex;

                //Label lblSerial = (Label)e.Row.FindControl("lblSerialNo");
                //if (lblSerial != null)
                //{
                //    lblSerial.Text = serialNo.ToString();
                //}
                Label lblSerial = (Label)e.Row.FindControl("lblSerialNo");
                if (lblSerial != null)
                {
                    // Set the serial number as (RowIndex + 1)
                    lblSerial.Text = (e.Row.RowIndex + 1).ToString();
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

            //if (e.CommandName == "roomdetials")
            //{
            //    int roomTypeId = Convert.ToInt32(e.CommandArgument);
            //    DisplayRoomDetails(roomTypeId);


            //}
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


        private void BindDataToGrid(List<dcCARGO_CREATION_DETAIL> listData)
        {
            int rowCount = listData.Count;
            this.totalRowCount = listData.Count;
            //if (rowCount == 0)
            //{
            //    listData.Add(new dcCARGO_CREATION_DETAIL());
            //}

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
                    //((TextBox)gvR.FindControl("txtCNName")).Enabled = isEnabled;
                    ((TextBox)gvR.FindControl("txtCNName")).Attributes.Add("readonly", "readonly");
                    ((TextBox)gvR.FindControl("txtClientName")).Attributes.Add("readonly", "readonly");
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
            //this.listDetails.Clear();

            ///addition
            foreach (GridViewRow gvR in this.GridView1.Rows)
            {
                if (gvR.RowType == DataControlRowType.DataRow)
                {

                    dcCARGO_CREATION_DETAIL cObj = new dcCARGO_CREATION_DETAIL();
                    ReadGridRowToObject(gvR, this.GridView1.DataKeys, cObj);

                    if (cObj._RecordState == RecordStateEnum.Deleted)
                    {
                        if (cObj.CARGO_DETAIL_ID > 0)
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

        private void ReadGridRowToObject(GridViewRow gvR, DataKeyArray dataKeys, dcCARGO_CREATION_DETAIL cObj)
        {
            decimal d;
            string strD;

            strD = ((HiddenField)gvR.FindControl("hdnCargoDtlId")).Value;
            cObj.CARGO_DETAIL_ID = Conversion.StringToInt(strD);
            if (cObj.CARGO_DETAIL_ID > 0)
            {
                cObj._RecordState = RecordStateEnum.Edited;
            }
            else
            {
                cObj._RecordState = RecordStateEnum.Added;
            }

            strD = ((HiddenField)gvR.FindControl("hdnCNID")).Value;
            cObj.CN_ID = Conversion.StringToInt(strD);

            strD = ((TextBox)gvR.FindControl("txtCNName")).Text;
            cObj.CN_NUMBER = strD;

            strD = ((TextBox)gvR.FindControl("txtClientName")).Text;
            cObj.CLIENT_NAME = strD;


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

        private bool ValidateDetails(List<dcCARGO_CREATION_DETAIL> list)
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

            //if(txtCheckInDate.Text == "")
            //{
            //    ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Please Enter Check In Date !!');", true);
            //    txtCheckInDate.Focus();
            //    return false;

            //}

            //if (txtCheckOutDate.Text == "")
            //{
            //    ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Please Enter Check Out Date !!');", true);
            //    txtCheckOutDate.Focus();
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
            int newCARGO_ID = 0;
            dcCARGO_CREATION_MST cObj = new dcCARGO_CREATION_MST();
            if (this.CARGO_ID > 0)
            {

                cObj.CARGO_ID = this.CARGO_ID;
                cObj._RecordState = RecordStateEnum.Edited;
            }
            else
            {
                cObj._RecordState = RecordStateEnum.Added;
                isAdd = true;
            }

            cObj.CARGO_NUMBER = txtCargoNo.Text.Trim();
            cObj.CARGO_DATE = Conversion.StringToDate(txtCargoDate.Text);
            cObj.CARGO_STARTING_DIS_ID = Conversion.StringToInt(hdnStartingDistId.Value);
            cObj.CARGO_DESTINATION_DIST_ID = Conversion.StringToInt(hdnStartingDistId.Value);
            cObj.CARGO_DESTINATION_TOWN_ID = Conversion.StringToInt(hdnStartingDistId.Value);
            cObj.ROUTE_ID = Conversion.StringToInt(hdnStartingDistId.Value);
            cObj.MANAGER_ID = hdnManagerId.Value;
            cObj.WEIGHT_IN_KG = Conversion.StringToDecimal(txtWeight.Text);
            cObj.REMARKS = txtRemarks.Text.Trim();


            if (isAdd)
            {
                cObj.CREATE_BY = loggedinUser.UserName;
                cObj.CREATE_DATE = DateTime.Now;

            }
            else
            {
                cObj.EDIT_BY = loggedinUser.UserName;
                cObj.EDIT_DATE = DateTime.Now;

            }



            cObj.cargoDetails = this.listDetails;

            newCARGO_ID = CARGO_CREATION_MSTBL.Save(cObj);
            if (newCARGO_ID > 0)
            {
                this.CARGO_ID = newCARGO_ID;
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

            if (GridView1.Rows.Count > 0)
            {
                TextBox txtTopCN = GridView1.Rows[0].FindControl("txtCNName") as TextBox;
                if (txtTopCN != null)
                {
                    ScriptManager.GetCurrent(this).SetFocus(txtTopCN);
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
            dcCARGO_CREATION_DETAIL cObj = new dcCARGO_CREATION_DETAIL();
            cObj._RecordState = RecordStateEnum.Added;
            this.listDetails.Add(cObj);
            //this.listDetails.Insert(0, cObj);
        }

        protected void txtCNName_TextChanged(object sender, EventArgs e)
        {
            //btnNewRow_Click(sender, e);
            
        }
        private void SetFocusToLastTxtCNName()
        {
            int lastIndex = GridView1.Rows.Count - 1;
            if (lastIndex >= 0)
            {
                TextBox lastTxtCN = GridView1.Rows[0].FindControl("txtCNName") as TextBox;
                if (lastTxtCN != null)
                {
                    ScriptManager.GetCurrent(this).SetFocus(lastTxtCN);
                }
            }
        }

        protected void txtCNNo_TextChanged(object sender, EventArgs e)
        {
            dcCARGO_CREATION_DETAIL cobjb = CARGO_CREATION_DETAILBL.GetCNInfoByCNNumber(txtCNNo.Text.Trim());
            hdnCNIDtop.Value = cobjb.CN_ID.ToString();
            hdnClientName.Value = cobjb.CLIENT_NAME;
            
            btnadd_Click(sender, e);
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            dcCARGO_CREATION_DETAIL cobjb = new dcCARGO_CREATION_DETAIL();
            cobjb.CN_ID =Conversion.StringToInt(hdnCNIDtop.Value);
            cobjb.CN_NUMBER = txtCNNo.Text;
            cobjb.CLIENT_NAME = hdnClientName.Value;
            listDetails.Add(cobjb);
            ReadDetailsFromGrid();
            BindDataToCNNumber(this.listDetails);
            cleartext();
            txtCNNo.Focus();
        }

        private void BindDataToCNNumber(List<dcCARGO_CREATION_DETAIL> listDataCN)
        {
            int rowCount = listDataCN.Count;
            //if (rowCount == 0)
            //{
            //    listData.Add(new dcGLAccountHistoryRef());
            //}

            GridView1.DataSource = listDataCN.ToList();
            GridView1.DataBind();

            GridView1.CssClass = "grid";
            //SumDetGrid1();
            //SumDetGrid1();

            //int i = GRDDTLITEMLIST.PageCount;

            //if (GRDDTLITEMLIST.PageIndex > 0)
            //{
          //  GRDDTLITEMLIST.PageIndex = GRDDTLITEMLIST.PageCount;
               // GRDDTLITEMLIST.PageIndex = GRDDTLITEMLIST.PageIndex - 1;



            //}

        }

        private void cleartext()
        {
            hdnCNIDtop.Value = "";
            txtCNNo.Text = "";
            hdnClientName.Value = "";
            
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (!FileUpload1.HasFile)
            {
                //lblMessage.Text = "Please select an Excel file.";
                return;
            }

            string ext = Path.GetExtension(FileUpload1.FileName).ToLower();
            string path = Server.MapPath("~/Uploads/" + FileUpload1.FileName);

            // Delete existing file if needed
            //if (File.Exists(path))
            //{
            //    File.Delete(path);
            //}

            //// Save uploaded file
            //FileUpload1.SaveAs(path);

            //IWorkbook workbook;
            //using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            //{
            //    if (ext == ".xls")
            //    {
            //        workbook = new HSSFWorkbook(fs); // For Excel 97-2003
            //    }
            //    else if (ext == ".xlsx")
            //    {
            //        workbook = new XSSFWorkbook(fs); // For Excel 2007+
            //    }
            //    else
            //    {
            //        //lblMessage.Text = "Invalid file type. Only .xls or .xlsx allowed.";
            //        return;
            //    }
            //}

            //ISheet worksheet = workbook.GetSheetAt(0); // First worksheet

            //DataTable tbl = new DataTable();
            //bool firstRow = true;

            //for (int rowNum = worksheet.FirstRowNum; rowNum <= worksheet.LastRowNum; rowNum++)
            //{
            //    IRow row = worksheet.GetRow(rowNum);
            //    if (row == null) continue;

            //    if (firstRow)
            //    {
            //        // Header row
            //        foreach (ICell cell in row.Cells)
            //        {
            //            tbl.Columns.Add(cell.ToString());
            //        }
            //        firstRow = false;
            //    }
            //    else
            //    {
            //        DataRow dr = tbl.NewRow();
            //        int i = 0;
            //        foreach (ICell cell in row.Cells)
            //        {
            //            dr[i++] = cell.ToString();
            //        }
            //        tbl.Rows.Add(dr);
            //    }
            //}

            //// Process DataTable and bind to GridView
            //this.listDetails.Clear();

            //foreach (DataRow row in tbl.Rows)
            //{
            //    if (!tbl.Columns.Contains("CN_NUMBER")) continue;

            //    string cnNumber = row["CN_NUMBER"].ToString();
            //    if (string.IsNullOrWhiteSpace(cnNumber)) continue;

            //    dcCARGO_CREATION_DETAIL cObj = new dcCARGO_CREATION_DETAIL();
            //    cObj.CN_NUMBER = cnNumber;

            //    // Fetch CN info from DB
            //    dcCARGO_CREATION_DETAIL cobjbdt = CARGO_CREATION_DETAILBL.GetCNInfoByCNNumber(cObj.CN_NUMBER);

            //    if (cobjbdt != null)
            //    {
            //        cObj.CN_ID = cobjbdt.CN_ID;
            //        cObj.CLIENT_NAME = cobjbdt.CLIENT_NAME;

            //        this.listDetails.Add(cObj);
            //    }
            //}

            //GridView1.DataSource = listDetails;
            //GridView1.DataBind();

            //btnSave.Enabled = listDetails.Count > 0;
            // lblMessage.Text = "Excel file uploaded successfully.";
        }

//        protected void btnUpload_Click(object sender, EventArgs e)
//        {

//            string ext = Path.GetExtension(FileUpload1.FileName).ToLower();
//            string path = Server.MapPath("~/Uploads/" + FileUpload1.FileName);
//            if (!File.Exists(path))
//            {
//                File.Delete(path);
//            }
//            FileUpload1.SaveAs(path);
//XLWorkbook workbook = new XLWorkbook(path);
//IXLWorksheet worksheet = workbook.Worksheet(1); // first worksheet

//DataTable tbl = new DataTable();

//bool firstRow = true;

//var firstRowUsed = worksheet.FirstRowUsed();
//var lastRowUsed = worksheet.LastRowUsed();

//for (int rowNum = firstRowUsed.RowNumber(); rowNum <= lastRowUsed.RowNumber(); rowNum++)
//{
//    var row = worksheet.Row(rowNum);

//    if (firstRow)
//    {
//        // Loop cells by index for header row
//        int colIndex = 1;
//        while (true)
//        {
//            var cell = row.Cell(colIndex);
//            if (cell == null || cell.IsEmpty())
//                break;

//            tbl.Columns.Add(cell.Value.ToString());
//            colIndex++;
//        }
//        firstRow = false;
//    }
//    else
//    {
//        DataRow dr = tbl.NewRow();
//        int i = 0;
//        int colIndex = 1;
//        while (true)
//        {
//            var cell = row.Cell(colIndex);
//            if (cell == null || cell.IsEmpty())
//                break;

//            dr[i++] = cell.Value.ToString();
//            colIndex++;
//        }
//        tbl.Rows.Add(dr);
//    }
//}

//// Bind to GridView or your list
//this.listDetails.Clear();

//foreach (DataRow row in tbl.Rows)
//{
//    if (row["CN_NUMBER"] == DBNull.Value || string.IsNullOrWhiteSpace(row["CN_NUMBER"].ToString()))
//        continue;

//    dcCARGO_CREATION_DETAIL cObj = new dcCARGO_CREATION_DETAIL();
//    cObj.CN_NUMBER = row["CN_NUMBER"].ToString();

//    // Fetch CN info from DB
//    dcCARGO_CREATION_DETAIL cobjbdt = CARGO_CREATION_DETAILBL.GetCNInfoByCNNumber(cObj.CN_NUMBER);

//    if (cobjbdt != null)
//    {
//        cObj.CN_ID = cobjbdt.CN_ID;
//        cObj.CLIENT_NAME = cobjbdt.CLIENT_NAME;

//        this.listDetails.Add(cObj);
//    }
//}

//GridView1.DataSource = listDetails;
//GridView1.DataBind();

//btnSave.Enabled = listDetails.Count > 0;

//            //string ConStr = "";
//            //string ext = Path.GetExtension(FileUpload1.FileName).ToLower();


//            //if (!string.IsNullOrEmpty(ext))
//            //{
//            //    string path = Server.MapPath("~/Uploads/" + FileUpload1.FileName);
//            //    if (!File.Exists(path))
//            //    {
//            //        File.Delete(path);
//            //    }
//            //    FileUpload1.SaveAs(path);

//            //    if (ext.Trim() == ".xls")
//            //    {
//            //        //connection string for that file which extantion is .xls  
//            //        ConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
//            //    }
//            //    else if (ext.Trim() == ".xlsx")
//            //    {
//            //        //connection string for that file which extantion is .xlsx  
//            //        ConStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
//            //    }

//            //    string query = "SELECT * FROM [Sheet1$]";
//            //    //Providing connection  
//            //    OleDbConnection conn = new OleDbConnection(ConStr);
//            //    //checking that connection state is closed or not if closed the   
//            //    //open the connection  
//            //    if (conn.State == ConnectionState.Closed)
//            //    {
//            //        conn.Open();
//            //    }
//            //    //create command object  
//            //    OleDbCommand cmd = new OleDbCommand(query, conn);
//            //    // create a data adapter and get the data into dataadapter  
//            //    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
//            //    DataSet ds = new DataSet();
//            //    //DataTable tbl = new DataTable();
//            //    //fill the Excel data to data set  
//            //    da.Fill(ds);

//            //    //set data source of the grid view  

//            //    conn.Close();

//            //    DataTable tbl = ds.Tables[0];
//            //    int count = tbl.Rows.Count;
//            //    //DBContext dc = null;
//            //    //bool isDCInit = false;

//            //    string strSql = string.Empty;
//            //    //DataTable dtData = null;

//            //    //DataTable dtDataSum = null;
//            //    this.listDetails.Clear();
//            //    //List<dcSALE_TARGET_SE> listObj = new List<dcSALE_TARGET_SE>();

//            //    foreach (DataRow Row in tbl.Rows)
//            //    {
//            //        dcCARGO_CREATION_DETAIL cObj = new dcCARGO_CREATION_DETAIL();
//            //        cObj.CN_NUMBER = Row["CN_NUMBER"].ToString();
//            //        dcCARGO_CREATION_DETAIL cobjbdt = new dcCARGO_CREATION_DETAIL();
//            //        cobjbdt = CARGO_CREATION_DETAILBL.GetCNInfoByCNNumber(txtCNNo.Text.Trim()); ;

//            //        if (cobjbdt != null)
//            //        {
//            //            cObj.CN_ID = Conversion.StringToInt(Row["CN_ID"].ToString());
//            //            cObj.CLIENT_NAME = cobjbdt.CLIENT_NAME;


//            //            this.listDetails.Add(cObj);
//            //        }
//            //    }

//            //    GridView1.DataSource = listDetails;
//            //    GridView1.DataBind();
//            //    //SetControlGrid();

//            //    btnSave.Enabled = true;

//            //}
//        }

    }
}