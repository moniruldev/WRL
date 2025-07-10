using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using PG.Core.DBBase;


namespace PG.DBClass.AccountingDC.TaxDC
{
    [DBTable(Name = "tblTaxYear")]
    public partial class dcTaxYear : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_TaxYearID = 0;
        private string m_TaxYearName = string.Empty;
        private DateTime? m_YearStartDate = null;
        private DateTime? m_YearEndDate = null;
        private int m_IncomeTaxStartMonth = 0;
        private int m_IncomeTaxEndMonth = 0;
        private int m_IncomeTaxInfoID = 0;

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


        [DBColumn(Name = "TaxYearID", Storage = "m_TaxYearID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true)]
        public int TaxYearID
        {
            get { return this.m_TaxYearID; }
            set
            {
                this.m_TaxYearID = value;
                this.NotifyPropertyChanged("TaxYearID");
            }
        }

        [DBColumn(Name = "TaxYearName", Storage = "m_TaxYearName", DbType = "NChar(10) NULL")]
        public string TaxYearName
        {
            get { return this.m_TaxYearName; }
            set
            {
                this.m_TaxYearName = value;
                this.NotifyPropertyChanged("TaxYearName");
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

        [DBColumn(Name = "IncomeTaxStartMonth", Storage = "m_IncomeTaxStartMonth", DbType = "Int NULL")]
        public int IncomeTaxStartMonth
        {
            get { return this.m_IncomeTaxStartMonth; }
            set
            {
                this.m_IncomeTaxStartMonth = value;
                this.NotifyPropertyChanged("IncomeTaxStartMonth");
            }
        }

        [DBColumn(Name = "IncomeTaxEndMonth", Storage = "m_IncomeTaxEndMonth", DbType = "Int NULL")]
        public int IncomeTaxEndMonth
        {
            get { return this.m_IncomeTaxEndMonth; }
            set
            {
                this.m_IncomeTaxEndMonth = value;
                this.NotifyPropertyChanged("IncomeTaxEndMonth");
            }
        }

        [DBColumn(Name = "IncomeTaxInfoID", Storage = "m_IncomeTaxInfoID", DbType = "Int NULL")]
        public int IncomeTaxInfoID
        {
            get { return this.m_IncomeTaxInfoID; }
            set
            {
                this.m_IncomeTaxInfoID = value;
                this.NotifyPropertyChanged("IncomeTaxInfoID");
            }
        }

        #endregion //properties
    }
}
