using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass
{
    [Serializable]
    [DBTable(Name = "DEPARTMENT_INFO")]
    public partial class dcDEPARTMENT_INFO : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_DEPARTMENT_ID = 0;
        private string m_DEPARTMENT_CODE = string.Empty;
        private string m_DEPARTMENT_NAME = string.Empty;
        private int m_COMPANY_ID = 0;
        private string m_DEPT_ADMIN = string.Empty;
        private string m_UNIT = string.Empty;
        private string m_IS_SALES_DEPT = string.Empty;
        private string m_IS_STORE = string.Empty;
        private int m_STORE_ID = 0;
        private int m_DEP_HEAD_EMP_KEY = 0;

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


        [DBColumn(Name = "DEPARTMENT_ID", Storage = "m_DEPARTMENT_ID", DbType = "107", IsPrimaryKey = true)]
        public int DEPARTMENT_ID
        {
            get { return this.m_DEPARTMENT_ID; }
            set
            {
                this.m_DEPARTMENT_ID = value;
                this.NotifyPropertyChanged("DEPARTMENT_ID");
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

        [DBColumn(Name = "DEPARTMENT_NAME", Storage = "m_DEPARTMENT_NAME", DbType = "126")]
        public string DEPARTMENT_NAME
        {
            get { return this.m_DEPARTMENT_NAME; }
            set
            {
                this.m_DEPARTMENT_NAME = value;
                this.NotifyPropertyChanged("DEPARTMENT_NAME");
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

        [DBColumn(Name = "DEPT_ADMIN", Storage = "m_DEPT_ADMIN", DbType = "126")]
        public string DEPT_ADMIN
        {
            get { return this.m_DEPT_ADMIN; }
            set
            {
                this.m_DEPT_ADMIN = value;
                this.NotifyPropertyChanged("DEPT_ADMIN");
            }
        }

        [DBColumn(Name = "UNIT", Storage = "m_UNIT", DbType = "126")]
        public string UNIT
        {
            get { return this.m_UNIT; }
            set
            {
                this.m_UNIT = value;
                this.NotifyPropertyChanged("UNIT");
            }
        }

        [DBColumn(Name = "IS_SALES_DEPT", Storage = "m_IS_SALES_DEPT", DbType = "126")]
        public string IS_SALES_DEPT
        {
            get { return this.m_IS_SALES_DEPT; }
            set
            {
                this.m_IS_SALES_DEPT = value;
                this.NotifyPropertyChanged("IS_SALES_DEPT");
            }
        }

        [DBColumn(Name = "IS_STORE", Storage = "m_IS_STORE", DbType = "126")]
        public string IS_STORE
        {
            get { return this.m_IS_STORE; }
            set
            {
                this.m_IS_STORE = value;
                this.NotifyPropertyChanged("IS_STORE");
            }
        }

        [DBColumn(Name = "STORE_ID", Storage = "m_STORE_ID", DbType = "107")]
        public int STORE_ID
        {
            get { return this.m_STORE_ID; }
            set
            {
                this.m_STORE_ID = value;
                this.NotifyPropertyChanged("STORE_ID");
            }
        }

        [DBColumn(Name = "DEP_HEAD_EMP_KEY", Storage = "m_DEP_HEAD_EMP_KEY", DbType = "107")]
        public int DEP_HEAD_EMP_KEY
        {
            get { return this.m_DEP_HEAD_EMP_KEY; }
            set
            {
                this.m_DEP_HEAD_EMP_KEY = value;
                this.NotifyPropertyChanged("DEP_HEAD_EMP_KEY");
            }
        }

        #endregion //properties
    }

    
      public partial class dcDEPARTMENT_INFO
      {
          public bool AllowLogin { set; get; }
      }
}
