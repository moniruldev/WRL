using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.InventoryDC
{
    [DBTable(Name = "INV_ADJUST_DEPT_MASTER_TEMP")]
    public partial class dcINV_ADJUST_DEPT_MASTER_TEMP : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_INV_ADJUST_ID = 0;
        private string m_INV_ADJUST_NO = string.Empty;
        private DateTime? m_INV_ADJUST_DATE = null;
        private string m_INV_ADJUST_DESC = string.Empty;
        private DateTime? m_PHYSICAL_COUNT_DATE = null;
        private decimal m_PHYSICAL_COUNT_BY = 0;
        private string m_APPROVE_BY = string.Empty;
        private DateTime? m_APPROVE_DATE = null;
        private int m_CREATE_BY = 0;
        private DateTime? m_CREATE_DATE = null;
        private string m_REMARKS = string.Empty;
        private string m_DOC_REF_NO = string.Empty;
        private int m_DEPARTMENT_ID = 0;
        private string m_AUTH_STATUS = string.Empty;
        private int m_COMPANY_ID = 0;

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


        [DBColumn(Name = "INV_ADJUST_ID", Storage = "m_INV_ADJUST_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int INV_ADJUST_ID
        {
            get { return this.m_INV_ADJUST_ID; }
            set
            {
                this.m_INV_ADJUST_ID = value;
                this.NotifyPropertyChanged("INV_ADJUST_ID");
            }
        }

        [DBColumn(Name = "INV_ADJUST_NO", Storage = "m_INV_ADJUST_NO", DbType = "126")]
        public string INV_ADJUST_NO
        {
            get { return this.m_INV_ADJUST_NO; }
            set
            {
                this.m_INV_ADJUST_NO = value;
                this.NotifyPropertyChanged("INV_ADJUST_NO");
            }
        }

        [DBColumn(Name = "INV_ADJUST_DATE", Storage = "m_INV_ADJUST_DATE", DbType = "106")]
        public DateTime? INV_ADJUST_DATE
        {
            get { return this.m_INV_ADJUST_DATE; }
            set
            {
                this.m_INV_ADJUST_DATE = value;
                this.NotifyPropertyChanged("INV_ADJUST_DATE");
            }
        }

        [DBColumn(Name = "INV_ADJUST_DESC", Storage = "m_INV_ADJUST_DESC", DbType = "126")]
        public string INV_ADJUST_DESC
        {
            get { return this.m_INV_ADJUST_DESC; }
            set
            {
                this.m_INV_ADJUST_DESC = value;
                this.NotifyPropertyChanged("INV_ADJUST_DESC");
            }
        }

        [DBColumn(Name = "PHYSICAL_COUNT_DATE", Storage = "m_PHYSICAL_COUNT_DATE", DbType = "106")]
        public DateTime? PHYSICAL_COUNT_DATE
        {
            get { return this.m_PHYSICAL_COUNT_DATE; }
            set
            {
                this.m_PHYSICAL_COUNT_DATE = value;
                this.NotifyPropertyChanged("PHYSICAL_COUNT_DATE");
            }
        }

        [DBColumn(Name = "PHYSICAL_COUNT_BY", Storage = "m_PHYSICAL_COUNT_BY", DbType = "107")]
        public decimal PHYSICAL_COUNT_BY
        {
            get { return this.m_PHYSICAL_COUNT_BY; }
            set
            {
                this.m_PHYSICAL_COUNT_BY = value;
                this.NotifyPropertyChanged("PHYSICAL_COUNT_BY");
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

        [DBColumn(Name = "DOC_REF_NO", Storage = "m_DOC_REF_NO", DbType = "126")]
        public string DOC_REF_NO
        {
            get { return this.m_DOC_REF_NO; }
            set
            {
                this.m_DOC_REF_NO = value;
                this.NotifyPropertyChanged("DOC_REF_NO");
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

        #endregion //properties
    }

     public partial class dcINV_ADJUST_DEPT_MASTER_TEMP
     {
         private string m_FROM_DEPT_NAME = string.Empty;
         public string FROM_DEPT_NAME
         {
             get { return m_FROM_DEPT_NAME; }
             set { this.m_FROM_DEPT_NAME = value; }
         }

         private string m_CREATE_BY_NAME = string.Empty;
         public string CREATE_BY_NAME
         {
             get { return m_CREATE_BY_NAME; }
             set { this.m_CREATE_BY_NAME = value; }
         }

         private string m_ITEM_NAME = string.Empty;
         public string ITEM_NAME
         {
             get { return m_ITEM_NAME; }
             set { this.m_ITEM_NAME = value; }
         }

         
     }
}
