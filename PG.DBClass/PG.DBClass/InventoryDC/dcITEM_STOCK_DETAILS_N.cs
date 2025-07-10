using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
    [Serializable]
    [DBTable(Name = "ITEM_STOCK_DETAILS")]
    public partial class dcITEM_STOCK_DETAILS : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private Int64 m_ITEM_STK_DET_ID = 0;
        private DateTime? m_TRANS_DATE = null;
        private decimal m_INV_TRANS_TYPE_ID = 0;
        private decimal m_INV_TRANS_DET_ID = 0;
        private decimal m_ITEM_ID = 0;
        private decimal m_UOM_ID = 0;
        private decimal m_ISS_QTY = 0;
        private decimal m_RCV_QTY = 0;
        private decimal m_TRANS_QTY = 0;
        private decimal m_UNIT_PRICE = 0;
        private string m_TRANS_REF_NO = string.Empty;
        private string m_TRANS_REMARKS = string.Empty;
        private decimal m_STORE_ID = 0;
        private DateTime? m_TRANS_TIME = null;
        private decimal m_CREATE_BY = 0;
        private DateTime? m_CREATE_DATE = null;
        private int m_BTY_TYPE_ID = 0;
        private string m_IS_REPAIR = string.Empty;
        public int m_FROM_DEPARTMENT_ID = 0;
        public int m_TO_DEPARTMENT_ID = 0;
        public int m_DEPARTMENT_ID = 0;
        private string m_IS_PRODUCTION = string.Empty;
        private DateTime? m_TRANS_DATE_TIME = null;
        private int? m_ITEM_SPECIFICATION_ID = 0;
        private string m_IS_RETURN = string.Empty;
        private int? m_ITEM_TYPE_ID = 0;
        private int m_COMPANY_ID = 0;
        private string m_MRR_NO = string.Empty;
        private string m_PROD_BATCH_NO = string.Empty;
        private int m_STLM_ID = 0;
        private string m_FORM_STATUS = string.Empty;
        private decimal m_ITEM_WEIGHT = 0;
        private int m_WEIGHT_UOM_ID = 0;

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


        [DBColumn(Name = "ITEM_STK_DET_ID", Storage = "m_ITEM_STK_DET_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public Int64 ITEM_STK_DET_ID
        {
            get { return this.m_ITEM_STK_DET_ID; }
            set
            {
                this.m_ITEM_STK_DET_ID = value;
                this.NotifyPropertyChanged("ITEM_STK_DET_ID");
            }
        }

        [DBColumn(Name = "TRANS_DATE", Storage = "m_TRANS_DATE", DbType = "106")]
        public DateTime? TRANS_DATE
        {
            get { return this.m_TRANS_DATE; }
            set
            {
                this.m_TRANS_DATE = value;
                this.NotifyPropertyChanged("TRANS_DATE");
            }
        }

        [DBColumn(Name = "INV_TRANS_TYPE_ID", Storage = "m_INV_TRANS_TYPE_ID", DbType = "107")]
        public decimal INV_TRANS_TYPE_ID
        {
            get { return this.m_INV_TRANS_TYPE_ID; }
            set
            {
                this.m_INV_TRANS_TYPE_ID = value;
                this.NotifyPropertyChanged("INV_TRANS_TYPE_ID");
            }
        }

        [DBColumn(Name = "INV_TRANS_DET_ID", Storage = "m_INV_TRANS_DET_ID", DbType = "107")]
        public decimal INV_TRANS_DET_ID
        {
            get { return this.m_INV_TRANS_DET_ID; }
            set
            {
                this.m_INV_TRANS_DET_ID = value;
                this.NotifyPropertyChanged("INV_TRANS_DET_ID");
            }
        }

        [DBColumn(Name = "ITEM_ID", Storage = "m_ITEM_ID", DbType = "107")]
        public decimal ITEM_ID
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

        [DBColumn(Name = "ISS_QTY", Storage = "m_ISS_QTY", DbType = "107")]
        public decimal ISS_QTY
        {
            get { return this.m_ISS_QTY; }
            set
            {
                this.m_ISS_QTY = value;
                this.NotifyPropertyChanged("ISS_QTY");
            }
        }

        [DBColumn(Name = "RCV_QTY", Storage = "m_RCV_QTY", DbType = "107")]
        public decimal RCV_QTY
        {
            get { return this.m_RCV_QTY; }
            set
            {
                this.m_RCV_QTY = value;
                this.NotifyPropertyChanged("RCV_QTY");
            }
        }

        [DBColumn(Name = "TRANS_QTY", Storage = "m_TRANS_QTY", DbType = "107")]
        public decimal TRANS_QTY
        {
            get { return this.m_TRANS_QTY; }
            set
            {
                this.m_TRANS_QTY = value;
                this.NotifyPropertyChanged("TRANS_QTY");
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

        [DBColumn(Name = "TRANS_REF_NO", Storage = "m_TRANS_REF_NO", DbType = "126")]
        public string TRANS_REF_NO
        {
            get { return this.m_TRANS_REF_NO; }
            set
            {
                this.m_TRANS_REF_NO = value;
                this.NotifyPropertyChanged("TRANS_REF_NO");
            }
        }

        [DBColumn(Name = "TRANS_REMARKS", Storage = "m_TRANS_REMARKS", DbType = "126")]
        public string TRANS_REMARKS
        {
            get { return this.m_TRANS_REMARKS; }
            set
            {
                this.m_TRANS_REMARKS = value;
                this.NotifyPropertyChanged("TRANS_REMARKS");
            }
        }

        [DBColumn(Name = "STORE_ID", Storage = "m_STORE_ID", DbType = "107")]
        public decimal STORE_ID
        {
            get { return this.m_STORE_ID; }
            set
            {
                this.m_STORE_ID = value;
                this.NotifyPropertyChanged("STORE_ID");
            }
        }

        [DBColumn(Name = "TRANS_TIME", Storage = "m_TRANS_TIME", DbType = "106")]
        public DateTime? TRANS_TIME
        {
            get { return this.m_TRANS_TIME; }
            set
            {
                this.m_TRANS_TIME = value;
                this.NotifyPropertyChanged("TRANS_TIME");
            }
        }

        [DBColumn(Name = "CREATE_BY", Storage = "m_CREATE_BY", DbType = "107")]
        public decimal CREATE_BY
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

        [DBColumn(Name = "BTY_TYPE_ID", Storage = "m_BTY_TYPE_ID", DbType = "107")]
        public int BTY_TYPE_ID
        {
            get { return this.m_BTY_TYPE_ID; }
            set
            {
                this.m_BTY_TYPE_ID = value;
                this.NotifyPropertyChanged("BTY_TYPE_ID");
            }

        }


        [DBColumn(Name = "FROM_DEPARTMENT_ID", Storage = "m_FROM_DEPARTMENT_ID", DbType = "107")]
        public int FROM_DEPARTMENT_ID
        {
            get { return this.m_FROM_DEPARTMENT_ID; }
            set
            {
                this.m_FROM_DEPARTMENT_ID = value;
                this.NotifyPropertyChanged("FROM_DEPARTMENT_ID");
            }
        }

        [DBColumn(Name = "TO_DEPARTMENT_ID", Storage = "m_TO_DEPARTMENT_ID", DbType = "107")]
        public int TO_DEPARTMENT_ID
        {
            get { return this.m_TO_DEPARTMENT_ID; }
            set
            {
                this.m_TO_DEPARTMENT_ID = value;
                this.NotifyPropertyChanged("TO_DEPARTMENT_ID");
            }
        }

        [DBColumn(Name = "DEPARTMENT_ID", Storage = "m_DEPARTMENT_ID", DbType = "107")]
        public int DEPARTMENT_ID
        {
            get { return this.m_DEPARTMENT_ID; }
            set
            {
                this.m_DEPARTMENT_ID = value;
                this.NotifyPropertyChanged("DEPARTMENT_ID");
            }
        }

        [DBColumn(Name = "IS_REPAIR", Storage = "m_IS_REPAIR", DbType = "126")]
        public string IS_REPAIR
        {
            get { return this.m_IS_REPAIR; }
            set
            {
                this.m_IS_REPAIR = value;
                this.NotifyPropertyChanged("IS_REPAIR");
            }

        }


        [DBColumn(Name = "IS_PRODUCTION", Storage = "m_IS_PRODUCTION", DbType = "126")]
        public string IS_PRODUCTION
        {
            get { return this.m_IS_PRODUCTION; }
            set
            {
                this.m_IS_PRODUCTION = value;
                this.NotifyPropertyChanged("IS_PRODUCTION");
            }
        }


        [DBColumn(Name = "TRANS_DATE_TIME", Storage = "m_TRANS_DATE_TIME", DbType = "106")]
        public DateTime? TRANS_DATE_TIME
        {
            get { return this.m_TRANS_DATE_TIME; }
            set
            {
                this.m_TRANS_DATE_TIME = value;
                this.NotifyPropertyChanged("TRANS_DATE_TIME");
            }
        }

        [DBColumn(Name = "ITEM_SPECIFICATION_ID", Storage = "m_ITEM_SPECIFICATION_ID", DbType = "107")]
        public int? ITEM_SPECIFICATION_ID
        {
            get { return this.m_ITEM_SPECIFICATION_ID; }
            set
            {
                this.m_ITEM_SPECIFICATION_ID = value;
                this.NotifyPropertyChanged("ITEM_SPECIFICATION_ID");
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

        [DBColumn(Name = "COMPANY_ID", Storage = "m_COMPANY_ID", DbType = "107")]
        public int COMPANY_ID
        {
            get { return this.m_COMPANY_ID; }
            set
            {
                this.m_COMPANY_ID = value;
                this.NotifyPropertyChanged("COMPANY_ID");
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

        [DBColumn(Name = "PROD_BATCH_NO", Storage = "m_PROD_BATCH_NO", DbType = "126")]
        public string PROD_BATCH_NO
        {
            get { return this.m_PROD_BATCH_NO; }
            set
            {
                this.m_PROD_BATCH_NO = value;
                this.NotifyPropertyChanged("PROD_BATCH_NO");
            }
        }


        [DBColumn(Name = "STLM_ID", Storage = "m_STLM_ID", DbType = "107")]
        public int STLM_ID
        {
            get { return this.m_STLM_ID; }
            set
            {
                this.m_STLM_ID = value;
                this.NotifyPropertyChanged("STLM_ID");
            }
        }

        [DBColumn(Name = "FORM_STATUS", Storage = "m_FORM_STATUS", DbType = "126")]
        public string FORM_STATUS
        {
            get { return this.m_FORM_STATUS; }
            set
            {
                this.m_FORM_STATUS = value;
                this.NotifyPropertyChanged("FORM_STATUS");
            }
        }

        [DBColumn(Name = "ITEM_WEIGHT", Storage = "m_ITEM_WEIGHT", DbType = "107")]
        public decimal ITEM_WEIGHT
        {
            get { return this.m_ITEM_WEIGHT; }
            set
            {
                this.m_ITEM_WEIGHT = value;
                this.NotifyPropertyChanged("ITEM_WEIGHT");
            }
        }

        [DBColumn(Name = "WEIGHT_UOM_ID", Storage = "m_WEIGHT_UOM_ID", DbType = "107")]
        public int WEIGHT_UOM_ID
        {
            get { return this.m_WEIGHT_UOM_ID; }
            set
            {
                this.m_WEIGHT_UOM_ID = value;
                this.NotifyPropertyChanged("WEIGHT_UOM_ID");
            }
        }
       

        #endregion //properties

       
    }

    public partial class dcITEM_STOCK_DETAILS
    {
        public string ITEM_NAME { get; set; }
        public string UOM_NAME { get; set; }
        public decimal OPENING_QTY { get; set; }
        public decimal CLOSING_QTY { get; set; }
        public string ITEM_CODE { get; set; }

      

    }


}
