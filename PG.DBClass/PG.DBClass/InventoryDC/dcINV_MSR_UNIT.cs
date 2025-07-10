using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "INV_MSR_UNIT")]
   public class dcINV_MSR_UNIT : DBBaseClass, INotifyPropertyChanged 
    {
        #region private members

        private string m_MSR_ID = string.Empty;
        private string m_MSR_ABBR = string.Empty;
        private string m_MSR_SYMBOL = string.Empty;
        private string m_MSR_NAME = string.Empty;
        private string m_CREATE_BY = string.Empty;
        private DateTime? m_CREATE_DATE = null;
        private string m_UPDATE_BY = string.Empty;
        private DateTime? m_UPDATE_DATE = null;
        private int? m_CONVERT_UNIT_VALUE = 0;

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


        [DBColumn(Name = "MSR_ID", Storage = "m_MSR_ID", DbType = "126", IsPrimaryKey = true)]
        public string MSR_ID
        {
            get { return this.m_MSR_ID; }
            set
            {
                this.m_MSR_ID = value;
                this.NotifyPropertyChanged("MSR_ID");
            }
        }

        [DBColumn(Name = "MSR_ABBR", Storage = "m_MSR_ABBR", DbType = "126")]
        public string MSR_ABBR
        {
            get { return this.m_MSR_ABBR; }
            set
            {
                this.m_MSR_ABBR = value;
                this.NotifyPropertyChanged("MSR_ABBR");
            }
        }

        [DBColumn(Name = "MSR_SYMBOL", Storage = "m_MSR_SYMBOL", DbType = "126")]
        public string MSR_SYMBOL
        {
            get { return this.m_MSR_SYMBOL; }
            set
            {
                this.m_MSR_SYMBOL = value;
                this.NotifyPropertyChanged("MSR_SYMBOL");
            }
        }

        [DBColumn(Name = "MSR_NAME", Storage = "m_MSR_NAME", DbType = "126")]
        public string MSR_NAME
        {
            get { return this.m_MSR_NAME; }
            set
            {
                this.m_MSR_NAME = value;
                this.NotifyPropertyChanged("MSR_NAME");
            }
        }

        [DBColumn(Name = "CREATE_BY", Storage = "m_CREATE_BY", DbType = "126")]
        public string CREATE_BY
        {
            get { return this.m_CREATE_BY; }
            set
            {
                this.m_CREATE_BY = value;
                this.NotifyPropertyChanged("CREATE_BY");
            }
        }

        [DBColumn(Name = "CREATE_DATE", Storage = "m_CREATE_DATE", DbType = "106")]
        public DateTime? CREATE_DATE
        {
            get { return this.m_CREATE_DATE; }
            set
            {
                this.m_CREATE_DATE = value;
                this.NotifyPropertyChanged("CREATE_DATE");
            }
        }

        [DBColumn(Name = "UPDATE_BY", Storage = "m_UPDATE_BY", DbType = "126")]
        public string UPDATE_BY
        {
            get { return this.m_UPDATE_BY; }
            set
            {
                this.m_UPDATE_BY = value;
                this.NotifyPropertyChanged("UPDATE_BY");
            }
        }

        [DBColumn(Name = "UPDATE_DATE", Storage = "m_UPDATE_DATE", DbType = "106")]
        public DateTime? UPDATE_DATE
        {
            get { return this.m_UPDATE_DATE; }
            set
            {
                this.m_UPDATE_DATE = value;
                this.NotifyPropertyChanged("UPDATE_DATE");
            }
        }

        [DBColumn(Name = "CONVERT_UNIT_VALUE", Storage = "m_CONVERT_UNIT_VALUE", DbType = "112")]
        public int? CONVERT_UNIT_VALUE
        {
            get { return this.m_CONVERT_UNIT_VALUE; }
            set
            {
                this.m_CONVERT_UNIT_VALUE = value;
                this.NotifyPropertyChanged("CONVERT_UNIT_VALUE");
            }
        }

        #endregion //properties 
    }
}
