using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;

using PG.Core;
using PG.Core.Web;

using PG.DBClass.SecurityDC;
using PG.BLLibrary.SecurityBL;

namespace PG.Web.Admin
{
    public partial class User : BagePage
    {
        int UserID = 0;
        string ViewStateKey = "UserID";
        string EditModeKey = "EditMode";
        string saveMsg = string.Empty;

        protected override void OnPreInit(EventArgs e)
        {
            if (AdminGlobals.AdminMasterPage != string.Empty)
            {
                this.MasterPageFile = AdminGlobals.AdminMasterPage;
            }
            base.OnPreInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            base.AppObjectID = AppObjectEnum.Frm_User;
            base.CheckPermissionRead();

            
            //base.RestrictByPageInTab();

            //ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(this.LinkButton1);
            this.UserID = base.GetPageQueryInteger("id");

            if (!IsPostBack) //first Time
            {
                FillCombo();
                if (this.UserID == 0) //not query string
                {
                    AddTask();
                }
                else
                {
                    EditTask();
                    //if (Session["MsgSaveStatus"] != null)
                    //{
                    //    string sMsg = Session["MsgSaveStatus"].ToString();
                    //    lblMessage.Text = sMsg.ToString();
                    //    Session["MsgSaveStatus"] = null;
                    //}
                }

            }
            else
            {
                this.EditMode = base.GetEditModeFromViewState(EditModeKey);
                this.UserID = int.Parse(ViewState[ViewStateKey].ToString());
            }
            this.ShowPageMessage(this.lblMessage);

           // Response.Write("UserID : " + this.UserID.ToString());
        }

        private void FillCombo()
        {

            ddlRole.Items.Clear();
            ddlRole.Items.Add("(select role)");

            ddlRole.DataTextField = "RoleName";
            ddlRole.DataValueField = "RoleID";
            ddlRole.AppendDataBoundItems = true;
            ddlRole.DataSource = AppSecurity.GetRoleList(AppInfo.AppID);
            ddlRole.DataBind();

        }

        private void AddTask()
        {
            txtUserID.Text = "";
            txtFullName.Text = "";
            txtPassword.Text = "";
            txtPassword2.Text = "";
            ddlIsActive.SelectedValue = "1";
            ViewState[EditModeKey] = (int)FormDataMode.Add;
            ViewState[ViewStateKey] = "0";

            this.EditMode = FormDataMode.Add;
            //lblMode.Text = "Mode: Add";
            lblHeader.Text = "User : New User";
            txtUserID.Enabled = true;
            txtUserID.Focus();

            txtPassword.Visible = true;
            lblPassword2.Visible = true;
            txtPassword2.Visible = true;

            CompareValidator1.Enabled = true;
            CompareValidator1.Visible = false;

            lnkSetPass.Visible = false;
            Helper.SetStatusMessage(lblMessage, string.Empty, MessageTypeEnum.None);
        }

        private void EditTask()
        {
            ViewState[EditModeKey] = (int)FormDataMode.Edit;
            this.EditMode = FormDataMode.Edit;
            //lblMode.Text = "Mode: Edit";
            lblHeader.Text = "User: Edit";
            //this.SetMasterHeader("Edit User");
            ReadData(this.UserID);
            txtUserID.Enabled = false;
            ViewState[ViewStateKey] = this.UserID.ToString();
            //lnkAddNew.Visible = true;
            txtFullName.Focus();

            txtPassword.Visible = false;
            lblPassword2.Visible = false;
            txtPassword2.Visible = false;
            CompareValidator1.Visible = false;
            CompareValidator1.Enabled = false;
            lnkSetPass.Visible = true;
           
        }

        private bool ReadData(int pUserID)
        {
            dcUser cObj = UserBL.GetUserByUserID(pUserID);

            bool bStatus;
            if (cObj != null)
            {
                txtUserID.Text = cObj.UserName;
                txtFullName.Text = cObj.FullName;
                txtPassword.Text = cObj.Password;
                txtPassword2.Text = cObj.Password;
                ddlRole.SelectedIndex = Helper.FindIndexByValue(ddlRole, cObj.RoleID.ToString());
                //txtEmail.Text = cObj.Email;
                ddlIsActive.SelectedValue = cObj.IsActive ? "1" : "0";

                //safeguard password
                ViewState["password"] = cObj.Password;

                if (cObj.UserID == 1 | cObj.UserName.ToUpper() == "ADMIN")
                {
                    txtUserID.Enabled = false;
                }
                else
                {
                    txtUserID.Enabled = true;
                }


               // lnkSetPass.NavigateUrl = @"~/Admin/SetPassword.aspx?uk=" + cObj.UserID.ToString();
                lnkSetPass.NavigateUrl = @"javascript:tbopen(" + cObj.UserID.ToString() + ")";
                bStatus = true;
            }
            else
            {
                bStatus = false;
                //MMS.Globals.ShowMessagePage(MMS.MessageTypeEnum.Error, "User ID not Found!", "");
            }
            return bStatus;

        }


        private bool CheckData()
        {
            string userID = txtUserID.Text.Trim();

            if (userID == string.Empty)
            {
                Helper.SetStatusMessage(lblMessage, "Please Enter User ID", MessageTypeEnum.InvalidData);
                txtUserID.Focus();
                return false;
            }


            if (ddlRole.SelectedIndex <= 0)
            {
                Helper.SetStatusMessage(lblMessage, "Please Select Role", MessageTypeEnum.InvalidData);
                ddlRole.Focus();
                return false;
            }

            if (EditMode == FormDataMode.Add)
            {
                base.CheckPermissionAdd();


                if (UserBL.IsUserExists(AppInfo.AppID, userID))
                {
                    Helper.SetStatusMessage(lblMessage, "User ID Already Exists", MessageTypeEnum.InvalidData);
                    return false;

                }

                if (txtPassword.Text == "")
                {
                    Helper.SetStatusMessage(lblMessage, "Please Enter Password", MessageTypeEnum.InvalidData);
                    return false;
                }

                if (txtPassword.Text != txtPassword2.Text)
                {
                    Helper.SetStatusMessage(lblMessage, "Password Not Matched", MessageTypeEnum.InvalidData);
                    return false;
                }
            }
            else if (EditMode == FormDataMode.Edit)
            {
                base.CheckPermissionEdit();
            }
            return true;
        }


        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            //Helper.SetStatusMessage(lblMessage, "", MessageTypeEnum.Successful);
            AddTask();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {          
            if (!Page.IsValid)
            { return; }

            if (!CheckData())
            { return; }

            if (SaveData())
            {
                AppCache.Remove(AppCache.CacheKey_Users);
                Helper.SetStatusMessage(lblMessage, saveMsg, MessageTypeEnum.Successful);
                EditTask();
            }
            else
            {
                Helper.SetStatusMessage(lblMessage, saveMsg, MessageTypeEnum.Error);
            }            
        }


        private bool SaveData()
        {
            bool bStatus = false;
            int newUserID = 0;

            dcUser cObj = new dcUser();

            cObj.AppID = AppInfo.AppID;
            cObj.UserName = txtUserID.Text;
            cObj.RoleID = Convert.ToInt32(ddlRole.SelectedValue);
            cObj.FullName = txtFullName.Text;
            cObj.Email = "";
            cObj.IsActive = ddlIsActive.SelectedValue == "1" ? true : false;

            if (EditMode == FormDataMode.Add)
            {
                try
                {
                    cObj.Password = txtPassword.Text;
                    newUserID = UserBL.Insert(cObj);
                    
                    if (newUserID > 0)
                    {
                        bStatus = true;
                        saveMsg = "New User saved successfully.";
                    }
                }
                catch (Exception e)
                {
                    saveMsg = e.Message;
                }

            }
            else
            {
                if (EditMode == FormDataMode.Edit)
                {
                    cObj.UserID = this.UserID;
                    //try
                    {
                        bStatus = UserBL.Update(cObj);
                        newUserID = cObj.UserID;
                        saveMsg = "Edited User saved successfully.";
                    }
                    //catch (Exception e)
                    {
                        //    saveMsg = e.Message;
                    }
                }
            }
            if (bStatus)
            {
                UserID = newUserID;
            }

            return bStatus;
        }


    }
}
