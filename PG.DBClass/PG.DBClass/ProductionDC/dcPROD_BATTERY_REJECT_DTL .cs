using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "PROD_BATTERY_REJECT_DTL")]
    public partial class dcPROD_BATTERY_REJECT_DTL : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_REJECT_DTL_ID = 0;
        private int m_REJECT_MST_ID = 0;
        private int m_ITEM_ID = 0;
        private string m_ITEM_NAME = string.Empty;
        private decimal m_REJECT_QTY = 0;
        private string m_REMARKS = string.Empty;
        private decimal m_ITEM_WEIGHT = 0;
        private int m_REJECT_ITEM_ID = 0;
        private string m_REJECT_ITEM_NAME = String.Empty;

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


        [DBColumn(Name = "REJECT_DTL_ID", Storage = "m_REJECT_DTL_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int REJECT_DTL_ID
        {
            get { return this.m_REJECT_DTL_ID; }
            set
            {
                this.m_REJECT_DTL_ID = value;
                this.NotifyPropertyChanged("REJECT_DTL_ID");
            }
        }

        [DBColumn(Name = "REJECT_MST_ID", Storage = "m_REJECT_MST_ID", DbType = "107")]
        public int REJECT_MST_ID
        {
            get { return this.m_REJECT_MST_ID; }
            set
            {
                this.m_REJECT_MST_ID = value;
                this.NotifyPropertyChanged("REJECT_MST_ID");
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

        [DBColumn(Name = "REJECT_QTY", Storage = "m_REJECT_QTY", DbType = "107")]
        public decimal REJECT_QTY
        {
            get { return this.m_REJECT_QTY; }
            set
            {
                this.m_REJECT_QTY = value;
                this.NotifyPropertyChanged("REJECT_QTY");
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


       [DBColumn(Name = "REJECT_ITEM_ID", Storage = "m_REJECT_ITEM_ID", DbType = "107")]
       public int REJECT_ITEM_ID
       {
           get { return this.m_REJECT_ITEM_ID; }
           set
           {
               this.m_REJECT_ITEM_ID = value;
               this.NotifyPropertyChanged("REJECT_ITEM_ID");
           }
       }

        [DBColumn(Name = "REJECT_ITEM_NAME", Storage = "m_REJECT_ITEM_NAME", DbType = "126")]
       public string REJECT_ITEM_NAME
        {
            get { return this.m_REJECT_ITEM_NAME; }
            set
            {
                this.m_REJECT_ITEM_NAME = value;
                this.NotifyPropertyChanged("REJECT_ITEM_NAME");
            }
        }

        #endregion //properties
    }

     public partial class dcPROD_BATTERY_REJECT_DTL
     {
         public int SLNO { get; set; }

         

     }
     
 
}
