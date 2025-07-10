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
using PG.BLLibrary.AccountingBL;




namespace PG.Web.Accounting
{


    public partial class AccYear : BagePage
    {
        int CompanyID = 0;

        int AccYearID = 0;
        string ViewStateKey = "AccYearID";
        string ViewStateKeyPrev = "AccYearID_Prev";

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

            this.AccYearID = base.GetPageQueryInteger("id");

            if (!IsPostBack) //first Time
            {
                FillCombo();
                if (this.AccYearID == 0) //not query string
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
                this.AccYearID = int.Parse(ViewState[ViewStateKey].ToString());
            }
            SetHyperLink();
            this.ShowPageMessage(this.lblMessage);
           // Response.Write("UserID : " + this.UserID.ToString());
        }

        private void SetHyperLink()
        {

            //new button
            //string hLink = "javascript:tbopenSalInfo("+ this.AccYearID.ToString() +")";
            //if (base.PageMode == PG.Core.Web.PageModeEnum.InTab)
            //{
            //    hLink = "javascript:tbopenSalInfo(" + this.AccYearID.ToString() + ")";
            //    //this.btnSalaryInfo = string.Empty;
            //    this.btnSalaryInfo.Attributes.Add("onclick", hLink);
            //}
            //else
            //{
            //    hLink = "~/Master/EmpSalaryInfo.aspx?eid=" + this.AccYearID.ToString();
            //    //this.btnAddNew.PostBackUrl = hLink;
            //    //this.btnAddNew.OnClientClick = string.Empty;
            //    this.btnSalaryInfo.Attributes.Add("onclick", hLink);
            //}

        }


        private void FillCombo()
        {

            //this.ddlMonth.Items.Clear();
            //this.ddlMonth.Items.Add(new ListItem("(select)","0"));
            //this.ddlMonth.Items.Add(new ListItem("January", "1"));
            //this.ddlMonth.Items.Add(new ListItem("February", "2"));
            //this.ddlMonth.Items.Add(new ListItem("March", "3"));
            //this.ddlMonth.Items.Add(new ListItem("April", "4"));
            //this.ddlMonth.Items.Add(new ListItem("May", "5"));
            //this.ddlMonth.Items.Add(new ListItem("June", "6"));
            //this.ddlMonth.Items.Add(new ListItem("July", "7"));
            //this.ddlMonth.Items.Add(new ListItem("August", "8"));
            //this.ddlMonth.Items.Add(new ListItem("September", "9"));
            //this.ddlMonth.Items.Add(new ListItem("October", "10"));
            //this.ddlMonth.Items.Add(new ListItem("November", "11"));
            //this.ddlMonth.Items.Add(new ListItem("December", "12"));



            //ddlTaxInfo.Items.Clear();
            //ddlTaxInfo.Items.Add(new ListItem("(select)", "0"));
            //ddlTaxInfo.AppendDataBoundItems = true;

            //ddlTaxInfo.DataTextField = "IncomeTaxInfoName";
            //ddlTaxInfo.DataValueField = "IncomeTaxInfoID";
            //ddlTaxInfo.AppendDataBoundItems = true;
            //ddlTaxInfo.DataSource = BLLibrary.PayRollBL.IncomeTaxInfoBL.GetIncomeTaxInfoList(this.DbContext);
            //ddlTaxInfo.DataBind();



 

        }


        private void SetControl(FormDataMode dataMode)
        {
            bool isEnabled = false;

            if (dataMode == FormDataMode.Add | dataMode == FormDataMode.Edit)
            {
                isEnabled = true;
            }


            txtAccYearName.Enabled = isEnabled;
            txtStartDate.Enabled = isEnabled;
            txtEndDate.Enabled = isEnabled;
            txtOpBalDate.Enabled = isEnabled;

            ddlIsClosed.Enabled = isEnabled;

            //txtTotRequisitionAmt.Enabled = isEnabled;

            //buttons
            btnAddNew.Visible = !isEnabled;
            btnEdit.Visible = !isEnabled;
            btnDelete.Visible = !isEnabled;

            btnSave.Visible = isEnabled;
            btnCancel.Visible = isEnabled;

        }



        private void ReadTask()
        {
            lblHeader.Text = "Accounting Year : View";
            this.EditMode = FormDataMode.Read;
            //this.hdnPaymentReqID.Value = this.PaymentReqID.ToString();
            ReadData(this.AccYearID);

            ViewState[ViewStateKey] = this.AccYearID.ToString();

            SetControl(FormDataMode.Read);


            //var c = new { Name = "Name", ID = 344 };
        }


        private void AddTask()
        {
            ViewState[ViewStateKeyPrev] = this.AccYearID.ToString();

            txtAccYearName.Text = "";
            ddlIsClosed.SelectedValue = "0";

            this.EditMode = FormDataMode.Add;
            this.IsDirty = false;
            ViewState[ViewStateKey] = "0";
            this.hdnAccYearID.Value = "0";
            //lblMode.Text = "Mode: Add";
            lblHeader.Text = "Accounting Year : New";
            SetNextYear();
            txtAccYearName.Focus();

            SetControl(FormDataMode.Add);
            Helper.SetStatusMessage(lblMessage, string.Empty, MessageTypeEnum.None);
        }

        private void EditTask()
        {
            this.EditMode = FormDataMode.Edit;
            //lblMode.Text = "Mode: Edit";
            lblHeader.Text = "Accounting Year: Edit";
            //this.SetMasterHeader("Edit User");
            ReadData(this.AccYearID);
            ViewState[ViewStateKey] = this.AccYearID.ToString();
            //lnkAddNew.Visible = true;
            txtAccYearName.Focus();
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
            if (this.AccYearID > 0)
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
                    this.AccYearID = prevID;
                    ReadTask();
                }
                else
                {
                    this.AccYearID = 0;
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

                string redirectURL = "~/Accounting/AccYear.aspx?id=" + this.AccYearID.ToString();
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


        private bool ReadData(int pAccYearID)
        {
            dcAccYear cObj = AccYearBL.GetAccYearByID(this.CompanyID, pAccYearID);

            bool bStatus;
            if (cObj != null)
            {
                this.AccYearID = cObj.AccYearID;
                this.hdnAccYearID.Value = cObj.AccYearID.ToString();

                txtAccYearName.Text = cObj.AccYearName;
                txtStartDate.Text = cObj.YearStartDate.Value.ToString("dd-MMM-yyyy");
                txtEndDate.Text = cObj.YearEndDate.Value.ToString("dd-MMM-yyyy");

                txtOpBalDate.Text = cObj.OpBalanceDate.Value.ToString("dd-MMM-yyyy");

                ddlIsClosed.SelectedValue = cObj.IsClosed ? "1" : "0";
                //chkIsOpenning.Checked = cObj.IsOpenningPeriod;
               //// lnkSetPass.NavigateUrl = @"~/Admin/SetPassword.aspx?uk=" + cObj.UserID.ToString();
               // lnkSetPass.NavigateUrl = @"javascript:tbopen(" + cObj.UserID.ToString() + ")";
                bStatus = true;
            }
            else
            {
                this.hdnAccYearID.Value = "0";
                this.AccYearID = 0;
                bStatus = false;
                //MMS.Globals.ShowMessagePage(MMS.MessageTypeEnum.Error, "User ID not Found!", "");
            }
            return bStatus;

        }


        private bool CheckData()
        {

            if (txtAccYearName.Text.Trim() == string.Empty)
            {
                //Helper.SetStatusMessage(lblMessage, "Please Enter Employee Name", MessageTypeEnum.InvalidData);
                this.SetPageMessage("Please Enter Year Name", MessageTypeEnum.InvalidData);
                this.ShowPageMessage(lblMessage, true);
                txtAccYearName.Focus();
                return false;
            }

            DateTime dt;

            if (!DateTime.TryParse(txtStartDate.Text, out dt))
            {
                //Helper.SetStatusMessage(lblMessage, "Please Enter Join Date", MessageTypeEnum.InvalidData);
                this.SetPageMessage("Please Year Start Date", MessageTypeEnum.InvalidData);
                this.ShowPageMessage(lblMessage, true);
                txtStartDate.Focus();
                return false;
            }

            if (!DateTime.TryParse(txtEndDate.Text, out dt))
            {
                //Helper.SetStatusMessage(lblMessage, "Please Enter Join Date", MessageTypeEnum.InvalidData);
                this.SetPageMessage("Please Year End Date", MessageTypeEnum.InvalidData);
                this.ShowPageMessage(lblMessage, true);
                txtEndDate.Focus();
                return false;
            }

            if (!DateTime.TryParse(txtOpBalDate.Text, out dt))
            {
                //Helper.SetStatusMessage(lblMessage, "Please Enter Join Date", MessageTypeEnum.InvalidData);
                this.SetPageMessage("Please Year Op. Blanace Date", MessageTypeEnum.InvalidData);
                this.ShowPageMessage(lblMessage, true);
                txtOpBalDate.Focus();
                return false;
            }



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

                //if (BLLibrary.PayRollBL.SalaryPeriodBL.IsPeriodExists(month, year))
                //{
                //    //Helper.SetStatusMessage(lblMessage, "Employee Already Exists", MessageTypeEnum.InvalidData);
                //    this.SetPageMessage("Period Already Exists", MessageTypeEnum.InvalidData);
                //    this.ShowPageMessage(lblMessage, true);
                //    return false;

                //}
            }
            else if (EditMode == FormDataMode.Edit)
            {
                //if (BLLibrary.PayRollBL.SalaryPeriodBL.IsPeriodExists(month, year,this.AccYearID))
                //{
                //    //Helper.SetStatusMessage(lblMessage, "Employee Already Exists", MessageTypeEnum.InvalidData);
                //    this.SetPageMessage("Period Already Exists", MessageTypeEnum.InvalidData);
                //    this.ShowPageMessage(lblMessage, true);
                //    return false;

                //}
            }
            return true;
        }


        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            //Helper.SetStatusMessage(lblMessage, "", MessageTypeEnum.Successful);
            AddTask();
        }

        private void SetNextYear()
        {
            dcAccYear cObj =  AccYearBL.GetAccYearList(this.CompanyID).OrderByDescending(c=>c.YearEndDate).FirstOrDefault();
            if (cObj != null)
            {
                DateTime startDate = cObj.YearEndDate.Value.AddDays(1);
                DateTime endDate = startDate.AddYears(1).AddDays(-1);

                txtAccYearName.Text = startDate.Year.ToString();

                txtStartDate.Text = startDate.ToString("dd-MMM-yyyy");
                txtEndDate.Text = endDate.ToString("dd-MMM-yyyy");

                txtOpBalDate.Text = startDate.ToString("dd-MMM-yyyy");

                ddlIsClosed.SelectedValue = "0";


            }
        }
    

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveTask();
        } 


        private bool SaveData()
        {
            bool bStatus = false;
            int newAccYearID = 0;

            dcAccYear cObj = new dcAccYear();

            cObj.CompanyID = this.CompanyID;
            cObj.AccYearName = txtAccYearName.Text.Trim();
            cObj.YearStartDate = PG.Core.Utility.Conversion.StringToDate(txtStartDate.Text);
            cObj.YearEndDate = PG.Core.Utility.Conversion.StringToDate(txtEndDate.Text);

            cObj.OpBalanceDate = PG.Core.Utility.Conversion.StringToDate(txtOpBalDate.Text);

            cObj.AccYearNo = cObj.YearStartDate.Value.Year;
            cObj.IsClosed = ddlIsClosed.SelectedValue == "1" ? true : false;
            


            if (EditMode == FormDataMode.Add)
            {
                //try
                {
                    newAccYearID = AccYearBL.Insert(cObj, this.DbContext);
                    if (newAccYearID > 0)
                    {
                        bStatus = true;
                        saveMsg = "New Year saved successfully.";
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
                    cObj.AccYearID = this.AccYearID;
                    //try
                    {
                        bStatus =   AccYearBL.Update(cObj,this.DbContext);
                        newAccYearID = cObj.AccYearID;
                        saveMsg = "Edited Year saved successfully.";
                    }
                    //catch (Exception e)
                    {
                        //    saveMsg = e.Message;
                    }
                }
            }

            if (bStatus)
            {
                AccYearID = newAccYearID;
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
