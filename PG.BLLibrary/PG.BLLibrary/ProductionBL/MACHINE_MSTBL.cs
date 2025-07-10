using PG.Core.DBBase;
using PG.DBClass.ProductionDC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Text;

namespace PG.BLLibrary.ProductionBL
{
    public class MACHINE_MSTBL
    {

        public static string GetMachineListString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" Select *  ");
            sb.Append(" FROM MACHINE_MST mst ");
            sb.Append(" Where 1=1 ");
            return sb.ToString();
        }

        public static string GetASMMachineListString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT DISTINCT MACHINE_OPERATOR_CONF.MACHINE_ID,MACHINE_MST_ASM.MACHINE_NAME FROM MACHINE_OPERATOR_CONF ");
            sb.Append(" INNER JOIN MACHINE_MST_ASM ON MACHINE_OPERATOR_CONF.MACHINE_ID=MACHINE_MST_ASM.MACHINE_ID ");
            sb.Append(" Where 1=1 ");
            return sb.ToString();
        }

        public static List<dcMACHINE_MST> GetMachineListBYDeptID(string _DEPT_ID, DBContext dc)
        {
            List<dcMACHINE_MST> cObjList = new List<dcMACHINE_MST>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetMachineListString());
                if ( _DEPT_ID != "0")
                {
                    sb.Append(" AND mst.DEPT_ID= @DEPT_ID ");
                    cmdInfo.DBParametersInfo.Add("@DEPT_ID", _DEPT_ID);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcMACHINE_MST>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcMACHINE_MST> GetASMMachineList(int refMachineId, DBContext dc)
        {
            List<dcMACHINE_MST> cObjList = new List<dcMACHINE_MST>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetASMMachineListString());
                if (refMachineId > 0 )
                {
                    sb.Append(" AND MACHINE_OPERATOR_CONF.REF_MACHINE_ID= @refMachineId ");
                    cmdInfo.DBParametersInfo.Add("@refMachineId", refMachineId);
                }

                sb.Append(" ORDER BY MACHINE_OPERATOR_CONF.MACHINE_ID ");
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcMACHINE_MST>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }


        public static List<dcMACHINE_MST> GetMachineListBYDeptID(string pDeptID)
        {
            return GetMachineListBYDeptID(pDeptID, null);
        }

        public static DataLoadOptions MACHINE_MSTLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcMACHINE_MST>(obj => obj.relatedclassname);
            return dlo;
        }

        public static List<dcMACHINE_MST> GetMachineList(int Dept_ID)
        {
            return GetMachineList(null, Dept_ID,null);
        }
        //public static List<dcMACHINE_MST> GetMachineList(DBContext dc)
        //{
        //    return GetMachineList(null, dc);
        //}
        public static List<dcMACHINE_MST> GetMachineList(DBQuery dbq, int Dept_ID,DBContext dc)
        {

            List<dcMACHINE_MST> cObjList = new List<dcMACHINE_MST>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder(GetMachineListString());
                    if (Dept_ID != 0)
                    {
                        sb.Append(" AND mst.DEPT_ID= @DEPT_ID ");
                        cmdInfo.DBParametersInfo.Add("@DEPT_ID", Dept_ID);
                    }
                    dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcMACHINE_MST>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;


        }
        public static List<dcMACHINE_MST> GetMACHINE_MSTList()
        {
            return GetMACHINE_MSTList(null, null);
        }
        public static List<dcMACHINE_MST> GetMACHINE_MSTList(DBContext dc)
        {
            return GetMACHINE_MSTList(null, dc);
        }
        public static List<dcMACHINE_MST> GetMACHINE_MSTList(DBQuery dbq, DBContext dc)
        {
            List<dcMACHINE_MST> cObjList = new List<dcMACHINE_MST>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcMACHINE_MST>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcMACHINE_MST GetMACHINE_MSTByID(int pMACHINE_MSTID)
        {
            return GetMACHINE_MSTByID(pMACHINE_MSTID, null);
        }
        public static dcMACHINE_MST GetMACHINE_MSTByID(int pMACHINE_MSTID, DBContext dc)
        {
            dcMACHINE_MST cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcMACHINE_MST>()
                                  where c.MACHINE_ID == pMACHINE_MSTID
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

        public static int Insert(dcMACHINE_MST cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcMACHINE_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcMACHINE_MST>(cObj, true);
                if (id > 0) { cObj.MACHINE_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcMACHINE_MST cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcMACHINE_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcMACHINE_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pMACHINE_MSTID)
        {
            return Delete(pMACHINE_MSTID, null);
        }
        public static bool Delete(int pMACHINE_MSTID, DBContext dc)
        {
            dcMACHINE_MST cObj = new dcMACHINE_MST();
            cObj.MACHINE_ID = pMACHINE_MSTID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcMACHINE_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcMACHINE_MST cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcMACHINE_MST cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcMACHINE_MST cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcMACHINE_MST cObj, DBContext dc)
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
                                newID = cObj.MACHINE_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.MACHINE_ID, dc))
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

        public static bool SaveList(List<dcMACHINE_MST> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcMACHINE_MST> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcMACHINE_MST oDet in detList)
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
                        bool d = Delete(oDet.MACHINE_ID, dc);
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
