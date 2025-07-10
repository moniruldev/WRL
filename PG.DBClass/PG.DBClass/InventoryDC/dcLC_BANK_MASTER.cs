using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "LC_BANK_MASTER")]
    public partial class dcLC_BANK_MASTER : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private decimal m_BANK_ID = 0;
        private string m_BANK_CODE = string.Empty;
        private string m_BANK_NAME = string.Empty;
        private string m_HO_ADDRESS = string.Empty;
        private string m_PHONE = string.Empty;
        private string m_PAY_TYPE = string.Empty;
        private decimal m_MATURITY_DAYS = 0;

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


        [DBColumn(Name = "BANK_ID", Storage = "m_BANK_ID", DbType = "107", IsPrimaryKey = true)]
        public decimal BANK_ID
        {
            get { return this.m_BANK_ID; }
            set
            {
                this.m_BANK_ID = value;
                this.NotifyPropertyChanged("BANK_ID");
            }
        }

        [DBColumn(Name = "BANK_CODE", Storage = "m_BANK_CODE", DbType = "126")]
        public string BANK_CODE
        {
            get { return this.m_BANK_CODE; }
            set
            {
                this.m_BANK_CODE = value;
                this.NotifyPropertyChanged("BANK_CODE");
            }
        }

        [DBColumn(Name = "BANK_NAME", Storage = "m_BANK_NAME", DbType = "126")]
        public string BANK_NAME
        {
            get { return this.m_BANK_NAME; }
            set
            {
                this.m_BANK_NAME = value;
                this.NotifyPropertyChanged("BANK_NAME");
            }
        }

        [DBColumn(Name = "HO_ADDRESS", Storage = "m_HO_ADDRESS", DbType = "126")]
        public string HO_ADDRESS
        {
            get { return this.m_HO_ADDRESS; }
            set
            {
                this.m_HO_ADDRESS = value;
                this.NotifyPropertyChanged("HO_ADDRESS");
            }
        }

        [DBColumn(Name = "PHONE", Storage = "m_PHONE", DbType = "126")]
        public string PHONE
        {
            get { return this.m_PHONE; }
            set
            {
                this.m_PHONE = value;
                this.NotifyPropertyChanged("PHONE");
            }
        }

        [DBColumn(Name = "PAY_TYPE", Storage = "m_PAY_TYPE", DbType = "126")]
        public string PAY_TYPE
        {
            get { return this.m_PAY_TYPE; }
            set
            {
                this.m_PAY_TYPE = value;
                this.NotifyPropertyChanged("PAY_TYPE");
            }
        }

        [DBColumn(Name = "MATURITY_DAYS", Storage = "m_MATURITY_DAYS", DbType = "107")]
        public decimal MATURITY_DAYS
        {
            get { return this.m_MATURITY_DAYS; }
            set
            {
                this.m_MATURITY_DAYS = value;
                this.NotifyPropertyChanged("MATURITY_DAYS");
            }
        }

        #endregion //properties
    }
}
