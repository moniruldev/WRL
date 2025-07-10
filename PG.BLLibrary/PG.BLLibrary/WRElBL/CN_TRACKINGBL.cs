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
    public class CN_TRACKINGBL
    {
        public static DataLoadOptions CN_TRACKINGLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcCN_TRACKING>(obj => obj.relatedclassname);
            return dlo;
        }
        public static List<dcCN_TRACKING> GetCN_TRACKINGList()
        {
            return GetCN_TRACKINGList(null, null);
        }
        public static List<dcCN_TRACKING> GetCN_TRACKINGList(DBContext dc)
        {
            return GetCN_TRACKINGList(null, dc);
        }
        public static List<dcCN_TRACKING> GetCN_TRACKINGList(DBQuery dbq, DBContext dc)
        {
            List<dcCN_TRACKING> cObjList = new List<dcCN_TRACKING>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcCN_TRACKING>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcCN_TRACKING GetCN_TRACKINGByID(int pCN_TRACKINGID)
        {
            return GetCN_TRACKINGByID(pCN_TRACKINGID, null);
        }
        public static dcCN_TRACKING GetCN_TRACKINGByID(int pCN_TRACKINGID, DBContext dc)
        {
            dcCN_TRACKING cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcCN_TRACKING>()
                                  where c.CN_TRACK_ID == pCN_TRACKINGID
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

        public static int Insert(dcCN_TRACKING cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcCN_TRACKING cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcCN_TRACKING>(cObj, true);
                if (id > 0) { cObj.CN_TRACK_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcCN_TRACKING cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcCN_TRACKING cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcCN_TRACKING>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pCN_TRACKINGID)
        {
            return Delete(pCN_TRACKINGID, null);
        }
        public static bool Delete(int pCN_TRACKINGID, DBContext dc)
        {
            dcCN_TRACKING cObj = new dcCN_TRACKING();
            cObj.CN_TRACK_ID = pCN_TRACKINGID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcCN_TRACKING>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcCN_TRACKING cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcCN_TRACKING cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcCN_TRACKING cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcCN_TRACKING cObj, DBContext dc)
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
                                newID = cObj.CN_TRACK_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.CN_TRACK_ID, dc))
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

        public static bool SaveList(List<dcCN_TRACKING> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcCN_TRACKING> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcCN_TRACKING oDet in detList)
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
                    //    bool d = Delete(oDet.CN_TRACKINGID, dc);
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
