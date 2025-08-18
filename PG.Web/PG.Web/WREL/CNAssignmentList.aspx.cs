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
using PG.DBClass.InventoryDC;
using PG.BLLibrary.InventoryBL;
using PG.Core.Utility;
using PG.DBClass.HMSDC;
using PG.BLLibrary.HMSBL;
using PG.DBClass.WRELDC;
using PG.BLLibrary.WRElBL;
using Oracle.ManagedDataAccess.Client;

namespace PG.Web.WREL
{
    public partial class CNAssignmentList : BagePage
    {
        int CompanyID = 0;
        public string ItemListServiceLink = PageLinks.InventoryLink.GetLink_ItemList;
        public string ItemGroupListServiceLink = PageLinks.InventoryLink.GetLink_ItemGroupList;

        public string DeliveryManlistServiceLink = PageLinks.InventoryLink.GetLink_DeliveryManList;
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

            if (!IsPostBack)
            {
                FillCombo();
                SetDate();
                LoadData();
                btnGridPageGoTo.Style.Add("display", "none");
            }
            SetHyperLink();
        }

        private void FillCombo()
        {
           
        }

        private void SetDate()
        {
            var now = DateTime.Now;
            var firstDate = new DateTime(now.Year, now.Month, 1);

        }

        private void SetHyperLink()
        {
            //new button
            string hLink = "javascript:tbopen(0)";
            if (base.PageMode == PG.Core.Web.PageModeEnum.InTab)
            {
                hLink = "javascript:tbopen(0)";
                this.btnNewAdd.Attributes.Add("onclick", hLink);
            }
            else
            {
                hLink = "javascript:tbopen(0)";
                this.btnNewAdd.Attributes.Add("onclick", hLink);
            }
        }
        private void LoadData()
        {
            

            //List<dcCN_ASSIGNMENT> listData = CN_ASSIGNMENTBL.GetCNAssignmentMstListDatabyDelManID(Conversion.StringToInt(hdnDeliveryManID.Value),null);
           
            //var bindList = listData.Select(x => new 
            //{
            //    x.CN_NUMBER,
            //    x.CN_ID,
            //    x.ASSIGN_DATE,
            //    x.DELIVERY_MAN_NAME,
            //    x.CONSIGNEE_NAME,
            //    x.CONSIGNEE_MOBILE_NO,
            //    POD = x.POD != null && x.POD.Length > 0
            //        ? "data:image/png;base64," + Convert.ToBase64String(x.POD)
            //        : null
            //}).ToList();

            //BindGridData(bindList);
            List<dcCN_ASSIGNMENT> listData = CN_ASSIGNMENTBL.GetCNAssignmentMstListDatabyDelManID(Conversion.StringToInt(hdnDeliveryManID.Value), null);

            var bindList = listData.Select(x => new
            {
                x.CN_ASSIGN_ID,
                x.CN_NUMBER,
                x.CN_ID,
                x.ASSIGN_DATE,
                x.DELIVERY_MAN_NAME,
                x.CONSIGNEE_NAME,
                x.CONSIGNEE_MOBILE_NO,
                x.OTP_CODE,
                POD = x.POD != null && x.POD.Length > 0
                    ? "data:image/png;base64," + Convert.ToBase64String(x.POD)
                    : null
            }).ToList();

            GridView1.DataSource = bindList;
            GridView1.DataBind();
            SetGridInfo(listData.Count);

        }


        private void BindGridData(List<dcCN_ASSIGNMENT> listData)
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
            //GridView1.CssClass = "grid";
        }


        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Image img = (Image)e.Row.FindControl("imgPhoto");
                if (img != null)
                {
                    string pod = DataBinder.Eval(e.Row.DataItem, "POD") as string;

                    if (string.IsNullOrEmpty(pod))
                    {
                        img.Visible = false; // hide when no image
                    }
                    else
                    {
                        img.ImageUrl = pod;  // show uploaded image
                        img.Visible = true;
                    }
                }
                //string strD = DataBinder.Eval(e.Row.DataItem, "CN_ASSIGN_ID").ToString(); ;
                //HyperLink lnk = (HyperLink)e.Row.Cells[0].Controls[0];

                //string hLink = "javascript:tbopen(" + strD + ")";
                //if (base.PageMode == PG.Core.Web.PageModeEnum.InTab)
                //{
                //    hLink = "javascript:tbopen(" + strD + ")";
                //}
                //else
                //{
                //    hLink = "~/WREL/CNAssignmentV2.aspx?id=" + strD;
                //}
                //lnk.NavigateUrl = hLink;
                Label lblSL = (Label)e.Row.FindControl("lblSL");
                if (lblSL != null)
                {
                    lblSL.Text = (e.Row.RowIndex + 1).ToString();
                }

               
            }

            if (e.Row.RowType == DataControlRowType.Pager)
            {
                e.Row.Visible = false;
            }
        }


        protected void btnLoadData_Click(object sender, EventArgs e)
        {
            GridView1.PageIndex = 0;
            LoadData();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            LoadData();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        public void SetGridInfo(int rowCount)
        {
            txtGridPageNo.Text = "0";
            lblGridPageInfo.Text = " of 0";
            if (GridView1.PageCount > 0)
            {
                txtGridPageNo.Text = (GridView1.PageIndex + 1).ToString();
                lblGridPageInfo.Text = "of " + GridView1.PageCount.ToString();
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
                lblTotal.Text = string.Format("Rows: {0} of {0}", rowCount);
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
                BindGridData(new List<dcCN_ASSIGNMENT>());
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
                BindGridData(new List<dcCN_ASSIGNMENT>());
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
                BindGridData(new List<dcCN_ASSIGNMENT>());
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
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "submit")
            {
                GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                LinkButton btnforward = (LinkButton)gvr.FindControl("lnkView");
               
                int CN_ID =Conversion.StringToInt( e.CommandArgument.ToString());

            }

            if (e.CommandName == "UploadImage")
            {
                int rowIndex = Convert.ToInt32(((GridViewRow)((Button)e.CommandSource).NamingContainer).RowIndex);
                GridViewRow row = GridView1.Rows[rowIndex];
                FileUpload fu = (FileUpload)row.FindControl("FileUpload1");

                if (fu.HasFile)
                {
                    byte[] fileData = fu.FileBytes;
                    string id = e.CommandArgument.ToString();

                    using (OracleConnection con = new OracleConnection("User Id=WRCUORIER;Password=WRCUORIER;Data Source=DESKTOP-0UVQ4LL/ORCL"))
                    {
                        con.Open();
                        OracleCommand cmd = new OracleCommand("UPDATE CN_CREATION_MST SET POD = :photo WHERE CN_ID = :id", con);
                        cmd.Parameters.Add(":photo", OracleDbType.Blob).Value = fileData;
                        cmd.Parameters.Add(":CN_ID", OracleDbType.Int32).Value = Convert.ToInt32(id);
                        cmd.ExecuteNonQuery();
                    }

                    LoadData();
                }
            }
        }



    }
}
