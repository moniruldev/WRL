using PG.BLLibrary.InventoryBL;
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
    public class PRODUCTION_DTLBL
    {

        public static string GetProductionEntryDtlSQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" Select ");
            sb.Append(" dtl.* ");
            sb.Append(" FROM  ");
            sb.Append(" PRODUCTION_DTL dtl ");
            sb.Append(" Where 1=1 ");
            return sb.ToString();
        }

        public static string GetProductionDtlsSQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" Select ");
            sb.Append(" dtl.PROD_MST_ID ");
            sb.Append(" ,dtl.PROD_DTL_ID ");
            sb.Append(" ,dtl.SLNO ");
            sb.Append(" ,inv.ITEM_NAME ");
            sb.Append(" ,inv.ITEM_ID ");
            sb.Append(" ,dtl.PANEL_PC ");
            sb.Append(" ,panel.UOM_NAME PANEL_UOM_NAME ");
            sb.Append(" ,dtl.ITEM_PANEL_QTY ");
            sb.Append(" ,dtl.PANEL_UOM_ID ");
            sb.Append(" ,dtl.ITEM_QTY ");
            sb.Append(" ,dtl.MIXER_BATCH_QTY ");
            sb.Append(" ,itmUOM.UOM_CODE_SHORT UOM_NAME ");
            sb.Append(" ,dtl.UOM_ID ");
            sb.Append(" ,dtl.ITEM_WEIGHT ");
            sb.Append(" ,weightUOM.UOM_CODE_SHORT WEIGHT_UOM_NAME ");
            sb.Append(" ,dtl.WEIGHT_UOM_ID ");
            sb.Append(" ,dtl.MACHINE_ID ");
            sb.Append(" ,mac.MACHINE_NAME ");
            sb.Append(" ,dtl.BOM_ID ");
            sb.Append(" ,bom.BOM_ITEM_DESC BOM_NAME ");
            sb.Append(" ,dtl.REMARKS ");
            sb.Append(" ,dtl.OPERATOR_ID ");
            sb.Append(" ,dtl.USED_BAR_PC ");
            sb.Append(" ,dtl.BAR_TYPE ");
            sb.Append(" ,dtl.USED_QTY_KG ");
            sb.Append(" ,dtl.BAR_WEIGHT ");
            sb.Append(" ,dtl.IS_PACKING,dtl.CHARGED_QTY,dtl.PACKING_QUANTITY ");
            sb.Append(" ,OpMst.FULL_NAME OPERATOR_NAME ");
            sb.Append(" ,bar.UOM_CODE_SHORT BAR_TYPE_NAME ");
            //sb.Append(" ,dtl.GRID_BATCH ");
            sb.Append(" ,dtl.FORMATION_STARTTIME ");
            sb.Append(" ,dtl.FORMATION_OFFTIME ");
            sb.Append(" ,dtl.FORMATION_OFFDATE ");
            sb.Append(" ,dtl.FORMED_QTY ");
            sb.Append(" ,dtl.UNFORMED_QTY ");
            sb.Append(" ,dtl.REJECT_QTY ");
            sb.Append(" ,dtl.AMPERE ");
            sb.Append(" ,dtl.CYCLETIME ");
            sb.Append(" ,dtl.SULFURIC_GRAVITY ");
            sb.Append(" ,dtl.TEMPARATURE ");
            sb.Append(" ,dtl.GRID_BATCH ");
            sb.Append(" ,dtl.PASTING_BATCH ");
            sb.Append(" ,dtl.FILLING_BATCH ");
            sb.Append(" ,dtl.SULPHATION_STARTTIME ");
            sb.Append(" ,dtl.SULPHATION_OFFDATE ");
            sb.Append(" ,dtl.SULPHATION_OFFTIME ");
            sb.Append(" ,dtl.USED_ITEM_ID,dtl.IS_UNFORMED ");
            sb.Append(" ,usedInv.ITEM_NAME USED_ITEM_NAME  ");
            sb.Append(" ,pwt.PASTE_PC_KG PASTE_PANEL_KG ");
            sb.Append(" ,gp.GRID_ITEM_ID USED_GRID_ID");
            sb.Append(" ,ginv.ITEM_NAME USED_GRID_NAME ");
            sb.Append(" ,inv.ITEM_STANDARD_WEIGHT_KG ");
            sb.Append(" ,dtl.SML_ITEM_PC,NVL(dtl.ITEM_SPECIFICATION_ID,0) ITEM_SPECIFICATION_ID ");
            sb.Append(" ,pwt.PASTE_PC_KG ITEM_STD_PASTE_KG ");
            sb.Append(" ,dtl.ITEM_WEIGHT_PASTE_KG,dtl.PROD_BATCH_NO_DTL ");
            sb.Append(" ,DTL.RM_SUP_ID,SUP.SUP_NAME,dtl.FALSE_LUG_ITEM_ID,fls.ITEM_NAME FALSE_LUG_NAME,dtl.MANUAL_SLNO ");

            sb.Append(" FROM  ");
            sb.Append(" production_mst mst  ");
            sb.Append(" inner join  PRODUCTION_DTL dtl   on mst.PROD_ID=dtl.PROD_MST_ID ");
            sb.Append(" INNER JOIN INV_ITEM_MASTER inv ON dtl.ITEM_ID=inv.ITEM_ID ");
            sb.Append(" INNER JOIN UOM_INFO itmUOM  ON dtl.UOM_ID=itmUOM.UOM_ID ");
            sb.Append(" LEFT JOIN MACHINE_MST mac ON dtl.MACHINE_ID=mac.MACHINE_ID ");
            sb.Append(" LEFT JOIN UOM_INFO panel on dtl.PANEL_UOM_ID=panel.UOM_ID ");
            sb.Append(" LEFT JOIN SUPPERVISOR_MST OpMst On dtl.OPERATOR_ID=OpMst.EMP_ID ");
            sb.Append(" LEFT JOIN BOM_MST_T bom ON dtl.BOM_ID=bom.BOM_ID ");
            sb.Append(" LEFT JOIN UOM_INFO weightUOM ON dtl.WEIGHT_UOM_ID=weightUOM.UOM_ID ");
            sb.Append(" LEFT JOIN UOM_INFO bar ON dtl.BAR_TYPE=bar.UOM_ID ");
            sb.Append(" LEFT JOIN INV_ITEM_MASTER usedInv ON dtl.USED_ITEM_ID=usedInv.ITEM_ID ");
            sb.Append(" LEFT JOIN  PROD_ITEM_STANDARD_WEIGHT pwt ON pwt.ITEM_ID=dtl.ITEM_ID  and pwt.DEPT_ID=mst.DEPT_ID ");
            sb.Append(" LEFT JOIN  PROD_PASTING_GRID_MAPPING gp ON gp.PASTE_ITEM_ID=inv.ITEM_ID ");
            sb.Append(" LEFT JOIN INV_ITEM_MASTER ginv ON gp.GRID_ITEM_ID=ginv.ITEM_ID  ");
            sb.Append(" LEFT JOIN SUPPLIER_INFO SUP ON DTL.RM_SUP_ID=SUP.SUP_ID ");
            sb.Append(" LEFT JOIN INV_ITEM_MASTER fls ON dtl.FALSE_LUG_ITEM_ID=fls.ITEM_ID  ");

            sb.Append(" WHERE 1=1 ");
            return sb.ToString();
        }


        public static string GetSolarProductionDtlsSQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" Select ");
            sb.Append(" dtl.PROD_MST_ID ");
            sb.Append(" ,dtl.PROD_DTL_ID ");
            sb.Append(" ,dtl.SLNO ");
            sb.Append(" ,inv.ITEM_NAME ");
            sb.Append(" ,inv.ITEM_ID ");
            sb.Append(" ,dtl.PANEL_PC ");
            sb.Append(" ,panel.UOM_NAME PANEL_UOM_NAME ");
            sb.Append(" ,dtl.ITEM_PANEL_QTY ");
            sb.Append(" ,dtl.PANEL_UOM_ID ");
            sb.Append(" ,dtl.ITEM_QTY ");
            sb.Append(" ,itmUOM.UOM_CODE_SHORT UOM_NAME ");
            sb.Append(" ,dtl.UOM_ID ");
            sb.Append(" ,dtl.ITEM_WEIGHT ");
            sb.Append(" ,weightUOM.UOM_CODE_SHORT WEIGHT_UOM_NAME ");
            sb.Append(" ,dtl.WEIGHT_UOM_ID ");
            sb.Append(" ,dtl.MACHINE_ID ");
            sb.Append(" ,mac.MACHINE_NAME ");
            sb.Append(" ,dtl.BOM_ID ");
            sb.Append(" ,bom.BOM_ITEM_DESC BOM_NAME ");
            sb.Append(" ,dtl.REMARKS ");
            sb.Append(" ,dtl.OPERATOR_ID ");
            sb.Append(" ,dtl.USED_BAR_PC ");
            sb.Append(" ,dtl.BAR_TYPE ");
            sb.Append(" ,dtl.USED_QTY_KG ");
            sb.Append(" ,dtl.BAR_WEIGHT ");
            sb.Append(" ,NVL(F_GET_AVAILABLE_PACKING_QTY(dtl.ITEM_ID),0) AVAILABLE_PACKING_QUANTITY");
            sb.Append(" ,NVL(F_GET_AVAILABLE_CHARGING_QTY(dtl.ITEM_ID,dtl.ITEM_SPECIFICATION_ID),0) AVAILABLE_CHARGING_QUANTITY");            
            sb.Append(" ,dtl.IS_PACKING,dtl.CHARGED_QTY,dtl.PACKING_QUANTITY ");
            sb.Append(" ,OpMst.FULL_NAME OPERATOR_NAME ");
            sb.Append(" ,bar.UOM_CODE_SHORT BAR_TYPE_NAME ");
            //sb.Append(" ,dtl.GRID_BATCH ");
            sb.Append(" ,dtl.FORMATION_STARTTIME ");
            sb.Append(" ,dtl.FORMATION_OFFTIME ");
            sb.Append(" ,dtl.FORMATION_OFFDATE ");
            sb.Append(" ,dtl.FORMED_QTY ");
            sb.Append(" ,dtl.UNFORMED_QTY ");
            sb.Append(" ,dtl.REJECT_QTY ");
            sb.Append(" ,dtl.AMPERE ");
            sb.Append(" ,dtl.CYCLETIME ");
            sb.Append(" ,dtl.SULFURIC_GRAVITY ");
            sb.Append(" ,dtl.TEMPARATURE ");
            sb.Append(" ,dtl.GRID_BATCH ");
            sb.Append(" ,dtl.PASTING_BATCH ");
            sb.Append(" ,dtl.FILLING_BATCH ");
            sb.Append(" ,dtl.SULPHATION_STARTTIME ");
            sb.Append(" ,dtl.SULPHATION_OFFDATE ");
            sb.Append(" ,dtl.SULPHATION_OFFTIME ");
            sb.Append(" ,dtl.USED_ITEM_ID,dtl.IS_UNFORMED ");
            sb.Append(" ,usedInv.ITEM_NAME USED_ITEM_NAME  ");
            sb.Append(" ,pwt.PASTE_PC_KG PASTE_PANEL_KG ");
            sb.Append(" ,gp.GRID_ITEM_ID USED_GRID_ID");
            sb.Append(" ,ginv.ITEM_NAME USED_GRID_NAME ");
            sb.Append(" ,inv.ITEM_STANDARD_WEIGHT_KG ");
            sb.Append(" ,dtl.SML_ITEM_PC,NVL(dtl.ITEM_SPECIFICATION_ID,0) ITEM_SPECIFICATION_ID ");
            sb.Append(" FROM  ");
            sb.Append(" production_mst mst  ");
            sb.Append(" inner join  PRODUCTION_DTL dtl   on mst.PROD_ID=dtl.PROD_MST_ID ");
            sb.Append(" INNER JOIN INV_ITEM_MASTER inv ON dtl.ITEM_ID=inv.ITEM_ID ");
            sb.Append(" INNER JOIN UOM_INFO itmUOM  ON dtl.UOM_ID=itmUOM.UOM_ID ");
            sb.Append(" LEFT JOIN MACHINE_MST mac ON dtl.MACHINE_ID=mac.MACHINE_ID ");
            sb.Append(" LEFT JOIN UOM_INFO panel on dtl.PANEL_UOM_ID=panel.UOM_ID ");
            sb.Append(" LEFT JOIN SUPPERVISOR_MST OpMst On dtl.OPERATOR_ID=OpMst.EMP_ID ");
            sb.Append(" LEFT JOIN BOM_MST_T bom ON dtl.BOM_ID=bom.BOM_ID ");
            sb.Append(" LEFT JOIN UOM_INFO weightUOM ON dtl.WEIGHT_UOM_ID=weightUOM.UOM_ID ");
            sb.Append(" LEFT JOIN UOM_INFO bar ON dtl.BAR_TYPE=bar.UOM_ID ");
            sb.Append(" LEFT JOIN INV_ITEM_MASTER usedInv ON dtl.USED_ITEM_ID=usedInv.ITEM_ID ");
            sb.Append(" LEFT JOIN  PROD_ITEM_STANDARD_WEIGHT pwt ON pwt.ITEM_ID=dtl.ITEM_ID  and pwt.DEPT_ID=mst.DEPT_ID ");
            sb.Append(" LEFT JOIN  PROD_PASTING_GRID_MAPPING gp ON gp.PASTE_ITEM_ID=inv.ITEM_ID ");
            sb.Append(" LEFT JOIN INV_ITEM_MASTER ginv ON gp.GRID_ITEM_ID=ginv.ITEM_ID  ");

            sb.Append(" WHERE 1=1 ");
            return sb.ToString();
        }


        public static string GetSolarProductionDtlsSQLStringForLoading()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" Select ");
            sb.Append(" dtl.PROD_MST_ID ");
            sb.Append(" ,dtl.PROD_DTL_ID ");
            sb.Append(" ,dtl.SLNO ");
            sb.Append(" ,inv.ITEM_NAME ");
            sb.Append(" ,inv.ITEM_ID ");
            sb.Append(" ,dtl.PANEL_PC ");
            sb.Append(" ,panel.UOM_NAME PANEL_UOM_NAME ");
            sb.Append(" ,dtl.ITEM_PANEL_QTY ");
            sb.Append(" ,dtl.PANEL_UOM_ID ");
            sb.Append(" ,dtl.ITEM_QTY ");
            sb.Append(" ,itmUOM.UOM_CODE_SHORT UOM_NAME ");
            sb.Append(" ,dtl.UOM_ID ");
            sb.Append(" ,dtl.ITEM_WEIGHT ");
            sb.Append(" ,weightUOM.UOM_CODE_SHORT WEIGHT_UOM_NAME ");
            sb.Append(" ,dtl.WEIGHT_UOM_ID ");
            sb.Append(" ,dtl.MACHINE_ID ");
            sb.Append(" ,mac.MACHINE_NAME ");
            sb.Append(" ,dtl.BOM_ID ");
            sb.Append(" ,bom.BOM_ITEM_DESC BOM_NAME ");
            sb.Append(" ,dtl.REMARKS ");
            sb.Append(" ,dtl.OPERATOR_ID ");
            sb.Append(" ,dtl.USED_BAR_PC ");
            sb.Append(" ,dtl.BAR_TYPE ");
            sb.Append(" ,dtl.USED_QTY_KG ");
            sb.Append(" ,dtl.BAR_WEIGHT ");
            sb.Append(" ,NVL(GET_SOLAR_LOAD_AVAILABLE_QTY(dtl.ITEM_ID,54,dtl.ITEM_SPECIFICATION_ID),0) CLOSING_QTY");          
            sb.Append(" ,OpMst.FULL_NAME OPERATOR_NAME ");
            sb.Append(" ,bar.UOM_CODE_SHORT BAR_TYPE_NAME ");
            //sb.Append(" ,dtl.GRID_BATCH ");
            sb.Append(" ,dtl.FORMATION_STARTTIME ");
            sb.Append(" ,dtl.FORMATION_OFFTIME ");
            sb.Append(" ,dtl.FORMATION_OFFDATE ");
            sb.Append(" ,dtl.FORMED_QTY ");
            sb.Append(" ,dtl.UNFORMED_QTY ");
            sb.Append(" ,dtl.REJECT_QTY ");
            sb.Append(" ,dtl.AMPERE ");
            sb.Append(" ,dtl.CYCLETIME ");
            sb.Append(" ,dtl.SULFURIC_GRAVITY ");
            sb.Append(" ,dtl.TEMPARATURE ");
            sb.Append(" ,dtl.GRID_BATCH ");
            sb.Append(" ,dtl.PASTING_BATCH ");
            sb.Append(" ,dtl.FILLING_BATCH ");
            sb.Append(" ,dtl.SULPHATION_STARTTIME ");
            sb.Append(" ,dtl.SULPHATION_OFFDATE ");
            sb.Append(" ,dtl.SULPHATION_OFFTIME ");
            sb.Append(" ,dtl.USED_ITEM_ID,dtl.IS_UNFORMED ");
            sb.Append(" ,usedInv.ITEM_NAME USED_ITEM_NAME  ");
            sb.Append(" ,pwt.PASTE_PC_KG PASTE_PANEL_KG ");
            sb.Append(" ,gp.GRID_ITEM_ID USED_GRID_ID");
            sb.Append(" ,ginv.ITEM_NAME USED_GRID_NAME ");
            sb.Append(" ,inv.ITEM_STANDARD_WEIGHT_KG ");
            sb.Append(" ,dtl.SML_ITEM_PC,NVL(dtl.ITEM_SPECIFICATION_ID,0) ITEM_SPECIFICATION_ID ");
            sb.Append(" FROM  ");
            sb.Append(" production_mst mst  ");
            sb.Append(" inner join  PRODUCTION_DTL dtl   on mst.PROD_ID=dtl.PROD_MST_ID ");
            sb.Append(" INNER JOIN INV_ITEM_MASTER inv ON dtl.ITEM_ID=inv.ITEM_ID ");
            sb.Append(" INNER JOIN UOM_INFO itmUOM  ON dtl.UOM_ID=itmUOM.UOM_ID ");
            sb.Append(" LEFT JOIN MACHINE_MST mac ON dtl.MACHINE_ID=mac.MACHINE_ID ");
            sb.Append(" LEFT JOIN UOM_INFO panel on dtl.PANEL_UOM_ID=panel.UOM_ID ");
            sb.Append(" LEFT JOIN SUPPERVISOR_MST OpMst On dtl.OPERATOR_ID=OpMst.EMP_ID ");
            sb.Append(" LEFT JOIN BOM_MST_T bom ON dtl.BOM_ID=bom.BOM_ID ");
            sb.Append(" LEFT JOIN UOM_INFO weightUOM ON dtl.WEIGHT_UOM_ID=weightUOM.UOM_ID ");
            sb.Append(" LEFT JOIN UOM_INFO bar ON dtl.BAR_TYPE=bar.UOM_ID ");
            sb.Append(" LEFT JOIN INV_ITEM_MASTER usedInv ON dtl.USED_ITEM_ID=usedInv.ITEM_ID ");
            sb.Append(" LEFT JOIN  PROD_ITEM_STANDARD_WEIGHT pwt ON pwt.ITEM_ID=dtl.ITEM_ID  and pwt.DEPT_ID=mst.DEPT_ID ");
            sb.Append(" LEFT JOIN  PROD_PASTING_GRID_MAPPING gp ON gp.PASTE_ITEM_ID=inv.ITEM_ID ");
            sb.Append(" LEFT JOIN INV_ITEM_MASTER ginv ON gp.GRID_ITEM_ID=ginv.ITEM_ID  ");

            sb.Append(" WHERE 1=1 ");
            return sb.ToString();
        }
        public static string GetProductionPackingDtlsSQLString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" Select ");
            sb.Append(" dtl.PROD_MST_ID ");
            sb.Append(" ,dtl.PROD_DTL_ID ");
            sb.Append(" ,dtl.SLNO ");
            sb.Append(" ,inv.ITEM_NAME ");
            sb.Append(" ,inv.ITEM_ID ");
            sb.Append(" ,dtl.PANEL_PC ");
            sb.Append(" ,panel.UOM_NAME PANEL_UOM_NAME ");
            sb.Append(" ,dtl.ITEM_PANEL_QTY ");
            sb.Append(" ,dtl.PANEL_UOM_ID ");
            sb.Append(" ,dtl.ITEM_QTY ");
            sb.Append(" ,itmUOM.UOM_CODE_SHORT UOM_NAME ");
            sb.Append(" ,dtl.UOM_ID ");
            sb.Append(" ,dtl.ITEM_WEIGHT ");
            sb.Append(" ,weightUOM.UOM_CODE_SHORT WEIGHT_UOM_NAME ");
            sb.Append(" ,dtl.WEIGHT_UOM_ID ");
            sb.Append(" ,dtl.MACHINE_ID ");
            sb.Append(" ,mac.MACHINE_NAME ");
            sb.Append(" ,dtl.BOM_ID ");
            sb.Append(" ,bom.BOM_ITEM_DESC BOM_NAME ");
            sb.Append(" ,dtl.REMARKS ");
            sb.Append(" ,dtl.OPERATOR_ID ");
            sb.Append(" ,dtl.USED_BAR_PC ");
            sb.Append(" ,dtl.BAR_TYPE ");
            sb.Append(" ,dtl.USED_QTY_KG ");
            sb.Append(" ,dtl.BAR_WEIGHT ");
            sb.Append(" ,OpMst.FULL_NAME OPERATOR_NAME ");
            sb.Append(" ,bar.UOM_CODE_SHORT BAR_TYPE_NAME ");
            sb.Append(" ,dtl.GRID_BATCH ");
            sb.Append(" ,dtl.FORMATION_STARTTIME ");
            sb.Append(" ,dtl.FORMATION_OFFTIME ");
            sb.Append(" ,dtl.FORMATION_OFFDATE ");
            sb.Append(" ,dtl.FORMED_QTY ");
            sb.Append(" ,dtl.UNFORMED_QTY ");
            sb.Append(" ,dtl.REJECT_QTY ");
            sb.Append(" ,dtl.AMPERE ");
            sb.Append(" ,dtl.CYCLETIME ");
            sb.Append(" ,dtl.SULFURIC_GRAVITY ");
            sb.Append(" ,dtl.TEMPARATURE ");
            sb.Append(" ,dtl.GRID_BATCH ");
            sb.Append(" ,dtl.PASTING_BATCH ");
            sb.Append(" ,dtl.PACK_FINISHED_BATCH ");
            sb.Append(" FROM  ");
            sb.Append(" PRODUCTION_DTL dtl ");
            sb.Append(" INNER JOIN INV_ITEM_MASTER inv ON dtl.ITEM_ID=inv.ITEM_ID ");
            sb.Append(" INNER JOIN UOM_INFO itmUOM  ON dtl.UOM_ID=itmUOM.UOM_ID ");
            sb.Append(" LEFT JOIN MACHINE_MST mac ON dtl.MACHINE_ID=mac.MACHINE_ID ");
            sb.Append(" LEFT JOIN UOM_INFO panel on dtl.PANEL_UOM_ID=panel.UOM_ID ");
            sb.Append(" LEFT JOIN SUPPERVISOR_MST OpMst On dtl.OPERATOR_ID=OpMst.EMP_ID ");
            sb.Append(" LEFT JOIN BOM_MST_T bom ON dtl.BOM_ID=bom.BOM_ID ");
            sb.Append(" LEFT JOIN UOM_INFO weightUOM ON dtl.WEIGHT_UOM_ID=weightUOM.UOM_ID ");
            sb.Append(" LEFT JOIN UOM_INFO bar ON dtl.BAR_TYPE=bar.UOM_ID ");
            sb.Append(" WHERE 1=1 ");
            sb.Append(" AND dtl.IS_PACKING='Y' ");
            return sb.ToString();
        }

        public static string GetProductionDtlsByProdID_ItemID(int pProd_id, int pItem_id, DBContext dc)
        {
            //List<dcPRODUCTION_DTL> cObjList = new List<dcPRODUCTION_DTL>();
            string cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder(GetProductionEntryDtlSQLString());

                    if (pProd_id > 0)
                    {
                        sb.Append("  AND dtl.PROD_MST_ID=@PROD_MST_ID ");
                        cmdInfo.DBParametersInfo.Add("@PROD_MST_ID", pProd_id);
                    }

                    if (pItem_id > 0)
                    {
                        sb.Append("  AND dtl.item_id=@item_id ");
                        cmdInfo.DBParametersInfo.Add("@item_id", pItem_id);
                    }

                    sb.Append(" ORDER BY dtl.SLNO");
                    DBQuery dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery(dbq, dc).Rows[0][0].ToString();
                    //
                    //DBQuery.ExecuteDBQuery(dbq, dc).ToString();
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static List<dcPRODUCTION_DTL> GetProductionDtlsByProdID(int pProd_id, DBContext dc)
        {
            List<dcPRODUCTION_DTL> cObjList = new List<dcPRODUCTION_DTL>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder(GetProductionDtlsSQLString());

                    if (pProd_id > 0)
                    {
                        sb.Append("  AND dtl.PROD_MST_ID=@PROD_MST_ID ");
                        cmdInfo.DBParametersInfo.Add("@PROD_MST_ID", pProd_id);
                    }

                    sb.Append("  AND dtl.IS_PACKING  IS NULL");

                    sb.Append(" ORDER BY dtl.SLNO");
                    DBQuery dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_DTL>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static dcPRODUCTION_DTL GetFalseLugDtlsByItemId(int pItem_id, DBContext dc)
        {
            dcPRODUCTION_DTL cObjList = new dcPRODUCTION_DTL();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT BDTL.*,PDTL.FALSE_LUG_ITEM_ID,ITM.ITEM_NAME FALSE_LUG_NAME ");
                    sb.Append(" FROM BOM_DTL_T BDTL ");
                    sb.Append(" INNER JOIN BOM_MST_T BMST ON BDTL.BOM_MST_ID=BMST.BOM_ID ");
                    sb.Append(" INNER JOIN INV_ITEM_MASTER IM ON BDTL.ITEM_ID=IM.ITEM_ID ");
                    sb.Append(" INNER JOIN PRODUCTION_DTL PDTL ON BDTL.ITEM_ID=PDTL.ITEM_ID ");
                    sb.Append(" LEFT JOIN INV_ITEM_MASTER ITM ON PDTL.FALSE_LUG_ITEM_ID=ITM.ITEM_ID ");
                    sb.Append(" WHERE 1=1 ");

                    if (pItem_id > 0)
                    {
                        sb.Append("  AND BMST.BOM_ITEM_ID=@pItem_id ");
                        cmdInfo.DBParametersInfo.Add("@pItem_id", pItem_id);
                    }

                   
                    DBQuery dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_DTL>(dbq, dc).FirstOrDefault();
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }


        public static List<dcPRODUCTION_DTL> GetSolarProductionDtlsByProdID(int pProd_id, DBContext dc)
        {
            List<dcPRODUCTION_DTL> cObjList = new List<dcPRODUCTION_DTL>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder(GetSolarProductionDtlsSQLString());

                    if (pProd_id > 0)
                    {
                        sb.Append("  AND dtl.PROD_MST_ID=@PROD_MST_ID ");
                        cmdInfo.DBParametersInfo.Add("@PROD_MST_ID", pProd_id);
                    }

                    //sb.Append("  AND dtl.IS_PACKING  IS NULL");

                    sb.Append(" ORDER BY dtl.SLNO");
                    DBQuery dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_DTL>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcPRODUCTION_DTL> GetSolarProductionDtlsForLoadingByProdID(int pProd_id, DBContext dc)
        {
            List<dcPRODUCTION_DTL> cObjList = new List<dcPRODUCTION_DTL>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder(GetSolarProductionDtlsSQLStringForLoading());
                    if (pProd_id > 0)
                    {
                        sb.Append("  AND dtl.PROD_MST_ID=@PROD_MST_ID ");
                        cmdInfo.DBParametersInfo.Add("@PROD_MST_ID", pProd_id);
                    }
                    sb.Append(" ORDER BY dtl.SLNO");
                    DBQuery dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_DTL>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        //GetBatteryTypeList

        public static List<dcPRODUCTION_DTL> GetBatteryTypeList(DBContext dc)
        {
            List<dcPRODUCTION_DTL> cObjList = new List<dcPRODUCTION_DTL>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder(" SELECT * FROM PROD_BATTERY_TYPE_MST ");


                    sb.Append(" ORDER BY PROD_BAT_TYPE_ID");
                    DBQuery dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_DTL>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        //END Battery Type List
        public static List<dcPRODUCTION_DTL> GetProductionDtlsByProdID(clsPrmInventory pObj, DBContext dc)
        {
            List<dcPRODUCTION_DTL> cObjList = new List<dcPRODUCTION_DTL>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder(GetProductionDtlsSQLString());

                    if (pObj.prod_id > 0)
                    {
                        sb.Append("  AND dtl.PROD_MST_ID=@PROD_MST_ID ");
                        cmdInfo.DBParametersInfo.Add("@PROD_MST_ID", pObj.prod_id);
                    }


                    if (pObj.ProcessType != "")
                    {
                        sb.Append("  AND dtl.PROCESSTYPE=@PROCESSTYPE ");
                        cmdInfo.DBParametersInfo.Add("@PROCESSTYPE", pObj.ProcessType);
                    }
                    sb.Append("  AND dtl.IS_PACKING  IS NULL");

                    sb.Append(" ORDER BY dtl.SLNO");
                    DBQuery dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_DTL>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcPRODUCTION_DTL> GetBasicProductionDtlsByProdID(int pProd_id, DBContext dc)
        {
            List<dcPRODUCTION_DTL> cObjList = new List<dcPRODUCTION_DTL>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder(GetProductionEntryDtlSQLString());
                    if (pProd_id > 0)
                    {
                        sb.Append("  AND dtl.PROD_MST_ID=@PROD_MST_ID ");
                        cmdInfo.DBParametersInfo.Add("@PROD_MST_ID", pProd_id);
                    }
                    sb.Append("  AND dtl.IS_PACKING  IS NULL");
                    sb.Append(" ORDER BY dtl.SLNO");
                    DBQuery dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_DTL>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static string AuthorizeFormationProduction(int prodId, clsPrmInventory prm, DBContext dc)
        {

            string msg = string.Empty;
            bool bStatus = false;
            bool isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            bool isTransInit = dc.StartTransaction();

            try
            {

                dcPRODUCTION_MST prodObj = PRODUCTION_MSTBL.GetProductionByProdID(prodId.ToString(), dc);
                if (prodObj != null)
                {
                    if (prodObj.AUTH_STATUS == "Y")
                    {
                        msg = "Already Authorised";
                        return msg;
                    }
                }
                else
                {
                    msg = "Invalid Production number.";
                    return msg;
                }
                PRODUCTION_MSTBL.UpdateAuthorized(prodId, prm.user_id, dc);
                List<dcITEM_STOCK_DETAILS> stkDtlList = new List<dcITEM_STOCK_DETAILS>();
                List<dcITEM_STOCK_DETAILS> closingStkDtlList = new List<dcITEM_STOCK_DETAILS>();
                List<dcPRODUCTION_DTL> listDetails = GetBasicProductionDtlsByProdID(prodId, dc);

                if (prm.Loading_Type == "U")
                {
                    #region Formation_Unloading_Stock


                    decimal totalQty = listDetails.Sum(x => x.ITEM_QTY);
                    decimal unLoadQty = listDetails.Sum(x => x.TOTAL_UNLOAD_QTY);

                    if (totalQty != unLoadQty)
                    {
                        msg = msg + "Totam Item Quantity must be rqual to unload quantity.";
                        return msg;
                    }

                    foreach (var item in listDetails)
                    {

                        if (item.FORMED_QTY > 0)
                        {
                            //formed Issue
                            dcITEM_STOCK_DETAILS formedItemIssue = new dcITEM_STOCK_DETAILS();
                            formedItemIssue.ITEM_ID = item.ITEM_ID;
                            formedItemIssue.UOM_ID = item.UOM_ID;
                            formedItemIssue.TRANS_DATE = prodObj.PRODUCTION_DATE;
                            formedItemIssue.TRANS_TIME = DateTime.Now;
                            formedItemIssue.INV_TRANS_DET_ID = item.PROD_DTL_ID;
                            formedItemIssue.TRANS_REF_NO = prodObj.PROD_NO;
                            formedItemIssue.STORE_ID = 0;
                            formedItemIssue.CREATE_BY = prm.user_id;
                            formedItemIssue.CREATE_DATE = DateTime.Now;
                            formedItemIssue.TRANS_REMARKS = "Production Formation Formed quantity Issue";
                            formedItemIssue.FROM_DEPARTMENT_ID = prodObj.DEPT_ID;
                            formedItemIssue.TO_DEPARTMENT_ID = prodObj.DEPT_ID;
                            formedItemIssue.DEPARTMENT_ID = prodObj.DEPT_ID;
                            formedItemIssue.IS_PRODUCTION = "Y";
                            formedItemIssue.TRANS_QTY = item.FORMED_QTY;
                            formedItemIssue.ISS_QTY = item.FORMED_QTY;
                            formedItemIssue.INV_TRANS_TYPE_ID = 1002;//here 1005 for production quantity issue
                            //stkReceive.INV_TRANS_TYPE_ID = 1005;//here 1005 for formed quantity receive
                            formedItemIssue.TRANS_DATE_TIME = prm.productiondate;
                            ITEM_STOCK_DETAILS_NBL.Insert(formedItemIssue, dc);

                            //formed receive

                            dcITEM_STOCK_DETAILS formedItemRcv = new dcITEM_STOCK_DETAILS();
                            formedItemRcv.ITEM_ID = item.ITEM_ID;
                            formedItemRcv.UOM_ID = item.UOM_ID;
                            formedItemRcv.TRANS_DATE = prodObj.PRODUCTION_DATE;
                            formedItemRcv.TRANS_TIME = DateTime.Now;
                            formedItemRcv.INV_TRANS_DET_ID = item.PROD_DTL_ID;
                            formedItemRcv.TRANS_REF_NO = prodObj.PROD_NO;
                            formedItemRcv.STORE_ID = 0;
                            formedItemRcv.CREATE_BY = prm.user_id;
                            formedItemRcv.CREATE_DATE = DateTime.Now;
                            formedItemRcv.TRANS_REMARKS = "Production Formation Formed quantity Receive";
                            formedItemRcv.FROM_DEPARTMENT_ID = prodObj.DEPT_ID;
                            formedItemRcv.TO_DEPARTMENT_ID = prodObj.DEPT_ID;
                            formedItemRcv.DEPARTMENT_ID = prodObj.DEPT_ID;
                            formedItemRcv.IS_PRODUCTION = "Y";
                            formedItemRcv.TRANS_QTY = item.FORMED_QTY;
                            formedItemRcv.RCV_QTY = item.FORMED_QTY;
                            formedItemRcv.INV_TRANS_TYPE_ID = 1001;//here 1005 for production quantity issue
                            formedItemRcv.TRANS_DATE_TIME = prm.productiondate;
                            //stkReceive.INV_TRANS_TYPE_ID = 1005;//here 1005 for formed quantity receive
                            ITEM_STOCK_DETAILS_NBL.Insert(formedItemRcv, dc);

                        }

                        if (item.UNFORMED_QTY > 0)
                        {
                            dcITEM_STOCK_DETAILS stkReceive = new dcITEM_STOCK_DETAILS();
                            stkReceive.ITEM_ID = item.ITEM_ID;
                            stkReceive.UOM_ID = item.UOM_ID;
                            stkReceive.TRANS_DATE = prodObj.PRODUCTION_DATE;
                            stkReceive.TRANS_TIME = DateTime.Now;
                            stkReceive.INV_TRANS_DET_ID = item.PROD_DTL_ID;
                            stkReceive.TRANS_REF_NO = prodObj.PROD_NO;
                            stkReceive.STORE_ID = 0;
                            stkReceive.CREATE_BY = prm.user_id;
                            stkReceive.CREATE_DATE = DateTime.Now;
                            stkReceive.TRANS_REMARKS = "Production Formation Unformed Quantity Receive";
                            stkReceive.FROM_DEPARTMENT_ID = prodObj.DEPT_ID;
                            stkReceive.TO_DEPARTMENT_ID = prodObj.DEPT_ID;
                            stkReceive.DEPARTMENT_ID = prodObj.DEPT_ID;
                            stkReceive.IS_PRODUCTION = "Y";
                            stkReceive.TRANS_QTY = item.UNFORMED_QTY;
                            stkReceive.RCV_QTY = item.UNFORMED_QTY;
                            stkReceive.INV_TRANS_TYPE_ID = 1006;//1006 is for unformed quantity receive
                            stkReceive.TRANS_DATE_TIME = prm.productiondate;
                            ITEM_STOCK_DETAILS_NBL.Insert(stkReceive, dc);
                        }

                        if (item.REJECT_QTY > 0)
                        {
                            dcITEM_STOCK_DETAILS rejectReceive = new dcITEM_STOCK_DETAILS();
                            rejectReceive.ITEM_ID = item.ITEM_ID;//Here 5400 is for reject plate
                            rejectReceive.UOM_ID = item.UOM_ID;
                            rejectReceive.TRANS_DATE = prodObj.PRODUCTION_DATE;
                            rejectReceive.TRANS_TIME = DateTime.Now;
                            rejectReceive.INV_TRANS_DET_ID = item.PROD_DTL_ID;
                            rejectReceive.TRANS_REF_NO = prodObj.PROD_NO;
                            rejectReceive.STORE_ID = 0;
                            rejectReceive.CREATE_BY = prm.user_id;
                            rejectReceive.CREATE_DATE = DateTime.Now;
                            rejectReceive.TRANS_REMARKS = "Production Formation Reject Quantity Receive";
                            rejectReceive.FROM_DEPARTMENT_ID = prodObj.DEPT_ID;
                            rejectReceive.TO_DEPARTMENT_ID = prodObj.DEPT_ID;
                            rejectReceive.DEPARTMENT_ID = prodObj.DEPT_ID;
                            rejectReceive.IS_PRODUCTION = "Y";
                            rejectReceive.TRANS_QTY = item.REJECT_QTY;
                            rejectReceive.RCV_QTY = item.REJECT_QTY;
                            rejectReceive.INV_TRANS_TYPE_ID = 1004;//Here 1004 for reject quantity receive
                            rejectReceive.TRANS_DATE_TIME = prm.productiondate;
                            ITEM_STOCK_DETAILS_NBL.Insert(rejectReceive, dc);

                            dcITEM_STOCK_DETAILS stkReceive = new dcITEM_STOCK_DETAILS();
                            stkReceive.ITEM_ID = item.ITEM_ID;//Here Item Id 5400 is for reject plate
                            stkReceive.UOM_ID = item.UOM_ID;
                            stkReceive.TRANS_DATE = prodObj.PRODUCTION_DATE;
                            stkReceive.TRANS_TIME = DateTime.Now;
                            stkReceive.INV_TRANS_DET_ID = item.PROD_DTL_ID;
                            stkReceive.TRANS_REF_NO = prodObj.PROD_NO;
                            stkReceive.STORE_ID = 0;
                            stkReceive.CREATE_BY = prm.user_id;
                            stkReceive.CREATE_DATE = DateTime.Now;
                            stkReceive.TRANS_REMARKS = "Production Formation Reject Plate Issue";
                            stkReceive.FROM_DEPARTMENT_ID = prodObj.DEPT_ID;
                            stkReceive.TO_DEPARTMENT_ID = prodObj.DEPT_ID;
                            stkReceive.DEPARTMENT_ID = prodObj.DEPT_ID;
                            stkReceive.IS_PRODUCTION = "Y";
                            stkReceive.TRANS_QTY = item.REJECT_QTY;
                            stkReceive.ISS_QTY = item.REJECT_QTY;
                            stkReceive.INV_TRANS_TYPE_ID = 1002;
                            stkReceive.TRANS_DATE_TIME = prm.productiondate;
                            ITEM_STOCK_DETAILS_NBL.Insert(stkReceive, dc);

                        }

                    }
                    #endregion


                    // ITEM_STOCK_DETAILS_NBL.SaveList(stkDtlList, dc);



                }
                else
                {

                    #region Formation_Loading_Stock

                    //this is plate issue to Formation loading

                    foreach (var item in listDetails)
                    {

                        if (item.IS_UNFORMED == "Y")
                        {

                            dcITEM_STOCK_DETAILS stkReceive = new dcITEM_STOCK_DETAILS();
                            stkReceive.ITEM_ID = item.ITEM_ID;
                            stkReceive.UOM_ID = item.UOM_ID;
                            stkReceive.TRANS_DATE = prodObj.PRODUCTION_DATE;
                            stkReceive.TRANS_TIME = DateTime.Now;
                            stkReceive.INV_TRANS_DET_ID = item.PROD_DTL_ID;

                            stkReceive.TRANS_REF_NO = prodObj.PROD_NO;
                            stkReceive.STORE_ID = 0;

                            stkReceive.CREATE_BY = prm.user_id;
                            stkReceive.CREATE_DATE = DateTime.Now;
                            stkReceive.TRANS_REMARKS = "Production Formation Unformed Plate Issue";
                            stkReceive.FROM_DEPARTMENT_ID = prodObj.DEPT_ID;
                            stkReceive.TO_DEPARTMENT_ID = prodObj.DEPT_ID;
                            stkReceive.DEPARTMENT_ID = prodObj.DEPT_ID;
                            stkReceive.IS_PRODUCTION = "Y";

                            stkReceive.TRANS_QTY = item.ITEM_QTY;
                            stkReceive.ISS_QTY = item.ITEM_QTY;

                            //1007 for unformed plate Issue.

                            stkReceive.INV_TRANS_TYPE_ID = 1007;
                            ITEM_STOCK_DETAILS_NBL.Insert(stkReceive, dc);

                        }
                        else
                        {
                            dcITEM_STOCK_DETAILS stkReceive = new dcITEM_STOCK_DETAILS();
                            stkReceive.ITEM_ID = item.ITEM_ID;
                            stkReceive.UOM_ID = item.UOM_ID;
                            stkReceive.TRANS_DATE = prodObj.PRODUCTION_DATE;
                            stkReceive.TRANS_TIME = DateTime.Now;
                            stkReceive.INV_TRANS_DET_ID = item.PROD_DTL_ID;

                            stkReceive.TRANS_REF_NO = prodObj.PROD_NO;
                            stkReceive.STORE_ID = 0;

                            stkReceive.CREATE_BY = prm.user_id;
                            stkReceive.CREATE_DATE = DateTime.Now;
                            stkReceive.TRANS_REMARKS = "FORMED_RCV_LOADING";
                            stkReceive.FROM_DEPARTMENT_ID = prodObj.DEPT_ID;
                            stkReceive.TO_DEPARTMENT_ID = prodObj.DEPT_ID;
                            stkReceive.DEPARTMENT_ID = prodObj.DEPT_ID;
                            stkReceive.IS_PRODUCTION = "Y";
                            stkReceive.TRANS_QTY = item.ITEM_QTY;
                            stkReceive.RCV_QTY = item.ITEM_QTY;
                            stkReceive.INV_TRANS_TYPE_ID = 1016;
                            ITEM_STOCK_DETAILS_NBL.Insert(stkReceive, dc);
                        }

                    }
                    //Raw material issue to production from formation department when loading
                    List<dcPRODUCTION_FLOOR_CLOSING> listClosingDetls = PRODUCTION_FLOOR_CLOSINGBL.GetProductionClosingDtlsByProdID(prodId, dc);

                    if (listClosingDetls != null && listClosingDetls.Any())
                    {
                        foreach (var item in listClosingDetls)
                        {
                            // Stock Issue 
                            dcITEM_STOCK_DETAILS stkIssue = new dcITEM_STOCK_DETAILS();
                            stkIssue.ITEM_ID = item.CLOSING_ITEM_ID;
                            stkIssue.UOM_ID = item.CLOSING_UOM_ID;
                            stkIssue.TRANS_DATE = prodObj.PRODUCTION_DATE;
                            stkIssue.TRANS_TIME = DateTime.Now;
                            stkIssue.INV_TRANS_DET_ID = item.CLOSING_ID;
                            stkIssue.TRANS_QTY = item.ISSUE_STOCK;
                            stkIssue.ISS_QTY = item.ISSUE_STOCK;
                            stkIssue.TRANS_REF_NO = prodObj.PROD_NO;
                            stkIssue.STORE_ID = 0;
                            stkIssue.INV_TRANS_TYPE_ID = 1002;//1002 issue to production
                            stkIssue.CREATE_BY = prm.user_id;
                            stkIssue.CREATE_DATE = DateTime.Now;
                            stkIssue.TRANS_REMARKS = "Production Formation Entry Issue";
                            stkIssue.FROM_DEPARTMENT_ID = prodObj.DEPT_ID;
                            stkIssue.TO_DEPARTMENT_ID = prodObj.DEPT_ID;
                            stkIssue.DEPARTMENT_ID = prodObj.DEPT_ID;
                            stkIssue.IS_PRODUCTION = "Y";
                            ITEM_STOCK_DETAILS_NBL.Insert(stkIssue, dc);

                        }
                    }


                    #endregion


                }
                dc.CommitTransaction(isTransInit);
            }
            catch
            {
                dc.RollbackTransaction();
            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }

            return msg;


        }



        public static List<dcPRODUCTION_DTL> GetProductionPackingDtlsByProdID(int pProd_id, DBContext dc)
        {
            List<dcPRODUCTION_DTL> cObjList = new List<dcPRODUCTION_DTL>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder(GetProductionPackingDtlsSQLString());

                    if (pProd_id > 0)
                    {
                        sb.Append("  AND dtl.PROD_MST_ID=@PROD_MST_ID ");
                        cmdInfo.DBParametersInfo.Add("@PROD_MST_ID", pProd_id);
                    }

                    sb.Append(" ORDER BY dtl.SLNO");
                    DBQuery dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_DTL>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static List<dcPRODUCTION_DTL> GetPRODUCTION_DTLList()
        {
            return GetPRODUCTION_DTLList(null, null);
        }
        public static List<dcPRODUCTION_DTL> GetPRODUCTION_DTLList(DBContext dc)
        {
            return GetPRODUCTION_DTLList(null, dc);
        }
        public static List<dcPRODUCTION_DTL> GetPRODUCTION_DTLList(DBQuery dbq, DBContext dc)
        {
            List<dcPRODUCTION_DTL> cObjList = new List<dcPRODUCTION_DTL>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_DTL>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcPRODUCTION_DTL GetPRODUCTION_DTLByID(int pPRODUCTION_DTLID)
        {
            return GetPRODUCTION_DTLByID(pPRODUCTION_DTLID, null);
        }
        public static dcPRODUCTION_DTL GetPRODUCTION_DTLByID(int pPRODUCTION_DTLID, DBContext dc)
        {
            dcPRODUCTION_DTL cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcPRODUCTION_DTL>()
                                  where c.PROD_DTL_ID == pPRODUCTION_DTLID
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

        public static int Insert(dcPRODUCTION_DTL cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcPRODUCTION_DTL cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcPRODUCTION_DTL>(cObj, true);
                if (id > 0) { cObj.PROD_DTL_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcPRODUCTION_DTL cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcPRODUCTION_DTL cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcPRODUCTION_DTL>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pPRODUCTION_DTLID)
        {
            return Delete(pPRODUCTION_DTLID, null);
        }
        public static bool Delete(int pPRODUCTION_DTLID, DBContext dc)
        {
            dcPRODUCTION_DTL cObj = new dcPRODUCTION_DTL();
            cObj.PROD_DTL_ID = pPRODUCTION_DTLID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcPRODUCTION_DTL>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcPRODUCTION_DTL cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcPRODUCTION_DTL cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcPRODUCTION_DTL cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcPRODUCTION_DTL cObj, DBContext dc)
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
                                newID = cObj.PROD_DTL_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.PROD_DTL_ID, dc))
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

        public static bool SaveList(List<dcPRODUCTION_DTL> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcPRODUCTION_DTL> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            int NewDetID = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcPRODUCTION_DTL oDet in detList)
            {
                switch (oDet._RecordState)
                {
                    case RecordStateEnum.Added:
                        {
                            NewDetID = Insert(oDet, dc);
                        }
                        break;
                    case RecordStateEnum.Edited:
                        bool e = Update(oDet, dc);
                        break;
                    case RecordStateEnum.Deleted:
                        bool d = Delete(oDet.PROD_DTL_ID, dc);
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

        public static bool DeleteByPROD_MST_ID(int pPROD_MST_ID)
        {
            return DeleteByPROD_MST_ID(pPROD_MST_ID, null);
        }
        public static bool DeleteByPROD_MST_ID(int pPROD_MST_ID, DBContext dc)
        {
            dcPRODUCTION_DTL cObj = new dcPRODUCTION_DTL();
            cObj.PROD_MST_ID = pPROD_MST_ID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcPRODUCTION_DTL>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool DeleteByItem_ID(int pPROD_MST_ID,int pItemId,int pMachineId)
        {
            return DeleteByItem_ID(pPROD_MST_ID, pItemId, pMachineId, null);
        }
        public static bool DeleteByItem_ID(int pPROD_MST_ID, int pItemId, int pMachineId, DBContext dc)
        {
            dcPRODUCTION_DTL cObj = new dcPRODUCTION_DTL();
            cObj.PROD_MST_ID = pPROD_MST_ID;
            cObj.ITEM_ID = pItemId;
            cObj.MACHINE_ID = pMachineId;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcPRODUCTION_DTL>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static string AuthorizeSolarUnloadingEntry(int prodId, clsPrmInventory prm, DBContext dc)
        {
            string msg = string.Empty;
            bool bStatus = false;
            bool isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            bool isTransInit = dc.StartTransaction();

            try
            {
                dcPRODUCTION_MST prodObj = PRODUCTION_MSTBL.GetProductionByProdID(prodId.ToString(), dc);
                if (prodObj != null)
                {
                    if (prodObj.AUTH_STATUS == "Y")
                    {
                        msg = "Already Authorised";
                        return msg;
                    }
                }
                else
                {
                    msg = "Invalid Production number.";
                    return msg;
                }

                PRODUCTION_MSTBL.UpdateAuthorized(prodId, prm.user_id, dc);
                List<dcITEM_STOCK_DETAILS> stkDtlList = new List<dcITEM_STOCK_DETAILS>();
                List<dcITEM_STOCK_DETAILS> closingStkDtlList = new List<dcITEM_STOCK_DETAILS>();
                List<dcPRODUCTION_DTL> listDetails =GetSolarProductionDtlsByProdID(prodId, dc);

                foreach (var item in listDetails)
                {

                    if (item.CHARGED_QTY>0)
                    {
                        dcITEM_STOCK_DETAILS formedItemRcv = new dcITEM_STOCK_DETAILS();
                        formedItemRcv.ITEM_ID = item.ITEM_ID;
                        formedItemRcv.UOM_ID = item.UOM_ID;
                        formedItemRcv.TRANS_DATE = prodObj.PRODUCTION_DATE;
                        formedItemRcv.TRANS_TIME = DateTime.Now;
                        formedItemRcv.INV_TRANS_DET_ID = item.PROD_DTL_ID;
                        formedItemRcv.TRANS_REF_NO = prodObj.PROD_NO;
                        formedItemRcv.STORE_ID = 0;
                        formedItemRcv.CREATE_BY = prm.user_id;
                        formedItemRcv.CREATE_DATE = DateTime.Now;
                        formedItemRcv.TRANS_REMARKS = "Production Solar Unloading Entry Receive";
                        formedItemRcv.FROM_DEPARTMENT_ID = prodObj.DEPT_ID;
                        formedItemRcv.TO_DEPARTMENT_ID = prodObj.DEPT_ID;
                        formedItemRcv.DEPARTMENT_ID = prodObj.DEPT_ID;
                        formedItemRcv.IS_PRODUCTION = "Y";
                        formedItemRcv.TRANS_QTY = Conversion.StringToDecimal(item.CHARGED_QTY.ToString());
                        formedItemRcv.RCV_QTY = Conversion.StringToDecimal(item.CHARGED_QTY.ToString());
                        formedItemRcv.ITEM_SPECIFICATION_ID =item.ITEM_SPECIFICATION_ID;
                        formedItemRcv.INV_TRANS_TYPE_ID = 9008;                      
                        formedItemRcv.TRANS_DATE_TIME = prm.productiondate;
                        ITEM_STOCK_DETAILS_NBL.Insert(formedItemRcv, dc);
                    }

                    if(item.REJECT_QTY>0)
                    {
                        dcITEM_STOCK_DETAILS formedItemRcv = new dcITEM_STOCK_DETAILS();
                        formedItemRcv.ITEM_ID = item.ITEM_ID;
                        formedItemRcv.UOM_ID = item.UOM_ID;
                        formedItemRcv.TRANS_DATE = prodObj.PRODUCTION_DATE;
                        formedItemRcv.TRANS_TIME = DateTime.Now;
                        formedItemRcv.INV_TRANS_DET_ID = item.PROD_DTL_ID;
                        formedItemRcv.TRANS_REF_NO = prodObj.PROD_NO;
                        formedItemRcv.STORE_ID = 0;
                        formedItemRcv.CREATE_BY = prm.user_id;
                        formedItemRcv.CREATE_DATE = DateTime.Now;
                        formedItemRcv.TRANS_REMARKS = "Production Solar Unloading Reject Receive";
                        formedItemRcv.FROM_DEPARTMENT_ID = prodObj.DEPT_ID;
                        formedItemRcv.TO_DEPARTMENT_ID = prodObj.DEPT_ID;
                        formedItemRcv.DEPARTMENT_ID = prodObj.DEPT_ID;
                        formedItemRcv.IS_PRODUCTION = "Y";
                        formedItemRcv.TRANS_QTY = item.REJECT_QTY;
                        formedItemRcv.RCV_QTY = item.REJECT_QTY;
                        formedItemRcv.ITEM_SPECIFICATION_ID = item.ITEM_SPECIFICATION_ID;                 
                        formedItemRcv.INV_TRANS_TYPE_ID = 1004;                       
                        formedItemRcv.TRANS_DATE_TIME = prm.productiondate;
                        ITEM_STOCK_DETAILS_NBL.Insert(formedItemRcv, dc);
                    }
                    if (item.IS_PACKING=="Y")
                    {
                        dcITEM_STOCK_DETAILS formedItemRcv = new dcITEM_STOCK_DETAILS();
                        formedItemRcv.ITEM_ID = item.ITEM_ID;
                        formedItemRcv.UOM_ID = item.UOM_ID;
                        formedItemRcv.TRANS_DATE = prodObj.PRODUCTION_DATE;
                        formedItemRcv.TRANS_TIME = DateTime.Now;
                        formedItemRcv.INV_TRANS_DET_ID = item.PROD_DTL_ID;
                        formedItemRcv.TRANS_REF_NO = prodObj.PROD_NO;
                        formedItemRcv.STORE_ID = 0;
                        formedItemRcv.CREATE_BY = prm.user_id;
                        formedItemRcv.CREATE_DATE = DateTime.Now;
                        formedItemRcv.TRANS_REMARKS = "Production Solar Packing Entry Receive";
                        formedItemRcv.FROM_DEPARTMENT_ID = prodObj.DEPT_ID;
                        formedItemRcv.TO_DEPARTMENT_ID = prodObj.DEPT_ID;
                        formedItemRcv.DEPARTMENT_ID = prodObj.DEPT_ID;
                        formedItemRcv.IS_PRODUCTION = "Y";
                        formedItemRcv.TRANS_QTY =Conversion.StringToDecimal(item.PACKING_QUANTITY.ToString());
                        formedItemRcv.RCV_QTY = Conversion.StringToDecimal(item.PACKING_QUANTITY.ToString());
                        formedItemRcv.ITEM_SPECIFICATION_ID = 11;
                        formedItemRcv.INV_TRANS_TYPE_ID = 1001;                        
                        formedItemRcv.TRANS_DATE_TIME = prm.productiondate;
                        ITEM_STOCK_DETAILS_NBL.Insert(formedItemRcv, dc);
                    }
                }

                //Raw material issue to production from formation department when loading
                List<dcPRODUCTION_FLOOR_CLOSING> listClosingDetls = PRODUCTION_FLOOR_CLOSINGBL.GetProductionClosingDtlsByProdID(prodId, dc);

                if (listClosingDetls != null && listClosingDetls.Any())
                {
                    foreach (var item in listClosingDetls)
                    {
                        // Stock Issue 
                        dcITEM_STOCK_DETAILS stkIssue = new dcITEM_STOCK_DETAILS();
                        stkIssue.ITEM_ID = item.CLOSING_ITEM_ID;
                        stkIssue.UOM_ID = item.CLOSING_UOM_ID;
                        stkIssue.TRANS_DATE = prodObj.PRODUCTION_DATE;
                        stkIssue.TRANS_TIME = DateTime.Now;
                        stkIssue.INV_TRANS_DET_ID = item.CLOSING_ID;
                        stkIssue.TRANS_QTY = item.ISSUE_STOCK;
                        stkIssue.ISS_QTY = item.ISSUE_STOCK;
                        stkIssue.TRANS_REF_NO = prodObj.PROD_NO;
                        stkIssue.STORE_ID = 0;
                        stkIssue.INV_TRANS_TYPE_ID = 1002;//1002 issue to production
                        stkIssue.CREATE_BY = prm.user_id;
                        stkIssue.CREATE_DATE = DateTime.Now;
                        stkIssue.TRANS_REMARKS = "Production Solar Entry Raw Material Issue";
                        stkIssue.FROM_DEPARTMENT_ID = prodObj.DEPT_ID;
                        stkIssue.TO_DEPARTMENT_ID = prodObj.DEPT_ID;
                        stkIssue.DEPARTMENT_ID = prodObj.DEPT_ID;
                        stkIssue.IS_PRODUCTION = "Y";
                        stkIssue.ITEM_SPECIFICATION_ID = 1;
                        ITEM_STOCK_DETAILS_NBL.Insert(stkIssue, dc);
                    }
                }


                //#endregion


                //}
                dc.CommitTransaction(isTransInit);
            }
            catch
            {
                dc.RollbackTransaction();
            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }

            return msg;


        }


        public static string AuthorizeAssemblySolarLoading(int prodId, clsPrmInventory prm, DBContext dc)
        {

            string msg = string.Empty;
            bool bStatus = false;
            bool isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            bool isTransInit = dc.StartTransaction();

            try
            {
                dcPRODUCTION_MST prodObj = PRODUCTION_MSTBL.GetProductionByProdID(prodId.ToString(), dc);
                if (prodObj != null)
                {
                    if (prodObj.AUTH_STATUS == "Y")
                    {
                        msg = "Already Authorised";
                        return msg;
                    }
                }
                else
                {
                    msg = "Invalid Production number.";
                    return msg;
                }
                PRODUCTION_MSTBL.UpdateAuthorized(prodId, prm.user_id, dc);
                List<dcITEM_STOCK_DETAILS> stkDtlList = new List<dcITEM_STOCK_DETAILS>();
                List<dcITEM_STOCK_DETAILS> closingStkDtlList = new List<dcITEM_STOCK_DETAILS>();
                List<dcPRODUCTION_DTL> listDetails = GetBasicProductionDtlsByProdID(prodId, dc);

                foreach (var item in listDetails)
                {
                    dcITEM_STOCK_DETAILS formedItemRcv = new dcITEM_STOCK_DETAILS();
                    formedItemRcv.ITEM_ID = item.ITEM_ID;
                    formedItemRcv.UOM_ID = item.UOM_ID;
                    formedItemRcv.TRANS_DATE = prodObj.PRODUCTION_DATE;
                    formedItemRcv.TRANS_TIME = DateTime.Now;
                    formedItemRcv.INV_TRANS_DET_ID = item.PROD_DTL_ID;
                    formedItemRcv.TRANS_REF_NO = prodObj.PROD_NO;
                    formedItemRcv.STORE_ID = 0;
                    formedItemRcv.CREATE_BY = prm.user_id;
                    formedItemRcv.CREATE_DATE = DateTime.Now;
                    formedItemRcv.TRANS_REMARKS = "Production Charging Loading Receive";
                    formedItemRcv.FROM_DEPARTMENT_ID = prodObj.DEPT_ID;
                    formedItemRcv.TO_DEPARTMENT_ID = prodObj.DEPT_ID;
                    formedItemRcv.DEPARTMENT_ID = prodObj.DEPT_ID;
                    formedItemRcv.IS_PRODUCTION = "Y";
                    formedItemRcv.TRANS_QTY = item.ITEM_QTY;
                    formedItemRcv.RCV_QTY = item.ITEM_QTY;
                    formedItemRcv.ITEM_SPECIFICATION_ID = item.ITEM_SPECIFICATION_ID;
                    formedItemRcv.INV_TRANS_TYPE_ID = 1025;//Here solar loading code
                    ////formedItemRcv.INV_TRANS_TYPE_ID = 1001;
                    //switch (formedItemRcv.ITEM_SPECIFICATION_ID)
                    //{
                    //    case 9:
                         
                    //        break;
                    //    case 10:
                    //        formedItemRcv.INV_TRANS_TYPE_ID = 9005;
                    //        break;
                    //}                  
                    formedItemRcv.TRANS_DATE_TIME = prm.productiondate;                   
                    ITEM_STOCK_DETAILS_NBL.Insert(formedItemRcv, dc);

                }




                //#endregion


                //}
                dc.CommitTransaction(isTransInit);
            }
            catch
            {
                dc.RollbackTransaction();
            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }

            return msg;


        }


        #region MRB

        public static List<dcPRODUCTION_DTL> GetProductionMRBDtlsByProdID(int pProd_id, DBContext dc)
        {
            List<dcPRODUCTION_DTL> cObjList = new List<dcPRODUCTION_DTL>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder(GetProductionMRBDtlsSQLString());

                    if (pProd_id > 0)
                    {
                        sb.Append("  AND dtl.PROD_MST_ID=@PROD_MST_ID ");
                        cmdInfo.DBParametersInfo.Add("@PROD_MST_ID", pProd_id);
                    }

                    sb.Append("  AND dtl.IS_PACKING  IS NULL");

                    sb.Append(" ORDER BY dtl.SLNO");
                    DBQuery dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_DTL>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }


        public static string GetProductionMRBDtlsSQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" Select ");
            sb.Append(" dtl.PROD_MST_ID ");
            sb.Append(" ,dtl.PROD_DTL_ID ");
            sb.Append(" ,dtl.SLNO ");
            sb.Append(" ,inv.ITEM_NAME ");
            sb.Append(" ,inv.ITEM_ID ");
            sb.Append(" ,dtl.PANEL_PC ");
            sb.Append(" ,panel.UOM_NAME PANEL_UOM_NAME ");
            sb.Append(" ,dtl.ITEM_PANEL_QTY ");
            sb.Append(" ,dtl.PANEL_UOM_ID ");
            sb.Append(" ,dtl.ITEM_QTY ");
            sb.Append(" ,itmUOM.UOM_CODE_SHORT UOM_NAME ");
            sb.Append(" ,dtl.UOM_ID ");
            sb.Append(" ,dtl.ITEM_WEIGHT ");
            sb.Append(" ,weightUOM.UOM_CODE_SHORT WEIGHT_UOM_NAME ");
            sb.Append(" ,dtl.WEIGHT_UOM_ID ");
            sb.Append(" ,dtl.MACHINE_ID ");
            sb.Append(" ,mac.MACHINE_NAME ");
            sb.Append(" ,dtl.BOM_ID ");
            sb.Append(" ,bom.BOM_ITEM_DESC BOM_NAME ");
            sb.Append(" ,dtl.REMARKS ");
            sb.Append(" ,dtl.OPERATOR_ID ");
            sb.Append(" ,dtl.USED_BAR_PC ");
            sb.Append(" ,dtl.BAR_TYPE ");
            sb.Append(" ,dtl.USED_QTY_KG ");
            sb.Append(" ,dtl.BAR_WEIGHT ");
            sb.Append(" ,dtl.IS_PACKING,dtl.CHARGED_QTY,dtl.PACKING_QUANTITY ");
            sb.Append(" ,OpMst.FULL_NAME OPERATOR_NAME ");
            sb.Append(" ,bar.UOM_CODE_SHORT BAR_TYPE_NAME ");
            //sb.Append(" ,dtl.GRID_BATCH ");
            sb.Append(" ,dtl.FORMATION_STARTTIME ");
            sb.Append(" ,dtl.FORMATION_OFFTIME ");
            sb.Append(" ,dtl.FORMATION_OFFDATE ");
            sb.Append(" ,dtl.FORMED_QTY ");
            sb.Append(" ,dtl.UNFORMED_QTY ");
            sb.Append(" ,dtl.REJECT_QTY ");
            sb.Append(" ,dtl.AMPERE ");
            sb.Append(" ,dtl.CYCLETIME ");
            sb.Append(" ,dtl.SULFURIC_GRAVITY ");
            sb.Append(" ,dtl.TEMPARATURE ");
            sb.Append(" ,dtl.GRID_BATCH ");
            sb.Append(" ,dtl.PASTING_BATCH ");
            sb.Append(" ,dtl.FILLING_BATCH ");
            sb.Append(" ,dtl.SULPHATION_STARTTIME ");
            sb.Append(" ,dtl.SULPHATION_OFFDATE ");
            sb.Append(" ,dtl.SULPHATION_OFFTIME ");
            sb.Append(" ,dtl.USED_ITEM_ID,dtl.IS_UNFORMED ");
            sb.Append(" ,usedInv.ITEM_NAME USED_ITEM_NAME  ");
            sb.Append(" ,pwt.PASTE_PC_KG PASTE_PANEL_KG ");
            sb.Append(" ,gp.GRID_ITEM_ID USED_GRID_ID");
            sb.Append(" ,ginv.ITEM_NAME USED_GRID_NAME ");
            sb.Append(" ,inv.ITEM_STANDARD_WEIGHT_KG ");
            sb.Append(" ,dtl.SML_ITEM_PC,NVL(dtl.ITEM_SPECIFICATION_ID,0) ITEM_SPECIFICATION_ID ");
            sb.Append(" ,pwt.PASTE_PC_KG ITEM_STD_PASTE_KG ");
            sb.Append(" ,dtl.ITEM_WEIGHT_PASTE_KG ");
            sb.Append(" ,pinv.ITEM_NAME  MRB_PLATE_NAME ");
            sb.Append(" ,dtl.MRB_PLATE_ID  ");
            sb.Append(" ,dtl.MRB_PLATE_QTY  ");
            sb.Append(" ,dtl.MRB_PLATE_WEIGHT  ");
            sb.Append(" ,dtl.SCRAP_BATTERY_WEIGHT  ");
            sb.Append("  ,npinv.ITEM_NAME  MRB_PLATE_NAME_N  ");
            sb.Append("  ,dtl.MRB_PLATE_ID_N   ");
            sb.Append("  ,dtl.MRB_PLATE_QTY_N   ");
            sb.Append("  ,dtl.MRB_PLATE_WEIGHT_N  ");
            sb.Append(" FROM  ");
            sb.Append(" production_mst mst  ");
            sb.Append(" inner join  PRODUCTION_DTL dtl   on mst.PROD_ID=dtl.PROD_MST_ID ");
            sb.Append(" INNER JOIN INV_ITEM_MASTER inv ON dtl.ITEM_ID=inv.ITEM_ID ");
            sb.Append(" LEFT JOIN UOM_INFO itmUOM  ON dtl.UOM_ID=itmUOM.UOM_ID ");
            sb.Append(" LEFT JOIN MACHINE_MST mac ON dtl.MACHINE_ID=mac.MACHINE_ID ");
            sb.Append(" LEFT JOIN UOM_INFO panel on dtl.PANEL_UOM_ID=panel.UOM_ID ");
            sb.Append(" LEFT JOIN SUPPERVISOR_MST OpMst On dtl.OPERATOR_ID=OpMst.EMP_ID ");
            sb.Append(" LEFT JOIN BOM_MST_T bom ON dtl.BOM_ID=bom.BOM_ID ");
            sb.Append(" LEFT JOIN UOM_INFO weightUOM ON dtl.WEIGHT_UOM_ID=weightUOM.UOM_ID ");
            sb.Append(" LEFT JOIN UOM_INFO bar ON dtl.BAR_TYPE=bar.UOM_ID ");
            sb.Append(" LEFT JOIN INV_ITEM_MASTER usedInv ON dtl.USED_ITEM_ID=usedInv.ITEM_ID ");
            sb.Append(" LEFT JOIN  PROD_ITEM_STANDARD_WEIGHT pwt ON pwt.ITEM_ID=dtl.ITEM_ID  and pwt.DEPT_ID=mst.DEPT_ID ");
            sb.Append(" LEFT JOIN  PROD_PASTING_GRID_MAPPING gp ON gp.PASTE_ITEM_ID=inv.ITEM_ID ");
            sb.Append(" LEFT JOIN INV_ITEM_MASTER ginv ON gp.GRID_ITEM_ID=ginv.ITEM_ID  ");
            sb.Append(" LEFT JOIN INV_ITEM_MASTER pinv ON dtl.MRB_PLATE_ID=pinv.ITEM_ID  ");
            sb.Append(" LEFT JOIN INV_ITEM_MASTER npinv ON dtl.MRB_PLATE_ID_N=npinv.ITEM_ID  ");
            sb.Append(" WHERE 1=1 ");
            return sb.ToString();
        }

        public static string GetMRBDtlsByProdID_ItemID(int pProd_id, int pItem_id, int pMRB_PLATE_ID, DBContext dc)
        {
            //List<dcPRODUCTION_DTL> cObjList = new List<dcPRODUCTION_DTL>();
            string cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder(GetProductionEntryDtlSQLString());

                    if (pProd_id > 0)
                    {
                        sb.Append("  AND dtl.PROD_MST_ID=@PROD_MST_ID ");
                        cmdInfo.DBParametersInfo.Add("@PROD_MST_ID", pProd_id);
                    }

                    if (pItem_id > 0)
                    {
                        sb.Append("  AND dtl.item_id=@item_id ");
                        cmdInfo.DBParametersInfo.Add("@item_id", pItem_id);
                    }

                    if (pMRB_PLATE_ID > 0)
                    {
                        sb.Append("  AND dtl.MRB_PLATE_ID=@MRB_PLATE_ID ");
                        cmdInfo.DBParametersInfo.Add("@MRB_PLATE_ID", pMRB_PLATE_ID);
                    }

                    sb.Append(" ORDER BY dtl.SLNO");
                    DBQuery dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery(dbq, dc).Rows[0][0].ToString();
                    //
                    //DBQuery.ExecuteDBQuery(dbq, dc).ToString();
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }



        public static string GetNMRBDtlsByProdID_ItemID(int pProd_id, int pItem_id, int pMRB_PLATE_ID, DBContext dc)
        {
            //List<dcPRODUCTION_DTL> cObjList = new List<dcPRODUCTION_DTL>();
            string cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder(GetProductionEntryDtlSQLString());

                    if (pProd_id > 0)
                    {
                        sb.Append("  AND dtl.PROD_MST_ID=@PROD_MST_ID ");
                        cmdInfo.DBParametersInfo.Add("@PROD_MST_ID", pProd_id);
                    }

                    if (pItem_id > 0)
                    {
                        sb.Append("  AND dtl.item_id=@item_id ");
                        cmdInfo.DBParametersInfo.Add("@item_id", pItem_id);
                    }

                    if (pMRB_PLATE_ID > 0)
                    {
                        sb.Append("  AND dtl.MRB_PLATE_ID_N=@MRB_PLATE_ID ");
                        cmdInfo.DBParametersInfo.Add("@MRB_PLATE_ID", pMRB_PLATE_ID);
                    }

                    sb.Append(" ORDER BY dtl.SLNO");
                    DBQuery dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery(dbq, dc).Rows[0][0].ToString();
                    //
                    //DBQuery.ExecuteDBQuery(dbq, dc).ToString();
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        #endregion  


        #region VRLA Charging
        public static string AuthorizeVRLAChargingLoading(int prodId, clsPrmInventory prm, DBContext dc)
        {

            string msg = string.Empty;
            bool bStatus = false;
            bool isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            bool isTransInit = dc.StartTransaction();

            try
            {
                dcPRODUCTION_MST prodObj = PRODUCTION_MSTBL.GetProductionByProdID(prodId.ToString(), dc);
                if (prodObj != null)
                {
                    if (prodObj.AUTH_STATUS == "Y")
                    {
                        msg = "Already Authorised";
                        return msg;
                    }
                }
                else
                {
                    msg = "Invalid Production number.";
                    return msg;
                }
                PRODUCTION_MSTBL.UpdateAuthorized(prodId, prm.user_id, dc);
                List<dcITEM_STOCK_DETAILS> stkDtlList = new List<dcITEM_STOCK_DETAILS>();
                List<dcITEM_STOCK_DETAILS> closingStkDtlList = new List<dcITEM_STOCK_DETAILS>();
                List<dcPRODUCTION_DTL> listDetails = GetBasicProductionDtlsByProdID(prodId, dc);

                foreach (var item in listDetails)
                {
                    dcITEM_STOCK_DETAILS formedItemRcv = new dcITEM_STOCK_DETAILS();
                    formedItemRcv.ITEM_ID = item.ITEM_ID;
                    formedItemRcv.UOM_ID = item.UOM_ID;
                    formedItemRcv.TRANS_DATE = prodObj.PRODUCTION_DATE;
                    formedItemRcv.TRANS_TIME = DateTime.Now;
                    formedItemRcv.INV_TRANS_DET_ID = item.PROD_DTL_ID;
                    formedItemRcv.TRANS_REF_NO = prodObj.PROD_NO;
                    formedItemRcv.STORE_ID = 0;
                    formedItemRcv.CREATE_BY = prm.user_id;
                    formedItemRcv.CREATE_DATE = DateTime.Now;
                    formedItemRcv.TRANS_REMARKS = "VRLA CHARGING RECEIVE";
                    formedItemRcv.FROM_DEPARTMENT_ID = prodObj.DEPT_ID;
                    formedItemRcv.TO_DEPARTMENT_ID = prodObj.DEPT_ID;
                    formedItemRcv.DEPARTMENT_ID = prodObj.DEPT_ID;
                    formedItemRcv.IS_PRODUCTION = "Y";
                    formedItemRcv.TRANS_QTY = item.ITEM_QTY;
                    formedItemRcv.RCV_QTY = item.ITEM_QTY;
                    formedItemRcv.ITEM_SPECIFICATION_ID = item.ITEM_SPECIFICATION_ID;
                    formedItemRcv.INV_TRANS_TYPE_ID = 1024;//Here VRLA loading code
                    ////formedItemRcv.INV_TRANS_TYPE_ID = 1001;
                    //switch (formedItemRcv.ITEM_SPECIFICATION_ID)
                    //{
                    //    case 9:

                    //        break;
                    //    case 10:
                    //        formedItemRcv.INV_TRANS_TYPE_ID = 9005;
                    //        break;
                    //}                  
                    formedItemRcv.TRANS_DATE_TIME = prm.productiondate;
                    ITEM_STOCK_DETAILS_NBL.Insert(formedItemRcv, dc);

                }




                //#endregion


                //}
                dc.CommitTransaction(isTransInit);
            }
            catch
            {
                dc.RollbackTransaction();
            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }

            return msg;


        }

        public static string AuthorizeVRLAUnloadingEntry(int prodId, clsPrmInventory prm, DBContext dc)
        {
            string msg = string.Empty;
            bool bStatus = false;
            bool isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            bool isTransInit = dc.StartTransaction();

            try
            {
                dcPRODUCTION_MST prodObj = PRODUCTION_MSTBL.GetProductionByProdID(prodId.ToString(), dc);
                if (prodObj != null)
                {
                    if (prodObj.AUTH_STATUS == "Y")
                    {
                        msg = "Already Authorised";
                        return msg;
                    }
                }
                else
                {
                    msg = "Invalid Production number.";
                    return msg;
                }

                PRODUCTION_MSTBL.UpdateAuthorized(prodId, prm.user_id, dc);
                List<dcITEM_STOCK_DETAILS> stkDtlList = new List<dcITEM_STOCK_DETAILS>();
                List<dcITEM_STOCK_DETAILS> closingStkDtlList = new List<dcITEM_STOCK_DETAILS>();
                List<dcPRODUCTION_DTL> listDetails = GetSolarProductionDtlsByProdID(prodId, dc);

                foreach (var item in listDetails)
                {

                    if (item.CHARGED_QTY > 0)
                    {
                        dcITEM_STOCK_DETAILS formedItemRcv = new dcITEM_STOCK_DETAILS();
                        formedItemRcv.ITEM_ID = item.ITEM_ID;
                        formedItemRcv.UOM_ID = item.UOM_ID;
                        formedItemRcv.TRANS_DATE = prodObj.PRODUCTION_DATE;
                        formedItemRcv.TRANS_TIME = DateTime.Now;
                        formedItemRcv.INV_TRANS_DET_ID = item.PROD_DTL_ID;
                        formedItemRcv.TRANS_REF_NO = prodObj.PROD_NO;
                        formedItemRcv.STORE_ID = 0;
                        formedItemRcv.CREATE_BY = prm.user_id;
                        formedItemRcv.CREATE_DATE = DateTime.Now;
                        formedItemRcv.TRANS_REMARKS = "VRLA CHARGE UNLOADING RCV";
                        formedItemRcv.FROM_DEPARTMENT_ID = prodObj.DEPT_ID;
                        formedItemRcv.TO_DEPARTMENT_ID = prodObj.DEPT_ID;
                        formedItemRcv.DEPARTMENT_ID = prodObj.DEPT_ID;
                        formedItemRcv.IS_PRODUCTION = "Y";
                        formedItemRcv.TRANS_QTY = Conversion.StringToDecimal(item.CHARGED_QTY.ToString());
                        formedItemRcv.RCV_QTY = Conversion.StringToDecimal(item.CHARGED_QTY.ToString());
                        formedItemRcv.ITEM_SPECIFICATION_ID = item.ITEM_SPECIFICATION_ID;
                        formedItemRcv.INV_TRANS_TYPE_ID = 9008;
                        formedItemRcv.TRANS_DATE_TIME = prm.productiondate;
                        ITEM_STOCK_DETAILS_NBL.Insert(formedItemRcv, dc);
                    }

                    if (item.REJECT_QTY > 0)
                    {
                        dcITEM_STOCK_DETAILS formedItemRcv = new dcITEM_STOCK_DETAILS();
                        formedItemRcv.ITEM_ID = item.ITEM_ID;
                        formedItemRcv.UOM_ID = item.UOM_ID;
                        formedItemRcv.TRANS_DATE = prodObj.PRODUCTION_DATE;
                        formedItemRcv.TRANS_TIME = DateTime.Now;
                        formedItemRcv.INV_TRANS_DET_ID = item.PROD_DTL_ID;
                        formedItemRcv.TRANS_REF_NO = prodObj.PROD_NO;
                        formedItemRcv.STORE_ID = 0;
                        formedItemRcv.CREATE_BY = prm.user_id;
                        formedItemRcv.CREATE_DATE = DateTime.Now;
                        formedItemRcv.TRANS_REMARKS = "VRLA CHARGE UNLOADING REJ RCV";
                        formedItemRcv.FROM_DEPARTMENT_ID = prodObj.DEPT_ID;
                        formedItemRcv.TO_DEPARTMENT_ID = prodObj.DEPT_ID;
                        formedItemRcv.DEPARTMENT_ID = prodObj.DEPT_ID;
                        formedItemRcv.IS_PRODUCTION = "Y";
                        formedItemRcv.TRANS_QTY = item.REJECT_QTY;
                        formedItemRcv.RCV_QTY = item.REJECT_QTY;
                        formedItemRcv.ITEM_SPECIFICATION_ID = item.ITEM_SPECIFICATION_ID;
                        formedItemRcv.INV_TRANS_TYPE_ID = 1004;
                        formedItemRcv.TRANS_DATE_TIME = prm.productiondate;
                        ITEM_STOCK_DETAILS_NBL.Insert(formedItemRcv, dc);
                    }
                    if (item.IS_PACKING == "Y")
                    {
                        dcITEM_STOCK_DETAILS formedItemRcv = new dcITEM_STOCK_DETAILS();
                        formedItemRcv.ITEM_ID = item.ITEM_ID;
                        formedItemRcv.UOM_ID = item.UOM_ID;
                        formedItemRcv.TRANS_DATE = prodObj.PRODUCTION_DATE;
                        formedItemRcv.TRANS_TIME = DateTime.Now;
                        formedItemRcv.INV_TRANS_DET_ID = item.PROD_DTL_ID;
                        formedItemRcv.TRANS_REF_NO = prodObj.PROD_NO;
                        formedItemRcv.STORE_ID = 0;
                        formedItemRcv.CREATE_BY = prm.user_id;
                        formedItemRcv.CREATE_DATE = DateTime.Now;
                        formedItemRcv.TRANS_REMARKS = "VRLA Charging Packing Entry Receive";
                        formedItemRcv.FROM_DEPARTMENT_ID = prodObj.DEPT_ID;
                        formedItemRcv.TO_DEPARTMENT_ID = prodObj.DEPT_ID;
                        formedItemRcv.DEPARTMENT_ID = prodObj.DEPT_ID;
                        formedItemRcv.IS_PRODUCTION = "Y";
                        formedItemRcv.TRANS_QTY = Conversion.StringToDecimal(item.PACKING_QUANTITY.ToString());
                        formedItemRcv.RCV_QTY = Conversion.StringToDecimal(item.PACKING_QUANTITY.ToString());
                        formedItemRcv.ITEM_SPECIFICATION_ID = 11;
                        formedItemRcv.INV_TRANS_TYPE_ID = 1001;
                        formedItemRcv.TRANS_DATE_TIME = prm.productiondate;
                        ITEM_STOCK_DETAILS_NBL.Insert(formedItemRcv, dc);
                    }
                }

                //Raw material issue to production from formation department when loading
                List<dcPRODUCTION_FLOOR_CLOSING> listClosingDetls = PRODUCTION_FLOOR_CLOSINGBL.GetProductionClosingDtlsByProdID(prodId, dc);

                if (listClosingDetls != null && listClosingDetls.Any())
                {
                    foreach (var item in listClosingDetls)
                    {
                        // Stock Issue 
                        dcITEM_STOCK_DETAILS stkIssue = new dcITEM_STOCK_DETAILS();
                        stkIssue.ITEM_ID = item.CLOSING_ITEM_ID;
                        stkIssue.UOM_ID = item.CLOSING_UOM_ID;
                        stkIssue.TRANS_DATE = prodObj.PRODUCTION_DATE;
                        stkIssue.TRANS_TIME = DateTime.Now;
                        stkIssue.INV_TRANS_DET_ID = item.CLOSING_ID;
                        stkIssue.TRANS_QTY = item.ISSUE_STOCK;
                        stkIssue.ISS_QTY = item.ISSUE_STOCK;
                        stkIssue.TRANS_REF_NO = prodObj.PROD_NO;
                        stkIssue.STORE_ID = 0;
                        stkIssue.INV_TRANS_TYPE_ID = 1002;//1002 issue to production
                        stkIssue.CREATE_BY = prm.user_id;
                        stkIssue.CREATE_DATE = DateTime.Now;
                        stkIssue.TRANS_REMARKS = "VRLA Charging Packing Entry Issue";
                        stkIssue.FROM_DEPARTMENT_ID = prodObj.DEPT_ID;
                        stkIssue.TO_DEPARTMENT_ID = prodObj.DEPT_ID;
                        stkIssue.DEPARTMENT_ID = prodObj.DEPT_ID;
                        stkIssue.IS_PRODUCTION = "Y";
                        stkIssue.ITEM_SPECIFICATION_ID = 1;
                        ITEM_STOCK_DETAILS_NBL.Insert(stkIssue, dc);
                    }
                }


                //#endregion


                //}
                dc.CommitTransaction(isTransInit);
            }
            catch
            {
                dc.RollbackTransaction();
            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }

            return msg;


        }
        
        #endregion

        #region QA Details Data

        public static string GetProductionDtlsAfterQASQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" Select ");
            sb.Append(" dtl.PROD_MST_ID ");
            sb.Append(" ,dtl.PROD_DTL_ID ");
            sb.Append(" ,dtl.SLNO ");
            sb.Append(" ,inv.ITEM_NAME ");
            sb.Append(" ,inv.ITEM_ID ");
            sb.Append(" ,dtl.PANEL_PC ");
            sb.Append(" ,panel.UOM_NAME PANEL_UOM_NAME ");
            sb.Append(" ,dtl.ITEM_PANEL_QTY ");
            sb.Append(" ,dtl.PANEL_UOM_ID ");
            sb.Append(" ,dtl.ITEM_QTY ");
            sb.Append(" ,itmUOM.UOM_CODE_SHORT UOM_NAME ");
            sb.Append(" ,dtl.UOM_ID ");
            sb.Append(" ,dtl.ITEM_WEIGHT ");
            sb.Append(" ,weightUOM.UOM_CODE_SHORT WEIGHT_UOM_NAME ");
            sb.Append(" ,dtl.WEIGHT_UOM_ID ");
            sb.Append(" ,dtl.MACHINE_ID ");
            sb.Append(" ,mac.MACHINE_NAME ");
            sb.Append(" ,dtl.BOM_ID ");
            sb.Append(" ,bom.BOM_ITEM_DESC BOM_NAME ");
            sb.Append(" ,dtl.REMARKS ");
            sb.Append(" ,dtl.OPERATOR_ID ");
            sb.Append(" ,dtl.USED_BAR_PC ");
            sb.Append(" ,dtl.BAR_TYPE ");
            sb.Append(" ,dtl.USED_QTY_KG ");
            sb.Append(" ,dtl.BAR_WEIGHT ");
            sb.Append(" ,dtl.IS_PACKING,dtl.CHARGED_QTY,dtl.PACKING_QUANTITY ");
            sb.Append(" ,OpMst.FULL_NAME OPERATOR_NAME ");
            sb.Append(" ,bar.UOM_CODE_SHORT BAR_TYPE_NAME ");
            //sb.Append(" ,dtl.GRID_BATCH ");
            sb.Append(" ,dtl.FORMATION_STARTTIME ");
            sb.Append(" ,dtl.FORMATION_OFFTIME ");
            sb.Append(" ,dtl.FORMATION_OFFDATE ");
            sb.Append(" ,dtl.FORMED_QTY ");
            sb.Append(" ,dtl.UNFORMED_QTY ");
            sb.Append(" ,dtl.REJECT_QTY ");
            sb.Append(" ,dtl.AMPERE ");
            sb.Append(" ,dtl.CYCLETIME ");
            sb.Append(" ,dtl.SULFURIC_GRAVITY ");
            sb.Append(" ,dtl.TEMPARATURE ");
            sb.Append(" ,dtl.GRID_BATCH ");
            sb.Append(" ,dtl.PASTING_BATCH ");
            sb.Append(" ,dtl.FILLING_BATCH ");
            sb.Append(" ,dtl.SULPHATION_STARTTIME ");
            sb.Append(" ,dtl.SULPHATION_OFFDATE ");
            sb.Append(" ,dtl.SULPHATION_OFFTIME ");
            sb.Append(" ,dtl.USED_ITEM_ID,dtl.IS_UNFORMED ");
            sb.Append(" ,usedInv.ITEM_NAME USED_ITEM_NAME  ");
            sb.Append(" ,pwt.PASTE_PC_KG PASTE_PANEL_KG ");
            sb.Append(" ,gp.GRID_ITEM_ID USED_GRID_ID");
            sb.Append(" ,ginv.ITEM_NAME USED_GRID_NAME ");
            sb.Append(" ,inv.ITEM_STANDARD_WEIGHT_KG ");
            sb.Append(" ,dtl.SML_ITEM_PC,NVL(dtl.ITEM_SPECIFICATION_ID,0) ITEM_SPECIFICATION_ID ");
            sb.Append(" ,pwt.PASTE_PC_KG ITEM_STD_PASTE_KG ");
            sb.Append(" ,dtl.ITEM_WEIGHT_PASTE_KG,dtl.PROD_BATCH_NO_DTL ");

            sb.Append(" FROM  ");
            sb.Append(" production_mst mst  ");
            sb.Append(" inner join  PRODUCTION_DTL dtl   on mst.PROD_ID=dtl.PROD_MST_ID ");
            sb.Append(" INNER JOIN INV_ITEM_MASTER inv ON dtl.ITEM_ID=inv.ITEM_ID ");
            sb.Append(" INNER JOIN UOM_INFO itmUOM  ON dtl.UOM_ID=itmUOM.UOM_ID ");
            sb.Append(" LEFT JOIN MACHINE_MST mac ON dtl.MACHINE_ID=mac.MACHINE_ID ");
            sb.Append(" LEFT JOIN UOM_INFO panel on dtl.PANEL_UOM_ID=panel.UOM_ID ");
            sb.Append(" LEFT JOIN SUPPERVISOR_MST OpMst On dtl.OPERATOR_ID=OpMst.EMP_ID ");
            sb.Append(" LEFT JOIN BOM_MST_T bom ON dtl.BOM_ID=bom.BOM_ID ");
            sb.Append(" LEFT JOIN UOM_INFO weightUOM ON dtl.WEIGHT_UOM_ID=weightUOM.UOM_ID ");
            sb.Append(" LEFT JOIN UOM_INFO bar ON dtl.BAR_TYPE=bar.UOM_ID ");
            sb.Append(" LEFT JOIN INV_ITEM_MASTER usedInv ON dtl.USED_ITEM_ID=usedInv.ITEM_ID ");
            sb.Append(" LEFT JOIN  PROD_ITEM_STANDARD_WEIGHT pwt ON pwt.ITEM_ID=dtl.ITEM_ID  and pwt.DEPT_ID=mst.DEPT_ID ");
            sb.Append(" LEFT JOIN  PROD_PASTING_GRID_MAPPING gp ON gp.PASTE_ITEM_ID=inv.ITEM_ID ");
            sb.Append(" LEFT JOIN INV_ITEM_MASTER ginv ON gp.GRID_ITEM_ID=ginv.ITEM_ID  ");
            sb.Append(" INNER JOIN PROD_QA_OPERATION_DTL qad ON dtl.PROD_DTL_ID=qad.PROD_DTL_ID ");
            sb.Append(" INNER JOIN PROD_QA_OPERATION_MST prod_qa ON qad.PROD_QA_ID=prod_qa.PROD_QA_ID  ");
            sb.Append(" WHERE 1=1 AND  dtl.IS_QA_PASS='Y' ");
            return sb.ToString();
        }

        //After Save
        public static List<dcPRODUCTION_DTL> GetProductionDtlsByProdIDAfterQA(int pProd_id,int pProdQAID, DBContext dc)
        {
            List<dcPRODUCTION_DTL> cObjList = new List<dcPRODUCTION_DTL>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder(GetProductionDtlsAfterQASQLString());

                    if (pProd_id > 0)
                    {
                        sb.Append("  AND dtl.PROD_MST_ID=@PROD_MST_ID ");
                        cmdInfo.DBParametersInfo.Add("@PROD_MST_ID", pProd_id);
                    }
                    sb.Append("  AND prod_qa.PROD_QA_ID=@pProdQAID ");
                    cmdInfo.DBParametersInfo.Add("@pProdQAID", pProdQAID);
                    sb.Append("  AND dtl.IS_PACKING  IS NULL");

                    sb.Append(" ORDER BY dtl.SLNO");
                    DBQuery dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_DTL>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        //After Save Then Pending List

        public static string GetProductionDtlsAfterQAPendingSQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" Select ");
            sb.Append(" dtl.PROD_MST_ID ");
            sb.Append(" ,dtl.PROD_DTL_ID ");
            sb.Append(" ,dtl.SLNO ");
            sb.Append(" ,inv.ITEM_NAME ");
            sb.Append(" ,inv.ITEM_ID ");
            sb.Append(" ,dtl.PANEL_PC ");
            sb.Append(" ,panel.UOM_NAME PANEL_UOM_NAME ");
            sb.Append(" ,dtl.ITEM_PANEL_QTY ");
            sb.Append(" ,dtl.PANEL_UOM_ID ");
            sb.Append(" ,dtl.ITEM_QTY ");
            sb.Append(" ,itmUOM.UOM_CODE_SHORT UOM_NAME ");
            sb.Append(" ,dtl.UOM_ID ");
            sb.Append(" ,dtl.ITEM_WEIGHT ");
            sb.Append(" ,weightUOM.UOM_CODE_SHORT WEIGHT_UOM_NAME ");
            sb.Append(" ,dtl.WEIGHT_UOM_ID ");
            sb.Append(" ,dtl.MACHINE_ID ");
            sb.Append(" ,mac.MACHINE_NAME ");
            sb.Append(" ,dtl.BOM_ID ");
            sb.Append(" ,bom.BOM_ITEM_DESC BOM_NAME ");
            sb.Append(" ,dtl.REMARKS ");
            sb.Append(" ,dtl.OPERATOR_ID ");
            sb.Append(" ,dtl.USED_BAR_PC ");
            sb.Append(" ,dtl.BAR_TYPE ");
            sb.Append(" ,dtl.USED_QTY_KG ");
            sb.Append(" ,dtl.BAR_WEIGHT ");
            sb.Append(" ,dtl.IS_PACKING,dtl.CHARGED_QTY,dtl.PACKING_QUANTITY ");
            sb.Append(" ,OpMst.FULL_NAME OPERATOR_NAME ");
            sb.Append(" ,bar.UOM_CODE_SHORT BAR_TYPE_NAME ");
            //sb.Append(" ,dtl.GRID_BATCH ");
            sb.Append(" ,dtl.FORMATION_STARTTIME ");
            sb.Append(" ,dtl.FORMATION_OFFTIME ");
            sb.Append(" ,dtl.FORMATION_OFFDATE ");
            sb.Append(" ,dtl.FORMED_QTY ");
            sb.Append(" ,dtl.UNFORMED_QTY ");
            sb.Append(" ,dtl.REJECT_QTY ");
            sb.Append(" ,dtl.AMPERE ");
            sb.Append(" ,dtl.CYCLETIME ");
            sb.Append(" ,dtl.SULFURIC_GRAVITY ");
            sb.Append(" ,dtl.TEMPARATURE ");
            sb.Append(" ,dtl.GRID_BATCH ");
            sb.Append(" ,dtl.PASTING_BATCH ");
            sb.Append(" ,dtl.FILLING_BATCH ");
            sb.Append(" ,dtl.SULPHATION_STARTTIME ");
            sb.Append(" ,dtl.SULPHATION_OFFDATE ");
            sb.Append(" ,dtl.SULPHATION_OFFTIME ");
            sb.Append(" ,dtl.USED_ITEM_ID,dtl.IS_UNFORMED ");
            sb.Append(" ,usedInv.ITEM_NAME USED_ITEM_NAME  ");
            sb.Append(" ,pwt.PASTE_PC_KG PASTE_PANEL_KG ");
            sb.Append(" ,gp.GRID_ITEM_ID USED_GRID_ID");
            sb.Append(" ,ginv.ITEM_NAME USED_GRID_NAME ");
            sb.Append(" ,inv.ITEM_STANDARD_WEIGHT_KG ");
            sb.Append(" ,dtl.SML_ITEM_PC,NVL(dtl.ITEM_SPECIFICATION_ID,0) ITEM_SPECIFICATION_ID ");
            sb.Append(" ,pwt.PASTE_PC_KG ITEM_STD_PASTE_KG ");
            sb.Append(" ,dtl.ITEM_WEIGHT_PASTE_KG,dtl.PROD_BATCH_NO_DTL ");

            sb.Append(" FROM  ");
            sb.Append(" production_mst mst  ");
            sb.Append(" inner join  PRODUCTION_DTL dtl   on mst.PROD_ID=dtl.PROD_MST_ID ");
            sb.Append(" INNER JOIN INV_ITEM_MASTER inv ON dtl.ITEM_ID=inv.ITEM_ID ");
            sb.Append(" INNER JOIN UOM_INFO itmUOM  ON dtl.UOM_ID=itmUOM.UOM_ID ");
            sb.Append(" LEFT JOIN MACHINE_MST mac ON dtl.MACHINE_ID=mac.MACHINE_ID ");
            sb.Append(" LEFT JOIN UOM_INFO panel on dtl.PANEL_UOM_ID=panel.UOM_ID ");
            sb.Append(" LEFT JOIN SUPPERVISOR_MST OpMst On dtl.OPERATOR_ID=OpMst.EMP_ID ");
            sb.Append(" LEFT JOIN BOM_MST_T bom ON dtl.BOM_ID=bom.BOM_ID ");
            sb.Append(" LEFT JOIN UOM_INFO weightUOM ON dtl.WEIGHT_UOM_ID=weightUOM.UOM_ID ");
            sb.Append(" LEFT JOIN UOM_INFO bar ON dtl.BAR_TYPE=bar.UOM_ID ");
            sb.Append(" LEFT JOIN INV_ITEM_MASTER usedInv ON dtl.USED_ITEM_ID=usedInv.ITEM_ID ");
            sb.Append(" LEFT JOIN  PROD_ITEM_STANDARD_WEIGHT pwt ON pwt.ITEM_ID=dtl.ITEM_ID  and pwt.DEPT_ID=mst.DEPT_ID ");
            sb.Append(" LEFT JOIN  PROD_PASTING_GRID_MAPPING gp ON gp.PASTE_ITEM_ID=inv.ITEM_ID ");
            sb.Append(" LEFT JOIN INV_ITEM_MASTER ginv ON gp.GRID_ITEM_ID=ginv.ITEM_ID  ");

            sb.Append(" WHERE 1=1 AND  dtl.IS_QA_PASS='N' ");
            return sb.ToString();
        }

        public static List<dcPRODUCTION_DTL> GetProductionDtlsByProdIDAfterSavePending(int pProd_id, DBContext dc)
        {
            List<dcPRODUCTION_DTL> cObjList = new List<dcPRODUCTION_DTL>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder(GetProductionDtlsAfterQAPendingSQLString());

                    if (pProd_id > 0)
                    {
                        sb.Append("  AND dtl.PROD_MST_ID=@PROD_MST_ID ");
                        cmdInfo.DBParametersInfo.Add("@PROD_MST_ID", pProd_id);
                    }

                    sb.Append("  AND dtl.IS_PACKING  IS NULL");

                    sb.Append(" ORDER BY dtl.SLNO");
                    DBQuery dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_DTL>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }



        #endregion

        public static dcPRODUCTION_DTL GetFormationProductionInfoByDtlBatch(int pProdDtlId, string pBatchNoDtl, DBContext dc)
        {
            dcPRODUCTION_DTL cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetProductionEntryDtlSQLString());

                if(pProdDtlId > 0)
                {
                    sb.Append(" AND dtl.PROD_DTL_ID != @pProdDtlId ");
                    cmdInfo.DBParametersInfo.Add("@pProdDtlId", pProdDtlId);

                }

                if (pBatchNoDtl != "")
                {
                    sb.Append(" AND UPPER(dtl.PROD_BATCH_NO_DTL)=@pBatchNoDtl ");
                    cmdInfo.DBParametersInfo.Add("@pBatchNoDtl", pBatchNoDtl.ToUpper());
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcPRODUCTION_DTL>(dbq, dc).FirstOrDefault();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
    }
}
