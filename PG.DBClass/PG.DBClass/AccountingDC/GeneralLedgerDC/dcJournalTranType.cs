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
    [DBTable(Name = "tblJournalTranType")]
    public partial class dcJournalTranType : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_JournalTranTypeID = 0;
        private string m_JournalTranTypeCode = string.Empty;
        private string m_JournalTranTypeName = string.Empty;
        private string m_JournalTranTypeNameSys = string.Empty;
        private int m_JournalTranTypeSLNo = 0;
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


        [DBColumn(Name = "JournalTranTypeID", Storage = "m_JournalTranTypeID", DbType = "Int NOT NULL", IsPrimaryKey = true)]
        public int JournalTranTypeID
        {
            get { return this.m_JournalTranTypeID; }
            set
            {
                this.m_JournalTranTypeID = value;
                this.NotifyPropertyChanged("JournalTranTypeID");
            }
        }

        [DBColumn(Name = "JournalTranTypeCode", Storage = "m_JournalTranTypeCode", DbType = "NVarChar(20) NULL")]
        public string JournalTranTypeCode
        {
            get { return this.m_JournalTranTypeCode; }
            set
            {
                this.m_JournalTranTypeCode = value;
                this.NotifyPropertyChanged("JournalTranTypeCode");
            }
        }

        [DBColumn(Name = "JournalTranTypeName", Storage = "m_JournalTranTypeName", DbType = "NVarChar(50) NULL")]
        public string JournalTranTypeName
        {
            get { return this.m_JournalTranTypeName; }
            set
            {
                this.m_JournalTranTypeName = value;
                this.NotifyPropertyChanged("JournalTranTypeName");
            }
        }

        [DBColumn(Name = "JournalTranTypeNameSys", Storage = "m_JournalTranTypeNameSys", DbType = "NVarChar(50) NULL")]
        public string JournalTranTypeNameSys
        {
            get { return this.m_JournalTranTypeNameSys; }
            set
            {
                this.m_JournalTranTypeNameSys = value;
                this.NotifyPropertyChanged("JournalTranTypeNameSys");
            }
        }

        [DBColumn(Name = "JournalTranTypeSLNo", Storage = "m_JournalTranTypeSLNo", DbType = "Int NULL")]
        public int JournalTranTypeSLNo
        {
            get { return this.m_JournalTranTypeSLNo; }
            set
            {
                this.m_JournalTranTypeSLNo = value;
                this.NotifyPropertyChanged("JournalTranTypeSLNo");
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
