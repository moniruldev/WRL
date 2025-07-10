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
    [DBTable(Name = "tblAppObject")]
    public class dcAppObject : DBBaseClass, INotifyPropertyChanged
    {
        #region private members


        private int m_AppObjectID = 0;
        private int m_AppID = 0;
        private string m_AppObjectCode = string.Empty;
        private string m_AppObjectName = string.Empty;
        private int m_AppObjectNo = 0;
        private int m_AppObjectTypeID = 0;
        private int m_Permission = 0;
        private bool m_IsSystem = false;
        private bool m_Invisible = false;
        private int m_OpenCount = 0;

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



        [DBColumn(Name = "AppObjectID", Storage = "m_AppObjectID", DbType = "Int NOT NULL", IsPrimaryKey = true)]
        public int AppObjectID
        {
            get { return this.m_AppObjectID; }
            set
            {
                this.m_AppObjectID = value;
                this.NotifyPropertyChanged("AppObjectID");
            }
        }

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

        [DBColumn(Name = "AppObjectCode", Storage = "m_AppObjectCode", DbType = "VarChar(50) NOT NULL")]
        public string AppObjectCode
        {
            get { return this.m_AppObjectCode; }
            set
            {
                this.m_AppObjectCode = value;
                this.NotifyPropertyChanged("AppObjectCode");
            }
        }

        [DBColumn(Name = "AppObjectName", Storage = "m_AppObjectName", DbType = "VarChar(50) NULL")]
        public string AppObjectName
        {
            get { return this.m_AppObjectName; }
            set
            {
                this.m_AppObjectName = value;
                this.NotifyPropertyChanged("AppObjectName");
            }
        }

        [DBColumn(Name = "AppObjectNo", Storage = "m_AppObjectNo", DbType = "Int NOT NULL")]
        public int AppObjectNo
        {
            get { return this.m_AppObjectNo; }
            set
            {
                this.m_AppObjectNo = value;
                this.NotifyPropertyChanged("AppObjectNo");
            }
        }

        [DBColumn(Name = "AppObjectTypeID", Storage = "m_AppObjectTypeID", DbType = "Int NOT NULL")]
        public int AppObjectTypeID
        {
            get { return this.m_AppObjectTypeID; }
            set
            {
                this.m_AppObjectTypeID = value;
                this.NotifyPropertyChanged("AppObjectTypeID");
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

        [DBColumn(Name = "Invisible", Storage = "m_Invisible", DbType = "Bit NOT NULL")]
        public bool Invisible
        {
            get { return this.m_Invisible; }
            set
            {
                this.m_Invisible = value;
                this.NotifyPropertyChanged("Invisible");
            }
        }

        [DBColumn(Name = "OpenCount", Storage = "m_OpenCount", DbType = "Int NULL")]
        public int OpenCount
        {
            get { return this.m_OpenCount; }
            set
            {
                this.m_OpenCount = value;
                this.NotifyPropertyChanged("OpenCount");
            }
        }

        #endregion //properties


        #region properties custom
        private string m_AppObjectTypeName = string.Empty;
        public string AppObjectTypeName
        {
            get { return AppObjectType == null ? m_AppObjectTypeName : this.AppObjectType.AppObjectTypeName; }
            set { this.m_AppObjectTypeName = value; }
        }
        #endregion //properties custom

        #region Association
        private EntityRef<dcAppObjectType> m_AppObjectType = new EntityRef<dcAppObjectType>();
        [Association(Name = "FK_AppObjects_AppObjectType", Storage = "m_AppObjectType", ThisKey = "AppObjectTypeID", OtherKey = "AppObjectTypeID", IsForeignKey = true)]
        public dcAppObjectType AppObjectType
        {
            get { return m_AppObjectType.Entity; }
            set { m_AppObjectType.Entity = value; }
        }
        #endregion
    }
}
