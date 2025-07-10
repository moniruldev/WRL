using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "CAST_LEAD_REPROCESS_DROSS_DTL")]
    public partial class dcCAST_LEAD_REPROCESS_DROSS_DTL : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private string m_REMARKS = string.Empty;
        private int m_DROSS_DTL_ID = 0;
        private int m_LEAD_PROCESS_MST_ID = 0;
        private int m_DROSS_ITEM_ID = 0;
        private int m_UOM_ID = 0;
        private decimal m_DROSS_QTY = 0;

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

        [DBColumn(Name = "DROSS_DTL_ID", Storage = "m_DROSS_DTL_ID", DbType = "107", IsPrimaryKey = true)]
        public int DROSS_DTL_ID
        {
            get { return this.m_DROSS_DTL_ID; }
            set
            {
                this.m_DROSS_DTL_ID = value;
                this.NotifyPropertyChanged("DROSS_DTL_ID");
            }
        }

        [DBColumn(Name = "LEAD_PROCESS_MST_ID", Storage = "m_LEAD_PROCESS_MST_ID", DbType = "107")]
        public int LEAD_PROCESS_MST_ID
        {
            get { return this.m_LEAD_PROCESS_MST_ID; }
            set
            {
                this.m_LEAD_PROCESS_MST_ID = value;
                this.NotifyPropertyChanged("LEAD_PROCESS_MST_ID");
            }
        }

        [DBColumn(Name = "DROSS_ITEM_ID", Storage = "m_DROSS_ITEM_ID", DbType = "107")]
        public int DROSS_ITEM_ID
        {
            get { return this.m_DROSS_ITEM_ID; }
            set
            {
                this.m_DROSS_ITEM_ID = value;
                this.NotifyPropertyChanged("DROSS_ITEM_ID");
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

        [DBColumn(Name = "DROSS_QTY", Storage = "m_DROSS_QTY", DbType = "107")]
        public decimal DROSS_QTY
        {
            get { return this.m_DROSS_QTY; }
            set
            {
                this.m_DROSS_QTY = value;
                this.NotifyPropertyChanged("DROSS_QTY");
            }
        }

        #endregion //properties
    }
}
