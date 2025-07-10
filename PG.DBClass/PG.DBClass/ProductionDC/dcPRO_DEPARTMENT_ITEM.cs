using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "PRO_DEPARTMENT_ITEM")]
    public partial class dcPRO_DEPARTMENT_ITEM : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_DEPT_ITEM_ID = 0;
        private int m_DEPT_ID = 0;
        private int m_ITEM_ID = 0;
        private int m_ENTRY_BY = 0;
        private DateTime? m_ENTRY_DATE = null;
        private int m_UPDATE_BY = 0;
        private DateTime? m_UPDATE_DATE = null;
        private string m_IS_FINISHED = string.Empty;
        private int m_ORDER_NO = 0;
        private string m_IS_MIXTURE = string.Empty;
        private string m_IS_BY_PRODUCT = string.Empty;
        private int m_STLM_ID = 0;

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


        [DBColumn(Name = "DEPT_ITEM_ID", Storage = "m_DEPT_ITEM_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int DEPT_ITEM_ID
        {
            get { return this.m_DEPT_ITEM_ID; }
            set
            {
                this.m_DEPT_ITEM_ID = value;
                this.NotifyPropertyChanged("DEPT_ITEM_ID");
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

        [DBColumn(Name = "UPDATE_BY", Storage = "m_UPDATE_BY", DbType = "107")]
        public int UPDATE_BY
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

        [DBColumn(Name = "IS_FINISHED", Storage = "m_IS_FINISHED", DbType = "119")]
        public string IS_FINISHED
        {
            get { return this.m_IS_FINISHED; }
            set
            {
                this.m_IS_FINISHED = value;
                this.NotifyPropertyChanged("IS_FINISHED");
            }
        }

        [DBColumn(Name = "ORDER_NO", Storage = "m_ORDER_NO", DbType = "107")]
        public int ORDER_NO
        {
            get { return this.m_ORDER_NO; }
            set
            {
                this.m_ORDER_NO = value;
                this.NotifyPropertyChanged("ORDER_NO");
            }
        }

        [DBColumn(Name = "IS_MIXTURE", Storage = "m_IS_MIXTURE", DbType = "126")]
        public string IS_MIXTURE
        {
            get { return this.m_IS_MIXTURE; }
            set
            {
                this.m_IS_MIXTURE = value;
                this.NotifyPropertyChanged("IS_MIXTURE");
            }
        }

        [DBColumn(Name = "IS_BY_PRODUCT", Storage = "m_IS_BY_PRODUCT", DbType = "126")]
        public string IS_BY_PRODUCT
        {
            get { return this.m_IS_BY_PRODUCT; }
            set
            {
                this.m_IS_BY_PRODUCT = value;
                this.NotifyPropertyChanged("IS_BY_PRODUCT");
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

  

        #endregion //properties
    }
    public partial class dcPRO_DEPARTMENT_ITEM
    {
        public string ITEM_NAME { set; get; }
        public string IS_BATCH { set; get; }
        public int SLNO { set; get; }
        private List<dcPRO_DEPARTMENT_ITEM> m_ItemDetList = null;
        public List<dcPRO_DEPARTMENT_ITEM> ItemDetList
        {
            get { return m_ItemDetList; }
            set { m_ItemDetList = value; }
        }

    }
}
