using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
    [DBTable(Name = "INV_ITEM_GENERATION")]
    public partial class dcINV_ITEM_GENERATION : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_ITEM_GENERATION_ID = 0;
        private string m_GENERATION_NAME = string.Empty;
        private string m_GENERATION_DESC = string.Empty;
        private string m_CREATE_BY = string.Empty;
        private DateTime? m_CREATE_DATE = null;
        private string m_IS_ACTIVE = string.Empty;
        private string m_UPDATE_BY = string.Empty;
        private DateTime? m_UPDATE_DATE = null;

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


        [DBColumn(Name = "ITEM_GENERATION_ID", Storage = "m_ITEM_GENERATION_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int ITEM_GENERATION_ID
        {
            get { return this.m_ITEM_GENERATION_ID; }
            set
            {
                this.m_ITEM_GENERATION_ID = value;
                this.NotifyPropertyChanged("ITEM_GENERATION_ID");
            }
        }

        [DBColumn(Name = "GENERATION_NAME", Storage = "m_GENERATION_NAME", DbType = "126")]
        public string GENERATION_NAME
        {
            get { return this.m_GENERATION_NAME; }
            set
            {
                this.m_GENERATION_NAME = value;
                this.NotifyPropertyChanged("GENERATION_NAME");
            }
        }

        [DBColumn(Name = "GENERATION_DESC", Storage = "m_GENERATION_DESC", DbType = "126")]
        public string GENERATION_DESC
        {
            get { return this.m_GENERATION_DESC; }
            set
            {
                this.m_GENERATION_DESC = value;
                this.NotifyPropertyChanged("GENERATION_DESC");
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

        #endregion //properties
    }
}
