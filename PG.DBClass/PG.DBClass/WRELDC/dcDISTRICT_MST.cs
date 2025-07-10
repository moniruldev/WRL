using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.WRELDC
{
    [DBTable(Name = "DISTRICT_MST")]
    public partial class dcDISTRICT_MST : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_DIST_ID = 0;
        private string m_DIST_CODE = string.Empty;
        private string m_DIST_NAME = string.Empty;
        private string m_DESCRIPTION = string.Empty;
        private string m_IS_ACTIVE = string.Empty;
        private string m_CREATE_BY = string.Empty;
        private DateTime? m_CREATE_DATE = null;
        private string m_EDIT_BY = string.Empty;
        private string m_EDIT_DATE = string.Empty;

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


        [DBColumn(Name = "DIST_ID", Storage = "m_DIST_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int DIST_ID
        {
            get { return this.m_DIST_ID; }
            set
            {
                this.m_DIST_ID = value;
                this.NotifyPropertyChanged("DIST_ID");
            }
        }

        [DBColumn(Name = "DIST_CODE", Storage = "m_DIST_CODE", DbType = "126")]
        public string DIST_CODE
        {
            get { return this.m_DIST_CODE; }
            set
            {
                this.m_DIST_CODE = value;
                this.NotifyPropertyChanged("DIST_CODE");
            }
        }

        [DBColumn(Name = "DIST_NAME", Storage = "m_DIST_NAME", DbType = "126")]
        public string DIST_NAME
        {
            get { return this.m_DIST_NAME; }
            set
            {
                this.m_DIST_NAME = value;
                this.NotifyPropertyChanged("DIST_NAME");
            }
        }

        [DBColumn(Name = "DESCRIPTION", Storage = "m_DESCRIPTION", DbType = "126")]
        public string DESCRIPTION
        {
            get { return this.m_DESCRIPTION; }
            set
            {
                this.m_DESCRIPTION = value;
                this.NotifyPropertyChanged("DESCRIPTION");
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
        public string EDIT_DATE
        {
            get { return this.m_EDIT_DATE; }
            set
            {
                this.m_EDIT_DATE = value;
                this.NotifyPropertyChanged("EDIT_DATE");
            }
        }

        #endregion //properties
    }
}
