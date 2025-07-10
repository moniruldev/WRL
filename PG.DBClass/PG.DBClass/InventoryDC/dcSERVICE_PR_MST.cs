using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.InventoryDC
{
    [DBTable(Name = "SERVICE_PR_MST")]
    public partial class dcSERVICE_PR_MST : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_INDT_ID = 0;
        private string m_INDT_NO = string.Empty;
        private DateTime? m_INDT_DATE = null;
        private int m_COMP_ID = 0;
        private int m_DEPT_ID = 0;
        private int m_BRANCH_ID = 0;
        private int m_STORE_ID = 0;
        private int m_INDENT_BY = 0;
        private string m_AUTH_STATUS = string.Empty;
        private int m_AUTH_BY = 0;
        private DateTime? m_AUTH_DATE = null;
        private string m_IS_APPROVED = string.Empty;
        private int m_APPROVED_BY = 0;
        private DateTime? m_APPROVED_DATE = null;
        private string m_IS_CANCEL = string.Empty;
        private int m_CANCEL_BY = 0;
        private DateTime? m_CANCEL_DATE = null;
        private string m_INDT_REMARKS = string.Empty;
        private DateTime? m_REQUIRED_DATE = null;
        private int m_CREATE_BY = 0;
        private DateTime? m_CREATE_DATE = null;
        private string m_IS_CLOSED = string.Empty;
        private string m_CLOSED_BY = string.Empty;
        private DateTime? m_CLOSED_DATE = null;
        private int m_INDT_TYPE_ID = 0;
        private int m_INDT_STATUS_ID = 0;
        private int m_CHECKED_BY = 0;
        private string m_CHECKED_BY_STATUS = string.Empty;
        private DateTime? m_CHECKED_BY_DATE = null;
        private string m_RECEIVED_STATUS = string.Empty;
        private DateTime? m_RECEIVED_DATE = null;
        private int m_RECEIVED_BY = 0;

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


        [DBColumn(Name = "INDT_ID", Storage = "m_INDT_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int INDT_ID
        {
            get { return this.m_INDT_ID; }
            set
            {
                this.m_INDT_ID = value;
                this.NotifyPropertyChanged("INDT_ID");
            }
        }

        [DBColumn(Name = "INDT_NO", Storage = "m_INDT_NO", DbType = "126")]
        public string INDT_NO
        {
            get { return this.m_INDT_NO; }
            set
            {
                this.m_INDT_NO = value;
                this.NotifyPropertyChanged("INDT_NO");
            }
        }

        [DBColumn(Name = "INDT_DATE", Storage = "m_INDT_DATE", DbType = "106")]
        public DateTime? INDT_DATE
        {
            get { return this.m_INDT_DATE; }
            set
            {
                this.m_INDT_DATE = value;
                this.NotifyPropertyChanged("INDT_DATE");
            }
        }

        [DBColumn(Name = "COMP_ID", Storage = "m_COMP_ID", DbType = "107")]
        public int COMP_ID
        {
            get { return this.m_COMP_ID; }
            set
            {
                this.m_COMP_ID = value;
                this.NotifyPropertyChanged("COMP_ID");
            }
        }

        [DBColumn(Name = "DEPT_ID", Storage = "m_DEPT_ID", DbType = "107")]
        public int DEPT_ID
        {
            get { return this.m_DEPT_ID; }
            set
            {
                this.m_DEPT_ID = value;
                this.NotifyPropertyChanged("DEPT_ID");
            }
        }

        [DBColumn(Name = "BRANCH_ID", Storage = "m_BRANCH_ID", DbType = "107")]
        public int BRANCH_ID
        {
            get { return this.m_BRANCH_ID; }
            set
            {
                this.m_BRANCH_ID = value;
                this.NotifyPropertyChanged("BRANCH_ID");
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

        [DBColumn(Name = "INDENT_BY", Storage = "m_INDENT_BY", DbType = "107")]
        public int INDENT_BY
        {
            get { return this.m_INDENT_BY; }
            set
            {
                this.m_INDENT_BY = value;
                this.NotifyPropertyChanged("INDENT_BY");
            }
        }

        [DBColumn(Name = "AUTH_STATUS", Storage = "m_AUTH_STATUS", DbType = "126")]
        public string AUTH_STATUS
        {
            get { return this.m_AUTH_STATUS; }
            set
            {
                this.m_AUTH_STATUS = value;
                this.NotifyPropertyChanged("AUTH_STATUS");
            }
        }

        [DBColumn(Name = "AUTH_BY", Storage = "m_AUTH_BY", DbType = "107")]
        public int AUTH_BY
        {
            get { return this.m_AUTH_BY; }
            set
            {
                this.m_AUTH_BY = value;
                this.NotifyPropertyChanged("AUTH_BY");
            }
        }

        [DBColumn(Name = "AUTH_DATE", Storage = "m_AUTH_DATE", DbType = "106")]
        public DateTime? AUTH_DATE
        {
            get { return this.m_AUTH_DATE; }
            set
            {
                this.m_AUTH_DATE = value;
                this.NotifyPropertyChanged("AUTH_DATE");
            }
        }

        [DBColumn(Name = "IS_APPROVED", Storage = "m_IS_APPROVED", DbType = "126")]
        public string IS_APPROVED
        {
            get { return this.m_IS_APPROVED; }
            set
            {
                this.m_IS_APPROVED = value;
                this.NotifyPropertyChanged("IS_APPROVED");
            }
        }

        [DBColumn(Name = "APPROVED_BY", Storage = "m_APPROVED_BY", DbType = "107")]
        public int APPROVED_BY
        {
            get { return this.m_APPROVED_BY; }
            set
            {
                this.m_APPROVED_BY = value;
                this.NotifyPropertyChanged("APPROVED_BY");
            }
        }

        [DBColumn(Name = "APPROVED_DATE", Storage = "m_APPROVED_DATE", DbType = "106")]
        public DateTime? APPROVED_DATE
        {
            get { return this.m_APPROVED_DATE; }
            set
            {
                this.m_APPROVED_DATE = value;
                this.NotifyPropertyChanged("APPROVED_DATE");
            }
        }

        [DBColumn(Name = "IS_CANCEL", Storage = "m_IS_CANCEL", DbType = "126")]
        public string IS_CANCEL
        {
            get { return this.m_IS_CANCEL; }
            set
            {
                this.m_IS_CANCEL = value;
                this.NotifyPropertyChanged("IS_CANCEL");
            }
        }

        [DBColumn(Name = "CANCEL_BY", Storage = "m_CANCEL_BY", DbType = "107")]
        public int CANCEL_BY
        {
            get { return this.m_CANCEL_BY; }
            set
            {
                this.m_CANCEL_BY = value;
                this.NotifyPropertyChanged("CANCEL_BY");
            }
        }

        [DBColumn(Name = "CANCEL_DATE", Storage = "m_CANCEL_DATE", DbType = "106")]
        public DateTime? CANCEL_DATE
        {
            get { return this.m_CANCEL_DATE; }
            set
            {
                this.m_CANCEL_DATE = value;
                this.NotifyPropertyChanged("CANCEL_DATE");
            }
        }

        [DBColumn(Name = "INDT_REMARKS", Storage = "m_INDT_REMARKS", DbType = "126")]
        public string INDT_REMARKS
        {
            get { return this.m_INDT_REMARKS; }
            set
            {
                this.m_INDT_REMARKS = value;
                this.NotifyPropertyChanged("INDT_REMARKS");
            }
        }

        [DBColumn(Name = "REQUIRED_DATE", Storage = "m_REQUIRED_DATE", DbType = "106")]
        public DateTime? REQUIRED_DATE
        {
            get { return this.m_REQUIRED_DATE; }
            set
            {
                this.m_REQUIRED_DATE = value;
                this.NotifyPropertyChanged("REQUIRED_DATE");
            }
        }

        [DBColumn(Name = "CREATE_BY", Storage = "m_CREATE_BY", DbType = "107")]
        public int CREATE_BY
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

        [DBColumn(Name = "IS_CLOSED", Storage = "m_IS_CLOSED", DbType = "126")]
        public string IS_CLOSED
        {
            get { return this.m_IS_CLOSED; }
            set
            {
                this.m_IS_CLOSED = value;
                this.NotifyPropertyChanged("IS_CLOSED");
            }
        }

        [DBColumn(Name = "CLOSED_BY", Storage = "m_CLOSED_BY", DbType = "126")]
        public string CLOSED_BY
        {
            get { return this.m_CLOSED_BY; }
            set
            {
                this.m_CLOSED_BY = value;
                this.NotifyPropertyChanged("CLOSED_BY");
            }
        }

        [DBColumn(Name = "CLOSED_DATE", Storage = "m_CLOSED_DATE", DbType = "106")]
        public DateTime? CLOSED_DATE
        {
            get { return this.m_CLOSED_DATE; }
            set
            {
                this.m_CLOSED_DATE = value;
                this.NotifyPropertyChanged("CLOSED_DATE");
            }
        }

        [DBColumn(Name = "INDT_TYPE_ID", Storage = "m_INDT_TYPE_ID", DbType = "107")]
        public int INDT_TYPE_ID
        {
            get { return this.m_INDT_TYPE_ID; }
            set
            {
                this.m_INDT_TYPE_ID = value;
                this.NotifyPropertyChanged("INDT_TYPE_ID");
            }
        }

        [DBColumn(Name = "INDT_STATUS_ID", Storage = "m_INDT_STATUS_ID", DbType = "107")]
        public int INDT_STATUS_ID
        {
            get { return this.m_INDT_STATUS_ID; }
            set
            {
                this.m_INDT_STATUS_ID = value;
                this.NotifyPropertyChanged("INDT_STATUS_ID");
            }
        }

        [DBColumn(Name = "CHECKED_BY", Storage = "m_CHECKED_BY", DbType = "107")]
        public int CHECKED_BY
        {
            get { return this.m_CHECKED_BY; }
            set
            {
                this.m_CHECKED_BY = value;
                this.NotifyPropertyChanged("CHECKED_BY");
            }
        }

        [DBColumn(Name = "CHECKED_BY_STATUS", Storage = "m_CHECKED_BY_STATUS", DbType = "126")]
        public string CHECKED_BY_STATUS
        {
            get { return this.m_CHECKED_BY_STATUS; }
            set
            {
                this.m_CHECKED_BY_STATUS = value;
                this.NotifyPropertyChanged("CHECKED_BY_STATUS");
            }
        }

        [DBColumn(Name = "CHECKED_BY_DATE", Storage = "m_CHECKED_BY_DATE", DbType = "106")]
        public DateTime? CHECKED_BY_DATE
        {
            get { return this.m_CHECKED_BY_DATE; }
            set
            {
                this.m_CHECKED_BY_DATE = value;
                this.NotifyPropertyChanged("CHECKED_BY_DATE");
            }
        }

        [DBColumn(Name = "RECEIVED_STATUS", Storage = "m_RECEIVED_STATUS", DbType = "126")]
        public string RECEIVED_STATUS
        {
            get { return this.m_RECEIVED_STATUS; }
            set
            {
                this.m_RECEIVED_STATUS = value;
                this.NotifyPropertyChanged("RECEIVED_STATUS");
            }
        }

        [DBColumn(Name = "RECEIVED_DATE", Storage = "m_RECEIVED_DATE", DbType = "106")]
        public DateTime? RECEIVED_DATE
        {
            get { return this.m_RECEIVED_DATE; }
            set
            {
                this.m_RECEIVED_DATE = value;
                this.NotifyPropertyChanged("RECEIVED_DATE");
            }
        }

        [DBColumn(Name = "RECEIVED_BY", Storage = "m_RECEIVED_BY", DbType = "107")]
        public int RECEIVED_BY
        {
            get { return this.m_RECEIVED_BY; }
            set
            {
                this.m_RECEIVED_BY = value;
                this.NotifyPropertyChanged("RECEIVED_BY");
            }
        }

        #endregion //properties
    }

    public partial class dcSERVICE_PR_MST
    {
        public string DEPARTMENT_NAME { get; set; }
        public bool IsPurComplete { get; set; }
    }

    
}
