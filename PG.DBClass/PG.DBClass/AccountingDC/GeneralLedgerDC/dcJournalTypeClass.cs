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
    [DBTable(Name = "tblJournalTypeClass")]
    public partial class dcJournalTypeClass : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_JournalTypeClassID = 0;
        private string m_JournalTypeClassCode = string.Empty;
        private string m_JournalTypeClassName = string.Empty;
        private string m_GLGroupClassInclude = string.Empty;
        private string m_GLGroupClassExclude = string.Empty;
        private bool m_IsActive = false;
        private bool m_IsVisible = false;

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


        [DBColumn(Name = "JournalTypeClassID", Storage = "m_JournalTypeClassID", DbType = "Int NOT NULL", IsPrimaryKey = true)]
        public int JournalTypeClassID
        {
            get { return this.m_JournalTypeClassID; }
            set
            {
                this.m_JournalTypeClassID = value;
                this.NotifyPropertyChanged("JournalTypeClassID");
            }
        }

        [DBColumn(Name = "JournalTypeClassCode", Storage = "m_JournalTypeClassCode", DbType = "NVarChar(50) NULL")]
        public string JournalTypeClassCode
        {
            get { return this.m_JournalTypeClassCode; }
            set
            {
                this.m_JournalTypeClassCode = value;
                this.NotifyPropertyChanged("JournalTypeClassCode");
            }
        }

        [DBColumn(Name = "JournalTypeClassName", Storage = "m_JournalTypeClassName", DbType = "NVarChar(100) NULL")]
        public string JournalTypeClassName
        {
            get { return this.m_JournalTypeClassName; }
            set
            {
                this.m_JournalTypeClassName = value;
                this.NotifyPropertyChanged("JournalTypeClassName");
            }
        }

        [DBColumn(Name = "GLGroupClassInclude", Storage = "m_GLGroupClassInclude", DbType = "NVarChar(MAX) NULL")]
        public string GLGroupClassInclude
        {
            get { return this.m_GLGroupClassInclude; }
            set
            {
                this.m_GLGroupClassInclude = value;
                this.NotifyPropertyChanged("GLGroupClassInclude");
            }
        }

        [DBColumn(Name = "GLGroupClassExclude", Storage = "m_GLGroupClassExclude", DbType = "NVarChar(MAX) NULL")]
        public string GLGroupClassExclude
        {
            get { return this.m_GLGroupClassExclude; }
            set
            {
                this.m_GLGroupClassExclude = value;
                this.NotifyPropertyChanged("GLGroupClassExclude");
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

        #endregion //properties
    }
}
