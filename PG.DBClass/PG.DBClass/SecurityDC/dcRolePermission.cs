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
    [DBTable(Name = "tblRolePermission")]
    public partial class dcRolePermission : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_RolePermissionID = 0;
        private int m_AppObjectID = 0;
        private int m_RoleID = 0;
        private int m_Permission = 0;

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


        [DBColumn(Name = "RolePermissionID", Storage = "m_RolePermissionID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true)]
        public int RolePermissionID
        {
            get { return this.m_RolePermissionID; }
            set
            {
                this.m_RolePermissionID = value;
                this.NotifyPropertyChanged("RolePermissionID");
            }
        }

        [DBColumn(Name = "AppObjectID", Storage = "m_AppObjectID", DbType = "Int NULL")]
        public int AppObjectID
        {
            get { return this.m_AppObjectID; }
            set
            {
                this.m_AppObjectID = value;
                this.NotifyPropertyChanged("AppObjectID");
            }
        }

        [DBColumn(Name = "RoleID", Storage = "m_RoleID", DbType = "Int NULL")]
        public int RoleID
        {
            get { return this.m_RoleID; }
            set
            {
                this.m_RoleID = value;
                this.NotifyPropertyChanged("RoleID");
            }
        }

        [DBColumn(Name = "Permission", Storage = "m_Permission", DbType = "Int NULL")]
        public int Permission
        {
            get { return this.m_Permission; }
            set
            {
                this.m_Permission = value;
                this.NotifyPropertyChanged("Permission");
            }
        }

        #endregion //properties

        #region Association
        //private EntityRef<dcRole> m_Role = new EntityRef<dcRole>();
        //[Association(Name = "FK_RolePermisison_Roles", Storage = "m_Role", ThisKey = "RoleID", OtherKey = "RoleID", IsForeignKey = true)]
        //public dcRole Role
        //{
        //    get { return m_Role.Entity; }
        //    set { m_Role.Entity = value; }
        //}
        //private EntityRef<dcAppObject> m_AppObjects = new EntityRef<dcAppObject>();
        //[Association(Name = "FK_RolePermisison_AppObjects", Storage = "m_AppObjects", ThisKey = "AppObjectID", OtherKey = "AppObjectID", IsForeignKey = true)]
        //public dcAppObject AppObjects
        //{
        //    get { return m_AppObjects.Entity; }
        //    set { m_AppObjects.Entity = value; }
        //}


        #endregion

    }

    public partial class dcRolePermission : DBBaseClass, INotifyPropertyChanged
    {
        private string m_RoleName = string.Empty;
        public string RoleName
        {
            get { return m_RoleName ; }
            set { this.m_RoleName = value; }
        }
        private string m_AppObjectCode = string.Empty;
        public string AppObjectCode
        {
            get { return m_AppObjectCode; }
            set { this.m_AppObjectCode = value; }
        }

        private string m_AppObjectName = string.Empty;
        public string AppObjectName
        {
            get { return m_AppObjectName; }
            set { this.m_AppObjectName = value; }
        }


        private int m_AppObjectTypeID = 0;
        public int AppObjectTypeID
        {
            get
            {
                return m_AppObjectTypeID;
             }
            set { this.m_AppObjectTypeID = value; }
        }


        private string m_AppObjectTypeCode = string.Empty;
        public string AppObjectTypeCode
        {
            get
            {
                return m_AppObjectTypeCode;
             }
            set { this.m_AppObjectTypeCode = value; }
        }

        private string m_AppObjectTypeName = string.Empty;
        public string AppObjectTypeName
        {
            get
            {
                return m_AppObjectTypeName;
                //return AppObjects == null ? m_ObjTypeName : AppObjects.AppObjType == null ? m_ObjTypeName : this.AppObjects.AppObjType.ObjTypeName; 
            }
            set { this.m_AppObjectTypeName = value; }
        }
    }
}
