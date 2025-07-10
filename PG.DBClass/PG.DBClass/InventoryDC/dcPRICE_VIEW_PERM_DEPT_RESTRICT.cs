using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
    public class dcPRICE_VIEW_PERM_DEPT_RESTRICT : DBBaseClass, INotifyPropertyChanged
    {

        #region private members

        private int m_DEPT_ID = 0;
        private int m_NOT_VIEWABLE_DEPT_ID = 0;

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

        [DBColumn(Name = "DEPT_ID", Storage = "m_DEPT_ID", DbType = "126")]
        public int DEPT_ID
        {
            get { return this.m_DEPT_ID; }
            set
            {
                this.m_DEPT_ID = value;
                this.NotifyPropertyChanged("DEPT_ID");
            }
        }

        [DBColumn(Name = "NOT_VIEWABLE_DEPT_ID", Storage = "m_NOT_VIEWABLE_DEPT_ID", DbType = "126")]
        public int NOT_VIEWABLE_DEPT_ID
        {
            get { return this.m_NOT_VIEWABLE_DEPT_ID; }
            set
            {
                this.m_NOT_VIEWABLE_DEPT_ID = value;
                this.NotifyPropertyChanged("NOT_VIEWABLE_DEPT_ID");
            }
        }


        #endregion //properties

    }
}
