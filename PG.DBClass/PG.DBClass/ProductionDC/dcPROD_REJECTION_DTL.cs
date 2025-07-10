using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "PROD_REJECTION_DTL")]
    public partial class dcPROD_REJECTION_DTL : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_PROD_REJECTION_DTL_ID = 0;
        private int m_PROD_REJECTION_ID = 0;
        private string m_BATCH_NO = string.Empty;
        private int m_ITEM_ID = 0;
        private decimal m_REJECTION_QTY = 0;
        private int m_UOM_ID = 0;
        private string m_REJECTION_DET_REMARKS = string.Empty;
        private decimal m_PROD_REJ_DET_SLNO = 0;
        private decimal m_REJECTION_WEIGHT = 0;
        private decimal m_PRODUCTION_QTY = 0;
        private decimal m_GOOD_QTY = 0;
        private DateTime? m_REJECTION_DATE = null;
        private decimal m_ITEM_STANDARD_WEIGHT_KG = 0;
        private int m_TO_REJ_ITEM_ID = 0;
        private string m_IS_BATCH = string.Empty;
        private int m_PROD_ID = 0;
        private int m_BTY_TYPE_ID = 0;
        private string m_IS_BREAKING = string.Empty;
        private int m_TO_REJ_UOM_ID = 0;


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


        [DBColumn(Name = "PROD_REJECTION_DTL_ID", Storage = "m_PROD_REJECTION_DTL_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int PROD_REJECTION_DTL_ID
        {
            get { return this.m_PROD_REJECTION_DTL_ID; }
            set
            {
                this.m_PROD_REJECTION_DTL_ID = value;
                this.NotifyPropertyChanged("PROD_REJECTION_DTL_ID");
            }
        }

        [DBColumn(Name = "PROD_REJECTION_ID", Storage = "m_PROD_REJECTION_ID", DbType = "107")]
        public int PROD_REJECTION_ID
        {
            get { return this.m_PROD_REJECTION_ID; }
            set
            {
                this.m_PROD_REJECTION_ID = value;
                this.NotifyPropertyChanged("PROD_REJECTION_ID");
            }
        }

        [DBColumn(Name = "BATCH_NO", Storage = "m_BATCH_NO", DbType = "126")]
        public string BATCH_NO
        {
            get { return this.m_BATCH_NO; }
            set
            {
                this.m_BATCH_NO = value;
                this.NotifyPropertyChanged("BATCH_NO");
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

        [DBColumn(Name = "PRODUCTION_QTY", Storage = "m_PRODUCTION_QTY", DbType = "107")]
        public decimal PRODUCTION_QTY
        {
            get { return this.m_PRODUCTION_QTY; }
            set
            {
                this.m_PRODUCTION_QTY = value;
                this.NotifyPropertyChanged("PRODUCTION_QTY");
            }
        }

        [DBColumn(Name = "REJECTION_QTY", Storage = "m_REJECTION_QTY", DbType = "107")]
        public decimal REJECTION_QTY
        {
            get { return this.m_REJECTION_QTY; }
            set
            {
                this.m_REJECTION_QTY = value;
                this.NotifyPropertyChanged("REJECTION_QTY");
            }
        }

        [DBColumn(Name = "GOOD_QTY", Storage = "m_GOOD_QTY", DbType = "107")]
        public decimal GOOD_QTY
        {
            get { return this.m_GOOD_QTY; }
            set
            {
                this.m_GOOD_QTY = value;
                this.NotifyPropertyChanged("GOOD_QTY");
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

        [DBColumn(Name = "REJECTION_DET_REMARKS", Storage = "m_REJECTION_DET_REMARKS", DbType = "126")]
        public string REJECTION_DET_REMARKS
        {
            get { return this.m_REJECTION_DET_REMARKS; }
            set
            {
                this.m_REJECTION_DET_REMARKS = value;
                this.NotifyPropertyChanged("REJECTION_DET_REMARKS");
            }
        }
         [DBColumn(Name = "PROD_REJ_DET_SLNO", Storage = "m_PROD_REJ_DET_SLNO", DbType = "107")]
        public decimal PROD_REJ_DET_SLNO
        {
            get { return this.m_PROD_REJ_DET_SLNO; }
            set
            {
                this.m_PROD_REJ_DET_SLNO = value;
                this.NotifyPropertyChanged("PROD_REJ_DET_SLNO");
            }
        }

         [DBColumn(Name = "REJECTION_WEIGHT", Storage = "m_REJECTION_WEIGHT", DbType = "107")]
         public decimal REJECTION_WEIGHT
         {
             get { return this.m_REJECTION_WEIGHT; }
             set
             {
                 this.m_REJECTION_WEIGHT = value;
                 this.NotifyPropertyChanged("REJECTION_WEIGHT");
             }
         }


         [DBColumn(Name = "REJECTION_DATE", Storage = "m_REJECTION_DATE", DbType = "106")]
         public DateTime? REJECTION_DATE
         {
             get { return this.m_REJECTION_DATE; }
             set
             {
                 this.m_REJECTION_DATE = value;
                 this.NotifyPropertyChanged("REJECTION_DATE");
             }
         }

           [DBColumn(Name = "ITEM_STANDARD_WEIGHT_KG", Storage = "m_ITEM_STANDARD_WEIGHT_KG", DbType = "106")]
         public decimal ITEM_STANDARD_WEIGHT_KG
         {
             get { return this.m_ITEM_STANDARD_WEIGHT_KG; }
             set { this.m_ITEM_STANDARD_WEIGHT_KG = value;
             this.NotifyPropertyChanged("ITEM_STANDARD_WEIGHT_KG");
             }
         }

           [DBColumn(Name = "TO_REJ_ITEM_ID", Storage = "m_TO_REJ_ITEM_ID", DbType = "106")]
           public int TO_REJ_ITEM_ID
           {
               get { return this.m_TO_REJ_ITEM_ID; }
               set
               {
                   this.m_TO_REJ_ITEM_ID = value;
                   this.NotifyPropertyChanged("TO_REJ_ITEM_ID");
               }
           }

           [DBColumn(Name = "IS_BATCH", Storage = "m_IS_BATCH", DbType = "126")]
           public string IS_BATCH
           {
               get { return this.m_IS_BATCH; }
               set
               {
                   this.m_IS_BATCH = value;
                   this.NotifyPropertyChanged("IS_BATCH");
               }
           }

           [DBColumn(Name = "PROD_ID", Storage = "m_PROD_ID", DbType = "107")]
           public int PROD_ID
           {
               get { return this.m_PROD_ID; }
               set
               {
                   this.m_PROD_ID = value;
                   this.NotifyPropertyChanged("PROD_ID");
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

        
           [DBColumn(Name = "IS_BREAKING", Storage = "m_IS_BREAKING", DbType = "126")]
           public string IS_BREAKING
           {
               get { return this.m_IS_BREAKING; }
               set
               {
                   this.m_IS_BREAKING = value;
                   this.NotifyPropertyChanged("IS_BREAKING");
               }
           }

           [DBColumn(Name = "TO_REJ_UOM_ID", Storage = "m_TO_REJ_UOM_ID", DbType = "107")]
           public int TO_REJ_UOM_ID
           {
               get { return this.m_TO_REJ_UOM_ID; }
               set
               {
                   this.m_TO_REJ_UOM_ID = value;
                   this.NotifyPropertyChanged("TO_REJ_UOM_ID");
               }
           }
        #endregion //properties
    }

    public partial class dcPROD_REJECTION_DTL
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

        private string m_UOM_CODE = string.Empty;
        public string UOM_CODE
        {
            get { return m_UOM_CODE; }
            set { m_UOM_CODE = value; }
        }

        private string m_TO_REJ_UOM_NAME = string.Empty;
        public string TO_REJ_UOM_NAME
        {
            get { return m_TO_REJ_UOM_NAME; }
            set { m_TO_REJ_UOM_NAME = value; }
        }

        private int m_total_amount = 0;
        public int total_amount
        {
            get { return m_total_amount; }
            set { m_total_amount = value; }
        }

        private decimal m_CLOSING_QTY = 0;
        public decimal CLOSING_QTY
        {

            get { return m_CLOSING_QTY; }
            set { this.m_CLOSING_QTY = value; }
        }

        private int m_ISS_RCV_DET_SLNO = 0;
        public int ISS_RCV_DET_SLNO
        {
            get { return m_ISS_RCV_DET_SLNO; }
            set { m_ISS_RCV_DET_SLNO = value; }
        }

        private int m_RCV_QNTY = 0;
        public int RCV_QNTY
        {
            get { return m_RCV_QNTY; }
            set { m_RCV_QNTY = value; }
        }

        private int m_INVOICE_ID = 0;
        public int INVOICE_ID
        {
            get { return m_INVOICE_ID; }
            set { m_INVOICE_ID = value; }
        }

        private decimal m_REMAIN_QTY = 0;
        public decimal REMAIN_QTY
        {
            get { return m_REMAIN_QTY; }
            set { m_REMAIN_QTY = value; }
        }



        private decimal m_DC_QTY = 0;
        public decimal DC_QTY
        {
            get { return m_DC_QTY; }
            set { m_DC_QTY = value; }
        }

        private decimal m_INV_QTY = 0;
        public decimal INV_QTY
        {
            get { return m_INV_QTY; }
            set { m_INV_QTY = value; }
        }

        private decimal m_ALREADY_ISSUED_QTY = 0;
        public decimal ALREADY_ISSUED_QTY
        {
            get { return m_ALREADY_ISSUED_QTY; }
            set { m_ALREADY_ISSUED_QTY = value; }
        }

        private string m_INV_DET_REMARKS = string.Empty;
        public string INV_DET_REMARKS
        {
            get { return m_INV_DET_REMARKS; }
            set { m_INV_DET_REMARKS = value; }
        }

        private decimal m_SLNO = 0;
        public decimal SLNO
        {
            get { return m_SLNO; }
            set { m_SLNO = value; }
        }

        private string m_RTN_DET_NOTE = string.Empty;
        public string RTN_DET_NOTE
        {
            get { return m_RTN_DET_NOTE; }
            set { m_RTN_DET_NOTE = value; }
        }


        private decimal m_ALREADY_RNT_QNTY = 0;
        public decimal ALREADY_RNT_QNTY
        {
            get { return m_ALREADY_RNT_QNTY; }
            set { m_ALREADY_RNT_QNTY = value; }
        }

        private int m_PANEL_PC = 0;
        public int PANEL_PC
        {
            get { return this.m_PANEL_PC; }
            set { this.m_PANEL_PC = value; }
        }




        private string m_TO_REJ_ITEM_NAME = string.Empty;
        public string TO_REJ_ITEM_NAME
        {
            get { return m_TO_REJ_ITEM_NAME; }
            set { m_TO_REJ_ITEM_NAME = value; }
        }

        public int BOM_ID { get; set; }
        public string PROD_REJECTION_NO { get; set; }


    }
}
