using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "LP_PURCHASE_DETAILS")]
    public partial class dcLP_PURCHASE_DETAILS : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private Int64 m_PURCHASE_DET_ID = 0;
        private int m_SLNO = 0;
        private Int64 m_PURCHASE_ID = 0;
        private int m_ITEM_ID = 0;
        private decimal m_PURCHASE_QTY = 0;
        private decimal m_DISCOUNTS_AMT = 0;
        private int m_UOM_ID = 0;
        private decimal m_UNIT_RATE = 0;
        private int? m_SUP_ID = 0;
        private Int64? m_INDT_DET_ID = 0;
        
        private string m_NOTE = string.Empty;
        private string m_SUP_DC_NO = string.Empty;
        private int m_CREATE_BY = 0;
        private DateTime? m_CREATE_DATE = null;
        private int? m_UPDATE_BY = 0;
        private DateTime? m_UPDATE_DATE = null;
        private Int64? m_INVOICE_DET_ID = 0;
        private decimal m_UNIT_PRICE = 0;
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


        [DBColumn(Name = "PURCHASE_DET_ID", Storage = "m_PURCHASE_DET_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public Int64 PURCHASE_DET_ID
        {
            get { return this.m_PURCHASE_DET_ID; }
            set
            {
                this.m_PURCHASE_DET_ID = value;
                this.NotifyPropertyChanged("PURCHASE_DET_ID");
            }
        }


         

        
        [DBColumn(Name = "SLNO", Storage = "m_SLNO", DbType = "107")]
        public int SLNO
        {
            get { return this.m_SLNO; }
            set
            {
                this.m_SLNO = value;
                this.NotifyPropertyChanged("SLNO");
            }
        }

        [DBColumn(Name = "PURCHASE_ID", Storage = "m_PURCHASE_ID", DbType = "107")]
        public Int64 PURCHASE_ID
        {
            get { return this.m_PURCHASE_ID; }
            set
            {
                this.m_PURCHASE_ID = value;
                this.NotifyPropertyChanged("PURCHASE_ID");
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

        [DBColumn(Name = "PURCHASE_QTY", Storage = "m_PURCHASE_QTY", DbType = "107")]
        public decimal PURCHASE_QTY
        {
            get { return this.m_PURCHASE_QTY; }
            set
            {
                this.m_PURCHASE_QTY = value;
                this.NotifyPropertyChanged("PURCHASE_QTY");
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

        [DBColumn(Name = "UNIT_RATE", Storage = "m_UNIT_RATE", DbType = "107")]
        public decimal UNIT_RATE
        {
            get { return this.m_UNIT_RATE; }
            set
            {
                this.m_UNIT_RATE = value;
                this.NotifyPropertyChanged("UNIT_RATE");
            }
        }

        [DBColumn(Name = "DISCOUNTS_AMT", Storage = "m_DISCOUNTS_AMT", DbType = "107")]
        public decimal DISCOUNTS_AMT
        {
            get { return this.m_DISCOUNTS_AMT; }
            set
            {
                this.m_DISCOUNTS_AMT = value;
                this.NotifyPropertyChanged("DISCOUNTS_AMT");
            }
        }

        [DBColumn(Name = "NOTE", Storage = "m_NOTE", DbType = "126")]
        public string NOTE
        {
            get { return this.m_NOTE; }
            set
            {
                this.m_NOTE = value;
                this.NotifyPropertyChanged("NOTE");
            }
        }



        [DBColumn(Name = "SUP_DC_NO", Storage = "m_SUP_DC_NO", DbType = "126")]
        public string SUP_DC_NO
        {
            get { return this.m_SUP_DC_NO; }
            set
            {
                this.m_SUP_DC_NO = value;
                this.NotifyPropertyChanged("SUP_DC_NO");
            }
        }
        

        [DBColumn(Name = "SUP_ID", Storage = "m_SUP_ID", DbType = "107")]
        public int? SUP_ID
        {
            get { return this.m_SUP_ID; }
            set
            {
                this.m_SUP_ID = value;
                this.NotifyPropertyChanged("SUP_ID");
            }
        }

        [DBColumn(Name = "CREATE_BY", Storage = "m_CREATE_BY", DbType = "107")]
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

        [DBColumn(Name = "UPDATE_BY", Storage = "m_UPDATE_BY", DbType = "107")]
        public int? UPDATE_BY
        {
            get { return this.m_UPDATE_BY; }
            set
            {
                this.m_UPDATE_BY = value;
                this.NotifyPropertyChanged("UPDATE_BY");
            }
        }

        [DBColumn(Name = "UPDATE_DATE", Storage = "m_UPDATE_DATE", DbType = "106")]
        public DateTime? UPDATE_DATE
        {
            get { return this.m_UPDATE_DATE; }
            set
            {
                this.m_UPDATE_DATE = value;
                this.NotifyPropertyChanged("UPDATE_DATE");
            }
        }

        [DBColumn(Name = "INDT_DET_ID", Storage = "m_INDT_DET_ID")]
        public Int64? INDT_DET_ID
        {
            get { return this.m_INDT_DET_ID; }
            set
            {
                this.m_INDT_DET_ID = value;
                this.NotifyPropertyChanged("INDT_DET_ID");
            }
        }

        [DBColumn(Name = "INVOICE_DET_ID", Storage = "m_INVOICE_DET_ID_ID", DbType = "107")]
        public Int64? INVOICE_DET_ID
        {
            get { return this.m_INVOICE_DET_ID; }
            set
            {
                this.m_INVOICE_DET_ID = value;
                this.NotifyPropertyChanged("INVOICE_DET_ID");
            }
        }

        [DBColumn(Name = "UNIT_PRICE", Storage = "m_UNIT_PRICE", DbType = "107")]
        public decimal UNIT_PRICE
        {
            get { return this.m_UNIT_PRICE; }
            set
            {
                this.m_UNIT_PRICE = value;
                this.NotifyPropertyChanged("UNIT_PRICE");
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

    public partial class dcLP_PURCHASE_DETAILS
    {

        private string m_item_desc = string.Empty;
        private string m_item_name = string.Empty;
        private string m_uom_name = null;
        private string m_item_code = string.Empty;
        private string m_sup_name = string.Empty;
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
        public string sup_name
        {
            get { return m_sup_name; }
            set { this.m_sup_name = value; }
        }



        private Int64 m_MRR_DET_ID = 0;
        public Int64 MRR_DET_ID
        {
            get { return m_MRR_DET_ID; }
            set { m_MRR_DET_ID = value; }
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

        public decimal m_ALREADRY_PURCHASE_QTY = 0;
        public decimal ALREADRY_PURCHASE_QTY
        {
            get { return m_ALREADRY_PURCHASE_QTY; }
            set { m_ALREADRY_PURCHASE_QTY = value; }
        }

        public decimal m_INDT_QTY = 0;
        public decimal INDT_QTY
        {
            get { return m_INDT_QTY; }
            set { m_INDT_QTY = value; }
        }

        public decimal m_BALANCE_QTY = 0;
        public decimal BALANCE_QTY
        {
            get { return m_INDT_QTY - m_ALREADRY_PURCHASE_QTY; }
        }

        private string m_INDT_REMARKS = string.Empty;
        public string INDT_REMARKS
        {
            get { return m_INDT_REMARKS; }
            set { m_INDT_REMARKS = value; }
        }

        private string m_INDT_PRIORITY = string.Empty;
        public string INDT_PRIORITY
        {
            get { return m_INDT_PRIORITY; }
            set { m_INDT_PRIORITY = value; }
        }

        public string DEPARTMENT_NAME { get; set; }
        public string INDT_NO { get; set; }
        public DateTime? INDT_DATE { get; set; }

        public decimal WEIGHTED_AVERAGE_PRICE { get; set; }

        public decimal total_amount { get; set; }

        private string m_ITEM_TYPE_CODE = string.Empty;
        public string ITEM_TYPE_CODE
        {
            get { return m_ITEM_TYPE_CODE; }
            set { m_ITEM_TYPE_CODE = value; }
        }
        private bool m_IsPurComplete = false;
        public bool IsPurComplete
        {
            get { return this.m_IsPurComplete; }
            set { this.m_IsPurComplete = value; }
        }

        private decimal m_ALREADY_MRR_QTY = 0;
        public decimal ALREADY_MRR_QTY
        {
            get { return m_ALREADY_MRR_QTY; }
            set { this.m_ALREADY_MRR_QTY = value; }

        }

        private bool m_IsMRRDTLComplete = false;
        public bool IsMRRDTLComplete
        {
            get { return this.m_IsMRRDTLComplete; }
            set { this.m_IsMRRDTLComplete = value; }
        }

        private string m_IS_QC = string.Empty;
        public string IS_QC
        {
            get { return m_IS_QC; }
            set { m_IS_QC = value; }
        }


    }
}
