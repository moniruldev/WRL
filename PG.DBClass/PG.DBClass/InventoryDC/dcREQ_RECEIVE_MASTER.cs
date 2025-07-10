using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "REQ_RECEIVE_MASTER")]
   public class dcREQ_RECEIVE_MASTER : DBBaseClass, INotifyPropertyChanged 
    {
        #region private members

        private string m_REQ_NO = string.Empty;
        private string m_ISSUE_NO = string.Empty;
        private string m_RCV_NO = string.Empty;
        private string m_RCV_BY = string.Empty;
        private DateTime? m_RCV_DATE = null;
        private string m_CNCL_FLAG = string.Empty;
        private DateTime? m_CNCL_DATE = null;
        private string m_RCV_DEPT = string.Empty;
        private string m_RCV_BRANCH = string.Empty;
        private string m_WORKING_PLACE = string.Empty;
        private string m_COMP_ID = string.Empty;
        private string m_USER_ID = string.Empty;
        private DateTime? m_ENTER_DATE = null;
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

        [DBColumn(Name = "RCV_BY", Storage = "m_RCV_BY", DbType = "126")]
        public string RCV_BY
        {
            get { return this.m_RCV_BY; }
            set
            {
                this.m_RCV_BY = value;
                this.NotifyPropertyChanged("RCV_BY");
            }
        }

        [DBColumn(Name = "RCV_DATE", Storage = "m_RCV_DATE", DbType = "106")]
        public DateTime? RCV_DATE
        {
            get { return this.m_RCV_DATE; }
            set
            {
                this.m_RCV_DATE = value;
                this.NotifyPropertyChanged("RCV_DATE");
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

        [DBColumn(Name = "WORKING_PLACE", Storage = "m_WORKING_PLACE", DbType = "126")]
        public string WORKING_PLACE
        {
            get { return this.m_WORKING_PLACE; }
            set
            {
                this.m_WORKING_PLACE = value;
                this.NotifyPropertyChanged("WORKING_PLACE");
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
