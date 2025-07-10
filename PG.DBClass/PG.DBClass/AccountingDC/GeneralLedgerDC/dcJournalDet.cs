using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using PG.Core.DBBase;
using PG.DBClass.AccountingDC.AccEnums;

namespace PG.DBClass.AccountingDC.GeneralLedgerDC
{
    [DBTable(Name = "tblJournalDet")]
    public partial class dcJournalDet : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_JournalDetID = 0;
        private int m_JournalID = 0;
        private int m_GLAccountID = 0;
        private int m_JournalDetSLNo = 0;
        private string m_JournalDetNote = string.Empty;
        private string m_JournalDetDesc = string.Empty;
        private string m_RefNo = string.Empty;
        private int m_DrCr = 0;
        private decimal m_DebitAmt = 0;
        private decimal m_CreditAmt = 0;
        private decimal m_TranAmt = 0;
        private bool m_IsReconciled = false;
        private DateTime? m_ReconcileDate = null;
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


        [DBColumn(Name = "JournalDetID", Storage = "m_JournalDetID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity=true)]
        public int JournalDetID
        {
            get { return this.m_JournalDetID; }
            set
            {
                this.m_JournalDetID = value;
                this.NotifyPropertyChanged("JournalDetID");
            }
        }

        [DBColumn(Name = "JournalID", Storage = "m_JournalID", DbType = "Int NULL")]
        public int JournalID
        {
            get { return this.m_JournalID; }
            set
            {
                this.m_JournalID = value;
                this.NotifyPropertyChanged("JournalID");
            }
        }

        [DBColumn(Name = "GLAccountID", Storage = "m_GLAccountID", DbType = "Int NULL")]
        public int GLAccountID
        {
            get { return this.m_GLAccountID; }
            set
            {
                this.m_GLAccountID = value;
                this.NotifyPropertyChanged("GLAccountID");
            }
        }

        [DBColumn(Name = "JournalDetSLNo", Storage = "m_JournalDetSLNo", DbType = "Int NULL")]
        public int JournalDetSLNo
        {
            get { return this.m_JournalDetSLNo; }
            set
            {
                this.m_JournalDetSLNo = value;
                this.NotifyPropertyChanged("JournalDetSLNo");
            }
        }

        [DBColumn(Name = "JournalDetNote", Storage = "m_JournalDetNote", DbType = "NVarChar(100) NULL")]
        public string JournalDetNote
        {
            get { return this.m_JournalDetNote; }
            set
            {
                this.m_JournalDetNote = value;
                this.NotifyPropertyChanged("JournalDetNote");
            }
        }

        [DBColumn(Name = "JournalDetDesc", Storage = "m_JournalDetDesc", DbType = "NVarChar(200) NULL")]
        public string JournalDetDesc
        {
            get { return this.m_JournalDetDesc; }
            set
            {
                this.m_JournalDetDesc = value;
                this.NotifyPropertyChanged("JournalDetDesc");
            }
        }

        [DBColumn(Name = "RefNo", Storage = "m_RefNo", DbType = "NVarChar(100) NULL")]
        public string RefNo
        {
            get { return this.m_RefNo; }
            set
            {
                this.m_RefNo = value;
                this.NotifyPropertyChanged("RefNo");
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


        [DBColumn(Name = "IsReconciled", Storage = "m_IsReconciled", DbType = "Bit NULL")]
        public bool IsReconciled
        {
            get { return this.m_IsReconciled; }
            set
            {
                this.m_IsReconciled = value;
                this.NotifyPropertyChanged("IsReconciled");
            }
        }

        [DBColumn(Name = "ReconcileDate", Storage = "m_ReconcileDate", DbType = "DateTime NULL")]
        public DateTime? ReconcileDate
        {
            get { return this.m_ReconcileDate; }
            set
            {
                this.m_ReconcileDate = value;
                this.NotifyPropertyChanged("ReconcileDate");
            }
        }

        [DBColumn(Name = "Remarks", Storage = "m_Remarks", DbType = "NVarChar(100) NULL")]
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

    public partial class dcJournalDet
    {
        #region Association

        private EntityRef<dcJournal> m_Journal = new EntityRef<dcJournal>();
        [Association(Name = "FK_JournalDet_Journal", Storage = "m_Journal", ThisKey = "JournalID", OtherKey = "JournalID", IsForeignKey = true)]
        public dcJournal Journal
        {
            get { return m_Journal.Entity; }
            set { m_Journal.Entity = value; }
        }

        
        #endregion

        #region custom properties

        private string m_JournalNo = string.Empty;
        public string JournalNo
        {
            get { return m_JournalNo; }
            set { this.m_JournalNo = value; }
        }

        private DateTime? m_JournalDate = null ;
        public DateTime? JournalDate
        {
            get { return m_JournalDate; }
            set { this.m_JournalDate = value; }
        }

        private int m_JournalTypeID= 0;
        public int JournalTypeID
        {
            get { return m_JournalTypeID; }
            set { this.m_JournalTypeID = value; }
        }

        private string m_JournalTypeCode = string.Empty;
        public string JournalTypeCode
        {
            get { return m_JournalTypeCode; }
            set { this.m_JournalTypeCode = value; }
        }

        private string m_JournalTypeName = string.Empty;
        public string JournalTypeName
        {
            get { return m_JournalTypeName; }
            set { this.m_JournalTypeName = value; }
        }

        private bool m_IsPosted = false;
        public bool IsPosted
        {
            get { return m_IsPosted; }
            set { this.m_IsPosted = value; }
        }

        private int m_JournalAdjustTypeID = 0;
        public int JournalAdjustTypeID
        {
            get { return m_JournalAdjustTypeID; }
            set { this.m_JournalAdjustTypeID = value; }
        }

        private int m_JournalIDAdjust = 0;
        public int JournalIDAdjust
        {
            get { return m_JournalIDAdjust; }
            set { this.m_JournalIDAdjust = value; }
        }

        private string m_EditUserName = string.Empty;
        public string EditUserName
        {
            get { return m_EditUserName; }
            set { this.m_EditUserName = value; }
        }

        public DebitCreditEnum DebitCredit
        {
            get { return (DebitCreditEnum)m_DrCr; }
            set { m_DrCr = (int)value; }
        }

        private int m_GLAccountIDEdit = 0;
        public int GLAccountIDEdit
        {
            get { return m_GLAccountIDEdit; }
            set { this.m_GLAccountIDEdit = value; }
        }

        private string m_GLAccountCode = string.Empty;
        public string GLAccountCode
        {
            get { return m_GLAccountCode; }
            set { this.m_GLAccountCode = value; }
        }

        private string m_GLAccountName = string.Empty;
        public string GLAccountName
        {
            get { return m_GLAccountName; }
            set { this.m_GLAccountName = value; }
        }


        private int m_GLAccountTypeID = 0;
        public int GLAccountTypeID
        {
            get { return m_GLAccountTypeID; }
            set { this.m_GLAccountTypeID = value; }
        }

        private int m_GLAccountIDParent = 0;
        public int GLAccountIDParent
        {
            get { return m_GLAccountIDParent; }
            set { this.m_GLAccountIDParent = value; }
        }


        private int m_GLGroupID = 0;
        public int GLGroupID
        {
            get { return m_GLGroupID; }
            set { this.m_GLGroupID = value; }
        }

        private int m_GLGroupIDEdit = 0;
        public int GLGroupIDEdit
        {
            get { return m_GLGroupIDEdit; }
            set { this.m_GLGroupIDEdit = value; }
        }


        private string m_GLGroupCode = string.Empty;
        public string GLGroupCode
        {
            get { return m_GLGroupCode; }
            set { this.m_GLGroupCode = value; }
        }

        private string m_GLGroupNameShort = string.Empty;
        public string GLGroupNameShort
        {
            get { return m_GLGroupNameShort; }
            set { this.m_GLGroupNameShort = value; }
        }


        private string m_GLGroupName = string.Empty;
        public string GLGroupName
        {
            get { return m_GLGroupName; }
            set { this.m_GLGroupName = value; }
        }

        public string GLGroupNameShortName
        {
            get
            {
                string s = string.Empty;
                if (m_GLGroupNameShort != string.Empty)
                {
                    s = m_GLGroupNameShort + ", " + m_GLGroupName;
                }
                else
                {
                    s = m_GLGroupName;
                }
                return s;
            }
        }


        private int m_GLClassID = 0;
        public int GLClassID
        {
            get { return m_GLClassID; }
            set { this.m_GLClassID = value; }
        }

        private int m_GLGroupClassID = 0;
        public int GLGroupClassID
        {
            get { return m_GLGroupClassID; }
            set { this.m_GLGroupClassID = value; }
        }

        private string m_GLGroupClassName = string.Empty;
        public string GLGroupClassName
        {
            //get { return AppObjectType == null ? m_AppObjectTypeName : this.AppObjectType.AppObjectTypeName; }
            get { return m_GLGroupClassName; }
            set { this.m_GLGroupClassName = value; }
        }

        
        private bool m_IsInstrument = false;
        public bool IsInstrument
        {
            get { return m_IsInstrument; }
            set { this.m_IsInstrument = value; }
        }

        private bool m_IsCash = false;
        public bool IsCash
        {
            get { return m_IsCash; }
            set { this.m_IsCash = value; }
        } 


        private int m_TranTypeID = 0;
        public int TranTypeID
        {
            get { return m_TranTypeID; }
            set { this.m_TranTypeID = value; }
        }

        private string m_TranTypeCode = string.Empty;
        public string TranTypeCode
        {
            get { return m_TranTypeCode; }
            set { this.m_TranTypeCode = value; }
        }

        private string m_TranTypeName = string.Empty;
        public string TranTypeName
        {
            get { return m_TranTypeName; }
            set { this.m_TranTypeName = value; }
        }

        private int m_TranTypeCategoryID = 0;
        public int TranTypeCategoryID
        {
            get { return m_TranTypeCategoryID; }
            set { this.m_TranTypeCategoryID = value; }
        }

        private string m_CostCenterText = string.Empty;
        public string CostCenterText
        {
            get { return m_CostCenterText; }
            set { this.m_CostCenterText = value; }
        }

        private string m_ReferenceText = string.Empty;
        public string ReferenceText
        {
            get { return m_ReferenceText; }
            set { this.m_ReferenceText = value; }
        }

        private string m_InstrumentText = string.Empty;
        public string InstrumentText
        {
            get { return m_InstrumentText; }
            set { this.m_InstrumentText = value; }
        }

        //used for sum 
        private int m_TranCount = 0;
        public int TranCount
        {
            get { return m_TranCount; }
            set { this.m_TranCount = value; }
        }


        //used for grid ui - master details link
        private int m_JournalDetID_Link = 0;
        public int JournalDetID_Link
        {
            get { return m_JournalDetID_Link; }
            set { m_JournalDetID_Link = value; }
        }

        private List<dcJournalDetRef> m_JournalDetRefList = null;
        public List<dcJournalDetRef> JournalDetRefList
        {
            get { return m_JournalDetRefList; }
            set { m_JournalDetRefList = value; }
        }

        private List<dcJournalDetIns> m_JournalDetInsList = null;
        public List<dcJournalDetIns> JournalDetInsList
        {
            get { return m_JournalDetInsList; }
            set { m_JournalDetInsList = value; }
        }

        //TODO Change Monir

        private string m_LocationName = string.Empty;
        public string LocationName
        {
            get { return m_LocationName; }
            set { this.m_LocationName = value; }
        }

        #endregion
    }
}
