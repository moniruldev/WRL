using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.WRELDC
{
    [DBTable(Name = "CN_TRACKING")]
    public partial class dcCN_TRACKING : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_CN_TRACK_ID = 0;
        private int m_CN_ID = 0;
        private DateTime? m_CN_TRACK_DATE = null;
        private string m_CN_TRACK_BY = string.Empty;
        private decimal m_HUB_ID = 0;
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


        [DBColumn(Name = "CN_TRACK_ID", Storage = "m_CN_TRACK_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int CN_TRACK_ID
        {
            get { return this.m_CN_TRACK_ID; }
            set
            {
                this.m_CN_TRACK_ID = value;
                this.NotifyPropertyChanged("CN_TRACK_ID");
            }
        }

        [DBColumn(Name = "CN_ID", Storage = "m_CN_ID", DbType = "107")]
        public int CN_ID
        {
            get { return this.m_CN_ID; }
            set
            {
                this.m_CN_ID = value;
                this.NotifyPropertyChanged("CN_ID");
            }
        }

        [DBColumn(Name = "CN_TRACK_DATE", Storage = "m_CN_TRACK_DATE", DbType = "106")]
        public DateTime? CN_TRACK_DATE
        {
            get { return this.m_CN_TRACK_DATE; }
            set
            {
                this.m_CN_TRACK_DATE = value;
                this.NotifyPropertyChanged("CN_TRACK_DATE");
            }
        }

        [DBColumn(Name = "CN_TRACK_BY", Storage = "m_CN_TRACK_BY", DbType = "126")]
        public string CN_TRACK_BY
        {
            get { return this.m_CN_TRACK_BY; }
            set
            {
                this.m_CN_TRACK_BY = value;
                this.NotifyPropertyChanged("CN_TRACK_BY");
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
