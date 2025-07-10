using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
    [Serializable]
    [DBTable(Name = "BATTERY_TYPE_MST")]
    public partial class dcBATTERY_TYPE_MST : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private decimal m_BTY_TYPE_ID = 0;
        private string m_BTY_TYPE_NAME = string.Empty;
        private string m_BTY_TYPE_DESC = string.Empty;
        private decimal m_CREATE_BY = 0;
        private DateTime? m_CREATE_DATE = null;

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


        [DBColumn(Name = "BTY_TYPE_ID", Storage = "m_BTY_TYPE_ID", DbType = "107", IsPrimaryKey = true)]
        public decimal BTY_TYPE_ID
        {
            get { return this.m_BTY_TYPE_ID; }
            set
            {
                this.m_BTY_TYPE_ID = value;
                this.NotifyPropertyChanged("BTY_TYPE_ID");
            }
        }

        [DBColumn(Name = "BTY_TYPE_NAME", Storage = "m_BTY_TYPE_NAME", DbType = "126")]
        public string BTY_TYPE_NAME
        {
            get { return this.m_BTY_TYPE_NAME; }
            set
            {
                this.m_BTY_TYPE_NAME = value;
                this.NotifyPropertyChanged("BTY_TYPE_NAME");
            }
        }

        [DBColumn(Name = "BTY_TYPE_DESC", Storage = "m_BTY_TYPE_DESC", DbType = "126")]
        public string BTY_TYPE_DESC
        {
            get { return this.m_BTY_TYPE_DESC; }
            set
            {
                this.m_BTY_TYPE_DESC = value;
                this.NotifyPropertyChanged("BTY_TYPE_DESC");
            }
        }

        [DBColumn(Name = "CREATE_BY", Storage = "m_CREATE_BY", DbType = "107")]
        public decimal CREATE_BY
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

        #endregion //properties
    }
}
