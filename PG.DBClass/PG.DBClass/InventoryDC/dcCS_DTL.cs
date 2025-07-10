using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.InventoryDC
{
    [DBTable(Name = "CS_DTL")]
    public partial class dcCS_DTL : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private decimal m_CS_DTL_ID = 0;
        private decimal m_CS_MST_ID = 0;
        private decimal m_QUOTATION_MST_ID = 0;
        private decimal m_SUP_ID = 0;
        private decimal m_ITEM_ID = 0;
        private decimal m_UOM_ID = 0;
        private decimal m_ITEM_QTY = 0;
        private decimal m_ITEM_RATE = 0;
        private decimal m_DISCOUNT = 0;
        private string m_CS_DTL_REMARKS = string.Empty;
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


        [DBColumn(Name = "CS_DTL_ID", Storage = "m_CS_DTL_ID", DbType = "107", IsPrimaryKey = true)]
        public decimal CS_DTL_ID
        {
            get { return this.m_CS_DTL_ID; }
            set
            {
                this.m_CS_DTL_ID = value;
                this.NotifyPropertyChanged("CS_DTL_ID");
            }
        }

        [DBColumn(Name = "CS_MST_ID", Storage = "m_CS_MST_ID", DbType = "107")]
        public decimal CS_MST_ID
        {
            get { return this.m_CS_MST_ID; }
            set
            {
                this.m_CS_MST_ID = value;
                this.NotifyPropertyChanged("CS_MST_ID");
            }
        }

        [DBColumn(Name = "QUOTATION_MST_ID", Storage = "m_QUOTATION_MST_ID", DbType = "107")]
        public decimal QUOTATION_MST_ID
        {
            get { return this.m_QUOTATION_MST_ID; }
            set
            {
                this.m_QUOTATION_MST_ID = value;
                this.NotifyPropertyChanged("QUOTATION_MST_ID");
            }
        }

        [DBColumn(Name = "SUP_ID", Storage = "m_SUP_ID", DbType = "107")]
        public decimal SUP_ID
        {
            get { return this.m_SUP_ID; }
            set
            {
                this.m_SUP_ID = value;
                this.NotifyPropertyChanged("SUP_ID");
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

        [DBColumn(Name = "ITEM_QTY", Storage = "m_ITEM_QTY", DbType = "107")]
        public decimal ITEM_QTY
        {
            get { return this.m_ITEM_QTY; }
            set
            {
                this.m_ITEM_QTY = value;
                this.NotifyPropertyChanged("ITEM_QTY");
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

        [DBColumn(Name = "DISCOUNT", Storage = "m_DISCOUNT", DbType = "107")]
        public decimal DISCOUNT
        {
            get { return this.m_DISCOUNT; }
            set
            {
                this.m_DISCOUNT = value;
                this.NotifyPropertyChanged("DISCOUNT");
            }
        }

        [DBColumn(Name = "CS_DTL_REMARKS", Storage = "m_CS_DTL_REMARKS", DbType = "126")]
        public string CS_DTL_REMARKS
        {
            get { return this.m_CS_DTL_REMARKS; }
            set
            {
                this.m_CS_DTL_REMARKS = value;
                this.NotifyPropertyChanged("CS_DTL_REMARKS");
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
}
