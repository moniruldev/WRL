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
    public partial class ClientEntry : BagePage
    {
        //this 
        string ViewStateKey = "CLIENT_ID";
        string ViewStateKeyPrev = "CLIENT_ID_PREV";
        ReportOpenTypeEnum ReportOpenType = ReportOpenTypeEnum.Preview;
        // int CompanyID = 0;

        int CLIENT_ID = 0;
        string saveMsg = string.Empty;
        string errMsg = string.Empty;

        private  dcUser loggedinUser = null;
        public string ReportViewPageLink = PageLinks.ReportLinks.GetLink_ReportView;
        public string ReportViewPDFPageLink = PageLinks.ReportLinks.GetLink_ReportViewPDF;
        public string ReportPrintPageLink = PageLinks.ReportLinks.GetLink_ReportPrint;
        public string ReportPDFPageLink = PageLinks.ReportLinks.GetLink_ReportPDF;



        public string CountryListServiceLink = PageLinks.InventoryLink.GetLink_CountryList;

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

            this.CLIENT_ID = base.GetPageQueryInteger("id");

            if (!IsPostBack) //first Time
            {

              
                hdnLoggedInUser.Value = loggedinUser.UserID.ToString();
                FillCombo();


                if (this.CLIENT_ID == 0) //not query string
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
                this.CLIENT_ID = int.Parse(ViewState[ViewStateKey].ToString());
            }

            SetHyperLink();

          
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
            ddlClientType.Items.Clear();
            ddlClientType.AppendDataBoundItems = true;
            ddlClientType.DataTextField = "TYPE_NAME";
            ddlClientType.DataValueField = "CLIENT_TYPE_ID";
            ddlClientType.DataSource = CLIENT_TYPE_MSTBL.GetCLIENT_TYPEList(clientType, null);
            ddlClientType.DataBind();
            ddlClientType.SelectedIndex = 0;

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


        }

        private void ReadTask()
        {
            this.EditMode = FormDataMode.Read;
            ReadData(this.CLIENT_ID);
            ViewState[ViewStateKey] = this.CLIENT_ID.ToString();

            SetControl(FormDataMode.Read);

        }
        private void AddTask()
        {
            ViewState[ViewStateKeyPrev] = this.CLIENT_ID.ToString();

            this.EditMode = FormDataMode.Add;
            this.IsDirty = false;
            this.CLIENT_ID = 0;
            ResetFormFields();
            ViewState[ViewStateKey] = "0";
            SetControl(FormDataMode.Add);
        }
        private void EditTask()
        {
            this.EditMode = FormDataMode.Edit;
            ReadData(this.CLIENT_ID);
            this.EditMode = FormDataMode.Edit;
            ViewState[ViewStateKey] = this.CLIENT_ID.ToString();
            SetControl(FormDataMode.Edit);
        }

        private void ResetFormFields()
        {
            txtClientName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtMobileNo.Text = string.Empty;
            txtRemarks.Text = string.Empty;
        }


        private bool ReadData(int id)
        {
            bool bStatus = false;
            byte[] bytes = null;
            dcCLIENT_MST cObj = CLIENT_MSTBL.GetCLIENT_MSTInfoById(id);
            if (cObj != null)
            {

                txtClientName.Text = cObj.CLIENT_NAME;
                ddlClientType.SelectedValue = cObj.CLIENT_TYPE_ID.ToString();
                txtAddress.Text = cObj.CLIENT_ADDRESS;
                txtMobileNo.Text = cObj.MOBILE_NO;
                txtRemarks.Text = cObj.REMARKS;
                ddlStatus.SelectedValue = cObj.IS_ACTIVE;
                
            

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


            txtClientName.Enabled = isEnabled;
            ddlClientType.Enabled = isEnabled;
            txtAddress.Enabled = isEnabled;
            txtMobileNo.Enabled = isEnabled;
            txtRemarks.Enabled = isEnabled;
            ddlStatus.Enabled = isEnabled;

            
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

            if (txtClientName.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Please Enter Client Name !!');", true);
                txtClientName.Focus();
                return false;

            }

            


            return status;
        

        }
        private void SetHyperLink()
        {

            //new button
            string hLink = "javascript:tbopen(" + this.CLIENT_ID.ToString() + ")";
            if (base.PageMode == PG.Core.Web.PageModeEnum.InTab)
            {
                hLink = "javascript:tbopenSalInfo(" + this.CLIENT_ID.ToString() + ")";
                this.btnAddNew.Attributes.Add("onclick", hLink);
            }
            else
            {
                hLink = "~/HMS/ClientTypeEntry.aspx?id=" + this.CLIENT_ID.ToString();
                this.btnAddNew.Attributes.Add("onclick", hLink);
            }

        }

        //newly added comment
        private bool SaveData()
        {

            bool bStatus = false;

            bool isAdd = false;
            int newCLIENT_ID = 0;
            dcCLIENT_MST cObj = new dcCLIENT_MST();
            if (this.CLIENT_ID > 0)
            {
                cObj.CLIENT_ID = this.CLIENT_ID;
                cObj._RecordState = RecordStateEnum.Edited;
            }
            else
            {
                cObj._RecordState = RecordStateEnum.Added;
                isAdd = true;
            }


            cObj.CLIENT_NAME = txtClientName.Text.Trim();
            cObj.CLIENT_TYPE_ID = Conversion.StringToInt(ddlClientType.SelectedValue);
            cObj.CLIENT_ADDRESS = txtAddress.Text.Trim();
            cObj.MOBILE_NO = txtMobileNo.Text.Trim();
            cObj.REMARKS = txtRemarks.Text.Trim();
            cObj.IS_ACTIVE = ddlStatus.SelectedValue;
            

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

            newCLIENT_ID = CLIENT_MSTBL.Save(cObj);
            if (newCLIENT_ID > 0)
            {


                this.CLIENT_ID = newCLIENT_ID;
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