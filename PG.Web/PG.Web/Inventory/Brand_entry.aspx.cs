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

using PG.DBClass.InventoryDC;
using PG.BLLibrary.InventoryBL;
using PG.Core.Utility;
using PG.DBClass.SecurityDC;


namespace PG.Web.Inventory
{
    public partial class Brand_entry : BagePage
    {
        int CompanyID = 0;
        int BRAND_ID = 0;
        string ViewStateKey = "BRAND_ID";
        string ViewStateKeyPrev = "BRAND_ID_Prev";

        string saveMsg = string.Empty;

        //List<DBClass.SystemDC.dcSysOption> listSysOptions = null;
        public string ItemGroupListServiceLink = PageLinks.InventoryLink.GetLink_ItemGroupList;

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

            this.CompanyID = CompanyInfo.GetCompanyID();
            this.hdnCompanyID.Value = this.CompanyID.ToString();
            this.BRAND_ID = base.GetPageQueryInteger("id");

            if (!IsPostBack) //first Time
            {
                FillCombo();


                if (this.BRAND_ID == 0) //not query string
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
                  
                }

            }
            else
            {
                this.EditMode = base.GetEditModeFromViewState(base.EditModeViewStateKey);
                this.BRAND_ID = int.Parse(ViewState[ViewStateKey].ToString());
            }
            SetHyperLink();
            this.ShowPageMessage(this.lblMessage);
            // Response.Write("UserID : " + this.UserID.ToString());
        }

        private void SetHyperLink()
        {

            //new button
            //string hLink = "javascript:tbopenSalInfo("+ this.GLGroupID.ToString() +")";
            //if (base.PageMode == PG.Core.Web.PageModeEnum.InTab)
            //{
            //    hLink = "javascript:tbopenSalInfo(" + this.GLGroupID.ToString() + ")";
            //    //this.btnSalaryInfo = string.Empty;
            //    this.btnSalaryInfo.Attributes.Add("onclick", hLink);
            //}
            //else
            //{
            //    hLink = "~/Master/EmpSalaryInfo.aspx?eid=" + this.GLGroupID.ToString();
            //    //this.btnAddNew.PostBackUrl = hLink;
            //    //this.btnAddNew.OnClientClick = string.Empty;
            //    this.btnSalaryInfo.Attributes.Add("onclick", hLink);
            //}

        }

        private void FillCombo()
        {
            //this.ddlItemClass.AppendDataBoundItems = true;
            //this.ddlItemClass.DataSource = INV_ITEM_CLASSBL.Inv_Item_Class_List().OrderBy(c => c.ITEM_CLASS_NAME).ToList();
            //this.ddlItemClass.DataTextField = "ITEM_CLASS_NAME_CODE";
            //this.ddlItemClass.DataValueField = "ITEM_CLASS_ID";
            //this.ddlItemClass.DataBind();

            //this.ddlItemType.AppendDataBoundItems = true;
            //this.ddlItemType.DataSource = INV_ITEM_TYPEBL.Inv_Item_Type_List();
            //this.ddlItemType.DataTextField = "ITEM_TYPE_NAME_CODE";
            //this.ddlItemType.DataValueField = "ITEM_TYPE_ID";
            //this.ddlItemType.DataBind();


            //this.ddlUOM.AppendDataBoundItems = true;
            //this.ddlUOM.DataSource = UOM_INFOBL.GetUOMList().OrderBy(c => c.UOM_NAME).ToList();
            //this.ddlUOM.DataTextField = "UOM_NAME_CODE";
            //this.ddlUOM.DataValueField = "UOM_ID";
            //this.ddlUOM.DataBind();
        }

        //private void FillGLGroup()
        //{
        //    this.GLGroupTree1.SetGLGroupTreeText(this.CompanyID);   
        //}


        private void SetControl(FormDataMode dataMode)
        {
            bool isEnabled = false;
            //bool isSystem = hdnIsSystem.Value == "1" ? true : false;
            // int groupIDParent = Convert.ToInt32(hdnItemGroupIDParent.Value);
            //int groupIDParent = Convert.ToInt32(hdnGroupID.Value);

            if (dataMode == FormDataMode.Add | dataMode == FormDataMode.Edit)
            {
                isEnabled = true;
            }

            //txtBrandCode.Enabled = isEnabled;
            txtBrandCode.Attributes.Add("readonly", "readonly");
            txtBrandName.Enabled = isEnabled;
            ddlIsActive.Enabled = isEnabled;

            //txtItemGroupNameParent.Attributes.Add("readonly", "readonly");

            //buttons
            btnAddNew.Visible = !isEnabled;
            btnEdit.Visible = !isEnabled;
            btnDelete.Visible = !isEnabled;

            btnSave.Visible = isEnabled;
            btnCancel.Visible = isEnabled;

        }



        private void ReadTask()
        {
            lblHeader.Text = "Brand Entry : View";
            this.EditMode = FormDataMode.Read;
            //this.hdnPaymentReqID.Value = this.PaymentReqID.ToString();
            ReadData(this.BRAND_ID);

            ViewState[ViewStateKey] = this.BRAND_ID.ToString();

            SetControl(FormDataMode.Read);


            //var c = new { Name = "Name", ID = 344 };
        }


        private void AddTask()
        {
            ViewState[ViewStateKeyPrev] = this.BRAND_ID.ToString();

            hdnBrandID.Value = "0";
            txtBrandCode.Text = "";
            txtBrandName.Text = "";
            this.EditMode = FormDataMode.Add;
            this.IsDirty = false;
            ViewState[ViewStateKey] = "0";
            lblHeader.Text = "Brand Entry : New";
            txtBrandName.Focus();
            SetControl(FormDataMode.Add);
            Helper.SetStatusMessage(lblMessage, string.Empty, MessageTypeEnum.None);
        }

        private void EditTask()
        {
            this.EditMode = FormDataMode.Edit;
            lblHeader.Text = "Brand Entry : Edit";
            ViewState[ViewStateKey] = this.BRAND_ID.ToString();
            ReadData(this.BRAND_ID);
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
            if (this.BRAND_ID > 0)
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
                    this.BRAND_ID = prevID;
                    ReadTask();
                }
                else
                {
                    this.BRAND_ID = 0;
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

                string redirectURL = "~/Inventory/Brand_entry.aspx?id=" + this.BRAND_ID.ToString();
                redirectURL = base.SetPageTabQueryString(redirectURL);
                redirectURL = base.SetPageMessageQueryString(redirectURL, this.AppMessageID);
                Response.Redirect(redirectURL, false);
                ReadTask();
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


        private bool ReadData(int pBrandID)
        {
            dcBRAND cObj = BRANDBL.GetBrandInfoByID(pBrandID);

            bool bStatus;
            if (cObj != null)
            {
                this.BRAND_ID = cObj.BRAND_ID;
                this.hdnBrandID.Value = cObj.BRAND_ID.ToString();

                txtBrandCode.Text = cObj.BRAND_CODE;
                txtBrandName.Text = cObj.BRAND_NAME;
                ddlIsActive.SelectedValue =cObj.IS_ACTIVE;
                bStatus = true;
            }
            else
            {
                this.hdnBrandID.Value = "0";
                this.BRAND_ID = 0;
                bStatus = false;
               
            }
            return bStatus;

        }

        private bool CheckData()
        {

            int BrandID = Conversion.StringToInt(hdnBrandID.Value);

     


            if (txtBrandName.Text.Trim() == string.Empty)
            {
                //Helper.SetStatusMessage(lblMessage, "Please Enter Employee Name", MessageTypeEnum.InvalidData);
                this.SetPageMessage("Please Enter Brand Name", MessageTypeEnum.InvalidData);
                this.ShowPageMessage(lblMessage, true);
                txtBrandName.Focus();
                return false;
            }

            if(EditMode==FormDataMode.Add)
            { 
            if (BRANDBL.IsBrandNameExists(txtBrandName.Text.Trim()))
            { 
                this.SetPageMessage("Brand Name Already Exist !!!", MessageTypeEnum.InvalidData);
                this.ShowPageMessage(lblMessage, true);
                txtBrandName.Focus();
                return false;
            }
            }

            else if(EditMode==FormDataMode.Edit)
            {
                if (BRANDBL.IsBrandNameExists(txtBrandName.Text.Trim(),this.BRAND_ID))
                {
                    this.SetPageMessage("Brand Name Already Exist !!!", MessageTypeEnum.InvalidData);
                    this.ShowPageMessage(lblMessage, true);
                    txtBrandName.Focus();
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

            dcUser user = AppSecurity.GetUserInfoFromSession();

            //int locID = user.LoginLocationID;
            int userid = user.UserID;
            int newItemID = 0;

            dcBRAND cObj = new dcBRAND();

            //cObj.CompanyID = this.CompanyID;
            cObj.BRAND_ID = this.BRAND_ID;

            cObj.BRAND_NAME = txtBrandName.Text.Trim();
            if(txtBrandCode.Text != "")
            {
                cObj.BRAND_CODE=txtBrandCode.Text;

            }
            else
            {
                cObj.BRAND_CODE = BRANDBL.GETBrandCode(null);
            }
            
            cObj.IS_ACTIVE = ddlIsActive.SelectedValue;

            cObj.ENTRY_BY = user.UserName;
            cObj.ENTRY_DATE = DateTime.Now;


            bool isAdd = EditMode == FormDataMode.Add;

            newItemID = BRANDBL.Save(cObj, isAdd);
            if (newItemID > 0)
            {
                saveMsg = EditMode == FormDataMode.Add ? "New Data saved successfully." : "Edited Data saved successfully.";
                bStatus = true;
            }
            else
            {
                saveMsg = "Error Occured! Not Saved";
            }

            if (bStatus)
            {
                this.BRAND_ID = newItemID;
            }
            return bStatus;
        }

        //protected void SetIsGrossProfit(int pGLGroupIDParent)
        //{
        //    if (pGLGroupIDParent == (int)GLClassEnum.Income |
        //           pGLGroupIDParent == (int)GLClassEnum.Expense)
        //    {
        //        lblIsGrossProfit.Style.Add("visibility", "visible");
        //        ddlIsGrossProfit.Style.Add("visibility", "visible");
        //    }
        //    else
        //    {
        //        lblIsGrossProfit.Style.Add("visibility", "hidden");
        //        ddlIsGrossProfit.Style.Add("visibility", "hidden");
        //    }
        //}

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
