using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "SOLAR_BATT_OPENING")]
    public partial class dcSOLAR_BATT_OPENING : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_OPENING_ID = 0;
        private int m_OPEN_MST_ID = 0;
        private int m_ITEM_ID = 0;
        private int m_MONTH = 0;
        private int m_YEAR = 0;
        private decimal m_C_PACKED = 0;
        private decimal m_C_UNPACKED = 0;
        private decimal m_UN_GREEN = 0;
        private decimal m_UN_FORMED = 0;
        private decimal m_OP_ON_CHARGED = 0;
        private int m_DEPT_ID = 0;
        private decimal m_TOTAL_OPENNING = 0;


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


        [DBColumn(Name = "OPENING_ID", Storage = "m_OPENING_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int OPENING_ID
        {
            get { return this.m_OPENING_ID; }
            set
            {
                this.m_OPENING_ID = value;
                this.NotifyPropertyChanged("OPENING_ID");
            }
        }

        [DBColumn(Name = "OPEN_MST_ID", Storage = "m_OPEN_MST_ID", DbType = "107")]
        public int OPEN_MST_ID
        {
            get { return this.m_OPEN_MST_ID; }
            set
            {
                this.m_OPEN_MST_ID = value;
                this.NotifyPropertyChanged("OPEN_MST_ID");
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

        [DBColumn(Name = "MONTH", Storage = "m_MONTH", DbType = "107")]
        public int MONTH
        {
            get { return this.m_MONTH; }
            set
            {
                this.m_MONTH = value;
                this.NotifyPropertyChanged("MONTH");
            }
        }

        [DBColumn(Name = "YEAR", Storage = "m_YEAR", DbType = "107")]
        public int YEAR
        {
            get { return this.m_YEAR; }
            set
            {
                this.m_YEAR = value;
                this.NotifyPropertyChanged("YEAR");
            }
        }

        [DBColumn(Name = "C_PACKED", Storage = "m_C_PACKED", DbType = "107")]
        public decimal C_PACKED
        {
            get { return this.m_C_PACKED; }
            set
            {
                this.m_C_PACKED = value;
                this.NotifyPropertyChanged("C_PACKED");
            }
        }

        [DBColumn(Name = "C_UNPACKED", Storage = "m_C_UNPACKED", DbType = "107")]
        public decimal C_UNPACKED
        {
            get { return this.m_C_UNPACKED; }
            set
            {
                this.m_C_UNPACKED = value;
                this.NotifyPropertyChanged("C_UNPACKED");
            }
        }

        [DBColumn(Name = "UN_GREEN", Storage = "m_UN_GREEN", DbType = "107")]
        public decimal UN_GREEN
        {
            get { return this.m_UN_GREEN; }
            set
            {
                this.m_UN_GREEN = value;
                this.NotifyPropertyChanged("UN_GREEN");
            }
        }

        [DBColumn(Name = "UN_FORMED", Storage = "m_UN_FORMED", DbType = "107")]
        public decimal UN_FORMED
        {
            get { return this.m_UN_FORMED; }
            set
            {
                this.m_UN_FORMED = value;
                this.NotifyPropertyChanged("UN_FORMED");
            }
        }

        [DBColumn(Name = "OP_ON_CHARGED", Storage = "m_OP_ON_CHARGED", DbType = "107")]
        public decimal OP_ON_CHARGED
        {
            get { return this.m_OP_ON_CHARGED; }
            set
            {
                this.m_OP_ON_CHARGED = value;
                this.NotifyPropertyChanged("OP_ON_CHARGED");
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


         [DBColumn(Name = "TOTAL_OPENNING", Storage = "m_TOTAL_OPENNING", DbType = "107")]
        public decimal TOTAL_OPENNING
        {
            get { return this.m_TOTAL_OPENNING; }
            set
            {
                this.m_TOTAL_OPENNING = value;
                this.NotifyPropertyChanged("TOTAL_OPENNING");
            }
        }


        
        #endregion //properties
    }

    public partial class dcSOLAR_BATT_OPENING
    {
        public decimal OPENNING_QTY { set; get; }
        public string ITEM_NAME { set; get; }
        public decimal TOTAL_CHARGE_STOCK { set; get; }
        public decimal TOTAL_UN_CHARGE_STOCK { set; get; }
    }
}
