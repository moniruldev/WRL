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
    public class CARGO_TRACKINGBL
    {
        public static DataLoadOptions CARGO_TRACKINGLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcCARGO_TRACKING>(obj => obj.relatedclassname);
            return dlo;
        }

        public static string GetCargoTrackingSQLString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT C.*,fh.HUB_NAME F_HUBNAME,th.HUB_NAME T_HUBNAME,cc.CARGO_NUMBER,tm.TRANS_MEDIA_NAME ");
            sb.Append("  FROM CARGO_TRACKING c ");
            sb.Append("  INNER JOIN HUB_MST fh ON c.FROM_HUB_ID=fh.HUB_ID ");
            sb.Append("  INNER JOIN HUB_MST th ON c.TO_HUB_ID=th.HUB_ID ");
            sb.Append("  INNER JOIN CARGO_CREATION_MST cc ON c.CARGO_ID=cc.CARGO_ID ");
            sb.Append("  LEFT JOIN TRANSPORT_MEDIA_MST tm ON c.TRANS_MEDIA_ID=tm.TRANS_MEDIA_ID ");
            sb.Append(" WHERE 1=1 ");

            return sb.ToString();
        }
        public static dcCARGO_TRACKING GetCargoReceivedInfoById(int pCARGO_TRACK_ID)
        {
            return GetCargoReceivedListInfoById(pCARGO_TRACK_ID, null).FirstOrDefault();
        }
        public static List<dcCARGO_TRACKING> GetCargoReceivedListInfoById(int pCARGO_TRACK_ID, DBContext dc)
        {
            List<dcCARGO_TRACKING> cObjList = new List<dcCARGO_TRACKING>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetCargoTrackingSQLString());
                if (pCARGO_TRACK_ID > 0)
                {
                    sb.Append(" AND c.CARGO_TRACK_ID= @pCARGO_TRACK_ID ");
                    cmdInfo.DBParametersInfo.Add("@pCARGO_TRACK_ID", pCARGO_TRACK_ID);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcCARGO_TRACKING>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcCARGO_TRACKING> GetCargoTransferListInfo(dcCARGO_TRACKING Prm, DBContext dc)
        {
            List<dcCARGO_TRACKING> cObjList = new List<dcCARGO_TRACKING>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetCargoTrackingSQLString());
                if (Prm.TRANS_TYPE!=string.Empty)
                {
                    sb.Append(" AND c.TRANS_TYPE= @pTRANS_TYPE ");
                    cmdInfo.DBParametersInfo.Add("@pTRANS_TYPE", Prm.TRANS_TYPE);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcCARGO_TRACKING>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static List<dcCARGO_TRACKING> GetCARGO_TRACKINGList()
        {
            return GetCARGO_TRACKINGList(null, null);
        }
        public static List<dcCARGO_TRACKING> GetCARGO_TRACKINGList(DBContext dc)
        {
            return GetCARGO_TRACKINGList(null, dc);
        }
        public static List<dcCARGO_TRACKING> GetCARGO_TRACKINGList(DBQuery dbq, DBContext dc)
        {
            List<dcCARGO_TRACKING> cObjList = new List<dcCARGO_TRACKING>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcCARGO_TRACKING>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcCARGO_TRACKING GetCARGO_TRACKINGByID(int pCARGO_TRACKINGID)
        {
            return GetCARGO_TRACKINGByID(pCARGO_TRACKINGID, null);
        }
        public static dcCARGO_TRACKING GetCARGO_TRACKINGByID(int pCARGO_TRACKINGID, DBContext dc)
        {
            dcCARGO_TRACKING cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcCARGO_TRACKING>()
                                  where c.CARGO_TRACK_ID == pCARGO_TRACKINGID
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

        public static int Insert(dcCARGO_TRACKING cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcCARGO_TRACKING cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcCARGO_TRACKING>(cObj, true);
                if (id > 0) { cObj.CARGO_TRACK_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcCARGO_TRACKING cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcCARGO_TRACKING cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcCARGO_TRACKING>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pCARGO_TRACKINGID)
        {
            return Delete(pCARGO_TRACKINGID, null);
        }
        public static bool Delete(int pCARGO_TRACKINGID, DBContext dc)
        {
            dcCARGO_TRACKING cObj = new dcCARGO_TRACKING();
            cObj.CARGO_TRACK_ID = pCARGO_TRACKINGID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcCARGO_TRACKING>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcCARGO_TRACKING cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcCARGO_TRACKING cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcCARGO_TRACKING cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcCARGO_TRACKING cObj, DBContext dc)
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
                                newID = cObj.CARGO_TRACK_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.CARGO_TRACK_ID, dc))
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

        public static bool SaveList(List<dcCARGO_TRACKING> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcCARGO_TRACKING> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcCARGO_TRACKING oDet in detList)
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
                    //    bool d = Delete(oDet.CARGO_TRACKINGID, dc);
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

        public static string GetCargoReceivePendingSQLString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT C.*,fh.HUB_NAME F_HUBNAME,th.HUB_NAME T_HUBNAME,cc.CARGO_NUMBER,tm.TRANS_MEDIA_NAME ");
            sb.Append("  FROM CARGO_TRACKING c ");
            sb.Append("  INNER JOIN HUB_MST fh ON c.FROM_HUB_ID=fh.HUB_ID ");
            sb.Append("  INNER JOIN HUB_MST th ON c.TO_HUB_ID=th.HUB_ID ");
            sb.Append("  INNER JOIN CARGO_CREATION_MST cc ON c.CARGO_ID=cc.CARGO_ID ");
            sb.Append("  LEFT JOIN TRANSPORT_MEDIA_MST tm ON c.TRANS_MEDIA_ID=tm.TRANS_MEDIA_ID ");
            sb.Append(" WHERE 1=1 AND c.CARGO_TRACK_ID NOT IN (SELECT REF_TRANS_ID FROM CARGO_TRACKING WHERE REF_TRANS_ID IS NOT NULL) AND TRANS_TYPE='I'");

            return sb.ToString();
        }
    }
}
