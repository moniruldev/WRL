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
//using ClosedXML.Excel;
//using NPOI.SS.UserModel;
//using NPOI.XSSF.UserModel;
//using NPOI.HSSF.UserModel;
//using ClosedXML.Excel;
//using ClosedXML.Excel;

namespace PG.Web.WREL
{
    public partial class CNReferenceEntry : BagePage
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



        public string DistrictListServiceLink = PageLinks.InventoryLink.GetLink_DistrictList;
        public string TownListServiceLink = PageLinks.InventoryLink.GetLink_TownList;
        public string RouteListServiceLink = PageLinks.InventoryLink.GetLink_RouteList;
        public string CNListServiceLink = PageLinks.InventoryLink.GetLink_CNMasterList;
        public string EmplistServiceLink = PageLinks.InventoryLink.GetLink_EmployeeList;

        List<dcCN_REFERENCE_DTL> listDetails = new List<dcCN_REFERENCE_DTL>();
       
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
                this.CN_ID_REF = int.Parse(ViewState[ViewStateKey].ToString());
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
            ReadData(this.CN_ID_REF);
            ViewState[ViewStateKey] = this.CN_ID_REF.ToString();

            SetControl(FormDataMode.Read);

        }
        private void AddTask()
        {
            ViewState[ViewStateKeyPrev] = this.CN_ID_REF.ToString();

            this.EditMode = FormDataMode.Add;
            this.IsDirty = false;
            this.CN_ID_REF = 0;
            ViewState[ViewStateKey] = "0";
            this.listDetails.Clear();
            //CheckAndAddGridBlankRow();
            BindDataToGrid(this.listDetails);
            //add
            SetControl(FormDataMode.Add);
        }
        private void EditTask()
        {
            this.EditMode = FormDataMode.Edit;
            ReadData(this.CN_ID_REF);
            this.EditMode = FormDataMode.Edit;
            ViewState[ViewStateKey] = this.CN_ID_REF.ToString();
            SetControl(FormDataMode.Edit);
        }

        private bool ReadData(int CNID)
        {
            bool bStatus = false;





            this.listDetails = CN_REFERENCE_DTLBL.GetCNRefDetailsList(CNID, null);
                BindDataToGrid(listDetails);

                bStatus = true;
           
            return bStatus;

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
            // buttons
            btnAddNew.Visible = !isEnabled;
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
                
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //int currentRowIndex = e.Row.RowIndex;
                //int serialNo = totalRowCount - currentRowIndex;

                //Label lblSerial = (Label)e.Row.FindControl("lblSerialNo");
                //if (lblSerial != null)
                //{
                //    lblSerial.Text = serialNo.ToString();
                //}
                Label lblSerial = (Label)e.Row.FindControl("lblSerialNo");
                if (lblSerial != null)
                {
                    // Set the serial number as (RowIndex + 1)
                    lblSerial.Text = (e.Row.RowIndex + 1).ToString();
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

            }

            //if (e.CommandName == "roomdetials")
            //{
            //    int roomTypeId = Convert.ToInt32(e.CommandArgument);
            //    DisplayRoomDetails(roomTypeId);


            //}
        }
      


        //protected void DisplayRoomDetails(int roomTypeId)
        //{
        //    byte[] bytes = null;
        //    dcHMROOM_TYPE objRT = HMROOM_TYPEBL.GetRoomTypeInfoById(roomTypeId);
        //    if (objRT.THUMBNAILS_IMAGE != null)
        //    {
        //        bytes = (byte[])objRT.THUMBNAILS_IMAGE;
        //        string strBase64 = Convert.ToBase64String(bytes);
        //        ImgRoomType.ImageUrl = "data:Image/png;base64," + strBase64;
        //    }
        //    lblRoomTitle.Text = "Room Type : " + objRT.TITLE;
        //    lblRoomDescription.Text ="Room Type : " + objRT.TITLE +", Description: "+ objRT.DESCRIPTION + ", Max Person: " + objRT.MAX_PERSON + ", Normal Rate: " + objRT.NORMAL_RATE + ", Discounted Rate: " + objRT.DISCOUNTED_RATE;
        //    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "MyScript", "$('#modalRoomDetails').modal({backdrop: 'static', keyboard: false});", true);
        //}


        private void BindDataToGrid(List<dcCN_REFERENCE_DTL> listData)
        {
            int rowCount = listData.Count;
            this.totalRowCount = listData.Count;
           

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
                    //((TextBox)gvR.FindControl("txtCNName")).Enabled = isEnabled;
                    ((TextBox)gvR.FindControl("txtCNno")).Attributes.Add("readonly", "readonly");
                    ((TextBox)gvR.FindControl("txtClientCode")).Attributes.Add("readonly", "readonly");
                    ((TextBox)gvR.FindControl("txtMobileNo")).Attributes.Add("readonly", "readonly");
                    ((TextBox)gvR.FindControl("txtREF_CHALLAN_NO")).Attributes.Add("readonly", "readonly");
                    ((TextBox)gvR.FindControl("txtREF_ACCOUNT_NO")).Attributes.Add("readonly", "readonly");
                    
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
            //this.listDetails.Clear();

            ///addition
            foreach (GridViewRow gvR in this.GridView1.Rows)
            {
                if (gvR.RowType == DataControlRowType.DataRow)
                {

                    dcCN_REFERENCE_DTL cObj = new dcCN_REFERENCE_DTL();
                    ReadGridRowToObject(gvR, this.GridView1.DataKeys, cObj);

                    if (cObj._RecordState == RecordStateEnum.Deleted)
                    {
                        if (cObj.CN_REF_DTL_ID > 0)
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

        private void ReadGridRowToObject(GridViewRow gvR, DataKeyArray dataKeys, dcCN_REFERENCE_DTL cObj)
        {
            decimal d;
            string strD;

            strD = ((HiddenField)gvR.FindControl("hdnCN_REF_DTL_ID")).Value;
            cObj.CN_REF_DTL_ID = Conversion.StringToInt(strD);
            if (cObj.CN_REF_DTL_ID > 0)
            {
                cObj._RecordState = RecordStateEnum.Edited;
            }
            else
            {
                cObj._RecordState = RecordStateEnum.Added;
            }

            strD = ((HiddenField)gvR.FindControl("hdnCNID")).Value;
            cObj.CN_ID = Conversion.StringToInt(strD);
            strD = ((TextBox)gvR.FindControl("txtCNno")).Text;
            cObj.CN_NUMBER = strD;

            strD = ((TextBox)gvR.FindControl("txtClientCode")).Text;
            cObj.REF_CLIENT_CODE = strD;

            strD = ((TextBox)gvR.FindControl("txtMobileNo")).Text;
            cObj.REF_MOBILE_NO = strD;
            strD = ((TextBox)gvR.FindControl("txtREF_CHALLAN_NO")).Text;
            cObj.REF_CHALLAN_NO = strD;

            strD = ((TextBox)gvR.FindControl("txtREF_ACCOUNT_NO")).Text;
            cObj.REF_ACCOUNT_NO = strD;
            //strD = ((HiddenField)gvR.FindControl("hdnSourceCargoID")).Value;
            //cObj.SOURCE_CN_REF_DTL_ID = Conversion.StringToInt(strD);
            

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
                if(GridView1.Rows.Count==0)
                {
                    ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('First Upload Excel Then Save');", true);
                    return false;
                }

                bool bStatus = SaveData();

                if (bStatus)
                {
                    ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Data Saved Successfully');", true);

                }
                else
                {

                    ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Error !! Data not Saved');", true);
                }

            }
            else
            {

                ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Error !! Data not Saved');", true);
                this.SetPageMessage(errMsg, MessageTypeEnum.InvalidData);
            }

            return true;

        }

        private bool ValidateDetails(List<dcCN_REFERENCE_DTL> list)
        {
            bool y = true;
            foreach (var item in list)
            {
                if ((item.CN_ID == 0))
                {
                    ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Please Enter Valid CN Number !!');", true);
                    y = false;

                }

            }

            return y;
        }

        private bool CheckData()
        {
            errMsg = string.Empty;

            //if (txtCNNumber.Text == "")
            //{
            //    ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Please Enter Check In Date !!');", true);
            //    txtCNNumber.Focus();
            //    return false;

            //}

            

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
            int newREF_ID = 0;
             //ReadDetailsFromGrid();
           // listDetails = Session["CNList"] as List<dcCN_CREATION_MST>;
            foreach (dcCN_REFERENCE_DTL obj in this.listDetails)
            {
                dcCN_REFERENCE_DTL cObj = new dcCN_REFERENCE_DTL();
                cObj._RecordState = RecordStateEnum.Added;
                if (cObj._RecordState == RecordStateEnum.Added)
                {
                    cObj.CREATE_BY = loggedinUser.UserName;
                    cObj.CREATE_DATE = DateTime.Now;
                }
                else
                {
                    cObj.EDIT_BY = loggedinUser.UserName;
                    cObj.EDIT_DATE = DateTime.Now;
                }

                cObj.CN_ID = obj.CN_ID;
                cObj.REF_CLIENT_CODE = obj.REF_CLIENT_CODE;
                cObj.REF_MOBILE_NO = obj.REF_MOBILE_NO;
                cObj.REF_CHALLAN_NO = obj.REF_CHALLAN_NO;
                cObj.REF_ACCOUNT_NO = obj.REF_ACCOUNT_NO;



                newREF_ID = CN_REFERENCE_DTLBL.Save(cObj);
            }



            if (newREF_ID > 0)
            {
                //this.CN_REF_DTL_ID = newREF_ID;
                //ReadTask();
                SetControl(FormDataMode.Read);
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
            //int issueMasterId = Conversion.StringToInt(hdnCN_REF_DTL_ID.Value);
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
            //ReadDetailsFromGrid();
            AddBlankRowToGridList();

            BindDataToGrid(this.listDetails);

            if (GridView1.Rows.Count > 0)
            {
                TextBox txtTopCN = GridView1.Rows[0].FindControl("txtCNName") as TextBox;
                if (txtTopCN != null)
                {
                    ScriptManager.GetCurrent(this).SetFocus(txtTopCN);
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
            dcCN_REFERENCE_DTL cObj = new dcCN_REFERENCE_DTL();
            cObj._RecordState = RecordStateEnum.Added;
            this.listDetails.Add(cObj);
            //this.listDetails.Insert(0, cObj);
        }

   
        private void SetFocusToLastTxtCNName()
        {
            int lastIndex = GridView1.Rows.Count - 1;
            if (lastIndex >= 0)
            {
                TextBox lastTxtCN = GridView1.Rows[0].FindControl("txtCNName") as TextBox;
                if (lastTxtCN != null)
                {
                    ScriptManager.GetCurrent(this).SetFocus(lastTxtCN);
                }
            }
        }

        

       

        private void BindDataToCNNumber(List<dcCARGO_CREATION_DETAIL> listDataCN)
        {
            int rowCount = listDataCN.Count;
            //if (rowCount == 0)
            //{
            //    listData.Add(new dcGLAccountHistoryRef());
            //}

            GridView1.DataSource = listDataCN.ToList();
            GridView1.DataBind();

            GridView1.CssClass = "grid";
            //SumDetGrid1();
            //SumDetGrid1();

            //int i = GRDDTLITEMLIST.PageCount;

            //if (GRDDTLITEMLIST.PageIndex > 0)
            //{
          //  GRDDTLITEMLIST.PageIndex = GRDDTLITEMLIST.PageCount;
               // GRDDTLITEMLIST.PageIndex = GRDDTLITEMLIST.PageIndex - 1;



            //}

        }

      
   

protected void btnUpload_Click(object sender, EventArgs e)
{
    int k = 0;
    if (FileUpload1.HasFile)
    {
        string ext = Path.GetExtension(FileUpload1.FileName).ToLower();
        if (ext == ".xlsx")
        {
            string filePath = Server.MapPath("~/Uploads/" + FileUpload1.FileName);
            FileUpload1.SaveAs(filePath);

            var tbl = new DataTable();

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                if (package.Workbook.Worksheets.Count == 0)
                {
                    throw new Exception("❌ No worksheets found in the Excel file.");
                }

                var sheet = package.Workbook.Worksheets[1];

                if (sheet.Dimension == null)
                {
                    throw new Exception("Worksheet is empty.");
                }

                bool hasHeader = true;
                int totalCols = sheet.Dimension.End.Column;
                int totalRows = sheet.Dimension.End.Row;

                // Add columns to DataTable
                for (int col = 1; col <= totalCols; col++)
                {
                    string columnName = hasHeader ? sheet.Cells[1, col].Text : "Column{col}";

                    if (string.IsNullOrWhiteSpace(columnName))
                        columnName = "Column{col}";

                    // Ensure uniqueness
                    if (tbl.Columns.Contains(columnName))
                    {
                        int i = 1;
                        string newColumnName;
                        do
                        {
                            newColumnName = columnName + "_" + i++;
                        } while (tbl.Columns.Contains(newColumnName));
                        columnName = newColumnName;
                    }

                    tbl.Columns.Add(columnName);
                }

                // Add rows to DataTable
                int startRow = hasHeader ? 2 : 1;
                for (int rowNum = startRow; rowNum <= totalRows; rowNum++)
                {
                    DataRow row = tbl.NewRow();
                    for (int col = 1; col <= totalCols; col++)
                    {
                        row[col - 1] = sheet.Cells[rowNum, col].Text;
                    }
                    tbl.Rows.Add(row);
                }
            }

            // Populate your list from the DataTable
            this.listDetails.Clear();
            
            if (tbl.Rows.Count > 0)
            {
                foreach (DataRow Row in tbl.Rows)
                {
                    dcCN_REFERENCE_DTL cObj = new dcCN_REFERENCE_DTL();
                    cObj.CN_NUMBER = Row["CN_NUMBER"].ToString();
                    cObj = CN_REFERENCE_DTLBL.GetCNIDInfoByCNNumber(cObj.CN_NUMBER);
                    
                   if (cObj != null)
                    {
                        cObj.SLNO=k+1;
                        cObj.CN_ID = cObj.CN_ID;
                        
                        cObj.REF_CLIENT_CODE = Row["CLIENT_CODE"].ToString();
                        cObj.REF_MOBILE_NO = Row["MOBILE_NO"].ToString();
                        cObj.REF_CHALLAN_NO = Row["CHALLAN_NO"].ToString();
                        cObj.REF_ACCOUNT_NO = Row["ACCOUNT_NO"].ToString();
                        cObj.CN_REF_DTL_ID = 0;
                        
                        this.listDetails.Add(cObj);
                    }
                   else
                   {
                       throw new Exception("❌ Invalid CN No worksheets found in the Excel file. " + Row["CN_NUMBER"].ToString() + "");
                   }
                }

                GridView1.DataSource = listDetails;
                GridView1.DataBind();
                //SetControlGrid();

                btnSave.Enabled = true;
            }
        }
    }
}


      
        private void Clear()
        {
            Session["dtDebitTrans"] = null;
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
        private void InitiateDataSource()
        {
            DataTable dtTrans = new DataTable();
            dtTrans.Columns.Add("ChassisNo", typeof(string));
            dtTrans.Columns.Add("Description", typeof(string));

            Session["dtDebitTrans"] = dtTrans;
        }

        protected void btnPasteData_Click(object sender, EventArgs e)
        {
            finddataPaste();
        }

        
        private void finddataPaste()
        {
            //string rundate = BaseContent.GetCompanyDate().ToString("dd-MMM-yyyy");
            if (DrPasteTextBox.Text != "")
            {
                try
                {
                    DataTable dtCh = new DataTable("dtCh");
                    dtCh.Columns.Add("xlCNNo", typeof(string));
                    dtCh.Columns.Add("xlDesc", typeof(string));

                    string copiedContent = Request.Form[DrPasteTextBox.UniqueID];
                    foreach (string row in copiedContent.Split('\n'))
                    {
                        if (!string.IsNullOrEmpty(row))
                        {
                            dtCh.Rows.Add();
                            int i = 0;
                            foreach (string cell in row.Split('\t'))
                            {
                                dtCh.Rows[dtCh.Rows.Count - 1][i] = cell;
                                i++;
                            }
                        }
                    }
                    DrPasteTextBox.Text = "";


                    LoadGridDr(dtCh);


                }
                catch (Exception ex)
                {
                    //messageLabel2.Text = ex.ToString();
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(btnSave, this.Page.GetType(), "Message", string.Format("alert(\"Please select an Excel File{0}.\");", ""), true);
            }
        }

        private void LoadGridDr(DataTable dt)
        {
            DataTable dtDebitTrans = new DataTable();
            dtDebitTrans.Columns.Add("SlNo", typeof(int));
            dtDebitTrans.Columns.Add("CN_REF_DTL_ID", typeof(int));
            dtDebitTrans.Columns.Add("CARGO_DETAIL_ID", typeof(int));
            dtDebitTrans.Columns.Add("CN_ID", typeof(int));
            dtDebitTrans.Columns.Add("CN_NUMBER", typeof(string));
            dtDebitTrans.Columns.Add("Description", typeof(string));
            dtDebitTrans.Columns.Add("CLIENT_NAME", typeof(string));
            dtDebitTrans.Columns.Add("SOURCE_CN_REF_DTL_ID", typeof(int));
            
            
            Session["dtDebitTrans"] = dtDebitTrans;

            DataTable dtdr = (DataTable)Session["dtDebitTrans"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                #region forloop
                {
                    string strCNNo = "";
                    string strDescription = "";
                    int CN_ID = 0;
                    int CN_REF_DTL_ID = 0;
                    string strclientName = "";

                    strCNNo = dt.Rows[i]["xlCNNo"].ToString();
                    strDescription = dt.Rows[i]["xlDesc"].ToString();
                    

                    dcCARGO_CREATION_DETAIL cobjb = CARGO_CREATION_DETAILBL.GetCNInfoByCNNumber(strCNNo);
                    CN_ID = cobjb.CN_ID;
                    strclientName = cobjb.CLIENT_NAME;

                    dcCARGO_CREATION_DETAIL cobjb1 = CARGO_CREATION_DETAILBL.GetCargoIDInfoByCNID(CN_ID);
                    //CN_REF_DTL_ID = cobjb1.CN_REF_DTL_ID;

                    DataRow row;
                    row = dtdr.NewRow();
                    row["SlNo"] = (i + 1).ToString();
                    row["CN_NUMBER"] = strCNNo;
                    row["Description"] = strDescription; //strSubLedgerName;
                    row["CN_REF_DTL_ID"] = 0;
                    row["CN_ID"] = CN_ID;
                    row["CARGO_DETAIL_ID"] = 0;
                    row["SOURCE_CN_REF_DTL_ID"] = CN_REF_DTL_ID;
                    row["CLIENT_NAME"] = strclientName;
                    
                    dtdr.Rows.Add(row);
                    ViewState["dtdr"] = dtdr;
                }
                #endregion forloop
                DataTable dtf = new DataTable();
                dtf = (DataTable)ViewState["dtdr"];
                ViewState["BulkDebit"] = dtf;
                Session["dtDebitTrans"] = dtf;

                GridView1.DataSource = dtf;
                GridView1.DataBind();
            }
        }
        protected void ClearButton_OnClick(object sender, EventArgs e)
        {
            DrPasteTextBox.Text = "";

            //Modalpopupextender2.Show();
        }

    }
}