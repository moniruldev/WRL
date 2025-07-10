using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "PROD_PAST_PROCESS_LOSS_DTL")]
    public partial class dcPROD_PAST_PROCESS_LOSS_DTL : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_LOSS_DTL_ID = 0;
        private int m_LOSS_MST_ID = 0;
        private int m_ITEM_ID = 0;
        private int m_UOM_ID = 0;
        private decimal m_ITEM_QTY = 0;
        private string m_REMARKS = string.Empty;
        private int m_SI = 0;
        private decimal m_CLOSING_QTY = 0;
        private string m_IS_BATCH = string.Empty;

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


        [DBColumn(Name = "LOSS_DTL_ID", Storage = "m_LOSS_DTL_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int LOSS_DTL_ID
        {
            get { return this.m_LOSS_DTL_ID; }
            set
            {
                this.m_LOSS_DTL_ID = value;
                this.NotifyPropertyChanged("LOSS_DTL_ID");
            }
        }

        [DBColumn(Name = "LOSS_MST_ID", Storage = "m_LOSS_MST_ID", DbType = "107")]
        public int LOSS_MST_ID
        {
            get { return this.m_LOSS_MST_ID; }
            set
            {
                this.m_LOSS_MST_ID = value;
                this.NotifyPropertyChanged("LOSS_MST_ID");
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

        [DBColumn(Name = "IS_BATCH", Storage = "m_IS_BATCH", DbType = "126")]
        public string IS_BATCH
        {
            get { return this.m_IS_BATCH; }
            set
            {
                this.m_IS_BATCH = value;
                this.NotifyPropertyChanged("IS_BATCH");
            }
        }

        #endregion //properties
    }


    public partial class dcPROD_PAST_PROCESS_LOSS_DTL
    {
        public string ITEM_NAME {get; set;}
        public string UOM_NAME { get; set; }

    }
}
