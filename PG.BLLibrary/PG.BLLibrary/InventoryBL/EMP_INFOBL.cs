using PG.Core.DBBase;
using PG.DBClass.InventoryDC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Text;

namespace PG.BLLibrary.InventoryBL
{
   public class EMP_INFOBL
    {

        public static string GetEmp_Info_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" Select * FROM EMP_INFO where 1=1 ");
            return sb.ToString();
        }

        public static string GetEmp_FromHR_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT EMP_ID,EMP_NAME,DESIGNATION,DEPARTMENT,COMPANY_NAME FROM HRISAPP.EMP_INFO_HR WHERE 1=1 and CATEGORY='Management' ");
            return sb.ToString();
        }

        public static List<dcEMP_INFO> Emp_Info_List()
        {
            return Emp_Info_List(null, null, null);
        }
        public static List<dcEMP_INFO> Emp_Info_List(DBContext dc)
        {
            return Emp_Info_List(null, dc, null);
        }
        public static List<dcEMP_INFO> Emp_Info_List(string rackId)
        {
            return Emp_Info_List(null, null, rackId);
        }
        public static List<dcEMP_INFO> Emp_Info_List(DBQuery dbq, DBContext dc, string compId)
        {
            List<dcEMP_INFO> cObjList = new List<dcEMP_INFO>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder(GetEmp_Info_SQLString());                   

                    dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcEMP_INFO>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcEMP_INFO> GetEMP_MASTERList(DBQuery dbq, DBContext dc)
        {
            List<dcEMP_INFO> cObjList = new List<dcEMP_INFO>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcEMP_INFO>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static dcEMP_INFO Get_Emp_Info_By_Id(int id)
        {
            return Get_Emp_Info_By_Id(id, null);
        }
        public static dcEMP_INFO Get_Emp_Info_By_Id(int id, DBContext dc)
        {
            dcEMP_INFO cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcEMP_INFO>()                               
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

        public static int Insert(dcEMP_INFO cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcEMP_INFO cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcEMP_INFO>(cObj, false);
                //if (id > 0) { cObj.ELECTROLYTE_GRAVITYID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcEMP_INFO cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcEMP_INFO cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;

            dcEMP_INFO key = new dcEMP_INFO();
            key.COMPANY_ID = cObj.COMPANY_ID;

            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcEMP_INFO>(cObj, key);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int id)
        {
            return Delete(id, null);
        }
        public static bool Delete(int id, DBContext dc)
        {
            dcEMP_INFO cObj = new dcEMP_INFO();
            //cObj.ELECTROLYTE_GRAVITYID = pELECTROLYTE_GRAVITYID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcEMP_INFO>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcEMP_INFO cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcEMP_INFO cObj, bool isAdd, DBContext dc)
        {
            //cObj._RecordState = isAdd ? Interwave.Core.DBClass.RecordStateEnum.Added : Interwave.Core.DBClass.RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcEMP_INFO cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcEMP_INFO cObj, DBContext dc)
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

        public static bool SaveList(List<dcEMP_INFO> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcEMP_INFO> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcEMP_INFO oDet in detList)
            {
                switch (oDet._RecordState)
                {                    
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
