using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PG.Report.ReportEnums;

namespace PG.Report.ReportClass.AccountingRC
{
    [Serializable]
    public class rcJournal
    {
        private int m_JournalID = 0;
        public int JournalID
        {
            get { return this.m_JournalID; }
            set { this.m_JournalID = value; }
        }

        private string m_JournalNo = string.Empty;
        public string JournalNo
        {
            get { return this.m_JournalNo; }
            set { this.m_JournalNo = value; }
        }

        private int m_CompanyID = 0;
        public int CompanyID
        {
            get { return this.m_CompanyID; }
            set { this.m_CompanyID = value; }
        }

        private string m_CompanyName = string.Empty;
        public string CompanyName
        {
            get { return this.m_CompanyName; }
            set { this.m_CompanyName = value; }
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


        private DateTime? m_JournalDate = null;
        public DateTime? JournalDate
        {
            get { return this.m_JournalDate; }
            set { this.m_JournalDate = value; }
        }

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

        private string m_JournalDesc = string.Empty;
        public string JournalDesc
        {
            get { return this.m_JournalDesc; }
            set { this.m_JournalDesc = value; }
        }


        private bool m_IsPosted = false;
        public bool IsPosted
        {
            get { return this.m_IsPosted; }
            set { this.m_IsPosted = value; }
        }


        private int m_JournalAdjustTypeID = 0;
        public int JournalAdjustTypeID
        {
            get { return this.m_JournalAdjustTypeID; }
            set { this.m_JournalAdjustTypeID = value; }
        }

        private int m_JournalIDAdjust = 0;
        public int JournalIDAdjust
        {
            get { return this.m_JournalIDAdjust; }
            set { this.m_JournalIDAdjust = value; }
        }


        private bool m_IsDeleted = false;
        public bool IsDeleted
        {
            get { return this.m_IsDeleted; }
            set { this.m_IsDeleted = value; }
        }

        private decimal m_JournalAmt = 0;
        public decimal JournalAmt
        {
            get { return this.m_JournalAmt; }
            set { this.m_JournalAmt = value; }
        }


        private string m_JournalAmtInWord = string.Empty;
        public string JournalAmtInWord
        {
            get { return this.m_JournalAmtInWord; }
            set { this.m_JournalAmtInWord = value; }
        }


        private string m_EditUserName = string.Empty;
        public string EditUserName
        {
            get { return this.m_EditUserName; }
            set { this.m_EditUserName = value; }
        }

        private string m_EditUserNameFull = string.Empty;
        public string EditUserNameFull
        {
            get { return this.m_EditUserNameFull; }
            set { this.m_EditUserNameFull = value; }
        }

        private List<rcJournalTrans> m_JournalTrans = new List<rcJournalTrans>();
        public List<rcJournalTrans> JournalTrans
        {
            get { return m_JournalTrans; }
            set { m_JournalTrans = value; }
        }




        public List<rcJournal> GetData()
        {
            return new List<rcJournal>();
        }

        //TODO Change Monir
        private string m_LocationName = string.Empty;
        public string LocationName
        {
            get { return this.m_LocationName; }
            set { this.m_LocationName = value; }
        }
        private string m_LocationCode = string.Empty;
        public string LocationCode
        {
            get { return m_LocationCode; }
            set { this.m_LocationCode = value; }
        }
    }

    [Serializable]
    public class rcJournalTrans
    {
        private int m_JournalDetID = 0;
        public int JournalDetID
        {
            get { return this.m_JournalDetID; }
            set { this.m_JournalDetID = value; }
        }


        private int m_DrCr = 0;
        public int DrCr
        {
            get { return this.m_DrCr; }
            set { this.m_DrCr = value; }
        }


        private int m_JournalID = 0;
        public int JournalID
        {
            get { return this.m_JournalID; }
            set { this.m_JournalID = value; }
        }





        private string m_JournalNo = string.Empty;
        public string JournalNo
        {
            get { return this.m_JournalNo; }
            set { this.m_JournalNo = value; }
        }

        private int m_CompanyID = 0;
        public int CompanyID
        {
            get { return this.m_CompanyID; }
            set { this.m_CompanyID = value; }
        }

        private DateTime? m_JournalDate = null;
        public DateTime? JournalDate
        {
            get { return this.m_JournalDate; }
            set { this.m_JournalDate = value; }
        }

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

        private string m_JournalDesc = string.Empty;
        public string JournalDesc
        {
            get { return this.m_JournalDesc; }
            set { this.m_JournalDesc = value; }
        }

        private bool m_IsPosted = false;
        public bool IsPosted
        {
            get { return this.m_IsPosted; }
            set { this.m_IsPosted = value; }
        }

        private int m_JournalDetSLNo = 0;
        public int JournalDetSLNo
        {
            get { return this.m_JournalDetSLNo; }
            set { this.m_JournalDetSLNo = value; }
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

        private int m_GLAccountTypeID = 0;
        public int GLAccountTypeID
        {
            get { return this.m_GLAccountTypeID; }
            set { this.m_GLAccountTypeID = value; }
        }


        private int m_GLAccountIDParent = 0;
        public int GLAccountIDParent
        {
            get { return this.m_GLAccountIDParent; }
            set { this.m_GLAccountIDParent = value; }
        }


        private int m_GLGroupID = 0;
        public int GLGroupID
        {
            get { return this.m_GLGroupID; }
            set { this.m_GLGroupID = value; }
        }


        private string m_GLGroupCode = string.Empty;
        public string GLGroupCode
        {
            get { return this.m_GLGroupCode; }
            set { this.m_GLGroupCode = value; }
        }

        private string m_GLGroupName = string.Empty;
        public string GLGroupName
        {
            get { return this.m_GLGroupName; }
            set { this.m_GLGroupName = value; }
        }

        private string m_GLGroupNameShort = string.Empty;
        public string GLGroupNameShort
        {
            get { return this.m_GLGroupNameShort; }
            set { this.m_GLGroupNameShort = value; }
        }

        private int m_GLGroupIDParent = 0;
        public int GLGroupIDParent
        {
            get { return this.m_GLGroupIDParent; }
            set { this.m_GLGroupIDParent = value; }
        }


        private int m_GLClassID = 0;
        public int GLClassID
        {
            get { return this.m_GLClassID; }
            set { this.m_GLClassID = value; }
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

        private string m_AccRefCodeTran = string.Empty;
        public string AccRefCodeTran
        {
            get { return this.m_AccRefCodeTran; }
            set { this.m_AccRefCodeTran = value; }
        }

        private string m_RefNo = string.Empty;
        public string RefNo
        {
            get { return this.m_RefNo; }
            set { this.m_RefNo = value; }
        }

        private string m_JournalDetNote = string.Empty;
        public string JournalDetNote
        {
            get { return this.m_JournalDetNote; }
            set { this.m_JournalDetNote = value; }
        }

        private string m_JournalDetDesc = string.Empty;
        public string JournalDetDesc
        {
            get { return this.m_JournalDetDesc; }
            set { this.m_JournalDetDesc = value; }
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

        private decimal m_TotalAmt = 0;
        public decimal TotalAmt
        {
            get { return this.m_TotalAmt; }
            set { this.m_TotalAmt = value; }
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

        private int m_DetGroupID = 0;
        public int DetGroupID
        {
            get { return this.m_DetGroupID; }
            set { this.m_DetGroupID = value; }
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


        private string m_JournalDetInsText = string.Empty;
        public string JournalDetInsText
        {
            get { return this.m_JournalDetInsText; }
            set { this.m_JournalDetInsText = value; }
        }


        private string m_JournalDetCostCenterText = string.Empty;
        public string JournalDetCostCenterText
        {
            get { return this.m_JournalDetCostCenterText; }
            set { this.m_JournalDetCostCenterText = value; }
        }


        //contra

        private int m_GLAccountIDContra = 0;
        public int GLAccountIDContra
        {
            get { return this.m_GLAccountIDContra; }
            set { this.m_GLAccountIDContra = value; }
        }


        private string m_GLAccountCodeContra = string.Empty;
        public string GLAccountCodeContra
        {
            get { return this.m_GLAccountCodeContra; }
            set { this.m_GLAccountCodeContra = value; }
        }

        private string m_GLAccountNameContra = string.Empty;
        public string GLAccountNameContra
        {
            get { return this.m_GLAccountNameContra; }
            set { this.m_GLAccountNameContra = value; }
        }

        private int m_GLGroupIDContra = 0;
        public int GLGroupIDContra
        {
            get { return this.m_GLGroupIDContra; }
            set { this.m_GLGroupIDContra = value; }
        }


        private string m_GLGroupCodeContra = string.Empty;
        public string GLGroupCodeContra
        {
            get { return this.m_GLGroupCodeContra; }
            set { this.m_GLGroupCodeContra = value; }
        }

        private string m_GLGroupNameContra = string.Empty;
        public string GLGroupNameContra
        {
            get { return this.m_GLGroupNameContra; }
            set { this.m_GLGroupNameContra = value; }
        }

        private string m_GLGroupNameShortContra = string.Empty;
        public string GLGroupNameShortContra
        {
            get { return this.m_GLGroupNameShortContra; }
            set { this.m_GLGroupNameShortContra = value; }
        }


        private string m_DebitAmtInWord = string.Empty;
        public string DebitAmtInWord
        {
            get { return this.m_DebitAmtInWord; }
            set { this.m_DebitAmtInWord = value; }
        }


        private string m_CreditAmtInWord = string.Empty;
        public string CreditAmtInWord
        {
            get { return this.m_CreditAmtInWord; }
            set { this.m_CreditAmtInWord = value; }
        }



        private string m_EditUserName = string.Empty;
        public string EditUserName
        {
            get { return this.m_EditUserName; }
            set { this.m_EditUserName = value; }
        }


        private string m_EditUserNameFull = string.Empty;
        public string EditUserNameFull
        {
            get { return this.m_EditUserNameFull; }
            set { this.m_EditUserNameFull = value; }
        }


        private string m_JournalDetInsTextContra = string.Empty;
        public string JournalDetInsTextContra
        {
            get { return this.m_JournalDetInsTextContra; }
            set { this.m_JournalDetInsTextContra = value; }
        }

        //cost ceter 

        //referce
        private string m_LocationName = string.Empty;
        public string LocationName
        {
            get { return this.m_LocationName; }
            set { this.m_LocationName = value; }
        }

        public List<rcJournalTrans> GetData()
        {
            return new List<rcJournalTrans>();
        }

    }
}
