using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "PROD_PASTING_WASTAGE_MST")]
    public partial class dcPROD_PASTING_WASTAGE_MST : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_WASTAGE_ID = 0;
        private string m_WASTAGE_NO = string.Empty;
        private int m_DEPT_ID = 0;
        private DateTime? m_WASTAGE_DATE = null;
        private string m_CREATE_BY = string.Empty;
        private DateTime? m_CREATE_DATE = null;
        private string m_UPDATE_BY = string.Empty;
        private DateTime? m_UPDATE_DATE = null;
        private string m_AUTHO_STATUS = string.Empty;
        private DateTime? m_AUTHO_DATE = null;
        private string m_SHIFT_ID = string.Empty;

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


        [DBColumn(Name = "WASTAGE_ID", Storage = "m_WASTAGE_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int WASTAGE_ID
        {
            get { return this.m_WASTAGE_ID; }
            set
            {
                this.m_WASTAGE_ID = value;
                this.NotifyPropertyChanged("WASTAGE_ID");
            }
        }

        [DBColumn(Name = "WASTAGE_NO", Storage = "m_WASTAGE_NO", DbType = "126")]
        public string WASTAGE_NO
        {
            get { return this.m_WASTAGE_NO; }
            set
            {
                this.m_WASTAGE_NO = value;
                this.NotifyPropertyChanged("WASTAGE_NO");
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

        [DBColumn(Name = "WASTAGE_DATE", Storage = "m_WASTAGE_DATE", DbType = "106")]
        public DateTime? WASTAGE_DATE
        {
            get { return this.m_WASTAGE_DATE; }
            set
            {
                this.m_WASTAGE_DATE = value;
                this.NotifyPropertyChanged("WASTAGE_DATE");
            }
        }

        [DBColumn(Name = "CREATE_BY", Storage = "m_CREATE_BY", DbType = "126")]
        public string CREATE_BY
        {
            get { return this.m_CREATE_BY; }
            set
            {
                this.m_CREATE_BY = value;
                this.NotifyPropertyChanged("CREATE_BY");
            }
        }

        [DBColumn(Name = "CREATE_DATE", Storage = "m_CREATE_DATE", DbType = "106")]
        public DateTime? CREATE_DATE
        {
            get { return this.m_CREATE_DATE; }
            set
            {
                this.m_CREATE_DATE = value;
                this.NotifyPropertyChanged("CREATE_DATE");
            }
        }

        [DBColumn(Name = "UPDATE_BY", Storage = "m_UPDATE_BY", DbType = "126")]
        public string UPDATE_BY
        {
            get { return this.m_UPDATE_BY; }
            set
            {
                this.m_UPDATE_BY = value;
                this.NotifyPropertyChanged("UPDATE_BY");
            }
        }

        [DBColumn(Name = "UPDATE_DATE", Storage = "m_UPDATE_DATE", DbType = "106")]
        public DateTime? UPDATE_DATE
        {
            get { return this.m_UPDATE_DATE; }
            set
            {
                this.m_UPDATE_DATE = value;
                this.NotifyPropertyChanged("UPDATE_DATE");
            }
        }


        [DBColumn(Name = "AUTHO_STATUS", Storage = "m_AUTHO_STATUS", DbType = "126")]
        public string AUTHO_STATUS
        {
            get { return this.m_AUTHO_STATUS; }
            set
            {
                this.m_AUTHO_STATUS = value;
                this.NotifyPropertyChanged("AUTHO_STATUS");
            }
        }

        [DBColumn(Name = "AUTHO_DATE", Storage = "m_AUTHO_DATE", DbType = "106")]
        public DateTime? AUTHO_DATE
        {
            get { return this.m_AUTHO_DATE; }
            set
            {
                this.m_AUTHO_DATE = value;
                this.NotifyPropertyChanged("AUTHO_DATE");
            }
        }



        [DBColumn(Name = "SHIFT_ID", Storage = "m_SHIFT_ID", DbType = "126")]
        public string SHIFT_ID
        {
            get { return this.m_SHIFT_ID; }
            set
            {
                this.m_SHIFT_ID = value;
                this.NotifyPropertyChanged("SHIFT_ID");
            }
        }
        #endregion //properties
    }


    public partial class dcPROD_PASTING_WASTAGE_MST
    {
        private List<dcPROD_PASTING_WASTAGE_DTL> m_WastageDetList = null;
        public List<dcPROD_PASTING_WASTAGE_DTL> WastageDetList
        {
            get { return m_WastageDetList; }
            set { m_WastageDetList = value; }
        }

        private int m_WASTAGE_DTL_ID = 0;
        private int m_WASTAGE_MST_ID = 0;
        private int m_ITEM_ID = 0;
        private int m_UOM_ID = 0;
        private decimal m_ITEM_QTY = 0;
        private string m_REMARKS = string.Empty;
        private int m_SI = 0;


        public int WASTAGE_DTL_ID   
        {
            get { return m_WASTAGE_DTL_ID; }
            set { m_WASTAGE_DTL_ID = value; }
        }

        public int  ITEM_ID
        {
            get { return m_ITEM_ID; }
            set { m_ITEM_ID = value; }
        }

        public int UOM_ID
        {
            get { return m_UOM_ID; }
            set { m_UOM_ID = value; }
        }

        public decimal ITEM_QTY
        {
            get { return m_ITEM_QTY; }
            set { m_ITEM_QTY = value; }
        }
        public string REMARKS
        {
            get { return m_REMARKS; }
            set { m_REMARKS = value; }
        }
        public int SI
        {
            get { return m_SI; }
            set { m_SI = value; }
        }

        private string m_CREATE_BY_NAME = String.Empty;
        public string CREATE_BY_NAME
        {
            get { return m_CREATE_BY_NAME; }
            set { m_CREATE_BY_NAME = value; }
        }
    }
}
