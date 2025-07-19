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
    public class CN_ASSIGNMENTBL
    {
        public static DataLoadOptions CN_ASSIGNMENTLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcCN_ASSIGNMENT>(obj => obj.relatedclassname);
            return dlo;
        }
        public static List<dcCN_ASSIGNMENT> GetCN_ASSIGNMENTList()
        {
            return GetCN_ASSIGNMENTList(null, null);
        }
        public static List<dcCN_ASSIGNMENT> GetCN_ASSIGNMENTList(DBContext dc)
        {
            return GetCN_ASSIGNMENTList(null, dc);
        }
        public static List<dcCN_ASSIGNMENT> GetCN_ASSIGNMENTList(DBQuery dbq, DBContext dc)
        {
            List<dcCN_ASSIGNMENT> cObjList = new List<dcCN_ASSIGNMENT>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcCN_ASSIGNMENT>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcCN_ASSIGNMENT GetCN_ASSIGNMENTByID(int pCN_ASSIGNMENTID)
        {
            return GetCN_ASSIGNMENTByID(pCN_ASSIGNMENTID, null);
        }
        public static dcCN_ASSIGNMENT GetCN_ASSIGNMENTByID(int pCN_ASSIGNMENTID, DBContext dc)
        {
            dcCN_ASSIGNMENT cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcCN_ASSIGNMENT>()
                                  where c.CN_ASSIGN_ID == pCN_ASSIGNMENTID
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

        public static int Insert(dcCN_ASSIGNMENT cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcCN_ASSIGNMENT cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcCN_ASSIGNMENT>(cObj, true);
                if (id > 0) { cObj.CN_ASSIGN_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcCN_ASSIGNMENT cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcCN_ASSIGNMENT cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcCN_ASSIGNMENT>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pCN_ASSIGNMENTID)
        {
            return Delete(pCN_ASSIGNMENTID, null);
        }
        public static bool Delete(int pCN_ASSIGNMENTID, DBContext dc)
        {
            dcCN_ASSIGNMENT cObj = new dcCN_ASSIGNMENT();
            cObj.CN_ASSIGN_ID = pCN_ASSIGNMENTID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcCN_ASSIGNMENT>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcCN_ASSIGNMENT cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcCN_ASSIGNMENT cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcCN_ASSIGNMENT cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcCN_ASSIGNMENT cObj, DBContext dc)
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
                                newID = cObj.CN_ASSIGN_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.CN_ASSIGN_ID, dc))
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

        public static bool SaveList(List<dcCN_ASSIGNMENT> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcCN_ASSIGNMENT> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcCN_ASSIGNMENT oDet in detList)
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
                    //    bool d = Delete(oDet.CN_ASSIGNMENTID, dc);
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
