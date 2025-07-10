using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "LP_DEL_CHALL_DTL")]
    public partial class dcLP_DEL_CHALL_DTL : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private string m_CHALL_NO = string.Empty;
        private decimal m_SLNO = 0;
        private string m_ITEM_CODE = string.Empty;
        private string m_CAT_ID = string.Empty;
        private string m_CAT_SUB_ID = string.Empty;
        private string m_MSR_ID = string.Empty;
        private decimal m_SUP_QNTY = 0;
        private decimal m_UNIT_RATE = 0;
        private string m_COMP_ID = string.Empty;
        private string m_SUP_CODE = string.Empty;
        private string m_STORE_ID = string.Empty;
        private string m_DELIVERY_NOTE = string.Empty;
        private decimal m_SCALE_QNTY = 0;
        private string m_MRN_NO = string.Empty;

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


        [DBColumn(Name = "CHALL_NO", Storage = "m_CHALL_NO", DbType = "126", IsPrimaryKey = true)]
        public string CHALL_NO
        {
            get { return this.m_CHALL_NO; }
            set
            {
                this.m_CHALL_NO = value;
                this.NotifyPropertyChanged("CHALL_NO");
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

        [DBColumn(Name = "ITEM_CODE", Storage = "m_ITEM_CODE", DbType = "126")]
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

        [DBColumn(Name = "DELIVERY_NOTE", Storage = "m_DELIVERY_NOTE", DbType = "126")]
        public string DELIVERY_NOTE
        {
            get { return this.m_DELIVERY_NOTE; }
            set
            {
                this.m_DELIVERY_NOTE = value;
                this.NotifyPropertyChanged("DELIVERY_NOTE");
            }
        }

        [DBColumn(Name = "SCALE_QNTY", Storage = "m_SCALE_QNTY", DbType = "107")]
        public decimal SCALE_QNTY
        {
            get { return this.m_SCALE_QNTY; }
            set
            {
                this.m_SCALE_QNTY = value;
                this.NotifyPropertyChanged("SCALE_QNTY");
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

        #endregion //properties
    }
}
