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
    [DBTable(Name = "tblJournalReportFormat")]
    public partial class dcJournalReportFormat : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_JournalReportFormatID = 0;
        private string m_JournalReportFormatName = string.Empty;
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


        [DBColumn(Name = "JournalReportFormatID", Storage = "m_JournalReportFormatID", DbType = "Int NOT NULL", IsPrimaryKey = true)]
        public int JournalReportFormatID
        {
            get { return this.m_JournalReportFormatID; }
            set
            {
                this.m_JournalReportFormatID = value;
                this.NotifyPropertyChanged("JournalReportFormatID");
            }
        }

        [DBColumn(Name = "JournalReportFormatName", Storage = "m_JournalReportFormatName", DbType = "NVarChar(50) NULL")]
        public string JournalReportFormatName
        {
            get { return this.m_JournalReportFormatName; }
            set
            {
                this.m_JournalReportFormatName = value;
                this.NotifyPropertyChanged("JournalReportFormatName");
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
