using PG.BLLibrary.AccountingBL;
using PG.BLLibrary.AccountingBL.GeneralLedgerBL;
using PG.Core;
using PG.Core.DBBase;
using PG.Core.Web;
using PG.DBClass.AccountingDC;
using PG.DBClass.AccountingDC.AccEnums;
using PG.DBClass.AccountingDC.GeneralLedgerDC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PG.Web.Accounting.GeneralLedger
{
    public partial class GLAccountRefCategory : BagePage
    {

        public string GLAccountServiceLink = PageLinks.AccountingLinks.GetLink_GLAccount;
        public string GLAccRefCategoryServiceLink = PageLinks.AccountingLinks.GetLink_AccRefCategory;

        int CompanyID = 0;
        int GLAccountID = 0;
        string ViewStateKey = "GLAccountID";
        string ViewStateKeyPrev = "GLAccountID_Prev";

        int GLAccountTypeID = 0;
        string saveMsg = string.Empty;
        List<dcGLAccountRefCategory> listDetails = new List<dcGLAccountRefCategory>();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.CompanyID = CompanyInfo.GetCompanyID();
            this.hdnCompanyID.Value = this.CompanyID.ToString();

            this.hdnBtnLoadDataID.Value = this.btnRefresh.UniqueID;

            if (!IsPostBack)  //first time
            {
                this.GLAccountID = base.GetPageQueryInteger("glaccid");
                this.hdnGLAccountID.Value = this.GLAccountID.ToString();

                ViewState[ViewStateKeyPrev] = "0";

                //dcAccYear year = AccYearBL.GetCurrentAccYear(this.CompanyID);
                //ddlAccYear.SelectedValue = year.AccYearID.ToString();

                //int accYearID = base.GetPageQueryInteger("accyearid");
                //if (accYearID > 0)
                //{
                //    ddlAccYear.SelectedValue = accYearID.ToString();
                //}



                if (this.GLAccountID == 0) //not query string
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
                this.GLAccountID = base.GetViewStateInt(this.ViewStateKey);

            }
            rblAccRefType.SelectedItem.Attributes.Add("class", "selectedradio");
            SetHyperLink();
            this.ShowPageMessage(this.lblMessage);
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
            lblHeader.Text = "Accounts Ref. Category Binding";
            this.EditMode = FormDataMode.Read;
            //this.hdnJournalID.Value = this.JournalID.ToString();
            //ReadData(this.JournalID);
            ReadDetails();
            ViewState[ViewStateKey] = this.GLAccountID.ToString();

            SetControl(FormDataMode.Read);

            //var c = new { Name = "Name", ID = 344 };
        }

        private void AddTask()
        {
            ViewState[ViewStateKeyPrev] = this.GLAccountID.ToString();
           
           
            //this.EditMode = FormDataMode.Add;
            this.IsDirty = false;
            ViewState[ViewStateKey] = "0";
            ////lblMode.Text = "Mode: Add";
            this.listDetails.Clear();
            //CheckAndAddGridBlankRow();
            BindDataToGrid(this.listDetails);

            lblHeader.Text = "Accounts Ref. Category Binding";
            //SetControl(FormDataMode.Add);

            //Helper.SetStatusMessage(lblMessage, string.Empty, MessageTypeEnum.None);
        }

        private void BindDataToGrid(List<dcGLAccountRefCategory> listData)
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
            GLAccountRefCategoryBL.UpdateSLNo(this.listDetails);


            BindDataToGrid(this.listDetails);

            SetControlGrid(this.EditMode);
        }

        private void AddBlankRowToGridList()
        {
            dcGLAccountRefCategory cObj = new dcGLAccountRefCategory();
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
            //int accYearID = Convert.ToInt32(ddlAccYear.SelectedValue);
            int glAccountID = Convert.ToInt32(hdnGLAccountID.Value);
            //int accrefCatID = Convert.ToInt32(hdnGLAccountID.Value);
            //int accRefTypeID = Convert.ToInt32(ddlAccRefType.SelectedValue);
            int accRefTypeID = Convert.ToInt32(rblAccRefType.SelectedValue);
            this.listDetails.Clear();


            ///addition
            foreach (GridViewRow gvR in this.GridView1.Rows)
            {
                if (gvR.RowType == DataControlRowType.DataRow)
                {
                    //DataRow dRow = this.dtGrid.NewRow();
                    dcGLAccountRefCategory cObj = new dcGLAccountRefCategory();
                    //cObj.CompanyID = this.CompanyID;
                    cObj.GLAccountID = glAccountID;
                    //cObj.AccYearID = accYearID;
                    cObj.AccRefTypeID = accRefTypeID;
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

            if (this.GLAccountTypeID == (int)GLAccountTypeEnum.ControlAccount)
            {
                isEnabled = false;
            }



            foreach (GridViewRow gvR in this.GridView1.Rows)
            {
                if (gvR.RowType == DataControlRowType.DataRow)
                {
                    //((DropDownList)gvR.FindControl("ddlLandOwner")).Enabled = isEnabled;

                    ((TextBox)gvR.FindControl("txtAccRefCatCode")).Enabled = isEnabled;
                    ((TextBox)gvR.FindControl("txtAccRefCategoryName")).Enabled = isEnabled;
                   

                    if (isEnabled)
                    {
                        ((HtmlInputButton)gvR.FindControl("btnAccRefCatCode")).Attributes.Remove("disabled");
                    }
                    else
                    {
                        ((HtmlInputButton)gvR.FindControl("btnAccRefCatCode")).Attributes.Add("disabled", "disabled");
                    }


                    ////HyperLink lnkEdit = (HyperLink)gvR.Cells[4].Controls[0];
                    //LinkButton lnkEdit = (LinkButton)gvR.FindControl("btnEditRow");
                    //lnkEdit.Enabled = isEnabled;
                    //lnkEdit.Visible = false;

                    //LinkButton lnkDelete = (LinkButton)gvR.Cells[5].Controls[0];
                    LinkButton lnkDelete = (LinkButton)gvR.FindControl("btnDeleteRow");
                    lnkDelete.Enabled = isEnabled;


                    //make grid readonly
                    ((TextBox)gvR.FindControl("txtAccRefCategoryName")).Attributes.Add("readonly", "readonly");
                    //((TextBox)gvR.FindControl("txtAccRefCategory")).Attributes.Add("readonly", "readonly");
                    //((TextBox)gvR.FindControl("txtRequisitionAmt")).Attributes.Add("readonly", "readonly");
                    //((TextBox)gvR.FindControl("txtApprovedAmt")).Attributes.Add("readonly", "readonly");

                }
            }

            btnNewRow.Enabled = isEnabled;
        }

        private void ReadGridRowToObject(GridViewRow gvR, DataKeyArray dataKeys, dcGLAccountRefCategory cObj)
        {
            //decimal d;
            string strD;

            //strD = this.GridView1.DataKeys[gvR.RowIndex]["_RecordState"].ToString();
            //strD = dataKeys[gvR.RowIndex]["_RecordState"].ToString();
            //decimal.TryParse(strD == string.Empty ? "0" : strD, out d);
            //cObj._RecordState = (RecordStateEnum)Convert.ToInt32(d);


            //cObj.CompanyID = this.CompanyID;

            strD = dataKeys[gvR.RowIndex]["GLAccountRefCategoryID"].ToString();
            cObj.GLAccountRefCategoryID = Convert.ToInt32(strD == string.Empty ? "0" : strD);

            //strD = dataKeys[gvR.RowIndex]["GLAccHistRefID"].ToString();
            //cObj.GLAccHistRefID = Convert.ToInt32(strD == string.Empty ? "0" : strD);

            strD = ((HiddenField)gvR.FindControl("hdnAccRefCatID")).Value;
            cObj.AccRefCategoryID = Convert.ToInt32(strD == string.Empty ? "0" : strD);

            strD = ((TextBox)gvR.FindControl("txtAccRefCatCode")).Text;
            cObj.AccRefCategoryCode = strD.Trim();


            strD = ((TextBox)gvR.FindControl("txtAccRefCategoryName")).Text;
            cObj.AccRefCategoryName = strD.Trim();

            cObj.IsMandatory = ((CheckBox)gvR.FindControl("chkISMANDATORY")).Checked;

            cObj.IsDefault = ((CheckBox)gvR.FindControl("chkDefault")).Checked;



            if (!gvR.Visible)
            {
                cObj._RecordState = RecordStateEnum.Deleted;
            }

        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
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
            int glAccID = Convert.ToInt32(hdnGLAccountID.Value);
            if (glAccID > 0)
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
            lblHeader.Text = "Accounts Ref. Category Binding";
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

           // ddlAccYear.Enabled = isEnabled;
            rblAccRefType.Enabled = isEnabled;

            txtGLAccount.Enabled = isEnabled;


            txtGLAccountName.Attributes.Add("readonly", "readonly");



            if (this.GLAccountTypeID == (int)GLAccountTypeEnum.ControlAccount)
            {
                //btnNewRow.Enabled = false;
                btnSave.Enabled = false;
            }
            else
            {
                //btnNewRow.Enabled = dataMode == FormDataMode.Edit;
                btnSave.Enabled = dataMode == FormDataMode.Edit;
            }

            //btnSave.Visible = isEnabled;
            //btnCancel.Visible = isEnabled;



            SetControlGrid(dataMode);

        }

        private void ReadData()
        {
            //int accYearID = Convert.ToInt32(ddlAccYear.SelectedValue);
            int glAccountID = Convert.ToInt32(hdnGLAccountID.Value);
            //int accRefTypeID = Convert.ToInt32(ddlAccRefType.SelectedValue);
            int accRefTypeID = Convert.ToInt32(rblAccRefType.SelectedValue);

            this.GLAccountID = glAccountID;

            dcGLAccount glAcc = GLAccountBL.GetGLAccountByID(this.CompanyID, glAccountID);

            if (glAcc != null)
            {
                txtGLAccount.Text = glAcc.GLAccountCode;
                txtGLAccountName.Text = glAcc.GLAccountName;
                this.GLAccountTypeID = glAcc.GLAccountTypeID;
            }
            else
            {
                txtGLAccount.Text = "";
                txtGLAccountName.Text = "";
                hdnGLAccountID.Value = "0";

            }

            //dcGLAccountHistory glOpen = null;

            //if (glAcc.GLAccountTypeID == (int)GLAccountTypeEnum.ControlAccount)
            //{
            //    glOpen = GLAccountHistoryBL.GetGLAccountHistoryByID_Control(this.CompanyID, accYearID, glAccountID);
            //}
            //else
            //{
            //    glOpen = GLAccountHistoryBL.GetGLAccountHistoryByID(this.CompanyID, accYearID, glAccountID);
            //}

          



        }

        private void ReadDetails()
        {
            //int accYearID = Convert.ToInt32(ddlAccYear.SelectedValue);
            int glAccountID = Convert.ToInt32(hdnGLAccountID.Value);
            //int accRefTypeID = Convert.ToInt32(ddlAccRefType.SelectedValue);
            int accRefTypeID = Convert.ToInt32(rblAccRefType.SelectedValue);

            List<dcGLAccountRefCategory> listData = new List<dcGLAccountRefCategory>();
            //List<dcAccRefCategory> listData = new List<dcAccRefCategory>();

            //if (GLAccountTypeID == (int)GLAccountTypeEnum.ControlAccount)
            //{

            //    listData = GLAccountHistoryRefBL.GetGLAccountHistoryRefList_Control(this.CompanyID, accYearID, glAccountID, 0, 0, 0);
            //    //listData = GLAccountRefCategoryBL.GetAccountRefCategoryList()
            //}
            //else
            //{
               
                //listData = GLAccountHistoryRefBL.GetGLAccountHistoryRefList(this.CompanyID, accYearID, glAccountID, 0);
            listData = GLAccountRefCategoryBL.GetAccountRefCategoryList(glAccountID, accRefTypeID, 0);
            //}


            int costCount = listData.Where(c => c.AccRefTypeID == (int)AccRefTypeEnum.CostCenter).Count();
            int refCount = listData.Where(c => c.AccRefTypeID == (int)AccRefTypeEnum.Reference).Count();
            //int trCdCount = listData.Where(c => c.AccRefTypeID == (int)AccRefTypeEnum.TranCode).Count();


            rblAccRefType.Items[0].Text = string.Format("Cost Center ({0})", costCount);
            rblAccRefType.Items[1].Text = string.Format("Reference ({0})", refCount);
            //rblAccRefType.Items[2].Text = string.Format("Tran. Code ({0})", trCdCount);




            //listData = listData.Where(c => c.AccRefTypeID == accRefTypeID).ToList();

            //GLAccountHistoryRefBL.UpdateSLNo(listData);

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

        protected void rblAccRefType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataTask();
            SetAccRefTypeInfo();
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

        public void SetAccRefTypeInfo()
        {
            AccRefTypeEnum accRefType = (AccRefTypeEnum)Convert.ToInt32(rblAccRefType.SelectedValue);

            switch (accRefType)
            {
                case AccRefTypeEnum.CostCenter:
                    lblAccRefType.Text = "Cost Center Category";
                    break;

                case AccRefTypeEnum.Reference:
                    lblAccRefType.Text = "Reference Category ";
                    break;

                case AccRefTypeEnum.TranCode:
                    lblAccRefType.Text = "Tran. Code";
                    break;
            }
            rblAccRefType.SelectedItem.Attributes.Add("class", "selectedradio");
        }

        private bool CheckData()
        {

            if (txtGLAccount.Text.Trim() == string.Empty)
            {
                //Helper.SetStatusMessage(lblMessage, "Please Enter Defination Name", MessageTypeEnum.InvalidData);
                this.SetPageMessage("Please Enter Account", MessageTypeEnum.InvalidData);
                txtGLAccount.Focus();
                return false;
            }


            if (Convert.ToInt32(hdnGLAccountID.Value) == 0)
            {
                //Helper.SetStatusMessage(lblMessage, "Please Enter Defination Name", MessageTypeEnum.InvalidData);
                this.SetPageMessage("Please Enter Account", MessageTypeEnum.InvalidData);
                txtGLAccount.Focus();
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
            GLAccountRefCategoryBL.UpdateSLNo(this.listDetails);
            bool bStatus = false;

            ////int accYearID = Convert.ToInt32(ddlAccYear.SelectedValue);
            int glAccountID = Convert.ToInt32(hdnGLAccountID.Value);
            ////int accRefTypeID = Convert.ToInt32(ddlAccRefType.SelectedValue);
            int accRefTypeID = Convert.ToInt32(rblAccRefType.SelectedValue);

            bStatus = GLAccountRefCategoryBL.UpdateGLAccRefCategoryList(glAccountID, accRefTypeID, this.listDetails);
            //bStatus = GLAccountRefCategoryBL.Insert();
            ////saveMsg = "GL Account Opening Amount Updated Successfully.";
            saveMsg = "Data Saved Successfully.";



            return bStatus;
        }

        private void ValidateGridList(List<dcGLAccountRefCategory> cList)
        {
            List<int> delIndex = new List<int>();
            List<dcGLAccountRefCategory> nList = new List<dcGLAccountRefCategory>();
            foreach (dcGLAccountRefCategory cObj in cList)
            {
                int detID = cObj.GLAccountRefCategoryID;
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

        private Boolean IsGridRowValid(dcGLAccountRefCategory cObj)
        {
            bool isValid = false;

            if (cObj.AccRefCategoryID > 0)
            {
                isValid = true;
            }
            return isValid;
        }

    }
}