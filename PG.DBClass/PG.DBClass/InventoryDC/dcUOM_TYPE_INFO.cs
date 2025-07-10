using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "UOM_TYPE_INFO")]
    public partial class dcUOM_TYPE_INFO : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_UOM_TYPE_ID = 0;
        private string m_UOM_TYPE_CODE = string.Empty;
        private string m_UOM_TYPE_NAME = string.Empty;
        private string m_UOM_TYPE_DESC = string.Empty;

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


        [DBColumn(Name = "UOM_TYPE_ID", Storage = "m_UOM_TYPE_ID", DbType = "107")]
        public int UOM_TYPE_ID
        {
            get { return this.m_UOM_TYPE_ID; }
            set
            {
                this.m_UOM_TYPE_ID = value;
                this.NotifyPropertyChanged("UOM_TYPE_ID");
            }
        }

        [DBColumn(Name = "UOM_TYPE_CODE", Storage = "m_UOM_TYPE_CODE", DbType = "126")]
        public string UOM_TYPE_CODE
        {
            get { return this.m_UOM_TYPE_CODE; }
            set
            {
                this.m_UOM_TYPE_CODE = value;
                this.NotifyPropertyChanged("UOM_TYPE_CODE");
            }
        }

        [DBColumn(Name = "UOM_TYPE_NAME", Storage = "m_UOM_TYPE_NAME", DbType = "126")]
        public string UOM_TYPE_NAME
        {
            get { return this.m_UOM_TYPE_NAME; }
            set
            {
                this.m_UOM_TYPE_NAME = value;
                this.NotifyPropertyChanged("UOM_TYPE_NAME");
            }
        }

        [DBColumn(Name = "UOM_TYPE_DESC", Storage = "m_UOM_TYPE_DESC", DbType = "126")]
        public string UOM_TYPE_DESC
        {
            get { return this.m_UOM_TYPE_DESC; }
            set
            {
                this.m_UOM_TYPE_DESC = value;
                this.NotifyPropertyChanged("UOM_TYPE_DESC");
            }
        }

        #endregion //properties
    }
}
