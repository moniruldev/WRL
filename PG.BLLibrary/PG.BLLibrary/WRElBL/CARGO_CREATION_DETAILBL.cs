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
    public class CARGO_CREATION_DETAILBL
    {
        public static DataLoadOptions CARGO_CREATION_DETAILLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcCARGO_CREATION_DETAIL>(obj => obj.relatedclassname);
            return dlo;
        }
        public static List<dcCARGO_CREATION_DETAIL> GetCARGO_CREATION_DETAILList()
        {
            return GetCARGO_CREATION_DETAILList(null, null);
        }
        public static List<dcCARGO_CREATION_DETAIL> GetCARGO_CREATION_DETAILList(DBContext dc)
        {
            return GetCARGO_CREATION_DETAILList(null, dc);
        }
        public static List<dcCARGO_CREATION_DETAIL> GetCARGO_CREATION_DETAILList(DBQuery dbq, DBContext dc)
        {
            List<dcCARGO_CREATION_DETAIL> cObjList = new List<dcCARGO_CREATION_DETAIL>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcCARGO_CREATION_DETAIL>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcCARGO_CREATION_DETAIL GetCARGO_CREATION_DETAILByID(int pCARGO_CREATION_DETAILID)
        {
            return GetCARGO_CREATION_DETAILByID(pCARGO_CREATION_DETAILID, null);
        }
        public static dcCARGO_CREATION_DETAIL GetCARGO_CREATION_DETAILByID(int pCARGO_CREATION_DETAILID, DBContext dc)
        {
            dcCARGO_CREATION_DETAIL cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcCARGO_CREATION_DETAIL>()
                                  where c.CARGO_DETAIL_ID == pCARGO_CREATION_DETAILID
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

        public static int Insert(dcCARGO_CREATION_DETAIL cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcCARGO_CREATION_DETAIL cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcCARGO_CREATION_DETAIL>(cObj, true);
                if (id > 0) { cObj.CARGO_DETAIL_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcCARGO_CREATION_DETAIL cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcCARGO_CREATION_DETAIL cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcCARGO_CREATION_DETAIL>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pCARGO_CREATION_DETAILID)
        {
            return Delete(pCARGO_CREATION_DETAILID, null);
        }
        public static bool Delete(int pCARGO_CREATION_DETAILID, DBContext dc)
        {
            dcCARGO_CREATION_DETAIL cObj = new dcCARGO_CREATION_DETAIL();
            cObj.CARGO_DETAIL_ID = pCARGO_CREATION_DETAILID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcCARGO_CREATION_DETAIL>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcCARGO_CREATION_DETAIL cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcCARGO_CREATION_DETAIL cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcCARGO_CREATION_DETAIL cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcCARGO_CREATION_DETAIL cObj, DBContext dc)
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
                                newID = cObj.CARGO_DETAIL_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.CARGO_DETAIL_ID, dc))
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

        public static bool SaveList(List<dcCARGO_CREATION_DETAIL> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcCARGO_CREATION_DETAIL> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcCARGO_CREATION_DETAIL oDet in detList)
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
                    //    bool d = Delete(oDet.CARGO_CREATION_DETAILID, dc);
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
