using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "PROD_CMP_CRUSHING_PP_MST")]
    public partial class dcPROD_CMP_CRUSHING_PP_MST : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_PROD_CMP_CRUS_ID = 0;
        private string m_PROD_CMP_CRUS_NO = string.Empty;
        private DateTime? m_PROD_CMP_CRUS_DATE = null;
        private int m_DEPT_ID = 0;
        private string m_CMP_CRUS_REMARKS = string.Empty;
        private string m_CREATE_BY = string.Empty;
        private DateTime? m_CREATE_DATE = null;
        private string m_UPDATE_BY = string.Empty;
        private DateTime? m_UPDATE_DATE = null;
        private string m_AUTHO_STATUS = string.Empty;
        private DateTime? m_AUTHO_DATE = null;
        private string m_REC_STATUS = string.Empty;
        private string m_AUTHO_BY = string.Empty;
        private string m_SHIFT_ID = string.Empty;
        private decimal m_STLM_ID = 0;
        private string m_SUPERVISOR_ID = string.Empty;
        private string m_ENTRY_BY_REF_ID = string.Empty;
        private string m_WITH_BOM = string.Empty;
        private string m_PROD_BATCH_NO = string.Empty;


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


        [DBColumn(Name = "PROD_CMP_CRUS_ID", Storage = "m_PROD_CMP_CRUS_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int PROD_CMP_CRUS_ID
        {
            get { return this.m_PROD_CMP_CRUS_ID; }
            set
            {
                this.m_PROD_CMP_CRUS_ID = value;
                this.NotifyPropertyChanged("PROD_CMP_CRUS_ID");
            }
        }

        [DBColumn(Name = "PROD_CMP_CRUS_NO", Storage = "m_PROD_CMP_CRUS_NO", DbType = "126")]
        public string PROD_CMP_CRUS_NO
        {
            get { return this.m_PROD_CMP_CRUS_NO; }
            set
            {
                this.m_PROD_CMP_CRUS_NO = value;
                this.NotifyPropertyChanged("PROD_CMP_CRUS_NO");
            }
        }

        [DBColumn(Name = "PROD_CMP_CRUS_DATE", Storage = "m_PROD_CMP_CRUS_DATE", DbType = "106")]
        public DateTime? PROD_CMP_CRUS_DATE
        {
            get { return this.m_PROD_CMP_CRUS_DATE; }
            set
            {
                this.m_PROD_CMP_CRUS_DATE = value;
                this.NotifyPropertyChanged("PROD_CMP_CRUS_DATE");
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

        [DBColumn(Name = "CMP_CRUS_REMARKS", Storage = "m_CMP_CRUS_REMARKS", DbType = "126")]
        public string CMP_CRUS_REMARKS
        {
            get { return this.m_CMP_CRUS_REMARKS; }
            set
            {
                this.m_CMP_CRUS_REMARKS = value;
                this.NotifyPropertyChanged("CMP_CRUS_REMARKS");
            }
        }

        [DBColumn(Name = "CREATE_BY", Storage = "m_CREATE_BY", DbType = "126")]
        public string CREATE_BY
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

        [DBColumn(Name = "UPDATE_BY", Storage = "m_UPDATE_BY", DbType = "126")]
        public string UPDATE_BY
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

        [DBColumn(Name = "AUTHO_STATUS", Storage = "m_AUTHO_STATUS", DbType = "126")]
        public string AUTHO_STATUS
        {
            get { return this.m_AUTHO_STATUS; }
            set
            {
                this.m_AUTHO_STATUS = value;
                this.NotifyPropertyChanged("AUTHO_STATUS");
            }
        }

        [DBColumn(Name = "AUTHO_DATE", Storage = "m_AUTHO_DATE", DbType = "106")]
        public DateTime? AUTHO_DATE
        {
            get { return this.m_AUTHO_DATE; }
            set
            {
                this.m_AUTHO_DATE = value;
                this.NotifyPropertyChanged("AUTHO_DATE");
            }
        }

        [DBColumn(Name = "REC_STATUS", Storage = "m_REC_STATUS", DbType = "126")]
        public string REC_STATUS
        {
            get { return this.m_REC_STATUS; }
            set
            {
                this.m_REC_STATUS = value;
                this.NotifyPropertyChanged("REC_STATUS");
            }
        }

        [DBColumn(Name = "AUTHO_BY", Storage = "m_AUTHO_BY", DbType = "126")]
        public string AUTHO_BY
        {
            get { return this.m_AUTHO_BY; }
            set
            {
                this.m_AUTHO_BY = value;
                this.NotifyPropertyChanged("AUTHO_BY");
            }
        }

        [DBColumn(Name = "SHIFT_ID", Storage = "m_SHIFT_ID", DbType = "126")]
        public string SHIFT_ID
        {
            get { return this.m_SHIFT_ID; }
            set
            {
                this.m_SHIFT_ID = value;
                this.NotifyPropertyChanged("SHIFT_ID");
            }
        }

        [DBColumn(Name = "STLM_ID", Storage = "m_STLM_ID", DbType = "107")]
        public decimal STLM_ID
        {
            get { return this.m_STLM_ID; }
            set
            {
                this.m_STLM_ID = value;
                this.NotifyPropertyChanged("STLM_ID");
            }
        }

        [DBColumn(Name = "SUPERVISOR_ID", Storage = "m_SUPERVISOR_ID", DbType = "126")]
        public string SUPERVISOR_ID
        {
            get { return this.m_SUPERVISOR_ID; }
            set
            {
                this.m_SUPERVISOR_ID = value;
                this.NotifyPropertyChanged("SUPERVISOR_ID");
            }
        }

        [DBColumn(Name = "ENTRY_BY_REF_ID", Storage = "m_ENTRY_BY_REF_ID", DbType = "126")]
        public string ENTRY_BY_REF_ID
        {
            get { return this.m_ENTRY_BY_REF_ID; }
            set
            {
                this.m_ENTRY_BY_REF_ID = value;
                this.NotifyPropertyChanged("ENTRY_BY_REF_ID");
            }
        }

        [DBColumn(Name = "WITH_BOM", Storage = "m_WITH_BOM", DbType = "126")]
        public string WITH_BOM
        {
            get { return this.m_WITH_BOM; }
            set
            {
                this.m_WITH_BOM = value;
                this.NotifyPropertyChanged("WITH_BOM");
            }
        }

        [DBColumn(Name = "PROD_BATCH_NO", Storage = "m_PROD_BATCH_NO", DbType = "126")]
        public string PROD_BATCH_NO
        {
            get { return this.m_PROD_BATCH_NO; }
            set
            {
                this.m_PROD_BATCH_NO = value;
                this.NotifyPropertyChanged("PROD_BATCH_NO");
            }
        }

        #endregion //properties
    }

    public partial class dcPROD_CMP_CRUSHING_PP_MST
    {

        private List<dcPROD_CMP_CRUSHING_PP_DTL> m_PROD_CMP_CRUSHING_PP_DTLList = null;


        public List<dcPROD_CMP_CRUSHING_PP_DTL> PROD_CMP_CRUSHING_PP_DTLList
        {
            get { return m_PROD_CMP_CRUSHING_PP_DTLList; }
            set { m_PROD_CMP_CRUSHING_PP_DTLList = value; }
        }

        private List<dcPROD_REJECTION_CLOSING> m_listClosingDetails = null;

        public List<dcPROD_REJECTION_CLOSING> listClosingDetails
        {
            get { return m_listClosingDetails; }
            set { m_listClosingDetails = value; }
        }

        public bool IS_SELECTED { get; set; }

        private string m_FULL_NAME = "";
        private string m_DEPARTMENT_NAME = "";
        private string m_SHIFT_NAME = "";
        private string m_FORECUSTMONTH = "";
        private string m_FORECUSTYEAR = "";
        private string m_SUPERVISOR_NAME = "";
        private string m_SHIFT_INCHARGE_NAME = "";

        private string m_MACHINE_NAME = "";
        public string MACHINE_NAME
        {
            get { return m_MACHINE_NAME; }
            set { m_MACHINE_NAME = value; }
        }

        public string SHIFT_INCHARGE_NAME
        {
            get { return m_SHIFT_INCHARGE_NAME; }
            set { m_SHIFT_INCHARGE_NAME = value; }
        }
        public string FULL_NAME
        {
            get { return m_FULL_NAME; }
            set { m_FULL_NAME = value; }
        }

        public string DEPARTMENT_NAME
        {
            get { return m_DEPARTMENT_NAME; }
            set { m_DEPARTMENT_NAME = value; }
        }


        public string SHIFT_NAME
        {
            get { return m_SHIFT_NAME; }
            set { m_SHIFT_NAME = value; }
        }



        public string UN_LOAD_PROD_NO { get; set; }
        public string UN_LOAD_AUTH_STATUS { get; set; }


        public string FORECUSTMONTH
        {
            get { return m_FORECUSTMONTH; }
            set { m_FORECUSTMONTH = value; }
        }

        public string FORECUSTYEAR
        {
            get { return m_FORECUSTYEAR; }
            set { m_FORECUSTYEAR = value; }
        }

        private string m_ENTRY_BY = "";

        public string ENTRY_BY
        {
            get { return m_ENTRY_BY; }
            set { m_ENTRY_BY = value; }
        }

        public string PROD_QA_NO { get; set; }
        public string IS_UNLOADED { get; set; }
        public int? m_UN_LOADED_PROD_ID = 0;
        public int? UN_LOADED_PROD_ID
        {
            get { return this.m_UN_LOADED_PROD_ID; }
            set
            {
                this.m_UN_LOADED_PROD_ID = value;
            }
        }
        public string ENTRY_BY_REF_NAME { get; set; }
    }
}
