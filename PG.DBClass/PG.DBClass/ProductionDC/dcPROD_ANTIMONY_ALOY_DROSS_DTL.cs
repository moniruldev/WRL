using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "PROD_ANTIMONY_ALOY_DROSS_DTL")]
    public partial class dcPROD_ANTIMONY_ALOY_DROSS_DTL : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_PUR_DROSS_DTL_ID = 0;
        private int m_DROSS_AN_MST_ID = 0;
        private decimal m_MACHINE_ID = 0;
        private decimal m_UOM_ID = 0;
        private decimal m_ITEM_ID = 0;
        private string m_REMARKS = string.Empty;
        private decimal m_ITEM_QTY = 0;
        private int m_PROD_MST_ID = 0;
        private int m_SLNO = 0;

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


        [DBColumn(Name = "PUR_DROSS_DTL_ID", Storage = "m_PUR_DROSS_DTL_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int PUR_DROSS_DTL_ID
        {
            get { return this.m_PUR_DROSS_DTL_ID; }
            set
            {
                this.m_PUR_DROSS_DTL_ID = value;
                this.NotifyPropertyChanged("PUR_DROSS_DTL_ID");
            }
        }

        [DBColumn(Name = "DROSS_AN_MST_ID", Storage = "m_DROSS_AN_MST_ID", DbType = "107")]
        public int DROSS_AN_MST_ID
        {
            get { return this.m_DROSS_AN_MST_ID; }
            set
            {
                this.m_DROSS_AN_MST_ID = value;
                this.NotifyPropertyChanged("DROSS_AN_MST_ID");
            }
        }

        [DBColumn(Name = "MACHINE_ID", Storage = "m_MACHINE_ID", DbType = "107")]
        public decimal MACHINE_ID
        {
            get { return this.m_MACHINE_ID; }
            set
            {
                this.m_MACHINE_ID = value;
                this.NotifyPropertyChanged("MACHINE_ID");
            }
        }

        [DBColumn(Name = "UOM_ID", Storage = "m_UOM_ID", DbType = "107")]
        public decimal UOM_ID
        {
            get { return this.m_UOM_ID; }
            set
            {
                this.m_UOM_ID = value;
                this.NotifyPropertyChanged("UOM_ID");
            }
        }

        [DBColumn(Name = "ITEM_ID", Storage = "m_ITEM_ID", DbType = "107")]
        public decimal ITEM_ID
        {
            get { return this.m_ITEM_ID; }
            set
            {
                this.m_ITEM_ID = value;
                this.NotifyPropertyChanged("ITEM_ID");
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

         [DBColumn(Name = "SLNO", Storage = "m_SLNO", DbType = "107")]
         public int SLNO
         {
             get { return this.m_SLNO; }
             set
             {
                 this.m_SLNO = value;
                 this.NotifyPropertyChanged("SLNO");
             }
         }
        
        #endregion //properties
    }

    public partial class dcPROD_ANTIMONY_ALOY_DROSS_DTL
    {
        private string m_ITEM_NAME = string.Empty;
        public string ITEM_NAME
        {
            get { return m_ITEM_NAME; }
            set { m_ITEM_NAME = value; }
        }

        private string m_UOM_NAME = string.Empty;
        public string UOM_NAME
        {
            get { return m_UOM_NAME; }
            set { m_UOM_NAME = value; }
        }



        private string m_USED_ITEM_NAME = string.Empty;
        public string USED_ITEM_NAME
        {
            get { return m_USED_ITEM_NAME; }
            set { m_USED_ITEM_NAME = value; }
        }

        private string m_USED_ITEM_UOM_NAME = string.Empty;
        public string USED_ITEM_UOM_NAME
        {
            get { return m_USED_ITEM_UOM_NAME; }
            set { m_USED_ITEM_UOM_NAME = value; }
        }

        private int m_USED_ITEM_UOM_ID = 0;
        public int USED_ITEM_UOM_ID
        {
            get { return m_USED_ITEM_UOM_ID; }
            set { m_USED_ITEM_UOM_ID = value; }
        }

        private string m_DROSS_AN_NO = string.Empty;
        public string DROSS_AN_NO
        {
            get { return m_DROSS_AN_NO; }
            set { m_DROSS_AN_NO = value; }
        }

        public decimal CLOSING_QTY { get; set; }

    }
}
