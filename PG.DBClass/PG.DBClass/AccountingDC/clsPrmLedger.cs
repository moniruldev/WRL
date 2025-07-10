using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PG.DBClass.AccountingDC.GeneralLedgerDC;
using PG.DBClass.AccountingDC.AccEnums;

namespace PG.DBClass.AccountingDC
{
    public class clsPrmLedger : ICloneable
    {
        public string NumberFormat = "#,###.00;(-)#,###.00;#";
        public string NumberFormatZero = "#,###.00;(-)#,###.00;#0.00";
        public string NumberFormatBlank = "#;#;#";


        public int CompanyID = 0;
        public int GLClassID = 0;
        public int GLGroupID = 0;
        public int GLAccountID = 0;
       
   
        public int AccYearID = 0;
        public int AccYearIDPrev = 0;
        public DateTime? FromDate = null;
        public DateTime? ToDate = null;
        public bool IsBeforeDate = false;
        //public bool IncludeOpBalance = true;
        //public bool IncludeUnPosted = false;
        public bool DetailedPost = false;

        public bool IsNonProfit = false;


        public bool IsInventoryPeriodic = true;

        public IncludePostEnum IncludePostType = IncludePostEnum.Posted;
        public InculdeOpBalanceEnum IncludeOpBalanceType = InculdeOpBalanceEnum.IncludeALL;


        public GLAccountTypeFilterEnum GLAccountTypeFilter = GLAccountTypeFilterEnum.AllAccount;
        
        public ItemAmountTypeEnum ItemAmountTypeDisplay = ItemAmountTypeEnum.ClosingBalance;
        public ItemAmountTypeEnum ItemAmountTypeCheck = ItemAmountTypeEnum.ClosingBalance;


        public AmountShowTypeEnum AmountShowType = AmountShowTypeEnum.ClosingBalance;


        public int JournalAdjustTypeID = 0;
        public int JournalTypeID = 0;

        public int JournalID = 0;

        public bool GroupByYear = false;
        public bool GroupByJournal = false;
        public bool GroupByTranType = false;




        public bool ShowLiabilitiesFirst = false;
        public bool ShowProfitLossInLiability = false;
        public bool ShowPercentage = false;

        public bool IncludeZeroValue = false;

        public bool IncludeRootGLGroup = true;
        public bool IncludeGLClass = false;

        public bool InsertBlankBetweenGLClass = false;



        //public bool IncludeGroupHierarchy = false;
        public int MaxHierarchyLevel = -1;


        public int DisplayBalanceLevel = 2;


        public string FullNameSeperator = "-";
        
        //public bool IncludeLastGroupAccounts = false;


        public bool IsIndentName = false;
        public string IndentPaddingChar = "\t";
        public bool IsExcelExport = false;
       //public string IndentPaddingCharExcel = "\t\t";
        public string IndentPaddingCharExcel = "\t\t\t\t";



        public AccOrderByEnum OrderBy = AccOrderByEnum.Code;
        public string IndentString = "-";
        public int IndentStringCount = 1;

       // public bool LedgerOnly = false;

        public GroupsLedgerShowEnum GroupLedgerShowType = GroupsLedgerShowEnum.Groups;


        public bool IncludeGroupBalance = true;


        public bool IncludeGroupParents = false;
        public bool IncludeGroupChilds = true;


        public bool ShowGroupParentsBalance = false;


        public bool ControlAccountSummary = true;
        //public bool ShowControlAccountDetails = false;


        public bool IncludeContraEntry = false;
        public bool IncludeSubAccountForControl = false;

        
        public bool IncludeInstrument = false;

        public bool IncludeTranCode = false;
        public bool IncludeCostCenter = false;
        public bool IncludeReference = false;

        public bool IncludeDetOfDetails = false;

        public bool IsCash = false;
        public bool IsBank = false;


        public JournalReportFormatEnum JournalReportFormat = JournalReportFormatEnum.Default;
          //public GLReportTypeEnum ReportType = GLReportTypeEnum.Standard;



        public AccRefTypeEnum AccRefType = AccRefTypeEnum.CostCenter;
        public int AccRefCategoryID = 0;
        public int AccRefID = 0;
        //TODO Change monir
        public int LocationID = 0;

        public List<int> LocationIDList = new List<int>();

        public bool IsLocation=false;

        public clsPrmLedger()
        {

        }

        public clsPrmLedger(int companyID, int accYearID, int glGroupID, int glAccountID, DateTime? fromDate, DateTime? toDate, InculdeOpBalanceEnum includeOpBalanceType, IncludePostEnum includePostType)
        {
            CompanyID = companyID;
            GLGroupID = glGroupID;
            AccYearID = accYearID;
            GLAccountID = glAccountID;
           
            FromDate = fromDate;
            ToDate = toDate;
            IncludeOpBalanceType = includeOpBalanceType;
            IncludePostType = includePostType;
        }


        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }


    public class clsPrmGroup : ICloneable
    {
        public int GLGroupID = 0;
        public int AccYearID = 0;
        public DateTime? FromDate = null;
        public DateTime? ToDate = null;
        public bool IsBeforeDate = false;
        public bool IncludeOpBalance = true;
        public bool IncludeUnPosted = false;
        public bool DetailedPost = false;
        public bool IncludeZeroValue = false;

        public int AccYearIDPrev = 0;

        public int JournalID = 0;

        public bool GroupByYear = false;
        public bool GroupBySystem = false;
        public bool GroupByJournal = false;
        public bool GroupByTranType = false;


        public bool ShowControlAccount = true;
        public bool ShowControlAccountDetails = false;


        public clsPrmGroup()
        {

        }

        public clsPrmGroup(int glGroupID, int accYearID, DateTime? fromDate, DateTime? toDate, bool includeOpBalance, bool includeUnPosted)
        {
            GLGroupID = glGroupID;
            AccYearID = accYearID;
            FromDate = fromDate;
            ToDate = toDate;
            IncludeOpBalance = includeOpBalance;
            IncludeUnPosted = includeUnPosted;

        }


        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
