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

using PG.DBClass.AccountingDC.AccEnums;
using PG.DBClass.AccountingDC;
using PG.DBClass.AccountingDC.GeneralLedgerDC;
using PG.BLLibrary.AccountingBL;
using PG.BLLibrary.AccountingBL.GeneralLedgerBL;


namespace PG.Web.Accounting.GeneralLedger
{
    public partial class Reference : BagePage
    {
        int CompanyID = 0;

        int AccRefID = 0;
        string ViewStateKey = "AccRefID";
        string ViewStateKeyPrev = "AccRefID_Prev";

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

            this.AccRefID = base.GetPageQueryInteger("id");

            if (!IsPostBack) //first Time
            {
                FillCombo();

               

            
                if (this.AccRefID == 0) //not query string
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
                this.AccRefID = int.Parse(ViewState[ViewStateKey].ToString());
            }
            SetHyperLink();
            this.ShowPageMessage(this.lblMessage);
           // Response.Write("UserID : " + this.UserID.ToString());
        }

        private void SetHyperLink()
        {

            //new button
            //string hLink = "javascript:tbopenSalInfo("+ this.AccRefID.ToString() +")";
            //if (base.PageMode == PG.Core.Web.PageModeEnum.InTab)
            //{
            //    hLink = "javascript:tbopenSalInfo(" + this.AccRefID.ToString() + ")";
            //    //this.btnSalaryInfo = string.Empty;
            //    this.btnSalaryInfo.Attributes.Add("onclick", hLink);
            //}
            //else
            //{
            //    hLink = "~/Master/EmpSalaryInfo.aspx?eid=" + this.AccRefID.ToString();
            //    //this.btnAddNew.PostBackUrl = hLink;
            //    //this.btnAddNew.OnClientClick = string.Empty;
            //    this.btnSalaryInfo.Attributes.Add("onclick", hLink);
            //}

        }


        private void FillCombo()
        {
            ddlCategory.DataTextField = "AccRefCategoryName";
            ddlCategory.DataValueField = "AccRefCategoryID";
            ddlCategory.AppendDataBoundItems = true;
            ddlCategory.DataSource = AccRefCategoryBL.GetAccRefCategoryList(this.CompanyID, (int)AccRefTypeEnum.Reference);
            ddlCategory.DataBind();
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
            ddlCategory.Enabled = isEnabled;
           


            //buttons
            btnAddNew.Visible = !isEnabled;
            btnEdit.Visible = !isEnabled;
            btnDelete.Visible = !isEnabled;

            btnSave.Visible = isEnabled;
            btnCancel.Visible = isEnabled;

        }



        private void ReadTask()
        {
            lblHeader.Text = "Reference : View";
            this.EditMode = FormDataMode.Read;
            //this.hdnPaymentReqID.Value = this.PaymentReqID.ToString();
            ReadData(this.AccRefID);

            ViewState[ViewStateKey] = this.AccRefID.ToString();

            SetControl(FormDataMode.Read);


            //var c = new { Name = "Name", ID = 344 };
        }


        private void AddTask()
        {
            ViewState[ViewStateKeyPrev] = this.AccRefID.ToString();

            txtCode.Text = "";
            txtName.Text = "";
            



            this.EditMode = FormDataMode.Add;
            this.IsDirty = false;
            ViewState[ViewStateKey] = "0";
            this.hdnAccRefID.Value = "0";
            //lblMode.Text = "Mode: Add";
            lblHeader.Text = "Reference : New";
            txtCode.Focus();

            SetControl(FormDataMode.Add);
            Helper.SetStatusMessage(lblMessage, string.Empty, MessageTypeEnum.None);
        }

        private void EditTask()
        {
            this.EditMode = FormDataMode.Edit;
            //lblMode.Text = "Mode: Edit";
            lblHeader.Text = "Reference : Edit";
            //this.SetMasterHeader("Edit User");
            ReadData(this.AccRefID);
            ViewState[ViewStateKey] = this.AccRefID.ToString();
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
            if (this.AccRefID > 0)
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
                    this.AccRefID = prevID;
                    ReadTask();
                }
                else
                {
                    this.AccRefID = 0;
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

                string redirectURL = "~/Accounting/GeneralLedger/Reference.aspx?id=" + this.AccRefID.ToString();
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


        private bool ReadData(int pAccRefID)
        {
             dcAccRef cObj = AccRefBL.GetAccRefByID(this.CompanyID, pAccRefID);

            bool bStatus;
            if (cObj != null)
            {
                this.AccRefID = cObj.AccRefID;
                this.hdnAccRefID.Value = cObj.AccRefID.ToString();

                txtCode.Text = cObj.AccRefCode;
                txtName.Text = cObj.AccRefName;
                ddlCategory.SelectedValue = cObj.AccRefCategoryID.ToString();

                  //chkIsOpenning.Checked = cObj.IsOpenningPeriod;
               //// lnkSetPass.NavigateUrl = @"~/Admin/SetPassword.aspx?uk=" + cObj.UserID.ToString();
               // lnkSetPass.NavigateUrl = @"javascript:tbopen(" + cObj.UserID.ToString() + ")";
                bStatus = true;
            }
            else
            {
                this.hdnAccRefID.Value = "0";
                this.AccRefID = 0;
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


            if (Convert.ToInt32(ddlCategory.SelectedValue) == 0)
            {
                this.SetPageMessage("Please Select Category", MessageTypeEnum.InvalidData);
                this.ShowPageMessage(lblMessage, true);
                ddlCategory.Focus();
                return false;
            }


            //DateTime dt;

            //if (!DateTime.TryParse(txtStartDate.Text, out dt))
            //{
            //    //Helper.SetStatusMessage(lblMessage, "Please Enter Join Date", MessageTypeEnum.InvalidData);
            //    this.SetPageMessage("Please Year Start Date", MessageTypeEnum.InvalidData);
            //    this.ShowPageMessage(lblMessage, true);
            //    txtStartDate.Focus();
            //    return false;
            //}

            //if (!DateTime.TryParse(txtEndDate.Text, out dt))
            //{
            //    //Helper.SetStatusMessage(lblMessage, "Please Enter Join Date", MessageTypeEnum.InvalidData);
            //    this.SetPageMessage("Please Year End Date", MessageTypeEnum.InvalidData);
            //    this.ShowPageMessage(lblMessage, true);
            //    txtEndDate.Focus();
            //    return false;
            //}


            //if (ddlTaxInfo.SelectedValue == "0")
            //{
            //    //Helper.SetStatusMessage(lblMessage, "Please Enter Employee Name", MessageTypeEnum.InvalidData);
            //    this.SetPageMessage("Please Select Tax Info", MessageTypeEnum.InvalidData);
            //    this.ShowPageMessage(lblMessage, true);
            //    ddlTaxInfo.Focus();
            //    return false;
            //}



            //int month = Convert.ToInt32(ddlMonth.SelectedValue);
            //int year = PG.Core.Utility.Conversion.StringToInt(txtYear.Text);


            if (EditMode == FormDataMode.Add)
            {
                if (AccRefBL.IsAccRefCodeExists(this.CompanyID, txtCode.Text.Trim(),(int)AccRefTypeEnum.Reference))
                {
                    //Helper.SetStatusMessage(lblMessage, "Employee Already Exists", MessageTypeEnum.InvalidData);
                    this.SetPageMessage("Reference Code already exists!", MessageTypeEnum.InvalidData);
                    this.ShowPageMessage(lblMessage, true);
                    return false;

                }
            }
            else if (EditMode == FormDataMode.Edit)
            {
                if (AccRefBL.IsAccRefCodeExists(this.CompanyID, txtCode.Text.Trim(), (int)AccRefTypeEnum.Reference, this.AccRefID))
                {
                    //Helper.SetStatusMessage(lblMessage, "Employee Already Exists", MessageTypeEnum.InvalidData);
                    this.SetPageMessage("Reference Code already exists!", MessageTypeEnum.InvalidData);
                    this.ShowPageMessage(lblMessage, true);
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
            int newAccRefID = 0;

            dcAccRef cObj = new dcAccRef();

            cObj.AccRefCode = txtCode.Text.Trim();
            cObj.AccRefName = txtName.Text.Trim();
            cObj.AccRefCategoryID  = Convert.ToInt32(ddlCategory.SelectedValue);
            cObj.AccRefTypeID = (int)AccRefTypeEnum.Reference;
            cObj.CompanyID = this.CompanyID;

            if (EditMode == FormDataMode.Add)
            {
                //try
                {
                    newAccRefID = AccRefBL.Insert(cObj, this.DbContext);
                    if (newAccRefID > 0)
                    {
                        bStatus = true;
                        saveMsg = "New Reference saved successfully.";
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
                    cObj.AccRefID = this.AccRefID;
                    //try
                    {
                        bStatus = AccRefBL.Update(cObj, this.DbContext);
                        newAccRefID = cObj.AccRefID;
                        saveMsg = "Edited Reference saved successfully.";
                    }
                    //catch (Exception e)
                    {
                        //    saveMsg = e.Message;
                    }
                }
            }

            if (bStatus)
            {
                AccRefID = newAccRefID;
            }
            return bStatus;
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
