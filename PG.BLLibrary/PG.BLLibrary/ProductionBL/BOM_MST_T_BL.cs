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
    public class BOM_MST_TBL
    {

        public static string GetBOMinfobyBOMIDString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT   ");
            sb.Append(" mst.*   ");
            sb.Append(" ,itm.ITEM_NAME  ");
            sb.Append(" ,dpt.DEPARTMENT_NAME  ");
            sb.Append(" from BOM_MST_T mst  ");
            sb.Append(" INNER JOIN INV_ITEM_MASTER itm ON mst.BOM_ITEM_ID = itm.ITEM_ID  ");
            sb.Append(" INNER JOIN DEPARTMENT_INFO dpt ON mst.FROM_DEPARTMENT_ID = dpt.DEPARTMENT_ID  ");
            sb.Append(" Where 1=1 ");
            return sb.ToString();
        }


        public static string GetItemWithGridWeightList()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT   INV_ITEM_MASTER.ITEM_ID ");
            sb.Append(" ,INV_ITEM_MASTER.ITEM_NAME ");
            sb.Append(" ,UOM_INFO.UOM_CODE_SHORT UOM_NAME ");
            sb.Append(" ,UOM_INFO.UOM_ID ");
            sb.Append(" ,INV_ITEM_MASTER.IS_PRIME ");
            sb.Append(" ,pUOM_INFO.PCS_QTY  PANEL_PC ");
            sb.Append(" ,INV_ITEM_MASTER.ITEM_STANDARD_WEIGHT_KG");
            sb.Append(" FROM  ");
            sb.Append(" INV_ITEM_MASTER  ");
            sb.Append(" INNER JOIN INV_ITEM_GROUP ON INV_ITEM_MASTER.ITEM_GROUP_ID = INV_ITEM_GROUP.ITEM_GROUP_ID  ");
            sb.Append(" INNER JOIN INV_ITEM_CLASS ON INV_ITEM_MASTER.ITEM_CLASS_ID = INV_ITEM_CLASS.ITEM_CLASS_ID  ");
            sb.Append(" INNER JOIN UOM_INFO ON INV_ITEM_MASTER.UOM_ID = UOM_INFO.UOM_ID  ");
            sb.Append(" INNER JOIN  PRO_DEPARTMENT_ITEM dept ON dept.ITEM_ID=INV_ITEM_MASTER.ITEM_ID  AND dept.dept_id=135");
            sb.Append(" LEFT JOIN  UOM_INFO pUOM_INFO ON INV_ITEM_MASTER.PANEL_PC_UOM = pUOM_INFO.UOM_ID ");
            sb.Append(" Where 1=1 ");
            return sb.ToString();
        }

        public static string GetRMItem_RejectItemList()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT   INV_ITEM_MASTER.ITEM_ID  ");
            sb.Append(" ,INV_ITEM_MASTER.ITEM_NAME  ");
            sb.Append(" ,UOM_INFO.UOM_CODE_SHORT UOM_NAME  ");
            sb.Append(" ,UOM_INFO.UOM_ID  ");
            sb.Append(" ,rj.TO_REJ_ITEM_ID ");
            sb.Append(" ,toRj.ITEM_NAME TO_REJ_ITEM_NAME ");
            sb.Append(" FROM   ");
            sb.Append(" INV_ITEM_MASTER   ");
            sb.Append(" INNER JOIN UOM_INFO ON INV_ITEM_MASTER.UOM_ID = UOM_INFO.UOM_ID   ");
            sb.Append(" INNER JOIN  PRO_DEPARTMENT_ITEM dept ON dept.ITEM_ID=INV_ITEM_MASTER.ITEM_ID    ");
            sb.Append(" INNER JOIN PROD_REJECT_ITEM_MAPPING rj ON INV_ITEM_MASTER.item_id=rj.FROM_REJ_ITEM_ID ");
            sb.Append(" INNER JOIN  INV_ITEM_MASTER toRj ON rj.TO_REJ_ITEM_ID=toRj.ITEM_ID ");
            //sb.Append(" AND dept.DEPT_ID=136 ");
            //sb.Append(" AND dept.IS_FINISHED='N' ");
            sb.Append(" Where 1=1 ");
            return sb.ToString();
        }


        public static string GetItemWithPastedGridWeightList()
        {
            StringBuilder sb = new StringBuilder();
                sb.Append(" SELECT   INV_ITEM_MASTER.ITEM_ID ");
                sb.Append(" ,INV_ITEM_MASTER.ITEM_NAME ");
                sb.Append(" ,UOM_INFO.UOM_CODE_SHORT UOM_NAME "); 
                sb.Append(" ,UOM_INFO.UOM_ID ");
                sb.Append(" ,INV_ITEM_MASTER.IS_PRIME "); 
                sb.Append(" ,pUOM_INFO.PCS_QTY  PANEL_PC "); 
                sb.Append(" ,FN_GET_PASTED_PLATE_WT(dept.DEPT_ID,dept.ITEM_ID) ITEM_STANDARD_WEIGHT_KG");
                sb.Append(" FROM  ");
                sb.Append(" INV_ITEM_MASTER  ");
                sb.Append(" INNER JOIN INV_ITEM_GROUP ON INV_ITEM_MASTER.ITEM_GROUP_ID = INV_ITEM_GROUP.ITEM_GROUP_ID  ");
                sb.Append(" INNER JOIN INV_ITEM_CLASS ON INV_ITEM_MASTER.ITEM_CLASS_ID = INV_ITEM_CLASS.ITEM_CLASS_ID  ");
                sb.Append(" INNER JOIN UOM_INFO ON INV_ITEM_MASTER.UOM_ID = UOM_INFO.UOM_ID  ");
                sb.Append(" INNER JOIN  PRO_DEPARTMENT_ITEM dept ON dept.ITEM_ID=INV_ITEM_MASTER.ITEM_ID AND dept.dept_id IN (7,18) ");
                sb.Append(" LEFT JOIN  UOM_INFO pUOM_INFO ON INV_ITEM_MASTER.PANEL_PC_UOM = pUOM_INFO.UOM_ID "); 
                sb.Append(" LEFT JOIN PROD_ITEM_STANDARD_WEIGHT PISW ON INV_ITEM_MASTER.ITEM_ID=PISW.ITEM_ID ");
                sb.Append(" Where 1=1 AND dept.DEPT_ID IN (18,7)");
            return sb.ToString();
        }
        public static string GetBOMItemList_SQLString()
        {
            StringBuilder sb = new StringBuilder();

               sb.Append(" Select  inv.ITEM_ID ");
                sb.Append("  ,inv.ITEM_NAME ");
                sb.Append(" ,inv.ITEM_CODE,inv.IS_BATCH ");
                sb.Append(" , INV_ITEM_GROUP.ITEM_GROUP_CODE, INV_ITEM_GROUP.ITEM_GROUP_NAME   ");
                sb.Append(" , INV_ITEM_CLASS.ITEM_CLASS_CODE, INV_ITEM_CLASS.ITEM_CLASS_NAME   ");
                //sb.Append(" , INV_ITEM_TYPE.ITEM_TYPE_CODE, INV_ITEM_TYPE.ITEM_TYPE_NAME  ");
                //sb.Append(" ,GET_DEPT_WISE_CLOSING_QTY(inv.ITEM_ID,dept.DEPT_ID) CLOSING_QTY ");
                sb.Append(" ,GET_DEPARTMENT_CLOSING_QTY(inv.ITEM_ID,dept.DEPT_ID) CLOSING_QTY ");
                sb.Append(" ,UOM_INFO.UOM_CODE_SHORT UOM_NAME ");
                sb.Append(" ,UOM_INFO.UOM_ID ");
                sb.Append(" ,bmst.BOM_ID ");
                sb.Append(" ,bmst.BOM_NO ");
                sb.Append(" ,bmst.BOM_ITEM_DESC ");
                sb.Append(" ,inv.IS_PRIME ");
                sb.Append(",NVL(  Puom.UOM_NAME , 'Panel-2') PANEL_UOM_NAME  ");
                sb.Append(",NVL( inv.PANEL_PC_UOM ,'39') PANEL_UOM_ID  ");
                sb.Append(",NVL( Puom.PCS_QTY,2)  PANEL_PC ");
                //sb.Append(" ,'Panel-2' PANEL_UOM_NAME ");
                //sb.Append(" ,'39' PANEL_UOM_ID ");
                //sb.Append(" ,'2' PANEL_PC ");
                sb.Append(" ,inv.ITEM_ORDER ");
                sb.Append(" ,inv.ITEM_STANDARD_WEIGHT_KG ");
                sb.Append(" ,pwt.PASTE_PANEL_KG ");
                sb.Append(" ,gp.GRID_ITEM_ID USED_GRID_ID");
                sb.Append(" ,ginv.ITEM_NAME USED_GRID_NAME ");
                sb.Append(" ,pwt.PASTE_PC_KG ");
                sb.Append(" ,inv.WEIGHTED_AVERAGE_PRICE, inv.IS_OUTSALE_CASH,gt.FALSE_LUG_ITEM_ID,gt.FALSE_LUG_NAME ");
                //sb.Append(" ,(SELECT FALSE_LUG_ITEM_ID FROM GRID_CASTING_BOM_TEMP WHERE PARENT_ITEM_ID=inv.ITEM_ID) FALSE_LUG_ITEM_ID ");,gt.FALSE_LUG_ITEM_ID,gt.FALSE_LUG_NAME 
                //sb.Append(" ,(SELECT FALSE_LUG_NAME FROM GRID_CASTING_BOM_TEMP WHERE PARENT_ITEM_ID=inv.ITEM_ID) FALSE_LUG_NAME ");
                //sb.Append("  ,(select ITEM_NAME  from INV_ITEM_MASTER WHERE item_id=gp.GRID_ITEM_ID ) USED_GRID_NAME ");
                sb.Append(" FROM INV_ITEM_MASTER inv ");
                sb.Append(" INNER JOIN INV_ITEM_GROUP ON inv.ITEM_GROUP_ID = INV_ITEM_GROUP.ITEM_GROUP_ID  ");
                sb.Append(" INNER JOIN INV_ITEM_CLASS ON inv.ITEM_CLASS_ID = INV_ITEM_CLASS.ITEM_CLASS_ID  ");
                //sb.Append(" INNER JOIN INV_ITEM_TYPE ON inv.ITEM_TYPE_ID = INV_ITEM_TYPE.ITEM_TYPE_ID  ");
                sb.Append(" INNER JOIN UOM_INFO ON inv.UOM_ID = UOM_INFO.UOM_ID  ");
                sb.Append(" INNER JOIN  PRO_DEPARTMENT_ITEM dept ON dept.ITEM_ID=inv.ITEM_ID  ");
                sb.Append(" LEFT JOIN UOM_INFO Puom ON inv.PANEL_PC_UOM=Puom.UOM_ID ");
                sb.Append(" LEFT JOIN   BOM_MST_T bmst ON inv.ITEM_ID = bmst.BOM_ITEM_ID and bmst.ISACTIVE = 'Y' AND bmst.FROM_DEPARTMENT_ID=dept.DEPT_ID");
                sb.Append(" LEFT JOIN  PROD_ITEM_STANDARD_WEIGHT pwt ON pwt.ITEM_ID=inv.ITEM_ID AND pwt.DEPT_ID=dept.DEPT_ID ");
                sb.Append(" LEFT JOIN  PROD_PASTING_GRID_MAPPING gp ON gp.PASTE_ITEM_ID=inv.ITEM_ID ");
                sb.Append(" LEFT JOIN INV_ITEM_MASTER ginv ON gp.GRID_ITEM_ID=ginv.ITEM_ID  ");
                sb.Append(" LEFT JOIN GRID_CASTING_BOM_TEMP gt ON inv.ITEM_ID=gt.PARENT_ITEM_ID AND gt.FALSE_LUG_ITEM_ID IS NOT NULL  ");
                sb.Append(" Where 1=1  ");

            return sb.ToString();
        }

        public static string GetBOMItemListByBtyType_SQLString(int pBattTypeId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" Select  inv.ITEM_ID ");
            sb.Append("  ,inv.ITEM_NAME ");
            sb.Append(" ,inv.ITEM_CODE,inv.IS_BATCH,inv.ITEM_GROUP_ID ");
            sb.Append(" , INV_ITEM_GROUP.ITEM_GROUP_CODE, INV_ITEM_GROUP.ITEM_GROUP_NAME   ");
            sb.Append(" , INV_ITEM_CLASS.ITEM_CLASS_CODE, INV_ITEM_CLASS.ITEM_CLASS_NAME   ");
            //sb.Append(" , INV_ITEM_TYPE.ITEM_TYPE_CODE, INV_ITEM_TYPE.ITEM_TYPE_NAME  ");
            //sb.Append(" ,GET_DEPT_WISE_CLOSING_QTY(inv.ITEM_ID,dept.DEPT_ID) CLOSING_QTY ");
            if(pBattTypeId > 0)
            {
                sb.Append(" ,GET_DEPT_CLOSING_BY_BAT_TYPE(inv.ITEM_ID,dept.DEPT_ID," + pBattTypeId + ") CLOSING_QTY ");
            }
            else
            {
                sb.Append("  ,CASE WHEN INV.ITEM_GROUP_ID=62 THEN GET_DEPT_REJECT_CLOSING(inv.ITEM_ID,dept.DEPT_ID) ELSE GET_DEPARTMENT_CLOSING_QTY(inv.ITEM_ID,dept.DEPT_ID)END CLOSING_QTY ");
                //sb.Append(" ,GET_DEPARTMENT_CLOSING_QTY(inv.ITEM_ID,dept.DEPT_ID) CLOSING_QTY ");
            }
         
            sb.Append(" ,UOM_INFO.UOM_CODE_SHORT UOM_NAME ");
            sb.Append(" ,UOM_INFO.UOM_ID ");
            sb.Append(" ,bmst.BOM_ID ");
            sb.Append(" ,bmst.BOM_NO ");
            sb.Append(" ,bmst.BOM_ITEM_DESC ");
            sb.Append(" ,inv.IS_PRIME ");
            sb.Append(",NVL(  Puom.UOM_NAME , 'Panel-2') PANEL_UOM_NAME  ");
            sb.Append(",NVL( inv.PANEL_PC_UOM ,'39') PANEL_UOM_ID  ");
            sb.Append(",NVL( Puom.PCS_QTY,2)  PANEL_PC ");
            //sb.Append(" ,'Panel-2' PANEL_UOM_NAME ");
            //sb.Append(" ,'39' PANEL_UOM_ID ");
            //sb.Append(" ,'2' PANEL_PC ");
            sb.Append(" ,inv.ITEM_ORDER ");
            sb.Append(" ,inv.ITEM_STANDARD_WEIGHT_KG ");
            sb.Append(" ,pwt.PASTE_PANEL_KG ");
            sb.Append(" ,gp.GRID_ITEM_ID USED_GRID_ID");
            sb.Append(" ,ginv.ITEM_NAME USED_GRID_NAME ");
            sb.Append(" ,pwt.PASTE_PC_KG ");
            sb.Append(" ,inv.WEIGHTED_AVERAGE_PRICE, inv.IS_OUTSALE_CASH,gt.FALSE_LUG_ITEM_ID,gt.FALSE_LUG_NAME ");
            //sb.Append(" ,(SELECT FALSE_LUG_ITEM_ID FROM GRID_CASTING_BOM_TEMP WHERE PARENT_ITEM_ID=inv.ITEM_ID) FALSE_LUG_ITEM_ID ");,gt.FALSE_LUG_ITEM_ID,gt.FALSE_LUG_NAME 
            //sb.Append(" ,(SELECT FALSE_LUG_NAME FROM GRID_CASTING_BOM_TEMP WHERE PARENT_ITEM_ID=inv.ITEM_ID) FALSE_LUG_NAME ");
            //sb.Append("  ,(select ITEM_NAME  from INV_ITEM_MASTER WHERE item_id=gp.GRID_ITEM_ID ) USED_GRID_NAME ");
            sb.Append(" FROM INV_ITEM_MASTER inv ");
            sb.Append(" INNER JOIN INV_ITEM_GROUP ON inv.ITEM_GROUP_ID = INV_ITEM_GROUP.ITEM_GROUP_ID  ");
            sb.Append(" INNER JOIN INV_ITEM_CLASS ON inv.ITEM_CLASS_ID = INV_ITEM_CLASS.ITEM_CLASS_ID  ");
            //sb.Append(" INNER JOIN INV_ITEM_TYPE ON inv.ITEM_TYPE_ID = INV_ITEM_TYPE.ITEM_TYPE_ID  ");
            sb.Append(" INNER JOIN UOM_INFO ON inv.UOM_ID = UOM_INFO.UOM_ID  ");
            sb.Append(" INNER JOIN  PRO_DEPARTMENT_ITEM dept ON dept.ITEM_ID=inv.ITEM_ID  ");
            sb.Append(" LEFT JOIN UOM_INFO Puom ON inv.PANEL_PC_UOM=Puom.UOM_ID ");
            sb.Append(" LEFT JOIN   BOM_MST_T bmst ON inv.ITEM_ID = bmst.BOM_ITEM_ID and bmst.ISACTIVE = 'Y' AND bmst.FROM_DEPARTMENT_ID=dept.DEPT_ID");
            sb.Append(" LEFT JOIN  PROD_ITEM_STANDARD_WEIGHT pwt ON pwt.ITEM_ID=inv.ITEM_ID AND pwt.DEPT_ID=dept.DEPT_ID ");
            sb.Append(" LEFT JOIN  PROD_PASTING_GRID_MAPPING gp ON gp.PASTE_ITEM_ID=inv.ITEM_ID ");
            sb.Append(" LEFT JOIN INV_ITEM_MASTER ginv ON gp.GRID_ITEM_ID=ginv.ITEM_ID  ");
            sb.Append(" LEFT JOIN GRID_CASTING_BOM_TEMP gt ON inv.ITEM_ID=gt.PARENT_ITEM_ID AND gt.FALSE_LUG_ITEM_ID IS NOT NULL  ");
            sb.Append(" Where 1=1  ");

            return sb.ToString();
        }


        public static string GetBOMItemList_SQLString(int DeptId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" Select  inv.ITEM_ID ");
            sb.Append("  ,inv.ITEM_NAME ");
            sb.Append(" ,inv.ITEM_CODE,inv.IS_BATCH ");
            sb.Append(" , INV_ITEM_GROUP.ITEM_GROUP_CODE, INV_ITEM_GROUP.ITEM_GROUP_NAME   ");
            sb.Append(" , INV_ITEM_CLASS.ITEM_CLASS_CODE, INV_ITEM_CLASS.ITEM_CLASS_NAME   ");
            sb.Append(" ,GET_DEPARTMENT_CLOSING_QTY(inv.ITEM_ID," + DeptId + ") CLOSING_QTY ");
            sb.Append(" ,UOM_INFO.UOM_CODE_SHORT UOM_NAME ");
            sb.Append(" ,UOM_INFO.UOM_ID ");
            sb.Append(" ,bmst.BOM_ID ");
            sb.Append(" ,bmst.BOM_NO ");
            sb.Append(" ,bmst.BOM_ITEM_DESC ");
            sb.Append(" ,inv.IS_PRIME ");
            sb.Append(",NVL(  Puom.UOM_NAME , 'Panel-2') PANEL_UOM_NAME  ");
            sb.Append(",NVL( inv.PANEL_PC_UOM ,'39') PANEL_UOM_ID  ");
            sb.Append(",NVL( Puom.PCS_QTY,2)  PANEL_PC ");
            sb.Append(" ,inv.ITEM_ORDER ");
            sb.Append(" ,inv.ITEM_STANDARD_WEIGHT_KG ");
            sb.Append(" ,pwt.PASTE_PANEL_KG ");
            sb.Append(" ,gp.GRID_ITEM_ID USED_GRID_ID");
            sb.Append(" ,ginv.ITEM_NAME USED_GRID_NAME ");
            sb.Append(" ,pwt.PASTE_PC_KG ");
            sb.Append(" ,inv.WEIGHTED_AVERAGE_PRICE, inv.IS_OUTSALE_CASH,gt.FALSE_LUG_ITEM_ID,gt.FALSE_LUG_NAME ");
            sb.Append(" FROM INV_ITEM_MASTER inv ");
            sb.Append(" INNER JOIN INV_ITEM_GROUP ON inv.ITEM_GROUP_ID = INV_ITEM_GROUP.ITEM_GROUP_ID  ");
            sb.Append(" INNER JOIN INV_ITEM_CLASS ON inv.ITEM_CLASS_ID = INV_ITEM_CLASS.ITEM_CLASS_ID  ");
            sb.Append(" INNER JOIN UOM_INFO ON inv.UOM_ID = UOM_INFO.UOM_ID  ");
            sb.Append(" INNER JOIN  PRO_DEPARTMENT_ITEM dept ON dept.ITEM_ID=inv.ITEM_ID  ");
            sb.Append(" LEFT JOIN UOM_INFO Puom ON inv.PANEL_PC_UOM=Puom.UOM_ID ");
            sb.Append(" LEFT JOIN   BOM_MST_T bmst ON inv.ITEM_ID = bmst.BOM_ITEM_ID and bmst.ISACTIVE = 'Y' AND bmst.FROM_DEPARTMENT_ID=dept.DEPT_ID");
            sb.Append(" LEFT JOIN  PROD_ITEM_STANDARD_WEIGHT pwt ON pwt.ITEM_ID=inv.ITEM_ID AND pwt.DEPT_ID=dept.DEPT_ID ");
            sb.Append(" LEFT JOIN  PROD_PASTING_GRID_MAPPING gp ON gp.PASTE_ITEM_ID=inv.ITEM_ID ");
            sb.Append(" LEFT JOIN INV_ITEM_MASTER ginv ON gp.GRID_ITEM_ID=ginv.ITEM_ID  ");
            sb.Append(" LEFT JOIN GRID_CASTING_BOM_TEMP gt ON inv.ITEM_ID=gt.PARENT_ITEM_ID AND gt.FALSE_LUG_ITEM_ID IS NOT NULL  ");
            sb.Append(" Where 1=1  ");

            return sb.ToString();
        }

        public static string GetBOMItemList_For_Solar_Loading_SQLString(int specId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" Select  inv.ITEM_ID ");
            sb.Append("  ,inv.ITEM_NAME ");
            sb.Append(" ,inv.ITEM_CODE ");
            sb.Append(" , INV_ITEM_GROUP.ITEM_GROUP_CODE, INV_ITEM_GROUP.ITEM_GROUP_NAME   ");
            sb.Append(" , INV_ITEM_CLASS.ITEM_CLASS_CODE, INV_ITEM_CLASS.ITEM_CLASS_NAME   ");
            //sb.Append(" , INV_ITEM_TYPE.ITEM_TYPE_CODE, INV_ITEM_TYPE.ITEM_TYPE_NAME  ");
            sb.Append(" ,GET_SOLAR_LOAD_AVAILABLE_QTY(inv.ITEM_ID,54," + specId + ") CLOSING_QTY ");
            sb.Append(" ,UOM_INFO.UOM_CODE_SHORT UOM_NAME ");
            sb.Append(" ,UOM_INFO.UOM_ID ");
            sb.Append(" ,bmst.BOM_ID ");
            sb.Append(" ,bmst.BOM_NO ");
            sb.Append(" ,bmst.BOM_ITEM_DESC ");
            sb.Append(" ,inv.IS_PRIME ");
            sb.Append(",NVL(  Puom.UOM_NAME , 'Panel-2') PANEL_UOM_NAME  ");
            sb.Append(",NVL( inv.PANEL_PC_UOM ,'39') PANEL_UOM_ID  ");
            sb.Append(",NVL( Puom.PCS_QTY,2)  PANEL_PC ");
            //sb.Append(" ,'Panel-2' PANEL_UOM_NAME ");
            //sb.Append(" ,'39' PANEL_UOM_ID ");
            //sb.Append(" ,'2' PANEL_PC ");
            sb.Append(" ,inv.ITEM_ORDER ");
            sb.Append(" ,inv.ITEM_STANDARD_WEIGHT_KG ");
            sb.Append(" ,pwt.PASTE_PANEL_KG ");
            sb.Append(" ,gp.GRID_ITEM_ID USED_GRID_ID");
            sb.Append(" ,ginv.ITEM_NAME USED_GRID_NAME ");
            sb.Append(" ,pwt.PASTE_PC_KG ");
            //sb.Append("  ,(select ITEM_NAME  from INV_ITEM_MASTER WHERE item_id=gp.GRID_ITEM_ID ) USED_GRID_NAME ");
            sb.Append(" ,inv.WEIGHTED_AVERAGE_PRICE, inv.IS_OUTSALE_CASH ");
            sb.Append(" FROM INV_ITEM_MASTER inv ");
            sb.Append(" INNER JOIN INV_ITEM_GROUP ON inv.ITEM_GROUP_ID = INV_ITEM_GROUP.ITEM_GROUP_ID  ");
            sb.Append(" INNER JOIN INV_ITEM_CLASS ON inv.ITEM_CLASS_ID = INV_ITEM_CLASS.ITEM_CLASS_ID  ");
            //sb.Append(" INNER JOIN INV_ITEM_TYPE ON inv.ITEM_TYPE_ID = INV_ITEM_TYPE.ITEM_TYPE_ID  ");
            sb.Append(" INNER JOIN UOM_INFO ON inv.UOM_ID = UOM_INFO.UOM_ID  ");
            sb.Append(" INNER JOIN  PRO_DEPARTMENT_ITEM dept ON dept.ITEM_ID=inv.ITEM_ID  ");
            sb.Append(" LEFT JOIN UOM_INFO Puom ON inv.PANEL_PC_UOM=Puom.UOM_ID ");
            sb.Append(" LEFT JOIN   BOM_MST_T bmst ON inv.ITEM_ID = bmst.BOM_ITEM_ID and bmst.ISACTIVE = 'Y' AND bmst.FROM_DEPARTMENT_ID=dept.DEPT_ID");
            sb.Append(" LEFT JOIN  PROD_ITEM_STANDARD_WEIGHT pwt ON pwt.ITEM_ID=inv.ITEM_ID AND pwt.DEPT_ID=dept.DEPT_ID ");
            sb.Append(" LEFT JOIN  PROD_PASTING_GRID_MAPPING gp ON gp.PASTE_ITEM_ID=inv.ITEM_ID ");
            sb.Append(" LEFT JOIN INV_ITEM_MASTER ginv ON gp.GRID_ITEM_ID=ginv.ITEM_ID  ");
            sb.Append(" Where 1=1  ");

            return sb.ToString();
        }

        public static string GetBOMItemList_For_Solar_Unload_SQLString(int specId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" Select  inv.ITEM_ID ");
            sb.Append("  ,inv.ITEM_NAME ");
            sb.Append(" ,inv.ITEM_CODE ");
            sb.Append(" , INV_ITEM_GROUP.ITEM_GROUP_CODE, INV_ITEM_GROUP.ITEM_GROUP_NAME   ");
            sb.Append(" , INV_ITEM_CLASS.ITEM_CLASS_CODE, INV_ITEM_CLASS.ITEM_CLASS_NAME   ");
            sb.Append(" , INV_ITEM_TYPE.ITEM_TYPE_CODE, INV_ITEM_TYPE.ITEM_TYPE_NAME  ");
            sb.Append(" ,0 CLOSING_QTY ");
            sb.Append(" ,NVL(F_GET_AVAILABLE_CHARGING_QTY(inv.ITEM_ID," + specId + "),0) AVAILABLE_CHARGING_QUANTITY");
            sb.Append(" ,UOM_INFO.UOM_CODE_SHORT UOM_NAME ");
            sb.Append(" ,UOM_INFO.UOM_ID ");
            sb.Append(" ,bmst.BOM_ID ");
            sb.Append(" ,bmst.BOM_NO ");
            sb.Append(" ,bmst.BOM_ITEM_DESC ");
            sb.Append(" ,inv.IS_PRIME ");
            sb.Append(",NVL(  Puom.UOM_NAME , 'Panel-2') PANEL_UOM_NAME  ");
            sb.Append(",NVL( inv.PANEL_PC_UOM ,'39') PANEL_UOM_ID  ");
            sb.Append(",NVL( Puom.PCS_QTY,2)  PANEL_PC ");          
            sb.Append(" ,inv.ITEM_ORDER ");
            sb.Append(" ,inv.ITEM_STANDARD_WEIGHT_KG ");
            sb.Append(" ,pwt.PASTE_PANEL_KG ");
            sb.Append(" ,gp.GRID_ITEM_ID USED_GRID_ID");
            sb.Append(" ,ginv.ITEM_NAME USED_GRID_NAME ");
            sb.Append(" ,pwt.PASTE_PC_KG ");
            //sb.Append("  ,(select ITEM_NAME  from INV_ITEM_MASTER WHERE item_id=gp.GRID_ITEM_ID ) USED_GRID_NAME ");
            sb.Append(" FROM INV_ITEM_MASTER inv ");
            sb.Append(" INNER JOIN INV_ITEM_GROUP ON inv.ITEM_GROUP_ID = INV_ITEM_GROUP.ITEM_GROUP_ID  ");
            sb.Append(" INNER JOIN INV_ITEM_CLASS ON inv.ITEM_CLASS_ID = INV_ITEM_CLASS.ITEM_CLASS_ID  ");
            sb.Append(" INNER JOIN INV_ITEM_TYPE ON inv.ITEM_TYPE_ID = INV_ITEM_TYPE.ITEM_TYPE_ID  ");
            sb.Append(" INNER JOIN UOM_INFO ON inv.UOM_ID = UOM_INFO.UOM_ID  ");
            sb.Append(" INNER JOIN  PRO_DEPARTMENT_ITEM dept ON dept.ITEM_ID=inv.ITEM_ID  ");
            sb.Append(" LEFT JOIN UOM_INFO Puom ON inv.PANEL_PC_UOM=Puom.UOM_ID ");
            sb.Append(" LEFT JOIN   BOM_MST_T bmst ON inv.ITEM_ID = bmst.BOM_ITEM_ID and bmst.ISACTIVE = 'Y' AND bmst.FROM_DEPARTMENT_ID=dept.DEPT_ID");
            sb.Append(" LEFT JOIN  PROD_ITEM_STANDARD_WEIGHT pwt ON pwt.ITEM_ID=inv.ITEM_ID AND pwt.DEPT_ID=dept.DEPT_ID ");
            sb.Append(" LEFT JOIN  PROD_PASTING_GRID_MAPPING gp ON gp.PASTE_ITEM_ID=inv.ITEM_ID ");
            sb.Append(" LEFT JOIN INV_ITEM_MASTER ginv ON gp.GRID_ITEM_ID=ginv.ITEM_ID  ");
            sb.Append(" Where 1=1  ");

            return sb.ToString();
        }

        public static string GetBOMItemList_For_Solar_Packing_SQLString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" Select  inv.ITEM_ID ");
            sb.Append("  ,inv.ITEM_NAME ");
            sb.Append(" ,inv.ITEM_CODE ");
            sb.Append(" , INV_ITEM_GROUP.ITEM_GROUP_CODE, INV_ITEM_GROUP.ITEM_GROUP_NAME   ");
            sb.Append(" , INV_ITEM_CLASS.ITEM_CLASS_CODE, INV_ITEM_CLASS.ITEM_CLASS_NAME   ");
            sb.Append(" , INV_ITEM_TYPE.ITEM_TYPE_CODE, INV_ITEM_TYPE.ITEM_TYPE_NAME  ");
            sb.Append(" ,0 CLOSING_QTY ");
            sb.Append(" ,NVL(F_GET_AVAILABLE_PACKING_QTY(inv.ITEM_ID),0) AVAILABLE_PACKING_QUANTITY");
            sb.Append(" ,UOM_INFO.UOM_CODE_SHORT UOM_NAME ");
            sb.Append(" ,UOM_INFO.UOM_ID ");
            sb.Append(" ,bmst.BOM_ID ");
            sb.Append(" ,bmst.BOM_NO ");
            sb.Append(" ,bmst.BOM_ITEM_DESC ");
            sb.Append(" ,inv.IS_PRIME ");
            sb.Append(",NVL(  Puom.UOM_NAME , 'Panel-2') PANEL_UOM_NAME  ");
            sb.Append(",NVL( inv.PANEL_PC_UOM ,'39') PANEL_UOM_ID  ");
            sb.Append(",NVL( Puom.PCS_QTY,2)  PANEL_PC ");
            sb.Append(" ,inv.ITEM_ORDER ");
            sb.Append(" ,inv.ITEM_STANDARD_WEIGHT_KG ");
            sb.Append(" ,pwt.PASTE_PANEL_KG ");
            sb.Append(" ,gp.GRID_ITEM_ID USED_GRID_ID");
            sb.Append(" ,ginv.ITEM_NAME USED_GRID_NAME ");
            sb.Append(" ,pwt.PASTE_PC_KG ");
            //sb.Append("  ,(select ITEM_NAME  from INV_ITEM_MASTER WHERE item_id=gp.GRID_ITEM_ID ) USED_GRID_NAME ");
            sb.Append(" FROM INV_ITEM_MASTER inv ");
            sb.Append(" INNER JOIN INV_ITEM_GROUP ON inv.ITEM_GROUP_ID = INV_ITEM_GROUP.ITEM_GROUP_ID  ");
            sb.Append(" INNER JOIN INV_ITEM_CLASS ON inv.ITEM_CLASS_ID = INV_ITEM_CLASS.ITEM_CLASS_ID  ");
            sb.Append(" INNER JOIN INV_ITEM_TYPE ON inv.ITEM_TYPE_ID = INV_ITEM_TYPE.ITEM_TYPE_ID  ");
            sb.Append(" INNER JOIN UOM_INFO ON inv.UOM_ID = UOM_INFO.UOM_ID  ");
            sb.Append(" INNER JOIN  PRO_DEPARTMENT_ITEM dept ON dept.ITEM_ID=inv.ITEM_ID  ");
            sb.Append(" LEFT JOIN UOM_INFO Puom ON inv.PANEL_PC_UOM=Puom.UOM_ID ");
            sb.Append(" LEFT JOIN   BOM_MST_T bmst ON inv.ITEM_ID = bmst.BOM_ITEM_ID and bmst.ISACTIVE = 'Y' AND bmst.FROM_DEPARTMENT_ID=dept.DEPT_ID");
            sb.Append(" LEFT JOIN  PROD_ITEM_STANDARD_WEIGHT pwt ON pwt.ITEM_ID=inv.ITEM_ID AND pwt.DEPT_ID=dept.DEPT_ID ");
            sb.Append(" LEFT JOIN  PROD_PASTING_GRID_MAPPING gp ON gp.PASTE_ITEM_ID=inv.ITEM_ID ");
            sb.Append(" LEFT JOIN INV_ITEM_MASTER ginv ON gp.GRID_ITEM_ID=ginv.ITEM_ID  ");
            sb.Append(" Where 1=1  ");

            return sb.ToString();
        }

        public static string GetBOMPackingItemList_SQLString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT   inv.ITEM_ID ");
            sb.Append(" ,inv.ITEM_NAME ");
            sb.Append(" ,UOM_INFO.UOM_CODE_SHORT UOM_NAME ");
            sb.Append(" ,UOM_INFO.UOM_ID ");
            sb.Append(" ,bmst.BOM_ID ");
            sb.Append(" ,bmst.BOM_NO ");
            sb.Append(" ,bmst.BOM_ITEM_DESC ");
            sb.Append(" ,inv.IS_PRIME ");
            sb.Append(" , FN_ASSEMBLY_PACKING_BAL_QTY(proMst.PROD_BATCH_NO ,proDtl.ITEM_ID,proMst.DEPT_ID) BALPACKINGQTY ");  // Remain Assemble qty
            sb.Append(" FROM PRODUCTION_MST proMst  ");
            sb.Append(" INNER JOIN PRODUCTION_DTL proDtl ON  proDtl.PROD_MST_ID=proMst.PROD_ID AND proDtl.IS_PACKING IS NULL");
            sb.Append(" INNER JOIN INV_ITEM_MASTER inv ON inv.ITEM_ID=proDtl.ITEM_ID ");
            sb.Append(" INNER JOIN INV_ITEM_GROUP ON inv.ITEM_GROUP_ID = INV_ITEM_GROUP.ITEM_GROUP_ID  ");
            sb.Append(" INNER JOIN INV_ITEM_CLASS ON inv.ITEM_CLASS_ID = INV_ITEM_CLASS.ITEM_CLASS_ID  ");
            sb.Append(" INNER JOIN INV_ITEM_TYPE ON inv.ITEM_TYPE_ID = INV_ITEM_TYPE.ITEM_TYPE_ID  ");
            sb.Append(" INNER JOIN UOM_INFO ON inv.UOM_ID = UOM_INFO.UOM_ID  ");
            sb.Append(" LEFT JOIN   BOM_MST_T bmst ON inv.ITEM_ID = bmst.BOM_ITEM_ID ");
            sb.Append(" LEFT JOIN  PRO_DEPARTMENT_ITEM dept ON dept.ITEM_ID=inv.ITEM_ID and proMst.DEPT_ID=dept.DEPT_ID ");
            sb.Append(" Where 1=1  ");
           sb.Append(" AND proMst.DEPT_ID=136 ");

            return sb.ToString();
        }

        public static string GetVRLA_BOMPackingItemList_SQLString(int pDept)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT   inv.ITEM_ID ");
            sb.Append(" ,inv.ITEM_NAME ");
            sb.Append(" ,UOM_INFO.UOM_CODE_SHORT UOM_NAME ");
            sb.Append(" ,UOM_INFO.UOM_ID ");
            sb.Append(" ,bmst.BOM_ID ");
            sb.Append(" ,bmst.BOM_NO ");
            sb.Append(" ,bmst.BOM_ITEM_DESC ");
            sb.Append(" ,inv.IS_PRIME ");
            sb.Append(" , FN_ASSEMBLY_PACKING_BAL_QTY(proMst.PROD_BATCH_NO ,proDtl.ITEM_ID,proMst.DEPT_ID) BALPACKINGQTY ");  // Remain Assemble qty
            sb.Append(" FROM PRODUCTION_MST proMst  ");
            sb.Append(" INNER JOIN PRODUCTION_DTL proDtl ON  proDtl.PROD_MST_ID=proMst.PROD_ID AND proDtl.IS_PACKING IS NULL");
            sb.Append(" INNER JOIN INV_ITEM_MASTER inv ON inv.ITEM_ID=proDtl.ITEM_ID ");
            sb.Append(" INNER JOIN INV_ITEM_GROUP ON inv.ITEM_GROUP_ID = INV_ITEM_GROUP.ITEM_GROUP_ID  ");
            sb.Append(" INNER JOIN INV_ITEM_CLASS ON inv.ITEM_CLASS_ID = INV_ITEM_CLASS.ITEM_CLASS_ID  ");
            sb.Append(" INNER JOIN INV_ITEM_TYPE ON inv.ITEM_TYPE_ID = INV_ITEM_TYPE.ITEM_TYPE_ID  ");
            sb.Append(" INNER JOIN UOM_INFO ON inv.UOM_ID = UOM_INFO.UOM_ID  ");
            sb.Append(" LEFT JOIN   BOM_MST_T bmst ON inv.ITEM_ID = bmst.BOM_ITEM_ID ");
            sb.Append(" LEFT JOIN  PRO_DEPARTMENT_ITEM dept ON dept.ITEM_ID=inv.ITEM_ID and proMst.DEPT_ID=dept.DEPT_ID ");
            sb.Append(" Where 1=1  ");
            sb.Append(" AND proMst.DEPT_ID= " + pDept.ToString());

            return sb.ToString();
        }

       


        //Batch wise item List
        public static string GetBatchWiseItemList_SQLString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT   INV_ITEM_MASTER.ITEM_ID ");
            sb.Append(" ,INV_ITEM_MASTER.ITEM_NAME ");
            sb.Append(" ,UOM_INFO.UOM_CODE_SHORT UOM_NAME ");
            sb.Append(" ,UOM_INFO.UOM_ID ");
            sb.Append(" ,INV_ITEM_MASTER.IS_PRIME ");
            sb.Append(" ,pUOM_INFO.PCS_QTY  PANEL_PC  ");
            sb.Append(" ,INV_ITEM_MASTER.ITEM_STANDARD_WEIGHT_KG ");
            //sb.Append(" , SUM(proDtl.ITEM_QTY) PRODUCTION_QTY " );
            sb.Append(" ,  proDtl.ITEM_QTY  PRODUCTION_QTY ");
            sb.Append(" FROM PRODUCTION_MST proMst  ");
            sb.Append(" INNER JOIN PRODUCTION_DTL proDtl ON  proDtl.PROD_MST_ID=proMst.PROD_ID ");
            sb.Append(" INNER JOIN INV_ITEM_MASTER  ON INV_ITEM_MASTER.ITEM_ID=proDtl.ITEM_ID ");
            sb.Append(" INNER JOIN INV_ITEM_GROUP ON INV_ITEM_MASTER.ITEM_GROUP_ID = INV_ITEM_GROUP.ITEM_GROUP_ID  ");
            sb.Append(" INNER JOIN INV_ITEM_CLASS ON INV_ITEM_MASTER.ITEM_CLASS_ID = INV_ITEM_CLASS.ITEM_CLASS_ID  ");
            sb.Append(" INNER JOIN INV_ITEM_TYPE ON INV_ITEM_MASTER.ITEM_TYPE_ID = INV_ITEM_TYPE.ITEM_TYPE_ID  ");
            sb.Append(" INNER JOIN UOM_INFO ON INV_ITEM_MASTER.UOM_ID = UOM_INFO.UOM_ID  ");
            sb.Append(" LEFT JOIN  PRO_DEPARTMENT_ITEM dept ON dept.ITEM_ID=INV_ITEM_MASTER.ITEM_ID and proMst.DEPT_ID=dept.DEPT_ID ");
            sb.Append("  LEFT JOIN  UOM_INFO pUOM_INFO ON INV_ITEM_MASTER.PANEL_PC_UOM = pUOM_INFO.UOM_ID ");
            sb.Append(" Where 1=1   ");
            //sb.Append(" GROUP BY INV_ITEM_MASTER.ITEM_ID  ,INV_ITEM_MASTER.ITEM_NAME  ,UOM_INFO.UOM_CODE_SHORT   ,UOM_INFO.UOM_ID  ,INV_ITEM_MASTER.IS_PRIME  ");
            //sb.Append(" ,INV_ITEM_MASTER.PANEL_PC  ,INV_ITEM_MASTER.ITEM_STANDARD_WEIGHT_KG  ");
            //sb.Append(" AND proMst.DEPT_ID=136 ");

            return sb.ToString();
        }
        //End

        //Batch wise item List FOR Assembly
        public static string GetBatchWiseItemListASM_SQLString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT   INV_ITEM_MASTER.ITEM_ID ");
            sb.Append(" ,INV_ITEM_MASTER.ITEM_NAME ");
            sb.Append(" ,UOM_INFO.UOM_CODE_SHORT UOM_NAME ");
            sb.Append(" ,UOM_INFO.UOM_ID ");
            sb.Append(" ,INV_ITEM_MASTER.IS_PRIME ");
            sb.Append(" ,pUOM_INFO.PCS_QTY  PANEL_PC  ");
          //  sb.Append(" ,NVL(PISW.GRID_PC_KG,0)+NVL(PISW.PASTE_PC_KG,0) ITEM_STANDARD_WEIGHT_KG ");
            sb.Append(" ,FN_GET_PASTED_PLATE_WT(proMst.DEPT_ID,INV_ITEM_MASTER.ITEM_ID) ITEM_STANDARD_WEIGHT_KG");
            //sb.Append(" , SUM(proDtl.ITEM_QTY) PRODUCTION_QTY " );
            sb.Append(" ,  proDtl.ITEM_QTY  PRODUCTION_QTY ");
            sb.Append(" FROM PRODUCTION_MST proMst  ");
            sb.Append(" INNER JOIN PRODUCTION_DTL proDtl ON  proDtl.PROD_MST_ID=proMst.PROD_ID ");
            sb.Append(" INNER JOIN INV_ITEM_MASTER  ON INV_ITEM_MASTER.ITEM_ID=proDtl.ITEM_ID ");
            sb.Append(" INNER JOIN INV_ITEM_GROUP ON INV_ITEM_MASTER.ITEM_GROUP_ID = INV_ITEM_GROUP.ITEM_GROUP_ID  ");
            sb.Append(" INNER JOIN INV_ITEM_CLASS ON INV_ITEM_MASTER.ITEM_CLASS_ID = INV_ITEM_CLASS.ITEM_CLASS_ID  ");
            sb.Append(" INNER JOIN INV_ITEM_TYPE ON INV_ITEM_MASTER.ITEM_TYPE_ID = INV_ITEM_TYPE.ITEM_TYPE_ID  ");
            sb.Append(" INNER JOIN UOM_INFO ON INV_ITEM_MASTER.UOM_ID = UOM_INFO.UOM_ID  ");
            sb.Append(" LEFT JOIN  PRO_DEPARTMENT_ITEM dept ON dept.ITEM_ID=INV_ITEM_MASTER.ITEM_ID and proMst.DEPT_ID=dept.DEPT_ID ");
            sb.Append(" LEFT JOIN  UOM_INFO pUOM_INFO ON INV_ITEM_MASTER.PANEL_PC_UOM = pUOM_INFO.UOM_ID ");
            sb.Append(" LEFT JOIN PROD_ITEM_STANDARD_WEIGHT PISW ON INV_ITEM_MASTER.ITEM_ID=PISW.ITEM_ID and proMst.DEPT_ID=PISW.DEPT_ID ");
            sb.Append(" Where 1=1   ");
            //sb.Append(" GROUP BY INV_ITEM_MASTER.ITEM_ID  ,INV_ITEM_MASTER.ITEM_NAME  ,UOM_INFO.UOM_CODE_SHORT   ,UOM_INFO.UOM_ID  ,INV_ITEM_MASTER.IS_PRIME  ");
            //sb.Append(" ,INV_ITEM_MASTER.PANEL_PC  ,INV_ITEM_MASTER.ITEM_STANDARD_WEIGHT_KG  ");
            //sb.Append(" AND proMst.DEPT_ID=136 ");

            return sb.ToString();
        }


        public static string GetBatchWiseItemListPasting_SQLString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT   INV_ITEM_MASTER.ITEM_ID ");
            sb.Append(" ,INV_ITEM_MASTER.ITEM_NAME ");
            sb.Append(" ,UOM_INFO.UOM_CODE_SHORT UOM_NAME ");
            sb.Append(" ,UOM_INFO.UOM_ID ");
            sb.Append(" ,INV_ITEM_MASTER.IS_PRIME ");
            sb.Append(" ,pUOM_INFO.PCS_QTY  PANEL_PC  ");
            //  sb.Append(" ,NVL(PISW.GRID_PC_KG,0)+NVL(PISW.PASTE_PC_KG,0) ITEM_STANDARD_WEIGHT_KG ");
            sb.Append(" ,FN_GET_PASTED_PLATE_WT(proMst.DEPT_ID,INV_ITEM_MASTER.ITEM_ID) ITEM_STANDARD_WEIGHT_KG");
            //sb.Append(" , SUM(proDtl.ITEM_QTY) PRODUCTION_QTY " );
            sb.Append(" ,  proDtl.ITEM_QTY  PRODUCTION_QTY ");
            sb.Append(" FROM PRODUCTION_MST proMst  ");
            sb.Append(" INNER JOIN PRODUCTION_DTL proDtl ON  proDtl.PROD_MST_ID=proMst.PROD_ID ");
            sb.Append(" INNER JOIN INV_ITEM_MASTER  ON INV_ITEM_MASTER.ITEM_ID=proDtl.ITEM_ID ");
            sb.Append(" INNER JOIN INV_ITEM_GROUP ON INV_ITEM_MASTER.ITEM_GROUP_ID = INV_ITEM_GROUP.ITEM_GROUP_ID  ");
            sb.Append(" INNER JOIN INV_ITEM_CLASS ON INV_ITEM_MASTER.ITEM_CLASS_ID = INV_ITEM_CLASS.ITEM_CLASS_ID  ");
            sb.Append(" INNER JOIN INV_ITEM_TYPE ON INV_ITEM_MASTER.ITEM_TYPE_ID = INV_ITEM_TYPE.ITEM_TYPE_ID  ");
            sb.Append(" INNER JOIN UOM_INFO ON INV_ITEM_MASTER.UOM_ID = UOM_INFO.UOM_ID  ");
            sb.Append(" LEFT JOIN  PRO_DEPARTMENT_ITEM dept ON dept.ITEM_ID=INV_ITEM_MASTER.ITEM_ID and proMst.DEPT_ID=dept.DEPT_ID ");
            sb.Append(" LEFT JOIN  UOM_INFO pUOM_INFO ON INV_ITEM_MASTER.PANEL_PC_UOM = pUOM_INFO.UOM_ID ");
            sb.Append(" LEFT JOIN PROD_ITEM_STANDARD_WEIGHT PISW ON INV_ITEM_MASTER.ITEM_ID=PISW.ITEM_ID and proMst.DEPT_ID=PISW.DEPT_ID ");
            sb.Append(" Where 1=1   ");
            //sb.Append(" GROUP BY INV_ITEM_MASTER.ITEM_ID  ,INV_ITEM_MASTER.ITEM_NAME  ,UOM_INFO.UOM_CODE_SHORT   ,UOM_INFO.UOM_ID  ,INV_ITEM_MASTER.IS_PRIME  ");
            //sb.Append(" ,INV_ITEM_MASTER.PANEL_PC  ,INV_ITEM_MASTER.ITEM_STANDARD_WEIGHT_KG  ");
            //sb.Append(" AND proMst.DEPT_ID=136 ");

            return sb.ToString();
        }
        //End

        // Item & dept wise transferable item 
        public static string GetdeptWiseTransferableItemList_SQLString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT   INV.ITEM_ID   ");
            sb.Append(" ,INV.ITEM_NAME  ,UOM_INFO.UOM_CODE_SHORT UOM_NAME  ,UOM_INFO.UOM_ID  ,INV.IS_PRIME  ");
            sb.Append(" ,GET_DEPT_WISE_CLOSING_QTY(INV.ITEM_ID,dept.DEPT_ID) current_stock ");
            sb.Append(" FROM  INV_ITEM_MASTER  INV   ");
            sb.Append(" INNER JOIN INV_ITEM_GROUP ON INV.ITEM_GROUP_ID = INV_ITEM_GROUP.ITEM_GROUP_ID    ");
            sb.Append(" INNER JOIN INV_ITEM_CLASS ON INV.ITEM_CLASS_ID = INV_ITEM_CLASS.ITEM_CLASS_ID   "); 
            sb.Append(" INNER JOIN INV_ITEM_TYPE ON INV.ITEM_TYPE_ID = INV_ITEM_TYPE.ITEM_TYPE_ID  ");  
            sb.Append(" INNER JOIN UOM_INFO ON INV.UOM_ID = UOM_INFO.UOM_ID    ");
            sb.Append(" INNER JOIN INV_ITEM_TRANSFERABLE_MAPPING mapp ON mapp.TRANSFERABLE_ITEM_ID=INV.ITEM_ID    ");
            sb.Append(" INNER JOIN  PRO_DEPARTMENT_ITEM dept ON dept.ITEM_ID=mapp.TRANSFERABLE_ITEM_ID  ");
            sb.Append(" Where 1=1 ");
            
            //sb.Append(" AND proMst.DEPT_ID=136 ");

            return sb.ToString();
        }


        public static List<dcBOM_MST_T> GetDepartmentItemList(int dept_id, string is_finished, int pitemid, DBContext dc)
        {
            List<dcBOM_MST_T> cObjList = new List<dcBOM_MST_T>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                DBCommandInfo cmdInfo = new DBCommandInfo();

                StringBuilder sb = new StringBuilder();
                sb.Append(GetdeptWiseTransferableItemList_SQLString());

                if(pitemid>0)
                {
                    sb.Append(" AND INV.ITEM_ID =@pitemid ");
                    cmdInfo.DBParametersInfo.Add("@pitemid", pitemid);
                }
                if (dept_id > 0)
                {
                    sb.Append(" AND dept.DEPT_ID=@dept_id ");
                    cmdInfo.DBParametersInfo.Add("@dept_id", dept_id);
                }
                if (is_finished != "")
                {
                    sb.Append(" AND dept.IS_FINISHED=@is_finished ");
                    cmdInfo.DBParametersInfo.Add("@is_finished", is_finished);
                }




                //sb.Append(" ORDER BY a.ISSUE_RECEIVE_DATE DESC ");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                //cmd.CommandType = CommandType.Text;
                //cmd.CommandText = sb.ToString();

                //DBQuery dbq = new DBQuery();
                //dbq.DBQueryMode = PG.Core.DBBase.DBQueryModeEnum.DBCommand;
                //dbq.DBCommand = cmd;

                cObjList = GetBOM_MST_TList(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }







        public static List<dcBOM_MST_T> GetRejectItemList(int dept_id, string is_finished, DBContext dc)
        {
            List<dcBOM_MST_T> cObjList = new List<dcBOM_MST_T>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                DBCommandInfo cmdInfo = new DBCommandInfo();



                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT   INV_ITEM_MASTER.ITEM_ID  ,INV_ITEM_MASTER.ITEM_NAME  ");
                 sb.Append(" ,UOM_INFO.UOM_CODE_SHORT UOM_NAME  ,UOM_INFO.UOM_ID  ,INV_ITEM_MASTER.IS_PRIME ");
                 sb.Append(" FROM INV_ITEM_MASTER ");
                 sb.Append(" LEFT JOIN INV_ITEM_GROUP ON INV_ITEM_MASTER.ITEM_GROUP_ID = INV_ITEM_GROUP.ITEM_GROUP_ID  "); 
                 sb.Append(" LEFT JOIN INV_ITEM_CLASS ON INV_ITEM_MASTER.ITEM_CLASS_ID = INV_ITEM_CLASS.ITEM_CLASS_ID   ");
                 sb.Append(" INNER JOIN INV_ITEM_TYPE ON INV_ITEM_MASTER.ITEM_TYPE_ID = INV_ITEM_TYPE.ITEM_TYPE_ID   ");
                 sb.Append(" INNER JOIN UOM_INFO ON INV_ITEM_MASTER.UOM_ID = UOM_INFO.UOM_ID   ");
                 sb.Append(" LEFT JOIN  PRO_DEPARTMENT_ITEM dept ON dept.ITEM_ID=INV_ITEM_MASTER.ITEM_ID ");
                 sb.Append(" Where 1=1 ");
                //cmdInfo.DBParametersInfo.Add("@Fdate", Fdate);
                //cmdInfo.DBParametersInfo.Add("@TDate", TDate);



               
                
                if (dept_id > 0)
                {
                    sb.Append(" AND dept.DEPT_ID=@dept_id ");
                    cmdInfo.DBParametersInfo.Add("@dept_id", dept_id);
                }
                if (is_finished != "")
                {
                    sb.Append(" AND dept.IS_FINISHED=@is_finished ");
                    cmdInfo.DBParametersInfo.Add("@is_finished", is_finished);
                }




                //sb.Append(" ORDER BY a.ISSUE_RECEIVE_DATE DESC ");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                //cmd.CommandType = CommandType.Text;
                //cmd.CommandText = sb.ToString();

                //DBQuery dbq = new DBQuery();
                //dbq.DBQueryMode = PG.Core.DBBase.DBQueryModeEnum.DBCommand;
                //dbq.DBCommand = cmd;

                cObjList = GetBOM_MST_TList(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }


        public static string UpdateBOMAuthorized(int pBOM_ID, int pAUTH_BY_ID, DBContext dc)
        {
            bool isDCInit = false;
            string _BOM_NO = string.Empty;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                string abbr = " UPDATE BOM_MST_T SET AUTH_STATUS='Y',AUTH_BY=@AUTH_BY_ID,AUTH_DATE= SYSDATE WHERE BOM_ID=@BOM_ID ";
                cmdInfo.DBParametersInfo.Add("@AUTH_BY_ID", pAUTH_BY_ID);
                cmdInfo.DBParametersInfo.Add("@BOM_ID", pBOM_ID);
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBNonQuery(dbq, dc);
                _BOM_NO = pBOM_ID.ToString();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return _BOM_NO;
        }
      

        public static string NEW_BOM_NO(DateTime pdate, int pdeptid, DBContext dc)
        {
            bool isDCInit = false;
            string _FC_NO = string.Empty;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                string abbr = " SELECT FN_NEW_BOM_NO(@pdate,@pdeptid) A from Dual ";
                cmdInfo.DBParametersInfo.Add("@pdate", pdate);
                cmdInfo.DBParametersInfo.Add("@pdeptid", pdeptid);
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                _FC_NO = Convert.ToString(DBQuery.ExecuteDBScalar(dbq, dc));
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return _FC_NO;
        }

        public static DataLoadOptions BOM_MST_TLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcBOM_MST_T>(obj => obj.relatedclassname);
            return dlo;
        }
        public static List<dcBOM_MST_T> GetBOM_MST_TList()
        {
            return GetBOM_MST_TList(null, null);
        }
        public static List<dcBOM_MST_T> GetBOM_MST_TList(DBContext dc)
        {
            return GetBOM_MST_TList(null, dc);
        }
        public static List<dcBOM_MST_T> GetBOM_MST_TList(DBQuery dbq, DBContext dc)
        {
            List<dcBOM_MST_T> cObjList = new List<dcBOM_MST_T>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcBOM_MST_T>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcBOM_MST_T GetBOM_MST_TByID(int pBOM_MST_TID)
        {
            return GetBOM_MST_TByID(pBOM_MST_TID, null);
        }
        public static dcBOM_MST_T GetBOM_MST_TByID(int pBOM_MST_TID, DBContext dc)
        {
            dcBOM_MST_T cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetBOMinfobyBOMIDString());
                sb.Append(" AND mst.BOM_ID=@BOM_ID ");
                cmdInfo.DBParametersInfo.Add("@BOM_ID", pBOM_MST_TID);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObj = DBQuery.ExecuteDBQuery<dcBOM_MST_T>(dbq, dc).ToList().FirstOrDefault();

                //isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    var result = (from c in dataContext.GetTable<dcBOM_MST_T>()
                //                  where c.BOM_ID == pBOM_MST_TID
                //                  select c).ToList();
                //    if (result.Count() > 0)
                //    {
                //        cObj = result.First();
                //    }
                //}



            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObj;
        }

        public static int Insert(dcBOM_MST_T cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcBOM_MST_T cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcBOM_MST_T>(cObj, true);
                if (id > 0) { cObj.BOM_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcBOM_MST_T cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcBOM_MST_T cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcBOM_MST_T>(cObj);
            }

            if (cObj.BOMDetList != null)
            {
                foreach (dcBOM_DTL_T oDet in cObj.BOMDetList)
                {
                    oDet.BOM_MST_ID = cObj.BOM_ID;
                    if (oDet.BOM_DTL_ID > 0)
                    { 
                        if(oDet._RecordState != RecordStateEnum.Deleted)
                           oDet._RecordState = RecordStateEnum.Edited;
                    }
                }
                BOM_DTL_TBL.SaveList(cObj.BOMDetList, dc);

            }

            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pBOM_MST_TID)
        {
            return Delete(pBOM_MST_TID, null);
        }
        public static bool Delete(int pBOM_MST_TID, DBContext dc)
        {
            dcBOM_MST_T cObj = new dcBOM_MST_T();
            cObj.BOM_ID = pBOM_MST_TID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcBOM_MST_T>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcBOM_MST_T cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcBOM_MST_T cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcBOM_MST_T cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcBOM_MST_T cObj, DBContext dc)
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
                                newID = cObj.BOM_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.BOM_ID, dc))
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
                        if (cObj._RecordState == RecordStateEnum.Added)
                        {
                            if (cObj.BOMDetList != null)
                            {
                                foreach (dcBOM_DTL_T det in cObj.BOMDetList)
                                {
                                    det.BOM_MST_ID = newID;
                                }
                                bStatus = BOM_DTL_TBL.SaveList(cObj.BOMDetList, dc);

                            }
                        }
                        

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

        public static bool SaveList(List<dcBOM_MST_T> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcBOM_MST_T> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcBOM_MST_T oDet in detList)
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
                        bool d = Delete(oDet.BOM_ID, dc);
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


        public static List<dcBOM_MST_T> GetBomList(DateTime Fdate, DateTime TDate, int P_BOM_ID,string isactive,int dept_id,int item_id,int stmlid)
        {
            return GetBomList(Fdate, TDate, P_BOM_ID,isactive,dept_id,item_id,stmlid, null);
        }

        public static List<dcBOM_MST_T> GetBomList(DateTime Fdate, DateTime TDate, int P_BOM_ID,string isactive,int dept_id,int item_id,int stmlid,DBContext dc)
        {
            List<dcBOM_MST_T> cObjList = new List<dcBOM_MST_T>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                DBCommandInfo cmdInfo = new DBCommandInfo();
             
                StringBuilder sb = new StringBuilder();

                sb.Append(" Select a.BOM_ID,a.BOM_NO,a.BOM_ITEM_ID,a.BOM_ITEM_DESC,bim.ITEM_NAME,a.UNIT_ID,bum.UOM_CODE,us.FULLNAME FULL_NAME,CREATE_DATE,dept.DEPARTMENT_NAME FROM BOM_MST_T a ");
                sb.Append(" INNER JOIN INV_ITEM_MASTER bim ON a.BOM_ITEM_ID=bim.ITEM_ID ");
                sb.Append(" LEFT JOIN UOM_INFO bum ON a.UNIT_ID=bum.UOM_ID ");
                sb.Append(" INNER JOIN TBLUSER us ON a.CREATE_BY=us.USERID ");
                sb.Append(" INNER JOIN DEPARTMENT_INFO dept On a.FROM_DEPARTMENT_ID=dept.DEPARTMENT_ID ");
                sb.Append(" Where 1=1 ");
                //sb.Append(" AND TO_DATE(a.CREATE_DATE,'dd-mm-rrrr') Between @Fdate  and  @TDate ");
                //cmdInfo.DBParametersInfo.Add("@Fdate", Fdate);
                //cmdInfo.DBParametersInfo.Add("@TDate", TDate);
               
               

                if (P_BOM_ID > 0)
                {
                    sb.Append(" AND a.BOM_ID=@BOM_ID ");
                    cmdInfo.DBParametersInfo.Add("@BOM_ID", P_BOM_ID);
                }
                if (isactive!= "0")
                {
                    sb.Append(" AND a.ISACTIVE=@isactive ");
                    cmdInfo.DBParametersInfo.Add("@isactive", isactive);
                }
                if (dept_id > 0)
                {
                    sb.Append(" AND a.FROM_DEPARTMENT_ID=@dept_id ");
                    cmdInfo.DBParametersInfo.Add("@dept_id", dept_id);
                }
                if (item_id > 0)
                {
                    sb.Append(" AND a.BOM_ITEM_ID=@item_id ");
                    cmdInfo.DBParametersInfo.Add("@item_id", item_id);
                }
                if(stmlid>0)
                {
                    sb.Append(" AND a.STLM_ID=@stmlid ");
                    cmdInfo.DBParametersInfo.Add("@stmlid", stmlid);
                }



                //sb.Append(" ORDER BY a.ISSUE_RECEIVE_DATE DESC ");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                //cmd.CommandType = CommandType.Text;
                //cmd.CommandText = sb.ToString();

                //DBQuery dbq = new DBQuery();
                //dbq.DBQueryMode = PG.Core.DBBase.DBQueryModeEnum.DBCommand;
                //dbq.DBCommand = cmd;

                cObjList = GetBOM_MST_TList(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }


        public static List<dcBOM_MST_T> GetTransferableItemList(int dept_id, string is_transfarable, DBContext dc)
        {
            List<dcBOM_MST_T> cObjList = new List<dcBOM_MST_T>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT   INV_ITEM_MASTER.ITEM_ID  ,INV_ITEM_MASTER.ITEM_NAME  ");
                sb.Append(" ,UOM_INFO.UOM_CODE_SHORT UOM_NAME  ,UOM_INFO.UOM_ID  ,INV_ITEM_MASTER.IS_PRIME ");
                sb.Append(" FROM INV_ITEM_MASTER ");
                sb.Append(" LEFT JOIN INV_ITEM_GROUP ON INV_ITEM_MASTER.ITEM_GROUP_ID = INV_ITEM_GROUP.ITEM_GROUP_ID  ");
                sb.Append(" LEFT JOIN INV_ITEM_CLASS ON INV_ITEM_MASTER.ITEM_CLASS_ID = INV_ITEM_CLASS.ITEM_CLASS_ID   ");
                sb.Append(" INNER JOIN INV_ITEM_TYPE ON INV_ITEM_MASTER.ITEM_TYPE_ID = INV_ITEM_TYPE.ITEM_TYPE_ID   ");
                sb.Append(" INNER JOIN UOM_INFO ON INV_ITEM_MASTER.UOM_ID = UOM_INFO.UOM_ID   ");
                sb.Append(" INNER JOIN  PRO_DEPARTMENT_ITEM dept ON dept.ITEM_ID=INV_ITEM_MASTER.ITEM_ID ");
                sb.Append(" Where 1=1 ");

                if (dept_id > 0)
                {
                    sb.Append(" AND dept.DEPT_ID=@dept_id ");
                    cmdInfo.DBParametersInfo.Add("@dept_id", dept_id);
                }
                if (is_transfarable != "")
                {
                    sb.Append(" AND INV_ITEM_MASTER.STOCK_TRANSFARABLE=@is_transfarable ");
                    cmdInfo.DBParametersInfo.Add("@is_transfarable", is_transfarable);
                }

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                cObjList = GetBOM_MST_TList(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static string GetItem_fdateStockList_SQLString(string pfDate)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" Select  inv.ITEM_ID ");
            sb.Append("  ,inv.ITEM_NAME ");
            sb.Append(" ,inv.ITEM_CODE ");
            sb.Append(" , INV_ITEM_GROUP.ITEM_GROUP_CODE, INV_ITEM_GROUP.ITEM_GROUP_NAME   ");
            sb.Append(" , INV_ITEM_CLASS.ITEM_CLASS_CODE, INV_ITEM_CLASS.ITEM_CLASS_NAME   ");
            sb.Append(" , INV_ITEM_TYPE.ITEM_TYPE_CODE, INV_ITEM_TYPE.ITEM_TYPE_NAME  ");
            sb.Append(" ,ITEM_TARGET_DATE_CLOSING_QTY(inv.ITEM_ID,dept.DEPT_ID, '" + pfDate + "',601) CLOSING_QTY ");
            sb.Append(" ,UOM_INFO.UOM_CODE_SHORT UOM_NAME ");
            sb.Append(" ,UOM_INFO.UOM_ID ");
            sb.Append(" ,bmst.BOM_ID ");
            sb.Append(" ,bmst.BOM_NO ");
            sb.Append(" ,bmst.BOM_ITEM_DESC ");
            sb.Append(" ,inv.IS_PRIME ");
            sb.Append(",NVL(  Puom.UOM_NAME , 'Panel-2') PANEL_UOM_NAME  ");
            sb.Append(",NVL( inv.PANEL_PC_UOM ,'39') PANEL_UOM_ID  ");
            sb.Append(",NVL( Puom.PCS_QTY,2)  PANEL_PC ");
            sb.Append(" ,inv.ITEM_ORDER ");
            sb.Append(" ,inv.ITEM_STANDARD_WEIGHT_KG ");
            sb.Append(" ,pwt.PASTE_PANEL_KG ");
            sb.Append(" ,gp.GRID_ITEM_ID USED_GRID_ID");
            sb.Append(" ,ginv.ITEM_NAME USED_GRID_NAME ");
            sb.Append(" ,pwt.PASTE_PC_KG ");
            sb.Append(" FROM INV_ITEM_MASTER inv ");
            sb.Append(" INNER JOIN INV_ITEM_GROUP ON inv.ITEM_GROUP_ID = INV_ITEM_GROUP.ITEM_GROUP_ID  ");
            sb.Append(" INNER JOIN INV_ITEM_CLASS ON inv.ITEM_CLASS_ID = INV_ITEM_CLASS.ITEM_CLASS_ID  ");
            sb.Append(" INNER JOIN INV_ITEM_TYPE ON inv.ITEM_TYPE_ID = INV_ITEM_TYPE.ITEM_TYPE_ID  ");
            sb.Append(" INNER JOIN UOM_INFO ON inv.UOM_ID = UOM_INFO.UOM_ID  ");
            sb.Append(" INNER JOIN  PRO_DEPARTMENT_ITEM dept ON dept.ITEM_ID=inv.ITEM_ID  ");
            sb.Append(" LEFT JOIN UOM_INFO Puom ON inv.PANEL_PC_UOM=Puom.UOM_ID ");
            sb.Append(" LEFT JOIN   BOM_MST_T bmst ON inv.ITEM_ID = bmst.BOM_ITEM_ID and bmst.ISACTIVE = 'Y' AND bmst.FROM_DEPARTMENT_ID=dept.DEPT_ID");
            sb.Append(" LEFT JOIN  PROD_ITEM_STANDARD_WEIGHT pwt ON pwt.ITEM_ID=inv.ITEM_ID AND pwt.DEPT_ID=dept.DEPT_ID ");
            sb.Append(" LEFT JOIN  PROD_PASTING_GRID_MAPPING gp ON gp.PASTE_ITEM_ID=inv.ITEM_ID ");
            sb.Append(" LEFT JOIN INV_ITEM_MASTER ginv ON gp.GRID_ITEM_ID=ginv.ITEM_ID  ");
            sb.Append(" Where 1=1  ");

            return sb.ToString();
        }

    }
}
