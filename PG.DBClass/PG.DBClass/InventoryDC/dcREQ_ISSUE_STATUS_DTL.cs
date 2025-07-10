using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
    [DBTable(Name = "REQ_ISSUE_STATUS_DTL")]
    public partial class dcREQ_ISSUE_STATUS_DTL : DBBaseClass, INotifyPropertyChanged
    {
        #region private members
        private int m_STATUS_DTL_ID = 0;
        private int m_TRANS_ID = 0;
        private string m_TRANS_NO = string.Empty;
        private DateTime? m_TRANS_DATE = null;
        private DateTime? m_TRANS_TIME = null;
        private int m_REQ_ID = 0;
        private string m_REQ_NO = string.Empty;
        private int m_STATUS_ID = 0;
        private int m_FROM_DEPT_ID = 0;
        private int m_TO_DEPT_ID = 0;
        private int m_COMPANY_ID = 0;
        private int m_CREATE_BY = 0;
        private DateTime? m_CREATE_DATE = null;
        private string m_REMARKS = string.Empty;
        private DateTime? m_REQ_TIME = null;
        private DateTime? m_REQ_AUTHO_TIME = null;
        private DateTime? m_REQ_APPROVE_TIME = null;
        private DateTime? m_REQ_ISSUE_TIME = null;
        private DateTime? m_REQ_RECV_TIME = null;
        private string m_IGR_NO = string.Empty;
        private string m_ITC_NO = string.Empty;
        private string m_IRR_NO = string.Empty;
        private int m_IGR_ID = 0;
        private int m_ITC_ID = 0;
        private int m_IRR_ID = 0;

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

        [DBColumn(Name = "STATUS_DTL_ID", Storage = "m_STATUS_DTL_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int STATUS_DTL_ID
        {
            get { return this.m_STATUS_DTL_ID; }
            set
            {
                this.m_STATUS_DTL_ID = value;
                this.NotifyPropertyChanged("STATUS_DTL_ID");
            }
        }

        [DBColumn(Name = "TRANS_ID", Storage = "m_TRANS_ID", DbType = "107")]
        public int TRANS_ID
        {
            get { return this.m_TRANS_ID; }
            set
            {
                this.m_TRANS_ID = value;
                this.NotifyPropertyChanged("TRANS_ID");
            }
        }

        [DBColumn(Name = "TRANS_NO", Storage = "m_TRANS_NO", DbType = "126")]
        public string TRANS_NO
        {
            get { return this.m_TRANS_NO; }
            set
            {
                this.m_TRANS_NO = value;
                this.NotifyPropertyChanged("TRANS_NO");
            }
        }

        [DBColumn(Name = "TRANS_DATE", Storage = "m_TRANS_DATE", DbType = "106")]
        public DateTime? TRANS_DATE
        {
            get { return this.m_TRANS_DATE; }
            set
            {
                this.m_TRANS_DATE = value;
                this.NotifyPropertyChanged("TRANS_DATE");
            }
        }

        [DBColumn(Name = "TRANS_TIME", Storage = "m_TRANS_TIME", DbType = "106")]
        public DateTime? TRANS_TIME
        {
            get { return this.m_TRANS_TIME; }
            set
            {
                this.m_TRANS_TIME = value;
                this.NotifyPropertyChanged("TRANS_TIME");
            }
        }

        [DBColumn(Name = "REQ_ID", Storage = "m_REQ_ID", DbType = "107")]
        public int REQ_ID
        {
            get { return this.m_REQ_ID; }
            set
            {
                this.m_REQ_ID = value;
                this.NotifyPropertyChanged("REQ_ID");
            }
        }

        [DBColumn(Name = "REQ_NO", Storage = "m_REQ_NO", DbType = "126")]
        public string REQ_NO
        {
            get { return this.m_REQ_NO; }
            set
            {
                this.m_REQ_NO = value;
                this.NotifyPropertyChanged("REQ_NO");
            }
        }

        [DBColumn(Name = "STATUS_ID", Storage = "m_STATUS_ID", DbType = "107")]
        public int STATUS_ID
        {
            get { return this.m_STATUS_ID; }
            set
            {
                this.m_STATUS_ID = value;
                this.NotifyPropertyChanged("STATUS_ID");
            }
        }

        [DBColumn(Name = "FROM_DEPT_ID", Storage = "m_FROM_DEPT_ID", DbType = "107")]
        public int FROM_DEPT_ID
        {
            get { return this.m_FROM_DEPT_ID; }
            set
            {
                this.m_FROM_DEPT_ID = value;
                this.NotifyPropertyChanged("FROM_DEPT_ID");
            }
        }

        [DBColumn(Name = "TO_DEPT_ID", Storage = "m_TO_DEPT_ID", DbType = "107")]
        public int TO_DEPT_ID
        {
            get { return this.m_TO_DEPT_ID; }
            set
            {
                this.m_TO_DEPT_ID = value;
                this.NotifyPropertyChanged("TO_DEPT_ID");
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


        [DBColumn(Name = "REQ_TIME", Storage = "m_REQ_TIME", DbType = "106")]
        public DateTime? REQ_TIME
        {
            get { return this.m_REQ_TIME; }
            set
            {
                this.m_REQ_TIME = value;
                this.NotifyPropertyChanged("REQ_TIME");
            }
        }

        [DBColumn(Name = "REQ_AUTHO_TIME", Storage = "m_REQ_AUTHO_TIME", DbType = "106")]
        public DateTime? REQ_AUTHO_TIME
        {
            get { return this.m_REQ_AUTHO_TIME; }
            set
            {
                this.m_REQ_AUTHO_TIME = value;
                this.NotifyPropertyChanged("REQ_AUTHO_TIME");
            }
        }

        [DBColumn(Name = "REQ_APPROVE_TIME", Storage = "m_REQ_APPROVE_TIME", DbType = "106")]
        public DateTime? REQ_APPROVE_TIME
        {
            get { return this.m_REQ_APPROVE_TIME; }
            set
            {
                this.m_REQ_APPROVE_TIME = value;
                this.NotifyPropertyChanged("REQ_APPROVE_TIME");
            }
        }

        [DBColumn(Name = "REQ_ISSUE_TIME", Storage = "m_REQ_ISSUE_TIME", DbType = "106")]
        public DateTime? REQ_ISSUE_TIME
        {
            get { return this.m_REQ_ISSUE_TIME; }
            set
            {
                this.m_REQ_ISSUE_TIME = value;
                this.NotifyPropertyChanged("REQ_ISSUE_TIME");
            }
        }

        [DBColumn(Name = "REQ_RECV_TIME", Storage = "m_REQ_RECV_TIME", DbType = "106")]
        public DateTime? REQ_RECV_TIME
        {
            get { return this.m_REQ_RECV_TIME; }
            set
            {
                this.m_REQ_RECV_TIME = value;
                this.NotifyPropertyChanged("REQ_RECV_TIME");
            }
        }

        [DBColumn(Name = "IGR_NO", Storage = "m_IGR_NO", DbType = "126")]
        public string IGR_NO
        {
            get { return this.m_IGR_NO; }
            set
            {
                this.m_IGR_NO = value;
                this.NotifyPropertyChanged("IGR_NO");
            }
        }

        [DBColumn(Name = "ITC_NO", Storage = "m_ITC_NO", DbType = "126")]
        public string ITC_NO
        {
            get { return this.m_ITC_NO; }
            set
            {
                this.m_ITC_NO = value;
                this.NotifyPropertyChanged("ITC_NO");
            }
        }

        [DBColumn(Name = "IRR_NO", Storage = "m_IRR_NO", DbType = "126")]
        public string IRR_NO
        {
            get { return this.m_IRR_NO; }
            set
            {
                this.m_IRR_NO = value;
                this.NotifyPropertyChanged("IRR_NO");
            }
        }

        [DBColumn(Name = "IGR_ID", Storage = "m_IGR_ID", DbType = "107")]
        public int IGR_ID
        {
            get { return this.m_IGR_ID; }
            set
            {
                this.m_IGR_ID = value;
                this.NotifyPropertyChanged("IGR_ID");
            }
        }

        [DBColumn(Name = "ITC_ID", Storage = "m_ITC_ID", DbType = "107")]
        public int ITC_ID
        {
            get { return this.m_ITC_ID; }
            set
            {
                this.m_ITC_ID = value;
                this.NotifyPropertyChanged("ITC_ID");
            }
        }

        [DBColumn(Name = "IRR_ID", Storage = "m_IRR_ID", DbType = "107")]
        public int IRR_ID
        {
            get { return this.m_IRR_ID; }
            set
            {
                this.m_IRR_ID = value;
                this.NotifyPropertyChanged("IRR_ID");
            }
        }

        #endregion //properties
    }
}
