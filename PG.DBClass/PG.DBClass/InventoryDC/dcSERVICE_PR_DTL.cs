using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.InventoryDC
{
    [DBTable(Name = "SERVICE_PR_DTL")]
    public partial class dcSERVICE_PR_DTL : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_INDT_DET_ID = 0;
        private int m_INDT_SLNO = 0;
        private int m_INDT_ID = 0;
        private int m_ITEM_ID = 0;
        private decimal m_INDT_QTY = 0;
        private int m_UOM_ID = 0;
        private string m_INDT_REMARKS = string.Empty;
        private decimal m_INDT_QTY_APPROVED = 0;
        private string m_APPROVED_REMARKS = string.Empty;
        private int m_CREATE_BY = 0;
        private DateTime? m_CREATE_DATE = null;
        private string m_PRIORITY = string.Empty;
        private decimal m_UNIT_PRICE = 0;
        private int m_ITEM_TYPE_ID = 0;
        private string m_ITEM_NAME = string.Empty;

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


        [DBColumn(Name = "INDT_DET_ID", Storage = "m_INDT_DET_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int INDT_DET_ID
        {
            get { return this.m_INDT_DET_ID; }
            set
            {
                this.m_INDT_DET_ID = value;
                this.NotifyPropertyChanged("INDT_DET_ID");
            }
        }

        [DBColumn(Name = "INDT_SLNO", Storage = "m_INDT_SLNO", DbType = "107")]
        public int INDT_SLNO
        {
            get { return this.m_INDT_SLNO; }
            set
            {
                this.m_INDT_SLNO = value;
                this.NotifyPropertyChanged("INDT_SLNO");
            }
        }

        [DBColumn(Name = "INDT_ID", Storage = "m_INDT_ID", DbType = "107")]
        public int INDT_ID
        {
            get { return this.m_INDT_ID; }
            set
            {
                this.m_INDT_ID = value;
                this.NotifyPropertyChanged("INDT_ID");
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

        [DBColumn(Name = "INDT_QTY", Storage = "m_INDT_QTY", DbType = "107")]
        public decimal INDT_QTY
        {
            get { return this.m_INDT_QTY; }
            set
            {
                this.m_INDT_QTY = value;
                this.NotifyPropertyChanged("INDT_QTY");
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

        [DBColumn(Name = "INDT_REMARKS", Storage = "m_INDT_REMARKS", DbType = "126")]
        public string INDT_REMARKS
        {
            get { return this.m_INDT_REMARKS; }
            set
            {
                this.m_INDT_REMARKS = value;
                this.NotifyPropertyChanged("INDT_REMARKS");
            }
        }

        [DBColumn(Name = "INDT_QTY_APPROVED", Storage = "m_INDT_QTY_APPROVED", DbType = "107")]
        public decimal INDT_QTY_APPROVED
        {
            get { return this.m_INDT_QTY_APPROVED; }
            set
            {
                this.m_INDT_QTY_APPROVED = value;
                this.NotifyPropertyChanged("INDT_QTY_APPROVED");
            }
        }

        [DBColumn(Name = "APPROVED_REMARKS", Storage = "m_APPROVED_REMARKS", DbType = "126")]
        public string APPROVED_REMARKS
        {
            get { return this.m_APPROVED_REMARKS; }
            set
            {
                this.m_APPROVED_REMARKS = value;
                this.NotifyPropertyChanged("APPROVED_REMARKS");
            }
        }

        [DBColumn(Name = "CREATE_BY", Storage = "m_CREATE_BY", DbType = "107")]
        public int CREATE_BY
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

        [DBColumn(Name = "PRIORITY", Storage = "m_PRIORITY", DbType = "126")]
        public string PRIORITY
        {
            get { return this.m_PRIORITY; }
            set
            {
                this.m_PRIORITY = value;
                this.NotifyPropertyChanged("PRIORITY");
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

        [DBColumn(Name = "ITEM_NAME", Storage = "m_ITEM_NAME", DbType = "126")]
        public string ITEM_NAME
        {
            get { return this.m_ITEM_NAME; }
            set
            {
                this.m_ITEM_NAME = value;
                this.NotifyPropertyChanged("ITEM_NAME");
            }
        }

        #endregion //properties
    }

    public partial class dcSERVICE_PR_DTL
    {
        public string UOM_NAME { get; set; }
    }
}
