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
    [DBTable(Name = "tblAccYear")]
    public partial class dcAccYear : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_AccYearID = 0;
        private int m_CompanyID = 0;
        private string m_AccYearName = string.Empty;
        private int m_AccYearNo = 0;
        private DateTime? m_YearStartDate = null;
        private DateTime? m_YearEndDate = null;
        private DateTime? m_OpBalanceDate = null;
        private bool m_IsClosed = false;
        private DateTime? m_ClosingDate = null;
        private DateTime? m_ClosingDateSys = null;
        private string m_Remarks = string.Empty;

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


        [DBColumn(Name = "AccYearID", Storage = "m_AccYearID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, SequenceName = "TBLACCYEAR_SEQ")]
        public int AccYearID
        {
            get { return this.m_AccYearID; }
            set
            {
                this.m_AccYearID = value;
                this.NotifyPropertyChanged("AccYearID");
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

        [DBColumn(Name = "AccYearName", Storage = "m_AccYearName", DbType = "NVarChar(100) NULL")]
        public string AccYearName
        {
            get { return this.m_AccYearName; }
            set
            {
                this.m_AccYearName = value;
                this.NotifyPropertyChanged("AccYearName");
            }
        }

        [DBColumn(Name = "AccYearNo", Storage = "m_AccYearNo", DbType = "Int NULL")]
        public int AccYearNo
        {
            get { return this.m_AccYearNo; }
            set
            {
                this.m_AccYearNo = value;
                this.NotifyPropertyChanged("AccYearNo");
            }
        }

        [DBColumn(Name = "YearStartDate", Storage = "m_YearStartDate", DbType = "DateTime NULL")]
        public DateTime? YearStartDate
        {
            get { return this.m_YearStartDate; }
            set
            {
                this.m_YearStartDate = value;
                this.NotifyPropertyChanged("YearStartDate");
            }
        }

        [DBColumn(Name = "YearEndDate", Storage = "m_YearEndDate", DbType = "DateTime NULL")]
        public DateTime? YearEndDate
        {
            get { return this.m_YearEndDate; }
            set
            {
                this.m_YearEndDate = value;
                this.NotifyPropertyChanged("YearEndDate");
            }
        }

        [DBColumn(Name = "OpBalanceDate", Storage = "m_OpBalanceDate", DbType = "DateTime NULL")]
        public DateTime? OpBalanceDate
        {
            get { return this.m_OpBalanceDate; }
            set
            {
                this.m_OpBalanceDate = value;
                this.NotifyPropertyChanged("OpBalanceDate");
            }
        }

        [DBColumn(Name = "IsClosed", Storage = "m_IsClosed", DbType = "Bit NULL")]
        public bool IsClosed
        {
            get { return this.m_IsClosed; }
            set
            {
                this.m_IsClosed = value;
                this.NotifyPropertyChanged("IsClosed");
            }
        }

        [DBColumn(Name = "ClosingDate", Storage = "m_ClosingDate", DbType = "DateTime NULL")]
        public DateTime? ClosingDate
        {
            get { return this.m_ClosingDate; }
            set
            {
                this.m_ClosingDate = value;
                this.NotifyPropertyChanged("ClosingDate");
            }
        }

        [DBColumn(Name = "ClosingDateSys", Storage = "m_ClosingDateSys", DbType = "DateTime NULL")]
        public DateTime? ClosingDateSys
        {
            get { return this.m_ClosingDateSys; }
            set
            {
                this.m_ClosingDateSys = value;
                this.NotifyPropertyChanged("ClosingDateSys");
            }
        }

        [DBColumn(Name = "Remarks", Storage = "m_Remarks", DbType = "NVarChar(MAX) NULL")]
        public string Remarks
        {
            get { return this.m_Remarks; }
            set
            {
                this.m_Remarks = value;
                this.NotifyPropertyChanged("Remarks");
            }
        }

        #endregion //properties
    }
}
