using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
    [Serializable]
    [DBTable(Name = "REQ_MASTER")]
    public partial class dcREQ_MASTER : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private Int64 m_REQ_ID = 0;
        private string m_REQ_NO = string.Empty;
        private DateTime? m_REQ_DATE = null;
        private DateTime? m_REQ_TIME = null;
        private int m_STORE_ID = 0;
        private int m_REQ_COMPANY_ID = 0;
        private int m_REQ_BRANCH_ID = 0;
        private int m_FROM_DEPARTMENT_ID = 0;
        private int m_TO_DEPERATMENT_ID = 0;
        private int? m_REQUIRED_BY = 0;
        private DateTime? m_REQUIRED_DATE = null;
        private string m_REQ_REMARKS = string.Empty;
        private int m_CREATE_BY = 0;
        private DateTime? m_CREATE_DATE = null;
        private int? m_UPDATE_BY = 0;
        private DateTime? m_UPDATE_DATE = null;
        private string m_IS_APPROVED = string.Empty;
        private int? m_APPROVED_BY = 0;
        private DateTime? m_APPROVED_DATE = null;
        private string m_AUTH_STATUS = string.Empty;
        private int? m_AUTH_BY = 0;
        private DateTime? m_AUTH_DATE = null;
        private string m_IS_CANCEL = string.Empty;
        private int? m_CANCEL_BY = 0;
        private DateTime? m_CANCEL_DATE = null;
        private string m_REF_NO = string.Empty;

        private int m_TRANSFER_TYPE_ID = 0;
        private string m_IS_PRODUCTION = string.Empty;
        private string m_IS_CLOSE = string.Empty;
        private int m_REQ_STATUS_ID = 0;
        private int m_BTY_TYPE_ID = 0;
        

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


        [DBColumn(Name = "REQ_ID", Storage = "m_REQ_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public Int64 REQ_ID
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

        [DBColumn(Name = "REQ_DATE", Storage = "m_REQ_DATE", DbType = "106")]
        public DateTime? REQ_DATE
        {
            get { return this.m_REQ_DATE; }
            set
            {
                this.m_REQ_DATE = value;
                this.NotifyPropertyChanged("REQ_DATE");
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

        [DBColumn(Name = "REQ_COMPANY_ID", Storage = "m_REQ_COMPANY_ID", DbType = "107")]
        public int REQ_COMPANY_ID
        {
            get { return this.m_REQ_COMPANY_ID; }
            set
            {
                this.m_REQ_COMPANY_ID = value;
                this.NotifyPropertyChanged("REQ_COMPANY_ID");
            }
        }

        [DBColumn(Name = "REQ_BRANCH_ID", Storage = "m_REQ_BRANCH_ID", DbType = "107")]
        public int REQ_BRANCH_ID
        {
            get { return this.m_REQ_BRANCH_ID; }
            set
            {
                this.m_REQ_BRANCH_ID = value;
                this.NotifyPropertyChanged("REQ_BRANCH_ID");
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

        [DBColumn(Name = "TO_DEPERATMENT_ID", Storage = "m_TO_DEPERATMENT_ID", DbType = "107")]
        public int TO_DEPERATMENT_ID
        {
            get { return this.m_TO_DEPERATMENT_ID; }
            set
            {
                this.m_TO_DEPERATMENT_ID = value;
                this.NotifyPropertyChanged("TO_DEPERATMENT_ID");
            }
        }

        [DBColumn(Name = "REQUIRED_BY", Storage = "m_REQUIRED_BY", DbType = "107")]
        public int? REQUIRED_BY
        {
            get { return this.m_REQUIRED_BY; }
            set
            {
                this.m_REQUIRED_BY = value;
                this.NotifyPropertyChanged("REQUIRED_BY");
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

        [DBColumn(Name = "REQ_REMARKS", Storage = "m_REQ_REMARKS", DbType = "126")]
        public string REQ_REMARKS
        {
            get { return this.m_REQ_REMARKS; }
            set
            {
                this.m_REQ_REMARKS = value;
                this.NotifyPropertyChanged("REQ_REMARKS");
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
        public int? UPDATE_BY
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
        public int? APPROVED_BY
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
        public int? AUTH_BY
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
        public int? CANCEL_BY
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

        [DBColumn(Name = "REF_NO", Storage = "m_REF_NO", DbType = "126")]
        public string REF_NO
        {
            get { return this.m_REF_NO; }
            set
            {
                this.m_REF_NO = value;
                this.NotifyPropertyChanged("REF_NO");
            }
        }

        [DBColumn(Name = "TRANSFER_TYPE_ID", Storage = "m_TRANSFER_TYPE_ID", DbType = "107")]
        public int TRANSFER_TYPE_ID
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

        [DBColumn(Name = "IS_CLOSE", Storage = "m_IS_CLOSE", DbType = "126")]
        public string IS_CLOSE
        {
            get { return this.m_IS_CLOSE; }
            set
            {
                this.m_IS_CLOSE = value;
                this.NotifyPropertyChanged("IS_CLOSE");
            }
        }

        [DBColumn(Name = "REQ_STATUS_ID", Storage = "m_REQ_STATUS_ID", DbType = "107")]
        public int REQ_STATUS_ID
        {
            get { return this.m_REQ_STATUS_ID; }
            set
            {
                this.m_REQ_STATUS_ID = value;
                this.NotifyPropertyChanged("REQ_STATUS_ID");
            }
        }

        [DBColumn(Name = "BTY_TYPE_ID", Storage = "m_BTY_TYPE_ID", DbType = "107")]
        public int BTY_TYPE_ID
        {
            get { return this.m_BTY_TYPE_ID; }
            set
            {
                this.m_BTY_TYPE_ID = value;
                this.NotifyPropertyChanged("BTY_TYPE_ID");
            }
        }


        

        #endregion //properties


    }
    public partial class dcREQ_MASTER
    {

        private string m_branch_name = string.Empty;
        private string m_from_department_name = string.Empty;
        private string m_to_department_name = string.Empty;
        private string m_company_name = string.Empty;
        private string m_store_name = string.Empty;
        private string m_required_for = string.Empty;
        private bool m_IsITCComplete = false;
        private string m_create_by_name = string.Empty;
        private string m_req_issue_no = string.Empty;
        private DateTime? m_req_issue_date = null;
        private bool m_IS_EDIT_ABLE = true;
        private string m_REQ_QTY = string.Empty;
        private string m_ISSUE_QTY = string.Empty;
        private string m_RCV_QTY = string.Empty;

        private List<dcREQ_DETAILS> m_Req_DtlList = null;
        public List<dcREQ_DETAILS> Req_DtlList
        {
            get { return m_Req_DtlList; }
            set { m_Req_DtlList = value; }
        }
        


        public string branch_name
        {
            get { return m_branch_name; }
            set { this.m_branch_name = value; }
        }
        public string from_department_name
        {
            get { return m_from_department_name; }
            set { this.m_from_department_name = value; }
        }

        public string to_department_name
        {
            get { return m_to_department_name; }
            set { this.m_to_department_name = value; }
        }
        public string company_name
        {
            get { return m_company_name; }
            set { this.m_company_name = value; }
        }
        public string store_name
        {
            get { return m_store_name; }
            set { this.m_store_name = value; }
        }
        public string required_for
        {
            get { return m_required_for; }
            set { this.m_required_for = value; }
        }

        public bool IsITCComplete
        {
            get { return m_IsITCComplete; }
            set { this.m_IsITCComplete = value; }
        }
        public string CREATE_BY_NAME
        {
            get { return m_create_by_name; }
            set { this.m_create_by_name = value; }
        }

        public string REQ_ISSUE_NO
        {
            get { return m_req_issue_no; }
            set { this.m_req_issue_no = value; }
        }
        public DateTime? REQ_ISSUE_DATE
        {
            get { return m_req_issue_date; }
            set { this.m_req_issue_date = value; }
        }


        private DateTime? m_REQ_ISSUE_TIME = null;
        public DateTime? REQ_ISSUE_TIME
        {
            get { return m_REQ_ISSUE_TIME; }
            set { this.m_REQ_ISSUE_TIME = value; }
        }

        private DateTime? m_ISSUE_RECEIVE_TIME = null;
        public DateTime? ISSUE_RECEIVE_TIME
        {
            get { return m_ISSUE_RECEIVE_TIME; }
            set { this.m_ISSUE_RECEIVE_TIME = value; }
        }
        public bool IS_EDIT_ABLE
        {
            get { return m_IS_EDIT_ABLE; }
            set { this.m_IS_EDIT_ABLE = value; }
        }
        public string ITEM_NAME { get; set; }

        public string STATUS_NAME { get; set; }
        public string STATUS_TYPE { get; set; }

        public string REQ_QTY
        {
            get { return m_REQ_QTY; }
            set { this.m_REQ_QTY = value; }
        }

        public string ISSUE_QTY
        {
            get { return m_ISSUE_QTY; }
            set { this.m_ISSUE_QTY = value; }
        }

        public string RCV_QTY
        {
            get { return m_RCV_QTY; }
            set { this.m_RCV_QTY = value; }
        }

        private string m_ISSUE_RECEIVE_NO = string.Empty;
        public string ISSUE_RECEIVE_NO
        {
            get { return m_ISSUE_RECEIVE_NO; }
            set { this.m_ISSUE_RECEIVE_NO = value; }
        }

        private string m_TRANS_NO = string.Empty;
        public string TRANS_NO
        {
            get { return m_TRANS_NO; }
            set { this.m_TRANS_NO = value; }
        }

        private DateTime? m_TRANS_TIME = null;
        public DateTime? TRANS_TIME
        {
            get { return m_TRANS_TIME; }
            set { this.m_TRANS_TIME = value; }
        }

        public string DURATION_TIME { get; set; }

        public string FULLNAME { get; set; }

        private string m_ITEM_QTY = string.Empty;
        public string ITEM_QTY
        {
            get { return m_ITEM_QTY; }
            set { this.m_ITEM_QTY = value; }
        }

        private string m_TOTAL_DURATION_TIME = string.Empty;
        public string TOTAL_DURATION_TIME
        {
            get { return m_TOTAL_DURATION_TIME; }
            set { this.m_TOTAL_DURATION_TIME = value; }
        }


    }

}
