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
    public partial class ItemTypeEntry : BagePage
    {
        //this 
        string ViewStateKey = "ITEM_TYPE_ID";
        string ViewStateKeyPrev = "ITEM_TYPE_ID_PREV";
        ReportOpenTypeEnum ReportOpenType = ReportOpenTypeEnum.Preview;
        // int CompanyID = 0;

        int ITEM_TYPE_ID = 0;
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

            this.ITEM_TYPE_ID = base.GetPageQueryInteger("id");

            if (!IsPostBack) //first Time
            {

              
                hdnLoggedInUser.Value = loggedinUser.UserID.ToString();
                FillCombo();


                if (this.ITEM_TYPE_ID == 0) //not query string
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
                this.ITEM_TYPE_ID = int.Parse(ViewState[ViewStateKey].ToString());
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
            //dcITEM_TYPE_MST ItemType = new dcITEM_TYPE_MST();
            //ItemType.IS_ACTIVE = "Y";
            //ddlItemType.Items.Clear();
            //ddlItemType.AppendDataBoundItems = true;
            //ddlItemType.Items.Add(new ListItem("Select", "0"));
            //ddlItemType.DataTextField = "ITEM_TYPE_NAME";
            //ddlItemType.DataValueField = "ITEM_TYPE_ID";
            //ddlItemType.DataSource = ITEM_TYPE_MSTBL.GetItemTypeList(ItemType, null);
            //ddlItemType.DataBind();
            //ddlItemType.SelectedIndex = 0;



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
            ReadData(this.ITEM_TYPE_ID);
            ViewState[ViewStateKey] = this.ITEM_TYPE_ID.ToString();

            SetControl(FormDataMode.Read);

        }
        private void AddTask()
        {
            ViewState[ViewStateKeyPrev] = this.ITEM_TYPE_ID.ToString();

            this.EditMode = FormDataMode.Add;
            this.IsDirty = false;
            this.ITEM_TYPE_ID = 0;
            ResetFormFields();
            ViewState[ViewStateKey] = "0";
            SetControl(FormDataMode.Add);
        }
        private void EditTask()
        {
            this.EditMode = FormDataMode.Edit;
            ReadData(this.ITEM_TYPE_ID);
            this.EditMode = FormDataMode.Edit;
            ViewState[ViewStateKey] = this.ITEM_TYPE_ID.ToString();
            SetControl(FormDataMode.Edit);
        }

        private void ResetFormFields()
        {
            txtItemTypeName.Text = string.Empty;
            txtDescription.Text = string.Empty;
           
        }


        private bool ReadData(int id)
        {
            bool bStatus = false;
            dcITEM_TYPE_MST cObj = ITEM_TYPE_MSTBL.GetItemTypeById(id, null);
            if (cObj != null)
            {
                
                txtItemTypeName.Text = cObj.ITEM_TYPE_NAME;
                txtDescription.Text = cObj.DESCRIPTION;
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


            ddlStatus.Enabled = isEnabled;
            ddlStatus.CssClass = "form-control form-control-sm";
            SetTextBoxState(txtItemTypeName, isEnabled);
            SetTextBoxState(txtDescription, isEnabled);
            
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

            if (txtItemTypeName.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('error', 'Enter item type name!', 'Error');", true);
                txtItemTypeName.Focus();
                return false;

            }


            if (EditMode == FormDataMode.Add)
            {

                if (ITEM_TYPE_MSTBL.IsItemTypeNameExists(txtItemTypeName.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('error', 'Item type name already exists!', 'Error');", true);
                    txtItemTypeName.Focus();
                    return false;
                }

         

            }
            else if (EditMode == FormDataMode.Edit)
            {

                if (ITEM_TYPE_MSTBL.IsItemTypeNameExists(txtItemTypeName.Text.Trim(), this.ITEM_TYPE_ID))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('error', 'Item type name already exists!', 'Error');", true);
                    txtItemTypeName.Focus();
                    return false;
                }

              

            }

            


            return status;
        

        }
        private void SetHyperLink()
        {

            //new button
            string hLink = "javascript:tbopen(" + this.ITEM_TYPE_ID.ToString() + ")";
            if (base.PageMode == PG.Core.Web.PageModeEnum.InTab)
            {
                hLink = "javascript:tbopenSalInfo(" + this.ITEM_TYPE_ID.ToString() + ")";
                this.btnAddNew.Attributes.Add("onclick", hLink);
            }
            else
            {
                hLink = "~/WREL/ItemTypeEntry.aspx?id=" + this.ITEM_TYPE_ID.ToString();
                this.btnAddNew.Attributes.Add("onclick", hLink);
            }

        }

        //newly added comment
        private bool SaveData()
        {

            bool bStatus = false;

            bool isAdd = false;
            int newITEM_TYPE_ID = 0;
            dcITEM_TYPE_MST cObj = new dcITEM_TYPE_MST();
            if (this.ITEM_TYPE_ID > 0)
            {
                cObj.ITEM_TYPE_ID = this.ITEM_TYPE_ID;
                cObj._RecordState = RecordStateEnum.Edited;
            }
            else
            {
                cObj._RecordState = RecordStateEnum.Added;
                isAdd = true;
            }


            cObj.ITEM_TYPE_NAME = txtItemTypeName.Text.Trim();
            cObj.DESCRIPTION = txtDescription.Text.Trim();
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

            newITEM_TYPE_ID = ITEM_TYPE_MSTBL.Save(cObj);
            if (newITEM_TYPE_ID > 0)
            {


                this.ITEM_TYPE_ID = newITEM_TYPE_ID;
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