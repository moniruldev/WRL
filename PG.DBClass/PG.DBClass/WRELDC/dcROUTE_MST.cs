using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.WRELDC
{
    [DBTable(Name = "ROUTE_MST")]
    public partial class dcROUTE_MST : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_ROUTE_ID = 0;
        private string m_ROUTE_NAME = string.Empty;
        private string m_REMARKS = string.Empty;
        private decimal m_STARTING_DIST_ID = 0;
        private string m_DESTINATION_DIST_ID = string.Empty;
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


        [DBColumn(Name = "ROUTE_ID", Storage = "m_ROUTE_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int ROUTE_ID
        {
            get { return this.m_ROUTE_ID; }
            set
            {
                this.m_ROUTE_ID = value;
                this.NotifyPropertyChanged("ROUTE_ID");
            }
        }

        [DBColumn(Name = "ROUTE_NAME", Storage = "m_ROUTE_NAME", DbType = "126")]
        public string ROUTE_NAME
        {
            get { return this.m_ROUTE_NAME; }
            set
            {
                this.m_ROUTE_NAME = value;
                this.NotifyPropertyChanged("ROUTE_NAME");
            }
        }

        [DBColumn(Name = "REMARKS", Storage = "m_REMARKS", DbType = "126")]
        public string REMARKS
        {
            get { return this.m_REMARKS; }
            set
            {
                this.m_REMARKS = value;
                this.NotifyPropertyChanged("REMARKS");
            }
        }

        [DBColumn(Name = "STARTING_DIST_ID", Storage = "m_STARTING_DIST_ID", DbType = "107")]
        public decimal STARTING_DIST_ID
        {
            get { return this.m_STARTING_DIST_ID; }
            set
            {
                this.m_STARTING_DIST_ID = value;
                this.NotifyPropertyChanged("STARTING_DIST_ID");
            }
        }

        [DBColumn(Name = "DESTINATION_DIST_ID", Storage = "m_DESTINATION_DIST_ID", DbType = "126")]
        public string DESTINATION_DIST_ID
        {
            get { return this.m_DESTINATION_DIST_ID; }
            set
            {
                this.m_DESTINATION_DIST_ID = value;
                this.NotifyPropertyChanged("DESTINATION_DIST_ID");
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
}
