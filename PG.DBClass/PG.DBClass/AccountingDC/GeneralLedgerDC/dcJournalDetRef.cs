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
    [DBTable(Name = "tblJournalDetRef")]
    public partial class dcJournalDetRef : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_JournalDetRefID = 0;
        private int m_JournalDetID = 0;
        private int m_JournalDetRefSLNo = 0;
        private int m_AccRefID = 0;
        private int m_DrCr = 0;
        private decimal m_DebitAmt = 0;
        private decimal m_CreditAmt = 0;
        private decimal m_TranAmt = 0;
        private string m_AccRefNo = string.Empty;
        private string m_AccRefRemarks = string.Empty;

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


        [DBColumn(Name = "JournalDetRefID", Storage = "m_JournalDetRefID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity=true)]
        public int JournalDetRefID
        {
            get { return this.m_JournalDetRefID; }
            set
            {
                this.m_JournalDetRefID = value;
                this.NotifyPropertyChanged("JournalDetRefID");
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

        [DBColumn(Name = "JournalDetRefSLNo", Storage = "m_JournalDetRefSLNo", DbType = "Int NULL")]
        public int JournalDetRefSLNo
        {
            get { return this.m_JournalDetRefSLNo; }
            set
            {
                this.m_JournalDetRefSLNo = value;
                this.NotifyPropertyChanged("JournalDetRefSLNo");
            }
        }

        [DBColumn(Name = "AccRefID", Storage = "m_AccRefID", DbType = "Int NULL")]
        public int AccRefID
        {
            get { return this.m_AccRefID; }
            set
            {
                this.m_AccRefID = value;
                this.NotifyPropertyChanged("AccRefID");
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

        [DBColumn(Name = "AccRefNo", Storage = "m_AccRefNo", DbType = "NVarChar(100) NULL")]
        public string AccRefNo
        {
            get { return this.m_AccRefNo; }
            set
            {
                this.m_AccRefNo = value;
                this.NotifyPropertyChanged("AccRefNo");
            }
        }

        [DBColumn(Name = "AccRefRemarks", Storage = "m_AccRefRemarks", DbType = "NVarChar(500) NULL")]
        public string AccRefRemarks
        {
            get { return this.m_AccRefRemarks; }
            set
            {
                this.m_AccRefRemarks = value;
                this.NotifyPropertyChanged("AccRefRemarks");
            }
        }

        #endregion //properties
    }
   
    public partial class dcJournalDetRef : DBBaseClass, INotifyPropertyChanged
    {
        //used for grid ui - master details link
        private int m_JournalDetID_Link = 0;
        public int JournalDetID_Link
        {
            get { return m_JournalDetID_Link; }
            set { m_JournalDetID_Link = value; }
        }


        private string m_AccRefCode = string.Empty;
        public string AccRefCode
        {
            get { return m_AccRefCode; }
            set { m_AccRefCode = value; }
        }

        private string m_AccRefName = string.Empty;
        public string AccRefName
        {
            get { return m_AccRefName; }
            set { m_AccRefName = value; }
        }


        private int m_AccRefCategoryID = 0;
        public int AccRefCategoryID
        {
            get { return m_AccRefCategoryID; }
            set { m_AccRefCategoryID = value; }
        }


        private string m_AccRefCategoryCode = string.Empty;
        public string AccRefCategoryCode
        {
            get { return m_AccRefCategoryCode; }
            set { m_AccRefCategoryCode = value; }
        }

        private string m_AccRefCategoryName = string.Empty;
        public string AccRefCategoryName
        {
            get { return m_AccRefCategoryName; }
            set { m_AccRefCategoryName = value; }
        }

        private int m_AccRefTypeID = 0;
        public int AccRefTypeID
        {
            get { return m_AccRefTypeID; }
            set { m_AccRefTypeID = value; }
        }

        private string m_AccRefTypeCode = string.Empty;
        public string AccRefTypeCode
        {
            get { return m_AccRefTypeCode; }
            set { m_AccRefTypeCode = value; }
        }

        private string m_AccRefTypeName = string.Empty;
        public string AccRefTypeName
        {
            get { return m_AccRefTypeName; }
            set { m_AccRefTypeName = value; }
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


        private int m_JournalTypeID = 0;
        public int JournalTypeID
        {
            get { return m_JournalTypeID; }
            set { m_JournalTypeID = value; }
        }

        private string m_JournalTypeCode = string.Empty;
        public string JournalTypeCode
        {
            get { return m_JournalTypeCode; }
            set { m_JournalTypeCode = value; }
        }

        private string m_JournalTypeName = string.Empty;
        public string JournalTypeName
        {
            get { return m_JournalTypeName; }
            set { m_JournalTypeName = value; }
        }

       
        private bool m_IsPosted = false;
        public bool IsPosted
        {
            get { return m_IsPosted; }
            set { m_IsPosted = value; }
        }

        private int m_JournalAdjustTypeID = 0;
        public int JournalAdjustTypeID
        {
            get { return m_JournalAdjustTypeID; }
            set { m_JournalAdjustTypeID = value; }
        }

        private string m_JournalDesc = string.Empty;
        public string JournalDesc
        {
            get { return m_JournalDesc; }
            set { m_JournalDesc = value; }
        }

        private string m_JournalDetDesc = string.Empty;
        public string JournalDetDesc
        {
            get { return m_JournalDetDesc; }
            set { m_JournalDetDesc = value; }
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


        private int m_GLAccountTypeID = 0;
        public int GLAccountTypeID
        {
            get { return m_GLAccountTypeID; }
            set { m_GLAccountTypeID = value; }
        }

        private int m_GLAccountIDParent = 0;
        public int GLAccountIDParent
        {
            get { return m_GLAccountIDParent; }
            set { m_GLAccountIDParent = value; }
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


        private decimal m_Amt = 0;
        public decimal Amt
        {
            get { return m_Amt; }
            set { m_Amt = value; }
        }


    }


}
