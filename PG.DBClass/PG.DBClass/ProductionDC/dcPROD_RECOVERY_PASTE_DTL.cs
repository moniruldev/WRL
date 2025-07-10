using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "PROD_RECOVERY_PASTE_DTL")]
    public partial class dcPROD_RECOVERY_PASTE_DTL : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_RECOVERY_DTL_ID = 0;
        private int m_RECOVERY_ID = 0;
        private int m_ITEM_ID = 0;
        private int m_UOM_ID = 0;
        private decimal m_ITEM_QTY = 0;
        private string m_REMARKS = string.Empty;
        private int m_SI = 0;
        private decimal m_CLOSING_QTY = 0;
        private int m_PROD_ID = 0;

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


        [DBColumn(Name = "RECOVERY_DTL_ID", Storage = "m_RECOVERY_DTL_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int RECOVERY_DTL_ID
        {
            get { return this.m_RECOVERY_DTL_ID; }
            set
            {
                this.m_RECOVERY_DTL_ID = value;
                this.NotifyPropertyChanged("RECOVERY_DTL_ID");
            }
        }

        [DBColumn(Name = "RECOVERY_ID", Storage = "m_RECOVERY_ID", DbType = "107")]
        public int RECOVERY_ID
        {
            get { return this.m_RECOVERY_ID; }
            set
            {
                this.m_RECOVERY_ID = value;
                this.NotifyPropertyChanged("RECOVERY_ID");
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

        [DBColumn(Name = "SI", Storage = "m_SI", DbType = "107")]
        public int SI
        {
            get { return this.m_SI; }
            set
            {
                this.m_SI = value;
                this.NotifyPropertyChanged("SI");
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

        [DBColumn(Name = "PROD_ID", Storage = "m_PROD_ID", DbType = "107")]
        public int PROD_ID
        {
            get { return this.m_PROD_ID; }
            set
            {
                this.m_PROD_ID = value;
                this.NotifyPropertyChanged("PROD_ID");
            }
        }

        #endregion //properties
    }

     public partial class dcPROD_RECOVERY_PASTE_DTL
     {
         public string ITEM_NAME { get; set; }
         public string UOM_NAME { get; set; }
     }
}
