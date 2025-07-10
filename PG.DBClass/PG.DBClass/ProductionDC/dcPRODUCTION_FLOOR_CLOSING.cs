using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "PRODUCTION_FLOOR_CLOSING")]
    public partial class dcPRODUCTION_FLOOR_CLOSING : DBBaseClass, INotifyPropertyChanged
    {
        #region private members
        private int m_CLOSING_ID = 0;
        private int m_PROD_MST_ID = 0;
        private int m_CLOSING_ITEM_ID = 0;
        private decimal m_CLOSING_QTY = 0;
        private int m_UOM_ID = 0;
        private string m_CLOSING_REMARKS = "";
        private int m_CLOSING_SI = 0;
        private decimal m_SYSTEM_OPENING_STOCK = 0;
        private decimal m_MANUAL_OPENING_STOCK = 0;
        private decimal m_ISSUE_STOCK = 0;
        private decimal m_WASTAGE_QTY = 0;
        private decimal m_REJECTED_QTY = 0;
        private decimal m_POSITIVE_DEV = 0;
        private decimal m_NEGATIVE_DEV = 0;
        private decimal m_REUSE_QTY = 0;
        //private Int64 m_PROD_DTL_ID = 0;
        private int m_FINISHED_ITEM_ID = 0;
        private int m_ISMANUAL = 0;
        private decimal m_STD_USED_QTY = 0;
        private decimal m_RECOVERY_QTY = 0;
        private decimal m_ASM_OP_REJECT_QTY = 0;
        private decimal m_ASM_OP_RECOVERY_QTY = 0;
        private int m_MACHINE_ID = 0;
        private int m_STLM_ID = 0;
        private string m_PROD_BATCH_NO = "";
        private int m_COS_LEAD_ID = 0;
        private decimal m_COS_LEAD_QTY = 0;

        
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


        [DBColumn(Name = "CLOSING_ID", Storage = "m_CLOSING_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int CLOSING_ID
        {
            get { return this.m_CLOSING_ID; }
            set
            {
                this.m_CLOSING_ID = value;
                this.NotifyPropertyChanged("CLOSING_ID");
            }
        }



        [DBColumn(Name = "PROD_MST_ID", Storage = "m_PROD_MST_ID", DbType = "107")]
        public int PROD_MST_ID
        {
            get { return this.m_PROD_MST_ID; }
            set
            {
                this.m_PROD_MST_ID = value;
                this.NotifyPropertyChanged("PROD_MST_ID");
            }
        }

        [DBColumn(Name = "CLOSING_ITEM_ID", Storage = "m_CLOSING_ITEM_ID", DbType = "107")]
        public int CLOSING_ITEM_ID
        {
            get { return this.m_CLOSING_ITEM_ID; }
            set
            {
                this.m_CLOSING_ITEM_ID = value;
                this.NotifyPropertyChanged("CLOSING_ITEM_ID");
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

        [DBColumn(Name = "CLOSING_UOM_ID", Storage = "m_UOM_ID", DbType = "107")]
        public int CLOSING_UOM_ID
        {
            get { return this.m_UOM_ID; }
            set
            {
                this.m_UOM_ID = value;
                this.NotifyPropertyChanged("CLOSING_UOM_ID");
            }
        }


        [DBColumn(Name = "CLOSING_REMARKS", Storage = "m_CLOSING_REMARKS", DbType = "107")]
        public string CLOSING_REMARKS
        {
            get { return this.m_CLOSING_REMARKS; }
            set
            {
                this.m_CLOSING_REMARKS = value;
                this.NotifyPropertyChanged("CLOSING_REMARKS");
            }
        }



        [DBColumn(Name = "CLOSING_SI", Storage = "m_CLOSING_SI", DbType = "107")]
        public int CLOSING_SI
        {
            get { return this.m_CLOSING_SI; }
            set
            {
                this.m_CLOSING_SI = value;
                this.NotifyPropertyChanged("CLOSING_SI");
            }
        }

        [DBColumn(Name = "SYSTEM_OPENING_STOCK", Storage = "m_SYSTEM_OPENING_STOCK", DbType = "107")]
        public decimal SYSTEM_OPENING_STOCK
        {
            get { return this.m_SYSTEM_OPENING_STOCK; }
            set
            {
                this.m_SYSTEM_OPENING_STOCK = value;
                this.NotifyPropertyChanged("SYSTEM_OPENING_STOCK");
            }
        }


        [DBColumn(Name = "MANUAL_OPENING_STOCK", Storage = "m_MANUAL_OPENING_STOCK", DbType = "107")]
        public decimal MANUAL_OPENING_STOCK
        {
            get { return this.m_MANUAL_OPENING_STOCK; }
            set
            {
                this.m_MANUAL_OPENING_STOCK = value;
                this.NotifyPropertyChanged("MANUAL_OPENING_STOCK");
            }
        }


        [DBColumn(Name = "ISSUE_STOCK", Storage = "m_ISSUE_STOCK", DbType = "107")]
        public decimal ISSUE_STOCK
        {
            get { return this.m_ISSUE_STOCK; }
            set
            {
                this.m_ISSUE_STOCK = value;
                this.NotifyPropertyChanged("ISSUE_STOCK");
            }
        }







        [DBColumn(Name = "WASTAGE_QTY", Storage = "m_WASTAGE_QTY", DbType = "107")]
        public decimal WASTAGE_QTY
        {
            get { return this.m_WASTAGE_QTY; }
            set
            {
                this.m_WASTAGE_QTY = value;
                this.NotifyPropertyChanged("WASTAGE_QTY");
            }
        }


        [DBColumn(Name = "REJECTED_QTY", Storage = "m_REJECTED_QTY", DbType = "107")]
        public decimal REJECTED_QTY
        {
            get { return this.m_REJECTED_QTY; }
            set
            {
                this.m_REJECTED_QTY = value;
                this.NotifyPropertyChanged("REJECTED_QTY");
            }
        }


        [DBColumn(Name = "POSITIVE_DEV", Storage = "m_POSITIVE_DEV", DbType = "107")]
        public decimal POSITIVE_DEV
        {
            get { return this.m_POSITIVE_DEV; }
            set
            {
                this.m_POSITIVE_DEV = value;
                this.NotifyPropertyChanged("POSITIVE_DEV");
            }
        }


        [DBColumn(Name = "NEGATIVE_DEV", Storage = "m_NEGATIVE_DEV", DbType = "107")]
        public decimal NEGATIVE_DEV
        {
            get { return this.m_NEGATIVE_DEV; }
            set
            {
                this.m_NEGATIVE_DEV = value;
                this.NotifyPropertyChanged("NEGATIVE_DEV");
            }
        }



        [DBColumn(Name = "REUSE_QTY", Storage = "m_REUSE_QTY", DbType = "107")]
        public decimal REUSE_QTY
        {
            get { return this.m_REUSE_QTY; }
            set
            {
                this.m_REUSE_QTY = value;
                this.NotifyPropertyChanged("REUSE_QTY");
            }
        }


        //[DBColumn(Name = "PROD_DTL_ID", Storage = "m_PROD_DTL_ID", DbType = "107")]
        //public Int64 PROD_DTL_ID
        //{
        //    get { return this.m_PROD_DTL_ID; }
        //    set
        //    {
        //        this.m_PROD_DTL_ID = value;
        //        this.NotifyPropertyChanged("PROD_DTL_ID");
        //    }
        //}

        [DBColumn(Name = "FINISHED_ITEM_ID", Storage = "m_FINISHED_ITEM_ID", DbType = "107")]
        public int FINISHED_ITEM_ID
        {
            get { return this.m_FINISHED_ITEM_ID; }
            set
            {
                this.m_FINISHED_ITEM_ID = value;
                this.NotifyPropertyChanged("FINISHED_ITEM_ID");
            }
        }

         [DBColumn(Name = "ISMANUAL", Storage = "m_ISMANUAL", DbType = "107")]
        public int ISMANUAL
        {
            get { return this.m_ISMANUAL; }
            set
            {
                this.m_ISMANUAL = value;
                this.NotifyPropertyChanged("ISMANUAL");
            }
        }

         [DBColumn(Name = "STD_USED_QTY", Storage = "m_STD_USED_QTY", DbType = "107")]
         public decimal STD_USED_QTY
         {
             get { return this.m_STD_USED_QTY; }
             set
             {
                 this.m_STD_USED_QTY = value;
                 this.NotifyPropertyChanged("STD_USED_QTY");
             }
         }

         [DBColumn(Name = "RECOVERY_QTY", Storage = "m_RECOVERY_QTY", DbType = "107")]
         public decimal RECOVERY_QTY
         {
             get { return this.m_RECOVERY_QTY; }
             set
             {
                 this.m_RECOVERY_QTY = value;
                 this.NotifyPropertyChanged("RECOVERY_QTY");
             }
         }

         [DBColumn(Name = "ASM_OP_REJECT_QTY", Storage = "m_ASM_OP_REJECT_QTY", DbType = "107")]
         public decimal ASM_OP_REJECT_QTY
         {
             get { return this.m_ASM_OP_REJECT_QTY; }
             set
             {
                 this.m_ASM_OP_REJECT_QTY = value;
                 this.NotifyPropertyChanged("ASM_OP_REJECT_QTY");
             }
         }


         [DBColumn(Name = "ASM_OP_RECOVERY_QTY", Storage = "m_ASM_OP_RECOVERY_QTY", DbType = "107")]
         public decimal ASM_OP_RECOVERY_QTY
         {
             get { return this.m_ASM_OP_RECOVERY_QTY; }
             set
             {
                 this.m_ASM_OP_RECOVERY_QTY = value;
                 this.NotifyPropertyChanged("ASM_OP_RECOVERY_QTY");
             }
         }

         [DBColumn(Name = "MACHINE_ID", Storage = "m_MACHINE_ID", DbType = "107")]
         public int MACHINE_ID
         {
             get { return this.m_MACHINE_ID; }
             set
             {
                 this.m_MACHINE_ID = value;
                 this.NotifyPropertyChanged("MACHINE_ID");
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

         [DBColumn(Name = "PROD_BATCH_NO", Storage = "m_PROD_BATCH_NO", DbType = "107")]
         public string PROD_BATCH_NO
         {
             get { return this.m_PROD_BATCH_NO; }
             set
             {
                 this.m_PROD_BATCH_NO = value;
                 this.NotifyPropertyChanged("PROD_BATCH_NO");
             }
         }

         [DBColumn(Name = "COS_LEAD_ID", Storage = "m_COS_LEAD_ID", DbType = "107")]
         public int COS_LEAD_ID
         {
             get { return this.m_COS_LEAD_ID; }
             set
             {
                 this.m_COS_LEAD_ID = value;
                 this.NotifyPropertyChanged("COS_LEAD_ID");
             }
         }

         [DBColumn(Name = "COS_LEAD_QTY", Storage = "m_COS_LEAD_QTY", DbType = "107")]
         public decimal COS_LEAD_QTY
         {
             get { return this.m_COS_LEAD_QTY; }
             set
             {
                 this.m_COS_LEAD_QTY = value;
                 this.NotifyPropertyChanged("COS_LEAD_QTY");
             }
         }
        
        
        #endregion //properties
    }

    public partial class dcPRODUCTION_FLOOR_CLOSING
    {
        private string m_CLOSINGITEM_NAME = "";
        private string m_CLOSING_UOM_NAME = "";
        private string m_BOM_NAME = "";
        private int m_BOM_ID = 0;
        private decimal m_TOTAL_QTY = 0;
        
        public string CLOSINGITEM_NAME
        {
            get { return this.m_CLOSINGITEM_NAME; }
            set
            {
                this.m_CLOSINGITEM_NAME = value;
            }
        }


        private string m_FINISH_ITEM_NAME = "";

        public string FINISH_ITEM_NAME
        {
            get { return this.m_FINISH_ITEM_NAME; }
            set
            {
                this.m_FINISH_ITEM_NAME = value;
            }
        }



        public string CLOSING_UOM_NAME
        {
            get { return this.m_CLOSING_UOM_NAME; }
            set
            {
                this.m_CLOSING_UOM_NAME = value;
            }
        }

        public string BOM_NAME
        {
            get { return this.m_BOM_NAME; }
            set
            {
                this.m_BOM_NAME = value;
            }
        }

        public int BOM_ID
        {
            get { return this.m_BOM_ID; }
            set
            {
                this.m_BOM_ID = value;
            }
        }

      

        public decimal TOTAL_QTY
        {
            get { return this.m_TOTAL_QTY; }
            set
            {
                this.m_TOTAL_QTY = value;
            }
        }

        public string MACHINE_NAME { get; set; }
        public string RM_ITEM_CODE { get; set; }
        public decimal USED_QTY { get; set; }
        public decimal TEMP_BATCH_QTY { get; set; }
        public decimal UNAUTHO_PROD_QTY { get; set; }

        private int M_CLOSING_ITEM_GROUP_ID = 0;
        public int CLOSING_ITEM_GROUP_ID
        {
            get { return this.M_CLOSING_ITEM_GROUP_ID; }
            set
            {
                this.M_CLOSING_ITEM_GROUP_ID = value;
            }
        }

        private string m_IS_OWN_ITEM = string.Empty;
        public string IS_OWN_ITEM
        {
            get { return this.m_IS_OWN_ITEM; }
            set
            {
                this.m_IS_OWN_ITEM = value;
            }
        }

        private string m_IS_BATCH = string.Empty;
        public string IS_BATCH
        {
            get { return this.m_IS_BATCH; }
            set
            {
                this.m_IS_BATCH = value;
            }
        }

    }
}
