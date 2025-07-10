using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "GRID_REJ_CONVERSION_DTL")]
    public partial class dcGRID_REJ_CONVERSION_DTL : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_REJ_CONVERSION_DTL_ID = 0;
        private int m_REJ_CONVERSION_MST_ID = 0;
        private int m_REJECTION_DTL_ID = 0;
        private int m_REJECTION_ITEM_ID = 0;
        private decimal m_REJECTION_QTY = 0;
        private int m_REJECTION_UOM_ID = 0;
        private int m_CONVERTED_ITEM_ID = 0;
        private decimal m_CONVERTED_ITEM_QTY = 0;
        private int m_CONVERTED_UOM_ID = 0;
        private decimal m_CONVERTED_WEIGHT = 0;
        private int m_SL_NO = 0;
        private string m_REMARKS = string.Empty;
        private string m_IT_REMARKS = string.Empty;

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


        [DBColumn(Name = "REJ_CONVERSION_DTL_ID", Storage = "m_REJ_CONVERSION_DTL_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int REJ_CONVERSION_DTL_ID
        {
            get { return this.m_REJ_CONVERSION_DTL_ID; }
            set
            {
                this.m_REJ_CONVERSION_DTL_ID = value;
                this.NotifyPropertyChanged("REJ_CONVERSION_DTL_ID");
            }
        }

        [DBColumn(Name = "REJ_CONVERSION_MST_ID", Storage = "m_REJ_CONVERSION_MST_ID", DbType = "107")]
        public int REJ_CONVERSION_MST_ID
        {
            get { return this.m_REJ_CONVERSION_MST_ID; }
            set
            {
                this.m_REJ_CONVERSION_MST_ID = value;
                this.NotifyPropertyChanged("REJ_CONVERSION_MST_ID");
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

        [DBColumn(Name = "REJECTION_ITEM_ID", Storage = "m_REJECTION_ITEM_ID", DbType = "107")]
        public int REJECTION_ITEM_ID
        {
            get { return this.m_REJECTION_ITEM_ID; }
            set
            {
                this.m_REJECTION_ITEM_ID = value;
                this.NotifyPropertyChanged("REJECTION_ITEM_ID");
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

        [DBColumn(Name = "REJECTION_UOM_ID", Storage = "m_REJECTION_UOM_ID", DbType = "107")]
        public int REJECTION_UOM_ID
        {
            get { return this.m_REJECTION_UOM_ID; }
            set
            {
                this.m_REJECTION_UOM_ID = value;
                this.NotifyPropertyChanged("REJECTION_UOM_ID");
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

        #endregion //properties
    }

     public partial class dcGRID_REJ_CONVERSION_DTL
     {
         public string ITEM_NAME { get; set; }
         public int ITEM_ID { get; set; }
         public string REJECTION_ITEM_NAME { get; set; }
         public string REJECTION_UOM_NAME { get; set; }
         public string CONVERTED_ITEM_NAME { get; set; }
         public string CONVERTED_UOM_NAME { get; set; }

     }
}
