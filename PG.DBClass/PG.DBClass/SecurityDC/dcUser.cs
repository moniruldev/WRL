using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using PG.Core.DBBase;
using PG.DBClass.OrganiztionDC;

namespace PG.DBClass.SecurityDC
{
    [DBTable(Name = "tblUser")]
    [Serializable]
    public partial class dcUser : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_UserID = 0;
        private int m_AppID = 0;
        private string m_UserName = string.Empty;
        private string m_Password = string.Empty;
        private string m_FullName = string.Empty;
        private string m_Designation = string.Empty;
        private string m_Email = string.Empty;
        private string m_UserType = string.Empty;
        private int m_UserLevel = 0;
        private int m_RoleID = 0;
        private bool m_LogUser = false;
        private bool m_LogLocal = false;
        private int m_UserLoginTypeID = 1;
        private int m_UserIDGlobal = 0;
        private string m_EmpCode = string.Empty;
        private int m_EmpID = 0;
        private bool m_IsActive = false;
        private bool m_IsSystem = false;
        private bool m_IsVisible = false;
        private bool m_MRestrict = false;
        private string m_MName1 = string.Empty;
        private string m_MName2 = string.Empty;
        private string m_MName3 = string.Empty;
        private DateTime? m_UserCreateDt = null;
        private string m_IS_BACK_ENTRY_AUTH = "N";
        private string m_ALL_DEPT = "N";

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


        [DBColumn(Name = "UserID", Storage = "m_UserID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true,IsIdentity=true)]
        public int UserID
        {
            get { return this.m_UserID; }
            set
            {
                this.m_UserID = value;
                this.NotifyPropertyChanged("UserID");
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

        [DBColumn(Name = "UserName", Storage = "m_UserName", DbType = "VarChar(50) NOT NULL")]
        public string UserName
        {
            get { return this.m_UserName; }
            set
            {
                this.m_UserName = value;
                this.NotifyPropertyChanged("UserName");
            }
        }

        [DBColumn(Name = "Password", Storage = "m_Password", DbType = "VarChar(50) NULL")]
        public string Password
        {
            get { return this.m_Password; }
            set
            {
                this.m_Password = value;
                this.NotifyPropertyChanged("Password");
            }
        }

        [DBColumn(Name = "FullName", Storage = "m_FullName", DbType = "VarChar(50) NULL")]
        public string FullName
        {
            get { return this.m_FullName; }
            set
            {
                this.m_FullName = value;
                this.NotifyPropertyChanged("FullName");
            }
        }

        [DBColumn(Name = "Designation", Storage = "m_Designation", DbType = "VarChar(50) NULL")]
        public string Designation
        {
            get { return this.m_Designation; }
            set
            {
                this.m_Designation = value;
                this.NotifyPropertyChanged("Designation");
            }
        }

        [DBColumn(Name = "Email", Storage = "m_Email", DbType = "VarChar(50) NULL")]
        public string Email
        {
            get { return this.m_Email; }
            set
            {
                this.m_Email = value;
                this.NotifyPropertyChanged("Email");
            }
        }

        [DBColumn(Name = "UserType", Storage = "m_UserType", DbType = "VarChar(50) NULL")]
        public string UserType
        {
            get { return this.m_UserType; }
            set
            {
                this.m_UserType = value;
                this.NotifyPropertyChanged("UserType");
            }
        }

        [DBColumn(Name = "UserLevel", Storage = "m_UserLevel", DbType = "Int NULL")]
        public int UserLevel
        {
            get { return this.m_UserLevel; }
            set
            {
                this.m_UserLevel = value;
                this.NotifyPropertyChanged("UserLevel");
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

        [DBColumn(Name = "LogUser", Storage = "m_LogUser", DbType = "Bit NOT NULL")]
        public bool LogUser
        {
            get { return this.m_LogUser; }
            set
            {
                this.m_LogUser = value;
                this.NotifyPropertyChanged("LogUser");
            }
        }

        [DBColumn(Name = "LogLocal", Storage = "m_LogLocal", DbType = "Bit NOT NULL")]
        public bool LogLocal
        {
            get { return this.m_LogLocal; }
            set
            {
                this.m_LogLocal = value;
                this.NotifyPropertyChanged("LogLocal");
            }
        }

        [DBColumn(Name = "UserLoginTypeID", Storage = "m_UserLoginTypeID", DbType = "Int NULL")]
        public int UserLoginTypeID
        {
            get { return this.m_UserLoginTypeID; }
            set
            {
                this.m_UserLoginTypeID = value;
                this.NotifyPropertyChanged("UserLoginTypeID");
            }
        }



        [DBColumn(Name = "UserIDGlobal", Storage = "m_UserIDGlobal", DbType = "Int NULL")]
        public int UserIDGlobal
        {
            get { return this.m_UserIDGlobal; }
            set
            {
                this.m_UserIDGlobal = value;
                this.NotifyPropertyChanged("UserIDGlobal");
            }
        }

        [DBColumn(Name = "EmpCode", Storage = "m_EmpCode", DbType = "NVarChar(50) NULL")]
        public string EmpCode
        {
            get { return this.m_EmpCode; }
            set
            {
                this.m_EmpCode = value;
                this.NotifyPropertyChanged("EmpCode");
            }
        }

        [DBColumn(Name = "EmpID", Storage = "m_EmpID", DbType = "Int NULL")]
        public int EmpID
        {
            get { return this.m_EmpID; }
            set
            {
                this.m_UserLoginTypeID = value;
                this.NotifyPropertyChanged("EmpID");
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

        [DBColumn(Name = "MRestrict", Storage = "m_MRestrict", DbType = "Bit NOT NULL")]
        public bool MRestrict
        {
            get { return this.m_MRestrict; }
            set
            {
                this.m_MRestrict = value;
                this.NotifyPropertyChanged("MRestrict");
            }
        }

        [DBColumn(Name = "MName1", Storage = "m_MName1", DbType = "VarChar(50) NULL")]
        public string MName1
        {
            get { return this.m_MName1; }
            set
            {
                this.m_MName1 = value;
                this.NotifyPropertyChanged("MName1");
            }
        }

        [DBColumn(Name = "MName2", Storage = "m_MName2", DbType = "VarChar(50) NULL")]
        public string MName2
        {
            get { return this.m_MName2; }
            set
            {
                this.m_MName2 = value;
                this.NotifyPropertyChanged("MName2");
            }
        }

        [DBColumn(Name = "MName3", Storage = "m_MName3", DbType = "VarChar(50) NULL")]
        public string MName3
        {
            get { return this.m_MName3; }
            set
            {
                this.m_MName3 = value;
                this.NotifyPropertyChanged("MName3");
            }
        }

        [DBColumn(Name = "UserCreateDt", Storage = "m_UserCreateDt", DbType = "SmallDateTime NULL")]
        public DateTime? UserCreateDt
        {
            get { return this.m_UserCreateDt; }
            set
            {
                this.m_UserCreateDt = value;
                this.NotifyPropertyChanged("UserCreateDt");
            }
        }

        [DBColumn(Name = "IS_BACK_ENTRY_AUTH", Storage = "m_IS_BACK_ENTRY_AUTH", DbType = "126")]
        public string IS_BACK_ENTRY_AUTH
        {
            get { return this.m_IS_BACK_ENTRY_AUTH; }
            set
            {
                this.m_IS_BACK_ENTRY_AUTH = value;
                this.NotifyPropertyChanged("IS_BACK_ENTRY_AUTH");
            }
        }

        [DBColumn(Name = "ALL_DEPT", Storage = "m_ALL_DEPT", DbType = "126")]
        public string ALL_DEPT
        {
            get { return this.m_ALL_DEPT; }
            set
            {
                this.m_ALL_DEPT = value;
                this.NotifyPropertyChanged("ALL_DEPT");
            }
        }

        #endregion //properties

       
    }
   
    
    public partial class dcUser
    {
        private string m_RoleName = string.Empty;
        public string RoleName
        {
            get { return m_RoleName; }
            set { m_RoleName = value; }
        }

        private bool m_IsAdmin = false;
        public bool IsAdmin
        {
            get { return m_IsAdmin; }
            set { m_IsAdmin = value; }
        }

        public bool IsUserAdmin
        {
            get {
                bool isAdmin = false;
                if (m_RoleID == 1 | m_RoleName.ToUpper() == "ADMINS")
                {
                    isAdmin = true;
                }

                if (m_IsAdmin)
                {
                    isAdmin = true;
                }

                return isAdmin;
            }
        }

        private int m_LoginLocationID = 0;
        public int LoginLocationID
        {
            get { return m_LoginLocationID; }
            set { m_LoginLocationID = value; }
        }

        private string m_LoginLocationCode = string.Empty;
        public string LoginLocationCode
        {
            get { return m_LoginLocationCode; }
            set { m_LoginLocationCode = value; }
        }

        private string m_LoginLocationName = string.Empty;
        public string LoginLocationName
        {
            get { return m_LoginLocationName; }
            set { m_LoginLocationName = value; }
        }


        private List<dcLocationUser> m_LocationUserList = new List<dcLocationUser>();
        public List<dcLocationUser> LocationUserList
        {
            get { return m_LocationUserList; }
            set { m_LocationUserList = value; }
        }
        public int CompanyId { get; set; }
        public int DEPARTMENT_ID { get; set; }


    }
}
