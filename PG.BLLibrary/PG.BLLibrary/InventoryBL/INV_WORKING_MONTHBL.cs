using PG.Core.DBBase;
using PG.Core.Utility;
using PG.DBClass.InventoryDC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Text;

namespace PG.BLLibrary.InventoryBL
{
   public class INV_WORKING_MONTHBL
    {
       
        public static string GetWorkingMonth_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" Select wm.*,usr.FULLNAME ");
            sb.Append(" FROM ");
            sb.Append(" INV_WORKING_MONTH wm INNER JOIN TBLUSER usr ");
            sb.Append(" on wm.CREATE_BY=usr.USERID ");
            sb.Append(" where 1=1 ");
            return sb.ToString();
        }

        public static List<dcINV_WORKING_MONTH> Working_Month_List()
        {

            DBCommandInfo cmdInfo = new DBCommandInfo();
            StringBuilder sb = new StringBuilder(GetWorkingMonth_SQLString());
            sb.Append(" Order by wm.YEAR desc,wm.MONTH desc ");
            DBQuery dbq = new DBQuery();
            dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
            cmdInfo.CommandText = sb.ToString();
            cmdInfo.CommandType = CommandType.Text;
            dbq.DBCommandInfo = cmdInfo;
            return Working_Month_List(dbq, null);
        }
    
        public static List<dcINV_WORKING_MONTH> Working_Month_List(DBQuery dbq, DBContext dc)
        {
            List<dcINV_WORKING_MONTH> cObjList = new List<dcINV_WORKING_MONTH>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    if (dbq == null)
                    {
                        dbq = new DBQuery();
                       
                    }
                    cObjList = DBQuery.ExecuteDBQuery<dcINV_WORKING_MONTH>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static dcINV_WORKING_MONTH Working_Month_By_Year_And_Month(int year, int mon)
        {
            dcINV_WORKING_MONTH cobj = new dcINV_WORKING_MONTH();        
            try
            {
                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder(GetWorkingMonth_SQLString());
                    sb.Append(" AND wm.YEAR=@year AND wm.MONTH=@month ");
                    cmdInfo.DBParametersInfo.Add("@year",year);
                    cmdInfo.DBParametersInfo.Add("@month", mon);
                    DBQuery dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cobj = Working_Month_List(dbq, null).FirstOrDefault();
            }
            catch { throw; }
            finally { }
            return cobj;
        }


        public static dcINV_WORKING_MONTH Working_Month_By_Id(int id)
        {
            dcINV_WORKING_MONTH cobj = new dcINV_WORKING_MONTH();
          
            try
            {
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetWorkingMonth_SQLString());
                sb.Append(" AND wm.WORKING_MONTH_ID=@id ");
                cmdInfo.DBParametersInfo.Add("@id", id);
            
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                cobj = Working_Month_List(dbq, null).FirstOrDefault();
            }
            catch { throw; }
            finally { }
            return cobj;
        }

        public static string Is_Month_Close_Valid(DateTime? fromDate, DateTime? toDate, DBContext dc)
        {
            bool isDCInit = false;
            string msg = "";
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                string abbr = " SELECT F_IS_MONTH_CLOSING_VALID(@P_FROM_DATE,@P_TO_DATE) A from Dual ";
                cmdInfo.DBParametersInfo.Add("@P_FROM_DATE", fromDate);
                cmdInfo.DBParametersInfo.Add("@P_TO_DATE", toDate);
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                msg = DBQuery.ExecuteDBScalar(dbq, dc).ToString();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return msg;
        }
        public static int Insert(dcINV_WORKING_MONTH cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcINV_WORKING_MONTH cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcINV_WORKING_MONTH>(cObj, true);
                if (id > 0) { cObj.WORKING_MONTH_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcINV_WORKING_MONTH cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcINV_WORKING_MONTH cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;

            dcINV_WORKING_MONTH key = new dcINV_WORKING_MONTH();
            key.WORKING_MONTH_ID = cObj.WORKING_MONTH_ID;

            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcINV_WORKING_MONTH>(cObj, key);
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
            dcINV_WORKING_MONTH cObj = new dcINV_WORKING_MONTH();
            //cObj.ELECTROLYTE_GRAVITYID = pELECTROLYTE_GRAVITYID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcINV_WORKING_MONTH>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcINV_WORKING_MONTH cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcINV_WORKING_MONTH cObj, bool isAdd, DBContext dc)
        {
            
            return Save(cObj, dc);
        }

        public static int Save(dcINV_WORKING_MONTH cObj)
        {
            return Save(cObj, null);
        }




        public static int Save(dcINV_WORKING_MONTH cObj, DBContext dc)
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

        public static bool SaveList(List<dcINV_WORKING_MONTH> detList)
        {
            return SaveList(detList, null);
        }


        public static string Checking_Working_Month(DBContext dc,int year,int month)
        {
            bool isDCInit = false;
            string msg = string.Empty;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                string abbr = " SELECT FN_IS_WORKING_MONTH_VALID(@P_YEAR,@P_MONTH) A from Dual "; 
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.DBParametersInfo.Add("@P_YEAR", year);
                  cmdInfo.DBParametersInfo.Add("@P_MONTH", month);
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                msg = Convert.ToString(DBQuery.ExecuteDBScalar(dbq, dc));
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return msg;
        }
        public static string Is_Working_Date_Within_Declared_Month(DBContext dc,DateTime date)
        {
            int year = date.Year;
            int mon = date.Month;
            bool isDCInit = false;
            string msg =string.Empty;          
            
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                string abbr = " SELECT FN_IS_DATE_WITHIN_WORKING_MON(@P_YEAR,@P_MONTH,@P_TRAN_DATE) A from Dual ";
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.DBParametersInfo.Add("@P_YEAR", year);
                cmdInfo.DBParametersInfo.Add("@P_MONTH", mon);
                cmdInfo.DBParametersInfo.Add("@P_TRAN_DATE",date);
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                msg = Convert.ToString(DBQuery.ExecuteDBScalar(dbq, dc));
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return msg;
        }

        public static bool SaveList(List<dcINV_WORKING_MONTH> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcINV_WORKING_MONTH oDet in detList)
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
