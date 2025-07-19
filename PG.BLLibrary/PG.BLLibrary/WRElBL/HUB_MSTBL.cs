using PG.Core.DBBase;
using PG.DBClass.WRELDC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.BLLibrary.WRElBL
{
    public class HUB_MSTBL
    {
        public static DataLoadOptions HUB_MSTLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcHUB_MST>(obj => obj.relatedclassname);
            return dlo;
        }

        public static string GetHUB_MSTSQLString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT * ");
            sb.Append(" FROM HUB_MST  ");

            sb.Append(" WHERE 1=1 ");

            return sb.ToString();
        }

        public static string GetHUB_MSTListString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT a.HUB_ID,a.HUB_NAME,a.HUB_TYPE_ID,a.ADDRESS,a.PHONE_NO,a.RESPONSIBLE_PERSON,a.RP_MOBILE_NO,a.DESCRIPTION,a.IS_ACTIVE,a.DIST_ID,a.TOWN_ID,b.DIST_NAME,c.TOWN_NAME ");
            sb.Append(" FROM HUB_MST a INNER JOIN DISTRICT_MST b ON a.DIST_ID=b.DIST_ID ");
            sb.Append(" INNER JOIN THANA_TOWN_MST c ON a.TOWN_ID=b.TOWN_ID ");

           // sb.Append(" WHERE a.IS_ACTIVE='Y' ");


            return sb.ToString();
        }
        public static List<dcHUB_MST> GetHUB_MSTList()
        {
            return GetHUB_MSTList(null, null);
        }
        public static List<dcHUB_MST> GetHUB_MSTList(DBContext dc)
        {
            return GetHUB_MSTList(null, dc);
        }
        public static List<dcHUB_MST> GetHUB_MSTList(DBQuery dbq, DBContext dc)
        {
            List<dcHUB_MST> cObjList = new List<dcHUB_MST>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcHUB_MST>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcHUB_MST GetHUB_MSTByID(int pHUB_MSTID)
        {
            return GetHUB_MSTByID(pHUB_MSTID, null);
        }
        public static dcHUB_MST GetHUB_MSTByID(int pHUB_MSTID, DBContext dc)
        {
            dcHUB_MST cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcHUB_MST>()
                                  where c.HUB_ID == pHUB_MSTID
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

        public static dcHUB_MST GetHUB_MSTInfoById(int pHub_ID)
        {
            return GetHUB_MSTInfoById(pHub_ID, null).FirstOrDefault();
        }
        public static List<dcHUB_MST> GetHUB_MSTInfoById(int pHub_ID, DBContext dc)
        {
            List<dcHUB_MST> cObjList = new List<dcHUB_MST>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetHUB_MSTListString());
                if (pHub_ID > 0)
                {
                    sb.Append(" AND a.HUB_ID= @pHub_ID ");
                    cmdInfo.DBParametersInfo.Add("@pHub_ID", pHub_ID);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcHUB_MST>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcHUB_MST> GetHUB_MST_infoList(dcHUB_MST prmHms, DBContext dc)
        {
            List<dcHUB_MST> cObjList = new List<dcHUB_MST>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetHUB_MSTSQLString());




                if (prmHms.IS_ACTIVE != "0")
                {
                    sb.Append(" AND IS_ACTIVE= @IS_ACTIVE ");
                    cmdInfo.DBParametersInfo.Add("@IS_ACTIVE", prmHms.IS_ACTIVE);
                }


                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcHUB_MST>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

       


        public static int Insert(dcHUB_MST cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcHUB_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcHUB_MST>(cObj, true);
                if (id > 0) { cObj.HUB_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcHUB_MST cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcHUB_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcHUB_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pHUB_MSTID)
        {
            return Delete(pHUB_MSTID, null);
        }
        public static bool Delete(int pHUB_MSTID, DBContext dc)
        {
            dcHUB_MST cObj = new dcHUB_MST();
            cObj.HUB_ID = pHUB_MSTID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcHUB_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcHUB_MST cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcHUB_MST cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcHUB_MST cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcHUB_MST cObj, DBContext dc)
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
                                newID = cObj.HUB_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.HUB_ID, dc))
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

        public static bool SaveList(List<dcHUB_MST> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcHUB_MST> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcHUB_MST oDet in detList)
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
                    //    bool d = Delete(oDet.HUB_MSTID, dc);
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
