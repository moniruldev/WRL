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
    public class CLIENT_TYPE_MSTBL
    {
        public static DataLoadOptions CLIENT_TYPE_MSTLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcCLIENT_TYPE_MST>(obj => obj.relatedclassname);
            return dlo;
        }
        public static string GetCLIENT_TYPESQLString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT * ");
            sb.Append(" FROM CLIENT_TYPE_MST  ");
           
            sb.Append(" WHERE 1=1 ");

            return sb.ToString();
        }

        public static string GetCLIENT_TYPEListString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT CLIENT_TYPE_ID,TYPE_NAME,DESCRIPTION ");
            sb.Append(" FROM CLIENT_TYPE_MST  ");
           
            sb.Append(" WHERE IS_ACTIVE='Y' ");
            

            return sb.ToString();
        }
        public static List<dcCLIENT_TYPE_MST> GetCLIENT_TYPE_MSTList()
        {
            return GetCLIENT_TYPE_MSTList(null, null);
        }
        public static List<dcCLIENT_TYPE_MST> GetCLIENT_TYPE_MSTList(DBContext dc)
        {
            return GetCLIENT_TYPE_MSTList(null, dc);
        }
        public static List<dcCLIENT_TYPE_MST> GetCLIENT_TYPE_MSTList(DBQuery dbq, DBContext dc)
        {
            List<dcCLIENT_TYPE_MST> cObjList = new List<dcCLIENT_TYPE_MST>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcCLIENT_TYPE_MST>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcCLIENT_TYPE_MST GetCLIENT_TYPE_MSTByID(int pCLIENT_TYPE_MSTID)
        {
            return GetCLIENT_TYPE_MSTByID(pCLIENT_TYPE_MSTID, null);
        }
        public static dcCLIENT_TYPE_MST GetCLIENT_TYPE_MSTByID(int pCLIENT_TYPE_MSTID, DBContext dc)
        {
            dcCLIENT_TYPE_MST cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcCLIENT_TYPE_MST>()
                                  where c.CLIENT_TYPE_ID == pCLIENT_TYPE_MSTID
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
        public static dcCLIENT_TYPE_MST GetCLIENT_TYPEInfoById(int pCLIENT_TYPE_ID)
        {
            return GetCLIENT_TYPEInfoListById(pCLIENT_TYPE_ID, null).FirstOrDefault();
        }
        public static List<dcCLIENT_TYPE_MST> GetCLIENT_TYPEInfoListById(int pCLIENT_TYPE_ID, DBContext dc)
        {
            List<dcCLIENT_TYPE_MST> cObjList = new List<dcCLIENT_TYPE_MST>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetCLIENT_TYPESQLString());
                if (pCLIENT_TYPE_ID > 0)
                {
                    sb.Append(" AND CLIENT_TYPE_ID= @pRoomId ");
                    cmdInfo.DBParametersInfo.Add("@pRoomId", pCLIENT_TYPE_ID);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcCLIENT_TYPE_MST>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcCLIENT_TYPE_MST> GetCLIENT_TYPEList(dcCLIENT_TYPE_MST prmHms, DBContext dc)
        {
            List<dcCLIENT_TYPE_MST> cObjList = new List<dcCLIENT_TYPE_MST>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetCLIENT_TYPESQLString());
               

               

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

                cObjList = DBQuery.ExecuteDBQuery<dcCLIENT_TYPE_MST>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static int Insert(dcCLIENT_TYPE_MST cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcCLIENT_TYPE_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcCLIENT_TYPE_MST>(cObj, true);
                if (id > 0) { cObj.CLIENT_TYPE_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcCLIENT_TYPE_MST cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcCLIENT_TYPE_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcCLIENT_TYPE_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pCLIENT_TYPE_MSTID)
        {
            return Delete(pCLIENT_TYPE_MSTID, null);
        }
        public static bool Delete(int pCLIENT_TYPE_MSTID, DBContext dc)
        {
            dcCLIENT_TYPE_MST cObj = new dcCLIENT_TYPE_MST();
            cObj.CLIENT_TYPE_ID = pCLIENT_TYPE_MSTID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcCLIENT_TYPE_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcCLIENT_TYPE_MST cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcCLIENT_TYPE_MST cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcCLIENT_TYPE_MST cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcCLIENT_TYPE_MST cObj, DBContext dc)
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
                                newID = cObj.CLIENT_TYPE_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.CLIENT_TYPE_ID, dc))
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

        public static bool SaveList(List<dcCLIENT_TYPE_MST> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcCLIENT_TYPE_MST> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcCLIENT_TYPE_MST oDet in detList)
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
                    //    bool d = Delete(oDet.CLIENT_TYPE_MSTID, dc);
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
