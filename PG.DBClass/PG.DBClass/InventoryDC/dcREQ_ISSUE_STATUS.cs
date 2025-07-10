using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
    [DBTable(Name = "REQ_ISSUE_STATUS")]
    public partial class dcREQ_ISSUE_STATUS : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_STATUS_ID = 0;
        private string m_STATUS_NAME = string.Empty;
        private string m_STATUS_TYPE = string.Empty;
        private string m_CREATE_BY = string.Empty;
        private string m_CREATE_DATE = string.Empty;
        private string m_UPDATE_BY = string.Empty;
        private string m_UPDATE_DATE = string.Empty;
        private string m_IS_ACTIVE = string.Empty;
        private int m_SL_NO = 0;

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


        [DBColumn(Name = "STATUS_ID", Storage = "m_STATUS_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int STATUS_ID
        {
            get { return this.m_STATUS_ID; }
            set
            {
                this.m_STATUS_ID = value;
                this.NotifyPropertyChanged("STATUS_ID");
            }
        }

        [DBColumn(Name = "STATUS_NAME", Storage = "m_STATUS_NAME", DbType = "126")]
        public string STATUS_NAME
        {
            get { return this.m_STATUS_NAME; }
            set
            {
                this.m_STATUS_NAME = value;
                this.NotifyPropertyChanged("STATUS_NAME");
            }
        }

        [DBColumn(Name = "STATUS_TYPE", Storage = "m_STATUS_TYPE", DbType = "126")]
        public string STATUS_TYPE
        {
            get { return this.m_STATUS_TYPE; }
            set
            {
                this.m_STATUS_TYPE = value;
                this.NotifyPropertyChanged("STATUS_TYPE");
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

        [DBColumn(Name = "CREATE_DATE", Storage = "m_CREATE_DATE", DbType = "126")]
        public string CREATE_DATE
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

        [DBColumn(Name = "UPDATE_DATE", Storage = "m_UPDATE_DATE", DbType = "126")]
        public string UPDATE_DATE
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

        [DBColumn(Name = "SL_NO", Storage = "m_SL_NO", DbType = "107")]
        public int SL_NO
        {
            get { return this.m_SL_NO; }
            set
            {
                this.m_SL_NO = value;
                this.NotifyPropertyChanged("SL_NO");
            }
        }

        #endregion //properties
    }
}
