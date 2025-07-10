using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using PG.Core.DBBase;

namespace PG.DBClass.AccountingDC
{
    [DBTable(Name = "tblLocationJournalType")]
    public partial class dcLocationJournalType : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_LocationJournalTypeID = 0;
        private int m_LocationJournalTypeSLNo = 0;
        private int m_LocationID = 0;
        private int m_JournalTypeID = 0;
        private string m_JournalNoPrefix = string.Empty;
        private string m_JournalNoSuffix = string.Empty;
        private int m_AccSLNoID = 0;

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


        [DBColumn(Name = "LocationJournalTypeID", Storage = "m_LocationJournalTypeID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int LocationJournalTypeID
        {
            get { return this.m_LocationJournalTypeID; }
            set
            {
                this.m_LocationJournalTypeID = value;
                this.NotifyPropertyChanged("LocationJournalTypeID");
            }
        }

        [DBColumn(Name = "LocationJournalTypeSLNo", Storage = "m_LocationJournalTypeSLNo", DbType = "Int NULL")]
        public int LocationJournalTypeSLNo
        {
            get { return this.m_LocationJournalTypeSLNo; }
            set
            {
                this.m_LocationJournalTypeSLNo = value;
                this.NotifyPropertyChanged("LocationJournalTypeSLNo");
            }
        }

        [DBColumn(Name = "LocationID", Storage = "m_LocationID", DbType = "Int NULL")]
        public int LocationID
        {
            get { return this.m_LocationID; }
            set
            {
                this.m_LocationID = value;
                this.NotifyPropertyChanged("LocationID");
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

        [DBColumn(Name = "JournalNoPrefix", Storage = "m_JournalNoPrefix", DbType = "NVarChar(50) NULL")]
        public string JournalNoPrefix
        {
            get { return this.m_JournalNoPrefix; }
            set
            {
                this.m_JournalNoPrefix = value;
                this.NotifyPropertyChanged("JournalNoPrefix");
            }
        }

        [DBColumn(Name = "JournalNoSuffix", Storage = "m_JournalNoSuffix", DbType = "NVarChar(50) NULL")]
        public string JournalNoSuffix
        {
            get { return this.m_JournalNoSuffix; }
            set
            {
                this.m_JournalNoSuffix = value;
                this.NotifyPropertyChanged("JournalNoSuffix");
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

        #endregion //properties
    }
}
