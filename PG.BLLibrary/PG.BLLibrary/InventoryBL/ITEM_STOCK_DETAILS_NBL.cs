using PG.Core.DBBase;
using PG.DBClass.InventoryDC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Text;

namespace PG.BLLibrary.InventoryBL
{
    public class ITEM_STOCK_DETAILS_NBL
    {
        public static DataLoadOptions ITEM_STOCK_DETAILS_NLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcITEM_STOCK_DETAILS>(obj => obj.relatedclassname);
            return dlo;
        }

        // Stock Details

        public static string GetItemStockDtl_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT * FROM  ITEM_STOCK_DETAILS stk WHERE 1=1 ");
            return sb.ToString();
        }


        //Invoice
        public static string GetInvoicebyIDforStkListString()
        {
            StringBuilder sb = new StringBuilder();

           /* sb.Append(" Select a.INVOICE_DATE TRANS_DATE,201 INV_TRANS_TYPE_ID,b.INVOICE_DET_ID INV_TRANS_DET_ID,b.ITEM_ID,b.UOM_ID,b.ITEM_QNTY_APRROVED ISS_QTY,0 RCV_QTY,0 TRANS_QTY ");
            sb.Append(" ,a.INVOICE_NO TRANS_REF_NO,'Form Invoice Challan' TRANS_REMARKS,1 STORE_ID,SysDate TRANS_TIME,a.CREATE_BY,SysDate CREATE_DATE FROM INVOICE_MASTER a ");
            sb.Append(" INNER JOIN INVOICE_DETAILS b ON a.INVOICE_ID=b.INVOICE_ID ");

            sb.Append(" Where 1=1 ");
            sb.Append(" and a.IS_APPROVED='Y'");*/
            sb.Append(" Select a.DC_DATE TRANS_DATE,201 INV_TRANS_TYPE_ID,b.DC_DET_ID INV_TRANS_DET_ID,b.ITEM_ID,b.UOM_ID,b.ITEM_QTY ISS_QTY,0 RCV_QTY,b.ITEM_QTY TRANS_QTY ");
            sb.Append(" ,a.DC_NO TRANS_REF_NO,'Form DC Challan' TRANS_REMARKS,1 STORE_ID,SysDate TRANS_TIME,a.CREATE_BY,SysDate CREATE_DATE ");
            sb.Append(" FROM DC_MASTER a ");
            sb.Append(" INNER JOIN DC_DETAILS b ON a.DC_ID=b.DC_ID ");

            sb.Append("  Where 1=1 ");

            return sb.ToString();
        }

        //public static List<dcITEM_STOCK_DETAILS> GetSTKDtlforPROD_NO(int pITEM_STK_DET_ID, string pTRANS_REF_NO, int pINV_TRANS_TYPE_ID, DBContext dc)
        //{
        //    List<dcITEM_STOCK_DETAILS> cObjList = new List<dcITEM_STOCK_DETAILS>();
        //    bool isDCInit = false;
        //    try
        //    {
        //        isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
        //        //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
        //        DBCommandInfo cmdInfo = new DBCommandInfo();
        //        StringBuilder sb = new StringBuilder(GetItemStockDtl_SQLString());


        //        if(pITEM_STK_DET_ID>0)
        //        {
        //            sb.Append(" AND stk.ITEM_STK_DET_ID=@ITEM_STK_DET_ID ");
        //            cmdInfo.DBParametersInfo.Add("@ITEM_STK_DET_ID", pITEM_STK_DET_ID);
        //        }

        //        if(pTRANS_REF_NO != "")
        //        {
        //            sb.Append(" AND stk.TRANS_REF_NO=@TRANS_REF_NO ");
        //            cmdInfo.DBParametersInfo.Add("@TRANS_REF_NO", pTRANS_REF_NO);
        //        }

        //        if (pINV_TRANS_TYPE_ID > 0)
        //        {
        //            sb.Append(" AND stk.INV_TRANS_TYPE_ID=@INV_TRANS_TYPE_ID ");
        //            cmdInfo.DBParametersInfo.Add("@INV_TRANS_TYPE_ID", pINV_TRANS_TYPE_ID);
        //        }



        //        //sb.Append(" ORDER BY a.DC_DATE DESC ");

        //        DBQuery dbq = new DBQuery();
        //        dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
        //        cmdInfo.CommandText = sb.ToString();
        //        cmdInfo.CommandType = CommandType.Text;
        //        dbq.DBCommandInfo = cmdInfo;
        //        //cmd.CommandType = CommandType.Text;
        //        //cmd.CommandText = sb.ToString();

        //        //DBQuery dbq = new DBQuery();
        //        //dbq.DBQueryMode = PG.Core.DBBase.DBQueryModeEnum.DBCommand;
        //        //dbq.DBCommand = cmd;

        //        cObjList = GetITEM_STOCK_DETAILS_NList(dbq, dc);
        //    }
        //    catch { throw; }
        //    finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
        //    return cObjList;
        //}

        public static List<dcITEM_STOCK_DETAILS> GetInvoiceListbyInvIDforStock(int pInvoiceID,string pDcno)
        {
            return GetInvoiceListbyInvIDforStock(pInvoiceID,pDcno, null);
        }

        public static List<dcITEM_STOCK_DETAILS> GetInvoiceListbyInvIDforStock(int pInvoiceID,string pDcno, DBContext dc)
        {
            List<dcITEM_STOCK_DETAILS> cObjList = new List<dcITEM_STOCK_DETAILS>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetInvoicebyIDforStkListString());



                if (pInvoiceID > 0)
                {
                    sb.Append(" AND a.INVOICE_ID=@InvoiceID ");
                    cmdInfo.DBParametersInfo.Add("@InvoiceID", pInvoiceID);
                }

                if (pDcno!=string.Empty)
                {
                    sb.Append(" AND a.DC_NO=@pDcno ");
                    cmdInfo.DBParametersInfo.Add("@pDcno", pDcno);

                }



                sb.Append(" ORDER BY a.DC_DATE DESC ");

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

                cObjList = GetITEM_STOCK_DETAILS_NList(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }


        //Out station Transfer

        public static string GetInvoicebyIDforStkoutstatListString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" Select a.INVOICE_DATE TRANS_DATE,404 INV_TRANS_TYPE_ID,b.INVOICE_DET_ID INV_TRANS_DET_ID,b.ITEM_ID,b.UOM_ID,b.ITEM_QNTY_APRROVED ISS_QTY,0 RCV_QTY,0 TRANS_QTY ");
            sb.Append(" ,a.INVOICE_NO TRANS_REF_NO,'Form Invoice Challan' TRANS_REMARKS,1 STORE_ID,26 DEPARTMENT_ID,26 FROM_DEPARTMENT_ID,SysDate TRANS_TIME,a.CREATE_BY,SysDate CREATE_DATE FROM INVOICE_MASTER a ");
            sb.Append(" INNER JOIN INVOICE_DETAILS b ON a.INVOICE_ID=b.INVOICE_ID ");

            sb.Append(" Where 1=1 ");
            sb.Append(" and a.IS_APPROVED='Y'");

            return sb.ToString();
        }

        public static List<dcITEM_STOCK_DETAILS> GetInvoiceListbyInvIDforoutstatStock(int pInvoiceID)
        {
            return GetInvoiceListbyInvIDforoutstatStock(pInvoiceID, null);
        }

        public static List<dcITEM_STOCK_DETAILS> GetInvoiceListbyInvIDforoutstatStock(int pInvoiceID, DBContext dc)
        {
            List<dcITEM_STOCK_DETAILS> cObjList = new List<dcITEM_STOCK_DETAILS>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetInvoicebyIDforStkoutstatListString());




                sb.Append(" AND b.INVOICE_ID=@InvoiceID ");
                //cmd.Parameters.AddWithValue("@companyID", pCompanyID);
                cmdInfo.DBParametersInfo.Add("@InvoiceID", pInvoiceID);



                sb.Append(" ORDER BY a.INVOICE_DATE DESC ");

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

                cObjList = GetITEM_STOCK_DETAILS_NList(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }


        //Out Station Transfer For Rotary


        public static string GetInvoicebyIDforStoutstatrotarykListString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" Select a.INVOICE_DATE TRANS_DATE,405 INV_TRANS_TYPE_ID,b.INVOICE_DET_ID INV_TRANS_DET_ID,b.ITEM_ID,b.UOM_ID,b.ITEM_QNTY_APRROVED ISS_QTY,0 RCV_QTY,0 TRANS_QTY ");
            sb.Append(" ,a.INVOICE_NO TRANS_REF_NO,'Form Invoice Challan' TRANS_REMARKS,1 STORE_ID,SysDate TRANS_TIME,a.CREATE_BY,SysDate CREATE_DATE,a.BTY_TYPE_ID,a.IS_REPAIR FROM INVOICE_MASTER a ");
            sb.Append(" INNER JOIN INVOICE_DETAILS b ON a.INVOICE_ID=b.INVOICE_ID ");

            sb.Append(" Where 1=1 ");
            sb.Append(" and a.IS_APPROVED='Y'");

            return sb.ToString();
        }

        public static List<dcITEM_STOCK_DETAILS> GetInvoiceListbyInvIDforoutstatRotaryStock(int pInvoiceID)
        {
            return GetInvoiceListbyInvIDforoutstatRotaryStock(pInvoiceID, null);
        }

        public static List<dcITEM_STOCK_DETAILS> GetInvoiceListbyInvIDforoutstatRotaryStock(int pInvoiceID, DBContext dc)
        {
            List<dcITEM_STOCK_DETAILS> cObjList = new List<dcITEM_STOCK_DETAILS>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetInvoicebyIDforStoutstatrotarykListString());




                sb.Append(" AND b.INVOICE_ID=@InvoiceID ");
                //cmd.Parameters.AddWithValue("@companyID", pCompanyID);
                cmdInfo.DBParametersInfo.Add("@InvoiceID", pInvoiceID);



                sb.Append(" ORDER BY a.INVOICE_DATE DESC ");

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

                cObjList = GetITEM_STOCK_DETAILS_NList(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }



        public static List<dcITEM_STOCK_DETAILS> GetITEM_STOCK_DETAILS_NList()
        {
            return GetITEM_STOCK_DETAILS_NList(null, null);
        }
        public static List<dcITEM_STOCK_DETAILS> GetITEM_STOCK_DETAILS_NList(DBContext dc)
        {
            return GetITEM_STOCK_DETAILS_NList(null, dc);
        }
        public static List<dcITEM_STOCK_DETAILS> GetITEM_STOCK_DETAILS_NList(DBQuery dbq, DBContext dc)
        {
            List<dcITEM_STOCK_DETAILS> cObjList = new List<dcITEM_STOCK_DETAILS>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcITEM_STOCK_DETAILS>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcITEM_STOCK_DETAILS GetITEM_STOCK_DETAILS_NByID(int pITEM_STOCK_DETAILS_NID)
        {
            return GetITEM_STOCK_DETAILS_NByID(pITEM_STOCK_DETAILS_NID, null);
        }
        public static dcITEM_STOCK_DETAILS GetITEM_STOCK_DETAILS_NByID(int pITEM_STOCK_DETAILS_NID, DBContext dc)
        {
            dcITEM_STOCK_DETAILS cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcITEM_STOCK_DETAILS>()
                                  //where c.ITEM_STOCK_DETAILS_NID == pITEM_STOCK_DETAILS_NID
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

        public static int Insert(dcITEM_STOCK_DETAILS cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcITEM_STOCK_DETAILS cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcITEM_STOCK_DETAILS>(cObj, true);
                if (id > 0) { cObj.ITEM_STK_DET_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcITEM_STOCK_DETAILS cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcITEM_STOCK_DETAILS cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcITEM_STOCK_DETAILS>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pITEM_STOCK_DETAILS_NID)
        {
            return Delete(pITEM_STOCK_DETAILS_NID, null);
        }
        public static bool Delete(int pITEM_STOCK_DETAILS_NID, DBContext dc)
        {
            dcITEM_STOCK_DETAILS cObj = new dcITEM_STOCK_DETAILS();
            cObj.ITEM_STK_DET_ID = pITEM_STOCK_DETAILS_NID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcITEM_STOCK_DETAILS>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }


        public static bool Delete(string pTRANS_REF_NO, int pInvTransTypeID, DBContext dc)
        {
            dcITEM_STOCK_DETAILS cObj = new dcITEM_STOCK_DETAILS();
            cObj.TRANS_REF_NO = pTRANS_REF_NO;
            cObj.INV_TRANS_TYPE_ID = pInvTransTypeID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcITEM_STOCK_DETAILS>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(string pTRANS_REF_NO, DBContext dc)
        {
            dcITEM_STOCK_DETAILS cObj = new dcITEM_STOCK_DETAILS();
            cObj.TRANS_REF_NO = pTRANS_REF_NO;        
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcITEM_STOCK_DETAILS>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }
        public static int Save(dcITEM_STOCK_DETAILS cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcITEM_STOCK_DETAILS cObj, bool isAdd, DBContext dc)
        {
            //cObj._RecordState = isAdd ? Interwave.Core.DBClass.RecordStateEnum.Added : Interwave.Core.DBClass.RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcITEM_STOCK_DETAILS cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcITEM_STOCK_DETAILS cObj, DBContext dc)
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
                       /* case Interwave.Core.DBClass.RecordStateEnum.Added:
                            newID = Insert(cObj, dc);
                            break;
                        case Interwave.Core.DBClass.RecordStateEnum.Edited:
                            if (Update(cObj, dc))
                            {
                                newID = cObj.ITEM_STOCK_DETAILS_NID;
                            }
                            break;
                        case Interwave.Core.DBClass.RecordStateEnum.Deleted:
                            if (Delete(cObj.ITEM_STOCK_DETAILS_NID, dc))
                            {
                                newID = 1;
                            }
                            break;*/
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

        public static bool SaveList(List<dcITEM_STOCK_DETAILS> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcITEM_STOCK_DETAILS> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcITEM_STOCK_DETAILS oDet in detList)
            {
                //switch (oDet._RecordState)
                //{
                //    case RecordStateEnum.Added:
            int newID = Insert(oDet, dc);
                //        break;
                    
                //    default:
                //        break;
                //}
            }
            dc.CommitTransaction(isTransInit);
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            bStatus = true;
            return bStatus;
        }


        public static List<dcITEM_STOCK_DETAILS> GetSalesRetunListbyRTNIDforStock(int pRTNID)
        {
            return GetSalesRetunListbyRTNIDforStock(pRTNID, null);
        }

        public static List<dcITEM_STOCK_DETAILS> GetSalesRetunListbyRTNIDforStock(int pRTNID, DBContext dc)
        {
            List<dcITEM_STOCK_DETAILS> cObjList = new List<dcITEM_STOCK_DETAILS>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                sb.Append(" Select a.RTN_DATE TRANS_DATE,202 INV_TRANS_TYPE_ID,b.RTN_DET_ID INV_TRANS_DET_ID,b.ITEM_ID,b.UOM_ID,0 ISS_QTY,b.RTN_QNTY RCV_QTY,b.RTN_QNTY TRANS_QTY ");
                 sb.Append(" ,a.RTN_NO TRANS_REF_NO,'From Sales Return' TRANS_REMARKS,1 STORE_ID,SysDate TRANS_TIME,a.CREATE_BY,SysDate CREATE_DATE ");
                 sb.Append(" FROM  SALES_RETURN_MASTER a ");
                 sb.Append(" INNER JOIN SALES_RETURN_DETAILS b ON a.RTN_ID=b.RTN_ID ");
                sb.Append(" Where 1=1 ");
                sb.Append(" and a.AUTH_STATUS='A'");
                sb.Append(" AND a.RTN_ID=@pRTNID ");
                //cmd.Parameters.AddWithValue("@companyID", pCompanyID);
                cmdInfo.DBParametersInfo.Add("@pRTNID", pRTNID);
                sb.Append(" ORDER BY a.RTN_DATE DESC ");

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

                cObjList = GetITEM_STOCK_DETAILS_NList(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }


        public static int getstockCountbyDO(string DCNO)
        {
            return getstockCountbyDO(DCNO, null);
        }
        public static int getstockCountbyDO(string DCNO, DBContext dc)
        {
            //dcINVOICE_MASTER cObj = null;
            bool isDCInit = false;
            int  vcount = 0;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder("SELECT   FN_IS_STOCK_BY_GP_COM(@DCNO) FROM DUAL");
                cmdInfo.DBParametersInfo.Add("@DCNO", DCNO);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                vcount = Convert.ToInt32(DBQuery.ExecuteDBScalar(dbq, dc));
                //cObj = DBQuery.ExecuteDBQuery<dcINVOICE_MASTER>(dbq, dc).FirstOrDefault();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return vcount;
        }

        public static string Delete_Stock_DTLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" DELETE FROM   ITEM_STOCK_DETAILS b   ");
            return sb.ToString();
        }

        //Delete Stock Details
        public static void DeleteStockDtl(string pDCNo, DBContext dc)
        {
            List<dcITEM_STOCK_DETAILS> cObjList = new List<dcITEM_STOCK_DETAILS>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder(Delete_Stock_DTLString());

                    if (pDCNo != string.Empty)
                    {
                        sb.Append(" WHERE b.TRANS_REF_NO=@pDCNo ");
                        cmdInfo.DBParametersInfo.Add("@pDCNo", pDCNo);
                    }

                    DBQuery dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcITEM_STOCK_DETAILS>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }

        }

    }
}
