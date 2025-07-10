using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "BOM_COMPOSITE_PACKAGE_MST_T")]
    public partial class dcBOM_COMPOSITE_PACKAGE_MST_T : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_PACKAGE_ID = 0;
        private string m_PACKAGE_NAME = string.Empty;
        private int m_PACKAGE_VERSION = 0;
        private string m_IS_ACTIVE = string.Empty;
        private int m_ITEM_ID = 0;

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


        [DBColumn(Name = "PACKAGE_ID", Storage = "m_PACKAGE_ID", DbType = "107")]
        public int PACKAGE_ID
        {
            get { return this.m_PACKAGE_ID; }
            set
            {
                this.m_PACKAGE_ID = value;
                this.NotifyPropertyChanged("PACKAGE_ID");
            }
        }

        [DBColumn(Name = "PACKAGE_NAME", Storage = "m_PACKAGE_NAME", DbType = "126")]
        public string PACKAGE_NAME
        {
            get { return this.m_PACKAGE_NAME; }
            set
            {
                this.m_PACKAGE_NAME = value;
                this.NotifyPropertyChanged("PACKAGE_NAME");
            }
        }

        [DBColumn(Name = "PACKAGE_VERSION", Storage = "m_PACKAGE_VERSION", DbType = "107")]
        public int PACKAGE_VERSION
        {
            get { return this.m_PACKAGE_VERSION; }
            set
            {
                this.m_PACKAGE_VERSION = value;
                this.NotifyPropertyChanged("PACKAGE_VERSION");
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

        [DBColumn(Name = "ITEM_ID", Storage = "m_ITEM_ID", DbType = "107")]
        public int ITEM_ID
        {
            get { return this.m_ITEM_ID; }
            set
            {
                this.m_ITEM_ID = value;
                this.NotifyPropertyChanged("ITEM_ID");
            }
        }

        #endregion //properties
    }

    public partial class dcBOM_COMPOSITE_PACKAGE_MST_T
    {
        string m_ITEM_NAME = "";
        [DBColumn(Name = "ITEM_NAME", Storage = "m_ITEM_NAME", DbType = "126")]
        public string ITEM_NAME
        {
            get { return this.m_ITEM_NAME; }
            set
            {
                this.m_ITEM_NAME = value;
                this.NotifyPropertyChanged("PACKAGE_NAME");
            }
        }
    }
}
