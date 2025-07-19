using PG.Core.DBBase;
using PG.DBClass.WRELDC;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.BLLibrary.WRElBL
{
    public class AGREEMENT_MSTBL
    {
        public static DataLoadOptions AGREEMENT_MSTLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcAGREEMENT_MST>(obj => obj.relatedclassname);
            return dlo;
        }
        public static List<dcAGREEMENT_MST> GetAGREEMENT_MSTList()
        {
            return GetAGREEMENT_MSTList(null, null);
        }
        public static List<dcAGREEMENT_MST> GetAGREEMENT_MSTList(DBContext dc)
        {
            return GetAGREEMENT_MSTList(null, dc);
        }
        public static List<dcAGREEMENT_MST> GetAGREEMENT_MSTList(DBQuery dbq, DBContext dc)
        {
            List<dcAGREEMENT_MST> cObjList = new List<dcAGREEMENT_MST>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcAGREEMENT_MST>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcAGREEMENT_MST GetAGREEMENT_MSTByID(int pAGREEMENT_MSTID)
        {
            return GetAGREEMENT_MSTByID(pAGREEMENT_MSTID, null);
        }
        public static dcAGREEMENT_MST GetAGREEMENT_MSTByID(int pAGREEMENT_MSTID, DBContext dc)
        {
            dcAGREEMENT_MST cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcAGREEMENT_MST>()
                                  where c.AGR_ID == pAGREEMENT_MSTID
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

        public static int Insert(dcAGREEMENT_MST cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcAGREEMENT_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcAGREEMENT_MST>(cObj, true);
                if (id > 0) { cObj.AGR_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcAGREEMENT_MST cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcAGREEMENT_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcAGREEMENT_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pAGREEMENT_MSTID)
        {
            return Delete(pAGREEMENT_MSTID, null);
        }
        public static bool Delete(int pAGREEMENT_MSTID, DBContext dc)
        {
            dcAGREEMENT_MST cObj = new dcAGREEMENT_MST();
            cObj.AGR_ID = pAGREEMENT_MSTID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcAGREEMENT_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcAGREEMENT_MST cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcAGREEMENT_MST cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcAGREEMENT_MST cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcAGREEMENT_MST cObj, DBContext dc)
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
                                newID = cObj.AGR_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.AGR_ID, dc))
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

                        if (cObj.agreementDetails != null)
                        {
                            foreach (dcAGREEMENT_DETAILL det in cObj.agreementDetails)
                            {
                                det.AGR_ID = newID;
                            }
                            bStatus = AGREEMENT_DETAILLBL.SaveList(cObj.agreementDetails, dc);
                        }

                        //bStatus = true;
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

        public static bool SaveList(List<dcAGREEMENT_MST> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcAGREEMENT_MST> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcAGREEMENT_MST oDet in detList)
            {
                switch (oDet._RecordState)
                {
                    //case Interwave.Core.DBClass.RecordStateEnum.Added:
                    //    int a = Insert(oDet, dc);
                    //    break;
                    //case Interwave.Core.DBClass.RecordStateEnum.Edited:
                    //    bool e = Update(oDet, dc);
                    //    break;
                    //case Interwave.Core.DBClass.RecordStateEnum.Deleted:
                    //    bool d = Delete(oDet.AGREEMENT_MSTID, dc);
                    //    break;
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
