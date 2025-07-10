using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Data.Linq;
//using System.Linq.Dynamic;
using PG.Core.Extentions;
using PG.Core.DBBase;
using PG.Core.DBFilters;
using PG.DBClass.AccountingDC;

namespace PG.BLLibrary.AccountingBL
{
    /// <summary>
    /// AppAppInfoBL
    /// Last update By Moni, Date 09-03-2015
    /// </summary>
    public class AccYearBL
    {
        public static string GetAccYear_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT tblAccYear.* ");
            sb.Append(" FROM tblAccYear ");
            sb.Append(" WHERE 1=1 ");
            return sb.ToString();
        }

        public static DataLoadOptions AccYearLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcAccYear>(obj => obj.relatedclassname);
            return dlo;
        }
        public static List<dcAccYear> GetAccYearList(int pCompanyID)
        {
            return GetAccYearList(pCompanyID, null);
        }
        public static List<dcAccYear> GetAccYearList(int pCompanyID, DBContext dc)
        {
            List<dcAccYear> cObjList = new List<dcAccYear>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetAccYear_SQLString());
                sb.Append(" AND tblAccYear.CompanyID=@companyID ");
                cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcAccYear>(dbq, dc).ToList();
                
                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    cObjList = (from c in dataContext.GetTable<dcAccYear>()
                //                where c.CompanyID == pCompanyID
                //                orderby c.YearStartDate
                //                select c).ToList();
                //}
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static List<dcAccYear> GetAccYearList(DBQuery dbq, DBContext dc)
        {
            List<dcAccYear> cObjList = new List<dcAccYear>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                if (dbq == null)
                {
                    dbq = new DBQuery();
                    //dbq.OrderBy = "YearStartDate Desc";
                }
                cObjList = DBQuery.ExecuteDBQuery<dcAccYear>(dbq, dc);
                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    if (dbq == null)
                //    {
                //        dbq = new DBQuery();
                //        //dbq.OrderBy = "YearStartDate Desc";
                //    }
                //    cObjList = DBQuery.ExecuteDBQuery<dcAccYear>(dbq, dc);
                //}
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcAccYear GetAccYearByID(int pCompanyID, int pAccYearID)
        {
            return GetAccYearByID(pCompanyID, pAccYearID, null);
        }
        public static dcAccYear GetAccYearByID(int pCompanyID, int pAccYearID, DBContext dc)
        {
            dcAccYear cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetAccYear_SQLString());
                sb.Append(" AND tblAccYear.CompanyID=@companyID AND  tblAccYear.AccYearID=@accYearID");
                cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);
                cmdInfo.DBParametersInfo.Add("@accYearID", pAccYearID);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObj = DBQuery.ExecuteDBQuery<dcAccYear>(dbq, dc).FirstOrDefault();
                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    var result = (from c in dataContext.GetTable<dcAccYear>()
                //                  where  c.CompanyID == pCompanyID && c.AccYearID == pAccYearID
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

        public static int Insert(dcAccYear cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcAccYear cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcAccYear>(cObj, true);
                if (id > 0) { cObj.AccYearID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcAccYear cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcAccYear cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcAccYear>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pAccYearID)
        {
            return Delete(pAccYearID, null);
        }
        public static bool Delete(int pAccYearID, DBContext dc)
        {
            dcAccYear cObj = new dcAccYear();
            cObj.AccYearID = pAccYearID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcAccYear>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcAccYear cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcAccYear cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcAccYear cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcAccYear cObj, DBContext dc)
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
                                newID = cObj.AccYearID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.AccYearID, dc))
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

        public static bool SaveList(List<dcAccYear> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcAccYear> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcAccYear oDet in detList)
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
                        bool d = Delete(oDet.AccYearID, dc);
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

        public static dcAccYear GetCurrentAccYear(DBContext dc)
        {
            return GetCurrentAccYear(1, dc);
        }

        public static dcAccYear GetCurrentAccYear(int pCompanyID)
        {
            return GetCurrentAccYear(pCompanyID, null);
        }

        public static dcAccYear GetCurrentAccYear(int pCompanyID, DBContext dc)
        {
            dcAccYear cObj = null;
            dcAccSettings accSettings = AccSettingsBL.GetAccSettingByCompanyID(pCompanyID, dc);
            if (accSettings != null)
            {
                cObj = GetAccYearByID(pCompanyID, accSettings.AccYearID);
            }
            return cObj;
        }
    }
}
