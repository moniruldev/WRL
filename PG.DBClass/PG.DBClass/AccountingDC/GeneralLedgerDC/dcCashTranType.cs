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
    [DBTable(Name = "tblCashTranType")]
    public partial class dcCashTranType : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_CashTranTypeID = 0;
        private string m_CashTranTypeCode = string.Empty;
        private string m_CashTranTypeName = string.Empty;
        private int m_CashTranTypeSLNo = 0;
        private bool m_IsBankTranType = false;
        private bool m_IsSystem = false;
        private bool m_IsVisible = false;
        private bool m_IsActive = false;

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


        [DBColumn(Name = "CashTranTypeID", Storage = "m_CashTranTypeID", DbType = "Int NOT NULL", IsPrimaryKey = true)]
        public int CashTranTypeID
        {
            get { return this.m_CashTranTypeID; }
            set
            {
                this.m_CashTranTypeID = value;
                this.NotifyPropertyChanged("CashTranTypeID");
            }
        }

        [DBColumn(Name = "CashTranTypeCode", Storage = "m_CashTranTypeCode", DbType = "NVarChar(20) NULL")]
        public string CashTranTypeCode
        {
            get { return this.m_CashTranTypeCode; }
            set
            {
                this.m_CashTranTypeCode = value;
                this.NotifyPropertyChanged("CashTranTypeCode");
            }
        }

        [DBColumn(Name = "CashTranTypeName", Storage = "m_CashTranTypeName", DbType = "NVarChar(50) NULL")]
        public string CashTranTypeName
        {
            get { return this.m_CashTranTypeName; }
            set
            {
                this.m_CashTranTypeName = value;
                this.NotifyPropertyChanged("CashTranTypeName");
            }
        }

        [DBColumn(Name = "CashTranTypeSLNo", Storage = "m_CashTranTypeSLNo", DbType = "Int NULL")]
        public int CashTranTypeSLNo
        {
            get { return this.m_CashTranTypeSLNo; }
            set
            {
                this.m_CashTranTypeSLNo = value;
                this.NotifyPropertyChanged("CashTranTypeSLNo");
            }
        }

        [DBColumn(Name = "IsBankTranType", Storage = "m_IsBankTranType", DbType = "Bit NULL")]
        public bool IsBankTranType
        {
            get { return this.m_IsBankTranType; }
            set
            {
                this.m_IsBankTranType = value;
                this.NotifyPropertyChanged("IsBankTranType");
            }
        }

        [DBColumn(Name = "IsSystem", Storage = "m_IsSystem", DbType = "Bit NULL")]
        public bool IsSystem
        {
            get { return this.m_IsSystem; }
            set
            {
                this.m_IsSystem = value;
                this.NotifyPropertyChanged("IsSystem");
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

        [DBColumn(Name = "IsActive", Storage = "m_IsActive", DbType = "Bit NULL")]
        public bool IsActive
        {
            get { return this.m_IsActive; }
            set
            {
                this.m_IsActive = value;
                this.NotifyPropertyChanged("IsActive");
            }
        }

        #endregion //properties
    }
}
