using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using PG.Core.DBBase;

namespace PG.DBClass.AccountingDC
{
    [DBTable(Name = "tblAccSLNo")]
    public partial class dcAccSLNo : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_AccSLNoID = 0;
        private int m_CompanyID = 0;
        private string m_AccSLNoCode = string.Empty;
        private string m_AccSLNoName = string.Empty;
        private int m_AccSLNoLast = 0;
        private string m_AccSLNoPrefix = string.Empty;
        private string m_AccSLNoPrefixFormula = string.Empty;
        private string m_AccSLNoPrefixSep = string.Empty;
        private string m_AccSLNoSuffix = string.Empty;
        private string m_AccSLNoSuffixSep = string.Empty;
        private string m_AccSLNoSuffixFormula = string.Empty;
        private int m_AccSLNoLength = 0;
        private string m_AccSLNoFormula = string.Empty;
        private bool m_IsManual = false;
        private bool m_IsPad = false;
        private string m_PadChar = string.Empty;
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


        [DBColumn(Name = "AccSLNoID", Storage = "m_AccSLNoID", DbType = "Int NOT NULL", IsPrimaryKey = true)]
        public int AccSLNoID
        {
            get { return this.m_AccSLNoID; }
            set
            {
                this.m_AccSLNoID = value;
                this.NotifyPropertyChanged("AccSLNoID");
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

        [DBColumn(Name = "AccSLNoCode", Storage = "m_AccSLNoCode", DbType = "NVarChar(20) NULL")]
        public string AccSLNoCode
        {
            get { return this.m_AccSLNoCode; }
            set
            {
                this.m_AccSLNoCode = value;
                this.NotifyPropertyChanged("AccSLNoCode");
            }
        }

        [DBColumn(Name = "AccSLNoName", Storage = "m_AccSLNoName", DbType = "NVarChar(50) NULL")]
        public string AccSLNoName
        {
            get { return this.m_AccSLNoName; }
            set
            {
                this.m_AccSLNoName = value;
                this.NotifyPropertyChanged("AccSLNoName");
            }
        }

        [DBColumn(Name = "AccSLNoLast", Storage = "m_AccSLNoLast", DbType = "Int NULL")]
        public int AccSLNoLast
        {
            get { return this.m_AccSLNoLast; }
            set
            {
                this.m_AccSLNoLast = value;
                this.NotifyPropertyChanged("AccSLNoLast");
            }
        }

        [DBColumn(Name = "AccSLNoPrefix", Storage = "m_AccSLNoPrefix", DbType = "NVarChar(50) NULL")]
        public string AccSLNoPrefix
        {
            get { return this.m_AccSLNoPrefix; }
            set
            {
                this.m_AccSLNoPrefix = value;
                this.NotifyPropertyChanged("AccSLNoPrefix");
            }
        }

        [DBColumn(Name = "AccSLNoPrefixFormula", Storage = "m_AccSLNoPrefixFormula", DbType = "NVarChar(MAX) NULL")]
        public string AccSLNoPrefixFormula
        {
            get { return this.m_AccSLNoPrefixFormula; }
            set
            {
                this.m_AccSLNoPrefixFormula = value;
                this.NotifyPropertyChanged("AccSLNoPrefixFormula");
            }
        }

        [DBColumn(Name = "AccSLNoPrefixSep", Storage = "m_AccSLNoPrefixSep", DbType = "NVarChar(50) NULL")]
        public string AccSLNoPrefixSep
        {
            get { return this.m_AccSLNoPrefixSep; }
            set
            {
                this.m_AccSLNoPrefixSep = value;
                this.NotifyPropertyChanged("AccSLNoPrefixSep");
            }
        }

        [DBColumn(Name = "AccSLNoSuffix", Storage = "m_AccSLNoSuffix", DbType = "NVarChar(50) NULL")]
        public string AccSLNoSuffix
        {
            get { return this.m_AccSLNoSuffix; }
            set
            {
                this.m_AccSLNoSuffix = value;
                this.NotifyPropertyChanged("AccSLNoSuffix");
            }
        }

        [DBColumn(Name = "AccSLNoSuffixSep", Storage = "m_AccSLNoSuffixSep", DbType = "NVarChar(50) NULL")]
        public string AccSLNoSuffixSep
        {
            get { return this.m_AccSLNoSuffixSep; }
            set
            {
                this.m_AccSLNoSuffixSep = value;
                this.NotifyPropertyChanged("AccSLNoSuffixSep");
            }
        }

        [DBColumn(Name = "AccSLNoSuffixFormula", Storage = "m_AccSLNoSuffixFormula", DbType = "NVarChar(MAX) NULL")]
        public string AccSLNoSuffixFormula
        {
            get { return this.m_AccSLNoSuffixFormula; }
            set
            {
                this.m_AccSLNoSuffixFormula = value;
                this.NotifyPropertyChanged("AccSLNoSuffixFormula");
            }
        }

        [DBColumn(Name = "AccSLNoLength", Storage = "m_AccSLNoLength", DbType = "Int NULL")]
        public int AccSLNoLength
        {
            get { return this.m_AccSLNoLength; }
            set
            {
                this.m_AccSLNoLength = value;
                this.NotifyPropertyChanged("AccSLNoLength");
            }
        }

        [DBColumn(Name = "AccSLNoFormula", Storage = "m_AccSLNoFormula", DbType = "NVarChar(MAX) NULL")]
        public string AccSLNoFormula
        {
            get { return this.m_AccSLNoFormula; }
            set
            {
                this.m_AccSLNoFormula = value;
                this.NotifyPropertyChanged("AccSLNoFormula");
            }
        }

        [DBColumn(Name = "IsManual", Storage = "m_IsManual", DbType = "Bit NULL")]
        public bool IsManual
        {
            get { return this.m_IsManual; }
            set
            {
                this.m_IsManual = value;
                this.NotifyPropertyChanged("IsManual");
            }
        }

        [DBColumn(Name = "IsPad", Storage = "m_IsPad", DbType = "Bit NULL")]
        public bool IsPad
        {
            get { return this.m_IsPad; }
            set
            {
                this.m_IsPad = value;
                this.NotifyPropertyChanged("IsPad");
            }
        }

        [DBColumn(Name = "PadChar", Storage = "m_PadChar", DbType = "NVarChar(50) NULL")]
        public string PadChar
        {
            get { return this.m_PadChar; }
            set
            {
                this.m_PadChar = value;
                this.NotifyPropertyChanged("PadChar");
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

    public partial class dcAccSLNo
    {
        private string m_AccSLNo = string.Empty;

        public string AccSLNo
        {
            get
            {
                //string slNo = m_AccSLNoLast

                return this.m_AccSLNo; 
            }
            set { this.m_AccSLNo = value; }
        }
    }
}
