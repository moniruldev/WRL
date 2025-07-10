using PG.Core.DBBase;
using PG.DBClass.ProductionDC;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.BLLibrary.ProductionBL
{
    public class STORAGE_LOCATION_MSTBL
    {
        public static DataLoadOptions STORAGE_LOCATION_MSTLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcSTORAGE_LOCATION_MST>(obj => obj.relatedclassname);
            return dlo;
        }

        public static string GetSTORAGE_LOCATION_Info_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT * FROM STORAGE_LOCATION_MST where 1=1  ");
            return sb.ToString();
        }

       
        public static List<dcSTORAGE_LOCATION_MST> GetSTORAGE_LOCATION_MSTList()
        {
            return GetSTORAGE_LOCATION_MSTList(null, null);
        }
        public static List<dcSTORAGE_LOCATION_MST> GetSTORAGE_LOCATION_MSTList(DBContext dc)
        {
            return GetSTORAGE_LOCATION_MSTList(null, dc);
        }
        public static List<dcSTORAGE_LOCATION_MST> GetSTORAGE_LOCATION_MSTList(DBQuery dbq, DBContext dc)
        {
            List<dcSTORAGE_LOCATION_MST> cObjList = new List<dcSTORAGE_LOCATION_MST>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcSTORAGE_LOCATION_MST>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcSTORAGE_LOCATION_MST GetSTORAGE_LOCATION_MSTByID(int pSTORAGE_LOCATION_MSTID)
        {
            return GetSTORAGE_LOCATION_MSTByID(pSTORAGE_LOCATION_MSTID, null);
        }
        public static dcSTORAGE_LOCATION_MST GetSTORAGE_LOCATION_MSTByID(int pSTORAGE_LOCATION_MSTID, DBContext dc)
        {
            dcSTORAGE_LOCATION_MST cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcSTORAGE_LOCATION_MST>()
                                  //where c.STORAGE_LOCATION_MSTID == pSTORAGE_LOCATION_MSTID
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

        public static int Insert(dcSTORAGE_LOCATION_MST cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcSTORAGE_LOCATION_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcSTORAGE_LOCATION_MST>(cObj, true);
                //if (id > 0) { cObj.STORAGE_LOCATION_MSTID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcSTORAGE_LOCATION_MST cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcSTORAGE_LOCATION_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcSTORAGE_LOCATION_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pSTORAGE_LOCATION_MSTID)
        {
            return Delete(pSTORAGE_LOCATION_MSTID, null);
        }
        public static bool Delete(int pSTORAGE_LOCATION_MSTID, DBContext dc)
        {
            dcSTORAGE_LOCATION_MST cObj = new dcSTORAGE_LOCATION_MST();
            //cObj.STORAGE_LOCATION_MSTID = pSTORAGE_LOCATION_MSTID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcSTORAGE_LOCATION_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcSTORAGE_LOCATION_MST cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcSTORAGE_LOCATION_MST cObj, bool isAdd, DBContext dc)
        {
            //cObj._RecordState = isAdd ? Interwave.Core.DBClass.RecordStateEnum.Added : Interwave.Core.DBClass.RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcSTORAGE_LOCATION_MST cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcSTORAGE_LOCATION_MST cObj, DBContext dc)
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
                        //        newID = cObj.STORAGE_LOCATION_MSTID;
                        //    }
                        //    break;
                        //case Interwave.Core.DBClass.RecordStateEnum.Deleted:
                        //    if (Delete(cObj.STORAGE_LOCATION_MSTID, dc))
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

        public static bool SaveList(List<dcSTORAGE_LOCATION_MST> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcSTORAGE_LOCATION_MST> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcSTORAGE_LOCATION_MST oDet in detList)
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
                    //    bool d = Delete(oDet.STORAGE_LOCATION_MSTID, dc);
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


        public static string GetStorageListString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" Select *  ");
            sb.Append(" FROM STORAGE_LOCATION_MST mst ");
            sb.Append(" Where 1=1 ");

            return sb.ToString();
        }


       

    }
}
