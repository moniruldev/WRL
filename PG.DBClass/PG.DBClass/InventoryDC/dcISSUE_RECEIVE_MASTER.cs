using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "ISSUE_RECEIVE_MASTER")]
    public partial class dcISSUE_RECEIVE_MASTER : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private Int64 m_ISSUE_RECEIVE_ID = 0;
        private string m_ISSUE_RECEIVE_NO = string.Empty;
        private DateTime? m_ISSUE_RECEIVE_DATE = null;
        private DateTime? m_ISSUE_RECEIVE_TIME = null;
        private Int64 m_REQ_ISSUE_ID = 0;
        private string m_ISSUE_RCV_REMARKS = string.Empty;
        private int? m_CREATE_BY = 0;
        private DateTime? m_CREATE_DATE = null;
        private int? m_UPDATE_BY = 0;
        private DateTime? m_UPDATE_DATE = null;
        private string m_AUTH_STATUS = string.Empty;
        private int? m_AUTH_BY = 0;
        private DateTime? m_AUTH_DATE = null;
        private string m_IS_CANCEL = string.Empty;
        private int? m_CANCEL_BY = 0;
        private DateTime? m_CANCEL_DATE = null;
        private int m_BTY_TYPE_ID = 0;
        private string m_IS_REPAIR = string.Empty;
        private int m_DEPARTMENT_ID = 0;
        private int m_TRANSFER_TYPE_ID = 0;
        private string m_IS_PRODUCTION = string.Empty;
        private string m_IS_NOT_SND_TRANS = string.Empty;
        private string m_IS_RETURN = string.Empty;
        private int m_COMPANY_ID = 0;
        private int m_STORE_ID = 0;
        private int m_STLM_ID = 0;
        private int m_TO_STLM_ID = 0;
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


        [DBColumn(Name = "ISSUE_RECEIVE_ID", Storage = "m_ISSUE_RECEIVE_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public Int64 ISSUE_RECEIVE_ID
        {
            get { return this.m_ISSUE_RECEIVE_ID; }
            set
            {
                this.m_ISSUE_RECEIVE_ID = value;
                this.NotifyPropertyChanged("ISSUE_RECEIVE_ID");
            }
        }

        [DBColumn(Name = "ISSUE_RECEIVE_NO", Storage = "m_ISSUE_RECEIVE_NO", DbType = "126")]
        public string ISSUE_RECEIVE_NO
        {
            get { return this.m_ISSUE_RECEIVE_NO; }
            set
            {
                this.m_ISSUE_RECEIVE_NO = value;
                this.NotifyPropertyChanged("ISSUE_RECEIVE_NO");
            }
        }

        [DBColumn(Name = "ISSUE_RECEIVE_DATE", Storage = "m_ISSUE_RECEIVE_DATE", DbType = "106")]
        public DateTime? ISSUE_RECEIVE_DATE
        {
            get { return this.m_ISSUE_RECEIVE_DATE; }
            set
            {
                this.m_ISSUE_RECEIVE_DATE = value;
                this.NotifyPropertyChanged("ISSUE_RECEIVE_DATE");
            }
        }

        [DBColumn(Name = "ISSUE_RECEIVE_TIME", Storage = "m_ISSUE_RECEIVE_TIME", DbType = "106")]
        public DateTime? ISSUE_RECEIVE_TIME
        {
            get { return this.m_ISSUE_RECEIVE_TIME; }
            set
            {
                this.m_ISSUE_RECEIVE_TIME = value;
                this.NotifyPropertyChanged("ISSUE_RECEIVE_TIME");
            }
        }

        [DBColumn(Name = "REQ_ISSUE_ID", Storage = "m_REQ_ISSUE_ID", DbType = "107")]
        public Int64 REQ_ISSUE_ID
        {
            get { return this.m_REQ_ISSUE_ID; }
            set
            {
                this.m_REQ_ISSUE_ID = value;
                this.NotifyPropertyChanged("REQ_ISSUE_ID");
            }
        }

        [DBColumn(Name = "ISSUE_RCV_REMARKS", Storage = "m_ISSUE_RCV_REMARKS", DbType = "126")]
        public string ISSUE_RCV_REMARKS
        {
            get { return this.m_ISSUE_RCV_REMARKS; }
            set
            {
                this.m_ISSUE_RCV_REMARKS = value;
                this.NotifyPropertyChanged("ISSUE_RCV_REMARKS");
            }
        }

        [DBColumn(Name = "CREATE_BY", Storage = "m_CREATE_BY", DbType = "107")]
        public int? CREATE_BY
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

        [DBColumn(Name = "IS_REPAIR", Storage = "m_IS_REPAIR", DbType = "126")]
        public string IS_REPAIR
        {
            get { return this.m_IS_REPAIR; }
            set
            {
                this.m_IS_REPAIR = value;
                this.NotifyPropertyChanged("IS_REPAIR");
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

          [DBColumn(Name = "IS_NOT_SND_TRANS", Storage = "m_IS_NOT_SND_TRANS", DbType = "126")]
        public string IS_NOT_SND_TRANS
        {
            get { return this.m_IS_NOT_SND_TRANS; }
            set
            {
                this.m_IS_NOT_SND_TRANS = value;
                this.NotifyPropertyChanged("IS_NOT_SND_TRANS");
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
         
        #endregion //properties
    }
    public partial class dcISSUE_RECEIVE_MASTER
    {

        private string m_branch_name = string.Empty;
        private string m_from_department_name = string.Empty;
        private string m_to_department_name = string.Empty;
        private string m_company_name = string.Empty;
        private string m_store_name = string.Empty;
        private string m_is_req = string.Empty;
        private string m_RCV_TYPE = string.Empty;
        private string m_ITEM_NAME = string.Empty;
        

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
        public string IS_REQ
        {
            get { return m_is_req; }
            set { this.m_is_req = value; }
        }

        public string RCV_TYPE
        {
            get { return m_RCV_TYPE; }
            set { this.m_RCV_TYPE = value; }
        }

        public string ITEM_NAME
        {
            get { return m_ITEM_NAME; }
            set { this.m_ITEM_NAME = value; }
        }

        public string REQ_ISSUE_NO { get; set; }
        public string REQ_NO { get; set; }
        public int TO_DEPARTMENT_ID { get; set; }
        public DateTime? REQ_ISSUE_DATE { get; set; }
        public DateTime? REQ_DATE { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        
        
    }
}
