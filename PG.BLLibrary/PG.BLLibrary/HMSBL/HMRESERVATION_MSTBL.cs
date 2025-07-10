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
    public class HMRESERVATION_MSTBL
    {
        public static DataLoadOptions HMRESERVATION_MSTLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcHMRESERVATION_MST>(obj => obj.relatedclassname);
            return dlo;
        }

        public static string GetResarvationMstSQLString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT * FROM HMRESERVATION_MST MST ");
            sb.Append(" WHERE 1=1 ");

            return sb.ToString();
        }

        public static dcHMRESERVATION_MST GetResarvationMstById(int pReservationId)
        {
            return GetResarvationMstById(pReservationId, null);
        }

        public static dcHMRESERVATION_MST GetResarvationMstById(int pReservationId, DBContext dc)
        {
            dcHMRESERVATION_MST cObj = new dcHMRESERVATION_MST();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetResarvationMstSQLString());
                if (pReservationId > 0)
                {
                    sb.Append(" AND MST.RESERVATION_ID= @pReservationId ");
                    cmdInfo.DBParametersInfo.Add("@pReservationId", pReservationId);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObj = DBQuery.ExecuteDBQuery<dcHMRESERVATION_MST>(dbq, dc).FirstOrDefault();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObj;
        }
        public static List<dcHMRESERVATION_MST> GetHMRESERVATION_MSTList()
        {
            return GetHMRESERVATION_MSTList(null, null);
        }
        public static List<dcHMRESERVATION_MST> GetHMRESERVATION_MSTList(DBContext dc)
        {
            return GetHMRESERVATION_MSTList(null, dc);
        }
        public static List<dcHMRESERVATION_MST> GetHMRESERVATION_MSTList(DBQuery dbq, DBContext dc)
        {
            List<dcHMRESERVATION_MST> cObjList = new List<dcHMRESERVATION_MST>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcHMRESERVATION_MST>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcHMRESERVATION_MST GetHMRESERVATION_MSTByID(int pRESERVATION_ID)
        {
            return GetHMRESERVATION_MSTByID(pRESERVATION_ID, null);
        }
        public static dcHMRESERVATION_MST GetHMRESERVATION_MSTByID(int pRESERVATION_ID, DBContext dc)
        {
            dcHMRESERVATION_MST cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcHMRESERVATION_MST>()
                                  where c.RESERVATION_ID == pRESERVATION_ID
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

        public static int Insert(dcHMRESERVATION_MST cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcHMRESERVATION_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcHMRESERVATION_MST>(cObj, true);
                if (id > 0) { cObj.RESERVATION_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcHMRESERVATION_MST cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcHMRESERVATION_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcHMRESERVATION_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pRESERVATION_ID)
        {
            return Delete(pRESERVATION_ID, null);
        }
        public static bool Delete(int pRESERVATION_ID, DBContext dc)
        {
            dcHMRESERVATION_MST cObj = new dcHMRESERVATION_MST();
            cObj.RESERVATION_ID = pRESERVATION_ID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcHMRESERVATION_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcHMRESERVATION_MST cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcHMRESERVATION_MST cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcHMRESERVATION_MST cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcHMRESERVATION_MST cObj, DBContext dc)
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
                                newID = cObj.RESERVATION_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.RESERVATION_ID, dc))
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

        public static bool SaveList(List<dcHMRESERVATION_MST> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcHMRESERVATION_MST> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcHMRESERVATION_MST oDet in detList)
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
                        bool d = Delete(oDet.RESERVATION_ID, dc);
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
