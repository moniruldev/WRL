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
using System.Web.Script.Serialization;
using System.Text;


using PG.Core;
using PG.Core.Web;
using PG.Core.DBBase;
using PG.Core.Extentions;
using PG.Core.DBFilters;

using PG.DBClass.AccountingDC;
using PG.BLLibrary.AccountingBL;


namespace PG.Web.Accounting
{


    public partial class AccSettings : BagePage
    {
        int AccSettingsID = 0;
        int CompanyID = 0;
        string ViewStateKey = "AccSettingsID";
        string ViewStateKeyPrev = "AccSettingsID_Prev";

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

            this.AccSettingsID = base.GetPageQueryInteger("id");
            this.CompanyID = base.GetPageQueryInteger("compid");

            if (!IsPostBack) //first Time
            {
                FillCombo();
                if (this.AccSettingsID == 0) //not query string
                {
                    this.AccSettingsID = AccSettingsBL.GetAccSettingByCompanyID(CompanyInfo.GetCompanyID()).AccSettingsID;
                    if (this.AccSettingsID > 0)
                    {
                        ReadTask();
                    }
                    else
                    {

                        AddTask();
                    }
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
                this.AccSettingsID = int.Parse(ViewState[ViewStateKey].ToString());
            }
            SetHyperLink();
            this.ShowPageMessage(this.lblMessage);
           // Response.Write("UserID : " + this.UserID.ToString());
        }

 

        private void SetHyperLink()
        {
            //new button
            //string hLink = "javascript:tbopenSalInfo("+ this.AccSettingsID.ToString() +")";
            //if (base.PageMode == PG.Core.Web.PageModeEnum.InTab)
            //{
            //    hLink = "javascript:tbopenSalInfo(" + this.AccSettingsID.ToString() + ")";
            //    //this.btnSalaryInfo = string.Empty;
            //    this.btnSalaryInfo.Attributes.Add("onclick", hLink);
            //}
            //else
            //{
            //    hLink = "~/Master/EmpSalaryInfo.aspx?eid=" + this.AccSettingsID.ToString();
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



            ddlAccYear.Items.Clear();
            ddlAccYear.Items.Add(new ListItem("(select)", "0"));
            ddlAccYear.AppendDataBoundItems = true;

            ddlAccYear.DataTextField = "AccYearName";
            ddlAccYear.DataValueField = "AccYearID";
            ddlAccYear.AppendDataBoundItems = true;
            ddlAccYear.DataSource = AccYearBL.GetAccYearList(CompanyInfo.GetCompanyID());
            ddlAccYear.DataBind();



 

        }


        private void SetControl(FormDataMode dataMode)
        {
            bool isEnabled = false;

            if (dataMode == FormDataMode.Add | dataMode == FormDataMode.Edit)
            {
                isEnabled = true;
            }


            ddlAccYear.Enabled = isEnabled;
            ddlReportExport.Enabled = isEnabled;
            ddlReportPrint.Enabled = isEnabled;
            ddlPDFPrint.Enabled = isEnabled;
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
            lblHeader.Text = "Accounting Settings : View";
            this.EditMode = FormDataMode.Read;
            //this.hdnPaymentReqID.Value = this.PaymentReqID.ToString();
            ReadData(this.AccSettingsID);

            ViewState[ViewStateKey] = this.AccSettingsID.ToString();

            SetControl(FormDataMode.Read);


            //var c = new { Name = "Name", ID = 344 };
        }


        private void AddTask()
        {
            ViewState[ViewStateKeyPrev] = this.AccSettingsID.ToString();

            //ddlIsClosed.SelectedValue = "0";

            this.EditMode = FormDataMode.Add;
            this.IsDirty = false;
            ViewState[ViewStateKey] = "0";
            this.hdnAccSettingsID.Value = "0";
            //lblMode.Text = "Mode: Add";
            lblHeader.Text = "Accounting Settings : New";
            SetNextYear();
            ddlAccYear.Focus();

            SetControl(FormDataMode.Add);
            Helper.SetStatusMessage(lblMessage, string.Empty, MessageTypeEnum.None);
        }

        private void EditTask()
        {
            this.EditMode = FormDataMode.Edit;
            //lblMode.Text = "Mode: Edit";
            lblHeader.Text = "Accounting Settings: Edit";
            //this.SetMasterHeader("Edit User");
            ReadData(this.AccSettingsID);
            ViewState[ViewStateKey] = this.AccSettingsID.ToString();
            //lnkAddNew.Visible = true;
            ddlAccYear.Focus();
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
            if (this.AccSettingsID > 0)
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
                    this.AccSettingsID = prevID;
                    ReadTask();
                }
                else
                {
                    this.AccSettingsID = 0;
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

                string redirectURL = "~/Accounting/AccSettings.aspx?id=" + this.AccSettingsID.ToString();
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


        private bool ReadData(int pAccSettingsID)
        {
             dcAccSettings cObj = AccSettingsBL.GetAccSettingID(pAccSettingsID);

            bool bStatus;
            if (cObj != null)
            {
                this.AccSettingsID = cObj.AccSettingsID;
                this.hdnAccSettingsID.Value = cObj.AccSettingsID.ToString();

                ddlAccYear.SelectedValue = cObj.AccYearID.ToString();
                ddlReportExport.SelectedValue = cObj.DisableReportExport ? "1" : "0";
                ddlReportPrint.SelectedValue = cObj.DisableReportPrint ? "1" : "0";
                ddlPDFPrint.SelectedValue = cObj.DisablePDFPrint ? "1" : "0";
                //chkIsOpenning.Checked = cObj.IsOpenningPeriod;
               //// lnkSetPass.NavigateUrl = @"~/Admin/SetPassword.aspx?uk=" + cObj.UserID.ToString();
               // lnkSetPass.NavigateUrl = @"javascript:tbopen(" + cObj.UserID.ToString() + ")";
                bStatus = true;
            }
            else
            {
                this.hdnAccSettingsID.Value = "0";
                this.AccSettingsID = 0;
                bStatus = false;
                //MMS.Globals.ShowMessagePage(MMS.MessageTypeEnum.Error, "User ID not Found!", "");
            }
            return bStatus;

        }


        private bool CheckData()
        {

            if (Convert.ToInt32(ddlAccYear.SelectedValue) == 0)
            {
                //Helper.SetStatusMessage(lblMessage, "Please Enter Employee Name", MessageTypeEnum.InvalidData);
                this.SetPageMessage("Please Select Year", MessageTypeEnum.InvalidData);
                this.ShowPageMessage(lblMessage, true);
                ddlAccYear.Focus();
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
                //if (BLLibrary.PayRollBL.SalaryPeriodBL.IsPeriodExists(month, year,this.AccSettingsID))
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
           // DBClass.Accounting.dcAccYear cObj = BLLibrary.AccountingBL.TaxYearBL.GetTaxYearList().FirstOrDefault();
            //if (cObj != null)
            {
                //int year = cObj.PeriodMonthNo == 12 ? cObj.PeriodYearNo + 1 : cObj.PeriodYearNo;
                //int month = cObj.PeriodMonthNo + 1;
                //month = month > 12 ? 1 : month;

                //txtYear.Text = year.ToString();
                //ddlMonth.SelectedValue = month.ToString();
                //txtPeriodName.Text = (new DateTime(year, month, 1)).ToString("MMMM-yyyy");

            }
        }
    

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveTask();
        } 


        private bool SaveData()
        {
            bool bStatus = false;
            int newAccSettingsID = 0;

            dcAccSettings cObj = new dcAccSettings();
            cObj.CompanyID = CompanyInfo.GetCompanyID();
            cObj.AccYearID = Convert.ToInt32(ddlAccYear.SelectedValue);
            cObj.DisableReportExport = ddlReportExport.SelectedValue=="1" ? true : false;
            cObj.DisableReportPrint = ddlReportPrint.SelectedValue == "1" ? true : false;
            cObj.DisablePDFPrint = ddlPDFPrint.SelectedValue == "1" ? true : false;
            if (EditMode == FormDataMode.Add)
            {
                //try
                {
                    newAccSettingsID = AccSettingsBL.Insert(cObj, this.DbContext);
                    if (newAccSettingsID > 0)
                    {
                        bStatus = true;
                        saveMsg = "New Settings saved successfully.";
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
                    cObj.AccSettingsID = this.AccSettingsID;
                    //try
                    {
                        bStatus = AccSettingsBL.Update(cObj,this.DbContext);
                        newAccSettingsID = cObj.AccSettingsID;
                        saveMsg = "Edited Settings saved successfully.";
                    }
                    //catch (Exception e)
                    {
                        //    saveMsg = e.Message;
                    }
                }
            }

            if (bStatus)
            {
                AccSettingsID = newAccSettingsID;
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

        protected void ddlAccYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

    }
}
