using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "INV_ITEM_GROUP")]
    public partial class dcINV_ITEM_GROUP : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_ITEM_GROUP_ID = 0;
        private string m_ITEM_GROUP_CODE = string.Empty;
        private string m_ITEM_GROUP_NAME = string.Empty;
        private string m_ITEM_GROUP_DESC = string.Empty;
        private int m_ITEM_GROUP_ID_PARENT = 0;
        private int m_ITEM_GROUP_LEVEL = 0;
        private int m_ITEM_GROUP_SLNO = 0;
        private bool m_IS_ACTIVE = true;
        private bool m_IS_VISIBLE = true;
        private int m_ENTRY_BY = 0;
        private DateTime? m_ENTRY_DATE = null;

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


        [DBColumn(Name = "ITEM_GROUP_ID", Storage = "m_ITEM_GROUP_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int ITEM_GROUP_ID
        {
            get { return this.m_ITEM_GROUP_ID; }
            set
            {
                this.m_ITEM_GROUP_ID = value;
                this.NotifyPropertyChanged("ITEM_GROUP_ID");
            }
        }

        [DBColumn(Name = "ITEM_GROUP_CODE", Storage = "m_ITEM_GROUP_CODE", DbType = "126")]
        public string ITEM_GROUP_CODE
        {
            get { return this.m_ITEM_GROUP_CODE; }
            set
            {
                this.m_ITEM_GROUP_CODE = value;
                this.NotifyPropertyChanged("ITEM_GROUP_CODE");
            }
        }

        [DBColumn(Name = "ITEM_GROUP_NAME", Storage = "m_ITEM_GROUP_NAME", DbType = "126")]
        public string ITEM_GROUP_NAME
        {
            get { return this.m_ITEM_GROUP_NAME; }
            set
            {
                this.m_ITEM_GROUP_NAME = value;
                this.NotifyPropertyChanged("ITEM_GROUP_NAME");
            }
        }

        [DBColumn(Name = "ITEM_GROUP_DESC", Storage = "m_ITEM_GROUP_DESC", DbType = "126")]
        public string ITEM_GROUP_DESC
        {
            get { return this.m_ITEM_GROUP_DESC; }
            set
            {
                this.m_ITEM_GROUP_DESC = value;
                this.NotifyPropertyChanged("ITEM_GROUP_DESC");
            }
        }

        [DBColumn(Name = "ITEM_GROUP_ID_PARENT", Storage = "m_ITEM_GROUP_ID_PARENT", DbType = "107")]
        public int ITEM_GROUP_ID_PARENT
        {
            get { return this.m_ITEM_GROUP_ID_PARENT; }
            set
            {
                this.m_ITEM_GROUP_ID_PARENT = value;
                this.NotifyPropertyChanged("ITEM_GROUP_ID_PARENT");
            }
        }


        [DBColumn(Name = "ITEM_GROUP_LEVEL", Storage = "m_ITEM_GROUP_LEVEL", DbType = "107")]
        public int ITEM_GROUP_LEVEL
        {
            get { return this.m_ITEM_GROUP_LEVEL; }
            set
            {
                this.m_ITEM_GROUP_LEVEL = value;
                this.NotifyPropertyChanged("ITEM_GROUP_LEVEL");
            }
        }


        [DBColumn(Name = "ITEM_GROUP_SLNO", Storage = "m_ITEM_GROUP_SLNO", DbType = "107")]
        public int ITEM_GROUP_SLNO
        {
            get { return this.m_ITEM_GROUP_SLNO; }
            set
            {
                this.m_ITEM_GROUP_SLNO = value;
                this.NotifyPropertyChanged("ITEM_GROUP_SLNO");
            }
        }


        [DBColumn(Name = "IS_ACTIVE", Storage = "m_IS_ACTIVE", DbType = "126")]
        public bool IS_ACTIVE
        {
            get { return this.m_IS_ACTIVE; }
            set
            {
                this.m_IS_ACTIVE = value;
                this.NotifyPropertyChanged("IS_ACTIVE");
            }
        }

        [DBColumn(Name = "IS_VISIBLE", Storage = "m_IS_VISIBLE", DbType = "126")]
        public bool IS_VISIBLE
        {
            get { return this.m_IS_VISIBLE; }
            set
            {
                this.m_IS_VISIBLE = value;
                this.NotifyPropertyChanged("IS_VISIBLE");
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

        #endregion //properties
    }


    public partial class dcINV_ITEM_GROUP
    {

        private int m_ITEM_GROUP_LEVEL_CURRENT = 0;
        public int ITEM_GROUP_LEVEL_CURRENT
        {

            get { return m_ITEM_GROUP_LEVEL_CURRENT; }
            set { this.m_ITEM_GROUP_LEVEL_CURRENT = value; }
        }

        private bool m_HasParent = false;
        public bool HasParent
        {

            get { return m_HasParent; }
            set { this.m_HasParent = value; }
        }



        private string m_ITEM_GROUP_NAME_PARENT = string.Empty;
        public string ITEM_GROUP_NAME_PARENT
        {
            //get { return AppObjectType == null ? m_AppObjectTypeName : this.AppObjectType.AppObjectTypeName; }
            get { return m_ITEM_GROUP_NAME_PARENT; }
            set { this.m_ITEM_GROUP_NAME_PARENT = value; }
        }

        private string m_ITEM_GROUP_CODE_PARENT = string.Empty;
        public string ITEM_GROUP_CODE_PARENT
        {
            get { return m_ITEM_GROUP_CODE_PARENT; }
            set { this.m_ITEM_GROUP_CODE_PARENT = value; }
        }

        private string m_ITEM_GROUP_NAME_INDENT = string.Empty;
        public string ITEM_GROUP_NAME_INDENT
        {
            //get { return AppObjectType == null ? m_AppObjectTypeName : this.AppObjectType.AppObjectTypeName; }
            get { return m_ITEM_GROUP_NAME_INDENT; }
            set { this.m_ITEM_GROUP_NAME_INDENT = value; }
        }

        private int m_CHILD_GROUP_COUNT = 0;
        public int CHILD_GROUP_COUNT
        {
            get { return m_CHILD_GROUP_COUNT; }
            set { m_CHILD_GROUP_COUNT = value; }
        }


        private int m_CHILD_ITEM_COUNT = 0;
        public int CHILD_ITEM_COUNT
        {
            get { return m_CHILD_ITEM_COUNT; }
            set { m_CHILD_ITEM_COUNT = value; }
        }

        public string  ITEM_GROUP_PARENT_KEY
        {
            get
            {
                string pKey = string.Empty;
                pKey = "grpid" + m_ITEM_GROUP_ID_PARENT.ToString();
                return pKey;
            }
        }
        public string ITEM_CODE { set; get; }
    }


}
