using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PG.Report.ReportEnums;

namespace PG.Report.ReportClass.AccountingRC
{
    [Serializable]
    public class rcLedger
    {
        private int m_GLAccountID = 0;
        public int GLAccountID
        {
            get { return this.m_GLAccountID; }
            set { this.m_GLAccountID = value; }
        }


        private int m_GLGroupID = 0;
        public int GLGroupID
        {
            get { return this.m_GLGroupID; }
            set { this.m_GLGroupID = value; }
        }

        private string m_GLAccountCode = string.Empty;
        public string GLAccountCode
        {
            get { return this.m_GLAccountCode; }
            set { this.m_GLAccountCode = value; }
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

        private string m_GLGroupName = string.Empty;
        public string GLGroupName
        {
            get { return this.m_GLGroupName; }
            set { this.m_GLGroupName = value; }
        }

        private string m_GLClassName = string.Empty;
        public string GLClassName
        {
            get { return this.m_GLClassName; }
            set { this.m_GLClassName = value; }
        }


        public string GLAccountCodeName
        {
            get {
                string codeName = m_GLAccountCode + ", " + m_GLAccountName;
                return codeName; 
            }
        }

        public string GLAccountCodeNameGroup
        {
            get
            {
                string codeName = m_GLAccountCode + ", " + m_GLAccountName + ", " + m_GLGroupName;
                return codeName;
            }
        }

        public string GLAccountCodeNameGroupClass
        {
            get
            {
                string codeName = m_GLAccountCode + ", " + m_GLAccountName + ", " + m_GLGroupName + " (" + m_GLClassName +  ")";
                return codeName;
            }
        }


        private string m_GLAccountNameFull = string.Empty;
        public string GLAccountNameFull
        {
            get { return this.m_GLAccountNameFull; }
            set { this.m_GLAccountNameFull = value; }
        }




        private DateTime? m_LastTranDate = null;
        public DateTime? LastTranDate
        {
            get { return this.m_LastTranDate; }
            set { this.m_LastTranDate = value; }
        }


        private decimal m_TotDebitAmt = 0;
        public decimal TotDebitAmt
        {
            get { return this.m_TotDebitAmt; }
            set { this.m_TotDebitAmt = value; }
        }

        private decimal m_TotCreditAmt = 0;
        public decimal TotCreditAmt
        {
            get { return this.m_TotCreditAmt; }
            set { this.m_TotCreditAmt = value; }
        }


        private decimal m_TotDebitBalanceAmt = 0;
        public decimal TotDebitBalanceAmt
        {
            get { return this.m_TotDebitBalanceAmt; }
            set { this.m_TotDebitBalanceAmt = value; }
        }

        private decimal m_TotCreditBalanceAmt = 0;
        public decimal TotCreditBalanceAmt
        {
            get { return this.m_TotCreditBalanceAmt; }
            set { this.m_TotCreditBalanceAmt = value; }
        }

        private decimal m_TotDebitAmtDisplay = 0;
        public decimal TotDebitAmtDisplay
        {
            get { return this.m_TotDebitAmtDisplay; }
            set { this.m_TotDebitAmtDisplay = value; }
        }

        private decimal m_TotCreditAmtDisplay = 0;
        public decimal TotCreditAmtDisplay
        {
            get { return this.m_TotCreditAmtDisplay; }
            set { this.m_TotCreditAmtDisplay = value; }
        }

        private int m_BalanceType = 0;
        public int BalanceType
        {
            get { return this.m_BalanceType; }
            set { this.m_BalanceType = value; }
        }

        private decimal m_BalanceAmt = 0;
        public decimal BalanceAmt
        {
            get { return this.m_BalanceAmt; }
            set { this.m_BalanceAmt = value; }
        }


        private decimal m_BalanceAmtDisplay = 0;
        public decimal BalanceAmtDisplay
        {
            get { return this.m_BalanceAmtDisplay; }
            set { this.m_BalanceAmtDisplay = value; }
        }


        private int m_DrCrBalance = 0;
        public int DrCrBalance
        {
            get { return this.m_DrCrBalance; }
            set { this.m_DrCrBalance = value; }
        }


        private string m_DrCrText = string.Empty;
        public string DrCrText
        {
            get { return this.m_DrCrText; }
            set { this.m_DrCrText = value; }
        }

        private string m_AccYearName = string.Empty;
        public string AccYearName
        {
            get { return this.m_AccYearName; }
            set { this.m_AccYearName = value; }
        }


        private string m_DateString = string.Empty;
        public string DateString
        {
            get { return this.m_DateString; }
            set { this.m_DateString = value; }
        }


        private int m_AccRefID = 0;
        public int AccRefID
        {
            get { return this.m_AccRefID; }
            set { this.m_AccRefID = value; }
        }

        private string m_AccRefCode = string.Empty;
        public string AccRefCode
        {
            get { return this.m_AccRefCode; }
            set { this.m_AccRefCode = value; }
        }

        private string m_AccRefName = string.Empty;
        public string AccRefName
        {
            get { return this.m_AccRefName; }
            set { this.m_AccRefName = value; }
        }

        private string m_AccRefNameShort = string.Empty;
        public string AccRefNameShort
        {
            get { return this.m_AccRefNameShort; }
            set { this.m_AccRefNameShort = value; }
        }


        private int m_AccRefCategoryID = 0;
        public int AccRefCategoryID
        {
            get { return this.m_AccRefCategoryID; }
            set { this.m_AccRefCategoryID = value; }
        }

        private string m_AccRefCategoryCode = string.Empty;
        public string AccRefCategoryCode
        {
            get { return this.m_AccRefCategoryCode; }
            set { this.m_AccRefCategoryCode = value; }
        }

        private string m_AccRefCategoryName = string.Empty;
        public string AccRefCategoryName
        {
            get { return this.m_AccRefCategoryName; }
            set { this.m_AccRefCategoryName = value; }
        }

        private string m_AccRefCategoryNameShort = string.Empty;
        public string AccRefCategoryNameShort
        {
            get { return this.m_AccRefCategoryNameShort; }
            set { this.m_AccRefCategoryNameShort = value; }
        }


        private int m_AccRefTypeID = 0;
        public int AccRefTypeID
        {
            get { return this.m_AccRefTypeID; }
            set { this.m_AccRefTypeID = value; }
        }

        private string m_AccRefTypeCode = string.Empty;
        public string AccRefTypeCode
        {
            get { return this.m_AccRefTypeCode; }
            set { this.m_AccRefTypeCode = value; }
        }

        private string m_AccRefTypeName = string.Empty;
        public string AccRefTypeName
        {
            get { return this.m_AccRefTypeName; }
            set { this.m_AccRefTypeName = value; }
        }


        private string m_NumberFormat = string.Empty;
        public string NumberFormat
        {
            get { return this.m_NumberFormat; }
            set { this.m_NumberFormat = value; }
        }

        private List<rcLedgerTrans> m_LedgerTrans = new List<rcLedgerTrans>();
        public List<rcLedgerTrans> LedgerTrans
        {
            get { return m_LedgerTrans; }
            set { m_LedgerTrans = value; }
        }



        public List<rcLedger> GetData()
        {
            return new List<rcLedger>();
        }
        
    }

    [Serializable]
    public class rcLedgerTrans
    {
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



        private string m_GLClassName = string.Empty;
        public string GLClassName
        {
            get { return this.m_GLClassName; }
            set { this.m_GLClassName = value; }
        }


        public string GLAccountCodeName
        {
            get
            {
                string codeName = m_GLAccountCode + ", " + m_GLAccountName;
                return codeName;
            }
        }


        private int m_TranSLNo = 0;
        public int TranSLNo
        {
            get { return this.m_TranSLNo; }
            set { this.m_TranSLNo = value; }
        }

        private DateTime? m_TranDate = null;
        public DateTime? TranDate
        {
            get { return this.m_TranDate; }
            set { this.m_TranDate = value; }
        }


        private int m_JournalID = 0;
        public int JournalID
        {
            get { return this.m_JournalID; }
            set { this.m_JournalID = value; }
        }


        private int m_JournalDetID = 0;
        public int JournalDetID
        {
            get { return this.m_JournalDetID; }
            set { this.m_JournalDetID = value; }
        }


        private int m_JournalTypeID = 0;
        public int JournalTypeID
        {
            get { return this.m_JournalTypeID; }
            set { this.m_JournalTypeID = value; }
        }

        private string m_JournalTypeCode = string.Empty;
        public string JournalTypeCode
        {
            get { return this.m_JournalTypeCode; }
            set { this.m_JournalTypeCode = value; }
        }

        private string m_JournalTypeName = string.Empty;
        public string JournalTypeName
        {
            get { return this.m_JournalTypeName; }
            set { this.m_JournalTypeName = value; }
        }

        private string m_JournalNo = string.Empty;
        public string JournalNo
        {
            get { return this.m_JournalNo; }
            set { this.m_JournalNo = value; }
        }

        private string m_JournalNoText = string.Empty;
        public string JournalNoText
        {
            get {

                string jNo = string.Empty;
                jNo = m_JournalTypeCode + m_JournalNo;
                if (!IsPosted)
                {
                    if (jNo != string.Empty)
                    {
                        jNo = "*" + jNo;
                    }
                        
                }

                return jNo;
            }
            //set { this.m_JournalText = value; }
        }


        private int m_TranCodeAccRefID = 0;
        public int TranCodeAccRefID
        {
            get { return this.m_TranCodeAccRefID; }
            set { this.m_TranCodeAccRefID = value; }
        }

        private string m_TranCodeAccRefCode = string.Empty;
        public string TranCodeAccRefCode
        {
            get { return this.m_TranCodeAccRefCode; }
            set { this.m_TranCodeAccRefCode = value; }
        }

        private string m_TranCodeAccRefName = string.Empty;
        public string TranCodeAccRefName
        {
            get { return this.m_TranCodeAccRefName; }
            set { this.m_TranCodeAccRefName = value; }
        }

        //private string m_AccRefCodeTran = string.Empty;
        //public string AccRefCodeTran
        //{
        //    get { return this.m_AccRefCodeTran; }
        //    set { this.m_AccRefCodeTran = value; }
        //}



        private string m_RefNo = string.Empty;
        public string RefNo
        {
            get { return this.m_RefNo; }
            set { this.m_RefNo = value; }
        }


        private string m_TranNote = string.Empty;
        public string TranNote
        {
            get { return this.m_TranNote; }
            set { this.m_TranNote = value; }
        }

        private string m_TranDesc = string.Empty;
        public string TranDesc
        {
            get { return this.m_TranDesc; }
            set { this.m_TranDesc = value; }
        }

        private decimal m_DebitAmt = 0;
        public decimal DebitAmt
        {
            get { return this.m_DebitAmt; }
            set { this.m_DebitAmt = value; }
        }

        private decimal m_CreditAmt = 0;
        public decimal CreditAmt
        {
            get { return this.m_CreditAmt; }
            set { this.m_CreditAmt = value; }
        }


        private decimal m_DebitBalanceAmt = 0;
        public decimal DebitBalanceAmt
        {
            get { return this.m_DebitBalanceAmt; }
            set { this.m_DebitBalanceAmt = value; }
        }

        private decimal m_CreditBalanceAmt = 0;
        public decimal CreditBalanceAmt
        {
            get { return this.m_CreditBalanceAmt; }
            set { this.m_CreditBalanceAmt = value; }
        }

        private decimal m_DebitAmtDisplay = 0;
        public decimal DebitAmtDisplay
        {
            get { return this.m_DebitAmtDisplay; }
            set { this.m_DebitAmtDisplay = value; }
        }

        private decimal m_CreditAmtDisplay = 0;
        public decimal CreditAmtDisplay
        {
            get { return this.m_CreditAmtDisplay; }
            set { this.m_CreditAmtDisplay = value; }
        }

        private decimal m_RunningTotalAmt = 0;
        public decimal RunningTotalAmt
        {
            get { return this.m_RunningTotalAmt; }
            set { this.m_RunningTotalAmt = value; }
        }

        private decimal m_BalanceAmt = 0;
        public decimal BalanceAmt
        {
            get { return this.m_BalanceAmt; }
            set { this.m_BalanceAmt = value; }
        }

        private int m_DrCrBalance = 0;
        public int DrCrBalance
        {
            get { return this.m_DrCrBalance; }
            set { this.m_DrCrBalance = value; }
        }

        private string m_DrCrText = string.Empty;
        public string DrCrText
        {
            get { return this.m_DrCrText; }
            set { this.m_DrCrText = value; }
        }


        private decimal m_BalanceAmtDisplay = 0;
        public decimal BalanceAmtDisplay
        {
            get { return this.m_BalanceAmtDisplay; }
            set { this.m_BalanceAmtDisplay = value; }
        }


        private bool m_IsPosted = false;
        public bool IsPosted
        {
            get { return this.m_IsPosted; }
            set { this.m_IsPosted = value; }
        }

        private bool m_IsReconciled = false;
        public bool IsReconciled
        {
            get { return this.m_IsReconciled; }
            set { this.m_IsReconciled = value; }
        }

        private int m_JournalAdjsutTypeID = 0;
        public int JournalAdjsutTypeID
        {
            get { return this.m_JournalAdjsutTypeID; }
            set { this.m_JournalAdjsutTypeID = value; }
        }

        private int m_TranCodeRefCount = 0;
        public int TranCodeRefCount
        {
            get { return this.m_TranCodeRefCount; }
            set { this.m_TranCodeRefCount = value; }
        }

        private int m_CostCenterRefCount = 0;
        public int CostCenterRefCount
        {
            get { return this.m_CostCenterRefCount; }
            set { this.m_CostCenterRefCount = value; }
        }

        private int m_ReferenceRefCount = 0;
        public int ReferenceRefCount
        {
            get { return this.m_ReferenceRefCount; }
            set { this.m_ReferenceRefCount = value; }
        }

        private GLReportItemTranTypeEnum m_ReportTranType = GLReportItemTranTypeEnum.Tran;
        public GLReportItemTranTypeEnum ReportTranType
        {
            get { return this.m_ReportTranType; }
            set { this.m_ReportTranType = value; }
        }






        private int m_DetCount = 0;
        public int DetCount
        {
            get { return this.m_DetCount; }
            set { this.m_DetCount = value; }
        }

        //istumet


        private int m_DetGroupID = 0;
        public int DetGroupID
        {
            get { return this.m_DetGroupID; }
            set { this.m_DetGroupID = value; }
        }

        private string m_DetTypeName = string.Empty;
        public string DetTypeName
        {
            get { return this.m_DetTypeName; }
            set { this.m_DetTypeName = value; }
        }

        private int m_DetID = 0;
        public int DetID
        {
            get { return this.m_DetID; }
            set { this.m_DetID = value; }
        }


        private string m_DetNo = string.Empty;
        public string DetNo
        {
            get { return this.m_DetNo; }
            set { this.m_DetNo = value; }
        }

        private string m_DetDesc = string.Empty;
        public string DetDesc
        {
            get { return this.m_DetDesc; }
            set { this.m_DetDesc = value; }
        }



        private decimal m_DetAmt = 0;
        public decimal DetAmt
        {
            get { return this.m_DetAmt; }
            set { this.m_DetAmt = value; }
        }

        private decimal m_DetBalanceAmt = 0;
        public decimal DetBalanceAmt
        {
            get { return this.m_DetBalanceAmt; }
            set { this.m_DetBalanceAmt = value; }
        }


        private decimal m_DetAmtDisplay = 0;
        public decimal DetAmtDisplay
        {
            get { return this.m_DetAmtDisplay; }
            set { this.m_DetAmtDisplay = value; }
        }

        private int m_DetAmtDrCr = 0;
        public int DetAmtDrCr
        {
            get { return this.m_DetAmtDrCr; }
            set { this.m_DetAmtDrCr = value; }
        }

        private string m_DetAmtDrCrText = string.Empty;
        public string DetAmtDrCrText
        {
            get { return this.m_DetAmtDrCrText; }
            set { this.m_DetAmtDrCrText = value; }
        }




        private int m_AccRefID = 0;
        public int AccRefID
        {
            get { return this.m_AccRefID; }
            set { this.m_AccRefID = value; }
        }

        private string m_AccRefCode = string.Empty;
        public string AccRefCode
        {
            get { return this.m_AccRefCode; }
            set { this.m_AccRefCode = value; }
        }

        private string m_AccRefName = string.Empty;
        public string AccRefName
        {
            get { return this.m_AccRefName; }
            set { this.m_AccRefName = value; }
        }

        private string m_AccRefNameShort = string.Empty;
        public string AccRefNameShort
        {
            get { return this.m_AccRefNameShort; }
            set { this.m_AccRefNameShort = value; }
        }


        private int m_AccRefCategoryID = 0;
        public int AccRefCategoryID
        {
            get { return this.m_AccRefCategoryID; }
            set { this.m_AccRefCategoryID = value; }
        }

        private string m_AccRefCategoryCode = string.Empty;
        public string AccRefCategoryCode
        {
            get { return this.m_AccRefCategoryCode; }
            set { this.m_AccRefCategoryCode = value; }
        }

        private string m_AccRefCategoryName = string.Empty;
        public string AccRefCategoryName
        {
            get { return this.m_AccRefCategoryName; }
            set { this.m_AccRefCategoryName = value; }
        }

        private string m_AccRefCategoryNameShort = string.Empty;
        public string AccRefCategoryNameShort
        {
            get { return this.m_AccRefCategoryNameShort; }
            set { this.m_AccRefCategoryNameShort = value; }
        }


        private int m_AccRefTypeID = 0;
        public int AccRefTypeID
        {
            get { return this.m_AccRefTypeID; }
            set { this.m_AccRefTypeID = value; }
        }

        private string m_AccRefTypeCode = string.Empty;
        public string AccRefTypeCode
        {
            get { return this.m_AccRefTypeCode; }
            set { this.m_AccRefTypeCode = value; }
        }

        private string m_AccRefTypeName = string.Empty;
        public string AccRefTypeName
        {
            get { return this.m_AccRefTypeName; }
            set { this.m_AccRefTypeName = value; }
        }










        private string m_NumberFormat = string.Empty;
        public string NumberFormat
        {
            get { return this.m_NumberFormat; }
            set { this.m_NumberFormat = value; }
        }





        public List<rcLedgerTrans> GetData()
        {
            return new List<rcLedgerTrans>();
        }

    }
}
