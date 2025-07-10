using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "PROD_REJECTION_ISSUE_MST")]
    public partial class dcPROD_REJECTION_ISSUE_MST : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_REJ_ISSUE_ID = 0;
        private string m_REJ_ISSUE_NO = string.Empty;
        private DateTime? m_REJ_ISSUE_DATE = null;
        private DateTime? m_REJ_ISSUE_TIME = null;
        private int m_STORE_ID = 0;
        private int m_REJ_ISSUE_COMAPNY_ID = 0;
        private int m_FROM_DEPARTMENT_ID = 0;
        private int m_TO_DEPARTMENT_ID = 0;
        private string m_REJ_ISSUE_REMARKS = string.Empty;
        private int m_CREATE_BY = 0;
        private DateTime? m_CREATE_DATE = null;
        private int m_UPDATE_BY = 0;
        private DateTime? m_UPDATE_DATE = null;
        private string m_AUTH_STATUS = string.Empty;
        private int m_AUTH_BY = 0;
        private DateTime? m_AUTH_DATE = null;
        private string m_IS_CANCEL = string.Empty;
        private int m_CANCEL_BY = 0;
        private DateTime? m_CANCEL_DATE = null;
        private string m_IS_REQ = string.Empty;
        private int m_DEPARTMENT_ID = 0;
        private decimal m_TRANSFER_TYPE_ID = 0;
        private string m_IS_PRODUCTION = string.Empty;
        private string m_IS_RETURN = string.Empty;
        private string m_IS_STORE_TRANS = string.Empty;
        private int m_STLM_ID = 0;

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


        [DBColumn(Name = "REJ_ISSUE_ID", Storage = "m_REJ_ISSUE_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int REJ_ISSUE_ID
        {
            get { return this.m_REJ_ISSUE_ID; }
            set
            {
                this.m_REJ_ISSUE_ID = value;
                this.NotifyPropertyChanged("REJ_ISSUE_ID");
            }
        }

        [DBColumn(Name = "REJ_ISSUE_NO", Storage = "m_REJ_ISSUE_NO", DbType = "126")]
        public string REJ_ISSUE_NO
        {
            get { return this.m_REJ_ISSUE_NO; }
            set
            {
                this.m_REJ_ISSUE_NO = value;
                this.NotifyPropertyChanged("REJ_ISSUE_NO");
            }
        }

        [DBColumn(Name = "REJ_ISSUE_DATE", Storage = "m_REJ_ISSUE_DATE", DbType = "106")]
        public DateTime? REJ_ISSUE_DATE
        {
            get { return this.m_REJ_ISSUE_DATE; }
            set
            {
                this.m_REJ_ISSUE_DATE = value;
                this.NotifyPropertyChanged("REJ_ISSUE_DATE");
            }
        }

        [DBColumn(Name = "REJ_ISSUE_TIME", Storage = "m_REJ_ISSUE_TIME", DbType = "106")]
        public DateTime? REJ_ISSUE_TIME
        {
            get { return this.m_REJ_ISSUE_TIME; }
            set
            {
                this.m_REJ_ISSUE_TIME = value;
                this.NotifyPropertyChanged("REJ_ISSUE_TIME");
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

        [DBColumn(Name = "REJ_ISSUE_COMAPNY_ID", Storage = "m_REJ_ISSUE_COMAPNY_ID", DbType = "107")]
        public int REJ_ISSUE_COMAPNY_ID
        {
            get { return this.m_REJ_ISSUE_COMAPNY_ID; }
            set
            {
                this.m_REJ_ISSUE_COMAPNY_ID = value;
                this.NotifyPropertyChanged("REJ_ISSUE_COMAPNY_ID");
            }
        }

        [DBColumn(Name = "FROM_DEPARTMENT_ID", Storage = "m_FROM_DEPARTMENT_ID", DbType = "107")]
        public int FROM_DEPARTMENT_ID
        {
            get { return this.m_FROM_DEPARTMENT_ID; }
            set
            {
                this.m_FROM_DEPARTMENT_ID = value;
                this.NotifyPropertyChanged("FROM_DEPARTMENT_ID");
            }
        }

        [DBColumn(Name = "TO_DEPARTMENT_ID", Storage = "m_TO_DEPARTMENT_ID", DbType = "107")]
        public int TO_DEPARTMENT_ID
        {
            get { return this.m_TO_DEPARTMENT_ID; }
            set
            {
                this.m_TO_DEPARTMENT_ID = value;
                this.NotifyPropertyChanged("TO_DEPARTMENT_ID");
            }
        }

        [DBColumn(Name = "REJ_ISSUE_REMARKS", Storage = "m_REJ_ISSUE_REMARKS", DbType = "126")]
        public string REJ_ISSUE_REMARKS
        {
            get { return this.m_REJ_ISSUE_REMARKS; }
            set
            {
                this.m_REJ_ISSUE_REMARKS = value;
                this.NotifyPropertyChanged("REJ_ISSUE_REMARKS");
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

        [DBColumn(Name = "UPDATE_BY", Storage = "m_UPDATE_BY", DbType = "107")]
        public int UPDATE_BY
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

        [DBColumn(Name = "IS_REQ", Storage = "m_IS_REQ", DbType = "126")]
        public string IS_REQ
        {
            get { return this.m_IS_REQ; }
            set
            {
                this.m_IS_REQ = value;
                this.NotifyPropertyChanged("IS_REQ");
            }
        }

        [DBColumn(Name = "DEPARTMENT_ID", Storage = "m_DEPARTMENT_ID", DbType = "107")]
        public int DEPARTMENT_ID
        {
            get { return this.m_DEPARTMENT_ID; }
            set
            {
                this.m_DEPARTMENT_ID = value;
                this.NotifyPropertyChanged("DEPARTMENT_ID");
            }
        }

        [DBColumn(Name = "TRANSFER_TYPE_ID", Storage = "m_TRANSFER_TYPE_ID", DbType = "107")]
        public decimal TRANSFER_TYPE_ID
        {
            get { return this.m_TRANSFER_TYPE_ID; }
            set
            {
                this.m_TRANSFER_TYPE_ID = value;
                this.NotifyPropertyChanged("TRANSFER_TYPE_ID");
            }
        }

        [DBColumn(Name = "IS_PRODUCTION", Storage = "m_IS_PRODUCTION", DbType = "126")]
        public string IS_PRODUCTION
        {
            get { return this.m_IS_PRODUCTION; }
            set
            {
                this.m_IS_PRODUCTION = value;
                this.NotifyPropertyChanged("IS_PRODUCTION");
            }
        }

        [DBColumn(Name = "IS_RETURN", Storage = "m_IS_RETURN", DbType = "126")]
        public string IS_RETURN
        {
            get { return this.m_IS_RETURN; }
            set
            {
                this.m_IS_RETURN = value;
                this.NotifyPropertyChanged("IS_RETURN");
            }
        }

        [DBColumn(Name = "IS_STORE_TRANS", Storage = "m_IS_STORE_TRANS", DbType = "126")]
        public string IS_STORE_TRANS
        {
            get { return this.m_IS_STORE_TRANS; }
            set
            {
                this.m_IS_STORE_TRANS = value;
                this.NotifyPropertyChanged("IS_STORE_TRANS");
            }
        }

        [DBColumn(Name = "STLM_ID", Storage = "m_STLM_ID", DbType = "107")]
        public int STLM_ID
        {
            get { return this.m_STLM_ID; }
            set
            {
                this.m_STLM_ID = value;
                this.NotifyPropertyChanged("STLM_ID");
            }
        }

        #endregion //properties

       
    }

    public partial class dcPROD_REJECTION_ISSUE_MST
    {
        public List<dcPROD_REJECTION_ISSUE_DTL> RejIssueDtl = new List<dcPROD_REJECTION_ISSUE_DTL>();
        public string CREATE_BY_NAME { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        public string STLM_NAME { get; set; }
        public bool IS_RCV_COMPLETE { get; set; }
        public string FROM_DEPARTMENT_NAME { get; set; }
        public string TO_DEPARTMENT_NAME { get; set; }
    }
}
