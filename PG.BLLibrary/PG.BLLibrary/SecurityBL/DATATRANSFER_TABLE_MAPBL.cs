using PG.BLLibrary.InventoryBL;
using PG.BLLibrary.ProductionBL;
using PG.Core.DBBase;
using PG.Core.Utility;
using PG.DBClass.InventoryDC;
using PG.DBClass.ProductionDC;
using PG.DBClass.SecurityDC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Text;

namespace PG.BLLibrary.SecurityBL
{
    public class DATATRANSFER_TABLE_MAPBL
    {
        public static string DataTransfer_Table_String()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT * FROM DATATRANSFER_TABLE_MAP WHERE 1=1  ");

            return sb.ToString();
        }

        public static List<dcDATATRANSFER_MASTER_DETAIL> GetDataTransferTableList(int pID, DBContext dc)
        {
            List<dcDATATRANSFER_MASTER_DETAIL> cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                string sql = "select * from DATATRANSFER_MASTER_DETAIL Where 1=1 AND IS_ACTIVE='Y' ";
                //cmdInfo.DBParametersInfo.Add("@pID", pID);DATATRANSFER_TABLE_MAP.ID=@pID
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sql.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcDATATRANSFER_MASTER_DETAIL>(dbq, dc).ToList();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static DataLoadOptions DATATRANSFER_TABLE_MAPLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcDATATRANSFER_TABLE_MAP>(obj => obj.relatedclassname);
            return dlo;
        }
        public static List<dcDATATRANSFER_TABLE_MAP> GetDATATRANSFER_TABLE_MAPList()
        {
            return GetDATATRANSFER_TABLE_MAPList(null, null);
        }
        public static List<dcDATATRANSFER_TABLE_MAP> GetDATATRANSFER_TABLE_MAPList(DBContext dc)
        {
            return GetDATATRANSFER_TABLE_MAPList(null, dc);
        }
        public static List<dcDATATRANSFER_TABLE_MAP> GetDATATRANSFER_TABLE_MAPList(DBQuery dbq, DBContext dc)
        {
            List<dcDATATRANSFER_TABLE_MAP> cObjList = new List<dcDATATRANSFER_TABLE_MAP>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcDATATRANSFER_TABLE_MAP>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcDATATRANSFER_TABLE_MAP GetDATATRANSFER_TABLE_MAPByID(int pDATATRANSFER_TABLE_MAPID)
        {
            return GetDATATRANSFER_TABLE_MAPByID(pDATATRANSFER_TABLE_MAPID, null);
        }
        public static dcDATATRANSFER_TABLE_MAP GetDATATRANSFER_TABLE_MAPByID(int pDATATRANSFER_TABLE_MAPID, DBContext dc)
        {
            dcDATATRANSFER_TABLE_MAP cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcDATATRANSFER_TABLE_MAP>()
                                  where c.ID == pDATATRANSFER_TABLE_MAPID
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

        public static int Insert(dcDATATRANSFER_TABLE_MAP cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcDATATRANSFER_TABLE_MAP cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcDATATRANSFER_TABLE_MAP>(cObj, true);
                if (id > 0) { cObj.ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcDATATRANSFER_TABLE_MAP cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcDATATRANSFER_TABLE_MAP cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcDATATRANSFER_TABLE_MAP>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pDATATRANSFER_TABLE_MAPID)
        {
            return Delete(pDATATRANSFER_TABLE_MAPID, null);
        }
        public static bool Delete(int pDATATRANSFER_TABLE_MAPID, DBContext dc)
        {
            dcDATATRANSFER_TABLE_MAP cObj = new dcDATATRANSFER_TABLE_MAP();
            cObj.ID = pDATATRANSFER_TABLE_MAPID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcDATATRANSFER_TABLE_MAP>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcDATATRANSFER_TABLE_MAP cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcDATATRANSFER_TABLE_MAP cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcDATATRANSFER_TABLE_MAP cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcDATATRANSFER_TABLE_MAP cObj, DBContext dc)
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
                                newID = cObj.ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.ID, dc))
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

        public static bool SaveList(List<dcDATATRANSFER_TABLE_MAP> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcDATATRANSFER_TABLE_MAP> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcDATATRANSFER_TABLE_MAP oDet in detList)
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
                        bool d = Delete(oDet.ID, dc);
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

        public static void DataProcess(dcDATATRANSFER_TABLE_MAP pcObj)
        {
            try
            {

                string iobj = String.Empty;
                if (pcObj.TABLENAME != String.Empty)
                {
                    //DBContextSettings dbcontext = DBContextManager.GetDBContextSettings("PBL_PSP");  // Connection for ms
                    //DBContext dcc = DBContextManager.CreateAndInitDBContext(dbcontext);

                    //switch (pcObj.TABLENAME)
                    //{
                    //    case "PRODUCTION_MST":
                    //        List<dcPRODUCTION_MST> list = PRODUCTION_MSTBL.GetPRODUCTION_MST_MS(pcObj, null);

                    //        foreach (dcPRODUCTION_MST item in list)
                    //        {
                    //            PRODUCTION_MSTBL.Delete_MS(item.PROD_ID, dcc);
                    //            item._RecordState = RecordStateEnum.Added;
                    //        }
                    //        PRODUCTION_MSTBL.SaveList(list, dcc);
                    //        break;

                    //    case "PRODUCTION_DTL":
                    //        List<dcPRODUCTION_DTL> listprodDtl = PRODUCTION_DTLBL.GetPRODUCTION_DTL_MS(pcObj, null);
                          
                    //        foreach (dcPRODUCTION_DTL item in listprodDtl)
                    //        {
                    //            PRODUCTION_DTLBL.Delete_MS(item.PROD_DTL_ID, dcc);
                    //            item._RecordState = RecordStateEnum.Added;
                    //        }
                    //        PRODUCTION_DTLBL.SaveList(listprodDtl, dcc);
                    //        break;

                    //    case "PRODUCTION_FLOOR_CLOSING":
                    //        List<dcPRODUCTION_FLOOR_CLOSING> listprodCls = PRODUCTION_FLOOR_CLOSINGBL.GetPRODUCTION_CLOSING_MS(pcObj, null);
                    //        foreach (dcPRODUCTION_FLOOR_CLOSING item in listprodCls)
                    //        {
                    //            PRODUCTION_FLOOR_CLOSINGBL.Delete_MS(item.CLOSING_ID, dcc);
                    //            item._RecordState = RecordStateEnum.Added;
                    //        }
                    //        PRODUCTION_FLOOR_CLOSINGBL.SaveList(listprodCls, dcc);
                    //        break;

                    //    case "ITEM_STOCK_DETAILS":
                    //        List<dcITEM_STOCK_DETAILS> listStockDtl = ITEM_STOCK_DETAILSBL.GetItemStk_MS(pcObj, null);
                    //        foreach (dcITEM_STOCK_DETAILS item in listStockDtl)
                    //        {
                    //            ITEM_STOCK_DETAILSBL.Delete_MS(item.ITEM_STK_DET_ID, dcc);
                    //            item._RecordState = RecordStateEnum.Added;
                    //        }
                    //        ITEM_STOCK_DETAILSBL.SaveList(listStockDtl, dcc);
                    //        break;


                    //    case "ACCOUNT_HOLDER":
                    //        List<dcACCOUNT_HOLDER> listACCOUNT_HOLDER = ACCOUNT_HOLDERBL.GetACCOUNT_HOLDER_MS(pcObj, null);
                    //        foreach (dcACCOUNT_HOLDER item in listACCOUNT_HOLDER)
                    //        {
                    //            ACCOUNT_HOLDERBL.Delete_MS(item.ACC_ID, dcc);
                    //            item._RecordState = RecordStateEnum.Added;
                    //        }
                    //        ACCOUNT_HOLDERBL.SaveList(listACCOUNT_HOLDER, dcc);
                    //        break;

                    //    case "ACCOUNT_HOLDER_DTL":
                    //        List<dcACCOUNT_HOLDER_DTL> listACCOUNT_HOLDER_DTL = ACCOUNT_HOLDER_DTLBL.GetACCOUNT_HOLDER_DTL_MS(pcObj, null);
                    //        foreach (dcACCOUNT_HOLDER_DTL item in listACCOUNT_HOLDER_DTL)
                    //        {
                    //            ACCOUNT_HOLDER_DTLBL.Delete_MS(item.ACC_HOLDER_DTL_ID, dcc);
                    //            item._RecordState = RecordStateEnum.Added;
                    //        }
                    //        ACCOUNT_HOLDER_DTLBL.SaveList(listACCOUNT_HOLDER_DTL, dcc);
                    //        break;

                    //    case "ASM_REJECTION_PLATE_STORE_MST":
                    //        List<dcASM_REJECTION_PLATE_STORE_MST> lstASM_REJECTION_PLATE_STORE_MST = ASM_REJECTION_PLATE_STORE_MSTBL.GetASM_REJECTION_PLATE_STORE_MST_MS(pcObj, null);
                    //        foreach (dcASM_REJECTION_PLATE_STORE_MST item in lstASM_REJECTION_PLATE_STORE_MST)
                    //        {
                    //            ASM_REJECTION_PLATE_STORE_MSTBL.Delete_MS(item.PROD_REJECTION_ID, dcc);
                    //            item._RecordState = RecordStateEnum.Added;
                    //        }
                    //        ASM_REJECTION_PLATE_STORE_MSTBL.SaveList(lstASM_REJECTION_PLATE_STORE_MST, dcc);
                    //        break;

                    //    case "ASM_REJECTION_PLATE_STORE_DTL":
                    //        List<dcASM_REJECTION_PLATE_STORE_DTL> lstASM_REJECTION_PLATE_STORE_DTL = ASM_REJECTION_PLATE_STORE_DTLBL.GetASM_REJECTION_PLATE_STORE_MST_MS(pcObj, null);
                    //        foreach (dcASM_REJECTION_PLATE_STORE_DTL item in lstASM_REJECTION_PLATE_STORE_DTL)
                    //        {
                    //            ASM_REJECTION_PLATE_STORE_DTLBL.Delete_MS(item.PROD_REJECTION_DTL_ID, dcc);
                    //            item._RecordState = RecordStateEnum.Added;
                    //        }
                    //        ASM_REJECTION_PLATE_STORE_DTLBL.SaveList(lstASM_REJECTION_PLATE_STORE_DTL, dcc);
                    //        break;

                    //    case "BANK_BRANCH_MST":
                    //        List<dcBANK_BRANCH_MST> lstBANK_BRANCH_MST = BANK_BRANCH_MSTBL.GetBANK_BRANCH_MST_MS(pcObj, null);
                    //        foreach (dcBANK_BRANCH_MST item in lstBANK_BRANCH_MST)
                    //        {
                    //            BANK_BRANCH_MSTBL.Delete_MS(item.BRANCH_ID, dcc);
                    //            item._RecordState = RecordStateEnum.Added;
                    //        }
                    //        BANK_BRANCH_MSTBL.SaveList(lstBANK_BRANCH_MST, dcc);
                    //        break;

                    //    case "BANK_MST":
                    //        List<dcBANK_MST> lstBANK_MST = BANK_MSTBL.GetBANK_MST_MS(pcObj, null);
                    //        foreach (dcBANK_MST item in lstBANK_MST)
                    //        {
                    //            BANK_MSTBL.Delete_MS(item.BANK_ID, dcc);
                    //            item._RecordState = RecordStateEnum.Added;
                    //        }
                    //        BANK_MSTBL.SaveList(lstBANK_MST, dcc);
                    //        break;

                    //    case "BOM_MST_T":
                    //        List<dcBOM_MST_T> lstBOM_MST_T = BOM_MST_TBL.GetOM_MST_T_MS(pcObj, null);
                    //        foreach (dcBOM_MST_T item in lstBOM_MST_T)
                    //        {
                    //            BOM_MST_TBL.Delete_MS(item.BOM_ID, dcc);
                    //            item._RecordState = RecordStateEnum.Added;
                    //        }
                    //        BOM_MST_TBL.SaveList(lstBOM_MST_T, dcc);
                    //        break;

                    //    case "BOM_DTL_T":
                    //        List<dcBOM_DTL_T> lstBOM_DTL_T = BOM_DTL_TBL.GetBOM_DTL_T_MS(pcObj, null);
                    //        foreach (dcBOM_DTL_T item in lstBOM_DTL_T)
                    //        {
                    //            BOM_DTL_TBL.Delete_MS(item.BOM_DTL_ID, dcc);
                    //            item._RecordState = RecordStateEnum.Added;
                    //        }
                    //        BOM_DTL_TBL.SaveList(lstBOM_DTL_T, dcc);
                    //        break;
                    //    case "BRANCH_INFO":
                    //        List<dcBRANCH_INFO> lstBRANCH_INFO = BRANCH_INFOBL.GetBRANCH_INFO_MS(pcObj, null);
                    //        foreach (dcBRANCH_INFO item in lstBRANCH_INFO)
                    //        {
                    //            BRANCH_INFOBL.Delete_MS(item.BRANCH_ID, dcc);
                    //            item._RecordState = RecordStateEnum.Added;
                    //        }
                    //        BRANCH_INFOBL.SaveList(lstBRANCH_INFO, dcc);
                    //        break;
                    //    case "CASH_MEDIA_MST":
                    //        List<dcCASH_MEDIA_MST> lstCASH_MEDIA_MST = CASH_MEDIA_MSTBL.GetCASH_MEDIA_MST_MS(pcObj, null);
                    //        foreach (dcCASH_MEDIA_MST item in lstCASH_MEDIA_MST)
                    //        {
                    //            CASH_MEDIA_MSTBL.Delete_MS(item.CM_ID, dcc);
                    //            item._RecordState = RecordStateEnum.Added;
                    //        }
                    //        CASH_MEDIA_MSTBL.SaveList(lstCASH_MEDIA_MST, dcc);
                    //        break;

                    //    case "CNF_MASTER":
                    //        List<dcCNF_MASTER> lstCNF_MASTER = CNF_MASTERBL.GetCNF_MASTER_MS(pcObj, null);
                    //        foreach (dcCNF_MASTER item in lstCNF_MASTER)
                    //        {
                    //            CNF_MASTERBL.Delete_MS(item.CNF_ID, dcc);
                    //            item._RecordState = RecordStateEnum.Added;
                    //        }
                    //        CNF_MASTERBL.SaveList(lstCNF_MASTER, dcc);
                    //        break;
                    //    case "CUSTOMER_ADJUSTMENT":
                    //        List<dcCUSTOMER_ADJUSTMENT> lstCUSTOMER_ADJUSTMENT = CUSTOMER_ADJUSTMENTBL.GetCUSTOMER_ADJUSTMENT_MS(pcObj, null);
                    //        foreach (dcCUSTOMER_ADJUSTMENT item in lstCUSTOMER_ADJUSTMENT)
                    //        {
                    //            CUSTOMER_ADJUSTMENTBL.Delete_MS(item.ADJUST_ID, dcc);
                    //            item._RecordState = RecordStateEnum.Added;
                    //        }
                    //        CUSTOMER_ADJUSTMENTBL.SaveList(lstCUSTOMER_ADJUSTMENT, dcc);
                    //        break;

                    //    case "CUSTOMER_INFO":
                    //        List<dcCUSTOMER_INFO> lstCUSTOMER_INFO = CUSTOMER_INFOBL.GetCUSTOMER_INFO_MS(pcObj, null);
                    //        foreach (dcCUSTOMER_INFO item in lstCUSTOMER_INFO)
                    //        {
                    //            CUSTOMER_INFOBL.Delete_MS(item.CUST_ID, dcc);
                    //            item._RecordState = RecordStateEnum.Added;
                    //        }
                    //        CUSTOMER_INFOBL.SaveList(lstCUSTOMER_INFO, dcc);
                    //        break;

                    //    case "CUSTOMER_OPENNING_BALANCE":
                    //        List<dcCUSTOMER_OPENNING_BALANCE> lstCUSTOMER_OPENNING_BALANCE = CUSTOMER_OPENNING_BALANCEBL.GetCUSTOMER_OPENNING_BALANCE_MS(pcObj, null);
                    //        foreach (dcCUSTOMER_OPENNING_BALANCE item in lstCUSTOMER_OPENNING_BALANCE)
                    //        {
                    //            CUSTOMER_OPENNING_BALANCEBL.Delete_MS(item.COB_ID, dcc);
                    //            item._RecordState = RecordStateEnum.Added;
                    //        }
                    //        CUSTOMER_OPENNING_BALANCEBL.SaveList(lstCUSTOMER_OPENNING_BALANCE, dcc);
                    //        break;


                    //    case "DC_MASTER":
                    //        List<dcDC_MASTER> lstDC_MASTER = DC_MASTERBL.GetDC_MASTER_MS(pcObj, null);
                    //        foreach (dcDC_MASTER item in lstDC_MASTER)
                    //        {
                    //            DC_MASTERBL.Delete_MS(item.DC_ID, dcc);
                    //            item._RecordState = RecordStateEnum.Added;
                    //        }
                    //        DC_MASTERBL.SaveList(lstDC_MASTER, dcc);
                    //        break;

                    //    case "DC_DETAILS":
                    //        List<dcDC_DETAILS> lstDC_DETAILS = DC_DETAILSBL.GetDC_DETAILS_MS(pcObj, null);
                    //        foreach (dcDC_DETAILS item in lstDC_DETAILS)
                    //        {
                    //            DC_DETAILSBL.Delete_MS(item.DC_DET_ID, dcc);
                    //            item._RecordState = RecordStateEnum.Added;
                    //        }
                    //        DC_DETAILSBL.SaveList(lstDC_DETAILS, dcc);
                    //        break;


                    //    case "DEPARTMENT_INFO":
                    //        List<dcDEPARTMENT_INFO> lstDEPARTMENT_INFO = DEPARTMENT_INFOBL.GetDEPARTMENT_INFO_MS(pcObj, null);
                    //        foreach (dcDEPARTMENT_INFO item in lstDEPARTMENT_INFO)
                    //        {
                    //            DEPARTMENT_INFOBL.Delete_MS(item.DEPARTMENT_ID, dcc);
                    //            item._RecordState = RecordStateEnum.Added;
                    //        }
                    //        DEPARTMENT_INFOBL.SaveList(lstDEPARTMENT_INFO, dcc);
                    //        break;


                    //    case "DEPARTMENT_ITEM":
                    //        List<dcDEPARTMENT_ITEM> lstDEPARTMENT_ITEM = DEPARTMENT_ITEMBL.GetDEPARTMENT_ITEM_MS(pcObj, null);
                    //        foreach (dcDEPARTMENT_ITEM item in lstDEPARTMENT_ITEM)
                    //        {
                    //            DEPARTMENT_ITEMBL.Delete_MS(item.DEPT_ID, item.ITEM_ID, dcc);
                    //            item._RecordState = RecordStateEnum.Added;
                    //        }
                    //        DEPARTMENT_ITEMBL.SaveList(lstDEPARTMENT_ITEM, dcc);
                    //        break;

                     


                    //    case "GRID_DROSS_DTL":
                    //        List<dcGRID_DROSS_DTL> lstGRID_DROSS_DTL = GRID_DROSS_DTLBL.GetGRID_DROSS_DTL_MS(pcObj, null);
                    //        foreach (dcGRID_DROSS_DTL item in lstGRID_DROSS_DTL)
                    //        {
                    //            GRID_DROSS_DTLBL.Delete_MS(item.DROSS_DTL_ID, dcc);
                    //            item._RecordState = RecordStateEnum.Added;
                    //        }
                    //        GRID_DROSS_DTLBL.SaveList(lstGRID_DROSS_DTL, dcc);
                    //        break;

                    //    case "GRID_DROSS_MST":
                    //        List<dcGRID_DROSS_MST> lstGRID_DROSS_MST = GRID_DROSS_MSTBL.GetGRID_DROSS_MST_MS(pcObj, null);
                    //        foreach (dcGRID_DROSS_MST item in lstGRID_DROSS_MST)
                    //        {
                    //            GRID_DROSS_MSTBL.Delete_MS(item.DROSS_MST_ID, dcc);
                    //            item._RecordState = RecordStateEnum.Added;
                    //        }
                    //        GRID_DROSS_MSTBL.SaveList(lstGRID_DROSS_MST, dcc);
                    //        break;

                    //    case "IB_CUTTING_DTL":
                    //        List<dcIB_CUTTING_DTL> lstIB_CUTTING_DTL = INV_IB_CUTTING_DTLBL.GetIB_CUTTING_DTL_MS(pcObj, null);
                    //        foreach (dcIB_CUTTING_DTL item in lstIB_CUTTING_DTL)
                    //        {
                    //            INV_IB_CUTTING_DTLBL.Delete_MS(item.CUTTING_DTL_ID, dcc);
                    //            item._RecordState = RecordStateEnum.Added;
                    //        }
                    //        INV_IB_CUTTING_DTLBL.SaveList(lstIB_CUTTING_DTL, dcc);
                    //        break;


                    //    case "IB_CUTTING_MST":
                    //        List<dcIB_CUTTING_MST> lstIB_CUTTING_MST = INV_IB_CUTTING_MSTBL.GetIB_CUTTING_MST_MS(pcObj, null);
                    //        foreach (dcIB_CUTTING_MST item in lstIB_CUTTING_MST)
                    //        {
                    //            INV_IB_CUTTING_MSTBL.Delete_MS(item.CUTTING_MST_ID, dcc);
                    //            item._RecordState = RecordStateEnum.Added;
                    //        }
                    //        INV_IB_CUTTING_MSTBL.SaveList(lstIB_CUTTING_MST, dcc);
                    //        break;



                    //    case "IMP_PURCHASE_DETAILS":
                    //        List<dcIMP_PURCHASE_DETAILS> lstIMP_PURCHASE_DETAILS = IMP_PURCHASE_DETAILSBL.GetIMP_PURCHASE_DETAILS_MS(pcObj, null);
                    //        foreach (dcIMP_PURCHASE_DETAILS item in lstIMP_PURCHASE_DETAILS)
                    //        {
                    //            IMP_PURCHASE_DETAILSBL.Delete_MS(item.IMP_PUR_DET_ID, dcc);
                    //            item._RecordState = RecordStateEnum.Added;
                    //        }
                    //        IMP_PURCHASE_DETAILSBL.SaveList(lstIMP_PURCHASE_DETAILS, dcc);
                    //        break;


                    //    case "IMP_PURCHASE_MASTER":
                    //        List<dcIMP_PURCHASE_MASTER> lstIMP_PURCHASE_MASTER = IMP_PURCHASE_MASTERRBL.GetIMP_PURCHASE_MASTER_MS(pcObj, null);
                    //        foreach (dcIMP_PURCHASE_MASTER item in lstIMP_PURCHASE_MASTER)
                    //        {
                    //            IMP_PURCHASE_MASTERRBL.Delete_MS(item.IMP_PURCHASE_ID, dcc);
                    //            item._RecordState = RecordStateEnum.Added;
                    //        }
                    //        IMP_PURCHASE_MASTERRBL.SaveList(lstIMP_PURCHASE_MASTER, dcc);
                    //        break;



                    //    case "INVOICE_DETAILS":
                    //        List<dcINVOICE_DETAILS> lstINVOICE_DETAILS = INVOICE_DETAILSBL.GetINVOICE_DETAILS_MS(pcObj, null);
                    //        foreach (dcINVOICE_DETAILS item in lstINVOICE_DETAILS)
                    //        {
                    //            INVOICE_DETAILSBL.Delete_MS(item.INVOICE_DET_ID, dcc);
                    //            item._RecordState = RecordStateEnum.Added;
                    //        }
                    //        INVOICE_DETAILSBL.SaveList(lstINVOICE_DETAILS, dcc);
                    //        break;

                    //    case "INVOICE_MASTER":
                    //        List<dcINVOICE_MASTER> lstINVOICE_MASTER = INVOICE_MASTERBL.GetINVOICE_MASTER_MS(pcObj, null);
                    //        foreach (dcINVOICE_MASTER item in lstINVOICE_MASTER)
                    //        {
                    //            INVOICE_MASTERBL.Delete_MS(item.INVOICE_ID, dcc);
                    //            item._RecordState = RecordStateEnum.Added;
                    //        }
                    //        INVOICE_MASTERBL.SaveList(lstINVOICE_MASTER, dcc);
                    //        break;


                    //    case "INV_ADJUST_DETAILS":
                    //        List<dcINV_ADJUST_DETAILS> lstINV_ADJUST_DETAILS = INV_ADJUST_DETAILSBL.GetINV_ADJUST_DETAILS_MS(pcObj, null);
                    //        foreach (dcINV_ADJUST_DETAILS item in lstINV_ADJUST_DETAILS)
                    //        {
                    //            INV_ADJUST_DETAILSBL.Delete_MS(item.INV_ADJUST_DET_ID, dcc);
                    //            item._RecordState = RecordStateEnum.Added;
                    //        }
                    //        INV_ADJUST_DETAILSBL.SaveList(lstINV_ADJUST_DETAILS, dcc);
                    //        break;

                    //    case "INV_ADJUST_MASTER":
                    //        List<dcINV_ADJUST_MASTER> lstINV_ADJUST_MASTER = INV_ADJUST_MASTERBL.GetINV_ADJUST_MASTER_MS(pcObj, null);
                    //        foreach (dcINV_ADJUST_MASTER item in lstINV_ADJUST_MASTER)
                    //        {
                    //            INV_ADJUST_MASTERBL.Delete_MS(item.INV_ADJUST_ID, dcc);
                    //            item._RecordState = RecordStateEnum.Added;
                    //        }
                    //        INV_ADJUST_MASTERBL.SaveList(lstINV_ADJUST_MASTER, dcc);
                    //        break;


                    //    case "INV_ADJUST_DETAILS_DEPT":
                    //        List<dcINV_ADJUST_DETAILS_DEPT> lstINV_ADJUST_DETAILS_DEPT = INV_ADJUST_DETAILS_DEPTBL.GetINV_ADJUST_DETAILS_DEPT_MS(pcObj, null);
                    //        foreach (dcINV_ADJUST_DETAILS_DEPT item in lstINV_ADJUST_DETAILS_DEPT)
                    //        {
                    //            INV_ADJUST_DETAILS_DEPTBL.Delete_MS(item.INV_ADJUST_DET_ID, dcc);
                    //            item._RecordState = RecordStateEnum.Added;
                    //        }
                    //        INV_ADJUST_DETAILS_DEPTBL.SaveList(lstINV_ADJUST_DETAILS_DEPT, dcc);
                    //        break;




                    //    case "INV_DEPARTMENT_PERMISSION":
                    //        List<dcINV_DEPARTMENT_PERMISSION> lstINV_DEPARTMENT_PERMISSION = INV_DEPARTMENT_PERMISSIONBL.GetINV_DEPARTMENT_PERMISSION_MS(pcObj, null);
                    //        foreach (dcINV_DEPARTMENT_PERMISSION item in lstINV_DEPARTMENT_PERMISSION)
                    //        {
                    //            INV_DEPARTMENT_PERMISSIONBL.Delete_MS(item.PERMISSION_ID, dcc);
                    //            item._RecordState = RecordStateEnum.Added;
                    //        }
                    //        INV_DEPARTMENT_PERMISSIONBL.SaveList(lstINV_DEPARTMENT_PERMISSION, dcc);
                    //        break;


                    //}
                    pcObj.IS_SUCCESS = "Y";
                }
            }
            catch
            {
                pcObj.IS_SUCCESS = "N";
            }
            finally
            {
                DataProcessUpdate(pcObj);
            }

        }

        protected static void DataProcessUpdate(dcDATATRANSFER_TABLE_MAP icObj)
        {
            dcDATATRANSFER_TABLE_LOG pcObj = new dcDATATRANSFER_TABLE_LOG();
            pcObj.TABLENAME = icObj.TABLENAME;
            pcObj.TRANSFERDATETIME = Conversion.DBNullDateToNull(icObj.toProdDate);
            pcObj.IS_SUCCESS = icObj.IS_SUCCESS;
            pcObj._RecordState = RecordStateEnum.Added;
            DATATRANSFER_TABLE_LOGBL.Save(pcObj);
        }

    }
}
