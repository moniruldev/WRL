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
    public class TRANSPORT_MEDIA_MSTBL
    {
        public static DataLoadOptions TRANSPORT_MEDIA_MSTLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcTRANSPORT_MEDIA_MST>(obj => obj.relatedclassname);
            return dlo;
        }

        public static string GetTransporterListSQLString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT TRANS_MEDIA_ID,TRANS_MEDIA_NAME ");
            sb.Append(" FROM TRANSPORT_MEDIA_MST  ");

            sb.Append(" WHERE 1=1 ");

            return sb.ToString();
        }
        public static List<dcTRANSPORT_MEDIA_MST> GetTRANSPORT_MEDIA_MSTList()
        {
            return GetTRANSPORT_MEDIA_MSTList(null, null);
        }
        public static List<dcTRANSPORT_MEDIA_MST> GetTRANSPORT_MEDIA_MSTList(DBContext dc)
        {
            return GetTRANSPORT_MEDIA_MSTList(null, dc);
        }
        public static List<dcTRANSPORT_MEDIA_MST> GetTRANSPORT_MEDIA_MSTList(DBQuery dbq, DBContext dc)
        {
            List<dcTRANSPORT_MEDIA_MST> cObjList = new List<dcTRANSPORT_MEDIA_MST>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcTRANSPORT_MEDIA_MST>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcTRANSPORT_MEDIA_MST GetTRANSPORT_MEDIA_MSTByID(int pTRANSPORT_MEDIA_MSTID)
        {
            return GetTRANSPORT_MEDIA_MSTByID(pTRANSPORT_MEDIA_MSTID, null);
        }
        public static dcTRANSPORT_MEDIA_MST GetTRANSPORT_MEDIA_MSTByID(int pTRANSPORT_MEDIA_MSTID, DBContext dc)
        {
            dcTRANSPORT_MEDIA_MST cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcTRANSPORT_MEDIA_MST>()
                                  where c.TRANS_MEDIA_ID == pTRANSPORT_MEDIA_MSTID
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

        public static int Insert(dcTRANSPORT_MEDIA_MST cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcTRANSPORT_MEDIA_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcTRANSPORT_MEDIA_MST>(cObj, true);
                if (id > 0) { cObj.TRANS_MEDIA_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcTRANSPORT_MEDIA_MST cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcTRANSPORT_MEDIA_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcTRANSPORT_MEDIA_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pTRANSPORT_MEDIA_MSTID)
        {
            return Delete(pTRANSPORT_MEDIA_MSTID, null);
        }
        public static bool Delete(int pTRANSPORT_MEDIA_MSTID, DBContext dc)
        {
            dcTRANSPORT_MEDIA_MST cObj = new dcTRANSPORT_MEDIA_MST();
            cObj.TRANS_MEDIA_ID = pTRANSPORT_MEDIA_MSTID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcTRANSPORT_MEDIA_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcTRANSPORT_MEDIA_MST cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcTRANSPORT_MEDIA_MST cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcTRANSPORT_MEDIA_MST cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcTRANSPORT_MEDIA_MST cObj, DBContext dc)
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
                                newID = cObj.TRANS_MEDIA_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.TRANS_MEDIA_ID, dc))
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

        public static bool SaveList(List<dcTRANSPORT_MEDIA_MST> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcTRANSPORT_MEDIA_MST> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcTRANSPORT_MEDIA_MST oDet in detList)
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
                    //    bool d = Delete(oDet.TRANSPORT_MEDIA_MSTID, dc);
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
