using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Data.Linq;
using PG.Core.DBBase;
using PG.Core.Extentions;
using PG.Core.DBFilters;
using PG.DBClass.AccountingDC;
using PG.DBClass.AccountingDC.AccEnums;
using PG.DBClass.AccountingDC.GeneralLedgerDC;

namespace PG.BLLibrary.AccountingBL.GeneralLedgerBL
{
	public class GLAccountHistoryRefBL
	{
		/// <summary>
		/// GeneralLedgerBL
		/// Last update By Moni, Date 15-03-2015
		/// </summary>
		//public static DataLoadOptions GLAccountHistoryLoadOptions()
		//{
		//    DataLoadOptions dlo = new DataLoadOptions();
		//    //dlo.LoadWith<DBClass.PayRoll.SalaryPeriod>(obj => obj.PayRoll.SalaryPeriodType);
		//    //dlo.LoadWith<DBClass.dcPurchase>(obj => obj.Supplier);
		//    //dlo.LoadWith<DBClass.dcPurchase>(obj => obj.TranType);
		//    return dlo;
		//}
	   
		//public static List<dcGLAccountHistoryRef> GLAccountHistoryRefList(int pCompanyID, int pAccYearID)
		//{
		//    return GLAccountHistoryRefList(pCompanyID, pAccYearID, null);
		//}
		//public static List<dcGLAccountHistoryRef> GLAccountHistoryRefList(int pCompanyID, int pAccYearID, DBContext dc)
		//{
		//    List<dcGLAccountHistoryRef> cObjList = new List<dcGLAccountHistoryRef>();
		//    List<dcGLAccount> cAccList = new List<dcGLAccount>();
		//    List<dcGLAccountHistoryRef> cTranList = new List<dcGLAccountHistoryRef>();
		//    bool isDCInit = false;
		//    try
		//    {
		//        isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
		//        cAccList = GLAccountBL.GetGLAccountList(pCompanyID, dc);

		//        using (DataContext dataContext = dc.NewDataContext())
		//        {
		//           cTranList = (from c in dataContext.GetTable<dcGLAccountHistoryRef>()
		//                         where c.AccYearID == pAccYearID && c.CompanyID == pCompanyID
		//                         select c).ToList();
		//        }
		//    }
		//    catch { throw; }
		//    finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }

		//    //////////////now build hist list

		//    foreach (dcGLAccount acc in cAccList)
		//    {
		//        if (acc.GLAccountTypeID == (int)GLAccountTypeEnum.ControlAccount)
		//        {
		//            continue;
		//        }

		//        dcGLAccountHistoryRef accOpen = new dcGLAccountHistoryRef();

		//        //accOpen.GLAccountCode = acc.GLAccountCode;
		//        //accOpen.GLAccountID = acc.GLAccountID;
		//        //accOpen.GLAccountName = acc.GLAccountName;
		//        //accOpen.GLGroupName = acc.GLGroupName;

		//        //accOpen.GLAccountTypeName = acc.GLAccountTypeName;

		//        accOpen.AccYearID = pAccYearID;
		//        accOpen.CompanyID = pCompanyID;


		//        dcGLAccountHistoryRef opnTran = cTranList.SingleOrDefault(c=>c.GLAccountID == acc.GLAccountID);
		//        if (opnTran != null)
		//        {
		//            accOpen.DebitAmtOpen = opnTran.DebitAmtOpen;
		//            accOpen.CreditAmtOpen = opnTran.CreditAmtOpen;
		//        }

		//        cObjList.Add(accOpen);

		//    }
		//    return cObjList;
		//}



		public static List<dcGLAccountHistoryRef> GetGLAccountHistoryListALL(int pCompanyID, int pAccYearID)
		{
			return GetGLAccountHistoryListALL(pCompanyID, pAccYearID, null, null);
		}
		public static List<dcGLAccountHistoryRef> GetGLAccountHistoryListALL(int pCompanyID, int pAccYearID, DBContext dc)
		{
			return GetGLAccountHistoryListALL(pCompanyID, pAccYearID, null, dc);
		}

		public static List<dcGLAccountHistoryRef> GetGLAccountHistoryListALL(int pCompanyID, int pAccYearID, List<dcGLAccount> pGLAccList)
		{
			return GetGLAccountHistoryListALL(pCompanyID, pAccYearID, pGLAccList, null);
		}
		public static List<dcGLAccountHistoryRef> GetGLAccountHistoryListALL(int pCompanyID, int pAccYearID, List<dcGLAccount> pGLAccList, DBContext dc)
		{
			List<dcGLAccountHistoryRef> cObjList = new List<dcGLAccountHistoryRef>();

			bool isDCInit = false;
			try
			{
				isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

				//SqlCommand cmd = new SqlCommand();
				DBCommandInfo cmdInfo = new DBCommandInfo();
				StringBuilder sb = new StringBuilder();

				sb.Append(" SELECT tblGLAccountHistory.*, tblGLAccount.GLAccountTypeID, tblGLAccountType.GLAccountTypeName ");
				sb.Append(" FROM tblGLAccountHistory ");
				sb.Append(" INNER JOIN tblGLAccount ON tblGLAccountHistory.GLAccountID = tblGLAccount.GLAccountID ");
				sb.Append(" INNER JOIN tblGLAccountType ON tblGLAccount.GLAccountTypeID = tblGLAccountType.GLAccountTypeID ");

				sb.Append(" WHERE 1=1 ");

				sb.Append(" AND tblGLAccount.CompanyID=@companyID ");
				//cmd.Parameters.AddWithValue("@companyID", pCompanyID);
				cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);

				sb.Append(" AND tblGLAccountHistory.AccYearID =@accYearID ");
				//cmd.Parameters.AddWithValue("@accYearID", pAccYearID);
				cmdInfo.DBParametersInfo.Add("@accYearID", pAccYearID);


				if (pGLAccList != null)
				{
					string strAccList = string.Empty;
					string comma = "";

					foreach (dcGLAccount acc in pGLAccList)
					{
						strAccList += comma + acc.GLAccountID;
						comma = ",";
					}

					if (strAccList != string.Empty)
					{
						sb.Append(string.Format(" AND tblGLAccountHistory.GLAccountID IN ({0}) ", strAccList));
						
						//sb.Append(" AND tblGLAccountHistory.GLAccountID IN (@accidlist) ");
						//cmd.Parameters.AddWithValue("@accidlist", strAccList);
					}
				}
				sb.Append(" ORDER BY tblGLAccount.GLAccountCode ");

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

				cObjList = DBQuery.ExecuteDBQuery<dcGLAccountHistoryRef>(dbq, dc);
			}
			catch { throw; }
			finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
			return cObjList;
		}


		public static List<dcGLAccountHistoryRef> GetGLAccountHistoryRefList(int pCompanyID, int pAccYearID, int pGLAccountID)
		{
			return GetGLAccountHistoryRefList(pCompanyID, pAccYearID, pGLAccountID, 0, 0,null);
		}
		public static List<dcGLAccountHistoryRef> GetGLAccountHistoryRefList(int pCompanyID, int pAccYearID, int pGLAccountID, DBContext dc)
		{
			return GetGLAccountHistoryRefList(pCompanyID, pAccYearID, pGLAccountID, 0, 0, dc);
		}


		public static List<dcGLAccountHistoryRef> GetGLAccountHistoryRefList(int pCompanyID, int pAccYearID, int pGLAccountID, int pAccRefTypeID)
		{
			return GetGLAccountHistoryRefList(pCompanyID, pAccYearID, pGLAccountID, pAccRefTypeID, 0, null);
		}
		public static List<dcGLAccountHistoryRef> GetGLAccountHistoryRefList(int pCompanyID, int pAccYearID, int pGLAccountID, int pAccRefTypeID, DBContext dc)
		{
			return GetGLAccountHistoryRefList(pCompanyID, pAccYearID, pGLAccountID, pAccRefTypeID, 0, dc);
		}


		public static List<dcGLAccountHistoryRef> GetGLAccountHistoryRefList(int pCompanyID, int pAccYearID, int pGLAccountID, int pAccRefTypeID, int pAccRefCategoryID)
		{
			return GetGLAccountHistoryRefList(pCompanyID, pAccYearID, pGLAccountID, pAccRefTypeID, pAccRefCategoryID, null);
		}
		public static List<dcGLAccountHistoryRef> GetGLAccountHistoryRefList(int pCompanyID, int pAccYearID, int pGLAccountID
																				, int pAccRefTypeID, int pAccRefCategoryID, DBContext dc)
		{
			List<dcGLAccountHistoryRef> cObjList = new List<dcGLAccountHistoryRef>();

			bool isDCInit = false;
			try
			{
				isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

				//SqlCommand cmd = new SqlCommand();

				DBCommandInfo cmdInfo = new DBCommandInfo();
				StringBuilder sb = new StringBuilder();

				sb.Append(" SELECT tblGLAccountHistoryRef.* ");
				sb.Append(" , tblGLAccount.GLAccountCode, tblGLAccount.GLAccountName ");
				sb.Append(" , tblGLAccount.GLAccountTypeID, tblGLAccountType.GLAccountTypeName ");
				sb.Append(" , tblGLAccount.GLAccountIDParent, tblGLAccount.GLGroupID ");
			   

				sb.Append(", tblAccRef.AccRefCode, tblAccRef.AccRefName ");
				sb.Append(", tblAccRefCategory.AccRefCategoryID, tblAccRefCategory.AccRefCategoryCode, tblAccRefCategory.AccRefCategoryName ");
				sb.Append(", tblAccRefType.AccRefTypeID, tblAccRefType.AccRefTypeCode, tblAccRefType.AccRefTypeName ");

				sb.Append(" FROM tblGLAccountHistoryRef ");

				sb.Append(" INNER JOIN tblGLAccount ON tblGLAccountHistoryRef.GLAccountID = tblGLAccount.GLAccountID ");
				sb.Append(" INNER JOIN tblGLAccountType ON tblGLAccount.GLAccountTypeID = tblGLAccountType.GLAccountTypeID ");

				sb.Append(" INNER JOIN tblAccRef ON tblGLAccountHistoryRef.AccRefID = tblAccRef.AccRefID ");
				sb.Append(" INNER JOIN tblAccRefCategory ON tblAccRef.AccRefCategoryID = tblAccRefCategory.AccRefCategoryID ");
				sb.Append(" INNER JOIN tblAccRefType ON tblAccRefCategory.AccRefTypeID = tblAccRefType.AccRefTypeID ");

				sb.Append(" WHERE 1=1 ");

				sb.Append(" AND tblGLAccountHistoryRef.CompanyID=@companyID ");
				//cmd.Parameters.AddWithValue("@companyID", pCompanyID);
				cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);

				if (pAccYearID > 0)
				{
					sb.Append(" AND tblGLAccountHistoryRef.AccYearID =@accYearID ");
					//cmd.Parameters.AddWithValue("@accYearID", pAccYearID);
					cmdInfo.DBParametersInfo.Add("@accYearID", pAccYearID);
				}

				if (pGLAccountID > 0)
				{
					sb.Append(" AND tblGLAccountHistoryRef.GLAccountID =@gLAccountID ");
					//cmd.Parameters.AddWithValue("@gLAccountID", pGLAccountID);
					cmdInfo.DBParametersInfo.Add("@gLAccountID", pGLAccountID);
				}

				if (pAccRefTypeID > 0)
				{
					sb.Append(" AND tblAccRefCategory.AccRefTypeID =@accRefTypeID ");
					//cmd.Parameters.AddWithValue("@accRefTypeID", pAccRefTypeID);
					cmdInfo.DBParametersInfo.Add("@accRefTypeID", pAccRefTypeID);
				}

				if (pAccRefCategoryID > 0)
				{
					sb.Append(" AND tblAccRef.AccRefCategoryID =@accRefCategoryID ");
					//cmd.Parameters.AddWithValue("@accRefCategoryID", pAccRefCategoryID);
					cmdInfo.DBParametersInfo.Add("@accRefCategoryID", pAccRefCategoryID);
				}
			 
				sb.Append(" ORDER BY tblGLAccount.GLAccountCode, tblGLAccount.GLAccountName ");
				sb.Append(" ,tblAccRefType.AccRefTypeSLNo, tblAccRefCategory.AccRefCategoryCode, tblAccRef.AccRefCode ");


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

				cObjList = DBQuery.ExecuteDBQuery<dcGLAccountHistoryRef>(dbq, dc);
			}
			catch { throw; }
			finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
			return cObjList;
		}


		public static List<dcGLAccountHistoryRef> GetGLAccountHistoryRefList_Control(int pCompanyID, int pAccYearID, int pGLAccountID
																						, int pAccRefTypeID, int pAccRefCategoryID, int pAccRefID)
		{
			return GetGLAccountHistoryRefList_Control(pCompanyID, pAccYearID, pGLAccountID, pAccRefTypeID, pAccRefCategoryID, pAccRefID, null);
		}
		public static List<dcGLAccountHistoryRef> GetGLAccountHistoryRefList_Control(int pCompanyID, int pAccYearID, int pGLAccountID
																				, int pAccRefTypeID, int pAccRefCategoryID, int pAccRefID, DBContext dc)
		{
			List<dcGLAccountHistoryRef> cObjList = new List<dcGLAccountHistoryRef>();

			bool isDCInit = false;
			try
			{
				isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

				//SqlCommand cmd = new SqlCommand();
				DBCommandInfo cmdInfo = new DBCommandInfo();
				StringBuilder sb = new StringBuilder();

				sb.Append(" SELECT tblGLAccount.GLAccountID ");
				sb.Append(" , tblGLAccount.GLAccountCode, tblGLAccount.GLAccountName ");
				sb.Append(" , tblGLAccount.GLAccountTypeID, tblGLAccountType.GLAccountTypeName ");
				sb.Append(" , tblGLAccount.GLAccountIDParent, tblGLAccount.GLGroupID ");
				sb.Append(" , tblGLAccount.GLGroupID, tblGLGroup.GLGroupName ");

				sb.Append(", HistoryRefSum.AccRefTypeID, tblAccRefType.AccRefTypeCode, tblAccRefType.AccRefTypeName ");
				sb.Append(", HistoryRefSum.AccRefCategoryID, tblAccRefCategory.AccRefCategoryCode, tblAccRefCategory.AccRefCategoryName ");
				sb.Append(", HistoryRefSum.AccRefID, tblAccRef.AccRefCode, tblAccRef.AccRefName ");

				sb.Append(", tblAccRefCategory.AccRefCategoryID, tblAccRefCategory.AccRefCategoryCode, tblAccRefCategory.AccRefCategoryName ");
				sb.Append(", tblAccRefType.AccRefTypeID, tblAccRefType.AccRefTypeCode, tblAccRefType.AccRefTypeName ");

				sb.Append(", HistoryRefSum.DebitAmtOpen ");
				sb.Append(", HistoryRefSum.CreditAmtOpen ");
				sb.Append(", HistoryRefSum.OpenAmt ");
				   

				sb.Append(" FROM tblGLAccount ");
				sb.Append(" INNER JOIN ( ");
				sb.Append("  SELECT tblGLAccount.GLAccountIDParent AS GLAccountID ");
				sb.Append(" , tblAccRefCategory.AccRefTypeID ");
				sb.Append(" , tblAccRef.AccRefCategoryID ");
				sb.Append(" , tblGLAccountHistoryRef.AccRefID ");

				sb.Append(" , ISNULL(SUM(tblGLAccountHistoryRef.DebitAmtOpen),0) AS DebitAmtOpen ");
				sb.Append(" , ISNULL(SUM(tblGLAccountHistoryRef.CreditAmtOpen),0) AS CreditAmtOpen ");
				sb.Append(" , ISNULL(SUM(tblGLAccountHistoryRef.OpenAmt),0) AS OpenAmt ");


				sb.Append(" FROM tblGLAccountHistoryRef ");
				sb.Append(" INNER JOIN tblGLAccount ON tblGLAccountHistoryRef.GLAccountID = tblGLAccount.GLAccountID  ");
				sb.Append(" INNER JOIN tblGLGroup ON tblGLAccount.GLGroupID = tblGLGroup.GLGroupID ");
				sb.Append(" INNER JOIN tblAccRef ON tblGLAccountHistoryRef.AccRefID = tblAccRef.AccRefID ");
				sb.Append(" INNER JOIN tblAccRefCategory ON tblAccRef.AccRefCategoryID = tblAccRefCategory.AccRefCategoryID ");

				sb.Append(" WHERE 1=1 ");

				sb.Append(" AND tblGLAccountHistoryRef.CompanyID=@companyID ");
				//cmd.Parameters.AddWithValue("@companyID", pCompanyID);
				cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);

				if (pAccYearID > 0)
				{
					sb.Append(" AND tblGLAccountHistoryRef.AccYearID =@accYearID ");
					//cmd.Parameters.AddWithValue("@accYearID", pAccYearID);
					cmdInfo.DBParametersInfo.Add("@accYearID", pAccYearID);
				}

				sb.Append(" AND tblGLAccount.GLAccountTypeID=@glAccountTypeID ");
				//cmd.Parameters.AddWithValue("@glAccountTypeID", (int)GLAccountTypeEnum.SubAccount);
				cmdInfo.DBParametersInfo.Add("@glAccountTypeID", (int)GLAccountTypeEnum.SubAccount);

				if (pGLAccountID > 0)
				{
					sb.Append(" AND tblGLAccount.GLAccountIDParent =@gLAccountID ");
					//cmd.Parameters.AddWithValue("@gLAccountID", pGLAccountID);
					cmdInfo.DBParametersInfo.Add("@gLAccountID", pGLAccountID);
				}
				
				if (pAccRefTypeID > 0)
				{
					sb.Append(" AND tblAccRefCategory.AccRefTypeID =@accRefTypeID ");
					//cmd.Parameters.AddWithValue("@accRefTypeID", pAccRefTypeID);
					cmdInfo.DBParametersInfo.Add("@accRefTypeID", pAccRefTypeID);
				}

				sb.Append(" GROUP BY tblGLAccount.GLAccountIDParent ");
				sb.Append(" ,tblAccRefCategory.AccRefTypeID "); 
				sb.Append(" ,tblAccRef.AccRefCategoryID ");
				sb.Append(" ,tblGLAccountHistoryRef.AccRefID ");

				sb.Append(")  HistoryRefSum ON tblGLAccount.GLAccountID = HistoryRefSum.GLAccountID ");

				//sb.Append(" INNER JOIN tblGLAccount ON tblGLAccountHistoryRef.GLAccountID = tblGLAccount.GLAccountID ");
				sb.Append(" INNER JOIN tblGLAccountType ON tblGLAccount.GLAccountTypeID = tblGLAccountType.GLAccountTypeID ");
				sb.Append(" INNER JOIN tblGLGroup ON tblGLAccount.GLGroupID = tblGLGroup.GLGroupID ");
				sb.Append(" INNER JOIN tblAccRef ON HistoryRefSum.AccRefID = tblAccRef.AccRefID ");
				sb.Append(" INNER JOIN tblAccRefCategory ON tblAccRef.AccRefCategoryID = tblAccRefCategory.AccRefCategoryID ");
				sb.Append(" INNER JOIN tblAccRefType ON tblAccRefCategory.AccRefTypeID = tblAccRefType.AccRefTypeID ");

				sb.Append(" WHERE 1=1 ");

				sb.Append(" ORDER BY tblGLAccount.GLAccountCode, tblGLAccount.GLAccountName ");
				sb.Append(" ,tblAccRefType.AccRefTypeSLNo, tblAccRefCategory.AccRefCategoryCode, tblAccRef.AccRefCode ");

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

				cObjList = DBQuery.ExecuteDBQuery<dcGLAccountHistoryRef>(dbq, dc);

			}
			catch { throw; }
			finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
			return cObjList;
		}



		public static dcGLAccountHistoryRef GetGLAccountHistoryRefByID(int pCompanyID, int pAccYearID, int pGLAccountID, int pAccRefTypeID)
		{
			return GetGLAccountHistoryRefByID(pCompanyID, pAccYearID, pGLAccountID, pAccRefTypeID, null);
		}
		public static dcGLAccountHistoryRef GetGLAccountHistoryRefByID(int pCompanyID, int pAccYearID, int pGLAccountID, int pAccRefTypeID, DBContext dc)
		{
			dcGLAccountHistoryRef cObj = null;
			bool isDCInit = false;
			try
			{
				isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

				DBCommandInfo cmdInfo = new DBCommandInfo();
				//SqlCommand cmd = new SqlCommand();
				StringBuilder sb = new StringBuilder();

				sb.Append("SELECT * from tblGLAccountHistoryRef WHERE (1=1) ");

				sb.Append(" AND tblGLAccountHistoryRef.AccYearID=@accYearID ");
				//cmd.Parameters.AddWithValue("@accYearID", pAccYearID);
				cmdInfo.DBParametersInfo.Add("@accYearID", pAccYearID);

				if (pAccYearID > 0)
				{
					sb.Append(" AND tblGLAccountHistory.pCompanyID=@pCompanyID ");
					//cmd.Parameters.AddWithValue("@pCompanyID", pCompanyID);
					cmdInfo.DBParametersInfo.Add("@pCompanyID", pCompanyID);
				}


				if (pGLAccountID > 0)
				{
					sb.Append(" AND tblGLAccountHistory.GLAccountID=@glAccountID ");
					//cmd.Parameters.AddWithValue("@glAccountID", pGLAccountID);
					cmdInfo.DBParametersInfo.Add("@glAccountID", pGLAccountID);
				}

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

				List<dcGLAccountHistoryRef> cObjList = DBQuery.ExecuteDBQuery<dcGLAccountHistoryRef>(dbq, dc);

				if (cObjList.Count > 0)
				{
					cObj = cObjList.First();
				}


			}
			catch { throw; }
			finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
			return cObj;
		}


		public static dcGLAccountHistoryRef GetGLAccountHistoryRefSumByAccRefID(int pCompanyID, int pAccYearID, int pAccRefTypeID, int pAccRefCategoryID, int pAccRefID, int pGLAccountID)
		{
			return GetGLAccountHistoryRefSumByAccRefID(pCompanyID, pAccYearID, pAccRefTypeID, pAccRefCategoryID, pAccRefID, pGLAccountID, null);
		}
		public static dcGLAccountHistoryRef GetGLAccountHistoryRefSumByAccRefID(int pCompanyID, int pAccYearID, int pAccRefTypeID, int pAccRefCategoryID, int pAccRefID, int pGLAccountID, DBContext dc)
		{
			dcGLAccountHistoryRef cObj = null;
			bool isDCInit = false;
			try
			{
				isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

				int glAccountTypeID = GLAccountBL.GetGLAccountTypeID(pCompanyID, pGLAccountID, dc);

				//SqlCommand cmd = new SqlCommand();
				DBCommandInfo cmdInfo = new DBCommandInfo();
				StringBuilder sb = new StringBuilder();


				sb.Append("SELECT tblGLAccountHistoryRef.AccRefID  ");
				sb.Append(", SUM(tblGLAccountHistoryRef.DebitAmtOpen) AS DebitAmtOpen ");
				sb.Append(", SUM(CreditAmtOpen) AS CreditAmtOpen ");
				sb.Append(", SUM(OpenAmt) AS OpenAmt ");

				sb.Append(" FROM tblGLAccountHistoryRef ");
				sb.Append(" INNER JOIN tblAccRef ON tblGLAccountHistoryRef.AccRefID = tblAccRef.AccRefID ");
				sb.Append(" INNER JOIN tblAccRefCategory ON tblAccRef.AccRefCategoryID = tblAccRefCategory.AccRefCategoryID ");
				sb.Append(" INNER JOIN tblGLAccount ON tblGLAccountHistoryRef.GLAccountID = tblGLAccount.GLAccountID ");

				sb.Append(" WHERE (1=1) ");

				sb.Append(" AND tblGLAccountHistoryRef.CompanyID=@companyID ");
				//cmd.Parameters.AddWithValue("@companyID", pCompanyID);
				cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);

				if (pAccRefTypeID > 0)
				{
					sb.Append(" AND tblAccRefCategory.AccRefTypeID=@accRefTypeID ");
					//cmd.Parameters.AddWithValue("@accRefTypeID", pAccRefTypeID);
					cmdInfo.DBParametersInfo.Add("@accRefTypeID", pAccRefTypeID);
				}

				if (pAccRefCategoryID > 0)
				{
					sb.Append(" AND tblAccRef.AccRefCategoryID=@accRefCategoryID ");
					//cmd.Parameters.AddWithValue("@accRefCategoryID", pAccRefCategoryID);
					cmdInfo.DBParametersInfo.Add("@accRefCategoryID", pAccRefCategoryID);
				}

				if (pAccRefID > 0)
				{
					sb.Append(" AND tblGLAccountHistoryRef.accRefID=@accRefID ");
					//cmd.Parameters.AddWithValue("@accRefID", pAccRefID);
					cmdInfo.DBParametersInfo.Add("@accRefID", pAccRefID);
				}

				if (pGLAccountID > 0)
				{
					if (glAccountTypeID == (int)GLAccountTypeEnum.ControlAccount)
					{
						sb.Append(" AND tblGLAccount.GLAccountIDParent=@glAccountID ");
					}
					else
					{
						sb.Append(" AND tblGLAccountHistoryRef.GLAccountID=@glAccountID ");
					}
					//cmd.Parameters.AddWithValue("@glAccountID", pGLAccountID);
					cmdInfo.DBParametersInfo.Add("@glAccountID", pGLAccountID);
				}

				sb.Append(" GROUP BY tblGLAccountHistoryRef.AccRefID ");

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

				List<dcGLAccountHistoryRef> cObjList = DBQuery.ExecuteDBQuery<dcGLAccountHistoryRef>(dbq, dc);

				if (cObjList.Count > 0)
				{
					cObj = cObjList.First();
				}


			}
			catch { throw; }
			finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
			return cObj;
		}




		public static dcGLAccountHistoryRef GetGLAccountHistoryByID_Control(int pGLAccountID, int pAccYearID)
		{
			return GetGLAccountHistoryByID_Control(pGLAccountID, pAccYearID, null);
		}
		public static dcGLAccountHistoryRef GetGLAccountHistoryByID_Control(int pGLAccountID, int pAccYearID, DBContext dc)
		{
			dcGLAccountHistoryRef cObj = null;
			bool isDCInit = false;
			try
			{
				isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

				DBCommandInfo cmdInfo = new DBCommandInfo();
				//SqlCommand cmd = new SqlCommand();
				StringBuilder sb = new StringBuilder();


				sb.Append(" SELECT tblGLAccount.CompanyID, tblGLAccount.GLAccountIDParent AS GLAccountID ");
				sb.Append(" , SUM(tblGLAccountHistory.DebitAmtOpen) AS DebitAmtOpen ");
				sb.Append(" , SUM(tblGLAccountHistory.CreditAmtOpen) AS CreditAmtOpen ");
				sb.Append(" FROM tblGLAccountHistory ");
				sb.Append(" LEFT OUTER JOIN tblGLAccount ON tblGLAccountHistory.GLAccountID = tblGLAccount.GLAccountID ");
				
				sb.Append(" WHERE 1=1 ");

				sb.Append(" AND tblGLAccount.GLAccountIDParent=@pGLAccountID ");
				//cmd.Parameters.AddWithValue("@pGLAccountID", pGLAccountID);
				cmdInfo.DBParametersInfo.Add("@pGLAccountID", pGLAccountID);

				sb.Append(" AND tblGLAccountHistory.AccYearID =@accYearID ");
				//cmd.Parameters.AddWithValue("@accYearID", pAccYearID);
				cmdInfo.DBParametersInfo.Add("@accYearID", pAccYearID);

				sb.Append(" GROUP BY tblGLAccount.CompanyID, tblGLAccount.GLAccountIDParent ");

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

				List<dcGLAccountHistoryRef> cObjList = DBQuery.ExecuteDBQuery<dcGLAccountHistoryRef>(dbq, dc);

				dcGLAccountHistoryRef cObjD = cObjList.FirstOrDefault();
				if (cObjD != null)
				{
					cObj = new dcGLAccountHistoryRef();
					cObj.CompanyID = cObjD.CompanyID;
					cObj.GLAccountID = cObjD.GLAccountID;
					cObj.AccYearID = cObjD.AccYearID;

					decimal tranAmt = cObjD.DebitAmtOpen - cObjD.CreditAmtOpen;

					if (tranAmt < 0)
					{
						cObj.CreditAmtOpen = Math.Abs(tranAmt);
						cObj.DebitAmtOpen = 0;
					}
					else
					{
						cObj.DebitAmtOpen = Math.Abs(tranAmt);
						cObj.CreditAmtOpen = 0;
					}
				}
			}
			catch { throw; }
			finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
			return cObj;
		}


		public static List<dcGLAccountHistoryRef> GetGLAccountHistoryRefList(int pCompanyID, int pAccYearID, int pGLAccountID
																					  , int pAccRefTypeID, int pAccRefCategoryID, int pAccRefID)
		{
			return GetGLAccountHistoryRefList(pCompanyID, pAccYearID, pGLAccountID, pAccRefTypeID, pAccRefCategoryID, pAccRefID, null);
		}
		public static List<dcGLAccountHistoryRef> GetGLAccountHistoryRefList(int pCompanyID, int pAccYearID, int pGLAccountID
																				, int pAccRefTypeID, int pAccRefCategoryID, int pAccRefID, DBContext dc)
		{
			List<dcGLAccountHistoryRef> cObjList = new List<dcGLAccountHistoryRef>();

			bool isDCInit = false;
			try
			{
				isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

				DBCommandInfo cmdInfo = new DBCommandInfo();
				//SqlCommand cmd = new SqlCommand();
				StringBuilder sb = new StringBuilder();

				sb.Append(" SELECT tblGLAccount.GLAccountID ");
				sb.Append(" , tblGLAccount.GLAccountCode, tblGLAccount.GLAccountName ");
				sb.Append(" , tblGLAccount.GLAccountTypeID, tblGLAccountType.GLAccountTypeName ");
				sb.Append(" , tblGLAccount.GLAccountIDParent, tblGLAccount.GLGroupID ");
				sb.Append(" , tblGLAccount.GLGroupID, tblGLGroup.GLGroupName ");

				sb.Append(", HistoryRefSum.AccRefTypeID, tblAccRefType.AccRefTypeCode, tblAccRefType.AccRefTypeName ");
				sb.Append(", HistoryRefSum.AccRefCategoryID, tblAccRefCategory.AccRefCategoryCode, tblAccRefCategory.AccRefCategoryName ");
				sb.Append(", HistoryRefSum.AccRefID, tblAccRef.AccRefCode, tblAccRef.AccRefName ");

				sb.Append(", tblAccRefCategory.AccRefCategoryID, tblAccRefCategory.AccRefCategoryCode, tblAccRefCategory.AccRefCategoryName ");
				sb.Append(", tblAccRefType.AccRefTypeID, tblAccRefType.AccRefTypeCode, tblAccRefType.AccRefTypeName ");

				sb.Append(", HistoryRefSum.DebitAmtOpen ");
				sb.Append(", HistoryRefSum.CreditAmtOpen ");
				sb.Append(", HistoryRefSum.OpenAmt ");


				sb.Append(" FROM tblGLAccount ");
				sb.Append(" INNER JOIN ( ");
				sb.Append("  SELECT tblGLAccount.GLAccountIDParent AS GLAccountID ");
				sb.Append(" , tblAccRefCategory.AccRefTypeID ");
				sb.Append(" , tblAccRef.AccRefCategoryID ");
				sb.Append(" , tblGLAccountHistoryRef.AccRefID ");

				sb.Append(" , ISNULL(SUM(tblGLAccountHistoryRef.DebitAmtOpen),0) AS DebitAmtOpen ");
				sb.Append(" , ISNULL(SUM(tblGLAccountHistoryRef.CreditAmtOpen),0) AS CreditAmtOpen ");
				sb.Append(" , ISNULL(SUM(tblGLAccountHistoryRef.OpenAmt),0) AS OpenAmt ");


				sb.Append(" FROM tblGLAccountHistoryRef ");
				sb.Append(" INNER JOIN tblGLAccount ON tblGLAccountHistoryRef.GLAccountID = tblGLAccount.GLAccountID  ");
				sb.Append(" INNER JOIN tblGLGroup ON tblGLAccount.GLGroupID = tblGLGroup.GLGroupID ");
				sb.Append(" INNER JOIN tblAccRef ON tblGLAccountHistoryRef.AccRefID = tblAccRef.AccRefID ");
				sb.Append(" INNER JOIN tblAccRefCategory ON tblAccRef.AccRefCategoryID = tblAccRefCategory.AccRefCategoryID ");

				sb.Append(" WHERE 1=1 ");

				sb.Append(" AND tblGLAccountHistoryRef.CompanyID=@companyID ");
				//cmd.Parameters.AddWithValue("@companyID", pCompanyID);
				cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);

				if (pAccYearID > 0)
				{
					sb.Append(" AND tblGLAccountHistoryRef.AccYearID =@accYearID ");
					//cmd.Parameters.AddWithValue("@accYearID", pAccYearID);
					cmdInfo.DBParametersInfo.Add("@accYearID", pAccYearID);
				}

				sb.Append(" AND tblGLAccount.GLAccountTypeID=@glAccountTypeID ");
				//cmd.Parameters.AddWithValue("@glAccountTypeID", (int)GLAccountTypeEnum.SubAccount);
				cmdInfo.DBParametersInfo.Add("@glAccountTypeID", (int)GLAccountTypeEnum.SubAccount);


				if (pGLAccountID > 0)
				{
					sb.Append(" AND tblGLAccount.GLAccountIDParent =@gLAccountID ");
					//cmd.Parameters.AddWithValue("@gLAccountID", pGLAccountID);
					cmdInfo.DBParametersInfo.Add("@gLAccountID", pGLAccountID);
				}

				if (pAccRefTypeID > 0)
				{
					sb.Append(" AND tblAccRefCategory.AccRefTypeID =@accRefTypeID ");
					//cmd.Parameters.AddWithValue("@accRefTypeID", pAccRefTypeID);
					cmdInfo.DBParametersInfo.Add("@accRefTypeID", pAccRefTypeID);
				}

				sb.Append(" GROUP BY tblGLAccount.GLAccountIDParent ");
				sb.Append(" ,tblAccRefCategory.AccRefTypeID ");
				sb.Append(" ,tblAccRef.AccRefCategoryID ");
				sb.Append(" ,tblGLAccountHistoryRef.AccRefID ");

				sb.Append(") AS HistoryRefSum ON tblGLAccount.GLAccountID = HistoryRefSum.GLAccountID ");

				//sb.Append(" INNER JOIN tblGLAccount ON tblGLAccountHistoryRef.GLAccountID = tblGLAccount.GLAccountID ");
				sb.Append(" INNER JOIN tblGLAccountType ON tblGLAccount.GLAccountTypeID = tblGLAccountType.GLAccountTypeID ");
				sb.Append(" INNER JOIN tblGLGroup ON tblGLAccount.GLGroupID = tblGLGroup.GLGroupID ");
				sb.Append(" INNER JOIN tblAccRef ON HistoryRefSum.AccRefID = tblAccRef.AccRefID ");
				sb.Append(" INNER JOIN tblAccRefCategory ON tblAccRef.AccRefCategoryID = tblAccRefCategory.AccRefCategoryID ");
				sb.Append(" INNER JOIN tblAccRefType ON tblAccRefCategory.AccRefTypeID = tblAccRefType.AccRefTypeID ");

				sb.Append(" WHERE 1=1 ");

				sb.Append(" ORDER BY tblGLAccount.GLAccountCode, tblGLAccount.GLAccountName ");
				sb.Append(" ,tblAccRefType.AccRefTypeSLNo, tblAccRefCategory.AccRefCategoryCode, tblAccRef.AccRefCode ");

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

				cObjList = DBQuery.ExecuteDBQuery<dcGLAccountHistoryRef>(dbq, dc);

			}
			catch { throw; }
			finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
			return cObjList;
		}


		public static List<dcGLAccountHistoryRef> GetGLAccountHistoryRefSumByAccRefList(int pCompanyID, int pAccYearID, int pAccRefTypeID
																				   , int pAccRefCategoryID, int pAccRefID, int pGLAccountID)
		{
			return GetGLAccountHistoryRefSumByAccRefList(pCompanyID, pAccYearID, pAccRefTypeID, pAccRefCategoryID, pAccRefID, pGLAccountID, null);
		}

		public static List<dcGLAccountHistoryRef> GetGLAccountHistoryRefSumByAccRefList(int pCompanyID, int pAccYearID, int pAccRefTypeID
																						   , int pAccRefCategoryID, int pAccRefID, int pGLAccountID, DBContext dc)
		{
			List<dcGLAccountHistoryRef> cObjList = new List<dcGLAccountHistoryRef>();

			bool isDCInit = false;
			try
			{
				isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

				int glAccountTypeID = GLAccountBL.GetGLAccountTypeID(pCompanyID, pGLAccountID, dc);

				//SqlCommand cmd = new SqlCommand();
				DBCommandInfo cmdInfo = new DBCommandInfo();
				StringBuilder sb = new StringBuilder();


				sb.Append(" SELECT tblGLAccountHistoryRef.AccRefID ");
				sb.Append(" , ISNULL(SUM(tblGLAccountHistoryRef.DebitAmtOpen),0) AS DebitAmtOpen ");
				sb.Append(" , ISNULL(SUM(tblGLAccountHistoryRef.CreditAmtOpen),0) AS CreditAmtOpen ");
				sb.Append(" , ISNULL(SUM(tblGLAccountHistoryRef.OpenAmt),0) AS OpenAmt ");

				sb.Append(" FROM tblGLAccountHistoryRef ");
				sb.Append(" INNER JOIN tblAccRef ON tblGLAccountHistoryRef.AccRefID = tblAccRef.AccRefID ");
				sb.Append(" INNER JOIN tblAccRefCategory ON tblAccRef.AccRefCategoryID = tblAccRefCategory.AccRefCategoryID ");
				sb.Append(" INNER JOIN tblGLAccount  ON tblGLAccountHistoryRef.GLAccountID  = tblGLAccount.GLAccountID   ");

				sb.Append(" WHERE 1=1 ");

				sb.Append(" AND tblGLAccountHistoryRef.CompanyID=@companyID ");
				//cmd.Parameters.AddWithValue("@companyID", pCompanyID);
				cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);

				if (pAccYearID > 0)
				{
					sb.Append(" AND tblGLAccountHistoryRef.AccYearID =@accYearID ");
					//cmd.Parameters.AddWithValue("@accYearID", pAccYearID);
					cmdInfo.DBParametersInfo.Add("@accYearID", pAccYearID);
				}

				if (pAccRefTypeID > 0)
				{
					sb.Append(" AND tblAccRefCategory.AccRefTypeID =@accRefTypeID ");
					//cmd.Parameters.AddWithValue("@accRefTypeID", pAccRefTypeID);
					cmdInfo.DBParametersInfo.Add("@accRefTypeID", pAccRefTypeID);
				}

				if (pAccRefCategoryID > 0)
				{
					sb.Append(" AND tblAccRef.AccRefCategoryID =@accRefCategoryID ");
					//cmd.Parameters.AddWithValue("@accRefCategoryID", pAccRefCategoryID);
					cmdInfo.DBParametersInfo.Add("@accRefCategoryID", pAccRefCategoryID);
				}

				if (pAccRefID > 0)
				{
					sb.Append(" AND tblGLAccountHistoryRef.AccRefID =@accRefID ");
					//cmd.Parameters.AddWithValue("@accRefID", pAccRefID);
					cmdInfo.DBParametersInfo.Add("@accRefID", pAccRefID);
				}

				if (pGLAccountID > 0)
				{
					if (glAccountTypeID == (int)GLAccountTypeEnum.ControlAccount)
					{
						sb.Append(" AND tblGLAccount.GLAccountIDParent=@glAccountID ");
					}
					else
					{
						sb.Append(" AND tblGLAccountHistoryRef.GLAccountID=@glAccountID ");
					}
					//cmd.Parameters.AddWithValue("@glAccountID", pGLAccountID);
					cmdInfo.DBParametersInfo.Add("@glAccountID", pGLAccountID);
				}


				sb.Append(" GROUP BY tblGLAccountHistoryRef.AccRefID ");

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

				cObjList = DBQuery.ExecuteDBQuery<dcGLAccountHistoryRef>(dbq, dc);

			}
			catch { throw; }
			finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
			return cObjList;
		}



		//public static List<dcGLAccountHistoryRef> GetGLAccountHistoryRefSumByAccRefList(int pCompanyID, int pAccYearID, int pAccRefTypeID
		//                                                                                 , int pAccRefCategoryID, int pAccRefID, int pGLAccountID, DBContext dc)
		//{
		//    List<dcGLAccountHistoryRef> cObjList = new List<dcGLAccountHistoryRef>();

		//    bool isDCInit = false;
		//    try
		//    {
		//        isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

		//        SqlCommand cmd = new SqlCommand();
		//        StringBuilder sb = new StringBuilder();

		//        sb.Append(" SELECT tblGLAccountHistoryRef.CompanyID, tblGLAccountHistoryRef.AccYearID ");
		//        sb.Append(", tblGLAccountHistoryRef.AccRefID, tblAccRef.AccRefCode, tblAccRef.AccRefNameShort, tblAccRef.AccRefName ");
		//        sb.Append(", tblAccRef.AccRefCategoryID, tblAccRefCategory.AccRefCategoryCode, tblAccRefCategory.AccRefCategoryNameShort, tblAccRefCategory.AccRefCategoryName ");
		//        sb.Append(", tblAccRefCategory.AccRefTypeID ");

		//        sb.Append(", HistoryRefSum.DebitAmtOpen ");
		//        sb.Append(", HistoryRefSum.CreditAmtOpen ");
		//        sb.Append(", HistoryRefSum.OpenAmt ");

		//        sb.Append(" FROM tblGLAccountHistoryRef ");

		//        sb.Append(" INNER JOIN ( ");

		//        sb.Append(" SELECT tblGLAccountHistoryRef.AccRefID ");
		//        sb.Append(" , ISNULL(SUM(tblGLAccountHistoryRef.DebitAmtOpen),0) AS DebitAmtOpen ");
		//        sb.Append(" , ISNULL(SUM(tblGLAccountHistoryRef.CreditAmtOpen),0) AS CreditAmtOpen ");
		//        sb.Append(" , ISNULL(SUM(tblGLAccountHistoryRef.OpenAmt),0) AS OpenAmt ");

		//        sb.Append(" FROM tblGLAccountHistoryRef ");
		//        sb.Append(" INNER JOIN tblAccRef ON tblGLAccountHistoryRef.AccRefID = tblAccRef.AccRefID ");
		//        sb.Append(" INNER JOIN tblAccRefCategory ON tblAccRef.AccRefCategoryID = tblAccRefCategory.AccRefCategoryID ");
		//        //sb.Append(" INNER JOIN tblGLAccount  ON tblGLAccountHistoryRef.GLAccountID  = tblGLAccount.GLAccountID   ");

		//        sb.Append(" WHERE 1=1 ");

		//        sb.Append(" AND tblGLAccountHistoryRef.CompanyID=@companyID ");
		//        cmd.Parameters.AddWithValue("@companyID", pCompanyID);

		//        if (pAccYearID > 0)
		//        {
		//            sb.Append(" AND tblGLAccountHistoryRef.AccYearID =@accYearID ");
		//            cmd.Parameters.AddWithValue("@accYearID", pAccYearID);
		//        }

		//        if (pAccRefTypeID > 0)
		//        {
		//            sb.Append(" AND tblAccRefCategory.AccRefTypeID =@accRefTypeID ");
		//            cmd.Parameters.AddWithValue("@accRefTypeID", pAccRefTypeID);
		//        }

		//        if (pAccRefCategoryID > 0)
		//        {
		//            sb.Append(" AND tblAccRef.AccRefCategoryID =@accRefCategoryID ");
		//            cmd.Parameters.AddWithValue("@accRefCategoryID", pAccRefCategoryID);
		//        }

		//        if (pAccRefID > 0)
		//        {
		//            sb.Append(" AND tblGLAccountHistoryRef.AccRefID =@accRefID ");
		//            cmd.Parameters.AddWithValue("@accRefID", pAccRefID);
		//        }

		//        if (pGLAccountID > 0)
		//        {
		//            sb.Append(" AND tblGLAccountHistoryRef.GLAccountID =@gLAccountID ");
		//            cmd.Parameters.AddWithValue("@gLAccountID", pGLAccountID);
		//        }


		//        sb.Append(" GROUP BY tblGLAccountHistoryRef.AccRefID ");

		//        sb.Append(") AS HistoryRefSum ON tblGLAccountHistoryRef.AccRefID = HistoryRefSum.AccRefID ");

		//        sb.Append(" INNER JOIN tblAccRef ON tblGLAccountHistoryRef.AccRefID = tblAccRef.AccRefID ");
		//        sb.Append(" INNER JOIN tblAccRefCategory ON tblAccRef.AccRefCategoryID = tblAccRefCategory.AccRefCategoryID ");


		//        sb.Append(" WHERE 1=1 ");
		//        sb.Append(" ORDER BY tblAccRef.AccRefCode, tblAccRef.AccRefName ");


		//        cmd.CommandType = CommandType.Text;
		//        cmd.CommandText = sb.ToString();

		//        DBQuery dbq = new DBQuery();
		//        dbq.DBQueryMode = DBQueryModeEnum.DBCommand;
		//        dbq.DBCommand = cmd;

		//        cObjList = DBQuery.ExecuteDBQuery<dcGLAccountHistoryRef>(dbq, dc);

		//    }
		//    catch { throw; }
		//    finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
		//    return cObjList;
		//}




		public static void UpdateSLNo(List<dcGLAccountHistoryRef> pListDetails)
		{
			int slNo = 0;
			foreach (dcGLAccountHistoryRef oDet in pListDetails)
			{
				if (oDet._RecordState != RecordStateEnum.Deleted)
				{
					slNo++;
					oDet.GLAccHistRefSLNo = slNo;
				}
			}
		}
		public static int Insert(dcGLAccountHistoryRef cObj)
		{
			return Insert(cObj, null);
		}

		public static int Insert(dcGLAccountHistoryRef cObj, DBContext dc)
		{
			bool isDCInit = false;
			int id = 0;

			cObj.TranAmt = cObj.DebitAmt - cObj.CreditAmt;

			isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
			using (DataContext dataContext = dc.NewDataContext())
			{
				id = dc.DoInsert<dcGLAccountHistoryRef>(cObj, true);
				if (id > 0) { cObj.GLAccHistRefID = id; }
			}
			DBContextManager.ReleaseDBContext(ref dc, isDCInit);
			return id;
		}

		public static bool Update(dcGLAccountHistoryRef cObj)
		{
			return Update(cObj, null);
		}

		public static bool Update(dcGLAccountHistoryRef cObj, DBContext dc)
		{
			bool isDCInit = false;
			int cnt = 0;
			cObj.TranAmt = cObj.DebitAmt - cObj.CreditAmt;
			isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
			using (DataContext dataContext = dc.NewDataContext())
			{
				cnt = dc.DoUpdate<dcGLAccountHistoryRef>(cObj);
			}
			DBContextManager.ReleaseDBContext(ref dc, isDCInit);
			return cnt > 0;
		}

		public static bool Delete(int pGLAccHistRefID)
		{
			return Delete(pGLAccHistRefID, null);
		}
		public static bool Delete(int pGLAccHistRefID, DBContext dc)
		{
			dcGLAccountHistoryRef cObj = new dcGLAccountHistoryRef();
			cObj.GLAccHistRefID = pGLAccHistRefID;
			bool isDCInit = false;
			int cnt = 0;
			isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
			using (DataContext dataContext = dc.NewDataContext())
			{
				cnt = dc.DoDelete<dcGLAccountHistoryRef>(cObj);
			}
			DBContextManager.ReleaseDBContext(ref dc, isDCInit);
			return cnt > 0;
		}

		public static bool SaveList(List<dcGLAccountHistoryRef> detList)
		{
			return SaveList(detList, null);
		}

		public static bool SaveList(List<dcGLAccountHistoryRef> detList, DBContext dc)
		{

			bool bStatus = false;
			bool isDCInit = false;
			bool isTransInit = false;
			isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
			isTransInit = dc.StartTransaction();
			foreach (dcGLAccountHistoryRef oDet in detList)
			{
				switch (oDet._RecordState)
				{
					case RecordStateEnum.Added:
						int newID = Insert(oDet, dc);
						break;
					case RecordStateEnum.Edited:
						bool e = Update(oDet, dc);
						break;
					case RecordStateEnum.Deleted:
						bool d = Delete(oDet.GLAccHistRefID, dc);
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



		public static bool UpdateGLHistoryRefList(int pCompanyID, int pAccYearID, int pGLAccountID, int pAccRefTypeID, List<dcGLAccountHistoryRef> pGLHistoryRefList)
		{
			return UpdateGLHistoryRefList(pCompanyID, pAccYearID, pGLAccountID, pAccRefTypeID, pGLHistoryRefList, null);
		}

		public static bool UpdateGLHistoryRefList(int pCompanyID, int pAccYearID, int pGLAccountID, int pAccRefTypeID, List<dcGLAccountHistoryRef> pGLHistoryRefList, DBContext dc)
		{
			bool isDCInit = false;
			bool isTransInit = false;
			bool bStatus = false;
			//try
			{
				isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

				List<dcGLAccountHistoryRef> curOpenList = GetGLAccountHistoryRefList(pCompanyID, pAccYearID, pGLAccountID, pAccRefTypeID, dc);

				isTransInit = dc.StartTransaction();

				foreach (dcGLAccountHistoryRef accHistRef in pGLHistoryRefList)
				{

					dcGLAccountHistoryRef curOpen = curOpenList.SingleOrDefault(c => c.AccYearID == pCompanyID
																				   && c.AccYearID == pAccYearID
																				   && c.GLAccountID == accHistRef.GLAccountID
																				   && c.AccRefID == accHistRef.AccRefID);

					dcGLAccountHistoryRef cObj = new dcGLAccountHistoryRef();

					cObj.GLAccountID = accHistRef.GLAccountID;
					cObj.AccYearID = pAccYearID;
					cObj.CompanyID = pCompanyID;
					cObj.AccRefTypeID = accHistRef.AccRefTypeID;
					cObj.AccRefID = accHistRef.AccRefID;

					cObj.GLAccHistRefSLNo = accHistRef.GLAccHistRefSLNo;

					cObj.DebitAmtOpen = accHistRef.DebitAmtOpen;
					cObj.CreditAmtOpen = accHistRef.CreditAmtOpen;

					cObj.DebitAmtOpen = cObj.DebitAmtOpen <= 0 ? 0 : cObj.DebitAmtOpen;
					cObj.CreditAmtOpen = cObj.CreditAmtOpen <= 0 ? 0 : cObj.CreditAmtOpen;

					cObj.CreditAmtOpen = cObj.DebitAmtOpen > 0 ? 0 : cObj.CreditAmtOpen;

					cObj.OpenAmt = cObj.DebitAmtOpen - cObj.CreditAmtOpen;

					if (curOpen == null)
					{
						accHistRef.GLAccHistRefID = dc.DoInsert<dcGLAccountHistoryRef>(cObj, true);
						curOpenList.Add(cObj);
					}
					else
					{
						if (accHistRef._RecordState == RecordStateEnum.Deleted)
						{
							Delete(curOpen.GLAccHistRefID, dc);
							curOpenList.Remove(curOpen);
						}
						else
						{
							cObj.GLAccHistRefID = curOpen.GLAccHistRefID;

							//decimal dbAmt = accHistRef.DebitAmtOpen + curOpen.DebitAmtOpen;
							//decimal crAmt = accHistRef.CreditAmtOpen + curOpen.CreditAmtOpen;
							decimal dbAmt = accHistRef.DebitAmtOpen;
							decimal crAmt = accHistRef.CreditAmtOpen;
							decimal trAmt = dbAmt - crAmt;

							cObj.DebitAmtOpen = trAmt >= 0 ? Math.Abs(trAmt) : 0;
							cObj.CreditAmtOpen = trAmt < 0 ? Math.Abs(trAmt) : 0;
							cObj.OpenAmt = trAmt;

							dc.DoUpdate<dcGLAccountHistoryRef>(cObj);
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


		//public static bool UpdateGLAccountHistory(int pCompanyID, int pAccYearID, int pGLAccountID, decimal pDebitAmt, decimal pCreditAmt)
		//{
		//    return UpdateGLAccountHistory(pCompanyID, pAccYearID, pGLAccountID, pDebitAmt, pCreditAmt, null);
		//}
		//public static bool UpdateGLAccountHistory(int pCompanyID, int pAccYearID, int pGLAccountID, decimal pDebitAmt, decimal pCreditAmt , DBContext dc)
		//{
		//    decimal tAmt = pDebitAmt;
		//    DebitCreditEnum drCr = DebitCreditEnum.Debit;
		//    if (pCreditAmt > 0)
		//    {
		//        drCr = DebitCreditEnum.Credit;
		//        tAmt = pCreditAmt;
		//    }

		//    return UpdateGLAccountHistory(pCompanyID, pAccYearID, pGLAccountID, tAmt, drCr, null);
		//}



		//public static bool UpdateGLAccountHistory(int pCompanyID, int pAccYearID, int pGLAccountID, decimal pAmount, DebitCreditEnum pDrCr)
		//{
		//    return UpdateGLAccountHistory(pCompanyID, pGLAccountID, pAccYearID, pAmount, pDrCr, null);
		//}
		//public static bool UpdateGLAccountHistory(int pCompanyID, int pAccYearID, int pGLAccountID, decimal pAmount, DebitCreditEnum pDrCr, DBContext dc)
		//{
		//    bool isDCInit = false;
		//    bool bStatus = false;
		//    bool isAdd = false;
		//    //try
		//    {
		//        isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
		//        dcGLAccountHistoryRef accHist = GetGLAccountHistoryByID(pGLAccountID, pAccYearID, dc);
		//        if (accHist == null)
		//        {
		//            accHist = new dcGLAccountHistoryRef();
		//            accHist.GLAccountID = pGLAccountID;
		//            accHist.AccYearID = pAccYearID;
		//            accHist.CompanyID = pCompanyID;
		//            isAdd = true;
		//        }


		//        if (pDrCr == DebitCreditEnum.Debit)
		//        {
		//            accHist.DebitAmtOpen = pAmount;
		//            accHist.CreditAmtOpen = 0;
		//        }
		//        else
		//        {
		//            accHist.DebitAmtOpen = 0;
		//            accHist.CreditAmtOpen = pAmount;
		//        }

		//        if (isAdd)
		//        {
		//           int id= dc.DoInsert<dcGLAccountHistoryRef>(accHist, true);
		//        }
		//        else
		//        {
		//            accHist.GLAccHistID = accHist.GLAccHistID;
		//            dc.DoUpdate<dcGLAccountHistoryRef>(accHist);
		//        }
		//        bStatus = true;
		//    }
		//    //catch { throw; }
		//    //finally 
		//    {
		//        DBContextManager.ReleaseDBContext(ref dc, isDCInit);
		//    }
		//    return bStatus;
		//}


	}
}
