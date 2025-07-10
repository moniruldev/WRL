using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "LC_TYPE_MASTER")]
    public partial class dcLC_TYPE_MASTER : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private decimal m_LC_TYPE_ID = 0;
        private string m_LC_TYPE_DESC = string.Empty;
        private string m_REMARKS = string.Empty;
        private decimal m_ENTRY_BY = 0;
        private DateTime? m_ENTRY_DATE = null;
        private string m_IS_ACTIVE = string.Empty;
        private string m_LC_TYPE_CODE = string.Empty;

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


        [DBColumn(Name = "LC_TYPE_ID", Storage = "m_LC_TYPE_ID", DbType = "107", IsPrimaryKey = true)]
        public decimal LC_TYPE_ID
        {
            get { return this.m_LC_TYPE_ID; }
            set
            {
                this.m_LC_TYPE_ID = value;
                this.NotifyPropertyChanged("LC_TYPE_ID");
            }
        }

        [DBColumn(Name = "LC_TYPE_DESC", Storage = "m_LC_TYPE_DESC", DbType = "126")]
        public string LC_TYPE_DESC
        {
            get { return this.m_LC_TYPE_DESC; }
            set
            {
                this.m_LC_TYPE_DESC = value;
                this.NotifyPropertyChanged("LC_TYPE_DESC");
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

        [DBColumn(Name = "IS_ACTIVE", Storage = "m_IS_ACTIVE", DbType = "126")]
        public string IS_ACTIVE
        {
            get { return this.m_IS_ACTIVE; }
            set
            {
                this.m_IS_ACTIVE = value;
                this.NotifyPropertyChanged("IS_ACTIVE");
            }
        }

        [DBColumn(Name = "LC_TYPE_CODE", Storage = "m_LC_TYPE_CODE", DbType = "126")]
        public string LC_TYPE_CODE
        {
            get { return this.m_LC_TYPE_CODE; }
            set
            {
                this.m_LC_TYPE_CODE = value;
                this.NotifyPropertyChanged("LC_TYPE_CODE");
            }
        }

        #endregion //properties
    }
}
