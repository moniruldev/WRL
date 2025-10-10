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
using System.ComponentModel;

namespace PG.Web.WREL
{
    public partial class BranchThanaAssigning : BagePage
    {
        //this 
        string ViewStateKey = "BRANCH_THANA_ID";
        string ViewStateKeyPrev = "BRANCH_THANA_ID_PREV";
        ReportOpenTypeEnum ReportOpenType = ReportOpenTypeEnum.Preview;
        // int CompanyID = 0;

        int BRANCH_THANA_ID = 0;
        private int totalRowCount = 0;
        string saveMsg = string.Empty;
        string errMsg = string.Empty;

        private dcUser loggedinUser = null;
        public string ReportViewPageLink = PageLinks.ReportLinks.GetLink_ReportView;
        public string ReportViewPDFPageLink = PageLinks.ReportLinks.GetLink_ReportViewPDF;
        public string ReportPrintPageLink = PageLinks.ReportLinks.GetLink_ReportPrint;
        public string ReportPDFPageLink = PageLinks.ReportLinks.GetLink_ReportPDF;

        public string TownListServiceLink = PageLinks.InventoryLink.GetLink_TownList;
        public string BranchListServiceLink = PageLinks.InventoryLink.GetLink_BranchList;

        List<dcBRANCH_THANA_ASSIGNING> listDetails = new List<dcBRANCH_THANA_ASSIGNING>();

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

            this.BRANCH_THANA_ID = base.GetPageQueryInteger("id");

            if (!IsPostBack) //first Time
            {


                hdnLoggedInUser.Value = loggedinUser.UserID.ToString();
                //FillCombo();





                if (this.BRANCH_THANA_ID == 0) //not query string
                {
                    //List<dcBRANCH_THANA_ASSIGNING> roomList = HMRESERVATION_DTLBL.GetRoomInfoList();
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
                this.BRANCH_THANA_ID = int.Parse(ViewState[ViewStateKey].ToString());
            }

            SetHyperLink();


            //this.ShowPageMessage(this.lblMessage);
            // Response.Write("UserID : " + this.UserID.ToString());

        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {


        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            EditTask();

            //List<dcBRANCH_THANA_ASSIGNING> roomList = HMRESERVATION_DTLBL.GetRoomInfoList();
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
            ReadData(this.BRANCH_THANA_ID);
            ViewState[ViewStateKey] = this.BRANCH_THANA_ID.ToString();

            SetControl(FormDataMode.Read);

        }
        private void AddTask()
        {
            ViewState[ViewStateKeyPrev] = this.BRANCH_THANA_ID.ToString();

            this.EditMode = FormDataMode.Add;
            this.IsDirty = false;
            this.BRANCH_THANA_ID = 0;
            ViewState[ViewStateKey] = "0";
            this.listDetails.Clear();
            CheckAndAddGridBlankRow();
            BindDataToGrid(this.listDetails);
            //add
            SetControl(FormDataMode.Add);
        }
        private void EditTask()
        {
            this.EditMode = FormDataMode.Edit;
            ReadData(this.BRANCH_THANA_ID);
            this.EditMode = FormDataMode.Edit;
            ViewState[ViewStateKey] = this.BRANCH_THANA_ID.ToString();
            SetControl(FormDataMode.Edit);
        }

        private bool ReadData(int id)
        {
            bool bStatus = false;

            List<dcBRANCH_THANA_ASSIGNING> cObjList = BRANCH_THANA_ASSIGNINGBL.GetBranchThanaInfoByBranchId(Conversion.StringToInt(hdnBranchId.Value), null);
            if (cObjList.Any())
            {
                var obj = cObjList.First();

                hdnBranchId.Value = obj.BRANCH_ID.ToString();
                txtBranchName.Text = obj.BRANCH_NAME;

                BindDataToGrid(cObjList);
                bStatus = true;
            }
            return bStatus;

        }

        private void SetControl(FormDataMode dataMode)
        {

            bool isEnabled = (dataMode == FormDataMode.Add || dataMode == FormDataMode.Edit);

            btnEdit.Visible = !isEnabled;
            btnSave.Visible = isEnabled;

            SetControlGrid(dataMode);

        }

        private void SetTextBoxState(TextBox txtBox, bool isEnabled)
        {
            if (isEnabled)
            {
                txtBox.Attributes.Remove("disabled");
                txtBox.CssClass = "form-control form-control-sm";
            }
            else
            {
                txtBox.Attributes["disabled"] = "disabled";
                txtBox.CssClass = "form-control form-control-sm";
            }
        }


        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {

            switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:
                    e.Row.CssClass += " gridRow";
                    break;
                //case DataControlRowType.Header:
                //    e.Row.CssClass += " headerRow";
                //    break;
                //case DataControlRowType.Footer:
                //    e.Row.CssClass += " footerRow";
                //    break;
                //case DataControlRowType.Pager:
                //    e.Row.CssClass += " pagerRow";
                //    break;
                //case DataControlRowType.EmptyDataRow:
                //    e.Row.CssClass += " gridRow";
                //    break;
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int currentRowIndex = e.Row.RowIndex;
                int serialNo = currentRowIndex +1;

                Label lblSerial = (Label)e.Row.FindControl("lblSerialNo");
                if (lblSerial != null)
                {
                    lblSerial.Text = serialNo.ToString();
                }

                HiddenField hdnStatus = (HiddenField)e.Row.FindControl("hdnStatus");
                DropDownList ddlStatus = (DropDownList)e.Row.FindControl("ddlStatus");
                if(hdnStatus.Value != string.Empty)
                {
                    ddlStatus.SelectedValue = hdnStatus.Value;
                }


                string rowID = e.Row.ClientID;
                string js = string.Format("return ShowDetailsPopup('{0}');", rowID);

            }



        }



        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int RowIndex = gvr.RowIndex;
                GridView1.Rows[RowIndex].Visible = false;
                RefreshGrid();

            }

         
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
                        ((Label)gvR.FindControl("lblSerialNo")).Text = slNo.ToString();
                    }
                }
            }
        }

        private void BindDataToGrid(List<dcBRANCH_THANA_ASSIGNING> listData)
        {
            int rowCount = listData.Count;
            this.totalRowCount = listData.Count;
            if (rowCount == 0)
            {
                listData.Add(new dcBRANCH_THANA_ASSIGNING());
            }

            GridView1.DataSource = listData.ToList();
            GridView1.DataBind();
            //GridView1.CssClass = "grid";

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
                    ((TextBox)gvR.FindControl("txtTownName")).Enabled = isEnabled;
                    ((DropDownList)gvR.FindControl("ddlStatus")).Enabled = isEnabled;
                    //TextBox txtAgentCompanyName = ((TextBox)gvR.FindControl("txtAgentCompanyName"));
                    //SetTextBoxState(txtAgentCompanyName, false);
                    LinkButton lnkDelete = (LinkButton)gvR.FindControl("btnDeleteRow");
                    lnkDelete.Enabled = isEnabled;
                    if (!isEnabled)
                    {
                        lnkDelete.OnClientClick = "";
                    }

                }
            }
           

            btnNewRow.Enabled = isEnabled;

        }

        private void ReadDetailsFromGrid()
        {

            //int locationID = Convert.ToInt32(hdnLocationID.Value);
            this.listDetails.Clear();

            ///addition
            foreach (GridViewRow gvR in this.GridView1.Rows)
            {
                if (gvR.RowType == DataControlRowType.DataRow)
                {

                    dcBRANCH_THANA_ASSIGNING cObj = new dcBRANCH_THANA_ASSIGNING();
                    ReadGridRowToObject(gvR, this.GridView1.DataKeys, cObj);

                    if (cObj._RecordState == RecordStateEnum.Deleted)
                    {
                        if (cObj.BRANCH_THANA_ID > 0)
                        {
                            this.listDetails.Add(cObj);
                        }
                    }
                    else
                    {
                        //if(cObj.ROOM_QTY > 0)
                        this.listDetails.Add(cObj);
                    }

                }
            }
        }

        private void ReadGridRowToObject(GridViewRow gvR, DataKeyArray dataKeys, dcBRANCH_THANA_ASSIGNING cObj)
        {
            decimal d;
            string strD;


            strD = ((HiddenField)gvR.FindControl("hdnBRANCH_THANA_ID")).Value;
            cObj.BRANCH_THANA_ID = Conversion.StringToInt(strD);
            if (cObj.BRANCH_THANA_ID > 0)
            {
                cObj._RecordState = RecordStateEnum.Edited;
            }
            else
            {
                cObj._RecordState = RecordStateEnum.Added;
            }

            cObj.BRANCH_ID = Conversion.StringToInt(hdnBranchId.Value);

            strD = ((TextBox)gvR.FindControl("txtTownName")).Text;
            cObj.TOWN_NAME = strD;

            strD = ((HiddenField)gvR.FindControl("hdnTownId")).Value;
            cObj.TOWN_ID = Conversion.StringToInt(strD);

            strD = ((DropDownList)gvR.FindControl("ddlStatus")).SelectedValue;
            cObj.IS_ACTIVE = strD;

            


            if (!gvR.Visible)
            {
                cObj._RecordState = RecordStateEnum.Deleted;
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            SaveTask();

        }

        private bool SaveTask()
        {

            if (!Page.IsValid)
            { return false; }


            if (CheckData())
            {

                bool bStatus = SaveData();

                if (bStatus)
                {
                    //ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Data Saved Successfully');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('success', 'Data Saved Successfully!', 'Success');", true);

                }
                else
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('error', ' Data not Saved!', 'Error');", true);
                }

            }
            else
            {

                //ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Error !! Data not Saved');", true);
                //this.SetPageMessage(errMsg, MessageTypeEnum.InvalidData);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('error', ' Data not Saved!', 'Error');", true);
            }

            return true;

        }

        private bool ValidateDetails(List<dcBRANCH_THANA_ASSIGNING> list)
        {
            bool y = true;
            foreach (var item in list)
            {
                if (!(item.TOWN_ID > 0))
                {
                    ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Please select atleast one town !!');", true);
                    y = false;

                }

            }

            return y;
        }

        private bool CheckData()
        {
            errMsg = string.Empty;

            ReadDetailsFromGrid();

            if (ValidateDetails(this.listDetails))
            {
                return true;
            }
            else
            {


                ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('" + errMsg + "');", true);
                this.SetPageMessage(errMsg, MessageTypeEnum.InvalidData);
                return false;
            }

        }
        private void SetHyperLink()
        {

            //new button
            //string hLink = "javascript:tbopenSalInfo("+ this.AccYearID.ToString() +")";
            //if (base.PageMode == PG.Core.Web.PageModeEnum.InTab)
            //{
            //    hLink = "javascript:tbopenSalInfo(" + this.AccYearID.ToString() + ")";
            //    //this.btnSalaryInfo = string.Empty;
            //    this.btnSalaryInfo.Attributes.Add("onclick", hLink);
            //}
            //else
            //{
            //    hLink = "~/Master/EmpSalaryInfo.aspx?eid=" + this.AccYearID.ToString();
            //    //this.btnAddNew.PostBackUrl = hLink;
            //    //this.btnAddNew.OnClientClick = string.Empty;
            //    this.btnSalaryInfo.Attributes.Add("onclick", hLink);
            //}

        }

        //newly added comment
        private bool SaveData()
        {

            bool bStatus = false;
            bool isAdd = false;
            int newBRANCH_THANA_ID = 0;
            ReadDetailsFromGrid();
            foreach(dcBRANCH_THANA_ASSIGNING obj in this.listDetails)
            {
                dcBRANCH_THANA_ASSIGNING cObj = new dcBRANCH_THANA_ASSIGNING();
                cObj._RecordState = obj._RecordState;
                if(cObj._RecordState == RecordStateEnum.Added)
                {
                    cObj.CREATE_BY = loggedinUser.UserID.ToString();
                    cObj.CREATE_DATE = DateTime.Now;
                }
                else
                {
                    cObj.BRANCH_THANA_ID = obj.BRANCH_THANA_ID;
                    cObj.EDIT_BY = loggedinUser.UserID.ToString();
                    cObj.EDIT_DATE = DateTime.Now;
                }


                cObj.BRANCH_ID = obj.BRANCH_ID;
                cObj.TOWN_ID = obj.TOWN_ID;
                cObj.IS_ACTIVE = obj.IS_ACTIVE;



                newBRANCH_THANA_ID = BRANCH_THANA_ASSIGNINGBL.Save(cObj);
            }
   

          
            if (newBRANCH_THANA_ID > 0)
            {
                this.BRANCH_THANA_ID = newBRANCH_THANA_ID;
                ReadTask();
                bStatus = true;
            }

            return bStatus;
        }



        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            AddTask();
        }




        #region Report

        protected void btnMRRPrint_Click(object sender, EventArgs e)
        {
            //int issueMasterId = Conversion.StringToInt(hdnBRANCH_THANA_ID.Value);
            //if (issueMasterId > 0)
            //{
            //    //   string mrrNo = txtIssueNo.Text.Trim();

            //    ReportOptions rptOption = GetReportOptions();
            //    AppReport rpt = IGRReportRGN.GenerateIGRReport(issueMasterId, rptOption); //MaterialReceiveGenerateRGN.GenerateMaterialReceiveItemDetails(mrrNo, rptOption);
            //    string rk = AppReport.SetAppReportToSession(rpt, this.Context);

            //    ShowReport(rk);
            //}
            //else
            //{
            //    ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Please select IGR First. !!');", true);
            //}
        }


        private void ShowReport(string reportKey)
        {
            ReportOpenTypeEnum rptOpenType = this.ReportOpenType;
            ReportViewModeEnum rptViewMode = (ReportViewModeEnum)Convert.ToInt32(1);

            bool pdfView = true;

            string strWait = "true";
            string strIsPrint = "false";
            string strIsPDFAutoPrint = "false";
            string strPDFView = "false";


            switch (rptOpenType)
            {
                case ReportOpenTypeEnum.Preview:
                    //if (ddlReportFormat.SelectedValue == "1")
                    //{
                    //    strPDFView = "true";
                    //}

                    break;
                case ReportOpenTypeEnum.Print:
                    rptViewMode = ReportViewModeEnum.InThisTab;
                    strWait = "false";
                    strIsPrint = "true";
                    break;
                case ReportOpenTypeEnum.Export:
                    //rptViewMode = ReportViewModeEnum.InThisTab;
                    rptViewMode = ReportViewModeEnum.InNewWindow;
                    strWait = "false";
                    break;
            }

            bool isPDFAutoPrint = true;
            if (Request.Browser.Browser.ToLower().Contains("ie") == true)
            {
                // isPDFAutoPrint = !AccSettings.IsIERsClientPrint;
            }

            strIsPDFAutoPrint = isPDFAutoPrint ? "true" : "false";


            //string strTime = DateTime.Now.ToString("hhmm");
            string strTime = DateTime.Now.ToFileTime().ToString();
            //string strTime = DateTime.Now now.getTime().toString();
            string url = this.ReportViewPageLink + "?rk=" + reportKey + "&_tt=" + strTime;
            if (pdfView && rptOpenType == ReportOpenTypeEnum.Preview)
            {
                url = this.ReportViewPDFPageLink + "?rk=" + reportKey + "&_tt=" + strTime;
            }


            string jsScript = "$(function(){tbopen('" + reportKey + "', " + strPDFView + ", " + strIsPrint + "," + strIsPDFAutoPrint + "," + strWait + ");});";


            switch (rptViewMode)
            {
                case ReportViewModeEnum.InThisTab:
                    if (rptOpenType == ReportOpenTypeEnum.Print)
                    {
                        ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "showreport", jsScript, true);
                    }
                    else
                    {
                        Response.Redirect(url, false);
                    }


                    break;
                case ReportViewModeEnum.InNewTab:
                    ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "showreport", jsScript, true);
                    break;
                case ReportViewModeEnum.InNewWindow:
                    ClientScript.RegisterStartupScript(this.GetType(), "newWindow", String.Format("<script>window.open('{0}');</script>", url));
                    break;
                case ReportViewModeEnum.InDialog:
                    break;
                default:
                    ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "showreport", jsScript, true);
                    break;
            }
        }


        private ReportOptions GetReportOptions()
        {
            ReportOptions rptOption = new ReportOptions();

            //rptOption.ReportViewMode = (ReportViewModeEnum)Convert.ToInt32(ddlReportFormat.SelectedValue);
            //rptOption.ReportOpenType = this.ReportOpenType;

            //AppInfo.SetAppInfoToReportOptions(rptOption);
            //CompanyInfo.SetCompanyInfoToReportOptions(rptOption, this.Context);
            //rptOption.UserName = base.LoginUserName;


            return rptOption;
        }

        #endregion


        protected void btnNewRow_Click(object sender, EventArgs e)
        {
            ReadDetailsFromGrid();
            AddBlankRowToGridList();

            BindDataToGrid(this.listDetails);
            // to focusing the row
            if (GridView1.Rows.Count > 0)
            {
                int lastRowIndex = GridView1.Rows.Count - 1;
                TextBox txtLastCN = GridView1.Rows[lastRowIndex].FindControl("txtRecipientName") as TextBox;
                if (txtLastCN != null)
                {
                    ScriptManager.GetCurrent(this).SetFocus(txtLastCN);
                }
            }

            SetControlGrid(FormDataMode.Add);

        }

        private void CheckAndAddGridBlankRow()
        {

            int RowCheck = 1;
            int cntAdd = listDetails.Count();

            if (cntAdd < RowCheck)
            {
                int diffAdd = RowCheck - cntAdd;
                for (int i = 0; i < diffAdd; i++)
                {
                    AddBlankRowToGridList();
                }
            }
            else
            {
                AddBlankRowToGridList();
            }

        }

        private void AddBlankRowToGridList()
        {
            dcBRANCH_THANA_ASSIGNING cObj = new dcBRANCH_THANA_ASSIGNING();
            cObj._RecordState = RecordStateEnum.Added;
            this.listDetails.Add(cObj);
            //this.listDetails.Insert(0, cObj); // this is for row top order
        }

        protected void txtRecipientName_TextChanged(object sender, EventArgs e)
        {
            //dcBRANCH_THANA_ASSIGNING cnInfo = CN_CREATION_MSTBL.GetCNInfoByCNNumber(txtRecipientName.text);
            btnNewRow_Click(sender, e);
            //SetFocusToLasttxtRecipientName();
        }
        private void SetFocusToLasttxtRecipientName()
        {
            int lastIndex = GridView1.Rows.Count - 1;
            if (lastIndex >= 0)
            {
                TextBox lastTxtCN = GridView1.Rows[0].FindControl("txtRecipientName") as TextBox;
                if (lastTxtCN != null)
                {
                    ScriptManager.GetCurrent(this).SetFocus(lastTxtCN);
                }
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            //string ConStr = "";
            //string ext = Path.GetExtension(FileUpload1.FileName).ToLower();


            //if (!string.IsNullOrEmpty(ext))
            //{
            //    string path = Server.MapPath("~/Uploads/" + FileUpload1.FileName);
            //    if (!File.Exists(path))
            //    {
            //        File.Delete(path);
            //    }
            //    FileUpload1.SaveAs(path);

            //    if (ext.Trim() == ".xls")
            //    {
            //        //connection string for that file which extantion is .xls  
            //        ConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
            //    }
            //    else if (ext.Trim() == ".xlsx")
            //    {
            //        //connection string for that file which extantion is .xlsx  
            //        ConStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
            //    }

            //    string query = "SELECT * FROM [Sheet1$]";

            //    OleDbConnection conn = new OleDbConnection(ConStr);

            //    if (conn.State == ConnectionState.Closed)
            //    {
            //        conn.Open();
            //    }
            //    //create command object  
            //    OleDbCommand cmd = new OleDbCommand(query, conn);

            //    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            //    DataSet ds = new DataSet();

            //    da.Fill(ds);

            //    conn.Close();

            //    DataTable tbl = ds.Tables[0];
            //    int count = tbl.Rows.Count;
            //    string strSql = string.Empty;

            //    this.listDetails.Clear();


            //    foreach (DataRow Row in tbl.Rows)
            //    {
            //        dcBRANCH_THANA_ASSIGNING cObj = new dcBRANCH_THANA_ASSIGNING();
              
            //        cObj.CONSIGNEE_NAME = (Row["CONSIGNEE_NAME"].ToString());
            //        cObj.CONSIGNEE_ADDRESS = (Row["CONSIGNEE_ADDRESS"].ToString());
            //        cObj.CONSIGNEE_MOBILE_NO = (Row["CONSIGNEE_MOBILE_NO"].ToString());
            //        cObj.DESTINATION_DIST_NAME = (Row["DESTINATION_DIST_NAME"].ToString());
            //        cObj.DESTINATION_TOWN_NAME = (Row["DESTINATION_TOWN_NAME"].ToString());

            //    }

            //    GridView1.DataSource = listDetails;
            //    GridView1.DataBind();
            //    //SetControlGrid();

            //    btnSave.Enabled = true;

            //}


           
        }

        protected void btnDownloadSample_Click(object sender, EventArgs e)
        {
            string fileName = "sample_parcel_import.xlsx"; // Name of the file to download
            string filePath = Server.MapPath("~/DownloadSample/" + fileName);

            if (File.Exists(filePath))
            {
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", string.Format("attachment; filename={0}", fileName));
                Response.TransmitFile(filePath);
                Response.End();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('error', ' File not found!', 'Error');", true);
            }
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            this.listDetails = BRANCH_THANA_ASSIGNINGBL.GetBranchThanaInfoByBranchId(Conversion.StringToInt(hdnBranchId.Value), null);
            BindDataToGrid(this.listDetails);
            SetControlGrid(FormDataMode.Add);
            //SetTextBoxState(txtAgentName, false);


        }
    }
}