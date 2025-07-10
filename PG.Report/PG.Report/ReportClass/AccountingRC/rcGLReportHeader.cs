using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.Report.ReportClass.AccountingRC
{
    [Serializable]
    public class rcGLReportHeader
    {
        private int m_AccYearID = 0;
        public int AccYearID
        {
            get { return this.m_AccYearID; }
            set { this.m_AccYearID = value; }
        }

        private string m_AccYearName = string.Empty;
        public string AccYearName
        {
            get { return this.m_AccYearName; }
            set { this.m_AccYearName = value; }
        }


        private int m_AccYearIDPrev = 0;
        public int AccYearIDPrev
        {
            get { return this.m_AccYearIDPrev; }
            set { this.m_AccYearIDPrev = value; }
        }

        private string m_AccYearNamePrev = string.Empty;
        public string AccYearNamePrev
        {
            get { return this.m_AccYearNamePrev; }
            set { this.m_AccYearNamePrev = value; }
        }

        private DateTime? m_FromDate = null;
        public DateTime? FromDate
        {
            get { return this.m_FromDate; }
            set { this.m_FromDate = value; }
        }

        private DateTime? m_ToDate = null;
        public DateTime? ToDate
        {
            get { return this.m_ToDate; }
            set { this.m_ToDate = value; }
        }

        private string m_Remarks = string.Empty;
        public string Remarks
        {
            get { return this.m_Remarks; }
            set { this.m_Remarks = value; }
        }

        private string m_NumberFormat = string.Empty;
        public string NumberFormat
        {
            get { return this.m_NumberFormat; }
            set { this.m_NumberFormat = value; }
        }


        private int m_GLAccountID = 0;
        public int GLAccountID
        {
            get { return this.m_GLAccountID; }
            set { this.m_GLAccountID = value; }
        }

        private string m_GLAccountCode = string.Empty;
        public string GLAccountCode
        {
            get { return this.m_GLAccountCode; }
            set { this.m_GLAccountCode = value; }
        }

        private string m_GLAccountName = string.Empty;
        public string GLAccountName
        {
            get { return this.m_GLAccountName; }
            set { this.m_GLAccountName = value; }
        }

        private string m_GLAccountNameDisplay = string.Empty;
        public string GLAccountNameDisplay
        {
            get { return this.m_GLAccountNameDisplay; }
            set { this.m_GLAccountNameDisplay = value; }
        }

        private int m_GLAccountTypeID = 0;
        public int GLAccountTypeID
        {
            get { return this.m_GLAccountTypeID; }
            set { this.m_GLAccountTypeID = value; }
        }

        private int m_GLGroupID = 0;
        public int GLGroupID
        {
            get { return this.m_GLGroupID; }
            set { this.m_GLGroupID = value; }
        }

        private string m_GLGroupName = string.Empty;
        public string GLGroupName
        {
            get { return this.m_GLGroupName; }
            set { this.m_GLGroupName = value; }
        }

        private string m_GLGroupCode = string.Empty;
        public string GLGroupCode
        {
            get { return this.m_GLGroupCode; }
            set { this.m_GLGroupCode = value; }
        }

        private string m_GLGroupNameShort = string.Empty;
        public string GLGroupNameShort
        {
            get { return this.m_GLGroupNameShort; }
            set { this.m_GLGroupNameShort = value; }
        }






        #region Opening

        private decimal m_OpenDebitAmt = 0;
        public decimal OpenDebitAmt
        {
            get { return this.m_OpenDebitAmt; }
            set { this.m_OpenDebitAmt = value; }
        }

        private decimal m_OpenCreditAmt = 0;
        public decimal OpenCreditAmt
        {
            get { return this.m_OpenCreditAmt; }
            set { this.m_OpenCreditAmt = value; }
        }

        private decimal m_OpenAmt = 0;
        public decimal OpenAmt
        {
            get { return this.m_OpenAmt; }
            set { this.m_OpenAmt = value; }
        }

        private decimal m_OpenDebitBalanceAmt = 0;
        public decimal OpenDebitBalanceAmt
        {
            get { return this.m_OpenDebitBalanceAmt; }
            set { this.m_OpenDebitBalanceAmt = value; }
        }

        private decimal m_OpenCreditBalanceAmt = 0;
        public decimal OpenCreditBalanceAmt
        {
            get { return this.m_OpenCreditBalanceAmt; }
            set { this.m_OpenCreditBalanceAmt = value; }
        }


        private decimal m_OpenBalanceAmt = 0;
        public decimal OpenBalanceAmt
        {
            get { return this.m_OpenBalanceAmt; }
            set { this.m_OpenBalanceAmt = value; }
        }

        private int m_OpenBalanceDrCr = 0;
        public int OpenBalanceDrCr
        {
            get { return this.m_OpenBalanceDrCr; }
            set { this.m_OpenBalanceDrCr = value; }
        }

        private string m_OpenBalanceDrCrText = string.Empty;
        public string OpenBalanceDrCrText
        {
            get { return this.m_OpenBalanceDrCrText; }
            set { this.m_OpenBalanceDrCrText = value; }
        }

        private decimal m_OpenBalanceDisplay = 0;
        public decimal OpenBalanceDisplay
        {
            get { return this.m_OpenBalanceDisplay; }
            set { this.m_OpenBalanceDisplay = value; }
        }


        private string m_OpenBalanceText = string.Empty;
        public string OpenBalanceText
        {
            get { return this.m_OpenBalanceText; }
            set { this.m_OpenBalanceText = value; }
        }

        #endregion


        #region Transaction
        private decimal m_TranDebitAmt = 0;
        public decimal TranDebitAmt
        {
            get { return this.m_TranDebitAmt; }
            set { this.m_TranDebitAmt = value; }
        }

        private string m_TranDebitAmtText = string.Empty;
        public string TranDebitAmtText
        {
            get { return this.m_TranDebitAmtText; }
            set { this.m_TranDebitAmtText = value; }
        }



        private int m_TranDebitCount = 0;
        public int TranDebitCount
        {
            get { return this.m_TranDebitCount; }
            set { this.m_TranDebitCount = value; }
        }

        private decimal m_TranCreditAmt = 0;
        public decimal TranCreditAmt
        {
            get { return this.m_TranCreditAmt; }
            set { this.m_TranCreditAmt = value; }
        }

        private string m_TranCreditAmtText = string.Empty;
        public string TranCreditAmtText
        {
            get { return this.m_TranCreditAmtText; }
            set { this.m_TranCreditAmtText = value; }
        }
        private int m_TranCreditCount = 0;
        public int TranCreditCount
        {
            get { return this.m_TranCreditCount; }
            set { this.m_TranCreditCount = value; }
        }


        private decimal m_TranAmt = 0;
        public decimal TranAmt
        {
            get { return this.m_TranAmt; }
            set { this.m_TranAmt = value; }
        }


        private decimal m_TranDebitBalanceAmt = 0;
        public decimal TranDebitBalanceAmt
        {
            get { return this.m_TranDebitBalanceAmt; }
            set { this.m_TranDebitBalanceAmt = value; }
        }

        private decimal m_TranCreditBalanceAmt = 0;
        public decimal TranCreditBalanceAmt
        {
            get { return this.m_TranCreditBalanceAmt; }
            set { this.m_TranCreditBalanceAmt = value; }
        }


        private decimal m_TranBalanceAmt = 0;
        public decimal TranBalanceAmt
        {
            get { return this.m_TranBalanceAmt; }
            set { this.m_TranBalanceAmt = value; }
        }

        private int m_TranBalanceDrCr = 0;
        public int TranBalanceDrCr
        {
            get { return this.m_TranBalanceDrCr; }
            set { this.m_TranBalanceDrCr = value; }
        }

        private string m_TranBalanceDrCrText = string.Empty;
        public string TranBalanceDrCrText
        {
            get { return this.m_TranBalanceDrCrText; }
            set { this.m_TranBalanceDrCrText = value; }
        }


        private decimal m_TranBalanceDisplay = 0;
        public decimal TranBalanceDisplay
        {
            get { return this.m_TranBalanceDisplay; }
            set { this.m_TranBalanceDisplay = value; }
        }


        private string m_TranBalanceText =string.Empty;
        public string TranBalanceText
        {
            get { return this.m_TranBalanceText; }
            set { this.m_TranBalanceText = value; }
        }



        #endregion


        #region Closing Sub1
        private decimal m_CloseDebitAmtSub1 = 0;
        public decimal CloseDebitAmtSub1
        {
            get { return this.m_CloseDebitAmtSub1; }
            set { this.m_CloseDebitAmtSub1 = value; }
        }

        private decimal m_CloseCreditAmtSub1 = 0;
        public decimal CloseCreditAmtSub1
        {
            get { return this.m_CloseCreditAmtSub1; }
            set { this.m_CloseCreditAmtSub1 = value; }
        }


        private decimal m_CloseAmtSub1 = 0;
        public decimal CloseAmtSub1
        {
            get { return this.m_CloseAmtSub1; }
            set { this.m_CloseAmtSub1 = value; }
        }

        private decimal m_CloseBalanceAmtSub1 = 0;
        public decimal CloseBalanceAmtSub1
        {
            get { return this.m_CloseBalanceAmtSub1; }
            set { this.m_CloseBalanceAmtSub1 = value; }
        }

        private string m_CloseBalanceTextSub1 = string.Empty;
        public string CloseBalanceTextSub1
        {
            get { return this.m_CloseBalanceTextSub1; }
            set { this.m_CloseBalanceTextSub1 = value; }
        }


        private int m_CloseBalanceDrCrSub1 = 0;
        public int CloseBalanceDrCrSub1
        {
            get { return this.m_CloseBalanceDrCrSub1; }
            set { this.m_CloseBalanceDrCrSub1 = value; }
        }

        private string m_CloseBalanceDrCrTextSub1 = string.Empty;
        public string CloseBalanceDrCrTextSub1
        {
            get { return this.m_CloseBalanceDrCrTextSub1; }
            set { this.m_CloseBalanceDrCrTextSub1 = value; }
        }

        #endregion

        #region Closing
        private decimal m_CloseDebitAmt = 0;
        public decimal CloseDebitAmt
        {
            get { return this.m_CloseDebitAmt; }
            set { this.m_CloseDebitAmt = value; }
        }

        private decimal m_CloseCreditAmt = 0;
        public decimal CloseCreditAmt
        {
            get { return this.m_CloseCreditAmt; }
            set { this.m_CloseCreditAmt = value; }
        }


        private decimal m_CloseAmt = 0;
        public decimal CloseAmt
        {
            get { return this.m_CloseAmt; }
            set { this.m_CloseAmt = value; }
        }

        private decimal m_CloseDebitBalanceAmt = 0;
        public decimal CloseDebitBalanceAmt
        {
            get { return this.m_CloseDebitBalanceAmt; }
            set { this.m_CloseDebitBalanceAmt = value; }
        }

        private decimal m_CloseCreditBalanceAmt = 0;
        public decimal CloseCreditBalanceAmt
        {
            get { return this.m_CloseCreditBalanceAmt; }
            set { this.m_CloseCreditBalanceAmt = value; }
        }


        private decimal m_CloseBalanceAmt = 0;
        public decimal CloseBalanceAmt
        {
            get { return this.m_CloseBalanceAmt; }
            set { this.m_CloseBalanceAmt = value; }
        }

        private decimal m_CloseBalanceDisplay = 0;
        public decimal CloseBalanceDisplay
        {
            get { return this.m_CloseBalanceDisplay; }
            set { this.m_CloseBalanceDisplay = value; }
        }

        private string m_CloseBalanceText = string.Empty;
        public string CloseBalanceText
        {
            get { return this.m_CloseBalanceText; }
            set { this.m_CloseBalanceText = value; }
        }


        private int m_CloseBalanceDrCr = 0;
        public int CloseBalanceDrCr
        {
            get { return this.m_CloseBalanceDrCr; }
            set { this.m_CloseBalanceDrCr = value; }
        }

        private string m_CloseBalanceDrCrText = string.Empty;
        public string CloseBalanceDrCrText
        {
            get { return this.m_CloseBalanceDrCrText; }
            set { this.m_CloseBalanceDrCrText = value; }
        }



        #endregion

        public List<rcGLReportHeader> GetData()
        {
            return new List<rcGLReportHeader>();
        }
    }

}
