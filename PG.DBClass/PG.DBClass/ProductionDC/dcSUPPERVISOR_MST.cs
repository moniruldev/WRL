using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "SUPPERVISOR_MST")]
    public partial class dcSUPPERVISOR_MST : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_SUPPERVISOR_MSTID = 0;
        private string m_EMP_ID = "";
        private string m_FULL_NAME = string.Empty;
        private int m_DEPT_ID = 0;
        private int m_DESIGNATION_ID = 0;
        private string m_DESIGNATION_NAME = string.Empty;
        private string m_ISACTIVE = string.Empty;
        private string m_ISOPERATOR = string.Empty;

        private int m_ENTRY_BY = 0;
        private DateTime? m_ENTRY_DATE = null;
        private int m_EDIT_BY = 0;
        private DateTime? m_EDIT_DATE = null;
        private string m_DEPT_NAME = String.Empty;

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


        [DBColumn(Name = "SUPPERVISOR_MSTID", Storage = "m_SUPPERVISOR_MSTID", DbType = "107", IsPrimaryKey = true, IsDbGenerated = true, IsIdentity = true)]
        public int SUPPERVISOR_MSTID
        {
            get { return this.m_SUPPERVISOR_MSTID; }
            set
            {
                this.m_SUPPERVISOR_MSTID = value;
                this.NotifyPropertyChanged("SUPPERVISOR_MSTID");
            }
        }

        [DBColumn(Name = "EMP_ID", Storage = "m_EMP_ID", DbType = "107")]
        public string EMP_ID
        {
            get { return this.m_EMP_ID; }
            set
            {
                this.m_EMP_ID = value;
                this.NotifyPropertyChanged("EMP_ID");
            }
        }

        [DBColumn(Name = "FULL_NAME", Storage = "m_FULL_NAME", DbType = "126")]
        public string FULL_NAME
        {
            get { return this.m_FULL_NAME; }
            set
            {
                this.m_FULL_NAME = value;
                this.NotifyPropertyChanged("FULL_NAME");
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

        [DBColumn(Name = "DESIGNATION_ID", Storage = "m_DESIGNATION_ID", DbType = "107")]
        public int DESIGNATION_ID
        {
            get { return this.m_DESIGNATION_ID; }
            set
            {
                this.m_DESIGNATION_ID = value;
                this.NotifyPropertyChanged("DESIGNATION_ID");
            }
        }

        [DBColumn(Name = "DESIGNATION_NAME", Storage = "m_DESIGNATION_NAME", DbType = "126")]
        public string DESIGNATION_NAME
        {
            get { return this.m_DESIGNATION_NAME; }
            set
            {
                this.m_DESIGNATION_NAME = value;
                this.NotifyPropertyChanged("DESIGNATION_NAME");
            }
        }

        [DBColumn(Name = "ISACTIVE", Storage = "m_ISACTIVE", DbType = "126")]
        public string ISACTIVE
        {
            get { return this.m_ISACTIVE; }
            set
            {
                this.m_ISACTIVE = value;
                this.NotifyPropertyChanged("ISACTIVE");
            }
        }


        [DBColumn(Name = "ISOPERATOR", Storage = "m_ISOPERATOR", DbType = "126")]
        public string ISOPERATOR
        {
            get { return this.m_ISOPERATOR; }
            set
            {
                this.m_ISOPERATOR = value;
                this.NotifyPropertyChanged("ISOPERATOR");
            }
        }

       

        [DBColumn(Name = "ENTRY_BY", Storage = "m_ENTRY_BY", DbType = "107")]
        public int ENTRY_BY
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

        [DBColumn(Name = "EDIT_BY", Storage = "m_EDIT_BY", DbType = "107")]
        public int EDIT_BY
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


        [DBColumn(Name = "DEPT_NAME", Storage = "m_DEPT_NAME", DbType = "126")]
        public string DEPT_NAME
        {
            get { return this.m_DEPT_NAME; }
            set
            {
                this.m_DEPT_NAME = value;
                this.NotifyPropertyChanged("DEPT_NAME");
            }
        }

        #endregion //properties
    }

     public partial class dcSUPPERVISOR_MST
     {
         public int MACHINE_ID { get; set; }
         public int REF_MACHINE_ID { get; set; }
     }
}
