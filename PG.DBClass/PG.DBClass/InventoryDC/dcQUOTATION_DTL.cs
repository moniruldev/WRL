using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.InventoryDC
{
    [DBTable(Name = "QUOTATION_DTL")]
    public partial class dcQUOTATION_DTL : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_QUOTATION_DTL_ID = 0;
        private int m_QUOTATION_MST_ID = 0;
        private int m_ITEM_ID = 0;
        private int m_UOM_ID = 0;
        private decimal m_ITEM_QTY = 0;
        private decimal m_DISCOUNT = 0;
        private decimal m_ITEM_RATE = 0;
        private string m_DET_REMARKS = string.Empty;
        private int m_DTL_SERIAL_NO = 0;
        

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


        [DBColumn(Name = "QUOTATION_DTL_ID", Storage = "m_QUOTATION_DTL_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int QUOTATION_DTL_ID
        {
            get { return this.m_QUOTATION_DTL_ID; }
            set
            {
                this.m_QUOTATION_DTL_ID = value;
                this.NotifyPropertyChanged("QUOTATION_DTL_ID");
            }
        }

        [DBColumn(Name = "QUOTATION_MST_ID", Storage = "m_QUOTATION_MST_ID", DbType = "107")]
        public int QUOTATION_MST_ID
        {
            get { return this.m_QUOTATION_MST_ID; }
            set
            {
                this.m_QUOTATION_MST_ID = value;
                this.NotifyPropertyChanged("QUOTATION_MST_ID");
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

        [DBColumn(Name = "DISCOUNT", Storage = "m_DISCOUNT", DbType = "107")]
        public decimal DISCOUNT
        {
            get { return this.m_DISCOUNT; }
            set
            {
                this.m_DISCOUNT = value;
                this.NotifyPropertyChanged("DISCOUNT");
            }
        }

        [DBColumn(Name = "ITEM_RATE", Storage = "m_ITEM_RATE", DbType = "107")]
        public decimal ITEM_RATE
        {
            get { return this.m_ITEM_RATE; }
            set
            {
                this.m_ITEM_RATE = value;
                this.NotifyPropertyChanged("ITEM_RATE");
            }
        }

        [DBColumn(Name = "DET_REMARKS", Storage = "m_DET_REMARKS", DbType = "126")]
        public string DET_REMARKS
        {
            get { return this.m_DET_REMARKS; }
            set
            {
                this.m_DET_REMARKS = value;
                this.NotifyPropertyChanged("DET_REMARKS");
            }
        }


        [DBColumn(Name = "DTL_SERIAL_NO", Storage = "m_DTL_SERIAL_NO", DbType = "107")]
        public int DTL_SERIAL_NO
        {
            get { return this.m_DTL_SERIAL_NO; }
            set
            {
                this.m_DTL_SERIAL_NO = value;
                this.NotifyPropertyChanged("DTL_SERIAL_NO");
            }
        }

        #endregion //properties
    }


    public partial class dcQUOTATION_DTL
    {

        private string m_item_desc = string.Empty;
        private string m_item_name = string.Empty;
        private string m_uom_name = null;
        private string m_item_code = string.Empty;
        private string m_item_group_name = string.Empty;
        private int? m_item_group_id = 0;



        public string item_desc
        {
            get { return m_item_desc; }
            set { this.m_item_desc = value; }
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
        public string item_code
        {
            get { return m_item_code; }
            set { this.m_item_code = value; }
        }
        public string item_group_name
        {
            get { return m_item_group_name; }
            set { this.m_item_group_name = value; }
        }
        public int? item_group_id
        {
            get { return m_item_group_id; }
            set { this.m_item_group_id = value; }
        }

        private decimal m_SAFTY_STOCK_LEVEL = 0;
        public decimal SAFTY_STOCK_LEVEL
        {
            get { return m_SAFTY_STOCK_LEVEL; }
            set { this.m_SAFTY_STOCK_LEVEL = value; }
        }


        private decimal m_TOTAL_REQ_QTY = 0;
        public decimal TOTAL_REQ_QTY
        {
            get { return m_TOTAL_REQ_QTY; }
            set { this.m_TOTAL_REQ_QTY = value; }
        }

        private decimal m_RE_ORDER_LEVEL = 0;
        public decimal RE_ORDER_LEVEL
        {
            get { return m_RE_ORDER_LEVEL; }
            set { this.m_RE_ORDER_LEVEL = value; }
        }
        public string INCLUDE_SAFETY_STOCK { get; set; }

        //10  -5

        private decimal m_FINAL_INDT_QTY = 0;
       

        private decimal m_TOT_AMOUNT = 0;
        public decimal TOT_AMOUNT
        {
            get { return m_TOT_AMOUNT; }
            set { this.m_TOT_AMOUNT = value; }
        }

        private decimal m_CLOSING_QTY = 0;
        public decimal CLOSING_QTY
        {
            get { return m_CLOSING_QTY; }
            set { this.m_CLOSING_QTY = value; }
        }

        private string m_DEPARTMENT_NAME = string.Empty;
        public string DEPARTMENT_NAME
        {
            get { return m_DEPARTMENT_NAME; }
            set { this.m_DEPARTMENT_NAME = value; }
        }

        private string m_INDT_NO = string.Empty;
        public string INDT_NO
        {
            get { return m_INDT_NO; }
            set { this.m_INDT_NO = value; }
        }

        private DateTime? m_INDT_DATE = null;
        public DateTime? INDT_DATE
        {
            get { return m_INDT_DATE; }
            set { this.m_INDT_DATE = value; }
        }

        public decimal WEIGHTED_AVERAGE_PRICE { get; set; }

        private string m_ITEM_TYPE_CODE = string.Empty;
        public string ITEM_TYPE_CODE
        {
            get { return m_ITEM_TYPE_CODE; }
            set { this.m_ITEM_TYPE_CODE = value; }
        }


    }
}
