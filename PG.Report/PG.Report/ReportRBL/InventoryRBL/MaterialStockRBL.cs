using PG.Core.DBBase;
using PG.Report.ReportEnums;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using PG.DBClass.InventoryDC;
using PG.BLLibrary.InventoryBL;
using PG.Report.ReportClass.InventoryRC;
using System.Data;
using PG.Core.Utility;
using PG.Report.ReportClass.ProductionRC;
using PG.DBClass.ProductionDC;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace PG.Report.ReportRBL.InventoryRBL
{
    public class MaterialStockRBL
    {



        public static string GetItem_Master_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Select INV_ITEM_MASTER.* ");
            sb.Append(" , INV_ITEM_GROUP.ITEM_GROUP_CODE, INV_ITEM_GROUP.ITEM_GROUP_NAME  ");
            sb.Append(" , INV_ITEM_CLASS.ITEM_CLASS_CODE, INV_ITEM_CLASS.ITEM_CLASS_NAME  ");
            sb.Append(" , INV_ITEM_TYPE.ITEM_TYPE_CODE, INV_ITEM_TYPE.ITEM_TYPE_NAME  ");
            sb.Append(" , UOM_INFO.UOM_CODE, UOM_INFO.UOM_CODE_SHORT UOM_NAME , SNS.ITEM_SNS_NAME ");
            sb.Append(" ,GET_CLOSING_QTY(INV_ITEM_MASTER.ITEM_ID) CLOSING_QTY ");     
            sb.Append(" FROM INV_ITEM_MASTER ");
            sb.Append(" INNER JOIN INV_ITEM_GROUP ON INV_ITEM_MASTER.ITEM_GROUP_ID = INV_ITEM_GROUP.ITEM_GROUP_ID ");
            sb.Append(" INNER JOIN INV_ITEM_CLASS ON INV_ITEM_MASTER.ITEM_CLASS_ID = INV_ITEM_CLASS.ITEM_CLASS_ID ");
            sb.Append(" INNER JOIN INV_ITEM_TYPE ON INV_ITEM_MASTER.ITEM_TYPE_ID = INV_ITEM_TYPE.ITEM_TYPE_ID ");
            sb.Append(" INNER JOIN UOM_INFO ON INV_ITEM_MASTER.UOM_ID = UOM_INFO.UOM_ID ");
            sb.Append(" LEFT JOIN  INV_ITEM_SNS_MST SNS ON  INV_ITEM_MASTER.ITEM_SNS_ID=SNS.ITEM_SNS_ID ");
            sb.Append(" Where 1=1 ");
            return sb.ToString();
        }


        public static string StockEvaluation_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" Select ig.ITEM_GROUP_NAME,im.ITEM_CODE,im.ITEM_NAME ");
            sb.Append(" ,stk.INV_TRANS_TYPE_ID,stk.ISS_QTY TOT_ISSUE_QNTY,stk.RCV_QTY TOT_RECEIVE_QNTY,stk.TRANS_DATE ");
            sb.Append(" ,stk.TRANS_REMARKS,stkDept.DEPARTMENT_NAME STOCK_DEPARTMENT ");
            sb.Append(" ,fromDept.DEPARTMENT_NAME FROM_DEPARTMENT,toDept.DEPARTMENT_NAME TO_DEPARTMENT ");
            sb.Append(" ,tranType.INV_TRANS_TYPE_NAME ");
            sb.Append(" from ");
            sb.Append(" ITEM_STOCK_DETAILS stk ");
            sb.Append(" INNER JOIN INV_ITEM_MASTER im ");
            sb.Append(" on stk.ITEM_ID=im.ITEM_ID ");
            sb.Append(" INNER JOIN INV_ITEM_GROUP ig ");
            sb.Append(" on im.ITEM_GROUP_ID=ig.ITEM_GROUP_ID ");
            sb.Append(" LEFT JOIN DEPARTMENT_INFO stkDept ");
            sb.Append(" on stk.DEPARTMENT_ID=stkDept.DEPARTMENT_ID ");
            sb.Append(" LEFT JOIN DEPARTMENT_INFO fromDept ");
            sb.Append(" on stk.FROM_DEPARTMENT_ID=fromDept.DEPARTMENT_ID ");
            sb.Append(" LEFT JOIN DEPARTMENT_INFO toDept ");
            sb.Append(" on stk.TO_DEPARTMENT_ID=toDept.DEPARTMENT_ID ");
            sb.Append(" LEFT JOIN INV_TRANS_TYPE tranType ");
            sb.Append(" on stk.INV_TRANS_TYPE_ID=tranType.INV_TRANS_TYPE_ID ");           
            sb.Append(" Where 1=1 ");
            return sb.ToString();
        }



        //
        public static List<dcITEM_STOCK_DETAILS> LoadItemStockforLocalitemtypePreview(ReportParameterClass rptClass, DBContext dc)
        {

            List<dcITEM_STOCK_DETAILS> grpList = MaterialStockBL.GetItemStockforLocalitemtypePreview(rptClass, dc).ToList();
            return grpList;

        }


        public static List<rcMaterialStock> GetItemStockReport(clsPrmInventory prmINV)
        {
            return GetItemStockReport(prmINV, null);
        }

        public static List<rcMaterialStock> GetItemStockReport(clsPrmInventory prmINV, DBContext dc)
        {

            List<rcMaterialStock> cRptList = new List<rcMaterialStock>();


            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();
                cmdInfo.DBParametersInfo.Clear();

                if (prmINV.ToDate.HasValue)
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.ToDate.Value);
                }

                cmdInfo.DBParametersInfo.Add(":p_itemtype_id", prmINV.itemtype_id);
                cmdInfo.DBParametersInfo.Add(":p_itemGroup_id", prmINV.itemGroup_id);

                cmdInfo.DBParametersInfo.Add(":p_item_id", prmINV.item_id);
                cmdInfo.DBParametersInfo.Add(":p_store_id", prmINV.store_id);
                cmdInfo.DBParametersInfo.Add(":p_itemclass_id", prmINV.ItemClassId);
                cmdInfo.DBParametersInfo.Add(":p_ItemCategoryID", prmINV.ItemCategoryID);
                cmdInfo.DBParametersInfo.Add(":p_ItemSizeID", prmINV.ItemSizeID);
                cmdInfo.DBParametersInfo.Add(":p_ItemColorID", prmINV.ItemColorID);
                cmdInfo.DBParametersInfo.Add(":p_ItemBrandID", prmINV.ItemBrandID);
                cmdInfo.DBParametersInfo.Add(":p_battery_type_id", prmINV.Battery_Type_ID);
                cmdInfo.DBParametersInfo.Add(":p_is_repair", prmINV.Is_Repair);
                cmdInfo.DBParametersInfo.Add(":p_item_sns_id", prmINV.SNS_Type);

              

               
                

                //cmdInfo.DBParametersInfo.Add(":P_ITEM_CODE", prmSND.Item_Code);
                DBQuery dbq = new DBQuery();

                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                //dbq.CommandTimeout = 1000;
                cmdInfo.CommandTimeout = 600;
                cmdInfo.CommandText = "SP_STORE_ITEM_STOCK_NEW";
                //cmdInfo.CommandText = "SP_STORE_ITEM_STOCK_TEST";
                
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);

                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;
                //sb.Append(" SELECT * FROM TEMP_ITEM_STOCK_BAT_FOR_NET ");
                //sb.Append(" Select ITEM_GROUP_ID,ITEM_GROUP_NAME,ITEM_CODE,ITEM_ID,ITEM_NAME,UOM_ID,UOM_NAME,ITEM_TYPE_ID,ITEM_TYPE_NAME,ITEM_CLASS_ID,ITEM_CLASS_NAME,OP_BAL,OP_MRR_QTY,OP_IRR_QTY,OP_ITC_QTY,OP_OUTSALES_QTY,OP_ROTARY_QTY,OP_LOANRP_QTY,OP_RTN_QTY,OP_ADJ,OP_BAL+OP_MRR_QTY+OP_IRR_QTY-OP_ITC_QTY-OP_OUTSALES_QTY-OP_ROTARY_QTY-OP_LOANRP_QTY+OP_RTN_QTY+OP_ADJ OPPENING_BAL_QTY,MRR_QTY,IRR_QTY,ITC_QTY,OUTSALES_QTY,ROTARY_QTY,LOANRP_QTY,RTN_QTY, OP_BAL+OP_MRR_QTY+OP_IRR_QTY-OP_ITC_QTY-OP_OUTSALES_QTY-OP_ROTARY_QTY-OP_LOANRP_QTY+OP_RTN_QTY+OP_ADJ+MRR_QTY+IRR_QTY-ITC_QTY-OUTSALES_QTY-ROTARY_QTY-LOANRP_QTY+RTN_QTY+ADJUST_QTY COLISING_QTY, BAL_QTY,ADJUST_QTY, ITEM_RATE FROM TEMP_STORE_ITEM_STOCK");
                //sb.Append(" Select ITEM_GROUP_ID,ITEM_GROUP_NAME,ITEM_CODE,ITEM_ID,ITEM_NAME,UOM_ID,UOM_NAME,ITEM_TYPE_ID,ITEM_TYPE_NAME,ITEM_CLASS_ID,ITEM_CLASS_NAME,OP_BAL,OP_QC_PASS_QTY,OP_MRR_QTY,OP_IRR_QTY,OP_ITC_QTY,OP_OUTSALES_QTY,OP_ROTARY_QTY,OP_LOANRP_QTY,OP_RTN_QTY,OP_ADJ,OP_BAL+OP_QC_PASS_QTY+OP_MRR_QTY+OP_IRR_QTY-OP_ITC_QTY-OP_OUTSALES_QTY-OP_ROTARY_QTY-OP_LOANRP_QTY+OP_RTN_QTY+OP_ADJ OPPENING_BAL_QTY,QC_PASS_QTY,MRR_QTY,IRR_QTY,ITC_QTY,OUTSALES_QTY,ROTARY_QTY,LOANRP_QTY,RTN_QTY, OP_BAL+OP_QC_PASS_QTY+OP_MRR_QTY+OP_IRR_QTY-OP_ITC_QTY-OP_OUTSALES_QTY-OP_ROTARY_QTY-OP_LOANRP_QTY+OP_RTN_QTY+OP_ADJ+QC_PASS_QTY+MRR_QTY+IRR_QTY-ITC_QTY-OUTSALES_QTY-ROTARY_QTY-LOANRP_QTY+RTN_QTY+ADJUST_QTY COLISING_QTY, BAL_QTY,QC_REJECT_QTY,QC_RETURN_QTY,ADJUST_QTY, ITEM_RATE FROM TEMP_STORE_ITEM_STOCK");
                sb.Append(" Select ITEM_GROUP_ID,ITEM_GROUP_NAME,ITEM_CODE,ITEM_ID,ITEM_NAME,UOM_ID,UOM_NAME,ITEM_TYPE_ID,ITEM_TYPE_NAME,ITEM_CLASS_ID,ITEM_CLASS_NAME,OP_BAL,OP_QC_PASS_QTY,OP_MRR_QTY,OP_IRR_QTY,OP_ITC_QTY,OP_OUTSALES_QTY,OP_ROTARY_QTY,OP_LOANRP_QTY,OP_RTN_QTY,OP_ADJ,OP_BAL+OP_QC_PASS_QTY+OP_MRR_QTY+OP_IRR_QTY-OP_ITC_QTY-OP_OUTSALES_QTY-OP_ROTARY_QTY-OP_LOANRP_QTY+OP_ADJ OPPENING_BAL_QTY,QC_PASS_QTY,MRR_QTY,IRR_QTY,ITC_QTY,OUTSALES_QTY,ROTARY_QTY,LOANRP_QTY,RTN_QTY, OP_BAL+OP_QC_PASS_QTY+OP_MRR_QTY+OP_IRR_QTY-OP_ITC_QTY-OP_OUTSALES_QTY-OP_ROTARY_QTY-OP_LOANRP_QTY+OP_ADJ+QC_PASS_QTY+MRR_QTY+IRR_QTY-ITC_QTY-OUTSALES_QTY-ROTARY_QTY-LOANRP_QTY+ADJUST_QTY COLISING_QTY, BAL_QTY,QC_REJECT_QTY,OP_QC_RETURN_QTY+QC_RETURN_QTY-RTN_QTY-OP_RTN_QTY QC_RETURN_QTY ,ADJUST_QTY, ITEM_RATE,STORE_ID,STORE_NAME FROM TEMP_STORE_ITEM_STOCK");
                //OP_BAL,OP_MRR_QTY,OP_IRR_QTY,OP_ITC_QTY,OP_OUTSALES_QTY,OP_ROTARY_QTY,OP_LOANRP_QTY,OP_RTN_QTY,OP_ADJ

                DBQuery dbqtemp = new DBQuery();
                //DBQuery dbq = new DBQuery();


                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                //DBQuery.ExecuteDBQuerySP(dbq, dc);
                DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);

                //,,,,,,,,,,,,,,,,,


                foreach (DataRow dRow in dtData.Rows)
                {
                    rcMaterialStock stk = new rcMaterialStock();

                    stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    stk.ITEM_CODE = dRow["ITEM_CODE"].ToString();

                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();

                    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);

                    stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                    stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();

                    //here all possible transaction for calculation opening balance
                    stk.OP_BAL = Conversion.DBNullDecimalToZero(dRow["OP_BAL"]);
                    stk.OP_MRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_MRR_QTY"]);
                    stk.OP_QC_PASS_QTY = Conversion.DBNullDecimalToZero(dRow["OP_QC_PASS_QTY"]);
                    stk.OP_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_BAL"]);
                    stk.OP_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_IRR_QTY"]);
                    stk.OP_OUTSALES_QTY = Conversion.DBNullDecimalToZero(dRow["OP_OUTSALES_QTY"]);
                    stk.OP_ROTARY_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ROTARY_QTY"]);
                    stk.OP_LOANRP_QTY = Conversion.DBNullDecimalToZero(dRow["OP_LOANRP_QTY"]);
                    stk.OP_RTN_QTY = Conversion.DBNullDecimalToZero(dRow["OP_RTN_QTY"]);
                    stk.OP_ADJ = Conversion.DBNullDecimalToZero(dRow["OP_ADJ"]);
                    stk.STORE_ID = Conversion.DBNullIntToZero(dRow["STORE_ID"]);
                    stk.STORE_NAME = dRow["STORE_NAME"].ToString();


                    stk.OPPENING_BAL_QTY = stk.OP_BAL + stk.OP_QC_PASS_QTY + stk.OP_MRR_QTY + stk.OP_IRR_QTY - stk.OP_ITC_QTY - stk.OP_OUTSALES_QTY - stk.OP_ROTARY_QTY - stk.OP_LOANRP_QTY  + stk.OP_ADJ;

                    stk.OPPENING_BAL_QTY = Conversion.DBNullDecimalToZero(dRow["OPPENING_BAL_QTY"]);
                    stk.MRR_QTY = Conversion.DBNullDecimalToZero(dRow["MRR_QTY"]);
                    stk.QC_PASS_QTY = Conversion.DBNullDecimalToZero(dRow["QC_PASS_QTY"]);
                    stk.IRR_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_QTY"]);

                    stk.ITC_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_QTY"]);
                    stk.OUTSALES_QTY = Conversion.DBNullDecimalToZero(dRow["OUTSALES_QTY"]);

                    stk.ROTARY_QTY = Conversion.DBNullDecimalToZero(dRow["ROTARY_QTY"]);
                    stk.LOANRP_QTY = Conversion.DBNullDecimalToZero(dRow["LOANRP_QTY"]);
                    stk.RTN_QTY = Conversion.DBNullDecimalToZero(dRow["RTN_QTY"]);
                    stk.BAL_QTY = Conversion.DBNullDecimalToZero(dRow["BAL_QTY"]);
                    stk.COLISING_QTY = Conversion.DBNullDecimalToZero(dRow["COLISING_QTY"]);

                    stk.QC_RETURN_QTY = Conversion.DBNullDecimalToZero(dRow["QC_RETURN_QTY"]);
                    stk.QC_REJECT_QTY = Conversion.DBNullDecimalToZero(dRow["QC_REJECT_QTY"]);
                    stk.ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);

                    stk.ITEM_RATE = Conversion.DBNullDecimalToZero(dRow["ITEM_RATE"]);
                    stk.COLISING_VALUE = (stk.COLISING_QTY * stk.ITEM_RATE);

                    if (stk.OPPENING_BAL_QTY > 0 || stk.COLISING_VALUE > 0 || stk.COLISING_QTY > 0 || stk.ROTARY_QTY > 0 || stk.BAL_QTY > 0 || stk.ITC_QTY > 0 || stk.QC_PASS_QTY > 0 || stk.MRR_QTY > 0 || stk.IRR_QTY > 0 || stk.QC_RETURN_QTY > 0)
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

        //Get LC Costing Data

        public static List<rcLcCosting> GetLC_Costing_Report(clsPrmInventory prmINV)
        {
            return GetLC_Costing_Report(prmINV, null);
        }

        public static List<rcLcCosting> GetLC_Costing_Report(clsPrmInventory prmINV, DBContext dc)
        {

            List<rcLcCosting> cRptList = new List<rcLcCosting>();


            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();
                cmdInfo.DBParametersInfo.Clear();

                DBQuery dbq = new DBQuery();

               

                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;

                sb.Append(" SELECT lc.COSTING_NO,lc.B_E_NO,lc.BILL_NO,lm.LC_NO,si.SUP_NAME,im.ITEM_NAME, to_char(lc.COSTING_DATE,'rrrrmm') MONTH_YEAR,TO_CHAR(lc.COSTING_DATE,'Mon-YY') MONTHS,lc.ITEM_QTY,lc.UNIT_RATE,lc.LC_RATE,lc.UOM_NAME ");
                sb.Append(" ,lc.CONVERTED_UOM_NAME,lc.ITEM_QTY*lc.LC_RATE INVOICE_VALUED_USD,lc.CONVERSION_RATE,lc.ITEM_QTY*(NVL(lc.LC_RATE,0)*NVL(lc.CONVERSION_RATE,0)) INVOICE_VALUE_BDT ");
                sb.Append(" ,TOTAL_COST_WO_VAT_ACT LANDED_COST,lc.FACTOR,cic.COSTING_ITEM_CAT_NAME,lc.ASSESSABLE_VALUE,lc.GLOBAL_TAXES,NVL(lc.CD,0) CUSTOMS_DUTY,NVL(lc.RD,0) RD_IMPORT,NVL(lc.SD,0) SD_IMPORT ");
                sb.Append(" ,lc.VAT INPUT_VAT_RECEIVEABLE,NVL(lc.AIT,0) AIT_IMPORT,NVL(lc.AT,0) AT_IMPORT,lc.MARINE_INSURANCE,lc.SEA_FREIGHT,lc.TOTAL_CLEARING_CHARGE,lc.TRANSPORT CARRIGE_INWARD_IMPORT ,0 TOTAL  ");
                sb.Append(" FROM LC_COSTING_MST lc ");
                sb.Append(" INNER JOIN LC_MASTER lm ON lc.LC_ID=lm.LC_ID ");
                sb.Append(" INNER JOIN SUPPLIER_INFO si ON lm.SUP_ID=si.SUP_ID ");
                sb.Append(" INNER JOIN INV_ITEM_MASTER im ON lc.ITEM_ID=im.ITEM_ID ");
                sb.Append(" LEFT JOIN COSTING_ITEM_CATEGORY cic ON lc.COSTING_ITEM_CAT_ID=cic.COSTING_ITEM_CAT_ID ");
                sb.Append(" WHERE 1=1 ");

                 if (prmINV.ToDate.HasValue)
                {
                    sb.Append(" AND TO_DATE(lc.COSTING_DATE) BETWEEN :P_DATE_FROM AND :P_DATE_TO ");
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.ToDate.Value);
                }

                 string strListCostingitem = "0";

                 if (prmINV.Costing_item_catlist.Count > 0)
                 {
                     strListCostingitem = string.Join("','", prmINV.Costing_item_catlist.ToArray());

                 }




                 if (strListCostingitem != string.Empty)
                 {

                     sb.Append(string.Format(" AND lc.COSTING_ITEM_CAT_ID IN ({0}) ", strListCostingitem));
                 }


                //if(prmINV.Costing_Item_Type_id>0)
                //{
                //    sb.Append(" AND lc.COSTING_ITEM_CAT_ID=:P_Costing_Item_Type_id ");
                //    cmdInfo.DBParametersInfo.Add(":P_Costing_Item_Type_id", prmINV.Costing_Item_Type_id);
                //}

                if (prmINV.item_id > 0)
                {
                    sb.Append(" AND lc.ITEM_ID=:P_item_id ");
                    cmdInfo.DBParametersInfo.Add(":P_item_id", prmINV.item_id);
                }
               
                sb.Append(" ORDER BY to_char(lc.COSTING_DATE,'rrrrmm') ");
                

                DBQuery dbqtemp = new DBQuery();
                //DBQuery dbq = new DBQuery();


                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);

                //,,,,,,,,,,,,,,,,,


                foreach (DataRow dRow in dtData.Rows)
                {
                    rcLcCosting stk = new rcLcCosting();

                   
                    stk.COSTING_NO = dRow["COSTING_NO"].ToString();

                    stk.B_E_NO = dRow["B_E_NO"].ToString();

                    stk.BILL_NO = dRow["BILL_NO"].ToString();
                    stk.LC_NO = dRow["LC_NO"].ToString();

                    stk.SUP_NAME = dRow["SUP_NAME"].ToString();
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();

                    stk.MONTH_YEAR = Conversion.DBNullIntToZero(dRow["MONTH_YEAR"]);
                    stk.MONTHS = dRow["MONTHS"].ToString();
                    stk.ITEM_QTY =Conversion.DBNullDecimalToZero(dRow["ITEM_QTY"]);

                    //here all possible transaction for calculation opening balance
                    stk.UNIT_RATE = Conversion.DBNullDecimalToZero(dRow["UNIT_RATE"]);
                    stk.LC_RATE = Conversion.DBNullDecimalToZero(dRow["LC_RATE"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.CONVERTED_UOM_NAME = dRow["CONVERTED_UOM_NAME"].ToString();
                    stk.INVOICE_VALUED_USD = Conversion.DBNullDecimalToZero(dRow["INVOICE_VALUED_USD"]);
                    stk.CONVERSION_RATE = Conversion.DBNullDecimalToZero(dRow["CONVERSION_RATE"]);
                    stk.INVOICE_VALUE_BDT = Conversion.DBNullDecimalToZero(dRow["INVOICE_VALUE_BDT"]);
                    stk.LANDED_COST = Conversion.DBNullDecimalToZero(dRow["LANDED_COST"]);
                    stk.FACTOR = Conversion.DBNullDecimalToZero(dRow["FACTOR"]);
                    stk.COSTING_ITEM_CAT_NAME = dRow["COSTING_ITEM_CAT_NAME"].ToString();
                    stk.ASSESSABLE_VALUE = Conversion.DBNullDecimalToZero(dRow["ASSESSABLE_VALUE"]);
                    stk.GLOBAL_TAXES = Conversion.DBNullDecimalToZero(dRow["GLOBAL_TAXES"]);
                    stk.CUSTOMS_DUTY = Conversion.DBNullDecimalToZero(dRow["CUSTOMS_DUTY"]);
                    stk.RD_IMPORT = Conversion.DBNullDecimalToZero(dRow["RD_IMPORT"]);
                    stk.SD_IMPORT = Conversion.DBNullDecimalToZero(dRow["SD_IMPORT"]);
                    stk.INPUT_VAT_RECEIVEABLE = Conversion.DBNullDecimalToZero(dRow["INPUT_VAT_RECEIVEABLE"]);

                    stk.AIT_IMPORT = Conversion.DBNullDecimalToZero(dRow["AIT_IMPORT"]);
                    stk.AT_IMPORT = Conversion.DBNullDecimalToZero(dRow["AT_IMPORT"]);

                    stk.MARINE_INSURANCE = Conversion.DBNullDecimalToZero(dRow["MARINE_INSURANCE"]);
                    stk.SEA_FREIGHT = Conversion.DBNullDecimalToZero(dRow["SEA_FREIGHT"]);
                    stk.TOTAL_CLEARING_CHARGE = Conversion.DBNullDecimalToZero(dRow["TOTAL_CLEARING_CHARGE"]);
                    stk.CARRIGE_INWARD_IMPORT = Conversion.DBNullDecimalToZero(dRow["CARRIGE_INWARD_IMPORT"]);
                    stk.TOTAL = Conversion.DBNullDecimalToZero(dRow["TOTAL"]);

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


        public static Byte[] Get_LCCosting_Details_ExcelData(clsPrmInventory prmINV, bool pExecuteSP)
        {
            return Get_LCCosting_Details_ExcelData(prmINV, pExecuteSP, null);
        }

        public static Byte[] Get_LCCosting_Details_ExcelData(clsPrmInventory prmINV, bool pExecuteSP, DBContext dc)
        {

            bool isDCInit = false;

            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

            DBCommandInfo cmdInfo = new DBCommandInfo();
            StringBuilder sb = new StringBuilder();
            cmdInfo.DBParametersInfo.Clear();

            




            DBQuery dbq = new DBQuery();

           

            DBCommandInfo cmdInfotemp = new DBCommandInfo();
            sb.Length = 0;
            sb.Append(" SELECT lc.COSTING_NO,lc.B_E_NO,lc.BILL_NO,lm.LC_NO,si.SUP_NAME,im.ITEM_NAME, to_char(lc.COSTING_DATE,'rrrrmm') MONTH_YEAR,TO_CHAR(lc.COSTING_DATE,'Mon-YY') MONTHS,lc.ITEM_QTY,lc.CONVERTED_ITEM_QTY,(SELECT LC_DETAILS.UNIT_PRICE FROM LC_DETAILS WHERE LC_ID=lm.LC_ID  and ITEM_ID=lc.ITEM_ID AND ROWNUM=1) UNIT_RATE,lc.LC_RATE,lc.UOM_NAME ");
            sb.Append(" ,lc.CONVERTED_UOM_NAME,lc.ITEM_QTY*lc.LC_RATE INVOICE_VALUED_USD,lc.CONVERSION_RATE,lc.ITEM_QTY*(NVL(lc.LC_RATE,0)*NVL(lc.CONVERSION_RATE,0)) INVOICE_VALUE_BDT ");
            sb.Append(" ,TOTAL_COST_WO_VAT_ACT LANDED_COST,lc.FACTOR,cic.COSTING_ITEM_CAT_NAME,lc.ASSESSABLE_VALUE,lc.GLOBAL_TAXES,NVL(lc.CD,0) CUSTOMS_DUTY,NVL(lc.RD,0) RD_IMPORT,NVL(lc.SD,0) SD_IMPORT ");
            sb.Append(" ,lc.VAT INPUT_VAT_RECEIVEABLE,NVL(lc.AIT,0) AIT_IMPORT,NVL(lc.AT,0) AT_IMPORT,lc.MARINE_INSURANCE,lc.SEA_FREIGHT,(NVL(lc.TOTAL_CLEARING_CHARGE,0)-NVL(lc.TRANSPORT,0)) TOTAL_CLEARING_CHARGE,lc.TRANSPORT CARRIGE_INWARD_IMPORT ,0 TOTAL  ");
            sb.Append(" FROM LC_COSTING_MST lc ");
            sb.Append(" INNER JOIN LC_MASTER lm ON lc.LC_ID=lm.LC_ID ");
            sb.Append(" INNER JOIN SUPPLIER_INFO si ON lm.SUP_ID=si.SUP_ID ");
            sb.Append(" INNER JOIN INV_ITEM_MASTER im ON lc.ITEM_ID=im.ITEM_ID ");
            sb.Append(" LEFT JOIN COSTING_ITEM_CATEGORY cic ON lc.COSTING_ITEM_CAT_ID=cic.COSTING_ITEM_CAT_ID ");
            sb.Append(" WHERE 1=1 ");

            if (prmINV.ToDate.HasValue)
            {
                sb.Append(" AND lc.COSTING_DATE BETWEEN :P_DATE_FROM AND :P_DATE_TO ");
                cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.FromDate.Value);
                cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.ToDate.Value);
            }

            string strListCostingitem = "0";

            if (prmINV.Costing_item_catlist.Count > 0)
            {
                strListCostingitem = string.Join(",", prmINV.Costing_item_catlist.ToArray());

            }




            if (strListCostingitem != "0")
            {

                sb.Append(string.Format(" AND lc.COSTING_ITEM_CAT_ID IN ({0}) ", strListCostingitem));
            }

            //if (prmINV.Costing_Item_Type_id > 0)
            //{
            //    sb.Append(" AND lc.COSTING_ITEM_CAT_ID=:P_Costing_Item_Type_id ");
            //    cmdInfo.DBParametersInfo.Add(":P_Costing_Item_Type_id", prmINV.Costing_Item_Type_id);
            //}

            if (prmINV.item_id > 0)
            {
                sb.Append(" AND lc.ITEM_ID=:P_item_id ");
                cmdInfo.DBParametersInfo.Add(":P_item_id", prmINV.item_id);
            }


            sb.Append(" ORDER BY lm.LC_NO,to_char(lc.COSTING_DATE,'rrrrmm') ");

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
                workSheet.Cells[1, colNum].Value = "Costing No";

                colNum = 3;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Bill of Entry No";

                colNum = 4;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Bill No";

                colNum = 5;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "LC/PO No";

                colNum = 6;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Supplier";

                colNum = 7;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Material Description";


                colNum = 8;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Month Year";

                colNum = 9;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Months";
                colNum = 10;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Quantity";
                colNum = 11;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Rate Per";
               
                colNum = 12;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Unit";

                colNum = 13;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Converted Item Qty";
                colNum = 14;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Converted Uom Name";

                colNum = 15;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Invoice Value USD";
                colNum = 16;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Conversion";

                colNum = 17;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Invoice Value BDT";

                colNum = 18;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Landed Cost";

                colNum = 19;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Landed Cost Factor";

                colNum = 20;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Rate";

                colNum = 21;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Type";

                colNum = 22;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Assessable Value";

                colNum = 23;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Global_Taxes";

                colNum = 24;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Customs_Duty";


                colNum = 25;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "RD-Import";

                colNum = 26;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "SD-Import";
                colNum = 27;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Input Vat Receivable";
                colNum = 28;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "AIT-Import";
                colNum = 29;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "AT-Import";

                colNum = 30;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Marine_Insurance";

                colNum = 31;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Freight";

                colNum = 32;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Clearing Charges";
                colNum = 33;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Carrige Inward_Import";

                colNum = 34;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Total";


                //Body of table  

                int recordIndex = 2;
                int slno = 0;
                foreach (DataRow dRow in dtData.Rows)
                {
                    slno++;
                    workSheet.Cells[recordIndex, 1].Value = slno;
                    workSheet.Cells[recordIndex, 2].Value = dRow["COSTING_NO"].ToString();
                    workSheet.Cells[recordIndex, 3].Value = dRow["B_E_NO"].ToString();
                    workSheet.Cells[recordIndex, 4].Value = dRow["BILL_NO"].ToString();
                    workSheet.Cells[recordIndex, 5].Value = dRow["LC_NO"].ToString();
                    workSheet.Cells[recordIndex, 6].Value = dRow["SUP_NAME"].ToString();
                    workSheet.Cells[recordIndex, 7].Value = dRow["ITEM_NAME"].ToString();

                    workSheet.Cells[recordIndex, 8].Value = Conversion.DBNullDecimalToZero(dRow["MONTH_YEAR"]);
                    workSheet.Cells[recordIndex, 9].Value = dRow["MONTHS"].ToString();
                    workSheet.Cells[recordIndex, 10].Value = Conversion.DBNullDecimalToZero(dRow["ITEM_QTY"]);
                    workSheet.Cells[recordIndex, 11].Value = Conversion.DBNullDecimalToZero(dRow["UNIT_RATE"]);

                    
                    workSheet.Cells[recordIndex, 12].Value = dRow["UOM_NAME"].ToString();
                    workSheet.Cells[recordIndex, 13].Value = Conversion.DBNullDecimalToZero(dRow["CONVERTED_ITEM_QTY"]);
                    workSheet.Cells[recordIndex, 14].Value = dRow["CONVERTED_UOM_NAME"].ToString();
                    workSheet.Cells[recordIndex, 15].Value = Conversion.DBNullDecimalToZero(dRow["INVOICE_VALUED_USD"]);
                    workSheet.Cells[recordIndex, 16].Value = Conversion.DBNullDecimalToZero(dRow["CONVERSION_RATE"]);
                    workSheet.Cells[recordIndex, 17].Value = Conversion.DBNullDecimalToZero(dRow["INVOICE_VALUE_BDT"]);
                    workSheet.Cells[recordIndex, 18].Value = Conversion.DBNullDecimalToZero(dRow["LANDED_COST"]);
                    workSheet.Cells[recordIndex, 19].Value = Conversion.DBNullDecimalToZero(dRow["FACTOR"]);
                    workSheet.Cells[recordIndex, 20].Value = Conversion.DBNullDecimalToZero(dRow["LC_RATE"]);
                    workSheet.Cells[recordIndex, 21].Value = dRow["COSTING_ITEM_CAT_NAME"].ToString();

                    workSheet.Cells[recordIndex, 22].Value = Conversion.DBNullDecimalToZero(dRow["ASSESSABLE_VALUE"]);
                    workSheet.Cells[recordIndex, 23].Value = Conversion.DBNullDecimalToZero(dRow["GLOBAL_TAXES"]);
                    workSheet.Cells[recordIndex, 24].Value = Conversion.DBNullDecimalToZero(dRow["CUSTOMS_DUTY"]);
                    workSheet.Cells[recordIndex, 25].Value = Conversion.DBNullDecimalToZero(dRow["RD_IMPORT"]);
                    workSheet.Cells[recordIndex, 26].Value = Conversion.DBNullDecimalToZero(dRow["SD_IMPORT"]);
                    workSheet.Cells[recordIndex, 27].Value = Conversion.DBNullDecimalToZero(dRow["INPUT_VAT_RECEIVEABLE"]);
                    workSheet.Cells[recordIndex, 28].Value = Conversion.DBNullDecimalToZero(dRow["AIT_IMPORT"]);
                    workSheet.Cells[recordIndex, 29].Value = Conversion.DBNullDecimalToZero(dRow["AT_IMPORT"]);
                    workSheet.Cells[recordIndex, 30].Value = Conversion.DBNullDecimalToZero(dRow["MARINE_INSURANCE"]);
                    workSheet.Cells[recordIndex, 31].Value = Conversion.DBNullDecimalToZero(dRow["SEA_FREIGHT"]);
                    workSheet.Cells[recordIndex, 32].Value = Conversion.DBNullDecimalToZero(dRow["TOTAL_CLEARING_CHARGE"]);
                    workSheet.Cells[recordIndex, 33].Value = Conversion.DBNullDecimalToZero(dRow["CARRIGE_INWARD_IMPORT"]);
                    workSheet.Cells[recordIndex, 34].Value = Conversion.DBNullDecimalToZero(dRow["TOTAL"]);
                    


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
                workSheet.Column(16).AutoFit();
                workSheet.Column(17).AutoFit();
                workSheet.Column(18).AutoFit();
                workSheet.Column(19).AutoFit();
                workSheet.Column(20).AutoFit();
                workSheet.Column(21).AutoFit();
                workSheet.Column(22).AutoFit();
                workSheet.Column(23).AutoFit();
                workSheet.Column(24).AutoFit();
                workSheet.Column(25).AutoFit();
                workSheet.Column(26).AutoFit();
                workSheet.Column(27).AutoFit();
                workSheet.Column(28).AutoFit();
                workSheet.Column(29).AutoFit();
                workSheet.Column(30).AutoFit();
                workSheet.Column(31).AutoFit();
                workSheet.Column(32).AutoFit();
                workSheet.Column(33).AutoFit();
                workSheet.Column(34).AutoFit();
                
                

                bytes = excel.GetAsByteArray();

            }

            return bytes;

        }

        public static List<rcMaterialStock> GetItemStockReportWithPR(clsPrmInventory prmINV, DBContext dc)
        {

            List<rcMaterialStock> cRptList = new List<rcMaterialStock>();


            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();
                cmdInfo.DBParametersInfo.Clear();

                if (prmINV.ToDate.HasValue)
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.ToDate.Value);
                }

                cmdInfo.DBParametersInfo.Add(":p_itemtype_id", prmINV.itemtype_id);
                cmdInfo.DBParametersInfo.Add(":p_itemGroup_id", prmINV.itemGroup_id);

                cmdInfo.DBParametersInfo.Add(":p_item_id", prmINV.item_id);
                cmdInfo.DBParametersInfo.Add(":p_store_id", prmINV.store_id);
                cmdInfo.DBParametersInfo.Add(":p_itemclass_id", prmINV.ItemClassId);
                cmdInfo.DBParametersInfo.Add(":p_ItemCategoryID", prmINV.ItemCategoryID);
                cmdInfo.DBParametersInfo.Add(":p_ItemSizeID", prmINV.ItemSizeID);
                cmdInfo.DBParametersInfo.Add(":p_ItemColorID", prmINV.ItemColorID);
                cmdInfo.DBParametersInfo.Add(":p_ItemBrandID", prmINV.ItemBrandID);
                cmdInfo.DBParametersInfo.Add(":p_battery_type_id", prmINV.Battery_Type_ID);
                cmdInfo.DBParametersInfo.Add(":p_is_repair", prmINV.Is_Repair);
                cmdInfo.DBParametersInfo.Add(":p_item_sns_id", prmINV.SNS_Type);






                //cmdInfo.DBParametersInfo.Add(":P_ITEM_CODE", prmSND.Item_Code);
                DBQuery dbq = new DBQuery();

                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                //dbq.CommandTimeout = 1000;
                cmdInfo.CommandTimeout = 600;
                cmdInfo.CommandText = "SP_STORE_ITEM_STOCK_FOR_PR";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);

                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;
                //sb.Append(" SELECT * FROM TEMP_ITEM_STOCK_BAT_FOR_NET ");
                //sb.Append(" Select ITEM_GROUP_ID,ITEM_GROUP_NAME,ITEM_CODE,ITEM_ID,ITEM_NAME,UOM_ID,UOM_NAME,ITEM_TYPE_ID,ITEM_TYPE_NAME,ITEM_CLASS_ID,ITEM_CLASS_NAME,OP_BAL,OP_MRR_QTY,OP_IRR_QTY,OP_ITC_QTY,OP_OUTSALES_QTY,OP_ROTARY_QTY,OP_LOANRP_QTY,OP_RTN_QTY,OP_ADJ,OP_BAL+OP_MRR_QTY+OP_IRR_QTY-OP_ITC_QTY-OP_OUTSALES_QTY-OP_ROTARY_QTY-OP_LOANRP_QTY+OP_RTN_QTY+OP_ADJ OPPENING_BAL_QTY,MRR_QTY,IRR_QTY,ITC_QTY,OUTSALES_QTY,ROTARY_QTY,LOANRP_QTY,RTN_QTY, OP_BAL+OP_MRR_QTY+OP_IRR_QTY-OP_ITC_QTY-OP_OUTSALES_QTY-OP_ROTARY_QTY-OP_LOANRP_QTY+OP_RTN_QTY+OP_ADJ+MRR_QTY+IRR_QTY-ITC_QTY-OUTSALES_QTY-ROTARY_QTY-LOANRP_QTY+RTN_QTY+ADJUST_QTY COLISING_QTY, BAL_QTY,ADJUST_QTY, ITEM_RATE FROM TEMP_STORE_ITEM_STOCK");
                //sb.Append(" Select ITEM_GROUP_ID,ITEM_GROUP_NAME,ITEM_CODE,ITEM_ID,ITEM_NAME,UOM_ID,UOM_NAME,ITEM_TYPE_ID,ITEM_TYPE_NAME,ITEM_CLASS_ID,ITEM_CLASS_NAME,OP_BAL,OP_QC_PASS_QTY,OP_MRR_QTY,OP_IRR_QTY,OP_ITC_QTY,OP_OUTSALES_QTY,OP_ROTARY_QTY,OP_LOANRP_QTY,OP_RTN_QTY,OP_ADJ,OP_BAL+OP_QC_PASS_QTY+OP_MRR_QTY+OP_IRR_QTY-OP_ITC_QTY-OP_OUTSALES_QTY-OP_ROTARY_QTY-OP_LOANRP_QTY+OP_RTN_QTY+OP_ADJ OPPENING_BAL_QTY,QC_PASS_QTY,MRR_QTY,IRR_QTY,ITC_QTY,OUTSALES_QTY,ROTARY_QTY,LOANRP_QTY,RTN_QTY, OP_BAL+OP_QC_PASS_QTY+OP_MRR_QTY+OP_IRR_QTY-OP_ITC_QTY-OP_OUTSALES_QTY-OP_ROTARY_QTY-OP_LOANRP_QTY+OP_RTN_QTY+OP_ADJ+QC_PASS_QTY+MRR_QTY+IRR_QTY-ITC_QTY-OUTSALES_QTY-ROTARY_QTY-LOANRP_QTY+RTN_QTY+ADJUST_QTY COLISING_QTY, BAL_QTY,QC_REJECT_QTY,QC_RETURN_QTY,ADJUST_QTY, ITEM_RATE FROM TEMP_STORE_ITEM_STOCK");
                sb.Append(" Select ITEM_GROUP_ID,ITEM_GROUP_NAME,ITEM_CODE,ITEM_ID,ITEM_NAME,UOM_ID,UOM_NAME,ITEM_TYPE_ID,ITEM_TYPE_NAME,ITEM_CLASS_ID,ITEM_CLASS_NAME,OP_BAL,OP_QC_PASS_QTY,OP_MRR_QTY,OP_IRR_QTY,OP_ITC_QTY,OP_OUTSALES_QTY,OP_ROTARY_QTY,OP_LOANRP_QTY,OP_RTN_QTY,OP_ADJ,OP_BAL+OP_QC_PASS_QTY+OP_MRR_QTY+OP_IRR_QTY-OP_ITC_QTY-OP_OUTSALES_QTY-OP_ROTARY_QTY-OP_LOANRP_QTY-OP_RTN_QTY+OP_ADJ OPPENING_BAL_QTY ");
                sb.Append(" ,QC_PASS_QTY,MRR_QTY,IRR_QTY,ITC_QTY,OUTSALES_QTY,ROTARY_QTY,LOANRP_QTY,RTN_QTY, OP_BAL+OP_QC_PASS_QTY+OP_MRR_QTY+OP_IRR_QTY-OP_ITC_QTY-OP_OUTSALES_QTY-OP_ROTARY_QTY-OP_LOANRP_QTY-OP_RTN_QTY+OP_ADJ+QC_PASS_QTY+MRR_QTY+IRR_QTY-ITC_QTY-OUTSALES_QTY-ROTARY_QTY-LOANRP_QTY-RTN_QTY+ADJUST_QTY COLISING_QTY, BAL_QTY,QC_REJECT_QTY,OP_QC_RETURN_QTY+QC_RETURN_QTY-RTN_QTY-OP_RTN_QTY QC_RETURN_QTY ,ADJUST_QTY, ITEM_RATE,STORE_ID,STORE_NAME,PENDING_INDT_QTY,PENDING_PURCHASE_QTY,PENDING_MRR_QTY,REQ_PENDING_QTY,INDT_QTY FROM TEMP_STORE_ITEM_STOCK_WITH_PR ");
                //OP_BAL,OP_MRR_QTY,OP_IRR_QTY,OP_ITC_QTY,OP_OUTSALES_QTY,OP_ROTARY_QTY,OP_LOANRP_QTY,OP_RTN_QTY,OP_ADJ

                DBQuery dbqtemp = new DBQuery();
                //DBQuery dbq = new DBQuery();


                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                //DBQuery.ExecuteDBQuerySP(dbq, dc);
                DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);

                //,,,,,,,,,,,,,,,,,


                foreach (DataRow dRow in dtData.Rows)
                {
                    rcMaterialStock stk = new rcMaterialStock();

                    stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    stk.ITEM_CODE = dRow["ITEM_CODE"].ToString();

                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();

                    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);

                    stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                    stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();

                    //here all possible transaction for calculation opening balance
                    stk.OP_BAL = Conversion.DBNullDecimalToZero(dRow["OP_BAL"]);
                    stk.OP_MRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_MRR_QTY"]);
                    stk.OP_QC_PASS_QTY = Conversion.DBNullDecimalToZero(dRow["OP_QC_PASS_QTY"]);
                    stk.OP_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_BAL"]);
                    stk.OP_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_IRR_QTY"]);
                    stk.OP_OUTSALES_QTY = Conversion.DBNullDecimalToZero(dRow["OP_OUTSALES_QTY"]);
                    stk.OP_ROTARY_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ROTARY_QTY"]);
                    stk.OP_LOANRP_QTY = Conversion.DBNullDecimalToZero(dRow["OP_LOANRP_QTY"]);
                    stk.OP_RTN_QTY = Conversion.DBNullDecimalToZero(dRow["OP_RTN_QTY"]);
                    stk.OP_ADJ = Conversion.DBNullDecimalToZero(dRow["OP_ADJ"]);
                    stk.STORE_ID = Conversion.DBNullIntToZero(dRow["STORE_ID"]);
                    stk.STORE_NAME = dRow["STORE_NAME"].ToString();


                    stk.OPPENING_BAL_QTY = stk.OP_BAL + stk.OP_QC_PASS_QTY + stk.OP_MRR_QTY + stk.OP_IRR_QTY - stk.OP_ITC_QTY - stk.OP_OUTSALES_QTY - stk.OP_ROTARY_QTY - stk.OP_LOANRP_QTY - stk.OP_RTN_QTY + stk.OP_ADJ;

                    stk.OPPENING_BAL_QTY = Conversion.DBNullDecimalToZero(dRow["OPPENING_BAL_QTY"]);
                    stk.MRR_QTY = Conversion.DBNullDecimalToZero(dRow["MRR_QTY"]);
                    stk.QC_PASS_QTY = Conversion.DBNullDecimalToZero(dRow["QC_PASS_QTY"]);
                    stk.IRR_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_QTY"]);

                    stk.ITC_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_QTY"]);
                    stk.OUTSALES_QTY = Conversion.DBNullDecimalToZero(dRow["OUTSALES_QTY"]);

                    stk.ROTARY_QTY = Conversion.DBNullDecimalToZero(dRow["ROTARY_QTY"]);
                    stk.LOANRP_QTY = Conversion.DBNullDecimalToZero(dRow["LOANRP_QTY"]);
                    stk.RTN_QTY = Conversion.DBNullDecimalToZero(dRow["RTN_QTY"]);
                    stk.BAL_QTY = Conversion.DBNullDecimalToZero(dRow["BAL_QTY"]);
                    stk.COLISING_QTY = Conversion.DBNullDecimalToZero(dRow["COLISING_QTY"]);

                    stk.QC_RETURN_QTY = Conversion.DBNullDecimalToZero(dRow["QC_RETURN_QTY"]);
                    stk.QC_REJECT_QTY = Conversion.DBNullDecimalToZero(dRow["QC_REJECT_QTY"]);
                    stk.ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);

                    stk.ITEM_RATE = Conversion.DBNullDecimalToZero(dRow["ITEM_RATE"]);

                    stk.INDT_QTY = Conversion.DBNullDecimalToZero(dRow["INDT_QTY"]);
                    if (Conversion.DBNullDecimalToZero(dRow["PENDING_INDT_QTY"]) > 0)
                    stk.PENDING_INDT_QTY = Conversion.DBNullDecimalToZero(dRow["PENDING_INDT_QTY"]);

                    if (Conversion.DBNullDecimalToZero(dRow["PENDING_PURCHASE_QTY"]) > 0)
                    stk.PENDING_PURCHASE_QTY = Conversion.DBNullDecimalToZero(dRow["PENDING_PURCHASE_QTY"]);

                    if (Conversion.DBNullDecimalToZero(dRow["PENDING_MRR_QTY"]) > 0)
                    stk.PENDING_MRR_QTY = Conversion.DBNullDecimalToZero(dRow["PENDING_MRR_QTY"]);

                    if (Conversion.DBNullDecimalToZero(dRow["REQ_PENDING_QTY"]) > 0)
                    stk.REQ_PENDING_QTY = Conversion.DBNullDecimalToZero(dRow["REQ_PENDING_QTY"]);

                    stk.TOTAL_PR_PENDING_QTY = stk.PENDING_INDT_QTY + stk.PENDING_PURCHASE_QTY + stk.PENDING_MRR_QTY;

                    stk.COLISING_VALUE = (stk.COLISING_QTY * stk.ITEM_RATE);

                    //if (stk.OPPENING_BAL_QTY > 0 || stk.COLISING_VALUE > 0 || stk.COLISING_QTY > 0 || stk.ROTARY_QTY > 0 || stk.BAL_QTY > 0 || stk.ITC_QTY > 0 || stk.QC_PASS_QTY > 0 || stk.MRR_QTY > 0 || stk.IRR_QTY > 0)
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

        #region Stock_Evaluation


        public static List<rcMaterialStock> StockEvaluationReport(clsPrmInventory prmINV)
        {
            return StockEvaluationReport(prmINV, null);
        }

        public static List<rcMaterialStock> StockEvaluationReport(clsPrmInventory prmINV, DBContext dc)
        {

            List<rcMaterialStock> cObjList = new List<rcMaterialStock>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(StockEvaluation_SQLString());

                if (prmINV.FromDate != null && prmINV.ToDate != null)
                {
                    sb.Append(" AND stk.TRANS_DATE BETWEEN @P_FROM_DATE AND @P_TO_DATE ");
                    cmdInfo.DBParametersInfo.Add("@P_FROM_DATE", prmINV.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add("@P_TO_DATE", prmINV.ToDate.Value);
                }

                if (prmINV.itemGroup_id>0)
                {
                    sb.Append(" AND ig.ITEM_GROUP_ID=@P_ITEM_GROUP_ID ");
                    cmdInfo.DBParametersInfo.Add("@P_ITEM_GROUP_ID", prmINV.itemGroup_id);
                }

                if (prmINV.item_id > 0)
                {
                    sb.Append(" AND im.ITEM_ID=@P_ITEM_ID ");
                    cmdInfo.DBParametersInfo.Add("@P_ITEM_ID", prmINV.item_id);
                }              

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                cObjList = DBQuery.ExecuteDBQuery<rcMaterialStock>(dbq, dc).ToList();
              
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        #endregion

        public static Byte[] Get_ItemStock_Details_ExcelData(clsPrmInventory prmINV, bool pExecuteSP)
        {
            return Get_ItemStock_Details_ExcelData(prmINV, pExecuteSP, null);
        }

        public static Byte[] Get_ItemStock_Details_ExcelData(clsPrmInventory prmINV, bool pExecuteSP, DBContext dc)
        {

            bool isDCInit = false;
         
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();
                cmdInfo.DBParametersInfo.Clear();

                if (prmINV.ToDate.HasValue)
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.ToDate.Value);
                }

                cmdInfo.DBParametersInfo.Add(":p_itemtype_id", prmINV.itemtype_id);
                cmdInfo.DBParametersInfo.Add(":p_itemGroup_id", prmINV.itemGroup_id);

                cmdInfo.DBParametersInfo.Add(":p_item_id", prmINV.item_id);
                cmdInfo.DBParametersInfo.Add(":p_store_id", prmINV.store_id);
                cmdInfo.DBParametersInfo.Add(":p_itemclass_id", prmINV.ItemClassId);
                cmdInfo.DBParametersInfo.Add(":p_ItemCategoryID", prmINV.ItemCategoryID);
                cmdInfo.DBParametersInfo.Add(":p_ItemSizeID", prmINV.ItemSizeID);
                cmdInfo.DBParametersInfo.Add(":p_ItemColorID", prmINV.ItemColorID);
                cmdInfo.DBParametersInfo.Add(":p_ItemBrandID", prmINV.ItemBrandID);
                cmdInfo.DBParametersInfo.Add(":p_battery_type_id", prmINV.Battery_Type_ID);
                cmdInfo.DBParametersInfo.Add(":p_is_repair", prmINV.Is_Repair);
                cmdInfo.DBParametersInfo.Add(":p_item_sns_id", prmINV.SNS_Type);

              

               
                

                DBQuery dbq = new DBQuery();

                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;
                cmdInfo.CommandText = "SP_STORE_ITEM_STOCK_NEW";
                
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);

                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;
                //sb.Append(" Select ITEM_GROUP_ID,ITEM_GROUP_NAME,ITEM_CODE,ITEM_ID,ITEM_NAME,UOM_ID,UOM_NAME,ITEM_TYPE_ID,ITEM_TYPE_NAME,ITEM_CLASS_ID,ITEM_CLASS_NAME,OP_BAL,OP_QC_PASS_QTY,OP_MRR_QTY,OP_IRR_QTY,OP_ITC_QTY,OP_OUTSALES_QTY,OP_ROTARY_QTY,OP_LOANRP_QTY,OP_RTN_QTY,OP_ADJ,OP_BAL+OP_QC_PASS_QTY+OP_MRR_QTY+OP_IRR_QTY-OP_ITC_QTY-OP_OUTSALES_QTY-OP_ROTARY_QTY-OP_LOANRP_QTY-OP_RTN_QTY+OP_ADJ OPPENING_BAL_QTY,QC_PASS_QTY,MRR_QTY,IRR_QTY,ITC_QTY,OUTSALES_QTY,ROTARY_QTY,LOANRP_QTY,RTN_QTY, OP_BAL+OP_QC_PASS_QTY+OP_MRR_QTY+OP_IRR_QTY-OP_ITC_QTY-OP_OUTSALES_QTY-OP_ROTARY_QTY-OP_LOANRP_QTY-OP_RTN_QTY+OP_ADJ+QC_PASS_QTY+MRR_QTY+IRR_QTY-ITC_QTY-OUTSALES_QTY-ROTARY_QTY-LOANRP_QTY-RTN_QTY+ADJUST_QTY COLISING_QTY, BAL_QTY,QC_REJECT_QTY,OP_QC_RETURN_QTY+QC_RETURN_QTY-RTN_QTY-OP_RTN_QTY QC_RETURN_QTY ,ADJUST_QTY, ITEM_RATE,STORE_ID,STORE_NAME FROM TEMP_STORE_ITEM_STOCK");
                sb.Append(" Select ITEM_GROUP_ID,ITEM_GROUP_NAME,ITEM_CODE,ITEM_ID,ITEM_NAME,UOM_ID,UOM_NAME,ITEM_TYPE_ID,ITEM_TYPE_NAME,ITEM_CLASS_ID,ITEM_CLASS_NAME,OP_BAL,OP_QC_PASS_QTY,OP_MRR_QTY,OP_IRR_QTY,OP_ITC_QTY,OP_OUTSALES_QTY,OP_ROTARY_QTY,OP_LOANRP_QTY,OP_RTN_QTY,OP_ADJ,OP_BAL+OP_QC_PASS_QTY+OP_MRR_QTY+OP_IRR_QTY-OP_ITC_QTY-OP_OUTSALES_QTY-OP_ROTARY_QTY-OP_LOANRP_QTY+OP_ADJ OPPENING_BAL_QTY,QC_PASS_QTY,MRR_QTY,IRR_QTY,ITC_QTY,OUTSALES_QTY,ROTARY_QTY,LOANRP_QTY,RTN_QTY, OP_BAL+OP_QC_PASS_QTY+OP_MRR_QTY+OP_IRR_QTY-OP_ITC_QTY-OP_OUTSALES_QTY-OP_ROTARY_QTY-OP_LOANRP_QTY+OP_ADJ+QC_PASS_QTY+MRR_QTY+IRR_QTY-ITC_QTY-OUTSALES_QTY-ROTARY_QTY-LOANRP_QTY+ADJUST_QTY COLISING_QTY, BAL_QTY,QC_REJECT_QTY,OP_QC_RETURN_QTY+QC_RETURN_QTY-RTN_QTY-OP_RTN_QTY QC_RETURN_QTY ,ADJUST_QTY, ITEM_RATE,STORE_ID,STORE_NAME,CASE WHEN ITEM_TYPE_ID=1 THEN GET_PUR_PRICE_BY_TYPE(ITEM_ID,ITEM_TYPE_ID)WHEN ITEM_TYPE_ID=2 THEN GET_LC_COSTING_PRICE(ITEM_ID,ITEM_TYPE_ID) END PUR_RATE FROM TEMP_STORE_ITEM_STOCK");
                sb.Append(" ORDER BY STORE_ID,ITEM_GROUP_NAME,ITEM_CODE ");

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
                workSheet.Cells[1, colNum].Value = "Store";

                colNum = 3;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Item Group";

                colNum = 4;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Item Code";

                colNum = 5;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Item Name";

                colNum = 6;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "UOM";

                colNum = 7;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Item Type";


                colNum = 8;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Openning Balance";

                colNum = 9;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Purchase Quantity";
                colNum = 10;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Total Receive";
                colNum = 11;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Receive + Openning";
                colNum = 12;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "ITC";

                colNum = 13;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Total Issue";

                colNum = 14;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Adjust Qty";

                colNum = 15;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Return Qty";
                colNum = 16;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Reject Qty";

                colNum = 17;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Closing Balance";

                if(prmINV.withPrice == "Y")
                {
                    colNum = 18;
                    workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[1, colNum].Value = "Avg. Price";

                    colNum = 19;
                    workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[1, colNum].Value = "Last Pur. Price";


                }
               

               


                //Body of table  

                int recordIndex = 2;
                int slno = 0;
                foreach (DataRow dRow in dtData.Rows)
                {
                    slno++;
                    workSheet.Cells[recordIndex, 1].Value = slno;
                    workSheet.Cells[recordIndex, 2].Value = dRow["STORE_NAME"].ToString();
                    workSheet.Cells[recordIndex, 3].Value = dRow["ITEM_GROUP_NAME"].ToString();
                    workSheet.Cells[recordIndex, 4].Value = dRow["ITEM_CODE"].ToString();
                    workSheet.Cells[recordIndex, 5].Value = dRow["ITEM_NAME"].ToString();
                    workSheet.Cells[recordIndex, 6].Value = dRow["UOM_NAME"].ToString();
                    workSheet.Cells[recordIndex, 7].Value = dRow["ITEM_TYPE_NAME"].ToString();

                    workSheet.Cells[recordIndex, 8].Value = Conversion.DBNullDecimalToZero(dRow["OPPENING_BAL_QTY"]);
                    workSheet.Cells[recordIndex, 9].Value = Conversion.DBNullDecimalToZero(dRow["QC_PASS_QTY"]);
                    workSheet.Cells[recordIndex, 10].Value = Conversion.DBNullDecimalToZero(dRow["MRR_QTY"]) + Conversion.DBNullDecimalToZero(dRow["QC_PASS_QTY"]) + Conversion.DBNullDecimalToZero(dRow["IRR_QTY"]);
                    workSheet.Cells[recordIndex, 11].Value = Conversion.DBNullDecimalToZero(dRow["OPPENING_BAL_QTY"]) + Conversion.DBNullDecimalToZero(dRow["MRR_QTY"]) + Conversion.DBNullDecimalToZero(dRow["QC_PASS_QTY"]) + Conversion.DBNullDecimalToZero(dRow["IRR_QTY"]);

                    workSheet.Cells[recordIndex, 12].Value = Conversion.DBNullDecimalToZero(dRow["ITC_QTY"]);
                    workSheet.Cells[recordIndex, 13].Value = Conversion.DBNullDecimalToZero(dRow["ITC_QTY"]);
                    workSheet.Cells[recordIndex, 14].Value = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);
                    workSheet.Cells[recordIndex, 15].Value = Conversion.DBNullDecimalToZero(dRow["QC_RETURN_QTY"]);
                    workSheet.Cells[recordIndex, 16].Value = Conversion.DBNullDecimalToZero(dRow["QC_REJECT_QTY"]);
                    workSheet.Cells[recordIndex, 17].Value = Conversion.DBNullDecimalToZero(dRow["COLISING_QTY"]);
                    if(prmINV.withPrice == "Y")
                    {
                        workSheet.Cells[recordIndex, 18].Value = Conversion.DBNullDecimalToZero(dRow["ITEM_RATE"]);
                        workSheet.Cells[recordIndex, 19].Value = Conversion.DBNullDecimalToZero(dRow["PUR_RATE"]);
                    }


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
                workSheet.Column(16).AutoFit();
                workSheet.Column(17).AutoFit();
                if(prmINV.withPrice =="Y")
                {
                    workSheet.Column(18).AutoFit();
                    workSheet.Column(19).AutoFit();
                }


                bytes = excel.GetAsByteArray();

            }

            return bytes;

        }

        public static Byte[] Get_DeptStock_ExcelData(clsPrmInventory prmINV, bool pExecuteSP, DBContext dc)
        {

            List<rcMaterialStock> cRptList = new List<rcMaterialStock>();


            bool isDCInit = false;
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();

                if (prmINV.ToDate.HasValue)
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.ToDate.Value);
                }
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
                //dbq.CommandTimeout = 1000;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = "SP_DEPARTMENT_STOCK_REPORT";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);


                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;
                //sb.Append(" SELECT * FROM TEMP_ITEM_STOCK_BAT_FOR_NET ");

                sb.Append(" Select ITEM_GROUP_ID,ITEM_GROUP_NAME,ITEM_ID,SND_ITEM_CODE,ITEM_CODE,ITEM_NAME,UOM_ID,UOM_NAME,ITEM_TYPE_ID,ITEM_TYPE_NAME,ITEM_CLASS_ID,ITEM_CLASS_NAME,OP_IRR_QTY,OP_ITC_QTY,IRR_DEPT_QTY,IRR_STORE_QTY,IRR_PROD_QTY,ITC_DEPT_QTY,ITC_STORE_QTY,ITC_PROD_QTY,ADJUST_QTY,REJECT_QTY,OP_REJECT_QTY,GRID_REJECT_QTY,OP_GRID_REJECT_QTY,STLM_ID,STLM_NAME,PROCESS_LOSS_QTY,CONV_LEAD_QTY FROM TEMP_DEPARTMENT_ITEM_STOCK");
                sb.Append(" ORDER BY STLM_ID,ITEM_CODE ");

                DBQuery dbqtemp = new DBQuery();
                //DBQuery dbq = new DBQuery();


                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                //DBQuery.ExecuteDBQuerySP(dbq, dc);
                DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);

                //,,,,,,,,,,,,,,,,,


                foreach (DataRow dRow in dtData.Rows)
                {
                    rcMaterialStock stk = new rcMaterialStock();
                    stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();
                    stk.ITEM_CODE = dRow["ITEM_CODE"].ToString();
                    stk.SND_ITEM_CODE = dRow["SND_ITEM_CODE"].ToString();
                    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);

                    stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                    stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();
                    stk.OP_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_IRR_QTY"]);
                    stk.OP_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ITC_QTY"]);
                    stk.OP_REJECT_QTY = Conversion.DBNullDecimalToZero(dRow["OP_REJECT_QTY"]);
                    stk.OP_GRID_REJECT_QTY = Conversion.DBNullDecimalToZero(dRow["OP_GRID_REJECT_QTY"]);
                    stk.OPPENING_BAL_QTY = stk.OP_IRR_QTY - stk.OP_ITC_QTY - stk.OP_REJECT_QTY - stk.OP_GRID_REJECT_QTY;

                    stk.IRR_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_DEPT_QTY"]);
                    stk.IRR_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_STORE_QTY"]);
                    stk.IRR_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_PROD_QTY"]);
                    stk.CONV_LEAD_QTY = Conversion.DBNullDecimalToZero(dRow["CONV_LEAD_QTY"]);

                    stk.REJECT_QTY = Conversion.DBNullDecimalToZero(dRow["REJECT_QTY"]);
                    stk.GRID_REJECT_QTY = Conversion.DBNullDecimalToZero(dRow["GRID_REJECT_QTY"]);

                    stk.PROCESS_LOSS_QTY = Conversion.DBNullDecimalToZero(dRow["PROCESS_LOSS_QTY"]);


                    stk.IRR_BAL_QTY = stk.IRR_DEPT_QTY + stk.IRR_STORE_QTY + stk.IRR_PROD_QTY + stk.CONV_LEAD_QTY;

                    stk.ITC_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_DEPT_QTY"]);
                    stk.ITC_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_STORE_QTY"]);
                    stk.ITC_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_PROD_QTY"]);

                    //here total ITC quantity
                    stk.ITC_BAL_QTY = stk.ITC_DEPT_QTY + stk.ITC_STORE_QTY + stk.ITC_PROD_QTY;

                    //here total Adjust balance quantity
                    stk.ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);
                    stk.STLM_ID = Conversion.DBNullIntToZero(dRow["STLM_ID"]);
                    stk.STLM_NAME = dRow["STLM_NAME"].ToString();
                    if(stk.STLM_NAME == "")
                    {
                        stk.STLM_NAME = "Department";
                    }


                    stk.DEPARTMENT_NAME = prmINV.From_Dept_Name;
                    // stk.BAL_QTY = Conversion.DBNullDecimalToZero(dRow["BAL_QTY"]);
                    stk.CLOSING_QTY = (stk.OPPENING_BAL_QTY + stk.ADJUST_QTY + stk.IRR_BAL_QTY) - stk.ITC_BAL_QTY - stk.REJECT_QTY - stk.GRID_REJECT_QTY;

                    if (stk.GRID_REJECT_QTY > 0 && stk.REJECT_QTY == 0)
                    {
                        stk.REJECT_QTY = stk.GRID_REJECT_QTY;
                    }

                    if (stk.OPPENING_BAL_QTY == 0)
                    {
                        if (stk.IRR_BAL_QTY != 0 || stk.ITC_BAL_QTY != 0 || stk.CLOSING_QTY != 0)
                        {
                            cRptList.Add(stk);
                        }
                    }
                    else
                    {
                        cRptList.Add(stk);
                    }
                }

                //int COUNT = cRptList.Count;


            }
          
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
                workSheet.Cells[1, colNum].Value = "Department";

                colNum = 3;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Storage Location";

                colNum = 4;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Item Code";

                colNum = 5;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Sales Item Code";

                colNum = 6;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Item Name";

                colNum = 7;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "UOM";

                colNum = 8;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Opening Qty";


                colNum = 9;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Rcv. From Dept.";

                colNum = 10;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Rcv. From Store";

                colNum = 11;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Rcv. From Prod.";

                colNum = 12;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Conv.Lead Rcv";

                colNum = 13;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Total Receive";

                colNum = 14;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Issue To Dept.";

                colNum = 15;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Issue To Store";

                colNum = 16;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Issue To Prod.";

                colNum = 17;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Total Issue";
                colNum = 18;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Adjust Qty";

                colNum = 19;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Reject Qty";

                colNum = 20;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Process Loss";

                colNum = 21;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Closing Qty";
                

                //Body of table  

                int recordIndex = 2;
                int slno = 0;
                foreach (rcMaterialStock dRow in cRptList)
                {
                    slno++;
                    workSheet.Cells[recordIndex, 1].Value = slno;
                    workSheet.Cells[recordIndex, 2].Value = dRow.DEPARTMENT_NAME.ToString();
                    workSheet.Cells[recordIndex, 3].Value = dRow.STLM_NAME.ToString();
                    workSheet.Cells[recordIndex, 4].Value = dRow.ITEM_CODE.ToString();
                    workSheet.Cells[recordIndex, 5].Value = dRow.SND_ITEM_CODE.ToString();
                    workSheet.Cells[recordIndex, 6].Value = dRow.ITEM_NAME.ToString();
                    workSheet.Cells[recordIndex, 7].Value = dRow.UOM_NAME.ToString();
                    workSheet.Cells[recordIndex, 8].Value =Math.Round( Conversion.DBNullDecimalToZero(dRow.OPPENING_BAL_QTY),2);
                    workSheet.Cells[recordIndex, 9].Value = Math.Round(Conversion.DBNullDecimalToZero(dRow.IRR_DEPT_QTY),2);
                    workSheet.Cells[recordIndex, 10].Value = Math.Round(Conversion.DBNullDecimalToZero(dRow.IRR_STORE_QTY),2);
                    workSheet.Cells[recordIndex, 11].Value = Math.Round(Conversion.DBNullDecimalToZero(dRow.IRR_PROD_QTY),2);
                    workSheet.Cells[recordIndex, 12].Value = Math.Round(Conversion.DBNullDecimalToZero(dRow.CONV_LEAD_QTY),2);
                    workSheet.Cells[recordIndex, 13].Value = Math.Round(Conversion.DBNullDecimalToZero(dRow.IRR_BAL_QTY),2); 
                    workSheet.Cells[recordIndex, 14].Value = Math.Round(Conversion.DBNullDecimalToZero(dRow.ITC_DEPT_QTY),2);

                    workSheet.Cells[recordIndex, 15].Value = Math.Round(Conversion.DBNullDecimalToZero(dRow.ITC_STORE_QTY),2);
                    workSheet.Cells[recordIndex, 16].Value = Math.Round(Conversion.DBNullDecimalToZero(dRow.ITC_PROD_QTY),2);
                    workSheet.Cells[recordIndex, 17].Value = Math.Round(Conversion.DBNullDecimalToZero(dRow.ITC_BAL_QTY),2);
                    workSheet.Cells[recordIndex, 18].Value = Math.Round(Conversion.DBNullDecimalToZero(dRow.ADJUST_QTY),2);
                    workSheet.Cells[recordIndex, 19].Value = Math.Round(Conversion.DBNullDecimalToZero(dRow.REJECT_QTY),2);
                    workSheet.Cells[recordIndex, 20].Value = Math.Round(Conversion.DBNullDecimalToZero(dRow.PROCESS_LOSS_QTY),2);
                    workSheet.Cells[recordIndex, 21].Value = Math.Round(Conversion.DBNullDecimalToZero(dRow.CLOSING_QTY), 2);


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
                workSheet.Column(16).AutoFit();
                workSheet.Column(17).AutoFit();
                workSheet.Column(18).AutoFit();
                workSheet.Column(19).AutoFit();
                workSheet.Column(20).AutoFit();
                workSheet.Column(21).AutoFit();


                bytes = excel.GetAsByteArray();

            }

            return bytes;

        }

        public static Byte[] Get_DeptRejectStock_ExcelData(clsPrmInventory prmINV, bool pExecuteSP, DBContext dc)
        {

            List<rcMaterialStock> cRptList = new List<rcMaterialStock>();


            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();

                if (prmINV.ToDate.HasValue)
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.ToDate.Value);
                }
                cmdInfo.DBParametersInfo.Add(":p_itemGroup_id", prmINV.itemGroup_id);
                cmdInfo.DBParametersInfo.Add(":p_item_id", prmINV.item_id);
                cmdInfo.DBParametersInfo.Add(":p_itemclass_id", prmINV.ItemClassId);
                cmdInfo.DBParametersInfo.Add(":p_dept_id", prmINV.DeptID);
                cmdInfo.DBParametersInfo.Add(":p_stlm_id", prmINV.STLM_ID);

                DBQuery dbq = new DBQuery();

                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;


                cmdInfo.CommandText = "SP_DEPT_REJECT_REPORT";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);


                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;

                sb.Append(" SELECT * FROM TEMP_REJECT_ITEM_STOCK ");
                sb.Append(" ORDER BY STLM_ID,ITEM_CODE ");


                DBQuery dbqtemp = new DBQuery();


                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);

                //,,,,,,,,,,,,,,,,,


                //foreach (DataRow dRow in dtData.Rows)
                //{
                //    rcMaterialStock stk = new rcMaterialStock();
                //    stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                //    stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                //    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                //    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();
                //    stk.ITEM_CODE = dRow["ITEM_CODE"].ToString();

                //    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                //    stk.UOM_NAME = dRow["UOM_NAME"].ToString();

                //    stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                //    stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();
                //    //opening receive
                //    stk.OP_GRID_REJ_QTY = Conversion.DBNullDecimalToZero(dRow["OP_GRID_REJ_QTY"]);
                //    stk.OP_PROD_REJ_RCV_QTY = Conversion.DBNullDecimalToZero(dRow["OP_PROD_REJ_RCV_QTY"]);
                //    stk.OP_REJ_TRAN_RCV_QTY = Conversion.DBNullDecimalToZero(dRow["OP_REJ_TRAN_RCV_QTY"]);
                //    stk.OP_REJ_RCV_WITH_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["OP_REJ_RCV_WITH_PROD_QTY"]);
                //    // opening Issue
                //    stk.OP_REJ_TRAN_ISS_QTY = Conversion.DBNullDecimalToZero(dRow["OP_REJ_TRAN_ISS_QTY"]);
                //    stk.OP_REJ_ISSUE_FROM_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["OP_REJ_ISSUE_FROM_DEPT_QTY"]);

                //    stk.OPPENING_BAL_QTY = (stk.OP_GRID_REJ_QTY + stk.OP_PROD_REJ_RCV_QTY + stk.OP_REJ_TRAN_RCV_QTY + stk.OP_REJ_RCV_WITH_PROD_QTY) - stk.OP_REJ_TRAN_ISS_QTY - stk.OP_REJ_ISSUE_FROM_DEPT_QTY;

                //    stk.GRID_REJ_QTY = Conversion.DBNullDecimalToZero(dRow["GRID_REJ_QTY"]);
                //    stk.PROD_REJ_RCV_QTY = Conversion.DBNullDecimalToZero(dRow["PROD_REJ_RCV_QTY"]);
                //    stk.REJ_TRAN_RCV_QTY = Conversion.DBNullDecimalToZero(dRow["REJ_TRAN_RCV_QTY"]);
                //    stk.REJ_RCV_WITH_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["REJ_RCV_WITH_PROD_QTY"]);

                //    stk.REJ_TRAN_ISS_QTY = Conversion.DBNullDecimalToZero(dRow["REJ_TRAN_ISS_QTY"]);
                //    stk.REJ_ISSUE_FROM_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["REJ_ISSUE_FROM_DEPT_QTY"]);


                //    //here total IRR quantity
                //    stk.TOTAL_PROD_REJ_RCV = stk.GRID_REJ_QTY + stk.PROD_REJ_RCV_QTY + stk.REJ_RCV_WITH_PROD_QTY;

                //    //here total Adjust balance quantity
                //    stk.ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);
                //    stk.STLM_ID = Conversion.DBNullIntToZero(dRow["STLM_ID"]);
                //    stk.STLM_NAME = dRow["STLM_NAME"].ToString();
                //    if(stk.STLM_NAME == "")
                //    {
                //        stk.STLM_NAME = "Department";
                //    }


                //    stk.DEPARTMENT_NAME = prmINV.From_Dept_Name;

                //    stk.CLOSING_QTY = (stk.OPPENING_BAL_QTY + stk.ADJUST_QTY + stk.TOTAL_PROD_REJ_RCV + stk.REJ_TRAN_RCV_QTY) - stk.REJ_TRAN_ISS_QTY - stk.REJ_ISSUE_FROM_DEPT_QTY;

                //    if (stk.OPPENING_BAL_QTY == 0)
                //    {
                //        if (stk.TOTAL_PROD_REJ_RCV != 0 || stk.REJ_TRAN_ISS_QTY != 0 || stk.REJ_ISSUE_FROM_DEPT_QTY != 0 || stk.CLOSING_QTY != 0)
                //        {
                //            cRptList.Add(stk);
                //        }
                //    }
                //    else
                //    {
                //        cRptList.Add(stk);
                //    }
                //}

                foreach (DataRow dRow in dtData.Rows)
                {
                    rcMaterialStock stk = new rcMaterialStock();
                    stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();
                    stk.ITEM_CODE = dRow["ITEM_CODE"].ToString();

                    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();

                    stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();
                    //opening receive
                    stk.OP_GRID_REJ_QTY = Conversion.DBNullDecimalToZero(dRow["OP_GRID_REJ_QTY"]);
                    stk.OP_PROD_REJ_RCV_QTY = Conversion.DBNullDecimalToZero(dRow["OP_PROD_REJ_RCV_QTY"]);
                    stk.OP_REJ_TRAN_RCV_QTY = Conversion.DBNullDecimalToZero(dRow["OP_REJ_TRAN_RCV_QTY"]);
                    stk.OP_REJ_RCV_WITH_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["OP_REJ_RCV_WITH_PROD_QTY"]);
                    // opening Issue
                    stk.OP_REJ_TRAN_ISS_QTY = Conversion.DBNullDecimalToZero(dRow["OP_REJ_TRAN_ISS_QTY"]);
                    stk.OP_REJ_ISSUE_FROM_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["OP_REJ_ISSUE_FROM_DEPT_QTY"]);

                    stk.OP_ISSUE_FOR_PRODUCTION = Conversion.DBNullDecimalToZero(dRow["OP_ISSUE_FOR_PRODUCTION"]);
                    stk.OP_ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ADJUST_QTY"]);

                    stk.OPPENING_BAL_QTY = (stk.OP_GRID_REJ_QTY + stk.OP_PROD_REJ_RCV_QTY + stk.OP_REJ_TRAN_RCV_QTY + stk.OP_REJ_RCV_WITH_PROD_QTY + stk.OP_ADJUST_QTY) - stk.OP_REJ_TRAN_ISS_QTY - stk.OP_REJ_ISSUE_FROM_DEPT_QTY - stk.OP_ISSUE_FOR_PRODUCTION;

                    stk.GRID_REJ_QTY = Conversion.DBNullDecimalToZero(dRow["GRID_REJ_QTY"]);
                    stk.PROD_REJ_RCV_QTY = Conversion.DBNullDecimalToZero(dRow["PROD_REJ_RCV_QTY"]);
                    stk.REJ_TRAN_RCV_QTY = Conversion.DBNullDecimalToZero(dRow["REJ_TRAN_RCV_QTY"]);
                    stk.REJ_RCV_WITH_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["REJ_RCV_WITH_PROD_QTY"]);

                    stk.REJ_TRAN_ISS_QTY = Conversion.DBNullDecimalToZero(dRow["REJ_TRAN_ISS_QTY"]);
                    stk.REJ_ISSUE_FROM_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["REJ_ISSUE_FROM_DEPT_QTY"]);
                    stk.ISSUE_TO_BREAKING = Conversion.DBNullDecimalToZero(dRow["ISSUE_TO_BREAKING"]);

                    stk.ISSUE_FOR_PRODUCTION = Conversion.DBNullDecimalToZero(dRow["ISSUE_FOR_PRODUCTION"]);

                    stk.ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);
                    //here total IRR quantity
                    stk.TOTAL_PROD_REJ_RCV = stk.GRID_REJ_QTY + stk.PROD_REJ_RCV_QTY + stk.REJ_RCV_WITH_PROD_QTY;

                    //here total Adjust balance quantity
                    stk.ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);
                    stk.STLM_ID = Conversion.DBNullIntToZero(dRow["STLM_ID"]);
                    stk.STLM_NAME = dRow["STLM_NAME"].ToString();
                    if (stk.STLM_NAME == "")
                    {
                        stk.STLM_NAME = "Department";
                    }

                    stk.DEPARTMENT_NAME = prmINV.From_Dept_Name;

                    stk.CLOSING_QTY = (stk.OPPENING_BAL_QTY + stk.ADJUST_QTY + stk.TOTAL_PROD_REJ_RCV + stk.REJ_TRAN_RCV_QTY) - stk.REJ_TRAN_ISS_QTY - stk.REJ_ISSUE_FROM_DEPT_QTY - stk.ISSUE_FOR_PRODUCTION - stk.ISSUE_TO_BREAKING;

                    if (stk.OPPENING_BAL_QTY == 0)
                    {
                        if (stk.TOTAL_PROD_REJ_RCV != 0 || stk.REJ_TRAN_ISS_QTY != 0 || stk.REJ_ISSUE_FROM_DEPT_QTY != 0 || stk.ISSUE_FOR_PRODUCTION != 0 || stk.COLISING_QTY != 0 || stk.REJ_TRAN_RCV_QTY != 0)
                        {
                            cRptList.Add(stk);
                        }
                    }
                    else
                    {
                        cRptList.Add(stk);
                    }
                }




            }

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
                workSheet.Cells[1, colNum].Value = "Department";

                colNum = 3;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Storage Location";

                colNum = 4;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Item Code";

                colNum = 5;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Item Name";

                colNum = 6;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "UOM";

                colNum = 7;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Opening Qty";


                colNum = 8;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Rcv. From Dept.";

              
                colNum = 9;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Rcv. From Prod.";
                colNum = 10;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Total Receive";
                colNum = 11;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Issue To Dept.";

                colNum = 12;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Issue To Store";

                colNum = 13;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Total Issue";
                colNum = 14;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Adjust Qty";

                colNum = 15;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Closing Qty";


                //Body of table  

                int recordIndex = 2;
                int slno = 0;
                foreach (rcMaterialStock dRow in cRptList)
                {
                    slno++;
                    workSheet.Cells[recordIndex, 1].Value = slno;
                    workSheet.Cells[recordIndex, 2].Value = dRow.DEPARTMENT_NAME.ToString();
                    workSheet.Cells[recordIndex, 3].Value = dRow.STLM_NAME.ToString();
                    workSheet.Cells[recordIndex, 4].Value = dRow.ITEM_CODE.ToString();
                    workSheet.Cells[recordIndex, 5].Value = dRow.ITEM_NAME.ToString();
                    workSheet.Cells[recordIndex, 6].Value = dRow.UOM_NAME.ToString();
                    workSheet.Cells[recordIndex, 7].Value = Conversion.DBNullDecimalToZero(dRow.OPPENING_BAL_QTY);
                    workSheet.Cells[recordIndex, 8].Value = Conversion.DBNullDecimalToZero(dRow.REJ_TRAN_RCV_QTY);
                    workSheet.Cells[recordIndex, 9].Value = Conversion.DBNullDecimalToZero(dRow.TOTAL_PROD_REJ_RCV);
                    workSheet.Cells[recordIndex, 10].Value = Conversion.DBNullDecimalToZero(dRow.TOTAL_PROD_REJ_RCV) + Conversion.DBNullDecimalToZero(dRow.REJ_TRAN_RCV_QTY);
                    workSheet.Cells[recordIndex, 11].Value = Conversion.DBNullDecimalToZero(dRow.REJ_TRAN_ISS_QTY);
                    workSheet.Cells[recordIndex, 12].Value = Conversion.DBNullDecimalToZero(dRow.REJ_ISSUE_FROM_DEPT_QTY);
                    workSheet.Cells[recordIndex, 13].Value = Conversion.DBNullDecimalToZero(dRow.REJ_ISSUE_FROM_DEPT_QTY) + Conversion.DBNullDecimalToZero(dRow.REJ_TRAN_ISS_QTY);
                    workSheet.Cells[recordIndex, 14].Value = Conversion.DBNullDecimalToZero(dRow.ADJUST_QTY);
                    workSheet.Cells[recordIndex, 15].Value = Conversion.DBNullDecimalToZero(dRow.CLOSING_QTY);
                  


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

        //Department Closing WIP

        public static Byte[] Get_DepartmentClosingWIP_ExcelData(clsPrmInventory prmINV, bool pExecuteSP, DBContext dc)
        {

            bool isDCInit = false;

            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

            DBCommandInfo cmdInfo = new DBCommandInfo();
            StringBuilder sb = new StringBuilder();
            cmdInfo.DBParametersInfo.Clear();

            DBQuery dbq = new DBQuery();

           

            DBCommandInfo cmdInfotemp = new DBCommandInfo();
            sb.Length = 0;
            sb.Append(" SELECT di.DEPARTMENT_NAME,a.INV_ADJUST_NO,a.INV_ADJUST_DATE,a.INV_ADJUST_DESC,im.ITEM_CODE,im.ITEM_NAME,um.UOM_NAME,b.CONSUMPTION_QTY,b.REJECT_QTY,b.BALANCE_QTY,b.TARGET_QTY,b.REMARKS_DTL  ");
            sb.Append(" FROM INV_ADJUST_DEPT_MASTER_TEMP a ");
            sb.Append(" INNER JOIN INV_ADJUST_DEPT_DETAILS_TEMP b ON a.INV_ADJUST_ID=b.INV_ADJUST_ID ");
            sb.Append(" INNER JOIN INV_ITEM_MASTER im ON b.ITEM_ID=im.ITEM_ID  ");
            sb.Append(" INNER JOIN UOM_INFO um ON im.UOM_ID=um.UOM_ID ");
            sb.Append(" INNER JOIN DEPARTMENT_INFO di ON a.DEPARTMENT_ID=di.DEPARTMENT_ID ");
            sb.Append(" WHERE 1=1 ");

            if (prmINV.ToDate.HasValue)
            {
                sb.Append(" AND a.INV_ADJUST_DATE BETWEEN :P_DATE_FROM and :P_DATE_TO ");
                cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.FromDate.Value);
                cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.ToDate.Value);
            }

            if(prmINV.DeptID>0)
            {
                sb.Append(" AND a.DEPARTMENT_ID = :DeptID ");
                cmdInfo.DBParametersInfo.Add(":DeptID", prmINV.DeptID);
            }

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
                workSheet.Cells[1, colNum].Value = "Department";

                colNum = 3;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Adjust No";

                colNum = 4;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Date";

                colNum = 5;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Remarks";

                colNum = 6;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Item code";

                colNum = 7;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Item Name";


                colNum = 8;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "UOM";

                colNum = 9;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "OP Balance Quantity";
                colNum = 10;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Consumption Quantity";
                colNum = 11;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Rejection Quantity";
                colNum = 12;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Closing Quantity";

                colNum = 13;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Details Remarks";



                //Body of table  

                int recordIndex = 2;
                int slno = 0;
                foreach (DataRow dRow in dtData.Rows)
                {
                    slno++;
                    workSheet.Cells[recordIndex, 1].Value = slno;
                    workSheet.Cells[recordIndex, 2].Value = dRow["DEPARTMENT_NAME"].ToString();
                    workSheet.Cells[recordIndex, 3].Value = dRow["INV_ADJUST_NO"].ToString();
                    workSheet.Cells[recordIndex, 4].Value = dRow["INV_ADJUST_DATE"].ToString();
                    workSheet.Cells[recordIndex, 5].Value = dRow["INV_ADJUST_DESC"].ToString();
                    workSheet.Cells[recordIndex, 6].Value = dRow["ITEM_CODE"].ToString();
                    workSheet.Cells[recordIndex, 7].Value = dRow["ITEM_NAME"].ToString();
                    workSheet.Cells[recordIndex, 8].Value = dRow["UOM_NAME"].ToString();

                    workSheet.Cells[recordIndex, 9].Value = Conversion.DBNullDecimalToZero(dRow["BALANCE_QTY"]);
                    workSheet.Cells[recordIndex, 10].Value = Conversion.DBNullDecimalToZero(dRow["CONSUMPTION_QTY"]) - Conversion.DBNullDecimalToZero(dRow["REJECT_QTY"]);
                    workSheet.Cells[recordIndex, 11].Value = Conversion.DBNullDecimalToZero(dRow["REJECT_QTY"]);
                    workSheet.Cells[recordIndex, 12].Value = Conversion.DBNullDecimalToZero(dRow["TARGET_QTY"]);
                    workSheet.Cells[recordIndex, 13].Value = dRow["REMARKS_DTL"].ToString();
                    
                    


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
                


                bytes = excel.GetAsByteArray();

            }

            return bytes;

        }

        //Grid Casting Deptartment Stock Report

        public static List<rcMaterialStock> GridCastingDepartmentStockReport(clsPrmInventory prmINV)
        {
            return GridCastingDepartmentStockReport(prmINV, null);
        }

        public static List<rcMaterialStock> GridCastingDepartmentStockReport(clsPrmInventory prmINV, DBContext dc)
        {

            List<rcMaterialStock> cRptList = new List<rcMaterialStock>();


            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();

                if (prmINV.ToDate.HasValue)
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.ToDate.Value);
                }
                cmdInfo.DBParametersInfo.Add(":p_itemtype_id", prmINV.itemtype_id);
                cmdInfo.DBParametersInfo.Add(":p_itemGroup_id", prmINV.itemGroup_id);
                cmdInfo.DBParametersInfo.Add(":p_item_id", prmINV.item_id);
                cmdInfo.DBParametersInfo.Add(":p_store_id", prmINV.store_id);
                cmdInfo.DBParametersInfo.Add(":p_itemclass_id", prmINV.ItemClassId);
                cmdInfo.DBParametersInfo.Add(":p_battery_type_id", prmINV.Battery_Type_ID);
                cmdInfo.DBParametersInfo.Add(":p_is_repair", prmINV.Is_Repair);
                cmdInfo.DBParametersInfo.Add(":p_dept_id", prmINV.DeptID);
                cmdInfo.DBParametersInfo.Add(":p_is_finished", prmINV.is_Finished);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;
                cmdInfo.CommandText = "SP_GRID_MONTHLY_STOCK_REPORT";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);
                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;

                //sb.Append(" Select dept.ORDER_NO, ITEM_GROUP_ID,ITEM_GROUP_NAME,ITEM_ID,ITEM_NAME,UOM_ID,CASE WHEN UOM_NAME_P IS NULL THEN UOM_NAME else UOM_NAME_P END UOM_NAME,ITEM_TYPE_ID,ITEM_TYPE_NAME,ITEM_CLASS_ID,ITEM_CLASS_NAME,OP_IRR_QTY,OP_ITC_QTY,IRR_DEPT_QTY,IRR_STORE_QTY,IRR_PROD_QTY,ITC_DEPT_QTY,ITC_STORE_QTY,ITC_PROD_QTY,ADJUST_QTY,ITEM_STANDARD_WEIGHT_KG,PANNEL_QTY FROM TEMP_DEPARTMENT_ITEM_STOCK LEFT JOIN PRO_DEPARTMENT_ITEM dept ON ITEM_ID=dept.ITEM_ID ");

                sb.Append(" Select dept.ORDER_NO, tstk.ITEM_GROUP_ID,tstk.ITEM_GROUP_NAME,tstk.ITEM_ID,tstk.ITEM_NAME,tstk.UOM_ID,CASE WHEN tstk.UOM_NAME_P IS NULL THEN tstk.UOM_NAME else tstk.UOM_NAME_P END UOM_NAME,tstk.ITEM_TYPE_ID,tstk.ITEM_TYPE_NAME,tstk.ITEM_CLASS_ID,tstk.ITEM_CLASS_NAME,tstk.OP_IRR_QTY,tstk.OP_ITC_QTY,tstk.IRR_DEPT_QTY,tstk.IRR_STORE_QTY,tstk.IRR_PROD_QTY,tstk.ITC_DEPT_QTY,tstk.ITC_STORE_QTY,tstk.ITC_PROD_QTY,tstk.ADJUST_QTY,tstk.ITEM_STANDARD_WEIGHT_KG,tstk.PANNEL_QTY,ISSUE_FOR_PRODUCTION,ISSUE_FOR_DROSS FROM TEMP_DEPARTMENT_ITEM_STOCK tstk ");
                sb.Append(" INNER JOIN PRO_DEPARTMENT_ITEM dept  ON tstk.ITEM_ID=dept.ITEM_ID AND dept.DEPT_ID=135 ");


                DBQuery dbqtemp = new DBQuery();

                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);

                foreach (DataRow dRow in dtData.Rows)
                {
                    rcMaterialStock stk = new rcMaterialStock();
                    stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();

                    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);

                    stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                    stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();
                    stk.OP_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_IRR_QTY"]);
                    stk.OP_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ITC_QTY"]);
                    stk.OPPENING_BAL_QTY = stk.OP_IRR_QTY - stk.OP_ITC_QTY;

                    stk.IRR_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_DEPT_QTY"]);
                    stk.IRR_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_STORE_QTY"]);
                    stk.IRR_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_PROD_QTY"]);

                    //here total IRR quantity
                    stk.IRR_BAL_QTY = stk.IRR_DEPT_QTY + stk.IRR_STORE_QTY + stk.IRR_PROD_QTY;

                    stk.ITC_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_DEPT_QTY"]);
                    stk.ITC_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_STORE_QTY"]);
                    stk.ITC_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_PROD_QTY"]);

                    //here total ITC quantity
                    stk.ITC_BAL_QTY = stk.ITC_DEPT_QTY + stk.ITC_STORE_QTY + stk.ITC_PROD_QTY;

                    //here total Adjust balance quantity
                    stk.ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);

                    stk.PANNEL_QTY = Conversion.DBNullDecimalToZero(dRow["PANNEL_QTY"]);

                     stk.ISSUE_FOR_PRODUCTION = Conversion.DBNullDecimalToZero(dRow["ISSUE_FOR_PRODUCTION"]);
                     stk.ISSURE_FOR_DROSS = Conversion.DBNullDecimalToZero(dRow["ISSUE_FOR_DROSS"]);

                    stk.ITEM_STANDARD_WEIGHT_KG = Conversion.DBNullDecimalToZero(dRow["ITEM_STANDARD_WEIGHT_KG"]);
                    stk.TOTAL_GRID_STD_WEIGHT_KG = Conversion.DBNullDecimalToZero(dRow["ITEM_STANDARD_WEIGHT_KG"]) * stk.IRR_BAL_QTY;
                    stk.ORDER_NO = Conversion.DBNullIntToZero(dRow["ORDER_NO"]);
                    stk.DEPARTMENT_NAME = prmINV.From_Dept_Name;

                    if (stk.ADJUST_QTY > 0)
                    {
                        stk.IRR_BAL_QTY = stk.IRR_BAL_QTY + stk.ADJUST_QTY;
                    }
                    else
                    {
                        stk.ITC_BAL_QTY = stk.ITC_BAL_QTY + Math.Abs(stk.ADJUST_QTY);
                    }


                    stk.COLISING_QTY = (stk.OPPENING_BAL_QTY + stk.IRR_BAL_QTY) - stk.ITC_BAL_QTY;
                    stk.CLOSING_QTY_wt = stk.COLISING_QTY * stk.ITEM_STANDARD_WEIGHT_KG;
                    //cRptList.Add(stk);

                    //if (stk.OPPENING_BAL_QTY == 0)
                    //{
                    if (stk.COLISING_QTY > 0 || stk.OPPENING_BAL_QTY > 0 || stk.ITC_BAL_QTY > 0 || stk.ISSUE_FOR_PRODUCTION > 0 || stk.IRR_BAL_QTY >0)
                        {
                            cRptList.Add(stk);
                        }
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


        //Plastic Deptartment Stock Report

        public static List<rcMaterialStock> PlasticDepartmentStockReport(clsPrmInventory prmINV)
        {
            return PlasticDepartmentStockReport(prmINV, null);
        }

        public static List<rcMaterialStock> PlasticDepartmentStockReport(clsPrmInventory prmINV, DBContext dc)
        {

            List<rcMaterialStock> cRptList = new List<rcMaterialStock>();


            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();

                if (prmINV.ToDate.HasValue)
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.ToDate.Value);
                }
                cmdInfo.DBParametersInfo.Add(":p_itemtype_id", prmINV.itemtype_id);
                cmdInfo.DBParametersInfo.Add(":p_itemGroup_id", prmINV.itemGroup_id);
                cmdInfo.DBParametersInfo.Add(":p_item_id", prmINV.item_id);
                cmdInfo.DBParametersInfo.Add(":p_store_id", prmINV.store_id);
                cmdInfo.DBParametersInfo.Add(":p_itemclass_id", prmINV.ItemClassId);
                cmdInfo.DBParametersInfo.Add(":p_battery_type_id", prmINV.Battery_Type_ID);
                cmdInfo.DBParametersInfo.Add(":p_is_repair", prmINV.Is_Repair);
                cmdInfo.DBParametersInfo.Add(":p_dept_id", prmINV.DeptID);
                cmdInfo.DBParametersInfo.Add(":p_is_finished", prmINV.is_Finished);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;
                cmdInfo.CommandText = "SP_PPL_MONTHLY_STOCK_REPORT";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);
                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;

                //sb.Append(" Select dept.ORDER_NO, ITEM_GROUP_ID,ITEM_GROUP_NAME,ITEM_ID,ITEM_NAME,UOM_ID,CASE WHEN UOM_NAME_P IS NULL THEN UOM_NAME else UOM_NAME_P END UOM_NAME,ITEM_TYPE_ID,ITEM_TYPE_NAME,ITEM_CLASS_ID,ITEM_CLASS_NAME,OP_IRR_QTY,OP_ITC_QTY,IRR_DEPT_QTY,IRR_STORE_QTY,IRR_PROD_QTY,ITC_DEPT_QTY,ITC_STORE_QTY,ITC_PROD_QTY,ADJUST_QTY,ITEM_STANDARD_WEIGHT_KG,PANNEL_QTY FROM TEMP_DEPARTMENT_ITEM_STOCK LEFT JOIN PRO_DEPARTMENT_ITEM dept ON ITEM_ID=dept.ITEM_ID ");

                sb.Append(" Select dept.ORDER_NO, tstk.ITEM_GROUP_ID,tstk.ITEM_GROUP_NAME,tstk.ITEM_ID,tstk.ITEM_NAME,tstk.UOM_ID,CASE WHEN tstk.UOM_NAME_P IS NULL THEN tstk.UOM_NAME else tstk.UOM_NAME_P END UOM_NAME,tstk.ITEM_TYPE_ID,tstk.ITEM_TYPE_NAME,tstk.ITEM_CLASS_ID,tstk.ITEM_CLASS_NAME,tstk.OP_IRR_QTY,tstk.OP_ITC_QTY,tstk.IRR_DEPT_QTY,tstk.IRR_STORE_QTY,tstk.IRR_PROD_QTY,tstk.ITC_DEPT_QTY,tstk.ITC_STORE_QTY,tstk.ITC_PROD_QTY,tstk.ADJUST_QTY,tstk.ITEM_STANDARD_WEIGHT_KG,tstk.PANNEL_QTY,ISSUE_FOR_PRODUCTION,ISSUE_FOR_DROSS FROM TEMP_DEPARTMENT_ITEM_STOCK tstk ");
                sb.Append(" INNER JOIN PRO_DEPARTMENT_ITEM dept  ON tstk.ITEM_ID=dept.ITEM_ID AND dept.DEPT_ID= "+prmINV.DeptID);


                DBQuery dbqtemp = new DBQuery();

                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);

                foreach (DataRow dRow in dtData.Rows)
                {
                    rcMaterialStock stk = new rcMaterialStock();
                    stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();

                    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);

                    stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                    stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();
                    stk.OP_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_IRR_QTY"]);
                    stk.OP_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ITC_QTY"]);
                    stk.OPPENING_BAL_QTY = stk.OP_IRR_QTY - stk.OP_ITC_QTY;

                    stk.IRR_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_DEPT_QTY"]);
                    stk.IRR_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_STORE_QTY"]);
                    stk.IRR_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_PROD_QTY"]);

                    //here total IRR quantity
                    stk.IRR_BAL_QTY = stk.IRR_DEPT_QTY + stk.IRR_STORE_QTY + stk.IRR_PROD_QTY;

                    stk.ITC_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_DEPT_QTY"]);
                    stk.ITC_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_STORE_QTY"]);
                    stk.ITC_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_PROD_QTY"]);

                    //here total ITC quantity
                    stk.ITC_BAL_QTY = stk.ITC_DEPT_QTY + stk.ITC_STORE_QTY + stk.ITC_PROD_QTY;

                    //here total Adjust balance quantity
                    stk.ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);

                    stk.PANNEL_QTY = Conversion.DBNullDecimalToZero(dRow["PANNEL_QTY"]);

                    stk.ISSUE_FOR_PRODUCTION = Conversion.DBNullDecimalToZero(dRow["ISSUE_FOR_PRODUCTION"]);
                    stk.ISSURE_FOR_DROSS = Conversion.DBNullDecimalToZero(dRow["ISSUE_FOR_DROSS"]);

                    stk.ITEM_STANDARD_WEIGHT_KG = Conversion.DBNullDecimalToZero(dRow["ITEM_STANDARD_WEIGHT_KG"]);
                    stk.TOTAL_GRID_STD_WEIGHT_KG = Conversion.DBNullDecimalToZero(dRow["ITEM_STANDARD_WEIGHT_KG"]) * stk.IRR_BAL_QTY;
                    stk.ORDER_NO = Conversion.DBNullIntToZero(dRow["ORDER_NO"]);
                    stk.DEPARTMENT_NAME = prmINV.From_Dept_Name;

                    if (stk.ADJUST_QTY > 0)
                    {
                        stk.IRR_BAL_QTY = stk.IRR_BAL_QTY + stk.ADJUST_QTY;
                    }
                    else
                    {
                        stk.ITC_BAL_QTY = stk.ITC_BAL_QTY + Math.Abs(stk.ADJUST_QTY);
                    }


                    stk.COLISING_QTY = (stk.OPPENING_BAL_QTY + stk.IRR_BAL_QTY) - stk.ITC_BAL_QTY;
                    stk.CLOSING_QTY_wt = stk.COLISING_QTY * stk.ITEM_STANDARD_WEIGHT_KG;
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



        // Small Parts Stock Report 
        public static List<rcMaterialStock> GridSMLDepartmentStockReport(clsPrmInventory prmINV)
        {
            return GridSMLDepartmentStockReport(prmINV, null);
        }

        public static List<rcMaterialStock> GridSMLDepartmentStockReport(clsPrmInventory prmINV, DBContext dc)
        {
            List<rcMaterialStock> cRptList = new List<rcMaterialStock>();
            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();

                if (prmINV.ToDate.HasValue)
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.ToDate.Value);
                }
                cmdInfo.DBParametersInfo.Add(":p_itemtype_id", prmINV.itemtype_id);
                cmdInfo.DBParametersInfo.Add(":p_itemGroup_id", prmINV.itemGroup_id);
                cmdInfo.DBParametersInfo.Add(":p_item_id", prmINV.item_id);
                cmdInfo.DBParametersInfo.Add(":p_store_id", prmINV.store_id);
                cmdInfo.DBParametersInfo.Add(":p_itemclass_id", prmINV.ItemClassId);
                cmdInfo.DBParametersInfo.Add(":p_battery_type_id", prmINV.Battery_Type_ID);
                cmdInfo.DBParametersInfo.Add(":p_is_repair", prmINV.Is_Repair);
                cmdInfo.DBParametersInfo.Add(":p_dept_id", prmINV.DeptID);
                cmdInfo.DBParametersInfo.Add(":p_is_finished", prmINV.is_Finished);
                DBQuery dbq = new DBQuery();

                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;
                cmdInfo.CommandText = "SP_SMALL_PARTS_MONTHLY_REPORT";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);


                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;

                //sb.Append(" Select dept.ORDER_NO, ITEM_GROUP_ID,ITEM_GROUP_NAME,ITEM_ID,ITEM_NAME,UOM_ID,CASE WHEN UOM_NAME_P IS NULL THEN UOM_NAME else UOM_NAME_P END UOM_NAME,ITEM_TYPE_ID,ITEM_TYPE_NAME,ITEM_CLASS_ID,ITEM_CLASS_NAME,OP_IRR_QTY,OP_ITC_QTY,IRR_DEPT_QTY,IRR_STORE_QTY,IRR_PROD_QTY,ITC_DEPT_QTY,ITC_STORE_QTY,ITC_PROD_QTY,ADJUST_QTY,ITEM_STANDARD_WEIGHT_KG,PANNEL_QTY FROM TEMP_DEPARTMENT_ITEM_STOCK LEFT JOIN PRO_DEPARTMENT_ITEM dept ON ITEM_ID=dept.ITEM_ID ");

                sb.Append(" Select dept.ORDER_NO, tstk.ITEM_GROUP_ID,tstk.ITEM_GROUP_NAME,tstk.ITEM_ID,tstk.ITEM_NAME,tstk.UOM_ID,CASE WHEN tstk.UOM_NAME_P IS NULL THEN tstk.UOM_NAME else tstk.UOM_NAME_P END UOM_NAME,tstk.ITEM_TYPE_ID,tstk.ITEM_TYPE_NAME,tstk.ITEM_CLASS_ID,tstk.ITEM_CLASS_NAME,tstk.OP_IRR_QTY,tstk.OP_ITC_QTY,tstk.IRR_DEPT_QTY,tstk.IRR_STORE_QTY,tstk.IRR_PROD_QTY,tstk.ITC_DEPT_QTY,tstk.ITC_STORE_QTY,tstk.ITC_PROD_QTY,tstk.ADJUST_QTY,tstk.ITEM_STANDARD_WEIGHT_KG,tstk.PANNEL_QTY,tstk.ISSUE_FOR_PRODUCTION,tstk.ISSUE_FOR_DROSS FROM TEMP_SMALL_PARTS_ITEM_STOCK tstk ");
                sb.Append(" INNER JOIN PRO_DEPARTMENT_ITEM dept  ON tstk.ITEM_ID=dept.ITEM_ID AND dept.DEPT_ID=139 ");


                DBQuery dbqtemp = new DBQuery();

                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);

                foreach (DataRow dRow in dtData.Rows)
                {
                    rcMaterialStock stk = new rcMaterialStock();
                    stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();

                    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);

                    stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                    stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();
                    stk.OP_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_IRR_QTY"]);
                    stk.OP_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ITC_QTY"]);
                    stk.OPPENING_BAL_QTY = stk.OP_IRR_QTY - stk.OP_ITC_QTY;

                    stk.IRR_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_DEPT_QTY"]);
                    stk.IRR_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_STORE_QTY"]);
                    stk.IRR_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_PROD_QTY"]);

                    //here total IRR quantity
                    stk.IRR_BAL_QTY = stk.IRR_DEPT_QTY + stk.IRR_STORE_QTY + stk.IRR_PROD_QTY;

                    stk.ITC_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_DEPT_QTY"]);
                    stk.ITC_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_STORE_QTY"]);
                    stk.ITC_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_PROD_QTY"]);

                    //here total ITC quantity
                    stk.ITC_BAL_QTY = stk.ITC_DEPT_QTY + stk.ITC_STORE_QTY + stk.ITC_PROD_QTY;

                    //here total Adjust balance quantity
                    stk.ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);

                    stk.PANNEL_QTY = Conversion.DBNullDecimalToZero(dRow["PANNEL_QTY"]);
                    stk.ITEM_STANDARD_WEIGHT_KG = Conversion.DBNullDecimalToZero(dRow["ITEM_STANDARD_WEIGHT_KG"]);
                    //stk.TOTAL_GRID_STD_WEIGHT_KG = stk.ITEM_STANDARD_WEIGHT_KG * stk.IRR_BAL_QTY;  
                    stk.ISSUE_FOR_PRODUCTION = Conversion.DBNullDecimalToZero(dRow["ISSUE_FOR_PRODUCTION"]);
                    stk.ISSURE_FOR_DROSS = Conversion.DBNullDecimalToZero(dRow["ISSUE_FOR_DROSS"]) + stk.ITC_STORE_QTY;

                    stk.ORDER_NO = Conversion.DBNullIntToZero(dRow["ORDER_NO"]);
                    stk.DEPARTMENT_NAME = prmINV.From_Dept_Name;


                    if (stk.ADJUST_QTY > 0)
                    {
                        stk.IRR_BAL_QTY = stk.IRR_BAL_QTY + stk.ADJUST_QTY;
                    }
                    else
                    {
                        stk.ITC_BAL_QTY = stk.ITC_BAL_QTY + Math.Abs(stk.ADJUST_QTY);
                    }


                    stk.COLISING_QTY = (stk.OPPENING_BAL_QTY + stk.IRR_BAL_QTY) - stk.ITC_BAL_QTY;
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


        public static List<rcMaterialStock> GridCasting_Usuable_Report(clsPrmInventory prmINV)
        {
            return GridCasting_Usuable_Report(prmINV, null);
        }

        public static List<rcMaterialStock> GridCasting_Usuable_Report(clsPrmInventory prmINV, DBContext dc)
        {

            List<rcMaterialStock> cRptList = new List<rcMaterialStock>();


            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();

                if (prmINV.ToDate.HasValue)
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.ToDate.Value);
                }
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
                cmdInfo.CommandText = "SP_GRID_USUABLE_STOCK_REPORT";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);


                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;

                sb.Append(" Select ITEM_GROUP_ID,ITEM_GROUP_NAME,ITEM_ID,ITEM_NAME,ITEM_CODE,UOM_ID,CASE WHEN UOM_NAME_P IS NULL THEN UOM_NAME else UOM_NAME_P END UOM_NAME,ITEM_TYPE_ID,ITEM_TYPE_NAME,ITEM_CLASS_ID,ITEM_CLASS_NAME,OP_IRR_QTY,OP_ITC_QTY,IRR_DEPT_QTY,IRR_STORE_QTY,IRR_PROD_QTY,ITC_DEPT_QTY,ITC_STORE_QTY,ITC_PROD_QTY,ADJUST_QTY,ITEM_STANDARD_WEIGHT_KG,PANNEL_QTY,PROD_USUABLE_QTY FROM TEMP_DEPARTMENT_ITEM_STOCK");

                DBQuery dbqtemp = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);

                foreach (DataRow dRow in dtData.Rows)
                {
                    rcMaterialStock stk = new rcMaterialStock();
                    stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();
                    stk.ITEM_CODE = dRow["ITEM_CODE"].ToString();

                    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);

                    stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                    stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();
                    //total receive before given date rance
                    stk.OP_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_IRR_QTY"]);
                    //total issue before given date rance
                    stk.OP_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ITC_QTY"]);
                    stk.OPPENING_BAL_QTY = stk.OP_IRR_QTY - stk.OP_ITC_QTY;

                    stk.IRR_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_DEPT_QTY"]);
                    stk.IRR_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_STORE_QTY"]);
                    stk.IRR_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_PROD_QTY"]);
                    stk.USUABLE_IRR_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["PROD_USUABLE_QTY"]);

                    //here total IRR quantity within given date range
                    stk.IRR_BAL_QTY = stk.IRR_DEPT_QTY + stk.IRR_STORE_QTY + stk.IRR_PROD_QTY;
                    stk.USUABLE_IRR_BAL_QTY = stk.IRR_DEPT_QTY + stk.IRR_STORE_QTY + stk.USUABLE_IRR_PROD_QTY;

                    stk.ITC_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_DEPT_QTY"]);
                    stk.ITC_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_STORE_QTY"]);
                    stk.ITC_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_PROD_QTY"]);

                    //here total ITC quantity within given date range
                    stk.ITC_BAL_QTY = stk.ITC_DEPT_QTY + stk.ITC_STORE_QTY + stk.ITC_PROD_QTY;

                    //here total Adjust balance quantity within given date range
                    stk.ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);

                    stk.PANNEL_QTY = Conversion.DBNullDecimalToZero(dRow["PANNEL_QTY"]);
                    stk.ITEM_STANDARD_WEIGHT_KG = Conversion.DBNullDecimalToZero(dRow["ITEM_STANDARD_WEIGHT_KG"]);
                    stk.DEPARTMENT_NAME = prmINV.From_Dept_Name;

                    if (stk.ADJUST_QTY > 0)
                    {
                        stk.IRR_BAL_QTY = stk.IRR_BAL_QTY + stk.ADJUST_QTY;
                    }
                    else
                    {
                        stk.ITC_BAL_QTY = stk.ITC_BAL_QTY + Math.Abs(stk.ADJUST_QTY);
                    }

                    stk.COLISING_QTY = (stk.OPPENING_BAL_QTY + stk.IRR_BAL_QTY) - stk.ITC_BAL_QTY;
                    stk.USUABLE_QTY = (stk.OPPENING_BAL_QTY + stk.USUABLE_IRR_BAL_QTY) - stk.ITC_BAL_QTY;
                    stk.UN_USUABLE_QTY = stk.COLISING_QTY - stk.USUABLE_QTY;
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




        //End


        //Plastic Usable Report

        public static List<rcMaterialStock> Plastic_Usuable_Report(clsPrmInventory prmINV)
        {
            return Plastic_Usuable_Report(prmINV, null);
        }

        public static List<rcMaterialStock> Plastic_Usuable_Report(clsPrmInventory prmINV, DBContext dc)
        {

            List<rcMaterialStock> cRptList = new List<rcMaterialStock>();


            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();

                if (prmINV.ToDate.HasValue)
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.ToDate.Value);
                }
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
                cmdInfo.CommandText = "SP_PPL_USUABLE_STOCK_REPORT";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);


                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;

                sb.Append(" Select ITEM_GROUP_ID,ITEM_GROUP_NAME,ITEM_ID,ITEM_NAME,ITEM_CODE,UOM_ID,CASE WHEN UOM_NAME_P IS NULL THEN UOM_NAME else UOM_NAME_P END UOM_NAME,ITEM_TYPE_ID,ITEM_TYPE_NAME,ITEM_CLASS_ID,ITEM_CLASS_NAME,OP_IRR_QTY,OP_ITC_QTY,IRR_DEPT_QTY,IRR_STORE_QTY,IRR_PROD_QTY,ITC_DEPT_QTY,ITC_STORE_QTY,ITC_PROD_QTY,ADJUST_QTY,ITEM_STANDARD_WEIGHT_KG,PANNEL_QTY,PROD_USUABLE_QTY FROM TEMP_DEPARTMENT_ITEM_STOCK");

                DBQuery dbqtemp = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);

                foreach (DataRow dRow in dtData.Rows)
                {
                    rcMaterialStock stk = new rcMaterialStock();
                    stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();
                    stk.ITEM_CODE = dRow["ITEM_CODE"].ToString();

                    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);

                    stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                    stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();
                    //total receive before given date rance
                    stk.OP_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_IRR_QTY"]);
                    //total issue before given date rance
                    stk.OP_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ITC_QTY"]);
                    stk.OPPENING_BAL_QTY = stk.OP_IRR_QTY - stk.OP_ITC_QTY;

                    stk.IRR_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_DEPT_QTY"]);
                    stk.IRR_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_STORE_QTY"]);
                    stk.IRR_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_PROD_QTY"]);
                    stk.USUABLE_IRR_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["PROD_USUABLE_QTY"]);

                    //here total IRR quantity within given date range
                    stk.IRR_BAL_QTY = stk.IRR_DEPT_QTY + stk.IRR_STORE_QTY + stk.IRR_PROD_QTY;
                    stk.USUABLE_IRR_BAL_QTY = stk.IRR_DEPT_QTY + stk.IRR_STORE_QTY + stk.USUABLE_IRR_PROD_QTY;

                    stk.ITC_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_DEPT_QTY"]);
                    stk.ITC_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_STORE_QTY"]);
                    stk.ITC_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_PROD_QTY"]);

                    //here total ITC quantity within given date range
                    stk.ITC_BAL_QTY = stk.ITC_DEPT_QTY + stk.ITC_STORE_QTY + stk.ITC_PROD_QTY;

                    //here total Adjust balance quantity within given date range
                    stk.ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);

                    stk.PANNEL_QTY = Conversion.DBNullDecimalToZero(dRow["PANNEL_QTY"]);
                    stk.ITEM_STANDARD_WEIGHT_KG = Conversion.DBNullDecimalToZero(dRow["ITEM_STANDARD_WEIGHT_KG"]);
                    stk.DEPARTMENT_NAME = prmINV.From_Dept_Name;

                    if (stk.ADJUST_QTY > 0)
                    {
                        stk.IRR_BAL_QTY = stk.IRR_BAL_QTY + stk.ADJUST_QTY;
                    }
                    else
                    {
                        stk.ITC_BAL_QTY = stk.ITC_BAL_QTY + Math.Abs(stk.ADJUST_QTY);
                    }

                    stk.COLISING_QTY = (stk.OPPENING_BAL_QTY + stk.IRR_BAL_QTY) - stk.ITC_BAL_QTY;
                    stk.USUABLE_QTY = (stk.OPPENING_BAL_QTY + stk.USUABLE_IRR_BAL_QTY) - stk.ITC_BAL_QTY;
                    stk.UN_USUABLE_QTY = stk.COLISING_QTY - stk.USUABLE_QTY;
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

        //End


        #region Formation_Production_Report

        public static List<rcMaterialStock> Formation_Production_Report(clsPrmInventory prmINV)
        {
            return Formation_Production_Report(prmINV, null);
        }

        public static List<rcMaterialStock> Formation_Production_Report(clsPrmInventory prmINV, DBContext dc)
        {

            List<rcMaterialStock> cRptList = new List<rcMaterialStock>();


            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();

                if (prmINV.ToDate.HasValue)
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.ToDate.Value);
                }
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
                cmdInfo.CommandText = "SP_FORMATION_PRODUCTION_REPORT";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);


                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;

                sb.Append(" Select ITEM_GROUP_ID,ITEM_GROUP_NAME,ITEM_ID,ITEM_NAME,UOM_ID,CASE WHEN UOM_NAME_P IS NULL THEN UOM_NAME else UOM_NAME_P END UOM_NAME,ITEM_TYPE_ID,ITEM_TYPE_NAME,ITEM_CLASS_ID,ITEM_CLASS_NAME,OP_IRR_QTY,OP_ITC_QTY,IRR_DEPT_QTY,IRR_STORE_QTY,IRR_PROD_QTY,ITC_DEPT_QTY,ITC_STORE_QTY,ITC_PROD_QTY,ADJUST_QTY,ITEM_STANDARD_WEIGHT_KG,PANNEL_QTY FROM TEMP_DEPARTMENT_ITEM_STOCK");


                DBQuery dbqtemp = new DBQuery();

                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);

                foreach (DataRow dRow in dtData.Rows)
                {
                    rcMaterialStock stk = new rcMaterialStock();
                    stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();

                    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);

                    stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                    stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();
                    stk.OP_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_IRR_QTY"]);
                    stk.OP_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ITC_QTY"]);
                    stk.OPPENING_BAL_QTY = stk.OP_IRR_QTY - stk.OP_ITC_QTY;

                    stk.IRR_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_DEPT_QTY"]);
                    stk.IRR_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_STORE_QTY"]);
                    stk.IRR_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_PROD_QTY"]);

                    //here total IRR quantity
                    stk.IRR_BAL_QTY = stk.IRR_DEPT_QTY + stk.IRR_STORE_QTY + stk.IRR_PROD_QTY;

                    stk.ITC_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_DEPT_QTY"]);
                    stk.ITC_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_STORE_QTY"]);
                    stk.ITC_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_PROD_QTY"]);

                    //here total ITC quantity
                    stk.ITC_BAL_QTY = stk.ITC_DEPT_QTY + stk.ITC_STORE_QTY + stk.ITC_PROD_QTY;

                    //here total Adjust balance quantity
                    stk.ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);

                    stk.PANNEL_QTY = Conversion.DBNullDecimalToZero(dRow["PANNEL_QTY"]);
                    stk.ITEM_STANDARD_WEIGHT_KG = Conversion.DBNullDecimalToZero(dRow["ITEM_STANDARD_WEIGHT_KG"]);
                    stk.DEPARTMENT_NAME = prmINV.From_Dept_Name;


                    if (stk.ADJUST_QTY > 0)
                    {
                        stk.IRR_BAL_QTY = stk.IRR_BAL_QTY + stk.ADJUST_QTY;
                    }
                    else
                    {
                        stk.ITC_BAL_QTY = stk.ITC_BAL_QTY + Math.Abs(stk.ADJUST_QTY);
                    }


                    stk.COLISING_QTY = (stk.OPPENING_BAL_QTY + stk.IRR_BAL_QTY) - stk.ITC_BAL_QTY;
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




        public static List<rcMaterialStock> Formation_Production_Form_Unform_Report(clsPrmInventory prmINV)
        {
            return Formation_Production_Form_Unform_Report(prmINV, null);
        }

        public static List<rcMaterialStock> Formation_Production_Form_Unform_Report(clsPrmInventory prmINV, DBContext dc)
        {

            List<rcMaterialStock> cRptList = new List<rcMaterialStock>();


            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();

                if (prmINV.ToDate.HasValue)
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.ToDate.Value);
                }
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

                if (prmINV.Plate_Type == "F")
                {
                    cmdInfo.CommandText = "SP_FOR_PROD_FORM_PLATE_REPORT";
                }

                if (prmINV.Plate_Type == "U")
                {
                    cmdInfo.CommandText = "SP_FORPROD_UNFORM_PLATE_REPORT";
                }

                if (prmINV.Plate_Type == "R")
                {
                    cmdInfo.CommandText = "SP_FOR_PROD_REJECT_PLT_REPORT";
                }

                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);


                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;

                sb.Append(" Select ITEM_GROUP_ID,ITEM_GROUP_NAME,ITEM_ID,ITEM_NAME,UOM_ID,CASE WHEN UOM_NAME_P IS NULL THEN UOM_NAME else UOM_NAME_P END UOM_NAME,ITEM_TYPE_ID,ITEM_TYPE_NAME,ITEM_CLASS_ID,ITEM_CLASS_NAME,OP_IRR_QTY,OP_ITC_QTY,IRR_DEPT_QTY,IRR_STORE_QTY,IRR_PROD_QTY,ITC_DEPT_QTY,ITC_STORE_QTY,ITC_PROD_QTY,ADJUST_QTY,ITEM_STANDARD_WEIGHT_KG,PANNEL_QTY,ITEM_ORDER FROM TEMP_DEPARTMENT_ITEM_STOCK");


                DBQuery dbqtemp = new DBQuery();

                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                dbq.OrderBy = "ORDER_NO";
                DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);

                foreach (DataRow dRow in dtData.Rows)
                {
                    rcMaterialStock stk = new rcMaterialStock();
                    stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();

                    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);

                    stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                    stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();
                    stk.OP_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_IRR_QTY"]);
                    stk.OP_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ITC_QTY"]);
                    stk.OPPENING_BAL_QTY = stk.OP_IRR_QTY - stk.OP_ITC_QTY;

                    stk.IRR_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_DEPT_QTY"]);
                    stk.IRR_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_STORE_QTY"]);
                    stk.IRR_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_PROD_QTY"]);

                    //here total IRR quantity
                    stk.IRR_BAL_QTY = stk.IRR_DEPT_QTY + stk.IRR_STORE_QTY + stk.IRR_PROD_QTY;

                    stk.ITC_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_DEPT_QTY"]);
                    stk.ITC_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_STORE_QTY"]);
                    stk.ITC_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_PROD_QTY"]);

                    //here total ITC quantity
                    stk.ITC_BAL_QTY = stk.ITC_DEPT_QTY + stk.ITC_STORE_QTY + stk.ITC_PROD_QTY;

                    //here total Adjust balance quantity
                    stk.ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);

                    stk.PANNEL_QTY = Conversion.DBNullDecimalToZero(dRow["PANNEL_QTY"]);
                    stk.ITEM_STANDARD_WEIGHT_KG = Conversion.DBNullDecimalToZero(dRow["ITEM_STANDARD_WEIGHT_KG"]);
                    stk.ORDER_NO = Conversion.DBNullIntToZero(dRow["ITEM_ORDER"]);
                    stk.DEPARTMENT_NAME = prmINV.From_Dept_Name;


                    if (stk.ADJUST_QTY > 0)
                    {
                        stk.IRR_BAL_QTY = stk.IRR_BAL_QTY + stk.ADJUST_QTY;
                    }
                    else
                    {
                        stk.ITC_BAL_QTY = stk.ITC_BAL_QTY + Math.Abs(stk.ADJUST_QTY);
                    }


                    stk.COLISING_QTY = (stk.OPPENING_BAL_QTY + stk.IRR_BAL_QTY) - stk.ITC_BAL_QTY;
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

        public static List<rcFormationProductionSummary> Formation_Production_Summary_Report(clsPrmInventory prmINV)
        {
            return Formation_Production_Summary_Report(prmINV, null);
        }

        public static List<rcFormationProductionSummary> Formation_Production_Summary_Report(clsPrmInventory prmINV, DBContext dc)
        {

            List<rcFormationProductionSummary> cRptList = new List<rcFormationProductionSummary>();


            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();

                if (prmINV.ToDate.HasValue)
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.ToDate.Value);
                }
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
                sb.Append(" Select ITEM_GROUP_ID,ITEM_GROUP_NAME,ITEM_ID,ITEM_NAME,UOM_ID,UOM_NAME,ITEM_TYPE_ID,ITEM_TYPE_NAME,ITEM_CLASS_ID,ITEM_CLASS_NAME,OP_F_IRR_QTY,OP_F_ITC_QTY,OP_UF_IRR_QTY,OP_UF_ITC_QTY,UF_IRR_QTY,UF_ITC_QTY,IRR_DEPT_QTY,IRR_STORE_QTY,IRR_PROD_QTY,ITC_DEPT_QTY,ITC_STORE_QTY,ITC_PROD_QTY,ADJUST_QTY,RCV_FROM_IB,RCV_FROM_PASTING,REJECT_QTY,OP_TOTAL_RCV_QTY,WIP_QTY,OP_LOAD_QTY,OP_UN_LOAD_QTY,OP_TOTAL_REJECT_QTY");
                sb.Append(" ,ITEM_ORDER,ISSUE_TO_ASSEMBLY,  0   REJECT_PRT,OP_WIP_ADJUST_QTY,OP_F_ADJUST_QTY,OP_UF_ADJUST_QTY,CUR_F_ADJ_QTY,CUR_U_ADJ_QTY,CUR_WIP_ADJ_QTY,GRID_PC_KG_STD,PASTE_PC_KG_STD ");
                sb.Append("  FROM TEMP_FORMATION_ITEM_STOCK ");

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
                    stk.DEPARTMENT_NAME = prmINV.From_Dept_Name;
                    stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    stk.ITEM_ORDER = Conversion.DBNullIntToZero(dRow["ITEM_ORDER"]);
                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();

                    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);

                    stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                    stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();

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

                    if (stk.IRR_PROD_QTY > 0)
                        stk.REJECT_PRT = Conversion.DBNullDecimalToZero((stk.REJECT_QTY * 100) / (stk.IRR_PROD_QTY));
                    else
                        stk.REJECT_PRT = 0;

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
                    stk.PASTE_PC_KG_STD = Conversion.DBNullDecimalToZero(dRow["PASTE_PC_KG_STD"]);
                    stk.GRID_PC_KG_STD = Conversion.DBNullDecimalToZero(dRow["GRID_PC_KG_STD"]);
                    
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




        #endregion


        #region Pesting_Production_Related_Report



        public static List<rcMaterialStock> GeneratePestingProductionReport(clsPrmInventory prmINV)
        {
            return GeneratePestingProductionReport(prmINV, null);
        }

        public static List<rcMaterialStock> GeneratePestingProductionReport(clsPrmInventory prmINV, DBContext dc)
        {
            List<rcMaterialStock> cRptList = new List<rcMaterialStock>();
            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();

                if (prmINV.ToDate.HasValue)
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.ToDate.Value);
                }
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
                cmdInfo.CommandText = "SP_PESTING_PRODUCTION_REPORT";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);


                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;

                sb.Append(" Select ITEM_GROUP_ID,ITEM_GROUP_NAME,ITEM_ID,ITEM_NAME,UOM_ID,CASE WHEN UOM_NAME_P IS NULL THEN UOM_NAME else UOM_NAME_P END UOM_NAME,ITEM_TYPE_ID,ITEM_TYPE_NAME,ITEM_CLASS_ID,ITEM_CLASS_NAME,OP_IRR_QTY,OP_ITC_QTY,IRR_DEPT_QTY,IRR_STORE_QTY,IRR_PROD_QTY,ITC_DEPT_QTY,ITC_STORE_QTY,ITC_PROD_QTY,ADJUST_QTY,ITEM_STANDARD_WEIGHT_KG,PANNEL_QTY,REJECT_QTY_RCV,REJECT_QTY_ISS,ITC_FORMATION_QTY,ITC_ASSEMBLY_QTY,PASTE_PC_KG,ITEM_ORDER FROM TEMP_PESTING_ITEM_STOCK");


                DBQuery dbqtemp = new DBQuery();

                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);

                foreach (DataRow dRow in dtData.Rows)
                {
                    rcMaterialStock stk = new rcMaterialStock();
                    stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    stk.ITEM_ORDER = Conversion.DBNullIntToZero(dRow["ITEM_ORDER"]);
                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();

                    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);

                    stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                    stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();
                    stk.OP_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_IRR_QTY"]);

                    stk.REJECT_QTY_RCV = Conversion.DBNullDecimalToZero(dRow["REJECT_QTY_RCV"]);
                    stk.REJECT_QTY_ISS = Conversion.DBNullDecimalToZero(dRow["REJECT_QTY_ISS"]);

                    stk.OP_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ITC_QTY"]);
                    stk.OPPENING_BAL_QTY = stk.OP_IRR_QTY - stk.OP_ITC_QTY;

                    stk.IRR_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_DEPT_QTY"]);
                    stk.IRR_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_STORE_QTY"]);
                    stk.IRR_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_PROD_QTY"]);

                    //here total IRR quantity
                    //stk.IRR_BAL_QTY = stk.IRR_DEPT_QTY + stk.IRR_STORE_QTY + stk.IRR_PROD_QTY;
                    stk.IRR_BAL_QTY = stk.IRR_PROD_QTY;

                    stk.ITC_FORMATION_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_FORMATION_QTY"]);
                    stk.ITC_ASSEMBLY_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_ASSEMBLY_QTY"]);
                    stk.ITC_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_DEPT_QTY"]);
                    stk.ITC_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_STORE_QTY"]);
                    stk.ITC_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_PROD_QTY"]);

                    //here total ITC quantity
                    stk.ITC_BAL_QTY = stk.ITC_DEPT_QTY + stk.ITC_STORE_QTY + stk.ITC_PROD_QTY;

                    //here total Adjust balance quantity
                    stk.ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);

                    stk.PANNEL_QTY = Conversion.DBNullDecimalToZero(dRow["PANNEL_QTY"]);
                    stk.ITEM_STANDARD_WEIGHT_KG = Conversion.DBNullDecimalToZero(dRow["ITEM_STANDARD_WEIGHT_KG"]);
                    stk.PASTE_PC_KG = Conversion.DBNullDecimalToZero(dRow["PASTE_PC_KG"]);
                    stk.TOTAL_PASTE_PC_KG = Conversion.DBNullDecimalToZero(dRow["PASTE_PC_KG"]) * stk.IRR_BAL_QTY;
                    stk.DEPARTMENT_NAME = prmINV.From_Dept_Name;

                    stk.PASTED_PLATE_PC_KG_STD = stk.ITEM_STANDARD_WEIGHT_KG + stk.PASTE_PC_KG;
                    stk.TOTAL_PASTED_PLATE_PC_KG_STD = stk.PASTED_PLATE_PC_KG_STD * stk.IRR_BAL_QTY;

                    if (stk.ADJUST_QTY > 0)
                    {
                        stk.IRR_BAL_QTY = stk.IRR_BAL_QTY + stk.ADJUST_QTY;
                    }
                    else
                    {
                        stk.ITC_BAL_QTY = stk.ITC_BAL_QTY + Math.Abs(stk.ADJUST_QTY);
                    }


                    stk.COLISING_QTY = (stk.OPPENING_BAL_QTY + stk.IRR_BAL_QTY) - stk.ITC_BAL_QTY;
                    stk.CLOSING_QTY_wt = stk.COLISING_QTY * stk.PASTED_PLATE_PC_KG_STD;

                    //if (stk.COLISING_QTY>0)
                    if (stk.CLOSING_QTY > 0 || stk.OPPENING_BAL_QTY > 0 || stk.IRR_BAL_QTY > 0 || stk.ITC_BAL_QTY > 0 || stk.COLISING_QTY > 0)
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


        public static List<rcMaterialStock> GeneratePestingRawMaterialReport(clsPrmInventory prmINV)
        {
            return GeneratePestingRawMaterialReport(prmINV, null);
        }

        public static List<rcMaterialStock> GeneratePestingRawMaterialReport(clsPrmInventory prmINV, DBContext dc)
        {
            List<rcMaterialStock> cRptList = new List<rcMaterialStock>();
            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();

                if (prmINV.ToDate.HasValue)
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.ToDate.Value);
                }
                cmdInfo.DBParametersInfo.Add(":p_itemtype_id", prmINV.itemtype_id);
                cmdInfo.DBParametersInfo.Add(":p_itemGroup_id", prmINV.itemGroup_id);
                cmdInfo.DBParametersInfo.Add(":p_item_id", prmINV.item_id);
                cmdInfo.DBParametersInfo.Add(":p_store_id", prmINV.store_id);
                cmdInfo.DBParametersInfo.Add(":p_itemclass_id", prmINV.ItemClassId);
                cmdInfo.DBParametersInfo.Add(":p_battery_type_id", prmINV.Battery_Type_ID);
                cmdInfo.DBParametersInfo.Add(":p_is_repair", prmINV.Is_Repair);
                cmdInfo.DBParametersInfo.Add(":p_dept_id", prmINV.DeptID);
                cmdInfo.DBParametersInfo.Add(":p_is_finished", "N");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;
                cmdInfo.CommandText = "SP_PESTING_PRODUCTION_REPORT";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);


                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;

                sb.Append(" Select ITEM_GROUP_ID,ITEM_GROUP_NAME,ITEM_ID,ITEM_NAME,UOM_ID,CASE WHEN UOM_NAME_P IS NULL THEN UOM_NAME else UOM_NAME_P END UOM_NAME,ITEM_TYPE_ID,ITEM_TYPE_NAME,ITEM_CLASS_ID,ITEM_CLASS_NAME,OP_IRR_QTY,OP_ITC_QTY,IRR_DEPT_QTY,IRR_STORE_QTY,IRR_PROD_QTY,ITC_DEPT_QTY,ITC_STORE_QTY,ITC_PROD_QTY,ADJUST_QTY,ITEM_STANDARD_WEIGHT_KG,PANNEL_QTY,REJECT_QTY_RCV,REJECT_QTY_ISS,IS_BY_PRODUCT,IRR_RECP_QTY,ITC_TRANS_QTY FROM TEMP_PESTING_ITEM_STOCK");


                DBQuery dbqtemp = new DBQuery();

                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);

                foreach (DataRow dRow in dtData.Rows)
                {
                    rcMaterialStock stk = new rcMaterialStock();
                    stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();

                    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);
                    stk.IS_BY_PRODUCT = dRow["IS_BY_PRODUCT"].ToString();


                    stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                    stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();
                    stk.OP_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_IRR_QTY"]);

                    stk.REJECT_QTY_RCV = Conversion.DBNullDecimalToZero(dRow["REJECT_QTY_RCV"]);
                    stk.REJECT_QTY_ISS = Conversion.DBNullDecimalToZero(dRow["REJECT_QTY_ISS"]);

                    stk.OP_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ITC_QTY"]);
                    stk.OPPENING_BAL_QTY = stk.OP_IRR_QTY - stk.OP_ITC_QTY;

                    stk.IRR_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_DEPT_QTY"]);
                    stk.IRR_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_STORE_QTY"]);
                    stk.IRR_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_PROD_QTY"]);

                    //here total IRR quantity
                    stk.IRR_BAL_QTY = stk.IRR_DEPT_QTY + stk.IRR_STORE_QTY + stk.IRR_PROD_QTY;
                    //stk.IRR_BAL_QTY = stk.IRR_PROD_QTY;

                    stk.ITC_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_DEPT_QTY"]);
                    stk.ITC_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_STORE_QTY"]);
                    stk.ITC_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_PROD_QTY"]);

                    //here total ITC quantity
                    stk.ITC_BAL_QTY = stk.ITC_DEPT_QTY + stk.ITC_STORE_QTY + stk.ITC_PROD_QTY;

                    //here total Adjust balance quantity
                    stk.ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);


                    //stk.IRR_RECP_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_RECP_QTY"]);
                    //stk.ITC_TRANS_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_TRANS_QTY"]);

                    stk.PANNEL_QTY = Conversion.DBNullDecimalToZero(dRow["PANNEL_QTY"]);
                    stk.ITEM_STANDARD_WEIGHT_KG = Conversion.DBNullDecimalToZero(dRow["ITEM_STANDARD_WEIGHT_KG"]);
                    stk.DEPARTMENT_NAME = prmINV.From_Dept_Name;


                    if (stk.ADJUST_QTY > 0)
                    {
                        stk.IRR_BAL_QTY = stk.IRR_BAL_QTY + stk.ADJUST_QTY;
                    }
                    else
                    {
                        stk.ITC_BAL_QTY = stk.ITC_BAL_QTY + Math.Abs(stk.ADJUST_QTY);
                    }


                    stk.COLISING_QTY = (stk.OPPENING_BAL_QTY + stk.IRR_BAL_QTY) - stk.ITC_BAL_QTY;
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

        //Reject grid

        public static List<rcMaterialStock> GeneratePestingRejectGridSummReport(clsPrmInventory prmINV)
        {
            return GeneratePestingRejectGridSummReport(prmINV, null);
        }

        public static List<rcMaterialStock> GeneratePestingRejectGridSummReport(clsPrmInventory prmINV, DBContext dc)
        {
            List<rcMaterialStock> cRptList = new List<rcMaterialStock>();
            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();

                if (prmINV.ToDate.HasValue)
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.ToDate.Value);
                }
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

                sb.Append(" Select ITEM_GROUP_ID,ITEM_GROUP_NAME,ITEM_ID,ITEM_NAME,UOM_ID ");
                sb.Append(" ,UOM_NAME,ITEM_TYPE_ID,ITEM_TYPE_NAME,ITEM_CLASS_ID,ITEM_CLASS_NAME ");
                sb.Append(" ,OP_GD_GRID_IRR_QTY,OP_GD_GRID_ITC_QTY,OP_GD_GRID_BAL,IRR_GD_GRID_QTY ");
                sb.Append(" ,ITC_GD_GRID_QTY,ITC_GD_GRID_BAL_QTY,OP_REJ_GRID_IRR_QTY,OP_REJ_GRID_ITC_QTY ");
                sb.Append(" ,OP_REJ_GRID_BAL_QTY,IRR_REJ_GRID_QTY,ITC_REJ_GRID_QTY,ITC_REJ_GRID_BAL_QTY,ITEM_STANDARD_WEIGHT_KG,ISSUE_TO_STORE_REJ_GRID,OP_GD_GRID_ADJ_QTY ");
                sb.Append(" FROM TEMP_PASTE_GRID_REJ_STOCK ");

                DBQuery dbqtemp = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);
                foreach (DataRow dRow in dtData.Rows)
                {
                    rcMaterialStock stk = new rcMaterialStock();
                    stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();

                    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);

                    stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                    //stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    //stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();
                    stk.OP_GD_GRID_ADJ_QTY = Conversion.DBNullDecimalToZero(dRow["OP_GD_GRID_ADJ_QTY"]);
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
                    stk.ITEM_STANDARD_WEIGHT_KG = Conversion.DBNullDecimalToZero(dRow["ITEM_STANDARD_WEIGHT_KG"]);
                    stk.ISSUE_TO_STORE_REJ_GRID = Conversion.DBNullDecimalToZero(dRow["ISSUE_TO_STORE_REJ_GRID"]);

                    stk.ITC_GD_GRID_BAL_QTY = stk.OP_GD_GRID_BAL + stk.IRR_GD_GRID_QTY - stk.ITC_GD_GRID_QTY - stk.IRR_REJ_GRID_QTY;

                    stk.TOTAL_GOOD_GRID_RCV_QTY = stk.OP_GD_GRID_BAL + stk.IRR_GD_GRID_QTY;
                    stk.TOTAL_REJ_GRID_RCV_QTY = stk.OP_REJ_GRID_BAL_QTY + stk.IRR_REJ_GRID_QTY;
                    stk.TOTAL_REJ_GRID_WET = stk.ITEM_STANDARD_WEIGHT_KG * stk.TOTAL_REJ_GRID_RCV_QTY;
                    //if (stk.COLISING_QTY>0)
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

        //Issue to Store
        public static List<rcMaterialStock> GeneratePestingRejectGridIssuetoStoreSummReport(clsPrmInventory prmINV, DBContext dc)
        {
            List<rcMaterialStock> cRptList = new List<rcMaterialStock>();
            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();

                if (prmINV.ToDate.HasValue)
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.ToDate.Value);
                }
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
                cmdInfo.CommandText = "SP_PAST_GRID_REJ_ISS_ST_SUMM";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);


                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;

                sb.Append(" Select ITEM_ID,ITEM_NAME,UOM_ID ");
                sb.Append(" ,UOM_NAME,ITEM_TYPE_ID,ITEM_TYPE_NAME ");


                sb.Append(" ,IRR_REJ_GRID_QTY,ISSUE_TO_STORE_REJ_GRID,ISSUE_TO_CASTING_REJ_GRID ");
                sb.Append(" FROM TEMP_PASTE_GRID_REJ_STOCK ");


                DBQuery dbqtemp = new DBQuery();

                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);

                foreach (DataRow dRow in dtData.Rows)
                {
                    rcMaterialStock stk = new rcMaterialStock();
                    //stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    //stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();

                    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);

                    stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                   

                    stk.IRR_REJ_GRID_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_REJ_GRID_QTY"]);
                    stk.ISSUE_TO_CASTING_REJ_GRID = Conversion.DBNullDecimalToZero(dRow["ISSUE_TO_CASTING_REJ_GRID"]);
                    stk.ISSUE_TO_STORE_REJ_GRID = Conversion.DBNullDecimalToZero(dRow["ISSUE_TO_STORE_REJ_GRID"]);
                    stk.TOTAL_REJ_GRID_WET = stk.IRR_REJ_GRID_QTY - stk.ISSUE_TO_STORE_REJ_GRID - stk.ISSUE_TO_CASTING_REJ_GRID;
                    //if (stk.COLISING_QTY>0)
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

        #endregion



        #region IB Stock Report

        public static List<rcMaterialStock> IBStockReport(clsPrmInventory prmINV)
        {
            return IBStockReport(prmINV, null);
        }

        public static List<rcMaterialStock> IBStockReport(clsPrmInventory prmINV, DBContext dc)
        {

            List<rcMaterialStock> cRptList = new List<rcMaterialStock>();


            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();


                if (prmINV.ToDate.HasValue)
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.ToDate.Value);
                }
                cmdInfo.DBParametersInfo.Add(":p_itemtype_id", prmINV.itemtype_id);
                cmdInfo.DBParametersInfo.Add(":p_itemGroup_id", prmINV.itemGroup_id);
                cmdInfo.DBParametersInfo.Add(":p_item_id", prmINV.item_id);
                cmdInfo.DBParametersInfo.Add(":p_store_id", prmINV.store_id);
                cmdInfo.DBParametersInfo.Add(":p_itemclass_id", prmINV.ItemClassId);
                cmdInfo.DBParametersInfo.Add(":p_battery_type_id", prmINV.Battery_Type_ID);
                cmdInfo.DBParametersInfo.Add(":p_is_repair", prmINV.Is_Repair);
                cmdInfo.DBParametersInfo.Add(":p_dept_id", prmINV.DeptID);
                cmdInfo.DBParametersInfo.Add(":p_is_finised", prmINV.is_Finished);

                //cmdInfo.DBParametersInfo.Add(":P_ITEM_CODE", prmSND.Item_Code);
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;
                cmdInfo.CommandText = "SP_IB_DEPARTMENT_STOCK_REPORT";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);


                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;
                sb.Append(" Select ITEM_GROUP_ID,ITEM_GROUP_NAME,ITEM_ID,ITEM_NAME,UOM_ID,UOM_NAME,ITEM_TYPE_ID,ITEM_TYPE_NAME,ITEM_CLASS_ID,ITEM_CLASS_NAME,OP_IRR_QTY,OP_ITC_QTY,IRR_DEPT_QTY,IRR_STORE_QTY,IRR_PROD_QTY,ITC_DEPT_QTY,ITC_STORE_QTY,ITC_PROD_QTY,ADJUST_QTY,FORMATION_ISS_QTY, ASSEMBLY_ISS_QTY,GRID_PC_KG_STD,PASTE_PC_KG_STD,IS_BY_PRODUCT,IRR_REJECT,IRR_WASTAGE_QTY  FROM TEMP_IB_ITEM_STOCK WHERE ITEM_GROUP_ID !=1111 ");
                DBQuery dbqtemp = new DBQuery();
                //DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);

                foreach (DataRow dRow in dtData.Rows)
                {
                    rcMaterialStock stk = new rcMaterialStock();
                    stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();

                    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);

                    stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                    stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();
                    stk.OP_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_IRR_QTY"]);
                    stk.OP_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ITC_QTY"]);
                    stk.OPPENING_BAL_QTY = stk.OP_IRR_QTY - stk.OP_ITC_QTY;

                    stk.IRR_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_DEPT_QTY"]);
                    stk.IRR_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_STORE_QTY"]);
                    stk.IRR_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_PROD_QTY"]);

                    //here total IRR quantity
                    stk.IRR_BAL_QTY = stk.IRR_DEPT_QTY + stk.IRR_STORE_QTY + stk.IRR_PROD_QTY;
                    stk.ITC_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_DEPT_QTY"]);
                    stk.ITC_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_STORE_QTY"]);
                    stk.ITC_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_PROD_QTY"]);

                    stk.IRR_REJECT = Conversion.DBNullDecimalToZero(dRow["IRR_REJECT"]);


                    //here total ITC quantity
                    stk.ITC_BAL_QTY = stk.ITC_DEPT_QTY + stk.ITC_STORE_QTY + stk.ITC_PROD_QTY;

                    //here total Adjust balance quantity
                    stk.ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);

                    stk.FORMATION_ISS_QTY = Conversion.DBNullDecimalToZero(dRow["FORMATION_ISS_QTY"]);
                    stk.ASSEMBLY_ISS_QTY = Conversion.DBNullDecimalToZero(dRow["ASSEMBLY_ISS_QTY"]);

                    stk.IS_BY_PRODUCT = dRow["IS_BY_PRODUCT"].ToString();

                    stk.DEPARTMENT_NAME = prmINV.From_Dept_Name;
                    // stk.BAL_QTY = Conversion.DBNullDecimalToZero(dRow["BAL_QTY"]);
                    stk.COLISING_QTY = (stk.OPPENING_BAL_QTY + stk.ADJUST_QTY + stk.IRR_BAL_QTY) - stk.ITC_BAL_QTY - stk.IRR_REJECT;
                    stk.ITEM_STANDARD_WEIGHT_KG = Conversion.DBNullDecimalToZero(dRow["GRID_PC_KG_STD"]);
                    stk.PASTE_PC_KG = Conversion.DBNullDecimalToZero(dRow["PASTE_PC_KG_STD"]);
                    stk.PASTED_PLATE_PC_KG_STD = (stk.ITEM_STANDARD_WEIGHT_KG + stk.PASTE_PC_KG);
                    stk.TOTAL_PASTED_PLATE_PC_KG_STD = stk.PASTED_PLATE_PC_KG_STD * stk.IRR_BAL_QTY;
                    stk.TOTAL_PASTE_PC_KG = stk.IRR_BAL_QTY * stk.PASTE_PC_KG;

                    stk.IRR_WASTAGE_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_WASTAGE_QTY"]);

                    stk.CLOSING_QTY_wt = stk.COLISING_QTY * stk.PASTED_PLATE_PC_KG_STD;
                    cRptList.Add(stk);

                    //if (stk.OPPENING_BAL_QTY==0)
                    //{
                    //    if (stk.IRR_BAL_QTY != 0 || stk.ITC_BAL_QTY != 0 || stk.COLISING_QTY!=0)
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




        #endregion

        #region Assembly_Report
        public static List<rcMaterialStock> Assembly_Raw_Material_Report(clsPrmInventory prmINV)
        {
            return Assembly_Raw_Material_Report(prmINV, null);
        }

        public static List<rcMaterialStock> Assembly_Raw_Material_Report(clsPrmInventory prmINV, DBContext dc)
        {

            List<rcMaterialStock> cRptList = new List<rcMaterialStock>();


            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();

                if (prmINV.ToDate.HasValue)
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.ToDate.Value);
                }
                cmdInfo.DBParametersInfo.Add(":p_itemtype_id", prmINV.itemtype_id);
                cmdInfo.DBParametersInfo.Add(":p_itemGroup_id", prmINV.itemGroup_id);
                cmdInfo.DBParametersInfo.Add(":p_item_id", prmINV.item_id);
                cmdInfo.DBParametersInfo.Add(":p_store_id", prmINV.store_id);
                cmdInfo.DBParametersInfo.Add(":p_itemclass_id", prmINV.ItemClassId);
                cmdInfo.DBParametersInfo.Add(":p_battery_type_id", prmINV.Battery_Type_ID);
                cmdInfo.DBParametersInfo.Add(":p_is_repair", prmINV.Is_Repair);
                cmdInfo.DBParametersInfo.Add(":p_dept_id", prmINV.DeptID);
                cmdInfo.DBParametersInfo.Add(":p_is_finished", prmINV.is_Finished);

                DBQuery dbq = new DBQuery();

                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = "SP_ASSEMBLY_RAW_STOCK_REPORT";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);
                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;

                sb.Append(" Select ITEM_GROUP_ID,ITEM_GROUP_NAME,ITEM_ID,ITEM_NAME,UOM_ID,UOM_NAME,ITEM_TYPE_ID,ITEM_TYPE_NAME,ITEM_CLASS_ID,ITEM_CLASS_NAME,OP_IRR_QTY,OP_ITC_QTY,IRR_DEPT_QTY,IRR_STORE_QTY,IRR_PROD_QTY,ITC_DEPT_QTY,ITC_STORE_QTY,ITC_PROD_QTY,ADJUST_QTY FROM TEMP_ASSEMBLY_ITEM_STOCK");
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
                    rcMaterialStock stk = new rcMaterialStock();

                    stk.DEPARTMENT_NAME = prmINV.From_Dept_Name;
                    stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();

                    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);

                    stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                    stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();
                    #region Opening Calculation
                    stk.OP_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_IRR_QTY"]);
                    stk.OP_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ITC_QTY"]);
                    stk.OPPENING_BAL_QTY = stk.OP_IRR_QTY - stk.OP_ITC_QTY;
                    #endregion

                    #region Transaction Data Within Date Range
                    //Transaction Within Date Range

                    stk.IRR_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_DEPT_QTY"]);
                    stk.IRR_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_STORE_QTY"]);
                    stk.IRR_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_PROD_QTY"]);

                    //here total IRR quantity
                    stk.IRR_BAL_QTY = stk.IRR_DEPT_QTY + stk.IRR_STORE_QTY + stk.IRR_PROD_QTY;

                    stk.ITC_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_DEPT_QTY"]);
                    stk.ITC_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_STORE_QTY"]);
                    stk.ITC_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_PROD_QTY"]);

                    //here total ITC quantity
                    stk.ITC_BAL_QTY = stk.ITC_DEPT_QTY + stk.ITC_STORE_QTY + stk.ITC_PROD_QTY;

                    //here total Adjust balance quantity
                    stk.ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);
                    #endregion


                    #region Closing Calculation
                    //stk.COLISING_QTY = (stk.OPPENING_BAL_QTY + stk.ADJUST_QTY + stk.IRR_BAL_QTY) - stk.ITC_BAL_QTY;
                    stk.COLISING_QTY = (stk.IRR_BAL_QTY) - stk.ITC_BAL_QTY;
                    #endregion

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



        public static List<rcMaterialStock> Assembly_Raw_Material_Summary_Report(clsPrmInventory prmINV)
        {
            return Assembly_Raw_Material_Summary_Report(prmINV, null);
        }

        public static List<rcMaterialStock> Assembly_Raw_Material_Summary_Report(clsPrmInventory prmINV, DBContext dc)
        {

            List<rcMaterialStock> cRptList = new List<rcMaterialStock>();


            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();

                if (prmINV.ToDate.HasValue)
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.ToDate.Value);
                }
                cmdInfo.DBParametersInfo.Add(":p_itemtype_id", prmINV.itemtype_id);
                cmdInfo.DBParametersInfo.Add(":p_itemGroup_id", prmINV.itemGroup_id);
                cmdInfo.DBParametersInfo.Add(":p_item_id", prmINV.item_id);
                cmdInfo.DBParametersInfo.Add(":p_store_id", prmINV.store_id);
                cmdInfo.DBParametersInfo.Add(":p_itemclass_id", prmINV.ItemClassId);
                cmdInfo.DBParametersInfo.Add(":p_battery_type_id", prmINV.Battery_Type_ID);
                cmdInfo.DBParametersInfo.Add(":p_is_repair", prmINV.Is_Repair);
                cmdInfo.DBParametersInfo.Add(":p_dept_id", prmINV.DeptID);
                cmdInfo.DBParametersInfo.Add(":p_is_finished", prmINV.is_Finished);

                DBQuery dbq = new DBQuery();

                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

               // cmdInfo.CommandText = "SP_SOLAR_RAW_STOCK_REPORT";
                cmdInfo.CommandText = "SP_ASSEMBLY_RAW_STOCK_REPORT";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);
                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;

               // sb.Append(" Select ITEM_GROUP_ID,ITEM_GROUP_NAME,ITEM_ID,ITEM_NAME,UOM_ID,UOM_NAME,ITEM_TYPE_ID,ITEM_TYPE_NAME,ITEM_CLASS_ID,ITEM_CLASS_NAME,OP_IRR_QTY,OP_ITC_QTY,IRR_DEPT_QTY,IRR_STORE_QTY,IRR_PROD_QTY,ITC_DEPT_QTY,ITC_STORE_QTY,ITC_PROD_QTY,ADJUST_QTY,OP_ADJUST_QTY,CUR_REJECT_QTY FROM TEMP_ASSEMBLY_ITEM_STOCK  WHERE ITEM_GROUP_ID!=1111 order by  ITEM_GROUP_ID,ITEM_NAME");
                //sb.Append(" SELECT * FROM TEMP_SOLAR_RM_ITEM_STOCK ");
                sb.Append(" SELECT * FROM TEMP_ASSEMBLY_ITEM_STOCK "); 
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
                    rcMaterialStock stk = new rcMaterialStock();
                    stk.DEPARTMENT_NAME = prmINV.From_Dept_Name;
                    stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();

                    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);

                    stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                    stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();

                    #region Opening Calculation
                    stk.OP_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_IRR_QTY"]);
                    stk.OP_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ITC_QTY"]);
                    stk.OP_ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ADJUST_QTY"]);
                    stk.OPPENING_BAL_QTY = stk.OP_IRR_QTY + stk.OP_ADJUST_QTY - stk.OP_ITC_QTY;
                    #endregion

                    #region Transaction Data Within Date Rang
                  
                    stk.IRR_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_STORE_QTY"]);
                    stk.IRR_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_DEPT_QTY"]);
                    stk.IRR_BAL_QTY = stk.IRR_STORE_QTY + stk.IRR_DEPT_QTY;

                    stk.TOT_RECEIVE_QNTY = stk.OPPENING_BAL_QTY + stk.IRR_BAL_QTY;   
                
                    stk.ITC_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_STORE_QTY"]);
                    stk.ITC_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_PROD_QTY"]);                  
                    stk.ITC_BAL_QTY = stk.ITC_STORE_QTY + stk.ITC_PROD_QTY;

                    //here total Adjust balance quantity
                    stk.CUR_ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);
                    stk.ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);
                    stk.CUR_REJECT_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_REJECT_QTY"]);                  
                    #endregion

                    #region Closing Calculation
                    stk.COLISING_QTY = stk.OPPENING_BAL_QTY + stk.IRR_BAL_QTY + stk.CUR_ADJUST_QTY - (stk.ITC_BAL_QTY + stk.CUR_REJECT_QTY);
                    #endregion
                    stk.ORDER_NO = Conversion.StringToInt(dRow["ORDER_NO"].ToString());

                    //if (stk.OPPENING_BAL_QTY != 0 || stk.COLISING_QTY != 0 || stk.ITC_BAL_QTY != 0 || stk.IRR_BAL_QTY != 0)
                    if (stk.OPPENING_BAL_QTY != 0 ||  stk.IRR_STORE_QTY != 0 || stk.IRR_DEPT_QTY != 0 || stk.ITC_PROD_QTY != 0)
                    {
                        cRptList.Add(stk);
                    }
                   

                }




            }
            //catch { throw; }
            //finally 
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }

            return cRptList;

        }



        public static List<rcAssemblyFinishedStock> AssemblyFinishedGoodsProductionReport(clsPrmInventory prmINV)
        {
            return AssemblyFinishedGoodsProductionReport(prmINV, null);
        }

        public static List<rcAssemblyFinishedStock> AssemblyFinishedGoodsProductionReport(clsPrmInventory prmINV, DBContext dc)
        {

            List<rcAssemblyFinishedStock> cRptList = new List<rcAssemblyFinishedStock>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();

                if (prmINV.ToDate.HasValue)
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.ToDate.Value);
                }
                cmdInfo.DBParametersInfo.Add(":p_itemtype_id", prmINV.itemtype_id);
                cmdInfo.DBParametersInfo.Add(":p_itemGroup_id", prmINV.itemGroup_id);
                cmdInfo.DBParametersInfo.Add(":p_item_id", prmINV.item_id);
                cmdInfo.DBParametersInfo.Add(":p_store_id", prmINV.store_id);
                cmdInfo.DBParametersInfo.Add(":p_itemclass_id", prmINV.ItemClassId);
                cmdInfo.DBParametersInfo.Add(":p_battery_type_id", prmINV.Battery_Type_ID);
                cmdInfo.DBParametersInfo.Add(":p_is_repair", prmINV.Is_Repair);
                cmdInfo.DBParametersInfo.Add(":p_dept_id", prmINV.DeptID);
                cmdInfo.DBParametersInfo.Add(":p_is_finished", prmINV.is_Finished);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = "SP_ASSEMBLY_PROD_REPORT_N";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);

                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;              
                sb.Append(" Select ITEM_GROUP_ID,ITEM_GROUP_NAME,ITEM_ID,ITEM_NAME,UOM_ID,UOM_NAME,ITEM_TYPE_ID,ITEM_TYPE_NAME ");
                sb.Append(" ,ITEM_CLASS_ID,ITEM_CLASS_NAME,OP_GD_IRR_QTY,OP_GD_ITC_QTY,IRR_DEPT_QTY,IRR_STORE_QTY,IRR_PROD_QTY ");
                sb.Append(" ,ITC_SOLAR_QTY,ITC_STORE_QTY,ITC_RETURN_BAT_QTY,ADJUST_QTY,OP_ASSEMBLY_QTY,OP_PACKING_QTY ");
                sb.Append(" ,OP_PACKING_ADJUST_QTY,OP_PACKING_RCV_QTY,OP_PACKING_ISS_QTY,OP_ASSEMBEL_ADJUST_QTY,OP_ASSEMBEL_RCV_QTY ");
                sb.Append(" ,OP_ASSEMBEL_ISS_QTY,CUR_ASSEM_PROD_QTY,CUR_ASSEM_USED_QTY,CUR_ASSEM_ADJUST_QTY,CUR_PACKING_ADJUST_QTY  ");
                sb.Append(" ,OP_P_BAT_IRR_QTY,OP_P_BAT_ITC_QTY,OP_P_BAT_BAL_QTY,CUR_P_BAT_IRR_QTY,CUR_P_BAT_ITC_QTY,OP_SERVICE_BAT_IRR_QTY ");
                sb.Append(" ,OP_SERVICE_BAT_ITC_QTY,OP_SERVICE_BAT_BAL_QTY,CUR_SERVICE_BAT_IRR_QTY,CUR_SERVICE_BAT_ITC_QTY,OP_RND_BAT_IRR_QTY ");
                sb.Append(" ,OP_RND_BAT_ITC_QTY,OP_RND_BAT_BAL_QTY,CUR_RND_BAT_IRR_QTY,CUR_RND_BAT_ITC_QTY,OP_OTHERS_BAT_IRR_QTY ");
                sb.Append(" ,OP_OTHERS_BAT_ITC_QTY,OP_OTHERS_BAT_BAL_QTY,CUR_OTHERS_BAT_IRR_QTY,CUR_OTHERS_BAT_ITC_QTY,OP_MR_IRR_QTY ");
                sb.Append(" ,OP_MR_ITC_QTY,OP_MR_BAL_QTY,IRR_RETURN_BAT_QTY,BATERY_CAT_DESCR,BATERY_CAT_SL_NO,ITEM_ORDER,CUR_CON_IRR_QTY,CONVERT_BAT_ISS,CONVERT_BAT_RCV  ");
                sb.Append(" FROM TEMP_ASSEMBLY_FG_STOCK ");
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
                    stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();
                    stk.ITEM_ORDER = Conversion.DBNullIntToZero(dRow["ITEM_ORDER"]);
                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();

                    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);

                    stk.BATERY_CAT_SL_NO = Conversion.DBNullIntToZero(dRow["BATERY_CAT_SL_NO"]);
                    stk.BATERY_CAT_DESCR = dRow["BATERY_CAT_DESCR"].ToString();

                    stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                    stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();

                    stk.OP_GD_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_GD_IRR_QTY"]);
                    stk.OP_GD_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_GD_ITC_QTY"]);
                    stk.OP_GD_BAL = stk.OP_GD_IRR_QTY - stk.OP_GD_ITC_QTY;
                    //stk.OP_ASSEMBLY_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ASSEMBLY_QTY"]);

                    //here Packing quantity opening calculation

                    stk.OP_PACKING_RCV_QTY = Conversion.DBNullDecimalToZero(dRow["OP_PACKING_RCV_QTY"]);
                    stk.OP_PACKING_ISS_QTY = Conversion.DBNullDecimalToZero(dRow["OP_PACKING_ISS_QTY"]);
                    stk.OP_PACKING_ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["OP_PACKING_ADJUST_QTY"]);
                    stk.OP_PACKING_BAL_QTY = stk.OP_PACKING_RCV_QTY + stk.OP_PACKING_ADJUST_QTY - stk.OP_PACKING_ISS_QTY;

                    //Here Assembly Quantity Opening calculation                    
                    stk.OP_ASSEMBEL_RCV_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ASSEMBEL_RCV_QTY"]);

                    //Here issue to solar only
                    stk.OP_ASSEMBEL_ISS_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ASSEMBEL_ISS_QTY"]);
                    stk.OP_ASSEMBEL_ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ASSEMBEL_ADJUST_QTY"]);
                    //stk.OP_ASSEMBEL_BAL_QTY = stk.OP_ASSEMBEL_ADJUST_QTY + stk.OP_ASSEMBEL_RCV_QTY - stk.OP_ASSEMBEL_ISS_QTY;
                    stk.OP_ASSEMBEL_BAL_QTY = stk.OP_ASSEMBEL_ADJUST_QTY + stk.OP_ASSEMBEL_RCV_QTY - (stk.OP_PACKING_RCV_QTY + stk.OP_PACKING_ADJUST_QTY + stk.OP_ASSEMBEL_ISS_QTY);

                    stk.OP_PACKING_QTY = Conversion.DBNullDecimalToZero(dRow["OP_PACKING_QTY"]);
                    stk.OP_GD_WIP = stk.OP_ASSEMBLY_QTY - stk.OP_PACKING_RCV_QTY;//here calculation will be total assembly-total packing declaration

                    stk.IRR_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_DEPT_QTY"]);
                    stk.IRR_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_STORE_QTY"]);
                    stk.CUR_PACKING_RCV_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_PROD_QTY"]);//Here Packing quantity receive from production
                    stk.CUR_PACKING_ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_PACKING_ADJUST_QTY"]);
                    stk.CUR_CON_IRR_QTY = Conversion.DBNullIntToZero(dRow["CUR_CON_IRR_QTY"]);


                    stk.CUR_ASSEM_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_ASSEM_PROD_QTY"]);
                    stk.CUR_ASSEM_USED_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_ASSEM_USED_QTY"]);
                    stk.CUR_ASSEM_ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_ASSEM_ADJUST_QTY"]);

                    //here total IRR quantity
                    //stk.IRR_BAL_QTY = stk.IRR_DEPT_QTY + stk.IRR_STORE_QTY + stk.IRR_PROD_QTY;

                    stk.ITC_SOLAR_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_SOLAR_QTY"]);
                    stk.ITC_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_STORE_QTY"]);
                    stk.ITC_RETURN_BAT_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_RETURN_BAT_QTY"]);

                      /// Battery Conversion RCV 
                    stk.CONVERT_BAT_RCV = Conversion.DBNullIntToZero(dRow["CONVERT_BAT_RCV"]);

                    /// Battery Conversion Issue 
                    stk.CONVERT_BAT_ISS = Conversion.DBNullIntToZero(dRow["CONVERT_BAT_ISS"]);

                    //here total ITC quantity
                    //stk.ITC_BAL_QTY = stk.ITC_SOLAR_QTY + stk.ITC_STORE_QTY + stk.ITC_RETURN_BAT_QTY;
                    //+ stk.CONVERT_BAT_RCV

                    stk.OP_CUR_TOT_PACKING_QTY = stk.OP_PACKING_BAL_QTY + stk.CUR_PACKING_RCV_QTY + stk.CUR_PACKING_ADJUST_QTY + stk.IRR_STORE_QTY ;
                    stk.OP_CUR_TOT_ASSEMBLY_QTY = stk.OP_ASSEMBEL_BAL_QTY + stk.CUR_ASSEM_PROD_QTY + stk.CUR_ASSEM_ADJUST_QTY - (stk.CUR_PACKING_RCV_QTY + stk.CUR_PACKING_ADJUST_QTY);

                    stk.ASSEM_CLOSING_QTY = stk.OP_CUR_TOT_ASSEMBLY_QTY - stk.ITC_SOLAR_QTY - stk.CUR_OTHERS_BAT_ITC_QTY + stk.CUR_CON_IRR_QTY;  // Current Convert

                    stk.PACKING_CLOSING_QTY = stk.OP_CUR_TOT_PACKING_QTY - stk.ITC_STORE_QTY - stk.ITC_RETURN_BAT_QTY - stk.CONVERT_BAT_ISS;
                    //if (stk.ITEM_ID == 7998)
                    //    stk.PACKING_CLOSING_QTY = stk.OP_CUR_TOT_PACKING_QTY - stk.ITC_STORE_QTY - 22;
                    //else
                     //stk.PACKING_CLOSING_QTY = stk.OP_CUR_TOT_PACKING_QTY - stk.ITC_STORE_QTY;

                    //here total Adjust balance quantity
                    stk.ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);
                    stk.DEPARTMENT_NAME = prmINV.From_Dept_Name;

                    // P,SERVICE,RND,RETURN,OTHERS BATTERYS

                    stk.IRR_RETURN_BAT_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_RETURN_BAT_QTY"]);
                    //stk.OP_GD_WIP = stk.OP_ASSEMBLY_QTY - stk.OP_PACKING_RCV_QTY;//here calculation will be total assembly-total packing declaration

                    stk.OP_MR_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_MR_IRR_QTY"]);
                    stk.OP_MR_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_MR_ITC_QTY"]);
                    stk.OP_MR_BAL_QTY = Conversion.DBNullDecimalToZero(dRow["OP_MR_BAL_QTY"]);//Here Packing quantity receive from production
                    stk.OP_P_BAT_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_P_BAT_IRR_QTY"]);

                    stk.OP_P_BAT_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_P_BAT_ITC_QTY"]);
                    stk.OP_P_BAT_BAL_QTY = Conversion.DBNullDecimalToZero(dRow["OP_P_BAT_BAL_QTY"]);
                    stk.CUR_P_BAT_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_P_BAT_IRR_QTY"]);

                    //here total IRR quantity
                    //stk.IRR_BAL_QTY = stk.IRR_DEPT_QTY + stk.IRR_STORE_QTY + stk.IRR_PROD_QTY;

                    stk.CUR_P_BAT_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_P_BAT_ITC_QTY"]);
                    stk.OP_SERVICE_BAT_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_SERVICE_BAT_IRR_QTY"]);
                    stk.OP_SERVICE_BAT_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_SERVICE_BAT_ITC_QTY"]);

                    stk.OP_SERVICE_BAT_BAL_QTY = Conversion.DBNullDecimalToZero(dRow["OP_SERVICE_BAT_BAL_QTY"]);
                    //stk.OP_GD_WIP = stk.OP_ASSEMBLY_QTY - stk.OP_PACKING_RCV_QTY;//here calculation will be total assembly-total packing declaration

                    stk.CUR_SERVICE_BAT_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_SERVICE_BAT_IRR_QTY"]);
                    stk.CUR_SERVICE_BAT_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_SERVICE_BAT_ITC_QTY"]);
                    stk.OP_RND_BAT_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_RND_BAT_IRR_QTY"]);//Here Packing quantity receive from production
                    stk.OP_RND_BAT_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_RND_BAT_ITC_QTY"]);

                    stk.OP_RND_BAT_BAL_QTY = Conversion.DBNullDecimalToZero(dRow["OP_RND_BAT_BAL_QTY"]);
                    stk.CUR_RND_BAT_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_RND_BAT_IRR_QTY"]);
                    stk.CUR_RND_BAT_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_RND_BAT_ITC_QTY"]);

                    //here total IRR quantity
                    //stk.IRR_BAL_QTY = stk.IRR_DEPT_QTY + stk.IRR_STORE_QTY + stk.IRR_PROD_QTY;

                    stk.OP_OTHERS_BAT_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_OTHERS_BAT_IRR_QTY"]);
                    stk.OP_OTHERS_BAT_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_OTHERS_BAT_ITC_QTY"]);
                    stk.OP_OTHERS_BAT_BAL_QTY = Conversion.DBNullDecimalToZero(dRow["OP_OTHERS_BAT_IRR_QTY"]) - Conversion.DBNullDecimalToZero(dRow["OP_OTHERS_BAT_ITC_QTY"]);

                    stk.CUR_OTHERS_BAT_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_OTHERS_BAT_IRR_QTY"]);
                    stk.CUR_OTHERS_BAT_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_OTHERS_BAT_ITC_QTY"]);

                  

                    //stock
                    stk.OP_CUR_TOT_ASSEMBLY_DEPT_QTY = stk.OP_ASSEMBEL_BAL_QTY + stk.CUR_ASSEM_PROD_QTY + stk.CUR_ASSEM_ADJUST_QTY
                        - (stk.CUR_PACKING_RCV_QTY + stk.CUR_PACKING_ADJUST_QTY) + stk.OP_MR_BAL_QTY + stk.OP_P_BAT_BAL_QTY + stk.OP_SERVICE_BAT_BAL_QTY 
                        + stk.OP_RND_BAT_BAL_QTY + stk.OP_OTHERS_BAT_BAL_QTY + stk.IRR_RETURN_BAT_QTY + stk.CUR_P_BAT_IRR_QTY + stk.CUR_SERVICE_BAT_IRR_QTY
                        + stk.CUR_RND_BAT_IRR_QTY + stk.CUR_OTHERS_BAT_IRR_QTY + stk.CUR_CON_IRR_QTY;  // Convert Battery

                    stk.OP_TOTAL_ASS_PROD_BAT_QRT = stk.OP_ASSEMBEL_BAL_QTY + stk.OP_MR_BAL_QTY + stk.OP_P_BAT_BAL_QTY + stk.OP_SERVICE_BAT_BAL_QTY + stk.OP_RND_BAT_BAL_QTY + stk.OP_OTHERS_BAT_BAL_QTY;
                    stk.CUR_TOTAL_BAT_PROD_QTY = stk.CUR_ASSEM_PROD_QTY + stk.IRR_RETURN_BAT_QTY + stk.CUR_P_BAT_IRR_QTY + stk.CUR_SERVICE_BAT_IRR_QTY + stk.CUR_RND_BAT_IRR_QTY + stk.CUR_OTHERS_BAT_IRR_QTY;
//
                    stk.TOTAL_DELIVERY_TO_MRB = stk.CUR_SERVICE_BAT_ITC_QTY + stk.CUR_P_BAT_ITC_QTY+ stk.ITC_RETURN_BAT_QTY ;

                    stk.TOTAL_DELIVERY_FROM_ASSEMBLY = stk.TOTAL_DELIVERY_TO_MRB + stk.CUR_OTHERS_BAT_ITC_QTY + stk.CUR_RND_BAT_ITC_QTY + stk.ITC_SOLAR_QTY + stk.ITC_STORE_QTY;
//- stk.ITC_RETURN_BAT_QTY
                    stk.MR_BATTERY_CLOSING_QTY = stk.OP_MR_BAL_QTY + stk.IRR_RETURN_BAT_QTY ;
                    //if (stk.ITEM_ID == 7998)
                    //    stk.SERVICE_BATTERY_CLOSING_QTY = stk.OP_SERVICE_BAT_BAL_QTY + stk.CUR_SERVICE_BAT_IRR_QTY - stk.CUR_SERVICE_BAT_ITC_QTY + 22;
                    //else
                        stk.SERVICE_BATTERY_CLOSING_QTY = stk.OP_SERVICE_BAT_BAL_QTY + stk.CUR_SERVICE_BAT_IRR_QTY - stk.CUR_SERVICE_BAT_ITC_QTY;
                    stk.RND_BATTERY_CLOSING_QTY = stk.OP_RND_BAT_BAL_QTY + stk.CUR_RND_BAT_IRR_QTY - stk.CUR_RND_BAT_ITC_QTY;
                    stk.OTHERS_BATTERY_CLOSING_QTY = stk.OP_OTHERS_BAT_BAL_QTY + stk.CUR_OTHERS_BAT_IRR_QTY  - stk.CUR_OTHERS_BAT_ITC_QTY;
                    stk.P_BATTERY_CLOSING_QTY = stk.OP_P_BAT_BAL_QTY + stk.CUR_P_BAT_IRR_QTY - stk.CUR_P_BAT_ITC_QTY;

                    stk.TOTAL_ASSEMBLY_CLOSING_QTY = stk.ASSEM_CLOSING_QTY + stk.MR_BATTERY_CLOSING_QTY + stk.SERVICE_BATTERY_CLOSING_QTY + stk.RND_BATTERY_CLOSING_QTY + stk.OTHERS_BATTERY_CLOSING_QTY + stk.P_BATTERY_CLOSING_QTY; 

                    stk.TOTAL_ASSEMBLY_OPENING_QTY = stk.OP_ASSEMBEL_ADJUST_QTY + stk.OP_ASSEMBEL_RCV_QTY - (stk.OP_PACKING_RCV_QTY + stk.OP_PACKING_ADJUST_QTY + stk.OP_ASSEMBEL_ISS_QTY) + stk.OP_MR_BAL_QTY + stk.OP_P_BAT_BAL_QTY + stk.OP_SERVICE_BAT_BAL_QTY + stk.OP_RND_BAT_BAL_QTY + stk.OP_OTHERS_BAT_BAL_QTY;

                    if (stk.OP_PACKING_BAL_QTY > 0 || stk.OP_ASSEMBEL_BAL_QTY > 0 || stk.CUR_PACKING_RCV_QTY > 0 || stk.CUR_ASSEM_PROD_QTY > 0 || stk.ITC_SOLAR_QTY > 0 || stk.ITC_STORE_QTY > 0 || stk.OP_CUR_TOT_PACKING_QTY > 0 || stk.OP_CUR_TOT_ASSEMBLY_QTY > 0 || stk.TOTAL_ASSEMBLY_CLOSING_QTY > 0 || stk.OP_MR_BAL_QTY > 0 || stk.MR_BATTERY_CLOSING_QTY > 0 || stk.IRR_RETURN_BAT_QTY > 0 || stk.CUR_P_BAT_IRR_QTY > 0)
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

        #region ***************VRLA Charging Product Report********************************************
        public static List<rcMaterialStock> VRLAChargingGoodsReport(clsPrmInventory prmINV, DBContext dc)
        {
            List<rcMaterialStock> cRptList = new List<rcMaterialStock>();
            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();

                if (prmINV.ToDate.HasValue)
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.ToDate.Value);
                }
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
                cmdInfo.CommandText = "SP_VRLA_CHARGE_PROD_REPORT";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);

                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;
                sb.Append(" SELECT * FROM  TEMP_SOLAR_ITEM_STOCK ");
                DBQuery dbqtemp = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);

                foreach (DataRow dRow in dtData.Rows)
                {
                    rcMaterialStock stk = new rcMaterialStock();
                    stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();

                    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);

                    stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                    stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();
                    stk.ORDER_NO = Conversion.StringToInt(dRow["ORDER_NO"].ToString());

                    stk.DEPARTMENT_NAME = prmINV.From_Dept_Name;

                    stk.BRAND_NAME = dRow["BRAND_NAME"].ToString();
                    stk.BATERY_CAT_DESCR = dRow["BATERY_CAT_DESCR"].ToString();
                    stk.ITEM_ORDER = Conversion.StringToInt(dRow["ITEM_ORDER"].ToString());
                    stk.BATERY_CAT_SL_NO = Conversion.StringToInt(dRow["BATERY_CAT_SL_NO"].ToString());
                    stk.BRAND_SL_NO = Conversion.StringToInt(dRow["BRAND_SL_NO"].ToString());
                    #region Opening

                    stk.OP_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_IRR_QTY"]);
                    stk.OP_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ITC_QTY"]);
                    stk.OP_IRR_REJECT_QTY = Conversion.DBNullDecimalToZero(dRow["OP_IRR_REJECT_QTY"]);
                    stk.OPPENING_BAL_QTY = stk.OP_IRR_QTY - stk.OP_ITC_QTY - stk.OP_IRR_REJECT_QTY;   // Total Opening
                    #endregion

                    #region Delivery
                    // ITC TO department
                    stk.ITC_RB_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_RB_QTY"]);
                    stk.ITC_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_STORE_QTY"]);
                    stk.ITC_BAL_QTY = stk.ITC_RB_QTY + stk.ITC_STORE_QTY;
                    #endregion

                    #region    Full Month Packing
                    stk.FULL_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["FULL_IRR_QTY"]);
                    stk.FULL_LOADING = Conversion.DBNullDecimalToZero(dRow["FULL_LOADING"]);
                    stk.FULL_PAC_QTY = Conversion.DBNullDecimalToZero(dRow["FULL_PAC_QTY"]);
                    #endregion

                    #region Transaction_Within_date

                    stk.OP_PACKET_QTY = Conversion.DBNullDecimalToZero(dRow["OP_PACKET_QTY"]);
                    stk.CUR_PACKED_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_PACKED_QTY"]);
                    stk.CHARGE_PAC_QTY = (stk.OP_PACKET_QTY + stk.CUR_PACKED_QTY) - stk.ITC_BAL_QTY;   // Packed Qty 1


                    stk.CUR_UN_PACKED_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_UN_PACKED_QTY"]);
                    stk.OP_UN_PACKED_QTY = Conversion.DBNullDecimalToZero(dRow["OP_UN_PACKED_QTY"]);
                    stk.TOTAL_UN_PACKED_QTY = (stk.CUR_UN_PACKED_QTY + stk.OP_UN_PACKED_QTY) - stk.FULL_PAC_QTY; // UnPacked Qty 2

                    stk.TOTAL_CHARGED_QTY = stk.CHARGE_PAC_QTY + stk.TOTAL_UN_PACKED_QTY;  // Charge Stock   3


                    // stk.OP_GREEN_BAT_CHARGED_QTY = Conversion.DBNullDecimalToZero(dRow["OP_GREEN_BAT_CHARGED_QTY"]);
                    stk.CUR_CHARGED_GREEN_BAT_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_CHARGED_GREEN_BAT_QTY"]);

                    stk.TOTAL_UNCHARGED_GP_QTY = (Conversion.DBNullDecimalToZero(dRow["OP_IRR_GP_BAT_QTY"]) + Conversion.DBNullDecimalToZero(dRow["CUR_IRR_GP_BAT_QTY"])) - stk.CUR_CHARGED_GREEN_BAT_QTY;  // Uncharged Green Plate 4

                    stk.CUR_CHARGED_DRY_BAT_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_CHARGED_DRY_BAT_QTY"]);
                    stk.TOTAL_UNCHARGED_DRY_QTY = (Conversion.DBNullDecimalToZero(dRow["OP_IRR_FORM_BAT_QTY"])
                       + Conversion.DBNullDecimalToZero(dRow["CUR_IRR_FORM_BAT_QTY"])) - stk.CUR_CHARGED_DRY_BAT_QTY;  // Uncharged Dry Plate 5

                    stk.TOTAL_UNCHARGED_QTY = stk.TOTAL_UNCHARGED_GP_QTY + stk.TOTAL_UNCHARGED_DRY_QTY; // Total Uncharged Plate 6
                    // Openning ON Charging
                    stk.OP_ON_CHARGING = Conversion.DBNullDecimalToZero(dRow["OP_ON_CHARGING"]);

                    stk.CUR_GREEN_BAT_LOADING_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_GREEN_BAT_LOADING_QTY"]);
                    stk.CUR_DRY_BAT_LOADING_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_DRY_BAT_LOADING_QTY"]);
                    stk.CUR_LOADING_QTY = stk.CUR_GREEN_BAT_LOADING_QTY + stk.CUR_DRY_BAT_LOADING_QTY;
                    //stk.OPPENING_BAL_QTY - stk.TOTAL_CHARGED_QTY - stk.TOTAL_UNCHARGED_QTY;
                    stk.TOTAL_ON_CHARGING = stk.OP_ON_CHARGING + stk.CUR_LOADING_QTY; // Total ON Charging 7 

                    //stk.CUR_CHARGED_DRY_BAT_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_CHARGED_DRY_BAT_QTY"]);
                    stk.CUR_CHARGED_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_CHARGED_QTY"]);   // Today Charged qty  10

                    //Green and Dry Plate Battery Reject Quantity
                    stk.CUR_REJECT_GREEN_BAT_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_REJECT_GREEN_BAT_QTY"]);
                    stk.CUR_REJECT_DRY_BAT_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_REJECT_DRY_BAT_QTY"]);
                    stk.CUR_REJECT_QTY = stk.CUR_REJECT_GREEN_BAT_QTY + stk.CUR_REJECT_DRY_BAT_QTY;

                    stk.CUR_UNLOADING_QTY = stk.CUR_CHARGED_QTY + stk.CUR_REJECT_QTY;//Unloading means total charged declaration and total rejection declaration

                    //Closing quantity.This law may be changed
                    stk.CLOSING_QTY = stk.OPPENING_BAL_QTY + stk.CUR_IRR_QTY + stk.CUR_LOADING_QTY - (stk.ITC_RB_QTY + stk.ITC_STORE_QTY);
                    // stk.OPPENING_BAL_QTY - (stk.ITC_RB_QTY + stk.ITC_STORE_QTY);
                    #endregion
                    //if (stk.OPPENING_BAL_QTY != 0 || stk.IRR_BAL_QTY != 0 || stk.ITC_BAL_QTY != 0 || stk.COLISING_QTY != 0)
                    //  {
                    cRptList.Add(stk);
                    // }

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

        public static List<rcMaterialStock> VRLAChargingGoodsSummaryReport(clsPrmInventory prmINV, DBContext dc)
        {
            List<rcMaterialStock> cRptList = new List<rcMaterialStock>();
            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();

                if (prmINV.ToDate.HasValue)
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.ToDate.Value);
                }
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
                cmdInfo.CommandText = "SP_VRLA_CHARGING_STOCK_REPORT";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);

                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;
                //sb.Append(" Select ITEM_GROUP_ID,ITEM_GROUP_NAME,ITEM_ID,ITEM_NAME,UOM_ID,UOM_NAME,ITEM_TYPE_ID,ITEM_TYPE_NAME,ITEM_CLASS_ID,ITEM_CLASS_NAME ");
                //sb.Append(" ,OP_IRR_QTY,OP_ITC_QTY, ITC_RB_QTY,ITC_STORE_QTY,ADJUST_QTY ");
                //sb.Append(" ,CUR_GREEN_BAT_RCV_QTY,CUR_DRY_BAT_RCV_QTY,CUR_LOADING_QTY,CUR_UNLOADING_QTY,CUR_GREEN_BAT_LOADING_QTY,CUR_DRY_BAT_LOADING_QTY ");
                //sb.Append(" ,CUR_PACKED_QTY,CUR_CHARGED_QTY,CUR_CHARGED_GREEN_BAT_QTY,CUR_CHARGED_DRY_BAT_QTY,CUR_REJECT_GREEN_BAT_QTY,CUR_REJECT_DRY_BAT_QTY ");
                //sb.Append(" ,OP_GREEN_BAT_LOADING_QTY,OP_DRY_BAT_LOADING_QTY ,ORDER_NO,OP_GREEN_BAT_CHARGED_QTY,OP_DRY_BAT_CHARGED_QTY,OP_PACKET_QTY,OP_CHARGE_QTY,CUR_IRR_QTY,ITC_OTHERS, REJECT_QTY_RCV ");
                //sb.Append(" FROM TEMP_SOLAR_ITEM_STOCK ");
                sb.Append(" SELECT * FROM TEMP_SOLAR_ITEM_STOCK ");
                DBQuery dbqtemp = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);

                foreach (DataRow dRow in dtData.Rows)
                {
                    rcMaterialStock stk = new rcMaterialStock();
                    stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();



                    stk.BATERY_CAT_SL_NO = Conversion.DBNullIntToZero(dRow["BATERY_CAT_SL_NO"]);
                    stk.BATERY_CAT_DESCR = dRow["BATERY_CAT_DESCR"].ToString();


                    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);

                    stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                    stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();
                    stk.ORDER_NO = Conversion.StringToInt(dRow["ORDER_NO"].ToString());
                    stk.DEPARTMENT_NAME = prmINV.From_Dept_Name;
                    stk.OP_IRR_REJECT_QTY = Conversion.DBNullIntToZero(dRow["OP_IRR_REJECT_QTY"]);

                    stk.OP_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_IRR_QTY"]);
                    stk.OP_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ITC_QTY"]);
                    stk.OPPENING_BAL_QTY = stk.OP_IRR_QTY - stk.OP_ITC_QTY - stk.OP_IRR_REJECT_QTY;

                    stk.CUR_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_IRR_QTY"]);

                    stk.BATT_CONVERT_FROM = Conversion.DBNullDecimalToZero(dRow["BATT_CONVERT_FROM"]);
                    stk.BATT_CONVERT_TO = Conversion.DBNullDecimalToZero(dRow["BATT_CONVERT_TO"]);

                    stk.TOT_RECEIVE_QNTY = stk.OPPENING_BAL_QTY + stk.CUR_IRR_QTY - stk.BATT_CONVERT_FROM + stk.BATT_CONVERT_TO;

                    stk.ITC_RB_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_RB_QTY"]);
                    stk.ITC_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_STORE_QTY"]);
                    stk.ITC_OTHERS = Conversion.DBNullDecimalToZero(dRow["ITC_OTHERS"]);
                    stk.ITC_BAL_QTY = stk.ITC_RB_QTY + stk.ITC_STORE_QTY + stk.ITC_OTHERS;

                    stk.REJECT_QTY_RCV = Conversion.DBNullDecimalToZero(dRow["CUR_REJECT_GREEN_BAT_QTY"]);
                    //Closing quantity.This law may be changed
                    stk.CLOSING_QTY = stk.TOT_RECEIVE_QNTY - stk.ITC_BAL_QTY - stk.REJECT_QTY_RCV;

                    //if (stk.OPPENING_BAL_QTY != 0 || stk.IRR_BAL_QTY != 0 || stk.ITC_BAL_QTY != 0 || stk.COLISING_QTY != 0)
                    //  {
                    cRptList.Add(stk);
                    // }


                }




            }
            //catch { throw; }
            //finally 
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }

            return cRptList;

        }



        #endregion



        #region *******************Solar_Finish_Product_Report ***********************

        public static List<rcMaterialStock> SolarFinishedGoodsProductionReport(clsPrmInventory prmINV)
        {
            return SolarFinishedGoodsProductionReport(prmINV, null);
        }

        public static List<rcMaterialStock> SolarFinishedGoodsProductionReport(clsPrmInventory prmINV, DBContext dc)
        {
            List<rcMaterialStock> cRptList = new List<rcMaterialStock>();
            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();

                if (prmINV.ToDate.HasValue)
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.ToDate.Value);
                }
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
                cmdInfo.CommandText = "SP_SOLAR_PRODUCTION_REPORT";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);

                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;
                //sb.Append(" Select ITEM_GROUP_ID,ITEM_GROUP_NAME,ITEM_ID,ITEM_NAME,UOM_ID,UOM_NAME,ITEM_TYPE_ID,ITEM_TYPE_NAME,ITEM_CLASS_ID,ITEM_CLASS_NAME ");
                //sb.Append(" ,OP_IRR_QTY,OP_ITC_QTY, ITC_RB_QTY,ITC_STORE_QTY,ADJUST_QTY ");
                //sb.Append(" ,CUR_GREEN_BAT_RCV_QTY,CUR_DRY_BAT_RCV_QTY,CUR_LOADING_QTY,CUR_UNLOADING_QTY,CUR_GREEN_BAT_LOADING_QTY,CUR_DRY_BAT_LOADING_QTY ");              
                //sb.Append(" ,CUR_PACKED_QTY,CUR_CHARGED_QTY,CUR_CHARGED_GREEN_BAT_QTY,CUR_CHARGED_DRY_BAT_QTY,CUR_REJECT_GREEN_BAT_QTY,CUR_REJECT_DRY_BAT_QTY ");
                //sb.Append(" ,OP_GREEN_BAT_LOADING_QTY,OP_DRY_BAT_LOADING_QTY ,ORDER_NO,OP_GREEN_BAT_CHARGED_QTY,OP_DRY_BAT_CHARGED_QTY,OP_PACKET_QTY,OP_CHARGE_QTY,CUR_IRR_QTY ");
                //sb.Append(" FROM TEMP_SOLAR_ITEM_STOCK ");

                sb.Append(" SELECT * FROM  TEMP_SOLAR_ITEM_STOCK ");

                DBQuery dbqtemp = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);

                foreach (DataRow dRow in dtData.Rows)
                {
                    rcMaterialStock stk = new rcMaterialStock();
                    stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();

                    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);

                    stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                    stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();
                    stk.ORDER_NO = Conversion.StringToInt( dRow["ORDER_NO"].ToString());

                    stk.DEPARTMENT_NAME = prmINV.From_Dept_Name;

                    stk.BRAND_NAME = dRow["BRAND_NAME"].ToString();
                    stk.BATERY_CAT_DESCR = dRow["BATERY_CAT_DESCR"].ToString();
                    stk.ITEM_ORDER = Conversion.StringToInt(dRow["ITEM_ORDER"].ToString());
                    stk.BATERY_CAT_SL_NO = Conversion.StringToInt(dRow["BATERY_CAT_SL_NO"].ToString());
                    stk.BRAND_SL_NO = Conversion.StringToInt(dRow["BRAND_SL_NO"].ToString());
                    #region Opening
                    
                    stk.OP_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_IRR_QTY"]);
                    stk.OP_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ITC_QTY"]);
                    stk.OP_IRR_REJECT_QTY = Conversion.DBNullDecimalToZero(dRow["OP_IRR_REJECT_QTY"]);
                    stk.OPPENING_BAL_QTY = stk.OP_IRR_QTY - stk.OP_ITC_QTY - stk.OP_IRR_REJECT_QTY;   // Total Opening
                    #endregion

                    #region Delivery 
                    // ITC TO department
                    stk.ITC_RB_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_RB_QTY"]);
                    stk.ITC_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_STORE_QTY"]);
                    stk.ITC_BAL_QTY = stk.ITC_RB_QTY + stk.ITC_STORE_QTY;
                    #endregion

                    #region    Full Month Packing
                    stk.FULL_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["FULL_IRR_QTY"]);
                    stk.FULL_LOADING = Conversion.DBNullDecimalToZero(dRow["FULL_LOADING"]);
                    stk.FULL_PAC_QTY = Conversion.DBNullDecimalToZero(dRow["FULL_PAC_QTY"]);
                    #endregion

                    #region Transaction_Within_date

                    stk.OP_PACKET_QTY = Conversion.DBNullDecimalToZero(dRow["OP_PACKET_QTY"]);
                    stk.CUR_PACKED_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_PACKED_QTY"]);
                    stk.CHARGE_PAC_QTY = (stk.OP_PACKET_QTY + stk.CUR_PACKED_QTY) - stk.ITC_BAL_QTY;   // Packed Qty 1


                    stk.CUR_UN_PACKED_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_UN_PACKED_QTY"]);
                    stk.OP_UN_PACKED_QTY = Conversion.DBNullDecimalToZero(dRow["OP_UN_PACKED_QTY"]);
                    stk.TOTAL_UN_PACKED_QTY = (stk.CUR_UN_PACKED_QTY + stk.OP_UN_PACKED_QTY) - stk.FULL_PAC_QTY; // UnPacked Qty 2

                    stk.TOTAL_CHARGED_QTY = stk.CHARGE_PAC_QTY + stk.TOTAL_UN_PACKED_QTY;  // Charge Stock   3


                   // stk.OP_GREEN_BAT_CHARGED_QTY = Conversion.DBNullDecimalToZero(dRow["OP_GREEN_BAT_CHARGED_QTY"]);
                    stk.CUR_CHARGED_GREEN_BAT_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_CHARGED_GREEN_BAT_QTY"]);

                    stk.TOTAL_UNCHARGED_GP_QTY = (Conversion.DBNullDecimalToZero(dRow["OP_IRR_GP_BAT_QTY"]) + Conversion.DBNullDecimalToZero(dRow["CUR_IRR_GP_BAT_QTY"])) - stk.CUR_CHARGED_GREEN_BAT_QTY;  // Uncharged Green Plate 4

                    stk.CUR_CHARGED_DRY_BAT_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_CHARGED_DRY_BAT_QTY"]);
                    stk.TOTAL_UNCHARGED_DRY_QTY =   (Conversion.DBNullDecimalToZero(dRow["OP_IRR_FORM_BAT_QTY"])  
                       + Conversion.DBNullDecimalToZero(dRow["CUR_IRR_FORM_BAT_QTY"]) )- stk.CUR_CHARGED_DRY_BAT_QTY;  // Uncharged Dry Plate 5

                    stk.TOTAL_UNCHARGED_QTY = stk.TOTAL_UNCHARGED_GP_QTY + stk.TOTAL_UNCHARGED_DRY_QTY; // Total Uncharged Plate 6
                    // Openning ON Charging
                    stk.OP_ON_CHARGING = Conversion.DBNullDecimalToZero(dRow["OP_ON_CHARGING"]);

                    stk.CUR_GREEN_BAT_LOADING_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_GREEN_BAT_LOADING_QTY"]);
                    stk.CUR_DRY_BAT_LOADING_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_DRY_BAT_LOADING_QTY"]);
                    stk.CUR_LOADING_QTY = stk.CUR_GREEN_BAT_LOADING_QTY + stk.CUR_DRY_BAT_LOADING_QTY;
                    //stk.OPPENING_BAL_QTY - stk.TOTAL_CHARGED_QTY - stk.TOTAL_UNCHARGED_QTY;
                    stk.TOTAL_ON_CHARGING = stk.OP_ON_CHARGING + stk.CUR_LOADING_QTY; // Total ON Charging 7 


                    //stk.CUR_CHARGED_DRY_BAT_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_CHARGED_DRY_BAT_QTY"]);
                    stk.CUR_CHARGED_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_CHARGED_QTY"]);   // Today Charged qty  10

                    //Green and Dry Plate Battery Reject Quantity
                    stk.CUR_REJECT_GREEN_BAT_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_REJECT_GREEN_BAT_QTY"]);
                    stk.CUR_REJECT_DRY_BAT_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_REJECT_DRY_BAT_QTY"]);
                    stk.CUR_REJECT_QTY = stk.CUR_REJECT_GREEN_BAT_QTY + stk.CUR_REJECT_DRY_BAT_QTY;


                    stk.CUR_UNLOADING_QTY = stk.CUR_CHARGED_QTY + stk.CUR_REJECT_QTY;//Unloading means total charged declaration and total rejection declaration

                   

                  


                    //Closing quantity.This law may be changed
                    stk.CLOSING_QTY = stk.OPPENING_BAL_QTY + stk.CUR_IRR_QTY + stk.CUR_LOADING_QTY - (stk.ITC_RB_QTY + stk.ITC_STORE_QTY);
                        
                       // stk.OPPENING_BAL_QTY - (stk.ITC_RB_QTY + stk.ITC_STORE_QTY);

                    #endregion

                    //if (stk.OPPENING_BAL_QTY != 0 || stk.TOT_RECEIVE_QNTY != 0 || stk.IRR_BAL_QTY != 0 || stk.ITC_STORE_QTY != 0 || stk.CLOSING_QTY != 0)
                    //{
                        cRptList.Add(stk);
                    //}

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





        public static List<rcMaterialStock> SolarFinishedGoodsSummaryReport(clsPrmInventory prmINV, DBContext dc)
        {
            List<rcMaterialStock> cRptList = new List<rcMaterialStock>();
            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();

                if (prmINV.ToDate.HasValue)
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.ToDate.Value);
                }
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
                cmdInfo.CommandText = "SP_SOLAR_STOCK_REPORT";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);

                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;
                //sb.Append(" Select ITEM_GROUP_ID,ITEM_GROUP_NAME,ITEM_ID,ITEM_NAME,UOM_ID,UOM_NAME,ITEM_TYPE_ID,ITEM_TYPE_NAME,ITEM_CLASS_ID,ITEM_CLASS_NAME ");
                //sb.Append(" ,OP_IRR_QTY,OP_ITC_QTY, ITC_RB_QTY,ITC_STORE_QTY,ADJUST_QTY ");
                //sb.Append(" ,CUR_GREEN_BAT_RCV_QTY,CUR_DRY_BAT_RCV_QTY,CUR_LOADING_QTY,CUR_UNLOADING_QTY,CUR_GREEN_BAT_LOADING_QTY,CUR_DRY_BAT_LOADING_QTY ");
                //sb.Append(" ,CUR_PACKED_QTY,CUR_CHARGED_QTY,CUR_CHARGED_GREEN_BAT_QTY,CUR_CHARGED_DRY_BAT_QTY,CUR_REJECT_GREEN_BAT_QTY,CUR_REJECT_DRY_BAT_QTY ");
                //sb.Append(" ,OP_GREEN_BAT_LOADING_QTY,OP_DRY_BAT_LOADING_QTY ,ORDER_NO,OP_GREEN_BAT_CHARGED_QTY,OP_DRY_BAT_CHARGED_QTY,OP_PACKET_QTY,OP_CHARGE_QTY,CUR_IRR_QTY,ITC_OTHERS, REJECT_QTY_RCV ");
                //sb.Append(" FROM TEMP_SOLAR_ITEM_STOCK ");
                sb.Append(" SELECT * FROM TEMP_SOLAR_ITEM_STOCK ");
                DBQuery dbqtemp = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);

                foreach (DataRow dRow in dtData.Rows)
                {
                    rcMaterialStock stk = new rcMaterialStock();
                    stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();



                    stk.BATERY_CAT_SL_NO = Conversion.DBNullIntToZero(dRow["BATERY_CAT_SL_NO"]);
                    stk.BATERY_CAT_DESCR = dRow["BATERY_CAT_DESCR"].ToString();


                    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);

                    stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                    stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();
                    stk.ORDER_NO = Conversion.StringToInt(dRow["ORDER_NO"].ToString());
                    stk.DEPARTMENT_NAME = prmINV.From_Dept_Name;
                    stk.OP_IRR_REJECT_QTY = Conversion.DBNullIntToZero(dRow["OP_IRR_REJECT_QTY"]);

                    stk.OP_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_IRR_QTY"]);
                    stk.OP_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ITC_QTY"]);
                    stk.OPPENING_BAL_QTY = stk.OP_IRR_QTY - stk.OP_ITC_QTY - stk.OP_IRR_REJECT_QTY;

                    stk.CUR_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_IRR_QTY"]);

                    stk.BATT_CONVERT_FROM = Conversion.DBNullDecimalToZero(dRow["BATT_CONVERT_FROM"]);
                    stk.BATT_CONVERT_TO = Conversion.DBNullDecimalToZero(dRow["BATT_CONVERT_TO"]);

                    stk.TOT_RECEIVE_QNTY = stk.OPPENING_BAL_QTY + stk.CUR_IRR_QTY - stk.BATT_CONVERT_FROM + stk.BATT_CONVERT_TO;

                    stk.ITC_RB_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_RB_QTY"]);
                    stk.ITC_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_STORE_QTY"]);
                    stk.ITC_OTHERS = Conversion.DBNullDecimalToZero(dRow["ITC_OTHERS"]);
                    stk.ITC_BAL_QTY = stk.ITC_RB_QTY + stk.ITC_STORE_QTY + stk.ITC_OTHERS;

                    stk.REJECT_QTY_RCV = Conversion.DBNullDecimalToZero(dRow["CUR_REJECT_GREEN_BAT_QTY"]);
                    //Closing quantity.This law may be changed
                    stk.CLOSING_QTY = stk.TOT_RECEIVE_QNTY - stk.ITC_BAL_QTY - stk.REJECT_QTY_RCV;

                    //if (stk.OPPENING_BAL_QTY != 0 || stk.IRR_BAL_QTY != 0 || stk.ITC_BAL_QTY != 0 || stk.COLISING_QTY != 0)
                    //  {
                    if (stk.OPPENING_BAL_QTY != 0 || stk.TOT_RECEIVE_QNTY != 0 ||stk.BATT_CONVERT_FROM!=0 || stk.BATT_CONVERT_TO!=0 || stk.IRR_BAL_QTY != 0 || stk.ITC_STORE_QTY != 0 || stk.CLOSING_QTY != 0)
                    {
                    cRptList.Add(stk);
                    }

                     
                }




            }
            //catch { throw; }
            //finally 
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }

            return cRptList;

        }



        public static List<rcMaterialStock> Solar_Raw_Material_Summary_Report(clsPrmInventory prmINV)
        {
            return Solar_Raw_Material_Summary_Report(prmINV, null);
        }

        public static List<rcMaterialStock> Solar_Raw_Material_Summary_Report(clsPrmInventory prmINV, DBContext dc)
        {

            List<rcMaterialStock> cRptList = new List<rcMaterialStock>();


            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();

                if (prmINV.ToDate.HasValue)
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.ToDate.Value);
                }
                cmdInfo.DBParametersInfo.Add(":p_itemtype_id", prmINV.itemtype_id);
                cmdInfo.DBParametersInfo.Add(":p_itemGroup_id", prmINV.itemGroup_id);
                cmdInfo.DBParametersInfo.Add(":p_item_id", prmINV.item_id);
                cmdInfo.DBParametersInfo.Add(":p_store_id", prmINV.store_id);
                cmdInfo.DBParametersInfo.Add(":p_itemclass_id", prmINV.ItemClassId);
                cmdInfo.DBParametersInfo.Add(":p_battery_type_id", prmINV.Battery_Type_ID);
                cmdInfo.DBParametersInfo.Add(":p_is_repair", prmINV.Is_Repair);
                cmdInfo.DBParametersInfo.Add(":p_dept_id", prmINV.DeptID);
                cmdInfo.DBParametersInfo.Add(":p_is_finished", prmINV.is_Finished);

                DBQuery dbq = new DBQuery();

                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = "SP_SOLAR_RAW_STOCK_REPORT";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);
                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;

                // sb.Append(" Select ITEM_GROUP_ID,ITEM_GROUP_NAME,ITEM_ID,ITEM_NAME,UOM_ID,UOM_NAME,ITEM_TYPE_ID,ITEM_TYPE_NAME,ITEM_CLASS_ID,ITEM_CLASS_NAME,OP_IRR_QTY,OP_ITC_QTY,IRR_DEPT_QTY,IRR_STORE_QTY,IRR_PROD_QTY,ITC_DEPT_QTY,ITC_STORE_QTY,ITC_PROD_QTY,ADJUST_QTY,OP_ADJUST_QTY,CUR_REJECT_QTY FROM TEMP_ASSEMBLY_ITEM_STOCK  WHERE ITEM_GROUP_ID!=1111 order by  ITEM_GROUP_ID,ITEM_NAME");
                 sb.Append(" SELECT * FROM TEMP_SOLAR_RM_ITEM_STOCK ");
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
                    rcMaterialStock stk = new rcMaterialStock();
                    stk.DEPARTMENT_NAME = prmINV.From_Dept_Name;
                    stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();

                    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);

                    stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                    stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();

                    #region Opening Calculation
                    stk.OP_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_IRR_QTY"]);
                    stk.OP_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ITC_QTY"]);
                    stk.OP_ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ADJUST_QTY"]);
                    stk.OPPENING_BAL_QTY = stk.OP_IRR_QTY + stk.OP_ADJUST_QTY - stk.OP_ITC_QTY;
                    #endregion

                    #region Transaction Data Within Date Rang

                    stk.IRR_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_STORE_QTY"]);
                    stk.IRR_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_DEPT_QTY"]);
                    stk.IRR_BAL_QTY = stk.IRR_STORE_QTY + stk.IRR_DEPT_QTY;

                    stk.TOT_RECEIVE_QNTY = stk.OPPENING_BAL_QTY + stk.IRR_BAL_QTY;

                    stk.ITC_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_STORE_QTY"]);
                    stk.ITC_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_PROD_QTY"]);
                    stk.ITC_BAL_QTY = stk.ITC_STORE_QTY + stk.ITC_PROD_QTY;

                    //here total Adjust balance quantity
                    stk.CUR_ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);
                    stk.CUR_REJECT_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_REJECT_QTY"]);
                    #endregion

                    #region Closing Calculation
                    stk.COLISING_QTY = stk.OPPENING_BAL_QTY + stk.IRR_BAL_QTY + stk.CUR_ADJUST_QTY - (stk.ITC_BAL_QTY + stk.CUR_REJECT_QTY);
                    #endregion
                    stk.ORDER_NO = Conversion.StringToInt(dRow["ORDER_NO"].ToString());

                    //if (stk.OPPENING_BAL_QTY != 0 || stk.COLISING_QTY != 0 || stk.ITC_BAL_QTY != 0 || stk.IRR_BAL_QTY != 0)
                    if (stk.OPPENING_BAL_QTY != 0 || stk.IRR_STORE_QTY != 0 || stk.IRR_DEPT_QTY != 0 || stk.ITC_PROD_QTY != 0 || stk.ITC_BAL_QTY!=0)
                    {
                        cRptList.Add(stk);
                    }


                }




            }
            //catch { throw; }
            //finally 
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }

            return cRptList;

        }




        #endregion



        //Assembly Plate Report

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

                if (prmINV.ToDate.HasValue)
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.ToDate.Value);
                }
                cmdInfo.DBParametersInfo.Add(":p_itemtype_id", prmINV.itemtype_id);
                cmdInfo.DBParametersInfo.Add(":p_itemGroup_id", prmINV.itemGroup_id);
                cmdInfo.DBParametersInfo.Add(":p_item_id", prmINV.item_id);
                cmdInfo.DBParametersInfo.Add(":p_store_id", prmINV.store_id);
                cmdInfo.DBParametersInfo.Add(":p_itemclass_id", prmINV.ItemClassId);
                cmdInfo.DBParametersInfo.Add(":p_battery_type_id", prmINV.Battery_Type_ID);
                cmdInfo.DBParametersInfo.Add(":p_is_repair", prmINV.Is_Repair);
                cmdInfo.DBParametersInfo.Add(":p_dept_id", prmINV.DeptID);
                cmdInfo.DBParametersInfo.Add(":p_is_finished", prmINV.is_Finished);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = "SP_ASSEM_MONTH_PLATE_INV_RT";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);

                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;
                sb.Append(" Select * FROM TEMP_ASS_PLATE_STOCK");
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
                    stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();

                    stk.ITEM_ORDER = Conversion.DBNullIntToZero(dRow["ITEM_ORDER"]);
                    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);

                    stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                    stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();

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

        #endregion

        #region Bom_Vs_Actual_Comparision
        //Summary
        public static List<rcBomVsProduction> Bom_Vs_Actual_Production_Comparision_Report(clsPrmInventory prmINV)
        {
            return Bom_Vs_Actual_Production_Comparision_Report(prmINV, null);
        }

        public static List<rcBomVsProduction> Bom_Vs_Actual_Production_Comparision_Report(clsPrmInventory prmINV, DBContext dc)
        {
            List<rcBomVsProduction> cRptList = new List<rcBomVsProduction>();
            bool isDCInit = false;
            //try
            {

                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();
                cmdInfo.DBParametersInfo.Clear();

                /*if (prmINV.ToDate.HasValue)
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.ToDate.Value);
                }

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
                cmdInfo.CommandText = "SP_BOM_VS_ACTUAL_REPORT";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);

                DBCommandInfo cmdInfotemp = new DBCommandInfo();*/
                sb.Length = 0;

                /*sb.Append(" Select fg.ITEM_GROUP_ID,fg.ITEM_GROUP_NAME,fg.ITEM_ID,fg.ITEM_NAME,fg.UOM_ID,fg.UOM_NAME,fg.ITEM_TYPE_ID,fg.ITEM_TYPE_NAME,fg.ITEM_CLASS_ID,fg.ITEM_CLASS_NAME,fg.OP_IRR_QTY,fg.OP_ITC_QTY,fg.IRR_DEPT_QTY,fg.IRR_STORE_QTY,fg.IRR_PROD_QTY,fg.ITC_DEPT_QTY,fg.ITC_STORE_QTY,fg.ITC_PROD_QTY,fg.ADJUST_QTY,fg.ITEM_CODE ");
                sb.Append(" ,rawitem.ITEM_ID RM_ITEM_ID,rawitem.ITEM_NAME RM_ITEM_NAME,rawitem.UOM_ID RM_UOM_ID");
                sb.Append(" ,rawitem.UOM_NAME RM_UOM_NAME,rawitem.ITEM_CODE RM_ITEM_CODE,rawitem.STD_QTY,rawitem.WASTAGE_PERCENT RM_WASTAGE_PERCENT,rawitem.STD_WT RM_STD_WT,rawitem.RM_ACTUAL_QTY RM_ACTUAL_QTY,rawitem.RM_ACTUAL_WASTAGE_PERCENT,rawitem.RM_ACTUAL_WT ");                
                sb.Append(" FROM TEMP_BOM_VS_ACTUAL_FINISH_ITEM fg ");
                sb.Append(" LEFT JOIN TEMP_BOM_VS_ACTUAL_RAW_ITEM  rawitem ");
                sb.Append(" on fg.ITEM_ID=rawitem.BOM_ITEM_ID ");
                sb.Append("Where NVL(fg.IRR_PROD_QTY,0)>0");*/

                if (prmINV.autho_status == "N")
                {
                    sb.Append(" SELECT ITEM_NAME,UOM_CODE,RM_ITEM_NAME,RM_UOM_CODE,BOM_ITEM_DESC,PRODUCTION_QTY,ITEM_STANDARD_WEIGHT_KG ");
                    sb.Append(" ,USED_QTY_AS_BOM,RM_ACTUAL_USED,USE_DIFF  ");
                    sb.Append(" ,CASE WHEN ROUND(USE_DIFF,2)<>0 AND ROUND(USED_QTY_AS_BOM)<>0 THEN ROUND((ROUND(USE_DIFF)/ROUND(USED_QTY_AS_BOM))*100,2) else 0 END USE_PER ");
                    sb.Append(" FROM ( ");
                    sb.Append(" Select c.ITEM_NAME,UI.UOM_CODE,bi.ITEM_NAME RM_ITEM_NAME,BU.UOM_CODE RM_UOM_CODE,bm.BOM_ITEM_DESC,SUM(NVL(b.ITEM_QTY,0)+NVL(b.REJECT_QTY,0)) PRODUCTION_QTY,bd.ITEM_QTY ITEM_STANDARD_WEIGHT_KG ");
                    sb.Append(" ,ROUND(CASE WHEN BU.UOM_CODE='Percent' THEN (CASE WHEN SUM(NVL(b.MIXER_BATCH_QTY,0))>0 THEN SUM(NVL(b.MIXER_BATCH_QTY,0))*(bd.ITEM_QTY/100) else SUM(NVL(b.ITEM_QTY,0))*(bd.ITEM_QTY/100) END) else SUM(NVL(b.ITEM_QTY,0)+NVL(b.REJECT_QTY,0))*(bd.ITEM_QTY+NVL(FN_GET_FALSE_LUG_WT(bm.BOM_ITEM_ID,bd.ITEM_ID),0)) END,3) USED_QTY_AS_BOM,FN_USED_RAW_MATERIAL_QTY(bd.ITEM_ID,b.ITEM_ID,a.DEPT_ID,:P_FromDate,:P_ToDate,:P_autho_status) RM_ACTUAL_USED,FN_USED_RAW_MATERIAL_QTY(bd.ITEM_ID,b.ITEM_ID,a.DEPT_ID,:T_FromDate,:T_ToDate,:T_autho_status)-ROUND(CASE WHEN BU.UOM_CODE='Percent' THEN (CASE WHEN SUM(NVL(b.MIXER_BATCH_QTY,0))>0 THEN SUM(NVL(b.MIXER_BATCH_QTY,0))*(bd.ITEM_QTY/100) else SUM(NVL(b.ITEM_QTY,0))*(bd.ITEM_QTY/100) END) else SUM(NVL(b.ITEM_QTY,0)+NVL(b.REJECT_QTY,0))*(bd.ITEM_QTY+NVL(FN_GET_FALSE_LUG_WT(bm.BOM_ITEM_ID,bd.ITEM_ID),0)) END,3) USE_DIFF ");
                    sb.Append(" FROM PRODUCTION_MST a ");
                    sb.Append(" INNER JOIN PRODUCTION_DTL b ON a.PROD_ID=b.PROD_MST_ID ");
                    sb.Append(" INNER JOIN INV_ITEM_MASTER c ON b.ITEM_ID=c.ITEM_ID ");
                    sb.Append(" LEFT JOIN BOM_MST_T bm ON b.BOM_ID=bm.BOM_ID ");
                    sb.Append(" LEFT JOIN BOM_DTL_T bd ON bm.BOM_ID=bd.BOM_MST_ID ");
                    sb.Append(" LEFT JOIN INV_ITEM_MASTER bi ON bd.ITEM_ID=bi.ITEM_ID ");
                    sb.Append(" INNER JOIN UOM_INFO UI ON b.UOM_ID=UI.UOM_ID ");
                    sb.Append(" INNER JOIN UOM_INFO BU ON bd.ITEM_UNIT_ID=BU.UOM_ID ");
                    sb.Append(" Where 1=1 ");

                    if (prmINV.FromDate != null)
                    {
                        sb.Append(" AND  a.PRODUCTION_DATE BETWEEN  :FromDate and :ToDate ");

                        cmdInfo.DBParametersInfo.Add(":P_FromDate", prmINV.FromDate);
                        cmdInfo.DBParametersInfo.Add(":P_ToDate", prmINV.ToDate);
                        cmdInfo.DBParametersInfo.Add(":P_autho_status", prmINV.autho_status);
                        cmdInfo.DBParametersInfo.Add(":T_FromDate", prmINV.FromDate);
                        cmdInfo.DBParametersInfo.Add(":T_ToDate", prmINV.ToDate);
                        cmdInfo.DBParametersInfo.Add(":T_autho_status", prmINV.autho_status);


                        cmdInfo.DBParametersInfo.Add(":FromDate", prmINV.FromDate);
                        cmdInfo.DBParametersInfo.Add(":ToDate", prmINV.ToDate);
                    }
                    if (prmINV.item_id > 0)
                    {
                        sb.Append(" AND b.ITEM_ID=:Item_ID ");
                        cmdInfo.DBParametersInfo.Add(":Item_ID", prmINV.item_id);
                    }

                    if (prmINV.DeptID > 0)
                    {
                        sb.Append(" AND a.DEPT_ID=:DeptID");
                        cmdInfo.DBParametersInfo.Add(":DeptID", prmINV.DeptID);
                    }
                    //if (prmINV.DeptID > 0)
                    //{
                        sb.Append(" AND a.AUTH_STATUS=:autho_status");
                        cmdInfo.DBParametersInfo.Add(":autho_status", prmINV.autho_status);
                    //}
                    //sb.Append(" and b.ITEM_ID=6690 ");
                        if (prmINV.StorageLocationId > 0)
                        {
                            sb.Append(" AND a.STLM_ID=:StorageLocationId");
                            cmdInfo.DBParametersInfo.Add(":StorageLocationId", prmINV.StorageLocationId);
                        }

                        sb.Append(" GROUP BY c.ITEM_NAME,UI.UOM_CODE,bi.ITEM_NAME,BU.UOM_CODE,bm.BOM_ITEM_DESC,bd.ITEM_ID,b.ITEM_ID,a.DEPT_ID,bd.ITEM_QTY,bm.BOM_ITEM_ID ) WHERE PRODUCTION_QTY>0 ORDER BY ITEM_NAME ");
                }
                else
                {
                    sb.Append(" SELECT ITEM_NAME,UOM_CODE,RM_ITEM_NAME,RM_UOM_CODE,BOM_ITEM_DESC,PRODUCTION_QTY,ITEM_STANDARD_WEIGHT_KG ");
                    sb.Append(" ,USED_QTY_AS_BOM,RM_ACTUAL_USED,USE_DIFF  ");
                    sb.Append(" ,CASE WHEN ROUND(USE_DIFF,2)<>0 AND ROUND(USED_QTY_AS_BOM)<>0 THEN ROUND((ROUND(USE_DIFF)/ROUND(USED_QTY_AS_BOM))*100,2) else 0 END USE_PER ");
                    sb.Append(" FROM ( ");
                    sb.Append(" Select c.ITEM_NAME,UI.UOM_CODE,bi.ITEM_NAME RM_ITEM_NAME,BU.UOM_CODE RM_UOM_CODE,bm.BOM_ITEM_DESC ");
                    sb.Append(" ,SUM(NVL(b.RCV_QTY,0))+NVL(FN_USED_FG_REJECT_PARTING_QTY(a.DEPT_ID,b.ITEM_ID,:FN_FromDate,:FN_ToDate,:FN_autho_status),0) PRODUCTION_QTY,bd.ITEM_QTY ITEM_STANDARD_WEIGHT_KG,  CASE WHEN BU.UOM_CODE='Percent' THEN (CASE WHEN SUM(NVL((SELECT MIXER_BATCH_QTY FROM PRODUCTION_DTL Where PROD_MST_ID=a.PROD_ID and ITEM_ID=b.ITEM_ID and MIXER_BATCH_QTY>0),0))>0 THEN SUM(NVL((SELECT MIXER_BATCH_QTY FROM PRODUCTION_DTL Where PROD_MST_ID=a.PROD_ID and ITEM_ID=b.ITEM_ID and MIXER_BATCH_QTY>0),0))*(bd.ITEM_QTY/100) Else SUM(NVL(b.RCV_QTY,0))*(bd.ITEM_QTY/100) END) else (SUM(NVL(b.RCV_QTY,0))+NVL(FN_USED_FG_REJECT_PARTING_QTY(a.DEPT_ID,b.ITEM_ID,:FN1_FromDate,:FN1_ToDate,:FN1_autho_status),0))*(bd.ITEM_QTY+NVL(FN_GET_FALSE_LUG_WT(bm.BOM_ITEM_ID,bd.ITEM_ID),0)) END USED_QTY_AS_BOM ");
                    sb.Append(" ,FN_USED_RAW_MATERIAL_QTY(bd.ITEM_ID,b.ITEM_ID,a.DEPT_ID,:P_FromDate,:P_ToDate,:P_autho_status) RM_ACTUAL_USED ");
                    sb.Append(" ,FN_USED_RAW_MATERIAL_QTY(bd.ITEM_ID,b.ITEM_ID,a.DEPT_ID,:T_FromDate,:T_ToDate,:T_autho_status)- CASE WHEN BU.UOM_CODE='Percent' THEN (CASE WHEN SUM(NVL((SELECT MIXER_BATCH_QTY FROM PRODUCTION_DTL Where PROD_MST_ID=a.PROD_ID and ITEM_ID=b.ITEM_ID and MIXER_BATCH_QTY>0),0))>0 THEN SUM(NVL((SELECT MIXER_BATCH_QTY FROM PRODUCTION_DTL Where PROD_MST_ID=a.PROD_ID and ITEM_ID=b.ITEM_ID and MIXER_BATCH_QTY>0),0))*(bd.ITEM_QTY/100) Else SUM(NVL(b.RCV_QTY,0))*(bd.ITEM_QTY/100) END) else (SUM(NVL(b.RCV_QTY,0))+NVL(FN_USED_FG_REJECT_PARTING_QTY(a.DEPT_ID,b.ITEM_ID,:FN2_FromDate,:FN2_ToDate,:FN2_autho_status),0))*(bd.ITEM_QTY+NVL(FN_GET_FALSE_LUG_WT(bm.BOM_ITEM_ID,bd.ITEM_ID),0)) END  USE_DIFF ");
                    sb.Append(" FROM PRODUCTION_MST a ");
                    sb.Append(" INNER JOIN ITEM_STOCK_DETAILS b ON a.PROD_NO=b.TRANS_REF_NO ");
                    sb.Append(" INNER JOIN INV_ITEM_MASTER c ON b.ITEM_ID=c.ITEM_ID ");
                    sb.Append(" LEFT JOIN BOM_MST_T bm ON b.ITEM_ID=bm.BOM_ITEM_ID ");
                    sb.Append(" LEFT JOIN BOM_DTL_T bd ON bm.BOM_ID=bd.BOM_MST_ID ");
                    sb.Append(" LEFT JOIN INV_ITEM_MASTER bi ON bd.ITEM_ID=bi.ITEM_ID ");
                    sb.Append(" INNER JOIN UOM_INFO UI ON b.UOM_ID=UI.UOM_ID ");
                    sb.Append(" INNER JOIN UOM_INFO BU ON bd.ITEM_UNIT_ID=BU.UOM_ID ");
                    sb.Append(" Where 1=1 ");

                    if (prmINV.FromDate != null)
                    {
                        sb.Append(" AND  a.PRODUCTION_DATE BETWEEN  :FromDate and :ToDate ");

                        cmdInfo.DBParametersInfo.Add(":P_FromDate", prmINV.FromDate);
                        cmdInfo.DBParametersInfo.Add(":P_ToDate", prmINV.ToDate);
                        cmdInfo.DBParametersInfo.Add(":P_autho_status", prmINV.autho_status);
                        cmdInfo.DBParametersInfo.Add(":T_FromDate", prmINV.FromDate);
                        cmdInfo.DBParametersInfo.Add(":T_ToDate", prmINV.ToDate);
                        cmdInfo.DBParametersInfo.Add(":T_autho_status", prmINV.autho_status);
                        cmdInfo.DBParametersInfo.Add(":FN_FromDate", prmINV.FromDate);
                        cmdInfo.DBParametersInfo.Add(":FN_ToDate", prmINV.ToDate);
                        cmdInfo.DBParametersInfo.Add(":FN_autho_status", prmINV.autho_status);
                        cmdInfo.DBParametersInfo.Add(":FN1_FromDate", prmINV.FromDate);
                        cmdInfo.DBParametersInfo.Add(":FN1_ToDate", prmINV.ToDate);
                        cmdInfo.DBParametersInfo.Add(":FN1_autho_status", prmINV.autho_status);

                        cmdInfo.DBParametersInfo.Add(":FN2_FromDate", prmINV.FromDate);
                        cmdInfo.DBParametersInfo.Add(":FN2_ToDate", prmINV.ToDate);
                        cmdInfo.DBParametersInfo.Add(":FN2_autho_status", prmINV.autho_status);



                        cmdInfo.DBParametersInfo.Add(":FromDate", prmINV.FromDate);
                        cmdInfo.DBParametersInfo.Add(":ToDate", prmINV.ToDate);
                    }
                    if (prmINV.item_id > 0)
                    {
                        sb.Append(" AND b.ITEM_ID=:Item_ID ");
                        cmdInfo.DBParametersInfo.Add(":Item_ID", prmINV.item_id);
                    }

                    if (prmINV.DeptID > 0)
                    {
                        sb.Append(" AND a.DEPT_ID=:DeptID");
                        cmdInfo.DBParametersInfo.Add(":DeptID", prmINV.DeptID);
                    }
                    sb.Append(" AND a.AUTH_STATUS=:autho_status");
                    cmdInfo.DBParametersInfo.Add(":autho_status", prmINV.autho_status);
                    //sb.Append(" and b.ITEM_ID=6690 ");
                    if (prmINV.StorageLocationId > 0)
                    {
                        sb.Append(" AND bm.STLM_ID=:StorageLocationId");
                        cmdInfo.DBParametersInfo.Add(":StorageLocationId", prmINV.StorageLocationId);
                    }


                    sb.Append(" GROUP BY c.ITEM_NAME,UI.UOM_CODE,bi.ITEM_NAME,BU.UOM_CODE,bm.BOM_ITEM_DESC,bd.ITEM_QTY,bd.ITEM_ID,b.ITEM_ID,a.DEPT_ID,bm.BOM_ITEM_ID ");
                        
                        sb.Append(" UNION ALL ");

                    
                    sb.Append(" Select c.ITEM_NAME,UI.UOM_CODE,bi.ITEM_NAME RM_ITEM_NAME,BU.UOM_CODE RM_UOM_CODE,'Extra Item' BOM_ITEM_DESC ");
                    sb.Append(" ,0 PRODUCTION_QTY,0 ITEM_STANDARD_WEIGHT_KG, 0 USED_QTY_AS_BOM ");
                    sb.Append(" ,SUM(NVL(fc.ISSUE_STOCK,0)) RM_ACTUAL_USED ");
                    sb.Append(" ,0  USE_DIFF ");
                    sb.Append(" FROM PRODUCTION_MST a ");
                    sb.Append(" INNER JOIN PRODUCTION_DTL b ON a.PROD_ID=b.PROD_MST_ID ");
                    sb.Append(" INNER JOIN INV_ITEM_MASTER c ON b.ITEM_ID=c.ITEM_ID ");
                    sb.Append(" LEFT JOIN PRODUCTION_FLOOR_CLOSING fc ON a.PROD_ID=fc.PROD_MST_ID and b.ITEM_ID=fc.FINISHED_ITEM_ID ");
                    sb.Append(" LEFT JOIN INV_ITEM_MASTER bi ON fc.CLOSING_ITEM_ID=bi.ITEM_ID ");
                    sb.Append(" INNER JOIN UOM_INFO UI ON b.UOM_ID=UI.UOM_ID ");
                    sb.Append(" INNER JOIN UOM_INFO BU ON fc.CLOSING_UOM_ID=BU.UOM_ID ");
                    sb.Append(" Where 1=1 ");

                    if (prmINV.FromDate != null)
                    {
                        sb.Append(" AND  a.PRODUCTION_DATE BETWEEN  :FromDate and :ToDate ");


                        cmdInfo.DBParametersInfo.Add(":FromDate", prmINV.FromDate);
                        cmdInfo.DBParametersInfo.Add(":ToDate", prmINV.ToDate);
                    }
                    if (prmINV.item_id > 0)
                    {
                        sb.Append(" AND b.ITEM_ID=:Item_ID ");
                        cmdInfo.DBParametersInfo.Add(":Item_ID", prmINV.item_id);
                    }

                    if (prmINV.DeptID > 0)
                    {
                        sb.Append(" AND a.DEPT_ID=:DeptID");
                        cmdInfo.DBParametersInfo.Add(":DeptID", prmINV.DeptID);
                    }
                    sb.Append(" AND a.AUTH_STATUS=:autho_status");
                    cmdInfo.DBParametersInfo.Add(":autho_status", prmINV.autho_status);
                    sb.Append(" AND fc.ISMANUAL=1 ");
                    if (prmINV.StorageLocationId > 0)
                    {
                        sb.Append(" AND a.STLM_ID=:StorageLocationId");
                        cmdInfo.DBParametersInfo.Add(":StorageLocationId", prmINV.StorageLocationId);
                    }

                    sb.Append(" GROUP BY c.ITEM_NAME,UI.UOM_CODE,bi.ITEM_NAME,BU.UOM_CODE ") ;


                    sb.Append("     ) Where (PRODUCTION_QTY>0 OR RM_ACTUAL_USED<>0) ORDER BY ITEM_NAME ");

                    
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
                    rcBomVsProduction stk = new rcBomVsProduction();
                    //stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    //stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    //stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();
                    //stk.ITEM_CODE = dRow["ITEM_CODE"].ToString();

                    //stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_CODE"].ToString();
                    //stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);

                    // stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                    // stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    //  stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();
                    //   stk.OP_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_IRR_QTY"]);
                    // stk.OP_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ITC_QTY"]);
                    // stk.OPPENING_BAL_QTY = stk.OP_IRR_QTY - stk.OP_ITC_QTY;

                    // stk.IRR_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_DEPT_QTY"]);
                    //   stk.IRR_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_STORE_QTY"]);
                    stk.IRR_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["PRODUCTION_QTY"]);

                    //here total IRR quantity
                    // stk.IRR_BAL_QTY = stk.IRR_DEPT_QTY + stk.IRR_STORE_QTY + stk.IRR_PROD_QTY;

                    // stk.ITC_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_DEPT_QTY"]);
                    //stk.ITC_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_STORE_QTY"]);
                    //   stk.ITC_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_PROD_QTY"]);

                    //here total ITC quantity
                    // stk.ITC_BAL_QTY = stk.ITC_DEPT_QTY + stk.ITC_STORE_QTY + stk.ITC_PROD_QTY;

                    //here total Adjust balance quantity
                    //  stk.ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);

                    //  stk.DEPARTMENT_NAME = prmINV.From_Dept_Name;
                    //  stk.COLISING_QTY = (stk.OPPENING_BAL_QTY + stk.ADJUST_QTY + stk.IRR_BAL_QTY) - stk.ITC_BAL_QTY;

                    //BOM Raw material Part

                    //stk.RM_ITEM_ID = Conversion.DBNullIntToZero(dRow["RM_ITEM_ID"]);
                    stk.RM_ITEM_NAME = dRow["RM_ITEM_NAME"].ToString();
                    // stk.RM_UOM_ID = Conversion.DBNullIntToZero(dRow["RM_UOM_ID"]);
                    stk.RM_UOM_NAME = dRow["RM_UOM_CODE"].ToString();
                    //  stk.RM_ITEM_CODE = dRow["RM_ITEM_CODE"].ToString();


                    stk.RM_ACTUAL_QTY = Conversion.DBNullDecimalToZero(dRow["RM_ACTUAL_USED"]);
                    stk.STD_UNIT_QTY = Conversion.DBNullDecimalToZero(dRow["ITEM_STANDARD_WEIGHT_KG"]);
                    stk.RM_STD_QTY = Conversion.DBNullDecimalToZero(dRow["USED_QTY_AS_BOM"]);


                    //stk.RM_WASTAGE_PERCENT = Conversion.DBNullDecimalToZero(dRow["ITEM_STANDARD_WEIGHT_KG"]);
                    //stk.RM_STD_WT = Conversion.DBNullDecimalToZero(dRow["ITEM_STANDARD_WEIGHT_KG"]);
                    stk.RM_USE_DIFF = Conversion.DBNullDecimalToZero(dRow["USE_DIFF"]);
                    stk.USE_PER = Conversion.DBNullDecimalToZero(dRow["USE_PER"]);

                    //stk.RM_ACTUAL_WASTAGE_PERCENT = Conversion.DBNullDecimalToZero(dRow["RM_ACTUAL_WASTAGE_PERCENT"]);








                    cRptList.Add(stk);

                }

            }
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }

            return cRptList;

        }

        //Details

        public static List<rcBomVsProduction> Bom_Vs_Actual_Production_Comparision_Report_dtl(clsPrmInventory prmINV)
        {
            return Bom_Vs_Actual_Production_Comparision_Report_dtl(prmINV, null);
        }

        public static List<rcBomVsProduction> Bom_Vs_Actual_Production_Comparision_Report_dtl(clsPrmInventory prmINV, DBContext dc)
        {
            List<rcBomVsProduction> cRptList = new List<rcBomVsProduction>();
            bool isDCInit = false;
            //try
            {

                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();
                cmdInfo.DBParametersInfo.Clear();


                sb.Length = 0;



                if (prmINV.autho_status == "N")
                {
                    sb.Append(" SELECT PROD_ID,Production_Date,ITEM_NAME,UOM_CODE,RM_ITEM_NAME,RM_UOM_CODE,BOM_ITEM_DESC,PRODUCTION_QTY,ITEM_STANDARD_WEIGHT_KG ");
                    sb.Append(" ,NVL(USED_QTY_AS_BOM,0) USED_QTY_AS_BOM,NVL(RM_ACTUAL_USED,0) RM_ACTUAL_USED,NVL(USE_DIFF,0) USE_DIFF ");
                    sb.Append(" ,CASE WHEN ROUND(USE_DIFF,2)<>0 AND ROUND(USED_QTY_AS_BOM)<>0 THEN ROUND((ROUND(USE_DIFF)/ROUND(USED_QTY_AS_BOM))*100,2) else 0 END USE_PER ");
                    sb.Append(" FROM ( ");
                    sb.Append(" Select a.PROD_ID,a.Production_Date,c.ITEM_NAME,UI.UOM_CODE,bi.ITEM_NAME RM_ITEM_NAME,BU.UOM_CODE RM_UOM_CODE,bm.BOM_ITEM_DESC,SUM(NVL(b.ITEM_QTY,0)+NVL(b.REJECT_QTY,0)) PRODUCTION_QTY,bd.ITEM_QTY ITEM_STANDARD_WEIGHT_KG ");
                    sb.Append(" ,ROUND(CASE WHEN BU.UOM_CODE='Percent' THEN (CASE WHEN SUM(NVL(b.MIXER_BATCH_QTY,0))>0 THEN SUM(NVL(b.MIXER_BATCH_QTY,0))*(bd.ITEM_QTY/100) else SUM(NVL(b.ITEM_QTY,0))*(bd.ITEM_QTY/100) END) else SUM(NVL(b.ITEM_QTY,0)+NVL(b.REJECT_QTY,0))*(bd.ITEM_QTY+NVL(FN_GET_FALSE_LUG_WT(bm.BOM_ITEM_ID,bd.ITEM_ID),0)) END,3) USED_QTY_AS_BOM ,FN_USED_RAW_MATERIAL_DTL_QTY(a.PROD_ID,bd.ITEM_ID,b.ITEM_ID,a.DEPT_ID,b.MACHINE_ID,:P_FromDate,:P_ToDate,:P_autho_status) RM_ACTUAL_USED ");
                    sb.Append(" ,FN_USED_RAW_MATERIAL_DTL_QTY(a.PROD_ID,bd.ITEM_ID,b.ITEM_ID,a.DEPT_ID,b.MACHINE_ID,:T_FromDate,:T_ToDate,:T_autho_status)-ROUND(CASE WHEN BU.UOM_CODE='Percent' THEN (CASE WHEN SUM(NVL(b.MIXER_BATCH_QTY,0))>0 THEN SUM(NVL(b.MIXER_BATCH_QTY,0))*(bd.ITEM_QTY/100) else SUM(NVL(b.ITEM_QTY,0))*(bd.ITEM_QTY/100) END) else SUM(NVL(b.ITEM_QTY,0)+NVL(b.REJECT_QTY,0))*(bd.ITEM_QTY+NVL(FN_GET_FALSE_LUG_WT(bm.BOM_ITEM_ID,bd.ITEM_ID),0)) END,3)  USE_DIFF ");
                    sb.Append(" FROM PRODUCTION_MST a ");
                    sb.Append(" INNER JOIN PRODUCTION_DTL b ON a.PROD_ID=b.PROD_MST_ID ");
                    sb.Append(" INNER JOIN INV_ITEM_MASTER c ON b.ITEM_ID=c.ITEM_ID ");
                    sb.Append(" LEFT JOIN BOM_MST_T bm ON b.BOM_ID=bm.BOM_ID ");
                    sb.Append(" LEFT JOIN BOM_DTL_T bd ON bm.BOM_ID=bd.BOM_MST_ID ");
                    sb.Append(" LEFT JOIN INV_ITEM_MASTER bi ON bd.ITEM_ID=bi.ITEM_ID ");
                    sb.Append(" INNER JOIN UOM_INFO UI ON b.UOM_ID=UI.UOM_ID ");
                    sb.Append(" INNER JOIN UOM_INFO BU ON bd.ITEM_UNIT_ID=BU.UOM_ID ");
                    sb.Append(" Where 1=1 ");

                    if (prmINV.FromDate != null)
                    {
                        sb.Append(" AND  a.PRODUCTION_DATE BETWEEN  :FromDate and :ToDate ");

                        cmdInfo.DBParametersInfo.Add(":P_FromDate", prmINV.FromDate);
                        cmdInfo.DBParametersInfo.Add(":P_ToDate", prmINV.ToDate);
                        cmdInfo.DBParametersInfo.Add(":P_autho_status", prmINV.autho_status);
                        cmdInfo.DBParametersInfo.Add(":T_FromDate", prmINV.FromDate);
                        cmdInfo.DBParametersInfo.Add(":T_ToDate", prmINV.ToDate);
                        cmdInfo.DBParametersInfo.Add(":T_autho_status", prmINV.autho_status);


                        cmdInfo.DBParametersInfo.Add(":FromDate", prmINV.FromDate);
                        cmdInfo.DBParametersInfo.Add(":ToDate", prmINV.ToDate);
                    }
                    if (prmINV.item_id > 0)
                    {
                        sb.Append(" AND b.ITEM_ID=:Item_ID ");
                        cmdInfo.DBParametersInfo.Add(":Item_ID", prmINV.item_id);
                    }

                    if (prmINV.DeptID > 0)
                    {
                        sb.Append(" AND a.DEPT_ID=:DeptID");
                        cmdInfo.DBParametersInfo.Add(":DeptID", prmINV.DeptID);
                    }
                    sb.Append(" AND a.AUTH_STATUS=:autho_status");
                    cmdInfo.DBParametersInfo.Add(":autho_status", prmINV.autho_status);
                    //sb.Append(" and b.ITEM_ID=6690 ");
                    if (prmINV.StorageLocationId > 0)
                    {
                        sb.Append(" AND a.STLM_ID=:StorageLocationId");
                        cmdInfo.DBParametersInfo.Add(":StorageLocationId", prmINV.StorageLocationId);
                    }


                    sb.Append(" GROUP BY a.Production_Date,c.ITEM_NAME,UI.UOM_CODE,bi.ITEM_NAME,BU.UOM_CODE,bm.BOM_ITEM_DESC,a.PROD_ID,bd.ITEM_ID,b.ITEM_ID,a.DEPT_ID,b.MACHINE_ID,bd.ITEM_QTY,bm.BOM_ITEM_ID  ) WHERE PRODUCTION_QTY>0 ORDER BY Production_Date,ITEM_NAME ");
                }
                else
                {
                    sb.Append(" SELECT PROD_ID,Production_Date,ITEM_NAME,UOM_CODE,RM_ITEM_NAME,RM_UOM_CODE,BOM_ITEM_DESC,PRODUCTION_QTY,ITEM_STANDARD_WEIGHT_KG ");
                    sb.Append(" ,USED_QTY_AS_BOM,RM_ACTUAL_USED,USE_DIFF  ");
                    sb.Append(" ,CASE WHEN ROUND(USE_DIFF,2)<>0 AND ROUND(USED_QTY_AS_BOM)<>0 THEN ROUND((ROUND(USE_DIFF)/ROUND(USED_QTY_AS_BOM))*100,2) else 0 END USE_PER ");
                    sb.Append(" FROM ( ");
                    sb.Append(" Select a.PROD_ID,a.Production_Date,c.ITEM_NAME,UI.UOM_CODE,bi.ITEM_NAME RM_ITEM_NAME,BU.UOM_CODE RM_UOM_CODE,bm.BOM_ITEM_DESC ");
                    sb.Append(" ,SUM(NVL(b.RCV_QTY,0))+NVL(FN_USED_FG_REJECT_PARTING_QTY(a.DEPT_ID,b.ITEM_ID,:FN_FromDate,:FN_ToDate,:FN_autho_status),0) PRODUCTION_QTY, bd.ITEM_QTY ITEM_STANDARD_WEIGHT_KG,CASE WHEN BU.UOM_CODE='Percent' THEN (CASE WHEN SUM(NVL((SELECT MIXER_BATCH_QTY FROM PRODUCTION_DTL Where PROD_MST_ID=a.PROD_ID and ITEM_ID=b.ITEM_ID and MIXER_BATCH_QTY>0),0))>0 THEN SUM(NVL((SELECT MIXER_BATCH_QTY FROM PRODUCTION_DTL Where PROD_MST_ID=a.PROD_ID and ITEM_ID=b.ITEM_ID and MIXER_BATCH_QTY>0),0))*(bd.ITEM_QTY/100) Else SUM(NVL(b.RCV_QTY,0))*(bd.ITEM_QTY/100) END) else (SUM(NVL(b.RCV_QTY,0))+NVL(FN_USED_FG_REJECT_PARTING_QTY(a.DEPT_ID,b.ITEM_ID,:FN1_FromDate,:FN1_ToDate,:FN1_autho_status),0))*(bd.ITEM_QTY+NVL(FN_GET_FALSE_LUG_WT(bm.BOM_ITEM_ID,bd.ITEM_ID),0)) END USED_QTY_AS_BOM ");
                    sb.Append(" ,FN_USED_RAW_MAT_NOM_DTL_QTY(a.PROD_ID,bd.ITEM_ID,b.ITEM_ID,a.DEPT_ID,:P_FromDate,:P_ToDate,:P_autho_status) RM_ACTUAL_USED ");
                    sb.Append(" ,FN_USED_RAW_MAT_NOM_DTL_QTY(a.PROD_ID,bd.ITEM_ID,b.ITEM_ID,a.DEPT_ID,:T_FromDate,:T_ToDate,:T_autho_status)-CASE WHEN BU.UOM_CODE='Percent' THEN (CASE WHEN SUM(NVL((SELECT MIXER_BATCH_QTY FROM PRODUCTION_DTL Where PROD_MST_ID=a.PROD_ID and ITEM_ID=b.ITEM_ID and MIXER_BATCH_QTY>0),0))>0 THEN SUM(NVL((SELECT MIXER_BATCH_QTY FROM PRODUCTION_DTL Where PROD_MST_ID=a.PROD_ID and ITEM_ID=b.ITEM_ID and MIXER_BATCH_QTY>0),0))*(bd.ITEM_QTY/100) Else SUM(NVL(b.RCV_QTY,0))*(bd.ITEM_QTY/100) END) else (SUM(NVL(b.RCV_QTY,0))+NVL(FN_USED_FG_REJECT_PARTING_QTY(a.DEPT_ID,b.ITEM_ID,:FN2_FromDate,:FN2_ToDate,:FN2_autho_status),0))*(bd.ITEM_QTY+NVL(FN_GET_FALSE_LUG_WT(bm.BOM_ITEM_ID,bd.ITEM_ID),0)) END  USE_DIFF ");
                    sb.Append(" FROM PRODUCTION_MST a ");
                    sb.Append(" INNER JOIN ITEM_STOCK_DETAILS b ON a.PROD_NO=b.TRANS_REF_NO ");
                    sb.Append(" INNER JOIN INV_ITEM_MASTER c ON b.ITEM_ID=c.ITEM_ID ");
                    sb.Append(" LEFT JOIN BOM_MST_T bm ON b.ITEM_ID=bm.BOM_ITEM_ID ");
                    sb.Append(" LEFT JOIN BOM_DTL_T bd ON bm.BOM_ID=bd.BOM_MST_ID ");
                    sb.Append(" LEFT JOIN INV_ITEM_MASTER bi ON bd.ITEM_ID=bi.ITEM_ID ");
                    sb.Append(" INNER JOIN UOM_INFO UI ON b.UOM_ID=UI.UOM_ID ");
                    sb.Append(" INNER JOIN UOM_INFO BU ON bd.ITEM_UNIT_ID=BU.UOM_ID ");
                    sb.Append(" Where 1=1 ");

                    if (prmINV.FromDate != null)
                    {
                        sb.Append(" AND  a.PRODUCTION_DATE BETWEEN  :FromDate and :ToDate ");

                        cmdInfo.DBParametersInfo.Add(":P_FromDate", prmINV.FromDate);
                        cmdInfo.DBParametersInfo.Add(":P_ToDate", prmINV.ToDate);
                        cmdInfo.DBParametersInfo.Add(":P_autho_status", prmINV.autho_status);
                        cmdInfo.DBParametersInfo.Add(":T_FromDate", prmINV.FromDate);
                        cmdInfo.DBParametersInfo.Add(":T_ToDate", prmINV.ToDate);
                        cmdInfo.DBParametersInfo.Add(":T_autho_status", prmINV.autho_status);

                        cmdInfo.DBParametersInfo.Add(":FN_FromDate", prmINV.FromDate);
                        cmdInfo.DBParametersInfo.Add(":FN_ToDate", prmINV.ToDate);
                        cmdInfo.DBParametersInfo.Add(":FN_autho_status", prmINV.autho_status);
                        cmdInfo.DBParametersInfo.Add(":FN1_FromDate", prmINV.FromDate);
                        cmdInfo.DBParametersInfo.Add(":FN1_ToDate", prmINV.ToDate);
                        cmdInfo.DBParametersInfo.Add(":FN1_autho_status", prmINV.autho_status);

                        cmdInfo.DBParametersInfo.Add(":FN2_FromDate", prmINV.FromDate);
                        cmdInfo.DBParametersInfo.Add(":FN2_ToDate", prmINV.ToDate);
                        cmdInfo.DBParametersInfo.Add(":FN2_autho_status", prmINV.autho_status);

                        cmdInfo.DBParametersInfo.Add(":FromDate", prmINV.FromDate);
                        cmdInfo.DBParametersInfo.Add(":ToDate", prmINV.ToDate);
                    }
                    if (prmINV.item_id > 0)
                    {
                        sb.Append(" AND b.ITEM_ID=:Item_ID ");
                        cmdInfo.DBParametersInfo.Add(":Item_ID", prmINV.item_id);
                    }

                    if (prmINV.DeptID > 0)
                    {
                        sb.Append(" AND a.DEPT_ID=:DeptID");
                        cmdInfo.DBParametersInfo.Add(":DeptID", prmINV.DeptID);
                    }
                    sb.Append(" AND a.AUTH_STATUS=:autho_status");
                    cmdInfo.DBParametersInfo.Add(":autho_status", prmINV.autho_status);
                    if (prmINV.StorageLocationId > 0)
                    {
                        sb.Append(" AND bm.STLM_ID=:StorageLocationId");
                        cmdInfo.DBParametersInfo.Add(":StorageLocationId", prmINV.StorageLocationId);
                    }


                    sb.Append(" GROUP BY a.Production_Date, c.ITEM_NAME, UI.UOM_CODE,bi.ITEM_NAME,BU.UOM_CODE,bm.BOM_ITEM_DESC , bd.ITEM_QTY  ,a.PROD_ID,bd.ITEM_ID,b.ITEM_ID,a.DEPT_ID,bm.BOM_ITEM_ID  ");
                    sb.Append(" UNION ALL ");


                    sb.Append(" Select a.PROD_ID,a.Production_Date,c.ITEM_NAME,UI.UOM_CODE,bi.ITEM_NAME RM_ITEM_NAME,BU.UOM_CODE RM_UOM_CODE,'Extra Item' BOM_ITEM_DESC ");
                    sb.Append(" ,0 PRODUCTION_QTY,0 ITEM_STANDARD_WEIGHT_KG, 0 USED_QTY_AS_BOM ");
                    sb.Append(" ,SUM(NVL(fc.ISSUE_STOCK,0)) RM_ACTUAL_USED ");
                    sb.Append(" ,0  USE_DIFF ");
                    sb.Append(" FROM PRODUCTION_MST a ");
                    sb.Append(" INNER JOIN PRODUCTION_DTL b ON a.PROD_ID=b.PROD_MST_ID ");
                    sb.Append(" INNER JOIN INV_ITEM_MASTER c ON b.ITEM_ID=c.ITEM_ID ");
                    sb.Append(" LEFT JOIN PRODUCTION_FLOOR_CLOSING fc ON a.PROD_ID=fc.PROD_MST_ID and b.ITEM_ID=fc.FINISHED_ITEM_ID ");
                    sb.Append(" LEFT JOIN INV_ITEM_MASTER bi ON fc.CLOSING_ITEM_ID=bi.ITEM_ID ");
                    sb.Append(" INNER JOIN UOM_INFO UI ON b.UOM_ID=UI.UOM_ID ");
                    sb.Append(" INNER JOIN UOM_INFO BU ON fc.CLOSING_UOM_ID=BU.UOM_ID ");
                    sb.Append(" Where 1=1 ");

                    if (prmINV.FromDate != null)
                    {
                        sb.Append(" AND  a.PRODUCTION_DATE BETWEEN  :FromDate and :ToDate ");


                        cmdInfo.DBParametersInfo.Add(":FromDate", prmINV.FromDate);
                        cmdInfo.DBParametersInfo.Add(":ToDate", prmINV.ToDate);
                    }
                    if (prmINV.item_id > 0)
                    {
                        sb.Append(" AND b.ITEM_ID=:Item_ID ");
                        cmdInfo.DBParametersInfo.Add(":Item_ID", prmINV.item_id);
                    }

                    if (prmINV.DeptID > 0)
                    {
                        sb.Append(" AND a.DEPT_ID=:DeptID");
                        cmdInfo.DBParametersInfo.Add(":DeptID", prmINV.DeptID);
                    }
                    sb.Append(" AND a.AUTH_STATUS=:autho_status");
                    cmdInfo.DBParametersInfo.Add(":autho_status", prmINV.autho_status);
                    sb.Append(" AND fc.ISMANUAL=1 ");
                    if (prmINV.StorageLocationId > 0)
                    {
                        sb.Append(" AND a.STLM_ID=:StorageLocationId");
                        cmdInfo.DBParametersInfo.Add(":StorageLocationId", prmINV.StorageLocationId);
                    }

                    sb.Append(" GROUP BY c.ITEM_NAME,UI.UOM_CODE,bi.ITEM_NAME,BU.UOM_CODE,a.PROD_ID,a.Production_Date ");


                    sb.Append("     ) Where (PRODUCTION_QTY>0 OR RM_ACTUAL_USED<>0) ORDER BY Production_Date,ITEM_NAME ");

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
                    rcBomVsProduction stk = new rcBomVsProduction();
                    //stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    //stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    stk.PRODUCTION_DATE = Conversion.DBNullDateToNull(dRow["PRODUCTION_DATE"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();
                    //stk.ITEM_CODE = dRow["ITEM_CODE"].ToString();

                    //stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_CODE"].ToString();
                    //stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);

                    // stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                    // stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    //  stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();
                    //   stk.OP_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_IRR_QTY"]);
                    // stk.OP_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ITC_QTY"]);
                    // stk.OPPENING_BAL_QTY = stk.OP_IRR_QTY - stk.OP_ITC_QTY;

                    // stk.IRR_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_DEPT_QTY"]);
                    //   stk.IRR_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_STORE_QTY"]);
                    stk.IRR_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["PRODUCTION_QTY"]);

                    //here total IRR quantity
                    // stk.IRR_BAL_QTY = stk.IRR_DEPT_QTY + stk.IRR_STORE_QTY + stk.IRR_PROD_QTY;

                    // stk.ITC_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_DEPT_QTY"]);
                    //stk.ITC_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_STORE_QTY"]);
                    //   stk.ITC_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_PROD_QTY"]);

                    //here total ITC quantity
                    // stk.ITC_BAL_QTY = stk.ITC_DEPT_QTY + stk.ITC_STORE_QTY + stk.ITC_PROD_QTY;

                    //here total Adjust balance quantity
                    //  stk.ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);

                    //  stk.DEPARTMENT_NAME = prmINV.From_Dept_Name;
                    //  stk.COLISING_QTY = (stk.OPPENING_BAL_QTY + stk.ADJUST_QTY + stk.IRR_BAL_QTY) - stk.ITC_BAL_QTY;

                    //BOM Raw material Part

                    //stk.RM_ITEM_ID = Conversion.DBNullIntToZero(dRow["RM_ITEM_ID"]);
                    stk.RM_ITEM_NAME = dRow["RM_ITEM_NAME"].ToString();
                    // stk.RM_UOM_ID = Conversion.DBNullIntToZero(dRow["RM_UOM_ID"]);
                    stk.RM_UOM_NAME = dRow["RM_UOM_CODE"].ToString();
                    //  stk.RM_ITEM_CODE = dRow["RM_ITEM_CODE"].ToString();


                    stk.RM_ACTUAL_QTY = Conversion.DBNullDecimalToZero(dRow["RM_ACTUAL_USED"]);
                    stk.STD_UNIT_QTY = Conversion.DBNullDecimalToZero(dRow["ITEM_STANDARD_WEIGHT_KG"]);
                    stk.RM_STD_QTY = Conversion.DBNullDecimalToZero(dRow["USED_QTY_AS_BOM"]);


                    //stk.RM_WASTAGE_PERCENT = Conversion.DBNullDecimalToZero(dRow["ITEM_STANDARD_WEIGHT_KG"]);
                    //stk.RM_STD_WT = Conversion.DBNullDecimalToZero(dRow["ITEM_STANDARD_WEIGHT_KG"]);
                    stk.RM_USE_DIFF = Conversion.DBNullDecimalToZero(dRow["USE_DIFF"]);
                    stk.USE_PER = Conversion.DBNullDecimalToZero(dRow["USE_PER"]);

                    //stk.RM_ACTUAL_WASTAGE_PERCENT = Conversion.DBNullDecimalToZero(dRow["RM_ACTUAL_WASTAGE_PERCENT"]);








                    cRptList.Add(stk);

                }

            }
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }

            return cRptList;

        }

        #endregion

        public static List<rcMaterialStock> DepartmentStockReport(clsPrmInventory prmINV)
        {
            return DepartmentStockReport(prmINV, null);
        }

        public static List<rcMaterialStock> DepartmentStockReport(clsPrmInventory prmINV, DBContext dc)
        {

            List<rcMaterialStock> cRptList = new List<rcMaterialStock>();


            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();

                if (prmINV.ToDate.HasValue)
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.ToDate.Value);
                }
                cmdInfo.DBParametersInfo.Add(":p_itemtype_id", prmINV.itemtype_id);
                cmdInfo.DBParametersInfo.Add(":p_itemGroup_id", prmINV.itemGroup_id);
                cmdInfo.DBParametersInfo.Add(":p_item_id", prmINV.item_id);
                cmdInfo.DBParametersInfo.Add(":p_store_id", prmINV.store_id);
                cmdInfo.DBParametersInfo.Add(":p_itemclass_id", prmINV.ItemClassId);
                cmdInfo.DBParametersInfo.Add(":p_battery_type_id", prmINV.Battery_Type_ID);
                cmdInfo.DBParametersInfo.Add(":p_is_repair", prmINV.Is_Repair);
                cmdInfo.DBParametersInfo.Add(":p_dept_id", prmINV.DeptID);


                //cmdInfo.DBParametersInfo.Add(":P_ITEM_CODE", prmSND.Item_Code);
                DBQuery dbq = new DBQuery();

                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                //dbq.CommandTimeout = 1000;
                cmdInfo.CommandTimeout = 600;


                //this is previous item stock report
                //cmdInfo.CommandText = "SP_DEPARTMENT_STOCK_REPORT";

                //SP_DEPT_ASSIGND_ITEM_STOCK
                //but this is new sp replacing previous department stock report
                cmdInfo.CommandText = "SP_DEPARTMENT_STOCK_REPORT";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);


                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;
                //sb.Append(" SELECT * FROM TEMP_ITEM_STOCK_BAT_FOR_NET ");

                sb.Append(" Select ITEM_GROUP_ID,ITEM_GROUP_NAME,ITEM_ID,ITEM_CODE,ITEM_NAME,UOM_ID,UOM_NAME,ITEM_TYPE_ID,ITEM_TYPE_NAME,ITEM_CLASS_ID,ITEM_CLASS_NAME,OP_IRR_QTY,OP_ITC_QTY,IRR_DEPT_QTY,IRR_STORE_QTY,IRR_PROD_QTY,ITC_DEPT_QTY,ITC_STORE_QTY,ITC_PROD_QTY,ADJUST_QTY,REJECT_QTY,OP_REJECT_QTY,GRID_REJECT_QTY,OP_GRID_REJECT_QTY,STLM_ID,STLM_NAME,PROCESS_LOSS_QTY,CONV_LEAD_QTY FROM TEMP_DEPARTMENT_ITEM_STOCK");
                sb.Append(" ORDER BY STLM_ID,ITEM_NAME ");

                DBQuery dbqtemp = new DBQuery();
                //DBQuery dbq = new DBQuery();


                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                //DBQuery.ExecuteDBQuerySP(dbq, dc);
                DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);

                //,,,,,,,,,,,,,,,,,


                foreach (DataRow dRow in dtData.Rows)
                {
                    rcMaterialStock stk = new rcMaterialStock();
                    stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();

                    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);

                    stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                    stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();
                    stk.OP_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_IRR_QTY"]);
                    stk.OP_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ITC_QTY"]);
                    stk.OP_REJECT_QTY = Conversion.DBNullDecimalToZero(dRow["OP_REJECT_QTY"]);
                    stk.OP_GRID_REJECT_QTY = Conversion.DBNullDecimalToZero(dRow["OP_GRID_REJECT_QTY"]);
                    stk.OPPENING_BAL_QTY = stk.OP_IRR_QTY - stk.OP_ITC_QTY - stk.OP_REJECT_QTY - stk.OP_GRID_REJECT_QTY;

                    stk.IRR_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_DEPT_QTY"]);
                    stk.IRR_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_STORE_QTY"]);
                    stk.IRR_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_PROD_QTY"]);
                    stk.CONV_LEAD_QTY = Conversion.DBNullDecimalToZero(dRow["CONV_LEAD_QTY"]);

                    stk.REJECT_QTY = Conversion.DBNullDecimalToZero(dRow["REJECT_QTY"]);
                    stk.GRID_REJECT_QTY = Conversion.DBNullDecimalToZero(dRow["GRID_REJECT_QTY"]);

                    stk.PROCESS_LOSS_QTY = Conversion.DBNullDecimalToZero(dRow["PROCESS_LOSS_QTY"]);
                  

                    //here total IRR quantity
                    stk.IRR_BAL_QTY = stk.IRR_DEPT_QTY + stk.IRR_STORE_QTY + stk.IRR_PROD_QTY + stk.CONV_LEAD_QTY;

                    stk.ITC_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_DEPT_QTY"]);
                    stk.ITC_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_STORE_QTY"]);
                    stk.ITC_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_PROD_QTY"]);

                    //here total ITC quantity
                    stk.ITC_BAL_QTY = stk.ITC_DEPT_QTY + stk.ITC_STORE_QTY + stk.ITC_PROD_QTY;

                    //here total Adjust balance quantity
                    stk.ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);
                    stk.STLM_ID = Conversion.DBNullIntToZero(dRow["STLM_ID"]);
                    stk.STLM_NAME = dRow["STLM_NAME"].ToString();


                    stk.DEPARTMENT_NAME = prmINV.From_Dept_Name;
                    // stk.BAL_QTY = Conversion.DBNullDecimalToZero(dRow["BAL_QTY"]);
                    stk.COLISING_QTY = (stk.OPPENING_BAL_QTY + stk.ADJUST_QTY + stk.IRR_BAL_QTY) - stk.ITC_BAL_QTY - stk.REJECT_QTY - stk.GRID_REJECT_QTY;

                    if(stk.GRID_REJECT_QTY > 0 && stk.REJECT_QTY == 0)
                    {
                        stk.REJECT_QTY = stk.GRID_REJECT_QTY;
                    }

                    //cRptList.Add(stk); comment out by mamun 01-mar-2022
                    // open from comment out by mamun 01-mar-2022
                    if (stk.OPPENING_BAL_QTY == 0)
                    {
                        if (stk.IRR_BAL_QTY != 0 || stk.ITC_BAL_QTY != 0 || stk.COLISING_QTY != 0)
                        {
                            cRptList.Add(stk);
                        }
                    }
                    else
                    {
                        cRptList.Add(stk);
                    }
                    //end comment out 01-mar-2022
                }

                //int COUNT = cRptList.Count;


            }
            //catch { throw; }
            //finally 
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }

            return cRptList;

        }

        public static List<rcMaterialStock> DepartmentStockReportForPlaning(clsPrmInventory prmINV)
        {
            return DepartmentStockReportForPlaning(prmINV, null);
        }

        public static List<rcMaterialStock> DepartmentStockReportForPlaning(clsPrmInventory prmINV, DBContext dc)
        {

            List<rcMaterialStock> cRptList = new List<rcMaterialStock>();


            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();

                if (prmINV.ToDate.HasValue)
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.ToDate.Value);
                }
                cmdInfo.DBParametersInfo.Add(":p_itemtype_id", prmINV.itemtype_id);
                cmdInfo.DBParametersInfo.Add(":p_itemGroup_id", prmINV.itemGroup_id);
                cmdInfo.DBParametersInfo.Add(":p_item_id", prmINV.item_id);
                cmdInfo.DBParametersInfo.Add(":p_store_id", prmINV.store_id);
                cmdInfo.DBParametersInfo.Add(":p_itemclass_id", prmINV.ItemClassId);
                cmdInfo.DBParametersInfo.Add(":p_battery_type_id", prmINV.Battery_Type_ID);
                cmdInfo.DBParametersInfo.Add(":p_is_repair", prmINV.Is_Repair);
                cmdInfo.DBParametersInfo.Add(":p_dept_id", prmINV.DeptID);
                cmdInfo.DBParametersInfo.Add(":p_stlm_id", prmINV.STLM_ID);


                //cmdInfo.DBParametersInfo.Add(":P_ITEM_CODE", prmSND.Item_Code);
                DBQuery dbq = new DBQuery();

                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                //dbq.CommandTimeout = 1000;
                cmdInfo.CommandTimeout = 600;


                //this is previous item stock report
                //cmdInfo.CommandText = "SP_DEPARTMENT_STOCK_REPORT";

                //SP_DEPT_ASSIGND_ITEM_STOCK
                //but this is new sp replacing previous department stock report
                cmdInfo.CommandText = "SP_DEPARTMENT_STOCK_RPT_PLAN";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);


                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;
                //sb.Append(" SELECT * FROM TEMP_ITEM_STOCK_BAT_FOR_NET ");

                sb.Append(" Select ITEM_GROUP_ID,ITEM_GROUP_NAME,ITEM_ID,ITEM_CODE,ITEM_NAME,UOM_ID,UOM_NAME,ITEM_TYPE_ID,ITEM_TYPE_NAME,ITEM_CLASS_ID,ITEM_CLASS_NAME,OP_IRR_QTY,OP_ITC_QTY,IRR_DEPT_QTY,IRR_STORE_QTY,IRR_PROD_QTY,ITC_DEPT_QTY,ITC_STORE_QTY,ITC_PROD_QTY,ADJUST_QTY,REJECT_QTY,OP_REJECT_QTY,GRID_REJECT_QTY,OP_GRID_REJECT_QTY,DEPARTMENT_ID,DEPARTMENT_NAME,STLM_ID,STLM_NAME,PROCESS_LOSS_QTY FROM TEMP_DEPARTMENT_ITEM_STOCK");


                DBQuery dbqtemp = new DBQuery();
                //DBQuery dbq = new DBQuery();


                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                //DBQuery.ExecuteDBQuerySP(dbq, dc);
                DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);

                //,,,,,,,,,,,,,,,,,


                foreach (DataRow dRow in dtData.Rows)
                {
                    rcMaterialStock stk = new rcMaterialStock();
                    stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();
                    stk.ITEM_CODE = dRow["ITEM_CODE"].ToString();

                    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);

                    stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                    stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();
                    stk.OP_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_IRR_QTY"]);
                    stk.OP_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ITC_QTY"]);
                    stk.OP_REJECT_QTY = Conversion.DBNullDecimalToZero(dRow["OP_REJECT_QTY"]);
                    stk.OP_GRID_REJECT_QTY = Conversion.DBNullDecimalToZero(dRow["OP_GRID_REJECT_QTY"]);
                    stk.OPPENING_BAL_QTY = stk.OP_IRR_QTY - stk.OP_ITC_QTY - stk.OP_REJECT_QTY - stk.OP_GRID_REJECT_QTY;

                    stk.IRR_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_DEPT_QTY"]);
                    stk.IRR_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_STORE_QTY"]);
                    stk.IRR_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_PROD_QTY"]);
                    stk.ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);
                    stk.REJECT_QTY = Conversion.DBNullDecimalToZero(dRow["REJECT_QTY"]);
                    stk.GRID_REJECT_QTY = Conversion.DBNullDecimalToZero(dRow["GRID_REJECT_QTY"]);

                    stk.PROCESS_LOSS_QTY = Conversion.DBNullDecimalToZero(dRow["PROCESS_LOSS_QTY"]);


                    //here total IRR quantity
                    stk.IRR_BAL_QTY = stk.IRR_DEPT_QTY + stk.IRR_STORE_QTY + stk.IRR_PROD_QTY ;

                    stk.ITC_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_DEPT_QTY"]);
                    stk.ITC_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_STORE_QTY"]);
                    stk.ITC_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_PROD_QTY"]);

                    //here total ITC quantity
                    stk.ITC_BAL_QTY = stk.ITC_DEPT_QTY + stk.ITC_STORE_QTY + stk.ITC_PROD_QTY + stk.REJECT_QTY + stk.GRID_REJECT_QTY;

                    //here total Adjust balance quantity
                  
                    stk.STLM_ID = Conversion.DBNullIntToZero(dRow["STLM_ID"]);
                    stk.STLM_NAME = dRow["STLM_NAME"].ToString();

                    stk.DEPARTMENT_ID = Conversion.DBNullIntToZero(dRow["DEPARTMENT_ID"]);
                    stk.DEPARTMENT_NAME = dRow["DEPARTMENT_NAME"].ToString();
                    // stk.BAL_QTY = Conversion.DBNullDecimalToZero(dRow["BAL_QTY"]);
                    stk.COLISING_QTY = (stk.OPPENING_BAL_QTY + stk.IRR_BAL_QTY + stk.ADJUST_QTY) - stk.ITC_BAL_QTY;

                    if (stk.GRID_REJECT_QTY > 0 && stk.REJECT_QTY == 0)
                    {
                        stk.REJECT_QTY = stk.GRID_REJECT_QTY;
                    }

                    //cRptList.Add(stk); comment out by mamun 01-mar-2022
                    // open from comment out by mamun 01-mar-2022
                    if (stk.OPPENING_BAL_QTY == 0)
                    {
                        if (stk.IRR_BAL_QTY != 0 || stk.ITC_BAL_QTY != 0 || stk.COLISING_QTY != 0)
                        {
                            cRptList.Add(stk);
                        }
                    }
                    else
                    {
                        cRptList.Add(stk);
                    }
                    //end comment out 01-mar-2022
                }




            }
            //catch { throw; }
            //finally 
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }

            return cRptList;

        }

        public static Byte[] Get_ProductionStock_Summary_ExcelData(clsPrmInventory pObj, bool pExecuteSP, DBContext dc)
        {
            List<rcMaterialStock> cRptList = new List<rcMaterialStock>();

            bool isDCInit = false;

            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

            DBCommandInfo cmdInfo = new DBCommandInfo();
            StringBuilder sb = new StringBuilder();

            cmdInfo.DBParametersInfo.Clear();

            if (pObj.ToDate.HasValue)
            {
                cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", pObj.FromDate.Value);
                cmdInfo.DBParametersInfo.Add(":P_DATE_TO", pObj.ToDate.Value);
            }
            cmdInfo.DBParametersInfo.Add(":p_itemtype_id", pObj.itemtype_id);
            cmdInfo.DBParametersInfo.Add(":p_itemGroup_id", pObj.itemGroup_id);
            cmdInfo.DBParametersInfo.Add(":p_item_id", pObj.item_id);
            cmdInfo.DBParametersInfo.Add(":p_store_id", pObj.store_id);
            cmdInfo.DBParametersInfo.Add(":p_itemclass_id", pObj.ItemClassId);
            cmdInfo.DBParametersInfo.Add(":p_battery_type_id", pObj.Battery_Type_ID);
            cmdInfo.DBParametersInfo.Add(":p_is_repair", pObj.Is_Repair);
            cmdInfo.DBParametersInfo.Add(":p_dept_id", pObj.DeptID);
            cmdInfo.DBParametersInfo.Add(":p_stlm_id", pObj.STLM_ID);


            DBQuery dbq = new DBQuery();

            dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
            cmdInfo.CommandTimeout = 600;



            cmdInfo.CommandText = "SP_DEPARTMENT_STOCK_RPT_PLAN";
            cmdInfo.CommandType = CommandType.StoredProcedure;
            dbq.DBCommandInfo = cmdInfo;
            DBQuery.ExecuteDBQuerySP(dbq, dc);


            DBCommandInfo cmdInfotemp = new DBCommandInfo();
            sb.Length = 0;

            sb.Append(" Select ITEM_GROUP_ID,ITEM_GROUP_NAME,ITEM_ID,ITEM_CODE,ITEM_NAME,UOM_ID,UOM_NAME,ITEM_TYPE_ID,ITEM_TYPE_NAME,ITEM_CLASS_ID,ITEM_CLASS_NAME,OP_IRR_QTY,OP_ITC_QTY,IRR_DEPT_QTY,IRR_STORE_QTY,IRR_PROD_QTY,ITC_DEPT_QTY,ITC_STORE_QTY,ITC_PROD_QTY,ADJUST_QTY,REJECT_QTY,OP_REJECT_QTY,GRID_REJECT_QTY,OP_GRID_REJECT_QTY,DEPARTMENT_ID,DEPARTMENT_NAME,STLM_ID,STLM_NAME,PROCESS_LOSS_QTY FROM TEMP_DEPARTMENT_ITEM_STOCK");
            sb.Append(" ORDER BY DEPARTMENT_NAME,STLM_ID,ITEM_CODE ");

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

            foreach (DataRow dRow in dtData.Rows)
            {
                rcMaterialStock stk = new rcMaterialStock();
                stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();
                stk.ITEM_CODE = dRow["ITEM_CODE"].ToString();

                stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);

                stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();
                stk.OP_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_IRR_QTY"]);
                stk.OP_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ITC_QTY"]);
                stk.OP_REJECT_QTY = Conversion.DBNullDecimalToZero(dRow["OP_REJECT_QTY"]);
                stk.OP_GRID_REJECT_QTY = Conversion.DBNullDecimalToZero(dRow["OP_GRID_REJECT_QTY"]);
                stk.OPPENING_BAL_QTY = stk.OP_IRR_QTY - stk.OP_ITC_QTY - stk.OP_REJECT_QTY - stk.OP_GRID_REJECT_QTY;

                stk.IRR_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_DEPT_QTY"]);
                stk.IRR_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_STORE_QTY"]);
                stk.IRR_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_PROD_QTY"]);
                stk.ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);
                stk.REJECT_QTY = Conversion.DBNullDecimalToZero(dRow["REJECT_QTY"]);
                stk.GRID_REJECT_QTY = Conversion.DBNullDecimalToZero(dRow["GRID_REJECT_QTY"]);

                stk.PROCESS_LOSS_QTY = Conversion.DBNullDecimalToZero(dRow["PROCESS_LOSS_QTY"]);


                //here total IRR quantity
                stk.IRR_BAL_QTY = stk.IRR_DEPT_QTY + stk.IRR_STORE_QTY + stk.IRR_PROD_QTY;

                stk.ITC_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_DEPT_QTY"]);
                stk.ITC_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_STORE_QTY"]);
                stk.ITC_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_PROD_QTY"]);

                //here total ITC quantity
                stk.ITC_BAL_QTY = stk.ITC_DEPT_QTY + stk.ITC_STORE_QTY + stk.ITC_PROD_QTY + stk.REJECT_QTY + stk.GRID_REJECT_QTY;

                //here total Adjust balance quantity

                stk.STLM_ID = Conversion.DBNullIntToZero(dRow["STLM_ID"]);
                stk.STLM_NAME = dRow["STLM_NAME"].ToString();

                stk.DEPARTMENT_ID = Conversion.DBNullIntToZero(dRow["DEPARTMENT_ID"]);
                stk.DEPARTMENT_NAME = dRow["DEPARTMENT_NAME"].ToString();
                // stk.BAL_QTY = Conversion.DBNullDecimalToZero(dRow["BAL_QTY"]);
                stk.CLOSING_QTY = (stk.OPPENING_BAL_QTY + stk.IRR_BAL_QTY + stk.ADJUST_QTY) - stk.ITC_BAL_QTY;

                //if (stk.GRID_REJECT_QTY > 0 && stk.REJECT_QTY == 0)
                //{
                //    stk.REJECT_QTY = stk.GRID_REJECT_QTY;
                //}

              
                if (stk.OPPENING_BAL_QTY == 0)
                {
                    if (stk.IRR_BAL_QTY != 0 || stk.ITC_BAL_QTY != 0 || stk.CLOSING_QTY != 0)
                    {
                        cRptList.Add(stk);
                    }
                }
                else
                {
                    cRptList.Add(stk);
                }
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
                workSheet.Cells[1, colNum].Value = "Item Code";


                colNum = 5;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Item Name";



                colNum = 6;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "UOM";

                colNum = 7;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Opening Qty";

                colNum = 8;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Rcv. From Prod.";



                colNum = 9;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Issue To Prod.";


                colNum = 10;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Adjustment Qty";

                colNum = 11;
                workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, colNum].Value = "Closing Qty";



                //Body of table  

                int recordIndex = 2;
                int slno = 0;
                foreach (rcMaterialStock stk in cRptList)
                {
                    slno++;
                    workSheet.Cells[recordIndex, 1].Value = slno;

                    workSheet.Cells[recordIndex, 2].Value = stk.DEPARTMENT_NAME;
                    workSheet.Cells[recordIndex, 3].Value = stk.STLM_NAME;
                    workSheet.Cells[recordIndex, 4].Value = stk.ITEM_CODE;
                    workSheet.Cells[recordIndex, 5].Value = stk.ITEM_NAME;
                    workSheet.Cells[recordIndex, 6].Value = stk.UOM_NAME;
                    workSheet.Cells[recordIndex, 7].Value = stk.OPPENING_BAL_QTY;
                    workSheet.Cells[recordIndex, 8].Value = stk.IRR_BAL_QTY;
                    workSheet.Cells[recordIndex, 9].Value = stk.ITC_BAL_QTY;
                    workSheet.Cells[recordIndex, 10].Value = stk.ADJUST_QTY;
                    workSheet.Cells[recordIndex, 11].Value = stk.CLOSING_QTY;


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


                bytes = excel.GetAsByteArray();

            }

            return bytes;

        }

        public static List<rcMaterialStock> DepartmentRejectStockReport(clsPrmInventory prmINV)
        {
            return DepartmentRejectStockReport(prmINV, null);
        }

        public static List<rcMaterialStock> DepartmentRejectStockReport(clsPrmInventory prmINV, DBContext dc)
        {

            List<rcMaterialStock> cRptList = new List<rcMaterialStock>();


            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();

                if (prmINV.ToDate.HasValue)
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.ToDate.Value);
                }
                cmdInfo.DBParametersInfo.Add(":p_itemGroup_id", prmINV.itemGroup_id);
                cmdInfo.DBParametersInfo.Add(":p_item_id", prmINV.item_id);
                cmdInfo.DBParametersInfo.Add(":p_itemclass_id", prmINV.ItemClassId);
                cmdInfo.DBParametersInfo.Add(":p_dept_id", prmINV.DeptID);
                cmdInfo.DBParametersInfo.Add(":p_stlm_id", prmINV.STLM_ID);

                DBQuery dbq = new DBQuery();

                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;


                cmdInfo.CommandText = "SP_DEPT_REJECT_REPORT";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);


                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;

                sb.Append(" SELECT * FROM TEMP_REJECT_ITEM_STOCK ");
                sb.Append(" ORDER BY STLM_ID,ITEM_CODE ");


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
                    rcMaterialStock stk = new rcMaterialStock();
                    stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();

                    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();

                    stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();
                    //opening receive
                    stk.OP_GRID_REJ_QTY = Conversion.DBNullDecimalToZero(dRow["OP_GRID_REJ_QTY"]);
                    stk.OP_PROD_REJ_RCV_QTY = Conversion.DBNullDecimalToZero(dRow["OP_PROD_REJ_RCV_QTY"]);
                    stk.OP_REJ_TRAN_RCV_QTY = Conversion.DBNullDecimalToZero(dRow["OP_REJ_TRAN_RCV_QTY"]);
                    stk.OP_REJ_RCV_WITH_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["OP_REJ_RCV_WITH_PROD_QTY"]);
                    // opening Issue
                    stk.OP_REJ_TRAN_ISS_QTY = Conversion.DBNullDecimalToZero(dRow["OP_REJ_TRAN_ISS_QTY"]);
                    stk.OP_REJ_ISSUE_FROM_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["OP_REJ_ISSUE_FROM_DEPT_QTY"]);

                    stk.OP_ISSUE_FOR_PRODUCTION = Conversion.DBNullDecimalToZero(dRow["OP_ISSUE_FOR_PRODUCTION"]);
                    stk.OP_ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ADJUST_QTY"]);

                    stk.OPPENING_BAL_QTY = (stk.OP_GRID_REJ_QTY + stk.OP_PROD_REJ_RCV_QTY + stk.OP_REJ_TRAN_RCV_QTY + stk.OP_REJ_RCV_WITH_PROD_QTY + stk.OP_ADJUST_QTY) - stk.OP_REJ_TRAN_ISS_QTY - stk.OP_REJ_ISSUE_FROM_DEPT_QTY - stk.OP_ISSUE_FOR_PRODUCTION;

                    stk.GRID_REJ_QTY = Conversion.DBNullDecimalToZero(dRow["GRID_REJ_QTY"]);
                    stk.PROD_REJ_RCV_QTY = Conversion.DBNullDecimalToZero(dRow["PROD_REJ_RCV_QTY"]);
                    stk.REJ_TRAN_RCV_QTY = Conversion.DBNullDecimalToZero(dRow["REJ_TRAN_RCV_QTY"]);
                    stk.REJ_RCV_WITH_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["REJ_RCV_WITH_PROD_QTY"]);

                    stk.REJ_TRAN_ISS_QTY = Conversion.DBNullDecimalToZero(dRow["REJ_TRAN_ISS_QTY"]);
                    stk.REJ_ISSUE_FROM_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["REJ_ISSUE_FROM_DEPT_QTY"]);
                    stk.ISSUE_TO_BREAKING = Conversion.DBNullDecimalToZero(dRow["ISSUE_TO_BREAKING"]);

                    stk.ISSUE_FOR_PRODUCTION = Conversion.DBNullDecimalToZero(dRow["ISSUE_FOR_PRODUCTION"]);

                    stk.ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);
                    //here total IRR quantity
                    stk.TOTAL_PROD_REJ_RCV = stk.GRID_REJ_QTY + stk.PROD_REJ_RCV_QTY + stk.REJ_RCV_WITH_PROD_QTY;

                    //here total Adjust balance quantity
                    stk.ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);
                    stk.STLM_ID = Conversion.DBNullIntToZero(dRow["STLM_ID"]);
                    stk.STLM_NAME = dRow["STLM_NAME"].ToString();


                    stk.DEPARTMENT_NAME = prmINV.From_Dept_Name;

                    stk.COLISING_QTY = (stk.OPPENING_BAL_QTY + stk.ADJUST_QTY + stk.TOTAL_PROD_REJ_RCV + stk.REJ_TRAN_RCV_QTY) - stk.REJ_TRAN_ISS_QTY - stk.REJ_ISSUE_FROM_DEPT_QTY - stk.ISSUE_FOR_PRODUCTION - stk.ISSUE_TO_BREAKING;

                    if (stk.OPPENING_BAL_QTY == 0)
                    {
                        if (stk.TOTAL_PROD_REJ_RCV != 0 || stk.REJ_TRAN_ISS_QTY != 0 || stk.REJ_ISSUE_FROM_DEPT_QTY != 0 || stk.ISSUE_FOR_PRODUCTION != 0 || stk.COLISING_QTY != 0 || stk.REJ_TRAN_RCV_QTY !=0)
                        {
                            cRptList.Add(stk);
                        }
                    }
                    else
                    {
                        cRptList.Add(stk);
                    }
                }




            }
            //catch { throw; }
            //finally 
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }

            return cRptList;

        }

        public static List<rcMaterialStock> DepartmentRejectStockReportAcc(clsPrmInventory prmINV, DBContext dc)
        {

            List<rcMaterialStock> cRptList = new List<rcMaterialStock>();


            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();

                if (prmINV.ToDate.HasValue)
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.ToDate.Value);
                }
                cmdInfo.DBParametersInfo.Add(":p_itemGroup_id", prmINV.itemGroup_id);
                cmdInfo.DBParametersInfo.Add(":p_item_id", prmINV.item_id);
                cmdInfo.DBParametersInfo.Add(":p_itemclass_id", prmINV.ItemClassId);
                cmdInfo.DBParametersInfo.Add(":p_dept_id", prmINV.DeptID);
                cmdInfo.DBParametersInfo.Add(":p_stlm_id", prmINV.STLM_ID);

                DBQuery dbq = new DBQuery();

                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;


                cmdInfo.CommandText = "SP_DEPT_REJECT_REPORT_AC";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);


                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;

                sb.Append(" SELECT * FROM TEMP_REJECT_ITEM_STOCK ");


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
                    rcMaterialStock stk = new rcMaterialStock();
                    stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();

                    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();

                    stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();
                    //opening receive
                    stk.OP_GRID_REJ_QTY = Conversion.DBNullDecimalToZero(dRow["OP_GRID_REJ_QTY"]);
                    stk.OP_PROD_REJ_RCV_QTY = Conversion.DBNullDecimalToZero(dRow["OP_PROD_REJ_RCV_QTY"]);
                    stk.OP_REJ_TRAN_RCV_QTY = Conversion.DBNullDecimalToZero(dRow["OP_REJ_TRAN_RCV_QTY"]);
                    stk.OP_REJ_RCV_WITH_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["OP_REJ_RCV_WITH_PROD_QTY"]);
                    // opening Issue
                    stk.OP_REJ_TRAN_ISS_QTY = Conversion.DBNullDecimalToZero(dRow["OP_REJ_TRAN_ISS_QTY"]);
                    stk.OP_REJ_ISSUE_FROM_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["OP_REJ_ISSUE_FROM_DEPT_QTY"]);
                    stk.ISSUE_TO_BREAKING = Conversion.DBNullDecimalToZero(dRow["ISSUE_TO_BREAKING"]);

                    stk.OP_ISSUE_FOR_PRODUCTION = Conversion.DBNullDecimalToZero(dRow["OP_ISSUE_FOR_PRODUCTION"]);
                    stk.OP_ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ADJUST_QTY"]);

                    stk.OPPENING_BAL_QTY = (stk.OP_GRID_REJ_QTY + stk.OP_PROD_REJ_RCV_QTY + stk.OP_REJ_TRAN_RCV_QTY + stk.OP_REJ_RCV_WITH_PROD_QTY + stk.OP_ADJUST_QTY) - stk.OP_REJ_TRAN_ISS_QTY - stk.OP_REJ_ISSUE_FROM_DEPT_QTY - stk.OP_ISSUE_FOR_PRODUCTION;

                    stk.GRID_REJ_QTY = Conversion.DBNullDecimalToZero(dRow["GRID_REJ_QTY"]);
                    stk.PROD_REJ_RCV_QTY = Conversion.DBNullDecimalToZero(dRow["PROD_REJ_RCV_QTY"]);
                    stk.REJ_TRAN_RCV_QTY = Conversion.DBNullDecimalToZero(dRow["REJ_TRAN_RCV_QTY"]);
                    stk.REJ_RCV_WITH_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["REJ_RCV_WITH_PROD_QTY"]);

                    stk.REJ_TRAN_ISS_QTY = Conversion.DBNullDecimalToZero(dRow["REJ_TRAN_ISS_QTY"]);
                    stk.REJ_ISSUE_FROM_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["REJ_ISSUE_FROM_DEPT_QTY"]);

                    stk.ISSUE_FOR_PRODUCTION = Conversion.DBNullDecimalToZero(dRow["ISSUE_FOR_PRODUCTION"]);

                    stk.ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);

                    //here total IRR quantity
                    stk.TOTAL_PROD_REJ_RCV = stk.GRID_REJ_QTY + stk.PROD_REJ_RCV_QTY + stk.REJ_RCV_WITH_PROD_QTY;

                    //here total Adjust balance quantity
                    stk.ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);
                    stk.STLM_ID = Conversion.DBNullIntToZero(dRow["STLM_ID"]);
                    stk.STLM_NAME = dRow["STLM_NAME"].ToString();
                    stk.ITEM_STANDARD_WEIGHT_KG = Conversion.DBNullDecimalToZero(dRow["ITEM_STANDARD_WEIGHT_KG"]);


                    stk.DEPARTMENT_NAME = dRow["DEPARTMENT_NAME"].ToString();

                    stk.COLISING_QTY = (stk.OPPENING_BAL_QTY + stk.ADJUST_QTY + stk.TOTAL_PROD_REJ_RCV + stk.REJ_TRAN_RCV_QTY) - stk.REJ_TRAN_ISS_QTY - stk.REJ_ISSUE_FROM_DEPT_QTY - stk.ISSUE_FOR_PRODUCTION - stk.ISSUE_TO_BREAKING;
                    stk.CLOSING_QTY_wt = stk.COLISING_QTY * stk.ITEM_STANDARD_WEIGHT_KG;
                    if (stk.OPPENING_BAL_QTY == 0)
                    {
                        if (stk.TOTAL_PROD_REJ_RCV != 0 || stk.REJ_TRAN_ISS_QTY != 0 || stk.REJ_ISSUE_FROM_DEPT_QTY != 0 || stk.COLISING_QTY != 0 || stk.REJ_TRAN_RCV_QTY != 0)
                        {
                            cRptList.Add(stk);
                        }
                    }
                    else
                    {
                        cRptList.Add(stk);
                    }
                }




            }
            //catch { throw; }
            //finally 
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }

            return cRptList;

        }

        public static List<rcMaterialStock> StoreRejectStockReport(clsPrmInventory prmINV, DBContext dc)
        {

            List<rcMaterialStock> cRptList = new List<rcMaterialStock>();


            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();

                if (prmINV.ToDate.HasValue)
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.ToDate.Value);
                }
                cmdInfo.DBParametersInfo.Add(":p_itemGroup_id", prmINV.itemGroup_id);
                cmdInfo.DBParametersInfo.Add(":p_item_id", prmINV.item_id);
                cmdInfo.DBParametersInfo.Add(":p_itemclass_id", prmINV.ItemClassId);
                cmdInfo.DBParametersInfo.Add(":p_dept_id", prmINV.DeptID);
                cmdInfo.DBParametersInfo.Add(":p_store_id", prmINV.store_id);

                DBQuery dbq = new DBQuery();

                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;


                cmdInfo.CommandText = "SP_STORE_REJECT_REPORT";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);


                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;

                sb.Append(" SELECT * FROM TEMP_STORE_REJECT_ITEM_STOCK ");


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
                    rcMaterialStock stk = new rcMaterialStock();
                    stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();

                    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();

                    stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();
                    //opening receive
                    stk.OP_REJ_RCV_BY_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["OP_REJ_RCV_BY_STORE_QTY"]);
                    //stk.OP_REJ_RCV_WEIGHT = Conversion.DBNullDecimalToZero(dRow["OP_REJ_RCV_WEIGHT"]);
                    //stk.OP_REJ_TRAN_RCV_QTY = Conversion.DBNullDecimalToZero(dRow["OP_REJ_TRAN_RCV_QTY"]);
                    //stk.OP_REJ_RCV_WITH_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["OP_REJ_RCV_WITH_PROD_QTY"]);
                    // opening Issue
                    stk.OP_REJ_TRAN_ISS_QTY = Conversion.DBNullDecimalToZero(dRow["OP_REJ_TRAN_ISS_QTY"]);
                    //stk.OP_REJ_ISSUE_FROM_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["OP_REJ_ISSUE_FROM_DEPT_QTY"]);

                    //stk.OP_ISSUE_FOR_PRODUCTION = Conversion.DBNullDecimalToZero(dRow["OP_ISSUE_FOR_PRODUCTION"]);
                    stk.OP_ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ADJUST_QTY"]);

                    stk.OPPENING_BAL_QTY = (stk.OP_REJ_RCV_BY_STORE_QTY) - stk.OP_REJ_TRAN_ISS_QTY + stk.OP_ADJUST_QTY;

                    stk.REJ_RCV_BY_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["REJ_RCV_BY_STORE_QTY"]);
                    //stk.REJ_RCV_WEIGHT = Conversion.DBNullDecimalToZero(dRow["REJ_RCV_WEIGHT"]);
                    //stk.REJ_TRAN_RCV_QTY = Conversion.DBNullDecimalToZero(dRow["REJ_TRAN_RCV_QTY"]);
                    //stk.REJ_RCV_WITH_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["REJ_RCV_WITH_PROD_QTY"]);

                    stk.REJ_TRAN_ISS_QTY = Conversion.DBNullDecimalToZero(dRow["REJ_TRAN_ISS_QTY"]);
                    //stk.REJ_ISSUE_FROM_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["REJ_ISSUE_FROM_DEPT_QTY"]);

                    //stk.ISSUE_FOR_PRODUCTION = Conversion.DBNullDecimalToZero(dRow["ISSUE_FOR_PRODUCTION"]);

                    //stk.ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);
                    //here total IRR quantity
                    stk.TOTAL_PROD_REJ_RCV = stk.REJ_RCV_BY_STORE_QTY;

                    //here total Adjust balance quantity
                    stk.ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);
                    stk.STLM_NAME = dRow["STLM_NAME"].ToString();


                    stk.DEPARTMENT_NAME = prmINV.From_Dept_Name;

                    stk.COLISING_QTY = (stk.OPPENING_BAL_QTY + stk.ADJUST_QTY + stk.TOTAL_PROD_REJ_RCV) - stk.REJ_TRAN_ISS_QTY ;

                    if (stk.OPPENING_BAL_QTY == 0)
                    {
                        if (stk.TOTAL_PROD_REJ_RCV != 0 || stk.COLISING_QTY != 0)
                        {
                            cRptList.Add(stk);
                        }
                    }
                    else
                    {
                        cRptList.Add(stk);
                    }
                }




            }
            //catch { throw; }
            //finally 
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }

            return cRptList;

        }

        public static List<rcMaterialStock> GetPPBatchWiseStockReport(clsPrmInventory prmINV, DBContext dc)
        {

            List<rcMaterialStock> cRptList = new List<rcMaterialStock>();


            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();

                if (prmINV.ToDate.HasValue)
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.ToDate.Value);
                }
                cmdInfo.DBParametersInfo.Add(":p_itemGroup_id", prmINV.itemGroup_id);
                cmdInfo.DBParametersInfo.Add(":p_item_id", prmINV.item_id);
                cmdInfo.DBParametersInfo.Add(":p_itemclass_id", prmINV.ItemClassId);
                cmdInfo.DBParametersInfo.Add(":p_StorageLocationId", prmINV.StorageLocationId);
                cmdInfo.DBParametersInfo.Add(":p_dept_id", prmINV.DeptID);
                cmdInfo.DBParametersInfo.Add(":p_batt_type_id", prmINV.Battery_Type_ID);
                cmdInfo.DBParametersInfo.Add(":p_is_repair", prmINV.Is_Repair);


                DBQuery dbq = new DBQuery();

                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                //dbq.CommandTimeout = 1000;
                cmdInfo.CommandTimeout = 600;


                cmdInfo.CommandText = "SP_PP_BATCH_STOCK_REPORT";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);


                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;

                sb.Append(" SELECT IG.ITEM_GROUP_ID,IG.ITEM_GROUP_NAME,IM.ITEM_NAME,UI.UOM_ID,UI.UOM_CODE UOM_NAME,STLM.NAME STORAGE_LOCATION_NAME ");
                sb.Append(" ,STK.ITEM_ID,STK.STLM_ID,STK.DEPARTMENT_ID,STK.PROD_BATCH_NO,STK.RCV_QTY,STK.PROCESS_LOSS_QTY,IRR_DEPT_QTY,ITC_DEPT_QTY ");
                sb.Append(" ,STK.ISSUE_QTY,STK.OP_RCV_QTY,STK.OP_ISSUE_QTY,STK.PROD_QTY,STK.ADJUST_QTY,STK.OP_ADJUST_QTY,STK.PROD_REJECT_QTY,STK.OP_PROD_REJECT_QTY,STK.GRID_REJECT_QTY,STK.OP_GRID_REJECT_QTY ");
                sb.Append(" FROM TEMP_PP_BATCH_ITEM_STOCK STK ");
                sb.Append(" INNER JOIN INV_ITEM_MASTER IM ON STK.ITEM_ID=IM.ITEM_ID ");
                sb.Append(" INNER JOIN INV_ITEM_GROUP IG ON IM.ITEM_GROUP_ID=IG.ITEM_GROUP_ID ");
                sb.Append(" INNER JOIN UOM_INFO UI ON IM.UOM_ID=UI.UOM_ID ");
                sb.Append(" INNER JOIN STORAGE_LOCATION_MST STLM ON STK.STLM_ID=STLM.STLM_ID ");
                sb.Append(" WHERE 1=1 ");

                if(prmINV.itemGroup_id > 0)
                {
                    sb.Append(" AND IG.ITEM_GROUP_ID='"+prmINV.itemGroup_id+"' ");
                }

                sb.Append("  ORDER BY SUBSTR(STK.PROD_BATCH_NO,1,5), CASE WHEN SUBSTR( STK.PROD_BATCH_NO,6)='M' THEN 1 WHEN  SUBSTR( STK.PROD_BATCH_NO,6)='E' THEN 2 WHEN  SUBSTR( STK.PROD_BATCH_NO,6)='N' THEN 3 END ");
                DBQuery dbqtemp = new DBQuery();

                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);


                foreach (DataRow dRow in dtData.Rows)
                {
                    rcMaterialStock stk = new rcMaterialStock();
                    stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();
                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();
                    stk.STORAGE_LOCATION_NAME = dRow["STORAGE_LOCATION_NAME"].ToString();
                    stk.PROD_BATCH_NO = dRow["PROD_BATCH_NO"].ToString();

                    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();

                    stk.OP_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_RCV_QTY"]);
                    stk.OP_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ISSUE_QTY"]);
                    stk.OP_ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ADJUST_QTY"]);
                    stk.OP_REJECT_QTY = Conversion.DBNullDecimalToZero(dRow["OP_PROD_REJECT_QTY"]);
                    stk.OP_GRID_REJECT_QTY = Conversion.DBNullDecimalToZero(dRow["OP_GRID_REJECT_QTY"]);

                    stk.OPPENING_BAL_QTY = (stk.OP_IRR_QTY + stk.OP_ADJUST_QTY) - stk.OP_ITC_QTY - stk.OP_REJECT_QTY - stk.OP_GRID_REJECT_QTY;

                    stk.IRR_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["RCV_QTY"]);
                    stk.ITC_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["ISSUE_QTY"]);

                    stk.IRR_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_DEPT_QTY"]);
                    stk.ITC_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_DEPT_QTY"]);

                    stk.ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);

                    stk.REJECT_QTY = Conversion.DBNullDecimalToZero(dRow["PROD_REJECT_QTY"]);
                    stk.GRID_REJECT_QTY = Conversion.DBNullDecimalToZero(dRow["GRID_REJECT_QTY"]);

                    stk.PROCESS_LOSS_QTY = Conversion.DBNullDecimalToZero(dRow["PROCESS_LOSS_QTY"]);

                    stk.COLISING_QTY = (stk.OPPENING_BAL_QTY + stk.ADJUST_QTY + stk.IRR_PROD_QTY + stk.IRR_DEPT_QTY) - stk.ITC_PROD_QTY - stk.ITC_DEPT_QTY - stk.REJECT_QTY - stk.GRID_REJECT_QTY;

                    if (stk.GRID_REJECT_QTY > 0 && stk.REJECT_QTY == 0)
                    {
                        stk.REJECT_QTY = stk.GRID_REJECT_QTY;

                    }

                    //cRptList.Add(stk);

                    if (stk.OPPENING_BAL_QTY == 0)
                    {
                        if (stk.IRR_PROD_QTY != 0 || stk.IRR_DEPT_QTY != 0 || stk.ITC_PROD_QTY != 0 || stk.ITC_DEPT_QTY != 0 || stk.ADJUST_QTY != 0 || stk.COLISING_QTY != 0)
                        {
                            cRptList.Add(stk);
                        }
                    }
                    else
                    {
                        cRptList.Add(stk);
                    }
                }




            }
            //catch { throw; }
            //finally 
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }

            return cRptList;

        }

        public static List<rcMaterialStock> GetPPBatchWiseStockAgingReport(clsPrmInventory prmINV, DBContext dc)
        {

            List<rcMaterialStock> cRptList = new List<rcMaterialStock>();


            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();

                if (prmINV.ToDate.HasValue)
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.ToDate.Value);
                }
                cmdInfo.DBParametersInfo.Add(":p_itemGroup_id", prmINV.itemGroup_id);
                cmdInfo.DBParametersInfo.Add(":p_item_id", prmINV.item_id);
                cmdInfo.DBParametersInfo.Add(":p_itemclass_id", prmINV.ItemClassId);
                cmdInfo.DBParametersInfo.Add(":p_StorageLocationId", prmINV.StorageLocationId);
                cmdInfo.DBParametersInfo.Add(":p_dept_id", prmINV.DeptID);


                DBQuery dbq = new DBQuery();

                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                //dbq.CommandTimeout = 1000;
                cmdInfo.CommandTimeout = 600;


                cmdInfo.CommandText = "SP_BATCH_AGING_STOCK_REPORT";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);


                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;

                sb.Append(" SELECT  d.DEPARTMENT_NAME,st.NAME STORAGE_LOCATION,STK.STLM_ID,STK.TRANS_REF_NO,im.ITEM_CODE,im.ITEM_NAME,STK.PROD_BATCH_NO ");
                sb.Append(" ,STK.TRANS_DATE PRODUCTION_DATE ,NVL(ROUND(SYSDATE-STK.TRANS_DATE),0) TOTAL_DAYS ");
                sb.Append(" ,NVL((STK.OP_RCV_QTY+STK.RCV_QTY+STK.ADJUST_QTY+STK.OP_ADJUST_QTY)-(STK.ISSUE_QTY+STK.OP_ISSUE_QTY+STK.PROD_REJECT_QTY+STK.OP_PROD_REJECT_QTY+STK.GRID_REJECT_QTY+STK.OP_GRID_REJECT_QTY+NVL(STK.ITC_DEPT_QTY,0)),0)  CLOSING_QTY,uo.UOM_CODE  ");
                sb.Append(" FROM TEMP_PP_BATCH_ITEM_STOCK STK  ");
                sb.Append(" INNER JOIN INV_ITEM_MASTER im ON STK.ITEM_ID=im.ITEM_ID ");
                sb.Append(" INNER JOIN DEPARTMENT_INFO d ON STK.DEPARTMENT_ID=d.DEPARTMENT_ID ");
                sb.Append(" INNER JOIN STORAGE_LOCATION_MST st ON STK.STLM_ID=st.STLM_ID ");
                sb.Append(" INNER JOIN UOM_INFO uo ON im.UOM_ID=uo.UOM_ID ");
                sb.Append(" WHERE 1=1 ");

                if (prmINV.itemGroup_id > 0)
                {
                    sb.Append(" AND IG.ITEM_GROUP_ID='" + prmINV.itemGroup_id + "' ");
                }

                sb.Append("  AND  ((STK.OP_RCV_QTY+STK.RCV_QTY+STK.ADJUST_QTY+STK.OP_ADJUST_QTY)-(STK.ISSUE_QTY+STK.OP_ISSUE_QTY+STK.PROD_REJECT_QTY+STK.OP_PROD_REJECT_QTY+STK.GRID_REJECT_QTY+STK.OP_GRID_REJECT_QTY+NVL(STK.ITC_DEPT_QTY,0)))>0 ");
                sb.Append("  ORDER BY d.DEPARTMENT_NAME,st.NAME,im.ITEM_NAME,SUBSTR(STK.PROD_BATCH_NO,1,5), CASE WHEN SUBSTR( STK.PROD_BATCH_NO,6)='M' THEN 1 WHEN  SUBSTR( STK.PROD_BATCH_NO,6)='E' THEN 2 WHEN  SUBSTR( STK.PROD_BATCH_NO,6)='N' THEN 3 END ");
                DBQuery dbqtemp = new DBQuery();

                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);


                foreach (DataRow dRow in dtData.Rows)
                {
                    rcMaterialStock stk = new rcMaterialStock();
                    stk.DEPARTMENT_NAME = dRow["DEPARTMENT_NAME"].ToString();
                    stk.STORAGE_LOCATION_NAME = dRow["STORAGE_LOCATION"].ToString();
                    stk.ITEM_CODE = dRow["ITEM_CODE"].ToString();
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();
                    stk.PROD_BATCH_NO = dRow["PROD_BATCH_NO"].ToString();
                    if (dRow["PRODUCTION_DATE"] != null)
                    {
                        stk.TRANS_DATE = Conversion.DBNullDateToNull(dRow["PRODUCTION_DATE"]);
                    }
                    stk.UOM_NAME = dRow["UOM_CODE"].ToString();

                    stk.TOTAL_DAYS = Conversion.DBNullDecimalToZero(dRow["TOTAL_DAYS"]);
                    stk.CLOSING_QTY = Conversion.DBNullDecimalToZero(dRow["CLOSING_QTY"]);
                    

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

        //Get Excel Report
        public static Byte[] Get_BatchwiseStockAging_ExcelData(clsPrmInventory prmINV, bool pExecuteSP)
        {
            return Get_BatchwiseStockAging_ExcelData(prmINV, pExecuteSP, null);
        }

        public static Byte[] Get_BatchwiseStockAging_ExcelData(clsPrmInventory prmINV, bool pExecuteSP, DBContext dc)
        {

            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();

                if (prmINV.ToDate.HasValue)
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.ToDate.Value);
                }
                cmdInfo.DBParametersInfo.Add(":p_itemGroup_id", prmINV.itemGroup_id);
                cmdInfo.DBParametersInfo.Add(":p_item_id", prmINV.item_id);
                cmdInfo.DBParametersInfo.Add(":p_itemclass_id", prmINV.ItemClassId);
                cmdInfo.DBParametersInfo.Add(":p_StorageLocationId", prmINV.StorageLocationId);
                cmdInfo.DBParametersInfo.Add(":p_dept_id", prmINV.DeptID);


                DBQuery dbq = new DBQuery();

                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                //dbq.CommandTimeout = 1000;
                cmdInfo.CommandTimeout = 600;


                cmdInfo.CommandText = "SP_BATCH_AGING_STOCK_REPORT";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);







                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;

                sb.Append(" SELECT  d.DEPARTMENT_NAME,st.NAME STORAGE_LOCATION,STK.STLM_ID,STK.TRANS_REF_NO,im.ITEM_CODE,im.ITEM_NAME,STK.PROD_BATCH_NO ");
                sb.Append(" ,STK.TRANS_DATE PRODUCTION_DATE ,NVL(ROUND(SYSDATE-STK.TRANS_DATE),0) TOTAL_DAYS ");
                sb.Append(" ,NVL((STK.OP_RCV_QTY+STK.RCV_QTY+STK.ADJUST_QTY+STK.OP_ADJUST_QTY)-(STK.ISSUE_QTY+STK.OP_ISSUE_QTY+STK.PROD_REJECT_QTY+STK.OP_PROD_REJECT_QTY+STK.GRID_REJECT_QTY+STK.OP_GRID_REJECT_QTY+NVL(STK.ITC_DEPT_QTY,0)),0)  CLOSING_QTY,uo.UOM_CODE  ");
                sb.Append(" FROM TEMP_PP_BATCH_ITEM_STOCK STK  ");
                sb.Append(" INNER JOIN INV_ITEM_MASTER im ON STK.ITEM_ID=im.ITEM_ID ");
                sb.Append(" INNER JOIN DEPARTMENT_INFO d ON STK.DEPARTMENT_ID=d.DEPARTMENT_ID ");
                sb.Append(" INNER JOIN STORAGE_LOCATION_MST st ON STK.STLM_ID=st.STLM_ID ");
                sb.Append(" INNER JOIN UOM_INFO uo ON im.UOM_ID=uo.UOM_ID ");
                sb.Append(" WHERE 1=1 ");

                if (prmINV.itemGroup_id > 0)
                {
                    sb.Append(" AND IG.ITEM_GROUP_ID='" + prmINV.itemGroup_id + "' ");
                }

                sb.Append("  AND  ((STK.OP_RCV_QTY+STK.RCV_QTY+STK.ADJUST_QTY+STK.OP_ADJUST_QTY)-(STK.ISSUE_QTY+STK.OP_ISSUE_QTY+STK.PROD_REJECT_QTY+STK.OP_PROD_REJECT_QTY+STK.GRID_REJECT_QTY+STK.OP_GRID_REJECT_QTY+NVL(STK.ITC_DEPT_QTY,0)))>0 ");
                sb.Append("  ORDER BY d.DEPARTMENT_NAME,st.NAME,im.ITEM_NAME,SUBSTR(STK.PROD_BATCH_NO,1,5), CASE WHEN SUBSTR( STK.PROD_BATCH_NO,6)='M' THEN 1 WHEN  SUBSTR( STK.PROD_BATCH_NO,6)='E' THEN 2 WHEN  SUBSTR( STK.PROD_BATCH_NO,6)='N' THEN 3 END ");
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
                    workSheet.Cells[1, colNum].Value = "Department";

                    colNum = 3;
                    workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[1, colNum].Value = "Storage Location";

                   

                    colNum = 4;
                    workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[1, colNum].Value = "Item Code";

                    colNum = 5;
                    workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[1, colNum].Value = "Item Name";

                    colNum = 6;
                    workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[1, colNum].Value = "UOM";

                    colNum = 7;
                    workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[1, colNum].Value = "Batch No";

                    colNum = 8;
                    workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[1, colNum].Value = "Production Date";

                    colNum = 9;
                    workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[1, colNum].Value = "Total Days";
                    colNum = 10;
                    workSheet.Cells[1, colNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[1, colNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[1, colNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[1, colNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[1, colNum].Value = "Closing Quantity";



                    //Body of table  

                    int recordIndex = 2;
                    int slno = 0;
                    foreach (DataRow dRow in dtData.Rows)
                    {
                        slno++;
                        workSheet.Cells[recordIndex, 1].Value = slno;
                        workSheet.Cells[recordIndex, 2].Value = dRow["DEPARTMENT_NAME"].ToString();
                        workSheet.Cells[recordIndex, 3].Value = dRow["STORAGE_LOCATION"].ToString();
                        
                        workSheet.Cells[recordIndex, 4].Value = dRow["ITEM_CODE"].ToString();
                        workSheet.Cells[recordIndex, 5].Value = dRow["ITEM_NAME"].ToString();
                        workSheet.Cells[recordIndex, 6].Value = dRow["UOM_CODE"].ToString();

                        workSheet.Cells[recordIndex, 7].Value = dRow["PROD_BATCH_NO"].ToString();
                        if (dRow["PRODUCTION_DATE"].ToString() != string.Empty)
                        {
                            workSheet.Cells[recordIndex, 8].Value = Convert.ToDateTime(dRow["PRODUCTION_DATE"]).ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            workSheet.Cells[recordIndex, 8].Value = string.Empty;
                        }
                        workSheet.Cells[recordIndex, 9].Value = Conversion.DBNullDecimalToZero(dRow["TOTAL_DAYS"]);
                        workSheet.Cells[recordIndex, 10].Value = Conversion.DBNullDecimalToZero(dRow["CLOSING_QTY"]);








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
                    




                    bytes = excel.GetAsByteArray();

                }

                return bytes;

            }
        }

        public static List<rcMaterialStock> GetItemStockReportfordx(clsPrmInventory prmINV, DBContext dc)
        {

            List<rcMaterialStock> cRptList = new List<rcMaterialStock>();


            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();
                cmdInfo.DBParametersInfo.Clear();

                if (prmINV.ToDate.HasValue)
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.ToDate.Value);
                }
                cmdInfo.DBParametersInfo.Add(":p_itemtype_id", prmINV.itemtype_id);
                cmdInfo.DBParametersInfo.Add(":p_itemGroup_id", prmINV.itemGroup_id);
                cmdInfo.DBParametersInfo.Add(":p_item_id", prmINV.item_id);
                cmdInfo.DBParametersInfo.Add(":p_store_id", prmINV.store_id);
                cmdInfo.DBParametersInfo.Add(":p_itemclass_id", prmINV.ItemClassId);
                cmdInfo.DBParametersInfo.Add(":p_battery_type_id", prmINV.Battery_Type_ID);
                cmdInfo.DBParametersInfo.Add(":p_is_repair", prmINV.Is_Repair);

                DBQuery dbq = new DBQuery();

                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                //dbq.CommandTimeout = 1000;
                cmdInfo.CommandTimeout = 600;
                cmdInfo.CommandText = "SP_STORE_ITEM_STOCK";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);


                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;
                //sb.Append(" SELECT * FROM TEMP_ITEM_STOCK_BAT_FOR_NET ");

                sb.Append(" Select ITEM_GROUP_ID,ITEM_GROUP_NAME,ITEM_ID,ITEM_NAME,UOM_ID,UOM_NAME,ITEM_TYPE_ID,ITEM_TYPE_NAME,ITEM_CLASS_ID,ITEM_CLASS_NAME,OP_BAL+OP_MRR_QTY+OP_IRR_QTY-OP_ITC_QTY-OP_OUTSALES_QTY-OP_ROTARY_QTY-OP_LOANRP_QTY+OP_RTN_QTY+OP_ADJ OPPENING_BAL_QTY,MRR_QTY,IRR_QTY,ITC_QTY,OUTSALES_QTY,ROTARY_QTY,LOANRP_QTY,RTN_QTY, OP_BAL+OP_MRR_QTY+OP_IRR_QTY-OP_ITC_QTY-OP_OUTSALES_QTY-OP_ROTARY_QTY-OP_LOANRP_QTY+OP_RTN_QTY+OP_ADJ+MRR_QTY+IRR_QTY-ITC_QTY-OUTSALES_QTY-ROTARY_QTY-LOANRP_QTY+RTN_QTY+ADJUST_QTY COLISING_QTY, BAL_QTY,ADJUST_QTY,ITEM_CODE FROM TEMP_STORE_ITEM_STOCK ORDER BY ITEM_GROUP_NAME,ITEM_NAME ASC");


                DBQuery dbqtemp = new DBQuery();
                //DBQuery dbq = new DBQuery();


                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                //DBQuery.ExecuteDBQuerySP(dbq, dc);
                DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);

                //,,,,,,,,,,,,,,,,,


                foreach (DataRow dRow in dtData.Rows)
                {
                    rcMaterialStock stk = new rcMaterialStock();

                    stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);

                    stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_CODE = dRow["ITEM_CODE"].ToString();
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();

                    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);

                    stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                    stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();

                    stk.OPPENING_BAL_QTY = Conversion.DBNullDecimalToZero(dRow["OPPENING_BAL_QTY"]);
                    stk.MRR_QTY = Conversion.DBNullDecimalToZero(dRow["MRR_QTY"]);
                    stk.IRR_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_QTY"]);

                    stk.ITC_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_QTY"]);
                    stk.OUTSALES_QTY = Conversion.DBNullDecimalToZero(dRow["OUTSALES_QTY"]);

                    stk.ROTARY_QTY = Conversion.DBNullDecimalToZero(dRow["ROTARY_QTY"]);
                    stk.LOANRP_QTY = Conversion.DBNullDecimalToZero(dRow["LOANRP_QTY"]);
                    stk.RTN_QTY = Conversion.DBNullDecimalToZero(dRow["RTN_QTY"]);
                    stk.BAL_QTY = Conversion.DBNullDecimalToZero(dRow["BAL_QTY"]);
                    stk.COLISING_QTY = Conversion.DBNullDecimalToZero(dRow["COLISING_QTY"]);
                    stk.ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);
                    stk.TOT_RECEIVE_QNTY = Conversion.DBNullDecimalToZero(dRow["MRR_QTY"]) + Conversion.DBNullDecimalToZero(dRow["IRR_QTY"]) + Conversion.DBNullDecimalToZero(dRow["RTN_QTY"]);
                    stk.OPWITHTOTRCV_QTY = Conversion.DBNullDecimalToZero(dRow["OPPENING_BAL_QTY"]) + Conversion.DBNullDecimalToZero(dRow["MRR_QTY"]) + Conversion.DBNullDecimalToZero(dRow["IRR_QTY"]) + Conversion.DBNullDecimalToZero(dRow["RTN_QTY"]);
                    stk.TOT_ISSUE_QNTY = Conversion.DBNullDecimalToZero(dRow["ITC_QTY"]) + Conversion.DBNullDecimalToZero(dRow["OUTSALES_QTY"]) + Conversion.DBNullDecimalToZero(dRow["ROTARY_QTY"]) + Conversion.DBNullDecimalToZero(dRow["LOANRP_QTY"]);

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


        //commit;


        public static List<rcMaterialStock> GetItemStockMonitor(clsPrmInventory prmINV)
        {
            return GetItemStockMonitor(prmINV, null);
        }
        public static List<rcMaterialStock> GetItemStockMonitor(clsPrmInventory prmINV, DBContext dc)
        {

            List<rcMaterialStock> cRptList = new List<rcMaterialStock>();


            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();


                if (prmINV.ToDate.HasValue)
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.ToDate.Value);
                }

                cmdInfo.DBParametersInfo.Add(":p_itemtype_id", prmINV.itemtype_id);
                cmdInfo.DBParametersInfo.Add(":p_itemGroup_id", prmINV.itemGroup_id);
                cmdInfo.DBParametersInfo.Add(":p_item_id", prmINV.item_id);
                cmdInfo.DBParametersInfo.Add(":p_store_id", prmINV.store_id);
                cmdInfo.DBParametersInfo.Add(":p_itemclass_id", prmINV.ItemClassId);
                cmdInfo.DBParametersInfo.Add(":p_battery_type_id", prmINV.Battery_Type_ID);
                cmdInfo.DBParametersInfo.Add(":p_is_repair", prmINV.Is_Repair);
                cmdInfo.DBParametersInfo.Add(":p_item_sns_id", prmINV.SNS_Type);
              
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;              
                cmdInfo.CommandTimeout = 600;
                cmdInfo.CommandText = "SP_STORE_ITEM_STOCK";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);


                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;              
                sb.Append(" Select a.ITEM_GROUP_ID,a.ITEM_GROUP_NAME,a.ITEM_ID,a.ITEM_NAME,a.UOM_ID,a.UOM_NAME ");
                sb.Append(" ,a.ITEM_TYPE_ID,a.ITEM_TYPE_NAME,a.ITEM_CLASS_ID,a.ITEM_CLASS_NAME ");
                sb.Append(" ,a.OP_BAL+a.OP_MRR_QTY+a.OP_IRR_QTY-a.OP_ITC_QTY-a.OP_OUTSALES_QTY-a.OP_ROTARY_QTY-a.OP_LOANRP_QTY+a.OP_RTN_QTY+a.OP_ADJ OPPENING_BAL_QTY,a.MRR_QTY+a.IRR_QTY+a.RTN_QTY TOT_RECEIVE_QNTY ");
                sb.Append(" ,a.ITC_QTY+a.OUTSALES_QTY+a.ROTARY_QTY+a.LOANRP_QTY TOT_ISSUE_QNTY, a.OP_BAL+a.OP_MRR_QTY+a.OP_IRR_QTY-a.OP_ITC_QTY-a.OP_OUTSALES_QTY-a.OP_ROTARY_QTY-a.OP_LOANRP_QTY+a.OP_RTN_QTY+a.OP_ADJ+a.MRR_QTY+a.IRR_QTY-a.ITC_QTY-a.OUTSALES_QTY-a.ROTARY_QTY-a.LOANRP_QTY+RTN_QTY COLISING_QTY,NVL(a.SAFTY_STOCK_LEVEL,0) SAFTY_STOCK_LEVEL,a.ITC_QTY ");
                sb.Append(" , a.BAL_QTY,NVL(a.RE_ORDER_LEVEL,0) RE_ORDER_LEVEL,NVL(b.ITEM_FC_QTY,0) CUR_MONTH_FORCAST, sns.ITEM_SNS_NAME FROM TEMP_STORE_ITEM_STOCK a ");
                sb.Append(" LEFT JOIN INV_ITEM_SNS_MST sns ON a.ITEM_SNS_ID=sns.ITEM_SNS_ID ");
                sb.Append(" LEFT JOIN PROD_TBLFORECAST_DTL b ON a.ITEM_ID=b.ITEM_ID ");

                DBQuery dbqtemp = new DBQuery();              
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                //DBQuery.ExecuteDBQuerySP(dbq, dc);
                DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);

                //,,,,,,,,,,,,,,,,,


                foreach (DataRow dRow in dtData.Rows)
                {
                    rcMaterialStock stk = new rcMaterialStock();

                    stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);

                    stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();


                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();

                    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);

                    stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                    stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();

                    stk.OPPENING_BAL_QTY = Conversion.DBNullDecimalToZero(dRow["OPPENING_BAL_QTY"]);
                    stk.TOT_RECEIVE_QNTY = Conversion.DBNullDecimalToZero(dRow["TOT_RECEIVE_QNTY"]);
                    stk.TOT_ISSUE_QNTY = Conversion.DBNullDecimalToZero(dRow["TOT_ISSUE_QNTY"]);

                    stk.ITC_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_QTY"]);
                    stk.CUR_MONTH_FORCAST = Conversion.DBNullDecimalToZero(dRow["CUR_MONTH_FORCAST"]);

                    stk.SAFTY_STOCK_LEVEL = Conversion.DBNullDecimalToZero(dRow["SAFTY_STOCK_LEVEL"]);
                    //stk.LOANRP_QTY = Conversion.DBNullIntToZero(dRow["LOANRP_QTY"]);
                    // stk.RTN_QTY = Conversion.DBNullIntToZero(dRow["RTN_QTY"]);
                    // stk.BAL_QTY = Conversion.DBNullIntToZero(dRow["BAL_QTY"]);
                    stk.COLISING_QTY = Conversion.DBNullDecimalToZero(dRow["COLISING_QTY"]);
                    stk.ITEM_SNS_NAME = dRow["ITEM_SNS_NAME"].ToString();
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

        #region Master_Item_Report

        public static List<rcMaterialStock> Master_Item_Report(clsPrmInventory prmINV, DBContext dc)
        {
            List<rcMaterialStock> cObjList = new List<rcMaterialStock>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);             
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetItem_Master_SQLString());
                //if (prmINV.itemGroup_id > 0)
                //{
                //    //sb.Append(" AND lcMst.LC_DATE <='" + rptClass.ToDatet + "'");
                //}    
                if (prmINV.reorder_level=="1")
                sb.Append(" AND INV_ITEM_MASTER.RE_ORDER_LEVEL>0 ");

                if(prmINV.item_id>0)
                {
                    sb.Append(" AND INV_ITEM_MASTER.item_id=@pitem_id ");
                    cmdInfo.DBParametersInfo.Add("@pitem_id", prmINV.item_id);

                }
                if(prmINV.itemGroup_id>0)
                {
                    sb.Append(" AND INV_ITEM_MASTER.ITEM_GROUP_ID=@pITEM_GROUP_ID ");
                    cmdInfo.DBParametersInfo.Add("@pITEM_GROUP_ID", prmINV.itemGroup_id);
                }

                if (prmINV.itemtype_id > 0)
                {
                    sb.Append(" AND INV_ITEM_TYPE.ITEM_TYPE_ID = @pITEM_TYPE_ID ");
                    cmdInfo.DBParametersInfo.Add("@pITEM_TYPE_ID", prmINV.itemtype_id);
                }

                if (prmINV.ItemClassId > 0)
                {
                    sb.Append(" AND INV_ITEM_MASTER.ITEM_CLASS_ID = @pITEM_CLASS_ID ");
                    cmdInfo.DBParametersInfo.Add("@pITEM_CLASS_ID", prmINV.ItemClassId);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                cObjList = DBQuery.ExecuteDBQuery<rcMaterialStock>(dbq, dc).ToList();

                if (prmINV.StockLevel == "BRL")
                {

                    cObjList = cObjList.Where(x => x.CLOSING_QTY < x.RE_ORDER_LEVEL).ToList();
                }
                if (prmINV.StockLevel == "BSS")
                {
                    cObjList = cObjList.Where(x => x.CLOSING_QTY < x.SAFTY_STOCK_LEVEL).ToList();
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
     

        #endregion

        #region Oxide Stock Report Summary

        public static List<rcFormationProductionSummary> Oxide_Production_Summary_Report(clsPrmInventory prmINV)
        {
            return Oxide_Production_Summary_Report(prmINV, null);
        }

        public static List<rcFormationProductionSummary> Oxide_Production_Summary_Report(clsPrmInventory prmINV, DBContext dc)
        {

            List<rcFormationProductionSummary> cRptList = new List<rcFormationProductionSummary>();


            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();

                if (prmINV.toProdDate != "")
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.fromProdDate);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.toProdDate);
                }
                cmdInfo.DBParametersInfo.Add(":p_itemtype_id", prmINV.itemtype_id);
                cmdInfo.DBParametersInfo.Add(":p_itemGroup_id", prmINV.itemGroup_id);
                cmdInfo.DBParametersInfo.Add(":p_item_id", prmINV.item_id);
                cmdInfo.DBParametersInfo.Add(":p_store_id", prmINV.store_id);
                cmdInfo.DBParametersInfo.Add(":p_itemclass_id", prmINV.ItemClassId);
                cmdInfo.DBParametersInfo.Add(":p_battery_type_id", prmINV.Battery_Type_ID);
                cmdInfo.DBParametersInfo.Add(":p_is_repair", prmINV.Is_Repair);
                cmdInfo.DBParametersInfo.Add(":p_dept_id", prmINV.From_Dept_Id);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;
                cmdInfo.CommandText = "SP_OXIDE_PROD_SUMMARY";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);


                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;
                sb.Append(" Select  * FROM TEMP_OXIDE_ITEM_STOCK");
                //sb.Append(" Select ITEM_GROUP_ID,ITEM_GROUP_NAME,ITEM_ID,ITEM_NAME,UOM_ID,UOM_NAME,ITEM_TYPE_ID,ITEM_TYPE_NAME,ITEM_CLASS_ID,ITEM_CLASS_NAME,OP_F_IRR_QTY,OP_ADJUST_QTY,OP_IRR_PROD_QTY,OP_F_ITC_QTY,OP_UF_IRR_QTY,OP_UF_ITC_QTY,IRR_DEPT_QTY,IRR_STORE_QTY,IRR_PROD_QTY,ITC_DEPT_QTY,ITC_STORE_QTY,ITC_PROD_QTY,ADJUST_QTY,RCV_FROM_IB,RCV_FROM_PASTING,REJECT_QTY,CURR_ISS_PAST,CURR_ISS_IB,CURR_ISS_RED_OXID,CURR_ISS_ST,CURR_ISS_OTHERS,IRR_BAL_QTY,ITC_BAL_QTY,PRODUCTION_CAPACITY,DATEDIFF,STOCK_TRANS_ITEM_TO_ITEM,STOCK_TRANS_ITEM_TO_ITEM_ISSUE FROM TEMP_OXIDE_ITEM_STOCK");


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
                    stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();

                    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);

                    stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                    stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();


                    //here IRR including positive adjustment
                    stk.OP_IRR_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["OP_IRR_PROD_QTY"]);
                    //here ITC including Negative adjustment
                    stk.OP_F_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_F_ITC_QTY"]);
                    //stk.OP_ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ADJUST_QTY"]);
                    stk.OP_F_BAL = stk.OP_IRR_PROD_QTY - stk.OP_F_ITC_QTY;


                    //All issue Distribution Breakdown

                    stk.CURR_ISS_PAST = Conversion.DBNullDecimalToZero(dRow["CURR_ISS_PAST"]);
                    stk.CURR_ISS_RED_OXID = Conversion.DBNullDecimalToZero(dRow["CURR_ISS_RED_OXID"]);
                    stk.CURR_ISS_IB = Conversion.DBNullDecimalToZero(dRow["CURR_ISS_IB"]);

                    stk.CURR_ISS_ST = Conversion.DBNullDecimalToZero(dRow["CURR_ISS_ST"]);
                    stk.CURR_ISS_OTHERS = Conversion.DBNullDecimalToZero(dRow["CURR_ISS_OTHERS"]);






                    stk.IRR_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_PROD_QTY"]);
                    stk.IRR_OTHER_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_OTHER_QTY"]);
                    //here total IRR quantity without Adjust within date range
                    stk.IRR_BAL_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_BAL_QTY"]);
                    //here total ITC quantity without Adjust within date range
                    stk.ITC_BAL_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_BAL_QTY"]);
                    stk.ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);
                    stk.OXIDE_TOTAL_OUTSALE = stk.CURR_ISS_PAST + stk.CURR_ISS_RED_OXID + stk.CURR_ISS_IB + stk.CURR_ISS_ST;

                    // Stock Transfer Receiv ---issue
                    stk.STOCK_TRANS_ITEM_TO_ITEM = Conversion.DBNullDecimalToZero(dRow["STOCK_TRANS_ITEM_TO_ITEM"]);
                    stk.STOCK_TRANS_ITEM_TO_ITEM_ISSUE = Conversion.DBNullDecimalToZero(dRow["STOCK_TRANS_ITEM_TO_ITEM_ISSUE"]);
                    stk.ITC_BAL_QTY = stk.CURR_ISS_PAST + stk.CURR_ISS_RED_OXID + stk.CURR_ISS_IB + stk.CURR_ISS_ST + stk.CURR_ISS_OTHERS  + stk.STOCK_TRANS_ITEM_TO_ITEM_ISSUE;
                    //+ stk.ADJUST_QTY

                    //this is previous Running Closing Quantity
                    //stk.COLISING_QTY = stk.OP_F_BAL + stk.IRR_BAL_QTY + stk.STOCK_TRANS_ITEM_TO_ITEM + stk.ADJUST_QTY - stk.ITC_BAL_QTY;

                    //This is new Closing
                    stk.TOTAL_RECEIVE = stk.IRR_BAL_QTY + stk.OP_F_BAL + stk.STOCK_TRANS_ITEM_TO_ITEM;
                    stk.TOTAL_ISSUE = stk.ITC_BAL_QTY;
                    stk.COLISING_QTY = stk.TOTAL_RECEIVE + stk.IRR_OTHER_QTY + stk.ADJUST_QTY - stk.TOTAL_ISSUE;


                   // stk.COLISING_QTY = stk.OP_F_BAL + stk.IRR_BAL_QTY + stk.STOCK_TRANS_ITEM_TO_ITEM + stk.ADJUST_QTY - stk.ITC_BAL_QTY - stk.STOCK_TRANS_ITEM_TO_ITEM_ISSUE;




                    //stk.ITC_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_DEPT_QTY"]);
                    //stk.ITC_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_STORE_QTY"]);
                    stk.ITC_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_PROD_QTY"]);
                    stk.PRODUCTION_CAPACITY = Conversion.DBNullDecimalToZero(dRow["PRODUCTION_CAPACITY"]);
                    stk.DATEDIFF = Conversion.StringToInt(dRow["DATEDIFF"].ToString());
                    //here total ITC quantity
                   

                    //here total Adjust balance quantity

                    stk.DEPARTMENT_NAME = prmINV.From_Dept_Name;


                    //if (stk.ADJUST_QTY > 0)
                    //{
                    //    stk.IRR_BAL_QTY = stk.IRR_BAL_QTY + stk.ADJUST_QTY;
                    //}
                    //else
                    //{
                    //    stk.ITC_BAL_QTY = stk.ITC_BAL_QTY + Math.Abs(stk.ADJUST_QTY);
                    //}

                    //stk.COLISING_QTY = (stk.OP_F_BAL + stk.IRR_BAL_QTY) - stk.ITC_BAL_QTY;
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

        //Oxide Stock Summary Report
        public static List<rcFormationProductionSummary> Oxide_Production_Stock_Summary_Report(clsPrmInventory prmINV)
        {
            return Oxide_Production_Stock_Summary_Report(prmINV, null);
        }

        public static List<rcFormationProductionSummary> Oxide_Production_Stock_Summary_Report(clsPrmInventory prmINV, DBContext dc)
        {

            List<rcFormationProductionSummary> cRptList = new List<rcFormationProductionSummary>();


            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();

                if (prmINV.toProdDate != "")
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.fromProdDate);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.toProdDate);
                }
                cmdInfo.DBParametersInfo.Add(":p_itemtype_id", prmINV.itemtype_id);
                cmdInfo.DBParametersInfo.Add(":p_itemGroup_id", prmINV.itemGroup_id);
                cmdInfo.DBParametersInfo.Add(":p_item_id", prmINV.item_id);
                cmdInfo.DBParametersInfo.Add(":p_store_id", prmINV.store_id);
                cmdInfo.DBParametersInfo.Add(":p_itemclass_id", prmINV.ItemClassId);
                cmdInfo.DBParametersInfo.Add(":p_battery_type_id", prmINV.Battery_Type_ID);
                cmdInfo.DBParametersInfo.Add(":p_is_repair", prmINV.Is_Repair);
                cmdInfo.DBParametersInfo.Add(":p_dept_id", prmINV.From_Dept_Id);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;
                cmdInfo.CommandText = "SP_OXIDE_PROD_STOCK_SUMMARY";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);


                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;

                sb.Append(" Select ITEM_GROUP_ID,ITEM_GROUP_NAME,ITEM_ID,ITEM_NAME,UOM_ID,UOM_NAME,ITEM_TYPE_ID,ITEM_TYPE_NAME,ITEM_CLASS_ID,ITEM_CLASS_NAME,OP_F_IRR_QTY,OP_ADJUST_QTY,OP_IRR_PROD_QTY,OP_F_ITC_QTY,OP_UF_IRR_QTY,OP_UF_ITC_QTY,IRR_DEPT_QTY,IRR_STORE_QTY,IRR_PROD_QTY,ITC_DEPT_QTY,ITC_STORE_QTY,ITC_PROD_QTY,ADJUST_QTY,RCV_FROM_IB,RCV_FROM_PASTING,REJECT_QTY,CURR_ISS_PAST,CURR_ISS_IB,CURR_ISS_RED_OXID,CURR_ISS_ST,CURR_ISS_OTHERS,IRR_BAL_QTY,ITC_BAL_QTY,PRODUCTION_CAPACITY,DATEDIFF,STOCK_TRANS_ITEM_TO_ITEM_ISSUE FROM TEMP_OXIDE_ITEM_STOCK");


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
                    stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();

                    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);

                    stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                    stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();


                    //here IRR including positive adjustment
                    stk.OP_IRR_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["OP_IRR_PROD_QTY"]);
                    //here ITC including Negative adjustment
                    stk.OP_F_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_F_ITC_QTY"]);
                    //stk.OP_ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ADJUST_QTY"]);
                    stk.OP_F_BAL = stk.OP_IRR_PROD_QTY - stk.OP_F_ITC_QTY;


                    //All issue Distribution Breakdown

                    stk.CURR_ISS_PAST = Conversion.DBNullDecimalToZero(dRow["CURR_ISS_PAST"]);
                    stk.CURR_ISS_RED_OXID = Conversion.DBNullDecimalToZero(dRow["CURR_ISS_RED_OXID"]);
                    stk.CURR_ISS_IB = Conversion.DBNullDecimalToZero(dRow["CURR_ISS_IB"]);

                    stk.CURR_ISS_ST = Conversion.DBNullDecimalToZero(dRow["CURR_ISS_ST"]);
                    stk.CURR_ISS_OTHERS = Conversion.DBNullDecimalToZero(dRow["CURR_ISS_OTHERS"]);


                    // stock Transfer
                    stk.STOCK_TRANS_ITEM_TO_ITEM_ISSUE = Conversion.DBNullDecimalToZero(dRow["STOCK_TRANS_ITEM_TO_ITEM_ISSUE"]);



                    stk.IRR_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_PROD_QTY"]);

                    //here total IRR quantity without Adjust within date range
                    stk.IRR_BAL_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_BAL_QTY"]);


                    //here total ITC quantity without Adjust within date range
                  //  stk.ITC_BAL_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_BAL_QTY"]);
                    stk.ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);

                    //stk.ADJUST_QTY = stk.ADJUST_QTY + stk.STOCK_TRANS_ITEM_TO_ITEM_ISSUE;

                    stk.ITC_BAL_QTY = stk.CURR_ISS_PAST + stk.CURR_ISS_RED_OXID + stk.CURR_ISS_IB + stk.CURR_ISS_ST + stk.CURR_ISS_OTHERS + stk.ADJUST_QTY;
                    stk.COLISING_QTY = stk.OP_F_BAL + stk.IRR_BAL_QTY + stk.ADJUST_QTY - stk.ITC_BAL_QTY - stk.STOCK_TRANS_ITEM_TO_ITEM_ISSUE;




                    //stk.ITC_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_DEPT_QTY"]);
                    //stk.ITC_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_STORE_QTY"]);
                    stk.ITC_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_PROD_QTY"]);
                    stk.PRODUCTION_CAPACITY = Conversion.DBNullDecimalToZero(dRow["PRODUCTION_CAPACITY"]);
                    stk.DATEDIFF = Conversion.StringToInt(dRow["DATEDIFF"].ToString());
                    //here total ITC quantity


                    //here total Adjust balance quantity

                    stk.DEPARTMENT_NAME = prmINV.From_Dept_Name;


                    //if (stk.ADJUST_QTY > 0)
                    //{
                    //    stk.IRR_BAL_QTY = stk.IRR_BAL_QTY + stk.ADJUST_QTY;
                    //}
                    //else
                    //{
                    //    stk.ITC_BAL_QTY = stk.ITC_BAL_QTY + Math.Abs(stk.ADJUST_QTY);
                    //}

                    //stk.COLISING_QTY = (stk.OP_F_BAL + stk.IRR_BAL_QTY) - stk.ITC_BAL_QTY;
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




        public static List<rcFormationProductionSummary> Oxide_RM_Summary_Report(clsPrmInventory prmINV)
        {
            return Oxide_RM_Summary_Report(prmINV, null);
        }

        public static List<rcFormationProductionSummary> Oxide_RM_Summary_Report(clsPrmInventory prmINV, DBContext dc)
        {

            List<rcFormationProductionSummary> cRptList = new List<rcFormationProductionSummary>();


            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();

                if (prmINV.toProdDate != "")
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.fromProdDate);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.toProdDate);
                }
                cmdInfo.DBParametersInfo.Add(":p_itemtype_id", prmINV.itemtype_id);
                cmdInfo.DBParametersInfo.Add(":p_itemGroup_id", prmINV.itemGroup_id);
                cmdInfo.DBParametersInfo.Add(":p_item_id", prmINV.item_id);
                cmdInfo.DBParametersInfo.Add(":p_store_id", prmINV.store_id);
                cmdInfo.DBParametersInfo.Add(":p_itemclass_id", prmINV.ItemClassId);
                cmdInfo.DBParametersInfo.Add(":p_battery_type_id", prmINV.Battery_Type_ID);
                cmdInfo.DBParametersInfo.Add(":p_is_repair", prmINV.Is_Repair);
                cmdInfo.DBParametersInfo.Add(":p_dept_id", prmINV.From_Dept_Id);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;
                cmdInfo.CommandText = "SP_OXIDE_PROD_SUMMARY_RM";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);


                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;

                sb.Append(" Select ITEM_GROUP_ID,ITEM_GROUP_NAME,ITEM_ID,ITEM_NAME,UOM_ID,UOM_NAME,ITEM_TYPE_ID,ITEM_TYPE_NAME,ITEM_CLASS_ID,ITEM_CLASS_NAME,OP_F_IRR_QTY,OP_ADJUST_QTY,OP_IRR_PROD_QTY,OP_F_ITC_QTY,OP_UF_IRR_QTY,OP_UF_ITC_QTY,IRR_DEPT_QTY,IRR_STORE_QTY,IRR_PROD_QTY,ITC_DEPT_QTY,ITC_STORE_QTY,ITC_PROD_QTY,ADJUST_QTY,RCV_FROM_IB,RCV_FROM_PASTING,REJECT_QTY,CURR_ISS_PAST,CURR_ISS_IB,CURR_ISS_RED_OXID,CURR_ISS_ST,CURR_ISS_OTHERS,IRR_BAL_QTY,ITC_BAL_QTY,PRODUCTION_CAPACITY,DATEDIFF,IS_BY_PRODUCT FROM TEMP_OXIDE_ITEM_STOCK");


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
                    stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();

                    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);

                    stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                    stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();


                    //here IRR including positive adjustment
                    stk.OP_IRR_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["OP_IRR_PROD_QTY"]);
                    //here ITC including Negative adjustment
                    stk.OP_F_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_F_ITC_QTY"]);
                    //stk.OP_ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ADJUST_QTY"]);
                    stk.OP_F_BAL = stk.OP_IRR_PROD_QTY - stk.OP_F_ITC_QTY;


                    //All issue Distribution Breakdown

                    stk.CURR_ISS_PAST = Conversion.DBNullDecimalToZero(dRow["CURR_ISS_PAST"]);
                    stk.CURR_ISS_RED_OXID = Conversion.DBNullDecimalToZero(dRow["CURR_ISS_RED_OXID"]);
                    stk.CURR_ISS_IB = Conversion.DBNullDecimalToZero(dRow["CURR_ISS_IB"]);

                    stk.CURR_ISS_ST = Conversion.DBNullDecimalToZero(dRow["CURR_ISS_ST"]);
                    stk.CURR_ISS_OTHERS = Conversion.DBNullDecimalToZero(dRow["CURR_ISS_OTHERS"]);






                    stk.IRR_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_PROD_QTY"]);

                    //here total IRR quantity without Adjust within date range
                    stk.IRR_BAL_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_BAL_QTY"]);

                    //here total ITC quantity without Adjust within date range
                    stk.ITC_BAL_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_BAL_QTY"]);
                    stk.ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);


                    stk.COLISING_QTY = stk.OP_F_BAL + stk.IRR_BAL_QTY + stk.ADJUST_QTY - stk.ITC_BAL_QTY;




                    //stk.ITC_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_DEPT_QTY"]);
                    //stk.ITC_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_STORE_QTY"]);
                    stk.ITC_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_PROD_QTY"]);
                    stk.PRODUCTION_CAPACITY = Conversion.DBNullDecimalToZero(dRow["PRODUCTION_CAPACITY"]);
                    stk.DATEDIFF = Conversion.StringToInt(dRow["DATEDIFF"].ToString());
                    //here total ITC quantity
                    stk.IS_BY_PRODUCT = dRow["IS_BY_PRODUCT"].ToString();

                    //here total Adjust balance quantity

                    stk.DEPARTMENT_NAME = prmINV.From_Dept_Name;


                    //if (stk.ADJUST_QTY > 0)
                    //{
                    //    stk.IRR_BAL_QTY = stk.IRR_BAL_QTY + stk.ADJUST_QTY;
                    //}
                    //else
                    //{
                    //    stk.ITC_BAL_QTY = stk.ITC_BAL_QTY + Math.Abs(stk.ADJUST_QTY);
                    //}

                    //stk.COLISING_QTY = (stk.OP_F_BAL + stk.IRR_BAL_QTY) - stk.ITC_BAL_QTY;
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

        #endregion


        #region VRLA
        public static List<rcMaterialStock> VRLA_Assembly_Raw_Material_Summary_Report(clsPrmInventory prmINV, DBContext dc)
        {
            List<rcMaterialStock> cRptList = new List<rcMaterialStock>();
            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();

                if (prmINV.ToDate.HasValue)
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.ToDate.Value);
                }
                cmdInfo.DBParametersInfo.Add(":p_itemtype_id", prmINV.itemtype_id);
                cmdInfo.DBParametersInfo.Add(":p_itemGroup_id", prmINV.itemGroup_id);
                cmdInfo.DBParametersInfo.Add(":p_item_id", prmINV.item_id);
                cmdInfo.DBParametersInfo.Add(":p_store_id", prmINV.store_id);
                cmdInfo.DBParametersInfo.Add(":p_itemclass_id", prmINV.ItemClassId);
                cmdInfo.DBParametersInfo.Add(":p_battery_type_id", prmINV.Battery_Type_ID);
                cmdInfo.DBParametersInfo.Add(":p_is_repair", prmINV.Is_Repair);
                cmdInfo.DBParametersInfo.Add(":p_dept_id", prmINV.DeptID);
                cmdInfo.DBParametersInfo.Add(":p_is_finished", prmINV.is_Finished);

                DBQuery dbq = new DBQuery();

                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = "SP_RAW_STOCK_REPORT_VRLA";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);
                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;

                sb.Append(" Select ITEM_GROUP_ID,ITEM_GROUP_NAME,ITEM_ID,ITEM_NAME,UOM_ID,UOM_NAME,ITEM_TYPE_ID,ITEM_TYPE_NAME,ITEM_CLASS_ID,ITEM_CLASS_NAME,OP_IRR_QTY,OP_ITC_QTY,IRR_DEPT_QTY,IRR_STORE_QTY,IRR_PROD_QTY,ITC_DEPT_QTY,ITC_STORE_QTY,ITC_PROD_QTY,ADJUST_QTY,OP_ADJUST_QTY,CUR_REJECT_QTY FROM TEMP_ASSEMBLY_ITEM_STOCK  order by  ITEM_GROUP_ID");
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
                    rcMaterialStock stk = new rcMaterialStock();
                    stk.DEPARTMENT_NAME = prmINV.From_Dept_Name;
                    stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();

                    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);

                    stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                    stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();

                    #region Opening Calculation
                    stk.OP_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_IRR_QTY"]);
                    stk.OP_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ITC_QTY"]);
                    stk.OP_ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ADJUST_QTY"]);
                    stk.OPPENING_BAL_QTY = stk.OP_IRR_QTY + stk.OP_ADJUST_QTY - stk.OP_ITC_QTY;
                    #endregion

                    #region Transaction Data Within Date Rang

                    stk.IRR_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_STORE_QTY"]);
                    stk.IRR_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_DEPT_QTY"]);
                    stk.IRR_BAL_QTY = stk.IRR_STORE_QTY + stk.IRR_DEPT_QTY;

                    stk.TOT_RECEIVE_QNTY = stk.OPPENING_BAL_QTY + stk.IRR_BAL_QTY;

                    stk.ITC_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_STORE_QTY"]);
                    stk.ITC_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_PROD_QTY"]);
                    stk.ITC_BAL_QTY = stk.ITC_STORE_QTY + stk.ITC_PROD_QTY;

                    //here total Adjust balance quantity
                    stk.CUR_ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);
                    stk.CUR_REJECT_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_REJECT_QTY"]);
                    #endregion

                    #region Closing Calculation
                    stk.COLISING_QTY = stk.OPPENING_BAL_QTY + stk.IRR_BAL_QTY + stk.CUR_ADJUST_QTY - (stk.ITC_BAL_QTY + stk.CUR_REJECT_QTY);
                    #endregion


                    if (stk.OPPENING_BAL_QTY != 0 || stk.COLISING_QTY != 0 || stk.COLISING_QTY != 0 || stk.IRR_BAL_QTY != 0)
                    {
                        cRptList.Add(stk);
                    }
                }
            }
            //catch { throw; }
            //finally 
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }

            return cRptList;

        }

        public static List<rcMaterialStock> VRLA_Assembly_Raw_Material_Report(clsPrmInventory prmINV, DBContext dc)
        {

            List<rcMaterialStock> cRptList = new List<rcMaterialStock>();
            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();
                cmdInfo.DBParametersInfo.Clear();
                if (prmINV.ToDate.HasValue)
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.ToDate.Value);
                }
                cmdInfo.DBParametersInfo.Add(":p_itemtype_id", prmINV.itemtype_id);
                cmdInfo.DBParametersInfo.Add(":p_itemGroup_id", prmINV.itemGroup_id);
                cmdInfo.DBParametersInfo.Add(":p_item_id", prmINV.item_id);
                cmdInfo.DBParametersInfo.Add(":p_store_id", prmINV.store_id);
                cmdInfo.DBParametersInfo.Add(":p_itemclass_id", prmINV.ItemClassId);
                cmdInfo.DBParametersInfo.Add(":p_battery_type_id", prmINV.Battery_Type_ID);
                cmdInfo.DBParametersInfo.Add(":p_is_repair", prmINV.Is_Repair);
                cmdInfo.DBParametersInfo.Add(":p_dept_id", prmINV.DeptID);
                cmdInfo.DBParametersInfo.Add(":p_is_finished", prmINV.is_Finished);

                DBQuery dbq = new DBQuery();

                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = "SP_RAW_STOCK_REPORT_VRLA";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);
                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;

                sb.Append(" Select ITEM_GROUP_ID,ITEM_GROUP_NAME,ITEM_ID,ITEM_NAME,UOM_ID,UOM_NAME,ITEM_TYPE_ID,ITEM_TYPE_NAME,ITEM_CLASS_ID,ITEM_CLASS_NAME,OP_IRR_QTY,OP_ITC_QTY,IRR_DEPT_QTY,IRR_STORE_QTY,IRR_PROD_QTY,ITC_DEPT_QTY,ITC_STORE_QTY,ITC_PROD_QTY,ADJUST_QTY FROM TEMP_ASSEMBLY_ITEM_STOCK");
                DBQuery dbqtemp = new DBQuery();

                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);
                foreach (DataRow dRow in dtData.Rows)
                {
                    rcMaterialStock stk = new rcMaterialStock();

                    stk.DEPARTMENT_NAME = prmINV.From_Dept_Name;
                    stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();

                    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);

                    stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                    stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();
                    #region Opening Calculation
                    stk.OP_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_IRR_QTY"]);
                    stk.OP_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ITC_QTY"]);
                    stk.OPPENING_BAL_QTY = stk.OP_IRR_QTY - stk.OP_ITC_QTY;
                    #endregion

                    #region Transaction Data Within Date Range
                    //Transaction Within Date Range

                    stk.IRR_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_DEPT_QTY"]);
                    stk.IRR_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_STORE_QTY"]);
                    stk.IRR_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_PROD_QTY"]);

                    //here total IRR quantity
                    stk.IRR_BAL_QTY = stk.IRR_DEPT_QTY + stk.IRR_STORE_QTY + stk.IRR_PROD_QTY;

                    stk.ITC_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_DEPT_QTY"]);
                    stk.ITC_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_STORE_QTY"]);
                    stk.ITC_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_PROD_QTY"]);

                    //here total ITC quantity
                    stk.ITC_BAL_QTY = stk.ITC_DEPT_QTY + stk.ITC_STORE_QTY + stk.ITC_PROD_QTY;

                    //here total Adjust balance quantity
                    stk.ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);
                    #endregion
                    #region Closing Calculation
                    //stk.COLISING_QTY = (stk.OPPENING_BAL_QTY + stk.ADJUST_QTY + stk.IRR_BAL_QTY) - stk.ITC_BAL_QTY;
                    stk.COLISING_QTY = (stk.IRR_BAL_QTY) - stk.ITC_BAL_QTY;
                    #endregion

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

        public static List<rcAssemblyFinishedStock> VRLA_AssemblyFinishedGoodsProductionReport(clsPrmInventory prmINV, DBContext dc)
        {

            List<rcAssemblyFinishedStock> cRptList = new List<rcAssemblyFinishedStock>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();

                if (prmINV.ToDate.HasValue)
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.ToDate.Value);
                }
                cmdInfo.DBParametersInfo.Add(":p_itemtype_id", prmINV.itemtype_id);
                cmdInfo.DBParametersInfo.Add(":p_itemGroup_id", prmINV.itemGroup_id);
                cmdInfo.DBParametersInfo.Add(":p_item_id", prmINV.item_id);
                cmdInfo.DBParametersInfo.Add(":p_store_id", prmINV.store_id);
                cmdInfo.DBParametersInfo.Add(":p_itemclass_id", prmINV.ItemClassId);
                cmdInfo.DBParametersInfo.Add(":p_battery_type_id", prmINV.Battery_Type_ID);
                cmdInfo.DBParametersInfo.Add(":p_is_repair", prmINV.Is_Repair);
                cmdInfo.DBParametersInfo.Add(":p_dept_id", prmINV.DeptID);
                cmdInfo.DBParametersInfo.Add(":p_is_finished", prmINV.is_Finished);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = "SP_VRLA_PROD_REPORT_N";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);

                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;
                sb.Append(" Select ITEM_GROUP_ID,ITEM_GROUP_NAME,ITEM_ID,ITEM_NAME,UOM_ID,UOM_NAME,ITEM_TYPE_ID,ITEM_TYPE_NAME ");
                sb.Append(" ,ITEM_CLASS_ID,ITEM_CLASS_NAME,OP_GD_IRR_QTY,OP_GD_ITC_QTY,IRR_DEPT_QTY,IRR_STORE_QTY,IRR_PROD_QTY ");
                sb.Append(" ,ITC_SOLAR_QTY,ITC_STORE_QTY,ITC_RETURN_BAT_QTY,ADJUST_QTY,OP_ASSEMBLY_QTY,OP_PACKING_QTY ");
                sb.Append(" ,OP_PACKING_ADJUST_QTY,OP_PACKING_RCV_QTY,OP_PACKING_ISS_QTY,OP_ASSEMBEL_ADJUST_QTY,OP_ASSEMBEL_RCV_QTY ");
                sb.Append(" ,OP_ASSEMBEL_ISS_QTY,CUR_ASSEM_PROD_QTY,CUR_ASSEM_USED_QTY,CUR_ASSEM_ADJUST_QTY,CUR_PACKING_ADJUST_QTY  ");
                sb.Append(" ,OP_P_BAT_IRR_QTY,OP_P_BAT_ITC_QTY,OP_P_BAT_BAL_QTY,CUR_P_BAT_IRR_QTY,CUR_P_BAT_ITC_QTY,OP_SERVICE_BAT_IRR_QTY ");
                sb.Append(" ,OP_SERVICE_BAT_ITC_QTY,OP_SERVICE_BAT_BAL_QTY,CUR_SERVICE_BAT_IRR_QTY,CUR_SERVICE_BAT_ITC_QTY,OP_RND_BAT_IRR_QTY ");
                sb.Append(" ,OP_RND_BAT_ITC_QTY,OP_RND_BAT_BAL_QTY,CUR_RND_BAT_IRR_QTY,CUR_RND_BAT_ITC_QTY,OP_OTHERS_BAT_IRR_QTY ");
                sb.Append(" ,OP_OTHERS_BAT_ITC_QTY,OP_OTHERS_BAT_BAL_QTY,CUR_OTHERS_BAT_IRR_QTY,CUR_OTHERS_BAT_ITC_QTY,OP_MR_IRR_QTY ");
                sb.Append(" ,OP_MR_ITC_QTY,OP_MR_BAL_QTY,IRR_RETURN_BAT_QTY,ITEM_ORDER ");
                sb.Append(" ,BATERY_CAT_DESCR,BATERY_CAT_SL_NO ");
                sb.Append(" FROM TEMP_ASSEMBLY_FG_STOCK ");
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
                    stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();
                    stk.ITEM_ORDER = Conversion.DBNullIntToZero(dRow["ITEM_ORDER"]);

                    stk.BATERY_CAT_SL_NO = Conversion.DBNullIntToZero(dRow["BATERY_CAT_SL_NO"]);
                    stk.BATERY_CAT_DESCR = dRow["BATERY_CAT_DESCR"].ToString();

                    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);

                    stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                    stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();

                    stk.OP_GD_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_GD_IRR_QTY"]);
                    stk.OP_GD_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_GD_ITC_QTY"]);
                    stk.OP_GD_BAL = stk.OP_GD_IRR_QTY - stk.OP_GD_ITC_QTY;
                    //stk.OP_ASSEMBLY_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ASSEMBLY_QTY"]);

                    //here Packing quantity opening calculation

                    stk.OP_PACKING_RCV_QTY = Conversion.DBNullDecimalToZero(dRow["OP_PACKING_RCV_QTY"]);
                    stk.OP_PACKING_ISS_QTY = Conversion.DBNullDecimalToZero(dRow["OP_PACKING_ISS_QTY"]);
                    stk.OP_PACKING_ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["OP_PACKING_ADJUST_QTY"]);
                    stk.OP_PACKING_BAL_QTY = stk.OP_PACKING_RCV_QTY + stk.OP_PACKING_ADJUST_QTY - stk.OP_PACKING_ISS_QTY;

                    //Here Assembly Quantity Opening calculation                    
                    stk.OP_ASSEMBEL_RCV_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ASSEMBEL_RCV_QTY"]);

                    //Here issue to solar only
                    stk.OP_ASSEMBEL_ISS_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ASSEMBEL_ISS_QTY"]);
                    stk.OP_ASSEMBEL_ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ASSEMBEL_ADJUST_QTY"]);
                    //stk.OP_ASSEMBEL_BAL_QTY = stk.OP_ASSEMBEL_ADJUST_QTY + stk.OP_ASSEMBEL_RCV_QTY - stk.OP_ASSEMBEL_ISS_QTY;
                    stk.OP_ASSEMBEL_BAL_QTY = stk.OP_ASSEMBEL_ADJUST_QTY + stk.OP_ASSEMBEL_RCV_QTY - (stk.OP_PACKING_RCV_QTY + stk.OP_PACKING_ADJUST_QTY + stk.OP_ASSEMBEL_ISS_QTY);

                    stk.OP_PACKING_QTY = Conversion.DBNullDecimalToZero(dRow["OP_PACKING_QTY"]);
                    stk.OP_GD_WIP = stk.OP_ASSEMBLY_QTY - stk.OP_PACKING_RCV_QTY;//here calculation will be total assembly-total packing declaration

                    stk.IRR_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_DEPT_QTY"]);
                    stk.IRR_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_STORE_QTY"]);
                    stk.CUR_PACKING_RCV_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_PROD_QTY"]);//Here Packing quantity receive from production
                    stk.CUR_PACKING_ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_PACKING_ADJUST_QTY"]);

                    stk.CUR_ASSEM_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_ASSEM_PROD_QTY"]);
                    stk.CUR_ASSEM_USED_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_ASSEM_USED_QTY"]);
                    stk.CUR_ASSEM_ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_ASSEM_ADJUST_QTY"]);

                    //here total IRR quantity
                    //stk.IRR_BAL_QTY = stk.IRR_DEPT_QTY + stk.IRR_STORE_QTY + stk.IRR_PROD_QTY;

                    stk.ITC_SOLAR_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_SOLAR_QTY"]);
                    stk.ITC_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_STORE_QTY"]);

                    stk.ITC_RETURN_BAT_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_RETURN_BAT_QTY"]);
                    //here total ITC quantity
                    //stk.ITC_BAL_QTY = stk.ITC_SOLAR_QTY + stk.ITC_STORE_QTY + stk.ITC_RETURN_BAT_QTY;

                    stk.OP_CUR_TOT_PACKING_QTY = stk.OP_PACKING_BAL_QTY + stk.CUR_PACKING_RCV_QTY + stk.CUR_PACKING_ADJUST_QTY;
                    stk.OP_CUR_TOT_ASSEMBLY_QTY = stk.OP_ASSEMBEL_BAL_QTY + stk.CUR_ASSEM_PROD_QTY + stk.CUR_ASSEM_ADJUST_QTY - (stk.CUR_PACKING_RCV_QTY + stk.CUR_PACKING_ADJUST_QTY);
                   
                    stk.ASSEM_CLOSING_QTY = stk.OP_CUR_TOT_ASSEMBLY_QTY - stk.ITC_SOLAR_QTY;
                    // stk.PACKING_CLOSING_QTY = stk.OP_CUR_TOT_PACKING_QTY- stk.ITC_STORE_QTY - stk.ITC_RETURN_BAT_QTY;
                    stk.PACKING_CLOSING_QTY = stk.OP_CUR_TOT_PACKING_QTY - stk.ITC_STORE_QTY  ;

                    //here total Adjust balance quantity
                    stk.ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);
                    stk.DEPARTMENT_NAME = prmINV.From_Dept_Name;

                    // P,SERVICE,RND,RETURN,OTHERS BATTERYS

                    stk.IRR_RETURN_BAT_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_RETURN_BAT_QTY"]);
                    //stk.OP_GD_WIP = stk.OP_ASSEMBLY_QTY - stk.OP_PACKING_RCV_QTY;//here calculation will be total assembly-total packing declaration

                    stk.OP_MR_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_MR_IRR_QTY"]);
                    stk.OP_MR_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_MR_ITC_QTY"]);
                    stk.OP_MR_BAL_QTY = Conversion.DBNullDecimalToZero(dRow["OP_MR_BAL_QTY"]);//Here Packing quantity receive from production
                    stk.OP_P_BAT_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_P_BAT_IRR_QTY"]);

                    stk.OP_P_BAT_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_P_BAT_ITC_QTY"]);
                    stk.OP_P_BAT_BAL_QTY = stk.OP_P_BAT_IRR_QTY - stk.OP_P_BAT_ITC_QTY;
                        //Conversion.DBNullDecimalToZero(dRow["OP_P_BAT_BAL_QTY"]);
                    stk.CUR_P_BAT_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_P_BAT_IRR_QTY"]);

                    //here total IRR quantity
                    //stk.IRR_BAL_QTY = stk.IRR_DEPT_QTY + stk.IRR_STORE_QTY + stk.IRR_PROD_QTY;

                    stk.CUR_P_BAT_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_P_BAT_ITC_QTY"]);
                    stk.OP_SERVICE_BAT_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_SERVICE_BAT_IRR_QTY"]);
                    stk.OP_SERVICE_BAT_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_SERVICE_BAT_ITC_QTY"]);

                    stk.OP_SERVICE_BAT_BAL_QTY = Conversion.DBNullDecimalToZero(dRow["OP_SERVICE_BAT_BAL_QTY"]);
                    //stk.OP_GD_WIP = stk.OP_ASSEMBLY_QTY - stk.OP_PACKING_RCV_QTY;//here calculation will be total assembly-total packing declaration

                    stk.CUR_SERVICE_BAT_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_SERVICE_BAT_IRR_QTY"]);
                    stk.CUR_SERVICE_BAT_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_SERVICE_BAT_ITC_QTY"]);
                    stk.OP_RND_BAT_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_RND_BAT_IRR_QTY"]);//Here Packing quantity receive from production
                    stk.OP_RND_BAT_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_RND_BAT_ITC_QTY"]);

                    stk.OP_RND_BAT_BAL_QTY = Conversion.DBNullDecimalToZero(dRow["OP_RND_BAT_BAL_QTY"]);
                    stk.CUR_RND_BAT_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_RND_BAT_IRR_QTY"]);
                    stk.CUR_RND_BAT_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_RND_BAT_ITC_QTY"]);

                    //here total IRR quantity
                    //stk.IRR_BAL_QTY = stk.IRR_DEPT_QTY + stk.IRR_STORE_QTY + stk.IRR_PROD_QTY;

                    stk.OP_OTHERS_BAT_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_OTHERS_BAT_IRR_QTY"]);
                    stk.OP_OTHERS_BAT_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_OTHERS_BAT_ITC_QTY"]);
                    stk.OP_OTHERS_BAT_BAL_QTY = Conversion.DBNullDecimalToZero(dRow["OP_OTHERS_BAT_BAL_QTY"]);

                    stk.CUR_OTHERS_BAT_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_OTHERS_BAT_IRR_QTY"]);
                    stk.CUR_OTHERS_BAT_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_OTHERS_BAT_ITC_QTY"]);
                    //stock
                    stk.OP_CUR_TOT_ASSEMBLY_DEPT_QTY = stk.OP_ASSEMBEL_BAL_QTY + stk.CUR_ASSEM_PROD_QTY + stk.CUR_ASSEM_ADJUST_QTY - (stk.CUR_PACKING_RCV_QTY + stk.CUR_PACKING_ADJUST_QTY) + stk.OP_MR_BAL_QTY + stk.OP_P_BAT_BAL_QTY + stk.OP_SERVICE_BAT_BAL_QTY + stk.OP_RND_BAT_BAL_QTY + stk.OP_OTHERS_BAT_BAL_QTY + stk.IRR_RETURN_BAT_QTY + stk.CUR_P_BAT_IRR_QTY + stk.CUR_SERVICE_BAT_IRR_QTY + stk.CUR_RND_BAT_IRR_QTY + stk.CUR_OTHERS_BAT_IRR_QTY;

                    stk.OP_TOTAL_ASS_PROD_BAT_QRT = stk.OP_ASSEMBEL_BAL_QTY + stk.OP_MR_BAL_QTY + stk.OP_P_BAT_BAL_QTY + stk.OP_SERVICE_BAT_BAL_QTY + stk.OP_RND_BAT_BAL_QTY + stk.OP_OTHERS_BAT_BAL_QTY;
                    stk.CUR_TOTAL_BAT_PROD_QTY = stk.CUR_ASSEM_PROD_QTY + stk.IRR_RETURN_BAT_QTY + stk.CUR_P_BAT_IRR_QTY + stk.CUR_SERVICE_BAT_IRR_QTY + stk.CUR_RND_BAT_IRR_QTY + stk.CUR_OTHERS_BAT_IRR_QTY;

                    stk.TOTAL_DELIVERY_TO_MRB = stk.CUR_SERVICE_BAT_ITC_QTY + stk.CUR_P_BAT_ITC_QTY + stk.ITC_RETURN_BAT_QTY;

                    stk.TOTAL_DELIVERY_FROM_ASSEMBLY = stk.TOTAL_DELIVERY_TO_MRB + stk.CUR_OTHERS_BAT_ITC_QTY + stk.CUR_RND_BAT_ITC_QTY + stk.ITC_SOLAR_QTY + stk.ITC_STORE_QTY;

                    stk.MR_BATTERY_CLOSING_QTY = stk.OP_MR_BAL_QTY + stk.IRR_RETURN_BAT_QTY - stk.ITC_RETURN_BAT_QTY;
                    stk.SERVICE_BATTERY_CLOSING_QTY = stk.OP_SERVICE_BAT_BAL_QTY + stk.CUR_SERVICE_BAT_IRR_QTY - stk.CUR_SERVICE_BAT_ITC_QTY;
                    stk.RND_BATTERY_CLOSING_QTY = stk.OP_RND_BAT_BAL_QTY + stk.CUR_RND_BAT_IRR_QTY - stk.CUR_RND_BAT_ITC_QTY;
                    stk.OTHERS_BATTERY_CLOSING_QTY = stk.OP_OTHERS_BAT_BAL_QTY + stk.CUR_OTHERS_BAT_IRR_QTY - stk.CUR_OTHERS_BAT_ITC_QTY;
                    stk.P_BATTERY_CLOSING_QTY = stk.OP_P_BAT_BAL_QTY + stk.CUR_P_BAT_IRR_QTY - stk.CUR_P_BAT_ITC_QTY;

                    stk.TOTAL_ASSEMBLY_CLOSING_QTY = stk.ASSEM_CLOSING_QTY + stk.MR_BATTERY_CLOSING_QTY + stk.SERVICE_BATTERY_CLOSING_QTY + stk.RND_BATTERY_CLOSING_QTY + stk.OTHERS_BATTERY_CLOSING_QTY + stk.P_BATTERY_CLOSING_QTY;

                    stk.TOTAL_ASSEMBLY_OPENING_QTY = stk.OP_ASSEMBEL_ADJUST_QTY + stk.OP_ASSEMBEL_RCV_QTY - (stk.OP_PACKING_RCV_QTY + stk.OP_PACKING_ADJUST_QTY + stk.OP_ASSEMBEL_ISS_QTY) + stk.OP_MR_BAL_QTY + stk.OP_P_BAT_BAL_QTY + stk.OP_SERVICE_BAT_BAL_QTY + stk.OP_RND_BAT_BAL_QTY + stk.OP_OTHERS_BAT_BAL_QTY;

                    if (stk.OP_P_BAT_BAL_QTY > 0 || stk.OP_PACKING_BAL_QTY > 0 || stk.OP_ASSEMBEL_BAL_QTY > 0 || stk.CUR_PACKING_RCV_QTY > 0 || stk.CUR_ASSEM_PROD_QTY > 0 || stk.ITC_SOLAR_QTY > 0 || stk.ITC_STORE_QTY > 0 || stk.OP_CUR_TOT_PACKING_QTY > 0 || stk.OP_CUR_TOT_ASSEMBLY_QTY > 0 || stk.TOTAL_ASSEMBLY_CLOSING_QTY > 0 || stk.OP_MR_BAL_QTY > 0 || stk.MR_BATTERY_CLOSING_QTY > 0 || stk.IRR_RETURN_BAT_QTY > 0 || stk.CUR_P_BAT_IRR_QTY > 0)
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

                if (prmINV.ToDate.HasValue)
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.ToDate.Value);
                }
                cmdInfo.DBParametersInfo.Add(":p_itemtype_id", prmINV.itemtype_id);
                cmdInfo.DBParametersInfo.Add(":p_itemGroup_id", prmINV.itemGroup_id);
                cmdInfo.DBParametersInfo.Add(":p_item_id", prmINV.item_id);
                cmdInfo.DBParametersInfo.Add(":p_store_id", prmINV.store_id);
                cmdInfo.DBParametersInfo.Add(":p_itemclass_id", prmINV.ItemClassId);
                cmdInfo.DBParametersInfo.Add(":p_battery_type_id", prmINV.Battery_Type_ID);
                cmdInfo.DBParametersInfo.Add(":p_is_repair", prmINV.Is_Repair);
                cmdInfo.DBParametersInfo.Add(":p_dept_id", prmINV.DeptID);
                cmdInfo.DBParametersInfo.Add(":p_is_finished", prmINV.is_Finished);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = "SP_VRLA_MONTH_PLATE_INV_RT";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);

                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;
                sb.Append(" Select * FROM TEMP_ASS_PLATE_STOCK");
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
                    stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();

                    stk.ITEM_ORDER = Conversion.DBNullIntToZero(dRow["ITEM_ORDER"]);
                    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);

                    stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                    stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();

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

        #endregion

        #region Transfermation Report

        public static List<rcItemTransfermation> Transfermation_Details_Report(clsPrmInventory prmINV)
        {
            return Transfermation_Details_Report(prmINV, null);
        }

        public static List<rcItemTransfermation> Transfermation_Details_Report(clsPrmInventory prmINV, DBContext dc)
        {

            List<rcItemTransfermation> cRptList = new List<rcItemTransfermation>();


            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                //cmdInfo.DBParametersInfo.Clear();

                //if (prmINV.toProdDate != "")
                //{
                //    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.fromProdDate);
                //    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.toProdDate);
                //}
                //cmdInfo.DBParametersInfo.Add(":p_itemtype_id", prmINV.itemtype_id);
                //cmdInfo.DBParametersInfo.Add(":p_itemGroup_id", prmINV.itemGroup_id);
                //cmdInfo.DBParametersInfo.Add(":p_item_id", prmINV.item_id);
                //cmdInfo.DBParametersInfo.Add(":p_store_id", prmINV.store_id);
                //cmdInfo.DBParametersInfo.Add(":p_itemclass_id", prmINV.ItemClassId);
                //cmdInfo.DBParametersInfo.Add(":p_battery_type_id", prmINV.Battery_Type_ID);
                //cmdInfo.DBParametersInfo.Add(":p_is_repair", prmINV.Is_Repair);
                //cmdInfo.DBParametersInfo.Add(":p_dept_id", prmINV.From_Dept_Id);

                DBQuery dbq = new DBQuery();
                //dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                //cmdInfo.CommandTimeout = 600;
                //cmdInfo.CommandText = "SP_OXIDE_PROD_SUMMARY";
                //cmdInfo.CommandType = CommandType.StoredProcedure;
                //dbq.DBCommandInfo = cmdInfo;
                //DBQuery.ExecuteDBQuerySP(dbq, dc);


                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;

                sb.Append(" SELECT  ");
                sb.Append(" mst.STOCK_TRANSFER_NO ");
                sb.Append(" ,mst.DEPT_ID ");
                sb.Append("  ,mst.STOCK_TRANSFER_DATE ");
                sb.Append("  ,dtl.STOCK_TRANSFER_ITEM_ID  ");
                sb.Append("  ,mitem.ITEM_NAME TRANSFERM_ITEM ");
                sb.Append("  ,itm.ITEM_NAME STOCK_TRANSFER_ITEM_NAME  ");
                sb.Append("  ,dtl.TRANSFER_QTY ");
                sb.Append("  ,mst.TRANSFER_REASON ");
                sb.Append("  ,dtl.REMARKS ");
                sb.Append("  ,um.UOM_CODE_SHORT UOM_NAME ");
                sb.Append("  from INV_ITEM_STOCK_TRANSFER_MST mst ");
                sb.Append(" INNER JOIN  INV_ITEM_STOCK_TRANSFER_DTL dtl ON mst.STOCK_TRANSFER_ID=dtl.STOCK_TRANSFER_ID ");
                sb.Append("  INNER JOIN INV_ITEM_MASTER itm ON dtl.STOCK_TRANSFER_ITEM_ID=itm.ITEM_ID ");
                sb.Append(" INNER JOIN INV_ITEM_MASTER mitem ON mst.ITEM_ID=mitem.ITEM_ID ");
                sb.Append("  INNER JOIN UOM_INFO um ON itm.UOM_ID=um.UOM_ID ");
                sb.Append("  WHERE 1=1 ");

                if (prmINV.From_Dept_Id > 0)
                {
                    sb.Append(" AND mst.DEPT_ID=@pDEPT_ID");
                    cmdInfo.DBParametersInfo.Add("@pDEPT_ID", prmINV.From_Dept_Id);
                }

                if (prmINV.fromProdDate != "")
                {
                    sb.Append(" AND mst.STOCK_TRANSFER_DATE>=@pSTOCK_TRANSFER_DATE");
                    cmdInfo.DBParametersInfo.Add("@pSTOCK_TRANSFER_DATE", prmINV.fromProdDate);
                }

                if (prmINV.item_id > 0)
                {
                    sb.Append(" AND mst.ITEM_ID=@pitem_id");
                    cmdInfo.DBParametersInfo.Add("@pitem_id", prmINV.item_id);
                }
                if (prmINV.toProdDate != "")
                {
                    sb.Append(" AND mst.STOCK_TRANSFER_DATE<=@pTOSTOCK_TRANSFER_DATE");
                    cmdInfo.DBParametersInfo.Add("@pTOSTOCK_TRANSFER_DATE", prmINV.toProdDate);
                }
                sb.Append(" ORDER BY  mst.STOCK_TRANSFER_NO, mst.STOCK_TRANSFER_DATE");
                DBQuery dbqtemp = new DBQuery();

                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);

                foreach (DataRow dRow in dtData.Rows)
                {
                    rcItemTransfermation stk = new rcItemTransfermation();
                    stk.STOCK_TRANSFER_NO = dRow["STOCK_TRANSFER_NO"].ToString();
                    stk.DEPT_ID = Conversion.DBNullIntToZero(dRow["DEPT_ID"]);

                    stk.STOCK_TRANSFER_DATE = Conversion.StringToDate(dRow["STOCK_TRANSFER_DATE"].ToString());
                    stk.STOCK_TRANSFER_ITEM_NAME = dRow["STOCK_TRANSFER_ITEM_NAME"].ToString();

                    stk.TRANSFERM_ITEM = dRow["TRANSFERM_ITEM"].ToString();
                    stk.TRANSFER_QTY = Conversion.DBNullIntToZero(dRow["TRANSFER_QTY"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.TRANSFER_REASON = dRow["TRANSFER_REASON"].ToString();
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.REMARKS = dRow["REMARKS"].ToString();

                    //here total Adjust balance quantity

                    stk.DEPARTMENT_NAME = prmINV.From_Dept_Name;


                    //if (stk.ADJUST_QTY > 0)
                    //{
                    //    stk.IRR_BAL_QTY = stk.IRR_BAL_QTY + stk.ADJUST_QTY;
                    //}
                    //else
                    //{
                    //    stk.ITC_BAL_QTY = stk.ITC_BAL_QTY + Math.Abs(stk.ADJUST_QTY);
                    //}

                    //stk.COLISING_QTY = (stk.OP_F_BAL + stk.IRR_BAL_QTY) - stk.ITC_BAL_QTY;
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

        #endregion

        #region Pasting Recipe Report
        public static List<rcItemTransfermation> Pasting_Recipe_Details_Report(clsPrmInventory prmINV, DBContext dc)
        {

            List<rcItemTransfermation> cRptList = new List<rcItemTransfermation>();


            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();

                if (prmINV.FromDate !=null)
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.FromDate);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.ToDate);
                }
                cmdInfo.DBParametersInfo.Add(":p_itemtype_id", prmINV.itemtype_id);
                cmdInfo.DBParametersInfo.Add(":p_itemGroup_id", prmINV.itemGroup_id);
                cmdInfo.DBParametersInfo.Add(":p_item_id", prmINV.item_id);
                cmdInfo.DBParametersInfo.Add(":p_store_id", prmINV.store_id);
                cmdInfo.DBParametersInfo.Add(":p_itemclass_id", prmINV.ItemClassId);
                cmdInfo.DBParametersInfo.Add(":p_battery_type_id", prmINV.Battery_Type_ID);
                cmdInfo.DBParametersInfo.Add(":p_is_repair", prmINV.Is_Repair);
                cmdInfo.DBParametersInfo.Add(":p_dept_id",7);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;
                cmdInfo.CommandText = "SP_PASTING_RECIPE_REPORT";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);


                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;
                sb.Append(" Select * FROM TEMP_PASTING_RECIPE_STOCK");
                DBQuery dbqtemp = new DBQuery();

                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);

                foreach (DataRow dRow in dtData.Rows)
                {
                    rcItemTransfermation stk = new rcItemTransfermation();
                    stk.DEPT_ID = Conversion.DBNullIntToZero(dRow["DEPARTMENT_ID"]);
                    //stk.RECIPE_DATE = Conversion.StringToDate(dRow["RECIPE_DATE"].ToString());
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.DEPARTMENT_NAME = prmINV.From_Dept_Name;

                    #region Opening
                    stk.OP_ADJUST_QTY = Conversion.StringToDecimal(dRow["OP_ADJUST_QTY"].ToString());
                    stk.OP_IRR_QTY = Conversion.StringToDecimal(dRow["OP_IRR_QTY"].ToString());
                    stk.OP_ITC_QTY = Conversion.StringToDecimal(dRow["OP_ITC_QTY"].ToString());
                    stk.OP_SCRAP_QTY = Conversion.StringToDecimal(dRow["OP_SCRAP_QTY"].ToString());
                    stk.OP_BAL_WITHOUT_SCRAP = stk.OP_IRR_QTY + stk.OP_ADJUST_QTY - stk.OP_ITC_QTY;
                    #endregion


                    #region Transaction_Within_Date_Range
                    stk.IRR_PROD_QTY = Conversion.StringToDecimal(dRow["IRR_PROD_QTY"].ToString());
                    stk.IRR_RECP_QTY = Conversion.StringToDecimal(dRow["IRR_RECP_QTY"].ToString());
                    stk.IRR_SCRAP_QTY = Conversion.StringToDecimal(dRow["IRR_SCRAP_QTY"].ToString());
                    stk.CUR_ADJUST_QTY = Conversion.StringToDecimal(dRow["CUR_ADJUST_QTY"].ToString());

                    stk.IRR_TRANS_QTY = Conversion.StringToDecimal(dRow["IRR_TRANS_QTY"].ToString());
                    stk.ITC_TRANS_QTY = Conversion.StringToDecimal(dRow["ITC_TRANS_QTY"].ToString());
                    #endregion


                    #region Scrap
                    stk.ITC_SCRAP_QTY = Conversion.StringToDecimal(dRow["ITC_SCRAP_QTY"].ToString());
                    #endregion

                    #region Closing

                    stk.CLOSING_WITHOUT_SCRAP = stk.OP_BAL_WITHOUT_SCRAP + stk.IRR_TRANS_QTY + stk.IRR_RECP_QTY - stk.IRR_PROD_QTY - stk.ITC_TRANS_QTY + stk.CUR_ADJUST_QTY;
                    stk.BAL_QTY = stk.IRR_RECP_QTY - stk.IRR_PROD_QTY;
                    #endregion


                    //if (stk.ADJUST_QTY > 0)
                    //{
                    //    stk.IRR_BAL_QTY = stk.IRR_BAL_QTY + stk.ADJUST_QTY;
                    //}
                    //else
                    //{
                    //    stk.ITC_BAL_QTY = stk.ITC_BAL_QTY + Math.Abs(stk.ADJUST_QTY);
                    //}

                    //stk.COLISING_QTY = (stk.OP_F_BAL + stk.IRR_BAL_QTY) - stk.ITC_BAL_QTY;
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

        #endregion


        #region Electrolyte

        public static List<rcMaterialStock> Electrolyte_RM_Summary_Report(clsPrmInventory prmINV, DBContext dc)
        {

            List<rcMaterialStock> cRptList = new List<rcMaterialStock>();


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
                cmdInfo.DBParametersInfo.Add(":p_dept_id", prmINV.From_Dept_Id);
                //cmdInfo.DBParametersInfo.Add(":p_is_finished", prmINV.is_Finished);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;
                cmdInfo.CommandText = "SP_ELECTROLYTE_PROD_SUMMARY_RM";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);
                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;


                sb.Append(" SELECT ITEM_GROUP_ID,ITEM_GROUP_NAME,ITEM_ID,ITEM_NAME,UOM_ID,UOM_NAME,ITEM_TYPE_ID,ITEM_TYPE_NAME,ITEM_CLASS_ID,ITEM_CLASS_NAME,OP_IRR_QTY,OP_ITC_QTY,OP_BAL,IRR_DEPT_QTY,IRR_STORE_QTY,IRR_PROD_QTY ");
                sb.Append(" ,IRR_BAL_QTY,ITC_DEPT_QTY,ITC_STORE_QTY,ITC_PROD_QTY,ITC_BAL_QTY,DEPARTMENT_ID,ADJUST_QTY,ITEM_ORDER from TEMP_ELECTROLYTE_ITEM_STOCK_V1  ");


                DBQuery dbqtemp = new DBQuery();

                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);

                foreach (DataRow dRow in dtData.Rows)
                {
                    rcMaterialStock stk = new rcMaterialStock();
                    stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();

                    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);

                    stk.OP_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_IRR_QTY"]);
                    stk.OP_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ITC_QTY"]);
                    stk.OPPENING_BAL_QTY = stk.OP_IRR_QTY - stk.OP_ITC_QTY;




                    //stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                    //stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    //stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();


                    stk.IRR_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_DEPT_QTY"]);
                //    stk.IRR_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_STORE_QTY"]);
                   // stk.IRR_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_PROD_QTY"]);

                    ////here total IRR quantity   stk.IRR_DEPT_QTY + stk.IRR_PROD_QTY+
                    stk.IRR_BAL_QTY = stk.IRR_DEPT_QTY;

                    //stk.ITC_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_DEPT_QTY"]);
                    //stk.ITC_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_STORE_QTY"]);
                    stk.ITC_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_PROD_QTY"]);

                    ////here total ITC quantity stk.ITC_DEPT_QTY + stk.ITC_STORE_QTY + 
                     stk.ITC_BAL_QTY = stk.ITC_PROD_QTY;

                    ////here total Adjust balance quantity
                     stk.ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);

                    ////stk.PANNEL_QTY = Conversion.DBNullDecimalToZero(dRow["PANNEL_QTY"]);

                    //stk.ISSUE_FOR_PRODUCTION = Conversion.DBNullDecimalToZero(dRow["ISSUE_FOR_PRODUCTION"]);
                    //stk.ISSURE_FOR_DROSS = Conversion.DBNullDecimalToZero(dRow["ISSUE_FOR_DROSS"]);

                    //stk.ITEM_STANDARD_WEIGHT_KG = Conversion.DBNullDecimalToZero(dRow["ITEM_STANDARD_WEIGHT_KG"]);
                    //stk.TOTAL_GRID_STD_WEIGHT_KG = Conversion.DBNullDecimalToZero(dRow["ITEM_STANDARD_WEIGHT_KG"]) * stk.IRR_BAL_QTY;
                  //  stk.ORDER_NO = Conversion.DBNullIntToZero(dRow["ORDER_NO"]);
                    stk.DEPARTMENT_NAME = prmINV.From_Dept_Name;

                    if (stk.ADJUST_QTY > 0)
                    {
                        stk.IRR_BAL_QTY = stk.IRR_BAL_QTY + stk.ADJUST_QTY;
                    }
                    else
                    {
                        stk.ITC_BAL_QTY = stk.ITC_BAL_QTY + Math.Abs(stk.ADJUST_QTY);
                    }


                    stk.COLISING_QTY = (stk.OPPENING_BAL_QTY + stk.IRR_BAL_QTY) - stk.ITC_BAL_QTY;
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


        public static List<rcFormationProductionSummary> Electrolyte_Production_Summary_Report(clsPrmInventory prmINV, DBContext dc)
        {

            List<rcFormationProductionSummary> cRptList = new List<rcFormationProductionSummary>();


            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();

                if (prmINV.toProdDate != "")
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.fromProdDate);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.toProdDate);
                }
                cmdInfo.DBParametersInfo.Add(":p_itemtype_id", prmINV.itemtype_id);
                cmdInfo.DBParametersInfo.Add(":p_itemGroup_id", prmINV.itemGroup_id);
                cmdInfo.DBParametersInfo.Add(":p_item_id", prmINV.item_id);
                cmdInfo.DBParametersInfo.Add(":p_store_id", prmINV.store_id);
                cmdInfo.DBParametersInfo.Add(":p_itemclass_id", prmINV.ItemClassId);
                cmdInfo.DBParametersInfo.Add(":p_battery_type_id", prmINV.Battery_Type_ID);
                cmdInfo.DBParametersInfo.Add(":p_is_repair", prmINV.Is_Repair);
                cmdInfo.DBParametersInfo.Add(":p_dept_id", prmINV.From_Dept_Id);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;
                cmdInfo.CommandText = "SP_ELECTROLYTE_PROD_SUMMARY";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);


                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;

                sb.Append(" Select ITEM_GROUP_ID,ITEM_GROUP_NAME,ITEM_ID,ITEM_NAME,UOM_ID,UOM_NAME,ITEM_TYPE_ID,ITEM_TYPE_NAME,ITEM_CLASS_ID,ITEM_CLASS_NAME,OP_F_IRR_QTY,OP_ADJUST_QTY,OP_IRR_PROD_QTY,OP_F_ITC_QTY,OP_UF_IRR_QTY,OP_UF_ITC_QTY,IRR_DEPT_QTY,IRR_STORE_QTY,IRR_PROD_QTY,ITC_DEPT_QTY,ITC_STORE_QTY,ITC_PROD_QTY,ADJUST_QTY,RCV_FROM_IB,RCV_FROM_PASTING,REJECT_QTY,CURR_ISS_PAST,CURR_ISS_IB,CURR_ISS_RED_OXID,CURR_ISS_ST,CURR_ISS_OTHERS,IRR_BAL_QTY,ITC_BAL_QTY,PRODUCTION_CAPACITY,DATEDIFF,STOCK_TRANS_ITEM_TO_ITEM,STOCK_TRANS_ITEM_TO_ITEM_ISSUE FROM TEMP_ELECTROLYTE_ITEM_STOCK");


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
                    stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();

                    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);

                    stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                    stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();

                    //here IRR including positive adjustment
                    stk.OP_IRR_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["OP_IRR_PROD_QTY"]);
                    //here ITC including Negative adjustment
                    stk.OP_F_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_F_ITC_QTY"]);
                    //stk.OP_ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ADJUST_QTY"]);
                    stk.OP_F_BAL = stk.OP_IRR_PROD_QTY - stk.OP_F_ITC_QTY;

                    //All issue Distribution Breakdown

                    stk.CURR_ISS_PAST = Conversion.DBNullDecimalToZero(dRow["CURR_ISS_PAST"]);
                    stk.CURR_ISS_RED_OXID = Conversion.DBNullDecimalToZero(dRow["CURR_ISS_RED_OXID"]);
                    stk.CURR_ISS_IB = Conversion.DBNullDecimalToZero(dRow["CURR_ISS_IB"]);

                    stk.CURR_ISS_ST = Conversion.DBNullDecimalToZero(dRow["CURR_ISS_ST"]);
                    stk.CURR_ISS_OTHERS = Conversion.DBNullDecimalToZero(dRow["CURR_ISS_OTHERS"]);

                    stk.IRR_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_PROD_QTY"]);

                    //here total IRR quantity without Adjust within date range
                    stk.IRR_BAL_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_BAL_QTY"]);
                    //here total ITC quantity without Adjust within date range
                    stk.ITC_BAL_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_BAL_QTY"]);
                    stk.ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);
                    stk.OXIDE_TOTAL_OUTSALE = stk.CURR_ISS_PAST + stk.CURR_ISS_RED_OXID + stk.CURR_ISS_IB + stk.CURR_ISS_ST;

                    // Stock Transfer Receiv ---issue
                    stk.STOCK_TRANS_ITEM_TO_ITEM = Conversion.DBNullDecimalToZero(dRow["STOCK_TRANS_ITEM_TO_ITEM"]);
                    stk.STOCK_TRANS_ITEM_TO_ITEM_ISSUE = Conversion.DBNullDecimalToZero(dRow["STOCK_TRANS_ITEM_TO_ITEM_ISSUE"]);
                    stk.ITC_BAL_QTY = stk.CURR_ISS_PAST + stk.CURR_ISS_RED_OXID + stk.CURR_ISS_IB + stk.CURR_ISS_ST + stk.CURR_ISS_OTHERS + stk.ADJUST_QTY + stk.STOCK_TRANS_ITEM_TO_ITEM_ISSUE;


                    //this is previous Running Closing Quantity
                    //stk.COLISING_QTY = stk.OP_F_BAL + stk.IRR_BAL_QTY + stk.STOCK_TRANS_ITEM_TO_ITEM + stk.ADJUST_QTY - stk.ITC_BAL_QTY;

                    //This is new Closing
                    stk.TOTAL_RECEIVE = stk.IRR_BAL_QTY + stk.OP_F_BAL + stk.STOCK_TRANS_ITEM_TO_ITEM;
                    stk.TOTAL_ISSUE = stk.ITC_BAL_QTY;
                    stk.COLISING_QTY = stk.TOTAL_RECEIVE + stk.ADJUST_QTY - stk.TOTAL_ISSUE;

                    // stk.COLISING_QTY = stk.OP_F_BAL + stk.IRR_BAL_QTY + stk.STOCK_TRANS_ITEM_TO_ITEM + stk.ADJUST_QTY - stk.ITC_BAL_QTY - stk.STOCK_TRANS_ITEM_TO_ITEM_ISSUE;

                    //stk.ITC_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_DEPT_QTY"]);
                    //stk.ITC_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_STORE_QTY"]);
                    stk.ITC_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_PROD_QTY"]);
                    stk.PRODUCTION_CAPACITY = Conversion.DBNullDecimalToZero(dRow["PRODUCTION_CAPACITY"]);
                    stk.DATEDIFF = Conversion.StringToInt(dRow["DATEDIFF"].ToString());
                    //here total ITC quantity


                    //here total Adjust balance quantity

                    stk.DEPARTMENT_NAME = prmINV.From_Dept_Name;


                    //if (stk.ADJUST_QTY > 0)
                    //{
                    //    stk.IRR_BAL_QTY = stk.IRR_BAL_QTY + stk.ADJUST_QTY;
                    //}
                    //else
                    //{
                    //    stk.ITC_BAL_QTY = stk.ITC_BAL_QTY + Math.Abs(stk.ADJUST_QTY);
                    //}

                    //stk.COLISING_QTY = (stk.OP_F_BAL + stk.IRR_BAL_QTY) - stk.ITC_BAL_QTY;
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


        #region DMW
        public static List<rcMaterialStock> DMW_RM_Summary_Report(clsPrmInventory prmINV, DBContext dc)
        {

            List<rcMaterialStock> cRptList = new List<rcMaterialStock>();


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
                cmdInfo.DBParametersInfo.Add(":p_dept_id", prmINV.From_Dept_Id);
                //cmdInfo.DBParametersInfo.Add(":p_is_finished", prmINV.is_Finished);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;
                cmdInfo.CommandText = "SP_ELECTROLYTE_PROD_SUMMARY_RM";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);
                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;


                sb.Append(" SELECT ITEM_GROUP_ID,ITEM_GROUP_NAME,ITEM_ID,ITEM_NAME,UOM_ID,UOM_NAME,ITEM_TYPE_ID,ITEM_TYPE_NAME,ITEM_CLASS_ID,ITEM_CLASS_NAME,OP_IRR_QTY,OP_ITC_QTY,OP_BAL,IRR_DEPT_QTY,IRR_STORE_QTY,IRR_PROD_QTY ");
                sb.Append(" ,IRR_BAL_QTY,ITC_DEPT_QTY,ITC_STORE_QTY,ITC_PROD_QTY,ITC_BAL_QTY,DEPARTMENT_ID,ADJUST_QTY,ITEM_ORDER from TEMP_ELECTROLYTE_ITEM_STOCK  ");


                DBQuery dbqtemp = new DBQuery();

                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);

                foreach (DataRow dRow in dtData.Rows)
                {
                    rcMaterialStock stk = new rcMaterialStock();
                    stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();

                    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);

                    stk.OP_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_IRR_QTY"]);
                    stk.OP_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ITC_QTY"]);
                    stk.OPPENING_BAL_QTY = stk.OP_IRR_QTY - stk.OP_ITC_QTY;




                    //stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                    //stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    //stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();


                    //stk.IRR_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_DEPT_QTY"]);
                    //    stk.IRR_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_STORE_QTY"]);
                    // stk.IRR_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_PROD_QTY"]);

                    ////here total IRR quantity   stk.IRR_DEPT_QTY + stk.IRR_PROD_QTY+
                    stk.IRR_BAL_QTY = stk.IRR_STORE_QTY;

                    //stk.ITC_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_DEPT_QTY"]);
                    //stk.ITC_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_STORE_QTY"]);
                    stk.ITC_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_PROD_QTY"]);

                    ////here total ITC quantity stk.ITC_DEPT_QTY + stk.ITC_STORE_QTY + 
                    stk.ITC_BAL_QTY = stk.ITC_PROD_QTY;

                    ////here total Adjust balance quantity
                    stk.ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);

                    ////stk.PANNEL_QTY = Conversion.DBNullDecimalToZero(dRow["PANNEL_QTY"]);

                    //stk.ISSUE_FOR_PRODUCTION = Conversion.DBNullDecimalToZero(dRow["ISSUE_FOR_PRODUCTION"]);
                    //stk.ISSURE_FOR_DROSS = Conversion.DBNullDecimalToZero(dRow["ISSUE_FOR_DROSS"]);

                    //stk.ITEM_STANDARD_WEIGHT_KG = Conversion.DBNullDecimalToZero(dRow["ITEM_STANDARD_WEIGHT_KG"]);
                    //stk.TOTAL_GRID_STD_WEIGHT_KG = Conversion.DBNullDecimalToZero(dRow["ITEM_STANDARD_WEIGHT_KG"]) * stk.IRR_BAL_QTY;
                    //  stk.ORDER_NO = Conversion.DBNullIntToZero(dRow["ORDER_NO"]);
                    stk.DEPARTMENT_NAME = prmINV.From_Dept_Name;

                    if (stk.ADJUST_QTY > 0)
                    {
                        stk.IRR_BAL_QTY = stk.IRR_BAL_QTY + stk.ADJUST_QTY;
                    }
                    else
                    {
                        stk.ITC_BAL_QTY = stk.ITC_BAL_QTY + Math.Abs(stk.ADJUST_QTY);
                    }


                    stk.COLISING_QTY = (stk.OPPENING_BAL_QTY + stk.IRR_BAL_QTY) - stk.ITC_BAL_QTY;
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


        public static List<rcFormationProductionSummary> DMW_Production_Summary_Report(clsPrmInventory prmINV, DBContext dc)
        {

            List<rcFormationProductionSummary> cRptList = new List<rcFormationProductionSummary>();


            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();

                if (prmINV.toProdDate != "")
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.fromProdDate);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.toProdDate);
                }
                cmdInfo.DBParametersInfo.Add(":p_itemtype_id", prmINV.itemtype_id);
                cmdInfo.DBParametersInfo.Add(":p_itemGroup_id", prmINV.itemGroup_id);
                cmdInfo.DBParametersInfo.Add(":p_item_id", prmINV.item_id);
                cmdInfo.DBParametersInfo.Add(":p_store_id", prmINV.store_id);
                cmdInfo.DBParametersInfo.Add(":p_itemclass_id", prmINV.ItemClassId);
                cmdInfo.DBParametersInfo.Add(":p_battery_type_id", prmINV.Battery_Type_ID);
                cmdInfo.DBParametersInfo.Add(":p_is_repair", prmINV.Is_Repair);
                cmdInfo.DBParametersInfo.Add(":p_dept_id", prmINV.From_Dept_Id);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;
                cmdInfo.CommandText = "SP_DMW_PROD_SUMMARY";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);


                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;

                sb.Append(" Select ITEM_GROUP_ID,ITEM_GROUP_NAME,ITEM_ID,ITEM_NAME,UOM_ID,UOM_NAME,ITEM_TYPE_ID,ITEM_TYPE_NAME,ITEM_CLASS_ID,ITEM_CLASS_NAME,OP_F_IRR_QTY,OP_ADJUST_QTY,OP_IRR_PROD_QTY,OP_F_ITC_QTY,OP_UF_IRR_QTY,OP_UF_ITC_QTY,IRR_DEPT_QTY,IRR_STORE_QTY,IRR_PROD_QTY,ITC_DEPT_QTY,ITC_STORE_QTY,ITC_PROD_QTY,ADJUST_QTY,RCV_FROM_IB,RCV_FROM_PASTING,REJECT_QTY,CURR_ISS_PAST,CURR_ISS_IB,CURR_ISS_RED_OXID,CURR_ISS_ST,CURR_ISS_OTHERS,IRR_BAL_QTY,ITC_BAL_QTY,PRODUCTION_CAPACITY,DATEDIFF,STOCK_TRANS_ITEM_TO_ITEM,STOCK_TRANS_ITEM_TO_ITEM_ISSUE FROM TEMP_DMW_ITEM_STOCK");


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
                    stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();

                    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);

                    stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                    stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();

                    //here IRR including positive adjustment
                    stk.OP_IRR_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["OP_IRR_PROD_QTY"]);
                    //here ITC including Negative adjustment
                    stk.OP_F_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_F_ITC_QTY"]);
                    //stk.OP_ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ADJUST_QTY"]);
                    stk.OP_F_BAL = stk.OP_IRR_PROD_QTY - stk.OP_F_ITC_QTY;

                    //All issue Distribution Breakdown

                    stk.CURR_ISS_PAST = Conversion.DBNullDecimalToZero(dRow["CURR_ISS_PAST"]);
                    stk.CURR_ISS_RED_OXID = Conversion.DBNullDecimalToZero(dRow["CURR_ISS_RED_OXID"]);
                    stk.CURR_ISS_IB = Conversion.DBNullDecimalToZero(dRow["CURR_ISS_IB"]);

                    stk.CURR_ISS_ST = Conversion.DBNullDecimalToZero(dRow["CURR_ISS_ST"]);
                    stk.CURR_ISS_OTHERS = Conversion.DBNullDecimalToZero(dRow["CURR_ISS_OTHERS"]);

                    stk.IRR_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_PROD_QTY"]);

                    //here total IRR quantity without Adjust within date range
                    stk.IRR_BAL_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_BAL_QTY"]);
                    //here total ITC quantity without Adjust within date range
                    stk.ITC_BAL_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_BAL_QTY"]);
                    stk.ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);
                    stk.OXIDE_TOTAL_OUTSALE = stk.CURR_ISS_PAST + stk.CURR_ISS_RED_OXID + stk.CURR_ISS_IB + stk.CURR_ISS_ST;

                    // Stock Transfer Receiv ---issue
                    stk.STOCK_TRANS_ITEM_TO_ITEM = Conversion.DBNullDecimalToZero(dRow["STOCK_TRANS_ITEM_TO_ITEM"]);
                    stk.STOCK_TRANS_ITEM_TO_ITEM_ISSUE = Conversion.DBNullDecimalToZero(dRow["STOCK_TRANS_ITEM_TO_ITEM_ISSUE"]);
                    stk.ITC_BAL_QTY = stk.CURR_ISS_PAST + stk.CURR_ISS_RED_OXID + stk.CURR_ISS_IB + stk.CURR_ISS_ST + stk.CURR_ISS_OTHERS + stk.ADJUST_QTY + stk.STOCK_TRANS_ITEM_TO_ITEM_ISSUE;


                    //this is previous Running Closing Quantity
                    //stk.COLISING_QTY = stk.OP_F_BAL + stk.IRR_BAL_QTY + stk.STOCK_TRANS_ITEM_TO_ITEM + stk.ADJUST_QTY - stk.ITC_BAL_QTY;

                    //This is new Closing
                    stk.TOTAL_RECEIVE = stk.IRR_BAL_QTY + stk.OP_F_BAL + stk.STOCK_TRANS_ITEM_TO_ITEM;
                    stk.TOTAL_ISSUE = stk.ITC_BAL_QTY;
                    stk.COLISING_QTY = stk.TOTAL_RECEIVE + stk.ADJUST_QTY - stk.TOTAL_ISSUE;

                    // stk.COLISING_QTY = stk.OP_F_BAL + stk.IRR_BAL_QTY + stk.STOCK_TRANS_ITEM_TO_ITEM + stk.ADJUST_QTY - stk.ITC_BAL_QTY - stk.STOCK_TRANS_ITEM_TO_ITEM_ISSUE;

                    //stk.ITC_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_DEPT_QTY"]);
                    //stk.ITC_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_STORE_QTY"]);
                    stk.ITC_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_PROD_QTY"]);
                    stk.PRODUCTION_CAPACITY = Conversion.DBNullDecimalToZero(dRow["PRODUCTION_CAPACITY"]);
                    stk.DATEDIFF = Conversion.StringToInt(dRow["DATEDIFF"].ToString());
                    //here total ITC quantity


                    //here total Adjust balance quantity

                    stk.DEPARTMENT_NAME = prmINV.From_Dept_Name;


                    //if (stk.ADJUST_QTY > 0)
                    //{
                    //    stk.IRR_BAL_QTY = stk.IRR_BAL_QTY + stk.ADJUST_QTY;
                    //}
                    //else
                    //{
                    //    stk.ITC_BAL_QTY = stk.ITC_BAL_QTY + Math.Abs(stk.ADJUST_QTY);
                    //}

                    //stk.COLISING_QTY = (stk.OP_F_BAL + stk.IRR_BAL_QTY) - stk.ITC_BAL_QTY;
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

        #region -****************************** Pure Lead *************************************************-
        public static List<rcFormationProductionSummary> PureLeadFinishedGoodsSummaryReport(clsPrmInventory prmINV, DBContext dc)
        {
            List<rcFormationProductionSummary> cRptList = new List<rcFormationProductionSummary>();
            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();

                if (prmINV.ToDate.HasValue)
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.ToDate.Value);
                }
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
                cmdInfo.CommandText = " SP_PURELEAD_PROD_STOCK_SUMMARY ";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);

                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;
                sb.Append(" SELECT * FROM TEMP_PURELEAD_ITEM_STOCK ");
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
                    stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();

                    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);

                    stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                    stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();


                    //here IRR including positive adjustment
                    stk.OP_IRR_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["OP_IRR_PROD_QTY"]);
                    //here ITC including Negative adjustment
                    stk.OP_F_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_F_ITC_QTY"]);
                    stk.OP_F_BAL = stk.OP_IRR_PROD_QTY - stk.OP_F_ITC_QTY;

                    stk.IRR_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_PROD_QTY"]);


                    stk.TOTAL_RECEIVE = stk.OP_F_BAL + stk.IRR_PROD_QTY;



                    //All issue Distribution Breakdown

                    //stk.CURR_ISS_PAST = Conversion.DBNullDecimalToZero(dRow["CURR_ISS_PAST"]);
                    //stk.CURR_ISS_RED_OXID = Conversion.DBNullDecimalToZero(dRow["CURR_ISS_RED_OXID"]);
                    //stk.CURR_ISS_IB = Conversion.DBNullDecimalToZero(dRow["CURR_ISS_IB"]);

                    //stk.CURR_ISS_ST = Conversion.DBNullDecimalToZero(dRow["CURR_ISS_ST"]);
                    //stk.CURR_ISS_OTHERS = Conversion.DBNullDecimalToZero(dRow["CURR_ISS_OTHERS"]);


                  

                    ////here total IRR quantity without Adjust within date range
                    //stk.IRR_BAL_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_BAL_QTY"]);
                    ////here total ITC quantity without Adjust within date range
                   // stk.ITC_BAL_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_BAL_QTY"]);
                    stk.ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);
                    //stk.OXIDE_TOTAL_OUTSALE = stk.CURR_ISS_PAST + stk.CURR_ISS_RED_OXID + stk.CURR_ISS_IB + stk.CURR_ISS_ST;
                    stk.ITC_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_STORE_QTY"]);
                   

                    // Stock Transfer Receiv ---issue
                    //stk.STOCK_TRANS_ITEM_TO_ITEM = Conversion.DBNullDecimalToZero(dRow["STOCK_TRANS_ITEM_TO_ITEM"]);
                    //stk.STOCK_TRANS_ITEM_TO_ITEM_ISSUE = Conversion.DBNullDecimalToZero(dRow["STOCK_TRANS_ITEM_TO_ITEM_ISSUE"]);
                    //stk.ITC_BAL_QTY = stk.CURR_ISS_PAST + stk.CURR_ISS_RED_OXID + stk.CURR_ISS_IB + stk.CURR_ISS_ST + stk.CURR_ISS_OTHERS + stk.ADJUST_QTY + stk.STOCK_TRANS_ITEM_TO_ITEM_ISSUE;


                    //this is previous Running Closing Quantity
                    //stk.COLISING_QTY = stk.OP_F_BAL + stk.IRR_BAL_QTY + stk.STOCK_TRANS_ITEM_TO_ITEM + stk.ADJUST_QTY - stk.ITC_BAL_QTY;

                    //This is new Closing
                    //stk.TOTAL_RECEIVE = stk.IRR_BAL_QTY + stk.OP_F_BAL + stk.STOCK_TRANS_ITEM_TO_ITEM;

                    stk.ITC_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_DEPT_QTY"]);
                    stk.TOTAL_ISSUE = stk.ITC_STORE_QTY + stk.ITC_DEPT_QTY;
                    stk.COLISING_QTY = stk.TOTAL_RECEIVE + stk.ADJUST_QTY - stk.TOTAL_ISSUE;


                    // stk.COLISING_QTY = stk.OP_F_BAL + stk.IRR_BAL_QTY + stk.STOCK_TRANS_ITEM_TO_ITEM + stk.ADJUST_QTY - stk.ITC_BAL_QTY - stk.STOCK_TRANS_ITEM_TO_ITEM_ISSUE;




                   
                    
                    //stk.ITC_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_PROD_QTY"]);
                    //stk.PRODUCTION_CAPACITY = Conversion.DBNullDecimalToZero(dRow["PRODUCTION_CAPACITY"]);
                    //stk.DATEDIFF = Conversion.StringToInt(dRow["DATEDIFF"].ToString());
                    //here total ITC quantity


                    //here total Adjust balance quantity

                    stk.DEPARTMENT_NAME = prmINV.From_Dept_Name;


                    //if (stk.ADJUST_QTY > 0)
                    //{
                    //    stk.IRR_BAL_QTY = stk.IRR_BAL_QTY + stk.ADJUST_QTY;
                    //}
                    //else
                    //{
                    //    stk.ITC_BAL_QTY = stk.ITC_BAL_QTY + Math.Abs(stk.ADJUST_QTY);
                    //}

                    //stk.COLISING_QTY = (stk.OP_F_BAL + stk.IRR_BAL_QTY) - stk.ITC_BAL_QTY;
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



        public static List<rcMaterialStock> PureLead_Raw_Material_Summary_Report(clsPrmInventory prmINV, DBContext dc)
        {

            List<rcMaterialStock> cRptList = new List<rcMaterialStock>();


            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();

                if (prmINV.ToDate.HasValue)
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.ToDate.Value);
                }
                cmdInfo.DBParametersInfo.Add(":p_itemtype_id", prmINV.itemtype_id);
                cmdInfo.DBParametersInfo.Add(":p_itemGroup_id", prmINV.itemGroup_id);
                cmdInfo.DBParametersInfo.Add(":p_item_id", prmINV.item_id);
                cmdInfo.DBParametersInfo.Add(":p_store_id", prmINV.store_id);
                cmdInfo.DBParametersInfo.Add(":p_itemclass_id", prmINV.ItemClassId);
                cmdInfo.DBParametersInfo.Add(":p_battery_type_id", prmINV.Battery_Type_ID);
                cmdInfo.DBParametersInfo.Add(":p_is_repair", prmINV.Is_Repair);
                cmdInfo.DBParametersInfo.Add(":p_dept_id", prmINV.DeptID);
                cmdInfo.DBParametersInfo.Add(":p_is_finished", prmINV.is_Finished);

                DBQuery dbq = new DBQuery();

                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = "SP_PURE_LEAD_RAW_STOCK_REPORT";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);
                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;

                // sb.Append(" Select ITEM_GROUP_ID,ITEM_GROUP_NAME,ITEM_ID,ITEM_NAME,UOM_ID,UOM_NAME,ITEM_TYPE_ID,ITEM_TYPE_NAME,ITEM_CLASS_ID,ITEM_CLASS_NAME,OP_IRR_QTY,OP_ITC_QTY,IRR_DEPT_QTY,IRR_STORE_QTY,IRR_PROD_QTY,ITC_DEPT_QTY,ITC_STORE_QTY,ITC_PROD_QTY,ADJUST_QTY,OP_ADJUST_QTY,CUR_REJECT_QTY FROM TEMP_ASSEMBLY_ITEM_STOCK  WHERE ITEM_GROUP_ID!=1111 order by  ITEM_GROUP_ID,ITEM_NAME");
                sb.Append(" SELECT * FROM TEMP_PURE_LEAD_RM_ITEM_STOCK ");
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
                    rcMaterialStock stk = new rcMaterialStock();
                    stk.DEPARTMENT_NAME = prmINV.From_Dept_Name;
                    stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();

                    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);

                    stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                    stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();

                    #region Opening Calculation
                    stk.OP_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_IRR_QTY"]);
                    stk.OP_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ITC_QTY"]);
                    stk.OP_ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ADJUST_QTY"]);
                    stk.OPPENING_BAL_QTY = stk.OP_IRR_QTY + stk.OP_ADJUST_QTY - stk.OP_ITC_QTY;
                    #endregion

                    #region Transaction Data Within Date Rang

                    stk.IRR_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_STORE_QTY"]);
                    stk.IRR_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_DEPT_QTY"]);
                    stk.IRR_BAL_QTY = stk.IRR_STORE_QTY + stk.IRR_DEPT_QTY;

                    stk.TOT_RECEIVE_QNTY = stk.OPPENING_BAL_QTY + stk.IRR_BAL_QTY;

                    stk.ITC_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_STORE_QTY"]);
                    stk.ITC_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_PROD_QTY"]);
                    stk.ITC_BAL_QTY = stk.ITC_STORE_QTY + stk.ITC_PROD_QTY;

                    //here total Adjust balance quantity
                    stk.CUR_ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);
                    stk.CUR_REJECT_QTY = Conversion.DBNullDecimalToZero(dRow["CUR_REJECT_QTY"]);
                    #endregion

                    #region Closing Calculation
                    stk.COLISING_QTY = stk.OPPENING_BAL_QTY + stk.IRR_BAL_QTY + stk.CUR_ADJUST_QTY - (stk.ITC_BAL_QTY + stk.CUR_REJECT_QTY);
                    #endregion
                    stk.ORDER_NO = Conversion.StringToInt(dRow["ORDER_NO"].ToString());

                    //if (stk.OPPENING_BAL_QTY != 0 || stk.COLISING_QTY != 0 || stk.ITC_BAL_QTY != 0 || stk.IRR_BAL_QTY != 0)
                    if (stk.OPPENING_BAL_QTY != 0 || stk.IRR_STORE_QTY != 0 || stk.IRR_DEPT_QTY != 0 || stk.ITC_PROD_QTY != 0 || stk.ITC_BAL_QTY != 0)
                    {
                        cRptList.Add(stk);
                    }


                }




            }
            //catch { throw; }
            //finally 
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }

            return cRptList;

        }
        #endregion


        #region  ********************************* MRB*************************************************
        public static List<rcFormationProductionSummary> MRB_Production_Summary_Report(clsPrmInventory prmINV, DBContext dc)
        {

            List<rcFormationProductionSummary> cRptList = new List<rcFormationProductionSummary>();


            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();

                if (prmINV.toProdDate != "")
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.fromProdDate);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.toProdDate);
                }
                cmdInfo.DBParametersInfo.Add(":p_itemtype_id", prmINV.itemtype_id);
                cmdInfo.DBParametersInfo.Add(":p_itemGroup_id", prmINV.itemGroup_id);
                cmdInfo.DBParametersInfo.Add(":p_item_id", prmINV.item_id);
                cmdInfo.DBParametersInfo.Add(":p_store_id", prmINV.store_id);
                cmdInfo.DBParametersInfo.Add(":p_itemclass_id", prmINV.ItemClassId);
                cmdInfo.DBParametersInfo.Add(":p_battery_type_id", prmINV.Battery_Type_ID);
                cmdInfo.DBParametersInfo.Add(":p_is_repair", prmINV.Is_Repair);
                cmdInfo.DBParametersInfo.Add(":p_dept_id", prmINV.From_Dept_Id);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;
                cmdInfo.CommandText = "SP_MRB_PROD_SUMMARY";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);


                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;

                sb.Append(" Select ITEM_GROUP_ID,ITEM_GROUP_NAME,ITEM_ID,ITEM_NAME,UOM_ID,UOM_NAME,ITEM_TYPE_ID,ITEM_TYPE_NAME,ITEM_CLASS_ID,ITEM_CLASS_NAME,OP_F_IRR_QTY,OP_ADJUST_QTY,OP_IRR_PROD_QTY,OP_F_ITC_QTY,OP_UF_IRR_QTY,OP_UF_ITC_QTY,IRR_DEPT_QTY,IRR_STORE_QTY,IRR_PROD_QTY,ITC_DEPT_QTY,ITC_STORE_QTY,ITC_PROD_QTY,ADJUST_QTY,RCV_FROM_IB,RCV_FROM_PASTING,REJECT_QTY,CURR_ISS_PAST,CURR_ISS_IB,CURR_ISS_RED_OXID,CURR_ISS_ST,CURR_ISS_OTHERS,IRR_BAL_QTY,ITC_BAL_QTY,PRODUCTION_CAPACITY,DATEDIFF,STOCK_TRANS_ITEM_TO_ITEM,STOCK_TRANS_ITEM_TO_ITEM_ISSUE,ISSUE_TO_ASSEMBLY FROM TEMP_MRB_ITEM_STOCK");


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
                    stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();

                    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);

                    stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                    stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();

                    //here IRR including positive adjustment
                    stk.OP_IRR_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["OP_IRR_PROD_QTY"]);
                    //here ITC including Negative adjustment
                    stk.OP_F_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_F_ITC_QTY"]);
                    stk.OP_F_BAL = stk.OP_IRR_PROD_QTY - stk.OP_F_ITC_QTY;

                    //All issue Distribution Breakdown
                    stk.ISS_TO_ASSEMBLY = Conversion.DBNullDecimalToZero(dRow["ISSUE_TO_ASSEMBLY"]);

                    stk.CURR_ISS_ST = Conversion.DBNullDecimalToZero(dRow["CURR_ISS_ST"]);
                    stk.CURR_ISS_OTHERS = Conversion.DBNullDecimalToZero(dRow["CURR_ISS_OTHERS"]);
                  
                    stk.IRR_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_PROD_QTY"]);

                    //here total IRR quantity without Adjust within date range
                    stk.IRR_BAL_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_BAL_QTY"]);
                    //here total ITC quantity without Adjust within date range
                    stk.ITC_BAL_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_BAL_QTY"]);
                    stk.ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);
                    stk.OXIDE_TOTAL_OUTSALE = stk.ISS_TO_ASSEMBLY + stk.CURR_ISS_ST + stk.CURR_ISS_OTHERS;

                    // Stock Transfer Receiv ---issue
                    stk.ITC_BAL_QTY = stk.ISS_TO_ASSEMBLY +    stk.CURR_ISS_ST + stk.CURR_ISS_OTHERS + stk.ADJUST_QTY ;

                    //This is new Closing
                    stk.TOTAL_RECEIVE = stk.IRR_BAL_QTY + stk.OP_F_BAL;
                    stk.TOTAL_ISSUE = stk.ITC_BAL_QTY;
                    stk.COLISING_QTY = stk.TOTAL_RECEIVE + stk.ADJUST_QTY - stk.TOTAL_ISSUE;
                    stk.ITC_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_PROD_QTY"]);

                    //here total Adjust balance quantity
                    stk.DEPARTMENT_NAME = prmINV.From_Dept_Name;
                    if (stk.IRR_BAL_QTY > 0 || stk.OP_F_BAL > 0 || stk.ISS_TO_ASSEMBLY > 0 || stk.COLISING_QTY >0 || stk.TOTAL_RECEIVE>0)
                    {
                        cRptList.Add(stk);
                    }
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


        #region ***************************************** Forecast Report ********************

        public static List<rcFGF>  GetWeeklyForcastReport(clsPrmInventory prmINV, DBContext dc)
        {
            List<rcFGF> cRptList = new List<rcFGF>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();

                if (prmINV.toProdDate != "")
                {
                    cmdInfo.DBParametersInfo.Add(":p_from_date", prmINV.fromProdDate);
                   // cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.toProdDate);
                }
                cmdInfo.DBParametersInfo.Add(":p_month", prmINV.From_MONTH);
                cmdInfo.DBParametersInfo.Add(":p_year", prmINV.From_YEAR);
                //cmdInfo.DBParametersInfo.Add(":p_item_id", prmINV.item_id);
                //cmdInfo.DBParametersInfo.Add(":p_store_id", prmINV.store_id);
                //cmdInfo.DBParametersInfo.Add(":p_itemclass_id", prmINV.ItemClassId);
                //cmdInfo.DBParametersInfo.Add(":p_battery_type_id", prmINV.Battery_Type_ID);
                //cmdInfo.DBParametersInfo.Add(":p_is_repair", prmINV.Is_Repair);
                //cmdInfo.DBParametersInfo.Add(":p_dept_id", prmINV.From_Dept_Id);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;
                cmdInfo.CommandText = "SP_WKLY_FG_FORECAST";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);


                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;
                sb.Append(" Select  * FROM TEMP_WK_FG_FORECAST");
                DBQuery dbqtemp = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);
                foreach (DataRow dRow in dtData.Rows)
                {
                    rcFGF stk = new rcFGF();
                    stk.BATERY_CAT_ID = Conversion.StringToInt(dRow["BATERY_CAT_ID"].ToString());
                    stk.BATERY_CAT_DESCR = dRow["BATERY_CAT_DESCR"].ToString();
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();
                    stk.WK1_ITEM_QTY = Conversion.StringToInt(dRow["WK1_ITEM_QTY"].ToString());
                    stk.WK1_DELIVERED = Conversion.StringToInt(dRow["WK1_DELIVERED"].ToString());
                    stk.WK2_ITEM_QTY = Conversion.StringToInt(dRow["WK2_ITEM_QTY"].ToString());
                    stk.WK2_DELIVERED = Conversion.StringToInt(dRow["WK2_DELIVERED"].ToString());
                    stk.WK3_ITEM_QTY = Conversion.StringToInt(dRow["WK3_ITEM_QTY"].ToString());
                    stk.WK3_DELIVERED = Conversion.StringToInt(dRow["WK3_DELIVERED"].ToString());
                    stk.WK4_ITEM_QTY = Conversion.StringToInt(dRow["WK4_ITEM_QTY"].ToString());
                    stk.WK4_DELIVERED = Conversion.StringToInt(dRow["WK4_DELIVERED"].ToString());
                    stk.TOTAL_MONTH_QTY = Conversion.StringToInt(dRow["TOTAL_MONTH_QTY"].ToString());
                    stk.TOTAL_MONTH_DELIVERED = Conversion.StringToInt(dRow["TOTAL_MONTH_DELIVERED"].ToString());
                    cRptList.Add(stk);
                }

                 }
            catch(Exception ex)
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }

            return cRptList;
            }
      
        #endregion


        #region ********************************************** Item Stock Ledger*********************************************

        public static string GetItemOPBalance(clsPrmInventory prmINV, DBContext dc)
        {
            bool isDCInit = false;
            string _vBalance = string.Empty;
            try
            {

                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                DateTime dt = Convert.ToDateTime(prmINV.fromProdDate).AddDays(-1) ;
                //FN_GET_CLOSING_QTY_NORMAL_DEPT(@FromDate,@ToDate,@itemID, @deptID)
                string abbr = " SELECT ITEM_TARGET_DATE_CLOSING_QTY(@itemID,@deptID, @FromDate,601) vbalance FROM DUAL  ";
                cmdInfo.DBParametersInfo.Add("@itemID", prmINV.item_id);
                cmdInfo.DBParametersInfo.Add("@deptID", prmINV.From_Dept_Id);
                cmdInfo.DBParametersInfo.Add("@FromDate", Convert.ToDateTime(prmINV.fromProdDate).AddDays(-1));
                //cmdInfo.DBParametersInfo.Add("@ToDate", prmINV.toProdDate);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                _vBalance = DBQuery.ExecuteDBScalar(dbq, dc).ToString();

            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return _vBalance;
        }


        public static List<rcItemTransfermation> Item_Stock_Ledger_Report(clsPrmInventory prmINV, DBContext dc)
        {

            List<rcItemTransfermation> cRptList = new List<rcItemTransfermation>();

            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();
                decimal OP_BALANCE = Conversion.DBNullDecimalToZero(GetItemOPBalance(prmINV, null));
                decimal runningbalance = 0;
                DBQuery dbq = new DBQuery();
                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;
                sb.Append(" SELECT ");
                sb.Append(" isd.INV_TRANS_TYPE_ID, isd.ITEM_STK_DET_ID,isd.TRANS_DATE,inv.ITEM_ID,inv.ITEM_NAME,U.UOM_CODE_SHORT UOM_NAME,isd.TRANS_REF_NO,isd.TRANS_REMARKS,isd.RCV_QTY,isd.ISS_QTY,0 CLOSING_QTY,0 OPENING_QTY ");
                sb.Append(" FROM ITEM_STOCK_DETAILS isd ");
                sb.Append(" INNER JOIN INV_ITEM_MASTER inv ON isd.ITEM_ID=inv.ITEM_ID ");
                sb.Append(" LEFT JOIN UOM_INFO U ON isd.UOM_ID=U.UOM_ID ");
                sb.Append(" WHERE 1=1   ");
                //AND isd.INV_TRANS_TYPE_ID NOT IN (1024,9008,1013)
                if (prmINV.item_id > 0)
                {
                    sb.Append("  AND isd.item_id  = @pitem_id ");
                    cmdInfo.DBParametersInfo.Add("@pitem_id", prmINV.item_id);
                }
                else
                {
                    sb.Append("  AND isd.item_id  = 0 ");
                }

                if (prmINV.fromProdDate != "")
                {
                    sb.Append("  AND isd.TRANS_DATE  >= @PTRANS_DATE ");
                    cmdInfo.DBParametersInfo.Add("@PTRANS_DATE", prmINV.fromProdDate);
                }

                if (prmINV.toProdDate != "")
                {
                    sb.Append("  AND isd.TRANS_DATE  <= @PtoTRANS_DATE ");
                    cmdInfo.DBParametersInfo.Add("@PtoTRANS_DATE", prmINV.toProdDate);
                }

                if (prmINV.From_Dept_Id > 0)
                {
                    sb.Append("  AND isd.DEPARTMENT_ID  = @pDEPARTMENT_ID ");
                    cmdInfo.DBParametersInfo.Add("@pDEPARTMENT_ID", prmINV.From_Dept_Id);
                }
                sb.Append("  order by isd.TRANS_DATE,isd.ITEM_STK_DET_ID asc ");
                DBQuery dbqtemp = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);

                foreach (DataRow dRow in dtData.Rows)
                {
                    rcItemTransfermation stk = new rcItemTransfermation();
                    //stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    //stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();
                    stk.INV_TRANS_TYPE_ID = Conversion.DBNullIntToZero(dRow["INV_TRANS_TYPE_ID"]);
                    stk.ITEM_STK_DET_ID = Conversion.DBNullIntToZero(dRow["ITEM_STK_DET_ID"]);
                    stk.TRANS_DATE = Conversion.DBNullDateToNull(dRow["TRANS_DATE"]);
                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();
                    //stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.TRANS_REF_NO = dRow["TRANS_REF_NO"].ToString();
                    stk.TRANS_REMARKS = dRow["TRANS_REMARKS"].ToString();
                    stk.RCV_QTY = Conversion.DBNullDecimalToZero(dRow["RCV_QTY"]);
                    stk.ISS_QTY = Conversion.DBNullDecimalToZero(dRow["ISS_QTY"]);
                    stk.OPENING_QTY = OP_BALANCE;
                        //Conversion.DBNullDecimalToZero(dRow["OPENING_QTY"]);
                    stk.CLOSING_QTY = Conversion.DBNullDecimalToZero(dRow["CLOSING_QTY"]);

                    if(prmINV.From_Dept_Id==54)
                    {
                        if (!(prmINV.From_Dept_Id == 54 && (stk.INV_TRANS_TYPE_ID == 1001 || stk.INV_TRANS_TYPE_ID == 1024 || stk.INV_TRANS_TYPE_ID == 9008)))
                         runningbalance = runningbalance + OP_BALANCE + stk.RCV_QTY - stk.ISS_QTY;
                    }
                    else if (prmINV.From_Dept_Id == 136)
                    { 
                    if (!(prmINV.From_Dept_Id == 136 && (stk.INV_TRANS_TYPE_ID == 1009 )))
                        runningbalance = runningbalance + OP_BALANCE + stk.RCV_QTY - stk.ISS_QTY;
                    }
                    else
                    {
                        runningbalance = runningbalance + OP_BALANCE + stk.RCV_QTY - stk.ISS_QTY;
                    }

                    OP_BALANCE = 0;
                    stk.RUNNING_QTY = runningbalance;
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

        #endregion

        #region ********************************************** Item Stock Ledger for Store*********************************************

        public static string GetStoreItemOPBalance(clsPrmInventory prmINV, DBContext dc)
        {
            bool isDCInit = false;
            string _vBalance = string.Empty;
            try
            {

                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                DateTime dt = Convert.ToDateTime(prmINV.fromProdDate);
                //FN_GET_CLOSING_QTY_NORMAL_DEPT(@FromDate,@ToDate,@itemID, @deptID)
                string abbr = " SELECT GET_CLOSING_QTY_WITHIN_DATE(@itemID,@FromDate ) vbalance FROM DUAL  ";
                cmdInfo.DBParametersInfo.Add("@itemID", prmINV.item_id);
                //cmdInfo.DBParametersInfo.Add("@deptID", prmINV.From_Dept_Id);
                cmdInfo.DBParametersInfo.Add("@FromDate", Convert.ToDateTime(prmINV.fromProdDate));
                //cmdInfo.DBParametersInfo.Add("@ToDate", prmINV.toProdDate);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                _vBalance = DBQuery.ExecuteDBScalar(dbq, dc).ToString();

            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return _vBalance;
        }


        public static List<rcItemTransfermation> Item_Store_Item_Ledger_Report(clsPrmInventory prmINV, DBContext dc)
        {

            List<rcItemTransfermation> cRptList = new List<rcItemTransfermation>();

            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();
                decimal OP_BALANCE = Conversion.DBNullDecimalToZero(GetStoreItemOPBalance(prmINV, null));
                decimal runningbalance = 0;
                DBQuery dbq = new DBQuery();
                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;

                sb.Append(" SELECT 1 SL_NO,a.PURCHASE_ID INV_TRANS_TYPE_ID,0 ITEM_STK_DET_ID,a.PURCHASE_TIME TRANS_DATE,inv.ITEM_ID,inv.ITEM_NAME,U.UOM_CODE_SHORT UOM_NAME,a.PURCHASE_NO TRANS_REF_NO,'Purchase LP' TRANS_REMARKS,0 MRR_QTY,b.PURCHASE_QTY,0 RCV_QTY,0 ISS_QTY,0 CLOSING_QTY,0 OPENING_QTY FROM LP_PURCHASE_MASTER a  ");
                 sb.Append(" INNER JOIN LP_PURCHASE_DETAILS b ON a.PURCHASE_ID=b.PURCHASE_ID ");
                 sb.Append(" INNER JOIN INV_ITEM_MASTER inv ON b.ITEM_ID=inv.ITEM_ID  ");
                 sb.Append(" LEFT JOIN UOM_INFO U ON b.UOM_ID=U.UOM_ID  ");
                 sb.Append(" WHERE 1=1 ");

                 if (prmINV.item_id > 0)
                 {
                     sb.Append("  AND b.ITEM_ID  = @pitem_id ");
                     cmdInfo.DBParametersInfo.Add("@pitem_id", prmINV.item_id);
                 }
                 else
                 {
                     sb.Append("  AND b.ITEM_ID  = 0 ");
                 }

                 if (prmINV.fromProdDate != "")
                 {
                     sb.Append("  AND a.PURCHASE_DATE  >= @PTRANS_DATE ");
                     cmdInfo.DBParametersInfo.Add("@PTRANS_DATE", prmINV.fromProdDate);
                 }

                 if (prmINV.toProdDate != "")
                 {
                     sb.Append("  AND a.PURCHASE_DATE  <= @PtoTRANS_DATE ");
                     cmdInfo.DBParametersInfo.Add("@PtoTRANS_DATE", prmINV.toProdDate);
                 }   
                 
                sb.Append(" UNION ALL ");

                sb.Append(" SELECT 2 SL_NO,a.IMP_PURCHASE_ID INV_TRANS_TYPE_ID,0 ITEM_STK_DET_ID,a.IMP_PURCHASE_TIME TRANS_DATE,inv.ITEM_ID,inv.ITEM_NAME,U.UOM_CODE_SHORT UOM_NAME,a.IMP_PURCHASE_NO TRANS_REF_NO,'Purchase Against LC' TRANS_REMARKS,0 MRR_QTY,b.PUR_QTY PURCHASE_QTY,0 RCV_QTY,0 ISS_QTY,0 CLOSING_QTY,0 OPENING_QTY FROM IMP_PURCHASE_MASTER a  ");
                sb.Append(" INNER JOIN IMP_PURCHASE_DETAILS b ON a.IMP_PURCHASE_ID=b.IMP_PURCHASE_ID ");
                sb.Append(" INNER JOIN INV_ITEM_MASTER inv ON b.ITEM_ID=inv.ITEM_ID  ");
                sb.Append(" LEFT JOIN UOM_INFO U ON b.UOM_ID=U.UOM_ID  ");
                sb.Append(" WHERE 1=1 ");

                if (prmINV.item_id > 0)
                {
                    sb.Append("  AND b.ITEM_ID  = @pitem_id ");
                    cmdInfo.DBParametersInfo.Add("@pitem_id", prmINV.item_id);
                }
                else
                {
                    sb.Append("  AND b.ITEM_ID  = 0 ");
                }

                if (prmINV.fromProdDate != "")
                {
                    sb.Append("  AND a.IMP_PURCHASE_DATE  >= @PTRANS_DATE ");
                    cmdInfo.DBParametersInfo.Add("@PTRANS_DATE", prmINV.fromProdDate);
                }

                if (prmINV.toProdDate != "")
                {
                    sb.Append("  AND a.IMP_PURCHASE_DATE  <= @PtoTRANS_DATE ");
                    cmdInfo.DBParametersInfo.Add("@PtoTRANS_DATE", prmINV.toProdDate);
                }          
                 
                sb.Append(" UNION ALL ");

                sb.Append(" SELECT 3 SL_NO,a.MRR_ID INV_TRANS_TYPE_ID,0 ITEM_STK_DET_ID,a.MRR_TIME TRANS_DATE,inv.ITEM_ID,inv.ITEM_NAME,U.UOM_CODE_SHORT UOM_NAME,a.MRR_NO TRANS_REF_NO,'MRR Against PO' TRANS_REMARKS,b.MRR_QTY,0 PURCHASE_QTY,0 RCV_QTY,0 ISS_QTY,0 CLOSING_QTY,0 OPENING_QTY FROM MRR_MASTER a  ");
                sb.Append(" INNER JOIN MRR_DETAILS b ON a.MRR_ID=b.MRR_ID ");
                sb.Append(" INNER JOIN INV_ITEM_MASTER inv ON b.ITEM_ID=inv.ITEM_ID  ");
                sb.Append(" LEFT JOIN UOM_INFO U ON b.UOM_ID=U.UOM_ID  ");
                sb.Append(" WHERE 1=1  ");
                if (prmINV.item_id > 0)
                {
                    sb.Append("  AND b.ITEM_ID  = @pitem_id ");
                    cmdInfo.DBParametersInfo.Add("@pitem_id", prmINV.item_id);
                }
                else
                {
                    sb.Append("  AND b.ITEM_ID  = 0 ");
                }

                if (prmINV.fromProdDate != "")
                {
                    sb.Append("  AND a.MRR_DATE  >= @PTRANS_DATE ");
                    cmdInfo.DBParametersInfo.Add("@PTRANS_DATE", prmINV.fromProdDate);
                }

                if (prmINV.toProdDate != "")
                {
                    sb.Append("  AND a.MRR_DATE  <= @PtoTRANS_DATE ");
                    cmdInfo.DBParametersInfo.Add("@PtoTRANS_DATE", prmINV.toProdDate);
                }               
                 
                 
                sb.Append(" UNION ALL  ");

                sb.Append(" SELECT 4 SL_NO,isd.INV_TRANS_TYPE_ID, isd.ITEM_STK_DET_ID,isd.TRANS_TIME TRANS_DATE,inv.ITEM_ID,inv.ITEM_NAME,U.UOM_CODE_SHORT UOM_NAME,isd.TRANS_REF_NO,isd.TRANS_REMARKS,0 MRR_QTY,0 PURCHASE_QTY,isd.RCV_QTY,isd.ISS_QTY,0 CLOSING_QTY,0 OPENING_QTY  ");
                sb.Append(" FROM ITEM_STOCK_DETAILS isd  ");
                sb.Append(" INNER JOIN INV_ITEM_MASTER inv ON isd.ITEM_ID=inv.ITEM_ID  ");
                sb.Append(" LEFT JOIN UOM_INFO U ON isd.UOM_ID=U.UOM_ID  ");
                sb.Append(" WHERE 1=1 AND isd.STORE_ID=1   ");
                 if (prmINV.item_id > 0)
                {
                    sb.Append("  AND isd.item_id  = @pitem_id ");
                    cmdInfo.DBParametersInfo.Add("@pitem_id", prmINV.item_id);
                }
                else
                {
                    sb.Append("  AND isd.item_id  = 0 ");
                }

                if (prmINV.fromProdDate != "")
                {
                    sb.Append("  AND isd.TRANS_DATE  >= @PTRANS_DATE ");
                    cmdInfo.DBParametersInfo.Add("@PTRANS_DATE", prmINV.fromProdDate);
                }

                if (prmINV.toProdDate != "")
                {
                    sb.Append("  AND isd.TRANS_DATE  <= @PtoTRANS_DATE ");
                    cmdInfo.DBParametersInfo.Add("@PtoTRANS_DATE", prmINV.toProdDate);
                }

                if (prmINV.From_Dept_Id > 0)
                {
                    sb.Append("  AND isd.DEPARTMENT_ID  = @pDEPARTMENT_ID ");
                    cmdInfo.DBParametersInfo.Add("@pDEPARTMENT_ID", prmINV.From_Dept_Id);
                }
                sb.Append("  order by TRANS_DATE,SL_NO ");

                //sb.Append(" SELECT ");
                //sb.Append(" isd.INV_TRANS_TYPE_ID, isd.ITEM_STK_DET_ID,isd.TRANS_DATE,inv.ITEM_ID,inv.ITEM_NAME,U.UOM_CODE_SHORT UOM_NAME,isd.TRANS_REF_NO,isd.TRANS_REMARKS,isd.RCV_QTY,isd.ISS_QTY,0 CLOSING_QTY,0 OPENING_QTY ");
                //sb.Append(" FROM ITEM_STOCK_DETAILS isd ");
                //sb.Append(" INNER JOIN INV_ITEM_MASTER inv ON isd.ITEM_ID=inv.ITEM_ID ");
                //sb.Append(" LEFT JOIN UOM_INFO U ON isd.UOM_ID=U.UOM_ID ");
                //sb.Append(" WHERE 1=1 AND isd.STORE_ID=1  ");
                ////AND isd.INV_TRANS_TYPE_ID NOT IN (1024,9008,1013)
                //if (prmINV.item_id > 0)
                //{
                //    sb.Append("  AND isd.item_id  = @pitem_id ");
                //    cmdInfo.DBParametersInfo.Add("@pitem_id", prmINV.item_id);
                //}
                //else
                //{
                //    sb.Append("  AND isd.item_id  = 0 ");
                //}

                //if (prmINV.fromProdDate != "")
                //{
                //    sb.Append("  AND isd.TRANS_DATE  >= @PTRANS_DATE ");
                //    cmdInfo.DBParametersInfo.Add("@PTRANS_DATE", prmINV.fromProdDate);
                //}

                //if (prmINV.toProdDate != "")
                //{
                //    sb.Append("  AND isd.TRANS_DATE  <= @PtoTRANS_DATE ");
                //    cmdInfo.DBParametersInfo.Add("@PtoTRANS_DATE", prmINV.toProdDate);
                //}

                //if (prmINV.From_Dept_Id > 0)
                //{
                //    sb.Append("  AND isd.DEPARTMENT_ID  = @pDEPARTMENT_ID ");
                //    cmdInfo.DBParametersInfo.Add("@pDEPARTMENT_ID", prmINV.From_Dept_Id);
                //}
                //sb.Append("  order by isd.TRANS_DATE,isd.ITEM_STK_DET_ID asc ");
                DBQuery dbqtemp = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);

                foreach (DataRow dRow in dtData.Rows)
                {
                    rcItemTransfermation stk = new rcItemTransfermation();
                    //stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    //stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();
                    stk.SL_NO = Conversion.DBNullIntToZero(dRow["SL_NO"]);
                    stk.INV_TRANS_TYPE_ID = Conversion.DBNullIntToZero(dRow["INV_TRANS_TYPE_ID"]);
                    stk.ITEM_STK_DET_ID = Conversion.DBNullIntToZero(dRow["ITEM_STK_DET_ID"]);
                    stk.TRANS_DATE = Conversion.DBNullDateToNull(dRow["TRANS_DATE"]);
                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();
                    //stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.TRANS_REF_NO = dRow["TRANS_REF_NO"].ToString();
                    stk.TRANS_REMARKS = dRow["TRANS_REMARKS"].ToString();
                    stk.PURCHASE_QTY = Conversion.DBNullDecimalToZero(dRow["PURCHASE_QTY"]);
                    stk.MRR_QTY = Conversion.DBNullDecimalToZero(dRow["MRR_QTY"]);
                    stk.RCV_QTY = Conversion.DBNullDecimalToZero(dRow["RCV_QTY"]);
                    stk.ISS_QTY = Conversion.DBNullDecimalToZero(dRow["ISS_QTY"]);
                    stk.OPENING_QTY = OP_BALANCE;
                    //Conversion.DBNullDecimalToZero(dRow["OPENING_QTY"]);
                    stk.CLOSING_QTY = Conversion.DBNullDecimalToZero(dRow["CLOSING_QTY"]);

                    if (prmINV.From_Dept_Id == 54)
                    {
                        if (!(prmINV.From_Dept_Id == 54 && (stk.INV_TRANS_TYPE_ID == 1001 || stk.INV_TRANS_TYPE_ID == 1024 || stk.INV_TRANS_TYPE_ID == 9008)))
                            runningbalance = runningbalance + OP_BALANCE + stk.RCV_QTY - stk.ISS_QTY;
                    }
                    else if (prmINV.From_Dept_Id == 136)
                    {
                        if (!(prmINV.From_Dept_Id == 136 && (stk.INV_TRANS_TYPE_ID == 1009)))
                            runningbalance = runningbalance + OP_BALANCE + stk.RCV_QTY - stk.ISS_QTY;
                    }
                    else
                    {
                        runningbalance = runningbalance + OP_BALANCE + stk.RCV_QTY - stk.ISS_QTY;
                    }

                    OP_BALANCE = 0;
                    stk.RUNNING_QTY = runningbalance;
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


        public static List<rcMaterialStock> Production_Item_Ledger_Report(clsPrmInventory prmINV, DBContext dc)
        {
              List<rcMaterialStock> cRptList = new List<rcMaterialStock>();


            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();

                if (prmINV.ToDate.HasValue)
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.ToDate.Value);
                }
                cmdInfo.DBParametersInfo.Add(":p_itemtype_id", prmINV.itemtype_id);
                cmdInfo.DBParametersInfo.Add(":p_itemGroup_id", prmINV.itemGroup_id);
                cmdInfo.DBParametersInfo.Add(":p_itemclass_id", prmINV.ItemClassId);
                cmdInfo.DBParametersInfo.Add(":p_item_id", prmINV.item_id);
                cmdInfo.DBParametersInfo.Add(":p_dept_id", prmINV.DeptID);
                cmdInfo.DBParametersInfo.Add(":p_stlm_id", prmINV.STLM_ID);


                //cmdInfo.DBParametersInfo.Add(":P_ITEM_CODE", prmSND.Item_Code);
                DBQuery dbq = new DBQuery();

                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                //dbq.CommandTimeout = 1000;
                cmdInfo.CommandTimeout = 600;


                //this is previous item stock report
                //cmdInfo.CommandText = "SP_DEPARTMENT_STOCK_REPORT";

                //SP_DEPT_ASSIGND_ITEM_STOCK
                //but this is new sp replacing previous department stock report
                cmdInfo.CommandText = "SP_ITEM_LEDGER_STOCK";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);


                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;
                //sb.Append(" SELECT * FROM TEMP_ITEM_STOCK_BAT_FOR_NET ");

                sb.Append(" Select ITEM_GROUP_ID,ITEM_GROUP_NAME,ITEM_ID,ITEM_CODE,ITEM_NAME,UOM_ID,UOM_NAME,ITEM_TYPE_ID,ITEM_TYPE_NAME,ITEM_CLASS_ID,ITEM_CLASS_NAME,OP_IRR_QTY,OP_ITC_QTY,IRR_DEPT_QTY,IRR_STORE_QTY,IRR_PROD_QTY,ITC_DEPT_QTY,ITC_STORE_QTY,ITC_PROD_QTY,ADJUST_QTY,REJECT_QTY,OP_REJECT_QTY,GRID_REJECT_QTY,OP_GRID_REJECT_QTY,STLM_ID,STLM_NAME,PROCESS_LOSS_QTY,CONV_LEAD_QTY FROM TEMP_DEPARTMENT_ITEM_STOCK");
                sb.Append(" ORDER BY STLM_ID,ITEM_CODE ");

                DBQuery dbqtemp = new DBQuery();
                //DBQuery dbq = new DBQuery();


                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                //DBQuery.ExecuteDBQuerySP(dbq, dc);
                DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);

                //,,,,,,,,,,,,,,,,,


                foreach (DataRow dRow in dtData.Rows)
                {
                    rcMaterialStock stk = new rcMaterialStock();
                    stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();

                    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);

                    stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                    stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();
                    stk.OP_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_IRR_QTY"]);
                    stk.OP_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ITC_QTY"]);
                    stk.OP_REJECT_QTY = Conversion.DBNullDecimalToZero(dRow["OP_REJECT_QTY"]);
                    stk.OP_GRID_REJECT_QTY = Conversion.DBNullDecimalToZero(dRow["OP_GRID_REJECT_QTY"]);
                    stk.OPPENING_BAL_QTY = stk.OP_IRR_QTY - stk.OP_ITC_QTY - stk.OP_REJECT_QTY - stk.OP_GRID_REJECT_QTY;

                    stk.IRR_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_DEPT_QTY"]);
                    stk.IRR_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_STORE_QTY"]);
                    stk.IRR_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_PROD_QTY"]);
                    stk.CONV_LEAD_QTY = Conversion.DBNullDecimalToZero(dRow["CONV_LEAD_QTY"]);

                    stk.REJECT_QTY = Conversion.DBNullDecimalToZero(dRow["REJECT_QTY"]);
                    stk.GRID_REJECT_QTY = Conversion.DBNullDecimalToZero(dRow["GRID_REJECT_QTY"]);

                    stk.PROCESS_LOSS_QTY = Conversion.DBNullDecimalToZero(dRow["PROCESS_LOSS_QTY"]);
                  

                    //here total IRR quantity
                    stk.IRR_BAL_QTY = stk.IRR_DEPT_QTY + stk.IRR_STORE_QTY + stk.IRR_PROD_QTY + stk.CONV_LEAD_QTY;

                    stk.ITC_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_DEPT_QTY"]);
                    stk.ITC_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_STORE_QTY"]);
                    stk.ITC_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_PROD_QTY"]);

                    //here total ITC quantity
                    stk.ITC_BAL_QTY = stk.ITC_DEPT_QTY + stk.ITC_STORE_QTY + stk.ITC_PROD_QTY;

                    //here total Adjust balance quantity
                    stk.ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);
                    stk.STLM_ID = Conversion.DBNullIntToZero(dRow["STLM_ID"]);
                    stk.STLM_NAME = dRow["STLM_NAME"].ToString();


                    stk.DEPARTMENT_NAME = prmINV.From_Dept_Name;
                    // stk.BAL_QTY = Conversion.DBNullDecimalToZero(dRow["BAL_QTY"]);
                    stk.COLISING_QTY = (stk.OPPENING_BAL_QTY + stk.ADJUST_QTY + stk.IRR_BAL_QTY) - stk.ITC_BAL_QTY - stk.REJECT_QTY - stk.GRID_REJECT_QTY;

                    if(stk.GRID_REJECT_QTY > 0 && stk.REJECT_QTY == 0)
                    {
                        stk.REJECT_QTY = stk.GRID_REJECT_QTY;
                    }

                    //cRptList.Add(stk); comment out by mamun 01-mar-2022
                    // open from comment out by mamun 01-mar-2022
                    if (stk.OPPENING_BAL_QTY == 0)
                    {
                        if (stk.IRR_BAL_QTY != 0 || stk.ITC_BAL_QTY != 0 || stk.COLISING_QTY != 0)
                        {
                            cRptList.Add(stk);
                        }
                    }
                    else
                    {
                        cRptList.Add(stk);
                    }
                    //end comment out 01-mar-2022
                }

                //int COUNT = cRptList.Count;


            }
            //catch { throw; }
            //finally 
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }

            return cRptList;

            //List<rcItemTransfermation> cRptList = new List<rcItemTransfermation>();

            //bool isDCInit = false;
            ////try
            //{
            //    isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

            //    DBCommandInfo cmdInfo = new DBCommandInfo();
            //    StringBuilder sb = new StringBuilder();
            //    decimal OP_BALANCE = Conversion.DBNullDecimalToZero(GetStoreItemOPBalance(prmINV, null));
            //    decimal runningbalance = 0;
            //    DBQuery dbq = new DBQuery();
            //    DBCommandInfo cmdInfotemp = new DBCommandInfo();
            //    sb.Length = 0;

            //    sb.Append(" SELECT 1 SL_NO,a.PURCHASE_ID INV_TRANS_TYPE_ID,0 ITEM_STK_DET_ID,a.PURCHASE_TIME TRANS_DATE,inv.ITEM_ID,inv.ITEM_NAME,U.UOM_CODE_SHORT UOM_NAME,a.PURCHASE_NO TRANS_REF_NO,'Purchase LP' TRANS_REMARKS,0 MRR_QTY,b.PURCHASE_QTY,0 RCV_QTY,0 ISS_QTY,0 CLOSING_QTY,0 OPENING_QTY FROM LP_PURCHASE_MASTER a  ");
            //    sb.Append(" INNER JOIN LP_PURCHASE_DETAILS b ON a.PURCHASE_ID=b.PURCHASE_ID ");
            //    sb.Append(" INNER JOIN INV_ITEM_MASTER inv ON b.ITEM_ID=inv.ITEM_ID  ");
            //    sb.Append(" LEFT JOIN UOM_INFO U ON b.UOM_ID=U.UOM_ID  ");
            //    sb.Append(" WHERE 1=1 ");

            //    if (prmINV.item_id > 0)
            //    {
            //        sb.Append("  AND b.ITEM_ID  = @pitem_id ");
            //        cmdInfo.DBParametersInfo.Add("@pitem_id", prmINV.item_id);
            //    }
            //    else
            //    {
            //        sb.Append("  AND b.ITEM_ID  = 0 ");
            //    }

            //    if (prmINV.fromProdDate != "")
            //    {
            //        sb.Append("  AND a.PURCHASE_DATE  >= @PTRANS_DATE ");
            //        cmdInfo.DBParametersInfo.Add("@PTRANS_DATE", prmINV.fromProdDate);
            //    }

            //    if (prmINV.toProdDate != "")
            //    {
            //        sb.Append("  AND a.PURCHASE_DATE  <= @PtoTRANS_DATE ");
            //        cmdInfo.DBParametersInfo.Add("@PtoTRANS_DATE", prmINV.toProdDate);
            //    }

            //    sb.Append(" UNION ALL ");

            //    sb.Append(" SELECT 2 SL_NO,a.IMP_PURCHASE_ID INV_TRANS_TYPE_ID,0 ITEM_STK_DET_ID,a.IMP_PURCHASE_TIME TRANS_DATE,inv.ITEM_ID,inv.ITEM_NAME,U.UOM_CODE_SHORT UOM_NAME,a.IMP_PURCHASE_NO TRANS_REF_NO,'Purchase Against LC' TRANS_REMARKS,0 MRR_QTY,b.PUR_QTY PURCHASE_QTY,0 RCV_QTY,0 ISS_QTY,0 CLOSING_QTY,0 OPENING_QTY FROM IMP_PURCHASE_MASTER a  ");
            //    sb.Append(" INNER JOIN IMP_PURCHASE_DETAILS b ON a.IMP_PURCHASE_ID=b.IMP_PURCHASE_ID ");
            //    sb.Append(" INNER JOIN INV_ITEM_MASTER inv ON b.ITEM_ID=inv.ITEM_ID  ");
            //    sb.Append(" LEFT JOIN UOM_INFO U ON b.UOM_ID=U.UOM_ID  ");
            //    sb.Append(" WHERE 1=1 ");

            //    if (prmINV.item_id > 0)
            //    {
            //        sb.Append("  AND b.ITEM_ID  = @pitem_id ");
            //        cmdInfo.DBParametersInfo.Add("@pitem_id", prmINV.item_id);
            //    }
            //    else
            //    {
            //        sb.Append("  AND b.ITEM_ID  = 0 ");
            //    }

            //    if (prmINV.fromProdDate != "")
            //    {
            //        sb.Append("  AND a.IMP_PURCHASE_DATE  >= @PTRANS_DATE ");
            //        cmdInfo.DBParametersInfo.Add("@PTRANS_DATE", prmINV.fromProdDate);
            //    }

            //    if (prmINV.toProdDate != "")
            //    {
            //        sb.Append("  AND a.IMP_PURCHASE_DATE  <= @PtoTRANS_DATE ");
            //        cmdInfo.DBParametersInfo.Add("@PtoTRANS_DATE", prmINV.toProdDate);
            //    }

            //    sb.Append(" UNION ALL ");

            //    sb.Append(" SELECT 3 SL_NO,a.MRR_ID INV_TRANS_TYPE_ID,0 ITEM_STK_DET_ID,a.MRR_TIME TRANS_DATE,inv.ITEM_ID,inv.ITEM_NAME,U.UOM_CODE_SHORT UOM_NAME,a.MRR_NO TRANS_REF_NO,'MRR Against PO' TRANS_REMARKS,b.MRR_QTY,0 PURCHASE_QTY,0 RCV_QTY,0 ISS_QTY,0 CLOSING_QTY,0 OPENING_QTY FROM MRR_MASTER a  ");
            //    sb.Append(" INNER JOIN MRR_DETAILS b ON a.MRR_ID=b.MRR_ID ");
            //    sb.Append(" INNER JOIN INV_ITEM_MASTER inv ON b.ITEM_ID=inv.ITEM_ID  ");
            //    sb.Append(" LEFT JOIN UOM_INFO U ON b.UOM_ID=U.UOM_ID  ");
            //    sb.Append(" WHERE 1=1  ");
            //    if (prmINV.item_id > 0)
            //    {
            //        sb.Append("  AND b.ITEM_ID  = @pitem_id ");
            //        cmdInfo.DBParametersInfo.Add("@pitem_id", prmINV.item_id);
            //    }
            //    else
            //    {
            //        sb.Append("  AND b.ITEM_ID  = 0 ");
            //    }

            //    if (prmINV.fromProdDate != "")
            //    {
            //        sb.Append("  AND a.MRR_DATE  >= @PTRANS_DATE ");
            //        cmdInfo.DBParametersInfo.Add("@PTRANS_DATE", prmINV.fromProdDate);
            //    }

            //    if (prmINV.toProdDate != "")
            //    {
            //        sb.Append("  AND a.MRR_DATE  <= @PtoTRANS_DATE ");
            //        cmdInfo.DBParametersInfo.Add("@PtoTRANS_DATE", prmINV.toProdDate);
            //    }


            //    sb.Append(" UNION ALL  ");

            //    sb.Append(" SELECT 4 SL_NO,isd.INV_TRANS_TYPE_ID, isd.ITEM_STK_DET_ID,isd.TRANS_TIME TRANS_DATE,inv.ITEM_ID,inv.ITEM_NAME,U.UOM_CODE_SHORT UOM_NAME,isd.TRANS_REF_NO,isd.TRANS_REMARKS,0 MRR_QTY,0 PURCHASE_QTY,isd.RCV_QTY,isd.ISS_QTY,0 CLOSING_QTY,0 OPENING_QTY  ");
            //    sb.Append(" FROM ITEM_STOCK_DETAILS isd  ");
            //    sb.Append(" INNER JOIN INV_ITEM_MASTER inv ON isd.ITEM_ID=inv.ITEM_ID  ");
            //    sb.Append(" LEFT JOIN UOM_INFO U ON isd.UOM_ID=U.UOM_ID  ");
            //    sb.Append(" WHERE 1=1 AND isd.STORE_ID=1   ");
            //    if (prmINV.item_id > 0)
            //    {
            //        sb.Append("  AND isd.item_id  = @pitem_id ");
            //        cmdInfo.DBParametersInfo.Add("@pitem_id", prmINV.item_id);
            //    }
            //    else
            //    {
            //        sb.Append("  AND isd.item_id  = 0 ");
            //    }

            //    if (prmINV.fromProdDate != "")
            //    {
            //        sb.Append("  AND isd.TRANS_DATE  >= @PTRANS_DATE ");
            //        cmdInfo.DBParametersInfo.Add("@PTRANS_DATE", prmINV.fromProdDate);
            //    }

            //    if (prmINV.toProdDate != "")
            //    {
            //        sb.Append("  AND isd.TRANS_DATE  <= @PtoTRANS_DATE ");
            //        cmdInfo.DBParametersInfo.Add("@PtoTRANS_DATE", prmINV.toProdDate);
            //    }

            //    if (prmINV.From_Dept_Id > 0)
            //    {
            //        sb.Append("  AND isd.DEPARTMENT_ID  = @pDEPARTMENT_ID ");
            //        cmdInfo.DBParametersInfo.Add("@pDEPARTMENT_ID", prmINV.From_Dept_Id);
            //    }
            //    sb.Append("  order by TRANS_DATE,SL_NO ");

            //    //sb.Append(" SELECT ");
            //    //sb.Append(" isd.INV_TRANS_TYPE_ID, isd.ITEM_STK_DET_ID,isd.TRANS_DATE,inv.ITEM_ID,inv.ITEM_NAME,U.UOM_CODE_SHORT UOM_NAME,isd.TRANS_REF_NO,isd.TRANS_REMARKS,isd.RCV_QTY,isd.ISS_QTY,0 CLOSING_QTY,0 OPENING_QTY ");
            //    //sb.Append(" FROM ITEM_STOCK_DETAILS isd ");
            //    //sb.Append(" INNER JOIN INV_ITEM_MASTER inv ON isd.ITEM_ID=inv.ITEM_ID ");
            //    //sb.Append(" LEFT JOIN UOM_INFO U ON isd.UOM_ID=U.UOM_ID ");
            //    //sb.Append(" WHERE 1=1 AND isd.STORE_ID=1  ");
            //    ////AND isd.INV_TRANS_TYPE_ID NOT IN (1024,9008,1013)
            //    //if (prmINV.item_id > 0)
            //    //{
            //    //    sb.Append("  AND isd.item_id  = @pitem_id ");
            //    //    cmdInfo.DBParametersInfo.Add("@pitem_id", prmINV.item_id);
            //    //}
            //    //else
            //    //{
            //    //    sb.Append("  AND isd.item_id  = 0 ");
            //    //}

            //    //if (prmINV.fromProdDate != "")
            //    //{
            //    //    sb.Append("  AND isd.TRANS_DATE  >= @PTRANS_DATE ");
            //    //    cmdInfo.DBParametersInfo.Add("@PTRANS_DATE", prmINV.fromProdDate);
            //    //}

            //    //if (prmINV.toProdDate != "")
            //    //{
            //    //    sb.Append("  AND isd.TRANS_DATE  <= @PtoTRANS_DATE ");
            //    //    cmdInfo.DBParametersInfo.Add("@PtoTRANS_DATE", prmINV.toProdDate);
            //    //}

            //    //if (prmINV.From_Dept_Id > 0)
            //    //{
            //    //    sb.Append("  AND isd.DEPARTMENT_ID  = @pDEPARTMENT_ID ");
            //    //    cmdInfo.DBParametersInfo.Add("@pDEPARTMENT_ID", prmINV.From_Dept_Id);
            //    //}
            //    //sb.Append("  order by isd.TRANS_DATE,isd.ITEM_STK_DET_ID asc ");
            //    DBQuery dbqtemp = new DBQuery();
            //    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
            //    cmdInfo.CommandTimeout = 600;

            //    cmdInfo.CommandText = sb.ToString();
            //    cmdInfo.CommandType = CommandType.Text;
            //    dbq.DBCommandInfo = cmdInfo;
            //    DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);

            //    foreach (DataRow dRow in dtData.Rows)
            //    {
            //        rcItemTransfermation stk = new rcItemTransfermation();
            //        //stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
            //        //stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();
            //        stk.SL_NO = Conversion.DBNullIntToZero(dRow["SL_NO"]);
            //        stk.INV_TRANS_TYPE_ID = Conversion.DBNullIntToZero(dRow["INV_TRANS_TYPE_ID"]);
            //        stk.ITEM_STK_DET_ID = Conversion.DBNullIntToZero(dRow["ITEM_STK_DET_ID"]);
            //        stk.TRANS_DATE = Conversion.DBNullDateToNull(dRow["TRANS_DATE"]);
            //        stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
            //        stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();
            //        //stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
            //        stk.UOM_NAME = dRow["UOM_NAME"].ToString();
            //        stk.TRANS_REF_NO = dRow["TRANS_REF_NO"].ToString();
            //        stk.TRANS_REMARKS = dRow["TRANS_REMARKS"].ToString();
            //        stk.PURCHASE_QTY = Conversion.DBNullDecimalToZero(dRow["PURCHASE_QTY"]);
            //        stk.MRR_QTY = Conversion.DBNullDecimalToZero(dRow["MRR_QTY"]);
            //        stk.RCV_QTY = Conversion.DBNullDecimalToZero(dRow["RCV_QTY"]);
            //        stk.ISS_QTY = Conversion.DBNullDecimalToZero(dRow["ISS_QTY"]);
            //        stk.OPENING_QTY = OP_BALANCE;
            //        //Conversion.DBNullDecimalToZero(dRow["OPENING_QTY"]);
            //        stk.CLOSING_QTY = Conversion.DBNullDecimalToZero(dRow["CLOSING_QTY"]);

            //        if (prmINV.From_Dept_Id == 54)
            //        {
            //            if (!(prmINV.From_Dept_Id == 54 && (stk.INV_TRANS_TYPE_ID == 1001 || stk.INV_TRANS_TYPE_ID == 1024 || stk.INV_TRANS_TYPE_ID == 9008)))
            //                runningbalance = runningbalance + OP_BALANCE + stk.RCV_QTY - stk.ISS_QTY;
            //        }
            //        else if (prmINV.From_Dept_Id == 136)
            //        {
            //            if (!(prmINV.From_Dept_Id == 136 && (stk.INV_TRANS_TYPE_ID == 1009)))
            //                runningbalance = runningbalance + OP_BALANCE + stk.RCV_QTY - stk.ISS_QTY;
            //        }
            //        else
            //        {
            //            runningbalance = runningbalance + OP_BALANCE + stk.RCV_QTY - stk.ISS_QTY;
            //        }

            //        OP_BALANCE = 0;
            //        stk.RUNNING_QTY = runningbalance;
            //        cRptList.Add(stk);
            //    }

            //}
            ////catch { throw; }
            ////finally 
            //{
            //    DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            //}

            //return cRptList;

        }

        public static List<rcBomVsProduction> Production_Item_Ledger_ItemDtl(clsPrmInventory prmINV, DBContext dc)
        {
            List<rcBomVsProduction> cRptList = new List<rcBomVsProduction>();
            bool isDCInit = false;
            //try
            {

                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();
                cmdInfo.DBParametersInfo.Clear();
                sb.Length = 0;


                sb.Append("  SELECT FINISHED_ITEM_ID ,ITEM_CODE ,ITEM_NAME ,UOM_CODE ,CLOSING_ITEM_ID ,CLOSING_UOM_CODE ,ISSUE_QTY ,PRODUCTION_QTY ,USED_QTY_AS_BOM ,USE_DIFF ,ITEM_STANDARD_WEIGHT_KG ");
                sb.Append(" ,CASE WHEN ROUND(USE_DIFF,2)<>0 AND ROUND(USED_QTY_AS_BOM)<>0 THEN ROUND((ROUND(USE_DIFF)/ROUND(USED_QTY_AS_BOM))*100,2) else 0 END USE_PER ");
                sb.Append(" FROM ( ");
                sb.Append(" SELECT CLS.FINISHED_ITEM_ID,IM.ITEM_CODE,IM.ITEM_NAME,UOM.UOM_CODE,CLS.CLOSING_ITEM_ID,UOMCLS.UOM_CODE CLOSING_UOM_CODE,SUM(CLS.ISSUE_STOCK) ISSUE_QTY ");
                sb.Append(" ,CASE WHEN NVL(SUM(NVL(DTL.REJECT_QTY,0)),0) > 0 THEN (SUM(DTL.REJECT_QTY)+ SUM(DTL.ITEM_QTY)) ELSE SUM(DTL.ITEM_QTY) END PRODUCTION_QTY ");
                sb.Append(" ,CASE WHEN BU.UOM_CODE='Percent' THEN  (CASE WHEN SUM(NVL(DTL.MIXER_BATCH_QTY,0))>0  THEN SUM(NVL(DTL.MIXER_BATCH_QTY,0))*(bd.ITEM_QTY/100) else SUM(NVL(DTL.ITEM_QTY,0))*(bd.ITEM_QTY/100) END) ");
                sb.Append(" else SUM(NVL(DTL.ITEM_QTY,0)+NVL(DTL.REJECT_QTY,0))*(bd.ITEM_QTY+NVL(FN_GET_FALSE_LUG_WT(bm.BOM_ITEM_ID,bd.ITEM_ID),0))  END  USED_QTY_AS_BOM ");
                sb.Append(" ,SUM(CLS.ISSUE_STOCK) - CASE WHEN BU.UOM_CODE='Percent' THEN  (CASE WHEN SUM(NVL(DTL.MIXER_BATCH_QTY,0))>0  THEN SUM(NVL(DTL.MIXER_BATCH_QTY,0))*(bd.ITEM_QTY/100) else SUM(NVL(DTL.ITEM_QTY,0))*(bd.ITEM_QTY/100) END) ");
                sb.Append(" else SUM(NVL(DTL.ITEM_QTY,0)+NVL(DTL.REJECT_QTY,0))*(bd.ITEM_QTY+NVL(FN_GET_FALSE_LUG_WT(bm.BOM_ITEM_ID,bd.ITEM_ID),0))  END USE_DIFF ");
                sb.Append(" ,bd.ITEM_QTY ITEM_STANDARD_WEIGHT_KG ");
                sb.Append(" FROM PRODUCTION_FLOOR_CLOSING CLS ");
                sb.Append(" INNER JOIN PRODUCTION_DTL DTL ON CLS.PROD_MST_ID=DTL.PROD_MST_ID AND CLS.FINISHED_ITEM_ID=DTL.ITEM_ID AND CLS.MACHINE_ID=DTL.MACHINE_ID ");
                sb.Append(" INNER JOIN PRODUCTION_MST MST ON CLS.PROD_MST_ID=MST.PROD_ID ");
                sb.Append(" INNER JOIN INV_ITEM_MASTER IM ON DTL.ITEM_ID=IM.ITEM_ID ");
                sb.Append(" LEFT JOIN BOM_MST_T bm ON dtl.BOM_ID=bm.BOM_ID ");
                sb.Append(" LEFT JOIN BOM_DTL_T bd ON bm.BOM_ID=bd.BOM_MST_ID AND BD.ITEM_ID=CLS.CLOSING_ITEM_ID ");
                sb.Append(" INNER JOIN UOM_INFO UOM ON DTL.UOM_ID=UOM.UOM_ID ");
                sb.Append(" INNER JOIN UOM_INFO UOMCLS ON DTL.UOM_ID=UOMCLS.UOM_ID ");
                sb.Append(" INNER JOIN UOM_INFO BU ON bd.ITEM_UNIT_ID=BU.UOM_ID ");
                sb.Append(" WHERE 1=1 ");
                sb.Append(" AND MST.AUTH_STATUS='Y' ");



                if (prmINV.FromDate != null)
                {
                    sb.Append(" AND  MST.PRODUCTION_DATE BETWEEN  :FromDate and :ToDate ");

                    cmdInfo.DBParametersInfo.Add(":P_FromDate", prmINV.FromDate);
                    cmdInfo.DBParametersInfo.Add(":P_ToDate", prmINV.ToDate);

                }

                if (prmINV.item_id > 0)
                {
                    sb.Append(" AND cls.CLOSING_ITEM_ID=:Item_ID ");
                    cmdInfo.DBParametersInfo.Add(":Item_ID", prmINV.item_id);
                }



                sb.Append(" GROUP BY CLS.FINISHED_ITEM_ID,CLS.CLOSING_ITEM_ID,IM.ITEM_CODE,IM.ITEM_NAME,UOM.UOM_CODE,UOMCLS.UOM_CODE ,bd.ITEM_QTY,BU.UOM_CODE,bm.BOM_ITEM_ID,bd.ITEM_ID ");
                sb.Append(" ) A ");
             
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);

                foreach (DataRow dRow in dtData.Rows)
                {
                    rcBomVsProduction stk = new rcBomVsProduction();
                    //stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    //stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    //stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();
                    stk.ITEM_CODE = dRow["ITEM_CODE"].ToString();
                    stk.UOM_NAME = dRow["UOM_CODE"].ToString();
                    stk.RM_UOM_NAME = dRow["CLOSING_UOM_CODE"].ToString();
                    stk.RM_ACTUAL_QTY = Conversion.DBNullDecimalToZero(dRow["ISSUE_QTY"]);
                    stk.IRR_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["PRODUCTION_QTY"]);
                    stk.RM_STD_WT = Conversion.DBNullDecimalToZero(dRow["ITEM_STANDARD_WEIGHT_KG"]);
                    stk.USED_QTY_AS_BOM = Conversion.DBNullDecimalToZero(dRow["USED_QTY_AS_BOM"]);
                    stk.RM_USE_DIFF = Conversion.DBNullDecimalToZero(dRow["USE_DIFF"]);
                    stk.USE_PER = Conversion.DBNullDecimalToZero(dRow["USE_PER"]);

                    //here total IRR quantity
                    // stk.IRR_BAL_QTY = stk.IRR_DEPT_QTY + stk.IRR_STORE_QTY + stk.IRR_PROD_QTY;

                    // stk.ITC_DEPT_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_DEPT_QTY"]);
                    //stk.ITC_STORE_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_STORE_QTY"]);
                    //   stk.ITC_PROD_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_PROD_QTY"]);

                    //here total ITC quantity
                    // stk.ITC_BAL_QTY = stk.ITC_DEPT_QTY + stk.ITC_STORE_QTY + stk.ITC_PROD_QTY;

                    //here total Adjust balance quantity
                    //  stk.ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);

                    //  stk.DEPARTMENT_NAME = prmINV.From_Dept_Name;
                    //  stk.COLISING_QTY = (stk.OPPENING_BAL_QTY + stk.ADJUST_QTY + stk.IRR_BAL_QTY) - stk.ITC_BAL_QTY;

                    //BOM Raw material Part

                    //stk.RM_ITEM_ID = Conversion.DBNullIntToZero(dRow["RM_ITEM_ID"]);
                    //stk.RM_ITEM_NAME = dRow["RM_ITEM_NAME"].ToString();
                    // stk.RM_UOM_ID = Conversion.DBNullIntToZero(dRow["RM_UOM_ID"]);
                    //stk.RM_UOM_NAME = dRow["RM_UOM_CODE"].ToString();
                    //  stk.RM_ITEM_CODE = dRow["RM_ITEM_CODE"].ToString();


                    //stk.RM_ACTUAL_QTY = Conversion.DBNullDecimalToZero(dRow["RM_ACTUAL_USED"]);
                    //stk.STD_UNIT_QTY = Conversion.DBNullDecimalToZero(dRow["ITEM_STANDARD_WEIGHT_KG"]);
                    //stk.RM_STD_QTY = Conversion.DBNullDecimalToZero(dRow["USED_QTY_AS_BOM"]);


                    //stk.RM_WASTAGE_PERCENT = Conversion.DBNullDecimalToZero(dRow["ITEM_STANDARD_WEIGHT_KG"]);

                    //stk.RM_USE_DIFF = Conversion.DBNullDecimalToZero(dRow["USE_DIFF"]);
                    //stk.USE_PER = Conversion.DBNullDecimalToZero(dRow["USE_PER"]);

                    //stk.RM_ACTUAL_WASTAGE_PERCENT = Conversion.DBNullDecimalToZero(dRow["RM_ACTUAL_WASTAGE_PERCENT"]);








                    cRptList.Add(stk);

                }

            }
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }

            return cRptList;

        }

        #endregion




        #region -****************************** Pure Lead Consumption Department Wise *************************************************-
        public static List<dcMaterialStock> PureLead_RCV_ISSUE_DEPT(clsPrmInventory prmINV)
        {
            return PureLead_RCV_ISSUE_DEPT(prmINV, null);
        }

        public static List<dcMaterialStock> PureLead_RCV_ISSUE_DEPT(clsPrmInventory prmINV, DBContext dc)
        {

            List<dcMaterialStock> cRptList = new List<dcMaterialStock>();


            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();

                //if (prmINV.ToDate.HasValue)
                //{
                cmdInfo.DBParametersInfo.Add(":p_from_date", prmINV.FromTDate.Value);
                cmdInfo.DBParametersInfo.Add(":p_to_date", prmINV.ToTDate.Value);
                //}
                //cmdInfo.DBParametersInfo.Add(":p_itemtype_id", prmINV.itemtype_id);
                //cmdInfo.DBParametersInfo.Add(":p_itemGroup_id", prmINV.itemGroup_id);
                //cmdInfo.DBParametersInfo.Add(":p_item_id", prmINV.item_id);
                //cmdInfo.DBParametersInfo.Add(":p_store_id", prmINV.store_id);
                //cmdInfo.DBParametersInfo.Add(":p_itemclass_id", prmINV.ItemClassId);
                //cmdInfo.DBParametersInfo.Add(":p_battery_type_id", prmINV.Battery_Type_ID);
                //cmdInfo.DBParametersInfo.Add(":p_is_repair", prmINV.Is_Repair);
                cmdInfo.DBParametersInfo.Add(":p_dept_id", prmINV.DeptID);
                //cmdInfo.DBParametersInfo.Add(":p_is_finished", prmINV.is_Finished);

                DBQuery dbq = new DBQuery();

                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = "SP_PURE_LEAD_STOCK_REPORT_MAIL";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);
                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;

                //sb.Append(" SELECT * FROM TEMP_MAIL_DEPT_STOCK ");
                sb.Append("  SELECT  DEPT_ID,DEPT_NAME,SUM(NVL(CURRENT_MONTH_QTY,0)) CURRENT_MONTH_QTY,SUM(NVL(STR_RCV_CURRENT_MONTH_QTY,0)) STR_RCV_CURRENT_MONTH_QTY,SUM(NVL(STR_RCV_CURRENT_MONTH_WEIGHT,0)) STR_RCV_CURRENT_MONTH_WEIGHT ,SUM(NVL(STR_RCV_TODAY_QTY,0)) STR_RCV_TODAY_QTY   ");
                sb.Append("  ,SUM(NVL(STR_RCV_TODAY_WEIGHT,0)) STR_RCV_TODAY_WEIGHT,SUM(NVL(STR_ISS_CURRENT_MONTH_QTY,0)) STR_ISS_CURRENT_MONTH_QTY,SUM(NVL(STR_ISS_CURRENT_MONTH_WEIGHT,0)) STR_ISS_CURRENT_MONTH_WEIGHT   ");
                sb.Append("  ,SUM(NVL(STR_ISS_TODAY_QTY,0))  STR_ISS_TODAY_QTY  ,SUM(NVL(STR_ISS_TODAY_WEIGHT,0))  STR_ISS_TODAY_WEIGHT from TEMP_MAIL_DEPT_STOCK  group by DEPT_ID,DEPT_NAME  ");

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
                    stk.STR_RCV_CURRENT_MONTH_QTY = Conversion.DBNullDecimalToZero(dRow["STR_RCV_CURRENT_MONTH_QTY"].ToString());
                    stk.STR_RCV_CURRENT_MONTH_WEIGHT = Conversion.DBNullDecimalToZero(dRow["STR_RCV_CURRENT_MONTH_WEIGHT"].ToString());
                    stk.STR_RCV_TODAY_QTY = Conversion.DBNullDecimalToZero(dRow["STR_RCV_TODAY_QTY"].ToString());
                    stk.STR_RCV_TODAY_WEIGHT = Conversion.DBNullDecimalToZero(dRow["STR_RCV_TODAY_WEIGHT"].ToString());

                    stk.STR_ISS_CURRENT_MONTH_QTY = Conversion.DBNullDecimalToZero(dRow["STR_ISS_CURRENT_MONTH_QTY"].ToString());
                    stk.STR_ISS_CURRENT_MONTH_WEIGHT = Conversion.DBNullDecimalToZero(dRow["STR_ISS_CURRENT_MONTH_WEIGHT"].ToString());
                    stk.STR_ISS_TODAY_QTY = Conversion.DBNullDecimalToZero(dRow["STR_ISS_TODAY_QTY"].ToString());
                    stk.STR_ISS_TODAY_WEIGHT = Conversion.DBNullDecimalToZero(dRow["STR_ISS_TODAY_WEIGHT"].ToString());
                    cRptList.Add(stk);
                }

                if (cRptList.Count > 0)
                {
                    dcMaterialStock stk = new dcMaterialStock();
                    stk.DEPARTMENT_NAME = "";
                    stk.UOM_NAME = "";
                    stk.DEPT_ISS_CURRENT_MONTH_QTY = 0;
                }

            }
            //catch { throw; }
            //finally 
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }

            return cRptList;

        }






        public static List<dcMaterialStock> PRODUCTION_RCV_LIST(clsPrmInventory prmINV)
        {
            return PRODUCTION_RCV_LIST(prmINV, null);
        }

        public static List<dcMaterialStock> PRODUCTION_RCV_LIST(clsPrmInventory prmINV, DBContext dc)
        {

            List<dcMaterialStock> cRptList = new List<dcMaterialStock>();


            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();

                //if (prmINV.ToDate.HasValue)
                //{
                cmdInfo.DBParametersInfo.Add(":p_from_date", prmINV.FromTDate.Value);
                cmdInfo.DBParametersInfo.Add(":p_to_date", prmINV.ToTDate.Value);
                //}
                //cmdInfo.DBParametersInfo.Add(":p_itemtype_id", prmINV.itemtype_id);
                //cmdInfo.DBParametersInfo.Add(":p_itemGroup_id", prmINV.itemGroup_id);
                //cmdInfo.DBParametersInfo.Add(":p_item_id", prmINV.item_id);
                //cmdInfo.DBParametersInfo.Add(":p_store_id", prmINV.store_id);
                //cmdInfo.DBParametersInfo.Add(":p_itemclass_id", prmINV.ItemClassId);
                //cmdInfo.DBParametersInfo.Add(":p_battery_type_id", prmINV.Battery_Type_ID);
                //cmdInfo.DBParametersInfo.Add(":p_is_repair", prmINV.Is_Repair);
                cmdInfo.DBParametersInfo.Add(":p_dept_id", prmINV.DeptID);
                //cmdInfo.DBParametersInfo.Add(":p_is_finished", prmINV.is_Finished);

                DBQuery dbq = new DBQuery();

                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = "SP_PRODUCTION_STOCK_MAIL";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);
                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;

                //sb.Append(" SELECT * FROM TEMP_MAIL_DEPT_STOCK ");
                sb.Append("  SELECT  DEPT_ID,DEPT_NAME,SUM(NVL(CURRENT_MONTH_QTY,0)) CURRENT_MONTH_QTY,SUM(NVL(STR_RCV_CURRENT_MONTH_QTY,0)) STR_RCV_CURRENT_MONTH_QTY,SUM(NVL(STR_RCV_CURRENT_MONTH_WEIGHT,0)) STR_RCV_CURRENT_MONTH_WEIGHT ,SUM(NVL(STR_RCV_TODAY_QTY,0)) STR_RCV_TODAY_QTY   ");
                sb.Append("  ,SUM(NVL(STR_RCV_TODAY_WEIGHT,0)) STR_RCV_TODAY_WEIGHT,SUM(NVL(STR_ISS_CURRENT_MONTH_QTY,0)) STR_ISS_CURRENT_MONTH_QTY,SUM(NVL(STR_ISS_CURRENT_MONTH_WEIGHT,0)) STR_ISS_CURRENT_MONTH_WEIGHT   ");
                sb.Append("  ,SUM(NVL(STR_ISS_TODAY_QTY,0))  STR_ISS_TODAY_QTY  ,SUM(NVL(STR_ISS_TODAY_WEIGHT,0))  STR_ISS_TODAY_WEIGHT ");
                sb.Append(" ,sum(NVL(ROUND(PROD_TODAY_QTY,0),0))  PROD_TODAY_QTY,sum(ROUND( NVL(PROD_TODAY_WEIGHT,0),0)   ) PROD_TODAY_WEIGHT,sum(ROUND(NVL(PROD_CURRENT_WEIGHT,0),0)) PROD_CURRENT_WEIGHT,sum(ROUND(NVL(PROD_CURRENT_QTY,0),0)) PROD_CURRENT_QTY ");
                sb.Append(" from TEMP_MAIL_DEPT_STOCK  group by DEPT_ID,DEPT_NAME  ");
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
                    stk.PROD_TODAY_QTY = Conversion.DBNullDecimalToZero(dRow["PROD_TODAY_QTY"].ToString());
                    stk.PROD_TODAY_WEIGHT = Conversion.DBNullDecimalToZero(dRow["PROD_TODAY_WEIGHT"].ToString());
                    stk.PROD_CURRENT_WEIGHT = Conversion.DBNullDecimalToZero(dRow["PROD_CURRENT_WEIGHT"].ToString());
                    stk.PROD_CURRENT_QTY = Conversion.DBNullDecimalToZero(dRow["PROD_CURRENT_QTY"].ToString());

                    //stk.STR_ISS_CURRENT_MONTH_QTY = Conversion.DBNullDecimalToZero(dRow["STR_ISS_CURRENT_MONTH_QTY"].ToString());
                    //stk.STR_ISS_CURRENT_MONTH_WEIGHT = Conversion.DBNullDecimalToZero(dRow["STR_ISS_CURRENT_MONTH_WEIGHT"].ToString());
                    //stk.STR_ISS_TODAY_QTY = Conversion.DBNullDecimalToZero(dRow["STR_ISS_TODAY_QTY"].ToString());
                    //stk.STR_ISS_TODAY_WEIGHT = Conversion.DBNullDecimalToZero(dRow["STR_ISS_TODAY_WEIGHT"].ToString());
                    cRptList.Add(stk);
                }

                if (cRptList.Count > 0)
                {
                    dcMaterialStock stk = new dcMaterialStock();
                    stk.DEPARTMENT_NAME = "";
                    stk.UOM_NAME = "";
                    stk.DEPT_ISS_CURRENT_MONTH_QTY = 0;
                }

            }
            //catch { throw; }
            //finally 
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }

            return cRptList;

        }


        public static List<dcMaterialStock> PureLead_Purchase_LIST(clsPrmInventory prmINV)
        {
            return PureLead_Purchase_LIST(prmINV, null);
        }

        public static List<dcMaterialStock> PureLead_Purchase_LIST(clsPrmInventory prmINV, DBContext dc)
        {

            List<dcMaterialStock> cRptList = new List<dcMaterialStock>();


            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();

                //if (prmINV.ToDate.HasValue)
                //{
                cmdInfo.DBParametersInfo.Add(":p_from_date", prmINV.FromTDate.Value);
                cmdInfo.DBParametersInfo.Add(":p_to_date", prmINV.ToTDate.Value);
                //}
                //cmdInfo.DBParametersInfo.Add(":p_itemtype_id", prmINV.itemtype_id);
                //cmdInfo.DBParametersInfo.Add(":p_itemGroup_id", prmINV.itemGroup_id);
                //cmdInfo.DBParametersInfo.Add(":p_item_id", prmINV.item_id);
                //cmdInfo.DBParametersInfo.Add(":p_store_id", prmINV.store_id);
                //cmdInfo.DBParametersInfo.Add(":p_itemclass_id", prmINV.ItemClassId);
                //cmdInfo.DBParametersInfo.Add(":p_battery_type_id", prmINV.Battery_Type_ID);
                //cmdInfo.DBParametersInfo.Add(":p_is_repair", prmINV.Is_Repair);
                cmdInfo.DBParametersInfo.Add(":p_dept_id", prmINV.DeptID);
                //cmdInfo.DBParametersInfo.Add(":p_is_finished", prmINV.is_Finished);

                DBQuery dbq = new DBQuery();

                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;

                cmdInfo.CommandText = "SP_PURE_LEAD_PUR_REPORT_MAIL";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);
                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;

                //sb.Append(" SELECT * FROM TEMP_MAIL_DEPT_STOCK ");
                sb.Append("  SELECT  ITEM_ID,ITEM_NAME,SUM(NVL(CURRENT_MONTH_QTY,0)) CURRENT_MONTH_QTY,SUM(NVL(STR_RCV_CURRENT_MONTH_QTY,0)) STR_RCV_CURRENT_MONTH_QTY,SUM(NVL(STR_RCV_CURRENT_MONTH_WEIGHT,0)) STR_RCV_CURRENT_MONTH_WEIGHT ,SUM(NVL(STR_RCV_TODAY_QTY,0)) STR_RCV_TODAY_QTY     ");
                sb.Append("  ,SUM(NVL(STR_RCV_TODAY_WEIGHT,0)) STR_RCV_TODAY_WEIGHT,SUM(NVL(STR_ISS_CURRENT_MONTH_QTY,0)) STR_ISS_CURRENT_MONTH_QTY,SUM(NVL(STR_ISS_CURRENT_MONTH_WEIGHT,0)) STR_ISS_CURRENT_MONTH_WEIGHT    ");
                sb.Append("  ,SUM(NVL(STR_ISS_TODAY_QTY,0))  STR_ISS_TODAY_QTY  ,SUM(NVL(STR_ISS_TODAY_WEIGHT,0))  STR_ISS_TODAY_WEIGHT from TEMP_MAIL_DEPT_STOCK  group by ITEM_ID,ITEM_NAME  ");




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
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();

                    // stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    // stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();

                    //stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = "Kg";  // dRow["UOM_NAME"].ToString();
                    stk.STR_RCV_CURRENT_MONTH_QTY = Conversion.DBNullDecimalToZero(dRow["STR_RCV_CURRENT_MONTH_QTY"].ToString());
                    stk.STR_RCV_CURRENT_MONTH_WEIGHT = Conversion.DBNullDecimalToZero(dRow["STR_RCV_CURRENT_MONTH_WEIGHT"].ToString());
                    stk.STR_RCV_TODAY_QTY = Conversion.DBNullDecimalToZero(dRow["STR_RCV_TODAY_QTY"].ToString());
                    stk.STR_RCV_TODAY_WEIGHT = Conversion.DBNullDecimalToZero(dRow["STR_RCV_TODAY_WEIGHT"].ToString());

                    //stk.STR_ISS_CURRENT_MONTH_QTY = Conversion.DBNullDecimalToZero(dRow["STR_ISS_CURRENT_MONTH_QTY"].ToString());
                    //stk.STR_ISS_CURRENT_MONTH_WEIGHT = Conversion.DBNullDecimalToZero(dRow["STR_ISS_CURRENT_MONTH_WEIGHT"].ToString());
                    //stk.STR_ISS_TODAY_QTY = Conversion.DBNullDecimalToZero(dRow["STR_ISS_TODAY_QTY"].ToString());
                    //stk.STR_ISS_TODAY_WEIGHT = Conversion.DBNullDecimalToZero(dRow["STR_ISS_TODAY_WEIGHT"].ToString());
                    cRptList.Add(stk);
                }

                if (cRptList.Count > 0)
                {
                    dcMaterialStock stk = new dcMaterialStock();
                    stk.DEPARTMENT_NAME = "";
                    stk.UOM_NAME = "";
                    stk.DEPT_ISS_CURRENT_MONTH_QTY = 0;
                }

            }
            //catch { throw; }
            //finally 
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }

            return cRptList;

        }





        #endregion


        #region --------------------------************* Item Ageing ***********************************************-----
        public static List<rcMaterialStock> GetItemAgeingReport(clsPrmInventory prmINV, DBContext dc)
        {

            List<rcMaterialStock> cRptList = new List<rcMaterialStock>();


            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                cmdInfo.DBParametersInfo.Clear();


                if (prmINV.ToDate.HasValue)
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.ToDate.Value);
                }



                cmdInfo.DBParametersInfo.Add(":p_itemtype_id", prmINV.itemtype_id);
                cmdInfo.DBParametersInfo.Add(":p_itemGroup_id", prmINV.itemGroup_id);
                cmdInfo.DBParametersInfo.Add(":p_item_id", prmINV.item_id);
                cmdInfo.DBParametersInfo.Add(":p_store_id", prmINV.store_id);
                cmdInfo.DBParametersInfo.Add(":p_itemclass_id", prmINV.ItemClassId);
                cmdInfo.DBParametersInfo.Add(":p_battery_type_id", prmINV.Battery_Type_ID);
                cmdInfo.DBParametersInfo.Add(":p_is_repair", prmINV.Is_Repair);
                cmdInfo.DBParametersInfo.Add(":p_item_sns_id", prmINV.SNS_Type);

                //cmdInfo.DBParametersInfo.Add(":P_ITEM_CODE", prmSND.Item_Code);
                DBQuery dbq = new DBQuery();

                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                //dbq.CommandTimeout = 1000;
                cmdInfo.CommandTimeout = 600;
                cmdInfo.CommandText = "SP_STORE_ITEM_STOCK";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);

               
                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;
                //sb.Append(" SELECT * FROM TEMP_ITEM_STOCK_BAT_FOR_NET ");

                sb.Append(" Select ITEM_GROUP_ID,ITEM_GROUP_NAME,ITEM_CODE,ITEM_ID,ITEM_NAME,UOM_ID,UOM_NAME,ITEM_TYPE_ID,ITEM_TYPE_NAME,ITEM_CLASS_ID,ITEM_CLASS_NAME,OP_BAL,OP_MRR_QTY,OP_IRR_QTY,OP_ITC_QTY,OP_OUTSALES_QTY,OP_ROTARY_QTY,OP_LOANRP_QTY,OP_RTN_QTY,OP_ADJ,OP_BAL+OP_MRR_QTY+OP_IRR_QTY-OP_ITC_QTY-OP_OUTSALES_QTY-OP_ROTARY_QTY-OP_LOANRP_QTY+OP_RTN_QTY+OP_ADJ OPPENING_BAL_QTY,MRR_QTY,IRR_QTY,ITC_QTY,OUTSALES_QTY,ROTARY_QTY,LOANRP_QTY,RTN_QTY, OP_BAL+OP_MRR_QTY+OP_IRR_QTY-OP_ITC_QTY-OP_OUTSALES_QTY-OP_ROTARY_QTY-OP_LOANRP_QTY+OP_RTN_QTY+OP_ADJ+MRR_QTY+IRR_QTY-ITC_QTY-OUTSALES_QTY-ROTARY_QTY-LOANRP_QTY+RTN_QTY+ADJUST_QTY COLISING_QTY, BAL_QTY,ADJUST_QTY  ");
                sb.Append(" ,(SELECT max(trans_date) FROM item_stock_details d where d.item_id=tmp.ITEM_ID and d.inv_trans_type_id=301) LAST_ITC_DATE FROM TEMP_STORE_ITEM_STOCK  tmp where  1=1 ");
                sb.Append("  AND  (SELECT max(trans_date) FROM item_stock_details d where d.item_id=tmp.item_id and d.inv_trans_type_id=301) <= @pAgeingDate ");
                sb.Append("  AND ( OP_BAL+OP_MRR_QTY+OP_IRR_QTY-OP_ITC_QTY-OP_OUTSALES_QTY-OP_ROTARY_QTY-OP_LOANRP_QTY+OP_RTN_QTY+OP_ADJ+MRR_QTY+IRR_QTY-ITC_QTY-OUTSALES_QTY-ROTARY_QTY-LOANRP_QTY+RTN_QTY+ADJUST_QTY)>0 ");

                cmdInfotemp.DBParametersInfo.Add("@pAgeingDate", prmINV.AgeingDate);

                DBQuery dbqtemp = new DBQuery();
                //DBQuery dbq = new DBQuery();


                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfotemp.CommandTimeout = 600;

                cmdInfotemp.CommandText = sb.ToString();
                cmdInfotemp.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfotemp;
                //DBQuery.ExecuteDBQuerySP(dbq, dc);
                DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);

                //,,,,,,,,,,,,,,,,,


                foreach (DataRow dRow in dtData.Rows)
                {
                    rcMaterialStock stk = new rcMaterialStock();

                    stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    stk.ITEM_GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    stk.ITEM_CODE = dRow["ITEM_CODE"].ToString();

                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();

                    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);

                    stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                    stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();

                    //OP_BAL+OP_MRR_QTY+OP_IRR_QTY-OP_ITC_QTY-OP_OUTSALES_QTY-OP_ROTARY_QTY-OP_LOANRP_QTY+OP_RTN_QTY+OP_ADJ

                    //here all possible transaction for calculation opening balance
                    stk.OP_BAL = Conversion.DBNullDecimalToZero(dRow["OP_BAL"]);
                    stk.OP_MRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_MRR_QTY"]);
                    stk.OP_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_BAL"]);
                    stk.OP_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_IRR_QTY"]);
                    stk.OP_OUTSALES_QTY = Conversion.DBNullDecimalToZero(dRow["OP_OUTSALES_QTY"]);
                    stk.OP_ROTARY_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ROTARY_QTY"]);
                    stk.OP_LOANRP_QTY = Conversion.DBNullDecimalToZero(dRow["OP_LOANRP_QTY"]);
                    stk.OP_RTN_QTY = Conversion.DBNullDecimalToZero(dRow["OP_RTN_QTY"]);
                    stk.OP_ADJ = Conversion.DBNullDecimalToZero(dRow["OP_ADJ"]);
                    stk.LAST_ITC_DATE = Conversion.StringToDate( dRow["LAST_ITC_DATE"].ToString());

                    stk.OPPENING_BAL_QTY = stk.OP_BAL + stk.OP_MRR_QTY + stk.OP_IRR_QTY - stk.OP_ITC_QTY - stk.OP_OUTSALES_QTY - stk.OP_ROTARY_QTY - stk.OP_LOANRP_QTY + stk.OP_RTN_QTY + stk.OP_ADJ;

                    stk.OPPENING_BAL_QTY = Conversion.DBNullDecimalToZero(dRow["OPPENING_BAL_QTY"]);
                    stk.MRR_QTY = Conversion.DBNullDecimalToZero(dRow["MRR_QTY"]);
                    stk.IRR_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_QTY"]);

                    stk.ITC_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_QTY"]);
                    stk.OUTSALES_QTY = Conversion.DBNullDecimalToZero(dRow["OUTSALES_QTY"]);

                    stk.ROTARY_QTY = Conversion.DBNullDecimalToZero(dRow["ROTARY_QTY"]);
                    stk.LOANRP_QTY = Conversion.DBNullDecimalToZero(dRow["LOANRP_QTY"]);
                    stk.RTN_QTY = Conversion.DBNullDecimalToZero(dRow["RTN_QTY"]);
                    stk.BAL_QTY = Conversion.DBNullDecimalToZero(dRow["BAL_QTY"]);
                    stk.COLISING_QTY = Conversion.DBNullDecimalToZero(dRow["COLISING_QTY"]);
                    stk.ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);
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
        #endregion 



        #region *********************************** Item Stock PRice*******************************************
         

        public static List<rcMaterialStock> GetStockPriceReport(clsPrmInventory prmINV, DBContext dc)
        {

            List<rcMaterialStock> cRptList = new List<rcMaterialStock>();


            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();
                cmdInfo.DBParametersInfo.Clear();

                if (prmINV.ToDate.HasValue)
                {
                    cmdInfo.DBParametersInfo.Add(":P_DATE_FROM", prmINV.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add(":P_DATE_TO", prmINV.ToDate.Value);
                }

                cmdInfo.DBParametersInfo.Add(":p_itemtype_id", prmINV.itemtype_id);
                cmdInfo.DBParametersInfo.Add(":p_itemGroup_id", prmINV.itemGroup_id);
                cmdInfo.DBParametersInfo.Add(":p_item_id", prmINV.item_id);
                cmdInfo.DBParametersInfo.Add(":p_store_id", prmINV.store_id);
                cmdInfo.DBParametersInfo.Add(":p_itemclass_id", prmINV.ItemClassId);
                cmdInfo.DBParametersInfo.Add(":p_battery_type_id", prmINV.Battery_Type_ID);
                cmdInfo.DBParametersInfo.Add(":p_is_repair", prmINV.Is_Repair);
                cmdInfo.DBParametersInfo.Add(":p_item_sns_id", prmINV.SNS_Type);

                DBQuery dbq = new DBQuery();

                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                //dbq.CommandTimeout = 1000;
                cmdInfo.CommandTimeout = 600;
                cmdInfo.CommandText = "SP_STORE_ITEM_STOCK";
                cmdInfo.CommandType = CommandType.StoredProcedure;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBQuerySP(dbq, dc);

                DBCommandInfo cmdInfotemp = new DBCommandInfo();
                sb.Length = 0;
                sb.Append(" Select ITEM_GROUP_ID,ITEM_GROUP_NAME,ITEM_CODE,ITEM_ID,ITEM_NAME,UOM_ID,UOM_NAME,ITEM_TYPE_ID,ITEM_TYPE_NAME ");
                 sb.Append(" ,ITEM_CLASS_ID,ITEM_CLASS_NAME,OP_BAL,OP_MRR_QTY,OP_IRR_QTY,OP_ITC_QTY,OP_OUTSALES_QTY,OP_ROTARY_QTY ");

                 sb.Append(" , FN_STOCK_OPENING_PRICE(ITEM_ID,@P_FROM_DATE3,@P_TO_DATE3) OpeningRate ");


                 sb.Append(" ,OP_LOANRP_QTY,OP_RTN_QTY,OP_ADJ,OP_BAL+OP_MRR_QTY+OP_IRR_QTY-OP_ITC_QTY-OP_OUTSALES_QTY-OP_ROTARY_QTY-OP_LOANRP_QTY+OP_RTN_QTY+OP_ADJ OPPENING_BAL_QTY ");
               
                
                sb.Append(" ,FN_STOCK_RCV_PRICE(ITEM_ID,@P_FROM_DATE,@P_TO_DATE  ) InwardRate ");

                sb.Append( " ,MRR_QTY,IRR_QTY,ITC_QTY,OUTSALES_QTY,ROTARY_QTY,LOANRP_QTY,RTN_QTY ");
                sb.Append(" , FN_STOCK_ISS_PRICE(ITEM_ID,@P_FROM_DATE1,@P_TO_DATE1 )   OutwardRate ");

                sb.Append( " , OP_BAL+OP_MRR_QTY+OP_IRR_QTY-OP_ITC_QTY-OP_OUTSALES_QTY-OP_ROTARY_QTY-OP_LOANRP_QTY+OP_RTN_QTY+OP_ADJ+MRR_QTY+IRR_QTY-ITC_QTY-OUTSALES_QTY-ROTARY_QTY-LOANRP_QTY+RTN_QTY+ADJUST_QTY COLISING_QTY ");
                sb.Append(" , FN_STOCK_CLOSE_PRICE(ITEM_ID,@P_FROM_DATE2,@P_TO_DATE2 )  ClosingRate ");
                sb.Append(" , BAL_QTY,ADJUST_QTY, ITEM_RATE FROM TEMP_STORE_ITEM_STOCK");

                cmdInfotemp.DBParametersInfo.Add("@P_FROM_DATE", prmINV.FromDate.Value);
                cmdInfotemp.DBParametersInfo.Add("@P_TO_DATE", prmINV.ToDate.Value);

                cmdInfotemp.DBParametersInfo.Add("@P_FROM_DATE1", prmINV.FromDate.Value);
                cmdInfotemp.DBParametersInfo.Add("@P_TO_DATE1", prmINV.ToDate.Value);

                cmdInfotemp.DBParametersInfo.Add("@P_FROM_DATE2", prmINV.FromDate.Value);
                cmdInfotemp.DBParametersInfo.Add("@P_TO_DATE2", prmINV.ToDate.Value);

                cmdInfotemp.DBParametersInfo.Add("@P_FROM_DATE3", prmINV.FromDate.Value);
                cmdInfotemp.DBParametersInfo.Add("@P_TO_DATE3", prmINV.ToDate.Value);
                DBQuery dbqtemp = new DBQuery();
                //DBQuery dbq = new DBQuery();


                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfotemp.CommandTimeout = 600;

                cmdInfotemp.CommandText = sb.ToString();
                cmdInfotemp.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfotemp;
                //DBQuery.ExecuteDBQuerySP(dbq, dc);
                DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);

                //,,,,,,,,,,,,,,,,,


                foreach (DataRow dRow in dtData.Rows)
                {
                    rcMaterialStock stk = new rcMaterialStock();

                    stk.ITEM_GROUP_ID = Conversion.DBNullIntToZero(dRow["ITEM_GROUP_ID"]);
                    stk.GROUP_NAME = dRow["ITEM_GROUP_NAME"].ToString();

                    stk.ITEM_CODE = dRow["ITEM_CODE"].ToString();

                    stk.ITEM_ID = Conversion.DBNullIntToZero(dRow["ITEM_ID"]);
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();

                    stk.UOM_ID = Conversion.DBNullIntToZero(dRow["UOM_ID"]);
                    stk.UOM_NAME = dRow["UOM_NAME"].ToString();
                    stk.ITEM_TYPE_ID = Conversion.DBNullIntToZero(dRow["ITEM_TYPE_ID"]);

                    stk.ITEM_TYPE_NAME = dRow["ITEM_TYPE_NAME"].ToString();
                    stk.ITEM_CLASS_ID = Conversion.DBNullIntToZero(dRow["ITEM_CLASS_ID"]);
                    stk.ITEM_CLASS_NAME = dRow["ITEM_CLASS_NAME"].ToString();

                    //OP_BAL+OP_MRR_QTY+OP_IRR_QTY-OP_ITC_QTY-OP_OUTSALES_QTY-OP_ROTARY_QTY-OP_LOANRP_QTY+OP_RTN_QTY+OP_ADJ

                    //here all possible transaction for calculation opening balance
                    stk.OP_BAL = Conversion.DBNullDecimalToZero(dRow["OP_BAL"]);
                    stk.OP_MRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_MRR_QTY"]);
                    stk.OP_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_BAL"]);
                    stk.OP_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_IRR_QTY"]);
                    stk.OP_OUTSALES_QTY = Conversion.DBNullDecimalToZero(dRow["OP_OUTSALES_QTY"]);
                    stk.OP_ROTARY_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ROTARY_QTY"]);
                    stk.OP_LOANRP_QTY = Conversion.DBNullDecimalToZero(dRow["OP_LOANRP_QTY"]);
                    stk.OP_RTN_QTY = Conversion.DBNullDecimalToZero(dRow["OP_RTN_QTY"]);
                    stk.OP_ADJ = Conversion.DBNullDecimalToZero(dRow["OP_ADJ"]);


                    stk.OPPENING_BAL_QTY = stk.OP_BAL + stk.OP_MRR_QTY + stk.OP_IRR_QTY - stk.OP_ITC_QTY - stk.OP_OUTSALES_QTY - stk.OP_ROTARY_QTY - stk.OP_LOANRP_QTY + stk.OP_RTN_QTY + stk.OP_ADJ;

                    stk.OPPENING_BAL_QTY = Conversion.DBNullDecimalToZero(dRow["OPPENING_BAL_QTY"]);

                    stk.OP_Balance = Conversion.DBNullDecimalToZero(dRow["OPPENING_BAL_QTY"]); // price
                    stk.OpeningRate = Conversion.DBNullDecimalToZero(dRow["OpeningRate"]); ;
                    stk.Openning_Value = stk.OP_Balance * stk.OpeningRate;



                    stk.MRR_QTY = Conversion.DBNullDecimalToZero(dRow["MRR_QTY"]);
                    stk.IRR_QTY = Conversion.DBNullDecimalToZero(dRow["IRR_QTY"]);
                    stk.RTN_QTY = Conversion.DBNullDecimalToZero(dRow["RTN_QTY"]);

                    stk.InwardQty = stk.MRR_QTY + stk.IRR_QTY + stk.RTN_QTY;
                    stk.InwardRate = Conversion.DBNullDecimalToZero(dRow["InwardRate"]);
                    stk.InwardValue = stk.InwardQty * stk.InwardRate;


                   


                    stk.ITC_QTY = Conversion.DBNullDecimalToZero(dRow["ITC_QTY"]);
                    stk.OUTSALES_QTY = Conversion.DBNullDecimalToZero(dRow["OUTSALES_QTY"]);
                    stk.ROTARY_QTY = Conversion.DBNullDecimalToZero(dRow["ROTARY_QTY"]);
                    stk.LOANRP_QTY = Conversion.DBNullDecimalToZero(dRow["LOANRP_QTY"]);


                    stk.OutwardQty = stk.ITC_QTY + stk.LOANRP_QTY + stk.ROTARY_QTY + stk.OUTSALES_QTY;
                    stk.OutwardRate = Conversion.DBNullDecimalToZero(dRow["OutwardRate"]);
                    stk.OutwardValue = stk.OutwardQty * stk.OutwardRate;




                    stk.BAL_QTY = Conversion.DBNullDecimalToZero(dRow["BAL_QTY"]);
                    stk.COLISING_QTY = Conversion.DBNullDecimalToZero(dRow["COLISING_QTY"]);
                    stk.ADJUST_QTY = Conversion.DBNullDecimalToZero(dRow["ADJUST_QTY"]);

                    stk.ITEM_RATE = Conversion.DBNullDecimalToZero(dRow["ITEM_RATE"]);
                    stk.COLISING_VALUE = (stk.COLISING_QTY * stk.ITEM_RATE);


                    stk.Closing_Balance = Conversion.DBNullDecimalToZero(dRow["COLISING_QTY"]) + stk.ADJUST_QTY;
                    stk.ClosingRate = Conversion.DBNullDecimalToZero(dRow["ClosingRate"]);
                    stk.ClosingValue = stk.Closing_Balance * stk.ClosingRate;



                    //decimal OP_BAL = Conversion.DBNullDecimalToZero(dRow["OP_BAL"]);
                    //decimal OP_MRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_MRR_QTY"]);
                    //decimal OP_IRR_QTY = Conversion.DBNullDecimalToZero(dRow["OP_IRR_QTY"]);
                    //decimal OP_ITC_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ITC_QTY"]);
                    //decimal OP_OUTSALES_QTY = Conversion.DBNullDecimalToZero(dRow["OP_OUTSALES_QTY"]);
                    //decimal OP_ROTARY_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ROTARY_QTY"]);
                    //decimal OP_LOANRP_QTY = Conversion.DBNullDecimalToZero(dRow["OP_ROTARY_QTY"]);
                    //decimal OP_RTN_QTY = Conversion.DBNullDecimalToZero(dRow["OP_RTN_QTY"]);
                    //decimal OP_ADJ = Conversion.DBNullDecimalToZero(dRow["OP_ADJ"]);
                    //decimal total_op = OP_BAL + OP_MRR_QTY + OP_IRR_QTY - OP_ITC_QTY - OP_OUTSALES_QTY - OP_ROTARY_QTY - OP_LOANRP_QTY + OP_RTN_QTY + OP_ADJ;
                    if (stk.OPPENING_BAL_QTY > 0 || stk.COLISING_VALUE > 0 || stk.COLISING_QTY > 0 || stk.ROTARY_QTY > 0 || stk.BAL_QTY > 0 || stk.ITC_QTY > 0 || stk.MRR_QTY > 0 || stk.IRR_QTY>0)
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


        #endregion
    }
}
