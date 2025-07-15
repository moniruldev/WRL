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
    public partial class AgentEntry : BagePage
    {
        //this 
        string ViewStateKey = "AGENT_ID";
        string ViewStateKeyPrev = "AGENT_ID";
        ReportOpenTypeEnum ReportOpenType = ReportOpenTypeEnum.Preview;
        // int CompanyID = 0;

        int AGENT_ID = 0;
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

            this.AGENT_ID = base.GetPageQueryInteger("id");

            if (!IsPostBack) //first Time
            {

              
                hdnLoggedInUser.Value = loggedinUser.UserID.ToString();
                //FillCombo();


                if (this.AGENT_ID == 0) //not query string
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
                this.AGENT_ID = int.Parse(ViewState[ViewStateKey].ToString());
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
            //ddlRoomType.Items.Clear();
            //ddlRoomType.AppendDataBoundItems = true;
            //ddlRoomType.DataTextField = "TITLE";
            //ddlRoomType.DataValueField = "ROOM_TYPE_ID";
            //ddlRoomType.DataSource = HMROOM_TYPEBL.GetRoomTypeList();
            //ddlRoomType.DataBind();
            //ddlRoomType.SelectedIndex = 0;

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
            ReadData(this.AGENT_ID);
            ViewState[ViewStateKey] = this.AGENT_ID.ToString();

            SetControl(FormDataMode.Read);

        }
        private void AddTask()
        {
            ViewState[ViewStateKeyPrev] = this.AGENT_ID.ToString();

            this.EditMode = FormDataMode.Add;
            this.IsDirty = false;
            this.AGENT_ID = 0;
            ViewState[ViewStateKey] = "0";
            SetControl(FormDataMode.Add);
            ClearText();
        }
        private void EditTask()
        {
            this.EditMode = FormDataMode.Edit;
            ReadData(this.AGENT_ID);
            this.EditMode = FormDataMode.Edit;
            ViewState[ViewStateKey] = this.AGENT_ID.ToString();
            
            SetControl(FormDataMode.Edit);
        }

        private void ClearText()
        {
            txtCompayName.Text = string.Empty;
            txtOwner.Text = string.Empty;
            txtOwnercontact.Text = string.Empty;
            txtContactPerson.Text = string.Empty;
            txtContactNo.Text = string.Empty;
            txtBankName.Text = string.Empty;
            txtBranch.Text = string.Empty;
            txtAccountNo.Text = string.Empty;
            this.AGENT_ID = 0;
           // ddlStatus.SelectedValue = "";
        }

        private bool ReadData(int id)
        {
            bool bStatus = false;
            byte[] bytes = null;
            dcAGENT_MST cObj = AGENT_MSTBL.GetAgentInfoById(id);
            if (cObj != null)
            {

                txtCompayName.Text = cObj.AGENT_COMPANY_NAME;
                txtOwner.Text = cObj.OWNER_NAME;
                txtOwnercontact.Text = cObj.OWNER_MOBILE_NO;
                txtContactPerson.Text = cObj.CONTACT_PERSON;
                txtContactNo.Text = cObj.CONTACT_MOBILE_NO;
                txtBankName.Text = cObj.BANK_NAME;
                txtBranch.Text = cObj.BRANCH;
                txtAccountNo.Text = cObj.ACCOUNT_NO;
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


            txtCompayName.Enabled = isEnabled;
            txtOwner.Enabled = isEnabled;
            ddlStatus.Enabled = isEnabled;
            txtOwnercontact.Enabled = isEnabled;
            txtContactPerson.Enabled = isEnabled;
            txtContactNo.Enabled = isEnabled;
            txtBankName.Enabled = isEnabled;
            txtAccountNo.Enabled = isEnabled;
            txtBranch.Enabled = isEnabled;
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

            if (txtCompayName.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Please Enter Company Name !!');", true);
                txtCompayName.Focus();
                return false;

            }

            


            return status;
        

        }
        private void SetHyperLink()
        {

            //new button
            string hLink = "javascript:tbopen(" + this.AGENT_ID.ToString() + ")";
            if (base.PageMode == PG.Core.Web.PageModeEnum.InTab)
            {
                hLink = "javascript:tbopenSalInfo(" + this.AGENT_ID.ToString() + ")";
                this.btnAddNew.Attributes.Add("onclick", hLink);
            }
            else
            {
                hLink = "~/HMS/AgentEntry.aspx?id=" + this.AGENT_ID.ToString();
                this.btnAddNew.Attributes.Add("onclick", hLink);
            }

        }

        //newly added comment
        private bool SaveData()
        {

            bool bStatus = false;

            bool isAdd = false;
            int newAGENT_ID = 0;
            dcAGENT_MST cObj = new dcAGENT_MST();
            if (this.AGENT_ID > 0)
            {
                cObj.AGENT_ID = this.AGENT_ID;
                cObj._RecordState = RecordStateEnum.Edited;
            }
            else
            {
                cObj._RecordState = RecordStateEnum.Added;
                isAdd = true;
            }

            cObj.AGENT_COMPANY_NAME = txtCompayName.Text.Trim();
            cObj.OWNER_NAME = txtOwner.Text.Trim();
            cObj.OWNER_MOBILE_NO = txtOwnercontact.Text.Trim();
            cObj.CONTACT_PERSON = txtContactPerson.Text.Trim();
            cObj.CONTACT_MOBILE_NO = txtContactNo.Text.Trim();
            cObj.BANK_NAME = txtBankName.Text.Trim();
            cObj.BRANCH = txtBranch.Text.Trim();
            cObj.ACCOUNT_NO = txtAccountNo.Text.Trim();
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

            newAGENT_ID = AGENT_MSTBL.Save(cObj);
            if (newAGENT_ID > 0)
            {


                this.AGENT_ID = newAGENT_ID;
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