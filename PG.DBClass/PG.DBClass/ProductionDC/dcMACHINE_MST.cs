using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "MACHINE_MST")]
    public partial class dcMACHINE_MST : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_MACHINE_ID = 0;
        private string m_MACHINE_NAME = string.Empty;
        private string m_MACHINE_DESCRIPTION = string.Empty;
        private decimal m_DEPT_ID = 0;
        private decimal m_ENTRY_BY = 0;
        private DateTime? m_ENTRY_DATE = null;
        private string m_MACHINE_CODE = string.Empty;
        
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


        [DBColumn(Name = "MACHINE_ID", Storage = "m_MACHINE_ID", DbType = "107", IsPrimaryKey = true)]
        public int MACHINE_ID
        {
            get { return this.m_MACHINE_ID; }
            set
            {
                this.m_MACHINE_ID = value;
                this.NotifyPropertyChanged("MACHINE_ID");
            }
        }

        [DBColumn(Name = "MACHINE_NAME", Storage = "m_MACHINE_NAME", DbType = "126")]
        public string MACHINE_NAME
        {
            get { return this.m_MACHINE_NAME; }
            set
            {
                this.m_MACHINE_NAME = value;
                this.NotifyPropertyChanged("MACHINE_NAME");
            }
        }

        [DBColumn(Name = "MACHINE_DESCRIPTION", Storage = "m_MACHINE_DESCRIPTION", DbType = "126")]
        public string MACHINE_DESCRIPTION
        {
            get { return this.m_MACHINE_DESCRIPTION; }
            set
            {
                this.m_MACHINE_DESCRIPTION = value;
                this.NotifyPropertyChanged("MACHINE_DESCRIPTION");
            }
        }

        [DBColumn(Name = "DEPT_ID", Storage = "m_DEPT_ID", DbType = "107")]
        public decimal DEPT_ID
        {
            get { return this.m_DEPT_ID; }
            set
            {
                this.m_DEPT_ID = value;
                this.NotifyPropertyChanged("DEPT_ID");
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

         [DBColumn(Name = "MACHINE_CODE", Storage = "m_MACHINE_CODE", DbType = "126")]
        public string MACHINE_CODE
        {
            get { return this.m_MACHINE_CODE; }
            set
            {
                this.m_MACHINE_CODE = value;
                this.NotifyPropertyChanged("MACHINE_CODE");
            }
        }
        
        #endregion //properties
    }

    public partial class dcMACHINE_MST
    {
        public string OPERATOR_NAME { get; set; }
        public int OPERATOR_ID { get; set; }
        public string OPERATOR_EMP_ID { get; set; }
        public string EMP_ID { get; set; }
    }
}
