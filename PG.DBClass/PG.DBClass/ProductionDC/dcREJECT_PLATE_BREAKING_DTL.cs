using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "REJECT_PLATE_BREAKING_DTL")]
    public partial class dcREJECT_PLATE_BREAKING_DTL : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_REJECT_PLATE_BREAKING_DTL_ID = 0;
        private int m_REJECT_PLATE_BREAKING_MST_ID = 0;
        private decimal m_ITEM_ID = 0;
        private decimal m_QTY = 0;
        private string m_REMARKS_DTL = string.Empty;
        private int m_UOM_ID = 0;

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


        [DBColumn(Name = "REJECT_PLATE_BREAKING_DTL_ID", Storage = "m_REJECT_PLATE_BREAKING_DTL_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int REJECT_PLATE_BREAKING_DTL_ID
        {
            get { return this.m_REJECT_PLATE_BREAKING_DTL_ID; }
            set
            {
                this.m_REJECT_PLATE_BREAKING_DTL_ID = value;
                this.NotifyPropertyChanged("REJECT_PLATE_BREAKING_DTL_ID");
            }
        }

        [DBColumn(Name = "REJECT_PLATE_BREAKING_MST_ID", Storage = "m_REJECT_PLATE_BREAKING_MST_ID", DbType = "107")]
        public int REJECT_PLATE_BREAKING_MST_ID
        {
            get { return this.m_REJECT_PLATE_BREAKING_MST_ID; }
            set
            {
                this.m_REJECT_PLATE_BREAKING_MST_ID = value;
                this.NotifyPropertyChanged("REJECT_PLATE_BREAKING_MST_ID");
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

        [DBColumn(Name = "QTY", Storage = "m_QTY", DbType = "107")]
        public decimal QTY
        {
            get { return this.m_QTY; }
            set
            {
                this.m_QTY = value;
                this.NotifyPropertyChanged("QTY");
            }
        }

        [DBColumn(Name = "REMARKS_DTL", Storage = "m_REMARKS_DTL", DbType = "126")]
        public string REMARKS_DTL
        {
            get { return this.m_REMARKS_DTL; }
            set
            {
                this.m_REMARKS_DTL = value;
                this.NotifyPropertyChanged("REMARKS_DTL");
            }
        }

        #endregion //properties
    }

     public partial class dcREJECT_PLATE_BREAKING_DTL
     {

         public string ITEM_NAME { get; set; }
         public string UOM_NAME { get; set; }
     }


}
