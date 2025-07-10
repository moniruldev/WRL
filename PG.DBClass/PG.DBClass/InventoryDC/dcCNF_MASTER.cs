using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "CNF_MASTER")]
    public partial class dcCNF_MASTER : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private decimal m_CNF_ID = 0;
        private string m_CNF_CODE = string.Empty;
        private string m_CNF_NAME = string.Empty;
        private string m_ADDRESS = string.Empty;
        private string m_PHONE = string.Empty;
        private string m_CNF_TYPE = string.Empty;
        private decimal m_COUNTRY_ID = 0;
        private decimal m_ENTRY_BY = 0;
        private DateTime? m_ENTRY_DATE = null;
        private string m_CONTACT_PERSON = string.Empty;
        private string m_CONTACT_PHONE = string.Empty;

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


        [DBColumn(Name = "CNF_ID", Storage = "m_CNF_ID", DbType = "107", IsPrimaryKey = true)]
        public decimal CNF_ID
        {
            get { return this.m_CNF_ID; }
            set
            {
                this.m_CNF_ID = value;
                this.NotifyPropertyChanged("CNF_ID");
            }
        }

        [DBColumn(Name = "CNF_CODE", Storage = "m_CNF_CODE", DbType = "126")]
        public string CNF_CODE
        {
            get { return this.m_CNF_CODE; }
            set
            {
                this.m_CNF_CODE = value;
                this.NotifyPropertyChanged("CNF_CODE");
            }
        }

        [DBColumn(Name = "CNF_NAME", Storage = "m_CNF_NAME", DbType = "126")]
        public string CNF_NAME
        {
            get { return this.m_CNF_NAME; }
            set
            {
                this.m_CNF_NAME = value;
                this.NotifyPropertyChanged("CNF_NAME");
            }
        }

        [DBColumn(Name = "ADDRESS", Storage = "m_ADDRESS", DbType = "126")]
        public string ADDRESS
        {
            get { return this.m_ADDRESS; }
            set
            {
                this.m_ADDRESS = value;
                this.NotifyPropertyChanged("ADDRESS");
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

        [DBColumn(Name = "CNF_TYPE", Storage = "m_CNF_TYPE", DbType = "126")]
        public string CNF_TYPE
        {
            get { return this.m_CNF_TYPE; }
            set
            {
                this.m_CNF_TYPE = value;
                this.NotifyPropertyChanged("CNF_TYPE");
            }
        }

        [DBColumn(Name = "COUNTRY_ID", Storage = "m_COUNTRY_ID", DbType = "107")]
        public decimal COUNTRY_ID
        {
            get { return this.m_COUNTRY_ID; }
            set
            {
                this.m_COUNTRY_ID = value;
                this.NotifyPropertyChanged("COUNTRY_ID");
            }
        }

        [DBColumn(Name = "ENTRY_BY", Storage = "m_ENTRY_BY", DbType = "107")]
        public decimal ENTRY_BY
        {
            get { return this.m_ENTRY_BY; }
            set
            {
                this.m_ENTRY_BY = value;
                this.NotifyPropertyChanged("ENTRY_BY");
            }
        }

        [DBColumn(Name = "ENTRY_DATE", Storage = "m_ENTRY_DATE", DbType = "106")]
        public DateTime? ENTRY_DATE
        {
            get { return this.m_ENTRY_DATE; }
            set
            {
                this.m_ENTRY_DATE = value;
                this.NotifyPropertyChanged("ENTRY_DATE");
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

        [DBColumn(Name = "CONTACT_PHONE", Storage = "m_CONTACT_PHONE", DbType = "126")]
        public string CONTACT_PHONE
        {
            get { return this.m_CONTACT_PHONE; }
            set
            {
                this.m_CONTACT_PHONE = value;
                this.NotifyPropertyChanged("CONTACT_PHONE");
            }
        }

        #endregion //properties
    }
}
