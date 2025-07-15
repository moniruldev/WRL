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
    public class THANA_TOWN_MSTBL
    {
        public static DataLoadOptions THANA_TOWN_MSTLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcTHANA_TOWN_MST>(obj => obj.relatedclassname);
            return dlo;
        }
        public static string GetTownSQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT T.*,D.DIST_NAME  ");
            sb.Append(" FROM THANA_TOWN_MST T ");
            sb.Append(" INNER JOIN DISTRICT_MST D ON T.DIST_ID=D.DIST_ID ");
            sb.Append(" WHERE 1=1 ");

            return sb.ToString();
        }
        public static List<dcTHANA_TOWN_MST> GetTHANA_TOWN_MSTList()
        {
            return GetTHANA_TOWN_MSTList(null, null);
        }
        public static List<dcTHANA_TOWN_MST> GetTHANA_TOWN_MSTList(DBContext dc)
        {
            return GetTHANA_TOWN_MSTList(null, dc);
        }
        public static List<dcTHANA_TOWN_MST> GetTHANA_TOWN_MSTList(DBQuery dbq, DBContext dc)
        {
            List<dcTHANA_TOWN_MST> cObjList = new List<dcTHANA_TOWN_MST>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcTHANA_TOWN_MST>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcTHANA_TOWN_MST GetTHANA_TOWN_MSTByID(int pTHANA_TOWN_MSTID)
        {
            return GetTHANA_TOWN_MSTByID(pTHANA_TOWN_MSTID, null);
        }
        public static dcTHANA_TOWN_MST GetTHANA_TOWN_MSTByID(int pTHANA_TOWN_MSTID, DBContext dc)
        {
            dcTHANA_TOWN_MST cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcTHANA_TOWN_MST>()
                                  where c.TOWN_ID == pTHANA_TOWN_MSTID
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

        public static int Insert(dcTHANA_TOWN_MST cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcTHANA_TOWN_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcTHANA_TOWN_MST>(cObj, true);
                if (id > 0) { cObj.TOWN_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcTHANA_TOWN_MST cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcTHANA_TOWN_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcTHANA_TOWN_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pTHANA_TOWN_MSTID)
        {
            return Delete(pTHANA_TOWN_MSTID, null);
        }
        public static bool Delete(int pTHANA_TOWN_MSTID, DBContext dc)
        {
            dcTHANA_TOWN_MST cObj = new dcTHANA_TOWN_MST();
            cObj.TOWN_ID = pTHANA_TOWN_MSTID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcTHANA_TOWN_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcTHANA_TOWN_MST cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcTHANA_TOWN_MST cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcTHANA_TOWN_MST cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcTHANA_TOWN_MST cObj, DBContext dc)
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
                                newID = cObj.TOWN_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.TOWN_ID, dc))
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

        public static bool SaveList(List<dcTHANA_TOWN_MST> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcTHANA_TOWN_MST> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcTHANA_TOWN_MST oDet in detList)
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
                        bool d = Delete(oDet.TOWN_ID, dc);
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
