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
    public class REFUND_CAUSE_MSTBL
    {
        public static DataLoadOptions REFUND_CAUSE_MSTLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcREFUND_CAUSE_MST>(obj => obj.relatedclassname);
            return dlo;
        }
        public static List<dcREFUND_CAUSE_MST> GetREFUND_CAUSE_MSTList()
        {
            return GetREFUND_CAUSE_MSTList(null, null);
        }
        public static List<dcREFUND_CAUSE_MST> GetREFUND_CAUSE_MSTList(DBContext dc)
        {
            return GetREFUND_CAUSE_MSTList(null, dc);
        }
        public static List<dcREFUND_CAUSE_MST> GetREFUND_CAUSE_MSTList(DBQuery dbq, DBContext dc)
        {
            List<dcREFUND_CAUSE_MST> cObjList = new List<dcREFUND_CAUSE_MST>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcREFUND_CAUSE_MST>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcREFUND_CAUSE_MST GetREFUND_CAUSE_MSTByID(int pREFUND_CAUSE_MSTID)
        {
            return GetREFUND_CAUSE_MSTByID(pREFUND_CAUSE_MSTID, null);
        }
        public static dcREFUND_CAUSE_MST GetREFUND_CAUSE_MSTByID(int pREFUND_CAUSE_MSTID, DBContext dc)
        {
            dcREFUND_CAUSE_MST cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcREFUND_CAUSE_MST>()
                                  where c.REFUND_CAUSE_ID == pREFUND_CAUSE_MSTID
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

        public static int Insert(dcREFUND_CAUSE_MST cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcREFUND_CAUSE_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcREFUND_CAUSE_MST>(cObj, true);
                if (id > 0) { cObj.REFUND_CAUSE_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcREFUND_CAUSE_MST cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcREFUND_CAUSE_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcREFUND_CAUSE_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pREFUND_CAUSE_MSTID)
        {
            return Delete(pREFUND_CAUSE_MSTID, null);
        }
        public static bool Delete(int pREFUND_CAUSE_MSTID, DBContext dc)
        {
            dcREFUND_CAUSE_MST cObj = new dcREFUND_CAUSE_MST();
            cObj.REFUND_CAUSE_ID = pREFUND_CAUSE_MSTID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcREFUND_CAUSE_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcREFUND_CAUSE_MST cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcREFUND_CAUSE_MST cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcREFUND_CAUSE_MST cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcREFUND_CAUSE_MST cObj, DBContext dc)
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
                                newID = cObj.REFUND_CAUSE_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.REFUND_CAUSE_ID, dc))
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

        public static bool SaveList(List<dcREFUND_CAUSE_MST> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcREFUND_CAUSE_MST> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcREFUND_CAUSE_MST oDet in detList)
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
                    //    bool d = Delete(oDet.REFUND_CAUSE_MSTID, dc);
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
