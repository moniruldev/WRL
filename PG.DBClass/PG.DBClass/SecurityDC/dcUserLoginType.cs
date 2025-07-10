using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using PG.Core.DBBase;

namespace PG.DBClass.SecurityDC
{
     [Serializable]
    [DBTable(Name = "tblUserLoginType")]
    public partial class dcUserLoginType : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_UserLoginTypeID = 0;
        private string m_UserLoginTypeName = string.Empty;

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


        [DBColumn(Name = "UserLoginTypeID", Storage = "m_UserLoginTypeID", DbType = "Int NOT NULL", IsPrimaryKey = true)]
        public int UserLoginTypeID
        {
            get { return this.m_UserLoginTypeID; }
            set
            {
                this.m_UserLoginTypeID = value;
                this.NotifyPropertyChanged("UserLoginTypeID");
            }
        }

        [DBColumn(Name = "UserLoginTypeName", Storage = "m_UserLoginTypeName", DbType = "NVarChar(50) NULL")]
        public string UserLoginTypeName
        {
            get { return this.m_UserLoginTypeName; }
            set
            {
                this.m_UserLoginTypeName = value;
                this.NotifyPropertyChanged("UserLoginTypeName");
            }
        }

        #endregion //properties
    }
}
