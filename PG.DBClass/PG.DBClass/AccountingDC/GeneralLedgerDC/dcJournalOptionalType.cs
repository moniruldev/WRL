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
    [DBTable(Name = "tblJournalOptionalType")]
    public partial class dcJournalOptionalType : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_JournalOptionalTypeID = 0;
        private string m_JournalOptionalTypeCoe = string.Empty;
        private string m_JournalOptionalTypeName = string.Empty;
        private int m_JournalOptionalTypeSLNo = 0;
        private bool m_IsSystem = false;
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


        [DBColumn(Name = "JournalOptionalTypeID", Storage = "m_JournalOptionalTypeID", DbType = "Int NOT NULL", IsPrimaryKey = true)]
        public int JournalOptionalTypeID
        {
            get { return this.m_JournalOptionalTypeID; }
            set
            {
                this.m_JournalOptionalTypeID = value;
                this.NotifyPropertyChanged("JournalOptionalTypeID");
            }
        }

        [DBColumn(Name = "JournalOptionalTypeCoe", Storage = "m_JournalOptionalTypeCoe", DbType = "NVarChar(20) NULL")]
        public string JournalOptionalTypeCoe
        {
            get { return this.m_JournalOptionalTypeCoe; }
            set
            {
                this.m_JournalOptionalTypeCoe = value;
                this.NotifyPropertyChanged("JournalOptionalTypeCoe");
            }
        }

        [DBColumn(Name = "JournalOptionalTypeName", Storage = "m_JournalOptionalTypeName", DbType = "NVarChar(50) NULL")]
        public string JournalOptionalTypeName
        {
            get { return this.m_JournalOptionalTypeName; }
            set
            {
                this.m_JournalOptionalTypeName = value;
                this.NotifyPropertyChanged("JournalOptionalTypeName");
            }
        }

        [DBColumn(Name = "JournalOptionalTypeSLNo", Storage = "m_JournalOptionalTypeSLNo", DbType = "Int NULL")]
        public int JournalOptionalTypeSLNo
        {
            get { return this.m_JournalOptionalTypeSLNo; }
            set
            {
                this.m_JournalOptionalTypeSLNo = value;
                this.NotifyPropertyChanged("JournalOptionalTypeSLNo");
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

        #endregion //properties
    }
}
