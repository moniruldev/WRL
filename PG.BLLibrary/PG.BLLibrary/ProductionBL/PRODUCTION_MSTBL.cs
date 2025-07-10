using PG.BLLibrary.InventoryBL;
using PG.BLLibrary.ProductionDC;
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
    public class PRODUCTION_MSTBL
    {
        public static DataLoadOptions PRODUCTION_MSTLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcPRODUCTION_MST>(obj => obj.relatedclassname);
            return dlo;
        }



        public static string GetBatchString()
        {
            StringBuilder sb = new StringBuilder();


            sb.Append(" SELECT Distinct  ");
            //sb.Append("  mst.PROD_NO  ");
            //sb.Append(" ,mst.PROD_ID  ");
            //sb.Append(" ,mst.FACTORY_ID ");
            //sb.Append(" ,mst.SHIFT_ID ");
            //sb.Append(" ,mst.SUPERVISOR_ID ");
            //sb.Append(" ,mst.ENTRY_BY_ID ");
            //sb.Append(" ,mst.ENTRY_DATE ");
            //sb.Append(" ,mst.EDIT_BY_ID ");
            //sb.Append(" ,mst.EDIT_DATE ");
            //sb.Append(" ,mst.DEPT_ID ");
            //sb.Append(" ,mst.FORECUST_ID ");
            //sb.Append(" ,mst.REJECTED_QTY ");
            //sb.Append(" ,mst.REF_NO_MANUAL ");
            //sb.Append(" ,TO_CHAR( mst.BATCH_STARTTIME , 'DD-MON-YYYY') BATCH_STARTTIME ");
            //sb.Append(" ,TO_CHAR( mst.BATCH_ENDTIME, 'DD-MON-YYYY')  BATCH_ENDTIME ");
            //sb.Append(" ,mst.AUTH_STATUS ");
            //sb.Append(" ,mst.AUTH_BY_ID ");
            //sb.Append(" ,mst.AUTH_DATE ");
            //sb.Append(" ,mst.STARTTIME ");
            //sb.Append(" ,mst.ENDTIME ");
            //sb.Append(" ,mst.BATCH_ID ");
            //sb.Append(" ,TO_CHAR( mst.PRODUCTION_DATE, 'DD-MON-YYYY')  PRODUCTION_DATE ");
            sb.Append(" ,dept.DEPARTMENT_NAME,mst.PROD_BATCH_NO ");

            sb.Append(" FROM PRODUCTION_MST mst  ");
            sb.Append(" INNER JOIN DEPARTMENT_INFO dept ON mst.DEPT_ID=dept.DEPARTMENT_ID  ");

            sb.Append(" WHERE 1=1  and  mst.PROD_BATCH_NO IS NOT NULL");
            return sb.ToString();
        }

        //Get Batch No By Production Date

        public static string GetProductionDateWiseBatchString()
        {
            StringBuilder sb = new StringBuilder();


            sb.Append(" SELECT   ");
            sb.Append("  mst.PROD_NO  ");
            sb.Append(" ,mst.PROD_ID  ");
            sb.Append(" ,mst.FACTORY_ID ");
            sb.Append(" ,mst.SHIFT_ID ");
            sb.Append(" ,mst.SUPERVISOR_ID ");
            sb.Append(" ,mst.ENTRY_BY_ID ");
            sb.Append(" ,mst.ENTRY_DATE ");
            sb.Append(" ,mst.EDIT_BY_ID ");
            sb.Append(" ,mst.EDIT_DATE ");
            sb.Append(" ,mst.DEPT_ID ");
            sb.Append(" ,mst.FORECUST_ID ");
            sb.Append(" ,mst.REJECTED_QTY ");
            sb.Append(" ,mst.REF_NO_MANUAL ");
            sb.Append(" ,TO_CHAR( mst.BATCH_STARTTIME , 'DD-MON-YYYY') BATCH_STARTTIME ");
            sb.Append(" ,TO_CHAR( mst.BATCH_ENDTIME, 'DD-MON-YYYY')  BATCH_ENDTIME ");
            sb.Append(" ,mst.AUTH_STATUS ");
            sb.Append(" ,mst.AUTH_BY_ID ");
            sb.Append(" ,mst.AUTH_DATE ");
            sb.Append(" ,mst.STARTTIME ");
            sb.Append(" ,mst.ENDTIME ");
            sb.Append(" ,mst.BATCH_ID ");
            sb.Append(" ,TO_CHAR( mst.PRODUCTION_DATE, 'DD-MON-YYYY')  PRODUCTION_DATE ");
            sb.Append(" ,dept.DEPARTMENT_NAME,mst.PROD_BATCH_NO ");

            sb.Append(" FROM PRODUCTION_MST mst  ");
            sb.Append(" INNER JOIN DEPARTMENT_INFO dept ON mst.DEPT_ID=dept.DEPARTMENT_ID  ");

            sb.Append(" WHERE 1=1  and  mst.PROD_BATCH_NO IS NOT NULL");
            return sb.ToString();
        }

        public static string GetProductionMstList_Basic_SqlString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT mst.*   ");
            sb.Append(" from PRODUCTION_MST mst ");
            sb.Append(" where 1=1 ");
            return sb.ToString();
        }

        public static string GetProductionMstListString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT   ");
            sb.Append("  mst.PROD_NO  ");
            sb.Append(" ,mst.PROD_ID  ");
            sb.Append(" ,mst.FACTORY_ID ");
            sb.Append(" ,mst.SHIFT_ID ");
            sb.Append(" ,mst.SUPERVISOR_ID ");
            sb.Append(" ,mst.ENTRY_BY_ID ");
            sb.Append(" ,us.FULLNAME   ENTRY_BY ");
            sb.Append(" ,mst.ENTRY_DATE ");
            sb.Append(" ,mst.EDIT_BY_ID ");
            sb.Append(" ,mst.EDIT_DATE ");
            sb.Append(" ,mst.DEPT_ID ");
            sb.Append(" ,mst.FORECUST_ID ");
            sb.Append(" ,mst.REJECTED_QTY ");
            sb.Append(" ,mst.REF_NO_MANUAL ");
            sb.Append(" ,TO_CHAR( mst.BATCH_STARTTIME , 'DD-MON-YYYY') BATCH_STARTTIME ");
            sb.Append(" ,TO_CHAR( mst.BATCH_ENDTIME, 'DD-MON-YYYY')  BATCH_ENDTIME ");
            sb.Append(" ,mst.AUTH_STATUS ");
            sb.Append(" ,mst.AUTH_BY_ID ");
            sb.Append(" ,mst.AUTH_DATE ");
            sb.Append(" ,mst.STARTTIME ");
            sb.Append(" ,mst.ENDTIME ");
            sb.Append(" ,mst.BATCH_ID ");
            sb.Append(" ,mst.AUTH_STATUS ");
            sb.Append(" ,mst.FORMATION_STATUS ");
            sb.Append(" ,mst.STLM_ID ");
            
            sb.Append(" ,TO_CHAR( mst.PRODUCTION_DATE, 'DD-MON-YYYY')  PRODUCTION_DATE ");
            sb.Append(" ,mst.PROCESS_CODE ");
            sb.Append(" ,svr.FULL_NAME,dept.DEPARTMENT_NAME,shift.SHIFT_NAME, Fmst.FOR_MONTH FORECUSTMONTH ,Fmst.FOR_YEAR  FORECUSTYEAR,mst.PROD_BATCH_NO,mst.PROD_REMARKS  ");
            sb.Append("  ,mst.SHIFT_INCHARGE ");
            sb.Append(" ,incharge.FULL_NAME SHIFT_INCHARGE_NAME ");
            sb.Append(" ,MST.ENTRY_BY_REF_ID,ENTRYREF.FULL_NAME ENTRY_BY_REF_NAME ");
            sb.Append(" ,(Select PROD_ID from  PRODUCTION_MST where REF_PROD_ID=mst.PROD_ID AND ROWNUM=1) UN_LOADED_PROD_ID ");
            sb.Append(" ,stlm.NAME STLM_NAME ");
            sb.Append(" FROM PRODUCTION_MST mst  ");
            sb.Append(" INNER JOIN SUPPERVISOR_MST  svr ON mst.SUPERVISOR_ID = svr.EMP_ID AND svr.DEPT_ID=mst.DEPT_ID   ");
            sb.Append(" INNER JOIN DEPARTMENT_INFO dept ON mst.DEPT_ID=dept.DEPARTMENT_ID  ");
            sb.Append("  INNER JOIN STORAGE_LOCATION_MST stlm ON mst.STLM_ID=stlm.STLM_ID ");
            sb.Append(" INNER JOIN SHIFT_MST shift ON mst.SHIFT_ID=shift.SHIFT_ID  ");
            sb.Append(" LEFT JOIN  PROD_TBLFORECAST_MST Fmst ON mst.FORECUST_ID=Fmst.FC_ID ");
            sb.Append(" INNER JOIN TBLUSER us ON mst.ENTRY_BY_ID=us.USERID ");
            sb.Append(" LEFT JOIN SUPPERVISOR_MST  incharge  ON mst.SHIFT_INCHARGE = incharge.EMP_ID  and mst.DEPT_ID=incharge.DEPT_ID  ");
            sb.Append(" LEFT JOIN SUPPERVISOR_MST  entryref ON MST.ENTRY_BY_REF_ID = entryref.EMP_ID AND entryref.DEPT_ID=mst.DEPT_ID ");
            sb.Append(" WHERE 1=1  ");
            return sb.ToString();
           // AND incharge.DEPT_ID=mst.DEPT_ID
        }
        public static string GetProductionFormationLoadingList_SqlString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT   ");
            sb.Append("  mst.PROD_NO  ");
            sb.Append(" ,mst.PROD_ID  ");
            sb.Append(" ,mst.FACTORY_ID ");
            sb.Append(" ,mst.SHIFT_ID ");
            sb.Append(" ,mst.SUPERVISOR_ID ");
            sb.Append(" ,mst.ENTRY_BY_ID ");
            sb.Append(" ,us.FULLNAME   ENTRY_BY ");
            sb.Append(" ,mst.ENTRY_DATE ");
            sb.Append(" ,mst.EDIT_BY_ID ");
            sb.Append(" ,mst.EDIT_DATE ");
            sb.Append(" ,mst.DEPT_ID ");
            sb.Append(" ,mst.FORECUST_ID ");
            sb.Append(" ,mst.REJECTED_QTY ");
            sb.Append(" ,mst.IS_UNLOAD ");
            sb.Append(" ,mst.REF_NO_MANUAL ");
            sb.Append(" ,TO_CHAR( mst.BATCH_STARTTIME , 'DD-MON-YYYY') BATCH_STARTTIME ");
            sb.Append(" ,TO_CHAR( mst.BATCH_ENDTIME, 'DD-MON-YYYY')  BATCH_ENDTIME ");
            sb.Append(" ,mst.AUTH_STATUS ");
            sb.Append(" ,mst.AUTH_BY_ID ");
            sb.Append(" ,mst.AUTH_DATE ");
            sb.Append(" ,mst.STARTTIME ");
            sb.Append(" ,mst.ENDTIME ");
            sb.Append(" ,mst.BATCH_ID ");
            sb.Append(" ,unLoadMst.PROD_NO as UN_LOAD_PROD_NO ");
            sb.Append(" ,unLoadMst.IS_UNLOAD as IS_UNLOADED ");
            sb.Append(" ,unLoadMst.AUTH_STATUS as UN_LOAD_AUTH_STATUS  ");
            sb.Append(" ,TO_CHAR( mst.PRODUCTION_DATE, 'DD-MON-YYYY')  PRODUCTION_DATE ");
            sb.Append(" ,mst.PROCESS_CODE ");
            sb.Append(" ,svr.FULL_NAME,dept.DEPARTMENT_NAME,shift.SHIFT_NAME, Fmst.FOR_MONTH FORECUSTMONTH ,Fmst.FOR_YEAR  FORECUSTYEAR, ");
            sb.Append(" CONCAT( mst.PROD_BATCH_NO ,CONCAT('-' ,TO_CHAR(NVL( circuitObj.FORMATION_BATCH_SL,0)))) PROD_BATCH_NO");
            sb.Append("  ,mst.SHIFT_INCHARGE ");
            sb.Append(" ,incharge.FULL_NAME SHIFT_INCHARGE_NAME ");
            sb.Append(" ,mm.MACHINE_NAME ");
            //sb.Append(" ,(CASE WHEN (Select COUNT(*) from PRODUCTION_MST where REF_PROD_ID=mst.PROD_ID)>0 THEN 'Y' ELSE 'N' END)IS_UNLOADED  ");
            sb.Append(" FROM PRODUCTION_MST mst  ");
            sb.Append(" LEFT JOIN PRODUCTION_MST unLoadMst on mst.PROD_ID=unLoadMst.REF_PROD_ID ");
            sb.Append(" INNER JOIN SUPPERVISOR_MST  svr ON mst.SUPERVISOR_ID = svr.EMP_ID   AND svr.DEPT_ID=mst.DEPT_ID ");
            sb.Append(" INNER JOIN DEPARTMENT_INFO dept ON mst.DEPT_ID=dept.DEPARTMENT_ID  ");
            sb.Append(" INNER JOIN SHIFT_MST shift ON mst.SHIFT_ID=shift.SHIFT_ID  ");
            sb.Append(" LEFT JOIN  PROD_TBLFORECAST_MST Fmst ON mst.FORECUST_ID=Fmst.FC_ID ");
            sb.Append(" INNER JOIN TBLUSER us ON mst.ENTRY_BY_ID=us.USERID ");
            sb.Append(" LEFT JOIN SUPPERVISOR_MST  incharge  ON mst.SHIFT_INCHARGE = incharge.EMP_ID  AND incharge.DEPT_ID=mst.DEPT_ID ");
            sb.Append(" LEFT JOIN PROD_FORMATION_CIRCUIT_INFO  circuitObj on mst.PROD_ID=circuitObj.PROD_ID ");
            sb.Append(" LEFT JOIN MACHINE_MST mm on circuitObj.MACHINE_ID=mm.MACHINE_ID ");
            sb.Append(" WHERE 1=1  ");
            return sb.ToString();
        }

        public static string GetProductionSolarChargeLoadingList_SqlString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT   ");
            sb.Append("  mst.PROD_NO  ");
            sb.Append(" ,mst.PROD_ID  ");
            sb.Append(" ,mst.FACTORY_ID ");
            sb.Append(" ,mst.SHIFT_ID ");
            sb.Append(" ,mst.SUPERVISOR_ID ");
            sb.Append(" ,mst.ENTRY_BY_ID ");
            sb.Append(" ,us.FULLNAME   ENTRY_BY ");
            sb.Append(" ,mst.ENTRY_DATE ");
            sb.Append(" ,mst.EDIT_BY_ID ");
            sb.Append(" ,mst.EDIT_DATE ");
            sb.Append(" ,mst.DEPT_ID ");
            sb.Append(" ,mst.FORECUST_ID ");
            sb.Append(" ,mst.REJECTED_QTY ");
            sb.Append(" ,mst.IS_UNLOAD");
            sb.Append(" ,mst.REF_NO_MANUAL ");
            sb.Append(" ,TO_CHAR( mst.BATCH_STARTTIME , 'DD-MON-YYYY') BATCH_STARTTIME ");
            sb.Append(" ,TO_CHAR( mst.BATCH_ENDTIME, 'DD-MON-YYYY')  BATCH_ENDTIME ");
            sb.Append(" ,mst.AUTH_STATUS ");
            sb.Append(" ,mst.AUTH_BY_ID ");
            sb.Append(" ,mst.AUTH_DATE ");
            sb.Append(" ,mst.STARTTIME ");
            sb.Append(" ,mst.ENDTIME ");
            sb.Append(" ,mst.BATCH_ID ");
            sb.Append(" ,unLoadMst.PROD_NO as UN_LOAD_PROD_NO,unLoadMst.PROD_ID as UN_LOADED_PROD_ID ");
            sb.Append(" ,unLoadMst.IS_UNLOAD as IS_UNLOADED ");
            sb.Append(" ,unLoadMst.AUTH_STATUS as UN_LOAD_AUTH_STATUS  ");
            sb.Append(" ,TO_CHAR( mst.PRODUCTION_DATE, 'DD-MON-YYYY')  PRODUCTION_DATE ");
            sb.Append(" ,mst.PROCESS_CODE ");
            sb.Append(" ,svr.FULL_NAME,dept.DEPARTMENT_NAME,shift.SHIFT_NAME, Fmst.FOR_MONTH FORECUSTMONTH ,Fmst.FOR_YEAR  FORECUSTYEAR, ");
            sb.Append("  mst.PROD_BATCH_NO ");
            //sb.Append(" CONCAT( mst.PROD_BATCH_NO ,CONCAT('-' ,TO_CHAR(NVL( circuitObj.FORMATION_BATCH_SL,0)))) PROD_BATCH_NO");
            sb.Append("  ,mst.SHIFT_INCHARGE ");
            sb.Append(" ,incharge.FULL_NAME SHIFT_INCHARGE_NAME ");
            sb.Append(" ,mm.MACHINE_NAME ");            
            sb.Append(" FROM PRODUCTION_MST mst  ");
            sb.Append(" LEFT JOIN PRODUCTION_MST unLoadMst on mst.PROD_ID=unLoadMst.REF_PROD_ID ");
            sb.Append(" INNER JOIN SUPPERVISOR_MST  svr ON mst.SUPERVISOR_ID = svr.EMP_ID   ");
            sb.Append(" INNER JOIN DEPARTMENT_INFO dept ON mst.DEPT_ID=dept.DEPARTMENT_ID  ");
            sb.Append(" INNER JOIN SHIFT_MST shift ON mst.SHIFT_ID=shift.SHIFT_ID  ");
            sb.Append(" LEFT JOIN  PROD_TBLFORECAST_MST Fmst ON mst.FORECUST_ID=Fmst.FC_ID ");
            sb.Append(" INNER JOIN TBLUSER us ON mst.ENTRY_BY_ID=us.USERID ");
            sb.Append(" LEFT JOIN SUPPERVISOR_MST  incharge  ON mst.SHIFT_INCHARGE = incharge.EMP_ID ");
            sb.Append(" LEFT JOIN PROD_FORMATION_CIRCUIT_INFO  circuitObj on mst.PROD_ID=circuitObj.PROD_ID ");
            sb.Append(" LEFT JOIN MACHINE_MST mm on circuitObj.MACHINE_ID=mm.MACHINE_ID ");
            sb.Append(" WHERE 1=1  ");
            return sb.ToString();
        }

        public static string GetProductionDtlListString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT   ");
            sb.Append(" pmst.PROD_ID  ");
            sb.Append(" ,pmst.PROD_NO   ");
            sb.Append(" ,supper.FULL_NAME SUPERVISOR_NAME  ");
            sb.Append(" ,pmst.SUPERVISOR_ID  ");
            sb.Append(" ,pmst.DEPT_ID  ");
            sb.Append(" ,dept.DEPARTMENT_NAME  ");
            sb.Append(" ,pmst.REF_NO_MANUAL  ");
            sb.Append(" ,fmst.FOR_MONTH FORECUSTMONTH  ");
            sb.Append(" ,fmst.FOR_YEAR FORECUSTYEAR  ");
            sb.Append(" ,pmst.FORECUST_ID  ");
            sb.Append(" ,pmst.SHIFT_ID  ");
            sb.Append(" ,shift.SHIFT_NAME  ");
            sb.Append(" ,pmst.BATCH_STARTTIME  ");
            sb.Append(" ,pmst.BATCH_ENDTIME  ");
            sb.Append(" ,pmst.STARTTIME  ");
            sb.Append(" ,pmst.ENDTIME  ");
            sb.Append(" ,pmst.PROCESS_CODE  ");
            sb.Append(" ,pmst.PRODUCTION_DATE  ");
            sb.Append(" ,pmst.REJECTED_QTY  ");
            sb.Append(" ,pmst.BATCH_ID  ");
            sb.Append(" ,pmst.PROD_BATCH_NO  ");
            sb.Append(" ,dtl.PROD_MST_ID  ");
            sb.Append(" ,dtl.PROD_DTL_ID  ");
            sb.Append(" ,dtl.SLNO  ");
            sb.Append(" ,inv.ITEM_NAME  ");
            sb.Append(" ,inv.ITEM_ID  ");
            sb.Append(" ,dtl.PANEL_PC  ");
            sb.Append(" ,panel.UOM_NAME PANEL_UOM_NAME  ");
            sb.Append(" ,dtl.ITEM_PANEL_QTY  ");
            sb.Append(" ,dtl.PANEL_UOM_ID  ");
            sb.Append(" ,dtl.ITEM_QTY  ");
            sb.Append(" ,itmUOM.UOM_CODE_SHORT UOM_NAME  ");
            sb.Append(" ,dtl.UOM_ID  ");
            sb.Append(" ,dtl.ITEM_WEIGHT  ");
            sb.Append(" ,weightUOM.UOM_CODE_SHORT WEIGHT_UOM_NAME  ");
            sb.Append(" ,dtl.WEIGHT_UOM_ID  ");
            sb.Append(" ,dtl.MACHINE_ID  ");
            sb.Append(" ,mac.MACHINE_NAME  ");
            sb.Append(" ,dtl.BOM_ID  ");
            sb.Append(" ,bom.BOM_NO  ");
            sb.Append(" ,barU.UOM_NAME BAR_NAME  ");
            sb.Append(" ,bom.BOM_ITEM_DESC BOM_NAME  ");
            sb.Append(" ,dtl.REMARKS  ");
            sb.Append(" ,dtl.OPERATOR_ID  ");
            sb.Append(" ,OpMst.FULL_NAME OPERATOR_NAME  ");
            sb.Append(" ,dtl.USED_BAR_PC  ");
            sb.Append(" ,dtl.BAR_TYPE  ");
            sb.Append(" ,dtl.USED_QTY_KG  ");
            sb.Append(" ,dtl.BAR_WEIGHT  ");
            sb.Append(" ,bar.UOM_CODE_SHORT BAR_TYPE_NAME  ");
            sb.Append(" ,dtl.GRID_BATCH  ");
            sb.Append(" ,dtl.FORMATION_STARTTIME  ");
            sb.Append(" ,dtl.FORMATION_OFFTIME  ");
            sb.Append(" ,dtl.FORMATION_OFFDATE  ");
            sb.Append(" ,dtl.FORMED_QTY  ");
            sb.Append(" ,dtl.UNFORMED_QTY  ");
            sb.Append(" ,dtl.REJECT_QTY  ");
            sb.Append(" ,dtl.AMPERE  ");
            sb.Append(" ,dtl.CYCLETIME  ");
            sb.Append(" ,dtl.SULFURIC_GRAVITY  ");
            sb.Append(" ,dtl.TEMPARATURE  ");
            // sb.Append(" ,dtl.GRID_BATCH  ");
            sb.Append(" ,dtl.PASTING_BATCH  ");
            sb.Append(" ,us.FULLNAME CREATE_BY ");
            sb.Append(" ,pmst.ENTRY_DATE ");
            sb.Append(" ,pmst.ISSULPHATION ");
            sb.Append(" ,dtl.FILLING_BATCH ");
            sb.Append(" ,dtl.SULPHATION_STARTTIME ");
            sb.Append(" ,dtl.SULPHATION_OFFDATE ");
            sb.Append(" ,dtl.SULPHATION_OFFTIME ");
            sb.Append(" FROM   ");
            sb.Append(" PRODUCTION_MST pmst  ");
            sb.Append(" INNER JOIN  PRODUCTION_DTL dtl ON pmst.PROD_ID=dtl.PROD_MST_ID  ");
            sb.Append(" INNER JOIN INV_ITEM_MASTER inv ON dtl.ITEM_ID=inv.ITEM_ID  ");
            sb.Append(" INNER JOIN UOM_INFO itmUOM  ON dtl.UOM_ID=itmUOM.UOM_ID  ");

            sb.Append(" INNER JOIN SUPPERVISOR_MST supper ON supper.EMP_ID=pmst.SUPERVISOR_ID  ");
            sb.Append(" INNER JOIN DEPARTMENT_INFO dept ON pmst.DEPT_ID=dept.DEPARTMENT_ID  ");

            sb.Append(" INNER JOIN SHIFT_MST shift ON pmst.SHIFT_ID=shift.SHIFT_ID  ");
            sb.Append(" LEFT JOIN MACHINE_MST mac ON dtl.MACHINE_ID=mac.MACHINE_ID  ");
            sb.Append(" LEFT JOIN PROD_RM_FORECAST_MST fmst ON pmst.FORECUST_ID= fmst.RM_FC_ID  ");
            sb.Append(" LEFT JOIN UOM_INFO barU ON dtl.BAR_TYPE=barU.UOM_ID  ");
            sb.Append(" LEFT JOIN UOM_INFO panel on dtl.PANEL_UOM_ID=panel.UOM_ID  ");
            sb.Append(" LEFT JOIN SUPPERVISOR_MST OpMst On dtl.OPERATOR_ID=OpMst.EMP_ID  ");
            sb.Append(" LEFT JOIN BOM_MST_T bom ON dtl.BOM_ID=bom.BOM_ID  ");
            sb.Append(" LEFT JOIN UOM_INFO weightUOM ON dtl.WEIGHT_UOM_ID=weightUOM.UOM_ID  ");
            sb.Append(" LEFT JOIN UOM_INFO bar ON dtl.BAR_TYPE=bar.UOM_ID  ");
            sb.Append(" LEFT JOIN TBLUSER us  ON us.USERID=pmst.ENTRY_BY_ID  ");
            sb.Append(" WHERE 1=1  ");
            return sb.ToString();
        }

        public static string GetBatteryTypeListString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" Select mst.*  ");
            sb.Append(" FROM BATTERY_TYPE_MST mst ");
            sb.Append(" Where 1=1 ");

            return sb.ToString();
        }

        public static dcPRODUCTION_MST GetProductionByProdID(int pProd_ID)
        {
            return GetProductionByProdID(pProd_ID.ToString(), null);
        }

        public static string NEW_PROD_NO(string pdate, DBContext dc)
        {
            bool isDCInit = false;
            string _PROD_NO = string.Empty;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                string abbr = " SELECT FN_NEW_GRID_PROD_NO(@pdate) A from Dual ";
                cmdInfo.DBParametersInfo.Add("@pdate", pdate);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                _PROD_NO = Convert.ToString(DBQuery.ExecuteDBScalar(dbq, dc));
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return _PROD_NO;
        }



        public static string NEW_SMALL_PARTS_PROD_NO(int pdept, string pdate, DBContext dc)
        {
            bool isDCInit = false;
            string _PROD_NO = string.Empty;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                string abbr = "";
                abbr = " SELECT FN_NEW_SMALL_PARTS_PROD_NO(@pdate) A from Dual ";
                cmdInfo.DBParametersInfo.Add("@pdate", pdate);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                _PROD_NO = Convert.ToString(DBQuery.ExecuteDBScalar(dbq, dc));
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return _PROD_NO;
        }

        public static string NEW_SULPHATION_PROD_NO(int pdept, string pdate, DBContext dc)
        {
            bool isDCInit = false;
            string _PROD_NO = string.Empty;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();

                string abbr = "";
                abbr = " SELECT FN_NEW_SULPHATION_PROD_NO(@pdate) A from Dual ";
                cmdInfo.DBParametersInfo.Add("@pdate", pdate);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                _PROD_NO = Convert.ToString(DBQuery.ExecuteDBScalar(dbq, dc));
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return _PROD_NO;
        }

        public static string NEW_PROD_NO(int pdept, string pdate, DBContext dc)
        {
            bool isDCInit = false;
            string _PROD_NO = string.Empty;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();

                string abbr = "";

                if (pdept == 135)  // Casting
                    abbr = " SELECT FN_NEW_GRID_PROD_NO(@pdate) A from Dual ";

                if (pdept == 49)  // Plastic 
                    abbr = " SELECT FN_NEW_PPL_PROD_NO(@pdate) A from Dual ";

                if (pdept == 7)  //Pasting Section
                    abbr = " SELECT FN_NEW_PASTING_PROD_NO(@pdate) A from Dual ";

                if (pdept == 11)   // Formation
                    abbr = " SELECT FN_NEW_FORMATION_PROD_NO(@pdate) A from Dual ";

                if (pdept == 136) // Assembly
                    abbr = " SELECT FN_NEW_ASSEMBLY_PROD_NO(@pdate) A from Dual ";

                if (pdept == 2)  // GRAY OXIDE
                    abbr = " SELECT FN_NEW_OXIDE_PROD_NO(@pdate) A from Dual ";
                if (pdept == 137)  //Red OXIDE
                    abbr = " SELECT FN_NEW_REDOXIDE_PROD_NO(@pdate) A from Dual ";

                if (pdept == 18) // IB
                    abbr = " SELECT FN_NEW_IB_PROD_NO(@pdate) A from Dual ";
                if (pdept == 54) // Solar
                    abbr = " SELECT FN_NEW_CHERGING_PROD_NO(@pdate) A from Dual ";
                if (pdept == 113) // Solar
                    abbr = " SELECT FN_NEW_MC_CHARGE_PROD_NO(@pdate) A from Dual ";
                if (pdept == 140) // VRLA
                    abbr = " SELECT FN_NEW_VRLA_PROD_NO(@pdate) A from Dual ";
                if (pdept == 123) // DMW
                    abbr = " SELECT FN_NEW_DMW_PROD_NO(@pdate) A from Dual ";
                if(pdept==5)  // Pure Lead
                    abbr = "SELECT FN_NEW_PURELEAD_PROD_NO(@pdate) A FROM Dual ";
                if (pdept == 24)  // Pure Lead
                    abbr = "SELECT FN_NEW_MRB_PROD_NO(@pdate) A FROM Dual ";
                if (pdept == 142)  // VRLA CHarging
                    abbr = "SELECT FN_NEW_VRLA_CHARGE_NO(@pdate) A FROM Dual ";
                cmdInfo.DBParametersInfo.Add("@pdate", pdate);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                _PROD_NO = Convert.ToString(DBQuery.ExecuteDBScalar(dbq, dc));
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return _PROD_NO;
        }

        public static string GET_NEW_PROD_NO(int pdept, string pdate,int pSTLM_ID, DBContext dc)
        {
            bool isDCInit = false;
            string _PROD_NO = string.Empty;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();

                string abbr = "";
              
                abbr = " SELECT FN_NEW_PRODUCTION_NO(@pdate,@pdept,@pSTLM_ID) FROM DUAL ";
                cmdInfo.DBParametersInfo.Add("@pdate", pdate);
                cmdInfo.DBParametersInfo.Add("@pdept", pdept);
                cmdInfo.DBParametersInfo.Add("@pSTLM_ID", pSTLM_ID);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                _PROD_NO = Convert.ToString(DBQuery.ExecuteDBScalar(dbq, dc));
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return _PROD_NO;
        }

        


        public static string NEW_ELECTROLYTE_PROD_NO(int pdept, string pdate, DBContext dc)
        {
            bool isDCInit = false;
            string _PROD_NO = string.Empty;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();

                string abbr = "";
                if (pdept == 54) // Solar
                    abbr = " SELECT FN_NEW_ELECTROLYTE_PROD_NO(@pdate) A from Dual ";
                    cmdInfo.DBParametersInfo.Add("@pdate", pdate);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                _PROD_NO = Convert.ToString(DBQuery.ExecuteDBScalar(dbq, dc));
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return _PROD_NO;
        }
        public static string UpdateAuthorized(int pPROD_ID, int pAUTH_BY_ID, DBContext dc)
        {
            bool isDCInit = false;
            string _PROD_NO = string.Empty;
            try
            {

                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();

                string abbr = " UPDATE PRODUCTION_MST SET AUTH_STATUS='Y',AUTH_BY_ID=@AUTH_BY_ID,AUTH_DATE= SYSDATE WHERE PROD_ID=@PROD_ID ";
                cmdInfo.DBParametersInfo.Add("@AUTH_BY_ID", pAUTH_BY_ID);
                cmdInfo.DBParametersInfo.Add("@PROD_ID", pPROD_ID);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBNonQuery(dbq, dc);
                _PROD_NO = pPROD_ID.ToString();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return _PROD_NO;
        }

        public static string NEW_GRIDCast_BATCH_NO(string pdate, string pShiftID, DBContext dc)
        {
            bool isDCInit = false;
            string _BATCH_NO = string.Empty;
            try
            {

                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                string abbr = " SELECT FN_NEW_GRIDCASTING_BATCH_NO(@pdate,@pShiftID) A from Dual ";
                cmdInfo.DBParametersInfo.Add("@pdate", pdate);
                cmdInfo.DBParametersInfo.Add("@pShiftID", pShiftID);
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                _BATCH_NO = Convert.ToString(DBQuery.ExecuteDBScalar(dbq, dc));
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return _BATCH_NO;
        }

        public static string New_Production_Batch_Id(string pdate, string pShiftID,int pSTLMId, DBContext dc)
        {
            bool isDCInit = false;
            string _BATCH_NO = string.Empty;
            try
            {

                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                string abbr = " SELECT FN_NEW_PROD_BATCH_ID(@pdate,@pShiftID,@pSTLMId) A from Dual ";
                cmdInfo.DBParametersInfo.Add("@pdate", pdate);
                cmdInfo.DBParametersInfo.Add("@pShiftID", pShiftID);
                cmdInfo.DBParametersInfo.Add("@pSTLMId", pSTLMId);
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                _BATCH_NO = Convert.ToString(DBQuery.ExecuteDBScalar(dbq, dc));
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return _BATCH_NO;
        }

        public static string NEW_GRIDCast_BATCH_NO_NeWNo(string pdate, string pShiftID, DBContext dc)
        {
            bool isDCInit = false;
            string _BATCH_NO = string.Empty;
            try
            {

                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                string abbr = " SELECT FN_NEW_CASTING_BATCH_NO(@pdate,@pShiftID) A from Dual ";
                cmdInfo.DBParametersInfo.Add("@pdate", pdate);
                cmdInfo.DBParametersInfo.Add("@pShiftID", pShiftID);
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                _BATCH_NO = Convert.ToString(DBQuery.ExecuteDBScalar(dbq, dc));
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return _BATCH_NO;
        }

        //public static string NEW_Production_BatchNo(string pdate, string pShiftID, DBContext dc)
        //{
        //    bool isDCInit = false;
        //    string _BATCH_NO = string.Empty;
        //    try
        //    {

        //        isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
        //        DBCommandInfo cmdInfo = new DBCommandInfo();
        //        string abbr = " SELECT FN_NEW_CASTING_BATCH_NO(@pdate,@pShiftID) A from Dual ";
        //        cmdInfo.DBParametersInfo.Add("@pdate", pdate);
        //        cmdInfo.DBParametersInfo.Add("@pShiftID", pShiftID);
        //        DBQuery dbq = new DBQuery();
        //        dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
        //        cmdInfo.CommandText = abbr;
        //        cmdInfo.CommandType = CommandType.Text;
        //        dbq.DBCommandInfo = cmdInfo;
        //        _BATCH_NO = Convert.ToString(DBQuery.ExecuteDBScalar(dbq, dc));
        //    }
        //    catch { throw; }
        //    finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
        //    return _BATCH_NO;
        //}

        public static string NEW_GRIDCast_BATCH_NO_MachineWise(string pdate, string pShiftID,int pMACHINE_ID, DBContext dc)
        {
            bool isDCInit = false;
            string _BATCH_NO = string.Empty;
            try
            {

                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                string abbr = " SELECT FN_NEW_CASTING_BATCH_NO_M_W(@pdate,@pShiftID,@pMACHINE_ID) A from Dual ";
                cmdInfo.DBParametersInfo.Add("@pdate", pdate);
                cmdInfo.DBParametersInfo.Add("@pShiftID", pShiftID);
                cmdInfo.DBParametersInfo.Add("@pMACHINE_ID", pMACHINE_ID);
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                _BATCH_NO = Convert.ToString(DBQuery.ExecuteDBScalar(dbq, dc));
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return _BATCH_NO;
        }

        public static string NEW_IB_BATCH_NO(string pdate, string pShiftID, DBContext dc)
        {
            bool isDCInit = false;
            string _BATCH_NO = string.Empty;
            try
            {

                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                string abbr = " SELECT FN_NEW_IB_BATCH_NO(@pdate,@pShiftID) A from Dual ";
                cmdInfo.DBParametersInfo.Add("@pdate", pdate);
                cmdInfo.DBParametersInfo.Add("@pShiftID", pShiftID);
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                _BATCH_NO = Convert.ToString(DBQuery.ExecuteDBScalar(dbq, dc));
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return _BATCH_NO;
        }


        public static string NEW_IB_SULPHATION_BATCH_NO(string pdate, string pShiftID, DBContext dc)
        {
            bool isDCInit = false;
            string _BATCH_NO = string.Empty;
            try
            {

                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                string abbr = " SELECT FN_NEW_SULPHATION_BATCH_NO(@pdate,@pShiftID) A from Dual ";
                cmdInfo.DBParametersInfo.Add("@pdate", pdate);
                cmdInfo.DBParametersInfo.Add("@pShiftID", pShiftID);
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                _BATCH_NO = Convert.ToString(DBQuery.ExecuteDBScalar(dbq, dc));
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return _BATCH_NO;
        }
        public static string NEW_OXIDE_BATCH_NO(string pdate, string pShiftID, DBContext dc)
        {
            bool isDCInit = false;
            string _BATCH_NO = string.Empty;
            try
            {

                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                string abbr = " SELECT FN_NEW_OXIDE_BATCH_NO(@pdate,@pShiftID) A from Dual ";
                cmdInfo.DBParametersInfo.Add("@pdate", pdate);
                cmdInfo.DBParametersInfo.Add("@pShiftID", pShiftID);
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                _BATCH_NO = Convert.ToString(DBQuery.ExecuteDBScalar(dbq, dc));
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return _BATCH_NO;
        }


        public static string NEW_SMALL_PARTS_BATCH_NO(string pdate, string pShiftID, DBContext dc)
        {
            bool isDCInit = false;
            string _BATCH_NO = string.Empty;
            try
            {

                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                string abbr = " SELECT FN_NEW_SMALL_PARTS_BATCH_NO(@pdate,@pShiftID) A from Dual ";
                cmdInfo.DBParametersInfo.Add("@pdate", pdate);
                cmdInfo.DBParametersInfo.Add("@pShiftID", pShiftID);
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                _BATCH_NO = Convert.ToString(DBQuery.ExecuteDBScalar(dbq, dc));
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return _BATCH_NO;
        }

        public static string NEW_Pasting_BATCH_NO(string pdate, string pShiftID, DBContext dc)
        {
            bool isDCInit = false;
            string _BATCH_NO = string.Empty;
            try
            {

                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                string abbr = " SELECT FN_NEW_PASTING_BATCH_NO(@pdate,@pShiftID) A from Dual ";
                cmdInfo.DBParametersInfo.Add("@pdate", pdate);
                cmdInfo.DBParametersInfo.Add("@pShiftID", pShiftID);
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                _BATCH_NO = Convert.ToString(DBQuery.ExecuteDBScalar(dbq, dc));
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return _BATCH_NO;
        }

        public static string NEW_Formation_BATCH_NO(string pdate, string pShiftID, DBContext dc)
        {
            bool isDCInit = false;
            string _BATCH_NO = string.Empty;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                string abbr = " SELECT FN_NEW_FORMATION_BATCH_NO(@pdate,@pShiftID) A from Dual ";
                cmdInfo.DBParametersInfo.Add("@pdate", pdate);
                cmdInfo.DBParametersInfo.Add("@pShiftID", pShiftID);
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                _BATCH_NO = Convert.ToString(DBQuery.ExecuteDBScalar(dbq, dc));
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return _BATCH_NO;
        }


        public static string Delete_Production_By_Prod_No(string prodno, DBContext dc)
        {
            bool isDCInit = false;
            string _BATCH_NO = string.Empty;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                string abbr = " Delete from ITEM_STOCK_DETAILS where TRANS_REF_NO=@p_prod_no ";
                cmdInfo.DBParametersInfo.Add("@p_prod_no", prodno);
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                int i = DBQuery.ExecuteDBNonQuery(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return _BATCH_NO;
        }


        public static string NEW_Plastic_BATCH_NO(string pdate, string pShiftID, DBContext dc)
        {
            bool isDCInit = false;
            string _BATCH_NO = string.Empty;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                string abbr = " SELECT FN_NEW_PLASTIC_BATCH_NO(@pdate,@pShiftID) A from Dual ";
                cmdInfo.DBParametersInfo.Add("@pdate", pdate);
                cmdInfo.DBParametersInfo.Add("@pShiftID", pShiftID);
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                _BATCH_NO = Convert.ToString(DBQuery.ExecuteDBScalar(dbq, dc));
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return _BATCH_NO;
        }

        public static string NEW_Assembly_BATCH_NO(string pdate, string pShiftID, DBContext dc)
        {
            bool isDCInit = false;
            string _BATCH_NO = string.Empty;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                string abbr = " SELECT FN_NEW_ASSEMBLY_BATCH_NO(@pdate,@pShiftID) A from Dual ";
                cmdInfo.DBParametersInfo.Add("@pdate", pdate);
                cmdInfo.DBParametersInfo.Add("@pShiftID", pShiftID);
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                _BATCH_NO = Convert.ToString(DBQuery.ExecuteDBScalar(dbq, dc));
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return _BATCH_NO;
        }
        public static List<dcPRODUCTION_MST> GetProductionByDate(string pProductionDate, DBContext dc)
        {
            List<dcPRODUCTION_MST> cObjList = new List<dcPRODUCTION_MST>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetProductionMstListString());
                if (pProductionDate.ToString() != "")
                {
                    sb.Append("  AND TO_DATE(mst.PRODUCTION_DATE) = TO_DATE(@PRODUCTION_DATE)  ");
                    cmdInfo.DBParametersInfo.Add("@PRODUCTION_DATE", pProductionDate);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                //dbq.OrderBy = "mst.PROD_ID Desc";

                cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_MST>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcPRODUCTION_MST> GetProductionNoList(DBContext dc)
        {
            List<dcPRODUCTION_MST> cObjList = new List<dcPRODUCTION_MST>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetProductionMstList_Basic_SqlString());
               
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                //dbq.OrderBy = "mst.PROD_ID Desc";

                cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_MST>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }


        public static List<dcPRODUCTION_MST> GetProductionByDate(string pProductionDate, int deptId, DBContext dc)
        {
            List<dcPRODUCTION_MST> cObjList = new List<dcPRODUCTION_MST>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetProductionMstListString());
                if (pProductionDate.ToString() != "")
                {
                    sb.Append("  AND TO_DATE(mst.PRODUCTION_DATE) = TO_DATE(@PRODUCTION_DATE)  ");
                    cmdInfo.DBParametersInfo.Add("@PRODUCTION_DATE", pProductionDate);
                }
                if (deptId > 0)
                {
                    sb.Append(" AND mst.DEPT_ID = @DEPT_ID ");
                    cmdInfo.DBParametersInfo.Add("@DEPT_ID", deptId);
                }
                sb.Append("  order by mst.PRODUCTION_DATE desc ");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                //dbq.OrderBy = "mst.PROD_ID Desc";

                cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_MST>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }


        public static List<dcPRODUCTION_MST> GetListProductionDetails(clsPrmInventory pObj, DBContext dc)
        {
            List<dcPRODUCTION_MST> cObjList = new List<dcPRODUCTION_MST>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetProductionMstListString());
                if (pObj.fromProdDate.ToString() != "")
                {
                    sb.Append("  AND TO_DATE(mst.PRODUCTION_DATE) >= TO_DATE(@PRODUCTION_DATE)  ");
                    cmdInfo.DBParametersInfo.Add("@PRODUCTION_DATE", pObj.fromProdDate);
                }
                if (pObj.toProdDate.ToString() != "")
                {
                    sb.Append("  AND TO_DATE(mst.PRODUCTION_DATE) <= TO_DATE(@TO_DATE)  ");
                    cmdInfo.DBParametersInfo.Add("@TO_DATE", pObj.toProdDate);
                }
                if (pObj.autho_status.ToString() != "")
                {
                    sb.Append(" AND mst.AUTH_STATUS = @AUTH_STATUS ");
                    cmdInfo.DBParametersInfo.Add("@AUTH_STATUS", pObj.autho_status);
                }
                if (pObj.DeptID > 0)
                {
                    sb.Append(" AND mst.DEPT_ID = @DEPT_ID ");
                    cmdInfo.DBParametersInfo.Add("@DEPT_ID", pObj.DeptID);
                }

                if (pObj.FormStatus != "" && pObj.FormStatus!=null)
                {
                    sb.Append(" AND mst.FORMATION_STATUS = @FormStatus ");
                    cmdInfo.DBParametersInfo.Add("@FormStatus", pObj.FormStatus);
                }

                if (pObj.StorageLocationId > 0)
                {
                    sb.Append(" AND mst.STLM_ID = @STLM_ID ");
                    cmdInfo.DBParametersInfo.Add("@STLM_ID", pObj.StorageLocationId);
                }

                if (pObj.isElectrolyte == "Y")
                {
                    sb.Append(" AND mst.IS_ELECTROLYTE = @isElectrolyte ");
                    cmdInfo.DBParametersInfo.Add("@isElectrolyte", pObj.isElectrolyte);
                }

                else
                {
                    sb.Append(" AND mst.IS_ELECTROLYTE = 'N' ");
                }

                sb.Append("  order by mst.PRODUCTION_DATE desc,SHIFT.SHIFT_MSTID asc ");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_MST>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static List<dcPRODUCTION_MST> GetProductionAssemb_PackingByDate(string pProductionDate, string pTodate, int deptId, string ispacking, string putho_status,string ispbattery,int StlmId, DBContext dc)
        {
            List<dcPRODUCTION_MST> cObjList = new List<dcPRODUCTION_MST>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetProductionMstListString());
                if (pProductionDate.ToString() != "")
                {
                    sb.Append("  AND TO_DATE(mst.PRODUCTION_DATE) >= TO_DATE(@PRODUCTION_DATE)  ");
                    cmdInfo.DBParametersInfo.Add("@PRODUCTION_DATE", pProductionDate);
                }

                if (pTodate.ToString() != "")
                {
                    sb.Append("  AND TO_DATE(mst.PRODUCTION_DATE) <= TO_DATE(@TOPRODUCTION_DATE)  ");
                    cmdInfo.DBParametersInfo.Add("@TOPRODUCTION_DATE", pTodate);
                }

                if (putho_status.ToString() != "")
                {
                    sb.Append(" AND mst.AUTH_STATUS = @AUTH_STATUS ");
                    cmdInfo.DBParametersInfo.Add("@AUTH_STATUS", putho_status);
                }

                if (deptId > 0)
                {
                    sb.Append(" AND mst.DEPT_ID = @DEPT_ID ");
                    cmdInfo.DBParametersInfo.Add("@DEPT_ID", deptId);
                }

                if (StlmId > 0)
                {
                    sb.Append(" AND mst.STLM_ID = @StlmId ");
                    cmdInfo.DBParametersInfo.Add("@StlmId", StlmId);
                }

                if (ispacking != "")
                {
                    sb.Append(" AND mst.IS_PACKING = @pIS_PACKING ");
                    cmdInfo.DBParametersInfo.Add("@pIS_PACKING", ispacking);
                }
                if (ispbattery != "")
                {
                    sb.Append(" AND mst.IS_P_BATTERY = @ispbattery ");
                    cmdInfo.DBParametersInfo.Add("@ispbattery", ispbattery);
                }
                sb.Append("  order by mst.PRODUCTION_DATE desc ");
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                //dbq.OrderBy = "mst.PROD_ID Desc";

                cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_MST>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static List<dcPRODUCTION_MST> GetFormarionProductionLoadingList(string pProductionDate, int deptId, DBContext dc)
        {
            List<dcPRODUCTION_MST> cObjList = new List<dcPRODUCTION_MST>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetProductionFormationLoadingList_SqlString());
                if (pProductionDate.ToString() != "")
                {
                    sb.Append("  AND TO_DATE(mst.PRODUCTION_DATE) = TO_DATE(@PRODUCTION_DATE)  ");
                    cmdInfo.DBParametersInfo.Add("@PRODUCTION_DATE", pProductionDate);
                }
                if (deptId > 0)
                {
                    sb.Append(" AND mst.DEPT_ID = @DEPT_ID ");
                    cmdInfo.DBParametersInfo.Add("@DEPT_ID", deptId);
                }
                sb.Append(" order by mst.PRODUCTION_DATE desc,mst.PROD_ID asc ");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_MST>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcPRODUCTION_MST> GetSolarProductionLoadingList(string pProductionDate, int deptId, DBContext dc)
        {
            List<dcPRODUCTION_MST> cObjList = new List<dcPRODUCTION_MST>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetProductionSolarChargeLoadingList_SqlString());
                if (pProductionDate.ToString() != "")
                {
                    sb.Append("  AND TO_DATE(mst.PRODUCTION_DATE) = TO_DATE(@PRODUCTION_DATE)  ");
                    cmdInfo.DBParametersInfo.Add("@PRODUCTION_DATE", pProductionDate);
                }
                if (deptId > 0)
                {
                    sb.Append(" AND mst.DEPT_ID = @DEPT_ID ");
                    cmdInfo.DBParametersInfo.Add("@DEPT_ID", deptId);
                }
                sb.Append(" order by mst.PRODUCTION_DATE desc,mst.PROD_ID asc ");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_MST>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static List<dcPRODUCTION_MST> GetSulphationProductionByDate(string pProductionDate, int deptId, string issulpahtion, DBContext dc)
        {
            List<dcPRODUCTION_MST> cObjList = new List<dcPRODUCTION_MST>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetProductionMstListString());
                if (pProductionDate.ToString() != "")
                {
                    sb.Append("  AND TO_DATE(mst.PRODUCTION_DATE) = TO_DATE(@PRODUCTION_DATE)  ");
                    cmdInfo.DBParametersInfo.Add("@PRODUCTION_DATE", pProductionDate);
                }
                if (deptId > 0)
                {
                    sb.Append(" AND mst.DEPT_ID = @DEPT_ID ");
                    cmdInfo.DBParametersInfo.Add("@DEPT_ID", deptId);
                }
                sb.Append("  order by mst.PRODUCTION_DATE desc ");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                //dbq.OrderBy = "mst.PROD_ID Desc";

                cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_MST>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcPRODUCTION_MST> GetSmallPartsProductionByDate(string pProductionDate, int deptId, string issmallparts, DBContext dc)
        {
            List<dcPRODUCTION_MST> cObjList = new List<dcPRODUCTION_MST>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetProductionMstListString());
                if (pProductionDate.ToString() != "")
                {
                    sb.Append("  AND TO_DATE(mst.PRODUCTION_DATE) = TO_DATE(@PRODUCTION_DATE)  ");
                    cmdInfo.DBParametersInfo.Add("@PRODUCTION_DATE", pProductionDate);
                }
                if (deptId > 0)
                {
                    sb.Append(" AND mst.DEPT_ID = @DEPT_ID ");
                    cmdInfo.DBParametersInfo.Add("@DEPT_ID", deptId);
                }

                if (issmallparts != "")
                {
                    sb.Append(" AND mst.ISSMALLPARTS = @ISSMALLPARTS ");
                    cmdInfo.DBParametersInfo.Add("@ISSMALLPARTS", issmallparts);
                }
                else
                {
                    sb.Append(" AND mst.ISSMALLPARTS IS NULL ");
                }

                sb.Append(" order by  mst.PRODUCTION_DATE desc ");


                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                //dbq.OrderBy = "mst.PROD_ID Desc";

                cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_MST>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static dcPRODUCTION_MST GetProductionByProdID(string pProd_ID, DBContext dc)
        {
            dcPRODUCTION_MST cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetProductionMstListString());
                if (pProd_ID != "")
                {
                    sb.Append(" AND mst.PROD_ID=@PROD_ID ");
                    cmdInfo.DBParametersInfo.Add("@PROD_ID", pProd_ID);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_MST>(dbq, dc).FirstOrDefault();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static dcPRODUCTION_MST GetProductionByProdID_For_Update(string pProd_ID, DBContext dc)
        {
            dcPRODUCTION_MST cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetProductionMstList_Basic_SqlString());
                if (pProd_ID != "")
                {
                    sb.Append(" AND mst.PROD_ID=@PROD_ID ");
                    cmdInfo.DBParametersInfo.Add("@PROD_ID", pProd_ID);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_MST>(dbq, dc).FirstOrDefault();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static dcPRODUCTION_MST GetFormationProductionInfo(clsPrmInventory pObj, DBContext dc)
        {
            dcPRODUCTION_MST cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetProductionMstListString());

                if (pObj.productiondate != null)
                {
                    sb.Append(" AND mst.PRODUCTION_DATE=@PPRODUCTION_DATE ");
                    cmdInfo.DBParametersInfo.Add("@PPRODUCTION_DATE", pObj.productiondate);
                }

                if (pObj.Shift_Id != "")
                {
                    sb.Append(" AND mst.SHIFT_ID=@SHIFT_ID ");
                    cmdInfo.DBParametersInfo.Add("@SHIFT_ID", pObj.Shift_Id);
                }

                if (pObj.DeptID > 0)
                {
                    sb.Append(" AND mst.DEPT_ID=@DEPT_ID ");
                    cmdInfo.DBParametersInfo.Add("@DEPT_ID", pObj.DeptID);
                }

                if (pObj.MachineId > 0)
                {
                    sb.Append(" AND circuitObj.MACHINE_ID=@MACHINE_ID ");
                    cmdInfo.DBParametersInfo.Add("@MACHINE_ID", pObj.MachineId);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_MST>(dbq, dc).FirstOrDefault();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static dcPRODUCTION_MST GetProductionInfoByDateShift(DateTime? pPRODUCTION_DATE, string pSHIFT_ID, int pDEPT_ID, DBContext dc)
        {
            dcPRODUCTION_MST cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetProductionMstListString());
                if (pPRODUCTION_DATE != null)
                {
                    sb.Append(" AND mst.PRODUCTION_DATE=@PPRODUCTION_DATE ");
                    cmdInfo.DBParametersInfo.Add("@PPRODUCTION_DATE", pPRODUCTION_DATE);
                }

                if (pSHIFT_ID != "")
                {
                    sb.Append(" AND mst.SHIFT_ID=@SHIFT_ID ");
                    cmdInfo.DBParametersInfo.Add("@SHIFT_ID", pSHIFT_ID);
                }

                if (pDEPT_ID > 0)
                {
                    sb.Append(" AND mst.DEPT_ID=@DEPT_ID ");
                    cmdInfo.DBParametersInfo.Add("@DEPT_ID", pDEPT_ID);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_MST>(dbq, dc).FirstOrDefault();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }


        public static dcPRODUCTION_MST GetAssembleProductionInfoByDateShift_P_BAT(DateTime? pPRODUCTION_DATE, string pSHIFT_ID, int pDEPT_ID, string ispacking, DBContext dc)
        {
            dcPRODUCTION_MST cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetProductionMstListString());
                if (pPRODUCTION_DATE != null)
                {
                    sb.Append(" AND mst.PRODUCTION_DATE=@PPRODUCTION_DATE ");
                    cmdInfo.DBParametersInfo.Add("@PPRODUCTION_DATE", pPRODUCTION_DATE);
                }

                if (pSHIFT_ID != "")
                {
                    sb.Append(" AND mst.SHIFT_ID=@SHIFT_ID ");
                    cmdInfo.DBParametersInfo.Add("@SHIFT_ID", pSHIFT_ID);
                }

                if (pDEPT_ID > 0)
                {
                    sb.Append(" AND mst.DEPT_ID=@DEPT_ID ");
                    cmdInfo.DBParametersInfo.Add("@DEPT_ID", pDEPT_ID);
                }

                if (ispacking != "")
                {
                    sb.Append(" AND mst.IS_PACKING=@pIS_PACKING ");
                    cmdInfo.DBParametersInfo.Add("@pIS_PACKING", ispacking);
                }

                sb.Append(" AND mst.IS_P_BATTERY='Y' ");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_MST>(dbq, dc).FirstOrDefault();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static dcPRODUCTION_MST GetAssembleProductionInfoByDateShift(DateTime? pPRODUCTION_DATE, string pSHIFT_ID, int pDEPT_ID, string ispacking, DBContext dc)
        {
            dcPRODUCTION_MST cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetProductionMstListString());
                if (pPRODUCTION_DATE != null)
                {
                    sb.Append(" AND mst.PRODUCTION_DATE=@PPRODUCTION_DATE ");
                    cmdInfo.DBParametersInfo.Add("@PPRODUCTION_DATE", pPRODUCTION_DATE);
                }

                if (pSHIFT_ID != "")
                {
                    sb.Append(" AND mst.SHIFT_ID=@SHIFT_ID ");
                    cmdInfo.DBParametersInfo.Add("@SHIFT_ID", pSHIFT_ID);
                }

                if (pDEPT_ID > 0)
                {
                    sb.Append(" AND mst.DEPT_ID=@DEPT_ID ");
                    cmdInfo.DBParametersInfo.Add("@DEPT_ID", pDEPT_ID);
                }

                if (ispacking != "")
                {
                    sb.Append(" AND mst.IS_PACKING=@pIS_PACKING ");
                    cmdInfo.DBParametersInfo.Add("@pIS_PACKING", ispacking);
                }

                sb.Append(" AND mst.IS_P_BATTERY!='Y' ");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_MST>(dbq, dc).FirstOrDefault();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static string Is_New_Formation_Loading_Valid(int machineId, string shiftName, DateTime? prod_date, DBContext dc)
        {
            bool isDCInit = false;
            string _msg = string.Empty;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                string abbr = "";
                abbr = " SELECT FN_IS_NEW_FORM_LOADING_VALID(@P_MACHINE_ID,@P_SHIFT_NO,@P_DATE) A from Dual ";
                cmdInfo.DBParametersInfo.Add("@P_MACHINE_ID", machineId);
                cmdInfo.DBParametersInfo.Add("@P_SHIFT_NO", shiftName);
                cmdInfo.DBParametersInfo.Add("@P_DATE", prod_date.Value);
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                _msg = Convert.ToString(DBQuery.ExecuteDBScalar(dbq, dc));
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return _msg;
        }


        public static dcPRODUCTION_MST GetSolarPackingEntryByDateShift(DateTime? pPRODUCTION_DATE, string pSHIFT_ID, int pDEPT_ID,  DBContext dc)
        {
            dcPRODUCTION_MST cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetProductionMstListString());
               

                if (pPRODUCTION_DATE != null)
                {
                    sb.Append(" AND mst.PRODUCTION_DATE=@PPRODUCTION_DATE ");
                    cmdInfo.DBParametersInfo.Add("@PPRODUCTION_DATE", pPRODUCTION_DATE);
                }

                if (pSHIFT_ID != "")
                {
                    sb.Append(" AND mst.SHIFT_ID=@SHIFT_ID ");
                    cmdInfo.DBParametersInfo.Add("@SHIFT_ID", pSHIFT_ID);
                }

                if (pDEPT_ID > 0)
                {
                    sb.Append(" AND mst.DEPT_ID=@DEPT_ID ");
                    cmdInfo.DBParametersInfo.Add("@DEPT_ID", pDEPT_ID);
                }
                sb.Append(" AND mst.IS_PACKING='Y' ");
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_MST>(dbq, dc).FirstOrDefault();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static dcPRODUCTION_MST GetSolarElectrolyteEntryByDateShift(DateTime? pPRODUCTION_DATE, string pSHIFT_ID, int pDEPT_ID, DBContext dc)
        {
            dcPRODUCTION_MST cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetProductionMstListString());


                if (pPRODUCTION_DATE != null)
                {
                    sb.Append(" AND mst.PRODUCTION_DATE=@PPRODUCTION_DATE ");
                    cmdInfo.DBParametersInfo.Add("@PPRODUCTION_DATE", pPRODUCTION_DATE);
                }

                if (pSHIFT_ID != "")
                {
                    sb.Append(" AND mst.SHIFT_ID=@SHIFT_ID ");
                    cmdInfo.DBParametersInfo.Add("@SHIFT_ID", pSHIFT_ID);
                }

                if (pDEPT_ID > 0)
                {
                    sb.Append(" AND mst.DEPT_ID=@DEPT_ID ");
                    cmdInfo.DBParametersInfo.Add("@DEPT_ID", pDEPT_ID);
                }
                sb.Append(" AND mst.IS_ELECTROLYTE='Y' ");
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_MST>(dbq, dc).FirstOrDefault();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static dcPRODUCTION_MST GetCastSmallProductionInfoByDateShift(DateTime? pPRODUCTION_DATE, string pSHIFT_ID, int pDEPT_ID, string issmallparts,int pStlmId, DBContext dc)
        {
            dcPRODUCTION_MST cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetProductionMstListString());
                if (issmallparts == "Y")
                    sb.Append(" AND mst.ISSMALLPARTS='Y' ");
                else
                    sb.Append("  AND mst.ISSMALLPARTS IS NULL ");

                if (pPRODUCTION_DATE != null)
                {
                    sb.Append(" AND mst.PRODUCTION_DATE=@PPRODUCTION_DATE ");
                    cmdInfo.DBParametersInfo.Add("@PPRODUCTION_DATE", pPRODUCTION_DATE);
                }

                if (pSHIFT_ID != "")
                {
                    sb.Append(" AND mst.SHIFT_ID=@SHIFT_ID ");
                    cmdInfo.DBParametersInfo.Add("@SHIFT_ID", pSHIFT_ID);
                }

                if (pDEPT_ID > 0)
                {
                    sb.Append(" AND mst.DEPT_ID=@DEPT_ID ");
                    cmdInfo.DBParametersInfo.Add("@DEPT_ID", pDEPT_ID);
                }

                if (pStlmId > 0)
                {
                    sb.Append(" AND mst.STLM_ID=@StlmId ");
                    cmdInfo.DBParametersInfo.Add("@StlmId", pStlmId);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_MST>(dbq, dc).FirstOrDefault();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static dcPRODUCTION_MST GetFormationProductionInfoByDateShift(DateTime? pPRODUCTION_DATE, string pSHIFT_ID, int pDEPT_ID, string pFormationStatus, int pStlmId, DBContext dc)
        {
            dcPRODUCTION_MST cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetProductionMstListString());

                if (pPRODUCTION_DATE != null)
                {
                    sb.Append(" AND mst.PRODUCTION_DATE=@PPRODUCTION_DATE ");
                    cmdInfo.DBParametersInfo.Add("@PPRODUCTION_DATE", pPRODUCTION_DATE);
                }

                if (pSHIFT_ID != "")
                {
                    sb.Append(" AND mst.SHIFT_ID=@SHIFT_ID ");
                    cmdInfo.DBParametersInfo.Add("@SHIFT_ID", pSHIFT_ID);
                }

                if (pDEPT_ID > 0)
                {
                    sb.Append(" AND mst.DEPT_ID=@DEPT_ID ");
                    cmdInfo.DBParametersInfo.Add("@DEPT_ID", pDEPT_ID);
                }

                if (pStlmId > 0)
                {
                    sb.Append(" AND mst.STLM_ID=@StlmId ");
                    cmdInfo.DBParametersInfo.Add("@StlmId", pStlmId);
                }

                if (pFormationStatus != "")
                {
                    sb.Append(" AND mst.FORMATION_STATUS=@pFormationStatus ");
                    cmdInfo.DBParametersInfo.Add("@pFormationStatus", pFormationStatus);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_MST>(dbq, dc).FirstOrDefault();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static dcPRODUCTION_MST GetSulphationProductionInfoByDateShift(DateTime? pPRODUCTION_DATE, string pSHIFT_ID, int pDEPT_ID, string issulphation, DBContext dc)
        {
            dcPRODUCTION_MST cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetProductionMstListString());
                if (issulphation == "Y")
                    sb.Append(" AND mst.ISSULPHATION='Y' ");
                else
                    sb.Append("  AND mst.ISSULPHATION IS NULL ");

                if (pPRODUCTION_DATE != null)
                {
                    sb.Append(" AND mst.PRODUCTION_DATE=@PPRODUCTION_DATE ");
                    cmdInfo.DBParametersInfo.Add("@PPRODUCTION_DATE", pPRODUCTION_DATE);
                }

                if (pSHIFT_ID != "")
                {
                    sb.Append(" AND mst.SHIFT_ID=@SHIFT_ID ");
                    cmdInfo.DBParametersInfo.Add("@SHIFT_ID", pSHIFT_ID);
                }

                if (pDEPT_ID > 0)
                {
                    sb.Append(" AND mst.DEPT_ID=@DEPT_ID ");
                    cmdInfo.DBParametersInfo.Add("@DEPT_ID", pDEPT_ID);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_MST>(dbq, dc).FirstOrDefault();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcPRODUCTION_DTL> GetProductionDtlList(clsPrmInventory pObj, DBContext dc)
        {
            List<dcPRODUCTION_DTL> cObjList = new List<dcPRODUCTION_DTL>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetProductionDtlListString());
                if (pObj.productiondate.ToString() != "")
                {
                    sb.Append("  AND TO_DATE(mst.PRODUCTION_DATE) = TO_DATE(@PRODUCTION_DATE)  ");
                    cmdInfo.DBParametersInfo.Add("@PRODUCTION_DATE", pObj.productiondate);
                }
                if (pObj.DeptID > 0)
                {
                    sb.Append(" AND pmst.DEPT_ID = @DEPT_ID ");
                    cmdInfo.DBParametersInfo.Add("@DEPT_ID", pObj.DeptID);
                }

                if (pObj.fromProdDate.ToString() != "")
                {
                    sb.Append(" AND  TO_DATE(pmst.PRODUCTION_DATE) >=   TO_DATE(@FromPRODUCTION_DATE)   ");
                    cmdInfo.DBParametersInfo.Add("@FromPRODUCTION_DATE", pObj.fromProdDate.ToString());
                }

                if (pObj.toProdDate.ToString() != "")
                {
                    sb.Append(" AND  TO_DATE(pmst.PRODUCTION_DATE) <=   TO_DATE(@ToPRODUCTION_DATE) ");
                    cmdInfo.DBParametersInfo.Add("@ToPRODUCTION_DATE", pObj.toProdDate.ToString());
                }

                if (pObj.prod_id > 0)
                {
                    sb.Append(" AND pmst.PROD_ID = @PROD_ID ");
                    cmdInfo.DBParametersInfo.Add("@PROD_ID", pObj.prod_id);
                }

                if (pObj.prod_no != "")
                {
                    sb.Append(" AND pmst.PROD_NO = @PROD_NO ");
                    cmdInfo.DBParametersInfo.Add("@PROD_ID", pObj.prod_no);
                }

                if (pObj.DeptID == 136)  // Assembly Dept
                {
                    if (pObj.isPacking == "Y")
                    {
                        sb.Append(" AND dtl.IS_PACKING = @IS_PACKING ");
                        cmdInfo.DBParametersInfo.Add("@IS_PACKING", pObj.isPacking);
                    }
                    else
                    {
                        sb.Append(" AND dtl.IS_PACKING IS NULL ");
                        // cmdInfo.DBParametersInfo.Add("@IS_PACKING", pObj.isPacking);
                    }

                }

                if (pObj.DeptID == 18)  // IB Dept Sulphation
                {
                    if (pObj.issulphation == "Y")
                    {
                        sb.Append(" AND pmst.ISSULPHATION = @ISSULPHATION ");
                        cmdInfo.DBParametersInfo.Add("@ISSULPHATION", pObj.issulphation);
                    }
                    else
                    {
                        sb.Append(" AND pmst.ISSULPHATION IS NULL ");
                    }
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_DTL>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcPRODUCTION_MST> GetPRODUCTION_MSTList()
        {
            return GetPRODUCTION_MSTList(null, null);
        }
        public static List<dcPRODUCTION_MST> GetPRODUCTION_MSTList(DBContext dc)
        {
            return GetPRODUCTION_MSTList(null, dc);
        }
        public static List<dcPRODUCTION_MST> GetPRODUCTION_MSTList(DBQuery dbq, DBContext dc)
        {
            List<dcPRODUCTION_MST> cObjList = new List<dcPRODUCTION_MST>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    if (dbq == null)
                    {
                        dbq = new DBQuery();
                    }
                    cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_MST>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcPRODUCTION_MST GetPRODUCTION_MSTByID(int pPRODUCTION_MSTID)
        {
            return GetPRODUCTION_MSTByID(pPRODUCTION_MSTID, null);
        }
        public static dcPRODUCTION_MST GetPRODUCTION_MSTByID(int pPRODUCTION_MSTID, DBContext dc)
        {
            dcPRODUCTION_MST cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcPRODUCTION_MST>()
                                  where c.PROD_ID == pPRODUCTION_MSTID
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

        public static int Insert(dcPRODUCTION_MST cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcPRODUCTION_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcPRODUCTION_MST>(cObj, true);
                if (id > 0) { cObj.PROD_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcPRODUCTION_MST cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcPRODUCTION_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcPRODUCTION_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pPRODUCTION_MSTID)
        {
            return Delete(pPRODUCTION_MSTID, null);
        }
        public static bool Delete(int pPRODUCTION_MSTID, DBContext dc)
        {
            dcPRODUCTION_MST cObj = new dcPRODUCTION_MST();
            cObj.PROD_ID = pPRODUCTION_MSTID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcPRODUCTION_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcPRODUCTION_MST cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcPRODUCTION_MST cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcPRODUCTION_MST cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcPRODUCTION_MST cObj, DBContext dc)
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
                                newID = cObj.PROD_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.PROD_ID, dc))
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
                        ///// Save Production   Details 
                        if (cObj.ProductionDetList != null)
                        {
                            foreach (dcPRODUCTION_DTL det in cObj.ProductionDetList)
                            {
                                det.PROD_MST_ID = newID;
                                // det.PROD_DTL_ID = 0;
                            }
                            bStatus = PRODUCTION_DTLBL.SaveList(cObj.ProductionDetList, dc);
                        }

                        //Save Production Packing Details 
                        if (cObj.ProductionPackingDetList != null)
                        {
                            foreach (dcPRODUCTION_DTL det in cObj.ProductionPackingDetList)
                            {
                                det.PROD_MST_ID = newID;

                            }
                            bStatus = PRODUCTION_DTLBL.SaveList(cObj.ProductionPackingDetList, dc);
                        }


                        //Save Production Cutting Details 
                        if (cObj.ProductionCuttingDetList != null)
                        {
                            foreach (dcPRODUCTION_DTL det in cObj.ProductionCuttingDetList)
                            {
                                det.PROD_MST_ID = newID;
                            }
                            bStatus = PRODUCTION_DTLBL.SaveList(cObj.ProductionCuttingDetList, dc);
                        }

                        //Save Production Filling Details 
                        if (cObj.ProductionFillingDetList != null)
                        {
                            foreach (dcPRODUCTION_DTL det in cObj.ProductionFillingDetList)
                            {
                                det.PROD_MST_ID = newID;
                            }
                            bStatus = PRODUCTION_DTLBL.SaveList(cObj.ProductionFillingDetList, dc);
                        }

                        //Save Production Sulphation Details 
                        if (cObj.ProductionSulphationDetList != null)
                        {
                            foreach (dcPRODUCTION_DTL det in cObj.ProductionSulphationDetList)
                            {
                                det.PROD_MST_ID = newID;
                            }
                            bStatus = PRODUCTION_DTLBL.SaveList(cObj.ProductionSulphationDetList, dc);
                        }

                        // Save Production closing Details OR Used Items Details
                        if (cObj.ProductionClosingDetList != null)
                        {
                            foreach (dcPRODUCTION_FLOOR_CLOSING det in cObj.ProductionClosingDetList)
                            {
                                det.PROD_MST_ID = newID;
                            }

                            bStatus = PRODUCTION_FLOOR_CLOSINGBL.SaveList(cObj.ProductionClosingDetList, dc);
                        }

                        //Assembly Operator save
                        if (cObj.PROD_OPERATOR_LIST != null)
                        {
                            foreach (dcPROD_OPERATOR_LIST det in cObj.PROD_OPERATOR_LIST)
                            {
                                det.PROD_ID = newID;
                            }

                            bStatus = PROD_OPERATOR_LISTBL.SaveList(cObj.PROD_OPERATOR_LIST, dc);
                        }
                        // 
                        ///// Save Pure Lead Dross   Details 
                        if (cObj.DrossDetList != null)
                        {
                            foreach (dcPROD_PURELEAD_DROSS_DTL det in cObj.DrossDetList)
                            {
                                det.PROD_MST_ID = newID;
                                // det.PROD_DTL_ID = 0;
                            }
                            bStatus = PROD_PURELEAD_DROSS_DTLBL.SaveList(cObj.DrossDetList, dc);
                        }
                        dcDEPARTMENT_INFO objstlm = DEPARTMENT_INFOBL.GetStorageLocationById(cObj.STLM_ID, null);
                        //bStatus = true;
                        //if (cObj.STLM_ID != 1 && cObj.STLM_ID != 4 && cObj.STLM_ID != 11 && cObj.STLM_ID != 12)
                        if(cObj.IS_REPAIR != "Y")
                        {
                        
                        if(objstlm.IS_BATCH_USABLE == "Y")
                        {
                            // Production Batch Info 
                            List<dcPRODUCTION_BATCH_INFO> batchList = new List<dcPRODUCTION_BATCH_INFO>();
                            batchList = PRODUCTION_BATCH_INFOBL.GetTempBatchDeptItemInfo(cObj.DEPT_ID, cObj.STLM_ID, null);
                            batchList.Select(x => { x._RecordState = RecordStateEnum.Added; x.PROD_ID = newID; x.PROD_NO = cObj.PROD_NO; return x; }).ToList();
                            if (batchList.Count > 0)
                            {

                                foreach (dcPRODUCTION_BATCH_INFO batchItem in batchList)
                                {
                                    PRODUCTION_BATCH_INFOBL.DeleteByItemID(newID, batchItem.ITEM_ID, batchItem.FG_ITEM_ID, batchItem.MACHINE_ID);
                                }

                                foreach (dcPRODUCTION_BATCH_INFO batchItem in batchList)
                                {
                                    //PRODUCTION_BATCH_INFOBL.DeleteByItemID(newID, batchItem.ITEM_ID, batchItem.FG_ITEM_ID, batchItem.MACHINE_ID, batchItem.PROD_BATCH_NO);
                                    bStatus = PRODUCTION_BATCH_INFOBL.Insert(batchItem) > 0;
                                }
                              
                                if (bStatus)
                                {
                                    PROD_TEMP_BATCH_INFOBL.DeleteByStorageLocation(cObj.DEPT_ID, cObj.STLM_ID, null);
                                }
                            }
                            else
                            {
                                if (cObj._RecordState == RecordStateEnum.Added)
                                    bStatus = false;
                            }

                            // End Production Batch Info
                        }
                        }
                      
                        if (bStatus)
                        {
                            dc.CommitTransaction(isTransInit);
                        }
                        else
                        {
                            dc.RollbackTransaction(isTransInit);
                            newID = 0;
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

        public static bool SaveList(List<dcPRODUCTION_MST> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcPRODUCTION_MST> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcPRODUCTION_MST oDet in detList)
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
                        bool d = Delete(oDet.PROD_ID, dc);
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


        public static DateTime? GetProductionDateByBatchNo_initial(string pBatchNo, DBContext dc)
        {
            bool isDCInit = false;
            DateTime? vProductionDate = null;
            try
            {

                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();

                string abbr = " SELECT FN_BATCH_NO_TO_DATE@SD106(@BATCH_NO) vProductionDate FROM DUAL  ";
                cmdInfo.DBParametersInfo.Add("@BATCH_NO", pBatchNo);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                vProductionDate = Conversion.DBNullDateToNull(DBQuery.ExecuteDBScalar(dbq, dc));

            }
            catch
            {
                // vProductionDate = "NA";
                //throw;

            }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return vProductionDate;
        }


        //Get Production Solar Charging
        public static List<dcPRODUCTION_MST> GetProductionSolarChargingByDate(string pProductionDate, string pTodate, int deptId, string putho_status, DBContext dc)
        {
            List<dcPRODUCTION_MST> cObjList = new List<dcPRODUCTION_MST>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetProductionMstListString());
                //sb.Append("  AND  mst.is_recovery_process = 'N'  ");
                if (pProductionDate.ToString() != "")
                {
                    sb.Append("  AND TO_DATE(mst.PRODUCTION_DATE) >= TO_DATE(@PRODUCTION_DATE)  ");
                    cmdInfo.DBParametersInfo.Add("@PRODUCTION_DATE", pProductionDate);
                }

                if (pTodate.ToString() != "")
                {
                    sb.Append("  AND TO_DATE(mst.PRODUCTION_DATE) <= TO_DATE(@TOPRODUCTION_DATE)  ");
                    cmdInfo.DBParametersInfo.Add("@TOPRODUCTION_DATE", pTodate);
                }

                if (putho_status.ToString() != "")
                {
                    sb.Append(" AND mst.AUTH_STATUS = @AUTH_STATUS ");
                    cmdInfo.DBParametersInfo.Add("@AUTH_STATUS", putho_status);
                }

                if (deptId > 0)
                {
                    sb.Append(" AND mst.DEPT_ID = @DEPT_ID ");
                    cmdInfo.DBParametersInfo.Add("@DEPT_ID", deptId);
                }

              
                sb.Append("  order by mst.PRODUCTION_DATE desc ");
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                //dbq.OrderBy = "mst.PROD_ID Desc";

                cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_MST>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

     

        public static List<dcPRODUCTION_MST> GetBatteryTypeList(string pBatteryTypeID)
        {
            return GetBatteryTypeList(pBatteryTypeID, null);
        }

        public static List<dcPRODUCTION_MST> GetBatteryTypeList(string pBatteryTypeID, DBContext dc)
        {
            List<dcPRODUCTION_MST> cObjList = new List<dcPRODUCTION_MST>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetBatteryTypeListString());
                if (pBatteryTypeID != "")
                {
                    sb.Append(" AND mst.BTY_TYPE_ID= @BTY_TYPE_ID ");
                    cmdInfo.DBParametersInfo.Add("@BTY_TYPE_ID", pBatteryTypeID);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_MST>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }


        public static List<dcPRODUCTION_MST> GetProductionRepairBatteryList(string pProductionDate, string pTodate, int deptId, string ispacking, string putho_status, string isRepair, int StlmId, DBContext dc)
        {
            List<dcPRODUCTION_MST> cObjList = new List<dcPRODUCTION_MST>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetProductionMstListString());
                if (pProductionDate.ToString() != "")
                {
                    sb.Append("  AND TO_DATE(mst.PRODUCTION_DATE) >= TO_DATE(@PRODUCTION_DATE)  ");
                    cmdInfo.DBParametersInfo.Add("@PRODUCTION_DATE", pProductionDate);
                }

                if (pTodate.ToString() != "")
                {
                    sb.Append("  AND TO_DATE(mst.PRODUCTION_DATE) <= TO_DATE(@TOPRODUCTION_DATE)  ");
                    cmdInfo.DBParametersInfo.Add("@TOPRODUCTION_DATE", pTodate);
                }

                if (putho_status.ToString() != "")
                {
                    sb.Append(" AND mst.AUTH_STATUS = @AUTH_STATUS ");
                    cmdInfo.DBParametersInfo.Add("@AUTH_STATUS", putho_status);
                }

                if (deptId > 0)
                {
                    sb.Append(" AND mst.DEPT_ID = @DEPT_ID ");
                    cmdInfo.DBParametersInfo.Add("@DEPT_ID", deptId);
                }

                if (StlmId > 0)
                {
                    sb.Append(" AND mst.STLM_ID = @StlmId ");
                    cmdInfo.DBParametersInfo.Add("@StlmId", StlmId);
                }

                if (ispacking != "")
                {
                    sb.Append(" AND mst.IS_PACKING = @pIS_PACKING ");
                    cmdInfo.DBParametersInfo.Add("@pIS_PACKING", ispacking);
                }
                if (isRepair != "")
                {
                    sb.Append(" AND mst.IS_REPAIR = @isRepair ");
                    cmdInfo.DBParametersInfo.Add("@isRepair", isRepair);
                }
                sb.Append("  order by mst.PRODUCTION_DATE desc ");
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                //dbq.OrderBy = "mst.PROD_ID Desc";

                cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_MST>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }




        #region DMW

        public static dcPRODUCTION_MST GetProductionDMWByProdID(string pProd_ID, DBContext dc)
        {
            dcPRODUCTION_MST cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetProductionMstListString());
                if (pProd_ID != "")
                {
                    sb.Append(" AND mst.PROD_ID=@PROD_ID ");
                    cmdInfo.DBParametersInfo.Add("@PROD_ID", pProd_ID);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_MST>(dbq, dc).FirstOrDefault();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static string GetDMWaterbyIDListString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("  SELECT PROD_ID,PROD_NO,PRODUCTION_DATE,DEPT_ID ");
            sb.Append(" ,ENTRY_BY_ID,ENTRY_DATE,EDIT_BY_ID,EDIT_DATE,AUTH_STATUS,AUTH_DATE,AUTH_BY_ID ");
            //sb.Append(" ,mst.UOM ,U.UOM_CODE UOM_NAME ");
            sb.Append(" FROM PRODUCTION_MST ");
            //sb.Append(" LEFT JOIN UOM_INFO U ON mst.UOM= U.UOM_ID  ");
            sb.Append(" Where 1=1 ");

            return sb.ToString();
        }


        public static List<dcPRODUCTION_MST> GetProductionDMWaterByDate(string pProductionDateFrom, string pProductionDateto, int deptid, string is_Authorized, DBContext dc)
        {
            List<dcPRODUCTION_MST> cObjList = new List<dcPRODUCTION_MST>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetDMWaterbyIDListString());
                if (pProductionDateFrom.ToString() != "")
                {
                    sb.Append("  AND TO_DATE(PRODUCTION_DATE) Between TO_DATE(@pProductionDateFrom) and  TO_DATE(@pProductionDateto) ");
                    cmdInfo.DBParametersInfo.Add("@pProductionDateFrom", pProductionDateFrom);
                    cmdInfo.DBParametersInfo.Add("@pProductionDateto", pProductionDateto);
                }

                if (deptid > 0)
                {
                    sb.Append(" AND DEPT_ID=@deptid ");
                    cmdInfo.DBParametersInfo.Add("@deptid", deptid);

                }
                if (is_Authorized != "0")
                {
                    sb.Append("  AND AUTH_STATUS=@is_Authorized ");
                    cmdInfo.DBParametersInfo.Add("@is_Authorized", is_Authorized);
                }
                //sb.Append("  AND  IS_RM='N' ");
                //sb.Append("  order by PRODUCTION_DATE desc ");
                sb.Append(" order by PROD_NO desc  ");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_MST>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }


        #endregion


        #region pure Lead

        public static List<dcPRODUCTION_MST> GetProductionPureLeadByDate(string pProductionDate, string pTodate, int deptId, string putho_status, DBContext dc)
        {
            List<dcPRODUCTION_MST> cObjList = new List<dcPRODUCTION_MST>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetProductionMstListString());
                if (pProductionDate.ToString() != "")
                {
                    sb.Append("  AND TO_DATE(mst.PRODUCTION_DATE) >= TO_DATE(@PRODUCTION_DATE)  ");
                    cmdInfo.DBParametersInfo.Add("@PRODUCTION_DATE", pProductionDate);
                }

                if (pTodate.ToString() != "")
                {
                    sb.Append("  AND TO_DATE(mst.PRODUCTION_DATE) <= TO_DATE(@TOPRODUCTION_DATE)  ");
                    cmdInfo.DBParametersInfo.Add("@TOPRODUCTION_DATE", pTodate);
                }

                if (putho_status.ToString() != "")
                {
                    sb.Append(" AND mst.AUTH_STATUS = @AUTH_STATUS ");
                    cmdInfo.DBParametersInfo.Add("@AUTH_STATUS", putho_status);
                }

                if (deptId > 0)
                {
                    sb.Append(" AND mst.DEPT_ID = @DEPT_ID ");
                    cmdInfo.DBParametersInfo.Add("@DEPT_ID", deptId);
                }

                sb.Append("  order by mst.PRODUCTION_DATE desc ");
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                //dbq.OrderBy = "mst.PROD_ID Desc";

                cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_MST>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        #endregion


        #region MRB

        public static string GetProductionMRBMstListString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT   ");
            sb.Append("  mst.PROD_NO  ");
            sb.Append(" ,mst.PROD_ID  ");
            sb.Append(" ,mst.FACTORY_ID ");
            sb.Append(" ,mst.SHIFT_ID ");
            sb.Append(" ,mst.SUPERVISOR_ID ");
            sb.Append(" ,mst.ENTRY_BY_ID ");
            sb.Append(" ,us.FULLNAME   ENTRY_BY ");
            sb.Append(" ,mst.ENTRY_DATE ");
            sb.Append(" ,mst.EDIT_BY_ID ");
            sb.Append(" ,mst.EDIT_DATE ");
            sb.Append(" ,mst.DEPT_ID ");
            sb.Append(" ,mst.FORECUST_ID ");
            sb.Append(" ,mst.REJECTED_QTY ");
            sb.Append(" ,mst.REF_NO_MANUAL ");
            sb.Append(" ,TO_CHAR( mst.BATCH_STARTTIME , 'DD-MON-YYYY') BATCH_STARTTIME ");
            sb.Append(" ,TO_CHAR( mst.BATCH_ENDTIME, 'DD-MON-YYYY')  BATCH_ENDTIME ");
            sb.Append(" ,mst.AUTH_STATUS ");
            sb.Append(" ,mst.AUTH_BY_ID ");
            sb.Append(" ,mst.AUTH_DATE ");
            sb.Append(" ,mst.STARTTIME ");
            sb.Append(" ,mst.ENDTIME ");
            sb.Append(" ,mst.BATCH_ID ");
            sb.Append(" ,mst.AUTH_STATUS ");
            sb.Append(" ,TO_CHAR( mst.PRODUCTION_DATE, 'DD-MON-YYYY')  PRODUCTION_DATE ");
            sb.Append(" ,mst.PROCESS_CODE ");
            sb.Append(" ,svr.FULL_NAME,dept.DEPARTMENT_NAME,shift.SHIFT_NAME, Fmst.FOR_MONTH FORECUSTMONTH ,Fmst.FOR_YEAR  FORECUSTYEAR,mst.PROD_BATCH_NO ");
            sb.Append("  ,mst.SHIFT_INCHARGE ");
            sb.Append(" ,incharge.FULL_NAME SHIFT_INCHARGE_NAME ");
            sb.Append(" ,(Select PROD_ID from  PRODUCTION_MST where REF_PROD_ID=mst.PROD_ID AND ROWNUM=1) UN_LOADED_PROD_ID ");
            sb.Append(" FROM PRODUCTION_MST mst  ");
            sb.Append(" LEFT JOIN SUPPERVISOR_MST  svr ON mst.SUPERVISOR_ID = svr.EMP_ID AND svr.DEPT_ID=mst.DEPT_ID   ");
            sb.Append(" LEFT JOIN DEPARTMENT_INFO dept ON mst.DEPT_ID=dept.DEPARTMENT_ID  ");
            sb.Append(" LEFT JOIN SHIFT_MST shift ON mst.SHIFT_ID=shift.SHIFT_ID  ");
            sb.Append(" LEFT JOIN  PROD_TBLFORECAST_MST Fmst ON mst.FORECUST_ID=Fmst.FC_ID ");
            sb.Append(" LEFT JOIN TBLUSER us ON mst.ENTRY_BY_ID=us.USERID ");
            sb.Append(" LEFT JOIN SUPPERVISOR_MST  incharge  ON mst.SHIFT_INCHARGE = incharge.EMP_ID  and mst.DEPT_ID=incharge.DEPT_ID  ");
            sb.Append(" WHERE 1=1  ");
            return sb.ToString();
            // AND incharge.DEPT_ID=mst.DEPT_ID
        }
        
        public static List<dcPRODUCTION_MST> GetProductionMRBByDate(string pProductionDate, string pTodate, int deptId, string putho_status, DBContext dc)
        {
            List<dcPRODUCTION_MST> cObjList = new List<dcPRODUCTION_MST>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetProductionMRBMstListString());
                if (pProductionDate.ToString() != "")
                {
                    sb.Append("  AND TO_DATE(mst.PRODUCTION_DATE) >= TO_DATE(@PRODUCTION_DATE)  ");
                    cmdInfo.DBParametersInfo.Add("@PRODUCTION_DATE", pProductionDate);
                }

                if (pTodate.ToString() != "")
                {
                    sb.Append("  AND TO_DATE(mst.PRODUCTION_DATE) <= TO_DATE(@TOPRODUCTION_DATE)  ");
                    cmdInfo.DBParametersInfo.Add("@TOPRODUCTION_DATE", pTodate);
                }

                if (putho_status.ToString() != "")
                {
                    sb.Append(" AND mst.AUTH_STATUS = @AUTH_STATUS ");
                    cmdInfo.DBParametersInfo.Add("@AUTH_STATUS", putho_status);
                }

                if (deptId > 0)
                {
                    sb.Append(" AND mst.DEPT_ID = @DEPT_ID ");
                    cmdInfo.DBParametersInfo.Add("@DEPT_ID", deptId);
                }

                sb.Append("  order by mst.PROD_NO desc ");
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                //dbq.OrderBy = "mst.PROD_ID Desc";

                cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_MST>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }


        public static dcPRODUCTION_MST GetProductionMRBByProdID(string pProd_ID, DBContext dc)
        {
            dcPRODUCTION_MST cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetProductionMRBMstListString());
                if (pProd_ID != "")
                {
                    sb.Append(" AND mst.PROD_ID=@PROD_ID ");
                    cmdInfo.DBParametersInfo.Add("@PROD_ID", pProd_ID);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_MST>(dbq, dc).FirstOrDefault();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        
        #endregion

        #region Batch
        public static string GetProductionDateBatchNO(clsPrmInventory pObj, DBContext dc)
        {
            bool isDCInit = false;
            string vProductionDate = string.Empty;
            try
            {

                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();

                string abbr = " SELECT FN_GET_BATCH_PRODUCTION_DATE(@LOC_ID,@BATCH_NO) vProductionDate FROM DUAL  ";
                cmdInfo.DBParametersInfo.Add("@LOC_ID", pObj.LocCode);
                cmdInfo.DBParametersInfo.Add("@BATCH_NO", pObj.BatchNO);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                vProductionDate = DBQuery.ExecuteDBScalar(dbq, dc).ToString();

            }
            catch
            {
                // vProductionDate = "NA";
                //throw;

            }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return vProductionDate;
        }
        #endregion 


        #region Recovery Lead
//string pProductionDate, string pTodate, int deptId, string putho_status
        //public static List<dcPRODUCTION_MST> GetProductionRecoveryList(clsPrmInventory pObj , DBContext dc)
        //{
        //    List<dcPRODUCTION_MST> cObjList = new List<dcPRODUCTION_MST>();
        //    bool isDCInit = false;
        //    try
        //    {
        //        isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

        //        DBCommandInfo cmdInfo = new DBCommandInfo();
        //      
        //        //sb.Append("  AND  mst.is_recovery_process = 'Y'  ");

        //        if (pObj.FromDate.ToString() != "")
        //        {
        //            sb.Append("  AND TO_DATE(mst.PRODUCTION_DATE) >= TO_DATE(@PRODUCTION_DATE)  ");
        //            cmdInfo.DBParametersInfo.Add("@PRODUCTION_DATE", pObj.FromDate.Value);
        //        }

        //        if (pObj.ToDate.ToString() != "")
        //        {
        //            sb.Append("  AND TO_DATE(mst.PRODUCTION_DATE) <= TO_DATE(@TOPRODUCTION_DATE)  ");
        //            cmdInfo.DBParametersInfo.Add("@TOPRODUCTION_DATE", pObj.ToDate.Value);
        //        }

        //        if (pObj.autho_status.ToString() != "")
        //        {
        //            sb.Append(" AND mst.AUTH_STATUS = @AUTH_STATUS ");
        //            cmdInfo.DBParametersInfo.Add("@AUTH_STATUS", pObj.autho_status);
        //        }

        //        if (pObj.DeptID > 0)
        //        {
        //            sb.Append(" AND mst.DEPT_ID = @DEPT_ID ");
        //            cmdInfo.DBParametersInfo.Add("@DEPT_ID", pObj.DeptID.ToString());
        //        }

        //        sb.Append("  order by mst.PRODUCTION_DATE desc ");
        //        DBQuery dbq = new DBQuery();
        //        dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
        //        cmdInfo.CommandText = sb.ToString();
        //        cmdInfo.CommandType = CommandType.Text;
        //        dbq.DBCommandInfo = cmdInfo;
        //        //dbq.OrderBy = "mst.PROD_ID Desc";

        //        cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_MST>(dbq, dc);
        //    }
        //    catch { throw; }
        //    finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
        //    return cObjList;
        //}

        public static List<dcPRODUCTION_MST> GetProductionRecoveryList(string pProductionDate, string pTodate, int deptId, string putho_status, DBContext dc)
        {
            List<dcPRODUCTION_MST> cObjList = new List<dcPRODUCTION_MST>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetProductionMstListString());
                sb.Append("  AND  mst.is_recovery_process = 'Y'  ");
                 

                if (pProductionDate.ToString() != "")
                {
                    sb.Append("  AND TO_DATE(mst.PRODUCTION_DATE) >= TO_DATE(@PRODUCTION_DATE)  ");
                    cmdInfo.DBParametersInfo.Add("@PRODUCTION_DATE", pProductionDate);
                }

                if (pTodate.ToString() != "")
                {
                    sb.Append("  AND TO_DATE(mst.PRODUCTION_DATE) <= TO_DATE(@TOPRODUCTION_DATE)  ");
                    cmdInfo.DBParametersInfo.Add("@TOPRODUCTION_DATE", pTodate);
                }

                if (putho_status.ToString() != "")
                {
                    sb.Append(" AND mst.AUTH_STATUS = @AUTH_STATUS ");
                    cmdInfo.DBParametersInfo.Add("@AUTH_STATUS", putho_status);
                }

                if (deptId > 0)
                {
                    sb.Append(" AND mst.DEPT_ID = @DEPT_ID ");
                    cmdInfo.DBParametersInfo.Add("@DEPT_ID", deptId);
                }

                sb.Append("  order by mst.PRODUCTION_DATE desc ");
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                //dbq.OrderBy = "mst.PROD_ID Desc";

                cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_MST>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        #endregion

        #region QA


        public static string GetProductionMstListStringAfterQA()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT   ");
            sb.Append("  mst.PROD_NO  ");
            sb.Append(" ,mst.PROD_ID  ");
            sb.Append(" ,mst.FACTORY_ID ");
            sb.Append(" ,mst.SHIFT_ID ");
            sb.Append(" ,mst.SUPERVISOR_ID ");
            sb.Append(" ,mst.ENTRY_BY_ID ");
            sb.Append(" ,us.FULLNAME   ENTRY_BY ");
            sb.Append(" ,mst.ENTRY_DATE ");
            sb.Append(" ,mst.EDIT_BY_ID ");
            sb.Append(" ,mst.EDIT_DATE ");
            sb.Append(" ,mst.DEPT_ID ");
            sb.Append(" ,mst.FORECUST_ID ");
            sb.Append(" ,mst.REJECTED_QTY ");
            sb.Append(" ,mst.REF_NO_MANUAL ");
            sb.Append(" ,TO_CHAR( mst.BATCH_STARTTIME , 'DD-MON-YYYY') BATCH_STARTTIME ");
            sb.Append(" ,TO_CHAR( mst.BATCH_ENDTIME, 'DD-MON-YYYY')  BATCH_ENDTIME ");
            sb.Append(" ,mst.AUTH_STATUS ");
            sb.Append(" ,mst.AUTH_BY_ID ");
            sb.Append(" ,mst.AUTH_DATE ");
            sb.Append(" ,mst.STARTTIME ");
            sb.Append(" ,mst.ENDTIME ");
            sb.Append(" ,mst.BATCH_ID ");
            sb.Append(" ,mst.AUTH_STATUS ");
            sb.Append(" ,TO_CHAR( mst.PRODUCTION_DATE, 'DD-MON-YYYY')  PRODUCTION_DATE ");
            sb.Append(" ,mst.PROCESS_CODE ");
            sb.Append(" ,svr.FULL_NAME,dept.DEPARTMENT_NAME,shift.SHIFT_NAME, Fmst.FOR_MONTH FORECUSTMONTH ,Fmst.FOR_YEAR  FORECUSTYEAR,mst.PROD_BATCH_NO ");
            sb.Append("  ,mst.SHIFT_INCHARGE ");
            sb.Append(" ,incharge.FULL_NAME SHIFT_INCHARGE_NAME ");
            sb.Append(" ,(Select PROD_ID from  PRODUCTION_MST where REF_PROD_ID=mst.PROD_ID AND ROWNUM=1) UN_LOADED_PROD_ID,prod_qa.PROD_QA_NO ");
            sb.Append(" FROM PRODUCTION_MST mst  ");
            sb.Append(" INNER JOIN SUPPERVISOR_MST  svr ON mst.SUPERVISOR_ID = svr.EMP_ID AND svr.DEPT_ID=mst.DEPT_ID   ");
            sb.Append(" INNER JOIN DEPARTMENT_INFO dept ON mst.DEPT_ID=dept.DEPARTMENT_ID  ");
            sb.Append(" INNER JOIN SHIFT_MST shift ON mst.SHIFT_ID=shift.SHIFT_ID  ");
            sb.Append(" LEFT JOIN  PROD_TBLFORECAST_MST Fmst ON mst.FORECUST_ID=Fmst.FC_ID ");
            sb.Append(" INNER JOIN TBLUSER us ON mst.ENTRY_BY_ID=us.USERID ");
            sb.Append(" LEFT JOIN SUPPERVISOR_MST  incharge  ON mst.SHIFT_INCHARGE = incharge.EMP_ID  and mst.DEPT_ID=incharge.DEPT_ID  ");
            sb.Append(" INNER JOIN PROD_QA_OPERATION_MST prod_qa ON mst.PROD_ID=prod_qa.PROD_ID ");
            sb.Append(" WHERE 1=1  ");
            return sb.ToString();
            // AND incharge.DEPT_ID=mst.DEPT_ID
        }


        public static dcPRODUCTION_MST GetProductionByProdIDAfterQA(string pProd_QA_ID, DBContext dc)
        {
            dcPRODUCTION_MST cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetProductionMstListStringAfterQA());
                if (pProd_QA_ID != "")
                {
                    sb.Append(" AND prod_qa.PROD_QA_ID=@pProd_QA_ID ");
                    cmdInfo.DBParametersInfo.Add("@pProd_QA_ID", pProd_QA_ID);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_MST>(dbq, dc).FirstOrDefault();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }


        //public static Int64 GET_PRODUCTION_QTYByPROD_IDForValidation(int pProd_ID, DBContext dc)
        //{
        //    bool isDCInit = false;
        //    string _msg = string.Empty;
        //    try
        //    {
        //        isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
        //        DBCommandInfo cmdInfo = new DBCommandInfo();
        //        string abbr = "";
        //        abbr = " SELECT FN_IS_NEW_FORM_LOADING_VALID(@P_MACHINE_ID,@P_SHIFT_NO,@P_DATE) A from Dual ";
        //        cmdInfo.DBParametersInfo.Add("@P_MACHINE_ID", machineId);
        //        cmdInfo.DBParametersInfo.Add("@P_SHIFT_NO", shiftName);
        //        cmdInfo.DBParametersInfo.Add("@P_DATE", prod_date.Value);
        //        DBQuery dbq = new DBQuery();
        //        dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
        //        cmdInfo.CommandText = abbr;
        //        cmdInfo.CommandType = CommandType.Text;
        //        dbq.DBCommandInfo = cmdInfo;
        //        _msg = Convert.ToString(DBQuery.ExecuteDBScalar(dbq, dc));
        //    }
        //    catch { throw; }
        //    finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
        //    return _msg;
        //}

        public static int GET_PRODUCTION_QTYByPROD_IDForValidation(int pProd_ID, DBContext dc)
        {
            //dcINVOICE_MASTER cObj = null;
            bool isDCInit = false;
            int tot_qty = 0;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder("Select SUM(NVL(ITEM_QTY,0)) FROM PRODUCTION_DTL  WHERE PROD_MST_ID=@pProd_ID ");
                cmdInfo.DBParametersInfo.Add("@pProd_ID", pProd_ID);


                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                tot_qty = Convert.ToInt32(DBQuery.ExecuteDBScalar(dbq, dc));
                //cObj = DBQuery.ExecuteDBQuery<dcINVOICE_MASTER>(dbq, dc).FirstOrDefault();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return tot_qty;
        }

        public static void UpdateProduction_MstForQCPassByProdNoGridCasting(int pProd_ID, DBContext dc)
        {
            bool isDCInit = false;
            //string _vBalance = string.Empty;
            // DBContext dc = null;
            try
            {

                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();

                string abbr = " UPDATE PRODUCTION_MST SET IS_QA_PASS = 'Y' WHERE 1=1 AND PROD_ID = :pProd_ID ";

                cmdInfo.DBParametersInfo.Add(":pProd_ID", pProd_ID);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBNonQuery(dbq, dc);
                //DBQuery.ExecuteDBQuerySP(dbq, dc);

                //ExecuteDBNonQuery(dbq, dc);

            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            //return _vBalance;
        }

        public static void UpdateProduction_MstAuthorizedForQCPassByProdNoGridCasting(int pProd_ID, DBContext dc)
        {
            bool isDCInit = false;
            //string _vBalance = string.Empty;
            // DBContext dc = null;
            try
            {

                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();

                string abbr = " UPDATE PRODUCTION_MST SET AUTH_STATUS = 'Y' WHERE 1=1 AND PROD_ID = :pProd_ID ";

                cmdInfo.DBParametersInfo.Add(":pProd_ID", pProd_ID);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBNonQuery(dbq, dc);
                //DBQuery.ExecuteDBQuerySP(dbq, dc);

                //ExecuteDBNonQuery(dbq, dc);

            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            //return _vBalance;
        }

        public static List<dcPRODUCTION_MST> GetListProductionDetailsAfterQA(clsPrmInventory pObj, DBContext dc)
        {
            List<dcPRODUCTION_MST> cObjList = new List<dcPRODUCTION_MST>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetProductionMstListString());
                if (pObj.fromProdDate.ToString() != "")
                {
                    sb.Append("  AND TO_DATE(mst.PRODUCTION_DATE) >= TO_DATE(@PRODUCTION_DATE)  ");
                    cmdInfo.DBParametersInfo.Add("@PRODUCTION_DATE", pObj.fromProdDate);
                }
                if (pObj.toProdDate.ToString() != "")
                {
                    sb.Append("  AND TO_DATE(mst.PRODUCTION_DATE) <= TO_DATE(@TO_DATE)  ");
                    cmdInfo.DBParametersInfo.Add("@TO_DATE", pObj.toProdDate);
                }
                if (pObj.autho_status.ToString() != "")
                {
                    sb.Append(" AND mst.AUTH_STATUS = @AUTH_STATUS ");
                    cmdInfo.DBParametersInfo.Add("@AUTH_STATUS", pObj.autho_status);
                }
                if (pObj.DeptID > 0)
                {
                    sb.Append(" AND mst.DEPT_ID = @DEPT_ID ");
                    cmdInfo.DBParametersInfo.Add("@DEPT_ID", pObj.DeptID);
                }
                if (pObj.StorageLocationId > 0)
                {
                    sb.Append(" AND mst.STLM_ID = @STLM_ID ");
                    cmdInfo.DBParametersInfo.Add("@STLM_ID", pObj.StorageLocationId);
                }

                if (pObj.isElectrolyte == "Y")
                {
                    sb.Append(" AND mst.IS_ELECTROLYTE = @isElectrolyte ");
                    cmdInfo.DBParametersInfo.Add("@isElectrolyte", pObj.isElectrolyte);
                }
                else
                {
                    sb.Append(" AND mst.IS_ELECTROLYTE = 'N' ");
                }
                sb.Append(" AND mst.IS_QA_PASS = 'N' ");
                sb.Append("  order by mst.PRODUCTION_DATE desc ");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_MST>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        #endregion
    }
}
