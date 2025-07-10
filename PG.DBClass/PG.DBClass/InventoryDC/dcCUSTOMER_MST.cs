using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "CUSTOMER_MST")]
    public class dcCUSTOMER_MST : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private decimal m_CUST_ID = 0;
        private string m_CUST_NAME = string.Empty;
        private string m_CUST_ADDRESS = string.Empty;
        private string m_CUST_MOBILE = string.Empty;
        private string m_CUST_CONT_PERSON_NAME = string.Empty;
        private string m_CUST_CONT_MOBILE = string.Empty;
        private string m_CUST_TYPE = string.Empty;

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


        [DBColumn(Name = "CUST_ID", Storage = "m_CUST_ID", DbType = "107", IsPrimaryKey = true)]
        public decimal CUST_ID
        {
            get { return this.m_CUST_ID; }
            set
            {
                this.m_CUST_ID = value;
                this.NotifyPropertyChanged("CUST_ID");
            }
        }

        [DBColumn(Name = "CUST_NAME", Storage = "m_CUST_NAME", DbType = "126")]
        public string CUST_NAME
        {
            get { return this.m_CUST_NAME; }
            set
            {
                this.m_CUST_NAME = value;
                this.NotifyPropertyChanged("CUST_NAME");
            }
        }

        [DBColumn(Name = "CUST_ADDRESS", Storage = "m_CUST_ADDRESS", DbType = "126")]
        public string CUST_ADDRESS
        {
            get { return this.m_CUST_ADDRESS; }
            set
            {
                this.m_CUST_ADDRESS = value;
                this.NotifyPropertyChanged("CUST_ADDRESS");
            }
        }

        [DBColumn(Name = "CUST_MOBILE", Storage = "m_CUST_MOBILE", DbType = "126")]
        public string CUST_MOBILE
        {
            get { return this.m_CUST_MOBILE; }
            set
            {
                this.m_CUST_MOBILE = value;
                this.NotifyPropertyChanged("CUST_MOBILE");
            }
        }

        [DBColumn(Name = "CUST_CONT_PERSON_NAME", Storage = "m_CUST_CONT_PERSON_NAME", DbType = "126")]
        public string CUST_CONT_PERSON_NAME
        {
            get { return this.m_CUST_CONT_PERSON_NAME; }
            set
            {
                this.m_CUST_CONT_PERSON_NAME = value;
                this.NotifyPropertyChanged("CUST_CONT_PERSON_NAME");
            }
        }

        [DBColumn(Name = "CUST_CONT_MOBILE", Storage = "m_CUST_CONT_MOBILE", DbType = "126")]
        public string CUST_CONT_MOBILE
        {
            get { return this.m_CUST_CONT_MOBILE; }
            set
            {
                this.m_CUST_CONT_MOBILE = value;
                this.NotifyPropertyChanged("CUST_CONT_MOBILE");
            }
        }

        [DBColumn(Name = "CUST_TYPE", Storage = "m_CUST_TYPE", DbType = "126")]
        public string CUST_TYPE
        {
            get { return this.m_CUST_TYPE; }
            set
            {
                this.m_CUST_TYPE = value;
                this.NotifyPropertyChanged("CUST_TYPE");
            }
        }

        #endregion //properties
    }
}
