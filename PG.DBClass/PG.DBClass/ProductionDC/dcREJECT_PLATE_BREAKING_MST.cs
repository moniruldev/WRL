using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "REJECT_PLATE_BREAKING_MST")]
    public partial class dcREJECT_PLATE_BREAKING_MST : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_REJECT_PLATE_BREAKING_MST_ID = 0;
        private int m_REJECT_ITEM_ID = 0;
        private int m_DEPT_ID = 0;
        private DateTime? m_BREAKING_DATE = null;
        private decimal m_BREAK_QTY = 0;
        private string m_REMARKS = string.Empty;
        private DateTime? m_CREATE_DATE = null;
        private string m_CREATE_BY = string.Empty;
        private string m_EDIT_BY = string.Empty;
        private DateTime? m_EDIT_DATE = null;
        private string m_AUTHO_STATUS = null;
        private DateTime? m_AUTHO_DATE = null;
        private string m_PLATE_BREAKING_NO = null;
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


        [DBColumn(Name = "REJECT_PLATE_BREAKING_MST_ID", Storage = "m_REJECT_PLATE_BREAKING_MST_ID",DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int REJECT_PLATE_BREAKING_MST_ID
        {
            get { return this.m_REJECT_PLATE_BREAKING_MST_ID; }
            set
            {
                this.m_REJECT_PLATE_BREAKING_MST_ID = value;
                this.NotifyPropertyChanged("REJECT_PLATE_BREAKING_MST_ID");
            }
        }


        [DBColumn(Name = "PLATE_BREAKING_NO", Storage = "m_PLATE_BREAKING_NO", DbType = "107")]
        public string PLATE_BREAKING_NO
        {
            get { return this.m_PLATE_BREAKING_NO; }
            set
            {
                this.m_PLATE_BREAKING_NO = value;
                this.NotifyPropertyChanged("PLATE_BREAKING_NO");
            }
        }


        [DBColumn(Name = "REJECT_ITEM_ID", Storage = "m_REJECT_ITEM_ID", DbType = "107")]
        public int REJECT_ITEM_ID
        {
            get { return this.m_REJECT_ITEM_ID; }
            set
            {
                this.m_REJECT_ITEM_ID = value;
                this.NotifyPropertyChanged("REJECT_ITEM_ID");
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

        [DBColumn(Name = "BREAKING_DATE", Storage = "m_BREAKING_DATE", DbType = "106")]
        public DateTime? BREAKING_DATE
        {
            get { return this.m_BREAKING_DATE; }
            set
            {
                this.m_BREAKING_DATE = value;
                this.NotifyPropertyChanged("BREAKING_DATE");
            }
        }

        [DBColumn(Name = "BREAK_QTY", Storage = "m_BREAK_QTY", DbType = "107")]
        public decimal BREAK_QTY
        {
            get { return this.m_BREAK_QTY; }
            set
            {
                this.m_BREAK_QTY = value;
                this.NotifyPropertyChanged("BREAK_QTY");
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

        [DBColumn(Name = "EDIT_BY", Storage = "m_EDIT_BY", DbType = "126")]
        public string EDIT_BY
        {
            get { return this.m_EDIT_BY; }
            set
            {
                this.m_EDIT_BY = value;
                this.NotifyPropertyChanged("EDIT_BY");
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


        #endregion //properties
    }

    public partial class dcREJECT_PLATE_BREAKING_MST
    {
        private List<dcREJECT_PLATE_BREAKING_DTL> m_REJECT_PLATE_BREAKING_dtl_List = null;

        public List<dcREJECT_PLATE_BREAKING_DTL> REJECT_PLATE_BREAKING_dtl_List
        {
            get { return m_REJECT_PLATE_BREAKING_dtl_List; }
            set { m_REJECT_PLATE_BREAKING_dtl_List = value; }
        }

        public string department_name { get; set; }
        public string UOM_NAME { get; set; }

    }
}
