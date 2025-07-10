using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.InventoryDC
{
    [DBTable(Name = "INDT_STATUS_DTL")]
    public partial class dcINDT_STATUS_DTL : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_STATUS_DTL_ID = 0;
        private int m_TRANS_ID = 0;
        private string m_TRANS_NO = string.Empty;
        private DateTime? m_TRANS_DATE = null;
        private DateTime? m_TRANS_TIME = null;
        private string m_INDT_NO = string.Empty;
        private int m_STATUS_ID = 0;
        private int m_FROM_DEPT = 0;
        private int m_TO_DEPT = 0;
        private int m_COMPANY_ID = 0;
        private int m_INDT_ID = 0;
        private DateTime? m_INDT_TIME = null;
        private DateTime? m_INDT_AUTHO_TIME = null;
        private DateTime? m_INDT_APPR_TIME = null;
        private int m_PURCHASE_ID = 0;
        private DateTime? m_PURCHASE_TIME = null;
        private DateTime? m_PURCHASE_AUTH_TIME = null;
        private int m_MRR_ID = 0;
        private DateTime? m_MRR_TIME = null;
        private DateTime? m_MRR_AUTH_TIME = null;
        private int m_CREATE_BY = 0;
        private DateTime? m_CREATE_DATE = null;
        private string m_REMARKS = string.Empty;
        private int m_INDT_TYPE_ID = 0;

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

        [DBColumn(Name = "FROM_DEPT", Storage = "m_FROM_DEPT", DbType = "107")]
        public int FROM_DEPT
        {
            get { return this.m_FROM_DEPT; }
            set
            {
                this.m_FROM_DEPT = value;
                this.NotifyPropertyChanged("FROM_DEPT");
            }
        }

        [DBColumn(Name = "TO_DEPT", Storage = "m_TO_DEPT", DbType = "107")]
        public int TO_DEPT
        {
            get { return this.m_TO_DEPT; }
            set
            {
                this.m_TO_DEPT = value;
                this.NotifyPropertyChanged("TO_DEPT");
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

        [DBColumn(Name = "INDT_ID", Storage = "m_INDT_ID", DbType = "107")]
        public int INDT_ID
        {
            get { return this.m_INDT_ID; }
            set
            {
                this.m_INDT_ID = value;
                this.NotifyPropertyChanged("INDT_ID");
            }
        }

        [DBColumn(Name = "INDT_TIME", Storage = "m_INDT_TIME", DbType = "106")]
        public DateTime? INDT_TIME
        {
            get { return this.m_INDT_TIME; }
            set
            {
                this.m_INDT_TIME = value;
                this.NotifyPropertyChanged("INDT_TIME");
            }
        }

        [DBColumn(Name = "INDT_AUTHO_TIME", Storage = "m_INDT_AUTHO_TIME", DbType = "106")]
        public DateTime? INDT_AUTHO_TIME
        {
            get { return this.m_INDT_AUTHO_TIME; }
            set
            {
                this.m_INDT_AUTHO_TIME = value;
                this.NotifyPropertyChanged("INDT_AUTHO_TIME");
            }
        }

        [DBColumn(Name = "INDT_APPR_TIME", Storage = "m_INDT_APPR_TIME", DbType = "106")]
        public DateTime? INDT_APPR_TIME
        {
            get { return this.m_INDT_APPR_TIME; }
            set
            {
                this.m_INDT_APPR_TIME = value;
                this.NotifyPropertyChanged("INDT_APPR_TIME");
            }
        }

        [DBColumn(Name = "PURCHASE_ID", Storage = "m_PURCHASE_ID", DbType = "107")]
        public int PURCHASE_ID
        {
            get { return this.m_PURCHASE_ID; }
            set
            {
                this.m_PURCHASE_ID = value;
                this.NotifyPropertyChanged("PURCHASE_ID");
            }
        }

        [DBColumn(Name = "PURCHASE_TIME", Storage = "m_PURCHASE_TIME", DbType = "106")]
        public DateTime? PURCHASE_TIME
        {
            get { return this.m_PURCHASE_TIME; }
            set
            {
                this.m_PURCHASE_TIME = value;
                this.NotifyPropertyChanged("PURCHASE_TIME");
            }
        }

        [DBColumn(Name = "PURCHASE_AUTH_TIME", Storage = "m_PURCHASE_AUTH_TIME", DbType = "106")]
        public DateTime? PURCHASE_AUTH_TIME
        {
            get { return this.m_PURCHASE_AUTH_TIME; }
            set
            {
                this.m_PURCHASE_AUTH_TIME = value;
                this.NotifyPropertyChanged("PURCHASE_AUTH_TIME");
            }
        }

        [DBColumn(Name = "MRR_ID", Storage = "m_MRR_ID", DbType = "107")]
        public int MRR_ID
        {
            get { return this.m_MRR_ID; }
            set
            {
                this.m_MRR_ID = value;
                this.NotifyPropertyChanged("MRR_ID");
            }
        }

        [DBColumn(Name = "MRR_TIME", Storage = "m_MRR_TIME", DbType = "106")]
        public DateTime? MRR_TIME
        {
            get { return this.m_MRR_TIME; }
            set
            {
                this.m_MRR_TIME = value;
                this.NotifyPropertyChanged("MRR_TIME");
            }
        }

        [DBColumn(Name = "MRR_AUTH_TIME", Storage = "m_MRR_AUTH_TIME", DbType = "106")]
        public DateTime? MRR_AUTH_TIME
        {
            get { return this.m_MRR_AUTH_TIME; }
            set
            {
                this.m_MRR_AUTH_TIME = value;
                this.NotifyPropertyChanged("MRR_AUTH_TIME");
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

        #endregion //properties
    }
}
