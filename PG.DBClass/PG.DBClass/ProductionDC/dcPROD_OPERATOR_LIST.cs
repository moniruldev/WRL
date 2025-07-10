using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "PROD_OPERATOR_LIST")]
    public partial class dcPROD_OPERATOR_LIST : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_PROD_OP_ID = 0;
        private int m_PROD_ID = 0;
        private int m_OPERATOR_ID = 0;
        private string m_EMP_ID = string.Empty;
        private int m_MACHINE_ID = 0;
        private int m_REF_MACHINE_ID = 0;
        private int m_ITEM_ID = 0;
        private DateTime? m_ENTRY_DATE = null;
        private int m_DEPT_ID = 0;
        private int m_STLM_ID = 0;

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


        [DBColumn(Name = "PROD_OP_ID", Storage = "m_PROD_OP_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int PROD_OP_ID
        {
            get { return this.m_PROD_OP_ID; }
            set
            {
                this.m_PROD_OP_ID = value;
                this.NotifyPropertyChanged("PROD_OP_ID");
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

        [DBColumn(Name = "OPERATOR_ID", Storage = "m_OPERATOR_ID", DbType = "107")]
        public int OPERATOR_ID
        {
            get { return this.m_OPERATOR_ID; }
            set
            {
                this.m_OPERATOR_ID = value;
                this.NotifyPropertyChanged("OPERATOR_ID");
            }
        }

        [DBColumn(Name = "EMP_ID", Storage = "m_EMP_ID", DbType = "126")]
        public string EMP_ID
        {
            get { return this.m_EMP_ID; }
            set
            {
                this.m_EMP_ID = value;
                this.NotifyPropertyChanged("EMP_ID");
            }
        }

        [DBColumn(Name = "MACHINE_ID", Storage = "m_MACHINE_ID", DbType = "107")]
        public int MACHINE_ID
        {
            get { return this.m_MACHINE_ID; }
            set
            {
                this.m_MACHINE_ID = value;
                this.NotifyPropertyChanged("MACHINE_ID");
            }
        }

        [DBColumn(Name = "REF_MACHINE_ID", Storage = "m_REF_MACHINE_ID", DbType = "107")]
        public int REF_MACHINE_ID
        {
            get { return this.m_REF_MACHINE_ID; }
            set
            {
                this.m_REF_MACHINE_ID = value;
                this.NotifyPropertyChanged("REF_MACHINE_ID");
            }
        }

        [DBColumn(Name = "ITEM_ID", Storage = "m_ITEM_ID", DbType = "107")]
        public int ITEM_ID
        {
            get { return this.m_ITEM_ID; }
            set
            {
                this.m_ITEM_ID = value;
                this.NotifyPropertyChanged("ITEM_ID");
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

        #endregion //properties
    }

    public partial class dcPROD_OPERATOR_LIST
    {
        public string MACHINE_NAME { get; set; }
        public string REF_MACHINE_NAME { get; set; }
        public string ITEM_NAME { get; set; }
        public string OPERATOR_NAME { get; set; }
        public int SLNO { get; set; }
        
    }
}
