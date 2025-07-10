using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
    [DBTable(Name = "LEAD_VALIDATION_MONTHLY")]
    public partial class dcLEAD_VALIDATION_MONTHLY : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private decimal m_LV_ID = 0;
        private decimal m_LV_MONTH = 0;
        private decimal m_LV_YEAR = 0;
        private DateTime? m_LV_ENTRY_DATE = null;
        private decimal m_LV_ENTRY_BY = 0;
        private decimal m_LV_MONTHLY_TOTAL = 0;
        private decimal m_LV_INNER_MONTH_TOTAL = 0;
        private decimal m_LV_INNER_MONTH_DATYS = 0;
        private decimal m_LV_OPENING_RM = 0;
        private decimal m_LV_OPENING_FG = 0;
        private string m_REMARKS = string.Empty;

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


        [DBColumn(Name = "LV_ID", Storage = "m_LV_ID", DbType = "107", IsPrimaryKey = true)]
        public decimal LV_ID
        {
            get { return this.m_LV_ID; }
            set
            {
                this.m_LV_ID = value;
                this.NotifyPropertyChanged("LV_ID");
            }
        }

        [DBColumn(Name = "LV_MONTH", Storage = "m_LV_MONTH", DbType = "107")]
        public decimal LV_MONTH
        {
            get { return this.m_LV_MONTH; }
            set
            {
                this.m_LV_MONTH = value;
                this.NotifyPropertyChanged("LV_MONTH");
            }
        }

        [DBColumn(Name = "LV_YEAR", Storage = "m_LV_YEAR", DbType = "107")]
        public decimal LV_YEAR
        {
            get { return this.m_LV_YEAR; }
            set
            {
                this.m_LV_YEAR = value;
                this.NotifyPropertyChanged("LV_YEAR");
            }
        }

        [DBColumn(Name = "LV_ENTRY_DATE", Storage = "m_LV_ENTRY_DATE", DbType = "106")]
        public DateTime? LV_ENTRY_DATE
        {
            get { return this.m_LV_ENTRY_DATE; }
            set
            {
                this.m_LV_ENTRY_DATE = value;
                this.NotifyPropertyChanged("LV_ENTRY_DATE");
            }
        }

        [DBColumn(Name = "LV_ENTRY_BY", Storage = "m_LV_ENTRY_BY", DbType = "107")]
        public decimal LV_ENTRY_BY
        {
            get { return this.m_LV_ENTRY_BY; }
            set
            {
                this.m_LV_ENTRY_BY = value;
                this.NotifyPropertyChanged("LV_ENTRY_BY");
            }
        }

        [DBColumn(Name = "LV_MONTHLY_TOTAL", Storage = "m_LV_MONTHLY_TOTAL", DbType = "107")]
        public decimal LV_MONTHLY_TOTAL
        {
            get { return this.m_LV_MONTHLY_TOTAL; }
            set
            {
                this.m_LV_MONTHLY_TOTAL = value;
                this.NotifyPropertyChanged("LV_MONTHLY_TOTAL");
            }
        }

        [DBColumn(Name = "LV_INNER_MONTH_TOTAL", Storage = "m_LV_INNER_MONTH_TOTAL", DbType = "107")]
        public decimal LV_INNER_MONTH_TOTAL
        {
            get { return this.m_LV_INNER_MONTH_TOTAL; }
            set
            {
                this.m_LV_INNER_MONTH_TOTAL = value;
                this.NotifyPropertyChanged("LV_INNER_MONTH_TOTAL");
            }
        }

        [DBColumn(Name = "LV_INNER_MONTH_DATYS", Storage = "m_LV_INNER_MONTH_DATYS", DbType = "107")]
        public decimal LV_INNER_MONTH_DATYS
        {
            get { return this.m_LV_INNER_MONTH_DATYS; }
            set
            {
                this.m_LV_INNER_MONTH_DATYS = value;
                this.NotifyPropertyChanged("LV_INNER_MONTH_DATYS");
            }
        }

        [DBColumn(Name = "LV_OPENING_RM", Storage = "m_LV_OPENING_RM", DbType = "107")]
        public decimal LV_OPENING_RM
        {
            get { return this.m_LV_OPENING_RM; }
            set
            {
                this.m_LV_OPENING_RM = value;
                this.NotifyPropertyChanged("LV_OPENING_RM");
            }
        }

        [DBColumn(Name = "LV_OPENING_FG", Storage = "m_LV_OPENING_FG", DbType = "107")]
        public decimal LV_OPENING_FG
        {
            get { return this.m_LV_OPENING_FG; }
            set
            {
                this.m_LV_OPENING_FG = value;
                this.NotifyPropertyChanged("LV_OPENING_FG");
            }
        }

        [DBColumn(Name = "REMARKS", Storage = "m_REMARKS", DbType = "126")]
        public string REMARKS
        {
            get { return this.m_REMARKS; }
            set
            {
                this.m_REMARKS = value;
                this.NotifyPropertyChanged("REMARKS");
            }
        }

        #endregion //properties
    }
}
