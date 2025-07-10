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
    /// AccRefBL
    /// Last update By Moni, Date 10-03-2015
    /// </summary>

    public class AccRefBL
    {
        //public static DataLoadOptions AccRefLoadOptions()
        //{
        //    DataLoadOptions dlo = new DataLoadOptions();
        //    //dlo.LoadWith<DBClass.PayRoll.SalaryPeriod>(obj => obj.PayRoll.SalaryPeriodType);
        //    //dlo.LoadWith<DBClass.dcPurchase>(obj => obj.Supplier);
        //    //dlo.LoadWith<DBClass.dcPurchase>(obj => obj.TranType);
        //    return dlo;
        //}

        public static string GetAccRefListString()
        {
            StringBuilder sb = new StringBuilder();
            //TODO Change this query as
            sb.Append("SELECT tblAccRef.* ");
            sb.Append(", tblAccRefCategory.AccRefCategoryCode, tblAccRefCategory.AccRefCategoryName ");
            sb.Append(", tblAccRefType.AccRefTypeCode, tblAccRefType.AccRefTypeName ");
            sb.Append(", tblAccRef_1.AccRefCode AS AccRefCodeParent, tblAccRef_1.AccRefName AS AccRefNameParent ");
            sb.Append(" FROM tblAccRef ");
            sb.Append(" INNER JOIN tblAccRefCategory ON tblAccRef.AccRefCategoryID = tblAccRefCategory.AccRefCategoryID ");
            sb.Append(" INNER JOIN tblAccRefType ON tblAccRefCategory.AccRefTypeID = tblAccRefType.AccRefTypeID ");
            sb.Append(" LEFT OUTER JOIN tblAccRef  tblAccRef_1 ON tblAccRef.AccRefIDParent = tblAccRef_1.AccRefID ");
            sb.Append(" LEFT OUTER JOIN tblLocationAccRef   ON tblAccRef.AccRefID = tblLocationAccRef.AccRefID ");

            sb.Append(" WHERE 1=1 ");

            return sb.ToString();
        }

        public static string GetLocationAccRefListString()
        {
            StringBuilder sb = new StringBuilder();
            //TODO Change this query as
            sb.Append("SELECT tblAccRef.* ");
            sb.Append(", tblAccRefCategory.AccRefCategoryCode, tblAccRefCategory.AccRefCategoryName ");
            sb.Append(", tblAccRefType.AccRefTypeCode, tblAccRefType.AccRefTypeName ");
            sb.Append(", tblAccRef_1.AccRefCode AS AccRefCodeParent, tblAccRef_1.AccRefName AS AccRefNameParent ");
            sb.Append(" FROM tblAccRef ");
            sb.Append(" INNER JOIN tblAccRefCategory ON tblAccRef.AccRefCategoryID = tblAccRefCategory.AccRefCategoryID ");
            sb.Append(" INNER JOIN tblAccRefType ON tblAccRefCategory.AccRefTypeID = tblAccRefType.AccRefTypeID ");
            sb.Append(" LEFT OUTER JOIN tblAccRef  tblAccRef_1 ON tblAccRef.AccRefIDParent = tblAccRef_1.AccRefID ");

            sb.Append(" WHERE 1=1 ");
            sb.Append(" AND tblAccRef.AccRefID not in (Select AccRefID From tblLocationAccRef) ");

            return sb.ToString();
        }

        public static List<dcAccRef> GetAccRefList(int pCompanyID, int pAccRefTypeID)
        {
            return GetAccRefList(pCompanyID, pAccRefTypeID, 0,0, null);
        }
        public static List<dcAccRef> GetAccRefList(int pCompanyID, int pAccRefTypeID, DBContext dc)
        {
            return GetAccRefList(pCompanyID, pAccRefTypeID, 0, 0, dc);
        }

        public static List<dcAccRef> GetAccRefList(int pCompanyID, int pAccRefTypeID, int pAccRefCategoryID)
        {
            return GetAccRefList(pCompanyID, pAccRefTypeID, pAccRefCategoryID, 0, null);
        }

        public static List<dcAccRef> GetAccRefList(int pCompanyID, int pAccRefTypeID, int pAccRefCategoryID, DBContext dc)
        {
            return GetAccRefList(pCompanyID, pAccRefTypeID, pAccRefCategoryID, 0, dc);
        }

        public static List<dcAccRef> GetAccRefList(int pCompanyID, int pAccRefTypeID, int pAccRefCategoryID, int pAccRefID)
        {
            return GetAccRefList(pCompanyID, pAccRefTypeID, pAccRefCategoryID, pAccRefID, null);
        }
        public static List<dcAccRef> GetAccRefList(int pCompanyID, int pAccRefTypeID, int pAccRefCategoryID, int pAccRefID, DBContext dc)
        {
            DBCommandInfo cmdInfo = new DBCommandInfo();
            //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            StringBuilder sb = new StringBuilder(GetAccRefListString());

            sb.Append(" AND tblAccRef.CompanyID=@companyID ");
            //cmd.Parameters.AddWithValue("@companyID", pCompanyID);
            cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);

            if (pAccRefTypeID > 0)
            {
                sb.Append(" AND tblAccRefCategory.AccRefTypeID=@pAccRefTypeID ");
                //cmd.Parameters.AddWithValue("@pAccRefTypeID", pAccRefTypeID);
                cmdInfo.DBParametersInfo.Add("@pAccRefTypeID", pAccRefTypeID);
            }

            if (pAccRefCategoryID > 0)
            {
                sb.Append(" AND tblAccRef.AccRefCategoryID=@accRefCategoryID ");
                //cmd.Parameters.AddWithValue("@accRefCategoryID", pAccRefCategoryID);
                cmdInfo.DBParametersInfo.Add("@accRefCategoryID", pAccRefCategoryID);
            }

            if (pAccRefID > 0)
            {
                sb.Append(" AND tblAccRef.AccRefID=@accRefID ");
                //cmd.Parameters.AddWithValue("@accRefID", pAccRefID);
                cmdInfo.DBParametersInfo.Add("@accRefID", pAccRefID);
            }


            sb.Append(" ORDER BY tblAccRef.AccRefCode, tblAccRef.AccRefName ");

           
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
            return GetAccRefList(dbq, dc);
         
        }

        public static List<dcAccRef> GetAccRefListByIDList(int pCompanyID, List<int> pIDList, DBContext dc)
        {
            DBCommandInfo cmdInfo = new DBCommandInfo();
            //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            StringBuilder sb = new StringBuilder(GetAccRefListString());

            sb.Append(" AND tblAccRef.CompanyID=@companyID ");
            //cmd.Parameters.AddWithValue("@companyID", pCompanyID);
            cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);

            string strParams = string.Empty;
            string paramName = string.Empty;
            string comma = string.Empty;

            int idx = 0;
            if (pIDList.Count > 0)
            {
                foreach(int refID in pIDList)
                {
                    paramName = "@arrrefid_" + idx.ToString();
                    strParams += comma + paramName;
                    cmdInfo.DBParametersInfo.Add(paramName, refID);
                    idx++;
                    comma = ",";
                }
                sb.Append(string.Format(" AND tblAccRef.AccRefID IN ({0}) ", strParams));
                //cmdInfo.DBParametersInfo.Add("@accRefID", pAccRefID);
            }


            sb.Append(" ORDER BY tblAccRef.AccRefCode, tblAccRef.AccRefName ");

            DBQuery dbq = new DBQuery();
            dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
            cmdInfo.CommandText = sb.ToString();
            cmdInfo.CommandType = CommandType.Text;
            dbq.DBCommandInfo = cmdInfo;
            return GetAccRefList(dbq, dc);

        }



        public static List<dcAccRef> GetAccRefList(DBQuery dbq, DBContext dc)
        {
            List<dcAccRef> cObjList = new List<dcAccRef>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                if (dbq == null)
                {
                    dbq = new DBQuery();
                    //dbq.OrderBy = "PeriodStartDate";
                }
                cObjList = DBQuery.ExecuteDBQuery<dcAccRef>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static dcAccRef GetAccRefByID(int pCompanyID, int pAccRefID)
        {
            return GetAccRefByID(pCompanyID, pAccRefID, null);
        }
        public static dcAccRef GetAccRefByID(int pCompanyID, int pAccRefID, DBContext dc)
        {
            dcAccRef cObj = null;
            DBCommandInfo cmdInfo = new DBCommandInfo();
            //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            StringBuilder sb = new StringBuilder(GetAccRefListString());

            sb.Append(" AND tblAccRef.CompanyID=@companyID ");
            //cmd.Parameters.AddWithValue("@companyID", pCompanyID);
            cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);


            if (pAccRefID > 0)
            {
                sb.Append(" AND tblAccRef.AccRefID=@accRefID ");
                //cmd.Parameters.AddWithValue("@accRefID", pAccRefID);
                cmdInfo.DBParametersInfo.Add("@accRefID", pAccRefID);
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


            cObj = GetAccRefList(dbq, dc).FirstOrDefault();

           
            return cObj;
        }

        public static int Insert(dcAccRef cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcAccRef cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcAccRef>(cObj, true);
                if (id > 0) { cObj.AccRefID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcAccRef cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcAccRef cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcAccRef>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pAccRefID)
        {
            return Delete(pAccRefID, null);
        }
        public static bool Delete(int pAccRefID, DBContext dc)
        {
            dcAccRef cObj = new dcAccRef();
            cObj.AccRefID = pAccRefID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcAccRef>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool IsAccRefCodeExists(int pCompanyID, string pAccRefCode, int pAccRefTypeID)
        {
            return IsAccRefCodeExists(pCompanyID, pAccRefCode, pAccRefTypeID, null);
        }
        public static bool IsAccRefCodeExists(int pCompanyID, string pAccRefCode, int pAccRefTypeID, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                StringBuilder sb = new StringBuilder(GetAccRefListString());

                sb.Append(" AND tblAccRef.AccRefCode=@accRefCode ");
                //cmd.Parameters.AddWithValue("@accRefCode", pAccRefCode);
                cmdInfo.DBParametersInfo.Add("@accRefCode", pAccRefCode);

                sb.Append(" AND tblAccRefCategory.AccRefTypeID=@accRefTypeID ");
                //cmd.Parameters.AddWithValue("@accRefTypeID", pAccRefTypeID);
                cmdInfo.DBParametersInfo.Add("@accRefTypeID", pAccRefTypeID);

                sb.Append(" AND tblAccRefCategory.CompanyID=@companyID ");
                //cmd.Parameters.AddWithValue("@companyID", pCompanyID);
                cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);
                
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

                isData = GetAccRefList(dbq, dc).Count > 0;

            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return isData;
        }
        public static bool IsAccRefCodeExists(int pCompanyID, string pAccRefCode, int pAccRefTypeID,int pAccRefID)
        {
            return IsAccRefCodeExists(pCompanyID, pAccRefCode, pAccRefTypeID, pAccRefID, null);
        }
        public static bool IsAccRefCodeExists(int pCompanyID, string pAccRefCode, int pAccRefTypeID,int pAccRefID, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetAccRefListString());

                sb.Append(" AND tblAccRef.AccRefCode=@accRefCode ");
                //cmd.Parameters.AddWithValue("@accRefCode", pAccRefCode);
                cmdInfo.DBParametersInfo.Add("@accRefCode", pAccRefCode);

                sb.Append(" AND tblAccRefCategory.AccRefTypeID=@accRefTypeID ");
                //cmd.Parameters.AddWithValue("@accRefTypeID", pAccRefTypeID);
                cmdInfo.DBParametersInfo.Add("@accRefTypeID", pAccRefTypeID);


                sb.Append(" AND tblAccRefCategory.CompanyID=@companyID ");
                //cmd.Parameters.AddWithValue("@companyID", pCompanyID);
                cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);


                sb.Append(" AND tblAccRef.AccRefID <> @accRefID ");
                //cmd.Parameters.AddWithValue("@accRefID", pAccRefID);
                cmdInfo.DBParametersInfo.Add("@accRefID", pAccRefID);

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

                isData = GetAccRefList(dbq, dc).Count > 0;
            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return isData;
        }



        public static List<dcAccRef> GetGLTranTypeListHierarchy(int pCompanyID, int pAccRefTypeID, int pAccRefCategoryID)
        {
            return GetGLTranTypeListHierarchy(pCompanyID, pAccRefTypeID, pAccRefCategoryID, null);
        }

        public static List<dcAccRef> GetGLTranTypeListHierarchy(int pCompanyID, int pAccRefTypeID, int pAccRefCategoryID, DBContext dc)
        {
            List<dcAccRef> cObjList = GetAccRefList(pCompanyID, pAccRefTypeID,pAccRefCategoryID, 0, dc);
            return FormatGLTranTypeListHierarchy(cObjList);
        }

        public static List<dcAccRef> FormatGLTranTypeListHierarchy(List<dcAccRef> cObjList)
        {
            List<dcAccRef> groupListNew = new List<dcAccRef>();

            List<dcAccRef> rootList = cObjList.Where(c => c.AccRefIDParent == 0).OrderBy(c=>c.AccRefName).ToList();
            foreach (dcAccRef rootType in rootList)
            {
                groupListNew.Add(rootType);
                ProcessChildGroup(rootType, groupListNew, cObjList);
            }
            return groupListNew;
        }

        public static bool ProcessChildGroup(dcAccRef parentGroup, List<dcAccRef> groupListNew, List<dcAccRef> groupListFull)
        {
            List<dcAccRef> childGroupList = groupListFull.Where(c => c.AccRefIDParent == parentGroup.AccRefID).OrderBy(c => c.AccRefName).ToList();
            int count = 0;
            foreach (dcAccRef childGroup in childGroupList)
            {
                count++;
                groupListNew.Add(childGroup);
                ProcessChildGroup(childGroup, groupListNew, groupListFull);
            }
            return count > 0;
        }


        public static string GetAccRefULTreeText(int pAccRefCategoryID, int pParentID, string treeClassName, List<dcAccRef> cList)
        {
            StringBuilder sb = new StringBuilder();
            if (cList == null)
            {
                cList = GetAccRefList(0, 0, pAccRefCategoryID, 0, null);
            }

            sb.Append("<ul class='" + treeClassName + "'>");
            FillGLGroupStringRecursive(sb, pParentID, -1, cList);
            sb.Append("</ul>");

            return sb.ToString();
        }

        private static int FillGLGroupStringRecursive(StringBuilder pSbTree, int pParentID, int pLevel, List<dcAccRef> cList)
        {
            int cnt = 0;
            string sTab = "\t";
            string sNewLine = "\r\n";

            pLevel++;

            List<dcAccRef> pList = (from c in cList
                                     where c.AccRefIDParent == pParentID
                                     orderby c.AccRefName
                                     select c).ToList();

            foreach (dcAccRef grp in pList)
            {
                cnt++;
                int childCount = (from c in cList
                                  where c.AccRefIDParent == grp.AccRefID
                                  orderby c.AccRefName
                                  select c).Count();

                //string jsScript = "javascript:onGLGroupSelect(" + grp.AccGLGroupID.ToString() + ");";
                string strPrefix = PG.Core.Utility.Helper.RepeatString(sTab, pLevel);
                pSbTree.AppendLine();
                pSbTree.Append(strPrefix);

                //string jsClick = "onGLGroupSelect(" + grp.AccGLGroupID.ToString() + ");";

                //string attrID = "id=\"" + grp.GLTranTypeID.ToString() + "\"";
                string attrID = "";
                
                string attrGID = "gid=\"" + grp.AccRefID.ToString() + "\"";
                string attrGName = "gname=\"" + System.Web.HttpUtility.HtmlEncode(grp.AccRefName) + "\"";

                string attrChildCount = "childcount=\"" + childCount.ToString() + "\"";

                //data="url: 'http://code.google.com'"


                StringBuilder sbData = new StringBuilder();
                sbData.Append(string.Format("gid:'{0}'",grp.AccRefID.ToString()));
                sbData.Append(string.Format(",gname:'{0}'", System.Web.HttpUtility.HtmlEncode(grp.AccRefName)));


                string attrData = string.Format("data=\"{0}\"", sbData.ToString());



                string attrNodeClass = "class=\"group_node\"";
                //string attrNodeSpanClass = "class=\"group_nodeSpan\"";

                //data="key: '3', isFolder: true"
                //with checkbox
                //pSbTree.Append("<li " + attrLiID + " " + attrData + "><div style=\"height:20px;width:200px\"> <input type=\"checkbox\" /><span class=\"tree_Node\" " + attrID + " " + attrChildCount + " onclick= " + jsClick + " >").Append(grp.AccGLGroupName).Append("</span></div>");


                pSbTree.Append("<li " + attrID + " " + attrData + " " + attrGID + " " + attrGName + " " + attrChildCount + " " + attrNodeClass + ">");
                pSbTree.Append(grp.AccRefName);
                //pSbTree.Append("<span " + attrNodeSpanClass + ">").Append(grp.GLTranTypeName).Append("</span>");


                //pSbTree.Append("<span " + attrGID + " " + attrNodeSpanClass + ">").Append(grp.AccGLGroupName).Append("</span>");

                //remember the positon for insert <ul> tab if it has child node
                int ulPos = pSbTree.Length;

                //Recurisive call
                int totChild = FillGLGroupStringRecursive(pSbTree, grp.AccRefID, pLevel, cList);
                if (totChild > 0)
                {
                    string ulTag = sNewLine + strPrefix + "<ul>" + sNewLine;
                    pSbTree.Insert(ulPos, ulTag);
                    pSbTree.AppendLine();
                    pSbTree.Append(strPrefix + "</ul>");
                    pSbTree.AppendLine();

                }
                pSbTree.Append("</li>");

            }
            return cnt;
        }



        public static List<dcAccRef> GetAccRefBalance(clsPrmLedger prmLedger, List<dcAccRefCategory> pAccRefCategoryList)
        {
            return GetAccRefBalance(prmLedger, pAccRefCategoryList, null);
        }

        public static List<dcAccRef> GetAccRefBalance(clsPrmLedger prmLedger, List<dcAccRefCategory> pAccRefCategoryList, DBContext dc)
        {

            List<dcGLAccountHistoryRef> accHistRefList = new List<dcGLAccountHistoryRef>();
            List<dcJournalDetRef> transBeforeDate = new List<dcJournalDetRef>();
            List<dcJournalDetRef> transDateRange = new List<dcJournalDetRef>();


            if (prmLedger.IncludeOpBalanceType == InculdeOpBalanceEnum.IncludeALL
                | prmLedger.IncludeOpBalanceType == InculdeOpBalanceEnum.IncludeALLIndvidual
                | prmLedger.IncludeOpBalanceType == InculdeOpBalanceEnum.IncludeYear)
            {
                accHistRefList = GLAccountHistoryRefBL.GetGLAccountHistoryRefSumByAccRefList(prmLedger.CompanyID, prmLedger.AccYearID, (int)prmLedger.AccRefType, prmLedger.AccRefCategoryID, prmLedger.AccRefID, prmLedger.GLAccountID, dc);
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
                    transBeforeDate = JournalDetRefBL.GetJournalDetRefSumByDate(prmLdgBfDate, dc);
                }
            }

            //trans between date
            transDateRange = JournalDetRefBL.GetJournalDetRefSumByDate(prmLedger, dc);

            List<int> refIDList = accHistRefList.Select(c => c.AccRefID).ToList();
            refIDList.AddRange(transBeforeDate.Select(c => c.AccRefID).ToList());
            refIDList.AddRange(transDateRange.Select(c => c.AccRefID).ToList());

            //New
           


                List<dcAccRef> accRefList=new List<dcAccRef>();
            if(refIDList.Count!=0)
            { 
                accRefList = GetAccRefListByIDList(prmLedger.CompanyID, refIDList, dc);
            }
                //accRefList = GetAccRefList(prmLedger.CompanyID, (int)prmLedger.AccRefType, prmLedger.AccRefCategoryID, prmLedger.AccRefID);//, prmLedger.GLAccountID

                //if (prmLedger.GLAccountTypeFilter == GLAccountTypeFilterEnum.SubAccountByControl)
                //{
                //    accRefList = GetGLAccountListbyGroups(prmLedger.CompanyID, pGLGroupList, prmLedger.GLAccountTypeFilter, prmLedger.GLAccountID);
                //}
                //else
                //{
                //    accRefList = GetGLAccountListbyGroups(prmLedger.CompanyID, pGLGroupList, prmLedger.GLAccountTypeFilter, 0);
                //}
               

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
                    foreach (dcAccRef accRef in accRefList)
                    {
                        //beforedate + open
                        debitAmtYear = 0;
                        creditAmtYear = 0;

                        debitAmtBfDt = 0;
                        creditAmtBfDt = 0;
                        debitAmt = 0;
                        creditAmt = 0;


                        dcGLAccountHistoryRef accHistRef = accHistRefList.Where(c => c.AccRefID == accRef.AccRefID).FirstOrDefault();
                        if (accHistRef != null)
                        {
                            debitAmtYear = accHistRef.DebitAmtOpen;
                            creditAmtYear = accHistRef.CreditAmtOpen;
                        }

                        dcJournalDetRef detByRef = transBeforeDate.Where(c => c.AccRefID == accRef.AccRefID).FirstOrDefault();
                        if (detByRef != null)
                        {
                            debitAmtBfDt = detByRef.DebitAmt;
                            creditAmtBfDt = detByRef.CreditAmt;
                        }

                        //openning Year
                        accRef.OpenDebitAmtYear = debitAmtYear;
                        accRef.OpenCreditAmtYear = creditAmtYear;
                        accRef.OpenAmtYear = debitAmtYear - creditAmtYear;

                        accRef.OpenDebitBalanceAmtYear = accRef.OpenAmtYear >= 0 ? Math.Abs(accRef.OpenAmtYear) : 0;
                        accRef.OpenCreditBalanceAmtYear = accRef.OpenAmtYear <= 0 ? Math.Abs(accRef.OpenAmtYear) : 0;

                        accRef.OpenBalnceAmtYear = Math.Abs(accRef.OpenAmtYear);
                        //accRef.DrCrOpenYear = AccHelper.GetDrCrBalanceType(accRef.OpenAmtYear, accRef.BalanceType);
                        //accRef.DrCrOpenTextYear = AccHelper.GetDrCrBalanceText(accRef.OpenAmtYear, accRef.BalanceType);

                        //openning daterange

                        accRef.OpenDebitAmtDateRange = debitAmtBfDt;
                        accRef.OpenCreditAmtDateRange = creditAmtBfDt;
                        accRef.OpenAmtDateRange = debitAmtBfDt - creditAmtBfDt;

                        accRef.OpenDebitBalanceAmtDateRange = accRef.OpenAmtDateRange >= 0 ? Math.Abs(accRef.OpenAmtDateRange) : 0;
                        accRef.OpenCreditBalanceAmtDateRange = accRef.OpenAmtDateRange <= 0 ? Math.Abs(accRef.OpenAmtDateRange) : 0;
                        accRef.OpenBalanceAmtDateRange = Math.Abs(accRef.OpenAmtDateRange);
                        //accRef.DrCrOpenDateRange = AccHelper.GetDrCrBalanceType(accRef.OpenAmtDateRange, accRef.BalanceType);
                        //accRef.DrCrOpenTextDateRange = AccHelper.GetDrCrBalanceText(accRef.OpenAmtDateRange, accRef.BalanceType);


                        ///openning

                        //acc.OpenAmt = acc.OpenAmt + debitAmtBfDt - creditAmtBfDt;
                        //old code
                        //accRef.OpenDebitAmt = debitAmtYear + debitAmtBfDt;
                        //accRef.OpenCreditAmt = creditAmtYear + creditAmtBfDt;
                        //accRef.OpenAmt = debitAmtYear - creditAmtYear + debitAmtBfDt - creditAmtBfDt;

                        //New code

                        accRef.OpenDebitAmt = debitAmtYear;
                        accRef.OpenCreditAmt = creditAmtYear;
                        accRef.OpenAmt = debitAmtYear - creditAmtYear;

                        accRef.OpenDebitBalanceAmt = accRef.OpenAmt >= 0 ? Math.Abs(accRef.OpenAmt) : 0;
                        accRef.OpenCreditBalanceAmt = accRef.OpenAmt <= 0 ? Math.Abs(accRef.OpenAmt) : 0;

                        accRef.OpenBalanceAmt = Math.Abs(accRef.OpenAmt);
                        //accRef.DrCrOpen = AccHelper.GetDrCrBalanceType(accRef.OpenAmt, accRef.BalanceType);
                        accRef.DrCrOpenText = AccHelper.GetDrCrBalanceText(accRef.OpenAmt);


                        //in tran
                        dcJournalDetRef detDateRange = transDateRange.Where(c => c.AccRefID == accRef.AccRefID).FirstOrDefault();


                        if (detDateRange != null)
                        {
                            debitAmt = detDateRange.DebitAmt;
                            creditAmt = detDateRange.CreditAmt;
                        }

                        accRef.DebitAmt = debitAmt;
                        accRef.CreditAmt = creditAmt;
                        accRef.TranAmt = debitAmt - creditAmt;

                        accRef.TranDebitBalanceAmt = accRef.TranAmt >= 0 ? Math.Abs(accRef.TranAmt) : 0;
                        accRef.TranCreditBalanceAmt = accRef.TranAmt <= 0 ? Math.Abs(accRef.TranAmt) : 0;

                        accRef.TranBalanceAmt = Math.Abs(accRef.TranAmt);
                        //accRef.DrCrTranBalance = AccHelper.GetDrCrBalanceType(accRef.TranAmt, accRef.BalanceType);
                        accRef.DrCrTranBalanceText = AccHelper.GetDrCrBalanceText(accRef.TranAmt);


                        //closing
                        //Old

                        //accRef.CloseDebitAmt = accRef.OpenDebitAmt + accRef.DebitAmt;
                        //accRef.CloseCreditAmt = accRef.OpenCreditAmt + accRef.CreditAmt;
                        //accRef.CloseAmt = accRef.OpenAmt + accRef.DebitAmt - accRef.CreditAmt;
                        //New
                        accRef.CloseDebitAmt = accRef.OpenDebitAmt + accRef.DebitAmt + debitAmtBfDt;
                        accRef.CloseCreditAmt = accRef.OpenCreditAmt + accRef.CreditAmt + creditAmtBfDt;
                        accRef.CloseAmt = accRef.OpenAmt + accRef.DebitAmt - accRef.CreditAmt + accRef.OpenDebitAmtDateRange - accRef.OpenCreditAmtDateRange;
                        //New end
                        accRef.CloseDebitBalanceAmt = accRef.CloseAmt >= 0 ? Math.Abs(accRef.CloseAmt) : 0;
                        accRef.CloseCreditBalanceAmt = accRef.CloseAmt <= 0 ? Math.Abs(accRef.CloseAmt) : 0;

                        accRef.CloseBalanceAmt = Math.Abs(accRef.CloseAmt);
                        //accRef.DrCrCloseBalance = AccHelper.GetDrCrBalanceType(accRef.CloseAmt, accRef.BalanceType);
                        accRef.DrCrCloseBalanceText = AccHelper.GetDrCrBalanceText(accRef.CloseAmt);

                        closeDebitAmt += accRef.CloseDebitAmt;
                        closeCreditAmt += accRef.CloseCreditAmt;

                    }
                

                return accRefList;
            
        }

       

    }
}
