using PG.Core.DBBase;
using PG.DBClass.HMSDC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.BLLibrary.HMSBL
{
    public class HMROOMBL
    {
        public static DataLoadOptions HMROOMLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcHMROOM>(obj => obj.relatedclassname);
            return dlo;
        }

        public static string GetRoomSQLString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT R.*,RT.TITLE ROOM_TYPE_NAME,F.FLOOR_NAME,ST.ROOM_STATUS ");
            sb.Append(" FROM HMROOM R ");
            sb.Append(" INNER JOIN HMROOM_TYPE RT ON R.ROOM_TYPE_ID=RT.ROOM_TYPE_ID "); 
            sb.Append(" LEFT JOIN HMFLOOR_MST F ON R.FLOOR_ID=F.FLOOR_ID ");
            sb.Append(" LEFT JOIN HMROOM_STATUS ST ON R.ROOM_STATUS_ID=ST.ROOM_STATUS_ID ");
            sb.Append(" WHERE 1=1 ");

            return sb.ToString();
        }

        public static string GetRoomListString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT RT.TITLE ROOM_TYPE_NAME,RT.DESCRIPTION ROOM_DESCRIPTION,RT.DISCOUNTED_RATE,RT.MAX_PERSON,RT.NORMAL_RATE,COUNT(R.ROOM_ID) NO_OF_ROOM ");
            sb.Append(" FROM HMROOM R ");
            sb.Append(" INNER JOIN HMROOM_TYPE RT ON R.ROOM_TYPE_ID=RT.ROOM_TYPE_ID ");
            sb.Append(" WHERE R.IS_ACTIVE='Y' ");
            sb.Append(" GROUP BY RT.ROOM_TYPE_ID,RT.TITLE,RT.DESCRIPTION,RT.DISCOUNTED_RATE,RT.MAX_PERSON,RT.NORMAL_RATE ");

            return sb.ToString();
        }

        public static string GetRoomStatusSQLString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT ST.* FROM HMROOM_STATUS ST ");
            sb.Append(" WHERE 1=1 ");
            sb.Append(" AND ST.IS_ACTIVE ='Y' ");

            return sb.ToString();
        }

        public static dcHMROOM GetRoomInfoById(int pRoomId)
        {
            return GetRoomInfoListById(pRoomId,null).FirstOrDefault();
        }

        public static List<dcHMROOM> GetRoomInfoListById(int pRoomId, DBContext dc)
        {
            List<dcHMROOM> cObjList = new List<dcHMROOM>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetRoomSQLString());
                if (pRoomId > 0)
                {
                    sb.Append(" AND R.ROOM_ID= @pRoomId ");
                    cmdInfo.DBParametersInfo.Add("@pRoomId", pRoomId);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcHMROOM>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcHMROOM> GetRoomInfoList(int pRoomId)
        {
            return GetRoomInfoList(pRoomId, null);
        }

        public static List<dcHMROOM> GetRoomInfoList(int pRoomId, DBContext dc)
        {
            List<dcHMROOM> cObjList = new List<dcHMROOM>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetRoomListString());
                if (pRoomId > 0)
                {
                    sb.Append(" AND R.ROOM_ID= @pRoomId ");
                    cmdInfo.DBParametersInfo.Add("@pRoomId", pRoomId);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcHMROOM>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcHMROOM> GetRoomList(dcPrmHMS prmHms, DBContext dc)
        {
            List<dcHMROOM> cObjList = new List<dcHMROOM>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetRoomSQLString());
                if (prmHms.TRANS_ID > 0)
                {
                    sb.Append(" AND RT.ROOM_TYPE_ID= @TRANS_ID ");
                    cmdInfo.DBParametersInfo.Add("@TRANS_ID", prmHms.TRANS_ID);
                }

                if (prmHms.TITLE != "")
                {
                    sb.Append(" AND UPPER(RT.TITLE) LIKE UPPER('%" + prmHms.TITLE + "%') ");
                }

                if (prmHms.IS_ACTIVE != "0")
                {
                    sb.Append(" AND R.IS_ACTIVE= @IS_ACTIVE ");
                    cmdInfo.DBParametersInfo.Add("@IS_ACTIVE", prmHms.IS_ACTIVE);
                }


                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcHMROOM>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcHMROOM> GetRoomStatusList()
        {
            return GetRoomStatusList(null);
        }

        public static List<dcHMROOM> GetRoomStatusList(DBContext dc)
        {
            List<dcHMROOM> cObjList = new List<dcHMROOM>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetRoomStatusSQLString());
            
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcHMROOM>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcHMROOM> GetHMROOMList()
        {
            return GetHMROOMList(null, null);
        }
        public static List<dcHMROOM> GetHMROOMList(DBContext dc)
        {
            return GetHMROOMList(null, dc);
        }
        public static List<dcHMROOM> GetHMROOMList(DBQuery dbq, DBContext dc)
        {
            List<dcHMROOM> cObjList = new List<dcHMROOM>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    if (dbq == null)
                    {
                        dbq = new DBQuery();
                        //dbq.OrderBy = "YearStartDate Desc";
                    }
                    cObjList = DBQuery.ExecuteDBQuery<dcHMROOM>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcHMROOM GetHMROOMByID(int pHMROOMID)
        {
            return GetHMROOMByID(pHMROOMID, null);
        }
        public static dcHMROOM GetHMROOMByID(int pHMROOMID, DBContext dc)
        {
            dcHMROOM cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcHMROOM>()
                                  where c.ROOM_ID == pHMROOMID
                                  select c).ToList();
                    if (result.Count() > 0)
                    {
                        cObj = result.First();
                    }
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObj;
        }

        public static int Insert(dcHMROOM cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcHMROOM cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcHMROOM>(cObj, true);
                if (id > 0) { cObj.ROOM_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcHMROOM cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcHMROOM cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcHMROOM>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pHMROOMID)
        {
            return Delete(pHMROOMID, null);
        }
        public static bool Delete(int pHMROOMID, DBContext dc)
        {
            dcHMROOM cObj = new dcHMROOM();
            cObj.ROOM_ID = pHMROOMID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcHMROOM>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcHMROOM cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcHMROOM cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcHMROOM cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcHMROOM cObj, DBContext dc)
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
                                newID = cObj.ROOM_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.ROOM_ID, dc))
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

        public static bool SaveList(List<dcHMROOM> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcHMROOM> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcHMROOM oDet in detList)
            {
                switch (oDet._RecordState)
                {
                    case RecordStateEnum.Added:
                        int a = Insert(oDet, dc);
                        break;
                    case RecordStateEnum.Edited:
                        bool e = Update(oDet, dc);
                        break;
                    case RecordStateEnum.Deleted:
                        bool d = Delete(oDet.ROOM_ID, dc);
                        break;
                    default:
                        break;
                }
            }
            dc.CommitTransaction(isTransInit);
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            bStatus = true;
            return bStatus;
        }
    }
}
