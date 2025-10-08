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
using System.IO;
using System.Text;
using System.Data;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;
using PG.BLLibrary.WRElBL;
using PG.DBClass.WRELDC;

namespace PG.Web.WREL
{
    public partial class CargoReceivedMst : BagePage
    {
        //this 
        string ViewStateKey = "CARGO_TRACK_ID";
        string ViewStateKeyPrev = "CARGO_TRACK_ID_PREV";
        ReportOpenTypeEnum ReportOpenType = ReportOpenTypeEnum.Preview;
        // int CompanyID = 0;

        int CARGO_TRACK_ID = 0;
        string saveMsg = string.Empty;
        string errMsg = string.Empty;

        private  dcUser loggedinUser = null;
        public string ReportViewPageLink = PageLinks.ReportLinks.GetLink_ReportView;
        public string ReportViewPDFPageLink = PageLinks.ReportLinks.GetLink_ReportViewPDF;
        public string ReportPrintPageLink = PageLinks.ReportLinks.GetLink_ReportPrint;
        public string ReportPDFPageLink = PageLinks.ReportLinks.GetLink_ReportPDF;



        public string CountryListServiceLink = PageLinks.InventoryLink.GetLink_CountryList;
        //public string CargoListService = PageLinks.InventoryLink.GetLink_CargoMstList;
        //public string HubListServiceLink = PageLinks.InventoryLink.GetLink_HubList;
        public string CargoReceivePendingListServiceLink = PageLinks.InventoryLink.GetLink_CargoReceivePendingList;
        
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

            this.CARGO_TRACK_ID = base.GetPageQueryInteger("id");

            if (!IsPostBack) //first Time
            {

              
                hdnLoggedInUser.Value = loggedinUser.UserID.ToString();
                FillCombo();


                if (this.CARGO_TRACK_ID == 0) //not query string
                {
                   
                    AddTask();
                    SetDate();
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
                this.CARGO_TRACK_ID = int.Parse(ViewState[ViewStateKey].ToString());
            }

            SetHyperLink();
            txtFromHubName.Attributes.Add("readonly", "readonly");
            txtToHubName.Attributes.Add("readonly", "readonly");
            txtTransportMedia.Attributes.Add("readonly", "readonly");
            txtContactPerson.Attributes.Add("readonly", "readonly");

            txtMobileNo.Attributes.Add("readonly", "readonly");
              
                  
            //this.ShowPageMessage(this.lblMessage);
            // Response.Write("UserID : " + this.UserID.ToString());

        }
     
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            EditTask();
        }

        public void FillCombo()
        {
            dcCLIENT_TYPE_MST clientType = new dcCLIENT_TYPE_MST();
            clientType.IS_ACTIVE = "Y";
            //ddlClientType.Items.Clear();
            //ddlClientType.AppendDataBoundItems = true;
            //ddlClientType.DataTextField = "TYPE_NAME";
            //ddlClientType.DataValueField = "CLIENT_TYPE_ID";
            //ddlClientType.DataSource = CLIENT_TYPE_MSTBL.GetCLIENT_TYPEList(clientType, null);
            //ddlClientType.DataBind();
            //ddlClientType.SelectedIndex = 0;

            //ddlRoomStatus.Items.Clear();
            //ddlRoomStatus.AppendDataBoundItems = true;
            //ddlRoomStatus.DataTextField = "ROOM_STATUS";
            //ddlRoomStatus.DataValueField = "ROOM_STATUS_ID";
            //ddlRoomStatus.DataSource = HMROOMBL.GetRoomStatusList();
            //ddlRoomStatus.DataBind();
            //ddlRoomStatus.SelectedIndex = 0;


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
        
            txtIssuedate.Text = DateTime.Now.ToString("dd-MMM-yyyy");

        }

        private void ReadTask()
        {
            this.EditMode = FormDataMode.Read;
            ReadData(this.CARGO_TRACK_ID);
            ViewState[ViewStateKey] = this.CARGO_TRACK_ID.ToString();

            SetControl(FormDataMode.Read);

        }
        private void AddTask()
        {
            ViewState[ViewStateKeyPrev] = this.CARGO_TRACK_ID.ToString();

            this.EditMode = FormDataMode.Add;
            this.IsDirty = false;
            this.CARGO_TRACK_ID = 0;
           

            ResetFormFields();
            ViewState[ViewStateKey] = "0";
            SetControl(FormDataMode.Add);
        }
        private void EditTask()
        {
            this.EditMode = FormDataMode.Edit;
            ReadData(this.CARGO_TRACK_ID);
            this.EditMode = FormDataMode.Edit;
            ViewState[ViewStateKey] = this.CARGO_TRACK_ID.ToString();
            SetControl(FormDataMode.Edit);
        }

        private void ResetFormFields()
        {
            hdnCargoID.Value = string.Empty;
            txtCargoNo.Text = string.Empty;
            txtFromHubName.Text = string.Empty;
            hdnFromHubID.Value = string.Empty;
            txtToHubName.Text = string.Empty;
            hdnToHubID.Value = string.Empty;
            txtContactPerson.Text = string.Empty;
            txtTransportMedia.Text = string.Empty;
            hdnTransMediaID.Value = string.Empty;
            txtIssuedate.Text = string.Empty;

            txtMobileNo.Text = string.Empty;
            txtRemarks.Text = string.Empty;
        }


        private bool ReadData(int id)
        {
            bool bStatus = false;
            byte[] bytes = null;
            dcCARGO_TRACKING cObj = CARGO_TRACKINGBL.GetCargoReceivedInfoById(id);
            if (cObj != null)
            {
                hdnCargoID.Value = cObj.CARGO_ID.ToString();
                txtCargoNo.Text = cObj.CARGO_NUMBER;
                txtFromHubName.Text = cObj.F_HUBNAME;
                hdnFromHubID.Value = cObj.FROM_HUB_ID.ToString();
                txtToHubName.Text = cObj.T_HUBNAME;
                hdnToHubID.Value = cObj.TO_HUB_ID.ToString();
                txtContactPerson.Text = cObj.TRANS_CONTACT_PERSON;
                txtTransportMedia.Text = cObj.TRANS_MEDIA_NAME;
                hdnTransMediaID.Value = cObj.TRANS_MEDIA_ID.ToString();
                txtIssuedate.Text = Convert.ToDateTime(cObj.TRACK_DATE).ToString("dd-MMM-yyyy");
                txtMobileNo.Text = cObj.TRANS_CONTACT_NO;
                txtRemarks.Text = cObj.REMARKS;

                
            

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


            txtCargoNo.Enabled = isEnabled;
            txtFromHubName.Enabled = isEnabled;
            txtToHubName.Enabled = isEnabled;
            txtContactPerson.Enabled = isEnabled;
            txtTransportMedia.Enabled = isEnabled;
            txtIssuedate.Enabled = isEnabled;
            txtMobileNo.Enabled = isEnabled;
            txtRemarks.Enabled = isEnabled;


            
            //buttons
            btnAddNew.Visible = !isEnabled;
            btnEdit.Visible = !isEnabled;
            btnSave.Visible = isEnabled;
            //btnUpdate.Visible = !isEnabled;


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


        private bool CheckData()
        {
            bool status = true;
            errMsg = string.Empty;

            if (txtCargoNo.Text.Trim() == "")
            {
                ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Please Select Cargo !!');", true);
                txtCargoNo.Focus();
                return false;

            }

            if (txtFromHubName.Text.Trim() == "")
            {
                ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Please Select From Hub !!');", true);
                txtFromHubName.Focus();
                return false;

            }

            if (txtIssuedate.Text.Trim() == "")
            {
                ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Please Enter Issue Date !!');", true);
                txtIssuedate.Focus();
                return false;

            }
            


            return status;
        

        }
        private void SetHyperLink()
        {

            //new button
            string hLink = "javascript:tbopen(" + this.CARGO_TRACK_ID.ToString() + ")";
            if (base.PageMode == PG.Core.Web.PageModeEnum.InTab)
            {
                hLink = "javascript:tbopenSalInfo(" + this.CARGO_TRACK_ID.ToString() + ")";
                this.btnAddNew.Attributes.Add("onclick", hLink);
            }
            else
            {
                hLink = "~/HMS/CargoIssuedMst.aspx?id=" + this.CARGO_TRACK_ID.ToString();
                this.btnAddNew.Attributes.Add("onclick", hLink);
            }

        }

        //newly added comment
        private bool SaveData()
        {

            bool bStatus = false;

            bool isAdd = false;
            int newCARGO_TRACK_ID = 0;
            dcCARGO_TRACKING cObj = new dcCARGO_TRACKING();
            if (this.CARGO_TRACK_ID > 0)
            {
                cObj.CARGO_TRACK_ID = this.CARGO_TRACK_ID;
                cObj._RecordState = RecordStateEnum.Edited;
            }
            else
            {
                cObj._RecordState = RecordStateEnum.Added;
                isAdd = true;
            }

         
            cObj.CARGO_ID =Conversion.StringToInt( hdnCargoID.Value);
            cObj.TRACK_DATE =Conversion.StringToDate(txtIssuedate.Text.Trim());
            cObj.TRACK_BY = loggedinUser.UserID.ToString();
            cObj.FROM_HUB_ID = Conversion.StringToInt(hdnFromHubID.Value);
            cObj.TRANS_MEDIA_ID = Conversion.StringToInt(hdnTransMediaID.Value);
            cObj.TRANS_CONTACT_NO = txtMobileNo.Text.Trim();
            cObj.TRANS_CONTACT_PERSON = txtContactPerson.Text.Trim();
            cObj.REMARKS = txtRemarks.Text;
            cObj.TRANS_TYPE = "R";
            cObj.TO_HUB_ID = Conversion.StringToInt(hdnToHubID.Value);
            cObj.HUB_ID = Conversion.StringToInt(hdnFromHubID.Value);
            cObj.REF_TRANS_ID = Conversion.StringToInt(hdnCargoTrackingID.Value);
            

            if (isAdd)
            {
                cObj.CREATE_BY = loggedinUser.UserID.ToString();
                cObj.CREATE_DATE = DateTime.Now;

            }
            else
            {
                cObj.EDIT_BY = loggedinUser.UserID.ToString();
                cObj.EDIT_DATE = DateTime.Now;

            }

            newCARGO_TRACK_ID = CARGO_TRACKINGBL.Save(cObj);
            if (newCARGO_TRACK_ID > 0)
            {


                this.CARGO_TRACK_ID = newCARGO_TRACK_ID;
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

       


    }
}