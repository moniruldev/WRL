using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "CUSTOMER_INFO")]
    public partial class dcCUSTOMER_INFO : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_CUST_ID = 0;
        private string m_CUST_CODE = string.Empty;
        private string m_CUST_NAME = string.Empty;
        private string m_CUST_ADDRESS = string.Empty;
        private string m_CUST_PHONE = string.Empty;
        private string m_CUST_PHONE2 = string.Empty;
        private string m_EMAIL = string.Empty;
        private int? m_COUNTRY_ID = 0;
        private string m_CONTACT_PERSON = string.Empty;
        private string m_CONTACT_PERSON_PHONE = string.Empty;
        private string m_CONTACT_PERSON_PHONE2 = string.Empty;
        private string m_IS_REFUNDABLE=string.Empty;
        private string m_IS_ROTARY = string.Empty;
        private int m_COMPANY_ID = 0;

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


        [DBColumn(Name = "CUST_ID", Storage = "m_CUST_ID", DbType = "107")]
        public int CUST_ID
        {
            get { return this.m_CUST_ID; }
            set
            {
                this.m_CUST_ID = value;
                this.NotifyPropertyChanged("CUST_ID");
            }
        }

        [DBColumn(Name = "CUST_CODE", Storage = "m_CUST_CODE", DbType = "126")]
        public string CUST_CODE
        {
            get { return this.m_CUST_CODE; }
            set
            {
                this.m_CUST_CODE = value;
                this.NotifyPropertyChanged("CUST_CODE");
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

        [DBColumn(Name = "CUST_PHONE", Storage = "m_CUST_PHONE", DbType = "126")]
        public string CUST_PHONE
        {
            get { return this.m_CUST_PHONE; }
            set
            {
                this.m_CUST_PHONE = value;
                this.NotifyPropertyChanged("CUST_PHONE");
            }
        }

        [DBColumn(Name = "CUST_PHONE2", Storage = "m_CUST_PHONE2", DbType = "126")]
        public string CUST_PHONE2
        {
            get { return this.m_CUST_PHONE2; }
            set
            {
                this.m_CUST_PHONE2 = value;
                this.NotifyPropertyChanged("CUST_PHONE2");
            }
        }

        [DBColumn(Name = "EMAIL", Storage = "m_EMAIL", DbType = "126")]
        public string EMAIL
        {
            get { return this.m_EMAIL; }
            set
            {
                this.m_EMAIL = value;
                this.NotifyPropertyChanged("EMAIL");
            }
        }

        [DBColumn(Name = "COUNTRY_ID", Storage = "m_COUNTRY_ID", DbType = "107")]
        public int? COUNTRY_ID
        {
            get { return this.m_COUNTRY_ID; }
            set
            {
                this.m_COUNTRY_ID = value;
                this.NotifyPropertyChanged("COUNTRY_ID");
            }
        }

        [DBColumn(Name = "CONTACT_PERSON", Storage = "m_CONTACT_PERSON", DbType = "126")]
        public string CONTACT_PERSON
        {
            get { return this.m_CONTACT_PERSON; }
            set
            {
                this.m_CONTACT_PERSON = value;
                this.NotifyPropertyChanged("CONTACT_PERSON");
            }
        }

        [DBColumn(Name = "CONTACT_PERSON_PHONE", Storage = "m_CONTACT_PERSON_PHONE", DbType = "126")]
        public string CONTACT_PERSON_PHONE
        {
            get { return this.m_CONTACT_PERSON_PHONE; }
            set
            {
                this.m_CONTACT_PERSON_PHONE = value;
                this.NotifyPropertyChanged("CONTACT_PERSON_PHONE");
            }
        }

        [DBColumn(Name = "CONTACT_PERSON_PHONE2", Storage = "m_CONTACT_PERSON_PHONE2", DbType = "126")]
        public string CONTACT_PERSON_PHONE2
        {
            get { return this.m_CONTACT_PERSON_PHONE2; }
            set
            {
                this.m_CONTACT_PERSON_PHONE2 = value;
                this.NotifyPropertyChanged("CONTACT_PERSON_PHONE2");
            }
        }

        [DBColumn(Name = "IS_REFUNDABLE", Storage = "m_IS_REFUNDABLE", DbType = "126")]
        public string IS_REFUNDABLE
        {
            get { return this.m_IS_REFUNDABLE; }
            set
            {
                this.m_IS_REFUNDABLE = value;
                this.NotifyPropertyChanged("IS_REFUNDABLE");
            }
        }

        [DBColumn(Name = "IS_ROTARY", Storage = "m_IS_ROTARY", DbType = "126")]
        public string IS_ROTARY
        {
            get { return this.m_IS_ROTARY; }
            set
            {
                this.m_IS_ROTARY = value;
                this.NotifyPropertyChanged("IS_ROTARY");
            }
        }

        [DBColumn(Name = "COMPANY_ID", Storage = "m_COMPANY_ID", DbType = "107")]
        public int COMPANY_ID
        {
            get { return this.m_COMPANY_ID; }
            set
            {
                this.m_COMPANY_ID = value;
                this.NotifyPropertyChanged("COMPANY_ID");
            }
        }

        #endregion //properties
    }
    public partial class dcCUSTOMER_INFO
    {
        //public string ITEM_NAME { get; set; }
        //public string MSR_NAME { get; set; }
        //public string STORE_NAME { get; set; }

    }
}

