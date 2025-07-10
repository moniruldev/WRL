using PG.Core.DBBase;
using PG.Core.Utility;
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
    public class PRODUCTION_FLOOR_CLOSINGBL
    {
        public static DataLoadOptions PRODUCTION_FLOOR_CLOSINGLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcPRODUCTION_FLOOR_CLOSING>(obj => obj.relatedclassname);
            return dlo;
        }
        public static string GetProductionClosingDtlsSQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" Select ");
            sb.Append("  cl.CLOSING_ID ");
            sb.Append(" ,cl.PROD_MST_ID  ");
            sb.Append(" ,cl.CLOSING_ITEM_ID  ");
            sb.Append(" ,cl.CLOSING_QTY  ");
            sb.Append(" ,cl.CLOSING_UOM_ID  ");
            sb.Append(" ,cl.CLOSING_REMARKS  ");
            sb.Append(" ,cl.CLOSING_SI  ");
            sb.Append("   ,(CASE WHEN (SELECT COUNT(PROD_BATCH_NO) FROM PRODUCTION_BATCH_INFO WHERE 1=1 AND DEPT_ID=M.DEPT_ID AND STLM_ID=M.STLM_ID AND ITEM_ID=cl.CLOSING_ITEM_ID  AND PROD_ID=CL.PROD_MST_ID AND FG_ITEM_ID=cl.FINISHED_ITEM_ID)>0 THEN ");
            sb.Append(" CL.SYSTEM_OPENING_STOCK ");
            //sb.Append(" (SELECT SUM(NVL(USED_QTY,0)) FROM PRODUCTION_BATCH_INFO WHERE 1=1 AND DEPT_ID=M.DEPT_ID AND STLM_ID=M.STLM_ID AND ITEM_ID=cl.CLOSING_ITEM_ID  AND PROD_ID=CL.PROD_MST_ID AND FG_ITEM_ID=cl.FINISHED_ITEM_ID ) ");
            sb.Append(" ELSE ( SELECT GET_DEPT_WISE_CLOSING_QTY(cl.CLOSING_ITEM_ID,m.DEPT_ID) FROM DUAL) END) SYSTEM_OPENING_STOCK  ");
            //sb.Append(" ,GET_DEPT_WISE_CLOSING_QTY(cl.CLOSING_ITEM_ID,m.DEPT_ID) SYSTEM_OPENING_STOCK  ");
            sb.Append(" ,cl.MANUAL_OPENING_STOCK  ");
            sb.Append(" ,cl.ISSUE_STOCK  ");
            sb.Append(" ,cl.WASTAGE_QTY  ");
            sb.Append(" ,cl.REJECTED_QTY  ");
            sb.Append(" ,cl.POSITIVE_DEV  ");
            sb.Append(" ,cl.NEGATIVE_DEV ");
            sb.Append(" ,cl.REUSE_QTY ");
            sb.Append(" ,cl.FINISHED_ITEM_ID ");
            sb.Append(" ,cl.ISMANUAL ");
            sb.Append(" ,cl.STD_USED_QTY ");
            sb.Append(" ,cl.RECOVERY_QTY ");
            sb.Append(" ,cl.ASM_OP_RECOVERY_QTY ");
            sb.Append(" ,cl.ASM_OP_REJECT_QTY ");
            sb.Append(" ,itm.ITEM_NAME CLOSINGITEM_NAME ");
            sb.Append(" ,itm.ITEM_GROUP_ID CLOSING_ITEM_GROUP_ID,itm.IS_BATCH");
            sb.Append("  ,finishItem.ITEM_NAME FINISH_ITEM_NAME ");
            sb.Append("  ,U.UOM_CODE_SHORT CLOSING_UOM_NAME ");
            sb.Append("  ,MC.MACHINE_NAME ");
            sb.Append("  ,cl.MACHINE_ID ");
            sb.Append(" ,(Select DISTINCT STLM_ID FROM PRO_DEPARTMENT_ITEM Where ITEM_ID=cl.CLOSING_ITEM_ID AND IS_FINISHED='N' and rownum=1) STLM_ID ");
            sb.Append("  FROM ");
            sb.Append("  production_mst m  INNER JOIN PRODUCTION_FLOOR_CLOSING cl ON m.PROD_ID=cl.PROD_MST_ID ");
            sb.Append("  INNER JOIN INV_ITEM_MASTER itm ON cl.CLOSING_ITEM_ID=itm.ITEM_ID ");
            sb.Append("  LEFT JOIN INV_ITEM_MASTER finishItem ON cl.FINISHED_ITEM_ID=finishItem.ITEM_ID ");
            sb.Append("  INNER JOIN UOM_INFO U ON cl.CLOSING_UOM_ID=U.UOM_ID ");
            sb.Append("  LEFT JOIN MACHINE_MST MC ON MC.MACHINE_ID=cl.MACHINE_ID ");
           
            sb.Append(" Where 1=1 ");
            return sb.ToString();
        }

        public static string GetProductionClosingDtlsFormationSQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" Select ");
            sb.Append("  cl.CLOSING_ID ");
            sb.Append(" ,cl.PROD_MST_ID  ");
            sb.Append(" ,cl.CLOSING_ITEM_ID  ");
            sb.Append(" ,cl.CLOSING_QTY  ");
            sb.Append(" ,cl.CLOSING_UOM_ID  ");
            sb.Append(" ,cl.CLOSING_REMARKS  ");
            sb.Append(" ,cl.CLOSING_SI  ");
            //sb.Append(" ,GET_DEPT_WISE_CLOSING_QTY(cl.CLOSING_ITEM_ID,m.DEPT_ID) SYSTEM_OPENING_STOCK  ");
            sb.Append(" ,CASE WHEN ITM.ITEM_GROUP_ID =65 THEN GET_UNFORM_QTY(cl.CLOSING_ITEM_ID) ELSE CL.SYSTEM_OPENING_STOCK END SYSTEM_OPENING_STOCK ");
            sb.Append(" ,cl.MANUAL_OPENING_STOCK  ");
            sb.Append(" ,cl.ISSUE_STOCK  ");
            sb.Append(" ,cl.WASTAGE_QTY  ");
            sb.Append(" ,cl.REJECTED_QTY  ");
            sb.Append(" ,cl.POSITIVE_DEV  ");
            sb.Append(" ,cl.NEGATIVE_DEV ");
            sb.Append(" ,cl.REUSE_QTY ");
            sb.Append(" ,cl.FINISHED_ITEM_ID ");
            sb.Append(" ,cl.ISMANUAL ");
            sb.Append(" ,cl.STD_USED_QTY ");
            sb.Append(" ,cl.RECOVERY_QTY ");
            sb.Append(" ,cl.ASM_OP_RECOVERY_QTY ");
            sb.Append(" ,cl.ASM_OP_REJECT_QTY ");
            sb.Append(" ,itm.ITEM_NAME CLOSINGITEM_NAME ");
            sb.Append(" ,itm.ITEM_GROUP_ID CLOSING_ITEM_GROUP_ID,itm.IS_BATCH ");
            sb.Append("  ,finishItem.ITEM_NAME FINISH_ITEM_NAME ");
            sb.Append("  ,U.UOM_CODE_SHORT CLOSING_UOM_NAME ");
            sb.Append("  ,MC.MACHINE_NAME ");
            sb.Append("  ,cl.MACHINE_ID ");
            sb.Append(" ,(Select DISTINCT STLM_ID FROM PRO_DEPARTMENT_ITEM Where ITEM_ID=cl.CLOSING_ITEM_ID AND IS_FINISHED='N' and rownum=1) STLM_ID ");
            sb.Append("  FROM ");
            sb.Append("  production_mst m  INNER JOIN PRODUCTION_FLOOR_CLOSING cl ON m.PROD_ID=cl.PROD_MST_ID ");
            sb.Append("  INNER JOIN INV_ITEM_MASTER itm ON cl.CLOSING_ITEM_ID=itm.ITEM_ID ");
            sb.Append("  LEFT JOIN INV_ITEM_MASTER finishItem ON cl.FINISHED_ITEM_ID=finishItem.ITEM_ID ");
            sb.Append("  INNER JOIN UOM_INFO U ON cl.CLOSING_UOM_ID=U.UOM_ID ");
            sb.Append("  LEFT JOIN MACHINE_MST MC ON MC.MACHINE_ID=cl.MACHINE_ID ");

            sb.Append(" Where 1=1 ");
            return sb.ToString();
        }



        
        public static List<dcPRODUCTION_FLOOR_CLOSING> GetProductionClosingDtlsByProdID(int pProd_id, DBContext dc)
        {
            List<dcPRODUCTION_FLOOR_CLOSING> cObjList = new List<dcPRODUCTION_FLOOR_CLOSING>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                     StringBuilder sb = new StringBuilder(GetProductionClosingDtlsSQLString());

                   // StringBuilder sb = new StringBuilder(GetProductionClosingDtlsWithDeptIDSQLString());
                    if (pProd_id > 0)
                    {
                        sb.Append("  AND cl.PROD_MST_ID=@PROD_MST_ID ");
                        cmdInfo.DBParametersInfo.Add("@PROD_MST_ID", pProd_id);
                    }

                    sb.Append(" ORDER BY  cl.CLOSING_SI, finishItem.ITEM_NAME,itm.ITEM_NAME ");
                    DBQuery dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_FLOOR_CLOSING>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcPRODUCTION_FLOOR_CLOSING> GetProductionClosingDtlsFormationByProdID(int pProd_id, DBContext dc)
        {
            List<dcPRODUCTION_FLOOR_CLOSING> cObjList = new List<dcPRODUCTION_FLOOR_CLOSING>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder(GetProductionClosingDtlsFormationSQLString());

                    // StringBuilder sb = new StringBuilder(GetProductionClosingDtlsWithDeptIDSQLString());
                    if (pProd_id > 0)
                    {
                        sb.Append("  AND cl.PROD_MST_ID=@PROD_MST_ID ");
                        cmdInfo.DBParametersInfo.Add("@PROD_MST_ID", pProd_id);
                    }

                    sb.Append(" ORDER BY  cl.CLOSING_SI, finishItem.ITEM_NAME,itm.ITEM_NAME ");
                    DBQuery dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_FLOOR_CLOSING>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }


        public static List<dcPRODUCTION_FLOOR_CLOSING> GetPRODUCTION_FLOOR_CLOSINGList()
        {
            return GetPRODUCTION_FLOOR_CLOSINGList(null, null);
        }
        public static List<dcPRODUCTION_FLOOR_CLOSING> GetPRODUCTION_FLOOR_CLOSINGList(DBContext dc)
        {
            return GetPRODUCTION_FLOOR_CLOSINGList(null, dc);
        }
        public static List<dcPRODUCTION_FLOOR_CLOSING> GetPRODUCTION_FLOOR_CLOSINGList(DBQuery dbq, DBContext dc)
        {
            List<dcPRODUCTION_FLOOR_CLOSING> cObjList = new List<dcPRODUCTION_FLOOR_CLOSING>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_FLOOR_CLOSING>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcPRODUCTION_FLOOR_CLOSING GetPRODUCTION_FLOOR_CLOSINGByID(int pPRODUCTION_FLOOR_CLOSINGID)
        {
            return GetPRODUCTION_FLOOR_CLOSINGByID(pPRODUCTION_FLOOR_CLOSINGID, null);
        }
        public static dcPRODUCTION_FLOOR_CLOSING GetPRODUCTION_FLOOR_CLOSINGByID(int pPRODUCTION_FLOOR_CLOSINGID, DBContext dc)
        {
            dcPRODUCTION_FLOOR_CLOSING cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcPRODUCTION_FLOOR_CLOSING>()
                                  where c.CLOSING_ID == pPRODUCTION_FLOOR_CLOSINGID
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

        public static int Insert(dcPRODUCTION_FLOOR_CLOSING cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcPRODUCTION_FLOOR_CLOSING cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcPRODUCTION_FLOOR_CLOSING>(cObj, true);
                if (id > 0) { cObj.CLOSING_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcPRODUCTION_FLOOR_CLOSING cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcPRODUCTION_FLOOR_CLOSING cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcPRODUCTION_FLOOR_CLOSING>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pPRODUCTION_FLOOR_CLOSINGID)
        {
            return Delete(pPRODUCTION_FLOOR_CLOSINGID, null);
        }
        public static bool Delete(int pPRODUCTION_FLOOR_CLOSINGID, DBContext dc)
        {
            dcPRODUCTION_FLOOR_CLOSING cObj = new dcPRODUCTION_FLOOR_CLOSING();
            cObj.CLOSING_ID = pPRODUCTION_FLOOR_CLOSINGID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcPRODUCTION_FLOOR_CLOSING>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool DeleteByPROD_MST_ID(int pPROD_MST_ID)
        {
            return DeleteByPROD_MST_ID(pPROD_MST_ID, null);
        }
        public static bool DeleteByPROD_MST_ID(int pPROD_MST_ID, DBContext dc)
        {
            dcPRODUCTION_FLOOR_CLOSING cObj = new dcPRODUCTION_FLOOR_CLOSING();
            cObj.PROD_MST_ID = pPROD_MST_ID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcPRODUCTION_FLOOR_CLOSING>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool DeleteByItem_ID(int pPROD_MST_ID,int pFgItemId,int pMacineId)
        {
            return DeleteByItem_ID(pPROD_MST_ID, pFgItemId, pMacineId, null);
        }
        public static bool DeleteByItem_ID(int pPROD_MST_ID, int pFgItemId, int pMacineId, DBContext dc)
        {
            dcPRODUCTION_FLOOR_CLOSING cObj = new dcPRODUCTION_FLOOR_CLOSING();
            cObj.PROD_MST_ID = pPROD_MST_ID;
            cObj.FINISHED_ITEM_ID = pFgItemId;
            cObj.MACHINE_ID = pMacineId;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcPRODUCTION_FLOOR_CLOSING>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcPRODUCTION_FLOOR_CLOSING cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcPRODUCTION_FLOOR_CLOSING cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ?  RecordStateEnum.Added :  RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcPRODUCTION_FLOOR_CLOSING cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcPRODUCTION_FLOOR_CLOSING cObj, DBContext dc)
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
                        case  RecordStateEnum.Added:
                            newID = Insert(cObj, dc);
                            break;
                        case  RecordStateEnum.Edited:
                            if (Update(cObj, dc))
                            {
                                newID = cObj.CLOSING_ID;
                            }
                            break;
                        case  RecordStateEnum.Deleted:
                            if (Delete(cObj.CLOSING_ID, dc))
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

        public static bool SaveList(List<dcPRODUCTION_FLOOR_CLOSING> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcPRODUCTION_FLOOR_CLOSING> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcPRODUCTION_FLOOR_CLOSING oDet in detList)
            {
                switch (oDet._RecordState)
                {
                    case  RecordStateEnum.Added:
                        int a = Insert(oDet, dc);
                        break;
                    case  RecordStateEnum.Edited:
                        bool e = Update(oDet, dc);
                        break;
                    case RecordStateEnum.Deleted:
                        bool d = Delete(oDet.CLOSING_ID, dc);
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

        public static string GetRMDtlASBOM_For_SML_SQLString(clsPrmInventory cObj)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT ");
            sb.Append(" bmst.BOM_ID ");
            sb.Append(" ,bmst.BOM_ITEM_ID FINISHED_ITEM_ID ");
            sb.Append(" ,mitm.ITEM_NAME FINISH_ITEM_NAME ");
            sb.Append(" ,bdtl.ITEM_ID  CLOSING_ITEM_ID ");
            sb.Append(" ,ditm.ITEM_NAME CLOSINGITEM_NAME ");
            sb.Append(" ,DITM.ITEM_GROUP_ID CLOSING_ITEM_GROUP_ID ");
            //sb.Append(" ,bdtl.ITEM_QTY STD_USED_QTY bdtl.ITEM_QTY *");
            sb.Append("  ,ROUND( " + cObj.qty + " ,2) STD_USED_QTY ");
            sb.Append(" ,duom.UOM_CODE_SHORT CLOSING_UOM_NAME ");
            sb.Append(" , duom.UOM_ID CLOSING_UOM_ID ");
            sb.Append(" , GET_DEPT_WISE_CLOSING_QTY(bdtl.ITEM_ID," + cObj.DeptID.ToString() + ") SYSTEM_OPENING_STOCK ");
            sb.Append(" FROM BOM_MST_T bmst  ");
            sb.Append(" INNER JOIN BOM_DTL_T bdtl ON bmst.BOM_ID=bdtl.BOM_MST_ID ");
            sb.Append(" INNER JOIN INV_ITEM_MASTER mitm ON bmst.BOM_ITEM_ID=mitm.ITEM_ID ");
            sb.Append(" INNER JOIN INV_ITEM_MASTER ditm ON bdtl.ITEM_ID=ditm.ITEM_ID ");
            sb.Append(" INNER JOIN UOM_INFO duom ON bdtl.ITEM_UNIT_ID=duom.UOM_ID ");
            sb.Append(" Where 1=1 ");
            //sb.Append(" WHERE bmst.BOM_ITEM_ID=6695 ");
            return sb.ToString();
        }

        public static string GetBatchWiseStockSQLString(int pMachineId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT ISD.PROD_BATCH_NO,IM.ITEM_ID CLOSING_ITEM_ID,IM.ITEM_NAME CLOSINGITEM_NAME,IM.UOM_ID CLOSING_UOM_ID,UOM.UOM_CODE CLOSING_UOM_NAME  ");
            sb.Append(" ,(case when TB.MACHINE_ID= '" + pMachineId + "' then  ");
            sb.Append("  SUM(NVL(isd.RCV_QTY,0))-SUM(NVL(isd.ISS_QTY,0)) ");
            //sb.Append("  ELSE(CASE WHEN PMST.AUTH_STATUS='N' THEN SUM(NVL(isd.RCV_QTY,0))-SUM(NVL(isd.ISS_QTY,0))-SUM(NVL(TB.USED_QTY,0))-SUM(NVL(B.USED_QTY,0)) ELSE  SUM(NVL(isd.RCV_QTY,0))-SUM(NVL(isd.ISS_QTY,0))  END) end) SYSTEM_OPENING_STOCK ");
            sb.Append("  ELSE SUM(NVL(isd.RCV_QTY,0))-SUM(NVL(isd.ISS_QTY,0))-SUM(NVL(TB.USED_QTY,0)) end) SYSTEM_OPENING_STOCK "); //-SUM(NVL(B.USED_QTY,0))
            //sb.Append(" ,SUM(NVL(isd.RCV_QTY,0))-SUM(NVL(isd.ISS_QTY,0))-SUM(NVL(TB.USED_QTY,0)) SYSTEM_OPENING_STOCK "); //-SUM(NVL(TB.USED_QTY,0))
            sb.Append(" FROM ITEM_STOCK_DETAILS isd  ");
            sb.Append(" INNER JOIN INV_ITEM_MASTER IM ON ISD.ITEM_ID=IM.ITEM_ID ");
            sb.Append(" INNER JOIN UOM_INFO UOM ON UOM.UOM_ID=IM.UOM_ID ");
            sb.Append(" LEFT JOIN PROD_TEMP_BATCH_INFO TB ON ISD.PROD_BATCH_NO=TB.PROD_BATCH_NO AND ISD.ITEM_ID=TB.ITEM_ID "); // AND TB.MACHINE_ID='" + pMachineId + "'
            //sb.Append(" LEFT JOIN PRODUCTION_BATCH_INFO B ON ISD.PROD_BATCH_NO=B.PROD_BATCH_NO AND ISD.ITEM_ID=B.ITEM_ID  ");
            //sb.Append("  LEFT JOIN PRODUCTION_MST PMST ON B.PROD_ID=PMST.PROD_ID ");
            sb.Append(" WHERE 1=1 ");
            sb.Append(" AND isd.INV_TRANS_TYPE_ID  IN (402,305,1001,401,304,1002,601,1018) ");
            return sb.ToString();
        }


        public static string GetRMDtlASBOMSQLString(clsPrmInventory cObj)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT ");
            sb.Append(" bmst.BOM_ID ");
            sb.Append(" ,bmst.BOM_ITEM_ID FINISHED_ITEM_ID ");
            sb.Append(" ,mitm.ITEM_NAME FINISH_ITEM_NAME ");
            sb.Append(" ,bdtl.ITEM_ID  CLOSING_ITEM_ID ");
            sb.Append(" ,ditm.ITEM_NAME CLOSINGITEM_NAME ");
            sb.Append(" ,DITM.ITEM_CODE RM_ITEM_CODE ");
            //sb.Append(" ,bdtl.ITEM_QTY STD_USED_QTY ");
            sb.Append("  ,ROUND(bdtl.ITEM_QTY * " + cObj.qty + " ,5) STD_USED_QTY ");
            sb.Append(" ,duom.UOM_CODE_SHORT CLOSING_UOM_NAME ");
            sb.Append(" , duom.UOM_ID CLOSING_UOM_ID ");
            sb.Append(" , GET_DEPT_WISE_CLOSING_QTY(bdtl.ITEM_ID," + cObj.DeptID.ToString() + ") SYSTEM_OPENING_STOCK ");
            sb.Append(" FROM BOM_MST_T bmst  ");
            sb.Append(" INNER JOIN BOM_DTL_T bdtl ON bmst.BOM_ID=bdtl.BOM_MST_ID ");
            sb.Append(" INNER JOIN INV_ITEM_MASTER mitm ON bmst.BOM_ITEM_ID=mitm.ITEM_ID ");
            sb.Append(" INNER JOIN INV_ITEM_MASTER ditm ON bdtl.ITEM_ID=ditm.ITEM_ID ");
            sb.Append(" INNER JOIN UOM_INFO duom ON bdtl.ITEM_UNIT_ID=duom.UOM_ID ");
            sb.Append(" Where 1=1 ");
 //sb.Append(" WHERE bmst.BOM_ITEM_ID=6695 ");
            return sb.ToString();
        }

        public static string GetRMDtlASBOMSQLStringforGridCasting(clsPrmInventory cObj)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT ");
            sb.Append(" bmst.BOM_ID ");
            sb.Append(" ,bmst.BOM_ITEM_ID FINISHED_ITEM_ID ");
            sb.Append(" ,mitm.ITEM_NAME FINISH_ITEM_NAME ");
            sb.Append(" ,bdtl.ITEM_ID  CLOSING_ITEM_ID ");
            sb.Append(" ,ditm.ITEM_NAME CLOSINGITEM_NAME,ditm.IS_BATCH ");
            sb.Append(" ,DITM.ITEM_GROUP_ID CLOSING_ITEM_GROUP_ID ");
            sb.Append(" ,bdtl.COS_LEAD_ID ");
            sb.Append(" ,CASE WHEN BDTL.COS_LEAD_ID IS NOT NULL THEN BDTL.COS_LEAD_QTY * " + cObj.qty + " END COS_LEAD_QTY ");
            //sb.Append(" ,bdtl.IS_OWN_ITEM ");
            sb.Append("  ,ROUND((bdtl.ITEM_QTY+NVL(FN_GET_FALSE_LUG_WT(bmst.BOM_ITEM_ID,bdtl.ITEM_ID),0)) * " + cObj.qty + " ,5) STD_USED_QTY ");
            sb.Append(" ,duom.UOM_CODE_SHORT CLOSING_UOM_NAME ");
            sb.Append(" , duom.UOM_ID CLOSING_UOM_ID ");
            sb.Append(" , GET_DEPT_WISE_CLOSING_QTY(bdtl.ITEM_ID," + cObj.DeptID.ToString() + ") SYSTEM_OPENING_STOCK," + cObj.MachineId + " MACHINE_ID ");
            sb.Append(" FROM BOM_MST_T bmst  ");
            sb.Append(" INNER JOIN BOM_DTL_T bdtl ON bmst.BOM_ID=bdtl.BOM_MST_ID ");
            sb.Append(" INNER JOIN INV_ITEM_MASTER mitm ON bmst.BOM_ITEM_ID=mitm.ITEM_ID ");
            sb.Append(" INNER JOIN INV_ITEM_MASTER ditm ON bdtl.ITEM_ID=ditm.ITEM_ID ");
            sb.Append(" INNER JOIN UOM_INFO duom ON bdtl.ITEM_UNIT_ID=duom.UOM_ID ");
            sb.Append(" Where 1=1 ");
            //sb.Append(" WHERE bmst.BOM_ITEM_ID=6695 ");
            return sb.ToString();
        }

        public static string GetRMDtlASBOMSQLStringforMixer(clsPrmInventory cObj)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT ");
            sb.Append(" bmst.BOM_ID ");
            sb.Append(" ,bmst.BOM_ITEM_ID FINISHED_ITEM_ID ");
            sb.Append(" ,mitm.ITEM_NAME FINISH_ITEM_NAME ");
            sb.Append(" ,bdtl.ITEM_ID  CLOSING_ITEM_ID ");
            sb.Append(" ,ditm.ITEM_NAME CLOSINGITEM_NAME,ditm.IS_BATCH ");
            //sb.Append(" ,bdtl.ITEM_QTY STD_USED_QTY ");
            sb.Append("  ,ROUND((bdtl.ITEM_QTY/100) * " + cObj.qty + " ,5) STD_USED_QTY ");
            sb.Append(" ,duom.UOM_CODE CLOSING_UOM_NAME ");
            sb.Append(" , duom.UOM_ID CLOSING_UOM_ID ");
            sb.Append(" , GET_DEPT_WISE_CLOSING_QTY(bdtl.ITEM_ID," + cObj.DeptID.ToString() + ") SYSTEM_OPENING_STOCK," + cObj.MachineId + " MACHINE_ID ");
            sb.Append(" FROM BOM_MST_T bmst  ");
            sb.Append(" INNER JOIN BOM_DTL_T bdtl ON bmst.BOM_ID=bdtl.BOM_MST_ID ");
            sb.Append(" INNER JOIN INV_ITEM_MASTER mitm ON bmst.BOM_ITEM_ID=mitm.ITEM_ID ");
            sb.Append(" INNER JOIN INV_ITEM_MASTER ditm ON bdtl.ITEM_ID=ditm.ITEM_ID ");
            sb.Append(" INNER JOIN UOM_INFO duom ON DITM.UOM_ID=duom.UOM_ID  ");
            sb.Append(" Where 1=1 ");
            //sb.Append(" WHERE bmst.BOM_ITEM_ID=6695 ");
            return sb.ToString();
        }


        //public static string GetRMDtlASBOMSQLStringforMixer(clsPrmInventory cObj)
        //{
        //    StringBuilder sb = new StringBuilder();

        //    sb.Append(" SELECT ");
        //    sb.Append(" bmst.BOM_ID ");
        //    sb.Append(" ,bmst.BOM_ITEM_ID FINISHED_ITEM_ID ");
        //    sb.Append(" ,mitm.ITEM_NAME FINISH_ITEM_NAME ");
        //    sb.Append(" ,bdtl.ITEM_ID  CLOSING_ITEM_ID ");
        //    sb.Append(" ,ditm.ITEM_NAME CLOSINGITEM_NAME,ditm.IS_BATCH ");
        //    //sb.Append(" ,bdtl.ITEM_QTY STD_USED_QTY ");
        //    sb.Append("  ,ROUND((bdtl.ITEM_QTY/100) * " + cObj.qty + " ,5) STD_USED_QTY ");
        //    sb.Append(" ,duom.UOM_CODE CLOSING_UOM_NAME ");
        //    sb.Append(" , duom.UOM_ID CLOSING_UOM_ID ");
        //    sb.Append(" , GET_DEPT_WISE_CLOSING_QTY(bdtl.ITEM_ID," + cObj.DeptID.ToString() + ") SYSTEM_OPENING_STOCK," + cObj.MachineId + " MACHINE_ID ");
        //    sb.Append(" FROM BOM_MST_T bmst  ");
        //    sb.Append(" INNER JOIN BOM_DTL_T bdtl ON bmst.BOM_ID=bdtl.BOM_MST_ID ");
        //    sb.Append(" INNER JOIN INV_ITEM_MASTER mitm ON bmst.BOM_ITEM_ID=mitm.ITEM_ID ");
        //    sb.Append(" INNER JOIN INV_ITEM_MASTER ditm ON bdtl.ITEM_ID=ditm.ITEM_ID ");
        //    sb.Append(" INNER JOIN UOM_INFO duom ON DITM.UOM_ID=duom.UOM_ID  ");
        //    sb.Append(" Where 1=1 ");
        //    //sb.Append(" WHERE bmst.BOM_ITEM_ID=6695 ");
        //    return sb.ToString();
        //}

        public static List<dcPRODUCTION_FLOOR_CLOSING> GetRMDtlASBOM_For_SML_List(clsPrmInventory cObj, DBContext dc)
        {
            List<dcPRODUCTION_FLOOR_CLOSING> cObjList = new List<dcPRODUCTION_FLOOR_CLOSING>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder(GetRMDtlASBOM_For_SML_SQLString(cObj));

                    if (cObj.item_id > 0)
                    {
                        sb.Append("  AND bmst.BOM_ITEM_ID=@BOM_ITEM_ID ");
                        cmdInfo.DBParametersInfo.Add("@BOM_ITEM_ID", cObj.item_id);
                    }

                    if (cObj.bomid > 0)
                    {
                        sb.Append("  AND bmst.BOM_ID=@BOM_ID ");
                        cmdInfo.DBParametersInfo.Add("@BOM_ID", cObj.bomid);
                    }
                    // sb.Append(" ORDER BY cl.CLOSING_SI");
                    DBQuery dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_FLOOR_CLOSING>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcPRODUCTION_FLOOR_CLOSING> GetRMDtlASBOMList(clsPrmInventory cObj, DBContext dc)
        {
            List<dcPRODUCTION_FLOOR_CLOSING> cObjList = new List<dcPRODUCTION_FLOOR_CLOSING>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder(GetRMDtlASBOMSQLString(cObj));

                    if (cObj.item_id > 0)
                    {
                        sb.Append("  AND bmst.BOM_ITEM_ID=@BOM_ITEM_ID ");
                        cmdInfo.DBParametersInfo.Add("@BOM_ITEM_ID", cObj.item_id);
                    }

                    if (cObj.bomid > 0)
                    {
                        sb.Append("  AND bmst.BOM_ID=@BOM_ID ");
                        cmdInfo.DBParametersInfo.Add("@BOM_ID", cObj.bomid);
                    }
                   // sb.Append(" ORDER BY cl.CLOSING_SI");
                    DBQuery dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_FLOOR_CLOSING>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        //Get RM Details as BOM
        public static List<dcPRODUCTION_FLOOR_CLOSING> GetRMDtlASBOMListforGridCasting(clsPrmInventory cObj, DBContext dc)
        {
            List<dcPRODUCTION_FLOOR_CLOSING> cObjList = new List<dcPRODUCTION_FLOOR_CLOSING>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder(GetRMDtlASBOMSQLStringforGridCasting(cObj));

                    if (cObj.item_id > 0)
                    {
                        sb.Append("  AND bmst.BOM_ITEM_ID=@BOM_ITEM_ID ");
                        cmdInfo.DBParametersInfo.Add("@BOM_ITEM_ID", cObj.item_id);
                    }

                    if (cObj.bomid > 0)
                    {
                        sb.Append("  AND bmst.BOM_ID=@BOM_ID ");
                        cmdInfo.DBParametersInfo.Add("@BOM_ID", cObj.bomid);
                    }
                    sb.Append(" ORDER BY bdtl.ITEM_ID ");
                    DBQuery dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_FLOOR_CLOSING>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcPRODUCTION_FLOOR_CLOSING> GetRMDtlASBOMListforMixer(clsPrmInventory cObj, DBContext dc)
        {
            List<dcPRODUCTION_FLOOR_CLOSING> cObjList = new List<dcPRODUCTION_FLOOR_CLOSING>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder(GetRMDtlASBOMSQLStringforMixer(cObj));

                    if (cObj.item_id > 0)
                    {
                        sb.Append("  AND bmst.BOM_ITEM_ID=@BOM_ITEM_ID ");
                        cmdInfo.DBParametersInfo.Add("@BOM_ITEM_ID", cObj.item_id);
                    }

                    if (cObj.bomid > 0)
                    {
                        sb.Append("  AND bmst.BOM_ID=@BOM_ID ");
                        cmdInfo.DBParametersInfo.Add("@BOM_ID", cObj.bomid);
                    }
                    // sb.Append(" ORDER BY cl.CLOSING_SI");
                    DBQuery dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_FLOOR_CLOSING>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        //get Finished item wise raw matterial used

        public static List<dcPRODUCTION_FLOOR_CLOSING> GetFinishedItemWise_Bom_Dtl(int _Item_ID,decimal item_qty, DBContext dc)
        {
            List<dcPRODUCTION_FLOOR_CLOSING> cObjList = new List<dcPRODUCTION_FLOOR_CLOSING>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("  SELECT dtl.BOM_DTL_ID ");
                    sb.Append(" ,dtl.BOM_MST_ID ");
                    sb.Append(" ,dtl.ITEM_ID  CLOSING_ITEM_ID ");
                    sb.Append(" ,dtl.ITEM_QTY ");
                    sb.Append(" ,dtl.ITEM_UNIT_ID CLOSING_UOM_ID ");
                    sb.Append(",dtl.ITEM_WEIGHT ");
                    sb.Append(" ,dtl.IS_PRIME ");
                    sb.Append(" ,dtl.PACKAGE_ID ");
                    sb.Append(", dtl.REMARKS ");
                    sb.Append(", dtl.SLNO ");
                    sb.Append(" ,dtl.ITEM_BOM_ID ");
                    sb.Append(" ,itm.ITEM_NAME CLOSINGITEM_NAME ");
                    sb.Append(" ,U.UOM_CODE_SHORT CLOSING_UOM_NAME ");
                    sb.Append(" ,G.ITEM_GROUP_NAME ITEM_GROUP_DESC ");
                    sb.Append(" ,bMst.BOM_ITEM_DESC  BOM_ITEM_DESC ");
                    sb.Append(" ,dtl.WASTAGE_PERCENT ");
                    sb.Append(" ,fm.ITEM_NAME FINISH_ITEM_NAME ");
                    sb.Append(" ,mst.BOM_ITEM_ID FINISHED_ITEM_ID ");
                    sb.Append(" ,GET_DEPT_WISE_CLOSING_QTY(dtl.ITEM_ID,mst.FROM_DEPARTMENT_ID) SYSTEM_OPENING_STOCK ");
                    sb.Append(" ,NVL(dtl.ITEM_QTY,0)*NVL(@item_qty,0)  ISSUE_STOCK ");
                    sb.Append(" ,GET_DEPT_WISE_CLOSING_QTY(dtl.ITEM_ID,mst.FROM_DEPARTMENT_ID)-NVL(dtl.ITEM_QTY,0)*NVL(dtl.ITEM_WEIGHT,0) CLOSING_QTY ");
                    sb.Append(" FROM BOM_MST_T mst  ");
                    sb.Append(" INNER JOIN BOM_DTL_T dtl ON mst.BOM_ID=dtl.BOM_MST_ID ");
                    sb.Append("  INNER JOIN INV_ITEM_MASTER itm ON dtl.ITEM_ID=itm.ITEM_ID ");
                    sb.Append(" INNER JOIN UOM_INFO U ON dtl.ITEM_UNIT_ID=U.UOM_ID ");
                    sb.Append(" INNER JOIN INV_ITEM_GROUP G ON itm.ITEM_GROUP_ID=G.ITEM_GROUP_ID ");
                    sb.Append(" LEFT JOIN BOM_MST_T bMst ON dtl.ITEM_BOM_ID=bMst.BOM_ID ");
                    sb.Append(" INNER JOIN INV_ITEM_MASTER fm ON mst.BOM_ITEM_ID=fm.ITEM_ID ");
                    sb.Append(" WHERE 1=1 AND mst.ISACTIVE='Y' ");

                    cmdInfo.DBParametersInfo.Add("@item_qty", item_qty);
                    if (_Item_ID > 0)
                    {
                        sb.Append(" AND  mst.BOM_ITEM_ID=@Item_ID ");
                        cmdInfo.DBParametersInfo.Add("@Item_ID", _Item_ID);
                    }
                    sb.Append(" ORDER BY dtl.SLNO ");
                    DBQuery dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    //dbq.OrderBy = " dtl.SLNO ";
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_FLOOR_CLOSING>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        //Assembly Packing Material
        public static List<dcPRODUCTION_FLOOR_CLOSING> GetRMDtlASBOMPACKINGList(clsPrmInventory cObj, DBContext dc)
        {
            List<dcPRODUCTION_FLOOR_CLOSING> cObjList = new List<dcPRODUCTION_FLOOR_CLOSING>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder(GetRMDtlASBOMSQLString(cObj));

                    if (cObj.item_id > 0)
                    {
                        sb.Append("  AND bmst.BOM_ITEM_ID=@BOM_ITEM_ID ");
                        cmdInfo.DBParametersInfo.Add("@BOM_ITEM_ID", cObj.item_id);
                    }

                    if (cObj.bomid > 0)
                    {
                        sb.Append("  AND bmst.BOM_ID=@BOM_ID ");
                        cmdInfo.DBParametersInfo.Add("@BOM_ID", cObj.bomid);
                    }

                    if (cObj.ItemClassId > 0)
                    {
                        sb.Append("  AND ditm.ITEM_CLASS_ID=@ItemClassId ");
                        cmdInfo.DBParametersInfo.Add("@ItemClassId", cObj.ItemClassId);
                    }
                    // sb.Append(" ORDER BY cl.CLOSING_SI");
                    DBQuery dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_FLOOR_CLOSING>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        //Assembly Raw as BOM Material
        public static List<dcPRODUCTION_FLOOR_CLOSING> GetRMDtlASBOMASSEMBLYRAWList(clsPrmInventory cObj, DBContext dc)
        {
            List<dcPRODUCTION_FLOOR_CLOSING> cObjList = new List<dcPRODUCTION_FLOOR_CLOSING>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder(GetRMDtlASBOMSQLString(cObj));

                    if (cObj.item_id > 0)
                    {
                        sb.Append("  AND bmst.BOM_ITEM_ID=@BOM_ITEM_ID ");
                        cmdInfo.DBParametersInfo.Add("@BOM_ITEM_ID", cObj.item_id);
                    }

                    if (cObj.bomid > 0)
                    {
                        sb.Append("  AND bmst.BOM_ID=@BOM_ID ");
                        cmdInfo.DBParametersInfo.Add("@BOM_ID", cObj.bomid);
                    }

                    if (cObj.ItemClassId > 0)
                    {
                        sb.Append("  AND ditm.ITEM_CLASS_ID<>@ItemClassId ");
                        cmdInfo.DBParametersInfo.Add("@ItemClassId", cObj.ItemClassId);
                    }
                    // sb.Append(" ORDER BY cl.CLOSING_SI");
                    DBQuery dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_FLOOR_CLOSING>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }


        //Plastic Raw as BOM Material
        public static List<dcPRODUCTION_FLOOR_CLOSING> GetRMDtlASBOMPlasticRAWList(clsPrmInventory cObj, DBContext dc)
        {
            List<dcPRODUCTION_FLOOR_CLOSING> cObjList = new List<dcPRODUCTION_FLOOR_CLOSING>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder(GetRMDtlASBOMSQLString(cObj));

                    if (cObj.item_id > 0)
                    {
                        sb.Append("  AND bmst.BOM_ITEM_ID=@BOM_ITEM_ID ");
                        cmdInfo.DBParametersInfo.Add("@BOM_ITEM_ID", cObj.item_id);
                    }

                    if (cObj.bomid > 0)
                    {
                        sb.Append("  AND bmst.BOM_ID=@BOM_ID ");
                        cmdInfo.DBParametersInfo.Add("@BOM_ID", cObj.bomid);
                    }

                    if (cObj.ItemClassId > 0)
                    {
                        sb.Append("  AND ditm.ITEM_CLASS_ID<>@ItemClassId ");
                        cmdInfo.DBParametersInfo.Add("@ItemClassId", cObj.ItemClassId);
                    }
                    // sb.Append(" ORDER BY cl.CLOSING_SI");
                    DBQuery dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_FLOOR_CLOSING>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcPRODUCTION_FLOOR_CLOSING> GetBatchStockByItemId(DateTime pToDate, int pItemId, int pDeptId, int pStlmId, int pMachineId, int prodId, int pBattTypeId, DBContext dc)
        {

            List<dcPRODUCTION_FLOOR_CLOSING> cRptList = new List<dcPRODUCTION_FLOOR_CLOSING>();


            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();
                cmdInfo.DBParametersInfo.Clear();

                //cmdInfo.DBParametersInfo.Add(":P_FROM_DATE", "");
                //cmdInfo.DBParametersInfo.Add(":P_TO_DATE", pToDate);
                cmdInfo.DBParametersInfo.Add(":P_DEPT_ID", pDeptId);
                cmdInfo.DBParametersInfo.Add(":P_STLM_ID", pStlmId);

                cmdInfo.DBParametersInfo.Add(":P_CLOSING_ITEM_ID", pItemId);
                cmdInfo.DBParametersInfo.Add(":P_MACHINE_ID", pMachineId);
                cmdInfo.DBParametersInfo.Add(":P_PROD_ID", prodId);
                cmdInfo.DBParametersInfo.Add(":P_BAT_TYPE_ID", pBattTypeId);

                DBQuery dbq = new DBQuery();

                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;
                cmdInfo.CommandText = "SP_PRODUCTION_BATCH_STOCK";

                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);

                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;
                sb.Append(" SELECT STK.PROD_BATCH_NO,STK.ITEM_ID CLOSING_ITEM_ID,STK.ITEM_NAME CLOSINGITEM_NAME ");
                sb.Append(" ,STK.DEPT_ID,STK.PROD_ID,STK.UOM_ID CLOSING_UOM_ID,STK.UOM_NAME CLOSING_UOM_NAME,STK.MACHINE_ID ");
                sb.Append(" ,STK.CLOSING_QTY,STK.TEMP_BATCH_QTY,STK.UNAUTHO_PROD_QTY,STK.REJECT_QTY ");
                sb.Append(" ,NVL(STK.CLOSING_QTY,0)-NVL(STK.TEMP_BATCH_QTY,0)-NVL(STK.UNAUTHO_PROD_QTY,0) SYSTEM_OPENING_STOCK ");
                sb.Append(" FROM TEMP_PROD_BATCH_STOCK STK ");

                DBQuery dbqtemp = new DBQuery();

                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);

                //,,,,,,,,,,,,,,,,,


                foreach (DataRow dRow in dtData.Rows)
                {
                    dcPRODUCTION_FLOOR_CLOSING stk = new dcPRODUCTION_FLOOR_CLOSING();

                    stk.PROD_BATCH_NO = dRow["PROD_BATCH_NO"].ToString();
                    stk.CLOSING_ITEM_ID = Conversion.DBNullIntToZero(dRow["CLOSING_ITEM_ID"]);

                    stk.CLOSINGITEM_NAME = dRow["CLOSINGITEM_NAME"].ToString();
                    stk.PROD_MST_ID = Conversion.DBNullIntToZero(dRow["PROD_ID"]);
                    stk.CLOSING_UOM_NAME = dRow["CLOSING_UOM_NAME"].ToString();
                    stk.CLOSING_UOM_ID = Conversion.DBNullIntToZero(dRow["CLOSING_UOM_ID"]);
                    stk.MACHINE_ID = Conversion.DBNullIntToZero(dRow["MACHINE_ID"]);
                    stk.CLOSING_QTY = Conversion.DBNullDecimalToZero(dRow["CLOSING_QTY"]);
                    stk.TEMP_BATCH_QTY = Conversion.DBNullDecimalToZero(dRow["TEMP_BATCH_QTY"]);
                    stk.UNAUTHO_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["UNAUTHO_PROD_QTY"]);
                    stk.REJECTED_QTY = Conversion.DBNullDecimalToZero(dRow["REJECT_QTY"]);
                   // -stk.TEMP_BATCH_QTY;
                    //if(stk.MACHINE_ID == pMachineId)
                    //{
                    //List<dcPROD_TEMP_BATCH_INFO> batchList = PROD_TEMP_BATCH_INFOBL.GetTempBatchInfo(pMachineId, pItemId, null);
                    //List<dcPROD_TEMP_BATCH_INFO> batchList = PROD_TEMP_BATCH_INFOBL.GetTempBatchDeptItemInfo(pMachineId, pItemId, null); 
                    //foreach (var batch in batchList)
                    //{
                    //    if (batch.PROD_BATCH_NO == stk.PROD_BATCH_NO)
                    //    {
                    //        stk.SYSTEM_OPENING_STOCK += batch.USED_QTY;
                    //    }
                    //}
                    //}

                    if(prodId > 0)
                    {
                    
                    //if(stk.PROD_MST_ID == prodId)
                    {
                        stk.SYSTEM_OPENING_STOCK = stk.CLOSING_QTY - stk.REJECTED_QTY; // - stk.TEMP_BATCH_QTY
                    }
                 
                    }
                    else
                    {
                        stk.SYSTEM_OPENING_STOCK = stk.CLOSING_QTY - stk.UNAUTHO_PROD_QTY - stk.REJECTED_QTY; 
                    }

                    if (Math.Round(stk.SYSTEM_OPENING_STOCK,4) > 0)
                        cRptList.Add(stk);
                }

            }
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }

            return cRptList;

        }

        public static decimal GetBatchStockByClosingItemId(int pItemId, int pDeptId, int pStlmId, int pMachineId, int prodId, int pBattTypeId,string batchNo, DBContext dc)
        {

            List<dcPRODUCTION_FLOOR_CLOSING> cRptList = new List<dcPRODUCTION_FLOOR_CLOSING>();
            decimal ClosingQty = 0;

            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();
                cmdInfo.DBParametersInfo.Clear();

                //cmdInfo.DBParametersInfo.Add(":P_FROM_DATE", "");
                //cmdInfo.DBParametersInfo.Add(":P_TO_DATE", pToDate);
                cmdInfo.DBParametersInfo.Add(":P_DEPT_ID", pDeptId);
                cmdInfo.DBParametersInfo.Add(":P_STLM_ID", pStlmId);

                cmdInfo.DBParametersInfo.Add(":P_CLOSING_ITEM_ID", pItemId);
                cmdInfo.DBParametersInfo.Add(":P_MACHINE_ID", pMachineId);
                cmdInfo.DBParametersInfo.Add(":P_PROD_ID", prodId);
                cmdInfo.DBParametersInfo.Add(":P_BAT_TYPE_ID", pBattTypeId);

                DBQuery dbq = new DBQuery();

                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;
                cmdInfo.CommandText = "SP_PRODUCTION_BATCH_STOCK";

                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);

                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;
                sb.Append(" SELECT STK.PROD_BATCH_NO,STK.ITEM_ID CLOSING_ITEM_ID,STK.ITEM_NAME CLOSINGITEM_NAME ");
                sb.Append(" ,STK.DEPT_ID,STK.PROD_ID,STK.UOM_ID CLOSING_UOM_ID,STK.UOM_NAME CLOSING_UOM_NAME,STK.MACHINE_ID ");
                sb.Append(" ,STK.CLOSING_QTY,STK.TEMP_BATCH_QTY,STK.UNAUTHO_PROD_QTY,STK.REJECT_QTY ");
                sb.Append(" ,NVL(STK.CLOSING_QTY,0)-NVL(STK.TEMP_BATCH_QTY,0)-NVL(STK.UNAUTHO_PROD_QTY,0) SYSTEM_OPENING_STOCK ");
                sb.Append(" FROM TEMP_PROD_BATCH_STOCK STK ");
                sb.Append(" WHERE 1=1  ");

                sb.Append("  AND STK.PROD_BATCH_NO=@batchNo ");
                cmdInfotemp.DBParametersInfo.Add("@batchNo", batchNo);

                DBQuery dbqtemp = new DBQuery();

                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfotemp.CommandTimeout = 600;

                cmdInfotemp.CommandText = sb.ToString();
                cmdInfotemp.CommandType = CommandType.Text;
                dbqtemp.DBCommandInfo = cmdInfotemp;
                DataTable dtData = DBQuery.ExecuteDBQuery(dbqtemp, dc);

                //sb.Append("  AND STK.PROD_BATCH_NO=@batchNo ");
                //cmdInfo.DBParametersInfo.Add("@batchNo", batchNo);

                //DBQuery dbqtemp = new DBQuery();

                //dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                //cmdInfo.CommandTimeout = 600;

                //cmdInfo.CommandText = sb.ToString();
                //cmdInfo.CommandType = CommandType.Text;
                //dbq.DBCommandInfo = cmdInfo;
                //DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);

                //,,,,,,,,,,,,,,,,,


                foreach (DataRow dRow in dtData.Rows)
                {
                    dcPRODUCTION_FLOOR_CLOSING stk = new dcPRODUCTION_FLOOR_CLOSING();

                    stk.PROD_BATCH_NO = dRow["PROD_BATCH_NO"].ToString();
                    stk.CLOSING_ITEM_ID = Conversion.DBNullIntToZero(dRow["CLOSING_ITEM_ID"]);

                    stk.CLOSINGITEM_NAME = dRow["CLOSINGITEM_NAME"].ToString();
                    stk.PROD_MST_ID = Conversion.DBNullIntToZero(dRow["PROD_ID"]);
                    stk.CLOSING_UOM_NAME = dRow["CLOSING_UOM_NAME"].ToString();
                    stk.CLOSING_UOM_ID = Conversion.DBNullIntToZero(dRow["CLOSING_UOM_ID"]);
                    stk.MACHINE_ID = Conversion.DBNullIntToZero(dRow["MACHINE_ID"]);
                    stk.CLOSING_QTY = Conversion.DBNullDecimalToZero(dRow["CLOSING_QTY"]);
                    stk.TEMP_BATCH_QTY = Conversion.DBNullDecimalToZero(dRow["TEMP_BATCH_QTY"]);
                    stk.UNAUTHO_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["UNAUTHO_PROD_QTY"]);
                    stk.REJECTED_QTY = Conversion.DBNullDecimalToZero(dRow["REJECT_QTY"]);

                    ClosingQty = stk.CLOSING_QTY - stk.REJECTED_QTY;
                   

                
                }

            }
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }

            return ClosingQty;

        }

        public static List<dcPRODUCTION_FLOOR_CLOSING> GetClosingItemStockByItemId(int pItemId, int pDeptId, int pStlmId,int pMachineId, DBContext dc)
        {
            List<dcPRODUCTION_FLOOR_CLOSING> cObjList = new List<dcPRODUCTION_FLOOR_CLOSING>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder(GetBatchWiseStockSQLString(pMachineId));

                    //if (pItemId > 0)
                    {
                        sb.Append(" AND isd.ITEM_ID=@ITEM_ID ");
                        cmdInfo.DBParametersInfo.Add("@ITEM_ID", pItemId);
                    }

                    //if (pDeptId > 0)
                    {
                        sb.Append(" AND isd.DEPARTMENT_ID=@pDeptId ");
                        cmdInfo.DBParametersInfo.Add("@pDeptId", pDeptId);
                    }

                     sb.Append(" AND isd.STLM_ID=@pStlmId ");
                     cmdInfo.DBParametersInfo.Add("@pStlmId", pStlmId);

                     sb.Append("  GROUP BY ISD.PROD_BATCH_NO,IM.ITEM_ID,IM.ITEM_NAME,IM.UOM_ID,UOM.UOM_CODE,TB.MACHINE_ID "); //,PMST.AUTH_STATUS
                    sb.Append("  ORDER BY SUBSTR(PROD_BATCH_NO,1,5), CASE WHEN SUBSTR( ISD.PROD_BATCH_NO,6)='M' THEN 1 WHEN  SUBSTR( ISD.PROD_BATCH_NO,6)='E' THEN 2 WHEN  SUBSTR( ISD.PROD_BATCH_NO,6)='N' THEN 3 END ");

                    DBQuery dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_FLOOR_CLOSING>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcPRODUCTION_FLOOR_CLOSING> GetUnformStockByItemId(int pItemId,DBContext dc)
        {
            List<dcPRODUCTION_FLOOR_CLOSING> cObjList = new List<dcPRODUCTION_FLOOR_CLOSING>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT IM.ITEM_ID CLOSING_ITEM_ID,IM.ITEM_NAME CLOSINGITEM_NAME,IM.UOM_ID CLOSING_UOM_ID,UOM.UOM_CODE CLOSING_UOM_NAME ");
                    sb.Append(" ,GET_UNFORM_QTY(IM.ITEM_ID) SYSTEM_OPENING_STOCK ");
                    sb.Append(" FROM INV_ITEM_MASTER IM ");
                    sb.Append(" INNER JOIN UOM_INFO UOM ON IM.UOM_ID=UOM.UOM_ID ");
                    sb.Append(" WHERE 1=1 ");

                    sb.Append(" AND IM.ITEM_ID=@ITEM_ID ");
                    cmdInfo.DBParametersInfo.Add("@ITEM_ID", pItemId);

                    DBQuery dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_FLOOR_CLOSING>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }



        #region Afer Save QA

        public static string GetProductionClosingDtlsafterQASQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" Select ");
            sb.Append("  cl.CLOSING_ID ");
            sb.Append(" ,cl.PROD_MST_ID  ");
            sb.Append(" ,cl.CLOSING_ITEM_ID  ");
            sb.Append(" ,cl.CLOSING_QTY  ");
            sb.Append(" ,cl.CLOSING_UOM_ID  ");
            sb.Append(" ,cl.CLOSING_REMARKS  ");
            sb.Append(" ,cl.CLOSING_SI  ");
            sb.Append(" ,GET_DEPT_WISE_CLOSING_QTY(cl.CLOSING_ITEM_ID,m.DEPT_ID) SYSTEM_OPENING_STOCK  ");
            sb.Append(" ,cl.MANUAL_OPENING_STOCK  ");
            sb.Append(" ,cl.ISSUE_STOCK  ");
            sb.Append(" ,cl.WASTAGE_QTY  ");
            sb.Append(" ,cl.REJECTED_QTY  ");
            sb.Append(" ,cl.POSITIVE_DEV  ");
            sb.Append(" ,cl.NEGATIVE_DEV ");
            sb.Append(" ,cl.REUSE_QTY ");
            sb.Append(" ,cl.FINISHED_ITEM_ID ");
            sb.Append(" ,cl.ISMANUAL ");
            sb.Append(" ,cl.STD_USED_QTY ");
            sb.Append(" ,cl.RECOVERY_QTY ");
            sb.Append(" ,cl.ASM_OP_RECOVERY_QTY ");
            sb.Append(" ,cl.ASM_OP_REJECT_QTY ");
            sb.Append(" ,itm.ITEM_NAME CLOSINGITEM_NAME ");
            sb.Append("  ,finishItem.ITEM_NAME FINISH_ITEM_NAME ");
            sb.Append("  ,U.UOM_CODE_SHORT CLOSING_UOM_NAME ");
            sb.Append("  ,MC.MACHINE_NAME ");
            sb.Append("  ,cl.MACHINE_ID ");
            sb.Append("  FROM ");
            sb.Append("  production_mst m  INNER JOIN PRODUCTION_FLOOR_CLOSING cl ON m.PROD_ID=cl.PROD_MST_ID ");
            sb.Append("  INNER JOIN INV_ITEM_MASTER itm ON cl.CLOSING_ITEM_ID=itm.ITEM_ID ");
            sb.Append("  LEFT JOIN INV_ITEM_MASTER finishItem ON cl.FINISHED_ITEM_ID=finishItem.ITEM_ID ");
            sb.Append("  INNER JOIN UOM_INFO U ON cl.CLOSING_UOM_ID=U.UOM_ID ");
            sb.Append("  LEFT JOIN MACHINE_MST MC ON MC.MACHINE_ID=cl.MACHINE_ID ");
            
            sb.Append(" Where 1=1  ");
            return sb.ToString();
        }

        public static string GetProductionClosingDtlsafterQASaveSQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" Select ");
            sb.Append("  cl.CLOSING_ID ");
            sb.Append(" ,cl.PROD_MST_ID  ");
            sb.Append(" ,cl.CLOSING_ITEM_ID  ");
            sb.Append(" ,cl.CLOSING_QTY  ");
            sb.Append(" ,cl.CLOSING_UOM_ID  ");
            sb.Append(" ,cl.CLOSING_REMARKS  ");
            sb.Append(" ,cl.CLOSING_SI  ");
            sb.Append(" ,GET_DEPT_WISE_CLOSING_QTY(cl.CLOSING_ITEM_ID,m.DEPT_ID) SYSTEM_OPENING_STOCK  ");
            sb.Append(" ,cl.MANUAL_OPENING_STOCK  ");
            sb.Append(" ,cl.ISSUE_STOCK  ");
            sb.Append(" ,cl.WASTAGE_QTY  ");
            sb.Append(" ,cl.REJECTED_QTY  ");
            sb.Append(" ,cl.POSITIVE_DEV  ");
            sb.Append(" ,cl.NEGATIVE_DEV ");
            sb.Append(" ,cl.REUSE_QTY ");
            sb.Append(" ,cl.FINISHED_ITEM_ID ");
            sb.Append(" ,cl.ISMANUAL ");
            sb.Append(" ,cl.STD_USED_QTY ");
            sb.Append(" ,cl.RECOVERY_QTY ");
            sb.Append(" ,cl.ASM_OP_RECOVERY_QTY ");
            sb.Append(" ,cl.ASM_OP_REJECT_QTY ");
            sb.Append(" ,itm.ITEM_NAME CLOSINGITEM_NAME ");
            sb.Append("  ,finishItem.ITEM_NAME FINISH_ITEM_NAME ");
            sb.Append("  ,U.UOM_CODE_SHORT CLOSING_UOM_NAME ");
            sb.Append("  ,MC.MACHINE_NAME ");
            sb.Append("  ,cl.MACHINE_ID ");
            sb.Append("  FROM ");
            sb.Append("  production_mst m  INNER JOIN PRODUCTION_FLOOR_CLOSING cl ON m.PROD_ID=cl.PROD_MST_ID ");
            sb.Append("  INNER JOIN INV_ITEM_MASTER itm ON cl.CLOSING_ITEM_ID=itm.ITEM_ID ");
            sb.Append("  LEFT JOIN INV_ITEM_MASTER finishItem ON cl.FINISHED_ITEM_ID=finishItem.ITEM_ID ");
            sb.Append("  INNER JOIN UOM_INFO U ON cl.CLOSING_UOM_ID=U.UOM_ID ");
            sb.Append("  LEFT JOIN MACHINE_MST MC ON MC.MACHINE_ID=cl.MACHINE_ID ");
            sb.Append("  INNER JOIN PROD_QA_OPERATION_DTL dtlQA ON cl.FINISHED_ITEM_ID=dtlQA.ITEM_ID ");

            sb.Append(" INNER JOIN PROD_QA_OPERATION_MST prod_qa ON dtlQA.PROD_QA_ID=prod_qa.PROD_QA_ID ");
            sb.Append(" Where 1=1  ");
            return sb.ToString();
        }



        public static List<dcPRODUCTION_FLOOR_CLOSING> GetProductionClosingDtlsByProdIDAfterPendingQA(int pProd_id, List<string> item_idlist, DBContext dc)
        {
            List<dcPRODUCTION_FLOOR_CLOSING> cObjList = new List<dcPRODUCTION_FLOOR_CLOSING>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder(GetProductionClosingDtlsafterQASQLString());

                    // StringBuilder sb = new StringBuilder(GetProductionClosingDtlsWithDeptIDSQLString());
                    if (pProd_id > 0)
                    {
                        sb.Append("  AND cl.PROD_MST_ID=@PROD_MST_ID ");
                        cmdInfo.DBParametersInfo.Add("@PROD_MST_ID", pProd_id);
                    }
                    

                    sb.Append(" AND cl.FINISHED_ITEM_ID in (SELECT distinct b.ITEM_ID FROM production_mst a INNER JOIN PRODUCTION_DTL b ON a.PROD_ID=b.PROD_MST_ID WHERE b.IS_QA_PASS='N' AND a.PROD_ID=@PROD_MST_IDpend ) ");
                    cmdInfo.DBParametersInfo.Add("@PROD_MST_IDpend", pProd_id);
                    //string stritemList = string.Empty;

                    //if (item_idlist.Count > 0)
                    //{

                    //    stritemList = string.Join(",", item_idlist.ToArray());

                    //}

                    //if (stritemList != string.Empty)
                    //{

                    //    sb.Append(string.Format(" AND cl.FINISHED_ITEM_ID IN ({0}) ", stritemList));
                    //}
                    sb.Append(" ORDER BY  cl.CLOSING_SI, finishItem.ITEM_NAME,itm.ITEM_NAME ");
                    DBQuery dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_FLOOR_CLOSING>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcPRODUCTION_FLOOR_CLOSING> GetProductionClosingDtlsByProdIDAfterSaveQA(int pProd_id, int pProdQAID, List<string> item_idlist, DBContext dc)
        {
            List<dcPRODUCTION_FLOOR_CLOSING> cObjList = new List<dcPRODUCTION_FLOOR_CLOSING>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder(GetProductionClosingDtlsafterQASaveSQLString());

                    // StringBuilder sb = new StringBuilder(GetProductionClosingDtlsWithDeptIDSQLString());
                    if (pProd_id > 0)
                    {
                        sb.Append("  AND cl.PROD_MST_ID=@PROD_MST_ID ");
                        cmdInfo.DBParametersInfo.Add("@PROD_MST_ID", pProd_id);
                    }
                    sb.Append("  AND prod_qa.PROD_QA_ID=@pProdQAID ");
                    cmdInfo.DBParametersInfo.Add("@pProdQAID", pProdQAID);

                    sb.Append(" AND cl.FINISHED_ITEM_ID in (SELECT distinct b.ITEM_ID FROM production_mst a INNER JOIN PRODUCTION_DTL b ON a.PROD_ID=b.PROD_MST_ID WHERE b.IS_QA_PASS='Y' AND a.PROD_ID=@PROD_MST_IDpend ) ");
                    cmdInfo.DBParametersInfo.Add("@PROD_MST_IDpend", pProd_id);
                    //string stritemList = string.Empty;

                    //if (item_idlist.Count > 0)
                    //{

                    //    stritemList = string.Join(",", item_idlist.ToArray());

                    //}

                    //if (stritemList != string.Empty)
                    //{

                    //    sb.Append(string.Format(" AND cl.FINISHED_ITEM_ID IN ({0}) ", stritemList));
                    //}
                    sb.Append(" ORDER BY  cl.CLOSING_SI, finishItem.ITEM_NAME,itm.ITEM_NAME ");
                    DBQuery dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_FLOOR_CLOSING>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }


        #endregion
    }
}
