using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.WRELDC
{
    [DBTable(Name = "THANA_TOWN_MST")]
    public partial class dcTHANA_TOWN_MST : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_TOWN_ID = 0;
        private string m_TOWN_NAME = string.Empty;
        private int m_DIST_ID = 0;
        private string m_IS_DEFAULT_TOWN = string.Empty;
        private string m_IS_ACTIVE = string.Empty;
        private string m_CREATE_BY = string.Empty;
        private DateTime? m_CREATE_DATE = null;
        private string m_EDIT_BY = string.Empty;
        private DateTime? m_EDIT_DATE = null;

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


        [DBColumn(Name = "TOWN_ID", Storage = "m_TOWN_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int TOWN_ID
        {
            get { return this.m_TOWN_ID; }
            set
            {
                this.m_TOWN_ID = value;
                this.NotifyPropertyChanged("TOWN_ID");
            }
        }

        [DBColumn(Name = "TOWN_NAME", Storage = "m_TOWN_NAME", DbType = "126")]
        public string TOWN_NAME
        {
            get { return this.m_TOWN_NAME; }
            set
            {
                this.m_TOWN_NAME = value;
                this.NotifyPropertyChanged("TOWN_NAME");
            }
        }

        [DBColumn(Name = "DIST_ID", Storage = "m_DIST_ID", DbType = "107")]
        public int DIST_ID
        {
            get { return this.m_DIST_ID; }
            set
            {
                this.m_DIST_ID = value;
                this.NotifyPropertyChanged("DIST_ID");
            }
        }

        [DBColumn(Name = "IS_DEFAULT_TOWN", Storage = "m_IS_DEFAULT_TOWN", DbType = "126")]
        public string IS_DEFAULT_TOWN
        {
            get { return this.m_IS_DEFAULT_TOWN; }
            set
            {
                this.m_IS_DEFAULT_TOWN = value;
                this.NotifyPropertyChanged("IS_DEFAULT_TOWN");
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

        [DBColumn(Name = "EDIT_DATE", Storage = "m_EDIT_DATE", DbType = "106")]
        public DateTime? EDIT_DATE
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

     public partial class dcTHANA_TOWN_MST
     {
         public string DIST_NAME { get; set; }
     }
}
