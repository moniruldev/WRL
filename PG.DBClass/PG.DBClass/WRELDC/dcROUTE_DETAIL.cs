using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.WRELDC
{
    [DBTable(Name = "ROUTE_DETAIL")]
    public partial class dcROUTE_DETAIL : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_ROUTE_DETAIL_ID = 0;
        private int m_ROUTE_ID = 0;
        private int m_DIST_ID = 0;
        private int m_TOWN_ID = 0;
        private string m_REMARKS = string.Empty;
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


        [DBColumn(Name = "ROUTE_DETAIL_ID", Storage = "m_ROUTE_DETAIL_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int ROUTE_DETAIL_ID
        {
            get { return this.m_ROUTE_DETAIL_ID; }
            set
            {
                this.m_ROUTE_DETAIL_ID = value;
                this.NotifyPropertyChanged("ROUTE_DETAIL_ID");
            }
        }

        [DBColumn(Name = "ROUTE_ID", Storage = "m_ROUTE_ID", DbType = "107")]
        public int ROUTE_ID
        {
            get { return this.m_ROUTE_ID; }
            set
            {
                this.m_ROUTE_ID = value;
                this.NotifyPropertyChanged("ROUTE_ID");
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

        [DBColumn(Name = "TOWN_ID", Storage = "m_TOWN_ID", DbType = "107")]
        public int TOWN_ID
        {
            get { return this.m_TOWN_ID; }
            set
            {
                this.m_TOWN_ID = value;
                this.NotifyPropertyChanged("TOWN_ID");
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
}
