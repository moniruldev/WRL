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
    public partial class LocationwiseRefBinding : BagePage
    {
        int CompanyID = 0;
        int LocationAccRefID = 0;
        string ViewStateKey = "LocationAccRefID";
        string ViewStateKeyPrev = "LocationAccRefID_Prev";

  

        string saveMsg = string.Empty;
        public string AccRefServiceLink = PageLinks.AccountingLinks.GetLink_LocationAccRef;



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
            this.LocationAccRefID = base.GetPageQueryInteger("id");

            if (!IsPostBack) //first Time
            {
                //FillCombo();
                FillCombo_Location();
                FillComboAccRefCategory();



                if (this.LocationAccRefID == 0) //not query string
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
                this.LocationAccRefID = int.Parse(ViewState[ViewStateKey].ToString());
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
            //if (ddlLocation.Items.Count < 3)
            // {
            //ddlLocation.SelectedValue = locID.ToString();

            //int locID = AppSecurity.GetValidLocationUserList().Select(c => c.LocationID).FirstOrDefault();

            //if (ddlLocation.Items.Count < 3)
            //{
             //   ddlLocation.SelectedValue = locID.ToString();

           // }

        }

        private void FillComboAccRefCategory()
        {
            ddlAccRefCategory.Items.Clear();
            ddlAccRefCategory.Items.Add(new ListItem("All", "0"));


            ddlAccRefCategory.DataTextField = "AccRefCategoryName";
            ddlAccRefCategory.DataValueField = "AccRefCategoryID";
            ddlAccRefCategory.AppendDataBoundItems = true;
            ddlAccRefCategory.DataSource = AccRefCategoryBL.GetAccRefCategoryList(this.CompanyID);
            ddlAccRefCategory.DataBind();


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

            ddlAccRefCategory.Enabled = isEnabled;
            txtAccRefCode.Enabled = isEnabled;

            txtAccRefName.Enabled = isEnabled;
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
            lblHeader.Text = "Location wise Reference : View";
            this.EditMode = FormDataMode.Read;
            //this.hdnPaymentReqID.Value = this.PaymentReqID.ToString();
            ReadData(this.LocationAccRefID);

            ViewState[ViewStateKey] = this.LocationAccRefID.ToString();

            SetControl(FormDataMode.Read);


            //var c = new { Name = "Name", ID = 344 };
        }


        private void AddTask()
        {
            ViewState[ViewStateKeyPrev] = this.LocationAccRefID.ToString();

            txtAccRefCode.Text = "";
            txtAccRefName.Text = "";
            



            this.EditMode = FormDataMode.Add;
            this.IsDirty = false;
            ViewState[ViewStateKey] = "0";
            this.hdnAccRefID.Value = "0";
            //lblMode.Text = "Mode: Add";
            lblHeader.Text = "Location wise Reference : New";
            txtAccRefCode.Focus();

            SetControl(FormDataMode.Add);
            Helper.SetStatusMessage(lblMessage, string.Empty, MessageTypeEnum.None);
        }

        private void EditTask()
        {
            this.EditMode = FormDataMode.Edit;
            //lblMode.Text = "Mode: Edit";
            lblHeader.Text = "Location wise Reference : Edit";
            //this.SetMasterHeader("Edit User");
            ReadData(this.LocationAccRefID);
            ViewState[ViewStateKey] = this.LocationAccRefID.ToString();
            //lnkAddNew.Visible = true;
            txtAccRefCode.Focus();
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
            if (this.LocationAccRefID > 0)
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
                    this.LocationAccRefID = prevID;
                    ReadTask();
                }
                else
                {
                    this.LocationAccRefID = 0;
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

                string redirectURL = "~/Accounting/GeneralLedger/LocationwiseRefBinding.aspx?id=" + this.LocationAccRefID.ToString();
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


        private bool ReadData(int pLocationAccRefID)
        {
            //dcLocationAccRef cObj = new dcLocationAccRef();
             dcLocationAccRef cObj = LocationAccRefBL.GetLocationAccRefByID( pLocationAccRefID);

            bool bStatus;
            if (cObj != null)
            {
                //this.LocationGLAccountID = cObj.LocationGLAccountID;
                this.hdnAccRefID.Value = cObj.AccRefID.ToString();

                ddlAccRefCategory.SelectedValue = cObj.AccRefCategoryID.ToString();
                

                txtAccRefCode.Text = cObj.AccRefCode;
                txtAccRefName.Text = cObj.AccRefName;
                ddlLocation.SelectedValue = cObj.LocationID.ToString();

                bStatus = true;
            }
            else
            {
                this.hdnAccRefID.Value = "0";
                this.LocationAccRefID = 0;
                bStatus = false;
                //MMS.Globals.ShowMessagePage(MMS.MessageTypeEnum.Error, "User ID not Found!", "");
            }
            return bStatus;

        }


        private bool CheckData()
        {

            if (txtAccRefCode.Text.Trim() == string.Empty)
            {
                //Helper.SetStatusMessage(lblMessage, "Please Enter Employee Name", MessageTypeEnum.InvalidData);
                this.SetPageMessage("Please Enter Code", MessageTypeEnum.InvalidData);
                this.ShowPageMessage(lblMessage, true);
                txtAccRefCode.Focus();
                return false;
            }


            if (txtAccRefName.Text.Trim() == string.Empty)
            {
                //Helper.SetStatusMessage(lblMessage, "Please Enter Employee Name", MessageTypeEnum.InvalidData);
                this.SetPageMessage("Please Enter Name", MessageTypeEnum.InvalidData);
                this.ShowPageMessage(lblMessage, true);
                txtAccRefCode.Focus();
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
                if (LocationGLAccountBL.IsAccRefCodeExists(Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(hdnAccRefID.Value)))
                {
                    //Helper.SetStatusMessage(lblMessage, "Employee Already Exists", MessageTypeEnum.InvalidData);
                    this.SetPageMessage("Ref Code already exists!", MessageTypeEnum.InvalidData);
                    this.ShowPageMessage(lblMessage, true);
                    return false;

                }
            }
            else if (EditMode == FormDataMode.Edit)
            {
                if (LocationGLAccountBL.IsAccRefCodeExists(Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(hdnAccRefID.Value), this.LocationAccRefID))
                {
                    //Helper.SetStatusMessage(lblMessage, "Employee Already Exists", MessageTypeEnum.InvalidData);
                    this.SetPageMessage("Ref Code already exists!", MessageTypeEnum.InvalidData);
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
            int newLocationAccRefID = 0;

            dcLocationAccRef cObj = new dcLocationAccRef();

            cObj.AccRefID = Convert.ToInt32(hdnAccRefID.Value);
            cObj.LocationID = Convert.ToInt32(ddlLocation.SelectedValue);
            

            if (EditMode == FormDataMode.Add)
            {
                //try
                {
                    newLocationAccRefID = LocationAccRefBL.Insert(cObj, this.DbContext);
                    if (newLocationAccRefID > 0)
                    {
                        bStatus = true;
                        saveMsg = "New Ref Code Binding saved successfully.";
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
                    cObj.LocationAccRefID = this.LocationAccRefID;
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
                LocationAccRefID = newLocationAccRefID;
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
