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
    /// AppMenuBL
    /// Last update By Moni, Date 09-03-2015
    /// </summary>
    

    public class AppMenuBL
    {
        public static string GetAppMenu_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT tblAppMenu.* ");
            sb.Append(" FROM tblAppMenu ");
            sb.Append(" WHERE 1=1 ");
            return sb.ToString();
        }

        public static string GetAppMenuInfo_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT m.*, P.APPMENUNAME AppMenuNameParent ");
            sb.Append(" FROM tblAppMenu m ");
            sb.Append(" LEFT JOIN TBLAPPMENU P ON m.PARENTMENUID=P.APPMENUID ");
            sb.Append(" WHERE 1=1 ");
            return sb.ToString();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pAppID"></param>
        /// <returns></returns>
        public static List<dcAppMenu> GetAppMenuList(int pAppID)
        {

            return GetAppMenuList(pAppID, null, null);
        }

        public static List<dcAppMenu> GetAppMenuList(int pAppID, bool pMenuLinkLeadingDot)
        {
            return GetAppMenuList(pAppID, pMenuLinkLeadingDot,null);
        }
        public static List<dcAppMenu> GetAppMenuList(int pAppID, bool? pMenuLinkLeadingDot, DBContext dc)
        {
            List<dcAppMenu> cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                bool addDot = false;
                if (pMenuLinkLeadingDot.HasValue)
                {
                    addDot = pMenuLinkLeadingDot.Value;
                }
                else
                {
                    dcAppInfo appInfo = AppInfoBL.GetAppInfoByAppID(pAppID, dc);
                    if (appInfo != null)
                    {
                        addDot = appInfo.MenuLinkLeadingDot;
                    }
                }
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetAppMenu_SQLString());
                sb.Append(" AND tblAppMenu.AppID=@appID "); 
                cmdInfo.DBParametersInfo.Add("@appID", pAppID);
                
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcAppMenu>(dbq, dc).ToList();
                if (addDot)
                {
                    foreach (dcAppMenu menu in cObjList)
                    {
                        menu.AppMenuURL = "." + menu.AppMenuURL;
                    }
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcAppMenu> GetMenuItemList(DBQuery dbq, DBContext dc)
        {
            List<dcAppMenu> cObjList = new List<dcAppMenu>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                if (dbq == null)
                {
                    dbq = new DBQuery();
                }
                cObjList = DBQuery.ExecuteDBQuery<dcAppMenu>(dbq, dc).ToList();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static dcAppMenu GetAppMenuByID(int pAppMenuID)
        {
            return GetAppMenuByID(pAppMenuID, null);
        }
        public static dcAppMenu GetAppMenuByID(int pAppMenuID, DBContext dc)
        {
            dcAppMenu cObj = null;
            //List<dcAppMenu> cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetAppMenu_SQLString());
                sb.Append(" AND tblAppMenu.AppID=@AppMenuID ");
                cmdInfo.DBParametersInfo.Add("@AppMenuID", pAppMenuID);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObj = DBQuery.ExecuteDBQuery<dcAppMenu>(dbq, dc).FirstOrDefault();
                ////using (DataContext dataContext = dc.NewDataContext())
                //{
                //    var result = (from c in dataContext.GetTable<dcAppMenu>()
                //                  where c.AppMenuID == pAppMenuID
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

        public static bool IsAppMenuExists(int pAppMenuID)
        {
            return IsAppMenuExists(pAppMenuID, null);
        }
        public static bool IsAppMenuExists(int pAppMenuID, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                string sql = "select Count(AppMenuID) tcount from tblAppMenu Where AppMenuID=@AppMenuID";
                cmdInfo.DBParametersInfo.Add("@AppMenuID", pAppMenuID);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sql.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;


                int tcount =Convert.ToInt32(DBQuery.ExecuteDBScalar(dbq));
                if (tcount > 0)
                {
                    isData = true;
                }
                //cObjList = DBQuery.ExecuteDBQuery<dcAppMenu>(dbq, dc);
                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    var result = from cObj in dataContext.GetTable<dcAppMenu>()
                //                 where cObj.AppID == pAppMenuID
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
        public static bool IsAppMenuExists(int pAppMenuID, int pAppMenuIDPrev)
        {
            return IsAppMenuExists(pAppMenuID, pAppMenuIDPrev, null);
        }
        public static bool IsAppMenuExists(int pAppMenuID, int pAppMenuIDPrev, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                string sql = "select Count(AppMenuID) tcount from tblAppMenu Where AppMenuID<>@AppMenuIDPrev AND AppMenuID=@AppMenuID";
                cmdInfo.DBParametersInfo.Add("@AppMenuID", pAppMenuID);
                cmdInfo.DBParametersInfo.Add("@AppMenuIDPrev", pAppMenuID);

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
                //    var result = from cObj in dataContext.GetTable<dcAppMenu>()
                //                 where cObj.AppMenuID != pAppMenuIDPrev && cObj.AppMenuID == pAppMenuID
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


        public static int Save(dcAppMenu cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcAppMenu cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcAppMenu cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcAppMenu cObj, DBContext dc)
        {
            int newID = 0;
            bool isDCInit = false;
            bool isTransInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                isTransInit = dc.StartTransaction();
                using (DataContext dataContext = dc.NewDataContext())
                {

                    switch (cObj._RecordState)
                    {
                        case RecordStateEnum.Added:
                            newID = Insert(cObj, dc);
                            break;
                        case RecordStateEnum.Edited:
                            if (Update(cObj, dc))
                            {
                                newID = cObj.AppMenuID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.AppMenuID, dc))
                            {
                                newID = 1;
                            }
                            break;
                        default:
                            break;
                    }

                    if (newID > 0)
                    {
                        bool bStatus = false;

                        ///code list save logic here

                        bStatus = true;
                        if (bStatus)
                        {
                            dc.CommitTransaction(isTransInit);
                        }
                    }
                }
            }
            catch
            {
                dc.RollbackTransaction(isTransInit);
                throw;
            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return newID;
        }

        public static int Insert(dcAppMenu cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcAppMenu cObj, DBContext dc)
        {
            bool isDCInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            int id = dc.DoInsert<dcAppMenu>(cObj, true);
            if (id > 0) { cObj.AppMenuID = id; }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcAppMenu cObj, dcAppMenu cObjKey)
        {
            return Update(cObj, cObjKey, null);
        }

        public static bool Update(dcAppMenu cObj, dcAppMenu cObjKey , DBContext dc)
        {
            bool isDCInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            int cnt = dc.DoUpdate<dcAppMenu>(cObj, cObjKey);
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }
      

        public static bool Update(dcAppMenu cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcAppMenu>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pAppMenuID)
        {
            return Delete(pAppMenuID, null);
        }
        public static bool Delete(int pAppMenuID, DBContext dc)
        {
            dcAppMenu cObj = new dcAppMenu();
            cObj.AppMenuID = pAppMenuID;
            bool isDCInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            int cnt = dc.DoDelete<dcAppMenu>(cObj);
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static string GetMenuList_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("  SELECT M.*,P.APPMENUNAME AppMenuNameParent ");
            sb.Append("  FROM TBLAPPMENU M ");
            sb.Append("LEFT JOIN TBLAPPMENU P ON M.PARENTMENUID=P.APPMENUID   ");
            sb.Append(" WHERE 1=1   ");
            //sb.Append(" AND M.APPMENUURL ='NULL'");
            //sb.Append(" AND M.SHOWMENU = 'Y'");
           
            return sb.ToString();
        }

        public static List<dcAppMenu> GetMenuList( DBContext dc)
        {
            List<dcAppMenu> cObjList = new List<dcAppMenu>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetMenuList_SQLString());

                //if (pDeptID > 0)
                //{
                //    sb.Append(" and DEPT.DEPT_ID=@pDeptID ");
                //    cmdInfo.DBParametersInfo.Add("@pDeptID", pDeptID);
                //}
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                cObjList = DBQuery.ExecuteDBQuery<dcAppMenu>(dbq, dc).ToList();

            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static dcAppMenu GetAppMenuInfoByID(int pAppMenuID)
        {
            return GetAppMenuInfoByID(pAppMenuID, null);
        }
        public static dcAppMenu GetAppMenuInfoByID(int pAppMenuID, DBContext dc)
        {
            dcAppMenu cObj = null;
            //List<dcAppMenu> cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetAppMenuInfo_SQLString());
                sb.Append(" AND m.APPMENUID=@AppMenuID ");
                cmdInfo.DBParametersInfo.Add("@AppMenuID", pAppMenuID);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObj = DBQuery.ExecuteDBQuery<dcAppMenu>(dbq, dc).FirstOrDefault();
                ////using (DataContext dataContext = dc.NewDataContext())
                //{
                //    var result = (from c in dataContext.GetTable<dcAppMenu>()
                //                  where c.AppMenuID == pAppMenuID
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

        public static List<dcAppMenu> GetMenuList(int Pmenuid,int menuid, DBContext dc)
        {
            List<dcAppMenu> cObjList = new List<dcAppMenu>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder(GetAppMenuInfo_SQLString());

                    if (Pmenuid > 0)
                    {
                        sb.Append(" and M.PARENTMENUID=@Pmenuid ");
                        cmdInfo.DBParametersInfo.Add("@Pmenuid", Pmenuid);
                    }
                    if (menuid > 0)
                    {
                        sb.Append(" and M.AppMenuID=@menuid ");
                        cmdInfo.DBParametersInfo.Add("@menuid", menuid);
                    }

                    //if (itemId > 0)
                    //{
                    //    sb.Append(" and INV_ITEM_MASTER.ITEM_ID=@itemId ");
                    //    cmdInfo.DBParametersInfo.Add("@itemId", itemId);
                    //}


                    //if (itemName != string.Empty)
                    //{
                    //    sb.Append(" AND UPPER(INV_ITEM_MASTER.ITEM_NAME) LIKE UPPER(:itemName) ");
                    //    //cmd.Parameters.AddWithValue("@glGroupID", pGLGroupID);
                    //    cmdInfo.DBParametersInfo.Add(":itemName", '%' + itemName + '%');
                    //}


                    //if (itemsnsid > 0)
                    //{
                    //    sb.Append(" AND INV_ITEM_MASTER.ITEM_SNS_ID=@itemsnsId ");
                    //    cmdInfo.DBParametersInfo.Add("@itemsnsId", itemsnsid);
                    //}
                    //sb.Append(" ORDER BY INV_ITEM_MASTER.ITEM_CODE DESC ");

                    DBQuery dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcAppMenu>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static string GetMenuSL()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("  SELECT MAX(m.APPMENUSLNO) APPMENUSLNO ");
            sb.Append("  FROM tblAppMenu m ");
            sb.Append("  WHERE 1=1   ");
           
            //sb.Append(" AND M.APPMENUURL ='NULL'");
            //sb.Append(" AND M.SHOWMENU = 'Y'");

            return sb.ToString();
        }

        public static List<dcAppMenu> GetMenuLastSL(int Pmenuid,  DBContext dc)
        {
            List<dcAppMenu> cObjList = new List<dcAppMenu>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder(GetMenuSL());

                    if (Pmenuid > 0)
                    {
                        sb.Append(" and M.PARENTMENUID=@Pmenuid ");
                        cmdInfo.DBParametersInfo.Add("@Pmenuid", Pmenuid);
                    }

                    sb.Append(" ORDER BY m.APPMENUSLNO   ");
                    DBQuery dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcAppMenu>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
    }
}
