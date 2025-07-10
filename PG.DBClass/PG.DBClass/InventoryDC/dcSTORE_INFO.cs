using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "STORE_INFO")]
    public partial class dcSTORE_INFO : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private decimal m_STORE_ID = 0;
        private string m_STORE_CODE = string.Empty;
        private string m_STORE_NAME = string.Empty;
        private decimal m_STORE_ID_PARENT = 0;
        private decimal m_COMPANY_ID = 0;
        private decimal m_BRANCH_ID = 0;
        private string m_IS_QC = string.Empty;

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


        [DBColumn(Name = "STORE_ID", Storage = "m_STORE_ID", DbType = "107", IsPrimaryKey = true)]
        public decimal STORE_ID
        {
            get { return this.m_STORE_ID; }
            set
            {
                this.m_STORE_ID = value;
                this.NotifyPropertyChanged("STORE_ID");
            }
        }

        [DBColumn(Name = "STORE_CODE", Storage = "m_STORE_CODE", DbType = "126")]
        public string STORE_CODE
        {
            get { return this.m_STORE_CODE; }
            set
            {
                this.m_STORE_CODE = value;
                this.NotifyPropertyChanged("STORE_CODE");
            }
        }

        [DBColumn(Name = "STORE_NAME", Storage = "m_STORE_NAME", DbType = "126")]
        public string STORE_NAME
        {
            get { return this.m_STORE_NAME; }
            set
            {
                this.m_STORE_NAME = value;
                this.NotifyPropertyChanged("STORE_NAME");
            }
        }

        [DBColumn(Name = "STORE_ID_PARENT", Storage = "m_STORE_ID_PARENT", DbType = "107")]
        public decimal STORE_ID_PARENT
        {
            get { return this.m_STORE_ID_PARENT; }
            set
            {
                this.m_STORE_ID_PARENT = value;
                this.NotifyPropertyChanged("STORE_ID_PARENT");
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

        [DBColumn(Name = "BRANCH_ID", Storage = "m_BRANCH_ID", DbType = "107")]
        public decimal BRANCH_ID
        {
            get { return this.m_BRANCH_ID; }
            set
            {
                this.m_BRANCH_ID = value;
                this.NotifyPropertyChanged("BRANCH_ID");
            }
        }

        [DBColumn(Name = "IS_QC", Storage = "m_IS_QC", DbType = "126")]
        public string IS_QC
        {
            get { return this.m_IS_QC; }
            set
            {
                this.m_IS_QC = value;
                this.NotifyPropertyChanged("IS_QC");
            }
        }

        #endregion //properties
    }
}
