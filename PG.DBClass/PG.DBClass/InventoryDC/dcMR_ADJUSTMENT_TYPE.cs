using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
    [DBTable(Name = "MR_ADJUSTMENT_TYPE")]
    public partial class dcMR_ADJUSTMENT_TYPE : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private decimal m_MR_ADJUST_TYPE_ID = 0;
        private string m_ADJUST_TYPE_ID = string.Empty;
        private string m_ADJUST_TYPE_DESC = string.Empty;
        private string m_DR_COA_CODE = string.Empty;
        private string m_FIELD2 = string.Empty;
        private string m_FIELD3 = string.Empty;
        private string m_CREATE_BY = string.Empty;
        private DateTime? m_CREATE_DATE = null;
        private string m_UPDATE_BY = string.Empty;
        private DateTime? m_UPDATE_DATE = null;
        private string m_AUTHO_BY = string.Empty;
        private DateTime? m_AUTHO_DATE = null;
        private string m_REC_STATUS = string.Empty;

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


        [DBColumn(Name = "MR_ADJUST_TYPE_ID", Storage = "m_MR_ADJUST_TYPE_ID", DbType = "107", IsPrimaryKey = true)]
        public decimal MR_ADJUST_TYPE_ID
        {
            get { return this.m_MR_ADJUST_TYPE_ID; }
            set
            {
                this.m_MR_ADJUST_TYPE_ID = value;
                this.NotifyPropertyChanged("MR_ADJUST_TYPE_ID");
            }
        }

        [DBColumn(Name = "ADJUST_TYPE_ID", Storage = "m_ADJUST_TYPE_ID", DbType = "126")]
        public string ADJUST_TYPE_ID
        {
            get { return this.m_ADJUST_TYPE_ID; }
            set
            {
                this.m_ADJUST_TYPE_ID = value;
                this.NotifyPropertyChanged("ADJUST_TYPE_ID");
            }
        }

        [DBColumn(Name = "ADJUST_TYPE_DESC", Storage = "m_ADJUST_TYPE_DESC", DbType = "126")]
        public string ADJUST_TYPE_DESC
        {
            get { return this.m_ADJUST_TYPE_DESC; }
            set
            {
                this.m_ADJUST_TYPE_DESC = value;
                this.NotifyPropertyChanged("ADJUST_TYPE_DESC");
            }
        }

        [DBColumn(Name = "DR_COA_CODE", Storage = "m_DR_COA_CODE", DbType = "126")]
        public string DR_COA_CODE
        {
            get { return this.m_DR_COA_CODE; }
            set
            {
                this.m_DR_COA_CODE = value;
                this.NotifyPropertyChanged("DR_COA_CODE");
            }
        }

        [DBColumn(Name = "FIELD2", Storage = "m_FIELD2", DbType = "126")]
        public string FIELD2
        {
            get { return this.m_FIELD2; }
            set
            {
                this.m_FIELD2 = value;
                this.NotifyPropertyChanged("FIELD2");
            }
        }

        [DBColumn(Name = "FIELD3", Storage = "m_FIELD3", DbType = "126")]
        public string FIELD3
        {
            get { return this.m_FIELD3; }
            set
            {
                this.m_FIELD3 = value;
                this.NotifyPropertyChanged("FIELD3");
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

        #endregion //properties
    }
}
