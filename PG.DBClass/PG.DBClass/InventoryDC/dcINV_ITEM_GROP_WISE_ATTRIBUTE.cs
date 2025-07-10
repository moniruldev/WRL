using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
    [DBTable(Name = "INV_ITEM_GROP_WISE_ATTRIBUTE")]
    public partial class dcINV_ITEM_GROP_WISE_ATTRIBUTE : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_ITEM_GROUP_ATT_ID = 0;
        private int m_ITEM_GROUP_ID = 0;
        private int m_ITEM_SIZE_ID = 0;
        private int m_ITEM_COLOR_ID = 0;
        private int m_ITEM_GENERATION_ID = 0;
        private int m_ITEM_BRAND_ID = 0;
        private int m_CAT_ID = 0;
        private string m_CREATE_BY = string.Empty;
        private DateTime? m_CREATE_DATE = null;
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


        [DBColumn(Name = "ITEM_GROUP_ATT_ID", Storage = "m_ITEM_GROUP_ATT_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int ITEM_GROUP_ATT_ID
        {
            get { return this.m_ITEM_GROUP_ATT_ID; }
            set
            {
                this.m_ITEM_GROUP_ATT_ID = value;
                this.NotifyPropertyChanged("ITEM_GROUP_ATT_ID");
            }
        }

        [DBColumn(Name = "ITEM_GROUP_ID", Storage = "m_ITEM_GROUP_ID", DbType = "107")]
        public int ITEM_GROUP_ID
        {
            get { return this.m_ITEM_GROUP_ID; }
            set
            {
                this.m_ITEM_GROUP_ID = value;
                this.NotifyPropertyChanged("ITEM_GROUP_ID");
            }
        }

        [DBColumn(Name = "ITEM_SIZE_ID", Storage = "m_ITEM_SIZE_ID", DbType = "107")]
        public int ITEM_SIZE_ID
        {
            get { return this.m_ITEM_SIZE_ID; }
            set
            {
                this.m_ITEM_SIZE_ID = value;
                this.NotifyPropertyChanged("ITEM_SIZE_ID");
            }
        }

        [DBColumn(Name = "ITEM_COLOR_ID", Storage = "m_ITEM_COLOR_ID", DbType = "107")]
        public int ITEM_COLOR_ID
        {
            get { return this.m_ITEM_COLOR_ID; }
            set
            {
                this.m_ITEM_COLOR_ID = value;
                this.NotifyPropertyChanged("ITEM_COLOR_ID");
            }
        }

        [DBColumn(Name = "ITEM_GENERATION_ID", Storage = "m_ITEM_GENERATION_ID", DbType = "107")]
        public int ITEM_GENERATION_ID
        {
            get { return this.m_ITEM_GENERATION_ID; }
            set
            {
                this.m_ITEM_GENERATION_ID = value;
                this.NotifyPropertyChanged("ITEM_GENERATION_ID");
            }
        }

        [DBColumn(Name = "ITEM_BRAND_ID", Storage = "m_ITEM_BRAND_ID", DbType = "107")]
        public int ITEM_BRAND_ID
        {
            get { return this.m_ITEM_BRAND_ID; }
            set
            {
                this.m_ITEM_BRAND_ID = value;
                this.NotifyPropertyChanged("ITEM_BRAND_ID");
            }
        }

        [DBColumn(Name = "CAT_ID", Storage = "m_CAT_ID", DbType = "107")]
        public int CAT_ID
        {
            get { return this.m_CAT_ID; }
            set
            {
                this.m_CAT_ID = value;
                this.NotifyPropertyChanged("CAT_ID");
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

        #endregion //properties
    }

     public partial class dcINV_ITEM_GROP_WISE_ATTRIBUTE
     {
         public string ITEM_GROUP_NAME { get; set; }
         public string ITEM_SIZE { get; set; }
         public string BRAND_NAME { get; set; }
         public string COLOR_NAME { get; set; }
         public string GENERATION_NAME { get; set; }
         public string CAT_NAME { get; set; }
     }
}
