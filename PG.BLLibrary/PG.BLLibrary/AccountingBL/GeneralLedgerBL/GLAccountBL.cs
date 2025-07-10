using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Data.Linq;
using PG.Core.Utility;
using PG.Core.DBBase;
using PG.Core.Extentions;
using PG.Core.DBFilters;
using PG.DBClass.AccountingDC;
using PG.DBClass.AccountingDC.AccEnums;
using PG.DBClass.AccountingDC.GeneralLedgerDC;

namespace PG.BLLibrary.AccountingBL.GeneralLedgerBL
{
    /// <summary>
    /// AppMenuBL
    /// Last update By Moni, Date 09-03-2015
    /// </summary>
    public class GLAccountBL
    {
        //public static DataLoadOptions GLAccountLoadOptions()
        //{
        //    DataLoadOptions dlo = new DataLoadOptions();
        //    //dlo.LoadWith<DBClass.PayRoll.SalaryPeriod>(obj => obj.PayRoll.SalaryPeriodType);
        //    //dlo.LoadWith<DBClass.dcPurchase>(obj => obj.Supplier);
        //    //dlo.LoadWith<DBClass.dcPurchase>(obj => obj.TranType);
        //    return dlo;
        //}

        //TODO: need to change this Query Remove as
        public static string GetGLAccountListString()
        {

            StringBuilder sb = new StringBuilder();
            
            sb.Append("SELECT tblGLAccount.* ");
            sb.Append(", tblGLGroup.GLGroupCode, tblGLGroup.GLGroupName,  tblGLGroup.GLGroupNameShort,tblGLGroup.GLGroupSLNo, tblGLGroup.GLClassID, tblGLGroup.GLGroupClassID ");
            sb.Append(", tblGLAccount_1.GLAccountCode AS GLAccountCodeParent, tblGLAccount_1.GLAccountName AS GLAccountNameParent ");
            sb.Append(", tblGLAccountType.GLAccountTypeName ");
            sb.Append(", tblGLGroupClass.GLGroupClassName, ISNULL(tblGLGroupClass.IsInstrument,0) as IsInstrument ");
            sb.Append(", tblGLClass.GLClassName ");
            sb.Append(" FROM tblGLAccount ");
            sb.Append(" INNER JOIN tblGLGroup ON tblGLAccount.GLGroupID = tblGLGroup.GLGroupID " );
            sb.Append(" INNER JOIN tblGLClass ON tblGLGroup.GLClassID = tblGLClass.GLClassID ");
            sb.Append(" INNER JOIN tblGLAccountType ON tblGLAccount.GLAccountTypeID = tblGLAccountType.GLAccountTypeID ");
            sb.Append(" LEFT OUTER JOIN tblGLAccount tblGLAccount_1 ON tblGLAccount.GLAccountIDParent = tblGLAccount_1.GLAccountID ");
            sb.Append(" LEFT OUTER JOIN tblGLGroupClass ON tblGLGroup.GLGroupClassID = tblGLGroupClass.GLGroupClassID ");
            sb.Append(" LEFT OUTER JOIN tblLocationGLAccount ON tblGLAccount.GLAccountID=tblLocationGLAccount.GLAccountID "); //Add Location filter

            sb.Append(" WHERE (1=1) ");


            return sb.ToString();
        }

        public static string GetGLAccountGroupNameListString()
        {

            StringBuilder sb = new StringBuilder();

            sb.Append("Select * FROM tblGLGroup"); //Add Location filter

            sb.Append(" WHERE (1=1) ");


            return sb.ToString();
        }

        public static List<dcGLAccount> GetGLAccountList(int pCompanyID)
        {
            return GetGLAccountList(pCompanyID, 0, -1, 0, null);
        }

        public static List<dcGLAccount> GetGLAccountList(int pCompanyID, DBContext dc)
        {
            return GetGLAccountList(pCompanyID, 0, -1, 0, dc);
        }

        public static List<dcGLAccount> GetGLAccountList(int pCompanyID, int pGLClassID, int pGLGroupID)
        {
            return GetGLAccountList(pCompanyID, pGLClassID, pGLGroupID, 0, null);
        }

        public static List<dcGLAccount> GetGLAccountList(int pCompanyID, int pGLClassID, int pGLGroupID, int pGLAccountTypeID)
        {
            return GetGLAccountList(pCompanyID, pGLClassID, pGLGroupID, pGLAccountTypeID, null);
        }

        public static List<dcGLAccount> GetGLAccountList(int pCompanyID, int pGLClassID, int pGLGroupID, int pGLAccountTypeID, DBContext dc)
        {
            List<dcGLAccount> cObjList = new List<dcGLAccount>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

               // System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetGLAccountListString());

                sb.Append(" AND tblGLAccount.CompanyID=@companyID ");
                //cmd.Parameters.AddWithValue("@companyID", pCompanyID);
                cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);

                if (pGLClassID > 0)
                {
                    sb.Append(" AND tblGLGroup.GLClassID=@GLClassID ");
                    //cmd.Parameters.AddWithValue("@GLClassID", pGLClassID);
                    cmdInfo.DBParametersInfo.Add("@GLClassID", pGLClassID);
                }

                if (pGLGroupID != -1)
                {
                    sb.Append(" AND tblGLAccount.GLGroupID=@gLGroupID ");
                    //cmd.Parameters.AddWithValue("@gLGroupID", pGLGroupID);
                    cmdInfo.DBParametersInfo.Add("@gLGroupID", pGLGroupID);
                }



                if (pGLAccountTypeID > 0)
                {
                    sb.Append(" AND tblGLAccount.GLAccountTypeID=@gLAccountTypeID ");
                    //cmd.Parameters.AddWithValue("@gLAccountTypeID", pGLAccountTypeID);
                    cmdInfo.DBParametersInfo.Add("@gLAccountTypeID", pGLAccountTypeID);
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
                //dbq.DBQueryMode = PG.Core.DBBase.DBQueryModeEnum.DBCommand;
                //dbq.DBCommand = cmd;

                cObjList = GetGLAccountList(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        //GLAccountTypeFilterEnum

        public static List<dcGLAccount> GetGLAccountListAccountType(int pCompanyID, int pGLClassID, int pGLGroupID, GLAccountTypeFilterEnum pGLAccountTypeFilter)
        {
            return GetGLAccountListAccountType(pCompanyID, pGLClassID, pGLGroupID, pGLAccountTypeFilter,null);
        }

        public static List<dcGLAccount> GetGLAccountListAccountType(int pCompanyID, int pGLClassID, int pGLGroupID, GLAccountTypeFilterEnum pGLAccountTypeFilter, DBContext dc)
        {
            List<dcGLAccount> cObjList = new List<dcGLAccount>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetGLAccountListString());

                sb.Append(" AND tblGLAccount.CompanyID=@companyID ");
                //cmd.Parameters.AddWithValue("@companyID", pCompanyID);
                cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);

                if (pGLClassID > 0)
                {
                    sb.Append(" AND tblGLGroup.GLClassID=@GLClassID ");
                    //cmd.Parameters.AddWithValue("@GLClassID", pGLClassID);
                    cmdInfo.DBParametersInfo.Add("@GLClassID", pGLClassID);
                }

                if (pGLGroupID != -1)
                {
                    sb.Append(" AND tblGLAccount.GLGroupID=@gLGroupID ");
                    //cmd.Parameters.AddWithValue("@gLGroupID", pGLGroupID);
                    cmdInfo.DBParametersInfo.Add("@gLGroupID", pGLGroupID);
                }


                if (pGLAccountTypeFilter != GLAccountTypeFilterEnum.NoFilter
                                && pGLAccountTypeFilter != GLAccountTypeFilterEnum.AllAccount)
                {
                    string strList = AccHelper.GetGLAccountTypeIDFilterListString(pGLAccountTypeFilter);
                    if (strList != string.Empty)
                    {
                        sb.Append(string.Format( " AND tblGLAccount.GLAccountTypeID IN ({0}) ", strList));
                    }
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
                //dbq.DBQueryMode = PG.Core.DBBase.DBQueryModeEnum.DBCommand;
                //dbq.DBCommand = cmd;

                cObjList = GetGLAccountList(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }


        public static List<dcGLAccount> GetGLAccountListAccountType(int pCompanyID, int pGLClassID, int pGLGroupID, List<string> pGLAccTypeTypeList)
        {
            return GetGLAccountListAccountType(pCompanyID, pGLClassID, pGLGroupID,  pGLAccTypeTypeList, null);
        }

        public static List<dcGLAccount> GetGLAccountListAccountType(int pCompanyID, int pGLClassID, int pGLGroupID,List<string> pGLAccTypeTypeList, DBContext dc)
        {
            List<dcGLAccount> cObjList = new List<dcGLAccount>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetGLAccountListString());

                sb.Append(" AND tblGLAccount.CompanyID=@companyID ");
                //cmd.Parameters.AddWithValue("@companyID", pCompanyID);
                cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);

                if (pGLClassID > 0)
                {
                    sb.Append(" AND tblGLGroup.GLClassID=@GLClassID ");
                    //cmd.Parameters.AddWithValue("@GLClassID", pGLClassID);
                    cmdInfo.DBParametersInfo.Add("@GLClassID", pGLClassID);
                }

                if (pGLGroupID > 0)
                {
                    sb.Append(" AND tblGLAccount.GLGroupID=@gLGroupID ");
                    //cmd.Parameters.AddWithValue("@gLGroupID", pGLGroupID);
                    cmdInfo.DBParametersInfo.Add("@gLGroupID", pGLGroupID);
                }


                string strList = "0";

                if (pGLAccTypeTypeList.Count > 0)
                {
                    strList = string.Join(",", pGLAccTypeTypeList.ToArray());
                }

                if (strList != string.Empty)
                {
                    sb.Append(string.Format(" AND tblGLAccount.GLAccountTypeID IN ({0}) ", strList));
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
                //dbq.DBQueryMode = PG.Core.DBBase.DBQueryModeEnum.DBCommand;
                //dbq.DBCommand = cmd;

                cObjList = GetGLAccountList(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcGLAccount> GetGLAccountListByIDList(List<int> pListID)
        {
            return GetGLAccountListByIDList(pListID, null);
        }

        public static List<dcGLAccount> GetGLAccountListByIDList(List<int> pListID, DBContext dc)
        {
            List<dcGLAccount> cObjList = new List<dcGLAccount>();
            if (pListID.Count == 0 )
            {
                return cObjList;
            }
            
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetGLAccountListString());
                string strList = String.Join(",", pListID.ToArray());


                string strParams = string.Empty;
                string paramName = string.Empty;
                string strVals = string.Empty;
                int idx = 0;
                string comma = "";
                foreach (int glID in pListID)
                {
                    paramName = "@glaccid_" + idx.ToString();
                    cmdInfo.DBParametersInfo.Add(paramName, glID);
                    strParams += comma + paramName;
                    strVals += comma + glID.ToString();
                    comma = ",";
                    idx++;
                }


                if (pListID.Count > 0)
                {
                    sb.Append(string.Format(" AND tblGLAccount.GLAccountID IN ({0}) ",strParams));
                    //sb.Append(string.Format(" AND tblGLAccount.GLAccountID IN ({0}) ", strVals));
                }
                //sb.Append(" AND tblGLAccount.GLAccountID IN (@gLAccountIDList) ");
                //cmdInfo.DBParametersInfo.Add("@gLAccountIDList", strList);

                //pListID.ToList<string>();

                sb.Append(" ORDER BY tblGLAccount.GLAccountCode, tblGLAccount.GLAccountName ");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                //cmd.CommandType = CommandType.Text;
                //cmd.CommandText = sb.ToString();

                //DBQuery dbq = new DBQuery();
                //dbq.DBQueryMode = PG.Core.DBBase.DBQueryModeEnum.DBCommand;
                //dbq.DBCommand = cmd;

                cObjList = GetGLAccountList(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }



        public static List<dcGLAccount> GetGLAccountList(DBQuery dbq, DBContext dc)
        {
            List<dcGLAccount> cObjList = new List<dcGLAccount>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                if (dbq == null)
                {
                    dbq = new DBQuery();
                    //dbq.OrderBy = "PeriodStartDate";
                }
                cObjList = DBQuery.ExecuteDBQuery<dcGLAccount>(dbq, dc);
                
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcGLAccount> GetGLAccountList()
        {
            return GetGLAccountList(false, AccOrderByEnum.SLNo, string.Empty, null);
        }

        public static List<dcGLAccount> GetGLAccountList(DBContext dc)
        {
            return GetGLAccountList(false, AccOrderByEnum.SLNo , string.Empty, dc);
        }

        public static List<dcGLAccount> GetGLAccountList(bool orderByGroup, AccOrderByEnum pOrderBy, string indentChar)
        {
            return GetGLAccountList(orderByGroup, pOrderBy, indentChar, null);
        }
        public static List<dcGLAccount> GetGLAccountList(bool orderByGroup, AccOrderByEnum pOrderBy, string indentChar, DBContext dc)
        {
            List<dcGLAccount> cObjList = new List<dcGLAccount>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                StringBuilder sb = new StringBuilder(GetGLAccountListString());

                //if (pGLAccountID > 0)
                //{
                //    sb.Append(" AND tblGLAccount.GLAccountID=@glAccountID ");
                //    cmd.Parameters.AddWithValue("@glAccountID", pGLAccountID);
                //}

                sb.Append(" ORDER BY tblGLAccount.GLAccountCode ");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                //cmd.CommandType = CommandType.Text;
                //cmd.CommandText = sb.ToString();

                //DBQuery dbq = new DBQuery();
                //dbq.DBQueryMode = PG.Core.DBBase.DBQueryModeEnum.DBCommand;
                //dbq.DBCommand = cmd;

                cObjList = GetGLAccountList(dbq, dc);
                //cObjList = FormatGLAccount(cObjList, orderByGroup, pOrderBy, indentChar, GLGroupBL.GetGLGroupList(true, true, AccOrderByEnum.Name, indentChar, dc));


                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    cObjList = (from c in dataContext.GetTable<dcGLAccount>()
                //                orderby c.GLAccountSLNo
                //                 select c).ToList();
                //}
            }


            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        //public static List<dcGLAccount> GetAccGLAccountListByGroupID(int pAccGLGroupID, bool includeChildAccount, bool orderByGroup, bool orderByName, string indentChar)
        //{
        //    return GetAccGLAccountListByGroupID(pAccGLGroupID, includeChildAccount, orderByGroup, orderByName, indentChar, null, null);
        //}

        //public static List<dcGLAccount> GetAccGLAccountListByGroupID(int pAccGLGroupID, bool includeChildAccount, bool orderByGroup, bool orderByName, string indentChar, List<dcGLAccount> pAccGLGroupList)
        //{
        //    return GetAccGLAccountListByGroupID(pAccGLGroupID, includeChildAccount, orderByGroup, orderByName, indentChar, pAccGLGroupList, null);
        //}

        public static List<dcGLAccount> GetGLAccountListByGroupID(int pGLGroupID,
                                                                    bool includeChildAccount,
                                                                    bool orderByGroup,
                                                                    AccOrderByEnum pOrderBy,
                                                                    string indentChar,
                                                                    DBContext dc)
        {
            //if (pAccGLGroupList == null)
            //{
            //    pAccGLGroupList = GetAccGLAccountList(false, orderByName, indentChar, dc);
            //}
            //List<dcAccGLGroup> groupList = BLLibrary.AccountingBL.AccGLGroupBL.GetAccGLGroupListByParentID(pAccGLGroupID, dc);
            List<dcGLAccount> pGLGroupList = GetGLAccountList(false, pOrderBy, indentChar, dc);


            List<dcGLAccount> cObjList = new List<dcGLAccount>();
            cObjList = pGLGroupList.Where(c => c.GLGroupID == pGLGroupID).OrderBy(c => c.GLAccountSLNo).ToList(); 
            return cObjList;
        }


        public static List<dcGLAccount> GetGLControlAccountList()
        {
            return GetGLControlAccountList(null);
        }

        public static List<dcGLAccount> GetGLControlAccountList(DBContext dc)
        {
            //if (pAccGLGroupList == null)
            //{
            //    pAccGLGroupList = GetAccGLAccountList(false, orderByName, indentChar, dc);
            //}
            //List<dcAccGLGroup> groupList = BLLibrary.AccountingBL.AccGLGroupBL.GetAccGLGroupListByParentID(pAccGLGroupID, dc);
            List<dcGLAccount> GLAccountList = GetGLAccountList(false, AccOrderByEnum.Name, string.Empty , dc);


            List<dcGLAccount> cObjList = new List<dcGLAccount>();
            cObjList = GLAccountList.Where(c => c.GLAccountTypeID == (int)GLAccountTypeEnum.ControlAccount).ToList();
            return cObjList;
        }


        public static List<dcGLAccount> GetPostableGLAccountList(int pCompanyID)
        {
            return GetPostableGLAccountList(pCompanyID, null);
        }

        public static List<dcGLAccount> GetPostableGLAccountList(int pCompanyID, DBContext dc)
        {
          
            List<dcGLAccount> GLAccountList = GetGLAccountList(pCompanyID, dc);

            List<dcGLAccount> cObjList = new List<dcGLAccount>();
            cObjList = GLAccountList.Where(c => c.CompanyID == pCompanyID &&
                                            c.GLAccountTypeID != (int)GLAccountTypeEnum.ControlAccount).ToList();
            return cObjList;
        }

        public static dcGLAccount GetGLAccountByID(int pCompanyID, int pGLAccountID)
        {
            return GetGLAccountByID(pCompanyID, pGLAccountID, null);
        }
        public static dcGLAccount GetGLAccountByID(int pCompanyID, int pGLAccountID, DBContext dc)
        {
            //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            DBCommandInfo cmdInfo = new DBCommandInfo();
            StringBuilder sb = new StringBuilder(GetGLAccountListString());

            sb.Append(" AND tblGLAccount.CompanyID=@companyID ");
            //cmd.Parameters.AddWithValue("@companyID", pCompanyID);
            cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);

            if (pGLAccountID > 0)
            {
                sb.Append(" AND tblGLAccount.GLAccountID=@glAccountID ");
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
            //dbq.DBQueryMode = PG.Core.DBBase.DBQueryModeEnum.DBCommand;
            //dbq.DBCommand = cmd;

            dcGLAccount cObj = GetGLAccountList(dbq, dc).FirstOrDefault();
            return cObj;
        }

        public static dcGLAccount GetGLAccountByCode(int pCompanyID,string pGLAccountCode)
        {
            return GetGLAccountByCode(pCompanyID, pGLAccountCode, null);
        }
        public static dcGLAccount GetGLAccountByCode(int pCompanyID, string pGLAccountCode, DBContext dc)
        {
            DBCommandInfo cmdInfo = new DBCommandInfo();
            //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            StringBuilder sb = new StringBuilder(GetGLAccountListString());

            sb.Append(" AND tblGLAccount.CompanyID=@companyID ");
            //cmd.Parameters.AddWithValue("@companyID", pCompanyID);
            cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);

            if (pGLAccountCode != string.Empty)
            {
                sb.Append(" AND tblGLAccount.GLAccountCode=@glAccountCode ");
                //cmd.Parameters.AddWithValue("@glAccountCode", pGLAccountCode);
                cmdInfo.DBParametersInfo.Add("@glAccountCode", pGLAccountCode);
            }

            DBQuery dbq = new DBQuery();
            dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
            cmdInfo.CommandText = sb.ToString();
            cmdInfo.CommandType = CommandType.Text;
            dbq.DBCommandInfo = cmdInfo;

            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = sb.ToString();

            //DBQuery dbq = new DBQuery();
            //dbq.DBQueryMode = PG.Core.DBBase.DBQueryModeEnum.DBCommand;
            //dbq.DBCommand = cmd;

            dcGLAccount cObj = GetGLAccountList(dbq, dc).FirstOrDefault();
            return cObj;

        }

        public static bool IsGLAccountIDExists(int pCompanyID, int pGLAccountID)
        {
            return IsGLAccountIDExists(pCompanyID, pGLAccountID, null);
        }
        public static bool IsGLAccountIDExists(int pCompanyID, int pGLAccountID, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetGLAccountListString());
                sb.Append(" AND tblGLAccount.CompanyID=@companyID AND  tblGLAccount.GLAccountID=@gLAccountID");
                cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);
                cmdInfo.DBParametersInfo.Add("@gLAccountID", pGLAccountID);
                

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
                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    var result = from cObj in dataContext.GetTable<dcGLAccount>()
                //                 where cObj.CompanyID == pCompanyID && cObj.GLAccountID == pGLAccountID
                //                 select cObj;
                //    isData = result.Count() > 0;
                //}
            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return isData;
        }


        public static bool IsGLAccountCodeExists(int pCompanyID, string pGLAccountCode)
        {
            return IsGLAccountCodeExists(pCompanyID,pGLAccountCode, null);
        }
        public static bool IsGLAccountCodeExists(int pCompanyID, string pGLAccountCode, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetGLAccountListString());
                sb.Append(" AND tblGLAccount.CompanyID=@companyID AND  tblGLAccount.GLAccountCode=@gLAccountCode");
                cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);
                cmdInfo.DBParametersInfo.Add("@gLAccountCode", pGLAccountCode);

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
                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    var result = from cObj in dataContext.GetTable<dcGLAccount>()
                //                 where cObj.CompanyID== pCompanyID && cObj.GLAccountCode == pGLAccountCode
                //                 select cObj;
                //    isData = result.Count() > 0;
                //}
            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return isData;
        }
        public static bool IsGLAccountCodeExists(int pCompanyID, string pGLAccountCode, int pGLAccountID)
        {
            return IsGLAccountCodeExists(pCompanyID,pGLAccountCode, pGLAccountID, null);
        }
        public static bool IsGLAccountCodeExists(int pCompanyID, string pGLAccountCode, int pGLAccountID, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetGLAccountListString());
                sb.Append(" AND tblGLAccount.CompanyID=@companyID AND  tblGLAccount.GLAccountCode=@gLAccountCode AND  tblGLAccount.GLAccountID <> @gLAccountID");
                cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);
                cmdInfo.DBParametersInfo.Add("@gLAccountCode", pGLAccountCode);
                cmdInfo.DBParametersInfo.Add("@gLAccountID", pGLAccountID);

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
                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    var result = from cObj in dataContext.GetTable<dcGLAccount>()
                //                 where cObj.CompanyID == pCompanyID && cObj.GLAccountID != pGLAccountID && cObj.GLAccountCode == pGLAccountCode
                //                 select cObj;
                //    isData = result.Count() > 0;
                //}
            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return isData;
        }




        public static bool IsGLAccountNameExists(int pCompanyID, string pGLAccountName)
        {
            return IsGLAccountNameExists(pCompanyID, pGLAccountName, null);
        }
        public static bool IsGLAccountNameExists(int pCompanyID, string pGLAccountName, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetGLAccountListString());
                sb.Append(" AND tblGLAccount.CompanyID=@companyID AND  tblGLAccount.GLAccountName=@gLAccountName");
                cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);
                cmdInfo.DBParametersInfo.Add("@gLAccountName", pGLAccountName);


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
        public static bool IsGLAccountNameExists(int pCompanyID, string pGLAccountName, int pGLAccountID)
        {
            return IsGLAccountNameExists(pCompanyID ,pGLAccountName, pGLAccountID, null);
        }
        public static bool IsGLAccountNameExists(int pCompanyID, string pGLAccountName, int pGLAccountID, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetGLAccountListString());
                sb.Append(" AND tblGLAccount.CompanyID=@companyID AND  tblGLAccount.GLAccountName=@gLAccountName AND  tblGLAccount.GLAccountID <> @gLAccountID");
                cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);
                cmdInfo.DBParametersInfo.Add("@gLAccountName", pGLAccountName);
                cmdInfo.DBParametersInfo.Add("@gLAccountID", pGLAccountID);

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


        public static bool IsGLAccountNameExistsNormalControl(int pCompanyID, string pGLAccountCode)
        {
            return IsGLAccountNameExistsNormalControl(pCompanyID, pGLAccountCode, null);
        }
        public static bool IsGLAccountNameExistsNormalControl(int pCompanyID, string pGLAccountCode, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetGLAccountListString());
                sb.Append(" AND tblGLAccount.CompanyID=@companyID AND  tblGLAccount.GLAccountCode=@gLAccountCode");

                sb.Append(string.Format(" AND (tblGLAccount.GLAccountTypeID = {0} OR tblGLAccount.GLAccountTypeID = {1} )", (int)GLAccountTypeEnum.NormalAccount, (int)GLAccountTypeEnum.ControlAccount));

                cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);
                cmdInfo.DBParametersInfo.Add("@gLAccountCode", pGLAccountCode);

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
        public static bool IsGLAccountNameExistsNormalControl(int pCompanyID, string pGLAccountCode, int pGLAccountID)
        {
            return IsGLAccountNameExistsNormalControl(pCompanyID, pGLAccountCode, pGLAccountID, null);
        }
        public static bool IsGLAccountNameExistsNormalControl(int pCompanyID, string pGLAccountCode, int pGLAccountID, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetGLAccountListString());
                sb.Append(" AND tblGLAccount.CompanyID=@companyID AND  tblGLAccount.GLAccountCode=@gLAccountCode AND  tblGLAccount.GLAccountID <> @gLAccountID");
                sb.Append(string.Format(" AND (tblGLAccount.GLAccountTypeID = {0} OR tblGLAccount.GLAccountTypeID = {1} )", (int)GLAccountTypeEnum.NormalAccount, (int)GLAccountTypeEnum.ControlAccount));


                cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);
                cmdInfo.DBParametersInfo.Add("@gLAccountCode", pGLAccountCode);
                cmdInfo.DBParametersInfo.Add("@gLAccountID", pGLAccountID);

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
                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    var result = from cObj in dataContext.GetTable<dcGLAccount>()
                //                 where cObj.CompanyID == pCompanyID && cObj.GLAccountID != pGLAccountID && cObj.GLAccountCode == pGLAccountCode
                //                 select cObj;
                //    isData = result.Count() > 0;
                //}
            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return isData;
        }


        public static bool IsGLAccountNameExistsSub(int pCompanyID, string pGLAccountCode, int pGLAccountIDParent)
        {
            return IsGLAccountNameExistsSub(pCompanyID, pGLAccountCode, pGLAccountIDParent, null);
        }
        public static bool IsGLAccountNameExistsSub(int pCompanyID, string pGLAccountCode, int pGLAccountIDParent, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetGLAccountListString());
                sb.Append(" AND tblGLAccount.CompanyID = @companyID AND  tblGLAccount.GLAccountCode = @gLAccountCode");
                sb.Append(" AND tblGLAccount.GLAccountIDParent = @glAccountIDParent ");
                sb.Append(string.Format(" AND (tblGLAccount.GLAccountTypeID = {0})", (int)GLAccountTypeEnum.SubAccount));

                cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);
                cmdInfo.DBParametersInfo.Add("@gLAccountCode", pGLAccountCode);
                cmdInfo.DBParametersInfo.Add("@glAccountIDParent", pGLAccountIDParent);

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
        public static bool IsGLAccountNameExistsSub(int pCompanyID, string pGLAccountCode, int pGLAccountIDParent, int pGLAccountID)
        {
            return IsGLAccountNameExistsSub(pCompanyID, pGLAccountCode, pGLAccountIDParent, pGLAccountID, null);
        }
        public static bool IsGLAccountNameExistsSub(int pCompanyID, string pGLAccountCode, int pGLAccountIDParent, int pGLAccountID, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetGLAccountListString());
                sb.Append(" AND tblGLAccount.CompanyID=@companyID AND  tblGLAccount.GLAccountCode=@gLAccountCode AND  tblGLAccount.GLAccountID <> @gLAccountID");
                sb.Append(" AND tblGLAccount.GLAccountIDParent = @glAccountIDParent ");
                sb.Append(string.Format(" AND (tblGLAccount.GLAccountTypeID = {0} OR tblGLAccount.GLAccountTypeID = {1} )", (int)GLAccountTypeEnum.SubAccount));

                cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);
                cmdInfo.DBParametersInfo.Add("@gLAccountCode", pGLAccountCode);
                cmdInfo.DBParametersInfo.Add("@gLAccountID", pGLAccountID);
                cmdInfo.DBParametersInfo.Add("@glAccountIDParent", pGLAccountIDParent);

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


        //Create Moni

        public static bool IsGLAccountNameExistsGroupName(int pCompanyID, string pGLAccountName)
        {
            return IsGLAccountNameExistsGroupName(pCompanyID, pGLAccountName, null);
        }
        public static bool IsGLAccountNameExistsGroupName(int pCompanyID, string pGLAccountName, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetGLAccountGroupNameListString());
                sb.Append(" AND tblGLGroup.CompanyID=@companyID AND  tblGLGroup.GLGroupName=@gLAccountName");
                cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);
                cmdInfo.DBParametersInfo.Add("@gLAccountName", pGLAccountName);


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
        //end 

        public static int GetGLAccountNoMax(int pCompanyID, int pGLGroupID, DBContext dc)
        {
            int maxNO = 0;

            //SqlCommand cmd = new SqlCommand();
            DBCommandInfo cmdInfo = new DBCommandInfo();
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT MAX(tblGLAccount.GLAccountNo) AS GLAccountNo ");

            sb.Append(" FROM tblGLAccount ");
            sb.Append(" WHERE (1=1) ");

            sb.Append(" AND tblGLAccount.CompanyID = @companyID");
            //cmd.Parameters.AddWithValue("@companyID", pCompanyID);
            cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);

            sb.Append(" AND tblGLAccount.GLGroupID = @pGLGroupID");
            //cmd.Parameters.AddWithValue("@pGLGroupID", pGLGroupID);
            cmdInfo.DBParametersInfo.Add("@pGLGroupID", pGLGroupID);

            cmdInfo.CommandType = CommandType.Text;
            cmdInfo.CommandText = sb.ToString();

            bool isDCInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            maxNO = Conversion.DBNullIntToZero(dc.ExecuteScalar(cmdInfo));
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);


            return maxNO;

        }
        
        public static string GetNextGLAccountCode(int pCompanyID, int pGLGroupID)
        {
            return GetNextGLAccountCode(pCompanyID, pGLGroupID, null);
        }
        public static string GetNextGLAccountCode(int pCompanyID, int pGLGroupID,  DBContext dc)
        {
            string glAccCode = string.Empty;
            string strSLNo = string.Empty;

 
            dcAccSettings accSettings = AccSettingsBL.GetAccSettingByCompanyID(pCompanyID, dc);

            if (accSettings.AutoGLAccountCode)
            {
                string grpCode = string.Empty;
                if (accSettings.PrefixGLAccountCode)
                {
                    dcGLGroup grp = GLGroupBL.GetGLGroupByID(pCompanyID, pGLGroupID, dc);
                    grpCode = grp.GLGroupCode;
                }

                strSLNo = (GetGLAccountNoMax(pCompanyID, pGLGroupID, dc) + 1).ToString();

                if (accSettings.IsPadGLAccountCode)
                {
                    if (accSettings.GLAccountCodePadChar != string.Empty)
                    {
                        strSLNo = strSLNo.PadLeft(accSettings.GLAccountCodeLength, accSettings.GLAccountCodePadChar.ToCharArray()[0]);
                    }
                }
                glAccCode = grpCode + accSettings.GLAccountCodePrefixSep + strSLNo;
            }
            return glAccCode;
        }


        public static int GetGLAccountTypeID(int pCompanyID, int pGLAccountID)
        {
            return GetGLAccountTypeID(pCompanyID, pGLAccountID, null);
        }
        public static int GetGLAccountTypeID(int pCompanyID, int pGLAccountID, DBContext dc)
        {
            int glAccountTypeID = 0;

            dcGLAccount glAcc = GLAccountBL.GetGLAccountByID(pCompanyID, pGLAccountID, dc);

            if (glAcc != null)
            {
                glAccountTypeID = glAcc.GLAccountTypeID;
            }
            return glAccountTypeID;
        }



        public static int Insert(dcGLAccount cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcGLAccount cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcGLAccount>(cObj, true);
                if (id > 0) { cObj.GLAccountID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcGLAccount cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcGLAccount cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcGLAccount>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pGLAccountID)
        {
            return Delete(pGLAccountID, null);
        }
        public static bool Delete(int pGLAccountID, DBContext dc)
        {
            dcGLAccount cObj = new dcGLAccount();
            cObj.GLAccountID = pGLAccountID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcGLAccount>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcGLAccount cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcGLAccount cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcGLAccount cObj)
        {
            return Save(cObj, null);
        }
        public static int Save(dcGLAccount cObj, DBContext dc)
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
                            cObj.GLAccountNo = GetGLAccountNoMax(cObj.CompanyID, cObj.GLGroupID, dc) + 1 ;
                            newID = Insert(cObj, dc);
                            break;
                        case RecordStateEnum.Edited:
                            if (Update(cObj, dc))
                            {
                                newID = cObj.GLAccountID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.GLAccountID, dc))
                            {
                                newID = cObj.GLAccountID;
                            }
                            break;
                        default:
                            break;
                    }

                    if (newID > 0)
                    {
                        bool bStatus = false;

                        if (cObj.GLAccountHistory != null)
                        {
                            GLAccountHistoryBL.UpdateGLAccountHistory(cObj.CompanyID, cObj.GLAccountHistory.AccYearID
                                                                    , newID, cObj.GLAccountHistory.DebitAmtOpen, cObj.GLAccountHistory.CreditAmtOpen, dc);
                        }

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

        public static List<dcGLAccount> GetGLAccountListbyGroups(int pCompanyID, List<dcGLGroup> pGLGroupList)
        {
            return GetGLAccountListbyGroups(pCompanyID, pGLGroupList, 0, null);
        }
        public static List<dcGLAccount> GetGLAccountListbyGroups(int pCompanyID, List<dcGLGroup> pGLGroupList, DBContext dc)
        {
            return GetGLAccountListbyGroups(pCompanyID, pGLGroupList, 0, dc);
        }

        public static List<dcGLAccount> GetGLAccountListbyGroups(int pCompanyID, List<dcGLGroup> pGLGroupList, int pGLAccountTypeID)
        {
            return GetGLAccountListbyGroups(pCompanyID, pGLGroupList, pGLAccountTypeID, null);
        }
        public static List<dcGLAccount> GetGLAccountListbyGroups(int pCompanyID, List<dcGLGroup> pGLGroupList, int pGLAccountTypeID, DBContext dc)
        {
            List<dcGLAccount> cObjList = new List<dcGLAccount>();

            string strGrpList = string.Empty;
            string comma = "";

            if (pGLGroupList != null)
            {
                foreach (dcGLGroup grp in pGLGroupList)
                {
                    strGrpList += comma + grp.GLGroupID;
                    comma = ",";
                }
            }

            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetGLAccountListString());

                sb.Append(" AND tblGLAccount.CompanyID=@companyID ");
                //cmd.Parameters.AddWithValue("@companyID", pCompanyID);
                cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);

                if (strGrpList != string.Empty )
                {
                    sb.Append(string.Format(" AND tblGLAccount.GLGroupID IN ({0}) ", strGrpList));
                    //cmd.Parameters.Add("@grpidlist", SqlDbType.NVarChar);
                    //cmd.Parameters["@grpidlist"].Value = strGrpList;
                }

                if (pGLAccountTypeID > 0)
                {
                    sb.Append(" AND tblGLAccount.GLAccountTypeID=@gLAccountTypeID ");
                    //cmd.Parameters.AddWithValue("@gLAccountTypeID", pGLAccountTypeID);
                    cmdInfo.DBParametersInfo.Add("@gLAccountTypeID", pGLAccountTypeID);
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
                //dbq.DBQueryMode = PG.Core.DBBase.DBQueryModeEnum.DBCommand;
                //dbq.DBCommand = cmd;

                cObjList = GetGLAccountList(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }

            return cObjList;
        }

        public static List<dcGLAccount> GetGLAccountListbyGroups(int pCompanyID, List<dcGLGroup> pGLGroupList, GLAccountTypeFilterEnum pGLAccountTypeFilter, int pGLAccountID)
        {
            return GetGLAccountListbyGroups(pCompanyID, pGLGroupList, pGLAccountTypeFilter, pGLAccountID, null);
        }
        public static List<dcGLAccount> GetGLAccountListbyGroups(int pCompanyID, List<dcGLGroup> pGLGroupList, GLAccountTypeFilterEnum pGLAccountTypeFilter, int pGLAccountID, DBContext dc)
        {
            List<dcGLAccount> cObjList = new List<dcGLAccount>();

            string strGrpList = string.Empty;
            string comma = "";

            if (pGLGroupList != null)
            {
                foreach (dcGLGroup grp in pGLGroupList)
                {
                    strGrpList += comma + grp.GLGroupID;
                    comma = ",";
                }
            }

            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetGLAccountListString());

                sb.Append(" AND tblGLAccount.CompanyID=@companyID ");
                //cmd.Parameters.AddWithValue("@companyID", pCompanyID);
                cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);

                if (strGrpList != string.Empty)
                {
                    sb.Append(string.Format(" AND tblGLAccount.GLGroupID IN ({0}) ", strGrpList));
                    //cmd.Parameters.Add("@grpidlist", SqlDbType.NVarChar);
                    //cmd.Parameters["@grpidlist"].Value = strGrpList;
                }

                if (pGLAccountTypeFilter != GLAccountTypeFilterEnum.NoFilter
                        && pGLAccountTypeFilter != GLAccountTypeFilterEnum.AllAccount)
                {
                    string strTypes = AccHelper.GetGLAccountTypeIDFilterListString(pGLAccountTypeFilter);
                    sb.Append(string.Format(" AND tblGLAccount.GLAccountTypeID IN ({0}) ", strTypes));
                }

                
                if (pGLAccountID > 0)
                {
                    if (pGLAccountTypeFilter == GLAccountTypeFilterEnum.SubAccountByControl)
                    {
                        sb.Append(" AND tblGLAccount.GLAccountIDParent=@glAccountIDParent ");
                        //cmd.Parameters.AddWithValue("@glAccountIDParent", pGLAccountID);
                        cmdInfo.DBParametersInfo.Add("@glAccountIDParent", pGLAccountID);
                    }
                }



                //if (pGLAccountTypeID > 0)
                //{
                //    sb.Append(" AND tblGLAccount.GLAccountTypeID=@gLAccountTypeID ");
                //    cmd.Parameters.AddWithValue("@gLAccountTypeID", pGLAccountTypeID);
                //}

                sb.Append(" ORDER BY tblGLAccount.GLAccountCode, tblGLAccount.GLAccountName ");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                //cmd.CommandType = CommandType.Text;
                //cmd.CommandText = sb.ToString();

                //DBQuery dbq = new DBQuery();
                //dbq.DBQueryMode = PG.Core.DBBase.DBQueryModeEnum.DBCommand;
                //dbq.DBCommand = cmd;

                cObjList = GetGLAccountList(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }

            return cObjList;
        }

        public static List<dcGLAccount> FormatGLAccount(List<dcGLAccount> accList, bool isOrderByGroup, AccOrderByEnum pOrderBy, string indentChar, List<dcGLGroup> groupListFull)
        {
            List<dcGLAccount> newAccList = new List<dcGLAccount>();

            //List<dcGLGroup> groupList = GLGroupBL.FormatGLGroup(groupListFull, true, true, pOrderBy, indentChar, groupListFull);

            foreach (dcGLGroup glGroup in groupListFull)
            {
                List<dcGLAccount> grpAccList = accList.Where(c => c.GLGroupID == glGroup.GLGroupID).OrderBy(c=>c.GLAccountSLNo).ToList();


                switch (pOrderBy)
                {
                    case AccOrderByEnum.Code:
                        grpAccList = grpAccList.OrderBy(c => c.GLAccountCode).ToList();
                        break;
                    case AccOrderByEnum.Name:
                        grpAccList = grpAccList.OrderBy(c => c.GLAccountCode).ToList();
                        break;
                    case AccOrderByEnum.SLNo:
                        grpAccList = grpAccList.OrderBy(c => c.GLAccountSLNo).ToList();
                        break;
                }
                foreach (dcGLAccount acc in grpAccList)
                {
                    acc.GLClassID = glGroup.GLClassID;
                    acc.GLGroupName = glGroup.GLGroupName;
                    newAccList.Add(acc);
                }
            }


            //if (!isOrderByGroup)
            //{
            //    if (isOrderByName)
            //    {
            //        newAccList = newAccList.OrderBy(c => c.AccGLAccountName).ToList();
            //    }
            //    else
            //    {
            //        newAccList = newAccList.OrderBy(c => c.AccGLAccountSLNo).ToList();
            //    }
            //}


            return newAccList;
        }

        public static List<dcGLAccount> GetAccountBalance(clsPrmLedger prmLedger)
        {
            return GetAccountBalance(prmLedger, null, null);
        }

        public static List<dcGLAccount> GetAccountBalance(clsPrmLedger prmLedger, DBContext dc)
        {
            return GetAccountBalance(prmLedger, null, dc);
        }

        public static List<dcGLAccount> GetAccountBalance(clsPrmLedger prmLedger, List<dcGLGroup> pGLGroupList)
        {
            return GetAccountBalance(prmLedger, pGLGroupList, null);
        }

        public static List<dcGLAccount> GetAccountBalance(clsPrmLedger prmLedger,  List<dcGLGroup> pGLGroupList,  DBContext dc)
        {

            List<dcGLAccountHistory> accHistList = new List<dcGLAccountHistory>();
            List<dcJournalDet> transBeforeDate = new List<dcJournalDet>();
            List<dcJournalDet> transDateRange = new List<dcJournalDet>();


            if (prmLedger.IncludeOpBalanceType == InculdeOpBalanceEnum.IncludeALL
                | prmLedger.IncludeOpBalanceType == InculdeOpBalanceEnum.IncludeALLIndvidual
                | prmLedger.IncludeOpBalanceType == InculdeOpBalanceEnum.IncludeYear)
            {
                accHistList = GLAccountHistoryBL.GetGLAccountHistoryList(prmLedger.CompanyID, prmLedger.AccYearID, prmLedger.GLAccountID, prmLedger.GLAccountTypeFilter, pGLGroupList, dc);
            }

            //trans beforedate
            if (prmLedger.IncludeOpBalanceType == InculdeOpBalanceEnum.IncludeALL
                | prmLedger.IncludeOpBalanceType == InculdeOpBalanceEnum.IncludeALLIndvidual
                | prmLedger.IncludeOpBalanceType == InculdeOpBalanceEnum.IncludeDateRange)
            {
                if (prmLedger.FromDate.HasValue)
                {
                    clsPrmLedger prmLdgBfDate = (clsPrmLedger)prmLedger.Clone();
                    prmLdgBfDate.IsBeforeDate = true;
                    transBeforeDate = JournalDetBL.GetJournalDetSumByDate(prmLdgBfDate, pGLGroupList, dc);
                }
            }

            //trans between date
            transDateRange = JournalDetBL.GetJournalDetSumByDate(prmLedger, pGLGroupList, dc);

            List<dcGLAccount> accList = new List<dcGLAccount>();
            if (prmLedger.GLAccountTypeFilter == GLAccountTypeFilterEnum.SubAccountByControl)
            {
                accList = GetGLAccountListbyGroups(prmLedger.CompanyID, pGLGroupList, prmLedger.GLAccountTypeFilter, prmLedger.GLAccountID);
            }
            else
            {
                accList  = GetGLAccountListbyGroups(prmLedger.CompanyID, pGLGroupList, prmLedger.GLAccountTypeFilter,0);
            }
            

            decimal debitAmtYear = 0;
            decimal creditAmtYear = 0;

            decimal debitAmtBfDt = 0;
            decimal creditAmtBfDt = 0;


            decimal debitAmt = 0;
            decimal creditAmt = 0;


            decimal closeDebitAmt = 0;
            decimal closeCreditAmt = 0;


            //List<dcGLAccount> accNonControl = pGLGroupList.Where(c => c.GLAccountTypeID != (int)GLAccountTypeEnum.ControlAccount).ToList();
            //
            foreach (dcGLAccount acc in accList)
            {
                //beforedate + open
                debitAmtYear = 0;
                creditAmtYear = 0;
                
                debitAmtBfDt = 0;
                creditAmtBfDt = 0;
                debitAmt = 0;
                creditAmt = 0;


                dcGLAccountHistory accHist = accHistList.Where(c => c.GLAccountID == acc.GLAccountID).FirstOrDefault();
                if (accHist != null)
                {
                    debitAmtYear = accHist.DebitAmtOpen;
                    creditAmtYear = accHist.CreditAmtOpen;
                }

                dcJournalDet detByAcc = transBeforeDate.Where(c => c.GLAccountID == acc.GLAccountID).FirstOrDefault();
                if (detByAcc != null)
                {
                    debitAmtBfDt = detByAcc.DebitAmt;
                    creditAmtBfDt = detByAcc.CreditAmt;
                }

                //openning Year
                acc.OpenDebitAmtYear = debitAmtYear;
                acc.OpenCreditAmtYear = creditAmtYear;
                acc.OpenAmtYear = debitAmtYear - creditAmtYear;

                acc.OpenDebitBalanceAmtYear = acc.OpenAmtYear >= 0 ? Math.Abs(acc.OpenAmtYear) : 0;
                acc.OpenCreditBalanceAmtYear = acc.OpenAmtYear <= 0 ? Math.Abs(acc.OpenAmtYear) : 0;

                acc.OpenBalnceAmtYear= Math.Abs(acc.OpenAmtYear);
                acc.DrCrOpenYear = AccHelper.GetDrCrBalanceType(acc.OpenAmtYear, acc.BalanceType);
                acc.DrCrOpenTextYear = AccHelper.GetDrCrBalanceText(acc.OpenAmtYear, acc.BalanceType);

                //openning daterange
               
                acc.OpenDebitAmtDateRange = debitAmtBfDt;
                acc.OpenCreditAmtDateRange = creditAmtBfDt;
                acc.OpenAmtDateRange = debitAmtBfDt - creditAmtBfDt;

                acc.OpenDebitBalanceAmtDateRange = acc.OpenAmtDateRange >= 0 ? Math.Abs(acc.OpenAmtDateRange) : 0;
                acc.OpenCreditBalanceAmtDateRange = acc.OpenAmtDateRange <= 0 ? Math.Abs(acc.OpenAmtDateRange) : 0;
                acc.OpenBalanceAmtDateRange = Math.Abs(acc.OpenAmtDateRange);
                acc.DrCrOpenDateRange = AccHelper.GetDrCrBalanceType(acc.OpenAmtDateRange, acc.BalanceType);
                acc.DrCrOpenTextDateRange = AccHelper.GetDrCrBalanceText(acc.OpenAmtDateRange, acc.BalanceType);


                ///openning
                
                //acc.OpenAmt = acc.OpenAmt + debitAmtBfDt - creditAmtBfDt;

                //old

                //acc.OpenDebitAmt = debitAmtYear + debitAmtBfDt;
                //acc.OpenCreditAmt = creditAmtYear + creditAmtBfDt;
                //acc.OpenAmt = debitAmtYear - creditAmtYear + debitAmtBfDt - creditAmtBfDt;

                //New

                acc.OpenDebitAmt = debitAmtYear ;
                acc.OpenCreditAmt = creditAmtYear ;
                acc.OpenAmt = debitAmtYear - creditAmtYear ;
                //New End
                acc.OpenDebitBalanceAmt = acc.OpenAmt >= 0 ? Math.Abs(acc.OpenAmt) : 0;
                acc.OpenCreditBalanceAmt = acc.OpenAmt <= 0 ? Math.Abs(acc.OpenAmt) : 0;

                acc.OpenBalanceAmt = Math.Abs(acc.OpenAmt);
                acc.DrCrOpen = AccHelper.GetDrCrBalanceType(acc.OpenAmt, acc.BalanceType);
                acc.DrCrOpenText = AccHelper.GetDrCrBalanceText(acc.OpenAmt, acc.BalanceType);


                //in tran
                dcJournalDet detDateRange = transDateRange.Where(c => c.GLAccountID == acc.GLAccountID).FirstOrDefault();


                if (detDateRange != null)
                {
                    debitAmt = detDateRange.DebitAmt;
                    creditAmt = detDateRange.CreditAmt;
                }

                acc.DebitAmt = debitAmt;
                acc.CreditAmt = creditAmt;
                acc.TranAmt = debitAmt - creditAmt;

                acc.TranDebitBalanceAmt = acc.TranAmt >= 0 ? Math.Abs(acc.TranAmt) : 0;
                acc.TranCreditBalanceAmt = acc.TranAmt <= 0 ? Math.Abs(acc.TranAmt) : 0;

                acc.TranBalanceAmt = Math.Abs(acc.TranAmt);
                acc.DrCrTranBalance = AccHelper.GetDrCrBalanceType(acc.TranAmt, acc.BalanceType);
                acc.DrCrTranBalanceText = AccHelper.GetDrCrBalanceText(acc.TranAmt, acc.BalanceType);


                //closing
                //acc.CloseDebitAmt = acc.OpenDebitAmt + acc.DebitAmt;
                //acc.CloseCreditAmt = acc.OpenCreditAmt + acc.CreditAmt;
                //acc.CloseAmt = acc.OpenAmt + acc.DebitAmt - acc.CreditAmt;

                //New
                acc.CloseDebitAmt = acc.OpenDebitAmt + acc.DebitAmt + debitAmtBfDt;
                acc.CloseCreditAmt = acc.OpenCreditAmt + acc.CreditAmt + creditAmtBfDt;
                acc.CloseAmt = acc.OpenAmt + acc.DebitAmt - acc.CreditAmt + acc.OpenDebitAmtDateRange - acc.OpenCreditAmtDateRange;
                //New end

                acc.CloseDebitBalanceAmt = acc.CloseAmt >= 0 ? Math.Abs(acc.CloseAmt) : 0;
                acc.CloseCreditBalanceAmt = acc.CloseAmt <= 0 ? Math.Abs(acc.CloseAmt) : 0;

                acc.CloseBalanceAmt = Math.Abs(acc.CloseAmt);
                acc.DrCrCloseBalance = AccHelper.GetDrCrBalanceType(acc.CloseAmt, acc.BalanceType);
                acc.DrCrCloseBalanceText = AccHelper.GetDrCrBalanceText(acc.CloseAmt, acc.BalanceType);

                closeDebitAmt += acc.CloseDebitAmt;
                closeCreditAmt += acc.CloseCreditAmt;
                    
            }


            ////now control
            //List<dcGLAccount> accControl = AccList.Where(c => c.GLAccountTypeID == (int)GLAccountTypeEnum.ControlAccount).ToList();
            //foreach (dcGLAccount acc in accControl)
            //{
            //    List<dcGLAccount> accSubList = AccList.Where(c => c.GLAccountIDParent == acc.GLAccountID).ToList();

            //    decimal sOpenDebitAmtYear = 0;
            //    decimal sOpenCreditAmtYear = 0;

            //    decimal sOpenDebitAmtDateRange = 0;
            //    decimal sOpenCreditAmtDateRange = 0;

            //    decimal sOpenDebitAmt = 0;
            //    decimal sOpenCreditAmt = 0;

            //    decimal sTranDebitAmt = 0;
            //    decimal sTranCreditAmt = 0;

            //    decimal sCloseDebitAmt = 0;
            //    decimal sCloseCreditAmt = 0;


            //    foreach(dcGLAccount accSub in accSubList)
            //    {
            //        sOpenDebitAmtYear += accSub.OpenDebitAmtYear;
            //        sOpenCreditAmtYear += accSub.OpenCreditAmtYear;

            //        sOpenDebitAmtDateRange += accSub.OpenDebitAmtDateRange;
            //        sOpenCreditAmtDateRange += accSub.OpenDebitAmtDateRange;

            //        sOpenDebitAmt += sOpenDebitAmtYear + sOpenDebitAmtDateRange;
            //        sOpenCreditAmt += sOpenCreditAmtYear + sOpenCreditAmtDateRange;

            //        sTranDebitAmt += accSub.DebitAmt;
            //        sTranCreditAmt += accSub.CreditAmt;

            //        sCloseDebitAmt += sOpenDebitAmt + sTranDebitAmt;
            //        sCloseCreditAmt += sOpenCreditAmt + sTranCreditAmt;

            //    }


            //    //opening Year

            //    acc.OpenDebitAmtYear = sOpenDebitAmtYear;
            //    acc.OpenCreditAmtYear = sOpenCreditAmtYear;
            //    acc.OpenAmtYear = sOpenDebitAmtYear - sOpenCreditAmtYear;
            //    acc.OpenDebitBalanceAmtYear = acc.OpenAmtYear >= 0 ? Math.Abs(acc.OpenAmtYear) : 0;
            //    acc.OpenCreditBalanceAmtYear = acc.OpenAmtYear <= 0 ? Math.Abs(acc.OpenAmtYear) : 0;
            //    acc.OpenBalnceAmtYear = Math.Abs(acc.OpenAmtYear);
            //    acc.DrCrOpenYear = AccHelper.GetDrCrBalanceType(acc.OpenAmtYear, acc.BalanceType);
            //    acc.DrCrOpenTextYear = AccHelper.GetDrCrBalanceText(acc.OpenAmtYear, acc.BalanceType);


            //    //opening date range
            //    acc.OpenDebitAmtDateRange = sOpenDebitAmtDateRange;
            //    acc.OpenCreditAmtDateRange = sOpenCreditAmtDateRange;
            //    acc.OpenAmtDateRange = sOpenDebitAmtDateRange - sOpenCreditAmtDateRange;
            //    acc.OpenDebitBalanceAmtDateRange = acc.OpenAmtDateRange >= 0 ? Math.Abs(acc.OpenAmtDateRange) : 0;
            //    acc.OpenCreditBalanceAmtDateRange = acc.OpenAmtDateRange <= 0 ? Math.Abs(acc.OpenAmtDateRange) : 0;
            //    acc.OpenBalanceAmtDateRange = Math.Abs(acc.OpenAmtDateRange);
            //    acc.DrCrOpenDateRange = AccHelper.GetDrCrBalanceType(acc.OpenAmtDateRange, acc.BalanceType);
            //    acc.DrCrOpenTextDateRange = AccHelper.GetDrCrBalanceText(acc.OpenAmtDateRange, acc.BalanceType);


            //    //opening
            //    acc.OpenDebitAmt = sOpenDebitAmt;
            //    acc.OpenCreditAmt = sOpenCreditAmt;
            //    acc.OpenAmt = sOpenDebitAmt - sOpenCreditAmt;
            //    acc.OpenDebitBalanceAmt = acc.OpenAmt >= 0 ? Math.Abs(acc.OpenAmt) : 0;
            //    acc.OpenCreditBalanceAmt = acc.OpenAmt <= 0 ? Math.Abs(acc.OpenAmt) : 0;
            //    acc.OpenBalanceAmt = Math.Abs(acc.OpenAmt);
            //    acc.DrCrOpen = AccHelper.GetDrCrBalanceType(acc.OpenAmt, acc.BalanceType);
            //    acc.DrCrOpenText = AccHelper.GetDrCrBalanceText(acc.OpenAmt, acc.BalanceType);



            //    ///tran
            //    acc.DebitAmt = sTranDebitAmt;
            //    acc.CreditAmt = sTranCreditAmt;
            //    acc.TranAmt = sTranDebitAmt - sTranCreditAmt;
            //    acc.TranDebitBalanceAmt = acc.TranAmt >= 0 ? Math.Abs(acc.TranAmt) : 0;
            //    acc.TranCreditBalanceAmt = acc.TranAmt <= 0 ? Math.Abs(acc.TranAmt) : 0;
            //    acc.TranBalanceAmt = Math.Abs(acc.TranAmt);
            //    acc.DrCrTranBalance = AccHelper.GetDrCrBalanceType(acc.TranAmt, acc.BalanceType);
            //    acc.DrCrTranBalanceText = AccHelper.GetDrCrBalanceText(acc.TranAmt, acc.BalanceType);


            //    //closing

            //    acc.CloseDebitAmt = sCloseDebitAmt;
            //    acc.CloseCreditAmt = sCloseCreditAmt;
            //    acc.CloseAmt = sCloseDebitAmt - sCloseCreditAmt;
            //    acc.CloseDebitBalanceAmt = acc.CloseAmt >= 0 ? Math.Abs(acc.CloseAmt) : 0;
            //    acc.CloseCreditBalanceAmt = acc.CloseAmt <= 0 ? Math.Abs(acc.CloseAmt) : 0;
            //    acc.CloseBalanceAmt = Math.Abs(acc.CloseAmt);
            //    acc.DrCrCloseBalance = AccHelper.GetDrCrBalanceType(acc.CloseAmt, acc.BalanceType);
            //    acc.DrCrCloseBalanceText = AccHelper.GetDrCrBalanceText(acc.CloseAmt, acc.BalanceType);
            //}


            return accList;
        }



        public static List<dcGLAccount> GetAccountTranCash(clsPrmLedger prmLedger, CashTranOption cashTranOpt)
        {
            return GetAccountTranCash(prmLedger, cashTranOpt, null);
        }

        public static List<dcGLAccount> GetAccountTranCash(clsPrmLedger prmLedger, CashTranOption cashTranOpt , DBContext dc)
        {
            List<dcGLAccount> cObjList = new List<dcGLAccount>();

            List<dcGLAccount> accList = GetGLAccountListAccountType(prmLedger.CompanyID, 0, -1
                                                                     , GLAccountTypeFilterEnum.NormalControlAccount, dc);
            List<dcJournalDet> transDateRange = JournalDetBL.GetJournalDetByDate_Cash(prmLedger, cashTranOpt, dc);


            foreach (dcGLAccount acc in accList)
            {
                dcJournalDet jDet = transDateRange.SingleOrDefault(c => c.GLAccountID == acc.GLAccountID);
                if (jDet != null)
                {
                    acc.DebitAmt = jDet.DebitAmt;
                    acc.CreditAmt = jDet.CreditAmt;
                    cObjList.Add(acc); 
                }
            }
            return cObjList;
        }


        //public static dcGLAccount GetGLGroupLedgerSummary(dcGLGroup pGLGroup, List<dcGLAccount> pAccBalanceList)
        //{
        //    return GetGLGroupLedgerSummary(pGLGroup, pAccBalanceList, false);
        //}

        //public static dcGLAccount GetGLGroupLedgerSummary(dcGLGroup pGLGroup, List<dcGLAccount> pAccBalanceList, bool includeZeroBalance)
        //{
        //    dcGLAccount acc = null;

        //    List<dcGLAccount> grpAccList = pAccBalanceList.Where(c => c.GLGroupID == pGLGroup.GLGroupID
        //                                                         && c.GLAccountTypeID != (int)GLAccountTypeEnum.SubAccount).ToList();


        //    int ldgCount = includeZeroBalance ? grpAccList.Count : grpAccList.Where(c => c.CloseAmt != 0).Count();

        //    if (ldgCount > 0)
        //    {
        //        acc = new dcGLAccount();

        //        acc.GLAccountID = 0;
        //        acc.GLAccountTypeID = (int)GLAccountTypeEnum.ControlAccount;
               
        //        acc.GLAccountCode = "";
        //        acc.GLAccountName = string.Format("Ledgers ({0})", ldgCount);
               
        //        acc.GLClassID = pGLGroup.GLClassID;
        //        //acc.GLClassName = pGLGroup.GLClassName;

        //        acc.GLGroupClassID = pGLGroup.GLGroupClassID;
        //        acc.GLGroupClassName = pGLGroup.GLGroupClassName;

        //        //acc.GLAccountIDParent = pGLGroup.GLGroupIDParent;
        //        //acc.GLGroupNameParent = pGLGroup.GLGroupNameParent;

        //        acc.GLGroupID = pGLGroup.GLGroupID;
        //        acc.GLGroupName = pGLGroup.GLGroupName;

        //        acc.GLAccountLevel = pGLGroup.GLGroupLevel;
        //        acc.ChildAccountCount = grpAccList.Count;


        //        decimal openAmtYear = grpAccList.Sum(c => c.OpenAmtYear);
        //        decimal openAmtDateRange = grpAccList.Sum(c => c.OpenAmtDateRange);
        //        decimal openAmt = grpAccList.Sum(c => c.OpenAmt);

        //        decimal debitAmt = grpAccList.Sum(c => c.DebitAmt);
        //        decimal creditAmt = grpAccList.Sum(c => c.CreditAmt);

        //        decimal closeAmt = grpAccList.Sum(c => c.CloseAmt);


        //        acc.OpenAmtYear = openAmtYear;
        //        acc.OpenDebitAmtYear = Math.Abs(acc.OpenAmtYear >= 0 ? acc.OpenAmtYear : 0);
        //        acc.OpenCreditAmtYear = Math.Abs(acc.OpenAmtYear < 0 ? acc.OpenAmtYear : 0);
        //        acc.OpenBalnceAmtYear = Math.Abs(openAmtYear);
        //        acc.DrCrOpenYear = AccHelper.GetDrCrBalanceType(acc.OpenAmtYear, acc.BalanceType);
        //        acc.DrCrOpenTextYear = AccHelper.GetDrCrBalanceText(acc.OpenAmtYear, acc.BalanceType);


        //        //opening daterange
        //        acc.OpenAmtDateRange = openAmtDateRange;
        //        acc.OpenDebitAmtDateRange = Math.Abs(acc.OpenAmtDateRange >= 0 ? acc.OpenAmtDateRange : 0);
        //        acc.OpenCreditAmtDateRange = Math.Abs(acc.OpenAmtDateRange < 0 ? acc.OpenAmtDateRange : 0);
        //        acc.OpenBalanceAmtDateRange = Math.Abs(openAmtDateRange);
        //        acc.DrCrOpenDateRange = AccHelper.GetDrCrBalanceType(acc.OpenAmtDateRange, acc.BalanceType);
        //        acc.DrCrOpenTextDateRange = AccHelper.GetDrCrBalanceText(acc.OpenAmtDateRange, acc.BalanceType);



        //        //opening
        //        acc.OpenAmt = openAmt;
        //        acc.OpenDebitAmt = Math.Abs(acc.OpenAmt >= 0 ? acc.OpenAmt : 0);
        //        acc.OpenCreditAmt = Math.Abs(acc.OpenAmt < 0 ? acc.OpenAmt : 0);
        //        acc.OpenBalanceAmt = Math.Abs(openAmt);
        //        acc.DrCrOpen = AccHelper.GetDrCrBalanceType(acc.OpenAmt, acc.BalanceType);
        //        acc.DrCrOpenText = AccHelper.GetDrCrBalanceText(acc.OpenAmt, acc.BalanceType);

        //        //
        //        acc.DebitAmt = debitAmt;
        //        acc.CreditAmt = creditAmt;

        //        //closing
        //        acc.CloseAmt = acc.OpenAmt + acc.DebitAmt - acc.CreditAmt;
        //        acc.CloseDebitAmt = Math.Abs(acc.CloseAmt >= 0 ? acc.CloseAmt : 0);
        //        acc.CloseCreditAmt = Math.Abs(acc.CloseAmt < 0 ? acc.CloseAmt : 0);
        //        acc.CloseBalanceAmt = Math.Abs(acc.CloseAmt);
        //        acc.DrCrCloseBalance = AccHelper.GetDrCrBalanceType(acc.CloseAmt, acc.BalanceType);
        //        acc.DrCrCloseBalanceText = AccHelper.GetDrCrBalanceText(acc.CloseAmt, acc.BalanceType); ;

        //    }
        //    return acc;
        //}

        public static bool IsAccountInGLGroup(int pCompanyID, int pGLAccountID, int pParentGLGroupID)
        {
            return IsAccountInGLGroup(pCompanyID,pGLAccountID, pParentGLGroupID, null, null);
        }

        public static bool IsAccountInGLGroup(int pCompanyID, int pGLAccountID, int pParentGLGroupID, List<dcGLGroup> pGLGroupList)
        {
            return IsAccountInGLGroup(pCompanyID, pGLAccountID, pParentGLGroupID, pGLGroupList, null);
        }

        public static bool IsAccountInGLGroup(int pCompanyID, int pGLAccountID, int pParentGLGroupID, List<dcGLGroup> pGLGroupList, DBContext dc)
        {
            bool bStatus = false;

            dcGLAccount acc = GetGLAccountByID(pCompanyID, pGLAccountID, dc);

            bStatus = pParentGLGroupID == acc.GLGroupID;
            if (bStatus == false)
            {
                bStatus = GLGroupBL.IsGLGroupInGLGroup(acc.GLGroupID, pParentGLGroupID, pGLGroupList, dc);
            }

            return bStatus;
        }


        public static void UpdateGLAccountClass(int pCompanyID)
        {
            UpdateGLAccountClass(pCompanyID, null);
        }

        public static void UpdateGLAccountClass(int pCompanyID, DBContext dc)
        {
            List<dcGLGroupClass> grpClassList = GLGroupClassBL.GetGLGroupClassList(dc);
            List<dcGLGroup> grpList = GLGroupBL.GetGLGroupList(pCompanyID, dc);


            /////Cash Acc
            //dcGLGroup grpCash = GLGroupBL.GetGLGroupList(pCompanyID, dc)
            //                        .Where(c => c.GLGroupClassID == (int)GLGroupClassEnum.CashInHand).SingleOrDefault();

            //if (grpCash != null)
            //{
            //    dcGLAccount accCash = GLAccountBL.GetGLAccountList(pCompanyID, dc)
            //                        .Where(c => c.GLGroupID == grpCash.GLGroupID).SingleOrDefault();

            //    if (accCash == null)
            //    {
            //        dcGLAccount accCashNew = new dcGLAccount();
            //        accCashNew.CompanyID = pCompanyID;
            //        accCashNew.GLClassID = grpCash.GLClassID;
            //        accCashNew.GLGroupClassID = grpCash.GLGroupClassID;
            //        accCashNew.GLGroupID = grpCash.GLGroupID;
            //        accCashNew.GLAccountIDParent = 0;
            //        accCashNew.GLAccountTypeID = (int)GLAccountTypeEnum.NormalAccount;
            //        accCashNew.IsSystem = true;
            //        accCashNew.GLAccountSLNo = 1;
            //        accCashNew.GLAccountCode = "120010";
            //        accCashNew.GLAccountName = "Cash";
            //        accCashNew.GLAccountNameSys = accCashNew.GLAccountName;
            //        accCashNew.GLAccountNamePrint = accCashNew.GLAccountName;
            //        GLAccountBL.Insert(accCashNew, dc);
            //    }
            //}




            ///PL Acc
            dcGLGroup grpPL = GLGroupBL.GetGLGroupList(pCompanyID, dc)
                                    .Where(c => c.GLGroupClassID == (int)GLGroupClassEnum.ProfitAndLoss).SingleOrDefault();

            if (grpPL != null)
            {
                dcGLAccount accPL = GLAccountBL.GetGLAccountList(pCompanyID, dc)
                                    .Where(c => c.GLGroupID == grpPL.GLGroupID).FirstOrDefault();

                if (accPL == null)
                {
                    dcGLAccount accPLNew = new dcGLAccount();
                    accPLNew.CompanyID = pCompanyID;
                    accPLNew.GLClassID = grpPL.GLClassID;
                    accPLNew.GLGroupClassID = grpPL.GLGroupClassID;
                    accPLNew.GLGroupID = grpPL.GLGroupID;
                    accPLNew.GLAccountIDParent = 0;
                    accPLNew.GLAccountTypeID = (int)GLAccountTypeEnum.NormalAccount;
                    accPLNew.IsSystem = true;
                    accPLNew.BalanceType = grpPL.BalanceType;

                    accPLNew.GLAccountSLNo = 1;
                    accPLNew.GLAccountCode = "59999";
                    accPLNew.GLAccountName = "Profit & Loss Account";
                    accPLNew.GLAccountNameSys = accPLNew.GLAccountName;
                    accPLNew.GLAccountNamePrint = accPLNew.GLAccountName;
                    GLAccountBL.Insert(accPLNew, dc);
                }
            }
        }

        //Create Moni
        public static string GetLocationGLAccountListString()
        {

            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT tblGLAccount.* ");
            sb.Append(", tblGLGroup.GLGroupCode, tblGLGroup.GLGroupName,  tblGLGroup.GLGroupNameShort,tblGLGroup.GLGroupSLNo, tblGLGroup.GLClassID, tblGLGroup.GLGroupClassID ");
            sb.Append(", tblGLAccount_1.GLAccountCode AS GLAccountCodeParent, tblGLAccount_1.GLAccountName AS GLAccountNameParent ");
            sb.Append(", tblGLAccountType.GLAccountTypeName ");
            sb.Append(", tblGLGroupClass.GLGroupClassName, ISNULL(tblGLGroupClass.IsInstrument,0) as IsInstrument ");
            sb.Append(", tblGLClass.GLClassName ");
            sb.Append(" FROM tblGLAccount ");
            sb.Append(" INNER JOIN tblGLGroup ON tblGLAccount.GLGroupID = tblGLGroup.GLGroupID ");
            sb.Append(" INNER JOIN tblGLClass ON tblGLGroup.GLClassID = tblGLClass.GLClassID ");
            sb.Append(" INNER JOIN tblGLAccountType ON tblGLAccount.GLAccountTypeID = tblGLAccountType.GLAccountTypeID ");
            sb.Append(" LEFT OUTER JOIN tblGLAccount tblGLAccount_1 ON tblGLAccount.GLAccountIDParent = tblGLAccount_1.GLAccountID ");
            sb.Append(" LEFT OUTER JOIN tblGLGroupClass ON tblGLGroup.GLGroupClassID = tblGLGroupClass.GLGroupClassID ");
            sb.Append(" LEFT OUTER JOIN tblLocationGLAccount ON tblGLAccount.GLAccountID=tblLocationGLAccount.GLAccountID "); //Add Location filter

            sb.Append(" WHERE (1=1) ");
            sb.Append(" AND tblGLAccount.GLAccountID not in (Select GLAccountID From tblLocationGLAccount) ");

            return sb.ToString();
        }


    }
}
