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
    public class ROUTE_DETAILBL
    {
        public static DataLoadOptions ROUTE_DETAILLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcROUTE_DETAIL>(obj => obj.relatedclassname);
            return dlo;
        }
        public static List<dcROUTE_DETAIL> GetROUTE_DETAILList()
        {
            return GetROUTE_DETAILList(null, null);
        }
        public static List<dcROUTE_DETAIL> GetROUTE_DETAILList(DBContext dc)
        {
            return GetROUTE_DETAILList(null, dc);
        }
        public static List<dcROUTE_DETAIL> GetROUTE_DETAILList(DBQuery dbq, DBContext dc)
        {
            List<dcROUTE_DETAIL> cObjList = new List<dcROUTE_DETAIL>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcROUTE_DETAIL>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcROUTE_DETAIL GetROUTE_DETAILByID(int pROUTE_DETAILID)
        {
            return GetROUTE_DETAILByID(pROUTE_DETAILID, null);
        }
        public static dcROUTE_DETAIL GetROUTE_DETAILByID(int pROUTE_DETAILID, DBContext dc)
        {
            dcROUTE_DETAIL cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcROUTE_DETAIL>()
                                  where c.ROUTE_DETAIL_ID == pROUTE_DETAILID
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

        public static int Insert(dcROUTE_DETAIL cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcROUTE_DETAIL cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcROUTE_DETAIL>(cObj, true);
                if (id > 0) { cObj.ROUTE_DETAIL_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcROUTE_DETAIL cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcROUTE_DETAIL cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcROUTE_DETAIL>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pROUTE_DETAILID)
        {
            return Delete(pROUTE_DETAILID, null);
        }
        public static bool Delete(int pROUTE_DETAILID, DBContext dc)
        {
            dcROUTE_DETAIL cObj = new dcROUTE_DETAIL();
            cObj.ROUTE_DETAIL_ID = pROUTE_DETAILID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcROUTE_DETAIL>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcROUTE_DETAIL cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcROUTE_DETAIL cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcROUTE_DETAIL cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcROUTE_DETAIL cObj, DBContext dc)
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
                                newID = cObj.ROUTE_DETAIL_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.ROUTE_DETAIL_ID, dc))
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

        public static bool SaveList(List<dcROUTE_DETAIL> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcROUTE_DETAIL> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcROUTE_DETAIL oDet in detList)
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
                    //    bool d = Delete(oDet.ROUTE_DETAILID, dc);
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
