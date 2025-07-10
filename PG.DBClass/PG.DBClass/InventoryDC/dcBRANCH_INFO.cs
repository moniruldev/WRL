using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "BRANCH_INFO")]
    public partial class dcBRANCH_INFO : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private decimal m_BRANCH_ID = 0;
        private string m_BRANCH_CODE = string.Empty;
        private string m_BRANCH_NAME = string.Empty;
        private decimal m_COMPANY_ID = 0;
        private string m_COMPANY_CODE = string.Empty;
        private string m_BRANCH_ADDRESS = string.Empty;
        private string m_IS_ACTIVE = string.Empty;
        private string m_IS_VISIBLE = string.Empty;

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


        [DBColumn(Name = "BRANCH_ID", Storage = "m_BRANCH_ID", DbType = "107", IsPrimaryKey = true)]
        public decimal BRANCH_ID
        {
            get { return this.m_BRANCH_ID; }
            set
            {
                this.m_BRANCH_ID = value;
                this.NotifyPropertyChanged("BRANCH_ID");
            }
        }

        [DBColumn(Name = "BRANCH_CODE", Storage = "m_BRANCH_CODE", DbType = "126")]
        public string BRANCH_CODE
        {
            get { return this.m_BRANCH_CODE; }
            set
            {
                this.m_BRANCH_CODE = value;
                this.NotifyPropertyChanged("BRANCH_CODE");
            }
        }

        [DBColumn(Name = "BRANCH_NAME", Storage = "m_BRANCH_NAME", DbType = "126")]
        public string BRANCH_NAME
        {
            get { return this.m_BRANCH_NAME; }
            set
            {
                this.m_BRANCH_NAME = value;
                this.NotifyPropertyChanged("BRANCH_NAME");
            }
        }

        [DBColumn(Name = "COMPANY_ID", Storage = "m_COMPANY_ID", DbType = "107")]
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

        [DBColumn(Name = "BRANCH_ADDRESS", Storage = "m_BRANCH_ADDRESS", DbType = "126")]
        public string BRANCH_ADDRESS
        {
            get { return this.m_BRANCH_ADDRESS; }
            set
            {
                this.m_BRANCH_ADDRESS = value;
                this.NotifyPropertyChanged("BRANCH_ADDRESS");
            }
        }

        [DBColumn(Name = "IS_ACTIVE", Storage = "m_IS_ACTIVE", DbType = "126")]
        public string IS_ACTIVE
        {
            get { return this.m_IS_ACTIVE; }
            set
            {
                this.m_IS_ACTIVE = value;
                this.NotifyPropertyChanged("IS_ACTIVE");
            }
        }

        [DBColumn(Name = "IS_VISIBLE", Storage = "m_IS_VISIBLE", DbType = "126")]
        public string IS_VISIBLE
        {
            get { return this.m_IS_VISIBLE; }
            set
            {
                this.m_IS_VISIBLE = value;
                this.NotifyPropertyChanged("IS_VISIBLE");
            }
        }

        #endregion //properties
    }
}
