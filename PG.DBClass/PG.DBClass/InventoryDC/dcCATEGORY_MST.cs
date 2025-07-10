using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
    [DBTable(Name = "CATEGORY_MST")]
    public partial class dcCATEGORY_MST : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_CAT_ID = 0;
        private string m_CAT_CODE = string.Empty;
        private string m_CAT_NAME = string.Empty;
        private int m_CAT_SL_NO = 0;
        private int m_BATTERY_CAT_ID = 0;
        private string m_IS_ACTIVE = string.Empty;
        private string m_ENTRY_BY = string.Empty;
        private DateTime? m_ENTRY_DATE = null;
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


        [DBColumn(Name = "CAT_ID", Storage = "m_CAT_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int CAT_ID
        {
            get { return this.m_CAT_ID; }
            set
            {
                this.m_CAT_ID = value;
                this.NotifyPropertyChanged("CAT_ID");
            }
        }

        [DBColumn(Name = "CAT_CODE", Storage = "m_CAT_CODE", DbType = "126")]
        public string CAT_CODE
        {
            get { return this.m_CAT_CODE; }
            set
            {
                this.m_CAT_CODE = value;
                this.NotifyPropertyChanged("CAT_CODE");
            }
        }

        [DBColumn(Name = "CAT_NAME", Storage = "m_CAT_NAME", DbType = "126")]
        public string CAT_NAME
        {
            get { return this.m_CAT_NAME; }
            set
            {
                this.m_CAT_NAME = value;
                this.NotifyPropertyChanged("CAT_NAME");
            }
        }

        [DBColumn(Name = "CAT_SL_NO", Storage = "m_CAT_SL_NO", DbType = "107")]
        public int CAT_SL_NO
        {
            get { return this.m_CAT_SL_NO; }
            set
            {
                this.m_CAT_SL_NO = value;
                this.NotifyPropertyChanged("CAT_SL_NO");
            }
        }

        [DBColumn(Name = "BATTERY_CAT_ID", Storage = "m_BATTERY_CAT_ID", DbType = "107")]
        public int BATTERY_CAT_ID
        {
            get { return this.m_BATTERY_CAT_ID; }
            set
            {
                this.m_BATTERY_CAT_ID = value;
                this.NotifyPropertyChanged("BATTERY_CAT_ID");
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
