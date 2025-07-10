using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "PROD_BATT_BREAKING_MST")]
    public partial class dcPROD_BATT_BREAKING_MST : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_BREAKING_ID = 0;
        private int m_FACTORY_ID = 0;
        private string m_SHIFT_ID = string.Empty;
        private string m_SUPERVISOR_ID = string.Empty;
        private int m_ENTRY_BY_ID = 0;
        private DateTime? m_ENTRY_DATE = null;
        private int m_EDIT_BY_ID = 0;
        private DateTime? m_EDIT_DATE = null;
        private int m_DEPT_ID = 0;
        private int m_FORECUST_ID = 0;
        private decimal m_REJECTED_QTY = 0;
        private string m_REF_NO_MANUAL = string.Empty;
        private DateTime? m_BATCH_STARTTIME = null;
        private DateTime? m_BATCH_ENDTIME = null;
        private string m_AUTH_STATUS = string.Empty;
        private int m_AUTH_BY_ID = 0;
        private DateTime? m_AUTH_DATE = null;
        private string m_STARTTIME = string.Empty;
        private string m_ENDTIME = string.Empty;
        private DateTime? m_BREAKING_DATE = null;
        private decimal m_PROCESS_CODE = 0;
        private string m_BATCH_ID = string.Empty;
        private string m_BREAKING_NO = string.Empty;
        private string m_BATCH_NO = string.Empty;
        private string m_ISSMALLPARTS = string.Empty;
        private string m_ISSULPHATION = string.Empty;
        private string m_SHIFT_INCHARGE = string.Empty;
        private string m_IS_UNLOAD = string.Empty;
        private int m_REF_PROD_ID = 0;
        private string m_IS_PACKING = string.Empty;
        private string m_IS_P_BATTERY = string.Empty;
        private string m_IS_ELECTROLYTE = string.Empty;
        private string m_IS_RECOVERY_PROCESS = string.Empty;
        private string m_REMARKS = string.Empty;
        private string m_ENTRY_BY_REF_ID = string.Empty;
        private int m_QA_PASS_STATUS_ID = 0;
        private string m_IS_QA_PASS = string.Empty;
        private int m_STLM_ID = 0;
        private string m_FORMATION_STATUS = string.Empty;
        private int m_BTY_TYPE_ID = 0;
        private string m_IS_REPAIR = string.Empty;

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


        [DBColumn(Name = "BREAKING_ID", Storage = "m_BREAKING_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int BREAKING_ID
        {
            get { return this.m_BREAKING_ID; }
            set
            {
                this.m_BREAKING_ID = value;
                this.NotifyPropertyChanged("BREAKING_ID");
            }
        }

        [DBColumn(Name = "FACTORY_ID", Storage = "m_FACTORY_ID", DbType = "107")]
        public int FACTORY_ID
        {
            get { return this.m_FACTORY_ID; }
            set
            {
                this.m_FACTORY_ID = value;
                this.NotifyPropertyChanged("FACTORY_ID");
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

        [DBColumn(Name = "SUPERVISOR_ID", Storage = "m_SUPERVISOR_ID", DbType = "119")]
        public string SUPERVISOR_ID
        {
            get { return this.m_SUPERVISOR_ID; }
            set
            {
                this.m_SUPERVISOR_ID = value;
                this.NotifyPropertyChanged("SUPERVISOR_ID");
            }
        }

        [DBColumn(Name = "ENTRY_BY_ID", Storage = "m_ENTRY_BY_ID", DbType = "107")]
        public int ENTRY_BY_ID
        {
            get { return this.m_ENTRY_BY_ID; }
            set
            {
                this.m_ENTRY_BY_ID = value;
                this.NotifyPropertyChanged("ENTRY_BY_ID");
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

        [DBColumn(Name = "EDIT_BY_ID", Storage = "m_EDIT_BY_ID", DbType = "107")]
        public int EDIT_BY_ID
        {
            get { return this.m_EDIT_BY_ID; }
            set
            {
                this.m_EDIT_BY_ID = value;
                this.NotifyPropertyChanged("EDIT_BY_ID");
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

        [DBColumn(Name = "FORECUST_ID", Storage = "m_FORECUST_ID", DbType = "107")]
        public int FORECUST_ID
        {
            get { return this.m_FORECUST_ID; }
            set
            {
                this.m_FORECUST_ID = value;
                this.NotifyPropertyChanged("FORECUST_ID");
            }
        }

        [DBColumn(Name = "REJECTED_QTY", Storage = "m_REJECTED_QTY", DbType = "107")]
        public decimal REJECTED_QTY
        {
            get { return this.m_REJECTED_QTY; }
            set
            {
                this.m_REJECTED_QTY = value;
                this.NotifyPropertyChanged("REJECTED_QTY");
            }
        }

        [DBColumn(Name = "REF_NO_MANUAL", Storage = "m_REF_NO_MANUAL", DbType = "126")]
        public string REF_NO_MANUAL
        {
            get { return this.m_REF_NO_MANUAL; }
            set
            {
                this.m_REF_NO_MANUAL = value;
                this.NotifyPropertyChanged("REF_NO_MANUAL");
            }
        }

        [DBColumn(Name = "BATCH_STARTTIME", Storage = "m_BATCH_STARTTIME", DbType = "106")]
        public DateTime? BATCH_STARTTIME
        {
            get { return this.m_BATCH_STARTTIME; }
            set
            {
                this.m_BATCH_STARTTIME = value;
                this.NotifyPropertyChanged("BATCH_STARTTIME");
            }
        }

        [DBColumn(Name = "BATCH_ENDTIME", Storage = "m_BATCH_ENDTIME", DbType = "106")]
        public DateTime? BATCH_ENDTIME
        {
            get { return this.m_BATCH_ENDTIME; }
            set
            {
                this.m_BATCH_ENDTIME = value;
                this.NotifyPropertyChanged("BATCH_ENDTIME");
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

        [DBColumn(Name = "AUTH_BY_ID", Storage = "m_AUTH_BY_ID", DbType = "107")]
        public int AUTH_BY_ID
        {
            get { return this.m_AUTH_BY_ID; }
            set
            {
                this.m_AUTH_BY_ID = value;
                this.NotifyPropertyChanged("AUTH_BY_ID");
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

        [DBColumn(Name = "STARTTIME", Storage = "m_STARTTIME", DbType = "126")]
        public string STARTTIME
        {
            get { return this.m_STARTTIME; }
            set
            {
                this.m_STARTTIME = value;
                this.NotifyPropertyChanged("STARTTIME");
            }
        }

        [DBColumn(Name = "ENDTIME", Storage = "m_ENDTIME", DbType = "126")]
        public string ENDTIME
        {
            get { return this.m_ENDTIME; }
            set
            {
                this.m_ENDTIME = value;
                this.NotifyPropertyChanged("ENDTIME");
            }
        }

        [DBColumn(Name = "BREAKING_DATE", Storage = "m_BREAKING_DATE", DbType = "106")]
        public DateTime? BREAKING_DATE
        {
            get { return this.m_BREAKING_DATE; }
            set
            {
                this.m_BREAKING_DATE = value;
                this.NotifyPropertyChanged("BREAKING_DATE");
            }
        }

        [DBColumn(Name = "PROCESS_CODE", Storage = "m_PROCESS_CODE", DbType = "107")]
        public decimal PROCESS_CODE
        {
            get { return this.m_PROCESS_CODE; }
            set
            {
                this.m_PROCESS_CODE = value;
                this.NotifyPropertyChanged("PROCESS_CODE");
            }
        }

        [DBColumn(Name = "BATCH_ID", Storage = "m_BATCH_ID", DbType = "126")]
        public string BATCH_ID
        {
            get { return this.m_BATCH_ID; }
            set
            {
                this.m_BATCH_ID = value;
                this.NotifyPropertyChanged("BATCH_ID");
            }
        }

        [DBColumn(Name = "BREAKING_NO", Storage = "m_BREAKING_NO", DbType = "126")]
        public string BREAKING_NO
        {
            get { return this.m_BREAKING_NO; }
            set
            {
                this.m_BREAKING_NO = value;
                this.NotifyPropertyChanged("BREAKING_NO");
            }
        }

        [DBColumn(Name = "BATCH_NO", Storage = "m_BATCH_NO", DbType = "126")]
        public string BATCH_NO
        {
            get { return this.m_BATCH_NO; }
            set
            {
                this.m_BATCH_NO = value;
                this.NotifyPropertyChanged("BATCH_NO");
            }
        }

        [DBColumn(Name = "ISSMALLPARTS", Storage = "m_ISSMALLPARTS", DbType = "126")]
        public string ISSMALLPARTS
        {
            get { return this.m_ISSMALLPARTS; }
            set
            {
                this.m_ISSMALLPARTS = value;
                this.NotifyPropertyChanged("ISSMALLPARTS");
            }
        }

        [DBColumn(Name = "ISSULPHATION", Storage = "m_ISSULPHATION", DbType = "126")]
        public string ISSULPHATION
        {
            get { return this.m_ISSULPHATION; }
            set
            {
                this.m_ISSULPHATION = value;
                this.NotifyPropertyChanged("ISSULPHATION");
            }
        }

        [DBColumn(Name = "SHIFT_INCHARGE", Storage = "m_SHIFT_INCHARGE", DbType = "126")]
        public string SHIFT_INCHARGE
        {
            get { return this.m_SHIFT_INCHARGE; }
            set
            {
                this.m_SHIFT_INCHARGE = value;
                this.NotifyPropertyChanged("SHIFT_INCHARGE");
            }
        }

        [DBColumn(Name = "IS_UNLOAD", Storage = "m_IS_UNLOAD", DbType = "126")]
        public string IS_UNLOAD
        {
            get { return this.m_IS_UNLOAD; }
            set
            {
                this.m_IS_UNLOAD = value;
                this.NotifyPropertyChanged("IS_UNLOAD");
            }
        }

        [DBColumn(Name = "REF_PROD_ID", Storage = "m_REF_PROD_ID", DbType = "107")]
        public int REF_PROD_ID
        {
            get { return this.m_REF_PROD_ID; }
            set
            {
                this.m_REF_PROD_ID = value;
                this.NotifyPropertyChanged("REF_PROD_ID");
            }
        }

        [DBColumn(Name = "IS_PACKING", Storage = "m_IS_PACKING", DbType = "126")]
        public string IS_PACKING
        {
            get { return this.m_IS_PACKING; }
            set
            {
                this.m_IS_PACKING = value;
                this.NotifyPropertyChanged("IS_PACKING");
            }
        }

        [DBColumn(Name = "IS_P_BATTERY", Storage = "m_IS_P_BATTERY", DbType = "126")]
        public string IS_P_BATTERY
        {
            get { return this.m_IS_P_BATTERY; }
            set
            {
                this.m_IS_P_BATTERY = value;
                this.NotifyPropertyChanged("IS_P_BATTERY");
            }
        }

        [DBColumn(Name = "IS_ELECTROLYTE", Storage = "m_IS_ELECTROLYTE", DbType = "126")]
        public string IS_ELECTROLYTE
        {
            get { return this.m_IS_ELECTROLYTE; }
            set
            {
                this.m_IS_ELECTROLYTE = value;
                this.NotifyPropertyChanged("IS_ELECTROLYTE");
            }
        }

        [DBColumn(Name = "IS_RECOVERY_PROCESS", Storage = "m_IS_RECOVERY_PROCESS", DbType = "126")]
        public string IS_RECOVERY_PROCESS
        {
            get { return this.m_IS_RECOVERY_PROCESS; }
            set
            {
                this.m_IS_RECOVERY_PROCESS = value;
                this.NotifyPropertyChanged("IS_RECOVERY_PROCESS");
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

        [DBColumn(Name = "QA_PASS_STATUS_ID", Storage = "m_QA_PASS_STATUS_ID", DbType = "107")]
        public int QA_PASS_STATUS_ID
        {
            get { return this.m_QA_PASS_STATUS_ID; }
            set
            {
                this.m_QA_PASS_STATUS_ID = value;
                this.NotifyPropertyChanged("QA_PASS_STATUS_ID");
            }
        }

        [DBColumn(Name = "IS_QA_PASS", Storage = "m_IS_QA_PASS", DbType = "126")]
        public string IS_QA_PASS
        {
            get { return this.m_IS_QA_PASS; }
            set
            {
                this.m_IS_QA_PASS = value;
                this.NotifyPropertyChanged("IS_QA_PASS");
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

        [DBColumn(Name = "FORMATION_STATUS", Storage = "m_FORMATION_STATUS", DbType = "126")]
        public string FORMATION_STATUS
        {
            get { return this.m_FORMATION_STATUS; }
            set
            {
                this.m_FORMATION_STATUS = value;
                this.NotifyPropertyChanged("FORMATION_STATUS");
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

        #endregion //properties
    }
    public partial class dcPROD_BATT_BREAKING_MST
    {

        private List<dcPROD_BATT_BREAKING_DTL> m_ProductionDetList = null;
        private List<dcPROD_BATT_BREAKING_DTL> m_ProductionPackingDetList = null;
        private List<dcPROD_BATT_BREAKING_CLOSING> m_ProductionClosingDetList = null;
        //private List<dcPROD_PURELEAD_DROSS_DTL> m_dcPROD_PURELEAD_DROSS_DTL = null;
        public List<dcPROD_BATT_BREAKING_DTL> BreakingDetList
        {
            get { return m_ProductionDetList; }
            set { m_ProductionDetList = value; }
        }

        public List<dcPROD_BATT_BREAKING_DTL> ProductionPackingDetList
        {
            get { return m_ProductionPackingDetList; }
            set { m_ProductionPackingDetList = value; }
        }

        //public List<dcPROD_PURELEAD_DROSS_DTL> DrossDetList
        //{
        //    get { return m_dcPROD_PURELEAD_DROSS_DTL; }
        //    set { m_dcPROD_PURELEAD_DROSS_DTL = value; }
        //}

        private List<dcPROD_BATT_BREAKING_DTL> m_ProductionCuttingDetList = null;
        public List<dcPROD_BATT_BREAKING_DTL> ProductionCuttingDetList
        {
            get { return m_ProductionCuttingDetList; }
            set { m_ProductionCuttingDetList = value; }
        }


        private List<dcPROD_BATT_BREAKING_DTL> m_ProductionFillingDetList = null;
        public List<dcPROD_BATT_BREAKING_DTL> ProductionFillingDetList
        {
            get { return m_ProductionFillingDetList; }
            set { m_ProductionFillingDetList = value; }
        }

        private List<dcPROD_BATT_BREAKING_DTL> m_ProductionSulphationDetList = null;
        public List<dcPROD_BATT_BREAKING_DTL> ProductionSulphationDetList
        {
            get { return m_ProductionSulphationDetList; }
            set { m_ProductionSulphationDetList = value; }
        }

        public List<dcPROD_BATT_BREAKING_CLOSING> BreakingClosingDetList
        {
            get { return m_ProductionClosingDetList; }
            set { m_ProductionClosingDetList = value; }
        }

        private List<dcPROD_OPERATOR_LIST> m_PROD_OPERATOR_LIST = null;

        public List<dcPROD_OPERATOR_LIST> PROD_OPERATOR_LIST
        {
            get { return m_PROD_OPERATOR_LIST; }
            set { m_PROD_OPERATOR_LIST = value; }
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
        public string BTY_TYPE_NAME { get; set; }
        public string BTY_TYPE_DESC { get; set; }
        public string STLM_NAME { get; set; }
    }
}
