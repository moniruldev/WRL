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
    [DBTable(Name = "tblGLAccountHistory")]
    public partial class dcGLAccountHistory : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_GLAccHistID = 0;
        private int m_CompanyID = 0;
        private int m_AccYearID = 0;
        private int m_GLAccountID = 0;
        private decimal m_DebitAmtOpen = 0;
        private decimal m_CreditAmtOpen = 0;
        private decimal m_OpenAmt = 0;
        private decimal m_DebitAmtOpenSys = 0;
        private decimal m_CreditAmtOpenSys = 0;
        private decimal m_OpenAmtSys = 0;
        private decimal m_DebitAmt = 0;
        private decimal m_CreditAmt = 0;
        private decimal m_TranAmt = 0;
        private decimal m_DebitAmtClose = 0;
        private decimal m_CreditAmtClose = 0;
        private decimal m_CloseAmt = 0;
        private string m_Remarks = string.Empty;
        private DateTime? m_EntryDate = null;
        private DateTime? m_EditDate = null;

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


        [DBColumn(Name = "GLAccHistID", Storage = "m_GLAccHistID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity=true)]
        public int GLAccHistID
        {
            get { return this.m_GLAccHistID; }
            set
            {
                this.m_GLAccHistID = value;
                this.NotifyPropertyChanged("GLAccHistID");
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

        [DBColumn(Name = "AccYearID", Storage = "m_AccYearID", DbType = "Int NULL")]
        public int AccYearID
        {
            get { return this.m_AccYearID; }
            set
            {
                this.m_AccYearID = value;
                this.NotifyPropertyChanged("AccYearID");
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

        [DBColumn(Name = "DebitAmtOpen", Storage = "m_DebitAmtOpen", DbType = "Money NULL")]
        public decimal DebitAmtOpen
        {
            get { return this.m_DebitAmtOpen; }
            set
            {
                this.m_DebitAmtOpen = value;
                this.NotifyPropertyChanged("DebitAmtOpen");
            }
        }

        [DBColumn(Name = "CreditAmtOpen", Storage = "m_CreditAmtOpen", DbType = "Money NULL")]
        public decimal CreditAmtOpen
        {
            get { return this.m_CreditAmtOpen; }
            set
            {
                this.m_CreditAmtOpen = value;
                this.NotifyPropertyChanged("CreditAmtOpen");
            }
        }

        [DBColumn(Name = "OpenAmt", Storage = "m_OpenAmt", DbType = "Money NULL")]
        public decimal OpenAmt
        {
            get { return this.m_OpenAmt; }
            set
            {
                this.m_OpenAmt = value;
                this.NotifyPropertyChanged("OpenAmt");
            }
        }

        [DBColumn(Name = "DebitAmtOpenSys", Storage = "m_DebitAmtOpenSys", DbType = "Money NULL")]
        public decimal DebitAmtOpenSys
        {
            get { return this.m_DebitAmtOpenSys; }
            set
            {
                this.m_DebitAmtOpenSys = value;
                this.NotifyPropertyChanged("DebitAmtOpenSys");
            }
        }

        [DBColumn(Name = "CreditAmtOpenSys", Storage = "m_CreditAmtOpenSys", DbType = "Money NULL")]
        public decimal CreditAmtOpenSys
        {
            get { return this.m_CreditAmtOpenSys; }
            set
            {
                this.m_CreditAmtOpenSys = value;
                this.NotifyPropertyChanged("CreditAmtOpenSys");
            }
        }

        [DBColumn(Name = "OpenAmtSys", Storage = "m_OpenAmtSys", DbType = "Money NULL")]
        public decimal OpenAmtSys
        {
            get { return this.m_OpenAmtSys; }
            set
            {
                this.m_OpenAmtSys = value;
                this.NotifyPropertyChanged("OpenAmtSys");
            }
        }

        [DBColumn(Name = "DebitAmt", Storage = "m_DebitAmt", DbType = "Money NULL")]
        public decimal DebitAmt
        {
            get { return this.m_DebitAmt; }
            set
            {
                this.m_DebitAmt = value;
                this.NotifyPropertyChanged("DebitAmt");
            }
        }

        [DBColumn(Name = "CreditAmt", Storage = "m_CreditAmt", DbType = "Money NULL")]
        public decimal CreditAmt
        {
            get { return this.m_CreditAmt; }
            set
            {
                this.m_CreditAmt = value;
                this.NotifyPropertyChanged("CreditAmt");
            }
        }

        [DBColumn(Name = "TranAmt", Storage = "m_TranAmt", DbType = "Money NULL")]
        public decimal TranAmt
        {
            get { return this.m_TranAmt; }
            set
            {
                this.m_TranAmt = value;
                this.NotifyPropertyChanged("TranAmt");
            }
        }

        [DBColumn(Name = "DebitAmtClose", Storage = "m_DebitAmtClose", DbType = "Money NULL")]
        public decimal DebitAmtClose
        {
            get { return this.m_DebitAmtClose; }
            set
            {
                this.m_DebitAmtClose = value;
                this.NotifyPropertyChanged("DebitAmtClose");
            }
        }

        [DBColumn(Name = "CreditAmtClose", Storage = "m_CreditAmtClose", DbType = "Money NULL")]
        public decimal CreditAmtClose
        {
            get { return this.m_CreditAmtClose; }
            set
            {
                this.m_CreditAmtClose = value;
                this.NotifyPropertyChanged("CreditAmtClose");
            }
        }

        [DBColumn(Name = "CloseAmt", Storage = "m_CloseAmt", DbType = "Money NULL")]
        public decimal CloseAmt
        {
            get { return this.m_CloseAmt; }
            set
            {
                this.m_CloseAmt = value;
                this.NotifyPropertyChanged("CloseAmt");
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

        [DBColumn(Name = "EntryDate", Storage = "m_EntryDate", DbType = "DateTime NULL")]
        public DateTime? EntryDate
        {
            get { return this.m_EntryDate; }
            set
            {
                this.m_EntryDate = value;
                this.NotifyPropertyChanged("EntryDate");
            }
        }

        [DBColumn(Name = "EditDate", Storage = "m_EditDate", DbType = "DateTime NULL")]
        public DateTime? EditDate
        {
            get { return this.m_EditDate; }
            set
            {
                this.m_EditDate = value;
                this.NotifyPropertyChanged("EditDate");
            }
        }

        #endregion //properties
    }

    public partial class dcGLAccountHistory
    {
        #region cusotm properties

        private int m_GLGroupClassID = 0;
        public int GLGroupClassID
        {
            //get { return AppObjectType == null ? m_AppObjectTypeName : this.AppObjectType.AppObjectTypeName; }
            get { return m_GLGroupClassID; }
            set { this.m_GLGroupClassID = value; }
        }

        private string m_GLAccountCode = string.Empty;
        public string GLAccountCode
        {
            //get { return AppObjectType == null ? m_AppObjectTypeName : this.AppObjectType.AppObjectTypeName; }
            get { return m_GLAccountCode; }
            set { this.m_GLAccountCode = value; }
        }

        
        private string m_GLAccountName = string.Empty;
        public string GLAccountName
        {
            //get { return AppObjectType == null ? m_AppObjectTypeName : this.AppObjectType.AppObjectTypeName; }
            get { return m_GLAccountName; }
            set { this.m_GLAccountName = value; }
        }



        private string m_GLGroupName = string.Empty;
        public string GLGroupName
        {
            //get { return AppObjectType == null ? m_AppObjectTypeName : this.AppObjectType.AppObjectTypeName; }
            get { return m_GLGroupName; }
            set { this.m_GLGroupName = value; }
        }


        private int m_GLAccountTypeID = 0;
        public int GLAccountTypeID
        {
            //get { return AppObjectType == null ? m_AppObjectTypeName : this.AppObjectType.AppObjectTypeName; }
            get { return m_GLAccountTypeID; }
            set { this.m_GLAccountTypeID = value; }
        }

        private string m_GLAccountTypeName = string.Empty;
        public string GLAccountTypeName
        {
            //get { return AppObjectType == null ? m_AppObjectTypeName : this.AppObjectType.AppObjectTypeName; }
            get { return m_GLAccountTypeName; }
            set { this.m_GLAccountTypeName = value; }
        }

        #endregion
    }


}
