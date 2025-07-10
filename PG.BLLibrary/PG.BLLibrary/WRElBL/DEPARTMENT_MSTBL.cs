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
    public class DEPARTMENT_MSTBL
    {
        public static DataLoadOptions DEPARTMENT_MSTLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcDEPARTMENT_MST>(obj => obj.relatedclassname);
            return dlo;
        }
        public static List<dcDEPARTMENT_MST> GetDEPARTMENT_MSTList()
        {
            return GetDEPARTMENT_MSTList(null, null);
        }
        public static List<dcDEPARTMENT_MST> GetDEPARTMENT_MSTList(DBContext dc)
        {
            return GetDEPARTMENT_MSTList(null, dc);
        }
        public static List<dcDEPARTMENT_MST> GetDEPARTMENT_MSTList(DBQuery dbq, DBContext dc)
        {
            List<dcDEPARTMENT_MST> cObjList = new List<dcDEPARTMENT_MST>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcDEPARTMENT_MST>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcDEPARTMENT_MST GetDEPARTMENT_MSTByID(int pDEPARTMENT_MSTID)
        {
            return GetDEPARTMENT_MSTByID(pDEPARTMENT_MSTID, null);
        }
        public static dcDEPARTMENT_MST GetDEPARTMENT_MSTByID(int pDEPARTMENT_MSTID, DBContext dc)
        {
            dcDEPARTMENT_MST cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcDEPARTMENT_MST>()
                                  where c.DEPT_ID == pDEPARTMENT_MSTID
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

        public static int Insert(dcDEPARTMENT_MST cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcDEPARTMENT_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcDEPARTMENT_MST>(cObj, true);
                if (id > 0) { cObj.DEPT_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcDEPARTMENT_MST cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcDEPARTMENT_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcDEPARTMENT_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pDEPARTMENT_MSTID)
        {
            return Delete(pDEPARTMENT_MSTID, null);
        }
        public static bool Delete(int pDEPARTMENT_MSTID, DBContext dc)
        {
            dcDEPARTMENT_MST cObj = new dcDEPARTMENT_MST();
            cObj.DEPT_ID = pDEPARTMENT_MSTID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcDEPARTMENT_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcDEPARTMENT_MST cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcDEPARTMENT_MST cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcDEPARTMENT_MST cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcDEPARTMENT_MST cObj, DBContext dc)
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
                                newID = cObj.DEPT_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.DEPT_ID, dc))
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

        public static bool SaveList(List<dcDEPARTMENT_MST> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcDEPARTMENT_MST> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcDEPARTMENT_MST oDet in detList)
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
                    //    bool d = Delete(oDet.DEPARTMENT_MSTID, dc);
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
