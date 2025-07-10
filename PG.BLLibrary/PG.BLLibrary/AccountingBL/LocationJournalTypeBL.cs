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
    /// AppMenuBL
    /// Last update By Moni, Date 09-03-2015
    /// </summary>

    public class LocationJournalTypeBL
    {

        public static string GetLocationJournalType_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT tblLocationJournalType.*  ");
            sb.Append(" , tblLocation.LocationCode, tblLocation.LocationName ");
            sb.Append(" , tblJournalType.JournalTypeCode, tblJournalType.JournalTypeName ");
            sb.Append(" FROM tblLocationJournalType ");
            sb.Append(" INNER JOIN tblLocation ON tblLocationJournalType.LocationID = tblLocation.LocationID ");
            sb.Append(" INNER JOIN tblJournalType ON tblLocationJournalType.JournalTypeID = tblJournalType.JournalTypeID ");

            sb.Append(" WHERE 1=1 ");
            return sb.ToString();
        }

        public static List<dcLocationJournalType> GetLocationJournalTypeList(int pCompanyID)
        {
            return GetLocationJournalTypeList(pCompanyID, null);
        }
        public static List<dcLocationJournalType> GetLocationJournalTypeList(int pCompanyID, DBContext dc)
        {
            List<dcLocationJournalType> cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();

                StringBuilder sb = new StringBuilder(GetLocationJournalType_SQLString());

                sb.Append(" AND tblLocation.CompanyID = companyID ");
                cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcLocationJournalType>(dbq, dc).ToList();
                 
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcLocationJournalType> GetLocationJournalTypeListByLocation(int pLocationID)
        {
            return GetLocationJournalTypeListByLocation(pLocationID, null);
        }
        public static List<dcLocationJournalType> GetLocationJournalTypeListByLocation(int pLocationID, DBContext dc)
        {

            List<dcLocationJournalType> cObjList = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetLocationJournalType_SQLString());
                sb.Append(" AND tblLocationJournalType.LocationID = @locationID ");
                cmdInfo.DBParametersInfo.Add("@locationID", pLocationID);
                
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcLocationJournalType>(dbq, dc).ToList();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }


        public static List<dcLocationJournalType> GetJournalLocationTypeList(DBQuery dbq, DBContext dc)
        {
            List<dcLocationJournalType> cObjList = new List<dcLocationJournalType>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                if (dbq == null)
                {
                    dbq = new DBQuery();
                    //dbq.OrderBy = "PeriodStartDate";
                }
                cObjList = DBQuery.ExecuteDBQuery<dcLocationJournalType>(dbq, dc).ToList();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static dcLocationJournalType GetLocationJournalType(int pLocationID, int pJournalTypeID)
        {
            return GetLocationJournalType(pLocationID, pJournalTypeID, null);
        }
        public static dcLocationJournalType GetLocationJournalType(int pLocationID, int pJournalTypeID, DBContext dc)
        {
            dcLocationJournalType cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetLocationJournalType_SQLString());
                
                sb.Append(" AND tblLocationJournalType.LocationID = locationID ");
                cmdInfo.DBParametersInfo.Add("@locationID", pLocationID);

                sb.Append(" AND tblLocationJournalType.JournalTypeID = journalTypeID ");
                cmdInfo.DBParametersInfo.Add("@journalTypeID", pJournalTypeID);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;


                cObj = DBQuery.ExecuteDBQuery<dcLocationJournalType>(dbq, dc).FirstOrDefault();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObj;
        }


        public static void UpdateSLNo(List<dcLocationJournalType> pListDetails)
        {
            int slNo = 0;
            foreach (dcLocationJournalType oDet in pListDetails)
            {
                if (oDet._RecordState != RecordStateEnum.Deleted)
                {
                    slNo++;
                    oDet.LocationJournalTypeSLNo = slNo;
                }
            }
        }


        public static int Insert(dcLocationJournalType cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcLocationJournalType cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcLocationJournalType>(cObj, true);
                if (id > 0) { cObj.LocationJournalTypeID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcLocationJournalType cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcLocationJournalType cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcLocationJournalType>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pLocationJournalTypeID)
        {
            return Delete(pLocationJournalTypeID, null);
        }
        public static bool Delete(int pLocationJournalTypeID, DBContext dc)
        {
            dcLocationJournalType cObj = new dcLocationJournalType();
            cObj.LocationJournalTypeID = pLocationJournalTypeID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcLocationJournalType>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }




        public static bool UpdateLocationJournalTypeList(int pLocationID, List<dcLocationJournalType> pLocationJournalTypeList)
        {
            return UpdateLocationJournalTypeList(pLocationID, pLocationJournalTypeList, null);
        }

        public static bool UpdateLocationJournalTypeList(int pLocationID, List<dcLocationJournalType> pLocationJournalTypeList, DBContext dc)
        {
            bool isDCInit = false;
            bool isTransInit = false;
            bool bStatus = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                List<dcLocationJournalType> curLocJrnlTypeList = GetLocationJournalTypeListByLocation(pLocationID, dc);

                isTransInit = dc.StartTransaction();

                foreach (dcLocationJournalType locJrnlType in pLocationJournalTypeList)
                {

                    dcLocationJournalType curLocJrnlType = curLocJrnlTypeList.SingleOrDefault(c => c.LocationID == locJrnlType.LocationID
                                                                                   && c.JournalTypeID == locJrnlType.JournalTypeID);

                    dcLocationJournalType cObj = new dcLocationJournalType();

                    cObj.LocationID = locJrnlType.LocationID;
                    cObj.JournalTypeID = locJrnlType.JournalTypeID;
                    cObj.JournalNoPrefix = locJrnlType.JournalNoPrefix;
                    cObj.LocationJournalTypeSLNo = locJrnlType.LocationJournalTypeSLNo;


                    if (curLocJrnlType == null)
                    {
                        cObj.LocationJournalTypeID = dc.DoInsert<dcLocationJournalType>(cObj, true);
                        curLocJrnlTypeList.Add(cObj);
                    }
                    else
                    {
                        if (locJrnlType._RecordState == RecordStateEnum.Deleted)
                        {
                            //TODO: need to reset SLNO
                            Delete(curLocJrnlType.LocationJournalTypeID, dc);
                            curLocJrnlTypeList.Remove(curLocJrnlType);
                        }
                        else
                        {
                            cObj.LocationJournalTypeID = curLocJrnlType.LocationJournalTypeID;
                            dc.DoUpdate<dcLocationJournalType>(cObj);
                        }
                    }
                }
                dc.CommitTransaction(isTransInit);
                bStatus = true;
            }
            //catch { throw; }
            //finally 
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return bStatus;
        }

    }
}
