using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "ITEM_CONVERSION_DTL")]
    public partial class dcITEM_CONVERSION_DTL : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_CONVERSION_DTL_ID = 0;
        private int m_CONVERSION_MST_ID = 0;
        private int m_PROD_DTL_ID = 0;
        private int m_ITEM_ID = 0;
        private decimal m_ITEM_QTY = 0;
        private int m_UOM_ID = 0;
        private int m_CONVERTED_ITEM_ID = 0;
        private decimal m_CONVERTED_ITEM_QTY = 0;
        private int m_CONVERTED_UOM_ID = 0;
        private decimal m_CONVERTED_WEIGHT = 0;
        private int m_SL_NO = 0;
        private string m_REMARKS = string.Empty;
        private string m_IT_REMARKS = string.Empty;
        private string m_IS_BATCH = string.Empty;
        private int m_BTY_TYPE_ID = 0;

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


        [DBColumn(Name = "CONVERSION_DTL_ID", Storage = "m_CONVERSION_DTL_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int CONVERSION_DTL_ID
        {
            get { return this.m_CONVERSION_DTL_ID; }
            set
            {
                this.m_CONVERSION_DTL_ID = value;
                this.NotifyPropertyChanged("CONVERSION_DTL_ID");
            }
        }

        [DBColumn(Name = "CONVERSION_MST_ID", Storage = "m_CONVERSION_MST_ID", DbType = "107")]
        public int CONVERSION_MST_ID
        {
            get { return this.m_CONVERSION_MST_ID; }
            set
            {
                this.m_CONVERSION_MST_ID = value;
                this.NotifyPropertyChanged("CONVERSION_MST_ID");
            }
        }

        [DBColumn(Name = "PROD_DTL_ID", Storage = "m_PROD_DTL_ID", DbType = "107")]
        public int PROD_DTL_ID
        {
            get { return this.m_PROD_DTL_ID; }
            set
            {
                this.m_PROD_DTL_ID = value;
                this.NotifyPropertyChanged("PROD_DTL_ID");
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

        [DBColumn(Name = "CONVERTED_ITEM_ID", Storage = "m_CONVERTED_ITEM_ID", DbType = "107")]
        public int CONVERTED_ITEM_ID
        {
            get { return this.m_CONVERTED_ITEM_ID; }
            set
            {
                this.m_CONVERTED_ITEM_ID = value;
                this.NotifyPropertyChanged("CONVERTED_ITEM_ID");
            }
        }

        [DBColumn(Name = "CONVERTED_ITEM_QTY", Storage = "m_CONVERTED_ITEM_QTY", DbType = "107")]
        public decimal CONVERTED_ITEM_QTY
        {
            get { return this.m_CONVERTED_ITEM_QTY; }
            set
            {
                this.m_CONVERTED_ITEM_QTY = value;
                this.NotifyPropertyChanged("CONVERTED_ITEM_QTY");
            }
        }

        [DBColumn(Name = "CONVERTED_UOM_ID", Storage = "m_CONVERTED_UOM_ID", DbType = "107")]
        public int CONVERTED_UOM_ID
        {
            get { return this.m_CONVERTED_UOM_ID; }
            set
            {
                this.m_CONVERTED_UOM_ID = value;
                this.NotifyPropertyChanged("CONVERTED_UOM_ID");
            }
        }

        [DBColumn(Name = "CONVERTED_WEIGHT", Storage = "m_CONVERTED_WEIGHT", DbType = "107")]
        public decimal CONVERTED_WEIGHT
        {
            get { return this.m_CONVERTED_WEIGHT; }
            set
            {
                this.m_CONVERTED_WEIGHT = value;
                this.NotifyPropertyChanged("CONVERTED_WEIGHT");
            }
        }

        [DBColumn(Name = "SL_NO", Storage = "m_SL_NO", DbType = "107")]
        public int SL_NO
        {
            get { return this.m_SL_NO; }
            set
            {
                this.m_SL_NO = value;
                this.NotifyPropertyChanged("SL_NO");
            }
        }

        [DBColumn(Name = "REMARKS", Storage = "m_REMARKS", DbType = "126")]
        public string REMARKS
        {
            get { return this.m_REMARKS; }
            set
            {
                this.m_REMARKS = value;
                this.NotifyPropertyChanged("REMARKS");
            }
        }

        [DBColumn(Name = "IT_REMARKS", Storage = "m_IT_REMARKS", DbType = "126")]
        public string IT_REMARKS
        {
            get { return this.m_IT_REMARKS; }
            set
            {
                this.m_IT_REMARKS = value;
                this.NotifyPropertyChanged("IT_REMARKS");
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

        #endregion //properties
    }

    public partial class dcITEM_CONVERSION_DTL
     {
         public string ITEM_NAME { get; set; }
         public string CONVERTED_ITEM_NAME { get; set; }
         public string UOM_NAME { get; set; }
         public string CONVERTED_UOM_NAME { get; set; }
     }
}
