using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "PROD_REJECTION_DTL_ASM")]
    public partial class dcPROD_REJECTION_DTL_ASM : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private decimal m_PROD_ASM_REJ_DTL_ID = 0;
        private decimal m_PROD_ASM_REJ_ID = 0;
        private int m_FROM_DEPT_ID = 0;
        private string m_BATCH_NO = string.Empty;
        private decimal m_ITEM_ID = 0;
        private decimal m_GOOD_STOCK_QTY = 0;
        private decimal m_REJECTION_QTY = 0;
        private decimal m_UOM_ID = 0;
        private string m_REJECTION_DET_REMARKS = string.Empty;
        private decimal m_PROD_REJ_DET_SLNO = 0;
        private decimal m_REJECTION_WEIGHT = 0;
        private decimal m_ITEM_STANDARD_WEIGHT_KG = 0;

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


        [DBColumn(Name = "PROD_ASM_REJ_DTL_ID", Storage = "m_PROD_ASM_REJ_DTL_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public decimal PROD_ASM_REJ_DTL_ID
        {
            get { return this.m_PROD_ASM_REJ_DTL_ID; }
            set
            {
                this.m_PROD_ASM_REJ_DTL_ID = value;
                this.NotifyPropertyChanged("PROD_ASM_REJ_DTL_ID");
            }
        }

        [DBColumn(Name = "PROD_ASM_REJ_ID", Storage = "m_PROD_ASM_REJ_ID", DbType = "107")]
        public decimal PROD_ASM_REJ_ID
        {
            get { return this.m_PROD_ASM_REJ_ID; }
            set
            {
                this.m_PROD_ASM_REJ_ID = value;
                this.NotifyPropertyChanged("PROD_ASM_REJ_ID");
            }
        }

        [DBColumn(Name = "FROM_DEPT_ID", Storage = "m_FROM_DEPT_ID", DbType = "107")]
        public int FROM_DEPT_ID
        {
            get { return this.m_FROM_DEPT_ID; }
            set
            {
                this.m_FROM_DEPT_ID = value;
                this.NotifyPropertyChanged("FROM_DEPT_ID");
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
        public decimal ITEM_ID
        {
            get { return this.m_ITEM_ID; }
            set
            {
                this.m_ITEM_ID = value;
                this.NotifyPropertyChanged("ITEM_ID");
            }
        }

        [DBColumn(Name = "GOOD_STOCK_QTY", Storage = "m_GOOD_STOCK_QTY", DbType = "107")]
        public decimal GOOD_STOCK_QTY
        {
            get { return this.m_GOOD_STOCK_QTY; }
            set
            {
                this.m_GOOD_STOCK_QTY = value;
                this.NotifyPropertyChanged("GOOD_STOCK_QTY");
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

        [DBColumn(Name = "ITEM_STANDARD_WEIGHT_KG", Storage = "m_ITEM_STANDARD_WEIGHT_KG", DbType = "107")]
        public decimal ITEM_STANDARD_WEIGHT_KG
        {
            get { return this.m_ITEM_STANDARD_WEIGHT_KG; }
            set
            {
                this.m_ITEM_STANDARD_WEIGHT_KG = value;
                this.NotifyPropertyChanged("ITEM_STANDARD_WEIGHT_KG");
            }
        }

        #endregion //properties
    }

    public partial class dcPROD_REJECTION_DTL_ASM
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



        //private decimal m_ITEM_STANDARD_WEIGHT_KG = 0;
        //public decimal ITEM_STANDARD_WEIGHT_KG
        //{
        //    get { return this.m_ITEM_STANDARD_WEIGHT_KG; }
        //    set { this.m_ITEM_STANDARD_WEIGHT_KG = value; }
        //}


        private string m_DEPARTMENT_NAME = string.Empty;
        public string DEPARTMENT_NAME
        {
            get { return m_DEPARTMENT_NAME; }
            set { m_DEPARTMENT_NAME = value; }
        }

        private int m_DEPARTMENT_ID = 0;
         public int DEPARTMENT_ID
        {
            get { return m_DEPARTMENT_ID; }
            set { m_DEPARTMENT_ID = value; }
        }


        

    }

}
