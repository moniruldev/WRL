using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Data.Linq;
//using System.Linq.Dynamic;
using PG.Core.DBBase;
using PG.DBClass.SystemDC;


namespace PG.BLLibrary.SystemsBL
{
    /// <summary>
    /// AppAppInfoBL
    /// Last update By Moni, Date 09-03-2015
    /// </summary>
    
    public class AppInfoBL
    {
        public static string GetAppInfo_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT tblAppInfo.* ");
            sb.Append(" FROM tblAppInfo ");
            sb.Append(" WHERE 1=1 ");
            return sb.ToString();
        }


        public static List<dcAppInfo> GetAppInfoList()
        {
            return GetAppInfoList(null);
        }
        public static List<dcAppInfo> GetAppInfoList(DBContext dc)
        {
            List<dcAppInfo> cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                //TODO: need to change this code
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetAppInfo_SQLString());

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcAppInfo>(dbq,dc);

                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    cObjList = (from c in dataContext.GetTable<dcAppInfo>()
                //                select c).ToList();
                //}
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static dcAppInfo GetAppInfoByAppID(int pAppID)
        {
            return GetAppInfoByAppID(pAppID, null);
        }
        public static dcAppInfo GetAppInfoByAppID(int pAppID, DBContext dc)
        {
            dcAppInfo cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetAppInfo_SQLString());
                sb.Append(" AND tblAppInfo.AppID=@appID ");
                cmdInfo.DBParametersInfo.Add("@appID", pAppID);
                
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObj = DBQuery.ExecuteDBQuery<dcAppInfo>(dbq, dc).FirstOrDefault();
                
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObj;
        }


        public static int Insert(dcAppInfo cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcAppInfo cObj, DBContext dc)
        {
            bool isDCInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            int id = dc.DoInsert<dcAppInfo>(cObj);
            if (id > 0) { cObj.AppID = id; }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcAppInfo cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcAppInfo cObj, DBContext dc)
        {
            bool isDCInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            int cnt = dc.DoUpdate<dcAppInfo>(cObj);
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int AppID)
        {
            return Delete(AppID, null);
        }
        public static bool Delete(int AppID, DBContext dc)
        {
            dcAppInfo cObj = new dcAppInfo();
            cObj.AppID = AppID;
            bool isDCInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            int cnt = dc.DoDelete<dcAppInfo>(cObj);
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }


        public static bool CheckAppRegistration(int pAppID)
        {
            return CheckAppRegistration(pAppID, null);
        }

        public static bool CheckAppRegistration(int pAppID, DBContext dc)
        {
            bool result = false;
            bool doBlock = false;

            dcAppInfo appInfo = GetAppInfoByAppID(pAppID, dc);

            if (appInfo == null)
            {
                return false;
            }

            if (appInfo.CheckReg)
            {
                if (!appInfo.IsValid)
                {
                    result = false;
                    doBlock = true;
                }
                else
                {
                    if (appInfo.RegDate.Value.AddDays(appInfo.RegDays) <= DateTime.Today)
                    {
                        //string strSql = "Select * From tblAppInfo";
                        doBlock = true;
                        result = false;
                    }
                    else
                    {
                        result = true;
                    }
                }
            }
            else
            {
                result = true;
            }

            if (doBlock)
            {
                appInfo.AppID = appInfo.AppID;
                appInfo.IsValid = false;
                Update(appInfo);
            }
            return result;
        }

    }
}
