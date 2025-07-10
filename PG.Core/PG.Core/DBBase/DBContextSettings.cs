using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Linq;

namespace PG.Core.DBBase
{
    public delegate void DatabaseTypeChangedEventHandler(object sender, EventArgs e);
    public delegate void xUpdateNoFieldNameChangedEventHandler(object sender, EventArgs e);
    //public delegate void DeferredLoadingEnabledChangedEventHandler(object sender, EventArgs e);
   // public delegate void ObjectTrackingEnabledChangedEventHandler(object sender, EventArgs e);

    public partial class DBContextSettings
    {
        private string m_Name = string.Empty;
        private DatabaseTypeEnum m_DatabaseType = DatabaseTypeEnum.SQLServer;
        private DatabaseConnectModeEnum m_DatabaseConnectMode = DatabaseConnectModeEnum.Direct;
        private IsolationLevel m_IsolationLevel = IsolationLevel.ReadCommitted;
        private string m_ApplicationName = string.Empty;
        private string m_ConnectionString = string.Empty;
        private int m_ConnectionTimeOut = 15;
        private bool m_IsWindowsAuthentication = false;
        private bool m_IsCredantialInternal = false;
        private bool m_IsConfigEncrypted = false;
        private string m_EncryptionKey = string.Empty;
        private string m_UserID = string.Empty;
        private string m_Password = string.Empty;
        private string m_AccessFilePassword = string.Empty;
        private bool m_IsSqlAppRole = false;
        private string m_SqlAppRoleName = string.Empty;
        private string m_SqlAppRolePassword = string.Empty;
        private bool m_DeferredLoadingEnabled = false;
        private bool m_ObjectTrackingEnabled = false;
        private string m_xUpdateNoFieldName = "xUpdateNo";

        private bool m_UseDBColumnMap = false;
        private bool m_ConvertNullToDefault = false;
        private DBQueryModeEnum m_DBQueryMode = DBQueryModeEnum.DBCommandInfo;
        private bool m_LoadAssociation = false;
        private DataLoadOptions m_DataLoadOption = null;

        private string m_DatabaseVersion = string.Empty;
        private string m_DBSchemaName = "";
        private bool m_AlterDBSchema = false;

        private bool m_TextCaseInsensitive = false;

        private bool m_ConvertBoolData = false;
        private string m_BoolDataType = "String";
        private string m_BoolTrueValue = "Y";
        private string m_BoolFalseValue = "N";


        #region public events
        public event DatabaseTypeChangedEventHandler OnDatabaseTypeChanged;
        public event xUpdateNoFieldNameChangedEventHandler OnxUpdateNoFieldNameChanged;
        //public event DeferredLoadingEnabledChangedEventHandler OnDeferredLoadingEnabledChanged;
        // public event ObjectTrackingEnabledChangedEventHandler OnObjectTrackingEnabledChanged;

        protected virtual void DatabaseTypeChanged(EventArgs e)
        {
            if (OnDatabaseTypeChanged != null)
            {
                OnDatabaseTypeChanged(this, EventArgs.Empty);
            }
        }

        protected virtual void xUpdateNoFieldNameChanged(EventArgs e)
        {
            if (OnxUpdateNoFieldNameChanged != null)
            {
                OnxUpdateNoFieldNameChanged(this, EventArgs.Empty);
            }
        }

        //protected virtual void DeferredLoadingEnabledChanged(EventArgs e)
        //{
        //    if (OnDeferredLoadingEnabledChanged != null)
        //    {
        //        OnDeferredLoadingEnabledChanged(this, EventArgs.Empty);
        //    }
        //}

        //protected virtual void ObjectTrackingEnabledChanged(EventArgs e)
        //{
        //    if (OnObjectTrackingEnabledChanged != null)
        //    {
        //        OnObjectTrackingEnabledChanged(this, EventArgs.Empty);
        //    }
        //}

        //public event EventHandler OnDatabaseTypeChanged;
        //public event EventHandler OnxUpdateNoFieldNameChanged;



        #endregion //public events


        public string Name
        {
            get { return m_Name; }
            set
            {
                m_Name = value;
            }
        }

        public DatabaseTypeEnum DatabaseType
        {
            get { return m_DatabaseType; }
            set
            {
                m_DatabaseType = value;
                //OnDatabaseTypeChanged(this,null);
                DatabaseTypeChanged(EventArgs.Empty);
            }
        }

        public string DatabaseVersion
        {
            get { return m_DatabaseVersion; }
            set
            {
                m_DatabaseVersion = value;
            }
        }

        public DatabaseConnectModeEnum DatabaseConnectMode
        {
            get { return m_DatabaseConnectMode; }
            set
            {
                m_DatabaseConnectMode = value;
            }
        }


        public IsolationLevel IsolationLevel
        {
            get { return m_IsolationLevel; }
            set
            {
                m_IsolationLevel = value;
            }
        }


        public string ApplicationName
        {
            get { return m_ApplicationName; }
            set { m_ApplicationName = value; }
        }



        public string ConnectionString
        {
            get { return m_ConnectionString; }
            set
            {
                m_ConnectionString = value;
            }
        }

        public int ConnectionTimeOut
        {
            get { return m_ConnectionTimeOut; }
            set { m_ConnectionTimeOut = value; }
        }

        public bool IsWindowsAuthentication
        {
            get { return m_IsWindowsAuthentication; }
            set { m_IsWindowsAuthentication = value; }
        }


        public bool IsCredantialInternal
        {
            get { return m_IsCredantialInternal; }
            set { m_IsCredantialInternal = value; }
        }
        public bool IsConfigEncrypted
        {
            get { return m_IsConfigEncrypted; }
            set { m_IsConfigEncrypted = value; }
        }
        public string EncryptionKey
        {
            get { return m_EncryptionKey; }
            set { m_EncryptionKey = value; }
        }

        public string UserID
        {
            get { return m_UserID; }
            set { m_UserID = value; }
        }

        public string Password
        {
            get { return m_Password; }
            set { m_Password = value; }
        }

        public string AccessFilePassword
        {
            get { return m_AccessFilePassword; }
            set { m_AccessFilePassword = value; }
        }

        public bool IsSqlAppRole
        {
            get { return m_IsSqlAppRole; }
            set { m_IsSqlAppRole = value; }
        }

        public string SqlAppRoleName
        {
            get { return m_SqlAppRoleName; }
            set { m_SqlAppRoleName = value; }
        }

        public string SqlAppRolePassword
        {
            get { return m_SqlAppRolePassword; }
            set { m_SqlAppRolePassword = value; }
        }


        public string xUpdateNoFieldName
        {
            get { return m_xUpdateNoFieldName; }
            set
            {
                m_xUpdateNoFieldName = value;
                xUpdateNoFieldNameChanged(EventArgs.Empty);
                // OnDatabaseTypeChanged(this, null);
            }
        }

        public bool DeferredLoadingEnabled
        {
            //for disconnected use set FALSE

            get { return m_DeferredLoadingEnabled; }
            set
            {
                m_DeferredLoadingEnabled = value;
                //DeferredLoadingEnabledChanged(EventArgs.Empty);
            }
        }

        public bool ObjectTrackingEnabled
        {
            //for disconnected use set FALSE
            get { return m_ObjectTrackingEnabled; }
            set
            {
                m_ObjectTrackingEnabled = value;
                // ObjectTrackingEnabledChanged(EventArgs.Empty);
            }
        }

        public bool ConvertNullToDefault
        {
            get { return m_ConvertNullToDefault; }
            set { m_ConvertNullToDefault = value; }
        }

        public bool UseDBColumnMap
        {
            get { return m_UseDBColumnMap; }
            set { m_UseDBColumnMap = value; }
        }

        public DBQueryModeEnum DBQueryMode
        {
            get { return m_DBQueryMode; }
            set { m_DBQueryMode = value; }
        }

        public bool LoadAssociation
        {
            get { return m_LoadAssociation; }
            set { m_LoadAssociation = value; }
        }

        public DataLoadOptions DataLoadOption
        {
            get { return m_DataLoadOption; }
            set { m_DataLoadOption = value; }
        }


        public string DBSchemaName
        {
            get { return m_DBSchemaName; }
            set { m_DBSchemaName = value; }
        }

        public bool AlterDBSchema
        {
            get { return m_AlterDBSchema; }
            set { m_AlterDBSchema = value; }
        }


        public bool TextCaseInsensitive
        {
            get { return m_TextCaseInsensitive; }
            set { m_TextCaseInsensitive = value; }
        }


        public bool ConvertBoolData
        {
            get { return m_ConvertBoolData; }
            set { m_ConvertBoolData = value; }
        }

        public string BoolDataType
        {
            get { return m_BoolDataType; }
            set { m_BoolDataType = value; }
        }

        public string BoolTrueValue
        {
            get { return m_BoolTrueValue; }
            set { m_BoolTrueValue = value; }
        }

        public string BoolFalseValue
        {
            get { return m_BoolFalseValue; }
            set { m_BoolFalseValue = value; }
        }
    }
}
