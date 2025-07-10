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
using PG.Core.Utility;
using PG.DBClass.AccountingDC;
using PG.DBClass.AccountingDC.AccEnums;
using PG.DBClass.AccountingDC.GeneralLedgerDC;

namespace PG.BLLibrary.AccountingBL.GeneralLedgerBL
{
    public class JournalBL
    {
        //public static DataLoadOptions JournalLoadOptions()
        //{
        //    DataLoadOptions dlo = new DataLoadOptions();
        //    //dlo.LoadWith<DBClass.PayRoll.SalaryPeriod>(obj => obj.PayRoll.SalaryPeriodType);
        //    //dlo.LoadWith<DBClass.dcPurchase>(obj => obj.Supplier);
        //    //dlo.LoadWith<DBClass.dcPurchase>(obj => obj.TranType);
        //    return dlo;
        //}

        public static string GetJournalListString()
        {

            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT tblJournal.* ");
            sb.Append(",tblJournalType.JournalTypeCode , tblJournalType.JournalTypeName ");
            sb.Append(",tblAccYear.AccYearName,tblLocation.LocationName ");
       
            sb.Append(" FROM tblJournal ");
            sb.Append(" INNER JOIN tblJournalType ON tblJournal.JournalTypeID = tblJournalType.JournalTypeID ");
            sb.Append(" INNER JOIN tblAccYear ON tblJournal.AccYearID = tblAccYear.AccYearID ");
            sb.Append(" LEFT JOIN tblLocation ON TBLJOURNAL.LOCATIONID=TBLLOCATION.LOCATIONID ");
            sb.Append(" WHERE (1=1) ");


            return sb.ToString();
        }

        public static List<dcJournal> GetJournalList(int pCompanyID, int pAccYearID)
        {
            return GetJournalList(pCompanyID, pAccYearID, 0, 0, null);
        }

        public static List<dcJournal> GetJournalList(int pCompanyID, int pAccYearID, DBContext dc)
        {
            return GetJournalList(pCompanyID, pAccYearID, 0, 0, dc);
        }


        public static List<dcJournal> GetJournalList(int pCompanyID, int pAccYearID, int pJournalTypeID, int pPostOption)
        {
            return GetJournalList(pCompanyID, pAccYearID, pJournalTypeID, pPostOption, null);
        }

        public static List<dcJournal> GetJournalList(int pCompanyID, int pAccYearID, int pJournalTypeID, int pPostOption, DBContext dc)
        {

            DBCommandInfo cmdInfo = new DBCommandInfo();
            //SqlCommand cmd = new SqlCommand();
            StringBuilder sb = new StringBuilder(GetJournalListString());

            sb.Append(" AND tblJournal.CompanyID = @companyID");
            //cmd.Parameters.AddWithValue("@companyID", pCompanyID);
            cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);

            if (pAccYearID > 0)
            {
                sb.Append(" AND tblJournal.AccYearID = @accYearID");
                //cmd.Parameters.AddWithValue("@accYearID", pAccYearID);
                cmdInfo.DBParametersInfo.Add("@accYearID", pAccYearID);
            }


            if (pJournalTypeID > 0)
            {
                sb.Append(" AND tblJournal.JournalTypeID = @journalTypeID");
                //cmd.Parameters.AddWithValue("@journalTypeID", pJournalTypeID);
                cmdInfo.DBParametersInfo.Add("@journalTypeID", pJournalTypeID);
            }

            if (pPostOption > 0)
            {
                if (pPostOption == 1)
                {
                    sb.Append(" AND tblJournal.IsPosted = @isPosted");
                    //cmd.Parameters.AddWithValue("@isPosted", true);
                    cmdInfo.DBParametersInfo.Add("@isPosted", true);
                }

                if (pPostOption == 2)
                {
                    sb.Append(" AND tblJournal.IsPosted = @isPosted");
                    //cmd.Parameters.AddWithValue("@isPosted", false);
                    cmdInfo.DBParametersInfo.Add("@isPosted", false);
                }
            }



            sb.Append(" ORDER BY tblJournal.JournalDate, tblJournal.JournalNo ");


            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = sb.ToString();

            //DBQuery dbq = new DBQuery();
            //dbq.DBQueryMode = DBQueryModeEnum.DBCommand;
            //dbq.DBCommand = cmd;
            DBQuery dbq = new DBQuery();
            dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
            cmdInfo.CommandText = sb.ToString();
            cmdInfo.CommandType = CommandType.Text;
            dbq.DBCommandInfo = cmdInfo;


            return GetJournalList(dbq, dc);

            //List<dcJournal> cObjList = new List<dcJournal>();
            //bool isDCInit = false;
            //try
            //{
            //    isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            //    using (DataContext dataContext = dc.NewDataContext())
            //    {
            //        cObjList = (from c in dataContext.GetTable<dcJournal>()
            //                     select c).ToList();
            //    }
            //}
            //catch { throw; }
            //finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            //return cObjList;
        }

        public static List<dcJournal> GetJournalList(DBQuery dbq, DBContext dc)
        {
            List<dcJournal> cObjList = new List<dcJournal>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                if (dbq == null)
                {
                    dbq = new DBQuery();
                    //dbq.OrderBy = "PeriodStartDate";
                }
                cObjList = DBQuery.ExecuteDBQuery<dcJournal>(dbq, dc);
                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    if (dbq == null)
                //    {
                //        dbq = new DBQuery();
                //        //dbq.OrderBy = "PeriodStartDate";
                //    }
                //    cObjList = DBQuery.ExecuteDBQuery<dcJournal>(dbq, dc);
                //}
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }


        public static dcJournal GetJournalByID(int pCompanyID, int pJournalID,int pLocationID)
        {
            return GetJournalByID(pCompanyID, pJournalID,pLocationID, null);
        }
        public static dcJournal GetJournalByID(int pCompanyID,int pJournalID,int pLocationID, DBContext dc)
        {

            DBCommandInfo cmdInfo = new DBCommandInfo();
            //SqlCommand cmd = new SqlCommand();
            StringBuilder sb = new StringBuilder(GetJournalListString());

            sb.Append(" AND tblJournal.CompanyID = @companyID");
            //cmd.Parameters.AddWithValue("@companyID", pCompanyID);
            cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);


            sb.Append(" AND tblJournal.JournalID = @journalID");
            //cmd.Parameters.AddWithValue("@journalID", pJournalID);
            cmdInfo.DBParametersInfo.Add("@journalID", pJournalID);

            if (pLocationID > 0)
            {
                sb.Append(" AND tblJournal.LocationID = @LocationID");
                cmdInfo.DBParametersInfo.Add("@LocationID", pLocationID);
            }

            //if (pJournalTypeID > 0)
            //{
            //    sb.Append(" AND tblJournal.JournalTypeID = @journalTypeID");
            //    cmd.Parameters.AddWithValue("@journalTypeID", pJournalTypeID);
            //}


            //sb.Append(" ORDER BY tblJournal.JournalNo ");

            DBQuery dbq = new DBQuery();
            dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
            cmdInfo.CommandText = sb.ToString();
            cmdInfo.CommandType = CommandType.Text;
            dbq.DBCommandInfo = cmdInfo;
            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = sb.ToString();

            //DBQuery dbq = new DBQuery();
            //dbq.DBQueryMode = DBQueryModeEnum.DBCommand;
            //dbq.DBCommand = cmd;

            dcJournal cObj = GetJournalList(dbq, dc).FirstOrDefault();

            return cObj;

        }



        public static dcJournal GetJournalByNo(int pCompanyID, int pYearID, string pJournalNo)
        {
            return GetJournalByNo(pCompanyID, pYearID, pJournalNo, 0, null);
        }

        public static dcJournal GetJournalByNo(int pCompanyID, int pYearID, string pJournalNo, DBContext dc)
        {
            return GetJournalByNo(pCompanyID, pYearID, pJournalNo, 0, dc);
        }

        public static dcJournal GetJournalByNo(int pCompanyID, int pYearID, string pJournalNo, int pJournalTypeID)
        {
            return GetJournalByNo(pCompanyID, pYearID, pJournalNo, pJournalTypeID, null);
        }

        public static dcJournal GetJournalByNo(int pCompanyID, int pYearID, string pJournalNo, int pJournalTypeID, DBContext dc)
        {

            DBCommandInfo cmdInfo = new DBCommandInfo();
            //SqlCommand cmd = new SqlCommand();
            StringBuilder sb = new StringBuilder(GetJournalListString());

            sb.Append(" AND tblJournal.CompanyID = @companyID");
            //cmd.Parameters.AddWithValue("@companyID", pCompanyID);
            cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);

            sb.Append(" AND tblJournal.AccYearID = @yearID");
            //cmd.Parameters.AddWithValue("@yearID", pYearID);
            cmdInfo.DBParametersInfo.Add("@yearID", pYearID);

            sb.Append(" AND tblJournal.JournalNo = @journalNo");
            //cmd.Parameters.AddWithValue("@journalNo", pJournalNo);
            cmdInfo.DBParametersInfo.Add("@journalNo", pJournalNo);


            if (pJournalTypeID > 0)
            {
                sb.Append(" AND tblJournal.JournalTypeID = @journalTypeID");
                //cmd.Parameters.AddWithValue("@journalTypeID", pJournalTypeID);
                cmdInfo.DBParametersInfo.Add("@journalTypeID", pJournalTypeID);
            }

            sb.Append(" ORDER BY tblJournal.JournalNo ");

            DBQuery dbq = new DBQuery();
            dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
            cmdInfo.CommandText = sb.ToString();
            cmdInfo.CommandType = CommandType.Text;
            dbq.DBCommandInfo = cmdInfo;
            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = sb.ToString();

            //DBQuery dbq = new DBQuery();
            //dbq.DBQueryMode = DBQueryModeEnum.DBCommand;
            //dbq.DBCommand = cmd;

            dcJournal cObj = GetJournalList(dbq, dc).FirstOrDefault();
            return cObj;




        }

        public static void SetDebitCreditCount(dcJournal jrnl, DBContext dc)
        {
//            StringBuilder sb = new StringBuilder();

//            sb.Append(" SELECT SUM(CASE WHEN DrCr = 0 THEN 1 ELSE 0 END) AS DebitCount, SUM(CASE WHEN DrCr = 1 THEN 1 ELSE 0 END) AS CreditCount ");
//            sb.Append(" FROM Accounting.tblJournalDet ");


//            sb.Append(" AND tblJournal.CompanyID = @companyID");
//            cmd.Parameters.AddWithValue("@companyID", pCompanyID);


//           string strSql ="   SELECT     SUM(CASE WHEN DrCr = 0 THEN 1 ELSE 0 END) AS DebitCount, SUM(CASE WHEN DrCr = 1 THEN 1 ELSE 0 END) AS CreditCount
//FROM         Accounting.tblJournalDet
//WHERE     (JournalID = 33)
//GROUP BY JournalID
        }


        public static bool IsJournalNoExists(int pCompanyID, int pAccYearID, string pJournalNo)
        {
            return IsJournalNoExists(pCompanyID, pAccYearID, pJournalNo, null);
        }
        public static bool IsJournalNoExists(int pCompanyID, int pAccYearID, string pJournalNo, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetJournalListString());
                sb.Append(" AND tblJournal.CompanyID=@companyID AND  tblJournal.AccYearID=@accYearID AND tblJournal.JournalNo=@journalNo");
                cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);
                cmdInfo.DBParametersInfo.Add("@accYearID", pAccYearID);
                cmdInfo.DBParametersInfo.Add("@journalNo", pJournalNo);


                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                int tcount = Convert.ToInt32(DBQuery.ExecuteDBScalar(dbq));
                if (tcount > 0)
                {
                    isData = true;
                }
            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return isData;
        }

        public static bool IsJournalNoExists(int pCompanyID, int pAccYearID, string pJournalNo, int pJournalID)
        {
            return IsJournalNoExists(pCompanyID, pAccYearID, pJournalNo, pJournalID, null);
        }
        public static bool IsJournalNoExists(int pCompanyID, int pAccYearID, string pJournalNo, int pJournalID , DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetJournalListString());
                
                sb.Append(" AND tblJournal.CompanyID=@companyID ");
                cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);

                sb.Append(" AND  tblJournal.AccYearID=@accYearID ");
                cmdInfo.DBParametersInfo.Add("@accYearID", pAccYearID);

                sb.Append(" AND tblJournal.JournalNo=@journalNo");
                cmdInfo.DBParametersInfo.Add("@journalNo", pJournalNo);

                sb.Append(" AND tblJournal.JournalID<>@journalID");
                cmdInfo.DBParametersInfo.Add("@journalID", pJournalID);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                int tcount = Convert.ToInt32(DBQuery.ExecuteDBScalar(dbq));
                if (tcount > 0)
                {
                    isData = true;
                }
            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return isData;
        }

        public static bool IsJournalYearValid(int pCompanyID, int pAccYearID)
        {
            return IsJournalYearValid(pCompanyID, pAccYearID, null);
        }
        public static bool IsJournalYearValid(int pCompanyID, int pAccYearID, DBContext dc)
        {
            bool isValid = false;
            dcAccYear year = AccYearBL.GetAccYearByID(pCompanyID, pAccYearID, dc);
            if (year != null)
            {
                if (!year.IsClosed)
                {
                    isValid = true;
                }
            }
            return isValid;
        }

        public static bool IsJournalDateValid(int pCompanyID, int pAccYearID, DateTime pJournalDate)
        {
            return IsJournalDateValid(pCompanyID, pAccYearID, pJournalDate, null);
        }
        public static bool IsJournalDateValid(int pCompanyID, int pAccYearID, DateTime pJournalDate, DBContext dc)
        {
            bool isValid = false;
            dcAccYear year = AccYearBL.GetAccYearByID(pCompanyID, pAccYearID, dc);
            if (year != null)
            {
                if (pJournalDate >= year.YearStartDate && pJournalDate <= year.YearEndDate)
                {
                    isValid = true;
                }

            }

            

            return isValid;
        }


        public static int GetJournalSLNoSysMax(int pCompanyID, int pYearID, int pJournalTypeID ,DBContext dc)
        {
            int maxNO = 0;

            DBCommandInfo cmdInfo = new DBCommandInfo();
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT MAX(tblJournal.JournalSLNoSys) AS JournalSLNoSys ");

            sb.Append(" FROM tblJournal ");

            sb.Append(" WHERE (1=1) ");

            sb.Append(" AND tblJournal.CompanyID = @companyID");
            //cmd.Parameters.AddWithValue("@companyID", pCompanyID);
            cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);

            sb.Append(" AND tblJournal.AccYearID = @yearID");
            //cmd.Parameters.AddWithValue("@yearID", pYearID);
            cmdInfo.DBParametersInfo.Add("@yearID", pYearID);

            if (pJournalTypeID > 0)
            {
                sb.Append(" AND tblJournal.JournalTypeID = @journalTypeID");
                //cmd.Parameters.AddWithValue("@journalTypeID", pJournalTypeID);
                cmdInfo.DBParametersInfo.Add("@journalTypeID", pJournalTypeID);
            }

            DBQuery dbq = new DBQuery();
            dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
            cmdInfo.CommandText = sb.ToString();
            cmdInfo.CommandType = CommandType.Text;
            dbq.DBCommandInfo = cmdInfo;
            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = sb.ToString();

            //DBQuery dbq = new DBQuery();
            //dbq.DBQueryMode = DBQueryModeEnum.DBCommand;
            //dbq.DBCommand = cmd;

            maxNO = Conversion.DBNullIntToZero(dc.ExecuteScalar(cmdInfo));
            return maxNO;

        }


        public static int Insert(dcJournal cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcJournal cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcJournal>(cObj, true);
                if (id > 0) { cObj.JournalID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcJournal cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcJournal cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcJournal>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pJournalID)
        {
            return Delete(pJournalID, null);
        }
        public static bool Delete(int pJournalID, DBContext dc)
        {
            dcJournal cObj = new dcJournal();
            cObj.JournalID = pJournalID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcJournal>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool PostJournal(int pJournalID, DateTime pPostDate)
        {
            return PostJournal(pJournalID, pPostDate, null);
        }
        public static bool PostJournal(int pJournalID, DateTime pPostDate, DBContext dc)
        {
            bool bStatus = false;

            dcJournal jrnl = new dcJournal();
            jrnl.JournalID = pJournalID;
            jrnl.IsPosted = true;
            jrnl.PostedDate = pPostDate;

            bStatus = Update(jrnl,dc);

            return bStatus;
        }



        public static int Save(dcJournal cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }
        public static int Save(dcJournal cObj, bool isAdd, DBContext dc)
        {
            int newID = 0;
            bool isDCInit = false;
            bool isTransInit = false;
            bool bStatus = false;

            if (JournalDetBL.IsDebitCreditBalanced(cObj.JournalDetList) == false)
            {
                ////if (dc.IsTransaction)
                ////{
                ////    dc.RollbackTransaction();
                ////}
                throw new ArgumentException("Debit Credit not balanced");
            }

            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                isTransInit = dc.StartTransaction();
                
                dcJournalType jType = JournalTypeBL.GetJournalTypeByID(cObj.CompanyID, cObj.JournalTypeID, dc);
                if (isAdd)
                {
                    if (jType.AccSLNoID > 0)
                    {
                        cObj.JournalNo = AccSLNoBL.GetNextAccSLNo(jType.AccSLNoID, true, dc);
                    }
                    cObj.JournalSLNoSys = GetJournalSLNoSysMax(cObj.CompanyID, cObj.AccYearID, 0, dc)  +  1;
                }

                if (cObj.JournalNo.Trim() == string.Empty)
                {
                    dc.RollbackTransaction(isTransInit);
                    DBContextManager.ReleaseDBContext(ref dc, isDCInit);
                    throw new Exception("Journal No must be provided!");
                }


                using (DataContext dataContext = dc.NewDataContext())
                {
                    //cnt = dc.DoDelete<DBClass.PayRoll.dcSalaryDef>(cObj);
                    decimal totAmt = cObj.JournalDetList.Sum(c => c.DebitAmt);
                    cObj.JournalAmt = totAmt;
                                       

                    if (isAdd)
                    {
                        newID = Insert(cObj, dc);
                    }
                    else
                    {
                        if (Update(cObj, dc))
                        {
                            newID = cObj.JournalID;
                        }
                    }
                    //save details
                    if (newID > 0)
                    {

                        if (cObj.JournalDetList != null)
                        {
                            foreach (dcJournalDet det in cObj.JournalDetList)
                            {
                                det.JournalID = newID;
                            }
                            bStatus = JournalDetBL.SaveList(cObj.JournalDetList, dc);

                        }

                        if (bStatus)
                        {
                            dc.CommitTransaction(isTransInit);
                        }
                    }
                }
            }
            //catch
            {
                //dc.RollbackTransaction(isTransInit);
                //throw;
            }
           //finally 
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return newID;
        }

    }
}
