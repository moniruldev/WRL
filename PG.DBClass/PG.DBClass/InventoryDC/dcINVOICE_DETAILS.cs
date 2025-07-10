using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "INVOICE_DETAILS")]
    public partial class dcINVOICE_DETAILS : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_INVOICE_DET_ID = 0;
        private int m_INVOICE_ID = 0;
        private decimal m_INV_DET_SLNO = 0;
        private decimal m_ITEM_ID = 0;
        private decimal m_UOM_ID = 0;
        private decimal m_ITEM_QNTY = 0;
        private decimal m_ITEM_QNTY_APRROVED = 0;
        private string m_INV_DET_REMARKS = string.Empty;
        private decimal m_CREATE_BY = 0;
        private DateTime? m_CREATE_DATE = null;
        private decimal m_EDIT_BY = 0;
        private DateTime? m_EDIT_DATE = null;
        private string m_APPROVED_NOTE = string.Empty;
        private decimal m_ITEM_RATE = 0;
        private int m_BTY_TYPE_ID = 0;
        private string m_IS_REPAIR = string.Empty;

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


        [DBColumn(Name = "INVOICE_DET_ID", Storage = "m_INVOICE_DET_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int INVOICE_DET_ID
        {
            get { return this.m_INVOICE_DET_ID; }
            set
            {
                this.m_INVOICE_DET_ID = value;
                this.NotifyPropertyChanged("INVOICE_DET_ID");
            }
        }

        [DBColumn(Name = "INVOICE_ID", Storage = "m_INVOICE_ID", DbType = "107")]
        public int INVOICE_ID
        {
            get { return this.m_INVOICE_ID; }
            set
            {
                this.m_INVOICE_ID = value;
                this.NotifyPropertyChanged("INVOICE_ID");
            }
        }

        [DBColumn(Name = "INV_DET_SLNO", Storage = "m_INV_DET_SLNO", DbType = "107")]
        public decimal INV_DET_SLNO
        {
            get { return this.m_INV_DET_SLNO; }
            set
            {
                this.m_INV_DET_SLNO = value;
                this.NotifyPropertyChanged("INV_DET_SLNO");
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

        [DBColumn(Name = "ITEM_QNTY_APRROVED", Storage = "m_ITEM_QNTY_APRROVED", DbType = "107")]
        public decimal ITEM_QNTY_APRROVED
        {
            get { return this.m_ITEM_QNTY_APRROVED; }
            set
            {
                this.m_ITEM_QNTY_APRROVED = value;
                this.NotifyPropertyChanged("ITEM_QNTY_APRROVED");
            }
        }

        [DBColumn(Name = "INV_DET_REMARKS", Storage = "m_INV_DET_REMARKS", DbType = "126")]
        public string INV_DET_REMARKS
        {
            get { return this.m_INV_DET_REMARKS; }
            set
            {
                this.m_INV_DET_REMARKS = value;
                this.NotifyPropertyChanged("INV_DET_REMARKS");
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

        [DBColumn(Name = "EDIT_BY", Storage = "m_EDIT_BY", DbType = "107")]
        public decimal EDIT_BY
        {
            get { return this.m_EDIT_BY; }
            set
            {
                this.m_EDIT_BY = value;
                this.NotifyPropertyChanged("EDIT_BY");
            }
        }

        [DBColumn(Name = "EDIT_DATE", Storage = "m_EDIT_DATE", DbType = "106")]
        public DateTime? EDIT_DATE
        {
            get { return this.m_EDIT_DATE; }
            set
            {
                this.m_EDIT_DATE = value;
                this.NotifyPropertyChanged("EDIT_DATE");
            }
        }

        [DBColumn(Name = "APPROVED_NOTE", Storage = "m_APPROVED_NOTE", DbType = "126")]
        public string APPROVED_NOTE
        {
            get { return this.m_APPROVED_NOTE; }
            set
            {
                this.m_APPROVED_NOTE = value;
                this.NotifyPropertyChanged("APPROVED_NOTE");
            }
        }

        [DBColumn(Name = "ITEM_RATE", Storage = "m_ITEM_RATE", DbType = "107")]
        public decimal ITEM_RATE
        {
            get { return this.m_ITEM_RATE; }
            set
            {
                this.m_ITEM_RATE = value;
                this.NotifyPropertyChanged("ITEM_RATE");
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

        #endregion //properties
    }


    public partial class dcINVOICE_DETAILS
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


        private decimal m_total_amount = 0;
        public decimal total_amount
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

        private string m_DC_DET_REMARKS = string.Empty;
        public string DC_DET_REMARKS
        {
            get { return m_DC_DET_REMARKS; }
            set { m_DC_DET_REMARKS = value; }
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


        private string m_IS_OUTSALE_CASH = string.Empty;
        public string IS_OUTSALE_CASH
        {
            get { return m_IS_OUTSALE_CASH; }
            set { m_IS_OUTSALE_CASH = value; }
        }


        private string m_IS_DELETED = string.Empty;
        public string IS_DELETED
        {
            get { return m_IS_DELETED; }
            set { m_IS_DELETED = value; }
        }

    }
}
