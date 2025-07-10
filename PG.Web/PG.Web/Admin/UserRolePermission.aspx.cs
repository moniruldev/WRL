using PG.BLLibrary.SecurityBL;
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

namespace PG.Web.Admin
{
    public partial class UserRolePermission : BagePage
    {
        private dcUser loggedinUser = null;
        string saveMsg = string.Empty;
        string errMsg = string.Empty;
        string ViewStateKey = "ViewStateKey";
        int USER_ID = 0;

        List<dcUserRole> listDetails = new List<dcUserRole>();
        
        public string RoleListServiceLink = PageLinks.SystemLinks.GetLink_RoleList;


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
            this.USER_ID = base.GetPageQueryInteger("id");
            loggedinUser = AppSecurity.GetUserInfoFromSession();
            if (!IsPostBack)
            {
                hdnLoggedInUser.Value = loggedinUser.UserID.ToString();
                if (this.USER_ID == 0) //not query string
                {
                    AddTask();
                }
                else
                {
                    hdnUSERID.Value = this.USER_ID.ToString();
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
            lblHeader.Text = "User Role Permission Entry: View";
            this.EditMode = FormDataMode.Read;
            base.ShowPageMessage(lblMessage, false);
            this.USER_ID = Conversion.StringToInt(hdnUSERID.Value);
            ReadMstData(this.USER_ID);
            ReadDetails(this.USER_ID);
            SetControl(FormDataMode.Read);
        }

        private void EditTask()
        {
            this.EditMode = FormDataMode.Edit;
            lblHeader.Text = "User Role Permission : Edit";
            base.ShowPageMessage(lblMessage, false);
            this.USER_ID = Conversion.StringToInt(hdnUSERID.Value);
            ReadMstData(this.USER_ID);
            ReadDetails(this.USER_ID);
            SetControl(FormDataMode.Edit);
        }

        private void ReadMstData(int pUser_ID)
        {
            clsPrmInventory  PObj=new clsPrmInventory();
            PObj.user_id = pUser_ID;  
            dcUserRole cObj =   UserRoleBL.GetUserPermittedRoleListByUserID(PObj).FirstOrDefault();
            if(cObj != null)
            {
                txtUSERNAME.Text = cObj.USER_NAME;
                lblUSERNAME.Text = cObj.FULLNAME;
                lblDefaultRole.Text = cObj.DEFAULT_ROLE;
                hdnUSERID.Value =  cObj.UserID.ToString();
                ddlAPPID.SelectedValue = cObj.AppID.ToString();
            }
        }

        private void ReadDetails(int pUSER_ID)
        {
            clsPrmInventory PObj = new clsPrmInventory();
            PObj.user_id = pUSER_ID;  
            List<dcUserRole> listDetails = UserRoleBL.GetUserPermittedRoleListByUserID(PObj);
            BindDataToGrid(listDetails);
        }
        private void AddTask()
        {

            this.EditMode = FormDataMode.Add;
            this.IsDirty = false;
            hdnUSERID.Value = "0";


            txtUSERNAME.Text = String.Empty;
            lblUSERNAME.Text = String.Empty;
            lblDefaultRole.Text = String.Empty;
            lblHeader.Text = "User Role Permission";
            this.listDetails.Clear();
            BindDataToGrid(this.listDetails);
            SetControl(FormDataMode.Add);
            Helper.SetStatusMessage(lblMessage, string.Empty, MessageTypeEnum.None);
        }

        private void SetControl(FormDataMode dataMode)
        {
            bool isEnabled = false;

            if (dataMode == FormDataMode.Add | dataMode == FormDataMode.Edit)
            {
                isEnabled = true;
            }

            lblUSERNAME.Attributes.Add("readOnly","true");
            //ddlAPPID.Attributes.Add("readOnly", "true");
            txtUSERNAME.Enabled = isEnabled;

            //buttons
            btnAddNew.Visible = !isEnabled;

            btnEdit.Visible = !isEnabled;

            btnSave.Visible = isEnabled;
            btnCancel.Visible = isEnabled;
            btnNewRow.Enabled = isEnabled;
            SetControlGrid(dataMode);

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


                dcUserRole det = e.Row.DataItem as dcUserRole;
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
                    ((TextBox)gvR.FindControl("txtROLENAME")).Enabled = isEnabled;
                    ((TextBox)gvR.FindControl("txtROLEDESC")).Enabled = false;
                    ((TextBox)gvR.FindControl("txtISACTIVE")).Enabled = false;
                    ((TextBox)gvR.FindControl("txtISADMIN")).Enabled = false;
                    LinkButton lnkDelete = (LinkButton)gvR.FindControl("btnDeleteRow");
                    lnkDelete.Enabled = isEnabled;
                    if (!isEnabled)
                    {
                        lnkDelete.OnClientClick = "";
                    }
                }
            }
        }
        private void BindDataToGrid(List<dcUserRole> listData)
        {
            int rowCount = listData.Count;
            GRDDTLITEMLIST.DataSource = listData.ToList();
            GRDDTLITEMLIST.DataBind();
            GRDDTLITEMLIST.CssClass = "grid";
        }
        private void AddBlankRowToGridList()
        {
            dcUserRole cObj = new dcUserRole();
            cObj._RecordState = RecordStateEnum.Added;
            cObj.SL = this.listDetails.Where(c => c._RecordState != RecordStateEnum.Deleted).Count() + 1;
            this.listDetails.Add(cObj);
        }
        private void ReadDetailsFromGrid()
        {

            this.listDetails.Clear();
            ///addition
            foreach (GridViewRow gvR in this.GRDDTLITEMLIST.Rows)
            {
                if (gvR.RowType == DataControlRowType.DataRow)
                {
                    dcUserRole cObj = new dcUserRole();
                    cObj.UserID = Conversion.StringToInt(hdnUSERID.Value);
                    cObj.AppID = 1;
                    ReadGridRowToObject(gvR, this.GRDDTLITEMLIST.DataKeys, cObj);
                    //if (cObj._RecordState != RecordStateEnum.Deleted)
                    //{
                        this.listDetails.Add(cObj);
                    //}
                    

                }
            }
        }

        private void ReadGridRowToObject(GridViewRow gvR, DataKeyArray dataKeys, dcUserRole cObj)
        {
            string strD;

            strD = ((Label)gvR.FindControl("lblSLNo")).Text;
            cObj.SL = Conversion.StringToInt(strD);

            strD = hdnUSERID.Value;
            cObj.UserID = Conversion.StringToInt(strD);

            strD = ((TextBox)gvR.FindControl("txtROLENAME")).Text;
            cObj.ROLENAME = strD;

            strD = ((HiddenField)gvR.FindControl("hndROLEID")).Value;
            cObj.RoleID = Conversion.StringToInt(strD);

            strD = ((TextBox)gvR.FindControl("txtROLEDESC")).Text;
            cObj.ROLEDESC =strD;

            strD = ((TextBox)gvR.FindControl("txtISACTIVE")).Text;
            cObj.ISACTIVE = strD;

            strD = ((TextBox)gvR.FindControl("txtISADMIN")).Text;
            cObj.ISADMIN = strD;

          
            cObj._RecordState = RecordStateEnum.Added;

            if (!gvR.Visible)
            {
                cObj._RecordState = RecordStateEnum.Deleted;
            }


        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveTask();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            EditTask();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {

        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            AddTask();
        }

        private bool CheckData()
        {
          

            ReadDetailsFromGrid();
            if (!ValidateDetails(this.listDetails))
            {
                return false;
            }
            return true;
        }

        private bool ValidateDetails(List<dcUserRole> list)
        {
            bool y = true;
            if (list.Where(x => x.RoleID > 0).Any())
            {
                if (!(list.Where(x => x._RecordState != RecordStateEnum.Deleted).GroupBy(x => x.RoleID).Any(g => g.Count() > 1)))
                {

                foreach (var item in list)
                {
                    if (item._RecordState != RecordStateEnum.Deleted)
                    {
                        if (!(item.RoleID > 0))
                        {
                            errMsg = errMsg + "Please Select Item for Sl . " + +item.SL + ".";
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

        private bool SaveData()
        {

            bool bStatus = false;
            string new_user_id = "";
            bool isAdd = false;

            DBContext dc = null;
            bool isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            bool isTransInit = dc.StartTransaction();
            try
            {

              
                dcUser user = AppSecurity.GetUserInfoFromSession();
                  new_user_id = (hdnUSERID.Value == "") ? "0" : hdnUSERID.Value;

                  //List<dcUserRole> deletedList = new List<dcUserRole>();
                  //deletedList = listDetails.Where(x => x._RecordState == RecordStateEnum.Deleted).ToList();

                  foreach (dcUserRole det in listDetails)
                  {
                      UserRoleBL.Delete(det.AppID,det.RoleID,det.UserID);
                  }

                  //List<dcUserRole> saveList = new List<dcUserRole>();
                  //saveList = listDetails.Where(x => x._RecordState == RecordStateEnum.Added).ToList();

                  bStatus = UserRoleBL.SaveList(listDetails, dc);

                if (bStatus)
                {
                    this.USER_ID = Conversion.StringToInt( new_user_id);
                    bStatus = true;
                    if (bStatus)
                    {
                        SetControl(FormDataMode.Read);
                        hdnUSERID.Value = USER_ID.ToString();
                        dc.CommitTransaction(isTransInit);
                        ReadTask();
                        base.ShowPageMessage(lblMessage, true);
                            if (isAdd)
                                this.SetPageMessage("New User Role Entry saved successfully.", MessageTypeEnum.Successful);
                            else
                                this.SetPageMessage("Edited User Role Entry saved successfully.", MessageTypeEnum.Successful);
                            this.SetPageMessageToSession();
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
                this.SetPageMessage("Data Save Fail !! ", MessageTypeEnum.Error);
                base.ShowPageMessage(lblMessage, true);
            }
            return bStatus;
        }


        protected void txtUSERNAME_TextChanged(object sender, EventArgs e)
        {
            if(txtUSERNAME.Text !="")
            {
                clsPrmInventory PObj = new clsPrmInventory();
                PObj.user_name = txtUSERNAME.Text.Trim();
                dcUserRole cObj = UserRoleBL.GetUserPermittedRoleListByUserID(PObj).FirstOrDefault();
                if (cObj != null)
                {
                    txtUSERNAME.Text = cObj.USER_NAME;
                    hdnUSERID.Value = cObj.UserID.ToString();
                    lblUSERNAME.Text = cObj.FULLNAME;
                    lblDefaultRole.Text = cObj.DEFAULT_ROLE;
                    ddlAPPID.SelectedValue = cObj.AppID.ToString();
               
                    this.USER_ID = Conversion.StringToInt(hdnUSERID.Value);
                    ReadDetails(this.USER_ID);
                    SetControl(FormDataMode.Edit);
                }
            } 
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}