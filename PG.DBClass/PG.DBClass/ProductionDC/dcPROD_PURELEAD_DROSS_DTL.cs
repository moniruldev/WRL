using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.BLLibrary.ProductionDC
{
    [DBTable(Name = "PROD_PURELEAD_DROSS_DTL")]
    public partial class dcPROD_PURELEAD_DROSS_DTL : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_PUR_DROSS_DTL_ID = 0;
        private decimal m_PROD_MST_ID = 0;
        private decimal m_MACHINE_ID = 0;
        private decimal m_UOM_ID = 0;
        private decimal m_ITEM_ID = 0;
        private string m_REMARKS = string.Empty;
        private decimal m_ITEM_QTY = 0;

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


        [DBColumn(Name = "PUR_DROSS_DTL_ID", Storage = "m_PUR_DROSS_DTL_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int PUR_DROSS_DTL_ID
        {
            get { return this.m_PUR_DROSS_DTL_ID; }
            set
            {
                this.m_PUR_DROSS_DTL_ID = value;
                this.NotifyPropertyChanged("PUR_DROSS_DTL_ID");
            }
        }

        [DBColumn(Name = "PROD_MST_ID", Storage = "m_PROD_MST_ID", DbType = "107")]
        public decimal PROD_MST_ID
        {
            get { return this.m_PROD_MST_ID; }
            set
            {
                this.m_PROD_MST_ID = value;
                this.NotifyPropertyChanged("PROD_MST_ID");
            }
        }

        [DBColumn(Name = "MACHINE_ID", Storage = "m_MACHINE_ID", DbType = "107")]
        public decimal MACHINE_ID
        {
            get { return this.m_MACHINE_ID; }
            set
            {
                this.m_MACHINE_ID = value;
                this.NotifyPropertyChanged("MACHINE_ID");
            }
        }

        [DBColumn(Name = "UOM_ID", Storage = "m_UOM_ID", DbType = "107")]
        public decimal UOM_ID
        {
            get { return this.m_UOM_ID; }
            set
            {
                this.m_UOM_ID = value;
                this.NotifyPropertyChanged("UOM_ID");
            }
        }

        [DBColumn(Name = "ITEM_ID", Storage = "m_ITEM_ID", DbType = "107")]
        public decimal ITEM_ID
        {
            get { return this.m_ITEM_ID; }
            set
            {
                this.m_ITEM_ID = value;
                this.NotifyPropertyChanged("ITEM_ID");
            }
        }

             [DBColumn(Name = "ITEM_QTY", Storage = "m_IITEM_QTY", DbType = "107")]
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

        #endregion //properties
    }

     public partial class dcPROD_PURELEAD_DROSS_DTL
     {
         public string ITEM_NAME { get; set; }

         public string UOM_NAME { get; set; }

         public string MACHINE_NAME { get; set; }

     }
}
