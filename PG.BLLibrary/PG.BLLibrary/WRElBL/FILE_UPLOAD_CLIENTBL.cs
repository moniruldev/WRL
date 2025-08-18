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
    public class FILE_UPLOAD_CLIENTBL
    {
        public static DataLoadOptions FILE_UPLOAD_CLIENTLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcFILE_UPLOAD_CLIENT>(obj => obj.relatedclassname);
            return dlo;
        }

        public static string GetFILE_UPLOADListSQLString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT mst.* FROM FILE_UPLOAD_CLIENT mst ");
            sb.Append(" WHERE 1=1 ");

            return sb.ToString();
        }

        public static List<dcFILE_UPLOAD_CLIENT> GetFILE_UPLOADListInfo( DBContext dc)
        {
            List<dcFILE_UPLOAD_CLIENT> cObjListd =new List<dcFILE_UPLOAD_CLIENT>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetFILE_UPLOADListSQLString());
                //if (pCNNumber != "")
                //{
                //    sb.Append(" AND mst.CN_NUMBER= @pCNNumber ");
                //    cmdInfo.DBParametersInfo.Add("@pCNNumber", pCNNumber);
                //}
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjListd = DBQuery.ExecuteDBQuery<dcFILE_UPLOAD_CLIENT>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjListd;
        }

        public static List<dcFILE_UPLOAD_CLIENT> GetFILE_UPLOAD_CLIENTList()
        {
            return GetFILE_UPLOAD_CLIENTList(null, null);
        }
        public static List<dcFILE_UPLOAD_CLIENT> GetFILE_UPLOAD_CLIENTList(DBContext dc)
        {
            return GetFILE_UPLOAD_CLIENTList(null, dc);
        }
        public static List<dcFILE_UPLOAD_CLIENT> GetFILE_UPLOAD_CLIENTList(DBQuery dbq, DBContext dc)
        {
            List<dcFILE_UPLOAD_CLIENT> cObjList = new List<dcFILE_UPLOAD_CLIENT>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcFILE_UPLOAD_CLIENT>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcFILE_UPLOAD_CLIENT GetFILE_UPLOAD_CLIENTByID(int pFILE_UPLOAD_CLIENTID)
        {
            return GetFILE_UPLOAD_CLIENTByID(pFILE_UPLOAD_CLIENTID, null);
        }
        public static dcFILE_UPLOAD_CLIENT GetFILE_UPLOAD_CLIENTByID(int pFILE_UPLOAD_CLIENTID, DBContext dc)
        {
            dcFILE_UPLOAD_CLIENT cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcFILE_UPLOAD_CLIENT>()
                                  where c.UPLOAD_ID == pFILE_UPLOAD_CLIENTID
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

        public static int Insert(dcFILE_UPLOAD_CLIENT cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcFILE_UPLOAD_CLIENT cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcFILE_UPLOAD_CLIENT>(cObj, true);
                if (id > 0) { cObj.UPLOAD_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcFILE_UPLOAD_CLIENT cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcFILE_UPLOAD_CLIENT cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcFILE_UPLOAD_CLIENT>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pFILE_UPLOAD_CLIENTID)
        {
            return Delete(pFILE_UPLOAD_CLIENTID, null);
        }
        public static bool Delete(int pFILE_UPLOAD_CLIENTID, DBContext dc)
        {
            dcFILE_UPLOAD_CLIENT cObj = new dcFILE_UPLOAD_CLIENT();
            cObj.UPLOAD_ID = pFILE_UPLOAD_CLIENTID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcFILE_UPLOAD_CLIENT>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcFILE_UPLOAD_CLIENT cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcFILE_UPLOAD_CLIENT cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcFILE_UPLOAD_CLIENT cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcFILE_UPLOAD_CLIENT cObj, DBContext dc)
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
                                newID = cObj.UPLOAD_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.UPLOAD_ID, dc))
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

        public static bool SaveList(List<dcFILE_UPLOAD_CLIENT> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcFILE_UPLOAD_CLIENT> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcFILE_UPLOAD_CLIENT oDet in detList)
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
                    //    bool d = Delete(oDet.FILE_UPLOAD_CLIENTID, dc);
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
