using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "PROD_RM_FORECAST_DTL")]
    public partial class dcPROD_RM_FORECAST_DTL : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_RM_FC_DET_ID = 0;
        private int m_RM_FC_ID = 0;
        private int m_RM_ITEM_ID = 0;
        private decimal m_RM_FC_QTY = 0;
        private int m_RM_UOM_ID = 0;
        private string m_RM_REMARKS = string.Empty;
        private int m_RM_BOM_ID = 0;
        private decimal m_RM_BOM_QTY = 0;
        private string m_RM_FC_NO = String.Empty;
        private string m_RM_BOM_NO = String.Empty;
        private decimal m_BOM_WASTAGE_PERCENT = 0;

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


        [DBColumn(Name = "RM_FC_DET_ID", Storage = "m_RM_FC_DET_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int RM_FC_DET_ID
        {
            get { return this.m_RM_FC_DET_ID; }
            set
            {
                this.m_RM_FC_DET_ID = value;
                this.NotifyPropertyChanged("RM_FC_DET_ID");
            }
        }

        [DBColumn(Name = "RM_FC_ID", Storage = "m_RM_FC_ID", DbType = "107")]
        public int RM_FC_ID
        {
            get { return this.m_RM_FC_ID; }
            set
            {
                this.m_RM_FC_ID = value;
                this.NotifyPropertyChanged("RM_FC_ID");
            }
        }

        [DBColumn(Name = "RM_ITEM_ID", Storage = "m_RM_ITEM_ID", DbType = "107")]
        public int RM_ITEM_ID
        {
            get { return this.m_RM_ITEM_ID; }
            set
            {
                this.m_RM_ITEM_ID = value;
                this.NotifyPropertyChanged("RM_ITEM_ID");
            }
        }

       

        [DBColumn(Name = "RM_FC_QTY", Storage = "m_RM_FC_QTY", DbType = "107")]
        public decimal RM_FC_QTY
        {
            get { return this.m_RM_FC_QTY; }
            set
            {
                this.m_RM_FC_QTY = value;
                this.NotifyPropertyChanged("RM_FC_QTY");
            }
        }

        [DBColumn(Name = "RM_UOM_ID", Storage = "m_RM_UOM_ID", DbType = "107")]
        public int RM_UOM_ID
        {
            get { return this.m_RM_UOM_ID; }
            set
            {
                this.m_RM_UOM_ID = value;
                this.NotifyPropertyChanged("RM_UOM_ID");
            }
        }



        [DBColumn(Name = "RM_REMARKS", Storage = "m_RM_REMARKS", DbType = "126")]
        public string RM_REMARKS
        {
            get { return this.m_RM_REMARKS; }
            set
            {
                this.m_RM_REMARKS = value;
                this.NotifyPropertyChanged("RM_REMARKS");
            }
        }

        [DBColumn(Name = "RM_BOM_NO", Storage = "m_RM_BOM_NO", DbType = "107")]
        public string RM_BOM_NO
        {
            get { return this.m_RM_BOM_NO; }
            set
            {
                this.m_RM_BOM_NO = value;
                this.NotifyPropertyChanged("RM_BOM_NO");
            }
        }



        [DBColumn(Name = "RM_BOM_ID", Storage = "m_RM_BOM_ID", DbType = "107")]
        public int RM_BOM_ID
        {
            get { return this.m_RM_BOM_ID; }
            set
            {
                this.m_RM_BOM_ID = value;
                this.NotifyPropertyChanged("RM_BOM_ID");
            }
        }



        [DBColumn(Name = "RM_BOM_QTY", Storage = "m_RM_BOM_QTY", DbType = "107")]
        public decimal RM_BOM_QTY
        {
            get { return this.m_RM_BOM_QTY; }
            set
            {
                this.m_RM_BOM_QTY = value;
                this.NotifyPropertyChanged("RM_BOM_QTY");
            }
        }

         [DBColumn(Name = "BOM_WASTAGE_PERCENT", Storage = "m_BOM_WASTAGE_PERCENT", DbType = "107")]
        public decimal BOM_WASTAGE_PERCENT
        {
            get { return this.m_BOM_WASTAGE_PERCENT; }
            set { 
                this.m_BOM_WASTAGE_PERCENT = value; 
                this.NotifyPropertyChanged("BOM_WASTAGE_PERCENT");
            }
        }
         private decimal m_RM_FC_QTY_WASTAGE = 0;
        [DBColumn(Name = "RM_FC_QTY_WASTAGE", Storage = "m_RM_FC_QTY_WASTAGE", DbType = "107")]
         public decimal RM_FC_QTY_WASTAGE
         {
             get { return this.m_RM_FC_QTY_WASTAGE; }
             set { this.m_RM_FC_QTY_WASTAGE = value;
             this.NotifyPropertyChanged("RM_FC_QTY_WASTAGE");
             
             }
         }
        #endregion //properties
    }

     public partial class dcPROD_RM_FORECAST_DTL
     {
         private string m_RM_ITEM_NAME = String.Empty;
         private string m_RM_BOM_Name = String.Empty;
         private string m_RM_UOM_NAME = String.Empty;

         public int ITEM_CLASS_ID { get; set; }
         public int ITEM_GROUP_ID { get; set; }
        

         // BOM_Name

         public string RM_BOM_Name
         {
             get { return this.m_RM_BOM_Name; }
             set
             { this.m_RM_BOM_Name = value; }
         }

         //RM_ITEM_NAME
         public string RM_ITEM_NAME
         {
             get { return this.m_RM_ITEM_NAME; }
             set
             { this.m_RM_ITEM_NAME = value; }
         }

         //RM_UOM_NAME

         public string RM_UOM_NAME
         {
             get { return this.m_RM_UOM_NAME; }
             set
             { this.m_RM_UOM_NAME = value; }
         }

         public string RM_FC_NO
         {
             get { return this.m_RM_FC_NO; }
             set { this.m_RM_FC_NO = value; }
         }

         private int m_IS_PRIME = 0;
         public int IS_PRIME
         {
             get { return this.m_IS_PRIME; }
             set { this.m_IS_PRIME = value; }
         }

         private int m_lvl = 0;
         public int lvl
         {
             get { return this.m_lvl; }
             set { this.m_lvl = value; }
         }

         private int m_FN_FC_QTY = 0;
         public int FN_FC_QTY
         {
             get { return this.m_FN_FC_QTY; }
             set { this.m_FN_FC_QTY = value; }
         }

        
        
     }
}
