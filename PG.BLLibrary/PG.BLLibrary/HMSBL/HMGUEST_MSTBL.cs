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
    public class HMGUEST_MSTBL
    {
        public static DataLoadOptions HMGUEST_MSTLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcHMGUEST_MST>(obj => obj.relatedclassname);
            return dlo;
        }

        public static string GetGuestListString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT G.*,R.CHECK_IN,R.CHECK_OUT,R.RESERVATION_ID,R.NOTE,C.COUNTRY_NAME ");
            sb.Append(" FROM HMGUEST_MST G ");
            sb.Append(" INNER JOIN HMRESERVATION_MST R ON G.GUEST_ID=R.GUEST_ID ");
            sb.Append(" INNER JOIN HMCOUNTRY_MST C ON G.COUNTRY_ID=C.COUNTRY_ID ");
            sb.Append(" WHERE 1=1 ");

            return sb.ToString();
        }

        public static dcHMGUEST_MST GetGuestInfoById(int pGuestId)
        {
            return GetGuestInfoList(pGuestId, null).FirstOrDefault();
        }

        public static List<dcHMGUEST_MST> GetGuestInfoList()
        {
            return GetGuestInfoList(0, null);
        }

        public static List<dcHMGUEST_MST> GetGuestInfoList(int pGuestId, DBContext dc)
        {
            List<dcHMGUEST_MST> cObjList = new List<dcHMGUEST_MST>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetGuestListString());
                if (pGuestId > 0)
                {
                    sb.Append(" AND G.GUEST_ID= @pGuestId ");
                    cmdInfo.DBParametersInfo.Add("@pGuestId", pGuestId);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcHMGUEST_MST>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcHMGUEST_MST> GetGuestInfoList(dcPrmHMS prmHms, DBContext dc)
        {
            List<dcHMGUEST_MST> cObjList = new List<dcHMGUEST_MST>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetGuestListString());
                if (prmHms.GUEST_ID > 0)
                {
                    sb.Append(" AND G.GUEST_ID= @pGuestId ");
                    cmdInfo.DBParametersInfo.Add("@pGuestId", prmHms.GUEST_ID);
                }

                if (prmHms.GUEST_NAME != "")
                {
                    sb.Append(" AND UPPER(G.GUEST_NAME) LIKE UPPER('%" + prmHms.GUEST_NAME+ "%') ");
                }

                if (prmHms.MOBILE_NO != "")
                {
                    sb.Append(" AND G.MOBILE_NO LIKE ('%" + prmHms.MOBILE_NO + "%') ");
                }

                if (prmHms.IDENTITY_NO != "")
                {
                    sb.Append(" AND UPPER(G.PASSPORT_NO) LIKE UPPER('%" + prmHms.IDENTITY_NO + "%') ");
                }

                if(prmHms.FROM_DATE.HasValue)
                {
                    if(prmHms.TO_DATE.HasValue)
                    {
                        sb.Append(" AND (R.CHECK_IN BETWEEN @fromDate AND @toDate) ");
                        cmdInfo.DBParametersInfo.Add("@fromDate", prmHms.FROM_DATE.Value);
                        cmdInfo.DBParametersInfo.Add("@toDate", prmHms.TO_DATE.Value);
                    }

                }

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcHMGUEST_MST>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static List<dcHMGUEST_MST> GetHMGUEST_MSTList()
        {
            return GetHMGUEST_MSTList(null, null);
        }
        public static List<dcHMGUEST_MST> GetHMGUEST_MSTList(DBContext dc)
        {
            return GetHMGUEST_MSTList(null, dc);
        }
        public static List<dcHMGUEST_MST> GetHMGUEST_MSTList(DBQuery dbq, DBContext dc)
        {
            List<dcHMGUEST_MST> cObjList = new List<dcHMGUEST_MST>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcHMGUEST_MST>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcHMGUEST_MST GetHMGUEST_MSTByID(int pGUEST_ID)
        {
            return GetHMGUEST_MSTByID(pGUEST_ID, null);
        }
        public static dcHMGUEST_MST GetHMGUEST_MSTByID(int pGUEST_ID, DBContext dc)
        {
            dcHMGUEST_MST cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcHMGUEST_MST>()
                                  where c.GUEST_ID == pGUEST_ID
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

        public static int Insert(dcHMGUEST_MST cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcHMGUEST_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcHMGUEST_MST>(cObj, true);
                if (id > 0) { cObj.GUEST_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcHMGUEST_MST cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcHMGUEST_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcHMGUEST_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pGUEST_ID)
        {
            return Delete(pGUEST_ID, null);
        }
        public static bool Delete(int pGUEST_ID, DBContext dc)
        {
            dcHMGUEST_MST cObj = new dcHMGUEST_MST();
            cObj.GUEST_ID = pGUEST_ID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcHMGUEST_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcHMGUEST_MST cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcHMGUEST_MST cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcHMGUEST_MST cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcHMGUEST_MST cObj, DBContext dc)
        {
            int newID = 0;
            int ReservationId = 0;
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
                                newID = cObj.GUEST_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.GUEST_ID, dc))
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

                      
                            if(cObj.objReservationMst != null)
                            {
                                cObj.objReservationMst.GUEST_ID = newID;
                                cObj.objReservationMst._RecordState = cObj._RecordState;
                                ReservationId = HMRESERVATION_MSTBL.Save(cObj.objReservationMst);

                            }
                            if (cObj.ReservationDtlList != null)
                            {
                                foreach (dcHMRESERVATION_DTL det in cObj.ReservationDtlList)
                                {
                                    det._RecordState = cObj._RecordState;
                                    det.RESERVATION_ID = ReservationId;
                                }
                                bStatus = HMRESERVATION_DTLBL.SaveList(cObj.ReservationDtlList, dc);

                            }

                        //bStatus = true;
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

        public static bool SaveList(List<dcHMGUEST_MST> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcHMGUEST_MST> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcHMGUEST_MST oDet in detList)
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
                        bool d = Delete(oDet.GUEST_ID, dc);
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
