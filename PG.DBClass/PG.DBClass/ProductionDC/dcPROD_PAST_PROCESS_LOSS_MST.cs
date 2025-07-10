using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "PROD_PAST_PROCESS_LOSS_MST")]
    public partial class dcPROD_PAST_PROCESS_LOSS_MST : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_LOSS_ID = 0;
        private string m_LOSS_NO = string.Empty;
        private decimal m_DEPT_ID = 0;
        private DateTime? m_LOSS_DATE = null;
        private string m_CREATE_BY = string.Empty;
        private DateTime? m_CREATE_DATE = null;
        private string m_UPDATE_BY = string.Empty;
        private DateTime? m_UPDATE_DATE = null;
        private string m_AUTHO_STATUS = string.Empty;
        private DateTime? m_AUTHO_DATE = null;
        private string m_AUTHO_BY = string.Empty;
        private string m_SHIFT_ID = string.Empty;
        private int m_STLM_ID = 0;
        private string m_IS_RM = string.Empty;

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


        [DBColumn(Name = "LOSS_ID", Storage = "m_LOSS_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int LOSS_ID
        {
            get { return this.m_LOSS_ID; }
            set
            {
                this.m_LOSS_ID = value;
                this.NotifyPropertyChanged("LOSS_ID");
            }
        }

        [DBColumn(Name = "LOSS_NO", Storage = "m_LOSS_NO", DbType = "126")]
        public string LOSS_NO
        {
            get { return this.m_LOSS_NO; }
            set
            {
                this.m_LOSS_NO = value;
                this.NotifyPropertyChanged("LOSS_NO");
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

        [DBColumn(Name = "LOSS_DATE", Storage = "m_LOSS_DATE", DbType = "106")]
        public DateTime? LOSS_DATE
        {
            get { return this.m_LOSS_DATE; }
            set
            {
                this.m_LOSS_DATE = value;
                this.NotifyPropertyChanged("LOSS_DATE");
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
        public int STLM_ID
        {
            get { return this.m_STLM_ID; }
            set
            {
                this.m_STLM_ID = value;
                this.NotifyPropertyChanged("STLM_ID");
            }
        }

        [DBColumn(Name = "IS_RM", Storage = "m_IS_RM", DbType = "126")]
        public string IS_RM
        {
            get { return this.m_IS_RM; }
            set
            {
                this.m_IS_RM = value;
                this.NotifyPropertyChanged("IS_RM");
            }
        }

        #endregion //properties
    }



    public partial class dcPROD_PAST_PROCESS_LOSS_MST
    {
        private List<dcPROD_PAST_PROCESS_LOSS_DTL> m_lossList = null;
        public List<dcPROD_PAST_PROCESS_LOSS_DTL>  lossList
        {
            get { return m_lossList; }
            set { m_lossList = value; }
        }

        public string STLM_NAME { get; set; }
        public string DEPARTMENT_NAME { get; set; }

     

    }
}
