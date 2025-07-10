using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "PROD_BATT_BREAKING_TEMP_BATCH")]
    public partial class dcPROD_BATT_BREAKING_TEMP_BATCH : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_BREAKING_ID = 0;
        private string m_BREAKING_NO = string.Empty;
        private int m_ITEM_ID = 0;
        private int m_DEPT_ID = 0;
        private int m_STLM_ID = 0;
        private string m_BREAKING_BATCH_NO = string.Empty;
        private decimal m_CLOSING_QTY = 0;
        private int m_UOM_ID = 0;
        private decimal m_USED_QTY = 0;
        private int m_SL_NO = 0;
        private int m_MACHINE_ID = 0;
        private int m_FG_ITEM_ID = 0;

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


        [DBColumn(Name = "BREAKING_ID", Storage = "m_BREAKING_ID", DbType = "107")]
        public int BREAKING_ID
        {
            get { return this.m_BREAKING_ID; }
            set
            {
                this.m_BREAKING_ID = value;
                this.NotifyPropertyChanged("BREAKING_ID");
            }
        }

        [DBColumn(Name = "BREAKING_NO", Storage = "m_BREAKING_NO", DbType = "126")]
        public string BREAKING_NO
        {
            get { return this.m_BREAKING_NO; }
            set
            {
                this.m_BREAKING_NO = value;
                this.NotifyPropertyChanged("BREAKING_NO");
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

        [DBColumn(Name = "BREAKING_BATCH_NO", Storage = "m_BREAKING_BATCH_NO", DbType = "126")]
        public string BREAKING_BATCH_NO
        {
            get { return this.m_BREAKING_BATCH_NO; }
            set
            {
                this.m_BREAKING_BATCH_NO = value;
                this.NotifyPropertyChanged("BREAKING_BATCH_NO");
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

        [DBColumn(Name = "USED_QTY", Storage = "m_USED_QTY", DbType = "107")]
        public decimal USED_QTY
        {
            get { return this.m_USED_QTY; }
            set
            {
                this.m_USED_QTY = value;
                this.NotifyPropertyChanged("USED_QTY");
            }
        }

        [DBColumn(Name = "SL_NO", Storage = "m_SL_NO", DbType = "107")]
        public int SL_NO
        {
            get { return this.m_SL_NO; }
            set
            {
                this.m_SL_NO = value;
                this.NotifyPropertyChanged("SL_NO");
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

        [DBColumn(Name = "FG_ITEM_ID", Storage = "m_FG_ITEM_ID", DbType = "107")]
        public int FG_ITEM_ID
        {
            get { return this.m_FG_ITEM_ID; }
            set
            {
                this.m_FG_ITEM_ID = value;
                this.NotifyPropertyChanged("FG_ITEM_ID");
            }
        }

        #endregion //properties
    }
}
