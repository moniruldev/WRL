using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "BOM_COMPOSITE_PACKAGE_DTL_T")]
    public partial class dcBOM_COMPOSITE_PACKAGE_DTL_T : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_PACKAGE_DTL_ID = 0;
        private int m_PACKAGE_MST_ID = 0;
        private int m_PACKAGE_ITEM_ID = 0;
        private decimal m_ITEM_QTY = 0;
        private int m_UOM_ID = 0;

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


        [DBColumn(Name = "PACKAGE_DTL_ID", Storage = "m_PACKAGE_DTL_ID", DbType = "107")]
        public int PACKAGE_DTL_ID
        {
            get { return this.m_PACKAGE_DTL_ID; }
            set
            {
                this.m_PACKAGE_DTL_ID = value;
                this.NotifyPropertyChanged("PACKAGE_DTL_ID");
            }
        }

        [DBColumn(Name = "PACKAGE_MST_ID", Storage = "m_PACKAGE_MST_ID", DbType = "107")]
        public int PACKAGE_MST_ID
        {
            get { return this.m_PACKAGE_MST_ID; }
            set
            {
                this.m_PACKAGE_MST_ID = value;
                this.NotifyPropertyChanged("PACKAGE_MST_ID");
            }
        }

        [DBColumn(Name = "PACKAGE_ITEM_ID", Storage = "m_PACKAGE_ITEM_ID", DbType = "107")]
        public int PACKAGE_ITEM_ID
        {
            get { return this.m_PACKAGE_ITEM_ID; }
            set
            {
                this.m_PACKAGE_ITEM_ID = value;
                this.NotifyPropertyChanged("PACKAGE_ITEM_ID");
            }
        }


        [DBColumn(Name = "UOM_ID", Storage = "m_UOM_ID", DbType = "107")]
        public int UOM_ID
        {
            get { return this.m_UOM_ID; }
            set
            {
                this.m_PACKAGE_ITEM_ID = value;
                this.NotifyPropertyChanged("UOM_ID");
            }
        }


        [DBColumn(Name = "ITEM_QTY", Storage = "m_ITEM_QTY", DbType = "107")]
        public decimal ITEM_QTY
        {
            get { return this.m_ITEM_QTY; }
            set
            {
                this.m_ITEM_QTY = value;
                this.NotifyPropertyChanged("ITEM_QTY");
            }
        }

        #endregion //properties
    }

    public partial class dcBOM_COMPOSITE_PACKAGE_DTL_T
    {

        string m_PACKAGE_NAME = "";
        int m_MAIN_ITEM_ID = 0;
        string m_MAIN_ITEM_NAME = "";
        string m_PACKAGE_ITEM_NAME = "";
        string m_UOM_NAME = "";

        [DBColumn(Name = "PACKAGE_NAME", Storage = "m_PACKAGE_NAME", DbType = "107")]
        public string PACKAGE_NAME
        {
            get { return this.m_PACKAGE_NAME; }
            set
            {
                this.m_PACKAGE_NAME = value;
                this.NotifyPropertyChanged("PACKAGE_NAME");
            }
        }


        [DBColumn(Name = "MAIN_ITEM_ID", Storage = "m_MAIN_ITEM_ID", DbType = "107")]
        public int MAIN_ITEM_ID
        {
            get { return this.m_MAIN_ITEM_ID; }
            set
            {
                this.m_MAIN_ITEM_ID = value;
                this.NotifyPropertyChanged("MAIN_ITEM_ID");
            }
        }


        [DBColumn(Name = "PACKAGE_ITEM_NAME", Storage = "m_PACKAGE_ITEM_NAME", DbType = "107")]
        public string PACKAGE_ITEM_NAME
        {
            get { return this.m_PACKAGE_ITEM_NAME; }
            set
            {
                this.m_PACKAGE_ITEM_NAME = value;
                this.NotifyPropertyChanged("PACKAGE_ITEM_NAME");
            }
        }

        [DBColumn(Name = "UOM_NAME", Storage = "m_UOM_NAME", DbType = "107")]
        public string UOM_NAME
        {
            get { return this.m_UOM_NAME; }
            set
            {
                this.m_UOM_NAME = value;
                this.NotifyPropertyChanged("UOM_NAME");
            }
        }
    }
}
