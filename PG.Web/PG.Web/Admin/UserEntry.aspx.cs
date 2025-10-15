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
    public partial class UserEntry : BagePage
    {
        //this 
        string ViewStateKey = "UserID";
        string ViewStateKeyPrev = "UserID";
        ReportOpenTypeEnum ReportOpenType = ReportOpenTypeEnum.Preview;
        // int CompanyID = 0;

        int UserID = 0;
        string saveMsg = string.Empty;
        string errMsg = string.Empty;

        private  dcUser loggedinUser = null;
        public string ReportViewPageLink = PageLinks.ReportLinks.GetLink_ReportView;
        public string ReportViewPDFPageLink = PageLinks.ReportLinks.GetLink_ReportViewPDF;
        public string ReportPrintPageLink = PageLinks.ReportLinks.GetLink_ReportPrint;
        public string ReportPDFPageLink = PageLinks.ReportLinks.GetLink_ReportPDF;

        public string ClientListServiceLink = PageLinks.InventoryLink.GetLink_ClientList;
        public string DeliveryManlistServiceLink = PageLinks.InventoryLink.GetLink_DeliveryManList;
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

            this.UserID = base.GetPageQueryInteger("id");

            if (!IsPostBack) //first Time
            {

              
                hdnLoggedInUser.Value = loggedinUser.UserID.ToString();
                FillCombo();


                if (this.UserID == 0) //not query string
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
                this.UserID = int.Parse(ViewState[ViewStateKey].ToString());
            }

            SetHyperLink();

          
            //this.ShowPageMessage(this.lblMessage);
            // Response.Write("UserID : " + this.UserID.ToString());

        }
     
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            EditTask();
        }

        private void FillCombo()
        {

            ddlRole.Items.Clear();
            ddlRole.Items.Add("(select role)");

            ddlRole.DataTextField = "RoleName";
            ddlRole.DataValueField = "RoleID";
            ddlRole.AppendDataBoundItems = true;
            ddlRole.DataSource = AppSecurity.GetRoleList(0).Where(x=>x.IsActive).ToList();
            ddlRole.DataBind();

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
            ReadData(this.UserID);
            ViewState[ViewStateKey] = this.UserID.ToString();

            SetControl(FormDataMode.Read);

        }
        private void AddTask()
        {
            ViewState[ViewStateKeyPrev] = this.UserID.ToString();

            this.EditMode = FormDataMode.Add;
            this.IsDirty = false;
            this.UserID = 0;
            ViewState[ViewStateKey] = "0";
            SetControl(FormDataMode.Add);
            ClearText();
        }
        private void EditTask()
        {
            this.EditMode = FormDataMode.Edit;
            ReadData(this.UserID);
            this.EditMode = FormDataMode.Edit;
            ViewState[ViewStateKey] = this.UserID.ToString();
            
            SetControl(FormDataMode.Edit);
        }

        private void ClearText()
        {
            txtUserName.Text = string.Empty;
            txtFullName.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtConfirmPassword.Text = string.Empty;
            txtClientName.Text = string.Empty;
            txtDeliveryMan.Text = string.Empty;
            txtAgentName.Text = string.Empty;
            ddlStatus.SelectedValue = "Y";   
            hdnClientId.Value = string.Empty;

        }

        private bool ReadData(int id)
        {
            bool bStatus = false;
            dcUser cObj = UserBL.GetAllUserList(0,0,id,"0",null).FirstOrDefault();
            if (cObj != null)
            {

                txtUserName.Text = cObj.UserName;
                txtFullName.Text = cObj.FullName;
                //txtPassword.Text = cObj.Password;
                //txtConfirmPassword.Text = cObj.Password;
                ddlUserType.SelectedValue = string.IsNullOrEmpty(cObj.UserType) ? "0" : cObj.UserType;
                if(cObj.UserType == "CLIENT")
                {
                    txtClientName.Text = cObj.CLIENT_NAME.ToString();
                    hdnClientId.Value = cObj.CLIENT_ID.ToString();
                }
                if (cObj.UserType == "DELIVERYMAN")
                {
                    txtDeliveryMan.Text = cObj.DELIVERY_MAN_NAME.ToString();
                    hdnDeliveryManID.Value = cObj.DELIVERY_MAN_ID.ToString();
                }
                if (cObj.UserType == "AGENT")
                {
                    txtAgentName.Text = cObj.AGENT_NAME.ToString();
                    hdnAgentId.Value = cObj.AGENT_ID.ToString();
                }
                ddlApp.SelectedValue = cObj.AppID.ToString();
                ddlRole.SelectedIndex = Helper.FindIndexByValue(ddlRole, cObj.RoleID.ToString());
                ddlStatus.SelectedValue = cObj.IsActive ? "Y" : "N";

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


            SetTextBoxState(txtUserName, isEnabled);
            SetTextBoxState(txtFullName, isEnabled);
          
            if (dataMode == FormDataMode.Add)
            {
             
                SetTextBoxState(txtUserName, true);
                SetTextBoxState(txtPassword, true);
                SetTextBoxState(txtConfirmPassword, true);
            }
            else
            {
                SetTextBoxState(txtPassword, false);
                SetTextBoxState(txtConfirmPassword, false);
                txtUserName.Attributes.Add("readonly", "readonly");
                ddlStatus.CssClass = "form-control form-control-sm";
            }
          
            SetTextBoxState(txtClientName, isEnabled);
            SetTextBoxState(txtAgentName, isEnabled);
            SetTextBoxState(txtDeliveryMan, isEnabled);
            ddlStatus.Enabled = isEnabled;
            ddlStatus.CssClass = "form-control form-control-sm";
            ddlRole.Enabled = isEnabled;
            ddlRole.CssClass = "form-control form-control-sm";
            ddlUserType.Enabled = isEnabled;
            ddlUserType.CssClass = "form-control form-control-sm";
            ddlApp.Enabled = isEnabled;
            ddlApp.CssClass = "form-control form-control-sm";
            txtUserName.Enabled = isEnabled;
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


            if (txtUserName.Text.Trim() == string.Empty)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('error', 'Enter User Name!', 'Error');", true);
                txtUserName.Focus();
                return false;
            }


            if (ddlRole.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('error', 'Please Select Role!', 'Error');", true);
                ddlRole.Focus();
                return false;
            }
            if(ddlUserType.SelectedValue == "CLIENT")
            {
                if(hdnClientId.Value == "0")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('error', 'Please Select Client', 'Error');", true);
                    return false;

                }
            }

            if (ddlUserType.SelectedValue == "DELIVERYMAN")
            {
                if (hdnDeliveryManID.Value == "0")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('error', 'Please Select Delivery Man', 'Error');", true);
                    return false;

                }
            }

            if (ddlUserType.SelectedValue == "AGENT")
            {
                if (hdnAgentId.Value == "0")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('error', 'Please Select Agent', 'Error');", true);
                    return false;

                }
            }

            if (EditMode == FormDataMode.Add)
            {


                if (UserBL.IsUserExists(Conversion.StringToInt(ddlApp.SelectedValue), txtUserName.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('error', 'User Name Already Exists', 'Error');", true);
                    return false;

                }

                if (txtPassword.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('error', 'Please Enter Password', 'Error');", true);
                    return false;
                }

                if (txtPassword.Text != txtConfirmPassword.Text)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('error', 'Password does not Matched', 'Error');", true);
                    return false;
                }
            }
            else if (EditMode == FormDataMode.Edit)
            {
                if (UserBL.IsUserExists(Conversion.StringToInt(ddlApp.SelectedValue), txtUserName.Text.Trim(),this.UserID))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('error', 'User Name Already Exists', 'Error');", true);
                    return false;

                }
            }

            


            return status;
        

        }
        private void SetHyperLink()
        {

            //new button
            string hLink = "javascript:tbopen(" + this.UserID.ToString() + ")";
            if (base.PageMode == PG.Core.Web.PageModeEnum.InTab)
            {
                hLink = "javascript:tbopenSalInfo(" + this.UserID.ToString() + ")";
                this.btnAddNew.Attributes.Add("onclick", hLink);
            }
            else
            {
                hLink = "~/WREL/DeliveryManEntry.aspx?id=" + this.UserID.ToString();
                this.btnAddNew.Attributes.Add("onclick", hLink);
            }

        }

        //newly added comment
        private bool SaveData()
        {

            bool bStatus = false;

            bool isAdd = false;
            int newUserID = 0;
            dcUser cObj = new dcUser();
            if (this.UserID > 0)
            {
                cObj.UserID = this.UserID;
                cObj._RecordState = RecordStateEnum.Edited;
            }
            else
            {
                cObj._RecordState = RecordStateEnum.Added;
                isAdd = true;
            }

            cObj.AppID = Conversion.StringToInt(ddlApp.SelectedValue);
            cObj.UserName = txtUserName.Text;
            cObj.RoleID = Convert.ToInt32(ddlRole.SelectedValue);
            cObj.FullName = txtFullName.Text;
            cObj.Email = "";
            cObj.UserType = ddlUserType.SelectedValue;
            cObj.CLIENT_ID =Conversion.StringToInt(hdnClientId.Value);
            cObj.DELIVERY_MAN_ID = Conversion.StringToInt(hdnDeliveryManID.Value);
            cObj.AGENT_ID = Conversion.StringToInt(hdnAgentId.Value);
            cObj.IsActive = ddlStatus.SelectedValue == "Y" ? true : false;
           

            if (isAdd)
            {
                cObj.Password = txtPassword.Text.Trim();
                cObj.UserCreateDt = DateTime.Now;
                newUserID = UserBL.Insert(cObj);
            }
            else
            {
                bStatus = UserBL.Update(cObj);
                newUserID = cObj.UserID;
            }

          
            if (newUserID > 0)
            {
                this.UserID = newUserID;
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

        protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlUserType.SelectedValue == "CLIENT")
            {
                dvClient.Visible = true;
            }
            else
            {
                dvClient.Visible = false;
            }

            if (ddlUserType.SelectedValue == "DELIVERYMAN")
            {
                dvDeliveryman.Visible = true;
            }
            else
            {
                dvDeliveryman.Visible = false;
            }

            if (ddlUserType.SelectedValue == "AGENT")
            {
                dvAgent.Visible = true;
            }
            else
            {
                dvAgent.Visible = false;
            }
            
        }


        protected void ddlApp_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ddlRole.Items.Clear();
            ddlRole.Items.Add("(select role)");

            ddlRole.DataTextField = "RoleName";
            ddlRole.DataValueField = "RoleID";
            ddlRole.AppendDataBoundItems = true;
            ddlRole.DataSource = AppSecurity.GetRoleList(0).Where(x => x.IsActive).ToList();
            ddlRole.DataBind();
           
        }
    }
}