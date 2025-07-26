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
        private int m_FROM_HUB_ID = 0;
        private string m_GPS_LOCATION = string.Empty;
        private int m_TRANS_MEDIA_ID = 0;
        private string m_TRANS_CONTACT_NO = string.Empty;
        private string m_TRANS_CONTACT_PERSON = string.Empty;
        private string m_REMARKS = string.Empty;
        private string m_TRANS_TYPE = string.Empty;
        private int m_TO_HUB_ID = 0;
        private int m_HUB_ID = 0;
        private int m_REF_TRANS_ID = 0;
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

        [DBColumn(Name = "FROM_HUB_ID", Storage = "m_FROM_HUB_ID", DbType = "107")]
        public int FROM_HUB_ID
        {
            get { return this.m_FROM_HUB_ID; }
            set
            {
                this.m_FROM_HUB_ID = value;
                this.NotifyPropertyChanged("FROM_HUB_ID");
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

        [DBColumn(Name = "TRANS_MEDIA_ID", Storage = "m_TRANS_MEDIA_ID", DbType = "107")]
        public int TRANS_MEDIA_ID
        {
            get { return this.m_TRANS_MEDIA_ID; }
            set
            {
                this.m_TRANS_MEDIA_ID = value;
                this.NotifyPropertyChanged("TRANS_MEDIA_ID");
            }
        }

        [DBColumn(Name = "TRANS_CONTACT_NO", Storage = "m_TRANS_CONTACT_NO", DbType = "126")]
        public string TRANS_CONTACT_NO
        {
            get { return this.m_TRANS_CONTACT_NO; }
            set
            {
                this.m_TRANS_CONTACT_NO = value;
                this.NotifyPropertyChanged("TRANS_CONTACT_NO");
            }
        }

        [DBColumn(Name = "TRANS_CONTACT_PERSON", Storage = "m_TRANS_CONTACT_PERSON", DbType = "126")]
        public string TRANS_CONTACT_PERSON
        {
            get { return this.m_TRANS_CONTACT_PERSON; }
            set
            {
                this.m_TRANS_CONTACT_PERSON = value;
                this.NotifyPropertyChanged("TRANS_CONTACT_PERSON");
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


        [DBColumn(Name = "TRANS_TYPE", Storage = "m_TRANS_TYPE", DbType = "126")]
        public string TRANS_TYPE
        {
            get { return this.m_TRANS_TYPE; }
            set
            {
                this.m_TRANS_TYPE = value;
                this.NotifyPropertyChanged("TRANS_TYPE");
            }
        }

        [DBColumn(Name = "TO_HUB_ID", Storage = "m_TO_HUB_ID", DbType = "107")]
        public int TO_HUB_ID
        {
            get { return this.m_TO_HUB_ID; }
            set
            {
                this.m_TO_HUB_ID = value;
                this.NotifyPropertyChanged("TO_HUB_ID");
            }
        }
        [DBColumn(Name = "HUB_ID", Storage = "m_HUB_ID", DbType = "107")]
        public int HUB_ID
        {
            get { return this.m_HUB_ID; }
            set
            {
                this.m_HUB_ID = value;
                this.NotifyPropertyChanged("HUB_ID");
            }
        }

        [DBColumn(Name = "REF_TRANS_ID", Storage = "m_REF_TRANS_ID", DbType = "107")]
        public int REF_TRANS_ID
        {
            get { return this.m_REF_TRANS_ID; }
            set
            {
                this.m_REF_TRANS_ID = value;
                this.NotifyPropertyChanged("REF_TRANS_ID");
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
