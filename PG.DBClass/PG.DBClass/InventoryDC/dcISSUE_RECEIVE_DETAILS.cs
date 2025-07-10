using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "ISSUE_RECEIVE_DETAILS")]
    public partial class dcISSUE_RECEIVE_DETAILS : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private Int64 m_ISSUE_RECEIVE_DET_ID = 0;
        private Int64 m_ISSUE_RECEIVE_ID = 0;
        private int m_ISS_RCV_DET_SLNO = 0;
        private int m_ITEM_ID = 0;
        private int m_UOM_ID = 0;
        private decimal m_RCV_QNTY = 0;
        private string m_RECEIVE_NOTE = null;
        private Int64 m_REQ_ISSUE_DET_ID = 0;
        private int? m_CREATE_BY = 0;
        private DateTime? m_CREATE_DATE = null;
        private int? m_UPDATE_BY = 0;
        private DateTime? m_UPDATE_DATE = null;
        private int m_BTY_TYPE_ID = 0;
        private string m_IS_REPAIR = string.Empty;
        private int? m_ITEM_SPECIFICATION_ID = 0;
        private int? m_ITEM_TYPE_ID = 0;
        private decimal m_UNIT_PRICE = 0;
        private decimal m_TOTAL_COST = 0;
         
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


        [DBColumn(Name = "ISSUE_RECEIVE_DET_ID", Storage = "m_ISSUE_RECEIVE_DET_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public Int64 ISSUE_RECEIVE_DET_ID
        {
            get { return this.m_ISSUE_RECEIVE_DET_ID; }
            set
            {
                this.m_ISSUE_RECEIVE_DET_ID = value;
                this.NotifyPropertyChanged("ISSUE_RECEIVE_DET_ID");
            }
        }

        [DBColumn(Name = "ISSUE_RECEIVE_ID", Storage = "m_ISSUE_RECEIVE_ID", DbType = "107")]
        public Int64 ISSUE_RECEIVE_ID
        {
            get { return this.m_ISSUE_RECEIVE_ID; }
            set
            {
                this.m_ISSUE_RECEIVE_ID = value;
                this.NotifyPropertyChanged("ISSUE_RECEIVE_ID");
            }
        }

        [DBColumn(Name = "ISS_RCV_DET_SLNO", Storage = "m_ISS_RCV_DET_SLNO", DbType = "107")]
        public int ISS_RCV_DET_SLNO
        {
            get { return this.m_ISS_RCV_DET_SLNO; }
            set
            {
                this.m_ISS_RCV_DET_SLNO = value;
                this.NotifyPropertyChanged("ISS_RCV_DET_SLNO");
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

        [DBColumn(Name = "RCV_QNTY", Storage = "m_RCV_QNTY", DbType = "107")]
        public decimal RCV_QNTY
        {
            get { return this.m_RCV_QNTY; }
            set
            {
                this.m_RCV_QNTY = value;
                this.NotifyPropertyChanged("RCV_QNTY");
            }
        }

        [DBColumn(Name = "RECEIVE_NOTE", Storage = "m_RECEIVE_NOTE", DbType = "107")]
        public string RECEIVE_NOTE
        {
            get { return this.m_RECEIVE_NOTE; }
            set
            {
                this.m_RECEIVE_NOTE = value;
                this.NotifyPropertyChanged("RECEIVE_NOTE");
            }
        }

        [DBColumn(Name = "REQ_ISSUE_DET_ID", Storage = "m_REQ_ISSUE_DET_ID", DbType = "107")]
        public Int64 REQ_ISSUE_DET_ID
        {
            get { return this.m_REQ_ISSUE_DET_ID; }
            set
            {
                this.m_REQ_ISSUE_DET_ID = value;
                this.NotifyPropertyChanged("REQ_ISSUE_DET_ID");
            }
        }

        [DBColumn(Name = "CREATE_BY", Storage = "m_CREATE_BY", DbType = "107")]
        public int? CREATE_BY
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

        [DBColumn(Name = "UPDATE_BY", Storage = "m_UPDATE_BY", DbType = "107")]
        public int? UPDATE_BY
        {
            get { return this.m_UPDATE_BY; }
            set
            {
                this.m_UPDATE_BY = value;
                this.NotifyPropertyChanged("UPDATE_BY");
            }
        }

        [DBColumn(Name = "UPDATE_DATE", Storage = "m_UPDATE_DATE", DbType = "106")]
        public DateTime? UPDATE_DATE
        {
            get { return this.m_UPDATE_DATE; }
            set
            {
                this.m_UPDATE_DATE = value;
                this.NotifyPropertyChanged("UPDATE_DATE");
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
       
        #endregion //properties



     
    }
    public partial class dcISSUE_RECEIVE_DETAILS
    {
        private string m_item_description = string.Empty;
        private string m_item_name = string.Empty;
        private string m_uom_name = string.Empty;
        private decimal? m_p_rcv_qnty = null;
        private string m_item_code = string.Empty;
        private string m_REQ_ISSUE_NOTE = string.Empty;
        private string m_SPECIFICATION_TYPE = string.Empty;
        private string m_ITEM_TYPE_CODE = string.Empty;
        public string item_description
        {
            get { return m_item_description; }
            set { this.m_item_description = value; }
        }
        public string uom_code { get; set; }
        public string uom_name
        {
            get { return m_uom_name; }
            set { this.m_uom_name = value; }
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

        public string REQ_ISSUE_NOTE
        {
            get { return m_REQ_ISSUE_NOTE; }
            set { m_REQ_ISSUE_NOTE = value; }
        }


        public string SPECIFICATION_TYPE
        {
            get { return m_SPECIFICATION_TYPE; }
            set { m_SPECIFICATION_TYPE = value; }
        }

        public string ITEM_TYPE_CODE
        {
            get { return m_ITEM_TYPE_CODE; }
            set { m_ITEM_TYPE_CODE = value; }
        }

        private decimal m_REQ_QNTY = 0;
        public decimal REQ_QNTY
        {
            get { return m_REQ_QNTY; }
            set { this.m_REQ_QNTY = value; }
        }

        

    }



}
