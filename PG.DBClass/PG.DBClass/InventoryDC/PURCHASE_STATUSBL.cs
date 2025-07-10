using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.InventoryDC
{
    public class PURCHASE_STATUSBL
    {
        public static DataLoadOptions PURCHASE_STATUSLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcPURCHASE_STATUS>(obj => obj.relatedclassname);
            return dlo;
        }
        public static string GetPurchaseStatusDetailsSQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT * FROM PURCHASE_STATUS ");
            sb.Append(" WHERE 1=1 ");
            sb.Append(" AND IS_ACTIVE='Y' ");
            sb.Append(" ORDER BY SL_NO ");
            return sb.ToString();
        }
        public static List<dcPURCHASE_STATUS> GetPURCHASE_STATUSList()
        {
            return GetPURCHASE_STATUSList(null, null);
        }
        public static List<dcPURCHASE_STATUS> GetPURCHASE_STATUSList(DBContext dc)
        {
            return GetPURCHASE_STATUSList(null, dc);
        }
        public static List<dcPURCHASE_STATUS> GetPURCHASE_STATUSList(DBQuery dbq, DBContext dc)
        {
            List<dcPURCHASE_STATUS> cObjList = new List<dcPURCHASE_STATUS>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcPURCHASE_STATUS>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcPURCHASE_STATUS> GetPurchaseStatusList(DBContext dc)
        {
            List<dcPURCHASE_STATUS> cObjList = new List<dcPURCHASE_STATUS>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetPurchaseStatusDetailsSQLString());

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                cObjList = GetPURCHASE_STATUSList(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcPURCHASE_STATUS GetPURCHASE_STATUSByID(int pPURCHASE_STATUSID)
        {
            return GetPURCHASE_STATUSByID(pPURCHASE_STATUSID, null);
        }
        public static dcPURCHASE_STATUS GetPURCHASE_STATUSByID(int pPURCHASE_STATUSID, DBContext dc)
        {
            dcPURCHASE_STATUS cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcPURCHASE_STATUS>()
                                  where c.PURCHASE_STATUS_ID == pPURCHASE_STATUSID
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

        public static int Insert(dcPURCHASE_STATUS cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcPURCHASE_STATUS cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcPURCHASE_STATUS>(cObj, true);
                if (id > 0) { cObj.PURCHASE_STATUS_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcPURCHASE_STATUS cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcPURCHASE_STATUS cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcPURCHASE_STATUS>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pPURCHASE_STATUSID)
        {
            return Delete(pPURCHASE_STATUSID, null);
        }
        public static bool Delete(int pPURCHASE_STATUSID, DBContext dc)
        {
            dcPURCHASE_STATUS cObj = new dcPURCHASE_STATUS();
            cObj.PURCHASE_STATUS_ID = pPURCHASE_STATUSID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcPURCHASE_STATUS>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcPURCHASE_STATUS cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcPURCHASE_STATUS cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcPURCHASE_STATUS cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcPURCHASE_STATUS cObj, DBContext dc)
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
                                newID = cObj.PURCHASE_STATUS_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.PURCHASE_STATUS_ID, dc))
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

        public static bool SaveList(List<dcPURCHASE_STATUS> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcPURCHASE_STATUS> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcPURCHASE_STATUS oDet in detList)
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
                        bool d = Delete(oDet.PURCHASE_STATUS_ID, dc);
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
