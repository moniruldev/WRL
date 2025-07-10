using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
    [DBTable(Name = "INV_ITEM_SIZE")]
    public partial class dcINV_ITEM_SIZE : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_ITEM_SIZE_ID = 0;
        private string m_ITEM_SIZE = string.Empty;
        private string m_SIZE_DESC = string.Empty;
        private string m_CREATE_BY = string.Empty;
        private DateTime? m_CREATE_DATE = null;
        private string m_IS_ACTIVE = string.Empty;

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


        [DBColumn(Name = "ITEM_SIZE_ID", Storage = "m_ITEM_SIZE_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int ITEM_SIZE_ID
        {
            get { return this.m_ITEM_SIZE_ID; }
            set
            {
                this.m_ITEM_SIZE_ID = value;
                this.NotifyPropertyChanged("ITEM_SIZE_ID");
            }
        }

        [DBColumn(Name = "ITEM_SIZE", Storage = "m_ITEM_SIZE", DbType = "126")]
        public string ITEM_SIZE
        {
            get { return this.m_ITEM_SIZE; }
            set
            {
                this.m_ITEM_SIZE = value;
                this.NotifyPropertyChanged("ITEM_SIZE");
            }
        }

        [DBColumn(Name = "SIZE_DESC", Storage = "m_SIZE_DESC", DbType = "126")]
        public string SIZE_DESC
        {
            get { return this.m_SIZE_DESC; }
            set
            {
                this.m_SIZE_DESC = value;
                this.NotifyPropertyChanged("SIZE_DESC");
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

        #endregion //properties
    }
}
