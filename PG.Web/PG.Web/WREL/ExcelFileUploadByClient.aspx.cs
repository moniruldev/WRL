using PG.BLLibrary.InventoryBL;
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
using PG.BLLibrary.OrganizationBL;
using PG.Report.ReportRBL.InventoryRBL;
using PG.Report.ReportEnums;
using PG.Report;
using PG.Report.ReportGen.InventoryRGN;
using PG.DBClass.HMSDC;
using PG.BLLibrary.HMSBL;
using PG.DBClass.WRELDC;
using PG.BLLibrary.WRElBL;
using System.Collections;
using System.IO;
using System.Data.OleDb;
using System.Data;
using OfficeOpenXml;
using Oracle.ManagedDataAccess.Client;
//using ClosedXML.Excel;
//using NPOI.SS.UserModel;
//using NPOI.XSSF.UserModel;
//using NPOI.HSSF.UserModel;
//using ClosedXML.Excel;
//using ClosedXML.Excel;

namespace PG.Web.WREL
{
    public partial class ExcelFileUploadByClient : BagePage
    {
        //this 
        string ViewStateKey = "CN_REF_DTL_ID";
        string ViewStateKeyPrev = "CN_REF_DTL_ID_PREV";
        ReportOpenTypeEnum ReportOpenType = ReportOpenTypeEnum.Preview;
        // int CompanyID = 0;

        int CN_ID_REF = 0;
        private int totalRowCount = 0;
        string saveMsg = string.Empty;
        string errMsg = string.Empty;

        private dcUser loggedinUser = null;
        public string ReportViewPageLink = PageLinks.ReportLinks.GetLink_ReportView;
        public string ReportViewPDFPageLink = PageLinks.ReportLinks.GetLink_ReportViewPDF;
        public string ReportPrintPageLink = PageLinks.ReportLinks.GetLink_ReportPrint;
        public string ReportPDFPageLink = PageLinks.ReportLinks.GetLink_ReportPDF;




      
       
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

            loggedinUser = AppSecurity.GetUserInfoFromSession();
            //base.AppObjectID = BLLibrary.SystemBL.AppObjectEnum.Frm1001_OptionInfo;
            //base.RestrictByPageInTab();

            //ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(this.LinkButton1);

            this.CN_ID_REF = base.GetPageQueryInteger("id");

            if (!IsPostBack) //first Time
            {


                hdnLoggedInUser.Value = loggedinUser.UserID.ToString();
                
                //FillCombo();





                if (this.CN_ID_REF == 0) //not query string
                {
                    //List<dcCARGO_CREATION_DETAIL> roomList = HMRESERVATION_DTLBL.GetRoomInfoList();
                    //GridView1.DataSource = roomList;
                    //GridView1.DataBind();

                    SetDate();
                    AddTask();
                    this.EditMode = FormDataMode.Add;
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
               
            }

            SetHyperLink();

            //txtCargoNo.Attributes.Add("readonly", "readonly");
            //this.ShowPageMessage(this.lblMessage);
            // Response.Write("UserID : " + this.UserID.ToString());

        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            

        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            EditTask();

            //List<dcCARGO_CREATION_DETAIL> roomList = HMRESERVATION_DTLBL.GetRoomInfoList();
            //GridView1.DataSource = roomList;
            //GridView1.DataBind();
        }

        public void FillCombo()
        {
            //ddlCountryId.Items.Clear();
            //ddlCountryId.AppendDataBoundItems = true;
            //ddlCountryId.DataTextField = "COUNTRY_NAME";
            //ddlCountryId.DataValueField = "COUNTRY_ID";
            //ddlCountryId.DataSource = HMCOUNTRY_MSTBL.GetCountryList();
            //ddlCountryId.DataBind();
            //ddlCountryId.SelectedIndex = 0;


        }

        protected override void Render(HtmlTextWriter writer)
        {

            //Page.ClientScript.RegisterForEventValidation(btnPopupTrigger.UniqueID);
            //Page.ClientScript.RegisterForEventValidation(btnPopupTrigger.UniqueID, "");
            //Page.ClientScript.RegisterForEventValidation(btnPopupTrigger.UniqueID, "fillcombo");
            //Page.ClientScript.RegisterForEventValidation(btnPopupTrigger.UniqueID, "getbalance");

            base.Render(writer);
        }

        private void SetDate()
        {


        }

        private void ReadTask()
        {
            this.EditMode = FormDataMode.Read;

            SetControl(FormDataMode.Read);

        }
        private void AddTask()
        {
            
            this.EditMode = FormDataMode.Add;
            this.IsDirty = false;
            

            //add
            SetControl(FormDataMode.Add);
        }
        private void EditTask()
        {
            this.EditMode = FormDataMode.Edit;
          
            this.EditMode = FormDataMode.Edit;

        }

       

        private void SetControl(FormDataMode dataMode)
        {
            //bool isEnabled = false;

            //if (dataMode == FormDataMode.Add | dataMode == FormDataMode.Edit)
            //{
            //    isEnabled = true;
            //}


            //SetControlGrid(dataMode);

            bool isEnabled = (dataMode == FormDataMode.Add || dataMode == FormDataMode.Edit);

           

            //txtCargoDate.Enabled = isEnabled;
    ;

   

        }

      
      





        protected void btnSave_Click(object sender, EventArgs e)
        {

            //SaveTask();

        }

      
        
        private void SetHyperLink()
        {

            

        }

        //newly added comment
       



        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            AddTask();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {

            string networkFolder = @"\\192.168.12.235\ExcelFile"; // Use shared folder, not c$
            string username = "Administrator";
            string password = "NMl@152634";

            try
            {
                if (fileUploadExcel.HasFile)
                {
                    string ext = Path.GetExtension(fileUploadExcel.FileName).ToLower();
                    if (ext == ".xlsx")
                    {
                        string fileName = Path.GetFileName(fileUploadExcel.FileName);

                        using (new NetworkConnection(networkFolder, username, password))
                        {
                            if (!Directory.Exists(networkFolder))
                            {
                                Directory.CreateDirectory(networkFolder);
                            }

                            string fullPath = Path.Combine(networkFolder, fileName);
                            fileUploadExcel.SaveAs(fullPath);

                            SaveFilePathToOracle(fileName, fullPath);

                            lblMessage.Text = "File uploaded and path saved successfully.";
                            lblMessage.ForeColor = System.Drawing.Color.Green;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(btnUpload, GetType(), "", "alert('Only Excel File Upload is allowed!!');", true);
                    }
                }
                else
                {
                    lblMessage.Text = "Please select a file.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
            }

            //try
            //{
            //    if (fileUploadExcel.HasFile)
            //    {
            //        string ext = Path.GetExtension(fileUploadExcel.FileName).ToLower();
            //        if (ext == ".xlsx")
            //        {
            //            string fileName = Path.GetFileName(fileUploadExcel.FileName);

            //            // Server physical path to save
            //            // string folderPath = Server.MapPath("\\192.168.12.235\\c$\\ExcelFile");
            //            string folderPath = @"\\192.168.12.235\c$\ExcelFile";
            //            if (!Directory.Exists(folderPath))
            //            {
            //                Directory.CreateDirectory(folderPath);
            //            }

            //            // Full physical path
            //            string fullPath = Path.Combine(folderPath, fileName);

            //            fileUploadExcel.SaveAs(fullPath);

            //            string dbPath = fullPath;

            //            SaveFilePathToOracle(fileName, dbPath);

            //            lblMessage.Text = "File uploaded and path saved successfully.";
            //            lblMessage.ForeColor = System.Drawing.Color.Green;
            //        }
            //        else
            //        {
            //            ScriptManager.RegisterClientScriptBlock(btnUpload, GetType(), "", "alert('Only Excel File Upload is allowed!!');", true);
            //            return ;
            //        }
            //    }
            //    else
            //    {
            //        lblMessage.Text = "Please select a file.";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    lblMessage.Text = "Error: " + ex.Message;
            //}
        }

     
        private void SaveFilePathToOracle(string fileName, string filePath)
        {
            string connStr = "Data Source=192.168.12.235/ORCL;User Id=WRCUORIER;Password=WRCUORIER;";
            using (OracleConnection conn = new OracleConnection(connStr))
            {
                conn.Open();

                string sql = @"INSERT INTO FILE_UPLOAD_CLIENT 
                       (CLIENT_ID, FILE_NAME, FILE_PATH, UPLOAD_DATE, UPLOAD_BY) 
                       VALUES (:clientid, :fileName, :filePath, SYSDATE, :uploadBy)";

                using (OracleCommand cmd = new OracleCommand(sql, conn))
                {
                    cmd.Parameters.Add(":clientid", loggedinUser.CLIENT_ID);
                    cmd.Parameters.Add(":fileName", fileName);
                    cmd.Parameters.Add(":filePath", filePath);
                    cmd.Parameters.Add(":uploadBy", loggedinUser.UserName);

                    cmd.ExecuteNonQuery();
                }
                 conn.Close(); // Not required, using{} will close it
            }
        }



//protected void btnUpload_Click(object sender, EventArgs e)
//{
//    int k = 0;
//    if (FileUpload1.HasFile)
//    {
//        string ext = Path.GetExtension(FileUpload1.FileName).ToLower();
//        if (ext == ".xlsx")
//        {
//            string filePath = Server.MapPath("~/Uploads/" + FileUpload1.FileName);
//            FileUpload1.SaveAs(filePath);

//            var tbl = new DataTable();

//            using (var package = new ExcelPackage(new FileInfo(filePath)))
//            {
//                if (package.Workbook.Worksheets.Count == 0)
//                {
//                    throw new Exception("❌ No worksheets found in the Excel file.");
//                }

//                var sheet = package.Workbook.Worksheets[1];

//                if (sheet.Dimension == null)
//                {
//                    throw new Exception("Worksheet is empty.");
//                }

//                bool hasHeader = true;
//                int totalCols = sheet.Dimension.End.Column;
//                int totalRows = sheet.Dimension.End.Row;

//                // Add columns to DataTable
//                for (int col = 1; col <= totalCols; col++)
//                {
//                    string columnName = hasHeader ? sheet.Cells[1, col].Text : "Column{col}";

//                    if (string.IsNullOrWhiteSpace(columnName))
//                        columnName = "Column{col}";

//                    // Ensure uniqueness
//                    if (tbl.Columns.Contains(columnName))
//                    {
//                        int i = 1;
//                        string newColumnName;
//                        do
//                        {
//                            newColumnName = columnName + "_" + i++;
//                        } while (tbl.Columns.Contains(newColumnName));
//                        columnName = newColumnName;
//                    }

//                    tbl.Columns.Add(columnName);
//                }

//                // Add rows to DataTable
//                int startRow = hasHeader ? 2 : 1;
//                for (int rowNum = startRow; rowNum <= totalRows; rowNum++)
//                {
//                    DataRow row = tbl.NewRow();
//                    for (int col = 1; col <= totalCols; col++)
//                    {
//                        row[col - 1] = sheet.Cells[rowNum, col].Text;
//                    }
//                    tbl.Rows.Add(row);
//                }
//            }

//            // Populate your list from the DataTable
//            this.listDetails.Clear();
            
//            if (tbl.Rows.Count > 0)
//            {
//                foreach (DataRow Row in tbl.Rows)
//                {
//                    dcCN_REFERENCE_DTL cObj = new dcCN_REFERENCE_DTL();
//                    cObj.CN_NUMBER = Row["CN_NUMBER"].ToString();
//                    cObj = CN_REFERENCE_DTLBL.GetCNIDInfoByCNNumber(cObj.CN_NUMBER);
                    
//                   if (cObj != null)
//                    {
//                        cObj.SLNO=k+1;
//                        cObj.CN_ID = cObj.CN_ID;
                        
//                        cObj.REF_CLIENT_CODE = Row["CLIENT_CODE"].ToString();
//                        cObj.REF_MOBILE_NO = Row["MOBILE_NO"].ToString();
//                        cObj.REF_CHALLAN_NO = Row["CHALLAN_NO"].ToString();
//                        cObj.REF_ACCOUNT_NO = Row["ACCOUNT_NO"].ToString();
//                        cObj.CN_REF_DTL_ID = 0;
                        
//                        this.listDetails.Add(cObj);
//                    }
//                   else
//                   {
//                       throw new Exception("❌ Invalid CN No worksheets found in the Excel file. " + Row["CN_NUMBER"].ToString() + "");
//                   }
//                }

//                GridView1.DataSource = listDetails;
//                GridView1.DataBind();
//                //SetControlGrid();

//                btnSave.Enabled = true;
//            }
//        }
//    }
//}


    }
}