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
using PG.Core.Utility;

using PG.DBClass.AccountingDC;
using PG.DBClass.AccountingDC.AccEnums;
using PG.DBClass.AccountingDC.GeneralLedgerDC;

using PG.BLLibrary.AccountingBL;
using PG.BLLibrary.AccountingBL.GeneralLedgerBL;


namespace PG.Web.Accounting.GeneralLedger
{
    public partial class GLAccount : BagePage
    {
        int CompanyID = 0;
        int GLAccountID = 0;
        string ViewStateKey = "GLAccountID";
        string ViewStateKeyPrev = "GLAccountID_Prev";

        public string GLAccountServiceLink = PageLinks.AccountingLinks.GetLink_GLAccount;
        public string GLGroupServiceLink = PageLinks.AccountingLinks.GetLink_GLGroup;


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
            this.GLAccountID = base.GetPageQueryInteger("id");


            //PG.BLLibrary.AccountingBL

            if (!IsPostBack) //first Time
            {
                FillCombo();
                FillGLGroup();
           
                if (this.GLAccountID == 0) //not query string
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
                this.GLAccountID = int.Parse(ViewState[ViewStateKey].ToString());
            }
            SetHyperLink();
            this.ShowPageMessage(this.lblMessage);
           // Response.Write("UserID : " + this.UserID.ToString());
        }

        private void SetHyperLink()
        {

            //new button
            //string hLink = "javascript:tbopenSalInfo("+ this.GLAccountID.ToString() +")";
            //if (base.PageMode == PG.Core.Web.PageModeEnum.InTab)
            //{
            //    hLink = "javascript:tbopenSalInfo(" + this.GLAccountID.ToString() + ")";
            //    //this.btnSalaryInfo = string.Empty;
            //    this.btnSalaryInfo.Attributes.Add("onclick", hLink);
            //}
            //else
            //{
            //    hLink = "~/Master/EmpSalaryInfo.aspx?eid=" + this.GLAccountID.ToString();
            //    //this.btnAddNew.PostBackUrl = hLink;
            //    //this.btnAddNew.OnClientClick = string.Empty;
            //    this.btnSalaryInfo.Attributes.Add("onclick", hLink);
            //}

        }




        private void FillCombo()
        {
            this.ddlAccountType.AppendDataBoundItems = true;
            this.ddlAccountType.DataSource = GLAccountTypeBL.GetGLAccountTypeList(this.DbContext);
            this.ddlAccountType.DataTextField = "GLAccountTypeName";
            this.ddlAccountType.DataValueField = "GLAccountTypeID";
            this.ddlAccountType.DataBind();

            this.ddlOpBalYear.AppendDataBoundItems = true;
            this.ddlOpBalYear.DataSource = AccYearBL.GetAccYearList(this.CompanyID, this.DbContext);
            this.ddlOpBalYear.DataTextField = "AccYearName";
            this.ddlOpBalYear.DataValueField = "AccYearID";
            this.ddlOpBalYear.DataBind();


        }

        private void FillGLGroup()
        {
            this.GLGroupTree1.SetGLGroupTreeText(this.CompanyID);   
        }


        private void SetControl(FormDataMode dataMode)
        {
            bool isEnabled = false;

            if (dataMode == FormDataMode.Add | dataMode == FormDataMode.Edit)
            {
                isEnabled = true;
            }


            txtCode.Enabled = isEnabled;
            txtName.Enabled = isEnabled;
            txtNameB.Enabled = isEnabled;

            ddlAccountType.Enabled = isEnabled;

            ddlBalanceType.Enabled = isEnabled;

            //txtGLGroupNameParent.Enabled = isEnabled;
            
            //ddlShowAsSubLedger.Enabled = isEnabled;
            //ddlIsGrossProfit.Enabled = isEnabled;

            //txtTotRequisitionAmt.Enabled = isEnabled;

            this.txtGLGroupName.Enabled = isEnabled;
            this.txtGLAccountCodeParent.Enabled = isEnabled;
            this.txtGLAccountNameParent.Enabled = isEnabled;

            this.txtGLGroupName.Attributes.Add("readonly", "readonly");
            //this.txtGLAccountCodeParent.Attributes.Add("readonly", "readonly");
            this.txtGLAccountNameParent.Attributes.Add("readonly", "readonly");

            GLAccountTypeEnum accType = (GLAccountTypeEnum)Convert.ToInt32(ddlAccountType.SelectedValue);

            if (accType == GLAccountTypeEnum.SubAccount)
            {
                dvGLAccount.Style.Add("visibility", "visible");
                lblGLAccount.Style.Add("visibility", "visible");
            }
            else
            {
                dvGLAccount.Style.Add("visibility", "hidden");
                lblGLAccount.Style.Add("visibility", "hidden");
            }


            ddlOpBalYear.Enabled = false;
            txtOpBalance.Enabled = false; //isEnabled;
            ddlOpBalDrCr.Enabled = isEnabled;

            if (dataMode == FormDataMode.Edit)
            {

                //this.txtCode.Attributes.Add("readonly", "readonly");
                if (accType == GLAccountTypeEnum.ControlAccount)
                {
                    txtOpBalance.Attributes.Add("disabled", "disabled");
                    ddlOpBalDrCr.Attributes.Add("disabled", "disabled");
                }
            }


            //buttons
            btnAddNew.Visible = !isEnabled;
            btnEdit.Visible = !isEnabled;
            btnDelete.Visible = !isEnabled;

            btnSave.Visible = isEnabled;
            btnCancel.Visible = isEnabled;

        }

        private void SetGLAccountCode(FormDataMode dataMode)
        {
            //if (dataMode == FormDataMode.Add | dataMode == FormDataMode.Edit)
            //{

            //    dcAccSettings accSett
            //    int jTypeID = Convert.ToInt32(ddlJournalType.SelectedValue);
            //    dcJournalType jType = JournalTypeBL.GetJournalTypeByID(this.CompanyID, jTypeID);
            //    if (jType.AccSLNoID > 0)
            //    {
            //        txtJournalNo.Attributes.Add("readonly", "readonly");
            //    }
            //}
        }


        //private void AccountTypeSettings()
        //{
        //    GLAccountType accType = (GLAccountType)Convert.ToInt32(ddlAccountType.SelectedValue);

        //    if (accType == GLAccountType.SubAccount)
        //    {
        //        lblParent.Text = "Sub Account Of";
        //        dvGLGroup.Style.Add("display", "none");
        //        dvGLAccount.Style.Add("display", "");
        //    }
        //    else
        //    {
        //        lblParent.Text = "GL Group";
        //        dvGLAccount.Style.Add("display", "none");
        //        dvGLGroup.Style.Add("display", "");

        //    }
        //}


        private void ReadTask()
        {
            lblHeader.Text = "GL Account : View";
            this.EditMode = FormDataMode.Read;
            //this.hdnPaymentReqID.Value = this.PaymentReqID.ToString();
            ReadData(this.GLAccountID);

            ViewState[ViewStateKey] = this.GLAccountID.ToString();

            SetControl(FormDataMode.Read);


            //var c = new { Name = "Name", ID = 344 };
        }


        private void AddTask()
        {
            ViewState[ViewStateKeyPrev] = this.GLAccountID.ToString();

            txtCode.Text = "";
            txtName.Text = "";
            txtNameB.Text = "";
            txtNamePredifined.Text = "";

            hdnGLGroupID.Value = "0";
            hdnGLClassID.Value = "0";
            hdnGLGroupClassID.Value = "0";
            hdnGLGroupKey.Value = "";

            txtGLGroupName.Text = "";

            hdnGLGroupIDEdit.Value = "0";
            hdnGLClassIDEdit.Value = "0";
            hdnGLGroupClassIDEdit.Value = "0";

            lblNamePredefined.Visible = false;
            txtNamePredifined.Visible = false;

            txtOpBalance.Text = "";


            dcAccYear year = AccYearBL.GetCurrentAccYear(this.CompanyID);
            ddlOpBalYear.SelectedValue = year.AccYearID.ToString();


            this.EditMode = FormDataMode.Add;
            this.IsDirty = false;
            ViewState[ViewStateKey] = "0";
            this.hdnGLAccountID.Value = "0";
            //lblMode.Text = "Mode: Add";
            lblHeader.Text = "GL Account : New";
            txtName.Focus();

            SetControl(FormDataMode.Add);
            Helper.SetStatusMessage(lblMessage, string.Empty, MessageTypeEnum.None);
        }

        private void EditTask()
        {
            this.EditMode = FormDataMode.Edit;
            //lblMode.Text = "Mode: Edit";
            lblHeader.Text = "GL Account: Edit";
            //this.SetMasterHeader("Edit User");
            ReadData(this.GLAccountID);
           
            ViewState[ViewStateKey] = this.GLAccountID.ToString();
            //lnkAddNew.Visible = true;
            txtCode.Focus();
            SetControl(FormDataMode.Edit);
            //AccountTypeSettings();
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
            if (this.GLAccountID > 0)
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
                    this.GLAccountID = prevID;
                    ReadTask();
                }
                else
                {
                    this.GLAccountID = 0;
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

                string redirectURL = "~/Accounting/GeneralLedger/GLAccount.aspx?id=" + this.GLAccountID.ToString();
                redirectURL = base.SetPageTabQueryString(redirectURL);
                redirectURL = base.SetPageMessageQueryString(redirectURL, this.AppMessageID);
                Response.Redirect(redirectURL, false);

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


        private bool ReadData(int pGLAccountID)
        {
             dcGLAccount cObj = GLAccountBL.GetGLAccountByID(this.CompanyID, pGLAccountID);

            bool bStatus;
            if (cObj != null)
            {
                this.GLAccountID = cObj.GLAccountID;
                this.hdnGLAccountID.Value = cObj.GLAccountID.ToString();

                txtName.Text = cObj.GLAccountName;
                txtNameB.Text = cObj.GLAccountNameB;
                txtCode.Text = cObj.GLAccountCode;


                hdnGLGroupID.Value = cObj.GLGroupID.ToString();
                hdnGLClassID.Value = cObj.GLClassID.ToString();
                hdnGLGroupClassID.Value = cObj.GLGroupClassID.ToString();
                txtGLGroupName.Text = cObj.GLGroupName;
                hdnGLGroupKey.Value = cObj.GLGroupKey;

                //hdnBalanceType.Value = cObj.BalanceType.ToString();
                ddlBalanceType.SelectedValue = cObj.BalanceType.ToString();

                hdnGLGroupIDEdit.Value = cObj.GLGroupID.ToString();
                hdnGLClassIDEdit.Value = cObj.GLClassID.ToString();
                hdnGLGroupClassIDEdit.Value = cObj.GLGroupClassID.ToString();


                //ddlGroupParent.Enabled = !cObj.IsSystem;
                lblNamePredefined.Visible = cObj.IsSystem;
                txtNamePredifined.Visible = cObj.IsSystem;


                ddlAccountType.SelectedValue = cObj.GLAccountTypeID.ToString();

                hdnGLAccountIDParent.Value = "0";
                txtGLAccountCodeParent.Text = string.Empty;
                txtGLAccountNameParent.Text = string.Empty;
                if (cObj.GLAccountTypeID == (int)GLAccountTypeEnum.SubAccount)
                {
                    hdnGLAccountIDParent.Value = cObj.GLAccountIDParent.ToString();
                    txtGLAccountCodeParent.Text = cObj.GLAccountCodeParent;
                    txtGLAccountNameParent.Text = cObj.GLAccountNameParent;
                }

                txtNamePredifined.Text = cObj.GLAccountNameSys;

                dcAccYear year = AccYearBL.GetCurrentAccYear(this.CompanyID);
                ddlOpBalYear.SelectedValue = year.AccYearID.ToString();

                txtOpBalance.Text = "0";
                ddlOpBalDrCr.SelectedValue = "0";

                if (cObj.GLAccountTypeID == (int)GLAccountTypeEnum.ControlAccount)
                {
                    dcGLAccountHistory accHisC = GLAccountHistoryBL.GetGLAccountHistoryByID_Control(this.CompanyID,year.AccYearID, cObj.GLAccountID);
                    if (accHisC != null)
                    {
                        txtOpBalance.Text = (accHisC.DebitAmtOpen + accHisC.CreditAmtOpen).ToString("#0.00");
                        
                        ddlOpBalDrCr.SelectedValue = accHisC.CreditAmtOpen > 0 ? "1" : "0";
                        if (accHisC.DebitAmtOpen + accHisC.CreditAmtOpen == 0)
                        {
                            ddlOpBalDrCr.SelectedValue = cObj.BalanceType.ToString();
                        }

                    }

                }
                else
                {
                    dcGLAccountHistory accHis = GLAccountHistoryBL.GetGLAccountHistoryByID(this.CompanyID,year.AccYearID, cObj.GLAccountID );
                    if (accHis != null)
                    {
                        txtOpBalance.Text = (accHis.DebitAmtOpen + accHis.CreditAmtOpen).ToString("#0.00");
                        ddlOpBalDrCr.SelectedValue = accHis.CreditAmtOpen > 0 ? "1" : "0";
                        if (accHis.DebitAmtOpen + accHis.CreditAmtOpen == 0)
                        {
                            ddlOpBalDrCr.SelectedValue = cObj.BalanceType.ToString();
                        }
                    }
                }


                //chkControlAccount.Checked = cObj.IsControlAccount;
                //chkShowAsSubLedger.Checked = cObj.ShowAsLedger;

                //SetIsGrossProfit(cObj.AccGLGroupIDParent);
                //chkIsGrossProfit.Checked = cObj.IsGrossProfit;


                  //chkIsOpenning.Checked = cObj.IsOpenningPeriod;
               //// lnkSetPass.NavigateUrl = @"~/Admin/SetPassword.aspx?uk=" + cObj.UserID.ToString();
               // lnkSetPass.NavigateUrl = @"javascript:tbopen(" + cObj.UserID.ToString() + ")";
                bStatus = true;
            }
            else
            {
                this.hdnGLAccountID.Value = "0";
                this.GLAccountID = 0;
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
                txtCode.Focus();
                return false;
            }

            if (Convert.ToInt32(ddlAccountType.SelectedValue) == 0)
            {
                //Helper.SetStatusMessage(lblMessage, "Please Enter Employee Name", MessageTypeEnum.InvalidData);
                this.SetPageMessage("Please select account type.", MessageTypeEnum.InvalidData);
                this.ShowPageMessage(lblMessage, true);
                ddlAccountType.Focus();
                return false;
            }

            if (Convert.ToInt32(ddlAccountType.SelectedValue) == (int)GLAccountTypeEnum.SubAccount)
            {
                if (Convert.ToInt32(hdnGLAccountIDParent.Value) == 0)
                {
                    //Helper.SetStatusMessage(lblMessage, "Please Enter Employee Name", MessageTypeEnum.InvalidData);
                    this.SetPageMessage("Please select parent account.", MessageTypeEnum.InvalidData);
                    this.ShowPageMessage(lblMessage, true);
                    txtGLAccountCodeParent.Focus();
                    return false;
                }
            }
            else
            {
                if (Convert.ToInt32(hdnGLGroupID.Value) == 0)
                {
                    //Helper.SetStatusMessage(lblMessage, "Please Enter Employee Name", MessageTypeEnum.InvalidData);
                    this.SetPageMessage("Please select parent GL Group.", MessageTypeEnum.InvalidData);
                    this.ShowPageMessage(lblMessage, true);
                    txtGLGroupName.Focus();
                    return false;
                }
            }


            int glGroupID = Convert.ToInt32(hdnGLGroupID.Value);
            int glClassID = Convert.ToInt32(hdnGLClassID.Value);
            int glGroupClassID = Convert.ToInt32(hdnGLGroupClassID.Value);

            int glGroupIDEdit = Convert.ToInt32(hdnGLGroupIDEdit.Value);
            int glClassIDEdit = Convert.ToInt32(hdnGLClassIDEdit.Value);
            int glGroupClassIDEdit = Convert.ToInt32(hdnGLGroupClassIDEdit.Value);



            dcAccSettings accSettings = AccSettingsBL.GetAccSettingByCompanyID(this.CompanyID);

            if (EditMode == FormDataMode.Add)
            {

                if (GLAccountBL.IsGLAccountCodeExists(this.CompanyID, txtCode.Text.Trim()))
                {
                    //Helper.SetStatusMessage(lblMessage, "Employee Already Exists", MessageTypeEnum.InvalidData);
                    this.SetPageMessage("GL Account Code already exists", MessageTypeEnum.InvalidData);
                    this.ShowPageMessage(lblMessage, true);
                    txtCode.Focus();
                    return false;
                }

                if (GLAccountBL.IsGLAccountNameExists(this.CompanyID, txtName.Text.Trim()))
                {
                    //Helper.SetStatusMessage(lblMessage, "Employee Already Exists", MessageTypeEnum.InvalidData);
                    this.SetPageMessage("GL Account Name already exists", MessageTypeEnum.InvalidData);
                    this.ShowPageMessage(lblMessage, true);
                    txtName.Focus();
                    return false;
                }
                //moni
                if (GLAccountBL.IsGLAccountNameExistsGroupName(this.CompanyID, txtName.Text.Trim()))
                {
                    //Helper.SetStatusMessage(lblMessage, "Employee Already Exists", MessageTypeEnum.InvalidData);
                    this.SetPageMessage("GL Account Name already exists Group Name ", MessageTypeEnum.InvalidData);
                    this.ShowPageMessage(lblMessage, true);
                    txtName.Focus();
                    return false;
                }
               
                if (glClassID == (int)GLClassEnum.ProfitAndLoss)
                {
                    if (accSettings.AllowGrpAccUnderPLGrp == false)
                    {
                        this.SetPageMessage("Account Create Not Allowed Under Profit And Loss", MessageTypeEnum.InvalidData);
                        this.ShowPageMessage(lblMessage, true);
                        txtGLGroupName.Focus();
                        return false;
                    }
                }



            }
            else if (EditMode == FormDataMode.Edit)
            {
                if (GLAccountBL.IsGLAccountCodeExists(this.CompanyID, txtCode.Text.Trim(), this.GLAccountID))
                {
                    //Helper.SetStatusMessage(lblMessage, "Employee Already Exists", MessageTypeEnum.InvalidData);
                    this.SetPageMessage("GL Account Code already exists", MessageTypeEnum.InvalidData);
                    this.ShowPageMessage(lblMessage, true);
                    txtCode.Focus();
                    return false;

                }


                if (GLAccountBL.IsGLAccountNameExists(this.CompanyID, txtName.Text.Trim(), this.GLAccountID))
                {
                    //Helper.SetStatusMessage(lblMessage, "Employee Already Exists", MessageTypeEnum.InvalidData);
                    this.SetPageMessage("GL Account Name already exists", MessageTypeEnum.InvalidData);
                    this.ShowPageMessage(lblMessage, true);
                    txtName.Focus();
                    return false;

                }

                if (glClassIDEdit != glClassID)
                {
                    this.SetPageMessage("Account Cannot move to different GL Class!", MessageTypeEnum.InvalidData);
                    this.ShowPageMessage(lblMessage, true);
                    txtGLGroupName.Focus();
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
            SaveTask();
        } 


        private bool SaveData()
        {
            bool bStatus = false;
            int newGLAccountID = 0;

            dcGLAccount cObj = new dcGLAccount();
            cObj.CompanyID = this.CompanyID;
            cObj.GLAccountID = this.GLAccountID;
            cObj.GLAccountName = txtName.Text.Trim();
            cObj.GLAccountNameB = txtNameB.Text.Trim();
            cObj.GLAccountCode = txtCode.Text.Trim();

            cObj.GLAccountTypeID = Convert.ToInt32(ddlAccountType.SelectedValue);
            if (cObj.GLAccountTypeID == (int)GLAccountTypeEnum.SubAccount)
            {
                cObj.GLAccountIDParent = Convert.ToInt32(hdnGLAccountIDParent.Value);
                dcGLAccount accParent = GLAccountBL.GetGLAccountByID(cObj.CompanyID, cObj.GLAccountIDParent);
                cObj.GLGroupID = accParent.GLGroupID;
            }
            else
            {
                cObj.GLGroupID = Convert.ToInt32(hdnGLGroupID.Value);
                cObj.GLAccountIDParent = 0;
            }

            cObj.BalanceType = Convert.ToInt32(ddlBalanceType.SelectedValue);

            //update op bal for : normal and sub acc
            cObj.GLAccountHistory = null;
            if (cObj.GLAccountTypeID != (int)GLAccountTypeEnum.ControlAccount)
            {
                int opBalYearID = Convert.ToInt32(ddlOpBalYear.SelectedValue);
                decimal opBal = Conversion.StringToDecimal(txtOpBalance.Text);

                cObj.GLAccountHistory = new dcGLAccountHistory();
                cObj.GLAccountHistory.AccYearID = opBalYearID;
                cObj.GLAccountHistory.GLAccountID = cObj.GLAccountID;
                cObj.CompanyID = this.CompanyID;
                if (Convert.ToInt32(ddlOpBalDrCr.SelectedValue) == (int)DebitCreditEnum.Credit)
                {
                    cObj.GLAccountHistory.CreditAmtOpen = opBal;
                    cObj.GLAccountHistory.DebitAmtOpen = 0;
                }
                else
                {
                    cObj.GLAccountHistory.CreditAmtOpen = 0;
                    cObj.GLAccountHistory.DebitAmtOpen = opBal;
                }
            }

            bool isAdd = EditMode == FormDataMode.Add ? true : false;
            newGLAccountID = GLAccountBL.Save(cObj, isAdd, this.DbContext);

            if (newGLAccountID > 0)
            {
                saveMsg = isAdd ? "New GL Account saved successfully." : "Edited GL Account saved successfully.";
                bStatus = true;
            }
            else
            {
                saveMsg = "Error Occured! Not Saved";
            }


            if (bStatus)
            {
                this.GLAccountID = newGLAccountID;
            }
            return bStatus;
        }

        protected void ddlAccountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //FillCombo_ControlAccount();
            //AccountTypeSettings();
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
