using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "SOLAR_BATT_OPENING_MST")]
    public partial class dcSOLAR_BATT_OPENING_MST : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_OPEN_MST_ID = 0;
        private int m_MONTH = 0;
        private int m_YEAR = 0;
        private int m_ENTRY_ID = 0;
        private DateTime? m_ENTRY_DATE = null;
        private int m_EDIT_ID = 0;
        private DateTime? m_EDIT_DATE = null;
        private int m_AUTHO_ID = 0;
        private DateTime? m_AUTHO_DATE = null;
        private int m_DEPT_ID = 0;
        private string m_AUTH_STATUS = null;
        private DateTime? m_OPENNING_DATE = null;

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


        [DBColumn(Name = "OPEN_MST_ID", Storage = "m_OPEN_MST_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int OPEN_MST_ID
        {
            get { return this.m_OPEN_MST_ID; }
            set
            {
                this.m_OPEN_MST_ID = value;
                this.NotifyPropertyChanged("OPEN_MST_ID");
            }
        }

        [DBColumn(Name = "MONTH", Storage = "m_MONTH", DbType = "107")]
        public int MONTH
        {
            get { return this.m_MONTH; }
            set
            {
                this.m_MONTH = value;
                this.NotifyPropertyChanged("MONTH");
            }
        }

        [DBColumn(Name = "YEAR", Storage = "m_YEAR", DbType = "107")]
        public int YEAR
        {
            get { return this.m_YEAR; }
            set
            {
                this.m_YEAR = value;
                this.NotifyPropertyChanged("YEAR");
            }
        }

        [DBColumn(Name = "ENTRY_ID", Storage = "m_ENTRY_ID", DbType = "107")]
        public int ENTRY_ID
        {
            get { return this.m_ENTRY_ID; }
            set
            {
                this.m_ENTRY_ID = value;
                this.NotifyPropertyChanged("ENTRY_ID");
            }
        }

        [DBColumn(Name = "ENTRY_DATE", Storage = "m_ENTRY_DATE", DbType = "107")]
        public DateTime? ENTRY_DATE
        {
            get { return this.m_ENTRY_DATE; }
            set
            {
                this.m_ENTRY_DATE = value;
                this.NotifyPropertyChanged("ENTRY_DATE");
            }
        }

        [DBColumn(Name = "EDIT_ID", Storage = "m_EDIT_ID", DbType = "107")]
        public int EDIT_ID
        {
            get { return this.m_EDIT_ID; }
            set
            {
                this.m_EDIT_ID = value;
                this.NotifyPropertyChanged("EDIT_ID");
            }
        }

        [DBColumn(Name = "EDIT_DATE", Storage = "m_EDIT_DATE", DbType = "107")]
        public DateTime? EDIT_DATE
        {
            get { return this.m_EDIT_DATE; }
            set
            {
                this.m_EDIT_DATE = value;
                this.NotifyPropertyChanged("EDIT_DATE");
            }
        }

        [DBColumn(Name = "AUTHO_ID", Storage = "m_AUTHO_ID", DbType = "107")]
        public int AUTHO_ID
        {
            get { return this.m_AUTHO_ID; }
            set
            {
                this.m_AUTHO_ID = value;
                this.NotifyPropertyChanged("AUTHO_ID");
            }
        }

       [DBColumn(Name = "AUTH_STATUS", Storage = "m_AUTH_STATUS", DbType = "107")]
        public string AUTH_STATUS
        {
            get { return this.m_AUTH_STATUS; }
            set
            {
                this.m_AUTH_STATUS = value;
                this.NotifyPropertyChanged("AUTH_STATUS");
            }
        }

        [DBColumn(Name = "AUTHO_DATE", Storage = "m_AUTHO_DATE", DbType = "107")]
        public DateTime? AUTHO_DATE
        {
            get { return this.m_AUTHO_DATE; }
            set
            {
                this.m_AUTHO_DATE = value;
                this.NotifyPropertyChanged("AUTHO_DATE");
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



              [DBColumn(Name = "OPENNING_DATE", Storage = "m_OPENNING_DATE", DbType = "107")]
        public DateTime? OPENNING_DATE
        {
            get { return this.m_OPENNING_DATE; }
            set
            {
                this.m_OPENNING_DATE = value;
                this.NotifyPropertyChanged("OPENNING_DATE");
            }
        }
        #endregion //properties
    }
     public partial class dcSOLAR_BATT_OPENING_MST
     {
         public List<dcSOLAR_BATT_OPENING> m_SOLARDetList = null;
     }
}
