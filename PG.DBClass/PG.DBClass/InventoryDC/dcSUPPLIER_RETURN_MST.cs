using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.InventoryDC
{
    [DBTable(Name = "SUPPLIER_RETURN_MST")]
    public partial class dcSUPPLIER_RETURN_MST : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_RTN_ID = 0;
        private string m_RTN_NO = string.Empty;
        private DateTime? m_RTN_DATE = null;
        private int m_DC_ID = 0;
        private int m_SUPPLIER_ID = 0;
        private int m_CREATE_BY = 0;
        private DateTime? m_CREATE_DATE = null;
        private int m_UPDATE_BY = 0;
        private DateTime? m_UPDATE_DATE = null;
        private string m_REMARKS = string.Empty;
        private string m_AUTH_STATUS = string.Empty;
        private int m_AUTH_BY = 0;
        private DateTime? m_AUTH_DATE = null;
        private string m_IS_CANCEL = string.Empty;
        private int m_COMPANY_ID = 0;
        private string m_DC_NO = string.Empty;
        private string m_GP_NO = string.Empty;
        private int m_MRR_ID = 0;
        private int m_QC_ID = 0;
        private string m_SUP_CHALLAN_NO = string.Empty;
        private int m_STORE_ID = 0;
        private int m_DEPT_ID = 0;
        private int m_PURCHASE_ID = 0;
        private string m_IS_REPLACABLE = string.Empty;

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


        [DBColumn(Name = "RTN_ID", Storage = "m_RTN_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int RTN_ID
        {
            get { return this.m_RTN_ID; }
            set
            {
                this.m_RTN_ID = value;
                this.NotifyPropertyChanged("RTN_ID");
            }
        }

        [DBColumn(Name = "RTN_NO", Storage = "m_RTN_NO", DbType = "126")]
        public string RTN_NO
        {
            get { return this.m_RTN_NO; }
            set
            {
                this.m_RTN_NO = value;
                this.NotifyPropertyChanged("RTN_NO");
            }
        }

        [DBColumn(Name = "RTN_DATE", Storage = "m_RTN_DATE", DbType = "106")]
        public DateTime? RTN_DATE
        {
            get { return this.m_RTN_DATE; }
            set
            {
                this.m_RTN_DATE = value;
                this.NotifyPropertyChanged("RTN_DATE");
            }
        }

        [DBColumn(Name = "DC_ID", Storage = "m_DC_ID", DbType = "107")]
        public int DC_ID
        {
            get { return this.m_DC_ID; }
            set
            {
                this.m_DC_ID = value;
                this.NotifyPropertyChanged("DC_ID");
            }
        }

        [DBColumn(Name = "SUPPLIER_ID", Storage = "m_SUPPLIER_ID", DbType = "107")]
        public int SUPPLIER_ID
        {
            get { return this.m_SUPPLIER_ID; }
            set
            {
                this.m_SUPPLIER_ID = value;
                this.NotifyPropertyChanged("SUPPLIER_ID");
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

        [DBColumn(Name = "AUTH_BY", Storage = "m_AUTH_BY", DbType = "107")]
        public int AUTH_BY
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

        [DBColumn(Name = "IS_CANCEL", Storage = "m_IS_CANCEL", DbType = "126")]
        public string IS_CANCEL
        {
            get { return this.m_IS_CANCEL; }
            set
            {
                this.m_IS_CANCEL = value;
                this.NotifyPropertyChanged("IS_CANCEL");
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

        [DBColumn(Name = "DC_NO", Storage = "m_DC_NO", DbType = "126")]
        public string DC_NO
        {
            get { return this.m_DC_NO; }
            set
            {
                this.m_DC_NO = value;
                this.NotifyPropertyChanged("DC_NO");
            }
        }

        [DBColumn(Name = "GP_NO", Storage = "m_GP_NO", DbType = "126")]
        public string GP_NO
        {
            get { return this.m_GP_NO; }
            set
            {
                this.m_GP_NO = value;
                this.NotifyPropertyChanged("GP_NO");
            }
        }

        [DBColumn(Name = "MRR_ID", Storage = "m_MRR_ID", DbType = "107")]
        public int MRR_ID
        {
            get { return this.m_MRR_ID; }
            set
            {
                this.m_MRR_ID = value;
                this.NotifyPropertyChanged("MRR_ID");
            }
        }

        [DBColumn(Name = "QC_ID", Storage = "m_QC_ID", DbType = "107")]
        public int QC_ID
        {
            get { return this.m_QC_ID; }
            set
            {
                this.m_QC_ID = value;
                this.NotifyPropertyChanged("QC_ID");
            }
        }

        [DBColumn(Name = "SUP_CHALLAN_NO", Storage = "m_SUP_CHALLAN_NO", DbType = "126")]
        public string SUP_CHALLAN_NO
        {
            get { return this.m_SUP_CHALLAN_NO; }
            set
            {
                this.m_SUP_CHALLAN_NO = value;
                this.NotifyPropertyChanged("SUP_CHALLAN_NO");
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

        [DBColumn(Name = "DEPT_ID", Storage = "m_DEPT_ID", DbType = "107")]
        public int DEPT_ID
        {
            get { return this.m_DEPT_ID; }
            set
            {
                this.m_DEPT_ID = value;
                this.NotifyPropertyChanged("DEPT_ID");
            }
        }
        [DBColumn(Name = "PURCHASE_ID", Storage = "m_PURCHASE_ID", DbType = "107")]
        public int PURCHASE_ID
        {
            get { return this.m_PURCHASE_ID; }
            set
            {
                this.m_PURCHASE_ID = value;
                this.NotifyPropertyChanged("PURCHASE_ID");
            }
        }

        [DBColumn(Name = "IS_REPLACABLE", Storage = "m_IS_REPLACABLE", DbType = "126")]
        public string IS_REPLACABLE
        {
            get { return this.m_IS_REPLACABLE; }
            set
            {
                this.m_IS_REPLACABLE = value;
                this.NotifyPropertyChanged("IS_REPLACABLE");
            }
        }

        #endregion //properties
    }

     public partial class dcSUPPLIER_RETURN_MST
     {
         public string SUPPLIER_NAME { get; set; }
         public string SUP_CODE { get; set; }
         public string FULLNAME { get; set; }
         public string MRR_NO { get; set; }
         public string PURCHASE_NO { get; set; }

         private bool m_IS_RECEIVE = false;
         public bool IS_RECEIVE
         {
             get { return this.m_IS_RECEIVE; }
             set { this.m_IS_RECEIVE = value; }
         }
         
     }
}
