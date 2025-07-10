using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.SecurityDC
{
     [Serializable]
     [DBTable(Name = "DATATRANSFER_MASTER_DETAIL")]
    public partial class dcDATATRANSFER_MASTER_DETAIL : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_ID = 0;
        private string m_MASTER_TABLE = string.Empty;
        private string m_MASTER_TABLE_FILTER = string.Empty;
        private string m_MASTER_TO_DETAIL = string.Empty;
        private string m_DETAIL_TABLE = string.Empty;
        private string m_DETAIL_TO_MASTER = string.Empty;
        private string m_IS_ACTIVE = string.Empty;

        #endregion
        
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

        [DBColumn(Name = "ID", Storage = "m_ID", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true, DbType = "Int NOT NULL IDENTITY")]
        public int ID
        {
            get { return this.m_ID; }
            set
            {
                this.m_ID = value;
                this.NotifyPropertyChanged("ID");
            }
        }

        [DBColumn(Name = "MASTER_TABLE", Storage = "m_MASTER_TABLE", DbType = "126")]
        public string MASTER_TABLE
        {
            get { return this.m_MASTER_TABLE; }
            set
            {
                this.m_MASTER_TABLE = value;
                this.NotifyPropertyChanged("MASTER_TABLE");
            }
        }

        [DBColumn(Name = "MASTER_TABLE_FILTER", Storage = "m_MASTER_TABLE_FILTER", DbType = "126")]
        public string MASTER_TABLE_FILTER
        {
            get { return this.m_MASTER_TABLE_FILTER; }
            set
            {
                this.m_MASTER_TABLE_FILTER = value;
                this.NotifyPropertyChanged("MASTER_TABLE_FILTER");
            }
        }


        [DBColumn(Name = "MASTER_TO_DETAIL", Storage = "m_MASTER_TO_DETAIL", DbType = "126")]
        public string MASTER_TO_DETAIL
        {
            get { return this.m_MASTER_TO_DETAIL; }
            set
            {
                this.m_MASTER_TO_DETAIL = value;
                this.NotifyPropertyChanged("MASTER_TO_DETAIL");
            }
        }


        [DBColumn(Name = "DETAIL_TABLE", Storage = "m_DETAIL_TABLE", DbType = "126")]
        public string DETAIL_TABLE
        {
            get { return this.m_DETAIL_TABLE; }
            set
            {
                this.m_DETAIL_TABLE = value;
                this.NotifyPropertyChanged("DETAIL_TABLE");
            }
        }

        [DBColumn(Name = "DETAIL_TO_MASTER", Storage = "m_DETAIL_TO_MASTER", DbType = "126")]
        public string DETAIL_TO_MASTER
        {
            get { return this.m_DETAIL_TO_MASTER; }
            set
            {
                this.m_DETAIL_TO_MASTER = value;
                this.NotifyPropertyChanged("DETAIL_TO_MASTER");
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

        #endregion
    }

}
