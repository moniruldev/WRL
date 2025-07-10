using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
     [DBTable(Name = "INV_RACK")]
    public partial class dcINV_RACK : DBBaseClass, INotifyPropertyChanged 
    {
        #region private members

        private string m_RACK_ID = string.Empty;
        private string m_RACK_NAME = string.Empty;
        private string m_SHORT_NAME = string.Empty;
        private string m_STORE_ID = string.Empty;
        private string m_DESCRIPTION = string.Empty;

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


        [DBColumn(Name = "RACK_ID", Storage = "m_RACK_ID", DbType = "126", IsPrimaryKey = true)]
        public string RACK_ID
        {
            get { return this.m_RACK_ID; }
            set
            {
                this.m_RACK_ID = value;
                this.NotifyPropertyChanged("RACK_ID");
            }
        }

        [DBColumn(Name = "RACK_NAME", Storage = "m_RACK_NAME", DbType = "126")]
        public string RACK_NAME
        {
            get { return this.m_RACK_NAME; }
            set
            {
                this.m_RACK_NAME = value;
                this.NotifyPropertyChanged("RACK_NAME");
            }
        }

        [DBColumn(Name = "SHORT_NAME", Storage = "m_SHORT_NAME", DbType = "126")]
        public string SHORT_NAME
        {
            get { return this.m_SHORT_NAME; }
            set
            {
                this.m_SHORT_NAME = value;
                this.NotifyPropertyChanged("SHORT_NAME");
            }
        }

        [DBColumn(Name = "STORE_ID", Storage = "m_STORE_ID", DbType = "126")]
        public string STORE_ID
        {
            get { return this.m_STORE_ID; }
            set
            {
                this.m_STORE_ID = value;
                this.NotifyPropertyChanged("STORE_ID");
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

        #endregion //properties 
    }
}
