using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "PROD_RM_FORECAST_MST")]
    public partial class dcPROD_RM_FORECAST_MST : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_RM_FC_ID = 0;
        private string m_RM_FC_NO = string.Empty;
        private int m_FOR_MONTH = 0;
        private int m_FOR_YEAR = 0;
        private DateTime? m_ENTRY_DATE = null;
        private int m_ENTRY_BY_ID = 0;
        private int m_FORECAST_BY_ID = 0;
        private DateTime? m_EDIT_DATE = null;
        private int m_EDIT_BY_ID = 0;
        private string m_RM_FC_DESC = String.Empty;
        private int m_FN_FC_ID = 0;
        private string m_FN_FC_NO = String.Empty;

        private int m_FN_ITEM_ID = 0;
        private int m_FN_UOM_ID = 0;
        private decimal m_FN_FC_QTY = 0;
        private int m_FN_BOM_ID = 0;

        private string m_AUTH_STATUS = "";
        private int m_AUTH_BY = 0;
        private DateTime? m_AUTH_DATE = null;

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


        [DBColumn(Name = "RM_FC_ID", Storage = "m_RM_FC_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int RM_FC_ID
        {
            get { return this.m_RM_FC_ID; }
            set
            {
                this.m_RM_FC_ID = value;
                this.NotifyPropertyChanged("RM_FC_ID");
            }
        }

        [DBColumn(Name = "RM_FC_NO", Storage = "m_RM_FC_NO", DbType = "126")]
        public string RM_FC_NO
        {
            get { return this.m_RM_FC_NO; }
            set
            {
                this.m_RM_FC_NO = value;
                this.NotifyPropertyChanged("RM_FC_NO");
            }
        }

        [DBColumn(Name = "FOR_MONTH", Storage = "m_FOR_MONTH", DbType = "107")]
        public int FOR_MONTH
        {
            get { return this.m_FOR_MONTH; }
            set
            {
                this.m_FOR_MONTH = value;
                this.NotifyPropertyChanged("FOR_MONTH");
            }
        }

        [DBColumn(Name = "FOR_YEAR", Storage = "m_FOR_YEAR", DbType = "107")]
        public int FOR_YEAR
        {
            get { return this.m_FOR_YEAR; }
            set
            {
                this.m_FOR_YEAR = value;
                this.NotifyPropertyChanged("FOR_YEAR");
            }
        }

        [DBColumn(Name = "ENTRY_DATE", Storage = "m_ENTRY_DATE", DbType = "106")]
        public DateTime? ENTRY_DATE
        {
            get { return this.m_ENTRY_DATE; }
            set
            {
                this.m_ENTRY_DATE = value;
                this.NotifyPropertyChanged("ENTRY_DATE");
            }
        }

        [DBColumn(Name = "ENTRY_BY_ID", Storage = "m_ENTRY_BY_ID", DbType = "126")]
        public int ENTRY_BY_ID
        {
            get { return this.m_ENTRY_BY_ID; }
            set
            {
                this.m_ENTRY_BY_ID = value;
                this.NotifyPropertyChanged("ENTRY_BY_ID");
            }
        }

        [DBColumn(Name = "FORECAST_BY_ID", Storage = "m_FORECAST_BY_ID", DbType = "126")]
        public int FORECAST_BY_ID
        {
            get { return this.m_FORECAST_BY_ID; }
            set
            {
                this.m_FORECAST_BY_ID = value;
                this.NotifyPropertyChanged("FORECAST_BY_ID");
            }
        }

        [DBColumn(Name = "EDIT_DATE", Storage = "m_EDIT_DATE", DbType = "106")]
        public DateTime? EDIT_DATE
        {
            get { return this.m_EDIT_DATE; }
            set
            {
                this.m_EDIT_DATE = value;
                this.NotifyPropertyChanged("EDIT_DATE");
            }
        }

        [DBColumn(Name = "EDIT_BY_ID", Storage = "m_EDIT_BY_ID", DbType = "126")]
        public int EDIT_BY_ID
        {
            get { return this.m_EDIT_BY_ID; }
            set
            {
                this.m_EDIT_BY_ID = value;
                this.NotifyPropertyChanged("EDIT_BY_ID");
            }
        }



       [DBColumn(Name = "RM_FC_DESC", Storage = "m_RM_FC_DESC", DbType = "126")]
        public string RM_FC_DESC
        {
            get { return this.m_RM_FC_DESC; }
            set
            {
                this.m_RM_FC_DESC = value;
                this.NotifyPropertyChanged("RM_FC_DESC");
            }
        }


       [DBColumn(Name = "FN_FC_ID", Storage = "m_FN_FC_ID", DbType = "126")]
       public int FN_FC_ID
       {
           get { return this.m_FN_FC_ID; }
           set
           {
               this.m_FN_FC_ID  = value;
               this.NotifyPropertyChanged("FN_FC_ID");
           }
       }


       [DBColumn(Name = "FN_FC_NO", Storage = "m_FN_FC_NO", DbType = "126")]
       public string FN_FC_NO
       {
           get { return this.m_FN_FC_NO; }
           set
           {
               this.m_FN_FC_NO = value;
               this.NotifyPropertyChanged("FN_FC_NO");
           }
       }

       [DBColumn(Name = "FN_ITEM_ID", Storage = "m_FN_ITEM_ID", DbType = "107")]
       public int FN_ITEM_ID
       {
           get { return this.m_FN_ITEM_ID; }
           set
           {
               this.m_FN_ITEM_ID = value;
               this.NotifyPropertyChanged("FN_ITEM_ID");
           }
       }

       [DBColumn(Name = "FN_FC_QTY", Storage = "m_FN_FC_QTY", DbType = "107")]
       public decimal FN_FC_QTY
       {
           get { return this.m_FN_FC_QTY; }
           set
           {
               this.m_FN_FC_QTY = value;
               this.NotifyPropertyChanged("FN_FC_QTY");
           }
       }


       [DBColumn(Name = "FN_UOM_ID", Storage = "m_FN_UOM_ID", DbType = "107")]
       public int FN_UOM_ID
       {
           get { return this.m_FN_UOM_ID; }
           set
           {
               this.m_FN_UOM_ID = value;
               this.NotifyPropertyChanged("FN_UOM_ID");
           }
       }

       [DBColumn(Name = "FN_BOM_ID", Storage = "m_FN_BOM_ID", DbType = "107")]
       public int FN_BOM_ID
       {
           get { return this.m_FN_BOM_ID; }
           set
           {
               this.m_FN_BOM_ID = value;
               this.NotifyPropertyChanged("FN_BOM_ID");
           }
       }

       [DBColumn(Name = "AUTH_STATUS", Storage = "m_AUTH_STATUS", DbType = "107")]
       public string AUTH_STATUS
       {
           get { return this.m_AUTH_STATUS; }
           set
           {
               m_AUTH_STATUS = value;
               this.NotifyPropertyChanged("AUTH_STATUS");
           }
       }



       [DBColumn(Name = "AUTH_BY", Storage = "m_AUTH_BY", DbType = "107")]
       public int AUTH_BY
       {
           get { return this.m_AUTH_BY; }
           set
           {
               m_AUTH_BY = value;
               this.NotifyPropertyChanged("AUTH_BY");
           }
       }


       [DBColumn(Name = "AUTH_DATE", Storage = "m_AUTH_DATE", DbType = "106")]
       public DateTime? AUTH_DATE
       {
           get { return this.m_AUTH_DATE; }
           set
           {
               this.m_AUTH_DATE = value;
               this.NotifyPropertyChanged("AUTH_DATE");
           }
       }
        
        #endregion //properties
    }


     public partial class dcPROD_RM_FORECAST_MST
     {
         public string m_FN_ITEM_NAME = "";
         public string m_FN_BOM_NAME = "";
         private List<dcPROD_RM_FORECAST_DTL> m_RM_ForecastDetList = null;
         public List<dcPROD_RM_FORECAST_DTL> RM_ForecastDetList
         {
             get { return m_RM_ForecastDetList; }
             set { m_RM_ForecastDetList = value; }
         }




         public string  FN_ITEM_NAME
         {
             get { return m_FN_ITEM_NAME; }
             set { m_FN_ITEM_NAME = value; }
         }

         public string FN_BOM_NAME
         {
             get { return m_FN_BOM_NAME; }
             set { m_FN_BOM_NAME = value; }
         }

         private string m_BOM_NO = "";
         public string BOM_NO
         {
             get { return m_BOM_NO; }
             set { m_BOM_NO = value; }
         }
     }
}
