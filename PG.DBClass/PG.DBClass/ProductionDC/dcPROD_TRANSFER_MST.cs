using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "PROD_TRANSFER_MST")]
    public partial class dcPROD_TRANSFER_MST : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_TRANSFER_ID = 0;
        private string m_TRANSFER_NO = string.Empty;
        private DateTime? m_TRANSFER_DATE = null;
        private int m_FROM_DEPT_ID = 0;
        private string m_TRANSFER_TYPE = string.Empty;
        private string m_REMARKS = string.Empty;
        private string m_CREATE_BY = string.Empty;
        private DateTime? m_CREATE_DATE = null;
        private string m_UPDATE_BY = string.Empty;
        private DateTime? m_UPDATE_DATE = null;
        private string m_AUTHO_STATUS = string.Empty;
        private DateTime? m_AUTHO_DATE = null;
        private string m_REC_STATUS = string.Empty;
        private string m_AUTHO_BY = string.Empty;
        private string m_IS_RM = string.Empty;
        private string m_SHIFT_ID = string.Empty;
        private int m_FROM_STLM_ID = 0;
        private string m_APPROVAL_STATUS = string.Empty;
        private decimal m_APPROVED_BY = 0;
        private DateTime? m_APPROVED_DATE = null;
        private int m_PROD_REJECTION_ID = 0;
        private int m_TO_DEPT_ID = 0;
        private int m_TO_STLM_ID = 0;
        private string m_TRANS_REF_NO = string.Empty;


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


        [DBColumn(Name = "TRANSFER_ID", Storage = "m_TRANSFER_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int TRANSFER_ID
        {
            get { return this.m_TRANSFER_ID; }
            set
            {
                this.m_TRANSFER_ID = value;
                this.NotifyPropertyChanged("TRANSFER_ID");
            }
        }

        [DBColumn(Name = "TRANSFER_NO", Storage = "m_TRANSFER_NO", DbType = "126")]
        public string TRANSFER_NO
        {
            get { return this.m_TRANSFER_NO; }
            set
            {
                this.m_TRANSFER_NO = value;
                this.NotifyPropertyChanged("TRANSFER_NO");
            }
        }

        [DBColumn(Name = "TRANSFER_DATE", Storage = "m_TRANSFER_DATE", DbType = "106")]
        public DateTime? TRANSFER_DATE
        {
            get { return this.m_TRANSFER_DATE; }
            set
            {
                this.m_TRANSFER_DATE = value;
                this.NotifyPropertyChanged("TRANSFER_DATE");
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

        [DBColumn(Name = "TRANSFER_TYPE", Storage = "m_TRANSFER_TYPE", DbType = "126")]
        public string TRANSFER_TYPE
        {
            get { return this.m_TRANSFER_TYPE; }
            set
            {
                this.m_TRANSFER_TYPE = value;
                this.NotifyPropertyChanged("TRANSFER_TYPE");
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

        [DBColumn(Name = "UPDATE_BY", Storage = "m_UPDATE_BY", DbType = "126")]
        public string UPDATE_BY
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

        [DBColumn(Name = "AUTHO_STATUS", Storage = "m_AUTHO_STATUS", DbType = "126")]
        public string AUTHO_STATUS
        {
            get { return this.m_AUTHO_STATUS; }
            set
            {
                this.m_AUTHO_STATUS = value;
                this.NotifyPropertyChanged("AUTHO_STATUS");
            }
        }

        [DBColumn(Name = "AUTHO_DATE", Storage = "m_AUTHO_DATE", DbType = "106")]
        public DateTime? AUTHO_DATE
        {
            get { return this.m_AUTHO_DATE; }
            set
            {
                this.m_AUTHO_DATE = value;
                this.NotifyPropertyChanged("AUTHO_DATE");
            }
        }

        [DBColumn(Name = "REC_STATUS", Storage = "m_REC_STATUS", DbType = "126")]
        public string REC_STATUS
        {
            get { return this.m_REC_STATUS; }
            set
            {
                this.m_REC_STATUS = value;
                this.NotifyPropertyChanged("REC_STATUS");
            }
        }

        [DBColumn(Name = "AUTHO_BY", Storage = "m_AUTHO_BY", DbType = "126")]
        public string AUTHO_BY
        {
            get { return this.m_AUTHO_BY; }
            set
            {
                this.m_AUTHO_BY = value;
                this.NotifyPropertyChanged("AUTHO_BY");
            }
        }

        [DBColumn(Name = "IS_RM", Storage = "m_IS_RM", DbType = "126")]
        public string IS_RM
        {
            get { return this.m_IS_RM; }
            set
            {
                this.m_IS_RM = value;
                this.NotifyPropertyChanged("IS_RM");
            }
        }

        [DBColumn(Name = "SHIFT_ID", Storage = "m_SHIFT_ID", DbType = "126")]
        public string SHIFT_ID
        {
            get { return this.m_SHIFT_ID; }
            set
            {
                this.m_SHIFT_ID = value;
                this.NotifyPropertyChanged("SHIFT_ID");
            }
        }

        [DBColumn(Name = "FROM_STLM_ID", Storage = "m_FROM_STLM_ID", DbType = "107")]
        public int FROM_STLM_ID
        {
            get { return this.m_FROM_STLM_ID; }
            set
            {
                this.m_FROM_STLM_ID = value;
                this.NotifyPropertyChanged("FROM_STLM_ID");
            }
        }

        [DBColumn(Name = "APPROVAL_STATUS", Storage = "m_APPROVAL_STATUS", DbType = "126")]
        public string APPROVAL_STATUS
        {
            get { return this.m_APPROVAL_STATUS; }
            set
            {
                this.m_APPROVAL_STATUS = value;
                this.NotifyPropertyChanged("APPROVAL_STATUS");
            }
        }

        [DBColumn(Name = "APPROVED_BY", Storage = "m_APPROVED_BY", DbType = "107")]
        public decimal APPROVED_BY
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

        [DBColumn(Name = "PROD_REJECTION_ID", Storage = "m_PROD_REJECTION_ID", DbType = "107")]
        public int PROD_REJECTION_ID
        {
            get { return this.m_PROD_REJECTION_ID; }
            set
            {
                this.m_PROD_REJECTION_ID = value;
                this.NotifyPropertyChanged("PROD_REJECTION_ID");
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

        [DBColumn(Name = "TO_STLM_ID", Storage = "m_TO_STLM_ID", DbType = "107")]
        public int TO_STLM_ID
        {
            get { return this.m_TO_STLM_ID; }
            set
            {
                this.m_TO_STLM_ID = value;
                this.NotifyPropertyChanged("TO_STLM_ID");
            }
        }

        [DBColumn(Name = "TRANS_REF_NO", Storage = "m_TRANS_REF_NO", DbType = "126")]
        public string TRANS_REF_NO
        {
            get { return this.m_TRANS_REF_NO; }
            set
            {
                this.m_TRANS_REF_NO = value;
                this.NotifyPropertyChanged("TRANS_REF_NO");
            }
        }

        #endregion //properties
    }

    public partial class dcPROD_TRANSFER_MST
    {
        private List<dcPROD_TRANSFER_DTL> m_TransferDetList = null;
        public List<dcPROD_TRANSFER_DTL> TransferDetList
        {
            get { return m_TransferDetList; }
            set { m_TransferDetList = value; }
        }


        private string m_COMPANY_NAME = string.Empty;
        public string COMPANY_NAME
        {
            get { return m_COMPANY_NAME; }
            set { m_COMPANY_NAME = value; }
        }

        private int m_SLNO = 0;
        public int SLNO
        {
            get { return m_SLNO; }
            set { m_SLNO = value; }
        }

        public string ITEM_NAME { get; set; }
        public string UOM_NAME { get; set; }
        public string STLM_NAME { get; set; }
    }

}
