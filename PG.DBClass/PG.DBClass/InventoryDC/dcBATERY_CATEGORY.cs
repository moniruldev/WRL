using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
    [DBTable(Name = "BATERY_CATEGORY")]
    public partial class dcBATERY_CATEGORY : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private string m_BATERY_CAT_ID = string.Empty;
        private string m_BATERY_CAT_DESCR = string.Empty;
        private decimal m_GUARANTEE_MONTHS = 0;
        private string m_MST_CAT_ID = string.Empty;
        private string m_MAIN_CAT_GROUP = string.Empty;
        private decimal m_ITEM_SEGMENT_ID = 0;
        private string m_SALES_CAT_GROUP = string.Empty;
        private decimal m_BATERY_CAT_SL_NO = 0;
        private string m_BAT_CAT_ID_PS = string.Empty;
        private decimal m_CAT_ID = 0;

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


        [DBColumn(Name = "BATERY_CAT_ID", Storage = "m_BATERY_CAT_ID", DbType = "126")]
        public string BATERY_CAT_ID
        {
            get { return this.m_BATERY_CAT_ID; }
            set
            {
                this.m_BATERY_CAT_ID = value;
                this.NotifyPropertyChanged("BATERY_CAT_ID");
            }
        }

        [DBColumn(Name = "BATERY_CAT_DESCR", Storage = "m_BATERY_CAT_DESCR", DbType = "126")]
        public string BATERY_CAT_DESCR
        {
            get { return this.m_BATERY_CAT_DESCR; }
            set
            {
                this.m_BATERY_CAT_DESCR = value;
                this.NotifyPropertyChanged("BATERY_CAT_DESCR");
            }
        }

        [DBColumn(Name = "GUARANTEE_MONTHS", Storage = "m_GUARANTEE_MONTHS", DbType = "107")]
        public decimal GUARANTEE_MONTHS
        {
            get { return this.m_GUARANTEE_MONTHS; }
            set
            {
                this.m_GUARANTEE_MONTHS = value;
                this.NotifyPropertyChanged("GUARANTEE_MONTHS");
            }
        }

        [DBColumn(Name = "MST_CAT_ID", Storage = "m_MST_CAT_ID", DbType = "126")]
        public string MST_CAT_ID
        {
            get { return this.m_MST_CAT_ID; }
            set
            {
                this.m_MST_CAT_ID = value;
                this.NotifyPropertyChanged("MST_CAT_ID");
            }
        }

        [DBColumn(Name = "MAIN_CAT_GROUP", Storage = "m_MAIN_CAT_GROUP", DbType = "126")]
        public string MAIN_CAT_GROUP
        {
            get { return this.m_MAIN_CAT_GROUP; }
            set
            {
                this.m_MAIN_CAT_GROUP = value;
                this.NotifyPropertyChanged("MAIN_CAT_GROUP");
            }
        }

        [DBColumn(Name = "ITEM_SEGMENT_ID", Storage = "m_ITEM_SEGMENT_ID", DbType = "107")]
        public decimal ITEM_SEGMENT_ID
        {
            get { return this.m_ITEM_SEGMENT_ID; }
            set
            {
                this.m_ITEM_SEGMENT_ID = value;
                this.NotifyPropertyChanged("ITEM_SEGMENT_ID");
            }
        }

        [DBColumn(Name = "SALES_CAT_GROUP", Storage = "m_SALES_CAT_GROUP", DbType = "126")]
        public string SALES_CAT_GROUP
        {
            get { return this.m_SALES_CAT_GROUP; }
            set
            {
                this.m_SALES_CAT_GROUP = value;
                this.NotifyPropertyChanged("SALES_CAT_GROUP");
            }
        }

        [DBColumn(Name = "BATERY_CAT_SL_NO", Storage = "m_BATERY_CAT_SL_NO", DbType = "107")]
        public decimal BATERY_CAT_SL_NO
        {
            get { return this.m_BATERY_CAT_SL_NO; }
            set
            {
                this.m_BATERY_CAT_SL_NO = value;
                this.NotifyPropertyChanged("BATERY_CAT_SL_NO");
            }
        }

        [DBColumn(Name = "BAT_CAT_ID_PS", Storage = "m_BAT_CAT_ID_PS", DbType = "126")]
        public string BAT_CAT_ID_PS
        {
            get { return this.m_BAT_CAT_ID_PS; }
            set
            {
                this.m_BAT_CAT_ID_PS = value;
                this.NotifyPropertyChanged("BAT_CAT_ID_PS");
            }
        }

        [DBColumn(Name = "CAT_ID", Storage = "m_CAT_ID", DbType = "107")]
        public decimal CAT_ID
        {
            get { return this.m_CAT_ID; }
            set
            {
                this.m_CAT_ID = value;
                this.NotifyPropertyChanged("CAT_ID");
            }
        }

        #endregion //properties
    }

     public partial class dcBATERY_CATEGORY {
         public string battery_cat_id { get; set; }
     }
    
}
