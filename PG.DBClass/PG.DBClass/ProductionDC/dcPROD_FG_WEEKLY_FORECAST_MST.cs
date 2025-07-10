using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "PROD_FG_WEEKLY_FORECAST_MST")]
    public partial class dcPROD_FG_WEEKLY_FORECAST_MST : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_WK_FC_ID = 0;
        private string m_WK_FC_DESC = string.Empty;
        private int m_FOR_MONTH = 0;
        private int m_FOR_YEAR = 0;
        private DateTime? m_ENTRY_DATE = null;
        private int m_FORCASTE_BY = 0;
        private DateTime? m_EDIT_DATE = null;
        private int m_EDIT_BY = 0;
        private string m_FC_NO = string.Empty;
        private string m_AUTH_STATUS = string.Empty;
        private int m_AUTH_BY = 0;
        private DateTime? m_AUTH_DATE = null;
        private int m_FGFC_TYPE = 0;

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


        [DBColumn(Name = "WK_FC_ID", Storage = "m_WK_FC_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int WK_FC_ID
        {
            get { return this.m_WK_FC_ID; }
            set
            {
                this.m_WK_FC_ID = value;
                this.NotifyPropertyChanged("WK_FC_ID");
            }
        }

        [DBColumn(Name = "WK_FC_DESC", Storage = "m_WK_FC_DESC", DbType = "126")]
        public string WK_FC_DESC
        {
            get { return this.m_WK_FC_DESC; }
            set
            {
                this.m_WK_FC_DESC = value;
                this.NotifyPropertyChanged("WK_FC_DESC");
            }
        }

        [DBColumn(Name = "FOR_MONTH", Storage = "m_FOR_MONTH", DbType = "107")]
        public int FOR_MONTH
        {
            get { return this.m_FOR_MONTH; }
            set
            {
                this.m_FOR_MONTH = value;
                this.NotifyPropertyChanged("FOR_MONTH");
            }
        }

        [DBColumn(Name = "FOR_YEAR", Storage = "m_FOR_YEAR", DbType = "107")]
        public int FOR_YEAR
        {
            get { return this.m_FOR_YEAR; }
            set
            {
                this.m_FOR_YEAR = value;
                this.NotifyPropertyChanged("FOR_YEAR");
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

        [DBColumn(Name = "FORCASTE_BY", Storage = "m_FORCASTE_BY", DbType = "107")]
        public int FORCASTE_BY
        {
            get { return this.m_FORCASTE_BY; }
            set
            {
                this.m_FORCASTE_BY = value;
                this.NotifyPropertyChanged("FORCASTE_BY");
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

        [DBColumn(Name = "FC_NO", Storage = "m_FC_NO", DbType = "126")]
        public string FC_NO
        {
            get { return this.m_FC_NO; }
            set
            {
                this.m_FC_NO = value;
                this.NotifyPropertyChanged("FC_NO");
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

        [DBColumn(Name = "AUTH_BY", Storage = "m_AUTH_BY", DbType = "107")]
        public int AUTH_BY
        {
            get { return this.m_AUTH_BY; }
            set
            {
                this.m_AUTH_BY = value;
                this.NotifyPropertyChanged("AUTH_BY");
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

        [DBColumn(Name = "FGFC_TYPE", Storage = "m_FGFC_TYPE", DbType = "107")]
        public int FGFC_TYPE
        {
            get { return this.m_FGFC_TYPE; }
            set
            {
                this.m_FGFC_TYPE = value;
                this.NotifyPropertyChanged("FGFC_TYPE");
            }
        }

        #endregion //properties
    }

      public partial class dcPROD_FG_WEEKLY_FORECAST_MST
      {
         private List<dcPROD_FG_WEEKLY_FORECAST_DTL>  m_WEEKLY_FORECASTList = null;
          public List<dcPROD_FG_WEEKLY_FORECAST_DTL> WEEKLY_FORECASTList
          {
              get { return m_WEEKLY_FORECASTList; }
              set { m_WEEKLY_FORECASTList = value; }
          }

      }
     

}
