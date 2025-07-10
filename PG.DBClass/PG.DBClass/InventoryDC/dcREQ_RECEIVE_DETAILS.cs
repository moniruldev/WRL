using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "REQ_RECEIVE_DETAILS")]
   public class dcREQ_RECEIVE_DETAILS : DBBaseClass, INotifyPropertyChanged 
    {
        #region private members

        private string m_REQ_NO = string.Empty;
        private string m_ISSUE_NO = string.Empty;
        private string m_RCV_NO = string.Empty;
        private decimal m_RCV_SLNO = 0;
        private string m_ITEM_CODE = string.Empty;
        private decimal m_RCV_QNTY = 0;
        private decimal m_REJ_QNTY = 0;
        private decimal m_CNCL_FLAG = 0;
        private string m_CNCL_NOTE = string.Empty;
        private string m_MSR_ID = string.Empty;
        private string m_RCV_DEPT = string.Empty;
        private string m_RCV_BRANCH = string.Empty;
        private string m_COMP_ID = string.Empty;
        private string m_CAT_ID = string.Empty;
        private string m_CAT_SUB_ID = string.Empty;
        private string m_RCV_NOTE = string.Empty;
        private decimal m_SHORT_QNTY = 0;
        private string m_STORE_ID = string.Empty;
        private string m_MRN_NO = string.Empty;
        private string m_RCV_COMP_CODE = string.Empty;

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


        [DBColumn(Name = "REQ_NO", Storage = "m_REQ_NO", DbType = "126", IsPrimaryKey = true)]
        public string REQ_NO
        {
            get { return this.m_REQ_NO; }
            set
            {
                this.m_REQ_NO = value;
                this.NotifyPropertyChanged("REQ_NO");
            }
        }

        [DBColumn(Name = "ISSUE_NO", Storage = "m_ISSUE_NO", DbType = "126", IsPrimaryKey = true)]
        public string ISSUE_NO
        {
            get { return this.m_ISSUE_NO; }
            set
            {
                this.m_ISSUE_NO = value;
                this.NotifyPropertyChanged("ISSUE_NO");
            }
        }

        [DBColumn(Name = "RCV_NO", Storage = "m_RCV_NO", DbType = "126", IsPrimaryKey = true)]
        public string RCV_NO
        {
            get { return this.m_RCV_NO; }
            set
            {
                this.m_RCV_NO = value;
                this.NotifyPropertyChanged("RCV_NO");
            }
        }

        [DBColumn(Name = "RCV_SLNO", Storage = "m_RCV_SLNO", DbType = "107")]
        public decimal RCV_SLNO
        {
            get { return this.m_RCV_SLNO; }
            set
            {
                this.m_RCV_SLNO = value;
                this.NotifyPropertyChanged("RCV_SLNO");
            }
        }

        [DBColumn(Name = "ITEM_CODE", Storage = "m_ITEM_CODE", DbType = "126", IsPrimaryKey = true)]
        public string ITEM_CODE
        {
            get { return this.m_ITEM_CODE; }
            set
            {
                this.m_ITEM_CODE = value;
                this.NotifyPropertyChanged("ITEM_CODE");
            }
        }

        [DBColumn(Name = "RCV_QNTY", Storage = "m_RCV_QNTY", DbType = "107")]
        public decimal RCV_QNTY
        {
            get { return this.m_RCV_QNTY; }
            set
            {
                this.m_RCV_QNTY = value;
                this.NotifyPropertyChanged("RCV_QNTY");
            }
        }

        [DBColumn(Name = "REJ_QNTY", Storage = "m_REJ_QNTY", DbType = "107")]
        public decimal REJ_QNTY
        {
            get { return this.m_REJ_QNTY; }
            set
            {
                this.m_REJ_QNTY = value;
                this.NotifyPropertyChanged("REJ_QNTY");
            }
        }

        [DBColumn(Name = "CNCL_FLAG", Storage = "m_CNCL_FLAG", DbType = "107")]
        public decimal CNCL_FLAG
        {
            get { return this.m_CNCL_FLAG; }
            set
            {
                this.m_CNCL_FLAG = value;
                this.NotifyPropertyChanged("CNCL_FLAG");
            }
        }

        [DBColumn(Name = "CNCL_NOTE", Storage = "m_CNCL_NOTE", DbType = "126")]
        public string CNCL_NOTE
        {
            get { return this.m_CNCL_NOTE; }
            set
            {
                this.m_CNCL_NOTE = value;
                this.NotifyPropertyChanged("CNCL_NOTE");
            }
        }

        [DBColumn(Name = "MSR_ID", Storage = "m_MSR_ID", DbType = "126")]
        public string MSR_ID
        {
            get { return this.m_MSR_ID; }
            set
            {
                this.m_MSR_ID = value;
                this.NotifyPropertyChanged("MSR_ID");
            }
        }

        [DBColumn(Name = "RCV_DEPT", Storage = "m_RCV_DEPT", DbType = "126", IsPrimaryKey = true)]
        public string RCV_DEPT
        {
            get { return this.m_RCV_DEPT; }
            set
            {
                this.m_RCV_DEPT = value;
                this.NotifyPropertyChanged("RCV_DEPT");
            }
        }

        [DBColumn(Name = "RCV_BRANCH", Storage = "m_RCV_BRANCH", DbType = "126", IsPrimaryKey = true)]
        public string RCV_BRANCH
        {
            get { return this.m_RCV_BRANCH; }
            set
            {
                this.m_RCV_BRANCH = value;
                this.NotifyPropertyChanged("RCV_BRANCH");
            }
        }

        [DBColumn(Name = "COMP_ID", Storage = "m_COMP_ID", DbType = "126", IsPrimaryKey = true)]
        public string COMP_ID
        {
            get { return this.m_COMP_ID; }
            set
            {
                this.m_COMP_ID = value;
                this.NotifyPropertyChanged("COMP_ID");
            }
        }

        [DBColumn(Name = "CAT_ID", Storage = "m_CAT_ID", DbType = "126", IsPrimaryKey = true)]
        public string CAT_ID
        {
            get { return this.m_CAT_ID; }
            set
            {
                this.m_CAT_ID = value;
                this.NotifyPropertyChanged("CAT_ID");
            }
        }

        [DBColumn(Name = "CAT_SUB_ID", Storage = "m_CAT_SUB_ID", DbType = "126", IsPrimaryKey = true)]
        public string CAT_SUB_ID
        {
            get { return this.m_CAT_SUB_ID; }
            set
            {
                this.m_CAT_SUB_ID = value;
                this.NotifyPropertyChanged("CAT_SUB_ID");
            }
        }

        [DBColumn(Name = "RCV_NOTE", Storage = "m_RCV_NOTE", DbType = "126")]
        public string RCV_NOTE
        {
            get { return this.m_RCV_NOTE; }
            set
            {
                this.m_RCV_NOTE = value;
                this.NotifyPropertyChanged("RCV_NOTE");
            }
        }

        [DBColumn(Name = "SHORT_QNTY", Storage = "m_SHORT_QNTY", DbType = "107")]
        public decimal SHORT_QNTY
        {
            get { return this.m_SHORT_QNTY; }
            set
            {
                this.m_SHORT_QNTY = value;
                this.NotifyPropertyChanged("SHORT_QNTY");
            }
        }

        [DBColumn(Name = "STORE_ID", Storage = "m_STORE_ID", DbType = "126")]
        public string STORE_ID
        {
            get { return this.m_STORE_ID; }
            set
            {
                this.m_STORE_ID = value;
                this.NotifyPropertyChanged("STORE_ID");
            }
        }

        [DBColumn(Name = "MRN_NO", Storage = "m_MRN_NO", DbType = "126", IsPrimaryKey = true)]
        public string MRN_NO
        {
            get { return this.m_MRN_NO; }
            set
            {
                this.m_MRN_NO = value;
                this.NotifyPropertyChanged("MRN_NO");
            }
        }

        [DBColumn(Name = "RCV_COMP_CODE", Storage = "m_RCV_COMP_CODE", DbType = "126")]
        public string RCV_COMP_CODE
        {
            get { return this.m_RCV_COMP_CODE; }
            set
            {
                this.m_RCV_COMP_CODE = value;
                this.NotifyPropertyChanged("RCV_COMP_CODE");
            }
        }

        #endregion //properties 
    }
}
