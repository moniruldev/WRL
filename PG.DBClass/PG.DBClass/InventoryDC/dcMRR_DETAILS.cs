using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "MRR_DETAILS")]
    public partial class dcMRR_DETAILS : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private Int64 m_MRR_DET_ID = 0;
        private int m_MRR_DET_SLNO = 0;
        private Int64 m_MRR_ID = 0;
        private Int64? m_PURCHASE_DET_ID = 0;
        private Int64? m_FRN_PURCHASE_DET_ID = 0;
        private int m_ITEM_ID = 0;
        private int m_UOM_ID = 0;
        private decimal m_PURCHASE_QTY = 0;
        private decimal m_UNIT_PRICE = 0;
        
        private decimal m_MRR_QTY = 0;
         
        private decimal m_REJECT_QTY = 0;
        private Int64 m_TRANS_DET_ID = 0;        
        private int m_SUP_ID = 0;
        private string m_NOTE = string.Empty;
        private decimal m_TOTAL_COST = 0;
        private int? m_ITEM_TYPE_ID = 0;
        private decimal m_MRR_AUTHO_QTY = 0;
        private int m_PURCHASE_UOM_ID = 0;
        private int m_UPDATE_BY = 0;
        private DateTime? m_UPDATE_DATE = null;
        private int? m_RTN_RCV_DET_ID = 0;
        private string m_IS_RETURN = string.Empty;
         

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

        [DBColumn(Name = "MRR_DET_ID", Storage = "m_MRR_DET_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public Int64 MRR_DET_ID
        {
            get { return this.m_MRR_DET_ID; }
            set
            {
                this.m_MRR_DET_ID = value;
                this.NotifyPropertyChanged("MRR_DET_ID");
            }
        }

        [DBColumn(Name = "MRR_DET_SLNO", Storage = "m_MRR_DET_SLNO", DbType = "107")]
        public int MRR_DET_SLNO
        {
            get { return this.m_MRR_DET_SLNO; }
            set
            {
                this.m_MRR_DET_SLNO = value;
                this.NotifyPropertyChanged("MRR_DET_SLNO");
            }
        }

        [DBColumn(Name = "MRR_ID", Storage = "m_MRR_ID", DbType = "107")]
        public Int64 MRR_ID
        {
            get { return this.m_MRR_ID; }
            set
            {
                this.m_MRR_ID = value;
                this.NotifyPropertyChanged("MRR_ID");
            }
        }

        [DBColumn(Name = "PURCHASE_DET_ID", Storage = "m_PURCHASE_DET_ID", DbType = "107")]
        public Int64? PURCHASE_DET_ID
        {
            get { return this.m_PURCHASE_DET_ID; }
            set
            {
                this.m_PURCHASE_DET_ID = value;
                this.NotifyPropertyChanged("PURCHASE_DET_ID");
            }
        }

        [DBColumn(Name = "FRN_PURCHASE_DET_ID", Storage = "m_FRN_PURCHASE_DET_ID", DbType = "107")]
        public Int64? FRN_PURCHASE_DET_ID
        {
            get { return this.m_FRN_PURCHASE_DET_ID; }
            set
            {
                this.m_FRN_PURCHASE_DET_ID = value;
                this.NotifyPropertyChanged("FRN_PURCHASE_DET_ID");
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

        [DBColumn(Name = "MRR_QTY", Storage = "m_MRR_QTY", DbType = "107")]
        public decimal MRR_QTY
        {
            get { return this.m_MRR_QTY; }
            set
            {
                this.m_MRR_QTY = value;
                this.NotifyPropertyChanged("MRR_QTY");
            }
        }


        [DBColumn(Name = "REJECT_QTY", Storage = "m_REJECT_QTY", DbType = "107")]
        public decimal REJECT_QTY
        {
            get { return this.m_REJECT_QTY; }
            set
            {
                this.m_REJECT_QTY = value;
                this.NotifyPropertyChanged("REJECT_QTY");
            }
        }

        [DBColumn(Name = "SUP_ID", Storage = "m_SUP_ID", DbType = "107")]
        public int SUP_ID
        {
            get { return this.m_SUP_ID; }
            set
            {
                this.m_SUP_ID = value;
                this.NotifyPropertyChanged("SUP_ID");
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

        [DBColumn(Name = "TRANS_DET_ID", Storage = "m_TRANS_DET_ID", DbType = "107")]
        public Int64 TRANS_DET_ID
        {
            get { return this.m_TRANS_DET_ID; }
            set
            {
                this.m_TRANS_DET_ID = value;
                this.NotifyPropertyChanged("TRANS_DET_ID");
            }
        }

        [DBColumn(Name = "TOTAL_COST", Storage = "m_TOTAL_COST", DbType = "107")]
        public decimal TOTAL_COST
        {
            get { return this.m_TOTAL_COST; }
            set
            {
                this.m_TOTAL_COST = value;
                this.NotifyPropertyChanged("TOTAL_COST");
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

        [DBColumn(Name = "MRR_AUTHO_QTY", Storage = "m_MRR_AUTHO_QTY", DbType = "107")]
        public decimal MRR_AUTHO_QTY
        {
            get { return this.m_MRR_AUTHO_QTY; }
            set
            {
                this.m_MRR_AUTHO_QTY = value;
                this.NotifyPropertyChanged("MRR_AUTHO_QTY");
            }
        }


        [DBColumn(Name = "PURCHASE_UOM_ID", Storage = "m_PURCHASE_UOM_ID", DbType = "107")]
        public int PURCHASE_UOM_ID
        {
            get { return this.m_PURCHASE_UOM_ID; }
            set
            {
                this.m_PURCHASE_UOM_ID = value;
                this.NotifyPropertyChanged("PURCHASE_UOM_ID");
            }
        }

        [DBColumn(Name = "UPDATE_BY", Storage = "m_UPDATE_BY", DbType = "107")]
        public int UPDATE_BY
        {
            get { return this.m_UPDATE_BY; }
            set
            {
                this.m_UPDATE_BY = value;
                this.NotifyPropertyChanged("UPDATE_BY");
            }
        }

        [DBColumn(Name = "UPDATE_DATE", Storage = "m_UPDATE_DATE", DbType = "107")]
        public DateTime? UPDATE_DATE
        {
            get { return this.m_UPDATE_DATE; }
            set
            {
                this.m_UPDATE_DATE = value;
                this.NotifyPropertyChanged("UPDATE_DATE");
            }
        }

        [DBColumn(Name = "RTN_RCV_DET_ID", Storage = "m_RTN_RCV_DET_ID", DbType = "107")]
        public int? RTN_RCV_DET_ID
        {
            get { return this.m_RTN_RCV_DET_ID; }
            set
            {
                this.m_RTN_RCV_DET_ID = value;
                this.NotifyPropertyChanged("RTN_RCV_DET_ID");
            }
        }

        [DBColumn(Name = "IS_RETURN", Storage = "m_IS_RETURN", DbType = "126")]
        public string IS_RETURN
        {
            get { return this.m_IS_RETURN; }
            set
            {
                this.m_IS_RETURN = value;
                this.NotifyPropertyChanged("IS_RETURN");
            }
        }

        #endregion //properties



      
    }



    public partial class dcMRR_DETAILS
    {

        private string m_sup_name = string.Empty;
        private string m_sup_address = string.Empty;
        private string m_item_name = string.Empty;
        private string m_uom_name = string.Empty;

        private decimal m_WEIGHTED_AVERAGE_PRICE = 0;
        private decimal m_alreadymrrqty = 0;
        private decimal m_unit_rate = 0;
        public string m_PURCHASE_NOTE = string.Empty;     
        public decimal m_BALANCE_QTY = 0;
       
        

        public string sup_name
        {
            get { return m_sup_name; }
            set { this.m_sup_name = value; }
        }

        public string sup_address
        {
            get { return m_sup_address; }
            set { this.m_sup_address = value; }
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

        public decimal WEIGHTED_AVERAGE_PRICE
        {
            get { return m_WEIGHTED_AVERAGE_PRICE; }
            set { this.m_WEIGHTED_AVERAGE_PRICE = value; }
        }
     
     
       
        public decimal alreadymrrqty
        {
            get { return m_alreadymrrqty; }
            set { this.m_alreadymrrqty = value; }
        }
        public decimal unit_rate
        {
            get { return m_unit_rate; }
            set { this.m_unit_rate = value; }
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
        public string PURCHASE_NOTE
        {
            get { return m_PURCHASE_NOTE; }
            set { m_PURCHASE_NOTE = value; }
        }

        public decimal MRR_BALANCE_QTY
        {
            get { return m_PURCHASE_QTY-(m_alreadymrrqty+m_REJECT_QTY); }
          
        }

        public string ITEM_CODE { get; set; }

        private string m_ITEM_TYPE_CODE = string.Empty;
        public string ITEM_TYPE_CODE
        {
            get { return m_ITEM_TYPE_CODE; }
            set { m_ITEM_TYPE_CODE = value; }
        }
    
        private bool m_IsQCComplete = false;
        public bool IsQCComplete
        {
            get { return this.m_IsQCComplete; }
            set { this.m_IsQCComplete = value; }
        }

        private bool m_IsMRRDTLComplete = false;
        public bool IsMRRDTLComplete
        {
            get { return this.m_IsMRRDTLComplete; }
            set { this.m_IsMRRDTLComplete = value; }
        }

        public decimal m_ALREADY_QC_QTY = 0;
        public decimal ALREADY_QC_QTY
        {
            get { return m_ALREADY_QC_QTY; }
            set { m_ALREADY_QC_QTY = value; }
        }


        private string m_PURCHASE_UOM_CODE = string.Empty;
        public string PURCHASE_UOM_CODE
        {
            get { return m_PURCHASE_UOM_CODE; }
            set { m_PURCHASE_UOM_CODE = value; }
        }

        private string m_IS_QC = string.Empty;
        public string IS_QC
        {
            get { return m_IS_QC; }
            set { this.m_IS_QC = value; }
        }

        private string m_MRR_NO = string.Empty;
        public string MRR_NO
        {
            get { return m_MRR_NO; }
            set { this.m_MRR_NO = value; }
        }

        private DateTime? m_MRR_DATE = null;
        public DateTime? MRR_DATE
        {
            get { return m_MRR_DATE; }
            set { this.m_MRR_DATE = value; }
        }

        private string m_CREATE_BY_NAME = string.Empty;
        public string CREATE_BY_NAME
        {
            get { return m_CREATE_BY_NAME; }
            set { this.m_CREATE_BY_NAME = value; }
        }

        public string LC_NO { get; set; }

        private decimal m_CONVERSION_FACTOR = 0;
        public decimal CONVERSION_FACTOR
        {
            get { return m_CONVERSION_FACTOR; }
            set { this.m_CONVERSION_FACTOR = value; }
        }
        public int STORE_ID { get; set; }
     


    }

}
