using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
    [DBTable(Name = "INV_ITEM_SNS_MST")]
    public partial class dcINV_ITEM_SNS_MST : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_ITEM_SNS_ID = 0;
        private string m_ITEM_SNS_NAME = string.Empty;
        private string m_ITEM_SNS_DESC = string.Empty;
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


        [DBColumn(Name = "ITEM_SNS_ID", Storage = "m_ITEM_SNS_ID", DbType = "107", IsPrimaryKey = true)]
        public int ITEM_SNS_ID
        {
            get { return this.m_ITEM_SNS_ID; }
            set
            {
                this.m_ITEM_SNS_ID = value;
                this.NotifyPropertyChanged("ITEM_SNS_ID");
            }
        }

        [DBColumn(Name = "ITEM_SNS_NAME", Storage = "m_ITEM_SNS_NAME", DbType = "126")]
        public string ITEM_SNS_NAME
        {
            get { return this.m_ITEM_SNS_NAME; }
            set
            {
                this.m_ITEM_SNS_NAME = value;
                this.NotifyPropertyChanged("ITEM_SNS_NAME");
            }
        }

        [DBColumn(Name = "ITEM_SNS_DESC", Storage = "m_ITEM_SNS_DESC", DbType = "126")]
        public string ITEM_SNS_DESC
        {
            get { return this.m_ITEM_SNS_DESC; }
            set
            {
                this.m_ITEM_SNS_DESC = value;
                this.NotifyPropertyChanged("ITEM_SNS_DESC");
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
