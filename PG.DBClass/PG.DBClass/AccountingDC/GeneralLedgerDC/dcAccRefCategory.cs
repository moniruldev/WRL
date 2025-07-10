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
    [DBTable(Name = "tblAccRefCategory")]
    public partial class dcAccRefCategory : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_AccRefCategoryID = 0;
        private string m_AccRefCategoryCode = string.Empty;
        private string m_AccRefCategoryName = string.Empty;
        private int m_AccRefCategoryIDParent = 0;
        private int m_AccRefTypeID = 0;
        private int m_CompanyID = 0;

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


        [DBColumn(Name = "AccRefCategoryID", Storage = "m_AccRefCategoryID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true,IsIdentity=true)]
        public int AccRefCategoryID
        {
            get { return this.m_AccRefCategoryID; }
            set
            {
                this.m_AccRefCategoryID = value;
                this.NotifyPropertyChanged("AccRefCategoryID");
            }
        }

        [DBColumn(Name = "AccRefCategoryCode", Storage = "m_AccRefCategoryCode", DbType = "NVarChar(50) NULL")]
        public string AccRefCategoryCode
        {
            get { return this.m_AccRefCategoryCode; }
            set
            {
                this.m_AccRefCategoryCode = value;
                this.NotifyPropertyChanged("AccRefCategoryCode");
            }
        }

        [DBColumn(Name = "AccRefCategoryName", Storage = "m_AccRefCategoryName", DbType = "NVarChar(100) NULL")]
        public string AccRefCategoryName
        {
            get { return this.m_AccRefCategoryName; }
            set
            {
                this.m_AccRefCategoryName = value;
                this.NotifyPropertyChanged("AccRefCategoryName");
            }
        }

        [DBColumn(Name = "AccRefCategoryIDParent", Storage = "m_AccRefCategoryIDParent", DbType = "Int NULL")]
        public int AccRefCategoryIDParent
        {
            get { return this.m_AccRefCategoryIDParent; }
            set
            {
                this.m_AccRefCategoryIDParent = value;
                this.NotifyPropertyChanged("AccRefCategoryIDParent");
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

        #endregion //properties
    }


    public partial class dcAccRefCategory
    {
        private string m_AccRefTypeName = string.Empty;
        public string AccRefTypeName
        {
            get { return m_AccRefTypeName; }
            set { m_AccRefTypeName = value; }
        }

        private string m_AccRefCategoryNameParent = string.Empty;
        public string AccRefCategoryNameParent
        {
            get { return m_AccRefCategoryNameParent; }
            set { m_AccRefCategoryNameParent = value; }
        }


        private int m_AccYearID = 0;
        public int AccYearID
        {
            get { return m_AccYearID; }
            set { m_AccYearID = value; }
        }

        #region openingYear
        private decimal m_OpenDebitAmtYear = 0;
        public decimal OpenDebitAmtYear
        {
            get { return this.m_OpenDebitAmtYear; }
            set { this.m_OpenDebitAmtYear = value; }
        }

        private decimal m_OpenCreditAmtYear = 0;
        public decimal OpenCreditAmtYear
        {
            get { return this.m_OpenCreditAmtYear; }
            set { this.m_OpenCreditAmtYear = value; }
        }


        private decimal m_OpenAmtYear = 0;
        public decimal OpenAmtYear
        {
            get { return m_OpenAmtYear; }
            set { m_OpenAmtYear = value; }
        }

        private decimal m_OpenDebitBalanceAmtYear = 0;
        public decimal OpenDebitBalanceAmtYear
        {
            get { return m_OpenDebitBalanceAmtYear; }
            set { m_OpenDebitBalanceAmtYear = value; }
        }

        private decimal m_OpenCreditBalanceAmtYear = 0;
        public decimal OpenCreditBalanceAmtYear
        {
            get { return m_OpenCreditBalanceAmtYear; }
            set { m_OpenCreditBalanceAmtYear = value; }
        }



        private decimal m_OpenBalnceAmtYear = 0;
        public decimal OpenBalnceAmtYear
        {
            get { return m_OpenBalnceAmtYear; }
            set { m_OpenBalnceAmtYear = value; }
        }

        private int m_DrCrOpenYear = 0;
        public int DrCrOpenYear
        {
            get { return this.m_DrCrOpenYear; }
            set { this.m_DrCrOpenYear = value; }
        }

        private string m_DrCrOpenTextYear = string.Empty;
        public string DrCrOpenTextYear
        {
            get { return this.m_DrCrOpenTextYear; }
            set { this.m_DrCrOpenTextYear = value; }
        }

        #endregion

        #region opening daterange
        private decimal m_OpenDebitAmtDateRange = 0;
        public decimal OpenDebitAmtDateRange
        {
            get { return this.m_OpenDebitAmtDateRange; }
            set { this.m_OpenDebitAmtDateRange = value; }
        }

        private decimal m_OpenCreditAmtDateRange = 0;
        public decimal OpenCreditAmtDateRange
        {
            get { return this.m_OpenCreditAmtDateRange; }
            set { this.m_OpenCreditAmtDateRange = value; }
        }


        private decimal m_OpenAmtDateRange = 0;
        public decimal OpenAmtDateRange
        {
            get { return m_OpenAmtDateRange; }
            set { m_OpenAmtDateRange = value; }
        }


        private decimal m_OpenDebitBalanceAmtDateRange = 0;
        public decimal OpenDebitBalanceAmtDateRange
        {
            get { return m_OpenDebitBalanceAmtDateRange; }
            set { m_OpenDebitBalanceAmtDateRange = value; }
        }

        private decimal m_OpenCreditBalanceAmtDateRange = 0;
        public decimal OpenCreditBalanceAmtDateRange
        {
            get { return m_OpenCreditBalanceAmtDateRange; }
            set { m_OpenCreditBalanceAmtDateRange = value; }
        }



        private decimal m_OpenBalanceAmtDateRange = 0;
        public decimal OpenBalanceAmtDateRange
        {
            get { return m_OpenBalanceAmtDateRange; }
            set { m_OpenBalanceAmtDateRange = value; }
        }

        private int m_DrCrOpenDateRange = 0;
        public int DrCrOpenDateRange
        {
            get { return this.m_DrCrOpenDateRange; }
            set { this.m_DrCrOpenDateRange = value; }
        }

        private string m_DrCrOpenTextDateRange = string.Empty;
        public string DrCrOpenTextDateRange
        {
            get { return this.m_DrCrOpenTextDateRange; }
            set { this.m_DrCrOpenTextDateRange = value; }
        }

        #endregion

        #region opening
        private decimal m_OpenDebitAmt = 0;
        public decimal OpenDebitAmt
        {
            get { return this.m_OpenDebitAmt; }
            set { this.m_OpenDebitAmt = value; }
        }

        private decimal m_OpenCreditAmt = 0;
        public decimal OpenCreditAmt
        {
            get { return this.m_OpenCreditAmt; }
            set { this.m_OpenCreditAmt = value; }
        }


        private decimal m_OpenAmt = 0;
        public decimal OpenAmt
        {
            get { return m_OpenAmt; }
            set { m_OpenAmt = value; }
        }


        private decimal m_OpenDebitBalanceAmt = 0;
        public decimal OpenDebitBalanceAmt
        {
            get { return m_OpenDebitBalanceAmt; }
            set { m_OpenDebitBalanceAmt = value; }
        }

        private decimal m_OpenCreditBalanceAmt = 0;
        public decimal OpenCreditBalanceAmt
        {
            get { return m_OpenCreditBalanceAmt; }
            set { m_OpenCreditBalanceAmt = value; }
        }


        private decimal m_OpenBalanceAmt = 0;
        public decimal OpenBalanceAmt
        {
            get { return m_OpenBalanceAmt; }
            set { m_OpenBalanceAmt = value; }
        }

        private int m_DrCrOpen = 0;
        public int DrCrOpen
        {
            get { return this.m_DrCrOpen; }
            set { this.m_DrCrOpen = value; }
        }

        private string m_DrCrOpenText = string.Empty;
        public string DrCrOpenText
        {
            get { return this.m_DrCrOpenText; }
            set { this.m_DrCrOpenText = value; }
        }

        #endregion

        #region trans
        private decimal m_DebitAmt = 0;
        public decimal DebitAmt
        {
            get { return m_DebitAmt; }
            set { m_DebitAmt = value; }
        }

        private decimal m_CreditAmt = 0;
        public decimal CreditAmt
        {
            get { return m_CreditAmt; }
            set { m_CreditAmt = value; }
        }

        private decimal m_TranAmt = 0;
        public decimal TranAmt
        {
            get { return m_TranAmt; }
            set { m_TranAmt = value; }
        }

        private decimal m_TranDebitBalanceAmt = 0;
        public decimal TranDebitBalanceAmt
        {
            get { return m_TranDebitBalanceAmt; }
            set { m_TranDebitBalanceAmt = value; }
        }

        private decimal m_TranCreditBalanceAmt = 0;
        public decimal TranCreditBalanceAmt
        {
            get { return m_TranCreditBalanceAmt; }
            set { m_TranCreditBalanceAmt = value; }
        }


        private decimal m_TranBalanceAmt = 0;
        public decimal TranBalanceAmt
        {
            get { return m_TranBalanceAmt; }
            set { m_TranBalanceAmt = value; }
        }
        private int m_DrCrTranBalance = 0;
        public int DrCrTranBalance
        {
            get { return this.m_DrCrTranBalance; }
            set { this.m_DrCrTranBalance = value; }
        }

        private string m_DrCrTranBalanceText = string.Empty;
        public string DrCrTranBalanceText
        {
            get { return this.m_DrCrTranBalanceText; }
            set { this.m_DrCrTranBalanceText = value; }
        }


        #endregion

        #region closing

        private decimal m_CloseDebitAmt = 0;
        public decimal CloseDebitAmt
        {
            get { return this.m_CloseDebitAmt; }
            set { this.m_CloseDebitAmt = value; }
        }

        private decimal m_CloseCreditAmt = 0;
        public decimal CloseCreditAmt
        {
            get { return this.m_CloseCreditAmt; }
            set { this.m_CloseCreditAmt = value; }
        }


        private decimal m_CloseAmt = 0;
        public decimal CloseAmt
        {
            get { return this.m_CloseAmt; }
            set { this.m_CloseAmt = value; }
        }


        private decimal m_CloseDebitBalanceAmt = 0;
        public decimal CloseDebitBalanceAmt
        {
            get { return m_CloseDebitBalanceAmt; }
            set { m_CloseDebitBalanceAmt = value; }
        }

        private decimal m_CloseCreditBalanceAmt = 0;
        public decimal CloseCreditBalanceAmt
        {
            get { return m_CloseCreditBalanceAmt; }
            set { m_CloseCreditBalanceAmt = value; }
        }

        private decimal m_CloseBalanceAmt = 0;
        public decimal CloseBalanceAmt
        {
            get { return this.m_CloseBalanceAmt; }
            set { this.m_CloseBalanceAmt = value; }
        }

        private int m_DrCrCloseBalance = 0;
        public int DrCrCloseBalance
        {
            get { return this.m_DrCrCloseBalance; }
            set { this.m_DrCrCloseBalance = value; }
        }

        private string m_DrCrCloseBalanceText = string.Empty;
        public string DrCrCloseBalanceText
        {
            get { return this.m_DrCrCloseBalanceText; }
            set { this.m_DrCrCloseBalanceText = value; }
        }

        #endregion

    }
}
