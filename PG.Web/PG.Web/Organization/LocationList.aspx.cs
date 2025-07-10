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
using PG.BLLibrary.OrganizationBL;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;


namespace PG.Web.Organization
{
    public partial class LocationList : BagePage
    {
        int CompanyID = 0;
        OleDbConnection Econ;
        SqlConnection con;

        string constr, Query, sqlconn;  
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
                LoadData();
            }
            SetHyperLink();
            this.ShowPageMessage(this.lblMessage);
        }

        private void FillCombo()
        {

            
        }

        private void SetHyperLink()
        {

            //new button
            string hLink = "javascript:tbopen(0)";
            if (base.PageMode == PG.Core.Web.PageModeEnum.InTab)
            {
                hLink = "javascript:tbopen(0)";
                this.btnAddNew.Attributes.Add("onclick", hLink);
            }
            else
            {
                //hLink = "~/Accounting/AccYear.aspx";
                //hLink = ResolveUrl("~/Accounting/AccYear.aspx");
                
                hLink = "javascript:tbopen(0)";
                this.btnAddNew.Attributes.Add("onclick", hLink);
            }
        }
        private void LoadData()
        {
            GridView1.DataSource = LocationBL.GetLocationList(this.CompanyID);
            GridView1.DataBind();

            lblTotal.Text = "Total: " + GridView1.Rows.Count.ToString();
        }


        protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
        {
           // LoadData();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string strD = DataBinder.Eval(e.Row.DataItem, "LocationID").ToString(); ;
                HyperLink lnk = (HyperLink)e.Row.Cells[0].Controls[0];

                string hLink = "javascript:tbopen(" + strD + ")";
                if (base.PageMode == PG.Core.Web.PageModeEnum.InTab)
                {
                    hLink = "javascript:tbopen(" + strD + ")";
                }
                else
                {
                    hLink = "~/Organization/Location.aspx?id=" + strD;
                }
                //lnk.NavigateUrl = "~/Admin/UserAddEdit.aspx?UserID=" + strD;
                lnk.NavigateUrl = hLink;


              //  LinkButton lnkBtn = (LinkButton)e.Row.Cells[4].Controls[0];
               // lnkBtn.OnClientClick = "if(!confirm('Are you sure you want to delete this?')) return false;";
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


        //Excel Export

        private void ExcelConn(string FilePath)
        {

            constr = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES;""", FilePath);
            Econ = new OleDbConnection(constr);
           // string sSourceConstr = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES;""", sPath);

        }
        private void connection()
        {
            sqlconn = ConfigurationManager.ConnectionStrings["SqlCom"].ConnectionString;
            con = new SqlConnection(sqlconn);

        }


        private void InsertExcelRecords(string FilePath)
        {
            ExcelConn(FilePath);

            Query = string.Format("Select [Name],[City],[Address],[Designation] FROM [{0}]", "Sheet1$");
            OleDbCommand Ecom = new OleDbCommand(Query, Econ);
            Econ.Open();

            DataSet ds = new DataSet();
            OleDbDataAdapter oda = new OleDbDataAdapter(Query, Econ);
            Econ.Close();
            oda.Fill(ds);
            DataTable Exceldt = ds.Tables[0];
            connection();
            //creating object of SqlBulkCopy    
            SqlBulkCopy objbulk = new SqlBulkCopy(con);
            //assigning Destination table name    
            objbulk.DestinationTableName = "Employee";
            //Mapping Table column    
            objbulk.ColumnMappings.Add("Name", "Name");
            objbulk.ColumnMappings.Add("City", "City");
            objbulk.ColumnMappings.Add("Address", "Address");
            objbulk.ColumnMappings.Add("Designation", "Designation");
            //inserting Datatable Records to DataBase    
            con.Open();
            objbulk.WriteToServer(Exceldt);
            con.Close();

        }  

        protected void UPButton_Click(object sender, EventArgs e)
        {
            string CurrentFilePath = Path.GetFullPath(FileUpload1.PostedFile.FileName);
            InsertExcelRecords(CurrentFilePath); 
        }
    }
}
