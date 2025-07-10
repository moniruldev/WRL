using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "LC_CURRENCY")]
    public partial class dcLC_CURRENCY : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private decimal m_CURR_ID = 0;
        private string m_CURR_CODE = string.Empty;
        private string m_CURR_NAME = string.Empty;
        private decimal m_CONV_RATE = 0;
        private decimal m_COUNTRY_ID = 0;
        private string m_IS_ACTIVE = string.Empty;
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


        [DBColumn(Name = "CURR_ID", Storage = "m_CURR_ID", DbType = "107", IsPrimaryKey = true)]
        public decimal CURR_ID
        {
            get { return this.m_CURR_ID; }
            set
            {
                this.m_CURR_ID = value;
                this.NotifyPropertyChanged("CURR_ID");
            }
        }

        [DBColumn(Name = "CURR_CODE", Storage = "m_CURR_CODE", DbType = "126")]
        public string CURR_CODE
        {
            get { return this.m_CURR_CODE; }
            set
            {
                this.m_CURR_CODE = value;
                this.NotifyPropertyChanged("CURR_CODE");
            }
        }

        [DBColumn(Name = "CURR_NAME", Storage = "m_CURR_NAME", DbType = "126")]
        public string CURR_NAME
        {
            get { return this.m_CURR_NAME; }
            set
            {
                this.m_CURR_NAME = value;
                this.NotifyPropertyChanged("CURR_NAME");
            }
        }

        [DBColumn(Name = "CONV_RATE", Storage = "m_CONV_RATE", DbType = "107")]
        public decimal CONV_RATE
        {
            get { return this.m_CONV_RATE; }
            set
            {
                this.m_CONV_RATE = value;
                this.NotifyPropertyChanged("CONV_RATE");
            }
        }

        [DBColumn(Name = "COUNTRY_ID", Storage = "m_COUNTRY_ID", DbType = "107")]
        public decimal COUNTRY_ID
        {
            get { return this.m_COUNTRY_ID; }
            set
            {
                this.m_COUNTRY_ID = value;
                this.NotifyPropertyChanged("COUNTRY_ID");
            }
        }

        [DBColumn(Name = "IS_ACTIVE", Storage = "m_IS_ACTIVE", DbType = "126")]
        public string IS_ACTIVE
        {
            get { return this.m_IS_ACTIVE; }
            set
            {
                this.m_IS_ACTIVE = value;
                this.NotifyPropertyChanged("IS_ACTIVE");
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
