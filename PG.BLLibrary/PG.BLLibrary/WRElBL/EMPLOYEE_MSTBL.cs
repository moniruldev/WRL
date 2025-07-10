using PG.Core.DBBase;
using PG.DBClass.WRELDC;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.BLLibrary.WRElBL
{
    public class EMPLOYEE_MSTBL
    {
        public static DataLoadOptions EMPLOYEE_MSTLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcEMPLOYEE_MST>(obj => obj.relatedclassname);
            return dlo;
        }
        public static List<dcEMPLOYEE_MST> GetEMPLOYEE_MSTList()
        {
            return GetEMPLOYEE_MSTList(null, null);
        }
        public static List<dcEMPLOYEE_MST> GetEMPLOYEE_MSTList(DBContext dc)
        {
            return GetEMPLOYEE_MSTList(null, dc);
        }
        public static List<dcEMPLOYEE_MST> GetEMPLOYEE_MSTList(DBQuery dbq, DBContext dc)
        {
            List<dcEMPLOYEE_MST> cObjList = new List<dcEMPLOYEE_MST>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcEMPLOYEE_MST>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcEMPLOYEE_MST GetEMPLOYEE_MSTByID(int pEMPLOYEE_MSTID)
        {
            return GetEMPLOYEE_MSTByID(pEMPLOYEE_MSTID, null);
        }
        public static dcEMPLOYEE_MST GetEMPLOYEE_MSTByID(int pEMPLOYEE_MSTID, DBContext dc)
        {
            dcEMPLOYEE_MST cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcEMPLOYEE_MST>()
                                  where c.EMP_CODE == pEMPLOYEE_MSTID
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

        public static int Insert(dcEMPLOYEE_MST cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcEMPLOYEE_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcEMPLOYEE_MST>(cObj, true);
                if (id > 0) { cObj.EMP_CODE = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcEMPLOYEE_MST cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcEMPLOYEE_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcEMPLOYEE_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pEMPLOYEE_MSTID)
        {
            return Delete(pEMPLOYEE_MSTID, null);
        }
        public static bool Delete(int pEMPLOYEE_MSTID, DBContext dc)
        {
            dcEMPLOYEE_MST cObj = new dcEMPLOYEE_MST();
            cObj.EMP_CODE = pEMPLOYEE_MSTID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcEMPLOYEE_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcEMPLOYEE_MST cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcEMPLOYEE_MST cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcEMPLOYEE_MST cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcEMPLOYEE_MST cObj, DBContext dc)
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
                                newID = cObj.EMP_CODE;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.EMP_CODE, dc))
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

        public static bool SaveList(List<dcEMPLOYEE_MST> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcEMPLOYEE_MST> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcEMPLOYEE_MST oDet in detList)
            {
                switch (oDet._RecordState)
                {
                    //case Interwave.Core.DBClass.RecordStateEnum.Added:
                    //    int a = Insert(oDet, dc);
                    //    break;
                    //case Interwave.Core.DBClass.RecordStateEnum.Edited:
                    //    bool e = Update(oDet, dc);
                    //    break;
                    //case Interwave.Core.DBClass.RecordStateEnum.Deleted:
                    //    bool d = Delete(oDet.EMPLOYEE_MSTID, dc);
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
    }
}
