using PG.BLLibrary.InventoryBL;
using PG.BLLibrary.ProductionBL;
using PG.Core;
using PG.Core.DBBase;
using PG.Core.Utility;
using PG.Core.Web;
using PG.DBClass;
using PG.DBClass.InventoryDC;
using PG.DBClass.ProductionDC;
using PG.DBClass.SecurityDC;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PG.Web.Admin
{
    public partial class Role_Menu_bind : BagePage
    {
        private  dcUser loggedinUser = null;
        string saveMsg = string.Empty;
        string errMsg = string.Empty;
        string ViewStateKey = "ViewStateKey";

        public string MenuListServiceLink = PageLinks.SystemLinks.GetLink_MenuItem;
        //public string ItemListServiceLink = PageLinks.ProductionLink.GetLink_ProductionItemList;
        //public string BOMItemNameListServiceLink =  PageLinks.ProductionLink.GetLink_BOMItemList;  
        //public string BOMItemListServiceLink = PageLinks.ProductionLink.GetLink_BOMItemList;
        //public string PanelUOMServiceLink = PageLinks.ProductionLink.GetLink_UOMList;
        //public string BOMListServiceLink = PageLinks.ProductionLink.GetLink_BOMList;
        //public string SupporvisorListServiceLink = PageLinks.ProductionLink.GetLink_SuppervisorList;
        //public string MachineListServiceLink = PageLinks.ProductionLink.GetLink_MACHINEList;

        int PROD_ID = 0;
        string newProd_NO = "";

        List<dcPRODUCTION_MST> listMst = new List<dcPRODUCTION_MST>();
        List<dcPRODUCTION_DTL> listDetails = new List<dcPRODUCTION_DTL>();
        List<dcPRODUCTION_FLOOR_CLOSING> listClosingDetails = new List<dcPRODUCTION_FLOOR_CLOSING>();
        List<dcITEM_STOCK_DETAILS> stockReceivelist = new List<dcITEM_STOCK_DETAILS>();
        List<dcITEM_STOCK_DETAILS> stockIssuelist = new List<dcITEM_STOCK_DETAILS>();

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
            this.PROD_ID = base.GetPageQueryInteger("id");
            loggedinUser = AppSecurity.GetUserInfoFromSession();

            if (!IsPostBack)
            {
               
                hdnLoggedInUser.Value = loggedinUser.UserID.ToString();
                hdnCompanyID.Value = loggedinUser.CompanyId.ToString();
                FillCombo();
                if (this.PROD_ID == 0) //not query string
                {
                    AddTask();
                }
                else
                {
                    hdnPROD_ID.Value = this.PROD_ID.ToString();
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
        }

        private void ReadTask()
        {
            lblHeader.Text = "IB Filling Entry : View";
            this.EditMode = FormDataMode.Read;
            base.ShowPageMessage(lblMessage, false);
            this.PROD_ID = Conversion.StringToInt(hdnPROD_ID.Value);
            ReadMstData(this.PROD_ID);
            ReadDetails(this.PROD_ID);
             
            ViewState[ViewStateKey] = this.PROD_ID.ToString();

            SetControl(FormDataMode.Read);
            SetControlClosingGrid(FormDataMode.Read);

        }

        private void EditTask()
        {
            this.EditMode = FormDataMode.Edit;
            lblHeader.Text = "IB Filling Entry : Edit";
            base.ShowPageMessage(lblMessage, false);
            this.PROD_ID = Conversion.StringToInt(hdnPROD_ID.Value);
            ReadMstData(this.PROD_ID);
            ReadDetails(this.PROD_ID);
            ViewState[ViewStateKey] = this.PROD_ID.ToString();
            SetControl(FormDataMode.Edit);
            SetControlClosingGrid(FormDataMode.Edit);
        }

        //private void ReadSTKDtl(int pPROD_NO, string pTRANS_REF_NO)
        //{
        //    stockReceivelist = ITEM_STOCK_DETAILBL.GetSTKDtlforPROD_NO(0, pTRANS_REF_NO, 1001, null);
        //}

        private void ReadDetails(int pPROD_ID)
        {
            List<dcPRODUCTION_DTL> listDetails = PRODUCTION_DTLBL.GetProductionDtlsByProdID(pPROD_ID, null);
            List<dcPRODUCTION_FLOOR_CLOSING> listClosingDetls = PRODUCTION_FLOOR_CLOSINGBL.GetProductionClosingDtlsByProdID(pPROD_ID, null);

           
            BindDataToGrid(listDetails);
            BindClosingDataToGrid(listClosingDetls);

            //lblTotal.Text = "Total:" + GridView1.Rows.Count.ToString();
        }

        private void ReadMstData(int pProd_ID)
        {
            dcPRODUCTION_MST cObj = PRODUCTION_MSTBL.GetProductionByProdID(pProd_ID.ToString(),null);

            if (cObj != null)
            {
                hdnPROD_ID.Value = cObj.PROD_ID.ToString();
               
                hdnSUPERVISOR_ID.Value = cObj.SUPERVISOR_ID;
                ddlDEPT_ID.SelectedValue =  cObj.DEPT_ID.ToString();
                //txtREF_NO_MANUAL.Text = cObj.REF_NO_MANUAL;
                hndFORECUSTID.Value = cObj.FORECUST_ID.ToString();

                if (cObj.AUTH_STATUS == "Y")
                { 
                    btnEdit.Enabled = false;
                    btnAuthorize.Enabled = false;
                }
                else
                {
                    btnEdit.Enabled = true;
                    btnAuthorize.Enabled = true;
                }

            }
        }

        private void AddTask()
        {

            this.EditMode = FormDataMode.Add;
            this.IsDirty = false;
            hdnPROD_ID.Value = "0";
            

            hdnSUPERVISOR_ID.Value = "0";
            //txtPROCESS_CODE.Text = String.Empty;
            hdnPROD_ID.Value = String.Empty;

            lblHeader.Text = "IB Filling Entry";
            this.listDetails.Clear();
            this.listClosingDetails.Clear();
            BindDataToGrid(this.listDetails);
            BindClosingDataToGrid(this.listClosingDetails);
            SetControl(FormDataMode.Add);
            Helper.SetStatusMessage(lblMessage, string.Empty, MessageTypeEnum.None);
        }

        public void FillCombo()
        {

            bool hasMultipleItem;
            ddlRole.Items.Clear();
            ddlRole.AppendDataBoundItems = true;
            ddlRole.DataTextField = "DEPARTMENT";
            ddlRole.DataValueField = "DEPARTMENT_ID";

            ddlRole.DataSource = loggedinUser.IsAdmin ? DEPARTMENT_INFOBL.UserDepartment_List(0, out hasMultipleItem) : DEPARTMENT_INFOBL.UserDepartment_List(loggedinUser.UserID, out hasMultipleItem);
            if (hasMultipleItem)
            {
                ddlRole.Items.Insert(0, new ListItem("(--all--)", ""));
            }

            ddlRole.DataBind();
            //ddlDEPT_ID.SelectedIndex = 0;
           // ddlRole.SelectedValue = "18";
            //if (ddlDEPT_ID.SelectedValue != "18")
            //{
            //    ddlDEPT_ID.Items.Insert(0, new ListItem("(--all--)", ""));
            //    ddlDEPT_ID.SelectedIndex = 0;
            //}
            //ddlDEPT_ID.Enabled = false;
        }

       


        protected void GRDDTLITEMLIST_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int RowIndex = gvr.RowIndex;
                GRDDTLITEMLIST.Rows[RowIndex].Visible = false;
                RefreshGrid();

            }
        }

        private void RefreshGrid()
        {
            int slNo = 0;
            foreach (GridViewRow gvR in this.GRDDTLITEMLIST.Rows)
            {
                if (gvR.RowType == DataControlRowType.DataRow)
                {
                    if (gvR.Visible)
                    {
                        slNo++;
                        ((Label)gvR.FindControl("lblSLNo")).Text = slNo.ToString();
                    }
                }
            }
        }

        protected void GRDDTLITEMLIST_RowCreated(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:
                    e.Row.CssClass += " gridRow";
                    break;
                case DataControlRowType.Header:
                    e.Row.CssClass += " headerRow_Prod";
                    break;
                case DataControlRowType.Footer:
                    e.Row.CssClass += " footerRow";
                    break;
                case DataControlRowType.Pager:
                    e.Row.CssClass += " pagerRow";
                    break;
                case DataControlRowType.EmptyDataRow:
                    e.Row.CssClass += " gridRow";
                    break;
            }
        }

        protected void GRDDTLITEMLIST_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string rowID = e.Row.ClientID;
                string js = string.Format("return ShowDetailsPopup('{0}');", rowID);

                LinkButton lnkDelete = (LinkButton)e.Row.FindControl("btnDeleteRow");
                string jsDelete = "return confirm('Are you sure to delete current row?');";
                lnkDelete.OnClientClick = jsDelete;


                dcPRODUCTION_DTL det = e.Row.DataItem as dcPRODUCTION_DTL;
                if (det._RecordState == RecordStateEnum.Deleted)
                {
                    e.Row.Visible = false;
                }
            }
        }

        protected void GRDDTLITEMLIST_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void btnNewRow_Click(object sender, EventArgs e)
        {
            ReadDetailsFromGrid();
            AddBlankRowToGridList();
            BindDataToGrid(this.listDetails);
            SetControlGrid(FormDataMode.Add);
        }

        private void SetControlGrid(FormDataMode dataMode)
        {
            bool isEnabled = false;

            if (dataMode == FormDataMode.Add | dataMode == FormDataMode.Edit)
            {
                isEnabled = true;
            }

            foreach (GridViewRow gvR in this.GRDDTLITEMLIST.Rows)
            {
                if (gvR.RowType == DataControlRowType.DataRow)
                {
                    ((TextBox)gvR.FindControl("txtITEM_NAME")).Enabled = isEnabled;
                    //((TextBox)gvR.FindControl("txtITEM_PANEL_QTY")).Enabled = isEnabled;
                    //((TextBox)gvR.FindControl("txtPANEL_UOM_ID")).Enabled = isEnabled;
                    ((TextBox)gvR.FindControl("txtItem_qty")).Enabled = isEnabled;
                    //((TextBox)gvR.FindControl("txtITEM_WEIGHT")).Enabled = isEnabled;
                    //((TextBox)gvR.FindControl("txtWEIGHT_UOM_NAME")).Enabled = isEnabled;
                    //((TextBox)gvR.FindControl("txtMACHINE_NAME")).Enabled = isEnabled;
                    //((TextBox)gvR.FindControl("txtBOM_Name")).Enabled = isEnabled;
                    ((TextBox)gvR.FindControl("txtRemarks")).Enabled = isEnabled;
                    LinkButton lnkDelete = (LinkButton)gvR.FindControl("btnDeleteRow");
                    ((TextBox)gvR.FindControl("txtUOM_NAME")).Attributes.Add("readonly", "readonly");
                    //((TextBox)gvR.FindControl("txtOPERATOR_NAME")).Enabled = isEnabled;
                    //((TextBox)gvR.FindControl("txtUSED_BAR_PC")).Enabled = isEnabled;
                    //((TextBox)gvR.FindControl("txtBAR_TYPE")).Enabled = isEnabled;
                    //((TextBox)gvR.FindControl("txtUSED_QTY_KG")).Enabled = isEnabled;
                    lnkDelete.Enabled = isEnabled;
                    if (!isEnabled)
                    {
                        lnkDelete.OnClientClick = "";
                    }
                }
            }


        }


 

        private void BindDataToGrid(List<dcPRODUCTION_DTL> listData)
        {
            int rowCount = listData.Count;
            GRDDTLITEMLIST.DataSource = listData.ToList();
            GRDDTLITEMLIST.DataBind();
            GRDDTLITEMLIST.CssClass = "grid";
        }

        private void ReadDetailsFromGrid()
        {

            this.listDetails.Clear();
            ///addition
            foreach (GridViewRow gvR in this.GRDDTLITEMLIST.Rows)
            {
                if (gvR.RowType == DataControlRowType.DataRow)
                {

                    dcPRODUCTION_DTL cObj = new dcPRODUCTION_DTL();
                    cObj.PROD_MST_ID = this.PROD_ID;
                    ReadGridRowToObject(gvR, this.GRDDTLITEMLIST.DataKeys, cObj);


                    if (cObj._RecordState == RecordStateEnum.Deleted)
                    {
                        if (cObj.PROD_DTL_ID > 0)
                        {
                            this.listDetails.Add(cObj);
                        }
                    }
                    else
                    {
                        this.listDetails.Add(cObj);
                    }

                }
            }
        }


        private void AddBlankRowToGridList()
        {
            dcPRODUCTION_DTL cObj = new dcPRODUCTION_DTL();
            cObj._RecordState = RecordStateEnum.Added;
            cObj.SLNO = this.listDetails.Where(c => c._RecordState != RecordStateEnum.Deleted).Count() + 1;
            this.listDetails.Add(cObj);
        }


        private void ReadGridRowToObject(GridViewRow gvR, DataKeyArray dataKeys, dcPRODUCTION_DTL cObj)
        {
            string strD;
            
            strD = ((Label)gvR.FindControl("lblSLNo")).Text;
            cObj.SLNO = Conversion.StringToInt(strD);

            //strD = ((TextBox)gvR.FindControl("txtGroupName")).Text;
            //cObj.ITEM_GROUP_DESC = strD;

            //strD = ((HiddenField)gvR.FindControl("hdngroupId")).Value;
            //cObj.ITEM_GROUP_ID = Conversion.StringToInt(strD);

            strD = ((HiddenField)gvR.FindControl("hdnPROD_DTL_ID")).Value;
            cObj.PROD_DTL_ID = Conversion.StringToInt(strD);

            strD = ((TextBox)gvR.FindControl("txtITEM_NAME")).Text;
            cObj.ITEM_NAME = strD;

            strD = ((HiddenField)gvR.FindControl("hdnItemID")).Value;
            cObj.ITEM_ID = Conversion.StringToInt(strD);

            //strD = ((TextBox)gvR.FindControl("txtITEM_PANEL_QTY")).Text;
            //cObj.ITEM_PANEL_QTY = Conversion.StringToDecimal(strD);

            //strD = ((HiddenField)gvR.FindControl("hdnPANEL_PC")).Value;
            //cObj.PANEL_PC = Conversion.StringToInt(strD);

            //strD = ((TextBox)gvR.FindControl("txtPANEL_UOM_ID")).Text;
            //cObj.PANEL_UOM_NAME = strD;

            //strD = ((HiddenField)gvR.FindControl("hdnPANEL_UOM_ID")).Value;
            //cObj.PANEL_UOM_ID = Conversion.StringToInt(strD);

            strD = ((TextBox)gvR.FindControl("txtItem_qty")).Text;
            cObj.ITEM_QTY = Conversion.StringToDecimal(strD);

            strD = ((TextBox)gvR.FindControl("txtUOM_NAME")).Text;
             cObj.UOM_NAME = strD;

             strD = ((HiddenField)gvR.FindControl("hdnUomID")).Value;
             cObj.UOM_ID = Conversion.StringToInt(strD);

            // strD = ((TextBox)gvR.FindControl("txtITEM_WEIGHT")).Text;
            //cObj.ITEM_WEIGHT = Conversion.StringToDecimal(strD);

            //strD = ((TextBox)gvR.FindControl("txtWEIGHT_UOM_NAME")).Text;
            //cObj.WEIGHT_UOM_NAME = strD;

            //strD = ((HiddenField)gvR.FindControl("hndWEIGHT_UOM_ID")).Value;
            //cObj.WEIGHT_UOM_ID = Conversion.StringToInt(strD);

            //strD = ((TextBox)gvR.FindControl("txtMACHINE_NAME")).Text;
            //cObj.MACHINE_NAME =  strD;

            //strD = ((HiddenField)gvR.FindControl("hndMACHINE_ID")).Value;
            //cObj.MACHINE_ID =  Conversion.StringToInt(strD);

            //strD = ((TextBox)gvR.FindControl("txtBOM_Name")).Text;
            //cObj.BOM_NAME = strD;

            //strD = ((HiddenField)gvR.FindControl("hdnITEM_BOM_ID")).Value;
            //cObj.BOM_ID = Conversion.StringToInt(strD); ;

            strD = ((TextBox)gvR.FindControl("txtRemarks")).Text;
            cObj.REMARKS = strD;
            
            //strD = ((TextBox)gvR.FindControl("txtOPERATOR_NAME")).Text;
            //cObj.OPERATOR_NAME = strD;

            //strD = ((HiddenField)gvR.FindControl("hdnOPERATOR_ID")).Value;
            //cObj.OPERATOR_ID = strD;

            //strD = ((TextBox)gvR.FindControl("txtUSED_BAR_PC")).Text;
            //cObj.USED_BAR_PC = Conversion.StringToInt(strD);

            //strD = ((TextBox)gvR.FindControl("txtBAR_TYPE")).Text;
            //cObj.BAR_TYPE_NAME = strD;

            //strD = ((HiddenField)gvR.FindControl("hdnBAR_TYPE")).Value;
            //cObj.BAR_TYPE = Conversion.StringToInt( strD);


            //strD = ((TextBox)gvR.FindControl("txtUSED_QTY_KG")).Text;
            //cObj.USED_QTY_KG = Conversion.StringToDecimal(strD);

            //strD = ((HiddenField)gvR.FindControl("hdnBAR_WEIGHT")).Value;
            //cObj.BAR_WEIGHT = Conversion.StringToDecimal(strD);

            cObj._RecordState = RecordStateEnum.Added;

            ////strD = ((TextBox)gvR.FindControl("txtReference")).Text;
            ////cObj.ReferenceText = strD;

            if(cObj.PROD_DTL_ID>0)
            {
                cObj._RecordState = RecordStateEnum.Edited;
            }

            if (!gvR.Visible)
            {
                cObj._RecordState = RecordStateEnum.Deleted;
            }


        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
           if(isExists())
              SaveTask();
        }

        private bool isExists()
        {
            bool isExist = true;
            if (Conversion.StringToInt(hdnPROD_ID.Value) == 0)
            {
                //dcPRODUCTION_MST cExist = PRODUCTION_MSTBL.GetSulphationProductionInfoByDateShift(Conversion.StringToDateORNull(txtPRODUCTION_DATE.Text), ddlSHIFT_ID.SelectedValue, Conversion.StringToInt(ddlDEPT_ID.SelectedValue), "", null);
                //if (cExist != null)
                //{
                //    this.SetPageMessage("Production entry already exists for this shift !!", MessageTypeEnum.InvalidData);
                //    base.ShowPageMessage(lblMessage, true);
                //    isExist = false;
                //}
            }

             //string msg = INV_WORKING_MONTHBL.Is_Working_Date_Within_Declared_Month(null, Conversion.StringToDate(txtPRODUCTION_DATE.Text));
             //if (!string.IsNullOrEmpty(msg))
             //{
             //    this.SetPageMessage("Month is not declared for the month of selected date.!!", MessageTypeEnum.InvalidData);
             //    base.ShowPageMessage(lblMessage, true);
             //    // Month is not declared for the month of selected date.
             //    txtPRODUCTION_DATE.Focus();
             //    isExist = false;
             //}

            return isExist;
        }
        private bool CheckData()
        {
            if (txtSUPERVISOR_NAME.Text.Trim() == string.Empty)
            {
                this.SetPageMessage("Please Enter Supervisor Name !!", MessageTypeEnum.InvalidData);
                txtSUPERVISOR_NAME.Focus();
                return false;
            }

            if (ddlDEPT_ID.SelectedValue == string.Empty)
            {
                this.SetPageMessage("Please Select Department Name !!", MessageTypeEnum.InvalidData);
                ddlDEPT_ID.Focus();
                return false;
            }

            if (ddlFORECUSTMONTH.SelectedValue == string.Empty)
            {
                this.SetPageMessage("Please Select Forecast Name !!", MessageTypeEnum.InvalidData);
                ddlSHIFT_ID.Focus();
                return false;
            }

            if (ddlSHIFT_ID.SelectedValue == string.Empty)
            {
                this.SetPageMessage("Please Select Shift Name !!", MessageTypeEnum.InvalidData);
                ddlSHIFT_ID.Focus();
                return false;
            }

            if (txtPRODUCTION_DATE.Text.Trim() == string.Empty)
            {
                this.SetPageMessage("Please Enter Production Date !!", MessageTypeEnum.InvalidData);
                txtPRODUCTION_DATE.Focus();
                return false;
            }


            if (txtPROD_BATCH_NO.Text.Trim() == string.Empty)
            {
                this.SetPageMessage("Please Enter batch NO !!", MessageTypeEnum.InvalidData);
                txtPROD_BATCH_NO.Focus();
                return false;
            }

            if(!CheckStarttime(txtBATCH_STARTTIMEs.Text,txtBATCH_ENDTIMEs.Text))
            {
                this.SetPageMessage("Please Enter Correct Batch Start & End Time !!", MessageTypeEnum.InvalidData);
                txtBATCH_STARTTIMEs.Focus();
                return false;
            }

            ReadDetailsFromGrid();

            if (!ValidateDetails(this.listDetails))
            {
                return false;
            }

            ReadClosingDetailsFromGrid();
           

            if (ValidateClosingDetails(this.listClosingDetails))
            {
                return true;
            }
            else
            {
                return false;
            }
            base.ShowPageMessage(lblMessage, false);
            return true;
        }

        private bool CheckStarttime(string pStartTime, string pEndTime)
        {
            bool sStatus = true;
            if (pStartTime != "")
            {
                // 02:30
               
                int Sfp = Conversion.StringToInt(pStartTime.Substring(0, 2));
                int Ssp = Conversion.StringToInt(pStartTime.Substring(3, 2));

                int Efp = Conversion.StringToInt(pEndTime.Substring(0, 2));
                int Esp = Conversion.StringToInt(pEndTime.Substring(3, 2));

                if (Sfp > 12 | Ssp > 60)
                {
                    sStatus = false;
                }

                if (Efp > 12 | Esp > 60)
                {
                    sStatus = false;
                }
            }
                return sStatus;
            
        }

        private void CreateItemStockDetailsFromGrid()
        {
             this.stockReceivelist.Clear();
               foreach (var item in this.listDetails)
               {
                   // Stock Receive 
                   dcITEM_STOCK_DETAILS stkReceive = new dcITEM_STOCK_DETAILS();
                   stkReceive.ITEM_ID = item.ITEM_ID;
                   stkReceive.UOM_ID = item.UOM_ID;
                   stkReceive.TRANS_DATE = DateTime.Now;
                   stkReceive.TRANS_TIME = DateTime.Now;
                   stkReceive.INV_TRANS_DET_ID = item.PROD_DTL_ID;
                   stkReceive.TRANS_QTY = item.ITEM_QTY;
                   stkReceive.RCV_QTY = item.ITEM_QTY;
                   stkReceive.TRANS_REF_NO = txtPROD_NO.Text;
                   stkReceive.STORE_ID = 0;
                   stkReceive.INV_TRANS_TYPE_ID = 1001;
                   stkReceive.CREATE_BY = loggedinUser.UserID;
                   stkReceive.CREATE_DATE = DateTime.Now;
                   stkReceive.TRANS_REMARKS = "IB Filling Entry Receive";
                   stkReceive.FROM_DEPARTMENT_ID = Conversion.StringToInt(ddlDEPT_ID.SelectedValue);
                   stkReceive.TO_DEPARTMENT_ID = Conversion.StringToInt(ddlDEPT_ID.SelectedValue);
                   stkReceive.DEPARTMENT_ID = Conversion.StringToInt(ddlDEPT_ID.SelectedValue);
                   stkReceive.IS_PRODUCTION = "Y";
                   stockReceivelist.Add(stkReceive);
               }
        }
        

        private bool ValidateClosingDetails(List<dcPRODUCTION_FLOOR_CLOSING> list)
        {
            bool y = true;
            if (list.Where(x => x.ISSUE_STOCK > 0).Any())
            {


                if (!(list.Where(x => x._RecordState != RecordStateEnum.Deleted).GroupBy(x => x.CLOSING_ITEM_ID).Any(g => g.Count() > 1)))
                {
                    foreach (var item in list)
                    {
                        if (item._RecordState != RecordStateEnum.Deleted)
                        {
                            if (!(item.CLOSING_ITEM_ID > 0))
                            {
                                errMsg = errMsg + "Please Select Item for Sl . " + +item.CLOSING_SI + ".";
                                this.SetPageMessage(errMsg, MessageTypeEnum.InvalidData);
                            }
                            if (!(item.ISSUE_STOCK > 0))
                            {
                                errMsg = errMsg + "Issue Quantity is required for item " + item.CLOSING_SI + ".";
                                this.SetPageMessage(errMsg, MessageTypeEnum.InvalidData);
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(errMsg))
                    {
                        return false;
                    }
                }
                else
                {
                    errMsg = errMsg + "You can't add duplicate item .";
                    this.SetPageMessage(errMsg, MessageTypeEnum.InvalidData);
                    return false;
                }
            }
            else
            {
                errMsg = errMsg + "Please  add at least one item.";
                this.SetPageMessage(errMsg, MessageTypeEnum.InvalidData);
                return false;
            }

            return y;
        }

        private bool ValidateDetails(List<dcPRODUCTION_DTL> list)
        {
            bool y = true;
            if (list.Where(x => x.ITEM_QTY > 0).Any())
            {
                ////if (!(list.Where(x => x._RecordState != RecordStateEnum.Deleted).GroupBy(x => x.ITEM_ID).Any(g => g.Count() > 1)))
                //{
                    foreach (var item in list)
                    {
                        if (item._RecordState != RecordStateEnum.Deleted)
                        {
                            if (!(item.ITEM_ID > 0))
                            {
                                errMsg = errMsg + "Please Select Item for Sl . " + +item.SLNO + ".";
                                this.SetPageMessage(errMsg, MessageTypeEnum.InvalidData);
                            }
                            if (!(item.ITEM_QTY > 0))
                            {
                                errMsg = errMsg + "Quantity is required for item " + item.SLNO + ".";
                                this.SetPageMessage(errMsg, MessageTypeEnum.InvalidData);
                            }
                            //if (!(item.ITEM_PANEL_QTY > 0))
                            //{
                            //    errMsg = errMsg + "Quantity is required for item panel Qty " + item.SLNO + ".";
                            //    this.SetPageMessage(errMsg, MessageTypeEnum.InvalidData);
                            //}

                            //if (!(item.ITEM_WEIGHT > 0))
                            //{
                            //    errMsg = errMsg + "Quantity is required for item weight Qty " + item.SLNO + ".";
                            //    this.SetPageMessage(errMsg, MessageTypeEnum.InvalidData);
                            //}

                            //if (!(item.WEIGHT_UOM_ID > 0))
                            //{
                            //    errMsg = errMsg + "Please Select for item weight UOM " + item.SLNO + ".";
                            //    this.SetPageMessage(errMsg, MessageTypeEnum.InvalidData);
                            //}

                            //if (!(item.PANEL_UOM_ID > 0))
                            //{
                            //    errMsg = errMsg + "Please Select for item panel UOM " + item.SLNO + ".";
                            //    this.SetPageMessage(errMsg, MessageTypeEnum.InvalidData);
                            //}

                            //if (!(item.BOM_ID > 0))
                            //{
                            //    errMsg = errMsg + "Please Select for item BOM Name " + item.SLNO + ".";
                            //    this.SetPageMessage(errMsg, MessageTypeEnum.InvalidData);
                            //}
                        }
                    }

                    if (!string.IsNullOrEmpty(errMsg))
                    {
                        return false;
                    }
                //}
                //else
                //{
                //    errMsg = errMsg + "You can't add duplicate item .";
                //    this.SetPageMessage(errMsg, MessageTypeEnum.InvalidData);
                //    return false;
                //}


            }
            else
            {
                errMsg = errMsg + "Please  add at least one item.";
                this.SetPageMessage(errMsg, MessageTypeEnum.InvalidData);
                return false;
            }

            return y;
        }

        private bool SaveTask()
        {
            if (!Page.IsValid)
            { return false; }

            if (!CheckData())
            {
                base.ShowPageMessage(lblMessage, true);
                return false;
            }

            bool bStatus = SaveData();

            if (bStatus)
            {
                this.IsDirty = false;
                base.ShowPageMessage(lblMessage, true);
            }
            else
            {
                saveMsg = "Data Save Fail !!";
                this.SetPageMessage(saveMsg, MessageTypeEnum.Error);
                base.ShowPageMessage(lblMessage, true);
            }
            return bStatus;
        }

        private bool SaveData()
        {
           
                bool bStatus = false;
                int newProd_ID = 0;
                bool isAdd = false;

                DBContext dc = null;
                bool isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                bool isTransInit = dc.StartTransaction();
            try
                {

                dcPRODUCTION_MST cObj = new dcPRODUCTION_MST();
                dcUser user = AppSecurity.GetUserInfoFromSession();
                string _Prod_ID = (hdnPROD_ID.Value == "") ? "0" : hdnPROD_ID.Value;
                cObj.PROD_ID = Conversion.StringToInt(_Prod_ID);
                if (txtPROD_NO.Text == "")
                    cObj.PROD_NO = PRODUCTION_MSTBL.NEW_PROD_NO(Conversion.StringToInt(ddlDEPT_ID.SelectedValue), txtPRODUCTION_DATE.Text.Trim(), null);
                else
                    cObj.PROD_NO = txtPROD_NO.Text;

                newProd_NO = cObj.PROD_NO;
                hdnPROD_ID.Value = cObj.PROD_ID.ToString();
                cObj.FACTORY_ID = Conversion.StringToInt(hdnCompanyID.Value);
                cObj.SHIFT_ID = ddlSHIFT_ID.SelectedValue.ToString();
                cObj.SUPERVISOR_ID = hdnSUPERVISOR_ID.Value;

                cObj.DEPT_ID = Conversion.StringToInt(ddlDEPT_ID.SelectedValue.ToString());
                cObj.FORECUST_ID = Conversion.StringToInt(ddlFORECUSTMONTH.SelectedValue);
                //cObj.REJECTED_QTY = Conversion.StringToDecimal(txtREJECTED_QTY.Text.Trim());
                //cObj.REF_NO_MANUAL = txtREF_NO_MANUAL.Text.Trim();

                if (txtBATCH_STARTTIME.Text !="")
                cObj.BATCH_STARTTIME = Conversion.StringToDateORNull(txtBATCH_STARTTIME.Text.Trim());

                if (txtBATCH_ENDTIME.Text != "")
                cObj.BATCH_ENDTIME = Conversion.StringToDateORNull(txtBATCH_ENDTIME.Text.Trim());

                cObj.STARTTIME = txtBATCH_STARTTIMEs.Text.Trim();
                cObj.ENDTIME = txtBATCH_ENDTIMEs.Text.Trim();

               // cObj.PROCESS_CODE = Conversion.StringToInt(txtPROCESS_CODE.Text.Trim());
                cObj.PRODUCTION_DATE = Conversion.StringToDate(txtPRODUCTION_DATE.Text);

                if (txtBATCH_ID.Text == "")
                    cObj.BATCH_ID = PRODUCTION_MSTBL.NEW_IB_BATCH_NO(txtPRODUCTION_DATE.Text.Trim(), ddlSHIFT_ID.SelectedValue.ToString(), null);
                else
                    cObj.BATCH_ID = txtBATCH_ID.Text;

                cObj.PROD_BATCH_NO = txtPROD_BATCH_NO.Text;

                cObj.ProductionDetList = listDetails;
                cObj.ProductionClosingDetList = listClosingDetails;


                if (this.PROD_ID > 0)
                {
                    cObj.EDIT_BY_ID = user.UserID;
                    cObj.EDIT_DATE = DateTime.Now;

                    this.EditMode = FormDataMode.Edit;
                    isAdd = this.EditMode == FormDataMode.Add ? true : false;
                }
                else
                {
                    cObj.ENTRY_BY_ID = user.UserID; 
                    cObj.ENTRY_DATE = DateTime.Now;

                    this.EditMode = FormDataMode.Add;
                    isAdd = this.EditMode == FormDataMode.Add ? true : false;

                }

              

                // Productoin details Delete 
                List<dcPRODUCTION_DTL> deletedList = new List<dcPRODUCTION_DTL>();
                deletedList = cObj.ProductionDetList.Where(x => x._RecordState == RecordStateEnum.Deleted).ToList();
                foreach (dcPRODUCTION_DTL det in deletedList)
                {
                    PRODUCTION_DTLBL.Delete(det.PROD_DTL_ID);
                }

                //************** Closing Details Delete *******************
                List<dcPRODUCTION_FLOOR_CLOSING> deletedClosingList = new List<dcPRODUCTION_FLOOR_CLOSING>();
                deletedClosingList = cObj.ProductionClosingDetList.Where(x => x._RecordState == RecordStateEnum.Deleted).ToList();
                foreach (dcPRODUCTION_FLOOR_CLOSING det in deletedClosingList)
                {
                    PRODUCTION_FLOOR_CLOSINGBL.Delete(det.CLOSING_ID);
                }
                //******* Closing Detail Delete END************************

                newProd_ID = PRODUCTION_MSTBL.Save(cObj, isAdd);




                this.PROD_ID = newProd_ID;

                if (newProd_ID > 0)
                {
                   bStatus = true;

                    if (bStatus)
                    {
                        SetControl(FormDataMode.Read);
                        SetControlClosingGrid(FormDataMode.Read);
                        // txtPROD_NO.Text = newProd_ID.ToString();

                        hdnPROD_ID.Value = newProd_ID.ToString();
                       
                        dc.CommitTransaction(isTransInit);
                        ReadTask();
                        saveMsg = isAdd ? "New Production Entry saved successfully." : "Edited Production Entry saved successfully.";
                        base.ShowPageMessage(lblMessage, true);
                       
                        if (isAdd)
                            this.SetPageMessage("New Production Entry saved successfully.", MessageTypeEnum.Successful);
                        else
                            this.SetPageMessage("Edited Production Entry saved successfully.", MessageTypeEnum.Successful);
                        this.SetPageMessageToSession();
                       // ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Data saved successfully !!');", true);
                    }
                    else
                    {
                        dc.RollbackTransaction();
                    }
                }
            }
            catch
            {
                dc.RollbackTransaction();
            }

            // bStatus = true;
            return bStatus;
        }


        private void CreateClosingItemStockIssue(int _prodId)
        {
            List<dcPRODUCTION_FLOOR_CLOSING> listClosingDetls = PRODUCTION_FLOOR_CLOSINGBL.GetProductionClosingDtlsByProdID(_prodId, null);
            this.stockIssuelist.Clear();
            foreach (var item in  listClosingDetails)
            {
                // Stock Issue 
                dcITEM_STOCK_DETAILS stkIssue = new dcITEM_STOCK_DETAILS();
                stkIssue.ITEM_ID = item.CLOSING_ITEM_ID;
                stkIssue.UOM_ID = item.CLOSING_UOM_ID;
                stkIssue.TRANS_DATE = DateTime.Now;
                stkIssue.TRANS_TIME = DateTime.Now;
                stkIssue.INV_TRANS_DET_ID = item.CLOSING_ID;
                stkIssue.TRANS_QTY = item.ISSUE_STOCK;
                stkIssue.ISS_QTY = item.ISSUE_STOCK;
                stkIssue.TRANS_REF_NO = txtPROD_NO.Text;
                stkIssue.STORE_ID = 0;
                stkIssue.INV_TRANS_TYPE_ID = 1002;
                stkIssue.CREATE_BY = loggedinUser.UserID;
                stkIssue.CREATE_DATE = DateTime.Now;
                stkIssue.TRANS_REMARKS = "IB Filling Entry Issue";
                stkIssue.FROM_DEPARTMENT_ID = Conversion.StringToInt(ddlDEPT_ID.SelectedValue);
                stkIssue.TO_DEPARTMENT_ID = Conversion.StringToInt(ddlDEPT_ID.SelectedValue);
                stkIssue.DEPARTMENT_ID = Conversion.StringToInt(ddlDEPT_ID.SelectedValue);
                stkIssue.IS_PRODUCTION = "Y";
                stockIssuelist.Add(stkIssue);
            }
        }


        private void SetControl(FormDataMode dataMode)
        {
            bool isEnabled = false;

            if (dataMode == FormDataMode.Add | dataMode == FormDataMode.Edit)
            {
                isEnabled = true;
            }

            ddlSHIFT_ID.Enabled = isEnabled;
            txtSUPERVISOR_NAME.Enabled = isEnabled;
            //txtREF_NO_MANUAL.Enabled = isEnabled;
            //txtREJECTED_QTY.Enabled = isEnabled;
            txtBATCH_ENDTIME.Enabled = isEnabled;
            txtBATCH_STARTTIME.Enabled = isEnabled;
            txtBATCH_ENDTIMEs.Enabled = isEnabled;
            txtBATCH_STARTTIMEs.Enabled = isEnabled;
            txtPRODUCTION_DATE.Enabled = isEnabled;
            txtPROD_BATCH_NO.Enabled = isEnabled;
            txtBATCH_ID.Enabled = isEnabled;
            ddlFORECUSTMONTH.Enabled = isEnabled;

            //buttons
            btnAddNew.Visible = !isEnabled;
           
            btnEdit.Visible = !isEnabled;
            btnDelete.Visible = !isEnabled;

            btnSave.Visible = isEnabled;
            //btnAuthorize.Visible = !isEnabled;
            btnCancel.Visible = isEnabled;
            btnNewRow.Enabled = isEnabled;
            btnNewRowClosing.Enabled = isEnabled;
            SetControlGrid(dataMode);

        }

        protected void btnNewRowClosing_Click(object sender, EventArgs e)
        {
            ReadClosingDetailsFromGrid();
            AddBlankClosingRowToGridList();
            BindClosingDataToGrid(this.listClosingDetails);
            SetControlClosingGrid(FormDataMode.Add);
        }

        private void SetControlClosingGrid(FormDataMode dataMode)
        {
            bool isEnabled = false;

            if (dataMode == FormDataMode.Add | dataMode == FormDataMode.Edit)
            {
                isEnabled = true;
            }

            foreach (GridViewRow gvR in this.grdClosingRowMaterial.Rows)
            {
                if (gvR.RowType == DataControlRowType.DataRow)
                {
                    ((TextBox)gvR.FindControl("txtCLOSINGITEM_NAME")).Enabled = isEnabled;
                    ((TextBox)gvR.FindControl("txtCLOSING_QTY")).Enabled = isEnabled;
                    ((TextBox)gvR.FindControl("txtCLOSING_REMARKS")).Enabled = isEnabled;
                    //((TextBox)gvR.FindControl("txtMANUAL_OPENING_STOCK")).Enabled = isEnabled;
                    //((TextBox)gvR.FindControl("txtISSUE_STOCK")).Enabled = isEnabled;
                    ((TextBox)gvR.FindControl("txtCLOSING_QTY")).Enabled = isEnabled;

                    ((TextBox)gvR.FindControl("txtWASTAGE_QTY")).Enabled = isEnabled;
                    ((TextBox)gvR.FindControl("txtREJECTED_QTY")).Enabled = isEnabled;
                    //((TextBox)gvR.FindControl("txtPOSITIVE_DEV")).Enabled = isEnabled;
                    //((TextBox)gvR.FindControl("txtNEGATIVE_DEV")).Enabled = isEnabled;
                    if (isEnabled)
                    {
                        ((TextBox)gvR.FindControl("txtISSUE_STOCK")).Attributes.Remove("readonly");
                    }
                    else
                        ((TextBox)gvR.FindControl("txtISSUE_STOCK")).Attributes.Add("readonly", "readonly");
                    //((TextBox)gvR.FindControl("txtISSUE_STOCK")).Attributes.Add("readonly", "readonly");
                    LinkButton lnkDelete = (LinkButton)gvR.FindControl("btnDeleteRow");
                    ((TextBox)gvR.FindControl("txtCLOSING_UOM_NAME")).Attributes.Add("readonly", "readonly");
                    ((TextBox)gvR.FindControl("txtSYSTEM_OPENING_STOCK")).Attributes.Add("readonly", "readonly");
                    //((TextBox)gvR.FindControl("txtCLOSING_QTY")).Attributes.Add("readonly", "readonly");

                    lnkDelete.Enabled = isEnabled;
                    if (!isEnabled)
                    {
                        lnkDelete.OnClientClick = "";
                    }
                }
            }

            btnNewRowClosing.Enabled = isEnabled;
        }
        private void BindClosingDataToGrid(List<dcPRODUCTION_FLOOR_CLOSING> listData)
        {
            int rowCount = listData.Count;
            grdClosingRowMaterial.DataSource = listData.ToList();
            grdClosingRowMaterial.DataBind();
            grdClosingRowMaterial.CssClass = "grid";
        }


        private void AddBlankClosingRowToGridList()
        {
            dcPRODUCTION_FLOOR_CLOSING cObj = new dcPRODUCTION_FLOOR_CLOSING();
            cObj._RecordState = RecordStateEnum.Added;
            cObj.CLOSING_SI = this.listClosingDetails.Where(c => c._RecordState != RecordStateEnum.Deleted).Count() + 1;
            this.listClosingDetails.Add(cObj);
        }


        private void ReadClosingDetailsFromGrid()
        {

            this.listClosingDetails.Clear();
            ///addition
            foreach (GridViewRow gvR in this.grdClosingRowMaterial.Rows)
            {
                if (gvR.RowType == DataControlRowType.DataRow)
                {
                    dcPRODUCTION_FLOOR_CLOSING cObj = new dcPRODUCTION_FLOOR_CLOSING();
                    cObj.PROD_MST_ID = this.PROD_ID;
                    ReadClosingGridRowToObject(gvR, this.grdClosingRowMaterial.DataKeys, cObj);

                    if (cObj._RecordState == RecordStateEnum.Deleted)
                    {
                        if (cObj.PROD_MST_ID > 0)
                        {
                            this.listClosingDetails.Add(cObj);
                        }
                    }
                    else
                    {
                        this.listClosingDetails.Add(cObj);
                    }
                }
            }
        }


        private void ReadClosingGridRowToObject(GridViewRow gvR, DataKeyArray dataKeys, dcPRODUCTION_FLOOR_CLOSING cObj)
        {
            string strD;

            strD = ((HiddenField)gvR.FindControl("hdnCLOSING_ID")).Value;
            cObj.CLOSING_ID = Conversion.StringToInt(strD);

            strD = ((HiddenField)gvR.FindControl("hdnPROD_MST_ID")).Value;
            cObj.PROD_MST_ID = Conversion.StringToInt(strD);

            strD = ((Label)gvR.FindControl("lblCLOSING_SI")).Text;
            cObj.CLOSING_SI = Conversion.StringToInt(strD);

            strD = ((TextBox)gvR.FindControl("txtCLOSINGITEM_NAME")).Text;
            cObj.CLOSINGITEM_NAME = strD;

            strD = ((HiddenField)gvR.FindControl("hdnCLOSING_ITEM_ID")).Value;
            cObj.CLOSING_ITEM_ID = Conversion.StringToInt(strD);


            strD = ((TextBox)gvR.FindControl("txtCLOSING_QTY")).Text;
            cObj.CLOSING_QTY = Conversion.StringToDecimal(strD);

            strD = ((TextBox)gvR.FindControl("txtCLOSING_UOM_NAME")).Text;
            cObj.CLOSING_UOM_NAME = strD;

            strD = ((HiddenField)gvR.FindControl("hdnCLOSING_UOM_ID")).Value;
            cObj.CLOSING_UOM_ID = Conversion.StringToInt(strD);

            strD = ((TextBox)gvR.FindControl("txtCLOSING_REMARKS")).Text;
            cObj.CLOSING_REMARKS = strD;

            strD = ((TextBox)gvR.FindControl("txtSYSTEM_OPENING_STOCK")).Text;
            cObj.SYSTEM_OPENING_STOCK = Conversion.StringToDecimal(strD);

            //strD = ((TextBox)gvR.FindControl("txtMANUAL_OPENING_STOCK")).Text;
            //cObj.MANUAL_OPENING_STOCK = Conversion.StringToDecimal(strD);

            strD = ((TextBox)gvR.FindControl("txtISSUE_STOCK")).Text;
            cObj.ISSUE_STOCK = Conversion.StringToDecimal(strD);

            strD = ((TextBox)gvR.FindControl("txtWASTAGE_QTY")).Text;
            cObj.WASTAGE_QTY = Conversion.StringToDecimal(strD);

            strD = ((TextBox)gvR.FindControl("txtREJECTED_QTY")).Text;
            cObj.REJECTED_QTY = Conversion.StringToDecimal(strD);

            //strD = ((TextBox)gvR.FindControl("txtPOSITIVE_DEV")).Text;
            //cObj.POSITIVE_DEV = Conversion.StringToDecimal(strD);

            //strD = ((TextBox)gvR.FindControl("txtNEGATIVE_DEV")).Text;
            //cObj.NEGATIVE_DEV = Conversion.StringToDecimal(strD);

            if (cObj.PROD_MST_ID > 0)
                cObj._RecordState = RecordStateEnum.Edited;
            else
                cObj._RecordState = RecordStateEnum.Added;

            if (!gvR.Visible)
            {
                cObj._RecordState = RecordStateEnum.Deleted;
            }

        }

        protected void grdClosingRowMaterial_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int RowIndex = gvr.RowIndex;
                grdClosingRowMaterial.Rows[RowIndex].Visible = false;
                RefreshClosingGrid();

            }
        }

        private void RefreshClosingGrid()
        {
            int slNo = 0;
            foreach (GridViewRow gvR in this.grdClosingRowMaterial.Rows)
            {
                if (gvR.RowType == DataControlRowType.DataRow)
                {
                    if (gvR.Visible)
                    {
                        slNo++;
                        ((Label)gvR.FindControl("lblCLOSING_SI")).Text = slNo.ToString();
                    }
                }
            }
        }

        protected void grdClosingRowMaterial_RowCreated(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:
                    e.Row.CssClass += " gridRow";
                    break;
                case DataControlRowType.Header:
                    e.Row.CssClass += " headerRow_Prod";
                    break;
                case DataControlRowType.Footer:
                    e.Row.CssClass += " footerRow";
                    break;
                case DataControlRowType.Pager:
                    e.Row.CssClass += " pagerRow";
                    break;
                case DataControlRowType.EmptyDataRow:
                    e.Row.CssClass += " gridRow";
                    break;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ((TextBox)e.Row.FindControl("txtCLOSING_QTY")).Attributes.Add("onchange", "javascript:calcIssueStock(this);");
                //((TextBox)e.Row.FindControl("txtMANUAL_OPENING_STOCK")).Attributes.Add("onchange", "javascript:calcIssueStock(this);");
                
            }
        }

        protected void grdClosingRowMaterial_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                string rowID = e.Row.ClientID;
                string js = string.Format("return ShowDetailsPopup('{0}');", rowID);

                LinkButton lnkDelete = (LinkButton)e.Row.FindControl("btnDeleteRow");
                string jsDelete = "return confirm('Are you sure to delete current row?');";
                lnkDelete.OnClientClick = jsDelete;


                dcPRODUCTION_FLOOR_CLOSING det = e.Row.DataItem as dcPRODUCTION_FLOOR_CLOSING;
                if (det._RecordState == RecordStateEnum.Deleted)
                {
                    e.Row.Visible = false;
                }
            }
        }

        protected void grdClosingRowMaterial_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

       

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            EditTask();
        }

        protected void btnAuthorize_Click(object sender, EventArgs e)
        {
           // bool bStatus = false;

           //if(isExists())
           //   bStatus = AuthorizationTask();

           // if (bStatus)
           //{
               saveMsg = "Authorization is completed successfully.";
               this.SetPageMessage("Authorization is completed successfully.", MessageTypeEnum.Successful);
           //}
           //else
           //{
           //    saveMsg = "Authorization is fail !!";
           //    this.SetPageMessage("Authorization is fail !!", MessageTypeEnum.Successful);
           //}
            base.ShowPageMessage(lblMessage, true);
            this.SetPageMessageToSession();
        }
        private bool AuthorizationTask()
        {
           
            if (!Page.IsValid)
            { return false; }

            if (!CheckData())
            {
                base.ShowPageMessage(lblMessage, true);
                return false;
            }
            bool bStatus = false;
            int newProd_ID = 0;
            DBContext dc = null;
            bool isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            bool isTransInit = dc.StartTransaction();
            try
            {
                newProd_ID = Conversion.StringToInt(hdnPROD_ID.Value);
                PRODUCTION_MSTBL.UpdateAuthorized(newProd_ID, loggedinUser.UserID, null);
                
                //************ Start Production Stock Receive**************************//
                CreateItemStockDetailsFromGrid();
                if (stockReceivelist != null)
                {
                    foreach (dcITEM_STOCK_DETAILS det in stockReceivelist)
                    {
                        // det.TRANS_REF_NO = newProd_ID.ToString();
                        det.INV_TRANS_DET_ID = Conversion.StringToDecimal(PRODUCTION_DTLBL.GetProductionDtlsByProdID_ItemID(newProd_ID, Conversion.StringToInt(det.ITEM_ID.ToString()), dc).ToString());
                    }
                }

                if (stockReceivelist.Count > 0)
                {
                    foreach (dcITEM_STOCK_DETAILS det in stockReceivelist)
                    {
                        ITEM_STOCK_DETAILS_NBL.Delete(det.TRANS_REF_NO, 1001, null);

                    }

                    bStatus = ITEM_STOCK_DETAILS_NBL.SaveList(stockReceivelist, dc);
                }
                //*****************End Production Stock Receive ************************//

                //************ Start Production Closing Issue **************************//
                CreateClosingItemStockIssue(newProd_ID);
                if (stockIssuelist.Count > 0)
                {
                    foreach (dcITEM_STOCK_DETAILS det in stockIssuelist)
                    {
                        ITEM_STOCK_DETAILS_NBL.Delete(det.TRANS_REF_NO, 1002, null);
                    }

                    bStatus = ITEM_STOCK_DETAILS_NBL.SaveList(stockIssuelist, dc);
                }

                //************ End Production Closing Issue **************************//

                dc.CommitTransaction(isTransInit);

                ReadTask();
            }
            catch
            {
                bStatus = false;
                dc.RollbackTransaction();
            }



            return bStatus;
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            AddTask();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            ReadTask();
        }
    }
}