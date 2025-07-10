using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Data.Linq;
//using System.Linq.Dynamic;
using PG.Core.DBBase;
using PG.Core.Extentions;
using PG.Core.DBFilters;
using PG.DBClass;
using PG.DBClass.AccountingDC;
using PG.DBClass.AccountingDC.AccEnums;
using PG.DBClass.AccountingDC.GeneralLedgerDC;

namespace PG.BLLibrary.AccountingBL.GeneralLedgerBL
{
    /// <summary>
    /// AccRefCategoryBL
    /// Last update By Moni, Date 10-03-2015
    /// </summary>
    public class AccRefCategoryBL
    {
       

        public static string GetAccRefCategoryListString()
        {
            StringBuilder sb = new StringBuilder();
            //TODO Change this query as
            sb.Append("SELECT tblAccRefCategory.* ");
            sb.Append(", tblAccRefType.AccRefTypeCode, tblAccRefType.AccRefTypeName ");
           // sb.Append(", tblAccRefCategory_1.AccRefCategoryCode AS AccRefCategoryCodeParent, tblAccRefCategory_1.AccRefCategoryName AS AccRefCategoryNameParent ");
            sb.Append(" FROM tblAccRefCategory ");
            sb.Append(" INNER JOIN tblAccRefType ON tblAccRefCategory.AccRefTypeID = tblAccRefType.AccRefTypeID ");
           // sb.Append(" LEFT OUTER JOIN tblAccRefCategory  tblAccRefCategory_1 ON tblAccRefCategory.AccRefCategoryID = tblAccRefCategory_1.AccRefCategoryID ");
            sb.Append(" WHERE 1=1 ");

            return sb.ToString();
        }

        public static List<dcAccRefCategory> GetAccRefCategoryList(int pCompanyID)
        {
            return GetAccRefCategoryList(pCompanyID, null);
        }
        public static List<dcAccRefCategory> GetAccRefCategoryList(int pCompanyID,  DBContext dc)
        {
            //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            DBCommandInfo cmdInfo = new DBCommandInfo();
            StringBuilder sb = new StringBuilder(GetAccRefCategoryListString());


            if (pCompanyID > 0)
            {
                sb.Append(" AND tblAccRefCategory.CompanyID=@companyID ");
                //cmd.Parameters.AddWithValue("@companyID", pCompanyID);
                cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);
            }

            sb.Append(" ORDER BY tblAccRefCategory.AccRefCategoryName ");

            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = sb.ToString();

            //PG.Core.DBBase.DBQuery dbq = new PG.Core.DBBase.DBQuery();
            //dbq.DBQueryMode = PG.Core.DBBase.DBQueryModeEnum.DBCommand;
            //dbq.DBCommand = cmd;

            DBQuery dbq = new DBQuery();
            dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
            cmdInfo.CommandText = sb.ToString();
            cmdInfo.CommandType = CommandType.Text;
            dbq.DBCommandInfo = cmdInfo;

            return GetAccRefCategoryList(dbq, dc);


        }
        public static List<dcAccRefCategory> GetAccRefCategoryList(int pCompanyID, int pAccRefTypeID)
        {
            return GetAccRefCategoryList(pCompanyID, pAccRefTypeID, null);
        }
        public static List<dcAccRefCategory> GetAccRefCategoryList(int pCompanyID, int pAccRefTypeID, DBContext dc)
        {
            //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            DBCommandInfo cmdInfo = new DBCommandInfo();
            StringBuilder sb = new StringBuilder(GetAccRefCategoryListString());


            if (pCompanyID > 0)
            {
                sb.Append(" AND tblAccRefCategory.CompanyID=@companyID ");
                //cmd.Parameters.AddWithValue("@companyID", pCompanyID);
                cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);
            }


            if (pAccRefTypeID > 0)
            {
                sb.Append(" AND tblAccRefCategory.AccRefTypeID=@pAccRefTypeID ");
                //cmd.Parameters.AddWithValue("@pAccRefTypeID", pAccRefTypeID);
                cmdInfo.DBParametersInfo.Add("@pAccRefTypeID", pAccRefTypeID);

            }

            sb.Append(" ORDER BY tblAccRefCategory.AccRefCategoryName ");

            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = sb.ToString();

            //PG.Core.DBBase.DBQuery dbq = new PG.Core.DBBase.DBQuery();
            //dbq.DBQueryMode = PG.Core.DBBase.DBQueryModeEnum.DBCommand;
            //dbq.DBCommand = cmd;

            DBQuery dbq = new DBQuery();
            dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
            cmdInfo.CommandText = sb.ToString();
            cmdInfo.CommandType = CommandType.Text;
            dbq.DBCommandInfo = cmdInfo;

            return GetAccRefCategoryList(dbq, dc);

         
        }

        public static List<dcAccRefCategory> GetAccRefCategoryList(DBQuery dbq, DBContext dc)
        {
            List<dcAccRefCategory> cObjList = new List<dcAccRefCategory>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                if (dbq == null)
                {
                    dbq = new DBQuery();
                    //dbq.OrderBy = "PeriodStartDate";
                }
                cObjList = DBQuery.ExecuteDBQuery<dcAccRefCategory>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static dcAccRefCategory GetAccRefCategoryByID(int pAccRefCategoryID)
        {
            return GetAccRefCategoryByID(pAccRefCategoryID, null);
        }
        public static dcAccRefCategory GetAccRefCategoryByID(int pAccRefCategoryID, DBContext dc)
        {
            dcAccRefCategory cObj = null;
            //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            DBCommandInfo cmdInfo = new DBCommandInfo();
            StringBuilder sb = new StringBuilder(GetAccRefCategoryListString());


            if (pAccRefCategoryID > 0)
            {
                sb.Append(" AND tblAccRefCategory.AccRefCategoryID=@accRefCategoryID ");
                //cmd.Parameters.AddWithValue("@accRefCategoryID", pAccRefCategoryID);
                cmdInfo.DBParametersInfo.Add("@accRefCategoryID", pAccRefCategoryID);
            }

            DBQuery dbq = new DBQuery();
            dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
            cmdInfo.CommandText = sb.ToString();
            cmdInfo.CommandType = CommandType.Text;
            dbq.DBCommandInfo = cmdInfo;

            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = sb.ToString();

            //PG.Core.DBBase.DBQuery dbq = new PG.Core.DBBase.DBQuery();
            //dbq.DBQueryMode = PG.Core.DBBase.DBQueryModeEnum.DBCommand;
            //dbq.DBCommand = cmd;
            cObj = GetAccRefCategoryList(dbq, dc).FirstOrDefault();

           
            return cObj;
        }

        public static int Insert(dcAccRefCategory cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcAccRefCategory cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcAccRefCategory>(cObj, true);
                if (id > 0) { cObj.AccRefCategoryID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcAccRefCategory cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcAccRefCategory cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcAccRefCategory>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pAccRefCategoryID)
        {
            return Delete(pAccRefCategoryID, null);
        }
        public static bool Delete(int pAccRefCategoryID, DBContext dc)
        {
            dcAccRefCategory cObj = new dcAccRefCategory();
            cObj.AccRefCategoryID = pAccRefCategoryID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcAccRefCategory>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }


        public static bool IsAccRefCategoryNameExists(int pCompanyID, string pAccRefCategoryCode, int pAccRefTypeID)
        {
            return IsAccRefCategoryNameExists(pCompanyID,pAccRefCategoryCode, pAccRefTypeID, null);
        }
        public static bool IsAccRefCategoryNameExists(int pCompanyID, string pAccRefCategoryCode, int pAccRefTypeID, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetAccRefCategoryListString());

                sb.Append(" AND tblAccRefCategory.AccRefCategoryCode=@accRefCategoryCode ");
                //cmd.Parameters.AddWithValue("@accRefCategoryCode", pAccRefCategoryCode);
                cmdInfo.DBParametersInfo.Add("@accRefCategoryCode",pAccRefCategoryCode);

                sb.Append(" AND tblAccRefCategory.AccRefTypeID=@accRefTypeID ");
                //cmd.Parameters.AddWithValue("@accRefTypeID", pAccRefTypeID);
                cmdInfo.DBParametersInfo.Add("@accRefTypeID",pAccRefTypeID);

                sb.Append(" AND tblAccRefCategory.CompanyID=@companyID ");
                //cmd.Parameters.AddWithValue("@companyID", pCompanyID);
                cmdInfo.DBParametersInfo.Add("@companyID",pCompanyID);


                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                //cmd.CommandType = CommandType.Text;
                //cmd.CommandText = sb.ToString();

                //PG.Core.DBBase.DBQuery dbq = new PG.Core.DBBase.DBQuery();
                //dbq.DBQueryMode = PG.Core.DBBase.DBQueryModeEnum.DBCommand;
                //dbq.DBCommand = cmd;
                isData = GetAccRefCategoryList(dbq, dc).Count > 0;

            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return isData;
        }
        public static bool IsAccRefCategoryNameExists(int pCompanyID, string pAccRefCategoryCode, int pAccRefTypeID, int pAccRefCategoryID)
        {
            return IsAccRefCategoryNameExists(pCompanyID,pAccRefCategoryCode, pAccRefTypeID, pAccRefCategoryID, null);
        }
        public static bool IsAccRefCategoryNameExists(int pCompanyID, string pAccRefCategoryCode, int pAccRefTypeID, int pAccRefCategoryID, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetAccRefCategoryListString());

                sb.Append(" AND tblAccRefCategory.AccRefCategoryCode=@accRefCategoryCode ");
               // cmd.Parameters.AddWithValue("@accRefCategoryCode", pAccRefCategoryCode);
                cmdInfo.DBParametersInfo.Add("@accRefCategoryCode", pAccRefCategoryCode);

                sb.Append(" AND tblAccRefCategory.AccRefTypeID=@accRefTypeID ");
                //cmd.Parameters.AddWithValue("@accRefTypeID", pAccRefTypeID);
                cmdInfo.DBParametersInfo.Add("@accRefTypeID", pAccRefTypeID);

                sb.Append(" AND tblAccRefCategory.CompanyID=@companyID ");
                //cmd.Parameters.AddWithValue("@companyID", pCompanyID);
                cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);

                sb.Append(" AND tblAccRefCategory.AccRefCategoryID <> @accRefCategoryID ");
                //cmd.Parameters.AddWithValue("@accRefCategoryID", pAccRefCategoryID);
                cmdInfo.DBParametersInfo.Add("@accRefCategoryID", pAccRefCategoryID);

                //cmd.CommandType = CommandType.Text;
                //cmd.CommandText = sb.ToString();

                //PG.Core.DBBase.DBQuery dbq = new PG.Core.DBBase.DBQuery();
                //dbq.DBQueryMode = PG.Core.DBBase.DBQueryModeEnum.DBCommand;
                //dbq.DBCommand = cmd;
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                isData = GetAccRefCategoryList(dbq, dc).Count > 0;
            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return isData;
        }




    }
}
