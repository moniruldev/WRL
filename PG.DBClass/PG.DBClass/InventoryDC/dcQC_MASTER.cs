using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.InventoryDC
{
    [DBTable(Name = "QC_MASTER")]
    public partial class dcQC_MASTER : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_QC_ID = 0;
        private string m_QC_NO = string.Empty;
        private DateTime? m_QC_DATE = null;
        private int m_STORE_ID = 0;
        private int m_QC_TYPE_ID = 0;
        private int m_TRANS_ID = 0;
        private string m_QC_NOTE = string.Empty;
        private string m_AUTH_STATUS = string.Empty;
        private int m_AUTH_BY = 0;
        private DateTime? m_AUTH_DATE = null;
        private string m_IS_APPROVED = string.Empty;
        private int m_APPROVED_BY = 0;
        private DateTime? m_APPROVE_DATE = null;
        private int m_CREATE_BY = 0;
        private DateTime? m_CREATE_DATE = null;
        private int m_SUP_ID = 0;
        private DateTime? m_QC_TIME = null;
        private int m_COMPANY_ID = 0;
        private string m_QC_PASS_STATUS = string.Empty;
        private int m_QC_PASSED_BY = 0;
        private DateTime? m_QC_PASS_TIME = null;

        

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


        [DBColumn(Name = "QC_ID", Storage = "m_QC_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int QC_ID
        {
            get { return this.m_QC_ID; }
            set
            {
                this.m_QC_ID = value;
                this.NotifyPropertyChanged("QC_ID");
            }
        }

        [DBColumn(Name = "QC_NO", Storage = "m_QC_NO", DbType = "126")]
        public string QC_NO
        {
            get { return this.m_QC_NO; }
            set
            {
                this.m_QC_NO = value;
                this.NotifyPropertyChanged("QC_NO");
            }
        }

        [DBColumn(Name = "QC_DATE", Storage = "m_QC_DATE", DbType = "106")]
        public DateTime? QC_DATE
        {
            get { return this.m_QC_DATE; }
            set
            {
                this.m_QC_DATE = value;
                this.NotifyPropertyChanged("QC_DATE");
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

        [DBColumn(Name = "QC_TYPE_ID", Storage = "m_QC_TYPE_ID", DbType = "107")]
        public int QC_TYPE_ID
        {
            get { return this.m_QC_TYPE_ID; }
            set
            {
                this.m_QC_TYPE_ID = value;
                this.NotifyPropertyChanged("QC_TYPE_ID");
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

        [DBColumn(Name = "QC_NOTE", Storage = "m_QC_NOTE", DbType = "126")]
        public string QC_NOTE
        {
            get { return this.m_QC_NOTE; }
            set
            {
                this.m_QC_NOTE = value;
                this.NotifyPropertyChanged("QC_NOTE");
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

        [DBColumn(Name = "APPROVE_DATE", Storage = "m_APPROVE_DATE", DbType = "106")]
        public DateTime? APPROVE_DATE
        {
            get { return this.m_APPROVE_DATE; }
            set
            {
                this.m_APPROVE_DATE = value;
                this.NotifyPropertyChanged("APPROVE_DATE");
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

        [DBColumn(Name = "SUP_ID", Storage = "m_SUP_ID", DbType = "107")]
        public int SUP_ID
        {
            get { return this.m_SUP_ID; }
            set
            {
                this.m_SUP_ID = value;
                this.NotifyPropertyChanged("SUP_ID");
            }
        }

        [DBColumn(Name = "QC_TIME", Storage = "m_QC_TIME", DbType = "106")]
        public DateTime? QC_TIME
        {
            get { return this.m_QC_TIME; }
            set
            {
                this.m_QC_TIME = value;
                this.NotifyPropertyChanged("QC_TIME");
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

        [DBColumn(Name = "QC_PASS_STATUS", Storage = "m_QC_PASS_STATUS", DbType = "126")]
        public string QC_PASS_STATUS
        {
            get { return this.m_QC_PASS_STATUS; }
            set
            {
                this.m_QC_PASS_STATUS = value;
                this.NotifyPropertyChanged("QC_PASS_STATUS");
            }
        }

        [DBColumn(Name = "QC_PASSED_BY", Storage = "m_QC_PASSED_BY", DbType = "107")]
        public int QC_PASSED_BY
        {
            get { return this.m_QC_PASSED_BY; }
            set
            {
                this.m_QC_PASSED_BY = value;
                this.NotifyPropertyChanged("QC_PASSED_BY");
            }
        }

        [DBColumn(Name = "QC_PASS_TIME", Storage = "m_QC_PASS_TIME", DbType = "106")]
        public DateTime? QC_PASS_TIME
        {
            get { return this.m_QC_PASS_TIME; }
            set
            {
                this.m_QC_PASS_TIME = value;
                this.NotifyPropertyChanged("QC_PASS_TIME");
            }
        }


        #endregion //properties
    }

    public partial class dcQC_MASTER
    {
        public string QC_TYPE_NAME { get; set; }
        public string SUP_NAME { get; set; }
        public string MRR_NO { get; set; }
        public DateTime MRR_DATE { get; set; }
        public string FULLNAME { get; set; }
        public string ITEM_NAME { get; set; }
        public string PURCHASE_NO { get; set; }
        public string SUP_CHALLAN_NO { get; set; }
        public int PURCHASE_ID { get; set; }
        public int DEPARTMENT_ID { get; set; }

        private bool m_IS_RETURN = false;
        public bool IS_RETURN
        {
            get { return this.m_IS_RETURN; }
            set { this.m_IS_RETURN = value; }
        }
        

        
        
        
    }
}
