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
    public partial class HubTypeEntry : BagePage
    {
        //this 
        string ViewStateKey = "HUB_TYPE_ID";
        string ViewStateKeyPrev = "HUB_TYPE_ID_PREV";
        ReportOpenTypeEnum ReportOpenType = ReportOpenTypeEnum.Preview;
        // int CompanyID = 0;

        int HUB_TYPE_ID = 0;
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

            this.HUB_TYPE_ID = base.GetPageQueryInteger("id");

            if (!IsPostBack) //first Time
            {

              
                hdnLoggedInUser.Value = loggedinUser.UserID.ToString();
                //FillCombo();


                if (this.HUB_TYPE_ID == 0) //not query string
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
                this.HUB_TYPE_ID = int.Parse(ViewState[ViewStateKey].ToString());
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
            //ddlRoomType.Items.Clear();
            //ddlRoomType.AppendDataBoundItems = true;
            //ddlRoomType.DataTextField = "TITLE";
            //ddlRoomType.DataValueField = "ROOM_TYPE_ID";
            //ddlRoomType.DataSource = HMROOM_TYPEBL.GetRoomTypeList();
            //ddlRoomType.DataBind();
            //ddlRoomType.SelectedIndex = 0;

            //ddlRoomStatus.Items.Clear();
            //ddlRoomStatus.AppendDataBoundItems = true;
            //ddlRoomStatus.DataTextField = "ROOM_STATUS";
            //ddlRoomStatus.DataValueField = "ROOM_STATUS_ID";
            //ddlRoomStatus.DataSource = HMROOMBL.GetRoomStatusList();
            //ddlRoomStatus.DataBind();
            //ddlRoomStatus.SelectedIndex = 0;


        }

        protected override void Render(HtmlTextWriter writer)
        {

            //Page.ClientScript.RegisterForEventValidation(btnPopupTrigger.UniqueID);
            //Page.ClientScript.RegisterForEventValidation(btnPopupTrigger.UniqueID, "");
            //Page.ClientScript.RegisterForEventValidation(btnPopupTrigger.UniqueID, "fillcombo");
            //Page.ClientScript.RegisterForEventValidation(btnPopupTrigger.UniqueID, "getbalance");

            base.Render(writer);
        }

        private void SetDate()
        {


        }

        private void ReadTask()
        {
            this.EditMode = FormDataMode.Read;
            ReadData(this.HUB_TYPE_ID);
            ViewState[ViewStateKey] = this.HUB_TYPE_ID.ToString();

            SetControl(FormDataMode.Read);

        }
        private void AddTask()
        {
            ViewState[ViewStateKeyPrev] = this.HUB_TYPE_ID.ToString();

            this.EditMode = FormDataMode.Add;
            this.IsDirty = false;
            this.HUB_TYPE_ID = 0;
            ViewState[ViewStateKey] = "0";
            SetControl(FormDataMode.Add);
        }
        private void EditTask()
        {
            this.EditMode = FormDataMode.Edit;
            ReadData(this.HUB_TYPE_ID);
            this.EditMode = FormDataMode.Edit;
            ViewState[ViewStateKey] = this.HUB_TYPE_ID.ToString();
            SetControl(FormDataMode.Edit);
        }

        private bool ReadData(int id)
        {
            bool bStatus = false;
            byte[] bytes = null;
            dcHUB_TYPE_MST cObj = HUB_TYPE_MSTBL.GetHUB_TYPEInfoById(id);
            if (cObj != null)
            {

                txtTypeName.Text = cObj.HUB_TYPE_NAME;
                txtDescription.Text = cObj.DESCRIPTION;
                ddlStatus.SelectedValue = cObj.IS_ACTIVE.ToString();
                
            

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


            txtTypeName.Enabled = isEnabled;
            txtDescription.Enabled = isEnabled;
            ddlStatus.Enabled = isEnabled;
            
            //buttons
            btnAddNew.Visible = !isEnabled;
            btnEdit.Visible = !isEnabled;
            btnSave.Visible = isEnabled;
            //btnUpdate.Visible = !isEnabled;


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
                    ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Data Saved Successfully');", true);
                   
                }
                else
                {
                    
                    ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Error !! Data not Saved');", true);
                }

            }
            else
            {

                ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Error !! Data not Saved');", true);
                this.SetPageMessage(errMsg, MessageTypeEnum.InvalidData);
            }

            return true;

        }


        private bool CheckData()
        {
            bool status = true;
            errMsg = string.Empty;

            if (txtTypeName.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Please Enter Type Name !!');", true);
                txtTypeName.Focus();
                return false;

            }

            


            return status;
        

        }
        private void SetHyperLink()
        {

            //new button
            string hLink = "javascript:tbopen(" + this.HUB_TYPE_ID.ToString() + ")";
            if (base.PageMode == PG.Core.Web.PageModeEnum.InTab)
            {
                hLink = "javascript:tbopenSalInfo(" + this.HUB_TYPE_ID.ToString() + ")";
                this.btnAddNew.Attributes.Add("onclick", hLink);
            }
            else
            {
                hLink = "~/HMS/HubTypeEntry.aspx?id=" + this.HUB_TYPE_ID.ToString();
                this.btnAddNew.Attributes.Add("onclick", hLink);
            }

        }

        //newly added comment
        private bool SaveData()
        {

            bool bStatus = false;

            bool isAdd = false;
            int newHUB_TYPE_ID = 0;
            dcHUB_TYPE_MST cObj = new dcHUB_TYPE_MST();
            if (this.HUB_TYPE_ID > 0)
            {
                cObj.HUB_TYPE_ID = this.HUB_TYPE_ID;
                cObj._RecordState = RecordStateEnum.Edited;
            }
            else
            {
                cObj._RecordState = RecordStateEnum.Added;
                isAdd = true;
            }


            cObj.HUB_TYPE_NAME = txtTypeName.Text.Trim();
            cObj.DESCRIPTION = txtDescription.Text.Trim();
            cObj.IS_ACTIVE = ddlStatus.SelectedValue;
            

            if (isAdd)
            {
                cObj.CREATE_BY = loggedinUser.UserName;
                cObj.CREATE_DATE = DateTime.Now;

            }
            else
            {
                cObj.EDIT_BY = loggedinUser.UserName;
                cObj.EDIT_DATE = DateTime.Now;

            }

            newHUB_TYPE_ID = HUB_TYPE_MSTBL.Save(cObj);
            if (newHUB_TYPE_ID > 0)
            {


                this.HUB_TYPE_ID = newHUB_TYPE_ID;
                ReadTask();
                bStatus = true;
                ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Data saved successfully !!');", true);
            }

            return bStatus;
        }



        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            AddTask();
        }

       


    }
}