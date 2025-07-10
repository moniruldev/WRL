using PG.Core.DBBase;
using PG.DBClass.ProductionDC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Text;

namespace PG.BLLibrary.ProductionBL
{
    public class BOM_DTL_TBL
    {
        public static DataLoadOptions BOM_DTL_TLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcBOM_DTL_T>(obj => obj.relatedclassname);
            return dlo;
        }

        public static string GetBOMDtlbyBOM_IDSQLString()
        {
            StringBuilder sb = new StringBuilder();

             sb.Append(" SELECT ");
             sb.Append(" dtl.BOM_DTL_ID ");
             sb.Append(" ,dtl.BOM_MST_ID ");
             sb.Append(" ,dtl.ITEM_ID ");
             sb.Append(" ,dtl.ITEM_QTY ");
             sb.Append(" ,dtl.ITEM_UNIT_ID ");
             sb.Append(" ,dtl.ITEM_WEIGHT ");
             sb.Append(" ,dtl.IS_PRIME ");
             sb.Append(" ,dtl.PACKAGE_ID ");
             sb.Append(" ,dtl.REMARKS ");
             sb.Append(" ,dtl.SLNO ");
             sb.Append(" ,dtl.ITEM_BOM_ID ");
             sb.Append(" ,itm.ITEM_NAME ");
             sb.Append(" ,U.UOM_CODE_SHORT UOM_NAME ");
             sb.Append(" ,G.ITEM_GROUP_NAME ITEM_GROUP_DESC ");
             sb.Append(" ,bMst.BOM_ITEM_DESC  BOM_ITEM_DESC");
             sb.Append(" ,dtl.WASTAGE_PERCENT ");
             sb.Append(" FROM BOM_MST_T mst  ");
             sb.Append(" INNER JOIN BOM_DTL_T dtl ON mst.BOM_ID=dtl.BOM_MST_ID ");
             sb.Append(" INNER JOIN INV_ITEM_MASTER itm ON dtl.ITEM_ID=itm.ITEM_ID ");
             sb.Append(" INNER JOIN UOM_INFO U ON dtl.ITEM_UNIT_ID=U.UOM_ID ");
             sb.Append(" INNER JOIN INV_ITEM_GROUP G ON itm.ITEM_GROUP_ID=G.ITEM_GROUP_ID ");
             sb.Append(" LEFT JOIN BOM_MST_T bMst ON dtl.ITEM_BOM_ID=bMst.BOM_ID ");
             sb.Append(" WHERE 1=1 ");
       
            return sb.ToString();
        }
        public static List<dcBOM_DTL_T> GetBOM_DTL_TList()
        {
            return GetBOM_DTL_TList(null, null);
        }
        public static List<dcBOM_DTL_T> GetBOM_DTL_TList(DBContext dc)
        {
            return GetBOM_DTL_TList(null, dc);
        }
        public static List<dcBOM_DTL_T> GetBOM_DTL_TList(DBQuery dbq, DBContext dc)
        {
            List<dcBOM_DTL_T> cObjList = new List<dcBOM_DTL_T>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcBOM_DTL_T>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        //public static dcBOM_DTL_T GetBOM_DTL_TByID(int pBOM_DTL_TID)
        //{
        //    return GetBOM_DTL_TByID(pBOM_DTL_TID, null);
        //}


        public static List<dcBOM_DTL_T> GetBOM_DTL_TByID(int pBOM_DTL_TID)
        {
            return GetBOM_DTL_TByID(pBOM_DTL_TID, null);
        }


        public static List<dcBOM_DTL_T> GetBOM_DTL_TByID(int _BOM_ID, DBContext dc)
        {
            List<dcBOM_DTL_T> cObjList = new List<dcBOM_DTL_T>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder(GetBOMDtlbyBOM_IDSQLString());

                    //if (_BOM_ID > 0)
                    //{
                        sb.Append(" AND mst.BOM_ID=@BOM_ID ");
                        cmdInfo.DBParametersInfo.Add("@BOM_ID", _BOM_ID);
                    //}
                        sb.Append(" ORDER BY dtl.SLNO ");
                    DBQuery dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    //dbq.OrderBy = " dtl.SLNO ";
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcBOM_DTL_T>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }




        //public static dcBOM_DTL_T GetBOM_DTL_TByID(int pBOM_DTL_TID, DBContext dc)
        //{
        //    dcBOM_DTL_T cObj = null;
        //    bool isDCInit = false;
        //    try
        //    {
        //        isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
        //        using (DataContext dataContext = dc.NewDataContext())
        //        {
        //            var result = (from c in dataContext.GetTable<dcBOM_DTL_T>()
        //                          where c.BOM_DTL_ID == pBOM_DTL_TID
        //                          select c).ToList();
        //            if (result.Count() > 0)
        //            {
        //                cObj = result.First();
        //            }
        //        }
        //    }
        //    catch { throw; }
        //    finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
        //    return cObj;
        //}

        public static int Insert(dcBOM_DTL_T cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcBOM_DTL_T cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcBOM_DTL_T>(cObj, true);
                if (id > 0) { cObj.BOM_DTL_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcBOM_DTL_T cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcBOM_DTL_T cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcBOM_DTL_T>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pBOM_DTL_TID)
        {
            return Delete(pBOM_DTL_TID, null);
        }
        public static bool Delete(int pBOM_DTL_TID, DBContext dc)
        {
            dcBOM_DTL_T cObj = new dcBOM_DTL_T();
            cObj.BOM_DTL_ID = pBOM_DTL_TID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcBOM_DTL_T>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcBOM_DTL_T cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcBOM_DTL_T cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ?  RecordStateEnum.Added :  RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcBOM_DTL_T cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcBOM_DTL_T cObj, DBContext dc)
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
                                newID = cObj.BOM_DTL_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.BOM_DTL_ID, dc))
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

        public static bool SaveList(List<dcBOM_DTL_T> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcBOM_DTL_T> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcBOM_DTL_T oDet in detList)
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
                        bool d = Delete(oDet.BOM_DTL_ID, dc);
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

        //get Finished item wise raw matterial used

        public static List<dcBOM_DTL_T> GetFinishedItemWise_Bom_Dtl(int _BOM_ID, DBContext dc)
        {
            List<dcBOM_DTL_T> cObjList = new List<dcBOM_DTL_T>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder(GetBOMDtlbyBOM_IDSQLString());

                    //if (_BOM_ID > 0)
                    //{
                    sb.Append(" AND mst.BOM_ID=@BOM_ID ");
                    cmdInfo.DBParametersInfo.Add("@BOM_ID", _BOM_ID);
                    //}
                    sb.Append(" ORDER BY dtl.SLNO ");
                    DBQuery dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    //dbq.OrderBy = " dtl.SLNO ";
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcBOM_DTL_T>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
    }
}
