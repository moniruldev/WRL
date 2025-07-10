using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "MRP_REQUISITION")]
    public class dcMRP_REQUISITION : DBBaseClass, INotifyPropertyChanged 
    {
        #region private members

        private string m_REQUI_NO = string.Empty;
        private DateTime? m_MPS_DATE = null;
        private DateTime? m_PO_DATE_CAL = null;
        private string m_CAT_ID = string.Empty;
        private string m_CAT_SUB_ID = string.Empty;
        private string m_ITEM_CODE = string.Empty;
        private string m_SUBMIT = string.Empty;
        private double m_PO_QNTY_CAL = 0;
        private string m_ENTER_BY = string.Empty;
        private DateTime? m_ENTER_DATE = null;
        private string m_NOTE = string.Empty;
        private string m_COMP_ID = string.Empty;
        private string m_MSR_ID = string.Empty;
        private Int16 m_SLNO = 0;
        private string m_ITEM_TYPE = string.Empty;
        private string m_RTYPE = string.Empty;
        private string m_RFTYPE = string.Empty;

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


        [DBColumn(Name = "REQUI_NO", Storage = "m_REQUI_NO", DbType = "126", IsPrimaryKey = true)]
        public string REQUI_NO
        {
            get { return this.m_REQUI_NO; }
            set
            {
                this.m_REQUI_NO = value;
                this.NotifyPropertyChanged("REQUI_NO");
            }
        }

        [DBColumn(Name = "MPS_DATE", Storage = "m_MPS_DATE", DbType = "106")]
        public DateTime? MPS_DATE
        {
            get { return this.m_MPS_DATE; }
            set
            {
                this.m_MPS_DATE = value;
                this.NotifyPropertyChanged("MPS_DATE");
            }
        }

        [DBColumn(Name = "PO_DATE_CAL", Storage = "m_PO_DATE_CAL", DbType = "106")]
        public DateTime? PO_DATE_CAL
        {
            get { return this.m_PO_DATE_CAL; }
            set
            {
                this.m_PO_DATE_CAL = value;
                this.NotifyPropertyChanged("PO_DATE_CAL");
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

        [DBColumn(Name = "SUBMIT", Storage = "m_SUBMIT", DbType = "126")]
        public string SUBMIT
        {
            get { return this.m_SUBMIT; }
            set
            {
                this.m_SUBMIT = value;
                this.NotifyPropertyChanged("SUBMIT");
            }
        }

        [DBColumn(Name = "PO_QNTY_CAL", Storage = "m_PO_QNTY_CAL", DbType = "108")]
        public double PO_QNTY_CAL
        {
            get { return this.m_PO_QNTY_CAL; }
            set
            {
                this.m_PO_QNTY_CAL = value;
                this.NotifyPropertyChanged("PO_QNTY_CAL");
            }
        }

        [DBColumn(Name = "ENTER_BY", Storage = "m_ENTER_BY", DbType = "126")]
        public string ENTER_BY
        {
            get { return this.m_ENTER_BY; }
            set
            {
                this.m_ENTER_BY = value;
                this.NotifyPropertyChanged("ENTER_BY");
            }
        }

        [DBColumn(Name = "ENTER_DATE", Storage = "m_ENTER_DATE", DbType = "106")]
        public DateTime? ENTER_DATE
        {
            get { return this.m_ENTER_DATE; }
            set
            {
                this.m_ENTER_DATE = value;
                this.NotifyPropertyChanged("ENTER_DATE");
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

        [DBColumn(Name = "COMP_ID", Storage = "m_COMP_ID", DbType = "126")]
        public string COMP_ID
        {
            get { return this.m_COMP_ID; }
            set
            {
                this.m_COMP_ID = value;
                this.NotifyPropertyChanged("COMP_ID");
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

        [DBColumn(Name = "SLNO", Storage = "m_SLNO", DbType = "111")]
        public Int16 SLNO
        {
            get { return this.m_SLNO; }
            set
            {
                this.m_SLNO = value;
                this.NotifyPropertyChanged("SLNO");
            }
        }

        [DBColumn(Name = "ITEM_TYPE", Storage = "m_ITEM_TYPE", DbType = "126")]
        public string ITEM_TYPE
        {
            get { return this.m_ITEM_TYPE; }
            set
            {
                this.m_ITEM_TYPE = value;
                this.NotifyPropertyChanged("ITEM_TYPE");
            }
        }

        [DBColumn(Name = "RTYPE", Storage = "m_RTYPE", DbType = "126")]
        public string RTYPE
        {
            get { return this.m_RTYPE; }
            set
            {
                this.m_RTYPE = value;
                this.NotifyPropertyChanged("RTYPE");
            }
        }

        [DBColumn(Name = "RFTYPE", Storage = "m_RFTYPE", DbType = "126")]
        public string RFTYPE
        {
            get { return this.m_RFTYPE; }
            set
            {
                this.m_RFTYPE = value;
                this.NotifyPropertyChanged("RFTYPE");
            }
        }

        #endregion //properties
    }
}
