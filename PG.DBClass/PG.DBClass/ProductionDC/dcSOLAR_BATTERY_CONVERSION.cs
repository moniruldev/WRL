using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "SOLAR_BATTERY_CONVERSION")]
    public partial class dcSOLAR_BATTERY_CONVERSION : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_CONVERT_ID = 0;
        private decimal m_DEPT_ID = 0;
        private string m_CONVERT_NO = string.Empty;
        private decimal m_UOM_ID = 0;
        private decimal m_FROM_ITEM_ID = 0;
        private decimal m_TO_ITEM_ID = 0;
        private int m_IS_RETURN_RM = 0;
        private decimal m_QTY = 0;
        private string m_REMARKS = string.Empty;
        private DateTime? m_CONVERT_DATE = null;

        private DateTime? m_AUTH_DATE = null;
        private DateTime? m_ENTRY_DATE = null;
        private DateTime? m_EDIT_DATE = null; 
        private string  m_AUTH_STATUS = String.Empty;
        private int  m_AUTH_BY_ID =0;
        private int  m_ENTRY_BY=0;
        private int m_EDIT_BY = 0;
        private int m_BOM_ID = 0;
        private string m_BOM_NO = String.Empty;
 

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


        [DBColumn(Name = "CONVERT_ID", Storage = "m_CONVERT_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int CONVERT_ID
        {
            get { return this.m_CONVERT_ID; }
            set
            {
                this.m_CONVERT_ID = value;
                this.NotifyPropertyChanged("CONVERT_ID");
            }
        }

        [DBColumn(Name = "CONVERT_NO", Storage = "m_CONVERT_NO", DbType = "126")]
        public string CONVERT_NO
        {
            get { return this.m_CONVERT_NO; }
            set
            {
                this.m_CONVERT_NO = value;
                this.NotifyPropertyChanged("CONVERT_NO");
            }
        }


        [DBColumn(Name = "CONVERT_DATE", Storage = "m_CONVERT_DATE", DbType = "106")]
        public DateTime? CONVERT_DATE
        {
            get { return this.m_CONVERT_DATE; }
            set
            {
                this.m_CONVERT_DATE = value;
                this.NotifyPropertyChanged("CONVERT_DATE");
            }
        }


        [DBColumn(Name = "DEPT_ID", Storage = "m_DEPT_ID", DbType = "107")]
        public decimal DEPT_ID
        {
            get { return this.m_DEPT_ID; }
            set
            {
                this.m_DEPT_ID = value;
                this.NotifyPropertyChanged("DEPT_ID");
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

        [DBColumn(Name = "FROM_ITEM_ID", Storage = "m_FROM_ITEM_ID", DbType = "107")]
        public decimal FROM_ITEM_ID
        {
            get { return this.m_FROM_ITEM_ID; }
            set
            {
                this.m_FROM_ITEM_ID = value;
                this.NotifyPropertyChanged("FROM_ITEM_ID");
            }
        }

        [DBColumn(Name = "TO_ITEM_ID", Storage = "m_TO_ITEM_ID", DbType = "107")]
        public decimal TO_ITEM_ID
        {
            get { return this.m_TO_ITEM_ID; }
            set
            {
                this.m_TO_ITEM_ID = value;
                this.NotifyPropertyChanged("TO_ITEM_ID");
            }
        }

        [DBColumn(Name = "IS_RETURN_RM", Storage = "m_IS_RETURN_RM", DbType = "107")]
        public int IS_RETURN_RM
        {
            get { return this.m_IS_RETURN_RM; }
            set
            {
                this.m_IS_RETURN_RM = value;
                this.NotifyPropertyChanged("IS_RETURN_RM");
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


        [DBColumn(Name = "AUTH_DATE", Storage = "m_AUTH_DATE", DbType = "106")]
        public DateTime? AUTH_DATE
        {
            get { return this.m_AUTH_DATE; }
            set
            {
                this.m_AUTH_DATE = value;
                this.NotifyPropertyChanged("AUTH_DATE");
            }
        }

        [DBColumn(Name = "ENTRY_DATE", Storage = "m_ENTRY_DATE", DbType = "106")]
        public DateTime? ENTRY_DATE
        {
            get { return this.m_ENTRY_DATE; }
            set
            {
                this.m_ENTRY_DATE = value;
                this.NotifyPropertyChanged("ENTRY_DATE");
            }
        }

        [DBColumn(Name = "EDIT_DATE", Storage = "m_EDIT_DATE", DbType = "106")]
        public DateTime? EDIT_DATE
        {
            get { return this.m_EDIT_DATE; }
            set
            {
                this.m_EDIT_DATE = value;
                this.NotifyPropertyChanged("EDIT_DATE");
            }
        }


        [DBColumn(Name = "AUTH_BY_ID", Storage = "m_AUTH_BY_ID", DbType = "107")]
        public int AUTH_BY_ID
        {
            get { return this.m_AUTH_BY_ID; }
            set
            {
                this.m_AUTH_BY_ID = value;
                this.NotifyPropertyChanged("AUTH_BY_ID");
            }
        }
        [DBColumn(Name = "ENTRY_BY", Storage = "m_ENTRY_BY", DbType = "107")]
        public int ENTRY_BY
        {
            get { return this.m_ENTRY_BY; }
            set
            {
                this.m_ENTRY_BY = value;
                this.NotifyPropertyChanged("ENTRY_BY");
            }
        }
        [DBColumn(Name = "EDIT_BY", Storage = "m_EDIT_BY", DbType = "107")]
        public int EDIT_BY
        {
            get { return this.m_EDIT_BY; }
            set
            {
                this.m_EDIT_BY = value;
                this.NotifyPropertyChanged("EDIT_BY");
            }
        }

      [DBColumn(Name = "AUTH_STATUS", Storage = "m_AUTH_STATUS", DbType = "126")]
        public string AUTH_STATUS
        {
            get { return this.m_AUTH_STATUS; }
            set
            {
                this.m_AUTH_STATUS = value;
                this.NotifyPropertyChanged("AUTH_STATUS");
            }
        }

      [DBColumn(Name = "BOM_ID", Storage = "m_BOM_ID", DbType = "107")]
      public int BOM_ID
      {
          get { return this.m_BOM_ID; }
          set
          {
              this.m_BOM_ID = value;
              this.NotifyPropertyChanged("BOM_ID");
          }
      }

      [DBColumn(Name = "BOM_NO", Storage = "m_BOM_NO", DbType = "126")]
      public string BOM_NO
      {
          get { return this.m_BOM_NO; }
          set
          {
              this.m_BOM_NO = value;
              this.NotifyPropertyChanged("BOM_NO");
          }
      }
        #endregion //properties
    }

    public partial class dcSOLAR_BATTERY_CONVERSION
    {
        public string FROM_ITEM_NAME { get; set; }
        public string TO_ITEM_NAME { get; set; }
        public string UOM_NAME { get; set; }
        public string UOM_CODE_SHORT { get; set; }
        //public string AUTH_STATUS { get; set; }
    }
}
