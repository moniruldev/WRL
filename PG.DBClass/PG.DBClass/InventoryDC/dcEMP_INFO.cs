using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "EMP_INFO")]
    public partial class dcEMP_INFO : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private decimal m_EMP_KEY = 0;
        private string m_EMP_ID = string.Empty;
        private string m_FULL_NAME = string.Empty;
        private string m_NICK_NAME = string.Empty;
        private string m_FATHER_NAME = string.Empty;
        private string m_MOTHER_NAME = string.Empty;
        private DateTime? m_DOB = null;
        private DateTime? m_JOINING_DATE = null;
        private DateTime? m_RT_DATE = null;
        private string m_RT_STATUS = string.Empty;
        private string m_DEPARTMENT = string.Empty;
        private string m_DESIGNATION = string.Empty;
        private string m_WORKING_LOCATION = string.Empty;
        private Int16 m_PROVISION_PERIOD = 0;
        private string m_EMP_STATUS = string.Empty;
        private string m_EMPLOYEE_CATEGORY = string.Empty;
        private string m_DEPT_NAME = string.Empty;
        private string m_DESIG_NAME = string.Empty;
        private string m_COMPANY_CODE = string.Empty;
        private string m_BRANCH_CODE = string.Empty;
        private decimal m_COMPANY_ID = 0;
        private decimal m_BRANCH_ID = 0;
        private decimal m_DEPARTMENT_ID = 0;
        private decimal m_DESIGNATION_ID = 0;
        private decimal m_EMP_KEY_PMIS = 0;
        private string m_APP_STATUS = string.Empty;

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


        [DBColumn(Name = "EMP_KEY", Storage = "m_EMP_KEY", DbType = "107")]
        public decimal EMP_KEY
        {
            get { return this.m_EMP_KEY; }
            set
            {
                this.m_EMP_KEY = value;
                this.NotifyPropertyChanged("EMP_KEY");
            }
        }

        [DBColumn(Name = "EMP_ID", Storage = "m_EMP_ID", DbType = "126")]
        public string EMP_ID
        {
            get { return this.m_EMP_ID; }
            set
            {
                this.m_EMP_ID = value;
                this.NotifyPropertyChanged("EMP_ID");
            }
        }

        [DBColumn(Name = "FULL_NAME", Storage = "m_FULL_NAME", DbType = "126")]
        public string FULL_NAME
        {
            get { return this.m_FULL_NAME; }
            set
            {
                this.m_FULL_NAME = value;
                this.NotifyPropertyChanged("FULL_NAME");
            }
        }

        [DBColumn(Name = "NICK_NAME", Storage = "m_NICK_NAME", DbType = "126")]
        public string NICK_NAME
        {
            get { return this.m_NICK_NAME; }
            set
            {
                this.m_NICK_NAME = value;
                this.NotifyPropertyChanged("NICK_NAME");
            }
        }

        [DBColumn(Name = "FATHER_NAME", Storage = "m_FATHER_NAME", DbType = "126")]
        public string FATHER_NAME
        {
            get { return this.m_FATHER_NAME; }
            set
            {
                this.m_FATHER_NAME = value;
                this.NotifyPropertyChanged("FATHER_NAME");
            }
        }

        [DBColumn(Name = "MOTHER_NAME", Storage = "m_MOTHER_NAME", DbType = "126")]
        public string MOTHER_NAME
        {
            get { return this.m_MOTHER_NAME; }
            set
            {
                this.m_MOTHER_NAME = value;
                this.NotifyPropertyChanged("MOTHER_NAME");
            }
        }

        [DBColumn(Name = "DOB", Storage = "m_DOB", DbType = "106")]
        public DateTime? DOB
        {
            get { return this.m_DOB; }
            set
            {
                this.m_DOB = value;
                this.NotifyPropertyChanged("DOB");
            }
        }

        [DBColumn(Name = "JOINING_DATE", Storage = "m_JOINING_DATE", DbType = "106")]
        public DateTime? JOINING_DATE
        {
            get { return this.m_JOINING_DATE; }
            set
            {
                this.m_JOINING_DATE = value;
                this.NotifyPropertyChanged("JOINING_DATE");
            }
        }

        [DBColumn(Name = "RT_DATE", Storage = "m_RT_DATE", DbType = "106")]
        public DateTime? RT_DATE
        {
            get { return this.m_RT_DATE; }
            set
            {
                this.m_RT_DATE = value;
                this.NotifyPropertyChanged("RT_DATE");
            }
        }

        [DBColumn(Name = "RT_STATUS", Storage = "m_RT_STATUS", DbType = "126")]
        public string RT_STATUS
        {
            get { return this.m_RT_STATUS; }
            set
            {
                this.m_RT_STATUS = value;
                this.NotifyPropertyChanged("RT_STATUS");
            }
        }

        [DBColumn(Name = "DEPARTMENT", Storage = "m_DEPARTMENT", DbType = "126")]
        public string DEPARTMENT
        {
            get { return this.m_DEPARTMENT; }
            set
            {
                this.m_DEPARTMENT = value;
                this.NotifyPropertyChanged("DEPARTMENT");
            }
        }

        [DBColumn(Name = "DESIGNATION", Storage = "m_DESIGNATION", DbType = "126")]
        public string DESIGNATION
        {
            get { return this.m_DESIGNATION; }
            set
            {
                this.m_DESIGNATION = value;
                this.NotifyPropertyChanged("DESIGNATION");
            }
        }

        [DBColumn(Name = "WORKING_LOCATION", Storage = "m_WORKING_LOCATION", DbType = "126")]
        public string WORKING_LOCATION
        {
            get { return this.m_WORKING_LOCATION; }
            set
            {
                this.m_WORKING_LOCATION = value;
                this.NotifyPropertyChanged("WORKING_LOCATION");
            }
        }

        [DBColumn(Name = "PROVISION_PERIOD", Storage = "m_PROVISION_PERIOD", DbType = "111")]
        public Int16 PROVISION_PERIOD
        {
            get { return this.m_PROVISION_PERIOD; }
            set
            {
                this.m_PROVISION_PERIOD = value;
                this.NotifyPropertyChanged("PROVISION_PERIOD");
            }
        }

        [DBColumn(Name = "EMP_STATUS", Storage = "m_EMP_STATUS", DbType = "126")]
        public string EMP_STATUS
        {
            get { return this.m_EMP_STATUS; }
            set
            {
                this.m_EMP_STATUS = value;
                this.NotifyPropertyChanged("EMP_STATUS");
            }
        }

        [DBColumn(Name = "EMPLOYEE_CATEGORY", Storage = "m_EMPLOYEE_CATEGORY", DbType = "126")]
        public string EMPLOYEE_CATEGORY
        {
            get { return this.m_EMPLOYEE_CATEGORY; }
            set
            {
                this.m_EMPLOYEE_CATEGORY = value;
                this.NotifyPropertyChanged("EMPLOYEE_CATEGORY");
            }
        }

        [DBColumn(Name = "DEPT_NAME", Storage = "m_DEPT_NAME", DbType = "126")]
        public string DEPT_NAME
        {
            get { return this.m_DEPT_NAME; }
            set
            {
                this.m_DEPT_NAME = value;
                this.NotifyPropertyChanged("DEPT_NAME");
            }
        }

        [DBColumn(Name = "DESIG_NAME", Storage = "m_DESIG_NAME", DbType = "126")]
        public string DESIG_NAME
        {
            get { return this.m_DESIG_NAME; }
            set
            {
                this.m_DESIG_NAME = value;
                this.NotifyPropertyChanged("DESIG_NAME");
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

        [DBColumn(Name = "DEPARTMENT_ID", Storage = "m_DEPARTMENT_ID", DbType = "107")]
        public decimal DEPARTMENT_ID
        {
            get { return this.m_DEPARTMENT_ID; }
            set
            {
                this.m_DEPARTMENT_ID = value;
                this.NotifyPropertyChanged("DEPARTMENT_ID");
            }
        }

        [DBColumn(Name = "DESIGNATION_ID", Storage = "m_DESIGNATION_ID", DbType = "107")]
        public decimal DESIGNATION_ID
        {
            get { return this.m_DESIGNATION_ID; }
            set
            {
                this.m_DESIGNATION_ID = value;
                this.NotifyPropertyChanged("DESIGNATION_ID");
            }
        }

        [DBColumn(Name = "EMP_KEY_PMIS", Storage = "m_EMP_KEY_PMIS", DbType = "107")]
        public decimal EMP_KEY_PMIS
        {
            get { return this.m_EMP_KEY_PMIS; }
            set
            {
                this.m_EMP_KEY_PMIS = value;
                this.NotifyPropertyChanged("EMP_KEY_PMIS");
            }
        }


        [DBColumn(Name = "APP_STATUS", Storage = "m_APP_STATUS", DbType = "126")]
        public string APP_STATUS
        {
            get { return this.m_APP_STATUS; }
            set
            {
                this.m_APP_STATUS = value;
                this.NotifyPropertyChanged("APP_STATUS");
            }
        }
        #endregion //properties
    }

    public partial class dcEMP_INFO
    {
        public string EMP_NAME { get; set; }
        public string COMPANY_NAME { get; set; }
    }
}
