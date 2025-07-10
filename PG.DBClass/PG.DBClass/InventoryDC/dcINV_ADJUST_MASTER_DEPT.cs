using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
    [DBTable(Name = "INV_ADJUST_MASTER_DEPT")]
    public partial class dcINV_ADJUST_MASTER_DEPT : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private Int64 m_INV_ADJUST_ID = 0;
        private string m_INV_ADJUST_NO = string.Empty;
        private DateTime? m_INV_ADJUST_DATE = null;
        private string m_INV_ADJUST_DESC = string.Empty;
        private DateTime? m_PHYSICAL_COUNT_DATE = null;
        private decimal m_PHYSICAL_COUNT_BY = 0;
        private string m_APPROVE_BY = string.Empty;
        private DateTime? m_APPROVE_DATE = null;
        private int m_ENTRY_BY = 0;
        private DateTime? m_ENTRY_DATE = null;
        private string m_REMARKS = string.Empty;
        private string m_DOC_REF_NO = string.Empty;
        private int m_DEPARTMENT_ID = 0;
        private int m_CREATE_BY = 0;
        private DateTime? m_CREATE_DATE = null;

        private string m_AUTH_STATUS = "U";
        private int m_STLM_ID = 0;
        private string m_ADJUSTMENT_TYPE = string.Empty;
        

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
        public Int64 INV_ADJUST_ID
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


        [DBColumn(Name = "DEPARTMENT_ID", Storage = "m_DEPARTMENT_ID", DbType = "126")]
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

        [DBColumn(Name = "STLM_ID", Storage = "m_STLM_ID", DbType = "126")]
        public int STLM_ID
        {
            get { return this.m_STLM_ID; }
            set
            {
                this.m_STLM_ID = value;
                this.NotifyPropertyChanged("STLM_ID");
            }
        }

        [DBColumn(Name = "ADJUSTMENT_TYPE", Storage = "m_ADJUSTMENT_TYPE", DbType = "126")]
        public string ADJUSTMENT_TYPE
        {
            get { return this.m_ADJUSTMENT_TYPE; }
            set
            {
                this.m_ADJUSTMENT_TYPE = value;
                this.NotifyPropertyChanged("ADJUSTMENT_TYPE");
            }
        }



        #endregion //properties
    }


    public partial class dcINV_ADJUST_MASTER_DEPT
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



    }

}
