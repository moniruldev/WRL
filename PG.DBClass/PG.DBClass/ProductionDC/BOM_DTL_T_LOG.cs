using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "BOM_DTL_T_LOG")]
    public partial class dcBOM_DTL_T_LOG : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_BOM_DTL_ID = 0;
        private int m_BOM_MST_ID = 0;
        private int m_ITEM_ID = 0;
        private decimal m_ITEM_QTY = 0;
        private decimal m_ITEM_UNIT_ID = 0;
        private decimal m_IS_PRIME = 0;
        private decimal m_PACKAGE_ID = 0;
        private string m_REMARKS = string.Empty;
        private decimal m_SLNO = 0;
        private decimal m_ITEM_BOM_ID = 0;
        private decimal m_WASTAGE_PERCENT = 0;
        private int m_SI_MST = 0;

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

        [DBColumn(Name = "SI_MST", Storage = "m_SI_MST", DbType = "107")]
        public int SI_MST
        {
            get { return this.m_SI_MST; }
            set
            {
                this.m_SI_MST = value;
                this.NotifyPropertyChanged("SI_MST");
            }
        }

        [DBColumn(Name = "BOM_DTL_ID", Storage = "m_BOM_DTL_ID", DbType = "107")]
        public int BOM_DTL_ID
        {
            get { return this.m_BOM_DTL_ID; }
            set
            {
                this.m_BOM_DTL_ID = value;
                this.NotifyPropertyChanged("BOM_DTL_ID");
            }
        }

        [DBColumn(Name = "BOM_MST_ID", Storage = "m_BOM_MST_ID", DbType = "107")]
        public int BOM_MST_ID
        {
            get { return this.m_BOM_MST_ID; }
            set
            {
                this.m_BOM_MST_ID = value;
                this.NotifyPropertyChanged("BOM_MST_ID");
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

        [DBColumn(Name = "ITEM_UNIT_ID", Storage = "m_ITEM_UNIT_ID", DbType = "107")]
        public decimal ITEM_UNIT_ID
        {
            get { return this.m_ITEM_UNIT_ID; }
            set
            {
                this.m_ITEM_UNIT_ID = value;
                this.NotifyPropertyChanged("ITEM_UNIT_ID");
            }
        }

        [DBColumn(Name = "IS_PRIME", Storage = "m_IS_PRIME", DbType = "107")]
        public decimal IS_PRIME
        {
            get { return this.m_IS_PRIME; }
            set
            {
                this.m_IS_PRIME = value;
                this.NotifyPropertyChanged("IS_PRIME");
            }
        }

        [DBColumn(Name = "PACKAGE_ID", Storage = "m_PACKAGE_ID", DbType = "107")]
        public decimal PACKAGE_ID
        {
            get { return this.m_PACKAGE_ID; }
            set
            {
                this.m_PACKAGE_ID = value;
                this.NotifyPropertyChanged("PACKAGE_ID");
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

        [DBColumn(Name = "SLNO", Storage = "m_SLNO", DbType = "107")]
        public decimal SLNO
        {
            get { return this.m_SLNO; }
            set
            {
                this.m_SLNO = value;
                this.NotifyPropertyChanged("SLNO");
            }
        }

        [DBColumn(Name = "ITEM_BOM_ID", Storage = "m_ITEM_BOM_ID", DbType = "107")]
        public decimal ITEM_BOM_ID
        {
            get { return this.m_ITEM_BOM_ID; }
            set
            {
                this.m_ITEM_BOM_ID = value;
                this.NotifyPropertyChanged("ITEM_BOM_ID");
            }
        }

        [DBColumn(Name = "WASTAGE_PERCENT", Storage = "m_WASTAGE_PERCENT", DbType = "107")]
        public decimal WASTAGE_PERCENT
        {
            get { return this.m_WASTAGE_PERCENT; }
            set
            {
                this.m_WASTAGE_PERCENT = value;
                this.NotifyPropertyChanged("WASTAGE_PERCENT");
            }
        }

        #endregion //properties
    }


    public partial class dcBOM_DTL_T_LOG
    {
        string m_ITEM_NAME = "";
        string m_ITEM_TYPE_NAME = "";
        string m_PACKAGE_NAME = "";
        string m_ITEM_GROUP_NAME = "";
        string m_UOM_NAME = "";
        string m_ITEM_GROUP_ID = "";
        string m_BOM_ITEM_DESC = "";



        public string UOM_NAME
        {
            get { return this.m_UOM_NAME; }
            set
            {
                this.m_UOM_NAME = value;
            }
        }



        public string ITEM_NAME
        {
            get { return this.m_ITEM_NAME; }
            set
            {
                this.m_ITEM_NAME = value;
            }
        }

        public string ITEM_TYPE_NAME
        {
            get { return this.m_ITEM_TYPE_NAME; }
            set
            {
                this.m_ITEM_TYPE_NAME = value;
            }
        }

        public string PACKAGE_NAME
        {
            get { return this.m_PACKAGE_NAME; }
            set
            {
                this.m_PACKAGE_NAME = value;
            }
        }

        public string ITEM_GROUP_DESC
        {
            get { return this.m_ITEM_GROUP_NAME; }
            set
            {
                this.m_ITEM_GROUP_NAME = value;
            }
        }

        public string ITEM_GROUP_ID
        {
            get { return this.m_ITEM_GROUP_ID; }
            set
            {
                this.m_ITEM_GROUP_ID = value;
            }
        }
        public string BOM_ITEM_DESC
        {
            get { return this.m_BOM_ITEM_DESC; }
            set { this.m_BOM_ITEM_DESC = value; }
        }


    }
}
