using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using PG.Core.DBBase;

namespace PG.DBClass.SystemDC
{
    [Serializable]
    [DBTable(Name = "tblSysOption")]
    public partial class dcSysOption : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_SysOptionID = 0;
        private int m_SysOptionValue = 0;
        private string m_SysOptionCode = string.Empty;
        private string m_SysOptionName = string.Empty;
        private string m_SysOptionNameDisplay = string.Empty;
        private string m_SysOptionNamePrint = string.Empty;
        private string m_SysOptionNameFull = string.Empty;
        private int m_SysOptionIDParent = 0;
        private int m_SysOptionSLNo = 0;
        private int m_SysOptionPriority = 0;
        private bool m_IsEnabled = false;
        private bool m_IsVisible = false;

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


        [DBColumn(Name = "SysOptionID", Storage = "m_SysOptionID", DbType = "Int NOT NULL", IsPrimaryKey = true)]
        public int SysOptionID
        {
            get { return this.m_SysOptionID; }
            set
            {
                this.m_SysOptionID = value;
                this.NotifyPropertyChanged("SysOptionID");
            }
        }


        [DBColumn(Name = "SysOptionValue", Storage = "m_SysOptionValue", DbType = "Int NULL")]
        public int SysOptionValue
        {
            get { return this.m_SysOptionValue; }
            set
            {
                this.m_SysOptionValue = value;
                this.NotifyPropertyChanged("SysOptionValue");
            }
        }

        [DBColumn(Name = "SysOptionCode", Storage = "m_SysOptionCode", DbType = "NVarChar(50) NULL")]
        public string SysOptionCode
        {
            get { return this.m_SysOptionCode; }
            set
            {
                this.m_SysOptionCode = value;
                this.NotifyPropertyChanged("SysOptionCode");
            }
        }

        [DBColumn(Name = "SysOptionName", Storage = "m_SysOptionName", DbType = "NVarChar(100) NULL")]
        public string SysOptionName
        {
            get { return this.m_SysOptionName; }
            set
            {
                this.m_SysOptionName = value;
                this.NotifyPropertyChanged("SysOptionName");
            }
        }

        [DBColumn(Name = "SysOptionNameDisplay", Storage = "m_SysOptionNameDisplay", DbType = "NVarChar(100) NULL")]
        public string SysOptionNameDisplay
        {
            get { return this.m_SysOptionNameDisplay; }
            set
            {
                this.m_SysOptionNameDisplay = value;
                this.NotifyPropertyChanged("SysOptionNameDisplay");
            }
        }

        [DBColumn(Name = "SysOptionNamePrint", Storage = "m_SysOptionNamePrint", DbType = "NVarChar(100) NULL")]
        public string SysOptionNamePrint
        {
            get { return this.m_SysOptionNamePrint; }
            set
            {
                this.m_SysOptionNamePrint = value;
                this.NotifyPropertyChanged("SysOptionNamePrint");
            }
        }

        [DBColumn(Name = "SysOptionNameFull", Storage = "m_SysOptionNameFull", DbType = "NVarChar(2000) NULL")]
        public string SysOptionNameFull
        {
            get { return this.m_SysOptionNameFull; }
            set
            {
                this.m_SysOptionNameFull = value;
                this.NotifyPropertyChanged("SysOptionNameFull");
            }
        }

        [DBColumn(Name = "SysOptionIDParent", Storage = "m_SysOptionIDParent", DbType = "Int NULL")]
        public int SysOptionIDParent
        {
            get { return this.m_SysOptionIDParent; }
            set
            {
                this.m_SysOptionIDParent = value;
                this.NotifyPropertyChanged("SysOptionIDParent");
            }
        }

        [DBColumn(Name = "SysOptionSLNo", Storage = "m_SysOptionSLNo", DbType = "Int NULL")]
        public int SysOptionSLNo
        {
            get { return this.m_SysOptionSLNo; }
            set
            {
                this.m_SysOptionSLNo = value;
                this.NotifyPropertyChanged("SysOptionSLNo");
            }
        }

        [DBColumn(Name = "SysOptionPriority", Storage = "m_SysOptionPriority", DbType = "Int NULL")]
        public int SysOptionPriority
        {
            get { return this.m_SysOptionPriority; }
            set
            {
                this.m_SysOptionPriority = value;
                this.NotifyPropertyChanged("SysOptionPriority");
            }
        }

        [DBColumn(Name = "IsEnabled", Storage = "m_IsEnabled", DbType = "Bit NULL")]
        public bool IsEnabled
        {
            get { return this.m_IsEnabled; }
            set
            {
                this.m_IsEnabled = value;
                this.NotifyPropertyChanged("IsEnabled");
            }
        }

        [DBColumn(Name = "IsVisible", Storage = "m_IsVisible", DbType = "Bit NULL")]
        public bool IsVisible
        {
            get { return this.m_IsVisible; }
            set
            {
                this.m_IsVisible = value;
                this.NotifyPropertyChanged("IsVisible");
            }
        }

        #endregion //properties
    }
}
