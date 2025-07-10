using PG.Core.DBBase;
using PG.DBClass.SecurityDC;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;

namespace PG.BLLibrary.SecurityBL
{
    public class DATATRANSFER_TABLE_LOGBL
    {
        public static DataLoadOptions DATATRANSFER_TABLE_LOGLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcDATATRANSFER_TABLE_LOG>(obj => obj.relatedclassname);
            return dlo;
        }
        public static List<dcDATATRANSFER_TABLE_LOG> GetDATATRANSFER_TABLE_LOGList()
        {
            return GetDATATRANSFER_TABLE_LOGList(null, null);
        }
        public static List<dcDATATRANSFER_TABLE_LOG> GetDATATRANSFER_TABLE_LOGList(DBContext dc)
        {
            return GetDATATRANSFER_TABLE_LOGList(null, dc);
        }
        public static List<dcDATATRANSFER_TABLE_LOG> GetDATATRANSFER_TABLE_LOGList(DBQuery dbq, DBContext dc)
        {
            List<dcDATATRANSFER_TABLE_LOG> cObjList = new List<dcDATATRANSFER_TABLE_LOG>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcDATATRANSFER_TABLE_LOG>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcDATATRANSFER_TABLE_LOG GetDATATRANSFER_TABLE_LOGByID(int pDATATRANSFER_TABLE_LOGID)
        {
            return GetDATATRANSFER_TABLE_LOGByID(pDATATRANSFER_TABLE_LOGID, null);
        }
        public static dcDATATRANSFER_TABLE_LOG GetDATATRANSFER_TABLE_LOGByID(int pDATATRANSFER_TABLE_LOGID, DBContext dc)
        {
            dcDATATRANSFER_TABLE_LOG cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcDATATRANSFER_TABLE_LOG>()
                                  where c.ID == pDATATRANSFER_TABLE_LOGID
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

        public static int Insert(dcDATATRANSFER_TABLE_LOG cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcDATATRANSFER_TABLE_LOG cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcDATATRANSFER_TABLE_LOG>(cObj, true);
                if (id > 0) { cObj.ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcDATATRANSFER_TABLE_LOG cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcDATATRANSFER_TABLE_LOG cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcDATATRANSFER_TABLE_LOG>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pDATATRANSFER_TABLE_LOGID)
        {
            return Delete(pDATATRANSFER_TABLE_LOGID, null);
        }
        public static bool Delete(int pDATATRANSFER_TABLE_LOGID, DBContext dc)
        {
            dcDATATRANSFER_TABLE_LOG cObj = new dcDATATRANSFER_TABLE_LOG();
            cObj.ID = pDATATRANSFER_TABLE_LOGID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcDATATRANSFER_TABLE_LOG>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcDATATRANSFER_TABLE_LOG cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcDATATRANSFER_TABLE_LOG cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcDATATRANSFER_TABLE_LOG cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcDATATRANSFER_TABLE_LOG cObj, DBContext dc)
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
                                newID = cObj.ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.ID, dc))
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

        public static bool SaveList(List<dcDATATRANSFER_TABLE_LOG> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcDATATRANSFER_TABLE_LOG> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcDATATRANSFER_TABLE_LOG oDet in detList)
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
                        bool d = Delete(oDet.ID, dc);
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
