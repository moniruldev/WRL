using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "CAST_LEAD_REPROCESS_MST")]
    public partial class dcCAST_LEAD_REPROCESS_MST : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_LEAD_PROCESS_ID = 0;
        private string m_LEAD_PROCESS_NO = string.Empty;
        private int m_DEPARTMENT_ID = 0;
        private DateTime? m_PROCESS_DATE = null;
        private string m_CREATE_BY = string.Empty;
        private DateTime? m_CREATE_DATE = null;
        private string m_EDIT_BY = string.Empty;
        private DateTime? m_EDIT_DATE = null;
        private string m_AUTH_STATUS = string.Empty;
        private DateTime? m_AUTHO_DATE = null;
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


        [DBColumn(Name = "LEAD_PROCESS_ID", Storage = "m_LEAD_PROCESS_ID", DbType = "107", IsPrimaryKey = true)]
        public int LEAD_PROCESS_ID
        {
            get { return this.m_LEAD_PROCESS_ID; }
            set
            {
                this.m_LEAD_PROCESS_ID = value;
                this.NotifyPropertyChanged("LEAD_PROCESS_ID");
            }
        }

        [DBColumn(Name = "LEAD_PROCESS_NO", Storage = "m_LEAD_PROCESS_NO", DbType = "126", IsPrimaryKey = true)]
        public string LEAD_PROCESS_NO
        {
            get { return this.m_LEAD_PROCESS_NO; }
            set
            {
                this.m_LEAD_PROCESS_NO = value;
                this.NotifyPropertyChanged("LEAD_PROCESS_NO");
            }
        }

        [DBColumn(Name = "DEPARTMENT_ID", Storage = "m_DEPARTMENT_ID", DbType = "107")]
        public int DEPARTMENT_ID
        {
            get { return this.m_DEPARTMENT_ID; }
            set
            {
                this.m_DEPARTMENT_ID = value;
                this.NotifyPropertyChanged("DEPARTMENT_ID");
            }
        }

        [DBColumn(Name = "PROCESS_DATE", Storage = "m_PROCESS_DATE", DbType = "106")]
        public DateTime? PROCESS_DATE
        {
            get { return this.m_PROCESS_DATE; }
            set
            {
                this.m_PROCESS_DATE = value;
                this.NotifyPropertyChanged("PROCESS_DATE");
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

        [DBColumn(Name = "EDIT_BY", Storage = "m_EDIT_BY", DbType = "126")]
        public string EDIT_BY
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
        #endregion //properties
    }


     public partial class dcCAST_LEAD_REPROCESS_MST 
     {
         private List<dcCAST_LEAD_REPROCESS_FG_DTL> m_ProductionDetList = null;
         private List<dcCAST_LEAD_REPROCESS_RM_DTL> m_ProductionClosingDetList = null;
         private List<dcCAST_LEAD_REPROCESS_DROSS_DTL> m_DrossDetList = null;
         public List<dcCAST_LEAD_REPROCESS_FG_DTL> ProductionDetList
         {
             get { return m_ProductionDetList; }
             set { m_ProductionDetList = value; }
         }

         public List<dcCAST_LEAD_REPROCESS_RM_DTL> ProductionClosingDetList
         {
             get { return m_ProductionClosingDetList; }
             set { m_ProductionClosingDetList = value; }
         }

         public List<dcCAST_LEAD_REPROCESS_DROSS_DTL> DrossDetList
         {
             get { return m_DrossDetList; }
             set { m_DrossDetList = value; }
         }
     }
    
}
