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
using PG.DBClass.AccountingDC.GeneralLedgerDC;
using PG.BLLibrary.AccountingBL;
using PG.DBClass.AccountingDC.AccEnums;
using PG.BLLibrary.AccountingBL.GeneralLedgerBL;

namespace PG.Web.Accounting
{
    public partial class InstrumentReceive : BagePage
    {
        int CompanyID = 0;
        int InstrumentID = 0;
        string ViewStateKey = "InstrumentID";
        string ViewStateKeyPrev = "InstrumentID_Prev";

        string saveMsg = string.Empty;

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
            this.InstrumentID = base.GetPageQueryInteger("id");

            if (!IsPostBack) //first Time
            {
                FillCombo();




                if (this.InstrumentID == 0) //not query string
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
                this.InstrumentID = int.Parse(ViewState[ViewStateKey].ToString());
            }
            SetHyperLink();
            this.ShowPageMessage(this.lblMessage);
            // Response.Write("UserID : " + this.UserID.ToString());
        }

        private void SetHyperLink()
        {

            //new button
            //string hLink = "javascript:tbopenSalInfo("+ this.InstrumentID.ToString() +")";
            //if (base.PageMode == PG.Core.Web.PageModeEnum.InTab)
            //{
            //    hLink = "javascript:tbopenSalInfo(" + this.InstrumentID.ToString() + ")";
            //    //this.btnSalaryInfo = string.Empty;
            //    this.btnSalaryInfo.Attributes.Add("onclick", hLink);
            //}
            //else
            //{
            //    hLink = "~/Master/EmpSalaryInfo.aspx?eid=" + this.InstrumentID.ToString();
            //    //this.btnAddNew.PostBackUrl = hLink;
            //    //this.btnAddNew.OnClientClick = string.Empty;
            //    this.btnSalaryInfo.Attributes.Add("onclick", hLink);
            //}

        }


        private void FillCombo()
        {
            ddlInstrumentType.DataTextField = "InstrumentTypeName";
            ddlInstrumentType.DataValueField = "InstrumentTypeID";
            ddlInstrumentType.AppendDataBoundItems = true;
            ddlInstrumentType.DataSource = InstrumentTypeBL.GetInstrumentTypeList();
            ddlInstrumentType.DataBind();

            ddlInstrumentStatus.DataTextField = "InstrumentStatusName";
            ddlInstrumentStatus.DataValueField = "InstrumentStatusID";
            ddlInstrumentStatus.AppendDataBoundItems = true;
            ddlInstrumentStatus.DataSource = InstrumentStatusBL.GetInstrumentStatusList();
            ddlInstrumentStatus.DataBind();

            ddlInstrumentMode.DataTextField = "InstrumentModeName";
            ddlInstrumentMode.DataValueField = "InstrumentModeID";
            ddlInstrumentMode.AppendDataBoundItems = true;
            ddlInstrumentMode.DataSource = InstrumentModeBL.GetInstrumentModeList();
            ddlInstrumentMode.DataBind();

        }


        private void SetControl(FormDataMode dataMode)
        {
            bool isEnabled = false;

            if (dataMode == FormDataMode.Add | dataMode == FormDataMode.Edit)
            {
                isEnabled = true;
            }


            txtInstrumentNo.Enabled = isEnabled;
            txtInstrumentDate.Enabled = isEnabled;

            txtIssueName.Enabled = isEnabled;
            txtBankName.Enabled = isEnabled;
            txtBranchName.Enabled = isEnabled;

            txtInstrumentAmt.Enabled = isEnabled;

            ddlInstrumentMode.Enabled = isEnabled;
            ddlInstrumentType.Enabled = isEnabled;

            ddlInstrumentStatus.Enabled = isEnabled;
            txtInstrumentStatusDate.Enabled = isEnabled;


            //buttons
            btnAddNew.Visible = !isEnabled;
            btnEdit.Visible = !isEnabled;
            btnDelete.Visible = !isEnabled;

            btnSave.Visible = isEnabled;
            btnCancel.Visible = isEnabled;

            SetControlPager();
        }


        private void SetControlPager()
        {
            btnGridPageGoTo.Style.Add("display", "none");
        }


        private void ReadTask()
        {
            lblHeader.Text = "Instrument : View";
            this.EditMode = FormDataMode.Read;
            //this.hdnPaymentReqID.Value = this.PaymentReqID.ToString();
            ReadData(this.InstrumentID);
            ReadDetails(this.InstrumentID);
            ViewState[ViewStateKey] = this.InstrumentID.ToString();

            SetControl(FormDataMode.Read);


            //var c = new { Name = "Name", ID = 344 };
        }


        private void AddTask()
        {
            ViewState[ViewStateKeyPrev] = this.InstrumentID.ToString();

            txtInstrumentNo.Text = "";
            txtInstrumentDate.Text = "";

            txtIssueName.Text = "";
            txtBankName.Text = "";
            txtBranchName.Text = "";

            txtInstrumentAmt.Text = "";

            txtInstrumentStatusDate.Text = "";

            //ddlInstrumentMode.Text = "";
            //ddlInstrumentType.Text = "";

            //ddlInstrumentStatus.Enabled = isEnabled;

            this.EditMode = FormDataMode.Add;
            this.IsDirty = false;
            ViewState[ViewStateKey] = "0";
            this.hdnInstrumentID.Value = "0";
            //lblMode.Text = "Mode: Add";
            lblHeader.Text = "Instruement : New";

            BindGridData(new List<dcJournalDetIns>());
            txtInstrumentNo.Focus();

            SetControl(FormDataMode.Add);
            Helper.SetStatusMessage(lblMessage, string.Empty, MessageTypeEnum.None);
        }

        private void EditTask()
        {
            this.EditMode = FormDataMode.Edit;
            //lblMode.Text = "Mode: Edit";
            lblHeader.Text = "Instruement : Edit";
            //this.SetMasterHeader("Edit User");
            ReadData(this.InstrumentID);
            ReadDetails(this.InstrumentID);
            ViewState[ViewStateKey] = this.InstrumentID.ToString();
            //lnkAddNew.Visible = true;
            txtInstrumentNo.Focus();
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
            if (this.InstrumentID > 0)
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
                    this.InstrumentID = prevID;
                    ReadTask();
                }
                else
                {
                    this.InstrumentID = 0;
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

                string redirectURL = "~/Accounting/Instrument.aspx?id=" + this.InstrumentID.ToString();
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


        private bool ReadData(int pInstrumentID)
        {
            dcInstrument cObj = InstrumentBL.GetInstrumentByID(this.CompanyID, pInstrumentID);

            bool bStatus = false;
            if (cObj != null)
            {
                this.InstrumentID = cObj.InstrumentID;
                this.hdnInstrumentID.Value = cObj.InstrumentID.ToString();

                ddlInstrumentMode.SelectedValue = cObj.InstrumentModeID.ToString();
                ddlInstrumentType.SelectedValue = cObj.InstrumentTypeID.ToString();
                ddlInstrumentStatus.SelectedValue = cObj.InstrumentStatusID.ToString();

                txtInstrumentNo.Text = cObj.InstrumentNo;
                txtInstrumentDate.Text = cObj.InstrumentDate.Value.ToString("dd-MMM-yyyy");

                txtIssueName.Text = cObj.IssueName;
                txtBankName.Text = cObj.BankName;
                txtBranchName.Text = cObj.BranchName;


                txtInstrumentAmt.Text = cObj.InstrumentAmt.ToString("#0.00");
                txtInstrumentStatusDate.Text = "";
                if (cObj.InstrumentStatusID == (int)InstrumentStatusEnum.Cleared)
                {
                    txtInstrumentStatusDate.Text = Conversion.DateTimeNullToEmpty(cObj.InstrumentStatusDate, "dd-MMM-yyyy");
                }


                //chkIsOpenning.Checked = cObj.IsOpenningPeriod;
                //// lnkSetPass.NavigateUrl = @"~/Admin/SetPassword.aspx?uk=" + cObj.UserID.ToString();
                // lnkSetPass.NavigateUrl = @"javascript:tbopen(" + cObj.UserID.ToString() + ")";
                bStatus = true;
            }
            else
            {
                this.hdnInstrumentID.Value = "0";
                this.InstrumentID = 0;
                bStatus = false;
                //MMS.Globals.ShowMessagePage(MMS.MessageTypeEnum.Error, "User ID not Found!", "");
            }
            return bStatus;

        }


        private bool ReadDetails(int pInstrumentID)
        {
            bool bStatus = false;

            List<dcJournalDetIns> listData = JournalDetInsBL.GetJournalDetInsListByInstrumentID(this.CompanyID, pInstrumentID); ;
            //listData.Clear();

            BindGridData(listData);
            SetGridInfo(listData.Count);



            bStatus = true;
            return bStatus;
        }


        private void BindGridData(List<dcJournalDetIns> listData)
        {
            int pageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);
            if (pageSize == 0)
            {
                GridView1.AllowPaging = false;
                GridView1.PageIndex = 0;
            }
            else
            {
                GridView1.AllowPaging = true;
                GridView1.PageSize = pageSize;
            }
            int rowCount = listData.Count;
            GridView1.DataSource = listData;
            GridView1.DataBind();
            GridView1.CssClass = "grid";
        }


        public void SetGridInfo(int rowCount)
        {
            txtGridPageNo.Text = "0";
            lblGridPageInfo.Text = " of 0";
            if (GridView1.PageCount > 0)
            {
                txtGridPageNo.Text = (GridView1.PageIndex + 1).ToString();
                lblGridPageInfo.Text = "of " + GridView1.PageCount.ToString();
            }

            hdnRowCount.Value = rowCount.ToString();

            int startRow = 0;
            int endRow = 0;

            int pageSize = GridView1.AllowPaging ? GridView1.PageSize : rowCount;

            if (rowCount > 0)
            {
                startRow = (GridView1.PageIndex * pageSize);
                endRow = startRow + pageSize;
                endRow = endRow > rowCount ? rowCount : endRow;

                startRow = startRow + 1;
            }


            if (rowCount > 1)
            {
                lblTotal.Text = string.Format("Rows: {0}-{1} of {2}", startRow, endRow, rowCount);
            }
            else
            {
                lblTotal.Text = string.Format("Rows: {0} of {0}", rowCount);
            }


        }


        private bool CheckData()
        {
            string insNo = txtInstrumentNo.Text.Trim();

            if (insNo == string.Empty)
            {
                //Helper.SetStatusMessage(lblMessage, "Please Enter Employee Name", MessageTypeEnum.InvalidData);
                this.SetPageMessage("Please Enter Instrument No", MessageTypeEnum.InvalidData);
                this.ShowPageMessage(lblMessage, true);
                txtInstrumentNo.Focus();
                return false;
            }
            int insModeID = Convert.ToInt32(ddlInstrumentMode.SelectedValue);
            int insTypeID = Convert.ToInt32(ddlInstrumentType.SelectedValue);
            int insStatusID = Convert.ToInt32(ddlInstrumentStatus.SelectedValue);


            DateTime dt;
            if (!DateTime.TryParse(txtInstrumentDate.Text, out dt))
            {
                //Helper.SetStatusMessage(lblMessage, "Please Enter Defination Name", MessageTypeEnum.InvalidData);
                this.SetPageMessage("Please Enter Date", MessageTypeEnum.InvalidData);
                this.ShowPageMessage(lblMessage, true);
                txtInstrumentDate.Focus();
                return false;
            }

            if (insStatusID == (int)InstrumentStatusEnum.Cleared)
            {

                if (!DateTime.TryParse(txtInstrumentStatusDate.Text, out dt))
                {
                    //Helper.SetStatusMessage(lblMessage, "Please Enter Defination Name", MessageTypeEnum.InvalidData);
                    this.SetPageMessage("Please Enter Cleared Date", MessageTypeEnum.InvalidData);
                    this.ShowPageMessage(lblMessage, true);
                    txtInstrumentStatusDate.Focus();
                    return false;
                }

            }



            //if (txtName.Text.Trim() == string.Empty)
            //{
            //    //Helper.SetStatusMessage(lblMessage, "Please Enter Employee Name", MessageTypeEnum.InvalidData);
            //    this.SetPageMessage("Please Enter Name", MessageTypeEnum.InvalidData);
            //    this.ShowPageMessage(lblMessage, true);
            //    txtCode.Focus();
            //    return false;
            //}


            //if (Convert.ToInt32(ddlCategory.SelectedValue) == 0)
            //{
            //    this.SetPageMessage("Please Select Category", MessageTypeEnum.InvalidData);
            //    this.ShowPageMessage(lblMessage, true);
            //    ddlCategory.Focus();
            //    return false;
            //}


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
                if (InstrumentBL.IsInstrumentNoExists(this.CompanyID, insNo, insModeID, insTypeID))
                {
                    //Helper.SetStatusMessage(lblMessage, "Employee Already Exists", MessageTypeEnum.InvalidData);
                    this.SetPageMessage("Instrument No already exists!", MessageTypeEnum.InvalidData);
                    this.ShowPageMessage(lblMessage, true);
                    txtInstrumentNo.Focus();
                    return false;

                }
            }
            else if (EditMode == FormDataMode.Edit)
            {
                if (InstrumentBL.IsInstrumentNoExists(this.CompanyID, insNo, insModeID, insTypeID, this.InstrumentID))
                {
                    //Helper.SetStatusMessage(lblMessage, "Employee Already Exists", MessageTypeEnum.InvalidData);
                    this.SetPageMessage("Instrument No already exists!", MessageTypeEnum.InvalidData);
                    this.ShowPageMessage(lblMessage, true);
                    txtInstrumentNo.Focus();
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
            int newInstrumentID = 0;

            dcInstrument cObj = new dcInstrument();

            cObj.CompanyID = this.CompanyID;
            cObj.InstrumentID = this.InstrumentID;


            cObj.InstrumentModeID = Convert.ToInt32(InstrumentModeEnum.Receive);
            cObj.InstrumentTypeID = Convert.ToInt32(ddlInstrumentType.SelectedValue);

            cObj.InstrumentNo = txtInstrumentNo.Text.Trim();
            cObj.InstrumentDate = Conversion.StringToDateORNull(txtInstrumentDate.Text);
            cObj.IssueName = txtIssueName.Text;
            cObj.BankName = txtBankName.Text;
            cObj.BranchName = txtBranchName.Text;

            cObj.InstrumentAmt = Conversion.StringToDecimal(txtInstrumentAmt.Text);

            cObj.InstrumentStatusID = Convert.ToInt32(ddlInstrumentStatus.SelectedValue);

            cObj.InstrumentStatusDate = null;
            if (cObj.InstrumentStatusID == (int)InstrumentStatusEnum.Cleared)
            {
                cObj.InstrumentStatusDate = Conversion.StringToDateORNull(txtInstrumentStatusDate.Text);
            }

            bool isAdd = EditMode == FormDataMode.Add;

            newInstrumentID = InstrumentBL.Save(cObj, isAdd);
            if (newInstrumentID > 0)
            {
                bStatus = true;
                saveMsg = isAdd ? "New Instrument saved successfully." : "Edited Instrument saved successfully.";
            }

            if (bStatus)
            {
                InstrumentID = newInstrumentID;
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

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string strD = DataBinder.Eval(e.Row.DataItem, "JournalID").ToString(); ;
                HyperLink lnk = (HyperLink)e.Row.Cells[0].Controls[0];

                string hLink = "javascript:tbopen(" + strD + ")";
                if (base.PageMode == PG.Core.Web.PageModeEnum.InTab)
                {
                    hLink = "javascript:tbopen(" + strD + ")";
                }
                else
                {
                    hLink = "~/Accounting/Journal.aspx?id=" + strD;
                }
                //lnk.NavigateUrl = "~/Admin/UserAddEdit.aspx?UserID=" + strD;
                lnk.NavigateUrl = hLink;


                //  LinkButton lnkBtn = (LinkButton)e.Row.Cells[4].Controls[0];
                // lnkBtn.OnClientClick = "if(!confirm('Are you sure you want to delete this?')) return false;";
            }

            if (e.Row.RowType == DataControlRowType.Pager)
            {
                e.Row.Visible = false;
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void btnGridPageGoTo_Click(object sender, EventArgs e)
        {

        }

        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnGridPageFirst_Click(object sender, EventArgs e)
        {

        }

        protected void btnGridPagePrev_Click(object sender, EventArgs e)
        {

        }

        protected void btnGridPageNext_Click(object sender, EventArgs e)
        {

        }

        protected void btnGridPageLast_Click(object sender, EventArgs e)
        {

        }
    }
}