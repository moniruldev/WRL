using PG.Core.DBBase;
using PG.DBClass.SecurityDC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Text;

namespace PG.BLLibrary.SecurityBL
{
    public class RoleMenuBL
    {
        public static string GetRoleMenu_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from tblRoleMenu ");
            //sb.Append(" INNER JOIN tblRolePermission  ON tblRole.RoleID=tblRolePermission.RoleID ");
            sb.Append(" WHERE 1=1 ");
            return sb.ToString();
        }

        public static List<dcRoleMenu> GetRoleMenuList(int pAppID)
        {
            return GetRoleMenuList(pAppID, null);
        }
        public static List<dcRoleMenu> GetRoleMenuList(int pAppID, DBContext dc)
        {
            List<dcRoleMenu> cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();

                StringBuilder sb = new StringBuilder(GetRoleMenu_SQLString());
                sb.Append(" AND tblRoleMenu.AppID=@appID");
                cmdInfo.DBParametersInfo.Add("@appID", pAppID);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcRoleMenu>(dbq, dc).ToList();

                //using (DataContext dataContext = dc.NewDataContext())
                //{

                //    cObjList = (from c in dataContext.GetTable<dcRolePermission>()
                //                from r in dataContext.GetTable<dcRole>()
                //                where c.RoleID == r.RoleID && r.AppID == pAppID
                //                select c).ToList();

            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }



        public static List<dcRoleMenu> GetRoleMenuListByRole(int pAppID, int pRoleID)
        {
            return GetRoleMenuListByRole(pAppID, pRoleID, null);
        }
        public static List<dcRoleMenu> GetRoleMenuListByRole(int pAppID, int pRoleID, DBContext dc)
        {

            List<dcRoleMenu> cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();             
                StringBuilder sb = new StringBuilder(GetRoleMenu_SQLString());

                sb.Append(" AND tblRoleMenu.AppID=@appID");
                cmdInfo.DBParametersInfo.Add("@appID", pAppID);

                sb.Append(" AND tblRoleMenu.RoleID=@rollID ");
                cmdInfo.DBParametersInfo.Add("@rollID", pRoleID);


                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcRoleMenu>(dbq, dc).ToList();
           
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcRoleMenu> GetRoleMenuListByRole(int pAppID, int pRoleID, int pUserID)
        {
            return GetRoleMenuListByRole(pAppID, pRoleID, pUserID, true, null);
        }

        public static List<dcRoleMenu> GetRoleMenuListByRole(int pAppID, int pRoleID, int pUserID, DBContext dc)
        {
            return GetRoleMenuListByRole(pAppID, pRoleID, pUserID, true, null);
        }

        public static List<dcRoleMenu> GetRoleMenuListByRole(int pAppID, int pRoleID, int pUserID, bool removeDuplicate)
        {
            return GetRoleMenuListByRole(pAppID, pRoleID, pUserID, removeDuplicate, null);
        }

        public static List<dcRoleMenu> GetRoleMenuListByRole(int pAppID, int pRoleID, int pUserID, bool removeDuplicate, DBContext dc)
        {

            List<dcRoleMenu> cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                //string sql = "select * from tblRolePermission Where tblRolePermission.RoleID=@rollID ";
                StringBuilder sb = new StringBuilder(GetRoleMenu_SQLString());

                sb.Append(" AND tblRoleMenu.AppID=@appID");
                cmdInfo.DBParametersInfo.Add("@appID", pAppID);

                sb.Append(" AND tblRoleMenu.RoleID=@rollID ");
                cmdInfo.DBParametersInfo.Add("@rollID", pRoleID);


                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcRoleMenu>(dbq, dc).ToList();


                List<dcUserRole> userRoleList = UserRoleBL.GetUserRoleListByUserID(pAppID, pUserID);

                foreach (dcUserRole userRole in userRoleList)
                {
                    cObjList.AddRange(RoleMenuBL.GetRoleMenuListByRole(pAppID, userRole.RoleID));
                }


                if (removeDuplicate)
                {
                    //select distinct value
                    cObjList = cObjList.GroupBy(g => new { g.APPID, g.APPMENUID })
                                   .Select(g => g.First())
                                   .ToList();
                }

            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
















        //public static List<dcRoleMenu> GetRoleMenuListByRole(int pRoleID)
        //{
        //    return GetRoleMenuListByRole(pRoleID, null);
        //}
        //public static List<dcRoleMenu> GetRoleMenuListByRole(int pRoleID, DBContext dc)
        //{

        //    List<dcRoleMenu> cObjList = null;
        //    bool isDCInit = false;
        //    try
        //    {
        //        isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

        //        DBCommandInfo cmdInfo = new DBCommandInfo();
        //        //string sql = "select * from tblRolePermission Where tblRolePermission.RoleID=@rollID ";
        //        StringBuilder sb = new StringBuilder(GetRoleMenu_SQLString());
        //        sb.Append(" AND tblRoleMenu.RoleID=@rollID ");
        //        cmdInfo.DBParametersInfo.Add("@rollID", pRoleID);


        //        DBQuery dbq = new DBQuery();
        //        dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
        //        cmdInfo.CommandText = sb.ToString();
        //        cmdInfo.CommandType = CommandType.Text;
        //        dbq.DBCommandInfo = cmdInfo;

        //        cObjList = DBQuery.ExecuteDBQuery<dcRoleMenu>(dbq, dc).ToList();

        //        //using (DataContext dataContext = dc.NewDataContext())
        //        //{
        //        //    cObjList = (from c in dataContext.GetTable<dcRolePermission>()
        //        //                where c.RoleID == pRoleID
        //        //                select c).ToList();
        //        //}
        //    }
        //    catch { throw; }
        //    finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
        //    return cObjList;
        //}


        public static int Insert(dcRoleMenu cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcRoleMenu cObj, DBContext dc)
        {
            bool isDCInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            int id = dc.DoInsert<dcRoleMenu>(cObj, true);
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }


        public static bool Delete(int AppID, int pRoleID, int pMenuID)
        {
            return Delete(AppID, pRoleID, pMenuID, null);
        }
        public static bool Delete(int AppID, int pRoleID, int pMenuID, DBContext dc)
        {
            dcRoleMenu cObj = new dcRoleMenu();
            cObj.APPID = AppID;
            cObj.ROLEID = pRoleID;
            cObj.APPMENUID = pMenuID;
            bool isDCInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            int cnt = dc.DoDelete<dcRoleMenu>(cObj);
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }






    }
}
