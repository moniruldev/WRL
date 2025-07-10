using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "UOM_INFO")]
    public partial class dcUOM_INFO : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_UOM_ID = 0;
        private string m_UOM_CODE = string.Empty;
        private string m_UOM_CODE_SHORT = string.Empty;
        private string m_UOM_NAME = string.Empty;
        private int m_UOM_TYPE_ID = 0;
        private string m_MSR_ID = string.Empty;

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


        [DBColumn(Name = "UOM_ID", Storage = "m_UOM_ID", DbType = "107")]
        public int UOM_ID
        {
            get { return this.m_UOM_ID; }
            set
            {
                this.m_UOM_ID = value;
                this.NotifyPropertyChanged("UOM_ID");
            }
        }

        [DBColumn(Name = "UOM_CODE", Storage = "m_UOM_CODE", DbType = "126")]
        public string UOM_CODE
        {
            get { return this.m_UOM_CODE; }
            set
            {
                this.m_UOM_CODE = value;
                this.NotifyPropertyChanged("UOM_CODE");
            }
        }

        [DBColumn(Name = "UOM_CODE_SHORT", Storage = "m_UOM_CODE_SHORT", DbType = "126")]
        public string UOM_CODE_SHORT
        {
            get { return this.m_UOM_CODE_SHORT; }
            set
            {
                this.m_UOM_CODE_SHORT = value;
                this.NotifyPropertyChanged("UOM_CODE_SHORT");
            }
        }

        [DBColumn(Name = "UOM_NAME", Storage = "m_UOM_NAME", DbType = "126")]
        public string UOM_NAME
        {
            get { return this.m_UOM_NAME; }
            set
            {
                this.m_UOM_NAME = value;
                this.NotifyPropertyChanged("UOM_NAME");
            }
        }

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

        [DBColumn(Name = "MSR_ID", Storage = "m_MSR_ID", DbType = "126")]
        public string MSR_ID
        {
            get { return this.m_MSR_ID; }
            set
            {
                this.m_MSR_ID = value;
                this.NotifyPropertyChanged("MSR_ID");
            }
        }

        #endregion //properties
    }


    public partial class dcUOM_INFO
    {
        private string m_UOM_TYPE_NAME = string.Empty;
        private decimal m_PCS_QTY = 0;
        public string UOM_TYPE_NAME
        {
            get { return m_UOM_TYPE_NAME; }
            set { this.m_UOM_TYPE_NAME = value; }
        }


        public string UOM_NAME_CODE
        {
            get { return m_UOM_NAME + " - " + m_UOM_CODE; }

        }

        public decimal PCS_QTY
        {
            get { return m_PCS_QTY; }
            set { this.m_PCS_QTY = value; }
        }
    }
}
