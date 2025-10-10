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
    public partial class BranchEntry : BagePage
    {
        //this 
        string ViewStateKey = "BRANCH_ID";
        string ViewStateKeyPrev = "BRANCH_ID_PREV";
        ReportOpenTypeEnum ReportOpenType = ReportOpenTypeEnum.Preview;
        // int CompanyID = 0;

        int BRANCH_ID = 0;
        string saveMsg = string.Empty;
        string errMsg = string.Empty;

        private  dcUser loggedinUser = null;
        public string ReportViewPageLink = PageLinks.ReportLinks.GetLink_ReportView;
        public string ReportViewPDFPageLink = PageLinks.ReportLinks.GetLink_ReportViewPDF;
        public string ReportPrintPageLink = PageLinks.ReportLinks.GetLink_ReportPrint;
        public string ReportPDFPageLink = PageLinks.ReportLinks.GetLink_ReportPDF;


        public string EmployeeListServiceLink = PageLinks.InventoryLink.GetLink_EmployeeList;

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

            this.BRANCH_ID = base.GetPageQueryInteger("id");

            if (!IsPostBack) //first Time
            {

              
                hdnLoggedInUser.Value = loggedinUser.UserID.ToString();
                FillCombo();


                if (this.BRANCH_ID == 0) //not query string
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
                this.BRANCH_ID = int.Parse(ViewState[ViewStateKey].ToString());
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
            ReadData(this.BRANCH_ID);
            ViewState[ViewStateKey] = this.BRANCH_ID.ToString();

            SetControl(FormDataMode.Read);

        }
        private void AddTask()
        {
            ViewState[ViewStateKeyPrev] = this.BRANCH_ID.ToString();

            this.EditMode = FormDataMode.Add;
            this.IsDirty = false;
            this.BRANCH_ID = 0;
            ResetFormFields();
            ViewState[ViewStateKey] = "0";
            SetControl(FormDataMode.Add);
        }
        private void EditTask()
        {
            this.EditMode = FormDataMode.Edit;
            ReadData(this.BRANCH_ID);
            this.EditMode = FormDataMode.Edit;
            ViewState[ViewStateKey] = this.BRANCH_ID.ToString();
            SetControl(FormDataMode.Edit);
        }

        private void ResetFormFields()
        {
            txtBranchName.Text = string.Empty;
            txtBranchCode.Text = string.Empty;
            txtBranchHead.Text = string.Empty;
        
        }


        private bool ReadData(int id)
        {
            bool bStatus = false;
            byte[] bytes = null;
            dcBRANCH_MST cObj = BRANCH_MSTBL.GetBranchMstInfoById(id);
            if (cObj != null)
            {

                txtBranchName.Text = cObj.BRANCH_NAME;
                txtBranchCode.Text = cObj.BRANCH_CODE;
                txtBranchHead.Text = cObj.BRANCH_HEAD_NAME;
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


            SetTextBoxState(txtBranchName, isEnabled);
            SetTextBoxState(txtBranchCode, isEnabled);
            SetTextBoxState(txtBranchHead, isEnabled);
            ddlStatus.Enabled = isEnabled;
            ddlStatus.CssClass = "form-control form-control-sm";
            
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

            if (txtBranchName.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('error', 'Enter branch name!', 'Error');", true);
                txtBranchName.Focus();
                return false;

            }

            if (EditMode == FormDataMode.Add)
            {

                if (BRANCH_MSTBL.IsBranchNameExists(txtBranchName.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('error', 'Branch name already exists!', 'Error');", true);
                    txtBranchName.Focus();
                    return false;
                }



            }
            else if (EditMode == FormDataMode.Edit)
            {

                if (BRANCH_MSTBL.IsBranchNameExists(txtBranchName.Text.Trim(), this.BRANCH_ID))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('error', 'Branch name already exists!', 'Error');", true);
                    txtBranchName.Focus();
                    return false;
                }



            }


            return status;
        

        }
        private void SetHyperLink()
        {

            //new button
            string hLink = "javascript:tbopen(" + this.BRANCH_ID.ToString() + ")";
            if (base.PageMode == PG.Core.Web.PageModeEnum.InTab)
            {
                hLink = "javascript:tbopenSalInfo(" + this.BRANCH_ID.ToString() + ")";
                this.btnAddNew.Attributes.Add("onclick", hLink);
            }
            else
            {
                hLink = "~/WRL/BranchEntry.aspx?id=" + this.BRANCH_ID.ToString();
                this.btnAddNew.Attributes.Add("onclick", hLink);
            }

        }

        //newly added comment
        private bool SaveData()
        {

            bool bStatus = false;

            bool isAdd = false;
            int newBRANCH_ID = 0;
            dcBRANCH_MST cObj = new dcBRANCH_MST();
            if (this.BRANCH_ID > 0)
            {
                cObj.BRANCH_ID = this.BRANCH_ID;
                cObj._RecordState = RecordStateEnum.Edited;
            }
            else
            {
                cObj._RecordState = RecordStateEnum.Added;
                isAdd = true;
            }


            cObj.BRANCH_NAME = txtBranchName.Text.Trim();
            cObj.BRANCH_CODE = txtBranchCode.Text.Trim();
            cObj.BRANCH_HEAD = hdnEmpId.Value;
            cObj.IS_ACTIVE = ddlStatus.SelectedValue;
            

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

            newBRANCH_ID = BRANCH_MSTBL.Save(cObj);
            if (newBRANCH_ID > 0)
            {


                this.BRANCH_ID = newBRANCH_ID;
                ReadTask();
                bStatus = true;
               
            }

            return bStatus;
        }



        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            AddTask();
        }

       


    }
}