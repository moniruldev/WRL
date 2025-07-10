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

using PG.DBClass.SystemDC;
using PG.BLLibrary.SystemsBL;


namespace PG.Web.Admin
{
    public partial class AppMenu : BagePage
    {
        int AppMenuID = 0;
        string ViewStateKey = "AppMenuID";
        string EditModeKey = "EditMode";
        string saveMsg = string.Empty;

        //string DateParse = "dd-MMM-yyyy";



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
            base.AppObjectID = AppObjectEnum.Frm_PFMember;
            this.CheckPermissionRead();

            //this.ddlInBlockList.Enabled = AppSecurity.CheckObjectPermissionByUserID(this.LoginUserID, AppObjectEnum.FrmOpt_BlockedEmployee, PermissionEnum.Read | PermissionEnum.Edit);
            //base.RestrictByPageInTab();
            //ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(this.LinkButton1);
            this.AppMenuID = base.GetPageQueryInteger("id");

            if (!IsPostBack) //first Time
            {
                FillCombo();
                if (this.AppMenuID == 0) //not query string
                {
                    AddTask();
                }
                else
                {
                   // AppSecurity.CheckBlockedEmpOption(this.LoginUserID, this.AppMenuID, PermissionEnum.Read);
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
                this.AppMenuID = int.Parse(ViewState[ViewStateKey].ToString());
            }
            SetHyperLink();


           // Response.Write("UserID : " + this.UserID.ToString());
        }

        private void SetHyperLink()
        {

            //new button
            //string hLink = "javascript:tbopenSalInfo("+ this.AppMenuID.ToString() +")";
            
            //if (base.PageMode == PG.Core.Web.PageModeEnum.InTab)
            //{
            //    hLink = "javascript:tbopenSalInfo(" + this.AppMenuID.ToString() + ")";
            //    //this.btnSalaryInfo = string.Empty;
            //    this.btnSalaryInfo.Attributes.Add("onclick", hLink);

            //    hLink = "javascript:tbopenEmpTax(" + this.AppMenuID.ToString() + ")";
            //    //this.btnSalaryInfo = string.Empty;
            //    this.btnIncomeTax.Attributes.Add("onclick", hLink);

            //}
            //else
            //{
            //    hLink = "~/Salary/EmpSalaryInfo.aspx?id=" + this.AppMenuID.ToString();
            //    //this.btnAddNew.PostBackUrl = hLink;
            //    //this.btnAddNew.OnClientClick = string.Empty;
            //    this.btnSalaryInfo.Attributes.Add("onclick", hLink);

            //    hLink = "~/Salary/EmpIncomeTax.aspx?id=" + this.AppMenuID.ToString();
            //    //this.btnAddNew.PostBackUrl = hLink;
            //    //this.btnAddNew.OnClientClick = string.Empty;
            //    this.btnIncomeTax.Attributes.Add("onclick", hLink);

            //}

        }


        private void FillCombo()
        {

            this.ddlParentMenu.Items.Clear();
            this.ddlParentMenu.Items.Add(new ListItem("None", "0"));

            this.ddlParentMenu.AppendDataBoundItems = true;
            this.ddlParentMenu.DataSource = AppMenuBL.GetAppMenuList(AppInfo.AppID);
            this.ddlParentMenu.DataTextField = "AppMenuText";
            this.ddlParentMenu.DataValueField = "AppMenuID";
            this.ddlParentMenu.DataBind();



            //ddlRole.Items.Clear();
            //ddlRole.Items.Add("(select role)");

            //ddlRole.DataTextField = "RoleName";
            //ddlRole.DataValueField = "RoleID";
            //ddlRole.AppendDataBoundItems = true;
            //ddlRole.DataSource = AppSecurity.GetRoleList(Globals.AppID);
            //ddlRole.DataBind();

        }
        public void FillBankBranch()
        {

        }


        private void AddTask()
        {
            txtAppID.Text = "";
            txtAppMenuID.Text = "";
            txtAppMenuName.Text = "";
            txtAppMenuText.Text = "";

            txtAppMenuPath.Text = "";
            txtAppMenuURL.Text = "";


            ViewState[EditModeKey] = (int)FormDataMode.Add;
            ViewState[ViewStateKey] = "0";
            //lblMode.Text = "Mode: Add";
            lblHeader.Text = "App Menu : New";


            Helper.SetStatusMessage(lblMessage, string.Empty, MessageTypeEnum.None);
        }

        private void EditTask()
        {
            ViewState[EditModeKey] = (int)FormDataMode.Edit;
            //lblMode.Text = "Mode: Edit";
            lblHeader.Text = "App Menu: Edit";
            //this.SetMasterHeader("Edit User");
            ReadData(this.AppMenuID);
            ViewState[ViewStateKey] = this.AppMenuID.ToString();
            //lnkAddNew.Visible = true;

        }

        private bool ReadData(int pAppMenuID)
        {
            dcAppMenu cObj = AppMenuBL.GetAppMenuByID(pAppMenuID);

            bool bStatus;
            if (cObj != null)
            {

                txtAppID.Text = cObj.AppID.ToString();
                txtAppMenuID.Text = cObj.AppMenuID.ToString();
                txtAppMenuName.Text = cObj.AppMenuName;
                txtAppMenuText.Text = cObj.AppMenuText;


                ddlParentMenu.SelectedValue = cObj.ParentMenuID.ToString();

                txtSLNo.Text = cObj.AppMenuSLNo.ToString();
                txtAppMenuPath.Text = cObj.AppMenuPath;

                ddlIsAppURL.SelectedValue = cObj.IsAppURL ? "1" : "0";
                
                txtAppMenuURL.Text = cObj.AppMenuURL;


                ddlSetAppHeader.SelectedValue = cObj.AppHeaderText;
                txtAppHeaderText.Text = cObj.SetAppHeader ? "1" : "0";


                ddlSelectAction.SelectedValue = cObj.SelectAction.ToString();
                ddlTabAction.SelectedValue = cObj.TabAction.ToString();

                ddlExpanded.SelectedValue = cObj.Expanded ? "1" : "0";
                ddlShowMenu.SelectedValue = cObj.ShowMenu ? "1" : "0";

    
               //// lnkSetPass.NavigateUrl = @"~/Admin/SetPassword.aspx?uk=" + cObj.UserID.ToString();
               // lnkSetPass.NavigateUrl = @"javascript:tbopen(" + cObj.UserID.ToString() + ")";
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


            if (txtAppMenuID.Text.Trim() == string.Empty)
            {
               //Helper.SetStatusMessage(lblMessage, "Please Enter Employee Code", MessageTypeEnum.InvalidData);

                this.SetPageMessage("Please Enter Menu ID", MessageTypeEnum.InvalidData);
                this.ShowPageMessage(lblMessage, true);
                txtAppMenuID.Focus();
                return false;
            }

            if (txtAppMenuText.Text.Trim() == string.Empty)
            {
                //Helper.SetStatusMessage(lblMessage, "Please Enter Employee Name", MessageTypeEnum.InvalidData);
                this.SetPageMessage("Please Enter Menu Text", MessageTypeEnum.InvalidData);
                this.ShowPageMessage(lblMessage, true);
                txtAppMenuText.Focus();
                return false;
            }

            if (this.AppMenuID == Convert.ToInt32(ddlParentMenu.SelectedValue))
            {
                //Helper.SetStatusMessage(lblMessage, "Please Enter Employee Name", MessageTypeEnum.InvalidData);
                this.SetPageMessage("Parent menu cannot be same", MessageTypeEnum.InvalidData);
                this.ShowPageMessage(lblMessage, true);
                ddlParentMenu.Focus();
                return false;
            }

            //if (ddlRole.SelectedIndex <= 0)
            //{
            //    Helper.SetStatusMessage(lblMessage, "Please Select Role", MessageTypeEnum.InvalidData);
            //    ddlRole.Focus();
            //    return false;
            //}

            int appMenuID = PG.Core.Utility.Conversion.StringToInt(txtAppMenuID.Text);
            if (EditMode == FormDataMode.Add)
            {

                if (AppMenuBL.IsAppMenuExists(appMenuID))
                {
                    //Helper.SetStatusMessage(lblMessage, "Employee Already Exists", MessageTypeEnum.InvalidData);
                    this.SetPageMessage("App Menu ID Already Exists", MessageTypeEnum.InvalidData);
                    this.ShowPageMessage(lblMessage, true);
                    return false;
                }
            }
            else if (EditMode == FormDataMode.Edit)
            {
                //this.CheckPermissionEdit();
                //AppSecurity.CheckBlockedEmpOption(this.LoginUserID, this.AppMenuID, PermissionEnum.Edit);

                if (AppMenuBL.IsAppMenuExists(appMenuID,this.AppMenuID))
                {
                    //Helper.SetStatusMessage(lblMessage, "Employee Already Exists", MessageTypeEnum.InvalidData);
                    this.SetPageMessage("App Menu ID Already Exists", MessageTypeEnum.InvalidData);
                    this.ShowPageMessage(lblMessage, true);
                    return false;
                }


                if (this.AppMenuID == Convert.ToInt32(ddlParentMenu.SelectedValue))
                {
                    //Helper.SetStatusMessage(lblMessage, "Please Enter Employee Name", MessageTypeEnum.InvalidData);
                    this.SetPageMessage("Parent menu cannot be same", MessageTypeEnum.InvalidData);
                    this.ShowPageMessage(lblMessage, true);
                    ddlParentMenu.Focus();
                    return false;
                }

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
                AppCache.Remove(AppCache.CacheKey_AppMenu);
                SetHyperLink();
                //Helper.SetStatusMessage(lblMessage, saveMsg, MessageTypeEnum.Successful);
                this.SetPageMessage(saveMsg, MessageTypeEnum.Successful);

                EditTask();
            }
            else
            {
              //  Helper.SetStatusMessage(lblMessage, saveMsg, MessageTypeEnum.Error);
                this.SetPageMessage(saveMsg, MessageTypeEnum.Error);
            }
            base.ShowPageMessage(lblMessage, true);
            
        }


        private bool SaveData()
        {
            bool bStatus = false;
            int newAppMenuID = 0;

            dcAppMenu cObj = new dcAppMenu();


            cObj.AppID = PG.Core.Utility.Conversion.StringToInt(txtAppID.Text);
            cObj.AppMenuID = PG.Core.Utility.Conversion.StringToInt(txtAppMenuID.Text);
            cObj.AppMenuName = txtAppMenuName.Text;


            cObj.ParentMenuID = Convert.ToInt32(ddlParentMenu.SelectedValue);

            cObj.AppMenuSLNo = PG.Core.Utility.Conversion.StringToInt(txtSLNo.Text);
            cObj.AppMenuPath = txtAppMenuPath.Text;

           cObj.IsAppURL = ddlIsAppURL.SelectedValue == "1" ? true : false;
           
            cObj.AppMenuText = txtAppMenuText.Text;

            cObj.SetAppHeader = ddlSetAppHeader.SelectedValue == "1" ? true : false;
            //cObj.SetAppHeader = ddlSetAppHeader.SelectedValue;
            cObj.AppHeaderText = txtAppHeaderText.Text;

            cObj.SelectAction = Convert.ToInt32(ddlSelectAction.SelectedValue);
            cObj.TabAction = Convert.ToInt32(ddlTabAction.SelectedValue);

            cObj.Expanded = ddlExpanded.SelectedValue == "1" ? true : false;
            cObj.ShowMenu = ddlShowMenu.SelectedValue == "1" ? true : false;
           


            if (EditMode == FormDataMode.Add)
            {
                //try
                {
                    newAppMenuID = AppMenuBL.Insert(cObj, this.DbContext);
                    if (newAppMenuID > 0)
                    {
                        bStatus = true;
                        saveMsg = "New Menu saved successfully.";
                    }
                }
                //catch (Exception e)
                //{
                //    saveMsg = e.Message;
                //}

            }
            else
            {
                if (EditMode == FormDataMode.Edit)
                {
                    dcAppMenu cObjKey = new dcAppMenu();
                    cObjKey.AppMenuID = this.AppMenuID;
                    //try
                    {
                        bStatus = AppMenuBL.Update(cObj, cObjKey, this.DbContext);
                        newAppMenuID = cObj.AppMenuID;
                        saveMsg = "Edited Menu saved successfully.";
                    }
                    //catch (Exception e)
                    {
                        //    saveMsg = e.Message;
                    }
                }
            }

            if (bStatus)
            {
                AppMenuID = newAppMenuID;
            }

            return bStatus;
        }

    }
}
