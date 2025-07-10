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
using PG.DBClass.SecurityDC;


namespace PG.Web.Accounting.GeneralLedger
{
    public partial class LocationwiseLedgerBinding : BagePage
    {
        int CompanyID = 0;
        int LocationGLAccountID = 0;
        string ViewStateKey = "LocationGLAccountID";
        string ViewStateKeyPrev = "LocationGLAccountID_Prev";

  

        string saveMsg = string.Empty;
        public string GLAccountServiceLink = PageLinks.AccountingLinks.GetLink_LocationGLAccount;
        public string GLGroupServiceLink = PageLinks.AccountingLinks.GetLink_GLGroup;



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
            this.LocationGLAccountID =base.GetPageQueryInteger("id");

            if (!IsPostBack) //first Time
            {
                //FillCombo();
                FillCombo_Location();

               
                
            
                if (this.LocationGLAccountID == 0) //not query string
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
                this.LocationGLAccountID = int.Parse(ViewState[ViewStateKey].ToString());
            }
            SetHyperLink();
            this.ShowPageMessage(this.lblMessage);
           // Response.Write("UserID : " + this.UserID.ToString());
        }

        private void FillCombo_Location()
        {
            int companyID = this.CompanyID;
            ddlLocation.Items.Clear();
            //ddlLocation.Items.Add(new ListItem("All", "0"));
            //ddlLocation.AppendDataBoundItems = true;

            ddlLocation.DataTextField = "LocationCodeName";//"LocationName";
            ddlLocation.DataValueField = "LocationID";
            //ddlLocation.DataSource = LocationBL.GetLocationList(companyID);
            //ddlLocation.DataBind();
            ddlLocation.DataSource = AppSecurity.GetValidLocationUserList(); //LocationBL.GetLocationList(this.CompanyID);
            ddlLocation.DataBind();
            //foreach (dcLocation loc in LocationBL.GetLocationList(companyID))
            //{
            //    ddlLocation.Items.Add(new ListItem(loc.LocationCode + " - " + loc.LocationName, loc.LocationID.ToString()));
            //}

            dcUser user = AppSecurity.GetUserInfoFromSession();
            int locID = user.LoginLocationID;
            ddlLocation.SelectedValue = locID.ToString();

            //int locID = AppSecurity.GetValidLocationUserList().Select(c => c.LocationID).FirstOrDefault();

            //if (ddlLocation.Items.Count < 3)
            //{
               // ddlLocation.SelectedValue = locID.ToString();

           // }

        }
        private void SetHyperLink()
        {

            //new button
            //string hLink = "javascript:tbopenSalInfo("+ this.LocationGLAccountID.ToString() +")";
            //if (base.PageMode == PG.Core.Web.PageModeEnum.InTab)
            //{
            //    hLink = "javascript:tbopenSalInfo(" + this.LocationGLAccountID.ToString() + ")";
            //    //this.btnSalaryInfo = string.Empty;
            //    this.btnSalaryInfo.Attributes.Add("onclick", hLink);
            //}
            //else
            //{
            //    hLink = "~/Master/EmpSalaryInfo.aspx?eid=" + this.LocationGLAccountID.ToString();
            //    //this.btnAddNew.PostBackUrl = hLink;
            //    //this.btnAddNew.OnClientClick = string.Empty;
            //    this.btnSalaryInfo.Attributes.Add("onclick", hLink);
            //}

        }


        private void SetControl(FormDataMode dataMode)
        {
            bool isEnabled = false;

            if (dataMode == FormDataMode.Add | dataMode == FormDataMode.Edit)
            {
                isEnabled = true;
            }

            txtGLGroup.Enabled = isEnabled;
            txtGLGroupName.Enabled = isEnabled;

            txtGLAccount.Enabled = isEnabled;
            txtGLAccountName.Enabled = isEnabled;
            ddlLocation.Enabled = isEnabled;
           


            //buttons
            btnAddNew.Visible = !isEnabled;
            btnEdit.Visible = !isEnabled;
            btnDelete.Visible = !isEnabled;

            btnSave.Visible = isEnabled;
            btnCancel.Visible = isEnabled;

        }



        private void ReadTask()
        {
            lblHeader.Text = "Location wise Ledger : View";
            this.EditMode = FormDataMode.Read;
            //this.hdnPaymentReqID.Value = this.PaymentReqID.ToString();
            ReadData(this.LocationGLAccountID);

            ViewState[ViewStateKey] = this.LocationGLAccountID.ToString();

            SetControl(FormDataMode.Read);


            //var c = new { Name = "Name", ID = 344 };
        }


        private void AddTask()
        {
            ViewState[ViewStateKeyPrev] = this.LocationGLAccountID.ToString();


            txtGLGroup.Text = "";
            txtGLGroupName.Text = "";
            txtGLAccount.Text = "";
            txtGLAccountName.Text = "";
            



            this.EditMode = FormDataMode.Add;
            this.IsDirty = false;
            ViewState[ViewStateKey] = "0";
            this.hdnGLAccountID.Value = "0";
            //lblMode.Text = "Mode: Add";
            lblHeader.Text = "Location wise Ledger : New";
            txtGLGroup.Focus();

            SetControl(FormDataMode.Add);
            Helper.SetStatusMessage(lblMessage, string.Empty, MessageTypeEnum.None);
        }

        private void EditTask()
        {
            this.EditMode = FormDataMode.Edit;
            //lblMode.Text = "Mode: Edit";
            lblHeader.Text = "Location wise Ledger : Edit";
            //this.SetMasterHeader("Edit User");
            ReadData(this.LocationGLAccountID);
            ViewState[ViewStateKey] = this.LocationGLAccountID.ToString();
            //lnkAddNew.Visible = true;
            txtGLAccount.Focus();
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
            if (this.LocationGLAccountID > 0)
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
                    this.LocationGLAccountID = prevID;
                    ReadTask();
                }
                else
                {
                    this.LocationGLAccountID = 0;
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

                string redirectURL = "~/Accounting/GeneralLedger/LocationwiseLedgerBinding.aspx?id=" + this.LocationGLAccountID.ToString();
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


        private bool ReadData(int pLocationGLAccountID)
        {
            
             dcLocationGLAccount cObj = LocationGLAccountBL.GetLocationGLAccountByID( pLocationGLAccountID);

            bool bStatus;
            if (cObj != null)
            {
                this.LocationGLAccountID = cObj.LocationGLAccountID;
                this.hdnGLAccountID.Value = cObj.LocationGLAccountID.ToString();

                txtGLGroup.Text = cObj.GLGroupCode;
                txtGLGroupName.Text = cObj.GLGroupName;

                txtGLAccount.Text = cObj.GLAccountCode;
                txtGLAccountName.Text = cObj.GLAccountName;
                ddlLocation.SelectedValue = cObj.LocationID.ToString();

                bStatus = true;
            }
            else
            {
                this.hdnGLAccountID.Value = "0";
                this.LocationGLAccountID = 0;
                bStatus = false;
                //MMS.Globals.ShowMessagePage(MMS.MessageTypeEnum.Error, "User ID not Found!", "");
            }
            return bStatus;

        }


        private bool CheckData()
        {

            if (txtGLAccount.Text.Trim() == string.Empty)
            {
                //Helper.SetStatusMessage(lblMessage, "Please Enter Employee Name", MessageTypeEnum.InvalidData);
                this.SetPageMessage("Please Enter Code", MessageTypeEnum.InvalidData);
                this.ShowPageMessage(lblMessage, true);
                txtGLAccount.Focus();
                return false;
            }


            if (txtGLAccountName.Text.Trim() == string.Empty)
            {
                //Helper.SetStatusMessage(lblMessage, "Please Enter Employee Name", MessageTypeEnum.InvalidData);
                this.SetPageMessage("Please Enter Name", MessageTypeEnum.InvalidData);
                this.ShowPageMessage(lblMessage, true);
                txtGLAccount.Focus();
                return false;
            }


            if (Convert.ToInt32(ddlLocation.SelectedValue) == 0)
            {
                this.SetPageMessage("Please Select Category", MessageTypeEnum.InvalidData);
                this.ShowPageMessage(lblMessage, true);
                ddlLocation.Focus();
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
                if (LocationGLAccountBL.IsAccLedgerCodeExists(Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(hdnGLAccountID.Value)))
                {
                    //Helper.SetStatusMessage(lblMessage, "Employee Already Exists", MessageTypeEnum.InvalidData);
                    this.SetPageMessage("Ledger Code already exists!", MessageTypeEnum.InvalidData);
                    this.ShowPageMessage(lblMessage, true);
                    return false;

                }
            }
            else if (EditMode == FormDataMode.Edit)
            {
                if (LocationGLAccountBL.IsAccLedgerCodeExists(Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(hdnGLAccountID.Value), this.LocationGLAccountID))
                {
                    //Helper.SetStatusMessage(lblMessage, "Employee Already Exists", MessageTypeEnum.InvalidData);
                    this.SetPageMessage("Ledger Code already exists!", MessageTypeEnum.InvalidData);
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
            int newLocationGLAccountID = 0;

            dcLocationGLAccount cObj = new dcLocationGLAccount();

            cObj.GLAccountID = Convert.ToInt32(hdnGLAccountID.Value);
            cObj.LocationID = Convert.ToInt32(ddlLocation.SelectedValue);
            

            if (EditMode == FormDataMode.Add)
            {
                //try
                {
                    newLocationGLAccountID = LocationGLAccountBL.Insert(cObj, this.DbContext);
                    if (newLocationGLAccountID > 0)
                    {
                        bStatus = true;
                        saveMsg = "New Ledger Code Binding saved successfully.";
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
                    cObj.LocationGLAccountID = this.LocationGLAccountID;
                    //try
                    {
                        //bStatus = LocationGLAccountBL.Update(cObj, this.DbContext);
                        //newLocationGLAccountID = cObj.LocationGLAccountID;
                        //saveMsg = "Edited Ledger Code Binding saved successfully.";
                    }
                    //catch (Exception e)
                    {
                        //    saveMsg = e.Message;
                    }
                }
            }

            if (bStatus)
            {
                LocationGLAccountID = newLocationGLAccountID;
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
