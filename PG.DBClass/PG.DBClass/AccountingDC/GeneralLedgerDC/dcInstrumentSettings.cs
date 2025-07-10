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
    [DBTable(Name = "tblInstrumentSettings")]
    public partial class dcInstrumentSettings : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_InstrumentSettingsID = 0;
        private int m_CompanyID = 0;
        private int m_DefInsTypeIDIssue = 0;
        private int m_DefStatusIDIssue = 0;
        private bool m_AutoClearDateTypeIssue = false;
        private bool m_AllowSkipInstrumentEntryIssue = false;
        private int m_DefInsTypeIDReceive = 0;
        private int m_DefStatusIDReceive = 0;
        private bool m_AutoClearDateTypeReceive = false;
        private bool m_AllowSkipInstrumentEntryReceive = false;

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


        [DBColumn(Name = "InstrumentSettingsID", Storage = "m_InstrumentSettingsID", DbType = "Int NOT NULL", IsPrimaryKey = true)]
        public int InstrumentSettingsID
        {
            get { return this.m_InstrumentSettingsID; }
            set
            {
                this.m_InstrumentSettingsID = value;
                this.NotifyPropertyChanged("InstrumentSettingsID");
            }
        }

        [DBColumn(Name = "CompanyID", Storage = "m_CompanyID", DbType = "Int NOT NULL")]
        public int CompanyID
        {
            get { return this.m_CompanyID; }
            set
            {
                this.m_CompanyID = value;
                this.NotifyPropertyChanged("CompanyID");
            }
        }

        [DBColumn(Name = "DefInsTypeIDIssue", Storage = "m_DefInsTypeIDIssue", DbType = "Int NULL")]
        public int DefInsTypeIDIssue
        {
            get { return this.m_DefInsTypeIDIssue; }
            set
            {
                this.m_DefInsTypeIDIssue = value;
                this.NotifyPropertyChanged("DefInsTypeIDIssue");
            }
        }

        [DBColumn(Name = "DefStatusIDIssue", Storage = "m_DefStatusIDIssue", DbType = "Int NULL")]
        public int DefStatusIDIssue
        {
            get { return this.m_DefStatusIDIssue; }
            set
            {
                this.m_DefStatusIDIssue = value;
                this.NotifyPropertyChanged("DefStatusIDIssue");
            }
        }

        [DBColumn(Name = "AutoClearDateTypeIssue", Storage = "m_AutoClearDateTypeIssue", DbType = "Bit NULL")]
        public bool AutoClearDateTypeIssue
        {
            get { return this.m_AutoClearDateTypeIssue; }
            set
            {
                this.m_AutoClearDateTypeIssue = value;
                this.NotifyPropertyChanged("AutoClearDateTypeIssue");
            }
        }

        [DBColumn(Name = "AllowSkipInstrumentEntryIssue", Storage = "m_AllowSkipInstrumentEntryIssue", DbType = "Bit NULL")]
        public bool AllowSkipInstrumentEntryIssue
        {
            get { return this.m_AllowSkipInstrumentEntryIssue; }
            set
            {
                this.m_AllowSkipInstrumentEntryIssue = value;
                this.NotifyPropertyChanged("AllowSkipInsEntryIssue");
            }
        }

        [DBColumn(Name = "DefInsTypeIDReceive", Storage = "m_DefInsTypeIDReceive", DbType = "Int NULL")]
        public int DefInsTypeIDReceive
        {
            get { return this.m_DefInsTypeIDReceive; }
            set
            {
                this.m_DefInsTypeIDReceive = value;
                this.NotifyPropertyChanged("DefInsTypeIDReceive");
            }
        }

        [DBColumn(Name = "DefStatusIDReceive", Storage = "m_DefStatusIDReceive", DbType = "Int NULL")]
        public int DefStatusIDReceive
        {
            get { return this.m_DefStatusIDReceive; }
            set
            {
                this.m_DefStatusIDReceive = value;
                this.NotifyPropertyChanged("DefStatusIDReceive");
            }
        }

        [DBColumn(Name = "AutoClearDateTypeReceive", Storage = "m_AutoClearDateTypeReceive", DbType = "Bit NULL")]
        public bool AutoClearDateTypeReceive
        {
            get { return this.m_AutoClearDateTypeReceive; }
            set
            {
                this.m_AutoClearDateTypeReceive = value;
                this.NotifyPropertyChanged("AutoClearDateTypeReceive");
            }
        }

        [DBColumn(Name = "AllowSkipInstrumentEntryReceive", Storage = "m_AllowSkipInstrumentEntryReceive", DbType = "Bit NULL")]
        public bool AllowSkipInstrumentEntryReceive
        {
            get { return this.m_AllowSkipInstrumentEntryReceive; }
            set
            {
                this.m_AllowSkipInstrumentEntryReceive = value;
                this.NotifyPropertyChanged("AllowSkipInstrumentEntryReceive");
            }
        }

        #endregion //properties
    }
}
