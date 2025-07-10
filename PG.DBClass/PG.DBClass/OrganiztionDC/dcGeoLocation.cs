using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using PG.Core.DBBase;

namespace PG.DBClass.OrganiztionDC
{
    [Serializable]
    [DBTable(Name = "tblGeoLocation")]
    public partial class dcGeoLocation : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_GeoLocationID = 0;
        private string m_GeoLocationCode = string.Empty;
        private string m_GeoLocationName = string.Empty;
        private int m_GeoLocationSLNo = 0;
        private int m_CompanyID = 0;
        private int m_GeoLocationTypeID = 0;
        private int m_GeoLocationIDParent = 0;

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


        [DBColumn(Name = "GeoLocationID", Storage = "m_GeoLocationID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int GeoLocationID
        {
            get { return this.m_GeoLocationID; }
            set
            {
                this.m_GeoLocationID = value;
                this.NotifyPropertyChanged("GeoLocationID");
            }
        }

        [DBColumn(Name = "GeoLocationCode", Storage = "m_GeoLocationCode", DbType = "NVarChar(50) NULL")]
        public string GeoLocationCode
        {
            get { return this.m_GeoLocationCode; }
            set
            {
                this.m_GeoLocationCode = value;
                this.NotifyPropertyChanged("GeoLocationCode");
            }
        }

        [DBColumn(Name = "GeoLocationName", Storage = "m_GeoLocationName", DbType = "NVarChar(50) NULL")]
        public string GeoLocationName
        {
            get { return this.m_GeoLocationName; }
            set
            {
                this.m_GeoLocationName = value;
                this.NotifyPropertyChanged("GeoLocationName");
            }
        }

        [DBColumn(Name = "GeoLocationSLNo", Storage = "m_GeoLocationSLNo", DbType = "Int NULL")]
        public int GeoLocationSLNo
        {
            get { return this.m_GeoLocationSLNo; }
            set
            {
                this.m_GeoLocationSLNo = value;
                this.NotifyPropertyChanged("GeoLocationSLNo");
            }
        }

        [DBColumn(Name = "CompanyID", Storage = "m_CompanyID", DbType = "Int NULL")]
        public int CompanyID
        {
            get { return this.m_CompanyID; }
            set
            {
                this.m_CompanyID = value;
                this.NotifyPropertyChanged("CompanyID");
            }
        }

        [DBColumn(Name = "GeoLocationTypeID", Storage = "m_GeoLocationTypeID", DbType = "Int NULL")]
        public int GeoLocationTypeID
        {
            get { return this.m_GeoLocationTypeID; }
            set
            {
                this.m_GeoLocationTypeID = value;
                this.NotifyPropertyChanged("GeoLocationTypeID");
            }
        }

        [DBColumn(Name = "GeoLocationIDParent", Storage = "m_GeoLocationIDParent", DbType = "Int NOT NULL")]
        public int GeoLocationIDParent
        {
            get { return this.m_GeoLocationIDParent; }
            set
            {
                this.m_GeoLocationIDParent = value;
                this.NotifyPropertyChanged("GeoLocationIDParent");
            }
        }

        #endregion //properties
    }
}
