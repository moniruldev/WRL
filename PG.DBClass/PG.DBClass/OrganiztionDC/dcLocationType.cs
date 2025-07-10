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
    [DBTable(Name = "tblLocationType")]
    public partial class dcLocationType : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_LocationTypeID = 0;
        private string m_LocationTypeCode = string.Empty;
        private string m_LocationTypeName = string.Empty;

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


        [DBColumn(Name = "LocationTypeID", Storage = "m_LocationTypeID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int LocationTypeID
        {
            get { return this.m_LocationTypeID; }
            set
            {
                this.m_LocationTypeID = value;
                this.NotifyPropertyChanged("LocationTypeID");
            }
        }

        [DBColumn(Name = "LocationTypeCode", Storage = "m_LocationTypeCode", DbType = "NVarChar(20) NULL")]
        public string LocationTypeCode
        {
            get { return this.m_LocationTypeCode; }
            set
            {
                this.m_LocationTypeCode = value;
                this.NotifyPropertyChanged("LocationTypeCode");
            }
        }

        [DBColumn(Name = "LocationTypeName", Storage = "m_LocationTypeName", DbType = "NVarChar(50) NULL")]
        public string LocationTypeName
        {
            get { return this.m_LocationTypeName; }
            set
            {
                this.m_LocationTypeName = value;
                this.NotifyPropertyChanged("LocationTypeName");
            }
        }

        #endregion //properties
    }
}
