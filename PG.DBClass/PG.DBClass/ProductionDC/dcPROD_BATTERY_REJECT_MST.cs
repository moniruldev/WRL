using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "PROD_BATTERY_REJECT_MST")]
    public partial class dcPROD_BATTERY_REJECT_MST : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_REJECT_MST_ID = 0;
        private string m_REJECTION_NO = string.Empty;
        private int m_DEPT_ID = 0;
        private DateTime? m_REJECT_DATE = null;
        private string m_SHIFT_ID = string.Empty;
        private string m_ENTRY_BY = string.Empty;
        private DateTime? m_ENTRY_DATE = null;
        private string m_EDIT_BY = string.Empty;
        private DateTime? m_EDIT_DATE = null;
        private string m_AUTH_STATUS = string.Empty;
        private string m_AUTH_BY = string.Empty;
        private DateTime? m_AUTH_DATE = null;

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


        [DBColumn(Name = "REJECT_MST_ID", Storage = "m_REJECT_MST_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int REJECT_MST_ID
        {
            get { return this.m_REJECT_MST_ID; }
            set
            {
                this.m_REJECT_MST_ID = value;
                this.NotifyPropertyChanged("REJECT_MST_ID");
            }
        }

        [DBColumn(Name = "REJECTION_NO", Storage = "m_REJECTION_NO", DbType = "126")]
        public string REJECTION_NO
        {
            get { return this.m_REJECTION_NO; }
            set
            {
                this.m_REJECTION_NO = value;
                this.NotifyPropertyChanged("REJECTION_NO");
            }
        }

        [DBColumn(Name = "DEPT_ID", Storage = "m_DEPT_ID", DbType = "107")]
        public int DEPT_ID
        {
            get { return this.m_DEPT_ID; }
            set
            {
                this.m_DEPT_ID = value;
                this.NotifyPropertyChanged("DEPT_ID");
            }
        }

        [DBColumn(Name = "REJECT_DATE", Storage = "m_REJECT_DATE", DbType = "106")]
        public DateTime? REJECT_DATE
        {
            get { return this.m_REJECT_DATE; }
            set
            {
                this.m_REJECT_DATE = value;
                this.NotifyPropertyChanged("REJECT_DATE");
            }
        }

        [DBColumn(Name = "SHIFT_ID", Storage = "m_SHIFT_ID", DbType = "107")]
        public string SHIFT_ID
        {
            get { return this.m_SHIFT_ID; }
            set
            {
                this.m_SHIFT_ID = value;
                this.NotifyPropertyChanged("SHIFT_ID");
            }
        }

        [DBColumn(Name = "ENTRY_BY", Storage = "m_ENTRY_BY", DbType = "126")]
        public string ENTRY_BY
        {
            get { return this.m_ENTRY_BY; }
            set
            {
                this.m_ENTRY_BY = value;
                this.NotifyPropertyChanged("ENTRY_BY");
            }
        }

        [DBColumn(Name = "ENTRY_DATE", Storage = "m_ENTRY_DATE", DbType = "106")]
        public DateTime? ENTRY_DATE
        {
            get { return this.m_ENTRY_DATE; }
            set
            {
                this.m_ENTRY_DATE = value;
                this.NotifyPropertyChanged("ENTRY_DATE");
            }
        }

        [DBColumn(Name = "EDIT_BY", Storage = "m_EDIT_BY", DbType = "126")]
        public string EDIT_BY
        {
            get { return this.m_EDIT_BY; }
            set
            {
                this.m_EDIT_BY = value;
                this.NotifyPropertyChanged("EDIT_BY");
            }
        }

        [DBColumn(Name = "EDIT_DATE", Storage = "m_EDIT_DATE", DbType = "106")]
        public DateTime? EDIT_DATE
        {
            get { return this.m_EDIT_DATE; }
            set
            {
                this.m_EDIT_DATE = value;
                this.NotifyPropertyChanged("EDIT_DATE");
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

        [DBColumn(Name = "AUTH_BY", Storage = "m_AUTH_BY", DbType = "126")]
        public string AUTH_BY
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

        #endregion //properties
    }

     public partial class dcPROD_BATTERY_REJECT_MST
     {
         private List<dcPROD_BATTERY_REJECT_DTL> m_RejectionDetList = null;
         public List<dcPROD_BATTERY_REJECT_DTL> RejectionDetList
         {
             get { return m_RejectionDetList; }
             set { m_RejectionDetList = value; }
         }

         public string SHIFT_NAME { get; set; }
         public string DEPARTMENT_NAME { get; set; }
        

     }
}
