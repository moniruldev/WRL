using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using PG.Core.DBBase;

namespace PG.DBClass.OrganiztionDC
{

    [DBTable(Name = "tblLocationUser")]
    [Serializable]
    public partial class dcLocationUser : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_LocationUserID = 0;
        private int m_LocationID = 0;
        private int m_UserID = 0;
        private bool m_AllowLogin = false;


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


        [DBColumn(Name = "LocationUserID", Storage = "m_LocationUserID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int LocationUserID
        {
            get { return this.m_LocationUserID; }
            set
            {
                this.m_LocationUserID = value;
                this.NotifyPropertyChanged("LocationUserID");
            }
        }

        [DBColumn(Name = "LocationID", Storage = "m_LocationID", DbType = "Int NULL")]
        public int LocationID
        {
            get { return this.m_LocationID; }
            set
            {
                this.m_LocationID = value;
                this.NotifyPropertyChanged("LocationID");
            }
        }

        [DBColumn(Name = "UserID", Storage = "m_UserID", DbType = "Int NULL")]
        public int UserID
        {
            get { return this.m_UserID; }
            set
            {
                this.m_UserID = value;
                this.NotifyPropertyChanged("UserID");
            }
        }

        [DBColumn(Name = "AllowLogin", Storage = "m_AllowLogin", DbType = "Bit NULL")]
        public bool AllowLogin
        {
            get { return this.m_AllowLogin; }
            set
            {
                this.m_AllowLogin = value;
                this.NotifyPropertyChanged("AllowLogin");
            }
        }

        #endregion //properties
    }
    public partial class dcLocationUser : DBBaseClass, INotifyPropertyChanged
        {

            private string m_LocationCode = string.Empty;
            public string LocationCode
            {
                get { return m_LocationCode; }
                set { this.m_LocationCode = value; }
            }

            private string m_LocationName = string.Empty;
            public string LocationName
            {
                get { return m_LocationName; }
                set { this.m_LocationName = value; }
            }
            public string LocationCodeName
            {
                get
                {
                    return this.m_LocationCode + " - " + m_LocationName;
                }
            }
       
        }
}
