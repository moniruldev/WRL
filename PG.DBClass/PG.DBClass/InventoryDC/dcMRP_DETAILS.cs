using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "MRP_DETAILS")]
    public partial class dcMRP_DETAILS : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_MRP_DET_ID = 0;
        private decimal m_MRP_DET_SLNO = 0;
        private decimal m_MRP_ID = 0;
        private decimal m_ITEM_ID = 0;
        private decimal m_ITEM_QNTY = 0;
        private decimal m_UOM_ID = 0;
        private string m_DETAILS_NOTE = string.Empty;
        private decimal m_APRV_QNTY = 0;
        private string m_APRV_NOTE = string.Empty;
        private decimal m_CREATE_BY = 0;
        private DateTime? m_CREATE_DATE = null;
        private decimal m_UPDATE_BY = 0;
        private DateTime? m_UPDATE_DATE = null;

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


        [DBColumn(Name = "MRP_DET_ID", Storage = "m_MRP_DET_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int MRP_DET_ID
        {
            get { return this.m_MRP_DET_ID; }
            set
            {
                this.m_MRP_DET_ID = value;
                this.NotifyPropertyChanged("MRP_DET_ID");
            }
        }

        [DBColumn(Name = "MRP_DET_SLNO", Storage = "m_MRP_DET_SLNO", DbType = "107")]
        public decimal MRP_DET_SLNO
        {
            get { return this.m_MRP_DET_SLNO; }
            set
            {
                this.m_MRP_DET_SLNO = value;
                this.NotifyPropertyChanged("MRP_DET_SLNO");
            }
        }

        [DBColumn(Name = "MRP_ID", Storage = "m_MRP_ID", DbType = "107")]
        public decimal MRP_ID
        {
            get { return this.m_MRP_ID; }
            set
            {
                this.m_MRP_ID = value;
                this.NotifyPropertyChanged("MRP_ID");
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

        [DBColumn(Name = "DETAILS_NOTE", Storage = "m_DETAILS_NOTE", DbType = "126")]
        public string DETAILS_NOTE
        {
            get { return this.m_DETAILS_NOTE; }
            set
            {
                this.m_DETAILS_NOTE = value;
                this.NotifyPropertyChanged("DETAILS_NOTE");
            }
        }

        [DBColumn(Name = "APRV_QNTY", Storage = "m_APRV_QNTY", DbType = "107")]
        public decimal APRV_QNTY
        {
            get { return this.m_APRV_QNTY; }
            set
            {
                this.m_APRV_QNTY = value;
                this.NotifyPropertyChanged("APRV_QNTY");
            }
        }

        [DBColumn(Name = "APRV_NOTE", Storage = "m_APRV_NOTE", DbType = "126")]
        public string APRV_NOTE
        {
            get { return this.m_APRV_NOTE; }
            set
            {
                this.m_APRV_NOTE = value;
                this.NotifyPropertyChanged("APRV_NOTE");
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

        [DBColumn(Name = "UPDATE_BY", Storage = "m_UPDATE_BY", DbType = "107")]
        public decimal UPDATE_BY
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

        #endregion //properties

    }

    public partial class dcMRP_DETAILS
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


        private int m_total_amount = 0;
        public int total_amount
        {
            get { return m_total_amount; }
            set { m_total_amount = value; }
        }

    }


}
