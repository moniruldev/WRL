using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "REQ_RETURN_DETAILS")]
  public  class dcREQ_RETURN_DETAILS : DBBaseClass, INotifyPropertyChanged 
    {
        #region private members

        private string m_RTN_NO = string.Empty;
        private string m_REQ_NO = string.Empty;
        private string m_ISSUE_NO = string.Empty;
        private string m_RCV_NO = string.Empty;
        private decimal m_RTN_SLNO = 0;
        private string m_ITEM_CODE = string.Empty;
        private decimal m_RTN_QNTY = 0;
        private string m_MSR_ID = string.Empty;
        private string m_RTN_DEPT = string.Empty;
        private string m_RTN_BRANCH = string.Empty;
        private string m_COMP_ID = string.Empty;
        private string m_CAT_ID = string.Empty;
        private string m_CAT_SUB_ID = string.Empty;
        private string m_RTN_NOTE = string.Empty;
        private string m_STORE_ID = string.Empty;
        private string m_MRN_NO = string.Empty;
        private string m_RTN_COMP_CODE = string.Empty;

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


        [DBColumn(Name = "RTN_NO", Storage = "m_RTN_NO", DbType = "126", IsPrimaryKey = true)]
        public string RTN_NO
        {
            get { return this.m_RTN_NO; }
            set
            {
                this.m_RTN_NO = value;
                this.NotifyPropertyChanged("RTN_NO");
            }
        }

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

        [DBColumn(Name = "RTN_SLNO", Storage = "m_RTN_SLNO", DbType = "107")]
        public decimal RTN_SLNO
        {
            get { return this.m_RTN_SLNO; }
            set
            {
                this.m_RTN_SLNO = value;
                this.NotifyPropertyChanged("RTN_SLNO");
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

        [DBColumn(Name = "RTN_QNTY", Storage = "m_RTN_QNTY", DbType = "107")]
        public decimal RTN_QNTY
        {
            get { return this.m_RTN_QNTY; }
            set
            {
                this.m_RTN_QNTY = value;
                this.NotifyPropertyChanged("RTN_QNTY");
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

        [DBColumn(Name = "RTN_DEPT", Storage = "m_RTN_DEPT", DbType = "126", IsPrimaryKey = true)]
        public string RTN_DEPT
        {
            get { return this.m_RTN_DEPT; }
            set
            {
                this.m_RTN_DEPT = value;
                this.NotifyPropertyChanged("RTN_DEPT");
            }
        }

        [DBColumn(Name = "RTN_BRANCH", Storage = "m_RTN_BRANCH", DbType = "126", IsPrimaryKey = true)]
        public string RTN_BRANCH
        {
            get { return this.m_RTN_BRANCH; }
            set
            {
                this.m_RTN_BRANCH = value;
                this.NotifyPropertyChanged("RTN_BRANCH");
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

        [DBColumn(Name = "RTN_NOTE", Storage = "m_RTN_NOTE", DbType = "126")]
        public string RTN_NOTE
        {
            get { return this.m_RTN_NOTE; }
            set
            {
                this.m_RTN_NOTE = value;
                this.NotifyPropertyChanged("RTN_NOTE");
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

        [DBColumn(Name = "RTN_COMP_CODE", Storage = "m_RTN_COMP_CODE", DbType = "126")]
        public string RTN_COMP_CODE
        {
            get { return this.m_RTN_COMP_CODE; }
            set
            {
                this.m_RTN_COMP_CODE = value;
                this.NotifyPropertyChanged("RTN_COMP_CODE");
            }
        }

        #endregion //properties 
    }
}
