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
    public class CN_CREATION_MSTBL
    {
        public static DataLoadOptions CN_CREATION_MSTLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcCN_CREATION_MST>(obj => obj.relatedclassname);
            return dlo;
        }

        public static string GetCNListSQLString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT mst.* FROM CN_CREATION_MST mst ");
            sb.Append(" WHERE 1=1 ");

            return sb.ToString();
        }

        public static string GetCNInfoListSQLString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT mst.*, ");
            sb.Append(" cl.client_name, ");
            sb.Append(" agm.description AS AGREEMENT_DESCRIPTION, ");
            sb.Append(" im.item_name, ");
            sb.Append(" r.route_name, ");
            sb.Append(" dis.dist_name AS DESTINATION_DIST_NAME, ");
            sb.Append(" th.town_name AS DESTINATION_TOWN_NAME,dp.DEPT_NAME ");
            sb.Append(" FROM CN_CREATION_MST mst ");
            sb.Append(" LEFT JOIN client_mst cl ON mst.client_id = cl.client_id ");
            sb.Append(" LEFT JOIN agreement_detaill ag ON mst.agr_detail_id = ag.agr_detail_id ");
            sb.Append(" LEFT JOIN agreement_mst agm ON ag.agr_id = agm.agr_id ");
            sb.Append(" LEFT JOIN item_mst im ON mst.item_id = im.item_id ");
            sb.Append(" LEFT JOIN route_mst r ON mst.route_id = r.route_id ");
            sb.Append(" LEFT JOIN district_mst dis ON mst.destination_dist_id = dis.dist_id ");
            sb.Append(" LEFT JOIN thana_town_mst th ON mst.destination_town_id = th.town_id ");
            sb.Append(" LEFT JOIN DEPARTMENT_MST dp ON mst.CLIENT_DEPT_ID = dp.DEPT_ID ");
            sb.Append(" WHERE 1=1 ");

            return sb.ToString();
        }

        public static dcCN_CREATION_MST GetCNInfoByCNNumber(string pCNNumber, DBContext dc)
        {
            dcCN_CREATION_MST cObjList = new dcCN_CREATION_MST();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetCNListSQLString());
                if (pCNNumber != "")
                {
                    sb.Append(" AND mst.CN_NUMBER= @pCNNumber ");
                    cmdInfo.DBParametersInfo.Add("@pCNNumber", pCNNumber);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcCN_CREATION_MST>(dbq, dc).FirstOrDefault();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static List<dcCN_CREATION_MST> GetCNInfoList()
        {
            return GetCNInfoListById(0, null);
        }
        public static List<dcCN_CREATION_MST> GetCNInfoListById(int pCNId, DBContext dc)
        {
            List<dcCN_CREATION_MST> cObjList = new List<dcCN_CREATION_MST>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetCNInfoListSQLString());
                if (pCNId  > 0)
                {
                    sb.Append(" AND mst.CN_ID= @pCNId ");
                    cmdInfo.DBParametersInfo.Add("@pCNId", pCNId);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcCN_CREATION_MST>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcCN_CREATION_MST> GetCNInfoList(clsPrmWREL prm, DBContext dc)
        {
            List<dcCN_CREATION_MST> cObjList = new List<dcCN_CREATION_MST>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetCNInfoListSQLString());

                sb.Append(" AND mst.CLIENT_ID= @clientId ");
                cmdInfo.DBParametersInfo.Add("@clientId", prm.CLIENT_ID);

                if (!string.IsNullOrWhiteSpace(prm.ITEM_NAME))
                {
                    sb.Append(" AND UPPER(im.item_name) LIKE @itemName ");
                    cmdInfo.DBParametersInfo.Add("@itemName", "%" + prm.ITEM_NAME.ToUpper() + "%");
                }

                if (!string.IsNullOrWhiteSpace(prm.CONSIGNEE_NAME))
                {
                    sb.Append(" AND UPPER(mst.CONSIGNEE_NAME) LIKE @conName ");
                    cmdInfo.DBParametersInfo.Add("@conName", "%" + prm.CONSIGNEE_NAME.ToUpper() + "%");
                }

                if (!string.IsNullOrWhiteSpace(prm.CN_NUMBER))
                {
                    sb.Append(" AND UPPER(mst.CN_NUMBER) LIKE @cnNumber ");
                    cmdInfo.DBParametersInfo.Add("@cnNumber", "%" + prm.CN_NUMBER.ToUpper() + "%");
                }

                if (prm.CONSIGNEE_MOBILE_NO != "")
                {
                    sb.Append(" AND mst.CONSIGNEE_MOBILE_NO= @mobileNo ");
                    cmdInfo.DBParametersInfo.Add("@mobileNo", prm.CONSIGNEE_MOBILE_NO);
                }

                if (prm.FromDate.HasValue)
                {
                    if (prm.ToDate.HasValue)
                    {
                        sb.Append(" AND (TO_DATE(mst.CREATE_DATE) BETWEEN @fromDate AND @toDate) ");
                        cmdInfo.DBParametersInfo.Add("@fromDate", prm.FromDate.Value);
                        cmdInfo.DBParametersInfo.Add("@toDate", prm.ToDate.Value);
                    }
                    else
                    {
                        sb.Append(" AND TO_DATE(mst.CREATE_DATE) = @fromDate ");
                        cmdInfo.DBParametersInfo.Add("@fromDate", prm.FromDate.Value);

                    }

                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcCN_CREATION_MST>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static string Get_New_CN_No(string pdate, DBContext dc)
        {
            bool isDCInit = false;
            string _CN_No = string.Empty;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                string abbr = " SELECT FN_NEW_CN_NUMBER(@pdate) A from Dual ";
                cmdInfo.DBParametersInfo.Add("@pdate", pdate);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                _CN_No = Convert.ToString(DBQuery.ExecuteDBScalar(dbq, dc));
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return _CN_No;
        }
        public static List<dcCN_CREATION_MST> GetCN_CREATION_MSTList()
        {
            return GetCN_CREATION_MSTList(null, null);
        }
        public static List<dcCN_CREATION_MST> GetCN_CREATION_MSTList(DBContext dc)
        {
            return GetCN_CREATION_MSTList(null, dc);
        }
        public static List<dcCN_CREATION_MST> GetCN_CREATION_MSTList(DBQuery dbq, DBContext dc)
        {
            List<dcCN_CREATION_MST> cObjList = new List<dcCN_CREATION_MST>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcCN_CREATION_MST>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcCN_CREATION_MST GetCN_CREATION_MSTByID(int pCN_CREATION_MSTID)
        {
            return GetCN_CREATION_MSTByID(pCN_CREATION_MSTID, null);
        }
        public static dcCN_CREATION_MST GetCN_CREATION_MSTByID(int pCN_CREATION_MSTID, DBContext dc)
        {
            dcCN_CREATION_MST cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcCN_CREATION_MST>()
                                  where c.CN_ID == pCN_CREATION_MSTID
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

        public static int Insert(dcCN_CREATION_MST cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcCN_CREATION_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcCN_CREATION_MST>(cObj, true);
                if (id > 0) { cObj.CN_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcCN_CREATION_MST cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcCN_CREATION_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcCN_CREATION_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pCN_CREATION_MSTID)
        {
            return Delete(pCN_CREATION_MSTID, null);
        }
        public static bool Delete(int pCN_CREATION_MSTID, DBContext dc)
        {
            dcCN_CREATION_MST cObj = new dcCN_CREATION_MST();
            cObj.CN_ID = pCN_CREATION_MSTID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcCN_CREATION_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcCN_CREATION_MST cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcCN_CREATION_MST cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcCN_CREATION_MST cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcCN_CREATION_MST cObj, DBContext dc)
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
                                newID = cObj.CN_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.CN_ID, dc))
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

        public static bool SaveList(List<dcCN_CREATION_MST> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcCN_CREATION_MST> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcCN_CREATION_MST oDet in detList)
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
                    //    bool d = Delete(oDet.CN_CREATION_MSTID, dc);
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
