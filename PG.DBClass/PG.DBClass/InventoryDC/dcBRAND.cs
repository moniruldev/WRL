using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
    [DBTable(Name = "BRAND")]
    public partial class dcBRAND : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_BRAND_ID = 0;
        private string m_BRAND_NAME = string.Empty;
        private int m_ITEM_SEGMENT_ID = 0;
        private int m_BRAND_SL_NO = 0;
        private string m_BRAND_CODE = string.Empty;
        private string m_ENTRY_BY = string.Empty;
        private DateTime? m_ENTRY_DATE = null;
        private string m_UPDATE_BY = string.Empty;
        private DateTime? m_UPDATE_DATE = null;
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


        [DBColumn(Name = "BRAND_ID", Storage = "m_BRAND_ID", DbType = "107", IsPrimaryKey = true, IsDbGenerated = true, IsIdentity = true)]
        public int BRAND_ID
        {
            get { return this.m_BRAND_ID; }
            set
            {
                this.m_BRAND_ID = value;
                this.NotifyPropertyChanged("BRAND_ID");
            }
        }

        [DBColumn(Name = "BRAND_NAME", Storage = "m_BRAND_NAME", DbType = "126")]
        public string BRAND_NAME
        {
            get { return this.m_BRAND_NAME; }
            set
            {
                this.m_BRAND_NAME = value;
                this.NotifyPropertyChanged("BRAND_NAME");
            }
        }

        [DBColumn(Name = "ITEM_SEGMENT_ID", Storage = "m_ITEM_SEGMENT_ID", DbType = "107")]
        public int ITEM_SEGMENT_ID
        {
            get { return this.m_ITEM_SEGMENT_ID; }
            set
            {
                this.m_ITEM_SEGMENT_ID = value;
                this.NotifyPropertyChanged("ITEM_SEGMENT_ID");
            }
        }

        [DBColumn(Name = "BRAND_SL_NO", Storage = "m_BRAND_SL_NO", DbType = "107")]
        public int BRAND_SL_NO
        {
            get { return this.m_BRAND_SL_NO; }
            set
            {
                this.m_BRAND_SL_NO = value;
                this.NotifyPropertyChanged("BRAND_SL_NO");
            }
        }

        [DBColumn(Name = "BRAND_CODE", Storage = "m_BRAND_CODE", DbType = "126")]
        public string BRAND_CODE
        {
            get { return this.m_BRAND_CODE; }
            set
            {
                this.m_BRAND_CODE = value;
                this.NotifyPropertyChanged("BRAND_CODE");
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

     public partial class dcBRAND
     {
         public int ITEM_BRAND_ID { get; set; }
     }
}
