using PG.Core.DBBase;
using PG.Core.Utility;
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
    public class CN_ASSIGNMENTBL
    {
        public static DataLoadOptions CN_ASSIGNMENTLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcCN_ASSIGNMENT>(obj => obj.relatedclassname);
            return dlo;
        }

        public static string GetCNAssignmentMstinfoString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT DISTINCT MST.CN_ASSIGN_ID,MST.ASSIGN_DATE,mst.DELIVERY_MAN_ID,STD.DELIVERY_MAN_NAME ");
            sb.Append(" FROM CN_ASSIGNMENT mst ");
            sb.Append(" INNER JOIN DELIVERY_MAN_MST STD ON mst.DELIVERY_MAN_ID=std.DELIVERY_MAN_ID ");
            
            sb.Append(" WHERE 1=1 ");

            return sb.ToString();
        }

        public static string GetCNAssignmentMstListString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT  MST.CN_ASSIGN_ID,MST.ASSIGN_DATE,MST.CN_ID,mst.DELIVERY_MAN_ID,STD.CN_NUMBER,STD.CONSIGNEE_NAME,STD.CONSIGNEE_MOBILE_NO,STD.CONSIGNEE_ADDRESS,dm.DELIVERY_MAN_NAME,STD.POD,STD.OTP_CODE,STD.CUSTOMER_OTP,(SELECT ad.IS_OTP_SERVICE FROM AGREEMENT_DETAILL ad WHERE ad.ITEM_ID = STD.ITEM_ID AND ad.AGR_DETAIL_ID = ( SELECT MAX(AGR_DETAIL_ID) FROM AGREEMENT_DETAILL WHERE ITEM_ID = STD.ITEM_ID ) ) AS IS_OTP_SERVICE ");
            sb.Append(" FROM CN_ASSIGNMENT mst ");
            sb.Append(" INNER JOIN CN_CREATION_MST STD ON mst.CN_ID=std.CN_ID ");
            sb.Append(" INNER JOIN DELIVERY_MAN_MST dm ON mst.DELIVERY_MAN_ID=dm.DELIVERY_MAN_ID ");

            sb.Append(" WHERE 1=1 and  STD.IS_DELIVERED='N' ");

            return sb.ToString();
        }

        public static dcCN_ASSIGNMENT GetCNAssignMstInfoById(int pCN_ASSIGN_ID)
        {
            return GetCNAssignMstInfoById(pCN_ASSIGN_ID, null);
        }

        public static dcCN_ASSIGNMENT GetCNAssignMstInfoById(int pCN_ASSIGN_ID, DBContext dc)
        {
            dcCN_ASSIGNMENT cObjList = new dcCN_ASSIGNMENT();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetCNAssignmentMstinfoString());
                if (pCN_ASSIGN_ID > 0)
                {
                    sb.Append(" AND mst.CN_ASSIGN_ID= @pCN_ASSIGN_ID ");
                    cmdInfo.DBParametersInfo.Add("@pCN_ASSIGN_ID", pCN_ASSIGN_ID);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcCN_ASSIGNMENT>(dbq, dc).FirstOrDefault();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcCN_ASSIGNMENT> GetCNAssignmentMstListData()
        {
            return GetCNAssignmentMstListData(0, null);
        }

        public static List<dcCN_ASSIGNMENT> GetCNAssignmentMstListData(int pCN_ASSIGN_ID, DBContext dc)
        {
            List<dcCN_ASSIGNMENT> cObjList = new List<dcCN_ASSIGNMENT>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetCNAssignmentMstinfoString());
                if (pCN_ASSIGN_ID > 0)
                {
                    sb.Append(" AND mst.CN_ASSIGN_ID= @pCN_ASSIGN_ID ");
                    cmdInfo.DBParametersInfo.Add("@pCN_ASSIGN_ID", pCN_ASSIGN_ID);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcCN_ASSIGNMENT>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }


        public static List<dcCN_ASSIGNMENT> GetCNAssignmentMstListDatabyDelManID(int pDElMan_ID, DBContext dc)
        {
            List<dcCN_ASSIGNMENT> cObjList = new List<dcCN_ASSIGNMENT>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetCNAssignmentMstListString());
                if (pDElMan_ID > 0)
                {
                    sb.Append(" AND mst.DELIVERY_MAN_ID= @pDElMan_ID ");
                    cmdInfo.DBParametersInfo.Add("@pDElMan_ID", pDElMan_ID);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcCN_ASSIGNMENT>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcCN_ASSIGNMENT> GetCNAssignmentMstList()
        {
            return GetCNAssignmentMstList(0, null);
        }

        public static List<dcCN_ASSIGNMENT> GetCNAssignmentMstList(int pCN_ASSIGN_ID, DBContext dc)
        {
            List<dcCN_ASSIGNMENT> cObjList = new List<dcCN_ASSIGNMENT>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetCNAssignmentMstListString());
                if (pCN_ASSIGN_ID > 0)
                {
                    sb.Append(" AND mst.CN_ASSIGN_ID= @pCN_ASSIGN_ID ");
                    cmdInfo.DBParametersInfo.Add("@pCN_ASSIGN_ID", pCN_ASSIGN_ID);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcCN_ASSIGNMENT>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static List<dcCN_ASSIGNMENT> GetCN_ASSIGNMENTList()
        {
            return GetCN_ASSIGNMENTList(null, null);
        }
        public static List<dcCN_ASSIGNMENT> GetCN_ASSIGNMENTList(DBContext dc)
        {
            return GetCN_ASSIGNMENTList(null, dc);
        }
        public static List<dcCN_ASSIGNMENT> GetCN_ASSIGNMENTList(DBQuery dbq, DBContext dc)
        {
            List<dcCN_ASSIGNMENT> cObjList = new List<dcCN_ASSIGNMENT>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcCN_ASSIGNMENT>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcCN_ASSIGNMENT GetCN_ASSIGNMENTByID(int pCN_ASSIGNMENTID)
        {
            return GetCN_ASSIGNMENTByID(pCN_ASSIGNMENTID, null);
        }
        public static dcCN_ASSIGNMENT GetCN_ASSIGNMENTByID(int pCN_ASSIGNMENTID, DBContext dc)
        {
            dcCN_ASSIGNMENT cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcCN_ASSIGNMENT>()
                                  where c.CN_ASSIGN_ID == pCN_ASSIGNMENTID
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

        public static int Insert(dcCN_ASSIGNMENT cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcCN_ASSIGNMENT cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcCN_ASSIGNMENT>(cObj, true);
                if (id > 0) { cObj.CN_ASSIGN_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcCN_ASSIGNMENT cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcCN_ASSIGNMENT cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcCN_ASSIGNMENT>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pCN_ASSIGNMENTID)
        {
            return Delete(pCN_ASSIGNMENTID, null);
        }
        public static bool Delete(int pCN_ASSIGNMENTID, DBContext dc)
        {
            dcCN_ASSIGNMENT cObj = new dcCN_ASSIGNMENT();
            cObj.CN_ASSIGN_ID = pCN_ASSIGNMENTID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcCN_ASSIGNMENT>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcCN_ASSIGNMENT cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcCN_ASSIGNMENT cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcCN_ASSIGNMENT cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcCN_ASSIGNMENT cObj, DBContext dc)
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
                                newID = cObj.CN_ASSIGN_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.CN_ASSIGN_ID, dc))
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

        public static bool SaveList(List<dcCN_ASSIGNMENT> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcCN_ASSIGNMENT> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcCN_ASSIGNMENT oDet in detList)
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
                    //    bool d = Delete(oDet.CN_ASSIGNMENTID, dc);
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

        public static string GetCNListinfoSQLString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT mst.* FROM CN_CREATION_MST mst  ");
            sb.Append(" WHERE 1=1 ");

            return sb.ToString();
        }

        public static dcCN_ASSIGNMENT GetCNInfoByCNNumberassign(string pCN_NO)
        {
            return GetCNInfoByCNNumberassignList(pCN_NO, null).FirstOrDefault();
        }
        public static List<dcCN_ASSIGNMENT> GetCNInfoByCNNumberassignList(string pCN_NO, DBContext dc)
        {
            List<dcCN_ASSIGNMENT> cObjList = new List<dcCN_ASSIGNMENT>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetCNListinfoSQLString());
                if (pCN_NO != string.Empty)
                {
                    sb.Append(" AND mst.CN_NUMBER= @pCN_NO ");
                    cmdInfo.DBParametersInfo.Add("@pCN_NO", pCN_NO);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcCN_ASSIGNMENT>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static int CheckAlreadyExistCNNo(string pCNNo, int pDeliveryManID)
        {
            return CheckAlreadyExistCNNo(pCNNo, pDeliveryManID, null);
        }
        public static int CheckAlreadyExistCNNo(string pCNNo, int pDeliveryManID, DBContext dc)
        {
            //dcINVOICE_MASTER cObj = null;
            bool isDCInit = false;
            int chkinvaty = 0;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder("SELECT COUNT(*) FROM CN_ASSIGNMENT WHERE CN_NUMBER=@pCNNo AND DELIVERY_MAN_ID=@pDeliveryManID ");
                cmdInfo.DBParametersInfo.Add("@pCNNo", pCNNo);

                cmdInfo.DBParametersInfo.Add("@pDeliveryManID", pDeliveryManID);


                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                chkinvaty = Conversion.DBNullIntToZero(DBQuery.ExecuteDBScalar(dbq, dc));
                //cObj = DBQuery.ExecuteDBQuery<dcINVOICE_MASTER>(dbq, dc).FirstOrDefault();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return chkinvaty;
        }

        public static string GetCNListinfoSQLStringcln()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT mst.*,cl.CLIENT_NAME FROM CN_CREATION_MST mst INNER JOIN CLIENT_MST cl ON mst.CLIENT_ID=cl.CLIENT_ID ");
            sb.Append(" WHERE 1=1 ");

            return sb.ToString();
        }

        public static dcCN_ASSIGNMENT GetCNInfoByCNNumber(string pCN_NO)
        {
            return GetCNInfoByCNNumberList(pCN_NO, null).FirstOrDefault();
        }
        public static List<dcCN_ASSIGNMENT> GetCNInfoByCNNumberList(string pCN_NO, DBContext dc)
        {
            List<dcCN_ASSIGNMENT> cObjList = new List<dcCN_ASSIGNMENT>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetCNListinfoSQLStringcln());
                if (pCN_NO != string.Empty)
                {
                    sb.Append(" AND mst.CN_NUMBER= @pCN_NO ");
                    cmdInfo.DBParametersInfo.Add("@pCN_NO", pCN_NO);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcCN_ASSIGNMENT>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static string UpdateCNByCNID(int pCN_ID,string otpcode, DBContext dc)
        {
            bool isDCInit = false;
            string _CNID = string.Empty;
            try
            {

                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();

                string abbr = " UPDATE CN_CREATION_MST SET IS_DELIVERED='Y',DELIVERY_DATE= SYSDATE,CUSTOMER_OTP=@otpcode WHERE CN_ID=@CN_ID ";
                cmdInfo.DBParametersInfo.Add("@otpcode", otpcode);
                cmdInfo.DBParametersInfo.Add("@CN_ID", pCN_ID);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBNonQuery(dbq, dc);
                _CNID = pCN_ID.ToString();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return _CNID;
        }
        public static string UpdateCNReturnInfoByCNID(int pCN_ID, string otpcode,int rtncauseid, DBContext dc)
        {
            bool isDCInit = false;
            string _CNID = string.Empty;
            try
            {

                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();

                string abbr = " UPDATE CN_CREATION_MST SET IS_REFUND='Y',REFUND_CAUSE_ID=rtncauseid,REFUND_DATE=SYSDATE WHERE CN_ID=@CN_ID ";
           
                cmdInfo.DBParametersInfo.Add("@CN_ID", pCN_ID);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBNonQuery(dbq, dc);
                _CNID = pCN_ID.ToString();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return _CNID;
        }
        public static string UpdateGenerateOTPByCNID(int pCN_ID, string otpcode, DBContext dc)
        {
            bool isDCInit = false;
            string _CNID = string.Empty;
            try
            {

                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();

                string abbr = " UPDATE CN_CREATION_MST SET OTP_CODE=@otpcode WHERE CN_ID=@CN_ID ";
                cmdInfo.DBParametersInfo.Add("@otpcode", otpcode);
                cmdInfo.DBParametersInfo.Add("@CN_ID", pCN_ID);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                DBQuery.ExecuteDBNonQuery(dbq, dc);
                _CNID = pCN_ID.ToString();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return _CNID;
        }
    }
}
