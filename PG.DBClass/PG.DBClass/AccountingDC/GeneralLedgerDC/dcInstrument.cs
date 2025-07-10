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
    [DBTable(Name = "tblInstrument")]
    public partial class dcInstrument : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_InstrumentID = 0;
        private int m_CompanyID = 0;
        private int m_InstrumentModeID = 0;
        private int m_InstrumentTypeID = 0;
        private string m_InstrumentNo = string.Empty;
        private string m_IssueName = string.Empty;
        private int m_BankID = 0;
        private string m_BankName = string.Empty;
        private string m_BranchName = string.Empty;
        private DateTime? m_InstrumentDate = null;
        private decimal m_InstrumentAmt = 0;
        private decimal m_InstrumentAmtTran = 0;
        private decimal m_InstrumentAmtRemain = 0;
        private DateTime? m_InstrumentTranDate = null;
        private int m_InstrumentStatusID = 0;
        private DateTime? m_InstrumentStatusDate = null;
        private string m_Remarks = string.Empty;
        private int m_GLAccountID = 0;
        private int m_PrintCount = 0;
        private DateTime? m_LastPrintDate = null;

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


        [DBColumn(Name = "InstrumentID", Storage = "m_InstrumentID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity=true)]
        public int InstrumentID
        {
            get { return this.m_InstrumentID; }
            set
            {
                this.m_InstrumentID = value;
                this.NotifyPropertyChanged("InstrumentID");
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

        [DBColumn(Name = "InstrumentModeID", Storage = "m_InstrumentModeID", DbType = "Int NULL")]
        public int InstrumentModeID
        {
            get { return this.m_InstrumentModeID; }
            set
            {
                this.m_InstrumentModeID = value;
                this.NotifyPropertyChanged("InstrumentModeID");
            }
        }

        [DBColumn(Name = "InstrumentTypeID", Storage = "m_InstrumentTypeID", DbType = "Int NULL")]
        public int InstrumentTypeID
        {
            get { return this.m_InstrumentTypeID; }
            set
            {
                this.m_InstrumentTypeID = value;
                this.NotifyPropertyChanged("InstrumentTypeID");
            }
        }

        [DBColumn(Name = "InstrumentNo", Storage = "m_InstrumentNo", DbType = "NVarChar(100) NULL")]
        public string InstrumentNo
        {
            get { return this.m_InstrumentNo; }
            set
            {
                this.m_InstrumentNo = value;
                this.NotifyPropertyChanged("InstrumentNo");
            }
        }

        [DBColumn(Name = "IssueName", Storage = "m_IssueName", DbType = "NVarChar(100) NULL")]
        public string IssueName
        {
            get { return this.m_IssueName; }
            set
            {
                this.m_IssueName = value;
                this.NotifyPropertyChanged("IssueName");
            }
        }

        [DBColumn(Name = "BankID", Storage = "m_BankID", DbType = "Int NULL")]
        public int BankID
        {
            get { return this.m_BankID; }
            set
            {
                this.m_BankID = value;
                this.NotifyPropertyChanged("BankID");
            }
        }

        [DBColumn(Name = "BankName", Storage = "m_BankName", DbType = "NVarChar(200) NULL")]
        public string BankName
        {
            get { return this.m_BankName; }
            set
            {
                this.m_BankName = value;
                this.NotifyPropertyChanged("BankName");
            }
        }

        [DBColumn(Name = "BranchName", Storage = "m_BranchName", DbType = "NVarChar(200) NULL")]
        public string BranchName
        {
            get { return this.m_BranchName; }
            set
            {
                this.m_BranchName = value;
                this.NotifyPropertyChanged("BranchName");
            }
        }

        [DBColumn(Name = "InstrumentDate", Storage = "m_InstrumentDate", DbType = "DateTime NULL")]
        public DateTime? InstrumentDate
        {
            get { return this.m_InstrumentDate; }
            set
            {
                this.m_InstrumentDate = value;
                this.NotifyPropertyChanged("InstrumentDate");
            }
        }

        [DBColumn(Name = "InstrumentAmt", Storage = "m_InstrumentAmt", DbType = "Money NULL")]
        public decimal InstrumentAmt
        {
            get { return this.m_InstrumentAmt; }
            set
            {
                this.m_InstrumentAmt = value;
                this.NotifyPropertyChanged("InstrumentAmt");
            }
        }

        [DBColumn(Name = "InstrumentAmtTran", Storage = "m_InstrumentAmtTran", DbType = "Money NULL")]
        public decimal InstrumentAmtTran
        {
            get { return this.m_InstrumentAmtTran; }
            set
            {
                this.m_InstrumentAmtTran = value;
                this.NotifyPropertyChanged("InstrumentAmtTran");
            }
        }

        [DBColumn(Name = "InstrumentAmtRemain", Storage = "m_InstrumentAmtRemain", DbType = "Money NULL")]
        public decimal InstrumentAmtRemain
        {
            get { return this.m_InstrumentAmtRemain; }
            set
            {
                this.m_InstrumentAmtRemain = value;
                this.NotifyPropertyChanged("InstrumentAmtRemain");
            }
        }

        [DBColumn(Name = "InstrumentTranDate", Storage = "m_InstrumentTranDate", DbType = "DateTime NULL")]
        public DateTime? InstrumentTranDate
        {
            get { return this.m_InstrumentTranDate; }
            set
            {
                this.m_InstrumentTranDate = value;
                this.NotifyPropertyChanged("InstrumentTranDate");
            }
        }

        [DBColumn(Name = "InstrumentStatusID", Storage = "m_InstrumentStatusID", DbType = "Int NULL")]
        public int InstrumentStatusID
        {
            get { return this.m_InstrumentStatusID; }
            set
            {
                this.m_InstrumentStatusID = value;
                this.NotifyPropertyChanged("InstrumentStatusID");
            }
        }

        [DBColumn(Name = "InstrumentStatusDate", Storage = "m_InstrumentStatusDate", DbType = "DateTime NULL")]
        public DateTime? InstrumentStatusDate
        {
            get { return this.m_InstrumentStatusDate; }
            set
            {
                this.m_InstrumentStatusDate = value;
                this.NotifyPropertyChanged("InstrumentStatusDate");
            }
        }

        [DBColumn(Name = "Remarks", Storage = "m_Remarks", DbType = "NVarChar(200) NULL")]
        public string Remarks
        {
            get { return this.m_Remarks; }
            set
            {
                this.m_Remarks = value;
                this.NotifyPropertyChanged("Remarks");
            }
        }

        [DBColumn(Name = "GLAccountID", Storage = "m_GLAccountID", DbType = "Int NULL")]
        public int GLAccountID
        {
            get { return this.m_GLAccountID; }
            set
            {
                this.m_GLAccountID = value;
                this.NotifyPropertyChanged("GLAccountID");
            }
        }

        [DBColumn(Name = "PrintCount", Storage = "m_PrintCount", DbType = "Int NULL")]
        public int PrintCount
        {
            get { return this.m_PrintCount; }
            set
            {
                this.m_PrintCount = value;
                this.NotifyPropertyChanged("PrintCount");
            }
        }

        [DBColumn(Name = "LastPrintDate", Storage = "m_LastPrintDate", DbType = "DateTime NULL")]
        public DateTime? LastPrintDate
        {
            get { return this.m_LastPrintDate; }
            set
            {
                this.m_LastPrintDate = value;
                this.NotifyPropertyChanged("LastPrintDate");
            }
        }

        #endregion //properties
    }

    public partial class dcInstrument
    {
        private string m_InstrumentModeName = string.Empty;
        public string InstrumentModeName
        {
            get { return m_InstrumentModeName; }
            set { m_InstrumentModeName = value; }
        }

        private string m_InstrumentTypeName = string.Empty;
        public string InstrumentTypeName
        {
            get { return m_InstrumentTypeName; }
            set { m_InstrumentTypeName = value; }
        }

        private string m_InstrumentStatusName = string.Empty;
        public string InstrumentStatusName
        {
            get { return m_InstrumentStatusName; }
            set { m_InstrumentStatusName = value; }
        }

        public string BankBranchName
        {
            get {
                string bankBranchName = m_BankName;
                if (m_BranchName != string.Empty)
                {
                    bankBranchName += "," + m_BranchName;
                }
                return bankBranchName; 
            }
        }
    }
}
