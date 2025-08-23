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
    public partial class DeliveryManEntry : BagePage
    {
        //this 
        string ViewStateKey = "DELIVERY_MAN_ID";
        string ViewStateKeyPrev = "DELIVERY_MAN_ID";
        ReportOpenTypeEnum ReportOpenType = ReportOpenTypeEnum.Preview;
        // int CompanyID = 0;

        int DELIVERY_MAN_ID = 0;
        string saveMsg = string.Empty;
        string errMsg = string.Empty;

        private  dcUser loggedinUser = null;
        public string ReportViewPageLink = PageLinks.ReportLinks.GetLink_ReportView;
        public string ReportViewPDFPageLink = PageLinks.ReportLinks.GetLink_ReportViewPDF;
        public string ReportPrintPageLink = PageLinks.ReportLinks.GetLink_ReportPrint;
        public string ReportPDFPageLink = PageLinks.ReportLinks.GetLink_ReportPDF;

        public string AgentListServiceLink = PageLinks.InventoryLink.GetLink_AgentList;

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

            this.DELIVERY_MAN_ID = base.GetPageQueryInteger("id");

            if (!IsPostBack) //first Time
            {

              
                hdnLoggedInUser.Value = loggedinUser.UserID.ToString();
                FillCombo();


                if (this.DELIVERY_MAN_ID == 0) //not query string
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
                this.DELIVERY_MAN_ID = int.Parse(ViewState[ViewStateKey].ToString());
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
            //ddlHubType.Items.Clear();
            //ddlHubType.AppendDataBoundItems = true;
            //ddlHubType.DataTextField = "HUB_TYPE_NAME";
            //ddlHubType.DataValueField = "HUB_TYPE_ID";
            //ddlHubType.DataSource = HUB_TYPE_MSTBL.GetHUB_TYPEComboList();
            //ddlHubType.DataBind();
            //ddlHubType.SelectedIndex = 0;

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
            ReadData(this.DELIVERY_MAN_ID);
            ViewState[ViewStateKey] = this.DELIVERY_MAN_ID.ToString();

            SetControl(FormDataMode.Read);

        }
        private void AddTask()
        {
            ViewState[ViewStateKeyPrev] = this.DELIVERY_MAN_ID.ToString();

            this.EditMode = FormDataMode.Add;
            this.IsDirty = false;
            this.DELIVERY_MAN_ID = 0;
            ViewState[ViewStateKey] = "0";
            SetControl(FormDataMode.Add);
            ClearText();
        }
        private void EditTask()
        {
            this.EditMode = FormDataMode.Edit;
            ReadData(this.DELIVERY_MAN_ID);
            this.EditMode = FormDataMode.Edit;
            ViewState[ViewStateKey] = this.DELIVERY_MAN_ID.ToString();
            
            SetControl(FormDataMode.Edit);
        }

        private void ClearText()
        {
            txtName.Text = string.Empty;
            txtFathersName.Text = string.Empty;
            txtMothersName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtMobileNo.Text = string.Empty;
            txtAgent.Text = string.Empty;
            ddlStatus.SelectedValue = "Y";   
            hdnAgentId.Value = string.Empty;

        }

        private bool ReadData(int id)
        {
            bool bStatus = false;
            byte[] bytes = null;
            dcDELIVERY_MAN_MST cObj = DELIVERY_MAN_MSTBL.GetDeliveryManInfoById(id);
            if (cObj != null)
            {

                txtName.Text = cObj.DELIVERY_MAN_NAME;
                txtFathersName.Text = cObj.FATHER_NAME;
                txtMothersName.Text = cObj.MOTHER_NAME;
                txtAddress.Text = cObj.ADDRESS;
                txtMobileNo.Text = cObj.MOBILE_NO;
                txtAgent.Text = cObj.AGENT_NAME;
                ddlStatus.SelectedValue = cObj.IS_ACTIVE;
                hdnAgentId.Value = cObj.AGENT_ID.ToString();

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


            SetTextBoxState(txtName, isEnabled);
            SetTextBoxState(txtFathersName, isEnabled);
            SetTextBoxState(txtMothersName, isEnabled);
            SetTextBoxState(txtAddress, isEnabled);
            if(dataMode == FormDataMode.Add)
            {
                SetTextBoxState(txtMobileNo, true);
            }
            else
            {
                SetTextBoxState(txtMobileNo, false);
            }
          
            SetTextBoxState(txtAgent, isEnabled);
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

            if (txtName.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('error', 'Enter  name!', 'Error');", true);
                txtName.Focus();
                return false;

            }

            if (txtMobileNo.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('error', 'Enter mobile number!', 'Error');", true);
                txtMobileNo.Focus();
                return false;

            }

            if(DELIVERY_MAN_MSTBL.IsMobileNumberExists(txtMobileNo.Text,this.DELIVERY_MAN_ID,null))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('error', 'This mobile number already in used!', 'Error');", true);
                txtMobileNo.Focus();
                return false;
            }

            


            return status;
        

        }
        private void SetHyperLink()
        {

            //new button
            string hLink = "javascript:tbopen(" + this.DELIVERY_MAN_ID.ToString() + ")";
            if (base.PageMode == PG.Core.Web.PageModeEnum.InTab)
            {
                hLink = "javascript:tbopenSalInfo(" + this.DELIVERY_MAN_ID.ToString() + ")";
                this.btnAddNew.Attributes.Add("onclick", hLink);
            }
            else
            {
                hLink = "~/WREL/DeliveryManEntry.aspx?id=" + this.DELIVERY_MAN_ID.ToString();
                this.btnAddNew.Attributes.Add("onclick", hLink);
            }

        }

        //newly added comment
        private bool SaveData()
        {

            bool bStatus = false;

            bool isAdd = false;
            int newDELIVERY_MAN_ID = 0;
            dcDELIVERY_MAN_MST cObj = new dcDELIVERY_MAN_MST();
            if (this.DELIVERY_MAN_ID > 0)
            {
                cObj.DELIVERY_MAN_ID = this.DELIVERY_MAN_ID;
                cObj._RecordState = RecordStateEnum.Edited;
            }
            else
            {
                cObj._RecordState = RecordStateEnum.Added;
                isAdd = true;
            }

            cObj.DELIVERY_MAN_NAME = txtName.Text.Trim();
            cObj.ADDRESS = txtAddress.Text.Trim();
            cObj.MOBILE_NO = txtMobileNo.Text.Trim();
            cObj.FATHER_NAME = txtFathersName.Text.Trim();
            cObj.MOTHER_NAME = txtMothersName.Text.Trim();
            cObj.AGENT_ID = Conversion.StringToInt(hdnAgentId.Value);
            if(cObj.AGENT_ID > 0)
            {
                cObj.IS_UNDER_AGENT = "Y";
            }
            else
            {
                cObj.IS_UNDER_AGENT = "N";

            }
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

            newDELIVERY_MAN_ID = DELIVERY_MAN_MSTBL.Save(cObj);
            if (newDELIVERY_MAN_ID > 0)
            {


                this.DELIVERY_MAN_ID = newDELIVERY_MAN_ID;
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