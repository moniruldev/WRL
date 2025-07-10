using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "PROD_REJECTION_ISSUE_DTL")]
    public partial class dcPROD_REJECTION_ISSUE_DTL : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_REJ_ISSUE_DET_ID = 0;
        private int m_REJ_ISSUE_DET_SLNO = 0;
        private int m_REJ_ISSUE_ID = 0;
        private int m_ITEM_ID = 0;
        private int m_UOM_ID = 0;
        private decimal m_ISSUE_QNTY = 0;
        private string m_REJ_ISSUE_NOTE = string.Empty;
        private int m_CREATE_BY = 0;
        private DateTime? m_CREATE_DATE = null;
        private int m_UPDATE_BY = 0;
        private DateTime? m_UPDATE_DATE = null;
        private decimal m_UNIT_PRICE = 0;
        private decimal m_TOTAL_COST = 0;
        private int m_ITEM_TYPE_ID = 0;
        private int m_STLM_ID = 0;
        private decimal m_CLOSING_QTY = 0;
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


        [DBColumn(Name = "REJ_ISSUE_DET_ID", Storage = "m_REJ_ISSUE_DET_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int REJ_ISSUE_DET_ID
        {
            get { return this.m_REJ_ISSUE_DET_ID; }
            set
            {
                this.m_REJ_ISSUE_DET_ID = value;
                this.NotifyPropertyChanged("REJ_ISSUE_DET_ID");
            }
        }

        [DBColumn(Name = "REJ_ISSUE_DET_SLNO", Storage = "m_REJ_ISSUE_DET_SLNO", DbType = "107")]
        public int REJ_ISSUE_DET_SLNO
        {
            get { return this.m_REJ_ISSUE_DET_SLNO; }
            set
            {
                this.m_REJ_ISSUE_DET_SLNO = value;
                this.NotifyPropertyChanged("REJ_ISSUE_DET_SLNO");
            }
        }

        [DBColumn(Name = "REJ_ISSUE_ID", Storage = "m_REJ_ISSUE_ID", DbType = "107")]
        public int REJ_ISSUE_ID
        {
            get { return this.m_REJ_ISSUE_ID; }
            set
            {
                this.m_REJ_ISSUE_ID = value;
                this.NotifyPropertyChanged("REJ_ISSUE_ID");
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

        [DBColumn(Name = "ISSUE_QNTY", Storage = "m_ISSUE_QNTY", DbType = "107")]
        public decimal ISSUE_QNTY
        {
            get { return this.m_ISSUE_QNTY; }
            set
            {
                this.m_ISSUE_QNTY = value;
                this.NotifyPropertyChanged("ISSUE_QNTY");
            }
        }

        [DBColumn(Name = "REJ_ISSUE_NOTE", Storage = "m_REJ_ISSUE_NOTE", DbType = "126")]
        public string REJ_ISSUE_NOTE
        {
            get { return this.m_REJ_ISSUE_NOTE; }
            set
            {
                this.m_REJ_ISSUE_NOTE = value;
                this.NotifyPropertyChanged("REJ_ISSUE_NOTE");
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

        [DBColumn(Name = "UPDATE_BY", Storage = "m_UPDATE_BY", DbType = "107")]
        public int UPDATE_BY
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

        [DBColumn(Name = "CLOSING_QTY", Storage = "m_CLOSING_QTY", DbType = "107")]
        public decimal CLOSING_QTY
        {
            get { return this.m_CLOSING_QTY; }
            set
            {
                this.m_CLOSING_QTY = value;
                this.NotifyPropertyChanged("CLOSING_QTY");
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

    public partial class dcPROD_REJECTION_ISSUE_DTL
    {
        public string ITEM_NAME { get; set; }
        public string UOM_NAME { get; set; }
        public string STLM_NAME { get; set; }
        public string WEIGHT_UOM_NAME { get; set; }
        public string ITEM_CODE { get; set; }
        public string UOM_CODE { get; set; }
    }
}
