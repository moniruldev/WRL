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
    [DBTable(Name = "tblLocation")]
    public partial class dcLocation : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_LocationID = 0;
        private string m_LocationCode = string.Empty;
        private string m_LocationName = string.Empty;
        private int m_CompanyID = 0;
        private string m_LocationAddress = string.Empty;
        private int m_LocationIDParent = 0;
        private int m_LocationTypeID = 0;
        private int m_LocationIDMaster = 0;
        private string m_LocationCodeOld = string.Empty;

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


        [DBColumn(Name = "LocationID", Storage = "m_LocationID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int LocationID
        {
            get { return this.m_LocationID; }
            set
            {
                this.m_LocationID = value;
                this.NotifyPropertyChanged("LocationID");
            }
        }

        [DBColumn(Name = "LocationCode", Storage = "m_LocationCode", DbType = "NVarChar(20) NULL")]
        public string LocationCode
        {
            get { return this.m_LocationCode; }
            set
            {
                this.m_LocationCode = value;
                this.NotifyPropertyChanged("LocationCode");
            }
        }

        [DBColumn(Name = "LocationName", Storage = "m_LocationName", DbType = "NVarChar(50) NULL")]
        public string LocationName
        {
            get { return this.m_LocationName; }
            set
            {
                this.m_LocationName = value;
                this.NotifyPropertyChanged("LocationName");
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

        [DBColumn(Name = "LocationAddress", Storage = "m_LocationAddress", DbType = "NVarChar(100) NULL")]
        public string LocationAddress
        {
            get { return this.m_LocationAddress; }
            set
            {
                this.m_LocationAddress = value;
                this.NotifyPropertyChanged("LocationAddress");
            }
        }

        [DBColumn(Name = "LocationIDParent", Storage = "m_LocationIDParent", DbType = "Int NULL")]
        public int LocationIDParent
        {
            get { return this.m_LocationIDParent; }
            set
            {
                this.m_LocationIDParent = value;
                this.NotifyPropertyChanged("LocationIDParent");
            }
        }

        [DBColumn(Name = "LocationTypeID", Storage = "m_LocationTypeID", DbType = "Int NULL")]
        public int LocationTypeID
        {
            get { return this.m_LocationTypeID; }
            set
            {
                this.m_LocationTypeID = value;
                this.NotifyPropertyChanged("LocationTypeID");
            }
        }

        [DBColumn(Name = "LocationIDMaster", Storage = "m_LocationIDMaster", DbType = "Int NULL")]
        public int LocationIDMaster
        {
            get { return this.m_LocationIDMaster; }
            set
            {
                this.m_LocationIDMaster = value;
                this.NotifyPropertyChanged("LocationIDMaster");
            }
        }

        [DBColumn(Name = "LocationCodeOld", Storage = "m_LocationCodeOld", DbType = "NVarChar(50) NOT NULL")]
        public string LocationCodeOld
        {
            get { return this.m_LocationCodeOld; }
            set
            {
                this.m_LocationCodeOld = value;
                this.NotifyPropertyChanged("LocationCodeOld");
            }
        }

        #endregion //properties
    }

    public partial class dcLocation
    {
        public string LocationCodeName
        { 
            get { 
                return this.m_LocationCode + " - " + m_LocationName;
            }
        }

        private string m_LocationTypeCode = string.Empty;
        public string LocationTypeCode
        {
            get { return m_LocationTypeCode; }
            set { this.m_LocationTypeCode = value; }
        }

        private string m_LocationTypeName = string.Empty;
        public string LocationTypeName
        {
            get { return m_LocationTypeName; }
            set { this.m_LocationTypeName = value; }
        }

        public string LocationTypeCodeName
        {
            get
            {
                return this.m_LocationTypeCode + " - " + m_LocationTypeName;
            }
        }
    }

}
