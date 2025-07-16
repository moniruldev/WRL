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
    public class CARGO_CREATION_MSTBL
    {
        public static DataLoadOptions CARGO_CREATOION_MSTLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcCARGO_CREATION_MST>(obj => obj.relatedclassname);
            return dlo;
        }

        public static string GetCargoMstListString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT MST.*,STD.DIST_NAME STARTING_DIST_NAME ,DTD.DIST_NAME DESTINATION_DIST_NAME,TWN.TOWN_NAME,R.ROUTE_NAME ");
            sb.Append(" FROM CARGO_CREATION_MST mst ");
            sb.Append(" LEFT JOIN district_mst STD ON mst.cargo_starting_dis_id=std.dist_id ");
            sb.Append(" LEFT JOIN district_mst DTD ON mst.cargo_starting_dis_id=DTD.dist_id ");
            sb.Append(" LEFT JOIN thana_town_mst TWN ON mst.cargo_destination_town_id=twn.town_id ");
            sb.Append(" LEFT JOIN route_mst R ON mst.route_id=r.route_id ");
            sb.Append(" WHERE 1=1 ");

            return sb.ToString();
        }

        public static dcCARGO_CREATION_MST GetCargoMstInfoById(int pCargoMstId)
        {
            return GetCargoMstList(pCargoMstId, null).FirstOrDefault();
        }

        public static List<dcCARGO_CREATION_MST> GetCargoMstList()
        {
            return GetCargoMstList(0, null);
        }

        public static List<dcCARGO_CREATION_MST> GetCargoMstList(int pCargoMstId, DBContext dc)
        {
            List<dcCARGO_CREATION_MST> cObjList = new List<dcCARGO_CREATION_MST>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetCargoMstListString());
                if (pCargoMstId > 0)
                {
                    sb.Append(" AND mst.CARGO_ID= @pCargoMstId ");
                    cmdInfo.DBParametersInfo.Add("@pCargoMstId", pCargoMstId);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcCARGO_CREATION_MST>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static List<dcCARGO_CREATION_MST> GetCARGO_CREATOION_MSTList()
        {
            return GetCARGO_CREATOION_MSTList(null, null);
        }
        public static List<dcCARGO_CREATION_MST> GetCARGO_CREATOION_MSTList(DBContext dc)
        {
            return GetCARGO_CREATOION_MSTList(null, dc);
        }
        public static List<dcCARGO_CREATION_MST> GetCARGO_CREATOION_MSTList(DBQuery dbq, DBContext dc)
        {
            List<dcCARGO_CREATION_MST> cObjList = new List<dcCARGO_CREATION_MST>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcCARGO_CREATION_MST>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcCARGO_CREATION_MST GetCARGO_CREATOION_MSTByID(int pCARGO_CREATOION_MSTID)
        {
            return GetCARGO_CREATOION_MSTByID(pCARGO_CREATOION_MSTID, null);
        }
        public static dcCARGO_CREATION_MST GetCARGO_CREATOION_MSTByID(int pCARGO_CREATOION_MSTID, DBContext dc)
        {
            dcCARGO_CREATION_MST cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcCARGO_CREATION_MST>()
                                  where c.CARGO_ID == pCARGO_CREATOION_MSTID
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

        public static int Insert(dcCARGO_CREATION_MST cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcCARGO_CREATION_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcCARGO_CREATION_MST>(cObj, true);
                if (id > 0) { cObj.CARGO_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcCARGO_CREATION_MST cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcCARGO_CREATION_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcCARGO_CREATION_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pCARGO_CREATOION_MSTID)
        {
            return Delete(pCARGO_CREATOION_MSTID, null);
        }
        public static bool Delete(int pCARGO_CREATOION_MSTID, DBContext dc)
        {
            dcCARGO_CREATION_MST cObj = new dcCARGO_CREATION_MST();
            cObj.CARGO_ID = pCARGO_CREATOION_MSTID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcCARGO_CREATION_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcCARGO_CREATION_MST cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcCARGO_CREATION_MST cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcCARGO_CREATION_MST cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcCARGO_CREATION_MST cObj, DBContext dc)
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
                                newID = cObj.CARGO_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.CARGO_ID, dc))
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

                        if (cObj.cargoDetails != null)
                        {
                            foreach (dcCARGO_CREATION_DETAIL det in cObj.cargoDetails)
                            {
                                det.CARGO_ID = newID;
                            }
                            bStatus = CARGO_CREATION_DETAILBL.SaveList(cObj.cargoDetails, dc);
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

        public static bool SaveList(List<dcCARGO_CREATION_MST> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcCARGO_CREATION_MST> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcCARGO_CREATION_MST oDet in detList)
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
                    //    bool d = Delete(oDet.CARGO_CREATOION_MSTID, dc);
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
