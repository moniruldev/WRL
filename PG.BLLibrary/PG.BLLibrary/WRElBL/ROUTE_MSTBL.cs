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
    public class ROUTE_MSTBL
    {
        public static DataLoadOptions ROUTE_MSTLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcROUTE_MST>(obj => obj.relatedclassname);
            return dlo;
        }

        public static string GetRouteMstSQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT * FROM ROUTE_MST mst ");
            sb.Append(" WHERE 1=1 ");

            return sb.ToString();
        }

        public static string GetRouteInfoSQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT MST.*,stdist.dist_name STARTING_DIST_NAME,desdist.dist_name DESTINATION_DIST_NAME ");
            sb.Append(" FROM ROUTE_MST mst ");
            sb.Append(" LEFT JOIN district_mst STDIST ON mst.starting_dist_id=stdist.dist_id ");
            sb.Append(" LEFT JOIN district_mst DESDIST ON mst.destination_dist_id=desdist.dist_id ");
            sb.Append(" WHERE 1=1 ");

            return sb.ToString();
        }
        public static dcROUTE_MST GetRouteInfoById(int pRouteId)
        {
            return GetRouteInfoListById(pRouteId, null).FirstOrDefault();
        }
        public static List<dcROUTE_MST> GetRouteInfoList()
        {
            return GetRouteInfoListById(0, null);
        }
        public static List<dcROUTE_MST> GetRouteInfoListById(int pRouteId, DBContext dc)
        {
            List<dcROUTE_MST> cObjList = new List<dcROUTE_MST>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetRouteInfoSQLString());
                if (pRouteId > 0)
                {
                    sb.Append(" AND mst.ROUTE_ID= @pRouteId ");
                    cmdInfo.DBParametersInfo.Add("@pRouteId", pRouteId);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcROUTE_MST>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcROUTE_MST> GetRouteInfoList(clsPrmWREL prm, DBContext dc)
        {
            List<dcROUTE_MST> cObjList = new List<dcROUTE_MST>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetRouteInfoSQLString());
                if (prm.Status != string.Empty)
                {
                    sb.Append(" AND mst.IS_ACTIVE= @Status ");
                    cmdInfo.DBParametersInfo.Add("@Status", prm.Status);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcROUTE_MST>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static List<dcROUTE_MST> GetROUTE_MSTList()
        {
            return GetROUTE_MSTList(null, null);
        }
        public static List<dcROUTE_MST> GetROUTE_MSTList(DBContext dc)
        {
            return GetROUTE_MSTList(null, dc);
        }
        public static List<dcROUTE_MST> GetROUTE_MSTList(DBQuery dbq, DBContext dc)
        {
            List<dcROUTE_MST> cObjList = new List<dcROUTE_MST>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcROUTE_MST>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcROUTE_MST GetROUTE_MSTByID(int pROUTE_MSTID)
        {
            return GetROUTE_MSTByID(pROUTE_MSTID, null);
        }
        public static dcROUTE_MST GetROUTE_MSTByID(int pROUTE_MSTID, DBContext dc)
        {
            dcROUTE_MST cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcROUTE_MST>()
                                  where c.ROUTE_ID == pROUTE_MSTID
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

        public static int Insert(dcROUTE_MST cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcROUTE_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcROUTE_MST>(cObj, true);
                if (id > 0) { cObj.ROUTE_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcROUTE_MST cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcROUTE_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcROUTE_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pROUTE_MSTID)
        {
            return Delete(pROUTE_MSTID, null);
        }
        public static bool Delete(int pROUTE_MSTID, DBContext dc)
        {
            dcROUTE_MST cObj = new dcROUTE_MST();
            cObj.ROUTE_ID = pROUTE_MSTID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcROUTE_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcROUTE_MST cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcROUTE_MST cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcROUTE_MST cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcROUTE_MST cObj, DBContext dc)
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
                                newID = cObj.ROUTE_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.ROUTE_ID, dc))
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

                        if (cObj.RouteDetailsList != null)
                        {
                            foreach (dcROUTE_DETAIL det in cObj.RouteDetailsList)
                            {
                                det.ROUTE_ID = newID;
                            }
                            bStatus = ROUTE_DETAILBL.SaveList(cObj.RouteDetailsList, dc);
                        }
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

        public static bool SaveList(List<dcROUTE_MST> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcROUTE_MST> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcROUTE_MST oDet in detList)
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
                    //    bool d = Delete(oDet.ROUTE_MSTID, dc);
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
