using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.WRELDC
{
    [DBTable(Name = "AGENT_MST")]
    public partial class dcAGENT_MST : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_AGENT_ID = 0;
        private string m_AGENT_COMPANY_NAME = string.Empty;
        private string m_OWNER_NAME = string.Empty;
        private string m_OWNER_MOBILE_NO = string.Empty;
        private string m_CONTACT_PERSON = string.Empty;
        private string m_CONTACT_MOBILE_NO = string.Empty;
        private string m_BANK_NAME = string.Empty;
        private string m_BRANCH = string.Empty;
        private string m_ACCOUNT_NO = string.Empty;
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


        [DBColumn(Name = "AGENT_ID", Storage = "m_AGENT_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int AGENT_ID
        {
            get { return this.m_AGENT_ID; }
            set
            {
                this.m_AGENT_ID = value;
                this.NotifyPropertyChanged("AGENT_ID");
            }
        }

        [DBColumn(Name = "AGENT_COMPANY_NAME", Storage = "m_AGENT_COMPANY_NAME", DbType = "126")]
        public string AGENT_COMPANY_NAME
        {
            get { return this.m_AGENT_COMPANY_NAME; }
            set
            {
                this.m_AGENT_COMPANY_NAME = value;
                this.NotifyPropertyChanged("AGENT_COMPANY_NAME");
            }
        }

        [DBColumn(Name = "OWNER_NAME", Storage = "m_OWNER_NAME", DbType = "126")]
        public string OWNER_NAME
        {
            get { return this.m_OWNER_NAME; }
            set
            {
                this.m_OWNER_NAME = value;
                this.NotifyPropertyChanged("OWNER_NAME");
            }
        }

        [DBColumn(Name = "OWNER_MOBILE_NO", Storage = "m_OWNER_MOBILE_NO", DbType = "126")]
        public string OWNER_MOBILE_NO
        {
            get { return this.m_OWNER_MOBILE_NO; }
            set
            {
                this.m_OWNER_MOBILE_NO = value;
                this.NotifyPropertyChanged("OWNER_MOBILE_NO");
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

        [DBColumn(Name = "CONTACT_MOBILE_NO", Storage = "m_CONTACT_MOBILE_NO", DbType = "126")]
        public string CONTACT_MOBILE_NO
        {
            get { return this.m_CONTACT_MOBILE_NO; }
            set
            {
                this.m_CONTACT_MOBILE_NO = value;
                this.NotifyPropertyChanged("CONTACT_MOBILE_NO");
            }
        }

        [DBColumn(Name = "BANK_NAME", Storage = "m_BANK_NAME", DbType = "126")]
        public string BANK_NAME
        {
            get { return this.m_BANK_NAME; }
            set
            {
                this.m_BANK_NAME = value;
                this.NotifyPropertyChanged("BANK_NAME");
            }
        }

        [DBColumn(Name = "BRANCH", Storage = "m_BRANCH", DbType = "126")]
        public string BRANCH
        {
            get { return this.m_BRANCH; }
            set
            {
                this.m_BRANCH = value;
                this.NotifyPropertyChanged("BRANCH");
            }
        }

        [DBColumn(Name = "ACCOUNT_NO", Storage = "m_ACCOUNT_NO", DbType = "126")]
        public string ACCOUNT_NO
        {
            get { return this.m_ACCOUNT_NO; }
            set
            {
                this.m_ACCOUNT_NO = value;
                this.NotifyPropertyChanged("ACCOUNT_NO");
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
