using PG.Core.DBBase;
using PG.DBClass.HMSDC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.BLLibrary.HMSBL
{
    public class HMCOUNTRY_MSTBL
    {
        public static DataLoadOptions HMCOUNTRY_MSTLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcHMCOUNTRY_MST>(obj => obj.relatedclassname);
            return dlo;
        }

        public static string GetCountryInfoSQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT HMCOUNTRY_MST.* FROM HMCOUNTRY_MST WHERE 1=1 ");
            return sb.ToString();
        }

        public static List<dcHMCOUNTRY_MST> GetCountryList()
        {
            return GetCountryList(null);
        }
        public static List<dcHMCOUNTRY_MST> GetCountryList(DBContext dc)
        {
            List<dcHMCOUNTRY_MST> cObjList = new List<dcHMCOUNTRY_MST>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetCountryInfoSQLString());
              
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcHMCOUNTRY_MST>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcHMCOUNTRY_MST> GetHMCOUNTRY_MSTList()
        {
            return GetHMCOUNTRY_MSTList(null, null);
        }
        public static List<dcHMCOUNTRY_MST> GetHMCOUNTRY_MSTList(DBContext dc)
        {
            return GetHMCOUNTRY_MSTList(null, dc);
        }
        public static List<dcHMCOUNTRY_MST> GetHMCOUNTRY_MSTList(DBQuery dbq, DBContext dc)
        {
            List<dcHMCOUNTRY_MST> cObjList = new List<dcHMCOUNTRY_MST>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcHMCOUNTRY_MST>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcHMCOUNTRY_MST GetHMCOUNTRY_MSTByID(int pCOUNTRY_ID)
        {
            return GetHMCOUNTRY_MSTByID(pCOUNTRY_ID, null);
        }
        public static dcHMCOUNTRY_MST GetHMCOUNTRY_MSTByID(int pCOUNTRY_ID, DBContext dc)
        {
            dcHMCOUNTRY_MST cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcHMCOUNTRY_MST>()
                                  where c.COUNTRY_ID == pCOUNTRY_ID
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

        public static int Insert(dcHMCOUNTRY_MST cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcHMCOUNTRY_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcHMCOUNTRY_MST>(cObj, true);
                if (id > 0) { cObj.COUNTRY_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcHMCOUNTRY_MST cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcHMCOUNTRY_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcHMCOUNTRY_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pCOUNTRY_ID)
        {
            return Delete(pCOUNTRY_ID, null);
        }
        public static bool Delete(int pCOUNTRY_ID, DBContext dc)
        {
            dcHMCOUNTRY_MST cObj = new dcHMCOUNTRY_MST();
            cObj.COUNTRY_ID = pCOUNTRY_ID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcHMCOUNTRY_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcHMCOUNTRY_MST cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcHMCOUNTRY_MST cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcHMCOUNTRY_MST cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcHMCOUNTRY_MST cObj, DBContext dc)
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
                                newID = cObj.COUNTRY_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.COUNTRY_ID, dc))
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

        public static bool SaveList(List<dcHMCOUNTRY_MST> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcHMCOUNTRY_MST> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcHMCOUNTRY_MST oDet in detList)
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
                        bool d = Delete(oDet.COUNTRY_ID, dc);
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
