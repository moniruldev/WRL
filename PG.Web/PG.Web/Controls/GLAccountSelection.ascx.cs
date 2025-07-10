using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using PG.Core.Extentions;

using PG.Core;
using PG.Core.DBBase;
using PG.Core.DBFilters;

using PG.DBClass.AccountingDC.AccEnums;
using PG.DBClass.AccountingDC.GeneralLedgerDC;
using PG.BLLibrary.AccountingBL;
using PG.BLLibrary.AccountingBL.GeneralLedgerBL;

namespace PG.Web.Controls
{

    public partial class GLAccountSelection : System.Web.UI.UserControl
    {
        public int CompanyID = 0;
        public delegate void UserControlOKClicked(int id);

       // public event EventHandler UserControlOKClicked UserControlOKClicked;

        //public event UserControlOKClicked UserControlOK_Clicked;


        //private void OnUserControlOKClicked(EmpSelectionEventArgs eArgs)
        //{
        //    if (UserControlOKClicked != null)
        //    {
        //        UserControlOKClicked(this, eArgs);
        //    }
        //}


        protected string GetSearchPostBack()
        {
            return Page.ClientScript.GetPostBackEventReference(btnSearch, string.Empty);
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            this.CompanyID = CompanyInfo.GetCompanyID();

            if (!IsPostBack)
            {
                FillCombo();
                //LoadData();

                List<dcGLGroup> cList = GLGroupBL.GetGLGroupList(this.CompanyID);
                //litGLGroup.Text = BLLibrary.AccountingBL.AccGLGroupBL.GetAccGLGroupULTreeText();

                this.GroupTree1.SetGroupTreeText<dcGLGroup>(cList, "GLGroupID", "GLGroupName", "GLGroupIDParent", false);
                btnGridPageGoTo.Style.Add("display", "none");
                

            }
            //BindGridData(new List<dcGLAccount>());
            this.txtGLGroup.Attributes.Add("readonly", "readonly");
            this.hdnBtnSearchID.Value = btnSearch.UniqueID;
            this.hdnBtnPageGoToID.Value = btnGridPageGoTo.UniqueID;
        }

        private void FillCombo()
        {
            this.ddlSearch.Items.Clear();
            this.ddlSearch.Items.Add(new ListItem("By Code", "1"));
            this.ddlSearch.Items.Add(new ListItem("By Name", "2"));


            this.ddlAccType.Items.Add(new ListItem("(all)", "0"));
            this.ddlAccType.AppendDataBoundItems = true;
            this.ddlAccType.DataSource = GLAccountTypeBL.GetGLAccountTypeList();
            this.ddlAccType.DataTextField = "GLAccountTypeName";
            this.ddlAccType.DataValueField = "GLAccountTypeID";
            this.ddlAccType.DataBind();

            //this.ddlDepartment.AppendDataBoundItems = true;
            //this.ddlDepartment.DataSource = BLLibrary.HumanResourceBL.DepartmentBL.GetDepartemntList(this.DbContext);
            //this.ddlDepartment.DataTextField = "DeptName";
            //this.ddlDepartment.DataValueField = "DeptID";
            //this.ddlDepartment.DataBind();

            //this.ddlDesignation.AppendDataBoundItems = true;
            //this.ddlDesignation.DataSource = BLLibrary.HumanResourceBL.DesignationBL.GetDesignationList(this.DbContext);
            //this.ddlDesignation.DataTextField = "DesigName";
            //this.ddlDesignation.DataValueField = "DesigID";
            //this.ddlDesignation.DataBind();


            //this.ddlGLGroup.AppendDataBoundItems = true;
            //this.ddlStatus.DataSource = BLLibrary.SysOptionBL.GetSystOptionList((int)DBClass.SysOptionTypeEnum.EmployeeStatus);
            //this.ddlStatus.DataTextField = "SysOptionName";
            //this.ddlStatus.DataValueField = "SysOptionID";
            //this.ddlStatus.DataBind();

            //this.ddlType.AppendDataBoundItems = true;
            //this.ddlType.DataSource = BLLibrary.SysOptionBL.GetSystOptionList((int)DBClass.SysOptionTypeEnum.EmployeeType, this.DbContext);
            //this.ddlType.DataTextField = "SysOptionName";
            //this.ddlType.DataValueField = "SysOptionID";
            //this.ddlType.DataBind();

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GridView1.PageIndex = 0;
            LoadData();

            //EmpSelectionEventArgs x = new EmpSelectionEventArgs();
            //x.id = 3455;
            //x.name = "EmpSe";
            //UserControlOK_Clicked(344344);
            //OnUserControlOKClicked(x);
        }

        private List<dcGLAccount> GetData()
        {

            //int locationID = 0; // Convert.ToInt32(this.ddlLocation.SelectedValue);
            //int deptID = 0; // Convert.ToInt32(this.ddlDepartment.SelectedValue);
            //int desigID = 0; // Convert.ToInt32(this.ddlDesignation.SelectedValue);


            int glGroupID =  PG.Core.Utility.Conversion.StringToInt(this.hdnGLGroup.Value);

            int accTypeID = Convert.ToInt32(ddlAccType.SelectedValue);

            int searchID = Convert.ToInt32(this.ddlSearch.SelectedValue);
            string searchText = txtSearch.Text.Trim();


            List<int> accClassList = PG.Core.Utility.Conversion.StringToIntList(hdnAccClass.Value);


            //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            StringBuilder sb = new StringBuilder();


            sb.Append(GLAccountBL.GetGLAccountListString());

            DBQuery dbq = new DBQuery();
            List<DBFilter> filterList = new List<DBFilter>();



            if (glGroupID > 0)
            {
                //sb.Append(" AND tblGLAccount.GLGroupID=@glGroupID ");
                //cmd.Parameters.AddWithValue("@glGroupID", glGroupID);

                filterList.Add(new DBFilter("tblGLAccount.GLGroupID", glGroupID));
            }


            if (accTypeID > 0)
            {
                //sb.Append(" AND tblGLAccount.GLAccountTypeID=@accTypeID ");
                //cmd.Parameters.AddWithValue("@accTypeID", accTypeID);

                filterList.Add(new DBFilter("tblGLAccount.GLAccountTypeID", accTypeID));
            }


            //if (showType > 0)
            //{
            //    if (showType == 1)
            //    {
            //        sb.Append(" AND (tblEmployee.EMPID NOT IN (Select EmpID FROM tblEmpBlockList)) ");
            //    }
            //    else if (showType == 2)
            //    {
            //        sb.Append(" AND (tblEmployee.EMPID IN (Select EmpID FROM tblEmpBlockList)) ");
            //    }
            //}


            if (searchText != string.Empty)
            {
                if (searchID == 1)
                {
                    //sb.Append(" AND tblGLAccount.GLAccountCode Like @glAccCode ");
                    //cmd.Parameters.AddWithValue("@glAccCode", searchText + '%');

                    filterList.Add(new DBFilter("tblGLAccount.GLAccountCode", searchText, DBFilterDataTypeEnum.String, DBFilterCompareTypeEnum.StartsWith));
                }
                else
                {
                    //sb.Append(" AND tblGLAccount.GLAccountName Like @glAccName ");
                    //cmd.Parameters.AddWithValue("@glAccName", '%' + searchText + '%');

                    filterList.Add(new DBFilter("tblGLAccount.GLAccountName", searchText, DBFilterDataTypeEnum.String, DBFilterCompareTypeEnum.Contains));

                }
            }

            if (accClassList.Count > 0)
            {
                filterList.Add(new DBFilter("tblGLAccount.GLAccountClassID", accClassList, DBFilterDataTypeEnum.Integer, DBFilterCompareTypeEnum.IN));
            }



            dbq.OrderBy = "tblGLAccount.GLAccountCode,tblGLAccount.GLAccountName";
            //sb.Append(" ORDER BY tblGLAccount.GLAccountName ");

            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = sb.ToString();

            dbq.DBQueryMode = DBQueryModeEnum.SQLStatement;
            dbq.SQLStatement = sb.ToString();
            //dbq.DBCommand = cmd;
            dbq.DBFilterList = filterList;

            List<dcGLAccount> listData = GLAccountBL.GetGLAccountList( dbq, null);
            int totRec = listData.Count;
            //GridView1.DataSource = listData;
            //GridView1.DataBind();


            return listData;
            //BindGridData(listData);

            //lblTotal.Text = "Total:" + totRec.ToString();
        }

        private void LoadData()
        {
            List<dcGLAccount> listData = GetData();
            BindGridData(listData);
            SetGridInfo(listData.Count);
        }

        private void BindGridData(List<dcGLAccount> listData)
        {
            int pageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);
            if (pageSize == 0)
            {
                GridView1.AllowPaging = false;
                GridView1.PageIndex = 0;
            }
            else
            {
                GridView1.AllowPaging = true;
                GridView1.PageSize = pageSize;
            }

            int rowCount = listData.Count;
            GridView1.DataSource = listData;
            GridView1.DataBind();
            GridView1.CssClass = "grid";

        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            LoadData();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    e.Row.CssClass += " gridRow";
            //}

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

            if (e.Row.RowType == DataControlRowType.Pager)
            {
                e.Row.Visible = false;
            }

        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            //int x = 0;

            List<dcGLAccount> listData = GetData().OrderBy(c => c.GLAccountName).ToList();
            BindGridData(listData);
        }

        public void SetGridInfo(int rowCount)
        {
            txtGridPageNo.Text = "0";
            lblGridPageInfo.Text = " of 0";
            if (GridView1.PageCount > 0)
            {
                txtGridPageNo.Text = (GridView1.PageIndex + 1).ToString();
                lblGridPageInfo.Text = " of " + GridView1.PageCount.ToString();
            }

            hdnRowCount.Value = rowCount.ToString();

            int startRow = 0;
            int endRow = 0;

            int pageSize = GridView1.AllowPaging ? GridView1.PageSize : rowCount;

            if (rowCount > 0)
            {
                startRow = (GridView1.PageIndex * pageSize);
                endRow = startRow + pageSize;
                endRow = endRow > rowCount ? rowCount : endRow;

                startRow = startRow + 1;
            }


            if (rowCount > 1)
            {
                lblTotal.Text = string.Format("Rows: {0}-{1} of {2}", startRow, endRow, rowCount);
            }
            else
            {
                lblTotal.Text = string.Format("Rows: {0} of {0}",rowCount);
            }


        }

        public void GoToPageNext()
        {
            if (GridView1.PageCount > 0)
            {
                int totPage = GridView1.PageCount;
                int curPage = GridView1.PageIndex + 1;

                int gotoPage = curPage + 1;
                gotoPage = gotoPage > totPage ? totPage : gotoPage;
                GridView1.PageIndex = gotoPage - 1;
                LoadData();
            }
            else
            {
                BindGridData(new List<dcGLAccount>());
                SetGridInfo(0);
            }
        }

        public void GoTotPagePrevious()
        {
            if (GridView1.PageCount > 0)
            {
                int totPage = GridView1.PageCount;
                int curPage = GridView1.PageIndex + 1;

                int gotoPage = curPage - 1;
                gotoPage = gotoPage < 1 ? 1 : gotoPage;
                GridView1.PageIndex = gotoPage - 1;
                LoadData();
            }
            else
            {
                BindGridData(new List<dcGLAccount>());
                SetGridInfo(0);
            }
        }

        public void GoToPageFirst()
        {
            GoTotPageNo(1);
        }

        public void GoTotPageLast()
        {
            GoTotPageNo(GridView1.PageCount);
        }

        public void GoTotPageNo(int pageNo)
        {
            if (GridView1.PageCount > 0)
            {
                pageNo = pageNo > GridView1.PageCount ? GridView1.PageCount : pageNo;
                pageNo = pageNo < 1 ? 1 : pageNo;
                GridView1.PageIndex = pageNo - 1;
                LoadData();
            }
            else
            {
                BindGridData(new List<dcGLAccount>());
                SetGridInfo(0);
            }
        }


        protected void btnGridPagePrev_Click(object sender, EventArgs e)
        {
            GoTotPagePrevious();
        }

        protected void btnGridPageNext_Click(object sender, EventArgs e)
        {
            GoToPageNext();
        }

        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void btnGridPageLast_Click(object sender, EventArgs e)
        {
            GoTotPageLast();
        }

        protected void btnGridPageFirst_Click(object sender, EventArgs e)
        {
            GoToPageFirst();
        }

        protected void btnGridPageGoTo_Click(object sender, EventArgs e)
        {
            GoTotPageNo(PG.Core.Utility.Conversion.StringToInt(txtGridPageNo.Text));
            txtGridPageNo.Focus();
        }

    }
}