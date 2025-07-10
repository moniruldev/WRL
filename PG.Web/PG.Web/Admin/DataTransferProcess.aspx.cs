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

using PG.DBClass.SecurityDC;
using PG.BLLibrary.SecurityBL;
using PG.Core.DBBase;
using PG.Core.Utility;
using PG.BLLibrary.ProductionBL;
using PG.DBClass.ProductionDC;

namespace PG.Web.Admin
{
    public partial class DataTransferProcess : BagePage
    {

        List<dcDATATRANSFER_MASTER_DETAIL> lstDetails = new List<dcDATATRANSFER_MASTER_DETAIL>();

        protected override void OnPreInit(EventArgs e)
        {
            if (AdminGlobals.AdminMasterPage != string.Empty)
            {
                this.MasterPageFile = AdminGlobals.AdminMasterPage;
            }
            base.OnPreInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
           // SetHyperLink();
        }

      

        //private void SetHyperLink()
        //{

        //    //new button
        //    string hLink = "javascript:tbopen(0)";
        //    if (base.PageMode == PG.Core.Web.PageModeEnum.InTab)
        //    {
        //        hLink = "javascript:tbopen(0)";
        //        this.btnAddNew.Attributes.Add("onclick", hLink);
        //    }
        //    else
        //    {
        //        hLink = string.Format("javascript:window.open({0})", "/Admin/User.aspx");
        //        this.btnAddNew.Attributes.Add("onclick", hLink);

        //        //hLink = "~/Admin/User.aspx";
        //        //this.btnAddNew.PostBackUrl = hLink;
        //        //this.btnAddNew.OnClientClick = string.Empty;
        //    }
            
        //}


        private void LoadData()
        {

            //int roleKey = Convert.ToInt32(this.ddlRole.SelectedValue);
            List<dcDATATRANSFER_MASTER_DETAIL> listData = DATATRANSFER_TABLE_MAPBL.GetDataTransferTableList(0, null);
                //RoleBL.GetRoleList(AppInfo.AppID);
            BindGridData(listData);

            lblTotal.Text = "Total:" + GridView1.Rows.Count.ToString();
        }

        private void BindGridData(List<dcDATATRANSFER_MASTER_DETAIL> listData)
        {
            int rowCount = listData.Count;
            if (rowCount == 0)
            {
                listData.Add(new dcDATATRANSFER_MASTER_DETAIL());
            }

            GridView1.DataSource = listData;
            GridView1.DataBind();

            if (rowCount == 0)
            {
                GridView1.Rows[0].Visible = false;
            }
            hdnRowCount.Value = rowCount.ToString();

            
            GridView1.CssClass = "grid";
        }



        //protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    LoadData();
        //}

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string rowID = e.Row.ClientID;
                string js = string.Format("return ShowDetailsPopup('{0}');", rowID);

                LinkButton lnkDelete = (LinkButton)e.Row.FindControl("btnDeleteRow");
                string jsDelete = "return confirm('Are you sure to delete current row?');";
                lnkDelete.OnClientClick = jsDelete;


                dcDATATRANSFER_MASTER_DETAIL det = e.Row.DataItem as dcDATATRANSFER_MASTER_DETAIL;
                if (det._RecordState == RecordStateEnum.Deleted)
                {
                    e.Row.Visible = false;
                }
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
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

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int RowIndex = gvr.RowIndex;
                GridView1.Rows[RowIndex].Visible = false;
               // RefreshCuttingGrid();

            }
        }

        private void ReadDetailsFromGrid()
        {

            this.lstDetails.Clear();
            ///addition
            foreach (GridViewRow gvR in this.GridView1.Rows)
            {
                if (gvR.RowType == DataControlRowType.DataRow)
                {

                    dcDATATRANSFER_MASTER_DETAIL cObj = new dcDATATRANSFER_MASTER_DETAIL();
                    ReadGridRowToObject(gvR, this.GridView1.DataKeys, cObj);

                    if (cObj._RecordState != RecordStateEnum.Deleted)
                    {
                        if (cObj.MASTER_TABLE != String.Empty)
                        {
                            this.lstDetails.Add(cObj);
                        }
                    }
                    //else
                    //{
                    //    this.lstDetails.Add(cObj);
                    //}

                }
            }
        }

        private void ReadGridRowToObject(GridViewRow gvR, DataKeyArray dataKeys, dcDATATRANSFER_MASTER_DETAIL cObj)
        {
            string strD;

            
            strD = ((TextBox)gvR.FindControl("txtMasterTable")).Text;
            cObj.MASTER_TABLE = strD;

            strD = ((TextBox)gvR.FindControl("txtMasterTableFilter")).Text;
            cObj.MASTER_TABLE_FILTER = strD;

            strD = ((TextBox)gvR.FindControl("txtMasterToDetail")).Text;
            cObj.MASTER_TO_DETAIL = strD;

            strD = ((TextBox)gvR.FindControl("txtDetailTable")).Text;
            cObj.DETAIL_TABLE = strD;

            strD = ((TextBox)gvR.FindControl("txtDetailToMaster")).Text;
            cObj.DETAIL_TO_MASTER = strD;

            strD = ((TextBox)gvR.FindControl("txtIsActive")).Text;
            cObj.IS_ACTIVE = strD;


            cObj._RecordState = RecordStateEnum.Added;
            //if (cObj.PROD_DTL_ID > 0)
            //{
            //    cObj._RecordState = RecordStateEnum.Edited;
            //}

            if (!gvR.Visible)
            {
                cObj._RecordState = RecordStateEnum.Deleted;
            }
        }

        protected void btnProcess_Click(object sender, EventArgs e)
        {
            //ReadDetailsFromGrid();

            //foreach(dcDATATRANSFER_TABLE_MAP item in lstDetails)
            //{
            //    dcDATATRANSFER_TABLE_MAP cObj=new dcDATATRANSFER_TABLE_MAP();
            //    cObj.TABLENAME=item.TABLENAME;
            //    cObj.PERCENT = item.PERCENT;
            //    cObj.fromProdDate = txtFromDate.Text;
            //    cObj.toProdDate = txtToDate.Text;
            //    DATATRANSFER_TABLE_MAPBL.DataProcess(cObj);
            //}

            string LinkedServer = ConfigurationManager.AppSettings["DataTransferLinkedServer"];

            string fromDate = string.Empty;
            string toDate = string.Empty;

            if (!txtFromDate.Text.Equals(string.Empty))
                fromDate = Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy");
            if (!txtToDate.Text.Equals(string.Empty))
                toDate = Convert.ToDateTime(txtToDate.Text).ToString("dd-MMM-yyyy");

            string saveStatus = string.Empty;

            bool dataTransferStatus = DataTransferProcessBL.ProcessDataTransfer(LinkedServer, fromDate, toDate, out saveStatus, null);

            if (dataTransferStatus.Equals(true) && saveStatus.Equals(string.Empty))
                ScriptManager.RegisterClientScriptBlock(btnProcess, GetType(), "", "alert('Data transfer process done successfully.');", true);
            else
                ScriptManager.RegisterClientScriptBlock(btnProcess, GetType(), "", "alert('Data transfer failed due to " + saveStatus + ".!!');", true);


        }

        //protected void DataProcess(dcDATATRANSFER_TABLE_MAP pcObj)
        //{
        //    try { 
            
        //    string iobj = String.Empty;
        //    if (pcObj.TABLENAME != String.Empty)
        //    {
        //        switch (pcObj.TABLENAME)
        //        {
        //            case "PRODUCTION_MST" :
        //                List<dcPRODUCTION_MST> list= PRODUCTION_MSTBL.GetPRODUCTION_MST_MS(pcObj, null);

        //                  DBContextSettings dbcontext = DBContextManager.GetDBContextSettings("PBL_PSP");
        //                  DBContext dcc = DBContextManager.CreateAndInitDBContext(dbcontext);
                         
        //                  foreach (dcPRODUCTION_MST item in list)
        //                  {
        //                      PRODUCTION_MSTBL.Delete_MS(item.PROD_ID, dcc);
        //                  }

        //                    foreach(dcPRODUCTION_MST item in list)
        //                    {
        //                        PRODUCTION_MSTBL.Delete(item.PROD_ID,dcc);
        //                        item._RecordState = RecordStateEnum.Added;
        //                    }
        //                  PRODUCTION_MSTBL.SaveList(list, dcc);
        //                break;
        //            //case 2:
        //            //    Console.WriteLine("Case 2");
        //            //    break;
        //            //default:
        //            //    Console.WriteLine("Default case");
        //            //    break;
        //        }
        //        pcObj.IS_SUCCESS = "Y";
        //    }
        //    }
        //    catch
        //    {
        //        pcObj.IS_SUCCESS = "N";
        //    }
        //    finally { 
        //        DataProcessUpdate(pcObj);
        //    }
            
        //}

        //protected void DataProcessUpdate(dcDATATRANSFER_TABLE_MAP icObj)
        //{
        //    dcDATATRANSFER_TABLE_LOG pcObj = new dcDATATRANSFER_TABLE_LOG();
        //    pcObj.TABLENAME = icObj.TABLENAME;
        //    pcObj.TRANSFERDATETIME = Conversion.DBNullDateToNull(txtToDate.Text.Trim());
        //    pcObj.IS_SUCCESS = icObj.IS_SUCCESS;
        //    pcObj._RecordState = RecordStateEnum.Added;
        //    DATATRANSFER_TABLE_LOGBL.Save(pcObj);
        //}
    }
}
