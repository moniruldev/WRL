using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "PROD_TRANSFER_DTL")]
    public partial class dcPROD_TRANSFER_DTL : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_TRANSFER_DTL_ID = 0;
        private int m_TRANSFER_ID = 0;
        private int m_REJECTION_DTL_ID = 0;
        private int m_ITEM_ID = 0;
        private decimal m_TRANSFER_QTY = 0;
        private int m_UOM_ID = 0;
        private string m_TRANSFER_DET_REMARKS = string.Empty;
        private int m_TRANSFER_DET_SLNO = 0;
        private decimal m_TRANSFER_WEIGHT = 0;
        private decimal m_REJECTION_QTY = 0;
        private decimal m_GOOD_QTY = 0;
        private DateTime? m_TRANSFER_DATE = null;
        private decimal m_ITEM_STANDARD_WEIGHT_KG = 0;
        private int m_FROM_REJ_ITEM_ID = 0;

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


        [DBColumn(Name = "TRANSFER_DTL_ID", Storage = "m_TRANSFER_DTL_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int TRANSFER_DTL_ID
        {
            get { return this.m_TRANSFER_DTL_ID; }
            set
            {
                this.m_TRANSFER_DTL_ID = value;
                this.NotifyPropertyChanged("TRANSFER_DTL_ID");
            }
        }

        [DBColumn(Name = "TRANSFER_ID", Storage = "m_TRANSFER_ID", DbType = "107")]
        public int TRANSFER_ID
        {
            get { return this.m_TRANSFER_ID; }
            set
            {
                this.m_TRANSFER_ID = value;
                this.NotifyPropertyChanged("TRANSFER_ID");
            }
        }

        [DBColumn(Name = "REJECTION_DTL_ID", Storage = "m_REJECTION_DTL_ID", DbType = "107")]
        public int REJECTION_DTL_ID
        {
            get { return this.m_REJECTION_DTL_ID; }
            set
            {
                this.m_REJECTION_DTL_ID = value;
                this.NotifyPropertyChanged("REJECTION_DTL_ID");
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

        [DBColumn(Name = "TRANSFER_QTY", Storage = "m_TRANSFER_QTY", DbType = "107")]
        public decimal TRANSFER_QTY
        {
            get { return this.m_TRANSFER_QTY; }
            set
            {
                this.m_TRANSFER_QTY = value;
                this.NotifyPropertyChanged("TRANSFER_QTY");
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

        [DBColumn(Name = "TRANSFER_DET_REMARKS", Storage = "m_TRANSFER_DET_REMARKS", DbType = "126")]
        public string TRANSFER_DET_REMARKS
        {
            get { return this.m_TRANSFER_DET_REMARKS; }
            set
            {
                this.m_TRANSFER_DET_REMARKS = value;
                this.NotifyPropertyChanged("TRANSFER_DET_REMARKS");
            }
        }

        [DBColumn(Name = "TRANSFER_DET_SLNO", Storage = "m_TRANSFER_DET_SLNO", DbType = "107")]
        public int TRANSFER_DET_SLNO
        {
            get { return this.m_TRANSFER_DET_SLNO; }
            set
            {
                this.m_TRANSFER_DET_SLNO = value;
                this.NotifyPropertyChanged("TRANSFER_DET_SLNO");
            }
        }

        [DBColumn(Name = "TRANSFER_WEIGHT", Storage = "m_TRANSFER_WEIGHT", DbType = "107")]
        public decimal TRANSFER_WEIGHT
        {
            get { return this.m_TRANSFER_WEIGHT; }
            set
            {
                this.m_TRANSFER_WEIGHT = value;
                this.NotifyPropertyChanged("TRANSFER_WEIGHT");
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

        [DBColumn(Name = "TRANSFER_DATE", Storage = "m_TRANSFER_DATE", DbType = "106")]
        public DateTime? TRANSFER_DATE
        {
            get { return this.m_TRANSFER_DATE; }
            set
            {
                this.m_TRANSFER_DATE = value;
                this.NotifyPropertyChanged("TRANSFER_DATE");
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

        [DBColumn(Name = "FROM_REJ_ITEM_ID", Storage = "m_FROM_REJ_ITEM_ID", DbType = "107")]
        public int FROM_REJ_ITEM_ID
        {
            get { return this.m_FROM_REJ_ITEM_ID; }
            set
            {
                this.m_FROM_REJ_ITEM_ID = value;
                this.NotifyPropertyChanged("FROM_REJ_ITEM_ID");
            }
        }

        #endregion //properties
    }

    public partial class dcPROD_TRANSFER_DTL
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


        private decimal m_REMAIN_QTY = 0;
        public decimal REMAIN_QTY
        {
            get { return m_REMAIN_QTY; }
            set { m_REMAIN_QTY = value; }
        }



        private decimal m_ALREADY_ISSUED_QTY = 0;
        public decimal ALREADY_ISSUED_QTY
        {
            get { return m_ALREADY_ISSUED_QTY; }
            set { m_ALREADY_ISSUED_QTY = value; }
        }




        private decimal m_ALREADY_RNT_QNTY = 0;
        public decimal ALREADY_RNT_QNTY
        {
            get { return m_ALREADY_RNT_QNTY; }
            set { m_ALREADY_RNT_QNTY = value; }
        }


        private string m_FROM_REJ_ITEM_NAME = string.Empty;
        public string FROM_REJ_ITEM_NAME
        {
            get { return m_FROM_REJ_ITEM_NAME; }
            set { m_FROM_REJ_ITEM_NAME = value; }
        }

        public int BOM_ID { get; set; }


    }
}
