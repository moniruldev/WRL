using PG.Core.DBBase;
using PG.DBClass.InventoryDC;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;

namespace PG.BLLibrary.InventoryBL
{
    public class BANK_MSTBL
    {
        public static DataLoadOptions BANK_MSTLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcBANK_MST>(obj => obj.relatedclassname);
            return dlo;
        }

        public static string Bank_Sql_String()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" Select BANK_MST.* from BANK_MST ");
            sb.Append(" WHERE 1=1 ");
            return sb.ToString();
        }
        public static List<dcBANK_MST> GetBANK_MSTList()
        {
            return GetBANK_MSTList(null, null);
        }
        public static List<dcBANK_MST> GetBANK_MSTList(DBContext dc)
        {
            return GetBANK_MSTList(null, dc);
        }
        public static List<dcBANK_MST> GetBANK_MSTList(DBQuery dbq, DBContext dc)
        {
            List<dcBANK_MST> cObjList = new List<dcBANK_MST>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcBANK_MST>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcBANK_MST GetBANK_MSTByID(int pBANK_MSTID)
        {
            return GetBANK_MSTByID(pBANK_MSTID, null);
        }
        public static dcBANK_MST GetBANK_MSTByID(int pBANK_MSTID, DBContext dc)
        {
            dcBANK_MST cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcBANK_MST>()
                                 // where c.BANK_MSTID == pBANK_MSTID
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

        public static int Insert(dcBANK_MST cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcBANK_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcBANK_MST>(cObj, true);
                //if (id > 0) { cObj.BANK_MSTID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcBANK_MST cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcBANK_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcBANK_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pBANK_MSTID)
        {
            return Delete(pBANK_MSTID, null);
        }
        public static bool Delete(int pBANK_MSTID, DBContext dc)
        {
            dcBANK_MST cObj = new dcBANK_MST();
            //cObj.BANK_MSTID = pBANK_MSTID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcBANK_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcBANK_MST cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcBANK_MST cObj, bool isAdd, DBContext dc)
        {
            //cObj._RecordState = isAdd ? Interwave.Core.DBClass.RecordStateEnum.Added : Interwave.Core.DBClass.RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcBANK_MST cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcBANK_MST cObj, DBContext dc)
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
                                newID = cObj.BANK_MSTID;
                            }
                            break;
                        case Interwave.Core.DBClass.RecordStateEnum.Deleted:
                            if (Delete(cObj.BANK_MSTID, dc))
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

        public static bool SaveList(List<dcBANK_MST> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcBANK_MST> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcBANK_MST oDet in detList)
            {
                switch (oDet._RecordState)
                {
                    /*case Interwave.Core.DBClass.RecordStateEnum.Added:
                        int a = Insert(oDet, dc);
                        break;
                    case Interwave.Core.DBClass.RecordStateEnum.Edited:
                        bool e = Update(oDet, dc);
                        break;
                    case Interwave.Core.DBClass.RecordStateEnum.Deleted:
                        bool d = Delete(oDet.BANK_MSTID, dc);
                        break;*/
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
