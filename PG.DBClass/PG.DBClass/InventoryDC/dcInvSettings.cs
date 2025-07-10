using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "tblInvSettings")]
    public partial class dcInvSettings : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_AppID = 0;
        private bool m_AllowItemInParentGroup = false;

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


        [DBColumn(Name = "AppID", Storage = "m_AppID", DbType = "Int NULL")]
        public int AppID
        {
            get { return this.m_AppID; }
            set
            {
                this.m_AppID = value;
                this.NotifyPropertyChanged("AppID");
            }
        }

        [DBColumn(Name = "AllowItemInParentGroup", Storage = "m_AllowItemInParentGroup", DbType = "Bit NULL")]
        public bool AllowItemInParentGroup
        {
            get { return this.m_AllowItemInParentGroup; }
            set
            {
                this.m_AllowItemInParentGroup = value;
                this.NotifyPropertyChanged("AllowItemInParentGroup");
            }
        }

        #endregion //properties
    }
}
