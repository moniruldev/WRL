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
    [DBTable(Name = "tblGLAccountRefCategory")]
    public partial class dcGLAccountRefCategory : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_GLAccountRefCategoryID = 0;
        private int m_GLAccountRefCategorySLNo = 0;
        private int m_GLAccountID = 0;
        private int m_AccRefCategoryID = 0;
        private bool m_IsMandatory = false;
        private bool m_IsDefault = false;

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


        [DBColumn(Name = "GLAccountRefCategoryID", Storage = "m_GLAccountRefCategoryID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int GLAccountRefCategoryID
        {
            get { return this.m_GLAccountRefCategoryID; }
            set
            {
                this.m_GLAccountRefCategoryID = value;
                this.NotifyPropertyChanged("GLAccountRefCategoryID");
            }
        }

        [DBColumn(Name = "GLAccountRefCategorySLNo", Storage = "m_GLAccountRefCategorySLNo", DbType = "Int NULL")]
        public int GLAccountRefCategorySLNo
        {
            get { return this.m_GLAccountRefCategorySLNo; }
            set
            {
                this.m_GLAccountRefCategorySLNo = value;
                this.NotifyPropertyChanged("GLAccountRefCategorySLNo");
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

        [DBColumn(Name = "AccRefCategoryID", Storage = "m_AccRefCategoryID", DbType = "Int NULL")]
        public int AccRefCategoryID
        {
            get { return this.m_AccRefCategoryID; }
            set
            {
                this.m_AccRefCategoryID = value;
                this.NotifyPropertyChanged("AccRefCategoryID");
            }
        }

        [DBColumn(Name = "IsMandatory", Storage = "m_IsMandatory", DbType = "Bit NULL")]
        public bool IsMandatory
        {
            get { return this.m_IsMandatory; }
            set
            {
                this.m_IsMandatory = value;
                this.NotifyPropertyChanged("IsMandatory");
            }
        }

        [DBColumn(Name = "IsDefault", Storage = "m_IsDefault", DbType = "Bit NOT NULL")]
        public bool IsDefault
        {
            get { return this.m_IsDefault; }
            set
            {
                this.m_IsDefault = value;
                this.NotifyPropertyChanged("IsDefault");
            }
        }

        #endregion //properties
    }


    public partial class dcGLAccountRefCategory
    {
        private int m_AccRefTypeID = 0;
        public int AccRefTypeID
        {
            get { return m_AccRefTypeID; }
            set { this.m_AccRefTypeID = value; }
        }

        private string m_AccRefTypeName = string.Empty;
        public string AccRefTypeName
        {
            get { return m_AccRefTypeName; }
            set { this.m_AccRefTypeName = value; }
        }

        private string m_AccRefCategoryCode = string.Empty;
        public string AccRefCategoryCode
        {
            get { return m_AccRefCategoryCode; }
            set { this.m_AccRefCategoryCode = value; }
        }

        private string m_AccRefCategoryName = string.Empty;
        public string AccRefCategoryName
        {
            get { return m_AccRefCategoryName; }
            set { this.m_AccRefCategoryName = value; }
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

        private int m_GLGroupID = 0;
        public int GLGroupID
        {
            get { return m_GLGroupID; }
            set { this.m_GLGroupID = value; }
        }
    }
}
