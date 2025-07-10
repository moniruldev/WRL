using System;
using System.Collections;
using System.Collections.Generic;
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
using PG.Core;
using PG.Core.Web;
using PG.DBClass.AccountingDC;
using PG.DBClass.AccountingDC.AccEnums;
using PG.DBClass.AccountingDC.GeneralLedgerDC;
using PG.BLLibrary.AccountingBL;
using PG.BLLibrary.AccountingBL.GeneralLedgerBL;


namespace PG.Web.Accounting.GeneralLedger
{
    public partial class GLAccountOpen : BagePage
    {
        int CompanyID = 0;
        public string GLAccountServiceLink = PageLinks.AccountingLinks.GetLink_GLAccount;
        string saveMsg = string.Empty;
        List<dcGLAccountHistory> listDetails = new List<dcGLAccountHistory>();


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

            if (!IsPostBack)
            {
                FillCombo();

                dcAccYear year = AccYearBL.GetCurrentAccYear(this.CompanyID);
                ddlAccYear.SelectedValue = year.AccYearID.ToString();

                LoadData();
            }

            txtDebitAmtDiff.Attributes.Add("readonly", "readonly");
            txtCreditAmtDiff.Attributes.Add("readonly", "readonly");

            txtDebitAmt.Attributes.Add("readonly", "readonly");
            txtCreditAmt.Attributes.Add("readonly", "readonly");


            SetHyperLink();
        }

        private void FillCombo()
        {
            ddlAccYear.DataTextField = "AccYearName";
            ddlAccYear.DataValueField = "AccYearID";
            ddlAccYear.AppendDataBoundItems = true;
            ddlAccYear.DataSource = BLLibrary.AccountingBL.AccYearBL.GetAccYearList(this.CompanyID, this.DbContext);
            ddlAccYear.DataBind();
            
        }

        private void SetHyperLink()
        {

            //new button
            string hLink = "javascript:tbopenAccRef(0)";
            if (base.PageMode == PG.Core.Web.PageModeEnum.InTab)
            {
                hLink = "javascript:tbopenAccRef(0)";
                this.btnAccRef.PostBackUrl = string.Empty;
                this.btnAccRef.OnClientClick = hLink;
            }
            else
            {
                hLink = "~/Accounting/GeneralLedger/GLAccountOpenAccRef.aspx";
                this.btnAccRef.PostBackUrl = hLink;
                this.btnAccRef.OnClientClick = string.Empty;
            }
        }
        private void LoadData()
        {
            int accYearID = Convert.ToInt32(ddlAccYear.SelectedValue);


            List<dcGLAccountHistory> listData = GLAccountHistoryBL.GetGLAccountHistoryList_NormalControlWithAcc(this.CompanyID, accYearID, null, null);
            //List<dcGLAccountHistory> listData = GLAccountHistoryBL.GetGLAccountHistoryList(this.CompanyID, accYearID);
            BindGridData(listData);
            SumData(listData);

            lblTotal.Text = "Total: " + GridView1.Rows.Count.ToString();
        }

        private void BindGridData(List<dcGLAccountHistory> listData)
        {
            int rowCount = listData.Count;
            if (rowCount == 0)
            {
                listData.Add(new dcGLAccountHistory());
            }

            GridView1.DataSource = listData;
            GridView1.DataBind();

            if (rowCount == 0)
            {
                GridView1.Rows[0].Visible = false;
            }
            hdnRowCount.Value = rowCount.ToString();

            //using (System.IO.StreamWriter sw = new System.IO.StreamWriter("c:\\test.xls"))
            //{
            //    using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            //    {
            //        GridView1.RenderControl(hw);
            //    }
            //}
            GridView1.CssClass = "grid";
        }

        private void SumData(List<dcGLAccountHistory> listData)
        {
            txtDebitAmtDiff.Text = "0.00";
            txtCreditAmtDiff.Text = "0.00";
            txtDebitAmt.Text = "0.00";
            txtCreditAmt.Text = "0.00";

            if (listData != null)
            {
                decimal totDebit = listData.Sum(c => c.DebitAmtOpen);
                decimal totCredit = listData.Sum(c => c.CreditAmtOpen);

                if (totDebit > totCredit)
                {
                    txtCreditAmtDiff.Text = (totDebit - totCredit).ToString("#0.00");
                }

                if (totCredit > totDebit)
                {
                    txtDebitAmtDiff.Text = (totCredit - totDebit).ToString("#0.00");
                }

                txtDebitAmt.Text = totDebit.ToString("#0.00");
                txtCreditAmt.Text = totCredit.ToString("#0.00");
            }
        }


        protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
        {
           // LoadData();
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

                dcGLAccountHistory accHis = e.Row.DataItem as dcGLAccountHistory;

                TextBox txtDebit = (TextBox)e.Row.FindControl("txtDebitAmt");
                TextBox txtCredit = (TextBox)e.Row.FindControl("txtCreditAmt");

                txtDebit.Enabled = accHis.GLAccountTypeID != (int)GLAccountTypeEnum.ControlAccount;
                txtCredit.Enabled = accHis.GLAccountTypeID != (int)GLAccountTypeEnum.ControlAccount;



                HyperLink lnk = (HyperLink)e.Row.Cells[6].Controls[0];
                string strD = accHis.GLAccountID.ToString() ;
                string hLink = "javascript:tbopenAccRef(" + strD + ")";
                if (base.PageMode == PG.Core.Web.PageModeEnum.InTab)
                {
                    hLink = "javascript:tbopenAccRef(" + strD + ")";
                }
                else
                {
                    hLink = "~/Accounts/GeneralLedger/GLAccountOpenAccRef.aspx?glaccid=" + strD;
                }
                lnk.NavigateUrl = hLink;
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            GridView1.PageIndex = 0;
            LoadData();
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

        private void ReadDetailsFromGrid()
        {
            this.listDetails.Clear();


            ///addition
            foreach (GridViewRow gvR in this.GridView1.Rows)
            {
                if (gvR.RowType == DataControlRowType.DataRow)
                {
                    //DataRow dRow = this.dtGrid.NewRow();
                    dcGLAccountHistory cObj = new dcGLAccountHistory();
                  //  cObj.SalaryHeadType_sopt = (int)SalaryHeadTypeEnum.Addition;
                    ReadGridRowToObject(gvR, this.GridView1.DataKeys, cObj);
                    this.listDetails.Add(cObj);
                }
            }
        }
        private void ReadGridRowToObject(GridViewRow gvR, DataKeyArray dataKeys, dcGLAccountHistory cObj)
        {
            decimal d;
            string strD;

            //strD = this.GridView1.DataKeys[gvR.RowIndex]["_RecordState"].ToString();
            //strD = dataKeys[gvR.RowIndex]["_RecordState"].ToString();
            //decimal.TryParse(strD == string.Empty ? "0" : strD, out d);
            //cObj._RecordState = (RecordStateEnum)Convert.ToInt32(d);


            cObj.CompanyID = this.CompanyID;

            strD = dataKeys[gvR.RowIndex]["GLAccountID"].ToString();
            cObj.GLAccountID = Convert.ToInt32(strD == string.Empty ? "0" : strD);

            strD = dataKeys[gvR.RowIndex]["AccYearID"].ToString();
            cObj.AccYearID = Convert.ToInt32(strD == string.Empty ? "0" : strD);

            strD = dataKeys[gvR.RowIndex]["GLAccountTypeID"].ToString();
            cObj.GLAccountTypeID = Convert.ToInt32(strD == string.Empty ? "0" : strD);


            strD = ((TextBox)gvR.FindControl("txtDebitAmt")).Text;
            decimal.TryParse(strD == string.Empty ? "0" : strD, out d);
            cObj.DebitAmtOpen = Convert.ToInt32(d);


            strD = ((TextBox)gvR.FindControl("txtCreditAmt")).Text;
            decimal.TryParse(strD == string.Empty ? "0" : strD, out d);
            cObj.CreditAmtOpen = Convert.ToInt32(d);

            cObj.DebitAmtOpen = cObj.DebitAmtOpen <= 0 ? 0 : cObj.DebitAmtOpen;
            cObj.CreditAmtOpen = cObj.CreditAmtOpen <= 0 ? 0 : cObj.CreditAmtOpen;


            cObj.CreditAmtOpen = cObj.DebitAmtOpen > 0 ? 0 : cObj.CreditAmtOpen;

        }

        private bool CheckData()
        {

            //if (txtName.Text.Trim() == string.Empty)
            //{
            //    //Helper.SetStatusMessage(lblMessage, "Please Enter Defination Name", MessageTypeEnum.InvalidData);
            //    this.SetPageMessage("Please Enter Defination Name", MessageTypeEnum.InvalidData);
            //    txtName.Focus();
            //    return false;
            //}






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

            if (SaveData())
            {
                //Helper.SetStatusMessage(lblMessage, saveMsg, MessageTypeEnum.Successful);
                this.SetPageMessage(saveMsg, MessageTypeEnum.Successful);
                LoadData();
                //EditTask();
            }
            else
            {
                //Helper.SetStatusMessage(lblMessage, saveMsg, MessageTypeEnum.Error);
                this.SetPageMessage(saveMsg, MessageTypeEnum.Error);

            }
            base.ShowPageMessage(lblMessage, true);
        }

        private bool SaveData()
        {

            //get the details data
            ReadDetailsFromGrid();
            //ValidateGridList(this.listDetails);
            bool bStatus = false;

            int accYearID =  Convert.ToInt32(ddlAccYear.SelectedValue);

            bStatus =  GLAccountHistoryBL.UpdateGLHistory(this.CompanyID, accYearID, this.listDetails);
            saveMsg = "GL Account Opening Amount Updated Successfully.";


            return bStatus;
        }

        protected void ddlAccYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridView1.PageIndex = 0;
            LoadData();
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
    }
}
