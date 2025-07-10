using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PG.DBClass.AccountingDC;
using PG.DBClass.AccountingDC.AccEnums;
using PG.DBClass.AccountingDC.GeneralLedgerDC;

using PG.BLLibrary.AccountingBL.GeneralLedgerBL;

namespace PG.Web.Report
{
    public partial class ReportTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            clsPrmLedger prmLdg = new clsPrmLedger();
            prmLdg.CompanyID = 1;
            prmLdg.AccYearID = 1;
            prmLdg.FromDate = new DateTime(2013, 1, 1);
            prmLdg.ToDate = new DateTime(2013, 9, 2);
            prmLdg.IncludeOpBalanceType = DBClass.AccountingDC.AccEnums.InculdeOpBalanceEnum.IncludeALL;
            prmLdg.IncludePostType = DBClass.AccountingDC.AccEnums.IncludePostEnum.Posted;

            prmLdg.IncludeZeroValue =  true;
            prmLdg.AmountShowType = AmountShowTypeEnum.OpeningTransClosing;

            prmLdg.GLAccountTypeFilter = GLAccountTypeFilterEnum.NormalControlAccount;

            prmLdg.GroupLedgerShowType = GroupsLedgerShowEnum.Ledgers;
            prmLdg.MaxHierarchyLevel = -1;
            prmLdg.IncludeGLClass = false;
            prmLdg.OrderBy = AccOrderByEnum.Name;


            List<dcGLAccount> accBalance = GLAccountBL.GetAccountBalance(prmLdg, null, null);


            GridView1.DataSource = accBalance;
            GridView1.DataBind();

        }
    }
}