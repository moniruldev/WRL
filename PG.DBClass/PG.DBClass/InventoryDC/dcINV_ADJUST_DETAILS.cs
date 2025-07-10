using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
    [DBTable(Name = "INV_ADJUST_DETAILS")]
    public partial class dcINV_ADJUST_DETAILS : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_INV_ADJUST_DET_ID = 0;
        private decimal m_INV_ADJUST_ID = 0;
        private int m_ITEM_ID = 0;
        private decimal m_UOM_ID = 0;
        private decimal m_ADJUST_QTY = 0;
        private decimal m_ADD_ADJUST_QTY = 0;
        private decimal m_DEDUCT_ADJUST_QTY = 0;
        private string m_REMARKS_DTL = string.Empty;
        private decimal m_ADJ_SL_NO = 0;
        private int m_CREATE_BY = 0;
        private DateTime? m_CREATE_DATE = null;
        private decimal m_TARGET_QTY = 0;
        private int m_ADJUSTMENT_TYPE_ID = 0;
        private decimal m_LAST_CLOSING_QTY = 0;
        private decimal m_AUTH_ADJUST_QTY = 0;
        private decimal m_AUTH_ADD_ADJUST_QTY = 0;
        private decimal m_AUTH_DEDUCT_ADJUST_QTY = 0;
        private int? m_ITEM_TYPE_ID = 0;


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


        [DBColumn(Name = "INV_ADJUST_DET_ID", Storage = "m_INV_ADJUST_DET_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int INV_ADJUST_DET_ID
        {
            get { return this.m_INV_ADJUST_DET_ID; }
            set
            {
                this.m_INV_ADJUST_DET_ID = value;
                this.NotifyPropertyChanged("INV_ADJUST_DET_ID");
            }
        }

        [DBColumn(Name = "INV_ADJUST_ID", Storage = "m_INV_ADJUST_ID", DbType = "107")]
        public decimal INV_ADJUST_ID
        {
            get { return this.m_INV_ADJUST_ID; }
            set
            {
                this.m_INV_ADJUST_ID = value;
                this.NotifyPropertyChanged("INV_ADJUST_ID");
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
        public decimal UOM_ID
        {
            get { return this.m_UOM_ID; }
            set
            {
                this.m_UOM_ID = value;
                this.NotifyPropertyChanged("UOM_ID");
            }
        }

        [DBColumn(Name = "ADJUST_QTY", Storage = "m_ADJUST_QTY", DbType = "107")]
        public decimal ADJUST_QTY
        {
            get { return this.m_ADJUST_QTY; }
            set
            {
                this.m_ADJUST_QTY = value;
                this.NotifyPropertyChanged("ADJUST_QTY");
            }
        }

        [DBColumn(Name = "ADD_ADJUST_QTY", Storage = "m_ADD_ADJUST_QTY", DbType = "107")]
        public decimal ADD_ADJUST_QTY
        {
            get { return this.m_ADD_ADJUST_QTY; }
            set
            {
                this.m_ADD_ADJUST_QTY = value;
                this.NotifyPropertyChanged("ADD_ADJUST_QTY");
            }
        }

        [DBColumn(Name = "DEDUCT_ADJUST_QTY", Storage = "m_DEDUCT_ADJUST_QTY", DbType = "107")]
        public decimal DEDUCT_ADJUST_QTY
        {
            get { return this.m_DEDUCT_ADJUST_QTY; }
            set
            {
                this.m_DEDUCT_ADJUST_QTY = value;
                this.NotifyPropertyChanged("DEDUCT_ADJUST_QTY");
            }
        }

        [DBColumn(Name = "REMARKS_DTL", Storage = "m_REMARKS_DTL", DbType = "126")]
        public string REMARKS_DTL
        {
            get { return this.m_REMARKS_DTL; }
            set
            {
                this.m_REMARKS_DTL = value;
                this.NotifyPropertyChanged("REMARKS_DTL");
            }
        }
        [DBColumn(Name = "ADJ_SL_NO", Storage = "m_ADJ_SL_NO", DbType = "107")]
        public decimal ADJ_SL_NO
        {
            get { return this.m_ADJ_SL_NO; }
            set
            {
                this.m_ADJ_SL_NO = value;
                this.NotifyPropertyChanged("ADJ_SL_NO");
            }
        }

        [DBColumn(Name = "CREATE_BY", Storage = "m_CREATE_BY", DbType = "126")]
        public int CREATE_BY
        {
            get { return this.m_CREATE_BY; }
            set
            {
                this.m_CREATE_BY = value;
                this.NotifyPropertyChanged("CREATE_BY");
            }
        }

        [DBColumn(Name = "CREATE_DATE", Storage = "m_CREATE_DATE", DbType = "106")]
        public DateTime? CREATE_DATE
        {
            get { return this.m_CREATE_DATE; }
            set
            {
                this.m_CREATE_DATE = value;
                this.NotifyPropertyChanged("CREATE_DATE");
            }
        }

        [DBColumn(Name = "TARGET_QTY", Storage = "m_TARGET_QTY", DbType = "107")]
        public decimal TARGET_QTY
        {
            get { return this.m_TARGET_QTY; }
            set
            {
                this.m_TARGET_QTY = value;
                this.NotifyPropertyChanged("TARGET_QTY");
            }
        }


        [DBColumn(Name = "ADJUSTMENT_TYPE_ID", Storage = "m_ADJUSTMENT_TYPE_ID", DbType = "107")]
        public int ADJUSTMENT_TYPE_ID
        {
            get { return this.m_ADJUSTMENT_TYPE_ID; }
            set
            {
                this.m_ADJUSTMENT_TYPE_ID = value;
                this.NotifyPropertyChanged("ADJUSTMENT_TYPE_ID");
            }
        }


        [DBColumn(Name = "LAST_CLOSING_QTY", Storage = "m_LAST_CLOSING_QTY", DbType = "107")]
        public decimal LAST_CLOSING_QTY
        {
            get { return this.m_LAST_CLOSING_QTY; }
            set
            {
                this.m_LAST_CLOSING_QTY = value;
                this.NotifyPropertyChanged("LAST_CLOSING_QTY");
            }
        }



        [DBColumn(Name = "AUTH_ADJUST_QTY", Storage = "m_AUTH_ADJUST_QTY", DbType = "107")]
        public decimal AUTH_ADJUST_QTY
        {
            get { return this.m_AUTH_ADJUST_QTY; }
            set
            {
                this.m_AUTH_ADJUST_QTY = value;
                this.NotifyPropertyChanged("AUTH_ADJUST_QTY");
            }
        }



        [DBColumn(Name = "AUTH_ADD_ADJUST_QTY", Storage = "m_AUTH_ADD_ADJUST_QTY", DbType = "107")]
        public decimal AUTH_ADD_ADJUST_QTY
        {
            get { return this.m_AUTH_ADD_ADJUST_QTY; }
            set
            {
                this.m_AUTH_ADD_ADJUST_QTY = value;
                this.NotifyPropertyChanged("AUTH_ADD_ADJUST_QTY");
            }
        }

        [DBColumn(Name = "AUTH_DEDUCT_ADJUST_QTY", Storage = "m_AUTH_DEDUCT_ADJUST_QTY", DbType = "107")]
        public decimal AUTH_DEDUCT_ADJUST_QTY
        {
            get { return this.m_AUTH_DEDUCT_ADJUST_QTY; }
            set
            {
                this.m_AUTH_DEDUCT_ADJUST_QTY = value;
                this.NotifyPropertyChanged("AUTH_DEDUCT_ADJUST_QTY");
            }
        }

        [DBColumn(Name = "ITEM_TYPE_ID", Storage = "m_ITEM_TYPE_ID", DbType = "107")]
        public int? ITEM_TYPE_ID
        {
            get { return this.m_ITEM_TYPE_ID; }
            set
            {
                this.m_ITEM_TYPE_ID = value;
                this.NotifyPropertyChanged("ITEM_TYPE_ID");
            }
        }
       

        #endregion //properties
    }

    public partial class dcINV_ADJUST_DETAILS
    {
        private string m_item_code = string.Empty;
        private string m_item_name = string.Empty;
        private string m_uom_name = string.Empty;

        public string item_code
        {
            get { return m_item_code; }
            set { this.m_item_code = value; }
        }
        public string item_name
        {
            get { return m_item_name; }
            set { this.m_item_name = value; }
        }
        public string uom_name
        {
            get { return m_uom_name; }
            set { this.m_uom_name = value; }
        }

        private int m_item_group_id = 0;
        public int item_group_id
        {
            get { return m_item_group_id; }
            set { m_item_group_id = value; }
        }

        private string m_item_group_desc = string.Empty;
        public string item_group_desc
        {
            get { return m_item_group_desc; }
            set { m_item_group_desc = value; }
        }

        private string m_item_group_name = string.Empty;
        public string item_group_name
        {
            get { return m_item_group_name; }
            set { m_item_group_name = value; }
        }
        public decimal CLOSING_QTY { get; set; }

    }
}
