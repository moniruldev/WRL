using PG.Core.DBBase;
using PG.DBClass.ProductionDC;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;

namespace PG.BLLibrary.ProductionBL
{
    public class BOM_COMPOSITE_PACKAGE_MST_TBL
    {

        public static string GetPackageListString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT pa.*,INV.ITEM_DESC ITEM_NAME from BOM_COMPOSITE_PACKAGE_MST_T pa ");
             sb.Append(" INNER JOIN INV_Item_master INV ON pa.ITEM_ID=INV.ITEM_ID where 1=1");

            return sb.ToString();
        }

        public static DataLoadOptions BOM_COMPOSITE_PACKAGE_MST_TLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcBOM_COMPOSITE_PACKAGE_MST_T>(obj => obj.relatedclassname);
            return dlo;
        }
        public static List<dcBOM_COMPOSITE_PACKAGE_MST_T> GetBOM_COMPOSITE_PACKAGE_MST_TList()
        {
            return GetBOM_COMPOSITE_PACKAGE_MST_TList(null, null);
        }
        public static List<dcBOM_COMPOSITE_PACKAGE_MST_T> GetBOM_COMPOSITE_PACKAGE_MST_TList(DBContext dc)
        {
            return GetBOM_COMPOSITE_PACKAGE_MST_TList(null, dc);
        }
        public static List<dcBOM_COMPOSITE_PACKAGE_MST_T> GetBOM_COMPOSITE_PACKAGE_MST_TList(DBQuery dbq, DBContext dc)
        {
            List<dcBOM_COMPOSITE_PACKAGE_MST_T> cObjList = new List<dcBOM_COMPOSITE_PACKAGE_MST_T>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcBOM_COMPOSITE_PACKAGE_MST_T>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcBOM_COMPOSITE_PACKAGE_MST_T GetBOM_COMPOSITE_PACKAGE_MST_TByID(int pBOM_COMPOSITE_PACKAGE_MST_TID)
        {
            return GetBOM_COMPOSITE_PACKAGE_MST_TByID(pBOM_COMPOSITE_PACKAGE_MST_TID, null);
        }
        public static dcBOM_COMPOSITE_PACKAGE_MST_T GetBOM_COMPOSITE_PACKAGE_MST_TByID(int pBOM_COMPOSITE_PACKAGE_MST_TID, DBContext dc)
        {
            dcBOM_COMPOSITE_PACKAGE_MST_T cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcBOM_COMPOSITE_PACKAGE_MST_T>()
                                  where c.PACKAGE_ID == pBOM_COMPOSITE_PACKAGE_MST_TID
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

        public static int Insert(dcBOM_COMPOSITE_PACKAGE_MST_T cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcBOM_COMPOSITE_PACKAGE_MST_T cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcBOM_COMPOSITE_PACKAGE_MST_T>(cObj, true);
                if (id > 0) { cObj.PACKAGE_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcBOM_COMPOSITE_PACKAGE_MST_T cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcBOM_COMPOSITE_PACKAGE_MST_T cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcBOM_COMPOSITE_PACKAGE_MST_T>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pBOM_COMPOSITE_PACKAGE_MST_TID)
        {
            return Delete(pBOM_COMPOSITE_PACKAGE_MST_TID, null);
        }
        public static bool Delete(int pBOM_COMPOSITE_PACKAGE_MST_TID, DBContext dc)
        {
            dcBOM_COMPOSITE_PACKAGE_MST_T cObj = new dcBOM_COMPOSITE_PACKAGE_MST_T();
            cObj.PACKAGE_ID = pBOM_COMPOSITE_PACKAGE_MST_TID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcBOM_COMPOSITE_PACKAGE_MST_T>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcBOM_COMPOSITE_PACKAGE_MST_T cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcBOM_COMPOSITE_PACKAGE_MST_T cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcBOM_COMPOSITE_PACKAGE_MST_T cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcBOM_COMPOSITE_PACKAGE_MST_T cObj, DBContext dc)
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
                                newID = cObj.PACKAGE_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.PACKAGE_ID, dc))
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

        public static bool SaveList(List<dcBOM_COMPOSITE_PACKAGE_MST_T> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcBOM_COMPOSITE_PACKAGE_MST_T> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcBOM_COMPOSITE_PACKAGE_MST_T oDet in detList)
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
                        bool d = Delete(oDet.PACKAGE_ID, dc);
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
