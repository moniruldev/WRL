using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "REQ_RETURN_MASTER")]
  public  class dcREQ_RETURN_MASTER : DBBaseClass, INotifyPropertyChanged 
    {
        #region private members

        private string m_RTN_NO = string.Empty;
        private string m_REQ_NO = string.Empty;
        private string m_ISSUE_NO = string.Empty;
        private string m_RCV_NO = string.Empty;
        private string m_RTN_BY = string.Empty;
        private DateTime? m_RTN_DATE = null;
        private string m_CNCL_FLAG = string.Empty;
        private DateTime? m_CNCL_DATE = null;
        private string m_RTN_DEPT = string.Empty;
        private string m_RTN_BRANCH = string.Empty;
        private string m_COMP_ID = string.Empty;
        private string m_USER_ID = string.Empty;
        private DateTime? m_ENTER_DATE = null;
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

        [DBColumn(Name = "RTN_BY", Storage = "m_RTN_BY", DbType = "126")]
        public string RTN_BY
        {
            get { return this.m_RTN_BY; }
            set
            {
                this.m_RTN_BY = value;
                this.NotifyPropertyChanged("RTN_BY");
            }
        }

        [DBColumn(Name = "RTN_DATE", Storage = "m_RTN_DATE", DbType = "106")]
        public DateTime? RTN_DATE
        {
            get { return this.m_RTN_DATE; }
            set
            {
                this.m_RTN_DATE = value;
                this.NotifyPropertyChanged("RTN_DATE");
            }
        }

        [DBColumn(Name = "CNCL_FLAG", Storage = "m_CNCL_FLAG", DbType = "126")]
        public string CNCL_FLAG
        {
            get { return this.m_CNCL_FLAG; }
            set
            {
                this.m_CNCL_FLAG = value;
                this.NotifyPropertyChanged("CNCL_FLAG");
            }
        }

        [DBColumn(Name = "CNCL_DATE", Storage = "m_CNCL_DATE", DbType = "106")]
        public DateTime? CNCL_DATE
        {
            get { return this.m_CNCL_DATE; }
            set
            {
                this.m_CNCL_DATE = value;
                this.NotifyPropertyChanged("CNCL_DATE");
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

        [DBColumn(Name = "USER_ID", Storage = "m_USER_ID", DbType = "126")]
        public string USER_ID
        {
            get { return this.m_USER_ID; }
            set
            {
                this.m_USER_ID = value;
                this.NotifyPropertyChanged("USER_ID");
            }
        }

        [DBColumn(Name = "ENTER_DATE", Storage = "m_ENTER_DATE", DbType = "106")]
        public DateTime? ENTER_DATE
        {
            get { return this.m_ENTER_DATE; }
            set
            {
                this.m_ENTER_DATE = value;
                this.NotifyPropertyChanged("ENTER_DATE");
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
