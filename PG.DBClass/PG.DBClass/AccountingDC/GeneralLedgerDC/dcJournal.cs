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
    [DBTable(Name = "tblJournal")]
    public partial class dcJournal : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_JournalID = 0;
        private int m_CompanyID = 0;
        private int m_LocationID = 0;
        private int m_AccYearID = 0;
        private int m_JournalTypeID = 0;
        private string m_JournalNo = string.Empty;
        private int m_JournalSLNo = 0;
        private int m_JournalSLNoSys = 0;
        private DateTime m_JournalDate = DateTime.Now;
        private string m_JournalNote = string.Empty;
        private string m_JournalDesc = string.Empty;
        private string m_JournalRefNo = string.Empty;
        private decimal m_JournalAmt = 0;
        private bool m_IsDeleted = false;
        private bool m_IsEditable = false;
        private bool m_IsBalanced = false;
        private bool m_IsPosted = false;
        private DateTime? m_PostedDate = null;
        private string m_PostedBy = string.Empty;
        private int m_JournalAdjustTypeID = 0;
        private int m_JournalAdjustReasonID = 0;
        private int m_JournalIDAdjust = 0;
        private int m_JournalCreateTypeID = 1;  //manual
        private int m_JournalOptionalTypeID = 0;
        private DateTime? m_AddDate = null;
        private DateTime? m_EditDate = null;
        private int m_AddUserID = 0;
        private int m_EditUserID = 0;
        private string m_EditUserName = string.Empty;
        private int m_JournalUpdateNo = 0;

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


        [DBColumn(Name = "JournalID", Storage = "m_JournalID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity =true)]
        public int JournalID
        {
            get { return this.m_JournalID; }
            set
            {
                this.m_JournalID = value;
                this.NotifyPropertyChanged("JournalID");
            }
        }

        [DBColumn(Name = "CompanyID", Storage = "m_CompanyID", DbType = "Int NULL", DefaultValue="1")]
        public int CompanyID
        {
            get { return this.m_CompanyID; }
            set
            {
                this.m_CompanyID = value;
                this.NotifyPropertyChanged("CompanyID");
            }
        }


        [DBColumn(Name = "LocationID", Storage = "m_LocationID", DbType = "Int NOT NULL")]
        public int LocationID
        {
            get { return this.m_LocationID; }
            set
            {
                this.m_LocationID = value;
                this.NotifyPropertyChanged("LocationID");
            }
        }


        [DBColumn(Name = "AccYearID", Storage = "m_AccYearID", DbType = "Int NULL")]
        public int AccYearID
        {
            get { return this.m_AccYearID; }
            set
            {
                this.m_AccYearID = value;
                this.NotifyPropertyChanged("AccYearID");
            }
        }

        [DBColumn(Name = "JournalTypeID", Storage = "m_JournalTypeID", DbType = "Int NULL")]
        public int JournalTypeID
        {
            get { return this.m_JournalTypeID; }
            set
            {
                this.m_JournalTypeID = value;
                this.NotifyPropertyChanged("JournalTypeID");
            }
        }

        [DBColumn(Name = "JournalNo", Storage = "m_JournalNo", DbType = "NVarChar(50) NULL")]
        public string JournalNo
        {
            get { return this.m_JournalNo; }
            set
            {
                this.m_JournalNo = value;
                this.NotifyPropertyChanged("JournalNo");
            }
        }

        [DBColumn(Name = "JournalSLNo", Storage = "m_JournalSLNo", DbType = "Int NULL")]
        public int JournalSLNo
        {
            get { return this.m_JournalSLNo; }
            set
            {
                this.m_JournalSLNo = value;
                this.NotifyPropertyChanged("JournalSLNo");
            }
        }

        [DBColumn(Name = "JournalSLNoSys", Storage = "m_JournalSLNoSys", DbType = "Int NULL")]
        public int JournalSLNoSys
        {
            get { return this.m_JournalSLNoSys; }
            set
            {
                this.m_JournalSLNoSys = value;
                this.NotifyPropertyChanged("JournalSLNoSys");
            }
        }

        [DBColumn(Name = "JournalDate", Storage = "m_JournalDate", DbType = "DateTime NOT NULL")]
        public DateTime JournalDate
        {
            get { return this.m_JournalDate; }
            set
            {
                this.m_JournalDate = value;
                this.NotifyPropertyChanged("JournalDate");
            }
        }

        [DBColumn(Name = "JournalNote", Storage = "m_JournalNote", DbType = "NVarChar(100) NULL")]
        public string JournalNote
        {
            get { return this.m_JournalNote; }
            set
            {
                this.m_JournalNote = value;
                this.NotifyPropertyChanged("JournalNote");
            }
        }

        [DBColumn(Name = "JournalDesc", Storage = "m_JournalDesc", DbType = "NVarChar(200) NULL")]
        public string JournalDesc
        {
            get { return this.m_JournalDesc; }
            set
            {
                this.m_JournalDesc = value;
                this.NotifyPropertyChanged("JournalDesc");
            }
        }

        [DBColumn(Name = "JournalRefNo", Storage = "m_JournalRefNo", DbType = "NVarChar(50) NULL")]
        public string JournalRefNo
        {
            get { return this.m_JournalRefNo; }
            set
            {
                this.m_JournalRefNo = value;
                this.NotifyPropertyChanged("JournalRefNo");
            }
        }

        [DBColumn(Name = "JournalAmt", Storage = "m_JournalAmt", DbType = "Money NULL")]
        public decimal JournalAmt
        {
            get { return this.m_JournalAmt; }
            set
            {
                this.m_JournalAmt = value;
                this.NotifyPropertyChanged("JournalAmt");
            }
        }

        [DBColumn(Name = "IsDeleted", Storage = "m_IsDeleted", DbType = "Bit NULL")]
        public bool IsDeleted
        {
            get { return this.m_IsDeleted; }
            set
            {
                this.m_IsDeleted = value;
                this.NotifyPropertyChanged("IsDeleted");
            }
        }

        [DBColumn(Name = "IsEditable", Storage = "m_IsEditable", DbType = "Bit NULL")]
        public bool IsEditable
        {
            get { return this.m_IsEditable; }
            set
            {
                this.m_IsEditable = value;
                this.NotifyPropertyChanged("IsEditable");
            }
        }

        [DBColumn(Name = "IsBalanced", Storage = "m_IsBalanced", DbType = "Bit NULL")]
        public bool IsBalanced
        {
            get { return this.m_IsBalanced; }
            set
            {
                this.m_IsBalanced = value;
                this.NotifyPropertyChanged("IsBalanced");
            }
        }

        [DBColumn(Name = "IsPosted", Storage = "m_IsPosted", DbType = "Bit NULL")]
        public bool IsPosted
        {
            get { return this.m_IsPosted; }
            set
            {
                this.m_IsPosted = value;
                this.NotifyPropertyChanged("IsPosted");
            }
        }

        [DBColumn(Name = "PostedDate", Storage = "m_PostedDate", DbType = "DateTime NULL")]
        public DateTime? PostedDate
        {
            get { return this.m_PostedDate; }
            set
            {
                this.m_PostedDate = value;
                this.NotifyPropertyChanged("PostedDate");
            }
        }

        [DBColumn(Name = "PostedBy", Storage = "m_PostedBy", DbType = "NVarChar(50) NULL")]
        public string PostedBy
        {
            get { return this.m_PostedBy; }
            set
            {
                this.m_PostedBy = value;
                this.NotifyPropertyChanged("PostedBy");
            }
        }

        [DBColumn(Name = "JournalAdjustTypeID", Storage = "m_JournalAdjustTypeID", DbType = "Int NULL")]
        public int JournalAdjustTypeID
        {
            get { return this.m_JournalAdjustTypeID; }
            set
            {
                this.m_JournalAdjustTypeID = value;
                this.NotifyPropertyChanged("JournalAdjustTypeID");
            }
        }

        [DBColumn(Name = "JournalAdjustReasonID", Storage = "m_JournalAdjustReasonID", DbType = "Int NULL")]
        public int JournalAdjustReasonID
        {
            get { return this.m_JournalAdjustReasonID; }
            set
            {
                this.m_JournalAdjustReasonID = value;
                this.NotifyPropertyChanged("JournalAdjustReasonID");
            }
        }

        [DBColumn(Name = "JournalIDAdjust", Storage = "m_JournalIDAdjust", DbType = "Int NULL")]
        public int JournalIDAdjust
        {
            get { return this.m_JournalIDAdjust; }
            set
            {
                this.m_JournalIDAdjust = value;
                this.NotifyPropertyChanged("JournalIDAdjust");
            }
        }

        [DBColumn(Name = "JournalCreateTypeID", Storage = "m_JournalCreateTypeID", DbType = "Int NULL")]
        public int JournalCreateTypeID
        {
            get { return this.m_JournalCreateTypeID; }
            set
            {
                this.m_JournalCreateTypeID = value;
                this.NotifyPropertyChanged("JournalCreateTypeID");
            }
        }

        [DBColumn(Name = "JournalOptionalTypeID", Storage = "m_JournalOptionalTypeID", DbType = "Int NULL")]
        public int JournalOptionalTypeID
        {
            get { return this.m_JournalOptionalTypeID; }
            set
            {
                this.m_JournalOptionalTypeID = value;
                this.NotifyPropertyChanged("JournalOptionalTypeID");
            }
        }
       

        [DBColumn(Name = "AddDate", Storage = "m_AddDate", DbType = "DateTime NULL")]
        public DateTime? AddDate
        {
            get { return this.m_AddDate; }
            set
            {
                this.m_AddDate = value;
                this.NotifyPropertyChanged("AddDate");
            }
        }

        [DBColumn(Name = "EditDate", Storage = "m_EditDate", DbType = "DateTime NULL")]
        public DateTime? EditDate
        {
            get { return this.m_EditDate; }
            set
            {
                this.m_EditDate = value;
                this.NotifyPropertyChanged("EditDate");
            }
        }

        [DBColumn(Name = "AddUserID", Storage = "m_AddUserID", DbType = "Int NULL")]
        public int AddUserID
        {
            get { return this.m_AddUserID; }
            set
            {
                this.m_AddUserID = value;
                this.NotifyPropertyChanged("AddUserID");
            }
        }

        [DBColumn(Name = "EditUserID", Storage = "m_EditUserID", DbType = "Int NULL")]
        public int EditUserID
        {
            get { return this.m_EditUserID; }
            set
            {
                this.m_EditUserID = value;
                this.NotifyPropertyChanged("EditUserID");
            }
        }

        [DBColumn(Name = "EditUserName", Storage = "m_EditUserName", DbType = "NVarChar(50) NULL")]
        public string EditUserName
        {
            get { return this.m_EditUserName; }
            set
            {
                this.m_EditUserName = value;
                this.NotifyPropertyChanged("EditUserName");
            }
        }

        [DBColumn(Name = "JournalUpdateNo", Storage = "m_JournalUpdateNo", DbType = "Int NULL")]
        public int JournalUpdateNo
        {
            get { return this.m_JournalUpdateNo; }
            set
            {
                this.m_JournalUpdateNo = value;
                this.NotifyPropertyChanged("JournalUpdateNo");
            }
        }





        #endregion //properties
    }


    public partial class dcJournal
    {

        private int m_DebitCount = 0;
        public int DebitCount
        {
            get { return m_DebitCount; }
            set { m_DebitCount = value; }
        }

        private int m_CreditCount = 0;
        public int CreditCount
        {
            get { return m_CreditCount; }
            set { m_CreditCount = value; }
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

        private string m_AccYearName = string.Empty;
        public string AccYearName
        {
            get { return m_AccYearName; }
            set { m_AccYearName = value; }
        }



        private List<dcJournalDet> m_JournalDetList = null;
        public List<dcJournalDet> JournalDetList
        {
            get { return m_JournalDetList; }
            set { m_JournalDetList = value; }
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

        private string m_LocationCode = string.Empty;
        public string LocationCode
        {
            get { return m_LocationCode; }
            set { this.m_LocationCode = value; }
        }


    }
}
