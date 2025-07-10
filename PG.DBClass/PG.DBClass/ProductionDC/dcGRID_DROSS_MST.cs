using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "GRID_DROSS_MST")]
    public partial class dcGRID_DROSS_MST : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private Int64 m_DROSS_MST_ID = 0;
        private string m_DROSS_NO = string.Empty;
        private DateTime? m_RECEIVE_DATE = null;
        private int m_DEPT_ID = 0;
        private Int64 m_ITEM_ID = 0;
        private string m_REMARKS = string.Empty;
        private int m_CREATE_BY = 0;
        private DateTime? m_CREATE_DATE = null;
        private int? m_UPDATE_BY = 0;
        private DateTime? m_UPDATE_DATE = null;
        private string m_AUTHO_STATUS = string.Empty;
        private DateTime? m_AUTHO_DATE = null;
        private string m_REC_STATUS = string.Empty;
        private string m_AUTHO_BY = string.Empty;
        private int m_PROD_MST_ID = 0;

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

       

        [DBColumn(Name = "DROSS_MST_ID", Storage = "m_DROSS_MST_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public Int64 DROSS_MST_ID
        {
            get { return this.m_DROSS_MST_ID; }
            set
            {
                this.m_DROSS_MST_ID = value;
                this.NotifyPropertyChanged("DROSS_MST_ID");
            }
        }

        [DBColumn(Name = "DROSS_NO", Storage = "m_DROSS_NO", DbType = "126")]
        public string DROSS_NO
        {
            get { return this.m_DROSS_NO; }
            set
            {
                this.m_DROSS_NO = value;
                this.NotifyPropertyChanged("DROSS_NO");
            }
        }

        [DBColumn(Name = "RECEIVE_DATE", Storage = "m_RECEIVE_DATE", DbType = "106")]
        public DateTime? RECEIVE_DATE
        {
            get { return this.m_RECEIVE_DATE; }
            set
            {
                this.m_RECEIVE_DATE = value;
                this.NotifyPropertyChanged("RECEIVE_DATE");
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

        //[DBColumn(Name = "ITEM_ID", Storage = "m_ITEM_ID", DbType = "107")]
        //public Int64 ITEM_ID
        //{
        //    get { return this.m_ITEM_ID; }
        //    set
        //    {
        //        this.m_ITEM_ID = value;
        //        this.NotifyPropertyChanged("ITEM_ID");
        //    }
        //}

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

        [DBColumn(Name = "CREATE_BY", Storage = "m_CREATE_BY", DbType = "107")]
        public int CREATE_BY
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

        [DBColumn(Name = "UPDATE_BY", Storage = "m_UPDATE_BY", DbType = "107")]
        public int? UPDATE_BY
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

        [DBColumn(Name = "REC_STATUS", Storage = "m_REC_STATUS", DbType = "126")]
        public string REC_STATUS
        {
            get { return this.m_REC_STATUS; }
            set
            {
                this.m_REC_STATUS = value;
                this.NotifyPropertyChanged("REC_STATUS");
            }
        }

        [DBColumn(Name = "AUTHO_BY", Storage = "m_AUTHO_BY", DbType = "126")]
        public string AUTHO_BY
        {
            get { return this.m_AUTHO_BY; }
            set
            {
                this.m_AUTHO_BY = value;
                this.NotifyPropertyChanged("AUTHO_BY");
            }
        }

        [DBColumn(Name = "PROD_MST_ID", Storage = "m_PROD_MST_ID", DbType = "107")]
        public int PROD_MST_ID
        {
            get { return this.m_PROD_MST_ID; }
            set
            {
                this.m_PROD_MST_ID = value;
                this.NotifyPropertyChanged("PROD_MST_ID");
            }
        }

        #endregion //properties
    }
    public partial class dcGRID_DROSS_MST
    {

        private int m_UOM_ID = 0;
        public int UOM_ID
        {
            get { return m_UOM_ID; }
            set { m_UOM_ID = value; }
        }

        private string m_UOM_NAME = string.Empty;
        public string UOM_NAME
        {
            get { return m_UOM_NAME; }
            set { m_UOM_NAME = value; }
        }

        private string m_DEPARTMENT_NAME = string.Empty;
        public string DEPARTMENT_NAME
        {
            get { return m_DEPARTMENT_NAME; }
            set { m_DEPARTMENT_NAME = value; }
        }

        private string m_CREATE_BY_NAME = string.Empty;
        public string CREATE_BY_NAME
        {
            get { return m_CREATE_BY_NAME; }
            set { m_CREATE_BY_NAME = value; }
        }

        private List<dcGRID_DROSS_DTL> m_GRID_DROSS_DTL_List = null;
        public List<dcGRID_DROSS_DTL> GRID_DROSS_DTL_List
        {
            get { return m_GRID_DROSS_DTL_List; }
            set { m_GRID_DROSS_DTL_List = value; }
        }

    }
}
