using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;


namespace PG.DBClass.InventoryDC
{
     [Serializable]
     [DBTable(Name = "ITEM_MASTER_PRICE_UPDATE_LOG")]
    public partial class dcITEM_MASTER_PRICE_UPDATE_LOG : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private decimal m_ID = 0;
        private int m_ITEM_ID = 0;
        private string m_ITEM_CODE = string.Empty;
        private decimal m_OLD_WEIGHTED_AVERAGE_PRICE = 0;
        private decimal m_NEW_WEIGHTED_AVERAGE_PRICE = 0;
        private string m_CHANGE_BY = null;

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


        [DBColumn(Name = "ID", Storage = "m_ID", DbType = "107", IsPrimaryKey = true)]
        public decimal ID
        {
            get { return this.m_ID; }
            set
            {
                this.m_ID = value;
                this.NotifyPropertyChanged("ID");
            }
        }

        [DBColumn(Name = "ITEM_ID", Storage = "m_ITEM_ID", DbType = "126")]
        public int ITEM_ID
        {
            get { return this.m_ITEM_ID; }
            set
            {
                this.m_ITEM_ID = value;
                this.NotifyPropertyChanged("ITEM_ID");
            }
        }

        [DBColumn(Name = "ITEM_CODE", Storage = "m_ITEM_CODE", DbType = "126")]
        public string ITEM_CODE
        {
            get { return this.m_ITEM_CODE; }
            set
            {
                this.m_ITEM_CODE = value;
                this.NotifyPropertyChanged("ITEM_CODE");
            }
        }


        [DBColumn(Name = "OLD_WEIGHTED_AVERAGE_PRICE", Storage = "m_OLD_WEIGHTED_AVERAGE_PRICE", DbType = "126")]
        public decimal OLD_WEIGHTED_AVERAGE_PRICE
        {
            get { return this.m_OLD_WEIGHTED_AVERAGE_PRICE; }
            set
            {
                this.m_OLD_WEIGHTED_AVERAGE_PRICE = value;
                this.NotifyPropertyChanged("OLD_WEIGHTED_AVERAGE_PRICE");
            }
        }


        [DBColumn(Name = "NEW_WEIGHTED_AVERAGE_PRICE", Storage = "m_NEW_WEIGHTED_AVERAGE_PRICE", DbType = "126")]
        public decimal NEW_WEIGHTED_AVERAGE_PRICE
        {
            get { return this.m_NEW_WEIGHTED_AVERAGE_PRICE; }
            set
            {
                this.m_NEW_WEIGHTED_AVERAGE_PRICE = value;
                this.NotifyPropertyChanged("NEW_WEIGHTED_AVERAGE_PRICE");
            }
        }


        [DBColumn(Name = "CHANGE_BY", Storage = "m_CHANGE_BY", DbType = "126")]
        public string CHANGE_BY
        {
            get { return this.m_CHANGE_BY; }
            set
            {
                this.m_CHANGE_BY = value;
                this.NotifyPropertyChanged("CHANGE_BY");
            }
        }

        #endregion //properties
    }
}
