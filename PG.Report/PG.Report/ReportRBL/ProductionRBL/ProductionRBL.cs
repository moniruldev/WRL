using OfficeOpenXml;
using OfficeOpenXml.Style;
using PG.BLLibrary.InventoryBL;
using PG.Core.DBBase;
using PG.Core.Utility;
using PG.DBClass.InventoryDC;
using PG.DBClass.ProductionDC;
//using PG.Report.ReportClass.InventoryRC;
using PG.Report.ReportClass.ProductionRC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace PG.Report.ReportRBL.ProductionRBL
{
    public class ProductionRBL
    {


        #region SQL Query Section
        public static string GetProductionDtlListString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT   ");
            sb.Append(" pmst.PROD_ID  ");
            sb.Append(" ,pmst.PROD_NO   ");
            sb.Append(" ,CONCAT( CONCAT( pmst.SUPERVISOR_ID,': ') ,supper.FULL_NAME) SUPERVISOR_NAME  ");
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
            sb.Append(" ,(CASE WHEN dtl.IS_UNFORMED='Y' THEN dtl.ITEM_QTY ELSE 0 END)UF_REUSE_QTY ");
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
            sb.Append(" , shift.SHIFT_MSTID");
            sb.Append(",inv.ITEM_STANDARD_WEIGHT_KG");
            sb.Append(",(dtl.ITEM_QTY/ dtl.PANEL_PC)  PANEL_QTY, stm.NAME STLM_NAME");
            
            sb.Append(" FROM   ");
            sb.Append(" PRODUCTION_MST pmst  ");
            sb.Append(" INNER JOIN  PRODUCTION_DTL dtl ON pmst.PROD_ID=dtl.PROD_MST_ID  ");
            sb.Append(" INNER JOIN INV_ITEM_MASTER inv ON dtl.ITEM_ID=inv.ITEM_ID  ");
            sb.Append(" INNER JOIN UOM_INFO itmUOM  ON dtl.UOM_ID=itmUOM.UOM_ID  ");
            sb.Append(" INNER JOIN DEPARTMENT_INFO dept ON pmst.DEPT_ID=dept.DEPARTMENT_ID  ");
            sb.Append(" LEFT JOIN SUPPERVISOR_MST supper ON supper.EMP_ID=pmst.SUPERVISOR_ID  and pmst.DEPT_ID=supper.DEPT_ID ");
            sb.Append(" INNER JOIN SHIFT_MST shift ON pmst.SHIFT_ID=shift.SHIFT_ID  ");
            sb.Append(" LEFT JOIN MACHINE_MST mac ON dtl.MACHINE_ID=mac.MACHINE_ID  ");
            sb.Append(" LEFT JOIN PROD_RM_FORECAST_MST fmst ON pmst.FORECUST_ID= fmst.RM_FC_ID  ");
            sb.Append(" LEFT JOIN UOM_INFO barU ON dtl.BAR_TYPE=barU.UOM_ID  ");
            sb.Append(" LEFT JOIN UOM_INFO panel on dtl.PANEL_UOM_ID=panel.UOM_ID  ");
            sb.Append(" LEFT JOIN SUPPERVISOR_MST OpMst On dtl.OPERATOR_ID=OpMst.EMP_ID and pmst.DEPT_ID=OpMst.DEPT_ID   ");
            sb.Append(" LEFT JOIN BOM_MST_T bom ON dtl.BOM_ID=bom.BOM_ID  ");
            sb.Append(" LEFT JOIN UOM_INFO weightUOM ON dtl.WEIGHT_UOM_ID=weightUOM.UOM_ID  ");
            sb.Append(" LEFT JOIN UOM_INFO bar ON dtl.BAR_TYPE=bar.UOM_ID  ");
            sb.Append(" LEFT JOIN TBLUSER us  ON us.USERID=pmst.ENTRY_BY_ID  ");
            sb.Append(" LEFT JOIN STORAGE_LOCATION_MST stm ON pmst.STLM_ID=stm.STLM_ID ");
            sb.Append(" WHERE 1=1  ");
            return sb.ToString();
        }

        public static string Get_Used_Raw_Material_SqlString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT ");
            sb.Append(" pmst.PROD_ID,pmst.PROD_NO,pmst.SUPERVISOR_ID,pmst.DEPT_ID,pmst.REF_NO_MANUAL ");
            sb.Append(" ,pmst.SHIFT_ID SHIFT_NAME,pmst.BATCH_STARTTIME ,pmst.BATCH_ENDTIME  ,pmst.STARTTIME ,pmst.ENDTIME  ");
            sb.Append(" ,pmst.PROCESS_CODE,pmst.PRODUCTION_DATE ");
            sb.Append(" ,pmst.REJECTED_QTY,pmst.BATCH_ID,pmst.PROD_BATCH_NO,dept.DEPARTMENT_NAME ");
            sb.Append(" ,inv.ITEM_NAME,inv.ITEM_ID  ");
            sb.Append(" ,fc.ISSUE_STOCK ITEM_QTY ");
            sb.Append(" ,itmUOM.UOM_CODE_SHORT UOM_NAME  ");
            sb.Append(" ,us.FULLNAME CREATE_BY ");
            sb.Append(" ,pmst.ENTRY_DATE ");
            sb.Append(" ,shift.SHIFT_MSTID    ");
            sb.Append(" ,CONCAT( CONCAT( pmst.SUPERVISOR_ID,': ') ,supper.FULL_NAME) SUPERVISOR_NAME,stm.NAME STLM_NAME  ");
            sb.Append(" FROM ");
            sb.Append(" PRODUCTION_FLOOR_CLOSING fc  ");
            sb.Append(" INNER JOIN PRODUCTION_MST pmst ON fc.PROD_MST_ID=pmst.PROD_ID ");
            sb.Append(" INNER JOIN INV_ITEM_MASTER inv ON fc.CLOSING_ITEM_ID=inv.ITEM_ID  ");
            sb.Append(" INNER JOIN UOM_INFO itmUOM  ON fc.CLOSING_UOM_ID=itmUOM.UOM_ID  ");
            sb.Append(" LEFT JOIN SUPPERVISOR_MST supper ON supper.EMP_ID=pmst.SUPERVISOR_ID  AND pmst.DEPT_ID= supper.DEPT_ID ");
            sb.Append(" INNER JOIN DEPARTMENT_INFO dept ON pmst.DEPT_ID=dept.DEPARTMENT_ID  ");
            sb.Append(" INNER JOIN SHIFT_MST shift ON pmst.SHIFT_ID=shift.SHIFT_ID         ");
            sb.Append(" LEFT JOIN TBLUSER us  ON us.USERID=pmst.ENTRY_BY_ID  ");
            sb.Append(" LEFT JOIN STORAGE_LOCATION_MST stm ON pmst.STLM_ID=stm.STLM_ID ");
            sb.Append(" WHERE 1=1  ");
            return sb.ToString();
        }



        public static string GetProductionDtlListString(clsPrmInventory pObj)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT   ");
            sb.Append(" pmst.PROD_ID  ");
            sb.Append(" ,pmst.PROD_NO   ");
            sb.Append(" ,CONCAT( CONCAT( pmst.SUPERVISOR_ID,': ') ,supper.FULL_NAME) SUPERVISOR_NAME  ");
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
            sb.Append(" , shift.SHIFT_MSTID");
            sb.Append(",inv.ITEM_STANDARD_WEIGHT_KG");
            sb.Append(",(dtl.ITEM_QTY/ dtl.PANEL_PC)  PANEL_QTY");
            sb.Append(",dept.PRODUCTION_CAPACITY ");
            if(pObj.fromProdDate != "" && pObj.toProdDate!="")
                sb.Append(", TO_DATE('" + pObj.toProdDate + "' ) - TO_DATE('" + pObj.fromProdDate + "')+1 DATEDIFF ");

            sb.Append(" FROM   ");
            sb.Append(" PRODUCTION_MST pmst  ");
            sb.Append(" INNER JOIN  PRODUCTION_DTL dtl ON pmst.PROD_ID=dtl.PROD_MST_ID  ");
            sb.Append(" INNER JOIN INV_ITEM_MASTER inv ON dtl.ITEM_ID=inv.ITEM_ID  ");
            sb.Append(" INNER JOIN UOM_INFO itmUOM  ON dtl.UOM_ID=itmUOM.UOM_ID  ");
            sb.Append(" INNER JOIN DEPARTMENT_INFO dept ON pmst.DEPT_ID=dept.DEPARTMENT_ID  ");
            sb.Append(" INNER JOIN SHIFT_MST shift ON pmst.SHIFT_ID=shift.SHIFT_ID  ");
            sb.Append(" LEFT JOIN SUPPERVISOR_MST supper ON supper.EMP_ID=pmst.SUPERVISOR_ID AND pmst.DEPT_ID= supper.DEPT_ID  ");
            sb.Append(" LEFT JOIN MACHINE_MST mac ON dtl.MACHINE_ID=mac.MACHINE_ID  ");
            sb.Append(" LEFT JOIN PROD_RM_FORECAST_MST fmst ON pmst.FORECUST_ID= fmst.RM_FC_ID  ");
            sb.Append(" LEFT JOIN UOM_INFO barU ON dtl.BAR_TYPE=barU.UOM_ID  ");
            sb.Append(" LEFT JOIN UOM_INFO panel on dtl.PANEL_UOM_ID=panel.UOM_ID  ");
            sb.Append(" LEFT JOIN SUPPERVISOR_MST OpMst On dtl.OPERATOR_ID=OpMst.EMP_ID AND pmst.DEPT_ID= supper.DEPT_ID ");
            sb.Append(" LEFT JOIN BOM_MST_T bom ON dtl.BOM_ID=bom.BOM_ID  ");
            sb.Append(" LEFT JOIN UOM_INFO weightUOM ON dtl.WEIGHT_UOM_ID=weightUOM.UOM_ID  ");
            sb.Append(" LEFT JOIN UOM_INFO bar ON dtl.BAR_TYPE=bar.UOM_ID  ");
            sb.Append(" LEFT JOIN TBLUSER us  ON us.USERID=pmst.ENTRY_BY_ID  ");
            sb.Append(" WHERE 1=1  ");
            return sb.ToString();
        }
        public static string Get_IB_Used_RM_queryString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT   ");
            sb.Append(" WHERE 1=1  ");
            return sb.ToString();
        }
        #endregion


        public static List<rcProduction> Department_Production_Report(clsPrmInventory rptClass)
        {
            return Department_Production_Report(rptClass, null);
        }
        public static List<rcProduction> Department_Production_Report(clsPrmInventory pObj, DBContext dc)
        {
            List<rcProduction> cObjList = new List<rcProduction>();
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
                if (pObj.From_Dept_Id > 0)
                {
                    sb.Append(" AND pmst.DEPT_ID = @DEPT_ID ");
                    cmdInfo.DBParametersInfo.Add("@DEPT_ID", pObj.From_Dept_Id);
                }
                sb.Append(" AND pmst.PROD_NO NOT LIKE 'CHE%' AND pmst.PROD_NO NOT LIKE 'MCH%' ");
                //if (pObj.From_Dept_Id ==113)
                //{
                //    sb.Append(" AND pmst.PROD_NO not like 'MCH%' ");
                    
                //}

                //if (pObj.From_Dept_Id == 54)
                //{
                //    sb.Append(" AND pmst.PROD_NO not like 'MCH%' ");

                //}

                if(pObj.StorageLocationId>0)
                {
                    sb.Append(" AND pmst.STLM_ID= @StorageLocationId ");
                    cmdInfo.DBParametersInfo.Add("@StorageLocationId", pObj.StorageLocationId);
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

                if (pObj.isElectrolyte == "Y")
                {
                    sb.Append(" AND pmst.IS_ELECTROLYTE = @IS_ELECTROLYTE ");
                    cmdInfo.DBParametersInfo.Add("@IS_ELECTROLYTE", pObj.isElectrolyte);
                }

                if (pObj.item_id > 0)
                {
                    sb.Append(" AND dtl.ITEM_ID = @P_ITEM_ID ");
                    cmdInfo.DBParametersInfo.Add("@P_ITEM_ID", pObj.item_id);
                }

                if(pObj.itemGroup_id > 0)
                {
                    sb.Append(" AND inv.ITEM_GROUP_ID = @P_ITEM_GROUP_ID ");
                    cmdInfo.DBParametersInfo.Add("@P_ITEM_GROUP_ID", pObj.itemGroup_id);
                }

                if (pObj.prod_no != "")
                {
                    sb.Append(" AND pmst.PROD_NO = @PROD_NO ");
                    cmdInfo.DBParametersInfo.Add("@PROD_ID", pObj.prod_no);
                }

                if (pObj.isPacking == "Y")
                {
                    sb.Append(" AND dtl.IS_PACKING = @IS_PACKING ");
                    cmdInfo.DBParametersInfo.Add("@IS_PACKING", pObj.isPacking);
                }
                else
                {
                    sb.Append(" AND dtl.IS_PACKING IS NULL ");

                }

                if (!string.IsNullOrEmpty(pObj.Shift_Id) && pObj.Shift_Id!="0")
                {
                    sb.Append(" AND pmst.SHIFT_ID = @P_SHIFT_ID ");
                    cmdInfo.DBParametersInfo.Add("@P_SHIFT_ID", pObj.Shift_Id);
                }


                //if (pObj.issmallparts == "Y")
                //{
                //    sb.Append(" AND pmst.ISSMALLPARTS = @ISSMALLPARTS ");
                //    cmdInfo.DBParametersInfo.Add("@ISSMALLPARTS", pObj.issmallparts);
                //}
                //else
                //{
                //    sb.Append(" AND pmst.ISSMALLPARTS IS NULL ");

                //}

                if (pObj.autho_status != "" && pObj.autho_status != null)
                {
                    sb.Append(" AND pmst.AUTH_STATUS = @AUTH_STATUS ");
                    cmdInfo.DBParametersInfo.Add("@AUTH_STATUS", pObj.autho_status);
                }

                if (pObj.ProcessType != "" && pObj.ProcessType != null)
                {
                    sb.Append(" AND dtl.PROCESSTYPE = @PROCESSTYPE ");
                    cmdInfo.DBParametersInfo.Add("@PROCESSTYPE", pObj.ProcessType);
                }
                
                if(pObj.Chargetype!=string.Empty)
                {
                    sb.Append(" AND inv.ITEM_NAME LIKE '%" + pObj.Chargetype + "' ");
                    
                }

                if (pObj.MachineId >0)
                {
                    sb.Append(" AND dtl.MACHINE_ID = @pMachineId ");
                    cmdInfo.DBParametersInfo.Add("@pMachineId", pObj.MachineId);

                }


                if (pObj.Bat_cat_ID != string.Empty)
                {
                    sb.Append(" AND inv.BATTERY_CAT_ID = @pBat_cat_ID ");
                    cmdInfo.DBParametersInfo.Add("@pBat_cat_ID", pObj.Bat_cat_ID);

                }

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<rcProduction>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        //Excel Export Production Report
        public static Byte[] Get_Department_Production_Finished_ExcelData(clsPrmInventory prmINV, bool pExecuteSP)
        {
            return Get_Department_Production_Finished_ExcelData(prmINV, pExecuteSP, null);
        }

        public static Byte[] Get_Department_Production_Finished_ExcelData(clsPrmInventory pObj, bool pExecuteSP, DBContext dc)
        {

            bool isDCInit = false;

            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

            DBCommandInfo cmdInfo = new DBCommandInfo();
            StringBuilder sb = new StringBuilder();
            cmdInfo.DBParametersInfo.Clear();






            DBQuery dbq = new DBQuery();



            DBCommandInfo cmdInfotemp = new DBCommandInfo();
            sb.Length = 0;
            sb.Append(" SELECT   ");
            sb.Append(" pmst.PROD_ID  ");
            sb.Append(" ,pmst.PROD_NO   ");
            sb.Append(" ,CONCAT( CONCAT( pmst.SUPERVISOR_ID,': ') ,supper.FULL_NAME) SUPERVISOR_NAME  ");
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
            sb.Append(" ,inv.ITEM_NAME,inv.ITEM_CODE  ");
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
            sb.Append(" ,(CASE WHEN dtl.IS_UNFORMED='Y' THEN dtl.ITEM_QTY ELSE 0 END)UF_REUSE_QTY ");
            sb.Append(" ,dtl.UNFORMED_QTY  ");
            sb.Append(" ,dtl.REJECT_QTY  ");
            sb.Append(" ,dtl.AMPERE  ");
            sb.Append(" ,dtl.CYCLETIME  ");
            sb.Append(" ,dtl.SULFURIC_GRAVITY  ");
            sb.Append(" ,dtl.TEMPARATURE  ");
            
            sb.Append(" ,dtl.PASTING_BATCH  ");
            sb.Append(" ,us.FULLNAME CREATE_BY ");
            sb.Append(" ,to_char(pmst.ENTRY_DATE, 'mm/dd/yyyy hh24:mi:ss') ENTRY_DATE ");
            sb.Append(" , shift.SHIFT_MSTID");
            sb.Append(",inv.ITEM_STANDARD_WEIGHT_KG");
            sb.Append(",(dtl.ITEM_QTY/ dtl.PANEL_PC)  PANEL_QTY, stm.NAME STLM_NAME");

            sb.Append(" FROM   ");
            sb.Append(" PRODUCTION_MST pmst  ");
            sb.Append(" INNER JOIN  PRODUCTION_DTL dtl ON pmst.PROD_ID=dtl.PROD_MST_ID  ");
            sb.Append(" INNER JOIN INV_ITEM_MASTER inv ON dtl.ITEM_ID=inv.ITEM_ID  ");
            sb.Append(" INNER JOIN UOM_INFO itmUOM  ON dtl.UOM_ID=itmUOM.UOM_ID  ");
            sb.Append(" INNER JOIN DEPARTMENT_INFO dept ON pmst.DEPT_ID=dept.DEPARTMENT_ID  ");
            sb.Append(" LEFT JOIN SUPPERVISOR_MST supper ON supper.EMP_ID=pmst.SUPERVISOR_ID  and pmst.DEPT_ID=supper.DEPT_ID ");
            sb.Append(" INNER JOIN SHIFT_MST shift ON pmst.SHIFT_ID=shift.SHIFT_ID  ");
            sb.Append(" LEFT JOIN MACHINE_MST mac ON dtl.MACHINE_ID=mac.MACHINE_ID  ");
            sb.Append(" LEFT JOIN PROD_RM_FORECAST_MST fmst ON pmst.FORECUST_ID= fmst.RM_FC_ID  ");
            sb.Append(" LEFT JOIN UOM_INFO barU ON dtl.BAR_TYPE=barU.UOM_ID  ");
            sb.Append(" LEFT JOIN UOM_INFO panel on dtl.PANEL_UOM_ID=panel.UOM_ID  ");
            sb.Append(" LEFT JOIN SUPPERVISOR_MST OpMst On dtl.OPERATOR_ID=OpMst.EMP_ID and pmst.DEPT_ID=OpMst.DEPT_ID   ");
            sb.Append(" LEFT JOIN BOM_MST_T bom ON dtl.BOM_ID=bom.BOM_ID  ");
            sb.Append(" LEFT JOIN UOM_INFO weightUOM ON dtl.WEIGHT_UOM_ID=weightUOM.UOM_ID  ");
            sb.Append(" LEFT JOIN UOM_INFO bar ON dtl.BAR_TYPE=bar.UOM_ID  ");
            sb.Append(" LEFT JOIN TBLUSER us  ON us.USERID=pmst.ENTRY_BY_ID  ");
            sb.Append(" LEFT JOIN STORAGE_LOCATION_MST stm ON pmst.STLM_ID=stm.STLM_ID ");
            sb.Append(" WHERE 1=1 ");
            sb.Append(" AND pmst.PROD_NO NOT LIKE 'CHE%' AND pmst.PROD_NO NOT LIKE 'MCH%' ");

            if (pObj.productiondate.ToString() != "")
            {
                sb.Append("  AND TO_DATE(mst.PRODUCTION_DATE) = TO_DATE(@PRODUCTION_DATE)  ");
                cmdInfo.DBParametersInfo.Add("@PRODUCTION_DATE", pObj.productiondate);
            }
            if (pObj.From_Dept_Id > 0)
            {
                sb.Append(" AND pmst.DEPT_ID = @DEPT_ID ");
                cmdInfo.DBParametersInfo.Add("@DEPT_ID", pObj.From_Dept_Id);
            }
            
            if (pObj.StorageLocationId > 0)
            {
                sb.Append(" AND pmst.STLM_ID= @StorageLocationId ");
                cmdInfo.DBParametersInfo.Add("@StorageLocationId", pObj.StorageLocationId);
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

            if (pObj.isElectrolyte == "Y")
            {
                sb.Append(" AND pmst.IS_ELECTROLYTE = @IS_ELECTROLYTE ");
                cmdInfo.DBParametersInfo.Add("@IS_ELECTROLYTE", pObj.isElectrolyte);
            }

            if (pObj.item_id > 0)
            {
                sb.Append(" AND dtl.ITEM_ID = @P_ITEM_ID ");
                cmdInfo.DBParametersInfo.Add("@P_ITEM_ID", pObj.item_id);
            }

            if (pObj.itemGroup_id > 0)
            {
                sb.Append(" AND inv.ITEM_GROUP_ID = @P_ITEM_GROUP_ID ");
                cmdInfo.DBParametersInfo.Add("@P_ITEM_GROUP_ID", pObj.itemGroup_id);
            }

            if (pObj.prod_no != "")
            {
                sb.Append(" AND pmst.PROD_NO = @PROD_NO ");
                cmdInfo.DBParametersInfo.Add("@PROD_ID", pObj.prod_no);
            }

            if (pObj.isPacking == "Y")
            {
                sb.Append(" AND dtl.IS_PACKING = @IS_PACKING ");
                cmdInfo.DBParametersInfo.Add("@IS_PACKING", pObj.isPacking);
            }
            else
            {
                sb.Append(" AND dtl.IS_PACKING IS NULL ");

            }

            if (!string.IsNullOrEmpty(pObj.Shift_Id) && pObj.Shift_Id != "0")
            {
                sb.Append(" AND pmst.SHIFT_ID = @P_SHIFT_ID ");
                cmdInfo.DBParametersInfo.Add("@P_SHIFT_ID", pObj.Shift_Id);
            }

            if (pObj.autho_status != "" && pObj.autho_status != null)
            {
                sb.Append(" AND pmst.AUTH_STATUS = @AUTH_STATUS ");
                cmdInfo.DBParametersInfo.Add("@AUTH_STATUS", pObj.autho_status);
            }

            if (pObj.ProcessType != "" && pObj.ProcessType != null)
            {
                sb.Append(" AND dtl.PROCESSTYPE = @PROCESSTYPE ");
                cmdInfo.DBParametersInfo.Add("@PROCESSTYPE", pObj.ProcessType);
            }

            if (pObj.Chargetype != string.Empty)
            {
                sb.Append(" AND inv.ITEM_NAME LIKE '%" + pObj.Chargetype + "' ");

            }

            if (pObj.MachineId > 0)
            {
                sb.Append(" AND dtl.MACHINE_ID = @pMachineId ");
                cmdInfo.DBParametersInfo.Add("@pMachineId", pObj.MachineId);

            }


            if (pObj.Bat_cat_ID != string.Empty)
            {
                sb.Append(" AND inv.BATTERY_CAT_ID = @pBat_cat_ID ");
                cmdInfo.DBParametersInfo.Add("@pBat_cat_ID", pObj.Bat_cat_ID);

            }

            sb.Append(" ORDER BY dept.DEPARTMENT_NAME,stm.NAME   ");

            DBQuery dbqtemp = new DBQuery();

            dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
            cmdInfo.CommandTimeout = 600;

            cmdInfo.CommandText = sb.ToString();
            cmdInfo.CommandType = CommandType.Text;
            dbq.DBCommandInfo = cmdInfo;
            DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);

            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }


            Byte[] bytes;
            int colNum = 0;
            using (ExcelPackage excel = new ExcelPackage())
            {
                var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
                workSheet.TabColor = System.Drawing.Color.Black;
                workSheet.DefaultRowHeight = 12;

                //Header of table  

                workSheet.Row(1).Height = 20;
                workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Row(1).Style.Font.Bold = true;

                colNum = 1;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "SL No";

                colNum = 2;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Department Name";

                colNum = 3;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Storage Loction";

                colNum = 4;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Supervisor";
                

                colNum = 5;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Production No";

               

                colNum = 6;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Production Date";

                colNum = 7;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Batch";

                colNum = 8;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Shift";

                

                colNum = 9;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Machine Name";


                colNum = 10;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Item Code";

                colNum = 11;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Item Name";

                colNum = 12;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Uom";
                colNum = 13;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Production Quantity";
                colNum = 14;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Entry By";
                colNum = 15;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Entry Date";
                
                

                

                //Body of table  

                int recordIndex = 2;
                int slno = 0;
                foreach (DataRow dRow in dtData.Rows)
                {
                    slno++;
                    workSheet.Cells[recordIndex, 1].Value = slno;
                   
                    workSheet.Cells[recordIndex, 2].Value = dRow["DEPARTMENT_NAME"].ToString();
                    workSheet.Cells[recordIndex, 3].Value = dRow["STLM_NAME"].ToString();
                    workSheet.Cells[recordIndex, 4].Value = dRow["SUPERVISOR_NAME"].ToString();
                    workSheet.Cells[recordIndex, 5].Value = dRow["PROD_NO"].ToString();
                    workSheet.Cells[recordIndex, 6].Value = Convert.ToDateTime(dRow["PRODUCTION_DATE"]).ToString("dd-MMM-yyyy");
                    workSheet.Cells[recordIndex, 7].Value = dRow["PROD_BATCH_NO"].ToString();
                    workSheet.Cells[recordIndex, 8].Value = dRow["SHIFT_NAME"].ToString();
                    workSheet.Cells[recordIndex, 9].Value = dRow["MACHINE_NAME"].ToString();
                    workSheet.Cells[recordIndex, 10].Value = dRow["ITEM_CODE"].ToString();
                    workSheet.Cells[recordIndex, 11].Value = dRow["ITEM_NAME"].ToString();
                    workSheet.Cells[recordIndex, 12].Value = dRow["UOM_NAME"].ToString();
                    workSheet.Cells[recordIndex, 13].Value =Conversion.DBNullDecimalToZero(dRow["ITEM_QTY"].ToString());
                    workSheet.Cells[recordIndex, 14].Value = dRow["CREATE_BY"].ToString();
                    workSheet.Cells[recordIndex, 15].Value = dRow["ENTRY_DATE"].ToString();
                    



                    recordIndex++;
                }
                workSheet.Column(1).AutoFit();
                workSheet.Column(2).AutoFit();
                workSheet.Column(3).AutoFit();
                workSheet.Column(4).AutoFit();
                workSheet.Column(5).AutoFit();
                workSheet.Column(6).AutoFit();
                workSheet.Column(7).AutoFit();
                workSheet.Column(8).AutoFit();
                workSheet.Column(9).AutoFit();
                workSheet.Column(10).AutoFit();
                workSheet.Column(11).AutoFit();
                workSheet.Column(12).AutoFit();
                workSheet.Column(13).AutoFit();
                workSheet.Column(14).AutoFit();
                workSheet.Column(15).AutoFit();
                


                bytes = excel.GetAsByteArray();

            }

            return bytes;

        }


        public static Byte[] Get_Department_Production_RM_ExcelData(clsPrmInventory pObj, bool pExecuteSP, DBContext dc)
        {

            bool isDCInit = false;

            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

            DBCommandInfo cmdInfo = new DBCommandInfo();
            StringBuilder sb = new StringBuilder();
            cmdInfo.DBParametersInfo.Clear();






            DBQuery dbq = new DBQuery();



            DBCommandInfo cmdInfotemp = new DBCommandInfo();
            sb.Length = 0;
            sb.Append(" SELECT ");
            sb.Append(" pmst.PROD_ID,pmst.PROD_NO,pmst.SUPERVISOR_ID,pmst.DEPT_ID,pmst.REF_NO_MANUAL ");
            sb.Append(" ,pmst.SHIFT_ID SHIFT_NAME,pmst.BATCH_STARTTIME ,pmst.BATCH_ENDTIME  ,pmst.STARTTIME ,pmst.ENDTIME  ");
            sb.Append(" ,pmst.PROCESS_CODE,pmst.PRODUCTION_DATE ");
            sb.Append(" ,pmst.REJECTED_QTY,pmst.BATCH_ID,pmst.PROD_BATCH_NO,dept.DEPARTMENT_NAME ");
            sb.Append(" ,inv.ITEM_NAME,inv.ITEM_ID,inv.ITEM_CODE  ");
            sb.Append(" ,fc.ISSUE_STOCK ITEM_QTY ");
            sb.Append(" ,itmUOM.UOM_CODE_SHORT UOM_NAME  ");
            sb.Append(" ,us.FULLNAME CREATE_BY ");
            sb.Append(" ,to_char(pmst.ENTRY_DATE, 'mm/dd/yyyy hh24:mi:ss')  ENTRY_DATE ");
            sb.Append(" ,shift.SHIFT_MSTID    ");
            sb.Append(" ,CONCAT( CONCAT( pmst.SUPERVISOR_ID,': ') ,supper.FULL_NAME) SUPERVISOR_NAME,stm.NAME STLM_NAME  ");
            sb.Append(" FROM ");
            sb.Append(" PRODUCTION_FLOOR_CLOSING fc  ");
            sb.Append(" INNER JOIN PRODUCTION_MST pmst ON fc.PROD_MST_ID=pmst.PROD_ID ");
            sb.Append(" INNER JOIN INV_ITEM_MASTER inv ON fc.CLOSING_ITEM_ID=inv.ITEM_ID  ");
            sb.Append(" INNER JOIN UOM_INFO itmUOM  ON fc.CLOSING_UOM_ID=itmUOM.UOM_ID  ");
            sb.Append(" LEFT JOIN SUPPERVISOR_MST supper ON supper.EMP_ID=pmst.SUPERVISOR_ID  AND pmst.DEPT_ID= supper.DEPT_ID ");
            sb.Append(" INNER JOIN DEPARTMENT_INFO dept ON pmst.DEPT_ID=dept.DEPARTMENT_ID  ");
            sb.Append(" INNER JOIN SHIFT_MST shift ON pmst.SHIFT_ID=shift.SHIFT_ID         ");
            sb.Append(" LEFT JOIN TBLUSER us  ON us.USERID=pmst.ENTRY_BY_ID  ");
            sb.Append(" LEFT JOIN STORAGE_LOCATION_MST stm ON pmst.STLM_ID=stm.STLM_ID ");
            sb.Append(" WHERE 1=1  ");
           
            if (pObj.From_Dept_Id > 0)
            {
                sb.Append(" AND pmst.DEPT_ID = @DEPT_ID ");
                cmdInfo.DBParametersInfo.Add("@DEPT_ID", pObj.From_Dept_Id);
            }

            if (pObj.StorageLocationId > 0)
            {
                sb.Append(" AND pmst.STLM_ID = @StorageLocationId ");
                cmdInfo.DBParametersInfo.Add("@StorageLocationId", pObj.StorageLocationId);
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

            if (pObj.usedItemId > 0)
            {
                sb.Append(" AND fc.CLOSING_ITEM_ID = @P_CLOSING_ITEM_ID ");
                cmdInfo.DBParametersInfo.Add("@P_CLOSING_ITEM_ID", pObj.usedItemId);
            }

            if (pObj.itemGroup_id > 0)
            {
                sb.Append(" AND inv.ITEM_GROUP_ID = @P_ITEM_GROUP_ID ");
                cmdInfo.DBParametersInfo.Add("@P_ITEM_GROUP_ID", pObj.itemGroup_id);
            }

            if (pObj.prod_no != "")
            {
                sb.Append(" AND pmst.PROD_NO = @PROD_NO ");
                cmdInfo.DBParametersInfo.Add("@PROD_ID", pObj.prod_no);
            }


            if (!string.IsNullOrEmpty(pObj.Shift_Id) && pObj.Shift_Id != "0")
            {
                sb.Append(" AND pmst.SHIFT_ID = @P_SHIFT_ID ");
                cmdInfo.DBParametersInfo.Add("@P_SHIFT_ID", pObj.Shift_Id);
            }
            if (pObj.MachineId > 0)
            {
                sb.Append(" AND fc.MACHINE_ID = @pMachineId ");
                cmdInfo.DBParametersInfo.Add("@pMachineId", pObj.MachineId);

            }


            sb.Append(" ORDER BY dept.DEPARTMENT_NAME,stm.NAME   ");

            DBQuery dbqtemp = new DBQuery();

            dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
            cmdInfo.CommandTimeout = 600;

            cmdInfo.CommandText = sb.ToString();
            cmdInfo.CommandType = CommandType.Text;
            dbq.DBCommandInfo = cmdInfo;
            DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);

            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }


            Byte[] bytes;
            int colNum = 0;
            using (ExcelPackage excel = new ExcelPackage())
            {
                var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
                workSheet.TabColor = System.Drawing.Color.Black;
                workSheet.DefaultRowHeight = 12;

                //Header of table  

                workSheet.Row(1).Height = 20;
                workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Row(1).Style.Font.Bold = true;

                colNum = 1;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "SL No";

                colNum = 2;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Department Name";

                colNum = 3;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Storage Loction";

                colNum = 4;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Supervisor";


                colNum = 5;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Production No";



                colNum = 6;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Production Date";

                colNum = 7;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Batch";

                colNum = 8;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Shift";



                //colNum = 9;
                //workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                //workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                //workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                //workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                //workSheet.Cells[1, colNum].Value = "Machine Name";


                colNum = 9;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Item Code";

                colNum = 10;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Item Name";

                colNum = 11;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Uom";

                colNum = 12;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Used Quantity";

                colNum = 13;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Entry By";

                colNum = 14;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Entry Date";





                //Body of table  

                int recordIndex = 2;
                int slno = 0;
                foreach (DataRow dRow in dtData.Rows)
                {
                    slno++;
                    workSheet.Cells[recordIndex, 1].Value = slno;

                    workSheet.Cells[recordIndex, 2].Value = dRow["DEPARTMENT_NAME"].ToString();
                    workSheet.Cells[recordIndex, 3].Value = dRow["STLM_NAME"].ToString();
                    workSheet.Cells[recordIndex, 4].Value = dRow["SUPERVISOR_NAME"].ToString();
                    workSheet.Cells[recordIndex, 5].Value = dRow["PROD_NO"].ToString();
                    workSheet.Cells[recordIndex, 6].Value =Convert.ToDateTime(dRow["PRODUCTION_DATE"]).ToString("dd-MMM-yyyy");
                    workSheet.Cells[recordIndex, 7].Value = dRow["PROD_BATCH_NO"].ToString();
                    workSheet.Cells[recordIndex, 8].Value = dRow["SHIFT_NAME"].ToString();
                    //workSheet.Cells[recordIndex, 9].Value = dRow["MACHINE_NAME"].ToString();
                    workSheet.Cells[recordIndex, 9].Value = dRow["ITEM_CODE"].ToString();
                    workSheet.Cells[recordIndex, 10].Value = dRow["ITEM_NAME"].ToString();
                    workSheet.Cells[recordIndex, 11].Value = dRow["UOM_NAME"].ToString();
                    workSheet.Cells[recordIndex, 12].Value = Conversion.DBNullDecimalToZero(dRow["ITEM_QTY"].ToString());
                    workSheet.Cells[recordIndex, 13].Value = dRow["CREATE_BY"].ToString();
                    workSheet.Cells[recordIndex, 14].Value = dRow["ENTRY_DATE"].ToString();




                    recordIndex++;
                }
                workSheet.Column(1).AutoFit();
                workSheet.Column(2).AutoFit();
                workSheet.Column(3).AutoFit();
                workSheet.Column(4).AutoFit();
                workSheet.Column(5).AutoFit();
                workSheet.Column(6).AutoFit();
                workSheet.Column(7).AutoFit();
                workSheet.Column(8).AutoFit();
                workSheet.Column(9).AutoFit();
                workSheet.Column(10).AutoFit();
                workSheet.Column(11).AutoFit();
                workSheet.Column(12).AutoFit();
                workSheet.Column(13).AutoFit();
                workSheet.Column(14).AutoFit();



                bytes = excel.GetAsByteArray();

            }

            return bytes;

        }

        //End Export
        public static List<rcProduction> Oxide_Department_Production_Report(clsPrmInventory pObj, DBContext dc)
        {
            List<rcProduction> cObjList = new List<rcProduction>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetProductionDtlListString(pObj));
                if (pObj.productiondate.ToString() != "")
                {
                    sb.Append("  AND TO_DATE(mst.PRODUCTION_DATE) = TO_DATE(@PRODUCTION_DATE)  ");
                    cmdInfo.DBParametersInfo.Add("@PRODUCTION_DATE", pObj.productiondate);
                }
                if (pObj.From_Dept_Id > 0)
                {
                    sb.Append(" AND pmst.DEPT_ID = @DEPT_ID ");
                    cmdInfo.DBParametersInfo.Add("@DEPT_ID", pObj.From_Dept_Id);
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

                if (pObj.item_id > 0)
                {
                    sb.Append(" AND dtl.ITEM_ID = @P_ITEM_ID ");
                    cmdInfo.DBParametersInfo.Add("@P_ITEM_ID", pObj.item_id);
                }

                if (pObj.itemGroup_id > 0)
                {
                    sb.Append(" AND inv.ITEM_GROUP_ID = @P_ITEM_GROUP_ID ");
                    cmdInfo.DBParametersInfo.Add("@P_ITEM_GROUP_ID", pObj.itemGroup_id);
                }

                if (pObj.prod_no != "")
                {
                    sb.Append(" AND pmst.PROD_NO = @PROD_NO ");
                    cmdInfo.DBParametersInfo.Add("@PROD_ID", pObj.prod_no);
                }

                if (pObj.isPacking == "Y")
                {
                    sb.Append(" AND dtl.IS_PACKING = @IS_PACKING ");
                    cmdInfo.DBParametersInfo.Add("@IS_PACKING", pObj.isPacking);
                }
                else
                {
                    sb.Append(" AND dtl.IS_PACKING IS NULL ");

                }

                if (!string.IsNullOrEmpty(pObj.Shift_Id) && pObj.Shift_Id != "0")
                {
                    sb.Append(" AND pmst.SHIFT_ID = @P_SHIFT_ID ");
                    cmdInfo.DBParametersInfo.Add("@P_SHIFT_ID", pObj.Shift_Id);
                }


                //if (pObj.issmallparts == "Y")
                //{
                //    sb.Append(" AND pmst.ISSMALLPARTS = @ISSMALLPARTS ");
                //    cmdInfo.DBParametersInfo.Add("@ISSMALLPARTS", pObj.issmallparts);
                //}
                //else
                //{
                //    sb.Append(" AND pmst.ISSMALLPARTS IS NULL ");

                //}

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<rcProduction>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static List<rcProduction> Department_Used_Material_Report(clsPrmInventory rptClass)
        {
            return Department_Used_Material_Report(rptClass, null);
        }
        public static List<rcProduction> Department_Used_Material_Report(clsPrmInventory pObj, DBContext dc)
        {
            List<rcProduction> cObjList = new List<rcProduction>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(Get_Used_Raw_Material_SqlString());

                //if (pObj.productiondate.ToString() != "")
                //{
                //    sb.Append("  AND TO_DATE(mst.PRODUCTION_DATE) = TO_DATE(@PRODUCTION_DATE)  ");
                //    cmdInfo.DBParametersInfo.Add("@PRODUCTION_DATE", pObj.productiondate);
                //}
                if (pObj.From_Dept_Id > 0)
                {
                    sb.Append(" AND pmst.DEPT_ID = @DEPT_ID ");
                    cmdInfo.DBParametersInfo.Add("@DEPT_ID", pObj.From_Dept_Id);
                }

                if (pObj.StorageLocationId > 0)
                {
                    sb.Append(" AND pmst.STLM_ID = @StorageLocationId ");
                    cmdInfo.DBParametersInfo.Add("@StorageLocationId", pObj.StorageLocationId);
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

                //if (pObj.item_id > 0)
                //{
                //    sb.Append(" AND fc.ITEM_ID = @P_ITEM_ID ");
                //    cmdInfo.DBParametersInfo.Add("@P_ITEM_ID", pObj.item_id);
                //}

                if (pObj.usedItemId > 0)
                {
                    sb.Append(" AND fc.CLOSING_ITEM_ID = @P_CLOSING_ITEM_ID ");
                    cmdInfo.DBParametersInfo.Add("@P_CLOSING_ITEM_ID", pObj.usedItemId);
                }

                if (pObj.itemGroup_id > 0)
                {
                    sb.Append(" AND inv.ITEM_GROUP_ID = @P_ITEM_GROUP_ID ");
                    cmdInfo.DBParametersInfo.Add("@P_ITEM_GROUP_ID", pObj.itemGroup_id);
                }

                if (pObj.prod_no != "")
                {
                    sb.Append(" AND pmst.PROD_NO = @PROD_NO ");
                    cmdInfo.DBParametersInfo.Add("@PROD_ID", pObj.prod_no);
                }

          
                if (!string.IsNullOrEmpty(pObj.Shift_Id) && pObj.Shift_Id != "0")
                {
                    sb.Append(" AND pmst.SHIFT_ID = @P_SHIFT_ID ");
                    cmdInfo.DBParametersInfo.Add("@P_SHIFT_ID", pObj.Shift_Id);
                }
                if (pObj.MachineId > 0)
                {
                    sb.Append(" AND fc.MACHINE_ID = @pMachineId ");
                    cmdInfo.DBParametersInfo.Add("@pMachineId", pObj.MachineId);

                }


                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<rcProduction>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }


        public static List<rcProduction> IB_Used_RM_Report(clsPrmInventory pObj, DBContext dc)
        {
            List<rcProduction> cObjList = new List<rcProduction>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(Get_IB_Used_RM_queryString());
                if (pObj.productiondate.ToString() != "")
                {
                    sb.Append("  AND TO_DATE(mst.PRODUCTION_DATE) = TO_DATE(@PRODUCTION_DATE)  ");
                    cmdInfo.DBParametersInfo.Add("@PRODUCTION_DATE", pObj.productiondate);
                }
                if (pObj.From_Dept_Id > 0)
                {
                    sb.Append(" AND pmst.DEPT_ID = @DEPT_ID ");
                    cmdInfo.DBParametersInfo.Add("@DEPT_ID", pObj.From_Dept_Id);
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

                if (pObj.item_id > 0)
                {
                    sb.Append(" AND dtl.ITEM_ID = @P_ITEM_ID ");
                    cmdInfo.DBParametersInfo.Add("@P_ITEM_ID", pObj.item_id);
                }

                if (pObj.itemGroup_id > 0)
                {
                    sb.Append(" AND inv.ITEM_GROUP_ID = @P_ITEM_GROUP_ID ");
                    cmdInfo.DBParametersInfo.Add("@P_ITEM_GROUP_ID", pObj.itemGroup_id);
                }

                if (pObj.prod_no != "")
                {
                    sb.Append(" AND pmst.PROD_NO = @PROD_NO ");
                    cmdInfo.DBParametersInfo.Add("@PROD_ID", pObj.prod_no);
                }

                if (pObj.isPacking == "Y")
                {
                    sb.Append(" AND dtl.IS_PACKING = @IS_PACKING ");
                    cmdInfo.DBParametersInfo.Add("@IS_PACKING", pObj.isPacking);
                }
                else
                {
                    sb.Append(" AND dtl.IS_PACKING IS NULL ");

                }

                if (!string.IsNullOrEmpty(pObj.Shift_Id) && pObj.Shift_Id != "0")
                {
                    sb.Append(" AND pmst.SHIFT_ID = @P_SHIFT_ID ");
                    cmdInfo.DBParametersInfo.Add("@P_SHIFT_ID", pObj.Shift_Id);
                }


                if (pObj.issmallparts == "Y")
                {
                    sb.Append(" AND pmst.ISSMALLPARTS = @ISSMALLPARTS ");
                    cmdInfo.DBParametersInfo.Add("@ISSMALLPARTS", pObj.issmallparts);
                }
                else
                {
                    sb.Append(" AND pmst.ISSMALLPARTS IS NULL ");

                }

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<rcProduction>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        /// <summary>
        /// In this section oxide department all production related report will be included
        /// </summary>
        /// <param name="rptClass"></param>
        /// <returns></returns>

        #region Oxide_Department_Production_Related_Report


        public static string Get_Gray_DailyProduction_SqlString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" Select mm.MACHINE_NAME ");
            sb.Append(" ,(Select NVL(SUM(ITEM_QTY),0) from PRODUCTION_DTL prdtl INNER JOIN PRODUCTION_MST prdMst on prdtl.PROD_MST_ID =prdMst.PROD_ID where MACHINE_ID=mm.MACHINE_ID and prdMst.SHIFT_ID='M' and prdMst.SHIFT_ID='M' and prdMst.PRODUCTION_DATE BETWEEN @P_M_FROM_DATE AND @P_M_TO_DATE) as MORNING_SHIFT_QTY ");
            sb.Append(" ,(Select NVL(SUM(ITEM_QTY),0) from PRODUCTION_DTL prdtl INNER JOIN PRODUCTION_MST prdMst on prdtl.PROD_MST_ID =prdMst.PROD_ID where MACHINE_ID=mm.MACHINE_ID and prdMst.SHIFT_ID='E' and prdMst.PRODUCTION_DATE BETWEEN @P_E_FROM_DATE AND @P_E_TO_DATE) as EVENING_SHIFT_QTY ");
            sb.Append(" ,(Select NVL(SUM(ITEM_QTY),0) from PRODUCTION_DTL prdtl INNER JOIN PRODUCTION_MST prdMst on prdtl.PROD_MST_ID =prdMst.PROD_ID where MACHINE_ID=mm.MACHINE_ID and prdMst.SHIFT_ID='N' and prdMst.PRODUCTION_DATE BETWEEN @P_N_FROM_DATE AND @P_N_TO_DATE) as NIGHT_SHIFT_QTY ");
            sb.Append(" from ");
            sb.Append(" MACHINE_MST mm ");
            sb.Append(" where 1=1 ");
            return sb.ToString();
        }

        public static string Get_Red_Oxide_DailyProduction_SqlString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" Select mm.MACHINE_NAME ");
            sb.Append(" ,(Select NVL(SUM(ITEM_QTY),0) from PRODUCTION_DTL prdtl INNER JOIN PRODUCTION_MST prdMst on prdtl.PROD_MST_ID =prdMst.PROD_ID where MACHINE_ID=mm.MACHINE_ID and prdMst.PRODUCTION_DATE BETWEEN @P_FROM_DATE AND @P_TO_DATE) as MORNING_SHIFT_QTY ");          
            sb.Append(" from ");
            sb.Append(" MACHINE_MST mm ");
            sb.Append(" where 1=1 ");
            return sb.ToString();
        }

        public static List<rcProduction> Gray_Oxide_Daily_Production_Report(clsPrmInventory rptClass)
        {
            return Gray_Oxide_Daily_Production_Report(rptClass, null);
        }
        public static List<rcProduction> Gray_Oxide_Daily_Production_Report(clsPrmInventory pObj, DBContext dc)
        {
            List<rcProduction> cObjList = new List<rcProduction>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(Get_Gray_DailyProduction_SqlString());

                if (!string.IsNullOrEmpty(pObj.fromProdDate) && !string.IsNullOrEmpty(pObj.toProdDate))
                {
                    cmdInfo.DBParametersInfo.Add("@P_M_FROM_DATE", pObj.fromProdDate);
                    cmdInfo.DBParametersInfo.Add("@P_M_TO_DATE", pObj.toProdDate);
                }
                if (!string.IsNullOrEmpty(pObj.fromProdDate) && !string.IsNullOrEmpty(pObj.toProdDate))
                {
                    cmdInfo.DBParametersInfo.Add("@P_E_FROM_DATE", pObj.fromProdDate);
                    cmdInfo.DBParametersInfo.Add("@P_E_TO_DATE", pObj.toProdDate);
                }
                if (!string.IsNullOrEmpty(pObj.fromProdDate) && !string.IsNullOrEmpty(pObj.toProdDate))
                {
                    cmdInfo.DBParametersInfo.Add("@P_N_FROM_DATE", pObj.fromProdDate);
                    cmdInfo.DBParametersInfo.Add("@P_N_TO_DATE", pObj.toProdDate);
                }

                if (pObj.DeptID > 0)
                {
                    sb.Append(" AND mm.DEPT_ID = @P_DEPT_ID ");
                    cmdInfo.DBParametersInfo.Add("@P_DEPT_ID", pObj.DeptID);
                }

                sb.Append(" ORDER BY mm.ORDER_SI");
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<rcProduction>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }



        public static List<rcProduction> Oxide_Item_Closing_Report(clsPrmInventory rptClass)
        {
            return Oxide_Item_Closing_Report(rptClass, null);
        }
        public static List<rcProduction> Oxide_Item_Closing_Report(clsPrmInventory pObj, DBContext dc)
        {
            List<rcProduction> cObjList = new List<rcProduction>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();
                sb.Append(" Select INV_ITEM_MASTER.* ");             
                sb.Append(" ,GET_DEPT_WISE_CLOSING_QTY(INV_ITEM_MASTER.ITEM_ID,dept.DEPT_ID) CLOSING_QTY ");
                sb.Append(" FROM INV_ITEM_MASTER ");               
                sb.Append(" LEFT JOIN PRO_DEPARTMENT_ITEM dept ON INV_ITEM_MASTER.ITEM_ID = dept.ITEM_ID ");
                sb.Append(" Where 1=1 ");
                //sb.Append(" AND  INV_ITEM_MASTER.ITEM_ID IN(4923,4480,4847,4479)");
                sb.Append(" AND dept.DEPT_ID IN (2,137) AND dept.IS_FINISHED='Y' ");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<rcProduction>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }


        public static List<rcProduction> Red_Oxide_Daily_Production_Report(clsPrmInventory rptClass)
        {
            return Red_Oxide_Daily_Production_Report(rptClass, null);
        }
        public static List<rcProduction> Red_Oxide_Daily_Production_Report(clsPrmInventory pObj, DBContext dc)
        {
            List<rcProduction> cObjList = new List<rcProduction>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(Get_Red_Oxide_DailyProduction_SqlString());

                if (!string.IsNullOrEmpty(pObj.fromProdDate) && !string.IsNullOrEmpty(pObj.toProdDate))
                {
                    cmdInfo.DBParametersInfo.Add("@P_FROM_DATE", pObj.fromProdDate);
                    cmdInfo.DBParametersInfo.Add("@P_TO_DATE", pObj.toProdDate);
                }

                if (pObj.DeptID > 0)
                {
                    sb.Append(" AND mm.DEPT_ID = @P_DEPT_ID ");
                    cmdInfo.DBParametersInfo.Add("@P_DEPT_ID", pObj.DeptID);
                }

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<rcProduction>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }




        #endregion


        #region Grid_Casting


        public static List<rcProduction> Grid_Casting_Usuable_Report(clsPrmInventory rptClass)
        {
            return Grid_Casting_Usuable_Report(rptClass, null);
        }
        public static List<rcProduction> Grid_Casting_Usuable_Report(clsPrmInventory pObj, DBContext dc)
        {
            List<rcProduction> cObjList = new List<rcProduction>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(Get_Red_Oxide_DailyProduction_SqlString());

                if (!string.IsNullOrEmpty(pObj.fromProdDate) && !string.IsNullOrEmpty(pObj.toProdDate))
                {
                    cmdInfo.DBParametersInfo.Add("@P_FROM_DATE", pObj.fromProdDate);
                    cmdInfo.DBParametersInfo.Add("@P_TO_DATE", pObj.toProdDate);
                }

                if (pObj.DeptID > 0)
                {
                    sb.Append(" AND mm.DEPT_ID = @P_DEPT_ID ");
                    cmdInfo.DBParametersInfo.Add("@P_DEPT_ID", pObj.DeptID);
                }

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<rcProduction>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        #endregion

        #region Formation-Related Report


        public static List<rcProduction> Formation_Loading_Report(clsPrmInventory rptClass)
        {
            return Formation_Loading_Report(rptClass, null);
        }
        public static List<rcProduction> Formation_Loading_Report(clsPrmInventory pObj, DBContext dc)
        {
            List<rcProduction> cObjList = new List<rcProduction>();
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
                if (pObj.From_Dept_Id > 0)
                {
                    sb.Append(" AND pmst.DEPT_ID = @DEPT_ID ");
                    cmdInfo.DBParametersInfo.Add("@DEPT_ID", pObj.From_Dept_Id);
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

                if (pObj.item_id > 0)
                {
                    sb.Append(" AND dtl.ITEM_ID = @P_ITEM_ID ");
                    cmdInfo.DBParametersInfo.Add("@P_ITEM_ID", pObj.item_id);
                }

                if (pObj.itemGroup_id > 0)
                {
                    sb.Append(" AND inv.ITEM_GROUP_ID = @P_ITEM_GROUP_ID ");
                    cmdInfo.DBParametersInfo.Add("@P_ITEM_GROUP_ID", pObj.itemGroup_id);
                }

                if (pObj.prod_no != "")
                {
                    sb.Append(" AND pmst.PROD_NO = @PROD_NO ");
                    cmdInfo.DBParametersInfo.Add("@PROD_ID", pObj.prod_no);
                }

                if (pObj.isPacking == "Y")
                {
                    sb.Append(" AND dtl.IS_PACKING = @IS_PACKING ");
                    cmdInfo.DBParametersInfo.Add("@IS_PACKING", pObj.isPacking);
                }
                else
                {
                    sb.Append(" AND dtl.IS_PACKING IS NULL ");

                }

                if (!string.IsNullOrEmpty(pObj.Shift_Id) && pObj.Shift_Id != "0")
                {
                    sb.Append(" AND pmst.SHIFT_ID = @P_SHIFT_ID ");
                    cmdInfo.DBParametersInfo.Add("@P_SHIFT_ID", pObj.Shift_Id);
                }


                if (pObj.issmallparts == "Y")
                {
                    sb.Append(" AND pmst.ISSMALLPARTS = @ISSMALLPARTS ");
                    cmdInfo.DBParametersInfo.Add("@ISSMALLPARTS", pObj.issmallparts);
                }
                else
                {
                    sb.Append(" AND pmst.ISSMALLPARTS IS NULL ");

                }
                sb.Append(" AND pmst.IS_UNLOAD ='N' ");
                

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<rcProduction>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }



        public static List<rcProduction> Formation_Un_Loading_Report(clsPrmInventory rptClass)
        {
            return Formation_Un_Loading_Report(rptClass, null);
        }
        public static List<rcProduction> Formation_Un_Loading_Report(clsPrmInventory pObj, DBContext dc)
        {
            List<rcProduction> cObjList = new List<rcProduction>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetProductionDtlListString());
               
                if (pObj.From_Dept_Id > 0)
                {
                    sb.Append(" AND pmst.DEPT_ID = @DEPT_ID ");
                    cmdInfo.DBParametersInfo.Add("@DEPT_ID", pObj.From_Dept_Id);
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

                if (pObj.item_id > 0)
                {
                    sb.Append(" AND dtl.ITEM_ID = @P_ITEM_ID ");
                    cmdInfo.DBParametersInfo.Add("@P_ITEM_ID", pObj.item_id);
                }

                if (pObj.itemGroup_id > 0)
                {
                    sb.Append(" AND inv.ITEM_GROUP_ID = @P_ITEM_GROUP_ID ");
                    cmdInfo.DBParametersInfo.Add("@P_ITEM_GROUP_ID", pObj.itemGroup_id);
                }

                if (pObj.prod_no != "")
                {
                    sb.Append(" AND pmst.PROD_NO = @PROD_NO ");
                    cmdInfo.DBParametersInfo.Add("@PROD_ID", pObj.prod_no);
                }


                if (!string.IsNullOrEmpty(pObj.Shift_Id) && pObj.Shift_Id != "0")
                {
                    sb.Append(" AND pmst.SHIFT_ID = @P_SHIFT_ID ");
                    cmdInfo.DBParametersInfo.Add("@P_SHIFT_ID", pObj.Shift_Id);
                }
                sb.Append(" AND pmst.IS_UNLOAD='Y' ");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<rcProduction>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        #endregion

        public static List<rcProduction> IBProcessTypeWise_Report(clsPrmInventory prmINV)
        {
            return IBProcessTypeWise_Report(prmINV, null);
        }

        public static List<rcProduction> IBProcessTypeWise_Report(clsPrmInventory prmINV, DBContext dc)
        {
            List<rcProduction> cRptList = new List<rcProduction>();
            bool isDCInit = false;
            //try
            {

                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();
                cmdInfo.DBParametersInfo.Clear();


                sb.Length = 0;



                sb.Append("  Select a.PROD_NO,a.SUPERVISOR_ID,sm.FULL_NAME, ");

                sb.Append("  st.SHIFT_NAME SHIFT,st.SHIFT_MSTID, ");
                sb.Append("  di.DEPARTMENT_NAME,a.AUTH_STATUS,a.PRODUCTION_DATE,a.PROD_BATCH_NO,b.ITEM_ID,c.ITEM_NAME,d.UOM_CODE,NVL(b.ITEM_QTY,0) ITEM_QTY ");
                sb.Append("  ,CASE WHEN b.PROCESSTYPE='C' THEN 'Cutting' ");
                sb.Append("  WHEN b.PROCESSTYPE='F' THEN 'Filling' ");
                sb.Append("  WHEN b.PROCESSTYPE='S' THEN 'Sulphation' END PROCESSTYPE ");
                sb.Append("  FROM PRODUCTION_MST a ");
                sb.Append("  INNER JOIN PRODUCTION_DTL b ON a.PROD_ID=b.PROD_MST_ID ");
                sb.Append("  INNER JOIN INV_ITEM_MASTER c ON b.ITEM_ID=c.ITEM_ID ");
                sb.Append("  INNER JOIN UOM_INFO d ON b.UOM_ID=d.UOM_ID ");
                sb.Append("  INNER JOIN DEPARTMENT_INFO di ON a.DEPT_ID=di.DEPARTMENT_ID ");
                sb.Append(" LEFT JOIN SUPPERVISOR_MST sm ON a.SUPERVISOR_ID=sm.EMP_ID AND a.DEPT_ID= sm.DEPT_ID ");
                sb.Append(" LEFT JOIN SHIFT_MST st ON a.SHIFT_ID=st.SHIFT_ID ");
                
                sb.Append("  WHERE 1=1 ");

                if (prmINV.FromDate != null)
                {
                    sb.Append(" AND  a.PRODUCTION_DATE BETWEEN  :FromDate and :ToDate ");
                    cmdInfo.DBParametersInfo.Add(":FromDate", prmINV.FromDate);
                    cmdInfo.DBParametersInfo.Add(":ToDate", prmINV.ToDate);
                }

                if (prmINV.ProcessType != string.Empty)
                {
                    sb.Append("  AND b.PROCESSTYPE=:ProcessType ");
                    cmdInfo.DBParametersInfo.Add(":ProcessType", prmINV.ProcessType);
                }

                if (prmINV.DeptID > 0)
                {
                    sb.Append(" AND a.DEPT_ID=:DeptID");
                    cmdInfo.DBParametersInfo.Add(":DeptID", prmINV.DeptID);
                }
                if (prmINV.item_id > 0)
                {
                    sb.Append(" AND b.ITEM_ID=:item_id");
                    cmdInfo.DBParametersInfo.Add(":item_id", prmINV.item_id);
                }
                //sb.Append(" and b.ITEM_ID=6690 ");

                // sb.Append(" GROUP BY c.ITEM_NAME,UI.UOM_CODE,bi.ITEM_NAME,BU.UOM_CODE,bm.BOM_ITEM_DESC,b.ITEM_STANDARD_WEIGHT_KG,bd.ITEM_ID,b.ITEM_ID,a.DEPT_ID ) ");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);

                foreach (DataRow dRow in dtData.Rows)
                {
                    rcProduction stk = new rcProduction();

                    stk.PROD_NO = dRow["PROD_NO"].ToString();
                    stk.SUPERVISOR_ID = dRow["SUPERVISOR_ID"].ToString();
                    stk.FULL_NAME = dRow["FULL_NAME"].ToString();
                    stk.SHIFT = dRow["SHIFT"].ToString();
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();
                    stk.PRODUCTION_DATE = Conversion.DBNullDateToNull(dRow["PRODUCTION_DATE"]);
                    stk.DEPARTMENT_NAME = dRow["DEPARTMENT_NAME"].ToString();
                    stk.UOM_NAME = dRow["UOM_CODE"].ToString();
                    stk.AUTH_STATUS = dRow["AUTH_STATUS"].ToString();
                    stk.ITEM_QTY = Conversion.DBNullDecimalToZero(dRow["ITEM_QTY"]);
                    stk.PROCESSTYPE = dRow["PROCESSTYPE"].ToString();
                    stk.SHIFT_MSTID = Conversion.DBNullIntToZero(dRow["SHIFT_MSTID"]);

                    cRptList.Add(stk);

                }

            }
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }

            return cRptList;

        }


        //Rejection Report Summary

        //Only Assembly

        public static List<rcProduction> Get_RejectionInfoAssemblySumm_Report(clsPrmInventory prmINV)
        {
            return Get_RejectionInfoAssemblySumm_Report(prmINV, null);
        }

        public static List<rcProduction> Get_RejectionInfoAssemblySumm_Report(clsPrmInventory prmINV, DBContext dc)
        {
            List<rcProduction> cRptList = new List<rcProduction>();
            bool isDCInit = false;
            //try
            {

                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();
                cmdInfo.DBParametersInfo.Clear();


                sb.Length = 0;


                if (prmINV.DeptID == 136 || prmINV.DeptID == 140)
                {
                    //  , a.SHIFT_ID  Conversion.StringToDateORNull(txtPRODUCTION_DATE.Text)
                    sb.Append(" Select e.DEPARTMENT_NAME,c.ITEM_NAME,d.UOM_CODE,SUM(NVL(b.REJECTION_QTY,0)) REJECTION_QTY");
                    sb.Append(" ,NVL( FN_GET_ASM_RECOVERY_PLATE_QTY(a.DEPT_ID, c.ITEM_ID,  '" + Convert.ToDateTime(prmINV.FromDate.Value).ToString("dd-MMM-yyyy") + "'  ,  '" + Convert.ToDateTime(prmINV.ToDate.Value).ToString("dd-MMM-yyyy") + "'),0) RECOVERY_QTY ");
                    sb.Append(" FROM PROD_REJECTION_MST_ASM a ");
                    sb.Append(" INNER JOIN PROD_REJECTION_DTL_ASM b ON a.PROD_ASM_REJ_ID=b.PROD_ASM_REJ_ID ");
                    sb.Append(" INNER JOIN INV_ITEM_MASTER c ON b.ITEM_ID=c.ITEM_ID ");
                    sb.Append(" INNER JOIN UOM_INFO d ON b.UOM_ID=d.UOM_ID ");
                    sb.Append(" INNER JOIN DEPARTMENT_INFO e ON a.DEPT_ID=e.DEPARTMENT_ID ");
                    sb.Append(" INNER JOIN TBLUSER u ON a.CREATE_BY=u.USERNAME ");
                    //sb.Append(" LEFT JOIN SHIFT_MST s ON a.SHIFT_ID=s.SHIFT_ID ");
                    sb.Append(" Where 1=1 AND IS_RECOVERTOREJECT='N' ");

                    if (prmINV.FromDate != null)
                    {
                        sb.Append(" AND  a.PROD_ASM_REJ_DATE BETWEEN  :FromDate and :ToDate ");
                        cmdInfo.DBParametersInfo.Add(":FromDate", prmINV.FromDate);
                        cmdInfo.DBParametersInfo.Add(":ToDate", prmINV.ToDate);
                    }


                    if (prmINV.DeptID > 0)
                    {
                        sb.Append(" AND a.DEPT_ID=:DeptID");
                        cmdInfo.DBParametersInfo.Add(":DeptID", prmINV.DeptID);
                    }
                    if (prmINV.item_id > 0)
                    {
                        sb.Append(" AND b.ITEM_ID=:item_id ");
                        cmdInfo.DBParametersInfo.Add(":item_id", prmINV.item_id);
                    }
                }


                sb.Append(" GROUP BY c.ITEM_NAME,d.UOM_CODE,e.DEPARTMENT_NAME, c.ITEM_ID,a.DEPT_ID ");
                //,a.SHIFT_ID

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);

                foreach (DataRow dRow in dtData.Rows)
                {
                    rcProduction stk = new rcProduction();
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();
                    stk.DEPARTMENT_NAME = dRow["DEPARTMENT_NAME"].ToString();
                    stk.UOM_NAME = dRow["UOM_CODE"].ToString();
                    stk.ITEM_QTY = Conversion.DBNullDecimalToZero(dRow["REJECTION_QTY"]);
                    stk.RECOVERY_QTY = Conversion.DBNullDecimalToZero(dRow["RECOVERY_QTY"]);
                    cRptList.Add(stk);

                }

            }
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }

            return cRptList;

        }

        // Others
        public static List<rcProduction> Get_RejectionInfoSumm_Report(clsPrmInventory prmINV)
        {
            return Get_RejectionInfoSumm_Report(prmINV, null);
        }

        public static List<rcProduction> Get_RejectionInfoSumm_Report(clsPrmInventory prmINV, DBContext dc)
        {
            List<rcProduction> cRptList = new List<rcProduction>();
            bool isDCInit = false;
            //try
            {

                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();
                cmdInfo.DBParametersInfo.Clear();


                sb.Length = 0;


                //if (prmINV.DeptID == 11)
                //{

                //    sb.Append("  Select e.DEPARTMENT_NAME,c.ITEM_NAME,d.UOM_CODE,SUM(NVL(b.REJECT_QTY,0)) REJECTION_QTY, a.SHIFT_ID ");
                //    sb.Append(" FROM PRODUCTION_MST a ");
                //    sb.Append(" INNER JOIN PRODUCTION_DTL b ON a.PROD_ID=b.PROD_MST_ID ");
                //    sb.Append(" INNER JOIN INV_ITEM_MASTER c ON b.ITEM_ID=c.ITEM_ID ");
                //    sb.Append(" INNER JOIN UOM_INFO d ON b.UOM_ID=d.UOM_ID ");
                //    sb.Append(" INNER JOIN DEPARTMENT_INFO e ON a.DEPT_ID=e.DEPARTMENT_ID ");
                //    sb.Append(" INNER JOIN TBLUSER u ON a.ENTRY_BY_ID=u.USERID ");
                //    sb.Append(" LEFT JOIN SHIFT_MST s ON a.SHIFT_ID=s.SHIFT_ID ");
                //    sb.Append(" Where 1=1");
                //    sb.Append(" AND NVL(b.REJECT_QTY,0)>0 ");
                //    sb.Append(" AND a.IS_UNLOAD='Y' ");

                //    if (prmINV.FromDate != null)
                //    {
                //        sb.Append(" AND  a.PRODUCTION_DATE BETWEEN  :FromDate and :ToDate ");
                //        cmdInfo.DBParametersInfo.Add(":FromDate", prmINV.FromDate);
                //        cmdInfo.DBParametersInfo.Add(":ToDate", prmINV.ToDate);
                //    }


                //    if (prmINV.DeptID > 0)
                //    {
                //        sb.Append(" AND a.DEPT_ID=:DeptID");
                //        cmdInfo.DBParametersInfo.Add(":DeptID", prmINV.DeptID);
                //    }
                //    if (prmINV.item_id > 0)
                //    {
                //        sb.Append(" AND b.ITEM_ID=:item_id ");
                //        cmdInfo.DBParametersInfo.Add(":item_id", prmINV.item_id);
                //    }
                   

                //}
                //else
                //{
                    sb.Append(" Select e.DEPARTMENT_NAME,c.ITEM_NAME,d.UOM_CODE,SUM(NVL(b.REJECTION_QTY,0)) REJECTION_QTY,a.SHIFT_ID ");
                    sb.Append(" FROM PROD_REJECTION_MST a ");
                    sb.Append(" INNER JOIN PROD_REJECTION_DTL b ON a.PROD_REJECTION_ID=b.PROD_REJECTION_ID ");
                    sb.Append(" INNER JOIN INV_ITEM_MASTER c ON b.ITEM_ID=c.ITEM_ID ");
                    sb.Append(" INNER JOIN UOM_INFO d ON b.UOM_ID=d.UOM_ID ");
                    sb.Append(" INNER JOIN DEPARTMENT_INFO e ON a.DEPT_ID=e.DEPARTMENT_ID ");
                    sb.Append(" INNER JOIN TBLUSER u ON a.CREATE_BY=u.USERNAME ");
                    sb.Append(" LEFT JOIN SHIFT_MST s ON a.SHIFT_ID=s.SHIFT_ID ");
                    sb.Append(" Where 1=1 ");

                    if (prmINV.FromDate != null)
                    {
                        sb.Append(" AND  a.PROD_REJECTION_DATE BETWEEN  :FromDate and :ToDate ");
                        cmdInfo.DBParametersInfo.Add(":FromDate", prmINV.FromDate);
                        cmdInfo.DBParametersInfo.Add(":ToDate", prmINV.ToDate);
                    }


                    if (prmINV.DeptID > 0)
                    {
                        sb.Append(" AND a.DEPT_ID=:DeptID");
                        cmdInfo.DBParametersInfo.Add(":DeptID", prmINV.DeptID);
                    }
                    if (prmINV.item_id > 0)
                    {
                        sb.Append(" AND b.ITEM_ID=:item_id ");
                        cmdInfo.DBParametersInfo.Add(":item_id", prmINV.item_id);
                    }
                //}

                sb.Append(" GROUP BY c.ITEM_NAME,d.UOM_CODE,e.DEPARTMENT_NAME,a.SHIFT_ID  ");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);

                foreach (DataRow dRow in dtData.Rows)
                {
                    rcProduction stk = new rcProduction();
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();
                    stk.DEPARTMENT_NAME = dRow["DEPARTMENT_NAME"].ToString();
                    stk.UOM_NAME = dRow["UOM_CODE"].ToString();
                    stk.ITEM_QTY = Conversion.DBNullDecimalToZero(dRow["REJECTION_QTY"]);

                    cRptList.Add(stk);

                }

            }
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }

            return cRptList;

        }


        //Details

        //Only Assembly Details


        public static List<rcProduction> Get_RejectionAssemblyDetailsInfo_Report(clsPrmInventory prmINV)
        {
            return Get_RejectionAssemblyDetailsInfo_Report(prmINV, null);
        }

        public static List<rcProduction> Get_RejectionAssemblyDetailsInfo_Report(clsPrmInventory prmINV, DBContext dc)
        {
            List<rcProduction> cRptList = new List<rcProduction>();
            bool isDCInit = false;
            //try
            {

                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();
                cmdInfo.DBParametersInfo.Clear();


                sb.Length = 0;

                if (prmINV.DeptID == 136 || prmINV.DeptID == 140)
                {
                    sb.Append(" Select a.PROD_ASM_REJ_NO PROD_NO,a.PROD_ASM_REJ_DATE PRODUCTION_DATE,'Plate' REJECT_ITEM_TYPE,a.REJECTION_ASM_REASON REJECTION_REASON ");
                    sb.Append(" ,u.FULLNAME CREATE_BY,a.CREATE_DATE,e.DEPARTMENT_NAME,c.ITEM_NAME,b.REJECTION_QTY,d.UOM_CODE,b.REJECTION_DET_REMARKS,a.SHIFT_ID,s.SHIFT_NAME ");
                    sb.Append(" FROM PROD_REJECTION_MST_ASM a ");
                    sb.Append(" INNER JOIN PROD_REJECTION_DTL_ASM b ON a.PROD_ASM_REJ_ID=b.PROD_ASM_REJ_ID ");
                    sb.Append(" INNER JOIN INV_ITEM_MASTER c ON b.ITEM_ID=c.ITEM_ID ");
                    sb.Append(" INNER JOIN UOM_INFO d ON b.UOM_ID=d.UOM_ID ");
                    sb.Append(" INNER JOIN DEPARTMENT_INFO e ON a.DEPT_ID=e.DEPARTMENT_ID ");
                    sb.Append(" INNER JOIN TBLUSER u ON a.CREATE_BY=u.USERNAME ");
                    sb.Append(" LEFT JOIN SHIFT_MST s ON a.SHIFT_ID=s.SHIFT_ID ");
                    sb.Append(" Where 1=1 AND IS_RECOVERTOREJECT='N' ");
                    if (prmINV.FromDate != null)
                    {
                        sb.Append(" AND  a.PROD_ASM_REJ_DATE BETWEEN  :FromDate and :ToDate ");
                        cmdInfo.DBParametersInfo.Add(":FromDate", prmINV.FromDate);
                        cmdInfo.DBParametersInfo.Add(":ToDate", prmINV.ToDate);
                    }


                    if (prmINV.DeptID > 0)
                    {
                        sb.Append(" AND a.DEPT_ID=:DeptID");
                        cmdInfo.DBParametersInfo.Add(":DeptID", prmINV.DeptID);
                    }
                    if (prmINV.item_id > 0)
                    {
                        sb.Append(" AND b.ITEM_ID=:item_id ");
                        cmdInfo.DBParametersInfo.Add(":item_id", prmINV.item_id);
                    }
                }
               



                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);

                foreach (DataRow dRow in dtData.Rows)
                {
                    rcProduction stk = new rcProduction();

                    stk.PROD_NO = dRow["PROD_NO"].ToString();
                    stk.PRODUCTION_DATE = Conversion.DBNullDateToNull(dRow["PRODUCTION_DATE"]);
                    stk.CREATE_BY = dRow["CREATE_BY"].ToString();
                    stk.ENTRY_DATE = Conversion.DBNullDateToNull(dRow["CREATE_DATE"]);
                    stk.SHIFT_NAME = dRow["SHIFT_NAME"].ToString();
                    stk.SHIFT_ID = dRow["SHIFT_ID"].ToString();
                    stk.REJECT_ITEM_TYPE = dRow["REJECT_ITEM_TYPE"].ToString();
                    stk.REJECTION_REASON = dRow["REJECTION_REASON"].ToString();
                    stk.REJECTION_DET_REMARKS = dRow["REJECTION_DET_REMARKS"].ToString();
                    stk.DEPARTMENT_NAME = dRow["DEPARTMENT_NAME"].ToString();
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();
                    stk.UOM_NAME = dRow["UOM_CODE"].ToString();
                    stk.ITEM_QTY = Conversion.DBNullDecimalToZero(dRow["REJECTION_QTY"]);
                    cRptList.Add(stk);

                }

            }
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }

            return cRptList;

        }
        //Others


        public static List<rcProduction> Get_RejectionDetailsInfo_Report(clsPrmInventory prmINV)
        {
            return Get_RejectionDetailsInfo_Report(prmINV, null);
        }

        public static List<rcProduction> Get_RejectionDetailsInfo_Report(clsPrmInventory prmINV, DBContext dc)
        {
            List<rcProduction> cRptList = new List<rcProduction>();
            bool isDCInit = false;
            //try
            {

                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();
                cmdInfo.DBParametersInfo.Clear();


                sb.Length = 0;

                //if (prmINV.DeptID == 110)
                //{

                //    sb.Append("  Select a.PROD_NO,a.PRODUCTION_DATE,'Plate' REJECT_ITEM_TYPE,'Reject' REJECTION_REASON ");
                //    sb.Append(" ,u.FULLNAME CREATE_BY,a.ENTRY_DATE CREATE_DATE,e.DEPARTMENT_NAME,c.ITEM_NAME,b.REJECT_QTY REJECTION_QTY,d.UOM_CODE,'' REJECTION_DET_REMARKS, ");
                //    sb.Append(" a.SHIFT_ID,s.SHIFT_NAME ");
                //    sb.Append(" FROM PRODUCTION_MST a ");
                //    sb.Append(" INNER JOIN PRODUCTION_DTL b ON a.PROD_ID=b.PROD_MST_ID ");
                //    sb.Append(" INNER JOIN INV_ITEM_MASTER c ON b.ITEM_ID=c.ITEM_ID ");
                //    sb.Append(" INNER JOIN UOM_INFO d ON b.UOM_ID=d.UOM_ID ");
                //    sb.Append(" INNER JOIN DEPARTMENT_INFO e ON a.DEPT_ID=e.DEPARTMENT_ID ");
                //    sb.Append(" INNER JOIN TBLUSER u ON a.ENTRY_BY_ID=u.USERID ");
                //    sb.Append(" LEFT JOIN SHIFT_MST s ON a.SHIFT_ID=s.SHIFT_ID ");
                //    sb.Append(" Where 1=1 ");
                //    sb.Append(" AND NVL(b.REJECT_QTY,0)>0 ");
                //    sb.Append(" AND a.IS_UNLOAD='Y' ");

                //    if (prmINV.FromDate != null)
                //    {
                //        sb.Append(" AND  a.PRODUCTION_DATE BETWEEN  :FromDate and :ToDate ");
                //        cmdInfo.DBParametersInfo.Add(":FromDate", prmINV.FromDate);
                //        cmdInfo.DBParametersInfo.Add(":ToDate", prmINV.ToDate);
                //    }


                //    if (prmINV.DeptID > 0)
                //    {
                //        sb.Append(" AND a.DEPT_ID=:DeptID");
                //        cmdInfo.DBParametersInfo.Add(":DeptID", prmINV.DeptID);
                //    }
                //    if (prmINV.item_id > 0)
                //    {
                //        sb.Append(" AND b.ITEM_ID=:item_id ");
                //        cmdInfo.DBParametersInfo.Add(":item_id", prmINV.item_id);
                //    }

                //}
               
                //else
                //{
                  sb.Append(" SELECT MST.* FROM ( ");
                  sb.Append(" SELECT a.PROD_REJECTION_NO PROD_NO,b.PROD_REJECTION_DTL_ID,A.PROD_REJECTION_DATE PRODUCTION_DATE,a.REJECTION_REASON,DEPT.DEPARTMENT_NAME,STLM.NAME STLM_NAME ");
                  sb.Append(" ,B.PROD_REJECTION_ID,b.REJECTION_QTY,b.UOM_ID,f.UOM_CODE ");
                  sb.Append(" ,S.SHIFT_NAME,A.DEPT_ID,A.STLM_ID ");
                  sb.Append(" ,ritm.ITEM_NAME TO_REJ_ITEM_NAME ,B.TO_REJ_ITEM_ID ");
                  sb.Append(" ,u.FULLNAME CREATE_BY,a.CREATE_DATE ");
                  sb.Append(" FROM PROD_REJECTION_MST a ");
                  sb.Append(" INNER JOIN PROD_REJECTION_DTL b ON a.PROD_REJECTION_ID=b.PROD_REJECTION_ID ");
                  sb.Append(" INNER JOIN UOM_INFO f ON b.UOM_ID=f.UOM_ID "); 
                  sb.Append(" LEFT JOIN INV_ITEM_MASTER ritm ON b.TO_REJ_ITEM_ID=ritm.ITEM_ID  "); 
                  sb.Append(" INNER JOIN DEPARTMENT_INFO DEPT ON A.DEPT_ID=DEPT.DEPARTMENT_ID ");
                  sb.Append(" LEFT JOIN STORAGE_LOCATION_MST STLM ON A.STLM_ID=STLM.STLM_ID ");
                  sb.Append(" INNER JOIN SHIFT_MST S ON A.SHIFT_ID=S.SHIFT_ID ");
                  sb.Append(" INNER JOIN TBLUSER u ON a.CREATE_BY=u.USERNAME ");
                  sb.Append(" Where 1=1 ");
                     
                  sb.Append(" UNION ALL ");
                     
                  sb.Append(" SELECT A.SCRAP_NO PROD_NO,B.SCRAP_DTL_ID PROD_REJECTION_DTL_ID,A.SCRAP_DATE PRODUCTION_DATE,'Scrap' REJECTION_REASON,DEPT.DEPARTMENT_NAME,STLM.NAME STLM_NAME ");
                  sb.Append(" ,B.SCRAP_ID PROD_REJECTION_ID,B.ITEM_QTY REJECTION_QTY,b.UOM_ID,f.UOM_CODE ");
                  sb.Append(" ,S.SHIFT_NAME,A.DEPT_ID,A.STLM_ID ");
                  sb.Append(" ,ritm.ITEM_NAME TO_REJ_ITEM_NAME ,B.ITEM_ID TO_REJ_ITEM_ID ");
                  sb.Append(" ,u.FULLNAME CREATE_BY,a.CREATE_DATE ");
                  sb.Append(" FROM PROD_SCRAP_MST a ");
                  sb.Append(" INNER JOIN PROD_SCRAP_DTL b ON A.SCRAP_ID=B.SCRAP_ID ");
                  sb.Append(" INNER JOIN UOM_INFO f ON b.UOM_ID=f.UOM_ID  ");
                  sb.Append(" LEFT JOIN INV_ITEM_MASTER ritm ON B.ITEM_ID=ritm.ITEM_ID  "); 
                  sb.Append(" INNER JOIN DEPARTMENT_INFO DEPT ON A.DEPT_ID=DEPT.DEPARTMENT_ID ");
                  sb.Append(" LEFT JOIN STORAGE_LOCATION_MST STLM ON A.STLM_ID=STLM.STLM_ID ");
                  sb.Append(" LEFT JOIN SHIFT_MST S ON a.SHIFT_ID=S.SHIFT_ID ");
                  sb.Append(" INNER JOIN TBLUSER u ON a.CREATE_BY=u.USERNAME ");
                  sb.Append(" Where 1=1 ");
                  sb.Append(" ) MST ");
                  sb.Append(" Where 1=1  ");

                    if (prmINV.FromDate != null)
                    {
                        sb.Append(" AND  TO_DATE(MST.PRODUCTION_DATE) BETWEEN  :FromDate and :ToDate ");
                        cmdInfo.DBParametersInfo.Add(":FromDate", prmINV.FromDate);
                        cmdInfo.DBParametersInfo.Add(":ToDate", prmINV.ToDate);
                    }


                    if (prmINV.DeptID > 0)
                    {
                        sb.Append(" AND MST.DEPT_ID=:DeptID");
                        cmdInfo.DBParametersInfo.Add(":DeptID", prmINV.DeptID);
                    }

                    if (prmINV.STLM_ID > 0)
                    {
                        sb.Append(" AND MST.STLM_ID=:STLM_ID");
                        cmdInfo.DBParametersInfo.Add(":STLM_ID", prmINV.STLM_ID);
                    }
                    if (prmINV.item_id > 0)
                    {
                        sb.Append(" AND MST.TO_REJ_ITEM_ID=:item_id ");
                        cmdInfo.DBParametersInfo.Add(":item_id", prmINV.item_id);
                    }
                //}



                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);

                foreach (DataRow dRow in dtData.Rows)
                {
                    rcProduction stk = new rcProduction();

                    stk.PROD_NO = dRow["PROD_NO"].ToString();
                    stk.PRODUCTION_DATE = Conversion.DBNullDateToNull(dRow["PRODUCTION_DATE"]);
                    stk.CREATE_BY = dRow["CREATE_BY"].ToString();
                    stk.ENTRY_DATE = Conversion.DBNullDateToNull(dRow["CREATE_DATE"]);
                    stk.SHIFT_NAME = dRow["SHIFT_NAME"].ToString();
                    stk.STLM_NAME = dRow["STLM_NAME"].ToString();
                    stk.REJECTION_REASON = dRow["REJECTION_REASON"].ToString();
                    stk.DEPARTMENT_NAME = dRow["DEPARTMENT_NAME"].ToString();
                    stk.ITEM_NAME = dRow["TO_REJ_ITEM_NAME"].ToString();
                    stk.UOM_NAME = dRow["UOM_CODE"].ToString();
                    stk.ITEM_QTY = Conversion.DBNullDecimalToZero(dRow["REJECTION_QTY"]);
                    cRptList.Add(stk);

                }

            }
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }

            return cRptList;

        }


        #region  MRB

        public static List<rcProduction> MRB_Department_Production_Report(clsPrmInventory rptClass)
        {
            return MRB_Department_Production_Report(rptClass, null);
        }

        public static string GetMRBPlateProductionDtlListString()
        {
            StringBuilder sb = new StringBuilder();

             sb.Append(" SELECT   ");
             sb.Append("   pmst.PROD_ID  ");
             sb.Append("  ,pmst.PROD_NO  ");  
             sb.Append("  ,pmst.DEPT_ID   ");
             sb.Append("  ,dept.DEPARTMENT_NAME    ");
             sb.Append("  ,pmst.REF_NO_MANUAL  "); 
             sb.Append("  ,pmst.PRODUCTION_DATE  "); 
             sb.Append("  ,pmst.REJECTED_QTY   ");
             sb.Append("  ,dtl.PROD_MST_ID  "); 
             sb.Append("  ,dtl.PROD_DTL_ID   ");
             sb.Append("  ,dtl.SLNO   ");
             sb.Append("  ,inv.ITEM_NAME   ");
             sb.Append("  ,inv.ITEM_ID   ");
             sb.Append("  ,dtl.PANEL_PC   ");
             sb.Append("  ,dtl.ITEM_PANEL_QTY   ");
             sb.Append("  ,dtl.PANEL_UOM_ID   ");
             sb.Append("  ,dtl.ITEM_QTY   ");
             sb.Append("  ,dtl.UOM_ID   ");
             sb.Append("  ,dtl.ITEM_WEIGHT   ");
             sb.Append("  ,dtl.WEIGHT_UOM_ID   ");
             sb.Append("  ,dtl.BOM_ID   ");
             sb.Append("  ,dtl.REMARKS   ");
             sb.Append("  ,dtl.OPERATOR_ID   ");
             sb.Append("  ,dtl.USED_BAR_PC   ");
             sb.Append("  ,dtl.BAR_TYPE   ");
             sb.Append("  ,dtl.USED_QTY_KG   ");
             sb.Append("  ,dtl.BAR_WEIGHT   ");
             sb.Append("  ,dtl.GRID_BATCH   ");
             sb.Append("  ,dtl.REJECT_QTY   ");
             sb.Append("  ,us.FULLNAME CREATE_BY  ");
             sb.Append("  ,pmst.ENTRY_DATE  ");
             sb.Append(" ,inv.ITEM_STANDARD_WEIGHT_KG ");
             sb.Append(" ,pinv.ITEM_NAME MRB_PLATE_NAME ");
             sb.Append(" ,dtl.MRB_PLATE_QTY ");
             sb.Append(" ,dtl.MRB_PLATE_WEIGHT ");
             sb.Append(" ,dtl.SCRAP_BATTERY_WEIGHT ");
             sb.Append("  FROM    ");
             sb.Append("  PRODUCTION_MST pmst  "); 
             sb.Append("  INNER JOIN  PRODUCTION_DTL dtl ON pmst.PROD_ID=dtl.PROD_MST_ID   ");
             sb.Append("  INNER JOIN INV_ITEM_MASTER inv ON dtl.ITEM_ID=inv.ITEM_ID   ");
             sb.Append(" INNER JOIN INV_ITEM_MASTER pInv ON dtl.MRB_PLATE_ID=pInv.ITEM_ID ");
             sb.Append("  INNER JOIN DEPARTMENT_INFO dept ON pmst.DEPT_ID=dept.DEPARTMENT_ID   ");
             sb.Append("  LEFT JOIN MACHINE_MST mac ON dtl.MACHINE_ID=mac.MACHINE_ID   ");
             sb.Append("  LEFT JOIN TBLUSER us  ON us.USERID=pmst.ENTRY_BY_ID   ");
             sb.Append(" WHERE 1=1  ");
            return sb.ToString();
        }
        public static List<rcProduction> MRB_Department_Production_Report(clsPrmInventory pObj, DBContext dc)
        {
            List<rcProduction> cObjList = new List<rcProduction>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetMRBPlateProductionDtlListString());
                if (pObj.productiondate.ToString() != "")
                {
                    sb.Append("  AND TO_DATE(mst.PRODUCTION_DATE) = TO_DATE(@PRODUCTION_DATE)  ");
                    cmdInfo.DBParametersInfo.Add("@PRODUCTION_DATE", pObj.productiondate);
                }
                if (pObj.From_Dept_Id > 0)
                {
                    sb.Append(" AND pmst.DEPT_ID = @DEPT_ID ");
                    cmdInfo.DBParametersInfo.Add("@DEPT_ID", pObj.From_Dept_Id);
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

               
                if (pObj.item_id > 0)
                {
                    sb.Append(" AND dtl.ITEM_ID = @P_ITEM_ID ");
                    cmdInfo.DBParametersInfo.Add("@P_ITEM_ID", pObj.item_id);
                }

                if (pObj.itemGroup_id > 0)
                {
                    sb.Append(" AND inv.ITEM_GROUP_ID = @P_ITEM_GROUP_ID ");
                    cmdInfo.DBParametersInfo.Add("@P_ITEM_GROUP_ID", pObj.itemGroup_id);
                }

                if (pObj.prod_no != "")
                {
                    sb.Append(" AND pmst.PROD_NO = @PROD_NO ");
                    cmdInfo.DBParametersInfo.Add("@PROD_ID", pObj.prod_no);
                }

                if (pObj.isPacking == "Y")
                {
                    sb.Append(" AND dtl.IS_PACKING = @IS_PACKING ");
                    cmdInfo.DBParametersInfo.Add("@IS_PACKING", pObj.isPacking);
                }
                else
                {
                    sb.Append(" AND dtl.IS_PACKING IS NULL ");

                }

                if (!string.IsNullOrEmpty(pObj.Shift_Id) && pObj.Shift_Id != "0")
                {
                    sb.Append(" AND pmst.SHIFT_ID = @P_SHIFT_ID ");
                    cmdInfo.DBParametersInfo.Add("@P_SHIFT_ID", pObj.Shift_Id);
                }

                if (pObj.autho_status != "" && pObj.autho_status != null)
                {
                    sb.Append(" AND pmst.AUTH_STATUS = @AUTH_STATUS ");
                    cmdInfo.DBParametersInfo.Add("@AUTH_STATUS", pObj.autho_status);
                }

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<rcProduction>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        #endregion 


        #region ***************************************Pure Lead Comsumption *******************************************************************
        public static List<dcMaterialStock> GetPureLeadConsumption(clsPrmInventory prmINV)
        {
            return GetPureLeadConsumption(prmINV, null);
        }

        public static List<dcMaterialStock> GetPureLeadConsumption(clsPrmInventory prmINV, DBContext dc)
        {

            List<dcMaterialStock> cRptList = new List<dcMaterialStock>();


            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();

                //if (prmINV.ToDate.HasValue)
                //{
                cmdInfo.DBParametersInfo.Add(":p_from_date", prmINV.fromProdDate);
                cmdInfo.DBParametersInfo.Add(":p_to_date", prmINV.toProdDate);
                //}
                //cmdInfo.DBParametersInfo.Add(":p_itemtype_id", prmINV.itemtype_id);
                //cmdInfo.DBParametersInfo.Add(":p_itemGroup_id", prmINV.itemGroup_id);
                //cmdInfo.DBParametersInfo.Add(":p_item_id", prmINV.item_id);
                //cmdInfo.DBParametersInfo.Add(":p_store_id", prmINV.store_id);
                //cmdInfo.DBParametersInfo.Add(":p_itemclass_id", prmINV.ItemClassId);
                //cmdInfo.DBParametersInfo.Add(":p_battery_type_id", prmINV.Battery_Type_ID);
                //cmdInfo.DBParametersInfo.Add(":p_is_repair", prmINV.Is_Repair);
                //cmdInfo.DBParametersInfo.Add(":p_dept_id", prmINV.DeptID);
                //cmdInfo.DBParametersInfo.Add(":p_is_finished", prmINV.is_Finished);

                DBQuery dbq = new DBQuery();

                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = "SP_LEAD_CONSUMPTION_REPORT";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);
                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;

                sb.Append("  SELECT   ");
                sb.Append("  DEPT_ID  ");
                sb.Append("  ,DEPT_NAME  ");
                //sb.Append("  --,CURRENT_MONTH_QTY,CURRENT_MONTH_WEIGHT,TODAY_QTY,TODATY_WEIGHT  ");
                sb.Append("  ,SUM(OP_QTY) OP_QTY  ");
                //sb.Append("  --,OP_WEIGHT--,B_OP_QTY--,B_OP_WEIGHT  ");
                sb.Append("  ,SUM(DEPT_RCV_CUR_MONTH_QTY) DEPT_RCV_CUR_MONTH_QTY  ");
                sb.Append("  ,NVL(SUM(NVL(IB_CUTTING,0)),0) IB_CUTTING  ");
                //sb.Append("  --,DEPT_RCV_CUR_MONTH_WEIGHT,B_DEPT_RCV_CUR_MONTH_QTY,B_DEPT_RCV_CUR_MONTH_WEIGHT  ");
                sb.Append("  ,SUM(ISSUE_CURRENT_MONTH_QTY) ISSUE_CURRENT_MONTH_QTY ,Sum( B_ISSUE_CUR_MONTH_QTY ) B_ISSUE_CUR_MONTH_QTY ");
                //sb.Append("  --,ISSUE_CURRENT_MONTH_WEIGHT,B_ISSUE_CURT_MONTH_WEIGHT  ");
                sb.Append("  ,SUM(SCRAP_CUR_MONTH_QTY) SCRAP_CUR_MONTH_QTY ,SUM(B_SCRAP_CUR_MONTH_QTY) B_SCRAP_CUR_MONTH_QTY ");
                //sb.Append("  --,SCRAP_CUR_MONTH_WEIGHT,B_SCRAP_CUR_MONTH_WEIGHT,GRID_STD_WT,PASTE_STD_WT  ");
                sb.Append("  FROM TEMP_LEAD_CONSUMPTION_DEPT  ");
                sb.Append("  group by DEPT_ID,DEPT_NAME  ");
                 
                DBQuery dbqtemp = new DBQuery();

                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);
                foreach (DataRow dRow in dtData.Rows)
                {
                    dcMaterialStock stk = new dcMaterialStock();
                    stk.DEPARTMENT_NAME = dRow["DEPT_NAME"].ToString();

                    // stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    // stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();

                    //stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = "Kg";  // dRow["UOM_NAME"].ToString();
                    stk.OPPENING_BAL_QTY = Conversion.DBNullDecimalToZero(dRow["OP_QTY"].ToString());
                    stk.IRR_BAL_QTY = Conversion.DBNullDecimalToZero(dRow["DEPT_RCV_CUR_MONTH_QTY"].ToString());
                    stk.TOT_RECEIVE_QNTY = Conversion.DBNullDecimalToZero(dRow["OP_QTY"].ToString()) + Conversion.DBNullDecimalToZero(dRow["DEPT_RCV_CUR_MONTH_QTY"].ToString());
                    stk.ITC_BAL_QTY = Conversion.DBNullDecimalToZero(dRow["ISSUE_CURRENT_MONTH_QTY"].ToString());
                    stk.ITC_OTHERS = Conversion.DBNullDecimalToZero(dRow["B_ISSUE_CUR_MONTH_QTY"].ToString());
                    stk.IB_CUTTING = Conversion.DBNullDecimalToZero(dRow["IB_CUTTING"].ToString());   // + Conversion.DBNullDecimalToZero(dRow["IB_CUTTING"].ToString())
                    stk.REJECT_QTY_RCV = Conversion.DBNullDecimalToZero(dRow["SCRAP_CUR_MONTH_QTY"].ToString()) ;
                    stk.REMAINING_QTY = (Conversion.DBNullDecimalToZero(dRow["OP_QTY"].ToString()) + Conversion.DBNullDecimalToZero(dRow["DEPT_RCV_CUR_MONTH_QTY"].ToString())) - (Conversion.DBNullDecimalToZero(dRow["SCRAP_CUR_MONTH_QTY"].ToString()) + Conversion.DBNullDecimalToZero(dRow["IB_CUTTING"].ToString()) + Conversion.DBNullDecimalToZero(dRow["B_ISSUE_CUR_MONTH_QTY"].ToString()) + Conversion.DBNullDecimalToZero(dRow["ISSUE_CURRENT_MONTH_QTY"].ToString()));
                    stk.SCRAP_MIXTURE_QTY = Conversion.DBNullDecimalToZero(dRow["B_SCRAP_CUR_MONTH_QTY"].ToString());

                    // Conversion.DBNullDecimalToZero(dRow["SCRAP_CUR_MONTH_QTY"].ToString())+
                    //stk.CLOSING_QTY =
                    cRptList.Add(stk);
                }
                //if (cRptList.Count > 0)
                //{
                //    dcMaterialStock stk = new dcMaterialStock();
                //    stk.DEPARTMENT_NAME = "";
                //    stk.UOM_NAME = "";
                //    stk.DEPT_ISS_CURRENT_MONTH_QTY = 0;
                //}
            }
       
          catch { throw; }
           finally 
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }

            return cRptList;

        }
        #endregion




        #region ***************************************Battery Lead Comsumption *******************************************************************
        public static List<dcMaterialStock> GetBatteryLeadConsumption(clsPrmInventory prmINV)
        {
            return GetBatteryLeadConsumption(prmINV, null);
        }

        public static List<dcMaterialStock> GetBatteryLeadConsumption(clsPrmInventory prmINV, DBContext dc)
        {
            List<dcMaterialStock> cRptList = new List<dcMaterialStock>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();
                cmdInfo.DBParametersInfo.Clear();
                //if (prmINV.ToDate.HasValue)
                //{
                cmdInfo.DBParametersInfo.Add(":p_from_date", prmINV.fromProdDate);
                cmdInfo.DBParametersInfo.Add(":p_to_date", prmINV.toProdDate);
                //}
                //cmdInfo.DBParametersInfo.Add(":p_itemtype_id", prmINV.itemtype_id);
                //cmdInfo.DBParametersInfo.Add(":p_itemGroup_id", prmINV.itemGroup_id);
                //cmdInfo.DBParametersInfo.Add(":p_item_id", prmINV.item_id);
                //cmdInfo.DBParametersInfo.Add(":p_store_id", prmINV.store_id);
                //cmdInfo.DBParametersInfo.Add(":p_itemclass_id", prmINV.ItemClassId);
                //cmdInfo.DBParametersInfo.Add(":p_battery_type_id", prmINV.Battery_Type_ID);
                //cmdInfo.DBParametersInfo.Add(":p_is_repair", prmINV.Is_Repair);
                cmdInfo.DBParametersInfo.Add(":p_dept_id", 0);
                //cmdInfo.DBParametersInfo.Add(":p_is_finished", prmINV.is_Finished);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;
                cmdInfo.CommandText = "SP_BATTERY_LEAD_CONSUMPTION";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);
                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;
                sb.Append(" SELECT ");
                sb.Append(" DEPT_ID, DEPT_NAME,  SUM(BOM_WEIGHT)BOM_WEIGHT,SUM(PROD_QTY) PROD_QTY  ");
                sb.Append(" ,SUM(PROD_USED_QTY) PROD_USED_QTY , SUM( DIFF_WEIGHT) DIFF_WEIGHT ");
                sb.Append(" from TEMP_BAT_LEAD_CONSUMPTION where BOM_WEIGHT IS NOT NULL AND PROD_QTY IS NOT NULL ");
                sb.Append(" GROUP BY DEPT_ID, DEPT_NAME    ");
                DBQuery dbqtemp = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);
                foreach (DataRow dRow in dtData.Rows)
                {
                    dcMaterialStock stk = new dcMaterialStock();
                    stk.DEPARTMENT_NAME = dRow["DEPT_NAME"].ToString();
                    stk.UOM_NAME = "Kg";  // dRow["UOM_NAME"].ToString();
                    //stk.BAT_ITEM_NAME = dRow["BAT_ITEM_NAME"].ToString();
                    stk.BOM_WEIGHT = Conversion.DBNullDecimalToZero(dRow["BOM_WEIGHT"].ToString());
                    stk.PROD_QTY = Conversion.DBNullDecimalToZero(dRow["PROD_QTY"].ToString());
                    //stk.TOTAL_WEIGHT = Conversion.DBNullDecimalToZero(dRow["TOTAL_WEIGHT"].ToString());
                    stk.PROD_USED_QTY = Conversion.DBNullDecimalToZero(dRow["PROD_USED_QTY"].ToString());
                    stk.DIFF_WEIGHT = Conversion.DBNullDecimalToZero(dRow["DIFF_WEIGHT"].ToString());
                    //stk.CLOSING_QTY =
                    cRptList.Add(stk);
                }
            }

            catch { throw; }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }

            return cRptList;

        }
        #endregion


        #region**************************************************Pasting  Summary Report ********************************************
        public static List<dcMaterialStock> GeneratePestingRejectGridSummReport(clsPrmInventory prmINV)
        {
            return GeneratePestingRejectGridSummReport(prmINV, null);
        }

        public static List<dcMaterialStock> GeneratePestingRejectGridSummReport(clsPrmInventory prmINV, DBContext dc)
        {
            List<dcMaterialStock> cRptList = new List<dcMaterialStock>();
            bool isDCInit = false;
            //try
            {
                prmINV.DeptID = 7;

                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();

                //if (prmINV.ToDate.HasValue)
                //{
                cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.fromProdDate);
                cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.toProdDate);
                //}
                cmdInfo.DBParametersInfo.Add(":p_itemtype_id", prmINV.itemtype_id);
                cmdInfo.DBParametersInfo.Add(":p_itemGroup_id", prmINV.itemGroup_id);
                cmdInfo.DBParametersInfo.Add(":p_item_id", prmINV.item_id);
                cmdInfo.DBParametersInfo.Add(":p_store_id", prmINV.store_id);
                cmdInfo.DBParametersInfo.Add(":p_itemclass_id", prmINV.ItemClassId);
                cmdInfo.DBParametersInfo.Add(":p_battery_type_id", prmINV.Battery_Type_ID);
                cmdInfo.DBParametersInfo.Add(":p_is_repair", prmINV.Is_Repair);
                cmdInfo.DBParametersInfo.Add(":p_dept_id", prmINV.DeptID);
                cmdInfo.DBParametersInfo.Add(":p_is_finished", "Y");



                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;
                cmdInfo.CommandText = "SP_PASTING_GRID_REJ_SUMM_RT";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);

                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;

                //sb.Append(" Select ITEM_GROUP_ID,ITEM_GROUP_NAME,ITEM_ID,ITEM_NAME,UOM_ID ");
                //sb.Append(" ,UOM_NAME,ITEM_TYPE_ID,ITEM_TYPE_NAME,ITEM_CLASS_ID,ITEM_CLASS_NAME ");
                //sb.Append(" ,OP_GD_GRID_IRR_QTY,OP_GD_GRID_ITC_QTY,OP_GD_GRID_BAL,IRR_GD_GRID_QTY ");
                //sb.Append(" ,ITC_GD_GRID_QTY,ITC_GD_GRID_BAL_QTY,OP_REJ_GRID_IRR_QTY,OP_REJ_GRID_ITC_QTY ");
                //sb.Append(" ,OP_REJ_GRID_BAL_QTY,IRR_REJ_GRID_QTY,ITC_REJ_GRID_QTY,ITC_REJ_GRID_BAL_QTY,ITEM_STANDARD_WEIGHT_KG,ISSUE_TO_STORE_REJ_GRID,OP_GD_GRID_ADJ_QTY ");
                //sb.Append(" FROM TEMP_PASTE_GRID_REJ_STOCK ");
                // SUM(ITEM_STANDARD_WEIGHT_KG * (OP_REJ_GRID_BAL_QTY +  IRR_REJ_GRID_QTY))
                //SUM(IRR_REJ_GRID_QTY)
                sb.Append(" Select 'Pasting' DEPARTMENT_NAME,  ");
                sb.Append(" sum(OP_GD_GRID_IRR_QTY) OP_GD_GRID_IRR_QTY,SUM(OP_GD_GRID_ITC_QTY) OP_GD_GRID_ITC_QTY,  ");
                sb.Append(" sum(OP_GD_GRID_BAL) OP_GD_GRID_BAL ,SUM(IRR_GD_GRID_QTY ) IRR_GD_GRID_QTY  ");
                sb.Append(" ,SUM(ITC_GD_GRID_QTY) ITC_GD_GRID_QTY ,SUM(ITC_GD_GRID_BAL_QTY) ITC_GD_GRID_BAL_QTY,SUM(OP_REJ_GRID_IRR_QTY) OP_REJ_GRID_IRR_QTY,SUM(OP_REJ_GRID_ITC_QTY ) OP_REJ_GRID_ITC_QTY   ");
                sb.Append(" ,SUM(OP_REJ_GRID_BAL_QTY) OP_REJ_GRID_BAL_QTY,  FN_GET_REJECT_GRID_WT_PC('" + prmINV.fromProdDate + "','" + prmINV.toProdDate + "',7,'PC') IRR_REJ_GRID_QTY,SUM(ITC_REJ_GRID_QTY) ITC_REJ_GRID_QTY,SUM(ITC_REJ_GRID_BAL_QTY) ITC_REJ_GRID_BAL_QTY  ");
                sb.Append(" ,SUM(ISSUE_TO_STORE_REJ_GRID) ISSUE_TO_STORE_REJ_GRID,SUM(OP_GD_GRID_ADJ_QTY) OP_GD_GRID_ADJ_QTY ,  FN_GET_REJECT_GRID_WT_PC('" + prmINV.fromProdDate + "','" + prmINV.toProdDate + "',7,'WT') TOTAL_REJ_GRID_WET ");
                sb.Append(" FROM TEMP_PASTE_GRID_REJ_STOCK   ");

                DBQuery dbqtemp = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);

                foreach (DataRow dRow in dtData.Rows)
                {
                    dcMaterialStock stk = new dcMaterialStock();
                    //stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                   // stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                  //  stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                  //  stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();

                   // stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                   // stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                  //  stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);
                    stk.DEPARTMENT_NAME = dRow["DEPARTMENT_NAME"].ToString();
                  //  stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                    //stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    //stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();
                    stk.OP_GD_GRID_ADJ_QTY  = Conversion.DBNullDecimalToZero(dRow["OP_GD_GRID_ADJ_QTY"]);
                    stk.OP_GD_GRID_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_GD_GRID_IRR_QTY"]);
                    stk.OP_GD_GRID_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_GD_GRID_ITC_QTY"]);
                    stk.OP_GD_GRID_BAL = stk.OP_GD_GRID_IRR_QTY + stk.OP_GD_GRID_ADJ_QTY - stk.OP_GD_GRID_ITC_QTY;//Conversion.DBNullDecimalToZero(dRow["OP_GD_GRID_BAL"]);

                    stk.IRR_GD_GRID_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_GD_GRID_QTY"]);
                    //stk.OPPENING_BAL_QTY = stk.OP_IRR_QTY - stk.OP_ITC_QTY;

                    stk.ITC_GD_GRID_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_GD_GRID_QTY"]);
                    // Conversion.DBNullDecimalToZero(dRow["ITC_GD_GRID_BAL_QTY"]);
                    stk.OP_REJ_GRID_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_REJ_GRID_IRR_QTY"]);

                    //here total IRR quantity
                    //stk.IRR_BAL_QTY = stk.IRR_DEPT_QTY + stk.IRR_STORE_QTY + stk.IRR_PROD_QTY;
                    //stk.IRR_BAL_QTY = stk.IRR_PROD_QTY;

                    stk.OP_REJ_GRID_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_REJ_GRID_ITC_QTY"]);
                    stk.OP_REJ_GRID_BAL_QTY = stk.OP_REJ_GRID_IRR_QTY - stk.OP_REJ_GRID_ITC_QTY;// Conversion.DBNullDecimalToZero(dRow["OP_REJ_GRID_BAL_QTY"]);

                    stk.IRR_REJ_GRID_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_REJ_GRID_QTY"]);
                    stk.ITC_REJ_GRID_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_REJ_GRID_QTY"]);
                    stk.ITC_REJ_GRID_BAL_QTY = stk.OP_REJ_GRID_BAL_QTY + stk.IRR_REJ_GRID_QTY - stk.ITC_REJ_GRID_QTY;// Conversion.DBNullDecimalToZero(dRow["ITC_REJ_GRID_BAL_QTY"]);
                    //stk.ITEM_STANDARD_WEIGHT_KG = Conversion.DBNullDecimalToZero(dRow["ITEM_STANDARD_WEIGHT_KG"]);
                    stk.ISSUE_TO_STORE_REJ_GRID = Conversion.DBNullDecimalToZero(dRow["ISSUE_TO_STORE_REJ_GRID"]);

                    stk.ITC_GD_GRID_BAL_QTY = stk.OP_GD_GRID_BAL + stk.IRR_GD_GRID_QTY - stk.ITC_GD_GRID_QTY - stk.IRR_REJ_GRID_QTY;

                    stk.TOTAL_GOOD_GRID_RCV_QTY = stk.OP_GD_GRID_BAL + stk.IRR_GD_GRID_QTY;
                    stk.TOTAL_REJ_GRID_RCV_QTY = stk.OP_REJ_GRID_BAL_QTY + stk.IRR_REJ_GRID_QTY;
                    stk.TOTAL_REJ_GRID_WET = Conversion.DBNullDecimalToZero(dRow["TOTAL_REJ_GRID_WET"]); ;
                    //if (stk.COLISING_QTY>0) stk.ITEM_STANDARD_WEIGHT_KG * stk.TOTAL_REJ_GRID_RCV_QTY
                    cRptList.Add(stk);





                    //if (stk.OPPENING_BAL_QTY == 0)
                    //{
                    //    if (stk.IRR_BAL_QTY != 0 || stk.ITC_BAL_QTY != 0 || stk.COLISING_QTY != 0)
                    //    {
                    //        cRptList.Add(stk);
                    //    }
                    //}
                    //else
                    //{
                    //    cRptList.Add(stk);
                    //}
                }




            }
            //catch { throw; }
            //finally 
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }

            return cRptList;

        }

        #endregion**********************************************************************************************************************************


        #region ************************************** Formation Summary Report ************************************************

        public static List<rcFormationProductionSummary> Formation_Production_Summary_Report(clsPrmInventory prmINV, DBContext dc)
        {
            List<rcFormationProductionSummary> cRptList = new List<rcFormationProductionSummary>();
            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                prmINV.DeptID = 11;
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();

                
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.fromProdDate);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.toProdDate);
               
                cmdInfo.DBParametersInfo.Add(":p_itemtype_id", prmINV.itemtype_id);
                cmdInfo.DBParametersInfo.Add(":p_itemGroup_id", prmINV.itemGroup_id);
                cmdInfo.DBParametersInfo.Add(":p_item_id", prmINV.item_id);
                cmdInfo.DBParametersInfo.Add(":p_store_id", prmINV.store_id);
                cmdInfo.DBParametersInfo.Add(":p_itemclass_id", prmINV.ItemClassId);
                cmdInfo.DBParametersInfo.Add(":p_battery_type_id", prmINV.Battery_Type_ID);
                cmdInfo.DBParametersInfo.Add(":p_is_repair", prmINV.Is_Repair);
                cmdInfo.DBParametersInfo.Add(":p_dept_id", prmINV.DeptID);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;
                cmdInfo.CommandText = "SP_FORMATION_PROD_SUMMARY";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);

                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;
                //sb.Append(" Select ITEM_GROUP_ID,ITEM_GROUP_NAME,ITEM_ID,ITEM_NAME,UOM_ID,UOM_NAME,ITEM_TYPE_ID,ITEM_TYPE_NAME,ITEM_CLASS_ID,ITEM_CLASS_NAME,OP_F_IRR_QTY,OP_F_ITC_QTY,OP_UF_IRR_QTY,OP_UF_ITC_QTY,UF_IRR_QTY,UF_ITC_QTY,IRR_DEPT_QTY,IRR_STORE_QTY,IRR_PROD_QTY,ITC_DEPT_QTY,ITC_STORE_QTY,ITC_PROD_QTY,ADJUST_QTY,RCV_FROM_IB,RCV_FROM_PASTING,REJECT_QTY,OP_TOTAL_RCV_QTY,WIP_QTY,OP_LOAD_QTY,OP_UN_LOAD_QTY,OP_TOTAL_REJECT_QTY");
                //sb.Append(" ,ITEM_ORDER,ISSUE_TO_ASSEMBLY,  0   REJECT_PRT,OP_WIP_ADJUST_QTY,OP_F_ADJUST_QTY,OP_UF_ADJUST_QTY,CUR_F_ADJ_QTY,CUR_U_ADJ_QTY,CUR_WIP_ADJ_QTY,GRID_PC_KG_STD,PASTE_PC_KG_STD ");
                //sb.Append("  FROM TEMP_FORMATION_ITEM_STOCK ");
//SUM(NVL(REJECT_QTY,0)* ( NVL(GRID_PC_KG_STD,0)+ NVL(PASTE_PC_KG_STD,0) ))

sb.Append(" Select 'Format' DEPARTMENT_NAME,  ");
sb.Append(" SUM(OP_F_IRR_QTY)OP_F_IRR_QTY, SUM(OP_F_ITC_QTY) OP_F_ITC_QTY, SUM(OP_UF_IRR_QTY) OP_UF_IRR_QTY ");
sb.Append(" ,SUM(OP_UF_ITC_QTY)OP_UF_ITC_QTY,SUM(UF_IRR_QTY)UF_IRR_QTY,SUM(UF_ITC_QTY)UF_ITC_QTY,SUM(IRR_DEPT_QTY)IRR_DEPT_QTY,SUM(IRR_STORE_QTY)IRR_STORE_QTY,SUM(IRR_PROD_QTY)IRR_PROD_QTY,SUM(ITC_DEPT_QTY)ITC_DEPT_QTY,SUM(ITC_STORE_QTY)ITC_STORE_QTY ");
sb.Append(" ,SUM(ITC_PROD_QTY)ITC_PROD_QTY,SUM(ADJUST_QTY)ADJUST_QTY,SUM(RCV_FROM_IB)RCV_FROM_IB,SUM(RCV_FROM_PASTING)RCV_FROM_PASTING,SUM(REJECT_QTY)REJECT_QTY,SUM(OP_TOTAL_RCV_QTY)OP_TOTAL_RCV_QTY,SUM(WIP_QTY)WIP_QTY,SUM(OP_LOAD_QTY)OP_LOAD_QTY ");
sb.Append(" ,SUM(OP_UN_LOAD_QTY)OP_UN_LOAD_QTY,SUM(OP_TOTAL_REJECT_QTY )OP_TOTAL_REJECT_QTY ");
sb.Append(" ,SUM(ISSUE_TO_ASSEMBLY)ISSUE_TO_ASSEMBLY,   SUM(OP_WIP_ADJUST_QTY)OP_WIP_ADJUST_QTY,SUM(OP_F_ADJUST_QTY)OP_F_ADJUST_QTY,SUM(OP_UF_ADJUST_QTY)OP_UF_ADJUST_QTY,SUM(CUR_F_ADJ_QTY)CUR_F_ADJ_QTY,SUM(CUR_U_ADJ_QTY)CUR_U_ADJ_QTY,SUM(CUR_WIP_ADJ_QTY)CUR_WIP_ADJ_QTY ");
sb.Append(" ,  FN_GET_REJECT_GRID_WT_PC('" + prmINV.fromProdDate + "','" + prmINV.toProdDate + "',11,'WT') TOTAL_REJECT_WEIGHT ");
sb.Append(" FROM TEMP_FORMATION_ITEM_STOCK  ");


                DBQuery dbqtemp = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);

                foreach (DataRow dRow in dtData.Rows)
                {
                    rcFormationProductionSummary stk = new rcFormationProductionSummary();
                    stk.DEPARTMENT_NAME = dRow["DEPARTMENT_NAME"].ToString();
                    //stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    //stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    //stk.ITEM_ORDER = Conversion.DBNullIntToZero(dRow["ITEM_ORDER"]);
                    //stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    //stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();

                    //stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    //stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    //stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);

                    //stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                    //stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    //stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();

                    //opening Part
                    stk.OP_TOTAL_REJECT_QTY = Conversion.DBNullDecimalToZero(dRow["OP_TOTAL_REJECT_QTY"]);
                    stk.OP_TOTAL_RCV_QTY = Conversion.DBNullDecimalToZero(dRow["OP_TOTAL_RCV_QTY"]);

                    stk.OP_F_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_F_IRR_QTY"]);
                    stk.OP_F_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_F_ITC_QTY"]);
                    stk.OP_F_ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["OP_F_ADJUST_QTY"]);

                    stk.OP_F_BAL = stk.OP_F_IRR_QTY + stk.OP_F_ADJUST_QTY - stk.OP_F_ITC_QTY;

                    stk.OP_UF_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_UF_IRR_QTY"]);
                    stk.OP_UF_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_UF_ITC_QTY"]);
                    stk.OP_UF_ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["OP_UF_ADJUST_QTY"]);
                    stk.OP_UF_BAL = stk.OP_UF_IRR_QTY + stk.OP_UF_ADJUST_QTY - stk.OP_UF_ITC_QTY;

                    //Transaction within date range

                    //Un formed plate receive
                    stk.UF_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["UF_IRR_QTY"]);

                    //Unform plate reuse
                    stk.UF_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["UF_ITC_QTY"]);

                    //Normal plate receive from IB and Pasting
                    stk.RCV_FROM_IB = Conversion.DBNullDecimalToZero(dRow["RCV_FROM_IB"]);
                    stk.RCV_FROM_PASTING = Conversion.DBNullDecimalToZero(dRow["RCV_FROM_PASTING"]);

                    //Formed Plate Transaction
                    stk.IRR_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_DEPT_QTY"]);
                    stk.IRR_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_STORE_QTY"]);
                    stk.IRR_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_PROD_QTY"]);
                    stk.IRR_BAL_QTY = stk.IRR_DEPT_QTY + stk.IRR_STORE_QTY + stk.IRR_PROD_QTY;

                    //Formed Plate Issue
                    stk.ITC_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_DEPT_QTY"]);
                    stk.ITC_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_STORE_QTY"]);
                    stk.ITC_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_PROD_QTY"]);
                    stk.ISS_TO_ASSEMBLY = Conversion.DBNullDecimalToZero(dRow["ISSUE_TO_ASSEMBLY"]);
                    //Fomrd Balance Quantity                   
                    stk.ITC_BAL_QTY = stk.ITC_DEPT_QTY + stk.ITC_STORE_QTY + stk.ITC_PROD_QTY;

                    //here total Adjust balance quantity
                    stk.ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);
                    stk.REJECT_QTY = Conversion.DBNullDecimalToZero(dRow["REJECT_QTY"]);

                    stk.CUR_F_ADJ_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_F_ADJ_QTY"]);
                    stk.CUR_U_ADJ_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_U_ADJ_QTY"]);
                    stk.CUR_WIP_ADJ_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_WIP_ADJ_QTY"]);

                    //if (stk.IRR_PROD_QTY > 0)
                    //    stk.REJECT_PRT = Conversion.DBNullDecimalToZero((stk.REJECT_QTY * 100) / (stk.IRR_PROD_QTY));
                    //else
                    //    stk.REJECT_PRT = 0;

                    if (stk.ADJUST_QTY > 0)
                    {
                        stk.IRR_BAL_QTY = stk.IRR_BAL_QTY + stk.ADJUST_QTY;
                    }
                    else
                    {
                        stk.ITC_BAL_QTY = stk.ITC_BAL_QTY + Math.Abs(stk.ADJUST_QTY);
                    }

                    //Opening WIP=WIP Adjustment+Total Receive-Total Production Declaration(Formed,Unformed,Rejection)

                    stk.OP_WIP_ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["OP_WIP_ADJUST_QTY"]);

                    stk.OP_WIP = stk.OP_TOTAL_RCV_QTY + stk.OP_WIP_ADJUST_QTY - (stk.OP_F_IRR_QTY + stk.OP_F_ADJUST_QTY + stk.OP_UF_BAL + stk.OP_TOTAL_REJECT_QTY);

                    stk.OPPENING_BAL_QTY = stk.OP_WIP + stk.OP_F_BAL + stk.OP_UF_BAL;

                    stk.UNFORMED_BALANCE_QTY = stk.OP_UF_BAL + stk.UF_IRR_QTY + stk.CUR_U_ADJ_QTY - stk.UF_ITC_QTY;
                    stk.FORMED_BALANCE_QTY = stk.OP_F_BAL + stk.IRR_PROD_QTY + stk.CUR_F_ADJ_QTY - stk.ISS_TO_ASSEMBLY;
                    stk.WIP_QTY = (stk.OPPENING_BAL_QTY + stk.RCV_FROM_IB + stk.RCV_FROM_PASTING) - (stk.IRR_PROD_QTY + stk.CUR_F_ADJ_QTY + stk.REJECT_QTY + stk.UNFORMED_BALANCE_QTY);

                    //this is previous closing correct
                    //stk.COLISING_QTY = stk.WIP_QTY + stk.FORMED_BALANCE_QTY + stk.UNFORMED_BALANCE_QTY;
                    // stk.NEW_WIP_QTY = stk.WIP_QTY - stk.OP_F_BAL;
                    stk.COLISING_QTY = stk.WIP_QTY + stk.FORMED_BALANCE_QTY + stk.UNFORMED_BALANCE_QTY - stk.OP_F_BAL;

                    stk.NEW_COLISING_QTY = stk.OPPENING_BAL_QTY + stk.RCV_FROM_IB + stk.RCV_FROM_PASTING - stk.ISS_TO_ASSEMBLY - stk.REJECT_QTY;
                    //stk.PASTE_PC_KG_STD = Conversion.DBNullDecimalToZero(dRow["PASTE_PC_KG_STD"]);
                    //stk.GRID_PC_KG_STD = Conversion.DBNullDecimalToZero(dRow["GRID_PC_KG_STD"]);
                    stk.TOTAL_REJECT_WEIGHT = Conversion.DBNullDecimalToZero(dRow["TOTAL_REJECT_WEIGHT"]);

                    cRptList.Add(stk);
                }
            }
            //catch { throw; }
            //finally 
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }

            return cRptList;

        }

        #endregion**************************************************************************************************************

        #region *************************************** Assemble Plate Summary Report***********************************
        public static List<rcAssemblyFinishedStock> AssemblyUsePlateReport(clsPrmInventory prmINV)
        {
            return AssemblyUsePlateReport(prmINV, null);
        }

        public static List<rcAssemblyFinishedStock> AssemblyUsePlateReport(clsPrmInventory prmINV, DBContext dc)
        {

            List<rcAssemblyFinishedStock> cRptList = new List<rcAssemblyFinishedStock>();
            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();

                //if (prmINV.ToDate.HasValue)
                //{
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.fromProdDate);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.toProdDate);
                //}
                cmdInfo.DBParametersInfo.Add(":p_itemtype_id", prmINV.itemtype_id);
                cmdInfo.DBParametersInfo.Add(":p_itemGroup_id", prmINV.itemGroup_id);
                cmdInfo.DBParametersInfo.Add(":p_item_id", prmINV.item_id);
                cmdInfo.DBParametersInfo.Add(":p_store_id", prmINV.store_id);
                cmdInfo.DBParametersInfo.Add(":p_itemclass_id", prmINV.ItemClassId);
                cmdInfo.DBParametersInfo.Add(":p_battery_type_id", prmINV.Battery_Type_ID);
                cmdInfo.DBParametersInfo.Add(":p_is_repair", prmINV.Is_Repair);
                cmdInfo.DBParametersInfo.Add(":p_dept_id", 136);
                cmdInfo.DBParametersInfo.Add(":p_is_finished", "Y");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = "SP_ASSEM_MONTH_PLATE_INV_RT";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);

                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;
                
                sb.Append( " SELECT  ");
                sb.Append(" 'Assembly' DEPARTMENT_NAME ");
                sb.Append( ",SUM(NVL( OP_GD_PLATE_IRR_QTY  ,0)) OP_GD_PLATE_IRR_QTY ");
                sb.Append( ",SUM(NVL( OP_GD_PLATE_ITC_QTY  ,0)) OP_GD_PLATE_ITC_QTY ");
                sb.Append( ",SUM(NVL( OP_GD_PLATE_BAL  ,0)) OP_GD_PLATE_BAL ");
                sb.Append( ",SUM(NVL( IRR_GD_PLATE_QTY  ,0)) IRR_GD_PLATE_QTY ");
                sb.Append( ",SUM(NVL( ITC_GD_PLATE_QTY  ,0)) ITC_GD_PLATE_QTY ");
                sb.Append( ",SUM(NVL( ITC_GD_PLATE_BAL_QTY  ,0)) ITC_GD_PLATE_BAL_QTY ");
                sb.Append( ",SUM(NVL( OP_REC_PLATE_IRR_QTY  ,0)) OP_REC_PLATE_IRR_QTY ");
                sb.Append( ",SUM(NVL(  OP_REC_PLATE_ITC_QTY ,0)) OP_REC_PLATE_ITC_QTY ");
                sb.Append( ",SUM(NVL(  OP_REC_PLATE_BAL_QTY ,0)) OP_REC_PLATE_BAL_QTY ");
                sb.Append( ",SUM(NVL(  IRR_REC_PLATE_QTY ,0)) IRR_REC_PLATE_QTY ");
                sb.Append( ",SUM(NVL( ITC_REC_PLATE_QTY  ,0)) ITC_REC_PLATE_QTY ");
                sb.Append( ",SUM(NVL(  ITC_REC_BAL_QTY ,0)) ITC_REC_BAL_QTY ");
                sb.Append( ",SUM(NVL( OP_REJ_PLATE_IRR_QTY  ,0)) OP_REJ_PLATE_IRR_QTY ");
                sb.Append( ",SUM(NVL(  OP_REJ_PLATE_ITC_QTY ,0)) OP_REJ_PLATE_ITC_QTY ");
                sb.Append( ",SUM(NVL(  OP_REJ_PLATE_BAL_QTY ,0)) OP_REJ_PLATE_BAL_QTY ");
                sb.Append( ",SUM(NVL(  IRR_REJ_PLATE_QTY ,0)) IRR_REJ_PLATE_QTY ");
                sb.Append( ",SUM(NVL(  ITC_REJ_PLATE_QTY ,0)) ITC_REJ_PLATE_QTY ");
                sb.Append( ",SUM(NVL(   ITC_REJ_BAL_QTY,0)) ITC_REJ_BAL_QTY ");
                sb.Append( ",SUM(NVL(  OP_GD_PLATE_ADJ_QTY ,0)) OP_GD_PLATE_ADJ_QTY ");
                sb.Append( ",SUM(NVL( OP_REC_PLATE_ADJ_QTY  ,0)) OP_REC_PLATE_ADJ_QTY ");
                sb.Append( ",SUM(NVL( OP_REJ_PLATE_ADJ_QTY  ,0)) OP_REJ_PLATE_ADJ_QTY ");
                sb.Append(" ,SUM(NVL( ITC_REJ_STO_PLATE_QTY  ,0)) ITC_REJ_STO_PLATE_QTY  ");
                sb.Append(",FN_GET_REJECT_GRID_WT_PC('" + prmINV.fromProdDate + "','" + prmINV.toProdDate + "',136,'WT')  REJ_STOCK_QTY ");
                sb.Append( ",SUM(NVL(   OP_REJ_STO_PLATE_QTY,0)) OP_REJ_STO_PLATE_QTY ");
                sb.Append("FROM TEMP_ASS_PLATE_STOCK ");
 

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
                    rcAssemblyFinishedStock stk = new rcAssemblyFinishedStock();
                    //stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    //stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    //stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    //stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();

                    //stk.ITEM_ORDER = Conversion.DBNullIntToZero(dRow["ITEM_ORDER"]);
                    //stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    //stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    //stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);

                    //stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                    //stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    //stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();
                    stk.DEPARTMENT_NAME = dRow["DEPARTMENT_NAME"].ToString();
                    stk.OP_GD_PLATE_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_GD_PLATE_IRR_QTY"]);
                    stk.OP_GD_PLATE_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_GD_PLATE_ITC_QTY"]);
                    //stk.OP_GD_PLATE_ADJ_QTY = Conversion.DBNullDecimalToZero(dRow["OP_GD_PLATE_ADJ_QTY"]);
                    stk.OP_GD_PLATE_BAL = Conversion.DBNullDecimalToZero(dRow["OP_GD_PLATE_BAL"]);

                    stk.IRR_GD_PLATE_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_GD_PLATE_QTY"]);
                    stk.ITC_GD_PLATE_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_GD_PLATE_QTY"]);
                    stk.ITC_GD_PLATE_BAL_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_GD_PLATE_BAL_QTY"]);
                    stk.TOTAL_GD_PLATE_RCV_QTY = stk.OP_GD_PLATE_BAL + stk.IRR_GD_PLATE_QTY;

                    stk.OP_REC_PLATE_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_REC_PLATE_IRR_QTY"]);
                    stk.OP_REC_PLATE_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_REC_PLATE_ITC_QTY"]);
                    stk.OP_REC_PLATE_ADJ_QTY = Conversion.DBNullDecimalToZero(dRow["OP_REC_PLATE_ADJ_QTY"]);
                    stk.OP_REC_PLATE_BAL_QTY = Conversion.DBNullDecimalToZero(dRow["OP_REC_PLATE_BAL_QTY"]);


                    stk.IRR_REC_PLATE_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_REC_PLATE_QTY"]);
                    stk.ITC_REC_PLATE_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_REC_PLATE_QTY"]);
                    stk.ITC_REC_BAL_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_REC_BAL_QTY"]);

                    stk.TOTAL_REC_PLATE_RCV_QTY = stk.OP_REC_PLATE_BAL_QTY + stk.IRR_REC_PLATE_QTY;


                    stk.TOTAL_REC_REMAIN_QTY = stk.TOTAL_REC_PLATE_RCV_QTY - stk.ITC_REC_PLATE_QTY;

                    stk.OP_REJ_PLATE_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_REJ_PLATE_IRR_QTY"]);
                    stk.OP_REJ_PLATE_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_REJ_PLATE_ITC_QTY"]);
                    stk.OP_REJ_PLATE_ADJ_QTY = Conversion.DBNullDecimalToZero(dRow["OP_REJ_PLATE_ADJ_QTY"]);
                    stk.OP_REJ_PLATE_BAL_QTY = Conversion.DBNullDecimalToZero(dRow["OP_REJ_PLATE_BAL_QTY"]);

                    stk.IRR_REJ_PLATE_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_REJ_PLATE_QTY"]);//Here Packing quantity receive from production
                    stk.ITC_REJ_PLATE_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_REJ_PLATE_QTY"]);
                    stk.ITC_REJ_BAL_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_REJ_BAL_QTY"]);
                    stk.TOTAL_REJ_PLATE_RCV_QTY = stk.OP_REJ_PLATE_BAL_QTY + stk.IRR_REJ_PLATE_QTY;
                    stk.ITC_REJ_STO_PLATE_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_REJ_STO_PLATE_QTY"]);
                    stk.TOTAL_REJ_REMAIN_QTY = stk.TOTAL_REJ_PLATE_RCV_QTY - stk.IRR_REC_PLATE_QTY - stk.ITC_REJ_STO_PLATE_QTY;
                    // stk.TOTAL_REJ_REMAIN_QTY = stk.IRR_REJ_PLATE_QTY - stk.IRR_REC_PLATE_QTY;
                    stk.REJ_STOCK_QTY = Conversion.DBNullDecimalToZero(dRow["REJ_STOCK_QTY"]);
                    stk.TOTAL_PLATE_CONS_AS_PROD_QTY = stk.ITC_REC_PLATE_QTY + stk.ITC_GD_PLATE_QTY;

                    // stk.TOTAL_PLATE_CONS_WITH_REJ_QTY = stk.ITC_GD_PLATE_QTY + stk.TOTAL_REJ_PLATE_RCV_QTY;
                    stk.TOTAL_PLATE_CONS_WITH_REJ_QTY = stk.ITC_GD_PLATE_QTY + stk.IRR_REJ_PLATE_QTY;

                    stk.TOTAL_WIP_REMAIN_QTY = stk.TOTAL_GD_PLATE_RCV_QTY - (stk.IRR_REJ_PLATE_QTY + stk.ITC_GD_PLATE_QTY);

                    // + stk.ITC_REC_PLATE_QTY;

                    //if (stk.OP_PACKING_BAL_QTY > 0 || stk.OP_ASSEMBEL_BAL_QTY > 0 || stk.CUR_PACKING_RCV_QTY > 0 || stk.CUR_ASSEM_PROD_QTY > 0 || stk.ITC_SOLAR_QTY > 0 || stk.ITC_STORE_QTY > 0 || stk.OP_CUR_TOT_PACKING_QTY > 0 || stk.OP_CUR_TOT_ASSEMBLY_QTY > 0)
                    cRptList.Add(stk);
                }

            }
            //catch { throw; }
            //finally 
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }

            return cRptList;

        }



        public static List<rcAssemblyFinishedStock> VRLA_AssemblyUsePlateReport(clsPrmInventory prmINV, DBContext dc)
        {

            List<rcAssemblyFinishedStock> cRptList = new List<rcAssemblyFinishedStock>();
            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();


                cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.fromProdDate);
                cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.toProdDate);
               
                cmdInfo.DBParametersInfo.Add(":p_itemtype_id", prmINV.itemtype_id);
                cmdInfo.DBParametersInfo.Add(":p_itemGroup_id", prmINV.itemGroup_id);
                cmdInfo.DBParametersInfo.Add(":p_item_id", prmINV.item_id);
                cmdInfo.DBParametersInfo.Add(":p_store_id", prmINV.store_id);
                cmdInfo.DBParametersInfo.Add(":p_itemclass_id", prmINV.ItemClassId);
                cmdInfo.DBParametersInfo.Add(":p_battery_type_id", prmINV.Battery_Type_ID);
                cmdInfo.DBParametersInfo.Add(":p_is_repair", prmINV.Is_Repair);
                cmdInfo.DBParametersInfo.Add(":p_dept_id", 140);
                cmdInfo.DBParametersInfo.Add(":p_is_finished", "Y");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = "SP_VRLA_MONTH_PLATE_INV_RT";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);
               
                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;
                //sb.Append(" Select * FROM TEMP_ASS_PLATE_STOCK");

                sb.Append(" SELECT  ");
                sb.Append(" 'VRLA' DEPARTMENT_NAME ");
                sb.Append(" ,SUM(NVL( OP_GD_PLATE_IRR_QTY  ,0)) OP_GD_PLATE_IRR_QTY ");
                sb.Append(" ,SUM(NVL( OP_GD_PLATE_ITC_QTY  ,0)) OP_GD_PLATE_ITC_QTY ");
                sb.Append(" ,SUM(NVL( OP_GD_PLATE_BAL  ,0)) OP_GD_PLATE_BAL ");
                sb.Append(" ,SUM(NVL( IRR_GD_PLATE_QTY  ,0)) IRR_GD_PLATE_QTY ");
                sb.Append(" ,SUM(NVL( ITC_GD_PLATE_QTY  ,0)) ITC_GD_PLATE_QTY ");
                sb.Append(" ,SUM(NVL( ITC_GD_PLATE_BAL_QTY  ,0)) ITC_GD_PLATE_BAL_QTY ");
                sb.Append(" ,SUM(NVL( OP_REC_PLATE_IRR_QTY  ,0)) OP_REC_PLATE_IRR_QTY ");
                sb.Append(" ,SUM(NVL(  OP_REC_PLATE_ITC_QTY ,0)) OP_REC_PLATE_ITC_QTY ");
                sb.Append(" ,SUM(NVL(  OP_REC_PLATE_BAL_QTY ,0)) OP_REC_PLATE_BAL_QTY ");
                sb.Append(" ,SUM(NVL(  IRR_REC_PLATE_QTY ,0)) IRR_REC_PLATE_QTY ");
                sb.Append(" ,SUM(NVL( ITC_REC_PLATE_QTY  ,0)) ITC_REC_PLATE_QTY ");
                sb.Append(" ,SUM(NVL(  ITC_REC_BAL_QTY ,0)) ITC_REC_BAL_QTY ");
                sb.Append(" ,SUM(NVL( OP_REJ_PLATE_IRR_QTY  ,0)) OP_REJ_PLATE_IRR_QTY ");
                sb.Append(" ,SUM(NVL(  OP_REJ_PLATE_ITC_QTY ,0)) OP_REJ_PLATE_ITC_QTY ");
                sb.Append(" ,SUM(NVL(  OP_REJ_PLATE_BAL_QTY ,0)) OP_REJ_PLATE_BAL_QTY ");
                sb.Append(" ,SUM(NVL(  IRR_REJ_PLATE_QTY ,0)) IRR_REJ_PLATE_QTY ");
                sb.Append(" ,SUM(NVL(  ITC_REJ_PLATE_QTY ,0)) ITC_REJ_PLATE_QTY ");
                sb.Append(" ,SUM(NVL(   ITC_REJ_BAL_QTY,0)) ITC_REJ_BAL_QTY ");
                sb.Append(" ,SUM(NVL(  OP_GD_PLATE_ADJ_QTY ,0)) OP_GD_PLATE_ADJ_QTY ");
                sb.Append(" ,SUM(NVL( OP_REC_PLATE_ADJ_QTY  ,0)) OP_REC_PLATE_ADJ_QTY ");
                sb.Append(" ,SUM(NVL( OP_REJ_PLATE_ADJ_QTY  ,0)) OP_REJ_PLATE_ADJ_QTY ");
                sb.Append(" ,SUM(NVL( ITC_REJ_STO_PLATE_QTY  ,0)) ITC_REJ_STO_PLATE_QTY  ");
                sb.Append(" ,FN_GET_REJECT_GRID_WT_PC('" + prmINV.fromProdDate + "','" + prmINV.toProdDate + "',140,'WT')  REJ_STOCK_QTY ");
                sb.Append(" ,SUM(NVL(   OP_REJ_STO_PLATE_QTY,0)) OP_REJ_STO_PLATE_QTY ");
                sb.Append(" FROM TEMP_ASS_PLATE_STOCK ");

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
                    rcAssemblyFinishedStock stk = new rcAssemblyFinishedStock();
                    //stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    //stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    //stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    //stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();

                    //stk.ITEM_ORDER = Conversion.DBNullIntToZero(dRow["ITEM_ORDER"]);
                    //stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    //stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    //stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);

                    //stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                    //stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    //stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();
                    stk.DEPARTMENT_NAME = dRow["DEPARTMENT_NAME"].ToString();
                    stk.OP_GD_PLATE_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_GD_PLATE_IRR_QTY"]);
                    stk.OP_GD_PLATE_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_GD_PLATE_ITC_QTY"]);
                    //stk.OP_GD_PLATE_ADJ_QTY = Conversion.DBNullDecimalToZero(dRow["OP_GD_PLATE_ADJ_QTY"]);
                    stk.OP_GD_PLATE_BAL = Conversion.DBNullDecimalToZero(dRow["OP_GD_PLATE_BAL"]);

                    stk.IRR_GD_PLATE_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_GD_PLATE_QTY"]);
                    stk.ITC_GD_PLATE_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_GD_PLATE_QTY"]);
                    stk.ITC_GD_PLATE_BAL_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_GD_PLATE_BAL_QTY"]);
                    stk.TOTAL_GD_PLATE_RCV_QTY = stk.OP_GD_PLATE_BAL + stk.IRR_GD_PLATE_QTY;

                    stk.OP_REC_PLATE_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_REC_PLATE_IRR_QTY"]);
                    stk.OP_REC_PLATE_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_REC_PLATE_ITC_QTY"]);
                    stk.OP_REC_PLATE_ADJ_QTY = Conversion.DBNullDecimalToZero(dRow["OP_REC_PLATE_ADJ_QTY"]);
                    stk.OP_REC_PLATE_BAL_QTY = Conversion.DBNullDecimalToZero(dRow["OP_REC_PLATE_BAL_QTY"]);


                    stk.IRR_REC_PLATE_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_REC_PLATE_QTY"]);
                    stk.ITC_REC_PLATE_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_REC_PLATE_QTY"]);
                    stk.ITC_REC_BAL_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_REC_BAL_QTY"]);

                    stk.TOTAL_REC_PLATE_RCV_QTY = stk.OP_REC_PLATE_BAL_QTY + stk.IRR_REC_PLATE_QTY;


                    stk.TOTAL_REC_REMAIN_QTY = stk.TOTAL_REC_PLATE_RCV_QTY - stk.ITC_REC_PLATE_QTY;

                    stk.OP_REJ_PLATE_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_REJ_PLATE_IRR_QTY"]);
                    stk.OP_REJ_PLATE_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_REJ_PLATE_ITC_QTY"]);
                    stk.OP_REJ_PLATE_ADJ_QTY = Conversion.DBNullDecimalToZero(dRow["OP_REJ_PLATE_ADJ_QTY"]);
                    stk.OP_REJ_PLATE_BAL_QTY = Conversion.DBNullDecimalToZero(dRow["OP_REJ_PLATE_BAL_QTY"]);

                    stk.REJ_STOCK_QTY = Conversion.DBNullDecimalToZero(dRow["REJ_STOCK_QTY"]);

                    stk.IRR_REJ_PLATE_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_REJ_PLATE_QTY"]);//Here Packing quantity receive from production
                    stk.ITC_REJ_PLATE_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_REJ_PLATE_QTY"]);
                    stk.ITC_REJ_BAL_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_REJ_BAL_QTY"]);
                    stk.ITC_REJ_STO_PLATE_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_REJ_STO_PLATE_QTY"]);
                    stk.TOTAL_REJ_PLATE_RCV_QTY = stk.OP_REJ_PLATE_BAL_QTY + stk.IRR_REJ_PLATE_QTY;

                    stk.TOTAL_REJ_REMAIN_QTY = stk.TOTAL_REJ_PLATE_RCV_QTY - stk.ITC_REJ_PLATE_QTY - stk.ITC_REJ_STO_PLATE_QTY;

                    stk.TOTAL_PLATE_CONS_AS_PROD_QTY = stk.ITC_REC_PLATE_QTY + stk.ITC_GD_PLATE_QTY;

                    stk.TOTAL_PLATE_CONS_WITH_REJ_QTY = stk.ITC_GD_PLATE_QTY + stk.TOTAL_REJ_PLATE_RCV_QTY;

                    stk.TOTAL_WIP_REMAIN_QTY = stk.TOTAL_GD_PLATE_RCV_QTY - (stk.IRR_REJ_PLATE_QTY + stk.ITC_GD_PLATE_QTY);

                    // + stk.ITC_REC_PLATE_QTY;

                    //if (stk.OP_PACKING_BAL_QTY > 0 || stk.OP_ASSEMBEL_BAL_QTY > 0 || stk.CUR_PACKING_RCV_QTY > 0 || stk.CUR_ASSEM_PROD_QTY > 0 || stk.ITC_SOLAR_QTY > 0 || stk.ITC_STORE_QTY > 0 || stk.OP_CUR_TOT_PACKING_QTY > 0 || stk.OP_CUR_TOT_ASSEMBLY_QTY > 0)
                    cRptList.Add(stk);
                }

            }
            //catch { throw; }
            //finally 
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }

            return cRptList;

        }
        #endregion******************************************************************************************************

        #region ***************************************Battery Monthly Production *******************************************************************
        public static List<dcMaterialStock> GetMonthlyBatteryProduction(clsPrmInventory prmINV)
        {
            return GetMonthlyBatteryProduction(prmINV, null);
        }

        //static string ConvertStringArrayToString(string[] array)
        //{
        //    // Concatenate all the elements into a StringBuilder.
        //    StringBuilder builder = new StringBuilder();
        //    foreach (string value in array)
        //    {
        //        builder.Append(value);
        //        builder.Append("','");
        //    }
        //    return builder.ToString();
        //}

        public static List<dcMaterialStock> GetMonthlyBatteryProduction(clsPrmInventory prmINV, DBContext dc)
        {
            List<dcMaterialStock> cRptList = new List<dcMaterialStock>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();
                DBQuery dbq = new DBQuery();
                sb.Length = 0;
                cmdInfo.DBParametersInfo.Clear();

                sb.Append(" SELECT  ");
                sb.Append(" TO_CHAR(GC_ENTRY_MST.PROD_DATE, 'YYYYMM') pMonthYear ,TO_CHAR(GC_ENTRY_MST.PROD_DATE, 'MON-YYYY') pMonth  ");
                sb.Append(" ,BATERY_CATEGORY.BATERY_CAT_DESCR ,  SALES_ITEM_MST.ITEM_DESC");
                sb.Append(" , COUNT( GC_ENTRY_DTL.SL_NO) QTY ");
                sb.Append(" FROM sdndms.GC_ENTRY_DTL ");
                sb.Append(" INNER JOIN sdndms.GC_ENTRY_MST ON GC_ENTRY_DTL.ENTRY_NO = GC_ENTRY_MST.ENTRY_NO ");
                sb.Append(" INNER JOIN sdndms.SALES_ITEM_MST ON GC_ENTRY_DTL.ITEM_CODE = SALES_ITEM_MST.ITEM_CODE ");
                sb.Append(" INNER JOIN sdndms.BATERY_CATEGORY ON SALES_ITEM_MST.BATERY_CAT_ID = BATERY_CATEGORY.BATERY_CAT_ID ");
                sb.Append(" WHERE (1=1) ");
                sb.Append(" AND UPPER(BATERY_CATEGORY.MST_CAT_ID) = 'BAT01' ");
                sb.Append(" AND UPPER(GC_ENTRY_DTL.REMARKS) = UPPER('for Sale') ");
                sb.Append(" AND GC_ENTRY_MST.PROD_DATE    BETWEEN TO_DATE(@p_from_date)   AND TO_DATE(@p_to_date) ");

                cmdInfo.DBParametersInfo.Add("@p_from_date", prmINV.fromProdDate);
                cmdInfo.DBParametersInfo.Add("@p_to_date", prmINV.toProdDate);

                string strList = "0";
                if (prmINV.CatidList.Count > 0)
                {
                    // strList = string.Join(",", prmINV.CatidList.ToArray());
                    strList = string.Join(",", prmINV.CatidList.ToArray());
                    //strList = string.Join(",", prmINV.CatidList.ToArray());
                                       //strList =   strList.Replace(",", "','");
                                        // strList = "'" + strList + "'";
                }

                if (strList != string.Empty && strList != "0")
                {
                    //cmdInfo.DBParametersInfo.Add("@P_CAT", strList);
                    //sb.Append(" AND  BATERY_CATEGORY.BATERY_CAT_ID in (@P_CAT) ");
                    sb.Append(string.Format(" AND BATERY_CATEGORY.BATERY_CAT_ID IN ({0}) ", strList));
                }
                //else
                //{
                //    cmdInfo.DBParametersInfo.Add("@P_CAT", string.Empty);
                //}
                sb.Append("  GROUP BY BATERY_CATEGORY.BATERY_CAT_DESCR,GC_ENTRY_MST.PROD_DATE,SALES_ITEM_MST.ITEM_DESC ");

                //if (prmINV.ToDate.HasValue)
                //{
              
                //}
                //cmdInfo.DBParametersInfo.Add(":p_itemtype_id", prmINV.itemtype_id);
                //cmdInfo.DBParametersInfo.Add(":p_itemGroup_id", prmINV.itemGroup_id);
                //cmdInfo.DBParametersInfo.Add(":p_item_id", prmINV.item_id);
                //cmdInfo.DBParametersInfo.Add(":p_store_id", prmINV.store_id);
                //cmdInfo.DBParametersInfo.Add(":p_itemclass_id", prmINV.ItemClassId);
                //cmdInfo.DBParametersInfo.Add(":p_battery_type_id", prmINV.Battery_Type_ID);
                //cmdInfo.DBParametersInfo.Add(":p_is_repair", prmINV.Is_Repair);
                //cmdInfo.DBParametersInfo.Add("@p_dept_id", 0);
                //cmdInfo.DBParametersInfo.Add(":p_is_finished", prmINV.is_Finished);


             
              
                //dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                //cmdInfo.CommandTimeout = 600;

                //cmdInfo.CommandText = "SP_BATTERY_LEAD_CONSUMPTION";
                //cmdInfo.CommandType = CommandType.StoredProcedure;
                //dbq.DBCommandInfo = cmdInfo;
                //DBQuery.ExecuteDBQuerySP(dbq, dc);
                //DBCommandInfo cmdInfotemp = new DBCommandInfo();

//sb.Append(" SELECT  ");
//sb.Append(" TO_CHAR(mst.PRODUCTION_DATE, 'YYYYMM') pMonthYear ,TO_CHAR(mst.PRODUCTION_DATE, 'MON-YYYY') pMonth ");
//sb.Append(" ,dept.DEPARTMENT_NAME,inv.ITEM_NAME,sum(dtl.ITEM_QTY ) ITEM_QTY ");
//sb.Append(" FROM PRODUCTION_MST mst  ");
//sb.Append(" INNER JOIN PRODUCTION_DTL dtl ON mst.PROD_ID=dtl.PROD_MST_ID ");
//sb.Append(" INNER JOIN INV_ITEM_MASTER inv ON dtl.ITEM_ID=inv.ITEM_ID ");
//sb.Append(" INNER JOIN DEPARTMENT_INFO dept ON dept.DEPARTMENT_ID=mst.DEPT_ID ");
//sb.Append(" WHERE 1=1 ");
//sb.Append(" AND mst.IS_PACKING='N' ");
//sb.Append(" AND mst.DEPT_ID IN (136,140) ");
//sb.Append(" AND mst.PRODUCTION_DATE>='01-APR-2018' ");
//sb.Append(" AND mst.PRODUCTION_DATE<='30-APR-2019' ");
//sb.Append(" AND dtl.ITEM_SPECIFICATION_ID IS NULL ");
//sb.Append(" Group by TO_CHAR(mst.PRODUCTION_DATE, 'MON-YYYY'),inv.ITEM_NAME ,dept.DEPARTMENT_NAME, TO_CHAR(mst.PRODUCTION_DATE, 'YYYYMM') ");

                DBQuery dbqtemp = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);
                foreach (DataRow dRow in dtData.Rows)
                {
                    dcMaterialStock stk = new dcMaterialStock();
                    stk.pMonthYear = dRow["pMonthYear"].ToString();
                    stk.pMonth = dRow["pMonth"].ToString();
                    stk.DEPARTMENT_NAME = dRow["BATERY_CAT_DESCR"].ToString();
                    stk.UOM_NAME = "Pc";
                    stk.ITEM_NAME = dRow["ITEM_DESC"].ToString();
                    stk.ITEM_QTY = Conversion.StringToInt(dRow["QTY"].ToString());
                    cRptList.Add(stk);
                }
            }

            catch { throw; }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }

            return cRptList;

        }
        #endregion
    }
}
