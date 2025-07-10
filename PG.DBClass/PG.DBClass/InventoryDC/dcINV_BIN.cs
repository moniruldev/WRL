using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "INV_BIN")]
    public class dcINV_BIN : DBBaseClass, INotifyPropertyChanged 
    {
        #region private members

        private string m_BIN_ID = string.Empty;
        private string m_BIN_NAME = string.Empty;
        private string m_SHORT_NAME = string.Empty;
        private string m_DESCRIPTION = string.Empty;
        private string m_RACK_ID = string.Empty;

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


        [DBColumn(Name = "BIN_ID", Storage = "m_BIN_ID", DbType = "126", IsPrimaryKey = true)]
        public string BIN_ID
        {
            get { return this.m_BIN_ID; }
            set
            {
                this.m_BIN_ID = value;
                this.NotifyPropertyChanged("BIN_ID");
            }
        }

        [DBColumn(Name = "BIN_NAME", Storage = "m_BIN_NAME", DbType = "126")]
        public string BIN_NAME
        {
            get { return this.m_BIN_NAME; }
            set
            {
                this.m_BIN_NAME = value;
                this.NotifyPropertyChanged("BIN_NAME");
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

        [DBColumn(Name = "RACK_ID", Storage = "m_RACK_ID", DbType = "126")]
        public string RACK_ID
        {
            get { return this.m_RACK_ID; }
            set
            {
                this.m_RACK_ID = value;
                this.NotifyPropertyChanged("RACK_ID");
            }
        }

        #endregion //properties 
    }
}
