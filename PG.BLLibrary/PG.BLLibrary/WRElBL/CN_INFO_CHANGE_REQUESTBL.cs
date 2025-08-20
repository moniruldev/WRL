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
    public class CN_INFO_CHANGE_REQUESTBL
    {
        public static DataLoadOptions CN_INFO_CHANGE_REQUESTLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcCN_INFO_CHANGE_REQUEST>(obj => obj.relatedclassname);
            return dlo;
        }

        public static string GetRequestInfoSQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT REQ.*,CN.CN_NUMBER,c.client_name,usr.fullname AS REQUEST_BY_NAME FROM cn_info_change_request REQ ");
            sb.Append(" INNER JOIN cn_creation_mst CN ON req.cn_id=CN.cn_id ");
            sb.Append(" LEFT JOIN client_mst C ON REQ.client_id=c.client_id ");
            sb.Append(" LEFT JOIN TBLUSER USR ON req.request_by=usr.userid ");
            sb.Append(" WHERE 1=1 ");

            return sb.ToString();
        }

        public static dcCN_INFO_CHANGE_REQUEST GetRequestInfoById(int pRequestId, DBContext dc)
        {
            dcCN_INFO_CHANGE_REQUEST cObj = new dcCN_INFO_CHANGE_REQUEST();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetRequestInfoSQLString());
                if (pRequestId > 0)
                {
                    sb.Append(" AND REQ.REQUEST_ID= @pRequestId ");
                    cmdInfo.DBParametersInfo.Add("@pRequestId", pRequestId);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObj = DBQuery.ExecuteDBQuery<dcCN_INFO_CHANGE_REQUEST>(dbq, dc).FirstOrDefault();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObj;
        }

        public static List<dcCN_INFO_CHANGE_REQUEST> GetRequestInfoList(clsPrmWREL prm, DBContext dc)
        {
            List<dcCN_INFO_CHANGE_REQUEST> cObjList = new List<dcCN_INFO_CHANGE_REQUEST>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetRequestInfoSQLString());

                if (prm.TRANS_ID > 0)
                {
                    sb.Append(" AND REQ.REQUEST_ID= @pRequestId ");
                    cmdInfo.DBParametersInfo.Add("@pRequestId", prm.TRANS_ID);
                }
                if (!string.IsNullOrEmpty(prm.CN_NUMBER))
                {
                    sb.Append(" AND CN.CN_NUMBER= @pCnNumber ");
                    cmdInfo.DBParametersInfo.Add("@pCnNumber", prm.CN_NUMBER);
                }
                if (prm.USER_TYPE.ToUpper() != "ADMIN")
                {
                    sb.Append(" AND REQ.CLIENT_ID= @clientId ");
                    cmdInfo.DBParametersInfo.Add("@clientId", prm.CLIENT_ID);
                }

                if (prm.Status != "0")
                {
                    sb.Append(" AND REQ.APPROVED_STATUS= @status ");
                    cmdInfo.DBParametersInfo.Add("@status", prm.Status);
                }

                if (prm.FromDate.HasValue)
                {
                    if (prm.ToDate.HasValue)
                    {
                        sb.Append(" AND (TO_DATE(req.REQUEST_DATE) BETWEEN @fromDate AND @toDate) ");
                        cmdInfo.DBParametersInfo.Add("@fromDate", prm.FromDate.Value);
                        cmdInfo.DBParametersInfo.Add("@toDate", prm.ToDate.Value);
                    }
                    else
                    {
                        sb.Append(" AND TO_DATE(req.REQUEST_DATE) = @fromDate ");
                        cmdInfo.DBParametersInfo.Add("@fromDate", prm.FromDate.Value);

                    }

                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcCN_INFO_CHANGE_REQUEST>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static List<dcCN_INFO_CHANGE_REQUEST> GetCN_INFO_CHANGE_REQUESTList()
        {
            return GetCN_INFO_CHANGE_REQUESTList(null, null);
        }
        public static List<dcCN_INFO_CHANGE_REQUEST> GetCN_INFO_CHANGE_REQUESTList(DBContext dc)
        {
            return GetCN_INFO_CHANGE_REQUESTList(null, dc);
        }
        public static List<dcCN_INFO_CHANGE_REQUEST> GetCN_INFO_CHANGE_REQUESTList(DBQuery dbq, DBContext dc)
        {
            List<dcCN_INFO_CHANGE_REQUEST> cObjList = new List<dcCN_INFO_CHANGE_REQUEST>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcCN_INFO_CHANGE_REQUEST>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcCN_INFO_CHANGE_REQUEST GetCN_INFO_CHANGE_REQUESTByID(int pREQUEST_ID)
        {
            return GetCN_INFO_CHANGE_REQUESTByID(pREQUEST_ID, null);
        }
        public static dcCN_INFO_CHANGE_REQUEST GetCN_INFO_CHANGE_REQUESTByID(int pREQUEST_ID, DBContext dc)
        {
            dcCN_INFO_CHANGE_REQUEST cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcCN_INFO_CHANGE_REQUEST>()
                                  where c.REQUEST_ID == pREQUEST_ID
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

        public static int Insert(dcCN_INFO_CHANGE_REQUEST cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcCN_INFO_CHANGE_REQUEST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcCN_INFO_CHANGE_REQUEST>(cObj, true);
                if (id > 0) { cObj.REQUEST_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcCN_INFO_CHANGE_REQUEST cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcCN_INFO_CHANGE_REQUEST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcCN_INFO_CHANGE_REQUEST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pREQUEST_ID)
        {
            return Delete(pREQUEST_ID, null);
        }
        public static bool Delete(int pREQUEST_ID, DBContext dc)
        {
            dcCN_INFO_CHANGE_REQUEST cObj = new dcCN_INFO_CHANGE_REQUEST();
            cObj.REQUEST_ID = pREQUEST_ID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcCN_INFO_CHANGE_REQUEST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcCN_INFO_CHANGE_REQUEST cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcCN_INFO_CHANGE_REQUEST cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcCN_INFO_CHANGE_REQUEST cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcCN_INFO_CHANGE_REQUEST cObj, DBContext dc)
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
                                newID = cObj.REQUEST_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.REQUEST_ID, dc))
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

        public static bool SaveList(List<dcCN_INFO_CHANGE_REQUEST> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcCN_INFO_CHANGE_REQUEST> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcCN_INFO_CHANGE_REQUEST oDet in detList)
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
                        bool d = Delete(oDet.REQUEST_ID, dc);
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
