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
    public partial class Role : BagePage
    {
        int RoleID = 0;
        string ViewStateKey = "RoleID";
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
            this.RoleID = base.GetPageQueryInteger("id");

            if (!IsPostBack) //first Time
            {
                FillCombo();
                if (this.RoleID == 0) //not query string
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
                this.RoleID = int.Parse(ViewState[ViewStateKey].ToString());
            }
            this.ShowPageMessage(this.lblMessage);

           // Response.Write("RoleID : " + this.RoleID.ToString());
        }

        private void FillCombo()
        {



        }

        private void AddTask()
        {
            txtRoleName.Text = "";
            txtDesc.Text = "";

            ViewState[EditModeKey] = (int)FormDataMode.Add;
            ViewState[ViewStateKey] = "0";

            this.EditMode = FormDataMode.Add;
            //lblMode.Text = "Mode: Add";
            lblHeader.Text = "Role : New Role";
            txtRoleName.Enabled = true;
            txtRoleName.Focus();

            Helper.SetStatusMessage(lblMessage, string.Empty, MessageTypeEnum.None);
        }

        private void EditTask()
        {
            ViewState[EditModeKey] = (int)FormDataMode.Edit;
            this.EditMode = FormDataMode.Edit;
            //lblMode.Text = "Mode: Edit";
            lblHeader.Text = "Role: Edit";
            //this.SetMasterHeader("Edit User");
            ReadData(this.RoleID);
            txtRoleName.Enabled = false;
            ViewState[ViewStateKey] = this.RoleID.ToString();
           
        }

        private bool ReadData(int pRoleID)
        {
            dcRole cObj = RoleBL.GetRoleByRoleID(pRoleID);

            bool bStatus;
            if (cObj != null)
            {
                txtRoleName.Text = cObj.RoleName;
                txtDesc.Text = cObj.RoleDesc;
                this.RoleID = cObj.RoleID;

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
            else
            {
                bStatus = false;
                //MMS.Globals.ShowMessagePage(MMS.MessageTypeEnum.Error, "User ID not Found!", "");
            }
            return bStatus;

        }


        private bool CheckData()
        {
            string RoleName = txtRoleName.Text.Trim();

            if (RoleName == string.Empty)
            {
                Helper.SetStatusMessage(lblMessage, "Please Enter Role Name", MessageTypeEnum.InvalidData);
                txtRoleName.Focus();
                return false;
            }


            if (EditMode == FormDataMode.Add)
            {
                base.CheckPermissionAdd();


                if (RoleBL.IsRoleExists(AppInfo.AppID, RoleName))
                {
                    Helper.SetStatusMessage(lblMessage, "Role Already Exists", MessageTypeEnum.InvalidData);
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
            int newRoleID = 0;

            dcRole cObj = new dcRole();

            cObj.AppID = AppInfo.AppID;
            cObj.RoleName = txtRoleName.Text;
            cObj.RoleDesc = txtDesc.Text;

            if (EditMode == FormDataMode.Add)
            {
                try
                {
                    newRoleID = RoleBL.Insert(cObj);

                    if (newRoleID > 0)
                    {
                        bStatus = true;
                        saveMsg = "New Role saved successfully.";
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
                    cObj.RoleID = this.RoleID;
                    //try
                    {
                        bStatus = RoleBL.Update(cObj);
                        newRoleID = cObj.RoleID;
                        saveMsg = "Edited Role saved successfully.";
                    }
                    //catch (Exception e)
                    {
                        //    saveMsg = e.Message;
                    }
                }
            }
            if (bStatus)
            {
                RoleID = newRoleID;
            }

            return bStatus;
        }


    }
}
