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
using PG.DBClass.AccountingDC.GeneralLedgerDC;

namespace PG.BLLibrary.AccountingBL.GeneralLedgerBL
{

    /// <summary>
    /// AppAppInfoBL
    /// Last update By Moni, Date 10-03-2015
    /// </summary>
    public class GLAccountRefCategoryBL
    {
        public static string GetAccountRefCategory_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT tblGLAccountRefCategory.* ");
            sb.Append(", tblAccRefCategory.AccRefCategoryCode, tblAccRefCategory.AccRefCategoryName ");
            sb.Append(", tblAccRefType.AccRefTypeID, tblAccRefType.AccRefTypeName ");
            sb.Append(" FROM tblGLAccountRefCategory ");
            sb.Append(" INNER JOIN tblAccRefCategory ON tblGLAccountRefCategory.AccRefCategoryID = tblAccRefCategory.AccRefCategoryID ");
            sb.Append(" INNER JOIN tblAccRefType ON tblAccRefCategory.AccRefTypeID = tblAccRefType.AccRefTypeID ");
            sb.Append(" WHERE 1=1 ");
            return sb.ToString();
        }


        public static List<dcGLAccountRefCategory> GetAccountRefCategoryList(int pGLAccountID, int pAccRefTypeID)
        {
            return GetAccountRefCategoryList(pGLAccountID, pAccRefTypeID, 0, null);
        }

        public static List<dcGLAccountRefCategory> GetAccountRefCategoryList(int pGLAccountID, int pAccRefTypeID, DBContext dc)
        {
            return GetAccountRefCategoryList(pGLAccountID, pAccRefTypeID, 0, dc);
        }

        public static List<dcGLAccountRefCategory> GetAccountRefCategoryList(int pGLAccountID, int pAccRefTypeID, int pAccRefCategoryID)
        {
            return GetAccountRefCategoryList(pGLAccountID, pAccRefTypeID, pAccRefCategoryID, null);
        }
        public static List<dcGLAccountRefCategory> GetAccountRefCategoryList(int pGLAccountID, int pAccRefTypeID, int pAccRefCategoryID, DBContext dc)
        {
            List<dcGLAccountRefCategory> cObjList = new List<dcGLAccountRefCategory>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetAccountRefCategory_SQLString());

                if (pGLAccountID > 0)
                {
                    sb.Append(" AND tblGLAccountRefCategory.GLAccountID = @GLAccountID");
                    cmdInfo.DBParametersInfo.Add("@GLAccountID", pGLAccountID);
                }

                if (pAccRefTypeID > 0)
                {
                    sb.Append(" AND tblAccRefCategory.AccRefTypeID = @accRefTypeID");
                    cmdInfo.DBParametersInfo.Add("@accRefTypeID", pAccRefTypeID);
                }


                if (pAccRefCategoryID > 0)
                {
                    sb.Append(" AND tblGLAccountRefCategory.AccRefCategoryID = @accRefCategoryID");
                    cmdInfo.DBParametersInfo.Add("@accRefCategoryID", pAccRefCategoryID);
                }

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcGLAccountRefCategory>(dbq, dc).ToList();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcGLAccountRefCategory> GetAccountRefCategoryList(DBQuery dbq, DBContext dc)
        {
            List<dcGLAccountRefCategory> cObjList = new List<dcGLAccountRefCategory>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                if (dbq == null)
                {
                    dbq = new DBQuery();
                    //dbq.OrderBy = "PeriodStartDate";
                }
                cObjList = DBQuery.ExecuteDBQuery<dcGLAccountRefCategory>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }


        public static dcGLAccountRefCategory GetAccountRefCategoryByID(int pGLAccountRefCategoryID)
        {
            return GetAccountRefCategoryByID(pGLAccountRefCategoryID, null);
        }
        public static dcGLAccountRefCategory GetAccountRefCategoryByID(int pGLAccountRefCategoryID, DBContext dc)
        {
            dcGLAccountRefCategory cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetAccountRefCategory_SQLString());
                sb.Append(" AND tblGLAccountRefCategory.GLAccountRefCategoryID=@pGLAccountRefCategoryID");
                cmdInfo.DBParametersInfo.Add("@pGLAccountRefCategoryID", pGLAccountRefCategoryID);


                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObj = DBQuery.ExecuteDBQuery<dcGLAccountRefCategory>(dbq, dc).FirstOrDefault();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObj;
        }
        public static void UpdateSLNo(List<dcGLAccountRefCategory> pListDetails)
        {
            int slNo = 0;
            foreach (dcGLAccountRefCategory oDet in pListDetails)
            {
                if (oDet._RecordState != RecordStateEnum.Deleted)
                {
                    slNo++;
                    oDet.GLAccountRefCategorySLNo = slNo;
                }
            }
        }

        public static int Insert(dcGLAccountRefCategory cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcGLAccountRefCategory cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcGLAccountRefCategory>(cObj, true);
                if (id > 0) { cObj.GLAccountRefCategoryID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcGLAccountRefCategory cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcGLAccountRefCategory cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcGLAccountRefCategory>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pGLAccountRefCategoryID)
        {
            return Delete(pGLAccountRefCategoryID, null);
        }
        public static bool Delete(int pGLAccountRefCategoryID, DBContext dc)
        {
            dcGLAccountRefCategory cObj = new dcGLAccountRefCategory();
            cObj.GLAccountRefCategoryID = pGLAccountRefCategoryID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcGLAccountRefCategory>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool SaveList(List<dcGLAccountRefCategory> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcGLAccountRefCategory> detList, DBContext dc)
        {

            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcGLAccountRefCategory oDet in detList)
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
                        bool d = Delete(oDet.GLAccountRefCategoryID, dc);
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



        public static bool UpdateGLAccRefCategoryList(int pGLAccountID, int pAccRefTypeID, List<dcGLAccountRefCategory> pGLAccRefCategoryList)
        {
            return UpdateGLAccRefCategoryList(pGLAccountID, pAccRefTypeID, pGLAccRefCategoryList, null);
        }

        public static bool UpdateGLAccRefCategoryList(int pGLAccountID, int pAccRefTypeID, List<dcGLAccountRefCategory> pGLAccRefCategoryList, DBContext dc)
        {
            bool isDCInit = false;
            bool isTransInit = false;
            bool bStatus = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                List<dcGLAccountRefCategory> curCatList = GetAccountRefCategoryList(pGLAccountID, pAccRefTypeID, dc);

                isTransInit = dc.StartTransaction();

                foreach (dcGLAccountRefCategory accRefCat in pGLAccRefCategoryList)
                {

                    dcGLAccountRefCategory curCat = curCatList.SingleOrDefault(c => c.GLAccountID == accRefCat.GLAccountID
                                                                                   && c.AccRefCategoryID == accRefCat.AccRefCategoryID);

                    dcGLAccountRefCategory cObj = new dcGLAccountRefCategory();

                    cObj.GLAccountID = accRefCat.GLAccountID;
                    cObj.AccRefCategoryID = accRefCat.AccRefCategoryID;
                    cObj.IsMandatory = accRefCat.IsMandatory;
                    cObj.IsDefault = accRefCat.IsDefault;
                    cObj.GLAccountRefCategorySLNo = accRefCat.GLAccountRefCategorySLNo;


                    if (curCat == null)
                    {
                        accRefCat.GLAccountRefCategoryID = dc.DoInsert<dcGLAccountRefCategory>(cObj, true);
                        curCatList.Add(cObj);
                    }
                    else
                    {
                        if (accRefCat._RecordState == RecordStateEnum.Deleted)
                        {
                            Delete(curCat.GLAccountRefCategoryID, dc);
                            curCatList.Remove(curCat);
                        }
                        else
                        {
                            cObj.GLAccountRefCategoryID = curCat.GLAccountRefCategoryID;

    

                            dc.DoUpdate<dcGLAccountRefCategory>(cObj);
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
