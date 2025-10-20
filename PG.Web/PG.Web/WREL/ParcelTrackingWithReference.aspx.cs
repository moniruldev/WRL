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
using PG.Report;
using PG.Report.ReportEnums;
using PG.Report.ReportGen.WRELRGN;
using PG.DBClass.SecurityDC;

namespace PG.Web.WREL
{
    public partial class ParcelTrackingWithReference : BagePage
    {
        public string ReportViewPageLink = PageLinks.ReportLinks.GetLink_ReportView;
        public string ReportViewPDFPageLink = PageLinks.ReportLinks.GetLink_ReportViewPDF;
        public string ReportPrintPageLink = PageLinks.ReportLinks.GetLink_ReportPrint;
        public string ReportPDFPageLink = PageLinks.ReportLinks.GetLink_ReportPDF;

        int CompanyID = 0;
        public string ItemListServiceLink = PageLinks.InventoryLink.GetLink_ItemList;
        public string ItemGroupListServiceLink = PageLinks.InventoryLink.GetLink_ItemGroupList;

        ReportOpenTypeEnum ReportOpenType = ReportOpenTypeEnum.Preview;
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
                pnlTracking.Visible = false;
              
            }
    
        }


        protected void btnTrack_Click(object sender, EventArgs e)
        {
            clsPrmWREL prm = new clsPrmWREL();
            dcUser loggedinUser = AppSecurity.GetUserInfoFromSession();
            lblError.Text = string.Empty;
            prm.CN_NUMBER  = txtParcelNumber.Text.Trim();
            prm.CLIENT_ID = loggedinUser.CLIENT_ID;
            prm.USER_TYPE = loggedinUser.UserType;
          
            if (string.IsNullOrEmpty(prm.CN_NUMBER))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('error', ' Please enter a parcel number.', 'Error');", true);
                pnlTracking.Visible = false;
                return;
            }


            dcCN_CREATION_MST clientParcel = CN_CREATION_MSTBL.GetCNInfoByReference(prm, null);

            if (clientParcel == null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('error', ' Parcel number not found for this client.', 'Error');", true);
                pnlTracking.Visible = false;
                return;
            }

            prm.CN_NUMBER = clientParcel.CN_NUMBER;
            prm.CLIENT_ID = clientParcel.CLIENT_ID;
            // Get completed steps using your class
            List<dcCN_CREATION_MST> completedSteps = CN_CREATION_MSTBL.GetCNTrackingInfoList(prm, null);
          

            if (completedSteps == null || completedSteps.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('error', ' Parcel number not found.', 'Error');", true);
                pnlTracking.Visible = false;
                return;
            }

            pnlTracking.Visible = true;

            HighlightSteps(completedSteps);
            UpdateStatusText(completedSteps);
            lblCnNumber.Text = string.Format("Parcel Number: {0}", clientParcel.CN_NUMBER);
            lblRecepentName.Text = string.Format("Recipient Name: {0}", clientParcel.CONSIGNEE_NAME);
            lblRecepentAddress.Text = string.Format("Recipient Address: {0}", clientParcel.CONSIGNEE_ADDRESS);
            lblItemName.Text = string.Format("Item: {0}", clientParcel.ITEM_NAME);
            if (clientParcel.CONSIGNEE_MOBILE_NO != "")
            {
                lblMobileNo.Text = string.Format("Recipient Mobile: {0}", clientParcel.CONSIGNEE_MOBILE_NO);
            }
            if (clientParcel.DELIVERY_DATE != null)
            {
                lblDeliveryDate.Text = string.Format("Delivery Date: {0}", Convert.ToDateTime(clientParcel.DELIVERY_DATE).ToString("dd-MMM-yyyy"));
            }
            if (clientParcel.REFUND_DATE != null)
            {
                lblReturnedDate.Text = string.Format("Returned Date: {0}", Convert.ToDateTime(clientParcel.REFUND_DATE).ToString("dd-MMM-yyyy"));
            }
            lblBookingDate.Text = string.Format("Booking Date: {0}", Convert.ToDateTime(clientParcel.BOOKING_DATE).ToString("dd-MMM-yyyy"));
            lblDeliveryMan.Text = string.Format("Delivery Man: {0}", clientParcel.DELIVERY_MAN_NAME);
            lblDeliveryManMobile.Text = string.Format("Del. Man Mobile: {0}", clientParcel.DEL_MOBILE_NO);

            dcCN_CREATION_MST osf = new dcCN_CREATION_MST();
            osf = CN_CREATION_MSTBL.GetCNInfoListimage(prm, null);



            if (osf.PODIMG != null && osf.PODIMG.Length > 0)
            {
                // byte[] photoBytes = img.POD as byte[];

                string imageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(osf.PODIMG);
                imgPhoto.ImageUrl = imageUrl; // Your <asp:Image> control
            }
            else
            {
                imgPhoto.ImageUrl = "~/Images/no-image.png"; // Fallback
            }

        }


        private void HighlightSteps(List<dcCN_CREATION_MST> completedSteps)
        {
            var stepControls = new Dictionary<int, HtmlGenericControl>
            {
                { 1, step1 },
                { 2, step2 },
                { 3, step3 },
                { 4, step4 },
                { 5, step5 },
                { 6, step6 }
            };

            foreach (var stepNum in stepControls.Keys)
            {
                HtmlGenericControl stepDiv = stepControls[stepNum];

                stepDiv.Attributes["class"] = stepDiv.Attributes["class"].Replace("completed", "").Trim();

                if (completedSteps.Any(s => s.STEP_NUMBER == stepNum))
                {
                    stepDiv.Attributes["class"] += " completed";
                }
            }
        }

        private void UpdateStatusText(List<dcCN_CREATION_MST> completedSteps)
        {
            if (completedSteps.Count > 0)
            {
                int lastStep = completedSteps.Max(s => s.STEP_NUMBER);
                lblStatusMessage.Text = string.Format("Current Status: {0}", GetStatusLabel(lastStep));
            }
        }

        private string GetStatusLabel(int step)
        {
            switch (step)
            {
                case 1: return "Information Received";
                case 2: return "Shipment Picked Up";
                case 3: return "In Transit";
                case 4: return "Arrived at Destination";
                case 5: return "Out for Delivery";
                case 6: return "Delivered";
                default: return "Unknown";
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
        private void ShowReport(string reportKey)
        {

            ReportOpenTypeEnum rptOpenType = this.ReportOpenType;
            ReportViewModeEnum rptViewMode = (ReportViewModeEnum)Convert.ToInt32(ddlReportViewMode.SelectedValue);

            bool pdfView = ddlReportViewType.SelectedValue == "1";

            string strWait = "true";
            string strIsPrint = "false";
            string strIsPDFAutoPrint = "false";
            string strPDFView = "false";


            switch (rptOpenType)
            {
                case ReportOpenTypeEnum.Preview:
                    if (ddlReportViewType.SelectedValue == "1")
                    {
                        strPDFView = "true";
                    }

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

            //string url = this.ReportViewPageLink + "?rk=" + reportKey + "&_tt=" + strTime;
            //string jsScript = "$(function(){tbopen('" + reportKey + "'," + strWait + ");});";

            //string jsScript = "$(function(){tbopen('" + reportKey + "', " +  strIsPrint  +  "," + strWait + ");});";

            //string jsScript = "$(function(){tbopen('" + reportKey + "', " + strIsPrint +  "," + strIsPDFAutoPrint  + "," + strWait + ");});";

            string jsScript = "$(function(){tbopen('" + reportKey + "', " + strPDFView + ", " + strIsPrint + "," + strIsPDFAutoPrint + "," + strWait + ");});";

            //string jsScript = string.Format("$(function(){tbopen('{0}',{1},{2});});",reportKey,strIsPrint,strWait);


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
                    // ClientScript.RegisterStartupScript(this.GetType(), "newWindow", String.Format("<script>window.open('{0}');</script>", url));
                    ClientScript.RegisterStartupScript(this.GetType(), "newWindow", String.Format("<script>reportInNewWindow('{0}');</script>", url));
                    break;
                case ReportViewModeEnum.InDialog:
                    break;
                default:
                    ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "showreport", jsScript, true);
                    break;
            }
          
        }




    }
}
