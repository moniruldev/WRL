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
    [DBTable(Name = "tblUserRole")]
    [Serializable]
    public partial class dcUserRole : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_UserRoleID = 0;
        private int m_AppID = 0;
        private int m_UserID = 0;
        private int m_RoleID = 0;

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


        [DBColumn(Name = "UserRoleID", Storage = "m_UserRoleID", DbType = "Int NOT NULL IDENTITY", IsIdentity=true, IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true)]
        public int UserRoleID
        {
            get { return this.m_UserRoleID; }
            set
            {
                this.m_UserRoleID = value;
                this.NotifyPropertyChanged("UserRoleID");
            }
        }

        [DBColumn(Name = "AppID", Storage = "m_AppID", DbType = "Int NOT NULL")]
        public int AppID
        {
            get { return this.m_AppID; }
            set
            {
                this.m_AppID = value;
                this.NotifyPropertyChanged("AppID");
            }
        }


        [DBColumn(Name = "UserID", Storage = "m_UserID", DbType = "Int NOT NULL")]
        public int UserID
        {
            get { return this.m_UserID; }
            set
            {
                this.m_UserID = value;
                this.NotifyPropertyChanged("UserID");
            }
        }


        [DBColumn(Name = "RoleID", Storage = "m_RoleID", DbType = "Int NOT NULL")]
        public int RoleID
        {
            get { return this.m_RoleID; }
            set
            {
                this.m_RoleID = value;
                this.NotifyPropertyChanged("RoleID");
            }
        }


        #endregion //properties
    }

    public partial class dcUserRole
    {
          private int m_SL = 0;
          public int  SL
        {
            get { return this.m_SL; }
            set
            {
                this.m_SL = value;
            }
        }

        private string m_ROLENAME = "";
        public string ROLENAME
          {
              get { return this.m_ROLENAME; }
              set { this.m_ROLENAME = value; }
          }

        private string m_ROLEDESC = "";
        public string ROLEDESC
          {
              get { return this.m_ROLEDESC; }
              set { this.m_ROLEDESC = value; }
          }

        private string m_ISACTIVE = "";
        public string ISACTIVE
          {
              get { return this.m_ISACTIVE; }
              set { this.m_ISACTIVE = value; }
          }

        private string m_ISADMIN = "";
        public string ISADMIN
        {
            get { return this.m_ISADMIN; }
            set { this.m_ISADMIN  = value; }
        }


        private string m_USER_NAME = "";
        public string USER_NAME
        {
            get { return this.m_USER_NAME; }
            set { this.m_USER_NAME = value; }
        }


        private string m_FULLNAME = "";
        public string FULLNAME
        {
            get { return this.m_FULLNAME; }
            set { this.m_FULLNAME = value; }
        }


        private string m_DEFAULT_ROLE = "";
        public string DEFAULT_ROLE
        {
            get { return this.m_DEFAULT_ROLE; }
            set { this.m_DEFAULT_ROLE = value; }
        }
    }
   
}
