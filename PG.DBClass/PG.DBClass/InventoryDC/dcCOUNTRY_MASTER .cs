using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "COUNTRY_MASTER")]
    public partial class dcCOUNTRY_MASTER : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private decimal m_COUNTRY_ID = 0;
        private string m_COUNTRY_CODE = string.Empty;
        private string m_COUNTRY_NAME = string.Empty;
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


        [DBColumn(Name = "COUNTRY_ID", Storage = "m_COUNTRY_ID", DbType = "107", IsPrimaryKey = true)]
        public decimal COUNTRY_ID
        {
            get { return this.m_COUNTRY_ID; }
            set
            {
                this.m_COUNTRY_ID = value;
                this.NotifyPropertyChanged("COUNTRY_ID");
            }
        }

        [DBColumn(Name = "COUNTRY_CODE", Storage = "m_COUNTRY_CODE", DbType = "126")]
        public string COUNTRY_CODE
        {
            get { return this.m_COUNTRY_CODE; }
            set
            {
                this.m_COUNTRY_CODE = value;
                this.NotifyPropertyChanged("COUNTRY_CODE");
            }
        }

        [DBColumn(Name = "COUNTRY_NAME", Storage = "m_COUNTRY_NAME", DbType = "126")]
        public string COUNTRY_NAME
        {
            get { return this.m_COUNTRY_NAME; }
            set
            {
                this.m_COUNTRY_NAME = value;
                this.NotifyPropertyChanged("COUNTRY_NAME");
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
