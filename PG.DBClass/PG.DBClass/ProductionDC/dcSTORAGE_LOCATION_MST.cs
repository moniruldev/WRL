using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "STORAGE_LOCATION_MST")]
    public partial class dcSTORAGE_LOCATION_MST : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private decimal m_STLM_ID = 0;
        private string m_CODE = string.Empty;
        private string m_NAME = string.Empty;
        private string m_REMARKS = string.Empty;
        private decimal m_DEPT_ID = 0;
        private string m_SHORT_CODE = string.Empty;
        private string m_IS_BATCH_USABLE = string.Empty;

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


        [DBColumn(Name = "STLM_ID", Storage = "m_STLM_ID", DbType = "107", IsPrimaryKey = true)]
        public decimal STLM_ID
        {
            get { return this.m_STLM_ID; }
            set
            {
                this.m_STLM_ID = value;
                this.NotifyPropertyChanged("STLM_ID");
            }
        }

        [DBColumn(Name = "CODE", Storage = "m_CODE", DbType = "126")]
        public string CODE
        {
            get { return this.m_CODE; }
            set
            {
                this.m_CODE = value;
                this.NotifyPropertyChanged("CODE");
            }
        }

        [DBColumn(Name = "NAME", Storage = "m_NAME", DbType = "126")]
        public string NAME
        {
            get { return this.m_NAME; }
            set
            {
                this.m_NAME = value;
                this.NotifyPropertyChanged("NAME");
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

        [DBColumn(Name = "SHORT_CODE", Storage = "m_SHORT_CODE", DbType = "126")]
        public string SHORT_CODE
        {
            get { return this.m_SHORT_CODE; }
            set
            {
                this.m_SHORT_CODE = value;
                this.NotifyPropertyChanged("SHORT_CODE");
            }
        }

        [DBColumn(Name = "IS_BATCH_USABLE", Storage = "m_IS_BATCH_USABLE", DbType = "126")]
        public string IS_BATCH_USABLE
        {
            get { return this.m_IS_BATCH_USABLE; }
            set
            {
                this.m_IS_BATCH_USABLE = value;
                this.NotifyPropertyChanged("IS_BATCH_USABLE");
            }
        }

        #endregion //properties
    }
}
