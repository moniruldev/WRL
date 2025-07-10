using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "INSURANCE_COM_MASTER")]
    public partial class dcINSURANCE_COM_MASTER : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private decimal m_INS_COM_ID = 0;
        private string m_INS_COM_COME = string.Empty;
        private string m_INS_COM_NAME = string.Empty;
        private string m_ADDRESS = string.Empty;
        private string m_PHONE = string.Empty;
        private string m_EMAIL = string.Empty;
        private string m_WEBSITE = string.Empty;
        private string m_INS_COM_TYPE = string.Empty;
        private decimal m_COUNTRY_ID = 0;
        private decimal m_ENTRY_BY = 0;
        private DateTime? m_ENTRY_DATE = null;

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


        [DBColumn(Name = "INS_COM_ID", Storage = "m_INS_COM_ID", DbType = "107", IsPrimaryKey = true)]
        public decimal INS_COM_ID
        {
            get { return this.m_INS_COM_ID; }
            set
            {
                this.m_INS_COM_ID = value;
                this.NotifyPropertyChanged("INS_COM_ID");
            }
        }

        [DBColumn(Name = "INS_COM_COME", Storage = "m_INS_COM_COME", DbType = "126")]
        public string INS_COM_COME
        {
            get { return this.m_INS_COM_COME; }
            set
            {
                this.m_INS_COM_COME = value;
                this.NotifyPropertyChanged("INS_COM_COME");
            }
        }

        [DBColumn(Name = "INS_COM_NAME", Storage = "m_INS_COM_NAME", DbType = "126")]
        public string INS_COM_NAME
        {
            get { return this.m_INS_COM_NAME; }
            set
            {
                this.m_INS_COM_NAME = value;
                this.NotifyPropertyChanged("INS_COM_NAME");
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

        [DBColumn(Name = "WEBSITE", Storage = "m_WEBSITE", DbType = "126")]
        public string WEBSITE
        {
            get { return this.m_WEBSITE; }
            set
            {
                this.m_WEBSITE = value;
                this.NotifyPropertyChanged("WEBSITE");
            }
        }

        [DBColumn(Name = "INS_COM_TYPE", Storage = "m_INS_COM_TYPE", DbType = "126")]
        public string INS_COM_TYPE
        {
            get { return this.m_INS_COM_TYPE; }
            set
            {
                this.m_INS_COM_TYPE = value;
                this.NotifyPropertyChanged("INS_COM_TYPE");
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

        #endregion //properties
    }
}
