using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.InventoryDC
{
    [DBTable(Name = "QC_ITEM_CRITERION_CONFIG")]
    public partial class dcQC_ITEM_CRITERION_CONFIG : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_ITEM_CRITERION_ID = 0;
        private int m_ITEM_ID = 0;
        private string m_ITEM_CODE = string.Empty;
        private int m_ITEM_CLASS_ID = 0;
        private int m_ITEM_GROUP_ID = 0;
        private int m_ITEM_TYPE_ID = 0;
        private int m_CRITERION_ID = 0;
        private int m_CREATE_BY = 0;
        private DateTime? m_CREATE_DATE = null;
        private int m_UPDATE_BY = 0;
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


        [DBColumn(Name = "ITEM_CRITERION_ID", Storage = "m_ITEM_CRITERION_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int ITEM_CRITERION_ID
        {
            get { return this.m_ITEM_CRITERION_ID; }
            set
            {
                this.m_ITEM_CRITERION_ID = value;
                this.NotifyPropertyChanged("ITEM_CRITERION_ID");
            }
        }

        [DBColumn(Name = "ITEM_ID", Storage = "m_ITEM_ID", DbType = "107")]
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

        [DBColumn(Name = "ITEM_CLASS_ID", Storage = "m_ITEM_CLASS_ID", DbType = "107")]
        public int ITEM_CLASS_ID
        {
            get { return this.m_ITEM_CLASS_ID; }
            set
            {
                this.m_ITEM_CLASS_ID = value;
                this.NotifyPropertyChanged("ITEM_CLASS_ID");
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

        [DBColumn(Name = "ITEM_TYPE_ID", Storage = "m_ITEM_TYPE_ID", DbType = "107")]
        public int ITEM_TYPE_ID
        {
            get { return this.m_ITEM_TYPE_ID; }
            set
            {
                this.m_ITEM_TYPE_ID = value;
                this.NotifyPropertyChanged("ITEM_TYPE_ID");
            }
        }

        [DBColumn(Name = "CRITERION_ID", Storage = "m_CRITERION_ID", DbType = "107")]
        public int CRITERION_ID
        {
            get { return this.m_CRITERION_ID; }
            set
            {
                this.m_CRITERION_ID = value;
                this.NotifyPropertyChanged("CRITERION_ID");
            }
        }

        [DBColumn(Name = "CREATE_BY", Storage = "m_CREATE_BY", DbType = "107")]
        public int CREATE_BY
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

        [DBColumn(Name = "UPDATE_BY", Storage = "m_UPDATE_BY", DbType = "107")]
        public int UPDATE_BY
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
