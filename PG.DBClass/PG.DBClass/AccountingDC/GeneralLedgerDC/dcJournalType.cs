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
    [DBTable(Name = "tblJournalType")]
    public partial class dcJournalType : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_JournalTypeID = 0;
        private int m_CompanyID = 0;
        private int m_JournalTypeClassID = 0;
        private int m_JournalTypeSLNo = 0;
        private string m_JournalTypeCode = string.Empty;
        private string m_JournalTypeName = string.Empty;
        private bool m_IsGLGroupClass = false;
        private string m_GLGroupClassIncludeJT = string.Empty;
        private string m_GLGroupClassExcludeJT = string.Empty;
        private int m_AccSLNoID = 0;
        private string m_JournalColorHex = string.Empty;
        private bool m_IsAutoPost = false;
        private bool m_IsAutoPrint = false;
        private bool m_IsDesc = false;
        private bool m_IsDescEmpty = false;
        private bool m_IsDetDesc = false;
        private bool m_IsDetDescEmpty = false;
        private bool m_IsSystem = false;
        private bool m_IsVisible = false;
        private bool m_IsActive = false;

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


        [DBColumn(Name = "JournalTypeID", Storage = "m_JournalTypeID", DbType = "Int NOT NULL", IsPrimaryKey = true)]
        public int JournalTypeID
        {
            get { return this.m_JournalTypeID; }
            set
            {
                this.m_JournalTypeID = value;
                this.NotifyPropertyChanged("JournalTypeID");
            }
        }

        [DBColumn(Name = "CompanyID", Storage = "m_CompanyID", DbType = "Int NULL")]
        public int CompanyID
        {
            get { return this.m_CompanyID; }
            set
            {
                this.m_CompanyID = value;
                this.NotifyPropertyChanged("CompanyID");
            }
        }

        [DBColumn(Name = "JournalTypeClassID", Storage = "m_JournalTypeClassID", DbType = "Int NULL")]
        public int JournalTypeClassID
        {
            get { return this.m_JournalTypeClassID; }
            set
            {
                this.m_JournalTypeClassID = value;
                this.NotifyPropertyChanged("JournalTypeClassID");
            }
        }

        [DBColumn(Name = "JournalTypeSLNo", Storage = "m_JournalTypeSLNo", DbType = "Int NULL")]
        public int JournalTypeSLNo
        {
            get { return this.m_JournalTypeSLNo; }
            set
            {
                this.m_JournalTypeSLNo = value;
                this.NotifyPropertyChanged("JournalTypeSLNo");
            }
        }

        [DBColumn(Name = "JournalTypeCode", Storage = "m_JournalTypeCode", DbType = "NVarChar(20) NULL")]
        public string JournalTypeCode
        {
            get { return this.m_JournalTypeCode; }
            set
            {
                this.m_JournalTypeCode = value;
                this.NotifyPropertyChanged("JournalTypeCode");
            }
        }

        [DBColumn(Name = "JournalTypeName", Storage = "m_JournalTypeName", DbType = "NVarChar(100) NULL")]
        public string JournalTypeName
        {
            get { return this.m_JournalTypeName; }
            set
            {
                this.m_JournalTypeName = value;
                this.NotifyPropertyChanged("JournalTypeName");
            }
        }

        [DBColumn(Name = "IsGLGroupClass", Storage = "m_IsGLGroupClass", DbType = "Bit NULL")]
        public bool IsGLGroupClass
        {
            get { return this.m_IsGLGroupClass; }
            set
            {
                this.m_IsGLGroupClass = value;
                this.NotifyPropertyChanged("IsGLGroupClass");
            }
        }

        [DBColumn(Name = "GLGroupClassIncludeJT", Storage = "m_GLGroupClassIncludeJT", DbType = "NVarChar(MAX) NULL")]
        public string GLGroupClassIncludeJT
        {
            get { return this.m_GLGroupClassIncludeJT; }
            set
            {
                this.m_GLGroupClassIncludeJT = value;
                this.NotifyPropertyChanged("GLGroupClassIncludeJT");
            }
        }

        [DBColumn(Name = "GLGroupClassExcludeJT", Storage = "m_GLGroupClassExcludeJT", DbType = "NVarChar(MAX) NULL")]
        public string GLGroupClassExcludeJT
        {
            get { return this.m_GLGroupClassExcludeJT; }
            set
            {
                this.m_GLGroupClassExcludeJT = value;
                this.NotifyPropertyChanged("GLGroupClassExcludeJT");
            }
        }

        [DBColumn(Name = "AccSLNoID", Storage = "m_AccSLNoID", DbType = "Int NULL")]
        public int AccSLNoID
        {
            get { return this.m_AccSLNoID; }
            set
            {
                this.m_AccSLNoID = value;
                this.NotifyPropertyChanged("AccSLNoID");
            }
        }

        [DBColumn(Name = "JournalColorHex", Storage = "m_JournalColorHex", DbType = "NVarChar(50) NULL")]
        public string JournalColorHex
        {
            get { return this.m_JournalColorHex; }
            set
            {
                this.m_JournalColorHex = value;
                this.NotifyPropertyChanged("JournalColorHex");
            }
        }


        [DBColumn(Name = "IsAutoPost", Storage = "m_IsAutoPost", DbType = "Bit NULL")]
        public bool IsAutoPost
        {
            get { return this.m_IsAutoPost; }
            set
            {
                this.m_IsAutoPost = value;
                this.NotifyPropertyChanged("IsAutoPost");
            }
        }

        [DBColumn(Name = "IsAutoPrint", Storage = "m_IsAutoPrint", DbType = "Bit NULL")]
        public bool IsAutoPrint
        {
            get { return this.m_IsAutoPrint; }
            set
            {
                this.m_IsAutoPrint = value;
                this.NotifyPropertyChanged("IsAutoPrint");
            }
        }

        [DBColumn(Name = "IsDesc", Storage = "m_IsDesc", DbType = "Bit NULL")]
        public bool IsDesc
        {
            get { return this.m_IsDesc; }
            set
            {
                this.m_IsDesc = value;
                this.NotifyPropertyChanged("IsDesc");
            }
        }

        [DBColumn(Name = "IsDescEmpty", Storage = "m_IsDescEmpty", DbType = "Bit NULL")]
        public bool IsDescEmpty
        {
            get { return this.m_IsDescEmpty; }
            set
            {
                this.m_IsDescEmpty = value;
                this.NotifyPropertyChanged("IsDescEmpty");
            }
        }

        [DBColumn(Name = "IsDetDesc", Storage = "m_IsDetDesc", DbType = "Bit NULL")]
        public bool IsDetDesc
        {
            get { return this.m_IsDetDesc; }
            set
            {
                this.m_IsDetDesc = value;
                this.NotifyPropertyChanged("IsDetDesc");
            }
        }

        [DBColumn(Name = "IsDetDescEmpty", Storage = "m_IsDetDescEmpty", DbType = "Bit NULL")]
        public bool IsDetDescEmpty
        {
            get { return this.m_IsDetDescEmpty; }
            set
            {
                this.m_IsDetDescEmpty = value;
                this.NotifyPropertyChanged("IsDetDescEmpty");
            }
        }

        [DBColumn(Name = "IsSystem", Storage = "m_IsSystem", DbType = "Bit NULL")]
        public bool IsSystem
        {
            get { return this.m_IsSystem; }
            set
            {
                this.m_IsSystem = value;
                this.NotifyPropertyChanged("IsSystem");
            }
        }

        [DBColumn(Name = "IsVisible", Storage = "m_IsVisible", DbType = "Bit NULL")]
        public bool IsVisible
        {
            get { return this.m_IsVisible; }
            set
            {
                this.m_IsVisible = value;
                this.NotifyPropertyChanged("IsVisible");
            }
        }

        [DBColumn(Name = "IsActive", Storage = "m_IsActive", DbType = "Bit NULL")]
        public bool IsActive
        {
            get { return this.m_IsActive; }
            set
            {
                this.m_IsActive = value;
                this.NotifyPropertyChanged("IsActive");
            }
        }

        #endregion //properties
    }

    public partial class dcJournalType 
    {
        private string m_JournalTypeClassName = string.Empty;
        public string JournalTypeClassName
        {
            get { return m_JournalTypeClassName; }
            set { this.m_JournalTypeClassName = value; }
        }

        private string m_GLGroupClassInclude = string.Empty;
        public string GLGroupClassInclude
        {
            get { return m_GLGroupClassInclude; }
            set { this.m_GLGroupClassInclude = value; }
        }

        private string m_GLGroupClassExclude = string.Empty;
        public string GLGroupClassExclude
        {
            get { return m_GLGroupClassExclude; }
            set { this.m_GLGroupClassExclude = value; }
        }

    }

}
