using PG.BLLibrary.InventoryBL;
using PG.Core;
using PG.Core.DBBase;
using PG.Core.Utility;
using PG.Core.Web;
using PG.DBClass.InventoryDC;
using PG.DBClass.SecurityDC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PG.BLLibrary.OrganizationBL;
using PG.Report.ReportRBL.InventoryRBL;
using PG.Report.ReportEnums;
using PG.Report;
using PG.Report.ReportGen.InventoryRGN;
using PG.DBClass.HMSDC;
using PG.BLLibrary.HMSBL;
using System.IO;
using System.Text;
using System.Data;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;
using PG.BLLibrary.WRElBL;
using PG.DBClass.WRELDC;

namespace PG.Web.WREL
{
    public partial class ItemEntry : BagePage
    {
        //this 
        string ViewStateKey = "ITEM_ID";
        string ViewStateKeyPrev = "ITEM_ID_PREV";
        ReportOpenTypeEnum ReportOpenType = ReportOpenTypeEnum.Preview;
        // int CompanyID = 0;

        int ITEM_ID = 0;
        string saveMsg = string.Empty;
        string errMsg = string.Empty;

        private  dcUser loggedinUser = null;
        public string ReportViewPageLink = PageLinks.ReportLinks.GetLink_ReportView;
        public string ReportViewPDFPageLink = PageLinks.ReportLinks.GetLink_ReportViewPDF;
        public string ReportPrintPageLink = PageLinks.ReportLinks.GetLink_ReportPrint;
        public string ReportPDFPageLink = PageLinks.ReportLinks.GetLink_ReportPDF;



        public string CountryListServiceLink = PageLinks.InventoryLink.GetLink_CountryList;

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

            loggedinUser = AppSecurity.GetUserInfoFromSession();

            this.ITEM_ID = base.GetPageQueryInteger("id");

            if (!IsPostBack) //first Time
            {

              
                hdnLoggedInUser.Value = loggedinUser.UserID.ToString();
                FillCombo();


                if (this.ITEM_ID == 0) //not query string
                {
                    SetDate();
                    AddTask();
                    this.EditMode = FormDataMode.Add;
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
                this.ITEM_ID = int.Parse(ViewState[ViewStateKey].ToString());
            }

            SetHyperLink();

          
            //this.ShowPageMessage(this.lblMessage);
            // Response.Write("UserID : " + this.UserID.ToString());

        }
     
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            EditTask();
        }

        public void FillCombo()
        {
            dcITEM_TYPE_MST ItemType = new dcITEM_TYPE_MST();
            ItemType.IS_ACTIVE = "Y";
            ddlItemType.Items.Clear();
            ddlItemType.AppendDataBoundItems = true;
            ddlItemType.Items.Add(new ListItem("Select", "0"));
            ddlItemType.DataTextField = "ITEM_TYPE_NAME";
            ddlItemType.DataValueField = "ITEM_TYPE_ID";
            ddlItemType.DataSource = ITEM_TYPE_MSTBL.GetItemTypeList(ItemType, null);
            ddlItemType.DataBind();
            ddlItemType.SelectedIndex = 0;


            ddlUnitId.Items.Clear();
            ddlUnitId.AppendDataBoundItems = true;
            ddlUnitId.Items.Add(new ListItem("Select", "0"));
            ddlUnitId.DataTextField = "UOM_NAME";
            ddlUnitId.DataValueField = "UOM_ID";
            ddlUnitId.DataSource = UOM_INFOBL.GetUOMList(null);
            ddlUnitId.DataBind();
            ddlUnitId.SelectedIndex = 0;


        }

        protected override void Render(HtmlTextWriter writer)
        {

            //Page.ItemScript.RegisterForEventValidation(btnPopupTrigger.UniqueID);
            //Page.ItemScript.RegisterForEventValidation(btnPopupTrigger.UniqueID, "");
            //Page.ItemScript.RegisterForEventValidation(btnPopupTrigger.UniqueID, "fillcombo");
            //Page.ItemScript.RegisterForEventValidation(btnPopupTrigger.UniqueID, "getbalance");

            base.Render(writer);
        }

        private void SetDate()
        {


        }

        private void ReadTask()
        {
            this.EditMode = FormDataMode.Read;
            ReadData(this.ITEM_ID);
            ViewState[ViewStateKey] = this.ITEM_ID.ToString();

            SetControl(FormDataMode.Read);

        }
        private void AddTask()
        {
            ViewState[ViewStateKeyPrev] = this.ITEM_ID.ToString();

            this.EditMode = FormDataMode.Add;
            this.IsDirty = false;
            this.ITEM_ID = 0;
            ResetFormFields();
            ViewState[ViewStateKey] = "0";
            SetControl(FormDataMode.Add);
        }
        private void EditTask()
        {
            this.EditMode = FormDataMode.Edit;
            ReadData(this.ITEM_ID);
            this.EditMode = FormDataMode.Edit;
            ViewState[ViewStateKey] = this.ITEM_ID.ToString();
            SetControl(FormDataMode.Edit);
        }

        private void ResetFormFields()
        {
            txtItemName.Text = string.Empty;
          
            txtRemarks.Text = string.Empty;
        }


        private bool ReadData(int id)
        {
            bool bStatus = false;
            dcITEM_MST cObj = ITEM_MSTBL.GetItemByItemId(id,null);
            if (cObj != null)
            {

                txtItemName.Text = cObj.ITEM_NAME;
                ddlItemType.SelectedValue = cObj.ITEM_TYPE_ID.ToString();
                ddlUnitId.SelectedValue = cObj.UOM_ID.ToString();
                
                txtRemarks.Text = cObj.REMARKS;
                ddlStatus.SelectedValue = cObj.IS_ACTIVE;

                
            

                bStatus = true;
            }
            return bStatus;

        }

        private void SetControl(FormDataMode dataMode)
        {
            bool isEnabled = false;

            if (dataMode == FormDataMode.Add | dataMode == FormDataMode.Edit)
            {
                isEnabled = true;
            }


            ddlItemType.Enabled = isEnabled;
            ddlStatus.Enabled = isEnabled;
            ddlUnitId.Enabled = isEnabled;
            ddlItemType.CssClass = "form-control form-control-sm";
            ddlStatus.CssClass = "form-control form-control-sm";
            ddlUnitId.CssClass = "form-control form-control-sm";
            SetTextBoxState(txtItemName, isEnabled);
           
            SetTextBoxState(txtRemarks, isEnabled);
            
            //buttons
            btnAddNew.Visible = !isEnabled;
            btnEdit.Visible = !isEnabled;
            btnSave.Visible = isEnabled;
            //btnUpdate.Visible = !isEnabled;


        }

        private void SetTextBoxState(TextBox txtBox, bool isEnabled)
        {
            if (isEnabled)
            {
                txtBox.Attributes.Remove("disabled");
                txtBox.CssClass = "form-control form-control-sm";
            }
            else
            {
                txtBox.Attributes["disabled"] = "disabled";
                txtBox.CssClass = "form-control form-control-sm";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            SaveTask();

        }

        private bool SaveTask()
        {

            if (!Page.IsValid)
            { return false; }


            if (CheckData())
            {

                bool bStatus = SaveData();

                if (bStatus)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('success', 'Data Saved Successfully!', 'Success');", true);
                   
                }
                else
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('error', ' Data not Saved!', 'Error');", true);
                }

            }
            else
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('error', ' Data not Saved!', 'Error');", true);
            }

            return true;

        }


        private bool CheckData()
        {
            bool status = true;
            errMsg = string.Empty;

            if (txtItemName.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('error', 'Enter item name!', 'Error');", true);
                txtItemName.Focus();
                return false;

            }

            if (ddlItemType.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('error', 'Please select item type!', 'Error');", true);
                ddlItemType.Focus();
                return false;

            }

            if (ddlUnitId.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('error', 'Please select unit!', 'Error');", true);
                ddlUnitId.Focus();
                return false;

            }

            if (EditMode == FormDataMode.Add)
            {

                if (ITEM_MSTBL.IsItemNameExists(txtItemName.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('error', 'Item name already exists!', 'Error');", true);
                    txtItemName.Focus();
                    return false;
                }

         

            }
            else if (EditMode == FormDataMode.Edit)
            {

                if (ITEM_MSTBL.IsItemNameExists(txtItemName.Text.Trim(), this.ITEM_ID))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('error', 'Item name already exists!', 'Error');", true);
                    txtItemName.Focus();
                    return false;
                }

              

            }

            


            return status;
        

        }
        private void SetHyperLink()
        {

            //new button
            string hLink = "javascript:tbopen(" + this.ITEM_ID.ToString() + ")";
            if (base.PageMode == PG.Core.Web.PageModeEnum.InTab)
            {
                hLink = "javascript:tbopenSalInfo(" + this.ITEM_ID.ToString() + ")";
                this.btnAddNew.Attributes.Add("onclick", hLink);
            }
            else
            {
                hLink = "~/WREL/ItemEntry.aspx?id=" + this.ITEM_ID.ToString();
                this.btnAddNew.Attributes.Add("onclick", hLink);
            }

        }

        //newly added comment
        private bool SaveData()
        {

            bool bStatus = false;

            bool isAdd = false;
            int newITEM_ID = 0;
            dcITEM_MST cObj = new dcITEM_MST();
            if (this.ITEM_ID > 0)
            {
                cObj.ITEM_ID = this.ITEM_ID;
                cObj._RecordState = RecordStateEnum.Edited;
            }
            else
            {
                cObj._RecordState = RecordStateEnum.Added;
                isAdd = true;
            }


            cObj.ITEM_NAME = txtItemName.Text.Trim();
            cObj.ITEM_TYPE_ID = Conversion.StringToInt(ddlItemType.SelectedValue);
            cObj.UOM_ID = Conversion.StringToInt(ddlUnitId.SelectedValue);
           
            cObj.REMARKS = txtRemarks.Text.Trim();
            cObj.IS_ACTIVE = ddlStatus.SelectedValue;
            

            if (isAdd)
            {
                cObj.CREATE_BY = loggedinUser.UserID.ToString();
                cObj.CREATE_DATE = DateTime.Now;

            }
            else
            {
                cObj.EDIT_BY = loggedinUser.UserID.ToString();
                cObj.EDIT_DATE = DateTime.Now;

            }

            newITEM_ID = ITEM_MSTBL.Save(cObj);
            if (newITEM_ID > 0)
            {


                this.ITEM_ID = newITEM_ID;
                ReadTask();
                bStatus = true;
            }

            return bStatus;
        }



        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            AddTask();
        }

       


    }
}