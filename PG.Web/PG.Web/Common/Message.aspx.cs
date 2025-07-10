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
using PG.Core.DBBase;
using PG.Core.Extentions;
using PG.Core.DBFilters;


namespace PG.Web.Common
{
    public partial class Message : BagePage
    {
        private string saveMsg = string.Empty;

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

            this.AppMessageCurrent = base.GetAppMessage();

            this.lblMsg1.Text = "Permission Denied!";

            if (this.AppMessageCurrent != null)
            {
                this.lblMsg1.Text = this.AppMessageCurrent.MessageString;
                this.lblMsg2.Text = this.AppMessageCurrent.MessageDesc;

                this.btnBack.Visible = this.AppMessageCurrent.ShowBackButton;


                if (this.AppMessageCurrent.ShowMessageBox)
                {
                    this.SetStartupMessageBox(this.AppMessageCurrent.MessageString, this.AppMessageCurrent.MessageType);
                }
            }
            //txtUser.Text = HttpContext.Current.User.Identity.Name;

            //base.AppObjectID = BLLibrary.SystemBL.AppObjectEnum.Frm1001_OptionInfo;
            //base.RestrictByPageInTab();

            //ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(this.LinkButton1);

            //this.BranchID = base.GetPageQueryInteger("id");

            //if (!IsPostBack) //first Time
            //{
            //    FillCombo();
            //    if (this.BranchID == 0) //not query string
            //    {
            //        AddTask();
            //    }
            //    else
            //    {
            //        EditTask();
            //        //if (Session["MsgSaveStatus"] != null)
            //        //{
            //        //    string sMsg = Session["MsgSaveStatus"].ToString();
            //        //    lblMessage.Text = sMsg.ToString();
            //        //    Session["MsgSaveStatus"] = null;
            //        //}
            //    }

            //}
            //else
            //{
            //    this.EditMode = base.GetEditModeFromViewState(EditModeKey);
            //    this.BranchID = int.Parse(ViewState[ViewStateKey].ToString());
            //}
            SetHyperLink();

            // Response.Write("UserID : " + this.UserID.ToString());
        }

        private void SetHyperLink()
        {

            //new button
            //string hLink = "javascript:tbopenSalInfo("+ this.BranchID.ToString() +")";
            //if (base.PageMode == PG.Core.Web.PageModeEnum.InTab)
            //{
            //    hLink = "javascript:tbopenSalInfo(" + this.BranchID.ToString() + ")";
            //    //this.btnSalaryInfo = string.Empty;
            //    this.btnSalaryInfo.Attributes.Add("onclick", hLink);
            //}
            //else
            //{
            //    hLink = "~/Master/EmpSalaryInfo.aspx?eid=" + this.BranchID.ToString();
            //    //this.btnAddNew.PostBackUrl = hLink;
            //    //this.btnAddNew.OnClientClick = string.Empty;
            //    this.btnSalaryInfo.Attributes.Add("onclick", hLink);
            //}

        }


        private void FillCombo()
        {

 
 

        }

        private void AddTask()
        {
            //txtName.Text = "";
            //txtCode.Text = "";
            //ViewState[EditModeKey] = (int)FormDataMode.Add;
            //ViewState[ViewStateKey] = "0";
            ////lblMode.Text = "Mode: Add";
            //lblHeader.Text = "Branch : New";
            //txtName.Focus();
            //Helper.SetStatusMessage(lblMessage, string.Empty, MessageTypeEnum.None);
        }

        private void EditTask()
        {
            //ViewState[EditModeKey] = (int)FormDataMode.Edit;
            ////lblMode.Text = "Mode: Edit";
            //lblHeader.Text = "Branch: Edit";
            ////this.SetMasterHeader("Edit User");
            //ReadData(this.BranchID);
            //ViewState[ViewStateKey] = this.BranchID.ToString();
            ////lnkAddNew.Visible = true;
            //txtName.Focus();

        }

        private bool ReadData(int pBranchID)
        {
            //DBClass.Master.dcBranch cObj = BLLibrary.MasterBL.BranchBL.GetBranchByID(pBranchID);

            bool bStatus = false;
            //if (cObj != null)
            //{
            //    txtName.Text = cObj.BranchName;
            //    txtCode.Text = cObj.BranchCode;
            //   //// lnkSetPass.NavigateUrl = @"~/Admin/SetPassword.aspx?uk=" + cObj.UserID.ToString();
            //   // lnkSetPass.NavigateUrl = @"javascript:tbopen(" + cObj.UserID.ToString() + ")";
            //    bStatus = true;
            //}
            //else
            //{
            //    bStatus = false;
            //    //MMS.Globals.ShowMessagePage(MMS.MessageTypeEnum.Error, "User ID not Found!", "");
            //}
            return bStatus;

        }


        private bool CheckData()
        {

            //if (txtNewPassword.Text.Trim() == string.Empty)
            //{
            //    //Helper.SetStatusMessage(lblMessage, "Please Enter Employee Name", MessageTypeEnum.InvalidData);
            //    this.SetPageMessage("Empty password not allowed!", MessageTypeEnum.InvalidData);
            //    this.ShowPageMessage(lblMessage, true);
            //    txtNewPassword.Focus();
            //    return false;
            //}


            //if (txtNewPassword.Text != txtNewPasswordConfirm.Text )
            //{
            //    //Helper.SetStatusMessage(lblMessage, "Please Enter Employee Name", MessageTypeEnum.InvalidData);
            //    this.SetPageMessage("Passwor Not Matched!", MessageTypeEnum.InvalidData);
            //    this.ShowPageMessage(lblMessage, true);
            //    txtNewPasswordConfirm.Focus();
            //    return false;
            //}


            //if (EditMode == FormDataMode.Add)
            //{

            //    if (BLLibrary.PayRollBL.SalaryPeriodBL.IsPeriodExists(month, year))
            //    {
            //        //Helper.SetStatusMessage(lblMessage, "Employee Already Exists", MessageTypeEnum.InvalidData);
            //        this.SetPageMessage("Period Already Exists", MessageTypeEnum.InvalidData);
            //        this.ShowPageMessage(lblMessage, true);
            //        return false;

            //    }
            //}
            //else if (EditMode == FormDataMode.Edit)
            //{
            //    if (BLLibrary.PayRollBL.SalaryPeriodBL.IsPeriodExists(month, year,this.BranchID))
            //    {
            //        //Helper.SetStatusMessage(lblMessage, "Employee Already Exists", MessageTypeEnum.InvalidData);
            //        this.SetPageMessage("Period Already Exists", MessageTypeEnum.InvalidData);
            //        this.ShowPageMessage(lblMessage, true);
            //        return false;

            //    }
            //}
            return true;
        }



        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            { return; }

            if (!CheckData())
            { return; }

            if (SaveData())
            {
                //SetHyperLink();
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
            //string userName = txtUser.Text.Trim();

            //saveMsg = "Error! Passoword not changed";
            //if (BLLibrary.SystemBL.UserBL.CheckPasswordByName(Globals.AppID, userName, txtCurPassword.Text))
            //{
            //    //passowrd ok
            //    if (BLLibrary.SystemBL.UserBL.ChangePasswordByName(Globals.AppID, userName, txtNewPassword.Text))
            //    {
            //        saveMsg = "New Password Saved Successfull!";
            //        bStatus = true;
            //    }
            //}
            //else
            //{
            //    saveMsg = "Current Password not matched!";

            //}
            return bStatus;
        }

    }
}
