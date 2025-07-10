using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.InventoryDC
{
    [DBTable(Name = "QC_DETAILS")]
    public partial class dcQC_DETAILS : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_QC_DET_ID = 0;
        private int m_QC_DET_SLNO = 0;
        private int m_QC_ID = 0;
        private int m_TRANS_DET_ID = 0;
        private int m_ITEM_ID = 0;
        private int m_UOM_ID = 0;
        private decimal m_QC_ITEM_QTY = 0;
        private decimal m_MRR_QTY = 0;
        private decimal m_REJECT_QTY = 0;
        private int m_SUP_ID = 0;
        private string m_QC_NOTE_DTL = string.Empty;
        private decimal m_UNIT_PRICE = 0;
        private decimal m_TOTAL_COST = 0;
        private int m_ITEM_TYPE_ID = 0;
        private decimal m_QC_PASS_QTY = 0;
        private decimal m_QC_RETURN_QTY = 0;
        private string m_APPROVED_VENDOR = string.Empty;
        private string m_ANALYSIS_CIRTIFICATE = string.Empty;
        private string m_OES_RESULT = string.Empty;
        private decimal m_PERCENTAGE_OF_LEAD = 0;
        private string m_IRON_RANGE = string.Empty;
        private decimal m_SP_GRAVITY = 0;
        private string m_TRANSPARENCY = string.Empty;
        private decimal m_NAOH_CONTENT = 0;
        private string m_APPROVED_SAMPLE = string.Empty;
        private string m_WASHER = string.Empty;

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


        [DBColumn(Name = "QC_DET_ID", Storage = "m_QC_DET_ID", DbType = "107",IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int QC_DET_ID
        {
            get { return this.m_QC_DET_ID; }
            set
            {
                this.m_QC_DET_ID = value;
                this.NotifyPropertyChanged("QC_DET_ID");
            }
        }

        [DBColumn(Name = "QC_DET_SLNO", Storage = "m_QC_DET_SLNO", DbType = "107")]
        public int QC_DET_SLNO
        {
            get { return this.m_QC_DET_SLNO; }
            set
            {
                this.m_QC_DET_SLNO = value;
                this.NotifyPropertyChanged("QC_DET_SLNO");
            }
        }

        [DBColumn(Name = "QC_ID", Storage = "m_QC_ID", DbType = "107")]
        public int QC_ID
        {
            get { return this.m_QC_ID; }
            set
            {
                this.m_QC_ID = value;
                this.NotifyPropertyChanged("QC_ID");
            }
        }

        [DBColumn(Name = "TRANS_DET_ID", Storage = "m_TRANS_DET_ID", DbType = "107")]
        public int TRANS_DET_ID
        {
            get { return this.m_TRANS_DET_ID; }
            set
            {
                this.m_TRANS_DET_ID = value;
                this.NotifyPropertyChanged("TRANS_DET_ID");
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

        [DBColumn(Name = "QC_ITEM_QTY", Storage = "m_QC_ITEM_QTY", DbType = "107")]
        public decimal QC_ITEM_QTY
        {
            get { return this.m_QC_ITEM_QTY; }
            set
            {
                this.m_QC_ITEM_QTY = value;
                this.NotifyPropertyChanged("QC_ITEM_QTY");
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

        [DBColumn(Name = "QC_NOTE_DTL", Storage = "m_QC_NOTE_DTL", DbType = "126")]
        public string QC_NOTE_DTL
        {
            get { return this.m_QC_NOTE_DTL; }
            set
            {
                this.m_QC_NOTE_DTL = value;
                this.NotifyPropertyChanged("QC_NOTE_DTL");
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
        public int ITEM_TYPE_ID
        {
            get { return this.m_ITEM_TYPE_ID; }
            set
            {
                this.m_ITEM_TYPE_ID = value;
                this.NotifyPropertyChanged("ITEM_TYPE_ID");
            }
        }

        [DBColumn(Name = "QC_PASS_QTY", Storage = "m_QC_PASS_QTY", DbType = "107")]
        public decimal QC_PASS_QTY
        {
            get { return this.m_QC_PASS_QTY; }
            set
            {
                this.m_QC_PASS_QTY = value;
                this.NotifyPropertyChanged("QC_PASS_QTY");
            }
        }

        [DBColumn(Name = "QC_RETURN_QTY", Storage = "m_QC_RETURN_QTY", DbType = "107")]
        public decimal QC_RETURN_QTY
        {
            get { return this.m_QC_RETURN_QTY; }
            set
            {
                this.m_QC_RETURN_QTY = value;
                this.NotifyPropertyChanged("QC_RETURN_QTY");
            }
        }

        [DBColumn(Name = "APPROVED_VENDOR", Storage = "m_APPROVED_VENDOR", DbType = "126")]
        public string APPROVED_VENDOR
        {
            get { return this.m_APPROVED_VENDOR; }
            set
            {
                this.m_APPROVED_VENDOR = value;
                this.NotifyPropertyChanged("APPROVED_VENDOR");
            }
        }

        [DBColumn(Name = "ANALYSIS_CIRTIFICATE", Storage = "m_ANALYSIS_CIRTIFICATE", DbType = "126")]
        public string ANALYSIS_CIRTIFICATE
        {
            get { return this.m_ANALYSIS_CIRTIFICATE; }
            set
            {
                this.m_ANALYSIS_CIRTIFICATE = value;
                this.NotifyPropertyChanged("ANALYSIS_CIRTIFICATE");
            }
        }

        [DBColumn(Name = "OES_RESULT", Storage = "m_OES_RESULT", DbType = "126")]
        public string OES_RESULT
        {
            get { return this.m_OES_RESULT; }
            set
            {
                this.m_OES_RESULT = value;
                this.NotifyPropertyChanged("OES_RESULT");
            }
        }

        [DBColumn(Name = "PERCENTAGE_OF_LEAD", Storage = "m_PERCENTAGE_OF_LEAD", DbType = "107")]
        public decimal PERCENTAGE_OF_LEAD
        {
            get { return this.m_PERCENTAGE_OF_LEAD; }
            set
            {
                this.m_PERCENTAGE_OF_LEAD = value;
                this.NotifyPropertyChanged("PERCENTAGE_OF_LEAD");
            }
        }

        [DBColumn(Name = "IRON_RANGE", Storage = "m_IRON_RANGE", DbType = "126")]
        public string IRON_RANGE
        {
            get { return this.m_IRON_RANGE; }
            set
            {
                this.m_IRON_RANGE = value;
                this.NotifyPropertyChanged("IRON_RANGE");
            }
        }

        [DBColumn(Name = "SP_GRAVITY", Storage = "m_SP_GRAVITY", DbType = "107")]
        public decimal SP_GRAVITY
        {
            get { return this.m_SP_GRAVITY; }
            set
            {
                this.m_SP_GRAVITY = value;
                this.NotifyPropertyChanged("SP_GRAVITY");
            }
        }

        [DBColumn(Name = "TRANSPARENCY", Storage = "m_TRANSPARENCY", DbType = "126")]
        public string TRANSPARENCY
        {
            get { return this.m_TRANSPARENCY; }
            set
            {
                this.m_TRANSPARENCY = value;
                this.NotifyPropertyChanged("TRANSPARENCY");
            }
        }

        [DBColumn(Name = "NAOH_CONTENT", Storage = "m_NAOH_CONTENT", DbType = "107")]
        public decimal NAOH_CONTENT
        {
            get { return this.m_NAOH_CONTENT; }
            set
            {
                this.m_NAOH_CONTENT = value;
                this.NotifyPropertyChanged("NAOH_CONTENT");
            }
        }

        [DBColumn(Name = "APPROVED_SAMPLE", Storage = "m_APPROVED_SAMPLE", DbType = "126")]
        public string APPROVED_SAMPLE
        {
            get { return this.m_APPROVED_SAMPLE; }
            set
            {
                this.m_APPROVED_SAMPLE = value;
                this.NotifyPropertyChanged("APPROVED_SAMPLE");
            }
        }

        [DBColumn(Name = "WASHER", Storage = "m_WASHER", DbType = "126")]
        public string WASHER
        {
            get { return this.m_WASHER; }
            set
            {
                this.m_WASHER = value;
                this.NotifyPropertyChanged("WASHER");
            }
        }

        #endregion //properties
    }

    public partial class dcQC_DETAILS
    {
        private string m_sup_name = string.Empty;
        private string m_sup_address = string.Empty;
        private string m_item_name = string.Empty;
        private string m_uom_name = string.Empty;
        private string m_UOM_CODE = string.Empty;
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

        public string UOM_CODE
        {
            get { return m_UOM_CODE; }
            set { this.m_UOM_CODE = value; }
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

        public decimal m_ALREADY_QC_QTY = 0;
        public decimal ALREADY_QC_QTY
        {
            get { return m_ALREADY_QC_QTY; }
            set { m_ALREADY_QC_QTY = value; }
        }

        public decimal m_BALANCE_QTY = 0;
        public decimal BALANCE_QTY
        {
            get { return m_MRR_QTY - m_ALREADY_QC_QTY; }
        }

        public decimal m_ALREADY_TRANS_QTY = 0;
        public decimal ALREADY_TRANS_QTY
        {
            get { return m_ALREADY_TRANS_QTY; }
            set { m_ALREADY_TRANS_QTY = value; }
        }

        public decimal m_TRANS_BALANCE_QTY = 0;
        public decimal TRANS_BALANCE_QTY
        {
            get { return m_MRR_QTY - m_ALREADY_TRANS_QTY; }
        }

    

        private int m_MRR_DET_ID = 0;
        public int MRR_DET_ID
        {
            get { return m_MRR_DET_ID; }
            set { m_MRR_DET_ID = value; }
        }

        public decimal m_HDN_QC_QTY = 0;
        public decimal HDN_QC_QTY
        {
            get { return m_HDN_QC_QTY; }
            set { m_HDN_QC_QTY = value; }
        }

        public string QC_NO { get; set; }
        public string NOTE{ get; set; }
        public string MRR_NO { get; set; }
        public int MRR_ID { get; set; }
        public DateTime QC_DATE { get; set; }

        private decimal m_CLOSING_QTY = 0;
        public decimal CLOSING_QTY
        {
            get { return m_CLOSING_QTY; }
            set { m_CLOSING_QTY = value; }
        }

        


    }
}
