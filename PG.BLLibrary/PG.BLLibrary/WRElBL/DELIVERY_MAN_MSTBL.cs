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
    public class DELIVERY_MAN_MSTBL
    {
        public static DataLoadOptions DELIVERY_MAN_MSTLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcDELIVERY_MAN_MST>(obj => obj.relatedclassname);
            return dlo;
        }
        public static List<dcDELIVERY_MAN_MST> GetDELIVERY_MAN_MSTList()
        {
            return GetDELIVERY_MAN_MSTList(null, null);
        }
        public static List<dcDELIVERY_MAN_MST> GetDELIVERY_MAN_MSTList(DBContext dc)
        {
            return GetDELIVERY_MAN_MSTList(null, dc);
        }
        public static List<dcDELIVERY_MAN_MST> GetDELIVERY_MAN_MSTList(DBQuery dbq, DBContext dc)
        {
            List<dcDELIVERY_MAN_MST> cObjList = new List<dcDELIVERY_MAN_MST>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcDELIVERY_MAN_MST>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcDELIVERY_MAN_MST GetDELIVERY_MAN_MSTByID(int pDELIVERY_MAN_MSTID)
        {
            return GetDELIVERY_MAN_MSTByID(pDELIVERY_MAN_MSTID, null);
        }
        public static dcDELIVERY_MAN_MST GetDELIVERY_MAN_MSTByID(int pDELIVERY_MAN_MSTID, DBContext dc)
        {
            dcDELIVERY_MAN_MST cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcDELIVERY_MAN_MST>()
                                  where c.DELIVERY_MAN_ID == pDELIVERY_MAN_MSTID
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

        public static int Insert(dcDELIVERY_MAN_MST cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcDELIVERY_MAN_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcDELIVERY_MAN_MST>(cObj, true);
                if (id > 0) { cObj.DELIVERY_MAN_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcDELIVERY_MAN_MST cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcDELIVERY_MAN_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcDELIVERY_MAN_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pDELIVERY_MAN_MSTID)
        {
            return Delete(pDELIVERY_MAN_MSTID, null);
        }
        public static bool Delete(int pDELIVERY_MAN_MSTID, DBContext dc)
        {
            dcDELIVERY_MAN_MST cObj = new dcDELIVERY_MAN_MST();
            cObj.DELIVERY_MAN_ID = pDELIVERY_MAN_MSTID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcDELIVERY_MAN_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcDELIVERY_MAN_MST cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcDELIVERY_MAN_MST cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcDELIVERY_MAN_MST cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcDELIVERY_MAN_MST cObj, DBContext dc)
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
                                newID = cObj.DELIVERY_MAN_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.DELIVERY_MAN_ID, dc))
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

        public static bool SaveList(List<dcDELIVERY_MAN_MST> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcDELIVERY_MAN_MST> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcDELIVERY_MAN_MST oDet in detList)
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
                    //    bool d = Delete(oDet.DELIVERY_MAN_MSTID, dc);
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
