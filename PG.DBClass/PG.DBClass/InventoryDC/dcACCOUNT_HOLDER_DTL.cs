using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
    [DBTable(Name = "ACCOUNT_HOLDER_DTL")]
    public partial class dcACCOUNT_HOLDER_DTL : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private decimal m_ACC_HOLDER_DTL_ID = 0;
        private decimal m_ACC_ID = 0;
        private string m_ACC_HOLDER_ID = string.Empty;
        private decimal m_BANK_ID = 0;
        private decimal m_BRANCH_ID = 0;
        private string m_ACC_NO = string.Empty;
        private string m_RCV_COA_CODE = string.Empty;
        private string m_DR_COA_CODE = string.Empty;

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


        [DBColumn(Name = "ACC_HOLDER_DTL_ID", Storage = "m_ACC_HOLDER_DTL_ID", DbType = "107", IsPrimaryKey = true)]
        public decimal ACC_HOLDER_DTL_ID
        {
            get { return this.m_ACC_HOLDER_DTL_ID; }
            set
            {
                this.m_ACC_HOLDER_DTL_ID = value;
                this.NotifyPropertyChanged("ACC_HOLDER_DTL_ID");
            }
        }

        [DBColumn(Name = "ACC_ID", Storage = "m_ACC_ID", DbType = "107")]
        public decimal ACC_ID
        {
            get { return this.m_ACC_ID; }
            set
            {
                this.m_ACC_ID = value;
                this.NotifyPropertyChanged("ACC_ID");
            }
        }

        [DBColumn(Name = "ACC_HOLDER_ID", Storage = "m_ACC_HOLDER_ID", DbType = "126")]
        public string ACC_HOLDER_ID
        {
            get { return this.m_ACC_HOLDER_ID; }
            set
            {
                this.m_ACC_HOLDER_ID = value;
                this.NotifyPropertyChanged("ACC_HOLDER_ID");
            }
        }

        [DBColumn(Name = "BANK_ID", Storage = "m_BANK_ID", DbType = "107")]
        public decimal BANK_ID
        {
            get { return this.m_BANK_ID; }
            set
            {
                this.m_BANK_ID = value;
                this.NotifyPropertyChanged("BANK_ID");
            }
        }

        [DBColumn(Name = "BRANCH_ID", Storage = "m_BRANCH_ID", DbType = "107")]
        public decimal BRANCH_ID
        {
            get { return this.m_BRANCH_ID; }
            set
            {
                this.m_BRANCH_ID = value;
                this.NotifyPropertyChanged("BRANCH_ID");
            }
        }

        [DBColumn(Name = "ACC_NO", Storage = "m_ACC_NO", DbType = "126")]
        public string ACC_NO
        {
            get { return this.m_ACC_NO; }
            set
            {
                this.m_ACC_NO = value;
                this.NotifyPropertyChanged("ACC_NO");
            }
        }

        [DBColumn(Name = "RCV_COA_CODE", Storage = "m_RCV_COA_CODE", DbType = "126")]
        public string RCV_COA_CODE
        {
            get { return this.m_RCV_COA_CODE; }
            set
            {
                this.m_RCV_COA_CODE = value;
                this.NotifyPropertyChanged("RCV_COA_CODE");
            }
        }

        [DBColumn(Name = "DR_COA_CODE", Storage = "m_DR_COA_CODE", DbType = "126")]
        public string DR_COA_CODE
        {
            get { return this.m_DR_COA_CODE; }
            set
            {
                this.m_DR_COA_CODE = value;
                this.NotifyPropertyChanged("DR_COA_CODE");
            }
        }

        #endregion //properties
    }
}
