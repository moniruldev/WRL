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
using PG.BLLibrary.SecurityBL;

namespace PG.Web.Admin
{
    public partial class RoleEntry : BagePage
    {
        //this 
        string ViewStateKey = "RoleID";
        string ViewStateKeyPrev = "RoleID";
        ReportOpenTypeEnum ReportOpenType = ReportOpenTypeEnum.Preview;
        // int CompanyID = 0;

        int RoleID = 0;
        string saveMsg = string.Empty;
        string errMsg = string.Empty;

        private  dcUser loggedinUser = null;
        public string ReportViewPageLink = PageLinks.ReportLinks.GetLink_ReportView;
        public string ReportViewPDFPageLink = PageLinks.ReportLinks.GetLink_ReportViewPDF;
        public string ReportPrintPageLink = PageLinks.ReportLinks.GetLink_ReportPrint;
        public string ReportPDFPageLink = PageLinks.ReportLinks.GetLink_ReportPDF;

        public string ClientListServiceLink = PageLinks.InventoryLink.GetLink_ClientList;

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

            this.RoleID = base.GetPageQueryInteger("id");

            if (!IsPostBack) //first Time
            {

              
                hdnLoggedInUser.Value = loggedinUser.RoleID.ToString();
                FillCombo();


                if (this.RoleID == 0) //not query string
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
                this.RoleID = int.Parse(ViewState[ViewStateKey].ToString());
            }

            SetHyperLink();

          
            //this.ShowPageMessage(this.lblMessage);
            // Response.Write("RoleID : " + this.RoleID.ToString());

        }
     
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            EditTask();
        }

        private void FillCombo()
        {

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
            ReadData(this.RoleID);
            ViewState[ViewStateKey] = this.RoleID.ToString();

            SetControl(FormDataMode.Read);

        }
        private void AddTask()
        {
            ViewState[ViewStateKeyPrev] = this.RoleID.ToString();

            this.EditMode = FormDataMode.Add;
            this.IsDirty = false;
            this.RoleID = 0;
            ViewState[ViewStateKey] = "0";
            SetControl(FormDataMode.Add);
            ClearText();
        }
        private void EditTask()
        {
            this.EditMode = FormDataMode.Edit;
            ReadData(this.RoleID);
            this.EditMode = FormDataMode.Edit;
            ViewState[ViewStateKey] = this.RoleID.ToString();
            
            SetControl(FormDataMode.Edit);
        }

        private void ClearText()
        {
            txtRoleName.Text = string.Empty;
            txtDescription.Text = string.Empty;
            ddlStatus.SelectedValue = "Y";   

        }

        private bool ReadData(int id)
        {
            bool bStatus = false;
            dcRole cObj = RoleBL.GetRoleByRoleID(id);
            if (cObj != null)
            {

                txtRoleName.Text = cObj.RoleName;
                txtDescription.Text = cObj.RoleDesc;
                ddlApp.SelectedValue = cObj.AppID.ToString();
                ddlIsSystem.SelectedValue = cObj.IsSystem ? "Y" : "N";
                ddlIsAdmin.SelectedValue = cObj.IsAdmin ? "Y" : "N";
                ddlStatus.SelectedValue = cObj.IsActive ? "Y" : "N";
                if (cObj.RoleID == 1 | cObj.RoleName.ToUpper() == "ADMINS")
                {
                    txtRoleName.Enabled = false;
                }
                else
                {
                    txtRoleName.Enabled = true;
                }
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


            SetTextBoxState(txtRoleName, isEnabled);
            SetTextBoxState(txtDescription, isEnabled);
          
          
            ddlStatus.Enabled = isEnabled;
            ddlStatus.CssClass = "form-control form-control-sm";
            ddlApp.Enabled = isEnabled;
            ddlApp.CssClass = "form-control form-control-sm";
            ddlIsSystem.Enabled = isEnabled;
            ddlIsSystem.CssClass = "form-control form-control-sm";
            ddlIsAdmin.Enabled = isEnabled;
            ddlIsAdmin.CssClass = "form-control form-control-sm";
            
            //buttons
            btnAddNew.Visible = !isEnabled;
            btnEdit.Visible = !isEnabled;
            btnSave.Visible = isEnabled;
            //btnUpdate.Visible = !isEnabled;


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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('success', 'Data Saved Successfully!', 'Success');", true);

                }
                else
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('error', ' Data not Saved!', 'Error');", true);
                }

            }
            else
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('error', ' Data not Saved!', 'Error');", true);
            }

            return true;

        }


        private bool CheckData()
        {
            bool status = true;
            errMsg = string.Empty;


            if (txtRoleName.Text.Trim() == string.Empty)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('error', 'Enter Role Name!', 'Error');", true);
                txtRoleName.Focus();
                return false;
            }


            if (EditMode == FormDataMode.Add)
            {


                if (RoleBL.IsRoleExists(Conversion.StringToInt(ddlApp.SelectedValue), txtRoleName.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('error', 'Role Name Already Exists', 'Error');", true);
                    return false;

                }

               
            }

            if (EditMode == FormDataMode.Edit)
            {


                if (RoleBL.IsRoleExists(Conversion.StringToInt(ddlApp.SelectedValue), txtRoleName.Text.Trim(),this.RoleID))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('error', 'Role Name Already Exists', 'Error');", true);
                    return false;

                }


            }
           

            


            return status;
        

        }
        private void SetHyperLink()
        {

            //new button
            string hLink = "javascript:tbopen(" + this.RoleID.ToString() + ")";
            if (base.PageMode == PG.Core.Web.PageModeEnum.InTab)
            {
                hLink = "javascript:tbopenSalInfo(" + this.RoleID.ToString() + ")";
                this.btnAddNew.Attributes.Add("onclick", hLink);
            }
            else
            {
                hLink = "~/Admin/RoleEntry.aspx?id=" + this.RoleID.ToString();
                this.btnAddNew.Attributes.Add("onclick", hLink);
            }

        }

        //newly added comment
        private bool SaveData()
        {

            bool bStatus = false;

            bool isAdd = false;
            int newRoleID = 0;
            dcRole cObj = new dcRole();
            if (this.RoleID > 0)
            {
                cObj.RoleID = this.RoleID;
                cObj._RecordState = RecordStateEnum.Edited;
            }
            else
            {
                cObj._RecordState = RecordStateEnum.Added;
                isAdd = true;
            }

            cObj.AppID = Conversion.StringToInt(ddlApp.SelectedValue);
            cObj.RoleName = txtRoleName.Text;
            cObj.RoleDesc = txtDescription.Text;
            cObj.IsVisible = true;
            cObj.RoleCreateDt = DateTime.Now;
            cObj.IsSystem = ddlIsSystem.SelectedValue == "Y" ? true : false;
            cObj.IsAdmin = ddlIsSystem.SelectedValue == "Y" ? true : false;
            cObj.IsActive = ddlStatus.SelectedValue == "Y" ? true : false;
           

            if (isAdd)
            {
              
                newRoleID = RoleBL.Insert(cObj);
            }
            else
            {
                bStatus = RoleBL.Update(cObj);
                newRoleID = cObj.RoleID;
            }

          
            if (newRoleID > 0)
            {
                this.RoleID = newRoleID;
                ReadTask();
                bStatus = true;
            }

            return bStatus;
        }



        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            AddTask();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearText();
        }

       

       


    }
}