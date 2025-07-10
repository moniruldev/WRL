using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
    [DBTable(Name = "CUSTOMER_PAYMENT_MST")]
    public partial class dcCUSTOMER_PAYMENT_MST : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private decimal m_CP_ID = 0;
        private decimal m_CUST_ID = 0;
        private DateTime? m_CP_DATE = null;
        private decimal m_CP_AMOUNT = 0;
        private string m_CP_REMARKS = string.Empty;
        private decimal m_CREATE_BY = 0;
        private DateTime? m_CREATE_DATE = null;
        private decimal m_UPDATE_BY = 0;
        private DateTime? m_UPDATE_DATE = null;
        private string m_AUTH_STATUS = string.Empty;
        private decimal m_AUTH_BY = 0;
        private DateTime? m_AUTH_DATE = null;

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


        [DBColumn(Name = "CP_ID", Storage = "m_CP_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public decimal CP_ID
        {
            get { return this.m_CP_ID; }
            set
            {
                this.m_CP_ID = value;
                this.NotifyPropertyChanged("CP_ID");
            }
        }

        [DBColumn(Name = "CUST_ID", Storage = "m_CUST_ID", DbType = "107")]
        public decimal CUST_ID
        {
            get { return this.m_CUST_ID; }
            set
            {
                this.m_CUST_ID = value;
                this.NotifyPropertyChanged("CUST_ID");
            }
        }

        [DBColumn(Name = "CP_DATE", Storage = "m_CP_DATE", DbType = "106")]
        public DateTime? CP_DATE
        {
            get { return this.m_CP_DATE; }
            set
            {
                this.m_CP_DATE = value;
                this.NotifyPropertyChanged("CP_DATE");
            }
        }

        [DBColumn(Name = "CP_AMOUNT", Storage = "m_CP_AMOUNT", DbType = "107")]
        public decimal CP_AMOUNT
        {
            get { return this.m_CP_AMOUNT; }
            set
            {
                this.m_CP_AMOUNT = value;
                this.NotifyPropertyChanged("CP_AMOUNT");
            }
        }

        [DBColumn(Name = "CP_REMARKS", Storage = "m_CP_REMARKS", DbType = "126")]
        public string CP_REMARKS
        {
            get { return this.m_CP_REMARKS; }
            set
            {
                this.m_CP_REMARKS = value;
                this.NotifyPropertyChanged("CP_REMARKS");
            }
        }

        [DBColumn(Name = "CREATE_BY", Storage = "m_CREATE_BY", DbType = "107")]
        public decimal CREATE_BY
        {
            get { return this.m_CREATE_BY; }
            set
            {
                this.m_CREATE_BY = value;
                this.NotifyPropertyChanged("CREATE_BY");
            }
        }

        [DBColumn(Name = "CREATE_DATE", Storage = "m_CREATE_DATE", DbType = "106")]
        public DateTime? CREATE_DATE
        {
            get { return this.m_CREATE_DATE; }
            set
            {
                this.m_CREATE_DATE = value;
                this.NotifyPropertyChanged("CREATE_DATE");
            }
        }

        [DBColumn(Name = "UPDATE_BY", Storage = "m_UPDATE_BY", DbType = "107")]
        public decimal UPDATE_BY
        {
            get { return this.m_UPDATE_BY; }
            set
            {
                this.m_UPDATE_BY = value;
                this.NotifyPropertyChanged("UPDATE_BY");
            }
        }

        [DBColumn(Name = "UPDATE_DATE", Storage = "m_UPDATE_DATE", DbType = "106")]
        public DateTime? UPDATE_DATE
        {
            get { return this.m_UPDATE_DATE; }
            set
            {
                this.m_UPDATE_DATE = value;
                this.NotifyPropertyChanged("UPDATE_DATE");
            }
        }

        [DBColumn(Name = "AUTH_STATUS", Storage = "m_AUTH_STATUS", DbType = "126")]
        public string AUTH_STATUS
        {
            get { return this.m_AUTH_STATUS; }
            set
            {
                this.m_AUTH_STATUS = value;
                this.NotifyPropertyChanged("AUTH_STATUS");
            }
        }

        [DBColumn(Name = "AUTH_BY", Storage = "m_AUTH_BY", DbType = "107")]
        public decimal AUTH_BY
        {
            get { return this.m_AUTH_BY; }
            set
            {
                this.m_AUTH_BY = value;
                this.NotifyPropertyChanged("AUTH_BY");
            }
        }

        [DBColumn(Name = "AUTH_DATE", Storage = "m_AUTH_DATE", DbType = "106")]
        public DateTime? AUTH_DATE
        {
            get { return this.m_AUTH_DATE; }
            set
            {
                this.m_AUTH_DATE = value;
                this.NotifyPropertyChanged("AUTH_DATE");
            }
        }

        #endregion //properties
    }
}
