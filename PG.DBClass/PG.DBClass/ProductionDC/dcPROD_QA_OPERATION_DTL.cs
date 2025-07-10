using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "PROD_QA_OPERATION_DTL")]
    public partial class dcPROD_QA_OPERATION_DTL : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_PROD_QA_DTL_ID = 0;
        private int m_PROD_QA_ID = 0;
        private int m_ITEM_ID = 0;
        private decimal m_ITEM_QTY = 0;
        private decimal m_PASS_QTY = 0;
        private decimal m_REJECT_QTY = 0;
        private decimal m_WEIGHT = 0;
        private decimal m_WEIGHT_UOM_ID = 0;
        private decimal m_THIKNESS = 0;
        private decimal m_THIKNESS_UOM_ID = 0;
        private string m_FRAME_CRACK = string.Empty;
        private string m_CAVITY = string.Empty;
        private string m_WINDOW_MISSING = string.Empty;
        private string m_FEATHER = string.Empty;
        private decimal m_SPINE_DIAMETER = 0;
        private decimal m_SPINE_DIAMETER_UOM_ID = 0;
        private string m_REMARKS = string.Empty;
        private int m_PROD_DTL_ID = 0;
        private string m_PROD_BATCH_NO_DTL = string.Empty;
        
        private decimal m_WEIGHT_2 = 0;
        private decimal m_THIKNESS_2 = 0;
        private int m_PENDING_QTY = 0;
        
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


        [DBColumn(Name = "PROD_QA_DTL_ID", Storage = "m_PROD_QA_DTL_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int PROD_QA_DTL_ID
        {
            get { return this.m_PROD_QA_DTL_ID; }
            set
            {
                this.m_PROD_QA_DTL_ID = value;
                this.NotifyPropertyChanged("PROD_QA_DTL_ID");
            }
        }

        [DBColumn(Name = "PROD_QA_ID", Storage = "m_PROD_QA_ID", DbType = "107")]
        public int PROD_QA_ID
        {
            get { return this.m_PROD_QA_ID; }
            set
            {
                this.m_PROD_QA_ID = value;
                this.NotifyPropertyChanged("PROD_QA_ID");
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

        [DBColumn(Name = "PASS_QTY", Storage = "m_PASS_QTY", DbType = "107")]
        public decimal PASS_QTY
        {
            get { return this.m_PASS_QTY; }
            set
            {
                this.m_PASS_QTY = value;
                this.NotifyPropertyChanged("PASS_QTY");
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

        [DBColumn(Name = "WEIGHT", Storage = "m_WEIGHT", DbType = "107")]
        public decimal WEIGHT
        {
            get { return this.m_WEIGHT; }
            set
            {
                this.m_WEIGHT = value;
                this.NotifyPropertyChanged("WEIGHT");
            }
        }

        [DBColumn(Name = "WEIGHT_UOM_ID", Storage = "m_WEIGHT_UOM_ID", DbType = "107")]
        public decimal WEIGHT_UOM_ID
        {
            get { return this.m_WEIGHT_UOM_ID; }
            set
            {
                this.m_WEIGHT_UOM_ID = value;
                this.NotifyPropertyChanged("WEIGHT_UOM_ID");
            }
        }

        [DBColumn(Name = "THIKNESS", Storage = "m_THIKNESS", DbType = "107")]
        public decimal THIKNESS
        {
            get { return this.m_THIKNESS; }
            set
            {
                this.m_THIKNESS = value;
                this.NotifyPropertyChanged("THIKNESS");
            }
        }

        [DBColumn(Name = "THIKNESS_UOM_ID", Storage = "m_THIKNESS_UOM_ID", DbType = "107")]
        public decimal THIKNESS_UOM_ID
        {
            get { return this.m_THIKNESS_UOM_ID; }
            set
            {
                this.m_THIKNESS_UOM_ID = value;
                this.NotifyPropertyChanged("THIKNESS_UOM_ID");
            }
        }

        [DBColumn(Name = "FRAME_CRACK", Storage = "m_FRAME_CRACK", DbType = "126")]
        public string FRAME_CRACK
        {
            get { return this.m_FRAME_CRACK; }
            set
            {
                this.m_FRAME_CRACK = value;
                this.NotifyPropertyChanged("FRAME_CRACK");
            }
        }

        [DBColumn(Name = "CAVITY", Storage = "m_CAVITY", DbType = "126")]
        public string CAVITY
        {
            get { return this.m_CAVITY; }
            set
            {
                this.m_CAVITY = value;
                this.NotifyPropertyChanged("CAVITY");
            }
        }

        [DBColumn(Name = "WINDOW_MISSING", Storage = "m_WINDOW_MISSING", DbType = "126")]
        public string WINDOW_MISSING
        {
            get { return this.m_WINDOW_MISSING; }
            set
            {
                this.m_WINDOW_MISSING = value;
                this.NotifyPropertyChanged("WINDOW_MISSING");
            }
        }

        [DBColumn(Name = "FEATHER", Storage = "m_FEATHER", DbType = "126")]
        public string FEATHER
        {
            get { return this.m_FEATHER; }
            set
            {
                this.m_FEATHER = value;
                this.NotifyPropertyChanged("FEATHER");
            }
        }

        [DBColumn(Name = "SPINE_DIAMETER", Storage = "m_SPINE_DIAMETER", DbType = "107")]
        public decimal SPINE_DIAMETER
        {
            get { return this.m_SPINE_DIAMETER; }
            set
            {
                this.m_SPINE_DIAMETER = value;
                this.NotifyPropertyChanged("SPINE_DIAMETER");
            }
        }

        [DBColumn(Name = "SPINE_DIAMETER_UOM_ID", Storage = "m_SPINE_DIAMETER_UOM_ID", DbType = "107")]
        public decimal SPINE_DIAMETER_UOM_ID
        {
            get { return this.m_SPINE_DIAMETER_UOM_ID; }
            set
            {
                this.m_SPINE_DIAMETER_UOM_ID = value;
                this.NotifyPropertyChanged("SPINE_DIAMETER_UOM_ID");
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


        [DBColumn(Name = "PROD_DTL_ID", Storage = "m_PROD_DTL_ID", DbType = "107")]
        public int PROD_DTL_ID
        {
            get { return this.m_PROD_DTL_ID; }
            set
            {
                this.m_PROD_DTL_ID = value;
                this.NotifyPropertyChanged("PROD_DTL_ID");
            }
        }

         [DBColumn(Name = "PROD_BATCH_NO_DTL", Storage = "m_PROD_BATCH_NO_DTL", DbType = "126")]
        public string PROD_BATCH_NO_DTL
        {
            get { return this.m_PROD_BATCH_NO_DTL; }
            set
            {
                this.m_PROD_BATCH_NO_DTL = value;
                this.NotifyPropertyChanged("PROD_BATCH_NO_DTL");
            }
        }

        
        [DBColumn(Name = "WEIGHT_2", Storage = "m_WEIGHT_2", DbType = "107")]
        public decimal WEIGHT_2
        {
            get { return this.m_WEIGHT_2; }
            set
            {
                this.m_WEIGHT_2 = value;
                this.NotifyPropertyChanged("WEIGHT_2");
            }
        }

        [DBColumn(Name = "THIKNESS_2", Storage = "m_THIKNESS_2", DbType = "107")]
        public decimal THIKNESS_2
        {
            get { return this.m_THIKNESS_2; }
            set
            {
                this.m_THIKNESS_2 = value;
                this.NotifyPropertyChanged("THIKNESS_2");
            }
        }

        [DBColumn(Name = "PENDING_QTY", Storage = "m_PENDING_QTY", DbType = "107")]
        public int PENDING_QTY
        {
            get { return this.m_PENDING_QTY; }
            set
            {
                this.m_PENDING_QTY = value;
                this.NotifyPropertyChanged("PENDING_QTY");
            }
        }
        
        #endregion //properties
    }

    public partial class dcPROD_QA_OPERATION_DTL
    {
        private string m_sup_name = string.Empty;
        private string m_sup_address = string.Empty;
        private string m_item_name = string.Empty;
        private string m_uom_name = string.Empty;
        private decimal m_unit_rate = 0;
        private decimal m_WEIGHTED_AVERAGE_PRICE = 0;
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
        public decimal unit_rate
        {
            get { return m_unit_rate; }
            set { this.m_unit_rate = value; }
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

        public decimal m_ALREADY_PASS_QTY = 0;
        public decimal ALREADY_PASS_QTY
        {
            get { return m_ALREADY_PASS_QTY; }
            set { m_ALREADY_PASS_QTY = value; }
        }


        public decimal m_HDN_QC_QTY = 0;
        public decimal HDN_QC_QTY
        {
            get { return m_HDN_QC_QTY; }
            set { m_HDN_QC_QTY = value; }
        }

        public string PROD_QA_NO { get; set; }
        public string NOTE { get; set; }
      
        public DateTime QC_DATE { get; set; }

        public int QA_STATUS_ID { get; set; }
        



    }
}
