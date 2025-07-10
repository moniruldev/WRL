using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.WRELDC
{
    [DBTable(Name = "EMPLOYEE_MST")]
    public partial class dcEMPLOYEE_MST : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_EMP_CODE = 0;
        private string m_EMP_ID = string.Empty;
        private string m_EMP_NAME = string.Empty;
        private string m_NICK_NAME = string.Empty;
        private string m_FATHER = string.Empty;
        private string m_MOTHER = string.Empty;
        private string m_MOBILE_NO = string.Empty;
        private string m_MOBILE_NO_PERSONAL = string.Empty;
        private string m_DEPARTMENT = string.Empty;
        private decimal m_DEPT_ID = 0;
        private DateTime? m_JOINING_DATE = null;
        private string m_DESIGNATION = string.Empty;
        private string m_CURRENT_ADDRESS = string.Empty;
        private string m_PERMANENT_ADDRESS = string.Empty;
        private string m_IS_ACTIVE = string.Empty;
        private string m_CREATE_BY = string.Empty;
        private DateTime? m_CREATE_DATE = null;
        private string m_EDIT_BY = string.Empty;
        private DateTime? m_EDIT_DATE = null;

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


        [DBColumn(Name = "EMP_CODE", Storage = "m_EMP_CODE", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int EMP_CODE
        {
            get { return this.m_EMP_CODE; }
            set
            {
                this.m_EMP_CODE = value;
                this.NotifyPropertyChanged("EMP_CODE");
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

        [DBColumn(Name = "EMP_NAME", Storage = "m_EMP_NAME", DbType = "126")]
        public string EMP_NAME
        {
            get { return this.m_EMP_NAME; }
            set
            {
                this.m_EMP_NAME = value;
                this.NotifyPropertyChanged("EMP_NAME");
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

        [DBColumn(Name = "FATHER", Storage = "m_FATHER", DbType = "126")]
        public string FATHER
        {
            get { return this.m_FATHER; }
            set
            {
                this.m_FATHER = value;
                this.NotifyPropertyChanged("FATHER");
            }
        }

        [DBColumn(Name = "MOTHER", Storage = "m_MOTHER", DbType = "126")]
        public string MOTHER
        {
            get { return this.m_MOTHER; }
            set
            {
                this.m_MOTHER = value;
                this.NotifyPropertyChanged("MOTHER");
            }
        }

        [DBColumn(Name = "MOBILE_NO", Storage = "m_MOBILE_NO", DbType = "126")]
        public string MOBILE_NO
        {
            get { return this.m_MOBILE_NO; }
            set
            {
                this.m_MOBILE_NO = value;
                this.NotifyPropertyChanged("MOBILE_NO");
            }
        }

        [DBColumn(Name = "MOBILE_NO_PERSONAL", Storage = "m_MOBILE_NO_PERSONAL", DbType = "126")]
        public string MOBILE_NO_PERSONAL
        {
            get { return this.m_MOBILE_NO_PERSONAL; }
            set
            {
                this.m_MOBILE_NO_PERSONAL = value;
                this.NotifyPropertyChanged("MOBILE_NO_PERSONAL");
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

        [DBColumn(Name = "DEPT_ID", Storage = "m_DEPT_ID", DbType = "107")]
        public decimal DEPT_ID
        {
            get { return this.m_DEPT_ID; }
            set
            {
                this.m_DEPT_ID = value;
                this.NotifyPropertyChanged("DEPT_ID");
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

        [DBColumn(Name = "CURRENT_ADDRESS", Storage = "m_CURRENT_ADDRESS", DbType = "126")]
        public string CURRENT_ADDRESS
        {
            get { return this.m_CURRENT_ADDRESS; }
            set
            {
                this.m_CURRENT_ADDRESS = value;
                this.NotifyPropertyChanged("CURRENT_ADDRESS");
            }
        }

        [DBColumn(Name = "PERMANENT_ADDRESS", Storage = "m_PERMANENT_ADDRESS", DbType = "126")]
        public string PERMANENT_ADDRESS
        {
            get { return this.m_PERMANENT_ADDRESS; }
            set
            {
                this.m_PERMANENT_ADDRESS = value;
                this.NotifyPropertyChanged("PERMANENT_ADDRESS");
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

        [DBColumn(Name = "EDIT_BY", Storage = "m_EDIT_BY", DbType = "126")]
        public string EDIT_BY
        {
            get { return this.m_EDIT_BY; }
            set
            {
                this.m_EDIT_BY = value;
                this.NotifyPropertyChanged("EDIT_BY");
            }
        }

        [DBColumn(Name = "EDIT_DATE", Storage = "m_EDIT_DATE", DbType = "106")]
        public DateTime? EDIT_DATE
        {
            get { return this.m_EDIT_DATE; }
            set
            {
                this.m_EDIT_DATE = value;
                this.NotifyPropertyChanged("EDIT_DATE");
            }
        }

        #endregion //properties
    }
}
