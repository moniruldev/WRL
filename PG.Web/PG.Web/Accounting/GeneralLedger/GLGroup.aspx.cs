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

using PG.DBClass.AccountingDC;
using PG.DBClass.AccountingDC.AccEnums;
using PG.DBClass.AccountingDC.GeneralLedgerDC;

using PG.BLLibrary.AccountingBL;
using PG.BLLibrary.AccountingBL.GeneralLedgerBL;


namespace PG.Web.Accounting.GeneralLedger
{
    public partial class GLGroup : BagePage
    {
        int CompanyID = 0;
        int GLGroupID = 0;
        string ViewStateKey = "GLGroupID";
        string ViewStateKeyPrev = "GLGroupID_Prev";

        string saveMsg = string.Empty;

        //List<DBClass.SystemDC.dcSysOption> listSysOptions = null;


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
            //base.AppObjectID = BLLibrary.SystemBL.AppObjectEnum.Frm1001_OptionInfo;
            //base.RestrictByPageInTab();

            //ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(this.LinkButton1);

            this.CompanyID = CompanyInfo.GetCompanyID();
            this.hdnCompanyID.Value = this.CompanyID.ToString();
            this.GLGroupID = base.GetPageQueryInteger("id");

            if (!IsPostBack) //first Time
            {
                FillCombo();
                FillGLGroup();
                //List<dcGLGroup> cList = GLGroupBL.GetGLGroupList(true, false, AccOrderByEnum.SLNo, "");
                //this.GLGroupTree1.SetGroupTreeText<dcGLGroup>(cList, "GLGroupID", "GLGroupName", "GLGroupIDParent", false);
                this.GLGroupTree1.SetGLGroupTreeText(this.CompanyID);

                if (this.GLGroupID == 0) //not query string
                {
                    AddTask();
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
                this.EditMode = base.GetEditModeFromViewState(base.EditModeViewStateKey);
                this.GLGroupID = int.Parse(ViewState[ViewStateKey].ToString());
            }
            SetHyperLink();
            this.ShowPageMessage(this.lblMessage);
           // Response.Write("UserID : " + this.UserID.ToString());
        }

        private void SetHyperLink()
        {

            //new button
            //string hLink = "javascript:tbopenSalInfo("+ this.GLGroupID.ToString() +")";
            //if (base.PageMode == PG.Core.Web.PageModeEnum.InTab)
            //{
            //    hLink = "javascript:tbopenSalInfo(" + this.GLGroupID.ToString() + ")";
            //    //this.btnSalaryInfo = string.Empty;
            //    this.btnSalaryInfo.Attributes.Add("onclick", hLink);
            //}
            //else
            //{
            //    hLink = "~/Master/EmpSalaryInfo.aspx?eid=" + this.GLGroupID.ToString();
            //    //this.btnAddNew.PostBackUrl = hLink;
            //    //this.btnAddNew.OnClientClick = string.Empty;
            //    this.btnSalaryInfo.Attributes.Add("onclick", hLink);
            //}

        }

        private void FillCombo()
        {

        }

        private void FillGLGroup()
        {
            this.GLGroupTree1.SetGLGroupTreeText(this.CompanyID);   
        }


        private void SetControl(FormDataMode dataMode)
        {
            bool isEnabled = false;
            bool isSystem = hdnIsSystem.Value == "1" ? true : false;
            int groupIDParent = Convert.ToInt32(hdnGLGroupIDParent.Value);

            if (dataMode == FormDataMode.Add | dataMode == FormDataMode.Edit)
            {
                isEnabled = true;
            }

            txtCode.Enabled = isEnabled;
            txtName.Enabled = isEnabled;
            txtNameB.Enabled = isEnabled;
            txtNameShort.Enabled = isEnabled;
            
            txtGLGroupNameParent.Enabled = isEnabled;
            ddlBalanceType.Enabled = isEnabled;

            ddlShowAsLedger.Enabled = isEnabled;
            ddlIsGrossProfit.Enabled = isEnabled;

            ddlIsActive.Enabled = isEnabled;

            txtGLGroupCodeOS.Enabled = isEnabled;

            //txtTotRequisitionAmt.Enabled = isEnabled;

            txtGLGroupNameParent.Attributes.Add("readonly", "readonly");


            lblNamePredefined.Visible = isSystem;
            txtNamePredifined.Visible = isSystem;


            if (dataMode == FormDataMode.Edit)
            {
                txtGLGroupNameParent.Enabled = !isSystem;
                lblBalanceType.Enabled = !isSystem;
                ddlBalanceType.Enabled = !isSystem;
                ddlIsGrossProfit.Enabled = !isSystem;
                
            }


            SetIsGrossProfit(groupIDParent);
        


            //buttons
            btnAddNew.Visible = !isEnabled;
            btnEdit.Visible = !isEnabled;
            btnDelete.Visible = !isEnabled;

            btnSave.Visible = isEnabled;
            btnCancel.Visible = isEnabled;

        }



        private void ReadTask()
        {
            lblHeader.Text = "GL Head : View";
            this.EditMode = FormDataMode.Read;
            //this.hdnPaymentReqID.Value = this.PaymentReqID.ToString();
            ReadData(this.GLGroupID);

            ViewState[ViewStateKey] = this.GLGroupID.ToString();

            SetControl(FormDataMode.Read);


            //var c = new { Name = "Name", ID = 344 };
        }


        private void AddTask()
        {
            ViewState[ViewStateKeyPrev] = this.GLGroupID.ToString();

            hdnGLGroupID.Value = "0";
            hdnIsSystem.Value = "0";

            txtCode.Text = "";
            txtName.Text = "";
            txtNameShort.Text = "";

            ddlIsGrossProfit.SelectedValue = "0";
            ddlShowAsLedger.SelectedValue = "0";
            ddlBalanceType.SelectedValue = "0";

            txtNamePredifined.Text = "";

            hdnGLGroupIDParent.Value = "0";
            hdnGLClassID.Value = "0";
            hdnGLGroupClassID.Value = "0";
            hdnGLGroupParentKey.Value = "";

            txtGLGroupNameParent.Text = "";

            hdnGLGroupIDParentEdit.Value = "0";
            hdnGLClassIDEdit.Value = "0";
            hdnGLGroupClassIDEdit.Value = "0";

            txtGLGroupCodeOS.Text = "";

            lblNamePredefined.Visible = false;
            txtNamePredifined.Visible = false;


            this.EditMode = FormDataMode.Add;
            this.IsDirty = false;
            ViewState[ViewStateKey] = "0";
            this.hdnGLGroupID.Value = "0";
            //lblMode.Text = "Mode: Add";
            lblHeader.Text = "GL Head : New";
            txtName.Focus();

            SetControl(FormDataMode.Add);
            Helper.SetStatusMessage(lblMessage, string.Empty, MessageTypeEnum.None);
        }

        private void EditTask()
        {
            this.EditMode = FormDataMode.Edit;
            //lblMode.Text = "Mode: Edit";
            lblHeader.Text = "GL Head: Edit";
            //this.SetMasterHeader("Edit User");
            ReadData(this.GLGroupID);
            SetIsGrossProfit(Convert.ToInt32(hdnGLGroupIDParent.Value));
            ViewState[ViewStateKey] = this.GLGroupID.ToString();
            //lnkAddNew.Visible = true;
            txtCode.Focus();
            SetControl(FormDataMode.Edit);
        }

        private void RefreshTask()
        {
            switch (this.EditMode)
            {
                case FormDataMode.Add:
                    AddTask();
                    break;
                case FormDataMode.Edit:
                    EditTask();
                    break;
                case FormDataMode.Read:
                    ReadTask();
                    break;
            }
        }

        private void DeleteTask()
        {
            if (this.GLGroupID > 0)
            {
                //BLLibrary.PaymentBL.PaymentRequisitionBL.DeleteWithDetails(this.PaymentReqID);

                //this.SetPageMessage("Payment Requisition Deleted Successfully.", MessageTypeEnum.Successful);
                //this.SetPageMessageToSession();
                ////string redirectURL = "~/Project/Land.aspx?id=" + this.PaymentReqID.ToString() + "&" + AppMessage.CreateQueryString(this.AppMessageID);

                //string redirectURL = "~/Payment/PaymentRequisition.aspx?id=0";
                //redirectURL = base.SetPageTabQueryString(redirectURL);
                //redirectURL = base.SetPageMessageQueryString(redirectURL, this.AppMessageID);
                //Response.Redirect(redirectURL,false);
            }

        }

        private void CancelTask()
        {
            if (EditMode == FormDataMode.Add)
            {
                int prevID = base.GetViewStateInt(ViewStateKeyPrev);
                if (prevID > 0)
                {
                    this.GLGroupID = prevID;
                    ReadTask();
                }
                else
                {
                    this.GLGroupID = 0;
                    AddTask();
                }
            }
            else if (EditMode == FormDataMode.Edit)
            {
                ReadTask();
            }
            this.IsDirty = false;
        }

        private bool SaveTask()
        {
            if (!Page.IsValid)
            { return false; }

            if (!CheckData())
            { return false; }

            bool bStatus = SaveData();

            if (bStatus)
            {
                this.IsDirty = false;
                SetHyperLink();
                this.SetPageMessage(saveMsg, MessageTypeEnum.Successful);
                this.SetPageMessageToSession();

                string redirectURL = "~/Accounting/GeneralLedger/GLGroup.aspx?id=" + this.GLGroupID.ToString();
                redirectURL = base.SetPageTabQueryString(redirectURL);
                redirectURL = base.SetPageMessageQueryString(redirectURL, this.AppMessageID);
                Response.Redirect(redirectURL,false);

                //EditTask();
            }
            else
            {
                //  Helper.SetStatusMessage(lblMessage, saveMsg, MessageTypeEnum.Error);
                this.SetPageMessage(saveMsg, MessageTypeEnum.Error);
                base.ShowPageMessage(lblMessage, true);
            }
            return bStatus;
            
        }


        private bool ReadData(int pGLGroupID)
        {
             dcGLGroup cObj = GLGroupBL.GetGLGroupByID(this.CompanyID, pGLGroupID);

            bool bStatus;
            if (cObj != null)
            {
                this.GLGroupID = cObj.GLGroupID;
                this.hdnGLGroupID.Value = cObj.GLGroupID.ToString();
                this.hdnIsSystem.Value = cObj.IsSystem ? "1" : "0";

                txtCode.Text = cObj.GLGroupCode;
                txtName.Text = cObj.GLGroupName;
                txtNameB.Text = cObj.GLGroupNameB;
                txtNameShort.Text = cObj.GLGroupNameShort;
                
                hdnGLGroupIDParent.Value = cObj.GLGroupIDParent.ToString();
                hdnGLClassID.Value = cObj.GLClassID.ToString();
                hdnGLGroupClassID.Value = cObj.GLGroupClassID.ToString();
                //txtGLGroupNameParent.Text = cObj.GLGroupNameParentEffective;

                
                txtGLGroupNameParent.Text = cObj.GLGroupNameCodeShortNameParentEffective;

                hdnGLGroupParentKey.Value = cObj.GLGroupParentKey;
                //hdnGLGroupParentBalanceType.Value = cObj.GLGroupParentBalanceType.ToString();

                ddlIsActive.SelectedValue = cObj.IsActive ? "1" : "0";

                hdnGLGroupIDParentEdit.Value = cObj.GLGroupIDParent.ToString();
                hdnGLClassIDEdit.Value = cObj.GLClassID.ToString();
                hdnGLGroupClassIDEdit.Value = cObj.GLGroupClassID.ToString();

                ddlBalanceType.SelectedValue = cObj.BalanceType.ToString();

                ddlShowAsLedger.SelectedValue = cObj.ShowAsLedger ? "1" : "0" ;
                ddlIsGrossProfit.SelectedValue = cObj.IsGrossProfit ? "1" : "0";

                txtNamePredifined.Text = cObj.GLGroupNameSys;
                txtGLGroupCodeOS.Text = cObj.GLGroupCodeOS;
                  //chkIsOpenning.Checked = cObj.IsOpenningPeriod;
               //// lnkSetPass.NavigateUrl = @"~/Admin/SetPassword.aspx?uk=" + cObj.UserID.ToString();
               // lnkSetPass.NavigateUrl = @"javascript:tbopen(" + cObj.UserID.ToString() + ")";
                bStatus = true;
            }
            else
            {
                this.hdnGLGroupID.Value = "0";
                this.GLGroupID = 0;
                this.hdnIsSystem.Value = "0";
                this.hdnGLGroupIDParent.Value = "0";
                bStatus = false;
                //MMS.Globals.ShowMessagePage(MMS.MessageTypeEnum.Error, "User ID not Found!", "");
            }
            return bStatus;

        }

        private bool CheckData()
        {

            if (txtCode.Text.Trim() == string.Empty)
            {
                //Helper.SetStatusMessage(lblMessage, "Please Enter Employee Name", MessageTypeEnum.InvalidData);
                this.SetPageMessage("Please Enter Code", MessageTypeEnum.InvalidData);
                this.ShowPageMessage(lblMessage, true);
                txtCode.Focus();
                return false;
            }


            if (txtName.Text.Trim() == string.Empty)
            {
                //Helper.SetStatusMessage(lblMessage, "Please Enter Employee Name", MessageTypeEnum.InvalidData);
                this.SetPageMessage("Please Enter Name", MessageTypeEnum.InvalidData);
                this.ShowPageMessage(lblMessage, true);
                txtName.Focus();
                return false;
            }

            if (txtNameShort.Text.Trim() == string.Empty)
            {
                //Helper.SetStatusMessage(lblMessage, "Please Enter Employee Name", MessageTypeEnum.InvalidData);
                this.SetPageMessage("Please Enter Short Name", MessageTypeEnum.InvalidData);
                this.ShowPageMessage(lblMessage, true);
                txtNameShort.Focus();
                return false;
            }


            int glGroupID = Convert.ToInt32(hdnGLGroupID.Value);
            int glClassID = Convert.ToInt32(hdnGLClassID.Value);
            int glGroupIDParent = Convert.ToInt32(hdnGLGroupIDParent.Value);
            int glGroupClassID = Convert.ToInt32(hdnGLGroupClassID.Value);

            if (glGroupIDParent == 0)
            {
                if (glClassID == 0)
                {
                    //Helper.SetStatusMessage(lblMessage, "Please Enter Employee Name", MessageTypeEnum.InvalidData);
                    this.SetPageMessage("Please Select Group Parent", MessageTypeEnum.InvalidData);
                    this.ShowPageMessage(lblMessage, true);
                    txtGLGroupNameParent.Focus();
                    return false;
                }
            }

            dcAccSettings accSettings = AccSettingsBL.GetAccSettingByCompanyID(this.CompanyID);

            if (EditMode == FormDataMode.Add)
            {

                if (GLGroupBL.IsGLGroupCodeExists(this.CompanyID, txtCode.Text.Trim()))
                {
                    //Helper.SetStatusMessage(lblMessage, "Employee Already Exists", MessageTypeEnum.InvalidData);
                    this.SetPageMessage("Group Code Already Exists", MessageTypeEnum.InvalidData);
                    this.ShowPageMessage(lblMessage, true);
                    txtCode.Focus();
                    return false;

                }

                if (GLGroupBL.IsGLGroupNameExists(this.CompanyID,txtName.Text.Trim()))
                {
                    //Helper.SetStatusMessage(lblMessage, "Employee Already Exists", MessageTypeEnum.InvalidData);
                    this.SetPageMessage("Group Name Already Exists", MessageTypeEnum.InvalidData);
                    this.ShowPageMessage(lblMessage, true);
                    txtName.Focus();
                    return false;
                }

                if (GLGroupBL.IsGLGroupNameShortExists(this.CompanyID, txtNameShort.Text.Trim()))
                {
                    //Helper.SetStatusMessage(lblMessage, "Employee Already Exists", MessageTypeEnum.InvalidData);
                    this.SetPageMessage("Group Short Name Already Exists", MessageTypeEnum.InvalidData);
                    this.ShowPageMessage(lblMessage, true);
                    txtNameShort.Focus();
                    return false;
                }


                if (glClassID == (int)GLClassEnum.ProfitAndLoss)
                {
                    if (accSettings.AllowGrpAccUnderPLGrp == false)
                    {
                        this.SetPageMessage("Group Create Not Allowed Under Profit And Loss", MessageTypeEnum.InvalidData);
                        this.ShowPageMessage(lblMessage, true);
                        txtGLGroupNameParent.Focus();
                        return false;
                    }
                }

            }
            else if (EditMode == FormDataMode.Edit)
            {
                if (GLGroupBL.IsGLGroupCodeExists(this.CompanyID, txtCode.Text.Trim(), this.GLGroupID))
                {
                    //Helper.SetStatusMessage(lblMessage, "Employee Already Exists", MessageTypeEnum.InvalidData);
                    this.SetPageMessage("Code Name Already Exists", MessageTypeEnum.InvalidData);
                    this.ShowPageMessage(lblMessage, true);
                    txtName.Focus();
                    return false;
                }

                if (GLGroupBL.IsGLGroupNameExists(this.CompanyID, txtName.Text.Trim(), this.GLGroupID))
                {
                    //Helper.SetStatusMessage(lblMessage, "Employee Already Exists", MessageTypeEnum.InvalidData);
                    this.SetPageMessage("Group Name Already Exists", MessageTypeEnum.InvalidData);
                    this.ShowPageMessage(lblMessage, true);
                    txtName.Focus();
                    return false;
                }

                if (GLGroupBL.IsGLGroupNameShortExists(this.CompanyID, txtNameShort.Text.Trim(), this.GLGroupID))
                {
                    //Helper.SetStatusMessage(lblMessage, "Employee Already Exists", MessageTypeEnum.InvalidData);
                    this.SetPageMessage("Group Short Name Already Exists", MessageTypeEnum.InvalidData);
                    this.ShowPageMessage(lblMessage, true);
                    txtNameShort.Focus();
                    return false;
                }


                if (glGroupID == glGroupIDParent)
                {
                    this.SetPageMessage("Please Select another parent. Same Group parent not allowed!", MessageTypeEnum.InvalidData);
                    this.ShowPageMessage(lblMessage, true);
                    txtGLGroupNameParent.Focus();
                    return false;
                }

                int glClassIDEdit = Convert.ToInt32(hdnGLClassIDEdit.Value);
                if (glClassID != glClassIDEdit)
                {
                    this.SetPageMessage("Group Cannot move to different GL Class!", MessageTypeEnum.InvalidData);
                    this.ShowPageMessage(lblMessage, true);
                    txtGLGroupNameParent.Focus();
                    return false;
                }

                int glGroupClassIDEdit = Convert.ToInt32(hdnGLGroupClassIDEdit.Value);
                if (glGroupClassIDEdit > 0)
                {
                    if (accSettings.AllowChangeGLGroupCLass == false)
                    {
                        if (glGroupClassID != glGroupClassIDEdit)
                        {
                            this.SetPageMessage("Please Group Nature Not matched with GL Class", MessageTypeEnum.InvalidData);
                            this.ShowPageMessage(lblMessage, true);
                            txtGLGroupNameParent.Focus();
                            return false;
                        }
                    }
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
            SaveTask();
        } 


        private bool SaveData()
        {
            bool bStatus = false;

            bool isSystem = Convert.ToInt32(hdnIsSystem.Value) == 1 ;

            int newGLGroupID = 0;

            dcGLGroup cObj = new dcGLGroup();

            cObj.CompanyID = this.CompanyID;
            cObj.GLGroupID = this.GLGroupID;

            cObj.GLGroupName = txtName.Text.Trim();
            cObj.GLGroupNameB = txtNameB.Text.Trim();
            cObj.GLGroupCode = txtCode.Text.Trim();
            cObj.GLGroupNameShort = txtNameShort.Text.Trim();
            cObj.ShowAsLedger = ddlShowAsLedger.SelectedValue == "1" ? true : false;
            cObj.IsActive = ddlIsActive.SelectedValue == "1" ? true : false;
            cObj.GLGroupCodeOS = txtGLGroupCodeOS.Text.Trim();

            //cObj.AccGLGroupIDParent = Convert.ToInt32(ddlGroupParent.SelectedValue);

            if (!isSystem)
            {
                cObj.GLGroupIDParent = Convert.ToInt32(hdnGLGroupIDParent.Value);
                cObj.BalanceType = Convert.ToInt32(ddlBalanceType.SelectedValue);
                
                cObj.IsGrossProfit = false;
                if (cObj.GLGroupIDParent == (int)GLClassEnum.Income |
                          cObj.GLGroupIDParent == (int)GLClassEnum.Expense)
                {
                    cObj.IsGrossProfit = ddlIsGrossProfit.SelectedValue == "1" ? true : false;
                }

                cObj.GLClassID = Convert.ToInt32(hdnGLClassID.Value);
                cObj.GLGroupClassID = Convert.ToInt32(hdnGLGroupClassID.Value);
                //cObj.g = Convert.ToInt32(hdnGLGroupClassID.Value);
                cObj.GLGroupNameParent = txtGLGroupNameParent.Text;

            }

            bool isAdd = EditMode == FormDataMode.Add;

            newGLGroupID = GLGroupBL.Save(cObj, isAdd);
            if (newGLGroupID > 0)
            {
                saveMsg = EditMode == FormDataMode.Add ? "New GL Group saved successfully." : "Edited GL Group saved successfully.";
                bStatus = true;
            }
            else
            {
                saveMsg = "Error Occured! Not Saved";
            }

            if (bStatus)
            {
                GLGroupID = newGLGroupID;
            }
            return bStatus;
        }

        protected void SetIsGrossProfit(int pGLGroupIDParent)
        {
            if (pGLGroupIDParent == (int)GLClassEnum.Income |
                   pGLGroupIDParent == (int)GLClassEnum.Expense)
            {
                lblIsGrossProfit.Style.Add("visibility", "visible");
                ddlIsGrossProfit.Style.Add("visibility", "visible");
            }
            else
            {
                lblIsGrossProfit.Style.Add("visibility", "hidden");
                ddlIsGrossProfit.Style.Add("visibility", "hidden");
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteTask();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshTask();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            CancelTask();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            EditTask();
        }

    }
}
