using PG.Core.DBBase;
using PG.DBClass.InventoryDC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.BLLibrary.InventoryBL
{
    public class QC_MASTERBL
    {
        public static DataLoadOptions QC_MASTERLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcQC_MASTER>(obj => obj.relatedclassname);
            return dlo;
        }

        public static string GetQCMaster_ListString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT QMST.*,MRRMST.MRR_NO,MRRMST.MRR_DATE,SUP.SUP_NAME  ");
            sb.Append(" ,QTYPE.QC_TYPE_NAME,USR.FULLNAME ");
            //sb.Append(" ,F_CONCATE_MRR_DTL(MRRMST.MRR_ID) ITEM_NAME ");
            sb.Append(" FROM QC_MASTER QMST ");
            sb.Append(" INNER JOIN MRR_MASTER MRRMST ON QMST.TRANS_ID=MRRMST.MRR_ID ");
            sb.Append(" INNER JOIN SUPPLIER_INFO SUP ON QMST.SUP_ID=SUP.SUP_ID ");
            sb.Append(" INNER JOIN QC_TYPE QTYPE ON QMST.QC_TYPE_ID=QTYPE.QC_TYPE_ID ");
            sb.Append(" INNER JOIN TBLUSER USR ON QMST.CREATE_BY=USR.USERID ");
            sb.Append(" WHERE 1=1 ");
            return sb.ToString();
        }

        public static string GetQCMaster_ListForRef_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT QMST.*,SUP.SUP_NAME,MMST.MRR_NO,MMST.MRR_DATE,MMST.CHALLAN_NO SUP_CHALLAN_NO,MMST.DEPARTMENT_ID,USR.FULLNAME  ");
            sb.Append(" ,case when MMST.INV_TRANS_TYPE_ID=101 then lppurchase.PURCHASE_NO  when MMST.INV_TRANS_TYPE_ID=102 then impPurchase.IMP_PURCHASE_NO  else '' end PURCHASE_NO ");
            sb.Append(" ,case when MMST.INV_TRANS_TYPE_ID=101 then lppurchase.PURCHASE_ID  when MMST.INV_TRANS_TYPE_ID=102 then impPurchase.IMP_PURCHASE_ID  else 0 end PURCHASE_ID ");
            sb.Append(" ,FN_IS_RETURN_QC(QMST.QC_ID) IS_RETURN ");
            sb.Append(" FROM QC_MASTER QMST ");
            sb.Append(" INNER JOIN SUPPLIER_INFO SUP ON QMST.SUP_ID=SUP.SUP_ID ");
            sb.Append(" INNER JOIN MRR_MASTER MMST ON QMST.TRANS_ID=MMST.MRR_ID ");
            sb.Append(" INNER JOIN TBLUSER USR ON QMST.CREATE_BY=USR.USERID ");
            sb.Append(" LEFT OUTER JOIN LP_PURCHASE_MASTER lppurchase on MMST.TRANS_ID=lppurchase.PURCHASE_ID and MMST.INV_TRANS_TYPE_ID=101 ");
            sb.Append(" LEFT OUTER JOIN IMP_PURCHASE_MASTER impPurchase on MMST.TRANS_ID=impPurchase.IMP_PURCHASE_ID and MMST.INV_TRANS_TYPE_ID=102 ");
            sb.Append(" WHERE 1=1 ");
            sb.Append(" AND QMST.QC_TYPE_ID=2 ");
            return sb.ToString();
        }
        public static List<dcQC_MASTER> GetQC_MASTERList()
        {
            return GetQC_MASTERList(null, null);
        }
        public static List<dcQC_MASTER> GetQC_MASTERList(DBContext dc)
        {
            return GetQC_MASTERList(null, dc);
        }
        public static List<dcQC_MASTER> GetQC_MASTERList(DBQuery dbq, DBContext dc)
        {
            List<dcQC_MASTER> cObjList = new List<dcQC_MASTER>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcQC_MASTER>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcQC_MASTER GetQC_MASTERByID(int pQC_MASTERID)
        {
            return GetQC_MASTERByID(pQC_MASTERID, null);
        }
        public static dcQC_MASTER GetQC_MASTERByID(int pQC_MASTERID, DBContext dc)
        {
            dcQC_MASTER cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetQCMaster_ListString());

                if (pQC_MASTERID > 0)
                {
                    sb.Append(" AND QMST.QC_ID=@pQC_MASTERID ");
                    cmdInfo.DBParametersInfo.Add("@pQC_MASTERID", pQC_MASTERID);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                cObj = DBQuery.ExecuteDBQuery<dcQC_MASTER>(dbq, dc).ToList().FirstOrDefault();


                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    var result = (from c in dataContext.GetTable<dcQC_MASTER>()
                //                  where c.QC_ID == pQC_MASTERID
                //                  select c).ToList();
                //    if (result.Count() > 0)
                //    {
                //        cObj = result.First();
                //    }
                //}
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObj;
        }

        public static dcQC_MASTER GetQC_MASTERByIDForRef(int pQC_MASTERID, DBContext dc)
        {
            dcQC_MASTER cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetQCMaster_ListForRef_SQLString());

                if (pQC_MASTERID > 0)
                {
                    sb.Append(" AND QMST.QC_ID=@pQC_MASTERID ");
                    cmdInfo.DBParametersInfo.Add("@pQC_MASTERID", pQC_MASTERID);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                cObj = DBQuery.ExecuteDBQuery<dcQC_MASTER>(dbq, dc).ToList().FirstOrDefault();

            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObj;
        }

        public static int Insert(dcQC_MASTER cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcQC_MASTER cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcQC_MASTER>(cObj, true);
                if (id > 0) { cObj.QC_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcQC_MASTER cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcQC_MASTER cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcQC_MASTER>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pQC_MASTERID)
        {
            return Delete(pQC_MASTERID, null);
        }
        public static bool Delete(int pQC_MASTERID, DBContext dc)
        {
            dcQC_MASTER cObj = new dcQC_MASTER();
            cObj.QC_ID = pQC_MASTERID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcQC_MASTER>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcQC_MASTER cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcQC_MASTER cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcQC_MASTER cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcQC_MASTER cObj, DBContext dc)
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
                                newID = cObj.QC_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.QC_ID, dc))
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

        public static bool SaveList(List<dcQC_MASTER> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcQC_MASTER> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcQC_MASTER oDet in detList)
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
                        bool d = Delete(oDet.QC_ID, dc);
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

        public static List<dcQC_MASTER> GetQC_TypeList( DBContext dc)
        {
            List<dcQC_MASTER> cObjList = new List<dcQC_MASTER>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                sb.Append(" select QC_TYPE_ID,QC_TYPE_NAME from QC_TYPE WHERE 1=1 and IS_ACTIVE='Y' ");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcQC_MASTER>(dbq, dc).ToList();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static string New_QC_NO(DBContext dc, DateTime? qcDate)
        {
            bool isDCInit = false;
            string new_Transaction_no = string.Empty;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                string abbr = " SELECT FN_NEW_QC_NO(@qcDate) A from Dual ";
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.DBParametersInfo.Add("@qcDate", qcDate);
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                new_Transaction_no = Convert.ToString(DBQuery.ExecuteDBScalar(dbq, dc));
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return new_Transaction_no;
        }

        public static List<dcQC_MASTER> GetQC_MasterList(clsPrmInventory prm, DBContext dc)
        {
            List<dcQC_MASTER> cObjList = new List<dcQC_MASTER>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetQCMaster_ListString());

                if (prm.QC_NO != string.Empty)
                {
                    sb.Append(" AND QMST.QC_NO Like @QC_NO");
                    //cmd.Parameters.AddWithValue("@journalNo", journalNo + "%");
                    cmdInfo.DBParametersInfo.Add("@QC_NO", prm.QC_NO + "%");
                }


                if (prm.FromDate.HasValue)
                {
                    if (prm.ToDate.HasValue)
                    {
                        sb.Append(" AND (QMST.QC_DATE BETWEEN @fromDate AND @toDate) ");
                        cmdInfo.DBParametersInfo.Add("@fromDate", prm.FromDate.Value);
                        cmdInfo.DBParametersInfo.Add("@toDate", prm.ToDate.Value);
                    }
                    else
                    {
                        sb.Append(" AND (QMST.QC_DATE = @fromDate) ");
                        cmdInfo.DBParametersInfo.Add("@fromDate", prm.FromDate.Value);
                    }
                }
                if(prm.QC_TYPE_ID > 0)
                {
                    sb.Append(" AND (QMST.QC_TYPE_ID = @QC_TYPE_ID) ");
                    cmdInfo.DBParametersInfo.Add("@QC_TYPE_ID", prm.QC_TYPE_ID);
                }

                if (prm.sup_id > 0)
                {
                    sb.Append(" AND (QMST.SUP_ID = @sup_id) ");
                    cmdInfo.DBParametersInfo.Add("@sup_id", prm.sup_id);
                }

                if (prm.mr_no!=string.Empty)
                {
                    sb.Append(" AND (MRRMST.MRR_NO = @mr_no) ");
                    cmdInfo.DBParametersInfo.Add("@mr_no", prm.mr_no);
                }



                sb.Append(" ORDER BY QMST.QC_NO DESC ");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcQC_MASTER>(dbq, dc).ToList();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
    }
}
