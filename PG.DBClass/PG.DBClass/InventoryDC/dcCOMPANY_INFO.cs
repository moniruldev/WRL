using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "COMPANY_INFO")]
    public partial class dcCOMPANY_INFO : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private decimal m_COMPANY_ID = 0;
        private string m_COMPANY_CODE = string.Empty;
        private string m_COMPANY_NAME = string.Empty;
        private string m_COMPANY_ADDRESS = string.Empty;
        private string m_IS_INVENTORY = string.Empty;

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


        [DBColumn(Name = "COMPANY_ID", Storage = "m_COMPANY_ID", DbType = "107", IsPrimaryKey = true)]
        public decimal COMPANY_ID
        {
            get { return this.m_COMPANY_ID; }
            set
            {
                this.m_COMPANY_ID = value;
                this.NotifyPropertyChanged("COMPANY_ID");
            }
        }

        [DBColumn(Name = "COMPANY_CODE", Storage = "m_COMPANY_CODE", DbType = "126")]
        public string COMPANY_CODE
        {
            get { return this.m_COMPANY_CODE; }
            set
            {
                this.m_COMPANY_CODE = value;
                this.NotifyPropertyChanged("COMPANY_CODE");
            }
        }

        [DBColumn(Name = "COMPANY_NAME", Storage = "m_COMPANY_NAME", DbType = "126")]
        public string COMPANY_NAME
        {
            get { return this.m_COMPANY_NAME; }
            set
            {
                this.m_COMPANY_NAME = value;
                this.NotifyPropertyChanged("COMPANY_NAME");
            }
        }

        [DBColumn(Name = "COMPANY_ADDRESS", Storage = "m_COMPANY_ADDRESS", DbType = "126")]
        public string COMPANY_ADDRESS
        {
            get { return this.m_COMPANY_ADDRESS; }
            set
            {
                this.m_COMPANY_ADDRESS = value;
                this.NotifyPropertyChanged("COMPANY_ADDRESS");
            }
        }

        [DBColumn(Name = "IS_INVENTORY", Storage = "m_IS_INVENTORY", DbType = "126")]
        public string IS_INVENTORY
        {
            get { return this.m_IS_INVENTORY; }
            set
            {
                this.m_IS_INVENTORY = value;
                this.NotifyPropertyChanged("IS_INVENTORY");
            }
        }

        #endregion //properties
    }
}
