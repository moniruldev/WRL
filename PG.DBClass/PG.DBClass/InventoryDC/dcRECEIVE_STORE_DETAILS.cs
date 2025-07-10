using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "RECEIVE_STORE_DETAILS")]
   public class dcRECEIVE_STORE_DETAILS : DBBaseClass, INotifyPropertyChanged 
    {
        #region private members

        private string m_STORE_ID = string.Empty;
        private string m_STORE_DESC = string.Empty;
        private string m_BRANCH_CODE = string.Empty;
        private string m_DEPARTMENT_CODE = string.Empty;
        private string m_COMP_ID = string.Empty;
        private string m_USER_ID = string.Empty;
        private DateTime? m_ENTER_DATE = null;
        private DateTime? m_UPDATE_DATE = null;
        private string m_UPDATE_BY = string.Empty;

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


        [DBColumn(Name = "STORE_ID", Storage = "m_STORE_ID", DbType = "126", IsPrimaryKey = true)]
        public string STORE_ID
        {
            get { return this.m_STORE_ID; }
            set
            {
                this.m_STORE_ID = value;
                this.NotifyPropertyChanged("STORE_ID");
            }
        }

        [DBColumn(Name = "STORE_DESC", Storage = "m_STORE_DESC", DbType = "126")]
        public string STORE_DESC
        {
            get { return this.m_STORE_DESC; }
            set
            {
                this.m_STORE_DESC = value;
                this.NotifyPropertyChanged("STORE_DESC");
            }
        }

        [DBColumn(Name = "BRANCH_CODE", Storage = "m_BRANCH_CODE", DbType = "126", IsPrimaryKey = true)]
        public string BRANCH_CODE
        {
            get { return this.m_BRANCH_CODE; }
            set
            {
                this.m_BRANCH_CODE = value;
                this.NotifyPropertyChanged("BRANCH_CODE");
            }
        }

        [DBColumn(Name = "DEPARTMENT_CODE", Storage = "m_DEPARTMENT_CODE", DbType = "126", IsPrimaryKey = true)]
        public string DEPARTMENT_CODE
        {
            get { return this.m_DEPARTMENT_CODE; }
            set
            {
                this.m_DEPARTMENT_CODE = value;
                this.NotifyPropertyChanged("DEPARTMENT_CODE");
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

        [DBColumn(Name = "USER_ID", Storage = "m_USER_ID", DbType = "126")]
        public string USER_ID
        {
            get { return this.m_USER_ID; }
            set
            {
                this.m_USER_ID = value;
                this.NotifyPropertyChanged("USER_ID");
            }
        }

        [DBColumn(Name = "ENTER_DATE", Storage = "m_ENTER_DATE", DbType = "106")]
        public DateTime? ENTER_DATE
        {
            get { return this.m_ENTER_DATE; }
            set
            {
                this.m_ENTER_DATE = value;
                this.NotifyPropertyChanged("ENTER_DATE");
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

        #endregion //properties 
    }
}
