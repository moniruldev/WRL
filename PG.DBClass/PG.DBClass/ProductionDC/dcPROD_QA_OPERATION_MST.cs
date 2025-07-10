using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "PROD_QA_OPERATION_MST")]
    public partial class dcPROD_QA_OPERATION_MST : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_PROD_QA_ID = 0;
        private int m_PROD_ID = 0;
        private string m_PROD_NO = string.Empty;
        private string m_PROD_BATCH_NO = string.Empty;
        private string m_PROD_QA_NO = string.Empty;
        private DateTime? m_PROD_QA_DATE = null;
        private string m_PROD_QA_REMARKS = string.Empty;
        private decimal m_QA_STATUS_ID = 0;
        private decimal m_CREATE_BY = 0;
        private DateTime? m_CREATE_DATE = null;
        private decimal m_UPDATE_BY = 0;
        private DateTime? m_UPDATE_DATE = null;
        private DateTime? m_PROD_QA_TIME = null;
        private string m_IT_REMARKS = string.Empty;
        private decimal m_PROD_DEPT_ID = 0;
        private string m_IS_TRANSFER = string.Empty;
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


        [DBColumn(Name = "PROD_QA_ID", Storage = "m_PROD_QA_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int PROD_QA_ID
        {
            get { return this.m_PROD_QA_ID; }
            set
            {
                this.m_PROD_QA_ID = value;
                this.NotifyPropertyChanged("PROD_QA_ID");
            }
        }

        [DBColumn(Name = "PROD_ID", Storage = "m_PROD_ID", DbType = "107")]
        public int PROD_ID
        {
            get { return this.m_PROD_ID; }
            set
            {
                this.m_PROD_ID = value;
                this.NotifyPropertyChanged("PROD_ID");
            }
        }

        [DBColumn(Name = "PROD_NO", Storage = "m_PROD_NO", DbType = "126")]
        public string PROD_NO
        {
            get { return this.m_PROD_NO; }
            set
            {
                this.m_PROD_NO = value;
                this.NotifyPropertyChanged("PROD_NO");
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

        [DBColumn(Name = "PROD_QA_NO", Storage = "m_PROD_QA_NO", DbType = "126")]
        public string PROD_QA_NO
        {
            get { return this.m_PROD_QA_NO; }
            set
            {
                this.m_PROD_QA_NO = value;
                this.NotifyPropertyChanged("PROD_QA_NO");
            }
        }

        [DBColumn(Name = "PROD_QA_DATE", Storage = "m_PROD_QA_DATE", DbType = "106")]
        public DateTime? PROD_QA_DATE
        {
            get { return this.m_PROD_QA_DATE; }
            set
            {
                this.m_PROD_QA_DATE = value;
                this.NotifyPropertyChanged("PROD_QA_DATE");
            }
        }

        [DBColumn(Name = "PROD_QA_REMARKS", Storage = "m_PROD_QA_REMARKS", DbType = "126")]
        public string PROD_QA_REMARKS
        {
            get { return this.m_PROD_QA_REMARKS; }
            set
            {
                this.m_PROD_QA_REMARKS = value;
                this.NotifyPropertyChanged("PROD_QA_REMARKS");
            }
        }

        [DBColumn(Name = "QA_STATUS_ID", Storage = "m_QA_STATUS_ID", DbType = "107")]
        public decimal QA_STATUS_ID
        {
            get { return this.m_QA_STATUS_ID; }
            set
            {
                this.m_QA_STATUS_ID = value;
                this.NotifyPropertyChanged("QA_STATUS_ID");
            }
        }

        [DBColumn(Name = "CREATE_BY", Storage = "m_CREATE_BY", DbType = "107")]
        public decimal CREATE_BY
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

        [DBColumn(Name = "UPDATE_BY", Storage = "m_UPDATE_BY", DbType = "107")]
        public decimal UPDATE_BY
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

        [DBColumn(Name = "PROD_QA_TIME", Storage = "m_PROD_QA_TIME", DbType = "106")]
        public DateTime? PROD_QA_TIME
        {
            get { return this.m_PROD_QA_TIME; }
            set
            {
                this.m_PROD_QA_TIME = value;
                this.NotifyPropertyChanged("PROD_QA_TIME");
            }
        }

        [DBColumn(Name = "IT_REMARKS", Storage = "m_IT_REMARKS", DbType = "126")]
        public string IT_REMARKS
        {
            get { return this.m_IT_REMARKS; }
            set
            {
                this.m_IT_REMARKS = value;
                this.NotifyPropertyChanged("IT_REMARKS");
            }
        }

        [DBColumn(Name = "PROD_DEPT_ID", Storage = "m_PROD_DEPT_ID", DbType = "107")]
        public decimal PROD_DEPT_ID
        {
            get { return this.m_PROD_DEPT_ID; }
            set
            {
                this.m_PROD_DEPT_ID = value;
                this.NotifyPropertyChanged("PROD_DEPT_ID");
            }
        }


         [DBColumn(Name = "IS_TRANSFER", Storage = "m_IS_TRANSFER", DbType = "126")]
        public string IS_TRANSFER
        {
            get { return this.m_IS_TRANSFER; }
            set
            {
                this.m_IS_TRANSFER = value;
                this.NotifyPropertyChanged("IS_TRANSFER");
            }
        }
        
        #endregion //properties
    }

    public partial class dcPROD_QA_OPERATION_MST
    {

        private List<dcPROD_QA_OPERATION_DTL> m_ProductionQADetList = null;

        public List<dcPROD_QA_OPERATION_DTL> ProductionQADetList
        {
            get { return m_ProductionQADetList; }
            set { m_ProductionQADetList = value; }
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
        public string IS_UNLOADED { get; set; }
        public int? m_UN_LOADED_PROD_ID = 0;
        public string FULLNAME { get; set; }

        public string STATUS_NAME { get; set; }
        
        public int? UN_LOADED_PROD_ID
        {
            get { return this.m_UN_LOADED_PROD_ID; }
            set
            {
                this.m_UN_LOADED_PROD_ID = value;
            }
        }
    }
}
