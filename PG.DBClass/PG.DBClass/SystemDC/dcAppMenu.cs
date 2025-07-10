using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using PG.Core.DBBase;


namespace PG.DBClass.SystemDC
{
    [Serializable]
    //[DBTable(Name = "Systems.tblAppMenu")]
    [DBTable(Name = "TBLAPPMENU")]
    public partial class dcAppMenu : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_AppID = 0;
        private int m_AppMenuID = 0;
        private int m_AppMenuType = 0;
        private string m_AppMenuName = string.Empty;
        private string m_AppMenuText = string.Empty;
        private string m_AppMenuPath = string.Empty;
        private string m_ToolTip = string.Empty;
        private bool m_ShowImage = false;
        private string m_ImageURL = string.Empty;
        private bool m_IsAppURL = false;
        private string m_AppMenuURL = string.Empty;
        private string m_AppMenuDesc = string.Empty;
        private string m_AppHeaderText = string.Empty;
        private bool m_SetAppHeader = false;
        private int m_AppMenuSLNo = 0;
        private int m_SelectAction = 0;
        private int m_TabAction = 0;
        private bool m_Expanded = false;
        private bool m_ShowMenu = false;
        private bool m_IsRoleMenu = false;
        private int m_ParentMenuID = 0;

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

        [DBColumn(Name = "AppMenuID", Storage = "m_AppMenuID", DbType = "Int NOT NULL", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int AppMenuID
        {
            get { return this.m_AppMenuID; }
            set
            {
                this.m_AppMenuID = value;
                this.NotifyPropertyChanged("AppMenuID");
            }
        }

        [DBColumn(Name = "AppMenuType", Storage = "m_AppMenuType", DbType = "Int NULL")]
        public int AppMenuType
        {
            get { return this.m_AppMenuType; }
            set
            {
                this.m_AppMenuType = value;
                this.NotifyPropertyChanged("AppMenuType");
            }
        }

        [DBColumn(Name = "AppMenuName", Storage = "m_AppMenuName", DbType = "VarChar(255) NULL")]
        public string AppMenuName
        {
            get { return this.m_AppMenuName; }
            set
            {
                this.m_AppMenuName = value;
                this.NotifyPropertyChanged("AppMenuName");
            }
        }

        [DBColumn(Name = "AppMenuText", Storage = "m_AppMenuText", DbType = "VarChar(255) NULL")]
        public string AppMenuText
        {
            get { return this.m_AppMenuText; }
            set
            {
                this.m_AppMenuText = value;
                this.NotifyPropertyChanged("AppMenuText");
            }
        }

        [DBColumn(Name = "AppMenuPath", Storage = "m_AppMenuPath", DbType = "VarChar(255) NULL")]
        public string AppMenuPath
        {
            get { return this.m_AppMenuPath; }
            set
            {
                this.m_AppMenuPath = value;
                this.NotifyPropertyChanged("AppMenuPath");
            }
        }

        [DBColumn(Name = "ToolTip", Storage = "m_ToolTip", DbType = "VarChar(255) NULL")]
        public string ToolTip
        {
            get { return this.m_ToolTip; }
            set
            {
                this.m_ToolTip = value;
                this.NotifyPropertyChanged("ToolTip");
            }
        }

        [DBColumn(Name = "ShowImage", Storage = "m_ShowImage", DbType = "Bit NULL")]
        public bool ShowImage
        {
            get { return this.m_ShowImage; }
            set
            {
                this.m_ShowImage = value;
                this.NotifyPropertyChanged("ShowImage");
            }
        }

        [DBColumn(Name = "ImageURL", Storage = "m_ImageURL", DbType = "VarChar(50) NULL")]
        public string ImageURL
        {
            get { return this.m_ImageURL; }
            set
            {
                this.m_ImageURL = value;
                this.NotifyPropertyChanged("ImageURL");
            }
        }



        [DBColumn(Name = "IsAppURL", Storage = "m_IsAppURL", DbType = "Bit NOT NULL")]
        public bool IsAppURL
        {
            get { return this.m_IsAppURL; }
            set
            {
                this.m_IsAppURL = value;
                this.NotifyPropertyChanged("IsAppURL");
            }
        }

        [DBColumn(Name = "AppMenuURL", Storage = "m_AppMenuURL", DbType = "VarChar(255) NULL")]
        public string AppMenuURL
        {
            get { return this.m_AppMenuURL; }
            set
            {
                this.m_AppMenuURL = value;
                this.NotifyPropertyChanged("AppMenuURL");
            }
        }

        [DBColumn(Name = "AppMenuDesc", Storage = "m_AppMenuDesc", DbType = "VarChar(255) NULL")]
        public string AppMenuDesc
        {
            get { return this.m_AppMenuDesc; }
            set
            {
                this.m_AppMenuDesc = value;
                this.NotifyPropertyChanged("AppMenuDesc");
            }
        }

        [DBColumn(Name = "AppHeaderText", Storage = "m_AppHeaderText", DbType = "VarChar(255) NULL")]
        public string AppHeaderText
        {
            get { return this.m_AppHeaderText; }
            set
            {
                this.m_AppHeaderText = value;
                this.NotifyPropertyChanged("AppHeaderText");
            }
        }

        [DBColumn(Name = "SetAppHeader", Storage = "m_SetAppHeader", DbType = "Bit NOT NULL")]
        public bool SetAppHeader
        {
            get { return this.m_SetAppHeader; }
            set
            {
                this.m_SetAppHeader = value;
                this.NotifyPropertyChanged("SetAppHeader");
            }
        }

        [DBColumn(Name = "AppMenuSLNo", Storage = "m_AppMenuSLNo", DbType = "Int NULL")]
        public int AppMenuSLNo
        {
            get { return this.m_AppMenuSLNo; }
            set
            {
                this.m_AppMenuSLNo = value;
                this.NotifyPropertyChanged("AppMenuSLNo");
            }
        }

        [DBColumn(Name = "SelectAction", Storage = "m_SelectAction", DbType = "Int NULL")]
        public int SelectAction
        {
            get { return this.m_SelectAction; }
            set
            {
                this.m_SelectAction = value;
                this.NotifyPropertyChanged("SelectAction");
            }
        }

        [DBColumn(Name = "TabAction", Storage = "m_TabAction", DbType = "Int NULL")]
        public int TabAction
        {
            get { return this.m_TabAction; }
            set
            {
                this.m_TabAction = value;
                this.NotifyPropertyChanged("TabAction");
            }
        }

        [DBColumn(Name = "Expanded", Storage = "m_Expanded", DbType = "Bit NOT NULL")]
        public bool Expanded
        {
            get { return this.m_Expanded; }
            set
            {
                this.m_Expanded = value;
                this.NotifyPropertyChanged("Expanded");
            }
        }

        [DBColumn(Name = "ShowMenu", Storage = "m_ShowMenu", DbType = "Bit NULL")]
        public bool ShowMenu
        {
            get { return this.m_ShowMenu; }
            set
            {
                this.m_ShowMenu = value;
                this.NotifyPropertyChanged("ShowMenu");
            }
        }

        [DBColumn(Name = "IsRoleMenu", Storage = "m_IsRoleMenu", DbType = "Bit NULL")]
        public bool IsRoleMenu 
        {
            get { return this.m_IsRoleMenu; }
            set
            {
                this.m_IsRoleMenu = value;
                this.NotifyPropertyChanged("IsRoleMenu");
            }
        }

        [DBColumn(Name = "ParentMenuID", Storage = "m_ParentMenuID", DbType = "Int NULL")]
        public int ParentMenuID
        {
            get { return this.m_ParentMenuID; }
            set
            {
                this.m_ParentMenuID = value;
                this.NotifyPropertyChanged("ParentMenuID");
            }
        }




        #endregion //properties
    }

    public partial class dcAppMenu
    {
        private string m_AppMenuNameParent = string.Empty;
        public string AppMenuNameParent
        {
            get { return m_AppMenuNameParent; }
            set { m_AppMenuNameParent = value; }
        }

        //private string m_ChildName = string.Empty;
        //public string ChildName
        //{
        //    get { return m_ChildName; }
        //    set { m_ChildName = value; }
        //}
    }
}
