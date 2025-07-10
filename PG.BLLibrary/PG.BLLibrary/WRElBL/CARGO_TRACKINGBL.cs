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
    public class CARGO_TRACKINGBL
    {
        public static DataLoadOptions CARGO_TRACKINGLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcCARGO_TRACKING>(obj => obj.relatedclassname);
            return dlo;
        }
        public static List<dcCARGO_TRACKING> GetCARGO_TRACKINGList()
        {
            return GetCARGO_TRACKINGList(null, null);
        }
        public static List<dcCARGO_TRACKING> GetCARGO_TRACKINGList(DBContext dc)
        {
            return GetCARGO_TRACKINGList(null, dc);
        }
        public static List<dcCARGO_TRACKING> GetCARGO_TRACKINGList(DBQuery dbq, DBContext dc)
        {
            List<dcCARGO_TRACKING> cObjList = new List<dcCARGO_TRACKING>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcCARGO_TRACKING>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcCARGO_TRACKING GetCARGO_TRACKINGByID(int pCARGO_TRACKINGID)
        {
            return GetCARGO_TRACKINGByID(pCARGO_TRACKINGID, null);
        }
        public static dcCARGO_TRACKING GetCARGO_TRACKINGByID(int pCARGO_TRACKINGID, DBContext dc)
        {
            dcCARGO_TRACKING cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcCARGO_TRACKING>()
                                  where c.CARGO_TRACK_ID == pCARGO_TRACKINGID
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

        public static int Insert(dcCARGO_TRACKING cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcCARGO_TRACKING cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcCARGO_TRACKING>(cObj, true);
                if (id > 0) { cObj.CARGO_TRACK_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcCARGO_TRACKING cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcCARGO_TRACKING cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcCARGO_TRACKING>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pCARGO_TRACKINGID)
        {
            return Delete(pCARGO_TRACKINGID, null);
        }
        public static bool Delete(int pCARGO_TRACKINGID, DBContext dc)
        {
            dcCARGO_TRACKING cObj = new dcCARGO_TRACKING();
            cObj.CARGO_TRACK_ID = pCARGO_TRACKINGID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcCARGO_TRACKING>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcCARGO_TRACKING cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcCARGO_TRACKING cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcCARGO_TRACKING cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcCARGO_TRACKING cObj, DBContext dc)
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
                                newID = cObj.CARGO_TRACK_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.CARGO_TRACK_ID, dc))
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

        public static bool SaveList(List<dcCARGO_TRACKING> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcCARGO_TRACKING> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcCARGO_TRACKING oDet in detList)
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
                    //    bool d = Delete(oDet.CARGO_TRACKINGID, dc);
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
