using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.InventoryDC
{
    [DBTable(Name = "QC_ITEM_CRITERION_DTL_TEMP")]
    public partial class dcQC_ITEM_CRITERION_DTL_TEMP : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_ITEM_CRITERION_TEMP_ID = 0;
        private int m_MRR_ID = 0;
        private int m_MRR_DET_ID = 0;
        private string m_MRR_NO = string.Empty;
        private int m_ITEM_ID = 0;
        private int m_QC_ID = 0;
        private int m_QC_DET_ID = 0;
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
        private string m_REMARKS = string.Empty;

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


        [DBColumn(Name = "ITEM_CRITERION_TEMP_ID", Storage = "m_ITEM_CRITERION_TEMP_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int ITEM_CRITERION_TEMP_ID
        {
            get { return this.m_ITEM_CRITERION_TEMP_ID; }
            set
            {
                this.m_ITEM_CRITERION_TEMP_ID = value;
                this.NotifyPropertyChanged("ITEM_CRITERION_TEMP_ID");
            }
        }

        [DBColumn(Name = "MRR_ID", Storage = "m_MRR_ID", DbType = "107")]
        public int MRR_ID
        {
            get { return this.m_MRR_ID; }
            set
            {
                this.m_MRR_ID = value;
                this.NotifyPropertyChanged("MRR_ID");
            }
        }

        [DBColumn(Name = "MRR_DET_ID", Storage = "m_MRR_DET_ID", DbType = "107")]
        public int MRR_DET_ID
        {
            get { return this.m_MRR_DET_ID; }
            set
            {
                this.m_MRR_DET_ID = value;
                this.NotifyPropertyChanged("MRR_DET_ID");
            }
        }

        [DBColumn(Name = "MRR_NO", Storage = "m_MRR_NO", DbType = "126")]
        public string MRR_NO
        {
            get { return this.m_MRR_NO; }
            set
            {
                this.m_MRR_NO = value;
                this.NotifyPropertyChanged("MRR_NO");
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

        [DBColumn(Name = "QC_DET_ID", Storage = "m_QC_DET_ID", DbType = "107")]
        public int QC_DET_ID
        {
            get { return this.m_QC_DET_ID; }
            set
            {
                this.m_QC_DET_ID = value;
                this.NotifyPropertyChanged("QC_DET_ID");
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
}
