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
    public class AGREEMENT_MSTBL
    {
        public static DataLoadOptions AGREEMENT_MSTLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcAGREEMENT_MST>(obj => obj.relatedclassname);
            return dlo;
        }

        public static string GetAgreementMstListString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT MST.*,cl.CLIENT_NAME ,DTD.DEPT_NAME,BD.DEPT_NAME BILLING_DEPT ");
            sb.Append(" FROM AGREEMENT_MST mst " );
            sb.Append(" INNER JOIN CLIENT_MST cl ON mst.CLIENT_ID=cl.CLIENT_ID " );
            sb.Append(" INNER JOIN DEPARTMENT_MST DTD ON mst.DEPT_ID=DTD.DEPT_ID " );
            sb.Append(" LEFT JOIN DEPARTMENT_MST BD ON mst.BILLINGDEPT_ID=BD.DEPT_ID " );
            sb.Append(" WHERE 1=1 " );

            return sb.ToString();
        }
        public static dcAGREEMENT_MST GetAgreementMstInfoById(int pAGR_Id)
        {
            return GetAgreementMstList(pAGR_Id, null).FirstOrDefault();
        }

        public static List<dcAGREEMENT_MST> GetAgreementMstList()
        {
            return GetAgreementMstList(0, null);
        }

        public static List<dcAGREEMENT_MST> GetAgreementMstList(int pAGR_Id, DBContext dc)
        {
            List<dcAGREEMENT_MST> cObjList = new List<dcAGREEMENT_MST>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetAgreementMstListString());
                if (pAGR_Id > 0)
                {
                    sb.Append(" AND mst.AGR_ID= @pAGR_Id ");
                    cmdInfo.DBParametersInfo.Add("@pAGR_Id", pAGR_Id);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcAGREEMENT_MST>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcAGREEMENT_MST> GetAgreementList(clsPrmWREL prmHms, DBContext dc)
        {
            List<dcAGREEMENT_MST> cObjList = new List<dcAGREEMENT_MST>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetAgreementMstListString());


                if(prmHms.CLIENT_ID>0)
                {
                    sb.Append(" AND mst.CLIENT_ID= @CLIENT_ID ");
                    cmdInfo.DBParametersInfo.Add("@CLIENT_ID", prmHms.CLIENT_ID);
                }

                if (prmHms.IsActive != "0")
                {
                    sb.Append(" AND mst.IS_ACTIVE= @IS_ACTIVE ");
                    cmdInfo.DBParametersInfo.Add("@IS_ACTIVE", prmHms.IsActive);
                }
                if (prmHms.FromDate.HasValue)
                {
                    if (prmHms.ToDate.HasValue)
                    {
                        sb.Append(" AND (TO_DATE(mst.AGREEMENT_DATE) BETWEEN @fromDate AND @toDate) ");
                        cmdInfo.DBParametersInfo.Add("@fromDate", prmHms.FromDate.Value);
                        cmdInfo.DBParametersInfo.Add("@toDate", prmHms.ToDate.Value);
                    }
                    else
                    {
                        sb.Append(" AND TO_DATE(mst.AGREEMENT_DATE) = @fromDate ");
                        cmdInfo.DBParametersInfo.Add("@fromDate", prmHms.FromDate.Value);

                    }

                }


                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcAGREEMENT_MST>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }


        public static dcAGREEMENT_MST GetAGREEMENT_MSTByID(int pAGREEMENT_MSTID)
        {
            return GetAGREEMENT_MSTByID(pAGREEMENT_MSTID, null);
        }
        public static dcAGREEMENT_MST GetAGREEMENT_MSTByID(int pAGREEMENT_MSTID, DBContext dc)
        {
            dcAGREEMENT_MST cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcAGREEMENT_MST>()
                                  where c.AGR_ID == pAGREEMENT_MSTID
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

        public static int Insert(dcAGREEMENT_MST cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcAGREEMENT_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcAGREEMENT_MST>(cObj, true);
                if (id > 0) { cObj.AGR_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcAGREEMENT_MST cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcAGREEMENT_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcAGREEMENT_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pAGREEMENT_MSTID)
        {
            return Delete(pAGREEMENT_MSTID, null);
        }
        public static bool Delete(int pAGREEMENT_MSTID, DBContext dc)
        {
            dcAGREEMENT_MST cObj = new dcAGREEMENT_MST();
            cObj.AGR_ID = pAGREEMENT_MSTID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcAGREEMENT_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcAGREEMENT_MST cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcAGREEMENT_MST cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcAGREEMENT_MST cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcAGREEMENT_MST cObj, DBContext dc)
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
                                newID = cObj.AGR_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.AGR_ID, dc))
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

                        if (cObj.agreementDetails != null)
                        {
                            foreach (dcAGREEMENT_DETAILL det in cObj.agreementDetails)
                            {
                                det.AGR_ID = newID;
                            }
                            bStatus = AGREEMENT_DETAILLBL.SaveList(cObj.agreementDetails, dc);
                        }

                        //bStatus = true;
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

        public static bool SaveList(List<dcAGREEMENT_MST> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcAGREEMENT_MST> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcAGREEMENT_MST oDet in detList)
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
                    //    bool d = Delete(oDet.AGREEMENT_MSTID, dc);
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
