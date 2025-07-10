using PG.Core.DBBase;
using PG.DBClass.InventoryDC;
using PG.DBClass.ProductionDC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Text;

namespace PG.BLLibrary.ProductionBL
{
    public class SUPPERVISOR_MSTBL
    {
        public static DataLoadOptions SUPPERVISOR_MSTLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcSUPPERVISOR_MST>(obj => obj.relatedclassname);
            return dlo;
        }


        public static string GetSUPPERVISORString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" Select MST.SUPPERVISOR_MSTID,MST.EMP_ID,MST.FULL_NAME,MST.DEPT_ID,MST.DESIGNATION_NAME,MST.ISACTIVE,MST.ISOPERATOR,CONF.STLM_ID ");
            sb.Append(" FROM SUPPERVISOR_MST mst ");
            sb.Append(" LEFT JOIN STLM_SUPPERVISOR_CONFIG CONF ON MST.EMP_ID=CONF.EMP_ID ");
            sb.Append(" Where 1=1 ");
            sb.Append(" AND MST.ISACTIVE= 'Y'");


            return sb.ToString();
        }

        public static string GetSupervisorSqlString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" Select *  ");
            sb.Append(" FROM SUPPERVISOR_MST mst ");
            sb.Append(" Where 1=1 ");


            return sb.ToString();
        }

        public static string GetEmployeeInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT EMP_ID,FULL_NAME,DEPARTMENT DEPT_ID,DEPT_NAME ,DESIGNATION DESIGNATION_ID,DESIG_NAME DESIGNATION_NAME FROM PMIS.EMP_INFO WHERE 1=1");
            return sb.ToString();
        }

        public static List<dcSUPPERVISOR_MST> GetSUPPERVISOR_MSTList()
        {
            return GetSUPPERVISOR_MSTList(null, null);
        }
        public static List<dcSUPPERVISOR_MST> GetSUPPERVISOR_MSTList(DBContext dc)
        {
            return GetSUPPERVISOR_MSTList(null, dc);
        }
        public static List<dcSUPPERVISOR_MST> GetSUPPERVISOR_MSTList(DBQuery dbq, DBContext dc)
        {
            List<dcSUPPERVISOR_MST> cObjList = new List<dcSUPPERVISOR_MST>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcSUPPERVISOR_MST>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcSUPPERVISOR_MST GetSUPPERVISOR_MSTByID(int pSUPPERVISOR_MSTID)
        {
            return GetSUPPERVISOR_MSTByID(pSUPPERVISOR_MSTID, null);
        }
        public static dcSUPPERVISOR_MST GetSUPPERVISOR_MSTByID(int pSUPPERVISOR_MSTID, DBContext dc)
        {
            dcSUPPERVISOR_MST cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcSUPPERVISOR_MST>()
                                  where c.SUPPERVISOR_MSTID == pSUPPERVISOR_MSTID
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


        public static dcSUPPERVISOR_MST GetSUPPERVISOR_MSTByEmpID(int pSUPPERVISOR_MSTID, string pEmp_id,  DBContext dc)
        {
            dcSUPPERVISOR_MST cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcSUPPERVISOR_MST>()
                                  where c.EMP_ID == pEmp_id
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


        public static    dcSUPPERVISOR_MST  GetEmployeeByID(string pEmp_ID)
        {
            return GetEmployeeByID(pEmp_ID, null);
        }
        public static dcSUPPERVISOR_MST  GetEmployeeByID(string pEmp_ID, DBContext dc)
        {
           dcSUPPERVISOR_MST  cObjList = null ;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetEmployeeInfo());
                if (pEmp_ID != "")
                {
                    sb.Append(" AND EMP_ID= @EMP_ID ");
                    cmdInfo.DBParametersInfo.Add("@EMP_ID", pEmp_ID);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                cObjList = DBQuery.ExecuteDBQuery<dcSUPPERVISOR_MST>(dbq, dc).FirstOrDefault();
               
            }
            catch {  }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static dcSUPPERVISOR_MST GetSupervisorInfoByID(int pSupervisorMstId, DBContext dc)
        {
            dcSUPPERVISOR_MST cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetSupervisorSqlString());
                if (pSupervisorMstId > 0)
                {
                    sb.Append(" AND mst.SUPPERVISOR_MSTID= @SupervisorMstId ");
                    cmdInfo.DBParametersInfo.Add("@SupervisorMstId", pSupervisorMstId);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                cObjList = DBQuery.ExecuteDBQuery<dcSUPPERVISOR_MST>(dbq, dc).FirstOrDefault();

            }
            catch { }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcSUPPERVISOR_MST> GetSupervisorList(clsPrmInventory prm, DBContext dc)
        {
            List<dcSUPPERVISOR_MST> cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();
                sb.Append(" SELECT MST.SUPPERVISOR_MSTID,MST.EMP_ID,MST.FULL_NAME,MST.DESIGNATION_NAME ");
                sb.Append(" ,DECODE(MST.ISACTIVE,'Y','YES','NO') ISACTIVE ");
                sb.Append(" ,DECODE(MST.ISOPERATOR,'1','YES','NO') ISOPERATOR ");
                sb.Append(" FROM SUPPERVISOR_MST MST ");
                sb.Append(" WHERE 1=1 ");

                if (prm.IsActive != "0")
                {
                    sb.Append(" AND mst.ISACTIVE= @pIsActive ");
                    cmdInfo.DBParametersInfo.Add("@pIsActive", prm.IsActive);
                }

                if (prm.IsOperator != "")
                {
                    sb.Append(" AND mst.ISOPERATOR= @IsOperator ");
                    cmdInfo.DBParametersInfo.Add("@IsOperator", prm.IsOperator);
                }

                if (prm.DeptID > 0 )
                {
                    sb.Append(" AND mst.DEPT_ID= @DeptID ");
                    cmdInfo.DBParametersInfo.Add("@DeptID", prm.DeptID);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                cObjList = DBQuery.ExecuteDBQuery<dcSUPPERVISOR_MST>(dbq, dc);

            }
            catch { }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcSUPPERVISOR_MST> GetSupervisorListByMachineId(int machineId,int refMachineId, DBContext dc)
        {
            List<dcSUPPERVISOR_MST> cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();
                sb.Append(" SELECT CNF.OPERATOR_ID,CNF.MACHINE_ID,CNF.REF_MACHINE_ID,SUP.EMP_ID,(SUP.FULL_NAME||' ('||SUP.EMP_ID||') ') FULL_NAME,SUP.DESIGNATION_NAME,SUP.DEPT_ID,SUP.STLM_ID ");
                sb.Append(" FROM MACHINE_OPERATOR_CONF CNF ");
                sb.Append(" INNER JOIN SUPPERVISOR_MST SUP ON CNF.OPERATOR_ID=SUP.SUPPERVISOR_MSTID ");
                sb.Append(" WHERE 1=1 ");

                if (machineId > 0)
                {
                    sb.Append(" AND CNF.MACHINE_ID= @machineId ");
                    cmdInfo.DBParametersInfo.Add("@machineId", machineId);
                }

                if (refMachineId > 0)
                {
                    sb.Append(" AND CNF.REF_MACHINE_ID= @refMachineId ");
                    cmdInfo.DBParametersInfo.Add("@refMachineId", refMachineId);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                cObjList = DBQuery.ExecuteDBQuery<dcSUPPERVISOR_MST>(dbq, dc);

            }
            catch { }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }


        public static int Insert(dcSUPPERVISOR_MST cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcSUPPERVISOR_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcSUPPERVISOR_MST>(cObj, true);
                if (id > 0) { cObj.SUPPERVISOR_MSTID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcSUPPERVISOR_MST cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcSUPPERVISOR_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcSUPPERVISOR_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pSUPPERVISOR_MSTID)
        {
            return Delete(pSUPPERVISOR_MSTID, null);
        }
        public static bool Delete(int pSUPPERVISOR_MSTID, DBContext dc)
        {
            dcSUPPERVISOR_MST cObj = new dcSUPPERVISOR_MST();
            cObj.SUPPERVISOR_MSTID = pSUPPERVISOR_MSTID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcSUPPERVISOR_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcSUPPERVISOR_MST cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcSUPPERVISOR_MST cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcSUPPERVISOR_MST cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcSUPPERVISOR_MST cObj, DBContext dc)
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
                                newID = cObj.SUPPERVISOR_MSTID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.SUPPERVISOR_MSTID, dc))
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

        public static bool SaveList(List<dcSUPPERVISOR_MST> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcSUPPERVISOR_MST> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcSUPPERVISOR_MST oDet in detList)
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
                        bool d = Delete(oDet.SUPPERVISOR_MSTID, dc);
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

        public static bool IsSupervisorExists(string pEmpId)
        {
            return IsSupervisorExists(pEmpId, null);
        }
        public static bool IsSupervisorExists(string pEmpId, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                StringBuilder sb = new StringBuilder(GetSupervisorSqlString());

                sb.Append(" AND UPPER(MST.EMP_ID)=UPPER(@pEmpId) ");
                cmdInfo.DBParametersInfo.Add("@pEmpId", pEmpId);



                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                isData = GetSUPPERVISOR_MSTList(dbq, dc).Count > 0;

            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return isData;
        }

        public static bool IsSupervisorExists(string pEmpId,int pSupervisorMstId)
        {
            return IsSupervisorExists(pEmpId, pSupervisorMstId, null);
        }
        public static bool IsSupervisorExists(string pEmpId, int pSupervisorMstId, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                StringBuilder sb = new StringBuilder(GetSupervisorSqlString());

                sb.Append(" AND UPPER(MST.EMP_ID)=UPPER(@pEmpId) ");
                cmdInfo.DBParametersInfo.Add("@pEmpId", pEmpId);

                sb.Append(" AND MST.SUPPERVISOR_MSTID <> @pSupervisorMstId ");
                cmdInfo.DBParametersInfo.Add("@pSupervisorMstId", pSupervisorMstId);


                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                isData = GetSUPPERVISOR_MSTList(dbq, dc).Count > 0;

            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return isData;
        }
    }
}
