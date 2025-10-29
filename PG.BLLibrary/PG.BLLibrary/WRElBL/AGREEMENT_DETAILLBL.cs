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
    public class AGREEMENT_DETAILLBL
    {
        public static DataLoadOptions AGREEMENT_DETAILLLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcAGREEMENT_DETAILL>(obj => obj.relatedclassname);
            return dlo;
        }

        public static string GetAgreementDtlSQLString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT DTL.*,IM.ITEM_NAME,mst.description,dt.TYPE_NAME FROM AGREEMENT_DETAILL DTL ");
            sb.Append(" INNER JOIN Agreement_mst MST ON dtl.agr_id=mst.agr_id ");
            sb.Append(" INNER JOIN ITEM_MST IM ON DTL.ITEM_ID=IM.ITEM_ID ");
            sb.Append(" INNER JOIN DISTANCE_TYPE_MST dt ON DTL.DISTANCE_TYPE_ID=dt.DISTANCE_TYPE_ID  ");
            sb.Append(" WHERE 1=1 ");

            return sb.ToString();
        }

        public static string GetItemByAgreementSQLString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(@"SELECT im.ITEM_ID,im.item_name,im.item_type_id,amst.agr_id,amst.agreement_date
                        ,amst.agreement_name,amst.client_id,adtl.agr_detail_id,adtl.service_amount,adtl.sla_days
                        FROM ITEM_MST im
                        INNER JOIN agreement_detaill adtl ON im.item_id = adtl.item_id
                        INNER JOIN agreement_mst amst ON adtl.agr_id = amst.agr_id
                        WHERE im.is_active = 'Y'
                        AND amst.is_active = 'Y'
                        AND (amst.agreement_date, amst.agr_id) = (
                            SELECT MAX(amst2.agreement_date), MAX(amst2.agr_id)
                            FROM agreement_mst amst2
                            INNER JOIN agreement_detaill adtl2 ON amst2.agr_id = adtl2.agr_id
                            WHERE adtl2.item_id = im.item_id
                              AND adtl2.distance_type_id = adtl.distance_type_id
                              AND amst2.client_id = amst.client_id
                              AND amst2.is_active = 'Y'
                        ) ");

            return sb.ToString();
        }

        public static List<dcAGREEMENT_DETAILL> GetAgreementInfoListById(int pAgrId)
        {
            return GetAgreementInfoListById(pAgrId, null);
        }

        public static List<dcAGREEMENT_DETAILL> GetAgreementInfoListById(int pAgrId, DBContext dc)
        {
            List<dcAGREEMENT_DETAILL> cObjList = new List<dcAGREEMENT_DETAILL>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetAgreementDtlSQLString());
                if (pAgrId > 0)
                {
                    sb.Append(" AND DTL.AGR_ID= @pAgrId ");
                    cmdInfo.DBParametersInfo.Add("@pAgrId", pAgrId);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcAGREEMENT_DETAILL>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcAGREEMENT_DETAILL> GetAGREEMENT_DETAILLList()
        {
            return GetAGREEMENT_DETAILLList(null, null);
        }
        public static List<dcAGREEMENT_DETAILL> GetAGREEMENT_DETAILLList(DBContext dc)
        {
            return GetAGREEMENT_DETAILLList(null, dc);
        }
        public static List<dcAGREEMENT_DETAILL> GetAGREEMENT_DETAILLList(DBQuery dbq, DBContext dc)
        {
            List<dcAGREEMENT_DETAILL> cObjList = new List<dcAGREEMENT_DETAILL>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcAGREEMENT_DETAILL>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcAGREEMENT_DETAILL GetAGREEMENT_DETAILLByID(int pAGREEMENT_DETAILLID)
        {
            return GetAGREEMENT_DETAILLByID(pAGREEMENT_DETAILLID, null);
        }
        public static dcAGREEMENT_DETAILL GetAGREEMENT_DETAILLByID(int pAGREEMENT_DETAILLID, DBContext dc)
        {
            dcAGREEMENT_DETAILL cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcAGREEMENT_DETAILL>()
                                  where c.AGR_DETAIL_ID == pAGREEMENT_DETAILLID
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

        public static int Insert(dcAGREEMENT_DETAILL cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcAGREEMENT_DETAILL cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcAGREEMENT_DETAILL>(cObj, true);
                if (id > 0) { cObj.AGR_DETAIL_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcAGREEMENT_DETAILL cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcAGREEMENT_DETAILL cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcAGREEMENT_DETAILL>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pAGREEMENT_DETAILLID)
        {
            return Delete(pAGREEMENT_DETAILLID, null);
        }
        public static bool Delete(int pAGREEMENT_DETAILLID, DBContext dc)
        {
            dcAGREEMENT_DETAILL cObj = new dcAGREEMENT_DETAILL();
            cObj.AGR_DETAIL_ID = pAGREEMENT_DETAILLID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcAGREEMENT_DETAILL>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcAGREEMENT_DETAILL cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcAGREEMENT_DETAILL cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcAGREEMENT_DETAILL cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcAGREEMENT_DETAILL cObj, DBContext dc)
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
                                newID = cObj.AGR_DETAIL_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.AGR_DETAIL_ID, dc))
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

        public static bool SaveList(List<dcAGREEMENT_DETAILL> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcAGREEMENT_DETAILL> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcAGREEMENT_DETAILL oDet in detList)
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
                        bool d = Delete(oDet.AGR_DETAIL_ID, dc);
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

        public static string getServiceAmountByItemID(int pclientID, int pitemID,int pDistancetypeid, DBContext dc)
        {
            bool isDCInit = false;
            string AutoStatus = string.Empty;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder("SELECT SERVICE_AMOUNT FROM AGREEMENT_MST a INNER JOIN AGREEMENT_DETAILL b ON a.AGR_ID=b.AGR_ID Where a.CLIENT_ID=@pclientID and b.ITEM_ID=@pitemID AND b.DISTANCE_TYPE_ID=@pDistancetypeid AND ROWNUM = 1 ");
                cmdInfo.DBParametersInfo.Add("@pclientID", pclientID);
                cmdInfo.DBParametersInfo.Add("@pitemID", pitemID);
                cmdInfo.DBParametersInfo.Add("@pDistancetypeid", pDistancetypeid);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                AutoStatus = Convert.ToString(DBQuery.ExecuteDBScalar(dbq, dc));

            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return AutoStatus;
        }

        public static string getAgreementdtlIDByItemID(int pclientID, int pitemID,int pDistancetypeid, DBContext dc)
        {
            bool isDCInit = false;
            string AutoStatus = string.Empty;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder("SELECT b.AGR_DETAIL_ID FROM AGREEMENT_MST a INNER JOIN AGREEMENT_DETAILL b ON a.AGR_ID=b.AGR_ID Where a.CLIENT_ID=@pclientID and b.ITEM_ID=@pitemID AND b.DISTANCE_TYPE_ID=@pDistancetypeid AND ROWNUM = 1 ");
                cmdInfo.DBParametersInfo.Add("@pclientID", pclientID);
                cmdInfo.DBParametersInfo.Add("@pitemID", pitemID);
                cmdInfo.DBParametersInfo.Add("@pDistancetypeid", pDistancetypeid);
                

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                AutoStatus = Convert.ToString(DBQuery.ExecuteDBScalar(dbq, dc));

            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return AutoStatus;
        }
    }
}
