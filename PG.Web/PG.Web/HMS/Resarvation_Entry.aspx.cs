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

namespace PG.Web.HMS
{
    public partial class Resarvation_Entry : BagePage
    {
        //this 
        string ViewStateKey = "GuestId";
        string ViewStateKeyPrev = "GuestId_PREV";
        ReportOpenTypeEnum ReportOpenType = ReportOpenTypeEnum.Preview;
        // int CompanyID = 0;
       
        int GuestId = 0;
        string saveMsg = string.Empty;
        string errMsg = string.Empty;

        private  dcUser loggedinUser = null;
        public string ReportViewPageLink = PageLinks.ReportLinks.GetLink_ReportView;
        public string ReportViewPDFPageLink = PageLinks.ReportLinks.GetLink_ReportViewPDF;
        public string ReportPrintPageLink = PageLinks.ReportLinks.GetLink_ReportPrint;
        public string ReportPDFPageLink = PageLinks.ReportLinks.GetLink_ReportPDF;



        public string CountryListServiceLink = PageLinks.InventoryLink.GetLink_CountryList;

        List<dcHMRESERVATION_DTL> listDetails = new List<dcHMRESERVATION_DTL>();

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

            this.GuestId = base.GetPageQueryInteger("id");

            if (!IsPostBack) //first Time
            {

              
                hdnLoggedInUser.Value = loggedinUser.UserID.ToString();
                FillCombo();


             
              

                if (this.GuestId == 0) //not query string
                {
                    List<dcHMRESERVATION_DTL> roomList = HMRESERVATION_DTLBL.GetRoomInfoList();
                    GridView1.DataSource = roomList;
                    GridView1.DataBind();
                   
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
                this.GuestId = int.Parse(ViewState[ViewStateKey].ToString());
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

            List<dcHMRESERVATION_DTL> roomList = HMRESERVATION_DTLBL.GetRoomInfoList();
            GridView1.DataSource = roomList;
            GridView1.DataBind();
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
            ReadData(this.GuestId);
            ViewState[ViewStateKey] = this.GuestId.ToString();

            SetControl(FormDataMode.Read);

        }
        private void AddTask()
        {
            ViewState[ViewStateKeyPrev] = this.GuestId.ToString();

            this.EditMode = FormDataMode.Add;
            this.IsDirty = false;
            this.GuestId = 0;
            ViewState[ViewStateKey] = "0";
            this.listDetails.Clear();
           //add
            SetControl(FormDataMode.Add);
        }
        private void EditTask()
        {
            this.EditMode = FormDataMode.Edit;
            ReadData(this.GuestId);
            this.EditMode = FormDataMode.Edit;
            ViewState[ViewStateKey] = this.GuestId.ToString();
            SetControl(FormDataMode.Edit);
        }

        private bool ReadData(int id)
        {
            bool bStatus = false;

            dcHMGUEST_MST cObj = HMGUEST_MSTBL.GetGuestInfoById(id);
            if (cObj != null)
            {
                
                hdnGuestId.Value = cObj.GUEST_ID.ToString();
                txtName.Text = cObj.GUEST_NAME;
                txtMobileNo.Text = cObj.MOBILE_NO;
                txtAddress.Text = cObj.ADDRESS;
                txtEmail.Text = cObj.EMAIL;
                txtPhoneNo.Text = cObj.PABX_NO;
                txtPassportNo.Text = cObj.PASSPORT_NO;
                txtBirthDate.Text = Convert.ToDateTime(cObj.DATE_OF_BIRTH).ToString("dd-MMM-yyyy");
                txtCheckInDate.Text = cObj.CHECK_IN.ToString("dd-MMM-yyyy");
                txtCheckOutDate.Text = cObj.CHECK_OUT.ToString("dd-MMM-yyyy");
                txtNote.Text = cObj.NOTE;
                rblGender.SelectedValue = cObj.GENDER;
                hdnCountryId.Value = cObj.COUNTRY_ID.ToString();
                txtCountry.Text = cObj.COUNTRY_NAME;
                hdnReservationId.Value = cObj.RESERVATION_ID.ToString();
                dcHMRESERVATION_MST objRMst = HMRESERVATION_MSTBL.GetResarvationMstById(cObj.RESERVATION_ID);
                if(objRMst.CONFIRMED_NOTE != "")
                {
                    btnConfirm.Enabled = false;
                    btnEdit.Enabled = false;
                    btnConfirm.CssClass="btn btn-primary disabled";
                    btnEdit.CssClass = "btn btn-primary disabled";
                }
                if (objRMst.CANCEL_NOTE != "")
                {
                    btnCancel.Enabled = false;
                    btnEdit.Enabled = false;
                    btnCancel.CssClass = "btn btn-danger disabled";
                    btnEdit.CssClass = "btn btn-primary disabled";
                }

                this.listDetails = HMRESERVATION_DTLBL.GetResarvationInfoListById(cObj.RESERVATION_ID);
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

            txtCheckInDate.Enabled = isEnabled;
            txtCheckOutDate.Enabled = isEnabled;
            txtMobileNo.Enabled = isEnabled;
            txtName.Enabled = isEnabled;
            txtNote.Enabled = isEnabled;
            txtPassportNo.Enabled = isEnabled;
            txtPhoneNo.Enabled = isEnabled;
            txtEmail.Enabled = isEnabled;
            txtAddress.Enabled = isEnabled;
            txtBirthDate.Enabled = isEnabled;
            txtCountry.Enabled = isEnabled;
            rblGender.Enabled = isEnabled;
            
            //buttons
            btnAddNew.Visible = !isEnabled;
            btnEdit.Visible = !isEnabled;
            btnConfirm.Visible = !isEnabled;
            btnCancel.Visible = !isEnabled;
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

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnNoOfRoom = (e.Row.FindControl("hdnNoOfRoom") as HiddenField);
                HiddenField hdnRoomTypeId = (e.Row.FindControl("hdnRoomTypeId") as HiddenField);
                HiddenField hdnReservationDtlId = (e.Row.FindControl("hdnReservationDtlId") as HiddenField);
                HiddenField hdnRoomQty = (e.Row.FindControl("hdnRoomQty") as HiddenField);
                DropDownList ddlNoOfRoom = (e.Row.FindControl("ddlNoOfRoom") as DropDownList);
                for (int i = 0; i <= Convert.ToInt32(hdnNoOfRoom.Value); i++)
                {
                    ddlNoOfRoom.Items.Add(i.ToString());
                    
                }

                foreach (dcHMRESERVATION_DTL item in this.listDetails)
                {
                    hdnReservationDtlId.Value = item.RESERVATION_DTL_ID.ToString();
                    if(item.ROOM_QTY > 0 && item.ROOM_TYPE_ID == Convert.ToInt32(hdnRoomTypeId.Value))
                    {
                        ddlNoOfRoom.SelectedValue = item.ROOM_QTY.ToString();
                      
                    }
                    
                }

                //if(Conversion.StringToInt(hdnRoomQty.Value) > 0 )
                //{
                //    ddlNoOfRoom.SelectedValue = hdnRoomQty.Value;
                //}

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

            if (e.CommandName == "roomdetials")
            {
                int roomTypeId = Convert.ToInt32(e.CommandArgument);
                DisplayRoomDetails(roomTypeId);


            }
        }

        protected void DisplayRoomDetails(int roomTypeId)
        {
            byte[] bytes = null;
            dcHMROOM_TYPE objRT = HMROOM_TYPEBL.GetRoomTypeInfoById(roomTypeId);
            if (objRT.THUMBNAILS_IMAGE != null)
            {
                bytes = (byte[])objRT.THUMBNAILS_IMAGE;
                string strBase64 = Convert.ToBase64String(bytes);
                ImgRoomType.ImageUrl = "data:Image/png;base64," + strBase64;
            }
            lblRoomTitle.Text = "Room Type : " + objRT.TITLE;
            lblRoomDescription.Text ="Room Type : " + objRT.TITLE +", Description: "+ objRT.DESCRIPTION + ", Max Person: " + objRT.MAX_PERSON + ", Normal Rate: " + objRT.NORMAL_RATE + ", Discounted Rate: " + objRT.DISCOUNTED_RATE;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "MyScript", "$('#modalRoomDetails').modal({backdrop: 'static', keyboard: false});", true);
        }


        private void BindDataToGrid(List<dcHMRESERVATION_DTL> listData)
        {
            int rowCount = listData.Count;
            if (rowCount == 0)
            {
                listData.Add(new dcHMRESERVATION_DTL());
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
                    ((DropDownList)gvR.FindControl("ddlNoOfRoom")).Enabled = isEnabled;

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

                    dcHMRESERVATION_DTL cObj = new dcHMRESERVATION_DTL();
                    ReadGridRowToObject(gvR, this.GridView1.DataKeys, cObj);

                    if (cObj._RecordState == RecordStateEnum.Deleted)
                    {
                        if (cObj.RESERVATION_DTL_ID > 0)
                        {
                            this.listDetails.Add(cObj);
                        }
                    }
                    else
                    {
                        if(cObj.ROOM_QTY > 0)
                        this.listDetails.Add(cObj);
                    }

                }
            }
        }

        private void ReadGridRowToObject(GridViewRow gvR, DataKeyArray dataKeys, dcHMRESERVATION_DTL cObj)
        {
            decimal d;
            string strD;

            strD = ((HiddenField)gvR.FindControl("hdnReservationDtlId")).Value;
            cObj.RESERVATION_DTL_ID = Conversion.StringToInt(strD);
            if (cObj.RESERVATION_DTL_ID > 0)
            {
                cObj._RecordState = RecordStateEnum.Edited;
            }
            else
            {
                cObj._RecordState = RecordStateEnum.Added;
            }

            strD = ((DropDownList)gvR.FindControl("ddlNoOfRoom")).SelectedValue;
            cObj.ROOM_QTY = Conversion.StringToInt(strD);

            strD = ((HiddenField)gvR.FindControl("hdnRoomTypeId")).Value;
            cObj.ROOM_TYPE_ID = Conversion.StringToInt(strD);

            

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

        private bool ValidateDetails(List<dcHMRESERVATION_DTL> list)
        {
            bool y = true;
            foreach (var item in list)
            {
                if(!(item.ROOM_QTY > 0))
                {
                    ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Please Select atleast one Room !!');", true);
                    y = false;

                }
                
            }

            return y;
        }

        private bool CheckData()
        {
            errMsg = string.Empty;
          
            if(txtCheckInDate.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Please Enter Check In Date !!');", true);
                txtCheckInDate.Focus();
                return false;

            }

            if (txtCheckOutDate.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Please Enter Check Out Date !!');", true);
                txtCheckOutDate.Focus();
                return false;

            }

            if (txtName.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Please Enter Name !!');", true);
                txtName.Focus();
                return false;

            }

            if (txtAddress.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Please Enter Address !!');", true);
                txtAddress.Focus();
                return false;

            }

            if (txtMobileNo.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Please Enter Mobile No !!');", true);
                txtMobileNo.Focus();
                return false;

            }

            if (hdnCountryId.Value == "0")
            {
                ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Please Select Country !!');", true);
                txtCountry.Focus();
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
            int newGuestId = 0;
            dcHMGUEST_MST guest = new dcHMGUEST_MST();
            if(this.GuestId > 0)
            {
                dcHMGUEST_MST cObj = HMGUEST_MSTBL.GetGuestInfoById(this.GuestId);
                guest.objReservationMst.RESERVATION_ID = cObj.RESERVATION_ID;
                guest.GUEST_ID = this.GuestId;
                guest._RecordState = RecordStateEnum.Edited;
            }
            else
            {
                guest._RecordState = RecordStateEnum.Added;
                isAdd = true;
            }


            bool checkRoomNo = false;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow row = GridView1.Rows[i];

                DropDownList ddlNoOfRoom = (DropDownList)row.FindControl("ddlNoOfRoom");
                string a = ddlNoOfRoom.SelectedItem.Text;
                if (Convert.ToInt32(ddlNoOfRoom.SelectedValue) != 0)
                {
                    checkRoomNo = true;
                }



            }

            if (checkRoomNo == true)
            {
                guest.GUEST_NAME = txtName.Text;
                guest.ADDRESS = txtAddress.Text;
                guest.EMAIL = txtEmail.Text;
                guest.COUNTRY_ID = Conversion.StringToInt(hdnCountryId.Value);
                if (txtBirthDate.Text != "")
                {
                    guest.DATE_OF_BIRTH = Conversion.StringToDate(txtBirthDate.Text);
                }
                guest.GENDER = rblGender.SelectedValue;
                guest.MOBILE_NO = txtMobileNo.Text;
                guest.PASSPORT_NO = txtPassportNo.Text;
                guest.PABX_NO = txtPhoneNo.Text;
                guest.IS_ACTIVE = "Y";
                if(isAdd)
                {
                    guest.CREATE_BY = loggedinUser.UserName;
                    guest.CREATE_DATE = DateTime.Now;
                    guest.objReservationMst.CREATE_DATE = DateTime.Now;
                    guest.objReservationMst.CREATE_BY = loggedinUser.UserName;
                }
                else
                {
                    guest.UPDATE_BY = loggedinUser.UserName;
                    guest.UPDATE_DATE = DateTime.Now;

                }

                guest.objReservationMst.CHECK_IN = Conversion.StringToDate(txtCheckInDate.Text);
                guest.objReservationMst.CHECK_OUT = Conversion.StringToDate(txtCheckOutDate.Text);
               
                guest.objReservationMst.STATUS = "Request";
                guest.objReservationMst.NOTE = txtNote.Text;

                guest.ReservationDtlList = this.listDetails;

               newGuestId = HMGUEST_MSTBL.Save(guest);
                if(newGuestId > 0)
                {
                    this.GuestId = newGuestId;
                    ReadTask();
                    bStatus = true;
                    ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Data saved successfully !!');", true);
                }



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
            //int issueMasterId = Conversion.StringToInt(hdnGuestId.Value);
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

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "MyScript", "$('#modalbtnConfirm').modal({backdrop: 'static', keyboard: false});", true);

        }

        protected void btnConfirmSave_Click(object sender, EventArgs e)
        {
            if(txtConfirmNote.Text != "")
            {
                dcHMRESERVATION_MST cObj = new dcHMRESERVATION_MST();
                cObj.RESERVATION_ID = Conversion.StringToInt(hdnReservationId.Value);
                cObj.CONFIRMED_BY = loggedinUser.UserName;
                cObj.CONFIRMED_DATE = DateTime.Now;
                cObj.CONFIRMED_NOTE = txtConfirmNote.Text.Trim();
                HMRESERVATION_MSTBL.Update(cObj);
                btnConfirm.Enabled = false;
                ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Reservation Confirmed Successfully !!');", true);

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Enter Confirm Note !!');", true);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "MyScript", "$('#modalbtnConfirm').modal({backdrop: 'static', keyboard: false});", true);
            }
         

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "MyScript", "$('#modalbtnCancel').modal({backdrop: 'static', keyboard: false});", true);
        }

        protected void btnCancelSave_Click(object sender, EventArgs e)
        {
            if (txtCancelNote.Text != "")
            {
                dcHMRESERVATION_MST cObjcancel = new dcHMRESERVATION_MST();
                cObjcancel.RESERVATION_ID = Conversion.StringToInt(hdnReservationId.Value);
                cObjcancel.CANCEL_BY = loggedinUser.UserName;
                cObjcancel.CANCEL_DATE = DateTime.Now;
                cObjcancel.CANCEL_NOTE = txtCancelNote.Text.Trim();
                HMRESERVATION_MSTBL.Update(cObjcancel);
                btnCancel.Enabled = false;
                ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Reservation Canceled !!');", true);

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Enter Cancel Note !!');", true);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "MyScript", "$('#modalbtnCancel').modal({backdrop: 'static', keyboard: false});", true);
            }

        }
    }
}