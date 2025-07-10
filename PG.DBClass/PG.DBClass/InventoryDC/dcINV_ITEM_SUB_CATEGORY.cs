using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "INV_ITEM_SUB_CATEGORY")]
    public class dcINV_ITEM_SUB_CATEGORY : DBBaseClass, INotifyPropertyChanged 
    {
        #region private members

        private string m_CAT_SUB_ID = string.Empty;
        private string m_CAT_SUB_DESC = string.Empty;
        private string m_COMP_ID = string.Empty;
        private string m_CAT_ID = string.Empty;
        private string m_CREATE_BY = string.Empty;
        private DateTime? m_CREATE_DATE = null;
        private string m_UPDATE_BY = string.Empty;
        private DateTime? m_UPDATE_DATE = null;
        private string m_STORE_ID = string.Empty;
        private string m_BRANCH_CODE = string.Empty;
        private string m_DEPARTMENT_CODE = string.Empty;
        private string m_RCV_DR_COA_CODE = string.Empty;
        private string m_RCV_CR_COA_CODE = string.Empty;
        private string m_ISSUE_DR_COA_CODE = string.Empty;
        private string m_ISSUE_CR_COA_CODE = string.Empty;
        private string m_RC_CODE = string.Empty;
        private string m_COMP_EMP_ID = string.Empty;
        private string m_COMP_BRANCH_CODE = string.Empty;
        private string m_COMP_DEPARTMENT_CODE = string.Empty;

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


        [DBColumn(Name = "CAT_SUB_ID", Storage = "m_CAT_SUB_ID", DbType = "126", IsPrimaryKey = true)]
        public string CAT_SUB_ID
        {
            get { return this.m_CAT_SUB_ID; }
            set
            {
                this.m_CAT_SUB_ID = value;
                this.NotifyPropertyChanged("CAT_SUB_ID");
            }
        }

        [DBColumn(Name = "CAT_SUB_DESC", Storage = "m_CAT_SUB_DESC", DbType = "126")]
        public string CAT_SUB_DESC
        {
            get { return this.m_CAT_SUB_DESC; }
            set
            {
                this.m_CAT_SUB_DESC = value;
                this.NotifyPropertyChanged("CAT_SUB_DESC");
            }
        }

        [DBColumn(Name = "COMP_ID", Storage = "m_COMP_ID", DbType = "126", IsPrimaryKey = true)]
        public string COMP_ID
        {
            get { return this.m_COMP_ID; }
            set
            {
                this.m_COMP_ID = value;
                this.NotifyPropertyChanged("COMP_ID");
            }
        }

        [DBColumn(Name = "CAT_ID", Storage = "m_CAT_ID", DbType = "126", IsPrimaryKey = true)]
        public string CAT_ID
        {
            get { return this.m_CAT_ID; }
            set
            {
                this.m_CAT_ID = value;
                this.NotifyPropertyChanged("CAT_ID");
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

        [DBColumn(Name = "STORE_ID", Storage = "m_STORE_ID", DbType = "126")]
        public string STORE_ID
        {
            get { return this.m_STORE_ID; }
            set
            {
                this.m_STORE_ID = value;
                this.NotifyPropertyChanged("STORE_ID");
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

        [DBColumn(Name = "DEPARTMENT_CODE", Storage = "m_DEPARTMENT_CODE", DbType = "126")]
        public string DEPARTMENT_CODE
        {
            get { return this.m_DEPARTMENT_CODE; }
            set
            {
                this.m_DEPARTMENT_CODE = value;
                this.NotifyPropertyChanged("DEPARTMENT_CODE");
            }
        }

        [DBColumn(Name = "RCV_DR_COA_CODE", Storage = "m_RCV_DR_COA_CODE", DbType = "126")]
        public string RCV_DR_COA_CODE
        {
            get { return this.m_RCV_DR_COA_CODE; }
            set
            {
                this.m_RCV_DR_COA_CODE = value;
                this.NotifyPropertyChanged("RCV_DR_COA_CODE");
            }
        }

        [DBColumn(Name = "RCV_CR_COA_CODE", Storage = "m_RCV_CR_COA_CODE", DbType = "126")]
        public string RCV_CR_COA_CODE
        {
            get { return this.m_RCV_CR_COA_CODE; }
            set
            {
                this.m_RCV_CR_COA_CODE = value;
                this.NotifyPropertyChanged("RCV_CR_COA_CODE");
            }
        }

        [DBColumn(Name = "ISSUE_DR_COA_CODE", Storage = "m_ISSUE_DR_COA_CODE", DbType = "126")]
        public string ISSUE_DR_COA_CODE
        {
            get { return this.m_ISSUE_DR_COA_CODE; }
            set
            {
                this.m_ISSUE_DR_COA_CODE = value;
                this.NotifyPropertyChanged("ISSUE_DR_COA_CODE");
            }
        }

        [DBColumn(Name = "ISSUE_CR_COA_CODE", Storage = "m_ISSUE_CR_COA_CODE", DbType = "126")]
        public string ISSUE_CR_COA_CODE
        {
            get { return this.m_ISSUE_CR_COA_CODE; }
            set
            {
                this.m_ISSUE_CR_COA_CODE = value;
                this.NotifyPropertyChanged("ISSUE_CR_COA_CODE");
            }
        }

        [DBColumn(Name = "RC_CODE", Storage = "m_RC_CODE", DbType = "126")]
        public string RC_CODE
        {
            get { return this.m_RC_CODE; }
            set
            {
                this.m_RC_CODE = value;
                this.NotifyPropertyChanged("RC_CODE");
            }
        }

        [DBColumn(Name = "COMP_EMP_ID", Storage = "m_COMP_EMP_ID", DbType = "126")]
        public string COMP_EMP_ID
        {
            get { return this.m_COMP_EMP_ID; }
            set
            {
                this.m_COMP_EMP_ID = value;
                this.NotifyPropertyChanged("COMP_EMP_ID");
            }
        }

        [DBColumn(Name = "COMP_BRANCH_CODE", Storage = "m_COMP_BRANCH_CODE", DbType = "126")]
        public string COMP_BRANCH_CODE
        {
            get { return this.m_COMP_BRANCH_CODE; }
            set
            {
                this.m_COMP_BRANCH_CODE = value;
                this.NotifyPropertyChanged("COMP_BRANCH_CODE");
            }
        }

        [DBColumn(Name = "COMP_DEPARTMENT_CODE", Storage = "m_COMP_DEPARTMENT_CODE", DbType = "126")]
        public string COMP_DEPARTMENT_CODE
        {
            get { return this.m_COMP_DEPARTMENT_CODE; }
            set
            {
                this.m_COMP_DEPARTMENT_CODE = value;
                this.NotifyPropertyChanged("COMP_DEPARTMENT_CODE");
            }
        }

        #endregion //properties 
    }
}
