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
    public class AGREEMENT_DETAILLBL
    {
        public static DataLoadOptions AGREEMENT_DETAILLLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcAGREEMENT_DETAILL>(obj => obj.relatedclassname);
            return dlo;
        }
        public static List<dcAGREEMENT_DETAILL> GetAGREEMENT_DETAILLList()
        {
            return GetAGREEMENT_DETAILLList(null, null);
        }
        public static List<dcAGREEMENT_DETAILL> GetAGREEMENT_DETAILLList(DBContext dc)
        {
            return GetAGREEMENT_DETAILLList(null, dc);
        }
        public static List<dcAGREEMENT_DETAILL> GetAGREEMENT_DETAILLList(DBQuery dbq, DBContext dc)
        {
            List<dcAGREEMENT_DETAILL> cObjList = new List<dcAGREEMENT_DETAILL>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcAGREEMENT_DETAILL>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcAGREEMENT_DETAILL GetAGREEMENT_DETAILLByID(int pAGREEMENT_DETAILLID)
        {
            return GetAGREEMENT_DETAILLByID(pAGREEMENT_DETAILLID, null);
        }
        public static dcAGREEMENT_DETAILL GetAGREEMENT_DETAILLByID(int pAGREEMENT_DETAILLID, DBContext dc)
        {
            dcAGREEMENT_DETAILL cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcAGREEMENT_DETAILL>()
                                  where c.AGR_DETAIL_ID == pAGREEMENT_DETAILLID
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

        public static int Insert(dcAGREEMENT_DETAILL cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcAGREEMENT_DETAILL cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcAGREEMENT_DETAILL>(cObj, true);
                if (id > 0) { cObj.AGR_DETAIL_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcAGREEMENT_DETAILL cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcAGREEMENT_DETAILL cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcAGREEMENT_DETAILL>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pAGREEMENT_DETAILLID)
        {
            return Delete(pAGREEMENT_DETAILLID, null);
        }
        public static bool Delete(int pAGREEMENT_DETAILLID, DBContext dc)
        {
            dcAGREEMENT_DETAILL cObj = new dcAGREEMENT_DETAILL();
            cObj.AGR_DETAIL_ID = pAGREEMENT_DETAILLID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcAGREEMENT_DETAILL>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcAGREEMENT_DETAILL cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcAGREEMENT_DETAILL cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcAGREEMENT_DETAILL cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcAGREEMENT_DETAILL cObj, DBContext dc)
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
                                newID = cObj.AGR_DETAIL_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.AGR_DETAIL_ID, dc))
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

        public static bool SaveList(List<dcAGREEMENT_DETAILL> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcAGREEMENT_DETAILL> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcAGREEMENT_DETAILL oDet in detList)
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
                    //    bool d = Delete(oDet.AGREEMENT_DETAILLID, dc);
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
