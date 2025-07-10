using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "LC_DETAILS")]
    public partial class dcLC_DETAILS : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private Int64 m_LC_DET_ID = 0;
        private int m_LC_DET_SLNO = 0;
        private Int64 m_LC_ID = 0;
        private int m_ITEM_ID = 0;
        private decimal m_ITEM_QNTY = 0;
        private int m_UOM_ID = 0;
        private decimal m_UNIT_PRICE = 0;
        private int m_MANUFAC_ID = 0;
        private int m_COUNTRY_ID = 0;
        private string m_HS_CODE1 = string.Empty;
        private string m_HS_CODE2 = string.Empty;
        private string m_HS_CODE3 = string.Empty;
        private string m_IS_QNTY_CLOSE = string.Empty;
        private string m_ITEM_DET_REMARKS = string.Empty;
        private decimal m_ITEM_TOTAL_COST = 0;
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


        [DBColumn(Name = "LC_DET_ID", Storage = "m_LC_DET_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public Int64 LC_DET_ID
        {
            get { return this.m_LC_DET_ID; }
            set
            {
                this.m_LC_DET_ID = value;
                this.NotifyPropertyChanged("LC_DET_ID");
            }
        }

        [DBColumn(Name = "LC_DET_SLNO", Storage = "m_LC_DET_SLNO", DbType = "107")]
        public int LC_DET_SLNO
        {
            get { return this.m_LC_DET_SLNO; }
            set
            {
                this.m_LC_DET_SLNO = value;
                this.NotifyPropertyChanged("LC_DET_SLNO");
            }
        }

        [DBColumn(Name = "LC_ID", Storage = "m_LC_ID", DbType = "107")]
        public Int64 LC_ID
        {
            get { return this.m_LC_ID; }
            set
            {
                this.m_LC_ID = value;
                this.NotifyPropertyChanged("LC_ID");
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

        [DBColumn(Name = "ITEM_QNTY", Storage = "m_ITEM_QNTY", DbType = "107")]
        public decimal ITEM_QNTY
        {
            get { return this.m_ITEM_QNTY; }
            set
            {
                this.m_ITEM_QNTY = value;
                this.NotifyPropertyChanged("ITEM_QNTY");
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

      

        [DBColumn(Name = "MANUFAC_ID", Storage = "m_MANUFAC_ID", DbType = "107")]
        public int MANUFAC_ID
        {
            get { return this.m_MANUFAC_ID; }
            set
            {
                this.m_MANUFAC_ID = value;
                this.NotifyPropertyChanged("MANUFAC_ID");
            }
        }

        [DBColumn(Name = "COUNTRY_ID", Storage = "m_COUNTRY_ID", DbType = "107")]
        public int COUNTRY_ID
        {
            get { return this.m_COUNTRY_ID; }
            set
            {
                this.m_COUNTRY_ID = value;
                this.NotifyPropertyChanged("COUNTRY_ID");
            }
        }

        [DBColumn(Name = "HS_CODE1", Storage = "m_HS_CODE1", DbType = "126")]
        public string HS_CODE1
        {
            get { return this.m_HS_CODE1; }
            set
            {
                this.m_HS_CODE1 = value;
                this.NotifyPropertyChanged("HS_CODE1");
            }
        }

        [DBColumn(Name = "HS_CODE2", Storage = "m_HS_CODE2", DbType = "126")]
        public string HS_CODE2
        {
            get { return this.m_HS_CODE2; }
            set
            {
                this.m_HS_CODE2 = value;
                this.NotifyPropertyChanged("HS_CODE2");
            }
        }

        [DBColumn(Name = "HS_CODE3", Storage = "m_HS_CODE3", DbType = "126")]
        public string HS_CODE3
        {
            get { return this.m_HS_CODE3; }
            set
            {
                this.m_HS_CODE3 = value;
                this.NotifyPropertyChanged("HS_CODE3");
            }
        }

        [DBColumn(Name = "IS_QNTY_CLOSE", Storage = "m_IS_QNTY_CLOSE", DbType = "126")]
        public string IS_QNTY_CLOSE
        {
            get { return this.m_IS_QNTY_CLOSE; }
            set
            {
                this.m_IS_QNTY_CLOSE = value;
                this.NotifyPropertyChanged("IS_QNTY_CLOSE");
            }
        }


        [DBColumn(Name = "ITEM_DET_REMARKS", Storage = "m_ITEM_DET_REMARKS", DbType = "126")]
        public string ITEM_DET_REMARKS
        {
            get { return this.m_ITEM_DET_REMARKS; }
            set
            {
                this.m_ITEM_DET_REMARKS = value;
                this.NotifyPropertyChanged("ITEM_DET_REMARKS");
            }
        }


        [DBColumn(Name = "ITEM_TOTAL_COST", Storage = "m_ITEM_TOTAL_COST", DbType = "107")]
        public decimal ITEM_TOTAL_COST
        {
            get { return this.m_ITEM_TOTAL_COST; }
            set
            {
                this.m_ITEM_TOTAL_COST = value;
                this.NotifyPropertyChanged("ITEM_TOTAL_COST");
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


    public partial class dcLC_DETAILS
    {
        private int m_ITEM_GROUP_ID = 0;
        public int ITEM_GROUP_ID
        {
            get { return m_ITEM_GROUP_ID; }
            set { m_ITEM_GROUP_ID = value; }
        }
       

        private string m_ITEM_GROUP_DESC = string.Empty;
        public string ITEM_GROUP_DESC
        {
            get { return m_ITEM_GROUP_DESC; }
            set { m_ITEM_GROUP_DESC = value; }
        }

        private int m_SLNo = 0;
        public int SLNo
        {
            get { return m_SLNo; }
            set { m_SLNo = value; }
        }

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


        private int m_total_amount = 0;
        public int total_amount
        {
            get { return m_total_amount; }
            set { m_total_amount = value; }
        }

        private string m_MANUFAC_NAME = string.Empty;
        public string MANUFAC_NAME
        {
            get { return m_MANUFAC_NAME; }
            set { m_MANUFAC_NAME = value; }
        }

        private string m_SUP_NAME = string.Empty;
        public string SUP_NAME
        {
            get { return m_SUP_NAME; }
            set { m_SUP_NAME = value; }
        }


        
        private string m_COUNTRY_NAME = string.Empty;
         public string COUNTRY_NAME
        {
            get { return m_COUNTRY_NAME; }
            set { m_COUNTRY_NAME = value; }
        }


         private int m_SUP_ID = 0;
         public int SUP_ID
         {
             get { return m_SUP_ID; }
             set { m_SUP_ID = value; }
         }
        

        private decimal m_already_pur_qty = 0;
        public decimal already_pur_qty
        {
            get { return m_already_pur_qty; }
            set { m_already_pur_qty = value; }
        }


        public decimal WEIGHTED_AVERAGE_PRICE { get; set; }

        private string m_item_desc = string.Empty;
   
        private string m_item_code = string.Empty;
        public string item_desc
        {
            get { return m_item_desc; }
            set { this.m_item_desc = value; }
        }


        public string item_code
        {
            get { return m_item_code; }
            set { this.m_item_code = value; }
        }



        private Int64 m_MRR_DET_ID = 0;
        public Int64 MRR_DET_ID
        {
            get { return m_MRR_DET_ID; }
            set { m_MRR_DET_ID = value; }
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

        public string DEPARTMENT_NAME { get; set; }
        public string INDT_NO { get; set; }
        public DateTime? INDT_DATE { get; set; }

   

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

        public decimal CONVERTED_ITEM_QTY { get; set; }
        public string CONVERTED_UOM { get; set; }
        public string UOM_CODE { get; set; }
        public decimal MRR_QTY { get; set; }
        public string MRR_UOM { get; set; }

        public decimal PENDING_QNTY { get; set; }
        public decimal LC_QTY { get; set; }
        
        
    }
}
