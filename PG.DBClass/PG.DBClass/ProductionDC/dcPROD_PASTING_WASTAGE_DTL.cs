using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "PROD_PASTING_WASTAGE_DTL")]
    public partial class dcPROD_PASTING_WASTAGE_DTL : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int  m_WASTAGE_DTL_ID = 0;
        private int m_WASTAGE_MST_ID = 0;
        private int m_ITEM_ID = 0;
        private int m_UOM_ID = 0;
        private decimal m_ITEM_QTY = 0;
        private string m_REMARKS = string.Empty;
        private int m_SI = 0;
        private decimal m_CLOSING_QTY = 0;
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

         [DBColumn(Name = "WASTAGE_DTL_ID", Storage = "m_WASTAGE_DTL_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int WASTAGE_DTL_ID
        {
            get { return this.m_WASTAGE_DTL_ID; }
            set
            {
                this.m_WASTAGE_DTL_ID = value;
                this.NotifyPropertyChanged("WASTAGE_DTL_ID");
            }
        }

        [DBColumn(Name = "WASTAGE_MST_ID", Storage = "m_WASTAGE_MST_ID", DbType = "107")]
        public int WASTAGE_MST_ID
        {
            get { return this.m_WASTAGE_MST_ID; }
            set
            {
                this.m_WASTAGE_MST_ID = value;
                this.NotifyPropertyChanged("WASTAGE_MST_ID");
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

        [DBColumn(Name = "SI", Storage = "m_SI", DbType = "107")]
        public int SI
        {
            get { return this.m_SI; }
            set
            {
                this.m_SI = value;
                this.NotifyPropertyChanged("SI");
            }
        }


        [DBColumn(Name = "CLOSING_QTY", Storage = "m_CLOSING_QTY", DbType = "107")]
        public decimal CLOSING_QTY
        {
            get { return this.m_CLOSING_QTY; }
            set
            {
                this.m_CLOSING_QTY = value;
                this.NotifyPropertyChanged("CLOSING_QTY");
            }
        }

        #endregion //properties
    }

    public partial class dcPROD_PASTING_WASTAGE_DTL
    {
        private string m_ITEM_NAME = string.Empty;
        public string ITEM_NAME
        {
            get { return m_ITEM_NAME; }
            set { m_ITEM_NAME = value; }
        }

        private string m_UOM_NAME = string.Empty;
        public string UOM_NAME
        {
            get { return m_UOM_NAME; }
            set { m_UOM_NAME = value; }
        }

        private string m_UOM_CODE = string.Empty;
        public string UOM_CODE
        {
            get { return m_UOM_CODE; }
            set { m_UOM_CODE = value; }
        }

        //public decimal CLOSING_QTY { get; set; }
    }
}
