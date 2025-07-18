using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.WRELDC
{
    [DBTable(Name = "DEPARTMENT_MST")]
    public partial class dcDEPARTMENT_MST : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_DEPT_ID = 0;
        private string m_DEPT_NAME = string.Empty;
        private decimal m_CLIENT_ID = 0;
        private string m_CONTACT_PERSON = string.Empty;
        private string m_CONTACT_NO = string.Empty;
        private string m_REMARKS = string.Empty;
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


        [DBColumn(Name = "DEPT_ID", Storage = "m_DEPT_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int DEPT_ID
        {
            get { return this.m_DEPT_ID; }
            set
            {
                this.m_DEPT_ID = value;
                this.NotifyPropertyChanged("DEPT_ID");
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

        [DBColumn(Name = "CLIENT_ID", Storage = "m_CLIENT_ID", DbType = "107")]
        public decimal CLIENT_ID
        {
            get { return this.m_CLIENT_ID; }
            set
            {
                this.m_CLIENT_ID = value;
                this.NotifyPropertyChanged("CLIENT_ID");
            }
        }

        [DBColumn(Name = "CONTACT_PERSON", Storage = "m_CONTACT_PERSON", DbType = "126")]
        public string CONTACT_PERSON
        {
            get { return this.m_CONTACT_PERSON; }
            set
            {
                this.m_CONTACT_PERSON = value;
                this.NotifyPropertyChanged("CONTACT_PERSON");
            }
        }

        [DBColumn(Name = "CONTACT_NO", Storage = "m_CONTACT_NO", DbType = "126")]
        public string CONTACT_NO
        {
            get { return this.m_CONTACT_NO; }
            set
            {
                this.m_CONTACT_NO = value;
                this.NotifyPropertyChanged("CONTACT_NO");
            }
        }

        [DBColumn(Name = "REMARKS", Storage = "m_REMARKS", DbType = "126")]
        public string REMARKS
        {
            get { return this.m_REMARKS; }
            set
            {
                this.m_REMARKS = value;
                this.NotifyPropertyChanged("REMARKS");
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

    public partial class dcDEPARTMENT_MST
    {
        public string CLIENT_NAME { get; set; }
    }
}
