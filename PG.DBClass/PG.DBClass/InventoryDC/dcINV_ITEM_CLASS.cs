using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "INV_ITEM_CLASS")]
   public partial class dcINV_ITEM_CLASS : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private decimal m_ITEM_CLASS_ID = 0;
        private string m_ITEM_CLASS_CODE = string.Empty;
        private string m_ITEM_CLASS_NAME = string.Empty;
        private string m_ITEM_CLASS_DESC = string.Empty;
   
        private string m_CREATE_BY = string.Empty;
        private DateTime? m_CREATE_DATE = null;
        private string m_ITEM_CLASS_TYPE = string.Empty;

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


        [DBColumn(Name = "ITEM_CLASS_ID", Storage = "m_ITEM_CLASS_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public decimal ITEM_CLASS_ID
        {
            get { return this.m_ITEM_CLASS_ID; }
            set
            {
                this.m_ITEM_CLASS_ID = value;
                this.NotifyPropertyChanged("ITEM_CLASS_ID");
            }
        }

        [DBColumn(Name = "ITEM_CLASS_CODE", Storage = "m_ITEM_CLASS_CODE", DbType = "126")]
        public string ITEM_CLASS_CODE
        {
            get { return this.m_ITEM_CLASS_CODE; }
            set
            {
                this.m_ITEM_CLASS_CODE = value;
                this.NotifyPropertyChanged("ITEM_CLASS_CODE");
            }
        }

        [DBColumn(Name = "ITEM_CLASS_NAME", Storage = "m_ITEM_CLASS_NAME", DbType = "126")]
        public string ITEM_CLASS_NAME
        {
            get { return this.m_ITEM_CLASS_NAME; }
            set
            {
                this.m_ITEM_CLASS_NAME = value;
                this.NotifyPropertyChanged("ITEM_CLASS_NAME");
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

        [DBColumn(Name = "ITEM_CLASS_TYPE", Storage = "m_ITEM_CLASS_TYPE", DbType = "126")]
        public string ITEM_CLASS_TYPE
        {
            get { return this.m_ITEM_CLASS_TYPE; }
            set
            {
                this.m_ITEM_CLASS_TYPE = value;
                this.NotifyPropertyChanged("ITEM_CLASS_TYPE");
            }
        }

        #endregion //properties 
    }


    public partial class dcINV_ITEM_CLASS
    {

        public string ITEM_CLASS_NAME_CODE
        {
            get { return m_ITEM_CLASS_NAME + " - " + m_ITEM_CLASS_CODE; }
           
        }
    }
}
