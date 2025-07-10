using PG.Core.DBBase;
using PG.Core.Utility;
using PG.DBClass.InventoryDC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Text;

namespace PG.BLLibrary.InventoryBL
{
    public class INV_ITEM_MASTERBL
    {
        public static DataLoadOptions INV_ITEM_MASTERLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcINV_ITEM_MASTER>(obj => obj.relatedclassname);
            return dlo;
        }

        public static string GetItem_Master_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("   Select INV_ITEM_MASTER.* ");
            sb.Append(" , INV_ITEM_GROUP.ITEM_GROUP_CODE, INV_ITEM_GROUP.ITEM_GROUP_NAME  ");
            sb.Append(" , INV_ITEM_CLASS.ITEM_CLASS_CODE, INV_ITEM_CLASS.ITEM_CLASS_NAME  ");
            //sb.Append(" , INV_ITEM_TYPE.ITEM_TYPE_CODE, INV_ITEM_TYPE.ITEM_TYPE_NAME  ");
            sb.Append(" , UOM_INFO.UOM_CODE, UOM_INFO.UOM_NAME , SNS.ITEM_SNS_NAME ");
            //sb.Append(" ,GET_CLOSING_QTY(INV_ITEM_MASTER.ITEM_ID) CLOSING_QTY ");           
            sb.Append(" FROM INV_ITEM_MASTER ");
            sb.Append(" INNER JOIN INV_ITEM_GROUP ON INV_ITEM_MASTER.ITEM_GROUP_ID = INV_ITEM_GROUP.ITEM_GROUP_ID ");
            sb.Append(" INNER JOIN INV_ITEM_CLASS ON INV_ITEM_MASTER.ITEM_CLASS_ID = INV_ITEM_CLASS.ITEM_CLASS_ID ");
            //sb.Append(" INNER JOIN INV_ITEM_TYPE ON INV_ITEM_MASTER.ITEM_TYPE_ID = INV_ITEM_TYPE.ITEM_TYPE_ID ");
            sb.Append(" INNER JOIN UOM_INFO ON INV_ITEM_MASTER.UOM_ID = UOM_INFO.UOM_ID ");
            sb.Append(" LEFT JOIN  INV_ITEM_SNS_MST SNS ON  INV_ITEM_MASTER.ITEM_SNS_ID=SNS.ITEM_SNS_ID ");
            sb.Append(" Where 1=1 AND  INV_ITEM_MASTER.IS_ACTIVE='Y' ");
            return sb.ToString();
        }

        public static string GetItem_Master_ByStore_SQLString(int storeId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("   Select INV_ITEM_MASTER.* ");
            sb.Append(" , INV_ITEM_GROUP.ITEM_GROUP_CODE, INV_ITEM_GROUP.ITEM_GROUP_NAME  ");
            sb.Append(" , INV_ITEM_CLASS.ITEM_CLASS_CODE, INV_ITEM_CLASS.ITEM_CLASS_NAME  ");
            //sb.Append(" , INV_ITEM_TYPE.ITEM_TYPE_CODE, INV_ITEM_TYPE.ITEM_TYPE_NAME  ");
            sb.Append(" , UOM_INFO.UOM_CODE, UOM_INFO.UOM_NAME , SNS.ITEM_SNS_NAME ");
            //sb.Append(" ,GET_CLOSING_QTY(INV_ITEM_MASTER.ITEM_ID) CLOSING_QTY ");
            sb.Append(" ,GET_CLOSING_QTY_BY_STORE_IGR(INV_ITEM_MASTER.ITEM_ID," + storeId + ") CLOSING_QTY ");
            sb.Append(" FROM INV_ITEM_MASTER ");
            sb.Append(" INNER JOIN INV_ITEM_GROUP ON INV_ITEM_MASTER.ITEM_GROUP_ID = INV_ITEM_GROUP.ITEM_GROUP_ID ");
            sb.Append(" INNER JOIN INV_ITEM_CLASS ON INV_ITEM_MASTER.ITEM_CLASS_ID = INV_ITEM_CLASS.ITEM_CLASS_ID ");
            //sb.Append(" INNER JOIN INV_ITEM_TYPE ON INV_ITEM_MASTER.ITEM_TYPE_ID = INV_ITEM_TYPE.ITEM_TYPE_ID ");
            sb.Append(" INNER JOIN UOM_INFO ON INV_ITEM_MASTER.UOM_ID = UOM_INFO.UOM_ID ");
            sb.Append(" LEFT JOIN  INV_ITEM_SNS_MST SNS ON  INV_ITEM_MASTER.ITEM_SNS_ID=SNS.ITEM_SNS_ID ");
            sb.Append(" Where 1=1 AND  INV_ITEM_MASTER.IS_ACTIVE='Y' ");
            return sb.ToString();
        }

        public static string GetNullItem_Master_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("   Select INV_ITEM_MASTER.* ");
            sb.Append(" , INV_ITEM_GROUP.ITEM_GROUP_CODE, INV_ITEM_GROUP.ITEM_GROUP_NAME  ");
            sb.Append(" , INV_ITEM_CLASS.ITEM_CLASS_CODE, INV_ITEM_CLASS.ITEM_CLASS_NAME  ");
            //sb.Append(" , INV_ITEM_TYPE.ITEM_TYPE_CODE, INV_ITEM_TYPE.ITEM_TYPE_NAME  ");
            sb.Append(" , UOM_INFO.UOM_CODE, UOM_INFO.UOM_NAME , SNS.ITEM_SNS_NAME ");
            //sb.Append(" , GET_CLOSING_QTY_BY_TYPE(INV_ITEM_MASTER.ITEM_ID) CLOSING_QTY ");
            sb.Append(" ,GET_CLOSING_QTY(INV_ITEM_MASTER.ITEM_ID) CLOSING_QTY ");
            sb.Append(" FROM INV_ITEM_MASTER ");
            sb.Append(" INNER JOIN INV_ITEM_GROUP ON INV_ITEM_MASTER.ITEM_GROUP_ID = INV_ITEM_GROUP.ITEM_GROUP_ID ");
            sb.Append(" INNER JOIN INV_ITEM_CLASS ON INV_ITEM_MASTER.ITEM_CLASS_ID = INV_ITEM_CLASS.ITEM_CLASS_ID ");
            //sb.Append(" INNER JOIN INV_ITEM_TYPE ON INV_ITEM_MASTER.ITEM_TYPE_ID = INV_ITEM_TYPE.ITEM_TYPE_ID ");
            sb.Append(" INNER JOIN UOM_INFO ON INV_ITEM_MASTER.UOM_ID = UOM_INFO.UOM_ID ");
            sb.Append(" LEFT JOIN  INV_ITEM_SNS_MST SNS ON  INV_ITEM_MASTER.ITEM_SNS_ID=SNS.ITEM_SNS_ID ");
            sb.Append(" INNER  JOIN ITEM_STOCK_DETAILS STK ON INV_ITEM_MASTER.ITEM_ID=STK.ITEM_ID ");
            sb.Append(" Where 1=1 AND  INV_ITEM_MASTER.IS_ACTIVE='Y' AND INV_ITEM_MASTER.ITEM_ID=0 ");
            sb.Append(" AND STK.ITEM_TYPE_ID=0 ");
            return sb.ToString();
        }

        public static string GetItem_Master_ByType_SQLString(int pItemTypeID,int pStoreID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("   Select INV_ITEM_MASTER.* ");
            sb.Append(" , INV_ITEM_GROUP.ITEM_GROUP_CODE, INV_ITEM_GROUP.ITEM_GROUP_NAME  ");
            sb.Append(" , INV_ITEM_CLASS.ITEM_CLASS_CODE, INV_ITEM_CLASS.ITEM_CLASS_NAME  ");
            //sb.Append(" , INV_ITEM_TYPE.ITEM_TYPE_CODE, INV_ITEM_TYPE.ITEM_TYPE_NAME  ");
            sb.Append(" , UOM_INFO.UOM_CODE, UOM_INFO.UOM_NAME , SNS.ITEM_SNS_NAME ");
            //sb.Append(" , GET_CLOSING_QTY_BY_TYPE(INV_ITEM_MASTER.ITEM_ID,'" + pItemTypeID + "') CLOSING_QTY ");
            sb.Append(" , GET_CLOSING_QTY_FOR_IGR(INV_ITEM_MASTER.ITEM_ID,'" + pItemTypeID + "','" + pStoreID + "') CLOSING_QTY ");
            //sb.Append(" ,GET_CLOSING_QTY(INV_ITEM_MASTER.ITEM_ID) CLOSING_QTY ");
            //sb.Append(" ,GET_PENDING_REQ_QTY_BY_TYPE(INV_ITEM_MASTER.ITEM_ID,'" + pItemTypeID + "') PENDING_REQ_QNTY ");
            sb.Append(" FROM INV_ITEM_MASTER ");
            sb.Append(" INNER JOIN INV_ITEM_GROUP ON INV_ITEM_MASTER.ITEM_GROUP_ID = INV_ITEM_GROUP.ITEM_GROUP_ID ");
            sb.Append(" INNER JOIN INV_ITEM_CLASS ON INV_ITEM_MASTER.ITEM_CLASS_ID = INV_ITEM_CLASS.ITEM_CLASS_ID ");
            //sb.Append(" INNER JOIN INV_ITEM_TYPE ON INV_ITEM_MASTER.ITEM_TYPE_ID = INV_ITEM_TYPE.ITEM_TYPE_ID ");
            sb.Append(" INNER JOIN UOM_INFO ON INV_ITEM_MASTER.UOM_ID = UOM_INFO.UOM_ID ");
            sb.Append(" LEFT JOIN  INV_ITEM_SNS_MST SNS ON  INV_ITEM_MASTER.ITEM_SNS_ID=SNS.ITEM_SNS_ID ");
            sb.Append(" Where 1=1 AND  INV_ITEM_MASTER.IS_ACTIVE='Y' ");
            return sb.ToString();
        }

        public static string GetItem_Master_ByType_Store_SQLString(int pItemID,int pItemTypeID, int pStoreID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("   Select INV_ITEM_MASTER.* ");
            sb.Append(" , INV_ITEM_GROUP.ITEM_GROUP_CODE, INV_ITEM_GROUP.ITEM_GROUP_NAME  ");
            sb.Append(" , INV_ITEM_CLASS.ITEM_CLASS_CODE, INV_ITEM_CLASS.ITEM_CLASS_NAME  ");
            //sb.Append(" , INV_ITEM_TYPE.ITEM_TYPE_CODE, INV_ITEM_TYPE.ITEM_TYPE_NAME  ");
            sb.Append(" , UOM_INFO.UOM_CODE, UOM_INFO.UOM_NAME , SNS.ITEM_SNS_NAME ");
            sb.Append(" , GET_CLOSING_QTY_BY_TYPE('" + pItemID + "','" + pItemTypeID + "','" + pStoreID + "') CLOSING_QTY ");
            sb.Append(" ,GET_AVG_PRICE_BY_TYPE(INV_ITEM_MASTER.ITEM_ID,1,'" + pStoreID + "') UNIT_PRICE ");
            //sb.Append(" , GET_CLOSING_QTY_FOR_IGR('" + pItemID + "','" + pItemTypeID + "','" + pStoreID + "') CLOSING_QTY ");
            //sb.Append(" ,GET_CLOSING_QTY(INV_ITEM_MASTER.ITEM_ID) CLOSING_QTY ");
            //sb.Append(" ,GET_PENDING_REQ_QTY_BY_TYPE(INV_ITEM_MASTER.ITEM_ID,'" + pItemTypeID + "') PENDING_REQ_QNTY ");
            sb.Append(" FROM INV_ITEM_MASTER ");
            sb.Append(" INNER JOIN INV_ITEM_GROUP ON INV_ITEM_MASTER.ITEM_GROUP_ID = INV_ITEM_GROUP.ITEM_GROUP_ID ");
            sb.Append(" INNER JOIN INV_ITEM_CLASS ON INV_ITEM_MASTER.ITEM_CLASS_ID = INV_ITEM_CLASS.ITEM_CLASS_ID ");
            //sb.Append(" INNER JOIN INV_ITEM_TYPE ON INV_ITEM_MASTER.ITEM_TYPE_ID = INV_ITEM_TYPE.ITEM_TYPE_ID ");
            sb.Append(" INNER JOIN UOM_INFO ON INV_ITEM_MASTER.UOM_ID = UOM_INFO.UOM_ID ");
            sb.Append(" LEFT JOIN  INV_ITEM_SNS_MST SNS ON  INV_ITEM_MASTER.ITEM_SNS_ID=SNS.ITEM_SNS_ID ");
            sb.Append(" Where 1=1 AND  INV_ITEM_MASTER.IS_ACTIVE='Y' ");
            return sb.ToString();
        }

        public static string GetItem_Dept_Wise_ByType_SQLString(int pdeptID,int pItemTypeID,int pStoreID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("   Select INV_ITEM_MASTER.* ");
            sb.Append(" , INV_ITEM_GROUP.ITEM_GROUP_CODE, INV_ITEM_GROUP.ITEM_GROUP_NAME  ");
            sb.Append(" , INV_ITEM_CLASS.ITEM_CLASS_CODE, INV_ITEM_CLASS.ITEM_CLASS_NAME  ");
            sb.Append(" , UOM_INFO.UOM_CODE, UOM_INFO.UOM_NAME , SNS.ITEM_SNS_NAME ");
            //sb.Append(" , GET_CLOSING_QTY_BY_TYPE(INV_ITEM_MASTER.ITEM_ID,'" + pItemTypeID + "') CLOSING_QTY ");
            if(pItemTypeID > 0)
            {
                sb.Append(" , GET_CLOSING_QTY_FOR_IGR(INV_ITEM_MASTER.ITEM_ID,'" + pItemTypeID + "','" + pStoreID + "') CLOSING_QTY ");
            }
            else
            {
             
              sb.Append(" , GET_CLOSING_QTY_BY_STORE_IGR(INV_ITEM_MASTER.ITEM_ID,'" + pStoreID + "') CLOSING_QTY ");
              sb.Append(" ,GET_AVG_PRICE_BY_TYPE(INV_ITEM_MASTER.ITEM_ID,1,'" + pStoreID + "') UNIT_PRICE "); 
            }
           
            //sb.Append(" ,GET_CLOSING_QTY(INV_ITEM_MASTER.ITEM_ID) CLOSING_QTY ");
            sb.Append(" FROM INV_ITEM_MASTER ");
            sb.Append(" INNER JOIN INV_ITEM_GROUP ON INV_ITEM_MASTER.ITEM_GROUP_ID = INV_ITEM_GROUP.ITEM_GROUP_ID ");
            sb.Append(" INNER JOIN INV_ITEM_CLASS ON INV_ITEM_MASTER.ITEM_CLASS_ID = INV_ITEM_CLASS.ITEM_CLASS_ID ");
            sb.Append(" INNER JOIN UOM_INFO ON INV_ITEM_MASTER.UOM_ID = UOM_INFO.UOM_ID ");
            sb.Append(" INNER JOIN PRO_DEPARTMENT_ITEM dept on INV_ITEM_MASTER.ITEM_ID=DEPT.ITEM_ID ");
            sb.Append(" LEFT JOIN  INV_ITEM_SNS_MST SNS ON  INV_ITEM_MASTER.ITEM_SNS_ID=SNS.ITEM_SNS_ID ");
            sb.Append(" Where 1=1 AND  INV_ITEM_MASTER.IS_ACTIVE='Y' ");
            sb.Append(" and DEPT.DEPT_ID= '" + pdeptID + "' ");
            return sb.ToString();
        }

        public static string GetItem_Dept_List_ByType_SQLString(int pdeptID, int pItemTypeID, int pStoreID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("   Select INV_ITEM_MASTER.* ");
            sb.Append(" , INV_ITEM_GROUP.ITEM_GROUP_CODE, INV_ITEM_GROUP.ITEM_GROUP_NAME  ");
            sb.Append(" , INV_ITEM_CLASS.ITEM_CLASS_CODE, INV_ITEM_CLASS.ITEM_CLASS_NAME  ");
            sb.Append(" , UOM_INFO.UOM_CODE, UOM_INFO.UOM_NAME , SNS.ITEM_SNS_NAME ");
            //sb.Append(" , GET_CLOSING_QTY_BY_TYPE(INV_ITEM_MASTER.ITEM_ID,'" + pItemTypeID + "') CLOSING_QTY ");
            //if (pItemTypeID > 0)
            //{
            //    sb.Append(" , GET_CLOSING_QTY_FOR_IGR(INV_ITEM_MASTER.ITEM_ID,'" + pItemTypeID + "','" + pStoreID + "') CLOSING_QTY ");
            //}
            //else
            //{

            //    sb.Append(" , GET_CLOSING_QTY_BY_STORE_IGR(INV_ITEM_MASTER.ITEM_ID,'" + pStoreID + "') CLOSING_QTY ");
            //    sb.Append(" ,GET_AVG_PRICE_BY_TYPE(INV_ITEM_MASTER.ITEM_ID,1,'" + pStoreID + "') UNIT_PRICE ");
            //}

            //sb.Append(" ,GET_CLOSING_QTY(INV_ITEM_MASTER.ITEM_ID) CLOSING_QTY ");
            sb.Append(" FROM INV_ITEM_MASTER ");
            sb.Append(" INNER JOIN INV_ITEM_GROUP ON INV_ITEM_MASTER.ITEM_GROUP_ID = INV_ITEM_GROUP.ITEM_GROUP_ID ");
            sb.Append(" INNER JOIN INV_ITEM_CLASS ON INV_ITEM_MASTER.ITEM_CLASS_ID = INV_ITEM_CLASS.ITEM_CLASS_ID ");
            sb.Append(" INNER JOIN UOM_INFO ON INV_ITEM_MASTER.UOM_ID = UOM_INFO.UOM_ID ");
            sb.Append(" INNER JOIN PRO_DEPARTMENT_ITEM dept on INV_ITEM_MASTER.ITEM_ID=DEPT.ITEM_ID ");
            sb.Append(" LEFT JOIN  INV_ITEM_SNS_MST SNS ON  INV_ITEM_MASTER.ITEM_SNS_ID=SNS.ITEM_SNS_ID ");
            sb.Append(" Where 1=1 AND  INV_ITEM_MASTER.IS_ACTIVE='Y' ");
            sb.Append(" and DEPT.DEPT_ID= '" + pdeptID + "' ");
            return sb.ToString();
        }

        public static string GetItem_Dept_Wise_ByType_Store_SQLString(int pdeptID, int pItemTypeID, int pStoreID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT DISTINCT A.* ");
           
            if (pItemTypeID > 0)
            {
                sb.Append(" , GET_CLOSING_QTY_FOR_IGR(A.ITEM_ID,'" + pItemTypeID + "','" + pStoreID + "') CLOSING_QTY ");
            }
            else
            {

                sb.Append(" , GET_CLOSING_QTY_BY_STORE_IGR(A.ITEM_ID,'" + pStoreID + "') CLOSING_QTY ");

            }
            sb.Append(" FROM ");
            sb.Append(" ( ");
            sb.Append("   SELECT INV_ITEM_MASTER.ITEM_ID,INV_ITEM_MASTER.ITEM_GROUP_ID,INV_ITEM_MASTER.ITEM_CODE, INV_ITEM_MASTER.ITEM_NAME  ");
            sb.Append(" , INV_ITEM_GROUP.ITEM_GROUP_CODE, INV_ITEM_GROUP.ITEM_GROUP_NAME   ");
            sb.Append(" , INV_ITEM_CLASS.ITEM_CLASS_CODE, INV_ITEM_CLASS.ITEM_CLASS_NAME   ");
            sb.Append(" , UOM_INFO.UOM_CODE  ");
            //sb.Append(" , GET_CLOSING_QTY_FOR_IGR(INV_ITEM_MASTER.ITEM_ID,'" + pItemTypeID + "','" + pStoreID + "') CLOSING_QTY ");
            sb.Append(" FROM PRO_DEPARTMENT_ITEM DEPT ");
            sb.Append(" INNER JOIN INV_ITEM_MASTER  ON DEPT.ITEM_ID=INV_ITEM_MASTER.ITEM_ID ");
            sb.Append(" INNER JOIN INV_ITEM_GROUP ON INV_ITEM_MASTER.ITEM_GROUP_ID = INV_ITEM_GROUP.ITEM_GROUP_ID ");
            sb.Append(" INNER JOIN INV_ITEM_CLASS ON INV_ITEM_MASTER.ITEM_CLASS_ID = INV_ITEM_CLASS.ITEM_CLASS_ID ");
            sb.Append(" INNER JOIN UOM_INFO ON INV_ITEM_MASTER.UOM_ID = UOM_INFO.UOM_ID  ");
            sb.Append(" WHERE 1=1 ");
            sb.Append(" AND INV_ITEM_MASTER.IS_ACTIVE='Y' ");
            sb.Append(" AND DEPT.DEPT_ID= '" + pdeptID + "' ");
            sb.Append(" UNION ALL ");
            sb.Append(" SELECT INV_ITEM_MASTER.ITEM_ID,INV_ITEM_MASTER.ITEM_GROUP_ID,INV_ITEM_MASTER.ITEM_CODE,INV_ITEM_MASTER.ITEM_NAME ");
            sb.Append(" , INV_ITEM_GROUP.ITEM_GROUP_CODE, INV_ITEM_GROUP.ITEM_GROUP_NAME ");
            sb.Append(" , INV_ITEM_CLASS.ITEM_CLASS_CODE, INV_ITEM_CLASS.ITEM_CLASS_NAME ");
            sb.Append(" , UOM_INFO.UOM_CODE ");
            //sb.Append(" , GET_CLOSING_QTY_FOR_IGR(INV_ITEM_MASTER.ITEM_ID,'" + pItemTypeID + "','" + pStoreID + "') CLOSING_QTY ");
            sb.Append(" FROM INV_ITEM_MASTER ");
            sb.Append(" INNER JOIN INV_ITEM_GROUP ON INV_ITEM_MASTER.ITEM_GROUP_ID = INV_ITEM_GROUP.ITEM_GROUP_ID ");
            sb.Append(" INNER JOIN INV_ITEM_CLASS ON INV_ITEM_MASTER.ITEM_CLASS_ID = INV_ITEM_CLASS.ITEM_CLASS_ID ");
            sb.Append(" INNER JOIN UOM_INFO ON INV_ITEM_MASTER.UOM_ID = UOM_INFO.UOM_ID ");
            sb.Append(" WHERE 1=1 ");
            sb.Append(" AND INV_ITEM_MASTER.IS_ACTIVE='Y' ");
            sb.Append(" AND INV_ITEM_MASTER.IS_COMMON_ITEM='Y' ");
            sb.Append(" ) A ");
            sb.Append(" WHERE 1=1 ");

            return sb.ToString();
        }
        public static string GetItem_Master_With_DeptStock_SQLString(int deptId,int?specId)
        {
           StringBuilder sb = new StringBuilder();
            sb.Append("Select INV_ITEM_MASTER.* ");
            sb.Append(" , INV_ITEM_GROUP.ITEM_GROUP_CODE, INV_ITEM_GROUP.ITEM_GROUP_NAME  ");
            sb.Append(" , INV_ITEM_CLASS.ITEM_CLASS_CODE, INV_ITEM_CLASS.ITEM_CLASS_NAME  ");
            //sb.Append(" , INV_ITEM_TYPE.ITEM_TYPE_CODE, INV_ITEM_TYPE.ITEM_TYPE_NAME  ");
            sb.Append(" , UOM_INFO.UOM_CODE, UOM_INFO.UOM_NAME , SNS.ITEM_SNS_NAME ");
            sb.Append(" ,GET_DEPT_CLOSING_QTY(INV_ITEM_MASTER.ITEM_ID,'" + deptId + "','" + specId + "')CLOSING_QTY ");         
            sb.Append(" FROM INV_ITEM_MASTER ");
            sb.Append(" INNER JOIN INV_ITEM_GROUP ON INV_ITEM_MASTER.ITEM_GROUP_ID = INV_ITEM_GROUP.ITEM_GROUP_ID ");
            sb.Append(" INNER JOIN INV_ITEM_CLASS ON INV_ITEM_MASTER.ITEM_CLASS_ID = INV_ITEM_CLASS.ITEM_CLASS_ID ");
            //sb.Append(" INNER JOIN INV_ITEM_TYPE ON INV_ITEM_MASTER.ITEM_TYPE_ID = INV_ITEM_TYPE.ITEM_TYPE_ID ");
            sb.Append(" INNER JOIN UOM_INFO ON INV_ITEM_MASTER.UOM_ID = UOM_INFO.UOM_ID ");
            sb.Append(" LEFT JOIN  INV_ITEM_SNS_MST SNS ON  INV_ITEM_MASTER.ITEM_SNS_ID=SNS.ITEM_SNS_ID ");
             
             //sb.Append(" LEFT JOIN PRO_DEPARTMENT_ITEM dept ON INV_ITEM_MASTER.ITEM_ID = dept.ITEM_ID ");

            sb.Append(" Where 1=1  AND  INV_ITEM_MASTER.IS_ACTIVE='Y' ");
            return sb.ToString();
        }

        public static string GetItem_Master_Prod_DeptStock_SQLString(int deptId, int? specId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Select INV_ITEM_MASTER.* ");
            sb.Append(" , INV_ITEM_GROUP.ITEM_GROUP_CODE, INV_ITEM_GROUP.ITEM_GROUP_NAME  ");
            sb.Append(" , INV_ITEM_CLASS.ITEM_CLASS_CODE, INV_ITEM_CLASS.ITEM_CLASS_NAME  ");
            //sb.Append(" , INV_ITEM_TYPE.ITEM_TYPE_CODE, INV_ITEM_TYPE.ITEM_TYPE_NAME  ");
            sb.Append(" , UOM_INFO.UOM_CODE, UOM_INFO.UOM_NAME , SNS.ITEM_SNS_NAME ");
            //sb.Append(" ,GET_DEPT_WISE_CLOSING_QTY(INV_ITEM_MASTER.ITEM_ID,'" + deptId + "') CLOSING_QTY ");
            sb.Append(" ,GET_DEPARTMENT_CLOSING_QTY(INV_ITEM_MASTER.ITEM_ID,'" + deptId + "') CLOSING_QTY ");
            
            sb.Append(" FROM INV_ITEM_MASTER ");
            sb.Append(" INNER JOIN INV_ITEM_GROUP ON INV_ITEM_MASTER.ITEM_GROUP_ID = INV_ITEM_GROUP.ITEM_GROUP_ID ");
            sb.Append(" INNER JOIN INV_ITEM_CLASS ON INV_ITEM_MASTER.ITEM_CLASS_ID = INV_ITEM_CLASS.ITEM_CLASS_ID ");
            //sb.Append(" INNER JOIN INV_ITEM_TYPE ON INV_ITEM_MASTER.ITEM_TYPE_ID = INV_ITEM_TYPE.ITEM_TYPE_ID ");
            sb.Append(" INNER JOIN UOM_INFO ON INV_ITEM_MASTER.UOM_ID = UOM_INFO.UOM_ID ");
            sb.Append(" LEFT JOIN  INV_ITEM_SNS_MST SNS ON  INV_ITEM_MASTER.ITEM_SNS_ID=SNS.ITEM_SNS_ID ");
            sb.Append("  INNER JOIN PRO_DEPARTMENT_ITEM dept on INV_ITEM_MASTER.ITEM_ID=DEPT.ITEM_ID ");
            sb.Append(" Where 1=1  AND  INV_ITEM_MASTER.IS_ACTIVE='Y' ");
            sb.Append(" and DEPT.DEPT_ID= '" + deptId + "' ");
            return sb.ToString();
        }

        public static string GetItem_Master_With_Return_SQLString(int itemtypeId,int storeId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Select INV_ITEM_MASTER.* ");
            sb.Append(" , INV_ITEM_GROUP.ITEM_GROUP_CODE, INV_ITEM_GROUP.ITEM_GROUP_NAME  ");
            sb.Append(" , INV_ITEM_CLASS.ITEM_CLASS_CODE, INV_ITEM_CLASS.ITEM_CLASS_NAME  ");
            //sb.Append(" , INV_ITEM_TYPE.ITEM_TYPE_CODE, INV_ITEM_TYPE.ITEM_TYPE_NAME  ");
            sb.Append(" , UOM_INFO.UOM_CODE, UOM_INFO.UOM_NAME , SNS.ITEM_SNS_NAME ");
            sb.Append(" ,GET_RETURNABLE_QTY(INV_ITEM_MASTER.ITEM_ID,'" + itemtypeId + "','" + storeId + "')CLOSING_QTY ");
            sb.Append(" FROM INV_ITEM_MASTER ");
            sb.Append(" INNER JOIN INV_ITEM_GROUP ON INV_ITEM_MASTER.ITEM_GROUP_ID = INV_ITEM_GROUP.ITEM_GROUP_ID ");
            sb.Append(" INNER JOIN INV_ITEM_CLASS ON INV_ITEM_MASTER.ITEM_CLASS_ID = INV_ITEM_CLASS.ITEM_CLASS_ID ");
            //sb.Append(" INNER JOIN INV_ITEM_TYPE ON INV_ITEM_MASTER.ITEM_TYPE_ID = INV_ITEM_TYPE.ITEM_TYPE_ID ");
            sb.Append(" INNER JOIN UOM_INFO ON INV_ITEM_MASTER.UOM_ID = UOM_INFO.UOM_ID ");
            sb.Append(" LEFT JOIN  INV_ITEM_SNS_MST SNS ON  INV_ITEM_MASTER.ITEM_SNS_ID=SNS.ITEM_SNS_ID ");

            //sb.Append(" LEFT JOIN PRO_DEPARTMENT_ITEM dept ON INV_ITEM_MASTER.ITEM_ID = dept.ITEM_ID ");

            sb.Append(" Where 1=1  AND  INV_ITEM_MASTER.IS_ACTIVE='Y' ");
            return sb.ToString();
        }

        public static string GetItem_Mst_With_DeptStk_Bytype_SQLString(int deptId, int? specId,int itemtype)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Select INV_ITEM_MASTER.* ");
            sb.Append(" , INV_ITEM_GROUP.ITEM_GROUP_CODE, INV_ITEM_GROUP.ITEM_GROUP_NAME  ");
            sb.Append(" , INV_ITEM_CLASS.ITEM_CLASS_CODE, INV_ITEM_CLASS.ITEM_CLASS_NAME  ");
            sb.Append(" , UOM_INFO.UOM_CODE, UOM_INFO.UOM_NAME , SNS.ITEM_SNS_NAME ");
            sb.Append(" ,GET_DEPT_CLOSING_QTY(INV_ITEM_MASTER.ITEM_ID,'" + deptId + "','" + specId + "')CLOSING_QTY ");   
            //sb.Append(" ,GET_DEPT_CLOSING_QTY_BY_TYPE(INV_ITEM_MASTER.ITEM_ID,'" + deptId + "','" + specId + "','"+ itemtype +"')CLOSING_QTY ");
            sb.Append(" FROM INV_ITEM_MASTER ");
            sb.Append(" INNER JOIN INV_ITEM_GROUP ON INV_ITEM_MASTER.ITEM_GROUP_ID = INV_ITEM_GROUP.ITEM_GROUP_ID ");
            sb.Append(" INNER JOIN INV_ITEM_CLASS ON INV_ITEM_MASTER.ITEM_CLASS_ID = INV_ITEM_CLASS.ITEM_CLASS_ID ");
            sb.Append(" INNER JOIN UOM_INFO ON INV_ITEM_MASTER.UOM_ID = UOM_INFO.UOM_ID ");
            sb.Append(" LEFT JOIN  INV_ITEM_SNS_MST SNS ON  INV_ITEM_MASTER.ITEM_SNS_ID=SNS.ITEM_SNS_ID ");

            //sb.Append(" LEFT JOIN PRO_DEPARTMENT_ITEM dept ON INV_ITEM_MASTER.ITEM_ID = dept.ITEM_ID ");

            sb.Append(" Where 1=1  AND  INV_ITEM_MASTER.IS_ACTIVE='Y' ");
            return sb.ToString();
        }

        public static string GetItem_Mst_Prod_DeptStk_Bytype_SQLString(int deptId, int? specId, int itemtype)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Select INV_ITEM_MASTER.* ");
            sb.Append(" , INV_ITEM_GROUP.ITEM_GROUP_CODE, INV_ITEM_GROUP.ITEM_GROUP_NAME  ");
            sb.Append(" , INV_ITEM_CLASS.ITEM_CLASS_CODE, INV_ITEM_CLASS.ITEM_CLASS_NAME  ");
            sb.Append(" , UOM_INFO.UOM_CODE, UOM_INFO.UOM_NAME , SNS.ITEM_SNS_NAME ");
            //sb.Append(" ,GET_DEPT_CLOSING_QTY_BY_TYPE(INV_ITEM_MASTER.ITEM_ID,'" + deptId + "','" + specId + "','" + itemtype + "')CLOSING_QTY ");
            sb.Append(" ,GET_DEPARTMENT_CLOSING_QTY(INV_ITEM_MASTER.ITEM_ID,'" + deptId + "') CLOSING_QTY ");
            sb.Append(" FROM INV_ITEM_MASTER ");
            sb.Append(" INNER JOIN INV_ITEM_GROUP ON INV_ITEM_MASTER.ITEM_GROUP_ID = INV_ITEM_GROUP.ITEM_GROUP_ID ");
            sb.Append(" INNER JOIN INV_ITEM_CLASS ON INV_ITEM_MASTER.ITEM_CLASS_ID = INV_ITEM_CLASS.ITEM_CLASS_ID ");
            sb.Append(" INNER JOIN UOM_INFO ON INV_ITEM_MASTER.UOM_ID = UOM_INFO.UOM_ID ");
            sb.Append(" INNER JOIN PRO_DEPARTMENT_ITEM dept on INV_ITEM_MASTER.ITEM_ID=DEPT.ITEM_ID ");
            sb.Append(" LEFT JOIN  INV_ITEM_SNS_MST SNS ON  INV_ITEM_MASTER.ITEM_SNS_ID=SNS.ITEM_SNS_ID ");

            //sb.Append(" LEFT JOIN PRO_DEPARTMENT_ITEM dept ON INV_ITEM_MASTER.ITEM_ID = dept.ITEM_ID ");

            sb.Append(" Where 1=1  AND  INV_ITEM_MASTER.IS_ACTIVE='Y' ");
            sb.Append(" and DEPT.DEPT_ID='" + deptId + "' ");
            return sb.ToString();
        }


        public static string GetItem_PROD_Master_With_DeptStock_SQLString(int deptId, int? specId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Select INV_ITEM_MASTER.* ");
            sb.Append(" , INV_ITEM_GROUP.ITEM_GROUP_CODE, INV_ITEM_GROUP.ITEM_GROUP_NAME  ");
            sb.Append(" , INV_ITEM_CLASS.ITEM_CLASS_CODE, INV_ITEM_CLASS.ITEM_CLASS_NAME  ");
            sb.Append(" , INV_ITEM_TYPE.ITEM_TYPE_CODE, INV_ITEM_TYPE.ITEM_TYPE_NAME  ");
            sb.Append(" , UOM_INFO.UOM_CODE, UOM_INFO.UOM_NAME , SNS.ITEM_SNS_NAME ");
            sb.Append(" ,GET_DEPT_CLOSING_QTY(INV_ITEM_MASTER.ITEM_ID,'" + deptId + "','" + specId + "')CLOSING_QTY ");
            sb.Append(" FROM INV_ITEM_MASTER ");
            sb.Append(" INNER JOIN INV_ITEM_GROUP ON INV_ITEM_MASTER.ITEM_GROUP_ID = INV_ITEM_GROUP.ITEM_GROUP_ID ");
            sb.Append(" INNER JOIN INV_ITEM_CLASS ON INV_ITEM_MASTER.ITEM_CLASS_ID = INV_ITEM_CLASS.ITEM_CLASS_ID ");
            sb.Append(" INNER JOIN INV_ITEM_TYPE ON INV_ITEM_MASTER.ITEM_TYPE_ID = INV_ITEM_TYPE.ITEM_TYPE_ID ");
            sb.Append(" INNER JOIN UOM_INFO ON INV_ITEM_MASTER.UOM_ID = UOM_INFO.UOM_ID ");
            sb.Append(" LEFT JOIN  INV_ITEM_SNS_MST SNS ON  INV_ITEM_MASTER.ITEM_SNS_ID=SNS.ITEM_SNS_ID ");
            sb.Append(" INNER JOIN PRO_DEPARTMENT_ITEM dept ON INV_ITEM_MASTER.ITEM_ID = dept.ITEM_ID ");

            sb.Append(" Where 1=1  AND  INV_ITEM_MASTER.IS_ACTIVE='Y'  ");
            return sb.ToString();
        }


        public static string GetDeptRejectItem_Master_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("   Select INV_ITEM_MASTER.* ");
            sb.Append(" , INV_ITEM_GROUP.ITEM_GROUP_CODE, INV_ITEM_GROUP.ITEM_GROUP_NAME  ");
            sb.Append(" , INV_ITEM_CLASS.ITEM_CLASS_CODE, INV_ITEM_CLASS.ITEM_CLASS_NAME  ");
            sb.Append(" , INV_ITEM_TYPE.ITEM_TYPE_CODE, INV_ITEM_TYPE.ITEM_TYPE_NAME  ");
            sb.Append(" , UOM_INFO.UOM_CODE ");
            sb.Append(" , UOM_INFO.UOM_NAME,GET_DEPT_REJECT_CLOSING_QTY(INV_ITEM_MASTER.ITEM_ID,dept.DEPT_ID) CLOSING_QTY ");
            sb.Append(" FROM INV_ITEM_MASTER ");
            sb.Append(" INNER JOIN INV_ITEM_GROUP ON INV_ITEM_MASTER.ITEM_GROUP_ID = INV_ITEM_GROUP.ITEM_GROUP_ID ");
            sb.Append(" INNER JOIN INV_ITEM_CLASS ON INV_ITEM_MASTER.ITEM_CLASS_ID = INV_ITEM_CLASS.ITEM_CLASS_ID ");
            sb.Append(" INNER JOIN INV_ITEM_TYPE ON INV_ITEM_MASTER.ITEM_TYPE_ID = INV_ITEM_TYPE.ITEM_TYPE_ID ");
            sb.Append(" INNER JOIN UOM_INFO ON INV_ITEM_MASTER.UOM_ID = UOM_INFO.UOM_ID ");
            sb.Append(" LEFT JOIN PRO_DEPARTMENT_ITEM dept ON INV_ITEM_MASTER.ITEM_ID = dept.ITEM_ID ");
            sb.Append(" Where 1=1 and GET_DEPT_REJECT_CLOSING_QTY(INV_ITEM_MASTER.ITEM_ID,dept.DEPT_ID)>0");  //AND   INV_ITEM_CLASS.ITEM_CLASS_ID =9 
            return sb.ToString();
        }

        public static string GetDeptProductionItem_Master_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("   Select INV_ITEM_MASTER.* ");
            sb.Append(" , INV_ITEM_GROUP.ITEM_GROUP_CODE, INV_ITEM_GROUP.ITEM_GROUP_NAME  ");
            sb.Append(" , INV_ITEM_CLASS.ITEM_CLASS_CODE, INV_ITEM_CLASS.ITEM_CLASS_NAME  ");
            //sb.Append(" , INV_ITEM_TYPE.ITEM_TYPE_CODE, INV_ITEM_TYPE.ITEM_TYPE_NAME  ");
            sb.Append(" , UOM_INFO.UOM_CODE ");
            sb.Append(" , UOM_INFO.UOM_NAME,GET_DEPT_WISE_CLOSING_QTY(INV_ITEM_MASTER.ITEM_ID,dept.DEPT_ID) CLOSING_QTY ");
            sb.Append(" FROM INV_ITEM_MASTER ");
            sb.Append(" INNER JOIN INV_ITEM_GROUP ON INV_ITEM_MASTER.ITEM_GROUP_ID = INV_ITEM_GROUP.ITEM_GROUP_ID ");
            sb.Append(" INNER JOIN INV_ITEM_CLASS ON INV_ITEM_MASTER.ITEM_CLASS_ID = INV_ITEM_CLASS.ITEM_CLASS_ID ");
            //sb.Append(" INNER JOIN INV_ITEM_TYPE ON INV_ITEM_MASTER.ITEM_TYPE_ID = INV_ITEM_TYPE.ITEM_TYPE_ID ");
            sb.Append(" INNER JOIN UOM_INFO ON INV_ITEM_MASTER.UOM_ID = UOM_INFO.UOM_ID ");
            sb.Append(" LEFT JOIN PRO_DEPARTMENT_ITEM dept ON INV_ITEM_MASTER.ITEM_ID = dept.ITEM_ID ");
            sb.Append(" Where 1=1 ");
            return sb.ToString();
        }

        public static string GetDeptProductionItem_Master_SQLString(int _deptid)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("   Select INV_ITEM_MASTER.* ");
            sb.Append(" , INV_ITEM_GROUP.ITEM_GROUP_CODE, INV_ITEM_GROUP.ITEM_GROUP_NAME  ");
            sb.Append(" , INV_ITEM_CLASS.ITEM_CLASS_CODE, INV_ITEM_CLASS.ITEM_CLASS_NAME  ");
            //sb.Append(" , INV_ITEM_TYPE.ITEM_TYPE_CODE, INV_ITEM_TYPE.ITEM_TYPE_NAME  ");
            sb.Append(" , UOM_INFO.UOM_CODE ");
            sb.Append(" , UOM_INFO.UOM_NAME,GET_DEPT_WISE_CLOSING_QTY(INV_ITEM_MASTER.ITEM_ID," + _deptid + ") CLOSING_QTY,GET_DEPT_WISE_RECV_CLOSING_QTY(INV_ITEM_MASTER.ITEM_ID," + _deptid + ") ASM_REC_CLS_QTY,GET_DEPT_WISE_REJ_CLOSING_QTY(INV_ITEM_MASTER.ITEM_ID," + _deptid + ") ASM_REJ_CLS_QTY ");
            sb.Append(" FROM INV_ITEM_MASTER ");
            sb.Append(" INNER JOIN INV_ITEM_GROUP ON INV_ITEM_MASTER.ITEM_GROUP_ID = INV_ITEM_GROUP.ITEM_GROUP_ID ");
            sb.Append(" INNER JOIN INV_ITEM_CLASS ON INV_ITEM_MASTER.ITEM_CLASS_ID = INV_ITEM_CLASS.ITEM_CLASS_ID ");
            //sb.Append(" INNER JOIN INV_ITEM_TYPE ON INV_ITEM_MASTER.ITEM_TYPE_ID = INV_ITEM_TYPE.ITEM_TYPE_ID ");
            sb.Append(" INNER JOIN UOM_INFO ON INV_ITEM_MASTER.UOM_ID = UOM_INFO.UOM_ID ");
            sb.Append(" LEFT JOIN PRO_DEPARTMENT_ITEM dept ON INV_ITEM_MASTER.ITEM_ID = dept.ITEM_ID ");
            sb.Append(" Where 1=1  ");
           // sb.Append(" AND INV_ITEM_MASTER.ITEM_GROUP_ID IN (338, 23 ,1095,981) ");
            return sb.ToString();
        }

        public static string GetDeptProductionItem_Master_SQLStringwithstmlid(int _deptid,int stmlid)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT * FROM (   Select INV_ITEM_MASTER.* ");
            sb.Append(" , INV_ITEM_GROUP.ITEM_GROUP_CODE, INV_ITEM_GROUP.ITEM_GROUP_NAME  ");
            sb.Append(" , INV_ITEM_CLASS.ITEM_CLASS_CODE, INV_ITEM_CLASS.ITEM_CLASS_NAME  ");
            sb.Append(" , UOM_INFO.UOM_CODE ");
            sb.Append(" ,CASE WHEN INV_ITEM_MASTER.ITEM_GROUP_ID IN (48,62) THEN GET_DEPT_REJECT_CLOSING(INV_ITEM_MASTER.ITEM_ID," + _deptid + ") ELSE GET_DEPT_WISE_CLOSING_QTY(INV_ITEM_MASTER.ITEM_ID," + _deptid + ") END CLOSING_QTY ");
            sb.Append(" , UOM_INFO.UOM_NAME,GET_DEPT_WISE_RECV_CLOSING_QTY(INV_ITEM_MASTER.ITEM_ID," + _deptid + ") ASM_REC_CLS_QTY,GET_DEPT_WISE_REJ_CLOSING_QTY(INV_ITEM_MASTER.ITEM_ID," + _deptid + ") ASM_REJ_CLS_QTY,dept.DEPT_ID,dept.IS_FINISHED,dept.IS_MIXTURE ");
            sb.Append(" FROM INV_ITEM_MASTER ");
            sb.Append(" INNER JOIN INV_ITEM_GROUP ON INV_ITEM_MASTER.ITEM_GROUP_ID = INV_ITEM_GROUP.ITEM_GROUP_ID ");
            sb.Append(" INNER JOIN INV_ITEM_CLASS ON INV_ITEM_MASTER.ITEM_CLASS_ID = INV_ITEM_CLASS.ITEM_CLASS_ID ");
            sb.Append(" INNER JOIN UOM_INFO ON INV_ITEM_MASTER.UOM_ID = UOM_INFO.UOM_ID ");
            sb.Append(" LEFT JOIN PRO_DEPARTMENT_ITEM dept ON INV_ITEM_MASTER.ITEM_ID = dept.ITEM_ID ");
            sb.Append(" Where 1=1 AND dept.DEPT_ID=" + _deptid + " AND dept.STLM_ID=" + stmlid + " ");
            sb.Append(" UNION ALL ");
            sb.Append("   Select INV_ITEM_MASTER.* ");
            sb.Append(" , INV_ITEM_GROUP.ITEM_GROUP_CODE, INV_ITEM_GROUP.ITEM_GROUP_NAME  ");
            sb.Append(" , INV_ITEM_CLASS.ITEM_CLASS_CODE, INV_ITEM_CLASS.ITEM_CLASS_NAME  ");
            sb.Append(" , UOM_INFO.UOM_CODE ");
            sb.Append(" ,CASE WHEN INV_ITEM_MASTER.ITEM_GROUP_ID IN (48,62) THEN GET_DEPT_REJECT_CLOSING(INV_ITEM_MASTER.ITEM_ID," + _deptid + ") ELSE GET_DEPT_WISE_CLOSING_QTY(INV_ITEM_MASTER.ITEM_ID," + _deptid + ") END CLOSING_QTY ");
            sb.Append(" , UOM_INFO.UOM_NAME,GET_DEPT_WISE_RECV_CLOSING_QTY(INV_ITEM_MASTER.ITEM_ID," + _deptid + ") ASM_REC_CLS_QTY,GET_DEPT_WISE_REJ_CLOSING_QTY(INV_ITEM_MASTER.ITEM_ID," + _deptid + ") ASM_REJ_CLS_QTY,dept.DEPT_ID,dept.IS_FINISHED,dept.IS_MIXTURE  ");
            sb.Append(" FROM INV_ITEM_MASTER ");
            sb.Append(" INNER JOIN INV_ITEM_GROUP ON INV_ITEM_MASTER.ITEM_GROUP_ID = INV_ITEM_GROUP.ITEM_GROUP_ID ");
            sb.Append(" INNER JOIN INV_ITEM_CLASS ON INV_ITEM_MASTER.ITEM_CLASS_ID = INV_ITEM_CLASS.ITEM_CLASS_ID ");
            sb.Append(" INNER JOIN UOM_INFO ON INV_ITEM_MASTER.UOM_ID = UOM_INFO.UOM_ID ");
            sb.Append(" LEFT JOIN PRO_DEPARTMENT_ITEM dept ON INV_ITEM_MASTER.ITEM_ID = dept.ITEM_ID ");
            sb.Append(" Where 1=1 AND dept.DEPT_ID=" + _deptid + " AND dept.STLM_ID=0 ) a Where 1=1 ");
            
            return sb.ToString();
        }


        public static string GetDeptProductionRejectItem_SQLStringwithstmlid(int _deptid, int stmlid)
        {
            StringBuilder sb = new StringBuilder();
            //sb.Append(" SELECT * FROM (   Select INV_ITEM_MASTER.* ");
            //sb.Append(" , INV_ITEM_GROUP.ITEM_GROUP_CODE, INV_ITEM_GROUP.ITEM_GROUP_NAME  ");
            //sb.Append(" , INV_ITEM_CLASS.ITEM_CLASS_CODE, INV_ITEM_CLASS.ITEM_CLASS_NAME  ");
            //sb.Append(" , UOM_INFO.UOM_CODE ");
            //sb.Append(" ,GET_DEPT_REJECT_CLOSING_QTY(INV_ITEM_MASTER.ITEM_ID," + _deptid + "," + stmlid + ") CLOSING_QTY ");
            // sb.Append(" ,UOM_INFO.UOM_NAME,dept.DEPT_ID,dept.IS_FINISHED,dept.IS_MIXTURE ");
            //sb.Append(" FROM INV_ITEM_MASTER ");
            //sb.Append(" INNER JOIN INV_ITEM_GROUP ON INV_ITEM_MASTER.ITEM_GROUP_ID = INV_ITEM_GROUP.ITEM_GROUP_ID ");
            //sb.Append(" INNER JOIN INV_ITEM_CLASS ON INV_ITEM_MASTER.ITEM_CLASS_ID = INV_ITEM_CLASS.ITEM_CLASS_ID ");
            //sb.Append(" INNER JOIN UOM_INFO ON INV_ITEM_MASTER.UOM_ID = UOM_INFO.UOM_ID ");
            //sb.Append(" LEFT JOIN PRO_DEPARTMENT_ITEM dept ON INV_ITEM_MASTER.ITEM_ID = dept.ITEM_ID ");
            //sb.Append(" Where 1=1 AND dept.DEPT_ID=" + _deptid + " AND dept.STLM_ID=" + stmlid + " ");
            //sb.Append(" UNION ALL ");
            sb.Append("   Select DISTINCT INV_ITEM_MASTER.* ");
            sb.Append(" , INV_ITEM_GROUP.ITEM_GROUP_CODE, INV_ITEM_GROUP.ITEM_GROUP_NAME  ");
            sb.Append(" , INV_ITEM_CLASS.ITEM_CLASS_CODE, INV_ITEM_CLASS.ITEM_CLASS_NAME  ");
            sb.Append(" , UOM_INFO.UOM_CODE ");
            sb.Append(" ,GET_DEPT_REJECT_CLOSING_QTY(INV_ITEM_MASTER.ITEM_ID," + _deptid + "," + stmlid + ") CLOSING_QTY ");
            sb.Append(" , UOM_INFO.UOM_NAME   ,dept.DEPT_ID,dept.IS_FINISHED,dept.IS_MIXTURE  ");
            sb.Append(" FROM INV_ITEM_MASTER ");
            sb.Append(" INNER JOIN INV_ITEM_GROUP ON INV_ITEM_MASTER.ITEM_GROUP_ID = INV_ITEM_GROUP.ITEM_GROUP_ID ");
            sb.Append(" INNER JOIN INV_ITEM_CLASS ON INV_ITEM_MASTER.ITEM_CLASS_ID = INV_ITEM_CLASS.ITEM_CLASS_ID ");
            sb.Append(" INNER JOIN UOM_INFO ON INV_ITEM_MASTER.UOM_ID = UOM_INFO.UOM_ID ");
            sb.Append(" LEFT JOIN PRO_DEPARTMENT_ITEM dept ON INV_ITEM_MASTER.ITEM_ID = dept.ITEM_ID ");
            sb.Append(" Where 1=1 AND dept.DEPT_ID=" + _deptid + " ");
            //sb.Append(" AND dept.STLM_ID=0  ");
            //sb.Append(" ) a Where 1=1 ");

            return sb.ToString();
        }

        public static string GetRejectItem_SQLStringwithStore(int _deptid, int StoreId)
        {
            StringBuilder sb = new StringBuilder();
          
            sb.Append("   Select  INV_ITEM_MASTER.* ");
            sb.Append(" , INV_ITEM_GROUP.ITEM_GROUP_CODE, INV_ITEM_GROUP.ITEM_GROUP_NAME  ");
            sb.Append(" , INV_ITEM_CLASS.ITEM_CLASS_CODE, INV_ITEM_CLASS.ITEM_CLASS_NAME  ");
            sb.Append(" , UOM_INFO.UOM_CODE ");
            sb.Append(" ,GET_STORE_REJECTION_QTY(INV_ITEM_MASTER.ITEM_ID," + _deptid + "," + StoreId + ") CLOSING_QTY ");
            sb.Append(" , UOM_INFO.UOM_NAME  ");
            sb.Append(" ,(SELECT ITEM_STOCK_DETAILS.UOM_ID FROM ITEM_STOCK_DETAILS WHERE ITEM_ID=INV_ITEM_MASTER.ITEM_ID AND ITEM_STOCK_DETAILS.INV_TRANS_TYPE_ID IN (1027) AND ROWNUM<=1) RCV_UOM_ID ");
            sb.Append(" ,(SELECT UOM_INFO.UOM_CODE FROM ITEM_STOCK_DETAILS INNER JOIN UOM_INFO ON ITEM_STOCK_DETAILS.UOM_ID=UOM_INFO.UOM_ID WHERE ITEM_ID=INV_ITEM_MASTER.ITEM_ID AND ITEM_STOCK_DETAILS.INV_TRANS_TYPE_ID IN (1027) AND ROWNUM<=1) RCV_UOM_NAME ");
            sb.Append(" FROM INV_ITEM_MASTER ");
            sb.Append(" INNER JOIN INV_ITEM_GROUP ON INV_ITEM_MASTER.ITEM_GROUP_ID = INV_ITEM_GROUP.ITEM_GROUP_ID ");
            sb.Append(" INNER JOIN INV_ITEM_CLASS ON INV_ITEM_MASTER.ITEM_CLASS_ID = INV_ITEM_CLASS.ITEM_CLASS_ID ");
            sb.Append(" INNER JOIN UOM_INFO ON INV_ITEM_MASTER.UOM_ID = UOM_INFO.UOM_ID ");
            sb.Append(" Where 1=1  ");
            sb.Append(" AND INV_ITEM_MASTER.ITEM_GROUP_ID  IN (47,48,62) ");
            return sb.ToString();
        }

        public static string GetItem_Master_Formation_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("   Select INV_ITEM_MASTER.* ");
            sb.Append(" , INV_ITEM_GROUP.ITEM_GROUP_CODE, INV_ITEM_GROUP.ITEM_GROUP_NAME  ");
            sb.Append(" , INV_ITEM_CLASS.ITEM_CLASS_CODE, INV_ITEM_CLASS.ITEM_CLASS_NAME  ");
            sb.Append(" , UOM_INFO.UOM_CODE ");
            sb.Append(" , UOM_INFO.UOM_NAME ");
            sb.Append(" ,GET_UNFORM_QTY(INV_ITEM_MASTER.ITEM_ID) CLOSING_QTY ");
            sb.Append(" FROM INV_ITEM_MASTER ");
            sb.Append(" INNER JOIN INV_ITEM_GROUP ON INV_ITEM_MASTER.ITEM_GROUP_ID = INV_ITEM_GROUP.ITEM_GROUP_ID ");
            sb.Append(" INNER JOIN INV_ITEM_CLASS ON INV_ITEM_MASTER.ITEM_CLASS_ID = INV_ITEM_CLASS.ITEM_CLASS_ID ");
            sb.Append(" INNER JOIN UOM_INFO ON INV_ITEM_MASTER.UOM_ID = UOM_INFO.UOM_ID ");
            sb.Append(" INNER JOIN PRO_DEPARTMENT_ITEM dept ON INV_ITEM_MASTER.ITEM_ID = dept.ITEM_ID ");
            sb.Append(" Where 1=1  ");
            sb.Append(" AND DEPT.STLM_ID=9 ");
            return sb.ToString();
        }
        public static string GetDeptItem_Master_For_P_Battery_Entry_SQLString(int _deptid)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT * FROM( ");
            sb.Append("   Select INV_ITEM_MASTER.* ");
            sb.Append(" , INV_ITEM_GROUP.ITEM_GROUP_CODE, INV_ITEM_GROUP.ITEM_GROUP_NAME  ");
            sb.Append(" , INV_ITEM_CLASS.ITEM_CLASS_CODE, INV_ITEM_CLASS.ITEM_CLASS_NAME  ");
            sb.Append(" , UOM_INFO.UOM_CODE ,DEPT.DEPT_ID,DEPT.IS_FINISHED ");
            sb.Append(" , UOM_INFO.UOM_NAME,GET_DEPT_CLOSING_QTY(INV_ITEM_MASTER.ITEM_ID," + _deptid + ",1) CLOSING_QTY  ");
            sb.Append(" FROM INV_ITEM_MASTER ");
            sb.Append(" INNER JOIN INV_ITEM_GROUP ON INV_ITEM_MASTER.ITEM_GROUP_ID = INV_ITEM_GROUP.ITEM_GROUP_ID ");
            sb.Append(" INNER JOIN INV_ITEM_CLASS ON INV_ITEM_MASTER.ITEM_CLASS_ID = INV_ITEM_CLASS.ITEM_CLASS_ID ");
            sb.Append(" INNER JOIN UOM_INFO ON INV_ITEM_MASTER.UOM_ID = UOM_INFO.UOM_ID ");
            sb.Append(" INNER JOIN PRO_DEPARTMENT_ITEM dept ON INV_ITEM_MASTER.ITEM_ID = dept.ITEM_ID ");
            sb.Append(" ) a  ");
            sb.Append(" Where 1=1  ");          
            return sb.ToString();
        }

        public static string GetDeptWiseItem_Master_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Select INV_ITEM_MASTER.* ");
            sb.Append(" , INV_ITEM_GROUP.ITEM_GROUP_CODE, INV_ITEM_GROUP.ITEM_GROUP_NAME  ");
            sb.Append(" , INV_ITEM_CLASS.ITEM_CLASS_CODE, INV_ITEM_CLASS.ITEM_CLASS_NAME  ");
            sb.Append(" , INV_ITEM_TYPE.ITEM_TYPE_CODE, INV_ITEM_TYPE.ITEM_TYPE_NAME  ");
          
            sb.Append(" , UOM_INFO.UOM_CODE, UOM_INFO.UOM_NAME,GET_CLOSING_QTY(INV_ITEM_MASTER.ITEM_ID) CLOSING_QTY,(Select case WHEN Count(ITEM_ID)>0 then 1 else 0 end IsAssign from DEPARTMENT_ITEM where ITEM_ID=INV_ITEM_MASTER.ITEM_ID and DEPT_ID=@deptId) as IsAssigned ");
            sb.Append(" FROM INV_ITEM_MASTER ");
            sb.Append(" INNER JOIN INV_ITEM_GROUP ON INV_ITEM_MASTER.ITEM_GROUP_ID = INV_ITEM_GROUP.ITEM_GROUP_ID ");
            sb.Append(" INNER JOIN INV_ITEM_CLASS ON INV_ITEM_MASTER.ITEM_CLASS_ID = INV_ITEM_CLASS.ITEM_CLASS_ID ");
            sb.Append(" INNER JOIN INV_ITEM_TYPE ON INV_ITEM_MASTER.ITEM_TYPE_ID = INV_ITEM_TYPE.ITEM_TYPE_ID ");
            sb.Append(" INNER JOIN UOM_INFO ON INV_ITEM_MASTER.UOM_ID = UOM_INFO.UOM_ID ");
            sb.Append(" Where 1=1 "); ;
            return sb.ToString();
        }



        public static string Item_From_Purchase_Indent_Service_SqlSrting()
        {

            StringBuilder sb = new StringBuilder();
            sb.Append(" Select INV_ITEM_MASTER.* ");
            sb.Append(" , INV_ITEM_GROUP.ITEM_GROUP_CODE, INV_ITEM_GROUP.ITEM_GROUP_NAME  ");
            sb.Append(" , INV_ITEM_CLASS.ITEM_CLASS_CODE, INV_ITEM_CLASS.ITEM_CLASS_NAME  ");
            sb.Append(" , INV_ITEM_TYPE.ITEM_TYPE_CODE, INV_ITEM_TYPE.ITEM_TYPE_NAME  ");
            sb.Append(" , UOM_INFO.UOM_CODE, UOM_INFO.UOM_NAME,GET_CLOSING_QTY(INV_ITEM_MASTER.ITEM_ID) CLOSING_QTY , SNS.ITEM_SNS_NAME ");
            sb.Append(" ,indtDtl.INDT_DET_ID,indtDtl.INDT_QTY,indtDtl.INDT_QTY_APPROVED,indtDtl.INDT_DET_ID ");
            sb.Append(" ,indtMst.INDT_NO,indtMst.INDT_DATE,dept.DEPARTMENT_NAME FROM_DEPT_NAME ");
            sb.Append(" ,(SELECT NVL(SUM(purDtl.PURCHASE_QTY),0)  FROM LP_PURCHASE_DETAILS purDtl   WHERE purDtl.INDT_DET_ID =indtDtl.INDT_DET_ID ) AS ALREADRY_PURCHASE_QTY   ");
            sb.Append(" FROM LP_INDT_DETAILS indtDtl ");
            sb.Append(" INNER JOIN LP_INDT_MASTER indtMst ON indtDtl.INDT_ID=indtMst.INDT_ID ");
            sb.Append(" INNER JOIN DEPARTMENT_INFO dept on indtMst.DEPT_ID=dept. DEPARTMENT_ID ");
            sb.Append(" INNER JOIN INV_ITEM_MASTER  ON indtDtl.ITEM_ID=INV_ITEM_MASTER.ITEM_ID ");
            sb.Append(" INNER JOIN INV_ITEM_GROUP ON INV_ITEM_MASTER.ITEM_GROUP_ID = INV_ITEM_GROUP.ITEM_GROUP_ID ");
            sb.Append(" INNER JOIN INV_ITEM_CLASS ON INV_ITEM_MASTER.ITEM_CLASS_ID = INV_ITEM_CLASS.ITEM_CLASS_ID ");
            sb.Append(" INNER JOIN INV_ITEM_TYPE ON INV_ITEM_MASTER.ITEM_TYPE_ID = INV_ITEM_TYPE.ITEM_TYPE_ID ");
            sb.Append(" INNER JOIN UOM_INFO ON INV_ITEM_MASTER.UOM_ID = UOM_INFO.UOM_ID ");
            sb.Append(" LEFT JOIN  INV_ITEM_SNS_MST SNS ON  INV_ITEM_MASTER.ITEM_SNS_ID=SNS.ITEM_SNS_ID ");
            sb.Append(" Where 1=1 ");
            sb.Append(" AND indtMst.IS_CLOSED='N' ");
            return sb.ToString();

        }


        public static string ItemList_From_Purchase_Indent_Service_SqlSrting()
        {

            StringBuilder sb = new StringBuilder();
            sb.Append(" Select INV_ITEM_MASTER.ITEM_ID ");
            sb.Append(" , INV_ITEM_MASTER.ITEM_CODE ");
            sb.Append(" , INV_ITEM_MASTER.ITEM_NAME ");
            sb.Append(" , INV_ITEM_MASTER.ITEM_GROUP_ID ");
            sb.Append(" , INV_ITEM_GROUP.ITEM_GROUP_CODE  ");
            sb.Append(" , INV_ITEM_GROUP.ITEM_GROUP_NAME  ");
            sb.Append(" , INV_ITEM_MASTER.UOM_ID ");
            sb.Append(" , UOM_INFO.UOM_CODE  ");
            sb.Append(" , UOM_INFO.UOM_NAME  ");
            sb.Append(" , INV_ITEM_MASTER.SAFTY_STOCK_LEVEL ");
            sb.Append(" , INV_ITEM_MASTER.RE_ORDER_LEVEL ");
            sb.Append(" , INV_ITEM_CLASS.ITEM_CLASS_CODE  ");
            sb.Append(" , INV_ITEM_CLASS.ITEM_CLASS_NAME  ");
            sb.Append(" , INV_ITEM_TYPE.ITEM_TYPE_CODE  ");
            sb.Append(" , INV_ITEM_TYPE.ITEM_TYPE_NAME  ");
            sb.Append(" , GET_CLOSING_QTY(INV_ITEM_MASTER.ITEM_ID) CLOSING_QTY   ");
            sb.Append(" , INV_ITEM_MASTER.IS_PRIME ");
            sb.Append(" , SNS.ITEM_SNS_NAME ");
            sb.Append(" ,indtDtl.INDT_DET_ID  ");
            sb.Append(" ,indtDtl.INDT_QTY  ");
            sb.Append(" ,indtDtl.INDT_QTY_APPROVED  ");
            sb.Append(" ,indtDtl.INDT_DET_ID ");
            sb.Append(" ,indtMst.INDT_NO  ");
            sb.Append(" ,indtMst.INDT_DATE  ");
            sb.Append(" ,dept.DEPARTMENT_NAME FROM_DEPT_NAME ");
            sb.Append(" ,(SELECT NVL(SUM(purDtl.PURCHASE_QTY),0)  FROM LP_PURCHASE_DETAILS purDtl   WHERE purDtl.INDT_DET_ID =indtDtl.INDT_DET_ID ) AS ALREADRY_PURCHASE_QTY   ");
            sb.Append(" FROM LP_INDT_DETAILS indtDtl ");
            sb.Append(" INNER JOIN LP_INDT_MASTER indtMst ON indtDtl.INDT_ID=indtMst.INDT_ID ");
            sb.Append(" INNER JOIN DEPARTMENT_INFO dept on indtMst.DEPT_ID=dept. DEPARTMENT_ID ");
            sb.Append(" INNER JOIN INV_ITEM_MASTER  ON indtDtl.ITEM_ID=INV_ITEM_MASTER.ITEM_ID ");
            sb.Append(" INNER JOIN INV_ITEM_GROUP ON INV_ITEM_MASTER.ITEM_GROUP_ID = INV_ITEM_GROUP.ITEM_GROUP_ID ");
            sb.Append(" INNER JOIN INV_ITEM_CLASS ON INV_ITEM_MASTER.ITEM_CLASS_ID = INV_ITEM_CLASS.ITEM_CLASS_ID ");
            sb.Append(" INNER JOIN INV_ITEM_TYPE ON indtDtl.ITEM_TYPE_ID = INV_ITEM_TYPE.ITEM_TYPE_ID ");
            //sb.Append(" INNER JOIN INV_ITEM_TYPE ON INV_ITEM_MASTER.ITEM_TYPE_ID = INV_ITEM_TYPE.ITEM_TYPE_ID ");
            sb.Append(" INNER JOIN UOM_INFO ON INV_ITEM_MASTER.UOM_ID = UOM_INFO.UOM_ID ");
            sb.Append(" LEFT JOIN  INV_ITEM_SNS_MST SNS ON  INV_ITEM_MASTER.ITEM_SNS_ID=SNS.ITEM_SNS_ID ");
            sb.Append(" Where 1=1 ");
            sb.Append(" AND indtMst.IS_CLOSED='N' ");
            return sb.ToString();

        }



        public static List<dcINV_ITEM_MASTER> GetDepartmentItemList(int dept_id, string is_finished, int pitemid, DBContext dc)
        {
            List<dcINV_ITEM_MASTER> cObjList = new List<dcINV_ITEM_MASTER>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                DBCommandInfo cmdInfo = new DBCommandInfo();

                StringBuilder sb = new StringBuilder();
                sb.Append(GetDeptProductionItem_Master_SQLString(dept_id));

                if (pitemid > 0)
                {
                    sb.Append(" AND INV_ITEM_MASTER.ITEM_ID =@pitemid ");
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

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                cObjList = INV_ITEM_MASTERBL.GetItemList(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }






        public static string Item_From_Invoice_Dtl_For_Repair_Item_Receive_SqlSrting()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" Select ");
            sb.Append(" im.ITEM_ID,im.ITEM_NAME,im.ITEM_DESC,im.ITEM_CODE ");
            sb.Append(" ,ig.ITEM_GROUP_ID, ig.ITEM_GROUP_CODE, ig.ITEM_GROUP_NAME  ");
            sb.Append(" ,ic.ITEM_CLASS_CODE, ic.ITEM_CLASS_NAME  ");
            sb.Append(" ,it.ITEM_TYPE_CODE, it.ITEM_TYPE_NAME  ");
            sb.Append(" ,um.UOM_CODE, um.UOM_NAME,GET_CLOSING_QTY(im.ITEM_ID) CLOSING_QTY , SNS.ITEM_SNS_NAME ");
            sb.Append(" ,invDtl.INVOICE_DET_ID,invDtl.ITEM_QNTY INDT_QTY,invDtl.ITEM_QNTY_APRROVED  ");
            sb.Append(" ,invMst.INVOICE_NO INDT_NO ");            
            sb.Append(" FROM INVOICE_DETAILS invDtl ");
            sb.Append(" INNER JOIN INVOICE_MASTER invMst ON invDtl.INVOICE_ID=invMst.INVOICE_ID ");
            sb.Append(" INNER JOIN INV_ITEM_MASTER im  ON invDtl.ITEM_ID=im.ITEM_ID ");
            sb.Append(" INNER JOIN INV_ITEM_GROUP ig ON im.ITEM_GROUP_ID = ig.ITEM_GROUP_ID ");
            sb.Append(" INNER JOIN INV_ITEM_CLASS ic ON im.ITEM_CLASS_ID = ic.ITEM_CLASS_ID ");
            sb.Append(" INNER JOIN INV_ITEM_TYPE it ON im.ITEM_TYPE_ID = it.ITEM_TYPE_ID ");
            sb.Append(" INNER JOIN UOM_INFO um ON im.UOM_ID = um.UOM_ID ");
            sb.Append(" LEFT JOIN  INV_ITEM_SNS_MST SNS ON  im.ITEM_SNS_ID=SNS.ITEM_SNS_ID ");
            sb.Append(" Where 1=1 ");
            sb.Append(" AND (SELECT invDtl.ITEM_QNTY- NVL(SUM(purDtl.PURCHASE_QTY),0)  FROM LP_PURCHASE_DETAILS purDtl   WHERE purDtl.INVOICE_DET_ID =invDtl.INVOICE_DET_ID)>0 ");
            return sb.ToString();
        }



        public static List<dcINV_ITEM_MASTER> GetItemList()
        {
            return GetItemList(null);
        }

        public static List<dcINV_ITEM_MASTER> GetItemList(DBContext dc)
        {

            List<dcINV_ITEM_MASTER> cObjList = new List<dcINV_ITEM_MASTER>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetItem_Master_SQLString());

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcINV_ITEM_MASTER>(dbq, dc).ToList();


            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;


            return GetItemList(null, dc);
        }


        public static List<dcINV_ITEM_MASTER> GetItemListByQuery(string query)
        {
            return GetItemListByQuery(query, null);
        }

        public static List<dcINV_ITEM_MASTER> GetItemListByQuery(string query, DBContext dc)
        {

            List<dcINV_ITEM_MASTER> cObjList = new List<dcINV_ITEM_MASTER>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText =query;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcINV_ITEM_MASTER>(dbq, dc).ToList();


            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcINV_ITEM_MASTER> GetItemList(int pGroupID, DBContext dc)
        {
            List<dcINV_ITEM_MASTER> cObjList = new List<dcINV_ITEM_MASTER>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder(GetItem_Master_SQLString());

                    if (pGroupID > 0)
                    {
                        sb.Append(" and INV_ITEM_MASTER.ITEM_GROUP_ID=@GroupId ");
                        cmdInfo.DBParametersInfo.Add("@GroupId", pGroupID);
                    }
                   // sb.Append(" order by INV_ITEM_MASTER ITEM_DESC asc ");

                    DBQuery dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcINV_ITEM_MASTER>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }


        public static List<dcINV_ITEM_MASTER> GetItemList(int pGroupID,int classId,int itemId, string itemName,  DBContext dc)
        {
            List<dcINV_ITEM_MASTER> cObjList = new List<dcINV_ITEM_MASTER>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder(GetItem_Master_SQLString());

                    if (pGroupID > 0)
                    {
                        sb.Append(" and INV_ITEM_MASTER.ITEM_GROUP_ID=@GroupId ");
                        cmdInfo.DBParametersInfo.Add("@GroupId", pGroupID);
                    }
                    if (classId > 0)
                    {
                        sb.Append(" and INV_ITEM_MASTER.ITEM_CLASS_ID=@classId ");
                        cmdInfo.DBParametersInfo.Add("@classId", classId);
                    }

                    if (itemId > 0)
                    {
                        sb.Append(" and INV_ITEM_MASTER.ITEM_ID=@itemId ");
                        cmdInfo.DBParametersInfo.Add("@itemId", itemId);
                    }


                    if (itemName != string.Empty)
                    {
                        sb.Append(" AND UPPER(INV_ITEM_MASTER.ITEM_NAME) LIKE UPPER(:itemName) ");
                        //cmd.Parameters.AddWithValue("@glGroupID", pGLGroupID);
                        cmdInfo.DBParametersInfo.Add(":itemName", '%' + itemName + '%');
                    }

                    sb.Append(" ORDER BY INV_ITEM_MASTER.ITEM_NAME ");

                    DBQuery dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcINV_ITEM_MASTER>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }



        public static List<dcINV_ITEM_MASTER> GetItemList(int pGroupID, int classId, int itemId, string itemName, int itemsnsid, DBContext dc)
        {
            List<dcINV_ITEM_MASTER> cObjList = new List<dcINV_ITEM_MASTER>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder(GetItem_Master_SQLString());

                    if (pGroupID > 0)
                    {
                        sb.Append(" and INV_ITEM_MASTER.ITEM_GROUP_ID=@GroupId ");
                        cmdInfo.DBParametersInfo.Add("@GroupId", pGroupID);
                    }
                    if (classId > 0)
                    {
                        sb.Append(" and INV_ITEM_MASTER.ITEM_CLASS_ID=@classId ");
                        cmdInfo.DBParametersInfo.Add("@classId", classId);
                    }

                    if (itemId > 0)
                    {
                        sb.Append(" and INV_ITEM_MASTER.ITEM_ID=@itemId ");
                        cmdInfo.DBParametersInfo.Add("@itemId", itemId);
                    }


                    if (itemName != string.Empty)
                    {
                        sb.Append(" AND UPPER(INV_ITEM_MASTER.ITEM_NAME) LIKE UPPER(:itemName) ");
                        //cmd.Parameters.AddWithValue("@glGroupID", pGLGroupID);
                        cmdInfo.DBParametersInfo.Add(":itemName", '%' + itemName + '%');
                    }


                    if (itemsnsid >0)
                    {
                        sb.Append(" AND INV_ITEM_MASTER.ITEM_SNS_ID=@itemsnsId ");
                        cmdInfo.DBParametersInfo.Add("@itemsnsId", itemsnsid);
                    }
                    sb.Append(" ORDER BY INV_ITEM_MASTER.ITEM_CODE DESC ");

                    DBQuery dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcINV_ITEM_MASTER>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcINV_ITEM_MASTER> GetItemList1(clsPrmInventory prm, DBContext dc)
        {
            List<dcINV_ITEM_MASTER> cObjList = new List<dcINV_ITEM_MASTER>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder(GetItem_Master_SQLString());

                    if (prm.ItemTypeId > 0)
                    {
                        sb.Append(" and INV_ITEM_MASTER.ITEM_TYPE_ID=@ItemTypeId ");
                        cmdInfo.DBParametersInfo.Add("@ItemTypeId", prm.ItemTypeId);
                    }
                    if (prm.ItemClassId > 0)
                    {
                        sb.Append(" and INV_ITEM_MASTER.ITEM_CLASS_ID=@ItemClassId ");
                        cmdInfo.DBParametersInfo.Add("@ItemClassId", prm.ItemClassId);
                    }
                    if (prm.ItemGroupId > 0)
                    {
                        sb.Append(" and INV_ITEM_MASTER.ITEM_GROUP_ID=@ItemGroupId ");
                        cmdInfo.DBParametersInfo.Add("@ItemGroupId", prm.ItemGroupId);
                    }
                    if (prm.SNS_Type > 0)
                    {
                        sb.Append(" and INV_ITEM_MASTER.ITEM_SNS_ID=@itemSNSID ");
                        cmdInfo.DBParametersInfo.Add("@itemSNSID", prm.SNS_Type);
                    }

                    sb.Append(" ORDER BY INV_ITEM_MASTER.ITEM_CODE ");
                    DBQuery dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcINV_ITEM_MASTER>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcINV_ITEM_MASTER> GetItemList(DBQuery dbq, DBContext dc)
        {            
            List<dcINV_ITEM_MASTER> cObjList = new List<dcINV_ITEM_MASTER>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    if (dbq == null)
                    {
                        //DBCommandInfo cmdInfo = new DBCommandInfo();
                        //StringBuilder sb = new StringBuilder(GetItem_Master_SQLString());                  
                        dbq = new DBQuery();
                        //dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                        //cmdInfo.CommandText = sb.ToString();
                        //cmdInfo.CommandType = CommandType.Text;
                        //dbq.DBCommandInfo = cmdInfo;                       
                    }
                    cObjList = DBQuery.ExecuteDBQuery<dcINV_ITEM_MASTER>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcINV_ITEM_MASTER> GetIndentItemList(DBQuery dbq, DBContext dc)
        {
            List<dcINV_ITEM_MASTER> cObjList = new List<dcINV_ITEM_MASTER>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    if (dbq == null)
                    {
                        DBCommandInfo cmdInfo = new DBCommandInfo();
                        StringBuilder sb = new StringBuilder(Item_From_Purchase_Indent_Service_SqlSrting());
                        dbq = new DBQuery();
                        dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                        cmdInfo.CommandText = sb.ToString();
                        cmdInfo.CommandType = CommandType.Text;
                        dbq.DBCommandInfo = cmdInfo;

                    }
                    dbq.IsPaging = false;
                    cObjList = DBQuery.ExecuteDBQuery<dcINV_ITEM_MASTER>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcINV_ITEM_MASTER> GetRepairItemList(DBQuery dbq, DBContext dc)
        {
            List<dcINV_ITEM_MASTER> cObjList = new List<dcINV_ITEM_MASTER>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    if (dbq == null)
                    {
                        DBCommandInfo cmdInfo = new DBCommandInfo();
                        StringBuilder sb = new StringBuilder(Item_From_Invoice_Dtl_For_Repair_Item_Receive_SqlSrting());
                        sb.Append(" and invMst.GP_TYPE_ID=2 ");
                        dbq = new DBQuery();
                        dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                        cmdInfo.CommandText = sb.ToString();
                        cmdInfo.CommandType = CommandType.Text;
                        dbq.DBCommandInfo = cmdInfo;
                    }
                    dbq.IsPaging = false;
                    cObjList = DBQuery.ExecuteDBQuery<dcINV_ITEM_MASTER>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcINV_ITEM_DTL> Inv_ItemDtl_List()
        {
            return Inv_ItemDtl_List(null, null, null, null);
        }
        public static List<dcINV_ITEM_DTL> Inv_ItemDtl_List(string storeId)
        {
            return Inv_ItemDtl_List(null, null, storeId, null);
        }
        public static List<dcINV_ITEM_DTL> Inv_ItemDtl_List(DBContext dc)
        {
            return Inv_ItemDtl_List(null, dc, null, null);
        }

        public static List<dcINV_ITEM_DTL> Inv_ItemDtl_List(DBQuery dbq, DBContext dc, string storeId, string groupId)
        {
            List<dcINV_ITEM_DTL> cObjList = new List<dcINV_ITEM_DTL>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder(GetItem_Master_SQLString());

                    if (!String.IsNullOrEmpty(storeId))
                    {
                        sb.Append("and STORE_ID=@StoreId");
                        cmdInfo.DBParametersInfo.Add("@StoreId", storeId);
                    }

                    if (!String.IsNullOrEmpty(groupId))
                    {
                        sb.Append("and ITEM_GROUP_ID=@GroupId");
                        cmdInfo.DBParametersInfo.Add("@GroupId", groupId);
                    }
                    sb.Append(" order by ITEM_DESC asc");

                    dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcINV_ITEM_DTL>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcINV_ITEM_MASTER GetItemByID(int pItemID)
        {
            return GetItemByID(pItemID, null);
        }
        public static dcINV_ITEM_MASTER GetItemByID(int pItemID, DBContext dc)
        {
            dcINV_ITEM_MASTER cObjList = new dcINV_ITEM_MASTER();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetItem_Master_SQLString());

                //if (pItemID > 0)
                {
                    sb.Append(" and INV_ITEM_MASTER.ITEM_ID=@ITEM_ID ");
                    cmdInfo.DBParametersInfo.Add("@ITEM_ID", pItemID);               
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                cObjList = DBQuery.ExecuteDBQuery<dcINV_ITEM_MASTER>(dbq, dc).ToList().FirstOrDefault();

            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static dcINV_ITEM_MASTER GetItemByStoreID(int pItemID,int pItemTypeID,int pStoreID, DBContext dc)
        {
            dcINV_ITEM_MASTER cObjList = new dcINV_ITEM_MASTER();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetItem_Master_ByType_Store_SQLString(pItemID,pItemTypeID,pStoreID));

                //if (pItemID > 0)
                {
                    sb.Append(" and INV_ITEM_MASTER.ITEM_ID=@ITEM_ID ");
                    cmdInfo.DBParametersInfo.Add("@ITEM_ID", pItemID);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                cObjList = DBQuery.ExecuteDBQuery<dcINV_ITEM_MASTER>(dbq, dc).ToList().FirstOrDefault();

            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static dcINV_ITEM_MASTER GetItemByCode(string pItemCode)
        {
            return GetItemByCode(pItemCode, null);
        }
        public static dcINV_ITEM_MASTER GetItemByCode(string pItemCode, DBContext dc)
        {
            dcINV_ITEM_MASTER cObjList = new dcINV_ITEM_MASTER();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetItem_Master_SQLString());

                //if (pItemID > 0)
                {
                    sb.Append(" and INV_ITEM_MASTER.ITEM_CODE=@ITEM_CODE ");
                    cmdInfo.DBParametersInfo.Add("@ITEM_CODE", pItemCode.Trim());
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                cObjList = DBQuery.ExecuteDBQuery<dcINV_ITEM_MASTER>(dbq, dc).ToList().FirstOrDefault();

            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static int GetChildItemCountByItemGroupID(int pItemGroupID)
        {
            return GetChildItemCountByItemGroupID(pItemGroupID, null);
        }
        public static int GetChildItemCountByItemGroupID(int pItemGroupID, DBContext dc)
        {
            DBCommandInfo cmdInfo = new DBCommandInfo();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append("SELECT COUNT(*) TOT_REC ");
            sb.Append(" FROM INV_ITEM_MASTER ");
            sb.Append(" WHERE 1=1 ");

            //if (pItemGroupID > 0)
            {
                sb.Append(" AND INV_ITEM_MASTER.ITEM_GROUP_ID=@ItemGroupID ");
                cmdInfo.DBParametersInfo.Add("@ItemGroupID", pItemGroupID);
            }

            DBQuery dbq = new DBQuery();
            dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
            cmdInfo.CommandText = sb.ToString();
            cmdInfo.CommandType = CommandType.Text;
            dbq.DBCommandInfo = cmdInfo;


            int cnt = Convert.ToInt32(DBQuery.ExecuteDBScalar(dbq, dc));

            return cnt;
        }


        public static bool IsItemCodeExists(string pItemCode)
        {
            return IsItemCodeExists(pItemCode, null);
        }
        public static bool IsItemCodeExists(string pItemCode, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                StringBuilder sb = new StringBuilder(GetItem_Master_SQLString());

                sb.Append(" AND UPPER(INV_ITEM_MASTER.ITEM_CODE)=@itemCode ");
                //cmd.Parameters.AddWithValue("@gLGroupCode", pGLGroupCode);
                cmdInfo.DBParametersInfo.Add("@itemCode", pItemCode);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                isData = GetItemList(dbq, dc).Count > 0;

            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return isData;
        }
        public static bool IsItemCodeExists(string pItemCode, int pItemID)
        {
            return IsItemCodeExists(pItemCode, pItemID, null);
        }
        public static bool IsItemCodeExists(string pItemCode, int pItemID, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                StringBuilder sb = new StringBuilder(GetItem_Master_SQLString());

                sb.Append(" AND UPPER(INV_ITEM_MASTER.ITEM_CODE)=UPPER(@itemCode) ");
                //cmd.Parameters.AddWithValue("@gLGroupName", pGLGroupName);
                cmdInfo.DBParametersInfo.Add("@itemCode", pItemCode);


                sb.Append(" AND INV_ITEM_MASTER.ITEM_ID <> @itemID ");
                //cmd.Parameters.AddWithValue("@gLGroupID", pGLGroupID);
                cmdInfo.DBParametersInfo.Add("@itemID", pItemID);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                isData = GetItemList(dbq, dc).Count > 0;
            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return isData;
        }


        public static bool IsItemNameExists(string pItemName)
        {
            return IsItemNameExists(pItemName, null);
        }
        public static bool IsItemNameExists(string pItemName, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                StringBuilder sb = new StringBuilder(GetItem_Master_SQLString());

                sb.Append(" AND UPPER(INV_ITEM_MASTER.ITEM_NAME)=UPPER(@itemName) ");
                cmdInfo.DBParametersInfo.Add("@itemName", pItemName);



                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                isData = GetItemList(dbq, dc).Count > 0;

            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return isData;
        }
        public static bool IsItemNameExists(string pItemName, int pItemID)
        {
            return IsItemNameExists(pItemName, pItemID, null);
        }
        public static bool IsItemNameExists(string pItemName, int pItemID, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                StringBuilder sb = new StringBuilder(GetItem_Master_SQLString());

                sb.Append(" AND UPPER(INV_ITEM_MASTER.ITEM_NAME)=UPPER(@itemName) ");
                cmdInfo.DBParametersInfo.Add("@itemName", pItemName);


                sb.Append(" AND INV_ITEM_MASTER.ITEM_ID <> @itemID ");
                //cmd.Parameters.AddWithValue("@gLGroupID", pGLGroupID);
                cmdInfo.DBParametersInfo.Add("@itemID", pItemID);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                //cmd.CommandType = CommandType.Text;
                //cmd.CommandText = sb.ToString();

                //PG.Core.DBBase.DBQuery dbq = new PG.Core.DBBase.DBQuery();
                //dbq.DBQueryMode = PG.Core.DBBase.DBQueryModeEnum.DBCommand;
                //dbq.DBCommand = cmd;
                isData = GetItemList(dbq, dc).Count > 0;
            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return isData;
        }



        public static int Insert(dcINV_ITEM_MASTER cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcINV_ITEM_MASTER cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcINV_ITEM_MASTER>(cObj, true);
                if (id > 0) { cObj.ITEM_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcINV_ITEM_MASTER cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcINV_ITEM_MASTER cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcINV_ITEM_MASTER>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pINV_ITEM_MASTERID)
        {
            return Delete(pINV_ITEM_MASTERID, null);
        }
        public static bool Delete(int pINV_ITEM_MASTERID, DBContext dc)
        {
            dcINV_ITEM_MASTER cObj = new dcINV_ITEM_MASTER();
            //cObj.INV_ITEM_MASTERID = pINV_ITEM_MASTERID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcINV_ITEM_MASTER>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcINV_ITEM_MASTER cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcINV_ITEM_MASTER cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcINV_ITEM_MASTER cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcINV_ITEM_MASTER cObj, DBContext dc)
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
                                newID = cObj.ITEM_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.ITEM_ID, dc))
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

        public static bool SaveList(List<dcINV_ITEM_MASTER> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcINV_ITEM_MASTER> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcINV_ITEM_MASTER oDet in detList)
            {
                switch (oDet._RecordState)
                {
                    /*case Interwave.Core.DBClass.RecordStateEnum.Added:
                        int a = Insert(oDet, dc);
                        break;
                    case Interwave.Core.DBClass.RecordStateEnum.Edited:
                        bool e = Update(oDet, dc);
                        break;
                    case Interwave.Core.DBClass.RecordStateEnum.Deleted:
                        bool d = Delete(oDet.INV_ITEM_MASTERID, dc);
                        break;*/
                    default:
                        break;
                }
            }
            dc.CommitTransaction(isTransInit);
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            bStatus = true;
            return bStatus;
        }


        public static List<dcINV_ITEM_MASTER> GetItemListByGroupANDTypeWise(string type, string group_id)
        {
            return GetItemListByGroupANDTypeWise(type, group_id, null);
        }
        public static List<dcINV_ITEM_MASTER> GetItemListByGroupANDTypeWise(string Type, string group_id, DBContext dc)
        {
            List<dcINV_ITEM_MASTER> cObjList = new List<dcINV_ITEM_MASTER>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder("Select a.CAT_ID,b.CATEGORY_DESC,a.CAT_SUB_ID,c.CAT_SUB_DESC,a.ITEM_CODE,a.ITEM_DESC|| ' -' ||a.ITEM_REMARKS ITEM_DESC,a.ITEM_ID,NVL((Select SUM(NVL(OPENING_QNTY,0)+NVL(PURCHASE_QNTY,0)+NVL(RETURN_QNTY,0)+NVL(EXCESS_QNTY,0))-SUM(NVL(ISSUE_QNTY,0)) T_Stock  FROM ITEM_STOCK_DETAIL Where ITEM_CODE=a.ITEM_CODE AND CAT_ID=a.CAT_ID AND CAT_SUB_ID=a.CAT_SUB_ID),0) T_STOCK,d.MSR_NAME,e.ITEM_GROUP_DESC FROM INV_ITEM_DTL a INNER JOIN INV_ITEM_CATEGORY b ON a.CAT_ID=b.CAT_ID INNER JOIN INV_ITEM_SUB_CATEGORY c ON a.CAT_SUB_ID=c.CAT_SUB_ID and a.CAT_ID=c.CAT_ID INNER JOIN INV_MSR_UNIT d ON a.MSR_ID=d.MSR_ID LEFT JOIN INV_ITEM_GROUP e ON a.ITEM_GROUP_ID=e.ITEM_GROUP_ID Where a.IS_VISIBLE='Y'  ");
                    if (!String.IsNullOrEmpty(Type))
                    {
                        sb.Append("AND a.ITEM_TYPE=@Type");
                        cmdInfo.DBParametersInfo.Add("@Type", Type);
                    }
                    DBQuery dbq = new DBQuery();
                    //dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcINV_ITEM_MASTER>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }


        public static bool UpdateITEMVisiblebyITEMID(Int32 ITEM_ID, string userName)
        {
            return UpdateITEMVisiblebyITEMID(ITEM_ID, userName, null);
        }

        public static bool UpdateITEMVisiblebyITEMID(Int32 ITEM_ID, string userName, DBContext dc)
        {
            bool isDCInit = false;
            try
            {
                if (ITEM_ID != 0) //only GC No Update
                {
                    isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                    dcINV_ITEM_MASTER obj = new dcINV_ITEM_MASTER();
                    // obj.SL_NO = pSLNO;
                    //obj.GC_NO = pGCNO;

                    DBCommandInfo cmdInfo = new DBCommandInfo();

                    StringBuilder sb = new StringBuilder();
                    sb.Append(" Update INV_ITEM_DTL set IS_VISIBLE='N',IS_VISIBLE_DATE=SYSDATE,IS_VISIBLE_BY=" + "'" + userName + "'" + " Where ITEM_ID=@ITEM_ID ");
                    cmdInfo.DBParametersInfo.Add("@ITEM_ID", ITEM_ID);
                    //cmdInfo.DBParametersInfo.Add("@userName", userName);

                    DBQuery dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;

                    DBQuery.ExecuteDBQuery<dcINV_ITEM_DTL>(dbq, dc);
                    dc.CommitTransaction();
                }

            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return isDCInit;

        }

        public static List<dcINV_ITEM_MASTER> GetRepairBatteryItemList(string pisRepair, DBContext dc)
        {
            List<dcINV_ITEM_MASTER> cObjList = new List<dcINV_ITEM_MASTER>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder();

                    sb.Append("Select INV_ITEM_MASTER.* ");
                    sb.Append(" , INV_ITEM_GROUP.ITEM_GROUP_CODE, INV_ITEM_GROUP.ITEM_GROUP_NAME  ");
                    sb.Append(" , INV_ITEM_CLASS.ITEM_CLASS_CODE, INV_ITEM_CLASS.ITEM_CLASS_NAME  ");
                    sb.Append(" , INV_ITEM_TYPE.ITEM_TYPE_CODE, INV_ITEM_TYPE.ITEM_TYPE_NAME  ");
                    sb.Append(" , UOM_INFO.UOM_CODE, UOM_INFO.UOM_NAME,GET_CLOSING_QTY_RP_BTY(INV_ITEM_MASTER.ITEM_ID,@isRepair) CLOSING_QTY ");
                    sb.Append(" FROM INV_ITEM_MASTER ");
                    sb.Append(" INNER JOIN INV_ITEM_GROUP ON INV_ITEM_MASTER.ITEM_GROUP_ID = INV_ITEM_GROUP.ITEM_GROUP_ID ");
                    sb.Append(" INNER JOIN INV_ITEM_CLASS ON INV_ITEM_MASTER.ITEM_CLASS_ID = INV_ITEM_CLASS.ITEM_CLASS_ID ");
                    sb.Append(" INNER JOIN INV_ITEM_TYPE ON INV_ITEM_MASTER.ITEM_TYPE_ID = INV_ITEM_TYPE.ITEM_TYPE_ID ");
                    sb.Append(" INNER JOIN UOM_INFO ON INV_ITEM_MASTER.UOM_ID = UOM_INFO.UOM_ID ");
                    sb.Append(" Where 1=1 "); ;

                   
                    cmdInfo.DBParametersInfo.Add("@isRepair", pisRepair);
                   
                    // sb.Append(" order by INV_ITEM_MASTER ITEM_DESC asc ");

                    DBQuery dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcINV_ITEM_MASTER>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }



        public static decimal Get_Item_Closing_Qty(DBContext dc,Int64 itemId)
        {
            bool isDCInit = false;
            decimal closingQty = 0;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                string abbr = " SELECT GET_CLOSING_QTY(@ITEM_ID) CLOSING_QTY  from Dual ";
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.DBParametersInfo.Add("@ITEM_ID", itemId);
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                closingQty = Convert.ToDecimal(DBQuery.ExecuteDBScalar(dbq, dc)); 
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return closingQty;
        }


        public static bool GetISBattery(DBContext dc,  int ITEM_ID)
        {
            bool isDCInit = false;
            int new_sl_no = 0;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                string abbr = " select COUNT(*) from INV_ITEM_MASTER where 1=1 AND ITEM_ID=@ITEM_ID AND SND_ITEM_CODE IS NOT NULL and SND_TRANSFER='Y' and IS_BATTERY='Y' ";

                cmdInfo.DBParametersInfo.Add("@ITEM_ID", ITEM_ID);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;

                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                new_sl_no = Convert.ToInt32(DBQuery.ExecuteDBScalar(dbq, dc));
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return new_sl_no > 0;
        }


        public static decimal Get_Inv_Transaction_Type_Id(int? specId,string issrcv)
        {
            int invTransTypeId = 0;
            if (!(specId>0))
            {
                specId = 1;
            }

            if (issrcv=="I")//Here I=Issue
            {
                switch (specId)
                {
                    case 1:
                        invTransTypeId = 401;
                        break;
                    case 2:
                        invTransTypeId = 3002;
                        break;
                    case 3:
                        invTransTypeId = 3003;
                        break;
                    case 4:
                        invTransTypeId = 3005;
                        break;
                    case 5:
                        invTransTypeId = 3007;
                        break;
                    case 6:
                        invTransTypeId = 3009;
                        break;
                    default:
                        invTransTypeId = 401;
                        break;
                }
            }


            if (issrcv == "R")//Here R=Receive
            {
                switch (specId)
                {
                    case 1:
                        invTransTypeId = 402;
                        break;
                    case 2:
                        invTransTypeId = 3001;
                        break;
                    case 3:
                        invTransTypeId = 3004;
                        break;
                    case 4:
                        invTransTypeId = 3006;
                        break;
                    case 5:
                        invTransTypeId = 3008;
                        break;
                    case 6:
                        invTransTypeId = 3010;
                        break;
                    default:
                        invTransTypeId = 402;
                        break;
                }
            }

            return invTransTypeId;
        }

        public static List<dcINV_ITEM_MASTER> GetDeptWiseItemListByType(int pDeptID,int pItemTypeID,int pStoreID, DBContext dc)
        {
            List<dcINV_ITEM_MASTER> cObjList = new List<dcINV_ITEM_MASTER>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetItem_Dept_List_ByType_SQLString(pDeptID, pItemTypeID, pStoreID));

                //if (pDeptID > 0)
                //{
                //    sb.Append(" and DEPT.DEPT_ID=@pDeptID ");
                //    cmdInfo.DBParametersInfo.Add("@pDeptID", pDeptID);
                //}
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                cObjList = DBQuery.ExecuteDBQuery<dcINV_ITEM_MASTER>(dbq, dc).ToList();

            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }


       
        
    }
}
