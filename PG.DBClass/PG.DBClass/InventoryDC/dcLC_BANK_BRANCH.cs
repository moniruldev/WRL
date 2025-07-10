using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "LC_BANK_BRANCH")]
    public partial class dcLC_BANK_BRANCH : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private decimal m_BRANCH_ID = 0;
        private string m_BRANCH_CODE = string.Empty;
        private decimal m_BANK_ID = 0;
        private string m_BRANCH_NAME = string.Empty;
        private string m_ADDRESS = string.Empty;
        private string m_PHONE = string.Empty;
        private decimal m_ENTRY_BY = 0;
        private DateTime? m_ENTRY_DATE = null;

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


        [DBColumn(Name = "BRANCH_ID", Storage = "m_BRANCH_ID", DbType = "107", IsPrimaryKey = true)]
        public decimal BRANCH_ID
        {
            get { return this.m_BRANCH_ID; }
            set
            {
                this.m_BRANCH_ID = value;
                this.NotifyPropertyChanged("BRANCH_ID");
            }
        }

        [DBColumn(Name = "BRANCH_CODE", Storage = "m_BRANCH_CODE", DbType = "126")]
        public string BRANCH_CODE
        {
            get { return this.m_BRANCH_CODE; }
            set
            {
                this.m_BRANCH_CODE = value;
                this.NotifyPropertyChanged("BRANCH_CODE");
            }
        }

        [DBColumn(Name = "BANK_ID", Storage = "m_BANK_ID", DbType = "107")]
        public decimal BANK_ID
        {
            get { return this.m_BANK_ID; }
            set
            {
                this.m_BANK_ID = value;
                this.NotifyPropertyChanged("BANK_ID");
            }
        }

        [DBColumn(Name = "BRANCH_NAME", Storage = "m_BRANCH_NAME", DbType = "126")]
        public string BRANCH_NAME
        {
            get { return this.m_BRANCH_NAME; }
            set
            {
                this.m_BRANCH_NAME = value;
                this.NotifyPropertyChanged("BRANCH_NAME");
            }
        }

        [DBColumn(Name = "ADDRESS", Storage = "m_ADDRESS", DbType = "126")]
        public string ADDRESS
        {
            get { return this.m_ADDRESS; }
            set
            {
                this.m_ADDRESS = value;
                this.NotifyPropertyChanged("ADDRESS");
            }
        }

        [DBColumn(Name = "PHONE", Storage = "m_PHONE", DbType = "126")]
        public string PHONE
        {
            get { return this.m_PHONE; }
            set
            {
                this.m_PHONE = value;
                this.NotifyPropertyChanged("PHONE");
            }
        }

        [DBColumn(Name = "ENTRY_BY", Storage = "m_ENTRY_BY", DbType = "107")]
        public decimal ENTRY_BY
        {
            get { return this.m_ENTRY_BY; }
            set
            {
                this.m_ENTRY_BY = value;
                this.NotifyPropertyChanged("ENTRY_BY");
            }
        }

        [DBColumn(Name = "ENTRY_DATE", Storage = "m_ENTRY_DATE", DbType = "106")]
        public DateTime? ENTRY_DATE
        {
            get { return this.m_ENTRY_DATE; }
            set
            {
                this.m_ENTRY_DATE = value;
                this.NotifyPropertyChanged("ENTRY_DATE");
            }
        }

        #endregion //properties
    }
}
