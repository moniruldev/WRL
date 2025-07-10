using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "DEPARTMENT_ITEM")]
    public partial class dcDEPARTMENT_ITEM : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_DEPT_ITEM_ID = 0;
        private string m_DEPT_ID = string.Empty;
        private int m_ITEM_ID = 0;
        private string m_ENTRY_BY = string.Empty;
        private DateTime? m_ENTRY_DATE = null;
        private string m_UPDATE_BY = string.Empty;
        private DateTime? m_UPDATE_DATE = null;

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


        [DBColumn(Name = "DEPT_ITEM_ID", Storage = "m_DEPT_ITEM_ID", DbType = "107", IsPrimaryKey = true)]
        public int DEPT_ITEM_ID
        {
            get { return this.m_DEPT_ITEM_ID; }
            set
            {
                this.m_DEPT_ITEM_ID = value;
                this.NotifyPropertyChanged("DEPT_ITEM_ID");
            }
        }

        [DBColumn(Name = "DEPT_ID", Storage = "m_DEPT_ID", DbType = "126")]
        public string DEPT_ID
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

        [DBColumn(Name = "ENTRY_BY", Storage = "m_ENTRY_BY", DbType = "126")]
        public string ENTRY_BY
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

        #endregion //properties
    }

    public partial class dcDEPARTMENT_ITEM
    {
       
        private string m_ITEM_GROUP_NAME = string.Empty;
        public string ITEM_GROUP_NAME
        {

            get { return m_ITEM_GROUP_NAME; }
            set { this.m_ITEM_GROUP_NAME = value; }
        }

        private string m_ITEM_GROUP_CODE = string.Empty;
        public string ITEM_GROUP_CODE
        {

            get { return m_ITEM_GROUP_CODE; }
            set { this.m_ITEM_GROUP_CODE = value; }
        }
        
        private string m_ITEM_CLASS_NAME = string.Empty;
        public string ITEM_CLASS_NAME
        {

            get { return m_ITEM_CLASS_NAME; }
            set { this.m_ITEM_CLASS_NAME = value; }
        }

        private string m_ITEM_CLASS_CODE = string.Empty;
        public string ITEM_CLASS_CODE
        {

            get { return m_ITEM_CLASS_CODE; }
            set { this.m_ITEM_CLASS_CODE = value; }
        }


        private string m_ITEM_TYPE_NAME = string.Empty;
        public string ITEM_TYPE_NAME
        {

            get { return m_ITEM_TYPE_NAME; }
            set { this.m_ITEM_TYPE_NAME = value; }
        }

        private string m_ITEM_TYPE_CODE = string.Empty;
        public string ITEM_TYPE_CODE
        {

            get { return m_ITEM_TYPE_CODE; }
            set { this.m_ITEM_TYPE_CODE = value; }
        }

        private string m_UOM_NAME = string.Empty;
        public string UOM_NAME
        {

            get { return m_UOM_NAME; }
            set { this.m_UOM_NAME = value; }
        }
       
    }
}
