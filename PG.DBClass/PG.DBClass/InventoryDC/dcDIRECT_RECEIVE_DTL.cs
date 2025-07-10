using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.InventoryDC
{
    [DBTable(Name = "DIRECT_RECEIVE_DTL")]
    public partial class dcDIRECT_RECEIVE_DTL : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private decimal m_RCV_DET_ID = 0;
        private decimal m_RECEIVE_ID = 0;
        private decimal m_RCV_DET_SLNO = 0;
        private decimal m_ITEM_ID = 0;
        private decimal m_RECEIVE_QNTY = 0;
        private decimal m_UOM_ID = 0;
        private string m_RCV_DET_NOTE = string.Empty;

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


        [DBColumn(Name = "RCV_DET_ID", Storage = "m_RCV_DET_ID",  DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public decimal RCV_DET_ID
        {
            get { return this.m_RCV_DET_ID; }
            set
            {
                this.m_RCV_DET_ID = value;
                this.NotifyPropertyChanged("RCV_DET_ID");
            }
        }

        [DBColumn(Name = "RECEIVE_ID", Storage = "m_RECEIVE_ID", DbType = "107")]
        public decimal RECEIVE_ID
        {
            get { return this.m_RECEIVE_ID; }
            set
            {
                this.m_RECEIVE_ID = value;
                this.NotifyPropertyChanged("RECEIVE_ID");
            }
        }

        [DBColumn(Name = "RCV_DET_SLNO", Storage = "m_RCV_DET_SLNO", DbType = "107")]
        public decimal RCV_DET_SLNO
        {
            get { return this.m_RCV_DET_SLNO; }
            set
            {
                this.m_RCV_DET_SLNO = value;
                this.NotifyPropertyChanged("RCV_DET_SLNO");
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

        [DBColumn(Name = "RECEIVE_QNTY", Storage = "m_RECEIVE_QNTY", DbType = "107")]
        public decimal RECEIVE_QNTY
        {
            get { return this.m_RECEIVE_QNTY; }
            set
            {
                this.m_RECEIVE_QNTY = value;
                this.NotifyPropertyChanged("RECEIVE_QNTY");
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

        [DBColumn(Name = "RCV_DET_NOTE", Storage = "m_RCV_DET_NOTE", DbType = "126")]
        public string RCV_DET_NOTE
        {
            get { return this.m_RCV_DET_NOTE; }
            set
            {
                this.m_RCV_DET_NOTE = value;
                this.NotifyPropertyChanged("RCV_DET_NOTE");
            }
        }

        #endregion //properties
    }

    public partial class dcDIRECT_RECEIVE_DTL
    {

        private decimal m_CLOSING_QTY = 0;
        private string m_item_description = string.Empty;
        private string m_item_name = string.Empty;
        private string m_uom_name = string.Empty;
        private string m_uom_code = string.Empty;

        private decimal? m_p_rcv_qnty = null;
        private string m_item_code = string.Empty;
        public string item_description
        {
            get { return m_item_description; }
            set { this.m_item_description = value; }
        }

        public string uom_name
        {
            get { return m_uom_name; }
            set { this.m_uom_name = value; }
        }

        public string uom_code
        {
            get { return m_uom_code; }
            set { this.m_uom_code = value; }
        }
        public string item_name
        {
            get { return m_item_name; }
            set { this.m_item_name = value; }
        }
        public decimal? p_rcv_qnty
        {
            get { return m_p_rcv_qnty; }
            set { this.m_p_rcv_qnty = value; }
        }

        public string item_code
        {
            get { return m_item_code; }
            set { this.m_item_code = value; }
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


        private string m_REQ_NOTE = string.Empty;
        public string REQ_NOTE
        {
            get { return m_REQ_NOTE; }
            set { m_REQ_NOTE = value; }
        }

        public decimal CLOSING_QTY
        {
            get { return m_CLOSING_QTY; }
            set { m_CLOSING_QTY = value; }
        }

        public decimal m_ALREADRY_ISSUED_QTY = 0;
        public decimal ALREADRY_ISSUED_QTY
        {
            get { return m_ALREADRY_ISSUED_QTY; }
            set { m_ALREADRY_ISSUED_QTY = value; }
        }
        public decimal m_BALANCE_QTY = 0;
        public decimal BALANCE_QTY
        {
            get { return m_REQ_APRV_QNTY - m_ALREADRY_ISSUED_QTY; }
        }

        public decimal m_REQ_APRV_QNTY = 0;
        public decimal REQ_APRV_QNTY
        {
            get { return m_REQ_APRV_QNTY; }
            set { m_REQ_APRV_QNTY = value; }

        }
        public string SPECIFICATION_TYPE { get; set; }

        private string m_ITEM_TYPE_CODE = string.Empty;
        public string ITEM_TYPE_CODE
        {
            get { return m_ITEM_TYPE_CODE; }
            set { m_ITEM_TYPE_CODE = value; }
        }

        private bool m_IsITCDtlComplete = false;
        public bool IsITCDtlComplete
        {
            get { return this.m_IsITCDtlComplete; }
            set { this.m_IsITCDtlComplete = value; }
        }

        private decimal m_REQ_QNTY = 0;
        public decimal REQ_QNTY
        {
            get { return m_REQ_QNTY; }
            set { this.m_REQ_QNTY = value; }
        }

        public string RECEIVE_NO { get; set; }
        public DateTime? RECEIVE_DATE { get; set; }

    }
}
