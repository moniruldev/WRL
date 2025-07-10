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
    [DBTable(Name = "tblCompanyType")]
    public partial class dcCompanyType : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_CompanyTypeID = 0;
        private string m_CompanyTypeCode = string.Empty;
        private string m_CompanyTypeName = string.Empty;
        private bool m_IsPostable = false;

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


        [DBColumn(Name = "CompanyTypeID", Storage = "m_CompanyTypeID", DbType = "Int NOT NULL", IsPrimaryKey = true)]
        public int CompanyTypeID
        {
            get { return this.m_CompanyTypeID; }
            set
            {
                this.m_CompanyTypeID = value;
                this.NotifyPropertyChanged("CompanyTypeID");
            }
        }

        [DBColumn(Name = "CompanyTypeCode", Storage = "m_CompanyTypeCode", DbType = "NVarChar(20) NULL")]
        public string CompanyTypeCode
        {
            get { return this.m_CompanyTypeCode; }
            set
            {
                this.m_CompanyTypeCode = value;
                this.NotifyPropertyChanged("CompanyTypeCode");
            }
        }

        [DBColumn(Name = "CompanyTypeName", Storage = "m_CompanyTypeName", DbType = "NVarChar(50) NULL")]
        public string CompanyTypeName
        {
            get { return this.m_CompanyTypeName; }
            set
            {
                this.m_CompanyTypeName = value;
                this.NotifyPropertyChanged("CompanyTypeName");
            }
        }

        [DBColumn(Name = "IsPostable", Storage = "m_IsPostable", DbType = "Bit NULL")]
        public bool IsPostable
        {
            get { return this.m_IsPostable; }
            set
            {
                this.m_IsPostable = value;
                this.NotifyPropertyChanged("IsPostable");
            }
        }

        #endregion //properties
    }
}
