using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "CAST_LEAD_REPROCESS_FG_DTL")]
    public partial class dcCAST_LEAD_REPROCESS_FG_DTL : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_LEAD_FG_DTL_ID = 0;
        private int m_LEAD_PROCESS_MST_ID = 0;
        private int m_FG_ITEM_ID = 0;
        private int m_UOM_ID = 0;
        private decimal m_PROCESS_QTY = 0;
        private string m_REMARKS = string.Empty;

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


        [DBColumn(Name = "LEAD_FG_DTL_ID", Storage = "m_LEAD_FG_DTL_ID", DbType = "107", IsPrimaryKey = true)]
        public int LEAD_FG_DTL_ID
        {
            get { return this.m_LEAD_FG_DTL_ID; }
            set
            {
                this.m_LEAD_FG_DTL_ID = value;
                this.NotifyPropertyChanged("LEAD_FG_DTL_ID");
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

        [DBColumn(Name = "PROCESS_QTY", Storage = "m_PROCESS_QTY", DbType = "107")]
        public decimal PROCESS_QTY
        {
            get { return this.m_PROCESS_QTY; }
            set
            {
                this.m_PROCESS_QTY = value;
                this.NotifyPropertyChanged("PROCESS_QTY");
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

        #endregion //properties
    }

     public partial class dcCAST_LEAD_REPROCESS_FG_DTL
     {
         public string ITEM_NAME { get; set; }
         public string UOM_NAME { get; set; }
         public int SLNO { get; set; }
     }
    
}
