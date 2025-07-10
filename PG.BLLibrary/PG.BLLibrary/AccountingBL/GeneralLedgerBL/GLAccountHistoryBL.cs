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
using PG.DBClass.AccountingDC;
using PG.DBClass.AccountingDC.AccEnums;
using PG.DBClass.AccountingDC.GeneralLedgerDC;

namespace PG.BLLibrary.AccountingBL.GeneralLedgerBL
{
    /// <summary>
    /// AppAppInfoBL
    /// Last update By Moni, Date 10-03-2015
    /// </summary>
    public class GLAccountHistoryBL
    {

        public static string GLAccountHistoryList_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT tblGLAccountHistory.* ");
            sb.Append(" FROM tblGLAccountHistory ");
            sb.Append(" WHERE 1=1 ");
            return sb.ToString();
        }

        //public static DataLoadOptions GLAccountHistoryLoadOptions()
        //{
        //    DataLoadOptions dlo = new DataLoadOptions();
        //    //dlo.LoadWith<DBClass.PayRoll.SalaryPeriod>(obj => obj.PayRoll.SalaryPeriodType);
        //    //dlo.LoadWith<DBClass.dcPurchase>(obj => obj.Supplier);
        //    //dlo.LoadWith<DBClass.dcPurchase>(obj => obj.TranType);
        //    return dlo;
        //}
      
        public static List<dcGLAccountHistory> GetGLAccountHistoryList(DBQuery dbq, DBContext dc)
        {
            List<dcGLAccountHistory> cObjList = new List<dcGLAccountHistory>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    if (dbq == null)
                    {
                        dbq = new DBQuery();
                        //dbq.OrderBy = "PeriodStartDate";
                    }
                    cObjList = DBQuery.ExecuteDBQuery<dcGLAccountHistory>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcGLAccountHistory> GetGLAccountHistoryList(int pCompanyID, int pAccYearID, int pGLAccountID, List<dcGLGroup> pGLGroupList)
        {
            return GetGLAccountHistoryList(pCompanyID, pAccYearID, pGLAccountID, GLAccountTypeFilterEnum.AllAccount, pGLGroupList, null);
        }
        public static List<dcGLAccountHistory> GetGLAccountHistoryList(int pCompanyID, int pAccYearID, int pGLAccountID, GLAccountTypeFilterEnum pGLAccountTypeFilter, List<dcGLGroup> pGLGroupList, DBContext dc)
        {

            List<dcGLAccountHistory> cObjList = new List<dcGLAccountHistory>();
            switch (pGLAccountTypeFilter)
            {
                case GLAccountTypeFilterEnum.NoFilter:
                case GLAccountTypeFilterEnum.AllAccount:
                    cObjList = GetGLAccountHistoryList_ALL(pCompanyID, pAccYearID, pGLAccountID, pGLGroupList, dc);
                    break;
                case GLAccountTypeFilterEnum.NormalAccount:
                    cObjList = GetGLAccountHistoryList_Normal(pCompanyID, pAccYearID, pGLAccountID, pGLGroupList, dc);
                    break;
                case GLAccountTypeFilterEnum.ControlAccount:
                    cObjList = GetGLAccountHistoryList_Control(pCompanyID, pAccYearID, pGLAccountID, pGLGroupList, dc);
                    break;
                case GLAccountTypeFilterEnum.SubAccount:
                    cObjList = GetGLAccountHistoryList_Sub(pCompanyID, pAccYearID, pGLAccountID, pGLGroupList, dc);
                    break;
                case GLAccountTypeFilterEnum.NormalControlAccount:
                    cObjList = GetGLAccountHistoryList_NormalControl(pCompanyID, pAccYearID, pGLAccountID, pGLGroupList, dc);
                    break;
                case GLAccountTypeFilterEnum.NormalSubAccount:
                    cObjList = GetGLAccountHistoryList_Normal(pCompanyID, pAccYearID, pGLAccountID, pGLGroupList, dc);
                    cObjList.AddRange(GetGLAccountHistoryList_Sub(pCompanyID, pAccYearID, pGLAccountID, pGLGroupList, dc));
                    break;
                case GLAccountTypeFilterEnum.ControlSubAccount:
                    cObjList = GetGLAccountHistoryList_Control(pCompanyID, pAccYearID, pGLAccountID, pGLGroupList, dc);
                    cObjList.AddRange(GetGLAccountHistoryList_Sub(pCompanyID, pAccYearID, pGLAccountID, pGLGroupList, dc));
                    break;
                case GLAccountTypeFilterEnum.SubAccountByControl:
                    cObjList = GetGLAccountHistoryList_SubByControl(pCompanyID, pAccYearID, pGLAccountID, pGLGroupList, dc);
                    break;
            }
            return cObjList;



            //List<dcGLAccountHistory> cObjList = new List<dcGLAccountHistory>();
            //bool isDCInit = false;
            //try
            //{
            //    isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

            //    SqlCommand cmd = new SqlCommand();
            //    StringBuilder sb = new StringBuilder();


            //    sb.Append(" SELECT  tblGLAccount.GLAccountID, tblGLAccount.GLAccountCode, tblGLAccount.GLAccountName ");
            //    sb.Append(" , tblGLAccount.GLAccountTypeID, tblGLAccount.GLAccountIDParent, tblGLAccount.GLGroupID ");
            //    sb.Append(" , tblGLAccountType.GLAccountTypeName, tblGLGroup.GLGroupName ");
            //    sb.Append(" , ISNULL(tblGLAccountHistory.DebitAmtOpen, 0 ) AS DebitAmtOpen ");
            //    sb.Append(" , ISNULL(tblGLAccountHistory.CreditAmtOpen, 0) AS CreditAmtOpen ");
            //    sb.Append(" , ISNULL(tblGLAccountHistory.OpenAmt, 0) AS OpenAmt ");

            //    sb.Append(string.Format(", {0} AS CompanyID ", pCompanyID));
            //    sb.Append(string.Format(", {0} AS AccYearID ", pAccYearID));

            //    sb.Append(" FROM tblGLAccount ");
            //    sb.Append(" LEFT OUTER JOIN tblGLAccountHistory ON tblGLAccount.GLAccountID = tblGLAccountHistory.GLAccountID ");
            //    sb.Append(" INNER JOIN tblGLAccountType ON tblGLAccount.GLAccountTypeID = tblGLAccountType.GLAccountTypeID ");
            //    sb.Append(" INNER JOIN tblGLGroup ON tblGLAccount.GLGroupID = tblGLGroup.GLGroupID ");

            //    sb.Append(" WHERE 1=1 ");

            //    sb.Append(" AND tblGLAccountHistory.CompanyID=@CompanyID ");
            //    cmd.Parameters.AddWithValue("@CompanyID", pCompanyID);

            //    sb.Append(" AND tblGLAccountHistory.AccYearID =@accYearID ");
            //    cmd.Parameters.AddWithValue("@accYearID", pAccYearID);

            //    //sb.Append(" AND tblGLAccount.GLAccountTypeID=@glAccountTypeID ");
            //    //cmd.Parameters.AddWithValue("@glAccountTypeID", (int)GLAccountTypeEnum.SubAccount);

            //    if (pGLGroupList != null)
            //    {
            //        string strGrpList = string.Empty;
            //        string comma = "";

            //        foreach (dcGLGroup grp in pGLGroupList)
            //        {
            //            strGrpList += comma + grp.GLGroupID;
            //            comma = ",";
            //        }

            //        if (strGrpList != string.Empty)
            //        {
            //            sb.Append(string.Format(" AND tblGLAccount.GLGroupID IN ({0}) ", strGrpList));
            //        }
            //    }


            //    if (pGLAccountID > 0)
            //    {
            //        sb.Append(" AND tblGLAccount.GLAccountID=@glAccountID ");
            //        cmd.Parameters.AddWithValue("@glAccountID", pGLAccountID);
            //    }


            //    sb.Append(" ORDER BY tblGLAccount.GLAccountCode, tblGLAccount.GLAccountName ");




            //    cmd.CommandType = CommandType.Text;
            //    cmd.CommandText = sb.ToString();

            //    DBQuery dbq = new DBQuery();
            //    dbq.DBQueryMode = DBQueryModeEnum.DBCommand;
            //    dbq.DBCommand = cmd;

            //    cObjList = GetGLAccountHistoryList(dbq, dc);

            //}
            //catch { throw; }
            //finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            //return cObjList;
        }

        public static List<dcGLAccountHistory> GetGLAccountHistoryList_Normal(int pCompanyID, int pAccYearID, int pGLAccountID, List<dcGLGroup> pGLGroupList)
        {
            return GetGLAccountHistoryList_Normal(pCompanyID, pAccYearID, pGLAccountID, pGLGroupList, null);
        }
        public static List<dcGLAccountHistory> GetGLAccountHistoryList_Normal(int pCompanyID, int pAccYearID, int pGLAccountID, List<dcGLGroup> pGLGroupList, DBContext dc)
        {
            List<dcGLAccountHistory> cObjList = new List<dcGLAccountHistory>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                //TODO Change This code 
                DBCommandInfo cmdInfo = new DBCommandInfo();
                //SqlCommand cmd = new SqlCommand();
                StringBuilder sb = new StringBuilder();


                sb.Append(" SELECT  tblGLAccount.GLAccountID, tblGLAccount.GLAccountCode, tblGLAccount.GLAccountName ");
                sb.Append(" , tblGLAccount.GLAccountTypeID, tblGLAccount.GLAccountIDParent, tblGLAccount.GLGroupID ");
                sb.Append(" , tblGLAccountType.GLAccountTypeName, tblGLGroup.GLGroupName ");
                sb.Append(" , ISNULL(tblGLAccountHistory.DebitAmtOpen, 0 ) AS DebitAmtOpen ");
                sb.Append(" , ISNULL(tblGLAccountHistory.CreditAmtOpen, 0) AS CreditAmtOpen ");
                sb.Append(" , ISNULL(tblGLAccountHistory.OpenAmt, 0) AS OpenAmt ");

                sb.Append(string.Format(", {0} AS CompanyID ", pCompanyID));
                sb.Append(string.Format(", {0} AS AccYearID ", pAccYearID));

                sb.Append(" FROM tblGLAccount ");
                sb.Append(" LEFT OUTER JOIN tblGLAccountHistory ON tblGLAccount.GLAccountID = tblGLAccountHistory.GLAccountID ");
                sb.Append(" INNER JOIN tblGLAccountType ON tblGLAccount.GLAccountTypeID = tblGLAccountType.GLAccountTypeID ");
                sb.Append(" INNER JOIN tblGLGroup ON tblGLAccount.GLGroupID = tblGLGroup.GLGroupID ");
                
                sb.Append(" WHERE 1=1 ");

                sb.Append(" AND tblGLAccountHistory.CompanyID=@CompanyID ");
                cmdInfo.DBParametersInfo.Add("@CompanyID", pCompanyID);

                sb.Append(" AND tblGLAccountHistory.AccYearID =@accYearID ");
                cmdInfo.DBParametersInfo.Add("@accYearID", pAccYearID);

                sb.Append(" AND tblGLAccount.GLAccountTypeID=@glAccountTypeID ");

                cmdInfo.DBParametersInfo.Add("@glAccountTypeID", (int)GLAccountTypeEnum.NormalAccount);

                if (pGLGroupList != null)
                {
                    string strGrpList = string.Empty;
                    string comma = "";

                    foreach (dcGLGroup grp in pGLGroupList)
                    {
                        strGrpList += comma + grp.GLGroupID;
                        comma = ",";
                    }

                    if (strGrpList != string.Empty)
                    {
                        sb.Append(string.Format(" AND tblGLAccount.GLGroupID IN ({0}) ", strGrpList));
                    }
                }


                if (pGLAccountID > 0)
                {
                    sb.Append(" AND tblGLAccount.GLAccountID=@glAccountID ");
                    //cmd.Parameters.AddWithValue("@glAccountID", pGLAccountID);
                    cmdInfo.DBParametersInfo.Add("@glAccountID", pGLAccountID);
                }

                //sb.Append(" AND tblGLGroup.GLClassID in(1,2,5)");

                sb.Append(" ORDER BY tblGLAccount.GLAccountCode, tblGLAccount.GLAccountName ");

         


                //cmd.CommandType = CommandType.Text;
                //cmd.CommandText = sb.ToString();

                //DBQuery dbq = new DBQuery();
                //dbq.DBQueryMode = DBQueryModeEnum.DBCommand;
                //dbq.DBCommand = cmd;

                //cObjList = GetGLAccountHistoryList(dbq, dc);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = GetGLAccountHistoryList(dbq, dc);

            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcGLAccountHistory> GetGLAccountHistoryList_Sub(int pCompanyID, int pAccYearID, int pGLAccountID, List<dcGLGroup> pGLGroupList)
        {
            return GetGLAccountHistoryList_Sub(pCompanyID, pAccYearID, pGLAccountID, pGLGroupList, null);
        }
        public static List<dcGLAccountHistory> GetGLAccountHistoryList_Sub(int pCompanyID, int pAccYearID, int pGLAccountID, List<dcGLGroup> pGLGroupList, DBContext dc)
        {
            List<dcGLAccountHistory> cObjList = new List<dcGLAccountHistory>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                //SqlCommand cmd = new SqlCommand();
                //TODO Change This code 
                DBCommandInfo cmdInfo = new DBCommandInfo();

                StringBuilder sb = new StringBuilder();


                sb.Append(" SELECT  tblGLAccount.GLAccountID, tblGLAccount.GLAccountCode, tblGLAccount.GLAccountName ");
                sb.Append(" , tblGLAccount.GLAccountTypeID, tblGLAccount.GLAccountIDParent, tblGLAccount.GLGroupID ");
                sb.Append(" , tblGLAccountType.GLAccountTypeName, tblGLGroup.GLGroupName ");
                sb.Append(" , ISNULL(tblGLAccountHistory.DebitAmtOpen, 0 ) AS DebitAmtOpen ");
                sb.Append(" , ISNULL(tblGLAccountHistory.CreditAmtOpen, 0) AS CreditAmtOpen ");
                sb.Append(" , ISNULL(tblGLAccountHistory.OpenAmt, 0) AS OpenAmt ");

                sb.Append(string.Format(", {0} AS CompanyID ", pCompanyID));
                sb.Append(string.Format(", {0} AS AccYearID ", pAccYearID));

                sb.Append(" FROM tblGLAccount ");
                sb.Append(" LEFT OUTER JOIN tblGLAccountHistory ON tblGLAccount.GLAccountID = tblGLAccountHistory.GLAccountID ");
                sb.Append(" INNER JOIN tblGLAccountType ON tblGLAccount.GLAccountTypeID = tblGLAccountType.GLAccountTypeID ");
                sb.Append(" INNER JOIN tblGLGroup ON tblGLAccount.GLGroupID = tblGLGroup.GLGroupID ");

                sb.Append(" WHERE 1=1 ");

                sb.Append(" AND tblGLAccountHistory.CompanyID=@CompanyID ");
                //cmd.Parameters.AddWithValue("@CompanyID", pCompanyID);
                cmdInfo.DBParametersInfo.Add("@CompanyID", pCompanyID);

                sb.Append(" AND tblGLAccountHistory.AccYearID =@accYearID ");
                //cmd.Parameters.AddWithValue("@accYearID", pAccYearID);
                cmdInfo.DBParametersInfo.Add("@accYearID", pAccYearID);

                sb.Append(" AND tblGLAccount.GLAccountTypeID=@glAccountTypeID ");
                //cmd.Parameters.AddWithValue("@glAccountTypeID", (int)GLAccountTypeEnum.SubAccount);
                cmdInfo.DBParametersInfo.Add("@glAccountTypeID", (int)GLAccountTypeEnum.NormalAccount);


                if (pGLGroupList != null)
                {
                    string strGrpList = string.Empty;
                    string comma = "";

                    foreach (dcGLGroup grp in pGLGroupList)
                    {
                        strGrpList += comma + grp.GLGroupID;
                        comma = ",";
                    }

                    if (strGrpList != string.Empty)
                    {
                        sb.Append(string.Format(" AND tblGLAccount.GLGroupID IN ({0}) ", strGrpList));
                    }
                }


                if (pGLAccountID > 0)
                {
                    sb.Append(" AND tblGLAccount.GLAccountID=@glAccountID ");
                    //cmd.Parameters.AddWithValue("@glAccountID", pGLAccountID);
                    cmdInfo.DBParametersInfo.Add("@glAccountID", pGLAccountID);
                }


                sb.Append(" ORDER BY tblGLAccount.GLAccountCode, tblGLAccount.GLAccountName ");




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


                cObjList = GetGLAccountHistoryList(dbq, dc);

            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcGLAccountHistory> GetGLAccountHistoryList_SubByControl(int pCompanyID, int pAccYearID, int pGLAccountID, List<dcGLGroup> pGLGroupList)
        {
            return GetGLAccountHistoryList_SubByControl(pCompanyID, pAccYearID, pGLAccountID, pGLGroupList, null);
        }
        public static List<dcGLAccountHistory> GetGLAccountHistoryList_SubByControl(int pCompanyID, int pAccYearID, int pGLAccountID, List<dcGLGroup> pGLGroupList, DBContext dc)
        {
            List<dcGLAccountHistory> cObjList = new List<dcGLAccountHistory>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                //SqlCommand cmd = new SqlCommand();
                //TODO Change This code 
                DBCommandInfo cmdInfo = new DBCommandInfo();

                StringBuilder sb = new StringBuilder();


                sb.Append(" SELECT  tblGLAccount.GLAccountID, tblGLAccount.GLAccountCode, tblGLAccount.GLAccountName ");
                sb.Append(" , tblGLAccount.GLAccountTypeID, tblGLAccount.GLAccountIDParent, tblGLAccount.GLGroupID ");
                sb.Append(" , tblGLAccountType.GLAccountTypeName, tblGLGroup.GLGroupName ");
                sb.Append(" , ISNULL(tblGLAccountHistory.DebitAmtOpen, 0 ) AS DebitAmtOpen ");
                sb.Append(" , ISNULL(tblGLAccountHistory.CreditAmtOpen, 0) AS CreditAmtOpen ");
                sb.Append(" , ISNULL(tblGLAccountHistory.OpenAmt, 0) AS OpenAmt ");

                sb.Append(string.Format(", {0} AS CompanyID ", pCompanyID));
                sb.Append(string.Format(", {0} AS AccYearID ", pAccYearID));

                sb.Append(" FROM tblGLAccount ");
                sb.Append(" LEFT OUTER JOIN tblGLAccountHistory ON tblGLAccount.GLAccountID = tblGLAccountHistory.GLAccountID ");
                sb.Append(" INNER JOIN tblGLAccountType ON tblGLAccount.GLAccountTypeID = tblGLAccountType.GLAccountTypeID ");
                sb.Append(" INNER JOIN tblGLGroup ON tblGLAccount.GLGroupID = tblGLGroup.GLGroupID ");

                sb.Append(" WHERE 1=1 ");

                sb.Append(" AND tblGLAccountHistory.CompanyID=@CompanyID ");
                //cmd.Parameters.AddWithValue("@CompanyID", pCompanyID);
                cmdInfo.DBParametersInfo.Add("@CompanyID", pCompanyID);

                sb.Append(" AND tblGLAccountHistory.AccYearID =@accYearID ");
                //cmd.Parameters.AddWithValue("@accYearID", pAccYearID);
                cmdInfo.DBParametersInfo.Add("@accYearID", pAccYearID);

                sb.Append(" AND tblGLAccount.GLAccountTypeID=@glAccountTypeID ");
                //cmd.Parameters.AddWithValue("@glAccountTypeID", (int)GLAccountTypeEnum.SubAccount);
                cmdInfo.DBParametersInfo.Add("@glAccountTypeID", (int)GLAccountTypeEnum.NormalAccount);

                if (pGLGroupList != null)
                {
                    string strGrpList = string.Empty;
                    string comma = "";

                    foreach (dcGLGroup grp in pGLGroupList)
                    {
                        strGrpList += comma + grp.GLGroupID;
                        comma = ",";
                    }

                    if (strGrpList != string.Empty)
                    {
                        sb.Append(string.Format(" AND tblGLAccount.GLGroupID IN ({0}) ", strGrpList));
                    }
                }


                if (pGLAccountID > 0)
                {
                    sb.Append(" AND tblGLAccount.GLAccountIDParent=@glAccountID ");
                    //cmd.Parameters.AddWithValue("@glAccountID", pGLAccountID);
                    cmdInfo.DBParametersInfo.Add("@glAccountID", pGLAccountID);
                }


                sb.Append(" ORDER BY tblGLAccount.GLAccountCode, tblGLAccount.GLAccountName ");


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

                cObjList = GetGLAccountHistoryList(dbq, dc);

            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }



        //public static List<dcGLAccountHistory> GetGLAccountHistoryList_SubByControl(int pCompanyID, int pAccYearID, int pGLAccountID, List<dcGLGroup> pGLGroupList)
        //{
        //    return GetGLAccountHistoryList_SubByControl(pCompanyID, pAccYearID, pGLAccountID, pGLGroupList, null);
        //}
        //public static List<dcGLAccountHistory> GetGLAccountHistoryList_SubByControl(int pCompanyID, int pAccYearID, int pGLAccountID, List<dcGLGroup> pGLGroupList, DBContext dc)
        //{
        //    List<dcGLAccountHistory> cObjList = new List<dcGLAccountHistory>();
        //    bool isDCInit = false;
        //    try
        //    {
        //        isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

        //        SqlCommand cmd = new SqlCommand();
        //        StringBuilder sb = new StringBuilder();


        //        sb.Append(" SELECT  tblGLAccount.GLAccountID, tblGLAccount.GLAccountCode, tblGLAccount.GLAccountName ");
        //        sb.Append(" , tblGLAccount.GLAccountTypeID, tblGLAccount.GLAccountIDParent, tblGLAccount.GLGroupID ");
        //        sb.Append(" , tblGLAccountType.GLAccountTypeName, tblGLGroup.GLGroupName ");

        //        sb.Append(" , HistorySum.DebitAmtOpen, HistorySum.CreditAmtOpen , HistorySum.OpenAmt ");

        //        sb.Append(string.Format(", {0} AS CompanyID ", pCompanyID));
        //        sb.Append(string.Format(", {0} AS AccYearID ", pAccYearID));

        //        sb.Append(" FROM tblGLAccount ");

        //        sb.Append(" INNER JOIN ( ");
        //        sb.Append(" SELECT  tblGLAccount.GLAccountIDParent AS GLAccountID ");
        //        sb.Append(" , ISNULL(SUM(tblGLAccountHistory.DebitAmtOpen), 0 ) AS DebitAmtOpen ");
        //        sb.Append(" , ISNULL(SUM(tblGLAccountHistory.CreditAmtOpen), 0) AS CreditAmtOpen ");
        //        sb.Append(" , ISNULL( SUM(tblGLAccountHistory.OpenAmt), 0) AS OpenAmt ");
        //        sb.Append(" FROM tblGLAccount ");
        //        sb.Append(" LEFT OUTER JOIN tblGLAccountHistory ON tblGLAccount.GLAccountID = tblGLAccountHistory.GLAccountID ");

        //        sb.Append(" WHERE 1=1 ");

        //        sb.Append(" AND tblGLAccountHistory.CompanyID=@CompanyID ");
        //        cmd.Parameters.AddWithValue("@CompanyID", pCompanyID);

        //        sb.Append(" AND tblGLAccountHistory.AccYearID =@accYearID ");
        //        cmd.Parameters.AddWithValue("@accYearID", pAccYearID);

        //        sb.Append(" AND tblGLAccount.GLAccountTypeID=@glAccountTypeID ");
        //        cmd.Parameters.AddWithValue("@glAccountTypeID", (int)GLAccountTypeEnum.SubAccount);


        //        if (pGLGroupList != null)
        //        {
        //            string strGrpList = string.Empty;
        //            string comma = "";

        //            foreach (dcGLGroup grp in pGLGroupList)
        //            {
        //                strGrpList += comma + grp.GLGroupID;
        //                comma = ",";
        //            }

        //            if (strGrpList != string.Empty)
        //            {
        //                sb.Append(string.Format(" AND tblGLAccount.GLGroupID IN ({0}) ", strGrpList));
        //            }
        //        }



        //        if (pGLAccountID > 0)
        //        {
        //            sb.Append(" AND tblGLAccount.GLAccountIDParent=@glAccountID ");
        //            cmd.Parameters.AddWithValue("@glAccountID", pGLAccountID);
        //        }

        //        sb.Append(" GROUP BY tblGLAccount.GLAccountIDParent ");

        //        sb.Append(" ) AS HistorySum ON  tblGLAccount.GLAccountID = HistorySum.GLAccountID ");
        //        sb.Append(" INNER JOIN tblGLAccountType ON tblGLAccount.GLAccountTypeID = tblGLAccountType.GLAccountTypeID ");
        //        sb.Append(" INNER JOIN tblGLGroup ON tblGLAccount.GLGroupID = tblGLGroup.GLGroupID ");

        //        sb.Append(" WHERE 1=1 ");

        //        sb.Append(" ORDER BY tblGLAccount.GLAccountCode, tblGLAccount.GLAccountName ");

        //        cmd.CommandType = CommandType.Text;
        //        cmd.CommandText = sb.ToString();

        //        DBQuery dbq = new DBQuery();
        //        dbq.DBQueryMode = DBQueryModeEnum.DBCommand;
        //        dbq.DBCommand = cmd;

        //        cObjList = GetGLAccountHistoryList(dbq, dc);


        //    }
        //    catch { throw; }
        //    finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
        //    return cObjList;
        //}


        public static List<dcGLAccountHistory> GetGLAccountHistoryList_Control(int pCompanyID, int pAccYearID, int pGLAccountID, List<dcGLGroup> pGLGroupList)
        {
            return GetGLAccountHistoryList_Control(pCompanyID, pAccYearID, pGLAccountID, pGLGroupList, null);
        }
        public static List<dcGLAccountHistory> GetGLAccountHistoryList_Control(int pCompanyID, int pAccYearID, int pGLAccountID, List<dcGLGroup> pGLGroupList, DBContext dc)
        {
            List<dcGLAccountHistory> cObjList = new List<dcGLAccountHistory>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                //SqlCommand cmd = new SqlCommand();
                DBCommandInfo cmdInfo = new DBCommandInfo();

                StringBuilder sb = new StringBuilder();


                sb.Append(" SELECT  tblGLAccount.GLAccountID, tblGLAccount.GLAccountCode, tblGLAccount.GLAccountName ");
                sb.Append(" , tblGLAccount.GLAccountTypeID, tblGLAccount.GLAccountIDParent, tblGLAccount.GLGroupID ");
                sb.Append(" , tblGLAccountType.GLAccountTypeName, tblGLGroup.GLGroupName ");

                sb.Append(" , HistorySum.DebitAmtOpen, HistorySum.CreditAmtOpen , HistorySum.OpenAmt ");

                sb.Append(string.Format(", {0} AS CompanyID ", pCompanyID));
                sb.Append(string.Format(", {0} AS AccYearID ", pAccYearID));
                
                sb.Append(" FROM tblGLAccount ");

                sb.Append(" INNER JOIN ( ");
                sb.Append(" SELECT  tblGLAccount.GLAccountIDParent AS GLAccountID ");
                sb.Append(" , ISNULL(SUM(tblGLAccountHistory.DebitAmtOpen), 0 ) AS DebitAmtOpen ");
                sb.Append(" , ISNULL(SUM(tblGLAccountHistory.CreditAmtOpen), 0) AS CreditAmtOpen ");
                sb.Append(" , ISNULL( SUM(tblGLAccountHistory.OpenAmt), 0) AS OpenAmt ");
                sb.Append(" FROM tblGLAccount ");
                sb.Append(" LEFT OUTER JOIN tblGLAccountHistory ON tblGLAccount.GLAccountID = tblGLAccountHistory.GLAccountID ");

                sb.Append(" WHERE 1=1 ");

                sb.Append(" AND tblGLAccountHistory.CompanyID=@CompanyID ");
                //cmd.Parameters.AddWithValue("@CompanyID", pCompanyID);
                cmdInfo.DBParametersInfo.Add("@CompanyID", pCompanyID);

                sb.Append(" AND tblGLAccountHistory.AccYearID =@accYearID ");
                //cmd.Parameters.AddWithValue("@accYearID", pAccYearID);
                cmdInfo.DBParametersInfo.Add("@accYearID", pAccYearID);

                sb.Append(" AND tblGLAccount.GLAccountTypeID=@glAccountTypeID ");
                //cmd.Parameters.AddWithValue("@glAccountTypeID", (int)GLAccountTypeEnum.SubAccount);
                cmdInfo.DBParametersInfo.Add("@glAccountTypeID", (int)GLAccountTypeEnum.NormalAccount);


                if (pGLGroupList != null)
                {
                    string strGrpList = string.Empty;
                    string comma = "";

                    foreach (dcGLGroup grp in pGLGroupList)
                    {
                        strGrpList += comma + grp.GLGroupID;
                        comma = ",";
                    }

                    if (strGrpList != string.Empty)
                    {
                        sb.Append(string.Format(" AND tblGLAccount.GLGroupID IN ({0}) ", strGrpList));
                    }
                }



                if (pGLAccountID > 0)
                {
                    sb.Append(" AND tblGLAccount.GLAccountIDParent=@glAccountID ");
                    //cmd.Parameters.AddWithValue("@glAccountID", pGLAccountID);
                    cmdInfo.DBParametersInfo.Add("@glAccountID", pGLAccountID);
                }

                sb.Append(" GROUP BY tblGLAccount.GLAccountIDParent ");
                //TODO Change code as
                sb.Append(" )  HistorySum ON  tblGLAccount.GLAccountID = HistorySum.GLAccountID ");
                sb.Append(" INNER JOIN tblGLAccountType ON tblGLAccount.GLAccountTypeID = tblGLAccountType.GLAccountTypeID ");
                sb.Append(" INNER JOIN tblGLGroup ON tblGLAccount.GLGroupID = tblGLGroup.GLGroupID ");

                sb.Append(" WHERE 1=1 ");

                sb.Append(" ORDER BY tblGLAccount.GLAccountCode, tblGLAccount.GLAccountName ");


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

                cObjList = GetGLAccountHistoryList(dbq, dc);

          
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcGLAccountHistory> GetGLAccountHistoryList_NormalControl(int pCompanyID, int pAccYearID, int pGLAccountID, List<dcGLGroup> pGLGroupList)
        {
            return GetGLAccountHistoryList_NormalControl(pCompanyID, pAccYearID, pGLAccountID, pGLGroupList, null);
        }
        public static List<dcGLAccountHistory> GetGLAccountHistoryList_NormalControl(int pCompanyID, int pAccYearID, int pGLAccountID, List<dcGLGroup> pGLGroupList, DBContext dc)
        {
            List<dcGLAccountHistory> cObjList = GetGLAccountHistoryList_Normal(pCompanyID, pAccYearID, pGLAccountID, pGLGroupList, dc);
            cObjList.AddRange(GetGLAccountHistoryList_Control(pCompanyID, pAccYearID, pGLAccountID, pGLGroupList, dc));
            return cObjList;
        }


        public static List<dcGLAccountHistory> GetGLAccountHistoryList_ALL(int pCompanyID, int pAccYearID, int pGLAccountID,List<dcGLGroup> pGLGroupList)
        {
            return GetGLAccountHistoryList_ALL(pCompanyID, pAccYearID, pGLAccountID, pGLGroupList, null);
        }
        public static List<dcGLAccountHistory> GetGLAccountHistoryList_ALL(int pCompanyID, int pAccYearID, int pGLAccountID, List<dcGLGroup> pGLGroupList, DBContext dc)
        {
            List<dcGLAccountHistory> cObjList = GetGLAccountHistoryList_Normal(pCompanyID, pAccYearID, pGLAccountID, pGLGroupList, dc);
            cObjList.AddRange(GetGLAccountHistoryList_Control(pCompanyID, pAccYearID, pGLAccountID, pGLGroupList, dc));
            cObjList.AddRange(GetGLAccountHistoryList_Sub(pCompanyID, pAccYearID, pGLAccountID, pGLGroupList, dc));
            return cObjList;
        }


        public static List<dcGLAccountHistory> GetGLAccountHistoryList_NormalControlWithAcc(int pCompanyID, int pAccYearID, List<dcGLGroup> pGLGroupList, DBContext dc)
        {
            List<dcGLAccountHistory> cHistList = GetGLAccountHistoryList_NormalControl(pCompanyID, pAccYearID, 0, pGLGroupList, dc);
            
            List<dcGLAccount> accList = GLAccountBL.GetGLAccountListAccountType(pCompanyID,0,-1, GLAccountTypeFilterEnum.NormalControlAccount,dc);

            List<dcGLAccountHistory> cObjList = new List<dcGLAccountHistory>();

            foreach (dcGLAccount acc in accList)
            {
                dcGLAccountHistory accHist = new dcGLAccountHistory();

                PG.Core.Utility.Helper.CopyPropertyValueByName(acc, accHist);

                dcGLAccountHistory accHistDb = cHistList.SingleOrDefault(c => c.GLAccountID == acc.GLAccountID);
                if (accHistDb != null)
                {
                    PG.Core.Utility.Helper.CopyPropertyValueByName(accHistDb, accHist);
                }


                cObjList.Add(accHist);
            }


            return cObjList;
        }



        public static dcGLAccountHistory GetGLAccountHistoryByID(int pCompanyID, int pAccYearID, int pGLAccountID)
        {
            return GetGLAccountHistoryByID(pCompanyID,pAccYearID, pGLAccountID, null);
        }
        public static dcGLAccountHistory GetGLAccountHistoryByID(int pCompanyID, int pAccYearID, int pGLAccountID, DBContext dc)
        {
            dcGLAccountHistory cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                //SqlCommand cmd = new SqlCommand();
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();

                sb.Append("SELECT * from tblGLAccountHistory WHERE (1=1) ");

                sb.Append(" AND tblGLAccountHistory.CompanyID=@CompanyID ");
                //cmd.Parameters.AddWithValue("@CompanyID", pCompanyID);
                cmdInfo.DBParametersInfo.Add("@CompanyID", pCompanyID);

                if (pAccYearID > 0)
                {
                    sb.Append(" AND tblGLAccountHistory.AccYearID=@accYearID ");
                    //cmd.Parameters.AddWithValue("@accYearID", pAccYearID);
                    cmdInfo.DBParametersInfo.Add("@accYearID", pAccYearID);
                }


                if (pGLAccountID > 0)
                {
                    sb.Append(" AND tblGLAccountHistory.GLAccountID=@glAccountID ");
                    //cmd.Parameters.AddWithValue("@glAccountID", pGLAccountID);
                    cmdInfo.DBParametersInfo.Add("@glAccountID", pGLAccountID);
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

                List<dcGLAccountHistory> cObjList = DBQuery.ExecuteDBQuery<dcGLAccountHistory>(dbq, dc);

                if (cObjList.Count > 0)
                {
                    cObj = cObjList.First();
                }


            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObj;
        }

        public static dcGLAccountHistory GetGLAccountHistoryByID_Control(int pCompanyID, int pAccYearID, int pGLAccountID)
        {
            return GetGLAccountHistoryByID_Control(pCompanyID, pAccYearID, pGLAccountID, null);
        }
        public static dcGLAccountHistory GetGLAccountHistoryByID_Control(int pCompanyID, int pAccYearID, int pGLAccountID, DBContext dc)
        {
            dcGLAccountHistory cObj = null;
            cObj = GetGLAccountHistoryList_Control(pCompanyID, pAccYearID, pGLAccountID, null, dc).SingleOrDefault();
            return cObj;
        }

        public static bool UpdateGLHistory(int pCompanyID , int pAccYearID, List<dcGLAccountHistory> pGLHistoryList)
        {
            return UpdateGLHistory(pCompanyID, pAccYearID, pGLHistoryList, null);
        }
        public static bool UpdateGLHistory(int pCompanyID, int pAccYearID,List<dcGLAccountHistory> pGLHistoryList, DBContext dc)
        {
            bool isDCInit = false;
            bool isTransInit = false;
            bool bStatus = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                isTransInit = dc.StartTransaction();

                List<dcGLAccountHistory> curOpenList = new List<dcGLAccountHistory>();

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GLAccountHistoryList_SQLString());
                sb.Append(" AND tblGLAccountHistory.CompanyID=@companyID AND tblGLAccountHistory.AccYearID=@accYearID");
                cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);
                cmdInfo.DBParametersInfo.Add("@accYearID", pAccYearID);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                curOpenList = DBQuery.ExecuteDBQuery<dcGLAccountHistory>(dbq, dc).ToList();
                //using (DataContext dataContext = dc.NewDataContext())
                //{
          
                //    curOpenList = (from c in dataContext.GetTable<dcGLAccountHistory>()
                //                 where c.AccYearID == pAccYearID && c.CompanyID == pCompanyID
                //                 select c).ToList();
                //}

                foreach (dcGLAccountHistory accHist in pGLHistoryList)
                {
                    if (accHist.GLAccountTypeID == (int)GLAccountTypeEnum.ControlAccount)
                    {
                        continue;
                    }


                    dcGLAccountHistory curOpen = curOpenList.SingleOrDefault(c => c.AccYearID == pAccYearID 
                                                                                   && c.GLAccountID == accHist.GLAccountID);
                    dcGLAccountHistory cObj = new dcGLAccountHistory();

                    cObj.GLAccountID = accHist.GLAccountID;
                    cObj.AccYearID = pAccYearID;
                    cObj.CompanyID = pCompanyID;

                    cObj.DebitAmtOpen = accHist.DebitAmtOpen;
                    cObj.CreditAmtOpen = accHist.CreditAmtOpen;

                    cObj.DebitAmtOpen = cObj.DebitAmtOpen <= 0 ? 0 : cObj.DebitAmtOpen;
                    cObj.CreditAmtOpen = cObj.CreditAmtOpen <= 0 ? 0 : cObj.CreditAmtOpen;
                    
                    cObj.CreditAmtOpen = cObj.DebitAmtOpen > 0 ? 0 : cObj.CreditAmtOpen;

                    cObj.OpenAmt = cObj.DebitAmtOpen - cObj.CreditAmtOpen;

                    if (curOpen == null)
                    {
                        accHist.GLAccHistID = dc.DoInsert<dcGLAccountHistory>(cObj, true);
                    }
                    else
                    {
                        cObj.GLAccHistID = curOpen.GLAccHistID;
                        dc.DoUpdate<dcGLAccountHistory>(cObj);
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

        public static bool UpdateGLAccountHistory(int pCompanyID, int pAccYearID, int pGLAccountID, decimal pDebitAmt, decimal pCreditAmt)
        {
            return UpdateGLAccountHistory(pCompanyID, pAccYearID, pGLAccountID, pDebitAmt, pCreditAmt, null);
        }
        public static bool UpdateGLAccountHistory(int pCompanyID, int pAccYearID, int pGLAccountID, decimal pDebitAmt, decimal pCreditAmt , DBContext dc)
        {
            decimal tAmt = pDebitAmt;
            DebitCreditEnum drCr = DebitCreditEnum.Debit;
            if (pCreditAmt > 0)
            {
                drCr = DebitCreditEnum.Credit;
                tAmt = pCreditAmt;
            }

            return UpdateGLAccountHistory(pCompanyID, pAccYearID, pGLAccountID, tAmt, drCr, null);
        }

        public static bool UpdateGLAccountHistory(int pCompanyID, int pAccYearID, int pGLAccountID, decimal pAmount, DebitCreditEnum pDrCr)
        {
            return UpdateGLAccountHistory(pCompanyID, pGLAccountID, pAccYearID, pAmount, pDrCr, null);
        }
        public static bool UpdateGLAccountHistory(int pCompanyID, int pAccYearID, int pGLAccountID, decimal pAmount, DebitCreditEnum pDrCr, DBContext dc)
        {
            bool isDCInit = false;
            bool bStatus = false;
            bool isAdd = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                dcGLAccountHistory accHist = GetGLAccountHistoryByID(pCompanyID,pAccYearID ,pGLAccountID, dc);
                if (accHist == null)
                {
                    accHist = new dcGLAccountHistory();
                    accHist.GLAccountID = pGLAccountID;
                    accHist.AccYearID = pAccYearID;
                    accHist.CompanyID = pCompanyID;
                    isAdd = true;
                }


                if (pDrCr == DebitCreditEnum.Debit)
                {
                    accHist.DebitAmtOpen = pAmount;
                    accHist.CreditAmtOpen = 0;
                }
                else
                {
                    accHist.DebitAmtOpen = 0;
                    accHist.CreditAmtOpen = pAmount;
                }

                if (isAdd)
                {
                   int id= dc.DoInsert<dcGLAccountHistory>(accHist, true);
                }
                else
                {
                    accHist.GLAccHistID = accHist.GLAccHistID;
                    dc.DoUpdate<dcGLAccountHistory>(accHist);
                }
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
