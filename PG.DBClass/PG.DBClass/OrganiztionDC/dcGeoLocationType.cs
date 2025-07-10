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
    [DBTable(Name = "tblGeoLocationType")]
    public partial class dcGeoLocationType : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_GeoLocationTypeID = 0;
        private string m_GeoLocationTypeCode = string.Empty;
        private string m_GeoLocationTypeName = string.Empty;
        private int m_GeoLocationTypeLevel = 0;
        private int m_GeoLocationTypeIDParent = 0;

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


        [DBColumn(Name = "GeoLocationTypeID", Storage = "m_GeoLocationTypeID", DbType = "Int NOT NULL", IsPrimaryKey = true)]
        public int GeoLocationTypeID
        {
            get { return this.m_GeoLocationTypeID; }
            set
            {
                this.m_GeoLocationTypeID = value;
                this.NotifyPropertyChanged("GeoLocationTypeID");
            }
        }

        [DBColumn(Name = "GeoLocationTypeCode", Storage = "m_GeoLocationTypeCode", DbType = "NVarChar(50) NULL")]
        public string GeoLocationTypeCode
        {
            get { return this.m_GeoLocationTypeCode; }
            set
            {
                this.m_GeoLocationTypeCode = value;
                this.NotifyPropertyChanged("GeoLocationTypeCode");
            }
        }

        [DBColumn(Name = "GeoLocationTypeName", Storage = "m_GeoLocationTypeName", DbType = "NVarChar(50) NULL")]
        public string GeoLocationTypeName
        {
            get { return this.m_GeoLocationTypeName; }
            set
            {
                this.m_GeoLocationTypeName = value;
                this.NotifyPropertyChanged("GeoLocationTypeName");
            }
        }

        [DBColumn(Name = "GeoLocationTypeLevel", Storage = "m_GeoLocationTypeLevel", DbType = "Int NULL")]
        public int GeoLocationTypeLevel
        {
            get { return this.m_GeoLocationTypeLevel; }
            set
            {
                this.m_GeoLocationTypeLevel = value;
                this.NotifyPropertyChanged("GeoLocationTypeLevel");
            }
        }

        [DBColumn(Name = "GeoLocationTypeIDParent", Storage = "m_GeoLocationTypeIDParent", DbType = "Int NULL")]
        public int GeoLocationTypeIDParent
        {
            get { return this.m_GeoLocationTypeIDParent; }
            set
            {
                this.m_GeoLocationTypeIDParent = value;
                this.NotifyPropertyChanged("GeoLocationTypeIDParent");
            }
        }

        #endregion //properties
    }
}
