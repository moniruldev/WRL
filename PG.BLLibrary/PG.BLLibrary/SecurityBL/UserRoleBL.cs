using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Data.Linq;
//using System.Linq.Dynamic;
using PG.Core.Extentions;
using PG.Core.DBBase;
using PG.Core.DBFilters;
using PG.DBClass.SecurityDC;
using PG.DBClass.InventoryDC;

namespace PG.BLLibrary.SecurityBL
{
    /// <summary>
    /// AppMenuBL
    /// Last update By Moni, Date 09-03-2015
    /// </summary>
    public class UserRoleBL
    {
        public static string GetUserRole_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT tblUserRole.* ");
            sb.Append(" FROM tblUserRole ");
            sb.Append(" WHERE 1=1 ");
            return sb.ToString();
        }


        public static string GetUserRole_Permitted_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT ");
            sb.Append("  ROWNUM SL,u.USERID,u.FULLNAME, u.USERNAME USER_NAME, ur.APPID, ur.ROLEID,r.ROLENAME, r.ROLEDESC ");
            sb.Append(" ,r.ISACTIVE,r.ISADMIN ");
            sb.Append(" ,(SELECT ROLENAME FROM TBLROLE WHERE ROLEID=u.ROLEID) DEFAULT_ROLE ");
            sb.Append(" FROM tblUSER u  ");
            sb.Append(" LEFT JOIN  TBLUSERROLE ur    ON ur.USERID=u.USERID   ");
            sb.Append(" LEFT JOIN TBLROLE r ON ur.ROLEID=r.ROLEID   ");
            sb.Append(" WHERE 1=1 ");
            return sb.ToString();
        }

        public static DataLoadOptions RoleLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcPurchase>(obj => obj.Supplier);
            //dlo.LoadWith<DBClass.dcPurchase>(obj => obj.TranType);
            return dlo;
        }

        public static List<dcUserRole> GetUserRoleList()
        {
            return GetUserRoleList(0, null);
        }

        public static List<dcUserRole> GetUserRoleList(int pAppID)
        {
            return GetUserRoleList(pAppID, null);
        }

        public static List<dcUserRole> GetUserRoleList(int pAppID, DBContext dc)
        {
            DBQuery dbq = new DBQuery();
            if (pAppID > 0)
            {
                dbq.DBFilterList.Add(new DBFilter("AppID", pAppID));
            }
            return GetUserRoleList(dbq, dc);
        }


        public static List<dcUserRole> GetUserRoleList(DBQuery dbq, DBContext dc)
        {
            List<dcUserRole> cObjList = new List<dcUserRole>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetUserRole_SQLString());

                //DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcUserRole>(dbq, dc).ToList();

                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    if (dbq == null)
                //    {
                //        dbq = new DBQuery();
                //        //dbq.OrderBy = "PeriodStartDate";
                //    }


                //    cObjList = DBQuery.ExecuteDBQuery<dcRole>(dbq, dc);
                //}
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }


        public static List<dcUserRole> GetUserRoleListByUserID(int pAppID, int pUserID)
        {
            return GetUserRoleListByUserID(pAppID, pUserID, null);
        }
        public static List<dcUserRole> GetUserRoleListByUserID(int pAppID, int pUserID, DBContext dc)
        {
            List<dcUserRole> cObj = new List<dcUserRole>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);


                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetUserRole_SQLString());
                sb.Append(" AND tblUserRole.AppID=@appID AND tblUserRole.UserID=@userID");
                cmdInfo.DBParametersInfo.Add("@appID", pAppID);
                cmdInfo.DBParametersInfo.Add("@userID", pUserID);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObj = DBQuery.ExecuteDBQuery<dcUserRole>(dbq, dc).ToList();

                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    var result = (from c in dataContext.GetTable<dcRole>()
                //                  where c.AppID == pAppID && c.RoleName == pRoleName
                //                  select c).ToList();
                //    if (result.Count() > 0)
                //    {
                //        cObj = result.First();
                //    }
                //}
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObj;
        }

        public static dcUserRole GetUserRoleByUserRoleID(int pRoleID)
        {
            return GetUserRoleByUserRoleID(pRoleID, null);
        }
        public static dcUserRole GetUserRoleByUserRoleID(int pUserRoleID, DBContext dc)
        {
            dcUserRole cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetUserRole_SQLString());
                sb.Append(" AND tblUserRole.UserRoleID=@userRoleID");
                cmdInfo.DBParametersInfo.Add("@userRoleID", pUserRoleID);


                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObj = DBQuery.ExecuteDBQuery<dcUserRole>(dbq, dc).FirstOrDefault();

                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    var result = (from c in dataContext.GetTable<dcRole>()
                //                  where c.RoleID == pRoleID
                //                  select c).ToList();
                //    if (result.Count() > 0)
                //    {
                //        cObj = result.First();
                //    }
                //}
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObj;
        }



        public static bool IsUserRoleExists(int AppID, int pUserID, int pRoleID)
        {
            return IsUserRoleExists(AppID, pUserID, pRoleID, null);
        }
        public static bool IsUserRoleExists(int AppID, int pUserID, int pRoleID, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                string sql = "select Count(UserRoleID) tcount from tblUserRole Where tblUserRole.AppID=@appID AND tblUserRole.UserID=@userID AND tblUserRole.Role=@roleID";
                cmdInfo.DBParametersInfo.Add("@appID", AppID);
                cmdInfo.DBParametersInfo.Add("@userID", pUserID);
                cmdInfo.DBParametersInfo.Add("@roleID", pRoleID);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sql.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;


                int tcount = Convert.ToInt32(DBQuery.ExecuteDBScalar(dbq));
                if (tcount > 0)
                {
                    isData = true;
                }

                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    var result = from cObj in dataContext.GetTable<dcRole>()
                //                 where cObj.AppID == AppID && cObj.RoleName == pRoleName
                //                 select cObj;
                //    isData = result.Count() > 0;
                //}
            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return isData;
        }
        public static bool IsUserRoleExists(int AppID, int pUserID, int pRoleID, int pUserRoleID)
        {
            return IsUserRoleExists(AppID, pRoleID, pRoleID, pUserRoleID, null);
        }
        public static bool IsUserRoleExists(int AppID, int pUserID, int pRoleID, int pUserRoleID, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                string sql = "select Count(UserRoleID) tcount from tblUserRole Where tblUserRole.UserRoleID<>@useRrollID AND tblUserRole.AppID=@appID AND tblUserRole.UserID=@userID AND tblUserRole.RoleID=@roleID";
                cmdInfo.DBParametersInfo.Add("@useRrollID", pUserRoleID);
                cmdInfo.DBParametersInfo.Add("@appID", AppID);
                cmdInfo.DBParametersInfo.Add("@userId", pUserID);
                cmdInfo.DBParametersInfo.Add("@roleID", pRoleID);



                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sql.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;


                int tcount = Convert.ToInt32(DBQuery.ExecuteDBScalar(dbq));
                if (tcount > 0)
                {
                    isData = true;
                }

                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    var result = from cObj in dataContext.GetTable<dcRole>()
                //                 where cObj.RoleID != pRoleID && cObj.AppID == AppID && cObj.RoleName == pRoleName
                //                 select cObj;
                //    isData = result.Count() > 0;
                //}
            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return isData;
        }

        public static int Insert(dcUserRole cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcUserRole cObj, DBContext dc)
        {
            bool isDCInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            int id = dc.DoInsert<dcUserRole>(cObj, true);
            if (id > 0) { cObj.RoleID = id; }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcUserRole cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcUserRole cObj, DBContext dc)
        {
            bool isDCInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            int cnt = dc.DoUpdate<dcUserRole>(cObj);
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int AppID, int pRoleID)
        {
            return Delete(AppID, pRoleID, null);
        }
        public static bool Delete(int AppID, int pRoleID, DBContext dc)
        {
            dcUserRole cObj = new dcUserRole();
            cObj.AppID = AppID;
            cObj.RoleID = pRoleID;
            bool isDCInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            int cnt = dc.DoDelete<dcUserRole>(cObj);
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static List<dcRolePermission> GetRolePermissionList(int pRoleID, int pAppObjectID)
        {
            return GetRolePermissionList(pRoleID, pAppObjectID, null);
        }
        public static List<dcRolePermission> GetRolePermissionList(int pRoleID, int pAppObjectID, DBContext dc)
        {
            List<dcRolePermission> cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                string sql = "select * from tblRolePermission Where tblRolePermission.RoleID=@rollID AND tblRolePermission.AppObjectID=@appObjectID";
                //StringBuilder sb = new StringBuilder(GetRole_SQLString());
                //sb.Append(" AND tblRole.RoleID=@rollID  AND tblRole. ");
                cmdInfo.DBParametersInfo.Add("@rollID", pRoleID);
                cmdInfo.DBParametersInfo.Add("@appObjectID", pAppObjectID);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sql.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcRolePermission>(dbq, dc).ToList();

                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    cObjList = (from c in dataContext.GetTable<dcRolePermission>()
                //                where c.RoleID == pRoleID && c.AppObjectID == pAppObjectID
                //                select c).ToList();
                //}
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }


        public static List<dcUserRole> GetUserPermittedRoleListByUserID(clsPrmInventory cPboj)
        {
            return GetUserPermittedRoleListByUserID(cPboj, null);
        }
        public static List<dcUserRole> GetUserPermittedRoleListByUserID(clsPrmInventory cPboj, DBContext dc)
        {
            List<dcUserRole> cObj = new List<dcUserRole>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);


                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetUserRole_Permitted_SQLString());
               if(cPboj.user_name!= string.Empty)
               {
                   sb.Append(" AND UPPER(u.USERNAME)=@USERNAME ");
                   cmdInfo.DBParametersInfo.Add("@USERNAME",  cPboj.user_name.ToUpper());
               }

                if(cPboj.user_id>0)
                {
                    sb.Append(" AND ur.USERID=@USERID ");
                    cmdInfo.DBParametersInfo.Add("@USERID", cPboj.user_id);
                }
                

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObj = DBQuery.ExecuteDBQuery<dcUserRole>(dbq, dc).ToList();

                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    var result = (from c in dataContext.GetTable<dcRole>()
                //                  where c.AppID == pAppID && c.RoleName == pRoleName
                //                  select c).ToList();
                //    if (result.Count() > 0)
                //    {
                //        cObj = result.First();
                //    }
                //}
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObj;
        }




   

        

        

        public static bool SaveList(List<dcUserRole> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcUserRole> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcUserRole oDet in detList)
            {
                switch (oDet._RecordState)
                {
                    case RecordStateEnum.Added:
                        int a = Insert(oDet, dc);
                        break;
                    case RecordStateEnum.Edited:
                        bool e = Update(oDet, dc);
                        break;
                    //case RecordStateEnum.Deleted:
                    //    bool d = Delete(oDet.UserID, dc);
                    //    break;
                    default:
                        break;
                }
            }
            dc.CommitTransaction(isTransInit);
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            bStatus = true;
            return bStatus;
        }



        public static bool Delete(int AppID, int pRoleID, int pUserID)
        {
            return Delete(AppID, pRoleID, pUserID, null);
        }
        public static bool Delete(int AppID, int pRoleID, int pUserID, DBContext dc)
        {
            dcUserRole cObj = new dcUserRole();
            cObj.AppID = AppID;
            cObj.RoleID = pRoleID;
            cObj.UserID = pUserID;
            bool isDCInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            int cnt = dc.DoDelete<dcUserRole>(cObj);
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

    }
}
