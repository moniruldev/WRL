using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.WRELDC
{
    [DBTable(Name = "CARGO_TRACKING")]
    public partial class dcCARGO_TRACKING : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_CARGO_TRACK_ID = 0;
        private int m_CARGO_ID = 0;
        private DateTime? m_TRACK_DATE = null;
        private string m_TRACK_BY = string.Empty;
        private decimal m_HUB_ID = 0;
        private decimal m_DIST_ID = 0;
        private string m_GPS_LOCATION = string.Empty;

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


        [DBColumn(Name = "CARGO_TRACK_ID", Storage = "m_CARGO_TRACK_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int CARGO_TRACK_ID
        {
            get { return this.m_CARGO_TRACK_ID; }
            set
            {
                this.m_CARGO_TRACK_ID = value;
                this.NotifyPropertyChanged("CARGO_TRACK_ID");
            }
        }

        [DBColumn(Name = "CARGO_ID", Storage = "m_CARGO_ID", DbType = "107")]
        public int CARGO_ID
        {
            get { return this.m_CARGO_ID; }
            set
            {
                this.m_CARGO_ID = value;
                this.NotifyPropertyChanged("CARGO_ID");
            }
        }

        [DBColumn(Name = "TRACK_DATE", Storage = "m_TRACK_DATE", DbType = "106")]
        public DateTime? TRACK_DATE
        {
            get { return this.m_TRACK_DATE; }
            set
            {
                this.m_TRACK_DATE = value;
                this.NotifyPropertyChanged("TRACK_DATE");
            }
        }

        [DBColumn(Name = "TRACK_BY", Storage = "m_TRACK_BY", DbType = "126")]
        public string TRACK_BY
        {
            get { return this.m_TRACK_BY; }
            set
            {
                this.m_TRACK_BY = value;
                this.NotifyPropertyChanged("TRACK_BY");
            }
        }

        [DBColumn(Name = "HUB_ID", Storage = "m_HUB_ID", DbType = "107")]
        public decimal HUB_ID
        {
            get { return this.m_HUB_ID; }
            set
            {
                this.m_HUB_ID = value;
                this.NotifyPropertyChanged("HUB_ID");
            }
        }

        [DBColumn(Name = "DIST_ID", Storage = "m_DIST_ID", DbType = "107")]
        public decimal DIST_ID
        {
            get { return this.m_DIST_ID; }
            set
            {
                this.m_DIST_ID = value;
                this.NotifyPropertyChanged("DIST_ID");
            }
        }

        [DBColumn(Name = "GPS_LOCATION", Storage = "m_GPS_LOCATION", DbType = "126")]
        public string GPS_LOCATION
        {
            get { return this.m_GPS_LOCATION; }
            set
            {
                this.m_GPS_LOCATION = value;
                this.NotifyPropertyChanged("GPS_LOCATION");
            }
        }

        #endregion //properties
    }
}
