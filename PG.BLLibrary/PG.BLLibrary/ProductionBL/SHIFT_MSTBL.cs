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
    public class SHIFT_MSTBL
    {
        public static DataLoadOptions SHIFT_MSTLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcSHIFT_MST>(obj => obj.relatedclassname);
            return dlo;
        }

        public static string GetShiftListString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" Select *  ");
            sb.Append(" FROM SHIFT_MST mst ");
            sb.Append(" Where 1=1 ");

            return sb.ToString();
        }



        public static List<dcSHIFT_MST> GetShiftListBYShiftID(string shifID, DBContext dc)
        {
            List<dcSHIFT_MST> cObjList = new List<dcSHIFT_MST>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetShiftListString());
                if (shifID != "")
                {
                    sb.Append(" AND mst.SHIFT_ID= @SHIFT_ID ");
                    cmdInfo.DBParametersInfo.Add("@SHIFT_ID", shifID);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcSHIFT_MST>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }



        public static List<dcSHIFT_MST> GetShiftListBYShiftID(string pShiftID)
        {
            return GetShiftListBYShiftID(pShiftID, null);
        }



        public static List<dcSHIFT_MST> GetSHIFT_MSTList()
        {
            return GetSHIFT_MSTList(null, null);
        }
        public static List<dcSHIFT_MST> GetSHIFT_MSTList(DBContext dc)
        {
            return GetSHIFT_MSTList(null, dc);
        }
        public static List<dcSHIFT_MST> GetSHIFT_MSTList(DBQuery dbq, DBContext dc)
        {

            List<dcSHIFT_MST> cObjList = new List<dcSHIFT_MST>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder(GetShiftListString());                   
                    dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcSHIFT_MST>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;

         
        }
        public static dcSHIFT_MST GetSHIFT_MSTByID(int pSHIFT_MSTID)
        {
            return GetSHIFT_MSTByID(pSHIFT_MSTID, null);
        }
        public static dcSHIFT_MST GetSHIFT_MSTByID(int pSHIFT_MSTID, DBContext dc)
        {





            dcSHIFT_MST cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcSHIFT_MST>()
                                  where c.SHIFT_MSTID == pSHIFT_MSTID
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

        public static int Insert(dcSHIFT_MST cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcSHIFT_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcSHIFT_MST>(cObj, true);
                if (id > 0) { cObj.SHIFT_MSTID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcSHIFT_MST cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcSHIFT_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcSHIFT_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pSHIFT_MSTID)
        {
            return Delete(pSHIFT_MSTID, null);
        }
        public static bool Delete(int pSHIFT_MSTID, DBContext dc)
        {
            dcSHIFT_MST cObj = new dcSHIFT_MST();
            cObj.SHIFT_MSTID = pSHIFT_MSTID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcSHIFT_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcSHIFT_MST cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcSHIFT_MST cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcSHIFT_MST cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcSHIFT_MST cObj, DBContext dc)
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
                                newID = cObj.SHIFT_MSTID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.SHIFT_MSTID, dc))
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

        public static bool SaveList(List<dcSHIFT_MST> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcSHIFT_MST> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcSHIFT_MST oDet in detList)
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
                        bool d = Delete(oDet.SHIFT_MSTID, dc);
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
    }
}
