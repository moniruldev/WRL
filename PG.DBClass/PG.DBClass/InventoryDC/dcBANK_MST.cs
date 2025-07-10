using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
    [DBTable(Name = "BANK_MST")]
    public partial class dcBANK_MST : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private decimal m_BANK_ID = 0;
        private string m_BANK_CODE = string.Empty;
        private string m_BANK_DESC = string.Empty;
        private string m_STATUS = string.Empty;
        private string m_CREATE_BY = string.Empty;
        private string m_CREATE_DATE = string.Empty;
        private string m_UPDATE_BY = string.Empty;
        private string m_UPDATE_DATE = string.Empty;
        private string m_AUTHO_BY = string.Empty;
        private string m_AUTHO_DATE = string.Empty;
        private string m_B_TYPE = string.Empty;

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

        [DBColumn(Name = "BANK_DESC", Storage = "m_BANK_DESC", DbType = "126")]
        public string BANK_DESC
        {
            get { return this.m_BANK_DESC; }
            set
            {
                this.m_BANK_DESC = value;
                this.NotifyPropertyChanged("BANK_DESC");
            }
        }

        [DBColumn(Name = "STATUS", Storage = "m_STATUS", DbType = "126")]
        public string STATUS
        {
            get { return this.m_STATUS; }
            set
            {
                this.m_STATUS = value;
                this.NotifyPropertyChanged("STATUS");
            }
        }

        [DBColumn(Name = "CREATE_BY", Storage = "m_CREATE_BY", DbType = "126")]
        public string CREATE_BY
        {
            get { return this.m_CREATE_BY; }
            set
            {
                this.m_CREATE_BY = value;
                this.NotifyPropertyChanged("CREATE_BY");
            }
        }

        [DBColumn(Name = "CREATE_DATE", Storage = "m_CREATE_DATE", DbType = "126")]
        public string CREATE_DATE
        {
            get { return this.m_CREATE_DATE; }
            set
            {
                this.m_CREATE_DATE = value;
                this.NotifyPropertyChanged("CREATE_DATE");
            }
        }

        [DBColumn(Name = "UPDATE_BY", Storage = "m_UPDATE_BY", DbType = "126")]
        public string UPDATE_BY
        {
            get { return this.m_UPDATE_BY; }
            set
            {
                this.m_UPDATE_BY = value;
                this.NotifyPropertyChanged("UPDATE_BY");
            }
        }

        [DBColumn(Name = "UPDATE_DATE", Storage = "m_UPDATE_DATE", DbType = "126")]
        public string UPDATE_DATE
        {
            get { return this.m_UPDATE_DATE; }
            set
            {
                this.m_UPDATE_DATE = value;
                this.NotifyPropertyChanged("UPDATE_DATE");
            }
        }

        [DBColumn(Name = "AUTHO_BY", Storage = "m_AUTHO_BY", DbType = "126")]
        public string AUTHO_BY
        {
            get { return this.m_AUTHO_BY; }
            set
            {
                this.m_AUTHO_BY = value;
                this.NotifyPropertyChanged("AUTHO_BY");
            }
        }

        [DBColumn(Name = "AUTHO_DATE", Storage = "m_AUTHO_DATE", DbType = "126")]
        public string AUTHO_DATE
        {
            get { return this.m_AUTHO_DATE; }
            set
            {
                this.m_AUTHO_DATE = value;
                this.NotifyPropertyChanged("AUTHO_DATE");
            }
        }

        [DBColumn(Name = "B_TYPE", Storage = "m_B_TYPE", DbType = "126")]
        public string B_TYPE
        {
            get { return this.m_B_TYPE; }
            set
            {
                this.m_B_TYPE = value;
                this.NotifyPropertyChanged("B_TYPE");
            }
        }

        #endregion //properties
    }
}
