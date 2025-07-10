using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
    [Serializable]
    [DBTable(Name = "SUPPLIER_INFO")]
    public partial class dcSUPPLIER_INFO : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_SUP_ID = 0;
        private string m_SUP_CODE = string.Empty;
        private string m_SUP_NAME = string.Empty;
        private string m_SUP_ADDRESS = string.Empty;
        private string m_SUP_PHONE = string.Empty;
        private string m_CONTACT_PERSON = string.Empty;
        private string m_SUP_PHONE2 = string.Empty;
        private string m_CONTACT_PERSON_PHONE = string.Empty;
        private string m_CONTACT_PERSON_PHONE2 = string.Empty;
        private string m_EMAIL = string.Empty;
        private int? m_COUNTRY_ID = 0;
        private string m_VAT_REG_NO = string.Empty;
        private string m_IS_MANUFACTURER = string.Empty;
        private string m_IS_ENLISTED = string.Empty;
        private string m_ADVISE_BANK = string.Empty;
        private int m_COMPANY_ID = 0;
        private string m_SUP_TYPE = string.Empty;

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


        [DBColumn(Name = "SUP_ID", Storage = "m_SUP_ID", DbType = "107")]
        public int SUP_ID
        {
            get { return this.m_SUP_ID; }
            set
            {
                this.m_SUP_ID = value;
                this.NotifyPropertyChanged("SUP_ID");
            }
        }

        [DBColumn(Name = "SUP_CODE", Storage = "m_SUP_CODE", DbType = "126")]
        public string SUP_CODE
        {
            get { return this.m_SUP_CODE; }
            set
            {
                this.m_SUP_CODE = value;
                this.NotifyPropertyChanged("SUP_CODE");
            }
        }

        [DBColumn(Name = "SUP_NAME", Storage = "m_SUP_NAME", DbType = "126")]
        public string SUP_NAME
        {
            get { return this.m_SUP_NAME; }
            set
            {
                this.m_SUP_NAME = value;
                this.NotifyPropertyChanged("SUP_NAME");
            }
        }

        [DBColumn(Name = "SUP_ADDRESS", Storage = "m_SUP_ADDRESS", DbType = "126")]
        public string SUP_ADDRESS
        {
            get { return this.m_SUP_ADDRESS; }
            set
            {
                this.m_SUP_ADDRESS = value;
                this.NotifyPropertyChanged("SUP_ADDRESS");
            }
        }

        [DBColumn(Name = "SUP_PHONE", Storage = "m_SUP_PHONE", DbType = "126")]
        public string SUP_PHONE
        {
            get { return this.m_SUP_PHONE; }
            set
            {
                this.m_SUP_PHONE = value;
                this.NotifyPropertyChanged("SUP_PHONE");
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

        [DBColumn(Name = "SUP_PHONE2", Storage = "m_SUP_PHONE2", DbType = "126")]
        public string SUP_PHONE2
        {
            get { return this.m_SUP_PHONE2; }
            set
            {
                this.m_SUP_PHONE2 = value;
                this.NotifyPropertyChanged("SUP_PHONE2");
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

        [DBColumn(Name = "VAT_REG_NO", Storage = "m_VAT_REG_NO", DbType = "126")]
        public string VAT_REG_NO
        {
            get { return this.m_VAT_REG_NO; }
            set
            {
                this.m_VAT_REG_NO = value;
                this.NotifyPropertyChanged("VAT_REG_NO");
            }
        }

        [DBColumn(Name = "IS_MANUFACTURER", Storage = "m_IS_MANUFACTURER", DbType = "126")]
        public string IS_MANUFACTURER
        {
            get { return this.m_IS_MANUFACTURER; }
            set
            {
                this.m_IS_MANUFACTURER = value;
                this.NotifyPropertyChanged("IS_MANUFACTURER");
            }
        }

        [DBColumn(Name = "IS_ENLISTED", Storage = "m_IS_ENLISTED", DbType = "126")]
        public string IS_ENLISTED
        {
            get { return this.m_IS_ENLISTED; }
            set
            {
                this.m_IS_ENLISTED = value;
                this.NotifyPropertyChanged("IS_ENLISTED");
            }
        }

        [DBColumn(Name = "ADVISE_BANK", Storage = "m_ADVISE_BANK", DbType = "126")]
        public string ADVISE_BANK
        {
            get { return this.m_ADVISE_BANK; }
            set
            {
                this.m_ADVISE_BANK = value;
                this.NotifyPropertyChanged("ADVISE_BANK");
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

        [DBColumn(Name = "SUP_TYPE", Storage = "m_SUP_TYPE", DbType = "126")]
        public string SUP_TYPE
        {
            get { return this.m_SUP_TYPE; }
            set
            {
                this.m_SUP_TYPE = value;
                this.NotifyPropertyChanged("SUP_TYPE");
            }
        }

        #endregion //properties
    }

    public partial class dcSUPPLIER_INFO
    {
        public string COUNTRY_NAME { get; set; }
        //public string MSR_NAME { get; set; }
        //public string STORE_NAME { get; set; }
    }
}
