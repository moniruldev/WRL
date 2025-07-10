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
    public class HMRESERVATION_DTLBL
    {
        public static DataLoadOptions HMRESERVATION_DTLLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcHMRESERVATION_DTL>(obj => obj.relatedclassname);
            return dlo;
        }

        public static string GetRoomListString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT RT.ROOM_TYPE_ID,RT.TITLE ROOM_TYPE_NAME,RT.DESCRIPTION ROOM_DESCRIPTION,RT.DISCOUNTED_RATE,RT.MAX_PERSON,RT.NORMAL_RATE,COUNT(R.ROOM_ID) NO_OF_ROOM ");
            sb.Append(" FROM HMROOM R ");
            sb.Append(" INNER JOIN HMROOM_TYPE RT ON R.ROOM_TYPE_ID=RT.ROOM_TYPE_ID ");
            sb.Append(" WHERE R.IS_ACTIVE='Y' ");
            sb.Append(" GROUP BY RT.ROOM_TYPE_ID,RT.TITLE,RT.DESCRIPTION,RT.DISCOUNTED_RATE,RT.MAX_PERSON,RT.NORMAL_RATE ");

            return sb.ToString();
        }

        public static string GetResarvationDtlSQLString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT DTL.*,RT.TITLE ROOM_TYPE_NAME,RT.DESCRIPTION ROOM_DESCRIPTION,RT.DISCOUNTED_RATE,RT.MAX_PERSON,RT.NORMAL_RATE ");
            sb.Append(" ,(SELECT COUNT(R.ROOM_ID) from HMROOM R where R.ROOM_TYPE_ID=DTL.ROOM_TYPE_ID) NO_OF_ROOM ");
            sb.Append(" FROM HMRESERVATION_DTL DTL ");
            sb.Append(" INNER JOIN HMRESERVATION_MST MST ON DTL.RESERVATION_ID=MST.RESERVATION_ID ");
            sb.Append(" INNER JOIN HMROOM_TYPE RT ON DTL.ROOM_TYPE_ID=RT.ROOM_TYPE_ID ");
            sb.Append(" WHERE 1=1 ");

            return sb.ToString();
        }

        public static List<dcHMRESERVATION_DTL> GetRoomInfoList()
        {
            return GetRoomInfoList(0);
        }

        public static List<dcHMRESERVATION_DTL> GetRoomInfoList(int pRoomId)
        {
            return GetRoomInfoList(pRoomId, null);
        }

        public static List<dcHMRESERVATION_DTL> GetRoomInfoList(int pRoomId, DBContext dc)
        {
            List<dcHMRESERVATION_DTL> cObjList = new List<dcHMRESERVATION_DTL>();
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

                cObjList = DBQuery.ExecuteDBQuery<dcHMRESERVATION_DTL>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcHMRESERVATION_DTL> GetResarvationInfoListById(int pReservationId)
        {
            return GetResarvationInfoListById(pReservationId, null);
        }

        public static List<dcHMRESERVATION_DTL> GetResarvationInfoListById(int pReservationId, DBContext dc)
        {
            List<dcHMRESERVATION_DTL> cObjList = new List<dcHMRESERVATION_DTL>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetResarvationDtlSQLString());
                if (pReservationId > 0)
                {
                    sb.Append(" AND DTL.RESERVATION_ID= @pReservationId ");
                    cmdInfo.DBParametersInfo.Add("@pReservationId", pReservationId);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcHMRESERVATION_DTL>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static List<dcHMRESERVATION_DTL> GetHMRESERVATION_DTLList()
        {
            return GetHMRESERVATION_DTLList(null, null);
        }
        public static List<dcHMRESERVATION_DTL> GetHMRESERVATION_DTLList(DBContext dc)
        {
            return GetHMRESERVATION_DTLList(null, dc);
        }
        public static List<dcHMRESERVATION_DTL> GetHMRESERVATION_DTLList(DBQuery dbq, DBContext dc)
        {
            List<dcHMRESERVATION_DTL> cObjList = new List<dcHMRESERVATION_DTL>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcHMRESERVATION_DTL>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcHMRESERVATION_DTL GetHMRESERVATION_DTLByID(int pRESERVATION_DTL_ID)
        {
            return GetHMRESERVATION_DTLByID(pRESERVATION_DTL_ID, null);
        }
        public static dcHMRESERVATION_DTL GetHMRESERVATION_DTLByID(int pRESERVATION_DTL_ID, DBContext dc)
        {
            dcHMRESERVATION_DTL cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcHMRESERVATION_DTL>()
                                  where c.RESERVATION_DTL_ID == pRESERVATION_DTL_ID
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

        public static int Insert(dcHMRESERVATION_DTL cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcHMRESERVATION_DTL cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcHMRESERVATION_DTL>(cObj, true);
                if (id > 0) { cObj.RESERVATION_DTL_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcHMRESERVATION_DTL cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcHMRESERVATION_DTL cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcHMRESERVATION_DTL>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pRESERVATION_DTL_ID)
        {
            return Delete(pRESERVATION_DTL_ID, null);
        }
        public static bool Delete(int pRESERVATION_DTL_ID, DBContext dc)
        {
            dcHMRESERVATION_DTL cObj = new dcHMRESERVATION_DTL();
            cObj.RESERVATION_DTL_ID = pRESERVATION_DTL_ID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcHMRESERVATION_DTL>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcHMRESERVATION_DTL cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcHMRESERVATION_DTL cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcHMRESERVATION_DTL cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcHMRESERVATION_DTL cObj, DBContext dc)
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
                                newID = cObj.RESERVATION_DTL_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.RESERVATION_DTL_ID, dc))
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

        public static bool SaveList(List<dcHMRESERVATION_DTL> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcHMRESERVATION_DTL> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcHMRESERVATION_DTL oDet in detList)
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
                        bool d = Delete(oDet.RESERVATION_DTL_ID, dc);
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
