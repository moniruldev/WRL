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
    [DBTable(Name = "tblRole")]
    public class dcRole : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_RoleID = 0;
        private int m_AppID = 0;
        private string m_RoleName = string.Empty;
        private string m_RoleDesc = string.Empty;
        private bool m_IsSystem = false;
        private bool m_IsActive = false;
        private bool m_IsVisible = false;
        private DateTime? m_RoleCreateDt = DateTime.Now;
        private bool m_IsAdmin = false;

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


        [DBColumn(Name = "RoleID", Storage = "m_RoleID", DbType = "Int NOT NULL IDENTITY", SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int RoleID
        {
            get { return this.m_RoleID; }
            set
            {
                this.m_RoleID = value;
                this.NotifyPropertyChanged("RoleID");
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

        [DBColumn(Name = "RoleName", Storage = "m_RoleName", DbType = "VarChar(50) NOT NULL")]
        public string RoleName
        {
            get { return this.m_RoleName; }
            set
            {
                this.m_RoleName = value;
                this.NotifyPropertyChanged("RoleName");
            }
        }

        [DBColumn(Name = "RoleDesc", Storage = "m_RoleDesc", DbType = "VarChar(50) NULL")]
        public string RoleDesc
        {
            get { return this.m_RoleDesc; }
            set
            {
                this.m_RoleDesc = value;
                this.NotifyPropertyChanged("RoleDesc");
            }
        }

        [DBColumn(Name = "IsSystem", Storage = "m_IsSystem", DbType = "Bit NOT NULL")]
        public bool IsSystem
        {
            get { return this.m_IsSystem; }
            set
            {
                this.m_IsSystem = value;
                this.NotifyPropertyChanged("IsSystem");
            }
        }

        [DBColumn(Name = "IsActive", Storage = "m_IsActive", DbType = "Bit NOT NULL")]
        public bool IsActive
        {
            get { return this.m_IsActive; }
            set
            {
                this.m_IsActive = value;
                this.NotifyPropertyChanged("IsActive");
            }
        }

        [DBColumn(Name = "IsVisible", Storage = "m_IsVisible", DbType = "Bit NOT NULL")]
        public bool IsVisible
        {
            get { return this.m_IsVisible; }
            set
            {
                this.m_IsVisible = value;
                this.NotifyPropertyChanged("IsVisible");
            }
        }

        [DBColumn(Name = "RoleCreateDt", Storage = "m_RoleCreateDt", DbType = "SmallDateTime NULL")]
        public DateTime? RoleCreateDt
        {
            get { return this.m_RoleCreateDt; }
            set
            {
                this.m_RoleCreateDt = value;
                this.NotifyPropertyChanged("RoleCreateDt");
            }
        }



        [DBColumn(Name = "IsAdmin", Storage = "m_IsAdmin", DbType = "Bit NOT NULL")]
        public bool IsAdmin
        {
            get { return this.m_IsAdmin; }
            set
            {
                this.m_IsAdmin = value;
                this.NotifyPropertyChanged("IsAdmin");
            }
        }

        #endregion //properties
    }
}
