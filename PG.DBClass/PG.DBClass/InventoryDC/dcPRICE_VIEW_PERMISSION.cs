using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
    [Serializable]
    [DBTable(Name = "PRICE_VIEW_PERMISSION")]
    public partial class dcPRICE_VIEW_PERMISSION : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private decimal m_PRICE_VIEW_ID = 0;
        private int m_ITEM_ID = 0;
        private string m_USERNAME = string.Empty;
        private string m_IS_ACTIVE = string.Empty;
        private string m_ENTRY_BY = string.Empty;
        private DateTime? m_ENTRY_DATE = null;
        private string m_EDIT_BY = string.Empty;
        private DateTime? m_EDIT_DATE = null;

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

        [DBColumn(Name = "PRICE_VIEW_ID", Storage = "m_PRICE_VIEW_ID", DbType = "107", IsPrimaryKey = true)]
        public decimal PRICE_VIEW_ID
        {
            get { return this.m_PRICE_VIEW_ID; }
            set
            {
                this.m_PRICE_VIEW_ID = value;
                this.NotifyPropertyChanged("PRICE_VIEW_ID");
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


        [DBColumn(Name = "USERNAME", Storage = "m_USERNAME", DbType = "126")]
        public string USERNAME
        {
            get { return this.m_USERNAME; }
            set
            {
                this.m_USERNAME = value;
                this.NotifyPropertyChanged("USERNAME");
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

        [DBColumn(Name = "ENTRY_BY", Storage = "m_ENTRY_BY", DbType = "126")]
        public string ENTRY_BY
        {
            get { return this.m_ENTRY_BY; }
            set
            {
                this.m_ENTRY_BY = value;
                this.NotifyPropertyChanged("ENTRY_BY");
            }
        }


        [DBColumn(Name = "ENTRY_DATE", Storage = "m_ENTRY_DATE", DbType = "126")]
        public DateTime? ENTRY_DATE
        {
            get { return this.m_ENTRY_DATE; }
            set
            {
                this.m_ENTRY_DATE = value;
                this.NotifyPropertyChanged("ENTRY_DATE");
            }
        }

        [DBColumn(Name = "EDIT_BY", Storage = "m_EDIT_BY", DbType = "126")]
        public string EDIT_BY
        {
            get { return this.m_EDIT_BY; }
            set
            {
                this.m_EDIT_BY = value;
                this.NotifyPropertyChanged("EDIT_BY");
            }
        }

        [DBColumn(Name = "EDIT_DATE", Storage = "m_EDIT_DATE", DbType = "126")]
        public DateTime? EDIT_DATE
        {
            get { return this.m_EDIT_DATE; }
            set
            {
                this.m_EDIT_DATE = value;
                this.NotifyPropertyChanged("EDIT_DATE");
            }
        }

        #endregion
    }
}
