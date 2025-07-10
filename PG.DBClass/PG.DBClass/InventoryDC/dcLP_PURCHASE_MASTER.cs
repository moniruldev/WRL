using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "LP_PURCHASE_MASTER")]
    public partial class dcLP_PURCHASE_MASTER : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private Int64 m_PURCHASE_ID = 0;
        private string m_PURCHASE_NO = string.Empty;
        private Int64 m_INDT_ID = 0;
        private int m_SUP_ID = 0;
        private DateTime? m_PURCHASE_DATE = null;
        private DateTime? m_PURCHASE_TIME = null;
        
        private string m_PURCHASE_BY = string.Empty;
        private string m_AUDIT_NOTE = string.Empty;
        private int? m_AUDIT_BY = 0;
        private DateTime? m_AUDIT_DATE = null;
        private string m_AUTH_STATUS = string.Empty;
        private int? m_AUTH_BY = 0;
        private DateTime? m_AUTH_DATE = null;
        private string m_PURCHASE_REMARKS = string.Empty;
        private string m_PURCHASE_TYPE = string.Empty;
        private int m_CREATE_BY = 0;
        private DateTime? m_CREATE_DATE = null;
        private int? m_UPDATE_BY = 0;
        private DateTime? m_UPDATE_DATE = null;
        private string m_SUP_DC_NO = string.Empty;
        private int m_INV_TRANS_TYPE_ID = 0;
        private int m_COMPANY_ID = 0;
        private int m_APPROVED_BY_PUR_H = 0;
        private DateTime? m_APPROVED_DATE_PUR_H = null;
        private string m_APPROVED_STATUS_PUR_H = string.Empty;
        private int m_APPROVED_BY_FINANCE_H = 0;
        private DateTime? m_APPROVED_DATE_FINANCE_H = null;
        private string m_APPROVED_STATUS_FINANCE_H = string.Empty;
        private int m_APPROVED_BY_BUSINESS_H = 0;
        private DateTime? m_APPROVED_DATE_BUSINESS_H = null;
        private string m_APPROVED_STATUS_BUSINESS_H = string.Empty;
        private string m_IS_BUSINES_H_APPROVAL = string.Empty;
        private int m_APPROVED_BY_DIRECTOR = 0;
        private DateTime? m_APPROVED_DATE_DIRECTOR = null;
        private string m_APPROVED_STATUS_DIRECTOR = string.Empty;
        private int m_PURCHASE_STATUS_ID = 0;
        private DateTime? m_DELIVERY_DATE = null;
        private string m_DELIVERY_LOCATION = string.Empty;
        private int m_FOR_DEPT_ID = 0;
        private int m_STORE_ID = 0;
        private decimal m_SP_DISCOUNT = 0;
         private decimal m_PRICE_EX_VAT_AIT = 0;
         private decimal m_VAT_AIT = 0;
         private decimal m_INC_VAT_AIT = 0;
         private int m_APPROVED_BY_FINANCE_DIR = 0;
         private DateTime? m_APPROVED_DATE_FINANCE_DIR = null;
         private string m_APPROVED_STATUS_FINANCE_DIR = string.Empty;
         private string m_VAT_AIT_LABEL = string.Empty;



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


        [DBColumn(Name = "PURCHASE_ID", Storage = "m_PURCHASE_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public Int64 PURCHASE_ID
        {
            get { return this.m_PURCHASE_ID; }
            set
            {
                this.m_PURCHASE_ID = value;
                this.NotifyPropertyChanged("PURCHASE_ID");
            }
        }

        [DBColumn(Name = "PURCHASE_NO", Storage = "m_PURCHASE_NO", DbType = "126")]
        public string PURCHASE_NO
        {
            get { return this.m_PURCHASE_NO; }
            set
            {
                this.m_PURCHASE_NO = value;
                this.NotifyPropertyChanged("PURCHASE_NO");
            }
        }

        [DBColumn(Name = "INDT_ID", Storage = "m_INDT_ID", DbType = "107")]
        public Int64 INDT_ID
        {
            get { return this.m_INDT_ID; }
            set
            {
                this.m_INDT_ID = value;
                this.NotifyPropertyChanged("INDT_ID");
            }
        }

        [DBColumn(Name = "SUP_ID", Storage = "m_SUP_ID", DbType = "107")]
        public int SUP_ID
        {
            get { return this.m_SUP_ID; }
            set
            {
                this.m_SUP_ID = value;
                this.NotifyPropertyChanged("SUP_ID");
            }
        }


        [DBColumn(Name = "PURCHASE_DATE", Storage = "m_PURCHASE_DATE", DbType = "106")]
        public DateTime? PURCHASE_DATE
        {
            get { return this.m_PURCHASE_DATE; }
            set
            {
                this.m_PURCHASE_DATE = value;
                this.NotifyPropertyChanged("PURCHASE_DATE");
            }
        }

        [DBColumn(Name = "PURCHASE_TIME", Storage = "m_PURCHASE_TIME", DbType = "106")]
        public DateTime? PURCHASE_TIME
        {
            get { return this.m_PURCHASE_TIME; }
            set
            {
                this.m_PURCHASE_TIME = value;
                this.NotifyPropertyChanged("PURCHASE_TIME");
            }
        }

        [DBColumn(Name = "PURCHASE_BY", Storage = "m_PURCHASE_BY", DbType = "126")]
        public string PURCHASE_BY
        {
            get { return this.m_PURCHASE_BY; }
            set
            {
                this.m_PURCHASE_BY = value;
                this.NotifyPropertyChanged("PURCHASE_BY");
            }
        }

        [DBColumn(Name = "AUDIT_NOTE", Storage = "m_AUDIT_NOTE", DbType = "126")]
        public string AUDIT_NOTE
        {
            get { return this.m_AUDIT_NOTE; }
            set
            {
                this.m_AUDIT_NOTE = value;
                this.NotifyPropertyChanged("AUDIT_NOTE");
            }
        }

        [DBColumn(Name = "AUDIT_BY", Storage = "m_AUDIT_BY", DbType = "126")]
        public int? AUDIT_BY
        {
            get { return this.m_AUDIT_BY; }
            set
            {
                this.m_AUDIT_BY = value;
                this.NotifyPropertyChanged("AUDIT_BY");
            }
        }

        [DBColumn(Name = "AUDIT_DATE", Storage = "m_AUDIT_DATE", DbType = "106")]
        public DateTime? AUDIT_DATE
        {
            get { return this.m_AUDIT_DATE; }
            set
            {
                this.m_AUDIT_DATE = value;
                this.NotifyPropertyChanged("AUDIT_DATE");
            }
        }

        [DBColumn(Name = "AUTH_STATUS", Storage = "m_AUTH_STATUS", DbType = "126")]
        public string AUTH_STATUS
        {
            get { return this.m_AUTH_STATUS; }
            set
            {
                this.m_AUTH_STATUS = value;
                this.NotifyPropertyChanged("AUTH_STATUS");
            }
        }

        [DBColumn(Name = "AUTH_BY", Storage = "m_AUTH_BY", DbType = "126")]
        public int? AUTH_BY
        {
            get { return this.m_AUTH_BY; }
            set
            {
                this.m_AUTH_BY = value;
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

        [DBColumn(Name = "PURCHASE_REMARKS", Storage = "m_PURCHASE_REMARKS", DbType = "126")]
        public string PURCHASE_REMARKS
        {
            get { return this.m_PURCHASE_REMARKS; }
            set
            {
                this.m_PURCHASE_REMARKS = value;
                this.NotifyPropertyChanged("PURCHASE_REMARKS");
            }
        }


        [DBColumn(Name = "PURCHASE_TYPE", Storage = "m_PURCHASE_TYPE", DbType = "126")]
        public string PURCHASE_TYPE
        {
            get { return this.m_PURCHASE_TYPE; }
            set
            {
                this.m_PURCHASE_TYPE = value;
                this.NotifyPropertyChanged("PURCHASE_TYPE");
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
        public int? UPDATE_BY
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


       [DBColumn(Name = "SUP_DC_NO", Storage = "m_SUP_DC_NO", DbType = "126")]
        public string SUP_DC_NO
        {
            get { return this.m_SUP_DC_NO; }
            set
            {
                this.m_SUP_DC_NO = value;
                this.NotifyPropertyChanged("SUP_DC_NO");
            }
        }


       [DBColumn(Name = "INV_TRANS_TYPE_ID", Storage = "m_INV_TRANS_TYPE_ID", DbType = "107")]
       public int INV_TRANS_TYPE_ID
       {
           get { return this.m_INV_TRANS_TYPE_ID; }
           set
           {
               this.m_INV_TRANS_TYPE_ID = value;
               this.NotifyPropertyChanged("INV_TRANS_TYPE_ID");
           }
       }

       [DBColumn(Name = "COMPANY_ID", Storage = "m_COMPANY_ID", DbType = "107")]
       public int COMPANY_ID
       {
           get { return this.m_COMPANY_ID; }
           set
           {
               this.m_COMPANY_ID = value;
               this.NotifyPropertyChanged("COMPANY_ID");
           }
       }


       [DBColumn(Name = "APPROVED_BY_PUR_H", Storage = "m_APPROVED_BY_PUR_H", DbType = "107")]
       public int APPROVED_BY_PUR_H
       {
           get { return this.m_APPROVED_BY_PUR_H; }
           set
           {
               this.m_APPROVED_BY_PUR_H = value;
               this.NotifyPropertyChanged("APPROVED_BY_PUR_H");
           }
       }

       [DBColumn(Name = "APPROVED_DATE_PUR_H", Storage = "m_APPROVED_DATE_PUR_H", DbType = "106")]
       public DateTime? APPROVED_DATE_PUR_H
       {
           get { return this.m_APPROVED_DATE_PUR_H; }
           set
           {
               this.m_APPROVED_DATE_PUR_H = value;
               this.NotifyPropertyChanged("APPROVED_DATE_PUR_H");
           }
       }

       [DBColumn(Name = "APPROVED_STATUS_PUR_H", Storage = "m_APPROVED_STATUS_PUR_H", DbType = "126")]
       public string APPROVED_STATUS_PUR_H
       {
           get { return this.m_APPROVED_STATUS_PUR_H; }
           set
           {
               this.m_APPROVED_STATUS_PUR_H = value;
               this.NotifyPropertyChanged("APPROVED_STATUS_PUR_H");
           }
       }

       [DBColumn(Name = "APPROVED_BY_FINANCE_H", Storage = "m_APPROVED_BY_FINANCE_H", DbType = "107")]
       public int APPROVED_BY_FINANCE_H
       {
           get { return this.m_APPROVED_BY_FINANCE_H; }
           set
           {
               this.m_APPROVED_BY_FINANCE_H = value;
               this.NotifyPropertyChanged("APPROVED_BY_FINANCE_H");
           }
       }

       [DBColumn(Name = "APPROVED_DATE_FINANCE_H", Storage = "m_APPROVED_DATE_FINANCE_H", DbType = "106")]
       public DateTime? APPROVED_DATE_FINANCE_H
       {
           get { return this.m_APPROVED_DATE_FINANCE_H; }
           set
           {
               this.m_APPROVED_DATE_FINANCE_H = value;
               this.NotifyPropertyChanged("APPROVED_DATE_FINANCE_H");
           }
       }

       [DBColumn(Name = "APPROVED_STATUS_FINANCE_H", Storage = "m_APPROVED_STATUS_FINANCE_H", DbType = "126")]
       public string APPROVED_STATUS_FINANCE_H
       {
           get { return this.m_APPROVED_STATUS_FINANCE_H; }
           set
           {
               this.m_APPROVED_STATUS_FINANCE_H = value;
               this.NotifyPropertyChanged("APPROVED_STATUS_FINANCE_H");
           }
       }

       [DBColumn(Name = "APPROVED_BY_BUSINESS_H", Storage = "m_APPROVED_BY_BUSINESS_H", DbType = "107")]
       public int APPROVED_BY_BUSINESS_H
       {
           get { return this.m_APPROVED_BY_BUSINESS_H; }
           set
           {
               this.m_APPROVED_BY_BUSINESS_H = value;
               this.NotifyPropertyChanged("APPROVED_BY_BUSINESS_H");
           }
       }

       [DBColumn(Name = "APPROVED_DATE_BUSINESS_H", Storage = "m_APPROVED_DATE_BUSINESS_H", DbType = "106")]
       public DateTime? APPROVED_DATE_BUSINESS_H
       {
           get { return this.m_APPROVED_DATE_BUSINESS_H; }
           set
           {
               this.m_APPROVED_DATE_BUSINESS_H = value;
               this.NotifyPropertyChanged("APPROVED_DATE_BUSINESS_H");
           }
       }

       [DBColumn(Name = "APPROVED_STATUS_BUSINESS_H", Storage = "m_APPROVED_STATUS_BUSINESS_H", DbType = "126")]
       public string APPROVED_STATUS_BUSINESS_H
       {
           get { return this.m_APPROVED_STATUS_BUSINESS_H; }
           set
           {
               this.m_APPROVED_STATUS_BUSINESS_H = value;
               this.NotifyPropertyChanged("APPROVED_STATUS_BUSINESS_H");
           }
       }

       [DBColumn(Name = "IS_BUSINES_H_APPROVAL", Storage = "m_IS_BUSINES_H_APPROVAL", DbType = "126")]
       public string IS_BUSINES_H_APPROVAL
       {
           get { return this.m_IS_BUSINES_H_APPROVAL; }
           set
           {
               this.m_IS_BUSINES_H_APPROVAL = value;
               this.NotifyPropertyChanged("IS_BUSINES_H_APPROVAL");
           }
       }

       [DBColumn(Name = "APPROVED_BY_DIRECTOR", Storage = "m_APPROVED_BY_DIRECTOR", DbType = "107")]
       public int APPROVED_BY_DIRECTOR
       {
           get { return this.m_APPROVED_BY_DIRECTOR; }
           set
           {
               this.m_APPROVED_BY_DIRECTOR = value;
               this.NotifyPropertyChanged("APPROVED_BY_DIRECTOR");
           }
       }

       [DBColumn(Name = "APPROVED_DATE_DIRECTOR", Storage = "m_APPROVED_DATE_DIRECTOR", DbType = "106")]
       public DateTime? APPROVED_DATE_DIRECTOR
       {
           get { return this.m_APPROVED_DATE_DIRECTOR; }
           set
           {
               this.m_APPROVED_DATE_DIRECTOR = value;
               this.NotifyPropertyChanged("APPROVED_DATE_DIRECTOR");
           }
       }

       [DBColumn(Name = "APPROVED_STATUS_DIRECTOR", Storage = "m_APPROVED_STATUS_DIRECTOR", DbType = "126")]
       public string APPROVED_STATUS_DIRECTOR
       {
           get { return this.m_APPROVED_STATUS_DIRECTOR; }
           set
           {
               this.m_APPROVED_STATUS_DIRECTOR = value;
               this.NotifyPropertyChanged("APPROVED_STATUS_DIRECTOR");
           }
       }

       [DBColumn(Name = "PURCHASE_STATUS_ID", Storage = "m_PURCHASE_STATUS_ID", DbType = "107")]
       public int PURCHASE_STATUS_ID
       {
           get { return this.m_PURCHASE_STATUS_ID; }
           set
           {
               this.m_PURCHASE_STATUS_ID = value;
               this.NotifyPropertyChanged("PURCHASE_STATUS_ID");
           }
       }

       [DBColumn(Name = "DELIVERY_DATE", Storage = "m_DELIVERY_DATE", DbType = "106")]
       public DateTime? DELIVERY_DATE
       {
           get { return this.m_DELIVERY_DATE; }
           set
           {
               this.m_DELIVERY_DATE = value;
               this.NotifyPropertyChanged("DELIVERY_DATE");
           }
       }

       [DBColumn(Name = "DELIVERY_LOCATION", Storage = "m_DELIVERY_LOCATION", DbType = "126")]
       public string DELIVERY_LOCATION
       {
           get { return this.m_DELIVERY_LOCATION; }
           set
           {
               this.m_DELIVERY_LOCATION = value;
               this.NotifyPropertyChanged("DELIVERY_LOCATION");
           }
       }


       [DBColumn(Name = "FOR_DEPT_ID", Storage = "m_FOR_DEPT_ID", DbType = "107")]
       public int FOR_DEPT_ID
       {
           get { return this.m_FOR_DEPT_ID; }
           set
           {
               this.m_FOR_DEPT_ID = value;
               this.NotifyPropertyChanged("FOR_DEPT_ID");
           }
       }

       [DBColumn(Name = "STORE_ID", Storage = "m_STORE_ID", DbType = "107")]
       public int STORE_ID
       {
           get { return this.m_STORE_ID; }
           set
           {
               this.m_STORE_ID = value;
               this.NotifyPropertyChanged("STORE_ID");
           }
       }

       [DBColumn(Name = "SP_DISCOUNT", Storage = "m_SP_DISCOUNT", DbType = "107")]
       public decimal SP_DISCOUNT
       {
           get { return this.m_SP_DISCOUNT; }
           set
           {
               this.m_SP_DISCOUNT = value;
               this.NotifyPropertyChanged("SP_DISCOUNT");
           }
       }

          [DBColumn(Name = "PRICE_EX_VAT_AIT", Storage = "m_PRICE_EX_VAT_AIT", DbType = "107")]
       public decimal PRICE_EX_VAT_AIT
       {
           get { return this.m_PRICE_EX_VAT_AIT; }
           set
           {
               this.m_PRICE_EX_VAT_AIT = value;
               this.NotifyPropertyChanged("PRICE_EX_VAT_AIT");
           }
       }

          [DBColumn(Name = "VAT_AIT", Storage = "m_VAT_AIT", DbType = "107")]
       public decimal VAT_AIT
       {
           get { return this.m_VAT_AIT; }
           set
           {
               this.m_VAT_AIT = value;
               this.NotifyPropertyChanged("VAT_AIT");
           }
       }

         [DBColumn(Name = "INC_VAT_AIT", Storage = "m_INC_VAT_AIT", DbType = "107")]
       public decimal INC_VAT_AIT
       {
           get { return this.m_INC_VAT_AIT; }
           set
           {
               this.m_INC_VAT_AIT = value;
               this.NotifyPropertyChanged("INC_VAT_AIT");
           }
       }

         [DBColumn(Name = "APPROVED_BY_FINANCE_DIR", Storage = "m_APPROVED_BY_FINANCE_DIR", DbType = "107")]
         public int APPROVED_BY_FINANCE_DIR
         {
             get { return this.m_APPROVED_BY_FINANCE_DIR; }
             set
             {
                 this.m_APPROVED_BY_FINANCE_DIR = value;
                 this.NotifyPropertyChanged("APPROVED_BY_FINANCE_DIR");
             }
         }

         [DBColumn(Name = "APPROVED_DATE_FINANCE_DIR", Storage = "m_APPROVED_DATE_FINANCE_DIR", DbType = "106")]
         public DateTime? APPROVED_DATE_FINANCE_DIR
         {
             get { return this.m_APPROVED_DATE_FINANCE_DIR; }
             set
             {
                 this.m_APPROVED_DATE_FINANCE_DIR = value;
                 this.NotifyPropertyChanged("APPROVED_DATE_FINANCE_DIR");
             }
         }

         [DBColumn(Name = "APPROVED_STATUS_FINANCE_DIR", Storage = "m_APPROVED_STATUS_FINANCE_DIR", DbType = "126")]
         public string APPROVED_STATUS_FINANCE_DIR
         {
             get { return this.m_APPROVED_STATUS_FINANCE_DIR; }
             set
             {
                 this.m_APPROVED_STATUS_FINANCE_DIR = value;
                 this.NotifyPropertyChanged("APPROVED_STATUS_FINANCE_DIR");
             }
         }

         [DBColumn(Name = "VAT_AIT_LABEL", Storage = "m_VAT_AIT_LABEL", DbType = "126")]
         public string VAT_AIT_LABEL
         {
             get { return this.m_VAT_AIT_LABEL; }
             set
             {
                 this.m_VAT_AIT_LABEL = value;
                 this.NotifyPropertyChanged("VAT_AIT_LABEL");
             }
         }
      
        
        #endregion //properties


    
    }
    public partial class dcLP_PURCHASE_MASTER
    {

        private string m_sup_name = string.Empty;
        private string m_SUP_CODE = string.Empty;
        private string m_sup_address = string.Empty;
        private string m_item_name = string.Empty;
        private string m_uom_name = string.Empty;
        private string m_purchase_qty = string.Empty;
        private bool m_IS_MRR_COMPLETE = false;
       
        
        public Int64 m_MRR_ID = 0;

        public string m_MRR_AUTH_STATUS = "N";

        public string sup_name
        {
            get { return m_sup_name; }
            set { this.m_sup_name = value; }
        }

        public string SUP_CODE
        {
            get { return m_SUP_CODE; }
            set { this.m_SUP_CODE = value; }
        }

        public string sup_address
        {
            get { return m_sup_address; }
            set { this.m_sup_address = value; }
        }
        public string item_name
        {
            get { return m_item_name; }
            set { this.m_item_name = value; }
        }

        public string uom_name
        {
            get { return m_uom_name; }
            set { this.m_uom_name = value; }
        }
        public string purchase_qty
        {
            get { return m_purchase_qty; }
            set { this.m_purchase_qty = value; }
        }

        private DateTime? m_MRR_DATE = null;

        public DateTime? MRR_DATE
        {
            get { return m_MRR_DATE; }
            set { this.m_MRR_DATE = value; }
        }
        private string m_MRR_NO = null;

        public string MRR_NO
        {
            get { return m_MRR_NO; }
            set { this.m_MRR_NO = value; }
        }

        public string MRR_AUTH_STATUS
        {
            get { return m_MRR_AUTH_STATUS; }
            set { this.m_MRR_AUTH_STATUS = value; }
        }

        public Int64 MRR_ID
        {
            get { return m_MRR_ID; }
            set { this.m_MRR_ID = value; }
        }


        public bool m_IsMRRComplete = false;
        public bool IsMRRComplete
        {
            get { return m_IsMRRComplete; }
            set { this.m_IsMRRComplete = value; }
        }

        private bool m_IsMRRAuthComplete = false;
        public bool IsMRRAuthComplete
        {
            get { return m_IsMRRAuthComplete; }
            set { this.m_IsMRRAuthComplete = value; }
        }
        private string m_CREATE_BY_NAME = string.Empty;
        public string CREATE_BY_NAME
        {
            get { return m_CREATE_BY_NAME; }
            set { this.m_CREATE_BY_NAME = value; }
        }

        private string m_INDT_NO = string.Empty;
        public string INDT_NO
        {
            get { return m_INDT_NO; }
            set { this.m_INDT_NO = value; }
        }

        private string m_STATUS_NAME = string.Empty;
        public string STATUS_NAME
        {
            get { return m_STATUS_NAME; }
            set { this.m_STATUS_NAME = value; }
        }

        private string m_ID_NAME = string.Empty;
        public string ID_NAME
        {
            get { return m_ID_NAME; }
            set { this.m_ID_NAME = value; }
        }

        private string m_DURATION_TIME = string.Empty;
        public string DURATION_TIME
        {
            get { return m_DURATION_TIME; }
            set { this.m_DURATION_TIME = value; }
        }

         private string m_TOTAL_DURATION_TIME = string.Empty;
        public string TOTAL_DURATION_TIME
        {
            get { return m_TOTAL_DURATION_TIME; }
            set { this.m_TOTAL_DURATION_TIME = value; }
        }

        private DateTime? m_DATE_TIME = null;
        public DateTime? DATE_TIME
        {
            get { return m_DATE_TIME; }
            set { this.m_DATE_TIME = value; }
        }

        public string PO_TYPE { get; set; }

        
    }
}
