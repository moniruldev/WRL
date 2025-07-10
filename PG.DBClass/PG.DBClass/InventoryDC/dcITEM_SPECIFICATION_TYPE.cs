using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
    [DBTable(Name = "ITEM_SPECIFICATION_TYPE")]
    public partial class dcITEM_SPECIFICATION_TYPE : DBBaseClass, INotifyPropertyChanged
    {
        #region private members
        private int m_ITEM_SPECIFICATION_ID = 0;
        private string m_SPECIFICATION_TYPE = string.Empty;
        private string m_SPECIFICATION_DESC = string.Empty;
        private int m_ISSUE_TYPE_ID = 0;
        private int m_RCV_TYPE_ID = 0;
        #endregion

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

        [DBColumn(Name = "ITEM_SPECIFICATION_ID", Storage = "m_ITEM_SPECIFICATION_ID", DbType = "107", IsPrimaryKey = true)]
        public int ITEM_SPECIFICATION_ID
        {
            get { return this.m_ITEM_SPECIFICATION_ID; }
            set
            {
                this.m_ITEM_SPECIFICATION_ID = value;
                this.NotifyPropertyChanged("ITEM_SPECIFICATION_ID");
            }
        }

        [DBColumn(Name = "SPECIFICATION_TYPE", Storage = "m_SPECIFICATION_TYPE", DbType = "126")]
        public string SPECIFICATION_TYPE
        {
            get { return this.m_SPECIFICATION_TYPE; }
            set
            {
                this.m_SPECIFICATION_TYPE = value;
                this.NotifyPropertyChanged("SPECIFICATION_TYPE");
            }
        }

        [DBColumn(Name = "SPECIFICATION_DESC", Storage = "m_SPECIFICATION_DESC", DbType = "126")]
        public string SPECIFICATION_DESC
        {
            get { return this.m_SPECIFICATION_DESC; }
            set
            {
                this.m_SPECIFICATION_DESC = value;
                this.NotifyPropertyChanged("SPECIFICATION_DESC");
            }
        }

        [DBColumn(Name = "ISSUE_TYPE_ID", Storage = "m_ISSUE_TYPE_ID", DbType = "107")]
        public int ISSUE_TYPE_ID
        {
            get { return this.m_ISSUE_TYPE_ID; }
            set
            {
                this.m_ISSUE_TYPE_ID = value;
                this.NotifyPropertyChanged("ISSUE_TYPE_ID");
            }
        }

        [DBColumn(Name = "RCV_TYPE_ID", Storage = "m_RCV_TYPE_ID", DbType = "107")]
        public int RCV_TYPE_ID
        {
            get { return this.m_RCV_TYPE_ID; }
            set
            {
                this.m_RCV_TYPE_ID = value;
                this.NotifyPropertyChanged("RCV_TYPE_ID");
            }
        }

        #endregion //properties
    }
}
