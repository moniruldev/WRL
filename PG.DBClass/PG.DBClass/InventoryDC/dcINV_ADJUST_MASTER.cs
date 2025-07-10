using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
    [DBTable(Name = "INV_ADJUST_MASTER")]
    public partial class dcINV_ADJUST_MASTER : DBBaseClass, INotifyPropertyChanged
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
        private decimal m_ENTRY_BY = 0;
        private DateTime? m_ENTRY_DATE = null;
        private string m_REMARKS = string.Empty;
        private string m_DOC_REF_NO = string.Empty;
        private string m_AUTH_STATUS = string.Empty;
        private int m_DEPARTMENT_ID = 0;
        private string m_VERIFY_STATUS = string.Empty;
        private string m_VERIFIED_BY = string.Empty;
        private DateTime? m_VERIFIED_DATE = null;
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


        [DBColumn(Name = "INV_ADJUST_ID", Storage = "m_INV_ADJUST_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
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

        [DBColumn(Name = "ENTRY_BY", Storage = "m_ENTRY_BY", DbType = "107")]
        public decimal ENTRY_BY
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

        [DBColumn(Name = "DEPARTMENT_ID", Storage = "m_DEPARTMENT_ID", DbType = "107", IsPrimaryKey = true)]
        public int DEPARTMENT_ID
        {
            get { return this.m_DEPARTMENT_ID; }
            set
            {
                this.m_DEPARTMENT_ID = value;
                this.NotifyPropertyChanged("DEPARTMENT_ID");
            }
        }


        [DBColumn(Name = "VERIFY_STATUS", Storage = "m_VERIFY_STATUS", DbType = "126")]
        public string VERIFY_STATUS
        {
            get { return this.m_VERIFY_STATUS; }
            set
            {
                this.m_VERIFY_STATUS = value;
                this.NotifyPropertyChanged("VERIFY_STATUS");
            }
        }

        [DBColumn(Name = "VERIFIED_BY", Storage = "m_VERIFIED_BY", DbType = "126")]
        public string VERIFIED_BY
        {
            get { return this.m_VERIFIED_BY; }
            set
            {
                this.m_VERIFIED_BY = value;
                this.NotifyPropertyChanged("VERIFIED_BY");
            }
        }


        [DBColumn(Name = "VERIFIED_DATE", Storage = "m_VERIFIED_DATE", DbType = "126")]
        public DateTime? VERIFIED_DATE
        {
            get { return this.m_VERIFIED_DATE; }
            set
            {
                this.m_VERIFIED_DATE = value;
                this.NotifyPropertyChanged("VERIFIED_DATE");
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

    public partial class dcINV_ADJUST_MASTER
    {

        private string m_branch_name = string.Empty;
        private string m_from_department_name = string.Empty;
        private string m_to_department_name = string.Empty;
        private string m_company_name = string.Empty;
        private string m_store_name = string.Empty;
        private string m_required_for = string.Empty;
        private bool m_IsITCComplete = false;
        private string m_create_by_name = string.Empty;
        private bool m_IS_EDIT_ABLE = true;


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

        public bool IS_EDIT_ABLE
        {
            get { return m_IS_EDIT_ABLE; }
            set { this.m_IS_EDIT_ABLE = value; }
        }

        public decimal CLOSING_QTY { get; set; }

    }
}
