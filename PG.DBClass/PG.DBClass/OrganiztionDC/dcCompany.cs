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
    [DBTable(Name = "tblCompany")]
    [Serializable]
    public partial class dcCompany : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_CompanyID = 0;
        private string m_CompanyName = string.Empty;
        private string m_CompanyAddress = string.Empty;
        private int m_CompanyIDParent = 0;
        private int m_CompanyTypeID = 0;
        private int m_LocationID = 0;

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


        [DBColumn(Name = "CompanyID", Storage = "m_CompanyID", DbType = "Int NOT NULL", IsPrimaryKey = true)]
        public int CompanyID
        {
            get { return this.m_CompanyID; }
            set
            {
                this.m_CompanyID = value;
                this.NotifyPropertyChanged("CompanyID");
            }
        }

        [DBColumn(Name = "CompanyName", Storage = "m_CompanyName", DbType = "NVarChar(200) NULL")]
        public string CompanyName
        {
            get { return this.m_CompanyName; }
            set
            {
                this.m_CompanyName = value;
                this.NotifyPropertyChanged("CompanyName");
            }
        }

        [DBColumn(Name = "CompanyAddress", Storage = "m_CompanyAddress", DbType = "NVarChar(250) NULL")]
        public string CompanyAddress
        {
            get { return this.m_CompanyAddress; }
            set
            {
                this.m_CompanyAddress = value;
                this.NotifyPropertyChanged("CompanyAddress");
            }
        }

        [DBColumn(Name = "CompanyIDParent", Storage = "m_CompanyIDParent", DbType = "Int NULL")]
        public int CompanyIDParent
        {
            get { return this.m_CompanyIDParent; }
            set
            {
                this.m_CompanyIDParent = value;
                this.NotifyPropertyChanged("CompanyIDParent");
            }
        }

        [DBColumn(Name = "CompanyTypeID", Storage = "m_CompanyTypeID", DbType = "Int NULL")]
        public int CompanyTypeID
        {
            get { return this.m_CompanyTypeID; }
            set
            {
                this.m_CompanyTypeID = value;
                this.NotifyPropertyChanged("CompanyTypeID");
            }
        }

        [DBColumn(Name = "LocationID", Storage = "m_LocationID", DbType = "Int NULL")]
        public int LocationID
        {
            get { return this.m_LocationID; }
            set
            {
                this.m_LocationID = value;
                this.NotifyPropertyChanged("LocationID");
            }
        }

        #endregion //properties
    }

     public partial class dcCompany
     {
         public decimal LEAD_LIMIT { get; set; }
         public decimal OP_LEAD_STK { get; set; }
     }
}
