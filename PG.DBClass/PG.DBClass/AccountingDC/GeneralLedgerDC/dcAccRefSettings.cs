using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using PG.Core.DBBase;

namespace PG.DBClass.AccountingDC.GeneralLedgerDC
{
    [DBTable(Name = "tblAccRefSettings")]
    public partial class dcAccRefSettings : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_AccRefSettingsID = 0;
        private int m_CompanyID = 0;
        private int m_AccRefTypeID = 0;
        private string m_AccRefSettingsName = string.Empty;
        private int m_AccRefSettingsSLNo = 0;
        private bool m_AllowMultipleCategory = false;
        private bool m_TotalSumCheckByCtategory = false;
        private bool m_AllowNonBindCategory = false;

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


        [DBColumn(Name = "AccRefSettingsID", Storage = "m_AccRefSettingsID", DbType = "Int NOT NULL", IsPrimaryKey = true)]
        public int AccRefSettingsID
        {
            get { return this.m_AccRefSettingsID; }
            set
            {
                this.m_AccRefSettingsID = value;
                this.NotifyPropertyChanged("AccRefSettingsID");
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

        [DBColumn(Name = "AccRefTypeID", Storage = "m_AccRefTypeID", DbType = "Int NULL")]
        public int AccRefTypeID
        {
            get { return this.m_AccRefTypeID; }
            set
            {
                this.m_AccRefTypeID = value;
                this.NotifyPropertyChanged("AccRefTypeID");
            }
        }

        [DBColumn(Name = "AccRefSettingsName", Storage = "m_AccRefSettingsName", DbType = "NVarChar(50) NULL")]
        public string AccRefSettingsName
        {
            get { return this.m_AccRefSettingsName; }
            set
            {
                this.m_AccRefSettingsName = value;
                this.NotifyPropertyChanged("AccRefSettingsName");
            }
        }

        [DBColumn(Name = "AccRefSettingsSLNo", Storage = "m_AccRefSettingsSLNo", DbType = "Int NULL")]
        public int AccRefSettingsSLNo
        {
            get { return this.m_AccRefSettingsSLNo; }
            set
            {
                this.m_AccRefSettingsSLNo = value;
                this.NotifyPropertyChanged("AccRefSettingsSLNo");
            }
        }

        [DBColumn(Name = "AllowMultipleCategory", Storage = "m_AllowMultipleCategory", DbType = "Bit NULL")]
        public bool AllowMultipleCategory
        {
            get { return this.m_AllowMultipleCategory; }
            set
            {
                this.m_AllowMultipleCategory = value;
                this.NotifyPropertyChanged("AllowMultipleCategory");
            }
        }

        [DBColumn(Name = "TotalSumCheckByCtategory", Storage = "m_TotalSumCheckByCtategory", DbType = "Bit NULL")]
        public bool TotalSumCheckByCtategory
        {
            get { return this.m_TotalSumCheckByCtategory; }
            set
            {
                this.m_TotalSumCheckByCtategory = value;
                this.NotifyPropertyChanged("TotalSumCheckByCtategory");
            }
        }

        [DBColumn(Name = "AllowNonBindCategory", Storage = "m_AllowNonBindCategory", DbType = "Bit NULL")]
        public bool AllowNonBindCategory
        {
            get { return this.m_AllowNonBindCategory; }
            set
            {
                this.m_AllowNonBindCategory = value;
                this.NotifyPropertyChanged("AllowNonBindCategory");
            }
        }


        #endregion //properties
    }
}
