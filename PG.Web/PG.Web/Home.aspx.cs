using System;
using System.Collections;
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
using System.Collections.Generic;
using System.Text;

using PG.Core;
using PG.Core.Web;
using PG.Core.DBBase;
using PG.Core.Extentions;
using PG.Core.DBFilters;

using PG.BLLibrary.AccountingBL.GeneralLedgerBL;
using System.Web.Script.Serialization;


namespace PG.Web
{
    public partial class Home : BagePage
    {

        public string SearchTestLink = PageLinks.SystemLinks.GetLink_SearchTest;


        public StringBuilder sbTree = new StringBuilder();
        public StringBuilder sbTree2 = new StringBuilder();

        //string sTab = "\t";
        //string sNewLine = "\r\n";


        protected override void OnPreInit(EventArgs e)
        {
            if (Globals.AppMasterPage != string.Empty)
            {
                this.MasterPageFile = Globals.AppMasterPage;
            }
            base.OnPreInit(e);
        }

        private void EmpSelection1_UserControlOKClicked(int id)
        {
            // ... do something when event is fired

            //PayRoll.Controls.EmpSelectionEventArgs y = (PayRoll.Controls.EmpSelectionEventArgs)e;

            int x = id;
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadDashboard(DateTime.Now);
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {

        }

        private void LoadDashboard(DateTime filterDate)
        {
            // fetch metrics from DB based on filterDate
            litTotalParcel.Text = "0";
            litTotalUser.Text = "0";
            litTotalClient.Text = "0";
            litTotalDeliveryMan.Text = "0";

            litTotalHub.Text = "0";
            litTotalAccounts.Text = "0";
            litAgreement.Text = "0";
            litTotalParcelDelivered.Text = "0";

            litDMIncome.Text = "৳0";
            litDMExpense.Text = "৳0";
            litDMBalance.Text = "৳0";

            litMerchantIncome.Text = "৳0";
            litMerchantExpense.Text = "৳0";
            litMerchantBalance.Text = "৳0";

            litBranchIncome.Text = "৳0";
            litBranchExpense.Text = "৳0";
            litBranchBalance.Text = "৳0";

            // revenue and expense totals
            litIncomeTotal.Text = "0";
            litExpenseTotal.Text = "0";
            litRevenueTotal.Text = "0";

                           // Example: Static income/expense data
                var labels = new[] { "Jan", "Feb", "Mar", "Apr", "May" };
                var income = new[] { 5000, 7000, 6000, 8000, 7500 };
                var expense = new[] { 3000, 4000, 3500, 5000, 4500 };

                // Show totals
                litIncomeTotal.Text = income.Sum().ToString("N0");
                litExpenseTotal.Text = expense.Sum().ToString("N0");

                // Prepare chart data
                var chartData = new
                {
                    labels,
                    income,
                    expense
                };

                // Serialize to JSON and inject into page
                var serializer = new JavaScriptSerializer();
                string json = serializer.Serialize(chartData);

                // Literal to inject JSON safely
               litChartData.Text = string.Format("<script type='application/json' id='litChartDataJson'>{0}</script>", json);
           
              var pieData = new
                {
                    labels = new[] { "Product A", "Product B", "Product C" },
                    datasets = new[]
                    {
                        new
                        {
                            data = new[] { 300, 150, 100 },
                            backgroundColor = new[] { "#007bff", "#28a745", "#ffc107" }
                        }
                    }
                };

        string jsonPieData = serializer.Serialize(pieData);

        litPieChartData.Text = string.Format("<script type='application/json' id='pieChartData'>{0}</script>", jsonPieData);


        litRevenueTotal.Text = "550";
        Literal1.Text = "Expense";

      
        }





        public string FromControl(int x)
        {
            return x.ToString();
        }

        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    int companyID = CompanyInfo.GetCompanyID();
        //    GLGroupBL.UpdateGLGroupClass(companyID);
        //    GLAccountBL.UpdateGLAccountClass(companyID);
        //}



    }
}
