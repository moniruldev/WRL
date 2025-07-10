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


using PG.DBClass.OrganiztionDC;
using PG.BLLibrary.OrganizationBL;




namespace PG.Web.Organization
{


    public partial class Location : BagePage
    {
        int CompanyID = 0;

        int LocationID = 0;
        string ViewStateKey = "LocationID";
        string ViewStateKeyPrev = "LocationID_Prev";

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

            this.LocationID = base.GetPageQueryInteger("id");

            if (!IsPostBack) //first Time
            {
                FillCombo();
                if (this.LocationID == 0) //not query string
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
                this.LocationID = int.Parse(ViewState[ViewStateKey].ToString());
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



            ddlLocationType.Items.Clear();
            ddlLocationType.Items.Add(new ListItem("(select)", "0"));
            ddlLocationType.AppendDataBoundItems = true;

            ddlLocationType.DataTextField = "LocationTypeName";
            ddlLocationType.DataValueField = "LocationTypeID";
            ddlLocationType.AppendDataBoundItems = true;
            ddlLocationType.DataSource = LocationTypeBL.GetLocationTypeList(this.CompanyID);
            ddlLocationType.DataBind();



 

        }


        private void SetControl(FormDataMode dataMode)
        {
            bool isEnabled = false;

            if (dataMode == FormDataMode.Add | dataMode == FormDataMode.Edit)
            {
                isEnabled = true;
            }


            txtLocationName.Enabled = isEnabled;
            txtLocationCode.Enabled = isEnabled;
            txtLocationAddress.Enabled = isEnabled;
            ddlLocationType.Enabled = isEnabled;


            //buttons
            btnAddNew.Visible = !isEnabled;
            btnEdit.Visible = !isEnabled;
            btnDelete.Visible = !isEnabled;

            btnSave.Visible = isEnabled;
            btnCancel.Visible = isEnabled;

        }



        private void ReadTask()
        {
            lblHeader.Text = "Location : View";
            this.EditMode = FormDataMode.Read;
            //this.hdnPaymentReqID.Value = this.PaymentReqID.ToString();
            ReadData(this.LocationID);

            ViewState[ViewStateKey] = this.LocationID.ToString();

            SetControl(FormDataMode.Read);


            //var c = new { Name = "Name", ID = 344 };
        }


        private void AddTask()
        {
            ViewState[ViewStateKeyPrev] = this.LocationID.ToString();

            txtLocationName.Text = "";
            //ddlLocationType.SelectedValue = "0";

            this.EditMode = FormDataMode.Add;
            this.IsDirty = false;
            ViewState[ViewStateKey] = "0";
            this.hdnLocationID.Value = "0";
            //lblMode.Text = "Mode: Add";
            lblHeader.Text = "Location : New";
            //SetNextYear();
            txtLocationCode.Focus();

            SetControl(FormDataMode.Add);
            Helper.SetStatusMessage(lblMessage, string.Empty, MessageTypeEnum.None);
        }

        private void EditTask()
        {
            this.EditMode = FormDataMode.Edit;
            //lblMode.Text = "Mode: Edit";
            lblHeader.Text = "Location: Edit";
            //this.SetMasterHeader("Edit User");
            ReadData(this.LocationID);
            ViewState[ViewStateKey] = this.LocationID.ToString();
            //lnkAddNew.Visible = true;
            txtLocationCode.Focus();
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
            if (this.LocationID > 0)
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
                    this.LocationID = prevID;
                    ReadTask();
                }
                else
                {
                    this.LocationID = 0;
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

                string redirectURL = "~/Organization/Location.aspx?id=" + this.LocationID.ToString();
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


        private bool ReadData(int pLocationID)
        {
            dcLocation cObj = LocationBL.GetLocaionByID(pLocationID);

            bool bStatus;
            if (cObj != null)
            {
                this.LocationID = cObj.LocationID;
                this.hdnLocationID.Value = cObj.LocationID.ToString();

                txtLocationName.Text = cObj.LocationName;
                txtLocationCode.Text = cObj.LocationCode;
                txtLocationAddress.Text = cObj.LocationAddress;
              
                ddlLocationType.SelectedValue = cObj.LocationTypeID.ToString();
                //chkIsOpenning.Checked = cObj.IsOpenningPeriod;
               //// lnkSetPass.NavigateUrl = @"~/Admin/SetPassword.aspx?uk=" + cObj.UserID.ToString();
               // lnkSetPass.NavigateUrl = @"javascript:tbopen(" + cObj.UserID.ToString() + ")";
                bStatus = true;
            }
            else
            {
                this.hdnLocationID.Value = "0";
                this.LocationID = 0;
                bStatus = false;
                //MMS.Globals.ShowMessagePage(MMS.MessageTypeEnum.Error, "User ID not Found!", "");
            }
            return bStatus;

        }


        private bool CheckData()
        {

            if (txtLocationName.Text.Trim() == string.Empty)
            {
                //Helper.SetStatusMessage(lblMessage, "Please Enter Employee Name", MessageTypeEnum.InvalidData);
                this.SetPageMessage("Please Enter Location Name", MessageTypeEnum.InvalidData);
                this.ShowPageMessage(lblMessage, true);
                txtLocationName.Focus();
                return false;
            }

            if (txtLocationCode.Text.Trim() == string.Empty)
            {
                //Helper.SetStatusMessage(lblMessage, "Please Enter Employee Name", MessageTypeEnum.InvalidData);
                this.SetPageMessage("Please Enter Location Code", MessageTypeEnum.InvalidData);
                this.ShowPageMessage(lblMessage, true);
                txtLocationCode.Focus();
                return false;
            }


            if (ddlLocationType.SelectedValue == "0")
            {
                //Helper.SetStatusMessage(lblMessage, "Please Enter Employee Name", MessageTypeEnum.InvalidData);
                this.SetPageMessage("Please Select Location Type", MessageTypeEnum.InvalidData);
                this.ShowPageMessage(lblMessage, true);
                ddlLocationType.Focus();
                return false;
            }



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

   
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveTask();
        } 


        private bool SaveData()
        {
            bool bStatus = false;
            int newLocationID = 0;

            dcLocation cObj = new dcLocation();

            cObj.CompanyID = this.CompanyID;
            cObj.LocationName = txtLocationName.Text.Trim();
            cObj.LocationCode = txtLocationCode.Text.Trim();
            cObj.LocationAddress = txtLocationAddress.Text.Trim();
            cObj.LocationTypeID = Convert.ToInt32(ddlLocationType.SelectedValue);
            
            if (EditMode == FormDataMode.Add)
            {
                //try
                {
                    newLocationID = LocationBL.Insert(cObj, this.DbContext);
                    if (newLocationID > 0)
                    {
                        bStatus = true;
                        saveMsg = "New Location Saved successfully.";
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
                    cObj.LocationID = this.LocationID;
                    //try
                    {
                        bStatus =  LocationBL.Update(cObj,this.DbContext);
                        newLocationID = cObj.LocationID;
                        saveMsg = "Edited Location saved successfully.";
                    }
                    //catch (Exception e)
                    {
                        //    saveMsg = e.Message;
                    }
                }
            }

            if (bStatus)
            {
                LocationID = newLocationID;
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
