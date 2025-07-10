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
    [DBTable(Name = "tblAppObjectType")]
    public class dcAppObjectType : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_AppObjectTypeID = 0;
        private string m_AppObjectTypeCode = string.Empty;
        private string m_AppObjectTypeName = string.Empty;

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


        [DBColumn(Name = "AppObjectTypeID", Storage = "m_AppObjectTypeID", DbType = "Int NOT NULL", IsPrimaryKey = true)]
        public int AppObjectTypeID
        {
            get { return this.m_AppObjectTypeID; }
            set
            {
                this.m_AppObjectTypeID = value;
                this.NotifyPropertyChanged("AppObjectTypeID");
            }
        }

        [DBColumn(Name = "AppObjectTypeCode", Storage = "m_AppObjectTypeCode", DbType = "VarChar(50) NULL")]
        public string AppObjectTypeCode
        {
            get { return this.m_AppObjectTypeCode; }
            set
            {
                this.m_AppObjectTypeCode = value;
                this.NotifyPropertyChanged("AppObjectTypeCode");
            }
        }

        [DBColumn(Name = "AppObjectTypeName", Storage = "m_AppObjectTypeName", DbType = "VarChar(50) NULL")]
        public string AppObjectTypeName
        {
            get { return this.m_AppObjectTypeName; }
            set
            {
                this.m_AppObjectTypeName = value;
                this.NotifyPropertyChanged("AppObjectTypeName");
            }
        }

        #endregion //properties 
    }
}
