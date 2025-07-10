using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using PG.Core.DBBase;

namespace PG.DBClass.AccountingDC.GeneralLedgerDC
{
    [DBTable(Name = "tblJournalDetIns")]
    public partial class dcJournalDetIns : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_JournalDetInsID = 0;
        private int m_JournalDetID = 0;
        private int m_JournalDetInsSLNo = 0;
        private int m_InstrumentLinkTypeID = 0;
        private int m_InstrumentID = 0;
        private int m_DrCr = 0;
        private decimal m_DebitAmt = 0;
        private decimal m_CreditAmt = 0;
        private decimal m_TranAmt = 0;
        private decimal m_InsTranAmt = 0;
        private string m_Remarks = string.Empty;

        #endregion  //private members

        #region public events

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            _UpdateChangedList(info);
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        #endregion //public events

        #region properties


        [DBColumn(Name = "JournalDetInsID", Storage = "m_JournalDetInsID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int JournalDetInsID
        {
            get { return this.m_JournalDetInsID; }
            set
            {
                this.m_JournalDetInsID = value;
                this.NotifyPropertyChanged("JournalDetInsID");
            }
        }

        [DBColumn(Name = "JournalDetID", Storage = "m_JournalDetID", DbType = "Int NULL")]
        public int JournalDetID
        {
            get { return this.m_JournalDetID; }
            set
            {
                this.m_JournalDetID = value;
                this.NotifyPropertyChanged("JournalDetID");
            }
        }

        [DBColumn(Name = "JournalDetInsSLNo", Storage = "m_JournalDetInsSLNo", DbType = "Int NULL")]
        public int JournalDetInsSLNo
        {
            get { return this.m_JournalDetInsSLNo; }
            set
            {
                this.m_JournalDetInsSLNo = value;
                this.NotifyPropertyChanged("JournalDetInsSLNo");
            }
        }

        [DBColumn(Name = "InstrumentLinkTypeID", Storage = "m_InstrumentLinkTypeID", DbType = "Int NULL")]
        public int InstrumentLinkTypeID
        {
            get { return this.m_InstrumentLinkTypeID; }
            set
            {
                this.m_InstrumentLinkTypeID = value;
                this.NotifyPropertyChanged("InstrumentLinkTypeID");
            }
        }

        [DBColumn(Name = "InstrumentID", Storage = "m_InstrumentID", DbType = "Int NULL")]
        public int InstrumentID
        {
            get { return this.m_InstrumentID; }
            set
            {
                this.m_InstrumentID = value;
                this.NotifyPropertyChanged("InstrumentID");
            }
        }

        [DBColumn(Name = "DrCr", Storage = "m_DrCr", DbType = "Int NULL")]
        public int DrCr
        {
            get { return this.m_DrCr; }
            set
            {
                this.m_DrCr = value;
                this.NotifyPropertyChanged("DrCr");
            }
        }

        [DBColumn(Name = "DebitAmt", Storage = "m_DebitAmt", DbType = "Money NULL")]
        public decimal DebitAmt
        {
            get { return this.m_DebitAmt; }
            set
            {
                this.m_DebitAmt = value;
                this.NotifyPropertyChanged("DebitAmt");
            }
        }

        [DBColumn(Name = "CreditAmt", Storage = "m_CreditAmt", DbType = "Money NULL")]
        public decimal CreditAmt
        {
            get { return this.m_CreditAmt; }
            set
            {
                this.m_CreditAmt = value;
                this.NotifyPropertyChanged("CreditAmt");
            }
        }

        [DBColumn(Name = "TranAmt", Storage = "m_TranAmt", DbType = "Money NULL")]
        public decimal TranAmt
        {
            get { return this.m_TranAmt; }
            set
            {
                this.m_TranAmt = value;
                this.NotifyPropertyChanged("TranAmt");
            }
        }

        [DBColumn(Name = "InsTranAmt", Storage = "m_InsTranAmt", DbType = "Money NULL")]
        public decimal InsTranAmt
        {
            get { return this.m_InsTranAmt; }
            set
            {
                this.m_InsTranAmt = value;
                this.NotifyPropertyChanged("InsTranAmt");
            }
        }

        [DBColumn(Name = "Remarks", Storage = "m_Remarks", DbType = "NVarChar(200) NULL")]
        public string Remarks
        {
            get { return this.m_Remarks; }
            set
            {
                this.m_Remarks = value;
                this.NotifyPropertyChanged("Remarks");
            }
        }

        #endregion //properties
    }


    public partial class dcJournalDetIns
    {
        private int m_JournalDetID_Link = 0;
        public int JournalDetID_Link
        {
            get { return m_JournalDetID_Link; }
            set { m_JournalDetID_Link = value; }
        }

        private string m_InstrumentNo = string.Empty;
        public string InstrumentNo
        {
            get { return m_InstrumentNo; }
            set { m_InstrumentNo = value; }
        }

        private DateTime? m_InstrumentDate = null;
        public DateTime? InstrumentDate
        {
            get { return m_InstrumentDate; }
            set { m_InstrumentDate = value; }
        }

        private decimal m_InstrumentAmt = 0;
        public decimal InstrumentAmt
        {
            get { return m_InstrumentAmt; }
            set { m_InstrumentAmt = value; }
        }

        private int m_InstrumentModeID = 0;
        public int InstrumentModeID
        {
            get { return m_InstrumentModeID; }
            set { m_InstrumentModeID = value; }
        }

        private string m_InsrumentModeName = string.Empty;
        public string InsrumentModeName
        {
            get { return m_InsrumentModeName; }
            set { m_InsrumentModeName = value; }
        }

        private int m_InstrumentTypeID = 0;
        public int InstrumentTypeID
        {
            get { return m_InstrumentTypeID; }
            set { m_InstrumentTypeID = value; }
        }

        private string m_InstrumentTypeName = string.Empty;
        public string InstrumentTypeName
        {
            get { return m_InstrumentTypeName; }
            set { m_InstrumentTypeName = value; }
        }

        private int m_InstrumentStatusID = 0;
        public int InstrumentStatusID
        {
            get { return m_InstrumentStatusID; }
            set { m_InstrumentStatusID = value; }
        }

        private string m_InstrumentStatusName = string.Empty;
        public string InstrumentStatusName
        {
            get { return m_InstrumentStatusName; }
            set { m_InstrumentStatusName = value; }
        }


        private string m_IssueName = string.Empty;
        public string IssueName
        {
            get { return m_IssueName; }
            set { m_IssueName = value; }
        }

        private string m_BankName = string.Empty;
        public string BankName
        {
            get { return m_BankName; }
            set { m_BankName = value; }
        }

        private string m_BranchName = string.Empty;
        public string BranchName
        {
            get { return m_BranchName; }
            set { m_BranchName = value; }
        }


        private int m_CompanyID = 0;
        public int CompanyID
        {
            get { return m_CompanyID; }
            set { m_CompanyID = value; }
        }


        private int m_JournalID = 0;
        public int JournalID
        {
            get { return m_JournalID; }
            set { m_JournalID = value; }
        }

        private string m_JournalNo = string.Empty;
        public string JournalNo
        {
            get { return m_JournalNo; }
            set { m_JournalNo = value; }
        }

        private DateTime? m_JournalDate = null;
        public DateTime? JournalDate
        {
            get { return m_JournalDate; }
            set { m_JournalDate = value; }
        }

        private bool m_IsPosted = false;
        public bool IsPosted
        {
            get { return m_IsPosted; }
            set { m_IsPosted = value; }
        }


        private string m_JournalDetDesc = string.Empty;
        public string JournalDetDesc
        {
            get { return m_JournalDetDesc; }
            set { m_JournalDetDesc = value; }
        }

        private int m_JournalTypeID = 0;
        public int JournalTypeID
        {
            get { return m_JournalTypeID; }
            set { m_JournalTypeID = value; }
        }

        private string m_JournalTypeName = string.Empty;
        public string JournalTypeName
        {
            get { return m_JournalTypeName; }
            set { m_JournalTypeName = value; }
        }


        private int m_GLAccountID = 0;
        public int GLAccountID
        {
            get { return m_GLAccountID; }
            set { m_GLAccountID = value; }
        }

        private string m_GLAccountCode = string.Empty;
        public string GLAccountCode
        {
            get { return m_GLAccountCode; }
            set { m_GLAccountCode = value; }
        }


        private string m_GLAccountName = string.Empty;
        public string GLAccountName
        {
            get { return m_GLAccountName; }
            set { m_GLAccountName = value; }
        }

        private string m_GLAccountNameDisplay = string.Empty;
        public string GLAccountNameDisplay
        {
            get {
                string glAccName = m_GLAccountCode;
                glAccName += ", " + m_GLAccountName;
                glAccName += ", " + m_GLGroupNameShort;

                glAccName = m_GLAccountNameDisplay == string.Empty ? glAccName : m_GLAccountNameDisplay;

                return glAccName ; }
            set { m_GLAccountNameDisplay = value; }
        }



        private string m_JournalDetDescDisplay = string.Empty;
        public string JournalDetDescDisplay
        {
            get
            {
                string jrDetDesc = m_JournalDetDesc;
                jrDetDesc = GLAccountNameDisplay + jrDetDesc == string.Empty ? "" : GLAccountNameDisplay + "\n\r" + jrDetDesc;

                return jrDetDesc;
            }
            set { m_JournalDetDescDisplay = value; }
        }

        private int m_GLAccountTypeID = 0;
        public int GLAccountTypeID
        {
            get { return m_GLAccountTypeID; }
            set { m_GLAccountTypeID = value; }
        }

        private int m_GLAccountParent = 0;
        public int GLAccountParent
        {
            get { return m_GLAccountParent; }
            set { m_GLAccountParent = value; }
        }

        private int m_GLGroupID = 0;
        public int GLGroupID
        {
            get { return m_GLGroupID; }
            set { m_GLGroupID = value; }
        }

        private string m_GLGroupCode = string.Empty;
        public string GLGroupCode
        {
            get { return m_GLGroupCode; }
            set { m_GLGroupCode = value; }
        }


        private string m_GLGroupName = string.Empty;
        public string GLGroupName
        {
            get { return m_GLGroupName; }
            set { m_GLGroupName = value; }
        }

        private string m_GLGroupNameShort = string.Empty;
        public string GLGroupNameShort
        {
            get { return m_GLGroupNameShort; }
            set { m_GLGroupNameShort = value; }
        }

    }
}
