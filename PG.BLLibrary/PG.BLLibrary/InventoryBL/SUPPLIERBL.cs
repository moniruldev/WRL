using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Data.Linq;
using PG.Core.DBBase;
using PG.Core.Extentions;
using PG.Core.DBFilters;
using PG.DBClass.InventoryDC;


namespace PG.BLLibrary.InventoryBL
{
    public class SUPPLIERBL
    {

        public static string GetLastSUPCODE_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("   SELECT  'SUP-'|| LPAD(SUP_ID+1,6,'000000') SUP_CODE   FROM ( SELECT * FROM SUPPLIER_INFO ORDER BY SUP_ID DESC) ");
            sb.Append(" WHERE ROWNUM = 1 ");
            return sb.ToString();
        }

        public static dcSUPPLIER_INFO GetNEW_SUP_CODE(DBContext dc)
        {
            dcSUPPLIER_INFO cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();
                sb.Append(GetLastSUPCODE_SQLString());
                // cmdInfo.DBParametersInfo.Add("@custId", pCUSTOMER_MSTID);
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                cObj = DBQuery.ExecuteDBQuery<dcSUPPLIER_INFO>(dbq, dc).FirstOrDefault();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObj;
        }

        public static dcSUPPLIER_INFO GetSupplierById(int id)
        {
            return GetSupplierById(id, null);
        }
        public static dcSUPPLIER_INFO GetSupplierById(int id, DBContext dc)
        {
            dcSUPPLIER_INFO cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetSupplier_SQLString());
                sb.Append(" and SUP_ID =@supId ");
                cmdInfo.DBParametersInfo.Add("@supId", id);
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcSUPPLIER_INFO>(dbq, dc).FirstOrDefault();

            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
       

        public static DataLoadOptions SUPPLIERLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcSUPPLIER_INFO>(obj => obj.relatedclassname);
            return dlo;
        }

        public static string GetSupplier_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT SUPPLIER_INFO.*  FROM SUPPLIER_INFO");
            sb.Append(" WHERE 1=1 ");
            //sb.Append(" order by SUP_NAME ");
            return sb.ToString();
        }

        public static string GetSupplierWithCommon_SQLString(string pSupType)
        {
            StringBuilder sb = new StringBuilder();
            //sb.Append("  SELECT A.* FROM( ");
            sb.Append(" SELECT SUP.SUP_ID,SUP.SUP_NAME,SUP.SUP_CODE,SUP.SUP_ADDRESS,SUP.SUP_PHONE ");
            sb.Append(" FROM SUPPLIER_INFO sup ");
            sb.Append(" WHERE 1=1 ");
            //sb.Append(" AND SUP.IS_COMMON ='N' ");
            if(pSupType != "")
            {
                sb.Append(" AND SUP.SUP_TYPE IN ('C','" + pSupType + "') ");
            }
            //sb.Append(" UNION ALL ");
            //sb.Append(" SELECT SI.SUP_ID,SI.SUP_NAME,SI.SUP_CODE,SI.SUP_ADDRESS,SI.SUP_PHONE ");
            //sb.Append(" FROM SUPPLIER_INFO SI ");
            //sb.Append(" WHERE 1=1 ");
            //sb.Append(" AND SI.IS_COMMON ='Y' ");
            //sb.Append(" ) A ");
            //sb.Append(" WHERE 1=1 ");
            
           
            return sb.ToString();
        }




        public static string GetManufact_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT SUPPLIER_INFO.*,COUNTRY_MASTER.COUNTRY_NAME FROM SUPPLIER_INFO INNER JOIN COUNTRY_MASTER ON SUPPLIER_INFO.COUNTRY_ID=COUNTRY_MASTER.COUNTRY_ID ");
            sb.Append(" WHERE 1=1 ");
            //sb.Append(" order by SUP_NAME ");
            return sb.ToString();
        }
        public static List<dcSUPPLIER_INFO> GetSupplierList()
        {
            return GetSupplierList(null);
        }
        public static List<dcSUPPLIER_INFO> GetSupplierList(DBContext dc)
        {
            List<dcSUPPLIER_INFO> cObjList = new List<dcSUPPLIER_INFO>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetSupplier_SQLString());
                sb.Append(" order by SUP_NAME asc ");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcSUPPLIER_INFO>(dbq, dc).ToList();

            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }


        public static List<dcSUPPLIER_INFO> GetSupplierList(DBQuery dbq, DBContext dc)
        {
            List<dcSUPPLIER_INFO> cObjList = new List<dcSUPPLIER_INFO>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                if (dbq == null)
                {
                    dbq = new DBQuery();
                    //dbq.OrderBy = "PeriodStartDate";
                }
                cObjList = DBQuery.ExecuteDBQuery<dcSUPPLIER_INFO>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }



        public static List<dcSUPPLIER_INFO> GetManufactList(DBQuery dbq, DBContext dc)
        {
            List<dcSUPPLIER_INFO> cObjList = new List<dcSUPPLIER_INFO>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                if (dbq == null)
                {
                    dbq = new DBQuery();
                    //dbq.OrderBy = "PeriodStartDate";
                }
                cObjList = DBQuery.ExecuteDBQuery<dcSUPPLIER_INFO>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }


        public static bool Insert(dcSUPPLIER_INFO cObj)
        {
            if (cObj.SUP_ID != 0)
            {
                return Update(cObj, null);
            }
            else
            {
                return Insert(cObj, null);
            }

        }

        public static bool Insert(dcSUPPLIER_INFO cObj, DBContext dc)
        {
            List<dcSUPPLIER_INFO> objList = new List<dcSUPPLIER_INFO>();
            bool isDCInit = false;
            int cnt = 0;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetLastSupplier_SQLString());

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                objList = DBQuery.ExecuteDBQuery<dcSUPPLIER_INFO>(dbq, dc).ToList();
                if (objList.Count != 0)
                {
                    id = objList[0].SUP_ID + 1;
                }
                else
                {
                    id = 1;
                }
                if (id > 0) { cObj.SUP_ID = id; }
                if (cObj != null)
                {
                    dcSUPPLIER_INFO objItem = new dcSUPPLIER_INFO();
                    objItem.SUP_ID = cObj.SUP_ID;
                    objItem.SUP_CODE = cObj.SUP_CODE;
                    objItem.SUP_NAME = cObj.SUP_NAME;
                    objItem.SUP_ADDRESS = cObj.SUP_ADDRESS;
                    objItem.SUP_PHONE = cObj.SUP_PHONE;
                    objItem.SUP_PHONE2 = cObj.SUP_PHONE2;
                    objItem.EMAIL = cObj.EMAIL;
                    objItem.COUNTRY_ID = cObj.COUNTRY_ID;
                    objItem.VAT_REG_NO = cObj.VAT_REG_NO;
                    objItem.ADVISE_BANK = cObj.ADVISE_BANK;
                    objItem.CONTACT_PERSON = cObj.CONTACT_PERSON;
                    objItem.CONTACT_PERSON_PHONE = cObj.CONTACT_PERSON_PHONE;
                    objItem.CONTACT_PERSON_PHONE2 = cObj.CONTACT_PERSON_PHONE2;
                    objItem.IS_MANUFACTURER = cObj.IS_MANUFACTURER;
                    objItem.IS_ENLISTED = cObj.IS_ENLISTED;
                    objItem.SUP_TYPE = cObj.SUP_TYPE;
                    objItem.COMPANY_ID = cObj.COMPANY_ID;
                    cnt = dc.DoInsert<dcSUPPLIER_INFO>(objItem);
                }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }
        public static string GetLastSupplier_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT * FROM ( SELECT * FROM SUPPLIER_INFO ORDER BY Sup_ID DESC) ");
            sb.Append(" WHERE ROWNUM = 1 ");
            return sb.ToString();
        }
        //public static int Insert(dcSUPPLIER_INFO cObj)
        //{
        //    return Insert(cObj, null);
        //}

        //public static int Insert(dcSUPPLIER_INFO cObj, DBContext dc)
        //{
        //    bool isDCInit = false;
        //    int id = 0;
        //    isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
        //    using (DataContext dataContext = dc.NewDataContext())
        //    {
        //        id = dc.DoInsert<dcSUPPLIER_INFO>(cObj, true);
        //       // if (id > 0) { cObj.SUPPLIERID = id; }
        //    }
        //    DBContextManager.ReleaseDBContext(ref dc, isDCInit);
        //    return id;
        //}

        public static bool Update(dcSUPPLIER_INFO cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcSUPPLIER_INFO cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;

            dcSUPPLIER_INFO key = new dcSUPPLIER_INFO();
            key.SUP_ID = cObj.SUP_ID;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcSUPPLIER_INFO>(cObj, key);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pSUPPLIERID)
        {
            return Delete(pSUPPLIERID, null);
        }
        public static bool Delete(int pSUPPLIERID, DBContext dc)
        {
            dcSUPPLIER_INFO cObj = new dcSUPPLIER_INFO();
            cObj.SUP_ID = pSUPPLIERID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcSUPPLIER_INFO>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcSUPPLIER_INFO cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcSUPPLIER_INFO cObj, bool isAdd, DBContext dc)
        {
          //  cObj._RecordState = isAdd ? Interwave.Core.DBClass.RecordStateEnum.Added : Interwave.Core.DBClass.RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcSUPPLIER_INFO cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcSUPPLIER_INFO cObj, DBContext dc)
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
                        //case Interwave.Core.DBClass.RecordStateEnum.Added:
                        //    newID = Insert(cObj, dc);
                        //    break;
                        //case Interwave.Core.DBClass.RecordStateEnum.Edited:
                        //    if (Update(cObj, dc))
                        //    {
                        //        newID = cObj.SUPPLIERID;
                        //    }
                        //    break;
                        //case Interwave.Core.DBClass.RecordStateEnum.Deleted:
                        //    if (Delete(cObj.SUPPLIERID, dc))
                        //    {
                        //        newID = 1;
                        //    }
                        //    break;
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

        public static bool SaveList(List<dcSUPPLIER_INFO> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcSUPPLIER_INFO> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcSUPPLIER_INFO oDet in detList)
            {
                //switch (oDet._RecordState)
                //{
                    //case Interwave.Core.DBClass.RecordStateEnum.Added:
                    //    int a = Insert(oDet, dc);
                    //    break;
                    //case Interwave.Core.DBClass.RecordStateEnum.Edited:
                    //    bool e = Update(oDet, dc);
                    //    break;
                    //case Interwave.Core.DBClass.RecordStateEnum.Deleted:
                    //    bool d = Delete(oDet.SUPPLIERID, dc);
                    //    break;
                    //default:
                    //    break;
                //}
            }
            dc.CommitTransaction(isTransInit);
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            bStatus = true;
            return bStatus;
        }
    }
}
