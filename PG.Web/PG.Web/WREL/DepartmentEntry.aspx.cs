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
    public partial class DepartmentEntry : BagePage
    {
        //this 
        string ViewStateKey = "DEPT_ID";
        string ViewStateKeyPrev = "DEPT_ID_PREV";
        ReportOpenTypeEnum ReportOpenType = ReportOpenTypeEnum.Preview;
        // int CompanyID = 0;

        int DEPT_ID = 0;
        string saveMsg = string.Empty;
        string errMsg = string.Empty;

        private  dcUser loggedinUser = null;
        public string ReportViewPageLink = PageLinks.ReportLinks.GetLink_ReportView;
        public string ReportViewPDFPageLink = PageLinks.ReportLinks.GetLink_ReportViewPDF;
        public string ReportPrintPageLink = PageLinks.ReportLinks.GetLink_ReportPrint;
        public string ReportPDFPageLink = PageLinks.ReportLinks.GetLink_ReportPDF;


        public string ClientListServiceLink = PageLinks.InventoryLink.GetLink_ClientList;
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

            this.DEPT_ID = base.GetPageQueryInteger("id");

            if (!IsPostBack) //first Time
            {

              
                hdnLoggedInUser.Value = loggedinUser.UserID.ToString();
                FillCombo();


                if (this.DEPT_ID == 0) //not query string
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
                this.DEPT_ID = int.Parse(ViewState[ViewStateKey].ToString());
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
            //dcCLIENT_TYPE_MST clientType = new dcCLIENT_TYPE_MST();
            //clientType.IS_ACTIVE = "Y";
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


        }

        private void ReadTask()
        {
            this.EditMode = FormDataMode.Read;
            ReadData(this.DEPT_ID);
            ViewState[ViewStateKey] = this.DEPT_ID.ToString();

            SetControl(FormDataMode.Read);

        }
        private void AddTask()
        {
            ViewState[ViewStateKeyPrev] = this.DEPT_ID.ToString();

            this.EditMode = FormDataMode.Add;
            this.IsDirty = false;
            this.DEPT_ID = 0;
            ResetFormFields();
            ViewState[ViewStateKey] = "0";
            SetControl(FormDataMode.Add);
        }
        private void EditTask()
        {
            this.EditMode = FormDataMode.Edit;
            ReadData(this.DEPT_ID);
            this.EditMode = FormDataMode.Edit;
            ViewState[ViewStateKey] = this.DEPT_ID.ToString();
            SetControl(FormDataMode.Edit);
        }

        private void ResetFormFields()
        {
            txtDepartmentName.Text = string.Empty;
            txtContactPerson.Text = string.Empty;
            txtMobileNo.Text = string.Empty;
            txtRemarks.Text = string.Empty;
        }


        private bool ReadData(int id)
        {
            bool bStatus = false;
            byte[] bytes = null;
            dcDEPARTMENT_MST cObj = DEPARTMENT_MSTBL.GetDEPARTMENT_MSTInfoById(id);
            if (cObj != null)
            {

                txtDepartmentName.Text = cObj.DEPT_NAME;
                hdnClientId.Value = cObj.CLIENT_ID.ToString();
                txtClientName.Text = cObj.CLIENT_NAME;
                txtContactPerson.Text = cObj.CONTACT_PERSON;
                txtMobileNo.Text = cObj.CONTACT_NO;
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


            txtDepartmentName.Enabled = isEnabled;
            txtClientName.Enabled = isEnabled;
            txtContactPerson.Enabled = isEnabled;
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

            if (txtDepartmentName.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Please Enter Department Name !!');", true);
                txtDepartmentName.Focus();
                return false;

            }

            


            return status;
        

        }
        private void SetHyperLink()
        {

            //new button
            string hLink = "javascript:tbopen(" + this.DEPT_ID.ToString() + ")";
            if (base.PageMode == PG.Core.Web.PageModeEnum.InTab)
            {
                hLink = "javascript:tbopenSalInfo(" + this.DEPT_ID.ToString() + ")";
                this.btnAddNew.Attributes.Add("onclick", hLink);
            }
            else
            {
                hLink = "~/HMS/DepartmentEntry.aspx?id=" + this.DEPT_ID.ToString();
                this.btnAddNew.Attributes.Add("onclick", hLink);
            }

        }

        //newly added comment
        private bool SaveData()
        {

            bool bStatus = false;

            bool isAdd = false;
            int newDEPT_ID = 0;
            dcDEPARTMENT_MST cObj = new dcDEPARTMENT_MST();
            if (this.DEPT_ID > 0)
            {
                cObj.DEPT_ID = this.DEPT_ID;
                cObj._RecordState = RecordStateEnum.Edited;
            }
            else
            {
                cObj._RecordState = RecordStateEnum.Added;
                isAdd = true;
            }


            cObj.DEPT_NAME = txtDepartmentName.Text.Trim();
            cObj.CLIENT_ID = Conversion.StringToInt(hdnClientId.Value);
            cObj.CONTACT_PERSON = txtContactPerson.Text.Trim();
            cObj.CONTACT_NO = txtMobileNo.Text.Trim();
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

            newDEPT_ID = DEPARTMENT_MSTBL.Save(cObj);
            if (newDEPT_ID > 0)
            {


                this.DEPT_ID = newDEPT_ID;
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