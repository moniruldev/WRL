using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.Linq;
using System.Text;
using System.Reflection;
using System.Data.Linq.Mapping;
using System.Configuration;

namespace PG.Core.DBBase
{
    public partial class DBContextManager
    {
        public static readonly List<DBContextSettings> DBContextSettingsList = new List<DBContextSettings>();
        public static string DefaultContextSettingsName = "default";
        public static string SecurityContextSettingsName = "security";
        public static string HRMContextSettingsName = "hrm";

        public static DBContextSettings GetOrCreateDBContextSettings()
        {
            return GetOrCreateDBContextSettings(DefaultContextSettingsName);
        }

        public static DBContextSettings GetOrCreateDBContextSettings(string DBContextSettingsName)
        {
            DBContextSettings pDBCSettings = GetDBContextSettings(DBContextSettingsName);
            if (pDBCSettings == null)
            {
                pDBCSettings = CreateDBContextSettings(DBContextSettingsName);
            }
            return pDBCSettings;
        }

        public static DBContextSettings CreateDBContextSettings()
        {
            return CreateDBContextSettings(DefaultContextSettingsName);
        }

        public static DBContextSettings CreateDBContextSettings(string DBContextSettingsName)
        {
            DBContextSettings pDBCSettings = new DBContextSettings();
            pDBCSettings.Name = DBContextSettingsName;
            if (GetDBContextSettingsIndex(DBContextSettingsName) != -1)
            {
                throw new DuplicateKeyException(DBContextSettingsName, "DB Context Name Already exists!");
            }
            lock (DBContextSettingsList)
            {
                DBContextSettingsList.Add(pDBCSettings);
            }
            return pDBCSettings;
        }

        public static void RemoveDBConextSettings()
        {
            RemoveDBConextSettings(DefaultContextSettingsName);
        }
        public static void RemoveDBConextSettings(string DBContextSettingsName)
        {
            if (DBContextSettingsList.Count > 0)
            {
                int index = GetDBContextSettingsIndex(DBContextSettingsName);
                if (index != -1)
                {
                    lock (DBContextSettingsList)
                    {
                        DBContextSettingsList.RemoveAt(index);
                    }
                }
            }
        }
        private static int GetDBContextSettingsIndex(string DBContextSettingsName)
        {
            int index = -1;
            foreach (DBContextSettings cObj in DBContextSettingsList)
            {
                if (cObj.Name.ToLower() == DBContextSettingsName.ToLower())
                {
                    index = DBContextSettingsList.IndexOf(cObj);
                    break;
                }
            }
            return index;
        }


        public static DBContextSettings GetDBContextSettings()
        {
            return GetDBContextSettings(DefaultContextSettingsName);
        }

        public static DBContextSettings GetDBContextSettings(string ContextSettingsName)
        {
            DBContextSettings pDBCSettings = null;
            if (DBContextSettingsList.Count > 0)
            {
                foreach (DBContextSettings cObj in DBContextSettingsList)
                {
                    if (cObj.Name.ToLower() == ContextSettingsName.ToLower())
                    {
                        pDBCSettings = cObj;
                        break;
                    }
                }
            }
            return pDBCSettings;
        }

        public static DBContextSettings GetDBContextSettings(int index)
        {
            DBContextSettings pDBCSettings = null;
            if (DBContextSettingsList.Count > 0)
            {
                pDBCSettings = DBContextSettingsList[index];
            }
            return pDBCSettings;
        }

        public static DBContext CreateDBContext()
        {
            return CreateDBContext(GetDBContextSettings());
        }

        public static DBContext CreateDBContext(DBContextSettings pDBCSettings)
        {
            DBContext dc = new DBContext(pDBCSettings);
            return dc;
        }

        public static DBContext CreateAndInitDBContext()
        {
            return CreateAndInitDBContext(GetDBContextSettings());
        }

        public static DBContext CreateAndInitDBContext(DBContextSettings pDBCSettings)
        {
            DBContext dc = CreateDBContext(pDBCSettings);
            dc.InitDBContext();
            return dc;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dc"></param>
        /// <returns></returns>
        /// uses : isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
        /// << dc = DBContextManager.CheckAndInitDBContext(dc,out isDCInit); >>
        ///            
        public static bool CheckAndInitDBContext(ref DBContext dc)
        {
            return CheckAndInitDBContext(ref dc, GetDBContextSettings());
        }

        public static bool CheckAndInitDBContext(ref DBContext dc, DBContextSettings pDBCSettings)
        {
            if (dc == null)
            {
                dc = new DBContext(pDBCSettings);
            }
            return dc.InitDBContext();
        }


        public static bool CheckAndInitDBContextN(DBContext dc, DBContextSettings pDBCSettings)
        {
            if (dc == null)
            {
                dc = new DBContext(pDBCSettings);
            }
            return dc.InitDBContext();
        }


        public static void ReleaseDBContext(ref DBContext dc)
        {
            ReleaseDBContext(ref dc, true);
        }
        public static void ReleaseDBContext(ref DBContext dc, bool isDCInit)
        {
            if (isDCInit)
            {
                dc.ReleaseDBContext();
                dc = null;
            }
        }

        public static void LoadConfigruation()
        {
            //ConfigurationManager.ConnectionStrings["Accounting_ConnectionString"].ToString();
            //ConfigurationSettings.GetConfig("aspnet2ConfigurationDemo") as ASPNET2Configuration;

            DBContextManagerConfiguration config = ConfigurationManager.GetSection("dbContextManager") as DBContextManagerConfiguration;
            string defaultConfig = config.DefaultDBContext.Trim();

            DefaultContextSettingsName = defaultConfig;
            SecurityContextSettingsName = config.SecurityDBContext.Trim() == string.Empty ? defaultConfig : config.SecurityDBContext.Trim();
            HRMContextSettingsName = config.HrmDBContext.Trim() == string.Empty ? defaultConfig : config.HrmDBContext.Trim();
            foreach (DBContextManagerConfiguration.DBContextElement dbContextElm in config.DBContextCollection)
            {
                DBContextSettings contextSettings = new DBContextSettings();
                contextSettings.Name = dbContextElm.Name;
                contextSettings.DatabaseType = dbContextElm.DatabaseType;
                contextSettings.DatabaseVersion = dbContextElm.DatabaseVersion;
                
                contextSettings.ConnectionString = ConfigurationManager.ConnectionStrings[dbContextElm.ConnectionStringName].ToString();


                contextSettings.DBSchemaName = dbContextElm.DBSchemaName;
                contextSettings.TextCaseInsensitive = dbContextElm.TextCaseInsensitive;
                contextSettings.AlterDBSchema = dbContextElm.AlterDBSchema;

                contextSettings.ConvertBoolData = dbContextElm.ConvertBoolData;
                contextSettings.BoolDataType = dbContextElm.BoolDataType;
                contextSettings.BoolTrueValue = dbContextElm.BoolTrueValue;
                contextSettings.BoolFalseValue = dbContextElm.BoolFalseValue;
                contextSettings.ConvertNullToDefault = dbContextElm.NullToDefault;

                

                DBContextSettingsList.Add(contextSettings);
            }
        }
    }
    
    public class DCThreadSafe
    {
        //singleton pattern
        private static DCThreadSafe _oInstance = null;
        private static readonly object padlock = new object();

        public static DCThreadSafe Instance
        {
            get
            {
                if (DCThreadSafe._oInstance == null)
                {
                    lock (padlock)
                    {
                        if (DCThreadSafe._oInstance == null)
                        {
                            DCThreadSafe newDB = new DCThreadSafe();

                            //System.Threading.Thread.MemoryBarrier(); //for multi proccessor
                            DCThreadSafe._oInstance = newDB;
                        }
                    }
                }
                return DCThreadSafe._oInstance;
            }
        }

        private DCThreadSafe()
        {
            //constructor
        }
    }
}
