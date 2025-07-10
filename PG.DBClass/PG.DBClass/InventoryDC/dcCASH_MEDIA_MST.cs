using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
    [DBTable(Name = "CASH_MEDIA_MST")]
    public partial class dcCASH_MEDIA_MST : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private decimal m_CM_ID = 0;
        private string m_CODE = string.Empty;
        private string m_DESCRIPTION = string.Empty;
        private string m_ISCASH = string.Empty;
        private string m_ISCHEQUE = string.Empty;
        private string m_ISONLINE = string.Empty;

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


        [DBColumn(Name = "CM_ID", Storage = "m_CM_ID", DbType = "107", IsPrimaryKey = true)]
        public decimal CM_ID
        {
            get { return this.m_CM_ID; }
            set
            {
                this.m_CM_ID = value;
                this.NotifyPropertyChanged("CM_ID");
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

        [DBColumn(Name = "DESCRIPTION", Storage = "m_DESCRIPTION", DbType = "126")]
        public string DESCRIPTION
        {
            get { return this.m_DESCRIPTION; }
            set
            {
                this.m_DESCRIPTION = value;
                this.NotifyPropertyChanged("DESCRIPTION");
            }
        }

        [DBColumn(Name = "ISCASH", Storage = "m_ISCASH", DbType = "126")]
        public string ISCASH
        {
            get { return this.m_ISCASH; }
            set
            {
                this.m_ISCASH = value;
                this.NotifyPropertyChanged("ISCASH");
            }
        }

        [DBColumn(Name = "ISCHEQUE", Storage = "m_ISCHEQUE", DbType = "126")]
        public string ISCHEQUE
        {
            get { return this.m_ISCHEQUE; }
            set
            {
                this.m_ISCHEQUE = value;
                this.NotifyPropertyChanged("ISCHEQUE");
            }
        }

        [DBColumn(Name = "ISONLINE", Storage = "m_ISONLINE", DbType = "126")]
        public string ISONLINE
        {
            get { return this.m_ISONLINE; }
            set
            {
                this.m_ISONLINE = value;
                this.NotifyPropertyChanged("ISONLINE");
            }
        }

        #endregion //properties
    }
}
