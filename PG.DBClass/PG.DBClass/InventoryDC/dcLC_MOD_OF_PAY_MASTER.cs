using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "LC_MOD_OF_PAY_MASTER")]
    public partial class dcLC_MOD_OF_PAY_MASTER : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private decimal m_MOD_PAY_ID = 0;
        private string m_MOD_PAY_DESC = string.Empty;
        private string m_REMARKS = string.Empty;
        private string m_IS_ACTIVE = string.Empty;
        private decimal m_ENTRY_BY = 0;
        private DateTime? m_ENTRY_DATE = null;
        private string m_MOD_PAY_CODE = string.Empty;

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


        [DBColumn(Name = "MOD_PAY_ID", Storage = "m_MOD_PAY_ID", DbType = "107", IsPrimaryKey = true)]
        public decimal MOD_PAY_ID
        {
            get { return this.m_MOD_PAY_ID; }
            set
            {
                this.m_MOD_PAY_ID = value;
                this.NotifyPropertyChanged("MOD_PAY_ID");
            }
        }

        [DBColumn(Name = "MOD_PAY_DESC", Storage = "m_MOD_PAY_DESC", DbType = "126")]
        public string MOD_PAY_DESC
        {
            get { return this.m_MOD_PAY_DESC; }
            set
            {
                this.m_MOD_PAY_DESC = value;
                this.NotifyPropertyChanged("MOD_PAY_DESC");
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

        [DBColumn(Name = "MOD_PAY_CODE", Storage = "m_MOD_PAY_CODE", DbType = "126")]
        public string MOD_PAY_CODE
        {
            get { return this.m_MOD_PAY_CODE; }
            set
            {
                this.m_MOD_PAY_CODE = value;
                this.NotifyPropertyChanged("MOD_PAY_CODE");
            }
        }

        #endregion //properties
    }
}
