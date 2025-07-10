using PG.BLLibrary.ProductionDC;
using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Text;

namespace PG.BLLibrary.ProductionBL
{
    public class PROD_PURELEAD_DROSS_DTLBL
    {

        public static string GetDrossDtlsSQLString()
        {
            StringBuilder sb = new StringBuilder();
             sb.Append(" Select ");
             sb.Append("   dtl.PROD_MST_ID ");
             sb.Append("  ,dtl.PUR_DROSS_DTL_ID ");
             sb.Append("  ,inv.ITEM_NAME ");
             sb.Append("  ,inv.ITEM_ID ");
             sb.Append("  ,dtl.ITEM_QTY ");
             sb.Append("  ,itmUOM.UOM_CODE_SHORT UOM_NAME ");
             sb.Append("  ,dtl.UOM_ID ");
             sb.Append("  ,dtl.MACHINE_ID ");
             sb.Append("  ,mac.MACHINE_NAME ");
             sb.Append("  ,dtl.REMARKS ");
             sb.Append("  FROM  ");
             sb.Append("  production_mst mst  ");
             sb.Append("  inner join  PROD_PURELEAD_DROSS_DTL dtl   on mst.PROD_ID=dtl.PROD_MST_ID ");
             sb.Append("  INNER JOIN INV_ITEM_MASTER inv ON dtl.ITEM_ID=inv.ITEM_ID ");
             sb.Append("  INNER JOIN UOM_INFO itmUOM  ON dtl.UOM_ID=itmUOM.UOM_ID ");
             sb.Append("  LEFT JOIN MACHINE_MST mac ON dtl.MACHINE_ID=mac.MACHINE_ID ");
             sb.Append(" WHERE 1=1 ");
            return sb.ToString();
        }


        public static DataLoadOptions PROD_PURELEAD_DROSS_DTLLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcPROD_PURELEAD_DROSS_DTL>(obj => obj.relatedclassname);
            return dlo;
        }
        public static List<dcPROD_PURELEAD_DROSS_DTL> GetPROD_PURELEAD_DROSS_DTLList()
        {
            return GetPROD_PURELEAD_DROSS_DTLList(null, null);
        }
        public static List<dcPROD_PURELEAD_DROSS_DTL> GetPROD_PURELEAD_DROSS_DTLList(DBContext dc)
        {
            return GetPROD_PURELEAD_DROSS_DTLList(null, dc);
        }
        public static List<dcPROD_PURELEAD_DROSS_DTL> GetPROD_PURELEAD_DROSS_DTLList(DBQuery dbq, DBContext dc)
        {
            List<dcPROD_PURELEAD_DROSS_DTL> cObjList = new List<dcPROD_PURELEAD_DROSS_DTL>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcPROD_PURELEAD_DROSS_DTL>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcPROD_PURELEAD_DROSS_DTL GetPROD_PURELEAD_DROSS_DTLByID(int pPROD_PURELEAD_DROSS_DTLID)
        {
            return GetPROD_PURELEAD_DROSS_DTLByID(pPROD_PURELEAD_DROSS_DTLID, null);
        }
        public static dcPROD_PURELEAD_DROSS_DTL GetPROD_PURELEAD_DROSS_DTLByID(int pPROD_PURELEAD_DROSS_DTLID, DBContext dc)
        {
            dcPROD_PURELEAD_DROSS_DTL cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcPROD_PURELEAD_DROSS_DTL>()
                                  where c.PUR_DROSS_DTL_ID == pPROD_PURELEAD_DROSS_DTLID
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

        public static int Insert(dcPROD_PURELEAD_DROSS_DTL cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcPROD_PURELEAD_DROSS_DTL cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcPROD_PURELEAD_DROSS_DTL>(cObj, true);
                if (id > 0) { cObj.PUR_DROSS_DTL_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcPROD_PURELEAD_DROSS_DTL cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcPROD_PURELEAD_DROSS_DTL cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcPROD_PURELEAD_DROSS_DTL>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pPROD_PURELEAD_DROSS_DTLID)
        {
            return Delete(pPROD_PURELEAD_DROSS_DTLID, null);
        }
        public static bool Delete(int pPROD_PURELEAD_DROSS_DTLID, DBContext dc)
        {
            dcPROD_PURELEAD_DROSS_DTL cObj = new dcPROD_PURELEAD_DROSS_DTL();
            cObj.PUR_DROSS_DTL_ID = pPROD_PURELEAD_DROSS_DTLID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcPROD_PURELEAD_DROSS_DTL>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcPROD_PURELEAD_DROSS_DTL cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcPROD_PURELEAD_DROSS_DTL cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcPROD_PURELEAD_DROSS_DTL cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcPROD_PURELEAD_DROSS_DTL cObj, DBContext dc)
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
                                newID = cObj.PUR_DROSS_DTL_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.PUR_DROSS_DTL_ID, dc))
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

        public static bool SaveList(List<dcPROD_PURELEAD_DROSS_DTL> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcPROD_PURELEAD_DROSS_DTL> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcPROD_PURELEAD_DROSS_DTL oDet in detList)
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
                        bool d = Delete(oDet.PUR_DROSS_DTL_ID, dc);
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


        public static List<dcPROD_PURELEAD_DROSS_DTL> GetDrossDtlsByProdID(int pProd_id, DBContext dc)
        {
            List<dcPROD_PURELEAD_DROSS_DTL> cObjList = new List<dcPROD_PURELEAD_DROSS_DTL>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder(GetDrossDtlsSQLString());

                    if (pProd_id > 0)
                    {
                        sb.Append("  AND dtl.PROD_MST_ID=@PROD_MST_ID ");
                        cmdInfo.DBParametersInfo.Add("@PROD_MST_ID", pProd_id);
                    }

                    DBQuery dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcPROD_PURELEAD_DROSS_DTL>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
    }
}
