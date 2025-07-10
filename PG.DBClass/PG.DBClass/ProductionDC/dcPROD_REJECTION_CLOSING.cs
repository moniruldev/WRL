using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "PROD_REJECTION_CLOSING")]
    public partial class dcPROD_REJECTION_CLOSING : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_CLOSING_ID = 0;
        private int m_REJ_MST_ID = 0;
        private int m_CLOSING_ITEM_ID = 0;
        private decimal m_CLOSING_QTY = 0;
        private int m_CLOSING_UOM_ID = 0;
        private string m_CLOSING_REMARKS = string.Empty;
        private int m_CLOSING_SI = 0;
        private decimal m_SYSTEM_OPENING_STOCK = 0;
        private decimal m_MANUAL_OPENING_STOCK = 0;
        private decimal m_ISSUE_STOCK = 0;
        private int m_REJECT_ITEM_ID = 0;
        private int m_ISMANUAL = 0;
        private decimal m_STD_USED_QTY = 0;
        private int m_MACHINE_ID = 0;
        private int m_STLM_ID = 0;
        private string m_PROD_BATCH_NO = string.Empty;
        private decimal m_POSITIVE_DEV = 0;
        private decimal m_NEGATIVE_DEV = 0;

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


        [DBColumn(Name = "CLOSING_ID", Storage = "m_CLOSING_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int CLOSING_ID
        {
            get { return this.m_CLOSING_ID; }
            set
            {
                this.m_CLOSING_ID = value;
                this.NotifyPropertyChanged("CLOSING_ID");
            }
        }

        [DBColumn(Name = "REJ_MST_ID", Storage = "m_REJ_MST_ID", DbType = "107")]
        public int REJ_MST_ID
        {
            get { return this.m_REJ_MST_ID; }
            set
            {
                this.m_REJ_MST_ID = value;
                this.NotifyPropertyChanged("REJ_MST_ID");
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

        [DBColumn(Name = "CLOSING_UOM_ID", Storage = "m_CLOSING_UOM_ID", DbType = "107")]
        public int CLOSING_UOM_ID
        {
            get { return this.m_CLOSING_UOM_ID; }
            set
            {
                this.m_CLOSING_UOM_ID = value;
                this.NotifyPropertyChanged("CLOSING_UOM_ID");
            }
        }

        [DBColumn(Name = "CLOSING_REMARKS", Storage = "m_CLOSING_REMARKS", DbType = "126")]
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

        [DBColumn(Name = "PROD_BATCH_NO", Storage = "m_PROD_BATCH_NO", DbType = "126")]
        public string PROD_BATCH_NO
        {
            get { return this.m_PROD_BATCH_NO; }
            set
            {
                this.m_PROD_BATCH_NO = value;
                this.NotifyPropertyChanged("PROD_BATCH_NO");
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

        #endregion //properties
    }

    public partial class dcPROD_REJECTION_CLOSING
    {
        public string CLOSING_ITEM_NAME { get; set; }
        public string CLOSING_UOM_NAME { get; set; }
        public string REJECT_ITEM_NAME { get; set; }
        public int BOM_ID { get; set; }
        private string m_FINISH_ITEM_NAME = "";

        public string FINISH_ITEM_NAME
        {
            get { return this.m_FINISH_ITEM_NAME; }
            set
            {
                this.m_FINISH_ITEM_NAME = value;
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
        private int M_CLOSING_ITEM_GROUP_ID = 0;
        public int CLOSING_ITEM_GROUP_ID
        {
            get { return this.M_CLOSING_ITEM_GROUP_ID; }
            set
            {
                this.M_CLOSING_ITEM_GROUP_ID = value;
            }
        }
    }
}
