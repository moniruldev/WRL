using PG.BLLibrary.AccountingBL;
using PG.BLLibrary.AccountingBL.GeneralLedgerBL;
using PG.BLLibrary.OrganizationBL;
using PG.Core;
using PG.Core.DBBase;
using PG.Core.Web;
using PG.DBClass.AccountingDC;
using PG.DBClass.AccountingDC.AccEnums;
using PG.DBClass.AccountingDC.GeneralLedgerDC;
using PG.DBClass.OrganiztionDC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PG.Web.Accounting
{
    public partial class LocationJournalType : BagePage
    {
        public string LocationServiceLink = PageLinks.OrganizationLinks.GetLink_Location;

        int CompanyID = 0;
        int LocationID = 0;
        string ViewStateKey = "LocationID";
        string ViewStateKeyPrev = "LocationID_Prev";

        string saveMsg = string.Empty;

        List<dcJournalType> listJournalType = null;

        List<dcLocationJournalType> listDetails = new List<dcLocationJournalType>();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.CompanyID = CompanyInfo.GetCompanyID();
            this.hdnCompanyID.Value = this.CompanyID.ToString();

            this.hdnBtnLoadDataID.Value = this.btnRefresh.UniqueID;

            if (!IsPostBack)  //first time
            {
                this.LocationID = base.GetPageQueryInteger("locid");
                this.hdnLocationID.Value = this.LocationID.ToString();

                ViewState[ViewStateKeyPrev] = "0";

                //dcAccYear year = AccYearBL.GetCurrentAccYear(this.CompanyID);
                //ddlAccYear.SelectedValue = year.AccYearID.ToString();

                //int accYearID = base.GetPageQueryInteger("accyearid");
                //if (accYearID > 0)
                //{
                //    ddlAccYear.SelectedValue = accYearID.ToString();
                //}

                if (this.LocationID == 0) //not query string
                {
                    AddTask();
                }
                else
                {
                    EditTask();
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
                this.LocationID = base.GetViewStateInt(this.ViewStateKey);

            }
            SetHyperLink();
            this.ShowPageMessage(this.lblMessage);
        }

        private List<dcJournalType> GetList_JournalType()
        {
            if (listJournalType == null)
            {
                listJournalType = JournalTypeBL.GetJournalTypeList(this.CompanyID);
            }
            return listJournalType;
        }

        private void SetHyperLink()
        {

            //new button
            //string hLink = "javascript:tbopen(0)";
            //if (base.PageMode == PG.Core.Web.PageModeEnum.InTab)
            //{
            //    hLink = "javascript:tbopen(0)";
            //    this.btnAddNew.PostBackUrl = string.Empty;
            //    this.btnAddNew.OnClientClick = hLink;
            //}
            //else
            //{
            //    hLink = "~/Accounts/Investment.aspx";
            //    this.btnAddNew.PostBackUrl = hLink;
            //    this.btnAddNew.OnClientClick = string.Empty;
            //}
        }

        private void ReadTask()
        {
            lblHeader.Text = "Location Journal Type";
            this.EditMode = FormDataMode.Read;
            //this.hdnJournalID.Value = this.JournalID.ToString();
            //ReadData(this.JournalID);
            ReadDetails();
            ViewState[ViewStateKey] = this.LocationID.ToString();

            SetControl(FormDataMode.Read);

            //var c = new { Name = "Name", ID = 344 };
        }

        private void AddTask()
        {
            ViewState[ViewStateKeyPrev] = this.LocationID.ToString();
           
            //this.EditMode = FormDataMode.Add;
            this.IsDirty = false;
            ViewState[ViewStateKey] = "0";
            ////lblMode.Text = "Mode: Add";
            this.listDetails.Clear();
            //CheckAndAddGridBlankRow();
            BindDataToGrid(this.listDetails);

            lblHeader.Text = "Location Journal Type";
            //SetControl(FormDataMode.Add);

            //Helper.SetStatusMessage(lblMessage, string.Empty, MessageTypeEnum.None);
        }

        private void BindDataToGrid(List<dcLocationJournalType> listData)
        {
            int rowCount = listData.Count;
            //if (rowCount == 0)
            //{
            //    listData.Add(new dcGLAccountHistoryRef());
            //}

            GridView1.DataSource = listData;
            GridView1.DataBind();

            //if (rowCount == 0)
            //{
            //    GridView1.Rows[0].Visible = false;
            //}
            //hdnRowCount.Value = rowCount.ToString();

            //using (System.IO.StreamWriter sw = new System.IO.StreamWriter("c:\\test.xls"))
            //{
            //    using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            //    {
            //        GridView1.RenderControl(hw);
            //    }
            //}
            GridView1.CssClass = "grid";
        }
       
        protected void btnNewRow_Click(object sender, EventArgs e)
        {
            ReadDetailsFromGrid();
            AddBlankRowToGridList();
            //GLAccountHistoryRefBL.UpdateSLNo(this.listDetails);
            LocationJournalTypeBL.UpdateSLNo(this.listDetails);

            BindDataToGrid(this.listDetails);
            SetControlGrid(this.EditMode);
        }

        private void AddBlankRowToGridList()
        {
            dcLocationJournalType cObj = new dcLocationJournalType();
            cObj._RecordState = RecordStateEnum.Added;
            //cObj.JournalDetID = 0;
            //cObj.JournalID = this.JournalID;
            //cObj.JournalDetSLNo = this.listDetails.Where(c => c._RecordState != RecordStateEnum.Deleted).Count() + 1;
            ////cObj.JournalDetID_Link = (this.listDetails.DefaultIfEmpty(new dcJournalDet()).Max(c => c.JournalDetID_Link)) + 1;
            //cObj.JournalDetID_Link = this.listDetails.DefaultIfEmpty().Max(c => c == null ? 0 : c.JournalDetID_Link) + 1;
            this.listDetails.Add(cObj);
        }
        private void ReadDetailsFromGrid()
        {

            int locationID = Convert.ToInt32(hdnLocationID.Value);
            this.listDetails.Clear();

            ///addition
            foreach (GridViewRow gvR in this.GridView1.Rows)
            {
                if (gvR.RowType == DataControlRowType.DataRow)
                {
                    //DataRow dRow = this.dtGrid.NewRow();
                    dcLocationJournalType cObj = new dcLocationJournalType();
                    cObj.LocationID = locationID;
                    //  cObj.SalaryHeadType_sopt = (int)SalaryHeadTypeEnum.Addition;
                    ReadGridRowToObject(gvR, this.GridView1.DataKeys, cObj);
                    this.listDetails.Add(cObj);
                }
            }
        }

        private void SetControlGrid(FormDataMode dataMode)
        {
            bool isEnabled = false;

            if (dataMode == FormDataMode.Add | dataMode == FormDataMode.Edit)
            {
                isEnabled = true;
            }

            foreach (GridViewRow gvR in this.GridView1.Rows)
            {
                if (gvR.RowType == DataControlRowType.DataRow)
                {
                    //((DropDownList)gvR.FindControl("ddlLandOwner")).Enabled = isEnabled;

                    ((DropDownList)gvR.FindControl("ddlJournalType")).Enabled = isEnabled;
                    ((TextBox)gvR.FindControl("txtJournalNoPrefix")).Enabled = isEnabled;



                    ////HyperLink lnkEdit = (HyperLink)gvR.Cells[4].Controls[0];
                    //LinkButton lnkEdit = (LinkButton)gvR.FindControl("btnEditRow");
                    //lnkEdit.Enabled = isEnabled;
                    //lnkEdit.Visible = false;

                    //LinkButton lnkDelete = (LinkButton)gvR.Cells[5].Controls[0];
                    LinkButton lnkDelete = (LinkButton)gvR.FindControl("btnDeleteRow");
                    lnkDelete.Enabled = isEnabled;


                    //make grid readonly
                    //((TextBox)gvR.FindControl("txtAccRefCategoryName")).Attributes.Add("readonly", "readonly");
                    //((TextBox)gvR.FindControl("txtAccRefCategory")).Attributes.Add("readonly", "readonly");
                    //((TextBox)gvR.FindControl("txtRequisitionAmt")).Attributes.Add("readonly", "readonly");
                    //((TextBox)gvR.FindControl("txtApprovedAmt")).Attributes.Add("readonly", "readonly");

                }
            }

            btnNewRow.Enabled = isEnabled;
        }

        private void ReadGridRowToObject(GridViewRow gvR, DataKeyArray dataKeys, dcLocationJournalType cObj)
        {
            //decimal d;
            string strD;

            //strD = this.GridView1.DataKeys[gvR.RowIndex]["_RecordState"].ToString();
            //strD = dataKeys[gvR.RowIndex]["_RecordState"].ToString();
            //decimal.TryParse(strD == string.Empty ? "0" : strD, out d);
            //cObj._RecordState = (RecordStateEnum)Convert.ToInt32(d);


            //cObj.CompanyID = this.CompanyID;

            strD = dataKeys[gvR.RowIndex]["LocationJournalTypeID"].ToString();
            cObj.LocationJournalTypeID = Convert.ToInt32(strD == string.Empty ? "0" : strD);

            //strD = dataKeys[gvR.RowIndex]["GLAccHistRefID"].ToString();
            //cObj.GLAccHistRefID = Convert.ToInt32(strD == string.Empty ? "0" : strD);


            strD = ((DropDownList)gvR.FindControl("ddlJournalType")).SelectedValue;

            //strD = ((HiddenField)gvR.FindControl("hdnAccRefCatID")).Value;
            cObj.JournalTypeID = Convert.ToInt32(strD == string.Empty ? "0" : strD);

            //strD = ((TextBox)gvR.FindControl("txtAccRefCatCode")).Text;
            //cObj.AccRefCategoryCode = strD.Trim();


            strD = ((TextBox)gvR.FindControl("txtJournalNoPrefix")).Text;
            cObj.JournalNoPrefix = strD.Trim();

            if (!gvR.Visible)
            {
                cObj._RecordState = RecordStateEnum.Deleted;
            }

        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                //Find the DropDownList in the Row
                DropDownList ddlJournalType = (e.Row.FindControl("ddlJournalType") as DropDownList);
                ddlJournalType.Items.Add(new ListItem("(select type)", "0"));
                ddlJournalType.AppendDataBoundItems = true;
                ddlJournalType.DataSource = GetList_JournalType();
                ddlJournalType.DataTextField = "JournalTypeName";
                ddlJournalType.DataValueField = "JournalTypeID";
                ddlJournalType.DataBind();


                int jrnlTypeID = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "JournalTypeID"));
                ddlJournalType.SelectedValue = jrnlTypeID.ToString();


                //string strD = DataBinder.Eval(e.Row.DataItem, "AccGLAccountID").ToString(); ;
                //HyperLink lnk = (HyperLink)e.Row.Cells[0].Controls[0];

                //string hLink = "javascript:tbopen(" + strD + ")";
                //if (base.PageMode == PG.Core.Web.PageModeEnum.InTab)
                //{
                //    hLink = "javascript:tbopen(" + strD + ")";
                //}
                //else
                //{
                //    hLink = "~/Accounts/Investment.aspx?id=" + strD;
                //}
                //lnk.NavigateUrl = "~/Admin/UserAddEdit.aspx?UserID=" + strD;
                //lnk.NavigateUrl = hLink;


              //  LinkButton lnkBtn = (LinkButton)e.Row.Cells[4].Controls[0];
               // lnkBtn.OnClientClick = "if(!confirm('Are you sure you want to delete this?')) return false;";
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            //GridView1.PageIndex = 0;
            //RefreshTask();
            LoadDataTask();

        }

        private void LoadDataTask()
        {
            int locationID = Convert.ToInt32(hdnLocationID.Value);
            if (locationID > 0)
            {
                EditTask();
            }
            else
            {
                AddTask();
            }

        }

        private void EditTask()
        {
            this.EditMode = FormDataMode.Edit;
            //lblMode.Text = "Mode: Edit";
            lblHeader.Text = "Location Journal Type";
            //this.SetMasterHeader("Edit User");
            //this.hdnJournalID.Value = this.JournalID.ToString();
            ReadData();
            ReadDetails();

            this.IsDirty = false;
            //ViewState[ViewStateKey] = this.JournalID.ToString();
            //lnkAddNew.Visible = true;
            SetControl(FormDataMode.Edit);
        }

        private void SetControl(FormDataMode dataMode)
        {
            bool isEnabled = false;

            if (dataMode == FormDataMode.Add | dataMode == FormDataMode.Edit)
            {
                isEnabled = true;
            }

            //ddlAccYear.Enabled = isEnabled;
            //rblAccRefType.Enabled = isEnabled;

            txtLocation.Enabled = isEnabled;


            txtLocationName.Attributes.Add("readonly", "readonly");



            //btnSave.Visible = isEnabled;
            //btnCancel.Visible = isEnabled;



            SetControlGrid(dataMode);

        }

        private void ReadData()
        {
            int locationID = Convert.ToInt32(hdnLocationID.Value);
            this.LocationID = locationID;

            dcLocation location = LocationBL.GetLocaionByID(locationID);

            if (location != null)
            {
                txtLocation.Text = location.LocationCode;
                txtLocationName.Text = location.LocationName;
                hdnLocationID.Value = location.LocationID.ToString();

            }
            else
            {
                txtLocation.Text = "";
                txtLocationName.Text = "";
                hdnLocationID.Value = "0";

            }



        }

        private void ReadDetails()
        {

            int locationID = Convert.ToInt32(hdnLocationID.Value);

            List<dcLocationJournalType> listData = new List<dcLocationJournalType>();
        

            listData = LocationJournalTypeBL.GetLocationJournalTypeListByLocation(locationID);


            BindDataToGrid(listData);
            //SumData(listData);

            //lblTotal.Text = "Total: " + GridView1.Rows.Count.ToString();
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //GridView1.PageIndex = e.NewPageIndex;
            ////GridView1.DataBind();
            //LoadData();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ddlAccYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            //LoadDataTask();

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int RowIndex = gvr.RowIndex;
                GridView1.Rows[RowIndex].Visible = false;
                RefreshGrid();

                //this.listDetails[RowIndex]._RecordState = RecordStateEnum.Deleted;


                //BindDataToGrid();



                //int rowIndex = Convert.ToInt32(e.CommandArgument);

            }
        }

        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:
                    e.Row.CssClass += " gridRow";
                    break;
                case DataControlRowType.Header:
                    e.Row.CssClass += " headerRow";
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

        protected void ddlAccRefType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // LoadData();
            //LoadDataTask();
        }


        private void RefreshGrid()
        {
            int slNo = 0;
            foreach (GridViewRow gvR in this.GridView1.Rows)
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


        private bool CheckData()
        {

            if (txtLocation.Text.Trim() == string.Empty)
            {
                //Helper.SetStatusMessage(lblMessage, "Please Enter Defination Name", MessageTypeEnum.InvalidData);
                this.SetPageMessage("Please Enter Location", MessageTypeEnum.InvalidData);
                txtLocation.Focus();
                return false;
            }


            if (Convert.ToInt32(hdnLocationID.Value) == 0)
            {
                //Helper.SetStatusMessage(lblMessage, "Please Enter Defination Name", MessageTypeEnum.InvalidData);
                this.SetPageMessage("Please Enter Location", MessageTypeEnum.InvalidData);
                txtLocation.Focus();
                return false;
            }



            //if (ddlRole.SelectedIndex <= 0)
            //{
            //    Helper.SetStatusMessage(lblMessage, "Please Select Role", MessageTypeEnum.InvalidData);
            //    ddlRole.Focus();
            //    return false;
            //}

            if (EditMode == FormDataMode.Add)
            {

                //if (BLLibrary.MasterBL.EmployeeBL.IsEmployeeExists(empCode))
                //{
                //    Helper.SetStatusMessage(lblMessage, "Employee Already Exists", MessageTypeEnum.InvalidData);
                //    return false;

                //}
                //if (txtPassword.Text != txtPassword2.Text)
                //{
                //    Helper.SetStatusMessage(lblMessage, "Password Not Matched", MessageTypeEnum.InvalidData);
                //    return false;
                //}
            }
            return true;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            { return; }

            if (!CheckData())
            {
                base.ShowPageMessage(lblMessage, true);
                return;
            }

            if (GridView1.Rows.Count > 0)
            {

                if (SaveData())
                {
                    //Helper.SetStatusMessage(lblMessage, saveMsg, MessageTypeEnum.Successful);
                    this.SetPageMessage(saveMsg, MessageTypeEnum.Successful);
                    //LoadData();
                    EditTask();
                }
                else
                {
                    //Helper.SetStatusMessage(lblMessage, saveMsg, MessageTypeEnum.Error);
                    this.SetPageMessage(saveMsg, MessageTypeEnum.Error);

                }
                base.ShowPageMessage(lblMessage, true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(btnSave,GetType(),"","alert('No record found.');", true);
                return;
            }
        }

        private bool SaveData()
        {
            //Old Code
            ////get the details data
            ReadDetailsFromGrid();
            ValidateGridList(this.listDetails);
            LocationJournalTypeBL.UpdateSLNo(this.listDetails);
            bool bStatus = false;

            int locationID = Convert.ToInt32(hdnLocationID.Value);

            bStatus = LocationJournalTypeBL.UpdateLocationJournalTypeList(locationID, this.listDetails);

            saveMsg = "Data Saved Successfully.";



            return bStatus;
        }

        private void ValidateGridList(List<dcLocationJournalType> cList)
        {
            List<int> delIndex = new List<int>();
            List<dcLocationJournalType> nList = new List<dcLocationJournalType>();
            foreach (dcLocationJournalType cObj in cList)
            {
                int detID = cObj.JournalTypeID;
                if (cObj._RecordState == RecordStateEnum.Deleted)
                {
                    if (detID > 0)
                    {
                        nList.Add(cObj);
                    }
                }
                else
                {
                    bool isRowValid = IsGridRowValid(cObj);
                    if (isRowValid)
                    {
                        if (detID > 0)
                        {
                            //edited
                            cObj._RecordState = RecordStateEnum.Edited;
                            //dRow["RState"] = "edited";
                        }
                        else
                        {
                            //added
                            cObj._RecordState = RecordStateEnum.Added;
                            //dRow["RState"] = "added";
                        }

                        //dtUpdate.ImportRow(dRow);
                        nList.Add(cObj);
                    }
                    else
                    {
                        if (detID > 0)
                        {
                            cObj._RecordState = RecordStateEnum.Deleted;
                            nList.Add(cObj);
                            //dRow["RState"] = "deleted";
                            //dtUpdate.ImportRow(dRow);
                        }
                        else
                        {
                            //now use less
                            delIndex.Add(cList.IndexOf(cObj));
                            //delIndex.Add(dtToValid.Rows.IndexOf(dRow));
                        }
                    }
                }

            }

            this.listDetails = nList;
        }

        private Boolean IsGridRowValid(dcLocationJournalType cObj)
        {
            bool isValid = false;

            if (cObj.JournalTypeID > 0)
            {
                isValid = true;
            }
            return isValid;
        }

    }
}