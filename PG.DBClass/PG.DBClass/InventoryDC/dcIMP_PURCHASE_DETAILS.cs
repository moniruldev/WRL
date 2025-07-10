using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
    [Serializable]
    [DBTable(Name = "IMP_PURCHASE_DETAILS")]
    public partial class dcIMP_PURCHASE_DETAILS : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private Int64 m_IMP_PUR_DET_ID = 0;
        private Int64 m_IMP_PURCHASE_ID = 0;
        private Int64 m_LC_DET_ID = 0;
        private int m_IMP_PUR_DET_SLNO = 0;
        private int m_ITEM_ID = 0;
        private int m_UOM_ID = 0;
        private int m_SUP_ID = 0;
        private decimal m_PUR_QTY = 0;
        private decimal m_PUR_UNIT_RATE = 0;
        private decimal m_PUR_COST = 0;
        private string m_REMARKS = string.Empty;
        private string m_IS_CLOASED = "N";
        private decimal m_LESS_QTY = 0;
        private string m_DIFF_REMARKS = string.Empty;
        private int? m_DIFF_BY = 0;
        private DateTime? m_DIFF_DATE= null;
        private decimal m_EXTRA_QTY = 0;
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


        [DBColumn(Name = "IMP_PUR_DET_ID", Storage = "m_IMP_PUR_DET_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public Int64 IMP_PUR_DET_ID
        {
            get { return this.m_IMP_PUR_DET_ID; }
            set
            {
                this.m_IMP_PUR_DET_ID = value;
                this.NotifyPropertyChanged("IMP_PUR_DET_ID");
            }
        }

        [DBColumn(Name = "IMP_PURCHASE_ID", Storage = "m_IMP_PURCHASE_ID", DbType = "107")]
        public Int64 IMP_PURCHASE_ID
        {
            get { return this.m_IMP_PURCHASE_ID; }
            set
            {
                this.m_IMP_PURCHASE_ID = value;
                this.NotifyPropertyChanged("IMP_PURCHASE_ID");
            }
        }

        [DBColumn(Name = "IMP_PUR_DET_SLNO", Storage = "m_IMP_PUR_DET_SLNO", DbType = "107")]
        public int IMP_PUR_DET_SLNO
        {
            get { return this.m_IMP_PUR_DET_SLNO; }
            set
            {
                this.m_IMP_PUR_DET_SLNO = value;
                this.NotifyPropertyChanged("IMP_PUR_DET_SLNO");
            }
        }


        [DBColumn(Name = "LC_DET_ID", Storage = "m_LC_DET_ID", DbType = "107")]
        public Int64 LC_DET_ID
        {
            get { return this.m_LC_DET_ID; }
            set
            {
                this.m_LC_DET_ID = value;
                this.NotifyPropertyChanged("LC_DET_ID");
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

        [DBColumn(Name = "PUR_QTY", Storage = "m_PUR_QTY", DbType = "107")]
        public decimal PUR_QTY
        {
            get { return this.m_PUR_QTY; }
            set
            {
                this.m_PUR_QTY = value;
                this.NotifyPropertyChanged("PUR_QTY");
            }
        }

        [DBColumn(Name = "PUR_UNIT_RATE", Storage = "m_PUR_UNIT_RATE", DbType = "107")]
        public decimal PUR_UNIT_RATE
        {
            get { return this.m_PUR_UNIT_RATE; }
            set
            {
                this.m_PUR_UNIT_RATE = value;
                this.NotifyPropertyChanged("PUR_UNIT_RATE");
            }
        }

        [DBColumn(Name = "PUR_COST", Storage = "m_PUR_COST", DbType = "107")]
        public decimal PUR_COST
        {
            get { return this.m_PUR_COST; }
            set
            {
                this.m_PUR_COST = value;
                this.NotifyPropertyChanged("PUR_COST");
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

        [DBColumn(Name = "DIFF_REMARKS", Storage = "m_DIFF_REMARKS", DbType = "126")]
        public string DIFF_REMARKS
        {
            get { return this.m_DIFF_REMARKS; }
            set
            {
                this.m_DIFF_REMARKS = value;
                this.NotifyPropertyChanged("DIFF_REMARKS");
            }
        }


        [DBColumn(Name = "LESS_QTY", Storage = "m_LESS_QTY", DbType = "107")]
        public decimal LESS_QTY
        {
            get { return this.m_LESS_QTY; }
            set
            {
                this.m_LESS_QTY = value;
                this.NotifyPropertyChanged("LESS_QTY");
            }
        }

        [DBColumn(Name = "EXTRA_QTY", Storage = "m_EXTRA_QTY", DbType = "107")]
        public decimal EXTRA_QTY
        {
            get { return this.m_EXTRA_QTY; }
            set
            {
                this.m_EXTRA_QTY = value;
                this.NotifyPropertyChanged("EXTRA_QTY");
            }
        }

        

        [DBColumn(Name = "DIFF_BY", Storage = "m_DIFF_BY", DbType = "107")]
        public int? DIFF_BY
        {
            get { return this.m_DIFF_BY; }
            set
            {
                this.m_DIFF_BY = value;
                this.NotifyPropertyChanged("DIFF_BY");
            }
        }



        [DBColumn(Name = "DIFF_DATE", Storage = "m_DIFF_DATE", DbType = "107")]
        public DateTime? DIFF_DATE
        {
            get { return this.m_DIFF_DATE; }
            set
            {
                this.m_DIFF_DATE = value;
                this.NotifyPropertyChanged("DIFF_DATE");
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


    public partial class dcIMP_PURCHASE_DETAILS
    {

        private string m_item_desc = string.Empty;
        private string m_item_name = string.Empty;
        private string m_uom_name = null;
        private string m_item_code = string.Empty;
        private string m_sup_name = string.Empty;
        private decimal m_ALREADY_PUR_QTY = 0;
        private decimal m_LC_QTY = 0;
        private string m_LC_REMARKS = string.Empty;
        private decimal m_ALREADY_MRR_QTY = 0;
        private decimal m_MRR_BALANCE_QTY = 0;
        private string m_ITEM_TYPE_CODE = string.Empty;
        public string item_desc
        {
            get { return m_item_desc; }
            set { this.m_item_desc = value; }
        }

        private string m_ITEM_NAME = string.Empty;
        public string ITEM_NAME
        {
            get { return m_ITEM_NAME; }
            set { m_ITEM_NAME = value; }
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

        private string m_UOM_NAME = string.Empty;
        public string UOM_NAME
        {
            get { return m_UOM_NAME; }
            set { m_UOM_NAME = value; }
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

        public decimal ALREADY_PUR_QTY
        {
            get { return m_ALREADY_PUR_QTY; }
            set { this.m_ALREADY_PUR_QTY = value; }
        }

        public decimal LC_QTY
        {
            get { return m_LC_QTY; }
            set { this.m_LC_QTY = value; }
        }
        public decimal BALANCE_QTY
        {
            get { return m_LC_QTY - m_ALREADY_PUR_QTY; }

        }

        public decimal MRR_BALANCE_QTY
        {
            get { return m_PUR_QTY - m_ALREADY_MRR_QTY; }

        }
        public decimal ALREADY_MRR_QTY
        {
            get { return m_ALREADY_MRR_QTY; }
            set { this.m_ALREADY_MRR_QTY = value; }

        }


        public string LC_REMARKS
        {
            get { return m_LC_REMARKS; }
            set { this.m_LC_REMARKS = value; }

        }

        public decimal WEIGHTED_AVERAGE_PRICE { get; set; }

        public string ITEM_TYPE_CODE
        {
            get { return m_ITEM_TYPE_CODE; }
            set { this.m_ITEM_TYPE_CODE = value; }

        }

        private bool m_IsMRR_IMP_DTLComplete = false;
        public bool IsMRR_IMP_DTLComplete
        {
            get { return this.m_IsMRR_IMP_DTLComplete; }
            set { this.m_IsMRR_IMP_DTLComplete = value; }
        }

        public int LC_ID { get; set; }

        private decimal m_LC_RATE = 0;
        public decimal LC_RATE
        {
            get { return m_LC_RATE; }
            set { this.m_LC_RATE = value; }
        }



        private string m_item_group_name = string.Empty;
        public string item_group_name
        {
            get { return m_item_group_name; }
            set { m_item_group_name = value; }
        }

        public decimal CONVERTED_ITEM_QTY { get; set; }
        public string CONVERTED_UOM { get; set; }

    }
}
