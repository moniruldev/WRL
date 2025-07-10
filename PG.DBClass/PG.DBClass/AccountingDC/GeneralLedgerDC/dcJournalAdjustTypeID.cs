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
    [DBTable(Name = "tblJournalAdjustType")]
    public partial class dcJournalAdjustType : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_JournalAdjustTypeID = 0;
        private string m_JournalAdjustTypeName = string.Empty;
        private int m_JournalAdjustTypeSLNo = 0;
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


        [DBColumn(Name = "JournalAdjustTypeID", Storage = "m_JournalAdjustTypeID", DbType = "Int NOT NULL", IsPrimaryKey = true)]
        public int JournalAdjustTypeID
        {
            get { return this.m_JournalAdjustTypeID; }
            set
            {
                this.m_JournalAdjustTypeID = value;
                this.NotifyPropertyChanged("JournalAdjustTypeID");
            }
        }

        [DBColumn(Name = "JournalAdjustTypeName", Storage = "m_JournalAdjustTypeName", DbType = "NVarChar(50) NULL")]
        public string JournalAdjustTypeName
        {
            get { return this.m_JournalAdjustTypeName; }
            set
            {
                this.m_JournalAdjustTypeName = value;
                this.NotifyPropertyChanged("JournalAdjustTypeName");
            }
        }

        [DBColumn(Name = "JournalAdjustTypeSLNo", Storage = "m_JournalAdjustTypeSLNo", DbType = "Int NULL")]
        public int JournalAdjustTypeSLNo
        {
            get { return this.m_JournalAdjustTypeSLNo; }
            set
            {
                this.m_JournalAdjustTypeSLNo = value;
                this.NotifyPropertyChanged("JournalAdjustTypeSLNo");
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
