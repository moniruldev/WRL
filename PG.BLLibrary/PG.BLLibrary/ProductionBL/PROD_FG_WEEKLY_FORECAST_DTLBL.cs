using PG.Core.DBBase;
using PG.DBClass.InventoryDC;
using PG.DBClass.ProductionDC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Text;

namespace PG.BLLibrary.ProductionBL
{
    public class PROD_FG_WEEKLY_FORECAST_DTLBL
    {
        public static DataLoadOptions PROD_FG_WEEKLY_FORECAST_DTLLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcPROD_FG_WEEKLY_FORECAST_DTL>(obj => obj.relatedclassname);
            return dlo;
        }
        public static List<dcPROD_FG_WEEKLY_FORECAST_DTL> GetPROD_FG_WEEKLY_FORECAST_DTLList()
        {
            return GetPROD_FG_WEEKLY_FORECAST_DTLList(null, null);
        }
        public static List<dcPROD_FG_WEEKLY_FORECAST_DTL> GetPROD_FG_WEEKLY_FORECAST_DTLList(DBContext dc)
        {
            return GetPROD_FG_WEEKLY_FORECAST_DTLList(null, dc);
        }
        public static List<dcPROD_FG_WEEKLY_FORECAST_DTL> GetPROD_FG_WEEKLY_FORECAST_DTLList(DBQuery dbq, DBContext dc)
        {
            List<dcPROD_FG_WEEKLY_FORECAST_DTL> cObjList = new List<dcPROD_FG_WEEKLY_FORECAST_DTL>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcPROD_FG_WEEKLY_FORECAST_DTL>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcPROD_FG_WEEKLY_FORECAST_DTL GetPROD_FG_WEEKLY_FORECAST_DTLByID(int pPROD_FG_WEEKLY_FORECAST_DTLID)
        {
            return GetPROD_FG_WEEKLY_FORECAST_DTLByID(pPROD_FG_WEEKLY_FORECAST_DTLID, null);
        }
        public static dcPROD_FG_WEEKLY_FORECAST_DTL GetPROD_FG_WEEKLY_FORECAST_DTLByID(int pPROD_FG_WEEKLY_FORECAST_DTLID, DBContext dc)
        {
            dcPROD_FG_WEEKLY_FORECAST_DTL cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcPROD_FG_WEEKLY_FORECAST_DTL>()
                                  where c.FC_DET_ID == pPROD_FG_WEEKLY_FORECAST_DTLID
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

        public static int Insert(dcPROD_FG_WEEKLY_FORECAST_DTL cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcPROD_FG_WEEKLY_FORECAST_DTL cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcPROD_FG_WEEKLY_FORECAST_DTL>(cObj, true);
                if (id > 0) { cObj.FC_DET_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcPROD_FG_WEEKLY_FORECAST_DTL cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcPROD_FG_WEEKLY_FORECAST_DTL cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcPROD_FG_WEEKLY_FORECAST_DTL>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pPROD_FG_WEEKLY_FORECAST_DTLID)
        {
            return Delete(pPROD_FG_WEEKLY_FORECAST_DTLID, null);
        }
        public static bool Delete(int pPROD_FG_WEEKLY_FORECAST_DTLID, DBContext dc)
        {
            dcPROD_FG_WEEKLY_FORECAST_DTL cObj = new dcPROD_FG_WEEKLY_FORECAST_DTL();
            cObj.FC_DET_ID = pPROD_FG_WEEKLY_FORECAST_DTLID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcPROD_FG_WEEKLY_FORECAST_DTL>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcPROD_FG_WEEKLY_FORECAST_DTL cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcPROD_FG_WEEKLY_FORECAST_DTL cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcPROD_FG_WEEKLY_FORECAST_DTL cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcPROD_FG_WEEKLY_FORECAST_DTL cObj, DBContext dc)
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
                                newID = cObj.FC_DET_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.FC_DET_ID, dc))
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

        public static bool SaveList(List<dcPROD_FG_WEEKLY_FORECAST_DTL> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcPROD_FG_WEEKLY_FORECAST_DTL> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcPROD_FG_WEEKLY_FORECAST_DTL oDet in detList)
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
                        bool d = Delete(oDet.FC_DET_ID, dc);
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


        public static string GetForecastDtlbyFC_IDSQLString(string _FCDate, string _FGFC_TYPE)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" Select ");
            sb.Append("   Fmst.WK_FC_DESC,Fmst.FC_NO ,Fdtl.FC_DET_ID ,Fdtl.WK_FC_MST_ID ,Fdtl.REMARKS,INV_ITEM_MASTER.ITEM_ID,INV_ITEM_MASTER.ITEM_NAME ");
            sb.Append("   ,Fdtl.DEALER_RATE,Fdtl.WK1_ITEM_QTY,Fdtl.WK2_ITEM_QTY,Fdtl.WK3_ITEM_QTY, Fdtl.WK4_ITEM_QTY,fdtl.TOTAL_MONTH_QTY ");
            sb.Append(" , INV_ITEM_GROUP.ITEM_GROUP_CODE, INV_ITEM_GROUP.ITEM_GROUP_NAME  ");
            sb.Append(" , INV_ITEM_CLASS.ITEM_CLASS_CODE, INV_ITEM_CLASS.ITEM_CLASS_NAME  ");
            sb.Append(" , INV_ITEM_TYPE.ITEM_TYPE_CODE, INV_ITEM_TYPE.ITEM_TYPE_NAME  ");
            sb.Append(" , UOM_INFO.UOM_CODE, UOM_INFO.UOM_CODE_SHORT UOM_NAME ");
            sb.Append("  ,UOM_INFO.UOM_ID");
            sb.Append(" FROM ");
            sb.Append(" INV_ITEM_MASTER");
            sb.Append(" INNER JOIN PROD_FG_WEEKLY_FORECAST_DTL Fdtl ON Fdtl.ITEM_ID=INV_ITEM_MASTER.ITEM_ID  ");
            sb.Append(" and Fdtl.WK_FC_MST_ID IN (SELECT WK_FC_ID FROM PROD_FG_WEEKLY_FORECAST_MST WHERE   FOR_YEAR=to_char(to_date('" + _FCDate + "','dd-mon-yyyy'),'yyyy') ");
            sb.Append(" and FOR_MONTH= to_char(to_date('" + _FCDate + "','dd-mon-yyyy'),'mm') AND FGFC_TYPE=" + _FGFC_TYPE + ") ");
            sb.Append(" INNER JOIN PROD_FG_WEEKLY_FORECAST_MST Fmst ON  Fmst.WK_FC_ID=Fdtl.WK_FC_MST_ID ");
            sb.Append(" INNER JOIN INV_ITEM_GROUP ON INV_ITEM_MASTER.ITEM_GROUP_ID = INV_ITEM_GROUP.ITEM_GROUP_ID ");
            sb.Append(" INNER JOIN INV_ITEM_CLASS ON INV_ITEM_MASTER.ITEM_CLASS_ID = INV_ITEM_CLASS.ITEM_CLASS_ID ");
            sb.Append(" INNER JOIN INV_ITEM_TYPE ON INV_ITEM_MASTER.ITEM_TYPE_ID = INV_ITEM_TYPE.ITEM_TYPE_ID ");
            sb.Append(" INNER JOIN UOM_INFO ON INV_ITEM_MASTER.UOM_ID = UOM_INFO.UOM_ID ");
            sb.Append(" Where 1=1  AND SND_TRANSFER='Y'  ");
            return sb.ToString();
        }



        public static List<dcPROD_FG_WEEKLY_FORECAST_DTL> GetItemListBYTypeGroupClass(clsPrmInventory prm, DBContext dc)
        {
            List<dcPROD_FG_WEEKLY_FORECAST_DTL> cObjList = new List<dcPROD_FG_WEEKLY_FORECAST_DTL>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder(GetForecastDtlbyFC_IDSQLString(prm.TransMonth, prm.fcType.ToString()));

                    if (prm.itemtype_id > 0)
                    {
                        sb.Append(" and INV_ITEM_MASTER.ITEM_TYPE_ID=@ItemTypeId ");
                        cmdInfo.DBParametersInfo.Add("@ItemTypeId", prm.itemtype_id);
                    }
                    if (prm.ItemClassId > 0)
                    {
                        sb.Append(" and INV_ITEM_MASTER.ITEM_CLASS_ID=@ItemClassId ");
                        cmdInfo.DBParametersInfo.Add("@ItemClassId", prm.ItemClassId);
                    }
                    if (prm.itemGroup_id > 0)
                    {
                        sb.Append(" and INV_ITEM_MASTER.ITEM_GROUP_ID=@ItemGroupId ");
                        cmdInfo.DBParametersInfo.Add("@ItemGroupId", prm.itemGroup_id);
                    }

                    sb.Append(" ORDER BY Fdtl.WK_FC_MST_ID,INV_ITEM_MASTER.ITEM_NAME ");
                    DBQuery dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcPROD_FG_WEEKLY_FORECAST_DTL>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
    }
}
