using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.InventoryDC
{
    [DBTable(Name = "QUOTATION_MST")]
    public partial class dcQUOTATION_MST : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_QUOTATION_MST_ID = 0;
        private string m_QUOTATION_NO = string.Empty;
        private DateTime? m_QUOTATION_DATE = null;
        private DateTime? m_QUOTATION_EXPIRE_DATE = null;
        private string m_PR_REF_NO = string.Empty;
        private decimal m_INDENT_ID = 0;
        private decimal m_SUP_ID = 0;
        private string m_SUP_CONTACT_PERSON = string.Empty;
        private string m_CONTACT_NO = string.Empty;
        private string m_QUOTATION_FILE = string.Empty;
        private string m_REMARKS = string.Empty;
        private decimal m_CREATE_BY = 0;
        private DateTime? m_CREATE_DATE = null;
        private decimal m_UPDATE_BY = 0;
        private DateTime? m_UPDATE_DATE = null;
        private decimal m_QUOTATION_TYPE_ID = 0;
        private decimal m_DEPT_ID = 0;
        private string m_APPROVE_STATUS = string.Empty;
        private DateTime? m_APPROVE_DATE = null;
        private string m_APPROVE_BY = string.Empty;
        private string m_FILE_NAME = string.Empty;
        private string m_CONTENT_TYPE = string.Empty;
        



        
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


        [DBColumn(Name = "QUOTATION_MST_ID", Storage = "m_QUOTATION_MST_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int QUOTATION_MST_ID
        {
            get { return this.m_QUOTATION_MST_ID; }
            set
            {
                this.m_QUOTATION_MST_ID = value;
                this.NotifyPropertyChanged("QUOTATION_MST_ID");
            }
        }

        [DBColumn(Name = "QUOTATION_NO", Storage = "m_QUOTATION_NO", DbType = "126")]
        public string QUOTATION_NO
        {
            get { return this.m_QUOTATION_NO; }
            set
            {
                this.m_QUOTATION_NO = value;
                this.NotifyPropertyChanged("QUOTATION_NO");
            }
        }

        [DBColumn(Name = "QUOTATION_DATE", Storage = "m_QUOTATION_DATE", DbType = "106")]
        public DateTime? QUOTATION_DATE
        {
            get { return this.m_QUOTATION_DATE; }
            set
            {
                this.m_QUOTATION_DATE = value;
                this.NotifyPropertyChanged("QUOTATION_DATE");
            }
        }

        [DBColumn(Name = "QUOTATION_EXPIRE_DATE", Storage = "m_QUOTATION_EXPIRE_DATE", DbType = "106")]
        public DateTime? QUOTATION_EXPIRE_DATE
        {
            get { return this.m_QUOTATION_EXPIRE_DATE; }
            set
            {
                this.m_QUOTATION_EXPIRE_DATE = value;
                this.NotifyPropertyChanged("QUOTATION_EXPIRE_DATE");
            }
        }

        [DBColumn(Name = "PR_REF_NO", Storage = "m_PR_REF_NO", DbType = "126")]
        public string PR_REF_NO
        {
            get { return this.m_PR_REF_NO; }
            set
            {
                this.m_PR_REF_NO = value;
                this.NotifyPropertyChanged("PR_REF_NO");
            }
        }

        [DBColumn(Name = "INDENT_ID", Storage = "m_INDENT_ID", DbType = "107")]
        public decimal INDENT_ID
        {
            get { return this.m_INDENT_ID; }
            set
            {
                this.m_INDENT_ID = value;
                this.NotifyPropertyChanged("INDENT_ID");
            }
        }

        [DBColumn(Name = "SUP_ID", Storage = "m_SUP_ID", DbType = "107")]
        public decimal SUP_ID
        {
            get { return this.m_SUP_ID; }
            set
            {
                this.m_SUP_ID = value;
                this.NotifyPropertyChanged("SUP_ID");
            }
        }

        [DBColumn(Name = "SUP_CONTACT_PERSON", Storage = "m_SUP_CONTACT_PERSON", DbType = "126")]
        public string SUP_CONTACT_PERSON
        {
            get { return this.m_SUP_CONTACT_PERSON; }
            set
            {
                this.m_SUP_CONTACT_PERSON = value;
                this.NotifyPropertyChanged("SUP_CONTACT_PERSON");
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

        [DBColumn(Name = "QUOTATION_FILE", Storage = "m_QUOTATION_FILE", DbType = "102")]
        public string QUOTATION_FILE
        {
            get { return this.m_QUOTATION_FILE; }
            set
            {
                this.m_QUOTATION_FILE = value;
                this.NotifyPropertyChanged("QUOTATION_FILE");
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

        [DBColumn(Name = "CREATE_BY", Storage = "m_CREATE_BY", DbType = "107")]
        public decimal CREATE_BY
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
        public decimal UPDATE_BY
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

        [DBColumn(Name = "QUOTATION_TYPE_ID", Storage = "m_QUOTATION_TYPE_ID", DbType = "107")]
        public decimal QUOTATION_TYPE_ID
        {
            get { return this.m_QUOTATION_TYPE_ID; }
            set
            {
                this.m_QUOTATION_TYPE_ID = value;
                this.NotifyPropertyChanged("QUOTATION_TYPE_ID");
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


        [DBColumn(Name = "APPROVE_STATUS", Storage = "m_APPROVE_STATUS", DbType = "126")]
        public string APPROVE_STATUS
        {
            get { return this.m_APPROVE_STATUS; }
            set
            {
                this.m_APPROVE_STATUS = value;
                this.NotifyPropertyChanged("APPROVE_STATUS");
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

        [DBColumn(Name = "APPROVE_BY", Storage = "m_APPROVE_BY", DbType = "126")]
        public string APPROVE_BY
        {
            get { return this.m_APPROVE_BY; }
            set
            {
                this.m_APPROVE_BY = value;
                this.NotifyPropertyChanged("APPROVE_BY");
            }
        }

        [DBColumn(Name = "FILE_NAME", Storage = "m_FILE_NAME", DbType = "126")]
        public string FILE_NAME
        {
            get { return this.m_FILE_NAME; }
            set
            {
                this.m_FILE_NAME = value;
                this.NotifyPropertyChanged("FILE_NAME");
            }
        }

          [DBColumn(Name = "CONTENT_TYPE", Storage = "m_CONTENT_TYPE", DbType = "126")]
        public string CONTENT_TYPE
        {
            get { return this.m_CONTENT_TYPE; }
            set
            {
                this.m_CONTENT_TYPE = value;
                this.NotifyPropertyChanged("CONTENT_TYPE");
            }
        }
       
       

        #endregion //properties
    }

    public partial class dcQUOTATION_MST
    {

        private string m_sup_name = string.Empty;
        private string m_SUP_CODE = string.Empty;
        private string m_sup_address = string.Empty;
        private string m_item_name = string.Empty;
        private string m_uom_name = string.Empty;
        private string m_purchase_qty = string.Empty;
        private bool m_IS_MRR_COMPLETE = false;


        public Int64 m_MRR_ID = 0;

        public string m_MRR_AUTH_STATUS = "N";

        public string sup_name
        {
            get { return m_sup_name; }
            set { this.m_sup_name = value; }
        }

        public string SUP_CODE
        {
            get { return m_SUP_CODE; }
            set { this.m_SUP_CODE = value; }
        }

        public string sup_address
        {
            get { return m_sup_address; }
            set { this.m_sup_address = value; }
        }
        public string item_name
        {
            get { return m_item_name; }
            set { this.m_item_name = value; }
        }

        public string uom_name
        {
            get { return m_uom_name; }
            set { this.m_uom_name = value; }
        }
        public string purchase_qty
        {
            get { return m_purchase_qty; }
            set { this.m_purchase_qty = value; }
        }

        private DateTime? m_MRR_DATE = null;

        public DateTime? MRR_DATE
        {
            get { return m_MRR_DATE; }
            set { this.m_MRR_DATE = value; }
        }
        private string m_MRR_NO = null;

        public string MRR_NO
        {
            get { return m_MRR_NO; }
            set { this.m_MRR_NO = value; }
        }

        public string MRR_AUTH_STATUS
        {
            get { return m_MRR_AUTH_STATUS; }
            set { this.m_MRR_AUTH_STATUS = value; }
        }

        public Int64 MRR_ID
        {
            get { return m_MRR_ID; }
            set { this.m_MRR_ID = value; }
        }


        public bool m_IsMRRComplete = false;
        public bool IsMRRComplete
        {
            get { return m_IsMRRComplete; }
            set { this.m_IsMRRComplete = value; }
        }

        private bool m_IsMRRAuthComplete = false;
        public bool IsMRRAuthComplete
        {
            get { return m_IsMRRAuthComplete; }
            set { this.m_IsMRRAuthComplete = value; }
        }
        private string m_CREATE_BY_NAME = string.Empty;
        public string CREATE_BY_NAME
        {
            get { return m_CREATE_BY_NAME; }
            set { this.m_CREATE_BY_NAME = value; }
        }

        private string m_INDT_NO = string.Empty;
        public string INDT_NO
        {
            get { return m_INDT_NO; }
            set { this.m_INDT_NO = value; }
        }

        private string m_STATUS_NAME = string.Empty;
        public string STATUS_NAME
        {
            get { return m_STATUS_NAME; }
            set { this.m_STATUS_NAME = value; }
        }

        private string m_ID_NAME = string.Empty;
        public string ID_NAME
        {
            get { return m_ID_NAME; }
            set { this.m_ID_NAME = value; }
        }

        private string m_DURATION_TIME = string.Empty;
        public string DURATION_TIME
        {
            get { return m_DURATION_TIME; }
            set { this.m_DURATION_TIME = value; }
        }

        private string m_TOTAL_DURATION_TIME = string.Empty;
        public string TOTAL_DURATION_TIME
        {
            get { return m_TOTAL_DURATION_TIME; }
            set { this.m_TOTAL_DURATION_TIME = value; }
        }

        private DateTime? m_DATE_TIME = null;
        public DateTime? DATE_TIME
        {
            get { return m_DATE_TIME; }
            set { this.m_DATE_TIME = value; }
        }

        public string PO_TYPE { get; set; }


    }
}
