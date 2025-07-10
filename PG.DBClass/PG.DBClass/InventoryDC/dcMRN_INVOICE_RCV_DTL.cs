using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "MRN_INVOICE_RCV_DTL")]
    public partial class dcMRN_INVOICE_RCV_DTL : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private string m_INV_NO = string.Empty;
        private decimal m_SLNO = 0;
        private string m_ITEM_CODE = string.Empty;
        private string m_CAT_ID = string.Empty;
        private string m_CAT_SUB_ID = string.Empty;
        private string m_MSR_ID = string.Empty;
        private string m_DISTRICT_CODE = string.Empty;
        private string m_DIVISION_CODE = string.Empty;
        private decimal m_SUP_QNTY = 0;
        private decimal m_UNIT_RATE = 0;
        private string m_COMP_ID = string.Empty;
        private string m_SUP_CODE = string.Empty;
        private string m_MRN_NO = string.Empty;
        private string m_STORE_ID = string.Empty;
        private decimal m_REJ_QNTY = 0;
        private decimal m_RCV_QNTY = 0;
        private string m_NOTE = string.Empty;
        private string m_COUNTRY_CODE = string.Empty;

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


        [DBColumn(Name = "INV_NO", Storage = "m_INV_NO", DbType = "126", IsPrimaryKey = true)]
        public string INV_NO
        {
            get { return this.m_INV_NO; }
            set
            {
                this.m_INV_NO = value;
                this.NotifyPropertyChanged("INV_NO");
            }
        }

        [DBColumn(Name = "SLNO", Storage = "m_SLNO", DbType = "107", IsPrimaryKey = true)]
        public decimal SLNO
        {
            get { return this.m_SLNO; }
            set
            {
                this.m_SLNO = value;
                this.NotifyPropertyChanged("SLNO");
            }
        }

        [DBColumn(Name = "ITEM_CODE", Storage = "m_ITEM_CODE", DbType = "126", IsPrimaryKey = true)]
        public string ITEM_CODE
        {
            get { return this.m_ITEM_CODE; }
            set
            {
                this.m_ITEM_CODE = value;
                this.NotifyPropertyChanged("ITEM_CODE");
            }
        }

        [DBColumn(Name = "CAT_ID", Storage = "m_CAT_ID", DbType = "126", IsPrimaryKey = true)]
        public string CAT_ID
        {
            get { return this.m_CAT_ID; }
            set
            {
                this.m_CAT_ID = value;
                this.NotifyPropertyChanged("CAT_ID");
            }
        }

        [DBColumn(Name = "CAT_SUB_ID", Storage = "m_CAT_SUB_ID", DbType = "126", IsPrimaryKey = true)]
        public string CAT_SUB_ID
        {
            get { return this.m_CAT_SUB_ID; }
            set
            {
                this.m_CAT_SUB_ID = value;
                this.NotifyPropertyChanged("CAT_SUB_ID");
            }
        }

        [DBColumn(Name = "MSR_ID", Storage = "m_MSR_ID", DbType = "126")]
        public string MSR_ID
        {
            get { return this.m_MSR_ID; }
            set
            {
                this.m_MSR_ID = value;
                this.NotifyPropertyChanged("MSR_ID");
            }
        }

        [DBColumn(Name = "DISTRICT_CODE", Storage = "m_DISTRICT_CODE", DbType = "126")]
        public string DISTRICT_CODE
        {
            get { return this.m_DISTRICT_CODE; }
            set
            {
                this.m_DISTRICT_CODE = value;
                this.NotifyPropertyChanged("DISTRICT_CODE");
            }
        }

        [DBColumn(Name = "DIVISION_CODE", Storage = "m_DIVISION_CODE", DbType = "126")]
        public string DIVISION_CODE
        {
            get { return this.m_DIVISION_CODE; }
            set
            {
                this.m_DIVISION_CODE = value;
                this.NotifyPropertyChanged("DIVISION_CODE");
            }
        }

        [DBColumn(Name = "SUP_QNTY", Storage = "m_SUP_QNTY", DbType = "107")]
        public decimal SUP_QNTY
        {
            get { return this.m_SUP_QNTY; }
            set
            {
                this.m_SUP_QNTY = value;
                this.NotifyPropertyChanged("SUP_QNTY");
            }
        }

        [DBColumn(Name = "UNIT_RATE", Storage = "m_UNIT_RATE", DbType = "107")]
        public decimal UNIT_RATE
        {
            get { return this.m_UNIT_RATE; }
            set
            {
                this.m_UNIT_RATE = value;
                this.NotifyPropertyChanged("UNIT_RATE");
            }
        }

        [DBColumn(Name = "COMP_ID", Storage = "m_COMP_ID", DbType = "126", IsPrimaryKey = true)]
        public string COMP_ID
        {
            get { return this.m_COMP_ID; }
            set
            {
                this.m_COMP_ID = value;
                this.NotifyPropertyChanged("COMP_ID");
            }
        }

        [DBColumn(Name = "SUP_CODE", Storage = "m_SUP_CODE", DbType = "126")]
        public string SUP_CODE
        {
            get { return this.m_SUP_CODE; }
            set
            {
                this.m_SUP_CODE = value;
                this.NotifyPropertyChanged("SUP_CODE");
            }
        }

        [DBColumn(Name = "MRN_NO", Storage = "m_MRN_NO", DbType = "126")]
        public string MRN_NO
        {
            get { return this.m_MRN_NO; }
            set
            {
                this.m_MRN_NO = value;
                this.NotifyPropertyChanged("MRN_NO");
            }
        }

        [DBColumn(Name = "STORE_ID", Storage = "m_STORE_ID", DbType = "126")]
        public string STORE_ID
        {
            get { return this.m_STORE_ID; }
            set
            {
                this.m_STORE_ID = value;
                this.NotifyPropertyChanged("STORE_ID");
            }
        }

        [DBColumn(Name = "REJ_QNTY", Storage = "m_REJ_QNTY", DbType = "107")]
        public decimal REJ_QNTY
        {
            get { return this.m_REJ_QNTY; }
            set
            {
                this.m_REJ_QNTY = value;
                this.NotifyPropertyChanged("REJ_QNTY");
            }
        }

        [DBColumn(Name = "RCV_QNTY", Storage = "m_RCV_QNTY", DbType = "107")]
        public decimal RCV_QNTY
        {
            get { return this.m_RCV_QNTY; }
            set
            {
                this.m_RCV_QNTY = value;
                this.NotifyPropertyChanged("RCV_QNTY");
            }
        }

        [DBColumn(Name = "NOTE", Storage = "m_NOTE", DbType = "126")]
        public string NOTE
        {
            get { return this.m_NOTE; }
            set
            {
                this.m_NOTE = value;
                this.NotifyPropertyChanged("NOTE");
            }
        }

        [DBColumn(Name = "COUNTRY_CODE", Storage = "m_COUNTRY_CODE", DbType = "126")]
        public string COUNTRY_CODE
        {
            get { return this.m_COUNTRY_CODE; }
            set
            {
                this.m_COUNTRY_CODE = value;
                this.NotifyPropertyChanged("COUNTRY_CODE");
            }
        }

        #endregion //properties
    }
}
