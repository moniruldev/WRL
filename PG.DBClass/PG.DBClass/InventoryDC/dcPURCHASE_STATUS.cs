using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.InventoryDC
{
    [DBTable(Name = "PURCHASE_STATUS")]
    public partial class dcPURCHASE_STATUS : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_PURCHASE_STATUS_ID = 0;
        private string m_STATUS_NAME = string.Empty;
        private string m_CREATE_BY = string.Empty;
        private DateTime? m_CREATE_DATE = null;
        private string m_IS_ACTIVE = string.Empty;
        private int m_SL_NO = 0;

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


        [DBColumn(Name = "PURCHASE_STATUS_ID", Storage = "m_PURCHASE_STATUS_ID", DbType = "107", IsPrimaryKey = true)]
        public int PURCHASE_STATUS_ID
        {
            get { return this.m_PURCHASE_STATUS_ID; }
            set
            {
                this.m_PURCHASE_STATUS_ID = value;
                this.NotifyPropertyChanged("PURCHASE_STATUS_ID");
            }
        }

        [DBColumn(Name = "STATUS_NAME", Storage = "m_STATUS_NAME", DbType = "126")]
        public string STATUS_NAME
        {
            get { return this.m_STATUS_NAME; }
            set
            {
                this.m_STATUS_NAME = value;
                this.NotifyPropertyChanged("STATUS_NAME");
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

        [DBColumn(Name = "SL_NO", Storage = "m_SL_NO", DbType = "107")]
        public int SL_NO
        {
            get { return this.m_SL_NO; }
            set
            {
                this.m_SL_NO = value;
                this.NotifyPropertyChanged("SL_NO");
            }
        }

        #endregion //properties
    }
}
